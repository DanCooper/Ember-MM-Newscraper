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

Public Class dlgImgSelectTV

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Friend WithEvents bwDownloadFanart As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadData As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadImages As New System.ComponentModel.BackgroundWorker

    Private alContainer As New MediaContainers.ImagesContainer_TV
    Private TVDBImages As New TVImages
    Private tmpTVDBShow As New TVDBShow
    'Private tmpDBTV As New Structures.DBTV

    Private DefaultImages As New TVImages
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

    Private _efList As New List(Of String)
    Private _etList As New List(Of String)

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Results As TVImages
        Get
            Return TVDBImages
        End Get
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

        Me.bwLoadImages.ReportProgress(TVDBImages.SeasonImageList.Count + tmpTVDBShow.Episodes.Count + iProgress, "defaults")

        'AllSeason Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled AndAlso TVDBImages.AllSeasonsBanner.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASBanner(SeasonBannerList, ShowBannerList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.AllSeasonsBanner.WebImage = defImg.WebImage
                TVDBImages.AllSeasonsBanner.LocalFile = defImg.LocalFile
                TVDBImages.AllSeasonsBanner.LocalThumb = defImg.LocalThumb
                TVDBImages.AllSeasonsBanner.ThumbURL = defImg.ThumbURL
                TVDBImages.AllSeasonsBanner.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(1, "progress")

        'AllSeason Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled AndAlso TVDBImages.AllSeasonsFanart.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASFanart(GenericFanartList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.AllSeasonsFanart.WebImage = defImg.WebImage
                TVDBImages.AllSeasonsFanart.LocalFile = defImg.LocalFile
                TVDBImages.AllSeasonsFanart.LocalThumb = defImg.LocalThumb
                TVDBImages.AllSeasonsFanart.ThumbURL = defImg.ThumbURL
                TVDBImages.AllSeasonsFanart.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(2, "progress")

        'AllSeason Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape) AndAlso Master.eSettings.TVASLandscapeAnyEnabled AndAlso TVDBImages.AllSeasonsLandscape.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASLandscape(SeasonLandscapeList, ShowLandscapeList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.AllSeasonsLandscape.WebImage = defImg.WebImage
                TVDBImages.AllSeasonsLandscape.LocalFile = defImg.LocalFile
                TVDBImages.AllSeasonsLandscape.LocalThumb = defImg.LocalThumb
                TVDBImages.AllSeasonsLandscape.ThumbURL = defImg.ThumbURL
                TVDBImages.AllSeasonsLandscape.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(3, "progress")

        'AllSeason Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled AndAlso TVDBImages.AllSeasonsPoster.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASPoster(SeasonPosterList, GenericPosterList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.AllSeasonsPoster.WebImage = defImg.WebImage
                TVDBImages.AllSeasonsPoster.LocalFile = defImg.LocalFile
                TVDBImages.AllSeasonsPoster.LocalThumb = defImg.LocalThumb
                TVDBImages.AllSeasonsPoster.ThumbURL = defImg.ThumbURL
                TVDBImages.AllSeasonsPoster.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(4, "progress")

        'Show Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled AndAlso TVDBImages.ShowBanner.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowBanner(ShowBannerList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.ShowBanner.WebImage = defImg.WebImage
                TVDBImages.ShowBanner.LocalFile = defImg.LocalFile
                TVDBImages.ShowBanner.LocalThumb = defImg.LocalThumb
                TVDBImages.ShowBanner.ThumbURL = defImg.ThumbURL
                TVDBImages.ShowBanner.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(5, "progress")

        'Show CharacterArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowCharacterArt) AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso TVDBImages.ShowCharacterArt.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowCharacterArt(ShowCharacterArtList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.ShowCharacterArt.WebImage = defImg.WebImage
                TVDBImages.ShowCharacterArt.LocalFile = defImg.LocalFile
                TVDBImages.ShowCharacterArt.LocalThumb = defImg.LocalThumb
                TVDBImages.ShowCharacterArt.ThumbURL = defImg.ThumbURL
                TVDBImages.ShowCharacterArt.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(6, "progress")

        'Show ClearArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearArt) AndAlso Master.eSettings.TVShowClearArtAnyEnabled AndAlso TVDBImages.ShowClearArt.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowClearArt(ShowClearArtList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.ShowClearArt.WebImage = defImg.WebImage
                TVDBImages.ShowClearArt.LocalFile = defImg.LocalFile
                TVDBImages.ShowClearArt.LocalThumb = defImg.LocalThumb
                TVDBImages.ShowClearArt.ThumbURL = defImg.ThumbURL
                TVDBImages.ShowClearArt.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(7, "progress")

        'Show ClearLogo
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearLogo) AndAlso Master.eSettings.TVShowClearLogoAnyEnabled AndAlso TVDBImages.ShowClearLogo.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowClearLogo(ShowClearLogoList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.ShowClearLogo.WebImage = defImg.WebImage
                TVDBImages.ShowClearLogo.LocalFile = defImg.LocalFile
                TVDBImages.ShowClearLogo.LocalThumb = defImg.LocalThumb
                TVDBImages.ShowClearLogo.ThumbURL = defImg.ThumbURL
                TVDBImages.ShowClearLogo.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(8, "progress")

        'Show Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) AndAlso TVDBImages.ShowFanart.WebImage.Image Is Nothing Then 'TODO: add *FanartEnabled check
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowFanart(GenericFanartList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.ShowFanart.WebImage = defImg.WebImage
                TVDBImages.ShowFanart.LocalFile = defImg.LocalFile
                TVDBImages.ShowFanart.LocalThumb = defImg.LocalThumb
                TVDBImages.ShowFanart.ThumbURL = defImg.ThumbURL
                TVDBImages.ShowFanart.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(9, "progress")

        'Show Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape) AndAlso Master.eSettings.TVShowLandscapeAnyEnabled AndAlso TVDBImages.ShowLandscape.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowLandscape(ShowLandscapeList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.ShowLandscape.WebImage = defImg.WebImage
                TVDBImages.ShowLandscape.LocalFile = defImg.LocalFile
                TVDBImages.ShowLandscape.LocalThumb = defImg.LocalThumb
                TVDBImages.ShowLandscape.ThumbURL = defImg.ThumbURL
                TVDBImages.ShowLandscape.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(10, "progress")

        'Show Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled AndAlso TVDBImages.ShowPoster.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowPoster(GenericPosterList, defImg)

            If defImg IsNot Nothing Then
                TVDBImages.ShowPoster.WebImage = defImg.WebImage
                TVDBImages.ShowPoster.LocalFile = defImg.LocalFile
                TVDBImages.ShowPoster.LocalThumb = defImg.LocalThumb
                TVDBImages.ShowPoster.ThumbURL = defImg.ThumbURL
                TVDBImages.ShowPoster.URL = defImg.URL
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(11, "progress")

        'Season Banner/Fanart/Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster OrElse Me._type = Enums.ImageType_TV.SeasonBanner OrElse Me._type = Enums.ImageType_TV.SeasonFanart Then
            For Each cSeason As TVDBSeasonImage In TVDBImages.SeasonImageList
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
            For Each Episode As Structures.DBTV In tmpTVDBShow.Episodes

                'Fanart
                If Master.eSettings.TVEpisodeFanartAnyEnabled Then
                    If Not String.IsNullOrEmpty(Episode.EpFanartPath) Then
                        Episode.TVEp.Fanart.FromFile(Episode.EpFanartPath)
                    ElseIf TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                        Episode.TVEp.Fanart = TVDBImages.ShowFanart.WebImage
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

        DefaultImages = TVDBImages 'TVDBImages.Clone() 'TODO: fix the clone function

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
                If Not String.IsNullOrEmpty(TVDBImages.ShowBanner.LocalFile) AndAlso File.Exists(TVDBImages.ShowBanner.LocalFile) Then
                    TVDBImages.ShowBanner.WebImage.FromFile(TVDBImages.ShowBanner.LocalFile)
                ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowBanner.LocalFile) Then
                    TVDBImages.ShowBanner.WebImage.Clear()
                    TVDBImages.ShowBanner.WebImage.FromWeb(TVDBImages.ShowBanner.URL)
                    If TVDBImages.ShowBanner.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowBanner.LocalFile).FullName)
                        TVDBImages.ShowBanner.WebImage.Save(TVDBImages.ShowBanner.LocalFile)
                    End If
                End If
            End If

            'Show CharacterArt
            If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.ShowCharacterArt.LocalFile) AndAlso File.Exists(TVDBImages.ShowCharacterArt.LocalFile) Then
                    TVDBImages.ShowCharacterArt.WebImage.FromFile(TVDBImages.ShowCharacterArt.LocalFile)
                ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowCharacterArt.LocalFile) Then
                    TVDBImages.ShowCharacterArt.WebImage.Clear()
                    TVDBImages.ShowCharacterArt.WebImage.FromWeb(TVDBImages.ShowCharacterArt.URL)
                    If TVDBImages.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowCharacterArt.LocalFile).FullName)
                        TVDBImages.ShowCharacterArt.WebImage.Save(TVDBImages.ShowCharacterArt.LocalFile)
                    End If
                End If
            End If

            'Show ClearArt
            If Master.eSettings.TVShowClearArtAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.ShowClearArt.LocalFile) AndAlso File.Exists(TVDBImages.ShowClearArt.LocalFile) Then
                    TVDBImages.ShowClearArt.WebImage.FromFile(TVDBImages.ShowClearArt.LocalFile)
                    Master.currShow.ShowClearArtPath = TVDBImages.ShowClearArt.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowClearArt.LocalFile) Then
                    TVDBImages.ShowClearArt.WebImage.Clear()
                    TVDBImages.ShowClearArt.WebImage.FromWeb(TVDBImages.ShowClearArt.URL)
                    If TVDBImages.ShowClearArt.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowClearArt.LocalFile).FullName)
                        TVDBImages.ShowClearArt.WebImage.Save(TVDBImages.ShowClearArt.LocalFile)
                        Master.currShow.ShowClearArtPath = TVDBImages.ShowClearArt.LocalFile
                    End If
                End If
            End If

            'Show ClearLogo
            If Master.eSettings.TVShowClearLogoAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.ShowClearLogo.LocalFile) AndAlso File.Exists(TVDBImages.ShowClearLogo.LocalFile) Then
                    TVDBImages.ShowClearLogo.WebImage.FromFile(TVDBImages.ShowClearLogo.LocalFile)
                    Master.currShow.ShowClearLogoPath = TVDBImages.ShowClearLogo.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowClearLogo.LocalFile) Then
                    TVDBImages.ShowClearLogo.WebImage.Clear()
                    TVDBImages.ShowClearLogo.WebImage.FromWeb(TVDBImages.ShowClearLogo.URL)
                    If TVDBImages.ShowClearLogo.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowClearLogo.LocalFile).FullName)
                        TVDBImages.ShowClearLogo.WebImage.Save(TVDBImages.ShowClearLogo.LocalFile)
                        Master.currShow.ShowClearLogoPath = TVDBImages.ShowClearLogo.LocalFile
                    End If
                End If
            End If

            'Show Fanart
            If Master.eSettings.TVShowFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(TVDBImages.ShowFanart.LocalFile) Then
                    TVDBImages.ShowFanart.WebImage.FromFile(TVDBImages.ShowFanart.LocalFile)
                    Master.currShow.ShowFanartPath = TVDBImages.ShowFanart.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowFanart.LocalFile) Then
                    TVDBImages.ShowFanart.WebImage.Clear()
                    TVDBImages.ShowFanart.WebImage.FromWeb(TVDBImages.ShowFanart.URL)
                    If TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowFanart.LocalFile).FullName)
                        TVDBImages.ShowFanart.WebImage.Save(TVDBImages.ShowFanart.LocalFile)
                        Master.currShow.ShowFanartPath = TVDBImages.ShowFanart.LocalFile
                    End If
                End If
            End If

            'Show Landscape
            If Master.eSettings.TVShowLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.ShowLandscape.LocalFile) AndAlso File.Exists(TVDBImages.ShowLandscape.LocalFile) Then
                    TVDBImages.ShowLandscape.WebImage.FromFile(TVDBImages.ShowLandscape.LocalFile)
                    Master.currShow.ShowLandscapePath = TVDBImages.ShowLandscape.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowLandscape.LocalFile) Then
                    TVDBImages.ShowLandscape.WebImage.Clear()
                    TVDBImages.ShowLandscape.WebImage.FromWeb(TVDBImages.ShowLandscape.URL)
                    If TVDBImages.ShowLandscape.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowLandscape.LocalFile).FullName)
                        TVDBImages.ShowLandscape.WebImage.Save(TVDBImages.ShowLandscape.LocalFile)
                        Master.currShow.ShowLandscapePath = TVDBImages.ShowLandscape.LocalFile
                    End If
                End If
            End If

            'Show Poster
            If Master.eSettings.TVShowPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.ShowPoster.LocalFile) AndAlso File.Exists(TVDBImages.ShowPoster.LocalFile) Then
                    TVDBImages.ShowPoster.WebImage.FromFile(TVDBImages.ShowPoster.LocalFile)
                    Master.currShow.ShowPosterPath = TVDBImages.ShowPoster.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowPoster.LocalFile) Then
                    TVDBImages.ShowPoster.WebImage.Clear()
                    TVDBImages.ShowPoster.WebImage.FromWeb(TVDBImages.ShowPoster.URL)
                    If TVDBImages.ShowPoster.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowPoster.LocalFile).FullName)
                        TVDBImages.ShowPoster.WebImage.Save(TVDBImages.ShowPoster.LocalFile)
                        Master.currShow.ShowPosterPath = TVDBImages.ShowPoster.LocalFile
                    End If
                End If
            End If

            'AS Banner
            If Master.eSettings.TVASBannerAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsBanner.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsBanner.LocalFile) Then
                    TVDBImages.AllSeasonsBanner.WebImage.FromFile(TVDBImages.AllSeasonsBanner.LocalFile)
                    Master.currShow.SeasonBannerPath = TVDBImages.AllSeasonsBanner.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsBanner.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsBanner.LocalFile) Then
                    TVDBImages.AllSeasonsBanner.WebImage.Clear()
                    TVDBImages.AllSeasonsBanner.WebImage.FromWeb(TVDBImages.AllSeasonsBanner.URL)
                    If TVDBImages.AllSeasonsBanner.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsBanner.LocalFile).FullName)
                        TVDBImages.AllSeasonsBanner.WebImage.Save(TVDBImages.AllSeasonsBanner.LocalFile)
                        Master.currShow.SeasonBannerPath = TVDBImages.AllSeasonsBanner.LocalFile
                    End If
                End If
            End If

            'AS Fanart
            If Master.eSettings.TVASFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsFanart.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsFanart.LocalFile) Then
                    TVDBImages.AllSeasonsFanart.WebImage.FromFile(TVDBImages.AllSeasonsFanart.LocalFile)
                    Master.currShow.SeasonFanartPath = TVDBImages.AllSeasonsFanart.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsFanart.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsFanart.LocalFile) Then
                    TVDBImages.AllSeasonsFanart.WebImage.Clear()
                    TVDBImages.AllSeasonsFanart.WebImage.FromWeb(TVDBImages.AllSeasonsFanart.URL)
                    If TVDBImages.AllSeasonsFanart.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsFanart.LocalFile).FullName)
                        TVDBImages.AllSeasonsFanart.WebImage.Save(TVDBImages.AllSeasonsFanart.LocalFile)
                        Master.currShow.SeasonFanartPath = TVDBImages.AllSeasonsFanart.LocalFile
                    End If
                End If
            End If

            'AS Landscape
            If Master.eSettings.TVASLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsLandscape.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsLandscape.LocalFile) Then
                    TVDBImages.AllSeasonsLandscape.WebImage.FromFile(TVDBImages.AllSeasonsLandscape.LocalFile)
                    Master.currShow.SeasonLandscapePath = TVDBImages.AllSeasonsLandscape.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsLandscape.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsLandscape.LocalFile) Then
                    TVDBImages.AllSeasonsLandscape.WebImage.Clear()
                    TVDBImages.AllSeasonsLandscape.WebImage.FromWeb(TVDBImages.AllSeasonsLandscape.URL)
                    If TVDBImages.AllSeasonsLandscape.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsLandscape.LocalFile).FullName)
                        TVDBImages.AllSeasonsLandscape.WebImage.Save(TVDBImages.AllSeasonsLandscape.LocalFile)
                        Master.currShow.SeasonLandscapePath = TVDBImages.AllSeasonsLandscape.LocalFile
                    End If
                End If
            End If

            'AS Poster
            If Master.eSettings.TVASPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsPoster.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsPoster.LocalFile) Then
                    TVDBImages.AllSeasonsPoster.WebImage.FromFile(TVDBImages.AllSeasonsPoster.LocalFile)
                    Master.currShow.SeasonPosterPath = TVDBImages.AllSeasonsPoster.LocalFile
                ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsPoster.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsPoster.LocalFile) Then
                    TVDBImages.AllSeasonsPoster.WebImage.Clear()
                    TVDBImages.AllSeasonsPoster.WebImage.FromWeb(TVDBImages.AllSeasonsPoster.URL)
                    If TVDBImages.AllSeasonsPoster.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsPoster.LocalFile).FullName)
                        TVDBImages.AllSeasonsPoster.WebImage.Save(TVDBImages.AllSeasonsPoster.LocalFile)
                        Master.currShow.SeasonPosterPath = TVDBImages.AllSeasonsPoster.LocalFile
                    End If
                End If
            End If

        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsBanner.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsBanner.LocalFile) Then
                TVDBImages.AllSeasonsBanner.WebImage.FromFile(TVDBImages.AllSeasonsBanner.LocalFile)
                Me.pbCurrent.Image = TVDBImages.AllSeasonsBanner.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsBanner.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsBanner.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsBanner.LocalFile) Then
                TVDBImages.AllSeasonsBanner.WebImage.Clear()
                TVDBImages.AllSeasonsBanner.WebImage.FromWeb(TVDBImages.AllSeasonsBanner.URL)
                If TVDBImages.AllSeasonsBanner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsBanner.LocalFile).FullName)
                    TVDBImages.AllSeasonsBanner.WebImage.Save(TVDBImages.AllSeasonsBanner.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsBanner.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.AllSeasonsBanner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsFanart.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsFanart.LocalFile) Then
                TVDBImages.AllSeasonsFanart.WebImage.FromFile(TVDBImages.AllSeasonsFanart.LocalFile)
                Me.pbCurrent.Image = TVDBImages.AllSeasonsFanart.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsFanart.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsFanart.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsFanart.LocalFile) Then
                TVDBImages.AllSeasonsFanart.WebImage.Clear()
                TVDBImages.AllSeasonsFanart.WebImage.FromWeb(TVDBImages.AllSeasonsFanart.URL)
                If TVDBImages.AllSeasonsFanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsFanart.LocalFile).FullName)
                    TVDBImages.AllSeasonsFanart.WebImage.Save(TVDBImages.AllSeasonsFanart.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsFanart.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.AllSeasonsFanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsLandscape.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsLandscape.LocalFile) Then
                TVDBImages.AllSeasonsLandscape.WebImage.FromFile(TVDBImages.AllSeasonsLandscape.LocalFile)
                Me.pbCurrent.Image = TVDBImages.AllSeasonsLandscape.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsLandscape.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsLandscape.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsLandscape.LocalFile) Then
                TVDBImages.AllSeasonsLandscape.WebImage.Clear()
                TVDBImages.AllSeasonsLandscape.WebImage.FromWeb(TVDBImages.AllSeasonsLandscape.URL)
                If TVDBImages.AllSeasonsLandscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsLandscape.LocalFile).FullName)
                    TVDBImages.AllSeasonsLandscape.WebImage.Save(TVDBImages.AllSeasonsLandscape.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsLandscape.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.AllSeasonsLandscape.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.AllSeasonsPoster.LocalFile) AndAlso File.Exists(TVDBImages.AllSeasonsPoster.LocalFile) Then
                TVDBImages.AllSeasonsPoster.WebImage.FromFile(TVDBImages.AllSeasonsPoster.LocalFile)
                Me.pbCurrent.Image = TVDBImages.AllSeasonsPoster.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsPoster.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.AllSeasonsPoster.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.AllSeasonsPoster.LocalFile) Then
                TVDBImages.AllSeasonsPoster.WebImage.Clear()
                TVDBImages.AllSeasonsPoster.WebImage.FromWeb(TVDBImages.AllSeasonsPoster.URL)
                If TVDBImages.AllSeasonsPoster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.AllSeasonsPoster.LocalFile).FullName)
                    TVDBImages.AllSeasonsPoster.WebImage.Save(TVDBImages.AllSeasonsPoster.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsPoster.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.AllSeasonsPoster.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Banner.LocalFile) AndAlso File.Exists(TVDBImages.SeasonImageList(0).Banner.LocalFile) Then
                TVDBImages.SeasonImageList(0).Banner.WebImage.FromFile(TVDBImages.SeasonImageList(0).Banner.LocalFile)
                Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Banner.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Banner.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Banner.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Banner.LocalFile) Then
                TVDBImages.SeasonImageList(0).Banner.WebImage.Clear()
                TVDBImages.SeasonImageList(0).Banner.WebImage.FromWeb(TVDBImages.SeasonImageList(0).Banner.URL)
                If TVDBImages.SeasonImageList(0).Banner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.SeasonImageList(0).Banner.LocalFile).FullName)
                    TVDBImages.SeasonImageList(0).Banner.WebImage.Save(TVDBImages.SeasonImageList(0).Banner.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Banner.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Banner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Fanart.LocalFile) AndAlso File.Exists(TVDBImages.SeasonImageList(0).Fanart.LocalFile) Then
                TVDBImages.SeasonImageList(0).Fanart.WebImage.FromFile(TVDBImages.SeasonImageList(0).Fanart.LocalFile)
                Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Fanart.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Fanart.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Fanart.LocalFile) Then
                TVDBImages.SeasonImageList(0).Fanart.WebImage.Clear()
                TVDBImages.SeasonImageList(0).Fanart.WebImage.FromWeb(TVDBImages.SeasonImageList(0).Fanart.URL)
                If TVDBImages.SeasonImageList(0).Fanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.SeasonImageList(0).Fanart.LocalFile).FullName)
                    TVDBImages.SeasonImageList(0).Fanart.WebImage.Save(TVDBImages.SeasonImageList(0).Fanart.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Fanart.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Fanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Landscape.LocalFile) AndAlso File.Exists(TVDBImages.SeasonImageList(0).Landscape.LocalFile) Then
                TVDBImages.SeasonImageList(0).Landscape.WebImage.FromFile(TVDBImages.SeasonImageList(0).Landscape.LocalFile)
                Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Landscape.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Landscape.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Landscape.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Landscape.LocalFile) Then
                TVDBImages.SeasonImageList(0).Landscape.WebImage.Clear()
                TVDBImages.SeasonImageList(0).Landscape.WebImage.FromWeb(TVDBImages.SeasonImageList(0).Landscape.URL)
                If TVDBImages.SeasonImageList(0).Landscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.SeasonImageList(0).Landscape.LocalFile).FullName)
                    TVDBImages.SeasonImageList(0).Landscape.WebImage.Save(TVDBImages.SeasonImageList(0).Landscape.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Landscape.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Landscape.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Poster.LocalFile) AndAlso File.Exists(TVDBImages.SeasonImageList(0).Poster.LocalFile) Then
                TVDBImages.SeasonImageList(0).Poster.WebImage.FromFile(TVDBImages.SeasonImageList(0).Poster.LocalFile)
                Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Poster.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Poster.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Poster.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.SeasonImageList(0).Poster.LocalFile) Then
                TVDBImages.SeasonImageList(0).Poster.WebImage.Clear()
                TVDBImages.SeasonImageList(0).Poster.WebImage.FromWeb(TVDBImages.SeasonImageList(0).Poster.URL)
                If TVDBImages.SeasonImageList(0).Poster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.SeasonImageList(0).Poster.LocalFile).FullName)
                    TVDBImages.SeasonImageList(0).Poster.WebImage.Save(TVDBImages.SeasonImageList(0).Poster.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.SeasonImageList(0).Poster.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.SeasonImageList(0).Poster.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.ShowBanner.LocalFile) AndAlso File.Exists(TVDBImages.ShowBanner.LocalFile) Then
                TVDBImages.ShowBanner.WebImage.FromFile(TVDBImages.ShowBanner.LocalFile)
                Me.pbCurrent.Image = TVDBImages.ShowBanner.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowBanner.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowBanner.LocalFile) Then
                TVDBImages.ShowBanner.WebImage.Clear()
                TVDBImages.ShowBanner.WebImage.FromWeb(TVDBImages.ShowBanner.URL)
                If TVDBImages.ShowBanner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowBanner.LocalFile).FullName)
                    TVDBImages.ShowBanner.WebImage.Save(TVDBImages.ShowBanner.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.ShowBanner.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.ShowBanner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowCharacterArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.ShowCharacterArt.LocalFile) AndAlso File.Exists(TVDBImages.ShowCharacterArt.LocalFile) Then
                TVDBImages.ShowCharacterArt.WebImage.FromFile(TVDBImages.ShowCharacterArt.LocalFile)
                Me.pbCurrent.Image = TVDBImages.ShowCharacterArt.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowCharacterArt.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowCharacterArt.LocalFile) Then
                TVDBImages.ShowCharacterArt.WebImage.Clear()
                TVDBImages.ShowCharacterArt.WebImage.FromWeb(TVDBImages.ShowCharacterArt.URL)
                If TVDBImages.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowCharacterArt.LocalFile).FullName)
                    TVDBImages.ShowCharacterArt.WebImage.Save(TVDBImages.ShowCharacterArt.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.ShowCharacterArt.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.ShowCharacterArt.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.ShowClearArt.LocalFile) AndAlso File.Exists(TVDBImages.ShowClearArt.LocalFile) Then
                TVDBImages.ShowClearArt.WebImage.FromFile(TVDBImages.ShowClearArt.LocalFile)
                Me.pbCurrent.Image = TVDBImages.ShowClearArt.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowClearArt.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowClearArt.LocalFile) Then
                TVDBImages.ShowClearArt.WebImage.Clear()
                TVDBImages.ShowClearArt.WebImage.FromWeb(TVDBImages.ShowClearArt.URL)
                If TVDBImages.ShowClearArt.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowClearArt.LocalFile).FullName)
                    TVDBImages.ShowClearArt.WebImage.Save(TVDBImages.ShowClearArt.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.ShowClearArt.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.ShowClearArt.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearLogo Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.ShowClearLogo.LocalFile) AndAlso File.Exists(TVDBImages.ShowClearLogo.LocalFile) Then
                TVDBImages.ShowClearLogo.WebImage.FromFile(TVDBImages.ShowClearLogo.LocalFile)
                Me.pbCurrent.Image = TVDBImages.ShowClearLogo.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowClearLogo.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowClearLogo.LocalFile) Then
                TVDBImages.ShowClearLogo.WebImage.Clear()
                TVDBImages.ShowClearLogo.WebImage.FromWeb(TVDBImages.ShowClearLogo.URL)
                If TVDBImages.ShowClearLogo.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowClearLogo.LocalFile).FullName)
                    TVDBImages.ShowClearLogo.WebImage.Save(TVDBImages.ShowClearLogo.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.ShowClearLogo.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.ShowClearLogo.WebImage
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.ShowFanart.LocalFile) AndAlso File.Exists(TVDBImages.ShowFanart.LocalFile) Then
                TVDBImages.ShowFanart.WebImage.FromFile(TVDBImages.ShowFanart.LocalFile)
                Me.pbCurrent.Image = TVDBImages.ShowFanart.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowFanart.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowFanart.LocalFile) Then
                TVDBImages.ShowFanart.WebImage.Clear()
                TVDBImages.ShowFanart.WebImage.FromWeb(TVDBImages.ShowFanart.URL)
                If TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowFanart.LocalFile).FullName)
                    TVDBImages.ShowFanart.WebImage.Save(TVDBImages.ShowFanart.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.ShowFanart.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.ShowFanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.ShowLandscape.LocalFile) AndAlso File.Exists(TVDBImages.ShowLandscape.LocalFile) Then
                TVDBImages.ShowLandscape.WebImage.FromFile(TVDBImages.ShowLandscape.LocalFile)
                Me.pbCurrent.Image = TVDBImages.ShowLandscape.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowLandscape.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowLandscape.LocalFile) Then
                TVDBImages.ShowLandscape.WebImage.Clear()
                TVDBImages.ShowLandscape.WebImage.FromWeb(TVDBImages.ShowLandscape.URL)
                If TVDBImages.ShowLandscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowLandscape.LocalFile).FullName)
                    TVDBImages.ShowLandscape.WebImage.Save(TVDBImages.ShowLandscape.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.ShowLandscape.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.ShowLandscape.WebImage
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowPoster OrElse Me._type = Enums.ImageType_TV.EpisodePoster) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(TVDBImages.ShowPoster.LocalFile) AndAlso File.Exists(TVDBImages.ShowPoster.LocalFile) Then
                TVDBImages.ShowPoster.WebImage.FromFile(TVDBImages.ShowPoster.LocalFile)
                Me.pbCurrent.Image = TVDBImages.ShowPoster.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowPoster.WebImage
            ElseIf Not String.IsNullOrEmpty(TVDBImages.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(TVDBImages.ShowPoster.LocalFile) Then
                TVDBImages.ShowPoster.WebImage.Clear()
                TVDBImages.ShowPoster.WebImage.FromWeb(TVDBImages.ShowPoster.URL)
                If TVDBImages.ShowPoster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(TVDBImages.ShowPoster.LocalFile).FullName)
                    TVDBImages.ShowPoster.WebImage.Save(TVDBImages.ShowPoster.LocalFile)
                    Me.pbCurrent.Image = TVDBImages.ShowPoster.WebImage.Image
                    Me.pbCurrent.Tag = TVDBImages.ShowPoster.WebImage
                End If
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    Private Sub bwLoadData_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadData.DoWork
        Dim cSI As TVDBSeasonImage
        Dim iProgress As Integer = 1
        Dim iSeason As Integer = -1

        Me.bwLoadData.ReportProgress(tmpTVDBShow.Episodes.Count, "current")

        'initialize the struct
        TVDBImages.AllSeasonsBanner = New MediaContainers.Image
        TVDBImages.AllSeasonsFanart = New MediaContainers.Image
        TVDBImages.AllSeasonsLandscape = New MediaContainers.Image
        TVDBImages.AllSeasonsPoster = New MediaContainers.Image
        TVDBImages.SeasonImageList = New List(Of TVDBSeasonImage)
        TVDBImages.ShowBanner = New MediaContainers.Image
        TVDBImages.ShowCharacterArt = New MediaContainers.Image
        TVDBImages.ShowClearArt = New MediaContainers.Image
        TVDBImages.ShowClearLogo = New MediaContainers.Image
        TVDBImages.ShowFanart = New MediaContainers.Image
        TVDBImages.ShowLandscape = New MediaContainers.Image
        TVDBImages.ShowPoster = New MediaContainers.Image

        If Me.bwLoadData.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Select Case Me._type
            Case Enums.ImageType_TV.AllSeasonsBanner
                TVDBImages.AllSeasonsBanner.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsFanart
                TVDBImages.AllSeasonsFanart.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsLandscape
                TVDBImages.AllSeasonsLandscape.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.AllSeasonsPoster
                TVDBImages.AllSeasonsPoster.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.SeasonPoster
                cSI = New TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Poster.WebImage = CType(Me.pbCurrent.Tag, Images)
                TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonBanner
                cSI = New TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Banner.WebImage = CType(Me.pbCurrent.Tag, Images)
                TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonFanart
                cSI = New TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Fanart.WebImage = CType(Me.pbCurrent.Tag, Images)
                TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.SeasonLandscape
                cSI = New TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Landscape.WebImage = CType(Me.pbCurrent.Tag, Images)
                TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.ImageType_TV.ShowBanner
                TVDBImages.ShowBanner.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowCharacterArt
                TVDBImages.ShowCharacterArt.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowClearArt
                TVDBImages.ShowClearArt.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowClearLogo
                TVDBImages.ShowClearLogo.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowFanart, Enums.ImageType_TV.EpisodeFanart
                TVDBImages.ShowFanart.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowLandscape
                TVDBImages.ShowLandscape.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.ShowPoster
                TVDBImages.ShowPoster.WebImage = CType(Me.pbCurrent.Tag, Images)
            Case Enums.ImageType_TV.All
                If _withcurrent Then
                    If Master.eSettings.TVShowBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.Show.ShowBannerPath) Then
                        TVDBImages.ShowBanner.WebImage.FromFile(tmpTVDBShow.Show.ShowBannerPath)
                        TVDBImages.ShowBanner.LocalFile = tmpTVDBShow.Show.ShowBannerPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.Show.ShowCharacterArtPath) Then
                        TVDBImages.ShowCharacterArt.WebImage.FromFile(tmpTVDBShow.Show.ShowCharacterArtPath)
                        TVDBImages.ShowCharacterArt.LocalFile = tmpTVDBShow.Show.ShowCharacterArtPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearArtAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.Show.ShowClearArtPath) Then
                        TVDBImages.ShowClearArt.WebImage.FromFile(tmpTVDBShow.Show.ShowClearArtPath)
                        TVDBImages.ShowClearArt.LocalFile = tmpTVDBShow.Show.ShowClearArtPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearLogoAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.Show.ShowClearLogoPath) Then
                        TVDBImages.ShowClearLogo.WebImage.FromFile(tmpTVDBShow.Show.ShowClearLogoPath)
                        TVDBImages.ShowClearLogo.LocalFile = tmpTVDBShow.Show.ShowClearLogoPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.Show.ShowFanartPath) Then
                        TVDBImages.ShowFanart.WebImage.FromFile(tmpTVDBShow.Show.ShowFanartPath)
                        TVDBImages.ShowFanart.LocalFile = tmpTVDBShow.Show.ShowFanartPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.Show.ShowLandscapePath) Then
                        TVDBImages.ShowLandscape.WebImage.FromFile(tmpTVDBShow.Show.ShowLandscapePath)
                        TVDBImages.ShowLandscape.LocalFile = tmpTVDBShow.Show.ShowLandscapePath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.Show.ShowPosterPath) Then
                        TVDBImages.ShowPoster.WebImage.FromFile(tmpTVDBShow.Show.ShowPosterPath)
                        TVDBImages.ShowPoster.LocalFile = tmpTVDBShow.Show.ShowPosterPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.AllSeason.SeasonBannerPath) Then
                        TVDBImages.AllSeasonsBanner.WebImage.FromFile(tmpTVDBShow.AllSeason.SeasonBannerPath)
                        TVDBImages.AllSeasonsBanner.LocalFile = tmpTVDBShow.AllSeason.SeasonBannerPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.AllSeason.SeasonFanartPath) Then
                        TVDBImages.AllSeasonsFanart.WebImage.FromFile(tmpTVDBShow.AllSeason.SeasonFanartPath)
                        TVDBImages.AllSeasonsFanart.LocalFile = tmpTVDBShow.AllSeason.SeasonFanartPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.AllSeason.SeasonLandscapePath) Then
                        TVDBImages.AllSeasonsLandscape.WebImage.FromFile(tmpTVDBShow.AllSeason.SeasonLandscapePath)
                        TVDBImages.AllSeasonsLandscape.LocalFile = tmpTVDBShow.AllSeason.SeasonLandscapePath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpTVDBShow.AllSeason.SeasonPosterPath) Then
                        TVDBImages.AllSeasonsPoster.WebImage.FromFile(tmpTVDBShow.AllSeason.SeasonPosterPath)
                        TVDBImages.AllSeasonsPoster.LocalFile = tmpTVDBShow.AllSeason.SeasonPosterPath
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    For Each sEpisode As Structures.DBTV In tmpTVDBShow.Episodes
                        Try
                            iSeason = sEpisode.TVEp.Season
                            If iSeason > -1 Then
                                If Master.eSettings.TVEpisodePosterAnyEnabled AndAlso TVDBImages.ShowPoster.WebImage Is Nothing AndAlso Not String.IsNullOrEmpty(sEpisode.ShowPosterPath) Then
                                    TVDBImages.ShowPoster.WebImage.FromFile(sEpisode.ShowPosterPath)
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If

                                If Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso TVDBImages.ShowFanart.WebImage.Image Is Nothing AndAlso Not String.IsNullOrEmpty(sEpisode.ShowFanartPath) Then
                                    TVDBImages.ShowFanart.WebImage.FromFile(sEpisode.ShowFanartPath)
                                    TVDBImages.ShowFanart.LocalFile = sEpisode.ShowFanartPath
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If

                                If TVDBImages.SeasonImageList.Where(Function(s) s.Season = iSeason).Count = 0 Then
                                    cSI = New TVDBSeasonImage
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
                                    TVDBImages.SeasonImageList.Add(cSI)
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
                    For Each sEpisode As Structures.DBTV In tmpTVDBShow.Episodes
                        Try
                            iSeason = sEpisode.TVEp.Season

                            If TVDBImages.SeasonImageList.Where(Function(s) s.Season = iSeason).Count = 0 Then
                                cSI = New TVDBSeasonImage
                                cSI.Season = iSeason
                                TVDBImages.SeasonImageList.Add(cSI)
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

    Public Overloads Function ShowDialog(ByVal DBTV As Structures.DBTV, ByVal Type As Enums.ImageType_TV, ByRef ImagesContainer As MediaContainers.ImagesContainer_TV, ByRef efList As List(Of String), ByRef etList As List(Of String), Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.alContainer = ImagesContainer
        Me.tmpTVDBShow.Show = DBTV
        Return MyBase.ShowDialog()
    End Function

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

        Me.bwLoadImages.ReportProgress(tmpTVDBShow.Episodes.Count + alContainer.ShowFanarts.Count + alContainer.ShowPosters.Count + _
                                           alContainer.SeasonBanners.Count + alContainer.SeasonLandscapes.Count + alContainer.SeasonPosters.Count + _
                                           alContainer.ShowBanners.Count + alContainer.ShowCharacterArts.Count + alContainer.ShowClearArts.Count + _
                                           alContainer.ShowClearLogos.Count + alContainer.ShowLandscapes.Count + alContainer.SeasonPosters.Count, "max")

        'Banner AllSeasons/Show 
        For Each img In alContainer.ShowBanners
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showbanners", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Banner Season
        For Each img In alContainer.SeasonBanners
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonbanners", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'CharacterArt Show
        For Each img In alContainer.ShowCharacterArts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcharacterarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcharacterarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'ClearArt Show
        For Each img In alContainer.ShowClearArts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcleararts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcleararts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'ClearLogo Show 
        For Each img In alContainer.ShowClearLogos
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showclearlogos", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showclearlogos\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Fanart AllSeasons/Season/Show 
        For Each img In alContainer.ShowFanarts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showfanarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showfanarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Landscape AllSeasons/Show 
        For Each img In alContainer.ShowLandscapes
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Landscape Season 
        For Each img In alContainer.SeasonLandscapes
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Poster AllSeasons/Show 
        For Each img In alContainer.ShowPosters
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showposters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Poster Season 
        For Each img In alContainer.SeasonPosters
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonposters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpTVDBShow.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next


        'Episode Poster
        If Me._type = Enums.ImageType_TV.All Then
            For Each Epi As Structures.DBTV In tmpTVDBShow.Episodes
                If Not File.Exists(Epi.TVEp.LocalFile) Then
                    If Not String.IsNullOrEmpty(Epi.TVEp.PosterURL) Then
                        Epi.TVEp.Poster.FromWeb(Epi.TVEp.PosterURL)
                        If Epi.TVEp.Poster.Image IsNot Nothing Then
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

        'Season Poster/AllSeasons Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In alContainer.SeasonPosters
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
            For Each tImg As MediaContainers.Image In alContainer.SeasonBanners
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
            For Each tImg As MediaContainers.Image In alContainer.SeasonLandscapes
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
            For Each tImg As MediaContainers.Image In alContainer.ShowPosters
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
            For Each tImg As MediaContainers.Image In alContainer.ShowBanners
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
            For Each tImg As MediaContainers.Image In alContainer.ShowCharacterArts
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
            For Each tImg As MediaContainers.Image In alContainer.ShowClearArts
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
            For Each tImg As MediaContainers.Image In alContainer.ShowClearLogos
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
            For Each tImg As MediaContainers.Image In alContainer.ShowLandscapes
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
            For Each tImg As MediaContainers.Image In alContainer.ShowFanarts
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
            For Each cSeason As TVDBSeasonImage In TVDBImages.SeasonImageList.OrderBy(Function(s) s.Season)
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
                TVDBImages.ShowBanner.WebImage = DefaultImages.ShowBanner.WebImage
                TVDBImages.ShowBanner.LocalFile = DefaultImages.ShowBanner.LocalFile
                TVDBImages.ShowBanner.URL = DefaultImages.ShowBanner.URL
                Me.pbCurrent.Image = TVDBImages.ShowBanner.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowBanner.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                TVDBImages.ShowCharacterArt.WebImage = DefaultImages.ShowCharacterArt.WebImage
                TVDBImages.ShowCharacterArt.LocalFile = DefaultImages.ShowCharacterArt.LocalFile
                TVDBImages.ShowCharacterArt.URL = DefaultImages.ShowCharacterArt.URL
                Me.pbCurrent.Image = TVDBImages.ShowCharacterArt.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowCharacterArt.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                TVDBImages.ShowClearArt.WebImage = DefaultImages.ShowClearArt.WebImage
                TVDBImages.ShowClearArt.LocalFile = DefaultImages.ShowClearArt.LocalFile
                TVDBImages.ShowClearArt.URL = DefaultImages.ShowClearArt.URL
                Me.pbCurrent.Image = TVDBImages.ShowClearArt.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowClearArt.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                TVDBImages.ShowClearLogo.WebImage = DefaultImages.ShowClearLogo.WebImage
                TVDBImages.ShowClearLogo.LocalFile = DefaultImages.ShowClearLogo.LocalFile
                TVDBImages.ShowClearLogo.URL = DefaultImages.ShowClearLogo.URL
                Me.pbCurrent.Image = TVDBImages.ShowClearLogo.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowClearLogo.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                TVDBImages.ShowFanart.WebImage = DefaultImages.ShowFanart.WebImage
                TVDBImages.ShowFanart.LocalFile = DefaultImages.ShowFanart.LocalFile
                TVDBImages.ShowFanart.URL = DefaultImages.ShowFanart.URL
                Me.pbCurrent.Image = TVDBImages.ShowFanart.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowFanart.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                TVDBImages.ShowPoster.WebImage = DefaultImages.ShowPoster.WebImage
                TVDBImages.ShowPoster.LocalFile = DefaultImages.ShowPoster.LocalFile
                TVDBImages.ShowPoster.URL = DefaultImages.ShowPoster.URL
                Me.pbCurrent.Image = TVDBImages.ShowPoster.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.ShowPoster.WebImage
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                TVDBImages.AllSeasonsBanner.WebImage = DefaultImages.AllSeasonsBanner.WebImage
                TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                Me.pbCurrent.Image = TVDBImages.AllSeasonsBanner.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsBanner.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                TVDBImages.AllSeasonsFanart.WebImage = DefaultImages.AllSeasonsFanart.WebImage
                TVDBImages.AllSeasonsFanart.LocalFile = DefaultImages.AllSeasonsFanart.LocalFile
                TVDBImages.AllSeasonsFanart.URL = DefaultImages.AllSeasonsFanart.URL
                Me.pbCurrent.Image = TVDBImages.AllSeasonsFanart.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsFanart.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                TVDBImages.AllSeasonsLandscape.WebImage = DefaultImages.AllSeasonsLandscape.WebImage
                TVDBImages.AllSeasonsLandscape.LocalFile = DefaultImages.AllSeasonsLandscape.LocalFile
                TVDBImages.AllSeasonsLandscape.URL = DefaultImages.AllSeasonsLandscape.URL
                Me.pbCurrent.Image = TVDBImages.AllSeasonsLandscape.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsLandscape.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                TVDBImages.AllSeasonsPoster.WebImage = DefaultImages.AllSeasonsPoster.WebImage
                TVDBImages.AllSeasonsPoster.LocalFile = DefaultImages.AllSeasonsPoster.LocalFile
                TVDBImages.AllSeasonsPoster.URL = DefaultImages.AllSeasonsPoster.URL
                Me.pbCurrent.Image = TVDBImages.AllSeasonsPoster.WebImage.Image
                Me.pbCurrent.Tag = TVDBImages.AllSeasonsPoster.WebImage
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                Dim dSPost As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost.WebImage

                'TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner

                'TVDBImages.SeasonImageList..Image = DefaultImages.AllSeasonsBanner.Image
                'TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                'TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                'Me.pbCurrent.Image = TVDBImages.AllSeasonsBanner.WebImage.Image
                'Me.pbCurrent.Tag = TVDBImages.AllSeasonsBanner.Image

            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                Dim dSFan As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                Dim tSFan As MediaContainers.Image = TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tSFan.WebImage = dSFan.WebImage
                tSFan.LocalFile = dSFan.LocalFile
                tSFan.URL = dSFan.URL
                Me.pbCurrent.Image = dSFan.WebImage.Image
                Me.pbCurrent.Tag = dSFan.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                Dim dSPost As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost.WebImage
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                Dim dSPost As MediaContainers.Image = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster = dSPost
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
                TVDBImages.ShowBanner.WebImage = SelTag.ImageObj
                TVDBImages.ShowBanner.LocalFile = SelTag.Path
                TVDBImages.ShowBanner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                TVDBImages.ShowCharacterArt.WebImage = SelTag.ImageObj
                TVDBImages.ShowCharacterArt.LocalFile = SelTag.Path
                TVDBImages.ShowCharacterArt.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                TVDBImages.ShowClearArt.WebImage = SelTag.ImageObj
                TVDBImages.ShowClearArt.LocalFile = SelTag.Path
                TVDBImages.ShowClearArt.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                TVDBImages.ShowClearLogo.WebImage = SelTag.ImageObj
                TVDBImages.ShowClearLogo.LocalFile = SelTag.Path
                TVDBImages.ShowClearLogo.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowLandscape Then
                TVDBImages.ShowLandscape.WebImage = SelTag.ImageObj
                TVDBImages.ShowLandscape.LocalFile = SelTag.Path
                TVDBImages.ShowLandscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                TVDBImages.ShowFanart.WebImage = SelTag.ImageObj
                TVDBImages.ShowFanart.LocalFile = SelTag.Path
                TVDBImages.ShowFanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                TVDBImages.ShowPoster.WebImage = SelTag.ImageObj
                TVDBImages.ShowPoster.LocalFile = SelTag.Path
                TVDBImages.ShowPoster.URL = SelTag.URL
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                TVDBImages.AllSeasonsBanner.WebImage = SelTag.ImageObj
                TVDBImages.AllSeasonsBanner.LocalFile = SelTag.Path
                TVDBImages.AllSeasonsBanner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                TVDBImages.AllSeasonsFanart.WebImage = SelTag.ImageObj
                TVDBImages.AllSeasonsFanart.LocalFile = SelTag.Path
                TVDBImages.AllSeasonsFanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                TVDBImages.AllSeasonsLandscape.WebImage = SelTag.ImageObj
                TVDBImages.AllSeasonsLandscape.LocalFile = SelTag.Path
                TVDBImages.AllSeasonsLandscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                TVDBImages.AllSeasonsPoster.WebImage = SelTag.ImageObj
                TVDBImages.AllSeasonsPoster.LocalFile = SelTag.Path
                TVDBImages.AllSeasonsPoster.URL = SelTag.URL
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.WebImage = SelTag.ImageObj
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.LocalFile = SelTag.Path
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.WebImage = SelTag.ImageObj
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.LocalFile = SelTag.Path
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.WebImage = SelTag.ImageObj
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.LocalFile = SelTag.Path
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape.URL = SelTag.URL
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster.WebImage = SelTag.ImageObj
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster.LocalFile = SelTag.Path
                TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster.URL = SelTag.URL
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
                If TVDBImages.ShowBanner IsNot Nothing AndAlso TVDBImages.ShowBanner.WebImage IsNot Nothing AndAlso TVDBImages.ShowBanner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.ShowBanner.WebImage.Image
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
                If TVDBImages.ShowCharacterArt IsNot Nothing AndAlso TVDBImages.ShowCharacterArt.WebImage IsNot Nothing AndAlso TVDBImages.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.ShowCharacterArt.WebImage.Image
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
                If TVDBImages.ShowClearArt IsNot Nothing AndAlso TVDBImages.ShowClearArt.WebImage IsNot Nothing AndAlso TVDBImages.ShowClearArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.ShowClearArt.WebImage.Image
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
                If TVDBImages.ShowClearLogo IsNot Nothing AndAlso TVDBImages.ShowClearLogo.WebImage IsNot Nothing AndAlso TVDBImages.ShowClearLogo.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.ShowClearLogo.WebImage.Image
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
                If TVDBImages.ShowFanart IsNot Nothing AndAlso TVDBImages.ShowFanart.WebImage IsNot Nothing AndAlso TVDBImages.ShowFanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.ShowFanart.WebImage.Image
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
                If TVDBImages.ShowLandscape IsNot Nothing AndAlso TVDBImages.ShowLandscape.WebImage IsNot Nothing AndAlso TVDBImages.ShowLandscape.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.ShowLandscape.WebImage.Image
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
                If TVDBImages.ShowPoster IsNot Nothing AndAlso TVDBImages.ShowPoster.WebImage IsNot Nothing AndAlso TVDBImages.ShowPoster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.ShowPoster.WebImage.Image
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
                If TVDBImages.AllSeasonsBanner IsNot Nothing AndAlso TVDBImages.AllSeasonsBanner.WebImage IsNot Nothing AndAlso TVDBImages.AllSeasonsBanner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsBanner.WebImage.Image
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
                If TVDBImages.AllSeasonsFanart IsNot Nothing AndAlso TVDBImages.AllSeasonsFanart.WebImage IsNot Nothing AndAlso TVDBImages.AllSeasonsFanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsFanart.WebImage.Image
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
                If TVDBImages.AllSeasonsLandscape IsNot Nothing AndAlso TVDBImages.AllSeasonsLandscape.WebImage IsNot Nothing AndAlso TVDBImages.AllSeasonsLandscape.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsLandscape.WebImage.Image
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
                If TVDBImages.AllSeasonsPoster IsNot Nothing AndAlso TVDBImages.AllSeasonsPoster.WebImage IsNot Nothing AndAlso TVDBImages.AllSeasonsPoster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = TVDBImages.AllSeasonsPoster.WebImage.Image
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
                        Dim tBanner As TVDBSeasonImage = TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
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
                        Dim tFanart As TVDBSeasonImage = TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Convert.ToInt32(tMatch.Groups("num").Value))
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
                        Dim tLandscape As TVDBSeasonImage = TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
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
                        Dim tPoster As TVDBSeasonImage = TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
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

    <Serializable()> _
    Public Structure TVImages

