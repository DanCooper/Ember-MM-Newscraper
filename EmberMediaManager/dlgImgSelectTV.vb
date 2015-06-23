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

    Private DefaultImagesContainer As New MediaContainers.ImagesContainer_TV
    Private ImageResultsContainer As New MediaContainers.ImagesContainer_TV
    Private SearchResultsContainer As New MediaContainers.SearchResultsContainer_TV

    Private tmpShowContainer As New TVDBShow

    Private iCounter As Integer = 0
    Private iLeft As Integer = 5
    Private iTop As Integer = 5
    Private lblImage() As Label
    Private pbImage() As PictureBox
    Private pnlImage() As Panel
    Private SelImgType As Enums.ImageType_TV
    Private SelSeason As Integer = -999
    Private _id As Integer = -1
    Private _season As Integer = -999
    Private _type As Enums.ImageType_TV = Enums.ImageType_TV.All
    Private _withcurrent As Boolean = True
    Private _ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Results As MediaContainers.ImagesContainer_TV
        Get
            Return ImageResultsContainer
        End Get
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

        Me.bwLoadImages.ReportProgress(ImageResultsContainer.SeasonImages.Count + tmpShowContainer.Episodes.Count + iProgress, "defaults")

        'AllSeason Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled AndAlso DefaultImagesContainer.SeasonBanner.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASBanner(SearchResultsContainer.SeasonBanners, SearchResultsContainer.ShowBanners, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.SeasonBanner = defImg
                ImageResultsContainer.SeasonBanner = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(1, "progress")

        'AllSeason Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled AndAlso DefaultImagesContainer.SeasonFanart.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASFanart(SearchResultsContainer.ShowFanarts, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.SeasonFanart = defImg
                ImageResultsContainer.SeasonFanart = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(2, "progress")

        'AllSeason Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsLandscape) AndAlso Master.eSettings.TVASLandscapeAnyEnabled AndAlso DefaultImagesContainer.SeasonLandscape.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASLandscape(SearchResultsContainer.SeasonLandscapes, SearchResultsContainer.ShowLandscapes, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.SeasonLandscape = defImg
                ImageResultsContainer.SeasonLandscape = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(3, "progress")

        'AllSeason Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled AndAlso DefaultImagesContainer.SeasonPoster.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVASPoster(SearchResultsContainer.SeasonPosters, SearchResultsContainer.ShowPosters, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.SeasonPoster = defImg
                ImageResultsContainer.SeasonPoster = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(4, "progress")

        'Show Banner
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled AndAlso DefaultImagesContainer.ShowBanner.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowBanner(SearchResultsContainer.ShowBanners, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.ShowBanner = defImg
                ImageResultsContainer.ShowBanner = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(5, "progress")

        'Show CharacterArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowCharacterArt) AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso DefaultImagesContainer.ShowCharacterArt.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowCharacterArt(SearchResultsContainer.ShowCharacterArts, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.ShowCharacterArt = defImg
                ImageResultsContainer.ShowCharacterArt = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(6, "progress")

        'Show ClearArt
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearArt) AndAlso Master.eSettings.TVShowClearArtAnyEnabled AndAlso DefaultImagesContainer.ShowClearArt.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowClearArt(SearchResultsContainer.ShowClearArts, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.ShowClearArt = defImg
                ImageResultsContainer.ShowClearArt = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(7, "progress")

        'Show ClearLogo
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowClearLogo) AndAlso Master.eSettings.TVShowClearLogoAnyEnabled AndAlso DefaultImagesContainer.ShowClearLogo.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowClearLogo(SearchResultsContainer.ShowClearLogos, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.ShowClearLogo = defImg
                ImageResultsContainer.ShowClearLogo = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(8, "progress")

        'Show Fanart
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) AndAlso DefaultImagesContainer.ShowFanart.WebImage.Image Is Nothing Then 'TODO: add *FanartEnabled check
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowFanart(SearchResultsContainer.ShowFanarts, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.ShowFanart = defImg
                ImageResultsContainer.ShowFanart = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(9, "progress")

        'Show Landscape
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowLandscape) AndAlso Master.eSettings.TVShowLandscapeAnyEnabled AndAlso DefaultImagesContainer.ShowLandscape.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowLandscape(SearchResultsContainer.ShowLandscapes, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.ShowLandscape = defImg
                ImageResultsContainer.ShowLandscape = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(10, "progress")

        'Show Poster
        If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled AndAlso DefaultImagesContainer.ShowPoster.WebImage.Image Is Nothing Then
            Dim defImg As MediaContainers.Image = Nothing
            Images.GetPreferredTVShowPoster(SearchResultsContainer.ShowPosters, defImg)

            If defImg IsNot Nothing Then
                DefaultImagesContainer.ShowPoster = defImg
                ImageResultsContainer.ShowPoster = defImg
            End If
        End If
        If Me.bwLoadImages.CancellationPending Then
            Return True
        End If
        Me.bwLoadImages.ReportProgress(11, "progress")

        'Season Banner/Fanart/Poster
        If Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster OrElse Me._type = Enums.ImageType_TV.SeasonBanner OrElse Me._type = Enums.ImageType_TV.SeasonFanart Then
            For Each cSeason As MediaContainers.SeasonImagesContainer In DefaultImagesContainer.SeasonImages
                Try
                    iSeason = cSeason.Season

                    'Season Banner
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonBanner) AndAlso Master.eSettings.TVSeasonBannerAnyEnabled AndAlso cSeason.Banner.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonBanner(SearchResultsContainer.SeasonBanners, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Banner = defImg
                        End If
                    End If

                    'Season Fanart
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonFanart) AndAlso Master.eSettings.TVSeasonFanartAnyEnabled AndAlso cSeason.Fanart.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonFanart(SearchResultsContainer.ShowFanarts, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Fanart = defImg
                        End If
                    End If

                    'Season Landscape
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonLandscape) AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso cSeason.Landscape.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonLandscape(SearchResultsContainer.SeasonLandscapes, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Landscape = defImg
                        End If
                    End If

                    'Season Poster
                    If (Me._type = Enums.ImageType_TV.All OrElse Me._type = Enums.ImageType_TV.SeasonPoster) AndAlso Master.eSettings.TVSeasonPosterAnyEnabled AndAlso cSeason.Poster.WebImage.Image Is Nothing Then
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVSeasonPoster(SearchResultsContainer.SeasonPosters, defImg, iSeason)

                        If defImg IsNot Nothing Then
                            cSeason.Poster = defImg
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
            For Each Episode As Structures.DBTV In tmpShowContainer.Episodes

                'Fanart
                If Master.eSettings.TVEpisodeFanartAnyEnabled Then
                    If Not String.IsNullOrEmpty(Episode.FanartPath) Then
                        Episode.TVEp.Fanart.WebImage.FromFile(Episode.FanartPath)
                    ElseIf ImageResultsContainer.ShowFanart.WebImage.Image IsNot Nothing Then
                        Episode.TVEp.Fanart = ImageResultsContainer.ShowFanart
                    End If
                End If

                'Poster
                If Master.eSettings.TVEpisodePosterAnyEnabled Then
                    If Not String.IsNullOrEmpty(Episode.TVEp.LocalFile) Then
                        Episode.TVEp.Poster.WebImage.FromFile(Episode.TVEp.LocalFile)
                    ElseIf Not String.IsNullOrEmpty(Episode.PosterPath) Then
                        Episode.TVEp.Poster.WebImage.FromFile(Episode.PosterPath)
                    End If
                End If
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

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

    Private Sub AddImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal fTag As MediaContainers.Image)
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
            Me.pbImage(iIndex).Image = fTag.WebImage.Image
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
                If Not String.IsNullOrEmpty(ImageResultsContainer.ShowBanner.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowBanner.LocalFile) Then
                    ImageResultsContainer.ShowBanner.WebImage.FromFile(ImageResultsContainer.ShowBanner.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowBanner.LocalFile) Then
                    ImageResultsContainer.ShowBanner.WebImage.Clear()
                    ImageResultsContainer.ShowBanner.WebImage.FromWeb(ImageResultsContainer.ShowBanner.URL)
                    If ImageResultsContainer.ShowBanner.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowBanner.LocalFile).FullName)
                        ImageResultsContainer.ShowBanner.WebImage.Save(ImageResultsContainer.ShowBanner.LocalFile)
                    End If
                End If
            End If

            'Show CharacterArt
            If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.ShowCharacterArt.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowCharacterArt.LocalFile) Then
                    ImageResultsContainer.ShowCharacterArt.WebImage.FromFile(ImageResultsContainer.ShowCharacterArt.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowCharacterArt.LocalFile) Then
                    ImageResultsContainer.ShowCharacterArt.WebImage.Clear()
                    ImageResultsContainer.ShowCharacterArt.WebImage.FromWeb(ImageResultsContainer.ShowCharacterArt.URL)
                    If ImageResultsContainer.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowCharacterArt.LocalFile).FullName)
                        ImageResultsContainer.ShowCharacterArt.WebImage.Save(ImageResultsContainer.ShowCharacterArt.LocalFile)
                    End If
                End If
            End If

            'Show ClearArt
            If Master.eSettings.TVShowClearArtAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearArt.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowClearArt.LocalFile) Then
                    ImageResultsContainer.ShowClearArt.WebImage.FromFile(ImageResultsContainer.ShowClearArt.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearArt.LocalFile) Then
                    ImageResultsContainer.ShowClearArt.WebImage.Clear()
                    ImageResultsContainer.ShowClearArt.WebImage.FromWeb(ImageResultsContainer.ShowClearArt.URL)
                    If ImageResultsContainer.ShowClearArt.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowClearArt.LocalFile).FullName)
                        ImageResultsContainer.ShowClearArt.WebImage.Save(ImageResultsContainer.ShowClearArt.LocalFile)
                    End If
                End If
            End If

            'Show ClearLogo
            If Master.eSettings.TVShowClearLogoAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearLogo.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowClearLogo.LocalFile) Then
                    ImageResultsContainer.ShowClearLogo.WebImage.FromFile(ImageResultsContainer.ShowClearLogo.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearLogo.LocalFile) Then
                    ImageResultsContainer.ShowClearLogo.WebImage.Clear()
                    ImageResultsContainer.ShowClearLogo.WebImage.FromWeb(ImageResultsContainer.ShowClearLogo.URL)
                    If ImageResultsContainer.ShowClearLogo.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowClearLogo.LocalFile).FullName)
                        ImageResultsContainer.ShowClearLogo.WebImage.Save(ImageResultsContainer.ShowClearLogo.LocalFile)
                    End If
                End If
            End If

            'Show Fanart
            If Master.eSettings.TVShowFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.ShowFanart.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowFanart.LocalFile) Then
                    ImageResultsContainer.ShowFanart.WebImage.FromFile(ImageResultsContainer.ShowFanart.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowFanart.LocalFile) Then
                    ImageResultsContainer.ShowFanart.WebImage.Clear()
                    ImageResultsContainer.ShowFanart.WebImage.FromWeb(ImageResultsContainer.ShowFanart.URL)
                    If ImageResultsContainer.ShowFanart.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowFanart.LocalFile).FullName)
                        ImageResultsContainer.ShowFanart.WebImage.Save(ImageResultsContainer.ShowFanart.LocalFile)
                    End If
                End If
            End If

            'Show Landscape
            If Master.eSettings.TVShowLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.ShowLandscape.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowLandscape.LocalFile) Then
                    ImageResultsContainer.ShowLandscape.WebImage.FromFile(ImageResultsContainer.ShowLandscape.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowLandscape.LocalFile) Then
                    ImageResultsContainer.ShowLandscape.WebImage.Clear()
                    ImageResultsContainer.ShowLandscape.WebImage.FromWeb(ImageResultsContainer.ShowLandscape.URL)
                    If ImageResultsContainer.ShowLandscape.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowLandscape.LocalFile).FullName)
                        ImageResultsContainer.ShowLandscape.WebImage.Save(ImageResultsContainer.ShowLandscape.LocalFile)
                    End If
                End If
            End If

            'Show Poster
            If Master.eSettings.TVShowPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.ShowPoster.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowPoster.LocalFile) Then
                    ImageResultsContainer.ShowPoster.WebImage.FromFile(ImageResultsContainer.ShowPoster.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowPoster.LocalFile) Then
                    ImageResultsContainer.ShowPoster.WebImage.Clear()
                    ImageResultsContainer.ShowPoster.WebImage.FromWeb(ImageResultsContainer.ShowPoster.URL)
                    If ImageResultsContainer.ShowPoster.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowPoster.LocalFile).FullName)
                        ImageResultsContainer.ShowPoster.WebImage.Save(ImageResultsContainer.ShowPoster.LocalFile)
                    End If
                End If
            End If

            'AS Banner
            If Master.eSettings.TVASBannerAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonBanner.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonBanner.LocalFile) Then
                    ImageResultsContainer.SeasonBanner.WebImage.FromFile(ImageResultsContainer.SeasonBanner.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonBanner.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonBanner.LocalFile) Then
                    ImageResultsContainer.SeasonBanner.WebImage.Clear()
                    ImageResultsContainer.SeasonBanner.WebImage.FromWeb(ImageResultsContainer.SeasonBanner.URL)
                    If ImageResultsContainer.SeasonBanner.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonBanner.LocalFile).FullName)
                        ImageResultsContainer.SeasonBanner.WebImage.Save(ImageResultsContainer.SeasonBanner.LocalFile)
                    End If
                End If
            End If

            'AS Fanart
            If Master.eSettings.TVASFanartAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonFanart.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonFanart.LocalFile) Then
                    ImageResultsContainer.SeasonFanart.WebImage.FromFile(ImageResultsContainer.SeasonFanart.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonFanart.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonFanart.LocalFile) Then
                    ImageResultsContainer.SeasonFanart.WebImage.Clear()
                    ImageResultsContainer.SeasonFanart.WebImage.FromWeb(ImageResultsContainer.SeasonFanart.URL)
                    If ImageResultsContainer.SeasonFanart.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonFanart.LocalFile).FullName)
                        ImageResultsContainer.SeasonFanart.WebImage.Save(ImageResultsContainer.SeasonFanart.LocalFile)
                    End If
                End If
            End If

            'AS Landscape
            If Master.eSettings.TVASLandscapeAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonLandscape.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonLandscape.LocalFile) Then
                    ImageResultsContainer.SeasonLandscape.WebImage.FromFile(ImageResultsContainer.SeasonLandscape.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonLandscape.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonLandscape.LocalFile) Then
                    ImageResultsContainer.SeasonLandscape.WebImage.Clear()
                    ImageResultsContainer.SeasonLandscape.WebImage.FromWeb(ImageResultsContainer.SeasonLandscape.URL)
                    If ImageResultsContainer.SeasonLandscape.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonLandscape.LocalFile).FullName)
                        ImageResultsContainer.SeasonLandscape.WebImage.Save(ImageResultsContainer.SeasonLandscape.LocalFile)
                    End If
                End If
            End If

            'AS Poster
            If Master.eSettings.TVASPosterAnyEnabled Then
                If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonPoster.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonPoster.LocalFile) Then
                    ImageResultsContainer.SeasonPoster.WebImage.FromFile(ImageResultsContainer.SeasonPoster.LocalFile)
                ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonPoster.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonPoster.LocalFile) Then
                    ImageResultsContainer.SeasonPoster.WebImage.Clear()
                    ImageResultsContainer.SeasonPoster.WebImage.FromWeb(ImageResultsContainer.SeasonPoster.URL)
                    If ImageResultsContainer.SeasonPoster.WebImage.Image IsNot Nothing Then
                        Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonPoster.LocalFile).FullName)
                        ImageResultsContainer.SeasonPoster.WebImage.Save(ImageResultsContainer.SeasonPoster.LocalFile)
                    End If
                End If
            End If

        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonBanner.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonBanner.LocalFile) Then
                ImageResultsContainer.SeasonBanner.WebImage.FromFile(ImageResultsContainer.SeasonBanner.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonBanner.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonBanner.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonBanner.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonBanner.LocalFile) Then
                ImageResultsContainer.SeasonBanner.WebImage.Clear()
                ImageResultsContainer.SeasonBanner.WebImage.FromWeb(ImageResultsContainer.SeasonBanner.URL)
                If ImageResultsContainer.SeasonBanner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonBanner.LocalFile).FullName)
                    ImageResultsContainer.SeasonBanner.WebImage.Save(ImageResultsContainer.SeasonBanner.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonBanner.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonBanner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonFanart.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonFanart.LocalFile) Then
                ImageResultsContainer.SeasonFanart.WebImage.FromFile(ImageResultsContainer.SeasonFanart.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonFanart.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonFanart.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonFanart.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonFanart.LocalFile) Then
                ImageResultsContainer.SeasonFanart.WebImage.Clear()
                ImageResultsContainer.SeasonFanart.WebImage.FromWeb(ImageResultsContainer.SeasonFanart.URL)
                If ImageResultsContainer.SeasonFanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonFanart.LocalFile).FullName)
                    ImageResultsContainer.SeasonFanart.WebImage.Save(ImageResultsContainer.SeasonFanart.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonFanart.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonFanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonLandscape.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonLandscape.LocalFile) Then
                ImageResultsContainer.SeasonLandscape.WebImage.FromFile(ImageResultsContainer.SeasonLandscape.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonLandscape.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonLandscape.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonLandscape.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonLandscape.LocalFile) Then
                ImageResultsContainer.SeasonLandscape.WebImage.Clear()
                ImageResultsContainer.SeasonLandscape.WebImage.FromWeb(ImageResultsContainer.SeasonLandscape.URL)
                If ImageResultsContainer.SeasonLandscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonLandscape.LocalFile).FullName)
                    ImageResultsContainer.SeasonLandscape.WebImage.Save(ImageResultsContainer.SeasonLandscape.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonLandscape.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonLandscape.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.AllSeasonsPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonPoster.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonPoster.LocalFile) Then
                ImageResultsContainer.SeasonPoster.WebImage.FromFile(ImageResultsContainer.SeasonPoster.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonPoster.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonPoster.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonPoster.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonPoster.LocalFile) Then
                ImageResultsContainer.SeasonPoster.WebImage.Clear()
                ImageResultsContainer.SeasonPoster.WebImage.FromWeb(ImageResultsContainer.SeasonPoster.URL)
                If ImageResultsContainer.SeasonPoster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonPoster.LocalFile).FullName)
                    ImageResultsContainer.SeasonPoster.WebImage.Save(ImageResultsContainer.SeasonPoster.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonPoster.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonPoster.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Banner.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonImages(0).Banner.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Banner.WebImage.FromFile(ImageResultsContainer.SeasonImages(0).Banner.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Banner.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Banner.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Banner.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Banner.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Banner.WebImage.Clear()
                ImageResultsContainer.SeasonImages(0).Banner.WebImage.FromWeb(ImageResultsContainer.SeasonImages(0).Banner.URL)
                If ImageResultsContainer.SeasonImages(0).Banner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonImages(0).Banner.LocalFile).FullName)
                    ImageResultsContainer.SeasonImages(0).Banner.WebImage.Save(ImageResultsContainer.SeasonImages(0).Banner.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Banner.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Banner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonFanart Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Fanart.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonImages(0).Fanart.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Fanart.WebImage.FromFile(ImageResultsContainer.SeasonImages(0).Fanart.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Fanart.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Fanart.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Fanart.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Fanart.WebImage.Clear()
                ImageResultsContainer.SeasonImages(0).Fanart.WebImage.FromWeb(ImageResultsContainer.SeasonImages(0).Fanart.URL)
                If ImageResultsContainer.SeasonImages(0).Fanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonImages(0).Fanart.LocalFile).FullName)
                    ImageResultsContainer.SeasonImages(0).Fanart.WebImage.Save(ImageResultsContainer.SeasonImages(0).Fanart.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Fanart.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Fanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Landscape.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonImages(0).Landscape.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Landscape.WebImage.FromFile(ImageResultsContainer.SeasonImages(0).Landscape.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Landscape.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Landscape.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Landscape.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Landscape.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Landscape.WebImage.Clear()
                ImageResultsContainer.SeasonImages(0).Landscape.WebImage.FromWeb(ImageResultsContainer.SeasonImages(0).Landscape.URL)
                If ImageResultsContainer.SeasonImages(0).Landscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonImages(0).Landscape.LocalFile).FullName)
                    ImageResultsContainer.SeasonImages(0).Landscape.WebImage.Save(ImageResultsContainer.SeasonImages(0).Landscape.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Landscape.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Landscape.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.SeasonPoster Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Poster.LocalFile) AndAlso File.Exists(ImageResultsContainer.SeasonImages(0).Poster.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Poster.WebImage.FromFile(ImageResultsContainer.SeasonImages(0).Poster.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Poster.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Poster.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Poster.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.SeasonImages(0).Poster.LocalFile) Then
                ImageResultsContainer.SeasonImages(0).Poster.WebImage.Clear()
                ImageResultsContainer.SeasonImages(0).Poster.WebImage.FromWeb(ImageResultsContainer.SeasonImages(0).Poster.URL)
                If ImageResultsContainer.SeasonImages(0).Poster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.SeasonImages(0).Poster.LocalFile).FullName)
                    ImageResultsContainer.SeasonImages(0).Poster.WebImage.Save(ImageResultsContainer.SeasonImages(0).Poster.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonImages(0).Poster.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.SeasonImages(0).Poster.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowBanner Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.ShowBanner.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowBanner.LocalFile) Then
                ImageResultsContainer.ShowBanner.WebImage.FromFile(ImageResultsContainer.ShowBanner.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.ShowBanner.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowBanner.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowBanner.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowBanner.LocalFile) Then
                ImageResultsContainer.ShowBanner.WebImage.Clear()
                ImageResultsContainer.ShowBanner.WebImage.FromWeb(ImageResultsContainer.ShowBanner.URL)
                If ImageResultsContainer.ShowBanner.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowBanner.LocalFile).FullName)
                    ImageResultsContainer.ShowBanner.WebImage.Save(ImageResultsContainer.ShowBanner.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.ShowBanner.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.ShowBanner.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowCharacterArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.ShowCharacterArt.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowCharacterArt.LocalFile) Then
                ImageResultsContainer.ShowCharacterArt.WebImage.FromFile(ImageResultsContainer.ShowCharacterArt.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.ShowCharacterArt.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowCharacterArt.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowCharacterArt.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowCharacterArt.LocalFile) Then
                ImageResultsContainer.ShowCharacterArt.WebImage.Clear()
                ImageResultsContainer.ShowCharacterArt.WebImage.FromWeb(ImageResultsContainer.ShowCharacterArt.URL)
                If ImageResultsContainer.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowCharacterArt.LocalFile).FullName)
                    ImageResultsContainer.ShowCharacterArt.WebImage.Save(ImageResultsContainer.ShowCharacterArt.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.ShowCharacterArt.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.ShowCharacterArt.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearArt Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearArt.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowClearArt.LocalFile) Then
                ImageResultsContainer.ShowClearArt.WebImage.FromFile(ImageResultsContainer.ShowClearArt.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.ShowClearArt.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowClearArt.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearArt.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearArt.LocalFile) Then
                ImageResultsContainer.ShowClearArt.WebImage.Clear()
                ImageResultsContainer.ShowClearArt.WebImage.FromWeb(ImageResultsContainer.ShowClearArt.URL)
                If ImageResultsContainer.ShowClearArt.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowClearArt.LocalFile).FullName)
                    ImageResultsContainer.ShowClearArt.WebImage.Save(ImageResultsContainer.ShowClearArt.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.ShowClearArt.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.ShowClearArt.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowClearLogo Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearLogo.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowClearLogo.LocalFile) Then
                ImageResultsContainer.ShowClearLogo.WebImage.FromFile(ImageResultsContainer.ShowClearLogo.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.ShowClearLogo.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowClearLogo.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearLogo.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowClearLogo.LocalFile) Then
                ImageResultsContainer.ShowClearLogo.WebImage.Clear()
                ImageResultsContainer.ShowClearLogo.WebImage.FromWeb(ImageResultsContainer.ShowClearLogo.URL)
                If ImageResultsContainer.ShowClearLogo.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowClearLogo.LocalFile).FullName)
                    ImageResultsContainer.ShowClearLogo.WebImage.Save(ImageResultsContainer.ShowClearLogo.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.ShowClearLogo.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.ShowClearLogo.WebImage
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowFanart OrElse Me._type = Enums.ImageType_TV.EpisodeFanart) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.ShowFanart.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowFanart.LocalFile) Then
                ImageResultsContainer.ShowFanart.WebImage.FromFile(ImageResultsContainer.ShowFanart.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.ShowFanart.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowFanart.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowFanart.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowFanart.LocalFile) Then
                ImageResultsContainer.ShowFanart.WebImage.Clear()
                ImageResultsContainer.ShowFanart.WebImage.FromWeb(ImageResultsContainer.ShowFanart.URL)
                If ImageResultsContainer.ShowFanart.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowFanart.LocalFile).FullName)
                    ImageResultsContainer.ShowFanart.WebImage.Save(ImageResultsContainer.ShowFanart.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.ShowFanart.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.ShowFanart.WebImage
                End If
            End If
        ElseIf Me._type = Enums.ImageType_TV.ShowLandscape Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.ShowLandscape.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowLandscape.LocalFile) Then
                ImageResultsContainer.ShowLandscape.WebImage.FromFile(ImageResultsContainer.ShowLandscape.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.ShowLandscape.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowLandscape.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowLandscape.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowLandscape.LocalFile) Then
                ImageResultsContainer.ShowLandscape.WebImage.Clear()
                ImageResultsContainer.ShowLandscape.WebImage.FromWeb(ImageResultsContainer.ShowLandscape.URL)
                If ImageResultsContainer.ShowLandscape.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowLandscape.LocalFile).FullName)
                    ImageResultsContainer.ShowLandscape.WebImage.Save(ImageResultsContainer.ShowLandscape.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.ShowLandscape.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.ShowLandscape.WebImage
                End If
            End If
        ElseIf (Me._type = Enums.ImageType_TV.ShowPoster OrElse Me._type = Enums.ImageType_TV.EpisodePoster) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True
            If Not String.IsNullOrEmpty(ImageResultsContainer.ShowPoster.LocalFile) AndAlso File.Exists(ImageResultsContainer.ShowPoster.LocalFile) Then
                ImageResultsContainer.ShowPoster.WebImage.FromFile(ImageResultsContainer.ShowPoster.LocalFile)
                Me.pbCurrent.Image = ImageResultsContainer.ShowPoster.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowPoster.WebImage
            ElseIf Not String.IsNullOrEmpty(ImageResultsContainer.ShowPoster.URL) AndAlso Not String.IsNullOrEmpty(ImageResultsContainer.ShowPoster.LocalFile) Then
                ImageResultsContainer.ShowPoster.WebImage.Clear()
                ImageResultsContainer.ShowPoster.WebImage.FromWeb(ImageResultsContainer.ShowPoster.URL)
                If ImageResultsContainer.ShowPoster.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(ImageResultsContainer.ShowPoster.LocalFile).FullName)
                    ImageResultsContainer.ShowPoster.WebImage.Save(ImageResultsContainer.ShowPoster.LocalFile)
                    Me.pbCurrent.Image = ImageResultsContainer.ShowPoster.WebImage.Image
                    Me.pbCurrent.Tag = ImageResultsContainer.ShowPoster.WebImage
                End If
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    Private Sub bwLoadData_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadData.DoWork
        Dim cSI As MediaContainers.SeasonImagesContainer
        Dim iProgress As Integer = 1
        Dim iSeason As Integer = -1

        Me.bwLoadData.ReportProgress(tmpShowContainer.Episodes.Count, "current")

        If Me.bwLoadData.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Select Case Me._type
            Case Enums.ImageType_TV.AllSeasonsBanner
                ImageResultsContainer.SeasonBanner = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.AllSeasonsFanart
                ImageResultsContainer.SeasonFanart = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.AllSeasonsLandscape
                ImageResultsContainer.SeasonLandscape = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.AllSeasonsPoster
                ImageResultsContainer.SeasonPoster = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.SeasonPoster
                cSI = New MediaContainers.SeasonImagesContainer
                cSI.Season = Me._season
                cSI.Poster = CType(Me.pbCurrent.Tag, MediaContainers.Image)
                ImageResultsContainer.SeasonImages.Add(cSI)
            Case Enums.ImageType_TV.SeasonBanner
                cSI = New MediaContainers.SeasonImagesContainer
                cSI.Season = Me._season
                cSI.Banner = CType(Me.pbCurrent.Tag, MediaContainers.Image)
                ImageResultsContainer.SeasonImages.Add(cSI)
            Case Enums.ImageType_TV.SeasonFanart
                cSI = New MediaContainers.SeasonImagesContainer
                cSI.Season = Me._season
                cSI.Fanart = CType(Me.pbCurrent.Tag, MediaContainers.Image)
                ImageResultsContainer.SeasonImages.Add(cSI)
            Case Enums.ImageType_TV.SeasonLandscape
                cSI = New MediaContainers.SeasonImagesContainer
                cSI.Season = Me._season
                cSI.Landscape = CType(Me.pbCurrent.Tag, MediaContainers.Image)
                ImageResultsContainer.SeasonImages.Add(cSI)
            Case Enums.ImageType_TV.ShowBanner
                ImageResultsContainer.ShowBanner = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.ShowCharacterArt
                ImageResultsContainer.ShowCharacterArt = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.ShowClearArt
                ImageResultsContainer.ShowClearArt = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.ShowClearLogo
                ImageResultsContainer.ShowClearLogo = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.ShowFanart, Enums.ImageType_TV.EpisodeFanart
                ImageResultsContainer.ShowFanart = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.ShowLandscape
                ImageResultsContainer.ShowLandscape = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.ShowPoster
                ImageResultsContainer.ShowPoster = CType(Me.pbCurrent.Tag, MediaContainers.Image)
            Case Enums.ImageType_TV.All
                If _withcurrent Then
                    If Master.eSettings.TVShowBannerAnyEnabled AndAlso Me.tmpShowContainer.Show.ImagesContainer.ShowBanner.WebImage.Image IsNot Nothing Then
                        ImageResultsContainer.ShowBanner = Me.tmpShowContainer.Show.ImagesContainer.ShowBanner
                        DefaultImagesContainer.ShowBanner = Me.tmpShowContainer.Show.ImagesContainer.ShowBanner
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso Me.tmpShowContainer.Show.ImagesContainer.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                        ImageResultsContainer.ShowCharacterArt = Me.tmpShowContainer.Show.ImagesContainer.ShowCharacterArt
                        DefaultImagesContainer.ShowCharacterArt = Me.tmpShowContainer.Show.ImagesContainer.ShowCharacterArt
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearArtAnyEnabled AndAlso Me.tmpShowContainer.Show.ImagesContainer.ShowClearArt.WebImage.Image IsNot Nothing Then
                        ImageResultsContainer.ShowClearArt = Me.tmpShowContainer.Show.ImagesContainer.ShowClearArt
                        DefaultImagesContainer.ShowClearArt = Me.tmpShowContainer.Show.ImagesContainer.ShowClearArt
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowClearLogoAnyEnabled AndAlso Me.tmpShowContainer.Show.ImagesContainer.ShowClearLogo.WebImage.Image IsNot Nothing Then
                        ImageResultsContainer.ShowClearLogo = Me.tmpShowContainer.Show.ImagesContainer.ShowClearLogo
                        DefaultImagesContainer.ShowClearLogo = Me.tmpShowContainer.Show.ImagesContainer.ShowClearLogo
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowFanartAnyEnabled AndAlso Me.tmpShowContainer.Show.ImagesContainer.ShowFanart.WebImage.Image IsNot Nothing Then
                        ImageResultsContainer.ShowFanart = Me.tmpShowContainer.Show.ImagesContainer.ShowFanart
                        DefaultImagesContainer.ShowFanart = Me.tmpShowContainer.Show.ImagesContainer.ShowFanart
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowLandscapeAnyEnabled AndAlso Me.tmpShowContainer.Show.ImagesContainer.ShowLandscape.WebImage.Image IsNot Nothing Then
                        ImageResultsContainer.ShowLandscape = Me.tmpShowContainer.Show.ImagesContainer.ShowLandscape
                        DefaultImagesContainer.ShowLandscape = Me.tmpShowContainer.Show.ImagesContainer.ShowLandscape
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowPosterAnyEnabled AndAlso Me.tmpShowContainer.Show.ImagesContainer.ShowPoster.WebImage.Image IsNot Nothing Then
                        ImageResultsContainer.ShowPoster = Me.tmpShowContainer.Show.ImagesContainer.ShowPoster
                        DefaultImagesContainer.ShowPoster = Me.tmpShowContainer.Show.ImagesContainer.ShowPoster
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpShowContainer.AllSeason.BannerPath) Then
                        ImageResultsContainer.SeasonBanner = Me.tmpShowContainer.Show.ImagesContainer.SeasonBanner
                        DefaultImagesContainer.SeasonBanner = Me.tmpShowContainer.Show.ImagesContainer.SeasonBanner
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpShowContainer.AllSeason.FanartPath) Then
                        ImageResultsContainer.SeasonFanart = Me.tmpShowContainer.Show.ImagesContainer.SeasonFanart
                        DefaultImagesContainer.SeasonFanart = Me.tmpShowContainer.Show.ImagesContainer.SeasonFanart
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpShowContainer.AllSeason.LandscapePath) Then
                        ImageResultsContainer.SeasonLandscape = Me.tmpShowContainer.Show.ImagesContainer.SeasonLandscape
                        DefaultImagesContainer.SeasonLandscape = Me.tmpShowContainer.Show.ImagesContainer.SeasonLandscape
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVASPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(tmpShowContainer.AllSeason.PosterPath) Then
                        ImageResultsContainer.SeasonPoster = Me.tmpShowContainer.Show.ImagesContainer.SeasonPoster
                        DefaultImagesContainer.SeasonPoster = Me.tmpShowContainer.Show.ImagesContainer.SeasonPoster
                    End If
                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    For Each sEpisode As Structures.DBTV In tmpShowContainer.Episodes
                        Try
                            iSeason = sEpisode.TVEp.Season
                            If iSeason > -1 Then
                                If Master.eSettings.TVEpisodePosterAnyEnabled AndAlso ImageResultsContainer.ShowPoster.WebImage Is Nothing AndAlso Not String.IsNullOrEmpty(sEpisode.PosterPath) Then
                                    ImageResultsContainer.ShowPoster.WebImage.FromFile(sEpisode.PosterPath)
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If

                                If Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso ImageResultsContainer.ShowFanart.WebImage.Image Is Nothing AndAlso Not String.IsNullOrEmpty(sEpisode.FanartPath) Then
                                    ImageResultsContainer.ShowFanart.WebImage.FromFile(sEpisode.FanartPath)
                                    ImageResultsContainer.ShowFanart.LocalFile = sEpisode.FanartPath
                                End If

                                If Me.bwLoadData.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If

                                If ImageResultsContainer.SeasonImages.Where(Function(s) s.Season = iSeason).Count = 0 Then
                                    cSI = New MediaContainers.SeasonImagesContainer
                                    cSI.Season = iSeason
                                    If Master.eSettings.TVSeasonBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.BannerPath) Then
                                        cSI.Banner.WebImage.FromFile(sEpisode.BannerPath)
                                        cSI.Banner.LocalFile = sEpisode.BannerPath
                                    End If
                                    If Master.eSettings.TVSeasonFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.FanartPath) Then
                                        cSI.Fanart.WebImage.FromFile(sEpisode.FanartPath)
                                        cSI.Fanart.LocalFile = sEpisode.FanartPath
                                    End If
                                    If Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.LandscapePath) Then
                                        cSI.Landscape.WebImage.FromFile(sEpisode.LandscapePath)
                                        cSI.Landscape.LocalFile = sEpisode.LandscapePath
                                    End If
                                    If Master.eSettings.TVSeasonPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.PosterPath) Then
                                        cSI.Poster.WebImage.FromFile(sEpisode.PosterPath)
                                        cSI.Poster.LocalFile = sEpisode.PosterPath
                                    End If
                                    ImageResultsContainer.SeasonImages.Add(cSI)
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
                    For Each sEpisode As Structures.DBTV In tmpShowContainer.Episodes
                        Try
                            iSeason = sEpisode.TVEp.Season

                            If ImageResultsContainer.SeasonImages.Where(Function(s) s.Season = iSeason).Count = 0 Then
                                cSI = New MediaContainers.SeasonImagesContainer
                                cSI.Season = iSeason
                                ImageResultsContainer.SeasonImages.Add(cSI)
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

    Public Overloads Function ShowDialog(ByRef DBTV As Structures.DBTV, ByVal Type As Enums.ImageType_TV, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_TV, Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.SearchResultsContainer = ImagesContainer
        Me.tmpShowContainer.Show = DBTV
        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgTVImageSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.bwLoadData.WorkerReportsProgress = True
        Me.bwLoadData.WorkerSupportsCancellation = True
        Me.bwLoadData.RunWorkerAsync()
    End Sub

    Private Sub DoSelect(ByVal iIndex As Integer, ByVal SelTag As MediaContainers.Image)
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

        Me.bwLoadImages.ReportProgress(tmpShowContainer.Episodes.Count + SearchResultsContainer.ShowFanarts.Count + SearchResultsContainer.ShowPosters.Count + _
                                           SearchResultsContainer.SeasonBanners.Count + SearchResultsContainer.SeasonLandscapes.Count + SearchResultsContainer.SeasonPosters.Count + _
                                           SearchResultsContainer.ShowBanners.Count + SearchResultsContainer.ShowCharacterArts.Count + SearchResultsContainer.ShowClearArts.Count + _
                                           SearchResultsContainer.ShowClearLogos.Count + SearchResultsContainer.ShowLandscapes.Count + SearchResultsContainer.SeasonPosters.Count, "max")

        'Banner AllSeasons/Show 
        For Each img In SearchResultsContainer.ShowBanners
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showbanners", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Banner Season
        For Each img In SearchResultsContainer.SeasonBanners
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonbanners", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'CharacterArt Show
        For Each img In SearchResultsContainer.ShowCharacterArts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcharacterarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcharacterarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'ClearArt Show
        For Each img In SearchResultsContainer.ShowClearArts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcleararts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showcleararts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'ClearLogo Show 
        For Each img In SearchResultsContainer.ShowClearLogos
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showclearlogos", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showclearlogos\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Fanart AllSeasons/Season/Show 
        For Each img In SearchResultsContainer.ShowFanarts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showfanarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showfanarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Landscape AllSeasons/Show 
        For Each img In SearchResultsContainer.ShowLandscapes
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Landscape Season 
        For Each img In SearchResultsContainer.SeasonLandscapes
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Poster AllSeasons/Show 
        For Each img In SearchResultsContainer.ShowPosters
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showposters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "showposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Poster Season 
        For Each img In SearchResultsContainer.SeasonPosters
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonposters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, tmpShowContainer.Show.TVShow.TVDBID, Path.DirectorySeparatorChar, "seasonposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next


        'Episode Poster
        If Me._type = Enums.ImageType_TV.All Then
            For Each Epi As Structures.DBTV In tmpShowContainer.Episodes
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.SeasonPosters
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.SeasonBanners
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.SeasonLandscapes
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowPosters
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowBanners
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowCharacterArts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowClearArts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowClearLogos
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowLandscapes
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                If File.Exists(tImg.LocalThumb) Then
                    tImg.WebImage.FromFile(tImg.LocalThumb)
                ElseIf File.Exists(tImg.LocalFile) AndAlso String.IsNullOrEmpty(tImg.ThumbURL) Then
                    tImg.WebImage.FromFile(tImg.LocalFile)
                Else
                    If Not String.IsNullOrEmpty(tImg.ThumbURL) Then
                        tImg.WebImage.FromWeb(tImg.ThumbURL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalThumb).FullName)
                            tImg.WebImage.Save(tImg.LocalThumb)
                        End If
                    ElseIf Not String.IsNullOrEmpty(tImg.URL) Then
                        tImg.WebImage.FromWeb(tImg.URL)
                        If tImg.WebImage.Image IsNot Nothing Then
                            Directory.CreateDirectory(Directory.GetParent(tImg.LocalFile).FullName)
                            tImg.WebImage.Save(tImg.LocalFile)
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

    Private Sub DownloadFullsizeImage(ByVal iTag As MediaContainers.Image, ByRef tImage As Images)
        Dim sHTTP As New HTTP

        If Not String.IsNullOrEmpty(iTag.LocalFile) AndAlso File.Exists(iTag.LocalFile) Then
            tImage.FromFile(iTag.LocalFile)
        ElseIf Not String.IsNullOrEmpty(iTag.LocalFile) AndAlso Not String.IsNullOrEmpty(iTag.URL) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True

            Application.DoEvents()

            tImage.FromWeb(iTag.URL)
            If tImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(iTag.LocalFile).FullName)
                tImage.Save(iTag.LocalFile)
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
            For Each cSeason As MediaContainers.SeasonImagesContainer In ImageResultsContainer.SeasonImages.OrderBy(Function(s) s.Season)
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
        Me.DoSelect(iindex, DirectCast(DirectCast(sender, Label).Tag, MediaContainers.Image))
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
        Me.SetImage(New MediaContainers.Image)
    End Sub

    Private Sub pbImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pbImage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tImages As New Images
        Dim iTag As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        DownloadFullsizeImage(iTag, tImages)

        ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImages.Image)
    End Sub

    Private Sub pbUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbUndo.Click
        If Me.SelSeason = -999 Then
            If SelImgType = Enums.ImageType_TV.ShowBanner Then
                ImageResultsContainer.ShowBanner = DefaultImagesContainer.ShowBanner
                Me.pbCurrent.Image = ImageResultsContainer.ShowBanner.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowBanner
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                ImageResultsContainer.ShowCharacterArt = DefaultImagesContainer.ShowCharacterArt
                Me.pbCurrent.Image = ImageResultsContainer.ShowCharacterArt.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowCharacterArt
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                ImageResultsContainer.ShowClearArt = DefaultImagesContainer.ShowClearArt
                Me.pbCurrent.Image = ImageResultsContainer.ShowClearArt.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowClearArt
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                ImageResultsContainer.ShowClearLogo = DefaultImagesContainer.ShowClearLogo
                Me.pbCurrent.Image = ImageResultsContainer.ShowClearLogo.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowClearLogo
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                ImageResultsContainer.ShowFanart = DefaultImagesContainer.ShowFanart
                Me.pbCurrent.Image = ImageResultsContainer.ShowFanart.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowFanart
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                ImageResultsContainer.ShowPoster = DefaultImagesContainer.ShowPoster
                Me.pbCurrent.Image = ImageResultsContainer.ShowPoster.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.ShowPoster
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                ImageResultsContainer.SeasonBanner = DefaultImagesContainer.SeasonBanner
                Me.pbCurrent.Image = ImageResultsContainer.SeasonBanner.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonBanner
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                ImageResultsContainer.SeasonFanart = DefaultImagesContainer.SeasonFanart
                Me.pbCurrent.Image = ImageResultsContainer.SeasonFanart.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonFanart
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                ImageResultsContainer.SeasonLandscape = DefaultImagesContainer.SeasonLandscape
                Me.pbCurrent.Image = ImageResultsContainer.SeasonLandscape.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonLandscape
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                ImageResultsContainer.SeasonPoster = DefaultImagesContainer.SeasonPoster
                Me.pbCurrent.Image = ImageResultsContainer.SeasonPoster.WebImage.Image
                Me.pbCurrent.Tag = ImageResultsContainer.SeasonPoster
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                Dim dSPost As MediaContainers.Image = DefaultImagesContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost

                'TVDBImages.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner

                'TVDBImages.SeasonImages..Image = DefaultImages.AllSeasonsBanner.Image
                'TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                'TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                'Me.pbCurrent.Image = TVDBImages.AllSeasonsBanner.WebImage.Image
                'Me.pbCurrent.Tag = TVDBImages.AllSeasonsBanner.Image

            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                Dim dSFan As MediaContainers.Image = DefaultImagesContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                Dim tSFan As MediaContainers.Image = ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tSFan.WebImage = dSFan.WebImage
                tSFan.LocalFile = dSFan.LocalFile
                tSFan.URL = dSFan.URL
                Me.pbCurrent.Image = dSFan.WebImage.Image
                Me.pbCurrent.Tag = dSFan
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                Dim dSPost As MediaContainers.Image = DefaultImagesContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape
                ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                Dim dSPost As MediaContainers.Image = DefaultImagesContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster = dSPost
                Me.pbCurrent.Image = dSPost.WebImage.Image
                Me.pbCurrent.Tag = dSPost
            End If
        End If
    End Sub

    Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelect(iIndex, DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub SetImage(ByVal SelTag As MediaContainers.Image)
        If SelTag.WebImage.Image IsNot Nothing Then
            Me.pbCurrent.Image = SelTag.WebImage.Image
            Me.pbCurrent.Tag = SelTag
        End If

        If Me.SelSeason = -999 Then
            If SelImgType = Enums.ImageType_TV.ShowBanner Then
                ImageResultsContainer.ShowBanner = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                ImageResultsContainer.ShowCharacterArt = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                ImageResultsContainer.ShowClearArt = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                ImageResultsContainer.ShowClearLogo = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowLandscape Then
                ImageResultsContainer.ShowLandscape = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                ImageResultsContainer.ShowFanart = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                ImageResultsContainer.ShowPoster = SelTag
            End If
        ElseIf Me.SelSeason = 999 Then
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner Then
                ImageResultsContainer.SeasonBanner = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart Then
                ImageResultsContainer.SeasonFanart = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape Then
                ImageResultsContainer.SeasonLandscape = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster Then
                ImageResultsContainer.SeasonPoster = SelTag
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                ImageResultsContainer.SeasonImages.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster = SelTag
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
                If ImageResultsContainer.ShowBanner IsNot Nothing AndAlso ImageResultsContainer.ShowBanner.WebImage IsNot Nothing AndAlso ImageResultsContainer.ShowBanner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.ShowBanner.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowBanners
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'Show Characterart
            ElseIf e.Node.Tag.ToString = "showch" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowCharacterArt
                If ImageResultsContainer.ShowCharacterArt IsNot Nothing AndAlso ImageResultsContainer.ShowCharacterArt.WebImage IsNot Nothing AndAlso ImageResultsContainer.ShowCharacterArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.ShowCharacterArt.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowCharacterArts
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'Show ClearArt
            ElseIf e.Node.Tag.ToString = "showca" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearArt
                If ImageResultsContainer.ShowClearArt IsNot Nothing AndAlso ImageResultsContainer.ShowClearArt.WebImage IsNot Nothing AndAlso ImageResultsContainer.ShowClearArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.ShowClearArt.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowClearArts
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'Show ClearLogo
            ElseIf e.Node.Tag.ToString = "showcl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearLogo
                If ImageResultsContainer.ShowClearLogo IsNot Nothing AndAlso ImageResultsContainer.ShowClearLogo.WebImage IsNot Nothing AndAlso ImageResultsContainer.ShowClearLogo.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.ShowClearLogo.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowClearLogos
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'Show Fanart
            ElseIf e.Node.Tag.ToString = "showf" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowFanart
                If ImageResultsContainer.ShowFanart IsNot Nothing AndAlso ImageResultsContainer.ShowFanart.WebImage IsNot Nothing AndAlso ImageResultsContainer.ShowFanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.ShowFanart.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'Show Landscape
            ElseIf e.Node.Tag.ToString = "showl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowLandscape
                If ImageResultsContainer.ShowLandscape IsNot Nothing AndAlso ImageResultsContainer.ShowLandscape.WebImage IsNot Nothing AndAlso ImageResultsContainer.ShowLandscape.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.ShowLandscape.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowLandscapes
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'Show Poster
            ElseIf e.Node.Tag.ToString = "showp" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowPoster
                If ImageResultsContainer.ShowPoster IsNot Nothing AndAlso ImageResultsContainer.ShowPoster.WebImage IsNot Nothing AndAlso ImageResultsContainer.ShowPoster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.ShowPoster.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowPosters
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'AllSeasons Banner
            ElseIf e.Node.Tag.ToString = "allb" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsBanner
                If ImageResultsContainer.SeasonBanner IsNot Nothing AndAlso ImageResultsContainer.SeasonBanner.WebImage IsNot Nothing AndAlso ImageResultsContainer.SeasonBanner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonBanner.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = 999)
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowBanners
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'AllSeasons Fanart
            ElseIf e.Node.Tag.ToString = "allf" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsFanart
                If ImageResultsContainer.SeasonFanart IsNot Nothing AndAlso ImageResultsContainer.SeasonFanart.WebImage IsNot Nothing AndAlso ImageResultsContainer.SeasonFanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonFanart.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'AllSeasons Landscape
            ElseIf e.Node.Tag.ToString = "alll" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsLandscape
                If ImageResultsContainer.SeasonLandscape IsNot Nothing AndAlso ImageResultsContainer.SeasonLandscape.WebImage IsNot Nothing AndAlso ImageResultsContainer.SeasonLandscape.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonLandscape.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = 999)
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowLandscapes
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next

                'AllSeasons Poster
            ElseIf e.Node.Tag.ToString = "allp" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsPoster
                If ImageResultsContainer.SeasonPoster IsNot Nothing AndAlso ImageResultsContainer.SeasonPoster.WebImage IsNot Nothing AndAlso ImageResultsContainer.SeasonPoster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = ImageResultsContainer.SeasonPoster.WebImage.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = 999)
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
                    End If
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowPosters
                    If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                        Dim imgText As String = String.Empty
                        If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                            imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                        Else
                            imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                        End If
                        Me.AddImage(imgText, iCount, tvImage)
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
                        Dim tBanner As MediaContainers.SeasonImagesContainer = ImageResultsContainer.SeasonImages.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If tBanner IsNot Nothing AndAlso tBanner.Banner IsNot Nothing AndAlso tBanner.Banner.WebImage IsNot Nothing Then
                            Me.pbCurrent.Image = tBanner.Banner.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonBanners.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, tvImage)
                            End If
                            iCount += 1
                        Next

                        'Season Fanart
                    ElseIf tMatch.Groups("type").Value = "f" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonFanart
                        Dim tFanart As MediaContainers.SeasonImagesContainer = ImageResultsContainer.SeasonImages.FirstOrDefault(Function(f) f.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                        If tFanart IsNot Nothing AndAlso tFanart.Fanart IsNot Nothing AndAlso tFanart.Fanart.WebImage IsNot Nothing AndAlso tFanart.Fanart.WebImage.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tFanart.Fanart.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, tvImage)
                            End If
                            iCount += 1
                        Next

                        'Season Landscape
                    ElseIf tMatch.Groups("type").Value = "l" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonLandscape
                        Dim tLandscape As MediaContainers.SeasonImagesContainer = ImageResultsContainer.SeasonImages.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If tLandscape IsNot Nothing AndAlso tLandscape.Landscape IsNot Nothing AndAlso tLandscape.Landscape.WebImage IsNot Nothing Then
                            Me.pbCurrent.Image = tLandscape.Landscape.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonLandscapes.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, tvImage)
                            End If
                            iCount += 1
                        Next

                        'Season Poster
                    ElseIf tMatch.Groups("type").Value = "p" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonPoster
                        Dim tPoster As MediaContainers.SeasonImagesContainer = ImageResultsContainer.SeasonImages.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                        If tPoster IsNot Nothing AndAlso tPoster.Poster IsNot Nothing AndAlso tPoster.Poster.WebImage IsNot Nothing Then
                            Me.pbCurrent.Image = tPoster.Poster.WebImage.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonPosters.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If tvImage IsNot Nothing AndAlso tvImage.WebImage IsNot Nothing AndAlso tvImage.WebImage.Image IsNot Nothing Then
                                Dim imgText As String = String.Empty
                                If CDbl(tvImage.Width) = 0 OrElse CDbl(tvImage.Height) = 0 Then
                                    imgText = String.Format("{0}x{1}", tvImage.WebImage.Image.Size.Width, tvImage.WebImage.Image.Size.Height & Environment.NewLine & tvImage.LongLang)
                                Else
                                    imgText = String.Format("{0}x{1}", tvImage.Width, tvImage.Height & Environment.NewLine & tvImage.LongLang)
                                End If
                                Me.AddImage(imgText, iCount, tvImage)
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