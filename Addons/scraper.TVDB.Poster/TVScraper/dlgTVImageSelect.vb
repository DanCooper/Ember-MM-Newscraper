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
    Private GenericFanartList As New List(Of MediaContainers.Image)
    Private GenericPosterList As New List(Of MediaContainers.Image)
    Private iCounter As Integer = 0
    Private iLeft As Integer = 5
    Private iTop As Integer = 5
    Private lblImage() As Label
    Private pbImage() As PictureBox
    Private pnlImage() As Panel
    Private SeasonPosterList As New List(Of MediaContainers.Image)
    Private SeasonBannerList As New List(Of MediaContainers.Image)
    Private SeasonLandscapeList As New List(Of MediaContainers.Image)
    Private SelImgType As Enums.ImageType_TV
    Private SelSeason As Integer = -999
    Private ShowBannerList As New List(Of MediaContainers.Image)
    Private ShowCharacterArtList As New List(Of MediaContainers.Image)
    Private ShowClearArtList As New List(Of MediaContainers.Image)
    Private ShowClearLogoList As New List(Of MediaContainers.Image)
    Private ShowLandscapeList As New List(Of MediaContainers.Image)
    Private ShowPosterList As New List(Of MediaContainers.Image)
    Private _id As Integer = -1
    Private _season As Integer = -999
    Private _type As Enums.ImageType_TV = Enums.ImageType_TV.All
    Private _withcurrent As Boolean = True
    Private _ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV

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

        Me.bwLoadImages.ReportProgress(Scraper.TVDBImages.SeasonImageList.Count + Scraper.tmpTVDBShow.Episodes.Count + iProgress, "defaults")

        'AllSeason Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled AndAlso Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASBanner(SeasonBannerList, ShowBannerList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.AllSeasonsBanner.WebImage = defImg.WebImage
                Scraper.TVDBImages.AllSeasonsBanner.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.AllSeasonsBanner.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.AllSeasonsBanner.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.AllSeasonsBanner.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(1, "progress")

        'AllSeason Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled AndAlso Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASFanart(GenericFanartList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.AllSeasonsFanart.WebImage = defImg.WebImage
                Scraper.TVDBImages.AllSeasonsFanart.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.AllSeasonsFanart.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.AllSeasonsFanart.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.AllSeasonsFanart.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(2, "progress")

        'AllSeason Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape) AndAlso Master.eSettings.TVASLandscapeAnyEnabled AndAlso Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASLandscape(SeasonLandscapeList, ShowLandscapeList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.AllSeasonsLandscape.WebImage = defImg.WebImage
                Scraper.TVDBImages.AllSeasonsLandscape.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.AllSeasonsLandscape.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.AllSeasonsLandscape.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.AllSeasonsLandscape.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(3, "progress")

        'AllSeason Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled AndAlso Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASPoster(SeasonPosterList, GenericPosterList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.AllSeasonsPoster.WebImage = defImg.WebImage
                Scraper.TVDBImages.AllSeasonsPoster.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.AllSeasonsPoster.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.AllSeasonsPoster.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.AllSeasonsPoster.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(4, "progress")

        'Show Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled AndAlso Scraper.TVDBImages.ShowBanner.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowBanner(ShowBannerList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.ShowBanner.WebImage = defImg.WebImage
                Scraper.TVDBImages.ShowBanner.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.ShowBanner.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.ShowBanner.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.ShowBanner.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(5, "progress")

        'Show CharacterArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowCharacterArt) AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso Scraper.TVDBImages.ShowCharacterArt.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowCharacterArt(ShowCharacterArtList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.ShowCharacterArt.WebImage = defImg.WebImage
                Scraper.TVDBImages.ShowCharacterArt.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.ShowCharacterArt.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.ShowCharacterArt.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.ShowCharacterArt.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(6, "progress")

        'Show ClearArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearArt) AndAlso Master.eSettings.TVShowClearArtAnyEnabled AndAlso Scraper.TVDBImages.ShowClearArt.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowClearArt(ShowClearArtList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.ShowClearArt.WebImage = defImg.WebImage
                Scraper.TVDBImages.ShowClearArt.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.ShowClearArt.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.ShowClearArt.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.ShowClearArt.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(7, "progress")

        'Show ClearLogo
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearLogo) AndAlso Master.eSettings.TVShowClearLogoAnyEnabled AndAlso Scraper.TVDBImages.ShowClearLogo.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowClearLogo(ShowClearLogoList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.ShowClearLogo.WebImage = defImg.WebImage
                Scraper.TVDBImages.ShowClearLogo.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.ShowClearLogo.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.ShowClearLogo.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.ShowClearLogo.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(8, "progress")

        'Show Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) AndAlso Scraper.TVDBImages.ShowFanart.WebImage.Image Is Nothing Then 'TODO: add *FanartEnabled check
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowFanart(GenericFanartList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.ShowFanart.WebImage = defImg.WebImage
                Scraper.TVDBImages.ShowFanart.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.ShowFanart.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.ShowFanart.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.ShowFanart.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(9, "progress")

        'Show Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape) AndAlso Master.eSettings.TVShowLandscapeAnyEnabled AndAlso Scraper.TVDBImages.ShowLandscape.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowLandscape(ShowLandscapeList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.ShowLandscape.WebImage = defImg.WebImage
                Scraper.TVDBImages.ShowLandscape.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.ShowLandscape.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.ShowLandscape.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.ShowLandscape.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(10, "progress")

        'Show Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled AndAlso Scraper.TVDBImages.ShowPoster.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowPoster(GenericPosterList, defImg)

            If defImg IsNot Nothing Then
                Scraper.TVDBImages.ShowPoster.WebImage = defImg.WebImage
                Scraper.TVDBImages.ShowPoster.LocalFile = defImg.LocalFile
                Scraper.TVDBImages.ShowPoster.LocalThumb = defImg.LocalThumb
                Scraper.TVDBImages.ShowPoster.ThumbURL = defImg.ThumbURL
                Scraper.TVDBImages.ShowPoster.URL = defImg.URL
            End If
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
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonBanner) AndAlso Master.eSettings.TVSeasonBannerAnyEnabled AndAlso cSeason.Banner.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonBanner(SeasonBannerList, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Banner.WebImage = defImg.WebImage
                            cSeason.Banner.LocalFile = defImg.LocalFile
                            cSeason.Banner.LocalThumb = defImg.LocalThumb
                            cSeason.Banner.ThumbURL = defImg.ThumbURL
                            cSeason.Banner.URL = defImg.URL
                        End If
                    End If

                    'Season Fanart
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonFanart) AndAlso Master.eSettings.TVSeasonFanartAnyEnabled AndAlso cSeason.Fanart.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonFanart(GenericFanartList, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Fanart.WebImage = defImg.WebImage
                            cSeason.Fanart.LocalFile = defImg.LocalFile
                            cSeason.Fanart.LocalThumb = defImg.LocalThumb
                            cSeason.Fanart.ThumbURL = defImg.ThumbURL
                            cSeason.Fanart.URL = defImg.URL
                        End If
                    End If

                    'Season Landscape
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonLandscape) AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso cSeason.Landscape.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonLandscape(SeasonLandscapeList, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Landscape.WebImage = defImg.WebImage
                            cSeason.Landscape.LocalFile = defImg.LocalFile
                            cSeason.Landscape.LocalThumb = defImg.LocalThumb
                            cSeason.Landscape.ThumbURL = defImg.ThumbURL
                            cSeason.Landscape.URL = defImg.URL
                        End If
                    End If

                    'Season Poster
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster) AndAlso Master.eSettings.TVSeasonPosterAnyEnabled AndAlso cSeason.Poster.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonPoster(SeasonPosterList, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Poster.WebImage = defImg.WebImage
                            cSeason.Poster.LocalFile = defImg.LocalFile
                            cSeason.Poster.LocalThumb = defImg.LocalThumb
                            cSeason.Poster.ThumbURL = defImg.ThumbURL
                            cSeason.Poster.URL = defImg.URL
                        End If
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
                        Episode.TVEp.Fanart.WebImage.FromFile(Episode.EpFanartPath)
                    ElseIf Scraper.TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                        Episode.TVEp.Fanart = Scraper.TVDBImages.ShowFanart
                    End If
                End If

                'Poster
                If Master.eSettings.TVEpisodePosterAnyEnabled Then
                    If Not String.IsNullOrEmpty(Episode.TVEp.LocalFile) Then
                        Episode.TVEp.Poster.WebImage.FromFile(Episode.TVEp.LocalFile)
                    ElseIf Not String.IsNullOrEmpty(Episode.EpPosterPath) Then
                        Episode.TVEp.Poster.WebImage.FromFile(Episode.EpPosterPath)
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

    Public Overloads Function ShowDialog(ByVal ShowID As Integer, ByVal Type As Enums.ImageType_TV, ByVal ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV, ByVal WithCurrent As Boolean) As System.Windows.Forms.DialogResult
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
                    Scraper.TVDBImages.ShowBanner.WebImage.FromFile(Scraper.TVDBImages.ShowBanner.LocalFile)
                    Master.currShow.ShowBannerPath = Scraper.TVDBImages.ShowBanner.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.LocalFile) Then
                    Scraper.TVDBImages.ShowBanner.WebImage.Clear()
                    Scraper.TVDBImages.ShowBanner.WebImage.FromWeb(Scraper.TVDBImages.ShowBanner.URL)
                    If Scraper.TVDBImages.ShowBanner.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowBanner.LocalFile).FullName)
                        Scraper.TVDBImages.ShowBanner.WebImage.Save(Scraper.TVDBImages.ShowBanner.LocalFile)
                        Master.currShow.ShowBannerPath = Scraper.TVDBImages.ShowBanner.LocalFile
                    End If
                End If
            End If

            'Show CharacterArt
            If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                    Scraper.TVDBImages.ShowCharacterArt.WebImage.FromFile(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                    Master.currShow.ShowCharacterArtPath = Scraper.TVDBImages.ShowCharacterArt.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                    Scraper.TVDBImages.ShowCharacterArt.WebImage.Clear()
                    Scraper.TVDBImages.ShowCharacterArt.WebImage.FromWeb(Scraper.TVDBImages.ShowCharacterArt.URL)
                    If Scraper.TVDBImages.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowCharacterArt.LocalFile).FullName)
                        Scraper.TVDBImages.ShowCharacterArt.WebImage.Save(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                        Master.currShow.ShowCharacterArtPath = Scraper.TVDBImages.ShowCharacterArt.LocalFile
                    End If
                End If
            End If

            'Show ClearArt
            If Master.eSettings.TVShowClearArtAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                    Scraper.TVDBImages.ShowClearArt.WebImage.FromFile(Scraper.TVDBImages.ShowClearArt.LocalFile)
                    Master.currShow.ShowClearArtPath = Scraper.TVDBImages.ShowClearArt.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                    Scraper.TVDBImages.ShowClearArt.WebImage.Clear()
                    Scraper.TVDBImages.ShowClearArt.WebImage.FromWeb(Scraper.TVDBImages.ShowClearArt.URL)
                    If Scraper.TVDBImages.ShowClearArt.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearArt.LocalFile).FullName)
                        Scraper.TVDBImages.ShowClearArt.WebImage.Save(Scraper.TVDBImages.ShowClearArt.LocalFile)
                        Master.currShow.ShowClearArtPath = Scraper.TVDBImages.ShowClearArt.LocalFile
                    End If
                End If
            End If

            'Show ClearLogo
            If Master.eSettings.TVShowClearLogoAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                    Scraper.TVDBImages.ShowClearLogo.WebImage.FromFile(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                    Master.currShow.ShowClearLogoPath = Scraper.TVDBImages.ShowClearLogo.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                    Scraper.TVDBImages.ShowClearLogo.WebImage.Clear()
                    Scraper.TVDBImages.ShowClearLogo.WebImage.FromWeb(Scraper.TVDBImages.ShowClearLogo.URL)
                    If Scraper.TVDBImages.ShowClearLogo.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearLogo.LocalFile).FullName)
                        Scraper.TVDBImages.ShowClearLogo.WebImage.Save(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                        Master.currShow.ShowClearLogoPath = Scraper.TVDBImages.ShowClearLogo.LocalFile
                    End If
                End If
            End If

            'Show Fanart
            If Master.eSettings.TVShowFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                    Scraper.TVDBImages.ShowFanart.WebImage.FromFile(Scraper.TVDBImages.ShowFanart.LocalFile)
                    Master.currShow.ShowFanartPath = Scraper.TVDBImages.ShowFanart.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                    Scraper.TVDBImages.ShowFanart.WebImage.Clear()
                    Scraper.TVDBImages.ShowFanart.WebImage.FromWeb(Scraper.TVDBImages.ShowFanart.URL)
                    If Scraper.TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowFanart.LocalFile).FullName)
                        Scraper.TVDBImages.ShowFanart.WebImage.Save(Scraper.TVDBImages.ShowFanart.LocalFile)
                        Master.currShow.ShowFanartPath = Scraper.TVDBImages.ShowFanart.LocalFile
                    End If
                End If
            End If

            'Show Landscape
            If Master.eSettings.TVShowLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                    Scraper.TVDBImages.ShowLandscape.WebImage.FromFile(Scraper.TVDBImages.ShowLandscape.LocalFile)
                    Master.currShow.ShowLandscapePath = Scraper.TVDBImages.ShowLandscape.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                    Scraper.TVDBImages.ShowLandscape.WebImage.Clear()
                    Scraper.TVDBImages.ShowLandscape.WebImage.FromWeb(Scraper.TVDBImages.ShowLandscape.URL)
                    If Scraper.TVDBImages.ShowLandscape.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowLandscape.LocalFile).FullName)
                        Scraper.TVDBImages.ShowLandscape.WebImage.Save(Scraper.TVDBImages.ShowLandscape.LocalFile)
                        Master.currShow.ShowLandscapePath = Scraper.TVDBImages.ShowLandscape.LocalFile
                    End If
                End If
            End If

            'Show Poster
            If Master.eSettings.TVShowPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                    Scraper.TVDBImages.ShowPoster.WebImage.FromFile(Scraper.TVDBImages.ShowPoster.LocalFile)
                    Master.currShow.ShowPosterPath = Scraper.TVDBImages.ShowPoster.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                    Scraper.TVDBImages.ShowPoster.WebImage.Clear()
                    Scraper.TVDBImages.ShowPoster.WebImage.FromWeb(Scraper.TVDBImages.ShowPoster.URL)
                    If Scraper.TVDBImages.ShowPoster.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowPoster.LocalFile).FullName)
                        Scraper.TVDBImages.ShowPoster.WebImage.Save(Scraper.TVDBImages.ShowPoster.LocalFile)
                        Master.currShow.ShowPosterPath = Scraper.TVDBImages.ShowPoster.LocalFile
                    End If
                End If
            End If

            'AS Banner
            If Master.eSettings.TVASBannerAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsBanner.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                    Master.currShow.SeasonBannerPath = Scraper.TVDBImages.AllSeasonsBanner.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsBanner.WebImage.Clear()
                    Scraper.TVDBImages.AllSeasonsBanner.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsBanner.URL)
                    If Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsBanner.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsBanner.WebImage.Save(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                        Master.currShow.SeasonBannerPath = Scraper.TVDBImages.AllSeasonsBanner.LocalFile
                    End If
                End If
            End If

            'AS Fanart
            If Master.eSettings.TVASFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsFanart.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                    Master.currShow.SeasonFanartPath = Scraper.TVDBImages.AllSeasonsFanart.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsFanart.WebImage.Clear()
                    Scraper.TVDBImages.AllSeasonsFanart.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsFanart.URL)
                    If Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsFanart.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsFanart.WebImage.Save(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                        Master.currShow.SeasonFanartPath = Scraper.TVDBImages.AllSeasonsFanart.LocalFile
                    End If
                End If
            End If

            'AS Landscape
            If Master.eSettings.TVASLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsLandscape.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                    Master.currShow.SeasonLandscapePath = Scraper.TVDBImages.AllSeasonsLandscape.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Clear()
                    Scraper.TVDBImages.AllSeasonsLandscape.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsLandscape.URL)
                    If Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Save(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                        Master.currShow.SeasonLandscapePath = Scraper.TVDBImages.AllSeasonsLandscape.LocalFile
                    End If
                End If
            End If

            'AS Poster
            If Master.eSettings.TVASPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsPoster.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                    Master.currShow.SeasonPosterPath = Scraper.TVDBImages.AllSeasonsPoster.LocalFile
                ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                    Scraper.TVDBImages.AllSeasonsPoster.WebImage.Clear()
                    Scraper.TVDBImages.AllSeasonsPoster.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsPoster.URL)
                    If Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsPoster.LocalFile).FullName)
                        Scraper.TVDBImages.AllSeasonsPoster.WebImage.Save(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                        Master.currShow.SeasonPosterPath = Scraper.TVDBImages.AllSeasonsPoster.LocalFile
                    End If
                End If
            End If

        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsBanner.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsBanner.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsBanner.WebImage.Clear()
                Scraper.TVDBImages.AllSeasonsBanner.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsBanner.URL)
                If Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsBanner.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsBanner.WebImage.Save(Scraper.TVDBImages.AllSeasonsBanner.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsFanart.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsFanart.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsFanart.WebImage.Clear()
                Scraper.TVDBImages.AllSeasonsFanart.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsFanart.URL)
                If Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsFanart.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsFanart.WebImage.Save(Scraper.TVDBImages.AllSeasonsFanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsLandscape.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsLandscape.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Clear()
                Scraper.TVDBImages.AllSeasonsLandscape.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsLandscape.URL)
                If Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Save(Scraper.TVDBImages.AllSeasonsLandscape.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsLandscape.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsPoster.WebImage.FromFile(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsPoster.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.AllSeasonsPoster.LocalFile) Then
                Scraper.TVDBImages.AllSeasonsPoster.WebImage.Clear()
                Scraper.TVDBImages.AllSeasonsPoster.WebImage.FromWeb(Scraper.TVDBImages.AllSeasonsPoster.URL)
                If Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.AllSeasonsPoster.LocalFile).FullName)
                    Scraper.TVDBImages.AllSeasonsPoster.WebImage.Save(Scraper.TVDBImages.AllSeasonsPoster.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsPoster.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage.FromFile(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Banner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Banner.URL)
                If Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage.Save(Scraper.TVDBImages.SeasonImageList(0).Banner.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Banner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage.FromFile(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Fanart.URL)
                If Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage.Save(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Fanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage.FromFile(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Landscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Landscape.URL)
                If Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage.Save(Scraper.TVDBImages.SeasonImageList(0).Landscape.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Landscape.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage.FromFile(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Poster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile) Then
                Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage.Clear()
                Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage.FromWeb(Scraper.TVDBImages.SeasonImageList(0).Poster.URL)
                If Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile).FullName)
                    Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage.Save(Scraper.TVDBImages.SeasonImageList(0).Poster.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Poster.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowBanner.LocalFile) Then
                Scraper.TVDBImages.ShowBanner.WebImage.FromFile(Scraper.TVDBImages.ShowBanner.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowBanner.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowBanner.LocalFile) Then
                Scraper.TVDBImages.ShowBanner.WebImage.Clear()
                Scraper.TVDBImages.ShowBanner.WebImage.FromWeb(Scraper.TVDBImages.ShowBanner.URL)
                If Scraper.TVDBImages.ShowBanner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowBanner.LocalFile).FullName)
                    Scraper.TVDBImages.ShowBanner.WebImage.Save(Scraper.TVDBImages.ShowBanner.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowBanner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowCharacterArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                Scraper.TVDBImages.ShowCharacterArt.WebImage.FromFile(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowCharacterArt.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowCharacterArt.LocalFile) Then
                Scraper.TVDBImages.ShowCharacterArt.WebImage.Clear()
                Scraper.TVDBImages.ShowCharacterArt.WebImage.FromWeb(Scraper.TVDBImages.ShowCharacterArt.URL)
                If Scraper.TVDBImages.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowCharacterArt.LocalFile).FullName)
                    Scraper.TVDBImages.ShowCharacterArt.WebImage.Save(Scraper.TVDBImages.ShowCharacterArt.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowCharacterArt.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                Scraper.TVDBImages.ShowClearArt.WebImage.FromFile(Scraper.TVDBImages.ShowClearArt.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearArt.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearArt.LocalFile) Then
                Scraper.TVDBImages.ShowClearArt.WebImage.Clear()
                Scraper.TVDBImages.ShowClearArt.WebImage.FromWeb(Scraper.TVDBImages.ShowClearArt.URL)
                If Scraper.TVDBImages.ShowClearArt.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearArt.LocalFile).FullName)
                    Scraper.TVDBImages.ShowClearArt.WebImage.Save(Scraper.TVDBImages.ShowClearArt.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearArt.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearLogo Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                Scraper.TVDBImages.ShowClearLogo.WebImage.FromFile(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearLogo.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowClearLogo.LocalFile) Then
                Scraper.TVDBImages.ShowClearLogo.WebImage.Clear()
                Scraper.TVDBImages.ShowClearLogo.WebImage.FromWeb(Scraper.TVDBImages.ShowClearLogo.URL)
                If Scraper.TVDBImages.ShowClearLogo.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowClearLogo.LocalFile).FullName)
                    Scraper.TVDBImages.ShowClearLogo.WebImage.Save(Scraper.TVDBImages.ShowClearLogo.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearLogo.WebImage
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                Scraper.TVDBImages.ShowFanart.WebImage.FromFile(Scraper.TVDBImages.ShowFanart.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowFanart.LocalFile) Then
                Scraper.TVDBImages.ShowFanart.WebImage.Clear()
                Scraper.TVDBImages.ShowFanart.WebImage.FromWeb(Scraper.TVDBImages.ShowFanart.URL)
                If Scraper.TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowFanart.LocalFile).FullName)
                    Scraper.TVDBImages.ShowFanart.WebImage.Save(Scraper.TVDBImages.ShowFanart.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                Scraper.TVDBImages.ShowLandscape.WebImage.FromFile(Scraper.TVDBImages.ShowLandscape.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowLandscape.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowLandscape.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowLandscape.LocalFile) Then
                Scraper.TVDBImages.ShowLandscape.WebImage.Clear()
                Scraper.TVDBImages.ShowLandscape.WebImage.FromWeb(Scraper.TVDBImages.ShowLandscape.URL)
                If Scraper.TVDBImages.ShowLandscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowLandscape.LocalFile).FullName)
                    Scraper.TVDBImages.ShowLandscape.WebImage.Save(Scraper.TVDBImages.ShowLandscape.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowLandscape.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowLandscape.WebImage
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowPoster OrElse Me._type = Enums.ImageType_TV.EpisodePoster) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) AndAlso File.Exists(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                Scraper.TVDBImages.ShowPoster.WebImage.FromFile(Scraper.TVDBImages.ShowPoster.LocalFile)
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowPoster.WebImage
            ElseIf Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(Scraper.TVDBImages.ShowPoster.LocalFile) Then
                Scraper.TVDBImages.ShowPoster.WebImage.Clear()
                Scraper.TVDBImages.ShowPoster.WebImage.FromWeb(Scraper.TVDBImages.ShowPoster.URL)
                If Scraper.TVDBImages.ShowPoster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(Scraper.TVDBImages.ShowPoster.LocalFile).FullName)
                    Scraper.TVDBImages.ShowPoster.WebImage.Save(Scraper.TVDBImages.ShowPoster.LocalFile)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.WebImage.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowPoster.WebImage
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
        Scraper.TVDBImages.AllSeasonsBanner = New MediaContainers.Image
        Scraper.TVDBImages.AllSeasonsFanart = New MediaContainers.Image
        Scraper.TVDBImages.AllSeasonsLandscape = New MediaContainers.Image
        Scraper.TVDBImages.AllSeasonsPoster = New MediaContainers.Image
        Scraper.TVDBImages.SeasonImageList = New List(Of Scraper.TVDBSeasonImage)
        Scraper.TVDBImages.ShowBanner = New MediaContainers.Image
        Scraper.TVDBImages.ShowCharacterArt = New MediaContainers.Image
        Scraper.TVDBImages.ShowClearArt = New MediaContainers.Image
        Scraper.TVDBImages.ShowClearLogo = New MediaContainers.Image
        Scraper.TVDBImages.ShowFanart = New MediaContainers.Image
        Scraper.TVDBImages.ShowLandscape = New MediaContainers.Image
        Scraper.TVDBImages.ShowPoster = New MediaContainers.Image

        If Me.bwLoadData.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Select Case Me._type
            Case Enums.ImageType_TV.AllSeasonsBanner
                Scraper.TVDBImages.AllSeasonsBanner.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsFanart
                Scraper.TVDBImages.AllSeasonsFanart.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsLandscape
                Scraper.TVDBImages.AllSeasonsLandscape.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsPoster
                Scraper.TVDBImages.AllSeasonsPoster.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.SeasonPoster
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Poster.WebImage = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonBanner
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Banner.WebImage = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonFanart
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Fanart.WebImage = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonLandscape
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Landscape.WebImage = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.ShowBanner
                Scraper.TVDBImages.ShowBanner.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowCharacterArt
                Scraper.TVDBImages.ShowCharacterArt.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowClearArt
                Scraper.TVDBImages.ShowClearArt.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowClearLogo
                Scraper.TVDBImages.ShowClearLogo.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowFanart, Enums.ImageType_TV.EpisodeFanart
                Scraper.TVDBImages.ShowFanart.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowLandscape
                Scraper.TVDBImages.ShowLandscape.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowPoster
                Scraper.TVDBImages.ShowPoster.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.All
                If _withcurrent Then
                    If Master.eSettings.TVShowBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowBannerPath) Then
                        Scraper.TVDBImages.ShowBanner.WebImage.FromFile(Scraper.tmpTVDBShow.Show.ShowBannerPath)
                        Scraper.TVDBImages.ShowBanner.LocalFile = Scraper.tmpTVDBShow.Show.ShowBannerPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowCharacterArtPath) Then
                        Scraper.TVDBImages.ShowCharacterArt.WebImage.FromFile(Scraper.tmpTVDBShow.Show.ShowCharacterArtPath)
                        Scraper.TVDBImages.ShowCharacterArt.LocalFile = Scraper.tmpTVDBShow.Show.ShowCharacterArtPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearArtAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowClearArtPath) Then
                        Scraper.TVDBImages.ShowClearArt.WebImage.FromFile(Scraper.tmpTVDBShow.Show.ShowClearArtPath)
                        Scraper.TVDBImages.ShowClearArt.LocalFile = Scraper.tmpTVDBShow.Show.ShowClearArtPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearLogoAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowClearLogoPath) Then
                        Scraper.TVDBImages.ShowClearLogo.WebImage.FromFile(Scraper.tmpTVDBShow.Show.ShowClearLogoPath)
                        Scraper.TVDBImages.ShowClearLogo.LocalFile = Scraper.tmpTVDBShow.Show.ShowClearLogoPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowFanartPath) Then
                        Scraper.TVDBImages.ShowFanart.WebImage.FromFile(Scraper.tmpTVDBShow.Show.ShowFanartPath)
                        Scraper.TVDBImages.ShowFanart.LocalFile = Scraper.tmpTVDBShow.Show.ShowFanartPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowLandscapePath) Then
                        Scraper.TVDBImages.ShowLandscape.WebImage.FromFile(Scraper.tmpTVDBShow.Show.ShowLandscapePath)
                        Scraper.TVDBImages.ShowLandscape.LocalFile = Scraper.tmpTVDBShow.Show.ShowLandscapePath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowPosterPath) Then
                        Scraper.TVDBImages.ShowPoster.WebImage.FromFile(Scraper.tmpTVDBShow.Show.ShowPosterPath)
                        Scraper.TVDBImages.ShowPoster.LocalFile = Scraper.tmpTVDBShow.Show.ShowPosterPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonBannerPath) Then
                        Scraper.TVDBImages.AllSeasonsBanner.WebImage.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonBannerPath)
                        Scraper.TVDBImages.AllSeasonsBanner.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonBannerPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonFanartPath) Then
                        Scraper.TVDBImages.AllSeasonsFanart.WebImage.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonFanartPath)
                        Scraper.TVDBImages.AllSeasonsFanart.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonFanartPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonLandscapePath) Then
                        Scraper.TVDBImages.AllSeasonsLandscape.WebImage.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonLandscapePath)
                        Scraper.TVDBImages.AllSeasonsLandscape.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonLandscapePath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath) Then
                        Scraper.TVDBImages.AllSeasonsPoster.WebImage.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath)
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
                                If Master.eSettings.TVEpisodePosterAnyEnabled AndAlso Scraper.TVDBImages.ShowPoster.WebImage Is Nothing AndAlso Not String.IsNullOrEmpty(sEpisode.ShowPosterPath) Then
                                    Scraper.TVDBImages.ShowPoster.WebImage.FromFile(sEpisode.ShowPosterPath)
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If

                                If Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso Scraper.TVDBImages.ShowFanart.WebImage.Image Is Nothing AndAlso Not String.IsNullOrEmpty(sEpisode.ShowFanartPath) Then
                                    Scraper.TVDBImages.ShowFanart.WebImage.FromFile(sEpisode.ShowFanartPath)
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
                                        cSI.Banner.WebImage.FromFile(sEpisode.SeasonBannerPath)
                                        cSI.Banner.LocalFile = sEpisode.SeasonBannerPath
                                    End If
                                    If Master.eSettings.TVSeasonFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonFanartPath) Then
                                        cSI.Fanart.WebImage.FromFile(sEpisode.SeasonFanartPath)
                                        cSI.Fanart.LocalFile = sEpisode.SeasonFanartPath
                                    End If
                                    If Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonLandscapePath) Then
                                        cSI.Landscape.WebImage.FromFile(sEpisode.SeasonLandscapePath)
                                        cSI.Landscape.LocalFile = sEpisode.SeasonLandscapePath
                                    End If
                                    If Master.eSettings.TVSeasonPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonPosterPath) Then
                                        cSI.Poster.WebImage.FromFile(sEpisode.SeasonPosterPath)
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
        If _ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.FullAuto Then
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
        Me.pbDelete.Visible = Me.pbCurrent.Image IsNot Nothing AndAlso Me.pbCurrent.Visible
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
                For i As Integer = 0 To Me.pnlImage.Count - 1
                    If Me.pnlImage(i) IsNot Nothing Then
                        If Me.lblImage(i) IsNot Nothing AndAlso Me.pnlImage(i).Contains(Me.lblImage(i)) Then Me.pnlImage(i).Controls.Remove(Me.lblImage(i))
                        If Me.pbImage(i) IsNot Nothing AndAlso Me.pnlImage(i).Contains(Me.pbImage(i)) Then Me.pnlImage(i).Controls.Remove(Me.pbImage(i))
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
            For i As Integer = 0 To Me.pnlImage.Count - 1
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
                        Epi.TVEp.Poster.WebImage.FromWeb(Epi.TVEp.PosterURL)
                        If Epi.TVEp.Poster.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(Epi.TVEp.LocalFile).FullName)
                            Epi.TVEp.Poster.WebImage.Save(Epi.TVEp.LocalFile)
                        End If
                    End If
                Else
                    Epi.TVEp.Poster.WebImage.FromFile(Epi.TVEp.LocalFile)
                End If

                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If

                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Poster/AllSeasons Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.SeasonPosters
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    SeasonPosterList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    SeasonPosterList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            SeasonPosterList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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

        'Season Banner/AllSeasons Banner
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonBanner OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.SeasonBanners
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    SeasonBannerList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    SeasonBannerList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            SeasonBannerList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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

        'Season Landscape/AllSeasons Landscape
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonLandscape OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.SeasonLandscapes
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    SeasonLandscapeList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    SeasonLandscapeList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            SeasonLandscapeList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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

        'Show/AllSeasons Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.Posters
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    GenericPosterList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    GenericPosterList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            GenericPosterList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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

        'Show/AllSeasons Banner
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.ShowBanners
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    ShowBannerList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    ShowBannerList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            ShowBannerList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.ShowCharacterArts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    ShowCharacterArtList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    ShowCharacterArtList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            ShowCharacterArtList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.ShowClearArts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    ShowClearArtList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    ShowClearArtList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            ShowClearArtList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.ShowClearLogos
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    ShowClearLogoList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    ShowClearLogoList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            ShowClearLogoList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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

        'Show/AllSeasons Landscape
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.ShowLandscapes
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    ShowLandscapeList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    ShowLandscapeList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            ShowLandscapeList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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

        'Show/AllSeasons/Season/Episode Fanart
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart OrElse Me._type = Enums.ImageType_TV.SeasonFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart Then
            For Each tImg As MediaContainers.Image In Scraper.tmpTVDBShow.Fanarts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                    GenericFanartList.Add(tImg)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                    GenericFanartList.Add(tImg)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                            GenericFanartList.Add(tImg)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            If tImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(iTag.Path).FullName)
                tImage.Save(iTag.Path)
            End If

            sHTTP = Nothing

            Me.pnlStatus.Visible = False
        End If

    End Sub

    Private Sub GenerateList()
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(658, "TV Show Banner"), .Tag = "showb", .ImageIndex = 0, .SelectedImageIndex = 0})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowCharacterArt) AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1011, "TV Show CharacterArt"), .Tag = "showch", .ImageIndex = 1, .SelectedImageIndex = 1})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearArt) AndAlso Master.eSettings.TVShowClearArtAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1013, "TV Show ClearArt"), .Tag = "showca", .ImageIndex = 2, .SelectedImageIndex = 2})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearLogo) AndAlso Master.eSettings.TVShowClearLogoAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1012, "TV Show ClearLogo"), .Tag = "showcl", .ImageIndex = 3, .SelectedImageIndex = 3})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) AndAlso (Master.eSettings.TVShowFanartAnyEnabled OrElse Master.eSettings.TVEpisodeFanartAnyEnabled) Then Me.tvList.Nodes.Add(New TreeNode With {.Text = If(Me._type = Enums.ImageType_TV.EpisodeFanart, Master.eLang.GetString(688, "Episode Fanart"), Master.eLang.GetString(684, "TV Show Fanart")), .Tag = "showf", .ImageIndex = 4, .SelectedImageIndex = 4})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape) AndAlso Master.eSettings.TVShowLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1010, "TV Show Landscape"), .Tag = "showl", .ImageIndex = 5, .SelectedImageIndex = 5})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(956, "TV Show Poster"), .Tag = "showp", .ImageIndex = 6, .SelectedImageIndex = 6})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1014, "All Seasons Banner"), .Tag = "allb", .ImageIndex = 0, .SelectedImageIndex = 0})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1015, "All Seasons Fanart"), .Tag = "allf", .ImageIndex = 4, .SelectedImageIndex = 4})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape) AndAlso Master.eSettings.TVASLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1016, "All Seasons Landscape"), .Tag = "alll", .ImageIndex = 5, .SelectedImageIndex = 5})
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(735, "All Seasons Poster"), .Tag = "allp", .ImageIndex = 6, .SelectedImageIndex = 6})

        Dim TnS As TreeNode
        If Me._type = Enums.ImageType_TV.All Then
            For Each cSeason As Scraper.TVDBSeasonImage In Scraper.TVDBImages.SeasonImageList.OrderBy(Function(s) s.Season)
                Try
                    TnS = New TreeNode(String.Format(Master.eLang.GetString(726, "Season {0}"), cSeason.Season), 7, 7)
                    If Master.eSettings.TVSeasonBannerAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1017, "Season Banner"), .Tag = String.Concat("b", cSeason.Season), .ImageIndex = 0, .SelectedImageIndex = 0})
                    If Master.eSettings.TVSeasonFanartAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(686, "Season Fanart"), .Tag = String.Concat("f", cSeason.Season), .ImageIndex = 4, .SelectedImageIndex = 4})
                    If Master.eSettings.TVSeasonLandscapeAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1018, "Season Landscape"), .Tag = String.Concat("l", cSeason.Season), .ImageIndex = 5, .SelectedImageIndex = 5})
                    If Master.eSettings.TVSeasonPosterAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(685, "Season Posters"), .Tag = String.Concat("p", cSeason.Season), .ImageIndex = 6, .SelectedImageIndex = 6})
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
        'If iTag.isFanart Then
        DownloadFullsizeImage(iTag, tImages)
        tImage = tImages.Image
        'Else
        '    tImage = DirectCast(sender, PictureBox).Image
        'End If

        ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage)
    End Sub

    Private Sub pbUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbUndo.Click
        If Me.SelSeason = -999 Then
            If SelImgType = Enums.ImageType_TV.ShowBanner Then
                Scraper.TVDBImages.ShowBanner.WebImage = DefaultImages.ShowBanner.WebImage
                Scraper.TVDBImages.ShowBanner.LocalFile = DefaultImages.ShowBanner.LocalFile
                Scraper.TVDBImages.ShowBanner.URL = DefaultImages.ShowBanner.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowBanner.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                Scraper.TVDBImages.ShowCharacterArt.WebImage = DefaultImages.ShowCharacterArt.WebImage
                Scraper.TVDBImages.ShowCharacterArt.LocalFile = DefaultImages.ShowCharacterArt.LocalFile
                Scraper.TVDBImages.ShowCharacterArt.URL = DefaultImages.ShowCharacterArt.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowCharacterArt.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                Scraper.TVDBImages.ShowClearArt.WebImage = DefaultImages.ShowClearArt.WebImage
                Scraper.TVDBImages.ShowClearArt.LocalFile = DefaultImages.ShowClearArt.LocalFile
                Scraper.TVDBImages.ShowClearArt.URL = DefaultImages.ShowClearArt.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearArt.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                Scraper.TVDBImages.ShowClearLogo.WebImage = DefaultImages.ShowClearLogo.WebImage
                Scraper.TVDBImages.ShowClearLogo.LocalFile = DefaultImages.ShowClearLogo.LocalFile
                Scraper.TVDBImages.ShowClearLogo.URL = DefaultImages.ShowClearLogo.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowClearLogo.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                Scraper.TVDBImages.ShowFanart.WebImage = DefaultImages.ShowFanart.WebImage
                Scraper.TVDBImages.ShowFanart.LocalFile = DefaultImages.ShowFanart.LocalFile
                Scraper.TVDBImages.ShowFanart.URL = DefaultImages.ShowFanart.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                Scraper.TVDBImages.ShowPoster.WebImage = DefaultImages.ShowPoster.WebImage
                Scraper.TVDBImages.ShowPoster.LocalFile = DefaultImages.ShowPoster.LocalFile
                Scraper.TVDBImages.ShowPoster.URL = DefaultImages.ShowPoster.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowPoster.WebImage
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                Scraper.TVDBImages.AllSeasonsBanner.WebImage = DefaultImages.AllSeasonsBanner.WebImage
                Scraper.TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                Scraper.TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                Scraper.TVDBImages.AllSeasonsFanart.WebImage = DefaultImages.AllSeasonsFanart.WebImage
                Scraper.TVDBImages.AllSeasonsFanart.LocalFile = DefaultImages.AllSeasonsFanart.LocalFile
                Scraper.TVDBImages.AllSeasonsFanart.URL = DefaultImages.AllSeasonsFanart.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                Scraper.TVDBImages.AllSeasonsLandscape.WebImage = DefaultImages.AllSeasonsLandscape.WebImage
                Scraper.TVDBImages.AllSeasonsLandscape.LocalFile = DefaultImages.AllSeasonsLandscape.LocalFile
                Scraper.TVDBImages.AllSeasonsLandscape.URL = DefaultImages.AllSeasonsLandscape.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsLandscape.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                Scraper.TVDBImages.AllSeasonsPoster.WebImage = DefaultImages.AllSeasonsPoster.WebImage
                Scraper.TVDBImages.AllSeasonsPoster.LocalFile = DefaultImages.AllSeasonsPoster.LocalFile
                Scraper.TVDBImages.AllSeasonsPoster.URL = DefaultImages.AllSeasonsPoster.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsPoster.WebImage
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                Dim dSPost As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost.WebImage

                'Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner

                'Scraper.TVDBImages.SeasonImageList..Image = DefaultImages.AllSeasonsBanner.Image
                'Scraper.TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                'Scraper.TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                'Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image
                'Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.Image

            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                Dim dSFan As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                Dim tSFan As MediaContainers.Image = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tSFan.WebImage = dSFan.WebImage
                tSFan.LocalFile = dSFan.LocalFile
                tSFan.URL = dSFan.URL
                Me.pbCurrent.Image = dSFan.WebImage.Image
                Me.pbCurrent.Tag = dSFan.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                Dim dSPost As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                Dim dSPost As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost.WebImage
            End If
        End If
    End Sub

    Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelect(iIndex, DirectCast(DirectCast(sender, Panel).Tag, ImageTag))
    End Sub

    Private Sub SetImage(ByVal SelTag As ImageTag)
        If SelTag.ImageObj IsNot Nothing Then
            Me.pbCurrent.Image = SelTag.ImageObj.Image
            Me.pbCurrent.Tag = SelTag.ImageObj
        End If

        If Me.SelSeason = -999 Then
            If SelImgType = Enums.ImageType_TV.ShowBanner Then
                Scraper.TVDBImages.ShowBanner.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.ShowBanner.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowBanner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                Scraper.TVDBImages.ShowCharacterArt.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.ShowCharacterArt.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowCharacterArt.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                Scraper.TVDBImages.ShowClearArt.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.ShowClearArt.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowClearArt.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                Scraper.TVDBImages.ShowClearLogo.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.ShowClearLogo.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowClearLogo.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowLandscape Then
                Scraper.TVDBImages.ShowLandscape.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.ShowLandscape.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowLandscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                Scraper.TVDBImages.ShowFanart.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.ShowFanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowFanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                Scraper.TVDBImages.ShowPoster.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.ShowPoster.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowPoster.URL = SelTag.URL
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                Scraper.TVDBImages.AllSeasonsBanner.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsBanner.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsBanner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                Scraper.TVDBImages.AllSeasonsFanart.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsFanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsFanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                Scraper.TVDBImages.AllSeasonsLandscape.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsLandscape.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsLandscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                Scraper.TVDBImages.AllSeasonsPoster.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsPoster.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsPoster.URL = SelTag.URL
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.WebImage = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster.WebImage = SelTag.ImageObj
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
        If e.Node.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Node.Tag.ToString) Then
            Me.pbCurrent.Visible = True
            Me.lblCurrentImage.Visible = True

            'Show Banner
            If e.Node.Tag.ToString = "showb" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowBanner
                If Scraper.TVDBImages.ShowBanner IsNot Nothing AndAlso Scraper.TVDBImages.ShowBanner.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.ShowBanner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In ShowBannerList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'Show Characterart
            ElseIf e.Node.Tag.ToString = "showch" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowCharacterArt
                If Scraper.TVDBImages.ShowCharacterArt IsNot Nothing AndAlso Scraper.TVDBImages.ShowCharacterArt.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowCharacterArt.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In ShowCharacterArtList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'Show ClearArt
            ElseIf e.Node.Tag.ToString = "showca" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearArt
                If Scraper.TVDBImages.ShowClearArt IsNot Nothing AndAlso Scraper.TVDBImages.ShowClearArt.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.ShowClearArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearArt.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In ShowClearArtList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'Show ClearLogo
            ElseIf e.Node.Tag.ToString = "showcl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearLogo
                If Scraper.TVDBImages.ShowClearLogo IsNot Nothing AndAlso Scraper.TVDBImages.ShowClearLogo.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.ShowClearLogo.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowClearLogo.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In ShowClearLogoList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'Show Fanart
            ElseIf e.Node.Tag.ToString = "showf" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowFanart
                If Scraper.TVDBImages.ShowFanart IsNot Nothing AndAlso Scraper.TVDBImages.ShowFanart.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In GenericFanartList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'Show Landscape
            ElseIf e.Node.Tag.ToString = "showl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowLandscape
                If Scraper.TVDBImages.ShowLandscape IsNot Nothing AndAlso Scraper.TVDBImages.ShowLandscape.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.ShowLandscape.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowLandscape.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In ShowLandscapeList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'Show Poster
            ElseIf e.Node.Tag.ToString = "showp" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowPoster
                If Scraper.TVDBImages.ShowPoster IsNot Nothing AndAlso Scraper.TVDBImages.ShowPoster.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.ShowPoster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In GenericPosterList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'AllSeasons Banner
            ElseIf e.Node.Tag.ToString = "allb" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsBanner
                If Scraper.TVDBImages.AllSeasonsBanner IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsBanner.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SeasonBannerList.Where(Function(f) f.Season = 999)
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In ShowBannerList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'AllSeasons Fanart
            ElseIf e.Node.Tag.ToString = "allf" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsFanart
                If Scraper.TVDBImages.AllSeasonsFanart IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsFanart.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In GenericFanartList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'AllSeasons Landscape
            ElseIf e.Node.Tag.ToString = "alll" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsLandscape
                If Scraper.TVDBImages.AllSeasonsLandscape IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsLandscape.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsLandscape.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SeasonLandscapeList.Where(Function(f) f.Season = 999)
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In ShowLandscapeList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next

                'AllSeasons Poster
            ElseIf e.Node.Tag.ToString = "allp" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsPoster
                If Scraper.TVDBImages.AllSeasonsPoster IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsPoster.WebImage IsNot Nothing AndAlso Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SeasonPosterList.Where(Function(f) f.Season = 999)
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In GenericPosterList
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                    End If
                    iCount += 1
                Next
            Else
                Dim tMatch As Match = Regex.Match(e.Node.Tag.ToString, "(?<type>f|p|b|l)(?<num>[0-9]+)")
                If tMatch.Success Then

                    'Season Banner
                    If tMatch.Groups("type").Value = "b" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonBanner
                        Dim tBanner As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If tBanner IsNot Nothing AndAlso tBanner.Banner IsNot Nothing AndAlso tBanner.Banner.WebImage IsNot Nothing Then
                            Me.pbCurrent.Image = tBanner.Banner.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SeasonBannerList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                            End If
                            iCount += 1
                        Next

                        'Season Fanart
                    ElseIf tMatch.Groups("type").Value = "f" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonFanart
                        Dim tFanart As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                        If tFanart IsNot Nothing AndAlso tFanart.Fanart IsNot Nothing AndAlso tFanart.Fanart.WebImage IsNot Nothing AndAlso tFanart.Fanart.WebImage.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tFanart.Fanart.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In GenericFanartList
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                            End If
                            iCount += 1
                        Next

                        'Season Landscape
                    ElseIf tMatch.Groups("type").Value = "l" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonLandscape
                        Dim tLandscape As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If tLandscape IsNot Nothing AndAlso tLandscape.Landscape IsNot Nothing AndAlso tLandscape.Landscape.WebImage IsNot Nothing Then
                            Me.pbCurrent.Image = tLandscape.Landscape.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SeasonLandscapeList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
                            End If
                            iCount += 1
                        Next

                        'Season Poster
                    ElseIf tMatch.Groups("type").Value = "p" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonPoster
                        Dim tPoster As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If tPoster IsNot Nothing AndAlso tPoster.Poster IsNot Nothing AndAlso tPoster.Poster.WebImage IsNot Nothing Then
                            Me.pbCurrent.Image = tPoster.Poster.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SeasonPosterList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, New ImageTag With {.URL = tvImage.URL, .Path = tvImage.LocalFile, .isFanart = False, .ImageObj = tvImage.WebImage})
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