#Region "Fields"

        Dim AllSeasonsBanner As MediaContainers.Image
        Dim AllSeasonsFanart As MediaContainers.Image
        Dim AllSeasonsLandscape As MediaContainers.Image
        Dim AllSeasonsPoster As MediaContainers.Image
        Dim SeasonImageList As List(Of TVDBSeasonImage)
        Dim ShowBanner As MediaContainers.Image
        Dim ShowCharacterArt As MediaContainers.Image
        Dim ShowClearArt As MediaContainers.Image
        Dim ShowClearLogo As MediaContainers.Image
        Dim ShowFanart As MediaContainers.Image
        Dim ShowLandscape As MediaContainers.Image
        Dim ShowPoster As MediaContainers.Image

#End Region 'Fields

#Region "Methods"
        'TODO: make the new class serializable
        Public Function Clone() As TVImages
            Dim newTVI As New TVImages
            'Try
            '    Using ms As New IO.MemoryStream()
            '        Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            '        bf.Serialize(ms, Me)
            '        ms.Position = 0
            '        newTVI = DirectCast(bf.Deserialize(ms), TVImages)
            '        ms.Close()
            '    End Using
            'Catch ex As Exception
            '    logger.Error(New StackFrame().GetMethod().Name,ex)
            'End Try
            Return newTVI
        End Function

