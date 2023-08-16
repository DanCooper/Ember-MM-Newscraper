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

Imports EmberAPI
Imports NLog

Public Class dlgImgSelect

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwImgDefaults As New ComponentModel.BackgroundWorker
    Friend WithEvents bwImgDownload As New ComponentModel.BackgroundWorker

    Public Delegate Sub LoadImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
    Public Delegate Sub Delegate_MeActivate()

    'ListImage
    Private iListImage_DistanceLeft As Integer = 3
    Private iListImage_DistanceTop As Integer = 3
    Private iListImage_NextLeft As Integer = 3
    Private iListImage_NextTop As Integer = 3
    Private iListImage_Location_DiscType As Point = New Point(3, 150)
    Private iListImage_Location_Image As Point = New Point(3, 15)
    Private iListImage_Location_Language As Point = New Point(3, 180)
    Private iListImage_Location_Resolution As Point = New Point(3, 165)
    Private iListImage_Location_Scraper As Point = New Point(3, 0)
    Private iListImage_Size_DiscType As Size = New Size(174, 15)
    Private iListImage_Size_Image As Size = New Size(174, 135)
    Private iListImage_Size_Language As Size = New Size(174, 15)
    Private iListImage_Size_Panel As Size = New Size(180, 200)
    Private iListImage_Size_Resolution As Size = New Size(174, 15)
    Private iListImage_Size_Scraper As Size = New Size(174, 15)
    Private iListImage_Size_Select As Size = New Size(16, 16)
    Private iListImage_Location_Select As Point = New Point(iListImage_Size_Panel.Width - iListImage_Size_Select.Width - 5, 5)

    Private lblListImage_DiscType() As Label
    Private lblListImage_Language() As Label
    Private lblListImageList_Resolution() As Label
    Private lblListImage_Scraper() As Label
    Private pbListImage_Image() As PictureBox
    Private pbListImage_Select() As PictureBox
    Private pnlListImage_Panel() As Panel

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

    'SubImage (sizes for banners)
    Private iSubImage_DistanceLeft_Banner As Integer = 3
    Private iSubImage_DistanceTop_Banner As Integer = 3
    Private iSubImage_NextTop_Banner As Integer = 3
    Private iSubImage_Location_Image_Banner As Point = New Point(3, 15)
    Private iSubImage_Location_Resolution_Banner As Point = New Point(3, 155)
    Private iSubImage_Location_Title_Banner As Point = New Point(3, 0)
    Private iSubImage_Size_Image_Banner As Size = New Size(174, 135)
    Private iSubImage_Size_Panel_Banner As Size = New Size(180, 175)
    Private iSubImage_Size_Resolution_Banner As Size = New Size(174, 15)
    Private iSubImage_Size_Title_Banner As Size = New Size(174, 15)

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
    Private DoMainKeyart As Boolean = False
    Private DoMainLandscape As Boolean = False
    Private DoMainPoster As Boolean = False
    Private DoSeasonBanner As Boolean = False
    Private DoSeasonFanart As Boolean = False
    Private DoSeasonLandscape As Boolean = False
    Private DoSeasonPoster As Boolean = False

    Private DoOnlySeason As Integer = -2

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
    Private LoadedMainKeyart As Boolean = False
    Private LoadedMainLandscape As Boolean = False
    Private LoadedMainPoster As Boolean = False
    Private LoadedSeasonBanner As Boolean = False
    Private LoadedSeasonFanart As Boolean = False
    Private LoadedSeasonLandscape As Boolean = False
    Private LoadedSeasonPoster As Boolean = False

    Private currListImage As New iTag
    Private currListImageSelectedSeason As Integer = -2
    Private currListImageSelectedImageType As Enums.ModifierType
    Private currSubImage As New iTag
    Private currSubImageSelectedType As Enums.ModifierType
    Private currTopImage As New iTag

    Private tDBElement As Database.DBElement
    Private tPreferredImagesContainer As New MediaContainers.PreferredImagesContainer
    Private tSearchResultsContainer As New MediaContainers.SearchResultsContainer

    Private tScrapeModifiers As New Structures.ScrapeModifiers
    Private tContentType As Enums.ContentType

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As New MediaContainers.PreferredImagesContainer

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

    Public Overloads Function ShowDialog(ByVal dbElement As Database.DBElement, ByVal searchResultsContainer As MediaContainers.SearchResultsContainer) As DialogResult
        tSearchResultsContainer = searchResultsContainer
        tDBElement = dbElement
        tScrapeModifiers = dbElement.ScrapeModifiers

        Select Case dbElement.ContentType
            Case Enums.ContentType.TVShow
                If tScrapeModifiers.withEpisodes OrElse tScrapeModifiers.withSeasons Then
                    tContentType = Enums.ContentType.TV
                Else
                    tContentType = Enums.ContentType.TVShow
                End If
            Case Enums.ContentType.TVSeason
                tContentType = Enums.ContentType.TVSeason
                DoOnlySeason = dbElement.MainDetails.Season
            Case Else
                tContentType = dbElement.ContentType
                If Master.eSettings.GeneralImageFilter AndAlso Master.eSettings.GeneralImageFilterImagedialog Then
                    'mark all duplicate images in searchcontainer for movie
                    'posters
                    If Master.eSettings.GeneralImageFilterPoster Then
                        Images.DuplicateImages_Find(tSearchResultsContainer.MainPosters, dbElement.ContentType, MatchTolerance:=Master.eSettings.GeneralImageFilterPosterMatchTolerance, RemoveDuplicatesFromList:=False)
                        'this will consider IsDuplicate value of all images by moving all duplicate images to bottom
                        Dim orderedList = tSearchResultsContainer.MainPosters.OrderByDescending(Function(x) x.IsDuplicate = False).ToList()
                        tSearchResultsContainer.MainPosters = orderedList
                    End If
                    'fanarts
                    If Master.eSettings.GeneralImageFilterFanart Then
                        Images.DuplicateImages_Find(tSearchResultsContainer.MainFanarts, dbElement.ContentType, MatchTolerance:=Master.eSettings.GeneralImageFilterFanartMatchTolerance, RemoveDuplicatesFromList:=False)
                        'this will consider IsDuplicate value of all images by moving all duplicate images to bottom
                        Dim orderedList = tSearchResultsContainer.MainFanarts.OrderByDescending(Function(x) x.IsDuplicate = False).ToList()
                        tSearchResultsContainer.MainFanarts = orderedList
                    End If
                End If
        End Select

        SetParameters()

        Return ShowDialog()
    End Function

    Private Sub dlgImgSelect_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        AddHandler MouseWheel, AddressOf MouseWheelEvent
        Functions.PNLDoubleBuffer(pnlImgSelectMain)
        Setup()
    End Sub

    Private Sub dlgImgSelect_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pnlLoading.Location = New Point(CInt((pnlImgSelectMain.Left + (pnlImgSelectMain.Width / 2)) - pnlLoading.Width / 2), CInt((pnlImgSelectMain.Top + (pnlImgSelectMain.Height / 2)) - pnlLoading.Height / 2))
        tmrReorderMainList.Stop()
        tmrReorderMainList.Start()
    End Sub

    Private Sub dlgImgSelect_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
        lblStatus.Text = Master.eLang.GetString(953, "(Down)Loading Default Images...")
        pbStatus.Style = ProgressBarStyle.Marquee
        lblStatus.Visible = True
        pbStatus.Visible = True

        bwImgDefaults.WorkerReportsProgress = False
        bwImgDefaults.WorkerSupportsCancellation = True
        bwImgDefaults.RunWorkerAsync()
    End Sub

    Private Sub AddExtraImage(ByVal tTag As iTag)
        Select Case tTag.ImageType
            Case Enums.ModifierType.MainExtrafanarts
                If Result.ImagesContainer.Extrafanarts.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    Result.ImagesContainer.Extrafanarts.Add(tTag.Image)
                    AddSubImage(tTag.Image, pnlSubImages.Controls.Count, Enums.ModifierType.MainExtrafanarts, -2)
                    ReorderSubImages()
                End If
            Case Enums.ModifierType.MainExtrathumbs
                If Result.ImagesContainer.Extrathumbs.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    tTag.Image.Index = Result.ImagesContainer.Extrathumbs.Count
                    Result.ImagesContainer.Extrathumbs.Add(tTag.Image)
                    AddSubImage(tTag.Image, pnlSubImages.Controls.Count, Enums.ModifierType.MainExtrathumbs, -2)
                    ReorderSubImages()
                End If
        End Select
    End Sub

    Private Sub AddListImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -2)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason, iIndex)

        ReDim Preserve pnlListImage_Panel(iIndex)
        ReDim Preserve pbListImage_Image(iIndex)
        ReDim Preserve pbListImage_Select(iIndex)
        ReDim Preserve lblListImage_DiscType(iIndex)
        ReDim Preserve lblListImage_Language(iIndex)
        ReDim Preserve lblListImageList_Resolution(iIndex)
        ReDim Preserve lblListImage_Scraper(iIndex)

        pnlListImage_Panel(iIndex) = New Panel()
        pbListImage_Image(iIndex) = New PictureBox()
        pbListImage_Select(iIndex) = New PictureBox()
        lblListImage_DiscType(iIndex) = New Label()
        lblListImage_Language(iIndex) = New Label()
        lblListImageList_Resolution(iIndex) = New Label()
        lblListImage_Scraper(iIndex) = New Label()

        pbListImage_Select(iIndex).Image = My.Resources.menuAdd
        pbListImage_Select(iIndex).Location = iListImage_Location_Select
        pbListImage_Select(iIndex).Size = iListImage_Size_Select
        pbListImage_Select(iIndex).Tag = tTag
        pbListImage_Select(iIndex).Visible = False

        lblListImage_DiscType(iIndex).AutoSize = False
        lblListImage_DiscType(iIndex).BackColor = Color.White
        lblListImage_DiscType(iIndex).ContextMenuStrip = cmnuListImage
        lblListImage_DiscType(iIndex).Location = iListImage_Location_DiscType
        lblListImage_DiscType(iIndex).Name = iIndex.ToString
        lblListImage_DiscType(iIndex).Size = iListImage_Size_DiscType
        lblListImage_DiscType(iIndex).Tag = tTag
        lblListImage_DiscType(iIndex).Text = tImage.DiscType
        lblListImage_DiscType(iIndex).TextAlign = ContentAlignment.MiddleCenter
        lblListImage_Language(iIndex).AutoSize = False
        lblListImage_Language(iIndex).BackColor = Color.White
        lblListImage_Language(iIndex).ContextMenuStrip = cmnuListImage
        lblListImage_Language(iIndex).Location = iListImage_Location_Language
        lblListImage_Language(iIndex).Name = iIndex.ToString
        lblListImage_Language(iIndex).Size = iListImage_Size_Language
        lblListImage_Language(iIndex).Tag = tTag
        lblListImage_Language(iIndex).Text = tTag.Image.LongLanguage
        lblListImage_Language(iIndex).TextAlign = ContentAlignment.MiddleCenter
        lblListImageList_Resolution(iIndex).AutoSize = False
        lblListImageList_Resolution(iIndex).BackColor = Color.White
        lblListImageList_Resolution(iIndex).ContextMenuStrip = cmnuListImage
        lblListImageList_Resolution(iIndex).Location = iListImage_Location_Resolution
        lblListImageList_Resolution(iIndex).Name = iIndex.ToString
        lblListImageList_Resolution(iIndex).Size = iListImage_Size_Resolution
        lblListImageList_Resolution(iIndex).Tag = tTag
        lblListImageList_Resolution(iIndex).Text = tTag.strResolution
        lblListImageList_Resolution(iIndex).TextAlign = ContentAlignment.MiddleCenter
        lblListImage_Scraper(iIndex).AutoSize = False
        lblListImage_Scraper(iIndex).BackColor = Color.White
        lblListImage_Scraper(iIndex).ContextMenuStrip = cmnuListImage
        lblListImage_Scraper(iIndex).Location = iListImage_Location_Scraper
        lblListImage_Scraper(iIndex).Name = iIndex.ToString
        lblListImage_Scraper(iIndex).Size = iListImage_Size_Scraper
        lblListImage_Scraper(iIndex).Tag = tTag
        lblListImage_Scraper(iIndex).Text = tTag.Image.Scraper
        lblListImage_Scraper(iIndex).TextAlign = ContentAlignment.MiddleCenter
        pbListImage_Image(iIndex).ContextMenuStrip = cmnuListImage
        If tImage.IsDuplicate Then
            If tImage.ImageThumb.Image IsNot Nothing Then
                pbListImage_Image(iIndex).Image = ImageUtils.AddDuplicateStamp(CType(tImage.ImageThumb.Image.Clone, Image))
            ElseIf tImage.ImageOriginal.Image IsNot Nothing Then
                pbListImage_Image(iIndex).Image = ImageUtils.AddDuplicateStamp(CType(tImage.ImageOriginal.Image.Clone, Image))
            Else
                pbListImage_Image(iIndex).Image = Nothing
            End If
        Else
            pbListImage_Image(iIndex).Image = If(tImage.ImageThumb.Image IsNot Nothing, tImage.ImageThumb.Image,
                If(tImage.ImageOriginal.Image IsNot Nothing, tImage.ImageOriginal.Image, Nothing))
        End If
        pbListImage_Image(iIndex).Location = iListImage_Location_Image
        pbListImage_Image(iIndex).Name = iIndex.ToString
        pbListImage_Image(iIndex).Size = iListImage_Size_Image
        pbListImage_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        pbListImage_Image(iIndex).Tag = tTag
        pnlListImage_Panel(iIndex).BackColor = Color.White
        pnlListImage_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        pnlListImage_Panel(iIndex).ContextMenuStrip = cmnuListImage
        pnlListImage_Panel(iIndex).Left = iListImage_NextLeft
        pnlListImage_Panel(iIndex).Name = iIndex.ToString
        pnlListImage_Panel(iIndex).Size = iListImage_Size_Panel
        pnlListImage_Panel(iIndex).Tag = tTag
        pnlListImage_Panel(iIndex).Top = iListImage_NextTop

        pnlImgSelectMain.Controls.Add(pnlListImage_Panel(iIndex))
        pnlListImage_Panel(iIndex).Controls.Add(pbListImage_Select(iIndex))
        pnlListImage_Panel(iIndex).Controls.Add(pbListImage_Image(iIndex))
        pnlListImage_Panel(iIndex).Controls.Add(lblListImage_DiscType(iIndex))
        pnlListImage_Panel(iIndex).Controls.Add(lblListImage_Language(iIndex))
        pnlListImage_Panel(iIndex).Controls.Add(lblListImageList_Resolution(iIndex))
        pnlListImage_Panel(iIndex).Controls.Add(lblListImage_Scraper(iIndex))
        pnlListImage_Panel(iIndex).BringToFront()

        AddHandler pbListImage_Image(iIndex).DoubleClick, AddressOf pbAnyImage_DoubleClick
        AddHandler pbListImage_Image(iIndex).MouseDown, AddressOf pbListImage_MouseDown
        AddHandler pbListImage_Image(iIndex).MouseEnter, AddressOf pbListImage_MouseEnter
        AddHandler pbListImage_Select(iIndex).Click, AddressOf pbSelect_Click
        AddHandler pnlListImage_Panel(iIndex).DoubleClick, AddressOf pnlAnyImage_DoubleClick
        AddHandler pnlListImage_Panel(iIndex).MouseDown, AddressOf pnlListImage_MouseDown
        AddHandler pnlListImage_Panel(iIndex).MouseEnter, AddressOf pnlListImage_MouseEnter
        AddHandler pnlListImage_Panel(iIndex).MouseLeave, AddressOf pnlListImage_MouseLeave
        AddHandler lblListImage_DiscType(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblListImage_DiscType(iIndex).MouseDown, AddressOf lblListImage_MouseDown
        AddHandler lblListImage_DiscType(iIndex).MouseEnter, AddressOf lblListImage_MouseEnter
        AddHandler lblListImage_Language(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblListImage_Language(iIndex).MouseDown, AddressOf lblListImage_MouseDown
        AddHandler lblListImage_Language(iIndex).MouseEnter, AddressOf lblListImage_MouseEnter
        AddHandler lblListImageList_Resolution(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblListImageList_Resolution(iIndex).MouseDown, AddressOf lblListImage_MouseDown
        AddHandler lblListImageList_Resolution(iIndex).MouseEnter, AddressOf lblListImage_MouseEnter
        AddHandler lblListImage_Scraper(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblListImage_Scraper(iIndex).MouseDown, AddressOf lblListImage_MouseDown
        AddHandler lblListImage_Scraper(iIndex).MouseEnter, AddressOf lblListImage_MouseEnter

        If iListImage_NextLeft + iListImage_Size_Panel.Width + iListImage_DistanceLeft + iListImage_Size_Panel.Width > pnlImgSelectMain.Width - 20 Then
            iListImage_NextLeft = iListImage_DistanceLeft
            iListImage_NextTop = iListImage_NextTop + iListImage_Size_Panel.Height + iListImage_DistanceTop
        Else
            iListImage_NextLeft = iListImage_NextLeft + iListImage_Size_Panel.Width + iListImage_DistanceLeft
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

        lblSubImage_Resolution(iIndex).AutoSize = False
        lblSubImage_Resolution(iIndex).BackColor = Color.White
        lblSubImage_Resolution(iIndex).ContextMenuStrip = cmnuSubImage
        lblSubImage_Resolution(iIndex).Location = iSubImage_Location_Resolution
        lblSubImage_Resolution(iIndex).Name = iIndex.ToString
        lblSubImage_Resolution(iIndex).Size = iSubImage_Size_Resolution
        lblSubImage_Resolution(iIndex).Tag = tTag
        lblSubImage_Resolution(iIndex).Text = tTag.strResolution
        lblSubImage_Resolution(iIndex).TextAlign = ContentAlignment.MiddleCenter
        lblSubImage_Title(iIndex).AutoSize = False
        lblSubImage_Title(iIndex).BackColor = Color.White
        lblSubImage_Title(iIndex).ContextMenuStrip = cmnuSubImage
        lblSubImage_Title(iIndex).Location = iSubImage_Location_Title
        lblSubImage_Title(iIndex).Name = iIndex.ToString
        lblSubImage_Title(iIndex).Size = iSubImage_Size_Title
        lblSubImage_Title(iIndex).Tag = tTag
        lblSubImage_Title(iIndex).Text = tTag.strSeason
        lblSubImage_Title(iIndex).TextAlign = ContentAlignment.MiddleCenter
        pbSubImage_Image(iIndex).ContextMenuStrip = cmnuSubImage
        pbSubImage_Image(iIndex).Image = If(tImage.ImageThumb.Image IsNot Nothing, tImage.ImageThumb.Image,
            If(tImage.ImageOriginal.Image IsNot Nothing, tImage.ImageOriginal.Image, Nothing))
        pbSubImage_Image(iIndex).Location = iSubImage_Location_Image
        pbSubImage_Image(iIndex).Name = iIndex.ToString
        pbSubImage_Image(iIndex).Size = iSubImage_Size_Image
        pbSubImage_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        pbSubImage_Image(iIndex).Tag = tTag
        pnlSubImage_Panel(iIndex).BackColor = Color.White
        pnlSubImage_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        pnlSubImage_Panel(iIndex).ContextMenuStrip = cmnuSubImage
        pnlSubImage_Panel(iIndex).Left = iSubImage_DistanceLeft
        pnlSubImage_Panel(iIndex).Name = iIndex.ToString
        pnlSubImage_Panel(iIndex).Size = iSubImage_Size_Panel
        pnlSubImage_Panel(iIndex).Tag = tTag
        pnlSubImage_Panel(iIndex).Top = iSubImage_NextTop

        pnlSubImages.Controls.Add(pnlSubImage_Panel(iIndex))
        pnlSubImage_Panel(iIndex).Controls.Add(pbSubImage_Image(iIndex))
        pnlSubImage_Panel(iIndex).Controls.Add(lblSubImage_Resolution(iIndex))
        pnlSubImage_Panel(iIndex).Controls.Add(lblSubImage_Title(iIndex))
        pnlSubImage_Panel(iIndex).BringToFront()

        AddHandler pbSubImage_Image(iIndex).DoubleClick, AddressOf pbAnyImage_DoubleClick
        AddHandler pbSubImage_Image(iIndex).MouseDown, AddressOf pbSubImage_MouseDown
        AddHandler pnlSubImage_Panel(iIndex).DoubleClick, AddressOf pnlAnyImage_DoubleClick
        AddHandler pnlSubImage_Panel(iIndex).MouseDown, AddressOf pnlSubImage_MouseDown
        AddHandler lblSubImage_Resolution(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblSubImage_Resolution(iIndex).MouseDown, AddressOf lblSubImage_MouseDown
        AddHandler lblSubImage_Title(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblSubImage_Title(iIndex).MouseDown, AddressOf lblSubImage_MouseDown

        iSubImage_NextTop = iSubImage_NextTop + iSubImage_Size_Panel.Height + iSubImage_DistanceTop
    End Sub

    Private Sub AddTopImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -2)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason)

        ReDim Preserve pnlTopImage_Panel(iIndex)
        ReDim Preserve pbTopImage_Image(iIndex)
        ReDim Preserve lblTopImage_Resolution(iIndex)
        ReDim Preserve lblTopImage_Title(iIndex)

        pnlTopImage_Panel(iIndex) = New Panel()
        pbTopImage_Image(iIndex) = New PictureBox()
        lblTopImage_Resolution(iIndex) = New Label()
        lblTopImage_Title(iIndex) = New Label()

        lblTopImage_Resolution(iIndex).AutoSize = False
        lblTopImage_Resolution(iIndex).BackColor = Color.White
        lblTopImage_Resolution(iIndex).ContextMenuStrip = cmnuTopImage
        lblTopImage_Resolution(iIndex).Location = iTopImage_Location_Resolution
        lblTopImage_Resolution(iIndex).Name = iIndex.ToString
        lblTopImage_Resolution(iIndex).Size = iTopImage_Size_Resolution
        lblTopImage_Resolution(iIndex).Tag = tTag
        lblTopImage_Resolution(iIndex).Text = tTag.strResolution
        lblTopImage_Resolution(iIndex).TextAlign = ContentAlignment.MiddleCenter
        lblTopImage_Title(iIndex).AutoSize = False
        lblTopImage_Title(iIndex).BackColor = Color.White
        lblTopImage_Title(iIndex).ContextMenuStrip = cmnuTopImage
        lblTopImage_Title(iIndex).Location = iTopImage_Location_Title
        lblTopImage_Title(iIndex).Name = iIndex.ToString
        lblTopImage_Title(iIndex).Size = iTopImage_Size_Title
        lblTopImage_Title(iIndex).Tag = tTag
        lblTopImage_Title(iIndex).Text = tTag.strTitle
        lblTopImage_Title(iIndex).TextAlign = ContentAlignment.MiddleCenter
        pbTopImage_Image(iIndex).ContextMenuStrip = cmnuTopImage
        pbTopImage_Image(iIndex).Image = If(tImage.ImageThumb.Image IsNot Nothing, tImage.ImageThumb.Image,
            If(tImage.ImageOriginal.Image IsNot Nothing, tImage.ImageOriginal.Image, Nothing))
        pbTopImage_Image(iIndex).Location = iTopImage_Location_Image
        pbTopImage_Image(iIndex).Name = iIndex.ToString
        pbTopImage_Image(iIndex).Size = iTopImage_Size_Image
        pbTopImage_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        pbTopImage_Image(iIndex).Tag = tTag
        pnlTopImage_Panel(iIndex).BackColor = Color.White
        pnlTopImage_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        pnlTopImage_Panel(iIndex).ContextMenuStrip = cmnuTopImage
        pnlTopImage_Panel(iIndex).Left = iTopImage_NextLeft
        pnlTopImage_Panel(iIndex).Name = iIndex.ToString
        pnlTopImage_Panel(iIndex).Size = iTopImage_Size_Panel
        pnlTopImage_Panel(iIndex).Tag = tTag
        pnlTopImage_Panel(iIndex).Top = iTopImage_DistanceTop

        pnlTopImages.Controls.Add(pnlTopImage_Panel(iIndex))
        pnlTopImage_Panel(iIndex).Controls.Add(pbTopImage_Image(iIndex))
        pnlTopImage_Panel(iIndex).Controls.Add(lblTopImage_Resolution(iIndex))
        pnlTopImage_Panel(iIndex).Controls.Add(lblTopImage_Title(iIndex))
        pnlTopImage_Panel(iIndex).BringToFront()

        AddHandler pbTopImage_Image(iIndex).DoubleClick, AddressOf pbAnyImage_DoubleClick
        AddHandler pbTopImage_Image(iIndex).MouseDown, AddressOf pbTopImage_MouseDown
        AddHandler pnlTopImage_Panel(iIndex).DoubleClick, AddressOf pnlAnyImage_DoubleClick
        AddHandler pnlTopImage_Panel(iIndex).MouseDown, AddressOf pnlTopImage_MouseDown
        AddHandler lblTopImage_Resolution(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblTopImage_Resolution(iIndex).MouseDown, AddressOf lblTopImage_MouseDown
        AddHandler lblTopImage_Title(iIndex).DoubleClick, AddressOf lblAnyImage_DoubleClick
        AddHandler lblTopImage_Title(iIndex).MouseDown, AddressOf lblTopImage_MouseDown

        iTopImage_NextLeft = iTopImage_NextLeft + iTopImage_Size_Panel.Width + iTopImage_DistanceLeft
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
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

        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnExtrafanarts_Click(sender As Object, e As EventArgs) Handles btnExtrafanarts.Click
        SubImageTypeChanged(Enums.ModifierType.MainExtrafanarts)
    End Sub

    Private Sub btnExtrathumbs_Click(sender As Object, e As EventArgs) Handles btnExtrathumbs.Click
        SubImageTypeChanged(Enums.ModifierType.MainExtrathumbs)
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        DoneAndClose()
    End Sub

    Private Sub btnSubImageDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubImageDown.Click
        If Result.ImagesContainer.Extrathumbs.Count > 0 AndAlso currSubImage.iIndex < (Result.ImagesContainer.Extrathumbs.Count - 1) Then
            Dim iIndex As Integer = currSubImage.iIndex
            Result.ImagesContainer.Extrathumbs.Item(iIndex).Index = Result.ImagesContainer.Extrathumbs.Item(iIndex).Index + 1
            Result.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index = Result.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index - 1
            CreateSubImages()
            DoSelectSubImage(iIndex + 1, CType(pnlSubImage_Panel(iIndex + 1).Tag, iTag))
        End If
    End Sub

    Private Sub btnSubImageUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubImageUp.Click
        If Result.ImagesContainer.Extrathumbs.Count > 0 AndAlso currSubImage.iIndex > 0 Then
            Dim iIndex As Integer = currSubImage.iIndex
            Result.ImagesContainer.Extrathumbs.Item(iIndex).Index = Result.ImagesContainer.Extrathumbs.Item(iIndex).Index - 1
            Result.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index = Result.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index + 1
            CreateSubImages()
            DoSelectSubImage(iIndex - 1, CType(pnlSubImage_Panel(iIndex - 1).Tag, iTag))
        End If
    End Sub

    Private Sub btnSeasonBanner_Click(sender As Object, e As EventArgs) Handles btnSeasonBanner.Click
        SubImageTypeChanged(Enums.ModifierType.SeasonBanner)
    End Sub

    Private Sub btnSeasonFanart_Click(sender As Object, e As EventArgs) Handles btnSeasonFanart.Click
        SubImageTypeChanged(Enums.ModifierType.SeasonFanart)
    End Sub

    Private Sub btnSeasonLandscape_Click(sender As Object, e As EventArgs) Handles btnSeasonLandscape.Click
        SubImageTypeChanged(Enums.ModifierType.SeasonLandscape)
    End Sub

    Private Sub btnSeasonPoster_Click(sender As Object, e As EventArgs) Handles btnSeasonPoster.Click
        SubImageTypeChanged(Enums.ModifierType.SeasonPoster)
    End Sub

    Private Sub bwImgDefaults_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDefaults.DoWork
        GetPreferredImages()
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
            CreateListImages(currTopImage)
        ElseIf DirectCast(e.UserState, Enums.ModifierType) = currSubImage.ImageType Then
            CreateListImages(currSubImage)
        End If
    End Sub

    Private Sub bwImgDownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDownload.RunWorkerCompleted
        lblStatus.Visible = False
        pbStatus.Visible = False
    End Sub

    Private Sub ClearListImages()
        iListImage_NextLeft = iListImage_DistanceLeft
        iListImage_NextTop = iListImage_DistanceTop

        If pnlImgSelectMain.Controls.Count > 0 Then
            For iIndex As Integer = 0 To pnlListImage_Panel.Count - 1
                If pnlListImage_Panel(iIndex) IsNot Nothing Then
                    If lblListImage_DiscType(iIndex) IsNot Nothing AndAlso pnlListImage_Panel(iIndex).Contains(lblListImage_DiscType(iIndex)) Then
                        lblListImage_DiscType(iIndex).Dispose()
                        pnlListImage_Panel(iIndex).Controls.Remove(lblListImage_DiscType(iIndex))
                    End If
                    If lblListImage_Language(iIndex) IsNot Nothing AndAlso pnlListImage_Panel(iIndex).Contains(lblListImage_Language(iIndex)) Then
                        lblListImage_Language(iIndex).Dispose()
                        pnlListImage_Panel(iIndex).Controls.Remove(lblListImage_Language(iIndex))
                    End If
                    If lblListImageList_Resolution(iIndex) IsNot Nothing AndAlso pnlListImage_Panel(iIndex).Contains(lblListImageList_Resolution(iIndex)) Then
                        lblListImageList_Resolution(iIndex).Dispose()
                        pnlListImage_Panel(iIndex).Controls.Remove(lblListImageList_Resolution(iIndex))
                    End If
                    If lblListImage_Scraper(iIndex) IsNot Nothing AndAlso pnlListImage_Panel(iIndex).Contains(lblListImage_Scraper(iIndex)) Then
                        lblListImage_Scraper(iIndex).Dispose()
                        pnlListImage_Panel(iIndex).Controls.Remove(lblListImage_Scraper(iIndex))
                    End If
                    If pbListImage_Image(iIndex) IsNot Nothing AndAlso pnlListImage_Panel(iIndex).Contains(pbListImage_Image(iIndex)) Then
                        pbListImage_Image(iIndex).Dispose()
                        pnlListImage_Panel(iIndex).Controls.Remove(pbListImage_Image(iIndex))
                    End If
                    If pnlImgSelectMain.Contains(pnlListImage_Panel(iIndex)) Then
                        pnlListImage_Panel(iIndex).Dispose()
                        pnlImgSelectMain.Controls.Remove(pnlListImage_Panel(iIndex))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub ClearSubImages()
        currSubImage = New iTag
        btnSubImageDown.Enabled = False
        btnSubImageUp.Enabled = False
        iSubImage_NextTop = iSubImage_DistanceTop

        If pnlSubImages.Controls.Count > 0 Then
            For iIndex As Integer = 0 To pnlSubImage_Panel.Count - 1
                If pnlSubImage_Panel(iIndex) IsNot Nothing Then
                    If lblSubImage_Resolution(iIndex) IsNot Nothing AndAlso pnlSubImage_Panel(iIndex).Contains(lblSubImage_Resolution(iIndex)) Then
                        lblSubImage_Resolution(iIndex).Dispose()
                        pnlSubImage_Panel(iIndex).Controls.Remove(lblSubImage_Resolution(iIndex))
                    End If
                    If pbSubImage_Image(iIndex) IsNot Nothing AndAlso pnlSubImage_Panel(iIndex).Contains(pbSubImage_Image(iIndex)) Then
                        pbSubImage_Image(iIndex).Dispose()
                        pnlSubImage_Panel(iIndex).Controls.Remove(pbSubImage_Image(iIndex))
                    End If
                    If pnlSubImages.Contains(pnlSubImage_Panel(iIndex)) Then
                        pnlSubImage_Panel(iIndex).Dispose()
                        pnlSubImages.Controls.Remove(pnlSubImage_Panel(iIndex))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub cmnuListImagePreview_Click(sender As Object, e As EventArgs) Handles cmnuListImagePreview.Click
        Cursor.Current = Cursors.WaitCursor
        currListImage.Image.LoadAndCache(tContentType, True, True)

        If currListImage.Image.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(currListImage.Image.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuListImageSelect_Click(sender As Object, e As EventArgs) Handles cmnuListImageSelect.Click
        SetImage(currListImage)
    End Sub

    Private Sub cmnuListImageSelectAll_Click(sender As Object, e As EventArgs) Handles cmnuListImageSelectAll.Click
        For Each tPanel As Panel In pnlImgSelectMain.Controls
            Dim tImage As iTag = DirectCast(tPanel.Tag, iTag)
            SetImage(tImage)
        Next
    End Sub

    Private Sub cmnuTopImageRestoreOriginal_Click(sender As Object, e As EventArgs) Handles cmnuTopImageRestoreOriginal.Click
        Dim eImageType As Enums.ModifierType = currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Result.ImagesContainer.Banner = tDBElement.ImagesContainer.Banner
                currTopImage = CreateImageTag(Result.ImagesContainer.Banner, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainCharacterArt
                Result.ImagesContainer.CharacterArt = tDBElement.ImagesContainer.CharacterArt
                currTopImage = CreateImageTag(Result.ImagesContainer.CharacterArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearArt
                Result.ImagesContainer.ClearArt = tDBElement.ImagesContainer.ClearArt
                currTopImage = CreateImageTag(Result.ImagesContainer.ClearArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearLogo
                Result.ImagesContainer.ClearLogo = tDBElement.ImagesContainer.ClearLogo
                currTopImage = CreateImageTag(Result.ImagesContainer.ClearLogo, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainDiscArt
                Result.ImagesContainer.DiscArt = tDBElement.ImagesContainer.DiscArt
                currTopImage = CreateImageTag(Result.ImagesContainer.DiscArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                Result.ImagesContainer.Fanart = tDBElement.ImagesContainer.Fanart
                currTopImage = CreateImageTag(Result.ImagesContainer.Fanart, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainKeyart
                Result.ImagesContainer.Keyart = tDBElement.ImagesContainer.Keyart
                currTopImage = CreateImageTag(Result.ImagesContainer.Keyart, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Result.ImagesContainer.Landscape = tDBElement.ImagesContainer.Landscape
                currTopImage = CreateImageTag(Result.ImagesContainer.Landscape, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                Result.ImagesContainer.Poster = tDBElement.ImagesContainer.Poster
                currTopImage = CreateImageTag(Result.ImagesContainer.Poster, eImageType)
                RefreshTopImage(currTopImage)
        End Select
    End Sub

    Private Sub cmnuTopImageRestorePreferred_Click(sender As Object, e As EventArgs) Handles cmnuTopImageRestorePreferred.Click
        Dim eImageType As Enums.ModifierType = currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Result.ImagesContainer.Banner = tPreferredImagesContainer.ImagesContainer.Banner
                currTopImage = CreateImageTag(Result.ImagesContainer.Banner, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainCharacterArt
                Result.ImagesContainer.CharacterArt = tPreferredImagesContainer.ImagesContainer.CharacterArt
                currTopImage = CreateImageTag(Result.ImagesContainer.CharacterArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearArt
                Result.ImagesContainer.ClearArt = tPreferredImagesContainer.ImagesContainer.ClearArt
                currTopImage = CreateImageTag(Result.ImagesContainer.ClearArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearLogo
                Result.ImagesContainer.ClearLogo = tPreferredImagesContainer.ImagesContainer.ClearLogo
                currTopImage = CreateImageTag(Result.ImagesContainer.ClearLogo, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainDiscArt
                Result.ImagesContainer.DiscArt = tPreferredImagesContainer.ImagesContainer.DiscArt
                currTopImage = CreateImageTag(Result.ImagesContainer.DiscArt, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                Result.ImagesContainer.Fanart = tPreferredImagesContainer.ImagesContainer.Fanart
                currTopImage = CreateImageTag(Result.ImagesContainer.Fanart, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainKeyart
                Result.ImagesContainer.Keyart = tPreferredImagesContainer.ImagesContainer.Keyart
                currTopImage = CreateImageTag(Result.ImagesContainer.Keyart, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Result.ImagesContainer.Landscape = tPreferredImagesContainer.ImagesContainer.Landscape
                currTopImage = CreateImageTag(Result.ImagesContainer.Landscape, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                Result.ImagesContainer.Poster = tPreferredImagesContainer.ImagesContainer.Poster
                currTopImage = CreateImageTag(Result.ImagesContainer.Poster, eImageType)
                RefreshTopImage(currTopImage)
        End Select
    End Sub

    Private Sub cmnuTopImageRemove_Click(sender As Object, e As EventArgs) Handles cmnuTopImageRemove.Click
        Dim eImageType As Enums.ModifierType = currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Result.ImagesContainer.Banner = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainCharacterArt
                Result.ImagesContainer.CharacterArt = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearArt
                Result.ImagesContainer.ClearArt = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainClearLogo
                Result.ImagesContainer.ClearLogo = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainDiscArt
                Result.ImagesContainer.DiscArt = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                Result.ImagesContainer.Fanart = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainKeyart
                Result.ImagesContainer.Keyart = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Result.ImagesContainer.Landscape = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                Result.ImagesContainer.Poster = New MediaContainers.Image
                currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(currTopImage)
        End Select
    End Sub

    Private Sub cmnuSubImageRemove_Click(sender As Object, e As EventArgs) Handles cmnuSubImageRemove.Click
        Dim iSeason As Integer = currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Banner = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Fanart = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Landscape = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Poster = New MediaContainers.Image
                currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.MainExtrafanarts
                Result.ImagesContainer.Extrafanarts.Remove(currSubImage.Image)
                CreateSubImages()
            Case Enums.ModifierType.MainExtrathumbs
                Result.ImagesContainer.Extrathumbs.Remove(currSubImage.Image)
                CreateSubImages()
        End Select
    End Sub

    Private Sub cmnuSubImageRemoveAll_Click(sender As Object, e As EventArgs) Handles cmnuSubImageRemoveAll.Click
        Dim iSeason As Integer = currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainExtrafanarts
                Result.ImagesContainer.Extrafanarts.Clear()
                CreateSubImages()
            Case Enums.ModifierType.MainExtrathumbs
                Result.ImagesContainer.Extrathumbs.Clear()
                CreateSubImages()
        End Select
    End Sub

    Private Sub cmnuSubImageRestoreOriginal_Click(sender As Object, e As EventArgs) Handles cmnuSubImageRestoreOriginal.Click
        Dim iSeason As Integer = currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Dim sImg As MediaContainers.Image = tDBElement.Seasons.FirstOrDefault(Function(s) s.MainDetails.Season = iSeason).ImagesContainer.Banner
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Banner = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                Dim sImg As MediaContainers.Image = tDBElement.Seasons.FirstOrDefault(Function(s) s.MainDetails.Season = iSeason).ImagesContainer.Fanart
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Fanart = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Dim sImg As MediaContainers.Image = tDBElement.Seasons.FirstOrDefault(Function(s) s.MainDetails.Season = iSeason).ImagesContainer.Landscape
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Landscape = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                Dim sImg As MediaContainers.Image = tDBElement.Seasons.FirstOrDefault(Function(s) s.MainDetails.Season = iSeason).ImagesContainer.Poster
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Poster = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.MainExtrafanarts
                If MessageBox.Show(Master.eLang.GetString(99999, "Are you sure you want to reset to the default list of Extrafanarts?"), Master.eLang.GetString(99999, "Reload default list"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.OK Then
                    Result.ImagesContainer.Extrafanarts.Clear()
                    Result.ImagesContainer.Extrafanarts.AddRange(tDBElement.ImagesContainer.Extrafanarts)
                    CreateSubImages()
                End If
            Case Enums.ModifierType.MainExtrathumbs
                If MessageBox.Show(Master.eLang.GetString(99999, "Are you sure you want to reset to the default list of Extrathumbs?"), Master.eLang.GetString(99999, "Reload default list"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.OK Then
                    Result.ImagesContainer.Extrathumbs.Clear()
                    Result.ImagesContainer.Extrathumbs.AddRange(tDBElement.ImagesContainer.Extrathumbs)
                    CreateSubImages()
                End If
        End Select
    End Sub

    Private Sub cmnuSubImageRestorePreferred_Click(sender As Object, e As EventArgs) Handles cmnuSubImageRestorePreferred.Click
        Dim iSeason As Integer = currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Dim sImg As MediaContainers.Image = tPreferredImagesContainer.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Banner
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Banner = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                Dim sImg As MediaContainers.Image = tPreferredImagesContainer.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Fanart
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Fanart = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Dim sImg As MediaContainers.Image = tPreferredImagesContainer.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Landscape
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Landscape = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                Dim sImg As MediaContainers.Image = tPreferredImagesContainer.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Poster
                Result.Seasons.FirstOrDefault(Function(s) s.Season = iSeason).Poster = sImg
                currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(currSubImage)
            Case Enums.ModifierType.MainExtrafanarts
                If MessageBox.Show(Master.eLang.GetString(99999, "Are you sure you want to reset to the default list of Extrafanarts?"), Master.eLang.GetString(99999, "Reload default list"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.OK Then
                    Result.ImagesContainer.Extrafanarts.Clear()
                    Result.ImagesContainer.Extrafanarts.AddRange(tPreferredImagesContainer.ImagesContainer.Extrafanarts)
                    CreateSubImages()
                End If
            Case Enums.ModifierType.MainExtrathumbs
                If MessageBox.Show(Master.eLang.GetString(99999, "Are you sure you want to reset to the default list of Extrathumbs?"), Master.eLang.GetString(99999, "Reload default list"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.OK Then
                    Result.ImagesContainer.Extrathumbs.Clear()
                    Result.ImagesContainer.Extrathumbs.AddRange(tPreferredImagesContainer.ImagesContainer.Extrathumbs)
                    CreateSubImages()
                End If
        End Select
    End Sub

    Private Function CreateImageTag(ByRef tImage As MediaContainers.Image, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -2, Optional ByVal iIndex As Integer = -1) As iTag
        Dim nTag As New iTag

        nTag.Image = tImage
        nTag.ImageType = ModifierType

        tImage.LoadAndCache(tContentType, False, True)

        'Description
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If Not tImage.WidthSpecified OrElse Not tImage.HeightSpecified Then
                nTag.strResolution = String.Format("{0}x{1}", tImage.ImageOriginal.Image.Size.Width, tImage.ImageOriginal.Image.Size.Height)
            Else
                nTag.strResolution = String.Format("{0}x{1}", tImage.Width, tImage.Height)
            End If
        ElseIf tImage IsNot Nothing AndAlso tImage.ImageThumb IsNot Nothing AndAlso tImage.ImageThumb.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If CDbl(tImage.Width) = 0 OrElse CDbl(tImage.Height) = 0 Then
                nTag.strResolution = String.Concat("unknown", Environment.NewLine, tImage.LongLanguage)
            Else
                nTag.strResolution = String.Format("{0}x{1}", tImage.Width, tImage.Height)
            End If
        End If

        'Index
        If Not iIndex = -1 Then
            nTag.iIndex = iIndex
        End If

        'Season
        If iSeason = -1 Then
            nTag.iSeason = -1
            nTag.strSeason = Master.eLang.GetString(1256, "* All Seasons")
        ElseIf iSeason = 0 Then
            nTag.iSeason = 0
            nTag.strSeason = Master.eLang.GetString(655, "Season Specials")
        ElseIf Not iSeason = -2 Then
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
            Case Enums.ModifierType.MainKeyart
                nTag.strTitle = Master.eLang.GetString(1237, "Keyart")
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                nTag.strTitle = Master.eLang.GetString(1035, "Landscape")
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                nTag.strTitle = Master.eLang.GetString(148, "Poster")
        End Select

        Return nTag
    End Function

    Private Sub CreateListImages(ByVal tTag As iTag)
        ClearListImages()

        currListImageSelectedSeason = tTag.iSeason
        currListImageSelectedImageType = tTag.ImageType

        pnlLoading.Visible = True

        Select Case tTag.ImageType
            Case Enums.ModifierType.AllSeasonsBanner
                If LoadedMainBanner AndAlso LoadedSeasonBanner Then FillListImages(tTag)
            Case Enums.ModifierType.AllSeasonsFanart
                If LoadedMainFanart AndAlso LoadedSeasonFanart Then FillListImages(tTag)
            Case Enums.ModifierType.AllSeasonsLandscape
                If LoadedMainLandscape AndAlso LoadedSeasonLandscape Then FillListImages(tTag)
            Case Enums.ModifierType.AllSeasonsPoster
                If LoadedMainPoster AndAlso LoadedSeasonPoster Then FillListImages(tTag)
            Case Enums.ModifierType.EpisodeFanart
                If LoadedMainFanart AndAlso LoadedEpisodeFanart Then FillListImages(tTag)
            Case Enums.ModifierType.EpisodePoster
                If LoadedEpisodePoster Then FillListImages(tTag)
            Case Enums.ModifierType.MainBanner
                If LoadedMainBanner Then FillListImages(tTag)
            Case Enums.ModifierType.MainCharacterArt
                If LoadedMainCharacterArt Then FillListImages(tTag)
            Case Enums.ModifierType.MainClearArt
                If LoadedMainClearArt Then FillListImages(tTag)
            Case Enums.ModifierType.MainClearLogo
                If LoadedMainClearLogo Then FillListImages(tTag)
            Case Enums.ModifierType.MainDiscArt
                If LoadedMainDiscArt Then FillListImages(tTag)
            Case Enums.ModifierType.MainExtrafanarts
                If LoadedMainFanart Then FillListImages(tTag)
            Case Enums.ModifierType.MainExtrathumbs
                If LoadedMainFanart Then FillListImages(tTag)
            Case Enums.ModifierType.MainFanart
                If LoadedMainFanart Then FillListImages(tTag)
            Case Enums.ModifierType.MainKeyart
                If LoadedMainKeyart Then FillListImages(tTag)
            Case Enums.ModifierType.MainLandscape
                If LoadedMainLandscape Then FillListImages(tTag)
            Case Enums.ModifierType.MainPoster
                If LoadedMainPoster Then FillListImages(tTag)
            Case Enums.ModifierType.SeasonBanner
                If LoadedSeasonBanner Then FillListImages(tTag)
            Case Enums.ModifierType.SeasonFanart
                If LoadedMainFanart AndAlso LoadedSeasonFanart Then FillListImages(tTag)
            Case Enums.ModifierType.SeasonLandscape
                If LoadedSeasonLandscape Then FillListImages(tTag)
            Case Enums.ModifierType.SeasonPoster
                If LoadedSeasonPoster Then FillListImages(tTag)
        End Select
    End Sub

    Private Sub CreateSubImages()
        Dim iCount As Integer = 0

        ClearSubImages()

        If currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts AndAlso DoMainExtrafanarts Then
            For Each img In Result.ImagesContainer.Extrafanarts
                AddSubImage(img, iCount, Enums.ModifierType.MainExtrafanarts, -2)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs AndAlso DoMainExtrathumbs Then
            Result.ImagesContainer.SortExtrathumbs()
            For Each img In Result.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                img.Index = iCount
                AddSubImage(img, iCount, Enums.ModifierType.MainExtrathumbs, -2)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonBanner AndAlso DoSeasonBanner Then
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) f.Season = -1)
                AddSubImage(sSeason.Banner, iCount, Enums.ModifierType.AllSeasonsBanner, sSeason.Season)
                iCount += 1
            Next
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) Not f.Season = -1).OrderBy(Function(f) f.Season)
                AddSubImage(sSeason.Banner, iCount, Enums.ModifierType.SeasonBanner, sSeason.Season)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonFanart AndAlso DoSeasonFanart Then
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) f.Season = -1)
                AddSubImage(sSeason.Fanart, iCount, Enums.ModifierType.AllSeasonsFanart, sSeason.Season)
                iCount += 1
            Next
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) Not f.Season = -1).OrderBy(Function(f) f.Season)
                AddSubImage(sSeason.Fanart, iCount, Enums.ModifierType.SeasonFanart, sSeason.Season)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonLandscape AndAlso DoSeasonLandscape Then
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) f.Season = -1)
                AddSubImage(sSeason.Landscape, iCount, Enums.ModifierType.AllSeasonsLandscape, sSeason.Season)
                iCount += 1
            Next
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) Not f.Season = -1).OrderBy(Function(f) f.Season)
                AddSubImage(sSeason.Landscape, iCount, Enums.ModifierType.SeasonLandscape, sSeason.Season)
                iCount += 1
            Next
        ElseIf currSubImageSelectedType = Enums.ModifierType.SeasonPoster AndAlso DoSeasonPoster Then
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) f.Season = -1)
                AddSubImage(sSeason.Poster, iCount, Enums.ModifierType.AllSeasonsPoster, sSeason.Season)
                iCount += 1
            Next
            For Each sSeason As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.Where(Function(f) Not f.Season = -1).OrderBy(Function(f) f.Season)
                AddSubImage(sSeason.Poster, iCount, Enums.ModifierType.SeasonPoster, sSeason.Season)
                iCount += 1
            Next
        End If
    End Sub

    Private Sub CreateTopImages()
        Dim noTopImages As Boolean = True
        Dim iCount As Integer = 0

        'While Movie / MovieSet / TV / TVShow scraping
        If DoMainPoster Then
            AddTopImage(Result.ImagesContainer.Poster, iCount, Enums.ModifierType.MainPoster)
            iCount += 1
            noTopImages = False
        End If
        If DoMainKeyart Then
            AddTopImage(Result.ImagesContainer.Keyart, iCount, Enums.ModifierType.MainKeyart)
            iCount += 1
            noTopImages = False
        End If
        If DoMainFanart Then
            AddTopImage(Result.ImagesContainer.Fanart, iCount, Enums.ModifierType.MainFanart)
            iCount += 1
            noTopImages = False
        End If
        If DoMainBanner Then
            AddTopImage(Result.ImagesContainer.Banner, iCount, Enums.ModifierType.MainBanner)
            iCount += 1
            noTopImages = False
        End If
        If DoMainCharacterArt Then
            AddTopImage(Result.ImagesContainer.CharacterArt, iCount, Enums.ModifierType.MainCharacterArt)
            iCount += 1
            noTopImages = False
        End If
        If DoMainClearArt Then
            AddTopImage(Result.ImagesContainer.ClearArt, iCount, Enums.ModifierType.MainClearArt)
            iCount += 1
            noTopImages = False
        End If
        If DoMainClearLogo Then
            AddTopImage(Result.ImagesContainer.ClearLogo, iCount, Enums.ModifierType.MainClearLogo)
            iCount += 1
            noTopImages = False
        End If
        If DoMainDiscArt Then
            AddTopImage(Result.ImagesContainer.DiscArt, iCount, Enums.ModifierType.MainDiscArt)
            iCount += 1
            noTopImages = False
        End If
        If DoMainLandscape Then
            AddTopImage(Result.ImagesContainer.Landscape, iCount, Enums.ModifierType.MainLandscape)
            iCount += 1
            noTopImages = False
        End If

        'While TVEpisode scraping
        If DoEpisodePoster AndAlso tContentType = Enums.ContentType.TVEpisode Then
            AddTopImage(Result.ImagesContainer.Poster, iCount, Enums.ModifierType.EpisodePoster)
            iCount += 1
            noTopImages = False
        End If
        If DoEpisodeFanart AndAlso tContentType = Enums.ContentType.TVEpisode Then
            AddTopImage(Result.ImagesContainer.Fanart, iCount, Enums.ModifierType.EpisodeFanart)
            iCount += 1
            noTopImages = False
        End If

        'While TVSeason scraping
        If DoAllSeasonsPoster AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Poster, iCount, Enums.ModifierType.AllSeasonsPoster, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If
        If DoAllSeasonsFanart AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Fanart, iCount, Enums.ModifierType.AllSeasonsFanart, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If
        If DoAllSeasonsBanner AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Banner, iCount, Enums.ModifierType.AllSeasonsBanner, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If
        If DoAllSeasonsLandscape AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Landscape, iCount, Enums.ModifierType.AllSeasonsLandscape, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonPoster AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Poster, iCount, Enums.ModifierType.SeasonPoster, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonFanart AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Fanart, iCount, Enums.ModifierType.SeasonFanart, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonBanner AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Banner, iCount, Enums.ModifierType.SeasonBanner, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonLandscape AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(Result.ImagesContainer.Landscape, iCount, Enums.ModifierType.SeasonLandscape, DoOnlySeason)
            iCount += 1
            noTopImages = False
        End If

        If DoMainExtrafanarts Then btnExtrafanarts.Enabled = True
        If DoMainExtrathumbs Then btnExtrathumbs.Enabled = True
        If DoSeasonBanner AndAlso Not tContentType = Enums.ContentType.TVSeason Then btnSeasonBanner.Enabled = True
        If DoSeasonFanart AndAlso Not tContentType = Enums.ContentType.TVSeason Then btnSeasonFanart.Enabled = True
        If DoSeasonLandscape AndAlso Not tContentType = Enums.ContentType.TVSeason Then btnSeasonLandscape.Enabled = True
        If DoSeasonPoster AndAlso Not tContentType = Enums.ContentType.TVSeason Then btnSeasonPoster.Enabled = True

        'If we don't have any TopImage we can hide the panel (this should only be True while Extrafanarts or Extrathumbs scraping)
        If Not noTopImages Then
            DoSelectTopImage(0, CType(pnlTopImage_Panel(0).Tag, iTag))
        Else
            pnlTopImages.Visible = False
        End If
    End Sub

    Private Sub DeselectAllListImages()
        If pnlListImage_Panel IsNot Nothing Then
            For i As Integer = 0 To pnlListImage_Panel.Count - 1
                pnlListImage_Panel(i).BackColor = Color.White
                lblListImage_DiscType(i).BackColor = Color.White
                lblListImage_DiscType(i).ForeColor = Color.Black
                lblListImage_Language(i).BackColor = Color.White
                lblListImage_Language(i).ForeColor = Color.Black
                lblListImageList_Resolution(i).BackColor = Color.White
                lblListImageList_Resolution(i).ForeColor = Color.Black
                lblListImage_Scraper(i).BackColor = Color.White
                lblListImage_Scraper(i).ForeColor = Color.Black
            Next
        End If
    End Sub

    Private Sub DeselectAllSubImages()
        btnSubImageDown.Enabled = False
        btnSubImageUp.Enabled = False
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
        pnlTopImages.Enabled = False
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
        Result.ImagesContainer.Banner.LoadAndCache(tContentType, True)

        'CharacterArt
        Result.ImagesContainer.CharacterArt.LoadAndCache(tContentType, True)

        'ClearArt
        Result.ImagesContainer.ClearArt.LoadAndCache(tContentType, True)

        'ClearLogo
        Result.ImagesContainer.ClearLogo.LoadAndCache(tContentType, True)

        'DiscArt
        Result.ImagesContainer.DiscArt.LoadAndCache(tContentType, True)

        'Extrafanarts
        For Each img As MediaContainers.Image In Result.ImagesContainer.Extrafanarts
            img.LoadAndCache(tContentType, True)
        Next

        'Extrathumbs
        For Each img As MediaContainers.Image In Result.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
            img.LoadAndCache(tContentType, True)
        Next

        'Fanart
        Result.ImagesContainer.Fanart.LoadAndCache(tContentType, True)

        'Keyart
        Result.ImagesContainer.Keyart.LoadAndCache(tContentType, True)

        'Landscape
        Result.ImagesContainer.Landscape.LoadAndCache(tContentType, True)

        'Poster
        Result.ImagesContainer.Poster.LoadAndCache(tContentType, True)

        DialogResult = DialogResult.OK
    End Sub

    Private Sub DoSelectListImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        For i As Integer = 0 To pnlListImage_Panel.Count - 1
            pnlListImage_Panel(i).BackColor = Color.White
            lblListImage_DiscType(i).BackColor = Color.White
            lblListImage_DiscType(i).ForeColor = Color.Black
            lblListImage_Language(i).BackColor = Color.White
            lblListImage_Language(i).ForeColor = Color.Black
            lblListImageList_Resolution(i).BackColor = Color.White
            lblListImageList_Resolution(i).ForeColor = Color.Black
            lblListImage_Scraper(i).BackColor = Color.White
            lblListImage_Scraper(i).ForeColor = Color.Black
        Next

        pnlListImage_Panel(iIndex).BackColor = Color.Gray
        lblListImage_DiscType(iIndex).BackColor = Color.Gray
        lblListImage_DiscType(iIndex).ForeColor = Color.White
        lblListImage_Language(iIndex).BackColor = Color.Gray
        lblListImage_Language(iIndex).ForeColor = Color.White
        lblListImageList_Resolution(iIndex).BackColor = Color.Gray
        lblListImageList_Resolution(iIndex).ForeColor = Color.White
        lblListImage_Scraper(iIndex).BackColor = Color.Gray
        lblListImage_Scraper(iIndex).ForeColor = Color.White
        currListImage = tTag
        'SetImage(tTag)
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

        If tTag.ImageType = Enums.ModifierType.MainExtrathumbs Then
            If tTag.iIndex > 0 Then
                btnSubImageUp.Enabled = True
            Else
                btnSubImageUp.Enabled = False
            End If
            If tTag.iIndex < Result.ImagesContainer.Extrathumbs.Count - 1 Then
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
            CreateListImages(tTag)
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

        currTopImage = tTag
        If Not currListImageSelectedImageType = tTag.ImageType Then
            CreateListImages(tTag)
        End If
    End Sub

    Private Function DownloadAllImages() As Boolean
        Dim iProgress As Integer = 1

        bwImgDownload.ReportProgress(If(DoEpisodeFanart, tSearchResultsContainer.EpisodeFanarts.Count, 0) +
                                     If(DoEpisodePoster, tSearchResultsContainer.EpisodePosters.Count, 0) +
                                        tSearchResultsContainer.MainBanners.Count +
                                        tSearchResultsContainer.MainCharacterArts.Count +
                                        tSearchResultsContainer.MainClearArts.Count +
                                        tSearchResultsContainer.MainClearLogos.Count +
                                        tSearchResultsContainer.MainDiscArts.Count +
                                        tSearchResultsContainer.MainFanarts.Count +
                                        tSearchResultsContainer.MainKeyarts.Count +
                                        tSearchResultsContainer.MainLandscapes.Count +
                                        tSearchResultsContainer.MainPosters.Count +
                                        tSearchResultsContainer.SeasonBanners.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count +
                                        tSearchResultsContainer.SeasonFanarts.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count +
                                        tSearchResultsContainer.SeasonLandscapes.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count +
                                        tSearchResultsContainer.SeasonPosters.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason)).Count, "max")

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

        'Main Keyarts
        If DoMainKeyart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainKeyarts
                tImg.LoadAndCache(tContentType, False, True)
                If bwImgDownload.CancellationPending Then
                    Return True
                End If
                bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        LoadedMainKeyart = True
        bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainKeyart)

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

        'Season Banners
        If DoSeasonBanner OrElse DoAllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
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
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
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
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
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
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) If(Not DoOnlySeason = -2, f.Season = DoOnlySeason, Not f.Season = DoOnlySeason))
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

    Private Sub DownloadDefaultImages()

        'Episode Fanart
        If DoEpisodeFanart Then
            For Each tImg As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Episodes.OrderBy(Function(s) s.Season).OrderBy(Function(e) e.Episode)
                tImg.Fanart.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Episode Poster
        If DoEpisodePoster Then
            For Each tImg As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Episodes.OrderBy(Function(s) s.Season).OrderBy(Function(f) f.Episode)
                tImg.Poster.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Main Banner
        If DoMainBanner OrElse DoAllSeasonsBanner OrElse DoSeasonBanner Then
            Result.ImagesContainer.Banner.LoadAndCache(tContentType, False, True)
        End If

        'Main CharacterArt
        If DoMainCharacterArt Then
            Result.ImagesContainer.CharacterArt.LoadAndCache(tContentType, False, True)
        End If

        'Main ClearArt
        If DoMainClearArt Then
            Result.ImagesContainer.ClearArt.LoadAndCache(tContentType, False, True)
        End If

        'Main ClearLogo
        If DoMainClearLogo Then
            Result.ImagesContainer.ClearLogo.LoadAndCache(tContentType, False, True)
        End If

        'Main DiscArt
        If DoMainDiscArt Then
            Result.ImagesContainer.DiscArt.LoadAndCache(tContentType, False, True)
        End If

        'Main Extrafanarts
        If DoMainExtrafanarts Then
            For Each tImg In Result.ImagesContainer.Extrafanarts
                tImg.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Main Extrathumbs
        If DoMainExtrathumbs Then
            For Each tImg In Result.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                tImg.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Main Fanart
        If DoMainFanart OrElse DoAllSeasonsFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart Then
            Result.ImagesContainer.Fanart.LoadAndCache(tContentType, False, True)
        End If

        'Main Keyart
        If DoMainKeyart Then
            Result.ImagesContainer.Keyart.LoadAndCache(tContentType, False, True)
        End If

        'Main Landscape
        If DoMainLandscape OrElse DoAllSeasonsLandscape OrElse DoSeasonLandscape Then
            Result.ImagesContainer.Landscape.LoadAndCache(tContentType, False, True)
        End If

        'Main Poster
        If DoMainPoster OrElse DoAllSeasonsPoster OrElse DoEpisodePoster OrElse DoSeasonPoster Then
            Result.ImagesContainer.Poster.LoadAndCache(tContentType, False, True)
        End If

        'Season Banner
        If DoSeasonBanner Then
            For Each tImg As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.OrderBy(Function(s) s.Season)
                tImg.Banner.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Season Fanart
        If DoSeasonFanart Then
            For Each tImg As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.OrderBy(Function(s) s.Season)
                tImg.Fanart.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Season Landscape
        If DoSeasonLandscape Then
            For Each tImg As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.OrderBy(Function(s) s.Season)
                tImg.Landscape.LoadAndCache(tContentType, False, True)
            Next
        End If

        'Season Poster
        If DoSeasonPoster Then
            For Each tImg As MediaContainers.EpisodeOrSeasonImagesContainer In Result.Seasons.OrderBy(Function(s) s.Season)
                tImg.Poster.LoadAndCache(tContentType, False, True)
            Next
        End If
    End Sub

    Private Sub FillListImages(ByRef tTag As iTag)
        Dim iCount As Integer = 0

        pnlLoading.Visible = False
        Application.DoEvents()

        Select Case tTag.ImageType
            Case Enums.ModifierType.AllSeasonsBanner
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = -1)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner, -1)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsFanart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) f.Season = -1)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart, -1)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsLandscape
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = -1)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape, -1)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsPoster
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = -1)
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                    AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster, -1)
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
            Case Enums.ModifierType.MainKeyart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainKeyarts
                    AddListImage(tImage, iCount, Enums.ModifierType.MainKeyart)
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

    Public Sub GetPreferredImages()
        tPreferredImagesContainer = Images.GetPreferredImagesContainer(tDBElement, tSearchResultsContainer, tScrapeModifiers)
        Result.Episodes.AddRange(tPreferredImagesContainer.Episodes)
        Result.ImagesContainer.Banner = tPreferredImagesContainer.ImagesContainer.Banner
        Result.ImagesContainer.CharacterArt = tPreferredImagesContainer.ImagesContainer.CharacterArt
        Result.ImagesContainer.ClearArt = tPreferredImagesContainer.ImagesContainer.ClearArt
        Result.ImagesContainer.ClearLogo = tPreferredImagesContainer.ImagesContainer.ClearLogo
        Result.ImagesContainer.DiscArt = tPreferredImagesContainer.ImagesContainer.DiscArt
        With Master.eSettings
            Select Case tContentType
                Case Enums.ContentType.Movie
                    If .Movie.ImageSettings.Extrafanarts.Preselect OrElse .Movie.ImageSettings.Extrafanarts.KeepExisting Then Result.ImagesContainer.Extrafanarts.AddRange(tPreferredImagesContainer.ImagesContainer.Extrafanarts)
                    If .Movie.ImageSettings.Extrathumbs.Preselect OrElse .Movie.ImageSettings.Extrathumbs.KeepExisting Then Result.ImagesContainer.Extrathumbs.AddRange(tPreferredImagesContainer.ImagesContainer.Extrathumbs)
                Case Enums.ContentType.TV, Enums.ContentType.TVShow
                    If .TVShow.ImageSettings.Extrafanarts.Preselect OrElse .TVShow.ImageSettings.Extrafanarts.KeepExisting Then Result.ImagesContainer.Extrafanarts.AddRange(tPreferredImagesContainer.ImagesContainer.Extrafanarts)
            End Select
        End With
        Result.ImagesContainer.Fanart = tPreferredImagesContainer.ImagesContainer.Fanart
        Result.ImagesContainer.Keyart = tPreferredImagesContainer.ImagesContainer.Keyart
        Result.ImagesContainer.Landscape = tPreferredImagesContainer.ImagesContainer.Landscape
        Result.ImagesContainer.Poster = tPreferredImagesContainer.ImagesContainer.Poster

        'special handling for seasons (needed to prevent image overwriting in original containers):
        'a new EpisodeSeasonImagesContainer per season has to be added so we change single images independent
        'otherwise the original "EpisodeOrSeasonImagesContainer" object whould be added and changed while a single images has been exchanged
        For Each tSeason In tPreferredImagesContainer.Seasons
            Result.Seasons.Add(New MediaContainers.EpisodeOrSeasonImagesContainer With {
                               .Banner = tSeason.Banner,
                               .Fanart = tSeason.Fanart,
                               .Landscape = tSeason.Landscape,
                               .Poster = tSeason.Poster,
                               .Season = tSeason.Season})
        Next
    End Sub

    Private Sub lblAnyImage_DoubleClick(sender As Object, e As EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, Label).Tag, iTag).Image
        tImage.LoadAndCache(tContentType, True, True)

        If tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub lblListImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectListImage(Convert.ToInt32(DirectCast(sender, Label).Name), DirectCast(DirectCast(sender, Label).Tag, iTag))
        cmnuListImageSelectAll.Enabled = currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs
    End Sub

    Private Sub lblListImage_MouseEnter(sender As Object, e As EventArgs)
        Dim iIndex As Integer = DirectCast(DirectCast(sender, Label).Tag, iTag).iIndex
        pbListImage_Select(iIndex).Visible = True
    End Sub

    Private Sub lblSubImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectSubImage(Convert.ToInt32(DirectCast(sender, Label).Name), DirectCast(DirectCast(sender, Label).Tag, iTag))
        cmnuSubImageRemoveAll.Enabled = currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs
    End Sub

    Private Sub lblTopImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectTopImage(Convert.ToInt32(DirectCast(sender, Label).Name), DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim vScrollPosition As Integer = pnlImgSelectMain.VerticalScroll.Value
        vScrollPosition -= Math.Sign(e.Delta) * 50
        vScrollPosition = Math.Max(0, vScrollPosition)
        vScrollPosition = Math.Min(vScrollPosition, pnlImgSelectMain.VerticalScroll.Maximum)
        pnlImgSelectMain.AutoScrollPosition = New Point(pnlImgSelectMain.AutoScrollPosition.X, vScrollPosition)
        pnlImgSelectMain.Invalidate()
    End Sub

    Private Sub pbAnyImage_DoubleClick(sender As Object, e As EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, iTag).Image
        tImage.LoadAndCache(tContentType, True, True)

        If tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub pbSelect_Click(sender As Object, e As EventArgs)
        Dim tImage As iTag = DirectCast(DirectCast(sender, PictureBox).Tag, iTag)
        SetImage(tImage)
    End Sub

    Private Sub pbListImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectListImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
        cmnuListImageSelectAll.Enabled = currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs
    End Sub

    Private Sub pbListImage_MouseEnter(sender As Object, e As EventArgs)
        Dim iIndex As Integer = DirectCast(DirectCast(sender, PictureBox).Tag, iTag).iIndex
        pbListImage_Select(iIndex).Visible = True
    End Sub

    Private Sub pbSubImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectSubImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
        cmnuSubImageRemoveAll.Enabled = currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs
    End Sub

    Private Sub pbTopImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectTopImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub pnlAnyImage_DoubleClick(sender As Object, e As EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, Panel).Tag, iTag).Image
        tImage.LoadAndCache(tContentType, True, True)

        If tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub pnlListImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectListImage(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, iTag))
        cmnuListImageSelectAll.Enabled = currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs
    End Sub

    Private Sub pnlListImage_MouseEnter(sender As Object, e As EventArgs)
        Dim iIndex As Integer = DirectCast(DirectCast(sender, Panel).Tag, iTag).iIndex
        pbListImage_Select(iIndex).Visible = True
    End Sub

    Private Sub pnlListImage_MouseLeave(sender As Object, e As EventArgs)
        Dim iIndex As Integer = DirectCast(DirectCast(sender, Panel).Tag, iTag).iIndex
        pbListImage_Select(iIndex).Visible = False
    End Sub

    Private Sub pnlSubImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectSubImage(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, iTag))
        cmnuSubImageRemoveAll.Enabled = currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs
    End Sub

    Private Sub pnlTopImage_MouseDown(sender As Object, e As MouseEventArgs)
        DoSelectTopImage(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, iTag))
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
                        pbSubImage_Image(iIndex).Image = If(tTag.Image.ImageThumb.Image IsNot Nothing, tTag.Image.ImageThumb.Image,
                            If(tTag.Image.ImageOriginal.Image IsNot Nothing, tTag.Image.ImageOriginal.Image, Nothing))
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
                        pbTopImage_Image(iIndex).Image = If(tTag.Image.ImageThumb.Image IsNot Nothing, tTag.Image.ImageThumb.Image,
                            If(tTag.Image.ImageOriginal.Image IsNot Nothing, tTag.Image.ImageOriginal.Image, Nothing))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub ReorderListImages()
        iListImage_NextLeft = iListImage_DistanceLeft
        iListImage_NextTop = iListImage_DistanceTop

        If pnlImgSelectMain.Controls.Count > 0 Then
            pnlImgSelectMain.SuspendLayout()
            pnlImgSelectMain.AutoScrollPosition = New Point With {.X = 0, .Y = 0}
            For iIndex As Integer = 0 To pnlListImage_Panel.Count - 1
                If pnlListImage_Panel(iIndex) IsNot Nothing Then
                    pnlListImage_Panel(iIndex).Left = iListImage_NextLeft
                    pnlListImage_Panel(iIndex).Top = iListImage_NextTop

                    If iListImage_NextLeft + iListImage_Size_Panel.Width + iListImage_DistanceLeft + iListImage_Size_Panel.Width > pnlImgSelectMain.Width - 20 Then
                        iListImage_NextLeft = iListImage_DistanceLeft
                        iListImage_NextTop = iListImage_NextTop + iListImage_Size_Panel.Height + iListImage_DistanceTop
                    Else
                        iListImage_NextLeft = iListImage_NextLeft + iListImage_Size_Panel.Width + iListImage_DistanceLeft
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

    Private Sub SetImage(ByVal tTag As iTag)
        Select Case tTag.ImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                If tContentType = Enums.ContentType.TV Then
                    Result.Seasons.FirstOrDefault(Function(s) s.Season = tTag.iSeason).Banner = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    Result.ImagesContainer.Banner = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                If tContentType = Enums.ContentType.TV Then
                    Result.Seasons.FirstOrDefault(Function(s) s.Season = tTag.iSeason).Fanart = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    Result.ImagesContainer.Fanart = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                If tContentType = Enums.ContentType.TV Then
                    Result.Seasons.FirstOrDefault(Function(s) s.Season = tTag.iSeason).Landscape = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    Result.ImagesContainer.Landscape = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                If tContentType = Enums.ContentType.TV Then
                    Result.Seasons.FirstOrDefault(Function(s) s.Season = tTag.iSeason).Poster = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    Result.ImagesContainer.Poster = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.EpisodeFanart
                Result.ImagesContainer.Fanart = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.EpisodePoster
                Result.ImagesContainer.Poster = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainBanner
                Result.ImagesContainer.Banner = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainCharacterArt
                Result.ImagesContainer.CharacterArt = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainClearArt
                Result.ImagesContainer.ClearArt = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainClearLogo
                Result.ImagesContainer.ClearLogo = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainDiscArt
                Result.ImagesContainer.DiscArt = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainExtrafanarts
                AddExtraImage(tTag)
            Case Enums.ModifierType.MainExtrathumbs
                AddExtraImage(tTag)
            Case Enums.ModifierType.MainFanart
                Result.ImagesContainer.Fanart = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainKeyart
                Result.ImagesContainer.Keyart = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainLandscape
                Result.ImagesContainer.Landscape = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainPoster
                Result.ImagesContainer.Poster = tTag.Image
                RefreshTopImage(tTag)
        End Select
    End Sub

    Private Sub SetParameters()
        Dim noSubImages As Boolean = True

        Select Case tContentType
            Case Enums.ContentType.Movie
                DoMainBanner = tScrapeModifiers.Banner AndAlso Master.eSettings.MovieBannerAnyEnabled
                DoMainClearArt = tScrapeModifiers.Clearart AndAlso Master.eSettings.MovieClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifiers.Clearlogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled
                DoMainDiscArt = tScrapeModifiers.Discart AndAlso Master.eSettings.MovieDiscArtAnyEnabled
                DoMainExtrafanarts = tScrapeModifiers.Extrafanarts AndAlso Master.eSettings.MovieExtrafanartsAnyEnabled
                DoMainExtrathumbs = tScrapeModifiers.Extrathumbs AndAlso Master.eSettings.MovieExtrathumbsAnyEnabled
                DoMainFanart = tScrapeModifiers.Fanart AndAlso Master.eSettings.MovieFanartAnyEnabled
                DoMainKeyart = tScrapeModifiers.Keyart AndAlso Master.eSettings.MovieKeyartAnyEnabled
                DoMainLandscape = tScrapeModifiers.Landscape AndAlso Master.eSettings.MovieLandscapeAnyEnabled
                DoMainPoster = tScrapeModifiers.Poster AndAlso Master.eSettings.MoviePosterAnyEnabled
                If DoMainExtrafanarts OrElse DoMainExtrathumbs Then noSubImages = False
            Case Enums.ContentType.MovieSet
                DoMainBanner = tScrapeModifiers.Banner AndAlso Master.eSettings.MovieSetBannerAnyEnabled
                DoMainClearArt = tScrapeModifiers.Clearart AndAlso Master.eSettings.MovieSetClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifiers.Clearlogo AndAlso Master.eSettings.MovieSetClearLogoAnyEnabled
                DoMainDiscArt = tScrapeModifiers.Discart AndAlso Master.eSettings.MovieSetDiscArtAnyEnabled
                DoMainFanart = tScrapeModifiers.Fanart AndAlso Master.eSettings.MovieSetFanartAnyEnabled
                DoMainKeyart = tScrapeModifiers.Keyart AndAlso Master.eSettings.MovieSetKeyartAnyEnabled
                DoMainLandscape = tScrapeModifiers.Landscape AndAlso Master.eSettings.MovieSetLandscapeAnyEnabled
                DoMainPoster = tScrapeModifiers.Poster AndAlso Master.eSettings.MovieSetPosterAnyEnabled
            Case Enums.ContentType.TV
                DoAllSeasonsBanner = tScrapeModifiers.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled
                DoAllSeasonsFanart = tScrapeModifiers.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled
                DoAllSeasonsLandscape = tScrapeModifiers.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled
                DoAllSeasonsPoster = tScrapeModifiers.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled
                'DoEpisodeFanart = tScrapeModifiers.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                'DoEpisodePoster = tScrapeModifiers.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
                DoMainBanner = tScrapeModifiers.Banner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                DoMainCharacterArt = tScrapeModifiers.Characterart AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                DoMainClearArt = tScrapeModifiers.Clearart AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifiers.Clearlogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                DoMainExtrafanarts = tScrapeModifiers.Extrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
                DoMainFanart = tScrapeModifiers.Fanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                DoMainKeyart = tScrapeModifiers.Keyart AndAlso Master.eSettings.TVShowKeyartAnyEnabled
                DoMainLandscape = tScrapeModifiers.Landscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                DoMainPoster = tScrapeModifiers.Poster AndAlso Master.eSettings.TVShowPosterAnyEnabled
                DoSeasonBanner = tScrapeModifiers.Seasons.Banner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                DoSeasonFanart = tScrapeModifiers.Seasons.Fanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                DoSeasonLandscape = tScrapeModifiers.Seasons.Landscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                DoSeasonPoster = tScrapeModifiers.Seasons.Poster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
                If DoMainExtrafanarts OrElse DoSeasonBanner OrElse DoSeasonFanart OrElse DoSeasonLandscape OrElse DoSeasonPoster Then noSubImages = False
            Case Enums.ContentType.TVShow
                DoMainBanner = tScrapeModifiers.Banner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                DoMainCharacterArt = tScrapeModifiers.Characterart AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                DoMainClearArt = tScrapeModifiers.Clearart AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                DoMainClearLogo = tScrapeModifiers.Clearlogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                DoMainExtrafanarts = tScrapeModifiers.Extrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
                DoMainFanart = tScrapeModifiers.Fanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                DoMainKeyart = tScrapeModifiers.Keyart AndAlso Master.eSettings.TVShowKeyartAnyEnabled
                DoMainLandscape = tScrapeModifiers.Landscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                DoMainPoster = tScrapeModifiers.Poster AndAlso Master.eSettings.TVShowPosterAnyEnabled
                If DoMainExtrafanarts Then noSubImages = False
            Case Enums.ContentType.TVEpisode
                DoEpisodeFanart = tScrapeModifiers.Episodes.Fanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                DoEpisodePoster = tScrapeModifiers.Episodes.Poster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
            Case Enums.ContentType.TVSeason
                DoAllSeasonsBanner = tScrapeModifiers.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled
                DoAllSeasonsFanart = tScrapeModifiers.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled
                DoAllSeasonsLandscape = tScrapeModifiers.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled
                DoAllSeasonsPoster = tScrapeModifiers.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled
                DoSeasonBanner = tScrapeModifiers.Seasons.Banner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                DoSeasonFanart = tScrapeModifiers.Seasons.Fanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                DoSeasonLandscape = tScrapeModifiers.Seasons.Landscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                DoSeasonPoster = tScrapeModifiers.Seasons.Poster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
        End Select

        'If we don't have any SubImage we can hide the panel
        If noSubImages Then
            pnlImgSelectLeft.Visible = False
        End If
    End Sub

    Private Sub Setup()
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        btnExtrathumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        btnSeasonBanner.Text = Master.eLang.GetString(1017, "Season Banner")
        btnSeasonFanart.Text = Master.eLang.GetString(686, "Season Fanart")
        btnSeasonLandscape.Text = Master.eLang.GetString(1018, "Season Landscape")
        btnSeasonPoster.Text = Master.eLang.GetString(685, "Season Poster")
    End Sub

    Private Sub SubImageTypeChanged(ByVal tModifierType As Enums.ModifierType)
        If Not currSubImageSelectedType = tModifierType Then
            currSubImageSelectedType = tModifierType
            ClearListImages()
            CreateSubImages()
            If currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs Then
                currSubImage = New iTag With {.ImageType = currSubImageSelectedType, .iSeason = -2}
                DeselectAllTopImages()
                CreateListImages(currSubImage)
            End If
        End If
        pnlImgSelectMain.Focus()
    End Sub

    Private Sub tmrReorderMainList_Tick(sender As Object, e As EventArgs) Handles tmrReorderMainList.Tick
        tmrReorderMainList.Stop()
        ReorderListImages()
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