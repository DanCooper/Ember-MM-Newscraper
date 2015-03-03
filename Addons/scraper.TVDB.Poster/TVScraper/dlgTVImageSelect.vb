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

'TODO: 1.5 - TV Show renaming (including "dump folder")
'TODO: 1.5 - Support VIDEO_TS/BDMV folders for TV Shows

Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Public Class dlgTVImageSelect

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Friend WithEvents bwDownloadFanart As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadData As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadImages As New System.ComponentModel.BackgroundWorker

    Private DefaultImages As New Scraper.TVImages
    Private GenericFanartList As New List(Of Scraper.TVDBFanart)
    Private GenericPosterList As New List(Of Scraper.TVDBPoster)
    Private iCounter As Integer = 0
    Private iLeft As Integer = 5
    Private iTop As Integer = 5
    Private lblImage() As Label
    Private pbImage() As PictureBox
    Private pnlImage() As Panel
    Private SeasonPosterList As New List(Of Scraper.TVDBSeasonPoster)
    Private SeasonBannerList As New List(Of Scraper.TVDBSeasonBanner)
    Private SeasonLandscapeList As New List(Of Scraper.TVDBSeasonLandscape)
    Private SelImgType As Enums.ImageType_TV
    Private SelSeason As Integer = -999
    Private ShowBannerList As New List(Of Scraper.TVDBShowBanner)
    Private ShowCharacterArtList As New List(Of Scraper.TVDBShowCharacterArt)
    Private ShowClearArtList As New List(Of Scraper.TVDBShowClearArt)
    Private ShowClearLogoList As New List(Of Scraper.TVDBShowClearLogo)
    Private ShowLandscapeList As New List(Of Scraper.TVDBShowLandscape)
    Private ShowPosterList As New List(Of Scraper.TVDBPoster)
    Private _id As Integer = -1
    Private _season As Integer = -999
    Private _type As Enums.ImageType_TV = Enums.ImageType_TV.All
    Private _withcurrent As Boolean = True
    Private _ScrapeType As Enums.ScrapeType

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Function SetDefaults() As Boolean
        Dim iSeason As Integer = -1
        Dim iEpisode As Integer = -1
        Dim iProgress As Integer = 11

        Dim tSeaP As Scraper.TVDBSeasonPoster
        Dim tSeaB As Scraper.TVDBSeasonBanner
        Dim tSeaF As Scraper.TVDBFanart
        Dim tSeaL As Scraper.TVDBSeasonLandscape

        Me.bwLoadImages.ReportProgress(Scraper.TVDBImages.SeasonImageList.Count + Scraper.tmpTVDBShow.Episodes.Count + iProgress, "defaults")

        'AllSeason Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image.Image) Then
            Dim tSP As Scraper.TVDBShowBanner = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVASBannerPrefType AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If CBool(clsAdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) Then
                If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))
            End If

            If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVASBannerPrefType)
            'no preferred size, just get any one of them
            If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSP) Then Scraper.TVDBImages.AllSeasonsBanner = tSP
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(1, "progress")

        'AllSeason Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image.Image) Then
            Dim tSF As Scraper.TVDBFanart = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVASFanartPrefSize AndAlso f.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVASFanartPrefSize)
            'no fanart of the preferred size, just get the first available
            If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image))

            If Not IsNothing(tSF) Then Scraper.TVDBImages.AllSeasonsFanart = tSF
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(2, "progress")

        'AllSeason Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape) AndAlso Master.eSettings.TVASLandscapeAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.AllSeasonsLandscape.Image.Image) Then
            Dim tSP As Scraper.TVDBShowLandscape = ShowLandscapeList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSP) Then tSP = ShowLandscapeList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSP) Then
                Scraper.TVDBImages.AllSeasonsLandscape = tSP
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(3, "progress")

        'AllSeason Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image.Image) Then
            Dim tSPg As Scraper.TVDBPoster = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVASPosterPrefSize AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVASPosterPrefSize)
            'no preferred size, just get any one of them
            If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSPg) Then Scraper.TVDBImages.AllSeasonsPoster = tSPg
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(4, "progress")

        'Show Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowBanner.Image.Image) Then
            Dim tSP As Scraper.TVDBShowBanner = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVShowBannerPrefType AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If CBool(clsAdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) Then
                If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))
            End If

            If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVShowBannerPrefType)
            'no preferred size, just get any one of them
            If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSP) Then Scraper.TVDBImages.ShowBanner = tSP
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(5, "progress")

        'Show CharacterArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowCharacterArt) AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowCharacterArt.Image.Image) Then
            Dim tSPg As Scraper.TVDBShowCharacterArt = ShowCharacterArtList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSPg) Then tSPg = ShowCharacterArtList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSPg) Then Scraper.TVDBImages.ShowCharacterArt = tSPg
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(6, "progress")

        'Show ClearArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearArt) AndAlso Master.eSettings.TVShowClearArtAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowClearArt.Image.Image) Then
            Dim tSPg As Scraper.TVDBShowClearArt = ShowClearArtList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSPg) Then tSPg = ShowClearArtList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSPg) Then Scraper.TVDBImages.ShowClearArt = tSPg
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(7, "progress")

        'Show ClearLogo
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearLogo) AndAlso Master.eSettings.TVShowClearLogoAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowClearLogo.Image.Image) Then
            Dim tSPg As Scraper.TVDBShowClearLogo = ShowClearLogoList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSPg) Then tSPg = ShowClearLogoList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSPg) Then Scraper.TVDBImages.ShowClearLogo = tSPg
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(8, "progress")

        'Show Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) AndAlso IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then 'TODO: add *FanartEnabled check
            Dim tSF As Scraper.TVDBFanart = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVShowFanartPrefSize AndAlso f.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVShowFanartPrefSize)
            'no fanart of the preferred size, just get the first available
            If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image))

            If Not IsNothing(tSF) Then Scraper.TVDBImages.ShowFanart = tSF
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(9, "progress")

        'Show Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape) AndAlso Master.eSettings.TVShowLandscapeAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowLandscape.Image.Image) Then
            Dim tSPg As Scraper.TVDBShowLandscape = ShowLandscapeList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If IsNothing(tSPg) Then tSPg = ShowLandscapeList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSPg) Then Scraper.TVDBImages.ShowLandscape = tSPg
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(10, "progress")

        'Show Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image) Then
            Dim tSPg As Scraper.TVDBPoster = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVShowPosterPrefSize AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))

            If CBool(clsAdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) Then
                If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))
            End If

            If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVShowPosterPrefSize)
            'no preferred size, just get any one of them
            If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

            If Not IsNothing(tSPg) Then Scraper.TVDBImages.ShowPoster = tSPg
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(11, "progress")

        'Season Banner/Fanart/Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster OrElse Me._type = Enums.ImageType_TV.SeasonBanner OrElse Me._type = Enums.ImageType_TV.SeasonFanart Then
            For Each cSeason As Scraper.TVDBSeasonImage In Scraper.TVDBImages.SeasonImageList
                Try
                    iSeason = cSeason.Season

                    'Season Banner
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonBanner) AndAlso Master.eSettings.TVSeasonBannerAnyEnabled AndAlso IsNothing(cSeason.Banner.Image.Image) Then
                        tSeaB = SeasonBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonPosterPrefSize AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))
                        If IsNothing(tSeaB) Then tSeaB = SeasonBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonBannerPrefType)
                        'no preferred size, just get any one of them
                        If IsNothing(tSeaB) Then tSeaB = SeasonBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason)
                        If Not IsNothing(tSeaB) Then cSeason.Banner = tSeaB
                    End If

                    'Season Fanart
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonFanart) AndAlso Master.eSettings.TVSeasonFanartAnyEnabled AndAlso IsNothing(cSeason.Fanart.Image.Image) Then
                        tSeaF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVSeasonFanartPrefSize AndAlso f.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))
                        If IsNothing(tSeaF) Then tSeaF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVSeasonFanartPrefSize)
                        'no preferred size, just get any one of them
                        If IsNothing(tSeaF) Then tSeaF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image))
                        If Not IsNothing(tSeaF) Then
                            If Not IsNothing(tSeaF) Then cSeason.Fanart = tSeaF
                        End If
                    End If

                    'Season Landscape
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonLandscape) AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso IsNothing(cSeason.Landscape.Image.Image) Then
                        tSeaL = SeasonLandscapeList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))
                        If IsNothing(tSeaL) Then tSeaL = SeasonLandscapeList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason)
                        If Not IsNothing(tSeaL) Then cSeason.Landscape = tSeaL
                    End If

                    'Season Poster
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster) AndAlso Master.eSettings.TVSeasonPosterAnyEnabled AndAlso IsNothing(cSeason.Poster.Image.Image) Then
                        tSeaP = SeasonPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonPosterPrefSize AndAlso p.Language = clsAdvancedSettings.GetSetting("TVDBLang", "en"))
                        If IsNothing(tSeaP) Then tSeaP = SeasonPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonPosterPrefSize)
                        'no preferred size, just get any one of them
                        If IsNothing(tSeaP) Then tSeaP = SeasonPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason)
                        If Not IsNothing(tSeaP) Then cSeason.Poster = tSeaP
                    End If

                    If Me.bwLoadImages.CancellationPending Then
                        Return True
                    End If
                    Me.bwLoadImages.ReportProgress(iProgress, "progress")
                    iProgress += 1
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Next
        End If

        'Episode Fanart/Poster
        If Me._type = Enums.ImageType_TV.All Then
            For Each Episode As Structures.DBTV In Scraper.tmpTVDBShow.Episodes

                'Fanart
                If Master.eSettings.TVEpisodeFanartAnyEnabled Then
                    If Not String.IsNullOrEmpty(Episode.EpFanartPath) Then
                        Episode.TVEp.Fanart.FromFile(Episode.EpFanartPath)
                    ElseIf Not IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                        Episode.TVEp.Fanart = Scraper.TVDBImages.ShowFanart.Image
                    End If
                End If

                'Poster
                If Master.eSettings.TVEpisodePosterAnyEnabled Then
                    If Not String.IsNullOrEmpty(Episode.TVEp.LocalFile) Then
                        Episode.TVEp.Poster.FromFile(Episode.TVEp.LocalFile)
                    ElseIf Not String.IsNullOrEmpty(Episode.EpPosterPath) Then
                        Episode.TVEp.Poster.FromFile(Episode.EpPosterPath)
                    End If
                End If
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        DefaultImages = Scraper.TVDBImages 'Scraper.TVDBImages.Clone() 'TODO: fix the clone function

        Return False
    End Function

    Public Overloads Function ShowDialog(ByVal ShowID As Integer, ByVal Type As Enums.ImageType_TV, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As System.Windows.Forms.DialogResult
        Me._id = ShowID
        Me._type = Type
        Me._withcurrent = WithCurrent
        Me._ScrapeType = ScrapeType
        Return MyBase.ShowDialog
    End Function

    Public Overloads Function ShowDialog(ByVal ShowID As Integer, ByVal Type As Enums.ImageType_TV, ByVal Season As Integer, ByVal CurrentImage As Images) As Images
        Me._id = ShowID
        Me._type = Type
        Me._season = Season
        Me.pbCurrent.Image = CurrentImage.Image
        Me.pbCurrent.Tag = CurrentImage

        If MyBase.ShowDialog = Windows.Forms.DialogResult.OK Then
            Return CType(Me.pbCurrent.Tag, Images)
        Else
            Return Nothing
        End If
    End Function

    Private Sub AddImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal fTag As ImageTag)
        Try
            ReDim Preserve Me.pnlImage(iIndex)
            ReDim Preserve Me.pbImage(iIndex)
            ReDim Preserve Me.lblImage(iIndex)
            Me.pnlImage(iIndex) = New Panel()
            Me.pbImage(iIndex) = New PictureBox()
            Me.lblImage(iIndex) = New Label()
            Me.pbImage(iIndex).Name = iIndex.ToString
            Me.pnlImage(iIndex).Name = iIndex.ToString
            Me.lblImage(iIndex).Name = iIndex.ToString
            Me.pnlImage(iIndex).Size = New Size(187, 187)
            Me.pbImage(iIndex).Size = New Size(181, 151)
            Me.lblImage(iIndex).Size = New Size(181, 30)
            Me.pnlImage(iIndex).BackColor = Color.White
            Me.pnlImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.lblImage(iIndex).AutoSize = False
            Me.lblImage(iIndex).BackColor = Color.White
            Me.lblImage(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblImage(iIndex).Text = sDescription
            Me.pbImage(iIndex).Image = fTag.ImageObj.Image
            Me.pnlImage(iIndex).Left = iLeft
            Me.pbImage(iIndex).Left = 3
            Me.lblImage(iIndex).Left = 0
            Me.pnlImage(iIndex).Top = iTop
            Me.pbImage(iIndex).Top = 3
            Me.lblImage(iIndex).Top = 151
            Me.pnlImage(iIndex).Tag = fTag
            Me.pbImage(iIndex).Tag = fTag
            Me.lblImage(iIndex).Tag = fTag
            Me.pnlImages.Controls.Add(Me.pnlImage(iIndex))
            Me.pnlImage(iIndex).Controls.Add(Me.pbImage(iIndex))
            Me.pnlImage(iIndex).Controls.Add(Me.lblImage(iIndex))
            Me.pnlImage(iIndex).BringToFront()
            AddHandler pbImage(iIndex).Click, AddressOf pbImage_Click
            AddHandler pbImage(iIndex).DoubleClick, AddressOf pbImage_DoubleClick
            AddHandler pnlImage(iIndex).Click, AddressOf pnlImage_Click
            AddHandler lblImage(iIndex).Click, AddressOf lblImage_Click

            AddHandler pbImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            AddHandler pnlImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            AddHandler lblImage(iIndex).MouseWheel, AddressOf MouseWheelEvent

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iCounter += 1

        If Me.iCounter = 3 Then
            Me.iCounter = 0
            Me.iLeft = 5
            Me.iTop += 192
        Else
            Me.iLeft += 192
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Me.bwLoadData.IsBusy Then Me.bwLoadData.CancelAsync()
        If Me.bwLoadImages.IsBusy Then Me.bwLoadImages.CancelAsync()

        While Me.bwLoadData.IsBusy OrElse Me.bwLoadImages.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        DoneAndClose()
    End Sub

    Private Sub DoneAndClose()
        If Me._type = Enums.ImageType_TV.All Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True

            'Show Banner
            If Master.eSettings.TVShowBannerAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowBanner.LocalFile) Then
                    Scraper.TVDBImages.ShowBanner.Image.FromFile(Scraper.TVDBImages.ShowBanner.LocalFile)
                    Master.currShow.ShowBannerPath = Scraper.TVDBImages.ShowBanner.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.LocalFile) Then
                    Scraper.TVDBImages.ShowBanner.Image.Clear()
                    Scraper.TVDBImages.ShowBanner.Image.FromWeb(Scraper.TVDBImages.ShowBanner.URL)
                    If Not IsNothing(Scraper.TVDBImages.ShowBanner.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowBanner.LocalFile).FullName)
                        Scraper.TVDBImages.ShowBanner.Image.Save(Scraper.TVDBImages.ShowBanner.LocalFile)
                        Master.currShow.ShowBannerPath = Scraper.TVDBImages.ShowBanner.LocalFile
                    End If
                End If
            End If

            'Show CharacterArt
            If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                    Scraper.TVDBImages.ShowCharacterArt.Image.FromFile(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                    Master.currShow.ShowCharacterArtPath = Scraper.TVDBImages.ShowCharacterArt.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                    Scraper.TVDBImages.ShowCharacterArt.Image.Clear()
                    Scraper.TVDBImages.ShowCharacterArt.Image.FromWeb(Scraper.TVDBImages.ShowCharacterArt.URL)
                    If Not IsNothing(Scraper.TVDBImages.ShowCharacterArt.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowCharacterArt.LocalFile).FullName)
                        Scraper.TVDBImages.ShowCharacterArt.Image.Save(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                        Master.currShow.ShowCharacterArtPath = Scraper.TVDBImages.ShowCharacterArt.LocalFile
                    End If
                End If
            End If

            'Show ClearArt
            If Master.eSettings.TVShowClearArtAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                    Scraper.TVDBImages.ShowClearArt.Image.FromFile(Scraper.TVDBImages.ShowClearArt.LocalFile)
                    Master.currShow.ShowClearArtPath = Scraper.TVDBImages.ShowClearArt.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                    Scraper.TVDBImages.ShowClearArt.Image.Clear()
                    Scraper.TVDBImages.ShowClearArt.Image.FromWeb(Scraper.TVDBImages.ShowClearArt.URL)
                    If Not IsNothing(Scraper.TVDBImages.ShowClearArt.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearArt.LocalFile).FullName)
                        Scraper.TVDBImages.ShowClearArt.Image.Save(Scraper.TVDBImages.ShowClearArt.LocalFile)
                        Master.currShow.ShowClearArtPath = Scraper.TVDBImages.ShowClearArt.LocalFile
                    End If
                End If
            End If

            'Show ClearLogo
            If Master.eSettings.TVShowClearLogoAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                    Scraper.TVDBImages.ShowClearLogo.Image.FromFile(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                    Master.currShow.ShowClearLogoPath = Scraper.TVDBImages.ShowClearLogo.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                    Scraper.TVDBImages.ShowClearLogo.Image.Clear()
                    Scraper.TVDBImages.ShowClearLogo.Image.FromWeb(Scraper.TVDBImages.ShowClearLogo.URL)
                    If Not IsNothing(Scraper.TVDBImages.ShowClearLogo.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearLogo.LocalFile).FullName)
                        Scraper.TVDBImages.ShowClearLogo.Image.Save(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                        Master.currShow.ShowClearLogoPath = Scraper.TVDBImages.ShowClearLogo.LocalFile
                    End If
                End If
            End If

            'Show Fanart
            If Master.eSettings.TVShowFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                    Scraper.TVDBImages.ShowFanart.Image.FromFile(Scraper.TVDBImages.ShowFanart.LocalFile)
                    Master.currShow.ShowFanartPath = Scraper.TVDBImages.ShowFanart.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                    Scraper.TVDBImages.ShowFanart.Image.Clear()
                    Scraper.TVDBImages.ShowFanart.Image.FromWeb(Scraper.TVDBImages.ShowFanart.URL)
                    If Not IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowFanart.LocalFile).FullName)
                        Scraper.TVDBImages.ShowFanart.Image.Save(Scraper.TVDBImages.ShowFanart.LocalFile)
                        Master.currShow.ShowFanartPath = Scraper.TVDBImages.ShowFanart.LocalFile
                    End If
                End If
            End If

            'Show Landscape
            If Master.eSettings.TVShowLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                    Scraper.TVDBImages.ShowLandscape.Image.FromFile(Scraper.TVDBImages.ShowLandscape.LocalFile)
                    Master.currShow.ShowLandscapePath = Scraper.TVDBImages.ShowLandscape.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                    Scraper.TVDBImages.ShowLandscape.Image.Clear()
                    Scraper.TVDBImages.ShowLandscape.Image.FromWeb(Scraper.TVDBImages.ShowLandscape.URL)
                    If Not IsNothing(Scraper.TVDBImages.ShowLandscape.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowLandscape.LocalFile).FullName)
                        Scraper.TVDBImages.ShowLandscape.Image.Save(Scraper.TVDBImages.ShowLandscape.LocalFile)
                        Master.currShow.ShowLandscapePath = Scraper.TVDBImages.ShowLandscape.LocalFile
                    End If
                End If
            End If

            'Show Poster
            If Master.eSettings.TVShowPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                    Scraper.TVDBImages.ShowPoster.Image.FromFile(Scraper.TVDBImages.ShowPoster.LocalFile)
                    Master.currShow.ShowPosterPath = Scraper.TVDBImages.ShowPoster.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                    Scraper.TVDBImages.ShowPoster.Image.Clear()
                    Scraper.TVDBImages.ShowPoster.Image.FromWeb(Scraper.TVDBImages.ShowPoster.URL)
                    If Not IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowPoster.LocalFile).FullName)
                        Scraper.TVDBImages.ShowPoster.Image.Save(Scraper.TVDBImages.ShowPoster.LocalFile)
                        Master.currShow.ShowPosterPath = Scraper.TVDBImages.ShowPoster.LocalFile
                    End If
                End If
            End If

            'AS Banner
            If Master.eSettings.TVASBannerAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsBanner.Image.FromFile(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                    Master.currShow.SeasonBannerPath = Scraper.TVDBImages.AllSeasonsBanner.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsBanner.Image.Clear()
                    Scraper.TVDBImages.AllSeasonsBanner.Image.FromWeb(Scraper.TVDBImages.AllSeasonsBanner.URL)
                    If Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsBanner.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsBanner.Image.Save(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                        Master.currShow.SeasonBannerPath = Scraper.TVDBImages.AllSeasonsBanner.LocalFile
                    End If
                End If
            End If

            'AS Fanart
            If Master.eSettings.TVASFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsFanart.Image.FromFile(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                    Master.currShow.SeasonFanartPath = Scraper.TVDBImages.AllSeasonsFanart.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsFanart.Image.Clear()
                    Scraper.TVDBImages.AllSeasonsFanart.Image.FromWeb(Scraper.TVDBImages.AllSeasonsFanart.URL)
                    If Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsFanart.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsFanart.Image.Save(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                        Master.currShow.SeasonFanartPath = Scraper.TVDBImages.AllSeasonsFanart.LocalFile
                    End If
                End If
            End If

            'AS Landscape
            If Master.eSettings.TVASLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsLandscape.Image.FromFile(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                    Master.currShow.SeasonLandscapePath = Scraper.TVDBImages.AllSeasonsLandscape.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsLandscape.Image.Clear()
                    Scraper.TVDBImages.AllSeasonsLandscape.Image.FromWeb(Scraper.TVDBImages.AllSeasonsLandscape.URL)
                    If Not IsNothing(Scraper.TVDBImages.AllSeasonsLandscape.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsLandscape.Image.Save(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                        Master.currShow.SeasonLandscapePath = Scraper.TVDBImages.AllSeasonsLandscape.LocalFile
                    End If
                End If
            End If

            'AS Poster
            If Master.eSettings.TVASPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsPoster.Image.FromFile(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                    Master.currShow.SeasonPosterPath = Scraper.TVDBImages.AllSeasonsPoster.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsPoster.Image.Clear()
                    Scraper.TVDBImages.AllSeasonsPoster.Image.FromWeb(Scraper.TVDBImages.AllSeasonsPoster.URL)
                    If Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image.Image) Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsPoster.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsPoster.Image.Save(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                        Master.currShow.SeasonPosterPath = Scraper.TVDBImages.AllSeasonsPoster.LocalFile
                    End If
                End If
            End If

        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsBanner.Image.FromFile(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsBanner.Image.Clear()
                Scraper.TVDBImages.AllSeasonsBanner.Image.FromWeb(Scraper.TVDBImages.AllSeasonsBanner.URL)
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsBanner.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsBanner.Image.Save(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsFanart.Image.FromFile(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsFanart.Image.Clear()
                Scraper.TVDBImages.AllSeasonsFanart.Image.FromWeb(Scraper.TVDBImages.AllSeasonsFanart.URL)
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsFanart.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsFanart.Image.Save(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsLandscape.Image.FromFile(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsLandscape.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsLandscape.Image.Clear()
                Scraper.TVDBImages.AllSeasonsLandscape.Image.FromWeb(Scraper.TVDBImages.AllSeasonsLandscape.URL)
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsLandscape.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsLandscape.Image.Save(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsLandscape.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsPoster.Image.FromFile(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsPoster.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsPoster.Image.Clear()
                Scraper.TVDBImages.AllSeasonsPoster.Image.FromWeb(Scraper.TVDBImages.AllSeasonsPoster.URL)
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsPoster.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsPoster.Image.Save(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsPoster.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Banner.Image.FromFile(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Banner.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Banner.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Banner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Banner.Image.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Banner.Image.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Banner.URL)
                If Not IsNothing(Scraper.TVDBImages.SeasonImageList(0).Banner.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Banner.Image.Save(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Banner.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Banner.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.FromFile(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Fanart.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Fanart.URL)
                If Not IsNothing(Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.Save(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Fanart.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Landscape.Image.FromFile(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Landscape.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Landscape.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Landscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Landscape.Image.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Landscape.Image.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Landscape.URL)
                If Not IsNothing(Scraper.TVDBImages.SeasonImageList(0).Landscape.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Landscape.Image.Save(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Landscape.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Landscape.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Poster.Image.FromFile(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Poster.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Poster.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Poster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Poster.Image.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Poster.Image.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Poster.URL)
                If Not IsNothing(Scraper.TVDBImages.SeasonImageList(0).Poster.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Poster.Image.Save(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Poster.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Poster.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowBanner.LocalFile) Then
                Scraper.TVDBImages.ShowBanner.Image.FromFile(Scraper.TVDBImages.ShowBanner.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowBanner.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.LocalFile) Then
                Scraper.TVDBImages.ShowBanner.Image.Clear()
                Scraper.TVDBImages.ShowBanner.Image.FromWeb(Scraper.TVDBImages.ShowBanner.URL)
                If Not IsNothing(Scraper.TVDBImages.ShowBanner.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowBanner.LocalFile).FullName)
                    Scraper.TVDBImages.ShowBanner.Image.Save(Scraper.TVDBImages.ShowBanner.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowBanner.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowCharacterArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                Scraper.TVDBImages.ShowCharacterArt.Image.FromFile(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowCharacterArt.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                Scraper.TVDBImages.ShowCharacterArt.Image.Clear()
                Scraper.TVDBImages.ShowCharacterArt.Image.FromWeb(Scraper.TVDBImages.ShowCharacterArt.URL)
                If Not IsNothing(Scraper.TVDBImages.ShowCharacterArt.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowCharacterArt.LocalFile).FullName)
                    Scraper.TVDBImages.ShowCharacterArt.Image.Save(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowCharacterArt.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                Scraper.TVDBImages.ShowClearArt.Image.FromFile(Scraper.TVDBImages.ShowClearArt.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearArt.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                Scraper.TVDBImages.ShowClearArt.Image.Clear()
                Scraper.TVDBImages.ShowClearArt.Image.FromWeb(Scraper.TVDBImages.ShowClearArt.URL)
                If Not IsNothing(Scraper.TVDBImages.ShowClearArt.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearArt.LocalFile).FullName)
                    Scraper.TVDBImages.ShowClearArt.Image.Save(Scraper.TVDBImages.ShowClearArt.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearArt.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearLogo Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                Scraper.TVDBImages.ShowClearLogo.Image.FromFile(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearLogo.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                Scraper.TVDBImages.ShowClearLogo.Image.Clear()
                Scraper.TVDBImages.ShowClearLogo.Image.FromWeb(Scraper.TVDBImages.ShowClearLogo.URL)
                If Not IsNothing(Scraper.TVDBImages.ShowClearLogo.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearLogo.LocalFile).FullName)
                    Scraper.TVDBImages.ShowClearLogo.Image.Save(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearLogo.Image
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                Scraper.TVDBImages.ShowFanart.Image.FromFile(Scraper.TVDBImages.ShowFanart.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                Scraper.TVDBImages.ShowFanart.Image.Clear()
                Scraper.TVDBImages.ShowFanart.Image.FromWeb(Scraper.TVDBImages.ShowFanart.URL)
                If Not IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowFanart.LocalFile).FullName)
                    Scraper.TVDBImages.ShowFanart.Image.Save(Scraper.TVDBImages.ShowFanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.Image
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                Scraper.TVDBImages.ShowLandscape.Image.FromFile(Scraper.TVDBImages.ShowLandscape.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowLandscape.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowLandscape.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                Scraper.TVDBImages.ShowLandscape.Image.Clear()
                Scraper.TVDBImages.ShowLandscape.Image.FromWeb(Scraper.TVDBImages.ShowLandscape.URL)
                If Not IsNothing(Scraper.TVDBImages.ShowLandscape.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowLandscape.LocalFile).FullName)
                    Scraper.TVDBImages.ShowLandscape.Image.Save(Scraper.TVDBImages.ShowLandscape.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowLandscape.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowLandscape.Image
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowPoster OrElse Me._type = Enums.ImageType_TV.EpisodePoster) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                Scraper.TVDBImages.ShowPoster.Image.FromFile(Scraper.TVDBImages.ShowPoster.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowPoster.Image
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                Scraper.TVDBImages.ShowPoster.Image.Clear()
                Scraper.TVDBImages.ShowPoster.Image.FromWeb(Scraper.TVDBImages.ShowPoster.URL)
                If Not IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image) Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowPoster.LocalFile).FullName)
                    Scraper.TVDBImages.ShowPoster.Image.Save(Scraper.TVDBImages.ShowPoster.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowPoster.Image
                End If
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    Private Sub bwLoadData_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadData.DoWork
        Dim cSI As Scraper.TVDBSeasonImage
        Dim iProgress As Integer = 1
        Dim iSeason As Integer = -1

        Me.bwLoadData.ReportProgress(Scraper.tmpTVDBShow.Episodes.Count, "current")

        'initialize the struct
        Scraper.TVDBImages.AllSeasonsBanner = New Scraper.TVDBShowBanner
        Scraper.TVDBImages.AllSeasonsFanart = New Scraper.TVDBFanart
        Scraper.TVDBImages.AllSeasonsLandscape = New Scraper.TVDBShowLandscape
        Scraper.TVDBImages.AllSeasonsPoster = New Scraper.TVDBPoster
        Scraper.TVDBImages.SeasonImageList = New List(Of Scraper.TVDBSeasonImage)
        Scraper.TVDBImages.ShowBanner = New Scraper.TVDBShowBanner
        Scraper.TVDBImages.ShowCharacterArt = New Scraper.TVDBShowCharacterArt
        Scraper.TVDBImages.ShowClearArt = New Scraper.TVDBShowClearArt
        Scraper.TVDBImages.ShowClearLogo = New Scraper.TVDBShowClearLogo
        Scraper.TVDBImages.ShowFanart = New Scraper.TVDBFanart
        Scraper.TVDBImages.ShowLandscape = New Scraper.TVDBShowLandscape
        Scraper.TVDBImages.ShowPoster = New Scraper.TVDBPoster

        If Me.bwLoadData.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Select Case Me._type
            Case Enums.ImageType_TV.AllSeasonsBanner
                Scraper.TVDBImages.AllSeasonsBanner.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsFanart
                Scraper.TVDBImages.AllSeasonsFanart.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsLandscape
                Scraper.TVDBImages.AllSeasonsLandscape.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsPoster
                Scraper.TVDBImages.AllSeasonsPoster.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.SeasonPoster
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Poster.Image = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonBanner
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Banner.Image = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonFanart
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Fanart.Image = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonLandscape
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Landscape.Image = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.ShowBanner
                Scraper.TVDBImages.ShowBanner.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowCharacterArt
                Scraper.TVDBImages.ShowCharacterArt.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowClearArt
                Scraper.TVDBImages.ShowClearArt.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowClearLogo
                Scraper.TVDBImages.ShowClearLogo.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowFanart, Enums.ImageType_TV.EpisodeFanart
                Scraper.TVDBImages.ShowFanart.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowLandscape
                Scraper.TVDBImages.ShowLandscape.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowPoster
                Scraper.TVDBImages.ShowPoster.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.All
                If _withcurrent Then
                    If Master.eSettings.TVShowBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowBannerPath) Then
                        Scraper.TVDBImages.ShowBanner.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowBannerPath)
                        Scraper.TVDBImages.ShowBanner.LocalFile = Scraper.tmpTVDBShow.Show.ShowBannerPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowCharacterArtPath) Then
                        Scraper.TVDBImages.ShowCharacterArt.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowCharacterArtPath)
                        Scraper.TVDBImages.ShowCharacterArt.LocalFile = Scraper.tmpTVDBShow.Show.ShowCharacterArtPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearArtAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowClearArtPath) Then
                        Scraper.TVDBImages.ShowClearArt.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowClearArtPath)
                        Scraper.TVDBImages.ShowClearArt.LocalFile = Scraper.tmpTVDBShow.Show.ShowClearArtPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearLogoAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowClearLogoPath) Then
                        Scraper.TVDBImages.ShowClearLogo.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowClearLogoPath)
                        Scraper.TVDBImages.ShowClearLogo.LocalFile = Scraper.tmpTVDBShow.Show.ShowClearLogoPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowFanartPath) Then
                        Scraper.TVDBImages.ShowFanart.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowFanartPath)
                        Scraper.TVDBImages.ShowFanart.LocalFile = Scraper.tmpTVDBShow.Show.ShowFanartPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowLandscapePath) Then
                        Scraper.TVDBImages.ShowLandscape.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowLandscapePath)
                        Scraper.TVDBImages.ShowLandscape.LocalFile = Scraper.tmpTVDBShow.Show.ShowLandscapePath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowPosterPath) Then
                        Scraper.TVDBImages.ShowPoster.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowPosterPath)
                        Scraper.TVDBImages.ShowPoster.LocalFile = Scraper.tmpTVDBShow.Show.ShowPosterPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonBannerPath) Then
                        Scraper.TVDBImages.AllSeasonsBanner.Image.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonBannerPath)
                        Scraper.TVDBImages.AllSeasonsBanner.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonBannerPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonFanartPath) Then
                        Scraper.TVDBImages.AllSeasonsFanart.Image.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonFanartPath)
                        Scraper.TVDBImages.AllSeasonsFanart.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonFanartPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonLandscapePath) Then
                        Scraper.TVDBImages.AllSeasonsLandscape.Image.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonLandscapePath)
                        Scraper.TVDBImages.AllSeasonsLandscape.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonLandscapePath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath) Then
                        Scraper.TVDBImages.AllSeasonsPoster.Image.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath)
                        Scraper.TVDBImages.AllSeasonsPoster.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    For Each sEpisode As Structures.DBTV In Scraper.tmpTVDBShow.Episodes
                        Try
                            iSeason = sEpisode.TVEp.Season
                            If iSeason > -1 Then
                                If Master.eSettings.TVEpisodePosterAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowPoster.Image) AndAlso Not String.IsNullOrEmpty(sEpisode.ShowPosterPath) Then
                                    Scraper.TVDBImages.ShowPoster.Image.FromFile(sEpisode.ShowPosterPath)
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If

                                If Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) AndAlso Not String.IsNullOrEmpty(sEpisode.ShowFanartPath) Then
                                    Scraper.TVDBImages.ShowFanart.Image.FromFile(sEpisode.ShowFanartPath)
                                    Scraper.TVDBImages.ShowFanart.LocalFile = sEpisode.ShowFanartPath
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If

                                If Scraper.TVDBImages.SeasonImageList.Where(Function(s) s.Season = iSeason).Count = 0 Then
                                    cSI = New Scraper.TVDBSeasonImage
                                    cSI.Season = iSeason
                                    If Master.eSettings.TVSeasonBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonBannerPath) Then
                                        cSI.Banner.Image.FromFile(sEpisode.SeasonBannerPath)
                                        cSI.Banner.LocalFile = sEpisode.SeasonBannerPath
                                    End If
                                    If Master.eSettings.TVSeasonFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonFanartPath) Then
                                        cSI.Fanart.Image.FromFile(sEpisode.SeasonFanartPath)
                                        cSI.Fanart.LocalFile = sEpisode.SeasonFanartPath
                                    End If
                                    If Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonLandscapePath) Then
                                        cSI.Landscape.Image.FromFile(sEpisode.SeasonLandscapePath)
                                        cSI.Landscape.LocalFile = sEpisode.SeasonLandscapePath
                                    End If
                                    If Master.eSettings.TVSeasonPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonPosterPath) Then
                                        cSI.Poster.Image.FromFile(sEpisode.SeasonPosterPath)
                                        cSI.Poster.LocalFile = sEpisode.SeasonPosterPath
                                    End If
                                    Scraper.TVDBImages.SeasonImageList.Add(cSI)
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If
                            End If
                            Me.bwLoadData.ReportProgress(iProgress, "progress")
                            iProgress += 1
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    Next
                Else
                    For Each sEpisode As Structures.DBTV In Scraper.tmpTVDBShow.Episodes
                        Try
                            iSeason = sEpisode.TVEp.Season

                            If Scraper.TVDBImages.SeasonImageList.Where(Function(s) s.Season = iSeason).Count = 0 Then
                                cSI = New Scraper.TVDBSeasonImage
                                cSI.Season = iSeason
                                Scraper.TVDBImages.SeasonImageList.Add(cSI)
                            End If

                            If Me.bwLoadData.CancellationPending Then
                                e.Cancel = True
                                Return
                            End If

                            Me.bwLoadData.ReportProgress(iProgress, "progress")
                            iProgress += 1
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    Next
                End If
        End Select
    End Sub

    Private Sub bwLoadData_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadData.ProgressChanged
        If e.UserState.ToString = "progress" Then
            Me.pbStatus.Value = e.ProgressPercentage
        ElseIf e.UserState.ToString = "current" Then
            Me.lblStatus.Text = Master.eLang.GetString(953, "Loading Current Images...")
            Me.pbStatus.Value = 0
            Me.pbStatus.Maximum = e.ProgressPercentage
        Else
            Me.pbStatus.Value = 0
            Me.pbStatus.Maximum = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwLoadData_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadData.RunWorkerCompleted
        If Not e.Cancelled Then
            Me.GenerateList()

            Me.lblStatus.Text = Master.eLang.GetString(954, "(Down)Loading New Images...")
            Me.bwLoadImages.WorkerReportsProgress = True
            Me.bwLoadImages.WorkerSupportsCancellation = True
            Me.bwLoadImages.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwLoadImages_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadImages.DoWork
        e.Cancel = Me.DownloadAllImages()
    End Sub

    Private Sub bwLoadImages_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadImages.ProgressChanged
        If e.UserState.ToString = "progress" Then
            Me.pbStatus.Value = e.ProgressPercentage
        ElseIf e.UserState.ToString = "defaults" Then
            Me.lblStatus.Text = Master.eLang.GetString(955, "Setting Defaults...")
            Me.pbStatus.Value = 0
            Me.pbStatus.Maximum = e.ProgressPercentage
        Else
            Me.pbStatus.Value = 0
            Me.pbStatus.Maximum = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwLoadImages_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadImages.RunWorkerCompleted
        Me.pnlStatus.Visible = False
        If _ScrapeType = Enums.ScrapeType.FullAuto Then
            DoneAndClose()
        Else
            If Not e.Cancelled Then
                Me.tvList.Enabled = True
                Me.tvList.Visible = True
                If Me.tvList.Nodes.Count > 0 Then
                    Me.tvList.SelectedNode = Me.tvList.Nodes(0)
                End If
                Me.tvList.Focus()

                Me.btnOK.Enabled = True
            End If

            Me.pbCurrent.Visible = True
            Me.lblCurrentImage.Visible = True
        End If
    End Sub

    Private Sub CheckCurrentImage()
        Me.pbDelete.Visible = Not IsNothing(Me.pbCurrent.Image) AndAlso Me.pbCurrent.Visible
        Me.pbUndo.Visible = Me.pbCurrent.Visible
    End Sub

    Private Sub ClearImages()
        Try
            Me.iCounter = 0
            Me.iLeft = 5
            Me.iTop = 5
            Me.pbCurrent.Image = Nothing
            Me.pbCurrent.Tag = Nothing

            If Me.pnlImages.Controls.Count > 0 Then
                For i As Integer = UBound(Me.pnlImage) To 0 Step -1
                    If Not IsNothing(Me.pnlImage(i)) Then
                        If Not IsNothing(Me.lblImage(i)) AndAlso Me.pnlImage(i).Contains(Me.lblImage(i)) Then Me.pnlImage(i).Controls.Remove(Me.lblImage(i))
                        If Not IsNothing(Me.pbImage(i)) AndAlso Me.pnlImage(i).Contains(Me.pbImage(i)) Then Me.pnlImage(i).Controls.Remove(Me.pbImage(i))
                        If Me.pnlImages.Contains(Me.pnlImage(i)) Then Me.pnlImages.Controls.Remove(Me.pnlImage(i))
                    End If
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgTVImageSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler pnlImages.MouseWheel, AddressOf MouseWheelEvent
        AddHandler MyBase.MouseWheel, AddressOf MouseWheelEvent
        AddHandler tvList.MouseWheel, AddressOf MouseWheelEvent

        Functions.PNLDoubleBuffer(Me.pnlImages)

        Me.SetUp()
    End Sub

    Private Sub dlgTVImageSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.bwLoadData.WorkerReportsProgress = True
        Me.bwLoadData.WorkerSupportsCancellation = True
        Me.bwLoadData.RunWorkerAsync()
    End Sub

    Private Sub DoSelect(ByVal iIndex As Integer, ByVal SelTag As ImageTag)
        Try
            For i As Integer = 0 To UBound(Me.pnlImage)
                Me.pnlImage(i).BackColor = Color.White
                Me.lblImage(i).BackColor = Color.White
                Me.lblImage(i).ForeColor = Color.Black
            Next

            Me.pnlImage(iIndex).BackColor = Color.Blue
            Me.lblImage(iIndex).BackColor = Color.Blue
            Me.lblImage(iIndex).ForeColor = Color.White

            SetImage(SelTag)

            Me.CheckCurrentImage()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function DownloadAllImages() As Boolean
        Dim iProgress As Integer = 1

        Me.bwLoadImages.ReportProgress(Scraper.tmpTVDBShow.Episodes.Count + Scraper.tmpTVDBShow.Fanarts.Count + Scraper.tmpTVDBShow.Posters.Count + _
                                           Scraper.tmpTVDBShow.SeasonBanners.Count + Scraper.tmpTVDBShow.SeasonLandscapes.Count + Scraper.tmpTVDBShow.SeasonPosters.Count + _
                                           Scraper.tmpTVDBShow.ShowBanners.Count + Scraper.tmpTVDBShow.ShowCharacterArts.Count + Scraper.tmpTVDBShow.ShowClearArts.Count + _
                                           Scraper.tmpTVDBShow.ShowClearLogos.Count + Scraper.tmpTVDBShow.ShowLandscapes.Count + Scraper.tmpTVDBShow.Posters.Count, "max")

        'Episode Poster
        If Me._type = Enums.ImageType_TV.All Then
            For Each Epi As Structures.DBTV In Scraper.tmpTVDBShow.Episodes
                If Not File.Exists(Epi.TVEp.LocalFile) Then
                    If Not String.IsNullOrEmpty(Epi.TVEp.PosterURL) Then
                        Epi.TVEp.Poster.FromWeb(Epi.TVEp.PosterURL)
                        If Not IsNothing(Epi.TVEp.Poster.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(Epi.TVEp.LocalFile).FullName)
                            Epi.TVEp.Poster.Save(Epi.TVEp.LocalFile)
                        End If
                    End If
                Else
                    Epi.TVEp.Poster.FromFile(Epi.TVEp.LocalFile)
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster Then
            For Each tImg As Scraper.TVDBSeasonPoster In Scraper.tmpTVDBShow.SeasonPosters
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    SeasonPosterList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    SeasonPosterList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            SeasonPosterList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            SeasonPosterList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Banner
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonBanner Then
            For Each tImg As Scraper.TVDBSeasonBanner In Scraper.tmpTVDBShow.SeasonBanners
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    SeasonBannerList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    SeasonBannerList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            SeasonBannerList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            SeasonBannerList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Landscape
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonLandscape Then
            For Each tImg As Scraper.TVDBSeasonLandscape In Scraper.tmpTVDBShow.SeasonLandscapes
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    SeasonLandscapeList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    SeasonLandscapeList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            SeasonLandscapeList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            SeasonLandscapeList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show/AllSeason Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            For Each tImg As Scraper.TVDBPoster In Scraper.tmpTVDBShow.Posters
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    GenericPosterList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    GenericPosterList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            GenericPosterList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            GenericPosterList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show/AllSeason Banner
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner Then
            For Each tImg As Scraper.TVDBShowBanner In Scraper.tmpTVDBShow.ShowBanners
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    ShowBannerList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    ShowBannerList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            ShowBannerList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            ShowBannerList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show CharacterArt
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowCharacterArt Then
            For Each tImg As Scraper.TVDBShowCharacterArt In Scraper.tmpTVDBShow.ShowCharacterArts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    ShowCharacterArtList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    ShowCharacterArtList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            ShowCharacterArtList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            ShowCharacterArtList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show ClearArt
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearArt Then
            For Each tImg As Scraper.TVDBShowClearArt In Scraper.tmpTVDBShow.ShowClearArts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    ShowClearArtList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    ShowClearArtList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            ShowClearArtList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            ShowClearArtList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show ClearLogo
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearLogo Then
            For Each tImg As Scraper.TVDBShowClearLogo In Scraper.tmpTVDBShow.ShowClearLogos
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    ShowClearLogoList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    ShowClearLogoList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            ShowClearLogoList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            ShowClearLogoList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show/AllSeason Landscape
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape Then
            For Each tImg As Scraper.TVDBShowLandscape In Scraper.tmpTVDBShow.ShowLandscapes
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    ShowLandscapeList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    ShowLandscapeList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            ShowLandscapeList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            ShowLandscapeList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show/AllSeason/Season/Episode Fanart
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart OrElse Me._type = Enums.ImageType_TV.SeasonFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart Then
            For Each tImg As Scraper.TVDBFanart In Scraper.tmpTVDBShow.Fanarts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.Image.FromFile(tImg.LocalThumb)
                    GenericFanartList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.Image.FromFile(tImg.LocalFile)
                    GenericFanartList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.Image.FromWeb(tImg.ThumbURL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.Image.Save(tImg.LocalThumb)
                            GenericFanartList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.Image.FromWeb(tImg.URL)
                        If Not IsNothing(tImg.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.Image.Save(tImg.LocalFile)
                            GenericFanartList.Add(tImg)
                        End If
                    End If
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        Return Me.SetDefaults()
    End Function

    Private Sub DownloadFullsizeImage(ByVal iTag As ImageTag, ByRef tImage As Images)
        Dim sHTTP As New HTTP

        If Not String.IsNullOrEmpty(iTag.Path) AndAlso File.Exists(iTag.Path) Then
            tImage.FromFile(iTag.Path)
        ElseIf Not String.IsNullOrEmpty(iTag.Path) AndAlso Not String.IsNullOrEmpty(iTag.URL) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True

            Application.DoEvents()

            tImage.FromWeb(iTag.URL)
            If Not IsNothing(tImage.Image) Then
                Directory.CreateDirectory(Directory.GetParent(iTag.Path).FullName)
                tImage.Save(iTag.Path)
            End If

            sHTTP = Nothing

            Me.pnlStatus.Visible = False
        End If

    End Sub

    Private Sub GenerateList()
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = "Show Banner", .Tag = "showb", .ImageIndex = 0, .SelectedImageIndex = 0})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowCharacterArt) AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1011, "Show CharacterArt"), .Tag = "showch", .ImageIndex = 1, .SelectedImageIndex = 1})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearArt) AndAlso Master.eSettings.TVShowClearArtAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1013, "Show ClearArt"), .Tag = "showca", .ImageIndex = 2, .SelectedImageIndex = 2})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearLogo) AndAlso Master.eSettings.TVShowClearLogoAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1012, "Show ClearLogo"), .Tag = "showcl", .ImageIndex = 3, .SelectedImageIndex = 3})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) AndAlso (Master.eSettings.TVShowFanartAnyEnabled OrElse Master.eSettings.TVEpisodeFanartAnyEnabled) Then Me.tvList.Nodes.Add(New TreeNode With {.Text = If(Me._type = Enums.ImageType_TV.EpisodeFanart, Master.eLang.GetString(688, "Episode Fanart"), Master.eLang.GetString(684, "Show Fanart")), .Tag = "showf", .ImageIndex = 4, .SelectedImageIndex = 4})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape) AndAlso Master.eSettings.TVShowLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1010, "Show Landscape"), .Tag = "showl", .ImageIndex = 5, .SelectedImageIndex = 5})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(956, "Show Poster"), .Tag = "showp", .ImageIndex = 6, .SelectedImageIndex = 6})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1014, "All Seasons Banner"), .Tag = "allb", .ImageIndex = 0, .SelectedImageIndex = 0})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1015, "All Seasons Fanart"), .Tag = "allf", .ImageIndex = 4, .SelectedImageIndex = 4})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape) AndAlso Master.eSettings.TVASLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1016, "All Seasons Landscape"), .Tag = "alll", .ImageIndex = 5, .SelectedImageIndex = 5})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(735, "All Seasons Poster"), .Tag = "allp", .ImageIndex = 6, .SelectedImageIndex = 6})

        Dim TnS As TreeNode
        If Me._type = Enums.ImageType_TV.All Then
            For Each cSeason As Scraper.TVDBSeasonImage In Scraper.TVDBImages.SeasonImageList.OrderBy(Function(s) s.Season)
                Try
                    TnS = New TreeNode(String.Format(Master.eLang.GetString(726, "Season {0}"), cSeason.Season), 7, 7)
                    If Master.eSettings.TVSeasonBannerAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1017, "Season Banner"), .Tag = String.Concat("b", cSeason.Season.ToString), .ImageIndex = 0, .SelectedImageIndex = 0})
                    If Master.eSettings.TVSeasonFanartAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(686, "Season Fanart"), .Tag = String.Concat("f", cSeason.Season.ToString), .ImageIndex = 4, .SelectedImageIndex = 4})
                    If Master.eSettings.TVSeasonLandscapeAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1018, "Season Landscape"), .Tag = String.Concat("l", cSeason.Season.ToString), .ImageIndex = 5, .SelectedImageIndex = 5})
                    If Master.eSettings.TVSeasonPosterAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(685, "Season Posters"), .Tag = String.Concat("p", cSeason.Season.ToString), .ImageIndex = 6, .SelectedImageIndex = 6})
                    Me.tvList.Nodes.Add(TnS)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Next
        ElseIf Me._type = Enums.ImageType_TV.SeasonBanner Then
            If Master.eSettings.TVSeasonBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(1019, "Season {0} Banner"), Me._season), .Tag = String.Concat("b", Me._season)})
        ElseIf Me._type = Enums.ImageType_TV.SeasonFanart Then
            If Master.eSettings.TVSeasonFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(962, "Season {0} Fanart"), Me._season), .Tag = String.Concat("f", Me._season)})
        ElseIf Me._type = Enums.ImageType_TV.SeasonLandscape Then
            If Master.eSettings.TVSeasonLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(1020, "Season {0} Landscape"), Me._season), .Tag = String.Concat("l", Me._season)})
        ElseIf Me._type = Enums.ImageType_TV.SeasonPoster Then
            If Master.eSettings.TVSeasonPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(961, "Season {0} Posters"), Me._season), .Tag = String.Concat("p", Me._season)})
        End If

        Me.tvList.ExpandAll()
    End Sub

    Private Function GetFanartDims(ByVal fSize As Size) As Enums.FanartSize
        Try
            If (fSize.Width > 1000 AndAlso fSize.Height > 750) OrElse (fSize.Height > 1000 AndAlso fSize.Width > 750) Then
                Return Enums.FanartSize.Lrg
            ElseIf (fSize.Width > 700 AndAlso fSize.Height > 400) OrElse (fSize.Height > 700 AndAlso fSize.Width > 400) Then
                Return Enums.FanartSize.Mid
            Else
                Return Enums.FanartSize.Small
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Function

    Private Function GetPosterDims(ByVal pSize As Size) As Enums.PosterSize
        Try
            If (pSize.Width > pSize.Height) AndAlso (pSize.Width > (pSize.Height * 2)) AndAlso (pSize.Width > 300) Then
                'at least twice as wide than tall... consider it wide (also make sure it's big enough)
                Return Enums.PosterSize.Wide
            ElseIf (pSize.Height > 1000 AndAlso pSize.Width > 750) OrElse (pSize.Width > 1000 AndAlso pSize.Height > 750) Then
                Return Enums.PosterSize.Xlrg
            ElseIf (pSize.Height > 700 AndAlso pSize.Width > 500) OrElse (pSize.Width > 700 AndAlso pSize.Height > 500) Then
                Return Enums.PosterSize.Lrg
            ElseIf (pSize.Height > 250 AndAlso pSize.Width > 150) OrElse (pSize.Width > 250 AndAlso pSize.Height > 150) Then
                Return Enums.PosterSize.Mid
            Else
                Return Enums.PosterSize.Small
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Function

    Private Sub lblImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iindex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelect(iindex, DirectCast(DirectCast(sender, Label).Tag, ImageTag))
    End Sub

    Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim vScrollPosition As Integer = pnlImages.VerticalScroll.Value
        vScrollPosition -= Math.Sign(e.Delta) * 50
        vScrollPosition = Math.Max(0, vScrollPosition)
        vScrollPosition = Math.Min(vScrollPosition, pnlImages.VerticalScroll.Maximum)
        pnlImages.AutoScrollPosition = New Point(pnlImages.AutoScrollPosition.X, vScrollPosition)
        pnlImages.Invalidate()
    End Sub

    Private Sub pbDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbDelete.Click
        Me.pbCurrent.Image = Nothing
        Me.pbCurrent.Tag = Nothing
        Me.SetImage(New ImageTag)
    End Sub

    Private Sub pbImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, ImageTag))
    End Sub

    Private Sub pbImage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tImages As New Images
        Dim tImage As Image = Nothing
        Dim iTag As ImageTag = DirectCast(DirectCast(sender, PictureBox).Tag, ImageTag)
        If Not IsNothing(iTag) OrElse Not iTag.isFanart Then
            DownloadFullsizeImage(iTag, tImages)
            tImage = tImages.Image
        Else
            tImage = DirectCast(sender, PictureBox).Image
        End If

        ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage)
    End Sub

    Private Sub pbUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbUndo.Click
        If Me.SelSeason = -999 Then
            If SelImgType = Enums.ImageType_TV.ShowBanner Then
                Scraper.TVDBImages.ShowBanner.Image = DefaultImages.ShowBanner.Image
                Scraper.TVDBImages.ShowBanner.LocalFile = DefaultImages.ShowBanner.LocalFile
                Scraper.TVDBImages.ShowBanner.URL = DefaultImages.ShowBanner.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowBanner.Image
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                Scraper.TVDBImages.ShowCharacterArt.Image = DefaultImages.ShowCharacterArt.Image
                Scraper.TVDBImages.ShowCharacterArt.LocalFile = DefaultImages.ShowCharacterArt.LocalFile
                Scraper.TVDBImages.ShowCharacterArt.URL = DefaultImages.ShowCharacterArt.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowCharacterArt.Image
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                Scraper.TVDBImages.ShowClearArt.Image = DefaultImages.ShowClearArt.Image
                Scraper.TVDBImages.ShowClearArt.LocalFile = DefaultImages.ShowClearArt.LocalFile
                Scraper.TVDBImages.ShowClearArt.URL = DefaultImages.ShowClearArt.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearArt.Image
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                Scraper.TVDBImages.ShowClearLogo.Image = DefaultImages.ShowClearLogo.Image
                Scraper.TVDBImages.ShowClearLogo.LocalFile = DefaultImages.ShowClearLogo.LocalFile
                Scraper.TVDBImages.ShowClearLogo.URL = DefaultImages.ShowClearLogo.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearLogo.Image
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                Scraper.TVDBImages.ShowFanart.Image = DefaultImages.ShowFanart.Image
                Scraper.TVDBImages.ShowFanart.LocalFile = DefaultImages.ShowFanart.LocalFile
                Scraper.TVDBImages.ShowFanart.URL = DefaultImages.ShowFanart.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.Image
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                Scraper.TVDBImages.ShowPoster.Image = DefaultImages.ShowPoster.Image
                Scraper.TVDBImages.ShowPoster.LocalFile = DefaultImages.ShowPoster.LocalFile
                Scraper.TVDBImages.ShowPoster.URL = DefaultImages.ShowPoster.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowPoster.Image
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                Scraper.TVDBImages.AllSeasonsBanner.Image = DefaultImages.AllSeasonsBanner.Image
                Scraper.TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                Scraper.TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.Image
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                Scraper.TVDBImages.AllSeasonsFanart.Image = DefaultImages.AllSeasonsFanart.Image
                Scraper.TVDBImages.AllSeasonsFanart.LocalFile = DefaultImages.AllSeasonsFanart.LocalFile
                Scraper.TVDBImages.AllSeasonsFanart.URL = DefaultImages.AllSeasonsFanart.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.Image
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                Scraper.TVDBImages.AllSeasonsLandscape.Image = DefaultImages.AllSeasonsLandscape.Image
                Scraper.TVDBImages.AllSeasonsLandscape.LocalFile = DefaultImages.AllSeasonsLandscape.LocalFile
                Scraper.TVDBImages.AllSeasonsLandscape.URL = DefaultImages.AllSeasonsLandscape.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsLandscape.Image
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                Scraper.TVDBImages.AllSeasonsPoster.Image = DefaultImages.AllSeasonsPoster.Image
                Scraper.TVDBImages.AllSeasonsPoster.LocalFile = DefaultImages.AllSeasonsPoster.LocalFile
                Scraper.TVDBImages.AllSeasonsPoster.URL = DefaultImages.AllSeasonsPoster.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsPoster.Image
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                Dim dSPost As Scraper.TVDBSeasonBanner = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner = dSPost
                Me.pbCurrent.Image = dSPost.Image.Image
                Me.pbCurrent.Tag = dSPost.Image

                'Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner

                'Scraper.TVDBImages.SeasonImageList..Image = DefaultImages.AllSeasonsBanner.Image
                'Scraper.TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                'Scraper.TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                'Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.Image.Image
                'Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.Image

            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                Dim dSFan As Scraper.TVDBFanart = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                Dim tSFan As Scraper.TVDBFanart = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tSFan.Image = dSFan.Image
                tSFan.LocalFile = dSFan.LocalFile
                tSFan.URL = dSFan.URL
                Me.pbCurrent.Image = dSFan.Image.Image
                Me.pbCurrent.Tag = dSFan.Image
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                Dim dSPost As Scraper.TVDBSeasonLandscape = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape = dSPost
                Me.pbCurrent.Image = dSPost.Image.Image
                Me.pbCurrent.Tag = dSPost.Image
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                Dim dSPost As Scraper.TVDBSeasonPoster = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster = dSPost
                Me.pbCurrent.Image = dSPost.Image.Image
                Me.pbCurrent.Tag = dSPost.Image
            End If
        End If
    End Sub

    Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelect(iIndex, DirectCast(DirectCast(sender, Panel).Tag, ImageTag))
    End Sub

    Private Sub SetImage(ByVal SelTag As ImageTag)
        If Not IsNothing(SelTag.ImageObj) Then
            Me.pbCurrent.Image = SelTag.ImageObj.Image
            Me.pbCurrent.Tag = SelTag.ImageObj
        End If

        If Me.SelSeason = -999 Then
            If SelImgType = Enums.ImageType_TV.ShowBanner Then
                Scraper.TVDBImages.ShowBanner.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowBanner.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowBanner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                Scraper.TVDBImages.ShowCharacterArt.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowCharacterArt.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowCharacterArt.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                Scraper.TVDBImages.ShowClearArt.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowClearArt.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowClearArt.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                Scraper.TVDBImages.ShowClearLogo.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowClearLogo.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowClearLogo.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowLandscape Then
                Scraper.TVDBImages.ShowLandscape.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowLandscape.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowLandscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                Scraper.TVDBImages.ShowFanart.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowFanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowFanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                Scraper.TVDBImages.ShowPoster.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowPoster.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowPoster.URL = SelTag.URL
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                Scraper.TVDBImages.AllSeasonsBanner.Image = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsBanner.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsBanner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                Scraper.TVDBImages.AllSeasonsFanart.Image = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsFanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsFanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                Scraper.TVDBImages.AllSeasonsLandscape.Image = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsLandscape.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsLandscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                Scraper.TVDBImages.AllSeasonsPoster.Image = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsPoster.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsPoster.URL = SelTag.URL
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.Image = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.Image = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.Image = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster.Image = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster.URL = SelTag.URL
            End If
        End If
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(963, "TV Image Selection")
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.lblCurrentImage.Text = Master.eLang.GetString(964, "Current Image:")
    End Sub

    Private Sub tvList_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvList.AfterSelect
        Dim iCount As Integer = 0

        ClearImages()
        If Not IsNothing(e.Node.Tag) AndAlso Not String.IsNullOrEmpty(e.Node.Tag.ToString) Then
            Me.pbCurrent.Visible = True
            Me.lblCurrentImage.Visible = True

            If e.Node.Tag.ToString = "showb" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowBanner
                If Not IsNothing(Scraper.TVDBImages.ShowBanner) AndAlso Not IsNothing(Scraper.TVDBImages.ShowBanner.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowBanner.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = ShowBannerList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(ShowBannerList(i)) AndAlso Not IsNothing(ShowBannerList(i).Image) AndAlso Not IsNothing(ShowBannerList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If ShowBannerList(i).Size.Width = 0 OrElse ShowBannerList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", ShowBannerList(i).Image.Image.Size.Width, ShowBannerList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", ShowBannerList(i).Size.Width, ShowBannerList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = ShowBannerList(i).URL, .Path = ShowBannerList(i).LocalFile, .isFanart = False, .ImageObj = ShowBannerList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "showch" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowCharacterArt
                If Not IsNothing(Scraper.TVDBImages.ShowCharacterArt) AndAlso Not IsNothing(Scraper.TVDBImages.ShowCharacterArt.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowCharacterArt.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = ShowCharacterArtList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(ShowCharacterArtList(i)) AndAlso Not IsNothing(ShowCharacterArtList(i).Image) AndAlso Not IsNothing(ShowCharacterArtList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If ShowCharacterArtList(i).Size.Width = 0 OrElse ShowCharacterArtList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", ShowCharacterArtList(i).Image.Image.Size.Width, ShowCharacterArtList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", ShowCharacterArtList(i).Size.Width, ShowCharacterArtList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = ShowCharacterArtList(i).URL, .Path = ShowCharacterArtList(i).LocalFile, .isFanart = False, .ImageObj = ShowCharacterArtList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "showca" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearArt
                If Not IsNothing(Scraper.TVDBImages.ShowClearArt) AndAlso Not IsNothing(Scraper.TVDBImages.ShowClearArt.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowClearArt.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = ShowClearArtList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(ShowClearArtList(i)) AndAlso Not IsNothing(ShowClearArtList(i).Image) AndAlso Not IsNothing(ShowClearArtList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If ShowClearArtList(i).Size.Width = 0 OrElse ShowClearArtList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", ShowClearArtList(i).Image.Image.Size.Width, ShowClearArtList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", ShowClearArtList(i).Size.Width, ShowClearArtList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = ShowClearArtList(i).URL, .Path = ShowClearArtList(i).LocalFile, .isFanart = False, .ImageObj = ShowClearArtList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "showcl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearLogo
                If Not IsNothing(Scraper.TVDBImages.ShowClearLogo) AndAlso Not IsNothing(Scraper.TVDBImages.ShowClearLogo.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowClearLogo.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = ShowClearLogoList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(ShowClearLogoList(i)) AndAlso Not IsNothing(ShowClearLogoList(i).Image) AndAlso Not IsNothing(ShowClearLogoList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If ShowClearLogoList(i).Size.Width = 0 OrElse ShowClearLogoList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", ShowClearLogoList(i).Image.Image.Size.Width, ShowClearLogoList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", ShowClearLogoList(i).Size.Width, ShowClearLogoList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = ShowClearLogoList(i).URL, .Path = ShowClearLogoList(i).LocalFile, .isFanart = False, .ImageObj = ShowClearLogoList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "showf" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowFanart
                If Not IsNothing(Scraper.TVDBImages.ShowFanart) AndAlso Not IsNothing(Scraper.TVDBImages.ShowFanart.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                For i = 0 To GenericFanartList.Count - 1
                    If Not IsNothing(GenericFanartList(i)) AndAlso Not IsNothing(GenericFanartList(i).Image) AndAlso Not IsNothing(GenericFanartList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If GenericFanartList(i).Size.Width = 0 OrElse GenericFanartList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", GenericFanartList(i).Image.Image.Size.Width, GenericFanartList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", GenericFanartList(i).Size.Width, GenericFanartList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = GenericFanartList(i).URL, .Path = GenericFanartList(i).LocalFile, .isFanart = True, .ImageObj = GenericFanartList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "showl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowLandscape
                If Not IsNothing(Scraper.TVDBImages.ShowLandscape) AndAlso Not IsNothing(Scraper.TVDBImages.ShowLandscape.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowLandscape.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowLandscape.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = ShowLandscapeList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(ShowLandscapeList(i)) AndAlso Not IsNothing(ShowLandscapeList(i).Image) AndAlso Not IsNothing(ShowLandscapeList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If ShowLandscapeList(i).Size.Width = 0 OrElse ShowLandscapeList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", ShowLandscapeList(i).Image.Image.Size.Width, ShowLandscapeList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", ShowLandscapeList(i).Size.Width, ShowLandscapeList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = ShowLandscapeList(i).URL, .Path = ShowLandscapeList(i).LocalFile, .isFanart = False, .ImageObj = ShowLandscapeList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "showp" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowPoster
                If Not IsNothing(Scraper.TVDBImages.ShowPoster) AndAlso Not IsNothing(Scraper.TVDBImages.ShowPoster.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                For i = 0 To GenericPosterList.Count - 1
                    If Not IsNothing(GenericPosterList(i)) AndAlso Not IsNothing(GenericPosterList(i).Image) AndAlso Not IsNothing(GenericPosterList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If GenericPosterList(i).Size.Width = 0 OrElse GenericPosterList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", GenericPosterList(i).Image.Image.Size.Width, GenericPosterList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", GenericPosterList(i).Size.Width, GenericPosterList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i + iCount, New ImageTag With {.URL = GenericPosterList(i).URL, .Path = GenericPosterList(i).LocalFile, .isFanart = False, .ImageObj = GenericPosterList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "allb" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsBanner
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = ShowBannerList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(ShowBannerList(i)) AndAlso Not IsNothing(ShowBannerList(i).Image) AndAlso Not IsNothing(ShowBannerList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If ShowBannerList(i).Size.Width = 0 OrElse ShowBannerList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", ShowBannerList(i).Image.Image.Size.Width, ShowBannerList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", ShowBannerList(i).Size.Width, ShowBannerList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = ShowBannerList(i).URL, .Path = ShowBannerList(i).LocalFile, .isFanart = False, .ImageObj = ShowBannerList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "allf" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsFanart
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                For i = 0 To GenericFanartList.Count - 1
                    If Not IsNothing(GenericFanartList(i)) AndAlso Not IsNothing(GenericFanartList(i).Image) AndAlso Not IsNothing(GenericFanartList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If GenericFanartList(i).Size.Width = 0 OrElse GenericFanartList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", GenericFanartList(i).Image.Image.Size.Width, GenericFanartList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", GenericFanartList(i).Size.Width, GenericFanartList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = GenericFanartList(i).URL, .Path = GenericFanartList(i).LocalFile, .isFanart = True, .ImageObj = GenericFanartList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "alll" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsLandscape
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsLandscape) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsLandscape.Image) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsLandscape.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = ShowLandscapeList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(ShowLandscapeList(i)) AndAlso Not IsNothing(ShowLandscapeList(i).Image) AndAlso Not IsNothing(ShowLandscapeList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If ShowLandscapeList(i).Size.Width = 0 OrElse ShowLandscapeList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", ShowLandscapeList(i).Image.Image.Size.Width, ShowLandscapeList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", ShowLandscapeList(i).Size.Width, ShowLandscapeList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = ShowLandscapeList(i).URL, .Path = ShowLandscapeList(i).LocalFile, .isFanart = False, .ImageObj = ShowLandscapeList(i).Image})
                    End If
                Next

            ElseIf e.Node.Tag.ToString = "allp" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsPoster
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image.Image) Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.Image.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = GenericPosterList.Count
                For i = 0 To iCount - 1
                    If Not IsNothing(GenericPosterList(i)) AndAlso Not IsNothing(GenericPosterList(i).Image) AndAlso Not IsNothing(GenericPosterList(i).Image.Image) Then
                        Dim imgSize As String = String.Empty
                        If GenericPosterList(i).Size.Width = 0 OrElse GenericPosterList(i).Size.Height = 0 Then
                            imgSize = String.Format("{0}x{1}", GenericPosterList(i).Image.Image.Size.Width, GenericPosterList(i).Image.Image.Size.Height)
                        Else
                            imgSize = String.Format("{0}x{1}", GenericPosterList(i).Size.Width, GenericPosterList(i).Size.Height)
                        End If
                        Me.AddImage(imgSize, i, New ImageTag With {.URL = GenericPosterList(i).URL, .Path = GenericPosterList(i).LocalFile, .isFanart = False, .ImageObj = GenericPosterList(i).Image})
                    End If
                Next
            Else
                Dim tMatch As Match = Regex.Match(e.Node.Tag.ToString, "(?<type>f|p|b|l)(?<num>[0-9]+)")
                If tMatch.Success Then
                    If tMatch.Groups("type").Value = "b" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonBanner
                        Dim tBanner As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If Not IsNothing(tBanner) AndAlso Not IsNothing(tBanner.Banner) AndAlso Not IsNothing(tBanner.Banner.Image) Then
                            Me.pbCurrent.Image = tBanner.Banner.Image.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each SImage As Scraper.TVDBSeasonBanner In SeasonBannerList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If Not IsNothing(SImage.Image) AndAlso Not IsNothing(SImage.Image.Image) Then
                                Dim imgSize As String = String.Empty
                                If SImage.Size.Width = 0 OrElse SImage.Size.Height = 0 Then
                                    imgSize = String.Format("{0}x{1}", SImage.Image.Image.Size.Width, SImage.Image.Image.Size.Height)
                                Else
                                    imgSize = String.Format("{0}x{1}", SImage.Size.Width, SImage.Size.Height)
                                End If
                                Me.AddImage(imgSize, iCount, New ImageTag With {.URL = SImage.URL, .Path = SImage.LocalFile, .isFanart = False, .ImageObj = SImage.Image})
                            End If
                            iCount += 1
                        Next

                    ElseIf tMatch.Groups("type").Value = "f" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonFanart
                        Dim tFanart As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                        If Not IsNothing(tFanart) AndAlso Not IsNothing(tFanart.Fanart) AndAlso Not IsNothing(tFanart.Fanart.Image) AndAlso Not IsNothing(tFanart.Fanart.Image.Image) Then
                            Me.pbCurrent.Image = tFanart.Fanart.Image.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        For i = 0 To GenericFanartList.Count - 1
                            If Not IsNothing(GenericFanartList(i)) AndAlso Not IsNothing(GenericFanartList(i).Image) AndAlso Not IsNothing(GenericFanartList(i).Image.Image) Then
                                Dim imgSize As String = String.Empty
                                If GenericFanartList(i).Size.Width = 0 OrElse GenericFanartList(i).Size.Height = 0 Then
                                    imgSize = String.Format("{0}x{1}", GenericFanartList(i).Image.Image.Size.Width, GenericFanartList(i).Image.Image.Size.Height)
                                Else
                                    imgSize = String.Format("{0}x{1}", GenericFanartList(i).Size.Width, GenericFanartList(i).Size.Height)
                                End If
                                Me.AddImage(imgSize, i, New ImageTag With {.URL = GenericFanartList(i).URL, .Path = GenericFanartList(i).LocalFile, .isFanart = True, .ImageObj = GenericFanartList(i).Image})
                            End If
                        Next

                    ElseIf tMatch.Groups("type").Value = "l" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonLandscape
                        Dim tLandscape As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If Not IsNothing(tLandscape) AndAlso Not IsNothing(tLandscape.Landscape) AndAlso Not IsNothing(tLandscape.Landscape.Image) Then
                            Me.pbCurrent.Image = tLandscape.Landscape.Image.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each SImage As Scraper.TVDBSeasonLandscape In SeasonLandscapeList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If Not IsNothing(SImage.Image) AndAlso Not IsNothing(SImage.Image.Image) Then
                                Dim imgSize As String = String.Empty
                                If SImage.Size.Width = 0 OrElse SImage.Size.Height = 0 Then
                                    imgSize = String.Format("{0}x{1}", SImage.Image.Image.Size.Width, SImage.Image.Image.Size.Height)
                                Else
                                    imgSize = String.Format("{0}x{1}", SImage.Size.Width, SImage.Size.Height)
                                End If
                                Me.AddImage(imgSize, iCount, New ImageTag With {.URL = SImage.URL, .Path = SImage.LocalFile, .isFanart = False, .ImageObj = SImage.Image})
                            End If
                            iCount += 1
                        Next

                    ElseIf tMatch.Groups("type").Value = "p" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonPoster
                        Dim tPoster As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If Not IsNothing(tPoster) AndAlso Not IsNothing(tPoster.Poster) AndAlso Not IsNothing(tPoster.Poster.Image) Then
                            Me.pbCurrent.Image = tPoster.Poster.Image.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each SImage As Scraper.TVDBSeasonPoster In SeasonPosterList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If Not IsNothing(SImage.Image) AndAlso Not IsNothing(SImage.Image.Image) Then
                                Dim imgSize As String = String.Empty
                                If SImage.Size.Width = 0 OrElse SImage.Size.Height = 0 Then
                                    imgSize = String.Format("{0}x{1}", SImage.Image.Image.Size.Width, SImage.Image.Image.Size.Height)
                                Else
                                    imgSize = String.Format("{0}x{1}", SImage.Size.Width, SImage.Size.Height)
                                End If
                                Me.AddImage(imgSize, iCount, New ImageTag With {.URL = SImage.URL, .Path = SImage.LocalFile, .isFanart = False, .ImageObj = SImage.Image})
                            End If
                            iCount += 1
                        Next
                    End If
                End If
            End If
        Else
            Me.pbCurrent.Image = Nothing
            Me.pbCurrent.Visible = False
            Me.lblCurrentImage.Visible = False
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure ImageTag

#Region "Fields"

        Dim isFanart As Boolean
        Dim Path As String
		Dim URL As String
		Dim ImageObj As Images

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class