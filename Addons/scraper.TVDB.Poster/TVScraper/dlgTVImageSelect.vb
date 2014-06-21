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
    Private SelIsPoster As Boolean = True
    Private SelIsBanner As Boolean = True
    Private SelIsLandscape As Boolean = True
    Private SelSeason As Integer = -999
    Private ShowBannerList As New List(Of Scraper.TVDBShowBanner)
    Private ShowPosterList As New List(Of Scraper.TVDBPoster)
    Private _fanartchanged As Boolean = False
    Private _id As Integer = -1
    Private _season As Integer = -999
    Private _type As Enums.TVImageType = Enums.TVImageType.All
    Private _withcurrent As Boolean = True
    Private _ScrapeType As Enums.ScrapeType

#End Region 'Fields

#Region "Methods"

    Public Function SetDefaults() As Boolean
        Dim iSeason As Integer = -1
        Dim iEpisode As Integer = -1
        Dim iProgress As Integer = 4

        Dim tSeaP As Scraper.TVDBSeasonPoster
        Dim tSeaB As Scraper.TVDBSeasonBanner
        Dim tSeaF As Scraper.TVDBFanart

        Try
            Me.bwLoadImages.ReportProgress(Scraper.TVDBImages.SeasonImageList.Count + Scraper.tmpTVDBShow.Episodes.Count + 3, "defaults")

            'AllSeason Banner
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image.Image) Then
                Dim tSP As Scraper.TVDBShowBanner = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVASBannerPrefType AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))

                If CBool(AdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) Then
                    If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))
                End If

                If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVASBannerPrefType)

                'no preferred size, just get any one of them
                If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

                If Not IsNothing(tSP) Then
                    Scraper.TVDBImages.AllSeasonsBanner.Image = tSP.Image
                    Scraper.TVDBImages.AllSeasonsBanner.LocalFile = tSP.LocalFile
                    Scraper.TVDBImages.AllSeasonsBanner.URL = tSP.URL
                End If
            End If

            If Me.bwLoadImages.CancellationPending Then
                Return True
            End If
            Me.bwLoadImages.ReportProgress(3, "progress")

            'AllSeason Fanart
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image.Image) Then
                Dim tSF As Scraper.TVDBFanart = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVASFanartPrefSize AndAlso f.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))

                If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVASFanartPrefSize)

                'no fanart of the preferred size, just get the first available
                If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image))

                If Not IsNothing(tSF) Then
                    If Not String.IsNullOrEmpty(tSF.LocalFile) AndAlso File.Exists(tSF.LocalFile) Then
                        Scraper.TVDBImages.AllSeasonsFanart.Image.FromFile(tSF.LocalFile)
                        Scraper.TVDBImages.AllSeasonsFanart.LocalFile = tSF.LocalFile
                        Scraper.TVDBImages.AllSeasonsFanart.URL = tSF.URL
                    ElseIf Not String.IsNullOrEmpty(tSF.LocalFile) AndAlso Not String.IsNullOrEmpty(tSF.URL) Then
                        Scraper.TVDBImages.AllSeasonsFanart.Image.FromWeb(tSF.URL)
                        If Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tSF.LocalFile).FullName)
                            Scraper.TVDBImages.AllSeasonsFanart.Image.Save(tSF.LocalFile, , , False)
                            Scraper.TVDBImages.AllSeasonsFanart.LocalFile = tSF.LocalFile
                            Scraper.TVDBImages.AllSeasonsFanart.URL = tSF.URL
                        End If
                    End If
                End If
            End If

            If Me.bwLoadImages.CancellationPending Then
                Return True
            End If
            Me.bwLoadImages.ReportProgress(1, "progress")

            'AllSeason Poster
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image.Image) Then
                Dim tSPg As Scraper.TVDBPoster = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVASPosterPrefSize AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))

                If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVASPosterPrefSize)

                'no preferred size, just get any one of them
                If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

                If Not IsNothing(tSPg) Then
                    Scraper.TVDBImages.AllSeasonsPoster.Image = tSPg.Image
                    Scraper.TVDBImages.AllSeasonsPoster.LocalFile = tSPg.LocalFile
                    Scraper.TVDBImages.AllSeasonsPoster.URL = tSPg.URL
                End If
            End If

            If Me.bwLoadImages.CancellationPending Then
                Return True
            End If
            Me.bwLoadImages.ReportProgress(3, "progress")

            'Show Banner
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowBanner.Image.Image) Then
                Dim tSP As Scraper.TVDBShowBanner = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVShowBannerPrefType AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))

                If CBool(AdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) Then
                    If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))
                End If

                If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Type = Master.eSettings.TVShowBannerPrefType)

                'no preferred size, just get any one of them
                If IsNothing(tSP) Then tSP = ShowBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

                If Not IsNothing(tSP) Then
                    Scraper.TVDBImages.ShowBanner.Image = tSP.Image
                    Scraper.TVDBImages.ShowBanner.LocalFile = tSP.LocalFile
                    Scraper.TVDBImages.ShowBanner.URL = tSP.URL
                End If
            End If

            If Me.bwLoadImages.CancellationPending Then
                Return True
            End If
            Me.bwLoadImages.ReportProgress(1, "progress")

            'Show Fanart
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowFanart OrElse Me._type = Enums.TVImageType.EpisodeFanart) AndAlso IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then 'TODO: add *FanartEnabled check
                Dim tSF As Scraper.TVDBFanart = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVShowFanartPrefSize AndAlso f.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))

                If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVShowFanartPrefSize)

                'no fanart of the preferred size, just get the first available
                If IsNothing(tSF) Then tSF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image))

                If Not IsNothing(tSF) Then
                    If Not String.IsNullOrEmpty(tSF.LocalFile) AndAlso File.Exists(tSF.LocalFile) Then
                        Scraper.TVDBImages.ShowFanart.Image.FromFile(tSF.LocalFile)
                        Scraper.TVDBImages.ShowFanart.LocalFile = tSF.LocalFile
                        Scraper.TVDBImages.ShowFanart.URL = tSF.URL
                    ElseIf Not String.IsNullOrEmpty(tSF.LocalFile) AndAlso Not String.IsNullOrEmpty(tSF.URL) Then
                        Scraper.TVDBImages.ShowFanart.Image.FromWeb(tSF.URL)
                        If Not IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                            Directory.CreateDirectory(Directory.GetParent(tSF.LocalFile).FullName)
                            Scraper.TVDBImages.ShowFanart.Image.Save(tSF.LocalFile, , , False)
                            Scraper.TVDBImages.ShowFanart.LocalFile = tSF.LocalFile
                            Scraper.TVDBImages.ShowFanart.URL = tSF.URL
                        End If
                    End If
                End If
            End If

            If Me.bwLoadImages.CancellationPending Then
                Return True
            End If
            Me.bwLoadImages.ReportProgress(2, "progress")

            'Show Poster
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled AndAlso IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image) Then
                Dim tSPg As Scraper.TVDBPoster = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVShowPosterPrefSize AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))

                If CBool(AdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) Then
                    If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))
                End If

                If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso Me.GetPosterDims(p.Size) = Master.eSettings.TVShowPosterPrefSize)

                'no preferred size, just get any one of them
                If IsNothing(tSPg) Then tSPg = GenericPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image))

                If Not IsNothing(tSPg) Then
                    Scraper.TVDBImages.ShowPoster.Image = tSPg.Image
                    Scraper.TVDBImages.ShowPoster.LocalFile = tSPg.LocalFile
                    Scraper.TVDBImages.ShowPoster.URL = tSPg.URL
                End If
            End If

            If Me.bwLoadImages.CancellationPending Then
                Return True
            End If
            Me.bwLoadImages.ReportProgress(1, "progress")

            'Season Banner/Fanart/Poster
            If Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.SeasonPoster OrElse Me._type = Enums.TVImageType.SeasonBanner OrElse Me._type = Enums.TVImageType.SeasonFanart Then
                For Each cSeason As Scraper.TVDBSeasonImage In Scraper.TVDBImages.SeasonImageList
                    Try
                        iSeason = cSeason.Season

                        'Season Banner
                        If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.SeasonBanner) AndAlso Master.eSettings.TVSeasonBannerAnyEnabled AndAlso IsNothing(cSeason.Banner.Image) Then
                            tSeaB = SeasonBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonPosterPrefSize AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))
                            If IsNothing(tSeaB) Then tSeaB = SeasonBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonBannerPrefType)
                            If IsNothing(tSeaB) Then tSeaB = SeasonBannerList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason)
                            If Not IsNothing(tSeaB) Then cSeason.Banner = tSeaB.Image
                        End If

                        'Season Fanart
                        If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.SeasonFanart) AndAlso Master.eSettings.TVSeasonFanartAnyEnabled AndAlso IsNothing(cSeason.Fanart.Image.Image) Then
                            tSeaF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVSeasonFanartPrefSize AndAlso f.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))
                            If IsNothing(tSeaF) Then tSeaF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image) AndAlso Me.GetFanartDims(f.Size) = Master.eSettings.TVSeasonFanartPrefSize)
                            If IsNothing(tSeaF) Then tSeaF = GenericFanartList.FirstOrDefault(Function(f) Not IsNothing(f.Image.Image))
                            If Not IsNothing(tSeaF) Then
                                If Not String.IsNullOrEmpty(tSeaF.LocalFile) AndAlso File.Exists(tSeaF.LocalFile) Then
                                    cSeason.Fanart.Image.FromFile(tSeaF.LocalFile)
                                    cSeason.Fanart.LocalFile = tSeaF.LocalFile
                                    cSeason.Fanart.URL = tSeaF.URL
                                ElseIf Not String.IsNullOrEmpty(tSeaF.LocalFile) AndAlso Not String.IsNullOrEmpty(tSeaF.URL) Then
                                    cSeason.Fanart.Image.FromWeb(tSeaF.URL)
                                    If Not IsNothing(cSeason.Fanart.Image.Image) Then
                                        Directory.CreateDirectory(Directory.GetParent(tSeaF.LocalFile).FullName)
                                        cSeason.Fanart.Image.Save(tSeaF.LocalFile, , , False)
                                        cSeason.Fanart.LocalFile = tSeaF.LocalFile
                                        cSeason.Fanart.URL = tSeaF.URL
                                    End If
                                End If
                            End If
                        End If

                        'Season Poster
                        If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.SeasonPoster) AndAlso Master.eSettings.TVSeasonPosterAnyEnabled AndAlso IsNothing(cSeason.Poster.Image) Then
                            tSeaP = SeasonPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonPosterPrefSize AndAlso p.Language = AdvancedSettings.GetSetting("TVDBLang", "en"))
                            If IsNothing(tSeaP) Then tSeaP = SeasonPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason AndAlso p.Type = Master.eSettings.TVSeasonPosterPrefSize)
                            If IsNothing(tSeaP) Then tSeaP = SeasonPosterList.FirstOrDefault(Function(p) Not IsNothing(p.Image.Image) AndAlso p.Season = iSeason)
                            If Not IsNothing(tSeaP) Then cSeason.Poster = tSeaP.Image
                        End If

                        If Me.bwLoadImages.CancellationPending Then
                            Return True
                        End If
                        Me.bwLoadImages.ReportProgress(iProgress, "progress")
                        iProgress += 1
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

            'Episode Fanart/Poster
            If Me._type = Enums.TVImageType.All Then
                For Each Episode As Structures.DBTV In Scraper.tmpTVDBShow.Episodes
                    Try
                        If Master.eSettings.TVEpisodePosterAnyEnabled Then
                            If Not String.IsNullOrEmpty(Episode.TVEp.LocalFile) Then
                                Episode.TVEp.Poster.FromFile(Episode.TVEp.LocalFile)
                            ElseIf Not String.IsNullOrEmpty(Episode.EpPosterPath) Then
                                Episode.TVEp.Poster.FromFile(Episode.EpPosterPath)
                            End If
                        End If

                        If Master.eSettings.TVEpisodeFanartAnyEnabled Then
                            If Not String.IsNullOrEmpty(Episode.EpFanartPath) Then
                                Episode.TVEp.Fanart.FromFile(Episode.EpFanartPath)
                            ElseIf Not IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                                Episode.TVEp.Fanart = Scraper.TVDBImages.ShowFanart.Image
                            End If
                        End If

                        If Me.bwLoadImages.CancellationPending Then
                            Return True
                        End If
                        Me.bwLoadImages.ReportProgress(iProgress, "progress")
                        iProgress += 1
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try

        DefaultImages = Scraper.TVDBImages 'Scraper.TVDBImages.Clone() 'TODO: fix the clone function

        Return False
    End Function

    Public Overloads Function ShowDialog(ByVal ShowID As Integer, ByVal Type As Enums.TVImageType, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As System.Windows.Forms.DialogResult
        Me._id = ShowID
        Me._type = Type
        Me._withcurrent = WithCurrent
        Me._ScrapeType = ScrapeType
        Return MyBase.ShowDialog
    End Function

    Public Overloads Function ShowDialog(ByVal ShowID As Integer, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal CurrentImage As Images) As Images
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
            logger.Error(New StackFrame().GetMethod().Name,ex)
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
        If Me._type = Enums.TVImageType.All Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Fanart Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True

            'Show Banner
            If Master.eSettings.TVShowBannerAnyEnabled Then
                Master.currShow.ShowBannerPath = Scraper.TVDBImages.ShowBanner.LocalFile
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
                        Scraper.TVDBImages.ShowFanart.Image.Save(Scraper.TVDBImages.ShowFanart.LocalFile, , , False)
                        Master.currShow.ShowFanartPath = Scraper.TVDBImages.ShowFanart.LocalFile
                    End If
                End If
            End If

            'Show Poster
            If Master.eSettings.TVShowPosterAnyEnabled Then
                Master.currShow.ShowPosterPath = Scraper.TVDBImages.ShowPoster.LocalFile
            End If

            'AS Banner
            If Master.eSettings.TVASBannerAnyEnabled Then
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image.Image) Then
                    Master.currShow.SeasonBannerPath = Scraper.TVDBImages.AllSeasonsBanner.LocalFile
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
                        Scraper.TVDBImages.AllSeasonsFanart.Image.Save(Scraper.TVDBImages.AllSeasonsFanart.LocalFile, , , False)
                        Master.currShow.SeasonFanartPath = Scraper.TVDBImages.AllSeasonsFanart.LocalFile
                    End If
                End If
            End If

            'AS Poster
            If Master.eSettings.TVASPosterAnyEnabled Then
                If Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image.Image) Then
                    Master.currShow.SeasonPosterPath = Scraper.TVDBImages.AllSeasonsPoster.LocalFile
                End If
            End If

        ElseIf Me._type = Enums.TVImageType.AllSeasonsFanart AndAlso Me._fanartchanged Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Fanart Image...")
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
                    Scraper.TVDBImages.AllSeasonsFanart.Image.Save(Scraper.TVDBImages.AllSeasonsFanart.LocalFile, , , False)
                    Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.Image
                End If
            End If
        ElseIf Me._type = Enums.TVImageType.SeasonFanart AndAlso Me._fanartchanged Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Fanart Image...")
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
                    Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.Save(Scraper.TVDBImages.SeasonImageList(0).Fanart.LocalFile, , , False)
                    Me.pbCurrent.Image = Scraper.TVDBImages.SeasonImageList(0).Fanart.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.SeasonImageList(0).Fanart.Image
                End If
            End If
        ElseIf (Me._type = Enums.TVImageType.ShowFanart OrElse Me._type = Enums.TVImageType.EpisodeFanart) AndAlso Me._fanartchanged Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Fanart Image...")
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
                    Scraper.TVDBImages.ShowFanart.Image.Save(Scraper.TVDBImages.ShowFanart.LocalFile, , , False)
                    Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                    Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.Image
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
        Scraper.TVDBImages.ShowPoster = New Scraper.TVDBPoster
        Scraper.TVDBImages.ShowBanner = New Scraper.TVDBShowBanner
        Scraper.TVDBImages.ShowFanart = New Scraper.TVDBFanart
        Scraper.TVDBImages.AllSeasonsPoster = New Scraper.TVDBPoster
        Scraper.TVDBImages.AllSeasonsBanner = New Scraper.TVDBShowBanner
        Scraper.TVDBImages.AllSeasonsFanart = New Scraper.TVDBFanart
        Scraper.TVDBImages.SeasonImageList = New List(Of Scraper.TVDBSeasonImage)

        If Me.bwLoadData.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Select Case Me._type
            Case Enums.TVImageType.AllSeasonsPoster
                Scraper.TVDBImages.AllSeasonsPoster.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.TVImageType.AllSeasonsBanner
                Scraper.TVDBImages.AllSeasonsBanner.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.TVImageType.AllSeasonsFanart
                Scraper.TVDBImages.AllSeasonsFanart.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.TVImageType.SeasonPoster
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Poster = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.TVImageType.SeasonBanner
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Banner = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.TVImageType.SeasonFanart
                cSI = New Scraper.TVDBSeasonImage
                cSI.Season = Me._season
                cSI.Fanart.Image = CType(Me.pbCurrent.Tag, Images)
                Scraper.TVDBImages.SeasonImageList.Add(cSI)
            Case Enums.TVImageType.ShowPoster
                Scraper.TVDBImages.ShowPoster.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.TVImageType.ShowBanner
                Scraper.TVDBImages.ShowBanner.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.TVImageType.ShowFanart, Enums.TVImageType.EpisodeFanart
                Scraper.TVDBImages.ShowFanart.Image = CType(Me.pbCurrent.Tag, Images)
            Case Enums.TVImageType.All

                If _withcurrent Then
                    If Master.eSettings.TVShowPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowPosterPath) Then
                        Scraper.TVDBImages.ShowPoster.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowPosterPath)
                        Scraper.TVDBImages.ShowPoster.LocalFile = Scraper.tmpTVDBShow.Show.ShowPosterPath
                    End If

                    If Me.bwLoadData.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If

                    If Master.eSettings.TVShowBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.Show.ShowBannerPath) Then
                        Scraper.TVDBImages.ShowBanner.Image.FromFile(Scraper.tmpTVDBShow.Show.ShowBannerPath)
                        Scraper.TVDBImages.ShowBanner.LocalFile = Scraper.tmpTVDBShow.Show.ShowBannerPath
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

                    If Master.eSettings.TVASPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath) Then
                        Scraper.TVDBImages.AllSeasonsPoster.Image.FromFile(Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath)
                        Scraper.TVDBImages.AllSeasonsPoster.LocalFile = Scraper.tmpTVDBShow.AllSeason.SeasonPosterPath
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
                                    If Master.eSettings.TVSeasonPosterAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonPosterPath) Then
                                        cSI.Poster.FromFile(sEpisode.SeasonPosterPath)
                                    End If
                                    If Master.eSettings.TVSeasonBannerAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonBannerPath) Then
                                        cSI.Banner.FromFile(sEpisode.SeasonBannerPath)
                                    End If
                                    If Master.eSettings.TVSeasonFanartAnyEnabled AndAlso Not String.IsNullOrEmpty(sEpisode.SeasonFanartPath) Then
                                        cSI.Fanart.Image.FromFile(sEpisode.SeasonFanartPath)
                                        cSI.Fanart.LocalFile = sEpisode.SeasonFanartPath
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
                            logger.Error(New StackFrame().GetMethod().Name,ex)
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
                            logger.Error(New StackFrame().GetMethod().Name,ex)
                        End Try
                    Next
                End If
        End Select
    End Sub

    Private Sub bwLoadData_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadData.ProgressChanged
        Try
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
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
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
        Try
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
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
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
            logger.Error(New StackFrame().GetMethod().Name,ex)
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
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Function DownloadAllImages() As Boolean
        Dim iProgress As Integer = 1

        Try
            Me.bwLoadImages.ReportProgress(Scraper.tmpTVDBShow.Episodes.Count + Scraper.tmpTVDBShow.SeasonPosters.Count + Scraper.tmpTVDBShow.SeasonBanners.Count + Scraper.tmpTVDBShow.ShowBanners.Count + Scraper.tmpTVDBShow.Fanarts.Count + Scraper.tmpTVDBShow.Posters.Count, "max")

            'Epsiode Poster
            If Me._type = Enums.TVImageType.All Then
                For Each Epi As Structures.DBTV In Scraper.tmpTVDBShow.Episodes
                    Try
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
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

            'Season Poster
            If Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.SeasonPoster Then
                For Each Seas As Scraper.TVDBSeasonPoster In Scraper.tmpTVDBShow.SeasonPosters
                    Try
                        If Not File.Exists(Seas.LocalFile) Then
                            If Not String.IsNullOrEmpty(Seas.URL) Then
                                Seas.Image.FromWeb(Seas.URL)
                                If Not IsNothing(Seas.Image.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(Seas.LocalFile).FullName)
                                    Seas.Image.Save(Seas.LocalFile, , , False)
                                    SeasonPosterList.Add(Seas)
                                End If
                            End If
                        Else
                            Seas.Image.FromFile(Seas.LocalFile)
                            SeasonPosterList.Add(Seas)
                        End If

                        If Me.bwLoadImages.CancellationPending Then
                            Return True
                        End If

                        Me.bwLoadImages.ReportProgress(iProgress, "progress")
                        iProgress += 1
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

            'Season Banner
            If Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.SeasonBanner Then
                For Each Seas As Scraper.TVDBSeasonBanner In Scraper.tmpTVDBShow.SeasonBanners
                    Try
                        If Not File.Exists(Seas.LocalFile) Then
                            If Not String.IsNullOrEmpty(Seas.URL) Then
                                Seas.Image.FromWeb(Seas.URL)
                                If Not IsNothing(Seas.Image.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(Seas.LocalFile).FullName)
                                    Seas.Image.Save(Seas.LocalFile, , , False)
                                    SeasonBannerList.Add(Seas)
                                End If
                            End If
                        Else
                            Seas.Image.FromFile(Seas.LocalFile)
                            SeasonBannerList.Add(Seas)
                        End If

                        If Me.bwLoadImages.CancellationPending Then
                            Return True
                        End If

                        Me.bwLoadImages.ReportProgress(iProgress, "progress")
                        iProgress += 1
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

            'Show/AllSeason Poster
            If Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowPoster OrElse Me._type = Enums.TVImageType.AllSeasonsPoster Then
                For Each SPost As Scraper.TVDBPoster In Scraper.tmpTVDBShow.Posters
                    Try
                        If Not File.Exists(SPost.LocalFile) Then
                            If Not String.IsNullOrEmpty(SPost.URL) Then
                                SPost.Image.FromWeb(SPost.URL)
                                If Not IsNothing(SPost.Image.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(SPost.LocalFile).FullName)
                                    SPost.Image.Save(SPost.LocalFile, , , False)
                                    GenericPosterList.Add(SPost)
                                End If
                            End If
                        Else
                            SPost.Image.FromFile(SPost.LocalFile)
                            GenericPosterList.Add(SPost)
                        End If

                        If Me.bwLoadImages.CancellationPending Then
                            Return True
                        End If

                        Me.bwLoadImages.ReportProgress(iProgress, "progress")
                        iProgress += 1
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

            'Show/AllSeason Banner
            If Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowBanner OrElse Me._type = Enums.TVImageType.AllSeasonsBanner Then
                For Each SPost As Scraper.TVDBShowBanner In Scraper.tmpTVDBShow.ShowBanners
                    Try
                        If Not File.Exists(SPost.LocalFile) Then
                            If Not String.IsNullOrEmpty(SPost.URL) Then
                                SPost.Image.FromWeb(SPost.URL)
                                If Not IsNothing(SPost.Image.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(SPost.LocalFile).FullName)
                                    SPost.Image.Save(SPost.LocalFile, , , False)
                                    ShowBannerList.Add(SPost)
                                End If
                            End If
                        Else
                            SPost.Image.FromFile(SPost.LocalFile)
                            ShowBannerList.Add(SPost)
                        End If

                        If Me.bwLoadImages.CancellationPending Then
                            Return True
                        End If

                        Me.bwLoadImages.ReportProgress(iProgress, "progress")
                        iProgress += 1
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

            'Show/AllSeason/Season/Episode Fanart
            If Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowFanart OrElse Me._type = Enums.TVImageType.AllSeasonsFanart OrElse Me._type = Enums.TVImageType.SeasonFanart OrElse Me._type = Enums.TVImageType.EpisodeFanart Then
                For Each SFan As Scraper.TVDBFanart In Scraper.tmpTVDBShow.Fanarts
                    Try
                        If Not File.Exists(SFan.LocalThumb) Then
                            If Not String.IsNullOrEmpty(SFan.ThumbnailURL) Then
                                SFan.Image.FromWeb(SFan.ThumbnailURL)
                                If Not IsNothing(SFan.Image.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(SFan.LocalThumb).FullName)
                                    SFan.Image.Save(SFan.LocalThumb, , , False)
                                    GenericFanartList.Add(SFan)
                                End If
                            End If
                        Else
                            SFan.Image.FromFile(SFan.LocalThumb)
                            GenericFanartList.Add(SFan)
                        End If

                        If Me.bwLoadImages.CancellationPending Then
                            Return True
                        End If

                        Me.bwLoadImages.ReportProgress(iProgress, "progress")
                        iProgress += 1
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try

        Return Me.SetDefaults()
    End Function

    Private Sub DownloadFanart(ByVal iTag As ImageTag, ByRef tImage As Images)
        Dim sHTTP As New HTTP

        If Not String.IsNullOrEmpty(iTag.Path) AndAlso File.Exists(iTag.Path) Then
            tImage.FromFile(iTag.Path)
        ElseIf Not String.IsNullOrEmpty(iTag.Path) AndAlso Not String.IsNullOrEmpty(iTag.URL) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Fanart Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.pnlStatus.Visible = True

            Application.DoEvents()

            tImage.FromWeb(iTag.URL)
            If Not IsNothing(tImage.Image) Then
                Directory.CreateDirectory(Directory.GetParent(iTag.Path).FullName)
                tImage.Save(iTag.Path, , , False)
            End If

            sHTTP = Nothing

            Me.pnlStatus.Visible = False
        End If

    End Sub

    Private Sub GenerateList()
        Try
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowPoster) AndAlso Master.eSettings.TVShowPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(956, "Show Poster"), .Tag = "showp", .ImageIndex = 0, .SelectedImageIndex = 0})
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowBanner) AndAlso Master.eSettings.TVShowBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = "Show Banner", .Tag = "showb", .ImageIndex = 0, .SelectedImageIndex = 0})
            'If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowLandscape) AndAlso Master.eSettings.TVShowLandscapeEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1010, "Show Landscape"), .Tag = "showl", .ImageIndex = 0, .SelectedImageIndex = 0})
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowFanart OrElse Me._type = Enums.TVImageType.EpisodeFanart) AndAlso (Master.eSettings.TVShowFanartAnyEnabled OrElse Master.eSettings.TVEpisodeFanartAnyEnabled) Then Me.tvList.Nodes.Add(New TreeNode With {.Text = If(Me._type = Enums.TVImageType.EpisodeFanart, Master.eLang.GetString(688, "Episode Fanart"), Master.eLang.GetString(684, "Show Fanart")), .Tag = "showf", .ImageIndex = 1, .SelectedImageIndex = 1})
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.AllSeasonsPoster) AndAlso Master.eSettings.TVASPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(735, "All Seasons Poster"), .Tag = "allp", .ImageIndex = 2, .SelectedImageIndex = 2})
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.AllSeasonsBanner) AndAlso Master.eSettings.TVASBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1014, "All Seasons Banner"), .Tag = "allb", .ImageIndex = 2, .SelectedImageIndex = 2})
            'If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.AllSeasonsLandscape) AndAlso Master.eSettings.TVASLandscapeEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1016, "All Seasons Landscape"), .Tag = "alll", .ImageIndex = 2, .SelectedImageIndex = 2})
            If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.AllSeasonsFanart) AndAlso Master.eSettings.TVASFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1015, "All Seasons Fanart"), .Tag = "allf", .ImageIndex = 2, .SelectedImageIndex = 2})
            'If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowCharacterArt) AndAlso Master.eSettings.TVShowCharacterArtEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1011, "Show CharacterArt"), .Tag = "showcha", .ImageIndex = 0, .SelectedImageIndex = 0})
            'If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowClearArt) AndAlso Master.eSettings.TVShowClearArtEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1013, "Show ClearArt"), .Tag = "showca", .ImageIndex = 0, .SelectedImageIndex = 0})
            'If (Me._type = Enums.TVImageType.All OrElse Me._type = Enums.TVImageType.ShowClearLogo) AndAlso Master.eSettings.TVShowClearLogoEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1012, "Show ClearLogo"), .Tag = "showcl", .ImageIndex = 0, .SelectedImageIndex = 0})

            Dim TnS As TreeNode
            If Me._type = Enums.TVImageType.All Then
                For Each cSeason As Scraper.TVDBSeasonImage In Scraper.TVDBImages.SeasonImageList.OrderBy(Function(s) s.Season)
                    Try
                        TnS = New TreeNode(String.Format(Master.eLang.GetString(726, "Season {0}"), cSeason.Season), 3, 3)
                        If Master.eSettings.TVSeasonPosterAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(685, "Season Posters"), .Tag = String.Concat("p", cSeason.Season.ToString), .ImageIndex = 0, .SelectedImageIndex = 0})
                        If Master.eSettings.TVSeasonBannerAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1017, "Season Banner"), .Tag = String.Concat("b", cSeason.Season.ToString), .ImageIndex = 0, .SelectedImageIndex = 0})
                        'If Master.eSettings.TVSeasonLandscapeEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1018, "Season Landscape"), .Tag = String.Concat("l", cSeason.Season.ToString), .ImageIndex = 0, .SelectedImageIndex = 0})
                        If Master.eSettings.TVSeasonFanartAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(686, "Season Fanart"), .Tag = String.Concat("f", cSeason.Season.ToString), .ImageIndex = 1, .SelectedImageIndex = 1})
                        Me.tvList.Nodes.Add(TnS)
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name,ex)
                    End Try
                Next
            ElseIf Me._type = Enums.TVImageType.SeasonPoster Then
                If Master.eSettings.TVSeasonPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(961, "Season {0} Posters"), Me._season), .Tag = String.Concat("p", Me._season)})
            ElseIf Me._type = Enums.TVImageType.SeasonBanner Then
                If Master.eSettings.TVSeasonBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(1019, "Season {0} Banner"), Me._season), .Tag = String.Concat("b", Me._season)})
                'ElseIf Me._type = Enums.TVImageType.SeasonLandscape Then
                '    If Master.eSettings.TVSeasonLandscapeEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(1020, "Season {0} Landscape"), Me._season), .Tag = String.Concat("l", Me._season)})
            ElseIf Me._type = Enums.TVImageType.SeasonFanart Then
                If Master.eSettings.TVSeasonFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(962, "Season {0} Fanart"), Me._season), .Tag = String.Concat("f", Me._season)})
            End If

            Me.tvList.ExpandAll()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
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
            logger.Error(New StackFrame().GetMethod().Name,ex)
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
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Function

    Private Sub lblImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iindex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelect(iindex, DirectCast(DirectCast(sender, Label).Tag, ImageTag))
    End Sub

    Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Delta < 0 Then
            If (pnlImages.VerticalScroll.Value + 50) <= pnlImages.VerticalScroll.Maximum Then
                pnlImages.VerticalScroll.Value += 50
            Else
                pnlImages.VerticalScroll.Value = pnlImages.VerticalScroll.Maximum
            End If
        Else
            If (pnlImages.VerticalScroll.Value - 50) >= pnlImages.VerticalScroll.Minimum Then
                pnlImages.VerticalScroll.Value -= 50
            Else
                pnlImages.VerticalScroll.Value = pnlImages.VerticalScroll.Minimum
            End If
        End If
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
            DownloadFanart(iTag, tImages)
            tImage = tImages.Image
        Else
            tImage = DirectCast(sender, PictureBox).Image
        End If

        ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage)
    End Sub

    Private Sub pbUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbUndo.Click
        If Me.SelSeason = -999 Then
            If Me.SelIsPoster Then
                Scraper.TVDBImages.ShowPoster.Image = DefaultImages.ShowPoster.Image
                Scraper.TVDBImages.ShowPoster.LocalFile = DefaultImages.ShowPoster.LocalFile
                Scraper.TVDBImages.ShowPoster.URL = DefaultImages.ShowPoster.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowPoster.Image
            ElseIf Me.SelIsBanner Then
                Scraper.TVDBImages.ShowBanner.Image = DefaultImages.ShowBanner.Image
                Scraper.TVDBImages.ShowBanner.LocalFile = DefaultImages.ShowBanner.LocalFile
                Scraper.TVDBImages.ShowBanner.URL = DefaultImages.ShowBanner.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowBanner.Image
            Else
                Scraper.TVDBImages.ShowFanart.Image = DefaultImages.ShowFanart.Image
                Scraper.TVDBImages.ShowFanart.LocalFile = DefaultImages.ShowFanart.LocalFile
                Scraper.TVDBImages.ShowFanart.URL = DefaultImages.ShowFanart.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.ShowFanart.Image
            End If
        ElseIf Me.SelSeason = 999 Then
            If Me.SelIsPoster Then
                Scraper.TVDBImages.AllSeasonsPoster.Image = DefaultImages.AllSeasonsPoster.Image
                Scraper.TVDBImages.AllSeasonsPoster.LocalFile = DefaultImages.AllSeasonsPoster.LocalFile
                Scraper.TVDBImages.AllSeasonsPoster.URL = DefaultImages.AllSeasonsPoster.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsPoster.Image
            ElseIf Me.SelIsBanner Then
                Scraper.TVDBImages.AllSeasonsBanner.Image = DefaultImages.AllSeasonsBanner.Image
                Scraper.TVDBImages.AllSeasonsBanner.LocalFile = DefaultImages.AllSeasonsBanner.LocalFile
                Scraper.TVDBImages.AllSeasonsBanner.URL = DefaultImages.AllSeasonsBanner.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsBanner.Image
            Else
                Scraper.TVDBImages.AllSeasonsFanart.Image = DefaultImages.AllSeasonsFanart.Image
                Scraper.TVDBImages.AllSeasonsFanart.LocalFile = DefaultImages.AllSeasonsFanart.LocalFile
                Scraper.TVDBImages.AllSeasonsFanart.URL = DefaultImages.AllSeasonsFanart.URL
                Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.Image.Image
                Me.pbCurrent.Tag = Scraper.TVDBImages.AllSeasonsFanart.Image
            End If
        Else
            If Me.SelIsPoster Then
                Dim dSPost As Images = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster = dSPost
                Me.pbCurrent.Image = dSPost.Image
                Me.pbCurrent.Tag = dSPost
            ElseIf Me.SelIsBanner Then
                Dim dSPost As Images = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner = dSPost
                Me.pbCurrent.Image = dSPost.Image
                Me.pbCurrent.Tag = dSPost
            Else
                Dim dSFan As Scraper.TVDBFanart = DefaultImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                Dim tSFan As Scraper.TVDBFanart = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tSFan.Image = dSFan.Image
                tSFan.LocalFile = dSFan.LocalFile
                tSFan.URL = dSFan.URL
                Me.pbCurrent.Image = dSFan.Image.Image
                Me.pbCurrent.Tag = dSFan.Image
            End If
        End If
    End Sub

    Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelect(iIndex, DirectCast(DirectCast(sender, Panel).Tag, ImageTag))
    End Sub

    Private Sub SetImage(ByVal SelTag As ImageTag)
        Me.pbCurrent.Image = SelTag.ImageObj.Image
        Me.pbCurrent.Tag = SelTag.ImageObj

        Me._fanartchanged = True

        If Me.SelSeason = -999 Then
            If Me.SelIsPoster Then
                Scraper.TVDBImages.ShowPoster.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowPoster.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowPoster.URL = SelTag.URL
            ElseIf Me.SelIsBanner Then
                Scraper.TVDBImages.ShowBanner.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowBanner.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowBanner.URL = SelTag.URL
            Else
                Scraper.TVDBImages.ShowFanart.Image = SelTag.ImageObj
                Scraper.TVDBImages.ShowFanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.ShowFanart.URL = SelTag.URL
            End If
        ElseIf Me.SelSeason = 999 Then
            If Me.SelIsPoster Then
                Scraper.TVDBImages.AllSeasonsPoster.Image = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsPoster.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsPoster.URL = SelTag.URL
            ElseIf Me.SelIsBanner Then
                Scraper.TVDBImages.AllSeasonsBanner.Image = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsBanner.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsBanner.URL = SelTag.URL
            Else
                Scraper.TVDBImages.AllSeasonsFanart.Image = SelTag.ImageObj
                Scraper.TVDBImages.AllSeasonsFanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.AllSeasonsFanart.URL = SelTag.URL
            End If
        Else
            If Me.SelIsPoster Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster = SelTag.ImageObj
            ElseIf Me.SelIsBanner Then
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner = SelTag.ImageObj
            Else
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.Image = SelTag.ImageObj
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.LocalFile = SelTag.Path
                Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart.URL = SelTag.URL
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

        Try
            ClearImages()
            If Not IsNothing(e.Node.Tag) AndAlso Not String.IsNullOrEmpty(e.Node.Tag.ToString) Then
                Me.pbCurrent.Visible = True
                Me.lblCurrentImage.Visible = True
                If e.Node.Tag.ToString = "showp" Then
                    Me.SelSeason = -999
                    Me.SelIsPoster = True
                    Me.SelIsBanner = False
                    Me.SelIsLandscape = False
                    If Not IsNothing(Scraper.TVDBImages.ShowPoster) AndAlso Not IsNothing(Scraper.TVDBImages.ShowPoster.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowPoster.Image.Image) Then
                        Me.pbCurrent.Image = Scraper.TVDBImages.ShowPoster.Image.Image
                    Else
                        Me.pbCurrent.Image = Nothing
                    End If

                    'iCount = ShowBannerList.Count
                    'For i = 0 To iCount - 1
                    '    If Not IsNothing(ShowBannerList(i)) AndAlso Not IsNothing(ShowBannerList(i).Image) AndAlso Not IsNothing(ShowBannerList(i).Image.Image) Then
                    '        Me.AddImage(String.Format("{0}x{1}", ShowBannerList(i).Image.Image.Width, ShowBannerList(i).Image.Image.Height), i, New ImageTag With {.URL = ShowBannerList(i).URL, .Path = ShowBannerList(i).LocalFile, .isFanart = False, .ImageObj = ShowBannerList(i).Image})
                    '    End If
                    'Next

                    For i = 0 To GenericPosterList.Count - 1
                        If Not IsNothing(GenericPosterList(i)) AndAlso Not IsNothing(GenericPosterList(i).Image) AndAlso Not IsNothing(GenericPosterList(i).Image.Image) Then
                            Me.AddImage(String.Format("{0}x{1}", GenericPosterList(i).Image.Image.Width, GenericPosterList(i).Image.Image.Height), i + iCount, New ImageTag With {.URL = GenericPosterList(i).URL, .Path = GenericPosterList(i).LocalFile, .isFanart = False, .ImageObj = GenericPosterList(i).Image})
                        End If
                    Next

                ElseIf e.Node.Tag.ToString = "showb" Then
                    Me.SelSeason = -999
                    Me.SelIsPoster = False
                    Me.SelIsBanner = True
                    Me.SelIsLandscape = False
                    If Not IsNothing(Scraper.TVDBImages.ShowBanner) AndAlso Not IsNothing(Scraper.TVDBImages.ShowBanner.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowBanner.Image.Image) Then
                        Me.pbCurrent.Image = Scraper.TVDBImages.ShowBanner.Image.Image
                    Else
                        Me.pbCurrent.Image = Nothing
                    End If

                    iCount = ShowBannerList.Count
                    For i = 0 To iCount - 1
                        If Not IsNothing(ShowBannerList(i)) AndAlso Not IsNothing(ShowBannerList(i).Image) AndAlso Not IsNothing(ShowBannerList(i).Image.Image) Then
                            Me.AddImage(String.Format("{0}x{1}", ShowBannerList(i).Image.Image.Width, ShowBannerList(i).Image.Image.Height), i, New ImageTag With {.URL = ShowBannerList(i).URL, .Path = ShowBannerList(i).LocalFile, .isFanart = False, .ImageObj = ShowBannerList(i).Image})
                        End If
                    Next

                    'For i = 0 To GenericPosterList.Count - 1
                    '    If Not IsNothing(GenericPosterList(i)) AndAlso Not IsNothing(GenericPosterList(i).Image) AndAlso Not IsNothing(GenericPosterList(i).Image.Image) Then
                    '        Me.AddImage(String.Format("{0}x{1}", GenericPosterList(i).Image.Image.Width, GenericPosterList(i).Image.Image.Height), i + iCount, New ImageTag With {.URL = GenericPosterList(i).URL, .Path = GenericPosterList(i).LocalFile, .isFanart = False, .ImageObj = GenericPosterList(i).Image})
                    '    End If
                    'Next

                ElseIf e.Node.Tag.ToString = "showf" Then
                    Me.SelSeason = -999
                    Me.SelIsPoster = False
                    Me.SelIsBanner = False
                    Me.SelIsLandscape = False
                    If Not IsNothing(Scraper.TVDBImages.ShowFanart) AndAlso Not IsNothing(Scraper.TVDBImages.ShowFanart.Image) AndAlso Not IsNothing(Scraper.TVDBImages.ShowFanart.Image.Image) Then
                        Me.pbCurrent.Image = Scraper.TVDBImages.ShowFanart.Image.Image
                    Else
                        Me.pbCurrent.Image = Nothing
                    End If

                    For i = 0 To GenericFanartList.Count - 1
                        If Not IsNothing(GenericFanartList(i)) AndAlso Not IsNothing(GenericFanartList(i).Image) AndAlso Not IsNothing(GenericFanartList(i).Image.Image) Then
                            Me.AddImage(String.Format("{0}x{1}", GenericFanartList(i).Size.Width, GenericFanartList(i).Size.Height), i, New ImageTag With {.URL = GenericFanartList(i).URL, .Path = GenericFanartList(i).LocalFile, .isFanart = True, .ImageObj = GenericFanartList(i).Image})
                        End If
                    Next

                ElseIf e.Node.Tag.ToString = "allp" Then
                    Me.SelSeason = 999
                    Me.SelIsPoster = True
                    Me.SelIsBanner = False
                    Me.SelIsLandscape = False
                    If Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsPoster.Image.Image) Then
                        Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsPoster.Image.Image
                    Else
                        Me.pbCurrent.Image = Nothing
                    End If

                    iCount = GenericPosterList.Count
                    For i = 0 To iCount - 1
                        If Not IsNothing(GenericPosterList(i)) AndAlso Not IsNothing(GenericPosterList(i).Image) AndAlso Not IsNothing(GenericPosterList(i).Image.Image) Then
                            Me.AddImage(String.Format("{0}x{1}", GenericPosterList(i).Image.Image.Width, GenericPosterList(i).Image.Image.Height), i, New ImageTag With {.URL = GenericPosterList(i).URL, .Path = GenericPosterList(i).LocalFile, .isFanart = False, .ImageObj = GenericPosterList(i).Image})
                        End If
                    Next

                ElseIf e.Node.Tag.ToString = "allb" Then
                    Me.SelSeason = 999
                    Me.SelIsPoster = False
                    Me.SelIsBanner = True
                    Me.SelIsLandscape = False
                    If Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsBanner.Image.Image) Then
                        Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsBanner.Image.Image
                    Else
                        Me.pbCurrent.Image = Nothing
                    End If

                    iCount = ShowBannerList.Count
                    For i = 0 To iCount - 1
                        If Not IsNothing(ShowBannerList(i)) AndAlso Not IsNothing(ShowBannerList(i).Image) AndAlso Not IsNothing(ShowBannerList(i).Image.Image) Then
                            Me.AddImage(String.Format("{0}x{1}", ShowBannerList(i).Image.Image.Width, ShowBannerList(i).Image.Image.Height), i, New ImageTag With {.URL = ShowBannerList(i).URL, .Path = ShowBannerList(i).LocalFile, .isFanart = False, .ImageObj = ShowBannerList(i).Image})
                        End If
                    Next

                ElseIf e.Node.Tag.ToString = "allf" Then
                    Me.SelSeason = 999
                    Me.SelIsPoster = False
                    Me.SelIsBanner = False
                    Me.SelIsLandscape = False
                    If Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image) AndAlso Not IsNothing(Scraper.TVDBImages.AllSeasonsFanart.Image.Image) Then
                        Me.pbCurrent.Image = Scraper.TVDBImages.AllSeasonsFanart.Image.Image
                    Else
                        Me.pbCurrent.Image = Nothing
                    End If

                    For i = 0 To GenericFanartList.Count - 1
                        If Not IsNothing(GenericFanartList(i)) AndAlso Not IsNothing(GenericFanartList(i).Image) AndAlso Not IsNothing(GenericFanartList(i).Image.Image) Then
                            Me.AddImage(String.Format("{0}x{1}", GenericFanartList(i).Size.Width, GenericFanartList(i).Size.Height), i, New ImageTag With {.URL = GenericFanartList(i).URL, .Path = GenericFanartList(i).LocalFile, .isFanart = True, .ImageObj = GenericFanartList(i).Image})
                        End If
                    Next

                    'For i = 0 To GenericPosterList.Count - 1
                    '    If Not IsNothing(GenericPosterList(i)) AndAlso Not IsNothing(GenericPosterList(i).Image) AndAlso Not IsNothing(GenericPosterList(i).Image.Image) Then
                    '        Me.AddImage(String.Format("{0}x{1}", GenericPosterList(i).Image.Image.Width, GenericPosterList(i).Image.Image.Height), i + iCount, New ImageTag With {.URL = GenericPosterList(i).URL, .Path = GenericPosterList(i).LocalFile, .isFanart = False, .ImageObj = GenericPosterList(i).Image})
                    '    End If
                    'Next

                    'For i = 0 To ShowBannerList.Count - 1
                    '    If Not IsNothing(ShowBannerList(i)) AndAlso Not IsNothing(ShowBannerList(i).Image) AndAlso Not IsNothing(ShowBannerList(i).Image.Image) Then
                    '        Me.AddImage(String.Format("{0}x{1}", ShowBannerList(i).Image.Image.Width, ShowBannerList(i).Image.Image.Height), i + iCount, New ImageTag With {.URL = ShowBannerList(i).URL, .Path = ShowBannerList(i).LocalFile, .isFanart = False, .ImageObj = ShowBannerList(i).Image})
                    '    End If
                    'Next
                Else
                    Dim tMatch As Match = Regex.Match(e.Node.Tag.ToString, "(?<type>f|p|b|l)(?<num>[0-9]+)")
                    If tMatch.Success Then
                        If tMatch.Groups("type").Value = "f" Then
                            Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                            Me.SelIsPoster = False
                            Me.SelIsBanner = False
                            Me.SelIsLandscape = False
                            Dim tFanart As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                            If Not IsNothing(tFanart) AndAlso Not IsNothing(tFanart.Fanart) AndAlso Not IsNothing(tFanart.Fanart.Image) AndAlso Not IsNothing(tFanart.Fanart.Image.Image) Then
                                Me.pbCurrent.Image = tFanart.Fanart.Image.Image
                            Else
                                Me.pbCurrent.Image = Nothing
                            End If
                            For i = 0 To GenericFanartList.Count - 1
                                If Not IsNothing(GenericFanartList(i)) AndAlso Not IsNothing(GenericFanartList(i).Image) AndAlso Not IsNothing(GenericFanartList(i).Image.Image) Then
                                    Me.AddImage(String.Format("{0}x{1}", GenericFanartList(i).Size.Width, GenericFanartList(i).Size.Height), i, New ImageTag With {.URL = GenericFanartList(i).URL, .Path = GenericFanartList(i).LocalFile, .isFanart = True, .ImageObj = GenericFanartList(i).Image})
                                End If
                            Next
                        ElseIf tMatch.Groups("type").Value = "p" Then
                            Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                            Me.SelIsPoster = True
                            Me.SelIsBanner = False
                            Me.SelIsLandscape = False
                            Dim tPoster As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                            If Not IsNothing(tPoster) AndAlso Not IsNothing(tPoster.Poster) AndAlso Not IsNothing(tPoster.Poster.Image) Then
                                Me.pbCurrent.Image = tPoster.Poster.Image
                            Else
                                Me.pbCurrent.Image = Nothing
                            End If
                            iCount = 0
                            For Each SImage As Scraper.TVDBSeasonPoster In SeasonPosterList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                                If Not IsNothing(SImage.Image) AndAlso Not IsNothing(SImage.Image.Image) Then
                                    Me.AddImage(String.Format("{0}x{1}", SImage.Image.Image.Width, SImage.Image.Image.Height), iCount, New ImageTag With {.URL = SImage.URL, .Path = SImage.LocalFile, .isFanart = False, .ImageObj = SImage.Image})
                                End If
                                iCount += 1
                            Next
                        ElseIf tMatch.Groups("type").Value = "b" Then
                            Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                            Me.SelIsPoster = False
                            Me.SelIsBanner = True
                            Me.SelIsLandscape = False
                            Dim tBanner As Scraper.TVDBSeasonImage = Scraper.TVDBImages.SeasonImageList.FirstOrDefault(Function(f) f.Season = Me.SelSeason)
                            If Not IsNothing(tBanner) AndAlso Not IsNothing(tBanner.Banner) AndAlso Not IsNothing(tBanner.Banner.Image) Then
                                Me.pbCurrent.Image = tBanner.Banner.Image
                            Else
                                Me.pbCurrent.Image = Nothing
                            End If
                            iCount = 0
                            For Each SImage As Scraper.TVDBSeasonBanner In SeasonBannerList.Where(Function(s) s.Season = Convert.ToInt32(tMatch.Groups("num").Value))
                                If Not IsNothing(SImage.Image) AndAlso Not IsNothing(SImage.Image.Image) Then
                                    Me.AddImage(String.Format("{0}x{1}", SImage.Image.Image.Width, SImage.Image.Image.Height), iCount, New ImageTag With {.URL = SImage.URL, .Path = SImage.LocalFile, .isFanart = False, .ImageObj = SImage.Image})
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

            Me.CheckCurrentImage()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
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