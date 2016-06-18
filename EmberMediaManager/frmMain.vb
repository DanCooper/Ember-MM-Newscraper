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
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class frmMain

#Region "Fields"
    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCheckVersion As New ComponentModel.BackgroundWorker
    Friend WithEvents bwCleanDB As New ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadPic As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadEpInfo As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieInfo As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieSetInfo As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieSetPosters As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadSeasonInfo As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadShowInfo As New ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieSetScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_Movies As New ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_MovieSets As New ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_TVShows As New ComponentModel.BackgroundWorker
    Friend WithEvents bwRewrite_Movies As New ComponentModel.BackgroundWorker
    Friend WithEvents bwTVScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwTVEpisodeScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwTVSeasonScraper As New ComponentModel.BackgroundWorker

    Public fCommandLine As New CommandLine

    Private TaskList As New List(Of Task)
    Private TasksDone As Boolean = True

    Private alActors As New List(Of String)
    Private FilterPanelIsRaised_Movie As Boolean = False
    Private FilterPanelIsRaised_MovieSet As Boolean = False
    Private FilterPanelIsRaised_TVShow As Boolean = False
    Private InfoPanelState_Movie As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private InfoPanelState_MovieSet As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private InfoPanelState_TVShow As Integer = 0 '0 = down, 1 = mid, 2 = up

    Private bsMovies As New BindingSource
    Private bsMovieSets As New BindingSource
    Private bsTVEpisodes As New BindingSource
    Private bsTVSeasons As New BindingSource
    Private bsTVShows As New BindingSource

    Private dtMovies As New DataTable
    Private dtMovieSets As New DataTable
    Private dtTVEpisodes As New DataTable
    Private dtTVSeasons As New DataTable
    Private dtTVShows As New DataTable

    Private fScanner As New Scanner
    Private fTaskManager As New TaskManager

    Private GenreImage As Image
    Private InfoCleared As Boolean = False
    Private LoadingDone As Boolean = False
    Private MainActors As New Images
    Private MainBanner As New Images
    Private MainCharacterArt As New Images
    Private MainClearArt As New Images
    Private MainClearLogo As New Images
    Private MainDiscArt As New Images
    Private MainFanart As New Images
    Private MainFanartSmall As New Images
    Private MainLandscape As New Images
    Private MainPoster As New Images
    Private pbGenre() As PictureBox = Nothing
    Private pnlGenre() As Panel = Nothing
    Private ReportDownloadPercent As Boolean = False
    Private ScraperDone As Boolean = False
    Private sHTTP As New HTTP

    'Loading Delays
    Private currRow_Movie As Integer = -1
    Private currRow_MovieSet As Integer = -1
    Private currRow_TVEpisode As Integer = -1
    Private currRow_TVSeason As Integer = -1
    Private currRow_TVShow As Integer = -1
    Private currList As Integer = 0
    Private currThemeType As Theming.ThemeType
    Private prevRow_Movie As Integer = -1
    Private prevRow_MovieSet As Integer = -1
    Private prevRow_TVEpisode As Integer = -1
    Private prevRow_TVSeason As Integer = -1
    Private prevRow_TVShow As Integer = -1

    'list movies
    Private currList_Movies As String = "movielist" 'default movie list SQLite view
    Private listViews_Movies As New Dictionary(Of String, String)

    'list moviesets
    Private currList_MovieSets As String = "setslist" 'default moviesets list SQLite view
    Private listViews_MovieSets As New Dictionary(Of String, String)

    'list shows
    Private currList_TVShows As String = "tvshowlist" 'default tv show list SQLite view
    Private listViews_TVShows As New Dictionary(Of String, String)

    'filter movies
    Private bDoingSearch_Movies As Boolean = False
    Private FilterArray_Movies As New List(Of String)
    Private filDataField_Movies As String = String.Empty
    Private filSearch_Movies As String = String.Empty
    Private filSource_Movies As String = String.Empty
    Private filYear_Movies As String = String.Empty
    Private filGenre_Movies As String = String.Empty
    Private filCountry_Movies As String = String.Empty
    Private filMissing_Movies As String = String.Empty
    Private currTextSearch_Movies As String = String.Empty
    Private prevTextSearch_Movies As String = String.Empty

    'filter moviesets
    Private bDoingSearch_MovieSets As Boolean = False
    Private FilterArray_MovieSets As New List(Of String)
    Private filSearch_MovieSets As String = String.Empty
    Private filMissing_MovieSets As String = String.Empty
    Private currTextSearch_MovieSets As String = String.Empty
    Private prevTextSearch_MovieSets As String = String.Empty

    'filter shows
    Private bDoingSearch_TVShows As Boolean = False
    Private FilterArray_TVShows As New List(Of String)
    Private filSearch_TVShows As String = String.Empty
    Private filSource_TVShows As String = String.Empty
    Private filGenre_TVShows As String = String.Empty
    Private filMissing_TVShows As String = String.Empty
    Private currTextSearch_TVShows As String = String.Empty
    Private prevTextSearch_TVShows As String = String.Empty

    'Theme Information
    Private _bannermaxheight As Integer = 160
    Private _bannermaxwidth As Integer = 285
    Private _characterartmaxheight As Integer = 160
    Private _characterartmaxwidth As Integer = 160
    Private _clearartmaxheight As Integer = 160
    Private _clearartmaxwidth As Integer = 285
    Private _clearlogomaxheight As Integer = 160
    Private _clearlogomaxwidth As Integer = 285
    Private _discartmaxheight As Integer = 160
    Private _discartmaxwidth As Integer = 160
    Private _postermaxheight As Integer = 160
    Private _postermaxwidth As Integer = 160
    Private _fanartsmallmaxheight As Integer = 160
    Private _fanartsmallmaxwidth As Integer = 285
    Private _landscapemaxheight As Integer = 160
    Private _landscapemaxwidth As Integer = 285
    Private tTheme As New Theming
    Private _genrepanelcolor As Color = Color.Gainsboro
    Private _ipmid As Integer = 280
    Private _ipup As Integer = 500
    Private CloseApp As Boolean = False

    Private _SelectedScrapeOption As String = String.Empty
    Private _SelectedScrapeType As String = String.Empty
    Private _SelectedScrapeTypeMode As String = String.Empty
    Private _SelectedContentType As String = String.Empty

    Private oldStatus As String = String.Empty

    Private KeyBuffer As String = String.Empty

    Private currMovie As Database.DBElement
    Private currMovieSet As Database.DBElement
    Private currTV As Database.DBElement

#End Region 'Fields

#Region "Delegates"

    Delegate Sub DelegateEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Delegate Sub DelegateEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Delegate Sub DelegateEvent_TVShow(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)

    Delegate Sub Delegate_dtListAddRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
    Delegate Sub Delegate_dtListRemoveRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
    Delegate Sub Delegate_dtListUpdateRow(ByVal dRow As DataRow, ByVal v As DataRow)

    Delegate Sub MySettingsShow(ByVal dlg As dlgSettings)

#End Region 'Delegates

#Region "Properties"

    Public Property GenrePanelColor() As Color
        Get
            Return _genrepanelcolor
        End Get
        Set(ByVal value As Color)
            _genrepanelcolor = value
        End Set
    End Property

    Public Property IPMid() As Integer
        Get
            Return _ipmid
        End Get
        Set(ByVal value As Integer)
            _ipmid = value
        End Set
    End Property

    Public Property IPUp() As Integer
        Get
            Return _ipup
        End Get
        Set(ByVal value As Integer)
            _ipup = value
        End Set
    End Property

    Public Property BannerMaxHeight() As Integer
        Get
            Return _bannermaxheight
        End Get
        Set(ByVal value As Integer)
            _bannermaxheight = value
        End Set
    End Property

    Public Property BannerMaxWidth() As Integer
        Get
            Return _bannermaxwidth
        End Get
        Set(ByVal value As Integer)
            _bannermaxwidth = value
        End Set
    End Property

    Public Property CharacterArtMaxHeight() As Integer
        Get
            Return _characterartmaxheight
        End Get
        Set(ByVal value As Integer)
            _characterartmaxheight = value
        End Set
    End Property

    Public Property CharacterArtMaxWidth() As Integer
        Get
            Return _characterartmaxwidth
        End Get
        Set(ByVal value As Integer)
            _characterartmaxwidth = value
        End Set
    End Property

    Public Property ClearArtMaxHeight() As Integer
        Get
            Return _clearartmaxheight
        End Get
        Set(ByVal value As Integer)
            _clearartmaxheight = value
        End Set
    End Property

    Public Property ClearArtMaxWidth() As Integer
        Get
            Return _clearartmaxwidth
        End Get
        Set(ByVal value As Integer)
            _clearartmaxwidth = value
        End Set
    End Property

    Public Property ClearLogoMaxHeight() As Integer
        Get
            Return _clearlogomaxheight
        End Get
        Set(ByVal value As Integer)
            _clearlogomaxheight = value
        End Set
    End Property

    Public Property ClearLogoMaxWidth() As Integer
        Get
            Return _clearlogomaxwidth
        End Get
        Set(ByVal value As Integer)
            _clearlogomaxwidth = value
        End Set
    End Property

    Public Property DiscArtMaxHeight() As Integer
        Get
            Return _discartmaxheight
        End Get
        Set(ByVal value As Integer)
            _discartmaxheight = value
        End Set
    End Property

    Public Property DiscArtMaxWidth() As Integer
        Get
            Return _discartmaxwidth
        End Get
        Set(ByVal value As Integer)
            _discartmaxwidth = value
        End Set
    End Property

    Public Property PosterMaxHeight() As Integer
        Get
            Return _postermaxheight
        End Get
        Set(ByVal value As Integer)
            _postermaxheight = value
        End Set
    End Property

    Public Property PosterMaxWidth() As Integer
        Get
            Return _postermaxwidth
        End Get
        Set(ByVal value As Integer)
            _postermaxwidth = value
        End Set
    End Property

    Public Property FanartSmallMaxHeight() As Integer
        Get
            Return _fanartsmallmaxheight
        End Get
        Set(ByVal value As Integer)
            _fanartsmallmaxheight = value
        End Set
    End Property

    Public Property FanartSmallMaxWidth() As Integer
        Get
            Return _fanartsmallmaxwidth
        End Get
        Set(ByVal value As Integer)
            _fanartsmallmaxwidth = value
        End Set
    End Property

    Public Property LandscapeMaxHeight() As Integer
        Get
            Return _landscapemaxheight
        End Get
        Set(ByVal value As Integer)
            _landscapemaxheight = value
        End Set
    End Property

    Public Property LandscapeMaxWidth() As Integer
        Get
            Return _landscapemaxwidth
        End Get
        Set(ByVal value As Integer)
            _landscapemaxwidth = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub ClearInfo()
        InfoCleared = True

        If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
        If bwLoadMovieInfo.IsBusy Then bwLoadMovieInfo.CancelAsync()
        If bwLoadMovieSetInfo.IsBusy Then bwLoadMovieSetInfo.CancelAsync()
        If bwLoadMovieSetPosters.IsBusy Then bwLoadMovieSetPosters.CancelAsync()
        If bwLoadShowInfo.IsBusy Then bwLoadShowInfo.CancelAsync()
        If bwLoadSeasonInfo.IsBusy Then bwLoadSeasonInfo.CancelAsync()
        If bwLoadEpInfo.IsBusy Then bwLoadEpInfo.CancelAsync()

        While bwDownloadPic.IsBusy OrElse bwLoadMovieInfo.IsBusy OrElse bwLoadMovieSetInfo.IsBusy OrElse
                    bwLoadShowInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse
                    bwLoadMovieSetPosters.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If pbFanart.Image IsNot Nothing Then
            pbFanart.Image.Dispose()
            pbFanart.Image = Nothing
        End If
        MainFanart.Clear()

        If pbBanner.Image IsNot Nothing Then
            pbBanner.Image.Dispose()
            pbBanner.Image = Nothing
        End If
        pnlBanner.Visible = False
        MainBanner.Clear()

        If pbCharacterArt.Image IsNot Nothing Then
            pbCharacterArt.Image.Dispose()
            pbCharacterArt.Image = Nothing
        End If
        pnlCharacterArt.Visible = False
        MainCharacterArt.Clear()

        If pbClearArt.Image IsNot Nothing Then
            pbClearArt.Image.Dispose()
            pbClearArt.Image = Nothing
        End If
        pnlClearArt.Visible = False
        MainClearArt.Clear()

        If pbClearLogo.Image IsNot Nothing Then
            pbClearLogo.Image.Dispose()
            pbClearLogo.Image = Nothing
        End If
        pnlClearLogo.Visible = False
        MainClearLogo.Clear()

        If pbPoster.Image IsNot Nothing Then
            pbPoster.Image.Dispose()
            pbPoster.Image = Nothing
        End If
        pnlPoster.Visible = False
        MainPoster.Clear()

        If pbFanartSmall.Image IsNot Nothing Then
            pbFanartSmall.Image.Dispose()
            pbFanartSmall.Image = Nothing
        End If
        pnlFanartSmall.Visible = False
        MainFanartSmall.Clear()

        If pbLandscape.Image IsNot Nothing Then
            pbLandscape.Image.Dispose()
            pbLandscape.Image = Nothing
        End If
        pnlLandscape.Visible = False
        MainLandscape.Clear()

        If pbDiscArt.Image IsNot Nothing Then
            pbDiscArt.Image.Dispose()
            pbDiscArt.Image = Nothing
        End If
        pnlDiscArt.Visible = False
        MainDiscArt.Clear()

        'remove all the current genres
        Try
            For iDel As Integer = 0 To pnlGenre.Count - 1
                scMain.Panel2.Controls.Remove(pbGenre(iDel))
                scMain.Panel2.Controls.Remove(pnlGenre(iDel))
            Next
        Catch
        End Try

        If pbMPAA.Image IsNot Nothing Then
            pbMPAA.Image = Nothing
        End If
        pnlMPAA.Visible = False

        lblFanartSmallSize.Text = String.Empty
        lblTitle.Text = String.Empty
        lblOriginalTitle.Text = String.Empty
        lblPosterSize.Text = String.Empty
        lblRating.Text = String.Empty
        lblRuntime.Text = String.Empty
        lblStudio.Text = String.Empty
        pnlTop250.Visible = False
        lblTop250.Text = String.Empty
        pbStar1.Image = Nothing
        pbStar2.Image = Nothing
        pbStar3.Image = Nothing
        pbStar4.Image = Nothing
        pbStar5.Image = Nothing
        pbStar6.Image = Nothing
        pbStar7.Image = Nothing
        pbStar8.Image = Nothing
        pbStar9.Image = Nothing
        pbStar10.Image = Nothing
        ToolTips.SetToolTip(pbStar1, "")
        ToolTips.SetToolTip(pbStar2, "")
        ToolTips.SetToolTip(pbStar3, "")
        ToolTips.SetToolTip(pbStar4, "")
        ToolTips.SetToolTip(pbStar5, "")
        ToolTips.SetToolTip(pbStar6, "")
        ToolTips.SetToolTip(pbStar7, "")
        ToolTips.SetToolTip(pbStar8, "")
        ToolTips.SetToolTip(pbStar9, "")
        ToolTips.SetToolTip(pbStar10, "")

        lstActors.Items.Clear()
        If alActors IsNot Nothing Then
            alActors.Clear()
            alActors = Nothing
        End If
        If pbActors.Image IsNot Nothing Then
            pbActors.Image.Dispose()
            pbActors.Image = Nothing
        End If
        MainActors.Clear()
        lblDirectors.Text = String.Empty
        lblReleaseDate.Text = String.Empty
        txtCertifications.Text = String.Empty
        txtIMDBID.Text = String.Empty
        txtFilePath.Text = String.Empty
        txtOutline.Text = String.Empty
        txtPlot.Text = String.Empty
        txtTMDBID.Text = String.Empty
        txtTrailerPath.Text = String.Empty
        lblTagline.Text = String.Empty
        If pbMPAA.Image IsNot Nothing Then
            pbMPAA.Image.Dispose()
            pbMPAA.Image = Nothing
        End If
        pbStudio.Image = Nothing
        pbVideo.Image = Nothing
        pbVType.Image = Nothing
        pbAudio.Image = Nothing
        pbResolution.Image = Nothing
        pbChannels.Image = Nothing
        pbAudioLang0.Image = Nothing
        pbAudioLang1.Image = Nothing
        pbAudioLang2.Image = Nothing
        pbAudioLang3.Image = Nothing
        pbAudioLang4.Image = Nothing
        pbAudioLang5.Image = Nothing
        pbAudioLang6.Image = Nothing
        ToolTips.SetToolTip(pbAudioLang0, "")
        ToolTips.SetToolTip(pbAudioLang1, "")
        ToolTips.SetToolTip(pbAudioLang2, "")
        ToolTips.SetToolTip(pbAudioLang3, "")
        ToolTips.SetToolTip(pbAudioLang4, "")
        ToolTips.SetToolTip(pbAudioLang5, "")
        ToolTips.SetToolTip(pbAudioLang6, "")
        pbSubtitleLang0.Image = Nothing
        pbSubtitleLang1.Image = Nothing
        pbSubtitleLang2.Image = Nothing
        pbSubtitleLang3.Image = Nothing
        pbSubtitleLang4.Image = Nothing
        pbSubtitleLang5.Image = Nothing
        pbSubtitleLang6.Image = Nothing
        ToolTips.SetToolTip(pbSubtitleLang0, "")
        ToolTips.SetToolTip(pbSubtitleLang1, "")
        ToolTips.SetToolTip(pbSubtitleLang2, "")
        ToolTips.SetToolTip(pbSubtitleLang3, "")
        ToolTips.SetToolTip(pbSubtitleLang4, "")
        ToolTips.SetToolTip(pbSubtitleLang5, "")
        ToolTips.SetToolTip(pbSubtitleLang6, "")

        txtMetaData.Text = String.Empty
        pnlTop.Visible = False
        '.tslStatus.Text = String.Empty

        lvMoviesInSet.Items.Clear()
        ilMoviesInSet.Images.Clear()

        Application.DoEvents()
    End Sub

    Private Function CheckColumnHide_Movies(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.MovieGeneralMediaListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_MovieSets(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.MovieSetGeneralMediaListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_TVEpisodes(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.TVGeneralEpisodeListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_TVSeasons(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.TVGeneralSeasonListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_TVShows(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.TVGeneralShowListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Public Sub LoadMedia(ByVal Scan As Structures.ScanOrClean, Optional ByVal SourceID As Long = -1, Optional ByVal Folder As String = "")
        Try
            SetStatus(Master.eLang.GetString(116, "Performing Preliminary Tasks (Gathering Data)..."))
            tspbLoading.ProgressBar.Style = ProgressBarStyle.Marquee
            tspbLoading.Visible = True

            Application.DoEvents()

            ClearInfo()
            'ClearFilters_Movies()
            ClearFilters_MovieSets()
            ClearFilters_Shows()
            'EnableFilters_Movies(False)
            EnableFilters_MovieSets(False)
            EnableFilters_Shows(False)

            SetControlsEnabled(False)
            'txtSearchMovies.Text = String.Empty
            txtSearchMovieSets.Text = String.Empty
            txtSearchShows.Text = String.Empty

            fScanner.CancelAndWait()

            If Scan.MovieSets Then
                prevRow_MovieSet = -1
                dgvMovieSets.DataSource = Nothing
            End If

            If Scan.TV Then
                currList = 0
                prevRow_TVShow = -1
                prevRow_TVSeason = -1
                prevRow_TVEpisode = -1
                dgvTVShows.DataSource = Nothing
                dgvTVSeasons.DataSource = Nothing
                dgvTVEpisodes.DataSource = Nothing
            End If

            fScanner.Start(Scan, SourceID, Folder)

        Catch ex As Exception
            LoadingDone = True
            EnableFilters_Movies(True)
            EnableFilters_MovieSets(True)
            EnableFilters_Shows(True)
            SetControlsEnabled(True)
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpAbout.Click
        Using dAbout As New dlgAbout
            dAbout.ShowDialog()
        End Using
    End Sub

    Private Sub mnuMainToolsExportMovies_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportMovies.Click
        Try
            Dim table As New DataTable
            Dim ds As New DataSet
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "SELECT * FROM movie INNER JOIN MoviesVStreams ON (MoviesVStreams.MovieID = movie.idMovie) INNER JOIN MoviesAStreams ON (MoviesAStreams.MovieID = movie.idMovie);"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    ds.Tables.Add(table)
                    ds.EnforceConstraints = False
                    table.Load(SQLreader)
                End Using
            End Using

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.FileName = "export_movies" + ".xml"
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml"
            saveFileDialog1.FilterIndex = 2
            saveFileDialog1.RestoreDirectory = True

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                table.WriteXml(saveFileDialog1.FileName)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub mnuMainToolsExportTvShows_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportTvShows.Click
        Try
            Dim table As New DataTable
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "Select * from tvshow;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    'Load the SqlDataReader object to the DataTable object as follows. 
                    table.Load(SQLreader)
                End Using
            End Using

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.FileName = "export_tvshows" + ".xml"
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml"
            saveFileDialog1.FilterIndex = 2
            saveFileDialog1.RestoreDirectory = True

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                table.WriteXml(saveFileDialog1.FileName)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub ApplyTheme(ByVal tType As Theming.ThemeType)
        pnlInfoPanel.SuspendLayout()

        currThemeType = tType

        tTheme.ApplyTheme(tType)

        tmrAni.Stop()

        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)

        Select Case If(currMainTabTag.ContentType = Enums.ContentType.Movie, InfoPanelState_Movie, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, InfoPanelState_MovieSet, InfoPanelState_TVShow))
            Case 1
                If btnMid.Visible Then
                    pnlInfoPanel.Height = _ipmid
                    btnUp.Enabled = True
                    btnMid.Enabled = False
                    btnDown.Enabled = True
                ElseIf btnUp.Visible Then
                    pnlInfoPanel.Height = _ipup
                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        InfoPanelState_Movie = 2
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        InfoPanelState_MovieSet = 2
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        InfoPanelState_TVShow = 2
                    End If
                    btnUp.Enabled = False
                    btnMid.Enabled = True
                    btnDown.Enabled = True
                Else
                    pnlInfoPanel.Height = 25
                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        InfoPanelState_Movie = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        InfoPanelState_MovieSet = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        InfoPanelState_TVShow = 0
                    End If
                    btnUp.Enabled = True
                    btnMid.Enabled = True
                    btnDown.Enabled = False
                End If
            Case 2
                If btnUp.Visible Then
                    pnlInfoPanel.Height = _ipup
                    btnUp.Enabled = False
                    btnMid.Enabled = True
                    btnDown.Enabled = True
                ElseIf btnMid.Visible Then
                    pnlInfoPanel.Height = _ipmid

                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        InfoPanelState_Movie = 1
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        InfoPanelState_MovieSet = 1
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        InfoPanelState_TVShow = 1
                    End If

                    btnUp.Enabled = True
                    btnMid.Enabled = False
                    btnDown.Enabled = True
                Else
                    pnlInfoPanel.Height = 25
                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        InfoPanelState_Movie = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        InfoPanelState_MovieSet = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        InfoPanelState_TVShow = 0
                    End If
                    btnUp.Enabled = True
                    btnMid.Enabled = True
                    btnDown.Enabled = False
                End If
            Case Else
                pnlInfoPanel.Height = 25
                If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                    InfoPanelState_Movie = 0
                ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                    InfoPanelState_MovieSet = 0
                ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                    InfoPanelState_TVShow = 0
                End If

                btnUp.Enabled = True
                btnMid.Enabled = True
                btnDown.Enabled = False
        End Select

        pbActLoad.Visible = False
        pbActors.Image = My.Resources.actor_silhouette
        pbMILoading.Visible = False

        pnlInfoPanel.ResumeLayout()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancel.Visible = False
        lblCanceling.Visible = True
        prbCanceling.Visible = True

        If bwMovieScraper.IsBusy Then bwMovieScraper.CancelAsync()
        If bwMovieSetScraper.IsBusy Then bwMovieSetScraper.CancelAsync()
        If bwReload_Movies.IsBusy Then bwReload_Movies.CancelAsync()
        If bwReload_MovieSets.IsBusy Then bwReload_MovieSets.CancelAsync()
        If bwReload_TVShows.IsBusy Then bwReload_TVShows.CancelAsync()
        If bwRewrite_Movies.IsBusy Then bwRewrite_Movies.CancelAsync()
        If bwTVEpisodeScraper.IsBusy Then bwTVEpisodeScraper.CancelAsync()
        If bwTVScraper.IsBusy Then bwTVScraper.CancelAsync()
        If bwTVSeasonScraper.IsBusy Then bwTVSeasonScraper.CancelAsync()
        While bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
            bwReload_TVShows.IsBusy OrElse bwRewrite_Movies.IsBusy OrElse bwTVEpisodeScraper.IsBusy OrElse bwTVScraper.IsBusy OrElse
            bwTVSeasonScraper.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub btnClearFilters_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters_Movies.Click
        ClearFilters_Movies(True)
    End Sub

    Private Sub btnClearFilters_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters_MovieSets.Click
        ClearFilters_MovieSets(True)
    End Sub

    Private Sub btnClearFilters_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters_Shows.Click
        ClearFilters_Shows(True)
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        tcMain.Focus()
        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            InfoPanelState_Movie = 0
        ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
            InfoPanelState_MovieSet = 0
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            InfoPanelState_TVShow = 0
        End If
        tmrAni.Start()
    End Sub

    Private Sub btnFilterDown_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDown_Movies.Click
        FilterPanelIsRaised_Movie = False
        FilterMovement_Movies()
    End Sub

    Private Sub btnFilterDown_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDown_MovieSets.Click
        FilterPanelIsRaised_MovieSet = False
        FilterMovement_MovieSets()
    End Sub

    Private Sub btnFilterDown_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDown_Shows.Click
        FilterPanelIsRaised_TVShow = False
        FilterMovement_Shows()
    End Sub

    Private Sub btnFilterUp_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterUp_Movies.Click
        FilterPanelIsRaised_Movie = True
        FilterMovement_Movies()
    End Sub

    Private Sub btnFilterUp_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterUp_MovieSets.Click

        FilterPanelIsRaised_MovieSet = True
        FilterMovement_MovieSets()
    End Sub

    Private Sub btnFilterUp_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterUp_Shows.Click
        FilterPanelIsRaised_TVShow = True
        FilterMovement_Shows()
    End Sub



    Private Sub btnMarkAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkAll.Click
        Try
            Dim MarkAll As Boolean = Not btnMarkAll.Text = Master.eLang.GetString(105, "Unmark All")
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    SQLcommand.CommandText = "UPDATE movie SET mark = (?);"
                    parMark.Value = MarkAll
                    SQLcommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using
            For Each drvRow As DataRow In dtMovies.Rows
                drvRow.Item("Mark") = MarkAll
            Next
            dgvMovies.Refresh()
            btnMarkAll.Text = If(Not MarkAll, Master.eLang.GetString(35, "Mark All"), Master.eLang.GetString(105, "Unmark All"))
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMid.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        tcMain.Focus()
        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            InfoPanelState_Movie = 1
        ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
            InfoPanelState_MovieSet = 1
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            InfoPanelState_TVShow = 1
        End If
        tmrAni.Start()
    End Sub

    Private Sub btnMIRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMetaDataRefresh.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)

        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            If dgvMovies.SelectedRows.Count = 1 Then
                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainMeta, True)
                CreateScrapeList_Movie(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_Movie, ScrapeModifiers)
            End If
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            If dgvMovies.SelectedRows.Count = 1 AndAlso Not String.IsNullOrEmpty(currTV.Filename) Then
                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeMeta, True)
                CreateScrapeList_TVEpisode(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_TV, ScrapeModifiers)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Launch video using system default player
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilePlay.Click
        Functions.Launch(txtFilePath.Text, True)
        'Try
        '    If Not String.IsNullOrEmpty(Me.txtFilePath.Text) Then
        '        If File.Exists(Me.txtFilePath.Text) Then
        '            If Master.isWindows Then
        '                Process.Start(String.Concat("""", Me.txtFilePath.Text, """"))
        '            Else
        '                Using Explorer As New Process
        '                    Explorer.StartInfo.FileName = "xdg-open"
        '                    Explorer.StartInfo.Arguments = String.Format("""{0}""", Me.txtFilePath.Text)
        '                    Explorer.Start()
        '                End Using
        '            End If

        '        End If
        '    End If
        'Catch ex As Exception
        '    logger.Error(ex, New StackFrame().GetMethod().Name)
        'End Try
    End Sub
    ''' <summary>
    ''' Launch trailer using system default player
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTrailerPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrailerPlay.Click
        If txtTrailerPath.Text.StartsWith("plugin://plugin.video.youtube") Then
            Functions.Launch(StringUtils.ConvertFromKodiTrailerFormatToYouTubeURL(txtTrailerPath.Text), True)
        Else
            Functions.Launch(txtTrailerPath.Text, True)
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by adding date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the newest title on the top of the list</remarks>
    Private Sub btnFilterSortDateAdded_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortDateAdded_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortDateAdded_Movies.Tag.ToString = "DESC" Then
                btnFilterSortDateAdded_Movies.Tag = "ASC"
                btnFilterSortDateAdded_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns("DateAdded"), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortDateAdded_Movies.Tag = "DESC"
                btnFilterSortDateAdded_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns("DateAdded"), System.ComponentModel.ListSortDirection.Descending)
            End If

            SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by last modification date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the latest modified title on the top of the list</remarks>
    Private Sub btnFilterSortDateModified_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortDateModified_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortDateModified_Movies.Tag.ToString = "DESC" Then
                btnFilterSortDateModified_Movies.Tag = "ASC"
                btnFilterSortDateModified_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns("DateModified"), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortDateModified_Movies.Tag = "DESC"
                btnFilterSortDateModified_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns("DateModified"), System.ComponentModel.ListSortDirection.Descending)
            End If

            SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by sort title
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFilterSortTitle_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortTitle_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortTitle_Movies.Tag.ToString = "ASC" Then
                btnFilterSortTitle_Movies.Tag = "DSC"
                btnFilterSortTitle_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns("SortedTitle"), System.ComponentModel.ListSortDirection.Descending)
            Else
                btnFilterSortTitle_Movies.Tag = "ASC"
                btnFilterSortTitle_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns("SortedTitle"), System.ComponentModel.ListSortDirection.Ascending)
            End If

            SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the tvshowlist by sort title
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFilterSortTitle_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortTitle_Shows.Click
        If dgvTVShows.RowCount > 0 Then
            'Me.btnFilterSortDateAdded_Shows.Tag = String.Empty
            'Me.btnFilterSortDateAdded_Shows.Image = Nothing
            'Me.btnFilterSortDateModified_Shows.Tag = String.Empty
            'Me.btnFilterSortDateModified_Shows.Image = Nothing
            'Me.btnFilterSortRating_Shows.Tag = String.Empty
            'Me.btnFilterSortRating_Shows.Image = Nothing
            'Me.btnFilterSortYear_Shows.Tag = String.Empty
            'Me.btnFilterSortYear_Shows.Image = Nothing
            If btnFilterSortTitle_Shows.Tag.ToString = "ASC" Then
                btnFilterSortTitle_Shows.Tag = "DSC"
                btnFilterSortTitle_Shows.Image = My.Resources.desc
                dgvTVShows.Sort(dgvTVShows.Columns("SortedTitle"), System.ComponentModel.ListSortDirection.Descending)
            Else
                btnFilterSortTitle_Shows.Tag = "ASC"
                btnFilterSortTitle_Shows.Image = My.Resources.asc
                dgvTVShows.Sort(dgvTVShows.Columns("SortedTitle"), System.ComponentModel.ListSortDirection.Ascending)
            End If

            SaveFilter_Shows()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by rating
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the highest rated title on the top of the list</remarks>
    Private Sub btnFilterSortRating_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortRating_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortRating_Movies.Tag.ToString = "DESC" Then
                btnFilterSortRating_Movies.Tag = "ASC"
                btnFilterSortRating_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns("Rating"), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortRating_Movies.Tag = "DESC"
                btnFilterSortRating_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns("Rating"), System.ComponentModel.ListSortDirection.Descending)
            End If

            SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by year
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the highest year title on the top of the list</remarks>
    Private Sub btnFilterSortYear_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortYear_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            If btnFilterSortYear_Movies.Tag.ToString = "DESC" Then
                btnFilterSortYear_Movies.Tag = "ASC"
                btnFilterSortYear_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns("Year"), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortYear_Movies.Tag = "DESC"
                btnFilterSortYear_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns("Year"), System.ComponentModel.ListSortDirection.Descending)
            End If

            SaveFilter_Movies()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        tcMain.Focus()
        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            InfoPanelState_Movie = 2
        ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
            InfoPanelState_MovieSet = 2
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            InfoPanelState_TVShow = 2
        End If
        tmrAni.Start()
    End Sub

    Private Sub BuildStars(ByVal sinRating As Single)
        Try
            pbStar1.Image = Nothing
            pbStar2.Image = Nothing
            pbStar3.Image = Nothing
            pbStar4.Image = Nothing
            pbStar5.Image = Nothing
            pbStar6.Image = Nothing
            pbStar7.Image = Nothing
            pbStar8.Image = Nothing
            pbStar9.Image = Nothing
            pbStar10.Image = Nothing

            Dim tTip As String = String.Concat(Master.eLang.GetString(245, "Rating:"), String.Format(" {0:N}", sinRating))
            ToolTips.SetToolTip(pbStar1, tTip)
            ToolTips.SetToolTip(pbStar2, tTip)
            ToolTips.SetToolTip(pbStar3, tTip)
            ToolTips.SetToolTip(pbStar4, tTip)
            ToolTips.SetToolTip(pbStar5, tTip)
            ToolTips.SetToolTip(pbStar6, tTip)
            ToolTips.SetToolTip(pbStar7, tTip)
            ToolTips.SetToolTip(pbStar8, tTip)
            ToolTips.SetToolTip(pbStar9, tTip)
            ToolTips.SetToolTip(pbStar10, tTip)

            If sinRating >= 0.5 Then ' if rating is less than .5 out of ten, consider it a 0
                Select Case (sinRating)
                    Case Is <= 0.5
                        pbStar1.Image = My.Resources.starhalf
                        pbStar2.Image = My.Resources.starempty
                        pbStar3.Image = My.Resources.starempty
                        pbStar4.Image = My.Resources.starempty
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 1
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.starempty
                        pbStar3.Image = My.Resources.starempty
                        pbStar4.Image = My.Resources.starempty
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 1.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.starhalf
                        pbStar3.Image = My.Resources.starempty
                        pbStar4.Image = My.Resources.starempty
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 2
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.starempty
                        pbStar4.Image = My.Resources.starempty
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 2.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.starhalf
                        pbStar4.Image = My.Resources.starempty
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 3
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.starempty
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 3.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.starhalf
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 4
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.starempty
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 4.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.starhalf
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.starempty
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 5.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.starhalf
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 6
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.starempty
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 6.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.starhalf
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 7
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.star
                        pbStar8.Image = My.Resources.starempty
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 7.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.star
                        pbStar8.Image = My.Resources.starhalf
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 8
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.star
                        pbStar8.Image = My.Resources.star
                        pbStar9.Image = My.Resources.starempty
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 8.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.star
                        pbStar8.Image = My.Resources.star
                        pbStar9.Image = My.Resources.starhalf
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 9
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.star
                        pbStar8.Image = My.Resources.star
                        pbStar9.Image = My.Resources.star
                        pbStar10.Image = My.Resources.starempty
                    Case Is <= 9.5
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.star
                        pbStar8.Image = My.Resources.star
                        pbStar9.Image = My.Resources.star
                        pbStar10.Image = My.Resources.starhalf
                    Case Else
                        pbStar1.Image = My.Resources.star
                        pbStar2.Image = My.Resources.star
                        pbStar3.Image = My.Resources.star
                        pbStar4.Image = My.Resources.star
                        pbStar5.Image = My.Resources.star
                        pbStar6.Image = My.Resources.star
                        pbStar7.Image = My.Resources.star
                        pbStar8.Image = My.Resources.star
                        pbStar9.Image = My.Resources.star
                        pbStar10.Image = My.Resources.star
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub bwCleanDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCleanDB.DoWork
        Dim Args As Structures.ScanOrClean = DirectCast(e.Argument, Structures.ScanOrClean)
        Master.DB.Clean(Args.Movies, Args.MovieSets, Args.TV)
    End Sub

    Private Sub bwCleanDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCleanDB.RunWorkerCompleted
        SetStatus(String.Empty)
        tspbLoading.Visible = False

        FillList(True, True, True)
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try

            sHTTP.StartDownloadImage(Args.pURL)

            While sHTTP.IsDownloading
                Application.DoEvents()
                If bwDownloadPic.CancellationPending Then
                    e.Cancel = True
                    sHTTP.Cancel()
                    Return
                End If
                Threading.Thread.Sleep(50)
            End While

            e.Result = New Results With {.Result = sHTTP.Image}
        Catch ex As Exception
            e.Result = New Results With {.Result = Nothing}
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwDownloadPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadPic.RunWorkerCompleted
        '//
        ' Thread finished: display pic if it was able to get one
        '\\

        pbActLoad.Visible = False

        If e.Cancelled Then
            pbActors.Image = My.Resources.actor_silhouette
        Else
            Dim Res As Results = DirectCast(e.Result, Results)

            If Res.Result IsNot Nothing Then
                pbActors.Image = Res.Result
            Else
                pbActors.Image = My.Resources.actor_silhouette
            End If
        End If
    End Sub

    Private Sub bwLoadEpInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadEpInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        MainActors.Clear()
        MainBanner.Clear()
        MainCharacterArt.Clear()
        MainClearArt.Clear()
        MainClearLogo.Clear()
        MainDiscArt.Clear()
        MainFanart.Clear()
        MainFanartSmall.Clear()
        MainLandscape.Clear()
        MainPoster.Clear()

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        currTV = Master.DB.Load_TVEpisode(Args.ID, True)
        currTV.LoadAllImages(True, False)

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Master.eSettings.GeneralDisplayFanartSmall Then MainFanartSmall = currTV.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayPoster Then MainPoster = currTV.ImagesContainer.Poster.ImageOriginal

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Master.eSettings.GeneralDisplayFanart Then
            Dim NeedsGS As Boolean = False
            If currTV.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                MainFanart = currTV.ImagesContainer.Fanart.ImageOriginal
            Else
                Dim SeasonID As Long = Master.DB.GetTVSeasonIDFromEpisode(currTV)
                Dim TVSeasonFanart As String = Master.DB.GetArtForItem(SeasonID, "season", "fanart")
                If Not String.IsNullOrEmpty(TVSeasonFanart) Then
                    MainFanart.LoadFromFile(TVSeasonFanart, True)
                    NeedsGS = True
                Else
                    Dim TVShowFanart As String = Master.DB.GetArtForItem(currTV.ShowID, "tvshow", "fanart")
                    If Not String.IsNullOrEmpty(TVShowFanart) Then
                        MainFanart.LoadFromFile(TVShowFanart, True)
                        NeedsGS = True
                    End If
                End If
            End If

            If MainFanart.Image IsNot Nothing Then
                If String.IsNullOrEmpty(currTV.Filename) Then
                    MainFanart = ImageUtils.AddMissingStamp(MainFanart)
                ElseIf NeedsGS Then
                    MainFanart = ImageUtils.GrayScale(MainFanart)
                End If
            End If
        End If

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadEpInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadEpInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                FillScreenInfoWith_TVEpisode()
            Else
                SetControlsEnabled(True)
            End If

            dgvTVEpisodes.ResumeLayout()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub bwLoadMovieInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        MainActors.Clear()
        MainBanner.Clear()
        MainCharacterArt.Clear()
        MainClearArt.Clear()
        MainClearLogo.Clear()
        MainDiscArt.Clear()
        MainFanart.Clear()
        MainFanartSmall.Clear()
        MainLandscape.Clear()
        MainPoster.Clear()

        If bwLoadMovieInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        currMovie = Master.DB.Load_Movie(Args.ID)
        currMovie.LoadAllImages(True, False)

        If bwLoadMovieInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Master.eSettings.GeneralDisplayBanner Then MainBanner = currMovie.ImagesContainer.Banner.ImageOriginal
        If Master.eSettings.GeneralDisplayClearArt Then MainClearArt = currMovie.ImagesContainer.ClearArt.ImageOriginal
        If Master.eSettings.GeneralDisplayClearLogo Then MainClearLogo = currMovie.ImagesContainer.ClearLogo.ImageOriginal
        If Master.eSettings.GeneralDisplayDiscArt Then MainDiscArt = currMovie.ImagesContainer.DiscArt.ImageOriginal
        If Master.eSettings.GeneralDisplayFanart Then MainFanart = currMovie.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayFanartSmall Then MainFanartSmall = currMovie.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayLandscape Then MainLandscape = currMovie.ImagesContainer.Landscape.ImageOriginal
        If Master.eSettings.GeneralDisplayPoster Then MainPoster = currMovie.ImagesContainer.Poster.ImageOriginal

        If bwLoadMovieInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadMovieInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                FillScreenInfoWith_Movie()
            Else
                If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwRewrite_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                    SetControlsEnabled(True)
                    EnableFilters_Movies(True)
                Else
                    dgvMovies.Enabled = True
                End If
            End If
            dgvMovies.ResumeLayout()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub bwLoadMovieSetInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieSetInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        MainActors.Clear()
        MainBanner.Clear()
        MainCharacterArt.Clear()
        MainClearArt.Clear()
        MainClearLogo.Clear()
        MainDiscArt.Clear()
        MainFanart.Clear()
        MainFanartSmall.Clear()
        MainLandscape.Clear()
        MainPoster.Clear()

        If bwLoadMovieSetInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        currMovieSet = Master.DB.Load_MovieSet(Args.ID)
        currMovieSet.LoadAllImages(True, False)

        If bwLoadMovieSetInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Master.eSettings.GeneralDisplayBanner Then MainBanner = currMovieSet.ImagesContainer.Banner.ImageOriginal
        If Master.eSettings.GeneralDisplayClearArt Then MainClearArt = currMovieSet.ImagesContainer.ClearArt.ImageOriginal
        If Master.eSettings.GeneralDisplayClearLogo Then MainClearLogo = currMovieSet.ImagesContainer.ClearLogo.ImageOriginal
        If Master.eSettings.GeneralDisplayDiscArt Then MainDiscArt = currMovieSet.ImagesContainer.DiscArt.ImageOriginal
        If Master.eSettings.GeneralDisplayFanart Then MainFanart = currMovieSet.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayFanartSmall Then MainFanartSmall = currMovieSet.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayLandscape Then MainLandscape = currMovieSet.ImagesContainer.Landscape.ImageOriginal
        If Master.eSettings.GeneralDisplayPoster Then MainPoster = currMovieSet.ImagesContainer.Poster.ImageOriginal
        'read nfo if it's there

        If bwLoadMovieSetInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadMovieSetInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieSetInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                FillScreenInfoWith_MovieSet()
            Else
                If Not bwMovieSetScraper.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                    SetControlsEnabled(True)
                    EnableFilters_MovieSets(True)
                Else
                    dgvMovieSets.Enabled = True
                End If
            End If
            dgvMovieSets.ResumeLayout()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub bwLoadMovieSetPosters_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieSetPosters.DoWork
        Dim Posters As New List(Of MovieInSetPoster)

        Try
            If currMovieSet.MoviesInSet IsNot Nothing AndAlso currMovieSet.MoviesInSet.Count > 0 Then
                Try
                    For Each tMovieInSet As MediaContainers.MovieInSet In currMovieSet.MoviesInSet
                        If bwLoadMovieSetPosters.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If

                        Dim ResImg As Image
                        If tMovieInSet.DBMovie.ImagesContainer.Poster.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                            ResImg = tMovieInSet.DBMovie.ImagesContainer.Poster.ImageOriginal.Image
                            ImageUtils.ResizeImage(ResImg, 59, 88, True, Color.White.ToArgb())
                            Posters.Add(New MovieInSetPoster With {.MoviePoster = ResImg, .MovieTitle = tMovieInSet.DBMovie.Movie.Title, .MovieYear = tMovieInSet.DBMovie.Movie.Year})
                        Else
                            Posters.Add(New MovieInSetPoster With {.MoviePoster = My.Resources.noposter, .MovieTitle = tMovieInSet.DBMovie.Movie.Title, .MovieYear = tMovieInSet.DBMovie.Movie.Year})
                        End If
                    Next
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    e.Result = New Results With {.MovieInSetPosters = Nothing}
                    e.Cancel = True
                End Try
            End If

            e.Result = New Results With {.MovieInSetPosters = Posters}
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            e.Result = New Results With {.MovieInSetPosters = Nothing}
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadMovieSetPosters_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieSetPosters.RunWorkerCompleted
        lvMoviesInSet.Clear()
        ilMoviesInSet.Images.Clear()
        ilMoviesInSet.ImageSize = New Size(59, 88)
        ilMoviesInSet.ColorDepth = ColorDepth.Depth32Bit
        lvMoviesInSet.Visible = False

        If Not e.Cancelled Then
            Try
                Dim Res As Results = DirectCast(e.Result, Results)

                If Res.MovieInSetPosters IsNot Nothing AndAlso Res.MovieInSetPosters.Count > 0 Then
                    lvMoviesInSet.BeginUpdate()
                    For Each tPoster As MovieInSetPoster In Res.MovieInSetPosters
                        If tPoster IsNot Nothing Then
                            ilMoviesInSet.Images.Add(tPoster.MoviePoster)
                            lvMoviesInSet.Items.Add(String.Concat(tPoster.MovieTitle, Environment.NewLine, "(", tPoster.MovieYear, ")"), ilMoviesInSet.Images.Count - 1)
                        End If
                    Next
                    lvMoviesInSet.EndUpdate()
                    lvMoviesInSet.Visible = True
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub

    Private Sub bwLoadSeasonInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadSeasonInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        MainActors.Clear()
        MainBanner.Clear()
        MainCharacterArt.Clear()
        MainClearArt.Clear()
        MainClearLogo.Clear()
        MainDiscArt.Clear()
        MainFanart.Clear()
        MainFanartSmall.Clear()
        MainLandscape.Clear()
        MainPoster.Clear()

        currTV = Master.DB.Load_TVSeason(Args.ID, True, False)
        currTV.LoadAllImages(True, False)

        If bwLoadSeasonInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Master.eSettings.GeneralDisplayBanner Then MainBanner = currTV.ImagesContainer.Banner.ImageOriginal
        If Master.eSettings.GeneralDisplayFanartSmall Then MainFanartSmall = currTV.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayLandscape Then MainLandscape = currTV.ImagesContainer.Landscape.ImageOriginal
        If Master.eSettings.GeneralDisplayPoster Then MainPoster = currTV.ImagesContainer.Poster.ImageOriginal

        If bwLoadSeasonInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Master.eSettings.GeneralDisplayFanart Then
            Dim NeedsGS As Boolean = False
            If currTV.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                MainFanart = currTV.ImagesContainer.Fanart.ImageOriginal
            Else
                Dim TVShowFanart As String = Master.DB.GetArtForItem(currTV.ShowID, "tvshow", "fanart")
                If Not String.IsNullOrEmpty(TVShowFanart) Then
                    MainFanart.LoadFromFile(TVShowFanart, True)
                    NeedsGS = True
                End If
            End If

            If MainFanart.Image IsNot Nothing AndAlso NeedsGS Then
                MainFanart = ImageUtils.GrayScale(MainFanart)
            End If
        End If

        If bwLoadSeasonInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadSeasonInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadSeasonInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                FillScreenInfoWith_TVSeason()
            Else
                SetControlsEnabled(True)
            End If
            dgvTVSeasons.ResumeLayout()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub bwLoadShowInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadShowInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        MainActors.Clear()
        MainBanner.Clear()
        MainCharacterArt.Clear()
        MainClearArt.Clear()
        MainClearLogo.Clear()
        MainDiscArt.Clear()
        MainFanart.Clear()
        MainFanartSmall.Clear()
        MainLandscape.Clear()
        MainPoster.Clear()

        If bwLoadShowInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        currTV = Master.DB.Load_TVShow(Args.ID, False, False)
        currTV.LoadAllImages(True, False)

        If bwLoadShowInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Master.eSettings.GeneralDisplayBanner Then MainBanner = currTV.ImagesContainer.Banner.ImageOriginal
        If Master.eSettings.GeneralDisplayCharacterArt Then MainCharacterArt = currTV.ImagesContainer.CharacterArt.ImageOriginal
        If Master.eSettings.GeneralDisplayClearArt Then MainClearArt = currTV.ImagesContainer.ClearArt.ImageOriginal
        If Master.eSettings.GeneralDisplayClearLogo Then MainClearLogo = currTV.ImagesContainer.ClearLogo.ImageOriginal
        If Master.eSettings.GeneralDisplayFanart Then MainFanart = currTV.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayFanartSmall Then MainFanartSmall = currTV.ImagesContainer.Fanart.ImageOriginal
        If Master.eSettings.GeneralDisplayLandscape Then MainLandscape = currTV.ImagesContainer.Landscape.ImageOriginal
        If Master.eSettings.GeneralDisplayPoster Then MainPoster = currTV.ImagesContainer.Poster.ImageOriginal

        If bwLoadShowInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadShowInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadShowInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                FillScreenInfoWith_TVShow()
            Else
                SetControlsEnabled(True)
                EnableFilters_Shows(True)
            End If
            dgvTVShows.ResumeLayout()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub bwMovieScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMovieScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            InfoDownloaded_Movie(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped Movie from disk to get clean informations in DB
            Reload_Movie(Res.DBElement.ID, False, True)
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        Else
            FillList(False, True, False)
            If dgvMovies.SelectedRows.Count > 0 Then
                SelectRow_Movie(dgvMovies.SelectedRows(0).Index)
            Else
                ClearInfo()
            End If
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwMovieScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeMovie As New Database.DBElement(Enums.ContentType.Movie)

        logger.Trace(String.Format("[Movie Scraper] [Start] Movies Count [{0}]", Args.ScrapeList.Count.ToString))

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim Theme As New MediaContainers.Theme
            Dim tURL As String = String.Empty
            Dim aUrlList As New List(Of MediaContainers.Trailer)
            Dim tUrlList As New List(Of Themes)
            Dim OldListTitle As String = String.Empty
            Dim NewListTitle As String = String.Empty

            Cancelled = False

            If bwMovieScraper.CancellationPending Then Exit For
            OldListTitle = tScrapeItem.DataRow.Item("ListTitle").ToString
            bwMovieScraper.ReportProgress(1, OldListTitle)

            Dim dScrapeRow As DataRow = tScrapeItem.DataRow

            logger.Trace(String.Format("[Movie Scraper] [Start] Scraping {0}", OldListTitle))

            DBScrapeMovie = Master.DB.Load_Movie(Convert.ToInt64(tScrapeItem.DataRow.Item("idMovie")))

            If tScrapeItem.ScrapeModifiers.MainNFO Then
                If ModulesManager.Instance.ScrapeData_Movie(DBScrapeMovie, tScrapeItem.ScrapeModifiers, Args.ScrapeType, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
                    logger.Trace(String.Format("[Movie Scraper] [Cancelled] Scraping {0}", OldListTitle))
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        bwMovieScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the movie ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeMovie.Movie.ID) AndAlso (tScrapeItem.ScrapeModifiers.MainActorthumbs Or tScrapeItem.ScrapeModifiers.MainBanner Or tScrapeItem.ScrapeModifiers.MainClearArt Or
                                                                         tScrapeItem.ScrapeModifiers.MainClearLogo Or tScrapeItem.ScrapeModifiers.MainDiscArt Or tScrapeItem.ScrapeModifiers.MainExtrafanarts Or
                                                                         tScrapeItem.ScrapeModifiers.MainExtrathumbs Or tScrapeItem.ScrapeModifiers.MainFanart Or tScrapeItem.ScrapeModifiers.MainLandscape Or
                                                                         tScrapeItem.ScrapeModifiers.MainPoster Or tScrapeItem.ScrapeModifiers.MainTheme Or tScrapeItem.ScrapeModifiers.MainTrailer) Then
                    Dim tModifiers As New Structures.ScrapeModifiers With {.MainNFO = True}
                    Dim tOptions As New Structures.ScrapeOptions 'set all values to false to not override any field. ID's are always determined.
                    If ModulesManager.Instance.ScrapeData_Movie(DBScrapeMovie, tModifiers, Args.ScrapeType, tOptions, Args.ScrapeList.Count = 1) Then
                        logger.Trace(String.Format("[Movie Scraper] [Cancelled] Scraping {0}", OldListTitle))
                        Cancelled = True
                        If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            bwMovieScraper.CancelAsync()
                        End If
                    End If
                End If
            End If

            If bwMovieScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                If Master.eSettings.MovieScraperMetaDataScan AndAlso tScrapeItem.ScrapeModifiers.MainMeta Then
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(140, "Scanning Meta Data"), ":"))
                    MediaInfo.UpdateMediaInfo(DBScrapeMovie)
                End If
                If bwMovieScraper.CancellationPending Then Exit For

                NewListTitle = DBScrapeMovie.ListTitle

                If Not NewListTitle = OldListTitle Then
                    bwMovieScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
                End If

                'get all images 
                If tScrapeItem.ScrapeModifiers.MainBanner OrElse
                    tScrapeItem.ScrapeModifiers.MainClearArt OrElse
                    tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
                    tScrapeItem.ScrapeModifiers.MainDiscArt OrElse
                    tScrapeItem.ScrapeModifiers.MainExtrafanarts OrElse
                    tScrapeItem.ScrapeModifiers.MainExtrathumbs OrElse
                    tScrapeItem.ScrapeModifiers.MainFanart OrElse
                    tScrapeItem.ScrapeModifiers.MainLandscape OrElse
                    tScrapeItem.ScrapeModifiers.MainPoster Then

                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
                    If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.MovieImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeMovie, SearchResultsContainer, tScrapeItem.ScrapeModifiers) = DialogResult.OK Then
                                    Images.SetPreferredImages(DBScrapeMovie, dImgSelect.Result)
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Images.SetPreferredImages(DBScrapeMovie, SearchResultsContainer, tScrapeItem.ScrapeModifiers, IsAutoScraper:=True)
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Theme
                If tScrapeItem.ScrapeModifiers.MainTheme Then
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(266, "Scraping Themes"), ":"))
                    If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                        tURL = String.Empty
                        If Theme.WebTheme.IsAllowedToDownload(DBScrapeMovie) Then
                            If Not ModulesManager.Instance.ScrapeTheme_Movie(DBScrapeMovie, tUrlList) Then
                                If tUrlList.Count > 0 Then
                                    If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                                        Theme.WebTheme.FromWeb(tUrlList.Item(0).URL, tUrlList.Item(0).WebURL)
                                        If Theme.WebTheme IsNot Nothing Then 'TODO: fix check
                                            tURL = Theme.WebTheme.SaveAsMovieTheme(DBScrapeMovie)
                                            If Not String.IsNullOrEmpty(tURL) Then
                                                DBScrapeMovie.ThemePath = tURL
                                            End If
                                        End If
                                        'ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk  OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        '    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk  OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        '        MsgBox(Master.eLang.GetString(930, "Trailer of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size:"))
                                        '    End If
                                        '    Using dThemeSelect As New dlgThemeSelect()
                                        '        tURL = dThemeSelect.ShowDialog(DBScrapeMovie, tUrlList)
                                        '        If Not String.IsNullOrEmpty(tURL) Then
                                        '            DBScrapeMovie.ThemePath = tURL
                                        '            MovieScraperEvent(Enums.MovieScraperEventType.ThemeItem, DBScrapeMovie.ThemePath )
                                        '        End If
                                        '    End Using
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Trailer
                If tScrapeItem.ScrapeModifiers.MainTrailer Then
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(574, "Scraping Trailers"), ":"))
                    Dim SearchResults As New List(Of MediaContainers.Trailer)
                    If Not ModulesManager.Instance.ScrapeTrailer_Movie(DBScrapeMovie, Enums.ModifierType.MainTrailer, SearchResults) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Using dTrailerSelect As New dlgTrailerSelect
                                'note msavazzi why is always False with Player? If dTrailerSelect.ShowDialog(DBScrapeMovie, SearchResults, False, True, False) = DialogResult.OK Then
                                'DanCooper: the VLC COM interface is/was not able to call in multithread
                                If dTrailerSelect.ShowDialog(DBScrapeMovie, SearchResults, False, True, clsAdvancedSettings.GetBooleanSetting("UseAsVideoPlayer", False, "generic.EmberCore.VLCPlayer")) = DialogResult.OK Then
                                    DBScrapeMovie.Trailer = dTrailerSelect.Result
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Dim newPreferredTrailer As New MediaContainers.Trailer
                            If Trailers.GetPreferredMovieTrailer(SearchResults, newPreferredTrailer) Then
                                DBScrapeMovie.Trailer = newPreferredTrailer
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_Movie, Nothing, Nothing, False, DBScrapeMovie)
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.Save_Movie(DBScrapeMovie, False, tScrapeItem.ScrapeModifiers.MainNFO OrElse tScrapeItem.ScrapeModifiers.MainMeta, True, False)
                    bwMovieScraper.ReportProgress(-2, DBScrapeMovie.ID)
                    bwMovieScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
                End If
                logger.Trace(String.Format("[Movie Scraper] [Done] Scraping {0}", OldListTitle))
            Else
                logger.Trace(String.Format("[Movie Scraper] [Cancelled] Scraping {0}", OldListTitle))
            End If
        Next

        e.Result = New Results With {.DBElement = DBScrapeMovie, .ScrapeType = Args.ScrapeType, .Cancelled = bwMovieScraper.CancellationPending}
        logger.Trace(String.Format("[Movie Scraper] [Done] Scraping"))
    End Sub

    Private Sub bwMovieScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMovieScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"moviescraped", 3, Master.eLang.GetString(813, "Movie Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_Movie(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            tslLoading.Text = e.UserState.ToString
        Else
            tspbLoading.Value += e.ProgressPercentage
            SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwMovieSetScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMovieSetScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            InfoDownloaded_MovieSet(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped MovieSet from disk to get clean informations in DB
            Reload_MovieSet(Res.DBElement.ID)
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        Else
            If dgvMovieSets.SelectedRows.Count > 0 Then
                SelectRow_MovieSet(dgvMovieSets.SelectedRows(0).Index)
            Else
                ClearInfo()
            End If
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwMovieSetScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieSetScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeMovieSet As New Database.DBElement(Enums.ContentType.MovieSet)

        logger.Trace(String.Format("[MovieSet Scraper] [Start] MovieSets Count [{0}]", Args.ScrapeList.Count.ToString))

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim aContainer As New MediaContainers.SearchResultsContainer
            Dim NewListTitle As String = String.Empty
            Dim NewTMDBColID As String = String.Empty
            Dim NewTitle As String = String.Empty
            Dim OldListTitle As String = String.Empty
            Dim OldTMDBColID As String = String.Empty
            Dim OldTitle As String = String.Empty
            Dim efList As New List(Of String)
            Dim etList As New List(Of String)
            Dim tURL As String = String.Empty

            Cancelled = False

            If bwMovieSetScraper.CancellationPending Then Exit For
            OldListTitle = tScrapeItem.DataRow.Item("ListTitle").ToString
            OldTitle = tScrapeItem.DataRow.Item("SetName").ToString
            OldTMDBColID = tScrapeItem.DataRow.Item("TMDBColID").ToString
            bwMovieSetScraper.ReportProgress(1, OldListTitle)

            Dim dScrapeRow As DataRow = tScrapeItem.DataRow

            logger.Trace(String.Format("[MovieSet Scraper] [Start] Scraping {0}", OldListTitle))

            DBScrapeMovieSet = Master.DB.Load_MovieSet(Convert.ToInt64(tScrapeItem.DataRow.Item("idSet")))

            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEditMovieSet, Nothing, DBScrapeMovieSet)

            If tScrapeItem.ScrapeModifiers.MainNFO Then
                bwMovieSetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, tScrapeItem.ScrapeModifiers, Args.ScrapeType, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
                    logger.Trace(String.Format("[MovieSet Scraper] [Cancelled] Scraping {0}", OldListTitle))
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        bwMovieSetScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the movie set ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeMovieSet.MovieSet.TMDB) AndAlso (tScrapeItem.ScrapeModifiers.MainBanner Or tScrapeItem.ScrapeModifiers.MainClearArt Or
                                                                         tScrapeItem.ScrapeModifiers.MainClearLogo Or tScrapeItem.ScrapeModifiers.MainDiscArt Or
                                                                         tScrapeItem.ScrapeModifiers.MainFanart Or tScrapeItem.ScrapeModifiers.MainLandscape Or
                                                                         tScrapeItem.ScrapeModifiers.MainPoster) Then
                    Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, tScrapeItem.ScrapeModifiers, Args.ScrapeType, tOpt, Args.ScrapeList.Count = 1) Then
                        logger.Trace(String.Format("[MovieSet Scraper] [Cancelled] Scraping {0}", OldListTitle))
                        Exit For
                    End If
                End If
            End If

            If bwMovieSetScraper.CancellationPending Then Exit For

            If Not Cancelled Then

                NewListTitle = DBScrapeMovieSet.ListTitle
                NewTitle = DBScrapeMovieSet.MovieSet.Title
                NewTMDBColID = DBScrapeMovieSet.MovieSet.TMDB

                If Not NewListTitle = OldListTitle Then
                    bwMovieSetScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
                End If

                'get all images
                If tScrapeItem.ScrapeModifiers.MainBanner OrElse
                    tScrapeItem.ScrapeModifiers.MainClearArt OrElse
                    tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
                    tScrapeItem.ScrapeModifiers.MainDiscArt OrElse
                    tScrapeItem.ScrapeModifiers.MainExtrafanarts OrElse
                    tScrapeItem.ScrapeModifiers.MainFanart OrElse
                    tScrapeItem.ScrapeModifiers.MainLandscape OrElse
                    tScrapeItem.ScrapeModifiers.MainPoster Then

                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwMovieSetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
                    If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, SearchResultsContainer, tScrapeItem.ScrapeModifiers) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.MovieImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeMovieSet, SearchResultsContainer, tScrapeItem.ScrapeModifiers) = DialogResult.OK Then
                                    Images.SetPreferredImages(DBScrapeMovieSet, dImgSelect.Result)
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Images.SetPreferredImages(DBScrapeMovieSet, SearchResultsContainer, tScrapeItem.ScrapeModifiers, IsAutoScraper:=True)
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    bwMovieSetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.Save_MovieSet(DBScrapeMovieSet, True, True)
                    bwMovieSetScraper.ReportProgress(-2, DBScrapeMovieSet.ID)
                    bwMovieSetScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
                End If
                logger.Trace(String.Format("[MovieSet Scraper] [Done] Scraping {0}", OldListTitle))
            Else
                logger.Trace(String.Format("[MovieSet Scraper] [Cancelled] Scraping {0}", OldListTitle))
            End If
        Next

        e.Result = New Results With {.DBElement = DBScrapeMovieSet, .ScrapeType = Args.ScrapeType, .Cancelled = bwMovieSetScraper.CancellationPending}
        logger.Trace(String.Format("[MovieSet Scraper] [Done] Scraping"))
    End Sub

    Private Sub bwMovieSetScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMovieSetScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"moviesetscraped", 3, Master.eLang.GetString(1204, "MovieSet Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_MovieSet(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            tslLoading.Text = e.UserState.ToString
        Else
            tspbLoading.Value += e.ProgressPercentage
            SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwTVScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            InfoDownloaded_TV(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped TVShow from disk to get clean informations in DB
            Reload_TVShow(Res.DBElement.ID, False, True, True)
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        Else
            'Me.FillList(False, False, False)
            If dgvTVShows.SelectedRows.Count > 0 Then
                SelectRow_TVShow(dgvTVShows.SelectedRows(0).Index)
            Else
                ClearInfo()
            End If
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwTVScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeShow As New Database.DBElement(Enums.ContentType.TVShow)

        logger.Trace(String.Format("[TVScraper] [Start] TV Shows Count [{0}]", Args.ScrapeList.Count.ToString))

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim ShowTheme As New MediaContainers.Theme
            Dim tURL As String = String.Empty
            Dim tUrlList As New List(Of Themes)
            Dim OldListTitle As String = String.Empty
            Dim NewListTitle As String = String.Empty

            Cancelled = False

            If bwTVScraper.CancellationPending Then Exit For
            OldListTitle = tScrapeItem.DataRow.Item("ListTitle").ToString
            bwTVScraper.ReportProgress(1, OldListTitle)

            Dim dScrapeRow As DataRow = tScrapeItem.DataRow

            logger.Trace(String.Format("[TVScraper] [Start] Scraping {0}", OldListTitle))

            DBScrapeShow = Master.DB.Load_TVShow_Full(Convert.ToInt64(tScrapeItem.DataRow.Item("idShow")))
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

            If tScrapeItem.ScrapeModifiers.MainNFO Then
                bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeShow, tScrapeItem.ScrapeModifiers, Args.ScrapeType, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Concat("Canceled scraping: ", OldListTitle))
                        bwTVScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the tvshow ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeShow.TVShow.TVDB) AndAlso (tScrapeItem.ScrapeModifiers.MainActorthumbs Or tScrapeItem.ScrapeModifiers.MainBanner Or tScrapeItem.ScrapeModifiers.MainCharacterArt Or
                                                                           tScrapeItem.ScrapeModifiers.MainClearArt Or tScrapeItem.ScrapeModifiers.MainClearLogo Or tScrapeItem.ScrapeModifiers.MainExtrafanarts Or
                                                                           tScrapeItem.ScrapeModifiers.MainFanart Or tScrapeItem.ScrapeModifiers.MainLandscape Or tScrapeItem.ScrapeModifiers.MainPoster Or
                                                                           tScrapeItem.ScrapeModifiers.MainTheme) Then
                    Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeShow, tScrapeItem.ScrapeModifiers, Args.ScrapeType, tOpt, Args.ScrapeList.Count = 1) Then
                        Exit For
                    End If
                End If
            End If

            If bwTVScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                NewListTitle = DBScrapeShow.ListTitle

                If Not NewListTitle = OldListTitle Then
                    bwTVScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
                End If

                'get all images
                Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
                If Not ModulesManager.Instance.ScrapeImage_TV(DBScrapeShow, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
                    If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
                        Using dImgSelect As New dlgImgSelect
                            If dImgSelect.ShowDialog(DBScrapeShow, SearchResultsContainer, tScrapeItem.ScrapeModifiers) = DialogResult.OK Then
                                Images.SetPreferredImages(DBScrapeShow, dImgSelect.Result)
                            End If
                        End Using

                        'autoscraping
                    ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        Images.SetPreferredImages(DBScrapeShow, SearchResultsContainer, tScrapeItem.ScrapeModifiers, IsAutoScraper:=True)
                    End If
                End If

                If bwTVScraper.CancellationPending Then Exit For

                'Theme
                If tScrapeItem.ScrapeModifiers.MainTheme Then
                    bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(266, "Scraping Themes"), ":"))
                End If

                If bwTVScraper.CancellationPending Then Exit For

                'Episode Meta Data
                If tScrapeItem.ScrapeModifiers.withEpisodes AndAlso tScrapeItem.ScrapeModifiers.EpisodeMeta AndAlso Master.eSettings.TVScraperMetaDataScan Then
                    bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(140, "Scanning Meta Data"), ":"))
                    For Each tEpisode In DBScrapeShow.Episodes.Where(Function(f) f.FilenameSpecified)
                        MediaInfo.UpdateTVMediaInfo(tEpisode)
                    Next
                End If

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVShow, Nothing, Nothing, False, DBScrapeShow)
                    bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.Save_TVShow(DBScrapeShow, False, tScrapeItem.ScrapeModifiers.MainNFO OrElse tScrapeItem.ScrapeModifiers.MainMeta, True, tScrapeItem.ScrapeModifiers.withEpisodes)
                    bwTVScraper.ReportProgress(-2, DBScrapeShow.ID)
                    bwTVScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
                End If
            End If

            logger.Trace(String.Concat("Ended scraping: ", OldListTitle))
        Next

        e.Result = New Results With {.DBElement = DBScrapeShow, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVScraper.CancellationPending}
        logger.Trace("Ended TV SHOW scrape")
    End Sub

    Private Sub bwTVScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"tvshowscraped", 3, Master.eLang.GetString(248, "Show Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_TVShow(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            tslLoading.Text = e.UserState.ToString
        Else
            tspbLoading.Value += e.ProgressPercentage
            SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwTVEpisodeScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVEpisodeScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            InfoDownloaded_TVEpisode(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped Episode from disk to get clean informations in DB
            Reload_TVEpisode(Res.DBElement.ID, False, True)
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        Else
            'Me.FillList(False, False, False)
            If dgvTVEpisodes.SelectedRows.Count > 0 Then
                SelectRow_TVEpisode(dgvTVShows.SelectedRows(0).Index)
            Else
                ClearInfo()
            End If
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwTVEpisodeScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVEpisodeScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeEpisode As New Database.DBElement(Enums.ContentType.TVEpisode)

        logger.Trace(String.Format("[TVEpisode] [Start] Episodes Count [{0}]", Args.ScrapeList.Count.ToString))

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim OldEpisodeTitle As String = String.Empty
            Dim NewEpisodeTitle As String = String.Empty

            Cancelled = False

            If bwTVEpisodeScraper.CancellationPending Then Exit For
            OldEpisodeTitle = tScrapeItem.DataRow.Item("Title").ToString
            bwTVEpisodeScraper.ReportProgress(1, OldEpisodeTitle)

            Dim dScrapeRow As DataRow = tScrapeItem.DataRow

            logger.Trace(String.Format("[TVEpisodeScraper] [Start] Scraping {0}", OldEpisodeTitle))

            DBScrapeEpisode = Master.DB.Load_TVEpisode(Convert.ToInt64(tScrapeItem.DataRow.Item("idEpisode")), True)
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

            If tScrapeItem.ScrapeModifiers.EpisodeNFO Then
                bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_TVEpisode(DBScrapeEpisode, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Concat("Canceled scraping: ", OldEpisodeTitle))
                        bwTVEpisodeScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the episode ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeEpisode.TVEpisode.TVDB) AndAlso (tScrapeItem.ScrapeModifiers.MainActorthumbs Or tScrapeItem.ScrapeModifiers.MainBanner Or tScrapeItem.ScrapeModifiers.MainCharacterArt Or
                                                                         tScrapeItem.ScrapeModifiers.MainClearArt Or tScrapeItem.ScrapeModifiers.MainClearLogo Or tScrapeItem.ScrapeModifiers.MainExtrafanarts Or
                                                                         tScrapeItem.ScrapeModifiers.MainFanart Or tScrapeItem.ScrapeModifiers.MainLandscape Or tScrapeItem.ScrapeModifiers.MainPoster Or
                                                                         tScrapeItem.ScrapeModifiers.MainTheme) Then
                    Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_TVEpisode(DBScrapeEpisode, tOpt, Args.ScrapeList.Count = 1) Then
                        Exit For
                    End If
                End If
            End If

            If bwTVEpisodeScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                If Master.eSettings.TVScraperMetaDataScan AndAlso tScrapeItem.ScrapeModifiers.EpisodeMeta Then
                    MediaInfo.UpdateTVMediaInfo(DBScrapeEpisode)
                End If
                If bwTVEpisodeScraper.CancellationPending Then Exit For

                NewEpisodeTitle = DBScrapeEpisode.TVEpisode.Title

                If Not NewEpisodeTitle = OldEpisodeTitle Then
                    bwTVEpisodeScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldEpisodeTitle, NewEpisodeTitle))
                End If

                'get all images
                If tScrapeItem.ScrapeModifiers.EpisodeFanart OrElse
                    tScrapeItem.ScrapeModifiers.EpisodePoster Then
                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(265, "Scraping Episode Images"), ":"))
                    If Not ModulesManager.Instance.ScrapeImage_TV(DBScrapeEpisode, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeEpisode, SearchResultsContainer, tScrapeItem.ScrapeModifiers) = DialogResult.OK Then
                                    Images.SetPreferredImages(DBScrapeEpisode, dImgSelect.Result)
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Images.SetPreferredImages(DBScrapeEpisode, SearchResultsContainer, tScrapeItem.ScrapeModifiers, IsAutoScraper:=True)
                        End If
                    End If
                End If

                If bwTVEpisodeScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVEpisode, Nothing, Nothing, False, DBScrapeEpisode)
                    bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.Save_TVEpisode(DBScrapeEpisode, False, tScrapeItem.ScrapeModifiers.EpisodeNFO OrElse tScrapeItem.ScrapeModifiers.EpisodeMeta, True, True, True)
                    bwTVEpisodeScraper.ReportProgress(-2, DBScrapeEpisode.ID)
                    bwTVEpisodeScraper.ReportProgress(-1, If(Not OldEpisodeTitle = NewEpisodeTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldEpisodeTitle, NewEpisodeTitle), NewEpisodeTitle))
                End If
            End If

            logger.Trace(String.Concat("Ended scraping: ", OldEpisodeTitle))
        Next

        e.Result = New Results With {.DBElement = DBScrapeEpisode, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVEpisodeScraper.CancellationPending}
        logger.Trace("Ended EPISODE scrape")
    End Sub

    Private Sub bwTVEpisodeScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVEpisodeScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"tvepisodescraped", 3, Master.eLang.GetString(883, "Episode Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_TVEpisode(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            tslLoading.Text = e.UserState.ToString
        Else
            tspbLoading.Value += e.ProgressPercentage
            SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwTVSeasonScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVSeasonScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            InfoDownloaded_TVSeason(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped TVSeason from disk to get clean informations in DB
            Reload_TVSeason(Res.DBElement.ID, False, True, False)
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        Else
            'Me.FillList(False, False, False)
            If dgvTVSeasons.SelectedRows.Count > 0 Then
                SelectRow_TVSeason(dgvTVSeasons.SelectedRows(0).Index)
            Else
                ClearInfo()
            End If
            tslLoading.Visible = False
            tspbLoading.Visible = False
            btnCancel.Visible = False
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = False
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwTVSeasonScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVSeasonScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeSeason As New Database.DBElement(Enums.ContentType.TVSeason)

        logger.Trace(String.Format("[TVSeason Scraper] [Start] Seasons Count [{0}]", Args.ScrapeList.Count.ToString))

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim tURL As String = String.Empty
            Dim tUrlList As New List(Of Themes)

            Cancelled = False

            If bwTVSeasonScraper.CancellationPending Then Exit For

            Dim dScrapeRow As DataRow = tScrapeItem.DataRow

            DBScrapeSeason = Master.DB.Load_TVSeason(Convert.ToInt64(tScrapeItem.DataRow.Item("idSeason")), True, False)
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

            logger.Trace(String.Format("Start scraping: {0}: Season {1}", DBScrapeSeason.TVShow.Title, DBScrapeSeason.TVSeason.Season))

            If tScrapeItem.ScrapeModifiers.SeasonNFO Then
                bwTVSeasonScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_TVSeason(DBScrapeSeason, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Format("Canceled scraping: {0}: Season {1}", DBScrapeSeason.TVShow.Title, DBScrapeSeason.TVSeason.Season))
                        bwTVSeasonScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the tvshow ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeSeason.TVSeason.TVDB) AndAlso (tScrapeItem.ScrapeModifiers.SeasonBanner Or tScrapeItem.ScrapeModifiers.SeasonFanart Or
                                                                               tScrapeItem.ScrapeModifiers.SeasonLandscape Or tScrapeItem.ScrapeModifiers.SeasonPoster) Then
                    Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_TVSeason(DBScrapeSeason, tOpt, Args.ScrapeList.Count = 1) Then
                        Exit For
                    End If
                End If
            End If

            If bwTVSeasonScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                'get all images
                If tScrapeItem.ScrapeModifiers.AllSeasonsBanner OrElse
                    tScrapeItem.ScrapeModifiers.AllSeasonsFanart OrElse
                    tScrapeItem.ScrapeModifiers.AllSeasonsLandscape OrElse
                    tScrapeItem.ScrapeModifiers.AllSeasonsPoster OrElse
                    tScrapeItem.ScrapeModifiers.SeasonBanner OrElse
                    tScrapeItem.ScrapeModifiers.SeasonFanart OrElse
                    tScrapeItem.ScrapeModifiers.SeasonLandscape OrElse
                    tScrapeItem.ScrapeModifiers.SeasonPoster Then

                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwTVSeasonScraper.ReportProgress(-3, "Scraping Season Images:")
                    If Not ModulesManager.Instance.ScrapeImage_TV(DBScrapeSeason, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeSeason, SearchResultsContainer, tScrapeItem.ScrapeModifiers) = DialogResult.OK Then
                                    Images.SetPreferredImages(DBScrapeSeason, dImgSelect.Result)
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Images.SetPreferredImages(DBScrapeSeason, SearchResultsContainer, tScrapeItem.ScrapeModifiers, IsAutoScraper:=True)
                        End If
                    End If
                End If

                If bwTVSeasonScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVSeason, Nothing, Nothing, False, DBScrapeSeason)
                    bwTVSeasonScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.Save_TVSeason(DBScrapeSeason, False, True, True)
                    bwTVSeasonScraper.ReportProgress(-2, DBScrapeSeason.ID)
                End If
            End If

            logger.Trace(String.Format("Ended scraping: {0}: Season {1}", DBScrapeSeason.TVShow.Title, DBScrapeSeason.TVSeason.Season))
        Next

        e.Result = New Results With {.DBElement = DBScrapeSeason, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVSeasonScraper.CancellationPending}
        logger.Trace("Ended TV Season scrape")
    End Sub

    Private Sub bwTVSeasonScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVSeasonScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"tvseasonscraped", 3, Master.eLang.GetString(247, "Season Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_TVSeason(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            tslLoading.Text = e.UserState.ToString
        Else
            tspbLoading.Value += e.ProgressPercentage
            SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwReload_Movies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_Movies.DoWork
        Dim iCount As Integer = 0
        Dim MovieIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In dtMovies.Rows
            MovieIDs.Add(Convert.ToInt64(sRow.Item("idMovie")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieIDs
                If bwReload_Movies.CancellationPending Then Return
                bwReload_Movies.ReportProgress(iCount, KVP.Value)
                If Reload_Movie(KVP.Key, True, False) Then
                    doFill = True
                Else
                    bwReload_Movies.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_Movies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_Movies.ProgressChanged
        If e.ProgressPercentage = -1 Then
            RefreshRow_Movie(CLng(e.UserState))
        Else
            SetStatus(e.UserState.ToString)
            tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_Movies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_Movies.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        tslLoading.Text = String.Empty
        tspbLoading.Visible = False
        tslLoading.Visible = False

        If Res.doFill Then
            FillList(True, True, False)
        Else
            DoTitleCheck()
            SetControlsEnabled(True)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub bwReload_MovieSets_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_MovieSets.DoWork
        Dim iCount As Integer = 0
        Dim MovieSetIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In dtMovieSets.Rows
            MovieSetIDs.Add(Convert.ToInt64(sRow.Item("idSet")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieSetIDs
                If bwReload_MovieSets.CancellationPending Then Return
                bwReload_MovieSets.ReportProgress(iCount, KVP.Value)
                If Reload_MovieSet(KVP.Key, True) Then
                    doFill = True
                Else
                    bwReload_MovieSets.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_MovieSets_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_MovieSets.ProgressChanged
        If e.ProgressPercentage = -1 Then
            RefreshRow_MovieSet(CLng(e.UserState))
        Else
            SetStatus(e.UserState.ToString)
            tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_MovieSets_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_MovieSets.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        tslLoading.Text = String.Empty
        tspbLoading.Visible = False
        tslLoading.Visible = False

        If Res.doFill Then
            FillList(False, True, False)
        Else
            DoTitleCheck()
            SetControlsEnabled(True)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub bwReload_TVShows_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_TVShows.DoWork
        Dim reloadFull As Boolean = DirectCast(e.Argument, Boolean)
        Dim iCount As Integer = 0
        Dim ShowIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In dtTVShows.Rows
            ShowIDs.Add(Convert.ToInt64(sRow.Item("idShow")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In ShowIDs
                If bwReload_TVShows.CancellationPending Then Return
                bwReload_TVShows.ReportProgress(iCount, KVP.Value)
                If Reload_TVShow(KVP.Key, True, False, reloadFull) Then
                    doFill = True
                Else
                    bwReload_TVShows.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_TVShows_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_TVShows.ProgressChanged
        If e.ProgressPercentage = -1 Then
            RefreshRow_TVShow(CLng(e.UserState))
        Else
            SetStatus(e.UserState.ToString)
            tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_TVShows_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_TVShows.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        tslLoading.Text = String.Empty
        tspbLoading.Visible = False
        tslLoading.Visible = False

        If Res.doFill Then
            FillList(False, False, True)
        Else
            SetControlsEnabled(True)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub bwRewrite_Movies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwRewrite_Movies.DoWork
        Dim iCount As Integer = 0
        Dim MovieIDs As New Dictionary(Of Long, String)

        For Each sRow As DataRow In dtMovies.Rows
            MovieIDs.Add(Convert.ToInt64(sRow.Item("idMovie")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieIDs
                If bwRewrite_Movies.CancellationPending Then Return
                bwRewrite_Movies.ReportProgress(iCount, KVP.Value)
                RewriteMovie(KVP.Key, True)
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub bwRewrite_Movies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwRewrite_Movies.ProgressChanged
        SetStatus(e.UserState.ToString)
        tspbLoading.Value = e.ProgressPercentage
    End Sub

    Private Sub bwRewrite_Movies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRewrite_Movies.RunWorkerCompleted
        tslLoading.Text = String.Empty
        tslLoading.Visible = False
        tspbLoading.Visible = False
        btnCancel.Visible = False
        lblCanceling.Visible = False
        prbCanceling.Visible = False
        pnlCancel.Visible = False

        FillList(True, True, True)
    End Sub

    Private Sub cbFilterVideoSource_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterVideoSource_Movies.SelectedIndexChanged
        Try
            While fScanner.IsBusy OrElse bwLoadMovieInfo.IsBusy OrElse bwDownloadPic.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwRewrite_Movies.IsBusy OrElse bwCleanDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            For i As Integer = FilterArray_Movies.Count - 1 To 0 Step -1
                If FilterArray_Movies(i).ToString.StartsWith("VideoSource =") Then
                    FilterArray_Movies.RemoveAt(i)
                End If
            Next

            If Not cbFilterVideoSource_Movies.Text = Master.eLang.All Then
                FilterArray_Movies.Add(String.Format("VideoSource = '{0}'", If(cbFilterVideoSource_Movies.Text = Master.eLang.None, String.Empty, cbFilterVideoSource_Movies.Text)))
            End If

            RunFilter_Movies()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cbFilterLists_Movies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_Movies.SelectedIndexChanged
        While fScanner.IsBusy OrElse bwLoadMovieInfo.IsBusy OrElse bwDownloadPic.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwRewrite_Movies.IsBusy OrElse bwCleanDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        If Not currList_Movies = CType(cbFilterLists_Movies.SelectedItem, KeyValuePair(Of String, String)).Value Then
            currList_Movies = CType(cbFilterLists_Movies.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListMovies = currList_Movies
            FillList(True, False, False)
        End If
    End Sub

    Private Sub cbFilterLists_MovieSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_MovieSets.SelectedIndexChanged
        While fScanner.IsBusy OrElse bwLoadMovieSetInfo.IsBusy OrElse bwDownloadPic.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse bwRewrite_Movies.IsBusy OrElse bwCleanDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        If Not currList_MovieSets = CType(cbFilterLists_MovieSets.SelectedItem, KeyValuePair(Of String, String)).Value Then
            currList_MovieSets = CType(cbFilterLists_MovieSets.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListMovieSets = currList_MovieSets
            FillList(False, True, False)
        End If
    End Sub

    Private Sub cbFilterLists_Shows_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_Shows.SelectedIndexChanged
        While fScanner.IsBusy OrElse bwLoadMovieInfo.IsBusy OrElse bwDownloadPic.IsBusy OrElse bwReload_TVShows.IsBusy OrElse bwCleanDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        If Not currList_TVShows = CType(cbFilterLists_Shows.SelectedItem, KeyValuePair(Of String, String)).Value Then
            currList_TVShows = CType(cbFilterLists_Shows.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListTVShows = currList_TVShows
            FillList(False, False, True)
        End If
    End Sub

    Private Sub SetFilterMissing_Movies()
        Dim MissingFilter As New List(Of String)
        FilterArray_Movies.Remove(filMissing_Movies)
        If chkFilterMissing_Movies.Checked Then
            With Master.eSettings
                If .MovieMissingBanner Then MissingFilter.Add("BannerPath IS NULL OR BannerPath=''")
                If .MovieMissingClearArt Then MissingFilter.Add("ClearArtPath IS NULL OR ClearArtPath=''")
                If .MovieMissingClearLogo Then MissingFilter.Add("ClearLogoPath IS NULL OR ClearLogoPath=''")
                If .MovieMissingDiscArt Then MissingFilter.Add("DiscArtPath IS NULL OR DiscArtPath=''")
                If .MovieMissingExtrafanarts Then MissingFilter.Add("EFanartsPath IS NULL OR EFanartsPath=''")
                If .MovieMissingExtrathumbs Then MissingFilter.Add("EThumbsPath IS NULL OR EThumbsPath=''")
                If .MovieMissingFanart Then MissingFilter.Add("FanartPath IS NULL OR FanartPath=''")
                If .MovieMissingLandscape Then MissingFilter.Add("LandscapePath IS NULL OR LandscapePath=''")
                If .MovieMissingNFO Then MissingFilter.Add("NfoPath IS NULL OR NfoPath=''")
                If .MovieMissingPoster Then MissingFilter.Add("PosterPath IS NULL OR PosterPath=''")
                If .MovieMissingSubtitles Then MissingFilter.Add("HasSub = 0")
                If .MovieMissingTheme Then MissingFilter.Add("ThemePath IS NULL OR ThemePath=''")
                If .MovieMissingTrailer Then MissingFilter.Add("TrailerPath IS NULL OR TrailerPath=''")
            End With
            filMissing_Movies = Microsoft.VisualBasic.Strings.Join(MissingFilter.ToArray, " OR ")
            If filMissing_Movies IsNot Nothing Then FilterArray_Movies.Add(filMissing_Movies)
        End If
        RunFilter_Movies()
    End Sub

    Private Sub SetFilterMissing_MovieSets()
        Dim MissingFilter As New List(Of String)
        FilterArray_MovieSets.Remove(filMissing_MovieSets)
        If chkFilterMissing_MovieSets.Checked Then
            With Master.eSettings
                If .MovieSetMissingBanner Then MissingFilter.Add("BannerPath IS NULL OR BannerPath=''")
                If .MovieSetMissingClearArt Then MissingFilter.Add("ClearArtPath IS NULL OR ClearArtPath=''")
                If .MovieSetMissingClearLogo Then MissingFilter.Add("ClearLogoPath IS NULL OR ClearLogoPath=''")
                If .MovieSetMissingDiscArt Then MissingFilter.Add("DiscArtPath IS NULL OR DiscArtPath=''")
                If .MovieSetMissingFanart Then MissingFilter.Add("FanartPath IS NULL OR FanartPath=''")
                If .MovieSetMissingLandscape Then MissingFilter.Add("LandscapePath IS NULL OR LandscapePath=''")
                If .MovieSetMissingNFO Then MissingFilter.Add("NfoPath IS NULL OR NfoPath=''")
                If .MovieSetMissingPoster Then MissingFilter.Add("PosterPath IS NULL OR PosterPath=''")
            End With
            filMissing_MovieSets = Microsoft.VisualBasic.Strings.Join(MissingFilter.ToArray, " OR ")
            If filMissing_MovieSets IsNot Nothing Then FilterArray_MovieSets.Add(filMissing_MovieSets)
        End If
        RunFilter_MovieSets()
    End Sub

    Private Sub SetFilterMissing_Shows()
        Dim MissingFilter As New List(Of String)
        FilterArray_TVShows.Remove(filMissing_TVShows)
        If chkFilterMissing_Shows.Checked Then
            With Master.eSettings
                If .TVShowMissingBanner Then MissingFilter.Add("BannerPath IS NULL OR BannerPath=''")
                If .TVShowMissingCharacterArt Then MissingFilter.Add("CharacterArtPath IS NULL OR CharacterArtPath=''")
                If .TVShowMissingClearArt Then MissingFilter.Add("ClearArtPath IS NULL OR ClearArtPath=''")
                If .TVShowMissingClearLogo Then MissingFilter.Add("ClearLogoPath IS NULL OR ClearLogoPath=''")
                If .TVShowMissingExtrafanarts Then MissingFilter.Add("EFanartsPath IS NULL OR EFanartsPath=''")
                If .TVShowMissingFanart Then MissingFilter.Add("FanartPath IS NULL OR FanartPath=''")
                If .TVShowMissingLandscape Then MissingFilter.Add("LandscapePath IS NULL OR LandscapePath=''")
                If .TVShowMissingNFO Then MissingFilter.Add("NfoPath IS NULL OR NfoPath=''")
                If .TVShowMissingPoster Then MissingFilter.Add("PosterPath IS NULL OR PosterPath=''")
                If .TVShowMissingTheme Then MissingFilter.Add("ThemePath IS NULL OR ThemePath=''")
            End With
            filMissing_TVShows = Microsoft.VisualBasic.Strings.Join(MissingFilter.ToArray, " OR ")
            If filMissing_TVShows IsNot Nothing Then FilterArray_TVShows.Add(filMissing_TVShows)
        End If
        RunFilter_Shows()
    End Sub


    Private Sub SetFilterYear_Movies()
        Try
            If Not String.IsNullOrEmpty(cbFilterYearFrom_Movies.Text) AndAlso Not cbFilterYearFrom_Movies.Text = Master.eLang.All Then

                FilterArray_Movies.Remove(filYear_Movies)
                filYear_Movies = String.Empty

                Select Case cbFilterYearModFrom_Movies.Text
                    Case ">="
                        RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                        cbFilterYearModTo_Movies.Enabled = True
                        AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                        RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                        cbFilterYearTo_Movies.Enabled = True
                        AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                        If Not String.IsNullOrEmpty(cbFilterYearTo_Movies.Text) AndAlso Not cbFilterYearTo_Movies.Text = Master.eLang.All Then
                            filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text,
                                                              "' AND Year ", cbFilterYearModTo_Movies.Text, " '", cbFilterYearTo_Movies.Text, "'")
                        Else
                            filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, "'")
                        End If

                    Case ">"
                        RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                        cbFilterYearModTo_Movies.Enabled = True
                        AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                        RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                        cbFilterYearTo_Movies.Enabled = True
                        AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                        If Not String.IsNullOrEmpty(cbFilterYearTo_Movies.Text) AndAlso Not cbFilterYearTo_Movies.Text = Master.eLang.All Then
                            filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text,
                                                              "' AND Year ", cbFilterYearModTo_Movies.Text, " '", cbFilterYearTo_Movies.Text, "'")
                        Else
                            filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, "'")
                        End If

                    Case Else
                        RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                        cbFilterYearModTo_Movies.Enabled = False
                        cbFilterYearModTo_Movies.SelectedIndex = 0
                        AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                        RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                        cbFilterYearTo_Movies.Enabled = False
                        cbFilterYearTo_Movies.SelectedIndex = 0
                        AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                        filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, "'")
                End Select

                FilterArray_Movies.Add(filYear_Movies)
                RunFilter_Movies()
            Else
                RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                cbFilterYearModTo_Movies.Enabled = False
                cbFilterYearModTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                cbFilterYearTo_Movies.Enabled = False
                cbFilterYearTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                If Not String.IsNullOrEmpty(filYear_Movies) Then
                    FilterArray_Movies.Remove(filYear_Movies)
                    filYear_Movies = String.Empty
                    RunFilter_Movies()
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cbFilterYearModFrom_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearModFrom_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbFilterYearModTo_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearModTo_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbFilterYearFrom_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearFrom_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbFilterYearTo_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearTo_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbSearchMovies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSearchMovies.SelectedIndexChanged
        currTextSearch_Movies = txtSearchMovies.Text

        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub cbSearchMovieSets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSearchMovieSets.SelectedIndexChanged
        currTextSearch_MovieSets = txtSearchMovieSets.Text

        tmrSearchWait_MovieSets.Enabled = False
        tmrSearch_MovieSets.Enabled = False
        tmrSearchWait_MovieSets.Enabled = True
    End Sub

    Private Sub cbSearchShows_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSearchShows.SelectedIndexChanged
        currTextSearch_TVShows = txtSearchShows.Text

        tmrSearchWait_Shows.Enabled = False
        tmrSearch_Shows.Enabled = False
        tmrSearchWait_Shows.Enabled = True
    End Sub

    Private Sub chkFilterDuplicates_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterDuplicates_Movies.Click
        Try
            RunFilter_Movies(True)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub chkFilterEmpty_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterEmpty_MovieSets.Click
        If chkFilterEmpty_MovieSets.Checked Then
            FilterArray_MovieSets.Add("Count = 0")
        Else
            FilterArray_MovieSets.Remove("Count = 0")
        End If
        RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterMultiple_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMultiple_MovieSets.Click
        If chkFilterMultiple_MovieSets.Checked Then
            FilterArray_MovieSets.Add("Count > 1")
        Else
            FilterArray_MovieSets.Remove("Count > 1")
        End If
        RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterOne_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterOne_MovieSets.Click
        If chkFilterOne_MovieSets.Checked Then
            FilterArray_MovieSets.Add("Count = 1")
        Else
            FilterArray_MovieSets.Remove("Count = 1")
        End If
        RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterLock_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterLock_Movies.Click
        If chkFilterLock_Movies.Checked Then
            FilterArray_Movies.Add("Lock = 1")
        Else
            FilterArray_Movies.Remove("Lock = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkFilterLock_MovieSets_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterLock_MovieSets.Click
        If chkFilterLock_MovieSets.Checked Then
            FilterArray_MovieSets.Add("Lock = 1")
        Else
            FilterArray_MovieSets.Remove("Lock = 1")
        End If
        RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterLock_Shows_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterLock_Shows.Click
        If chkFilterLock_Shows.Checked Then
            FilterArray_TVShows.Add("Lock = 1")
        Else
            FilterArray_TVShows.Remove("Lock = 1")
        End If
        RunFilter_Shows()
    End Sub

    Private Sub chkFilterMark_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMark_Movies.Click
        If chkFilterMark_Movies.Checked Then
            FilterArray_Movies.Add("mark = 1")
        Else
            FilterArray_Movies.Remove("mark = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkFilterMark_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMark_MovieSets.Click
        If chkFilterMark_MovieSets.Checked Then
            FilterArray_MovieSets.Add("mark = 1")
        Else
            FilterArray_MovieSets.Remove("mark = 1")
        End If
        RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterMark_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMark_Shows.Click
        If chkFilterMark_Shows.Checked Then
            FilterArray_TVShows.Add("mark = 1")
        Else
            FilterArray_TVShows.Remove("mark = 1")
        End If
        RunFilter_Shows()
    End Sub

    Private Sub chkFilterMarkCustom1_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom1_Movies.Click
        If chkFilterMarkCustom1_Movies.Checked Then
            FilterArray_Movies.Add("markcustom1 = 1")
        Else
            FilterArray_Movies.Remove("markcustom1 = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkFilterMarkCustom2_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom2_Movies.Click
        If chkFilterMarkCustom2_Movies.Checked Then
            FilterArray_Movies.Add("markcustom2 = 1")
        Else
            FilterArray_Movies.Remove("markcustom2 = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkFilterMarkCustom3_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom3_Movies.Click
        If chkFilterMarkCustom3_Movies.Checked Then
            FilterArray_Movies.Add("markcustom3 = 1")
        Else
            FilterArray_Movies.Remove("markcustom3 = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkFilterMarkCustom4_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom4_Movies.Click
        If chkFilterMarkCustom4_Movies.Checked Then
            FilterArray_Movies.Add("markcustom4 = 1")
        Else
            FilterArray_Movies.Remove("markcustom4 = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkFilterMissing_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterMissing_Movies.Click
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkFilterMissing_MovieSets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterMissing_MovieSets.Click
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkFilterMissing_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterMissing_Shows.Click
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkFilterNew_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNew_Movies.Click
        If chkFilterNew_Movies.Checked Then
            FilterArray_Movies.Add("new = 1")
        Else
            FilterArray_Movies.Remove("new = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkFilterNew_Moviesets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNew_MovieSets.Click
        If chkFilterNew_MovieSets.Checked Then
            FilterArray_MovieSets.Add("new = 1")
        Else
            FilterArray_MovieSets.Remove("new = 1")
        End If
        RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterNewEpisodes_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNewEpisodes_Shows.Click
        If chkFilterNewEpisodes_Shows.Checked Then
            FilterArray_TVShows.Add("NewEpisodes > 0")
        Else
            FilterArray_TVShows.Remove("NewEpisodes > 0")
        End If
        RunFilter_Shows()
    End Sub

    Private Sub chkFilterNewShows_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNewShows_Shows.Click
        If chkFilterNewShows_Shows.Checked Then
            FilterArray_TVShows.Add("new = 1")
        Else
            FilterArray_TVShows.Remove("new = 1")
        End If
        RunFilter_Shows()
    End Sub

    Private Sub chkFilterTolerance_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterTolerance_Movies.Click
        If chkFilterTolerance_Movies.Checked Then
            FilterArray_Movies.Add("OutOfTolerance = 1")
        Else
            FilterArray_Movies.Remove("OutOfTolerance = 1")
        End If
        RunFilter_Movies()
    End Sub

    Private Sub chkMovieMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingBanner.CheckedChanged
        Master.eSettings.MovieMissingBanner = chkMovieMissingBanner.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingClearArt.CheckedChanged
        Master.eSettings.MovieMissingClearArt = chkMovieMissingClearArt.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingClearLogo.CheckedChanged
        Master.eSettings.MovieMissingClearLogo = chkMovieMissingClearLogo.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingDiscArt.CheckedChanged
        Master.eSettings.MovieMissingDiscArt = chkMovieMissingDiscArt.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingExtrafanarts_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingExtrafanarts.CheckedChanged
        Master.eSettings.MovieMissingExtrafanarts = chkMovieMissingExtrafanarts.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingExtrathumbs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingExtrathumbs.CheckedChanged
        Master.eSettings.MovieMissingExtrathumbs = chkMovieMissingExtrathumbs.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingFanart.CheckedChanged
        Master.eSettings.MovieMissingFanart = chkMovieMissingFanart.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingLandscape.CheckedChanged
        Master.eSettings.MovieMissingLandscape = chkMovieMissingLandscape.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingNFO.CheckedChanged
        Master.eSettings.MovieMissingNFO = chkMovieMissingNFO.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingPoster.CheckedChanged
        Master.eSettings.MovieMissingPoster = chkMovieMissingPoster.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingSubtitles_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingSubtitles.CheckedChanged
        Master.eSettings.MovieMissingSubtitles = chkMovieMissingSubtitles.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingTheme_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingTheme.CheckedChanged
        Master.eSettings.MovieMissingTheme = chkMovieMissingTheme.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingTrailer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingTrailer.CheckedChanged
        Master.eSettings.MovieMissingTrailer = chkMovieMissingTrailer.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieSetMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingBanner.CheckedChanged
        Master.eSettings.MovieSetMissingBanner = chkMovieSetMissingBanner.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingClearArt.CheckedChanged
        Master.eSettings.MovieSetMissingClearArt = chkMovieSetMissingClearArt.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingClearLogo.CheckedChanged
        Master.eSettings.MovieSetMissingClearLogo = chkMovieSetMissingClearLogo.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingDiscArt.CheckedChanged
        Master.eSettings.MovieSetMissingDiscArt = chkMovieSetMissingDiscArt.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingFanart.CheckedChanged
        Master.eSettings.MovieSetMissingFanart = chkMovieSetMissingFanart.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingLandscape.CheckedChanged
        Master.eSettings.MovieSetMissingLandscape = chkMovieSetMissingLandscape.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingNFO.CheckedChanged
        Master.eSettings.MovieSetMissingNFO = chkMovieSetMissingNFO.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingPoster.CheckedChanged
        Master.eSettings.MovieSetMissingPoster = chkMovieSetMissingPoster.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkShowMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingBanner.CheckedChanged
        Master.eSettings.TVShowMissingBanner = chkShowMissingBanner.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingCharacterArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingCharacterArt.CheckedChanged
        Master.eSettings.TVShowMissingCharacterArt = chkShowMissingCharacterArt.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingClearArt.CheckedChanged
        Master.eSettings.TVShowMissingClearArt = chkShowMissingClearArt.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingClearLogo.CheckedChanged
        Master.eSettings.TVShowMissingClearLogo = chkShowMissingClearLogo.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingExtrafanarts_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingExtrafanarts.CheckedChanged
        Master.eSettings.TVShowMissingExtrafanarts = chkShowMissingExtrafanarts.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingFanart.CheckedChanged
        Master.eSettings.TVShowMissingFanart = chkShowMissingFanart.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingLandscape.CheckedChanged
        Master.eSettings.TVShowMissingLandscape = chkShowMissingLandscape.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingNFO.CheckedChanged
        Master.eSettings.TVShowMissingNFO = chkShowMissingNFO.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingPoster.CheckedChanged
        Master.eSettings.TVShowMissingPoster = chkShowMissingPoster.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingTheme_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingTheme.CheckedChanged
        Master.eSettings.TVShowMissingTheme = chkShowMissingTheme.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub clbFilterGenres_Movies_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterGenres_Movies.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterGenres_Movies.Items.Count - 1
                clbFilterGenres_Movies.SetItemChecked(i, False)
            Next
        Else
            clbFilterGenres_Movies.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterGenres_Shows_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterGenres_Shows.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterGenres_Shows.Items.Count - 1
                clbFilterGenres_Shows.SetItemChecked(i, False)
            Next
        Else
            clbFilterGenres_Shows.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterCountries_Movies_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterCountries_Movies.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterCountries_Movies.Items.Count - 1
                clbFilterCountries_Movies.SetItemChecked(i, False)
            Next
        Else
            clbFilterCountries_Movies.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterGenres_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterGenres_Movies.LostFocus
        Try
            pnlFilterGenres_Movies.Visible = False
            pnlFilterGenres_Movies.Tag = "NO"

            If clbFilterGenres_Movies.CheckedItems.Count > 0 Then
                txtFilterGenre_Movies.Text = String.Empty
                FilterArray_Movies.Remove(filGenre_Movies)

                Dim alGenres As New List(Of String)
                alGenres.AddRange(clbFilterGenres_Movies.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Movies.Checked Then
                    txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")
                Else
                    txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")
                End If

                For i As Integer = 0 To alGenres.Count - 1
                    If alGenres.Item(i) = Master.eLang.None Then
                        alGenres.Item(i) = "Genre LIKE ''"
                    Else
                        alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                    End If
                Next

                If rbFilterAnd_Movies.Checked Then
                    filGenre_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND "))
                Else
                    filGenre_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR "))
                End If

                FilterArray_Movies.Add(filGenre_Movies)
                RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(filGenre_Movies) Then
                    txtFilterGenre_Movies.Text = String.Empty
                    FilterArray_Movies.Remove(filGenre_Movies)
                    filGenre_Movies = String.Empty
                    RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterGenres_Shows_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterGenres_Shows.LostFocus
        Try
            pnlFilterGenres_Shows.Visible = False
            pnlFilterGenres_Shows.Tag = "NO"

            If clbFilterGenres_Shows.CheckedItems.Count > 0 Then
                txtFilterGenre_Shows.Text = String.Empty
                FilterArray_TVShows.Remove(filGenre_TVShows)

                Dim alGenres As New List(Of String)
                alGenres.AddRange(clbFilterGenres_Shows.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Shows.Checked Then
                    txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")
                Else
                    txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")
                End If

                For i As Integer = 0 To alGenres.Count - 1
                    If alGenres.Item(i) = Master.eLang.None Then
                        alGenres.Item(i) = "Genre LIKE ''"
                    Else
                        alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                    End If
                Next

                If rbFilterAnd_Shows.Checked Then
                    filGenre_TVShows = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND "))
                Else
                    filGenre_TVShows = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR "))
                End If

                FilterArray_TVShows.Add(filGenre_TVShows)
                RunFilter_Shows()
            Else
                If Not String.IsNullOrEmpty(filGenre_TVShows) Then
                    txtFilterGenre_Shows.Text = String.Empty
                    FilterArray_TVShows.Remove(filGenre_TVShows)
                    filGenre_TVShows = String.Empty
                    RunFilter_Shows()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterCountries_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterCountries_Movies.LostFocus
        Try
            pnlFilterCountries_Movies.Visible = False
            pnlFilterCountries_Movies.Tag = "NO"

            If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
                txtFilterCountry_Movies.Text = String.Empty
                FilterArray_Movies.Remove(filCountry_Movies)

                Dim alCountries As New List(Of String)
                alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Movies.Checked Then
                    txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")
                Else
                    txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")
                End If

                For i As Integer = 0 To alCountries.Count - 1
                    If alCountries.Item(i) = Master.eLang.None Then
                        alCountries.Item(i) = "Country LIKE ''"
                    Else
                        alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
                    End If
                Next

                If rbFilterAnd_Movies.Checked Then
                    filCountry_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND "))
                Else
                    filCountry_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR "))
                End If

                FilterArray_Movies.Add(filCountry_Movies)
                RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(filCountry_Movies) Then
                    txtFilterCountry_Movies.Text = String.Empty
                    FilterArray_Movies.Remove(filCountry_Movies)
                    filCountry_Movies = String.Empty
                    RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterDataFields_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterDataFields_Movies.LostFocus, cbFilterDataField_Movies.SelectedIndexChanged
        Try
            pnlFilterDataFields_Movies.Visible = False
            pnlFilterDataFields_Movies.Tag = "NO"

            If clbFilterDataFields_Movies.CheckedItems.Count > 0 Then
                txtFilterDataField_Movies.Text = String.Empty
                FilterArray_Movies.Remove(filDataField_Movies)

                Dim alDataFields As New List(Of String)
                alDataFields.AddRange(clbFilterDataFields_Movies.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Movies.Checked Then
                    txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND ")
                Else
                    txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR ")
                End If

                For i As Integer = 0 To alDataFields.Count - 1
                    If cbFilterDataField_Movies.SelectedIndex = 0 Then
                        alDataFields.Item(i) = String.Format("{0} IS NULL OR {0} = ''", alDataFields.Item(i))
                    Else
                        alDataFields.Item(i) = String.Format("{0} NOT IS NULL AND {0} NOT = ''", alDataFields.Item(i))
                    End If
                Next

                If rbFilterAnd_Movies.Checked Then
                    filDataField_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND "))
                Else
                    filDataField_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR "))
                End If

                FilterArray_Movies.Add(filDataField_Movies)
                RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(filDataField_Movies) Then
                    txtFilterDataField_Movies.Text = String.Empty
                    FilterArray_Movies.Remove(filDataField_Movies)
                    filDataField_Movies = String.Empty
                    RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterSource_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterSources_Movies.LostFocus
        Try
            pnlFilterSources_Movies.Visible = False
            pnlFilterSources_Movies.Tag = "NO"

            If clbFilterSources_Movies.CheckedItems.Count > 0 Then
                txtFilterSource_Movies.Text = String.Empty
                FilterArray_Movies.Remove(filSource_Movies)

                Dim alSource As New List(Of String)
                alSource.AddRange(clbFilterSources_Movies.CheckedItems.OfType(Of String).ToList)

                txtFilterSource_Movies.Text = Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " | ")

                For i As Integer = 0 To alSource.Count - 1
                    alSource.Item(i) = String.Format("Source = '{0}'", alSource.Item(i))
                Next

                filSource_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " OR "))

                FilterArray_Movies.Add(filSource_Movies)
                RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(filSource_Movies) Then
                    txtFilterSource_Movies.Text = String.Empty
                    FilterArray_Movies.Remove(filSource_Movies)
                    filSource_Movies = String.Empty
                    RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterSource_Shows_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterSource_Shows.LostFocus
        Try
            pnlFilterSources_Shows.Visible = False
            pnlFilterSources_Shows.Tag = "NO"

            If clbFilterSource_Shows.CheckedItems.Count > 0 Then
                txtFilterSource_Shows.Text = String.Empty
                FilterArray_TVShows.Remove(filSource_TVShows)

                Dim alSource As New List(Of String)
                alSource.AddRange(clbFilterSource_Shows.CheckedItems.OfType(Of String).ToList)

                txtFilterSource_Shows.Text = Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " | ")

                For i As Integer = 0 To alSource.Count - 1
                    alSource.Item(i) = String.Format("Source = '{0}'", alSource.Item(i))
                Next

                filSource_TVShows = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " OR "))

                FilterArray_TVShows.Add(filSource_TVShows)
                RunFilter_Shows()
            Else
                If Not String.IsNullOrEmpty(filSource_TVShows) Then
                    txtFilterSource_Shows.Text = String.Empty
                    FilterArray_TVShows.Remove(filSource_TVShows)
                    filSource_TVShows = String.Empty
                    RunFilter_Shows()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub mnuMainToolsCleanDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsCleanDB.Click, cmnuTrayToolsCleanDB.Click
        CleanDB(New Structures.ScanOrClean With {.Movies = True, .MovieSets = True, .TV = True})
    End Sub

    Private Sub CleanDB(ByVal Clean As Structures.ScanOrClean)
        SetControlsEnabled(False, True)
        tspbLoading.Style = ProgressBarStyle.Marquee
        EnableFilters_Movies(False)
        EnableFilters_MovieSets(False)
        EnableFilters_Shows(False)

        SetStatus(Master.eLang.GetString(644, "Cleaning Database..."))
        tspbLoading.Visible = True

        bwCleanDB.WorkerSupportsCancellation = True
        bwCleanDB.RunWorkerAsync(Clean)
    End Sub

    Private Sub CleanFiles()
        FileUtils.CleanUp.DoCleanUp()
        'Try
        '    Dim sWarning As String = String.Empty
        '    Dim sWarningFile As String = String.Empty
        '    With Master.eSettings
        '        If .FileSystemExpertCleaner Then
        '            sWarning = String.Concat(Master.eLang.GetString(102, "WARNING: If you continue, all non-whitelisted file types will be deleted!"), Environment.NewLine, Environment.NewLine, Master.eLang.GetString(101, "Are you sure you want to continue?"))
        '        Else
        '            If .CleanDotFanartJPG Then sWarningFile += String.Concat("<movie>.fanart.jpg", Environment.NewLine)
        '            If .CleanFanartJPG Then sWarningFile += String.Concat("fanart.jpg", Environment.NewLine)
        '            If .CleanFolderJPG Then sWarningFile += String.Concat("folder.jpg", Environment.NewLine)
        '            If .CleanMovieFanartJPG Then sWarningFile += String.Concat("<movie>-fanart.jpg", Environment.NewLine)
        '            If .CleanMovieJPG Then sWarningFile += String.Concat("movie.jpg", Environment.NewLine)
        '            If .CleanMovieNameJPG Then sWarningFile += String.Concat("<movie>.jpg", Environment.NewLine)
        '            If .CleanMovieNFO Then sWarningFile += String.Concat("movie.nfo", Environment.NewLine)
        '            If .CleanMovieNFOB Then sWarningFile += String.Concat("<movie>.nfo", Environment.NewLine)
        '            If .CleanMovieTBN Then sWarningFile += String.Concat("movie.tbn", Environment.NewLine)
        '            If .CleanMovieTBNB Then sWarningFile += String.Concat("<movie>.tbn", Environment.NewLine)
        '            If .CleanPosterJPG Then sWarningFile += String.Concat("poster.jpg", Environment.NewLine)
        '            If .CleanPosterTBN Then sWarningFile += String.Concat("poster.tbn", Environment.NewLine)
        '            If .CleanExtrathumbs Then sWarningFile += String.Concat("/extrathumbs/", Environment.NewLine)
        '            sWarning = String.Concat(Master.eLang.GetString(103, "WARNING: If you continue, all files of the following types will be permanently deleted:"), Environment.NewLine, Environment.NewLine, sWarningFile, Environment.NewLine, Master.eLang.GetString(101, "Are you sure you want to continue?"))
        '        End If
        '    End With
        '    If MessageBox.Show(sWarning, Master.eLang.GetString(104, "Are you sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
        '        NonScrape(Enums.TaskManagerType.CleanFolders)
        '    End If
        'Catch ex As Exception
        '    logger.Error(ex, New StackFrame().GetMethod().Name)
        'End Try
    End Sub

    Private Sub mnuMainToolsCleanFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsCleanFiles.Click, cmnuTrayToolsCleanFiles.Click
        CleanFiles()
    End Sub

    Private Sub mnuMainToolsClearCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsClearCache.Click, cmnuTrayToolsClearCache.Click
        FileUtils.Delete.Cache_All()
    End Sub

    Private Sub ClearFilters_Movies(Optional ByVal Reload As Boolean = False)
        Try
            bsMovies.RemoveFilter()
            FilterArray_Movies.Clear()
            filSearch_Movies = String.Empty
            filGenre_Movies = String.Empty
            filYear_Movies = String.Empty
            filSource_Movies = String.Empty

            RemoveHandler txtSearchMovies.TextChanged, AddressOf txtSearchMovies_TextChanged
            txtSearchMovies.Text = String.Empty
            AddHandler txtSearchMovies.TextChanged, AddressOf txtSearchMovies_TextChanged
            If cbSearchMovies.Items.Count > 0 Then
                cbSearchMovies.SelectedIndex = 0
            End If

            chkFilterDuplicates_Movies.Checked = False
            chkFilterTolerance_Movies.Checked = False
            chkFilterMissing_Movies.Checked = False
            chkFilterMark_Movies.Checked = False
            chkFilterMarkCustom1_Movies.Checked = False
            chkFilterMarkCustom2_Movies.Checked = False
            chkFilterMarkCustom3_Movies.Checked = False
            chkFilterMarkCustom4_Movies.Checked = False
            chkFilterNew_Movies.Checked = False
            chkFilterLock_Movies.Checked = False
            pnlFilterMissingItems_Movies.Visible = False
            rbFilterOr_Movies.Checked = False
            rbFilterAnd_Movies.Checked = True
            txtFilterGenre_Movies.Text = String.Empty
            For i As Integer = 0 To clbFilterGenres_Movies.Items.Count - 1
                clbFilterGenres_Movies.SetItemChecked(i, False)
            Next
            txtFilterCountry_Movies.Text = String.Empty
            For i As Integer = 0 To clbFilterCountries_Movies.Items.Count - 1
                clbFilterCountries_Movies.SetItemChecked(i, False)
            Next
            txtFilterDataField_Movies.Text = String.Empty
            For i As Integer = 0 To clbFilterDataFields_Movies.Items.Count - 1
                clbFilterDataFields_Movies.SetItemChecked(i, False)
            Next
            txtFilterSource_Movies.Text = String.Empty
            For i As Integer = 0 To clbFilterSources_Movies.Items.Count - 1
                clbFilterSources_Movies.SetItemChecked(i, False)
            Next

            RemoveHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus
            If cbFilterDataField_Movies.Items.Count > 0 Then
                cbFilterDataField_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus

            RemoveHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearFrom_Movies_SelectedIndexChanged
            If cbFilterYearFrom_Movies.Items.Count > 0 Then
                cbFilterYearFrom_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearFrom_Movies_SelectedIndexChanged

            RemoveHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearModFrom_Movies_SelectedIndexChanged
            If cbFilterYearModFrom_Movies.Items.Count > 0 Then
                cbFilterYearModFrom_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearModFrom_Movies_SelectedIndexChanged

            RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
            If cbFilterYearTo_Movies.Items.Count > 0 Then
                cbFilterYearTo_Movies.SelectedIndex = 0
            End If
            cbFilterYearTo_Movies.Enabled = False
            AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

            RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
            If cbFilterYearModTo_Movies.Items.Count > 0 Then
                cbFilterYearModTo_Movies.SelectedIndex = 0
            End If
            cbFilterYearModTo_Movies.Enabled = False
            AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

            RemoveHandler cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf cbFilterVideoSource_Movies_SelectedIndexChanged
            If cbFilterVideoSource_Movies.Items.Count > 0 Then
                cbFilterVideoSource_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf cbFilterVideoSource_Movies_SelectedIndexChanged

            If Reload Then FillList(True, False, False)

            ModulesManager.Instance.RuntimeObjects.FilterMovies = String.Empty
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub ClearFilters_MovieSets(Optional ByVal Reload As Boolean = False)
        Try
            bsMovieSets.RemoveFilter()
            FilterArray_MovieSets.Clear()
            filSearch_MovieSets = String.Empty
            'Me.filGenre_Moviesets = String.Empty
            'Me.filYear_Moviesets = String.Empty
            'Me.filSource_Moviesets = String.Empty

            RemoveHandler txtSearchMovieSets.TextChanged, AddressOf txtSearchMovieSets_TextChanged
            txtSearchMovieSets.Text = String.Empty
            AddHandler txtSearchMovieSets.TextChanged, AddressOf txtSearchMovieSets_TextChanged
            If cbSearchMovieSets.Items.Count > 0 Then
                cbSearchMovieSets.SelectedIndex = 0
            End If

            'Me.chkFilterDuplicates.Checked = False
            'Me.chkFilterTolerance.Checked = False
            chkFilterEmpty_MovieSets.Checked = False
            chkFilterMissing_MovieSets.Checked = False
            chkFilterMark_MovieSets.Checked = False
            'Me.chkFilterMarkCustom1.Checked = False
            'Me.chkFilterMarkCustom2.Checked = False
            'Me.chkFilterMarkCustom3.Checked = False
            'Me.chkFilterMarkCustom4.Checked = False
            chkFilterMultiple_MovieSets.Checked = False
            chkFilterNew_MovieSets.Checked = False
            chkFilterLock_MovieSets.Checked = False
            chkFilterOne_MovieSets.Checked = False
            pnlFilterMissingItems_MovieSets.Visible = False
            rbFilterOr_MovieSets.Checked = False
            rbFilterAnd_MovieSets.Checked = True
            'Me.txtFilterGenre.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterGenres.Items.Count - 1
            '    Me.clbFilterGenres.SetItemChecked(i, False)
            'Next
            'Me.txtFilterCountry.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterCountries.Items.Count - 1
            '    Me.clbFilterCountries.SetItemChecked(i, False)
            'Next
            'Me.txtFilterSource.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterSource.Items.Count - 1
            '    Me.clbFilterSource.SetItemChecked(i, False)
            'Next

            'RemoveHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged
            'If Me.cbFilterYear.Items.Count > 0 Then
            '    Me.cbFilterYear.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged

            'RemoveHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged
            'If Me.cbFilterYearMod.Items.Count > 0 Then
            '    Me.cbFilterYearMod.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged

            'RemoveHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged
            'If Me.cbFilterFileSource.Items.Count > 0 Then
            '    Me.cbFilterFileSource.SelectedIndex = 0
            'End If
            'AddHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged

            If Reload Then FillList(False, True, False)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub ClearFilters_Shows(Optional ByVal Reload As Boolean = False)
        Try
            bsTVShows.RemoveFilter()
            FilterArray_TVShows.Clear()
            filSearch_TVShows = String.Empty
            filGenre_TVShows = String.Empty
            'Me.filYear_Shows = String.Empty
            filSource_TVShows = String.Empty

            RemoveHandler txtSearchShows.TextChanged, AddressOf txtSearchShows_TextChanged
            txtSearchShows.Text = String.Empty
            AddHandler txtSearchShows.TextChanged, AddressOf txtSearchShows_TextChanged
            If cbSearchShows.Items.Count > 0 Then
                cbSearchShows.SelectedIndex = 0
            End If

            'Me.chkFilterDuplicates.Checked = False
            'Me.chkFilterTolerance.Checked = False
            chkFilterMissing_Shows.Checked = False
            chkFilterMark_Shows.Checked = False
            'Me.chkFilterMarkCustom1.Checked = False
            'Me.chkFilterMarkCustom2.Checked = False
            'Me.chkFilterMarkCustom3.Checked = False
            'Me.chkFilterMarkCustom4.Checked = False
            chkFilterNewEpisodes_Shows.Checked = False
            chkFilterNewShows_Shows.Checked = False
            chkFilterLock_Shows.Checked = False
            pnlFilterMissingItems_Shows.Visible = False
            rbFilterOr_Shows.Checked = False
            rbFilterAnd_Shows.Checked = True
            txtFilterGenre_Shows.Text = String.Empty
            For i As Integer = 0 To clbFilterGenres_Shows.Items.Count - 1
                clbFilterGenres_Shows.SetItemChecked(i, False)
            Next
            'Me.txtFilterCountry.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterCountries.Items.Count - 1
            '    Me.clbFilterCountries.SetItemChecked(i, False)
            'Next
            txtFilterSource_Shows.Text = String.Empty
            For i As Integer = 0 To clbFilterSource_Shows.Items.Count - 1
                clbFilterSource_Shows.SetItemChecked(i, False)
            Next

            'RemoveHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged
            'If Me.cbFilterYear.Items.Count > 0 Then
            '    Me.cbFilterYear.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged

            'RemoveHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged
            'If Me.cbFilterYearMod.Items.Count > 0 Then
            '    Me.cbFilterYearMod.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged

            'RemoveHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged
            'If Me.cbFilterFileSource.Items.Count > 0 Then
            '    Me.cbFilterFileSource.SelectedIndex = 0
            'End If
            'AddHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged

            If Reload Then FillList(False, False, True)

            ModulesManager.Instance.RuntimeObjects.FilterTVShows = String.Empty
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuShowOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowOpenFolder.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVShows.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    Using Explorer As New Diagnostics.Process

                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", sRow.Cells("TVShowPath").Value.ToString)
                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            Explorer.StartInfo.Arguments = String.Format("""{0}""", sRow.Cells("TVShowPath").Value.ToString)
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub cmnuShowClearCacheDataAndImages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowClearCacheDataAndImages.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, True, True)
    End Sub

    Private Sub cmnuShowClearCacheDataOnly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowClearCacheDataOnly.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, True, False)
    End Sub

    Private Sub cmnuShowClearCacheImagesOnly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowClearCacheImagesOnly.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, False, True)
    End Sub

    Private Sub cmnuEpisodeChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeChange.Click
        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim ShowID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idShow", indX).Value)

        SetControlsEnabled(False, True)

        Dim tmpEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
        Dim tmpShow As Database.DBElement = Master.DB.Load_TVShow(ShowID, False, False)

        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)

        If Not ModulesManager.Instance.ScrapeData_TVShow(tmpShow, ScrapeModifiers, Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, True) Then
            If tmpShow.Episodes.Count > 0 Then
                Dim dlgChangeEp As New dlgTVChangeEp(tmpShow)
                If dlgChangeEp.ShowDialog = DialogResult.OK Then
                    If dlgChangeEp.Result.Count > 0 Then
                        Master.DB.Change_TVEpisode(tmpEpisode, dlgChangeEp.Result, False)
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(943, "There are no known episodes for this show. Scrape the show, season, or episode and try again."), Master.eLang.GetString(944, "No Known Episodes"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        RefreshRow_TVShow(ShowID, True)

        SetControlsEnabled(True)
    End Sub

    Private Sub cmnuShowChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowChange.Click
        If dgvTVShows.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)
            CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifiers)
        End If
    End Sub

    Private Sub cmnuSeasonRemoveFromDisk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonRemoveFromDisk.Click
        Try

            Dim SeasonsToDelete As New Dictionary(Of Long, Long)
            Dim ShowId As Long = -1
            Dim SeasonNum As Integer = -1

            For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                ShowId = Convert.ToInt64(sRow.Cells("idShow").Value)
                SeasonNum = Convert.ToInt32(sRow.Cells("Season").Value)
                'seasonnum first... showid can't be key or else only one season will be deleted
                If Not SeasonsToDelete.ContainsKey(SeasonNum) Then
                    SeasonsToDelete.Add(SeasonNum, ShowId)
                End If
            Next

            If SeasonsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(SeasonsToDelete, Enums.ContentType.TVSeason) = DialogResult.OK Then
                        FillSeasons(Convert.ToInt64(dgvTVSeasons.Item("idShow", currRow_TVSeason).Value))
                        SetTVCount()
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuEpisodeRemoveFromDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRemoveFromDisk.Click
        Try

            Dim EpsToDelete As New Dictionary(Of Long, Long)
            Dim EpId As Long = -1

            For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                EpId = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                If Not EpsToDelete.ContainsKey(EpId) Then
                    EpsToDelete.Add(EpId, 0)
                End If
            Next

            If EpsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(EpsToDelete, Enums.ContentType.TVEpisode) = DialogResult.OK Then
                        FillEpisodes(Convert.ToInt64(dgvTVSeasons.Item("idShow", currRow_TVSeason).Value), Convert.ToInt32(dgvTVSeasons.Item("Season", currRow_TVSeason).Value))
                        SetTVCount()
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

    End Sub

    Private Sub cmnuShowRemoveFromDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRemoveFromDisk.Click
        Try

            Dim ShowsToDelete As New Dictionary(Of Long, Long)
            Dim ShowId As Long = -1

            For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                ShowId = Convert.ToInt64(sRow.Cells("idShow").Value)
                If Not ShowsToDelete.ContainsKey(ShowId) Then
                    ShowsToDelete.Add(ShowId, 0)
                End If
            Next

            If ShowsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(ShowsToDelete, Enums.ContentType.TVShow) = DialogResult.OK Then
                        FillList(False, False, True)
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuEpisodeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeEdit.Click
        If dgvTVEpisodes.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim tmpDBTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
        Edit_TVEpisode(tmpDBTVEpisode)
    End Sub

    Private Sub cmnuMovieEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieEdit.Click
        If dgvMovies.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
        Dim tmpDBMovie As Database.DBElement = Master.DB.Load_Movie(ID)
        Edit_Movie(tmpDBMovie)
    End Sub

    Private Sub cmnuShowEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowEdit.Click
        If dgvTVShows.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
        Dim tmpDBMTVShow As Database.DBElement = Master.DB.Load_TVShow(ID, True, False)
        Edit_TVShow(tmpDBMTVShow)
    End Sub

    Private Sub cmnuEpisodeOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeOpenFolder.Click
        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim ePath As String = String.Empty

            If dgvTVEpisodes.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        If Not Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then
                            SQLCommand.CommandText = String.Concat("SELECT strFilename FROM files WHERE idFile = ", sRow.Cells("idFile").Value.ToString, ";")
                            ePath = SQLCommand.ExecuteScalar.ToString

                            If Not String.IsNullOrEmpty(ePath) Then
                                Using Explorer As New Diagnostics.Process

                                    If Master.isWindows Then
                                        Explorer.StartInfo.FileName = "explorer.exe"
                                        Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", ePath)
                                    Else
                                        Explorer.StartInfo.FileName = "xdg-open"
                                        Explorer.StartInfo.Arguments = String.Format("""{0}""", ePath)
                                    End If
                                    Explorer.Start()
                                End Using
                            End If
                        End If
                    Next
                End Using
            End If
        End If
    End Sub

    Private Sub cmnuMovieUnwatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUnwatched.Click
        SetWatchedState_Movie(False)
    End Sub

    Private Sub cmnuMovieWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieWatched.Click
        SetWatchedState_Movie(True)
    End Sub

    Private Sub cmnuEpisodeUnwatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeUnwatched.Click
        SetWatchedState_TVEpisode(False)
    End Sub

    Private Sub cmnuEpisodeWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeWatched.Click
        SetWatchedState_TVEpisode(True)
    End Sub

    Private Sub cmnuSeasonUnwatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonUnwatched.Click
        SetWatchedState_TVSeason(False)
    End Sub

    Private Sub cmnuHasWatchedSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonWatched.Click
        SetWatchedState_TVSeason(True)
    End Sub

    Private Sub cmnuShowUnwatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowUnwatched.Click
        SetWatchedState_TVShow(False)
    End Sub

    Private Sub cmnuShowWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowWatched.Click
        SetWatchedState_TVShow(True)
    End Sub

    Private Sub cmnuEpisodeLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeLock.Click
        Try
            Dim setLock As Boolean = False
            If dgvTVEpisodes.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "Lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idShow")
                    SQLcommand.CommandText = "UPDATE tvshow SET Lock = (?) WHERE idShow = (?);"
                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        parLock.Value = If(dgvTVEpisodes.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idEpisode").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value
                    Next
                End Using

                'now check the status of all episodes in the season so we can update the season lock flag if needed
                Dim LockCount As Integer = 0
                Dim NotLockCount As Integer = 0
                For Each sRow As DataGridViewRow In dgvTVEpisodes.Rows
                    If Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        LockCount += 1
                    Else
                        NotLockCount += 1
                    End If
                Next

                If LockCount = 0 OrElse NotLockCount = 0 Then
                    Using SQLSeacommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSeaLock As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaLock", DbType.Boolean, 0, "Lock")
                        Dim parTVShowID As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parTVShowID", DbType.Int64, 0, "idShow")
                        Dim parSeason As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeason", DbType.Int64, 0, "Season")
                        SQLSeacommand.CommandText = "UPDATE seasons SET Lock = (?) WHERE idShow = (?) AND Season = (?);"
                        If LockCount = 0 Then
                            parSeaLock.Value = False
                        ElseIf NotLockCount = 0 Then
                            parSeaLock.Value = True
                        End If
                        parTVShowID.Value = Convert.ToInt64(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value)
                        parSeason.Value = Convert.ToInt32(dgvTVSeasons.SelectedRows(0).Cells("Season").Value)
                        SQLSeacommand.ExecuteNonQuery()
                        dgvTVSeasons.SelectedRows(0).Cells("Lock").Value = parSeaLock.Value
                    End Using
                End If

                SQLtransaction.Commit()
            End Using

            dgvTVEpisodes.Invalidate()
            dgvTVSeasons.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuSeasonLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonLock.Click
        Try
            Dim setLock As Boolean = False
            If dgvTVSeasons.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idShow")
                    Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.Int64, 0, "Season")
                    SQLcommand.CommandText = "UPDATE seasons SET Lock = (?) WHERE idShow = (?) AND Season = (?);"
                    For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                        parLock.Value = If(dgvTVSeasons.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        parSeason.Value = sRow.Cells("Season").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parELock As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parELock", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int64, 0, "idShow")
                            Dim parESeason As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parESeason", DbType.Int64, 0, "Season")
                            SQLECommand.CommandText = "UPDATE episode SET Lock = (?) WHERE idShow = (?) AND Season = (?);"
                            parELock.Value = parLock.Value
                            parEID.Value = parID.Value
                            parESeason.Value = parSeason.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In dgvTVEpisodes.Rows
                                eRow.Cells("Lock").Value = parLock.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            dgvTVSeasons.Invalidate()
            dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuShowLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowLock.Click
        Try
            Dim setLock As Boolean = False
            If dgvTVShows.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idShow")
                    SQLcommand.CommandText = "UPDATE tvshow SET lock = (?) WHERE idShow = (?);"
                    For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                        parLock.Value = If(dgvTVShows.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value

                        Using SQLSeaCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSeaLock As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaLock", DbType.Boolean, 0, "lock")
                            Dim parSeaID As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaID", DbType.Int64, 0, "idShow")
                            SQLSeaCommand.CommandText = "UPDATE seasons SET lock = (?) WHERE idShow = (?);"
                            parSeaLock.Value = parLock.Value
                            parSeaID.Value = parID.Value
                            SQLSeaCommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In dgvTVSeasons.Rows
                                eRow.Cells("Lock").Value = parLock.Value
                            Next
                        End Using

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parELock As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parELock", DbType.Boolean, 0, "lock")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int64, 0, "idShow")
                            SQLECommand.CommandText = "UPDATE episode SET lock = (?) WHERE idShow = (?);"
                            parELock.Value = parLock.Value
                            parEID.Value = parID.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In dgvTVEpisodes.Rows
                                eRow.Cells("Lock").Value = parLock.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If chkFilterLock_Shows.Checked Then
                dgvTVShows.ClearSelection()
                dgvTVShows.CurrentCell = Nothing
                If dgvTVShows.RowCount <= 0 Then
                    ClearInfo()
                    dgvTVSeasons.DataSource = Nothing
                    dgvTVEpisodes.DataSource = Nothing
                End If
            End If

            dgvTVShows.Invalidate()
            dgvTVSeasons.Invalidate()
            dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuMovieLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieLock.Click
        Try
            Dim setLock As Boolean = False
            If dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idMovie")
                    SQLcommand.CommandText = "UPDATE movie SET lock = (?) WHERE idMovie = (?);"
                    For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                        parLock.Value = If(dgvMovies.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idMovie").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If chkFilterLock_Movies.Checked Then
                dgvMovies.ClearSelection()
                dgvMovies.CurrentCell = Nothing
                If dgvMovies.RowCount <= 0 Then ClearInfo()
            End If

            dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuMovieSetLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetLock.Click
        Try
            Dim setLock As Boolean = False
            If dgvMovieSets.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idSet")
                    SQLcommand.CommandText = "UPDATE sets SET Lock = (?) WHERE idSet = (?);"
                    For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                        parLock.Value = If(dgvMovieSets.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idSet").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If chkFilterLock_MovieSets.Checked Then
                dgvMovieSets.ClearSelection()
                dgvMovieSets.CurrentCell = Nothing
                If dgvMovieSets.RowCount <= 0 Then ClearInfo()
            End If

            dgvMovieSets.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuEpisodeMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeMark.Click
        Try
            Dim setMark As Boolean = False
            If dgvTVEpisodes.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idEpisode")
                    SQLcommand.CommandText = "UPDATE episode SET mark = (?) WHERE idEpisode = (?);"
                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        parMark.Value = If(dgvTVEpisodes.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                        parID.Value = sRow.Cells("idEpisode").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Mark").Value = parMark.Value
                    Next
                End Using

                'now check the status of all episodes in the season so we can update the season mark flag if needed
                Dim MarkCount As Integer = 0
                Dim NotMarkCount As Integer = 0
                For Each sRow As DataGridViewRow In dgvTVEpisodes.Rows
                    If Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        MarkCount += 1
                    Else
                        NotMarkCount += 1
                    End If
                Next

                If MarkCount = 0 OrElse NotMarkCount = 0 Then
                    Using SQLSeacommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSeaMark As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "Mark")
                        Dim parTVShowID As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parTVShowID", DbType.Int64, 0, "idShow")
                        Dim parSeason As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeason", DbType.Int64, 0, "Season")
                        SQLSeacommand.CommandText = "UPDATE seasons SET Mark = (?) WHERE idShow = (?) AND Season = (?);"
                        If MarkCount = 0 Then
                            parSeaMark.Value = False
                        ElseIf NotMarkCount = 0 Then
                            parSeaMark.Value = True
                        End If
                        parTVShowID.Value = Convert.ToInt64(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value)
                        parSeason.Value = Convert.ToInt32(dgvTVSeasons.SelectedRows(0).Cells("Season").Value)
                        SQLSeacommand.ExecuteNonQuery()
                        dgvTVSeasons.SelectedRows(0).Cells("Mark").Value = parSeaMark.Value
                    End Using
                End If

                SQLtransaction.Commit()
            End Using

            dgvTVSeasons.Invalidate()
            dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuSeasonMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonMark.Click
        Try
            Dim setMark As Boolean = False
            If dgvTVSeasons.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idShow")
                    Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.Int64, 0, "Season")
                    SQLcommand.CommandText = "UPDATE seasons SET mark = (?) WHERE idShow = (?) AND Season = (?);"
                    For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                        parMark.Value = If(dgvTVSeasons.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        parSeason.Value = sRow.Cells("Season").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Mark").Value = parMark.Value

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int64, 0, "idShow")
                            Dim parESeason As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parESeason", DbType.Int64, 0, "Season")
                            SQLECommand.CommandText = "UPDATE episode SET mark = (?) WHERE idShow = (?) AND Season = (?);"
                            parEMark.Value = parMark.Value
                            parEID.Value = parID.Value
                            parESeason.Value = parSeason.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In dgvTVEpisodes.Rows
                                eRow.Cells("Mark").Value = parMark.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            dgvTVSeasons.Invalidate()
            dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuShowMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowMark.Click
        Try
            Dim setMark As Boolean = False
            If dgvTVShows.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idShow")
                    SQLcommand.CommandText = "UPDATE tvshow SET mark = (?) WHERE idShow = (?);"
                    For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                        parMark.Value = If(dgvTVShows.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Mark").Value = parMark.Value

                        Using SQLSeaCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSeaMark As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "mark")
                            Dim parSeaID As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaID", DbType.Int64, 0, "idShow")
                            SQLSeaCommand.CommandText = "UPDATE seasons SET mark = (?) WHERE idShow = (?);"
                            parSeaMark.Value = parMark.Value
                            parSeaID.Value = parID.Value
                            SQLSeaCommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In dgvTVSeasons.Rows
                                eRow.Cells("Mark").Value = parMark.Value
                            Next
                        End Using

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int64, 0, "idShow")
                            SQLECommand.CommandText = "UPDATE episode SET mark = (?) WHERE idShow = (?);"
                            parEMark.Value = parMark.Value
                            parEID.Value = parID.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In dgvTVEpisodes.Rows
                                eRow.Cells("Mark").Value = parMark.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If chkFilterMark_Shows.Checked Then
                dgvTVShows.ClearSelection()
                dgvTVShows.CurrentCell = Nothing
                If dgvTVShows.RowCount <= 0 Then
                    ClearInfo()
                    dgvTVSeasons.DataSource = Nothing
                    dgvTVEpisodes.DataSource = Nothing
                End If
            End If

            dgvTVShows.Invalidate()
            dgvTVSeasons.Invalidate()
            dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cmnuMovieMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMark.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET Mark = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("Mark").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        setMark = False
        For Each sRow As DataGridViewRow In dgvMovies.Rows
            If Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                setMark = True
                Exit For
            End If
        Next
        btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If chkFilterMark_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieSetMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetMark.Click
        Dim setMark As Boolean = False
        If dgvMovieSets.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idSet")
                SQLcommand.CommandText = "UPDATE sets SET Mark = (?) WHERE idSet = (?);"
                For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                    parMark.Value = If(dgvMovieSets.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                    parID.Value = sRow.Cells("idSet").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("Mark").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        setMark = False
        For Each sRow As DataGridViewRow In dgvMovieSets.Rows
            If Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                setMark = True
                Exit For
            End If
        Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If chkFilterMark_MovieSets.Checked Then
            dgvMovieSets.ClearSelection()
            dgvMovieSets.CurrentCell = Nothing
            If dgvMovieSets.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovieSets.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom1.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom1").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom1", DbType.Boolean, 0, "MarkCustom1")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom1 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom1").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom1").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If chkFilterMarkCustom1_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom2.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom2").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom2", DbType.Boolean, 0, "MarkCustom2")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom2 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom2").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom2").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If chkFilterMarkCustom2_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom3.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom3").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom3", DbType.Boolean, 0, "MarkCustom3")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom3 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom3").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom3").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If chkFilterMarkCustom3_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom4.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom4").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom4", DbType.Boolean, 0, "MarkCustom4")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom4 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom4").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom4").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If chkFilterMarkCustom4_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieEditMetaData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieEditMetaData.Click
        If dgvMovies.SelectedRows.Count > 1 Then Return
        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
        Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
        Using dEditMeta As New dlgFileInfo(DBElement, False)
            Select Case dEditMeta.ShowDialog()
                Case DialogResult.OK
                    RefreshRow_Movie(ID)
            End Select
        End Using
    End Sub

    Private Sub cmnuMovieReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReload.Click
        dgvMovies.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        Dim showMessages As Boolean = dgvMovies.SelectedRows.Count = 1

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                If Reload_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value), True, showMessages) Then
                    doFill = True
                Else
                    RefreshRow_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                End If
            Next
            SQLtransaction.Commit()
        End Using

        dgvMovies.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then
            FillList(True, True, False)
        Else
            DoTitleCheck()
        End If
    End Sub

    Private Sub cmnuMovieSetEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetEdit.Click
        If dgvMovieSets.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
        Dim tmpDBMovieSet As Database.DBElement = Master.DB.Load_MovieSet(ID)
        Edit_MovieSet(tmpDBMovieSet)
    End Sub

    Private Sub cmnuMovieSetNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetNew.Click
        dgvMovieSets.ClearSelection()

        Dim tmpDBMovieSet = New Database.DBElement(Enums.ContentType.MovieSet) With {.MovieSet = New MediaContainers.MovieSet}

        Using dNewSet As New dlgNewSet()
            If dNewSet.ShowDialog(tmpDBMovieSet) = DialogResult.OK Then
                tmpDBMovieSet = Master.DB.Save_MovieSet(dNewSet.Result, False, False)
                FillList(False, True, False)
                Edit_MovieSet(tmpDBMovieSet)
            End If
        End Using

    End Sub

    Private Sub cmnuMovieSetReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetReload.Click
        dgvMovieSets.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False
        Dim tFill As Boolean = False

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                tFill = Reload_MovieSet(Convert.ToInt64(sRow.Cells("idSet").Value), True)
                If tFill Then doFill = True
            Next
            SQLtransaction.Commit()
        End Using

        dgvMovieSets.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then FillList(False, True, False)
    End Sub

    Private Sub cmnuMovieSetRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetRemove.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idSet").Value))
        Next


        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tID As Long In lItemsToRemove
                Master.DB.Delete_MovieSet(tID, True)
                RemoveRow_MovieSet(tID)
            Next
            SQLtransaction.Commit()
        End Using

        FillList(True, False, False)
    End Sub

    Private Sub cmnuEpisodeReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeReload.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    If Not Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then 'skipping missing episodes
                        If Reload_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value), True, dgvTVEpisodes.SelectedRows.Count = 1) Then
                            doFill = True
                        Else
                            RefreshRow_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value))
                        End If
                    End If
                Next
                SQLtransaction.Commit()
            End Using
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then FillEpisodes(Convert.ToInt64(dgvTVEpisodes.SelectedRows(0).Cells("idEpisode").Value), Convert.ToInt32(dgvTVEpisodes.SelectedRows(0).Cells("Season").Value))
    End Sub

    Private Sub cmnuSeasonReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonReload.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVSeasons.SelectedRows.Count > 0 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVSeasons.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(563, "Reloading Season"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value), True, dgvTVSeasons.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        RefreshRow_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then FillSeasons(Convert.ToInt64(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value))
    End Sub

    Private Sub cmnuSeasonReloadFull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonReloadFull.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVSeasons.SelectedRows.Count > 0 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVSeasons.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(563, "Reloading Season"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value), True, dgvTVSeasons.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        RefreshRow_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then FillSeasons(Convert.ToInt64(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value))
    End Sub

    Private Sub cmnuShowReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowReload.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVShows.SelectedRows.Count > 1 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVShows.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(562, "Reloading Show"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, dgvTVShows.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        RefreshRow_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        ElseIf dgvTVShows.SelectedRows.Count = 1 Then
            If Reload_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value), False, True, False) Then
                doFill = True
            Else
                RefreshRow_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value))
            End If
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then FillList(False, False, True)
    End Sub

    Private Sub cmnuShowReloadFull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowReloadFull.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVShows.SelectedRows.Count > 1 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVShows.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(562, "Reloading Show"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, dgvTVShows.SelectedRows.Count = 1, True) Then
                        doFill = True
                    Else
                        RefreshRow_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        ElseIf dgvTVShows.SelectedRows.Count = 1 Then
            If Reload_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value), False, True, True) Then
                doFill = True
            Else
                RefreshRow_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value))
            End If
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then FillList(False, False, True)
    End Sub

    Private Sub cmnuSeasonRemoveFromDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonRemoveFromDB.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idSeason").Value))
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim idShow As Integer = CInt(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value)
            For Each tID As Long In lItemsToRemove
                If Not tID = 999 Then
                    Master.DB.Delete_TVSeason(tID, True)
                    RemoveRow_TVSeason(tID)
                End If
            Next
            Reload_TVShow(idShow, True, True, False) 'TODO: check if needed
            SQLtransaction.Commit()
        End Using

        SetTVCount()
    End Sub

    Private Sub cmnuEpisodeRemoveFromDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRemoveFromDB.Click
        Dim lItemsToRemove As New Dictionary(Of Long, Boolean)
        Dim SeasonsList As New List(Of Integer)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
            If Not SeasonsList.Contains(CInt(sRow.Cells("Season").Value)) Then SeasonsList.Add(CInt(sRow.Cells("Season").Value))
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idEpisode").Value), Convert.ToInt64(sRow.Cells("idFile").Value) = -1)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim idShow As Integer = CInt(dgvTVEpisodes.SelectedRows(0).Cells("idShow").Value)
            For Each tID As KeyValuePair(Of Long, Boolean) In lItemsToRemove
                If tID.Value Then
                    Master.DB.Delete_TVEpisode(tID.Key, True, False, True) 'remove the "missing episode" from DB
                    RemoveRow_TVEpisode(tID.Key)
                Else
                    Master.DB.Delete_TVEpisode(tID.Key, False, False, True) 'set the episode as "missing episode"
                    RefreshRow_TVEpisode(tID.Key)
                End If
            Next

            For Each iSeason In SeasonsList
                RefreshRow_TVSeason(idShow, iSeason)
            Next
            RefreshRow_TVShow(idShow)

            SQLtransaction.Commit()
        End Using

        SetTVCount()
    End Sub

    Private Sub cmnuShowRemoveFromDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRemoveFromDB.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idShow").Value))
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tID As Long In lItemsToRemove
                Master.DB.Delete_TVShow(tID, True)
                RemoveRow_TVShow(tID)
            Next
            SQLtransaction.Commit()
        End Using

        SetTVCount()
    End Sub

    Private Sub cmnuEpisodeRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeScrape.Click
        If dgvTVEpisodes.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            CreateScrapeList_TVEpisode(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifiers)
        End If
    End Sub

    Private Sub cmnuShowRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowScrapeRefreshData.Click
        'Me.SetControlsEnabled(False, True)
        'RefreshData_TVShow()
    End Sub

    Private Sub cmnuMovieRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieScrape.Click
        If dgvMovies.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifiers)
        End If
    End Sub

    Private Sub cmnuMovieSetRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetScrape.Click
        If dgvMovieSets.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            CreateScrapeList_MovieSet(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_MovieSet, ScrapeModifiers)
        End If
    End Sub

    Private Sub cmnuShowRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowScrape.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)
            CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifiers)
        End If
    End Sub
    ''' <summary>
    ''' User has selected "Change Movie" from the context menu. This will re-validate the movie title with the user,
    ''' and initiate a new manually scrape of the movie.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmnuMovieChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieChange.Click
        If dgvMovies.SelectedRows.Count <> 1 Then Return 'This method is only valid for when exactly one movie is selected
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifiers)
    End Sub
    ''' <summary>
    ''' User has selected "Change Movie" from the context menu. This will re-validate the movie title with the user,
    ''' and initiate a new auto scrape of the movie.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmnuMovieChangeAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieChangeAuto.Click
        If dgvMovies.SelectedRows.Count <> 1 Then Return 'This method is only valid for when exactly one movie is selected
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleAuto, Master.DefaultOptions_Movie, ScrapeModifiers)
    End Sub

    Private Sub cmnuSeasonEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonEdit.Click
        Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
        Dim tmpDBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)
        Edit_TVSeason(tmpDBTVSeason)
    End Sub

    Private Sub cmnuSeasonOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonOpenFolder.Click
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim SeasonPath As String = String.Empty

            If dgvTVSeasons.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    SeasonPath = Functions.GetSeasonDirectoryFromShowPath(currTV.ShowPath, Convert.ToInt32(sRow.Cells("Season").Value))

                    Using Explorer As New Process
                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            If String.IsNullOrEmpty(SeasonPath) Then
                                Explorer.StartInfo.Arguments = String.Format("/root,""{0}""", currTV.ShowPath)
                            Else
                                Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", SeasonPath)
                            End If

                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            If String.IsNullOrEmpty(SeasonPath) Then
                                Explorer.StartInfo.Arguments = String.Format("""{0}""", currTV.ShowPath)
                            Else
                                Explorer.StartInfo.Arguments = String.Format("""{0}""", SeasonPath)
                            End If
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub cmnuSeasonRescrape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonScrape.Click
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            CreateScrapeList_TVSeason(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifiers)
        End If
    End Sub

    Private Sub mnuMainToolsSortFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsSortFiles.Click, cmnuTrayToolsSortFiles.Click
        SetControlsEnabled(False)
        Using dSortFiles As New dlgSortFiles
            If dSortFiles.ShowDialog() = DialogResult.OK Then
                LoadMedia(New Structures.ScanOrClean With {.Movies = True})
            Else
                SetControlsEnabled(True)
            End If
        End Using
    End Sub

    Private Sub mnuMainToolsBackdrops_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsBackdrops.Click, cmnuTrayToolsBackdrops.Click
        fTaskManager.AddTask(New TaskManager.TaskItem With {.ContentType = Enums.ContentType.Movie, .TaskType = Enums.TaskManagerType.CopyBackdrops})
    End Sub
    ''' <summary>
    ''' Populate the form's Genre panel and picture box arrays with the 
    ''' appropriate genre images and (conditionally) labels 
    ''' </summary>
    ''' <param name="genres"><c>List (Of String)</c> holding genre names</param>
    ''' <remarks>If any individual genre is invalid or generates an error, 
    ''' the remaining genres are still processed, however the placement/spacing
    ''' of the remaining genres may show gaps where the erronious genres should have been</remarks>
    Private Sub createGenreThumbs(ByVal genres As List(Of String))
        If ((genres Is Nothing) OrElse (genres.Count = 0)) Then Return

        For i As Integer = 0 To genres.Count - 1
            Try
                ReDim Preserve pnlGenre(i)
                ReDim Preserve pbGenre(i)
                pnlGenre(i) = New Panel()
                pnlGenre(i).Visible = False
                pbGenre(i) = New PictureBox()
                pbGenre(i).Name = genres(i).Trim.ToUpper
                pnlGenre(i).Size = New Size(68, 100)
                pbGenre(i).Size = New Size(62, 94)
                pnlGenre(i).BackColor = GenrePanelColor
                pbGenre(i).BackColor = GenrePanelColor
                pnlGenre(i).BorderStyle = BorderStyle.FixedSingle
                pbGenre(i).SizeMode = PictureBoxSizeMode.StretchImage
                pbGenre(i).Image = APIXML.GetGenreImage(genres(i).Trim)
                pnlGenre(i).Left = ((pnlInfoPanel.Right) - (i * 73)) - 73
                pbGenre(i).Left = 2
                pnlGenre(i).Top = pnlInfoPanel.Top - 105
                pbGenre(i).Top = 2
                scMain.Panel2.Controls.Add(pnlGenre(i))
                pnlGenre(i).Controls.Add(pbGenre(i))
                pnlGenre(i).BringToFront()
                AddHandler pbGenre(i).MouseEnter, AddressOf pbGenre_MouseEnter
                AddHandler pbGenre(i).MouseLeave, AddressOf pbGenre_MouseLeave
                If Master.eSettings.GeneralShowGenresText Then
                    pbGenre(i).Image = ImageUtils.AddGenreString(pbGenre(i).Image, pbGenre(i).Name)
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
    End Sub


    Private Sub cmnuMovieRemoveFromDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRemoveFromDisk.Click
        Dim MoviesToDelete As New Dictionary(Of Long, Long)
        Dim MovieId As Int64 = -1

        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
            MovieId = Convert.ToInt64(sRow.Cells("idMovie").Value)
            If Not MoviesToDelete.ContainsKey(MovieId) Then
                MoviesToDelete.Add(MovieId, 0)
            End If
        Next

        If MoviesToDelete.Count > 0 Then
            Using dlg As New dlgDeleteConfirm
                If dlg.ShowDialog(MoviesToDelete, Enums.ContentType.Movie) = DialogResult.OK Then
                    FillList(True, True, False)
                End If
            End Using
        End If
    End Sub

    Private Sub dgvMovies_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = dgvMovies.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "ListTitle" OrElse colName = "Playcount" OrElse Not Master.eSettings.MovieClickScrape Then
            If Not colName = "Playcount" Then
                If dgvMovies.SelectedRows.Count > 0 Then
                    If dgvMovies.RowCount > 0 Then
                        If dgvMovies.SelectedRows.Count > 1 Then
                            SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvMovies.SelectedRows.Count))
                        ElseIf dgvMovies.SelectedRows.Count = 1 Then
                            SetStatus(dgvMovies.SelectedRows(0).Cells("MoviePath").Value.ToString)
                        End If
                    End If
                    currRow_Movie = dgvMovies.SelectedRows(0).Index
                End If
            Else
                SetWatchedState_Movie(If(Not String.IsNullOrEmpty(dgvMovies.Rows(e.RowIndex).Cells("Playcount").Value.ToString) AndAlso
                                      Not dgvMovies.Rows(e.RowIndex).Cells("Playcount").Value.ToString = "0", False, True))
            End If

        ElseIf Master.eSettings.MovieClickScrape AndAlso colName = "HasSet" AndAlso Not bwMovieScraper.IsBusy Then
            Dim objCell As DataGridViewCell = CType(dgvMovies.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            dgvMovies.ClearSelection()
            dgvMovies.Rows(objCell.RowIndex).Selected = True
            currRow_Movie = objCell.RowIndex

            Dim scrapeOptions As New Structures.ScrapeOptions
            scrapeOptions.bMainCollectionID = True
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
            CreateScrapeList_Movie(Enums.ScrapeType.SingleField, scrapeOptions, ScrapeModifiers)

        ElseIf Master.eSettings.MovieClickScrape AndAlso
            (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse
            colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse
            colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse
            colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath") AndAlso
            Not bwMovieScraper.IsBusy Then
            Dim objCell As DataGridViewCell = CType(dgvMovies.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            dgvMovies.ClearSelection()
            dgvMovies.Rows(objCell.RowIndex).Selected = True
            currRow_Movie = objCell.RowIndex

            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                Case "ClearArtPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                Case "ClearLogoPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                Case "DiscArtPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
                Case "EFanartsPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrafanarts, True)
                Case "EThumbsPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrathumbs, True)
                Case "FanartPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                Case "NfoPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                Case "ThemePath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTheme, True)
                Case "TrailerPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTrailer, True)
                Case "HasSub"
                    'Functions.SetScraperMod(Enums.ModType.Subtitles, True)
                Case "MetaData" 'Metadata - need to add this column to the view.
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainMeta, True)
            End Select
            If Master.eSettings.MovieClickScrapeAsk Then
                CreateScrapeList_Movie(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_Movie, ScrapeModifiers)
            Else
                CreateScrapeList_Movie(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_Movie, ScrapeModifiers)
            End If
        End If
    End Sub

    Private Sub dgvMovies_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If fScanner.IsBusy OrElse bwLoadMovieInfo.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwRewrite_Movies.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
        Dim tmpDBMovie As Database.DBElement = Master.DB.Load_Movie(ID)
        Edit_Movie(tmpDBMovie)
    End Sub

    Private Sub dgvMovies_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellEnter
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currMainTabTag.ContentType = Enums.ContentType.Movie Then
            Debug.WriteLine("[dgvMovies_CellEnter] Return")
            Return
        End If

        tmrWait_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_Movie.Stop()
        tmrLoad_TVShow.Stop()
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrLoad_MovieSet.Stop()
        Debug.WriteLine("[dgvMovies_CellEnter] tmrLoad_Movie.Stop()")
        tmrLoad_Movie.Stop()

        currRow_Movie = e.RowIndex
        Debug.WriteLine("[dgvMovies_CellEnter] tmrWait_Movie.Start()")
        tmrWait_Movie.Start()
    End Sub

    Private Sub dgvMovies_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellMouseEnter
        Dim colName As String = dgvMovies.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        dgvMovies.ShowCellToolTips = True

        If colName = "Playcount" AndAlso e.RowIndex >= 0 Then
            oldStatus = GetStatus()
            SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse
            colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse
            colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse
            colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath" OrElse
            colName = "HasSet" OrElse colName = "HasSub") AndAlso e.RowIndex >= 0 Then
            dgvMovies.ShowCellToolTips = False

            If Master.eSettings.MovieClickScrape AndAlso Not bwMovieScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim movieTitle As String = dgvMovies.Rows(e.RowIndex).Cells("Title").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "ClearArtPath"
                        scrapeFor = Master.eLang.GetString(1122, "ClearArt Only")
                    Case "ClearLogoPath"
                        scrapeFor = Master.eLang.GetString(1123, "ClearLogo Only")
                    Case "DiscArtPath"
                        scrapeFor = Master.eLang.GetString(1124, "DiscArt Only")
                    Case "EFanartsPath"
                        scrapeFor = Master.eLang.GetString(975, "Extrafanarts Only")
                    Case "EThumbsPath"
                        scrapeFor = Master.eLang.GetString(74, "Extrathumbs Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "MetaData"
                        scrapeFor = Master.eLang.GetString(76, "Meta Data Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                    Case "ThemePath"
                        scrapeFor = Master.eLang.GetString(1125, "Theme Only")
                    Case "TrailerPath"
                        scrapeFor = Master.eLang.GetString(75, "Trailer Only")
                    Case "HasSet"
                        scrapeFor = Master.eLang.GetString(1354, "MovieSet Informations Only")
                    Case "HasSub"
                        scrapeFor = Master.eLang.GetString(1355, "Subtitles Only")
                End Select

                If Master.eSettings.MovieClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", movieTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvMovies_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then SetStatus(oldStatus)
    End Sub

    Private Sub dgvMovies_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovies.CellPainting
        Dim colName As String = dgvMovies.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvMovies.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse
            colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse
            colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse
            colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath" OrElse
            colName = "HasSet" OrElse colName = "HasSub" OrElse colName = "Playcount") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "ClearArtPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 4)
            ElseIf colName = "ClearLogoPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 5)
            ElseIf colName = "DiscArtPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 6)
            ElseIf colName = "EFanartsPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 7)
            ElseIf colName = "EThumbsPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 8)
            ElseIf colName = "FanartPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "NfoPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "ThemePath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 15)
            ElseIf colName = "TrailerPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 16)
            ElseIf colName = "HasSet" Then
                ilColumnIcons.Draw(e.Graphics, pt, 13)
            ElseIf colName = "HasSub" Then
                ilColumnIcons.Draw(e.Graphics, pt, 14)
            ElseIf colName = "Playcount" Then
                ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "Imdb" OrElse colName = "ListTitle" OrElse colName = "MPAA" OrElse colName = "OriginalTitle" OrElse
            colName = "Rating" OrElse colName = "TMDB" OrElse colName = "Year") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(dgvMovies.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(dgvMovies.Item("New", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            ElseIf Convert.ToBoolean(dgvMovies.Item("MarkCustom1", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
            ElseIf Convert.ToBoolean(dgvMovies.Item("MarkCustom2", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
            ElseIf Convert.ToBoolean(dgvMovies.Item("MarkCustom3", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
            ElseIf Convert.ToBoolean(dgvMovies.Item("MarkCustom4", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 2 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(dgvMovies.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            ElseIf Convert.ToBoolean(dgvMovies.Item("OutOfTolerance", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.MistyRose
                e.CellStyle.SelectionBackColor = Color.DarkMagenta
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse
                colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse
                colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse
                colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'playcount field
            If colName = "Playcount" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString) AndAlso Not e.Value.ToString = "0", 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasSet" OrElse colName = "HasSub" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvMovies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovies.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchMovies.Focus()
    End Sub

    Private Sub dgvMovies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvMovies.KeyPress
        Try
            If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
                KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
                tmrKeyBuffer.Start()
                For Each drvRow As DataGridViewRow In dgvMovies.Rows
                    If drvRow.Cells("ListTitle").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                        drvRow.Selected = True
                        dgvMovies.CurrentCell = drvRow.Cells("ListTitle")
                        Exit For
                    End If
                Next
            ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
                If fScanner.IsBusy OrElse bwLoadMovieInfo.IsBusy OrElse
                bwDownloadPic.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy _
                OrElse bwCleanDB.IsBusy OrElse bwRewrite_Movies.IsBusy Then Return

                SetStatus(currMovie.Filename)

                If dgvMovies.SelectedRows.Count > 1 Then Return

                Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                Dim tmpDBMovie As Database.DBElement = Master.DB.Load_Movie(ID)
                Edit_Movie(tmpDBMovie)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub dgvMovies_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvMovies.MouseDown
        If e.Button = MouseButtons.Right And dgvMovies.RowCount > 0 Then
            If bwCleanDB.IsBusy OrElse bwMovieScraper.IsBusy Then
                cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                Return
            End If

            cmnuMovie.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvMovies.HitTest(e.X, e.Y)

            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                If dgvMovies.SelectedRows.Count > 1 AndAlso dgvMovies.Rows(dgvHTI.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False
                    Dim bEnableUnwatched As Boolean = False
                    Dim bEnableWatched As Boolean = False

                    cmnuMovie.Enabled = True
                    cmnuMovieChange.Visible = False
                    cmnuMovieChangeAuto.Visible = False
                    cmnuMovieEdit.Visible = False
                    cmnuMovieEditMetaData.Visible = False
                    cmnuMovieScrape.Visible = False

                    For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                        End If
                        'if any one item is set as unwatched, enable menu "Mark as Watched"
                        'if any one item is set as watched, enable menu "Mark as Unwatched"
                        If String.IsNullOrEmpty(sRow.Cells("Playcount").Value.ToString) OrElse sRow.Cells("Playcount").Value.ToString = "0" Then
                            bEnableWatched = True
                            If setLock AndAlso setMark AndAlso bEnableUnwatched Then Exit For
                        Else
                            bEnableUnwatched = True
                            If setLock AndAlso setMark AndAlso bEnableWatched Then Exit For
                        End If
                    Next

                    cmnuMovieMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    cmnuMovieLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                    cmnuMovieTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    'Watched / Unwatched menu
                    cmnuMovieUnwatched.Enabled = bEnableUnwatched
                    cmnuMovieUnwatched.Visible = bEnableUnwatched
                    cmnuMovieWatched.Enabled = bEnableWatched
                    cmnuMovieWatched.Visible = bEnableWatched

                    'Language submenu
                    mnuLanguagesLanguage.Tag = String.Empty
                    If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                        mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                    End If
                    mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    mnuLanguagesSet.Enabled = False

                    'Genre submenu
                    mnuGenresGenre.Tag = String.Empty
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Tag submenu
                    mnuTagsTag.Tag = String.Empty
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False
                Else
                    cmnuMovieChange.Visible = True
                    cmnuMovieChangeAuto.Visible = True
                    cmnuMovieEdit.Visible = True
                    cmnuMovieEditMetaData.Visible = True
                    cmnuMovieScrape.Visible = True

                    cmnuMovieMark.Text = If(Convert.ToBoolean(dgvMovies.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    cmnuMovieLock.Text = If(Convert.ToBoolean(dgvMovies.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                    cmnuMovieTitle.Text = String.Concat(">> ", dgvMovies.Item("Title", dgvHTI.RowIndex).Value, " <<")

                    'Watched / Unwatched menu
                    Dim bIsWatched As Boolean = Not String.IsNullOrEmpty(dgvMovies.Item("Playcount", dgvHTI.RowIndex).Value.ToString) AndAlso Not dgvMovies.Item("Playcount", dgvHTI.RowIndex).Value.ToString = "0"
                    cmnuMovieUnwatched.Enabled = bIsWatched
                    cmnuMovieUnwatched.Visible = bIsWatched
                    cmnuMovieWatched.Enabled = Not bIsWatched
                    cmnuMovieWatched.Visible = Not bIsWatched

                    If Not dgvMovies.Rows(dgvHTI.RowIndex).Selected Then
                        prevRow_Movie = -1
                        dgvMovies.CurrentCell = Nothing
                        dgvMovies.ClearSelection()
                        dgvMovies.Rows(dgvHTI.RowIndex).Selected = True
                        dgvMovies.CurrentCell = dgvMovies.Item("ListTitle", dgvHTI.RowIndex)
                        'cmnuMovie.Enabled = True
                    Else
                        cmnuMovie.Enabled = True
                    End If

                    'Language submenu
                    Dim strLang As String = dgvMovies.Item("Language", dgvHTI.RowIndex).Value.ToString
                    Dim Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = strLang)
                    If Language IsNot Nothing AndAlso Not String.IsNullOrEmpty(Language.Description) Then
                        mnuLanguagesLanguage.SelectedItem = Language.Description
                    Else
                        If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                            mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                        End If
                        mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    End If
                    mnuLanguagesSet.Enabled = False

                    'Genre submenu
                    mnuGenresGenre.Tag = dgvMovies.Item("Genre", dgvHTI.RowIndex).Value
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Tag submenu
                    mnuTagsTag.Tag = dgvMovies.Item("Tag", dgvHTI.RowIndex).Value
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False
                End If
            Else
                cmnuMovie.Enabled = False
                cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvMovies_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovies.Resize
        ResizeMoviesList()
    End Sub

    Private Sub dgvMovies_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvMovies.RowsRemoved
        SetMovieCount()
    End Sub

    Private Sub dgvMovies_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvMovies.RowsAdded
        SetMovieCount()
    End Sub

    Private Sub dgvMovies_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovies.Sorted
        prevRow_Movie = -1
        If dgvMovies.RowCount > 0 Then
            dgvMovies.CurrentCell = Nothing
            dgvMovies.ClearSelection()
            dgvMovies.Rows(0).Selected = True
            dgvMovies.CurrentCell = dgvMovies.Rows(0).Cells("ListTitle")
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortYear_Movies.Tag = "ASC"
            btnFilterSortYear_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortYear_Movies.Tag = "DESC"
            btnFilterSortYear_Movies.Image = My.Resources.desc
        Else
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortRating_Movies.Tag = "ASC"
            btnFilterSortRating_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortRating_Movies.Tag = "DESC"
            btnFilterSortRating_Movies.Image = My.Resources.desc
        Else
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortTitle_Movies.Tag = "ASC"
            btnFilterSortTitle_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortTitle_Movies.Tag = "DESC"
            btnFilterSortTitle_Movies.Image = My.Resources.desc
        Else
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortDateAdded_Movies.Tag = "ASC"
            btnFilterSortDateAdded_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortDateAdded_Movies.Tag = "DESC"
            btnFilterSortDateAdded_Movies.Image = My.Resources.desc
        Else
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortDateModified_Movies.Tag = "ASC"
            btnFilterSortDateModified_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortDateModified_Movies.Tag = "DESC"
            btnFilterSortDateModified_Movies.Image = My.Resources.desc
        Else
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
        End If

        SaveFilter_Movies()
    End Sub

    Private Sub dgvMovieSets_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = dgvMovieSets.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "ListTitle" OrElse Not Master.eSettings.MovieSetClickScrape Then
            If dgvMovieSets.SelectedRows.Count > 0 Then
                If dgvMovieSets.RowCount > 0 Then
                    If dgvMovieSets.SelectedRows.Count > 1 Then
                        SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvMovieSets.SelectedRows.Count))
                    ElseIf dgvMovieSets.SelectedRows.Count = 1 Then
                        SetStatus(dgvMovieSets.SelectedRows(0).Cells("SetName").Value.ToString)
                    End If
                End If
                currRow_MovieSet = dgvMovieSets.SelectedRows(0).Index
            End If

        ElseIf Master.eSettings.MovieSetClickScrape AndAlso
            (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse colName = "DiscArtPath" OrElse
             colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath") AndAlso Not bwMovieSetScraper.IsBusy Then
            Dim objCell As DataGridViewCell = CType(dgvMovieSets.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            dgvMovieSets.ClearSelection()
            dgvMovieSets.Rows(objCell.RowIndex).Selected = True
            currRow_MovieSet = objCell.RowIndex
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                Case "ClearArtPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                Case "ClearLogoPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                Case "DiscArtPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
                Case "FanartPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                Case "NfoPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
            End Select
            If Master.eSettings.MovieSetClickScrapeAsk Then
                CreateScrapeList_MovieSet(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_MovieSet, ScrapeModifiers)
            Else
                CreateScrapeList_MovieSet(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_MovieSet, ScrapeModifiers)
            End If
        End If
    End Sub

    Private Sub dgvMovieSets_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If fScanner.IsBusy OrElse bwLoadMovieSetInfo.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
        Dim tmpDBMovieSet As Database.DBElement = Master.DB.Load_MovieSet(ID)
        Edit_MovieSet(tmpDBMovieSet)
    End Sub

    Private Sub dgvMovieSets_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellEnter
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currMainTabTag.ContentType = Enums.ContentType.MovieSet Then Return

        tmrWait_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrLoad_TVShow.Stop()
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        Debug.WriteLine("[dgvMovieSets_CellEnter] tmrLoad_Movie.Stop()")
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()

        currRow_MovieSet = e.RowIndex
        tmrWait_MovieSet.Start()
    End Sub

    Private Sub dgvMovieSets_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellMouseEnter
        Dim colName As String = dgvMovieSets.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        dgvMovieSets.ShowCellToolTips = True

        If (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse colName = "DiscArtPath" OrElse
             colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath") AndAlso e.RowIndex >= 0 Then
            dgvMovieSets.ShowCellToolTips = False

            If Master.eSettings.MovieSetClickScrape AndAlso Not bwMovieSetScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim movieSetName As String = dgvMovieSets.Rows(e.RowIndex).Cells("SetName").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "ClearArtPath"
                        scrapeFor = Master.eLang.GetString(1122, "ClearArt Only")
                    Case "ClearLogoPath"
                        scrapeFor = Master.eLang.GetString(1123, "ClearLogo Only")
                    Case "DiscArtPath"
                        scrapeFor = Master.eLang.GetString(1124, "DiscArt Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                End Select
                If Master.eSettings.MovieSetClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If
                SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", movieSetName, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvMovieSets_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then SetStatus(oldStatus)
    End Sub

    Private Sub dgvMovieSets_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovieSets.CellPainting
        Dim colName As String = dgvMovieSets.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvMovieSets.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse
            colName = "DiscArtPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse
            colName = "NfoPath" OrElse colName = "PosterPath") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "ClearArtPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 4)
            ElseIf colName = "ClearLogoPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 5)
            ElseIf colName = "DiscArtPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 6)
            ElseIf colName = "FanartPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "NfoPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 12)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "ListTitle") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(dgvMovieSets.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(dgvMovieSets.Item("New", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 1 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(dgvMovieSets.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse
                colName = "DiscArtPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse
                colName = "NfoPath" OrElse colName = "PosterPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvMovieSets_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovieSets.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchMovieSets.Focus()
    End Sub

    Private Sub dgvMovieSets_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvMovieSets.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In dgvMovieSets.Rows
                If drvRow.Cells("ListTitle").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    dgvMovieSets.CurrentCell = drvRow.Cells("ListTitle")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If fScanner.IsBusy OrElse bwLoadMovieSetInfo.IsBusy OrElse
            bwLoadMovieSetPosters.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
            bwCleanDB.IsBusy Then Return

            Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
            Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
            currMovieSet = Master.DB.Load_MovieSet(ID)
            SetStatus(currMovieSet.ListTitle)
            Dim tmpDBMovieSet As Database.DBElement = Master.DB.Load_MovieSet(ID)
            Edit_MovieSet(tmpDBMovieSet)
        End If
    End Sub

    Private Sub dgvMovieSets_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvMovieSets.MouseDown
        If e.Button = MouseButtons.Right And dgvMovieSets.RowCount > 0 Then
            If bwCleanDB.IsBusy OrElse bwMovieSetScraper.IsBusy Then
                cmnuMovieSetTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                Return
            End If

            cmnuMovieSet.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvMovieSets.HitTest(e.X, e.Y)

            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                If dgvMovieSets.SelectedRows.Count > 1 AndAlso dgvMovieSets.Rows(dgvHTI.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False

                    cmnuMovieSet.Enabled = True
                    cmnuMovieSetBrowseTMDB.Visible = True
                    cmnuMovieSetDatabaseSeparator.Visible = True
                    cmnuMovieSetEdit.Visible = False
                    cmnuMovieSetEditSeparator.Visible = True
                    cmnuMovieSetEditSortMethod.Visible = True
                    cmnuMovieSetLanguage.Visible = True
                    cmnuMovieSetLock.Visible = True
                    cmnuMovieSetMark.Visible = True
                    cmnuMovieSetNew.Visible = True
                    cmnuMovieSetNewSeparator.Visible = True
                    cmnuMovieSetReload.Visible = True
                    cmnuMovieSetRemove.Visible = True
                    cmnuMovieSetRemoveSeparator.Visible = True
                    cmnuMovieSetScrape.Visible = False
                    cmnuMovieSetScrapeSelected.Visible = True
                    cmnuMovieSetScrapeSeparator.Visible = True
                    cmnuMovieSetScrapeSingleDataField.Visible = True
                    cmnuMovieSetSep3.Visible = True
                    cmnuMovieSetTitle.Visible = True

                    For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark Then Exit For
                        End If
                        ''if any one item is set as unwatched, set menu to watched
                        ''else they are all watched so set menu to not watched
                        'If Not Convert.ToBoolean(sRow.Cells("HasWatched").Value) Then
                        '    setWatched = True
                        '    If setLock AndAlso setMark Then Exit For
                        'End If
                    Next

                    cmnuMovieSetMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    cmnuMovieSetLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                    cmnuMovieSetTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    cmnuMovieSetEditSortMethodMethods.SelectedIndex = -1
                    cmnuMovieSetEditSortMethodSet.Enabled = False

                    'Language submenu
                    mnuLanguagesLanguage.Tag = String.Empty
                    If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                        mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                    End If
                    mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    mnuLanguagesSet.Enabled = False
                Else
                    cmnuMovieSetBrowseTMDB.Visible = True
                    cmnuMovieSetDatabaseSeparator.Visible = True
                    cmnuMovieSetEdit.Visible = True
                    cmnuMovieSetEditSeparator.Visible = True
                    cmnuMovieSetEditSortMethod.Visible = True
                    cmnuMovieSetLanguage.Visible = True
                    cmnuMovieSetLock.Visible = True
                    cmnuMovieSetMark.Visible = True
                    cmnuMovieSetNew.Visible = True
                    cmnuMovieSetNewSeparator.Visible = True
                    cmnuMovieSetReload.Visible = True
                    cmnuMovieSetRemove.Visible = True
                    cmnuMovieSetRemoveSeparator.Visible = True
                    cmnuMovieSetScrape.Visible = True
                    cmnuMovieSetScrapeSelected.Visible = True
                    cmnuMovieSetScrapeSeparator.Visible = True
                    cmnuMovieSetScrapeSingleDataField.Visible = True
                    cmnuMovieSetSep3.Visible = True
                    cmnuMovieSetTitle.Visible = True

                    cmnuMovieSetMark.Text = If(Convert.ToBoolean(dgvMovieSets.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    cmnuMovieSetLock.Text = If(Convert.ToBoolean(dgvMovieSets.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                    cmnuMovieSetTitle.Text = String.Concat(">> ", dgvMovieSets.Item("SetName", dgvHTI.RowIndex).Value, " <<")

                    If Not dgvMovieSets.Rows(dgvHTI.RowIndex).Selected Then
                        prevRow_MovieSet = -1
                        dgvMovieSets.CurrentCell = Nothing
                        dgvMovieSets.ClearSelection()
                        dgvMovieSets.Rows(dgvHTI.RowIndex).Selected = True
                        dgvMovieSets.CurrentCell = dgvMovieSets.Item("ListTitle", dgvHTI.RowIndex)
                        'cmnuMovieSet.Enabled = True
                    Else
                        cmnuMovieSet.Enabled = True
                    End If

                    Dim SortMethod As Integer = CInt(dgvMovieSets.Item("SortMethod", dgvHTI.RowIndex).Value)
                    cmnuMovieSetEditSortMethodMethods.Text = DirectCast(CInt(dgvMovieSets.Item("SortMethod", dgvHTI.RowIndex).Value), Enums.SortMethod_MovieSet).ToString
                    cmnuMovieSetEditSortMethodSet.Enabled = False

                    'Language submenu
                    Dim strLang As String = dgvMovieSets.Item("Language", dgvHTI.RowIndex).Value.ToString
                    Dim Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = strLang)
                    If Language IsNot Nothing AndAlso Not String.IsNullOrEmpty(Language.Description) Then
                        mnuLanguagesLanguage.SelectedItem = Language.Description
                    Else
                        If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                            mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                        End If
                        mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    End If
                    mnuLanguagesSet.Enabled = False
                End If
            Else
                cmnuMovieSet.Enabled = True
                cmnuMovieSetBrowseTMDB.Visible = False
                cmnuMovieSetDatabaseSeparator.Visible = False
                cmnuMovieSetEdit.Visible = False
                cmnuMovieSetEditSeparator.Visible = False
                cmnuMovieSetEditSortMethod.Visible = False
                cmnuMovieSetLanguage.Visible = False
                cmnuMovieSetLock.Visible = False
                cmnuMovieSetMark.Visible = False
                cmnuMovieSetNew.Visible = True
                cmnuMovieSetNewSeparator.Visible = False
                cmnuMovieSetReload.Visible = False
                cmnuMovieSetRemove.Visible = False
                cmnuMovieSetRemoveSeparator.Visible = False
                cmnuMovieSetScrape.Visible = False
                cmnuMovieSetScrapeSelected.Visible = False
                cmnuMovieSetScrapeSeparator.Visible = False
                cmnuMovieSetScrapeSingleDataField.Visible = False
                cmnuMovieSetSep3.Visible = False
                cmnuMovieSetTitle.Visible = False
            End If
        End If
    End Sub

    Private Sub dgvMovieSets_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSets.Resize
        ResizeMovieSetsList()
    End Sub

    Private Sub dgvMovieSets_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvMovieSets.RowsRemoved
        SetMovieSetCount()
    End Sub

    Private Sub dgvMovieSets_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvMovieSets.RowsAdded
        SetMovieSetCount()
    End Sub

    Private Sub dgvMovieSets_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSets.Sorted
        prevRow_MovieSet = -1
        If dgvMovieSets.RowCount > 0 Then
            dgvMovieSets.CurrentCell = Nothing
            dgvMovieSets.ClearSelection()
            dgvMovieSets.Rows(0).Selected = True
            dgvMovieSets.CurrentCell = dgvMovieSets.Rows(0).Cells("ListTitle")
        End If

        SaveFilter_MovieSets()
    End Sub

    Private Sub dgvTVEpisodes_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = dgvTVEpisodes.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "Title" OrElse colName = "Playcount" OrElse Not Master.eSettings.TVGeneralClickScrape Then
            If Not colName = "Playcount" Then
                If dgvTVEpisodes.SelectedRows.Count > 0 Then
                    If dgvTVEpisodes.RowCount > 0 Then
                        If dgvTVEpisodes.SelectedRows.Count > 1 Then
                            SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVEpisodes.SelectedRows.Count))
                        ElseIf dgvTVEpisodes.SelectedRows.Count = 1 Then
                            SetStatus(dgvTVEpisodes.SelectedRows(0).Cells("Title").Value.ToString)
                        End If
                    End If
                    currRow_TVEpisode = dgvTVEpisodes.SelectedRows(0).Index
                    If Not currList = 2 Then
                        currList = 2
                        prevRow_TVEpisode = -1
                        SelectRow_TVEpisode(dgvTVEpisodes.SelectedRows(0).Index)
                    End If
                End If
            Else
                SetWatchedState_TVEpisode(If(Not String.IsNullOrEmpty(dgvTVEpisodes.Rows(e.RowIndex).Cells("Playcount").Value.ToString) AndAlso
                                      Not dgvTVEpisodes.Rows(e.RowIndex).Cells("Playcount").Value.ToString = "0", False, True))
            End If

        ElseIf Master.eSettings.TVGeneralClickScrape AndAlso
            (colName = "FanartPath" OrElse colName = "NfoPath" OrElse colName = "PosterPath") AndAlso
            Not bwTVEpisodeScraper.IsBusy Then
            Dim objCell As DataGridViewCell = CType(dgvTVEpisodes.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            dgvTVEpisodes.ClearSelection()
            dgvTVEpisodes.Rows(objCell.RowIndex).Selected = True
            currRow_TVEpisode = objCell.RowIndex

            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Select Case colName
                Case "FanartPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeFanart, True)
                Case "NfoPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodePoster, True)
                Case "HasSub"
                    'Functions.SetScraperMod(Enums.ModType.Subtitles, True)
                Case "MetaData" 'Metadata - need to add this column to the view.
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeMeta, True)
            End Select
            If Master.eSettings.TVGeneralClickScrapeAsk Then
                CreateScrapeList_TVEpisode(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_TV, ScrapeModifiers)
            Else
                CreateScrapeList_TVEpisode(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_TV, ScrapeModifiers)
            End If
        End If
    End Sub

    Private Sub dgvTVEpisodes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If fScanner.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwReload_MovieSets.IsBusy _
            OrElse bwRewrite_Movies.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim tmpDBTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
        Edit_TVEpisode(tmpDBTVEpisode)
    End Sub

    Private Sub dgvTVEpisodes_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellEnter
        Dim currTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currTag.ContentType = Enums.ContentType.TV OrElse Not currList = 2 Then Return

        tmrWait_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_TVEpisode.Stop()
        tmrLoad_TVShow.Stop()
        tmrLoad_TVSeason.Stop()
        Debug.WriteLine("[dgvTVEpisodes_CellEnter] tmrLoad_Movie.Stop()")
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()
        tmrLoad_TVEpisode.Stop()

        currRow_TVEpisode = e.RowIndex
        tmrWait_TVEpisode.Start()
    End Sub

    Private Sub dgvTVEpisodes_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellMouseEnter
        Dim colName As String = dgvTVEpisodes.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        dgvTVEpisodes.ShowCellToolTips = True

        If colName = "Playcount" AndAlso e.RowIndex >= 0 Then
            oldStatus = GetStatus()
            SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "FanartPath" OrElse colName = "NfoPath" OrElse
            colName = "PosterPath" OrElse colName = "HasSub") AndAlso e.RowIndex >= 0 Then
            dgvTVEpisodes.ShowCellToolTips = False

            If Master.eSettings.TVGeneralClickScrape AndAlso Not bwTVEpisodeScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim episodeTitle As String = dgvTVEpisodes.Rows(e.RowIndex).Cells("Title").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "MetaData"
                        scrapeFor = Master.eLang.GetString(76, "Meta Data Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                    Case "HasSub"
                        scrapeFor = Master.eLang.GetString(1355, "Subtitles Only")
                End Select

                If Master.eSettings.TVGeneralClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", episodeTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvTVEpisodes_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then SetStatus(oldStatus)
    End Sub

    Private Sub dgvTVEpisodes_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVEpisodes.CellPainting
        Dim colName As String = dgvTVEpisodes.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvTVEpisodes.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "FanartPath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse
            colName = "HasSub" OrElse colName = "Playcount") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "FanartPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "NfoPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "HasSub" Then
                ilColumnIcons.Draw(e.Graphics, pt, 14)
            ElseIf colName = "Playcount" Then
                ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "Aired" OrElse colName = "Episode" OrElse colName = "Season" OrElse
            colName = "Title") AndAlso e.RowIndex >= 0 Then
            If Convert.ToInt64(dgvTVEpisodes.Item("idFile", e.RowIndex).Value) = -1 Then
                e.CellStyle.ForeColor = Color.Gray
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.LightGray
            ElseIf Convert.ToBoolean(dgvTVEpisodes.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(dgvTVEpisodes.Item("New", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 2 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToInt64(dgvTVEpisodes.Item("idFile", e.RowIndex).Value) = -1 Then
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.DarkGray
            ElseIf Convert.ToBoolean(dgvTVEpisodes.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "FanartPath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'playcount field
            If colName = "Playcount" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString) AndAlso Not e.Value.ToString = "0", 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasSub" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvTVEpisodes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVEpisodes.KeyDown
        'stop enter key from selecting next list item
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub dgvTVEpisodes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVEpisodes.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In dgvTVEpisodes.Rows
                If drvRow.Cells("Title").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    dgvTVEpisodes.CurrentCell = drvRow.Cells("Title")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If fScanner.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

            Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
            Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
            Dim tmpDBTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
            Edit_TVEpisode(tmpDBTVEpisode)
        End If
    End Sub

    Private Sub ShowEpisodeMenuItems(ByVal Visible As Boolean)
        Dim cMnu As ToolStripMenuItem
        Dim cSep As ToolStripSeparator

        If Visible Then
            For Each cMnuItem As Object In cmnuEpisode.Items
                If TypeOf cMnuItem Is ToolStripMenuItem Then
                    DirectCast(cMnuItem, ToolStripMenuItem).Visible = True
                ElseIf TypeOf cMnuItem Is ToolStripSeparator Then
                    DirectCast(cMnuItem, ToolStripSeparator).Visible = True
                End If
            Next
            cmnuEpisodeRemoveFromDisk.Visible = True
        Else
            For Each cMnuItem As Object In cmnuEpisode.Items
                If TypeOf cMnuItem Is ToolStripMenuItem Then
                    cMnu = DirectCast(cMnuItem, ToolStripMenuItem)
                    If Not cMnu.Name = "RemoveEpToolStripMenuItem" AndAlso Not cMnu.Name = "cmnuEpTitle" Then
                        cMnu.Visible = False
                    End If
                ElseIf TypeOf cMnuItem Is ToolStripSeparator Then
                    cSep = DirectCast(cMnuItem, ToolStripSeparator)
                    If Not cSep.Name = "ToolStripSeparator6" Then
                        cSep.Visible = False
                    End If
                End If
                cmnuEpisodeRemoveFromDisk.Visible = False
            Next
        End If
    End Sub

    Private Sub dgvTVEpisodes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVEpisodes.MouseDown
        Dim hasMissing As Boolean = False

        If e.Button = MouseButtons.Right And dgvTVEpisodes.RowCount > 0 Then
            cmnuEpisode.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVEpisodes.HitTest(e.X, e.Y)
            If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                If dgvTVEpisodes.SelectedRows.Count > 1 AndAlso dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected Then
                    cmnuEpisode.Enabled = True

                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        If Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then
                            hasMissing = True
                            Exit For
                        End If
                    Next

                    cmnuEpisodeTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    If hasMissing Then
                        ShowEpisodeMenuItems(False)
                    Else
                        Dim setMark As Boolean = False
                        Dim setLock As Boolean = False
                        Dim bEnableUnwatched As Boolean = False
                        Dim bEnableWatched As Boolean = False

                        ShowEpisodeMenuItems(True)

                        cmnuEpisodeEditSeparator.Visible = False
                        cmnuEpisodeEdit.Visible = False
                        cmnuEpisodeScrapeSeparator.Visible = False
                        cmnuEpisodeScrape.Visible = False
                        cmnuEpisodeChange.Visible = False
                        cmnuEpisodeSep3.Visible = False
                        cmnuEpisodeOpenFolder.Visible = False

                        For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                            'if any one item is set as unmarked, set menu to mark
                            'else they are all marked, so set menu to unmark
                            If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                                setMark = True
                                If setLock AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                            End If
                            'if any one item is set as unlocked, set menu to lock
                            'else they are all locked so set menu to unlock
                            If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                                setLock = True
                                If setMark AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                            End If
                            'if any one item is set as unwatched, enable menu "Mark as Watched"
                            'if any one item is set as watched, enable menu "Mark as Unwatched"
                            If String.IsNullOrEmpty(sRow.Cells("Playcount").Value.ToString) OrElse sRow.Cells("Playcount").Value.ToString = "0" Then
                                bEnableWatched = True
                                If setLock AndAlso setMark AndAlso bEnableUnwatched Then Exit For
                            Else
                                bEnableUnwatched = True
                                If setLock AndAlso setMark AndAlso bEnableWatched Then Exit For
                            End If
                        Next

                        cmnuEpisodeMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                        cmnuEpisodeLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))

                        'Watched / Unwatched menu
                        cmnuEpisodeUnwatched.Enabled = bEnableUnwatched
                        cmnuEpisodeUnwatched.Visible = bEnableUnwatched
                        cmnuEpisodeWatched.Enabled = bEnableWatched
                        cmnuEpisodeWatched.Visible = bEnableWatched
                    End If
                Else
                    cmnuEpisodeTitle.Text = String.Concat(">> ", dgvTVEpisodes.Item("Title", dgvHTI.RowIndex).Value, " <<")

                    If Not dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected OrElse Not currList = 2 Then
                        prevRow_TVEpisode = -1
                        currList = 2
                        dgvTVEpisodes.CurrentCell = Nothing
                        dgvTVEpisodes.ClearSelection()
                        dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected = True
                        dgvTVEpisodes.CurrentCell = dgvTVEpisodes.Item("Title", dgvHTI.RowIndex)
                    Else
                        cmnuEpisode.Enabled = True
                    End If

                    If Convert.ToInt64(dgvTVEpisodes.Item("idFile", dgvHTI.RowIndex).Value) = -1 Then hasMissing = True

                    If hasMissing Then
                        ShowEpisodeMenuItems(False)
                    Else
                        ShowEpisodeMenuItems(True)

                        cmnuEpisodeEditSeparator.Visible = True
                        cmnuEpisodeEdit.Visible = True
                        cmnuEpisodeScrapeSeparator.Visible = True
                        cmnuEpisodeScrape.Visible = True
                        cmnuEpisodeChange.Visible = True
                        cmnuEpisodeSep3.Visible = True
                        cmnuEpisodeOpenFolder.Visible = True

                        cmnuEpisodeMark.Text = If(Convert.ToBoolean(dgvTVEpisodes.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                        cmnuEpisodeLock.Text = If(Convert.ToBoolean(dgvTVEpisodes.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))

                        'Watched / Unwatched menu
                        Dim bIsWatched As Boolean = Not String.IsNullOrEmpty(dgvTVEpisodes.Item("Playcount", dgvHTI.RowIndex).Value.ToString) AndAlso Not dgvTVEpisodes.Item("Playcount", dgvHTI.RowIndex).Value.ToString = "0"
                        cmnuEpisodeUnwatched.Enabled = bIsWatched
                        cmnuEpisodeUnwatched.Visible = bIsWatched
                        cmnuEpisodeWatched.Enabled = Not bIsWatched
                        cmnuEpisodeWatched.Visible = Not bIsWatched
                    End If

                End If
            Else
                cmnuEpisode.Enabled = False
                cmnuEpisodeTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvTVEpisodes_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVEpisodes.Resize
        ResizeTVLists(3)
    End Sub

    Private Sub dgvTVEpisodes_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVEpisodes.Sorted
        prevRow_TVEpisode = -1
        If dgvTVEpisodes.RowCount > 0 Then
            dgvTVEpisodes.CurrentCell = Nothing
            dgvTVEpisodes.ClearSelection()
            dgvTVEpisodes.Rows(0).Selected = True
            dgvTVEpisodes.CurrentCell = dgvTVEpisodes.Rows(0).Cells("Title")
        End If
    End Sub

    Private Sub dgvTVSeasons_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = dgvTVSeasons.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "SeasonText" OrElse colName = "HasWatched" OrElse Not Master.eSettings.TVGeneralClickScrape Then
            If Not colName = "HasWatched" Then
                If dgvTVSeasons.SelectedRows.Count > 0 Then
                    If dgvTVSeasons.RowCount > 0 Then
                        If dgvTVSeasons.SelectedRows.Count > 1 Then
                            SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVSeasons.SelectedRows.Count))
                        ElseIf dgvTVSeasons.SelectedRows.Count = 1 Then
                            SetStatus(dgvTVSeasons.SelectedRows(0).Cells("SeasonText").Value.ToString)
                        End If
                    End If
                    currRow_TVSeason = dgvTVSeasons.SelectedRows(0).Index
                    If Not currList = 1 Then
                        currList = 1
                        prevRow_TVSeason = -1
                        SelectRow_TVSeason(dgvTVSeasons.SelectedRows(0).Index)
                    End If
                End If
            Else
                If Not CInt(dgvTVSeasons.Rows(e.RowIndex).Cells("Season").Value) = 999 Then
                    SetWatchedState_TVSeason(If(CBool(dgvTVSeasons.Rows(e.RowIndex).Cells("HasWatched").Value), False, True))
                End If
            End If

        ElseIf Master.eSettings.TVGeneralClickScrape AndAlso
            (colName = "BannerPath" OrElse colName = "FanartPath" OrElse
             colName = "LandscapePath" OrElse colName = "PosterPath") AndAlso
            Not bwTVSeasonScraper.IsBusy Then
            Dim objCell As DataGridViewCell = CType(dgvTVSeasons.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            dgvTVSeasons.ClearSelection()
            dgvTVSeasons.Rows(objCell.RowIndex).Selected = True
            currRow_TVSeason = objCell.RowIndex

            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonBanner, True)
                Case "FanartPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonLandscape, True)
                Case "PosterPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonPoster, True)
            End Select
            If Master.eSettings.TVGeneralClickScrapeAsk Then
                CreateScrapeList_TVSeason(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_TV, ScrapeModifiers)
            Else
                CreateScrapeList_TVSeason(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_TV, ScrapeModifiers)
            End If
        End If
    End Sub

    Private Sub dgvTVSeasons_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If fScanner.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

        Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
        Dim tmpDBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)
        Edit_TVSeason(tmpDBTVSeason)
    End Sub

    Private Sub dgvTVSeasons_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellEnter
        Dim currTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currTag.ContentType = Enums.ContentType.TV OrElse Not currList = 1 Then Return

        tmrWait_TVShow.Stop()
        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_TVSeason.Stop()
        tmrLoad_TVShow.Stop()
        Debug.WriteLine("[dgvTVSeasons_CellEnter] tmrLoad_Movie.Stop()")
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrLoad_TVSeason.Stop()

        currRow_TVSeason = e.RowIndex
        tmrWait_TVSeason.Start()
    End Sub

    Private Sub dgvTVSeasons_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellMouseEnter
        Dim colName As String = dgvTVSeasons.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        dgvTVSeasons.ShowCellToolTips = True

        If colName = "HasWatched" AndAlso e.RowIndex >= 0 AndAlso Not CInt(dgvTVSeasons.Rows(e.RowIndex).Cells("Season").Value) = 999 Then
            oldStatus = GetStatus()
            SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "BannerPath" OrElse colName = "FanartPath" OrElse
            colName = "LandscapePath" OrElse colName = "PosterPath") AndAlso e.RowIndex >= 0 Then
            dgvTVSeasons.ShowCellToolTips = False

            If Master.eSettings.TVGeneralClickScrape AndAlso Not bwTVSeasonScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim seasonTitle As String = dgvTVSeasons.Rows(e.RowIndex).Cells("SeasonText").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                End Select

                If Master.eSettings.TVGeneralClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", seasonTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvTVSeasons_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then SetStatus(oldStatus)
    End Sub

    Private Sub dgvTVSeasons_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVSeasons.CellPainting
        Dim colName As String = dgvTVSeasons.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvTVSeasons.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse
            colName = "PosterPath" OrElse colName = "HasWatched") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "FanartPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "PosterPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "HasWatched" Then
                ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "SeasonText" OrElse colName = "Episodes") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(dgvTVSeasons.Item("Missing", e.RowIndex).Value) AndAlso Not CInt(dgvTVSeasons.Item("Season", e.RowIndex).Value) = 999 Then
                e.CellStyle.ForeColor = Color.Gray
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.LightGray
            ElseIf Convert.ToBoolean(dgvTVSeasons.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(dgvTVSeasons.Item("New", e.RowIndex).Value) OrElse
                Not String.IsNullOrEmpty(dgvTVSeasons.Item("NewEpisodes", e.RowIndex).Value.ToString) AndAlso CInt(dgvTVSeasons.Item("NewEpisodes", e.RowIndex).Value) > 0 Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 3 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(dgvTVSeasons.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse
                colName = "NfoPath" OrElse colName = "PosterPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasWatched" AndAlso Not CInt(dgvTVSeasons.Item("Season", e.RowIndex).Value) = 999 Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvTVSeasons_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVSeasons.KeyDown
        'stop enter key from selecting next list item
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub dgvTVSeasons_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVSeasons.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In dgvTVSeasons.Rows
                If drvRow.Cells("SeasonText").Value.ToString.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    dgvTVSeasons.CurrentCell = drvRow.Cells("SeasonText")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If fScanner.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

            Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
            Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
            Dim tmpDBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)
            Edit_TVSeason(tmpDBTVSeason)
        End If
    End Sub

    Private Sub dgvTVSeasons_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVSeasons.MouseDown
        If e.Button = MouseButtons.Right And dgvTVSeasons.RowCount > 0 Then

            cmnuSeason.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVSeasons.HitTest(e.X, e.Y)
            If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                If dgvTVSeasons.SelectedRows.Count > 1 AndAlso dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False
                    Dim bEnableUnwatched As Boolean = False
                    Dim bEnableWatched As Boolean = False

                    cmnuSeason.Enabled = True
                    cmnuSeasonEdit.Visible = False
                    cmnuSeasonEditSeparator.Visible = False
                    cmnuSeasonScrape.Visible = False

                    For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                        End If
                        'if any one item is set as unwatched, enable menu "Mark as Watched"
                        'if any one item is set as watched, enable menu "Mark as Unwatched"
                        If Not CInt(sRow.Cells("Season").Value) = 999 AndAlso Not Convert.ToBoolean(sRow.Cells("HasWatched").Value) Then
                            bEnableWatched = True
                            If setLock AndAlso setMark AndAlso bEnableUnwatched Then Exit For
                        Else
                            bEnableUnwatched = True
                            If setLock AndAlso setMark AndAlso bEnableWatched Then Exit For
                        End If
                    Next

                    cmnuSeasonMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    cmnuSeasonLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                    cmnuSeasonTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    'Watched / Unwatched menu
                    cmnuSeasonUnwatched.Enabled = bEnableUnwatched
                    cmnuSeasonUnwatched.Visible = bEnableUnwatched
                    cmnuSeasonWatched.Enabled = bEnableWatched
                    cmnuSeasonWatched.Visible = bEnableWatched

                Else
                    cmnuSeasonEdit.Visible = True
                    cmnuSeasonEditSeparator.Visible = True
                    cmnuSeasonScrape.Visible = True

                    cmnuSeasonMark.Text = If(Convert.ToBoolean(dgvTVSeasons.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    cmnuSeasonLock.Text = If(Convert.ToBoolean(dgvTVSeasons.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                    cmnuSeasonTitle.Text = String.Concat(">> ", dgvTVSeasons.Item("SeasonText", dgvHTI.RowIndex).Value, " <<")
                    cmnuSeasonEdit.Enabled = Convert.ToInt32(dgvTVSeasons.Item("Season", dgvHTI.RowIndex).Value) >= 0

                    'Watched / Unwatched menu
                    Dim bIsWatched As Boolean = Convert.ToBoolean(dgvTVShows.Item("HasWatched", dgvHTI.RowIndex).Value)
                    Dim bIsAllSeasons As Boolean = CInt(dgvTVSeasons.Item("Season", dgvHTI.RowIndex).Value) = 999
                    cmnuSeasonUnwatched.Enabled = bIsWatched AndAlso Not bIsAllSeasons
                    cmnuSeasonUnwatched.Visible = bIsWatched AndAlso Not bIsAllSeasons
                    cmnuSeasonWatched.Enabled = Not bIsWatched AndAlso Not bIsAllSeasons
                    cmnuSeasonWatched.Visible = Not bIsWatched AndAlso Not bIsAllSeasons

                    If Not dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected OrElse Not currList = 1 Then
                        prevRow_TVSeason = -1
                        currList = 1
                        dgvTVSeasons.CurrentCell = Nothing
                        dgvTVSeasons.ClearSelection()
                        dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected = True
                        dgvTVSeasons.CurrentCell = dgvTVSeasons.Item("SeasonText", dgvHTI.RowIndex)
                    Else
                        cmnuSeason.Enabled = True
                    End If
                End If
            Else
                cmnuSeason.Enabled = False
                cmnuSeasonTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvTVSeasons_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVSeasons.Sorted
        prevRow_TVSeason = -1
        If dgvTVSeasons.RowCount > 0 Then
            dgvTVSeasons.CurrentCell = Nothing
            dgvTVSeasons.ClearSelection()
            dgvTVSeasons.Rows(0).Selected = True
            dgvTVSeasons.CurrentCell = dgvTVSeasons.Rows(0).Cells("SeasonText")
        End If
    End Sub

    Private Sub dgvTVSeason_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVSeasons.Resize
        ResizeTVLists(2)
    End Sub

    Private Sub dgvTVShows_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = dgvTVShows.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "ListTitle" OrElse colName = "HasWatched" OrElse Not Master.eSettings.TVGeneralClickScrape Then
            If Not colName = "HasWatched" Then
                If dgvTVShows.SelectedRows.Count > 0 Then
                    If dgvTVShows.RowCount > 0 Then
                        If dgvTVShows.SelectedRows.Count > 1 Then
                            SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVShows.SelectedRows.Count))
                        ElseIf dgvTVShows.SelectedRows.Count = 1 Then
                            SetStatus(dgvTVShows.SelectedRows(0).Cells("TVShowPath").Value.ToString)
                        End If
                    End If

                    currRow_TVShow = dgvTVShows.SelectedRows(0).Index
                    If Not currList = 0 Then
                        currList = 0
                        prevRow_TVShow = -1
                        SelectRow_TVShow(dgvTVShows.SelectedRows(0).Index)
                    End If
                End If
            Else
                SetWatchedState_TVShow(If(CBool(dgvTVShows.Rows(e.RowIndex).Cells("HasWatched").Value), False, True))
            End If

        ElseIf Master.eSettings.TVGeneralClickScrape AndAlso
            (colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse
            colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse
            colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse
            colName = "ThemePath") AndAlso Not bwTVScraper.IsBusy Then
            Dim objCell As DataGridViewCell = CType(dgvTVShows.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            dgvTVShows.ClearSelection()
            dgvTVShows.Rows(objCell.RowIndex).Selected = True
            currRow_TVShow = objCell.RowIndex

            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                Case "CharacterArtPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainCharacterArt, True)
                Case "ClearArtPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                Case "ClearLogoPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                Case "EFanartsPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrafanarts, True)
                Case "FanartPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                Case "NfoPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                Case "ThemePath"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTheme, True)
            End Select
            If Master.eSettings.TVGeneralClickScrapeAsk Then
                CreateScrapeList_TV(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_TV, ScrapeModifiers)
            Else
                CreateScrapeList_TV(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_TV, ScrapeModifiers)
            End If
        End If
    End Sub

    Private Sub dgvTVShows_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If fScanner.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
        Dim tmpDBTVShow As Database.DBElement = Master.DB.Load_TVShow(ID, True, False)
        Edit_TVShow(tmpDBTVShow)
    End Sub

    Private Sub dgvTVShows_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellEnter
        Dim currTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currTag.ContentType = Enums.ContentType.TV OrElse Not currList = 0 Then Return

        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_TVShow.Stop()
        Debug.WriteLine("[dgvTVShows_CellEnter] tmrLoad_Movie.Stop()")
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrLoad_TVShow.Stop()

        currRow_TVShow = e.RowIndex
        tmrWait_TVShow.Start()
    End Sub

    Private Sub dgvTVShows_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellMouseEnter
        Dim colName As String = dgvTVShows.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        dgvTVShows.ShowCellToolTips = True

        If colName = "HasWatched" AndAlso e.RowIndex >= 0 Then
            oldStatus = GetStatus()
            SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse
            colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse
            colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse
            colName = "ThemePath") AndAlso e.RowIndex >= 0 Then
            dgvTVShows.ShowCellToolTips = False

            If Master.eSettings.TVGeneralClickScrape AndAlso Not bwTVScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim tvshowTitle As String = dgvTVShows.Rows(e.RowIndex).Cells("Title").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "CharacterArtPath"
                        scrapeFor = Master.eLang.GetString(1121, "CharacterArt Only")
                    Case "ClearArtPath"
                        scrapeFor = Master.eLang.GetString(1122, "ClearArt Only")
                    Case "ClearLogoPath"
                        scrapeFor = Master.eLang.GetString(1123, "ClearLogo Only")
                    Case "EFanartsPath"
                        scrapeFor = Master.eLang.GetString(975, "Extrafanarts Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                    Case "ThemePath"
                        scrapeFor = Master.eLang.GetString(1125, "Theme Only")
                End Select

                If Master.eSettings.TVGeneralClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", tvshowTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvTVShows_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then SetStatus(oldStatus)
    End Sub

    Private Sub dgvTVShows_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVShows.CellPainting
        Dim colName As String = dgvTVShows.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvTVShows.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse
            colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse
            colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse
            colName = "ThemePath" OrElse colName = "HasWatched") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "CharacterArtPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 3)
            ElseIf colName = "ClearArtPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 4)
            ElseIf colName = "ClearLogoPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 5)
            ElseIf colName = "EFanartsPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 7)
            ElseIf colName = "FanartPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "NfoPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "ThemePath" Then
                ilColumnIcons.Draw(e.Graphics, pt, 15)
            ElseIf colName = "HasWatched" Then
                ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "ListTitle" OrElse colName = "Status") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(dgvTVShows.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(dgvTVShows.Item("New", e.RowIndex).Value) OrElse
                Not String.IsNullOrEmpty(dgvTVShows.Item("NewEpisodes", e.RowIndex).Value.ToString) AndAlso CInt(dgvTVShows.Item("NewEpisodes", e.RowIndex).Value) > 0 Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 1 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(dgvTVShows.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse
                colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse
                colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse
                colName = "ThemePath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasWatched" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvTVShows_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVShows.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchShows.Focus()
    End Sub

    Private Sub dgvTVShows_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVShows.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In dgvTVShows.Rows
                If drvRow.Cells("ListTitle").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    dgvTVShows.CurrentCell = drvRow.Cells("ListTitle")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If fScanner.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwCleanDB.IsBusy Then Return

            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
            Dim tmpDBTVShow As Database.DBElement = Master.DB.Load_TVShow(ID, True, False)
            Edit_TVShow(tmpDBTVShow)
        End If
    End Sub

    Private Sub dgvTVShows_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVShows.MouseDown
        If e.Button = MouseButtons.Right And dgvTVShows.RowCount > 0 Then

            cmnuShow.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVShows.HitTest(e.X, e.Y)

            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                If dgvTVShows.SelectedRows.Count > 1 AndAlso dgvTVShows.Rows(dgvHTI.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False
                    Dim bEnableUnwatched As Boolean = False
                    Dim bEnableWatched As Boolean = False

                    cmnuShow.Enabled = True
                    cmnuShowChange.Visible = False
                    cmnuShowEdit.Visible = False
                    cmnuShowScrape.Visible = False

                    For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark AndAlso bEnableUnwatched AndAlso bEnableWatched Then Exit For
                        End If
                        'if any one item is set as unwatched, enable menu "Mark as Watched"
                        'if any one item is set as watched, enable menu "Mark as Unwatched"
                        If Not Convert.ToBoolean(sRow.Cells("HasWatched").Value) Then
                            bEnableWatched = True
                            If setLock AndAlso setMark AndAlso bEnableUnwatched Then Exit For
                        Else
                            bEnableUnwatched = True
                            If setLock AndAlso setMark AndAlso bEnableWatched Then Exit For
                        End If
                    Next

                    cmnuShowMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    cmnuShowLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                    cmnuShowTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    'Watched / Unwatched menu
                    cmnuShowUnwatched.Enabled = bEnableUnwatched
                    cmnuShowUnwatched.Visible = bEnableUnwatched
                    cmnuShowWatched.Enabled = bEnableWatched
                    cmnuShowWatched.Visible = bEnableWatched

                    'Language submenu
                    mnuLanguagesLanguage.Tag = String.Empty
                    If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                        mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                    End If
                    mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    mnuLanguagesSet.Enabled = False

                    'Genre submenu
                    mnuGenresGenre.Tag = String.Empty
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Tag submenu
                    mnuTagsTag.Tag = String.Empty
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False
                Else
                    cmnuShowChange.Visible = True
                    cmnuShowEdit.Visible = True
                    cmnuShowScrape.Visible = True

                    cmnuShowMark.Text = If(Convert.ToBoolean(dgvTVShows.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    cmnuShowLock.Text = If(Convert.ToBoolean(dgvTVShows.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                    cmnuShowTitle.Text = String.Concat(">> ", dgvTVShows.Item("Title", dgvHTI.RowIndex).Value, " <<")

                    'Watched / Unwatched menu
                    Dim bIsWatched As Boolean = Convert.ToBoolean(dgvTVShows.Item("HasWatched", dgvHTI.RowIndex).Value)
                    cmnuShowUnwatched.Enabled = bIsWatched
                    cmnuShowUnwatched.Visible = bIsWatched
                    cmnuShowWatched.Enabled = Not bIsWatched
                    cmnuShowWatched.Visible = Not bIsWatched

                    If Not dgvTVShows.Rows(dgvHTI.RowIndex).Selected OrElse Not currList = 0 Then
                        prevRow_TVShow = -1
                        currList = 0
                        dgvTVShows.CurrentCell = Nothing
                        dgvTVShows.ClearSelection()
                        dgvTVShows.Rows(dgvHTI.RowIndex).Selected = True
                        dgvTVShows.CurrentCell = dgvTVShows.Item("ListTitle", dgvHTI.RowIndex)
                        'cmnuShow.Enabled = True
                    Else
                        cmnuShow.Enabled = True
                    End If

                    'Language submenu
                    Dim strLang As String = dgvTVShows.Item("Language", dgvHTI.RowIndex).Value.ToString
                    Dim Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = strLang)
                    If Language IsNot Nothing AndAlso Not String.IsNullOrEmpty(Language.Description) Then
                        mnuLanguagesLanguage.SelectedItem = Language.Description
                    Else
                        If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                            mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                        End If
                        mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    End If
                    mnuLanguagesSet.Enabled = False

                    'Genre submenu
                    mnuGenresGenre.Tag = dgvTVShows.Item("Genre", dgvHTI.RowIndex).Value
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Tag submenu
                    mnuTagsTag.Tag = dgvTVShows.Item("Tag", dgvHTI.RowIndex).Value
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False
                End If
            Else
                cmnuShow.Enabled = False
                cmnuShowTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvTVShows_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVShows.Resize
        ResizeTVLists(1)
    End Sub

    Private Sub dgvTVShows_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvTVShows.RowsRemoved
        If dgvTVShows.RowCount = 0 OrElse dgvTVShows.SelectedRows.Count = 0 Then
            bsTVSeasons.DataSource = Nothing
            dgvTVSeasons.DataSource = Nothing
            bsTVEpisodes.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing
        End If
        SetTVCount()
    End Sub

    Private Sub dgvTVShows_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvTVShows.RowsAdded
        If dgvTVShows.RowCount = 0 OrElse dgvTVShows.SelectedRows.Count = 0 Then
            bsTVSeasons.DataSource = Nothing
            dgvTVSeasons.DataSource = Nothing
            bsTVEpisodes.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing
        End If
        SetTVCount()
    End Sub

    Private Sub dgvTVShows_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVShows.Sorted
        prevRow_TVShow = -1
        If dgvTVShows.RowCount > 0 Then
            dgvTVShows.CurrentCell = Nothing
            dgvTVShows.ClearSelection()
            dgvTVShows.Rows(0).Selected = True
            dgvTVShows.CurrentCell = dgvTVShows.Rows(0).Cells("ListTitle")
        End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortYear_Shows.Tag = "ASC"
        '    Me.btnFilterSortYear_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortYear_Shows.Tag = "DESC"
        '    Me.btnFilterSortYear_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortYear_Shows.Tag = String.Empty
        '    Me.btnFilterSortYear_Shows.Image = Nothing
        'End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortRating_Shows.Tag = "ASC"
        '    Me.btnFilterSortRating_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortRating_Shows.Tag = "DESC"
        '    Me.btnFilterSortRating_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortRating_Shows.Tag = String.Empty
        '    Me.btnFilterSortRating_Shows.Image = Nothing
        'End If

        If dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso dgvTVShows.SortOrder = 1 Then
            btnFilterSortTitle_Shows.Tag = "ASC"
            btnFilterSortTitle_Shows.Image = My.Resources.asc
        ElseIf dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso dgvTVShows.SortOrder = 2 Then
            btnFilterSortTitle_Shows.Tag = "DESC"
            btnFilterSortTitle_Shows.Image = My.Resources.desc
        Else
            btnFilterSortTitle_Shows.Tag = String.Empty
            btnFilterSortTitle_Shows.Image = Nothing
        End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortDateAdded_Shows.Tag = "ASC"
        '    Me.btnFilterSortDateAdded_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortDateAdded_Shows.Tag = "DESC"
        '    Me.btnFilterSortDateAdded_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortDateAdded_Shows.Tag = String.Empty
        '    Me.btnFilterSortDateAdded_Shows.Image = Nothing
        'End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortDateModified_Shows.Tag = "ASC"
        '    Me.btnFilterSortDateModified_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortDateModified_Shows.Tag = "DESC"
        '    Me.btnFilterSortDateModified_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortDateModified_Shows.Tag = String.Empty
        '    Me.btnFilterSortDateModified_Shows.Image = Nothing
        'End If

        SaveFilter_Shows()
    End Sub

    Private Sub DonateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainDonate.Click
        If Master.isWindows Then
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=VWVJCUV3KAUX2&lc=CH&item_name=Ember%2dTeam%3a%20DanCooper%2c%20m%2esavazzi%20%26%20Cocotus&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=VWVJCUV3KAUX2&lc=CH&item_name=Ember%2dTeam%3a%20DanCooper%2c%20m%2esavazzi%20%26%20Cocotus&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub DoTitleCheck()
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "UPDATE movie SET OutOfTolerance = (?) WHERE idMovie = (?);"
                Dim parOutOfTolerance As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOutOfTolerance", DbType.Boolean, 0, "OutOfTolerance")
                Dim par_idMovie As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                Dim LevFail As Boolean = False
                Dim bIsSingle As Boolean = False
                Dim bUseFolderName As Boolean = False
                For Each drvRow As DataGridViewRow In dgvMovies.Rows

                    bIsSingle = CBool(drvRow.Cells("Type").Value)

                    Using SQLcommand_source As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLcommand.CommandText = String.Concat("SELECT * FROM moviesource WHERE idSource = ", Convert.ToInt64(drvRow.Cells("idSource").Value), ";")
                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            If SQLreader.HasRows Then
                                SQLreader.Read()
                                bUseFolderName = Convert.ToBoolean(SQLreader("bFoldername"))
                            Else
                                bUseFolderName = False
                            End If
                        End Using
                    End Using

                    If Master.eSettings.MovieLevTolerance > 0 Then
                        LevFail = StringUtils.ComputeLevenshtein(drvRow.Cells("Title").Value.ToString, StringUtils.FilterTitleFromPath_Movie(drvRow.Cells("MoviePath").Value.ToString, bIsSingle, bUseFolderName)) > Master.eSettings.MovieLevTolerance

                        parOutOfTolerance.Value = LevFail
                        drvRow.Cells("OutOfTolerance").Value = LevFail
                        par_idMovie.Value = drvRow.Cells("idMovie").Value
                    Else
                        parOutOfTolerance.Value = False
                        drvRow.Cells("OutOfTolerance").Value = False
                        par_idMovie.Value = drvRow.Cells("idMovie").Value
                    End If
                    SQLcommand.ExecuteNonQuery()
                Next
            End Using

            SQLtransaction.Commit()
        End Using

        dgvMovies.Invalidate()
    End Sub

    Sub dtListAddRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
        dTable.Rows.Add(dRow)
    End Sub

    Sub dtListRemoveRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
        dTable.Rows.Remove(dRow)
    End Sub

    Sub dtListUpdateRow(ByVal dRow As DataRow, ByVal newRow As DataRow)
        dRow.ItemArray = newRow.ItemArray
    End Sub

    Private Sub Edit_Movie(ByRef DBMovie As Database.DBElement, Optional ByVal EventType As Enums.ModuleEventType = Enums.ModuleEventType.AfterEdit_Movie)
        SetControlsEnabled(False)
        If DBMovie.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBMovie, True) Then
            Using dEditMovie As New dlgEditMovie
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, DBMovie)
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                Select Case dEditMovie.ShowDialog(DBMovie)
                    Case DialogResult.OK
                        DBMovie = dEditMovie.Result
                        ModulesManager.Instance.RunGeneric(EventType, Nothing, Nothing, False, DBMovie)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_Movie(DBMovie, False, True, True, False)
                        RefreshRow_Movie(DBMovie.ID)
                    Case DialogResult.Retry
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifiers)
                    Case DialogResult.Abort
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifiers)
                    Case Else
                        If InfoCleared Then LoadInfo_Movie(CInt(DBMovie.ID), DBMovie.Filename, True)
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_MovieSet(ByRef DBMovieSet As Database.DBElement)
        SetControlsEnabled(False)
        'If DBMovieSet.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBMovieSet, True) Then
        Using dEditMovieSet As New dlgEditMovieSet
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_MovieSet, Nothing, Nothing, False, DBMovieSet)
            'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            Select Case dEditMovieSet.ShowDialog(DBMovieSet)
                Case DialogResult.OK
                    DBMovieSet = dEditMovieSet.Result
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_MovieSet, Nothing, Nothing, False, DBMovieSet)
                    tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                    Master.DB.Save_MovieSet(DBMovieSet, False, True)
                    RefreshRow_MovieSet(DBMovieSet.ID)
                Case DialogResult.Retry
                    Dim ScrapeModifier As New Structures.ScrapeModifiers
                    Functions.SetScrapeModifiers(ScrapeModifier, Enums.ModifierType.All, True)
                    CreateScrapeList_MovieSet(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_MovieSet, ScrapeModifier)
                Case DialogResult.Abort
                    Dim ScrapeModifier As New Structures.ScrapeModifiers
                    Functions.SetScrapeModifiers(ScrapeModifier, Enums.ModifierType.DoSearch, True)
                    Functions.SetScrapeModifiers(ScrapeModifier, Enums.ModifierType.All, True)
                    CreateScrapeList_MovieSet(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_MovieSet, ScrapeModifier)
                Case Else
                    If InfoCleared Then LoadInfo_MovieSet(CInt(DBMovieSet.ID), False)
            End Select
            'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
        End Using
        'End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVEpisode(ByRef DBTVEpisode As Database.DBElement, Optional ByVal EventType As Enums.ModuleEventType = Enums.ModuleEventType.AfterEdit_TVEpisode)
        SetControlsEnabled(False)
        If DBTVEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBTVEpisode, True) Then
            Using dEditTVEpisode As New dlgEditTVEpisode
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_TVEpisode, Nothing, Nothing, False, DBTVEpisode)
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVEpisode.GenericRunCallBack
                Select Case dEditTVEpisode.ShowDialog(DBTVEpisode)
                    Case DialogResult.OK
                        DBTVEpisode = dEditTVEpisode.Result
                        ModulesManager.Instance.RunGeneric(EventType, Nothing, Nothing, False, DBTVEpisode)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_TVEpisode(DBTVEpisode, False, True, True, True, True)
                        RefreshRow_TVEpisode(DBTVEpisode.ID)
                    Case Else
                        If InfoCleared Then LoadInfo_TVEpisode(CInt(DBTVEpisode.ID))
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVEpisode.GenericRunCallBack
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVSeason(ByRef DBTVSeason As Database.DBElement, Optional ByVal EventType As Enums.ModuleEventType = Enums.ModuleEventType.AfterEdit_TVSeason)
        SetControlsEnabled(False)
        If DBTVSeason.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVSeason, True) Then
            Using dEditTVSeason As New dlgEditTVSeason
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_TVSeason, Nothing, Nothing, False, DBTVSeason)
                'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVSeason.GenericRunCallBack
                Select Case dEditTVSeason.ShowDialog(DBTVSeason)
                    Case DialogResult.OK
                        DBTVSeason = dEditTVSeason.Result
                        ModulesManager.Instance.RunGeneric(EventType, Nothing, Nothing, False, DBTVSeason)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_TVSeason(DBTVSeason, False, True, True)
                        RefreshRow_TVSeason(DBTVSeason.ID)
                    Case Else
                        'If Me.InfoCleared Then Me.LoadInfo_TVSeason(CInt(DBTVSeason.ID)) 'TODO: 
                End Select
                'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVSeason.GenericRunCallBack
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVShow(ByRef DBTVShow As Database.DBElement, Optional ByVal EventType As Enums.ModuleEventType = Enums.ModuleEventType.AfterEdit_TVShow)
        SetControlsEnabled(False)
        If DBTVShow.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVShow, True) Then
            Using dEditTVShow As New dlgEditTVShow
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_TVShow, Nothing, Nothing, False, DBTVShow)
                'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVShow.GenericRunCallBack
                Select Case dEditTVShow.ShowDialog(DBTVShow)
                    Case DialogResult.OK
                        DBTVShow = dEditTVShow.Result
                        ModulesManager.Instance.RunGeneric(EventType, Nothing, Nothing, False, DBTVShow)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_TVShow(DBTVShow, False, True, True, True)
                        RefreshRow_TVShow(DBTVShow.ID)
                    Case DialogResult.Retry
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifiers)
                    Case DialogResult.Abort
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifiers)
                    Case Else
                        If InfoCleared Then LoadInfo_TVShow(CInt(DBTVShow.ID))
                End Select
                'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVShow.GenericRunCallBack
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub EnableFilters_Movies(ByVal isEnabled As Boolean)
        btnClearFilters_Movies.Enabled = isEnabled
        btnFilterMissing_Movies.Enabled = isEnabled
        btnFilterSortDateAdded_Movies.Enabled = isEnabled
        btnFilterSortDateModified_Movies.Enabled = isEnabled
        btnFilterSortRating_Movies.Enabled = isEnabled
        btnFilterSortTitle_Movies.Enabled = isEnabled
        btnFilterSortYear_Movies.Enabled = isEnabled
        cbFilterDataField_Movies.Enabled = isEnabled
        cbFilterVideoSource_Movies.Enabled = isEnabled
        cbFilterLists_Movies.Enabled = isEnabled
        cbFilterLists_MovieSets.Enabled = isEnabled
        cbFilterLists_Shows.Enabled = isEnabled
        cbFilterYearFrom_Movies.Enabled = isEnabled
        cbFilterYearModFrom_Movies.Enabled = isEnabled
        cbSearchMovies.Enabled = isEnabled
        chkFilterDuplicates_Movies.Enabled = isEnabled
        chkFilterLock_Movies.Enabled = isEnabled
        chkFilterMark_Movies.Enabled = isEnabled
        chkFilterMarkCustom1_Movies.Enabled = isEnabled
        chkFilterMarkCustom2_Movies.Enabled = isEnabled
        chkFilterMarkCustom3_Movies.Enabled = isEnabled
        chkFilterMarkCustom4_Movies.Enabled = isEnabled
        chkFilterMissing_Movies.Enabled = If(Master.eSettings.MovieMissingItemsAnyEnabled, isEnabled, False)
        chkFilterNew_Movies.Enabled = isEnabled
        chkFilterTolerance_Movies.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        pnlFilterMissingItems_Movies.Visible = If(Not isEnabled, False, pnlFilterMissingItems_Movies.Visible)
        rbFilterAnd_Movies.Enabled = isEnabled
        rbFilterOr_Movies.Enabled = isEnabled
        txtFilterCountry_Movies.Enabled = isEnabled
        txtFilterGenre_Movies.Enabled = isEnabled
        txtFilterDataField_Movies.Enabled = isEnabled
        txtFilterSource_Movies.Enabled = isEnabled
    End Sub

    Private Sub EnableFilters_MovieSets(ByVal isEnabled As Boolean)
        btnClearFilters_MovieSets.Enabled = isEnabled
        btnFilterMissing_MovieSets.Enabled = isEnabled
        'Me.btnSortDate.Enabled = isEnabled
        'Me.btnIMDBRating.Enabled = isEnabled
        'Me.btnSortTitle.Enabled = isEnabled
        'Me.cbFilterFileSource.Enabled = isEnabled
        'Me.cbFilterYear.Enabled = isEnabled
        'Me.cbFilterYearMod.Enabled = isEnabled
        cbSearchMovieSets.Enabled = isEnabled
        'Me.chkFilterDupe.Enabled = isEnabled
        chkFilterEmpty_MovieSets.Enabled = isEnabled
        chkFilterLock_MovieSets.Enabled = isEnabled
        chkFilterMark_MovieSets.Enabled = isEnabled
        'Me.chkFilterMarkCustom1.Enabled = isEnabled
        'Me.chkFilterMarkCustom2.Enabled = isEnabled
        'Me.chkFilterMarkCustom3.Enabled = isEnabled
        'Me.chkFilterMarkCustom4.Enabled = isEnabled
        chkFilterMissing_MovieSets.Enabled = If(Master.eSettings.MovieSetMissingItemsAnyEnabled, isEnabled, False)
        chkFilterMultiple_MovieSets.Enabled = isEnabled
        chkFilterNew_MovieSets.Enabled = isEnabled
        chkFilterOne_MovieSets.Enabled = isEnabled
        'Me.chkFilterTolerance.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        pnlFilterMissingItems_MovieSets.Visible = If(Not isEnabled, False, pnlFilterMissingItems_MovieSets.Visible)
        rbFilterAnd_MovieSets.Enabled = isEnabled
        rbFilterOr_MovieSets.Enabled = isEnabled
        'Me.txtFilterCountry.Enabled = isEnabled
        'Me.txtFilterGenre.Enabled = isEnabled
        'Me.txtFilterSource.Enabled = isEnabled
    End Sub

    Private Sub EnableFilters_Shows(ByVal isEnabled As Boolean)
        btnClearFilters_Shows.Enabled = isEnabled
        btnFilterMissing_Shows.Enabled = isEnabled
        'Me.btnSortDate.Enabled = isEnabled
        'Me.btnIMDBRating.Enabled = isEnabled
        btnFilterSortTitle_Shows.Enabled = isEnabled
        'Me.cbFilterFileSource.Enabled = isEnabled
        'Me.cbFilterYear.Enabled = isEnabled
        'Me.cbFilterYearMod.Enabled = isEnabled
        cbSearchShows.Enabled = isEnabled
        'Me.chkFilterDuplicates.Enabled = isEnabled
        chkFilterLock_Shows.Enabled = isEnabled
        chkFilterMark_Shows.Enabled = isEnabled
        'Me.chkFilterMarkCustom1.Enabled = isEnabled
        'Me.chkFilterMarkCustom2.Enabled = isEnabled
        'Me.chkFilterMarkCustom3.Enabled = isEnabled
        'Me.chkFilterMarkCustom4.Enabled = isEnabled
        chkFilterMissing_Shows.Enabled = If(Master.eSettings.TVShowMissingItemsAnyEnabled, isEnabled, False)
        chkFilterNewEpisodes_Shows.Enabled = isEnabled
        chkFilterNewShows_Shows.Enabled = isEnabled
        'Me.chkFilterTolerance.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        pnlFilterMissingItems_Shows.Visible = If(Not isEnabled, False, pnlFilterMissingItems_Shows.Visible)
        rbFilterAnd_Shows.Enabled = isEnabled
        rbFilterOr_Shows.Enabled = isEnabled
        'Me.txtFilterCountry.Enabled = isEnabled
        'Me.txtFilterGenre.Enabled = isEnabled
        txtFilterSource_Shows.Enabled = isEnabled
    End Sub

    Private Sub ErrorOccurred()
        mnuMainError.Visible = True
        If dlgErrorViewer.Visible Then dlgErrorViewer.UpdateLog()
    End Sub

    Private Sub ErrorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainError.Click
        dlgErrorViewer.Show(Me)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainFileExit.Click, cmnuTrayExit.Click
        If Master.isCL Then
            'fLoading.SetLoadingMesg("Canceling ...")
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(370, "Canceling Load..."))
            If bwMovieScraper.IsBusy Then bwMovieScraper.CancelAsync()
            If bwReload_Movies.IsBusy Then bwReload_Movies.CancelAsync()
            While bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse bwMovieScraper.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        Else
            Close()
            Application.Exit()
        End If
    End Sub

    Private Sub FillEpisodes(ByVal ShowID As Long, ByVal Season As Integer)
        Dim sEpisodeSorting As Enums.EpisodeSorting = Master.DB.GetTVShowEpisodeSorting(ShowID)

        bsTVEpisodes.DataSource = Nothing
        dgvTVEpisodes.DataSource = Nothing

        Application.DoEvents()

        dgvTVEpisodes.Enabled = False

        If Season = 999 Then
            Master.DB.FillDataTable(dtTVEpisodes, String.Concat("SELECT * FROM episodelist WHERE idShow = ", ShowID, If(Master.eSettings.TVDisplayMissingEpisodes, String.Empty, " AND Missing = 0"), " ORDER BY Season, Episode;"))
        Else
            Master.DB.FillDataTable(dtTVEpisodes, String.Concat("SELECT * FROM episodelist WHERE idShow = ", ShowID, " AND Season = ", Season, If(Master.eSettings.TVDisplayMissingEpisodes, String.Empty, " AND Missing = 0"), " ORDER BY Episode;"))
        End If

        If dtTVEpisodes.Rows.Count > 0 Then

            With Me
                .bsTVEpisodes.DataSource = .dtTVEpisodes
                .dgvTVEpisodes.DataSource = .bsTVEpisodes

                Try
                    If Master.eSettings.TVGeneralEpisodeListSorting.Count > 0 Then
                        For Each mColumn In Master.eSettings.TVGeneralEpisodeListSorting
                            dgvTVEpisodes.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                Catch ex As Exception
                    logger.Warn("default list for episode list sorting has been loaded")
                    Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVEpisodeListSorting, True)
                    If Master.eSettings.TVGeneralEpisodeListSorting.Count > 0 Then
                        For Each mColumn In Master.eSettings.TVGeneralEpisodeListSorting
                            dgvTVEpisodes.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                End Try

                dgvTVEpisodes.Columns("Season").DisplayIndex = 0
                dgvTVEpisodes.Columns("Episode").DisplayIndex = 1
                dgvTVEpisodes.Columns("Aired").DisplayIndex = 2

                For i As Integer = 0 To .dgvTVEpisodes.Columns.Count - 1
                    .dgvTVEpisodes.Columns(i).Visible = False
                Next

                .dgvTVEpisodes.Columns("Aired").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Aired").Width = 80
                .dgvTVEpisodes.Columns("Aired").ReadOnly = True
                .dgvTVEpisodes.Columns("Aired").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Aired").Visible = sEpisodeSorting = Enums.EpisodeSorting.Aired
                .dgvTVEpisodes.Columns("Aired").ToolTipText = Master.eLang.GetString(728, "Aired")
                .dgvTVEpisodes.Columns("Aired").HeaderText = Master.eLang.GetString(728, "Aired")
                .dgvTVEpisodes.Columns("Episode").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Episode").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                .dgvTVEpisodes.Columns("Episode").ReadOnly = True
                .dgvTVEpisodes.Columns("Episode").MinimumWidth = If(Season = 999, 35, 70)
                .dgvTVEpisodes.Columns("Episode").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Episode").Visible = Not sEpisodeSorting = Enums.EpisodeSorting.Aired
                .dgvTVEpisodes.Columns("Episode").ToolTipText = Master.eLang.GetString(755, "Episode #")
                .dgvTVEpisodes.Columns("Episode").HeaderText = "#"
                .dgvTVEpisodes.Columns("Episode").DefaultCellStyle.Format = "00"
                .dgvTVEpisodes.Columns("FanartPath").Width = 20
                .dgvTVEpisodes.Columns("FanartPath").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("FanartPath").ReadOnly = True
                .dgvTVEpisodes.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("FanartPath").Visible = Not CheckColumnHide_TVEpisodes("FanartPath")
                .dgvTVEpisodes.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                .dgvTVEpisodes.Columns("HasSub").Width = 20
                .dgvTVEpisodes.Columns("HasSub").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("HasSub").ReadOnly = True
                .dgvTVEpisodes.Columns("HasSub").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("HasSub").Visible = Not CheckColumnHide_TVEpisodes("HasSub")
                .dgvTVEpisodes.Columns("HasSub").ToolTipText = Master.eLang.GetString(152, "Subtitles")
                .dgvTVEpisodes.Columns("NfoPath").Width = 20
                .dgvTVEpisodes.Columns("NfoPath").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("NfoPath").ReadOnly = True
                .dgvTVEpisodes.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("NfoPath").Visible = Not CheckColumnHide_TVEpisodes("NfoPath")
                .dgvTVEpisodes.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                .dgvTVEpisodes.Columns("Playcount").Width = 20
                .dgvTVEpisodes.Columns("Playcount").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Playcount").ReadOnly = True
                .dgvTVEpisodes.Columns("Playcount").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Playcount").Visible = Not CheckColumnHide_TVEpisodes("Playcount")
                .dgvTVEpisodes.Columns("Playcount").ToolTipText = Master.eLang.GetString(981, "Watched")
                .dgvTVEpisodes.Columns("PosterPath").Width = 20
                .dgvTVEpisodes.Columns("PosterPath").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("PosterPath").ReadOnly = True
                .dgvTVEpisodes.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("PosterPath").Visible = Not CheckColumnHide_TVEpisodes("PosterPath")
                .dgvTVEpisodes.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                .dgvTVEpisodes.Columns("Season").MinimumWidth = 35
                .dgvTVEpisodes.Columns("Season").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Season").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                .dgvTVEpisodes.Columns("Season").ReadOnly = True
                .dgvTVEpisodes.Columns("Season").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Season").Visible = Season = 999
                .dgvTVEpisodes.Columns("Season").ToolTipText = Master.eLang.GetString(659, "Season #")
                .dgvTVEpisodes.Columns("Season").HeaderText = "#"
                .dgvTVEpisodes.Columns("Season").DefaultCellStyle.Format = "00"
                .dgvTVEpisodes.Columns("Title").Resizable = DataGridViewTriState.True
                .dgvTVEpisodes.Columns("Title").ReadOnly = True
                .dgvTVEpisodes.Columns("Title").MinimumWidth = 83
                .dgvTVEpisodes.Columns("Title").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Title").Visible = True
                .dgvTVEpisodes.Columns("Title").ToolTipText = Master.eLang.GetString(21, "Title")
                .dgvTVEpisodes.Columns("Title").HeaderText = Master.eLang.GetString(21, "Title")

                .dgvTVEpisodes.Columns("idEpisode").ValueType = GetType(Int32)
                .dgvTVEpisodes.Columns("idShow").ValueType = GetType(Int32)
                .dgvTVEpisodes.Columns("Episode").ValueType = GetType(Int32)
                .dgvTVEpisodes.Columns("Season").ValueType = GetType(Int32)

                If Master.isWindows Then .dgvTVEpisodes.Columns("Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                ResizeTVLists(.dgvTVEpisodes.Columns("Title").Index)

                .dgvTVEpisodes.ClearSelection()
                .dgvTVEpisodes.CurrentCell = Nothing

            End With
        End If

        dgvTVEpisodes.Enabled = True
    End Sub
    ''' <summary>
    ''' Reloads the DB and refresh the lists
    ''' </summary>
    ''' <param name="doMovies">reload movies</param>
    ''' <param name="doMovieSets">reload moviesets</param>
    ''' <param name="doTVShows">reload tv shows</param>
    ''' <remarks></remarks>
    Private Sub FillList(ByVal doMovies As Boolean, ByVal doMovieSets As Boolean, ByVal doTVShows As Boolean)
        If doMovies Then
            bsMovies.DataSource = Nothing
            dgvMovies.DataSource = Nothing
            ClearInfo()

            If Not String.IsNullOrEmpty(filSearch_Movies) AndAlso cbSearchMovies.Text = Master.eLang.GetString(100, "Actor") Then
                Master.DB.FillDataTable(dtMovies, String.Concat("SELECT DISTINCT '", currList_Movies, "'.* FROM actors ",
                                                                   "LEFT OUTER JOIN actorlinkmovie ON (actors.idActor = actorlinkmovie.idActor) ",
                                                                   "INNER JOIN '", currList_Movies, "' ON (actorlinkmovie.idMovie = '", currList_Movies, "'.idMovie) ",
                                                                   "WHERE actors.strActor LIKE '%", filSearch_Movies, "%' ",
                                                                   "ORDER BY '", currList_Movies, "'.ListTitle COLLATE NOCASE;"))
            ElseIf Not String.IsNullOrEmpty(filSearch_Movies) AndAlso cbSearchMovies.Text = Master.eLang.GetString(233, "Role") Then
                Master.DB.FillDataTable(dtMovies, String.Concat("SELECT DISTINCT '", currList_Movies, "'.* FROM actorlinkmovie ",
                                                                   "INNER JOIN '", currList_Movies, "' ON (actorlinkmovie.idMovie = '", currList_Movies, "'.idMovie) ",
                                                                   "WHERE actorlinkmovie.strRole LIKE '%", filSearch_Movies, "%' ",
                                                                   "ORDER BY '", currList_Movies, "'.ListTitle COLLATE NOCASE;"))
            Else
                If chkFilterDuplicates_Movies.Checked Then
                    Master.DB.FillDataTable(dtMovies, String.Concat("SELECT * FROM '", currList_Movies, "' ",
                                                                       "WHERE imdb IN (SELECT imdb FROM '", currList_Movies, "' WHERE imdb IS NOT NULL AND LENGTH(imdb) > 0 GROUP BY imdb HAVING ( COUNT(imdb) > 1 )) ",
                                                                       "ORDER BY ListTitle COLLATE NOCASE;"))
                Else
                    Master.DB.FillDataTable(dtMovies, String.Concat("SELECT * FROM '", currList_Movies, "' ",
                                                                       "ORDER BY ListTitle COLLATE NOCASE;"))
                End If
            End If
        End If

        If doMovieSets Then
            bsMovieSets.DataSource = Nothing
            dgvMovieSets.DataSource = Nothing
            ClearInfo()
            Master.DB.FillDataTable(dtMovieSets, String.Concat("SELECT * FROM '", currList_MovieSets, "' ",
                                                                  "ORDER BY ListTitle COLLATE NOCASE;"))
        End If

        If doTVShows Then
            bsTVShows.DataSource = Nothing
            dgvTVShows.DataSource = Nothing
            bsTVSeasons.DataSource = Nothing
            dgvTVSeasons.DataSource = Nothing
            bsTVEpisodes.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing
            ClearInfo()
            Master.DB.FillDataTable(dtTVShows, String.Concat("SELECT * FROM '", currList_TVShows, "' ",
                                                              "ORDER BY ListTitle COLLATE NOCASE;"))
        End If


        If Master.isCL Then
            LoadingDone = True
        Else
            If doMovies Then
                prevRow_Movie = -2
                If dtMovies.Rows.Count > 0 Then
                    With Me
                        .bsMovies.DataSource = .dtMovies
                        .dgvMovies.DataSource = .bsMovies

                        Try
                            If Master.eSettings.MovieGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieGeneralMediaListSorting
                                    dgvMovies.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        Catch ex As Exception
                            logger.Warn("default list for movie list sorting has been loaded")
                            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieListSorting, True)
                            If Master.eSettings.MovieGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieGeneralMediaListSorting
                                    dgvMovies.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        End Try

                        For i As Integer = 0 To .dgvMovies.Columns.Count - 1
                            .dgvMovies.Columns(i).Visible = False
                        Next

                        .dgvMovies.Columns("BannerPath").Width = 20
                        .dgvMovies.Columns("BannerPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("BannerPath").ReadOnly = True
                        .dgvMovies.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("BannerPath").Visible = Not CheckColumnHide_Movies("BannerPath")
                        .dgvMovies.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                        .dgvMovies.Columns("ClearArtPath").Width = 20
                        .dgvMovies.Columns("ClearArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("ClearArtPath").ReadOnly = True
                        .dgvMovies.Columns("ClearArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ClearArtPath").Visible = Not CheckColumnHide_Movies("ClearArtPath")
                        .dgvMovies.Columns("ClearArtPath").ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                        .dgvMovies.Columns("ClearLogoPath").Width = 20
                        .dgvMovies.Columns("ClearLogoPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("ClearLogoPath").ReadOnly = True
                        .dgvMovies.Columns("ClearLogoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ClearLogoPath").Visible = Not CheckColumnHide_Movies("ClearLogoPath")
                        .dgvMovies.Columns("ClearLogoPath").ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                        .dgvMovies.Columns("DiscArtPath").Width = 20
                        .dgvMovies.Columns("DiscArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("DiscArtPath").ReadOnly = True
                        .dgvMovies.Columns("DiscArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("DiscArtPath").Visible = Not CheckColumnHide_Movies("DiscArtPath")
                        .dgvMovies.Columns("DiscArtPath").ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                        .dgvMovies.Columns("EFanartsPath").Width = 20
                        .dgvMovies.Columns("EFanartsPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("EFanartsPath").ReadOnly = True
                        .dgvMovies.Columns("EFanartsPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("EFanartsPath").Visible = Not CheckColumnHide_Movies("EFanartsPath")
                        .dgvMovies.Columns("EFanartsPath").ToolTipText = Master.eLang.GetString(992, "Extrafanarts")
                        .dgvMovies.Columns("EThumbsPath").Width = 20
                        .dgvMovies.Columns("EThumbsPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("EThumbsPath").ReadOnly = True
                        .dgvMovies.Columns("EThumbsPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("EThumbsPath").Visible = Not CheckColumnHide_Movies("EThumbsPath")
                        .dgvMovies.Columns("EThumbsPath").ToolTipText = Master.eLang.GetString(153, "Extrathumbs")
                        .dgvMovies.Columns("FanartPath").Width = 20
                        .dgvMovies.Columns("FanartPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("FanartPath").ReadOnly = True
                        .dgvMovies.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("FanartPath").Visible = Not CheckColumnHide_Movies("FanartPath")
                        .dgvMovies.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                        .dgvMovies.Columns("HasSet").Width = 20
                        .dgvMovies.Columns("HasSet").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("HasSet").ReadOnly = True
                        .dgvMovies.Columns("HasSet").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("HasSet").Visible = Not CheckColumnHide_Movies("HasSet")
                        .dgvMovies.Columns("HasSet").ToolTipText = Master.eLang.GetString(1295, "Part of a MovieSet")
                        .dgvMovies.Columns("HasSub").Width = 20
                        .dgvMovies.Columns("HasSub").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("HasSub").ReadOnly = True
                        .dgvMovies.Columns("HasSub").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("HasSub").Visible = Not CheckColumnHide_Movies("HasSub")
                        .dgvMovies.Columns("HasSub").ToolTipText = Master.eLang.GetString(152, "Subtitles")
                        .dgvMovies.Columns("Imdb").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Imdb").ReadOnly = True
                        .dgvMovies.Columns("Imdb").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Imdb").Visible = Not CheckColumnHide_Movies("Imdb")
                        .dgvMovies.Columns("Imdb").ToolTipText = Master.eLang.GetString(61, "IMDB ID")
                        .dgvMovies.Columns("Imdb").HeaderText = Master.eLang.GetString(61, "IMDB ID")
                        .dgvMovies.Columns("Imdb").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("LandscapePath").Width = 20
                        .dgvMovies.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("LandscapePath").ReadOnly = True
                        .dgvMovies.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("LandscapePath").Visible = Not CheckColumnHide_Movies("LandscapePath")
                        .dgvMovies.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                        .dgvMovies.Columns("ListTitle").Resizable = DataGridViewTriState.True
                        .dgvMovies.Columns("ListTitle").ReadOnly = True
                        .dgvMovies.Columns("ListTitle").MinimumWidth = 83
                        .dgvMovies.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ListTitle").Visible = True
                        .dgvMovies.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                        .dgvMovies.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                        .dgvMovies.Columns("MPAA").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("MPAA").Width = 70
                        .dgvMovies.Columns("MPAA").ReadOnly = True
                        .dgvMovies.Columns("MPAA").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("MPAA").Visible = Not CheckColumnHide_Movies("MPAA")
                        .dgvMovies.Columns("MPAA").ToolTipText = Master.eLang.GetString(401, "MPAA")
                        .dgvMovies.Columns("MPAA").HeaderText = Master.eLang.GetString(401, "MPAA")
                        '.dgvMovies.Columns("MPAA").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("NfoPath").Width = 20
                        .dgvMovies.Columns("NfoPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("NfoPath").ReadOnly = True
                        .dgvMovies.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("NfoPath").Visible = Not CheckColumnHide_Movies("NfoPath")
                        .dgvMovies.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                        .dgvMovies.Columns("OriginalTitle").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("OriginalTitle").ReadOnly = True
                        .dgvMovies.Columns("OriginalTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("OriginalTitle").Visible = Not CheckColumnHide_Movies("OriginalTitle")
                        .dgvMovies.Columns("OriginalTitle").ToolTipText = Master.eLang.GetString(302, "Original Title")
                        .dgvMovies.Columns("OriginalTitle").HeaderText = Master.eLang.GetString(302, "Original Title")
                        .dgvMovies.Columns("OriginalTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("Playcount").Width = 20
                        .dgvMovies.Columns("Playcount").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Playcount").ReadOnly = True
                        .dgvMovies.Columns("Playcount").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Playcount").Visible = Not CheckColumnHide_Movies("Playcount")
                        .dgvMovies.Columns("Playcount").ToolTipText = Master.eLang.GetString(981, "Watched")
                        .dgvMovies.Columns("PosterPath").Width = 20
                        .dgvMovies.Columns("PosterPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("PosterPath").ReadOnly = True
                        .dgvMovies.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("PosterPath").Visible = Not CheckColumnHide_Movies("PosterPath")
                        .dgvMovies.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                        .dgvMovies.Columns("Rating").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Rating").ReadOnly = True
                        .dgvMovies.Columns("Rating").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Rating").Visible = Not CheckColumnHide_Movies("Rating")
                        .dgvMovies.Columns("Rating").ToolTipText = Master.eLang.GetString(400, "Rating")
                        .dgvMovies.Columns("Rating").HeaderText = Master.eLang.GetString(400, "Rating")
                        .dgvMovies.Columns("Rating").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("ThemePath").Width = 20
                        .dgvMovies.Columns("ThemePath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("ThemePath").ReadOnly = True
                        .dgvMovies.Columns("ThemePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ThemePath").Visible = Not CheckColumnHide_Movies("ThemePath")
                        .dgvMovies.Columns("ThemePath").ToolTipText = Master.eLang.GetString(1118, "Theme")
                        .dgvMovies.Columns("TMDB").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("TMDB").ReadOnly = True
                        .dgvMovies.Columns("TMDB").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("TMDB").Visible = Not CheckColumnHide_Movies("TMDB")
                        .dgvMovies.Columns("TMDB").ToolTipText = Master.eLang.GetString(933, "TMDB ID")
                        .dgvMovies.Columns("TMDB").HeaderText = Master.eLang.GetString(933, "TMDB ID")
                        .dgvMovies.Columns("TMDB").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("TrailerPath").Width = 20
                        .dgvMovies.Columns("TrailerPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("TrailerPath").ReadOnly = True
                        .dgvMovies.Columns("TrailerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("TrailerPath").Visible = Not CheckColumnHide_Movies("TrailerPath")
                        .dgvMovies.Columns("TrailerPath").ToolTipText = Master.eLang.GetString(151, "Trailer")
                        .dgvMovies.Columns("Year").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Year").ReadOnly = True
                        .dgvMovies.Columns("Year").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Year").Visible = Not CheckColumnHide_Movies("Year")
                        .dgvMovies.Columns("Year").ToolTipText = Master.eLang.GetString(278, "Year")
                        .dgvMovies.Columns("Year").HeaderText = Master.eLang.GetString(278, "Year")
                        .dgvMovies.Columns("Year").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

                        .dgvMovies.Columns("idMovie").ValueType = GetType(Int32)

                        If Master.isWindows Then .dgvMovies.Columns("ListTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        ResizeMoviesList()
                    End With
                End If
            End If

            If doMovieSets Then
                prevRow_MovieSet = -2
                dgvMovieSets.Enabled = False
                If dtMovieSets.Rows.Count > 0 Then
                    With Me
                        .bsMovieSets.DataSource = .dtMovieSets
                        .dgvMovieSets.DataSource = .bsMovieSets

                        Try
                            If Master.eSettings.MovieSetGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieSetGeneralMediaListSorting
                                    dgvMovieSets.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        Catch ex As Exception
                            logger.Warn("default list for movieset list sorting has been loaded")
                            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieSetListSorting, True)
                            If Master.eSettings.MovieSetGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieSetGeneralMediaListSorting
                                    dgvMovieSets.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        End Try

                        For i As Integer = 0 To .dgvMovieSets.Columns.Count - 1
                            .dgvMovieSets.Columns(i).Visible = False
                        Next

                        .dgvMovieSets.Columns("BannerPath").Width = 20
                        .dgvMovieSets.Columns("BannerPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("BannerPath").ReadOnly = True
                        .dgvMovieSets.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("BannerPath").Visible = Not CheckColumnHide_MovieSets("BannerPath")
                        .dgvMovieSets.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                        .dgvMovieSets.Columns("ClearArtPath").Width = 20
                        .dgvMovieSets.Columns("ClearArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("ClearArtPath").ReadOnly = True
                        .dgvMovieSets.Columns("ClearArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("ClearArtPath").Visible = Not CheckColumnHide_MovieSets("ClearArtPath")
                        .dgvMovieSets.Columns("ClearArtPath").ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                        .dgvMovieSets.Columns("ClearLogoPath").Width = 20
                        .dgvMovieSets.Columns("ClearLogoPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("ClearLogoPath").ReadOnly = True
                        .dgvMovieSets.Columns("ClearLogoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("ClearLogoPath").Visible = Not CheckColumnHide_MovieSets("ClearLogoPath")
                        .dgvMovieSets.Columns("ClearLogoPath").ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                        .dgvMovieSets.Columns("DiscArtPath").Width = 20
                        .dgvMovieSets.Columns("DiscArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("DiscArtPath").ReadOnly = True
                        .dgvMovieSets.Columns("DiscArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("DiscArtPath").Visible = Not CheckColumnHide_MovieSets("DiscArtPath")
                        .dgvMovieSets.Columns("DiscArtPath").ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                        .dgvMovieSets.Columns("FanartPath").Width = 20
                        .dgvMovieSets.Columns("FanartPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("FanartPath").ReadOnly = True
                        .dgvMovieSets.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("FanartPath").Visible = Not CheckColumnHide_MovieSets("FanartPath")
                        .dgvMovieSets.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                        .dgvMovieSets.Columns("LandscapePath").Width = 20
                        .dgvMovieSets.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("LandscapePath").ReadOnly = True
                        .dgvMovieSets.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("LandscapePath").Visible = Not CheckColumnHide_MovieSets("LandscapePath")
                        .dgvMovieSets.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                        .dgvMovieSets.Columns("ListTitle").Resizable = DataGridViewTriState.True
                        .dgvMovieSets.Columns("ListTitle").ReadOnly = True
                        .dgvMovieSets.Columns("ListTitle").MinimumWidth = 83
                        .dgvMovieSets.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("ListTitle").Visible = True
                        .dgvMovieSets.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                        .dgvMovieSets.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                        .dgvMovieSets.Columns("NfoPath").Width = 20
                        .dgvMovieSets.Columns("NfoPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("NfoPath").ReadOnly = True
                        .dgvMovieSets.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("NfoPath").Visible = Not CheckColumnHide_MovieSets("NfoPath")
                        .dgvMovieSets.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                        .dgvMovieSets.Columns("PosterPath").Width = 20
                        .dgvMovieSets.Columns("PosterPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("PosterPath").ReadOnly = True
                        .dgvMovieSets.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("PosterPath").Visible = Not CheckColumnHide_MovieSets("PosterPath")
                        .dgvMovieSets.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")

                        .dgvMovieSets.Columns("idSet").ValueType = GetType(Int32)

                        If Master.isWindows Then .dgvMovieSets.Columns("ListTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        ResizeMovieSetsList()
                    End With

                    dgvMovieSets.Enabled = True
                End If
            End If

            If doTVShows Then
                currList = 0
                prevRow_TVEpisode = -2
                prevRow_TVSeason = -2
                prevRow_TVShow = -2
                dgvTVShows.Enabled = False
                If dtTVShows.Rows.Count > 0 Then
                    With Me
                        .bsTVShows.DataSource = .dtTVShows
                        .dgvTVShows.DataSource = .bsTVShows

                        Try
                            If Master.eSettings.TVGeneralShowListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.TVGeneralShowListSorting
                                    dgvTVShows.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        Catch ex As Exception
                            logger.Warn("default list for tv show list sorting has been loaded")
                            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowListSorting, True)
                            If Master.eSettings.TVGeneralShowListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.TVGeneralShowListSorting
                                    dgvTVShows.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        End Try

                        For i As Integer = 0 To .dgvTVShows.Columns.Count - 1
                            .dgvTVShows.Columns(i).Visible = False
                        Next

                        .dgvTVShows.Columns("BannerPath").Width = 20
                        .dgvTVShows.Columns("BannerPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("BannerPath").ReadOnly = True
                        .dgvTVShows.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("BannerPath").Visible = Not CheckColumnHide_TVShows("BannerPath")
                        .dgvTVShows.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                        .dgvTVShows.Columns("CharacterArtPath").Width = 20
                        .dgvTVShows.Columns("CharacterArtPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("CharacterArtPath").ReadOnly = True
                        .dgvTVShows.Columns("CharacterArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("CharacterArtPath").Visible = Not CheckColumnHide_TVShows("CharacterArtPath")
                        .dgvTVShows.Columns("CharacterArtPath").ToolTipText = Master.eLang.GetString(1140, "CharacterArt")
                        .dgvTVShows.Columns("ClearArtPath").Width = 20
                        .dgvTVShows.Columns("ClearArtPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("ClearArtPath").ReadOnly = True
                        .dgvTVShows.Columns("ClearArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ClearArtPath").Visible = Not CheckColumnHide_TVShows("ClearArtPath")
                        .dgvTVShows.Columns("ClearArtPath").ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                        .dgvTVShows.Columns("ClearLogoPath").Width = 20
                        .dgvTVShows.Columns("ClearLogoPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("ClearLogoPath").ReadOnly = True
                        .dgvTVShows.Columns("ClearLogoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ClearLogoPath").Visible = Not CheckColumnHide_TVShows("ClearLogoPath")
                        .dgvTVShows.Columns("ClearLogoPath").ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                        .dgvTVShows.Columns("EFanartsPath").Width = 20
                        .dgvTVShows.Columns("EFanartsPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("EFanartsPath").ReadOnly = True
                        .dgvTVShows.Columns("EFanartsPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("EFanartsPath").Visible = Not CheckColumnHide_TVShows("EFanartsPath")
                        .dgvTVShows.Columns("EFanartsPath").ToolTipText = Master.eLang.GetString(992, "Extrafanarts")
                        .dgvTVShows.Columns("Episodes").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                        .dgvTVShows.Columns("Episodes").MinimumWidth = 30
                        .dgvTVShows.Columns("Episodes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .dgvTVShows.Columns("Episodes").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("Episodes").ReadOnly = True
                        .dgvTVShows.Columns("Episodes").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("Episodes").Visible = Not CheckColumnHide_TVShows("Episodes")
                        .dgvTVShows.Columns("Episodes").ToolTipText = Master.eLang.GetString(682, "Episodes")
                        .dgvTVShows.Columns("Episodes").HeaderText = String.Empty
                        .dgvTVShows.Columns("FanartPath").Width = 20
                        .dgvTVShows.Columns("FanartPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("FanartPath").ReadOnly = True
                        .dgvTVShows.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("FanartPath").Visible = Not CheckColumnHide_TVShows("FanartPath")
                        .dgvTVShows.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                        .dgvTVShows.Columns("HasWatched").Width = 20
                        .dgvTVShows.Columns("HasWatched").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("HasWatched").ReadOnly = True
                        .dgvTVShows.Columns("HasWatched").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("HasWatched").Visible = Not CheckColumnHide_TVShows("HasWatched")
                        .dgvTVShows.Columns("HasWatched").ToolTipText = Master.eLang.GetString(981, "Watched")
                        .dgvTVShows.Columns("LandscapePath").Width = 20
                        .dgvTVShows.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("LandscapePath").ReadOnly = True
                        .dgvTVShows.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("LandscapePath").Visible = Not CheckColumnHide_TVShows("LandscapePath")
                        .dgvTVShows.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                        .dgvTVShows.Columns("ListTitle").Resizable = DataGridViewTriState.True
                        .dgvTVShows.Columns("ListTitle").ReadOnly = True
                        .dgvTVShows.Columns("ListTitle").MinimumWidth = 83
                        .dgvTVShows.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ListTitle").Visible = True
                        .dgvTVShows.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                        .dgvTVShows.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                        .dgvTVShows.Columns("NfoPath").Width = 20
                        .dgvTVShows.Columns("NfoPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("NfoPath").ReadOnly = True
                        .dgvTVShows.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("NfoPath").Visible = Not CheckColumnHide_TVShows("NfoPath")
                        .dgvTVShows.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                        .dgvTVShows.Columns("PosterPath").Width = 20
                        .dgvTVShows.Columns("PosterPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("PosterPath").ReadOnly = True
                        .dgvTVShows.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("PosterPath").Visible = Not CheckColumnHide_TVShows("PosterPath")
                        .dgvTVShows.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                        .dgvTVShows.Columns("Status").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("Status").ReadOnly = True
                        .dgvTVShows.Columns("Status").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("Status").Visible = Not CheckColumnHide_TVShows("Status")
                        .dgvTVShows.Columns("Status").ToolTipText = Master.eLang.GetString(215, "Status")
                        .dgvTVShows.Columns("Status").HeaderText = Master.eLang.GetString(215, "Status")
                        .dgvTVShows.Columns("Status").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvTVShows.Columns("strOriginalTitle").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("strOriginalTitle").ReadOnly = True
                        .dgvTVShows.Columns("strOriginalTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("strOriginalTitle").Visible = Not CheckColumnHide_TVShows("strOriginalTitle")
                        .dgvTVShows.Columns("strOriginalTitle").ToolTipText = Master.eLang.GetString(302, "Original Title")
                        .dgvTVShows.Columns("strOriginalTitle").HeaderText = Master.eLang.GetString(302, "Original Title")
                        .dgvTVShows.Columns("strOriginalTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvTVShows.Columns("ThemePath").Width = 20
                        .dgvTVShows.Columns("ThemePath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("ThemePath").ReadOnly = True
                        .dgvTVShows.Columns("ThemePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ThemePath").Visible = Not CheckColumnHide_TVShows("ThemePath")
                        .dgvTVShows.Columns("ThemePath").ToolTipText = Master.eLang.GetString(1118, "Theme")

                        .dgvTVShows.Columns("idShow").ValueType = GetType(Int32)

                        If Master.isWindows Then .dgvTVShows.Columns("ListTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        ResizeTVLists(.dgvTVShows.Columns("ListTitle").Index)
                    End With

                    dgvTVShows.Enabled = True
                End If
            End If

            If dgvMovies.RowCount > 0 OrElse dgvMovieSets.RowCount > 0 OrElse dgvTVShows.RowCount > 0 Then
                SetControlsEnabled(True)
            Else
                SetControlsEnabled(False, False, False)
                SetStatus(String.Empty)
                ClearInfo()
            End If
        End If

        If Not Master.isCL Then
            mnuUpdate.Enabled = True
            cmnuTrayExit.Enabled = True
            cmnuTraySettings.Enabled = True
            mnuMainEdit.Enabled = True
            cmnuTrayUpdate.Enabled = True
            mnuMainHelp.Enabled = True
            tslLoading.Visible = False
            tspbLoading.Visible = False
            tspbLoading.Value = 0
            tcMain.Enabled = True
            DoTitleCheck()
            EnableFilters_Movies(True)
            EnableFilters_MovieSets(True)
            EnableFilters_Shows(True)
            If doMovies Then
                RestoreFilter_Movies()
            End If
            If doMovieSets Then
                RestoreFilter_MovieSets()
            End If
            If doTVShows Then
                RestoreFilter_Shows()
            End If
            If doMovies AndAlso doMovieSets AndAlso doTVShows Then
                UpdateMainTabCounts()
            End If
        End If
    End Sub

    Private Sub FillScreenInfoWithImages()
        Dim g As Graphics
        Dim strSize As String
        Dim lenSize As Integer
        Dim rect As Rectangle

        If MainPoster.Image IsNot Nothing OrElse MainPoster.LoadFromMemoryStream Then
            lblPosterSize.Text = String.Format("{0} x {1}", MainPoster.Image.Width, MainPoster.Image.Height)
            pbPosterCache.Image = MainPoster.Image
            ImageUtils.ResizePB(pbPoster, pbPosterCache, PosterMaxHeight, PosterMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbPoster)

            If Master.eSettings.GeneralShowImgDims Then
                lblPosterSize.Visible = True
            Else
                lblPosterSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblPosterTitle.Visible = True
            Else
                lblPosterTitle.Visible = False
            End If
        Else
            If pbPoster.Image IsNot Nothing Then
                pbPoster.Image.Dispose()
                pbPoster.Image = Nothing
            End If
        End If

        If MainFanartSmall.Image IsNot Nothing OrElse MainFanartSmall.LoadFromMemoryStream Then
            lblFanartSmallSize.Text = String.Format("{0} x {1}", MainFanartSmall.Image.Width, MainFanartSmall.Image.Height)
            pbFanartSmallCache.Image = MainFanartSmall.Image
            ImageUtils.ResizePB(pbFanartSmall, pbFanartSmallCache, FanartSmallMaxHeight, FanartSmallMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbFanartSmall)
            'Me.pnlFanartSmall.Location = New Point(Me.pnlPoster.Location.X + Me.pnlPoster.Width + 5, Me.pnlPoster.Location.Y)    TODO: move the Location to theme settings
            pnlFanartSmall.Location = New Point(124, 130)

            If Master.eSettings.GeneralShowImgDims Then
                lblFanartSmallSize.Visible = True
            Else
                lblFanartSmallSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblFanartSmallTitle.Visible = True
            Else
                lblFanartSmallTitle.Visible = False
            End If
        Else
            If pbFanartSmall.Image IsNot Nothing Then
                pbFanartSmall.Image.Dispose()
                pbFanartSmall.Image = Nothing
            End If
        End If

        If MainLandscape.Image IsNot Nothing OrElse MainLandscape.LoadFromMemoryStream Then
            lblLandscapeSize.Text = String.Format("{0} x {1}", MainLandscape.Image.Width, MainLandscape.Image.Height)
            pbLandscapeCache.Image = MainLandscape.Image
            ImageUtils.ResizePB(pbLandscape, pbLandscapeCache, LandscapeMaxHeight, LandscapeMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbLandscape)
            'Me.pnlLandscape.Location = New Point(Me.pnlFanartSmall.Location.X + Me.pnlFanartSmall.Width + 5, Me.pnlFanartSmall.Location.Y)
            pnlLandscape.Location = New Point(419, 130)

            If Master.eSettings.GeneralShowImgDims Then
                lblLandscapeSize.Visible = True
            Else
                lblLandscapeSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblLandscapeTitle.Visible = True
            Else
                lblLandscapeTitle.Visible = False
            End If
        Else
            If pbLandscape.Image IsNot Nothing Then
                pbLandscape.Image.Dispose()
                pbLandscape.Image = Nothing
            End If
        End If

        If MainClearArt.Image IsNot Nothing OrElse MainClearArt.LoadFromMemoryStream Then
            lblClearArtSize.Text = String.Format("{0} x {1}", MainClearArt.Image.Width, MainClearArt.Image.Height)
            pbClearArtCache.Image = MainClearArt.Image
            ImageUtils.ResizePB(pbClearArt, pbClearArtCache, ClearArtMaxHeight, ClearArtMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbClearArt)
            'Me.pnlClearArt.Location = New Point(Me.pnlLandscape.Location.X + Me.pnlLandscape.Width + 5, Me.pnlLandscape.Location.Y)
            pnlClearArt.Location = New Point(715, 130)

            If Master.eSettings.GeneralShowImgDims Then
                lblClearArtSize.Visible = True
            Else
                lblClearArtSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblClearArtTitle.Visible = True
            Else
                lblClearArtTitle.Visible = False
            End If
        Else
            If pbClearArt.Image IsNot Nothing Then
                pbClearArt.Image.Dispose()
                pbClearArt.Image = Nothing
            End If
        End If

        If MainCharacterArt.Image IsNot Nothing OrElse MainCharacterArt.LoadFromMemoryStream Then
            lblCharacterArtSize.Text = String.Format("{0} x {1}", MainCharacterArt.Image.Width, MainCharacterArt.Image.Height)
            pbCharacterArtCache.Image = MainCharacterArt.Image
            ImageUtils.ResizePB(pbCharacterArt, pbCharacterArtCache, CharacterArtMaxHeight, CharacterArtMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbCharacterArt)
            'Me.pnlCharacterArt.Location = New Point(Me.pnlClearArt.Location.X + Me.pnlClearArt.Width + 5, Me.pnlClearArt.Location.Y)
            pnlCharacterArt.Location = New Point(1011, 130)

            If Master.eSettings.GeneralShowImgDims Then
                lblCharacterArtSize.Visible = True
            Else
                lblCharacterArtSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblCharacterArtTitle.Visible = True
            Else
                lblCharacterArtTitle.Visible = False
            End If
        Else
            If pbCharacterArt.Image IsNot Nothing Then
                pbCharacterArt.Image.Dispose()
                pbCharacterArt.Image = Nothing
            End If
        End If

        If MainDiscArt.Image IsNot Nothing OrElse MainDiscArt.LoadFromMemoryStream Then
            lblDiscArtSize.Text = String.Format("{0} x {1}", MainDiscArt.Image.Width, MainDiscArt.Image.Height)
            pbDiscArtCache.Image = MainDiscArt.Image
            ImageUtils.ResizePB(pbDiscArt, pbDiscArtCache, DiscArtMaxHeight, DiscArtMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbDiscArt)
            'Me.pnlDiscArt.Location = New Point(Me.pnlClearArt.Location.X + Me.pnlClearArt.Width + 5, Me.pnlClearArt.Location.Y)
            pnlDiscArt.Location = New Point(1011, 130)

            If Master.eSettings.GeneralShowImgDims Then
                lblDiscArtSize.Visible = True
            Else
                lblDiscArtSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblDiscArtTitle.Visible = True
            Else
                lblDiscArtTitle.Visible = False
            End If
        Else
            If pbDiscArt.Image IsNot Nothing Then
                pbDiscArt.Image.Dispose()
                pbDiscArt.Image = Nothing
            End If
        End If

        If MainBanner.Image IsNot Nothing OrElse MainBanner.LoadFromMemoryStream Then
            lblBannerSize.Text = String.Format("{0} x {1}", MainBanner.Image.Width, MainBanner.Image.Height)
            pbBannerCache.Image = MainBanner.Image
            ImageUtils.ResizePB(pbBanner, pbBannerCache, BannerMaxHeight, BannerMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbBanner)
            'Me.pnlBanner.Location = New Point(Me.pnlFanartSmall.Location.X, Me.pnlFanartSmall.Location.Y + Me.pnlFanartSmall.Height + 5)
            pnlBanner.Location = New Point(124, 327)

            If Master.eSettings.GeneralShowImgDims Then
                lblBannerSize.Visible = True
            Else
                lblBannerSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblBannerTitle.Visible = True
            Else
                lblBannerTitle.Visible = False
            End If
        Else
            If pbBanner.Image IsNot Nothing Then
                pbBanner.Image.Dispose()
                pbBanner.Image = Nothing
            End If
        End If

        If MainClearLogo.Image IsNot Nothing OrElse MainClearLogo.LoadFromMemoryStream Then
            lblClearLogoSize.Text = String.Format("{0} x {1}", MainClearLogo.Image.Width, MainClearLogo.Image.Height)
            pbClearLogoCache.Image = MainClearLogo.Image
            ImageUtils.ResizePB(pbClearLogo, pbClearLogoCache, ClearLogoMaxHeight, ClearLogoMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(pbClearLogo)
            'Me.pnlClearLogo.Location = New Point(Me.pnlLandscape.Location.X, Me.pnlLandscape.Location.Y + Me.pnlLandscape.Height + 5)
            pnlClearLogo.Location = New Point(419, 327)

            If Master.eSettings.GeneralShowImgDims Then
                lblClearLogoSize.Visible = True
            Else
                lblClearLogoSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                lblClearLogoTitle.Visible = True
            Else
                lblClearLogoTitle.Visible = False
            End If
        Else
            If pbClearLogo.Image IsNot Nothing Then
                pbClearLogo.Image.Dispose()
                pbClearLogo.Image = Nothing
            End If
        End If

        If MainFanart.Image IsNot Nothing OrElse MainFanart.LoadFromMemoryStream Then
            pbFanartCache.Image = MainFanart.Image

            ImageUtils.ResizePB(pbFanart, pbFanartCache, scMain.Panel2.Height - 90, scMain.Panel2.Width)
            pbFanart.Left = Convert.ToInt32((scMain.Panel2.Width - pbFanart.Width) / 2)

            If pbFanart.Image IsNot Nothing AndAlso Master.eSettings.GeneralShowImgDims Then
                g = Graphics.FromImage(pbFanart.Image)
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                strSize = String.Format("{0} x {1}", MainFanart.Image.Width, MainFanart.Image.Height)
                lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                rect = New Rectangle(Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2 - 15), pbFanart.Height - 25, lenSize + 30, 25)
                ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2), pbFanart.Height - 20)
            End If
        Else
            If pbFanartCache.Image IsNot Nothing Then
                pbFanartCache.Image.Dispose()
                pbFanartCache.Image = Nothing
            End If
            If pbFanart.Image IsNot Nothing Then
                pbFanart.Image.Dispose()
                pbFanart.Image = Nothing
            End If
        End If
    End Sub

    Private Sub FillScreenInfoWith_Movie()
        Try
            SuspendLayout()
            If currMovie.Movie.TitleSpecified AndAlso currMovie.Movie.YearSpecified Then
                lblTitle.Text = String.Format("{0} ({1})", currMovie.Movie.Title, currMovie.Movie.Year)
            ElseIf currMovie.Movie.TitleSpecified AndAlso Not currMovie.Movie.YearSpecified Then
                lblTitle.Text = currMovie.Movie.Title
            ElseIf Not currMovie.Movie.TitleSpecified AndAlso currMovie.Movie.YearSpecified Then
                lblTitle.Text = String.Format(Master.eLang.GetString(117, "Unknown Movie ({0})"), currMovie.Movie.Year)
            End If

            If currMovie.Movie.OriginalTitleSpecified AndAlso Not currMovie.Movie.OriginalTitle = currMovie.Movie.Title Then
                lblOriginalTitle.Text = String.Format(String.Concat(Master.eLang.GetString(302, "Original Title"), ": {0}"), currMovie.Movie.OriginalTitle)
            Else
                lblOriginalTitle.Text = String.Empty
            End If

            Try
                If currMovie.Movie.RatingSpecified Then
                    If currMovie.Movie.VotesSpecified Then
                        Dim strRating As String = Double.Parse(currMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Dim strVotes As String = Double.Parse(currMovie.Movie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                    Else
                        Dim strRating As String = Double.Parse(currMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        lblRating.Text = String.Concat(strRating, "/10")
                    End If
                End If
            Catch ex As Exception
                logger.Error(String.Concat("Error: Not valid Rating or Votes (", currMovie.Movie.Rating, " / ", currMovie.Movie.Votes, ")"))
                lblRating.Text = "Error: Please rescrape Rating"
            End Try

            If currMovie.Movie.RuntimeSpecified Then
                lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), If(currMovie.Movie.Runtime.Contains("|"), Microsoft.VisualBasic.Strings.Left(currMovie.Movie.Runtime, currMovie.Movie.Runtime.IndexOf("|")), currMovie.Movie.Runtime)).Trim
            End If

            If currMovie.Movie.Top250Specified AndAlso Integer.TryParse(currMovie.Movie.Top250, 0) AndAlso (Integer.TryParse(currMovie.Movie.Top250, 0) AndAlso Convert.ToInt32(currMovie.Movie.Top250) > 0) Then
                pnlTop250.Visible = True
                lblTop250.Text = currMovie.Movie.Top250
            Else
                pnlTop250.Visible = False
            End If

            txtOutline.Text = currMovie.Movie.Outline
            txtPlot.Text = currMovie.Movie.Plot
            lblTagline.Text = currMovie.Movie.Tagline

            alActors = New List(Of String)

            If currMovie.Movie.ActorsSpecified Then
                pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In currMovie.Movie.Actors
                    If Not String.IsNullOrEmpty(imdbAct.LocalFilePath) AndAlso File.Exists(imdbAct.LocalFilePath) Then
                        If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                            alActors.Add(imdbAct.LocalFilePath)
                        Else
                            alActors.Add("none")
                        End If
                    ElseIf Not String.IsNullOrEmpty(imdbAct.URLOriginal) Then
                        If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                            alActors.Add(imdbAct.URLOriginal)
                        Else
                            alActors.Add("none")
                        End If
                    Else
                        alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                lstActors.SelectedIndex = 0
            End If

            If currMovie.Movie.MPAASpecified Then
                Dim tmpRatingImg As Image = APIXML.GetRatingImage(currMovie.Movie.MPAA)
                If tmpRatingImg IsNot Nothing Then
                    pbMPAA.Image = tmpRatingImg
                    MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(currMovie.Movie.Rating)
            If tmpRating > 0 Then
                BuildStars(tmpRating)
            End If

            If currMovie.Movie.GenresSpecified Then
                createGenreThumbs(currMovie.Movie.Genres)
            End If

            If currMovie.Movie.StudiosSpecified Then
                pbStudio.Image = APIXML.GetStudioImage(currMovie.Movie.Studios.Item(0).ToLower) 'ByDef all images file a lower case
                pbStudio.Tag = currMovie.Movie.Studios.Item(0)
            Else
                pbStudio.Image = APIXML.GetStudioImage("####")
                pbStudio.Tag = String.Empty
            End If

            If clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then
                lblStudio.Text = pbStudio.Tag.ToString
            End If

            If Master.eSettings.MovieScraperMetaDataScan Then
                SetAVImages(APIXML.GetAVImages(currMovie.Movie.FileInfo, currMovie.Filename, False, currMovie.Movie.VideoSource))
                pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
            Else
                pnlInfoIcons.Width = pbStudio.Width + 1
                pbStudio.Left = 0
            End If

            lblDirectors.Text = String.Join(" / ", currMovie.Movie.Directors.ToArray)

            txtIMDBID.Text = currMovie.Movie.IMDBID
            txtTMDBID.Text = currMovie.Movie.TMDBID

            txtFilePath.Text = currMovie.Filename
            txtTrailerPath.Text = If(Not String.IsNullOrEmpty(currMovie.Trailer.LocalFilePath), currMovie.Trailer.LocalFilePath, currMovie.Movie.Trailer)

            lblReleaseDate.Text = currMovie.Movie.ReleaseDate
            txtCertifications.Text = String.Join(" / ", currMovie.Movie.Certifications.ToArray)

            txtMetaData.Text = NFO.FIToString(currMovie.Movie.FileInfo, False)

            FillScreenInfoWithImages()

            InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                SetControlsEnabled(True)
                EnableFilters_Movies(True)
            Else
                dgvMovies.Enabled = True
            End If

            If bDoingSearch_Movies Then
                txtSearchMovies.Focus()
                bDoingSearch_Movies = False
            Else
                dgvMovies.Focus()
            End If


            Application.DoEvents()

            pnlTop.Visible = True
            If pbBanner.Image IsNot Nothing Then pnlBanner.Visible = True
            If pbClearArt.Image IsNot Nothing Then pnlClearArt.Visible = True
            If pbClearLogo.Image IsNot Nothing Then pnlClearLogo.Visible = True
            If pbDiscArt.Image IsNot Nothing Then pnlDiscArt.Visible = True
            If pbFanartSmall.Image IsNot Nothing Then pnlFanartSmall.Visible = True
            If pbLandscape.Image IsNot Nothing Then pnlLandscape.Visible = True
            If pbPoster.Image IsNot Nothing Then pnlPoster.Visible = True
            If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
            For i As Integer = 0 To pnlGenre.Count - 1
                pnlGenre(i).Visible = True
            Next

            SetStatus(currMovie.Filename)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_MovieSet()
        Try
            SuspendLayout()
            If currMovieSet.MovieSet.TitleSpecified AndAlso currMovieSet.MoviesInSet IsNot Nothing AndAlso currMovieSet.MoviesInSet.Count > 0 Then
                lblTitle.Text = String.Format("{0} ({1})", currMovieSet.MovieSet.Title, currMovieSet.MoviesInSet.Count)
            ElseIf currMovieSet.MovieSet.TitleSpecified Then
                lblTitle.Text = currMovieSet.MovieSet.Title
            Else
                lblTitle.Text = String.Empty
            End If

            txtPlot.Text = currMovieSet.MovieSet.Plot

            If currMovieSet.MoviesInSet IsNot Nothing AndAlso currMovieSet.MoviesInSet.Count > 0 Then
                bwLoadMovieSetPosters.WorkerSupportsCancellation = True
                bwLoadMovieSetPosters.RunWorkerAsync()
            End If

            FillScreenInfoWithImages()

            InfoCleared = False

            If Not bwMovieSetScraper.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                SetControlsEnabled(True)
                EnableFilters_MovieSets(True)
            Else
                dgvMovieSets.Enabled = True
            End If

            If bDoingSearch_MovieSets Then
                txtSearchMovieSets.Focus()
                bDoingSearch_MovieSets = False
            Else
                dgvMovieSets.Focus()
            End If


            Application.DoEvents()

            pnlTop.Visible = True
            If pbBanner.Image IsNot Nothing Then pnlBanner.Visible = True
            If pbClearArt.Image IsNot Nothing Then pnlClearArt.Visible = True
            If pbClearLogo.Image IsNot Nothing Then pnlClearLogo.Visible = True
            If pbDiscArt.Image IsNot Nothing Then pnlDiscArt.Visible = True
            If pbFanartSmall.Image IsNot Nothing Then pnlFanartSmall.Visible = True
            If pbLandscape.Image IsNot Nothing Then pnlLandscape.Visible = True
            If pbPoster.Image IsNot Nothing Then pnlPoster.Visible = True
            If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
            For i As Integer = 0 To pnlGenre.Count - 1
                pnlGenre(i).Visible = True
            Next
            'Me.SetStatus(Master.currMovie.Filename)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVEpisode()
        Try
            SuspendLayout()
            lblTitle.Text = If(String.IsNullOrEmpty(currTV.Filename), String.Concat(currTV.TVEpisode.Title, " ", Master.eLang.GetString(689, "[MISSING]")), currTV.TVEpisode.Title)
            txtPlot.Text = currTV.TVEpisode.Plot
            lblDirectors.Text = String.Join(" / ", currTV.TVEpisode.Directors.ToArray)
            txtFilePath.Text = currTV.Filename
            lblRuntime.Text = String.Format(Master.eLang.GetString(647, "Aired: {0}"), If(currTV.TVEpisode.AiredSpecified, Date.Parse(currTV.TVEpisode.Aired).ToShortDateString, "?"))

            Try
                If currTV.TVEpisode.RatingSpecified Then
                    If currTV.TVEpisode.VotesSpecified Then
                        Dim strRating As String = Double.Parse(currTV.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Dim strVotes As String = Double.Parse(currTV.TVEpisode.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                    Else
                        Dim strRating As String = Double.Parse(currTV.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        lblRating.Text = String.Concat(strRating, "/10")
                    End If
                End If
            Catch ex As Exception
                logger.Error(String.Concat("Error: Not valid Rating or Votes (", currTV.TVEpisode.Rating, " / ", currTV.TVEpisode.Votes, ")"))
                lblRating.Text = "Error: Please rescrape Rating"
            End Try

            lblTagline.Text = String.Format(Master.eLang.GetString(648, "Season: {0}, Episode: {1}"),
                            If(String.IsNullOrEmpty(currTV.TVEpisode.Season.ToString), "?", currTV.TVEpisode.Season.ToString),
                            If(String.IsNullOrEmpty(currTV.TVEpisode.Episode.ToString), "?", currTV.TVEpisode.Episode.ToString))

            alActors = New List(Of String)

            If currTV.TVEpisode.ActorsSpecified Then
                pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In currTV.TVEpisode.Actors
                    If Not String.IsNullOrEmpty(imdbAct.LocalFilePath) AndAlso File.Exists(imdbAct.LocalFilePath) Then
                        If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                            alActors.Add(imdbAct.LocalFilePath)
                        Else
                            alActors.Add("none")
                        End If
                    ElseIf Not String.IsNullOrEmpty(imdbAct.URLOriginal) Then
                        If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                            alActors.Add(imdbAct.URLOriginal)
                        Else
                            alActors.Add("none")
                        End If
                    Else
                        alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                lstActors.SelectedIndex = 0
            End If

            If currTV.TVShow.MPAASpecified Then
                Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(currTV.TVShow.MPAA)
                If tmpRatingImg IsNot Nothing Then
                    pbMPAA.Image = tmpRatingImg
                    MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(currTV.TVEpisode.Rating)
            If tmpRating > 0 Then
                BuildStars(tmpRating)
            End If

            If currTV.TVShow.GenresSpecified Then
                createGenreThumbs(currTV.TVShow.Genres)
            End If

            If currTV.TVShow.StudiosSpecified Then
                pbStudio.Image = APIXML.GetStudioImage(currTV.TVShow.Studios.Item(0).ToLower) 'ByDef all images file a lower case
                pbStudio.Tag = currTV.TVShow.Studios.Item(0)
            Else
                pbStudio.Image = APIXML.GetStudioImage("####")
                pbStudio.Tag = String.Empty
            End If
            If clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then
                lblStudio.Text = pbStudio.Tag.ToString
            End If
            If Master.eSettings.TVScraperMetaDataScan AndAlso Not String.IsNullOrEmpty(currTV.Filename) Then
                SetAVImages(APIXML.GetAVImages(currTV.TVEpisode.FileInfo, currTV.Filename, True, currTV.TVEpisode.VideoSource))
                pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
            Else
                pnlInfoIcons.Width = pbStudio.Width + 1
                pbStudio.Left = 0
            End If

            txtMetaData.Text = NFO.FIToString(currTV.TVEpisode.FileInfo, True)

            FillScreenInfoWithImages()

            InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                SetControlsEnabled(True)
                dgvTVEpisodes.Focus()
            Else
                dgvTVEpisodes.Enabled = True
                dgvTVSeasons.Enabled = True
                dgvTVShows.Enabled = True
                dgvTVEpisodes.Focus()
            End If

            Application.DoEvents()

            pnlTop.Visible = True
            If pbFanartSmall.Image IsNot Nothing Then pnlFanartSmall.Visible = True
            If pbPoster.Image IsNot Nothing Then pnlPoster.Visible = True
            If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
            For i As Integer = 0 To pnlGenre.Count - 1
                pnlGenre(i).Visible = True
            Next

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVSeason()
        SuspendLayout()
        If currTV.TVShow.TitleSpecified Then
            lblTitle.Text = currTV.TVShow.Title
        End If

        If currTV.TVShow.OriginalTitleSpecified AndAlso Not currTV.TVShow.OriginalTitle = currTV.TVShow.Title Then
            lblOriginalTitle.Text = String.Format(String.Concat(Master.eLang.GetString(302, "Original Title"), ": {0}"), currTV.TVShow.OriginalTitle)
        Else
            lblOriginalTitle.Text = String.Empty
        End If

        txtPlot.Text = currTV.TVSeason.Plot
        lblRuntime.Text = String.Format(Master.eLang.GetString(645, "Premiered: {0}"), If(currTV.TVShow.PremieredSpecified, Date.Parse(currTV.TVShow.Premiered).ToShortDateString, "?"))

        Try
            If currTV.TVShow.RatingSpecified Then
                If currTV.TVShow.VotesSpecified Then
                    Dim strRating As String = Double.Parse(currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    Dim strVotes As String = Double.Parse(currTV.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                Else
                    Dim strRating As String = Double.Parse(currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10")
                End If
            End If
        Catch ex As Exception
            logger.Error(String.Concat("Error: Not valid Rating or Votes (", currTV.TVShow.Rating, " / ", currTV.TVShow.Votes, ")"))
            lblRating.Text = "Error: Please rescrape Rating"
        End Try

        alActors = New List(Of String)

        If currTV.TVShow.ActorsSpecified Then
            pbActors.Image = My.Resources.actor_silhouette
            For Each imdbAct As MediaContainers.Person In currTV.TVShow.Actors
                If Not String.IsNullOrEmpty(imdbAct.LocalFilePath) AndAlso File.Exists(imdbAct.LocalFilePath) Then
                    If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(imdbAct.LocalFilePath)
                    Else
                        alActors.Add("none")
                    End If
                ElseIf Not String.IsNullOrEmpty(imdbAct.URLOriginal) Then
                    If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(imdbAct.URLOriginal)
                    Else
                        alActors.Add("none")
                    End If
                Else
                    alActors.Add("none")
                End If

                If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                    lstActors.Items.Add(imdbAct.Name.Trim)
                Else
                    lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                End If
            Next
            lstActors.SelectedIndex = 0
        End If

        If currTV.TVShow.MPAASpecified Then
            Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(currTV.TVShow.MPAA)
            If tmpRatingImg IsNot Nothing Then
                pbMPAA.Image = tmpRatingImg
                MoveMPAA()
            End If
        End If

        Dim tmpRating As Single = NumUtils.ConvertToSingle(currTV.TVShow.Rating)
        If tmpRating > 0 Then
            BuildStars(tmpRating)
        End If

        If currTV.TVShow.Genres.Count > 0 Then
            createGenreThumbs(currTV.TVShow.Genres)
        End If

        If currTV.TVShow.StudiosSpecified Then
            pbStudio.Image = APIXML.GetStudioImage(currTV.TVShow.Studios.Item(0).ToLower) 'ByDef all images file a lower case
            pbStudio.Tag = currTV.TVShow.Studios.Item(0)
        Else
            pbStudio.Image = APIXML.GetStudioImage("####")
            pbStudio.Tag = String.Empty
        End If

        pnlInfoIcons.Width = pbStudio.Width + 1
        pbStudio.Left = 0

        FillScreenInfoWithImages()

        InfoCleared = False

        If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
            SetControlsEnabled(True)
            dgvTVSeasons.Focus()
        Else
            dgvTVEpisodes.Enabled = True
            dgvTVSeasons.Enabled = True
            dgvTVShows.Enabled = True
            dgvTVSeasons.Focus()
        End If

        Application.DoEvents()

        pnlTop.Visible = True
        If pbBanner.Image IsNot Nothing Then pnlBanner.Visible = True
        If pbFanartSmall.Image IsNot Nothing Then pnlFanartSmall.Visible = True
        If pbLandscape.Image IsNot Nothing Then pnlLandscape.Visible = True
        If pbPoster.Image IsNot Nothing Then pnlPoster.Visible = True
        If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
        For i As Integer = 0 To pnlGenre.Count - 1
            pnlGenre(i).Visible = True
        Next

        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVShow()
        Try
            SuspendLayout()
            If currTV.TVShow.TitleSpecified Then
                lblTitle.Text = currTV.TVShow.Title
            End If

            If currTV.TVShow.OriginalTitleSpecified AndAlso Not currTV.TVShow.OriginalTitle = currTV.TVShow.Title Then
                lblOriginalTitle.Text = String.Format(String.Concat(Master.eLang.GetString(302, "Original Title"), ": {0}"), currTV.TVShow.OriginalTitle)
            Else
                lblOriginalTitle.Text = String.Empty
            End If

            txtPlot.Text = currTV.TVShow.Plot
            lblRuntime.Text = String.Format(Master.eLang.GetString(645, "Premiered: {0}"), If(currTV.TVShow.PremieredSpecified, Date.Parse(currTV.TVShow.Premiered).ToShortDateString, "?"))

            Try
                If currTV.TVShow.RatingSpecified Then
                    If currTV.TVShow.VotesSpecified Then
                        Dim strRating As String = Double.Parse(currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Dim strVotes As String = Double.Parse(currTV.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                    Else
                        Dim strRating As String = Double.Parse(currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        lblRating.Text = String.Concat(strRating, "/10")
                    End If
                End If
            Catch ex As Exception
                logger.Error(String.Concat("Error: Not valid Rating or Votes (", currTV.TVShow.Rating, " / ", currTV.TVShow.Votes, ")"))
                lblRating.Text = "Error: Please rescrape Rating"
            End Try

            alActors = New List(Of String)

            If currTV.TVShow.ActorsSpecified Then
                pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In currTV.TVShow.Actors
                    If Not String.IsNullOrEmpty(imdbAct.LocalFilePath) AndAlso File.Exists(imdbAct.LocalFilePath) Then
                        If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                            alActors.Add(imdbAct.LocalFilePath)
                        Else
                            alActors.Add("none")
                        End If
                    ElseIf Not String.IsNullOrEmpty(imdbAct.URLOriginal) Then
                        If Not imdbAct.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                            alActors.Add(imdbAct.URLOriginal)
                        Else
                            alActors.Add("none")
                        End If
                    Else
                        alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                lstActors.SelectedIndex = 0
            End If

            If currTV.TVShow.MPAASpecified Then
                Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(currTV.TVShow.MPAA)
                If tmpRatingImg IsNot Nothing Then
                    pbMPAA.Image = tmpRatingImg
                    MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(currTV.TVShow.Rating)
            If tmpRating > 0 Then
                BuildStars(tmpRating)
            End If

            If currTV.TVShow.Genres.Count > 0 Then
                createGenreThumbs(currTV.TVShow.Genres)
            End If

            If currTV.TVShow.StudiosSpecified Then
                pbStudio.Image = APIXML.GetStudioImage(currTV.TVShow.Studios.Item(0).ToLower) 'ByDef all images file a lower case
                pbStudio.Tag = currTV.TVShow.Studios.Item(0)
            Else
                pbStudio.Image = APIXML.GetStudioImage("####")
                pbStudio.Tag = String.Empty
            End If

            pnlInfoIcons.Width = pbStudio.Width + 1
            pbStudio.Left = 0

            FillScreenInfoWithImages()

            InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                SetControlsEnabled(True)
                EnableFilters_Shows(True)
            Else
                dgvTVEpisodes.Enabled = True
                dgvTVSeasons.Enabled = True
                dgvTVShows.Enabled = True
            End If

            If bDoingSearch_TVShows Then
                txtSearchShows.Focus()
                bDoingSearch_TVShows = False
            Else
                dgvTVShows.Focus()
            End If

            Application.DoEvents()

            pnlTop.Visible = True
            If pbBanner.Image IsNot Nothing Then pnlBanner.Visible = True
            If pbCharacterArt.Image IsNot Nothing Then pnlCharacterArt.Visible = True
            If pbClearArt.Image IsNot Nothing Then pnlClearArt.Visible = True
            If pbClearLogo.Image IsNot Nothing Then pnlClearLogo.Visible = True
            If pbFanartSmall.Image IsNot Nothing Then pnlFanartSmall.Visible = True
            If pbLandscape.Image IsNot Nothing Then pnlLandscape.Visible = True
            If pbPoster.Image IsNot Nothing Then pnlPoster.Visible = True
            If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
            For i As Integer = 0 To pnlGenre.Count - 1
                pnlGenre(i).Visible = True
            Next

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        ResumeLayout()
    End Sub

    Private Sub FillSeasons(ByVal ShowID As Long)
        bsTVSeasons.DataSource = Nothing
        dgvTVSeasons.DataSource = Nothing
        bsTVEpisodes.DataSource = Nothing
        dgvTVEpisodes.DataSource = Nothing

        Application.DoEvents()

        dgvTVSeasons.Enabled = False

        If Master.eSettings.TVDisplayMissingEpisodes Then
            Master.DB.FillDataTable(dtTVSeasons, String.Concat("SELECT * FROM seasonslist WHERE idShow = ", ShowID, " ORDER BY Season;"))
        Else
            Master.DB.FillDataTable(dtTVSeasons, String.Concat("SELECT DISTINCT seasonslist.* ",
                                                                "FROM seasonslist ",
                                                                "LEFT OUTER JOIN episodelist ON (seasonslist.idShow = episodelist.idShow) AND (seasonslist.Season = episodelist.Season) ",
                                                                "WHERE seasonslist.idShow = ", ShowID, " AND (episodelist.Missing = 0 OR seasonslist.Season = 999) ",
                                                                "ORDER BY seasonslist.Season;"))
        End If

        If dtTVSeasons.Rows.Count > 0 Then

            With Me
                .bsTVSeasons.DataSource = .dtTVSeasons
                .dgvTVSeasons.DataSource = .bsTVSeasons

                If dgvTVSeasons.Columns.Count > 0 Then
                    Try
                        If Master.eSettings.TVGeneralSeasonListSorting.Count > 0 Then
                            For Each mColumn In Master.eSettings.TVGeneralSeasonListSorting
                                dgvTVSeasons.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                            Next
                        End If
                    Catch ex As Exception
                        logger.Warn("default list for season list sorting has been loaded")
                        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVSeasonListSorting, True)
                        If Master.eSettings.TVGeneralSeasonListSorting.Count > 0 Then
                            For Each mColumn In Master.eSettings.TVGeneralSeasonListSorting
                                dgvTVSeasons.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                            Next
                        End If
                    End Try
                End If

                For i As Integer = 0 To .dgvTVSeasons.Columns.Count - 1
                    .dgvTVSeasons.Columns(i).Visible = False
                Next

                .dgvTVSeasons.Columns("BannerPath").Width = 20
                .dgvTVSeasons.Columns("BannerPath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("BannerPath").ReadOnly = True
                .dgvTVSeasons.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("BannerPath").Visible = Not CheckColumnHide_TVSeasons("BannerPath")
                .dgvTVSeasons.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                .dgvTVSeasons.Columns("Episodes").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                .dgvTVSeasons.Columns("Episodes").MinimumWidth = 30
                .dgvTVSeasons.Columns("Episodes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .dgvTVSeasons.Columns("Episodes").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("Episodes").ReadOnly = True
                .dgvTVSeasons.Columns("Episodes").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("Episodes").Visible = Not CheckColumnHide_TVSeasons("Episodes")
                .dgvTVSeasons.Columns("Episodes").ToolTipText = Master.eLang.GetString(682, "Episodes")
                .dgvTVSeasons.Columns("Episodes").HeaderText = String.Empty
                .dgvTVSeasons.Columns("FanartPath").Width = 20
                .dgvTVSeasons.Columns("FanartPath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("FanartPath").ReadOnly = True
                .dgvTVSeasons.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("FanartPath").Visible = Not CheckColumnHide_TVSeasons("FanartPath")
                .dgvTVSeasons.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                .dgvTVSeasons.Columns("HasWatched").Width = 20
                .dgvTVSeasons.Columns("HasWatched").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("HasWatched").ReadOnly = True
                .dgvTVSeasons.Columns("HasWatched").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("HasWatched").Visible = Not CheckColumnHide_TVSeasons("HasWatched")
                .dgvTVSeasons.Columns("HasWatched").ToolTipText = Master.eLang.GetString(981, "Watched")
                .dgvTVSeasons.Columns("LandscapePath").Width = 20
                .dgvTVSeasons.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("LandscapePath").ReadOnly = True
                .dgvTVSeasons.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("LandscapePath").Visible = Not CheckColumnHide_TVSeasons("LandscapePath")
                .dgvTVSeasons.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                .dgvTVSeasons.Columns("PosterPath").Width = 20
                .dgvTVSeasons.Columns("PosterPath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("PosterPath").ReadOnly = True
                .dgvTVSeasons.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("PosterPath").Visible = Not CheckColumnHide_TVSeasons("PosterPath")
                .dgvTVSeasons.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                .dgvTVSeasons.Columns("SeasonText").Resizable = DataGridViewTriState.True
                .dgvTVSeasons.Columns("SeasonText").ReadOnly = True
                .dgvTVSeasons.Columns("SeasonText").MinimumWidth = 83
                .dgvTVSeasons.Columns("SeasonText").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("SeasonText").Visible = True
                .dgvTVSeasons.Columns("SeasonText").ToolTipText = Master.eLang.GetString(650, "Season")
                .dgvTVSeasons.Columns("SeasonText").HeaderText = Master.eLang.GetString(650, "Season")

                .dgvTVSeasons.Columns("idSeason").ValueType = GetType(Int32)
                .dgvTVSeasons.Columns("idShow").ValueType = GetType(Int32)

                If Master.isWindows Then .dgvTVSeasons.Columns("SeasonText").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                ResizeTVLists(.dgvTVSeasons.Columns("SeasonText").Index)

                .dgvTVSeasons.Sort(.dgvTVSeasons.Columns("SeasonText"), System.ComponentModel.ListSortDirection.Ascending)

                FillEpisodes(ShowID, Convert.ToInt32(.dgvTVSeasons.Item("Season", 0).Value))
            End With
        End If

        dgvTVSeasons.Enabled = True
    End Sub

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        logger.Info("====Ember Media Manager exiting====")
    End Sub
    ''' <summary>
    ''' The FormClosing event has been called, so prepare the form to shut down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            Dim doSave As Boolean = True

            SetControlsEnabled(False, True)
            EnableFilters_Movies(False)
            EnableFilters_MovieSets(False)
            EnableFilters_Shows(False)

            If fScanner.IsBusy OrElse Master.isCL Then
                doSave = False
            End If

            If fScanner.IsBusy Then fScanner.Cancel()
            If bwLoadMovieInfo.IsBusy Then bwLoadMovieInfo.CancelAsync()
            If bwLoadMovieSetInfo.IsBusy Then bwLoadMovieSetInfo.CancelAsync()
            If bwLoadMovieSetPosters.IsBusy Then bwLoadMovieSetPosters.CancelAsync()
            If bwLoadShowInfo.IsBusy Then bwLoadShowInfo.CancelAsync()
            If bwLoadSeasonInfo.IsBusy Then bwLoadSeasonInfo.CancelAsync()
            If bwLoadEpInfo.IsBusy Then bwLoadEpInfo.CancelAsync()
            If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
            If bwReload_Movies.IsBusy Then bwReload_Movies.CancelAsync()
            If bwCleanDB.IsBusy Then bwCleanDB.CancelAsync()
            If bwMovieScraper.IsBusy Then bwMovieScraper.CancelAsync()

            lblCanceling.Text = Master.eLang.GetString(99, "Canceling All Processes...")
            btnCancel.Visible = False
            lblCanceling.Visible = True
            prbCanceling.Visible = True
            pnlCancel.Visible = True
            Refresh()

            If ModulesManager.Instance.QueryAnyGenericIsBusy Then
                If MessageBox.Show("One or more modules are busy. Do you want to wait until all tasks are finished?", "One or more external Modules are busy", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    While ModulesManager.Instance.QueryAnyGenericIsBusy
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                End If
            End If

            While fScanner.IsBusy OrElse bwLoadMovieInfo.IsBusy _
            OrElse bwLoadMovieSetInfo.IsBusy OrElse bwDownloadPic.IsBusy OrElse bwMovieScraper.IsBusy _
            OrElse bwReload_Movies.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse bwCleanDB.IsBusy _
            OrElse bwLoadShowInfo.IsBusy OrElse bwLoadEpInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy _
            OrElse bwLoadMovieSetPosters.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If doSave Then Master.DB.ClearNew()

            If Not Master.isCL Then
                Master.DB.Close_MyVideos()
            End If

            Master.eSettings.Version = String.Format("r{0}", My.Application.Info.Version.Revision)

            If Not Master.isCL AndAlso Not WindowState = FormWindowState.Minimized Then
                'disable filters to proper save TV Show/Season SplitterDistance
                pnlFilter_Movies.Visible = False
                pnlFilter_MovieSets.Visible = False
                pnlFilter_Shows.Visible = False
                Application.DoEvents()
                Master.eSettings.GeneralFilterPanelIsRaisedMovie = FilterPanelIsRaised_Movie
                Master.eSettings.GeneralFilterPanelIsRaisedMovieSet = FilterPanelIsRaised_MovieSet
                Master.eSettings.GeneralFilterPanelIsRaisedTVShow = FilterPanelIsRaised_TVShow
                Master.eSettings.GeneralInfoPanelStateMovie = InfoPanelState_Movie
                Master.eSettings.GeneralInfoPanelStateMovieSet = InfoPanelState_MovieSet
                Master.eSettings.GeneralInfoPanelStateTVShow = InfoPanelState_TVShow
                Master.eSettings.GeneralSplitterDistanceMain = scMain.SplitterDistance
                Master.eSettings.GeneralSplitterDistanceTVSeason = scTVSeasonsEpisodes.SplitterDistance
                Master.eSettings.GeneralSplitterDistanceTVShow = scTV.SplitterDistance
                Master.eSettings.GeneralWindowLoc = Location
                Master.eSettings.GeneralWindowSize = Size
                Master.eSettings.GeneralWindowState = WindowState
                Master.eSettings.Save()
            End If

        Catch ex As Exception
            ' If we got here, then some of the above not run. Application.Exit can not be used. 
            ' Because Exit will dispose object that are in use by BackgroundWorkers
            ' If any BackgroundWorker still running will raise exception 
            ' "Collection was modified; enumeration operation may not execute."
            ' Application.Exit()
        End Try
    End Sub
    ''' <summary>
    ''' The form is loading. This method occurs before the form is actually visible.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Visible = False

        If Master.isWindows Then 'Dam mono on MacOSX don't have trayicon implemented yet
            TrayIcon = New System.Windows.Forms.NotifyIcon(components)
            TrayIcon.Icon = Icon
            TrayIcon.ContextMenuStrip = cmnuTray
            TrayIcon.Text = "Ember Media Manager"
            TrayIcon.Visible = True
        End If

        bwCheckVersion.RunWorkerAsync()

        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(854, "Basic setup"))

        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyResolveEventHandler

        clsAdvancedSettings.Start()

        'Create Modules Folders
        Dim sPath = String.Concat(Functions.AppPath, "Modules")
        If Not Directory.Exists(sPath) Then
            Directory.CreateDirectory(sPath)
        End If

        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(855, "Creating default options..."))
        Functions.CreateDefaultOptions()

        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading database..."))
        Master.DB.Connect_MyVideos()
        Master.DB.LoadAllGenres()
        Master.DB.Load_Sources_Movie()
        Master.DB.Load_Sources_TVShow()
        Master.DB.Load_ExcludeDirs()

        tpMovies.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(36, "Movies"), .ContentType = Enums.ContentType.Movie, .DefaultList = "movielist"}
        tpMovieSets.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(366, "Sets"), .ContentType = Enums.ContentType.MovieSet, .DefaultList = "setslist"}
        tpTVShows.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(653, "TV Shows"), .ContentType = Enums.ContentType.TV, .DefaultList = "tvshowlist"}
        ModulesManager.Instance.RuntimeObjects.MediaTabSelected = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)

        ModulesManager.Instance.RuntimeObjects.DelegateLoadMedia(AddressOf LoadMedia)
        ModulesManager.Instance.RuntimeObjects.DelegateOpenImageViewer(AddressOf OpenImageViewer)
        ModulesManager.Instance.RuntimeObjects.MainMenu = mnuMain
        ModulesManager.Instance.RuntimeObjects.MainTabControl = tcMain
        ModulesManager.Instance.RuntimeObjects.MainToolStrip = tsMain
        ModulesManager.Instance.RuntimeObjects.MediaListMovies = dgvMovies
        ModulesManager.Instance.RuntimeObjects.MediaListMovieSets = dgvMovieSets
        ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes = dgvTVEpisodes
        ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons = dgvTVSeasons
        ModulesManager.Instance.RuntimeObjects.MediaListTVShows = dgvTVShows
        ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList = cmnuMovie
        ModulesManager.Instance.RuntimeObjects.ContextMenuMovieSetList = cmnuMovieSet
        ModulesManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList = cmnuEpisode
        ModulesManager.Instance.RuntimeObjects.ContextMenuTVSeasonList = cmnuSeason
        ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList = cmnuShow
        ModulesManager.Instance.RuntimeObjects.TrayMenu = cmnuTray

        'start loading modules in background
        ModulesManager.Instance.LoadAllModules()

        If Not Master.isCL Then
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(857, "Creating GUI..."))
        End If

        'setup some dummies so we don't get exceptions when resizing form/info panel
        ReDim Preserve pnlGenre(0)
        ReDim Preserve pbGenre(0)
        pnlGenre(0) = New Panel()
        pbGenre(0) = New PictureBox()

        AddHandler fCommandLine.TaskEvent, AddressOf TaskRunCallBack
        AddHandler fScanner.ScannerUpdated, AddressOf ScannerUpdated
        AddHandler fScanner.ScanningCompleted, AddressOf ScanningCompleted
        AddHandler fTaskManager.ProgressUpdate, AddressOf TaskManagerProgressUpdate
        'AddHandler fTaskManager.TaskManagerDone, AddressOf ScanningCompleted
        AddHandler ModulesManager.Instance.GenericEvent, AddressOf GenericRunCallBack
        AddHandler Master.DB.GenericEvent, AddressOf GenericRunCallBack

        Functions.DGVDoubleBuffer(dgvMovies)
        Functions.DGVDoubleBuffer(dgvMovieSets)
        Functions.DGVDoubleBuffer(dgvTVShows)
        Functions.DGVDoubleBuffer(dgvTVSeasons)
        Functions.DGVDoubleBuffer(dgvTVEpisodes)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)

        If TypeOf tsMain.Renderer Is ToolStripProfessionalRenderer Then
            CType(tsMain.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
        End If

        If Not Directory.Exists(Master.TempPath) Then Directory.CreateDirectory(Master.TempPath)

        While Not ModulesManager.Instance.ModulesLoaded()
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(856, "Loading modules..."))
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While


        RemoveHandler dgvMovies.RowsAdded, AddressOf dgvMovies_RowsAdded
        RemoveHandler dgvMovieSets.RowsAdded, AddressOf dgvMovieSets_RowsAdded
        RemoveHandler dgvTVShows.RowsAdded, AddressOf dgvTVShows_RowsAdded
        FillList(True, True, True)
        AddHandler dgvMovies.RowsAdded, AddressOf dgvMovies_RowsAdded
        AddHandler dgvMovieSets.RowsAdded, AddressOf dgvMovieSets_RowsAdded
        AddHandler dgvTVShows.RowsAdded, AddressOf dgvTVShows_RowsAdded

        If Master.isCL Then ' Command Line
            LoadWithCommandLine(Master.appArgs)
        Else 'Regular Run (GUI)
            LoadWithGUI()
        End If
        Master.fLoading.Close()
    End Sub
    ''' <summary>
    ''' Performs startup routines specific to being initiated by the command line
    ''' </summary>
    ''' <param name="appArgs">Command line arguments. Must NOT be empty!</param>
    ''' <remarks></remarks>
    Private Sub LoadWithCommandLine(ByVal appArgs As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs)
        Dim Args() As String = appArgs.CommandLine.ToArray

        fCommandLine.RunCommandLine(Args)

        While Not TaskList.Count = 0 OrElse Not TasksDone
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Master.fLoading.Close()
        Close()
    End Sub
    ''' <summary>
    ''' Performs startup routines specific to being initiated as a GUI application (user interaction intended)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadWithGUI()
        Try
            logger.Trace("LoadWithGUI()")
            'If Master.eSettings.CheckUpdates Then
            '    If Functions.CheckNeedUpdate() Then
            '        Using dNewVer As New dlgNewVersion
            '            fLoading.Hide()
            '            If dNewVer.ShowDialog() = Windows.Forms.DialogResult.Abort Then
            '                tmrAppExit.Enabled = True
            '                CloseApp = True
            '            End If
            '        End Using
            '    End If
            'End If

            ' Not localized as is the Assembly file version
            'Dim VersionNumberO As String = System.String.Format("{0}.{1}.{2}.{3}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

            If Not CloseApp Then

                'moved to frmMain_Load
                'Me.tpMovies.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(36, "Movies"), .ContentType = Enums.Content_Type.Movie, .DefaultList = "movielist"}
                'Me.tpMovieSets.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(366, "Sets"), .ContentType = Enums.Content_Type.MovieSet, .DefaultList = "setslist"}
                'Me.tpTVShows.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(653, "TV Shows"), .ContentType = Enums.Content_Type.TV, .DefaultList = "tvshowlist"}
                'ModulesManager.Instance.RuntimeObjects.MediaTabSelected = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)

                SetUp(True)

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(863, "Positioning controls..."))
                Location = Master.eSettings.GeneralWindowLoc
                Size = Master.eSettings.GeneralWindowSize
                WindowState = Master.eSettings.GeneralWindowState
                If Not WindowState = FormWindowState.Minimized Then
                    Master.AppPos = Bounds
                End If

                'SplitterDistance
                Try ' On error just ignore this a let it use default
                    scMain.SplitterDistance = Master.eSettings.GeneralSplitterDistanceMain
                    scTV.SplitterDistance = Master.eSettings.GeneralSplitterDistanceTVShow
                    scTVSeasonsEpisodes.SplitterDistance = Master.eSettings.GeneralSplitterDistanceTVSeason
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try

                'Info panels
                InfoPanelState_Movie = Master.eSettings.GeneralInfoPanelStateMovie
                Select Case InfoPanelState_Movie
                    Case 0
                        pnlInfoPanel.Height = 25
                        btnDown.Enabled = False
                        btnMid.Enabled = True
                        btnUp.Enabled = True
                    Case 1
                        pnlInfoPanel.Height = IPMid
                        btnMid.Enabled = False
                        btnDown.Enabled = True
                        btnUp.Enabled = True
                    Case 2
                        pnlInfoPanel.Height = IPUp
                        btnUp.Enabled = False
                        btnDown.Enabled = True
                        btnMid.Enabled = True
                End Select

                InfoPanelState_MovieSet = Master.eSettings.GeneralInfoPanelStateMovieSet
                InfoPanelState_TVShow = Master.eSettings.GeneralInfoPanelStateTVShow

                'Filter panels
                FilterPanelIsRaised_Movie = Master.eSettings.GeneralFilterPanelIsRaisedMovie
                If FilterPanelIsRaised_Movie Then
                    'Me.pnlFilter_Movies.Height = Functions.Quantize(Me.gbFilterSpecific_Movies.Height + Me.lblFilter_Movies.Height + 15, 5)
                    pnlFilter_Movies.AutoSize = True
                    btnFilterDown_Movies.Enabled = True
                    btnFilterUp_Movies.Enabled = False
                Else
                    'Me.pnlFilter_Movies.Height = 25
                    pnlFilter_Movies.AutoSize = False
                    pnlFilter_Movies.Height = pnlFilterTop_Movies.Height
                    btnFilterDown_Movies.Enabled = False
                    btnFilterUp_Movies.Enabled = True
                End If

                FilterPanelIsRaised_MovieSet = Master.eSettings.GeneralFilterPanelIsRaisedMovieSet
                If FilterPanelIsRaised_MovieSet Then
                    'Me.pnlFilter_MovieSets.Height = Functions.Quantize(Me.gbFilterSpecific_MovieSets.Height + Me.lblFilter_MovieSets.Height + 15, 5)
                    pnlFilter_MovieSets.AutoSize = True
                    btnFilterDown_MovieSets.Enabled = True
                    btnFilterUp_MovieSets.Enabled = False
                Else
                    'Me.pnlFilter_MovieSets.Height = 25
                    pnlFilter_MovieSets.AutoSize = False
                    pnlFilter_MovieSets.Height = pnlFilterTop_MovieSets.Height
                    btnFilterDown_MovieSets.Enabled = False
                    btnFilterUp_MovieSets.Enabled = True
                End If

                FilterPanelIsRaised_TVShow = Master.eSettings.GeneralFilterPanelIsRaisedTVShow
                If FilterPanelIsRaised_TVShow Then
                    'Me.pnlFilter_Shows.Height = Functions.Quantize(Me.gbFilterSpecific_Shows.Height + Me.lblFilter_Shows.Height + 15, 5)
                    pnlFilter_Shows.AutoSize = True
                    btnFilterDown_Shows.Enabled = True
                    btnFilterUp_Shows.Enabled = False
                Else
                    'Me.pnlFilter_Shows.Height = 25
                    pnlFilter_Shows.AutoSize = False
                    pnlFilter_Shows.Height = pnlFilterTop_Shows.Height
                    btnFilterDown_Shows.Enabled = False
                    btnFilterUp_Shows.Enabled = True
                End If

                pnlFilter_Movies.Visible = True

                'MenuItem Tags for better Enable/Disable handling
                mnuMainToolsCleanDB.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfNoMovieSets = True, .IfNoTVShows = True, .IfTabTVShows = True}
                mnuMainToolsClearCache.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfNoMovieSets = True, .IfNoTVShows = True, .IfTabTVShows = True}
                mnuMainToolsOfflineHolder.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfNoMovies = True}
                mnuMainToolsReloadMovies.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsReloadMovieSets.Tag = New Structures.ModulesMenus With {.ForMovieSets = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsReloadTVShows.Tag = New Structures.ModulesMenus With {.ForTVShows = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsRewriteMovieContent.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsSortFiles.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(1165, "Initializing Main Form. Please wait..."))

                Application.DoEvents()

                Visible = True

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(864, "Setting menus..."))
                SetMenus(True)
                Functions.GetListOfSources()
                cmnuTrayExit.Enabled = True
                cmnuTraySettings.Enabled = True
                mnuMainEdit.Enabled = True
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub frmMain_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        If Not WindowState = FormWindowState.Minimized Then
            Master.AppPos = Bounds
        End If
    End Sub
    ''' <summary>
    ''' The form has been resized, so re-position those controls that need to be re-located
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Created Then
            If Not WindowState = FormWindowState.Minimized Then
                Master.AppPos = Bounds
            End If
            MoveMPAA()
            MoveGenres()
            ImageUtils.ResizePB(pbFanart, pbFanartCache, scMain.Panel2.Height - 90, scMain.Panel2.Width)
            pbFanart.Left = Convert.ToInt32((scMain.Panel2.Width - pbFanart.Width) / 2)
            pnlNoInfo.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), Convert.ToInt32((scMain.Panel2.Height - pnlNoInfo.Height) / 2))
            pnlCancel.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), 124)
            pnlFilterCountries_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterCountry_Movies.Left + 1,
                                                              (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterCountry_Movies.Top) - pnlFilterCountries_Movies.Height)
            pnlFilterGenres_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterGenre_Movies.Left + 1,
                                                           (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterGenre_Movies.Top) - pnlFilterGenres_Movies.Height)
            pnlFilterGenres_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterGenre_Shows.Left + 1,
                                                          (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterGenre_Shows.Top) - pnlFilterGenres_Shows.Height)
            pnlFilterDataFields_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + gbFilterDataField_Movies.Left + tblFilterDataField_Movies.Left + txtFilterDataField_Movies.Left + 1,
                                                               (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + gbFilterDataField_Movies.Top + tblFilterDataField_Movies.Top + txtFilterDataField_Movies.Top) - pnlFilterDataFields_Movies.Height)
            pnlFilterMissingItems_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterGeneral_Movies.Left + tblFilterGeneral_Movies.Left + btnFilterMissing_Movies.Left + 1,
                                                                 (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterGeneral_Movies.Top + tblFilterGeneral_Movies.Top + btnFilterMissing_Movies.Top) - pnlFilterMissingItems_Movies.Height)
            pnlFilterMissingItems_MovieSets.Location = New Point(pnlFilter_MovieSets.Left + tblFilter_MovieSets.Left + gbFilterGeneral_MovieSets.Left + tblFilterGeneral_MovieSets.Left + btnFilterMissing_MovieSets.Left + 1,
                                                                 (pnlFilter_MovieSets.Top + tblFilter_MovieSets.Top + gbFilterGeneral_MovieSets.Top + tblFilterGeneral_MovieSets.Top + btnFilterMissing_MovieSets.Top) - pnlFilterMissingItems_MovieSets.Height)
            pnlFilterMissingItems_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterGeneral_Shows.Left + tblFilterGeneral_Shows.Left + btnFilterMissing_Shows.Left + 1,
                                                                 (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterGeneral_Shows.Top + tblFilterGeneral_Shows.Top + btnFilterMissing_Shows.Top) - pnlFilterMissingItems_Shows.Height)
            pnlFilterSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterSource_Movies.Left + 1,
                                                            (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterSource_Movies.Top) - pnlFilterSources_Movies.Height)
            pnlFilterSources_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterSource_Shows.Left + 1,
                                                           (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterSource_Shows.Top) - pnlFilterSources_Shows.Height)
            pnlLoadSettings.Location = New Point(Convert.ToInt32((Width - pnlLoadSettings.Width) / 2), Convert.ToInt32((Height - pnlLoadSettings.Height) / 2))
        End If
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not CloseApp Then
            BringToFront()
            Activate()
            cmnuTray.Enabled = True
            If Not Functions.CheckIfWindows Then Mono_Shown()
        End If
    End Sub

    Private Sub TaskRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        TaskList.Add(New Task With {.mType = mType, .Params = _params})
        If TasksDone Then
            tmrRunTasks.Start()
            TasksDone = False
        End If
    End Sub
    ''' <summary>
    ''' This is a generic callback function.
    ''' </summary>
    ''' <param name="mType"></param>
    ''' <param name="_params"></param>
    ''' <remarks></remarks>
    Private Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        Select Case mType

            Case Enums.ModuleEventType.CommandLine
                Select Case _params(0).ToString
                    Case "addmoviesource"
                        Using dSource As New dlgSourceMovie
                            If dSource.ShowDialog(CStr(_params(1)), CStr(_params(1))) = DialogResult.OK Then
                                Master.DB.Load_Sources_Movie()
                                SetMenus(True)
                            End If
                        End Using
                    Case "addtvshowsource"
                        Using dSource As New dlgSourceTVShow
                            If dSource.ShowDialog(CStr(_params(1)), CStr(_params(1))) = DialogResult.OK Then
                                Master.DB.Load_Sources_TVShow()
                                SetMenus(True)
                            End If
                        End Using
                    Case "cleanvideodb"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(644, "Cleaning Database..."))
                        CleanDB(New Structures.ScanOrClean With {.Movies = True, .MovieSets = True, .TV = True})
                        While bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "loadmedia"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(860, "Loading Media..."))
                        LoadingDone = False
                        LoadMedia(CType(_params(1), Structures.ScanOrClean), Convert.ToInt64(_params(2)), CStr(_params(3)))
                        While Not LoadingDone
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "run"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(859, "Running Module..."))
                        Dim strModuleName As String = CStr(_params(1))
                        Dim oParameters As List(Of Object) = CType(_params(2), List(Of Object))
                        Dim gModule As ModulesManager._externalGenericModuleClass = ModulesManager.Instance.externalGenericModules.FirstOrDefault(Function(y) y.ProcessorModule.ModuleName = strModuleName)
                        If gModule IsNot Nothing Then
                            gModule.ProcessorModule.RunGeneric(Enums.ModuleEventType.CommandLine, oParameters, Nothing, Nothing)
                        End If
                    Case "scrapemovies"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        Dim ScrapeModifiers As Structures.ScrapeModifiers = CType(_params(2), Structures.ScrapeModifiers)
                        CreateScrapeList_Movie(CType(_params(1), Enums.ScrapeType), Master.DefaultOptions_Movie, ScrapeModifiers)
                        While bwMovieScraper.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "scrapetvshows"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        Dim ScrapeModifiers As Structures.ScrapeModifiers = CType(_params(2), Structures.ScrapeModifiers)
                        CreateScrapeList_TV(CType(_params(1), Enums.ScrapeType), Master.DefaultOptions_TV, ScrapeModifiers)
                        While bwTVScraper.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                End Select

            Case Enums.ModuleEventType.Generic
                Select Case _params(0).ToString
                    Case "controlsenabled"
                        SetControlsEnabled(Convert.ToBoolean(_params(1)), If(_params.Count = 3, Convert.ToBoolean(_params(2)), False))
                    Case "filllist"
                        FillList(CBool(_params(1)), CBool(_params(2)), CBool(_params(3)))
                End Select
            Case Enums.ModuleEventType.Notification
                Select Case _params(0).ToString
                    Case "error"
                        dlgErrorViewer.Show(Me)
                    Case Else
                        Activate()
                End Select

            Case Enums.ModuleEventType.AfterEdit_Movie
                RefreshRow_Movie(Convert.ToInt64(_params(0)))

            Case Enums.ModuleEventType.AfterEdit_TVEpisode
                RefreshRow_TVEpisode(Convert.ToInt64(_params(0)))

            Case Enums.ModuleEventType.AfterEdit_TVShow
                RefreshRow_TVShow(Convert.ToInt64(_params(0)))

            Case Else
                logger.Warn("Callback for <{0}> with no handler.", mType)
        End Select
    End Sub

    Private Sub TaskManagerProgressUpdate(ByVal eProgressValue As TaskManager.ProgressValue)
        Select Case eProgressValue.EventType

            Case Enums.TaskManagerEventType.RefreshRow
                Select Case eProgressValue.ContentType
                    Case Enums.ContentType.Movie
                        RefreshRow_Movie(eProgressValue.ID)
                    Case Enums.ContentType.TVEpisode
                        RefreshRow_TVEpisode(eProgressValue.ID)
                    Case Enums.ContentType.TVSeason
                        RefreshRow_TVSeason(eProgressValue.ID)
                    Case Enums.ContentType.TVShow
                        RefreshRow_TVShow(eProgressValue.ID)
                End Select

            Case Enums.TaskManagerEventType.SimpleMessage
                SetStatus(eProgressValue.Message)
                'tspbLoading.Value = e.ProgressPercentage

            Case Else
                logger.Warn("Callback for <{0}> with no handler.", eProgressValue.EventType)
        End Select
    End Sub

    Private Sub mnuGenresAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGenresAdd.Click
        Dim strGenre As String = String.Empty
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            strGenre = mnuGenresNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuGenresGenre.Text.Trim) Then
            strGenre = mnuGenresGenre.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strGenre) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                            If Not tmpDBElement.Movie.Genres.Contains(strGenre) Then
                                tmpDBElement.Movie.Genres.Add(strGenre)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, False)
                                RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If Not tmpDBElement.TVShow.Genres.Contains(strGenre) Then
                                tmpDBElement.TVShow.Genres.Add(strGenre)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuGenresGenre_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGenresGenre.DropDown
        mnuGenresGenre.Items.Clear()
        Dim mGenre() As Object = APIXML.GetGenreList
        mnuGenresGenre.Items.AddRange(mGenre)

        mnuGenresNew.Text = String.Empty
    End Sub

    Private Sub mnuGenresGenre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGenresGenre.SelectedIndexChanged
        Dim iSelectedRowsCount As Integer = 0
        Select Case _SelectedContentType
            Case "movie"
                iSelectedRowsCount = dgvMovies.SelectedRows.Count
            Case "tvshow"
                iSelectedRowsCount = dgvTVShows.SelectedRows.Count
        End Select

        If iSelectedRowsCount > 1 Then
            mnuGenresRemove.Enabled = True
            mnuGenresAdd.Enabled = True
        Else
            mnuGenresRemove.Enabled = mnuGenresGenre.Tag.ToString.Contains(mnuGenresGenre.Text)
            mnuGenresAdd.Enabled = Not mnuGenresGenre.Tag.ToString.Contains(mnuGenresGenre.Text)
        End If
        mnuGenresSet.Enabled = True
    End Sub

    Private Sub mnuGenresNew_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGenresNew.TextChanged
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
            End If
            mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
        End If

        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            mnuGenresAdd.Enabled = True
            mnuGenresRemove.Enabled = False
            mnuGenresSet.Enabled = True
        Else
            mnuGenresAdd.Enabled = False
            mnuGenresRemove.Enabled = False
            mnuGenresSet.Enabled = False
        End If
    End Sub

    Private Sub mnuGenresRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGenresRemove.Click
        Dim strGenre As String = String.Empty
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            strGenre = mnuGenresNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuGenresGenre.Text.Trim) Then
            strGenre = mnuGenresGenre.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strGenre) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                            If tmpDBElement.Movie.Genres.Contains(strGenre) Then
                                tmpDBElement.Movie.Genres.Remove(strGenre)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, False)
                                RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If tmpDBElement.TVShow.Genres.Contains(strGenre) Then
                                tmpDBElement.TVShow.Genres.Remove(strGenre)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuGenresSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGenresSet.Click
        Dim strGenre As String = String.Empty
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            strGenre = mnuGenresNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuGenresGenre.Text.Trim) Then
            strGenre = mnuGenresGenre.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strGenre) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                            tmpDBElement.Movie.Genres.Clear()
                            tmpDBElement.Movie.Genres.Add(strGenre)
                            Master.DB.Save_Movie(tmpDBElement, True, True, False, False)
                            RefreshRow_Movie(tmpDBElement.ID)
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            tmpDBElement.TVShow.Genres.Clear()
                            tmpDBElement.TVShow.Genres.Add(strGenre)
                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                            RefreshRow_TVShow(tmpDBElement.ID)
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuLanguagesLanguage_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLanguagesLanguage.DropDown
        If mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
            mnuLanguagesLanguage.Items.Remove(String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
        End If
    End Sub

    Private Sub mnuLanguagesLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLanguagesLanguage.SelectedIndexChanged
        mnuLanguagesSet.Enabled = True
    End Sub

    Private Sub mnuLanguagesSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLanguagesSet.Click
        Dim strLanguage As String = String.Empty
        If Not String.IsNullOrEmpty(mnuLanguagesLanguage.Text.Trim) Then
            strLanguage = mnuLanguagesLanguage.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strLanguage) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                            tmpDBElement.Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = strLanguage).Abbreviation
                            tmpDBElement.Movie.Language = tmpDBElement.Language
                            Master.DB.Save_Movie(tmpDBElement, True, True, False, False)
                            RefreshRow_Movie(tmpDBElement.ID)
                        Next
                    Case "movieset"
                        For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(Convert.ToInt64(sRow.Cells("idSet").Value))
                            tmpDBElement.Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = strLanguage).Abbreviation
                            Master.DB.Save_MovieSet(tmpDBElement, True, True)
                            RefreshRow_MovieSet(tmpDBElement.ID)
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            tmpDBElement.Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = strLanguage).Abbreviation
                            tmpDBElement.TVShow.Language = tmpDBElement.Language
                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                            RefreshRow_TVShow(tmpDBElement.ID)
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuTagsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTagsAdd.Click
        Dim strTag As String = String.Empty
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            strTag = mnuTagsNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuTagsTag.Text.Trim) Then
            strTag = mnuTagsTag.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strTag) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                            If Not tmpDBElement.Movie.Tags.Contains(strTag) Then
                                tmpDBElement.Movie.Tags.Add(strTag)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, False)
                                RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If Not tmpDBElement.TVShow.Tags.Contains(strTag) Then
                                tmpDBElement.TVShow.Tags.Add(strTag)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuTagsNew_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTagsNew.TextChanged
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
            End If
            mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
        End If

        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            mnuTagsAdd.Enabled = True
            mnuTagsRemove.Enabled = False
            mnuTagsSet.Enabled = True
        Else
            mnuTagsAdd.Enabled = False
            mnuTagsRemove.Enabled = False
            mnuTagsSet.Enabled = False
        End If
    End Sub

    Private Sub mnuTagsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTagsRemove.Click
        Dim strTag As String = String.Empty
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            strTag = mnuTagsNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuTagsTag.Text.Trim) Then
            strTag = mnuTagsTag.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strTag) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                            If tmpDBElement.Movie.Tags.Contains(strTag) Then
                                tmpDBElement.Movie.Tags.Remove(strTag)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, False)
                                RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If tmpDBElement.TVShow.Tags.Contains(strTag) Then
                                tmpDBElement.TVShow.Tags.Remove(strTag)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuTagsSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTagsSet.Click
        Dim strTag As String = String.Empty
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            strTag = mnuTagsNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuTagsTag.Text.Trim) Then
            strTag = mnuTagsTag.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strTag) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                            tmpDBElement.Movie.Tags.Clear()
                            tmpDBElement.Movie.Tags.Add(strTag)
                            Master.DB.Save_Movie(tmpDBElement, True, True, False, False)
                            RefreshRow_Movie(tmpDBElement.ID)
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            tmpDBElement.TVShow.Tags.Clear()
                            tmpDBElement.TVShow.Tags.Add(strTag)
                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                            RefreshRow_TVShow(tmpDBElement.ID)
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuTagsTag_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuTagsTag.DropDown
        mnuTagsTag.Items.Clear()
        Dim mTag() As Object = Master.DB.GetAllTags
        mnuTagsTag.Items.AddRange(mTag)

        mnuTagsNew.Text = String.Empty
    End Sub

    Private Sub mnuTagsTag_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuTagsTag.SelectedIndexChanged
        Dim iSelectedRowsCount As Integer = 0
        Select Case _SelectedContentType
            Case "movie"
                iSelectedRowsCount = dgvMovies.SelectedRows.Count
            Case "tvshow"
                iSelectedRowsCount = dgvTVShows.SelectedRows.Count
        End Select

        If iSelectedRowsCount > 1 Then
            mnuTagsRemove.Enabled = True
            mnuTagsAdd.Enabled = True
        Else
            mnuTagsRemove.Enabled = mnuTagsTag.Tag.ToString.Contains(mnuTagsTag.Text)
            mnuTagsAdd.Enabled = Not mnuTagsTag.Tag.ToString.Contains(mnuTagsTag.Text)
        End If
        mnuTagsSet.Enabled = True
    End Sub

    Private Sub cmnuMovieSetSortMethodMethods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieSetEditSortMethodMethods.SelectedIndexChanged
        cmnuMovieSetEditSortMethodSet.Enabled = True
    End Sub

    Private Sub lblFilterGenreClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterGenresClose_Movies.Click
        txtFilterGenre_Movies.Focus()
        pnlFilterGenres_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterGenresClose_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterGenresClose_Shows.Click
        txtFilterGenre_Shows.Focus()
        pnlFilterGenres_Shows.Tag = String.Empty
    End Sub

    Private Sub lblFilterCountryClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterCountriesClose_Movies.Click
        txtFilterCountry_Movies.Focus()
        pnlFilterCountries_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterDataFieldsClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterDataFieldsClose_Movies.Click
        txtFilterDataField_Movies.Focus()
        pnlFilterDataFields_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterMissingItemsClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterMissingItemsClose_Movies.Click
        pnlFilterMissingItems_Movies.Visible = False
    End Sub

    Private Sub lblFilterMissingItemsClose_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterMissingItemsClose_MovieSets.Click
        pnlFilterMissingItems_MovieSets.Visible = False
    End Sub

    Private Sub lblFilterMissingItemsClose_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterMissingItemsClose_Shows.Click
        pnlFilterMissingItems_Shows.Visible = False
    End Sub

    Private Sub lblFilterSourceClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterSourcesClose_Movies.Click
        txtFilterSource_Movies.Focus()
        pnlFilterSources_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterSourceClose_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterSourcesClose_Shows.Click
        txtFilterSource_Shows.Focus()
        pnlFilterSources_Shows.Tag = String.Empty
    End Sub

    Private Sub LoadInfo_Movie(ByVal ID As Long, ByVal sPath As String, ByVal doInfo As Boolean)
        dgvMovies.SuspendLayout()
        SetControlsEnabled(False)
        ShowNoInfo(False)

        If doInfo Then
            ClearInfo()

            If bwLoadMovieInfo.IsBusy AndAlso Not bwLoadMovieInfo.CancellationPending Then
                bwLoadMovieInfo.CancelAsync()
            End If

            While bwLoadMovieInfo.IsBusy
                Application.DoEvents()
            End While

            bwLoadMovieInfo = New ComponentModel.BackgroundWorker
            bwLoadMovieInfo.WorkerSupportsCancellation = True
            bwLoadMovieInfo.RunWorkerAsync(New Arguments With {.ID = ID})
        End If
    End Sub

    Private Sub LoadInfo_MovieSet(ByVal ID As Long, ByVal doInfo As Boolean)
        dgvMovieSets.SuspendLayout()
        SetControlsEnabled(False)
        ShowNoInfo(False)

        If doInfo Then
            ClearInfo()

            If bwLoadMovieSetInfo.IsBusy AndAlso Not bwLoadMovieSetInfo.CancellationPending Then
                bwLoadMovieSetInfo.CancelAsync()
            End If

            While bwLoadMovieSetInfo.IsBusy
                Application.DoEvents()
            End While

            bwLoadMovieSetInfo = New ComponentModel.BackgroundWorker
            bwLoadMovieSetInfo.WorkerSupportsCancellation = True
            bwLoadMovieSetInfo.RunWorkerAsync(New Arguments With {.ID = ID})
        End If
    End Sub

    Private Sub LoadInfo_TVEpisode(ByVal ID As Long)
        dgvTVEpisodes.SuspendLayout()
        SetControlsEnabled(False)
        ShowNoInfo(False)

        If Not currThemeType = Theming.ThemeType.Episode Then ApplyTheme(Theming.ThemeType.Episode)

        ClearInfo()

        bwLoadEpInfo = New ComponentModel.BackgroundWorker
        bwLoadEpInfo.WorkerSupportsCancellation = True
        bwLoadEpInfo.RunWorkerAsync(New Arguments With {.ID = ID})
    End Sub

    Private Sub LoadInfo_TVSeason(ByVal SeasonID As Long, Optional ByVal isMissing As Boolean = False)
        dgvTVSeasons.SuspendLayout()
        SetControlsEnabled(False)
        ShowNoInfo(False)

        If Not currThemeType = Theming.ThemeType.Show Then
            ApplyTheme(Theming.ThemeType.Show)
        End If

        ClearInfo()

        bwLoadSeasonInfo = New ComponentModel.BackgroundWorker
        bwLoadSeasonInfo.WorkerSupportsCancellation = True
        bwLoadSeasonInfo.RunWorkerAsync(New Arguments With {.ID = SeasonID, .setEnabled = Not isMissing})
    End Sub

    Private Sub LoadInfo_TVShow(ByVal ID As Long)
        dgvTVShows.SuspendLayout()
        SetControlsEnabled(False)
        ShowNoInfo(False)

        If Not currThemeType = Theming.ThemeType.Show Then ApplyTheme(Theming.ThemeType.Show)

        ClearInfo()

        bwLoadShowInfo = New ComponentModel.BackgroundWorker
        bwLoadShowInfo.WorkerSupportsCancellation = True
        bwLoadShowInfo.RunWorkerAsync(New Arguments With {.ID = ID})

        FillSeasons(ID)
    End Sub

    Private Sub lstActors_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstActors.SelectedValueChanged
        If lstActors.Items.Count > 0 AndAlso lstActors.SelectedItems.Count > 0 AndAlso alActors.Item(lstActors.SelectedIndex) IsNot Nothing AndAlso Not alActors.Item(lstActors.SelectedIndex).ToString = "none" Then

            If pbActors.Image IsNot Nothing Then
                pbActors.Image.Dispose()
                pbActors.Image = Nothing
            End If

            If Not alActors.Item(lstActors.SelectedIndex).ToString.Trim.StartsWith("http") Then
                MainActors.LoadFromFile(alActors.Item(lstActors.SelectedIndex).ToString, True)

                If MainActors.Image IsNot Nothing Then
                    pbActors.Image = MainActors.Image
                Else
                    pbActors.Image = My.Resources.actor_silhouette
                End If
            Else
                pbActLoad.Visible = True

                If bwDownloadPic.IsBusy Then
                    bwDownloadPic.CancelAsync()
                    While bwDownloadPic.IsBusy
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                End If

                bwDownloadPic = New ComponentModel.BackgroundWorker
                bwDownloadPic.WorkerSupportsCancellation = True
                bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = alActors.Item(lstActors.SelectedIndex).ToString})
            End If

        Else
            pbActors.Image = My.Resources.actor_silhouette
        End If
    End Sub

    Private Sub mnuContextMenuStrip_Opened(sender As Object, e As EventArgs) Handles mnuGenres.Opened, mnuLanguages.Opened, mnuTags.Opened, mnuScrapeSubmenu.Opened
        Dim tContextMenuStrip As ContextMenuStrip = CType(sender, ContextMenuStrip)
        If tContextMenuStrip IsNot Nothing AndAlso tContextMenuStrip.OwnerItem IsNot Nothing AndAlso tContextMenuStrip.OwnerItem.Tag IsNot Nothing Then
            _SelectedContentType = tContextMenuStrip.OwnerItem.Tag.ToString
        End If
    End Sub

    Private Sub mnuScrapeMovies_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScrapeMovies.ButtonClick
        If Master.eSettings.MovieGeneralCustomScrapeButtonEnabled Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Master.eSettings.MovieGeneralCustomScrapeButtonModifierType, True)
            CreateScrapeList_Movie(Master.eSettings.MovieGeneralCustomScrapeButtonScrapeType, Master.DefaultOptions_Movie, ScrapeModifiers)
        Else
            mnuScrapeMovies.ShowDropDown()
        End If
    End Sub

    Private Sub mnuScrapeMovieSets_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScrapeMovieSets.ButtonClick
        If Master.eSettings.MovieSetGeneralCustomScrapeButtonEnabled Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Master.eSettings.MovieSetGeneralCustomScrapeButtonModifierType, True)
            CreateScrapeList_MovieSet(Master.eSettings.MovieSetGeneralCustomScrapeButtonScrapeType, Master.DefaultOptions_MovieSet, ScrapeModifiers)
        Else
            mnuScrapeMovieSets.ShowDropDown()
        End If
    End Sub

    Private Sub mnuScrapeTVShows_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScrapeTVShows.ButtonClick
        If Master.eSettings.TVGeneralCustomScrapeButtonEnabled Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Master.eSettings.TVGeneralCustomScrapeButtonModifierType, True)
            CreateScrapeList_TV(Master.eSettings.TVGeneralCustomScrapeButtonScrapeType, Master.DefaultOptions_TV, ScrapeModifiers)
        Else
            mnuScrapeTVShows.ShowDropDown()
        End If
    End Sub

    Private Sub mnuScrapeOption_Opened(sender As Object, e As EventArgs) Handles mnuScrapeOption.Opened
        _SelectedContentType = mnuScrapeOption.OwnerItem.Tag.ToString

        With Master.eSettings
            Select Case _SelectedContentType
                Case "movie"
                    mnuScrapeOptionActors.Enabled = .MovieScraperCast
                    mnuScrapeOptionActors.Visible = True
                    mnuScrapeOptionAired.Enabled = False
                    mnuScrapeOptionAired.Visible = False
                    mnuScrapeOptionCertifications.Enabled = .MovieScraperCert
                    mnuScrapeOptionCertifications.Visible = True
                    mnuScrapeOptionCollectionID.Enabled = .MovieScraperCollectionID
                    mnuScrapeOptionCollectionID.Visible = True
                    mnuScrapeOptionCountries.Enabled = .MovieScraperCountry
                    mnuScrapeOptionCountries.Visible = True
                    mnuScrapeOptionCreators.Enabled = False
                    mnuScrapeOptionCreators.Visible = False
                    mnuScrapeOptionDirectors.Enabled = .MovieScraperDirector
                    mnuScrapeOptionDirectors.Visible = True
                    mnuScrapeOptionEpiGuideURL.Enabled = False
                    mnuScrapeOptionEpiGuideURL.Visible = False
                    mnuScrapeOptionGenres.Enabled = .MovieScraperGenre
                    mnuScrapeOptionGenres.Visible = True
                    mnuScrapeOptionGuestStars.Enabled = False
                    mnuScrapeOptionGuestStars.Visible = False
                    mnuScrapeOptionMPAA.Enabled = .MovieScraperMPAA
                    mnuScrapeOptionMPAA.Visible = True
                    mnuScrapeOptionOriginalTitle.Enabled = .MovieScraperOriginalTitle
                    mnuScrapeOptionOriginalTitle.Visible = True
                    mnuScrapeOptionOutline.Enabled = .MovieScraperOutline
                    mnuScrapeOptionOutline.Visible = True
                    mnuScrapeOptionPlot.Enabled = .MovieScraperPlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = False
                    mnuScrapeOptionPremiered.Visible = False
                    mnuScrapeOptionRating.Enabled = .MovieScraperRating
                    mnuScrapeOptionRating.Visible = True
                    mnuScrapeOptionReleaseDate.Enabled = .MovieScraperRelease
                    mnuScrapeOptionReleaseDate.Visible = True
                    mnuScrapeOptionRuntime.Enabled = .MovieScraperRuntime
                    mnuScrapeOptionRuntime.Visible = True
                    mnuScrapeOptionStatus.Enabled = False
                    mnuScrapeOptionStatus.Visible = False
                    mnuScrapeOptionStudios.Enabled = .MovieScraperStudio
                    mnuScrapeOptionStudios.Visible = True
                    mnuScrapeOptionTagline.Enabled = .MovieScraperTagline
                    mnuScrapeOptionTagline.Visible = True
                    mnuScrapeOptionTitle.Enabled = .MovieScraperTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = .MovieScraperTop250
                    mnuScrapeOptionTop250.Visible = True
                    mnuScrapeOptionTrailer.Enabled = .MovieScraperTrailer
                    mnuScrapeOptionTrailer.Visible = True
                    mnuScrapeOptionWriters.Enabled = .MovieScraperCredits
                    mnuScrapeOptionWriters.Visible = True
                    mnuScrapeOptionYear.Enabled = .MovieScraperYear
                    mnuScrapeOptionYear.Visible = True
                Case "movieset"
                    mnuScrapeOptionActors.Enabled = False
                    mnuScrapeOptionActors.Visible = False
                    mnuScrapeOptionAired.Enabled = False
                    mnuScrapeOptionAired.Visible = False
                    mnuScrapeOptionCertifications.Enabled = False
                    mnuScrapeOptionCertifications.Visible = False
                    mnuScrapeOptionCollectionID.Enabled = False
                    mnuScrapeOptionCollectionID.Visible = False
                    mnuScrapeOptionCountries.Enabled = False
                    mnuScrapeOptionCountries.Visible = False
                    mnuScrapeOptionCreators.Enabled = False
                    mnuScrapeOptionCreators.Visible = False
                    mnuScrapeOptionDirectors.Enabled = False
                    mnuScrapeOptionDirectors.Visible = False
                    mnuScrapeOptionEpiGuideURL.Enabled = False
                    mnuScrapeOptionEpiGuideURL.Visible = False
                    mnuScrapeOptionGenres.Enabled = False
                    mnuScrapeOptionGenres.Visible = False
                    mnuScrapeOptionGuestStars.Enabled = False
                    mnuScrapeOptionGuestStars.Visible = False
                    mnuScrapeOptionMPAA.Enabled = False
                    mnuScrapeOptionMPAA.Visible = False
                    mnuScrapeOptionOriginalTitle.Enabled = False
                    mnuScrapeOptionOriginalTitle.Visible = False
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .MovieSetScraperPlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = False
                    mnuScrapeOptionPremiered.Visible = False
                    mnuScrapeOptionRating.Enabled = False
                    mnuScrapeOptionRating.Visible = False
                    mnuScrapeOptionReleaseDate.Enabled = False
                    mnuScrapeOptionReleaseDate.Visible = False
                    mnuScrapeOptionRuntime.Enabled = False
                    mnuScrapeOptionRuntime.Visible = False
                    mnuScrapeOptionStatus.Enabled = False
                    mnuScrapeOptionStatus.Visible = False
                    mnuScrapeOptionStudios.Enabled = False
                    mnuScrapeOptionStudios.Visible = False
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    mnuScrapeOptionTitle.Enabled = .MovieSetScraperTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionWriters.Enabled = False
                    mnuScrapeOptionWriters.Visible = False
                    mnuScrapeOptionYear.Enabled = False
                    mnuScrapeOptionYear.Visible = False
                Case "tvepisode"
                    mnuScrapeOptionActors.Enabled = .TVScraperEpisodeActors
                    mnuScrapeOptionActors.Visible = True
                    mnuScrapeOptionAired.Enabled = .TVScraperEpisodeAired
                    mnuScrapeOptionAired.Visible = True
                    mnuScrapeOptionCertifications.Enabled = False
                    mnuScrapeOptionCertifications.Visible = False
                    mnuScrapeOptionCollectionID.Enabled = False
                    mnuScrapeOptionCollectionID.Visible = False
                    mnuScrapeOptionCountries.Enabled = False
                    mnuScrapeOptionCountries.Visible = False
                    mnuScrapeOptionCreators.Enabled = False
                    mnuScrapeOptionCreators.Visible = False
                    mnuScrapeOptionDirectors.Enabled = .TVScraperEpisodeDirector
                    mnuScrapeOptionDirectors.Visible = True
                    mnuScrapeOptionEpiGuideURL.Enabled = False
                    mnuScrapeOptionEpiGuideURL.Visible = False
                    mnuScrapeOptionGenres.Enabled = False
                    mnuScrapeOptionGenres.Visible = False
                    mnuScrapeOptionGuestStars.Enabled = .TVScraperEpisodeGuestStars
                    mnuScrapeOptionGuestStars.Visible = True
                    mnuScrapeOptionMPAA.Enabled = False
                    mnuScrapeOptionMPAA.Visible = False
                    mnuScrapeOptionOriginalTitle.Enabled = False
                    mnuScrapeOptionOriginalTitle.Visible = False
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .TVScraperEpisodePlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = False
                    mnuScrapeOptionPremiered.Visible = False
                    mnuScrapeOptionRating.Enabled = .TVScraperEpisodeRating
                    mnuScrapeOptionRating.Visible = True
                    mnuScrapeOptionReleaseDate.Enabled = False
                    mnuScrapeOptionReleaseDate.Visible = False
                    mnuScrapeOptionRuntime.Enabled = .TVScraperEpisodeRuntime
                    mnuScrapeOptionRuntime.Visible = True
                    mnuScrapeOptionStatus.Enabled = False
                    mnuScrapeOptionStatus.Visible = False
                    mnuScrapeOptionStudios.Enabled = False
                    mnuScrapeOptionStudios.Visible = False
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    mnuScrapeOptionTitle.Enabled = .TVScraperEpisodeTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionWriters.Enabled = .TVScraperEpisodeCredits
                    mnuScrapeOptionWriters.Visible = True
                    mnuScrapeOptionYear.Enabled = False
                    mnuScrapeOptionYear.Visible = False
                Case "tvseason"
                    mnuScrapeOptionActors.Enabled = False
                    mnuScrapeOptionActors.Visible = False
                    mnuScrapeOptionAired.Enabled = .TVScraperSeasonAired
                    mnuScrapeOptionAired.Visible = True
                    mnuScrapeOptionCertifications.Enabled = False
                    mnuScrapeOptionCertifications.Visible = False
                    mnuScrapeOptionCollectionID.Enabled = False
                    mnuScrapeOptionCollectionID.Visible = False
                    mnuScrapeOptionCountries.Enabled = False
                    mnuScrapeOptionCountries.Visible = False
                    mnuScrapeOptionCreators.Enabled = False
                    mnuScrapeOptionCreators.Visible = False
                    mnuScrapeOptionDirectors.Enabled = False
                    mnuScrapeOptionDirectors.Visible = False
                    mnuScrapeOptionEpiGuideURL.Enabled = False
                    mnuScrapeOptionEpiGuideURL.Visible = False
                    mnuScrapeOptionGenres.Enabled = False
                    mnuScrapeOptionGenres.Visible = False
                    mnuScrapeOptionGuestStars.Enabled = False
                    mnuScrapeOptionGuestStars.Visible = False
                    mnuScrapeOptionMPAA.Enabled = False
                    mnuScrapeOptionMPAA.Visible = False
                    mnuScrapeOptionOriginalTitle.Enabled = False
                    mnuScrapeOptionOriginalTitle.Visible = False
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .TVScraperSeasonPlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = False
                    mnuScrapeOptionPremiered.Visible = False
                    mnuScrapeOptionRating.Enabled = False
                    mnuScrapeOptionRating.Visible = False
                    mnuScrapeOptionReleaseDate.Enabled = False
                    mnuScrapeOptionReleaseDate.Visible = False
                    mnuScrapeOptionRuntime.Enabled = False
                    mnuScrapeOptionRuntime.Visible = False
                    mnuScrapeOptionStatus.Enabled = False
                    mnuScrapeOptionStatus.Visible = False
                    mnuScrapeOptionStudios.Enabled = False
                    mnuScrapeOptionStudios.Visible = False
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    mnuScrapeOptionTitle.Enabled = .TVScraperSeasonTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionWriters.Enabled = False
                    mnuScrapeOptionWriters.Visible = False
                    mnuScrapeOptionYear.Enabled = False
                    mnuScrapeOptionYear.Visible = False
                Case "tvshow"
                    mnuScrapeOptionActors.Enabled = .TVScraperShowActors
                    mnuScrapeOptionActors.Visible = True
                    mnuScrapeOptionAired.Enabled = False
                    mnuScrapeOptionAired.Visible = False
                    mnuScrapeOptionCertifications.Enabled = .TVScraperShowCert
                    mnuScrapeOptionCertifications.Visible = True
                    mnuScrapeOptionCollectionID.Enabled = False
                    mnuScrapeOptionCollectionID.Visible = False
                    mnuScrapeOptionCountries.Enabled = .TVScraperShowCountry
                    mnuScrapeOptionCountries.Visible = True
                    mnuScrapeOptionCreators.Enabled = .TVScraperShowCreators
                    mnuScrapeOptionCreators.Visible = True
                    mnuScrapeOptionDirectors.Enabled = False
                    mnuScrapeOptionDirectors.Visible = False
                    mnuScrapeOptionEpiGuideURL.Enabled = .TVScraperShowEpiGuideURL
                    mnuScrapeOptionEpiGuideURL.Visible = True
                    mnuScrapeOptionGenres.Enabled = .TVScraperShowGenre
                    mnuScrapeOptionGenres.Visible = True
                    mnuScrapeOptionGuestStars.Enabled = False
                    mnuScrapeOptionGuestStars.Visible = False
                    mnuScrapeOptionMPAA.Enabled = .TVScraperShowMPAA
                    mnuScrapeOptionMPAA.Visible = True
                    mnuScrapeOptionOriginalTitle.Enabled = .TVScraperShowOriginalTitle
                    mnuScrapeOptionOriginalTitle.Visible = True
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .TVScraperShowPlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = .TVScraperShowPremiered
                    mnuScrapeOptionPremiered.Visible = True
                    mnuScrapeOptionRating.Enabled = .TVScraperShowRating
                    mnuScrapeOptionRating.Visible = True
                    mnuScrapeOptionReleaseDate.Enabled = False
                    mnuScrapeOptionReleaseDate.Visible = False
                    mnuScrapeOptionRuntime.Enabled = .TVScraperShowRuntime
                    mnuScrapeOptionRuntime.Visible = True
                    mnuScrapeOptionStatus.Enabled = .TVScraperShowStatus
                    mnuScrapeOptionStatus.Visible = True
                    mnuScrapeOptionStudios.Enabled = .TVScraperShowStudio
                    mnuScrapeOptionStudios.Visible = True
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    mnuScrapeOptionTitle.Enabled = .TVScraperShowTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionWriters.Enabled = False
                    mnuScrapeOptionWriters.Visible = False
                    mnuScrapeOptionYear.Enabled = False
                    mnuScrapeOptionYear.Visible = False
            End Select
        End With
    End Sub

    Private Sub mnuScrapeModifier_Opened(sender As Object, e As EventArgs) Handles mnuScrapeModifier.Opened
        _SelectedScrapeTypeMode = mnuScrapeModifier.OwnerItem.Tag.ToString

        With Master.eSettings
            Select Case _SelectedContentType
                Case "movie"
                    mnuScrapeModifierActorthumbs.Enabled = .MovieActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    mnuScrapeModifierDiscArt.Visible = True
                    mnuScrapeModifierExtrafanarts.Enabled = .MovieExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrafanarts)
                    mnuScrapeModifierExtrafanarts.Visible = True
                    mnuScrapeModifierExtrathumbs.Enabled = .MovieExtrathumbsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrathumbs)
                    mnuScrapeModifierExtrathumbs.Visible = True
                    mnuScrapeModifierFanart.Enabled = .MovieFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = .MovieScraperMetaDataScan
                    mnuScrapeModifierMetaData.Visible = True
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = .MovieThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    mnuScrapeModifierTheme.Visible = True
                    mnuScrapeModifierTrailer.Enabled = .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
                    mnuScrapeModifierTrailer.Visible = True
                Case "movieset"
                    mnuScrapeModifierActorthumbs.Enabled = False
                    mnuScrapeModifierActorthumbs.Visible = False
                    mnuScrapeModifierBanner.Enabled = .MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = .MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = .MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    mnuScrapeModifierDiscArt.Visible = True
                    mnuScrapeModifierExtrafanarts.Enabled = False
                    mnuScrapeModifierExtrafanarts.Visible = False
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = False
                    mnuScrapeModifierMetaData.Visible = False
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvepisode"
                    mnuScrapeModifierActorthumbs.Enabled = .TVEpisodeActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = False
                    mnuScrapeModifierBanner.Visible = False
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = False
                    mnuScrapeModifierClearArt.Visible = False
                    mnuScrapeModifierClearLogo.Enabled = False
                    mnuScrapeModifierClearLogo.Visible = False
                    mnuScrapeModifierDiscArt.Enabled = False
                    mnuScrapeModifierDiscArt.Visible = False
                    mnuScrapeModifierExtrafanarts.Enabled = False
                    mnuScrapeModifierExtrafanarts.Visible = False
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .TVEpisodeFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = False
                    mnuScrapeModifierLandscape.Visible = False
                    mnuScrapeModifierMetaData.Enabled = .TVScraperMetaDataScan
                    mnuScrapeModifierMetaData.Visible = True
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvseason"
                    mnuScrapeModifierActorthumbs.Enabled = False
                    mnuScrapeModifierActorthumbs.Visible = False
                    mnuScrapeModifierBanner.Enabled = .TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = False
                    mnuScrapeModifierClearArt.Visible = False
                    mnuScrapeModifierClearLogo.Enabled = False
                    mnuScrapeModifierClearLogo.Visible = False
                    mnuScrapeModifierDiscArt.Enabled = False
                    mnuScrapeModifierDiscArt.Visible = False
                    mnuScrapeModifierExtrafanarts.Enabled = False
                    mnuScrapeModifierExtrafanarts.Visible = False
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = False
                    mnuScrapeModifierMetaData.Visible = False
                    mnuScrapeModifierNFO.Enabled = False
                    mnuScrapeModifierNFO.Visible = False
                    mnuScrapeModifierPoster.Enabled = .TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvshow"
                    mnuScrapeModifierActorthumbs.Enabled = .TVShowActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = .TVShowBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = .TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    mnuScrapeModifierCharacterArt.Visible = True
                    mnuScrapeModifierClearArt.Enabled = .TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = False
                    mnuScrapeModifierDiscArt.Visible = False
                    mnuScrapeModifierExtrafanarts.Enabled = .TVShowExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainExtrafanarts)
                    mnuScrapeModifierExtrafanarts.Visible = True
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = False
                    mnuScrapeModifierMetaData.Visible = False
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = .TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
                    mnuScrapeModifierTheme.Visible = True
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
            End Select
        End With
    End Sub

    Private Sub mnuScrapeType_Opened(sender As Object, e As EventArgs) Handles mnuScrapeType.Opened
        If mnuScrapeType.OwnerItem.OwnerItem IsNot Nothing Then
            _SelectedScrapeType = mnuScrapeType.OwnerItem.Tag.ToString
            _SelectedContentType = mnuScrapeType.OwnerItem.OwnerItem.Tag.ToString
        Else
            _SelectedScrapeType = "selected"
            _SelectedContentType = mnuScrapeType.OwnerItem.Tag.ToString
        End If
    End Sub

    Private Sub Autoscraper(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        mnuScrapeModifierActorthumbs.Click,
        mnuScrapeModifierAll.Click,
        mnuScrapeModifierBanner.Click,
        mnuScrapeModifierCharacterArt.Click,
        mnuScrapeModifierClearArt.Click,
        mnuScrapeModifierClearLogo.Click,
        mnuScrapeModifierDiscArt.Click,
        mnuScrapeModifierExtrafanarts.Click,
        mnuScrapeModifierExtrathumbs.Click,
        mnuScrapeModifierFanart.Click,
        mnuScrapeModifierLandscape.Click,
        mnuScrapeModifierMetaData.Click,
        mnuScrapeModifierNFO.Click,
        mnuScrapeModifierPoster.Click,
        mnuScrapeModifierTheme.Click,
        mnuScrapeModifierTrailer.Click,
        mnuScrapeSubmenuCustom.Click

        Dim ContentType As String = String.Empty
        Dim ModifierType As String = String.Empty
        Dim ScrapeType As String = String.Empty
        Dim Type As Enums.ScrapeType
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Dim Menu As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        ModifierType = Menu.Tag.ToString
        ScrapeType = String.Concat(_SelectedScrapeType, "_", _SelectedScrapeTypeMode)
        ContentType = _SelectedContentType

        If Not ModifierType = "custom" Then
            Select Case ModifierType
                Case "all"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                Case "actorthumbs"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainActorThumbs, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeActorThumbs, True)
                Case "banner"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsBanner, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonBanner, True)
                Case "characterart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainCharacterArt, True)
                Case "clearart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                Case "clearlogo"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                Case "discart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
                Case "extrafanarts"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrafanarts, True)
                Case "extrathumbs"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrathumbs, True)
                Case "fanart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsFanart, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeFanart, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonFanart, True)
                Case "landscape"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsLandscape, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonLandscape, True)
                Case "metadata"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainMeta, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeMeta, True)
                Case "nfo"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeNFO, True)
                Case "poster"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsPoster, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodePoster, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonPoster, True)
                Case "subtitle"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainSubtitle, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeSubtitle, True)
                Case "theme"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTheme, True)
                Case "trailer"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTrailer, True)
            End Select

            Select Case ScrapeType
                Case "all_ask"
                    Type = Enums.ScrapeType.AllAsk
                Case "all_auto"
                    Type = Enums.ScrapeType.AllAuto
                Case "all_skip"
                    Type = Enums.ScrapeType.AllSkip
                Case "filter_ask"
                    Type = Enums.ScrapeType.FilterAsk
                Case "filter_auto"
                    Type = Enums.ScrapeType.FilterAuto
                Case "filter_skip"
                    Type = Enums.ScrapeType.FilterSkip
                Case "marked_ask"
                    Type = Enums.ScrapeType.MarkedAsk
                Case "marked_auto"
                    Type = Enums.ScrapeType.MarkedAuto
                Case "marked_skip"
                    Type = Enums.ScrapeType.MarkedSkip
                Case "missing_ask"
                    Type = Enums.ScrapeType.MissingAsk
                Case "missing_auto"
                    Type = Enums.ScrapeType.MissingAuto
                Case "missing_skip"
                    Type = Enums.ScrapeType.MissingSkip
                Case "new_ask"
                    Type = Enums.ScrapeType.NewAsk
                Case "new_auto"
                    Type = Enums.ScrapeType.NewAuto
                Case "new_skip"
                    Type = Enums.ScrapeType.NewSkip
                Case "selected_ask"
                    Type = Enums.ScrapeType.SelectedAsk
                Case "selected_auto"
                    Type = Enums.ScrapeType.SelectedAuto
                Case "selected_skip"
                    Type = Enums.ScrapeType.SelectedSkip
            End Select

            Select Case ContentType
                Case "movie"
                    CreateScrapeList_Movie(Type, Master.DefaultOptions_Movie, ScrapeModifiers)
                Case "movieset"
                    CreateScrapeList_MovieSet(Type, Master.DefaultOptions_MovieSet, ScrapeModifiers)
                Case "tvepisode"
                    CreateScrapeList_TVEpisode(Type, Master.DefaultOptions_TV, ScrapeModifiers)
                Case "tvseason"
                    CreateScrapeList_TVSeason(Type, Master.DefaultOptions_TV, ScrapeModifiers)
                Case "tvshow"
                    ScrapeModifiers.withEpisodes = True
                    ScrapeModifiers.withSeasons = True
                    CreateScrapeList_TV(Type, Master.DefaultOptions_TV, ScrapeModifiers)
            End Select
        Else
            Select Case ContentType
                Case "movie"
                    SetControlsEnabled(False)
                    Using dlgCustomScraper As New dlgCustomScraper(Enums.ContentType.Movie)
                        Dim CustomScraper As Structures.CustomUpdaterStruct = Nothing
                        CustomScraper = dlgCustomScraper.ShowDialog()
                        If Not CustomScraper.Canceled Then
                            CreateScrapeList_Movie(CustomScraper.ScrapeType, CustomScraper.ScrapeOptions, CustomScraper.ScrapeModifiers)
                        Else
                            SetControlsEnabled(True)
                        End If
                    End Using
                Case "movieset"
                    SetControlsEnabled(False)
                    Using dlgCustomScraper As New dlgCustomScraper(Enums.ContentType.MovieSet)
                        Dim CustomScraper As Structures.CustomUpdaterStruct = Nothing
                        CustomScraper = dlgCustomScraper.ShowDialog()
                        If Not CustomScraper.Canceled Then
                            CreateScrapeList_MovieSet(CustomScraper.ScrapeType, CustomScraper.ScrapeOptions, CustomScraper.ScrapeModifiers)
                        Else
                            SetControlsEnabled(True)
                        End If
                    End Using
                Case "tvshow"
                    SetControlsEnabled(False)
                    Using dlgCustomScraper As New dlgCustomScraper(Enums.ContentType.TV)
                        Dim CustomScraper As Structures.CustomUpdaterStruct = Nothing
                        CustomScraper = dlgCustomScraper.ShowDialog()
                        If Not CustomScraper.Canceled Then
                            CreateScrapeList_TV(CustomScraper.ScrapeType, CustomScraper.ScrapeOptions, CustomScraper.ScrapeModifiers)
                        Else
                            SetControlsEnabled(True)
                        End If
                    End Using
            End Select
        End If
    End Sub

    Private Sub SingleDataField(ByVal sender As Object, ByVal e As EventArgs) Handles _
        mnuScrapeOptionActors.Click,
        mnuScrapeOptionAired.Click,
        mnuScrapeOptionCertifications.Click,
        mnuScrapeOptionCollectionID.Click,
        mnuScrapeOptionCountries.Click,
        mnuScrapeOptionCreators.Click,
        mnuScrapeOptionDirectors.Click,
        mnuScrapeOptionEpiGuideURL.Click,
        mnuScrapeOptionGenres.Click,
        mnuScrapeOptionGuestStars.Click,
        mnuScrapeOptionMPAA.Click,
        mnuScrapeOptionOriginalTitle.Click,
        mnuScrapeOptionOutline.Click,
        mnuScrapeOptionPlot.Click,
        mnuScrapeOptionPremiered.Click,
        mnuScrapeOptionRating.Click,
        mnuScrapeOptionReleaseDate.Click,
        mnuScrapeOptionRuntime.Click,
        mnuScrapeOptionStatus.Click,
        mnuScrapeOptionStudios.Click,
        mnuScrapeOptionTagline.Click,
        mnuScrapeOptionTitle.Click,
        mnuScrapeOptionTop250.Click,
        mnuScrapeOptionTrailer.Click,
        mnuScrapeOptionWriters.Click,
        mnuScrapeOptionYear.Click

        Dim ContentType As String = String.Empty
        Dim ScrapeOption As String = String.Empty
        Dim ScrapeOptions As New Structures.ScrapeOptions
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Dim Menu As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        ScrapeOption = Menu.Tag.ToString
        ContentType = _SelectedContentType

        Select Case ScrapeOption
            Case "actors"
                ScrapeOptions.bEpisodeActors = True
                ScrapeOptions.bMainActors = True
            Case "aired"
                ScrapeOptions.bEpisodeAired = True
                ScrapeOptions.bSeasonAired = True
            Case "certifications"
                ScrapeOptions.bMainCertifications = True
            Case "collectionid"
                ScrapeOptions.bMainCollectionID = True
            Case "countries"
                ScrapeOptions.bMainCountries = True
            Case "creators"
                ScrapeOptions.bMainCreators = True
            Case "directors"
                ScrapeOptions.bEpisodeDirectors = True
                ScrapeOptions.bMainDirectors = True
            Case "epiguideurl"
                ScrapeOptions.bMainEpisodeGuide = True
            Case "genres"
                ScrapeOptions.bMainGenres = True
            Case "gueststars"
                ScrapeOptions.bEpisodeGuestStars = True
            Case "mpaa"
                ScrapeOptions.bMainMPAA = True
            Case "originaltitle"
                ScrapeOptions.bMainOriginalTitle = True
            Case "outline"
                ScrapeOptions.bMainOutline = True
            Case "plot"
                ScrapeOptions.bEpisodePlot = True
                ScrapeOptions.bMainPlot = True
                ScrapeOptions.bSeasonPlot = True
            Case "premiered"
                ScrapeOptions.bMainPremiered = True
            Case "rating"
                ScrapeOptions.bEpisodeRating = True
                ScrapeOptions.bMainRating = True
            Case "releasedate"
                ScrapeOptions.bMainRelease = True
            Case "runtime"
                ScrapeOptions.bEpisodeRuntime = True
                ScrapeOptions.bMainRuntime = True
            Case "status"
                ScrapeOptions.bMainStatus = True
            Case "studios"
                ScrapeOptions.bMainStudios = True
            Case "tagline"
                ScrapeOptions.bMainTagline = True
            Case "title"
                ScrapeOptions.bEpisodeTitle = True
                ScrapeOptions.bMainTitle = True
                ScrapeOptions.bSeasonTitle = True
            Case "top250"
                ScrapeOptions.bMainTop250 = True
            Case "trailer"
                ScrapeOptions.bMainTrailer = True
            Case "writers"
                ScrapeOptions.bEpisodeCredits = True
                ScrapeOptions.bMainWriters = True
            Case "year"
                ScrapeOptions.bMainYear = True
        End Select

        Select Case ContentType
            Case "movie"
                ScrapeModifiers.MainNFO = True
                CreateScrapeList_Movie(Enums.ScrapeType.SingleField, ScrapeOptions, ScrapeModifiers)
            Case "movieset"
                ScrapeModifiers.MainNFO = True
                CreateScrapeList_MovieSet(Enums.ScrapeType.SingleField, ScrapeOptions, ScrapeModifiers)
            Case "tvepisode"
                ScrapeModifiers.EpisodeNFO = True
                CreateScrapeList_TVEpisode(Enums.ScrapeType.SingleField, ScrapeOptions, ScrapeModifiers)
            Case "tvseason"
                ScrapeModifiers.SeasonNFO = True
                CreateScrapeList_TVSeason(Enums.ScrapeType.SingleField, ScrapeOptions, ScrapeModifiers)
            Case "tvshow"
                ScrapeModifiers.MainNFO = True
                CreateScrapeList_TV(Enums.ScrapeType.SingleField, ScrapeOptions, ScrapeModifiers)
        End Select
    End Sub

    Private Sub Mono_Shown()
        pnlNoInfo.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), Convert.ToInt32((scMain.Panel2.Height - pnlNoInfo.Height) / 2))
    End Sub
    ''' <summary>
    ''' Slide the genre images along with the panel and move with form resizing
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveGenres()
        Try
            For i As Integer = 0 To pnlGenre.Count - 1
                pnlGenre(i).Left = ((pnlInfoPanel.Right) - (i * 73)) - 73
                pnlGenre(i).Top = pnlInfoPanel.Top - 105
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Slide the MPAA image along with the panel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveMPAA()
        pnlMPAA.BringToFront()
        pnlMPAA.Size = New Size(pbMPAA.Width + 10, pbMPAA.Height + 10)
        pbMPAA.Location = New Point(4, 4)
        pnlMPAA.Top = pnlInfoPanel.Top - (pnlMPAA.Height + 10)
    End Sub

    Private Sub InfoDownloaded_Movie(ByRef DBMovie As Database.DBElement)
        If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
            tslLoading.Text = Master.eLang.GetString(576, "Verifying Movie Details:")
            Application.DoEvents()

            Edit_Movie(DBMovie, Enums.ModuleEventType.ScraperSingle_Movie)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        EnableFilters_Movies(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_Movie(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_Movie(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected movies
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In dtMovies.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim ActorThumbsAllowed As Boolean = Master.eSettings.MovieActorThumbsAnyEnabled
        Dim BannerAllowed As Boolean = Master.eSettings.MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
        Dim ClearArtAllowed As Boolean = Master.eSettings.MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
        Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
        Dim DiscArtAllowed As Boolean = Master.eSettings.MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
        Dim ExtrafanartsAllowed As Boolean = Master.eSettings.MovieExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        Dim ExtrathumbsAllowed As Boolean = Master.eSettings.MovieExtrathumbsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        Dim FanartAllowed As Boolean = Master.eSettings.MovieFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        Dim LandscapeAllowed As Boolean = Master.eSettings.MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
        Dim PosterAllowed As Boolean = Master.eSettings.MoviePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
        Dim ThemeAllowed As Boolean = Master.eSettings.MovieThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
        Dim TrailerAllowed As Boolean = Master.eSettings.MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)

        'create ScrapeList of movies acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

            Dim sModifier As New Structures.ScrapeModifiers
            sModifier.DoSearch = ScrapeModifiers.DoSearch
            sModifier.MainActorthumbs = ScrapeModifiers.MainActorthumbs AndAlso ActorThumbsAllowed
            sModifier.MainBanner = ScrapeModifiers.MainBanner AndAlso BannerAllowed
            sModifier.MainClearArt = ScrapeModifiers.MainClearArt AndAlso ClearArtAllowed
            sModifier.MainClearLogo = ScrapeModifiers.MainClearLogo AndAlso ClearLogoAllowed
            sModifier.MainDiscArt = ScrapeModifiers.MainDiscArt AndAlso DiscArtAllowed
            sModifier.MainExtrafanarts = ScrapeModifiers.MainExtrafanarts AndAlso ExtrafanartsAllowed
            sModifier.MainExtrathumbs = ScrapeModifiers.MainExtrathumbs AndAlso ExtrathumbsAllowed
            sModifier.MainFanart = ScrapeModifiers.MainFanart AndAlso FanartAllowed
            sModifier.MainLandscape = ScrapeModifiers.MainLandscape AndAlso LandscapeAllowed
            sModifier.MainMeta = ScrapeModifiers.MainMeta
            sModifier.MainNFO = ScrapeModifiers.MainNFO
            sModifier.MainPoster = ScrapeModifiers.MainPoster AndAlso PosterAllowed
            'sModifier.MainSubtitles = ScrapeModifier.MainSubtitles AndAlso SubtitlesAllowed
            sModifier.MainTheme = ScrapeModifiers.MainTheme AndAlso ThemeAllowed
            sModifier.MainTrailer = ScrapeModifiers.MainTrailer AndAlso TrailerAllowed

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = bsMovies.Find("idMovie", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.MainBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearArtPath").ToString) Then sModifier.MainClearArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearLogoPath").ToString) Then sModifier.MainClearLogo = False
                    If Not String.IsNullOrEmpty(drvRow.Item("DiscArtPath").ToString) Then sModifier.MainDiscArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("EFanartsPath").ToString) Then sModifier.MainExtrafanarts = False
                    If Not String.IsNullOrEmpty(drvRow.Item("EThumbsPath").ToString) Then sModifier.MainExtrathumbs = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.MainFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.MainLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.MainNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.MainPoster = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ThemePath").ToString) Then sModifier.MainTheme = False
                    If Not String.IsNullOrEmpty(drvRow.Item("TrailerPath").ToString) Then sModifier.MainTrailer = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        Next

        If Not ScrapeList.Count = 0 Then
            SetControlsEnabled(False)

            tspbLoading.Value = 0
            If ScrapeList.Count > 1 Then
                tspbLoading.Style = ProgressBarStyle.Continuous
                tspbLoading.Maximum = ScrapeList.Count
            Else
                tspbLoading.Maximum = 100
                tspbLoading.Style = ProgressBarStyle.Marquee
            End If

            Select Case sType
                Case Enums.ScrapeType.AllAsk
                    tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
                Case Enums.ScrapeType.AllAuto
                    tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
                Case Enums.ScrapeType.AllSkip
                    tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
                Case Enums.ScrapeType.MissingAuto
                    tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
                Case Enums.ScrapeType.MissingAsk
                    tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
                Case Enums.ScrapeType.MissingSkip
                    tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
                Case Enums.ScrapeType.NewAsk
                    tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
                Case Enums.ScrapeType.NewAuto
                    tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
                Case Enums.ScrapeType.NewSkip
                    tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
                Case Enums.ScrapeType.MarkedAsk
                    tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
                Case Enums.ScrapeType.MarkedAuto
                    tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
                Case Enums.ScrapeType.MarkedSkip
                    tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
                Case Enums.ScrapeType.FilterAsk
                    tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
                Case Enums.ScrapeType.SelectedAsk
                    tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
                Case Enums.ScrapeType.SelectedAuto
                    tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
                Case Enums.ScrapeType.SelectedSkip
                    tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
                Case Enums.ScrapeType.SingleField
                    tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
                Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                    tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
            End Select

            If Not sType = Enums.ScrapeType.SingleScrape Then
                btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                btnCancel.Visible = True
                lblCanceling.Visible = False
                prbCanceling.Visible = False
                pnlCancel.Visible = True
            End If

            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()
            bwMovieScraper.WorkerSupportsCancellation = True
            bwMovieScraper.WorkerReportsProgress = True
            bwMovieScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        End If
    End Sub

    Private Sub InfoDownloaded_MovieSet(ByRef DBMovieSet As Database.DBElement)
        If Not String.IsNullOrEmpty(DBMovieSet.ListTitle) Then
            tslLoading.Text = Master.eLang.GetString(1205, "Verifying MovieSet Details:")
            Application.DoEvents()

            Edit_MovieSet(DBMovieSet)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        EnableFilters_MovieSets(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_MovieSet(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_MovieSet(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected moviesets
                For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In dtMovieSets.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim BannerAllowed As Boolean = Master.eSettings.MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
        Dim ClearArtAllowed As Boolean = Master.eSettings.MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
        Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
        Dim DiscArtAllowed As Boolean = Master.eSettings.MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
        Dim FanartAllowed As Boolean = Master.eSettings.MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
        Dim LandscapeAllowed As Boolean = Master.eSettings.MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
        Dim PosterAllowed As Boolean = Master.eSettings.MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)

        'create ScrapeList of moviesets acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

            Dim sModifier As New Structures.ScrapeModifiers
            sModifier.DoSearch = ScrapeModifiers.DoSearch
            sModifier.MainBanner = ScrapeModifiers.MainBanner AndAlso BannerAllowed
            sModifier.MainClearArt = ScrapeModifiers.MainClearArt AndAlso ClearArtAllowed
            sModifier.MainClearLogo = ScrapeModifiers.MainClearLogo AndAlso ClearLogoAllowed
            sModifier.MainDiscArt = ScrapeModifiers.MainDiscArt AndAlso DiscArtAllowed
            sModifier.MainFanart = ScrapeModifiers.MainFanart AndAlso FanartAllowed
            sModifier.MainLandscape = ScrapeModifiers.MainLandscape AndAlso LandscapeAllowed
            sModifier.MainNFO = ScrapeModifiers.MainNFO
            sModifier.MainPoster = ScrapeModifiers.MainPoster AndAlso PosterAllowed

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = bsMovieSets.Find("idSet", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.MainBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearArtPath").ToString) Then sModifier.MainClearArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearLogoPath").ToString) Then sModifier.MainClearLogo = False
                    If Not String.IsNullOrEmpty(drvRow.Item("DiscArtPath").ToString) Then sModifier.MainDiscArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.MainFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.MainLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.MainNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.MainPoster = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        Next

        If Not ScrapeList.Count = 0 Then
            SetControlsEnabled(False)

            tspbLoading.Value = 0
            If ScrapeList.Count > 1 Then
                tspbLoading.Style = ProgressBarStyle.Continuous
                tspbLoading.Maximum = ScrapeList.Count
            Else
                tspbLoading.Maximum = 100
                tspbLoading.Style = ProgressBarStyle.Marquee
            End If

            Select Case sType
                Case Enums.ScrapeType.AllAsk
                    tslLoading.Text = Master.eLang.GetString(1215, "Scraping Media (All MovieSets - Ask):")
                Case Enums.ScrapeType.AllAuto
                    tslLoading.Text = Master.eLang.GetString(1216, "Scraping Media (All MovieSets - Auto):")
                Case Enums.ScrapeType.AllSkip
                    tslLoading.Text = Master.eLang.GetString(1217, "Scraping Media (All MovieSets - Skip):")
                Case Enums.ScrapeType.MissingAuto
                    tslLoading.Text = Master.eLang.GetString(1218, "Scraping Media (MovieSets Missing Items - Auto):")
                Case Enums.ScrapeType.MissingAsk
                    tslLoading.Text = Master.eLang.GetString(1219, "Scraping Media (MovieSets Missing Items - Ask):")
                Case Enums.ScrapeType.MissingSkip
                    tslLoading.Text = Master.eLang.GetString(1220, "Scraping Media (MovieSets Missing Items - Skip):")
                Case Enums.ScrapeType.NewAsk
                    tslLoading.Text = Master.eLang.GetString(1221, "Scraping Media (New MovieSets - Ask):")
                Case Enums.ScrapeType.NewAuto
                    tslLoading.Text = Master.eLang.GetString(1222, "Scraping Media (New MovieSets - Auto):")
                Case Enums.ScrapeType.NewSkip
                    tslLoading.Text = Master.eLang.GetString(1223, "Scraping Media (New MovieSets - Skip):")
                Case Enums.ScrapeType.MarkedAsk
                    tslLoading.Text = Master.eLang.GetString(1224, "Scraping Media (Marked MovieSets - Ask):")
                Case Enums.ScrapeType.MarkedAuto
                    tslLoading.Text = Master.eLang.GetString(1225, "Scraping Media (Marked MovieSets - Auto):")
                Case Enums.ScrapeType.MarkedSkip
                    tslLoading.Text = Master.eLang.GetString(1226, "Scraping Media (Marked MovieSets - Skip):")
                Case Enums.ScrapeType.FilterAsk
                    tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
                Case Enums.ScrapeType.AllAsk
                    tslLoading.Text = Master.eLang.GetString(1358, "Scraping Media (Selected MovieSets - Ask):")
                Case Enums.ScrapeType.AllAuto
                    tslLoading.Text = Master.eLang.GetString(1359, "Scraping Media (Selected MovieSets - Auto):")
                Case Enums.ScrapeType.AllSkip
                    tslLoading.Text = Master.eLang.GetString(1360, "Scraping Media (Selected MovieSets - Skip):")
                Case Enums.ScrapeType.SingleField
                    tslLoading.Text = Master.eLang.GetString(1357, "Scraping Media (Selected MovieSets - Single Field):")
                Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                    tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
            End Select


            If Not sType = Enums.ScrapeType.SingleScrape Then
                btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                btnCancel.Visible = True
                lblCanceling.Visible = False
                prbCanceling.Visible = False
                pnlCancel.Visible = True
            End If

            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()
            bwMovieSetScraper.WorkerSupportsCancellation = True
            bwMovieSetScraper.WorkerReportsProgress = True
            bwMovieSetScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        End If
    End Sub

    Private Sub InfoDownloaded_TV(ByRef DBTVShow As Database.DBElement)
        If DBTVShow.TVShow.TitleSpecified Then
            tslLoading.Text = Master.eLang.GetString(761, "Verifying TV Show Details:")
            Application.DoEvents()

            Edit_TVShow(DBTVShow, Enums.ModuleEventType.ScraperSingle_TVShow)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        EnableFilters_Shows(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TV(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_TV(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected tv show
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In dtTVShows.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim AllSeasonsBannerAllowed As Boolean = Master.eSettings.TVAllSeasonsBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsBanner)
        Dim AllSeasonsFanartAllowed As Boolean = Master.eSettings.TVAllSeasonsFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsFanart)
        Dim AllSeasonsLandscapeAllowed As Boolean = Master.eSettings.TVAllSeasonsLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsLandscape)
        Dim AllSeasonsPosterAllowed As Boolean = Master.eSettings.TVAllSeasonsPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsPoster)
        Dim EpisodeActorThumbsAllowed As Boolean = Master.eSettings.TVEpisodeActorThumbsAnyEnabled
        Dim EpisodeFanartAllowed As Boolean = Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
        Dim EpisodeMetaAllowed As Boolean = Master.eSettings.TVScraperMetaDataScan
        Dim EpisodePosterAllowed As Boolean = Master.eSettings.TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
        Dim MainActorThumbsAllowed As Boolean = Master.eSettings.TVShowActorThumbsAnyEnabled
        Dim MainBannerAllowed As Boolean = Master.eSettings.TVShowBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
        Dim MainCharacterArtAllowed As Boolean = Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
        Dim MainClearArtAllowed As Boolean = Master.eSettings.TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
        Dim MainClearLogoAllowed As Boolean = Master.eSettings.TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
        Dim MainExtrafanartsAllowed As Boolean = Master.eSettings.TVShowExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
        Dim MainFanartAllowed As Boolean = Master.eSettings.TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
        Dim MainLandscapeAllowed As Boolean = Master.eSettings.TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
        Dim MainPosterAllowed As Boolean = Master.eSettings.TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
        Dim MainThemeAllowed As Boolean = Master.eSettings.TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
        Dim SeasonBannerAllowed As Boolean = Master.eSettings.TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
        Dim SeasonFanartAllowed As Boolean = Master.eSettings.TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
        Dim SeasonLandscapeAllowed As Boolean = Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
        Dim SeasonPosterAllowed As Boolean = Master.eSettings.TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

        'create ScrapeList of tv shows acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

            Dim sModifier As New Structures.ScrapeModifiers
            sModifier.DoSearch = ScrapeModifiers.DoSearch
            sModifier.AllSeasonsBanner = ScrapeModifiers.AllSeasonsBanner AndAlso AllSeasonsBannerAllowed
            sModifier.AllSeasonsFanart = ScrapeModifiers.AllSeasonsFanart AndAlso AllSeasonsFanartAllowed
            sModifier.AllSeasonsLandscape = ScrapeModifiers.AllSeasonsLandscape AndAlso AllSeasonsLandscapeAllowed
            sModifier.AllSeasonsPoster = ScrapeModifiers.AllSeasonsPoster AndAlso AllSeasonsPosterAllowed
            sModifier.EpisodeActorThumbs = ScrapeModifiers.EpisodeActorThumbs AndAlso EpisodeActorThumbsAllowed
            sModifier.EpisodeFanart = ScrapeModifiers.EpisodeFanart AndAlso EpisodeFanartAllowed
            sModifier.EpisodeMeta = ScrapeModifiers.EpisodeMeta AndAlso EpisodeMetaAllowed
            sModifier.EpisodeNFO = ScrapeModifiers.EpisodeNFO
            sModifier.EpisodePoster = ScrapeModifiers.EpisodePoster AndAlso EpisodePosterAllowed
            sModifier.MainActorthumbs = ScrapeModifiers.MainActorthumbs AndAlso MainActorThumbsAllowed
            sModifier.MainBanner = ScrapeModifiers.MainBanner AndAlso MainBannerAllowed
            sModifier.MainCharacterArt = ScrapeModifiers.MainCharacterArt AndAlso MainCharacterArtAllowed
            sModifier.MainClearArt = ScrapeModifiers.MainClearArt AndAlso MainClearArtAllowed
            sModifier.MainClearLogo = ScrapeModifiers.MainClearLogo AndAlso MainClearLogoAllowed
            sModifier.MainExtrafanarts = ScrapeModifiers.MainExtrafanarts AndAlso MainExtrafanartsAllowed
            sModifier.MainFanart = ScrapeModifiers.MainFanart AndAlso MainFanartAllowed
            sModifier.MainLandscape = ScrapeModifiers.MainLandscape AndAlso MainLandscapeAllowed
            sModifier.MainNFO = ScrapeModifiers.MainNFO
            sModifier.MainPoster = ScrapeModifiers.MainPoster AndAlso MainPosterAllowed
            sModifier.MainTheme = ScrapeModifiers.MainTheme AndAlso MainThemeAllowed
            sModifier.SeasonBanner = ScrapeModifiers.SeasonBanner AndAlso SeasonBannerAllowed
            sModifier.SeasonFanart = ScrapeModifiers.SeasonFanart AndAlso SeasonFanartAllowed
            sModifier.SeasonLandscape = ScrapeModifiers.SeasonLandscape AndAlso SeasonLandscapeAllowed
            sModifier.SeasonPoster = ScrapeModifiers.SeasonPoster AndAlso SeasonPosterAllowed
            sModifier.withEpisodes = ScrapeModifiers.withEpisodes
            sModifier.withSeasons = ScrapeModifiers.withSeasons

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = bsTVShows.Find("idShow", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.MainBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("CharacterArtPath").ToString) Then sModifier.MainCharacterArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearArtPath").ToString) Then sModifier.MainClearArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearLogoPath").ToString) Then sModifier.MainClearLogo = False
                    If Not String.IsNullOrEmpty(drvRow.Item("EFanartsPath").ToString) Then sModifier.MainExtrafanarts = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.MainFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.MainLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.MainNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.MainPoster = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ThemePath").ToString) Then sModifier.MainTheme = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        Next

        If Not ScrapeList.Count = 0 Then
            SetControlsEnabled(False)

            tspbLoading.Value = 0
            If ScrapeList.Count > 1 Then
                tspbLoading.Style = ProgressBarStyle.Continuous
                tspbLoading.Maximum = ScrapeList.Count
            Else
                tspbLoading.Maximum = 100
                tspbLoading.Style = ProgressBarStyle.Marquee
            End If

            Select Case sType
                Case Enums.ScrapeType.AllAsk
                    tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
                Case Enums.ScrapeType.AllAuto
                    tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
                Case Enums.ScrapeType.AllSkip
                    tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
                Case Enums.ScrapeType.MissingAuto
                    tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
                Case Enums.ScrapeType.MissingAsk
                    tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
                Case Enums.ScrapeType.MissingSkip
                    tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
                Case Enums.ScrapeType.NewAsk
                    tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
                Case Enums.ScrapeType.NewAuto
                    tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
                Case Enums.ScrapeType.NewSkip
                    tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
                Case Enums.ScrapeType.MarkedAsk
                    tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
                Case Enums.ScrapeType.MarkedAuto
                    tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
                Case Enums.ScrapeType.MarkedSkip
                    tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
                Case Enums.ScrapeType.FilterAsk
                    tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
                Case Enums.ScrapeType.SelectedAsk
                    tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
                Case Enums.ScrapeType.SelectedAuto
                    tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
                Case Enums.ScrapeType.SelectedSkip
                    tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
                Case Enums.ScrapeType.SingleField
                    tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
                Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                    tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
            End Select

            If Not sType = Enums.ScrapeType.SingleScrape Then
                btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                btnCancel.Visible = True
                lblCanceling.Visible = False
                prbCanceling.Visible = False
                pnlCancel.Visible = True
            End If

            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()
            bwTVScraper.WorkerSupportsCancellation = True
            bwTVScraper.WorkerReportsProgress = True
            bwTVScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        End If
    End Sub

    Private Sub InfoDownloaded_TVEpisode(ByRef DBTVEpisode As Database.DBElement)
        If Not String.IsNullOrEmpty(DBTVEpisode.TVEpisode.Title) Then
            tslLoading.Text = Master.eLang.GetString(762, "Verifying TV Episode Details:")
            Application.DoEvents()

            Edit_TVEpisode(DBTVEpisode, Enums.ModuleEventType.ScraperSingle_TVEpisode)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        EnableFilters_Shows(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TVEpisode(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_TVEpisode(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected tv episode
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In dtTVEpisodes.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim ActorThumbsAllowed As Boolean = Master.eSettings.TVEpisodeActorThumbsAnyEnabled
        Dim FanartAllowed As Boolean = Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso (ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart) OrElse
                                                                                           ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart))
        Dim PosterAllowed As Boolean = Master.eSettings.TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)

        'create ScrapeList of episodes acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

            Dim sModifier As New Structures.ScrapeModifiers
            sModifier.DoSearch = ScrapeModifiers.DoSearch
            sModifier.EpisodeActorThumbs = ScrapeModifiers.EpisodeActorThumbs AndAlso ActorThumbsAllowed
            sModifier.EpisodeFanart = ScrapeModifiers.EpisodeFanart AndAlso FanartAllowed
            sModifier.EpisodeMeta = ScrapeModifiers.EpisodeMeta
            sModifier.EpisodeNFO = ScrapeModifiers.EpisodeNFO
            sModifier.EpisodePoster = ScrapeModifiers.EpisodePoster AndAlso PosterAllowed

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = bsTVEpisodes.Find("idEpisode", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.EpisodeFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.EpisodeNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.EpisodePoster = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        Next

        If Not ScrapeList.Count = 0 Then
            SetControlsEnabled(False)

            tspbLoading.Value = 0
            If ScrapeList.Count > 1 Then
                tspbLoading.Style = ProgressBarStyle.Continuous
                tspbLoading.Maximum = ScrapeList.Count
            Else
                tspbLoading.Maximum = 100
                tspbLoading.Style = ProgressBarStyle.Marquee
            End If

            Select Case sType
                Case Enums.ScrapeType.AllAsk
                    tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
                Case Enums.ScrapeType.AllAuto
                    tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
                Case Enums.ScrapeType.AllSkip
                    tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
                Case Enums.ScrapeType.MissingAuto
                    tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
                Case Enums.ScrapeType.MissingAsk
                    tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
                Case Enums.ScrapeType.MissingSkip
                    tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
                Case Enums.ScrapeType.NewAsk
                    tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
                Case Enums.ScrapeType.NewAuto
                    tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
                Case Enums.ScrapeType.NewSkip
                    tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
                Case Enums.ScrapeType.MarkedAsk
                    tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
                Case Enums.ScrapeType.MarkedAuto
                    tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
                Case Enums.ScrapeType.MarkedSkip
                    tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
                Case Enums.ScrapeType.FilterAsk
                    tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
                Case Enums.ScrapeType.AllAsk
                    tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
                Case Enums.ScrapeType.AllAuto
                    tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
                Case Enums.ScrapeType.AllSkip
                    tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
                Case Enums.ScrapeType.SingleField
                    tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
                Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                    tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
            End Select

            If Not sType = Enums.ScrapeType.SingleScrape Then
                btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                btnCancel.Visible = True
                lblCanceling.Visible = False
                prbCanceling.Visible = False
                pnlCancel.Visible = True
            End If

            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()
            bwTVEpisodeScraper.WorkerSupportsCancellation = True
            bwTVEpisodeScraper.WorkerReportsProgress = True
            bwTVEpisodeScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        End If
    End Sub

    Private Sub InfoDownloaded_TVSeason(ByRef DBTVSeason As Database.DBElement)
        If Not String.IsNullOrEmpty(DBTVSeason.TVShow.Title) Then
            tslLoading.Text = Master.eLang.GetString(80, "Verifying TV Season Details:")
            Application.DoEvents()

            Edit_TVSeason(DBTVSeason, Enums.ModuleEventType.ScraperSingle_TVSeason)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        EnableFilters_Shows(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TVSeason(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_TVSeason(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected tv season
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In dtTVSeasons.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim AllSeasonsBannerAllowed As Boolean = Master.eSettings.TVAllSeasonsBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsBanner)
        Dim AllSeasonsFanartAllowed As Boolean = Master.eSettings.TVAllSeasonsFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsFanart)
        Dim AllSeasonsLandscapeAllowed As Boolean = Master.eSettings.TVAllSeasonsLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsLandscape)
        Dim AllSeasonsPosterAllowed As Boolean = Master.eSettings.TVAllSeasonsPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsPoster)
        Dim SeasonBannerAllowed As Boolean = Master.eSettings.TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
        Dim SeasonFanartAllowed As Boolean = Master.eSettings.TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
        Dim SeasonLandscapeAllowed As Boolean = Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
        Dim SeasonPosterAllowed As Boolean = Master.eSettings.TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

        'create ScrapeList of tv seasons acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

            Dim sModifier As New Structures.ScrapeModifiers
            sModifier.DoSearch = ScrapeModifiers.DoSearch
            sModifier.AllSeasonsBanner = ScrapeModifiers.AllSeasonsBanner AndAlso AllSeasonsBannerAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.AllSeasonsFanart = ScrapeModifiers.AllSeasonsFanart AndAlso AllSeasonsFanartAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.AllSeasonsLandscape = ScrapeModifiers.AllSeasonsLandscape AndAlso AllSeasonsLandscapeAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.AllSeasonsPoster = ScrapeModifiers.AllSeasonsPoster AndAlso AllSeasonsPosterAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonBanner = ScrapeModifiers.SeasonBanner AndAlso SeasonBannerAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonFanart = ScrapeModifiers.SeasonFanart AndAlso SeasonFanartAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonLandscape = ScrapeModifiers.SeasonLandscape AndAlso SeasonLandscapeAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonNFO = ScrapeModifiers.SeasonNFO
            sModifier.SeasonPoster = ScrapeModifiers.SeasonPoster AndAlso SeasonPosterAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = bsTVShows.Find("idShow", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.SeasonBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.SeasonFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.SeasonLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.SeasonPoster = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        Next

        If Not ScrapeList.Count = 0 Then
            SetControlsEnabled(False)

            tspbLoading.Value = 0
            If ScrapeList.Count > 1 Then
                tspbLoading.Style = ProgressBarStyle.Continuous
                tspbLoading.Maximum = ScrapeList.Count
            Else
                tspbLoading.Maximum = 100
                tspbLoading.Style = ProgressBarStyle.Marquee
            End If

            Select Case sType
                Case Enums.ScrapeType.AllAsk
                    tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
                Case Enums.ScrapeType.AllAuto
                    tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
                Case Enums.ScrapeType.AllSkip
                    tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
                Case Enums.ScrapeType.MissingAuto
                    tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
                Case Enums.ScrapeType.MissingAsk
                    tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
                Case Enums.ScrapeType.MissingSkip
                    tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
                Case Enums.ScrapeType.NewAsk
                    tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
                Case Enums.ScrapeType.NewAuto
                    tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
                Case Enums.ScrapeType.NewSkip
                    tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
                Case Enums.ScrapeType.MarkedAsk
                    tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
                Case Enums.ScrapeType.MarkedAuto
                    tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
                Case Enums.ScrapeType.MarkedSkip
                    tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
                Case Enums.ScrapeType.FilterAsk
                    tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
                Case Enums.ScrapeType.FilterAuto
                    tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
                Case Enums.ScrapeType.SelectedAsk
                    tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
                Case Enums.ScrapeType.SelectedAuto
                    tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
                Case Enums.ScrapeType.SelectedSkip
                    tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
                Case Enums.ScrapeType.SingleField
                    tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
                Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                    tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
            End Select

            If Not sType = Enums.ScrapeType.SingleScrape Then
                btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                btnCancel.Visible = True
                lblCanceling.Visible = False
                prbCanceling.Visible = False
                pnlCancel.Visible = True
            End If

            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()
            bwTVSeasonScraper.WorkerSupportsCancellation = True
            bwTVSeasonScraper.WorkerReportsProgress = True
            bwTVSeasonScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        End If
    End Sub

    Function MyResolveEventHandler(ByVal sender As Object, ByVal args As ResolveEventArgs) As [Assembly]
        Dim name As String = args.Name.Split(Convert.ToChar(","))(0)
        Dim asm As Assembly = ModulesManager.AssemblyList.FirstOrDefault(Function(y) y.AssemblyName = name).Assembly
        If asm Is Nothing Then
            asm = ModulesManager.AssemblyList.FirstOrDefault(Function(y) y.AssemblyName = name.Split(Convert.ToChar("."))(0)).Assembly
        End If
        Return asm
    End Function

    Private Sub cmnuMovieOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieOpenFolder.Click
        If dgvMovies.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvMovies.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    Using Explorer As New Diagnostics.Process

                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", sRow.Cells("MoviePath").Value)
                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            Explorer.StartInfo.Arguments = String.Format("""{0}""", Path.GetDirectoryName(sRow.Cells("MoviePath").Value.ToString))
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Disable IMDB/TMDBID/TVDB menutitem if selected episodes don't have IMDBID/TMDBID/TVDB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuEpisode_Opened(sender As Object, e As EventArgs) Handles cmnuEpisode.Opened
        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim enableIMDB As Boolean = False
            Dim enableTMDB As Boolean = False
            Dim enableTVDB As Boolean = False
            For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                If Not String.IsNullOrEmpty(sRow.Cells("strIMDB").Value.ToString) Then
                    enableIMDB = True
                End If
                If Not String.IsNullOrEmpty(sRow.Cells("strTMDB").Value.ToString) Then
                    enableTMDB = True
                End If
                If Not String.IsNullOrEmpty(sRow.Cells("strTVDB").Value.ToString) Then
                    enableTVDB = True
                End If
            Next
            cmnuEpisodeBrowseIMDB.Enabled = enableIMDB
            cmnuEpisodeBrowseTMDB.Enabled = enableTMDB
            cmnuEpisodeBrowseTVDB.Enabled = enableTVDB
        End If
    End Sub
    ''' <summary>
    ''' Disable IMDB/TMDB menutitem if selected movies don't have IMDBID/TMDBID
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovie_Opened(sender As Object, e As EventArgs) Handles cmnuMovie.Opened
        If dgvMovies.SelectedRows.Count > 0 Then
            Dim enableIMDB As Boolean = False
            Dim enableTMDB As Boolean = False
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                If Not String.IsNullOrEmpty(sRow.Cells("Imdb").Value.ToString) Then
                    enableIMDB = True
                End If
                If Not String.IsNullOrEmpty(sRow.Cells("TMDB").Value.ToString) Then
                    enableTMDB = True
                End If
            Next
            cmnuMovieBrowseIMDB.Enabled = enableIMDB
            cmnuMovieBrowseTMDB.Enabled = enableTMDB
        End If
    End Sub
    ''' <summary>
    ''' Disable TMDB menutitem if selected moviesets don't have TMDBColID
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovieSet_Opened(sender As Object, e As EventArgs) Handles cmnuMovieSet.Opened
        If dgvMovieSets.SelectedRows.Count > 0 Then
            Dim enableTMDB As Boolean = False
            For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                If Not String.IsNullOrEmpty(sRow.Cells("TMDBColID").Value.ToString) Then
                    enableTMDB = True
                End If
            Next
            cmnuMovieSetBrowseTMDB.Enabled = enableTMDB
        End If
    End Sub
    ''' <summary>
    ''' Disable IMDB/TMDBID/TVDB menutitem if selected tvshow don't have IMDBID/TMDBID/TVDB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuSeason_Opened(sender As Object, e As EventArgs) Handles cmnuSeason.Opened
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim enableIMDB As Boolean = False
            Dim enableTMDB As Boolean = False
            Dim enableTVDB As Boolean = False
            If Not CInt(dgvTVSeasons.SelectedRows(0).Cells("season").Value) = 999 Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    If Not String.IsNullOrEmpty(dgvTVShows.SelectedRows(0).Cells("strIMDB").Value.ToString) Then
                        enableIMDB = True
                    End If
                    If Not String.IsNullOrEmpty(sRow.Cells("strTMDB").Value.ToString) Then
                        enableTMDB = True
                    End If
                    If Not String.IsNullOrEmpty(sRow.Cells("strTVDB").Value.ToString) Then
                        enableTVDB = True
                    End If
                Next
            End If
            cmnuSeasonBrowseIMDB.Enabled = enableIMDB
            cmnuSeasonBrowseTMDB.Enabled = enableTMDB
            cmnuSeasonBrowseTVDB.Enabled = enableTVDB
        End If
    End Sub
    ''' <summary>
    ''' Disable IMDB/TMDBID/TVDB menutitem if selected tvshow don't have IMDBID/TMDBID/TVDB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuShow_Opened(sender As Object, e As EventArgs) Handles cmnuShow.Opened
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim enableIMDB As Boolean = False
            Dim enableTMDB As Boolean = False
            Dim enableTVDB As Boolean = False
            For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                If Not String.IsNullOrEmpty(sRow.Cells("strIMDB").Value.ToString) Then
                    enableIMDB = True
                End If
                If Not String.IsNullOrEmpty(sRow.Cells("strTMDB").Value.ToString) Then
                    enableTMDB = True
                End If
                If Not String.IsNullOrEmpty(sRow.Cells("TVDB").Value.ToString) Then
                    enableTVDB = True
                End If
            Next
            cmnuShowBrowseIMDB.Enabled = enableIMDB
            cmnuShowBrowseTMDB.Enabled = enableTMDB
            cmnuShowBrowseTVDB.Enabled = enableTVDB
        End If
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected episode(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuEpisodeBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuEpisodeBrowseIMDB.Click
        Try
            If dgvTVEpisodes.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVEpisodes.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If

                If doOpen Then
                    Dim tmpstring As String = String.Empty
                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("strIMDB").Value.ToString) Then
                            tmpstring = sRow.Cells("strIMDB").Value.ToString.Replace("tt", String.Empty)
                            If Not My.Resources.urlIMDB.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "/title/tt", tmpstring))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "title/tt", tmpstring))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected episode(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuEpisodeBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuEpisodeBrowseTMDB.Click
        Try
            If dgvTVEpisodes.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVEpisodes.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    Dim ShowID As String = dgvTVShows.SelectedRows(0).Cells("strTMDB").Value.ToString
                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                            Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "/tv/", ShowID, "/season/", sRow.Cells("Season").Value.ToString, "/episode/", sRow.Cells("Episode").Value.ToString))
                        Else
                            Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "tv/", ShowID, "/season/", sRow.Cells("Season").Value.ToString, "/episode/", sRow.Cells("Episode").Value.ToString))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TVDB-Page of selected episode(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuEpisodeBrowseTVDB_Click(sender As Object, e As EventArgs) Handles cmnuEpisodeBrowseTVDB.Click
        Try
            If dgvTVEpisodes.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVEpisodes.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    Dim ShowID As String = dgvTVShows.SelectedRows(0).Cells("TVDB").Value.ToString
                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("strTVDB").Value.ToString) Then
                            If Not My.Resources.urlTVDB.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlTVDB, "/?tab=episode&seriesid=", ShowID & "&id=", sRow.Cells("strTVDB").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlTVDB, "?tab=episode&seriesid=", ShowID & "&id=", sRow.Cells("strTVDB").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected movie(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovieBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieBrowseIMDB.Click
        Try
            If dgvMovies.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvMovies.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If

                If doOpen Then
                    Dim tmpstring As String = String.Empty
                    For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("Imdb").Value.ToString) Then
                            tmpstring = sRow.Cells("Imdb").Value.ToString.Replace("tt", String.Empty)
                            If Not My.Resources.urlIMDB.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "/title/tt", tmpstring))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "title/tt", tmpstring))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected movie(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovieBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieBrowseTMDB.Click
        Try
            If dgvMovies.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvMovies.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("TMDB").Value.ToString) Then
                            If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "/movie/", sRow.Cells("TMDB").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "movie/", sRow.Cells("TMDB").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected movieset(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovieSetBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieSetBrowseTMDB.Click
        Try
            If dgvMovieSets.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvMovieSets.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMovieSets.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("TMDBColID").Value.ToString) Then
                            If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "/collection/", sRow.Cells("TMDBColID").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "collection/", sRow.Cells("TMDBColID").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected season(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuSeasonBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuSeasonBrowseIMDB.Click
        Try
            If dgvTVSeasons.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVSeasons.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If

                If doOpen Then
                    Dim ShowID As String = dgvTVShows.SelectedRows(0).Cells("strIMDB").Value.ToString
                    For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                        If Not String.IsNullOrEmpty(ShowID) Then
                            If Not My.Resources.urlIMDB.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "/title/", ShowID, "/episodes?season=", sRow.Cells("Season").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "title/", ShowID, "/episodes?season=", sRow.Cells("Season").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected season(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuSeasonBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuSeasonBrowseTMDB.Click
        Try
            If dgvTVSeasons.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVSeasons.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    Dim ShowID As String = dgvTVShows.SelectedRows(0).Cells("strTMDB").Value.ToString
                    For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                        If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                            Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "/tv/", ShowID, "/season/", sRow.Cells("Season").Value.ToString))
                        Else
                            Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "tv/", ShowID, "/season/", sRow.Cells("Season").Value.ToString))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TVDB-Page of selected seasons(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuSeasonBrowseTVDB_Click(sender As Object, e As EventArgs) Handles cmnuSeasonBrowseTVDB.Click
        Try
            If dgvTVSeasons.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVSeasons.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    Dim ShowID As String = dgvTVShows.SelectedRows(0).Cells("TVDB").Value.ToString
                    For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("strTVDB").Value.ToString) Then
                            If Not My.Resources.urlTVDB.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlTVDB, "/?tab=season&seriesid=", ShowID & "&seasonid=", sRow.Cells("strTVDB").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlTVDB, "?tab=season&seriesid=", ShowID, "&seasonid=", sRow.Cells("strTVDB").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected show(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuShowBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuShowBrowseIMDB.Click
        Try
            If dgvTVShows.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVShows.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If

                If doOpen Then
                    Dim tmpstring As String = String.Empty
                    For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("strIMDB").Value.ToString) Then
                            If Not My.Resources.urlIMDB.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "/title/", sRow.Cells("strIMDB").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlIMDB, "title/", sRow.Cells("strIMDB").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected tvshow(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuShowBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuShowBrowseTMDB.Click
        Try
            If dgvTVShows.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVShows.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("strTMDB").Value.ToString) Then
                            If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "/tv/", sRow.Cells("strTMDB").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlTheMovieDb, "tv/", sRow.Cells("strTMDB").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Open TVDB-Page of selected tvshow(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuShowBrowseTVDB_Click(sender As Object, e As EventArgs) Handles cmnuShowBrowseTVDB.Click
        Try
            If dgvTVShows.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If dgvTVShows.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("TVDB").Value.ToString) Then
                            If Not My.Resources.urlTVDB.EndsWith("/") Then
                                Functions.Launch(String.Concat(My.Resources.urlTVDB & "/?tab=series&id=" & sRow.Cells("TVDB").Value.ToString))
                            Else
                                Functions.Launch(String.Concat(My.Resources.urlTVDB & "?tab=series&id=" & sRow.Cells("TVDB").Value.ToString))
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub OpenImageViewer(ByVal _Image As Image)
        Using dImgView As New dlgImgView
            dImgView.ShowDialog(_Image)
        End Using
    End Sub
    ''' <summary>
    ''' Draw genre text over the image when mouse hovers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbGenre_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        If Master.eSettings.GeneralShowGenresText Then Return 'Because Image already has genre text displayed
        GenreImage = DirectCast(sender, PictureBox).Image    'Store the image for later retrieval
        DirectCast(sender, PictureBox).Image = ImageUtils.AddGenreString(DirectCast(sender, PictureBox).Image, DirectCast(sender, PictureBox).Name.ToString)
    End Sub
    ''' <summary>
    ''' Reset genre image when mouse leaves to "clear" the text
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbGenre_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        If Master.eSettings.GeneralShowGenresText Then Return
        DirectCast(sender, PictureBox).Image = GenreImage
    End Sub

    Private Sub pbBanner_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbBanner.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbBannerCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbBannerCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        If dgvMovies.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                            If aContainer.MainBanners.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                    Master.DB.Save_Movie(tmpDBElement, False, False, True, False)
                                    RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.MovieSet
                        If dgvMovieSets.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
                            If aContainer.MainBanners.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                    Master.DB.Save_MovieSet(tmpDBElement, False, True)
                                    RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            If dgvTVShows.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.MainBanners.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                        Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
                                        RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            If dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsBanner, True)
                            Else
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonBanner, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.SeasonBanners.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainBanners.Count > 0) Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                        Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
                                        RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbCharacterArt_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbCharacterArt.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbCharacterArtCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbCharacterArtCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        Return
                    Case Enums.ContentType.MovieSet
                        Return
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            If dgvTVShows.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainCharacterArt, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.MainCharacterArts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.CharacterArt = dlgImgS.Result.ImagesContainer.CharacterArt
                                        Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
                                        RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbClearArt_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbClearArt.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbClearArtCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbClearArtCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        If dgvMovies.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                            If aContainer.MainClearArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                                    Master.DB.Save_Movie(tmpDBElement, False, False, True, False)
                                    RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.MovieSet
                        If dgvMovieSets.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
                            If aContainer.MainClearArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                                    Master.DB.Save_MovieSet(tmpDBElement, False, True)
                                    RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            If dgvTVShows.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.MainClearArts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                                        Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
                                        RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbClearLogo_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbClearLogo.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbClearLogoCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbClearLogoCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        If dgvMovies.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                            If aContainer.MainClearLogos.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                                    Master.DB.Save_Movie(tmpDBElement, False, False, True, False)
                                    RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.MovieSet
                        If dgvMovieSets.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
                            If aContainer.MainClearLogos.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                                    Master.DB.Save_MovieSet(tmpDBElement, False, True)
                                    RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            If dgvTVShows.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.MainClearLogos.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                                        Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
                                        RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbDiscArt_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbDiscArt.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbDiscArtCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbDiscArtCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        If dgvMovies.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                            If aContainer.MainDiscArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
                                    Master.DB.Save_Movie(tmpDBElement, False, False, True, False)
                                    RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.MovieSet
                        If dgvMovieSets.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
                            If aContainer.MainDiscArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
                                    Master.DB.Save_MovieSet(tmpDBElement, False, True)
                                    RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            Return

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub
    ''' <summary>
    ''' Show the Fanart in the Image Viewer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbFanart_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbFanart.MouseDoubleClick, pbFanartSmall.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbFanartCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbFanartCache.Image)
                    End Using
                ElseIf pbFanartSmallCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbFanartSmallCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        If dgvMovies.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                            If aContainer.MainFanarts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                    Master.DB.Save_Movie(tmpDBElement, False, False, True, False)
                                    RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.MovieSet
                        If dgvMovieSets.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
                            If aContainer.MainFanarts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                    Master.DB.Save_MovieSet(tmpDBElement, False, True)
                                    RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            If dgvTVShows.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.MainFanarts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                        Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
                                        RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            If dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsFanart, True)
                            Else
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonFanart, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.SeasonFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                        Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
                                        RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            If dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeFanart, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.EpisodeFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                        Master.DB.Save_TVEpisode(tmpDBElement, False, False, True, False, True)
                                        RefreshRow_TVEpisode(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbLandscape_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbLandscape.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbLandscapeCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbLandscapeCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        If dgvMovies.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                            If aContainer.MainLandscapes.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                    Master.DB.Save_Movie(tmpDBElement, False, False, True, False)
                                    RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.MovieSet
                        If dgvMovieSets.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
                            If aContainer.MainLandscapes.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                    Master.DB.Save_MovieSet(tmpDBElement, False, True)
                                    RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            If dgvTVShows.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.MainLandscapes.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                        Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
                                        RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            If dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsLandscape, True)
                            Else
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonLandscape, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.SeasonLandscapes.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainLandscapes.Count > 0) Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                        Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
                                        RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbPoster_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPoster.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If pbPosterCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(pbPosterCache.Image)
                    End Using
                End If
            ElseIf e.Button = MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then

                Select Case DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType).ContentType
                    Case Enums.ContentType.Movie
                        If dgvMovies.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                            If aContainer.MainPosters.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                    Master.DB.Save_Movie(tmpDBElement, False, False, True, False)
                                    RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.MovieSet
                        If dgvMovieSets.SelectedRows.Count > 1 Then Return
                        SetControlsEnabled(False)

                        Dim indX As Integer = dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Long = Convert.ToInt64(dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
                            If aContainer.MainPosters.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                    Master.DB.Save_MovieSet(tmpDBElement, False, True)
                                    RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        SetControlsEnabled(True)
                    Case Enums.ContentType.TV
                        'TV Show list
                        If dgvTVShows.Focused Then
                            If dgvTVShows.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.MainPosters.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                        Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
                                        RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Season list
                        ElseIf dgvTVSeasons.Focused Then
                            If dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsPoster, True)
                            Else
                                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonPoster, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.SeasonPosters.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainPosters.Count > 0) Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                        Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
                                        RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf dgvTVEpisodes.Focused Then
                            If dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            SetControlsEnabled(False)

                            Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
                            Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifiers As New Structures.ScrapeModifiers

                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodePoster, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
                                If aContainer.EpisodePosters.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                        Master.DB.Save_TVEpisode(tmpDBElement, False, False, True, False, True)
                                        RefreshRow_TVEpisode(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub rbFilterAnd_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterAnd_Movies.Click
        If clbFilterGenres_Movies.CheckedItems.Count > 0 Then
            txtFilterGenre_Movies.Text = String.Empty
            FilterArray_Movies.Remove(filGenre_Movies)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Movies.CheckedItems.OfType(Of String).ToList)

            txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            filGenre_Movies = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            FilterArray_Movies.Add(filGenre_Movies)
        End If

        If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
            txtFilterCountry_Movies.Text = String.Empty
            FilterArray_Movies.Remove(filCountry_Movies)

            Dim alCountries As New List(Of String)
            alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

            txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

            For i As Integer = 0 To alCountries.Count - 1
                If alCountries.Item(i) = Master.eLang.None Then
                    alCountries.Item(i) = "Country LIKE ''"
                Else
                    alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
                End If
            Next

            filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

            FilterArray_Movies.Add(filCountry_Movies)
        End If

        If clbFilterDataFields_Movies.CheckedItems.Count > 0 Then
            txtFilterDataField_Movies.Text = String.Empty
            FilterArray_Movies.Remove(filDataField_Movies)

            Dim alDataFields As New List(Of String)
            alDataFields.AddRange(clbFilterDataFields_Movies.CheckedItems.OfType(Of String).ToList)

            txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND ")

            For i As Integer = 0 To alDataFields.Count - 1
                If cbFilterDataField_Movies.SelectedIndex = 0 Then
                    alDataFields.Item(i) = String.Format("{0} IS NULL OR {0} = ''", alDataFields.Item(i))
                Else
                    alDataFields.Item(i) = String.Format("{0} NOT IS NULL AND {0} NOT = ''", alDataFields.Item(i))
                End If
            Next

            filDataField_Movies = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND ")

            FilterArray_Movies.Add(filDataField_Movies)
        End If

        If (Not String.IsNullOrEmpty(cbFilterYearFrom_Movies.Text) AndAlso Not cbFilterYearFrom_Movies.Text = Master.eLang.All) OrElse
            (Not String.IsNullOrEmpty(cbFilterYearTo_Movies.Text) AndAlso Not cbFilterYearTo_Movies.Text = Master.eLang.All) OrElse
            clbFilterGenres_Movies.CheckedItems.Count > 0 OrElse clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse
            clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse chkFilterMark_Movies.Checked OrElse
            chkFilterMarkCustom1_Movies.Checked OrElse chkFilterMarkCustom2_Movies.Checked OrElse chkFilterMarkCustom3_Movies.Checked OrElse
            chkFilterMarkCustom4_Movies.Checked OrElse chkFilterNew_Movies.Checked OrElse chkFilterLock_Movies.Checked OrElse
            Not clbFilterSources_Movies.CheckedItems.Count > 0 OrElse chkFilterDuplicates_Movies.Checked OrElse
            chkFilterMissing_Movies.Checked OrElse chkFilterTolerance_Movies.Checked OrElse Not cbFilterVideoSource_Movies.Text = Master.eLang.All Then RunFilter_Movies()
    End Sub

    Private Sub rbFilterAnd_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterAnd_MovieSets.Click
        If chkFilterEmpty_MovieSets.Checked OrElse chkFilterMark_MovieSets.Checked OrElse chkFilterNew_MovieSets.Checked OrElse chkFilterLock_MovieSets.Checked OrElse
            chkFilterMissing_MovieSets.Checked OrElse chkFilterMultiple_MovieSets.Checked OrElse chkFilterOne_MovieSets.Checked Then RunFilter_MovieSets()
    End Sub

    Private Sub rbFilterAnd_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterAnd_Shows.Click
        If clbFilterGenres_Shows.CheckedItems.Count > 0 Then
            txtFilterGenre_Shows.Text = String.Empty
            FilterArray_TVShows.Remove(filGenre_TVShows)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Shows.CheckedItems.OfType(Of String).ToList)

            txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            filGenre_TVShows = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            FilterArray_TVShows.Add(filGenre_TVShows)
        End If

        'If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
        '    Me.txtFilterCountry_Movies.Text = String.Empty
        '    Me.FilterArray_Movies.Remove(Me.filCountry_Movies)

        '    Dim alCountries As New List(Of String)
        '    alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

        '    Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

        '    For i As Integer = 0 To alCountries.Count - 1
        '        If alCountries.Item(i) = Master.eLang.None Then
        '            alCountries.Item(i) = "Country LIKE ''"
        '        Else
        '            alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
        '        End If
        '    Next

        '    Me.filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

        '    Me.FilterArray_Movies.Add(Me.filCountry_Movies)
        'End If

        If clbFilterGenres_Shows.CheckedItems.Count > 0 OrElse chkFilterMark_Shows.Checked OrElse chkFilterNewEpisodes_Shows.Checked OrElse
            chkFilterNewShows_Shows.Checked OrElse chkFilterLock_Shows.Checked OrElse Not clbFilterSource_Shows.CheckedItems.Count > 0 OrElse
            chkFilterMissing_Shows.Checked Then RunFilter_Shows()
    End Sub

    Private Sub rbFilterOr_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterOr_Movies.Click
        If clbFilterGenres_Movies.CheckedItems.Count > 0 Then
            txtFilterGenre_Movies.Text = String.Empty
            FilterArray_Movies.Remove(filGenre_Movies)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Movies.CheckedItems.OfType(Of String).ToList)

            txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            filGenre_Movies = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            FilterArray_Movies.Add(filGenre_Movies)
        End If

        If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
            txtFilterCountry_Movies.Text = String.Empty
            FilterArray_Movies.Remove(filCountry_Movies)

            Dim alCountries As New List(Of String)
            alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

            txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

            For i As Integer = 0 To alCountries.Count - 1
                If alCountries.Item(i) = Master.eLang.None Then
                    alCountries.Item(i) = "Country LIKE ''"
                Else
                    alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
                End If
            Next

            filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

            FilterArray_Movies.Add(filCountry_Movies)
        End If

        If clbFilterDataFields_Movies.CheckedItems.Count > 0 Then
            txtFilterDataField_Movies.Text = String.Empty
            FilterArray_Movies.Remove(filDataField_Movies)

            Dim alDataFields As New List(Of String)
            alDataFields.AddRange(clbFilterDataFields_Movies.CheckedItems.OfType(Of String).ToList)

            txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR ")

            For i As Integer = 0 To alDataFields.Count - 1
                If cbFilterDataField_Movies.SelectedIndex = 0 Then
                    alDataFields.Item(i) = String.Format("{0} IS NULL OR {0} = ''", alDataFields.Item(i))
                Else
                    alDataFields.Item(i) = String.Format("{0} NOT IS NULL AND {0} NOT = ''", alDataFields.Item(i))
                End If
            Next

            filDataField_Movies = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR ")

            FilterArray_Movies.Add(filDataField_Movies)
        End If

        If (Not String.IsNullOrEmpty(cbFilterYearFrom_Movies.Text) AndAlso Not cbFilterYearFrom_Movies.Text = Master.eLang.All) OrElse
            (Not String.IsNullOrEmpty(cbFilterYearTo_Movies.Text) AndAlso Not cbFilterYearTo_Movies.Text = Master.eLang.All) OrElse
            clbFilterGenres_Movies.CheckedItems.Count > 0 OrElse clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse
            clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse chkFilterMark_Movies.Checked OrElse
            chkFilterMarkCustom1_Movies.Checked OrElse chkFilterMarkCustom2_Movies.Checked OrElse chkFilterMarkCustom3_Movies.Checked OrElse
            chkFilterMarkCustom4_Movies.Checked OrElse chkFilterNew_Movies.Checked OrElse chkFilterLock_Movies.Checked OrElse
            Not clbFilterSources_Movies.CheckedItems.Count > 0 OrElse chkFilterDuplicates_Movies.Checked OrElse
            chkFilterMissing_Movies.Checked OrElse chkFilterTolerance_Movies.Checked OrElse Not cbFilterVideoSource_Movies.Text = Master.eLang.All Then RunFilter_Movies()
    End Sub

    Private Sub rbFilterOr_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterOr_MovieSets.Click
        If chkFilterEmpty_MovieSets.Checked OrElse chkFilterMark_MovieSets.Checked OrElse chkFilterNew_MovieSets.Checked OrElse chkFilterLock_MovieSets.Checked OrElse
            chkFilterMissing_MovieSets.Checked OrElse chkFilterMultiple_MovieSets.Checked OrElse chkFilterOne_MovieSets.Checked Then RunFilter_MovieSets()
    End Sub

    Private Sub rbFilterOr_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterOr_Shows.Click
        If clbFilterGenres_Shows.CheckedItems.Count > 0 Then
            txtFilterGenre_Shows.Text = String.Empty
            FilterArray_TVShows.Remove(filGenre_TVShows)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Shows.CheckedItems.OfType(Of String).ToList)

            txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            filGenre_TVShows = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            FilterArray_TVShows.Add(filGenre_TVShows)
        End If

        'If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
        '    Me.txtFilterCountry_Movies.Text = String.Empty
        '    Me.FilterArray_Movies.Remove(Me.filCountry_Movies)

        '    Dim alCountries As New List(Of String)
        '    alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

        '    Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

        '    For i As Integer = 0 To alCountries.Count - 1
        '        If alCountries.Item(i) = Master.eLang.None Then
        '            alCountries.Item(i) = "Country LIKE ''"
        '        Else
        '            alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
        '        End If
        '    Next

        '    Me.filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

        '    Me.FilterArray_Movies.Add(Me.filCountry_Movies)
        'End If

        If clbFilterGenres_Shows.CheckedItems.Count > 0 OrElse chkFilterMark_Shows.Checked OrElse chkFilterNewEpisodes_Shows.Checked OrElse
            chkFilterNewShows_Shows.Checked OrElse chkFilterLock_Shows.Checked OrElse Not clbFilterSource_Shows.CheckedItems.Count > 0 OrElse
            chkFilterMissing_Shows.Checked Then RunFilter_Shows()
    End Sub

    Private Sub ReloadAll_Movie()
        If dtMovies.Rows.Count > 0 Then
            Cursor = Cursors.WaitCursor
            SetControlsEnabled(False, True)
            tspbLoading.Style = ProgressBarStyle.Continuous
            EnableFilters_Movies(False)
            EnableFilters_MovieSets(False)
            EnableFilters_Shows(False)

            tspbLoading.Maximum = dtMovies.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwReload_Movies.WorkerReportsProgress = True
            bwReload_Movies.WorkerSupportsCancellation = True
            bwReload_Movies.RunWorkerAsync()
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub ReloadAll_MovieSet()
        If dtMovieSets.Rows.Count > 0 Then
            Cursor = Cursors.WaitCursor
            SetControlsEnabled(False, True)
            tspbLoading.Style = ProgressBarStyle.Continuous
            EnableFilters_Movies(False)
            EnableFilters_MovieSets(False)
            EnableFilters_Shows(False)

            tspbLoading.Maximum = dtMovieSets.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwReload_MovieSets.WorkerReportsProgress = True
            bwReload_MovieSets.WorkerSupportsCancellation = True
            bwReload_MovieSets.RunWorkerAsync()
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub ReloadAll_TVShow(ByVal reloadFull As Boolean)
        If dtTVShows.Rows.Count > 0 Then
            Cursor = Cursors.WaitCursor
            SetControlsEnabled(False, True)
            tspbLoading.Style = ProgressBarStyle.Continuous
            EnableFilters_Movies(False)
            EnableFilters_MovieSets(False)
            EnableFilters_Shows(False)

            tspbLoading.Maximum = dtTVShows.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwReload_TVShows.WorkerReportsProgress = True
            bwReload_TVShows.WorkerSupportsCancellation = True
            bwReload_TVShows.RunWorkerAsync(reloadFull)
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub RewriteAll_Movie()
        If dtMovies.Rows.Count > 0 Then
            SetControlsEnabled(False)
            tspbLoading.Style = ProgressBarStyle.Continuous
            EnableFilters_Movies(False)
            EnableFilters_MovieSets(False)
            EnableFilters_Shows(False)

            btnCancel.Text = Master.eLang.GetString(1299, "Cancel Rewriting")
            lblCanceling.Text = Master.eLang.GetString(1300, "Canceling Rewriting...")
            btnCancel.Visible = True
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = True

            tspbLoading.Maximum = dtMovies.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = Master.eLang.GetString(1297, "Rewriting Media:")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwRewrite_Movies.WorkerReportsProgress = True
            bwRewrite_Movies.WorkerSupportsCancellation = True
            bwRewrite_Movies.RunWorkerAsync()
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub mnuMainToolsReloadMovies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadMovies.Click, cmnuTrayToolsReloadMovies.Click
        ReloadAll_Movie()
    End Sub

    Private Sub mnuMainToolsReloadMovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadMovieSets.Click
        ReloadAll_MovieSet()
    End Sub

    Private Sub mnuMainToolsReloadTVShows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadTVShows.Click
        ReloadAll_TVShow(True)
    End Sub

    Private Sub mnuMainToolsRewriteMovieContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsRewriteMovieContent.Click
        RewriteAll_Movie()
    End Sub
    ''' <summary>
    ''' Adds a new single Movie row with informations from DB
    ''' </summary>
    ''' <param name="lngID"></param>
    ''' <remarks></remarks>
    Private Sub AddRow_Movie(ByVal lngID As Long)
        If lngID = -1 Then Return

        Dim myDelegate As New Delegate_dtListAddRow(AddressOf dtListAddRow)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM movielist WHERE idMovie={0}", lngID))
        If newTable.Rows.Count = 1 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = dtMovies.NewRow()
        dRow.ItemArray = newRow.ItemArray

        If newRow IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dtMovies, dRow})
            Else
                dtMovies.Rows.Add(dRow)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single Movie row with informations from DB
    ''' </summary>
    ''' <param name="MovieID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_Movie(ByVal MovieID As Long)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf dtListUpdateRow)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM movielist WHERE idMovie={0}", MovieID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtMovies.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idMovie")) = MovieID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If dgvMovies.SelectedRows.Count > 0 AndAlso CInt(dgvMovies.SelectedRows(0).Cells("idMovie").Value) = MovieID Then
            SelectRow_Movie(dgvMovies.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single MovieSet row with informations from DB
    ''' </summary>
    ''' <param name="MovieSetID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_MovieSet(ByVal MovieSetID As Long)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf dtListUpdateRow)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM setslist WHERE idSet={0}", MovieSetID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtMovieSets.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSet")) = MovieSetID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If dgvMovieSets.SelectedRows.Count > 0 AndAlso CInt(dgvMovieSets.SelectedRows(0).Cells("idSet").Value) = MovieSetID Then
            SelectRow_MovieSet(dgvMovieSets.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single TVEpsiode row with informations from DB
    ''' </summary>
    ''' <param name="EpisodeID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_TVEpisode(ByVal EpisodeID As Long)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf dtListUpdateRow)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM episodelist WHERE idEpisode={0}", EpisodeID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtTVEpisodes.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idEpisode")) = EpisodeID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If dgvTVEpisodes.SelectedRows.Count > 0 AndAlso CInt(dgvTVEpisodes.SelectedRows(0).Cells("idEpisode").Value) = EpisodeID AndAlso currList = 2 Then
            SelectRow_TVEpisode(dgvTVEpisodes.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single TVSeason row with informations from DB
    ''' </summary>
    ''' <param name="SeasonID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_TVSeason(ByVal SeasonID As Long)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf dtListUpdateRow)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM seasonslist WHERE idSeason={0}", SeasonID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtTVSeasons.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSeason")) = SeasonID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If dgvTVSeasons.SelectedRows.Count > 0 AndAlso CInt(dgvTVSeasons.SelectedRows(0).Cells("idSeason").Value) = SeasonID AndAlso currList = 1 Then
            SelectRow_TVSeason(dgvTVSeasons.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub RefreshRow_TVSeason(ByVal ShowID As Long, ByVal iSeason As Integer)
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLNewcommand.CommandText = String.Concat("SELECT idSeason FROM seasons WHERE idShow = ", ShowID, " AND Season = ", iSeason, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLreader.Read()
                If SQLreader.HasRows Then
                    RefreshRow_TVSeason(Convert.ToInt64(SQLreader("idSeason")))
                End If
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Refresh a single TVShow row with informations from DB
    ''' </summary>
    ''' <param name="ShowID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_TVShow(ByVal ShowID As Long, Optional ByVal Force As Boolean = False)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf dtListUpdateRow)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM tvshowlist WHERE idShow={0}", ShowID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtTVShows.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idShow")) = ShowID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If dgvTVShows.SelectedRows.Count > 0 AndAlso CInt(dgvTVShows.SelectedRows(0).Cells("idShow").Value) = ShowID AndAlso (currList = 0 OrElse Force) Then
            SelectRow_TVShow(dgvTVShows.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns>Reload movie from disc</returns>
    ''' <remarks></remarks>
    Private Function Reload_Movie(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean) As Boolean
        Dim DBMovie As Database.DBElement = Master.DB.Load_Movie(ID)

        If DBMovie.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBMovie, Not showMessage) Then
            fScanner.Load_Movie(DBMovie, BatchMode)
            If Not BatchMode Then RefreshRow_Movie(DBMovie.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine,
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")),
                                                     Master.eLang.GetString(654, "Remove movie from library"),
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Master.DB.Delete_Movie(ID, BatchMode)
                Return True
            Else
                Return False
            End If
        End If

        If Not BatchMode Then
            DoTitleCheck()
        End If

        Return False
    End Function
    ''' <summary>
    ''' Reload movieset from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_MovieSet(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False) As Boolean
        Dim DBMovieSet As Database.DBElement = Master.DB.Load_MovieSet(ID)

        fScanner.Load_MovieSet(DBMovieSet, BatchMode)
        If Not BatchMode Then RefreshRow_MovieSet(DBMovieSet.ID)

        Return False
    End Function
    ''' <summary>
    ''' Reload tv episode from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_TVEpisode(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean) As Boolean
        Dim DBTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
        Dim epCount As Integer = 0

        If DBTVEpisode.FilenameID = -1 Then Return False 'skipping missing episodes

        If DBTVEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBTVEpisode, showMessage) Then
            fScanner.Load_TVEpisode(DBTVEpisode, False, BatchMode, False)
            If Not BatchMode Then RefreshRow_TVEpisode(DBTVEpisode.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine,
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")),
                                                     Master.eLang.GetString(738, "Remove episode from library"),
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Master.DB.Delete_TVEpisode(DBTVEpisode.Filename, False, BatchMode)
                Return True
            Else
                Return False
            End If
        End If

        Return False
    End Function
    ''' <summary>
    ''' Reload tv season from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_TVSeason(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean, reloadFull As Boolean) As Boolean
        Dim DBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

        If DBTVSeason.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVSeason, showMessage) Then
            fScanner.GetFolderContents_TVSeason(DBTVSeason)
            Master.DB.Save_TVSeason(DBTVSeason, BatchMode, False, True)
            If Not BatchMode Then RefreshRow_TVSeason(DBTVSeason.ID)
        Else
            Return False
        End If

        Return False
    End Function
    ''' <summary>
    ''' Reload tv show from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_TVShow(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean, ByVal reloadFull As Boolean) As Boolean
        Dim DBTVShow As Database.DBElement = Master.DB.Load_TVShow(ID, reloadFull, reloadFull)

        If DBTVShow.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVShow, showMessage) Then
            fScanner.Load_TVShow(DBTVShow, False, BatchMode, False)
            If Not BatchMode Then RefreshRow_TVShow(DBTVShow.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(719, "This path is no longer available"), ".", Environment.NewLine,
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")),
                                                     Master.eLang.GetString(776, "Remove tv show from library"),
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Master.DB.Delete_TVShow(ID, BatchMode)
                Return True
            Else
                Return False
            End If
        End If

        Return False
    End Function
    ''' <summary>
    ''' Removes a single Movie row from list
    ''' </summary>
    ''' <param name="ID">Movie ID</param>
    ''' <remarks></remarks>
    Private Sub RemoveRow_Movie(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf dtListRemoveRow)

        Dim dRow = From drvRow In dtMovies.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idMovie")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dtMovies, dRow(0)})
            Else
                dtMovies.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_Movie = -1
    End Sub
    ''' <summary>
    ''' Removes a single MovieSet row from list
    ''' </summary>
    ''' <param name="ID">MovieSet ID</param>
    ''' <remarks></remarks>
    Private Sub RemoveRow_MovieSet(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf dtListRemoveRow)

        Dim dRow = From drvRow In dtMovieSets.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSet")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dtMovieSets, dRow(0)})
            Else
                dtMovieSets.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_MovieSet = -1
    End Sub
    ''' <summary>
    ''' Removes a single TVEpisode row from list
    ''' </summary>
    ''' <param name="ID">TVEpisode ID</param>
    ''' <remarks></remarks>
    Private Sub RemoveRow_TVEpisode(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf dtListRemoveRow)

        Dim dRow = From drvRow In dtTVEpisodes.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idEpisode")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dtTVEpisodes, dRow(0)})
            Else
                dtTVEpisodes.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_TVEpisode = -1
    End Sub
    ''' <summary>
    ''' Removes a single TVSeason row from list
    ''' </summary>
    ''' <param name="ID">TVSeason ID</param>
    ''' <remarks></remarks>
    Private Sub RemoveRow_TVSeason(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf dtListRemoveRow)

        Dim dRow = From drvRow In dtTVSeasons.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSeason")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dtTVSeasons, dRow(0)})
            Else
                dtTVSeasons.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_TVSeason = -1
    End Sub
    ''' <summary>
    ''' Removes a single TVShow row from list
    ''' </summary>
    ''' <param name="ID">TVShow ID</param>
    ''' <remarks></remarks>
    Private Sub RemoveRow_TVShow(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf dtListRemoveRow)

        Dim dRow = From drvRow In dtTVShows.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idShow")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {dtTVShows, dRow(0)})
            Else
                dtTVShows.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_TVShow = -1
    End Sub
    ''' <summary>
    ''' Load existing movie content and save it again with all selected filenames
    ''' </summary>
    ''' <param name="ID">Movie ID</param>
    ''' <returns>reload list from database?</returns>
    ''' <remarks></remarks>
    Private Function RewriteMovie(ByVal ID As Long, ByVal BatchMode As Boolean) As Boolean
        Dim tmpMovieDB As Database.DBElement = Master.DB.Load_Movie(ID)

        If tmpMovieDB.IsOnline Then
            Master.DB.Save_Movie(tmpMovieDB, BatchMode, True, True, False)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub cmnuMovieRemoveFromDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRemoveFromDB.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idMovie").Value))
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tID As Long In lItemsToRemove
                Master.DB.Delete_Movie(tID, True)
                RemoveRow_Movie(tID)
            Next
            SQLtransaction.Commit()
        End Using

        FillList(False, True, False)
    End Sub

    Private Sub ResizeMoviesList()
        If Not Master.isWindows Then
            If dgvMovies.ColumnCount > 0 Then
                dgvMovies.Columns(3).Width = dgvMovies.Width -
                If(CheckColumnHide_Movies("Year"), dgvMovies.Columns(17).Width, 0) -
                If(CheckColumnHide_Movies("BannerPath"), 20, 0) -
                If(CheckColumnHide_Movies("ClearArtPath"), 20, 0) -
                If(CheckColumnHide_Movies("ClearLogoPath"), 20, 0) -
                If(CheckColumnHide_Movies("DiscArtPath"), 20, 0) -
                If(CheckColumnHide_Movies("EFanartsPath"), 20, 0) -
                If(CheckColumnHide_Movies("EThumbsPath"), 20, 0) -
                If(CheckColumnHide_Movies("FanartPath"), 20, 0) -
                If(CheckColumnHide_Movies("LandscapePath"), 20, 0) -
                If(CheckColumnHide_Movies("NfoPath"), 20, 0) -
                If(CheckColumnHide_Movies("PosterPath"), 20, 0) -
                If(CheckColumnHide_Movies("HasSet"), 20, 0) -
                If(CheckColumnHide_Movies("HasSub"), 20, 0) -
                If(CheckColumnHide_Movies("ThemePath"), 20, 0) -
                If(CheckColumnHide_Movies("TrailerPath"), 20, 0) -
                If(CheckColumnHide_Movies("Playcount"), 20, 0) -
                If(dgvMovies.DisplayRectangle.Height > dgvMovies.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If
        End If
    End Sub

    Private Sub ResizeMovieSetsList()
        If Not Master.isWindows Then
            If dgvMovieSets.ColumnCount > 0 Then
                dgvMovieSets.Columns(0).Width = dgvMovieSets.Width -
                If(CheckColumnHide_MovieSets("NfoPath"), 20, 0) -
                If(CheckColumnHide_MovieSets("PosterPath"), 20, 0) -
                If(CheckColumnHide_MovieSets("FanartPath"), 20, 0) -
                If(CheckColumnHide_MovieSets("BannerPath"), 20, 0) -
                If(CheckColumnHide_MovieSets("LandscapePath"), 20, 0) -
                If(CheckColumnHide_MovieSets("DiscArtPath"), 20, 0) -
                If(CheckColumnHide_MovieSets("ClearLogoPath"), 20, 0) -
                If(CheckColumnHide_MovieSets("ClearArtPath"), 20, 0) -
                If(dgvMovieSets.DisplayRectangle.Height > dgvMovieSets.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If
        End If
    End Sub

    Private Sub ResizeTVLists(ByVal iType As Integer)
        '0 = all.... needed???

        If Not Master.isWindows Then
            If (iType = 0 OrElse iType = 1) AndAlso dgvTVShows.ColumnCount > 0 Then
                dgvTVShows.Columns(1).Width = dgvTVShows.Width -
                If(CheckColumnHide_TVShows("BannerPath"), 20, 0) -
                If(CheckColumnHide_TVShows("CharacterArtPath"), 20, 0) -
                If(CheckColumnHide_TVShows("ClearArtPath"), 20, 0) -
                If(CheckColumnHide_TVShows("ClearLogoPath"), 20, 0) -
                If(CheckColumnHide_TVShows("EFanartsPath"), 20, 0) -
                If(CheckColumnHide_TVShows("FanartPath"), 20, 0) -
                If(CheckColumnHide_TVShows("LandscapePath"), 20, 0) -
                If(CheckColumnHide_TVShows("NfoPath"), 20, 0) -
                If(CheckColumnHide_TVShows("PosterPath"), 20, 0) -
                If(CheckColumnHide_TVShows("ThemePath"), 20, 0) -
                If(CheckColumnHide_TVShows("Playcount"), 20, 0) -
                If(dgvTVShows.DisplayRectangle.Height > dgvTVShows.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

            If (iType = 0 OrElse iType = 2) AndAlso dgvTVSeasons.ColumnCount > 0 Then
                dgvTVSeasons.Columns(1).Width = dgvTVSeasons.Width -
                If(CheckColumnHide_TVSeasons("BannerPath"), 20, 0) -
                If(CheckColumnHide_TVSeasons("FanartPath"), 20, 0) -
                If(CheckColumnHide_TVSeasons("LandscapePath"), 20, 0) -
                If(CheckColumnHide_TVSeasons("PosterPath"), 20, 0) -
                If(CheckColumnHide_TVSeasons("Playcount"), 20, 0) -
                If(dgvTVSeasons.DisplayRectangle.Height > dgvTVSeasons.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

            If (iType = 0 OrElse iType = 3) AndAlso dgvTVEpisodes.ColumnCount > 0 Then
                dgvTVEpisodes.Columns(2).Width = dgvTVEpisodes.Width - 40 -
                If(CheckColumnHide_TVEpisodes("FanartPath"), 20, 0) -
                If(CheckColumnHide_TVEpisodes("NfoPath"), 20, 0) -
                If(CheckColumnHide_TVEpisodes("PosterPath"), 20, 0) -
                If(CheckColumnHide_TVEpisodes("HasSub"), 20, 0) -
                If(CheckColumnHide_TVEpisodes("Playcount"), 20, 0) -
                If(dgvTVEpisodes.DisplayRectangle.Height > dgvTVEpisodes.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

        End If
    End Sub

    Private Sub RunFilter_MovieCustom(ByVal CustomFilterString As String)
        Try
            If Visible Then
                Dim table As New DataTable
                Dim ds As New DataSet
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = CustomFilterString
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        ds.Tables.Add(table)
                        ds.EnforceConstraints = False
                        table.Load(SQLreader)
                    End Using
                End Using
                Dim filterstring As String = ""
                Dim idColumnname As String = ""
                If table.Columns.Contains("idMovie") Then
                    idColumnname = "idMovie"
                ElseIf table.Columns.Contains("idMedia") Then
                    idColumnname = "idMedia"
                End If
                If Not String.IsNullOrEmpty(idColumnname) Then
                    For Each resultrow As DataRow In table.Rows
                        If Not String.IsNullOrEmpty(resultrow.Item(idColumnname).ToString) Then
                            If String.IsNullOrEmpty(filterstring) Then
                                filterstring = filterstring & "idMovie = " & resultrow.Item(idColumnname).ToString
                            Else
                                filterstring = filterstring & " OR " & "idMovie = " & resultrow.Item(idColumnname).ToString
                            End If

                        End If
                    Next
                Else
                    logger.Warn("[RunFilter_MovieCustom] Query: " & CustomFilterString & " doesn't return idMovie/idMedia field!")
                End If

                ClearInfo()
                prevRow_Movie = -2
                currRow_Movie = -1
                dgvMovies.ClearSelection()
                dgvMovies.CurrentCell = Nothing
                'in case there are no results for custom filter, don't display any movies by creating dummy filter
                If String.IsNullOrEmpty(filterstring) Then
                    filterstring = "Title LIKE '%Oh my nothing found, but Ember rocks anyway!%'"
                End If
                bsMovies.Filter = filterstring
                ModulesManager.Instance.RuntimeObjects.FilterMovies = filterstring
                txtSearchMovies.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub RunFilter_Movies(Optional ByVal doFill As Boolean = False)
        If Visible Then

            ClearInfo()

            prevRow_Movie = -2
            currRow_Movie = -1
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing

            If FilterArray_Movies.Count > 0 Then
                Dim FilterString As String = String.Empty

                If rbFilterAnd_Movies.Checked Then
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_Movies.ToArray, " AND ")
                Else
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_Movies.ToArray, " OR ")
                End If

                bsMovies.Filter = FilterString
                ModulesManager.Instance.RuntimeObjects.FilterMovies = FilterString
            Else
                bsMovies.RemoveFilter()
                ModulesManager.Instance.RuntimeObjects.FilterMovies = String.Empty
            End If

            If doFill Then
                FillList(True, False, False)
                ModulesManager.Instance.RuntimeObjects.FilterMoviesSearch = txtSearchMovies.Text
                ModulesManager.Instance.RuntimeObjects.FilterMoviesType = cbSearchMovies.Text
            Else
                txtSearchMovies.Focus()
            End If
        End If
    End Sub

    Private Sub RunFilter_MovieSets(Optional ByVal doFill As Boolean = False)
        If Visible Then

            ClearInfo()

            prevRow_MovieSet = -2
            currRow_MovieSet = -1
            dgvMovieSets.ClearSelection()
            dgvMovieSets.CurrentCell = Nothing

            If FilterArray_MovieSets.Count > 0 Then
                Dim FilterString As String = String.Empty

                If rbFilterAnd_MovieSets.Checked Then
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_MovieSets.ToArray, " AND ")
                Else
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_MovieSets.ToArray, " OR ")
                End If

                bsMovieSets.Filter = FilterString
            Else
                bsMovieSets.RemoveFilter()
            End If

            If doFill Then
                FillList(False, True, False)
            Else
                txtSearchMovieSets.Focus()
            End If
        End If
    End Sub

    Private Sub RunFilter_Shows(Optional ByVal doFill As Boolean = False)
        If Visible Then

            ClearInfo()

            prevRow_TVShow = -2
            currRow_TVShow = -1
            currList = 0
            dgvTVShows.ClearSelection()
            dgvTVShows.CurrentCell = Nothing

            dgvTVSeasons.ClearSelection()
            dgvTVSeasons.CurrentCell = Nothing
            bsTVSeasons.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing

            dgvTVEpisodes.ClearSelection()
            dgvTVEpisodes.CurrentCell = Nothing
            bsTVEpisodes.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing

            If FilterArray_TVShows.Count > 0 Then
                Dim FilterString As String = String.Empty

                If rbFilterAnd_Shows.Checked Then
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_TVShows.ToArray, " AND ")
                Else
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_TVShows.ToArray, " OR ")
                End If

                bsTVShows.Filter = FilterString
                ModulesManager.Instance.RuntimeObjects.FilterTVShows = FilterString
            Else
                bsTVShows.RemoveFilter()
                ModulesManager.Instance.RuntimeObjects.FilterTVShows = String.Empty
            End If

            If doFill Then
                FillList(False, False, True)
                ModulesManager.Instance.RuntimeObjects.FilterTVShowsSearch = txtSearchShows.Text
                ModulesManager.Instance.RuntimeObjects.FilterTVShowsType = cbSearchShows.Text
            Else
                txtSearchShows.Focus()
            End If
        End If
    End Sub

    Private Sub RestoreFilter_Movies()
        With Master.eSettings
            If .GeneralMainFilterSortColumn_Movies = 0 AndAlso .GeneralMainFilterSortOrder_Movies = 0 Then
                .GeneralMainFilterSortColumn_Movies = 3         'ListTitle in movielist
                .GeneralMainFilterSortOrder_Movies = 0          'ASC
            End If

            If dgvMovies.DataSource IsNot Nothing Then
                dgvMovies.Sort(dgvMovies.Columns(.GeneralMainFilterSortColumn_Movies), CType(.GeneralMainFilterSortOrder_Movies, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub RestoreFilter_MovieSets()
        With Master.eSettings
            If .GeneralMainFilterSortColumn_MovieSets = 0 AndAlso .GeneralMainFilterSortOrder_Movies = 0 Then
                .GeneralMainFilterSortColumn_MovieSets = 3         'ListTitle in movielist
                .GeneralMainFilterSortOrder_MovieSets = 0          'ASC
            End If

            If dgvMovieSets.DataSource IsNot Nothing Then
                dgvMovieSets.Sort(dgvMovieSets.Columns(.GeneralMainFilterSortColumn_MovieSets), CType(.GeneralMainFilterSortOrder_MovieSets, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub RestoreFilter_Shows()
        With Master.eSettings
            If .GeneralMainFilterSortColumn_Shows = 0 AndAlso .GeneralMainFilterSortOrder_Shows = 0 Then
                .GeneralMainFilterSortColumn_Shows = 1         'ListTitle in tvshowlist
                .GeneralMainFilterSortOrder_Shows = 0          'ASC
            End If

            If dgvTVShows.DataSource IsNot Nothing Then
                dgvTVShows.Sort(dgvTVShows.Columns(.GeneralMainFilterSortColumn_Shows), CType(.GeneralMainFilterSortOrder_Shows, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub SaveFilter_Movies()
        Dim Order As Integer
        If dgvMovies.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvMovies.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvMovies.SortOrder = SortOrder.Descending Then Order = 1

        Master.eSettings.GeneralMainFilterSortColumn_Movies = dgvMovies.SortedColumn.Index
        Master.eSettings.GeneralMainFilterSortOrder_Movies = Order
    End Sub

    Private Sub SaveFilter_MovieSets()
        Dim Order As Integer
        If dgvMovieSets.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvMovieSets.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvMovieSets.SortOrder = SortOrder.Descending Then Order = 1

        Master.eSettings.GeneralMainFilterSortColumn_MovieSets = dgvMovieSets.SortedColumn.Index
        Master.eSettings.GeneralMainFilterSortOrder_MovieSets = Order
    End Sub

    Private Sub SaveFilter_Shows()
        Dim Order As Integer
        If dgvTVShows.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvTVShows.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvTVShows.SortOrder = SortOrder.Descending Then Order = 1

        Master.eSettings.GeneralMainFilterSortColumn_Shows = dgvTVShows.SortedColumn.Index
        Master.eSettings.GeneralMainFilterSortOrder_Shows = Order
    End Sub

    Private Sub ScannerUpdated(ByVal eProgressValue As Scanner.ProgressValue)
        Select Case eProgressValue.Type
            Case Enums.ScannerEventType.AddedMovie
                SetStatus(String.Concat(Master.eLang.GetString(815, "Added Movie:"), " ", eProgressValue.Message))
                AddRow_Movie(eProgressValue.ID)
            Case Enums.ScannerEventType.AddedTVEpisode
                SetStatus(String.Concat(Master.eLang.GetString(814, "Added Episode:"), " ", eProgressValue.Message))
            Case Enums.ScannerEventType.CleaningDatabase
                SetStatus(Master.eLang.GetString(644, "Cleaning Database..."))
            Case Enums.ScannerEventType.PreliminaryTasks
                SetStatus(Master.eLang.GetString(116, "Performing Preliminary Tasks (Gathering Data)..."))
        End Select
    End Sub

    Private Sub ScanningCompleted()
        If Not Master.isCL Then
            SetStatus(String.Empty)
            FillList(False, True, True)
            tspbLoading.Visible = False
            tslLoading.Visible = False
            LoadingDone = True
        Else
            FillList(True, True, True)
            LoadingDone = True
        End If
    End Sub

    Private Sub scMain_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles scMain.SplitterMoved
        Try
            If Created Then
                SuspendLayout()
                MoveMPAA()
                MoveGenres()

                ImageUtils.ResizePB(pbFanart, pbFanartCache, scMain.Panel2.Height - 90, scMain.Panel2.Width)
                pbFanart.Left = Convert.ToInt32((scMain.Panel2.Width - pbFanart.Width) / 2)
                pnlNoInfo.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), Convert.ToInt32((scMain.Panel2.Height - pnlNoInfo.Height) / 2))
                pnlCancel.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), 124)
                pnlFilterCountries_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterCountry_Movies.Left + 1,
                                                                  (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterCountry_Movies.Top) - pnlFilterCountries_Movies.Height)
                pnlFilterGenres_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterGenre_Movies.Left + 1,
                                                               (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterGenre_Movies.Top) - pnlFilterGenres_Movies.Height)
                pnlFilterGenres_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterGenre_Shows.Left + 1,
                                                              (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterGenre_Shows.Top) - pnlFilterGenres_Shows.Height)
                pnlFilterDataFields_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + gbFilterDataField_Movies.Left + tblFilterDataField_Movies.Left + txtFilterDataField_Movies.Left + 1,
                                                                   (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + gbFilterDataField_Movies.Top + tblFilterDataField_Movies.Top + txtFilterDataField_Movies.Top) - pnlFilterDataFields_Movies.Height)
                pnlFilterMissingItems_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterGeneral_Movies.Left + tblFilterGeneral_Movies.Left + btnFilterMissing_Movies.Left + 1,
                                                                     (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterGeneral_Movies.Top + tblFilterGeneral_Movies.Top + btnFilterMissing_Movies.Top) - pnlFilterMissingItems_Movies.Height)
                pnlFilterMissingItems_MovieSets.Location = New Point(pnlFilter_MovieSets.Left + tblFilter_MovieSets.Left + gbFilterGeneral_MovieSets.Left + tblFilterGeneral_MovieSets.Left + btnFilterMissing_MovieSets.Left + 1,
                                                                     (pnlFilter_MovieSets.Top + tblFilter_MovieSets.Top + gbFilterGeneral_MovieSets.Top + tblFilterGeneral_MovieSets.Top + btnFilterMissing_MovieSets.Top) - pnlFilterMissingItems_MovieSets.Height)
                pnlFilterMissingItems_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterGeneral_Shows.Left + tblFilterGeneral_Shows.Left + btnFilterMissing_Shows.Left + 1,
                                                                     (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterGeneral_Shows.Top + tblFilterGeneral_Shows.Top + btnFilterMissing_Shows.Top) - pnlFilterMissingItems_Shows.Height)
                pnlFilterSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterSource_Movies.Left + 1,
                                                                (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterSource_Movies.Top) - pnlFilterSources_Movies.Height)
                pnlFilterSources_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterSource_Shows.Left + 1,
                                                               (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterSource_Shows.Top) - pnlFilterSources_Shows.Height)

                Select Case tcMain.SelectedIndex
                    Case 0
                        dgvMovies.Focus()
                    Case 1
                        dgvMovieSets.Focus()
                    Case 2
                        dgvTVShows.Focus()
                End Select

                ResumeLayout(True)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub SelectRow_Movie(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        ClearInfo()

        If dgvMovies.Rows.Count > iRow Then
            If String.IsNullOrEmpty(dgvMovies.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovies.Item("ClearArtPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvMovies.Item("ClearLogoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovies.Item("DiscArtPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvMovies.Item("EFanartsPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovies.Item("EThumbsPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvMovies.Item("FanartPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovies.Item("LandscapePath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvMovies.Item("NfoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovies.Item("PosterPath", iRow).Value.ToString) Then
                ShowNoInfo(True, 0)
                currMovie = Master.DB.Load_Movie(Convert.ToInt64(dgvMovies.Item("idMovie", iRow).Value))
                FillScreenInfoWith_Movie()

                If Not bwMovieScraper.IsBusy AndAlso Not bwMovieSetScraper.IsBusy AndAlso Not fScanner.IsBusy AndAlso Not bwLoadMovieInfo.IsBusy AndAlso Not bwLoadShowInfo.IsBusy AndAlso Not bwLoadSeasonInfo.IsBusy AndAlso Not bwLoadEpInfo.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                    cmnuMovie.Enabled = True
                End If
            Else
                LoadInfo_Movie(Convert.ToInt64(dgvMovies.Item("idMovie", iRow).Value), dgvMovies.Item("MoviePath", iRow).Value.ToString, True)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub SelectRow_MovieSet(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        ClearInfo()

        If dgvMovieSets.Rows.Count > iRow Then
            If String.IsNullOrEmpty(dgvMovieSets.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovieSets.Item("ClearArtPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvMovieSets.Item("ClearLogoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovieSets.Item("DiscArtPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvMovieSets.Item("FanartPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovieSets.Item("LandscapePath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvMovieSets.Item("NfoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvMovieSets.Item("PosterPath", iRow).Value.ToString) Then
                ShowNoInfo(True, Enums.ContentType.MovieSet)

                currMovieSet = Master.DB.Load_MovieSet(Convert.ToInt64(dgvMovieSets.Item("idSet", iRow).Value))
                FillScreenInfoWith_MovieSet()

                If Not bwMovieScraper.IsBusy AndAlso Not bwMovieSetScraper.IsBusy AndAlso Not fScanner.IsBusy AndAlso Not bwLoadMovieInfo.IsBusy AndAlso Not bwLoadShowInfo.IsBusy AndAlso Not bwLoadSeasonInfo.IsBusy AndAlso Not bwLoadEpInfo.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                    cmnuMovie.Enabled = True
                End If
            Else
                LoadInfo_MovieSet(Convert.ToInt64(dgvMovieSets.Item("idSet", iRow).Value), True)
            End If
        End If
    End Sub

    Private Sub SelectRow_TVEpisode(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        ClearInfo()

        If dgvTVEpisodes.Rows.Count > iRow Then
            If String.IsNullOrEmpty(dgvTVEpisodes.Item("FanartPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvTVEpisodes.Item("NfoPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvTVEpisodes.Item("PosterPath", iRow).Value.ToString) AndAlso Not Convert.ToInt64(dgvTVEpisodes.Item("idFile", iRow).Value) = -1 Then
                ShowNoInfo(True, Enums.ContentType.TVEpisode)

                currTV = Master.DB.Load_TVEpisode(Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", iRow).Value), True)
                FillScreenInfoWith_TVEpisode()

                If Not Convert.ToInt64(dgvTVEpisodes.Item("idFile", iRow).Value) = -1 AndAlso Not fScanner.IsBusy AndAlso Not bwLoadMovieInfo.IsBusy AndAlso Not bwLoadMovieSetInfo.IsBusy AndAlso Not bwLoadShowInfo.IsBusy AndAlso Not bwLoadSeasonInfo.IsBusy AndAlso Not bwLoadEpInfo.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                    cmnuEpisode.Enabled = True
                End If
            Else
                LoadInfo_TVEpisode(Convert.ToInt64(dgvTVEpisodes.SelectedRows(0).Cells("idEpisode").Value))
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Season selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub SelectRow_TVSeason(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        ClearInfo()

        If dgvTVSeasons.Rows.Count > iRow Then
            If String.IsNullOrEmpty(dgvTVSeasons.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvTVSeasons.Item("FanartPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvTVSeasons.Item("LandscapePath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvTVSeasons.Item("PosterPath", iRow).Value.ToString) AndAlso
                Not Convert.ToBoolean(dgvTVSeasons.Item("Missing", iRow).Value) Then
                If Not currThemeType = Theming.ThemeType.Show Then ApplyTheme(Theming.ThemeType.Show)
                ShowNoInfo(True, Enums.ContentType.TVSeason)

                currTV = Master.DB.Load_TVSeason(Convert.ToInt64(dgvTVSeasons.Item("idSeason", iRow).Value), True, False)
                FillEpisodes(Convert.ToInt64(dgvTVSeasons.Item("idShow", iRow).Value), Convert.ToInt32(dgvTVSeasons.Item("Season", iRow).Value))

                If Not fScanner.IsBusy AndAlso Not bwLoadMovieInfo.IsBusy AndAlso Not bwLoadMovieSetInfo.IsBusy AndAlso
                    Not bwLoadShowInfo.IsBusy AndAlso Not bwLoadSeasonInfo.IsBusy AndAlso Not bwLoadEpInfo.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso
                    Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                    cmnuSeason.Enabled = True
                End If
            Else
                LoadInfo_TVSeason(Convert.ToInt64(dgvTVSeasons.Item("idSeason", iRow).Value),
                                  If(CInt(dgvTVSeasons.Item("Season", iRow).Value) = 999, False, CBool(dgvTVSeasons.Item("Missing", iRow).Value)))
                FillEpisodes(Convert.ToInt64(dgvTVSeasons.Item("idShow", iRow).Value), Convert.ToInt32(dgvTVSeasons.Item("Season", iRow).Value))
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Show selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub SelectRow_TVShow(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        ClearInfo()

        If dgvTVShows.Rows.Count > iRow Then
            If String.IsNullOrEmpty(dgvTVShows.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvTVShows.Item("CharacterArtPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvTVShows.Item("ClearArtPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvTVShows.Item("ClearLogoPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvTVShows.Item("EFanartsPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvTVShows.Item("FanartPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvTVShows.Item("LandscapePath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(dgvTVShows.Item("NfoPath", iRow).Value.ToString) AndAlso
                String.IsNullOrEmpty(dgvTVShows.Item("PosterPath", iRow).Value.ToString) Then
                ShowNoInfo(True, Enums.ContentType.TVShow)

                currTV = Master.DB.Load_TVShow(Convert.ToInt64(dgvTVShows.Item("idShow", iRow).Value), False, False)
                FillSeasons(Convert.ToInt64(dgvTVShows.Item("idShow", iRow).Value))

                If Not fScanner.IsBusy AndAlso Not bwLoadMovieInfo.IsBusy AndAlso Not bwLoadMovieSetInfo.IsBusy AndAlso Not bwLoadShowInfo.IsBusy AndAlso Not bwLoadSeasonInfo.IsBusy AndAlso Not bwLoadEpInfo.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                    cmnuShow.Enabled = True
                End If
            Else
                LoadInfo_TVShow(Convert.ToInt64(dgvTVShows.Item("idShow", iRow).Value))
            End If
        End If
    End Sub

    Private Sub SetAVImages(ByVal aImage As Image())
        pbResolution.Image = aImage(0)
        pbVideo.Image = aImage(1)
        pbVType.Image = aImage(2)
        pbAudio.Image = aImage(3)
        pbChannels.Image = aImage(4)
        pbAudioLang0.Image = aImage(5)
        pbAudioLang1.Image = aImage(6)
        pbAudioLang2.Image = aImage(7)
        pbAudioLang3.Image = aImage(8)
        pbAudioLang4.Image = aImage(9)
        pbAudioLang5.Image = aImage(10)
        pbAudioLang6.Image = aImage(11)
        pbSubtitleLang0.Image = aImage(12)
        pbSubtitleLang1.Image = aImage(13)
        pbSubtitleLang2.Image = aImage(14)
        pbSubtitleLang3.Image = aImage(15)
        pbSubtitleLang4.Image = aImage(16)
        pbSubtitleLang5.Image = aImage(17)
        pbSubtitleLang6.Image = aImage(18)

        ToolTips.SetToolTip(pbAudioLang0, If(pbAudioLang0.Image IsNot Nothing, pbAudioLang0.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang1, If(pbAudioLang1.Image IsNot Nothing, pbAudioLang1.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang2, If(pbAudioLang2.Image IsNot Nothing, pbAudioLang2.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang3, If(pbAudioLang3.Image IsNot Nothing, pbAudioLang3.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang4, If(pbAudioLang4.Image IsNot Nothing, pbAudioLang4.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang5, If(pbAudioLang5.Image IsNot Nothing, pbAudioLang5.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang6, If(pbAudioLang6.Image IsNot Nothing, pbAudioLang6.Image.Tag.ToString, String.Empty))

        ToolTips.SetToolTip(pbSubtitleLang0, If(pbSubtitleLang0.Image IsNot Nothing, pbSubtitleLang0.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang1, If(pbSubtitleLang1.Image IsNot Nothing, pbSubtitleLang1.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang2, If(pbSubtitleLang2.Image IsNot Nothing, pbSubtitleLang2.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang3, If(pbSubtitleLang3.Image IsNot Nothing, pbSubtitleLang3.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang4, If(pbSubtitleLang4.Image IsNot Nothing, pbSubtitleLang4.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang5, If(pbSubtitleLang5.Image IsNot Nothing, pbSubtitleLang5.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang6, If(pbSubtitleLang6.Image IsNot Nothing, pbSubtitleLang6.Image.Tag.ToString, String.Empty))
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean, Optional ByVal withLists As Boolean = False, Optional ByVal withTools As Boolean = True)
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        For Each i As Object In mnuMainTools.DropDownItems
            If TypeOf i Is ToolStripMenuItem Then
                Dim o As ToolStripMenuItem = DirectCast(i, ToolStripMenuItem)
                If o.Tag Is Nothing Then
                    o.Enabled = isEnabled AndAlso ((dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie) OrElse
                                                   (dgvMovieSets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet) OrElse
                                                   (dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV))
                ElseIf TypeOf o.Tag Is Structures.ModulesMenus Then
                    Dim tagmenu As Structures.ModulesMenus = DirectCast(o.Tag, Structures.ModulesMenus)
                    o.Enabled = (isEnabled OrElse Not withTools) AndAlso (((tagmenu.IfTabMovies AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie) OrElse
                                                                           (tagmenu.IfTabMovieSets AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet) OrElse
                                                                           (tagmenu.IfTabTVShows AndAlso currMainTabTag.ContentType = Enums.ContentType.TV)) AndAlso
                                                                       ((tagmenu.ForMovies AndAlso (dgvMovies.RowCount > 0 OrElse tagmenu.IfNoMovies)) OrElse
                                                                        (tagmenu.ForMovieSets AndAlso (dgvMovieSets.RowCount > 0 OrElse tagmenu.IfNoMovieSets)) OrElse
                                                                        (tagmenu.ForTVShows AndAlso (dgvTVShows.RowCount > 0 OrElse tagmenu.IfNoTVShows))))
                End If
            ElseIf TypeOf i Is ToolStripSeparator Then
                Dim o As ToolStripSeparator = DirectCast(i, ToolStripSeparator)
                o.Visible = (mnuMainTools.DropDownItems.IndexOf(o) < mnuMainTools.DropDownItems.Count - 1)
            End If
        Next
        With Master.eSettings
            If (Not .FileSystemExpertCleaner AndAlso (.CleanDotFanartJPG OrElse .CleanFanartJPG OrElse .CleanFolderJPG OrElse .CleanMovieFanartJPG OrElse
            .CleanMovieJPG OrElse .CleanMovieNameJPG OrElse .CleanMovieNFO OrElse .CleanMovieNFOB OrElse
            .CleanMovieTBN OrElse .CleanMovieTBNB OrElse .CleanPosterJPG OrElse .CleanPosterTBN OrElse .CleanExtrathumbs)) OrElse
            (.FileSystemExpertCleaner AndAlso (.FileSystemCleanerWhitelist OrElse .FileSystemCleanerWhitelistExts.Count > 0)) Then
                mnuMainToolsCleanFiles.Enabled = isEnabled AndAlso dgvMovies.RowCount > 0 AndAlso tcMain.SelectedIndex = 0
            Else
                mnuMainToolsCleanFiles.Enabled = True  'False
            End If
            If Not String.IsNullOrEmpty(.MovieBackdropsPath) AndAlso dgvMovies.RowCount > 0 Then
                mnuMainToolsBackdrops.Enabled = True
            Else
                mnuMainToolsBackdrops.Enabled = False
            End If
        End With
        mnuMainEdit.Enabled = isEnabled
        mnuScrapeMovies.Enabled = isEnabled AndAlso dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie
        mnuScrapeMovies.Visible = currMainTabTag.ContentType = Enums.ContentType.Movie
        mnuScrapeMovieSets.Enabled = isEnabled AndAlso dgvMovieSets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet
        mnuScrapeMovieSets.Visible = currMainTabTag.ContentType = Enums.ContentType.MovieSet
        mnuScrapeTVShows.Enabled = isEnabled AndAlso dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV
        mnuScrapeTVShows.Visible = currMainTabTag.ContentType = Enums.ContentType.TV
        mnuUpdate.Enabled = isEnabled
        cmnuMovie.Enabled = isEnabled
        cmnuMovieSet.Enabled = isEnabled
        cmnuShow.Enabled = isEnabled
        cmnuSeason.Enabled = isEnabled
        cmnuEpisode.Enabled = isEnabled
        tcMain.Enabled = isEnabled
        btnMarkAll.Enabled = isEnabled
        btnMetaDataRefresh.Enabled = isEnabled
        scMain.IsSplitterFixed = Not isEnabled
        scTV.IsSplitterFixed = Not isEnabled
        scTVSeasonsEpisodes.IsSplitterFixed = Not isEnabled
        mnuMainHelp.Enabled = isEnabled
        cmnuTrayTools.Enabled = mnuMainTools.Enabled
        cmnuTrayScrapeMovies.Enabled = isEnabled AndAlso dgvMovies.RowCount > 0
        cmnuTrayScrapeMovieSets.Enabled = isEnabled AndAlso dgvMovieSets.RowCount > 0
        cmnuTrayScrapeTVShows.Enabled = isEnabled AndAlso dgvTVShows.RowCount > 0
        cmnuTrayUpdate.Enabled = isEnabled
        cmnuTraySettings.Enabled = isEnabled
        cmnuTrayExit.Enabled = isEnabled

        If withLists OrElse isEnabled Then
            dgvMovies.TabStop = isEnabled
            dgvMovieSets.TabStop = isEnabled
            dgvTVShows.TabStop = isEnabled
            dgvTVSeasons.TabStop = isEnabled
            dgvTVEpisodes.TabStop = isEnabled
            dgvMovies.Enabled = isEnabled
            dgvMovieSets.Enabled = isEnabled
            dgvTVShows.Enabled = isEnabled
            dgvTVSeasons.Enabled = isEnabled
            dgvTVEpisodes.Enabled = isEnabled
            txtSearchMovies.Enabled = isEnabled
            txtSearchMovieSets.Enabled = isEnabled
            txtSearchShows.Enabled = isEnabled
        End If
    End Sub

    Private Sub SetWatchedState_Movie(ByVal bSetToWatched As Boolean)
        Dim lItemsToChange As New List(Of Long)

        If dgvMovies.SelectedRows.Count > 0 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                lItemsToChange.Add(Convert.ToInt64(sRow.Cells("idMovie").Value))
            Next

            fTaskManager.AddTask(New TaskManager.TaskItem With {
                                 .CommonBoolean = bSetToWatched,
                                 .ListOfID = lItemsToChange,
                                 .ContentType = Enums.ContentType.Movie,
                                 .TaskType = Enums.TaskManagerType.SetWatchedState})
        End If
    End Sub

    Private Sub SetWatchedState_TVEpisode(ByVal bSetToWatched As Boolean)
        Dim lItemsToChange As New List(Of Long)

        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                lItemsToChange.Add(Convert.ToInt64(sRow.Cells("idEpisode").Value))
            Next

            fTaskManager.AddTask(New TaskManager.TaskItem With {
                                 .CommonBoolean = bSetToWatched,
                                 .ListOfID = lItemsToChange,
                                 .ContentType = Enums.ContentType.TVEpisode,
                                 .TaskType = Enums.TaskManagerType.SetWatchedState})
        End If
    End Sub

    Private Sub SetWatchedState_TVSeason(ByVal bSetToWatched As Boolean)
        Dim lItemsToChange As New List(Of Long)

        If dgvTVSeasons.SelectedRows.Count > 0 Then
            For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                If Not CInt(sRow.Cells("Season").Value) = 999 Then
                    lItemsToChange.Add(Convert.ToInt64(sRow.Cells("idSeason").Value))
                End If
            Next

            fTaskManager.AddTask(New TaskManager.TaskItem With {
                                 .CommonBoolean = bSetToWatched,
                                 .ListOfID = lItemsToChange,
                                 .ContentType = Enums.ContentType.TVSeason,
                                 .TaskType = Enums.TaskManagerType.SetWatchedState})
        End If
    End Sub

    Private Sub SetWatchedState_TVShow(ByVal bSetToWatched As Boolean)
        Dim lItemsToChange As New List(Of Long)

        If dgvTVShows.SelectedRows.Count > 0 Then
            For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                lItemsToChange.Add(Convert.ToInt64(sRow.Cells("idShow").Value))
            Next

            fTaskManager.AddTask(New TaskManager.TaskItem With {
                                 .CommonBoolean = bSetToWatched,
                                 .ListOfID = lItemsToChange,
                                 .ContentType = Enums.ContentType.TVShow,
                                 .TaskType = Enums.TaskManagerType.SetWatchedState})
        End If
    End Sub

    Private Sub cmnuMovieSetSortMethodSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetEditSortMethodSet.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In dgvMovieSets.SelectedRows
                Dim tmpDBMovieSet As Database.DBElement = Master.DB.Load_MovieSet(Convert.ToInt64(sRow.Cells("idSet").Value))
                tmpDBMovieSet.SortMethod = CType(cmnuMovieSetEditSortMethodMethods.ComboBox.SelectedValue, Enums.SortMethod_MovieSet)
                Master.DB.Save_MovieSet(tmpDBMovieSet, True, True)
                RefreshRow_MovieSet(tmpDBMovieSet.ID)
            Next
            SQLtransaction.Commit()
        End Using
    End Sub
    ''' <summary>
    ''' Enable or disable the various menu and context-menu actions based on the currently-defined settings
    ''' </summary>
    ''' <param name="ReloadFilters"></param>
    ''' <remarks></remarks>
    Private Sub SetMenus(ByVal ReloadFilters As Boolean)
        Dim mnuItem As ToolStripItem
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)

        With Master.eSettings
            If (Not .FileSystemExpertCleaner AndAlso (.CleanDotFanartJPG OrElse .CleanFanartJPG OrElse .CleanFolderJPG OrElse .CleanMovieFanartJPG OrElse
            .CleanMovieJPG OrElse .CleanMovieNameJPG OrElse .CleanMovieNFO OrElse .CleanMovieNFOB OrElse
            .CleanMovieTBN OrElse .CleanMovieTBNB OrElse .CleanPosterJPG OrElse .CleanPosterTBN OrElse .CleanExtrathumbs)) OrElse
            (.FileSystemExpertCleaner AndAlso (.FileSystemCleanerWhitelist OrElse .FileSystemCleanerWhitelistExts.Count > 0)) Then
                mnuMainToolsCleanFiles.Enabled = True AndAlso dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie
            Else
                mnuMainToolsCleanFiles.Enabled = True 'False
            End If

            mnuMainToolsBackdrops.Enabled = Not String.IsNullOrEmpty(.MovieBackdropsPath)

            ' for future use
            mnuMainToolsClearCache.Enabled = False

            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idMovie) AS mcount FROM movie WHERE mark = 1;")
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    If SQLcount.HasRows AndAlso Convert.ToInt32(SQLcount("mcount")) > 0 Then
                        btnMarkAll.Text = Master.eLang.GetString(105, "Unmark All")
                    Else
                        btnMarkAll.Text = Master.eLang.GetString(35, "Mark All")
                    End If
                End Using
            End Using

            mnuUpdateMovies.DropDownItems.Clear()
            cmnuTrayUpdateMovies.DropDownItems.Clear()
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = "SELECT COUNT(idSource) AS cID FROM moviesource;"
                If Convert.ToInt32(SQLNewcommand.ExecuteScalar) > 1 Then
                    mnuItem = mnuUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick_Movie))
                    mnuItem = cmnuTrayUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick_Movie))
                End If
                SQLNewcommand.CommandText = "SELECT idSource, strName, bExclude FROM moviesource;"
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        mnuItem = mnuUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("strName")), Nothing, New System.EventHandler(AddressOf SourceSubClick_Movie))
                        mnuItem.Tag = SQLReader("idSource")
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("bExclude")), Color.Gray, Color.Black)
                        mnuItem = cmnuTrayUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("strName")), Nothing, New System.EventHandler(AddressOf SourceSubClick_Movie))
                        mnuItem.Tag = SQLReader("idSource")
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("bExclude")), Color.Gray, Color.Black)
                    End While
                End Using
            End Using

            mnuUpdateShows.DropDownItems.Clear()
            cmnuTrayUpdateShows.DropDownItems.Clear()
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = "SELECT COUNT(idSource) AS cID FROM tvshowsource;"
                If Convert.ToInt32(SQLNewcommand.ExecuteScalar) > 1 Then
                    mnuItem = mnuUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick_TV))
                    mnuItem = cmnuTrayUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick_TV))
                End If
                SQLNewcommand.CommandText = "SELECT idSource, strName, bExclude FROM tvshowsource;"
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        mnuItem = mnuUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("strName")), Nothing, New System.EventHandler(AddressOf SourceSubClick_TV))
                        mnuItem.Tag = SQLReader("idSource")
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("bExclude")), Color.Gray, Color.Black)
                        mnuItem = cmnuTrayUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("strName")), Nothing, New System.EventHandler(AddressOf SourceSubClick_TV))
                        mnuItem.Tag = SQLReader("idSource")
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("bExclude")), Color.Gray, Color.Black)
                    End While
                End Using
            End Using

            clbFilterGenres_Movies.Items.Clear()
            Dim mGenre() As Object = APIXML.GetGenreList
            clbFilterGenres_Movies.Items.Add(Master.eLang.None)
            clbFilterGenres_Movies.Items.AddRange(mGenre)

            clbFilterGenres_Shows.Items.Clear()
            Dim sGenre() As Object = APIXML.GetGenreList
            clbFilterGenres_Shows.Items.Add(Master.eLang.None)
            clbFilterGenres_Shows.Items.AddRange(sGenre)

            clbFilterCountries_Movies.Items.Clear()
            Dim mCountry() As Object = Master.DB.GetMovieCountries
            clbFilterCountries_Movies.Items.Add(Master.eLang.None)
            clbFilterCountries_Movies.Items.AddRange(mCountry)

            clbFilterDataFields_Movies.Items.Clear()
            clbFilterDataFields_Movies.Items.AddRange(New Object() {"Certification", "Credits", "Director", "Imdb", "MPAA", "OriginalTitle", "Outline", "Plot", "Rating", "ReleaseDate", "Runtime", "SortTitle", "Studio", "TMDB", "TMDBColID", "Tag", "Tagline", "Title", "Top250", "Trailer", "VideoSource", "Votes", "Year"})

            Dim SortMethods As New Dictionary(Of String, Enums.SortMethod_MovieSet)
            SortMethods.Add(Master.eLang.GetString(278, "Year"), Enums.SortMethod_MovieSet.Year)
            SortMethods.Add(Master.eLang.GetString(21, "Title"), Enums.SortMethod_MovieSet.Title)
            cmnuMovieSetEditSortMethodMethods.ComboBox.DataSource = SortMethods.ToList
            cmnuMovieSetEditSortMethodMethods.ComboBox.DisplayMember = "Key"
            cmnuMovieSetEditSortMethodMethods.ComboBox.ValueMember = "Value"
            cmnuMovieSetEditSortMethodMethods.ComboBox.BindingContext = BindingContext

            listViews_Movies.Clear()
            listViews_Movies.Add(Master.eLang.GetString(786, "Default List"), "movielist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.Movie)
                listViews_Movies.Add(Regex.Replace(cList, "movie-", String.Empty).Trim, cList)
            Next
            cbFilterLists_Movies.DataSource = listViews_Movies.ToList
            cbFilterLists_Movies.DisplayMember = "Key"
            cbFilterLists_Movies.ValueMember = "Value"
            cbFilterLists_Movies.SelectedIndex = 0

            listViews_MovieSets.Clear()
            listViews_MovieSets.Add(Master.eLang.GetString(786, "Default List"), "setslist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.MovieSet)
                listViews_MovieSets.Add(Regex.Replace(cList, "sets-", String.Empty).Trim, cList)
            Next
            cbFilterLists_MovieSets.DataSource = listViews_MovieSets.ToList
            cbFilterLists_MovieSets.DisplayMember = "Key"
            cbFilterLists_MovieSets.ValueMember = "Value"
            cbFilterLists_MovieSets.SelectedIndex = 0

            listViews_TVShows.Clear()
            listViews_TVShows.Add(Master.eLang.GetString(786, "Default List"), "tvshowlist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.TVShow)
                listViews_TVShows.Add(Regex.Replace(cList, "tvshow-", String.Empty).Trim, cList)
            Next
            cbFilterLists_Shows.DataSource = listViews_TVShows.ToList
            cbFilterLists_Shows.DisplayMember = "Key"
            cbFilterLists_Shows.ValueMember = "Value"
            cbFilterLists_Shows.SelectedIndex = 0

            mnuLanguagesLanguage.Items.Clear()
            mnuLanguagesLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages.OrderBy(Function(f) f.Description) Select lLang.Description).ToArray)

            'not technically a menu, but it's a good place to put it
            If ReloadFilters Then

                RemoveHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus
                cbFilterDataField_Movies.Items.Clear()
                cbFilterDataField_Movies.Items.AddRange(New Object() {Master.eLang.GetString(1291, "Is Empty"), Master.eLang.GetString(1292, "Is Not Empty")})
                cbFilterDataField_Movies.SelectedIndex = 0
                AddHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus

                clbFilterSources_Movies.Items.Clear()
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT strName FROM moviesource;")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLReader.Read
                            clbFilterSources_Movies.Items.Add(SQLReader("strName"))
                        End While
                    End Using
                End Using

                clbFilterSource_Shows.Items.Clear()
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT strName FROM tvshowsource;")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLReader.Read
                            clbFilterSource_Shows.Items.Add(SQLReader("strName"))
                        End While
                    End Using
                End Using

                RemoveHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearFrom_Movies_SelectedIndexChanged
                cbFilterYearFrom_Movies.Items.Clear()
                cbFilterYearFrom_Movies.Items.Add(Master.eLang.All)
                For i As Integer = (Date.Now.Year + 1) To 1888 Step -1
                    cbFilterYearFrom_Movies.Items.Add(i)
                Next
                cbFilterYearFrom_Movies.SelectedIndex = 0
                AddHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearFrom_Movies_SelectedIndexChanged

                RemoveHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearModFrom_Movies_SelectedIndexChanged
                cbFilterYearModFrom_Movies.SelectedIndex = 0
                AddHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearModFrom_Movies_SelectedIndexChanged

                RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                cbFilterYearTo_Movies.Items.Clear()
                cbFilterYearTo_Movies.Items.Add(Master.eLang.All)
                For i As Integer = (Date.Now.Year + 1) To 1888 Step -1
                    cbFilterYearTo_Movies.Items.Add(i)
                Next
                cbFilterYearTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                cbFilterYearModTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                RemoveHandler cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf cbFilterVideoSource_Movies_SelectedIndexChanged
                cbFilterVideoSource_Movies.Items.Clear()
                cbFilterVideoSource_Movies.Items.Add(Master.eLang.All)
                'Cocotus 2014/10/11 Automatically populate available videosources from user settings to sourcefilter instead of using hardcoded list here!
                Dim mySources As New List(Of AdvancedSettingsComplexSettingsTableItem)
                mySources = clsAdvancedSettings.GetComplexSetting("MovieSources")
                If Not mySources Is Nothing Then
                    For Each k In mySources
                        If cbFilterVideoSource_Movies.Items.Contains(k.Value) = False Then
                            cbFilterVideoSource_Movies.Items.Add(k.Value)
                        End If
                    Next
                Else
                    cbFilterVideoSource_Movies.Items.AddRange(APIXML.SourceList.ToArray)
                End If
                cbFilterVideoSource_Movies.Items.Add(Master.eLang.None)
                cbFilterVideoSource_Movies.SelectedIndex = 0
                AddHandler cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf cbFilterVideoSource_Movies_SelectedIndexChanged
            End If

        End With
        mnuScrapeMovies.Enabled = (dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie)
        mnuScrapeMovies.Visible = currMainTabTag.ContentType = Enums.ContentType.Movie
        mnuScrapeMovieSets.Enabled = (dgvMovieSets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet)
        mnuScrapeMovieSets.Visible = currMainTabTag.ContentType = Enums.ContentType.MovieSet
        mnuScrapeTVShows.Enabled = (dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV)
        mnuScrapeTVShows.Visible = currMainTabTag.ContentType = Enums.ContentType.TV
        cmnuTrayScrapeMovies.Enabled = dgvMovies.RowCount > 0
        cmnuTrayScrapeMovieSets.Enabled = dgvMovieSets.RowCount > 0
        cmnuTrayScrapeTVShows.Enabled = dgvTVShows.RowCount > 0
    End Sub

    Private Sub mnuMainToolsOfflineMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsOfflineHolder.Click, cmnuTrayToolsOfflineHolder.Click
        SetControlsEnabled(False)
        'Using dOfflineHolder As New dlgOfflineHolder
        '    dOfflineHolder.ShowDialog()
        'End Using
        LoadMedia(New Structures.ScanOrClean With {.Movies = True, .TV = False})
        SetControlsEnabled(True)
    End Sub

    Private Sub SetStatus(ByVal sText As String)
        tslStatus.Text = sText.Replace("&", "&&")
    End Sub

    Private Function GetStatus() As String
        Return tslStatus.Text.Replace("&&", "&")
    End Function

    Sub HideLoadingSettings()
        If Not pnlLoadSettings.InvokeRequired Then
            pnlLoadSettings.Visible = False
        End If
    End Sub
    ''' <summary>
    ''' Display the settings dialog (passed as parameter) and process the results.
    ''' </summary>
    ''' <param name="dlg"></param>
    ''' <remarks></remarks>
    Sub SettingsShow(ByVal dlg As dlgSettings)
        AddHandler dlg.LoadEnd, AddressOf HideLoadingSettings
        Dim dresult As Structures.SettingsResult = dlg.ShowDialog()
        RemoveHandler dlg.LoadEnd, AddressOf HideLoadingSettings
        mnuMainEditSettings.Enabled = True
        pnlLoadSettings.Visible = False
        cmnuTraySettings.Enabled = True
        cmnuTrayExit.Enabled = True

        'set all lists back to default before run "FillList"
        currList_Movies = "movielist"
        currList_MovieSets = "setslist"
        currList_TVShows = "tvshowlist"

        If Not dresult.DidCancel Then

            SetUp(True)

            If dgvMovies.RowCount > 0 Then
                dgvMovies.Columns("BannerPath").Visible = Not CheckColumnHide_Movies("BannerPath")
                dgvMovies.Columns("ClearArtPath").Visible = Not CheckColumnHide_Movies("ClearArtPath")
                dgvMovies.Columns("ClearLogoPath").Visible = Not CheckColumnHide_Movies("ClearLogoPath")
                dgvMovies.Columns("DiscArtPath").Visible = Not CheckColumnHide_Movies("DiscArtPath")
                dgvMovies.Columns("EFanartsPath").Visible = Not CheckColumnHide_Movies("EFanartsPath")
                dgvMovies.Columns("EThumbsPath").Visible = Not CheckColumnHide_Movies("EThumbsPath")
                dgvMovies.Columns("FanartPath").Visible = Not CheckColumnHide_Movies("FanartPath")
                dgvMovies.Columns("HasSet").Visible = Not CheckColumnHide_Movies("HasSet")
                dgvMovies.Columns("HasSub").Visible = Not CheckColumnHide_Movies("HasSub")
                dgvMovies.Columns("Imdb").Visible = Not CheckColumnHide_Movies("Imdb")
                dgvMovies.Columns("LandscapePath").Visible = Not CheckColumnHide_Movies("LandscapePath")
                dgvMovies.Columns("MPAA").Visible = Not CheckColumnHide_Movies("MPAA")
                dgvMovies.Columns("NfoPath").Visible = Not CheckColumnHide_Movies("NfoPath")
                dgvMovies.Columns("OriginalTitle").Visible = Not CheckColumnHide_Movies("OriginalTitle")
                dgvMovies.Columns("Playcount").Visible = Not CheckColumnHide_Movies("Playcount")
                dgvMovies.Columns("PosterPath").Visible = Not CheckColumnHide_Movies("PosterPath")
                dgvMovies.Columns("Rating").Visible = Not CheckColumnHide_Movies("Rating")
                dgvMovies.Columns("ThemePath").Visible = Not CheckColumnHide_Movies("ThemePath")
                dgvMovies.Columns("TMDB").Visible = Not CheckColumnHide_Movies("TMDB")
                dgvMovies.Columns("TrailerPath").Visible = Not CheckColumnHide_Movies("TrailerPath")
                dgvMovies.Columns("Year").Visible = Not CheckColumnHide_Movies("Year")
            End If

            If dgvMovieSets.RowCount > 0 Then
                dgvMovieSets.Columns("BannerPath").Visible = Not CheckColumnHide_MovieSets("BannerPath")
                dgvMovieSets.Columns("ClearArtPath").Visible = Not CheckColumnHide_MovieSets("ClearArtPath")
                dgvMovieSets.Columns("ClearLogoPath").Visible = Not CheckColumnHide_MovieSets("ClearLogoPath")
                dgvMovieSets.Columns("DiscArtPath").Visible = Not CheckColumnHide_MovieSets("DiscArtPath")
                dgvMovieSets.Columns("FanartPath").Visible = Not CheckColumnHide_MovieSets("FanartPath")
                dgvMovieSets.Columns("LandscapePath").Visible = Not CheckColumnHide_MovieSets("LandscapePath")
                dgvMovieSets.Columns("NfoPath").Visible = Not CheckColumnHide_MovieSets("NfoPath")
                dgvMovieSets.Columns("PosterPath").Visible = Not CheckColumnHide_MovieSets("PosterPath")
            End If

            If dgvTVShows.RowCount > 0 Then
                dgvTVShows.Columns("BannerPath").Visible = Not CheckColumnHide_TVShows("BannerPath")
                dgvTVShows.Columns("CharacterArtPath").Visible = Not CheckColumnHide_TVShows("CharacterArtPath")
                dgvTVShows.Columns("ClearArtPath").Visible = Not CheckColumnHide_TVShows("ClearArtPath")
                dgvTVShows.Columns("ClearLogoPath").Visible = Not CheckColumnHide_TVShows("ClearLogoPath")
                dgvTVShows.Columns("EFanartsPath").Visible = Not CheckColumnHide_TVShows("EFanartsPath")
                dgvTVShows.Columns("FanartPath").Visible = Not CheckColumnHide_TVShows("FanartPath")
                dgvTVShows.Columns("LandscapePath").Visible = Not CheckColumnHide_TVShows("LandscapePath")
                dgvTVShows.Columns("NfoPath").Visible = Not CheckColumnHide_TVShows("NfoPath")
                dgvTVShows.Columns("PosterPath").Visible = Not CheckColumnHide_TVShows("PosterPath")
                dgvTVShows.Columns("Status").Visible = Not CheckColumnHide_TVShows("Status")
                dgvTVShows.Columns("ThemePath").Visible = Not CheckColumnHide_TVShows("ThemePath")
            End If

            If dgvTVSeasons.RowCount > 0 Then
                dgvTVSeasons.Columns("BannerPath").Visible = Not CheckColumnHide_TVSeasons("BannerPath")
                dgvTVSeasons.Columns("FanartPath").Visible = Not CheckColumnHide_TVSeasons("FanartPath")
                dgvTVSeasons.Columns("LandscapePath").Visible = Not CheckColumnHide_TVSeasons("LandscapePath")
                dgvTVSeasons.Columns("PosterPath").Visible = Not CheckColumnHide_TVSeasons("PosterPath")
            End If

            If dgvTVEpisodes.RowCount > 0 Then
                dgvTVEpisodes.Columns("FanartPath").Visible = Not CheckColumnHide_TVEpisodes("FanartPath")
                dgvTVEpisodes.Columns("PosterPath").Visible = Not CheckColumnHide_TVEpisodes("PosterPath")
                dgvTVEpisodes.Columns("HasSub").Visible = Not CheckColumnHide_TVEpisodes("HasSub")
                dgvTVEpisodes.Columns("Playcount").Visible = Not CheckColumnHide_TVEpisodes("Playcount")
            End If

            'might as well wait for these
            While bwDownloadPic.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If dresult.NeedsDBClean_Movie OrElse
                dresult.NeedsDBClean_TV OrElse
                dresult.NeedsDBUpdate_Movie OrElse
                dresult.NeedsDBUpdate_TV OrElse
                dresult.NeedsReload_Movie OrElse
                dresult.NeedsReload_MovieSet OrElse
                dresult.NeedsReload_TVEpisode OrElse
                dresult.NeedsReload_TVShow Then

                If dresult.NeedsDBClean_Movie OrElse dresult.NeedsDBClean_TV Then
                    If MessageBox.Show(String.Format(Master.eLang.GetString(1007, "You've changed a setting that makes it necessary that the database is cleaned up. Please make sure that all sources are available!{0}{0}Should the process be continued?"), Environment.NewLine), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                        While bwLoadMovieInfo.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse
                            bwLoadMovieSetInfo.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
                            bwLoadEpInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwReload_TVShows.IsBusy OrElse bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        Dim DBCleaner As New Structures.ScanOrClean
                        'it's not necessary to clean the DB if we clean it anyway after DB update
                        DBCleaner.Movies = dresult.NeedsDBClean_Movie AndAlso Not (dresult.NeedsDBUpdate_Movie AndAlso Master.eSettings.MovieCleanDB)
                        DBCleaner.TV = dresult.NeedsDBClean_TV AndAlso Not (dresult.NeedsDBUpdate_TV AndAlso Master.eSettings.TVCleanDB)
                        CleanDB(DBCleaner)
                    End If
                End If

                If dresult.NeedsReload_Movie Then
                    If Not fScanner.IsBusy Then
                        While bwLoadMovieInfo.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse
                            bwLoadMovieSetInfo.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
                            bwLoadEpInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwReload_TVShows.IsBusy OrElse bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        ReloadAll_Movie()
                    End If
                End If
                If dresult.NeedsReload_MovieSet Then
                    If Not fScanner.IsBusy Then
                        While bwLoadMovieInfo.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse
                            bwLoadMovieSetInfo.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
                            bwLoadEpInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwReload_TVShows.IsBusy OrElse bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        ReloadAll_MovieSet()
                    End If
                End If
                If dresult.NeedsReload_TVEpisode OrElse dresult.NeedsReload_TVShow Then
                    If Not fScanner.IsBusy Then
                        While bwLoadMovieInfo.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse
                            bwLoadMovieSetInfo.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
                            bwLoadEpInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwReload_TVShows.IsBusy OrElse bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        ReloadAll_TVShow(dresult.NeedsReload_TVEpisode)
                    End If
                End If
                If dresult.NeedsDBUpdate_Movie OrElse dresult.NeedsDBUpdate_TV Then
                    If Not fScanner.IsBusy Then
                        While bwLoadMovieInfo.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse
                            bwLoadMovieSetInfo.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
                            bwLoadEpInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwReload_TVShows.IsBusy OrElse bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        LoadMedia(New Structures.ScanOrClean With {.Movies = dresult.NeedsDBUpdate_Movie, .TV = dresult.NeedsDBUpdate_TV})
                    End If
                End If
            End If

            If Not fScanner.IsBusy AndAlso Not bwLoadMovieInfo.IsBusy AndAlso Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso
                    Not bwLoadMovieSetInfo.IsBusy AndAlso Not bwMovieSetScraper.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso
                    Not bwLoadEpInfo.IsBusy AndAlso Not bwLoadSeasonInfo.IsBusy AndAlso Not bwLoadShowInfo.IsBusy AndAlso Not bwReload_TVShows.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                FillList(True, True, True)
            End If

            SetMenus(True)

            If dresult.NeedsRestart Then
                While bwLoadMovieInfo.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwReload_Movies.IsBusy OrElse
                    bwLoadMovieSetInfo.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwReload_MovieSets.IsBusy OrElse
                    bwLoadEpInfo.IsBusy OrElse bwLoadSeasonInfo.IsBusy OrElse bwLoadShowInfo.IsBusy OrElse bwReload_TVShows.IsBusy OrElse bwCleanDB.IsBusy
                    Application.DoEvents()
                    Threading.Thread.Sleep(50)
                End While
                Dim dRestart As New dlgRestart
                If dRestart.ShowDialog = DialogResult.OK Then
                    Application.Restart()
                End If
            End If
        Else
            SetMenus(False)
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub mnuMainEditSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainEditSettings.Click, cmnuTraySettings.Click
        Try
            SetControlsEnabled(False)
            pnlLoadSettings.Visible = True

            Dim dThread As Threading.Thread = New Threading.Thread(AddressOf ShowSettings)
            dThread.SetApartmentState(Threading.ApartmentState.STA)
            dThread.Start()
        Catch ex As Exception
            mnuMainEditSettings.Enabled = True
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub UpdateMainTabCounts()
        For Each mTabPage As TabPage In tcMain.Controls
            Dim currTag As Structures.MainTabType = DirectCast(mTabPage.Tag, Structures.MainTabType)
            Dim mCount As Integer = Master.DB.GetViewMediaCount(currTag.DefaultList)
            Select Case currTag.ContentType
                Case Enums.ContentType.Movie, Enums.ContentType.MovieSet
                    If mCount > 0 Then
                        mTabPage.Text = String.Format("{0} ({1})", currTag.ContentName, mCount)
                    Else
                        mTabPage.Text = currTag.ContentName
                    End If
                Case Enums.ContentType.TV
                    If mCount > 0 Then
                        Dim epCount As Integer = Master.DB.GetViewMediaCount(currTag.DefaultList, True)
                        mTabPage.Text = String.Format("{0} ({1}/{2})", currTag.ContentName, mCount, epCount)
                    Else
                        mTabPage.Text = currTag.ContentName
                    End If
            End Select
        Next
    End Sub
    ''' <summary>
    ''' Update the displayed movie counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMovieCount()
        Dim currTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If currTag.ContentType = Enums.ContentType.Movie Then
            If dgvMovies.RowCount > 0 Then
                tcMain.SelectedTab.Text = String.Format("{0} ({1})", currTag.ContentName, dgvMovies.RowCount)
            Else
                tcMain.SelectedTab.Text = currTag.ContentName
            End If
        End If
    End Sub
    ''' <summary>
    ''' Update the displayed movieset counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMovieSetCount()
        Dim currTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If currTag.ContentType = Enums.ContentType.MovieSet Then
            If dgvMovieSets.RowCount > 0 Then
                tcMain.SelectedTab.Text = String.Format("{0} ({1})", currTag.ContentName, dgvMovieSets.RowCount)
            Else
                tcMain.SelectedTab.Text = currTag.ContentName
            End If
        End If
    End Sub
    ''' <summary>
    ''' Update the displayed show/episode counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTVCount()
        Dim currTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        If currTag.ContentType = Enums.ContentType.TV Then
            If dgvTVShows.RowCount > 0 Then
                Dim epCount As Integer = Master.DB.GetViewMediaCount(currList_TVShows, True)
                tcMain.SelectedTab.Text = String.Format("{0} ({1}/{2})", currTag.ContentName, dgvTVShows.RowCount, epCount)
            Else
                tcMain.SelectedTab.Text = currTag.ContentName
            End If
        End If
    End Sub
    ''' <summary>
    ''' Setup the default/initial text for the GUI's controls. 
    ''' Language used is based on the app's current setting.
    ''' </summary>
    ''' <param name="doTheme"></param>
    ''' <remarks></remarks>
    Private Sub SetUp(ByVal doTheme As Boolean)

        Try
            With Me
                .MinimumSize = New Size(800, 600)

                'Add
                Dim strAdd As String = Master.eLang.GetString(28, "Add")
                .mnuGenresAdd.Text = strAdd
                .mnuTagsAdd.Text = strAdd

                'All
                Dim strAll As String = Master.eLang.GetString(68, "All")
                .mnuScrapeSubmenuAll.Text = strAll

                'Missing Items
                Dim strMissingItems As String = Master.eLang.GetString(40, "Missing Items")
                .btnFilterMissing_Movies.Text = strMissingItems
                .btnFilterMissing_MovieSets.Text = strMissingItems
                .btnFilterMissing_Shows.Text = strMissingItems
                .mnuScrapeSubmenuMissing.Text = strMissingItems

                'New
                Dim strNew As String = Master.eLang.GetString(47, "New")
                .mnuScrapeSubmenuNew.Text = strNew

                'Marked
                Dim strMarked As String = Master.eLang.GetString(48, "Marked")
                .mnuScrapeSubmenuMarked.Text = strMarked

                'Change Language
                Dim strChangeLanguage As String = Master.eLang.GetString(1200, "Change Language")
                .cmnuMovieLanguage.Text = strChangeLanguage
                .cmnuMovieSetLanguage.Text = strChangeLanguage
                .cmnuShowLanguage.Text = strChangeLanguage

                'Current Filter
                Dim strCurrentFilter As String = Master.eLang.GetString(624, "Current Filter")
                .mnuScrapeSubmenuFilter.Text = strCurrentFilter

                'Custom Scraper
                Dim strCustomScraper = Master.eLang.GetString(81, "Custom Scraper...")
                .mnuScrapeSubmenuCustom.Text = strCustomScraper

                'Ask (Require Input If No Exact Match)
                Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                .mnuScrapeTypeAsk.Text = strAsk

                'Automatic (Force Best Match)
                Dim strAutomatic As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                .mnuScrapeTypeAuto.Text = strAutomatic

                'Skip (Skip If More Than One Match)
                Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
                .mnuScrapeTypeSkip.Text = strSkip

                'Actor Thumbs Only
                Dim strActorThumbsOnly = Master.eLang.GetString(973, "Actor Thumbs Only")
                .mnuScrapeModifierActorthumbs.Text = strActorThumbsOnly

                'All Items
                Dim strAllItems As String = Master.eLang.GetString(70, "All Items")
                .mnuScrapeModifierAll.Text = strAllItems

                'Banner Only
                Dim strBannerOnly As String = Master.eLang.GetString(1060, "Banner Only")
                .mnuScrapeModifierBanner.Text = strBannerOnly

                'CharacterArt Only
                Dim strCharacterArtOnly As String = Master.eLang.GetString(1121, "CharacterArt Only")
                .mnuScrapeModifierCharacterArt.Text = strCharacterArtOnly

                'ClearArt Only
                Dim strClearArtOnly As String = Master.eLang.GetString(1122, "ClearArt Only")
                .mnuScrapeModifierClearArt.Text = strClearArtOnly

                'ClearLogo Only
                Dim strClearLogoOnly As String = Master.eLang.GetString(1123, "ClearLogo Only")
                .mnuScrapeModifierClearLogo.Text = strClearLogoOnly

                'DiscArt Only
                Dim strDiscArtOnly As String = Master.eLang.GetString(1124, "DiscArt Only")
                .mnuScrapeModifierDiscArt.Text = strDiscArtOnly

                'Edit Genres
                Dim strEditGenres As String = Master.eLang.GetString(1051, "Edit Genres")
                .cmnuMovieEditGenres.Text = strEditGenres
                .cmnuShowEditGenres.Text = strEditGenres

                'Edit Movie Sorting
                Dim strEditMovieSorting As String = Master.eLang.GetString(939, "Edit Movie Sorting")
                .cmnuMovieSetEditSortMethod.Text = strEditMovieSorting

                'Edit Season
                Dim strEditSeason As String = Master.eLang.GetString(769, "Edit Season")
                .cmnuSeasonEdit.Text = strEditSeason

                'Edit Tags
                Dim strEditTags As String = Master.eLang.GetString(1052, "Edit Tags")
                .cmnuMovieEditTags.Text = strEditTags
                .cmnuShowEditTags.Text = strEditTags

                'Extrafanarts Only
                Dim strExtrafanartsOnly As String = Master.eLang.GetString(975, "Extrafanarts Only")
                .mnuScrapeModifierExtrafanarts.Text = strExtrafanartsOnly

                'Extrathumbs Only
                Dim strExtrathumbsOnly As String = Master.eLang.GetString(74, "Extrathumbs Only")
                .mnuScrapeModifierExtrathumbs.Text = strExtrathumbsOnly

                'Fanart Only
                Dim strFanartOnly As String = Master.eLang.GetString(73, "Fanart Only")
                .mnuScrapeModifierFanart.Text = strFanartOnly

                'Landscape Only
                Dim strLandscapeOnly As String = Master.eLang.GetString(1061, "Landscape Only")
                .mnuScrapeModifierLandscape.Text = strLandscapeOnly

                'Mark as Watched
                Dim strMarkAsWatched As String = Master.eLang.GetString(1072, "Mark as Watched")
                cmnuMovieWatched.Text = strMarkAsWatched
                cmnuEpisodeWatched.Text = strMarkAsWatched
                cmnuSeasonWatched.Text = strMarkAsWatched
                cmnuShowWatched.Text = strMarkAsWatched

                'Mark as Unwatched
                Dim strMarkAsUnwatched As String = Master.eLang.GetString(1073, "Mark as Unatched")
                cmnuMovieUnwatched.Text = strMarkAsUnwatched
                cmnuEpisodeUnwatched.Text = strMarkAsUnwatched
                cmnuSeasonUnwatched.Text = strMarkAsUnwatched
                cmnuShowUnwatched.Text = strMarkAsUnwatched

                'Meta Data Only
                Dim strMetaDataOnly As String = Master.eLang.GetString(76, "Meta Data Only")
                .mnuScrapeModifierMetaData.Text = strMetaDataOnly

                'NFO Only
                Dim strNFOOnly As String = Master.eLang.GetString(71, "NFO Only")
                .mnuScrapeModifierNFO.Text = strNFOOnly

                'Poster Only
                Dim strPosterOnly As String = Master.eLang.GetString(72, "Poster Only")
                .mnuScrapeModifierPoster.Text = strPosterOnly

                'Reload All Movies
                Dim strReloadAllMovies As String = Master.eLang.GetString(18, "Re&load All Movies")
                .cmnuTrayToolsReloadMovies.Text = strReloadAllMovies
                .mnuMainToolsReloadMovies.Text = strReloadAllMovies

                'Reload All MovieSets
                Dim strReloadAllMovieSets As String = Master.eLang.GetString(1208, "Reload All MovieSets")
                .cmnuTrayToolsReloadMovieSets.Text = strReloadAllMovieSets
                .mnuMainToolsReloadMovieSets.Text = strReloadAllMovieSets

                'Reload All TV Shows
                Dim strReloadAllTVShows As String = Master.eLang.GetString(250, "Reload All TV Shows")
                .cmnuTrayToolsReloadTVShows.Text = strReloadAllTVShows
                .mnuMainToolsReloadTVShows.Text = strReloadAllTVShows

                'Remove
                Dim strRemove As String = Master.eLang.GetString(30, "Remove")
                .mnuGenresRemove.Text = strRemove
                .mnuTagsRemove.Text = strRemove

                'Scrape Movies
                Dim strScrapeMovies As String = Master.eLang.GetString(67, "Scrape Movies")
                .mnuScrapeMovies.Text = strScrapeMovies
                .cmnuTrayScrapeMovies.Text = strScrapeMovies

                'Scrape MovieSets
                Dim strScrapeMovieSets As String = Master.eLang.GetString(1213, "Scrape MovieSets")
                .mnuScrapeMovieSets.Text = strScrapeMovieSets
                .cmnuTrayScrapeMovieSets.Text = strScrapeMovieSets

                'Scrape TV Shows
                Dim strScrapeTVShows As String = Master.eLang.GetString(1234, "Scrape TV Shows")
                .mnuScrapeTVShows.Text = strScrapeTVShows
                .cmnuTrayScrapeTVShows.Text = strScrapeTVShows

                'Set
                Dim strSet As String = Master.eLang.GetString(29, "Set")
                .cmnuMovieSetEditSortMethodSet.Text = strSet
                .mnuGenresSet.Text = strSet
                .mnuLanguagesSet.Text = strSet
                .mnuTagsSet.Text = strSet

                'Select Genre
                Dim strSelectGenre As String = Master.eLang.GetString(27, "Select Genre")
                .mnuGenresTitleSelect.Text = String.Concat(">> ", strSelectGenre, " <<")

                'Select Tag
                Dim strSelectTag As String = Master.eLang.GetString(1021, "Select Tag")
                .mnuTagsTitleSelect.Text = String.Concat(">> ", strSelectTag, " <<")

                'Update Single Data Field
                Dim strUpdateSingelDataField As String = Master.eLang.GetString(1126, "(Re)Scrape Single Data Field")
                .cmnuEpisodeScrapeSingleDataField.Text = strUpdateSingelDataField
                .cmnuMovieScrapeSingleDataField.Text = strUpdateSingelDataField
                .cmnuMovieSetScrapeSingleDataField.Text = strUpdateSingelDataField
                .cmnuSeasonScrapeSingleDataField.Text = strUpdateSingelDataField
                .cmnuShowScrapeSingleDataField.Text = strUpdateSingelDataField

                'Theme Only
                Dim strThemeOnly As String = Master.eLang.GetString(1125, "Theme Only")
                .mnuScrapeModifierTheme.Text = strThemeOnly

                'Trailer Only
                Dim strTrailerOnly As String = Master.eLang.GetString(75, "Trailer Only")
                .mnuScrapeModifierTrailer.Text = strTrailerOnly

                ' others
                .btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                .btnClearFilters_Movies.Text = Master.eLang.GetString(37, "Clear Filters")
                .btnClearFilters_MovieSets.Text = .btnClearFilters_Movies.Text
                .btnClearFilters_Shows.Text = .btnClearFilters_Movies.Text
                .btnMarkAll.Text = Master.eLang.GetString(35, "Mark All")
                .btnMetaDataRefresh.Text = Master.eLang.GetString(58, "Refresh")
                .btnFilterSortDateAdded_Movies.Tag = String.Empty
                .btnFilterSortDateAdded_Movies.Text = Master.eLang.GetString(601, "Date Added")
                .btnFilterSortDateModified_Movies.Tag = String.Empty
                .btnFilterSortDateModified_Movies.Text = Master.eLang.GetString(1330, "Date Modified")
                .btnFilterSortRating_Movies.Tag = String.Empty
                .btnFilterSortRating_Movies.Text = Master.eLang.GetString(400, "Rating")
                .btnFilterSortTitle_Movies.Tag = String.Empty
                .btnFilterSortTitle_Movies.Text = Master.eLang.GetString(642, "Sort Title")
                .btnFilterSortTitle_Shows.Tag = String.Empty
                .btnFilterSortTitle_Shows.Text = Master.eLang.GetString(642, "Sort Title")
                .btnFilterSortYear_Movies.Tag = String.Empty
                .btnFilterSortYear_Movies.Text = Master.eLang.GetString(278, "Year")
                .chkFilterDuplicates_Movies.Text = Master.eLang.GetString(41, "Duplicates")
                .chkFilterEmpty_MovieSets.Text = Master.eLang.GetString(1275, "Empty")
                .chkFilterLock_Movies.Text = Master.eLang.GetString(43, "Locked")
                .chkFilterLock_MovieSets.Text = chkFilterLock_Movies.Text
                .chkFilterLock_Shows.Text = chkFilterLock_Movies.Text
                .chkFilterMark_Movies.Text = Master.eLang.GetString(48, "Marked")
                .chkFilterMark_MovieSets.Text = .chkFilterMark_Movies.Text
                .chkFilterMark_Shows.Text = .chkFilterMark_Movies.Text
                .chkFilterMarkCustom1_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
                .chkFilterMarkCustom2_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
                .chkFilterMarkCustom3_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
                .chkFilterMarkCustom4_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
                .chkFilterMultiple_MovieSets.Text = Master.eLang.GetString(876, "Multiple Movies")
                .chkFilterNew_Movies.Text = Master.eLang.GetString(47, "New")
                .chkFilterNew_MovieSets.Text = .chkFilterNew_Movies.Text
                .chkFilterNewEpisodes_Shows.Text = Master.eLang.GetString(1361, "New Episode(s)")
                .chkFilterNewShows_Shows.Text = Master.eLang.GetString(1362, "New Show(s)")
                .chkFilterOne_MovieSets.Text = Master.eLang.GetString(1289, "Only One Movie")
                .chkFilterTolerance_Movies.Text = Master.eLang.GetString(39, "Out of Tolerance")

                RemoveHandler chkMovieMissingBanner.CheckedChanged, AddressOf chkMovieMissingBanner_CheckedChanged
                .chkMovieMissingBanner.Checked = Master.eSettings.MovieMissingBanner
                AddHandler chkMovieMissingBanner.CheckedChanged, AddressOf chkMovieMissingBanner_CheckedChanged

                RemoveHandler chkMovieMissingClearArt.CheckedChanged, AddressOf chkMovieMissingClearArt_CheckedChanged
                .chkMovieMissingClearArt.Checked = Master.eSettings.MovieMissingClearArt
                AddHandler chkMovieMissingClearArt.CheckedChanged, AddressOf chkMovieMissingClearArt_CheckedChanged

                RemoveHandler chkMovieMissingClearLogo.CheckedChanged, AddressOf chkMovieMissingClearLogo_CheckedChanged
                .chkMovieMissingClearLogo.Checked = Master.eSettings.MovieMissingClearLogo
                AddHandler chkMovieMissingClearLogo.CheckedChanged, AddressOf chkMovieMissingClearLogo_CheckedChanged

                RemoveHandler chkMovieMissingDiscArt.CheckedChanged, AddressOf chkMovieMissingDiscArt_CheckedChanged
                .chkMovieMissingDiscArt.Checked = Master.eSettings.MovieMissingDiscArt
                AddHandler chkMovieMissingDiscArt.CheckedChanged, AddressOf chkMovieMissingDiscArt_CheckedChanged

                RemoveHandler chkMovieMissingExtrafanarts.CheckedChanged, AddressOf chkMovieMissingExtrafanarts_CheckedChanged
                .chkMovieMissingExtrafanarts.Checked = Master.eSettings.MovieMissingExtrafanarts
                AddHandler chkMovieMissingExtrafanarts.CheckedChanged, AddressOf chkMovieMissingExtrafanarts_CheckedChanged

                RemoveHandler chkMovieMissingExtrathumbs.CheckedChanged, AddressOf chkMovieMissingExtrathumbs_CheckedChanged
                .chkMovieMissingExtrathumbs.Checked = Master.eSettings.MovieMissingExtrathumbs
                AddHandler chkMovieMissingExtrathumbs.CheckedChanged, AddressOf chkMovieMissingExtrathumbs_CheckedChanged

                RemoveHandler chkMovieMissingFanart.CheckedChanged, AddressOf chkMovieMissingFanart_CheckedChanged
                .chkMovieMissingFanart.Checked = Master.eSettings.MovieMissingFanart
                AddHandler chkMovieMissingFanart.CheckedChanged, AddressOf chkMovieMissingFanart_CheckedChanged

                RemoveHandler chkMovieMissingLandscape.CheckedChanged, AddressOf chkMovieMissingLandscape_CheckedChanged
                .chkMovieMissingLandscape.Checked = Master.eSettings.MovieMissingLandscape
                AddHandler chkMovieMissingLandscape.CheckedChanged, AddressOf chkMovieMissingLandscape_CheckedChanged

                RemoveHandler chkMovieMissingNFO.CheckedChanged, AddressOf chkMovieMissingNFO_CheckedChanged
                .chkMovieMissingNFO.Checked = Master.eSettings.MovieMissingNFO
                AddHandler chkMovieMissingNFO.CheckedChanged, AddressOf chkMovieMissingNFO_CheckedChanged

                RemoveHandler chkMovieMissingPoster.CheckedChanged, AddressOf chkMovieMissingPoster_CheckedChanged
                .chkMovieMissingPoster.Checked = Master.eSettings.MovieMissingPoster
                AddHandler chkMovieMissingPoster.CheckedChanged, AddressOf chkMovieMissingPoster_CheckedChanged

                RemoveHandler chkMovieMissingSubtitles.CheckedChanged, AddressOf chkMovieMissingSubtitles_CheckedChanged
                .chkMovieMissingSubtitles.Checked = Master.eSettings.MovieMissingSubtitles
                AddHandler chkMovieMissingSubtitles.CheckedChanged, AddressOf chkMovieMissingSubtitles_CheckedChanged

                RemoveHandler chkMovieMissingTheme.CheckedChanged, AddressOf chkMovieMissingTheme_CheckedChanged
                .chkMovieMissingTheme.Checked = Master.eSettings.MovieMissingTheme
                AddHandler chkMovieMissingTheme.CheckedChanged, AddressOf chkMovieMissingTheme_CheckedChanged

                RemoveHandler chkMovieMissingTrailer.CheckedChanged, AddressOf chkMovieMissingTrailer_CheckedChanged
                .chkMovieMissingTrailer.Checked = Master.eSettings.MovieMissingTrailer
                AddHandler chkMovieMissingTrailer.CheckedChanged, AddressOf chkMovieMissingTrailer_CheckedChanged

                RemoveHandler chkMovieSetMissingBanner.CheckedChanged, AddressOf chkMovieSetMissingBanner_CheckedChanged
                .chkMovieSetMissingBanner.Checked = Master.eSettings.MovieSetMissingBanner
                AddHandler chkMovieSetMissingBanner.CheckedChanged, AddressOf chkMovieSetMissingBanner_CheckedChanged

                RemoveHandler chkMovieSetMissingClearArt.CheckedChanged, AddressOf chkMovieSetMissingClearArt_CheckedChanged
                .chkMovieSetMissingClearArt.Checked = Master.eSettings.MovieSetMissingClearArt
                AddHandler chkMovieSetMissingClearArt.CheckedChanged, AddressOf chkMovieSetMissingClearArt_CheckedChanged

                RemoveHandler chkMovieSetMissingClearLogo.CheckedChanged, AddressOf chkMovieSetMissingClearLogo_CheckedChanged
                .chkMovieSetMissingClearLogo.Checked = Master.eSettings.MovieSetMissingClearLogo
                AddHandler chkMovieSetMissingClearLogo.CheckedChanged, AddressOf chkMovieSetMissingClearLogo_CheckedChanged

                RemoveHandler chkMovieSetMissingDiscArt.CheckedChanged, AddressOf chkMovieSetMissingDiscArt_CheckedChanged
                .chkMovieSetMissingDiscArt.Checked = Master.eSettings.MovieSetMissingDiscArt
                AddHandler chkMovieSetMissingDiscArt.CheckedChanged, AddressOf chkMovieSetMissingDiscArt_CheckedChanged

                RemoveHandler chkMovieSetMissingFanart.CheckedChanged, AddressOf chkMovieSetMissingFanart_CheckedChanged
                .chkMovieSetMissingFanart.Checked = Master.eSettings.MovieSetMissingFanart
                AddHandler chkMovieSetMissingFanart.CheckedChanged, AddressOf chkMovieSetMissingFanart_CheckedChanged

                RemoveHandler chkMovieSetMissingLandscape.CheckedChanged, AddressOf chkMovieSetMissingLandscape_CheckedChanged
                .chkMovieSetMissingLandscape.Checked = Master.eSettings.MovieSetMissingLandscape
                AddHandler chkMovieSetMissingLandscape.CheckedChanged, AddressOf chkMovieSetMissingLandscape_CheckedChanged

                RemoveHandler chkMovieSetMissingNFO.CheckedChanged, AddressOf chkMovieSetMissingNFO_CheckedChanged
                .chkMovieSetMissingNFO.Checked = Master.eSettings.MovieSetMissingNFO
                AddHandler chkMovieSetMissingNFO.CheckedChanged, AddressOf chkMovieSetMissingNFO_CheckedChanged

                RemoveHandler chkMovieSetMissingPoster.CheckedChanged, AddressOf chkMovieSetMissingPoster_CheckedChanged
                .chkMovieSetMissingPoster.Checked = Master.eSettings.MovieSetMissingPoster
                AddHandler chkMovieSetMissingPoster.CheckedChanged, AddressOf chkMovieSetMissingPoster_CheckedChanged

                RemoveHandler chkShowMissingBanner.CheckedChanged, AddressOf chkShowMissingBanner_CheckedChanged
                .chkShowMissingBanner.Checked = Master.eSettings.TVShowMissingBanner
                AddHandler chkShowMissingBanner.CheckedChanged, AddressOf chkShowMissingBanner_CheckedChanged

                RemoveHandler chkShowMissingCharacterArt.CheckedChanged, AddressOf chkShowMissingCharacterArt_CheckedChanged
                .chkShowMissingCharacterArt.Checked = Master.eSettings.TVShowMissingCharacterArt
                AddHandler chkShowMissingCharacterArt.CheckedChanged, AddressOf chkShowMissingCharacterArt_CheckedChanged

                RemoveHandler chkShowMissingClearArt.CheckedChanged, AddressOf chkShowMissingClearArt_CheckedChanged
                .chkShowMissingClearArt.Checked = Master.eSettings.TVShowMissingClearArt
                AddHandler chkShowMissingClearArt.CheckedChanged, AddressOf chkShowMissingClearArt_CheckedChanged

                RemoveHandler chkShowMissingClearLogo.CheckedChanged, AddressOf chkShowMissingClearLogo_CheckedChanged
                .chkShowMissingClearLogo.Checked = Master.eSettings.TVShowMissingClearLogo
                AddHandler chkShowMissingClearLogo.CheckedChanged, AddressOf chkShowMissingClearLogo_CheckedChanged

                RemoveHandler chkShowMissingExtrafanarts.CheckedChanged, AddressOf chkShowMissingExtrafanarts_CheckedChanged
                .chkShowMissingExtrafanarts.Checked = Master.eSettings.TVShowMissingExtrafanarts
                AddHandler chkShowMissingExtrafanarts.CheckedChanged, AddressOf chkShowMissingExtrafanarts_CheckedChanged

                RemoveHandler chkShowMissingFanart.CheckedChanged, AddressOf chkShowMissingFanart_CheckedChanged
                .chkShowMissingFanart.Checked = Master.eSettings.TVShowMissingFanart
                AddHandler chkShowMissingFanart.CheckedChanged, AddressOf chkShowMissingFanart_CheckedChanged

                RemoveHandler chkShowMissingLandscape.CheckedChanged, AddressOf chkShowMissingLandscape_CheckedChanged
                .chkShowMissingLandscape.Checked = Master.eSettings.TVShowMissingLandscape
                AddHandler chkShowMissingLandscape.CheckedChanged, AddressOf chkShowMissingLandscape_CheckedChanged

                RemoveHandler chkShowMissingNFO.CheckedChanged, AddressOf chkShowMissingNFO_CheckedChanged
                .chkShowMissingNFO.Checked = Master.eSettings.TVShowMissingNFO
                AddHandler chkShowMissingNFO.CheckedChanged, AddressOf chkShowMissingNFO_CheckedChanged

                RemoveHandler chkShowMissingPoster.CheckedChanged, AddressOf chkShowMissingPoster_CheckedChanged
                .chkShowMissingPoster.Checked = Master.eSettings.TVShowMissingPoster
                AddHandler chkShowMissingPoster.CheckedChanged, AddressOf chkShowMissingPoster_CheckedChanged

                RemoveHandler chkShowMissingTheme.CheckedChanged, AddressOf chkShowMissingTheme_CheckedChanged
                .chkShowMissingTheme.Checked = Master.eSettings.TVShowMissingTheme
                AddHandler chkShowMissingTheme.CheckedChanged, AddressOf chkShowMissingTheme_CheckedChanged

                .cmnuEpisodeChange.Text = Master.eLang.GetString(772, "Change Episode")
                .cmnuEpisodeEdit.Text = Master.eLang.GetString(656, "Edit Episode")
                .cmnuEpisodeLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuEpisodeMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuEpisodeReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuEpisodeRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuEpisodeRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuEpisodeRemoveFromDisk.Text = Master.eLang.GetString(773, "Delete Episode")
                .cmnuEpisodeScrape.Text = Master.eLang.GetString(147, "(Re)Scrape Episode")
                .cmnuMovieBrowseIMDB.Text = Master.eLang.GetString(1281, "Open IMDB-Page")
                .cmnuMovieBrowseTMDB.Text = Master.eLang.GetString(1282, "Open TMDB-Page")
                .cmnuMovieChange.Text = Master.eLang.GetString(32, "Change Movie")
                .cmnuMovieChangeAuto.Text = Master.eLang.GetString(1294, "Change Movie (Auto)")
                .cmnuMovieEdit.Text = Master.eLang.GetString(25, "Edit Movie")
                .cmnuMovieEditMetaData.Text = Master.eLang.GetString(603, "Edit Meta Data")
                .cmnuMovieLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuMovieMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuMovieMarkAs.Text = Master.eLang.GetString(1192, "Mark as")
                .cmnuMovieMarkAsCustom1.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
                .cmnuMovieMarkAsCustom1.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                .cmnuMovieMarkAsCustom2.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
                .cmnuMovieMarkAsCustom2.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                .cmnuMovieMarkAsCustom3.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
                .cmnuMovieMarkAsCustom3.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                .cmnuMovieMarkAsCustom4.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
                .cmnuMovieMarkAsCustom4.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                .cmnuMovieOpenFolder.Text = Master.eLang.GetString(33, "Open Containing Folder")
                .cmnuMovieReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuMovieRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuMovieRemoveFromDB.Text = Master.eLang.GetString(646, "Remove From Database")
                .cmnuMovieRemoveFromDisk.Text = Master.eLang.GetString(34, "Delete Movie")
                .cmnuMovieScrape.Text = Master.eLang.GetString(163, "(Re)Scrape Movie")
                .cmnuMovieScrapeSelected.Text = Master.eLang.GetString(31, "(Re)Scrape Selected Movies")
                .cmnuMovieSetEdit.Text = Master.eLang.GetString(207, "Edit MovieSet")
                .cmnuMovieSetNew.Text = Master.eLang.GetString(208, "Add New MovieSet")
                .cmnuMovieSetScrape.Text = Master.eLang.GetString(1233, "(Re)Scrape MovieSet")
                .cmnuMovieTitle.Text = Master.eLang.GetString(21, "Title")
                .cmnuSeasonRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuSeasonLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuSeasonMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuSeasonReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuSeasonRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuSeasonRemoveFromDisk.Text = Master.eLang.GetString(771, "Delete Season")
                .cmnuSeasonScrape.Text = Master.eLang.GetString(146, "(Re)Scrape Season")
                .cmnuShowChange.Text = Master.eLang.GetString(767, "Change Show")
                .cmnuShowClearCache.Text = Master.eLang.GetString(565, "Clear Cache")
                .cmnuShowClearCacheDataAndImages.Text = Master.eLang.GetString(583, "Data and Images")
                .cmnuShowClearCacheDataOnly.Text = Master.eLang.GetString(566, "Data Only")
                .cmnuShowClearCacheImagesOnly.Text = Master.eLang.GetString(567, "Images Only")
                .cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
                .cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
                .cmnuShowLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuShowMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuShowReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuShowRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuShowRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuShowRemoveFromDisk.Text = Master.eLang.GetString(768, "Delete TV Show")
                .cmnuShowScrapeRefreshData.Text = Master.eLang.GetString(1066, "Refresh Data")
                .cmnuShowScrape.Text = Master.eLang.GetString(766, "(Re)Scrape Show")
                .cmnuTrayExit.Text = Master.eLang.GetString(2, "E&xit")
                .cmnuTraySettings.Text = Master.eLang.GetString(4, "&Settings...")
                .cmnuTrayTools.Text = Master.eLang.GetString(8, "&Tools")
                .cmnuTrayUpdate.Text = Master.eLang.GetString(82, "Update Library")
                .gbFilterDataField_Movies.Text = String.Concat(Master.eLang.GetString(1290, "Data Field"), ":")
                .gbFilterGeneral_Movies.Text = Master.eLang.GetString(38, "General")
                .gbFilterGeneral_MovieSets.Text = .gbFilterGeneral_Movies.Text
                .gbFilterGeneral_Shows.Text = Master.eLang.GetString(680, "Shows")
                .gbFilterModifier_Movies.Text = Master.eLang.GetString(44, "Modifier")
                .gbFilterModifier_MovieSets.Text = .gbFilterModifier_Movies.Text
                .gbFilterModifier_Shows.Text = .gbFilterModifier_Movies.Text
                .gbFilterSpecific_Movies.Text = Master.eLang.GetString(42, "Specific")
                .gbFilterSpecific_MovieSets.Text = .gbFilterSpecific_Movies.Text
                .gbFilterSpecific_Shows.Text = .gbFilterSpecific_Movies.Text
                .gbFilterSorting_Movies.Text = Master.eLang.GetString(600, "Extra Sorting")
                .gbFilterSorting_Shows.Text = .gbFilterSorting_Movies.Text
                .lblActorsHeader.Text = Master.eLang.GetString(231, "Actors")
                .lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                .lblCertificationsHeader.Text = Master.eLang.GetString(56, "Certifications")
                .lblCharacterArtTitle.Text = Master.eLang.GetString(1140, "CharacterArt")
                .lblClearArtTitle.Text = Master.eLang.GetString(1096, "ClearArt")
                .lblClearLogoTitle.Text = Master.eLang.GetString(1097, "ClearLogo")
                .lblDirectorsHeader.Text = Master.eLang.GetString(940, "Directors")
                .lblDiscArtTitle.Text = Master.eLang.GetString(1098, "DiscArt")
                .lblFanartSmallTitle.Text = Master.eLang.GetString(149, "Fanart")
                .lblFilePathHeader.Text = Master.eLang.GetString(60, "File Path")
                .lblFilter_Movies.Text = Master.eLang.GetString(52, "Filters")
                .lblFilter_MovieSets.Text = .lblFilter_Movies.Text
                .lblFilter_Shows.Text = .lblFilter_Movies.Text
                .lblFilterCountries_Movies.Text = Master.eLang.GetString(237, "Countries")
                .lblFilterCountriesClose_Movies.Text = Master.eLang.GetString(19, "Close")
                .lblFilterCountry_Movies.Text = String.Concat(Master.eLang.GetString(237, "Countries"), ":")
                .lblFilterVideoSource_Movies.Text = Master.eLang.GetString(824, "Video Source:")
                .lblFilterGenre_Movies.Text = String.Concat(Master.eLang.GetString(725, "Genres"), ":")
                .lblFilterGenre_Shows.Text = .lblFilterGenre_Movies.Text
                .lblFilterGenres_Movies.Text = Master.eLang.GetString(725, "Genres")
                .lblFilterGenres_Shows.Text = .lblFilterGenres_Movies.Text
                .lblFilterSource_Movies.Text = Master.eLang.GetString(50, "Source:")
                .lblFilterSource_Shows.Text = .lblFilterSource_Movies.Text
                .lblFilterSources_Movies.Text = Master.eLang.GetString(602, "Sources")
                .lblFilterSources_Shows.Text = .lblFilterSources_Movies.Text
                .lblFilterYear_Movies.Text = Master.eLang.GetString(49, "Year:")
                .lblFilterGenresClose_Movies.Text = Master.eLang.GetString(19, "Close")
                .lblFilterGenresClose_Shows.Text = .lblFilterGenresClose_Movies.Text
                .lblFilterDataFields_Movies.Text = Master.eLang.GetString(1290, "Data Field")
                .lblFilterDataFieldsClose_Movies.Text = .lblFilterGenresClose_Movies.Text
                .lblFilterSourcesClose_Movies.Text = .lblFilterGenresClose_Movies.Text
                .lblFilterSourcesClose_Shows.Text = .lblFilterGenresClose_Movies.Text
                .lblIMDBHeader.Text = Master.eLang.GetString(61, "IMDB ID")
                .lblInfoPanelHeader.Text = Master.eLang.GetString(66, "Info")
                .lblLandscapeTitle.Text = Master.eLang.GetString(1035, "Landscape")
                .lblLoadSettings.Text = Master.eLang.GetString(484, "Loading Settings...")
                .lblMetaDataHeader.Text = Master.eLang.GetString(59, "Meta Data")
                .lblMoviesInSetHeader.Text = Master.eLang.GetString(367, "Movies In Set")
                .lblOutlineHeader.Text = Master.eLang.GetString(64, "Plot Outline")
                .lblPlotHeader.Text = Master.eLang.GetString(65, "Plot")
                .lblPosterTitle.Text = Master.eLang.GetString(148, "Poster")
                .lblReleaseDateHeader.Text = Master.eLang.GetString(57, "Release Date")
                .lblTrailerPathHeader.Text = Master.eLang.GetString(1058, "Trailer Path")
                .mnuMainDonate.Text = Master.eLang.GetString(708, "Donate")
                .mnuMainDonate.Text = Master.eLang.GetString(708, "Donate")
                .mnuMainEdit.Text = Master.eLang.GetString(3, "&Edit")
                .mnuMainEditSettings.Text = Master.eLang.GetString(4, "&Settings...")
                .mnuMainFile.Text = Master.eLang.GetString(1, "&File")
                .mnuMainFileExit.Text = Master.eLang.GetString(2, "E&xit")
                .mnuMainHelp.Text = Master.eLang.GetString(5, "&Help")
                .mnuMainHelpAbout.Text = Master.eLang.GetString(6, "&About...")
                .mnuMainHelpUpdate.Text = Master.eLang.GetString(850, "&Check For Updates...")
                .mnuMainHelpVersions.Text = Master.eLang.GetString(793, "&Versions...")
                .mnuMainHelpWiki.Text = Master.eLang.GetString(869, "EmberMM.com &Wiki...")
                .mnuMainToolsExport.Text = Master.eLang.GetString(1174, "Export")
                .mnuMainToolsExportMovies.Text = Master.eLang.GetString(36, "Movies")
                .mnuMainToolsExportTvShows.Text = Master.eLang.GetString(653, "TV Shows")
                .mnuMainTools.Text = Master.eLang.GetString(8, "&Tools")
                .mnuMainToolsBackdrops.Text = Master.eLang.GetString(11, "Copy Existing Fanart To &Backdrops Folder")
                .mnuMainToolsCleanDB.Text = Master.eLang.GetString(709, "Clean &Database")
                .mnuMainToolsCleanFiles.Text = Master.eLang.GetString(9, "&Clean Files")
                .mnuMainToolsClearCache.Text = Master.eLang.GetString(17, "Clear &All Caches")
                .mnuMainToolsOfflineHolder.Text = Master.eLang.GetString(524, "&Offline Media Manager")
                .mnuMainToolsRewriteMovieContent.Text = Master.eLang.GetString(1298, "Rewrite All Movie Content")
                .mnuMainToolsSortFiles.Text = Master.eLang.GetString(10, "&Sort Files Into Folders")
                .mnuScrapeOptionActors.Text = Master.eLang.GetString(231, "Actors")
                .mnuScrapeOptionAired.Text = Master.eLang.GetString(728, "Aired")
                .mnuScrapeOptionCertifications.Text = Master.eLang.GetString(56, "Certification")
                .mnuScrapeOptionCollectionID.Text = Master.eLang.GetString(1135, "Collection ID")
                .mnuScrapeOptionCountries.Text = Master.eLang.GetString(237, "Countries")
                .mnuScrapeOptionCreators.Text = Master.eLang.GetString(744, "Creators")
                .mnuScrapeOptionDirectors.Text = Master.eLang.GetString(940, "Directors")
                .mnuScrapeOptionGenres.Text = Master.eLang.GetString(725, "Genres")
                .mnuScrapeOptionGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
                .mnuScrapeOptionMPAA.Text = Master.eLang.GetString(401, "MPAA")
                .mnuScrapeOptionOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
                .mnuScrapeOptionOutline.Text = Master.eLang.GetString(64, "Plot Outline")
                .mnuScrapeOptionPlot.Text = Master.eLang.GetString(65, "Plot")
                .mnuScrapeOptionPremiered.Text = Master.eLang.GetString(724, "Premiered")
                .mnuScrapeOptionRating.Text = Master.eLang.GetString(400, "Rating")
                .mnuScrapeOptionReleaseDate.Text = Master.eLang.GetString(57, "Release Date")
                .mnuScrapeOptionRuntime.Text = Master.eLang.GetString(396, "Runtime")
                .mnuScrapeOptionStatus.Text = Master.eLang.GetString(215, "Status")
                .mnuScrapeOptionStudios.Text = Master.eLang.GetString(226, "Studios")
                .mnuScrapeOptionTagline.Text = Master.eLang.GetString(397, "Tagline")
                .mnuScrapeOptionTitle.Text = Master.eLang.GetString(21, "Title")
                .mnuScrapeOptionTop250.Text = Master.eLang.GetString(591, "Top 250")
                .mnuScrapeOptionTrailer.Text = Master.eLang.GetString(151, "Trailer")
                .mnuScrapeOptionWriters.Text = Master.eLang.GetString(777, "Writer")
                .mnuScrapeOptionYear.Text = Master.eLang.GetString(278, "Year")
                .mnuUpdate.Text = Master.eLang.GetString(82, "Update Library")
                .mnuUpdateMovies.Text = Master.eLang.GetString(36, "Movies")
                .mnuUpdateShows.Text = Master.eLang.GetString(653, "TV Shows")
                .pnlFilterCountries_Movies.Tag = String.Empty
                .pnlFilterGenres_Movies.Tag = String.Empty
                .pnlFilterGenres_Shows.Tag = String.Empty
                .pnlFilterDataFields_Movies.Tag = String.Empty
                .pnlFilterSources_Movies.Tag = String.Empty
                .pnlFilterSources_Shows.Tag = String.Empty
                .rbFilterAnd_Movies.Text = Master.eLang.GetString(45, "And")
                .rbFilterAnd_MovieSets.Text = .rbFilterAnd_Movies.Text
                .rbFilterAnd_Shows.Text = .rbFilterAnd_Movies.Text
                .rbFilterOr_Movies.Text = Master.eLang.GetString(46, "Or")
                .rbFilterOr_MovieSets.Text = .rbFilterOr_Movies.Text
                .rbFilterOr_Shows.Text = .rbFilterOr_Movies.Text
                .tslLoading.Text = Master.eLang.GetString(7, "Loading Media:")

                .cmnuEpisodeOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuMovieSetLock.Text = .cmnuMovieLock.Text
                .cmnuMovieSetMark.Text = .cmnuMovieMark.Text
                .cmnuMovieSetReload.Text = .cmnuMovieReload.Text
                .cmnuMovieSetRemove.Text = .cmnuMovieRemove.Text
                .cmnuSeasonOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuShowOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuTrayToolsBackdrops.Text = .mnuMainToolsBackdrops.Text
                .cmnuTrayToolsCleanFiles.Text = .mnuMainToolsCleanFiles.Text
                .cmnuTrayToolsClearCache.Text = .mnuMainToolsClearCache.Text
                .cmnuTrayToolsOfflineHolder.Text = .mnuMainToolsOfflineHolder.Text
                .cmnuTrayToolsSortFiles.Text = .mnuMainToolsSortFiles.Text

                Dim TT As ToolTip = New System.Windows.Forms.ToolTip(.components)
                .mnuScrapeMovies.ToolTipText = Master.eLang.GetString(84, "Scrape/download data from the internet for multiple movies.")
                .mnuScrapeMovieSets.ToolTipText = Master.eLang.GetString(1214, "Scrape/download data from the internet for multiple moviesets.")
                .mnuScrapeTVShows.ToolTipText = Master.eLang.GetString(1235, "Scrape/download data from the internet for multiple tv shows.")
                .mnuUpdate.ToolTipText = Master.eLang.GetString(85, "Scans sources for new content and cleans database.")
                TT.SetToolTip(.btnMarkAll, Master.eLang.GetString(87, "Mark or Unmark all movies in the list."))
                TT.SetToolTip(.txtSearchMovies, Master.eLang.GetString(88, "Search the movie titles by entering text here."))
                TT.SetToolTip(.txtSearchMovieSets, Master.eLang.GetString(1267, "Search the movie titles by entering text here."))
                TT.SetToolTip(.txtSearchShows, Master.eLang.GetString(1268, "Search the tv show titles by entering text here."))
                TT.SetToolTip(.btnFilePlay, Master.eLang.GetString(89, "Play the movie file with the system default media player."))
                TT.SetToolTip(.btnMetaDataRefresh, Master.eLang.GetString(90, "Rescan and save the meta data for the selected movie."))
                TT.SetToolTip(.chkFilterDuplicates_Movies, Master.eLang.GetString(91, "Display only movies that have duplicate IMDB IDs."))
                TT.SetToolTip(.chkFilterTolerance_Movies, Master.eLang.GetString(92, "Display only movies whose title matching is out of tolerance."))
                TT.SetToolTip(.chkFilterMissing_Movies, Master.eLang.GetString(93, "Display only movies that have items missing."))
                TT.SetToolTip(.chkFilterNew_Movies, Master.eLang.GetString(94, "Display only new movies."))
                TT.SetToolTip(.chkFilterNew_MovieSets, Master.eLang.GetString(1269, "Display only new moviesets."))
                TT.SetToolTip(.chkFilterMark_Movies, Master.eLang.GetString(95, "Display only marked movies."))
                TT.SetToolTip(.chkFilterMark_MovieSets, Master.eLang.GetString(1270, "Display only marked moviesets."))
                TT.SetToolTip(.chkFilterLock_Movies, Master.eLang.GetString(96, "Display only locked movies."))
                TT.SetToolTip(.chkFilterLock_MovieSets, Master.eLang.GetString(1271, "Display only locked moviesets."))
                TT.SetToolTip(.txtFilterSource_Movies, Master.eLang.GetString(97, "Display only movies from the selected source."))
                TT.SetToolTip(.cbFilterVideoSource_Movies, Master.eLang.GetString(580, "Display only movies from the selected video source."))
                TT.Active = True

                RemoveHandler cbSearchMovies.SelectedIndexChanged, AddressOf cbSearchMovies_SelectedIndexChanged
                .cbSearchMovies.Items.Clear()
                .cbSearchMovies.Items.AddRange(New Object() {Master.eLang.GetString(21, "Title"), Master.eLang.GetString(302, "Original Title"), Master.eLang.GetString(100, "Actor"), Master.eLang.GetString(233, "Role"), Master.eLang.GetString(62, "Director"), Master.eLang.GetString(729, "Credits"), Master.eLang.GetString(301, "Country"), Master.eLang.GetString(395, "Studio")})
                If cbSearchMovies.Items.Count > 0 Then
                    cbSearchMovies.SelectedIndex = 0
                End If
                AddHandler cbSearchMovies.SelectedIndexChanged, AddressOf cbSearchMovies_SelectedIndexChanged

                RemoveHandler cbSearchMovieSets.SelectedIndexChanged, AddressOf cbSearchMovieSets_SelectedIndexChanged
                .cbSearchMovieSets.Items.Clear()
                .cbSearchMovieSets.Items.AddRange(New Object() {Master.eLang.GetString(21, "Title")})
                If cbSearchMovieSets.Items.Count > 0 Then
                    cbSearchMovieSets.SelectedIndex = 0
                End If
                AddHandler cbSearchMovieSets.SelectedIndexChanged, AddressOf cbSearchMovieSets_SelectedIndexChanged

                RemoveHandler cbSearchShows.SelectedIndexChanged, AddressOf cbSearchShows_SelectedIndexChanged
                .cbSearchShows.Items.Clear()
                .cbSearchShows.Items.AddRange(New Object() {Master.eLang.GetString(21, "Title")})
                If cbSearchShows.Items.Count > 0 Then
                    cbSearchShows.SelectedIndex = 0
                End If
                AddHandler cbSearchShows.SelectedIndexChanged, AddressOf cbSearchShows_SelectedIndexChanged

                If doTheme Then
                    tTheme = New Theming
                    Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
                    .ApplyTheme(If(currMainTabTag.ContentType = Enums.ContentType.Movie, Theming.ThemeType.Movie, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, Theming.ThemeType.MovieSet, Theming.ThemeType.Show)))
                End If

            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Updates the label indicating there is no information for the current item.
    ''' </summary>
    ''' <param name="ShowIt"><c>Boolean</c> indicating whether the panel should be shown or not</param>
    ''' <param name="tType"></param>
    ''' <remarks></remarks>
    Private Sub ShowNoInfo(ByVal ShowIt As Boolean, Optional ByVal tType As Enums.ContentType = Enums.ContentType.Movie)
        If ShowIt Then
            Select Case tType
                Case Enums.ContentType.Movie
                    lblNoInfo.Text = Master.eLang.GetString(55, "No information is available for this Movie")
                    If Not currThemeType = Theming.ThemeType.Movie Then ApplyTheme(Theming.ThemeType.Movie)
                Case Enums.ContentType.MovieSet
                    lblNoInfo.Text = Master.eLang.GetString(1154, "No information is available for this MovieSet")
                    If Not currThemeType = Theming.ThemeType.MovieSet Then ApplyTheme(Theming.ThemeType.MovieSet)
                Case Enums.ContentType.TVEpisode
                    lblNoInfo.Text = Master.eLang.GetString(652, "No information is available for this Episode")
                    If Not currThemeType = Theming.ThemeType.Episode Then ApplyTheme(Theming.ThemeType.Episode)
                Case Enums.ContentType.TVSeason
                    lblNoInfo.Text = Master.eLang.GetString(1161, "No information is available for this Season")
                    If Not currThemeType = Theming.ThemeType.Show Then ApplyTheme(Theming.ThemeType.Show)
                Case Enums.ContentType.TVShow
                    lblNoInfo.Text = Master.eLang.GetString(651, "No information is available for this Show")
                    If Not currThemeType = Theming.ThemeType.Show Then ApplyTheme(Theming.ThemeType.Show)
                Case Else
                    logger.Warn("Invalid media type <{0}>", tType)
            End Select
        End If

        pnlNoInfo.Visible = ShowIt
    End Sub
    ''' <summary>
    ''' Triggers the display of the Settings dialog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowSettings()
        While ModulesManager.Instance.QueryAnyGenericIsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Using dSettings As New dlgSettings
            Invoke(New MySettingsShow(AddressOf SettingsShow), dSettings)
        End Using
    End Sub

    Private Sub SourceSubClick_Movie(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SourceID As Long = -1

        If DirectCast(sender, ToolStripItem).Tag IsNot Nothing Then
            SourceID = Convert.ToInt64(DirectCast(sender, ToolStripItem).Tag)
        End If

        LoadMedia(New Structures.ScanOrClean With {.Movies = True}, SourceID)
    End Sub

    Private Sub SourceSubClick_TV(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SourceID As Long = -1

        If DirectCast(sender, ToolStripItem).Tag IsNot Nothing Then
            SourceID = Convert.ToInt64(DirectCast(sender, ToolStripItem).Tag)
        End If

        LoadMedia(New Structures.ScanOrClean With {.TV = True}, SourceID)
    End Sub

    Private Sub tcMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcMain.SelectedIndexChanged
        ClearInfo()
        ShowNoInfo(False)
        Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
        ModulesManager.Instance.RuntimeObjects.MediaTabSelected = currMainTabTag
        Select Case currMainTabTag.ContentType
            Case Enums.ContentType.Movie
                currList_Movies = currMainTabTag.DefaultList
                cbFilterLists_Movies.SelectedValue = currList_Movies
                ModulesManager.Instance.RuntimeObjects.ListMovies = currList_Movies
                FillList(True, False, False)
                mnuMainTools.Enabled = True
                cmnuTrayTools.Enabled = True
                mnuScrapeMovies.Visible = True
                mnuScrapeMovieSets.Visible = False
                mnuScrapeTVShows.Visible = False
                pnlFilter_Movies.Visible = True
                pnlFilter_MovieSets.Visible = False
                pnlFilter_Shows.Visible = False
                pnlFilterMissingItems_MovieSets.Visible = False
                pnlFilterMissingItems_Shows.Visible = False
                pnlListTop.Height = 56
                pnlSearchMovies.Visible = True
                pnlSearchMovieSets.Visible = False
                pnlSearchTVShows.Visible = False
                btnMarkAll.Visible = True
                dgvMovieSets.Visible = False
                dgvMovies.Visible = True
                ApplyTheme(Theming.ThemeType.Movie)
                If bwLoadEpInfo.IsBusy Then bwLoadEpInfo.CancelAsync()
                If bwLoadSeasonInfo.IsBusy Then bwLoadSeasonInfo.CancelAsync()
                If bwLoadShowInfo.IsBusy Then bwLoadShowInfo.CancelAsync()
                If bwLoadMovieSetInfo.IsBusy Then bwLoadMovieSetInfo.CancelAsync()
                If bwLoadMovieSetPosters.IsBusy Then bwLoadMovieSetPosters.CancelAsync()
                If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
                If dgvMovies.RowCount > 0 Then
                    prevRow_Movie = -1

                    dgvMovies.CurrentCell = Nothing
                    dgvMovies.ClearSelection()
                    dgvMovies.Rows(0).Selected = True
                    dgvMovies.CurrentCell = dgvMovies.Rows(0).Cells("ListTitle")

                    dgvMovies.Focus()
                Else
                    SetControlsEnabled(True)
                End If

            Case Enums.ContentType.MovieSet
                currList_MovieSets = currMainTabTag.DefaultList
                cbFilterLists_MovieSets.SelectedValue = currList_MovieSets
                ModulesManager.Instance.RuntimeObjects.ListMovieSets = currList_MovieSets
                FillList(False, True, False)
                mnuMainTools.Enabled = True
                cmnuTrayTools.Enabled = True
                mnuScrapeMovies.Visible = False
                mnuScrapeMovieSets.Visible = True
                mnuScrapeTVShows.Visible = False
                pnlFilter_Movies.Visible = False
                pnlFilter_MovieSets.Visible = True
                pnlFilter_Shows.Visible = False
                pnlFilterMissingItems_Movies.Visible = False
                pnlFilterMissingItems_Shows.Visible = False
                pnlListTop.Height = 56
                pnlSearchMovies.Visible = False
                pnlSearchMovieSets.Visible = True
                pnlSearchTVShows.Visible = False
                btnMarkAll.Visible = False
                dgvMovies.Visible = False
                dgvMovieSets.Visible = True
                ApplyTheme(Theming.ThemeType.MovieSet)
                If bwLoadMovieInfo.IsBusy Then bwLoadMovieInfo.CancelAsync()
                If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
                If bwLoadEpInfo.IsBusy Then bwLoadEpInfo.CancelAsync()
                If bwLoadSeasonInfo.IsBusy Then bwLoadSeasonInfo.CancelAsync()
                If bwLoadShowInfo.IsBusy Then bwLoadShowInfo.CancelAsync()
                If dgvMovieSets.RowCount > 0 Then
                    prevRow_MovieSet = -1

                    dgvMovieSets.CurrentCell = Nothing
                    dgvMovieSets.ClearSelection()
                    dgvMovieSets.Rows(0).Selected = True
                    dgvMovieSets.CurrentCell = dgvMovieSets.Rows(0).Cells("ListTitle")

                    dgvMovieSets.Focus()
                Else
                    SetControlsEnabled(True)
                End If

            Case Enums.ContentType.TV
                currList_TVShows = currMainTabTag.DefaultList
                cbFilterLists_Shows.SelectedValue = currList_TVShows
                ModulesManager.Instance.RuntimeObjects.ListTVShows = currList_TVShows
                FillList(False, False, True)
                mnuMainTools.Enabled = True
                cmnuTrayTools.Enabled = True
                mnuScrapeMovies.Visible = False
                mnuScrapeMovieSets.Visible = False
                mnuScrapeTVShows.Visible = True
                dgvMovies.Visible = False
                dgvMovieSets.Visible = False
                pnlFilter_Movies.Visible = False
                pnlFilter_MovieSets.Visible = False
                pnlFilter_Shows.Visible = True
                pnlFilterMissingItems_Movies.Visible = False
                pnlFilterMissingItems_MovieSets.Visible = False
                pnlListTop.Height = 56
                pnlSearchMovies.Visible = False
                pnlSearchMovieSets.Visible = False
                pnlSearchTVShows.Visible = True
                btnMarkAll.Visible = False
                ApplyTheme(Theming.ThemeType.Show)
                If bwLoadMovieInfo.IsBusy Then bwLoadMovieInfo.CancelAsync()
                If bwLoadMovieSetInfo.IsBusy Then bwLoadMovieSetInfo.CancelAsync()
                If bwLoadMovieSetPosters.IsBusy Then bwLoadMovieSetPosters.CancelAsync()
                If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
                If dgvTVShows.RowCount > 0 Then
                    prevRow_TVShow = -1
                    currList = 0

                    dgvTVShows.CurrentCell = Nothing
                    dgvTVShows.ClearSelection()
                    dgvTVShows.Rows(0).Selected = True
                    dgvTVShows.CurrentCell = dgvTVShows.Rows(0).Cells("ListTitle")

                    dgvTVShows.Focus()
                Else
                    SetControlsEnabled(True)
                End If
        End Select
    End Sub

    Private Sub tmrAni_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrAni.Tick
        Try
            Dim currMainTabTag As Structures.MainTabType = DirectCast(tcMain.SelectedTab.Tag, Structures.MainTabType)
            Select Case If(currMainTabTag.ContentType = Enums.ContentType.Movie, InfoPanelState_Movie, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, InfoPanelState_MovieSet, InfoPanelState_TVShow))
                Case 0
                    pnlInfoPanel.Height = 25

                Case 1
                    pnlInfoPanel.Height = IPMid

                Case 2
                    pnlInfoPanel.Height = IPUp
            End Select

            MoveGenres()
            MoveMPAA()

            Dim aType As Integer = If(currMainTabTag.ContentType = Enums.ContentType.Movie, InfoPanelState_Movie, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, InfoPanelState_MovieSet, InfoPanelState_TVShow))
            Select Case aType
                Case 0
                    If pnlInfoPanel.Height = 25 Then
                        tmrAni.Stop()
                        btnDown.Enabled = False
                        btnMid.Enabled = True
                        btnUp.Enabled = True
                    End If
                Case 1
                    If pnlInfoPanel.Height = IPMid Then
                        tmrAni.Stop()
                        btnMid.Enabled = False
                        btnDown.Enabled = True
                        btnUp.Enabled = True
                    End If
                Case 2
                    If pnlInfoPanel.Height = IPUp Then
                        tmrAni.Stop()
                        btnUp.Enabled = False
                        btnDown.Enabled = True
                        btnMid.Enabled = True
                    End If
            End Select

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub FilterMovement_Movies()
        If FilterPanelIsRaised_Movie Then
            pnlFilter_Movies.AutoSize = True
        Else
            pnlFilter_Movies.AutoSize = False
            pnlFilter_Movies.Height = pnlFilterTop_Movies.Height
        End If

        If pnlFilter_Movies.Height = pnlFilterTop_Movies.Height Then
            btnFilterUp_Movies.Enabled = True
            btnFilterDown_Movies.Enabled = False
        ElseIf pnlFilter_Movies.AutoSize Then
            btnFilterUp_Movies.Enabled = False
            btnFilterDown_Movies.Enabled = True
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub FilterMovement_MovieSets()
        If FilterPanelIsRaised_MovieSet Then
            pnlFilter_MovieSets.AutoSize = True
        Else
            pnlFilter_MovieSets.AutoSize = False
            pnlFilter_MovieSets.Height = pnlFilterTop_MovieSets.Height
        End If

        If pnlFilter_MovieSets.Height = pnlFilterTop_MovieSets.Height Then
            btnFilterUp_MovieSets.Enabled = True
            btnFilterDown_MovieSets.Enabled = False
        ElseIf pnlFilter_MovieSets.AutoSize Then
            btnFilterUp_MovieSets.Enabled = False
            btnFilterDown_MovieSets.Enabled = True
        End If

        dgvMovieSets.Invalidate()
    End Sub

    Private Sub FilterMovement_Shows()
        If FilterPanelIsRaised_TVShow Then
            pnlFilter_Shows.AutoSize = True
        Else
            pnlFilter_Shows.AutoSize = False
            pnlFilter_Shows.Height = pnlFilterTop_Shows.Height
        End If

        If pnlFilter_Shows.Height = pnlFilterTop_Shows.Height Then
            btnFilterUp_Shows.Enabled = True
            btnFilterDown_Shows.Enabled = False
        ElseIf pnlFilter_Shows.AutoSize Then
            btnFilterUp_Shows.Enabled = False
            btnFilterDown_Shows.Enabled = True
        End If

        dgvTVShows.Invalidate()
    End Sub

    Private Sub tmrLoad_TVEpisode_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_TVEpisode.Tick
        tmrWait_TVEpisode.Stop()
        tmrLoad_TVEpisode.Stop()

        If dgvTVEpisodes.SelectedRows.Count > 0 Then

            If dgvTVEpisodes.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVEpisodes.SelectedRows.Count))
            ElseIf dgvTVEpisodes.SelectedRows.Count = 1 Then
                SetStatus(dgvTVEpisodes.SelectedRows(0).Cells("Title").Value.ToString)
            End If

            SelectRow_TVEpisode(dgvTVEpisodes.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_TVSeason_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_TVSeason.Tick
        tmrWait_TVSeason.Stop()
        tmrLoad_TVSeason.Stop()

        If dgvTVSeasons.SelectedRows.Count > 0 Then

            If dgvTVSeasons.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVSeasons.SelectedRows.Count))
            ElseIf dgvMovies.SelectedRows.Count = 1 Then
                SetStatus(dgvTVSeasons.SelectedRows(0).Cells("SeasonText").Value.ToString)
            End If

            SelectRow_TVSeason(dgvTVSeasons.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_TVShow_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_TVShow.Tick
        tmrWait_TVShow.Stop()
        tmrLoad_TVShow.Stop()

        If dgvTVShows.SelectedRows.Count > 0 Then

            If dgvTVShows.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVShows.SelectedRows.Count))
            ElseIf dgvTVShows.SelectedRows.Count = 1 Then
                SetStatus(dgvTVShows.SelectedRows(0).Cells("TVShowPath").Value.ToString)
            End If

            SelectRow_TVShow(dgvTVShows.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_Movie_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_Movie.Tick
        tmrWait_Movie.Stop()
        tmrLoad_Movie.Stop()

        If dgvMovies.SelectedRows.Count > 0 Then

            If dgvMovies.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvMovies.SelectedRows.Count))
            ElseIf dgvMovies.SelectedRows.Count = 1 Then
                SetStatus(dgvMovies.SelectedRows(0).Cells("MoviePath").Value.ToString)
            End If

            SelectRow_Movie(dgvMovies.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_MovieSet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_MovieSet.Tick
        tmrWait_MovieSet.Stop()
        tmrLoad_MovieSet.Stop()

        If dgvMovieSets.SelectedRows.Count > 0 Then

            If dgvMovieSets.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvMovieSets.SelectedRows.Count))
            ElseIf dgvMovieSets.SelectedRows.Count = 1 Then
                SetStatus(dgvMovieSets.SelectedRows(0).Cells("SetName").Value.ToString)
            End If

            SelectRow_MovieSet(dgvMovieSets.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrRunTasks_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRunTasks.Tick
        tmrRunTasks.Enabled = False
        TasksDone = False
        While TaskList.Count > 0
            GenericRunCallBack(TaskList.Item(0).mType, TaskList.Item(0).Params)
            TaskList.RemoveAt(0)
        End While
        TasksDone = True
    End Sub

    Private Sub tmrSearchWait_Movies_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait_Movies.Tick
        tmrSearch_Movies.Enabled = False
        If prevTextSearch_Movies = currTextSearch_Movies Then
            tmrSearch_Movies.Enabled = True
        Else
            prevTextSearch_Movies = currTextSearch_Movies
        End If
    End Sub

    Private Sub tmrSearch_Movies_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch_Movies.Tick
        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        bDoingSearch_Movies = True

        If Not String.IsNullOrEmpty(txtSearchMovies.Text) Then
            FilterArray_Movies.Remove(filSearch_Movies)
            filSearch_Movies = String.Empty

            Select Case cbSearchMovies.Text
                Case Master.eLang.GetString(21, "Title")
                    filSearch_Movies = String.Concat("Title LIKE '%", txtSearchMovies.Text, "%'")
                    FilterArray_Movies.Add(filSearch_Movies)
                Case Master.eLang.GetString(302, "Original Title")
                    filSearch_Movies = String.Concat("OriginalTitle LIKE '%", txtSearchMovies.Text, "%'")
                    FilterArray_Movies.Add(filSearch_Movies)
                Case Master.eLang.GetString(100, "Actor")
                    filSearch_Movies = txtSearchMovies.Text
                Case Master.eLang.GetString(233, "Role")
                    filSearch_Movies = txtSearchMovies.Text
                Case Master.eLang.GetString(62, "Director")
                    filSearch_Movies = String.Concat("Director LIKE '%", txtSearchMovies.Text, "%'")
                    FilterArray_Movies.Add(filSearch_Movies)
                Case Master.eLang.GetString(729, "Credits")
                    filSearch_Movies = String.Concat("Credits LIKE '%", txtSearchMovies.Text, "%'")
                    FilterArray_Movies.Add(filSearch_Movies)
                Case Master.eLang.GetString(301, "Country")
                    filSearch_Movies = String.Concat("Country LIKE '%", txtSearchMovies.Text, "%'")
                    FilterArray_Movies.Add(filSearch_Movies)
                Case Master.eLang.GetString(395, "Studio")
                    filSearch_Movies = String.Concat("Studio LIKE '%", txtSearchMovies.Text, "%'")
                    FilterArray_Movies.Add(filSearch_Movies)
            End Select

            RunFilter_Movies(cbSearchMovies.Text = Master.eLang.GetString(100, "Actor") OrElse cbSearchMovies.Text = Master.eLang.GetString(233, "Role"))

        Else
            If Not String.IsNullOrEmpty(filSearch_Movies) Then
                FilterArray_Movies.Remove(filSearch_Movies)
                filSearch_Movies = String.Empty
            End If
            RunFilter_Movies(True)
        End If
    End Sub

    Private Sub tmrSearchWait_MovieSets_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait_MovieSets.Tick
        tmrSearch_MovieSets.Enabled = False
        If prevTextSearch_MovieSets = currTextSearch_MovieSets Then
            tmrSearch_MovieSets.Enabled = True
        Else
            prevTextSearch_MovieSets = currTextSearch_MovieSets
        End If
    End Sub

    Private Sub tmrSearch_MovieSets_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch_MovieSets.Tick
        tmrSearchWait_MovieSets.Enabled = False
        tmrSearch_MovieSets.Enabled = False
        bDoingSearch_MovieSets = True

        If Not String.IsNullOrEmpty(txtSearchMovieSets.Text) Then
            FilterArray_MovieSets.Remove(filSearch_MovieSets)
            filSearch_MovieSets = String.Empty

            Select Case cbSearchMovieSets.Text
                Case Master.eLang.GetString(21, "Title")
                    filSearch_MovieSets = String.Concat("SetName LIKE '%", txtSearchMovieSets.Text, "%'")
                    FilterArray_MovieSets.Add(filSearch_MovieSets)
            End Select

            RunFilter_MovieSets(False)

        Else
            If Not String.IsNullOrEmpty(filSearch_MovieSets) Then
                FilterArray_MovieSets.Remove(filSearch_MovieSets)
                filSearch_MovieSets = String.Empty
            End If
            RunFilter_MovieSets(True)
        End If
    End Sub

    Private Sub tmrSearchWait_Shows_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait_Shows.Tick
        tmrSearch_Shows.Enabled = False
        If prevTextSearch_TVShows = currTextSearch_TVShows Then
            tmrSearch_Shows.Enabled = True
        Else
            prevTextSearch_TVShows = currTextSearch_TVShows
        End If
    End Sub

    Private Sub tmrSearch_Shows_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch_Shows.Tick
        tmrSearchWait_Shows.Enabled = False
        tmrSearch_Shows.Enabled = False
        bDoingSearch_TVShows = True

        If Not String.IsNullOrEmpty(txtSearchShows.Text) Then
            FilterArray_TVShows.Remove(filSearch_TVShows)
            filSearch_TVShows = String.Empty

            Select Case cbSearchShows.Text
                Case Master.eLang.GetString(21, "Title")
                    filSearch_TVShows = String.Concat("Title LIKE '%", txtSearchShows.Text, "%'")
                    FilterArray_TVShows.Add(filSearch_TVShows)
            End Select

            RunFilter_Shows(False)

        Else
            If Not String.IsNullOrEmpty(filSearch_TVShows) Then
                FilterArray_TVShows.Remove(filSearch_TVShows)
                filSearch_TVShows = String.Empty
            End If
            RunFilter_Shows(True)
        End If
    End Sub

    Private Sub tmrWait_TVEpisode_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_TVEpisode.Tick
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVShow.Stop()

        If Not prevRow_TVEpisode = currRow_TVEpisode Then
            prevRow_TVEpisode = currRow_TVEpisode
            tmrWait_TVEpisode.Stop()
            tmrLoad_TVEpisode.Start()
        Else
            tmrLoad_TVEpisode.Stop()
            tmrWait_TVEpisode.Stop()
        End If
    End Sub

    Private Sub tmrWait_TVSeason_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_TVSeason.Tick
        tmrLoad_TVShow.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrWait_TVShow.Stop()
        tmrWait_TVEpisode.Stop()

        If Not prevRow_TVSeason = currRow_TVSeason Then
            prevRow_TVSeason = currRow_TVSeason
            tmrWait_TVSeason.Stop()
            tmrLoad_TVSeason.Start()
        Else
            tmrLoad_TVSeason.Stop()
            tmrWait_TVSeason.Stop()
        End If
    End Sub

    Private Sub tmrWait_TVShow_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_TVShow.Tick
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()

        If Not prevRow_TVShow = currRow_TVShow Then
            prevRow_TVShow = currRow_TVShow
            tmrWait_TVShow.Stop()
            tmrLoad_TVShow.Start()
        Else
            tmrLoad_TVShow.Stop()
            tmrWait_TVShow.Stop()
        End If
    End Sub

    Private Sub tmrWait_Movie_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_Movie.Tick
        If Not prevRow_Movie = currRow_Movie Then
            prevRow_Movie = currRow_Movie
            tmrWait_Movie.Stop()
            tmrLoad_Movie.Start()
        Else
            tmrLoad_Movie.Stop()
            tmrWait_Movie.Stop()
        End If
    End Sub

    Private Sub tmrWait_MovieSet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_MovieSet.Tick
        If Not prevRow_MovieSet = currRow_MovieSet Then
            prevRow_MovieSet = currRow_MovieSet
            tmrWait_MovieSet.Stop()
            tmrLoad_MovieSet.Start()
        Else
            tmrLoad_MovieSet.Stop()
            tmrWait_MovieSet.Stop()
        End If
    End Sub

    Private Sub mnuUpdate_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUpdate.ButtonClick
        LoadMedia(New Structures.ScanOrClean With {.Movies = True, .MovieSets = True, .TV = True})
    End Sub

    Private Sub txtFilterGenre_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterGenre_Movies.Click
        pnlFilterGenres_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterGenre_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterGenre_Movies.Top) - pnlFilterGenres_Movies.Height)
        pnlFilterGenres_Movies.Width = txtFilterGenre_Movies.Width
        If pnlFilterGenres_Movies.Visible Then
            pnlFilterGenres_Movies.Visible = False
        ElseIf Not pnlFilterGenres_Movies.Tag.ToString = "NO" Then
            pnlFilterGenres_Movies.Tag = String.Empty
            pnlFilterGenres_Movies.Visible = True
            clbFilterGenres_Movies.Focus()
        Else
            pnlFilterGenres_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterGenre_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterGenre_Shows.Click
        pnlFilterGenres_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterGenre_Shows.Left + 1,
                                                       (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterGenre_Shows.Top) - pnlFilterGenres_Shows.Height)
        pnlFilterGenres_Shows.Width = txtFilterGenre_Shows.Width
        If pnlFilterGenres_Shows.Visible Then
            pnlFilterGenres_Shows.Visible = False
        ElseIf Not pnlFilterGenres_Shows.Tag.ToString = "NO" Then
            pnlFilterGenres_Shows.Tag = String.Empty
            pnlFilterGenres_Shows.Visible = True
            clbFilterGenres_Shows.Focus()
        Else
            pnlFilterGenres_Shows.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterCountry_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterCountry_Movies.Click
        pnlFilterCountries_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterCountry_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterCountry_Movies.Top) - pnlFilterCountries_Movies.Height)
        pnlFilterCountries_Movies.Width = txtFilterCountry_Movies.Width
        If pnlFilterCountries_Movies.Visible Then
            pnlFilterCountries_Movies.Visible = False
        ElseIf Not pnlFilterCountries_Movies.Tag.ToString = "NO" Then
            pnlFilterCountries_Movies.Tag = String.Empty
            pnlFilterCountries_Movies.Visible = True
            clbFilterCountries_Movies.Focus()
        Else
            pnlFilterCountries_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterDataField_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterDataField_Movies.Click
        pnlFilterDataFields_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + gbFilterDataField_Movies.Left + tblFilterDataField_Movies.Left + txtFilterDataField_Movies.Left + 1,
                                                        (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + gbFilterDataField_Movies.Top + tblFilterDataField_Movies.Top + txtFilterDataField_Movies.Top) - pnlFilterDataFields_Movies.Height)
        pnlFilterDataFields_Movies.Width = txtFilterDataField_Movies.Width
        If pnlFilterDataFields_Movies.Visible Then
            pnlFilterDataFields_Movies.Visible = False
        ElseIf Not pnlFilterDataFields_Movies.Tag.ToString = "NO" Then
            pnlFilterDataFields_Movies.Tag = String.Empty
            pnlFilterDataFields_Movies.Visible = True
            clbFilterDataFields_Movies.Focus()
        Else
            pnlFilterDataFields_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub btnFilterMissing_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterMissing_Movies.Click
        pnlFilterMissingItems_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterGeneral_Movies.Left + tblFilterGeneral_Movies.Left + btnFilterMissing_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterGeneral_Movies.Top + tblFilterGeneral_Movies.Top + btnFilterMissing_Movies.Top) - pnlFilterMissingItems_Movies.Height)
        If pnlFilterMissingItems_Movies.Visible Then
            pnlFilterMissingItems_Movies.Visible = False
        Else
            pnlFilterMissingItems_Movies.Visible = True
        End If
    End Sub

    Private Sub btnFilterMissing_MovieSets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterMissing_MovieSets.Click
        pnlFilterMissingItems_MovieSets.Location = New Point(pnlFilter_MovieSets.Left + tblFilter_MovieSets.Left + gbFilterGeneral_MovieSets.Left + tblFilterGeneral_MovieSets.Left + btnFilterMissing_MovieSets.Left + 1,
                                                       (pnlFilter_MovieSets.Top + tblFilter_MovieSets.Top + gbFilterGeneral_MovieSets.Top + tblFilterGeneral_MovieSets.Top + btnFilterMissing_MovieSets.Top) - pnlFilterMissingItems_MovieSets.Height)
        If pnlFilterMissingItems_MovieSets.Visible Then
            pnlFilterMissingItems_MovieSets.Visible = False
        Else
            pnlFilterMissingItems_MovieSets.Visible = True
        End If
    End Sub

    Private Sub btnFilterMissing_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterMissing_Shows.Click
        pnlFilterMissingItems_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterGeneral_Shows.Left + tblFilterGeneral_Shows.Left + btnFilterMissing_Shows.Left + 1,
                                                       (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterGeneral_Shows.Top + tblFilterGeneral_Shows.Top + btnFilterMissing_Shows.Top) - pnlFilterMissingItems_Shows.Height)
        If pnlFilterMissingItems_Shows.Visible Then
            pnlFilterMissingItems_Shows.Visible = False
        Else
            pnlFilterMissingItems_Shows.Visible = True
        End If
    End Sub

    Private Sub txtFilterSource_Movies_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterSource_Movies.Click
        pnlFilterSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterSource_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterSource_Movies.Top) - pnlFilterSources_Movies.Height)
        pnlFilterSources_Movies.Width = txtFilterSource_Movies.Width
        If pnlFilterSources_Movies.Visible Then
            pnlFilterSources_Movies.Visible = False
        ElseIf Not pnlFilterSources_Movies.Tag.ToString = "NO" Then
            pnlFilterSources_Movies.Tag = String.Empty
            pnlFilterSources_Movies.Visible = True
            clbFilterSources_Movies.Focus()
        Else
            pnlFilterSources_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterSource_Shows_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterSource_Shows.Click
        pnlFilterSources_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterSource_Shows.Left + 1,
                                                       (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterSource_Shows.Top) - pnlFilterSources_Shows.Height)
        pnlFilterSources_Shows.Width = txtFilterSource_Shows.Width
        If pnlFilterSources_Shows.Visible Then
            pnlFilterSources_Shows.Visible = False
        ElseIf Not pnlFilterSources_Shows.Tag.ToString = "NO" Then
            pnlFilterSources_Shows.Tag = String.Empty
            pnlFilterSources_Shows.Visible = True
            clbFilterSource_Shows.Focus()
        Else
            pnlFilterSources_Shows.Tag = String.Empty
        End If
    End Sub

    Private Sub txtSearchMovies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchMovies.KeyPress
        e.Handled = Not StringUtils.isValidFilterChar(e.KeyChar)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvMovies.Focus()
        End If
    End Sub

    Private Sub txtSearchMovies_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchMovies.TextChanged
        currTextSearch_Movies = txtSearchMovies.Text

        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub txtSearchMovieSets_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchMovieSets.KeyPress
        e.Handled = Not StringUtils.isValidFilterChar(e.KeyChar)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvMovieSets.Focus()
        End If
    End Sub

    Private Sub txtSearchMovieSets_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchMovieSets.TextChanged
        currTextSearch_MovieSets = txtSearchMovieSets.Text

        tmrSearchWait_MovieSets.Enabled = False
        tmrSearch_MovieSets.Enabled = False
        tmrSearchWait_MovieSets.Enabled = True
    End Sub

    Private Sub txtSearchShows_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchShows.KeyPress
        e.Handled = Not StringUtils.isValidFilterChar(e.KeyChar)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvTVShows.Focus()
        End If
    End Sub

    Private Sub txtSearchShows_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchShows.TextChanged
        currTextSearch_TVShows = txtSearchShows.Text

        tmrSearchWait_Shows.Enabled = False
        tmrSearch_Shows.Enabled = False
        tmrSearchWait_Shows.Enabled = True
    End Sub

    Private Sub VersionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpVersions.Click
        ModulesManager.Instance.GetVersions()
    End Sub

    Private Sub mnuMainHelpWiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpWiki.Click
        Functions.Launch(My.Resources.urlEmberWiki)
    End Sub

    Private Sub mnuMainHelpForumEng_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpForumEng.Click
        Functions.Launch(My.Resources.urlForumEng)
    End Sub

    Private Sub mnuMainHelpForumGer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpForumGer.Click
        Functions.Launch(My.Resources.urlForumGer)
    End Sub

    Private Sub mnuVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVersion.Click
        Functions.Launch(My.Resources.urlReleaseThread)
    End Sub

    Private Sub tmrAppExit_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAppExit.Tick
        tmrAppExit.Enabled = False
        Close()
    End Sub

    Private Sub CheckUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpUpdate.Click
        If Functions.CheckNeedUpdate() Then
            Using dNewVer As New dlgNewVersion
                If dNewVer.ShowDialog() = DialogResult.Abort Then
                    tmrAppExit.Enabled = True
                    CloseApp = True
                End If
            End Using
        Else
            MessageBox.Show(Master.eLang.GetString(851, "No Updates at this time"), "Updates", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub lblIMDBHeader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblIMDBHeader.Click
        If Not String.IsNullOrEmpty(txtIMDBID.Text) Then
            Functions.Launch(String.Format("http://www.imdb.com/title/tt{0}/", txtIMDBID.Text))
        End If
    End Sub

    Private Sub lblIMDBHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblIMDBHeader.MouseEnter
        If Not String.IsNullOrEmpty(txtIMDBID.Text) Then
            lblIMDBHeader.Tag = lblIMDBHeader.ForeColor
            lblIMDBHeader.ForeColor = Color.FromArgb(Not lblIMDBHeader.ForeColor.R, Not lblIMDBHeader.ForeColor.G, Not lblIMDBHeader.ForeColor.B)
            Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblIMDBHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblIMDBHeader.MouseLeave
        If Not lblIMDBHeader.Tag Is Nothing Then
            lblIMDBHeader.ForeColor = DirectCast(lblIMDBHeader.Tag, Color)
            Cursor = Cursors.Default
            lblIMDBHeader.Tag = Nothing
        End If
    End Sub

    Private Sub lblTMDBHeader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTMDBHeader.Click
        If Not String.IsNullOrEmpty(txtTMDBID.Text) Then
            If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                Functions.Launch(My.Resources.urlTheMovieDb & "/movie/" & txtTMDBID.Text)
            Else
                Functions.Launch(My.Resources.urlTheMovieDb & "movie/" & txtTMDBID.Text)
            End If
        End If
    End Sub

    Private Sub lblTMDBHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTMDBHeader.MouseEnter
        If Not String.IsNullOrEmpty(txtTMDBID.Text) Then
            lblTMDBHeader.Tag = lblTMDBHeader.ForeColor
            lblTMDBHeader.ForeColor = Color.FromArgb(Not lblTMDBHeader.ForeColor.R, Not lblTMDBHeader.ForeColor.G, Not lblTMDBHeader.ForeColor.B)
            Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblTMDBHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTMDBHeader.MouseLeave
        If Not lblTMDBHeader.Tag Is Nothing Then
            lblTMDBHeader.ForeColor = DirectCast(lblTMDBHeader.Tag, Color)
            Cursor = Cursors.Default
            lblTMDBHeader.Tag = Nothing
        End If
    End Sub

    Private Sub pbStudio_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStudio.MouseEnter

        If Not clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) AndAlso Not String.IsNullOrEmpty(pbStudio.Tag.ToString) Then
            lblStudio.Text = pbStudio.Tag.ToString
        End If
    End Sub
    Private Sub pbStudio_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStudio.MouseLeave
        If Not clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then lblStudio.Text = String.Empty
    End Sub

    Private Sub tmrKeyBuffer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrKeyBuffer.Tick
        tmrKeyBuffer.Enabled = False
        KeyBuffer = String.Empty
    End Sub

    Private Sub bwCheckVersion_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCheckVersion.DoWork
        Try
            Dim sHTTP As New EmberAPI.HTTP
            'Pull Assembly version info from current Ember repo on github
            Dim HTML As String
            HTML = sHTTP.DownloadData("https://raw.github.com/DanCooper/Ember-MM-Newscraper/master/EmberMediaManager/My%20Project/AssemblyInfo.vb")
            sHTTP = Nothing
            Dim aBit As String = "x64"
            If Master.is32Bit Then
                aBit = "x86"
            End If
            Dim VersionNumber As String = System.String.Format(Master.eLang.GetString(865, "Version {0}.{1}.{2}.{3} {4}"), My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision, aBit)
            ' Not localized as is the Assembly file version
            Dim VersionNumberO As String = System.String.Format("{0}.{1}.{2}.{3}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
            If Not String.IsNullOrEmpty(HTML) Then
                'Example: AssemblyFileVersion("1.3.0.18")>
                Dim mc As MatchCollection = System.Text.RegularExpressions.Regex.Matches(HTML, "AssemblyFileVersion([^<]+)>")
                'check to see if at least one entry was found
                If mc.Count > 0 Then
                    'just use the first match if more are found and compare with running Ember Version
                    If mc(0).Value.ToString <> "AssemblyFileVersion(""" & VersionNumberO & """)>" Then
                        'means that running Ember version is outdated!
                        Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), System.String.Format(Master.eLang.GetString(1009, "{0} - (New version available!)"), VersionNumber), Color.DarkRed)
                    Else
                        'Ember already up to date!
                        Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), VersionNumber, Color.DarkGreen)
                    End If
                End If
                'if no github query possible, than simply display Ember version on form
            Else
                Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), VersionNumber, Color.DarkBlue)
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Delegate Sub UpdatemnuVersionDel(sText As String, sForeColor As Color)

    Private Sub UpdatemnuVersion(sText As String, sForeColor As Color)
        mnuVersion.Text = sText
        mnuVersion.ForeColor = sForeColor
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim ID As Long
        Dim IsTV As Boolean
        Dim DBElement As Database.DBElement
        Dim Path As String
        Dim pURL As String
        Dim ScrapeList As List(Of ScrapeItem)
        Dim ScrapeOptions As Structures.ScrapeOptions
        Dim ScrapeType As Enums.ScrapeType
        Dim Season As Integer
        Dim setEnabled As Boolean
        Dim SetName As String
        Dim TaskType As Enums.TaskManagerType
        Dim withEpisodes As Boolean
        Dim withSeasons As Boolean

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim doFill As Boolean
        Dim fileInfo As String
        Dim IsTV As Boolean
        Dim DBElement As Database.DBElement
        Dim MovieInSetPosters As List(Of MovieInSetPoster)
        Dim ScrapeOptions As Structures.ScrapeOptions
        Dim Path As String
        Dim Result As Image
        Dim ScrapeType As Enums.ScrapeType
        Dim setEnabled As Boolean
        Dim Cancelled As Boolean

#End Region 'Fields

    End Structure

    Class MovieInSetPoster

#Region "Fields"

        Private _movieposter As Image
        Private _movietitle As String
        Private _movieyear As String

#End Region 'Fields

#Region "Properties"

        Public Property MoviePoster() As Image
            Get
                Return _movieposter
            End Get
            Set(ByVal value As Image)
                _movieposter = value
            End Set
        End Property

        Public Property MovieTitle() As String
            Get
                Return _movietitle
            End Get
            Set(ByVal value As String)
                _movietitle = value
            End Set
        End Property

        Public Property MovieYear() As String
            Get
                Return _movieyear
            End Get
            Set(ByVal value As String)
                _movieyear = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub New()
            Clear()
        End Sub

        Public Sub Clear()
            _movieposter = Nothing
            _movietitle = String.Empty
            _movieyear = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Structure ScrapeItem

#Region "Fields"

        Dim DataRow As DataRow
        Dim ScrapeModifiers As Structures.ScrapeModifiers

#End Region 'Fields

    End Structure

    Structure Task

#Region "Fields"

        Dim mType As Enums.ModuleEventType
        Dim Params As List(Of Object)

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class