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

    Private DefaultImagesContainer As New MediaContainers.ImagesContainer
    Private DefaultEpisodeImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private DefaultSeasonImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private SearchResultsContainer As New MediaContainers.SearchResultsContainer_TV

    Private tmpShowContainer As New Structures.DBTV

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
    Private _withcurrent As Boolean = True
    Private _ScrapeModifier As New Structures.ScrapeModifier

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Results As Structures.DBTV
        Get
            Return tmpShowContainer
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
        Images.SetDefaultImages_TV(tmpShowContainer, DefaultImagesContainer, DefaultSeasonImagesContainer, DefaultEpisodeImagesContainer, SearchResultsContainer, Me._ScrapeModifier)
        Return False
    End Function

    Public Overloads Function ShowDialog(ByVal ShowID As Integer, ByVal ScrapeModifier As Structures.ScrapeModifier, ByVal WithCurrent As Boolean) As System.Windows.Forms.DialogResult
        Me._id = ShowID
        Me._ScrapeModifier = _ScrapeModifier
        Me._withcurrent = WithCurrent
        Return MyBase.ShowDialog
    End Function

    Public Overloads Function ShowDialog(ByVal ShowID As Integer, ByVal ScrapeModifier As Structures.ScrapeModifier, ByVal Season As Integer, ByVal CurrentImage As Images) As Images
        Me._id = ShowID
        Me._ScrapeModifier = ScrapeModifier
        Me._season = Season
        Me.pbCurrent.Image = CurrentImage.Image
        Me.pbCurrent.Tag = CurrentImage

        If MyBase.ShowDialog = Windows.Forms.DialogResult.OK Then
            Return CType(Me.pbCurrent.Tag, Images)
        Else
            Return Nothing
        End If
    End Function

    Public Overloads Function ShowDialog(ByRef DBTV As Structures.DBTV, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_TV, ByVal ScrapeModifier As Structures.ScrapeModifier, Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.SearchResultsContainer = ImagesContainer
        Me.tmpShowContainer = DBTV
        Me._ScrapeModifier = ScrapeModifier
        Return MyBase.ShowDialog()
    End Function

    Private Sub AddImage(ByRef fTag As MediaContainers.Image, ByVal iIndex As Integer)
        Try
            Dim sDescription As String = String.Empty

            If fTag IsNot Nothing AndAlso fTag.WebImage IsNot Nothing AndAlso fTag.WebImage.Image IsNot Nothing Then
                Dim imgText As String = String.Empty
                If CDbl(fTag.Width) = 0 OrElse CDbl(fTag.Height) = 0 Then
                    sDescription = String.Format("{0}x{1}", fTag.WebImage.Image.Size.Width, fTag.WebImage.Image.Size.Height & Environment.NewLine & fTag.LongLang)
                Else
                    sDescription = String.Format("{0}x{1}", fTag.Width, fTag.Height & Environment.NewLine & fTag.LongLang)
                End If
            ElseIf fTag IsNot Nothing AndAlso fTag.WebThumb IsNot Nothing AndAlso fTag.WebThumb.Image IsNot Nothing Then
                Dim imgText As String = String.Empty
                If CDbl(fTag.Width) = 0 OrElse CDbl(fTag.Height) = 0 Then
                    sDescription = String.Concat("unknown", Environment.NewLine, fTag.LongLang)
                Else
                    sDescription = String.Format("{0}x{1}", fTag.Width, fTag.Height & Environment.NewLine & fTag.LongLang)
                End If
            End If

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
            Me.pbImage(iIndex).Image = If(fTag.WebImage.Image IsNot Nothing, CType(fTag.WebImage.Image.Clone(), Image), CType(fTag.WebThumb.Image.Clone(), Image))
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
    ''' <summary>
    ''' Downloading fullsize images for preview in Edit Episode / Season / Show dialog
    ''' </summary>
    ''' <remarks>All other images will be downloaded while saving to DB</remarks>
    Private Sub DoneAndClose()
        Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
        Me.pbStatus.Style = ProgressBarStyle.Marquee
        Me.pnlStatus.Visible = True

        'Banner
        If Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Banner.LocalFile) AndAlso File.Exists(tmpShowContainer.ImagesContainer.Banner.LocalFile) Then
            tmpShowContainer.ImagesContainer.Banner.WebImage.FromFile(tmpShowContainer.ImagesContainer.Banner.LocalFile)
        ElseIf Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Banner.URL) AndAlso Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Banner.LocalFile) Then
            tmpShowContainer.ImagesContainer.Banner.WebImage.Clear()
            tmpShowContainer.ImagesContainer.Banner.WebImage.FromWeb(tmpShowContainer.ImagesContainer.Banner.URL)
            If tmpShowContainer.ImagesContainer.Banner.WebImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(tmpShowContainer.ImagesContainer.Banner.LocalFile).FullName)
                tmpShowContainer.ImagesContainer.Banner.WebImage.Save(tmpShowContainer.ImagesContainer.Banner.LocalFile)
            End If
        End If

        'CharacterArt
        If Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.CharacterArt.LocalFile) AndAlso File.Exists(tmpShowContainer.ImagesContainer.CharacterArt.LocalFile) Then
            tmpShowContainer.ImagesContainer.CharacterArt.WebImage.FromFile(tmpShowContainer.ImagesContainer.CharacterArt.LocalFile)
        ElseIf Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.CharacterArt.URL) AndAlso Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.CharacterArt.LocalFile) Then
            tmpShowContainer.ImagesContainer.CharacterArt.WebImage.Clear()
            tmpShowContainer.ImagesContainer.CharacterArt.WebImage.FromWeb(tmpShowContainer.ImagesContainer.CharacterArt.URL)
            If tmpShowContainer.ImagesContainer.CharacterArt.WebImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(tmpShowContainer.ImagesContainer.CharacterArt.LocalFile).FullName)
                tmpShowContainer.ImagesContainer.CharacterArt.WebImage.Save(tmpShowContainer.ImagesContainer.CharacterArt.LocalFile)
            End If
        End If

        'ClearArt
        If Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.ClearArt.LocalFile) AndAlso File.Exists(tmpShowContainer.ImagesContainer.ClearArt.LocalFile) Then
            tmpShowContainer.ImagesContainer.ClearArt.WebImage.FromFile(tmpShowContainer.ImagesContainer.ClearArt.LocalFile)
        ElseIf Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.ClearArt.URL) AndAlso Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.ClearArt.LocalFile) Then
            tmpShowContainer.ImagesContainer.ClearArt.WebImage.Clear()
            tmpShowContainer.ImagesContainer.ClearArt.WebImage.FromWeb(tmpShowContainer.ImagesContainer.ClearArt.URL)
            If tmpShowContainer.ImagesContainer.ClearArt.WebImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(tmpShowContainer.ImagesContainer.ClearArt.LocalFile).FullName)
                tmpShowContainer.ImagesContainer.ClearArt.WebImage.Save(tmpShowContainer.ImagesContainer.ClearArt.LocalFile)
            End If
        End If

        'ClearLogo
        If Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.ClearLogo.LocalFile) AndAlso File.Exists(tmpShowContainer.ImagesContainer.ClearLogo.LocalFile) Then
            tmpShowContainer.ImagesContainer.ClearLogo.WebImage.FromFile(tmpShowContainer.ImagesContainer.ClearLogo.LocalFile)
        ElseIf Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.ClearLogo.URL) AndAlso Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.ClearLogo.LocalFile) Then
            tmpShowContainer.ImagesContainer.ClearLogo.WebImage.Clear()
            tmpShowContainer.ImagesContainer.ClearLogo.WebImage.FromWeb(tmpShowContainer.ImagesContainer.ClearLogo.URL)
            If tmpShowContainer.ImagesContainer.ClearLogo.WebImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(tmpShowContainer.ImagesContainer.ClearLogo.LocalFile).FullName)
                tmpShowContainer.ImagesContainer.ClearLogo.WebImage.Save(tmpShowContainer.ImagesContainer.ClearLogo.LocalFile)
            End If
        End If

        'Fanart
        If Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Fanart.LocalFile) AndAlso File.Exists(tmpShowContainer.ImagesContainer.Fanart.LocalFile) Then
            tmpShowContainer.ImagesContainer.Fanart.WebImage.FromFile(tmpShowContainer.ImagesContainer.Fanart.LocalFile)
        ElseIf Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Fanart.URL) AndAlso Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Fanart.LocalFile) Then
            tmpShowContainer.ImagesContainer.Fanart.WebImage.Clear()
            tmpShowContainer.ImagesContainer.Fanart.WebImage.FromWeb(tmpShowContainer.ImagesContainer.Fanart.URL)
            If tmpShowContainer.ImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(tmpShowContainer.ImagesContainer.Fanart.LocalFile).FullName)
                tmpShowContainer.ImagesContainer.Fanart.WebImage.Save(tmpShowContainer.ImagesContainer.Fanart.LocalFile)
            End If
        End If

        'Landscape
        If Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Landscape.LocalFile) AndAlso File.Exists(tmpShowContainer.ImagesContainer.Landscape.LocalFile) Then
            tmpShowContainer.ImagesContainer.Landscape.WebImage.FromFile(tmpShowContainer.ImagesContainer.Landscape.LocalFile)
        ElseIf Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Landscape.URL) AndAlso Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Landscape.LocalFile) Then
            tmpShowContainer.ImagesContainer.Landscape.WebImage.Clear()
            tmpShowContainer.ImagesContainer.Landscape.WebImage.FromWeb(tmpShowContainer.ImagesContainer.Landscape.URL)
            If tmpShowContainer.ImagesContainer.Landscape.WebImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(tmpShowContainer.ImagesContainer.Landscape.LocalFile).FullName)
                tmpShowContainer.ImagesContainer.Landscape.WebImage.Save(tmpShowContainer.ImagesContainer.Landscape.LocalFile)
            End If
        End If

        'Poster
        If Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Poster.LocalFile) AndAlso File.Exists(tmpShowContainer.ImagesContainer.Poster.LocalFile) Then
            tmpShowContainer.ImagesContainer.Poster.WebImage.FromFile(tmpShowContainer.ImagesContainer.Poster.LocalFile)
        ElseIf Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Poster.URL) AndAlso Not String.IsNullOrEmpty(tmpShowContainer.ImagesContainer.Poster.LocalFile) Then
            tmpShowContainer.ImagesContainer.Poster.WebImage.Clear()
            tmpShowContainer.ImagesContainer.Poster.WebImage.FromWeb(tmpShowContainer.ImagesContainer.Poster.URL)
            If tmpShowContainer.ImagesContainer.Poster.WebImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(tmpShowContainer.ImagesContainer.Poster.LocalFile).FullName)
                tmpShowContainer.ImagesContainer.Poster.WebImage.Save(tmpShowContainer.ImagesContainer.Poster.LocalFile)
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub bwLoadData_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadData.DoWork
        e.Cancel = Me.SetDefaults()
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
    ''' <summary>
    ''' Cache all images in Temp folder
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DownloadAllImages() As Boolean
        Dim iProgress As Integer = 1
        Dim uniqueID As String = String.Empty

        'get the TVDB ID for proper caching or cache all images in "mixed" folder
        If tmpShowContainer.TVShow Is Nothing OrElse (tmpShowContainer.TVShow IsNot Nothing AndAlso String.IsNullOrEmpty(tmpShowContainer.TVShow.TVDB)) Then
            uniqueID = Master.DB.LoadTVShowFromDB(tmpShowContainer.ShowID, False, False).TVShow.TVDB
        Else
            uniqueID = tmpShowContainer.TVShow.TVDB
        End If
        If String.IsNullOrEmpty(uniqueID) Then
            uniqueID = "mixed"
        End If

        Me.bwLoadImages.ReportProgress(SearchResultsContainer.EpisodeFanarts.Count + SearchResultsContainer.EpisodePosters.Count + SearchResultsContainer.SeasonBanners.Count + _
                                       SearchResultsContainer.SeasonFanarts.Count + SearchResultsContainer.SeasonLandscapes.Count + SearchResultsContainer.SeasonPosters.Count + _
                                       SearchResultsContainer.ShowBanners.Count + SearchResultsContainer.ShowCharacterArts.Count + SearchResultsContainer.ShowClearArts.Count + _
                                       SearchResultsContainer.ShowClearLogos.Count + SearchResultsContainer.ShowFanarts.Count + SearchResultsContainer.ShowLandscapes.Count + _
                                       SearchResultsContainer.ShowPosters.Count, "max")

        'Create caching paths

        'Banner Show
        For Each img In SearchResultsContainer.ShowBanners
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showbanners", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Banner Season
        For Each img In SearchResultsContainer.SeasonBanners
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "seasonbanners", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "seasonbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'CharacterArt Show
        For Each img In SearchResultsContainer.ShowCharacterArts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showcharacterarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showcharacterarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'ClearArt Show
        For Each img In SearchResultsContainer.ShowClearArts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showcleararts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showcleararts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'ClearLogo Show
        For Each img In SearchResultsContainer.ShowClearLogos
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showclearlogos", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showclearlogos\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Fanart Episode
        For Each img In SearchResultsContainer.EpisodeFanarts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "episodefanarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "episodefanarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Fanart Show
        For Each img In SearchResultsContainer.ShowFanarts
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showfanarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showfanarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Landscape Show
        For Each img In SearchResultsContainer.ShowLandscapes
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Landscape Season
        For Each img In SearchResultsContainer.SeasonLandscapes
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "seasonlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "seasonlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Poster Episode
        For Each img In SearchResultsContainer.EpisodePosters
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "episodeposters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "episodeposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Poster Show
        For Each img In SearchResultsContainer.ShowPosters
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showposters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "showposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Poster Season
        For Each img In SearchResultsContainer.SeasonPosters
            img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "seasonposters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            If Not String.IsNullOrEmpty(img.ThumbURL) Then
                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, uniqueID, Path.DirectorySeparatorChar, "seasonposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
            End If
        Next

        'Start images caching

        'Episode Fanarts
        If Me._ScrapeModifier.EpisodeFanart Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.EpisodeFanarts
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Episode Posters
        If Me._ScrapeModifier.EpisodePoster Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.EpisodePosters
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Poster / AllSeasons Poster
        If Me._ScrapeModifier.SeasonPoster OrElse Me._ScrapeModifier.AllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.SeasonPosters
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Banner / AllSeasons Banner
        If Me._ScrapeModifier.SeasonBanner OrElse Me._ScrapeModifier.AllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.SeasonBanners
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Fanart  /AllSeasons Fanart
        If Me._ScrapeModifier.SeasonFanart OrElse Me._ScrapeModifier.AllSeasonsFanart Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.SeasonFanarts
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Season Landscape  /AllSeasons Landscape
        If Me._ScrapeModifier.SeasonLandscape OrElse Me._ScrapeModifier.AllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.SeasonLandscapes
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show Poster / AllSeasons Poster
        If Me._ScrapeModifier.MainPoster OrElse Me._ScrapeModifier.AllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowPosters
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show Banner / AllSeasons Banner
        If Me._ScrapeModifier.MainBanner OrElse Me._ScrapeModifier.AllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowBanners
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show CharacterArt
        If Me._ScrapeModifier.MainCharacterArt Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowCharacterArts
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show ClearArt
        If Me._ScrapeModifier.MainClearArt Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowClearArts
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show ClearLogo
        If Me._ScrapeModifier.MainClearLogo Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowClearLogos
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show Landscape / AllSeasons Landscape
        If Me._ScrapeModifier.MainLandscape OrElse Me._ScrapeModifier.AllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowLandscapes
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        'Show Fanart / AllSeasons Fanart / Season Fanart / Episode Fanart
        If Me._ScrapeModifier.MainFanart OrElse Me._ScrapeModifier.AllSeasonsFanart OrElse Me._ScrapeModifier.SeasonFanart OrElse Me._ScrapeModifier.EpisodeFanart Then
            For Each tImg As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                CacheAndLoad(tImg)
                If Me.bwLoadImages.CancellationPending Then
                    Return True
                End If
                Me.bwLoadImages.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If

        Return False
    End Function

    Private Sub CacheAndLoad(ByRef tImage As MediaContainers.Image)
        If File.Exists(tImage.LocalFile) Then
            tImage.WebImage.FromFile(tImage.LocalFile)
        ElseIf File.Exists(tImage.LocalThumb) Then
            tImage.WebThumb.FromFile(tImage.LocalThumb)
        Else
            If Not String.IsNullOrEmpty(tImage.ThumbURL) Then
                tImage.WebThumb.FromWeb(tImage.ThumbURL)
                If tImage.WebThumb.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(tImage.LocalThumb).FullName)
                    tImage.WebThumb.Save(tImage.LocalThumb)
                End If
            ElseIf Not String.IsNullOrEmpty(tImage.URL) Then
                tImage.WebImage.FromWeb(tImage.URL)
                If tImage.WebImage.Image IsNot Nothing Then
                    Directory.CreateDirectory(Directory.GetParent(tImage.LocalFile).FullName)
                    tImage.WebImage.Save(tImage.LocalFile)
                End If
            End If
        End If
    End Sub

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
        If Me._ScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(658, "TV Show Banner"), .Tag = "showb", .ImageIndex = 0, .SelectedImageIndex = 0})
        If Me._ScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1011, "TV Show CharacterArt"), .Tag = "showch", .ImageIndex = 1, .SelectedImageIndex = 1})
        If Me._ScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1013, "TV Show ClearArt"), .Tag = "showca", .ImageIndex = 2, .SelectedImageIndex = 2})
        If Me._ScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1012, "TV Show ClearLogo"), .Tag = "showcl", .ImageIndex = 3, .SelectedImageIndex = 3})
        If Me._ScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(684, "TV Show Fanart"), .Tag = "showf", .ImageIndex = 4, .SelectedImageIndex = 4})
        If Me._ScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1010, "TV Show Landscape"), .Tag = "showl", .ImageIndex = 5, .SelectedImageIndex = 5})
        If Me._ScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(956, "TV Show Poster"), .Tag = "showp", .ImageIndex = 6, .SelectedImageIndex = 6})
        If Me._ScrapeModifier.AllSeasonsBanner AndAlso Master.eSettings.TVASBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1014, "All Seasons Banner"), .Tag = "allb", .ImageIndex = 0, .SelectedImageIndex = 0})
        If Me._ScrapeModifier.AllSeasonsFanart AndAlso Master.eSettings.TVASFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1015, "All Seasons Fanart"), .Tag = "allf", .ImageIndex = 4, .SelectedImageIndex = 4})
        If Me._ScrapeModifier.AllSeasonsLandscape AndAlso Master.eSettings.TVASLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1016, "All Seasons Landscape"), .Tag = "alll", .ImageIndex = 5, .SelectedImageIndex = 5})
        If Me._ScrapeModifier.AllSeasonsPoster AndAlso Master.eSettings.TVASPosterAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(735, "All Seasons Poster"), .Tag = "allp", .ImageIndex = 6, .SelectedImageIndex = 6})

        Dim TnS As TreeNode
        If tmpShowContainer.Seasons IsNot Nothing AndAlso tmpShowContainer.Seasons.Count > 0 Then
            For Each cSeason As Structures.DBTV In tmpShowContainer.Seasons.Where(Function(s) Not s.TVSeason.Season = 999).OrderBy(Function(s) s.TVSeason.Season)
                TnS = New TreeNode(String.Format(Master.eLang.GetString(726, "Season {0}"), cSeason.TVSeason.Season), 7, 7)
                If Master.eSettings.TVSeasonBannerAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1017, "Season Banner"), .Tag = String.Concat("b", cSeason.TVSeason.Season), .ImageIndex = 0, .SelectedImageIndex = 0})
                If Master.eSettings.TVSeasonFanartAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(686, "Season Fanart"), .Tag = String.Concat("f", cSeason.TVSeason.Season), .ImageIndex = 4, .SelectedImageIndex = 4})
                If Master.eSettings.TVSeasonLandscapeAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(1018, "Season Landscape"), .Tag = String.Concat("l", cSeason.TVSeason.Season), .ImageIndex = 5, .SelectedImageIndex = 5})
                If Master.eSettings.TVSeasonPosterAnyEnabled Then TnS.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(685, "Season Posters"), .Tag = String.Concat("p", cSeason.TVSeason.Season), .ImageIndex = 6, .SelectedImageIndex = 6})
                Me.tvList.Nodes.Add(TnS)
            Next
        ElseIf Me._ScrapeModifier.SeasonBanner Then
            If Master.eSettings.TVSeasonBannerAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(1019, "Season {0} Banner"), Me._season), .Tag = String.Concat("b", Me._season)})
        ElseIf Me._ScrapeModifier.SeasonFanart Then
            If Master.eSettings.TVSeasonFanartAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(962, "Season {0} Fanart"), Me._season), .Tag = String.Concat("f", Me._season)})
        ElseIf Me._ScrapeModifier.SeasonLandscape Then
            If Master.eSettings.TVSeasonLandscapeAnyEnabled Then Me.tvList.Nodes.Add(New TreeNode With {.Text = String.Format(Master.eLang.GetString(1020, "Season {0} Landscape"), Me._season), .Tag = String.Concat("l", Me._season)})
        ElseIf Me._ScrapeModifier.SeasonPoster Then
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
                tmpShowContainer.ImagesContainer.Banner = DefaultImagesContainer.Banner
                If DefaultImagesContainer.Banner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Banner.WebImage.Image
                ElseIf DefaultImagesContainer.Banner.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Banner.WebThumb.Image
                End If
                Me.pbCurrent.Tag = tmpShowContainer.ImagesContainer.Banner
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                tmpShowContainer.ImagesContainer.CharacterArt = DefaultImagesContainer.CharacterArt
                If DefaultImagesContainer.CharacterArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.CharacterArt.WebImage.Image
                ElseIf DefaultImagesContainer.Banner.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.CharacterArt.WebThumb.Image
                End If
                Me.pbCurrent.Tag = tmpShowContainer.ImagesContainer.CharacterArt
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                If DefaultImagesContainer.ClearArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearArt.WebImage.Image
                ElseIf DefaultImagesContainer.Banner.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearArt.WebThumb.Image
                End If
                Me.pbCurrent.Tag = tmpShowContainer.ImagesContainer.ClearArt
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                tmpShowContainer.ImagesContainer.ClearLogo = DefaultImagesContainer.ClearLogo
                If DefaultImagesContainer.ClearLogo.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearLogo.WebImage.Image
                ElseIf DefaultImagesContainer.Banner.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearLogo.WebThumb.Image
                End If
                Me.pbCurrent.Tag = tmpShowContainer.ImagesContainer.ClearLogo
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                tmpShowContainer.ImagesContainer.Fanart = DefaultImagesContainer.Fanart
                If DefaultImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Fanart.WebImage.Image
                ElseIf DefaultImagesContainer.Banner.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Fanart.WebThumb.Image
                End If
                Me.pbCurrent.Tag = tmpShowContainer.ImagesContainer.Fanart
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                tmpShowContainer.ImagesContainer.Poster = DefaultImagesContainer.Poster
                If DefaultImagesContainer.Poster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Poster.WebImage.Image
                ElseIf DefaultImagesContainer.Banner.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Poster.WebThumb.Image
                End If
                Me.pbCurrent.Tag = tmpShowContainer.ImagesContainer.Poster
            End If
        Else
            If SelImgType = Enums.ImageType_TV.SeasonBanner Then
                Dim sImg As MediaContainers.Image = DefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Banner = sImg
                If sImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebImage.Image
                ElseIf sImg.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebThumb.Image
                End If
                Me.pbCurrent.Tag = sImg
            ElseIf SelImgType = Enums.ImageType_TV.SeasonFanart Then
                Dim sImg As MediaContainers.Image = DefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Fanart = sImg
                If sImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebImage.Image
                ElseIf sImg.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebThumb.Image
                End If
                Me.pbCurrent.Tag = sImg
            ElseIf SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                Dim sImg As MediaContainers.Image = DefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Landscape = sImg
                If sImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebImage.Image
                ElseIf sImg.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebThumb.Image
                End If
                Me.pbCurrent.Tag = sImg
            ElseIf SelImgType = Enums.ImageType_TV.SeasonPoster Then
                Dim sImg As MediaContainers.Image = DefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Poster = sImg
                If sImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebImage.Image
                ElseIf sImg.WebThumb IsNot Nothing Then
                    Me.pbCurrent.Image = sImg.WebThumb.Image
                End If
                Me.pbCurrent.Tag = sImg
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
        ElseIf SelTag.WebThumb.Image IsNot Nothing Then
            Me.pbCurrent.Image = SelTag.WebThumb.Image
            Me.pbCurrent.Tag = SelTag
        End If

        If Me.SelSeason = -999 Then
            If SelImgType = Enums.ImageType_TV.ShowBanner Then
                tmpShowContainer.ImagesContainer.Banner = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowCharacterArt Then
                tmpShowContainer.ImagesContainer.CharacterArt = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearArt Then
                tmpShowContainer.ImagesContainer.ClearArt = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowClearLogo Then
                tmpShowContainer.ImagesContainer.ClearLogo = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowLandscape Then
                tmpShowContainer.ImagesContainer.Landscape = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowFanart Then
                tmpShowContainer.ImagesContainer.Fanart = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.ShowPoster Then
                tmpShowContainer.ImagesContainer.Poster = SelTag
            End If
        Else
            If SelImgType = Enums.ImageType_TV.AllSeasonsBanner OrElse SelImgType = Enums.ImageType_TV.SeasonBanner Then
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Banner = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsFanart OrElse SelImgType = Enums.ImageType_TV.SeasonFanart Then
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Fanart = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsLandscape OrElse SelImgType = Enums.ImageType_TV.SeasonLandscape Then
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Landscape = SelTag
            ElseIf SelImgType = Enums.ImageType_TV.AllSeasonsPoster OrElse SelImgType = Enums.ImageType_TV.SeasonPoster Then
                tmpShowContainer.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Poster = SelTag
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
                If tmpShowContainer.ImagesContainer.Banner IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Banner.WebImage IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Banner.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Banner.WebImage.Image
                ElseIf tmpShowContainer.ImagesContainer.Banner IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Banner.WebThumb IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Banner.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Banner.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowBanners
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'Show Characterart
            ElseIf e.Node.Tag.ToString = "showch" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowCharacterArt
                If tmpShowContainer.ImagesContainer.CharacterArt IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.CharacterArt.WebImage IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.CharacterArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.CharacterArt.WebImage.Image
                ElseIf tmpShowContainer.ImagesContainer.CharacterArt IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.CharacterArt.WebThumb IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.CharacterArt.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.CharacterArt.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowCharacterArts
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'Show ClearArt
            ElseIf e.Node.Tag.ToString = "showca" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearArt
                If tmpShowContainer.ImagesContainer.ClearArt IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearArt.WebImage IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearArt.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearArt.WebImage.Image
                ElseIf tmpShowContainer.ImagesContainer.ClearArt IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearArt.WebThumb IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearArt.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearArt.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowClearArts
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'Show ClearLogo
            ElseIf e.Node.Tag.ToString = "showcl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowClearLogo
                If tmpShowContainer.ImagesContainer.ClearLogo IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearLogo.WebImage IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearLogo.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearLogo.WebImage.Image
                ElseIf tmpShowContainer.ImagesContainer.ClearLogo IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearLogo.WebThumb IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.ClearLogo.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.ClearLogo.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowClearLogos
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'Show Fanart
            ElseIf e.Node.Tag.ToString = "showf" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowFanart
                If tmpShowContainer.ImagesContainer.Fanart IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Fanart.WebImage IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Fanart.WebImage.Image
                ElseIf tmpShowContainer.ImagesContainer.Fanart IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Fanart.WebThumb IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Fanart.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Fanart.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'Show Landscape
            ElseIf e.Node.Tag.ToString = "showl" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowLandscape
                If tmpShowContainer.ImagesContainer.Landscape IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Landscape.WebImage IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Landscape.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Landscape.WebImage.Image
                ElseIf tmpShowContainer.ImagesContainer.Landscape IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Landscape.WebThumb IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Landscape.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Landscape.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowLandscapes
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'Show Poster
            ElseIf e.Node.Tag.ToString = "showp" Then
                Me.SelSeason = -999
                Me.SelImgType = Enums.ImageType_TV.ShowPoster
                If tmpShowContainer.ImagesContainer.Poster IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Poster.WebImage IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Poster.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Poster.WebImage.Image
                ElseIf tmpShowContainer.ImagesContainer.Poster IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Poster.WebThumb IsNot Nothing AndAlso tmpShowContainer.ImagesContainer.Poster.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tmpShowContainer.ImagesContainer.Poster.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowPosters
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'AllSeasons Banner
            ElseIf e.Node.Tag.ToString = "allb" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsBanner
                Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Banner
                If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebImage.Image
                ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonBanners.Where(Function(s) s.Season = SelSeason)
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowBanners
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'AllSeasons Fanart
            ElseIf e.Node.Tag.ToString = "allf" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsFanart
                Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Fanart
                If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebImage.Image
                ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonFanarts.Where(Function(s) s.Season = SelSeason)
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'AllSeasons Landscape
            ElseIf e.Node.Tag.ToString = "alll" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsLandscape
                Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Landscape
                If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebImage.Image
                ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonLandscapes.Where(Function(s) s.Season = SelSeason)
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowLandscapes
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'AllSeasons Poster
            ElseIf e.Node.Tag.ToString = "allp" Then
                Me.SelSeason = 999
                Me.SelImgType = Enums.ImageType_TV.AllSeasonsPoster
                Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Poster
                If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebImage.Image
                ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                    Me.pbCurrent.Image = tImg.WebThumb.Image
                Else
                    Me.pbCurrent.Image = Nothing
                End If
                iCount = 0
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonPosters.Where(Function(s) s.Season = SelSeason)
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next
                For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowPosters
                    Me.AddImage(tvImage, iCount)
                    iCount += 1
                Next

                'Seasons
            Else
                Dim tMatch As Match = Regex.Match(e.Node.Tag.ToString, "(?<type>f|p|b|l)(?<num>[0-9]+)")
                If tMatch.Success Then

                    'Season Banner
                    If tMatch.Groups("type").Value = "b" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonBanner
                        Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Banner
                        If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebImage.Image
                        ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebThumb.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonBanners.Where(Function(s) s.Season = SelSeason)
                            Me.AddImage(tvImage, iCount)
                            iCount += 1
                        Next

                        'Season Fanart
                    ElseIf tMatch.Groups("type").Value = "f" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonFanart
                        Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Fanart
                        If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebImage.Image
                        ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebThumb.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonFanarts.Where(Function(s) s.Season = SelSeason)
                            Me.AddImage(tvImage, iCount)
                            iCount += 1
                        Next
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.ShowFanarts
                            Me.AddImage(tvImage, iCount)
                            iCount += 1
                        Next

                        'Season Landscape
                    ElseIf tMatch.Groups("type").Value = "l" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonLandscape
                        Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Landscape
                        If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebImage.Image
                        ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebThumb.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonLandscapes.Where(Function(s) s.Season = SelSeason)
                            Me.AddImage(tvImage, iCount)
                            iCount += 1
                        Next

                        'Season Poster
                    ElseIf tMatch.Groups("type").Value = "p" Then
                        Me.SelSeason = Convert.ToInt32(tMatch.Groups("num").Value)
                        Me.SelImgType = Enums.ImageType_TV.SeasonPoster
                        Dim tImg As MediaContainers.Image = tmpShowContainer.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = Me.SelSeason).ImagesContainer.Poster
                        If tImg IsNot Nothing AndAlso tImg.WebImage IsNot Nothing AndAlso tImg.WebImage.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebImage.Image
                        ElseIf tImg IsNot Nothing AndAlso tImg.WebThumb IsNot Nothing AndAlso tImg.WebThumb.Image IsNot Nothing Then
                            Me.pbCurrent.Image = tImg.WebThumb.Image
                        Else
                            Me.pbCurrent.Image = Nothing
                        End If
                        iCount = 0
                        For Each tvImage As MediaContainers.Image In SearchResultsContainer.SeasonPosters.Where(Function(s) s.Season = SelSeason)
                            Me.AddImage(tvImage, iCount)
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

#End Region 'Nested Types

End Class