#End Region 'Methods

    End Structure

    <Serializable()> _
    Public Class TVDBSeasonImage

#Region "Fields"

        Private _alreadysaved As Boolean
        Private _banner As MediaContainers.Image
        Private _fanart As MediaContainers.Image
        Private _landscape As MediaContainers.Image
        Private _poster As MediaContainers.Image
        Private _season As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AlreadySaved() As Boolean
            Get
                Return Me._alreadysaved
            End Get
            Set(ByVal value As Boolean)
                Me._alreadysaved = value
            End Set
        End Property

        Public Property Banner() As MediaContainers.Image
            Get
                Return Me._banner
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._banner = value
            End Set
        End Property

        Public Property Fanart() As MediaContainers.Image
            Get
                Return Me._fanart
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._fanart = value
            End Set
        End Property

        Public Property Landscape() As MediaContainers.Image
            Get
                Return Me._landscape
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._landscape = value
            End Set
        End Property

        Public Property Poster() As MediaContainers.Image
            Get
                Return Me._poster
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._poster = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._alreadysaved = False
            Me._banner = New MediaContainers.Image
            Me._fanart = New MediaContainers.Image
            Me._landscape = New MediaContainers.Image
            Me._poster = New MediaContainers.Image
            Me._season = -1
        End Sub

