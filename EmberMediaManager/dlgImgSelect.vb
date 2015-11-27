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

    Private DoOnlySeason As Integer = -1

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
    Private tDBElementResult As Database.DBElement

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
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual

        'Set panel sizes based on "Fields" settings
        pnlSubImages.Width = iSubImage_DistanceLeft + iSubImage_Size_Panel.Width + 20
        pnlTopImages.Height = iTopImage_DistanceTop + iTopImage_Size_Panel.Height + 20
    End Sub

    Public Overloads Function ShowDialog(ByVal DBElement As Database.DBElement, ByRef SearchResultsContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifier As Structures.ScrapeModifier) As DialogResult
        tSearchResultsContainer = SearchResultsContainer
        tDBElementResult = CType(DBElement.CloneDeep, Database.DBElement)
        tScrapeModifier = ScrapeModifier

        Select Case DBElement.ContentType
            Case Enums.ContentType.TVShow
                If ScrapeModifier.withEpisodes OrElse ScrapeModifier.withSeasons Then
                    tContentType = Enums.ContentType.TV
                Else
                    tContentType = Enums.ContentType.TVShow
                End If
            Case Enums.ContentType.TVSeason
                tContentType = Enums.ContentType.TVSeason
                DoOnlySeason = DBElement.TVSeason.Season
            Case Else
                tContentType = DBElement.ContentType
        End Select

        SetParameters()

        Return ShowDialog()
    End Function

    Private Sub dlgImageSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler MouseWheel, AddressOf MouseWheelEvent

        Functions.PNLDoubleBuffer(pnlImgSelectMain)

        SetUp()
    End Sub

    Private Sub dlgImageSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
        lblStatus.Text = Master.eLang.GetString(953, "(Down)Loading Default Images...")
        pbStatus.Style = ProgressBarStyle.Marquee
        lblStatus.Visible = True
        pbStatus.Visible = True

        bwImgDefaults.WorkerReportsProgress = False
        bwImgDefaults.WorkerSupportsCancellation = True
        bwImgDefaults.RunWorkerAsync()
    End Sub

    Private Sub dlgImgSelectNew_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlLoading.Location = New Point(CInt((pnlImgSelectMain.Left + (pnlImgSelectMain.Width / 2)) - pnlLoading.Width / 2), CInt((pnlImgSelectMain.Top + (pnlImgSelectMain.Height / 2)) - pnlLoading.Height / 2))
        tmrReorderMainList.Stop()
        tmrReorderMainList.Start()
    End Sub

    Private Sub dlgImgSelect_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyValue = Keys.Delete Then
            If btnRemoveSubImage.Enabled Then
                btnRemoveSubImage.PerformClick()
            ElseIf btnRemoveTopImage.Enabled Then
                btnRemoveTopImage.PerformClick()
            End If
        End If
    End Sub

    Private Sub tmrReorderMainList_Tick(sender As Object, e As EventArgs) Handles tmrReorderMainList.Tick
        tmrReorderMainList.Stop()
        ReorderImageList()
    End Sub

    Private Sub bwImgDefaults_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDefaults.DoWork
        SetDefaults()
        DownloadDefaultImages()
        e.Cancel = bwImgDefaults.CancellationPending
    End Sub

    Private Sub bwImgDefaults_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDefaults.RunWorkerCompleted
        If Not e.Cancelled Then
            CreateTopImages()

            lblStatus.Text = Master.eLang.GetString(954, "(Down)Loading New Images...")
            pbStatus.Style = ProgressBarStyle.Continuous
            bwImgDownload.WorkerReportsProgress = True
            bwImgDownload.WorkerSupportsCancellation = True
            bwImgDownload.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwImgDownload_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDownload.DoWork
        e.Cancel = DownloadAllImages()
    End Sub

    Private Sub bwImgDownload_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwImgDownload.ProgressChanged
        If e.UserState.ToString = "progress" Then
            pbStatus.Value = e.ProgressPercentage
        ElseIf e.UserState.ToString = "max" Then
            pbStatus.Value = 0
            pbStatus.Maximum = e.ProgressPercentage
        ElseIf DirectCast(e.UserState, Enums.ModifierType) = currTopImage.ImageType Then
            CreateImageList(currTopImage)
        ElseIf DirectCast(e.UserState, Enums.ModifierType) = currSubImage.ImageType Then
            CreateImageList(currSubImage)
        End If
    End Sub

    Private Sub bwImgDownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDownload.RunWorkerCompleted
        lblStatus.Visible = False
        pbStatus.Visible = False
    End Sub

    Public Sub SetDefaults()
        Images.SetDefaultImages(tDBElementResult, tDefaultImagesContainer, tSearchResultsContainer, tScrapeModifier, tDefaultSeasonImagesContainer, tDefaultEpisodeImagesContainer, IsAutoScraper:=False)
    End Sub

    Private Function DownloadDefaultImages() As Boolean

        'Episode Fanart
        If DoEpisodeFanart Then
            For Each tImg As Database.DBElement In tDBElementResult.Episodes.OrderBy(Function(s) s.TVEpisode.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                tImg.ImagesContainer.Fanart.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Episode Poster
        If DoEpisodePoster Then
            For Each tImg As Database.DBElement In tDBElementResult.Episodes.OrderBy(Function(s) s.TVEpisode.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                tImg.ImagesContainer.Poster.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Main Banner
        If DoMainBanner OrElse DoAllSeasonsBanner OrElse DoSeasonBanner Then
            tDBElementResult.ImagesContainer.Banner.LoadAndCache(tContentType, False, True)
        End If

        'Main CharacterArt
        If DoMainCharacterArt Then
            tDBElementResult.ImagesContainer.CharacterArt.LoadAndCache(tContentType, False, True)
        End If

        'Main ClearArt
        If DoMainClearArt Then
            tDBElementResult.ImagesContainer.ClearArt.LoadAndCache(tContentType, False, True)
        End If

        'Main ClearLogo
        If DoMainClearLogo Then
            tDBElementResult.ImagesContainer.ClearLogo.LoadAndCache(tContentType, False, True)
        End If

        'Main DiscArt
        If DoMainDiscArt Then
            tDBElementResult.ImagesContainer.DiscArt.LoadAndCache(tContentType, False, True)
        End If

        'Main Extrafanarts
        If DoMainExtrafanarts AndAlso tDBElementResult.ImagesContainer.Extrafanarts.Count > 0 Then
            For Each tImg In tDBElementResult.ImagesContainer.Extrafanarts
                tImg.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Main Extrathumbs
        If DoMainExtrathumbs AndAlso tDBElementResult.ImagesContainer.Extrathumbs.Count > 0 Then
            For Each tImg In tDBElementResult.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                tImg.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Main Fanart
        If DoMainFanart OrElse DoAllSeasonsFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart Then
            tDBElementResult.ImagesContainer.Fanart.LoadAndCache(tContentType, False, True)
        End If

        'Main Landscape
        If DoMainLandscape OrElse DoAllSeasonsLandscape OrElse DoSeasonLandscape Then
            tDBElementResult.ImagesContainer.Landscape.LoadAndCache(tContentType, False, True)
        End If

        'Main Poster
        If DoMainPoster OrElse DoAllSeasonsPoster OrElse DoEpisodePoster OrElse DoSeasonPoster Then
            tDBElementResult.ImagesContainer.Poster.LoadAndCache(tContentType, False, True)
        End If

        'Season Banner
        If DoSeasonBanner Then
            For Each tImg As Database.DBElement In tDBElementResult.Seasons.OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Banner.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Season Fanart
        If DoSeasonFanart Then
            For Each tImg As Database.DBElement In tDBElementResult.Seasons.OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Fanart.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Season Landscape
        If DoSeasonLandscape Then
            For Each tImg As Database.DBElement In tDBElementResult.Seasons.OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Landscape.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Season Poster
        If DoSeasonPoster Then
            For Each tImg As Database.DBElement In tDBElementResult.Seasons.OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Poster.LoadAndCache(tContentType, False, True)
            Next
        End If
    End Function

    Private Function DownloadAllImages() As Boolean
        Dim iProgress As Integer = 1

        bwImgDownload.ReportProgress(tSearchResultsContainer.EpisodeFanarts.Count + tSearchResultsContainer.EpisodePosters.Count +
                                        tSearchResultsContainer.MainBanners.Count + tSearchResultsContainer.MainCharacterArts.Count +
                                        tSearchResultsContainer.MainClearArts.Count + tSearchResultsContainer.MainClearLogos.Count +
                                        tSearchResultsContainer.MainDiscArts.Count + tSearchResultsContainer.MainFanarts.Count +
                                        tSearchResultsContainer.MainLandscapes.Count + tSearchResultsContainer.MainPosters.Count +
                                        tSearchResultsContainer.SeasonBanners.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count +
                                        tSearchResultsContainer.SeasonFanarts.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count +
                                        tSearchResultsContainer.SeasonLandscapes.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count +
                                        tSearchResultsContainer.SeasonPosters.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count, "max")

        'Main Posters
        If DoMainPoster OrElse DoAllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainPosters
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainPoster = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsPoster)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainPoster)

        'Main Banners
        If DoMainBanner OrElse DoAllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainBanners
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainBanner = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsBanner)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainBanner)

        'Main CharacterArts
        If DoMainCharacterArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainCharacterArt = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainCharacterArt)

        'Main ClearArts
        If DoMainClearArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainClearArt = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainClearArt)

        'Main ClearLogos
        If DoMainClearLogo Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainClearLogo = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainClearLogo)

        'Main Discarts
        If DoMainDiscArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainDiscArts
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainDiscArt = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainDiscArt)

        'Main Fanarts
        If DoMainFanart OrElse DoMainExtrafanarts OrElse DoMainExtrathumbs OrElse DoAllSeasonsFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainFanart = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsFanart)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodeFanart)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainExtrafanarts)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainExtrathumbs)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainFanart)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonFanart)

        'Main Landscapes
        If DoMainLandscape OrElse DoAllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainLandscape = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsLandscape)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainLandscape)

        'Season Banners
        If DoSeasonBanner OrElse DoAllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedSeasonBanner = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsBanner)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonBanner)

        'Season Fanarts
        If DoSeasonFanart OrElse DoAllSeasonsFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedSeasonFanart = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsFanart)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonFanart)

        'Season Landscapes
        If DoSeasonLandscape OrElse DoAllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedSeasonLandscape = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsLandscape)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonLandscape)

        'Season Posters
        If DoSeasonPoster OrElse DoAllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) If(Not DoOnlySeason = -1, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedSeasonPoster = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsPoster)
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonPoster)

        'Episode Fanarts
        If DoEpisodeFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodeFanarts
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedEpisodeFanart = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodeFanart)

        'Episode Posters
        If DoEpisodePoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedEpisodePoster = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodePoster)

        Return False
    End Function

    Private Sub SetUp()
        btnOK.Text = Master.eLang.GetString(179, "OK")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

    Private Sub CreateImageList(ByVal tTag As iTag)
        ClearImageList()

        currListImageSelectedSeason = tTag.iSeason
        currListImageSelectedImageType = tTag.ImageType

        pnlLoading.Visible = True

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

        pnlLoading.Visible = False
        Application.DoEvents()

        Select Case tTag.ImageType
            Case Enums.ModifierType.AllSeasonsBanner
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = 999)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsFanart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) f.Season = 999)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsLandscape
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = 999)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsPoster
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = 999)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.EpisodeFanart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.EpisodeFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.EpisodeFanart)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.EpisodeFanart)
                    iCount += 1
                Next
            Case Enums.ModifierType.EpisodePoster
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                    AddListImage(tImage, iCount, Enums.ModifierType.EpisodePoster)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainBanner
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                    AddListImage(tImage, iCount, Enums.ModifierType.MainBanner)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainCharacterArt
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                    AddListImage(tImage, iCount, Enums.ModifierType.MainCharacterArt)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainClearArt
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                    AddListImage(tImage, iCount, Enums.ModifierType.MainClearArt)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainClearLogo
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                    AddListImage(tImage, iCount, Enums.ModifierType.MainClearLogo)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainDiscArt
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainDiscArts
                    AddListImage(tImage, iCount, Enums.ModifierType.MainDiscArt)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainExtrafanarts
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.MainExtrafanarts)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainExtrathumbs
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.MainExtrathumbs)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainFanart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.MainFanart)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainLandscape
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                    AddListImage(tImage, iCount, Enums.ModifierType.MainLandscape)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainPoster
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                    AddListImage(tImage, iCount, Enums.ModifierType.MainPoster)
                    iCount += 1
                Next
            Case Enums.ModifierType.SeasonBanner
                Dim iSeason As Integer = tTag.iSeason
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = iSeason)
                    AddListImage(tImage, iCount, Enums.ModifierType.SeasonBanner)
                    iCount += 1
                Next
            Case Enums.ModifierType.SeasonFanart
                Dim iSeason As Integer = tTag.iSeason
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) f.Season = iSeason)
                    AddListImage(tImage, iCount, Enums.ModifierType.SeasonFanart)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.SeasonFanart, iSeason)
                    iCount += 1
                Next
            Case Enums.ModifierType.SeasonLandscape
                Dim iSeason As Integer = tTag.iSeason
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = iSeason)
                    AddListImage(tImage, iCount, Enums.ModifierType.SeasonLandscape)
                    iCount += 1
                Next
            Case Enums.ModifierType.SeasonPoster
                Dim iSeason As Integer = tTag.iSeason
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = iSeason)
                    AddListImage(tImage, iCount, Enums.ModifierType.SeasonPoster)
                    iCount += 1
                Next
        End Select
    End Sub

    Private Sub CreateSubImages()
        Dim iCount As Integer = 0

        ClearSubImages()

        If currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts AndAlso DoMainExtrafanarts Then
            For Each img In tDBElementResult.ImagesContainer.Extrafanarts
                AddSubImage(img, iCount, Enums.ModifierType.MainExtrafanarts, -1)
                iCount += 1
            Next
            If tDefaultImagesContainer.Extrafanarts.Count > 0 Then btnRestoreSubImage.Enabled = True
        ElseIf currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs AndAlso DoMainExtrathumbs Then
            tDBElementResult.ImagesContainer.SortExtrathumbs()
            For Each img In tDBElementResult.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                img.Index = iCount
                AddSubImage(img, iCount, Enums.ModifierType.MainExtrathumbs, -1)
                iCount += 1
            Next
            If tDefaultImagesContainer.Extrathumbs.Count > 0 Then btnRestoreSubImage.Enabled = True
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonBanner AndAlso DoSeasonBanner Then
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) f.TVSeason.Season = 999)
                AddSubImage(sSeason.ImagesContainer.Banner, iCount, Enums.ModifierType.AllSeasonsBanner, sSeason.TVSeason.Season)
                iCount += 1
            Next
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                AddSubImage(sSeason.ImagesContainer.Banner, iCount, Enums.ModifierType.SeasonBanner, sSeason.TVSeason.Season)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonFanart AndAlso DoSeasonFanart Then
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) f.TVSeason.Season = 999)
                AddSubImage(sSeason.ImagesContainer.Fanart, iCount, Enums.ModifierType.AllSeasonsFanart, sSeason.TVSeason.Season)
                iCount += 1
            Next
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                AddSubImage(sSeason.ImagesContainer.Fanart, iCount, Enums.ModifierType.SeasonFanart, sSeason.TVSeason.Season)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonLandscape AndAlso DoSeasonLandscape Then
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) f.TVSeason.Season = 999)
                AddSubImage(sSeason.ImagesContainer.Landscape, iCount, Enums.ModifierType.AllSeasonsLandscape, sSeason.TVSeason.Season)
                iCount += 1
            Next
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                AddSubImage(sSeason.ImagesContainer.Landscape, iCount, Enums.ModifierType.SeasonLandscape, sSeason.TVSeason.Season)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonPoster AndAlso DoSeasonPoster Then
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
                ComboBoxItems.Clear()
                If DoMainExtrafanarts Then ComboBoxItems.Add(Master.eLang.GetString(992, "Extrafanarts"), Enums.ModifierType.MainExtrafanarts)
                If DoMainExtrathumbs Then ComboBoxItems.Add(Master.eLang.GetString(153, "Extrathumbs"), Enums.ModifierType.MainExtrathumbs)
                cbSubImageType.DataSource = ComboBoxItems.ToList
                cbSubImageType.DisplayMember = "Key"
                cbSubImageType.ValueMember = "Value"
                If cbSubImageType.Items.Count > 0 Then
                    cbSubImageType.SelectedIndex = 0
                    cbSubImageType.Enabled = True
                End If
            Case Is = Enums.ContentType.TV, Enums.ContentType.TVShow
                ComboBoxItems.Clear()
                If DoMainExtrafanarts Then ComboBoxItems.Add(Master.eLang.GetString(992, "Extrafanarts"), Enums.ModifierType.MainExtrafanarts)
                If DoMainExtrathumbs Then ComboBoxItems.Add(Master.eLang.GetString(153, "Extrathumbs"), Enums.ModifierType.MainExtrathumbs)
                If DoSeasonBanner Then ComboBoxItems.Add(Master.eLang.GetString(1017, "Season Banner"), Enums.ModifierType.SeasonBanner)
                If DoSeasonFanart Then ComboBoxItems.Add(Master.eLang.GetString(686, "Season Fanart"), Enums.ModifierType.SeasonFanart)
                If DoSeasonLandscape Then ComboBoxItems.Add(Master.eLang.GetString(1018, "Season Landscape"), Enums.ModifierType.SeasonLandscape)
                If DoSeasonPoster Then ComboBoxItems.Add(Master.eLang.GetString(685, "Season Poster"), Enums.ModifierType.SeasonPoster)
                cbSubImageType.DataSource = ComboBoxItems.ToList
                cbSubImageType.DisplayMember = "Key"
                cbSubImageType.ValueMember = "Value"
                If cbSubImageType.Items.Count > 0 Then
                    cbSubImageType.SelectedIndex = 0
                    cbSubImageType.Enabled = True
                End If
        End Select

        'If we don't have any TopImage we can hide the panel (this should only be True while Extrafanarts or Extrathumbs scraping)
        If Not noTopImages Then
            DoSelectTopImage(0, CType(pnlTopImage_Panel(0).Tag, iTag))
        Else
            pnlImgSelectTop.Visible = False
        End If
    End Sub

    Private Sub AddListImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -1)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason)

        ReDim Preserve pnlImageList_Panel(iIndex)
        ReDim Preserve pbImageList_Image(iIndex)
        ReDim Preserve lblImageList_DiscType(iIndex)
        ReDim Preserve lblImageList_Language(iIndex)
        ReDim Preserve lblImageList_Resolution(iIndex)
        ReDim Preserve lblImageList_Scraper(iIndex)
        pnlImageList_Panel(iIndex) = New Panel()
        pbImageList_Image(iIndex) = New PictureBox()
        lblImageList_DiscType(iIndex) = New Label()
        lblImageList_Language(iIndex) = New Label()
        lblImageList_Resolution(iIndex) = New Label()
        lblImageList_Scraper(iIndex) = New Label()
        pbImageList_Image(iIndex).Name = iIndex.ToString
        pnlImageList_Panel(iIndex).Name = iIndex.ToString
        lblImageList_DiscType(iIndex).Name = iIndex.ToString
        lblImageList_Language(iIndex).Name = iIndex.ToString
        lblImageList_Resolution(iIndex).Name = iIndex.ToString
        lblImageList_Scraper(iIndex).Name = iIndex.ToString
        pnlImageList_Panel(iIndex).Size = iImageList_Size_Panel
        pbImageList_Image(iIndex).Size = iImageList_Size_Image
        lblImageList_DiscType(iIndex).Size = iImageList_Size_DiscType
        lblImageList_Language(iIndex).Size = iImageList_Size_Language
        lblImageList_Resolution(iIndex).Size = iImageList_Size_Resolution
        lblImageList_Scraper(iIndex).Size = iImageList_Size_Scraper
        pnlImageList_Panel(iIndex).BackColor = Color.White
        pnlImageList_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        pbImageList_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        lblImageList_DiscType(iIndex).AutoSize = False
        lblImageList_DiscType(iIndex).BackColor = Color.White
        lblImageList_DiscType(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblImageList_DiscType(iIndex).Text = tImage.DiscType
        lblImageList_Language(iIndex).AutoSize = False
        lblImageList_Language(iIndex).BackColor = Color.White
        lblImageList_Language(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblImageList_Language(iIndex).Text = tTag.Image.LongLang
        lblImageList_Resolution(iIndex).AutoSize = False
        lblImageList_Resolution(iIndex).BackColor = Color.White
        lblImageList_Resolution(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblImageList_Resolution(iIndex).Text = tTag.strResolution
        lblImageList_Scraper(iIndex).AutoSize = False
        lblImageList_Scraper(iIndex).BackColor = Color.White
        lblImageList_Scraper(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblImageList_Scraper(iIndex).Text = tTag.Image.Scraper
        pbImageList_Image(iIndex).Image = If(tImage.ImageOriginal.Image IsNot Nothing, CType(tImage.ImageOriginal.Image.Clone(), Image),
                                                If(tImage.ImageThumb.Image IsNot Nothing, CType(tImage.ImageThumb.Image.Clone(), Image), Nothing))
        pnlImageList_Panel(iIndex).Left = iImageList_NextLeft
        pnlImageList_Panel(iIndex).Top = iImageList_NextTop
        pbImageList_Image(iIndex).Location = iImageList_Location_Image
        lblImageList_DiscType(iIndex).Location = iImageList_Location_DiscType
        lblImageList_Language(iIndex).Location = iImageList_Location_Language
        lblImageList_Resolution(iIndex).Location = iImageList_Location_Resolution
        lblImageList_Scraper(iIndex).Location = iImageList_Location_Scraper
        pnlImageList_Panel(iIndex).Tag = tTag
        pbImageList_Image(iIndex).Tag = tTag
        lblImageList_DiscType(iIndex).Tag = tTag
        lblImageList_Language(iIndex).Tag = tTag
        lblImageList_Resolution(iIndex).Tag = tTag
        lblImageList_Scraper(iIndex).Tag = tTag
        pnlImgSelectMain.Controls.Add(pnlImageList_Panel(iIndex))
        pnlImageList_Panel(iIndex).Controls.Add(pbImageList_Image(iIndex))
        pnlImageList_Panel(iIndex).Controls.Add(lblImageList_DiscType(iIndex))
        pnlImageList_Panel(iIndex).Controls.Add(lblImageList_Language(iIndex))
        pnlImageList_Panel(iIndex).Controls.Add(lblImageList_Resolution(iIndex))
        pnlImageList_Panel(iIndex).Controls.Add(lblImageList_Scraper(iIndex))
        pnlImageList_Panel(iIndex).BringToFront()
        AddHandler pbImageList_Image(iIndex).Click, AddressOf pbImageList_Click
        AddHandler pbImageList_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlImageList_Panel(iIndex).Click, AddressOf pnlImageList_Click
        AddHandler lblImageList_DiscType(iIndex).Click, AddressOf lblImageList_Click
        AddHandler lblImageList_Language(iIndex).Click, AddressOf lblImageList_Click
        AddHandler lblImageList_Resolution(iIndex).Click, AddressOf lblImageList_Click
        AddHandler lblImageList_Scraper(iIndex).Click, AddressOf lblImageList_Click

        If iImageList_NextLeft + iImageList_Size_Panel.Width + iImageList_DistanceLeft + iImageList_Size_Panel.Width > pnlImgSelectMain.Width - 20 Then
            iImageList_NextLeft = iImageList_DistanceLeft
            iImageList_NextTop = iImageList_NextTop + iImageList_Size_Panel.Height + iImageList_DistanceTop
        Else
            iImageList_NextLeft = iImageList_NextLeft + iImageList_Size_Panel.Width + iImageList_DistanceLeft
        End If
    End Sub

    Private Sub AddSubImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, ByVal iSeason As Integer)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason, iIndex)

        ReDim Preserve pnlSubImage_Panel(iIndex)
        ReDim Preserve pbSubImage_Image(iIndex)
        ReDim Preserve lblSubImage_Resolution(iIndex)
        ReDim Preserve lblSubImage_Title(iIndex)
        pnlSubImage_Panel(iIndex) = New Panel()
        pbSubImage_Image(iIndex) = New PictureBox()
        lblSubImage_Resolution(iIndex) = New Label()
        lblSubImage_Title(iIndex) = New Label()
        pbSubImage_Image(iIndex).Name = iIndex.ToString
        pnlSubImage_Panel(iIndex).Name = iIndex.ToString
        lblSubImage_Resolution(iIndex).Name = iIndex.ToString
        lblSubImage_Title(iIndex).Name = iIndex.ToString
        pnlSubImage_Panel(iIndex).Size = iSubImage_Size_Panel
        pbSubImage_Image(iIndex).Size = iSubImage_Size_Image
        lblSubImage_Resolution(iIndex).Size = iSubImage_Size_Resolution
        lblSubImage_Title(iIndex).Size = iSubImage_Size_Title
        pnlSubImage_Panel(iIndex).BackColor = Color.White
        pnlSubImage_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        pbSubImage_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        lblSubImage_Resolution(iIndex).AutoSize = False
        lblSubImage_Resolution(iIndex).BackColor = Color.White
        lblSubImage_Resolution(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblSubImage_Resolution(iIndex).Text = tTag.strResolution
        lblSubImage_Title(iIndex).AutoSize = False
        lblSubImage_Title(iIndex).BackColor = Color.White
        'Me.lblSubImageType(iIndex).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblSubImage_Title(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblSubImage_Title(iIndex).Text = tTag.strSeason
        pbSubImage_Image(iIndex).Image = If(tImage.ImageOriginal.Image IsNot Nothing, CType(tImage.ImageOriginal.Image.Clone(), Image),
                                               If(tImage.ImageThumb.Image IsNot Nothing, CType(tImage.ImageThumb.Image.Clone(), Image), Nothing))
        pnlSubImage_Panel(iIndex).Left = iSubImage_DistanceLeft
        pbSubImage_Image(iIndex).Location = iSubImage_Location_Image
        lblSubImage_Resolution(iIndex).Location = iSubImage_Location_Resolution
        lblSubImage_Title(iIndex).Location = iSubImage_Location_Title
        pnlSubImage_Panel(iIndex).Top = iSubImage_NextTop
        pnlSubImage_Panel(iIndex).Tag = tTag
        pbSubImage_Image(iIndex).Tag = tTag
        lblSubImage_Resolution(iIndex).Tag = tTag
        lblSubImage_Title(iIndex).Tag = tTag
        pnlSubImages.Controls.Add(pnlSubImage_Panel(iIndex))
        pnlSubImage_Panel(iIndex).Controls.Add(pbSubImage_Image(iIndex))
        pnlSubImage_Panel(iIndex).Controls.Add(lblSubImage_Resolution(iIndex))
        pnlSubImage_Panel(iIndex).Controls.Add(lblSubImage_Title(iIndex))
        pnlSubImage_Panel(iIndex).BringToFront()
        AddHandler pbSubImage_Image(iIndex).Click, AddressOf pbSubImage_Click
        AddHandler pbSubImage_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlSubImage_Panel(iIndex).Click, AddressOf pnlSubImage_Click
        AddHandler lblSubImage_Resolution(iIndex).Click, AddressOf lblSubImage_Click
        AddHandler lblSubImage_Title(iIndex).Click, AddressOf lblSubImage_Click

        iSubImage_NextTop = iSubImage_NextTop + iSubImage_Size_Panel.Height + iSubImage_DistanceTop
    End Sub

    Private Sub AddTopImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -1)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason)

        ReDim Preserve pnlTopImage_Panel(iIndex)
        ReDim Preserve pbTopImage_Image(iIndex)
        ReDim Preserve lblTopImage_Resolution(iIndex)
        ReDim Preserve lblTopImage_Title(iIndex)
        pnlTopImage_Panel(iIndex) = New Panel()
        pbTopImage_Image(iIndex) = New PictureBox()
        lblTopImage_Resolution(iIndex) = New Label()
        lblTopImage_Title(iIndex) = New Label()
        pbTopImage_Image(iIndex).Name = iIndex.ToString
        pnlTopImage_Panel(iIndex).Name = iIndex.ToString
        lblTopImage_Resolution(iIndex).Name = iIndex.ToString
        lblTopImage_Title(iIndex).Name = iIndex.ToString
        pnlTopImage_Panel(iIndex).Size = iTopImage_Size_Panel
        pbTopImage_Image(iIndex).Size = iTopImage_Size_Image
        lblTopImage_Resolution(iIndex).Size = iTopImage_Size_Resolution
        lblTopImage_Title(iIndex).Size = iTopImage_Size_Title
        pnlTopImage_Panel(iIndex).BackColor = Color.White
        pnlTopImage_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        pbTopImage_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        lblTopImage_Resolution(iIndex).AutoSize = False
        lblTopImage_Resolution(iIndex).BackColor = Color.White
        lblTopImage_Resolution(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblTopImage_Resolution(iIndex).Text = tTag.strResolution
        lblTopImage_Title(iIndex).AutoSize = False
        lblTopImage_Title(iIndex).BackColor = Color.White
        'Me.lblTopImageType(iIndex).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTopImage_Title(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblTopImage_Title(iIndex).Text = tTag.strTitle
        pbTopImage_Image(iIndex).Image = If(tImage.ImageOriginal.Image IsNot Nothing, CType(tImage.ImageOriginal.Image.Clone(), Image),
                                               If(tImage.ImageThumb.Image IsNot Nothing, CType(tImage.ImageThumb.Image.Clone(), Image), Nothing))
        pnlTopImage_Panel(iIndex).Left = iTopImage_NextLeft
        pbTopImage_Image(iIndex).Location = iTopImage_Location_Image
        lblTopImage_Resolution(iIndex).Location = iTopImage_Location_Resolution
        lblTopImage_Title(iIndex).Location = iTopImage_Location_Title
        pnlTopImage_Panel(iIndex).Top = iTopImage_DistanceTop
        pnlTopImage_Panel(iIndex).Tag = tTag
        pbTopImage_Image(iIndex).Tag = tTag
        lblTopImage_Resolution(iIndex).Tag = tTag
        lblTopImage_Title(iIndex).Tag = tTag
        pnlTopImages.Controls.Add(pnlTopImage_Panel(iIndex))
        pnlTopImage_Panel(iIndex).Controls.Add(pbTopImage_Image(iIndex))
        pnlTopImage_Panel(iIndex).Controls.Add(lblTopImage_Resolution(iIndex))
        pnlTopImage_Panel(iIndex).Controls.Add(lblTopImage_Title(iIndex))
        pnlTopImage_Panel(iIndex).BringToFront()
        AddHandler pbTopImage_Image(iIndex).Click, AddressOf pbTopImage_Click
        AddHandler pbTopImage_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlTopImage_Panel(iIndex).Click, AddressOf pnlTopImage_Click
        AddHandler lblTopImage_Resolution(iIndex).Click, AddressOf lblTopImage_Click
        AddHandler lblTopImage_Title(iIndex).Click, AddressOf lblTopImage_Click

        iTopImage_NextLeft = iTopImage_NextLeft + iTopImage_Size_Panel.Width + iTopImage_DistanceLeft
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
        iImageList_NextLeft = iImageList_DistanceLeft
        iImageList_NextTop = iImageList_DistanceTop

        If pnlImgSelectMain.Controls.Count > 0 Then
            pnlImgSelectMain.SuspendLayout()
            pnlImgSelectMain.AutoScrollPosition = New Point With {.X = 0, .Y = 0}
            For iIndex As Integer = 0 To pnlImageList_Panel.Count - 1
                If pnlImageList_Panel(iIndex) IsNot Nothing Then
                    pnlImageList_Panel(iIndex).Left = iImageList_NextLeft
                    pnlImageList_Panel(iIndex).Top = iImageList_NextTop

                    If iImageList_NextLeft + iImageList_Size_Panel.Width + iImageList_DistanceLeft + iImageList_Size_Panel.Width > pnlImgSelectMain.Width - 20 Then
                        iImageList_NextLeft = iImageList_DistanceLeft
                        iImageList_NextTop = iImageList_NextTop + iImageList_Size_Panel.Height + iImageList_DistanceTop
                    Else
                        iImageList_NextLeft = iImageList_NextLeft + iImageList_Size_Panel.Width + iImageList_DistanceLeft
                    End If
                End If
            Next
            pnlImgSelectMain.ResumeLayout()
            pnlImgSelectMain.Update()
        End If
    End Sub

    Private Sub ReorderSubImages()
        iSubImage_NextTop = iSubImage_DistanceTop

        If pnlSubImages.Controls.Count > 0 Then
            pnlSubImages.SuspendLayout()
            pnlSubImages.AutoScrollPosition = New Point With {.X = 0, .Y = 0}
            For iIndex As Integer = 0 To pnlSubImage_Panel.Count - 1
                If pnlSubImage_Panel(iIndex) IsNot Nothing Then
                    pnlSubImage_Panel(iIndex).Left = iSubImage_DistanceLeft
                    pnlSubImage_Panel(iIndex).Top = iSubImage_NextTop

                    iSubImage_NextTop = iSubImage_NextTop + iSubImage_Size_Panel.Height + iSubImage_DistanceTop
                End If
            Next
            pnlSubImages.ResumeLayout()
            pnlSubImages.Update()
        End If
    End Sub

    Private Sub cbSubImageType_MouseLeave(sender As Object, e As EventArgs) Handles cbSubImageType.MouseLeave
        pnlImgSelectMain.Focus()
    End Sub

    Private Sub cbSubImageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSubImageType.SelectedIndexChanged
        If Not currSubImageSelectedType = CType(cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value Then
            currSubImageSelectedType = CType(cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            ClearImageList()
            CreateSubImages()
            If currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs Then
                currSubImage = New iTag With {.ImageType = currSubImageSelectedType, .iSeason = -1}
                DeselectAllTopImages()
                CreateImageList(currSubImage)
            End If
        End If
        pnlImgSelectMain.Focus()
    End Sub

    Private Sub pbImageList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSelectListImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub pbSubImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSelectSubImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub pbTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSelectTopImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub Image_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, iTag).Image
        tImage.LoadAndCache(tContentType, True, True)

        If tImage.ImageOriginal.Image IsNot Nothing Then
            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.ImageOriginal.Image)
        End If
    End Sub

    Private Sub pnlImageList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        DoSelectListImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub pnlSubImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        DoSelectSubImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub pnlTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub lblImageList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        DoSelectListImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub lblSubImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        DoSelectSubImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub lblTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub DoSelectListImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        For i As Integer = 0 To pnlImageList_Panel.Count - 1
            pnlImageList_Panel(i).BackColor = Color.White
            lblImageList_DiscType(i).BackColor = Color.White
            lblImageList_DiscType(i).ForeColor = Color.Black
            lblImageList_Language(i).BackColor = Color.White
            lblImageList_Language(i).ForeColor = Color.Black
            lblImageList_Resolution(i).BackColor = Color.White
            lblImageList_Resolution(i).ForeColor = Color.Black
            lblImageList_Scraper(i).BackColor = Color.White
            lblImageList_Scraper(i).ForeColor = Color.Black
        Next

        pnlImageList_Panel(iIndex).BackColor = Color.Gray
        lblImageList_DiscType(iIndex).BackColor = Color.Gray
        lblImageList_DiscType(iIndex).ForeColor = Color.White
        lblImageList_Language(iIndex).BackColor = Color.Gray
        lblImageList_Language(iIndex).ForeColor = Color.White
        lblImageList_Resolution(iIndex).BackColor = Color.Gray
        lblImageList_Resolution(iIndex).ForeColor = Color.White
        lblImageList_Scraper(iIndex).BackColor = Color.Gray
        lblImageList_Scraper(iIndex).ForeColor = Color.White

        SetImage(tTag)
    End Sub

    Private Sub DoSelectSubImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        DeselectAllTopImages()
        For i As Integer = 0 To pnlSubImage_Panel.Count - 1
            pnlSubImage_Panel(i).BackColor = Color.White
            lblSubImage_Resolution(i).BackColor = Color.White
            lblSubImage_Resolution(i).ForeColor = Color.Black
            lblSubImage_Title(i).BackColor = Color.White
            lblSubImage_Title(i).ForeColor = Color.Black
        Next

        pnlSubImage_Panel(iIndex).BackColor = Color.Gray
        lblSubImage_Resolution(iIndex).BackColor = Color.Gray
        lblSubImage_Resolution(iIndex).ForeColor = Color.White
        lblSubImage_Title(iIndex).BackColor = Color.Gray
        lblSubImage_Title(iIndex).ForeColor = Color.White

        btnRemoveSubImage.Enabled = True
        btnRestoreSubImage.Enabled = True

        If tTag.ImageType = Enums.ModifierType.MainExtrathumbs Then
            If tTag.iIndex > 0 Then
                btnSubImageUp.Enabled = True
            Else
                btnSubImageUp.Enabled = False
            End If
            If tTag.iIndex < tDBElementResult.ImagesContainer.Extrathumbs.Count - 1 Then
                btnSubImageDown.Enabled = True
            Else
                btnSubImageDown.Enabled = False
            End If
        Else
            btnSubImageDown.Enabled = False
            btnSubImageUp.Enabled = False
        End If

        currSubImage = tTag
        currSubImageSelectedType = tTag.ImageType
        If Not currListImageSelectedImageType = tTag.ImageType OrElse Not currListImageSelectedSeason = tTag.iSeason Then
            CreateImageList(tTag)
        End If
    End Sub

    Private Sub DoSelectTopImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        DeselectAllSubImages()
        For i As Integer = 0 To pnlTopImage_Panel.Count - 1
            pnlTopImage_Panel(i).BackColor = Color.White
            lblTopImage_Resolution(i).BackColor = Color.White
            lblTopImage_Resolution(i).ForeColor = Color.Black
            lblTopImage_Title(i).BackColor = Color.White
            lblTopImage_Title(i).ForeColor = Color.Black
        Next

        pnlTopImage_Panel(iIndex).BackColor = Color.Gray
        lblTopImage_Resolution(iIndex).BackColor = Color.Gray
        lblTopImage_Resolution(iIndex).ForeColor = Color.White
        lblTopImage_Title(iIndex).BackColor = Color.Gray
        lblTopImage_Title(iIndex).ForeColor = Color.White

        btnRemoveTopImage.Enabled = True
        btnRestoreTopImage.Enabled = True

        currTopImage = tTag
        If Not currListImageSelectedImageType = tTag.ImageType Then
            CreateImageList(tTag)
        End If
    End Sub

    Private Sub DeselectAllListImages()
        If pnlImageList_Panel IsNot Nothing Then
            For i As Integer = 0 To pnlImageList_Panel.Count - 1
                pnlImageList_Panel(i).BackColor = Color.White
                lblImageList_DiscType(i).BackColor = Color.White
                lblImageList_DiscType(i).ForeColor = Color.Black
                lblImageList_Language(i).BackColor = Color.White
                lblImageList_Language(i).ForeColor = Color.Black
                lblImageList_Resolution(i).BackColor = Color.White
                lblImageList_Resolution(i).ForeColor = Color.Black
                lblImageList_Scraper(i).BackColor = Color.White
                lblImageList_Scraper(i).ForeColor = Color.Black
            Next
        End If
    End Sub

    Private Sub DeselectAllSubImages()
        btnRemoveSubImage.Enabled = False
        btnSubImageDown.Enabled = False
        btnSubImageUp.Enabled = False
        If Not CType(cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value = Enums.ModifierType.MainExtrafanarts OrElse
            Not CType(cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value = Enums.ModifierType.MainExtrathumbs Then
            btnRestoreSubImage.Enabled = False
        End If
        currSubImage = New iTag
        currSubImageSelectedType = Enums.ModifierType.All
        If pnlSubImage_Panel IsNot Nothing Then
            For i As Integer = 0 To pnlSubImage_Panel.Count - 1
                pnlSubImage_Panel(i).BackColor = Color.White
                lblSubImage_Resolution(i).BackColor = Color.White
                lblSubImage_Resolution(i).ForeColor = Color.Black
                lblSubImage_Title(i).BackColor = Color.White
                lblSubImage_Title(i).ForeColor = Color.Black
            Next
        End If
    End Sub

    Private Sub DeselectAllTopImages()
        btnRemoveTopImage.Enabled = False
        btnRestoreTopImage.Enabled = False
        currTopImage = New iTag
        If pnlTopImage_Panel IsNot Nothing Then
            For i As Integer = 0 To pnlTopImage_Panel.Count - 1
                pnlTopImage_Panel(i).BackColor = Color.White
                lblTopImage_Resolution(i).BackColor = Color.White
                lblTopImage_Resolution(i).ForeColor = Color.Black
                lblTopImage_Title(i).BackColor = Color.White
                lblTopImage_Title(i).ForeColor = Color.Black
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
        Select Case tContentType
            Case Enums.ContentType.Movie
                DoMainBanner = tScrapeModifier.MainBanner AndAlso Master.eSettings.MovieBannerAnyEnabled
                DoMainClearArt = tScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled
                DoMainDiscArt = tScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieDiscArtAnyEnabled
                DoMainExtrafanarts = tScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.MovieExtrafanartsAnyEnabled
                DoMainExtrathumbs = tScrapeModifier.MainExtrathumbs AndAlso Master.eSettings.MovieExtrathumbsAnyEnabled
                DoMainFanart = tScrapeModifier.MainFanart AndAlso Master.eSettings.MovieFanartAnyEnabled
                DoMainLandscape = tScrapeModifier.MainLandscape AndAlso Master.eSettings.MovieLandscapeAnyEnabled
                DoMainPoster = tScrapeModifier.MainPoster AndAlso Master.eSettings.MoviePosterAnyEnabled
            Case Enums.ContentType.MovieSet
                DoMainBanner = tScrapeModifier.MainBanner AndAlso Master.eSettings.MovieSetBannerAnyEnabled
                DoMainClearArt = tScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieSetClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieSetClearLogoAnyEnabled
                DoMainDiscArt = tScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieSetDiscArtAnyEnabled
                DoMainFanart = tScrapeModifier.MainFanart AndAlso Master.eSettings.MovieSetFanartAnyEnabled
                DoMainLandscape = tScrapeModifier.MainLandscape AndAlso Master.eSettings.MovieSetLandscapeAnyEnabled
                DoMainPoster = tScrapeModifier.MainPoster AndAlso Master.eSettings.MovieSetPosterAnyEnabled
            Case Enums.ContentType.TV
                DoAllSeasonsBanner = tScrapeModifier.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled
                DoAllSeasonsFanart = tScrapeModifier.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled
                DoAllSeasonsLandscape = tScrapeModifier.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled
                DoAllSeasonsPoster = tScrapeModifier.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled
                DoEpisodeFanart = tScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                DoEpisodePoster = tScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
                DoMainBanner = tScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                DoMainCharacterArt = tScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                DoMainClearArt = tScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                DoMainExtrafanarts = tScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
                DoMainFanart = tScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                DoMainLandscape = tScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                DoMainPoster = tScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
                DoSeasonBanner = tScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                DoSeasonFanart = tScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                DoSeasonLandscape = tScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                DoSeasonPoster = tScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
            Case Enums.ContentType.TVShow
                DoMainBanner = tScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                DoMainCharacterArt = tScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                DoMainClearArt = tScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                DoMainExtrafanarts = tScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
                DoMainFanart = tScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                DoMainLandscape = tScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                DoMainPoster = tScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
            Case Enums.ContentType.TVEpisode
                DoEpisodeFanart = tScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                DoEpisodePoster = tScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
            Case Enums.ContentType.TVSeason
                DoAllSeasonsBanner = tScrapeModifier.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled
                DoAllSeasonsFanart = tScrapeModifier.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled
                DoAllSeasonsLandscape = tScrapeModifier.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled
                DoAllSeasonsPoster = tScrapeModifier.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled
                DoSeasonBanner = tScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                DoSeasonFanart = tScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                DoSeasonLandscape = tScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                DoSeasonPoster = tScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
        End Select
    End Sub

    Private Sub AddExtraImage(ByVal tTag As iTag)
        Select Case tTag.ImageType
            Case Enums.ModifierType.MainExtrafanarts
                If tDBElementResult.ImagesContainer.Extrafanarts.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    tDBElementResult.ImagesContainer.Extrafanarts.Add(tTag.Image)
                    AddSubImage(tTag.Image, pnlSubImages.Controls.Count, Enums.ModifierType.MainExtrafanarts, -1)
                    ReorderSubImages()
                End If
            Case Enums.ModifierType.MainExtrathumbs
                If tDBElementResult.ImagesContainer.Extrathumbs.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    tTag.Image.Index = tDBElementResult.ImagesContainer.Extrathumbs.Count
                    tDBElementResult.ImagesContainer.Extrathumbs.Add(tTag.Image)
                    AddSubImage(tTag.Image, pnlSubImages.Controls.Count, Enums.ModifierType.MainExtrathumbs, -1)
                    ReorderSubImages()
                End If
        End Select
    End Sub

    Private Sub RefreshSubImage(ByVal tTag As iTag)
        If pnlSubImages.Controls.Count > 0 Then
            For iIndex As Integer = 0 To pnlSubImage_Panel.Count - 1
                If DirectCast(pnlSubImage_Panel(iIndex).Tag, iTag).ImageType = tTag.ImageType AndAlso DirectCast(pnlSubImage_Panel(iIndex).Tag, iTag).iSeason = tTag.iSeason Then
                    If pnlSubImage_Panel(iIndex) IsNot Nothing AndAlso pnlSubImage_Panel(iIndex).Contains(pbSubImage_Image(iIndex)) Then
                        'tTag = CreateTag(tTag.Image, tTag.ImageType)
                        lblSubImage_Resolution(iIndex).Text = tTag.strResolution
                        pnlSubImage_Panel(iIndex).Tag = tTag
                        pbSubImage_Image(iIndex).Tag = tTag
                        lblSubImage_Title(iIndex).Tag = tTag
                        lblSubImage_Resolution(iIndex).Tag = tTag
                        pbSubImage_Image(iIndex).Image = If(tTag.Image.ImageOriginal.Image IsNot Nothing, CType(tTag.Image.ImageOriginal.Image.Clone(), Image),
                                                         If(tTag.Image.ImageThumb.Image IsNot Nothing, CType(tTag.Image.ImageThumb.Image.Clone(), Image), Nothing))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub RefreshTopImage(ByVal tTag As iTag)
        If pnlTopImages.Controls.Count > 0 Then
            For iIndex As Integer = 0 To pnlTopImage_Panel.Count - 1
                If DirectCast(pnlTopImage_Panel(iIndex).Tag, iTag).ImageType = tTag.ImageType Then
                    If pnlTopImage_Panel(iIndex) IsNot Nothing AndAlso pnlTopImage_Panel(iIndex).Contains(pbTopImage_Image(iIndex)) Then
                        'tTag = CreateTag(tTag.Image, tTag.ImageType)
                        lblTopImage_Resolution(iIndex).Text = tTag.strResolution
                        pnlTopImage_Panel(iIndex).Tag = tTag
                        pbTopImage_Image(iIndex).Tag = tTag
                        lblTopImage_Title(iIndex).Tag = tTag
                        lblTopImage_Resolution(iIndex).Tag = tTag
                        pbTopImage_Image(iIndex).Image = If(tTag.Image.ImageOriginal.Image IsNot Nothing, CType(tTag.Image.ImageOriginal.Image.Clone(), Image),
                                                         If(tTag.Image.ImageThumb.Image IsNot Nothing, CType(tTag.Image.ImageThumb.Image.Clone(), Image), Nothing))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub ClearImageList()
        iImageList_NextLeft = iImageList_DistanceLeft
        iImageList_NextTop = iImageList_DistanceTop

        If pnlImgSelectMain.Controls.Count > 0 Then
            For iIndex As Integer = 0 To pnlImageList_Panel.Count - 1
                If pnlImageList_Panel(iIndex) IsNot Nothing Then
                    If lblImageList_DiscType(iIndex) IsNot Nothing AndAlso pnlImageList_Panel(iIndex).Contains(lblImageList_DiscType(iIndex)) Then pnlImageList_Panel(iIndex).Controls.Remove(lblImageList_DiscType(iIndex))
                    If lblImageList_Language(iIndex) IsNot Nothing AndAlso pnlImageList_Panel(iIndex).Contains(lblImageList_Language(iIndex)) Then pnlImageList_Panel(iIndex).Controls.Remove(lblImageList_Language(iIndex))
                    If lblImageList_Resolution(iIndex) IsNot Nothing AndAlso pnlImageList_Panel(iIndex).Contains(lblImageList_Resolution(iIndex)) Then pnlImageList_Panel(iIndex).Controls.Remove(lblImageList_Resolution(iIndex))
                    If lblImageList_Scraper(iIndex) IsNot Nothing AndAlso pnlImageList_Panel(iIndex).Contains(lblImageList_Scraper(iIndex)) Then pnlImageList_Panel(iIndex).Controls.Remove(lblImageList_Scraper(iIndex))
                    If pbImageList_Image(iIndex) IsNot Nothing AndAlso pnlImageList_Panel(iIndex).Contains(pbImageList_Image(iIndex)) Then pnlImageList_Panel(iIndex).Controls.Remove(pbImageList_Image(iIndex))
                    If pnlImgSelectMain.Contains(pnlImageList_Panel(iIndex)) Then pnlImgSelectMain.Controls.Remove(pnlImageList_Panel(iIndex))
                End If
            Next
        End If
    End Sub

    Private Sub ClearSubImages()
        currSubImage = New iTag
        btnRemoveSubImage.Enabled = False
        btnRestoreSubImage.Enabled = False
        btnSubImageDown.Enabled = False
        btnSubImageUp.Enabled = False
        iSubImage_NextTop = iSubImage_DistanceTop

        If pnlSubImages.Controls.Count > 0 Then
            For iIndex As Integer = 0 To pnlSubImage_Panel.Count - 1
                If pnlSubImage_Panel(iIndex) IsNot Nothing Then
                    If lblSubImage_Resolution(iIndex) IsNot Nothing AndAlso pnlSubImage_Panel(iIndex).Contains(lblSubImage_Resolution(iIndex)) Then pnlSubImage_Panel(iIndex).Controls.Remove(lblSubImage_Resolution(iIndex))
                    If pbSubImage_Image(iIndex) IsNot Nothing AndAlso pnlSubImage_Panel(iIndex).Contains(pbSubImage_Image(iIndex)) Then pnlSubImage_Panel(iIndex).Controls.Remove(pbSubImage_Image(iIndex))
                    If pnlSubImages.Contains(pnlSubImage_Panel(iIndex)) Then pnlSubImages.Controls.Remove(pnlSubImage_Panel(iIndex))
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
        Dim iSeason As Integer = currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Banner = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Fanart = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Landscape = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Poster = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.MainExtrafanarts
                tDBElementResult.ImagesContainer.Extrafanarts.Remove(currSubImage.Image)
                CreateSubImages()
            Case Enums.ModifierType.MainExtrathumbs
                tDBElementResult.ImagesContainer.Extrathumbs.Remove(currSubImage.Image)
                CreateSubImages()
        End Select
    End Sub

    Private Sub btnRemoveTopImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTopImage.Click
        Dim eImageType As Enums.ModifierType = currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                tDBElementResult.ImagesContainer.Banner = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainCharacterArt
                tDBElementResult.ImagesContainer.CharacterArt = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearArt
                tDBElementResult.ImagesContainer.ClearArt = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearLogo
                tDBElementResult.ImagesContainer.ClearLogo = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainDiscArt
                tDBElementResult.ImagesContainer.DiscArt = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.ImagesContainer.Fanart = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.ImagesContainer.Landscape = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.ImagesContainer.Poster = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
        End Select
    End Sub

    Private Sub btnRestoreSubImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreSubImage.Click
        Dim iSeason As Integer = currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Banner
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Banner = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Fanart
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Fanart = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Landscape
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Landscape = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Poster
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Poster = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
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
        Dim eImageType As Enums.ModifierType = currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                tDBElementResult.ImagesContainer.Banner = tDefaultImagesContainer.Banner
                currTopImage = CreateImageTag(tDefaultImagesContainer.Banner, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainCharacterArt
                tDBElementResult.ImagesContainer.CharacterArt = tDefaultImagesContainer.CharacterArt
                currTopImage = CreateImageTag(tDefaultImagesContainer.CharacterArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearArt
                tDBElementResult.ImagesContainer.ClearArt = tDefaultImagesContainer.ClearArt
                currTopImage = CreateImageTag(tDefaultImagesContainer.ClearArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearLogo
                tDBElementResult.ImagesContainer.ClearLogo = tDefaultImagesContainer.ClearLogo
                currTopImage = CreateImageTag(tDefaultImagesContainer.ClearLogo, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainDiscArt
                tDBElementResult.ImagesContainer.DiscArt = tDefaultImagesContainer.DiscArt
                currTopImage = CreateImageTag(tDefaultImagesContainer.DiscArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.ImagesContainer.Fanart = tDefaultImagesContainer.Fanart
                currTopImage = CreateImageTag(tDefaultImagesContainer.Fanart, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.ImagesContainer.Landscape = tDefaultImagesContainer.Landscape
                currTopImage = CreateImageTag(tDefaultImagesContainer.Landscape, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.ImagesContainer.Poster = tDefaultImagesContainer.Poster
                currTopImage = CreateImageTag(tDefaultImagesContainer.Poster, eImageType)
                RefreshTopImage(currTopImage)
        End Select
    End Sub

    Private Sub btnSubImageDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubImageDown.Click
        If tDBElementResult.ImagesContainer.Extrathumbs.Count > 0 AndAlso currSubImage.iIndex < (tDBElementResult.ImagesContainer.Extrathumbs.Count - 1) Then
            Dim iIndex As Integer = currSubImage.iIndex
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index + 1
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index - 1
            CreateSubImages()
            DoSelectSubImage(iIndex + 1, CType(pnlSubImage_Panel(iIndex + 1).Tag, iTag))
        End If
    End Sub

    Private Sub btnSubImageUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubImageUp.Click
        If tDBElementResult.ImagesContainer.Extrathumbs.Count > 0 AndAlso currSubImage.iIndex > 0 Then
            Dim iIndex As Integer = currSubImage.iIndex
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index - 1
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index + 1
            CreateSubImages()
            DoSelectSubImage(iIndex - 1, CType(pnlSubImage_Panel(iIndex - 1).Tag, iTag))
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If bwImgDefaults.IsBusy Then bwImgDefaults.CancelAsync()
        If bwImgDownload.IsBusy Then bwImgDownload.CancelAsync()

        lblStatus.Text = Master.eLang.GetString(99, "Canceling All Processes...")
        pbStatus.Style = ProgressBarStyle.Marquee
        lblStatus.Visible = True
        pbStatus.Visible = True

        While bwImgDefaults.IsBusy OrElse bwImgDownload.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        DialogResult = Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        DoneAndClose()
    End Sub
    ''' <summary>
    ''' Downloading fullsize images for preview in Edit Episode / Season / Show dialog
    ''' </summary>
    ''' <remarks>All other images will be downloaded while saving to DB</remarks>
    Private Sub DoneAndClose()
        btnOK.Enabled = False
        DeselectAllListImages()
        DeselectAllSubImages()
        DeselectAllTopImages()
        pnlImgSelectLeft.Enabled = False
        pnlImgSelectMain.Enabled = False
        pnlImgSelectTop.Enabled = False
        pnlLoading.Visible = False

        If bwImgDefaults.IsBusy Then bwImgDefaults.CancelAsync()
        If bwImgDownload.IsBusy Then bwImgDownload.CancelAsync()

        While bwImgDefaults.IsBusy OrElse bwImgDownload.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image(s)...")
        pbStatus.Style = ProgressBarStyle.Marquee
        lblStatus.Visible = True
        pbStatus.Visible = True

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

        DialogResult = Windows.Forms.DialogResult.OK
        Close()
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