#End Region 'Methods

    End Class

    Public Class TVDBShow

#Region "Fields"

        Private _allseason As Structures.DBTV
        Private _episodes As New List(Of Structures.DBTV)
        Private _fanarts As New List(Of MediaContainers.Image)
        Private _posters As New List(Of MediaContainers.Image)
        Private _seasonposters As New List(Of MediaContainers.Image)
        Private _seasonbanners As New List(Of MediaContainers.Image)
        Private _seasonlandscapes As New List(Of MediaContainers.Image)
        Private _show As Structures.DBTV
        Private _showbanners As New List(Of MediaContainers.Image)
        Private _showcharacterarts As New List(Of MediaContainers.Image)
        Private _showcleararts As New List(Of MediaContainers.Image)
        Private _showclearlogos As New List(Of MediaContainers.Image)
        Private _showlandscapes As New List(Of MediaContainers.Image)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AllSeason() As Structures.DBTV
            Get
                Return Me._allseason
            End Get
            Set(ByVal value As Structures.DBTV)
                Me._allseason = value
            End Set
        End Property

        Public Property Episodes() As List(Of Structures.DBTV)
            Get
                Return Me._episodes
            End Get
            Set(ByVal value As List(Of Structures.DBTV))
                Me._episodes = value
            End Set
        End Property

        Public Property Fanarts() As List(Of MediaContainers.Image)
            Get
                Return Me._fanarts
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._fanarts = value
            End Set
        End Property

        Public Property Posters() As List(Of MediaContainers.Image)
            Get
                Return Me._posters
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._posters = value
            End Set
        End Property

        Public Property SeasonPosters() As List(Of MediaContainers.Image)
            Get
                Return Me._seasonposters
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._seasonposters = value
            End Set
        End Property

        Public Property SeasonBanners() As List(Of MediaContainers.Image)
            Get
                Return Me._seasonbanners
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._seasonbanners = value
            End Set
        End Property

        Public Property SeasonLandscapes() As List(Of MediaContainers.Image)
            Get
                Return Me._seasonlandscapes
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._seasonlandscapes = value
            End Set
        End Property

        Public Property Show() As Structures.DBTV
            Get
                Return Me._show
            End Get
            Set(ByVal value As Structures.DBTV)
                Me._show = value
            End Set
        End Property

        Public Property ShowBanners() As List(Of MediaContainers.Image)
            Get
                Return Me._showbanners
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showbanners = value
            End Set
        End Property

        Public Property ShowCharacterArts() As List(Of MediaContainers.Image)
            Get
                Return Me._showcharacterarts
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showcharacterarts = value
            End Set
        End Property

        Public Property ShowClearArts() As List(Of MediaContainers.Image)
            Get
                Return Me._showcleararts
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showcleararts = value
            End Set
        End Property

        Public Property ShowClearLogos() As List(Of MediaContainers.Image)
            Get
                Return Me._showclearlogos
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showclearlogos = value
            End Set
        End Property

        Public Property ShowLandscapes() As List(Of MediaContainers.Image)
            Get
                Return Me._showlandscapes
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showlandscapes = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._show = New Structures.DBTV
            Me._allseason = New Structures.DBTV
            Me._episodes = New List(Of Structures.DBTV)
            Me._fanarts = New List(Of MediaContainers.Image)
            Me._showbanners = New List(Of MediaContainers.Image)
            Me._showcharacterarts = New List(Of MediaContainers.Image)
            Me._showcleararts = New List(Of MediaContainers.Image)
            Me._showclearlogos = New List(Of MediaContainers.Image)
            Me._showlandscapes = New List(Of MediaContainers.Image)
            Me._seasonposters = New List(Of MediaContainers.Image)
            Me._seasonbanners = New List(Of MediaContainers.Image)
            Me._seasonlandscapes = New List(Of MediaContainers.Image)
            Me._posters = New List(Of MediaContainers.Image)
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class