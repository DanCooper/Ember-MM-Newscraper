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

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports RestSharp
Imports WatTmdb
Imports NLog
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary


Public Class frmMain

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    'Private aaa As New V3.Tmdb("aa")
    Friend WithEvents bwCleanDB As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadPic As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadEpInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieSetInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadSeasonInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadShowInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMetaInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieSetInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieScraper As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieSetScraper As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwNonScrape As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwRefreshMovies As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwRefreshMovieSets As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwCheckVersion As New System.ComponentModel.BackgroundWorker

    Private alActors As New List(Of String)
    Private alMoviesInSet As New List(Of String)
    Private aniFilterRaise As Boolean = False
    Private aniRaise As Boolean = False
    Private aniMovieType As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private aniMovieSetType As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private aniShowType As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private bsEpisodes As New BindingSource

    Private bsMovies As New BindingSource
    Private bsMovieSets As New BindingSource
    Private bsSeasons As New BindingSource
    Private bsShows As New BindingSource

    Private dtEpisodes As New DataTable
    Private dtMovies As New DataTable
    Private dtMovieSets As New DataTable
    Private dtSeasons As New DataTable
    Private dtShows As New DataTable
    Private dScrapeRow As DataRow = Nothing

    Private fScanner As New Scanner
    Private GenreImage As Image
    Private InfoCleared As Boolean = False
    Private LoadingDone As Boolean = False
    Private MainAllSeason As New Images
    Private MainClearArt As New Images
    Private MainFanart As New Images
    Private MainPoster As New Images
    Private MainFanartSmall As New Images
    Private MainLandscape As New Images
    Private pbGenre() As PictureBox = Nothing
    Private pnlGenre() As Panel = Nothing
    Private prevText As String = String.Empty
    Private ReportDownloadPercent As Boolean = False
    Private ScrapeList As New List(Of DataRow)
    Private ScraperDone As Boolean = False
    Private sHTTP As New EmberAPI.HTTP
    Private tmpLang As String = String.Empty
    Private tmpTitle As String = String.Empty
    Private tmpTVDB As String = String.Empty
    Private tmpOrdering As Enums.Ordering = Enums.Ordering.Standard

    'Loading Delays
    Private currMovieRow As Integer = -1
    Private currMovieSetRow As Integer = -1
    Private currEpRow As Integer = -1
    Private currSeasonRow As Integer = -1
    Private currShowRow As Integer = -1
    Private currList As Integer = 0
    Private currText As String = String.Empty
    Private currThemeType As Theming.ThemeType
    Private prevEpRow As Integer = -1
    Private prevMovieRow As Integer = -1
    Private prevMovieSetRow As Integer = -1
    Private prevSeasonRow As Integer = -1
    Private prevShowRow As Integer = -1
    Private bDoingSearch As Boolean = False

    'filters
    Private FilterArray As New List(Of String)
    Private filSearch As String = String.Empty
    Private filSource As String = String.Empty
    Private filYear As String = String.Empty
    Private filGenre As String = String.Empty
    Private filMissing As String = String.Empty

    'Theme Information
    Private _clearartmaxheight As Integer = 160
    Private _clearartmaxwidth As Integer = 285
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

    Private oldStatus As String = String.Empty

    Private _ScraperStatus As New EmberAPI.clsXMLRestartScraper
    
    Private KeyBuffer As String = String.Empty
    ' Environment variables
#End Region 'Fields

#Region "Delegates"

    Delegate Sub DelegateEvent_Movie(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)

    Delegate Sub DelegateEvent_MovieSet(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)


    Delegate Sub MydtListUpdate(ByVal drow As DataRow, ByVal i As Integer, ByVal v As Object)

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
    Public Sub ClearInfo(Optional ByVal WithAllSeasons As Boolean = True)

        Try
            With Me
                .InfoCleared = True

                If .bwDownloadPic.IsBusy Then .bwDownloadPic.CancelAsync()
                If .bwLoadMovieInfo.IsBusy Then .bwLoadMovieInfo.CancelAsync()
                If .bwLoadMovieSetInfo.IsBusy Then .bwLoadMovieSetInfo.CancelAsync()
                If .bwLoadShowInfo.IsBusy Then .bwLoadShowInfo.CancelAsync()
                If .bwLoadSeasonInfo.IsBusy Then .bwLoadSeasonInfo.CancelAsync()
                If .bwLoadEpInfo.IsBusy Then .bwLoadEpInfo.CancelAsync()

                While .bwDownloadPic.IsBusy OrElse .bwLoadMovieInfo.IsBusy OrElse .bwLoadMovieSetInfo.IsBusy OrElse _
                    .bwLoadShowInfo.IsBusy OrElse .bwLoadSeasonInfo.IsBusy OrElse .bwLoadEpInfo.IsBusy
                    Application.DoEvents()
                    Threading.Thread.Sleep(50)
                End While

                If Not IsNothing(.pbFanart.Image) Then
                    .pbFanart.Image.Dispose()
                    .pbFanart.Image = Nothing
                End If
                .MainFanart.Clear()

                If Not IsNothing(.pbClearArt.Image) Then
                    .pbClearArt.Image.Dispose()
                    .pbClearArt.Image = Nothing
                End If
                .pnlClearArt.Visible = False
                .MainClearArt.Clear()

                If Not IsNothing(.pbPoster.Image) Then
                    .pbPoster.Image.Dispose()
                    .pbPoster.Image = Nothing
                End If
                .pnlPoster.Visible = False
                .MainPoster.Clear()

                If Not IsNothing(.pbFanartSmall.Image) Then
                    .pbFanartSmall.Image.Dispose()
                    .pbFanartSmall.Image = Nothing
                End If
                .pnlFanartSmall.Visible = False
                .MainFanartSmall.Clear()

                If Not IsNothing(.pbLandscape.Image) Then
                    .pbLandscape.Image.Dispose()
                    .pbLandscape.Image = Nothing
                End If
                .pnlLandscape.Visible = False
                .MainLandscape.Clear()

                If WithAllSeasons Then
                    If Not IsNothing(.pbAllSeason.Image) Then
                        .pbAllSeason.Image.Dispose()
                        .pbAllSeason.Image = Nothing
                    End If
                    .MainAllSeason.Clear()
                End If
                .pnlAllSeason.Visible = False

                'remove all the current genres
                Try
                    For iDel As Integer = UBound(.pnlGenre) To 0 Step -1
                        .scMain.Panel2.Controls.Remove(.pbGenre(iDel))
                        .scMain.Panel2.Controls.Remove(.pnlGenre(iDel))
                    Next
                Catch
                End Try

                If Not IsNothing(.pbMPAA.Image) Then
                    .pbMPAA.Image = Nothing
                End If
                .pnlMPAA.Visible = False

                .lblTitle.Text = String.Empty
                .lblOriginalTitle.Text = String.Empty
                .lblRating.Text = String.Empty
                .lblRuntime.Text = String.Empty
                .pnlTop250.Visible = False
                .lblTop250.Text = String.Empty
                .pbStar1.Image = Nothing
                .pbStar2.Image = Nothing
                .pbStar3.Image = Nothing
                .pbStar4.Image = Nothing
                .pbStar5.Image = Nothing
                .pbStar6.Image = Nothing
                .pbStar7.Image = Nothing
                .pbStar8.Image = Nothing
                .pbStar9.Image = Nothing
                .pbStar10.Image = Nothing
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

                .lstActors.Items.Clear()
                If Not IsNothing(.alActors) Then
                    .alActors.Clear()
                    .alActors = Nothing
                End If
                If Not IsNothing(.pbActors.Image) Then
                    .pbActors.Image.Dispose()
                    .pbActors.Image = Nothing
                End If
                .lblDirector.Text = String.Empty
                .lblReleaseDate.Text = String.Empty
                .txtCerts.Text = String.Empty
                .txtIMDBID.Text = String.Empty
                .txtFilePath.Text = String.Empty
                .txtOutline.Text = String.Empty
                .txtPlot.Text = String.Empty
                .lblTagline.Text = String.Empty
                If Not IsNothing(.pbMPAA.Image) Then
                    .pbMPAA.Image.Dispose()
                    .pbMPAA.Image = Nothing
                End If
                .pbStudio.Image = Nothing
                .pbVideo.Image = Nothing
                .pbVType.Image = Nothing
                .pbAudio.Image = Nothing
                .pbResolution.Image = Nothing
                .pbChannels.Image = Nothing

                .txtMetaData.Text = String.Empty
                .pnlTop.Visible = False
                '.tslStatus.Text = String.Empty

                .lstMoviesInSet.Items.Clear()
                If Not IsNothing(.alMoviesInSet) Then
                    .alMoviesInSet.Clear()
                    .alMoviesInSet = Nothing
                End If

                Application.DoEvents()
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub LoadMedia(ByVal Scan As Structures.Scans, Optional ByVal SourceName As String = "")
        Try
            Me.SetStatus(Master.eLang.GetString(116, "Performing Preliminary Tasks (Gathering Data)..."))
            Me.tspbLoading.ProgressBar.Style = ProgressBarStyle.Marquee
            Me.tspbLoading.Visible = True

            Application.DoEvents()

            Me.ClearInfo()
            Me.ClearFilters()
            Me.EnableFilters(False)

            Me.SetControlsEnabled(False)
            Me.tpMovies.Text = Master.eLang.GetString(36, "Movies")
            Me.tpMovieSets.Text = Master.eLang.GetString(366, "Sets")
            Me.tpTVShows.Text = Master.eLang.GetString(653, "TV")
            Me.txtSearch.Text = String.Empty

            Me.fScanner.CancelAndWait()

            If Scan.Movies Then
                Me.dgvMovies.DataSource = Nothing
            End If

            If Scan.MovieSets Then
                Me.dgvMovieSets.DataSource = Nothing
            End If

            If Scan.TV Then
                Me.dgvTVShows.DataSource = Nothing
                Me.dgvTVSeasons.DataSource = Nothing
                Me.dgvTVEpisodes.DataSource = Nothing
            End If

            Me.fScanner.Start(Scan, SourceName)

        Catch ex As Exception
            Me.LoadingDone = True
            Me.EnableFilters(True)
            Me.SetControlsEnabled(True)
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub SetMovieListItemAfterEdit(ByVal iID As Integer, ByVal iRow As Integer)
        Try
            Dim dRow = From drvRow In dtMovies.Rows Where Convert.ToInt32(DirectCast(drvRow, DataRow).Item(0)) = iID Select drvRow

            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT mark, SortTitle FROM movies WHERE id = ", iID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    SQLreader.Read()
                    DirectCast(dRow(0), DataRow).Item(11) = Convert.ToBoolean(SQLreader("mark"))
                    If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then DirectCast(dRow(0), DataRow).Item(47) = SQLreader("SortTitle").ToString
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub SetMovieSetListItemAfterEdit(ByVal iID As Integer, ByVal iRow As Integer)
        Try
            'Dim dRow = From drvRow In dtMovieSets.Rows Where Convert.ToInt32(DirectCast(drvRow, DataRow).Item(0)) = iSetName Select drvRow

            'Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            '    SQLcommand.CommandText = String.Concat("SELECT mark, SortTitle FROM movies WHERE id = ", iSetName, ";")
            '    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
            '        SQLreader.Read()
            '        DirectCast(dRow(0), DataRow).Item(11) = Convert.ToBoolean(SQLreader("mark"))
            '        If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then DirectCast(dRow(0), DataRow).Item(47) = SQLreader("SortTitle").ToString
            '    End Using
            'End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub SetShowListItemAfterEdit(ByVal iID As Integer, ByVal iRow As Integer)
        Try
            Dim dRow = From drvRow In dtShows.Rows Where Convert.ToInt32(DirectCast(drvRow, DataRow).Item(0)) = iID Select drvRow

            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT Ordering FROM TVShows WHERE id = ", iID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    SQLreader.Read()
                    DirectCast(dRow(0), DataRow).Item(23) = Convert.ToInt32(SQLreader("Ordering"))
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpAbout.Click
        Using dAbout As New dlgAbout
            dAbout.ShowDialog()
        End Using
    End Sub

    Private Sub cmnuMovieGenresAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresAdd.Click
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parGenre As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parGenre", DbType.String, 0, "Genre")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE movies SET Genre = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        If Not sRow.Cells(27).Value.ToString.Contains(Me.cmnuMovieGenresGenre.Text) Then
                            If Not String.IsNullOrEmpty(sRow.Cells(27).Value.ToString) Then
                                parGenre.Value = String.Format("{0} / {1}", sRow.Cells(27).Value, Me.cmnuMovieGenresGenre.Text).Trim
                            Else
                                parGenre.Value = Me.cmnuMovieGenresGenre.Text.Trim
                            End If
                            parID.Value = sRow.Cells(0).Value
                            SQLcommand.ExecuteNonQuery()
                        End If
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    Me.RefreshMovie(Convert.ToInt64(sRow.Cells(0).Value), True, False, True)
                Next
                SQLtransaction.Commit()
            End Using

            Me.LoadMovieInfo(Convert.ToInt32(Me.dgvMovies.Item(0, Me.dgvMovies.CurrentCell.RowIndex).Value), Me.dgvMovies.Item(1, Me.dgvMovies.CurrentCell.RowIndex).Value.ToString, True, False)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub mnuMainToolsExportMovies_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportMovies.Click
        Try
            Dim table As New DataTable
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "Select * from Movies;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    'Load the SqlDataReader object to the DataTable object as follows. 
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub mnuMainToolsExportTvShows_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportTvShows.Click
        Try
            Dim table As New DataTable
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "Select * from TVShows;"
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ApplyTheme(ByVal tType As Theming.ThemeType)
        Me.pnlInfoPanel.SuspendLayout()

        Me.currThemeType = tType

        tTheme.ApplyTheme(tType)

        Me.tmrAni.Stop()

        Select Case If(Me.tcMain.SelectedIndex = 0, aniMovieType, If(Me.tcMain.SelectedIndex = 1, aniMovieSetType, aniShowType))
            Case 1
                If Me.btnMid.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipmid
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = False
                    Me.btnDown.Enabled = True
                ElseIf Me.btnUp.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipup
                    If Me.tcMain.SelectedIndex = 0 Then
                        aniMovieType = 2
                    ElseIf Me.tcMain.SelectedIndex = 1 Then
                        aniMovieSetType = 2
                    Else
                        aniShowType = 2
                    End If
                    Me.btnUp.Enabled = False
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = True
                Else
                    Me.pnlInfoPanel.Height = 25
                    If Me.tcMain.SelectedIndex = 0 Then
                        aniMovieType = 0
                    ElseIf Me.tcMain.SelectedIndex = 1 Then
                        aniMovieSetType = 0
                    Else
                        aniShowType = 0
                    End If
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = False
                End If
            Case 2
                If Me.btnMid.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipmid
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = False
                    Me.btnDown.Enabled = True
                ElseIf Me.btnUp.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipup
                    If Me.tcMain.SelectedIndex = 0 Then
                        aniMovieType = 2
                    ElseIf Me.tcMain.SelectedIndex = 1 Then
                        aniMovieSetType = 2
                    Else
                        aniShowType = 2
                    End If
                    Me.btnUp.Enabled = False
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = True
                Else
                    Me.pnlInfoPanel.Height = 25
                    If Me.tcMain.SelectedIndex = 0 Then
                        aniMovieType = 0
                    ElseIf Me.tcMain.SelectedIndex = 1 Then
                        aniMovieSetType = 0
                    Else
                        aniShowType = 0
                    End If
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = False
                End If

            Case 3
                If Me.btnUp.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipup
                    Me.btnUp.Enabled = False
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = True
                ElseIf Me.btnMid.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipmid

                    If Me.tcMain.SelectedIndex = 0 Then
                        aniMovieType = 1
                    ElseIf Me.tcMain.SelectedIndex = 1 Then
                        aniMovieSetType = 1
                    Else
                        aniShowType = 1
                    End If

                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = False
                    Me.btnDown.Enabled = True
                Else
                    Me.pnlInfoPanel.Height = 25
                    If Me.tcMain.SelectedIndex = 0 Then
                        aniMovieType = 0
                    ElseIf Me.tcMain.SelectedIndex = 1 Then
                        aniMovieSetType = 0
                    Else
                        aniShowType = 0
                    End If
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = False
                End If
            Case Else
                Me.pnlInfoPanel.Height = 25
                If Me.tcMain.SelectedIndex = 0 Then
                    aniMovieType = 0
                ElseIf Me.tcMain.SelectedIndex = 1 Then
                    aniMovieSetType = 0
                Else
                    aniShowType = 0
                End If

                Me.btnUp.Enabled = True
                Me.btnMid.Enabled = True
                Me.btnDown.Enabled = False
        End Select

        Me.pbActLoad.Visible = False
        Me.pbActors.Image = My.Resources.actor_silhouette
        Me.pbMILoading.Visible = False

        Me.pnlInfoPanel.ResumeLayout()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancel.Visible = False
        lblCanceling.Visible = True
        prbCanceling.Visible = True

        If Me.bwMovieScraper.IsBusy Then Me.bwMovieScraper.CancelAsync()
        If Me.bwMovieSetScraper.IsBusy Then Me.bwMovieSetScraper.CancelAsync()
        If Me.bwRefreshMovies.IsBusy Then Me.bwRefreshMovies.CancelAsync()
        If Me.bwRefreshMovieSets.IsBusy Then Me.bwRefreshMovieSets.CancelAsync()
        If Me.bwNonScrape.IsBusy Then Me.bwNonScrape.CancelAsync()
        While Me.bwMovieScraper.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwRefreshMovieSets.IsBusy OrElse Me.bwNonScrape.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub btnClearFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters.Click
        Me.ClearFilters(True)
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        '//
        ' Begin animation to lower panel all the way down
        '\\

        Me.tcMain.Focus()
        If Me.tcMain.SelectedIndex = 0 Then
            Me.aniMovieType = 0
        ElseIf Me.tcMain.SelectedIndex = 1 Then
            Me.aniMovieSetType = 0
        Else
            Me.aniShowType = 0
        End If
        Me.aniRaise = False
        Me.tmrAni.Start()
    End Sub

    Private Sub btnFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDown.Click
        '//
        ' Begin animation to lower panel all the way down
        '\\
        Me.aniFilterRaise = False
        Me.tmrFilterAni.Start()
    End Sub

    Private Sub btnFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterUp.Click
        '//
        ' Begin animation to raise panel all the way up
        '\\
        Me.aniFilterRaise = True
        Me.tmrFilterAni.Start()
    End Sub

    Private Sub btnIMDBRating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIMDBRating.Click
        If Me.dgvMovies.RowCount > 0 Then
            If Me.btnIMDBRating.Tag.ToString = "DESC" Then
                Me.btnIMDBRating.Tag = "ASC"
                Me.btnIMDBRating.Image = My.Resources.desc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns(18), ComponentModel.ListSortDirection.Descending)
            Else
                Me.btnIMDBRating.Tag = "DESC"
                Me.btnIMDBRating.Image = My.Resources.asc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns(18), ComponentModel.ListSortDirection.Ascending)
            End If
        End If
    End Sub

    Private Sub btnMarkAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkAll.Click
        Try
            Dim MarkAll As Boolean = Not btnMarkAll.Text = Master.eLang.GetString(105, "Unmark All")
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    SQLcommand.CommandText = "UPDATE movies SET mark = (?);"
                    parMark.Value = MarkAll
                    SQLcommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using
            For Each drvRow As DataRow In dtMovies.Rows
                drvRow.Item(11) = MarkAll
            Next
            dgvMovies.Refresh()
            btnMarkAll.Text = If(Not MarkAll, Master.eLang.GetString(35, "Mark All"), Master.eLang.GetString(105, "Unmark All"))
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMid.Click
        '//
        ' Begin animation to raise/lower panel to mid point
        '\\

        Me.tcMain.Focus()
        If Me.pnlInfoPanel.Height = Me.IPUp Then
            Me.aniRaise = False
        Else
            Me.aniRaise = True
        End If

        If Me.tcMain.SelectedIndex = 0 Then
            Me.aniMovieType = 1
        ElseIf Me.tcMain.SelectedIndex = 1 Then
            Me.aniMovieSetType = 1
        Else
            Me.aniShowType = 1
        End If

        Me.tmrAni.Start()
    End Sub

    Private Sub btnMIRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMetaDataRefresh.Click
        '//
        ' Refresh Media Info
        '\\

        If Me.tcMain.SelectedIndex = 0 Then
            If Not String.IsNullOrEmpty(Master.currMovie.Filename) AndAlso Me.dgvMovies.SelectedRows.Count > 0 Then
                Me.LoadMovieInfo(Convert.ToInt32(Master.currMovie.ID), Master.currMovie.Filename, False, True, True)
            End If
        ElseIf Me.tcMain.SelectedIndex = 1 Then
            'no NFO support for MovieSets
        ElseIf Not String.IsNullOrEmpty(Master.currShow.Filename) AndAlso Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
            Me.SetControlsEnabled(False, True)

            If Me.bwMetaInfo.IsBusy Then Me.bwMetaInfo.CancelAsync()

            Me.txtMetaData.Clear()
            Me.pbMILoading.Visible = True

            Me.bwMetaInfo = New System.ComponentModel.BackgroundWorker
            Me.bwMetaInfo.WorkerSupportsCancellation = True
            Me.bwMetaInfo.RunWorkerAsync(New Arguments With {.TVShow = Master.currShow, .IsTV = True, .setEnabled = True})
        End If
    End Sub
    ''' <summary>
    ''' Launch video using system default player
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click
        Functions.Launch(Me.txtFilePath.Text, True)
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
        '    logger.Error(New StackFrame().GetMethod().Name, ex)
        'End Try
    End Sub

    Private Sub btnSortDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSortDate.Click
        If Me.dgvMovies.RowCount > 0 Then
            If Me.btnSortDate.Tag.ToString = "DESC" Then
                Me.btnSortDate.Tag = "ASC"
                Me.btnSortDate.Image = My.Resources.desc
                'cotocus 201303 Wrong Column! DateAdd column is 48 instead of 0 (ID)!
                Me.dgvMovies.Sort(Me.dgvMovies.Columns(48), ComponentModel.ListSortDirection.Descending)
            Else
                Me.btnSortDate.Tag = "DESC"
                Me.btnSortDate.Image = My.Resources.asc
                'cotocus 201303 Wrong Column! DateAdd column is 48 instead of 0 (ID)!
                Me.dgvMovies.Sort(Me.dgvMovies.Columns(48), ComponentModel.ListSortDirection.Ascending)
            End If
        End If
    End Sub

    Private Sub btnSortTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSortTitle.Click
        If Me.dgvMovies.RowCount > 0 Then
            If Me.btnSortTitle.Tag.ToString = "DESC" Then
                Me.btnSortTitle.Tag = "ASC"
                Me.btnSortTitle.Image = My.Resources.desc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns(47), ComponentModel.ListSortDirection.Descending)
            Else
                Me.btnSortTitle.Tag = "DESC"
                Me.btnSortTitle.Image = My.Resources.asc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns(47), ComponentModel.ListSortDirection.Ascending)
            End If
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        '//
        ' Begin animation to raise panel all the way up
        '\\

        Me.tcMain.Focus()
        If Me.tcMain.SelectedIndex = 0 Then
            Me.aniMovieType = 2
        ElseIf Me.tcMain.SelectedIndex = 1 Then
            Me.aniMovieSetType = 2
        Else
            Me.aniShowType = 2
        End If
        Me.aniRaise = True
        Me.tmrAni.Start()
    End Sub

    Private Sub BuildStars(ByVal sinRating As Single)
        '//
        ' Convert # rating to star images
        '\\

        Try
            With Me
                .pbStar1.Image = Nothing
                .pbStar2.Image = Nothing
                .pbStar3.Image = Nothing
                .pbStar4.Image = Nothing
                .pbStar5.Image = Nothing
                .pbStar6.Image = Nothing
                .pbStar7.Image = Nothing
                .pbStar8.Image = Nothing
                .pbStar9.Image = Nothing
                .pbStar10.Image = Nothing

                Dim tTip As String = String.Concat(Master.eLang.GetString(245, "Rating:"), String.Format(" {0:N}", sinRating))
                ToolTips.SetToolTip(.pbStar1, tTip)
                ToolTips.SetToolTip(.pbStar2, tTip)
                ToolTips.SetToolTip(.pbStar3, tTip)
                ToolTips.SetToolTip(.pbStar4, tTip)
                ToolTips.SetToolTip(.pbStar5, tTip)
                ToolTips.SetToolTip(.pbStar6, tTip)
                ToolTips.SetToolTip(.pbStar7, tTip)
                ToolTips.SetToolTip(.pbStar8, tTip)
                ToolTips.SetToolTip(.pbStar9, tTip)
                ToolTips.SetToolTip(.pbStar10, tTip)

                If sinRating >= 0.5 Then ' if rating is less than .5 out of ten, consider it a 0
                    Select Case (sinRating)
                        Case Is <= 0.5
                            .pbStar1.Image = My.Resources.starhalf
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starhalf
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starhalf
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starhalf
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starhalf
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starhalf
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starhalf
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starhalf
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starhalf
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starhalf
                        Case Else
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.star
                    End Select
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwCleanDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCleanDB.DoWork
        Master.DB.Clean(True, True)
    End Sub

    Private Sub bwCleanDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCleanDB.RunWorkerCompleted
        Me.SetStatus(String.Empty)
        Me.tspbLoading.Visible = False

        Me.FillList(True, True, True)
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try

            sHTTP.StartDownloadImage(Args.pURL)

            While sHTTP.IsDownloading
                Application.DoEvents()
                If Me.bwDownloadPic.CancellationPending Then
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

        Me.pbActLoad.Visible = False

        If e.Cancelled Then
            Me.pbActors.Image = My.Resources.actor_silhouette
        Else
            Dim Res As Results = DirectCast(e.Result, Results)

            If Not IsNothing(Res.Result) Then
                Me.pbActors.Image = Res.Result
            Else
                Me.pbActors.Image = My.Resources.actor_silhouette
            End If
        End If
    End Sub

    Private Sub bwLoadEpInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadEpInfo.DoWork
        Try

            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Me.MainClearArt.Clear()
            Me.MainPoster.Clear()
            Me.MainFanart.Clear()
            Me.MainFanartSmall.Clear()
            Me.MainLandscape.Clear()

            If bwLoadEpInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            Master.currShow = Master.DB.LoadTVEpFromDB(Args.ID, True)

            If bwLoadEpInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster.FromFile(Master.currShow.EpPosterPath)
            If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall.FromFile(Master.currShow.EpFanartPath)

            If bwLoadEpInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideFanart Then
                Dim NeedsGS As Boolean = False
                If Not String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then
                    Me.MainFanart.FromFile(Master.currShow.EpFanartPath)
                Else
                    Me.MainFanart.FromFile(Master.currShow.ShowFanartPath)
                    NeedsGS = True
                End If

                If Not IsNothing(Me.MainFanart.Image) Then
                    If String.IsNullOrEmpty(Master.currShow.Filename) Then
                        Me.MainFanart = ImageUtils.AddMissingStamp(Me.MainFanart)
                    ElseIf NeedsGS Then
                        Me.MainFanart = ImageUtils.GrayScale(Me.MainFanart)
                    End If
                End If
            End If

            'wait for mediainfo to update the nfo
            While bwMetaInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If bwLoadEpInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadEpInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadEpInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.fillScreenInfoWithEpisode()
            Else
                Me.SetControlsEnabled(True)
            End If

            Me.dgvTVEpisodes.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadMovieInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieInfo.DoWork
        Try

            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Me.MainClearArt.Clear()
            Me.MainFanart.Clear()
            Me.MainPoster.Clear()
            Me.MainFanartSmall.Clear()
            Me.MainLandscape.Clear()

            If bwLoadMovieInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            Master.currMovie = Master.DB.LoadMovieFromDB(Args.ID)

            If bwLoadMovieInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideFanart Then Me.MainFanart.FromFile(Master.currMovie.FanartPath)

            If bwLoadMovieInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideClearArt Then Me.MainClearArt.FromFile(Master.currMovie.ClearArtPath)
            If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster.FromFile(Master.currMovie.PosterPath)
            If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall.FromFile(Master.currMovie.FanartPath)
            If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape.FromFile(Master.currMovie.LandscapePath)
            'read nfo if it's there

            'wait for mediainfo to update the nfo
            While bwMetaInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If bwLoadMovieInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadMovieInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.fillScreenInfoWithMovie()
            Else
                If Not bwMovieScraper.IsBusy AndAlso Not bwRefreshMovies.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not bwNonScrape.IsBusy Then
                    Me.SetControlsEnabled(True)
                    Me.EnableFilters(True)
                Else
                    Me.dgvMovies.Enabled = True
                End If
            End If
            Me.dgvMovies.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadMovieSetInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieSetInfo.DoWork
        Try

            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Me.MainClearArt.Clear()
            Me.MainFanart.Clear()
            Me.MainPoster.Clear()
            Me.MainFanartSmall.Clear()
            Me.MainLandscape.Clear()

            If bwLoadMovieSetInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            Master.currMovieSet = Master.DB.LoadMovieSetFromDB(Args.ID)

            If bwLoadMovieSetInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideFanart Then Me.MainFanart.FromFile(Master.currMovieSet.FanartPath)

            If bwLoadMovieSetInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideClearArt Then Me.MainClearArt.FromFile(Master.currMovieSet.ClearArtPath)
            If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster.FromFile(Master.currMovieSet.PosterPath)
            If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall.FromFile(Master.currMovieSet.FanartPath)
            If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape.FromFile(Master.currMovieSet.LandscapePath)
            'read nfo if it's there

            'wait for mediainfo to update the nfo
            While bwMovieSetInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If bwLoadMovieSetInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadMovieSetInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieSetInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.fillScreenInfoWithMovieSet()
            Else
                If Not bwMovieScraper.IsBusy AndAlso Not bwRefreshMovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not bwNonScrape.IsBusy Then
                    Me.SetControlsEnabled(True)
                    Me.EnableFilters(True)
                Else
                    Me.dgvMovieSets.Enabled = True
                End If
            End If
            Me.dgvMovieSets.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadSeasonInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadSeasonInfo.DoWork
        Try

            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Me.MainClearArt.Clear()
            Me.MainPoster.Clear()
            Me.MainFanart.Clear()
            Me.MainFanartSmall.Clear()
            Me.MainLandscape.Clear()

            Master.currShow = Master.DB.LoadTVSeasonFromDB(Args.ID, Args.Season, True)

            If bwLoadSeasonInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster.FromFile(Master.currShow.SeasonPosterPath)
            If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall.FromFile(Master.currShow.SeasonFanartPath)
            If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape.FromFile(Master.currShow.SeasonLandscapePath)

            If bwLoadSeasonInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideFanart Then
                If Not String.IsNullOrEmpty(Master.currShow.SeasonFanartPath) Then
                    Me.MainFanart.FromFile(Master.currShow.SeasonFanartPath)
                Else
                    Me.MainFanart.FromFile(Master.currShow.ShowFanartPath)
                    If Not IsNothing(Me.MainFanart.Image) Then Me.MainFanart = ImageUtils.GrayScale(Me.MainFanart)
                End If
            End If

            If bwLoadSeasonInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadSeasonInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadSeasonInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.fillScreenInfoWithSeason()
            Else
                Me.SetControlsEnabled(True)
            End If
            Me.dgvTVSeasons.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadShowInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadShowInfo.DoWork
        Try

            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Me.MainClearArt.Clear()
            Me.MainFanart.Clear()
            Me.MainPoster.Clear()
            Me.MainFanartSmall.Clear()
            Me.MainLandscape.Clear()

            If bwLoadShowInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            Master.currShow = Master.DB.LoadTVFullShowFromDB(Args.ID)

            If bwLoadShowInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideFanart Then Me.MainFanart.FromFile(Master.currShow.ShowFanartPath)

            If bwLoadShowInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

            If Not Master.eSettings.GeneralHideClearArt Then Me.MainClearArt.FromFile(Master.currShow.ShowClearArtPath)
            If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster.FromFile(Master.currShow.ShowPosterPath)
            If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall.FromFile(Master.currShow.ShowFanartPath)
            If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape.FromFile(Master.currShow.ShowLandscapePath)

            If Master.eSettings.TVGeneralDisplayASPoster AndAlso Master.eSettings.TVASPosterAnyEnabled Then
                Me.MainAllSeason.FromFile(Master.currShow.SeasonPosterPath)
            End If

            If bwLoadShowInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadShowInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadShowInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.fillScreenInfoWithShow()
            Else
                Me.SetControlsEnabled(True)
            End If
            Me.dgvTVShows.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwMetaInfo_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMetaInfo.DoWork
        '//
        ' Thread to procure technical and tag information about media via MediaInfo.dll
        '\\
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Try
            If Args.IsTV Then
                MediaInfo.UpdateTVMediaInfo(Args.TVShow)
                Master.DB.SaveTVEpToDB(Args.TVShow, False, False, False, True)
                e.Result = New Results With {.fileinfo = NFO.FIToString(Args.TVShow.TVEp.FileInfo, True), .TVShow = Args.TVShow, .IsTV = True, .setEnabled = Args.setEnabled}
            Else
                MediaInfo.UpdateMediaInfo(Args.Movie)
                Master.DB.SaveMovieToDB(Args.Movie, False, False, True)
                e.Result = New Results With {.fileinfo = NFO.FIToString(Args.Movie.Movie.FileInfo, False), .setEnabled = Args.setEnabled, .Path = Args.Path, .Movie = Args.Movie}
            End If

            If Me.bwMetaInfo.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Result = New Results With {.fileinfo = "error", .setEnabled = Args.setEnabled}
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwMetaInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMetaInfo.RunWorkerCompleted
        '//
        ' Thread finished: fill textbox with result
        '\\

        If Not e.Cancelled Then
            Dim Res As Results = DirectCast(e.Result, Results)

            Try
                If Not Res.fileInfo = "error" Then
                    Me.pbMILoading.Visible = False
                    Me.txtMetaData.Text = Res.fileInfo

                    If Res.IsTV Then
                        If Master.eSettings.TVScraperMetaDataScan Then
                            Me.SetAVImages(APIXML.GetAVImages(Res.TVShow.TVEp.FileInfo, Res.TVShow.Filename, True, ""))
                            Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                            Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
                        Else
                            Me.pnlInfoIcons.Width = pbStudio.Width + 1
                            Me.pbStudio.Left = 0
                        End If
                    Else
                        If Master.eSettings.MovieScraperMetaDataScan Then
                            Me.SetAVImages(APIXML.GetAVImages(Res.Movie.Movie.FileInfo, Res.Movie.Filename, False, Res.Movie.FileSource))
                            Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                            Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
                        Else
                            Me.pnlInfoIcons.Width = pbStudio.Width + 1
                            Me.pbStudio.Left = 0
                        End If

                        If Master.eSettings.MovieScraperUseMDDuration Then
                            If Not String.IsNullOrEmpty(Res.Movie.Movie.Runtime) Then
                                Me.lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), Res.Movie.Movie.Runtime)
                            End If
                        End If
                    End If
                    Me.btnMetaDataRefresh.Focus()
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            If Res.setEnabled Then
                Me.tcMain.Enabled = True
                Me.mnuUpdate.Enabled = True
                Me.cmnuTrayUpdate.Enabled = True
                If (Me.tcMain.SelectedIndex = 0 AndAlso Me.dgvMovies.RowCount > 0) OrElse _
                    (Me.tcMain.SelectedIndex = 1 AndAlso Me.dgvMovieSets.RowCount > 0) OrElse _
                    (Me.tcMain.SelectedIndex = 2 AndAlso Me.dgvTVShows.RowCount > 0) Then
                    Me.SetControlsEnabled(True)
                End If
            End If
        End If
    End Sub

    Private Sub bwMovieScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMovieScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            Me.ScraperDone = True
        End If

        If Res.scrapeType = Enums.ScrapeType.SingleScrape Then
            Me.MovieInfoDownloaded()
        Else
            If Me.dgvMovies.SelectedRows.Count > 0 Then
                Me.SelectMovieRow(Me.dgvMovies.SelectedRows(0).Index)
            Else
                Me.ClearInfo()
            End If
            Me.RefreshAllMovieSets()
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwMovieScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim OldTitle As String = String.Empty
        Dim NewTitle As String = String.Empty
        Dim Banner As New MediaContainers.Image
        Dim ClearArt As New MediaContainers.Image
        Dim ClearLogo As New MediaContainers.Image
        Dim DiscArt As New MediaContainers.Image
        Dim Fanart As New MediaContainers.Image
        Dim Landscape As New MediaContainers.Image
        Dim Poster As New MediaContainers.Image
        Dim Theme As New MediaContainers.Theme
        Dim Trailer As New MediaContainers.Trailer
        Dim tURL As String = String.Empty
        Dim aList As New List(Of MediaContainers.Image)
        Dim aUrlList As New List(Of Trailers)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)
        Dim tUrlList As New List(Of Themes)
        Dim DBScrapeMovie As New Structures.DBMovie
        Dim configpath As String = ""
        Dim formatter As New BinaryFormatter()

        logger.Trace("Starting MOVIE scrape")

        AddHandler ModulesManager.Instance.ScraperEvent_Movie, AddressOf MovieScraperEvent

        For Each dRow As DataRow In ScrapeList
            Try
                If bwMovieScraper.CancellationPending Then Exit For
                OldTitle = dRow.Item(3).ToString
                bwMovieScraper.ReportProgress(1, OldTitle)

                dScrapeRow = dRow

                DBScrapeMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(dRow.Item(0)))
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEditMovie, Nothing, DBScrapeMovie)

                If Master.GlobalScrapeMod.NFO Then
                    If ModulesManager.Instance.ScrapeData_Movie(DBScrapeMovie, Args.scrapeType, Args.Options) Then
                        Exit Try
                    End If
                Else
                    ' if we do not have the movie ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                    If String.IsNullOrEmpty(DBScrapeMovie.Movie.ID) AndAlso (Master.GlobalScrapeMod.ActorThumbs Or Master.GlobalScrapeMod.Banner Or Master.GlobalScrapeMod.ClearArt Or _
                                                                             Master.GlobalScrapeMod.ClearLogo Or Master.GlobalScrapeMod.DiscArt Or Master.GlobalScrapeMod.EFanarts Or _
                                                                             Master.GlobalScrapeMod.EThumbs Or Master.GlobalScrapeMod.Fanart Or Master.GlobalScrapeMod.Landscape Or _
                                                                             Master.GlobalScrapeMod.Poster Or Master.GlobalScrapeMod.Trailer) Then
                        Dim tOpt As New Structures.ScrapeOptions_Movie 'all false value not to override any field
                        If ModulesManager.Instance.ScrapeData_Movie(DBScrapeMovie, Args.scrapeType, tOpt) Then
                            Exit For
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                If Master.eSettings.MovieScraperMetaDataScan AndAlso Master.GlobalScrapeMod.Meta Then
                    MediaInfo.UpdateMediaInfo(DBScrapeMovie)
                End If
                If bwMovieScraper.CancellationPending Then Exit For

                If Not Args.scrapeType = Enums.ScrapeType.SingleScrape Then
                    MovieScraperEvent(Enums.ScraperEventType_Movie.NFOItem, True)
                End If

                NewTitle = DBScrapeMovie.ListTitle

                If Not NewTitle = OldTitle Then
                    bwMovieScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldTitle, NewTitle))
                End If

                If Not Args.scrapeType = Enums.ScrapeType.SingleScrape Then
                    MovieScraperEvent(Enums.ScraperEventType_Movie.ListTitle, NewTitle)
                    MovieScraperEvent(Enums.ScraperEventType_Movie.SortTitle, DBScrapeMovie.Movie.SortTitle)
                End If

                '-----

                'Poster
                If Master.GlobalScrapeMod.Poster Then
                    Poster.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If Poster.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.Poster) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.Poster, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) AndAlso Images.GetPreferredMoviePoster(aList, Poster) Then
                                If Not String.IsNullOrEmpty(Poster.URL) AndAlso IsNothing(Poster.WebImage.Image) Then
                                    Poster.WebImage.FromWeb(Poster.URL)
                                End If
                                If Not IsNothing(Poster.WebImage.Image) Then
                                    tURL = Poster.WebImage.SaveAsMoviePoster(DBScrapeMovie)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovie.PosterPath = tURL
                                        MovieScraperEvent(Enums.ScraperEventType_Movie.PosterItem, True)
                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                        'End If
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(928, "A poster of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovie, Enums.MovieImageType.Poster, aList, etList, efList) = DialogResult.OK Then
                                            Poster = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Poster.URL) AndAlso IsNothing(Poster.WebImage.Image) Then
                                                    Poster.WebImage.FromWeb(Poster.URL)
                                                End If
                                                If Not IsNothing(Poster.WebImage.Image) Then
                                                    tURL = Poster.WebImage.SaveAsMoviePoster(DBScrapeMovie)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovie.PosterPath = tURL
                                                        MovieScraperEvent(Enums.ScraperEventType_Movie.PosterItem, True)
                                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                                        'End If
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovie.PosterPath = ":" & Poster.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Fanart
                If Master.GlobalScrapeMod.Fanart Then
                    Fanart.Clear()
                    aList.Clear()
                    efList.Clear()
                    etList.Clear()
                    tURL = String.Empty
                    If Fanart.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.Fanart) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.Fanart, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) AndAlso Images.GetPreferredMovieFanart(aList, Fanart) Then
                                If Not String.IsNullOrEmpty(Fanart.URL) AndAlso IsNothing(Fanart.WebImage.Image) Then
                                    Fanart.WebImage.FromWeb(Fanart.URL)
                                End If
                                If Not IsNothing(Fanart.WebImage.Image) Then
                                    tURL = Fanart.WebImage.SaveAsMovieFanart(DBScrapeMovie)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovie.FanartPath = tURL
                                        MovieScraperEvent(Enums.ScraperEventType_Movie.FanartItem, True)
                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                        '    DBScrapeMovie.Movie.Fanart = fResults.Fanart
                                        'End If
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(927, "Fanart of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size:"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovie, Enums.MovieImageType.Fanart, aList, efList, etList) = DialogResult.OK Then
                                            Fanart = dImgSelect.Results
                                            efList = dImgSelect.efList
                                            etList = dImgSelect.etList
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Fanart.URL) AndAlso IsNothing(Fanart.WebImage.Image) Then
                                                    Fanart.WebImage.FromWeb(Fanart.URL)
                                                End If
                                                If Not IsNothing(Fanart.WebImage.Image) Then
                                                    tURL = Fanart.WebImage.SaveAsMovieFanart(DBScrapeMovie)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovie.FanartPath = tURL
                                                        MovieScraperEvent(Enums.ScraperEventType_Movie.FanartItem, True)
                                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                                        '    DBScrapeMovie.Movie.Fanart = fResults.Fanart
                                                        'End If
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovie.FanartPath = ":" & Fanart.URL
                                                DBScrapeMovie.efList = efList
                                                DBScrapeMovie.etList = etList
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Banner
                If Master.GlobalScrapeMod.Banner Then
                    Banner.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If Banner.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.Banner) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.Banner, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then 'AndAlso Images.GetPreferredPoster(aList, Banner) Then 'TODO: Check if we need PreferredBanner
                                If aList.Count > 0 Then Banner = aList.Item(0)
                                If Not String.IsNullOrEmpty(Banner.URL) AndAlso IsNothing(Banner.WebImage.Image) Then
                                    Banner.WebImage.FromWeb(Banner.URL)
                                End If
                                If Not IsNothing(Banner.WebImage.Image) Then
                                    tURL = Banner.WebImage.SaveAsMovieBanner(DBScrapeMovie)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovie.BannerPath = tURL
                                        MovieScraperEvent(Enums.ScraperEventType_Movie.BannerItem, True)
                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                        'End If
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1062, "A banner of your preferred type could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovie, Enums.MovieImageType.Banner, aList, etList, efList) = DialogResult.OK Then
                                            Banner = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Banner.URL) AndAlso IsNothing(Banner.WebImage.Image) Then
                                                    Banner.WebImage.FromWeb(Banner.URL)
                                                End If
                                                If Not IsNothing(Banner.WebImage.Image) Then
                                                    tURL = Banner.WebImage.SaveAsMovieBanner(DBScrapeMovie)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovie.BannerPath = tURL
                                                        MovieScraperEvent(Enums.ScraperEventType_Movie.BannerItem, True)
                                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                                        'End If
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovie.BannerPath = ":" & Banner.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Landscape
                If Master.GlobalScrapeMod.Landscape Then
                    Landscape.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If Landscape.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.Landscape) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.Landscape, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then 'AndAlso Images.GetPreferredPoster(aList, Landscape) Then
                                If aList.Count > 0 Then Landscape = aList.Item(0)
                                If Not String.IsNullOrEmpty(Landscape.URL) AndAlso IsNothing(Landscape.WebImage.Image) Then
                                    Landscape.WebImage.FromWeb(Landscape.URL)
                                End If
                                If Not IsNothing(Landscape.WebImage.Image) Then
                                    tURL = Landscape.WebImage.SaveAsMovieLandscape(DBScrapeMovie)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovie.LandscapePath = tURL
                                        MovieScraperEvent(Enums.ScraperEventType_Movie.LandscapeItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1063, "A landscape of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovie, Enums.MovieImageType.Landscape, aList, etList, efList) = DialogResult.OK Then
                                            Landscape = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Landscape.URL) AndAlso IsNothing(Landscape.WebImage.Image) Then
                                                    Landscape.WebImage.FromWeb(Landscape.URL)
                                                End If
                                                If Not IsNothing(Landscape.WebImage.Image) Then
                                                    tURL = Landscape.WebImage.SaveAsMovieLandscape(DBScrapeMovie)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovie.LandscapePath = tURL
                                                        MovieScraperEvent(Enums.ScraperEventType_Movie.LandscapeItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovie.LandscapePath = ":" & Landscape.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'ClearArt
                If Master.GlobalScrapeMod.ClearArt Then
                    ClearArt.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If ClearArt.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.ClearArt) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.ClearArt, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then ' AndAlso Images.GetPreferredPoster(aList, ClearArt) Then
                                If aList.Count > 0 Then ClearArt = aList.Item(0)
                                If Not String.IsNullOrEmpty(ClearArt.URL) AndAlso IsNothing(ClearArt.WebImage.Image) Then
                                    ClearArt.WebImage.FromWeb(ClearArt.URL)
                                End If
                                If Not IsNothing(ClearArt.WebImage.Image) Then
                                    tURL = ClearArt.WebImage.SaveAsMovieClearArt(DBScrapeMovie)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovie.ClearArtPath = tURL
                                        MovieScraperEvent(Enums.ScraperEventType_Movie.ClearArtItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1106, "A ClearArt of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovie, Enums.MovieImageType.ClearArt, aList, etList, efList) = DialogResult.OK Then
                                            ClearArt = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(ClearArt.URL) AndAlso IsNothing(ClearArt.WebImage.Image) Then
                                                    ClearArt.WebImage.FromWeb(ClearArt.URL)
                                                End If
                                                If Not IsNothing(ClearArt.WebImage.Image) Then
                                                    tURL = ClearArt.WebImage.SaveAsMovieLandscape(DBScrapeMovie)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovie.ClearArtPath = tURL
                                                        MovieScraperEvent(Enums.ScraperEventType_Movie.ClearArtItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovie.ClearArtPath = ":" & ClearArt.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'ClearLogo
                If Master.GlobalScrapeMod.ClearLogo Then
                    ClearLogo.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If ClearLogo.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.ClearLogo) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.ClearLogo, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then ' AndAlso Images.GetPreferredPoster(aList, ClearLogo) Then
                                If aList.Count > 0 Then ClearLogo = aList.Item(0)
                                If Not String.IsNullOrEmpty(ClearLogo.URL) AndAlso IsNothing(ClearLogo.WebImage.Image) Then
                                    ClearLogo.WebImage.FromWeb(ClearLogo.URL)
                                End If
                                If Not IsNothing(ClearLogo.WebImage.Image) Then
                                    tURL = ClearLogo.WebImage.SaveAsMovieClearLogo(DBScrapeMovie)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovie.ClearLogoPath = tURL
                                        MovieScraperEvent(Enums.ScraperEventType_Movie.ClearLogoItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1107, "A ClearLogo of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovie, Enums.MovieImageType.ClearLogo, aList, etList, efList) = DialogResult.OK Then
                                            ClearLogo = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(ClearLogo.URL) AndAlso IsNothing(ClearLogo.WebImage.Image) Then
                                                    ClearLogo.WebImage.FromWeb(ClearLogo.URL)
                                                End If
                                                If Not IsNothing(ClearLogo.WebImage.Image) Then
                                                    tURL = ClearLogo.WebImage.SaveAsMovieLandscape(DBScrapeMovie)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovie.ClearLogoPath = tURL
                                                        MovieScraperEvent(Enums.ScraperEventType_Movie.ClearLogoItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovie.ClearLogoPath = ":" & ClearLogo.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'DiscArt
                If Master.GlobalScrapeMod.DiscArt Then
                    DiscArt.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If DiscArt.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.DiscArt) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.DiscArt, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then ' AndAlso Images.GetPreferredPoster(aList, DiscArt) Then
                                If aList.Count > 0 Then DiscArt = aList.Item(0)
                                If Not String.IsNullOrEmpty(DiscArt.URL) AndAlso IsNothing(DiscArt.WebImage.Image) Then
                                    DiscArt.WebImage.FromWeb(DiscArt.URL)
                                End If
                                If Not IsNothing(DiscArt.WebImage.Image) Then
                                    tURL = DiscArt.WebImage.SaveAsMovieDiscArt(DBScrapeMovie)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovie.DiscArtPath = tURL
                                        MovieScraperEvent(Enums.ScraperEventType_Movie.DiscArtItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1108, "A DiscArt of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovie, Enums.MovieImageType.DiscArt, aList, etList, efList) = DialogResult.OK Then
                                            DiscArt = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(DiscArt.URL) AndAlso IsNothing(DiscArt.WebImage.Image) Then
                                                    DiscArt.WebImage.FromWeb(DiscArt.URL)
                                                End If
                                                If Not IsNothing(DiscArt.WebImage.Image) Then
                                                    tURL = DiscArt.WebImage.SaveAsMovieLandscape(DBScrapeMovie)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovie.DiscArtPath = tURL
                                                        MovieScraperEvent(Enums.ScraperEventType_Movie.DiscArtItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovie.DiscArtPath = ":" & DiscArt.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Theme
                If Master.GlobalScrapeMod.Theme Then
                    Theme.Clear()
                    tUrlList.Clear()
                    tURL = String.Empty
                    If Theme.WebTheme.IsAllowedToDownload(DBScrapeMovie) Then
                        If Not ModulesManager.Instance.ScrapeTheme_Movie(DBScrapeMovie, tUrlList) Then
                            If tUrlList.Count > 0 Then
                                If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                    Theme.WebTheme.FromWeb(tUrlList.Item(0).URL, tUrlList.Item(0).WebURL)
                                    If Not IsNothing(Theme.WebTheme) Then 'TODO: fix check
                                        tURL = Theme.WebTheme.SaveAsMovieTheme(DBScrapeMovie)
                                        If Not String.IsNullOrEmpty(tURL) Then
                                            DBScrapeMovie.ThemePath = tURL
                                            MovieScraperEvent(Enums.ScraperEventType_Movie.ThemeItem, True)
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
                                    '            MovieScraperEvent(Enums.MovieScraperEventType.ThemeItem, True)
                                    '        End If
                                    '    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Trailer
                If Master.GlobalScrapeMod.Trailer Then
                    Trailer.Clear()
                    aUrlList.Clear()
                    tURL = String.Empty
                    If Trailer.WebTrailer.IsAllowedToDownload(DBScrapeMovie) Then
                        If Not ModulesManager.Instance.ScrapeTrailer_Movie(DBScrapeMovie, Enums.ScraperCapabilities.Trailer, aUrlList) Then
                            If aUrlList.Count > 0 Then
                                If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) AndAlso Trailers.GetPreferredTrailer(aUrlList, Trailer) Then
                                    If Not String.IsNullOrEmpty(Trailer.URL) AndAlso IsNothing(Trailer.WebTrailer) Then
                                        Trailer.WebTrailer.FromWeb(Trailer.URL)
                                    End If
                                    If Not IsNothing(Trailer.WebTrailer) Then 'TODO: fix check
                                        tURL = Trailer.WebTrailer.SaveAsMovieTrailer(DBScrapeMovie)
                                        If Not String.IsNullOrEmpty(tURL) Then
                                            DBScrapeMovie.TrailerPath = tURL
                                            MovieScraperEvent(Enums.ScraperEventType_Movie.TrailerItem, True)
                                        End If
                                    End If
                                    'ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                    '    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                    '        MsgBox(Master.eLang.GetString(930, "Trailer of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size:"))
                                    '    End If
                                    'Using dlgTrailerSelect As New dlgTrailerSelect()
                                    '    If dlgTrailerSelect.ShowDialog(DBScrapeMovie, aUrlList) = Windows.Forms.DialogResult.OK Then
                                    '        Trailer = dlgTrailerSelect.Results
                                    '        If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                    '            If Not String.IsNullOrEmpty(Trailer.URL) AndAlso IsNothing(Trailer.WebTrailer) Then
                                    '                Trailer.WebTrailer.FromWeb(Trailer.URL)
                                    '            End If
                                    '            If Not IsNothing(Trailer.WebTrailer) Then
                                    '                tURL = Trailer.WebTrailer.SaveAsMovieTrailer(DBScrapeMovie)
                                    '                If Not String.IsNullOrEmpty(tURL) Then
                                    '                    DBScrapeMovie.TrailerPath = tURL
                                    '                    MovieScraperEvent(Enums.MovieScraperEventType.TrailerItem, True)
                                    '                End If
                                    '            End If
                                    '        Else
                                    '            DBScrapeMovie.TrailerPath = ":" & Trailer.URL
                                    '        End If
                                    '    End If
                                    'End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Extrathumbs
                If Master.GlobalScrapeMod.EThumbs Then
                    If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                        aList.Clear()
                        etList.Clear()
                        If Fanart.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.EThumbs) Then
                            If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.Fanart, aList) Then
                                etList = Images.GetPreferredMovieEThumbs(aList)
                                If etList.Count > 0 Then
                                    Dim eti As Integer = 0
                                    Dim etMax As Integer = Master.eSettings.MovieEThumbsLimit
                                    For Each lItem As String In etList
                                        Dim EThumb As New Images
                                        EThumb.FromWeb(lItem)
                                        If Not IsNothing(EThumb.Image) Then
                                            Dim etPath As String = EThumb.SaveAsMovieExtrathumb(DBScrapeMovie)
                                            If Not String.IsNullOrEmpty(etPath) Then
                                                DBScrapeMovie.EThumbsPath = etPath
                                                MovieScraperEvent(Enums.ScraperEventType_Movie.EThumbsItem, True)
                                                eti = eti + 1
                                            End If
                                        End If
                                        If etMax > 0 AndAlso eti >= etMax Then Exit For
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Extrafanarts
                If Master.GlobalScrapeMod.EFanarts Then
                    If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                        aList.Clear()
                        efList.Clear()
                        If Fanart.WebImage.IsAllowedToDownload(DBScrapeMovie, Enums.MovieImageType.EFanarts) Then
                            If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, Enums.ScraperCapabilities.Fanart, aList) Then
                                efList = Images.GetPreferredMovieEFanarts(aList)
                                If efList.Count > 0 Then
                                    Dim efi As Integer = 0
                                    Dim efMax As Integer = Master.eSettings.MovieEFanartsLimit
                                    For Each lItem As String In efList
                                        Dim EFanart As New Images
                                        EFanart.FromWeb(lItem)
                                        If Not IsNothing(EFanart.Image) Then
                                            Dim efPath As String = EFanart.SaveAsMovieExtrafanart(DBScrapeMovie, Path.GetFileName(lItem))
                                            If Not String.IsNullOrEmpty(efPath) Then
                                                DBScrapeMovie.EFanartsPath = efPath
                                                MovieScraperEvent(Enums.ScraperEventType_Movie.EFanartsItem, True)
                                                efi = efi + 1
                                            End If
                                        End If
                                        If efMax > 0 AndAlso efi >= efMax Then Exit For
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'ActorThumbs
                If Master.GlobalScrapeMod.ActorThumbs AndAlso (Master.eSettings.MovieActorThumbsFrodo OrElse Master.eSettings.MovieActorThumbsEden) Then
                    If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                        For Each act As MediaContainers.Person In DBScrapeMovie.Movie.Actors
                            Dim img As New Images
                            img.FromWeb(act.Thumb)
                            If Not IsNothing(img.Image) Then
                                img.SaveAsMovieActorThumb(act, Directory.GetParent(DBScrapeMovie.Filename).FullName, DBScrapeMovie)
                            End If
                        Next
                    End If
                End If

                '-----

                If bwMovieScraper.CancellationPending Then Exit For

                If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieScraperRDYtoSave, Nothing, Nothing, False, DBScrapeMovie)
                    MovieScraperEvent(Enums.ScraperEventType_Movie.MoviePath, DBScrapeMovie.Filename)
                    Master.DB.SaveMovieToDB(DBScrapeMovie, False, False, Not String.IsNullOrEmpty(DBScrapeMovie.Movie.IMDBID))
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, DBScrapeMovie)
                    bwMovieScraper.ReportProgress(-1, If(Not OldTitle = NewTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldTitle, NewTitle), NewTitle))
                    bwMovieScraper.ReportProgress(-2, dScrapeRow.Item(0).ToString)
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        If Args.scrapeType = Enums.ScrapeType.SingleScrape Then
            Master.currMovie = DBScrapeMovie
        End If
        RemoveHandler ModulesManager.Instance.ScraperEvent_Movie, AddressOf MovieScraperEvent
        e.Result = New Results With {.scrapeType = Args.scrapeType}
        logger.Trace("Ended MOVIE scrape")
    End Sub

    Private Sub bwMovieScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMovieScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"moviescraped", 3, Master.eLang.GetString(813, "Movie Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            If Me.dgvMovies.SelectedRows.Count > 0 AndAlso Me.dgvMovies.SelectedRows(0).Cells(0).Value.ToString = e.UserState.ToString Then
                If Me.dgvMovies.CurrentCell Is Me.dgvMovies.SelectedRows(0).Cells(3) Then
                    Me.SelectMovieRow(Me.dgvMovies.SelectedRows(0).Index)
                End If
            End If
        Else
            Me.tspbLoading.Value += e.ProgressPercentage
            Me.SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwMovieSetScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMovieSetScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            Me.ScraperDone = True
        End If

        If Res.scrapeType = Enums.ScrapeType.SingleScrape Then
            Me.MovieSetInfoDownloaded()
        Else
            If Me.dgvMovieSets.SelectedRows.Count > 0 Then
                Me.SelectMovieSetRow(Me.dgvMovieSets.SelectedRows(0).Index)
            Else
                Me.ClearInfo()
            End If
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwMovieSetScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieSetScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim OldTitle As String = String.Empty
        Dim NewTitle As String = String.Empty
        Dim Banner As New MediaContainers.Image
        Dim ClearArt As New MediaContainers.Image
        Dim ClearLogo As New MediaContainers.Image
        Dim DiscArt As New MediaContainers.Image
        Dim Fanart As New MediaContainers.Image
        Dim Landscape As New MediaContainers.Image
        Dim Poster As New MediaContainers.Image
        Dim tURL As String = String.Empty
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)
        Dim DBScrapeMovieSet As New Structures.DBMovieSet
        Dim configpath As String = ""
        Dim formatter As New BinaryFormatter()

        logger.Trace("Starting MOVIE SET scrape")

        AddHandler ModulesManager.Instance.ScraperEvent_MovieSet, AddressOf MovieSetScraperEvent

        For Each dRow As DataRow In ScrapeList
            Try
                If bwMovieSetScraper.CancellationPending Then Exit For
                OldTitle = dRow.Item(1).ToString
                bwMovieSetScraper.ReportProgress(1, OldTitle)

                dScrapeRow = dRow

                DBScrapeMovieSet = Master.DB.LoadMovieSetFromDB(Convert.ToInt64(dRow.Item(0)))
                'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEditMovieSet, Nothing, DBScrapeMovieSet)

                If Master.GlobalScrapeMod.NFO Then
                    If ModulesManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, Args.scrapeType, Args.Options_MovieSet) Then
                        Exit Try
                    End If
                Else
                    ' if we do not have the movie set ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                    If String.IsNullOrEmpty(DBScrapeMovieSet.MovieSet.ID) AndAlso (Master.GlobalScrapeMod.Banner Or Master.GlobalScrapeMod.ClearArt Or _
                                                                             Master.GlobalScrapeMod.ClearLogo Or Master.GlobalScrapeMod.DiscArt Or _
                                                                             Master.GlobalScrapeMod.Fanart Or Master.GlobalScrapeMod.Landscape Or _
                                                                             Master.GlobalScrapeMod.Poster) Then
                        Dim tOpt As New Structures.ScrapeOptions_MovieSet 'all false value not to override any field
                        If ModulesManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, Args.scrapeType, tOpt) Then
                            Exit For
                        End If
                    End If
                End If

                If bwMovieSetScraper.CancellationPending Then Exit For

                If Not Args.scrapeType = Enums.ScrapeType.SingleScrape Then
                    MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.NFOItem, True)
                End If

                NewTitle = DBScrapeMovieSet.ListTitle

                If Not NewTitle = OldTitle Then
                    bwMovieSetScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldTitle, NewTitle))
                End If

                If Not Args.scrapeType = Enums.ScrapeType.SingleScrape Then
                    MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.ListTitle, NewTitle)
                End If

                '-----

                'Poster
                If Master.GlobalScrapeMod.Poster Then
                    Poster.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If Poster.WebImage.IsAllowedToDownload(DBScrapeMovieSet, Enums.MovieImageType.Poster) Then
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, Enums.ScraperCapabilities.Poster, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) AndAlso Images.GetPreferredMovieSetPoster(aList, Poster) Then
                                If Not String.IsNullOrEmpty(Poster.URL) AndAlso IsNothing(Poster.WebImage.Image) Then
                                    Poster.WebImage.FromWeb(Poster.URL)
                                End If
                                If Not IsNothing(Poster.WebImage.Image) Then
                                    tURL = Poster.WebImage.SaveAsMovieSetPoster(DBScrapeMovieSet)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovieSet.PosterPath = tURL
                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.PosterItem, True)
                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                        'End If
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(928, "A poster of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovieSet, Enums.MovieImageType.Poster, aList, etList, efList) = DialogResult.OK Then
                                            Poster = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Poster.URL) AndAlso IsNothing(Poster.WebImage.Image) Then
                                                    Poster.WebImage.FromWeb(Poster.URL)
                                                End If
                                                If Not IsNothing(Poster.WebImage.Image) Then
                                                    tURL = Poster.WebImage.SaveAsMovieSetPoster(DBScrapeMovieSet)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovieSet.PosterPath = tURL
                                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.PosterItem, True)
                                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                                        'End If
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovieSet.PosterPath = ":" & Poster.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieSetScraper.CancellationPending Then Exit For

                'Fanart
                If Master.GlobalScrapeMod.Fanart Then
                    Fanart.Clear()
                    aList.Clear()
                    efList.Clear()
                    etList.Clear()
                    tURL = String.Empty
                    If Fanart.WebImage.IsAllowedToDownload(DBScrapeMovieSet, Enums.MovieImageType.Fanart) Then
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, Enums.ScraperCapabilities.Fanart, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) AndAlso Images.GetPreferredMovieSetFanart(aList, Fanart) Then
                                If Not String.IsNullOrEmpty(Fanart.URL) AndAlso IsNothing(Fanart.WebImage.Image) Then
                                    Fanart.WebImage.FromWeb(Fanart.URL)
                                End If
                                If Not IsNothing(Fanart.WebImage.Image) Then
                                    tURL = Fanart.WebImage.SaveAsMovieSetFanart(DBScrapeMovieSet)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovieSet.FanartPath = tURL
                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.FanartItem, True)
                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                        '    DBScrapeMovie.Movie.Fanart = fResults.Fanart
                                        'End If
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(927, "Fanart of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size:"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovieSet, Enums.MovieImageType.Fanart, aList, efList, etList) = DialogResult.OK Then
                                            Fanart = dImgSelect.Results
                                            efList = dImgSelect.efList
                                            etList = dImgSelect.etList
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Fanart.URL) AndAlso IsNothing(Fanart.WebImage.Image) Then
                                                    Fanart.WebImage.FromWeb(Fanart.URL)
                                                End If
                                                If Not IsNothing(Fanart.WebImage.Image) Then
                                                    tURL = Fanart.WebImage.SaveAsMovieSetFanart(DBScrapeMovieSet)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovieSet.FanartPath = tURL
                                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.FanartItem, True)
                                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                                        '    DBScrapeMovie.Movie.Fanart = fResults.Fanart
                                                        'End If
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovieSet.FanartPath = ":" & Fanart.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieSetScraper.CancellationPending Then Exit For

                'Banner
                If Master.GlobalScrapeMod.Banner Then
                    Banner.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If Banner.WebImage.IsAllowedToDownload(DBScrapeMovieSet, Enums.MovieImageType.Banner) Then
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, Enums.ScraperCapabilities.Banner, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then 'AndAlso Images.GetPreferredPoster(aList, Banner) Then 'TODO: Check if we need PreferredBanner
                                If aList.Count > 0 Then Banner = aList.Item(0)
                                If Not String.IsNullOrEmpty(Banner.URL) AndAlso IsNothing(Banner.WebImage.Image) Then
                                    Banner.WebImage.FromWeb(Banner.URL)
                                End If
                                If Not IsNothing(Banner.WebImage.Image) Then
                                    tURL = Banner.WebImage.SaveAsMovieSetBanner(DBScrapeMovieSet)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovieSet.BannerPath = tURL
                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.BannerItem, True)
                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                        'End If
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1062, "A banner of your preferred type could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovieSet, Enums.MovieImageType.Banner, aList, etList, efList) = DialogResult.OK Then
                                            Banner = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Banner.URL) AndAlso IsNothing(Banner.WebImage.Image) Then
                                                    Banner.WebImage.FromWeb(Banner.URL)
                                                End If
                                                If Not IsNothing(Banner.WebImage.Image) Then
                                                    tURL = Banner.WebImage.SaveAsMovieSetBanner(DBScrapeMovieSet)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovieSet.BannerPath = tURL
                                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.BannerItem, True)
                                                        'If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
                                                        '    DBScrapeMovie.Movie.Thumb = pResults.Posters
                                                        'End If
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovieSet.BannerPath = ":" & Banner.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieSetScraper.CancellationPending Then Exit For

                'Landscape
                If Master.GlobalScrapeMod.Landscape Then
                    Landscape.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If Landscape.WebImage.IsAllowedToDownload(DBScrapeMovieSet, Enums.MovieImageType.Landscape) Then
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, Enums.ScraperCapabilities.Landscape, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then 'AndAlso Images.GetPreferredPoster(aList, Landscape) Then
                                If aList.Count > 0 Then Landscape = aList.Item(0)
                                If Not String.IsNullOrEmpty(Landscape.URL) AndAlso IsNothing(Landscape.WebImage.Image) Then
                                    Landscape.WebImage.FromWeb(Landscape.URL)
                                End If
                                If Not IsNothing(Landscape.WebImage.Image) Then
                                    tURL = Landscape.WebImage.SaveAsMovieSetLandscape(DBScrapeMovieSet)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovieSet.LandscapePath = tURL
                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.LandscapeItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1063, "A landscape of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovieSet, Enums.MovieImageType.Landscape, aList, etList, efList) = DialogResult.OK Then
                                            Landscape = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(Landscape.URL) AndAlso IsNothing(Landscape.WebImage.Image) Then
                                                    Landscape.WebImage.FromWeb(Landscape.URL)
                                                End If
                                                If Not IsNothing(Landscape.WebImage.Image) Then
                                                    tURL = Landscape.WebImage.SaveAsMovieSetLandscape(DBScrapeMovieSet)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovieSet.LandscapePath = tURL
                                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.LandscapeItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovieSet.LandscapePath = ":" & Landscape.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieSetScraper.CancellationPending Then Exit For

                'ClearArt
                If Master.GlobalScrapeMod.ClearArt Then
                    ClearArt.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If ClearArt.WebImage.IsAllowedToDownload(DBScrapeMovieSet, Enums.MovieImageType.ClearArt) Then
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, Enums.ScraperCapabilities.ClearArt, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then ' AndAlso Images.GetPreferredPoster(aList, ClearArt) Then
                                If aList.Count > 0 Then ClearArt = aList.Item(0)
                                If Not String.IsNullOrEmpty(ClearArt.URL) AndAlso IsNothing(ClearArt.WebImage.Image) Then
                                    ClearArt.WebImage.FromWeb(ClearArt.URL)
                                End If
                                If Not IsNothing(ClearArt.WebImage.Image) Then
                                    tURL = ClearArt.WebImage.SaveAsMovieSetClearArt(DBScrapeMovieSet)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovieSet.ClearArtPath = tURL
                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.ClearArtItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1106, "A ClearArt of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovieSet, Enums.MovieImageType.ClearArt, aList, etList, efList) = DialogResult.OK Then
                                            ClearArt = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(ClearArt.URL) AndAlso IsNothing(ClearArt.WebImage.Image) Then
                                                    ClearArt.WebImage.FromWeb(ClearArt.URL)
                                                End If
                                                If Not IsNothing(ClearArt.WebImage.Image) Then
                                                    tURL = ClearArt.WebImage.SaveAsMovieSetLandscape(DBScrapeMovieSet)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovieSet.ClearArtPath = tURL
                                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.ClearArtItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovieSet.ClearArtPath = ":" & ClearArt.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieSetScraper.CancellationPending Then Exit For

                'ClearLogo
                If Master.GlobalScrapeMod.ClearLogo Then
                    ClearLogo.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If ClearLogo.WebImage.IsAllowedToDownload(DBScrapeMovieSet, Enums.MovieImageType.ClearLogo) Then
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, Enums.ScraperCapabilities.ClearLogo, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then ' AndAlso Images.GetPreferredPoster(aList, ClearLogo) Then
                                If aList.Count > 0 Then ClearLogo = aList.Item(0)
                                If Not String.IsNullOrEmpty(ClearLogo.URL) AndAlso IsNothing(ClearLogo.WebImage.Image) Then
                                    ClearLogo.WebImage.FromWeb(ClearLogo.URL)
                                End If
                                If Not IsNothing(ClearLogo.WebImage.Image) Then
                                    tURL = ClearLogo.WebImage.SaveAsMovieSetClearLogo(DBScrapeMovieSet)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovieSet.ClearLogoPath = tURL
                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.ClearLogoItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1107, "A ClearLogo of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovieSet, Enums.MovieImageType.ClearLogo, aList, etList, efList) = DialogResult.OK Then
                                            ClearLogo = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(ClearLogo.URL) AndAlso IsNothing(ClearLogo.WebImage.Image) Then
                                                    ClearLogo.WebImage.FromWeb(ClearLogo.URL)
                                                End If
                                                If Not IsNothing(ClearLogo.WebImage.Image) Then
                                                    tURL = ClearLogo.WebImage.SaveAsMovieSetLandscape(DBScrapeMovieSet)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovieSet.ClearLogoPath = tURL
                                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.ClearLogoItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovieSet.ClearLogoPath = ":" & ClearLogo.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieSetScraper.CancellationPending Then Exit For

                'DiscArt
                If Master.GlobalScrapeMod.DiscArt Then
                    DiscArt.Clear()
                    aList.Clear()
                    tURL = String.Empty
                    If DiscArt.WebImage.IsAllowedToDownload(DBScrapeMovieSet, Enums.MovieImageType.DiscArt) Then
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, Enums.ScraperCapabilities.DiscArt, aList) Then
                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then ' AndAlso Images.GetPreferredPoster(aList, DiscArt) Then
                                If aList.Count > 0 Then DiscArt = aList.Item(0)
                                If Not String.IsNullOrEmpty(DiscArt.URL) AndAlso IsNothing(DiscArt.WebImage.Image) Then
                                    DiscArt.WebImage.FromWeb(DiscArt.URL)
                                End If
                                If Not IsNothing(DiscArt.WebImage.Image) Then
                                    tURL = DiscArt.WebImage.SaveAsMovieSetDiscArt(DBScrapeMovieSet)
                                    If Not String.IsNullOrEmpty(tURL) Then
                                        DBScrapeMovieSet.DiscArtPath = tURL
                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.DiscArtItem, True)
                                    End If
                                End If
                            ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                If aList.Count > 0 Then
                                    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        MsgBox(Master.eLang.GetString(1108, "A DiscArt of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size"))
                                    End If
                                    Using dImgSelect As New dlgImgSelect()
                                        If dImgSelect.ShowDialog(DBScrapeMovieSet, Enums.MovieImageType.DiscArt, aList, etList, efList) = DialogResult.OK Then
                                            DiscArt = dImgSelect.Results
                                            If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                                                If Not String.IsNullOrEmpty(DiscArt.URL) AndAlso IsNothing(DiscArt.WebImage.Image) Then
                                                    DiscArt.WebImage.FromWeb(DiscArt.URL)
                                                End If
                                                If Not IsNothing(DiscArt.WebImage.Image) Then
                                                    tURL = DiscArt.WebImage.SaveAsMovieSetLandscape(DBScrapeMovieSet)
                                                    If Not String.IsNullOrEmpty(tURL) Then
                                                        DBScrapeMovieSet.DiscArtPath = tURL
                                                        MovieSetScraperEvent(Enums.ScraperEventType_MovieSet.DiscArtItem, True)
                                                    End If
                                                End If
                                            Else
                                                DBScrapeMovieSet.DiscArtPath = ":" & DiscArt.URL
                                            End If
                                        End If
                                    End Using
                                End If
                            End If
                        End If
                    End If
                End If

                '-----

                If bwMovieScraper.CancellationPending Then Exit For

                If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                    Master.DB.SaveMovieSetToDB(DBScrapeMovieSet, False, False, True)
                    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, DBScrapeMovieSet)
                    bwMovieSetScraper.ReportProgress(-1, If(Not OldTitle = NewTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldTitle, NewTitle), NewTitle))
                    bwMovieSetScraper.ReportProgress(-2, dScrapeRow.Item(0).ToString)
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        If Args.scrapeType = Enums.ScrapeType.SingleScrape Then
            Master.currMovieSet = DBScrapeMovieSet
        End If
        RemoveHandler ModulesManager.Instance.ScraperEvent_MovieSet, AddressOf MovieSetScraperEvent
        e.Result = New Results With {.scrapeType = Args.scrapeType}
        logger.Trace("Ended MOVIESET scrape")
    End Sub

    Private Sub bwMovieSetScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMovieSetScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"moviesetscraped", 3, Master.eLang.GetString(1204, "MovieSet Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            If Me.dgvMovieSets.SelectedRows.Count > 0 AndAlso Me.dgvMovieSets.SelectedRows(0).Cells(0).Value.ToString = e.UserState.ToString Then
                If Me.dgvMovieSets.CurrentCell Is Me.dgvMovieSets.SelectedRows(0).Cells(3) Then
                    Me.SelectMovieSetRow(Me.dgvMovieSets.SelectedRows(0).Index)
                End If
            End If
        Else
            Me.tspbLoading.Value += e.ProgressPercentage
            Me.SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwNonScrape_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwNonScrape.RunWorkerCompleted
        Dim configpath As String = FileUtils.Common.ReturnSettingsFile("Settings", "ScraperStatus.xml")
        If File.Exists(configpath) Then
            File.Delete(configpath)
        End If
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.btnCancel.Visible = False
        Me.lblCanceling.Visible = False
        Me.prbCanceling.Visible = False
        Me.pnlCancel.Visible = False
        Me.SetControlsEnabled(True)
        Me.EnableFilters(True)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bwNonScrape_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwNonScrape.DoWork
        Dim scrapeMovie As Structures.DBMovie
        Dim iCount As Integer = 0
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()

            Try
                If Me.dtMovies.Rows.Count > 0 Then

                    Select Case Args.scrapeType
                        Case Enums.ScrapeType.CleanFolders
                            Dim fDeleter As New FileUtils.Delete
                            For Each drvRow As DataRow In Me.dtMovies.Rows
                                Try
                                    Me.bwNonScrape.ReportProgress(iCount, drvRow.Item(15))
                                    iCount += 1
                                    If Convert.ToBoolean(drvRow.Item(14)) Then Continue For

                                    If Me.bwNonScrape.CancellationPending Then GoTo doCancel

                                    scrapeMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(drvRow.Item(0)))

                                    fDeleter.GetItemsToDelete(True, scrapeMovie)

                                    Me.RefreshMovie(Convert.ToInt64(drvRow.Item(0)), True, True)

                                    Me.bwNonScrape.ReportProgress(iCount, String.Format("[[{0}]]", drvRow.Item(0).ToString))
                                Catch ex As Exception
                                    logger.Error(New StackFrame().GetMethod().Name, ex)
                                End Try
                            Next
                        Case Enums.ScrapeType.CopyBackdrops 'TODO: check MovieBackdropsPath and VIDEO_TS parent
                            Dim sPath As String = String.Empty
                            For Each drvRow As DataRow In Me.dtMovies.Rows
                                Try
                                    Me.bwNonScrape.ReportProgress(iCount, drvRow.Item(15).ToString)
                                    iCount += 1

                                    If Me.bwNonScrape.CancellationPending Then GoTo doCancel
                                    sPath = drvRow.Item(37).ToString
                                    If Not String.IsNullOrEmpty(sPath) Then
                                        If FileUtils.Common.isVideoTS(sPath) Then
                                            'If Master.eSettings.VideoTSParent Then
                                            '    FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, Directory.GetParent(Directory.GetParent(sPath).FullName).Name), "-fanart.jpg")))
                                            'Else
                                            If Path.GetFileName(sPath).ToLower = "fanart.jpg" Then
                                                FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).Name, "-fanart.jpg")))
                                            Else
                                                FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(sPath)))
                                            End If
                                            'End If
                                        ElseIf FileUtils.Common.isBDRip(sPath) Then
                                            'If Master.eSettings.VideoTSParent Then
                                            '    FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName).Name), "-fanart.jpg")))
                                            'Else
                                            If Path.GetFileName(sPath).ToLower = "fanart.jpg" Then
                                                FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName).Name, "-fanart.jpg")))
                                            Else
                                                FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(sPath)))
                                            End If
                                            'End If
                                        Else
                                            If Path.GetFileName(sPath).ToLower = "fanart.jpg" Then
                                                FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.GetFileNameWithoutExtension(drvRow.Item(1).ToString), "-fanart.jpg")))
                                            Else
                                                FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(sPath)))
                                            End If

                                        End If
                                    End If
                                Catch ex As Exception
                                    logger.Error(New StackFrame().GetMethod().Name, ex)
                                End Try
                            Next
                    End Select

doCancel:
                    If Not Args.scrapeType = Enums.ScrapeType.CopyBackdrops Then
                        SQLtransaction.Commit()
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Using
    End Sub

    Private Sub bwNonScrape_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwNonScrape.ProgressChanged
        If Not Master.isCL Then
            If Regex.IsMatch(e.UserState.ToString, "\[\[[0-9]+\]\]") AndAlso Me.dgvMovies.SelectedRows.Count > 0 Then
                Try
                    If Me.dgvMovies.SelectedRows(0).Cells(0).Value.ToString = e.UserState.ToString.Replace("[[", String.Empty).Replace("]]", String.Empty).Trim Then
                        Me.SelectMovieRow(Me.dgvMovies.SelectedRows(0).Index)
                    End If
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Else
                Me.SetStatus(e.UserState.ToString)
                Me.tspbLoading.Value = e.ProgressPercentage
            End If
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub bwRefreshMovies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwRefreshMovies.DoWork
        Dim iCount As Integer = 0
        Dim MovieIDs As New Dictionary(Of Long, String)

        For Each sRow As DataRow In Me.dtMovies.Rows
            MovieIDs.Add(Convert.ToInt64(sRow.Item(0)), sRow.Item(3).ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieIDs
                Try
                    If Me.bwMovieScraper.CancellationPending Then Return
                    Me.bwRefreshMovies.ReportProgress(iCount, KVP.Value)
                    Me.RefreshMovie(KVP.Key, True)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub bwRefreshMovies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwRefreshMovies.ProgressChanged
        Me.SetStatus(e.UserState.ToString)
        Me.tspbLoading.Value = e.ProgressPercentage
    End Sub

    Private Sub bwRefreshMovies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRefreshMovies.RunWorkerCompleted
        Me.tslLoading.Text = String.Empty
        Me.tspbLoading.Visible = False
        Me.tslLoading.Visible = False

        Me.FillList(True, True, False)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bwRefreshMovieSets_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwRefreshMovieSets.DoWork
        Dim iCount As Integer = 0
        Dim MovieSetIDs As New Dictionary(Of Long, String)

        For Each sRow As DataRow In Me.dtMovieSets.Rows
            MovieSetIDs.Add(Convert.ToInt64(sRow.Item(0)), sRow.Item(1).ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieSetIDs
                Try
                    If Me.bwMovieScraper.CancellationPending Then Return
                    Me.bwRefreshMovieSets.ReportProgress(iCount, KVP.Value)
                    Me.RefreshMovieSet(KVP.Key, True)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub bwRefreshMovieSets_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwRefreshMovieSets.ProgressChanged
        Me.SetStatus(e.UserState.ToString)
        Me.tspbLoading.Value = e.ProgressPercentage
    End Sub

    Private Sub bwRefreshMovieSets_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRefreshMovieSets.RunWorkerCompleted
        Me.tslLoading.Text = String.Empty
        Me.tspbLoading.Visible = False
        Me.tslLoading.Visible = False

        Me.FillList(False, True, False)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cbFilterFileSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterFileSource.SelectedIndexChanged
        Try
            While Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwCleanDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            For i As Integer = Me.FilterArray.Count - 1 To 0 Step -1
                If Me.FilterArray(i).ToString.StartsWith("FileSource =") Then
                    Me.FilterArray.RemoveAt(i)
                End If
            Next

            If Not cbFilterFileSource.Text = Master.eLang.All Then
                Me.FilterArray.Add(String.Format("FileSource = '{0}'", If(cbFilterFileSource.Text = Master.eLang.None, String.Empty, cbFilterFileSource.Text)))
            End If

            Me.RunFilter()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cbFilterYearMod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearMod.SelectedIndexChanged
        Try
            If Not String.IsNullOrEmpty(cbFilterYear.Text) AndAlso Not cbFilterYear.Text = Master.eLang.All Then
                Me.FilterArray.Remove(Me.filYear)
                Me.filYear = String.Empty

                Me.filYear = String.Concat("Year ", cbFilterYearMod.Text, " '", cbFilterYear.Text, "'")

                Me.FilterArray.Add(Me.filYear)
                Me.RunFilter()
            Else
                If Not String.IsNullOrEmpty(Me.filYear) Then
                    Me.FilterArray.Remove(Me.filYear)
                    Me.filYear = String.Empty
                    Me.RunFilter()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub cbFilterYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYear.SelectedIndexChanged
        Try
            If Not String.IsNullOrEmpty(cbFilterYearMod.Text) AndAlso Not cbFilterYear.Text = Master.eLang.All Then
                Me.FilterArray.Remove(Me.filYear)
                Me.filYear = String.Empty

                Me.filYear = String.Concat("Year ", cbFilterYearMod.Text, " '", cbFilterYear.Text, "'")

                Me.FilterArray.Add(Me.filYear)
                Me.RunFilter()
            Else
                If Not String.IsNullOrEmpty(Me.filYear) Then
                    Me.FilterArray.Remove(Me.filYear)
                    Me.filYear = String.Empty
                    Me.RunFilter()
                End If

                If cbFilterYear.Text = Master.eLang.All Then
                    Me.cbFilterYearMod.Text = String.Empty
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub cbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSearch.SelectedIndexChanged
        Me.currText = Me.txtSearch.Text

        Me.tmrSearchWait.Enabled = False
        Me.tmrSearchMovie.Enabled = False
        Me.tmrSearchWait.Enabled = True

    End Sub

    Private Sub chkFilterDupe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterDupe.Click
        Try
            Me.RunFilter(True)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub chkFilterLock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterLock.Click
        Try
            If Me.chkFilterLock.Checked Then
                Me.FilterArray.Add("Lock = 1")
            Else
                Me.FilterArray.Remove("Lock = 1")
            End If
            Me.RunFilter()
        Catch
        End Try
    End Sub

    Private Sub chkFilterMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMark.Click
        Try
            If Me.chkFilterMark.Checked Then
                Me.FilterArray.Add("mark = 1")
            Else
                Me.FilterArray.Remove("mark = 1")
            End If
            Me.RunFilter()
        Catch
        End Try
    End Sub

    Private Sub chkFilterMissing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterMissing.Click
        Try
            Dim MissingFilter As New List(Of String)
            If Me.chkFilterMissing.Checked Then
                With Master.eSettings
                    If .MovieMissingBanner Then MissingFilter.Add("HasBanner = 0")
                    If .MovieMissingClearArt Then MissingFilter.Add("HasClearArt = 0")
                    If .MovieMissingClearLogo Then MissingFilter.Add("HasClearLogo = 0")
                    If .MovieMissingDiscArt Then MissingFilter.Add("HasDiscArt = 0")
                    If .MovieMissingEFanarts Then MissingFilter.Add("HasEFanarts = 0")
                    If .MovieMissingEThumbs Then MissingFilter.Add("HasEThumbs = 0")
                    If .MovieMissingFanart Then MissingFilter.Add("HasFanart = 0")
                    If .MovieMissingLandscape Then MissingFilter.Add("HasLandscape = 0")
                    If .MovieMissingNFO Then MissingFilter.Add("HasNfo = 0")
                    If .MovieMissingPoster Then MissingFilter.Add("HasPoster = 0")
                    If .MovieMissingSubs Then MissingFilter.Add("HasSub = 0")
                    If .MovieMissingTheme Then MissingFilter.Add("HasTheme = 0")
                    If .MovieMissingTrailer Then MissingFilter.Add("HasTrailer = 0")
                End With
                filMissing = Microsoft.VisualBasic.Strings.Join(MissingFilter.ToArray, " OR ")
                Me.FilterArray.Add(filMissing)
            Else
                Me.FilterArray.Remove(filMissing)
            End If
            Me.RunFilter()
        Catch
        End Try
    End Sub

    Private Sub chkFilterNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNew.Click
        Try
            If Me.chkFilterNew.Checked Then
                Me.FilterArray.Add("new = 1")
            Else
                Me.FilterArray.Remove("new = 1")
            End If
            Me.RunFilter()
        Catch
        End Try
    End Sub

    Private Sub chkFilterTolerance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterTolerance.Click
        Try
            If Me.chkFilterTolerance.Checked Then
                Me.FilterArray.Add("OutOfTolerance = 1")
            Else
                Me.FilterArray.Remove("OutOfTolerance = 1")
            End If
            Me.RunFilter()
        Catch
        End Try
    End Sub

    Private Sub clbFilterGenres_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterGenres.LostFocus
        Try
            Me.pnlFilterGenre.Visible = False
            Me.pnlFilterGenre.Tag = "NO"

            If clbFilterGenres.CheckedItems.Count > 0 Then
                Me.txtFilterGenre.Text = String.Empty
                Me.FilterArray.Remove(Me.filGenre)

                Dim alGenres As New List(Of String)
                alGenres.AddRange(clbFilterGenres.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd.Checked Then
                    Me.txtFilterGenre.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")
                Else
                    Me.txtFilterGenre.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")
                End If

                For i As Integer = 0 To alGenres.Count - 1
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                Next

                If rbFilterAnd.Checked Then
                    Me.filGenre = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND "))
                Else
                    Me.filGenre = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR "))
                End If

                Me.FilterArray.Add(Me.filGenre)
                Me.RunFilter()
            Else
                If Not String.IsNullOrEmpty(Me.filGenre) Then
                    Me.txtFilterGenre.Text = String.Empty
                    Me.FilterArray.Remove(Me.filGenre)
                    Me.filGenre = String.Empty
                    Me.RunFilter()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterSource_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterSource.LostFocus
        Try
            Me.pnlFilterSource.Visible = False
            Me.pnlFilterSource.Tag = "NO"

            If clbFilterSource.CheckedItems.Count > 0 Then
                Me.txtFilterSource.Text = String.Empty
                Me.FilterArray.Remove(Me.filSource)

                Dim alSource As New List(Of String)
                alSource.AddRange(clbFilterSource.CheckedItems.OfType(Of String).ToList)

                Me.txtFilterSource.Text = Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " | ")

                For i As Integer = 0 To alSource.Count - 1
                    alSource.Item(i) = String.Format("Source = '{0}'", alSource.Item(i))
                Next

                Me.filSource = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " OR "))

                Me.FilterArray.Add(Me.filSource)
                Me.RunFilter()
            Else
                If Not String.IsNullOrEmpty(Me.filSource) Then
                    Me.txtFilterSource.Text = String.Empty
                    Me.FilterArray.Remove(Me.filSource)
                    Me.filSource = String.Empty
                    Me.RunFilter()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub mnuMainToolsCleanDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsCleanDB.Click, cmnuTrayToolsCleanDB.Click
        Me.SetControlsEnabled(False, True)
        Me.tspbLoading.Style = ProgressBarStyle.Marquee
        Me.EnableFilters(False)

        Me.SetStatus(Master.eLang.GetString(644, "Cleaning Database..."))
        Me.tspbLoading.Visible = True

        Me.bwCleanDB.WorkerSupportsCancellation = True
        Me.bwCleanDB.RunWorkerAsync()
    End Sub

    Private Sub CleanFiles()
        Try
            Dim sWarning As String = String.Empty
            Dim sWarningFile As String = String.Empty
            With Master.eSettings
                If .FileSystemExpertCleaner Then
                    sWarning = String.Concat(Master.eLang.GetString(102, "WARNING: If you continue, all non-whitelisted file types will be deleted!"), vbNewLine, vbNewLine, Master.eLang.GetString(101, "Are you sure you want to continue?"))
                Else
                    If .CleanDotFanartJPG Then sWarningFile += String.Concat("<movie>.fanart.jpg", vbNewLine)
                    If .CleanFanartJPG Then sWarningFile += String.Concat("fanart.jpg", vbNewLine)
                    If .CleanFolderJPG Then sWarningFile += String.Concat("folder.jpg", vbNewLine)
                    If .CleanMovieFanartJPG Then sWarningFile += String.Concat("<movie>-fanart.jpg", vbNewLine)
                    If .CleanMovieJPG Then sWarningFile += String.Concat("movie.jpg", vbNewLine)
                    If .CleanMovieNameJPG Then sWarningFile += String.Concat("<movie>.jpg", vbNewLine)
                    If .CleanMovieNFO Then sWarningFile += String.Concat("movie.nfo", vbNewLine)
                    If .CleanMovieNFOB Then sWarningFile += String.Concat("<movie>.nfo", vbNewLine)
                    If .CleanMovieTBN Then sWarningFile += String.Concat("movie.tbn", vbNewLine)
                    If .CleanMovieTBNB Then sWarningFile += String.Concat("<movie>.tbn", vbNewLine)
                    If .CleanPosterJPG Then sWarningFile += String.Concat("poster.jpg", vbNewLine)
                    If .CleanPosterTBN Then sWarningFile += String.Concat("poster.tbn", vbNewLine)
                    If .CleanExtrathumbs Then sWarningFile += String.Concat("/extrathumbs/", vbNewLine)
                    sWarning = String.Concat(Master.eLang.GetString(103, "WARNING: If you continue, all files of the following types will be permanently deleted:"), vbNewLine, vbNewLine, sWarningFile, vbNewLine, Master.eLang.GetString(101, "Are you sure you want to continue?"))
                End If
            End With
            If MsgBox(sWarning, MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are you sure?")) = MsgBoxResult.Yes Then
                Me.NonScrape(Enums.ScrapeType.CleanFolders, Nothing)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub mnuMainToolsCleanFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsCleanFiles.Click, cmnuTrayToolsCleanFiles.Click
        Me.CleanFiles()
    End Sub

    Private Sub mnuMainToolsClearCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsClearCache.Click, cmnuTrayToolsClearCache.Click
        'for future use
    End Sub

    Private Sub ClearFilters(Optional ByVal Reload As Boolean = False)
        Try
            Me.bsMovies.RemoveFilter()
            Me.FilterArray.Clear()
            Me.filSearch = String.Empty
            Me.filGenre = String.Empty
            Me.filYear = String.Empty
            Me.filSource = String.Empty

            RemoveHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged
            Me.txtSearch.Text = String.Empty
            AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged
            If Me.cbSearch.Items.Count > 0 Then
                Me.cbSearch.SelectedIndex = 0
            End If
            Me.chkFilterDupe.Checked = False
            Me.chkFilterTolerance.Checked = False
            Me.chkFilterMissing.Checked = False
            Me.chkFilterMark.Checked = False
            Me.chkFilterNew.Checked = False
            Me.chkFilterLock.Checked = False
            Me.rbFilterOr.Checked = False
            Me.rbFilterAnd.Checked = True
            Me.txtFilterGenre.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterGenres.Items.Count - 1
                Me.clbFilterGenres.SetItemChecked(i, False)
            Next
            Me.txtFilterSource.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterSource.Items.Count - 1
                Me.clbFilterSource.SetItemChecked(i, False)
            Next

            RemoveHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged
            If Me.cbFilterYear.Items.Count > 0 Then
                Me.cbFilterYear.SelectedIndex = 0
            End If
            AddHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged

            RemoveHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged
            If Me.cbFilterYearMod.Items.Count > 0 Then
                Me.cbFilterYearMod.SelectedIndex = 0
            End If
            AddHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged

            RemoveHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged
            If Me.cbFilterFileSource.Items.Count > 0 Then
                Me.cbFilterFileSource.SelectedIndex = 0
            End If
            AddHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged

            If Reload Then Me.FillList(True, False, False)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnShowOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowOpenFolder.Click
        If Me.dgvTVShows.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If Me.dgvTVShows.SelectedRows.Count > 10 Then
                If Not MsgBox(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvTVShows.SelectedRows.Count), MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    Using Explorer As New Diagnostics.Process

                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", sRow.Cells(7).Value.ToString)
                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            Explorer.StartInfo.Arguments = String.Format("""{0}""", sRow.Cells(7).Value.ToString)
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub cmnuChangeEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeChange.Click
        Me.SetControlsEnabled(False, True)
        Dim tEpisode As MediaContainers.EpisodeDetails = ModulesManager.Instance.ChangeEpisode(Convert.ToInt32(Master.currShow.ShowID), Me.tmpTVDB, Me.tmpLang)

        If Not IsNothing(tEpisode) Then
            Master.currShow.TVEp = tEpisode
            Master.currShow.EpPosterPath = tEpisode.Poster.SaveAsTVEpisodePoster(Master.currShow)

            Master.DB.SaveTVEpToDB(Master.currShow, False, True, False, True)

            Me.FillEpisodes(Convert.ToInt32(Master.currShow.ShowID), Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(2).Value))
        End If

        Me.SetControlsEnabled(True)
    End Sub

    Private Sub cmnuChangeShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowChange.Click
        Me.SetControlsEnabled(False, True)
        Dim ShowLang As String = Me.dgvTVShows.Item(22, Me.dgvTVShows.SelectedRows(0).Index).Value.ToString
        Dim SourceLang As String = Master.DB.GetTVSourceLanguage(Me.dgvTVShows.Item(8, Me.dgvTVShows.SelectedRows(0).Index).Value.ToString)
        ModulesManager.Instance.TVScrapeOnly(Convert.ToInt32(Me.dgvTVShows.Item(0, Me.dgvTVShows.SelectedRows(0).Index).Value), Me.dgvTVShows.Item(1, Me.dgvTVShows.SelectedRows(0).Index).Value.ToString, String.Empty, If(String.IsNullOrEmpty(ShowLang), SourceLang, ShowLang), SourceLang, DirectCast(Convert.ToInt32(Me.dgvTVShows.Item(23, Me.dgvTVShows.SelectedRows(0).Index).Value), Enums.Ordering), Master.DefaultTVOptions, Enums.ScrapeType.FullAsk, False)
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub cmnuDeleteSeason_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonRemoveFromDisk.Click
        Try

            Dim SeasonsToDelete As New Dictionary(Of Long, Long)
            Dim ShowId As Long = -1
            Dim SeasonNum As Integer = -1

            For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                ShowId = Convert.ToInt64(sRow.Cells(0).Value)
                SeasonNum = Convert.ToInt32(sRow.Cells(2).Value)
                'seasonnum first... showid can't be key or else only one season will be deleted
                If Not SeasonsToDelete.ContainsKey(SeasonNum) Then
                    SeasonsToDelete.Add(SeasonNum, ShowId)
                End If
            Next

            If SeasonsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(SeasonsToDelete, Enums.DelType.Seasons) = Windows.Forms.DialogResult.OK Then
                        Me.FillSeasons(Convert.ToInt32(Me.dgvTVSeasons.Item(0, Me.currSeasonRow).Value))
                        Me.SetTVCount()
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuDeleteTVEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRemoveFromDisk.Click
        Try

            Dim EpsToDelete As New Dictionary(Of Long, Long)
            Dim EpId As Long = -1

            For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                EpId = Convert.ToInt64(sRow.Cells(0).Value)
                If Not EpsToDelete.ContainsKey(EpId) Then
                    EpsToDelete.Add(EpId, 0)
                End If
            Next

            If EpsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(EpsToDelete, Enums.DelType.Episodes) = Windows.Forms.DialogResult.OK Then
                        Me.FillEpisodes(Convert.ToInt32(Me.dgvTVSeasons.Item(0, Me.currSeasonRow).Value), Convert.ToInt32(Me.dgvTVSeasons.Item(2, Me.currSeasonRow).Value))
                        Me.SetTVCount()
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

    Private Sub cmnuDeleteTVShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRemoveFromDisk.Click
        Try

            Dim ShowsToDelete As New Dictionary(Of Long, Long)
            Dim ShowId As Long = -1

            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                ShowId = Convert.ToInt64(sRow.Cells(0).Value)
                If Not ShowsToDelete.ContainsKey(ShowId) Then
                    ShowsToDelete.Add(ShowId, 0)
                End If
            Next

            If ShowsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(ShowsToDelete, Enums.DelType.Shows) = Windows.Forms.DialogResult.OK Then
                        Me.FillList(False, False, True)
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuEditEpisode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeEdit.Click
        Try
            Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item(0, indX).Value)
            Master.currShow = Master.DB.LoadTVEpFromDB(ID, True)

            Me.SetControlsEnabled(False)

            Using dEditEpisode As New dlgEditEpisode
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEpisode.GenericRunCallBack
                Select Case dEditEpisode.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        If Me.RefreshEpisode(ID) Then
                            Me.FillEpisodes(Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVEp.Season)
                        End If
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEpisode.GenericRunCallBack
            End Using

            Me.SetControlsEnabled(True)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieEdit.Click
        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
        Try
            Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)

            Me.SetControlsEnabled(False)

            Functions.SetScraperMod(Enums.ModType_Movie.All, False, True)

            Using dEditMovie As New dlgEditMovie
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                Select Case dEditMovie.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.RenameMovie, New List(Of Object)(New Object() {False, False, False}), Master.currMovie)
                        Me.SetMovieListItemAfterEdit(ID, indX)
                        If Me.RefreshMovie(ID) Then
                            Me.FillList(True, True, False)
                        Else
                            Me.FillList(False, True, False)
                            Me.SetControlsEnabled(True)
                        End If
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                    Case Windows.Forms.DialogResult.Retry
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                        Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                    Case Windows.Forms.DialogResult.Abort
                        Master.currMovie.RemoveActorThumbs = False
                        Master.currMovie.RemoveBanner = False
                        Master.currMovie.RemoveClearArt = False
                        Master.currMovie.RemoveClearLogo = False
                        Master.currMovie.RemoveDiscArt = False
                        Master.currMovie.RemoveEThumbs = False
                        Master.currMovie.RemoveEFanarts = False
                        Master.currMovie.RemoveFanart = False
                        Master.currMovie.RemoveLandscape = False
                        Master.currMovie.RemovePoster = False
                        Master.currMovie.RemoveTheme = False
                        Master.currMovie.RemoveTrailer = False
                        Functions.SetScraperMod(Enums.ModType_Movie.DoSearch, True)
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True, False)
                        Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                    Case Else
                        If Me.InfoCleared Then
                            Me.LoadMovieInfo(ID, Me.dgvMovies.Item(1, indX).Value.ToString, True, False)
                        Else
                            Me.SetControlsEnabled(True)
                        End If
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuEditShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowEdit.Click
        Try
            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item(0, indX).Value)

            Master.currShow = Master.DB.LoadTVFullShowFromDB(ID)

            Me.SetControlsEnabled(False)

            Using dEditShow As New dlgEditShow

                Select Case dEditShow.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        Me.SetShowListItemAfterEdit(ID, indX)
                        If Me.RefreshShow(ID, False, True, False, False) Then
                            Me.FillList(False, False, True)
                        Else
                            Me.SetControlsEnabled(True)
                        End If
                    Case Else
                        Me.SetControlsEnabled(True)
                End Select

            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuEpOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeOpenFolder.Click
        If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim ePath As String = String.Empty

            If Me.dgvTVEpisodes.SelectedRows.Count > 10 Then
                If Not MsgBox(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvTVEpisodes.SelectedRows.Count), MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then doOpen = False
            End If

            If doOpen Then
                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        SQLCommand.CommandText = String.Concat("SELECT TVEpPath FROM TVEpPaths WHERE ID = ", sRow.Cells(9).Value.ToString, ";")
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
                    Next
                End Using
            End If
        End If
    End Sub

    Private Sub cmnuMovieWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieWatched.Click
        Try
            Dim setWatched As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as not watched, set menu to watched
                    'else they are all watched so set menu to not watched
                    If Not Convert.ToBoolean(sRow.Cells(34).Value) Then
                        setWatched = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parPlaycount As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlaycount", DbType.String, 0, "Playcount")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE movies SET Playcount = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        Dim currPlaycount As String = String.Empty
                        Dim hasWatched As Boolean = False
                        Dim newPlaycount As String = String.Empty

                        currPlaycount = Convert.ToString(sRow.Cells(33).Value)
                        hasWatched = If(Not String.IsNullOrEmpty(currPlaycount) AndAlso Not currPlaycount = "0", True, False)

                        If Me.dgvMovies.SelectedRows.Count > 1 AndAlso setWatched Then
                            newPlaycount = If(Not String.IsNullOrEmpty(currPlaycount) AndAlso Not currPlaycount = "0", currPlaycount, "1")
                        ElseIf Not hasWatched Then
                            newPlaycount = "1"
                        Else
                            newPlaycount = "0"
                        End If

                        parPlaycount.Value = newPlaycount
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(33).Value = newPlaycount
                        sRow.Cells(34).Value = If(Me.dgvMovies.SelectedRows.Count > 1, setWatched, Not hasWatched)
                    Next
                End Using
                SQLtransaction.Commit()

            End Using

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    Me.RefreshMovie(Convert.ToInt64(sRow.Cells(0).Value), True, False, True, True)
                Next
                SQLtransaction.Commit()
            End Using

            Me.LoadMovieInfo(Convert.ToInt32(Me.dgvMovies.Item(0, Me.dgvMovies.CurrentCell.RowIndex).Value), Me.dgvMovies.Item(1, Me.dgvMovies.CurrentCell.RowIndex).Value.ToString, True, False)

            'If Me.chkFilterLock.Checked Then
            '    Me.dgvMovies.ClearSelection()
            '    Me.dgvMovies.CurrentCell = Nothing
            '    If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            'End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuHasWatchedEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeWatched.Click
        Try
            Dim setHasWatched As Boolean = False
            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(24).Value) Then
                        setHasWatched = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parHasWatched As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasWatched", DbType.Boolean, 0, "HasWatched")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE TVEps SET HasWatched = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        parHasWatched.Value = If(Me.dgvTVEpisodes.SelectedRows.Count > 1, setHasWatched, Not Convert.ToBoolean(sRow.Cells(24).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(24).Value = parHasWatched.Value
                    Next
                End Using

                ''now check the status of all episodes in the season so we can update the season mark flag if needed
                'Dim MarkCount As Integer = 0
                'Dim NotMarkCount As Integer = 0
                'For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                '    If Convert.ToBoolean(sRow.Cells(8).Value) Then
                '        MarkCount += 1
                '    Else
                '        NotMarkCount += 1
                '    End If
                'Next

                'If MarkCount = 0 OrElse NotMarkCount = 0 Then
                '    Using SQLSeacommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                '        Dim parSeaMark As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "Mark")
                '        Dim parSeaID As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaID", DbType.Int32, 0, "TVShowID")
                '        Dim parSeason As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                '        SQLSeacommand.CommandText = "UPDATE TVSeason SET Mark = (?) WHERE TVShowID = (?) AND Season = (?);"
                '        If MarkCount = 0 Then
                '            parSeaMark.Value = False
                '        ElseIf NotMarkCount = 0 Then
                '            parSeaMark.Value = True
                '        End If
                '        parSeaID.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(0).Value)
                '        parSeason.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(2).Value)
                '        SQLSeacommand.ExecuteNonQuery()
                '        Me.dgvTVSeasons.SelectedRows(0).Cells(8).Value = parSeaMark.Value
                '    End Using
                'End If

                SQLtransaction.Commit()
            End Using

            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    'Private Sub cmnuHasWatchedSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuHasWatchedSeason.Click
    '    Try
    '        Dim setMark As Boolean = False
    '        If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
    '            For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
    '                If Not Convert.ToBoolean(sRow.Cells(8).Value) Then
    '                    setMark = True
    '                    Exit For
    '                End If
    '            Next
    '        End If

    '        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
    '            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
    '                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "TVShowID")
    '                Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
    '                SQLcommand.CommandText = "UPDATE TVSeason SET mark = (?) WHERE TVShowID = (?) AND Season = (?);"
    '                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
    '                    parMark.Value = If(Me.dgvTVSeasons.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(8).Value))
    '                    parID.Value = sRow.Cells(0).Value
    '                    parSeason.Value = sRow.Cells(2).Value
    '                    SQLcommand.ExecuteNonQuery()
    '                    sRow.Cells(8).Value = parMark.Value

    '                    Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                        Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
    '                        Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "TVShowID")
    '                        Dim parESeason As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parESeason", DbType.Int32, 0, "Season")
    '                        SQLECommand.CommandText = "UPDATE TVEps SET mark = (?) WHERE TVShowID = (?) AND Season = (?);"
    '                        parEMark.Value = parMark.Value
    '                        parEID.Value = parID.Value
    '                        parESeason.Value = parSeason.Value
    '                        SQLECommand.ExecuteNonQuery()

    '                        For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
    '                            eRow.Cells(8).Value = parMark.Value
    '                        Next
    '                    End Using
    '                Next
    '            End Using
    '            SQLtransaction.Commit()
    '        End Using

    '        Me.dgvTVSeasons.Invalidate()
    '        Me.dgvTVEpisodes.Invalidate()

    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    'Private Sub cmnuHasWatchedShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuHasWatchedShow.Click
    '    Try
    '        Dim setMark As Boolean = False
    '        If Me.dgvTVShows.SelectedRows.Count > 1 Then
    '            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
    '                'if any one item is set as unmarked, set menu to mark
    '                'else they are all marked, so set menu to unmark
    '                If Not Convert.ToBoolean(sRow.Cells(6).Value) Then
    '                    setMark = True
    '                    Exit For
    '                End If
    '            Next
    '        End If

    '        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
    '            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
    '                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
    '                SQLcommand.CommandText = "UPDATE TVShows SET mark = (?) WHERE id = (?);"
    '                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
    '                    parMark.Value = If(Me.dgvTVShows.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(6).Value))
    '                    parID.Value = sRow.Cells(0).Value
    '                    SQLcommand.ExecuteNonQuery()
    '                    sRow.Cells(6).Value = parMark.Value

    '                    Using SQLSeaCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                        Dim parSeaMark As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "mark")
    '                        Dim parSeaID As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaID", DbType.Int32, 0, "TVShowID")
    '                        SQLSeaCommand.CommandText = "UPDATE TVSeason SET mark = (?) WHERE TVShowID = (?);"
    '                        parSeaMark.Value = parMark.Value
    '                        parSeaID.Value = parID.Value
    '                        SQLSeaCommand.ExecuteNonQuery()

    '                        For Each eRow As DataGridViewRow In Me.dgvTVSeasons.Rows
    '                            eRow.Cells(8).Value = parMark.Value
    '                        Next
    '                    End Using

    '                    Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                        Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
    '                        Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "TVShowID")
    '                        SQLECommand.CommandText = "UPDATE TVEps SET mark = (?) WHERE TVShowID = (?);"
    '                        parEMark.Value = parMark.Value
    '                        parEID.Value = parID.Value
    '                        SQLECommand.ExecuteNonQuery()

    '                        For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
    '                            eRow.Cells(8).Value = parMark.Value
    '                        Next
    '                    End Using
    '                Next
    '            End Using
    '            SQLtransaction.Commit()
    '        End Using

    '        Me.dgvTVShows.Invalidate()
    '        Me.dgvTVSeasons.Invalidate()
    '        Me.dgvTVEpisodes.Invalidate()

    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    Private Sub cmnuLockEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells(11).Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE TVShows SET lock = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        parLock.Value = If(Me.dgvTVEpisodes.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells(11).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(11).Value = parLock.Value
                    Next
                End Using

                'now check the status of all episodes in the season so we can update the season lock flag if needed
                Dim LockCount As Integer = 0
                Dim NotLockCount As Integer = 0
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                    If Convert.ToBoolean(sRow.Cells(11).Value) Then
                        LockCount += 1
                    Else
                        NotLockCount += 1
                    End If
                Next

                If LockCount = 0 OrElse NotLockCount = 0 Then
                    Using SQLSeacommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSeaLock As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaLock", DbType.Boolean, 0, "lock")
                        Dim parSeaID As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaID", DbType.Int32, 0, "TVShowID")
                        Dim parSeason As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                        SQLSeacommand.CommandText = "UPDATE TVSeason SET lock = (?) WHERE TVShowID = (?) AND Season = (?);"
                        If LockCount = 0 Then
                            parSeaLock.Value = False
                        ElseIf NotLockCount = 0 Then
                            parSeaLock.Value = True
                        End If
                        parSeaID.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(0).Value)
                        parSeason.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(2).Value)
                        SQLSeacommand.ExecuteNonQuery()
                        Me.dgvTVSeasons.SelectedRows(0).Cells(7).Value = parSeaLock.Value
                    End Using
                End If

                SQLtransaction.Commit()
            End Using

            Me.dgvTVEpisodes.Invalidate()
            Me.dgvTVSeasons.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuLockSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    If Not Convert.ToBoolean(sRow.Cells(7).Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "TVShowID")
                    Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                    SQLcommand.CommandText = "UPDATE TVSeason SET Lock = (?) WHERE TVShowID = (?) AND Season = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                        parLock.Value = If(Me.dgvTVSeasons.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells(7).Value))
                        parID.Value = sRow.Cells(0).Value
                        parSeason.Value = sRow.Cells(2).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(7).Value = parLock.Value

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parELock As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parELock", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "TVShowID")
                            Dim parESeason As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parESeason", DbType.Int32, 0, "Season")
                            SQLECommand.CommandText = "UPDATE TVEps SET Lock = (?) WHERE TVShowID = (?) AND Season = (?);"
                            parELock.Value = parLock.Value
                            parEID.Value = parID.Value
                            parESeason.Value = parSeason.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells(11).Value = parLock.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuLockShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvTVShows.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells(10).Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE TVShows SET lock = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                        parLock.Value = If(Me.dgvTVShows.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells(10).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(10).Value = parLock.Value

                        Using SQLSeaCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSeaLock As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaLock", DbType.Boolean, 0, "lock")
                            Dim parSeaID As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaID", DbType.Int32, 0, "TVShowID")
                            SQLSeaCommand.CommandText = "UPDATE TVSeason SET lock = (?) WHERE TVShowID = (?);"
                            parSeaLock.Value = parLock.Value
                            parSeaID.Value = parID.Value
                            SQLSeaCommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVSeasons.Rows
                                eRow.Cells(7).Value = parLock.Value
                            Next
                        End Using

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parELock As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parELock", DbType.Boolean, 0, "lock")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "TVShowID")
                            SQLECommand.CommandText = "UPDATE TVEps SET lock = (?) WHERE TVShowID = (?);"
                            parELock.Value = parLock.Value
                            parEID.Value = parID.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells(11).Value = parLock.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Me.dgvTVShows.Invalidate()
            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells(14).Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE movies SET lock = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parLock.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells(14).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(14).Value = parLock.Value
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If Me.chkFilterLock.Checked Then
                Me.dgvMovies.ClearSelection()
                Me.dgvMovies.CurrentCell = Nothing
                If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMarkEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeMark.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(8).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE TVEps SET mark = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        parMark.Value = If(Me.dgvTVEpisodes.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(8).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(8).Value = parMark.Value
                    Next
                End Using

                'now check the status of all episodes in the season so we can update the season mark flag if needed
                Dim MarkCount As Integer = 0
                Dim NotMarkCount As Integer = 0
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                    If Convert.ToBoolean(sRow.Cells(8).Value) Then
                        MarkCount += 1
                    Else
                        NotMarkCount += 1
                    End If
                Next

                If MarkCount = 0 OrElse NotMarkCount = 0 Then
                    Using SQLSeacommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSeaMark As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "Mark")
                        Dim parSeaID As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaID", DbType.Int32, 0, "TVShowID")
                        Dim parSeason As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                        SQLSeacommand.CommandText = "UPDATE TVSeason SET Mark = (?) WHERE TVShowID = (?) AND Season = (?);"
                        If MarkCount = 0 Then
                            parSeaMark.Value = False
                        ElseIf NotMarkCount = 0 Then
                            parSeaMark.Value = True
                        End If
                        parSeaID.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(0).Value)
                        parSeason.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(2).Value)
                        SQLSeacommand.ExecuteNonQuery()
                        Me.dgvTVSeasons.SelectedRows(0).Cells(8).Value = parSeaMark.Value
                    End Using
                End If

                SQLtransaction.Commit()
            End Using

            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMarkSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonMark.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    If Not Convert.ToBoolean(sRow.Cells(8).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "TVShowID")
                    Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                    SQLcommand.CommandText = "UPDATE TVSeason SET mark = (?) WHERE TVShowID = (?) AND Season = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                        parMark.Value = If(Me.dgvTVSeasons.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(8).Value))
                        parID.Value = sRow.Cells(0).Value
                        parSeason.Value = sRow.Cells(2).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(8).Value = parMark.Value

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "TVShowID")
                            Dim parESeason As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parESeason", DbType.Int32, 0, "Season")
                            SQLECommand.CommandText = "UPDATE TVEps SET mark = (?) WHERE TVShowID = (?) AND Season = (?);"
                            parEMark.Value = parMark.Value
                            parEID.Value = parID.Value
                            parESeason.Value = parSeason.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells(8).Value = parMark.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMarkShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowMark.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvTVShows.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(6).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE TVShows SET mark = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                        parMark.Value = If(Me.dgvTVShows.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(6).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(6).Value = parMark.Value

                        Using SQLSeaCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSeaMark As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "mark")
                            Dim parSeaID As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaID", DbType.Int32, 0, "TVShowID")
                            SQLSeaCommand.CommandText = "UPDATE TVSeason SET mark = (?) WHERE TVShowID = (?);"
                            parSeaMark.Value = parMark.Value
                            parSeaID.Value = parID.Value
                            SQLSeaCommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVSeasons.Rows
                                eRow.Cells(8).Value = parMark.Value
                            Next
                        End Using

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "TVShowID")
                            SQLECommand.CommandText = "UPDATE TVEps SET mark = (?) WHERE TVShowID = (?);"
                            parEMark.Value = parMark.Value
                            parEID.Value = parID.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells(8).Value = parMark.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Me.dgvTVShows.Invalidate()
            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMark.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(11).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                    SQLcommand.CommandText = "UPDATE Movies SET Mark = (?) WHERE ID = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(11).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(11).Value = parMark.Value
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            setMark = False
            For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
                If Convert.ToBoolean(sRow.Cells(11).Value) Then
                    setMark = True
                    Exit For
                End If
            Next
            Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

            If Me.chkFilterMark.Checked Then
                Me.dgvMovies.ClearSelection()
                Me.dgvMovies.CurrentCell = Nothing
                If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieMarkAsCustom1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom1.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(66).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom1", DbType.Boolean, 0, "MarkCustom1")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                    SQLcommand.CommandText = "UPDATE Movies SET MarkCustom1 = (?) WHERE ID = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(66).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(66).Value = parMark.Value
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

            'If Me.chkFilterMark.Checked Then
            '    Me.dgvMovies.ClearSelection()
            '    Me.dgvMovies.CurrentCell = Nothing
            '    If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            'End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieMarkAsCustom2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom2.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(67).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom2", DbType.Boolean, 0, "MarkCustom2")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                    SQLcommand.CommandText = "UPDATE Movies SET MarkCustom2 = (?) WHERE ID = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(67).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(67).Value = parMark.Value
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

            'If Me.chkFilterMark.Checked Then
            '    Me.dgvMovies.ClearSelection()
            '    Me.dgvMovies.CurrentCell = Nothing
            '    If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            'End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieMarkAsCustom3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom3.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(68).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom3", DbType.Boolean, 0, "MarkCustom3")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                    SQLcommand.CommandText = "UPDATE Movies SET MarkCustom3 = (?) WHERE ID = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(68).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(68).Value = parMark.Value
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

            'If Me.chkFilterMark.Checked Then
            '    Me.dgvMovies.ClearSelection()
            '    Me.dgvMovies.CurrentCell = Nothing
            '    If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            'End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieMarkAsCustom4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom4.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells(69).Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom4", DbType.Boolean, 0, "MarkCustom4")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                    SQLcommand.CommandText = "UPDATE Movies SET MarkCustom4 = (?) WHERE ID = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells(69).Value))
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells(69).Value = parMark.Value
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

            'If Me.chkFilterMark.Checked Then
            '    Me.dgvMovies.ClearSelection()
            '    Me.dgvMovies.CurrentCell = Nothing
            '    If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            'End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieEditMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieEditMetaData.Click
        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)
        Using dEditMeta As New dlgFileInfo
            Select Case dEditMeta.ShowDialog(False)
                Case Windows.Forms.DialogResult.OK
                    Me.SetMovieListItemAfterEdit(ID, indX)
                    If Me.RefreshMovie(ID) Then
                        Me.FillList(True, False, False)
                    End If
            End Select
        End Using
    End Sub

    Private Sub cmnuMovieReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReload.Click
        ReloadMovie()
    End Sub

    Private Sub cmnuMovieSetEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetEdit.Click
        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
        Try
            Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item(0, indX).Value)

            Me.SetControlsEnabled(False)

            'Functions.SetScraperMod(Enums.ModType.All, False, True)

            Using dEditMovieSet As New dlgEditMovieSet
                'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
                Select Case dEditMovieSet.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieScraperRDYtoSave, Nothing, Master.currMovie)
                        Me.SetMovieSetListItemAfterEdit(ID, indX)
                        If Me.RefreshMovieSet(ID) Then
                            Me.FillList(False, True, True)
                        Else
                            Me.SetControlsEnabled(True)
                        End If
                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                    Case Windows.Forms.DialogResult.Retry
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                        Me.MovieSetScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieSetOptions)
                    Case Windows.Forms.DialogResult.Abort
                        'Master.currMovie.ClearBanner = False
                        'Master.currMovie.ClearClearArt = False
                        'Master.currMovie.ClearClearLogo = False
                        'Master.currMovie.ClearDiscArt = False
                        'Master.currMovie.ClearEThumbs = False
                        'Master.currMovie.ClearEFanarts = False
                        'Master.currMovie.ClearFanart = False
                        'Master.currMovie.ClearLandscape = False
                        'Master.currMovie.ClearPoster = False
                        'Master.currMovie.ClearTheme = False
                        'Master.currMovie.ClearTrailer = False
                        'Functions.SetScraperMod(Enums.ModType.DoSearch, True)
                        'Functions.SetScraperMod(Enums.ModType.All, True, False)
                        'Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                    Case Else
                        If Me.InfoCleared Then
                            Me.LoadMovieSetInfo(ID, Me.dgvMovieSets.Item(1, indX).Value.ToString, True, False)
                        Else
                            Me.SetControlsEnabled(True)
                        End If
                End Select
                'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieSetNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetNew.Click
        Master.currMovieSet = New Structures.DBMovieSet
        Master.currMovieSet.MovieSet = New MediaContainers.MovieSet
        Master.currMovieSet.ID = -1

        Try
            Me.SetControlsEnabled(False)

            Using dEditMovieSet As New dlgEditMovieSet
                'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
                Select Case dEditMovieSet.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieScraperRDYtoSave, Nothing, Master.currMovie)
                        'Me.SetMovieSetListItemAfterEdit(ID, indX)
                        'If Me.RefreshMovieSet(ID) Then
                        Me.FillList(False, True, False)
                        'Else
                        Me.SetControlsEnabled(True)
                        'End If
                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                    Case Windows.Forms.DialogResult.Retry
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                        Me.MovieSetScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieSetOptions)
                    Case Windows.Forms.DialogResult.Abort
                        'Master.currMovie.ClearBanner = False
                        'Master.currMovie.ClearClearArt = False
                        'Master.currMovie.ClearClearLogo = False
                        'Master.currMovie.ClearDiscArt = False
                        'Master.currMovie.ClearEThumbs = False
                        'Master.currMovie.ClearEFanarts = False
                        'Master.currMovie.ClearFanart = False
                        'Master.currMovie.ClearLandscape = False
                        'Master.currMovie.ClearPoster = False
                        'Master.currMovie.ClearTheme = False
                        'Master.currMovie.ClearTrailer = False
                        'Functions.SetScraperMod(Enums.ModType.DoSearch, True)
                        'Functions.SetScraperMod(Enums.ModType.All, True, False)
                        'Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                    Case Else
                        Me.SetControlsEnabled(True)
                End Select
                'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmnuMovieSetReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetReload.Click
        ReloadMovieSet()
    End Sub

    Private Sub cmnuMovieSetRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetRemove.Click
        Try
            Me.ClearInfo()
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()

                For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                    Master.DB.DeleteMovieSetFromDB(Convert.ToInt64(sRow.Cells(0).Value), True)
                Next

                SQLtransaction.Commit()
            End Using

            Me.FillList(False, True, False)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuReloadEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeReload.Click
        Try
            Me.dgvTVShows.Cursor = Cursors.WaitCursor
            Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
            Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)

            Dim doFill As Boolean = False
            Dim tFill As Boolean = False

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    tFill = Me.RefreshEpisode(Convert.ToInt64(sRow.Cells(0).Value), True)
                    If tFill Then doFill = True
                Next

                Master.DB.CleanSeasons(True)

                SQLtransaction.Commit()
            End Using

            Me.dgvTVShows.Cursor = Cursors.Default
            Me.dgvTVSeasons.Cursor = Cursors.Default
            Me.dgvTVEpisodes.Cursor = Cursors.Default
            Me.SetControlsEnabled(True)

            If doFill Then FillEpisodes(Convert.ToInt32(Me.dgvTVEpisodes.SelectedRows(0).Cells(0).Value), Convert.ToInt32(Me.dgvTVEpisodes.SelectedRows(0).Cells(12).Value))
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuReloadSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonReload.Click
        Me.dgvTVShows.Cursor = Cursors.WaitCursor
        Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
        Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False
        Dim tFill As Boolean = False

        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
            Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows

                    doFill = Me.RefreshSeason(Convert.ToInt32(sRow.Cells(0).Value), Convert.ToInt32(sRow.Cells(2).Value), True)

                    Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Concat("SELECT ID FROM TVEps WHERE TVShowID = ", sRow.Cells(0).Value, " AND Season = ", sRow.Cells(2).Value, " AND Missing = 0;")
                        Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                            While SQLReader.Read
                                tFill = Me.RefreshEpisode(Convert.ToInt64(SQLReader("ID")), True)
                                If tFill Then doFill = True
                            End While
                        End Using
                    End Using
                Next

                Master.DB.CleanSeasons(True)

                SQLTrans.Commit()
            End Using
        End If

        Me.dgvTVShows.Cursor = Cursors.Default
        Me.dgvTVSeasons.Cursor = Cursors.Default
        Me.dgvTVEpisodes.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then Me.FillSeasons(Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(0).Value))
    End Sub

    Private Sub cmnuReloadShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowReload.Click
        Try
            Me.dgvTVShows.Cursor = Cursors.WaitCursor
            Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
            Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)

            Dim doFill As Boolean = False
            Dim tFill As Boolean = False

            If Me.dgvTVShows.SelectedRows.Count > 1 Then
                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                        tFill = Me.RefreshShow(Convert.ToInt64(sRow.Cells(0).Value), True, True, False, True)
                        If tFill Then doFill = True
                    Next
                    SQLtransaction.Commit()
                End Using
            ElseIf Me.dgvTVShows.SelectedRows.Count = 1 Then
                'seperate single refresh so we can have a progress bar
                tFill = Me.RefreshShow(Convert.ToInt64(Me.dgvTVShows.SelectedRows(0).Cells(0).Value), False, True, False, True)
                If tFill Then doFill = True
            End If

            Me.dgvTVShows.Cursor = Cursors.Default
            Me.dgvTVSeasons.Cursor = Cursors.Default
            Me.dgvTVEpisodes.Cursor = Cursors.Default
            Me.SetControlsEnabled(True)

            If doFill Then FillList(False, False, True)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuRemoveSeasonFromDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuRemoveSeasonFromDB.Click
        Me.ClearInfo(False)

        Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                Master.DB.DeleteTVSeasonFromDB(Convert.ToInt32(sRow.Cells(0).Value), Convert.ToInt32(sRow.Cells(2).Value), True)
            Next
            SQLTrans.Commit()
        End Using

        If Me.dgvTVSeasons.RowCount > 0 Then
            Me.FillSeasons(Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells(0).Value))
        End If

        Me.SetTVCount()
    End Sub

    Private Sub cmnuRemoveTVEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRemoveFromDB.Click
        Me.ClearInfo(False)

        Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                Master.DB.DeleteTVEpFromDB(Convert.ToInt32(sRow.Cells(0).Value), True, False, True)
            Next

            Master.DB.CleanSeasons(True)

            SQLTrans.Commit()
        End Using

        Dim cSeas As Integer = 0

        If Not Me.currSeasonRow = -1 Then
            cSeas = Me.currSeasonRow
        End If

        If Me.dgvTVEpisodes.RowCount > 0 Then
            Me.FillEpisodes(Convert.ToInt32(Me.dgvTVSeasons.Item(0, cSeas).Value), Convert.ToInt32(Me.dgvTVSeasons.Item(2, cSeas).Value))
        End If

        Me.SetTVCount()
    End Sub

    Private Sub cmnuRemoveTVShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRemoveFromDB.Click
        Me.ClearInfo()

        Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                Master.DB.DeleteTVShowFromDB(Convert.ToInt32(sRow.Cells(0).Value), True)
            Next
            SQLTrans.Commit()
        End Using

        Me.FillList(False, False, True)
    End Sub

    Private Sub cmnuRescrapeEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRescrape.Click
        Me.SetControlsEnabled(False, True)
        ModulesManager.Instance.TVScrapeEpisode(Convert.ToInt32(Me.dgvTVEpisodes.Item(1, Me.dgvTVEpisodes.SelectedRows(0).Index).Value), Me.tmpTitle, Me.tmpTVDB, Convert.ToInt32(Me.dgvTVEpisodes.Item(2, Me.dgvTVEpisodes.SelectedRows(0).Index).Value), Convert.ToInt32(Me.dgvTVEpisodes.Item(12, Me.dgvTVEpisodes.SelectedRows(0).Index).Value), Me.tmpLang, Me.tmpOrdering, Master.DefaultTVOptions)
    End Sub

    Private Sub cmnuShowRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRefresh.Click
        Me.SetControlsEnabled(False, True)
        TVShowRefreshData()
    End Sub

    Private Sub cmnuShowRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRescrape.Click
        Me.SetControlsEnabled(False, True)
        TVShowScrapeData()
    End Sub

    Sub TVShowRefreshData()
        Me.SetControlsEnabled(False)
        For Each s As DataGridViewRow In Me.dgvTVShows.SelectedRows
            ' Temporary Scrapetype
            Dim ScrapeType As Enums.ScrapeType
            'If Me.dgvTVShows.SelectedRows.Count = 1 Then
            'ScrapeType = Enums.ScrapeType.FullAsk
            'Else
            'ScrapeType = Enums.ScrapeType.FullAuto
            'End If
            ScrapeType = Enums.ScrapeType.FullAuto
            Dim ShowLang As String = Me.dgvTVShows.Item(22, s.Index).Value.ToString
            Dim SourceLang As String = Master.DB.GetTVSourceLanguage(Me.dgvTVShows.Item(8, s.Index).Value.ToString)
            ModulesManager.Instance.TVScrapeOnly(Convert.ToInt32(Me.dgvTVShows.Item(0, s.Index).Value), Me.dgvTVShows.Item(1, s.Index).Value.ToString, Me.dgvTVShows.Item(9, s.Index).Value.ToString, If(String.IsNullOrEmpty(ShowLang), SourceLang, ShowLang), SourceLang, DirectCast(Convert.ToInt32(Me.dgvTVShows.Item(23, s.Index).Value), Enums.Ordering), Master.DefaultTVOptions, ScrapeType, True)
        Next
        Me.SetControlsEnabled(True)
    End Sub

    Sub TVShowScrapeData()
        Me.SetControlsEnabled(False)
        For Each s As DataGridViewRow In Me.dgvTVShows.SelectedRows
            ' Temporary Scrapetype
            Dim ScrapeType As Enums.ScrapeType
            'If Me.dgvTVShows.SelectedRows.Count = 1 Then
            'ScrapeType = Enums.ScrapeType.FullAsk
            'Else
            'ScrapeType = Enums.ScrapeType.FullAuto
            'End If
            ScrapeType = Enums.ScrapeType.FullAsk
            Dim ShowLang As String = Me.dgvTVShows.Item(22, s.Index).Value.ToString
            Dim SourceLang As String = Master.DB.GetTVSourceLanguage(Me.dgvTVShows.Item(8, s.Index).Value.ToString)
            ModulesManager.Instance.TVScrapeOnly(Convert.ToInt32(Me.dgvTVShows.Item(0, s.Index).Value), Me.dgvTVShows.Item(1, s.Index).Value.ToString, Me.dgvTVShows.Item(9, s.Index).Value.ToString, If(String.IsNullOrEmpty(ShowLang), SourceLang, ShowLang), SourceLang, DirectCast(Convert.ToInt32(Me.dgvTVShows.Item(23, s.Index).Value), Enums.Ordering), Master.DefaultTVOptions, ScrapeType, True)
        Next
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub cmnuMovieRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRescrape.Click
        If Me.dgvMovies.SelectedRows.Count = 1 Then
            Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
            Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
        End If
    End Sub

    Private Sub cmnuMovieSetRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetRescrape.Click
        If Me.dgvMovieSets.SelectedRows.Count = 1 Then
            Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
            Me.MovieSetScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieSetOptions)
        End If
    End Sub
    ''' <summary>
    ''' User has selected "Change Movie" from the context menu. This will re-validate the movie title with the user,
    ''' and initiate a new scrape of the movie.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmnuMovieChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieChange.Click
        If Me.dgvMovies.SelectedRows.Count <> 1 Then Return 'This method is only valid for when exactly one movie is selected
        Functions.SetScraperMod(Enums.ModType_Movie.DoSearch, True)
        Functions.SetScraperMod(Enums.ModType_Movie.All, True, False)
        Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuSeasonChangeImages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonChangeImages.Click
        Me.SetControlsEnabled(False)
        Using dEditSeason As New dlgEditSeason
            If dEditSeason.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If Me.RefreshSeason(Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVEp.Season, False) Then
                    Me.FillSeasons(Convert.ToInt32(Master.currShow.ShowID))
                End If
            End If
        End Using
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub cmnuSeasonOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonOpenFolder.Click
        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim SeasonPath As String = String.Empty

            If Me.dgvTVSeasons.SelectedRows.Count > 10 Then
                If Not MsgBox(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvTVSeasons.SelectedRows.Count), MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    SeasonPath = Functions.GetSeasonDirectoryFromShowPath(Master.currShow.ShowPath, Convert.ToInt32(sRow.Cells(2).Value))

                    Using Explorer As New Diagnostics.Process
                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            If String.IsNullOrEmpty(SeasonPath) Then
                                Explorer.StartInfo.Arguments = String.Format("/root,""{0}""", Master.currShow.ShowPath)
                            Else
                                Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", SeasonPath)
                            End If

                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            If String.IsNullOrEmpty(SeasonPath) Then
                                Explorer.StartInfo.Arguments = String.Format("""{0}""", Master.currShow.ShowPath)
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

    Private Sub cmnuSeasonRescrape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonRescrape.Click
        Me.SetControlsEnabled(False, True)
        ModulesManager.Instance.TVScrapeSeason(Convert.ToInt32(Me.dgvTVSeasons.Item(0, Me.dgvTVSeasons.SelectedRows(0).Index).Value), Me.tmpTitle, Me.tmpTVDB, Convert.ToInt32(Me.dgvTVSeasons.Item(2, Me.dgvTVSeasons.SelectedRows(0).Index).Value), Me.tmpLang, Me.tmpOrdering, Master.DefaultTVOptions)
    End Sub

    Private Sub mnuMainToolsSortFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsSortFiles.Click, cmnuTrayToolsSortFiles.Click
        Me.SetControlsEnabled(False)
        Using dSortFiles As New dlgSortFiles
            If dSortFiles.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.LoadMedia(New Structures.Scans With {.Movies = True})
            Else
                Me.SetControlsEnabled(True)
            End If
        End Using
    End Sub

    Private Sub mnuMainToolsBackdrops_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsBackdrops.Click, cmnuTrayToolsBackdrops.Click
        Me.NonScrape(Enums.ScrapeType.CopyBackdrops, Nothing)
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
                ReDim Preserve Me.pnlGenre(i)
                ReDim Preserve Me.pbGenre(i)
                Me.pnlGenre(i) = New Panel()
                Me.pnlGenre(i).Visible = False
                Me.pbGenre(i) = New PictureBox()
                Me.pbGenre(i).Name = genres(i).Trim.ToUpper
                Me.pnlGenre(i).Size = New Size(68, 100)
                Me.pbGenre(i).Size = New Size(62, 94)
                Me.pnlGenre(i).BackColor = Me.GenrePanelColor
                Me.pbGenre(i).BackColor = Me.GenrePanelColor
                Me.pnlGenre(i).BorderStyle = BorderStyle.FixedSingle
                Me.pbGenre(i).SizeMode = PictureBoxSizeMode.StretchImage
                Me.pbGenre(i).Image = APIXML.GetGenreImage(genres(i).Trim)
                Me.pnlGenre(i).Left = ((Me.pnlInfoPanel.Right) - (i * 73)) - 73
                Me.pbGenre(i).Left = 2
                Me.pnlGenre(i).Top = Me.pnlInfoPanel.Top - 105
                Me.pbGenre(i).Top = 2
                Me.scMain.Panel2.Controls.Add(Me.pnlGenre(i))
                Me.pnlGenre(i).Controls.Add(Me.pbGenre(i))
                Me.pnlGenre(i).BringToFront()
                AddHandler Me.pbGenre(i).MouseEnter, AddressOf pbGenre_MouseEnter
                AddHandler Me.pbGenre(i).MouseLeave, AddressOf pbGenre_MouseLeave
                If Master.eSettings.GeneralShowGenresText Then
                    pbGenre(i).Image = ImageUtils.AddGenreString(pbGenre(i).Image, pbGenre(i).Name)
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
    End Sub
    'Private Sub createGenreThumbs(ByVal strGenres As String)
    '    '//
    '    ' Parse the genre string and create panels/pictureboxes for each one
    '    '\\

    '    Dim genreArray() As String

    '    Try
    '        genreArray = Microsoft.VisualBasic.Strings.Split(strGenres, " / ")
    '        For i As Integer = 0 To UBound(genreArray)
    '            ReDim Preserve Me.pnlGenre(i)
    '            ReDim Preserve Me.pbGenre(i)
    '            Me.pnlGenre(i) = New Panel()
    '            Me.pnlGenre(i).Visible = False
    '            Me.pbGenre(i) = New PictureBox()
    '            Me.pbGenre(i).Name = genreArray(i).Trim.ToUpper
    '            Me.pnlGenre(i).Size = New Size(68, 100)
    '            Me.pbGenre(i).Size = New Size(62, 94)
    '            Me.pnlGenre(i).BackColor = Me.GenrePanelColor
    '            Me.pbGenre(i).BackColor = Me.GenrePanelColor
    '            Me.pnlGenre(i).BorderStyle = BorderStyle.FixedSingle
    '            Me.pbGenre(i).SizeMode = PictureBoxSizeMode.StretchImage
    '            Me.pbGenre(i).Image = APIXML.GetGenreImage(genreArray(i).Trim)
    '            Me.pnlGenre(i).Left = ((Me.pnlInfoPanel.Right) - (i * 73)) - 73
    '            Me.pbGenre(i).Left = 2
    '            Me.pnlGenre(i).Top = Me.pnlInfoPanel.Top - 105
    '            Me.pbGenre(i).Top = 2
    '            Me.scMain.Panel2.Controls.Add(Me.pnlGenre(i))
    '            Me.pnlGenre(i).Controls.Add(Me.pbGenre(i))
    '            Me.pnlGenre(i).BringToFront()
    '            AddHandler Me.pbGenre(i).MouseEnter, AddressOf pbGenre_MouseEnter
    '            AddHandler Me.pbGenre(i).MouseLeave, AddressOf pbGenre_MouseLeave
    '            If Master.eSettings.AllwaysDisplayGenresText Then
    '                pbGenre(i).Image = ImageUtils.AddGenreString(pbGenre(i).Image, pbGenre(i).Name)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    Private Sub CustomUpdaterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustom.Click, cmnuTrayCustom.Click
        Me.SetControlsEnabled(False)
        Using dUpdate As New dlgUpdateMedia
            Dim CustomUpdater As Structures.CustomUpdaterStruct = Nothing
            CustomUpdater = dUpdate.ShowDialog()
            If Not CustomUpdater.Canceled Then
                Me.MovieScrapeData(False, CustomUpdater.ScrapeType, CustomUpdater.Options)
            Else
                Me.SetControlsEnabled(True)
            End If
        End Using
    End Sub

    Private Sub RestartUpdaterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRestartScrape.Click, cmnuTrayRestart.Click
        Me.MovieScrapeData(False, Nothing, Nothing, True)
    End Sub


    Private Sub cmnuMovieRemoveFromDisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRemoveFromDisc.Click
        Try
            Dim MoviesToDelete As New Dictionary(Of Long, Long)
            Dim MovieId As Int64 = -1

            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                MovieId = Convert.ToInt64(sRow.Cells(0).Value)
                If Not MoviesToDelete.ContainsKey(MovieId) Then
                    MoviesToDelete.Add(MovieId, 0)
                End If
            Next

            If MoviesToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(MoviesToDelete, Enums.DelType.Movies) = Windows.Forms.DialogResult.OK Then
                        Me.FillList(True, True, False)
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellClick
        Try

            If e.ColumnIndex = 3 OrElse e.ColumnIndex = 34 OrElse Not Master.eSettings.MovieClickScrape Then 'Title
                If Not e.ColumnIndex = 34 Then
                    If Me.dgvMovies.SelectedRows.Count > 0 Then
                        If Me.dgvMovies.RowCount > 0 Then
                            If Me.dgvMovies.SelectedRows.Count > 1 Then
                                Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovies.SelectedRows.Count))
                            ElseIf Me.dgvMovies.SelectedRows.Count = 1 Then
                                Me.SetStatus(Me.dgvMovies.SelectedRows(0).Cells(1).Value.ToString)
                            End If
                        End If
                        Me.currMovieRow = Me.dgvMovies.SelectedRows(0).Index
                    End If
                Else
                    'TODO: maybe we can merge this to one sub/function together with "Private Sub cmnuMovieWatched_Click"
                    Try
                        Dim setWatched As Boolean = False
                        If Me.dgvMovies.SelectedRows.Count > 1 Then
                            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                                'if any one item is set as not watched, set menu to watched
                                'else they are all watched so set menu to not watched
                                If Not Convert.ToBoolean(sRow.Cells(34).Value) Then
                                    setWatched = True
                                    Exit For
                                End If
                            Next
                        End If

                        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                Dim parPlaycount As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlaycount", DbType.String, 0, "Playcount")
                                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                                SQLcommand.CommandText = "UPDATE movies SET Playcount = (?) WHERE id = (?);"
                                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                                    Dim currPlaycount As String = String.Empty
                                    Dim hasWatched As Boolean = False
                                    Dim newPlaycount As String = String.Empty

                                    currPlaycount = Convert.ToString(sRow.Cells(33).Value)
                                    hasWatched = If(Not String.IsNullOrEmpty(currPlaycount) AndAlso Not currPlaycount = "0", True, False)

                                    If Me.dgvMovies.SelectedRows.Count > 1 AndAlso setWatched Then
                                        newPlaycount = If(Not String.IsNullOrEmpty(currPlaycount) AndAlso Not currPlaycount = "0", currPlaycount, "1")
                                    ElseIf Not hasWatched Then
                                        newPlaycount = "1"
                                    Else
                                        newPlaycount = "0"
                                    End If

                                    parPlaycount.Value = newPlaycount
                                    parID.Value = sRow.Cells(0).Value
                                    SQLcommand.ExecuteNonQuery()
                                    sRow.Cells(33).Value = newPlaycount
                                    sRow.Cells(34).Value = If(Me.dgvMovies.SelectedRows.Count > 1, setWatched, Not hasWatched)
                                Next
                            End Using
                            SQLtransaction.Commit()

                        End Using

                        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                                Me.RefreshMovie(Convert.ToInt64(sRow.Cells(0).Value), True, False, True, True)
                            Next
                            SQLtransaction.Commit()
                        End Using

                        Me.LoadMovieInfo(Convert.ToInt32(Me.dgvMovies.Item(0, Me.dgvMovies.CurrentCell.RowIndex).Value), Me.dgvMovies.Item(1, Me.dgvMovies.CurrentCell.RowIndex).Value.ToString, True, False)
                        Me.dgvMovies.Invalidate()

                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name, ex)
                    End Try
                End If
            ElseIf Master.eSettings.MovieClickScrape AndAlso e.RowIndex >= 0 AndAlso e.ColumnIndex <> 8 AndAlso e.ColumnIndex <> 62 AndAlso Not bwMovieScraper.IsBusy Then
                Dim movie As Int32 = CType(Me.dgvMovies.Rows(e.RowIndex).Cells(0).Value, Int32)
                Dim objCell As DataGridViewCell = CType(Me.dgvMovies.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

                'EMM not able to scrape subtitles yet.
                'So don't set status for it, but leave the option open for the future.
                Me.dgvMovies.ClearSelection()
                Me.dgvMovies.Rows(objCell.RowIndex).Selected = True
                Me.currMovieRow = objCell.RowIndex
                Select Case e.ColumnIndex
                    Case 4 'Poster
                        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
                    Case 5 'Fanart
                        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
                    Case 6 'Nfo
                        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
                    Case 7 'Trailer
                        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
                    Case 8 'Subtitles
                        'Functions.SetScraperMod(Enums.ModType.Subtitles, True)
                    Case 9 'Extrathumbs
                        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
                    Case 10 'Metadata - need to add this column to the view.
                        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
                    Case 49 'Extrafanart
                        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
                    Case 51 'Banner
                        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
                    Case 53 'Landscape
                        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
                    Case 55 'Theme
                        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
                    Case 57 'DiscArt
                        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
                    Case 59 'ClearLogo
                        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
                    Case 61 'ClearArt
                        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
                End Select
                If Master.eSettings.MovieClickScrapeAsk Then
                    MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
                Else
                    MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellDoubleClick
        Try

            If e.RowIndex < 0 Then Exit Sub

            If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)
            Master.currMovie = Master.DB.LoadMovieFromDB(ID)

            Functions.SetScraperMod(Enums.ModType_Movie.All, False, True)

            Using dEditMovie As New dlgEditMovie
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                Select Case dEditMovie.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.RenameMovie, New List(Of Object)(New Object() {False, False, False}), Master.currMovie)
                        Me.SetMovieListItemAfterEdit(ID, indX)
                        If Me.RefreshMovie(ID) Then
                            Me.FillList(True, True, False)
                        Else
                            Me.FillList(False, True, False)
                        End If
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                    Case Windows.Forms.DialogResult.Retry
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                        Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                    Case Windows.Forms.DialogResult.Abort
                        Functions.SetScraperMod(Enums.ModType_Movie.DoSearch, True)
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True, False)
                        Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                    Case Else
                        If Me.InfoCleared Then Me.LoadMovieInfo(ID, Me.dgvMovies.Item(1, indX).Value.ToString, True, False)
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellEnter
        Try
            If Not Me.tcMain.SelectedIndex = 0 Then Return

            Me.tmrWaitShow.Stop()
            Me.tmrWaitSeason.Stop()
            Me.tmrWaitEp.Stop()
            Me.tmrWaitMovieSet.Stop()
            Me.tmrWaitMovie.Stop()
            Me.tmrLoadShow.Stop()
            Me.tmrLoadSeason.Stop()
            Me.tmrLoadEp.Stop()
            Me.tmrLoadMovieSet.Stop()
            Me.tmrLoadMovie.Stop()

            Me.currMovieRow = e.RowIndex
            Me.tmrWaitMovie.Start()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_CellMouseDown(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMovies.CellMouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvMovies.RowCount > 0 Then
                If bwCleanDB.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwNonScrape.IsBusy Then
                    Me.cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                    Return
                End If

                Me.cmnuMovie.Enabled = False


                If e.RowIndex >= 0 AndAlso dgvMovies.SelectedRows.Count > 0 Then

                    Me.cmnuMovie.Enabled = True
                    Me.cmnuMovieChange.Visible = False
                    Me.cmnuMovieEdit.Visible = False
                    Me.cmnuMovieEditMetaData.Visible = False
                    Me.cmnuMovieReSel.Visible = True
                    Me.cmnuMovieRescrape.Visible = False
                    Me.cmnuMovieUpSel.Visible = True
                    'Me.cmuRenamer.Visible = False
                    Me.cmnuSep2.Visible = False

                    If Me.dgvMovies.SelectedRows.Count > 1 AndAlso Me.dgvMovies.Rows(e.RowIndex).Selected Then
                        Dim setMark As Boolean = False
                        Dim setLock As Boolean = False
                        Dim setWatched As Boolean = False

                        Me.cmnuMovieTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                        For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                            'if any one item is set as unmarked, set menu to mark
                            'else they are all marked, so set menu to unmark
                            If Not Convert.ToBoolean(sRow.Cells(11).Value) Then
                                setMark = True
                                If setLock AndAlso setWatched Then Exit For
                            End If
                            'if any one item is set as unlocked, set menu to lock
                            'else they are all locked so set menu to unlock
                            If Not Convert.ToBoolean(sRow.Cells(14).Value) Then
                                setLock = True
                                If setMark AndAlso setWatched Then Exit For
                            End If
                            'if any one item is set as unwatched, set menu to watched
                            'else they are all watched so set menu to not watched
                            If Not Convert.ToBoolean(sRow.Cells(34).Value) Then
                                setWatched = True
                                If setLock AndAlso setMark Then Exit For
                            End If
                        Next

                        Me.cmnuMovieMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                        Me.cmnuMovieLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                        Me.cmnuMovieWatched.Text = If(setWatched, Master.eLang.GetString(981, "Watched"), Master.eLang.GetString(980, "Not Watched"))

                        Me.cmnuMovieGenresGenre.Items.Insert(0, Master.eLang.GetString(98, "Select Genre..."))
                        Me.cmnuMovieGenresGenre.SelectedItem = Master.eLang.GetString(98, "Select Genre...")
                        Me.cmnuMovieGenresAdd.Enabled = False
                        Me.cmnuMovieGenresSet.Enabled = False
                        Me.cmnuMovieGenresRemove.Enabled = False
                    Else
                        Me.cmnuMovieChange.Visible = True
                        Me.cmnuMovieEdit.Visible = True
                        Me.cmnuMovieEditMetaData.Visible = True
                        Me.cmnuMovieReSel.Visible = True
                        Me.cmnuMovieRescrape.Visible = True
                        Me.cmnuMovieUpSel.Visible = True
                        Me.cmnuSep.Visible = True
                        Me.cmnuSep2.Visible = True

                        cmnuMovieTitle.Text = String.Concat(">> ", Me.dgvMovies.Item(3, e.RowIndex).Value, " <<")

                        If Not Me.dgvMovies.Rows(e.RowIndex).Selected Then
                            Me.prevMovieRow = -1
                            Me.dgvMovies.CurrentCell = Nothing
                            Me.dgvMovies.ClearSelection()
                            Me.dgvMovies.Rows(e.RowIndex).Selected = True
                            Me.dgvMovies.CurrentCell = Me.dgvMovies.Item(3, e.RowIndex)
                        Else
                            Me.cmnuMovie.Enabled = True
                        End If

                        Me.cmnuMovieMark.Text = If(Convert.ToBoolean(Me.dgvMovies.Item(11, e.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                        Me.cmnuMovieLock.Text = If(Convert.ToBoolean(Me.dgvMovies.Item(14, e.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                        Me.cmnuMovieWatched.Text = If(Convert.ToBoolean(Me.dgvMovies.Item(34, e.RowIndex).Value), Master.eLang.GetString(980, "Not Watched"), Master.eLang.GetString(981, "Watched"))

                        Me.cmnuMovieGenresGenre.Tag = Me.dgvMovies.Item(27, e.RowIndex).Value
                        Me.cmnuMovieGenresGenre.Items.Insert(0, Master.eLang.GetString(98, "Select Genre..."))
                        Me.cmnuMovieGenresGenre.SelectedItem = Master.eLang.GetString(98, "Select Genre...")
                        Me.cmnuMovieGenresAdd.Enabled = False
                        Me.cmnuMovieGenresSet.Enabled = False
                        Me.cmnuMovieGenresRemove.Enabled = False
                    End If
                Else
                    Me.cmnuMovie.Enabled = False
                    Me.cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub


    Private Sub dgvMovies_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellMouseEnter
        'EMM not able to scrape subtitles yet.
        'So don't set status for it, but leave the option open for the future.
        If Master.eSettings.MovieClickScrape AndAlso e.RowIndex > 0 AndAlso e.ColumnIndex > 3 AndAlso e.ColumnIndex < 11 AndAlso e.ColumnIndex <> 8 AndAlso Not bwMovieScraper.IsBusy Then
            oldStatus = GetStatus()
            Dim movieName As String = Me.dgvMovies.Rows(e.RowIndex).Cells(15).Value.ToString
            Dim scrapeFor As String = ""
            Dim scrapeType As String = ""
            Select Case e.ColumnIndex
                Case 4
                    scrapeFor = Master.eLang.GetString(72, "Poster Only")
                Case 5
                    scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                Case 6
                    scrapeFor = Master.eLang.GetString(71, "NFO Only")
                Case 7
                    scrapeFor = Master.eLang.GetString(75, "Trailer Only")
                Case 8
                    'scrapeFor = Master.eLang.GetString(00, "Subtitles")
                Case 9
                    scrapeFor = Master.eLang.GetString(74, "Extrathumbs Only")
                Case 10
                    scrapeFor = Master.eLang.GetString(76, "Meta Data Only")
            End Select
            If Master.eSettings.MovieClickScrapeAsk Then
                scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
            Else
                scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
            End If
            Me.SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", movieName, scrapeFor, scrapeType))
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvMovies_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then Me.SetStatus(oldStatus)
    End Sub

    Private Sub dgvMovies_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovies.CellPainting
        Try

            If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvMovies.Item(e.ColumnIndex, e.RowIndex).Displayed Then
                e.Handled = True
                Return
            End If

            'icons
            If e.ColumnIndex >= 4 AndAlso e.ColumnIndex <= 61 AndAlso e.RowIndex = -1 Then
                e.PaintBackground(e.ClipBounds, False)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = 3
                If e.ColumnIndex = 34 Then 'Watched
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 8)
                ElseIf e.ColumnIndex = 49 Then 'Extrafanarts
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 9)
                ElseIf e.ColumnIndex = 51 Then 'Banner
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
                ElseIf e.ColumnIndex = 53 Then 'Landscape
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
                ElseIf e.ColumnIndex = 55 Then 'Theme
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 12)
                ElseIf e.ColumnIndex = 57 Then 'DiscArt
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 13)
                ElseIf e.ColumnIndex = 59 Then 'ClearLogo
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 14)
                ElseIf e.ColumnIndex = 61 Then 'ClearArt
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 15)
                Else 'Poster/Fanart/NFO/Trailer/Subs/Extrathumbs
                    Me.ilColumnIcons.Draw(e.Graphics, pt, e.ColumnIndex - 4)
                End If

                e.Handled = True

            End If

            'text
            If e.ColumnIndex = 3 AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(Me.dgvMovies.Item(11, e.RowIndex).Value) Then                  'is marked
                    e.CellStyle.ForeColor = Color.Crimson
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Crimson
                ElseIf Convert.ToBoolean(Me.dgvMovies.Item(10, e.RowIndex).Value) Then              'is new
                    e.CellStyle.ForeColor = Color.Green
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Green
                ElseIf Convert.ToBoolean(Me.dgvMovies.Item(66, e.RowIndex).Value) Then              'is marked custom 1
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                ElseIf Convert.ToBoolean(Me.dgvMovies.Item(67, e.RowIndex).Value) Then              'is marked custom 2
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                ElseIf Convert.ToBoolean(Me.dgvMovies.Item(68, e.RowIndex).Value) Then              'is marked custom 3
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                ElseIf Convert.ToBoolean(Me.dgvMovies.Item(69, e.RowIndex).Value) Then              'is marked custom 4
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                Else
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                    e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
                End If
            End If

            If e.ColumnIndex >= 3 AndAlso e.ColumnIndex <= 61 AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(Me.dgvMovies.Item(14, e.RowIndex).Value) Then                  'is locked
                    e.CellStyle.BackColor = Color.LightSteelBlue
                    e.CellStyle.SelectionBackColor = Color.DarkTurquoise
                ElseIf Convert.ToBoolean(Me.dgvMovies.Item(44, e.RowIndex).Value) Then              'out of tolerance
                    e.CellStyle.BackColor = Color.MistyRose
                    e.CellStyle.SelectionBackColor = Color.DarkMagenta
                Else
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
                End If

                If e.ColumnIndex >= 4 AndAlso e.ColumnIndex <= 61 Then
                    e.PaintBackground(e.ClipBounds, True)

                    Dim pt As Point = e.CellBounds.Location
                    Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                    pt.X += offset
                    pt.Y = e.CellBounds.Top + 3
                    Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 6, 7))
                    e.Handled = True
                End If
            End If

            Me.tpMovies.Text = String.Format("{0} ({1})", Master.eLang.GetString(36, "Movies"), Me.dgvMovies.RowCount)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovies.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearch.Focus()
    End Sub

    Private Sub dgvMovies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvMovies.KeyPress
        Try
            If StringUtils.AlphaNumericOnly(e.KeyChar) Then
                KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
                tmrKeyBuffer.Start()
                For Each drvRow As DataGridViewRow In Me.dgvMovies.Rows
                    If drvRow.Cells(3).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                        drvRow.Selected = True
                        Me.dgvMovies.CurrentCell = drvRow.Cells(3)

                        Exit For
                    End If
                Next
            ElseIf e.KeyChar = Chr(13) Then
                If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse _
                Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwRefreshMovies.IsBusy _
                OrElse Me.bwCleanDB.IsBusy Then Return

                Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)
                Master.currMovie = Master.DB.LoadMovieFromDB(ID)
                Me.SetStatus(Master.currMovie.Filename)

                Using dEditMovie As New dlgEditMovie
                    AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                    Select Case dEditMovie.ShowDialog()
                        Case Windows.Forms.DialogResult.OK
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.RenameMovie, New List(Of Object)(New Object() {False, False, False}), Master.currMovie)
                            Me.SetMovieListItemAfterEdit(ID, indX)
                            If Me.RefreshMovie(ID) Then
                                Me.FillList(True, True, False)
                            Else
                                Me.FillList(False, True, False)
                            End If
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                        Case Windows.Forms.DialogResult.Retry
                            Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                            Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                        Case Windows.Forms.DialogResult.Abort
                            Functions.SetScraperMod(Enums.ModType_Movie.DoSearch, True)
                            Functions.SetScraperMod(Enums.ModType_Movie.All, True, False)
                            Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                        Case Else
                            If Me.InfoCleared Then Me.LoadMovieInfo(ID, Me.dgvMovies.Item(1, indX).Value.ToString, True, False)
                    End Select
                    RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                End Using

            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovies.Resize
        ResizeMoviesList()
    End Sub

    Private Sub dgvMovies_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovies.Sorted
        Me.prevMovieRow = -1
        If Me.dgvMovies.RowCount > 0 Then
            Me.dgvMovies.CurrentCell = Nothing
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.Rows(0).Selected = True
            Me.dgvMovies.CurrentCell = Me.dgvMovies.Rows(0).Cells(3)
        End If
    End Sub

    Private Sub dgvMovieSets_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellClick
        Try

            If e.ColumnIndex = 1 OrElse Not Master.eSettings.MovieClickScrape Then 'Title
                If Me.dgvMovieSets.SelectedRows.Count > 0 Then
                    If Me.dgvMovieSets.RowCount > 0 Then
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then
                            Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovieSets.SelectedRows.Count))
                        ElseIf Me.dgvMovieSets.SelectedRows.Count = 1 Then
                            Me.SetStatus(Me.dgvMovieSets.SelectedRows(0).Cells(1).Value.ToString)
                        End If
                    End If
                    Me.currMovieSetRow = Me.dgvMovieSets.SelectedRows(0).Index
                End If
            ElseIf Master.eSettings.MovieClickScrape AndAlso e.RowIndex >= 1 AndAlso e.ColumnIndex <= 16 AndAlso Not bwMovieScraper.IsBusy Then
                Dim movieset As Int32 = CType(Me.dgvMovieSets.Rows(e.RowIndex).Cells(0).Value, Int32)
                Dim objCell As DataGridViewCell = CType(Me.dgvMovieSets.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

                Me.dgvMovieSets.ClearSelection()
                Me.dgvMovieSets.Rows(objCell.RowIndex).Selected = True
                Me.currMovieSetRow = objCell.RowIndex
                Select Case e.ColumnIndex
                    Case 2 'Nfo
                        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
                    Case 4 'Poster
                        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
                    Case 6 'Fanart
                        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
                    Case 8 'Banner
                        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
                    Case 10 'Landscape
                        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
                    Case 12 'DiscArt
                        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
                    Case 14 'ClearLogo
                        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
                    Case 16 'ClearArt
                        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
                End Select
                If Master.eSettings.MovieClickScrapeAsk Then
                    MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
                Else
                    MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovieSets_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellDoubleClick
        Try

            If e.RowIndex < 0 Then Exit Sub

            If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item(0, indX).Value)
            Master.currMovieSet = Master.DB.LoadMovieSetFromDB(ID)

            Functions.SetScraperMod(Enums.ModType_Movie.All, False, True)

            Using dEditMovieSet As New dlgEditMovieSet
                'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
                Select Case dEditMovieSet.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieScraperRDYtoSave, Nothing, Master.currMovieSet)
                        Me.SetMovieSetListItemAfterEdit(ID, indX)
                        If Me.RefreshMovieSet(ID) Then
                            Me.FillList(False, True, False)
                        End If
                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                    Case Windows.Forms.DialogResult.Retry
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                        Me.MovieSetScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieSetOptions)
                    Case Windows.Forms.DialogResult.Abort
                        'Functions.SetScraperMod(Enums.MovieModType.DoSearch, True)
                        'Functions.SetScraperMod(Enums.MovieModType.All, True, False)
                        'Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                    Case Else
                        If Me.InfoCleared Then Me.LoadMovieSetInfo(ID, Me.dgvMovieSets.Item(1, indX).Value.ToString, True, False)
                End Select
                'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovieSets_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellEnter
        Try
            If Not Me.tcMain.SelectedIndex = 1 Then Return

            Me.tmrWaitShow.Stop()
            Me.tmrWaitSeason.Stop()
            Me.tmrWaitEp.Stop()
            Me.tmrWaitMovie.Stop()
            Me.tmrWaitMovieSet.Stop()
            Me.tmrLoadShow.Stop()
            Me.tmrLoadSeason.Stop()
            Me.tmrLoadEp.Stop()
            Me.tmrLoadMovie.Stop()
            Me.tmrLoadMovieSet.Stop()

            Me.currMovieSetRow = e.RowIndex
            Me.tmrWaitMovieSet.Start()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovieSets_CellMouseDown(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMovieSets.CellMouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvMovieSets.RowCount > 0 Then
                If bwCleanDB.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwNonScrape.IsBusy Then
                    Me.cmnuMovieSetTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                    Return
                End If

                Me.cmnuMovieSet.Enabled = False


                If e.RowIndex >= 0 AndAlso dgvMovieSets.SelectedRows.Count > 0 Then

                    Me.cmnuMovieSet.Enabled = True
                    Me.cmnuMovieSetReload.Visible = True
                    Me.cmnuMovieSetSep1.Visible = True

                    If Me.dgvMovieSets.SelectedRows.Count > 1 AndAlso Me.dgvMovieSets.Rows(e.RowIndex).Selected Then
                        Dim setMark As Boolean = False
                        Dim setLock As Boolean = False
                        Dim setWatched As Boolean = False

                        Me.cmnuMovieTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                        'For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                        '    'if any one item is set as unmarked, set menu to mark
                        '    'else they are all marked, so set menu to unmark
                        '    If Not Convert.ToBoolean(sRow.Cells(11).Value) Then
                        '        setMark = True
                        '        If setLock AndAlso setWatched Then Exit For
                        '    End If
                        '    'if any one item is set as unlocked, set menu to lock
                        '    'else they are all locked so set menu to unlock
                        '    If Not Convert.ToBoolean(sRow.Cells(14).Value) Then
                        '        setLock = True
                        '        If setMark AndAlso setWatched Then Exit For
                        '    End If
                        '    'if any one item is set as unwatched, set menu to watched
                        '    'else they are all watched so set menu to not watched
                        '    If Not Convert.ToBoolean(sRow.Cells(34).Value) Then
                        '        setWatched = True
                        '        If setLock AndAlso setMark Then Exit For
                        '    End If
                        'Next

                    Else
                        Me.cmnuMovieSetReload.Visible = True

                        cmnuMovieSetTitle.Text = String.Concat(">> ", Me.dgvMovieSets.Item(1, e.RowIndex).Value, " <<")

                        If Not Me.dgvMovieSets.Rows(e.RowIndex).Selected Then
                            Me.prevMovieSetRow = -1
                            Me.dgvMovieSets.CurrentCell = Nothing
                            Me.dgvMovieSets.ClearSelection()
                            Me.dgvMovieSets.Rows(e.RowIndex).Selected = True
                            Me.dgvMovieSets.CurrentCell = Me.dgvMovieSets.Item(1, e.RowIndex)
                        Else
                            Me.cmnuMovieSet.Enabled = True
                        End If

                    End If
                Else
                    Me.cmnuMovieSet.Enabled = False
                    Me.cmnuMovieSetTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub


    Private Sub dgvMovieSets_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellMouseEnter
        'EMM not able to scrape subtitles yet.
        'So don't set status for it, but leave the option open for the future.
        If Master.eSettings.MovieClickScrape AndAlso e.RowIndex > 0 AndAlso e.ColumnIndex > 3 AndAlso e.ColumnIndex < 11 AndAlso e.ColumnIndex <> 8 AndAlso Not bwMovieScraper.IsBusy Then
            oldStatus = GetStatus()
            Dim movieName As String = Me.dgvMovies.Rows(e.RowIndex).Cells(15).Value.ToString
            Dim scrapeFor As String = ""
            Dim scrapeType As String = ""
            Select Case e.ColumnIndex
                Case 4
                    scrapeFor = Master.eLang.GetString(72, "Poster Only")
                Case 5
                    scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                Case 6
                    scrapeFor = Master.eLang.GetString(71, "NFO Only")
                Case 7
                    scrapeFor = Master.eLang.GetString(75, "Trailer Only")
                Case 8
                    'scrapeFor = Master.eLang.GetString(00, "Subtitles")
                Case 9
                    scrapeFor = Master.eLang.GetString(74, "Extrathumbs Only")
                Case 10
                    scrapeFor = Master.eLang.GetString(76, "Meta Data Only")
            End Select
            If Master.eSettings.MovieClickScrapeAsk Then
                scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
            Else
                scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
            End If
            Me.SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", movieName, scrapeFor, scrapeType))
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvMovieSets_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then Me.SetStatus(oldStatus)
    End Sub

    Private Sub dgvMovieSets_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovieSets.CellPainting
        Try

            If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvMovieSets.Item(e.ColumnIndex, e.RowIndex).Displayed Then
                e.Handled = True
                Return
            End If

            'icons
            If e.ColumnIndex >= 2 AndAlso e.ColumnIndex <= 17 AndAlso e.RowIndex = -1 Then
                e.PaintBackground(e.ClipBounds, False)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = 3
                If e.ColumnIndex = 2 Then 'NFO
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 2)
                ElseIf e.ColumnIndex = 4 Then 'Poster
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 0)
                ElseIf e.ColumnIndex = 6 Then 'Fanart
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 1)
                ElseIf e.ColumnIndex = 8 Then 'Banner
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
                ElseIf e.ColumnIndex = 10 Then 'Landscape
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
                ElseIf e.ColumnIndex = 12 Then 'DiscArt
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 13)
                ElseIf e.ColumnIndex = 14 Then 'ClearLogo
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 14)
                ElseIf e.ColumnIndex = 16 Then 'ClearArt
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 15)
                End If

                e.Handled = True

            End If

            ''text
            'If e.ColumnIndex = 3 AndAlso e.RowIndex >= 0 Then
            '    If Convert.ToBoolean(Me.dgvMovies.Item(11, e.RowIndex).Value) Then                  'is marked
            '        e.CellStyle.ForeColor = Color.Crimson
            '        e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            '        e.CellStyle.SelectionForeColor = Color.Crimson
            '    ElseIf Convert.ToBoolean(Me.dgvMovies.Item(10, e.RowIndex).Value) Then              'is new
            '        e.CellStyle.ForeColor = Color.Green
            '        e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            '        e.CellStyle.SelectionForeColor = Color.Green
            '    Else
            e.CellStyle.ForeColor = Color.Black
            e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
            e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            '    End If
            'End If

            If e.ColumnIndex >= 0 AndAlso e.ColumnIndex <= 16 AndAlso e.RowIndex >= 0 Then
                'If Convert.ToBoolean(Me.dgvMovies.Item(14, e.RowIndex).Value) Then                  'is locked
                '    e.CellStyle.BackColor = Color.LightSteelBlue
                '    e.CellStyle.SelectionBackColor = Color.DarkTurquoise
                'ElseIf Convert.ToBoolean(Me.dgvMovies.Item(44, e.RowIndex).Value) Then              'use folder
                '    e.CellStyle.BackColor = Color.MistyRose
                '    e.CellStyle.SelectionBackColor = Color.DarkMagenta
                'Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
                'End If

                If e.ColumnIndex >= 2 AndAlso e.ColumnIndex <= 17 Then
                    e.PaintBackground(e.ClipBounds, True)

                    Dim pt As Point = e.CellBounds.Location
                    Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                    pt.X += offset
                    pt.Y = e.CellBounds.Top + 3
                    Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 6, 7))
                    e.Handled = True
                End If
            End If

            Me.tpMovieSets.Text = String.Format("{0} ({1})", Master.eLang.GetString(366, "Sets"), Me.dgvMovieSets.RowCount)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovieSets_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovieSets.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearch.Focus()
    End Sub

    Private Sub dgvMovieSets_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvMovieSets.KeyPress
        Try
            If StringUtils.AlphaNumericOnly(e.KeyChar) Then
                KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
                tmrKeyBuffer.Start()
                For Each drvRow As DataGridViewRow In Me.dgvMovieSets.Rows
                    If drvRow.Cells(0).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                        drvRow.Selected = True
                        Me.dgvMovieSets.CurrentCell = drvRow.Cells(0)

                        Exit For
                    End If
                Next
            ElseIf e.KeyChar = Chr(13) Then
                If Me.fScanner.IsBusy OrElse Me.bwMovieSetInfo.IsBusy OrElse Me.bwLoadMovieSetInfo.IsBusy OrElse _
                Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwRefreshMovieSets.IsBusy _
                OrElse Me.bwCleanDB.IsBusy Then Return

                Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item(0, indX).Value)
                Master.currMovieSet = Master.DB.LoadMovieSetFromDB(ID)
                Me.SetStatus(Master.currMovieSet.ListTitle)

                Using dEditMovieSet As New dlgEditMovieSet
                    'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
                    Select Case dEditMovieSet.ShowDialog()
                        Case Windows.Forms.DialogResult.OK
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieScraperRDYtoSave, Nothing, Master.currMovieSet)
                            Me.SetMovieSetListItemAfterEdit(ID, indX)
                            If Me.RefreshMovieSet(ID) Then
                                Me.FillList(False, True, False)
                            End If
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovieSet)
                        Case Windows.Forms.DialogResult.Retry
                            Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                            Me.MovieSetScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieSetOptions)
                        Case Windows.Forms.DialogResult.Abort
                            'Functions.SetScraperMod(Enums.MovieModType.DoSearch, True)
                            'Functions.SetScraperMod(Enums.MovieModType.All, True, False)
                            'Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                        Case Else
                            If Me.InfoCleared Then Me.LoadMovieSetInfo(ID, Me.dgvMovies.Item(1, indX).Value.ToString, True, False)
                    End Select
                    'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
                End Using

            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovieSets_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSets.Resize
        ResizeMovieSetsList()
    End Sub

    Private Sub dgvMovieSets_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSets.Sorted
        Me.prevMovieSetRow = -1
        If Me.dgvMovieSets.RowCount > 0 Then
            Me.dgvMovieSets.CurrentCell = Nothing
            Me.dgvMovieSets.ClearSelection()
            Me.dgvMovieSets.Rows(0).Selected = True
            Me.dgvMovieSets.CurrentCell = Me.dgvMovieSets.Rows(0).Cells(1)
        End If
    End Sub

    Private Sub dgvTVEpisodes_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellClick
        If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
            If Me.dgvTVEpisodes.RowCount > 0 Then
                If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVEpisodes.SelectedRows.Count))
                ElseIf Me.dgvTVEpisodes.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvTVEpisodes.SelectedRows(0).Cells(3).Value.ToString)
                End If
            End If

            Me.currEpRow = Me.dgvTVEpisodes.SelectedRows(0).Index
            If Not Me.currList = 2 Then
                Me.currList = 2
                Me.prevEpRow = -1
                Me.SelectEpisodeRow(Me.dgvTVEpisodes.SelectedRows(0).Index)
            End If
        End If
    End Sub

    Private Sub dgvTVEpisodes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellDoubleClick
        Try

            If e.RowIndex < 0 Then Exit Sub

            If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item(0, indX).Value)
            Master.currShow = Master.DB.LoadTVEpFromDB(ID, True)

            Using dEditEpisode As New dlgEditEpisode
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEpisode.GenericRunCallBack
                Select Case dEditEpisode.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        If Me.RefreshEpisode(ID) Then
                            Me.FillEpisodes(Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVEp.Season)
                        End If
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEpisode.GenericRunCallBack
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVEpisodes_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellEnter
        Try
            If Not Me.tcMain.SelectedIndex = 2 OrElse Not Me.currList = 2 Then Return

            Me.tmrWaitShow.Stop()
            Me.tmrWaitSeason.Stop()
            Me.tmrWaitMovie.Stop()
            Me.tmrWaitMovieSet.Stop()
            Me.tmrWaitEp.Stop()
            Me.tmrLoadShow.Stop()
            Me.tmrLoadSeason.Stop()
            Me.tmrLoadMovie.Stop()
            Me.tmrLoadMovieSet.Stop()
            Me.tmrLoadEp.Stop()

            Me.currEpRow = e.RowIndex
            Me.tmrWaitEp.Start()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVEpisodes_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVEpisodes.CellPainting
        Try

            If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvTVEpisodes.Item(e.ColumnIndex, e.RowIndex).Displayed Then
                e.Handled = True
                Return
            End If

            'icons
            If e.ColumnIndex >= 4 AndAlso e.ColumnIndex <= 24 AndAlso e.RowIndex = -1 Then
                e.PaintBackground(e.ClipBounds, False)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = 3

                If e.ColumnIndex = 24 Then 'Watched
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 8)
                Else
                    Me.ilColumnIcons.Draw(e.Graphics, pt, e.ColumnIndex - 4)
                End If

                e.Handled = True
            End If

            If (e.ColumnIndex = 2 OrElse e.ColumnIndex = 3) AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(Me.dgvTVEpisodes.Item(22, e.RowIndex).Value) Then
                    e.CellStyle.ForeColor = Color.Gray
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                    e.CellStyle.SelectionForeColor = Color.LightGray
                ElseIf Convert.ToBoolean(Me.dgvTVEpisodes.Item(8, e.RowIndex).Value) Then
                    e.CellStyle.ForeColor = Color.Crimson
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Crimson
                ElseIf Convert.ToBoolean(Me.dgvTVEpisodes.Item(7, e.RowIndex).Value) Then
                    e.CellStyle.ForeColor = Color.Green
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Green
                Else
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                    e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
                End If
            End If

            If e.ColumnIndex >= 2 AndAlso e.ColumnIndex <= 24 AndAlso e.RowIndex >= 0 Then

                If Convert.ToBoolean(Me.dgvTVEpisodes.Item(22, e.RowIndex).Value) Then
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.DarkGray
                ElseIf Convert.ToBoolean(Me.dgvTVEpisodes.Item(11, e.RowIndex).Value) Then
                    e.CellStyle.BackColor = Color.LightSteelBlue
                    e.CellStyle.SelectionBackColor = Color.DarkTurquoise
                Else
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
                End If

                If e.ColumnIndex >= 4 AndAlso e.ColumnIndex <= 24 Then
                    e.PaintBackground(e.ClipBounds, True)

                    Dim pt As Point = e.CellBounds.Location
                    Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                    pt.X += offset
                    pt.Y = e.CellBounds.Top + 3
                    Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 6, 7))
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVEpisodes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVEpisodes.KeyDown
        'stop enter key from selecting next list item
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub dgvTVEpisodes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVEpisodes.KeyPress
        Try
            If StringUtils.AlphaNumericOnly(e.KeyChar) Then
                KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
                tmrKeyBuffer.Start()
                For Each drvRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                    If drvRow.Cells(3).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                        drvRow.Selected = True
                        Me.dgvTVEpisodes.CurrentCell = drvRow.Cells(3)
                        Exit For
                    End If
                Next
            ElseIf e.KeyChar = Chr(13) Then
                If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

                Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
                Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item(0, indX).Value)
                Master.currShow = Master.DB.LoadTVEpFromDB(ID, True)

                Using dEditEpisode As New dlgEditEpisode
                    AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEpisode.GenericRunCallBack
                    Select Case dEditEpisode.ShowDialog()
                        Case Windows.Forms.DialogResult.OK
                            If Me.RefreshEpisode(ID) Then
                                Me.FillEpisodes(Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVEp.Season)
                            End If
                    End Select
                    RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEpisode.GenericRunCallBack
                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ShowEpisodeMenuItems(ByVal Visible As Boolean)
        Dim cMnu As ToolStripMenuItem
        Dim cSep As ToolStripSeparator

        Try
            If Visible Then
                For Each cMnuItem As Object In Me.cmnuEpisode.Items
                    If TypeOf cMnuItem Is ToolStripMenuItem Then
                        DirectCast(cMnuItem, ToolStripMenuItem).Visible = True
                    ElseIf TypeOf cMnuItem Is ToolStripSeparator Then
                        DirectCast(cMnuItem, ToolStripSeparator).Visible = True
                    End If
                Next
                Me.cmnuEpisodeRemoveFromDisk.Visible = True
            Else
                For Each cMnuItem As Object In Me.cmnuEpisode.Items
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
                    Me.cmnuEpisodeRemoveFromDisk.Visible = False
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

    Private Sub dgvTVEpisodes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVEpisodes.MouseDown
        Try
            Dim hasMissing As Boolean = False

            If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvTVEpisodes.RowCount > 0 Then

                Me.cmnuEpisode.Enabled = False

                Dim dgvHTI As DataGridView.HitTestInfo = dgvTVEpisodes.HitTest(e.X, e.Y)
                If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                    If Me.dgvTVEpisodes.SelectedRows.Count > 1 AndAlso Me.dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected Then

                        Me.cmnuEpisode.Enabled = True

                        For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                            If Convert.ToBoolean(sRow.Cells(22).Value) Then
                                hasMissing = True
                                Exit For
                            End If
                        Next

                        Me.cmnuEpisodeTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                        If hasMissing Then
                            Me.ShowEpisodeMenuItems(False)
                        Else
                            Dim setMark As Boolean = False
                            Dim setLock As Boolean = False
                            Dim setWatched As Boolean = False

                            Me.ShowEpisodeMenuItems(True)

                            Me.ToolStripSeparator9.Visible = False
                            Me.cmnuEpisodeEdit.Visible = False
                            Me.ToolStripSeparator10.Visible = False
                            Me.cmnuEpisodeRescrape.Visible = False
                            Me.cmnuEpisodeChange.Visible = False
                            Me.ToolStripSeparator12.Visible = False
                            Me.cmnuEpisodeOpenFolder.Visible = False

                            For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                                'if any one item is set as unmarked, set menu to mark
                                'else they are all marked, so set menu to unmark
                                If Not Convert.ToBoolean(sRow.Cells(8).Value) Then
                                    setMark = True
                                    If setLock Then Exit For
                                End If
                                'if any one item is set as unlocked, set menu to lock
                                'else they are all locked so set menu to unlock
                                If Not Convert.ToBoolean(sRow.Cells(11).Value) Then
                                    setLock = True
                                    If setMark Then Exit For
                                End If
                                'if any one item is set as unwatched, set menu to watched
                                'else they are all watched so set menu to not watched
                                If Not Convert.ToBoolean(sRow.Cells(24).Value) Then
                                    setWatched = True
                                    If setWatched Then Exit For
                                End If
                            Next

                            Me.cmnuEpisodeMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                            Me.cmnuEpisodeLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                            Me.cmnuEpisodeWatched.Text = If(setWatched, Master.eLang.GetString(981, "Watched"), Master.eLang.GetString(980, "Not Watched"))
                        End If
                    Else
                        cmnuEpisodeTitle.Text = String.Concat(">> ", Me.dgvTVEpisodes.Item(3, dgvHTI.RowIndex).Value, " <<")

                        If Not Me.dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected OrElse Not Me.currList = 2 Then
                            Me.prevEpRow = -1
                            Me.currList = 2
                            Me.dgvTVEpisodes.CurrentCell = Nothing
                            Me.dgvTVEpisodes.ClearSelection()
                            Me.dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected = True
                            Me.dgvTVEpisodes.CurrentCell = Me.dgvTVEpisodes.Item(3, dgvHTI.RowIndex)
                        Else
                            Me.cmnuEpisode.Enabled = True
                        End If

                        If Convert.ToBoolean(Me.dgvTVEpisodes.Item(22, dgvHTI.RowIndex).Value) Then hasMissing = True

                        If hasMissing Then
                            Me.ShowEpisodeMenuItems(False)
                        Else
                            Me.ShowEpisodeMenuItems(True)

                            Me.ToolStripSeparator9.Visible = True
                            Me.cmnuEpisodeEdit.Visible = True
                            Me.ToolStripSeparator10.Visible = True
                            Me.cmnuEpisodeRescrape.Visible = True
                            Me.cmnuEpisodeChange.Visible = True
                            Me.ToolStripSeparator12.Visible = True
                            Me.cmnuEpisodeOpenFolder.Visible = True

                            Me.cmnuEpisodeMark.Text = If(Convert.ToBoolean(Me.dgvTVEpisodes.Item(8, dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                            Me.cmnuEpisodeLock.Text = If(Convert.ToBoolean(Me.dgvTVEpisodes.Item(11, dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                        End If

                    End If
                Else
                    Me.cmnuEpisode.Enabled = False
                    Me.cmnuEpisodeTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVEpisodes_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVEpisodes.Resize
        ResizeTVLists(3)
    End Sub

    Private Sub dgvTVEpisodes_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVEpisodes.Sorted
        Me.prevEpRow = -1
        If Me.dgvTVEpisodes.RowCount > 0 Then
            Me.dgvTVEpisodes.CurrentCell = Nothing
            Me.dgvTVEpisodes.ClearSelection()
            Me.dgvTVEpisodes.Rows(0).Selected = True
            Me.dgvTVEpisodes.CurrentCell = Me.dgvTVEpisodes.Rows(0).Cells(3)
        End If
    End Sub

    Private Sub dgvTVSeasons_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellClick
        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
            If Me.dgvTVSeasons.RowCount > 0 Then
                If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVSeasons.SelectedRows.Count))
                ElseIf Me.dgvTVSeasons.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvTVSeasons.SelectedRows(0).Cells(1).Value.ToString)
                End If
            End If

            Me.currSeasonRow = Me.dgvTVSeasons.SelectedRows(0).Index
            If Not Me.currList = 1 Then
                Me.currList = 1
                Me.prevSeasonRow = -1
                Me.SelectSeasonRow(Me.dgvTVSeasons.SelectedRows(0).Index)
            End If
        End If
    End Sub

    Private Sub dgvTVSeasons_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellDoubleClick
        Try

            If e.RowIndex < 0 Then Exit Sub

            If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(0, indX).Value)
            Dim Season As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(2, indX).Value)

            Master.currShow = Master.DB.LoadTVSeasonFromDB(ShowID, Season, True)

            Using dEditSeason As New dlgEditSeason
                If dEditSeason.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    If Me.RefreshSeason(ShowID, Season, False) Then
                        Me.FillSeasons(ShowID)
                    End If
                End If
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVSeasons_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellEnter
        Try

            If Not Me.tcMain.SelectedIndex = 2 OrElse Not Me.currList = 1 Then Return

            Me.tmrWaitShow.Stop()
            Me.tmrWaitMovie.Stop()
            Me.tmrWaitMovieSet.Stop()
            Me.tmrWaitEp.Stop()
            Me.tmrWaitSeason.Stop()
            Me.tmrLoadShow.Stop()
            Me.tmrLoadMovie.Stop()
            Me.tmrLoadMovieSet.Stop()
            Me.tmrLoadEp.Stop()
            Me.tmrLoadSeason.Stop()

            Me.currSeasonRow = e.RowIndex
            Me.tmrWaitSeason.Start()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVSeasons_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVSeasons.CellPainting
        Try

            If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvTVSeasons.Item(e.ColumnIndex, e.RowIndex).Displayed Then
                e.Handled = True
                Return
            End If

            'icons
            If (e.ColumnIndex >= 3 AndAlso e.ColumnIndex <= 13) AndAlso e.RowIndex = -1 Then
                e.PaintBackground(e.ClipBounds, False)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = 3

                If e.ColumnIndex = 3 Then 'Poster
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 0)
                ElseIf e.ColumnIndex = 4 Then 'Fanart
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 1)
                ElseIf e.ColumnIndex = 10 Then 'Banner
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
                ElseIf e.ColumnIndex = 12 Then 'Landscape
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
                End If

                e.Handled = True

            End If

            If e.ColumnIndex = 1 AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(Me.dgvTVSeasons.Item(8, e.RowIndex).Value) Then
                    e.CellStyle.ForeColor = Color.Crimson
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Crimson
                ElseIf Convert.ToBoolean(Me.dgvTVSeasons.Item(9, e.RowIndex).Value) Then
                    e.CellStyle.ForeColor = Color.Green
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Green
                Else
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                    e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
                End If
            End If

            If e.ColumnIndex >= 1 AndAlso e.ColumnIndex <= 13 AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(Me.dgvTVSeasons.Item(7, e.RowIndex).Value) Then
                    e.CellStyle.BackColor = Color.LightSteelBlue
                    e.CellStyle.SelectionBackColor = Color.DarkTurquoise
                Else
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
                End If

                If e.ColumnIndex >= 3 AndAlso e.ColumnIndex <= 13 Then
                    e.PaintBackground(e.ClipBounds, True)

                    Dim pt As Point = e.CellBounds.Location
                    Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                    pt.X += offset
                    pt.Y = e.CellBounds.Top + 3
                    Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 6, 7))
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVSeasons_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVSeasons.KeyDown
        'stop enter key from selecting next list item
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub dgvTVSeasons_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVSeasons.KeyPress
        Try
            If StringUtils.AlphaNumericOnly(e.KeyChar) Then
                KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
                tmrKeyBuffer.Start()
                For Each drvRow As DataGridViewRow In Me.dgvTVSeasons.Rows
                    If drvRow.Cells(2).Value.ToString.StartsWith(KeyBuffer) Then
                        drvRow.Selected = True
                        Me.dgvTVSeasons.CurrentCell = drvRow.Cells(1)
                        Exit For
                    End If
                Next
            ElseIf e.KeyChar = Chr(13) Then
                If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

                Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(0, indX).Value)
                Dim Season As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(2, indX).Value)

                Master.currShow = Master.DB.LoadTVSeasonFromDB(ShowID, Season, True)

                Using dEditSeason As New dlgEditSeason
                    If dEditSeason.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        If Me.RefreshSeason(ShowID, Season, False) Then
                            Me.FillSeasons(ShowID)
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVSeasons_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVSeasons.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvTVSeasons.RowCount > 0 Then

                Me.cmnuSeason.Enabled = False

                Dim dgvHTI As DataGridView.HitTestInfo = dgvTVSeasons.HitTest(e.X, e.Y)
                If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                    If Me.dgvTVSeasons.SelectedRows.Count > 1 AndAlso Me.dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected Then
                        Dim setMark As Boolean = False
                        Dim setLock As Boolean = False

                        Me.cmnuSeason.Enabled = True
                        Me.cmnuSeasonTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")
                        Me.ToolStripSeparator16.Visible = False
                        Me.cmnuSeasonChangeImages.Visible = False
                        Me.ToolStripSeparator14.Visible = False
                        Me.cmnuSeasonRescrape.Visible = False
                        Me.ToolStripSeparator15.Visible = False
                        Me.cmnuSeasonOpenFolder.Visible = False

                        For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                            'if any one item is set as unmarked, set menu to mark
                            'else they are all marked, so set menu to unmark
                            If Not Convert.ToBoolean(sRow.Cells(8).Value) Then
                                setMark = True
                                If setLock Then Exit For
                            End If
                            'if any one item is set as unlocked, set menu to lock
                            'else they are all locked so set menu to unlock
                            If Not Convert.ToBoolean(sRow.Cells(7).Value) Then
                                setLock = True
                                If setMark Then Exit For
                            End If
                        Next

                        Me.cmnuSeasonMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                        Me.cmnuSeasonLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))

                    Else
                        Me.ToolStripSeparator16.Visible = True
                        Me.cmnuSeasonChangeImages.Visible = True
                        Me.ToolStripSeparator14.Visible = True
                        Me.cmnuSeasonRescrape.Visible = True
                        Me.ToolStripSeparator15.Visible = True
                        Me.cmnuSeasonOpenFolder.Visible = True

                        Me.cmnuSeasonTitle.Text = String.Concat(">> ", Me.dgvTVSeasons.Item(1, dgvHTI.RowIndex).Value, " <<")
                        Me.cmnuSeasonMark.Text = If(Convert.ToBoolean(Me.dgvTVSeasons.Item(8, dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                        Me.cmnuSeasonLock.Text = If(Convert.ToBoolean(Me.dgvTVSeasons.Item(7, dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                        Me.cmnuSeasonChangeImages.Enabled = Convert.ToInt32(Me.dgvTVSeasons.Item(2, dgvHTI.RowIndex).Value) >= 0

                        If Not Me.dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected OrElse Not Me.currList = 1 Then
                            Me.prevSeasonRow = -1
                            Me.currList = 1
                            Me.dgvTVSeasons.CurrentCell = Nothing
                            Me.dgvTVSeasons.ClearSelection()
                            Me.dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected = True
                            Me.dgvTVSeasons.CurrentCell = Me.dgvTVSeasons.Item(1, dgvHTI.RowIndex)
                        Else
                            Me.cmnuSeason.Enabled = True
                        End If
                    End If
                Else
                    Me.cmnuSeason.Enabled = False
                    Me.cmnuSeasonTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVSeasons_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVSeasons.Sorted
        Me.prevSeasonRow = -1
        If Me.dgvTVSeasons.RowCount > 0 Then
            Me.dgvTVSeasons.CurrentCell = Nothing
            Me.dgvTVSeasons.ClearSelection()
            Me.dgvTVSeasons.Rows(0).Selected = True
            Me.dgvTVSeasons.CurrentCell = Me.dgvTVSeasons.Rows(0).Cells(1)
        End If
    End Sub

    Private Sub dgvTVSeason_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVSeasons.Resize
        ResizeTVLists(2)
    End Sub

    Private Sub dgvTVShows_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellClick
        If Me.dgvTVShows.SelectedRows.Count > 0 Then
            If Me.dgvTVShows.RowCount > 0 Then
                Me.tmpTitle = Me.dgvTVShows.SelectedRows(0).Cells(1).Value.ToString
                Me.tmpTVDB = Me.dgvTVShows.SelectedRows(0).Cells(9).Value.ToString
                Me.tmpLang = Me.dgvTVShows.SelectedRows(0).Cells(22).Value.ToString
                Me.tmpOrdering = DirectCast(Convert.ToInt32(Me.dgvTVShows.SelectedRows(0).Cells(23).Value), Enums.Ordering)
                If Me.dgvTVShows.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVShows.SelectedRows.Count))
                ElseIf Me.dgvTVShows.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvTVShows.SelectedRows(0).Cells(1).Value.ToString)
                End If
            End If

            Me.currShowRow = Me.dgvTVShows.SelectedRows(0).Index
            If Not Me.currList = 0 Then
                Me.currList = 0
                Me.prevShowRow = -1
                Me.SelectShowRow(Me.dgvTVShows.SelectedRows(0).Index)
            End If
        End If
    End Sub

    Private Sub dgvTVShows_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellDoubleClick
        Try

            If e.RowIndex < 0 Then Exit Sub

            If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item(0, indX).Value)

            Master.currShow = Master.DB.LoadTVFullShowFromDB(ID)

            Using dEditShow As New dlgEditShow

                Select Case dEditShow.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        Me.SetShowListItemAfterEdit(ID, indX)
                        If Me.RefreshShow(ID, False, True, False, False) Then
                            Me.FillList(False, False, True)
                        End If
                End Select

            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVShows_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellEnter
        Try
            If Not Me.tcMain.SelectedIndex = 2 OrElse Not Me.currList = 0 Then Return

            Me.tmrWaitMovie.Stop()
            Me.tmrWaitMovieSet.Stop()
            Me.tmrWaitSeason.Stop()
            Me.tmrWaitEp.Stop()
            Me.tmrWaitShow.Stop()
            Me.tmrLoadMovie.Stop()
            Me.tmrLoadMovieSet.Stop()
            Me.tmrLoadSeason.Stop()
            Me.tmrLoadEp.Stop()
            Me.tmrLoadShow.Stop()

            Me.currShowRow = e.RowIndex
            Me.tmrWaitShow.Start()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVShows_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVShows.CellPainting
        Try

            If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvTVShows.Item(e.ColumnIndex, e.RowIndex).Displayed Then
                e.Handled = True
                Return
            End If

            'icons
            If e.ColumnIndex >= 2 AndAlso e.ColumnIndex <= 38 AndAlso e.RowIndex = -1 Then
                e.PaintBackground(e.ClipBounds, False)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = 3
                If e.ColumnIndex = 2 Then 'Poster
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 0)
                ElseIf e.ColumnIndex = 3 Then 'Fanart
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 1)
                ElseIf e.ColumnIndex = 4 Then 'NFO
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 2)
                ElseIf e.ColumnIndex = 24 Then 'Banner
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
                ElseIf e.ColumnIndex = 26 Then 'Landscape
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
                ElseIf e.ColumnIndex = 29 Then 'Theme
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 12)
                ElseIf e.ColumnIndex = 31 Then 'CharacterArt
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 16)
                ElseIf e.ColumnIndex = 33 Then 'ClearLogo
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 14)
                ElseIf e.ColumnIndex = 35 Then 'ClearArt
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 15)
                ElseIf e.ColumnIndex = 37 Then 'Extrafanarts
                    Me.ilColumnIcons.Draw(e.Graphics, pt, 9)
                End If

                e.Handled = True

            End If

            If e.ColumnIndex = 1 AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(Me.dgvTVShows.Item(6, e.RowIndex).Value) Then
                    e.CellStyle.ForeColor = Color.Crimson
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Crimson
                ElseIf Convert.ToBoolean(Me.dgvTVShows.Item(5, e.RowIndex).Value) Then
                    e.CellStyle.ForeColor = Color.Green
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.SelectionForeColor = Color.Green
                Else
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                    e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
                End If
            End If

            If e.ColumnIndex >= 1 AndAlso e.ColumnIndex <= 38 AndAlso e.RowIndex >= 0 Then

                If Convert.ToBoolean(Me.dgvTVShows.Item(10, e.RowIndex).Value) Then
                    e.CellStyle.BackColor = Color.LightSteelBlue
                    e.CellStyle.SelectionBackColor = Color.DarkTurquoise
                Else
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
                End If

                If e.ColumnIndex >= 2 AndAlso e.ColumnIndex <= 38 Then
                    e.PaintBackground(e.ClipBounds, True)

                    Dim pt As Point = e.CellBounds.Location
                    Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                    pt.X += offset
                    pt.Y = e.CellBounds.Top + 3
                    Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 6, 7))
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVShows_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVShows.KeyDown
        'stop enter key from selecting next list item
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub dgvTVShows_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVShows.KeyPress
        Try
            If StringUtils.AlphaNumericOnly(e.KeyChar) Then
                KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
                tmrKeyBuffer.Start()
                For Each drvRow As DataGridViewRow In Me.dgvTVShows.Rows
                    If drvRow.Cells(1).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                        drvRow.Selected = True
                        Me.dgvTVShows.CurrentCell = drvRow.Cells(1)
                        Exit For
                    End If
                Next
            ElseIf e.KeyChar = Chr(13) Then
                If Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

                Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item(0, indX).Value)

                Master.currShow = Master.DB.LoadTVFullShowFromDB(ID)

                Using dEditShow As New dlgEditShow

                    Select Case dEditShow.ShowDialog()
                        Case Windows.Forms.DialogResult.OK
                            Me.SetShowListItemAfterEdit(ID, indX)
                            If Me.RefreshShow(ID, False, True, False, False) Then
                                Me.FillList(False, False, True)
                            End If
                    End Select

                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVShows_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVShows.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvTVShows.RowCount > 0 Then

                Me.cmnuShow.Enabled = False

                Dim dgvHTI As DataGridView.HitTestInfo = dgvTVShows.HitTest(e.X, e.Y)
                If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                    Me.tmpTitle = Me.dgvTVShows.Item(1, dgvHTI.RowIndex).Value.ToString
                    Me.tmpTVDB = Me.dgvTVShows.Item(9, dgvHTI.RowIndex).Value.ToString
                    Me.tmpLang = Me.dgvTVShows.Item(22, dgvHTI.RowIndex).Value.ToString
                    Me.tmpOrdering = DirectCast(Convert.ToInt32(Me.dgvTVShows.Item(23, dgvHTI.RowIndex).Value), Enums.Ordering)

                    If Me.dgvTVShows.SelectedRows.Count > 1 AndAlso Me.dgvTVShows.Rows(dgvHTI.RowIndex).Selected Then
                        Dim setMark As Boolean = False
                        Dim setLock As Boolean = False

                        Me.cmnuShow.Enabled = True
                        Me.cmnuShowTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")
                        Me.ToolStripSeparator8.Visible = False
                        Me.cmnuShowEdit.Visible = False
                        Me.ToolStripSeparator7.Visible = False
                        ' Me.cmnuRescrapeShow.Visible = False
                        Me.cmnuShowChange.Visible = False
                        Me.cmnuShowOpenFolder.Visible = False
                        Me.ToolStripSeparator20.Visible = False

                        For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                            'if any one item is set as unmarked, set menu to mark
                            'else they are all marked, so set menu to unmark
                            If Not Convert.ToBoolean(sRow.Cells(6).Value) Then
                                setMark = True
                                If setLock Then Exit For
                            End If
                            'if any one item is set as unlocked, set menu to lock
                            'else they are all locked so set menu to unlock
                            If Not Convert.ToBoolean(sRow.Cells(10).Value) Then
                                setLock = True
                                If setMark Then Exit For
                            End If
                        Next

                        Me.cmnuShowMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                        Me.cmnuShowLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))

                        Me.cmnuShowLanguageLanguages.Items.Insert(0, Master.eLang.GetString(1199, "Select Language..."))
                        Me.cmnuShowLanguageLanguages.SelectedItem = Master.eLang.GetString(1199, "Select Language...")
                        Me.cmnuShowLanguageSet.Enabled = False
                    Else
                        Me.ToolStripSeparator8.Visible = True
                        Me.cmnuShowEdit.Visible = True
                        Me.ToolStripSeparator7.Visible = True
                        Me.cmnuShowRefresh.Visible = True
                        Me.cmnuShowRescrape.Visible = True
                        Me.cmnuShowChange.Visible = True
                        Me.cmnuShowOpenFolder.Visible = True
                        Me.ToolStripSeparator20.Visible = True

                        Me.cmnuShowTitle.Text = String.Concat(">> ", Me.dgvTVShows.Item(1, dgvHTI.RowIndex).Value, " <<")
                        Me.cmnuShowMark.Text = If(Convert.ToBoolean(Me.dgvTVShows.Item(6, dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                        Me.cmnuShowLock.Text = If(Convert.ToBoolean(Me.dgvTVShows.Item(10, dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))

                        If Not Me.dgvTVShows.Rows(dgvHTI.RowIndex).Selected OrElse Not Me.currList = 0 Then
                            Me.prevShowRow = -1
                            Me.currList = 0
                            Me.dgvTVShows.CurrentCell = Nothing
                            Me.dgvTVShows.ClearSelection()
                            Me.dgvTVShows.Rows(dgvHTI.RowIndex).Selected = True
                            Me.dgvTVShows.CurrentCell = Me.dgvTVShows.Item(3, dgvHTI.RowIndex)
                        Else
                            Me.cmnuShow.Enabled = True
                        End If

                        Dim Lang As String = CStr(Me.dgvTVShows.Item(22, dgvHTI.RowIndex).Value)
                        Me.cmnuShowLanguageLanguages.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Lang).name
                        'Me.cmnuShowLanguageLanguages.Items.Insert(0, Master.eLang.GetString(1199, "Select Language..."))
                        'Me.cmnuShowLanguageLanguages.SelectedItem = Master.eLang.GetString(1199, "Select Language...")
                        Me.cmnuShowLanguageSet.Enabled = False
                    End If
                Else
                    Me.cmnuShow.Enabled = False
                    Me.cmnuShowTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvTVShows_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVShows.Resize
        ResizeTVLists(1)
    End Sub

    Private Sub dgvTVShows_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVShows.Sorted
        Me.prevShowRow = -1
        If Me.dgvTVShows.RowCount > 0 Then
            Me.dgvTVShows.CurrentCell = Nothing
            Me.dgvTVShows.ClearSelection()
            Me.dgvTVShows.Rows(0).Selected = True
            Me.dgvTVShows.CurrentCell = Me.dgvTVShows.Rows(0).Cells(1)
        End If
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
        Try

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "UPDATE movies SET OutOfTolerance = (?) WHERE ID = (?);"
                    Dim parOutOfTolerance As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOutOfTolerance", DbType.Boolean, 0, "OutOfTolerance")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                    Dim LevFail As Boolean = False
                    Dim pTitle As String = String.Empty
                    For Each drvRow As DataGridViewRow In Me.dgvMovies.Rows

                        If Master.eSettings.MovieLevTolerance > 0 Then
                            If FileUtils.Common.isVideoTS(drvRow.Cells(1).Value.ToString) Then
                                pTitle = Directory.GetParent(Directory.GetParent(drvRow.Cells(1).Value.ToString).FullName).Name
                            ElseIf FileUtils.Common.isBDRip(drvRow.Cells(1).Value.ToString) Then
                                pTitle = Directory.GetParent(Directory.GetParent(Directory.GetParent(drvRow.Cells(1).Value.ToString).FullName).FullName).Name
                            Else
                                If Convert.ToBoolean(drvRow.Cells(43).FormattedValue) AndAlso Convert.ToBoolean(drvRow.Cells(2).FormattedValue) Then
                                    pTitle = Directory.GetParent(drvRow.Cells(1).Value.ToString).Name
                                Else
                                    pTitle = Path.GetFileNameWithoutExtension(drvRow.Cells(1).Value.ToString)
                                End If
                            End If

                            LevFail = StringUtils.ComputeLevenshtein(StringUtils.FilterName(drvRow.Cells(15).Value.ToString, False, True).ToLower, StringUtils.FilterName(pTitle, False, True).ToLower) > Master.eSettings.MovieLevTolerance

                            parOutOfTolerance.Value = LevFail
                            drvRow.Cells(44).Value = LevFail
                            parID.Value = drvRow.Cells(0).Value
                        Else
                            parOutOfTolerance.Value = False
                            drvRow.Cells(44).Value = False
                            parID.Value = drvRow.Cells(0).Value
                        End If
                        SQLcommand.ExecuteNonQuery()
                    Next
                End Using

                SQLtransaction.Commit()
            End Using

            Me.dgvMovies.Invalidate()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Sub dtListUpdate(ByVal drow As DataRow, ByVal i As Integer, ByVal v As Object)
        drow.Item(i) = v
    End Sub

    Private Sub EnableFilters(ByVal isEnabled As Boolean)
        Me.txtSearch.Enabled = isEnabled
        Me.cbSearch.Enabled = isEnabled
        Me.chkFilterDupe.Enabled = isEnabled
        Me.chkFilterTolerance.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        Me.chkFilterMissing.Enabled = isEnabled
        Me.chkFilterMark.Enabled = isEnabled
        Me.chkFilterNew.Enabled = isEnabled
        Me.chkFilterLock.Enabled = isEnabled
        Me.rbFilterOr.Enabled = isEnabled
        Me.rbFilterAnd.Enabled = isEnabled
        Me.txtFilterSource.Enabled = isEnabled
        Me.cbFilterFileSource.Enabled = isEnabled
        Me.txtFilterGenre.Enabled = isEnabled
        Me.cbFilterYearMod.Enabled = isEnabled
        Me.cbFilterYear.Enabled = isEnabled
        Me.btnClearFilters.Enabled = isEnabled
    End Sub

    Private Sub ErrorOccurred()
        Me.mnuMainError.Visible = True
        If dlgErrorViewer.Visible Then dlgErrorViewer.UpdateLog()
    End Sub

    Private Sub ErrorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainError.Click
        dlgErrorViewer.Show(Me)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainFileExit.Click, cmnuTrayExit.Click
        If Master.isCL Then
            'fLoading.SetLoadingMesg("Canceling ...")
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(370, "Canceling Load..."))
            If Me.bwMovieScraper.IsBusy Then Me.bwMovieScraper.CancelAsync()
            If Me.bwRefreshMovies.IsBusy Then Me.bwRefreshMovies.CancelAsync()
            While Me.bwMovieScraper.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwMovieScraper.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        Else
            Me.Close()
            Application.Exit()
        End If
    End Sub

    Private Sub FillEpisodes(ByVal ShowID As Integer, ByVal Season As Integer)
        Me.bsEpisodes.DataSource = Nothing
        Me.dgvTVEpisodes.DataSource = Nothing

        Application.DoEvents()

        Me.dgvTVEpisodes.Enabled = False

        Master.DB.FillDataTable(Me.dtEpisodes, String.Concat("SELECT ID, TVShowID, Episode, Title, HasPoster, HasFanart, HasNfo, New, Mark, TVEpPathID, Source, Lock, Season, Rating, Plot, Aired, Director, Credits, PosterPath, FanartPath, NfoPath, NeedsSave, Missing, Playcount, HasWatched, DisplaySeason, DisplayEpisode, DateAdd FROM TVEps WHERE TVShowID = ", ShowID, " AND Season = ", Season, " ORDER BY Episode;"))

        If Me.dtEpisodes.Rows.Count > 0 Then

            With Me
                .bsEpisodes.DataSource = .dtEpisodes
                .dgvTVEpisodes.DataSource = .bsEpisodes

                .dgvTVEpisodes.Columns(0).Visible = False
                .dgvTVEpisodes.Columns(1).Visible = False
                .dgvTVEpisodes.Columns(2).Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns(2).ReadOnly = True
                .dgvTVEpisodes.Columns(2).Width = 40
                .dgvTVEpisodes.Columns(2).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns(2).ToolTipText = Master.eLang.GetString(755, "Episode #")
                .dgvTVEpisodes.Columns(2).HeaderText = "#"
                .dgvTVEpisodes.Columns(3).Resizable = DataGridViewTriState.True
                .dgvTVEpisodes.Columns(3).ReadOnly = True
                .dgvTVEpisodes.Columns(3).MinimumWidth = 83
                .dgvTVEpisodes.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns(3).ToolTipText = Master.eLang.GetString(21, "Title")
                .dgvTVEpisodes.Columns(3).HeaderText = Master.eLang.GetString(21, "Title")
                .dgvTVEpisodes.Columns(4).Width = 20
                .dgvTVEpisodes.Columns(4).Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns(4).ReadOnly = True
                .dgvTVEpisodes.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns(4).Visible = Not Master.eSettings.TVEpisodePosterCol
                .dgvTVEpisodes.Columns(4).ToolTipText = Master.eLang.GetString(148, "Poster")
                .dgvTVEpisodes.Columns(5).Width = 20
                .dgvTVEpisodes.Columns(5).Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns(5).ReadOnly = True
                .dgvTVEpisodes.Columns(5).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns(5).Visible = Not Master.eSettings.TVEpisodeFanartCol
                .dgvTVEpisodes.Columns(5).ToolTipText = Master.eLang.GetString(149, "Fanart")
                .dgvTVEpisodes.Columns(6).Width = 20
                .dgvTVEpisodes.Columns(6).Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns(6).ReadOnly = True
                .dgvTVEpisodes.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns(6).Visible = Not Master.eSettings.TVEpisodeNfoCol
                .dgvTVEpisodes.Columns(6).ToolTipText = Master.eLang.GetString(150, "Nfo")
                .dgvTVEpisodes.Columns(7).Visible = False
                .dgvTVEpisodes.Columns(8).Visible = False
                .dgvTVEpisodes.Columns(9).Visible = False
                .dgvTVEpisodes.Columns(10).Visible = False
                .dgvTVEpisodes.Columns(11).Visible = False
                .dgvTVEpisodes.Columns(12).Visible = False
                .dgvTVEpisodes.Columns(13).Visible = False
                .dgvTVEpisodes.Columns(14).Visible = False
                .dgvTVEpisodes.Columns(15).Visible = False
                .dgvTVEpisodes.Columns(16).Visible = False
                .dgvTVEpisodes.Columns(17).Visible = False
                .dgvTVEpisodes.Columns(18).Visible = False
                .dgvTVEpisodes.Columns(19).Visible = False
                .dgvTVEpisodes.Columns(20).Visible = False
                .dgvTVEpisodes.Columns(21).Visible = False
                .dgvTVEpisodes.Columns(22).Visible = False
                .dgvTVEpisodes.Columns(23).Visible = False
                .dgvTVEpisodes.Columns(24).Width = 20
                .dgvTVEpisodes.Columns(24).Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns(24).ReadOnly = True
                .dgvTVEpisodes.Columns(24).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns(24).Visible = Not Master.eSettings.TVEpisodeWatchedCol
                .dgvTVEpisodes.Columns(24).ToolTipText = Master.eLang.GetString(981, "Watched")
                For i As Integer = 25 To .dgvTVEpisodes.Columns.Count - 1
                    .dgvTVEpisodes.Columns(i).Visible = False
                Next

                .dgvTVEpisodes.Columns(0).ValueType = GetType(Int32)
                .dgvTVEpisodes.Columns(2).ValueType = GetType(Int32)

                If Master.isWindows Then .dgvTVEpisodes.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                ResizeTVLists(3)

                .dgvTVEpisodes.Sort(.dgvTVEpisodes.Columns(2), ComponentModel.ListSortDirection.Ascending)

                .dgvTVEpisodes.ClearSelection()
                .dgvTVEpisodes.CurrentCell = Nothing

            End With
        End If

        Me.dgvTVEpisodes.Enabled = True
    End Sub
    ''' <summary>
    ''' Reloads the DB and refresh the lists
    ''' </summary>
    ''' <param name="doMovies">reload movies</param>
    ''' <param name="doMovieSets">reload moviesets</param>
    ''' <param name="doTVShows">reload tv shows</param>
    ''' <remarks></remarks>
    Private Sub FillList(ByVal doMovies As Boolean, ByVal doMovieSets As Boolean, ByVal doTVShows As Boolean)
        Try
            If doMovies Then
                Me.bsMovies.DataSource = Nothing
                Me.dgvMovies.DataSource = Nothing
                Me.ClearInfo()
                If Not String.IsNullOrEmpty(Me.filSearch) AndAlso Me.cbSearch.Text = Master.eLang.GetString(100, "Actor") Then
                    Master.DB.FillDataTable(Me.dtMovies, String.Concat("SELECT ID, MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, ", _
                                                                      "HasTrailer, HasSub, HasEThumbs, New, Mark, Source, Imdb, Lock, Title, OriginalTitle, ", _
                                                                      "Year, Rating, Votes, MPAA, Top250, Country, Outline, Plot, Tagline, Certification, Genre, ", _
                                                                      "Studio, Runtime, ReleaseDate, Director, Credits, Playcount, HasWatched, Trailer, PosterPath, ", _
                                                                      "FanartPath, EThumbsPath, NfoPath, TrailerPath, SubPath, FanartURL, UseFolder, OutOfTolerance, ", _
                                                                      "FileSource, NeedsSave, SortTitle, DateAdd, HasEFanarts, EFanartsPath, HasBanner, BannerPath, ", _
                                                                      "HasLandscape, LandscapePath, HasTheme, ThemePath, HasDiscArt, DiscArtPath, ", _
                                                                      "HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDB, TMDBColID, LastScrape, ", _
                                                                      "MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4 FROM movies WHERE ID IN ", _
                                                                      "(SELECT MovieID FROM MoviesActors WHERE ActorName LIKE '%", Me.filSearch, "%') ORDER BY ListTitle COLLATE NOCASE;"))
                ElseIf Not String.IsNullOrEmpty(Me.filSearch) AndAlso Me.cbSearch.Text = Master.eLang.GetString(233, "Role") Then
                    Master.DB.FillDataTable(Me.dtMovies, String.Concat("SELECT ID, MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, HasTrailer, ", _
                                                                      "HasSub, HasEThumbs, New, Mark, Source, Imdb, Lock, Title, OriginalTitle, Year, Rating, Votes, ", _
                                                                      "MPAA, Top250, Country, Outline, Plot, Tagline, Certification, Genre, Studio, Runtime, ReleaseDate, ", _
                                                                      "Director, Credits, Playcount, HasWatched, Trailer, PosterPath, FanartPath, EThumbsPath, NfoPath, ", _
                                                                      "TrailerPath, SubPath, FanartURL, UseFolder, OutOfTolerance, FileSource, NeedsSave, SortTitle, DateAdd, ", _
                                                                      "HasEFanarts, EFanartsPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasTheme, ThemePath, HasDiscArt, DiscArtPath, ", _
                                                                      "HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDB, TMDBColID, LastScrape, ", _
                                                                      "MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4 FROM movies ", _
                                                                      "WHERE ID IN (SELECT MovieID FROM MoviesActors WHERE Role LIKE '%", Me.filSearch, "%') ORDER BY ListTitle COLLATE NOCASE;"))
                Else
                    If Me.chkFilterDupe.Checked Then
                        Master.DB.FillDataTable(Me.dtMovies, "SELECT ID, MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, HasTrailer, HasSub, HasEThumbs, New, Mark, Source, Imdb, Lock, Title, OriginalTitle, Year, Rating, Votes, MPAA, Top250, Country, Outline, Plot, Tagline, Certification, Genre, Studio, Runtime, ReleaseDate, Director, Credits, Playcount, HasWatched, Trailer, PosterPath, FanartPath, EThumbsPath, NfoPath, TrailerPath, SubPath, FanartURL, UseFolder, OutOfTolerance, FileSource, NeedsSave, SortTitle, DateAdd, HasEFanarts, EFanartsPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasTheme, ThemePath, HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDB, TMDBColID, LastScrape, MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4 FROM movies WHERE imdb IN (SELECT imdb FROM movies WHERE imdb IS NOT NULL AND LENGTH(imdb) > 0 GROUP BY imdb HAVING ( COUNT(imdb) > 1 )) ORDER BY ListTitle COLLATE NOCASE;")
                    Else
                        Master.DB.FillDataTable(Me.dtMovies, "SELECT ID, MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, HasTrailer, HasSub, HasEThumbs, New, Mark, Source, Imdb, Lock, Title, OriginalTitle, Year, Rating, Votes, MPAA, Top250, Country, Outline, Plot, Tagline, Certification, Genre, Studio, Runtime, ReleaseDate, Director, Credits, Playcount, HasWatched, Trailer, PosterPath, FanartPath, EThumbsPath, NfoPath, TrailerPath, SubPath, FanartURL, UseFolder, OutOfTolerance, FileSource, NeedsSave, SortTitle, DateAdd, HasEFanarts, EFanartsPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasTheme, ThemePath, HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDB, TMDBColID, LastScrape, MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4 FROM movies ORDER BY ListTitle COLLATE NOCASE;")
                    End If
                End If
            End If



            If doMovieSets Then
                Me.bsMovieSets.DataSource = Nothing
                Me.dgvMovieSets.DataSource = Nothing
                Me.ClearInfo()
                Master.DB.FillDataTable(Me.dtMovieSets, "SELECT ID, ListTitle, HasNfo, NfoPath, HasPoster, PosterPath, HasFanart, FanartPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDBColID, Plot, SetName FROM sets ORDER BY ListTitle COLLATE NOCASE;")
            End If



            If doTVShows Then
                Me.bsShows.DataSource = Nothing
                Me.dgvTVShows.DataSource = Nothing
                Me.bsSeasons.DataSource = Nothing
                Me.dgvTVSeasons.DataSource = Nothing
                Me.bsEpisodes.DataSource = Nothing
                Me.dgvTVEpisodes.DataSource = Nothing
                Me.ClearInfo()
                Master.DB.FillDataTable(Me.dtShows, "SELECT ID, Title, HasPoster, HasFanart, HasNfo, New, Mark, TVShowPath, Source, TVDB, Lock, EpisodeGuide, Plot, Genre, Premiered, Studio, MPAA, Rating, PosterPath, FanartPath, NfoPath, NeedsSave, Language, Ordering, HasBanner, BannerPath, HasLandscape, LandscapePath, Status, HasTheme, ThemePath, HasCharacterArt, CharacterArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, HasEFanarts, EFanartsPath FROM TVShows ORDER BY Title COLLATE NOCASE;")
            End If

            If Master.isCL Then
                Me.LoadingDone = True
            Else
                If doMovies Then
                    If Me.dtMovies.Rows.Count > 0 Then
                        With Me
                            .bsMovies.DataSource = .dtMovies
                            .dgvMovies.DataSource = .bsMovies

                            .dgvMovies.Columns(0).Visible = False
                            .dgvMovies.Columns(1).Visible = False
                            .dgvMovies.Columns(2).Visible = False
                            .dgvMovies.Columns(3).Resizable = DataGridViewTriState.True
                            .dgvMovies.Columns(3).ReadOnly = True
                            .dgvMovies.Columns(3).MinimumWidth = 83
                            .dgvMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(3).ToolTipText = Master.eLang.GetString(21, "Title")
                            .dgvMovies.Columns(3).HeaderText = Master.eLang.GetString(21, "Title")
                            .dgvMovies.Columns(4).Width = 20
                            .dgvMovies.Columns(4).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(4).ReadOnly = True
                            .dgvMovies.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(4).Visible = Not Master.eSettings.MoviePosterCol
                            .dgvMovies.Columns(4).ToolTipText = Master.eLang.GetString(148, "Poster")
                            .dgvMovies.Columns(5).Width = 20
                            .dgvMovies.Columns(5).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(5).ReadOnly = True
                            .dgvMovies.Columns(5).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(5).Visible = Not Master.eSettings.MovieFanartCol
                            .dgvMovies.Columns(5).ToolTipText = Master.eLang.GetString(149, "Fanart")
                            .dgvMovies.Columns(6).Width = 20
                            .dgvMovies.Columns(6).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(6).ReadOnly = True
                            .dgvMovies.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(6).Visible = Not Master.eSettings.MovieNFOCol
                            .dgvMovies.Columns(6).ToolTipText = Master.eLang.GetString(150, "Nfo")
                            .dgvMovies.Columns(7).Width = 20
                            .dgvMovies.Columns(7).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(7).ReadOnly = True
                            .dgvMovies.Columns(7).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(7).Visible = Not Master.eSettings.MovieTrailerCol
                            .dgvMovies.Columns(7).ToolTipText = Master.eLang.GetString(151, "Trailer")
                            .dgvMovies.Columns(8).Width = 20
                            .dgvMovies.Columns(8).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(8).ReadOnly = True
                            .dgvMovies.Columns(8).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(8).Visible = Not Master.eSettings.MovieSubCol
                            .dgvMovies.Columns(8).ToolTipText = Master.eLang.GetString(152, "Subtitles")
                            .dgvMovies.Columns(9).Width = 20
                            .dgvMovies.Columns(9).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(9).ReadOnly = True
                            .dgvMovies.Columns(9).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(9).Visible = Not Master.eSettings.MovieEThumbsCol
                            .dgvMovies.Columns(9).ToolTipText = Master.eLang.GetString(153, "Extrathumbs")
                            .dgvMovies.Columns(10).Visible = False
                            .dgvMovies.Columns(11).Visible = False
                            .dgvMovies.Columns(12).Visible = False
                            .dgvMovies.Columns(13).Visible = False
                            .dgvMovies.Columns(14).Visible = False
                            .dgvMovies.Columns(15).Visible = False
                            .dgvMovies.Columns(16).Visible = False
                            .dgvMovies.Columns(17).Visible = False
                            .dgvMovies.Columns(18).Visible = False
                            .dgvMovies.Columns(19).Visible = False
                            .dgvMovies.Columns(20).Visible = False
                            .dgvMovies.Columns(21).Visible = False
                            .dgvMovies.Columns(22).Visible = False
                            .dgvMovies.Columns(23).Visible = False
                            .dgvMovies.Columns(24).Visible = False
                            .dgvMovies.Columns(25).Visible = False
                            .dgvMovies.Columns(26).Visible = False
                            .dgvMovies.Columns(27).Visible = False
                            .dgvMovies.Columns(28).Visible = False
                            .dgvMovies.Columns(29).Visible = False
                            .dgvMovies.Columns(30).Visible = False
                            .dgvMovies.Columns(31).Visible = False
                            .dgvMovies.Columns(32).Visible = False
                            .dgvMovies.Columns(33).Visible = False
                            .dgvMovies.Columns(34).Width = 20
                            .dgvMovies.Columns(34).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(34).ReadOnly = True
                            .dgvMovies.Columns(34).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(34).Visible = Not Master.eSettings.MovieWatchedCol
                            .dgvMovies.Columns(34).ToolTipText = Master.eLang.GetString(981, "Watched")
                            .dgvMovies.Columns(35).Visible = False
                            .dgvMovies.Columns(36).Visible = False
                            .dgvMovies.Columns(37).Visible = False
                            .dgvMovies.Columns(38).Visible = False
                            .dgvMovies.Columns(39).Visible = False
                            .dgvMovies.Columns(40).Visible = False
                            .dgvMovies.Columns(41).Visible = False
                            .dgvMovies.Columns(42).Visible = False
                            .dgvMovies.Columns(43).Visible = False
                            .dgvMovies.Columns(44).Visible = False
                            .dgvMovies.Columns(45).Visible = False
                            .dgvMovies.Columns(46).Visible = False
                            .dgvMovies.Columns(47).Visible = False
                            .dgvMovies.Columns(48).Visible = False
                            .dgvMovies.Columns(49).Width = 20
                            .dgvMovies.Columns(49).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(49).ReadOnly = True
                            .dgvMovies.Columns(49).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(49).Visible = Not Master.eSettings.MovieEFanartsCol
                            .dgvMovies.Columns(49).ToolTipText = Master.eLang.GetString(992, "Extrafanarts")
                            .dgvMovies.Columns(50).Visible = False
                            .dgvMovies.Columns(51).Width = 20
                            .dgvMovies.Columns(51).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(51).ReadOnly = True
                            .dgvMovies.Columns(51).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(51).Visible = Not Master.eSettings.MovieBannerCol
                            .dgvMovies.Columns(51).ToolTipText = Master.eLang.GetString(838, "Banner")
                            .dgvMovies.Columns(52).Visible = False
                            .dgvMovies.Columns(53).Width = 20
                            .dgvMovies.Columns(53).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(53).ReadOnly = True
                            .dgvMovies.Columns(53).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(53).Visible = Not Master.eSettings.MovieLandscapeCol
                            .dgvMovies.Columns(53).ToolTipText = Master.eLang.GetString(1035, "Landscape")
                            .dgvMovies.Columns(54).Visible = False
                            .dgvMovies.Columns(55).Width = 20
                            .dgvMovies.Columns(55).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(55).ReadOnly = True
                            .dgvMovies.Columns(55).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(55).Visible = Not Master.eSettings.MovieThemeCol
                            .dgvMovies.Columns(55).ToolTipText = Master.eLang.GetString(1118, "Theme")
                            .dgvMovies.Columns(56).Visible = False
                            .dgvMovies.Columns(57).Width = 20
                            .dgvMovies.Columns(57).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(57).ReadOnly = True
                            .dgvMovies.Columns(57).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(57).Visible = Not Master.eSettings.MovieDiscArtCol
                            .dgvMovies.Columns(57).ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                            .dgvMovies.Columns(58).Visible = False
                            .dgvMovies.Columns(59).Width = 20
                            .dgvMovies.Columns(59).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(59).ReadOnly = True
                            .dgvMovies.Columns(59).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(59).Visible = Not Master.eSettings.MovieClearLogoCol
                            .dgvMovies.Columns(59).ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                            .dgvMovies.Columns(60).Visible = False
                            .dgvMovies.Columns(61).Width = 20
                            .dgvMovies.Columns(61).Resizable = DataGridViewTriState.False
                            .dgvMovies.Columns(61).ReadOnly = True
                            .dgvMovies.Columns(61).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovies.Columns(61).Visible = Not Master.eSettings.MovieClearArtCol
                            .dgvMovies.Columns(61).ToolTipText = Master.eLang.GetString(1096, "ClearArt")

                            For i As Integer = 62 To .dgvMovies.Columns.Count - 1
                                .dgvMovies.Columns(i).Visible = False
                            Next

                            .dgvMovies.Columns(0).ValueType = GetType(Int32)

                            If Master.isWindows Then .dgvMovies.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            ResizeMoviesList()

                            If .dgvMovies.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 0 Then
                                .dgvMovies.Sort(.dgvMovies.Columns(3), ComponentModel.ListSortDirection.Ascending)
                                .SetControlsEnabled(True)
                            End If

                        End With
                    End If
                End If

                If doMovieSets Then
                    Me.dgvMovieSets.Enabled = False
                    If Me.dtMovieSets.Rows.Count > 0 Then
                        With Me
                            .bsMovieSets.DataSource = .dtMovieSets
                            .dgvMovieSets.DataSource = .bsMovieSets

                            .dgvMovieSets.Columns(0).Visible = False
                            .dgvMovieSets.Columns(1).Resizable = DataGridViewTriState.True
                            .dgvMovieSets.Columns(1).ReadOnly = True
                            .dgvMovieSets.Columns(1).MinimumWidth = 83
                            .dgvMovieSets.Columns(1).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(1).ToolTipText = Master.eLang.GetString(21, "Title")
                            .dgvMovieSets.Columns(1).HeaderText = Master.eLang.GetString(21, "Title")
                            .dgvMovieSets.Columns(2).Width = 20
                            .dgvMovieSets.Columns(2).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(2).ReadOnly = True
                            .dgvMovieSets.Columns(2).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(2).Visible = Not Master.eSettings.MovieSetNfoCol
                            .dgvMovieSets.Columns(2).ToolTipText = Master.eLang.GetString(150, "Nfo")
                            .dgvMovieSets.Columns(3).Visible = False
                            .dgvMovieSets.Columns(4).Width = 20
                            .dgvMovieSets.Columns(4).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(4).ReadOnly = True
                            .dgvMovieSets.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(4).Visible = Not Master.eSettings.MovieSetPosterCol
                            .dgvMovieSets.Columns(4).ToolTipText = Master.eLang.GetString(148, "Poster")
                            .dgvMovieSets.Columns(5).Visible = False
                            .dgvMovieSets.Columns(6).Width = 20
                            .dgvMovieSets.Columns(6).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(6).ReadOnly = True
                            .dgvMovieSets.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(6).Visible = Not Master.eSettings.MovieSetFanartCol
                            .dgvMovieSets.Columns(6).ToolTipText = Master.eLang.GetString(149, "Fanart")
                            .dgvMovieSets.Columns(7).Visible = False
                            .dgvMovieSets.Columns(8).Width = 20
                            .dgvMovieSets.Columns(8).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(8).ReadOnly = True
                            .dgvMovieSets.Columns(8).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(8).Visible = Not Master.eSettings.MovieSetBannerCol
                            .dgvMovieSets.Columns(8).ToolTipText = Master.eLang.GetString(838, "Banner")
                            .dgvMovieSets.Columns(9).Visible = False
                            .dgvMovieSets.Columns(10).Width = 20
                            .dgvMovieSets.Columns(10).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(10).ReadOnly = True
                            .dgvMovieSets.Columns(10).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(10).Visible = Not Master.eSettings.MovieSetLandscapeCol
                            .dgvMovieSets.Columns(10).ToolTipText = Master.eLang.GetString(1035, "Landscape")
                            .dgvMovieSets.Columns(11).Visible = False
                            .dgvMovieSets.Columns(12).Width = 20
                            .dgvMovieSets.Columns(12).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(12).ReadOnly = True
                            .dgvMovieSets.Columns(12).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(12).Visible = Not Master.eSettings.MovieSetDiscArtCol
                            .dgvMovieSets.Columns(12).ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                            .dgvMovieSets.Columns(13).Visible = False
                            .dgvMovieSets.Columns(14).Width = 20
                            .dgvMovieSets.Columns(14).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(14).ReadOnly = True
                            .dgvMovieSets.Columns(14).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(14).Visible = Not Master.eSettings.MovieSetClearLogoCol
                            .dgvMovieSets.Columns(14).ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                            .dgvMovieSets.Columns(15).Visible = False
                            .dgvMovieSets.Columns(16).Width = 20
                            .dgvMovieSets.Columns(16).Resizable = DataGridViewTriState.False
                            .dgvMovieSets.Columns(16).ReadOnly = True
                            .dgvMovieSets.Columns(16).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvMovieSets.Columns(16).Visible = Not Master.eSettings.MovieSetClearArtCol
                            .dgvMovieSets.Columns(16).ToolTipText = Master.eLang.GetString(1096, "ClearArt")

                            For i As Integer = 17 To .dgvMovieSets.Columns.Count - 1
                                .dgvMovieSets.Columns(i).Visible = False
                            Next

                            .dgvMovieSets.Columns(0).ValueType = GetType(Int32)

                            If Master.isWindows Then .dgvMovieSets.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            ResizeMovieSetsList()

                            If .dgvMovieSets.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 1 Then
                                .dgvMovieSets.Sort(.dgvMovieSets.Columns(1), ComponentModel.ListSortDirection.Ascending)
                                .SetControlsEnabled(True)
                            End If

                        End With
                        Me.dgvMovieSets.Enabled = True
                    End If
                End If

                If doTVShows Then
                    Me.dgvTVShows.Enabled = False
                    If Me.dtShows.Rows.Count > 0 Then
                        With Me
                            .bsShows.DataSource = .dtShows
                            .dgvTVShows.DataSource = .bsShows

                            .dgvTVShows.Columns(0).Visible = False
                            .dgvTVShows.Columns(1).Resizable = DataGridViewTriState.True
                            .dgvTVShows.Columns(1).ReadOnly = True
                            .dgvTVShows.Columns(1).MinimumWidth = 83
                            .dgvTVShows.Columns(1).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(1).ToolTipText = Master.eLang.GetString(21, "Title")
                            .dgvTVShows.Columns(1).HeaderText = Master.eLang.GetString(21, "Title")
                            .dgvTVShows.Columns(2).Width = 20
                            .dgvTVShows.Columns(2).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(2).ReadOnly = True
                            .dgvTVShows.Columns(2).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(2).Visible = Not Master.eSettings.TVShowPosterCol
                            .dgvTVShows.Columns(2).ToolTipText = Master.eLang.GetString(148, "Poster")
                            .dgvTVShows.Columns(3).Width = 20
                            .dgvTVShows.Columns(3).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(3).ReadOnly = True
                            .dgvTVShows.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(3).Visible = Not Master.eSettings.TVShowFanartCol
                            .dgvTVShows.Columns(3).ToolTipText = Master.eLang.GetString(149, "Fanart")
                            .dgvTVShows.Columns(4).Width = 20
                            .dgvTVShows.Columns(4).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(4).ReadOnly = True
                            .dgvTVShows.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(4).Visible = Not Master.eSettings.TVShowNfoCol
                            .dgvTVShows.Columns(4).ToolTipText = Master.eLang.GetString(150, "Nfo")
                            .dgvTVShows.Columns(5).Visible = False
                            .dgvTVShows.Columns(6).Visible = False
                            .dgvTVShows.Columns(7).Visible = False
                            .dgvTVShows.Columns(8).Visible = False
                            .dgvTVShows.Columns(9).Visible = False
                            .dgvTVShows.Columns(10).Visible = False
                            .dgvTVShows.Columns(11).Visible = False
                            .dgvTVShows.Columns(12).Visible = False
                            .dgvTVShows.Columns(13).Visible = False
                            .dgvTVShows.Columns(14).Visible = False
                            .dgvTVShows.Columns(15).Visible = False
                            .dgvTVShows.Columns(16).Visible = False
                            .dgvTVShows.Columns(17).Visible = False
                            .dgvTVShows.Columns(18).Visible = False
                            .dgvTVShows.Columns(19).Visible = False
                            .dgvTVShows.Columns(20).Visible = False
                            .dgvTVShows.Columns(21).Visible = False
                            .dgvTVShows.Columns(22).Visible = False
                            .dgvTVShows.Columns(23).Visible = False
                            .dgvTVShows.Columns(24).Width = 20
                            .dgvTVShows.Columns(24).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(24).ReadOnly = True
                            .dgvTVShows.Columns(24).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(24).Visible = Not Master.eSettings.TVShowBannerCol
                            .dgvTVShows.Columns(24).ToolTipText = Master.eLang.GetString(838, "Banner")
                            .dgvTVShows.Columns(25).Visible = False
                            .dgvTVShows.Columns(26).Width = 20
                            .dgvTVShows.Columns(26).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(26).ReadOnly = True
                            .dgvTVShows.Columns(26).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(26).Visible = Not Master.eSettings.TVShowLandscapeCol
                            .dgvTVShows.Columns(26).ToolTipText = Master.eLang.GetString(1035, "Landscape")
                            .dgvTVShows.Columns(27).Visible = False
                            .dgvTVShows.Columns(28).Visible = False
                            .dgvTVShows.Columns(29).Width = 20
                            .dgvTVShows.Columns(29).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(29).ReadOnly = True
                            .dgvTVShows.Columns(29).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(29).Visible = Not Master.eSettings.TVShowThemeCol
                            .dgvTVShows.Columns(29).ToolTipText = Master.eLang.GetString(1118, "Theme")
                            .dgvTVShows.Columns(30).Visible = False
                            .dgvTVShows.Columns(31).Width = 20
                            .dgvTVShows.Columns(31).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(31).ReadOnly = True
                            .dgvTVShows.Columns(31).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(31).Visible = Not Master.eSettings.TVShowCharacterArtCol
                            .dgvTVShows.Columns(31).ToolTipText = Master.eLang.GetString(1140, "CharacterArt")
                            .dgvTVShows.Columns(32).Visible = False
                            .dgvTVShows.Columns(33).Width = 20
                            .dgvTVShows.Columns(33).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(33).ReadOnly = True
                            .dgvTVShows.Columns(33).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(33).Visible = Not Master.eSettings.TVShowClearLogoCol
                            .dgvTVShows.Columns(33).ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                            .dgvTVShows.Columns(34).Visible = False
                            .dgvTVShows.Columns(35).Width = 20
                            .dgvTVShows.Columns(35).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(35).ReadOnly = True
                            .dgvTVShows.Columns(35).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(35).Visible = Not Master.eSettings.TVShowClearArtCol
                            .dgvTVShows.Columns(35).ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                            .dgvTVShows.Columns(36).Visible = False
                            .dgvTVShows.Columns(37).Width = 20
                            .dgvTVShows.Columns(37).Resizable = DataGridViewTriState.False
                            .dgvTVShows.Columns(37).ReadOnly = True
                            .dgvTVShows.Columns(37).SortMode = DataGridViewColumnSortMode.Automatic
                            .dgvTVShows.Columns(37).Visible = Not Master.eSettings.TVShowEFanartsCol
                            .dgvTVShows.Columns(37).ToolTipText = Master.eLang.GetString(992, "Extrafanarts")

                            For i As Integer = 38 To .dgvTVShows.Columns.Count - 1
                                .dgvTVShows.Columns(i).Visible = False
                            Next

                            .dgvTVShows.Columns(0).ValueType = GetType(Int32)

                            If Master.isWindows Then .dgvTVShows.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            ResizeTVLists(1)

                            If .dgvTVShows.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 2 Then
                                .dgvTVShows.Sort(.dgvTVShows.Columns(1), ComponentModel.ListSortDirection.Ascending)
                                .SetControlsEnabled(True)
                            End If
                        End With
                    End If
                    Me.dgvTVShows.Enabled = True
                End If
            End If

            If Me.dtMovies.Rows.Count = 0 AndAlso Me.dtMovieSets.Rows.Count = 0 AndAlso Me.dtShows.Rows.Count = 0 Then
                Me.SetControlsEnabled(False, False, False)
                Me.SetStatus(String.Empty)
                Me.ClearInfo()
            End If
        Catch ex As Exception
            Me.LoadingDone = True
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        If Not Master.isCL Then
            Me.mnuUpdate.Enabled = True
            Me.cmnuTrayExit.Enabled = True
            Me.cmnuTraySettings.Enabled = True
            Me.mnuMainEdit.Enabled = True
            Me.cmnuTrayUpdate.Enabled = True
            Me.mnuMainHelp.Enabled = True
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.tspbLoading.Value = 0
            Me.tcMain.Enabled = True
            Me.DoTitleCheck()
            Me.EnableFilters(True)
            Me.SetMovieSetCount()
            Me.SetTVCount()
        End If
    End Sub

    Private Sub fillScreenInfoWithEpisode()
        Dim g As Graphics
        Dim strSize As String
        Dim lenSize As Integer
        Dim rect As Rectangle

        Try
            Me.SuspendLayout()
            Me.lblTitle.Text = If(String.IsNullOrEmpty(Master.currShow.Filename), String.Concat(Master.currShow.TVEp.Title, Master.eLang.GetString(689, " [MISSING]")), Master.currShow.TVEp.Title)
            Me.txtPlot.Text = Master.currShow.TVEp.Plot
            Me.lblDirector.Text = Master.currShow.TVEp.Director
            Me.txtFilePath.Text = Master.currShow.Filename
            Me.lblRuntime.Text = String.Format(Master.eLang.GetString(647, "Aired: {0}"), If(String.IsNullOrEmpty(Master.currShow.TVEp.Aired), "?", Master.currShow.TVEp.Aired))

            Me.lblTagline.Text = String.Format(Master.eLang.GetString(648, "Season: {0}, Episode: {1}"), _
                            If(String.IsNullOrEmpty(Master.currShow.TVEp.Season.ToString), "?", Master.currShow.TVEp.Season.ToString), _
                            If(String.IsNullOrEmpty(Master.currShow.TVEp.Episode.ToString), "?", Master.currShow.TVEp.Episode.ToString))

            Me.alActors = New List(Of String)

            If Master.currShow.TVEp.Actors.Count > 0 Then
                Me.pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In Master.currShow.TVEp.Actors
                    If Not String.IsNullOrEmpty(imdbAct.Thumb) Then
                        If Not imdbAct.Thumb.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.Thumb.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.Thumb)
                        Else
                            Me.alActors.Add("none")
                        End If
                    Else
                        Me.alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        Me.lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                Me.lstActors.SelectedIndex = 0
            End If

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.MPAA) Then
                Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(Master.currShow.TVShow.MPAA)
                If Not IsNothing(tmpRatingImg) Then
                    Me.pbMPAA.Image = tmpRatingImg
                    Me.MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(Master.currShow.TVEp.Rating)
            If tmpRating > 0 Then
                Me.BuildStars(tmpRating)
            End If

            If Master.currShow.TVShow.Genres.Count > 0 Then
                Me.createGenreThumbs(Master.currShow.TVShow.Genres)
            End If

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Studio) Then
                Me.pbStudio.Image = APIXML.GetStudioImage(Master.currShow.TVShow.Studio.ToLower) 'ByDef all images file a lower case
                Me.pbStudio.Tag = Master.currShow.TVShow.Studio
            Else
                Me.pbStudio.Image = APIXML.GetStudioImage("####")
                Me.pbStudio.Tag = String.Empty
            End If
            If clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then
                lblStudio.Text = pbStudio.Tag.ToString
            End If
            If Master.eSettings.TVScraperMetaDataScan AndAlso Not String.IsNullOrEmpty(Master.currShow.Filename) Then
                Me.SetAVImages(APIXML.GetAVImages(Master.currShow.TVEp.FileInfo, Master.currShow.Filename, True, APIXML.GetFileSource(Master.currShow.Filename)))
                Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
            Else
                Me.pnlInfoIcons.Width = pbStudio.Width + 1
                Me.pbStudio.Left = 0
            End If

            Me.txtMetaData.Text = NFO.FIToString(Master.currShow.TVEp.FileInfo, True)

            If Not IsNothing(Me.MainPoster.Image) Then
                Me.pbPosterCache.Image = Me.MainPoster.Image
                ImageUtils.ResizePB(Me.pbPoster, Me.pbPosterCache, Me.PosterMaxHeight, Me.PosterMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbPoster)
                Me.pnlPoster.Size = New Size(Me.pbPoster.Width + 10, Me.pbPoster.Height + 10)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbPoster.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainPoster.Image.Width, Me.MainPoster.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2 - 15), Me.pbPoster.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2), Me.pbPoster.Height - 20)
                End If

                Me.pbPoster.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbPoster.Image) Then
                    Me.pbPoster.Image.Dispose()
                    Me.pbPoster.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanartSmall.Image) Then
                Me.pbFanartSmallCache.Image = Me.MainFanartSmall.Image
                ImageUtils.ResizePB(Me.pbFanartSmall, Me.pbFanartSmallCache, Me.FanartSmallMaxHeight, Me.FanartSmallMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbFanartSmall)
                Me.pnlFanartSmall.Size = New Size(Me.pbFanartSmall.Width + 10, Me.pbFanartSmall.Height + 10)
                Me.pnlFanartSmall.Location = New Point(Me.pnlPoster.Location.X + Me.pnlPoster.Width + 10, Me.pnlPoster.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanartSmall.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanartSmall.Image.Width, Me.MainFanartSmall.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2 - 15), Me.pbFanartSmall.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2), Me.pbFanartSmall.Height - 20)
                End If

                Me.pbFanartSmall.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbFanartSmall.Image) Then
                    Me.pbFanartSmall.Image.Dispose()
                    Me.pbFanartSmall.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainLandscape.Image) Then
                Me.pbLandscapeCache.Image = Me.MainLandscape.Image
                ImageUtils.ResizePB(Me.pbLandscape, Me.pbLandscapeCache, Me.LandscapeMaxHeight, Me.LandscapeMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbLandscape)
                Me.pnlLandscape.Size = New Size(Me.pbLandscape.Width + 10, Me.pbLandscape.Height + 10)
                Me.pnlLandscape.Location = New Point(Me.pnlFanartSmall.Location.X + Me.pnlFanartSmall.Width + 10, Me.pnlFanartSmall.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbLandscape.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainLandscape.Image.Width, Me.MainLandscape.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2 - 15), Me.pbLandscape.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2), Me.pbLandscape.Height - 20)
                End If

                Me.pbLandscape.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbLandscape.Image) Then
                    Me.pbLandscape.Image.Dispose()
                    Me.pbLandscape.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainClearArt.Image) Then
                Me.pbClearArtCache.Image = Me.MainClearArt.Image
                ImageUtils.ResizePB(Me.pbClearArt, Me.pbClearArtCache, Me.ClearArtMaxHeight, Me.ClearArtMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbClearArt)
                Me.pnlClearArt.Size = New Size(Me.pbClearArt.Width + 10, Me.pbClearArt.Height + 10)
                Me.pnlClearArt.Location = New Point(Me.pnlLandscape.Location.X + Me.pnlLandscape.Width + 10, Me.pnlLandscape.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbClearArt.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainClearArt.Image.Width, Me.MainClearArt.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2 - 15), Me.pbClearArt.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2), Me.pbClearArt.Height - 20)
                End If

                Me.pbClearArt.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbClearArt.Image) Then
                    Me.pbClearArt.Image.Dispose()
                    Me.pbClearArt.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanart.Image) Then
                Me.pbFanartCache.Image = Me.MainFanart.Image

                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)

                If Not IsNothing(pbFanart.Image) AndAlso Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanart.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanart.Image.Width, Me.MainFanart.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2 - 15), Me.pbFanart.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((Me.pbFanart.Image.Width - lenSize) / 2), Me.pbFanart.Height - 20)
                End If
            Else
                If Not IsNothing(Me.pbFanartCache.Image) Then
                    Me.pbFanartCache.Image.Dispose()
                    Me.pbFanartCache.Image = Nothing
                End If
                If Not IsNothing(Me.pbFanart.Image) Then
                    Me.pbFanart.Image.Dispose()
                    Me.pbFanart.Image = Nothing
                End If
            End If

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwRefreshMovies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.dgvTVEpisodes.Focus()
            Else
                Me.dgvTVEpisodes.Enabled = True
                Me.dgvTVSeasons.Enabled = True
                Me.dgvTVShows.Enabled = True
                Me.dgvTVEpisodes.Focus()
            End If

            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Not IsNothing(Me.pbAllSeason.Image) Then Me.pnlAllSeason.Visible = True
            If Not IsNothing(Me.pbClearArt.Image) Then Me.pnlClearArt.Visible = True
            If Not IsNothing(Me.pbPoster.Image) Then Me.pnlPoster.Visible = True
            If Not IsNothing(Me.pbFanartSmall.Image) Then Me.pnlFanartSmall.Visible = True
            If Not IsNothing(Me.pbLandscape.Image) Then Me.pnlLandscape.Visible = True
            If Not IsNothing(Me.pbMPAA.Image) Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To UBound(Me.pnlGenre)
                Me.pnlGenre(i).Visible = True
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub fillScreenInfoWithMovie()
        Dim g As Graphics
        Dim strSize As String
        Dim lenSize As Integer
        Dim rect As Rectangle

        Try
            Me.SuspendLayout()
            If Not String.IsNullOrEmpty(Master.currMovie.Movie.Title) AndAlso Not String.IsNullOrEmpty(Master.currMovie.Movie.Year) Then
                Me.lblTitle.Text = String.Format("{0} ({1})", Master.currMovie.Movie.Title, Master.currMovie.Movie.Year)
            ElseIf Not String.IsNullOrEmpty(Master.currMovie.Movie.Title) AndAlso String.IsNullOrEmpty(Master.currMovie.Movie.Year) Then
                Me.lblTitle.Text = Master.currMovie.Movie.Title
            ElseIf String.IsNullOrEmpty(Master.currMovie.Movie.Title) AndAlso Not String.IsNullOrEmpty(Master.currMovie.Movie.Year) Then
                Me.lblTitle.Text = String.Format(Master.eLang.GetString(117, "Unknown Movie ({0})"), Master.currMovie.Movie.Year)
            End If

            If Not String.IsNullOrEmpty(Master.currMovie.Movie.OriginalTitle) AndAlso Master.currMovie.Movie.OriginalTitle <> StringUtils.FilterTokens_Movie(Master.currMovie.Movie.Title) Then
                Me.lblOriginalTitle.Text = String.Format(String.Concat(Master.eLang.GetString(302, "Original Title"), ": {0}"), Master.currMovie.Movie.OriginalTitle)
            Else
                Me.lblOriginalTitle.Text = String.Empty
            End If

            If Not String.IsNullOrEmpty(Master.currMovie.Movie.Votes) Then
                Me.lblRating.Text = String.Concat(Master.currMovie.Movie.Rating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), Master.currMovie.Movie.Votes), ")")
            End If

            If Not String.IsNullOrEmpty(Master.currMovie.Movie.Runtime) Then
                Me.lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), If(Master.currMovie.Movie.Runtime.Contains("|"), Microsoft.VisualBasic.Strings.Left(Master.currMovie.Movie.Runtime, Master.currMovie.Movie.Runtime.IndexOf("|")), Master.currMovie.Movie.Runtime)).Trim
            End If

            If Not String.IsNullOrEmpty(Master.currMovie.Movie.Top250) AndAlso IsNumeric(Master.currMovie.Movie.Top250) AndAlso (IsNumeric(Master.currMovie.Movie.Top250) AndAlso Convert.ToInt32(Master.currMovie.Movie.Top250) > 0) Then
                Me.pnlTop250.Visible = True
                Me.lblTop250.Text = Master.currMovie.Movie.Top250
            Else
                Me.pnlTop250.Visible = False
            End If

            Me.txtOutline.Text = Master.currMovie.Movie.Outline
            Me.txtPlot.Text = Master.currMovie.Movie.Plot
            Me.lblTagline.Text = Master.currMovie.Movie.Tagline

            Me.alActors = New List(Of String)

            If Master.currMovie.Movie.Actors.Count > 0 Then
                Me.pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In Master.currMovie.Movie.Actors
                    If Not String.IsNullOrEmpty(imdbAct.Thumb) Then
                        If Not imdbAct.Thumb.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.Thumb.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.Thumb)
                        Else
                            Me.alActors.Add("none")
                        End If
                    Else
                        Me.alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        Me.lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                Me.lstActors.SelectedIndex = 0
            End If

            If Not String.IsNullOrEmpty(Master.currMovie.Movie.MPAA) Then
                Dim tmpRatingImg As Image = APIXML.GetRatingImage(Master.currMovie.Movie.MPAA)
                If Not IsNothing(tmpRatingImg) Then
                    Me.pbMPAA.Image = tmpRatingImg
                    Me.MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(Master.currMovie.Movie.Rating)
            If tmpRating > 0 Then
                Me.BuildStars(tmpRating)
            End If

            If Master.currMovie.Movie.Genres.Count > 0 Then
                Me.createGenreThumbs(Master.currMovie.Movie.Genres)
            End If

            If Not String.IsNullOrEmpty(Master.currMovie.Movie.Studio) Then
                Me.pbStudio.Image = APIXML.GetStudioImage(Master.currMovie.Movie.Studio.ToLower) 'ByDef all images file a lower case
                Me.pbStudio.Tag = Master.currMovie.Movie.Studio
            Else
                Me.pbStudio.Image = APIXML.GetStudioImage("####")
                Me.pbStudio.Tag = String.Empty
            End If
            If clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then
                lblStudio.Text = pbStudio.Tag.ToString
            End If
            If Master.eSettings.MovieScraperMetaDataScan Then
                'Me.SetAVImages(APIXML.GetAVImages(Master.currMovie.Movie.FileInfo, Master.currMovie.Filename, False))
                Me.SetAVImages(APIXML.GetAVImages(Master.currMovie.Movie.FileInfo, Master.currMovie.Filename, False, Master.currMovie.FileSource))
                Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
            Else
                Me.pnlInfoIcons.Width = pbStudio.Width + 1
                Me.pbStudio.Left = 0
            End If

            Me.lblDirector.Text = Master.currMovie.Movie.Director

            Me.txtIMDBID.Text = Master.currMovie.Movie.IMDBID

            Me.txtFilePath.Text = Master.currMovie.Filename

            Me.lblReleaseDate.Text = Master.currMovie.Movie.ReleaseDate
            Me.txtCerts.Text = Master.currMovie.Movie.Certification

            Me.txtMetaData.Text = NFO.FIToString(Master.currMovie.Movie.FileInfo, False)

            If Not IsNothing(Me.MainPoster.Image) Then
                Me.pbPosterCache.Image = Me.MainPoster.Image
                ImageUtils.ResizePB(Me.pbPoster, Me.pbPosterCache, Me.PosterMaxHeight, Me.PosterMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbPoster)
                Me.pnlPoster.Size = New Size(Me.pbPoster.Width + 10, Me.pbPoster.Height + 10)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbPoster.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainPoster.Image.Width, Me.MainPoster.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2 - 15), Me.pbPoster.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2), Me.pbPoster.Height - 20)
                End If

                Me.pbPoster.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbPoster.Image) Then
                    Me.pbPoster.Image.Dispose()
                    Me.pbPoster.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanartSmall.Image) Then
                Me.pbFanartSmallCache.Image = Me.MainFanartSmall.Image
                ImageUtils.ResizePB(Me.pbFanartSmall, Me.pbFanartSmallCache, Me.FanartSmallMaxHeight, Me.FanartSmallMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbFanartSmall)
                Me.pnlFanartSmall.Size = New Size(Me.pbFanartSmall.Width + 10, Me.pbFanartSmall.Height + 10)
                Me.pnlFanartSmall.Location = New Point(Me.pnlPoster.Location.X + Me.pnlPoster.Width + 10, Me.pnlPoster.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanartSmall.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanartSmall.Image.Width, Me.MainFanartSmall.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2 - 15), Me.pbFanartSmall.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2), Me.pbFanartSmall.Height - 20)
                End If

                Me.pbFanartSmall.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbFanartSmall.Image) Then
                    Me.pbFanartSmall.Image.Dispose()
                    Me.pbFanartSmall.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainLandscape.Image) Then
                Me.pbLandscapeCache.Image = Me.MainLandscape.Image
                ImageUtils.ResizePB(Me.pbLandscape, Me.pbLandscapeCache, Me.LandscapeMaxHeight, Me.LandscapeMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbLandscape)
                Me.pnlLandscape.Size = New Size(Me.pbLandscape.Width + 10, Me.pbLandscape.Height + 10)
                Me.pnlLandscape.Location = New Point(Me.pnlFanartSmall.Location.X + Me.pnlFanartSmall.Width + 10, Me.pnlFanartSmall.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbLandscape.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainLandscape.Image.Width, Me.MainLandscape.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2 - 15), Me.pbLandscape.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2), Me.pbLandscape.Height - 20)
                End If

                Me.pbLandscape.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbLandscape.Image) Then
                    Me.pbLandscape.Image.Dispose()
                    Me.pbLandscape.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainClearArt.Image) Then
                Me.pbClearArtCache.Image = Me.MainClearArt.Image
                ImageUtils.ResizePB(Me.pbClearArt, Me.pbClearArtCache, Me.ClearArtMaxHeight, Me.ClearArtMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbClearArt)
                Me.pnlClearArt.Size = New Size(Me.pbClearArt.Width + 10, Me.pbClearArt.Height + 10)
                Me.pnlClearArt.Location = New Point(Me.pnlLandscape.Location.X + Me.pnlLandscape.Width + 10, Me.pnlLandscape.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbClearArt.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainClearArt.Image.Width, Me.MainClearArt.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2 - 15), Me.pbClearArt.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2), Me.pbClearArt.Height - 20)
                End If

                Me.pbClearArt.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbClearArt.Image) Then
                    Me.pbClearArt.Image.Dispose()
                    Me.pbClearArt.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanart.Image) Then
                Me.pbFanartCache.Image = Me.MainFanart.Image

                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)

                If Not IsNothing(pbFanart.Image) AndAlso Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanart.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanart.Image.Width, Me.MainFanart.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2 - 15), Me.pbFanart.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((Me.pbFanart.Image.Width - lenSize) / 2), Me.pbFanart.Height - 20)
                End If
            Else
                If Not IsNothing(Me.pbFanartCache.Image) Then
                    Me.pbFanartCache.Image.Dispose()
                    Me.pbFanartCache.Image = Nothing
                End If
                If Not IsNothing(Me.pbFanart.Image) Then
                    Me.pbFanart.Image.Dispose()
                    Me.pbFanart.Image = Nothing
                End If
            End If

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwRefreshMovies.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not Me.bwNonScrape.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.EnableFilters(True)
            Else
                Me.dgvMovies.Enabled = True
            End If
            If bDoingSearch Then
                Me.txtSearch.Focus()
                bDoingSearch = False
            Else
                Me.dgvMovies.Focus()
            End If


            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Not IsNothing(Me.pbClearArt.Image) Then Me.pnlClearArt.Visible = True
            If Not IsNothing(Me.pbPoster.Image) Then Me.pnlPoster.Visible = True
            If Not IsNothing(Me.pbFanartSmall.Image) Then Me.pnlFanartSmall.Visible = True
            If Not IsNothing(Me.pbLandscape.Image) Then Me.pnlLandscape.Visible = True
            If Not IsNothing(Me.pbMPAA.Image) Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To UBound(Me.pnlGenre)
                Me.pnlGenre(i).Visible = True
            Next
            'Me.SetStatus(Master.currMovie.Filename)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub fillScreenInfoWithMovieSet()
        Dim g As Graphics
        Dim strSize As String
        Dim lenSize As Integer
        Dim rect As Rectangle

        Try
            Me.SuspendLayout()
            If Not String.IsNullOrEmpty(Master.currMovieSet.ListTitle) AndAlso Not IsNothing(Master.currMovieSet.Movies) AndAlso Master.currMovieSet.Movies.Count > 0 Then
                Me.lblTitle.Text = String.Format("{0} ({1})", Master.currMovieSet.ListTitle, Master.currMovieSet.Movies.Count)
            ElseIf Not String.IsNullOrEmpty(Master.currMovieSet.ListTitle) Then
                Me.lblTitle.Text = Master.currMovieSet.ListTitle
            Else
                Me.lblTitle.Text = String.Empty
            End If

            Me.txtPlot.Text = Master.currMovieSet.MovieSet.Plot

            'If Not String.IsNullOrEmpty(Master.currMovie.Movie.OriginalTitle) AndAlso Master.currMovie.Movie.OriginalTitle <> StringUtils.FilterTokens(Master.currMovie.Movie.Title) Then
            '    Me.lblOriginalTitle.Text = String.Format(String.Concat(Master.eLang.GetString(302, "Original Title"), ": {0}"), Master.currMovie.Movie.OriginalTitle)
            'Else
            '    Me.lblOriginalTitle.Text = String.Empty
            'End If

            'If Not String.IsNullOrEmpty(Master.currMovie.Movie.Votes) Then
            '    Me.lblVotes.Text = String.Format(Master.eLang.GetString(118, "{0} Votes"), Master.currMovie.Movie.Votes)
            'End If

            'If Not String.IsNullOrEmpty(Master.currMovie.Movie.Runtime) Then
            '    Me.lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), If(Master.currMovie.Movie.Runtime.Contains("|"), Microsoft.VisualBasic.Strings.Left(Master.currMovie.Movie.Runtime, Master.currMovie.Movie.Runtime.IndexOf("|")), Master.currMovie.Movie.Runtime)).Trim
            'End If

            'If Not String.IsNullOrEmpty(Master.currMovie.Movie.Top250) AndAlso IsNumeric(Master.currMovie.Movie.Top250) AndAlso (IsNumeric(Master.currMovie.Movie.Top250) AndAlso Convert.ToInt32(Master.currMovie.Movie.Top250) > 0) Then
            '    Me.pnlTop250.Visible = True
            '    Me.lblTop250.Text = Master.currMovie.Movie.Top250
            'Else
            '    Me.pnlTop250.Visible = False
            'End If

            'Me.txtOutline.Text = Master.currMovie.Movie.Outline
            'Me.lblTagline.Text = Master.currMovie.Movie.Tagline

            'Me.alActors = New List(Of String)

            'If Master.currMovie.Movie.Actors.Count > 0 Then
            '    Me.pbActors.Image = My.Resources.actor_silhouette
            '    For Each imdbAct As MediaContainers.Person In Master.currMovie.Movie.Actors
            '        If Not String.IsNullOrEmpty(imdbAct.Thumb) Then
            '            If Not imdbAct.Thumb.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.Thumb.ToLower.IndexOf("no_photo") > 0 Then
            '                Me.alActors.Add(imdbAct.Thumb)
            '            Else
            '                Me.alActors.Add("none")
            '            End If
            '        Else
            '            Me.alActors.Add("none")
            '        End If

            '        If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
            '            Me.lstActors.Items.Add(imdbAct.Name.Trim)
            '        Else
            '            Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
            '        End If
            '    Next
            '    Me.lstActors.SelectedIndex = 0
            'End If

            Me.alMoviesInSet = New List(Of String)

            If Not IsNothing(Master.currMovieSet.Movies) AndAlso Master.currMovieSet.Movies.Count > 0 Then
                'Me.pbActors.Image = My.Resources.actor_silhouette
                For Each Movie As Structures.DBMovie In Master.currMovieSet.Movies

                    If String.IsNullOrEmpty(Movie.Movie.Title) Then
                        Me.lstMoviesInSet.Items.Add("Unknow Movie")
                    Else
                        Me.lstMoviesInSet.Items.Add(Movie.Movie.Title)
                    End If
                Next
                Me.lstMoviesInSet.SelectedIndex = 0
            End If

            'If Not String.IsNullOrEmpty(Master.currMovie.Movie.MPAA) Then
            '    Dim tmpRatingImg As Image = APIXML.GetRatingImage(Master.currMovie.Movie.MPAA)
            '    If Not IsNothing(tmpRatingImg) Then
            '        Me.pbMPAA.Image = tmpRatingImg
            '        Me.MoveMPAA()
            '    End If
            'End If

            'Dim tmpRating As Single = NumUtils.ConvertToSingle(Master.currMovie.Movie.Rating)
            'If tmpRating > 0 Then
            '    Me.BuildStars(tmpRating)
            'End If

            'If Master.currMovie.Movie.Genres.Count > 0 Then
            '    Me.createGenreThumbs(Master.currMovie.Movie.Genres)
            'End If

            'If Not String.IsNullOrEmpty(Master.currMovie.Movie.Studio) Then
            '    Me.pbStudio.Image = APIXML.GetStudioImage(Master.currMovie.Movie.Studio.ToLower) 'ByDef all images file a lower case
            '    Me.pbStudio.Tag = Master.currMovie.Movie.Studio
            'Else
            '    Me.pbStudio.Image = APIXML.GetStudioImage("####")
            '    Me.pbStudio.Tag = String.Empty
            'End If
            'If AdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then
            '    lblStudio.Text = pbStudio.Tag.ToString
            'End If
            'If Master.eSettings.MovieScraperMetaDataScan Then
            '    'Me.SetAVImages(APIXML.GetAVImages(Master.currMovie.Movie.FileInfo, Master.currMovie.Filename, False))
            '    Me.SetAVImages(APIXML.GetAVImages(Master.currMovie.Movie.FileInfo, Master.currMovie.Filename, False, Master.currMovie.FileSource))
            '    Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
            '    Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
            'Else
            '    Me.pnlInfoIcons.Width = pbStudio.Width + 1
            '    Me.pbStudio.Left = 0
            'End If

            'Me.lblDirector.Text = Master.currMovie.Movie.Director

            'Me.txtIMDBID.Text = Master.currMovie.Movie.IMDBID

            'Me.txtFilePath.Text = Master.currMovie.Filename

            'Me.lblReleaseDate.Text = Master.currMovie.Movie.ReleaseDate
            'Me.txtCerts.Text = Master.currMovie.Movie.Certification

            'Me.txtMetaData.Text = NFO.FIToString(Master.currMovie.Movie.FileInfo, False)

            If Not IsNothing(Me.MainPoster.Image) Then
                Me.pbPosterCache.Image = Me.MainPoster.Image
                ImageUtils.ResizePB(Me.pbPoster, Me.pbPosterCache, Me.PosterMaxHeight, Me.PosterMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbPoster)
                Me.pnlPoster.Size = New Size(Me.pbPoster.Width + 10, Me.pbPoster.Height + 10)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbPoster.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainPoster.Image.Width, Me.MainPoster.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2 - 15), Me.pbPoster.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2), Me.pbPoster.Height - 20)
                End If

                Me.pbPoster.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbPoster.Image) Then
                    Me.pbPoster.Image.Dispose()
                    Me.pbPoster.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanartSmall.Image) Then
                Me.pbFanartSmallCache.Image = Me.MainFanartSmall.Image
                ImageUtils.ResizePB(Me.pbFanartSmall, Me.pbFanartSmallCache, Me.FanartSmallMaxHeight, Me.FanartSmallMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbFanartSmall)
                Me.pnlFanartSmall.Size = New Size(Me.pbFanartSmall.Width + 10, Me.pbFanartSmall.Height + 10)
                Me.pnlFanartSmall.Location = New Point(Me.pnlPoster.Location.X + Me.pnlPoster.Width + 10, Me.pnlPoster.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanartSmall.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanartSmall.Image.Width, Me.MainFanartSmall.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2 - 15), Me.pbFanartSmall.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2), Me.pbFanartSmall.Height - 20)
                End If

                Me.pbFanartSmall.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbFanartSmall.Image) Then
                    Me.pbFanartSmall.Image.Dispose()
                    Me.pbFanartSmall.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainLandscape.Image) Then
                Me.pbLandscapeCache.Image = Me.MainLandscape.Image
                ImageUtils.ResizePB(Me.pbLandscape, Me.pbLandscapeCache, Me.LandscapeMaxHeight, Me.LandscapeMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbLandscape)
                Me.pnlLandscape.Size = New Size(Me.pbLandscape.Width + 10, Me.pbLandscape.Height + 10)
                Me.pnlLandscape.Location = New Point(Me.pnlFanartSmall.Location.X + Me.pnlFanartSmall.Width + 10, Me.pnlFanartSmall.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbLandscape.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainLandscape.Image.Width, Me.MainLandscape.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2 - 15), Me.pbLandscape.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2), Me.pbLandscape.Height - 20)
                End If

                Me.pbLandscape.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbLandscape.Image) Then
                    Me.pbLandscape.Image.Dispose()
                    Me.pbLandscape.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainClearArt.Image) Then
                Me.pbClearArtCache.Image = Me.MainClearArt.Image
                ImageUtils.ResizePB(Me.pbClearArt, Me.pbClearArtCache, Me.ClearArtMaxHeight, Me.ClearArtMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbClearArt)
                Me.pnlClearArt.Size = New Size(Me.pbClearArt.Width + 10, Me.pbClearArt.Height + 10)
                Me.pnlClearArt.Location = New Point(Me.pnlLandscape.Location.X + Me.pnlLandscape.Width + 10, Me.pnlLandscape.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbClearArt.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainClearArt.Image.Width, Me.MainClearArt.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2 - 15), Me.pbClearArt.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2), Me.pbClearArt.Height - 20)
                End If

                Me.pbClearArt.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbClearArt.Image) Then
                    Me.pbClearArt.Image.Dispose()
                    Me.pbClearArt.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanart.Image) Then
                Me.pbFanartCache.Image = Me.MainFanart.Image

                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)

                If Not IsNothing(pbFanart.Image) AndAlso Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanart.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanart.Image.Width, Me.MainFanart.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2 - 15), Me.pbFanart.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((Me.pbFanart.Image.Width - lenSize) / 2), Me.pbFanart.Height - 20)
                End If
            Else
                If Not IsNothing(Me.pbFanartCache.Image) Then
                    Me.pbFanartCache.Image.Dispose()
                    Me.pbFanartCache.Image = Nothing
                End If
                If Not IsNothing(Me.pbFanart.Image) Then
                    Me.pbFanart.Image.Dispose()
                    Me.pbFanart.Image = Nothing
                End If
            End If

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwRefreshMovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not Me.bwNonScrape.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.EnableFilters(True)
            Else
                Me.dgvMovieSets.Enabled = True
            End If
            If bDoingSearch Then
                Me.txtSearch.Focus()
                bDoingSearch = False
            Else
                Me.dgvMovieSets.Focus()
            End If


            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Not IsNothing(Me.pbClearArt.Image) Then Me.pnlClearArt.Visible = True
            If Not IsNothing(Me.pbPoster.Image) Then Me.pnlPoster.Visible = True
            If Not IsNothing(Me.pbFanartSmall.Image) Then Me.pnlFanartSmall.Visible = True
            If Not IsNothing(Me.pbLandscape.Image) Then Me.pnlLandscape.Visible = True
            If Not IsNothing(Me.pbMPAA.Image) Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To UBound(Me.pnlGenre)
                Me.pnlGenre(i).Visible = True
            Next
            'Me.SetStatus(Master.currMovie.Filename)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub fillScreenInfoWithSeason()
        Dim g As Graphics
        Dim strSize As String
        Dim lenSize As Integer
        Dim rect As Rectangle

        Try
            Me.SuspendLayout()
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Title) Then
                Me.lblTitle.Text = Master.currShow.TVShow.Title
            End If

            Me.txtPlot.Text = Master.currShow.TVShow.Plot
            Me.lblRuntime.Text = String.Format(Master.eLang.GetString(645, "Premiered: {0}"), If(String.IsNullOrEmpty(Master.currShow.TVShow.Premiered), "?", Master.currShow.TVShow.Premiered))

            Me.alActors = New List(Of String)

            If Master.currShow.TVShow.Actors.Count > 0 Then
                Me.pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In Master.currShow.TVShow.Actors
                    If Not String.IsNullOrEmpty(imdbAct.Thumb) Then
                        If Not imdbAct.Thumb.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.Thumb.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.Thumb)
                        Else
                            Me.alActors.Add("none")
                        End If
                    Else
                        Me.alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        Me.lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                Me.lstActors.SelectedIndex = 0
            End If

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.MPAA) Then
                Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(Master.currShow.TVShow.MPAA)
                If Not IsNothing(tmpRatingImg) Then
                    Me.pbMPAA.Image = tmpRatingImg
                    Me.MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(Master.currShow.TVShow.Rating)
            If tmpRating > 0 Then
                Me.BuildStars(tmpRating)
            End If

            If Master.currShow.TVShow.Genres.Count > 0 Then
                Me.createGenreThumbs(Master.currShow.TVShow.Genres)
            End If

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Studio) Then
                Me.pbStudio.Image = APIXML.GetStudioImage(Master.currShow.TVShow.Studio.ToLower) 'ByDef all images file a lower case
                Me.pbStudio.Tag = Master.currShow.TVShow.Studio
            Else
                Me.pbStudio.Image = APIXML.GetStudioImage("####")
                Me.pbStudio.Tag = String.Empty
            End If

            Me.pnlInfoIcons.Width = pbStudio.Width + 1
            Me.pbStudio.Left = 0

            If Not IsNothing(Me.MainPoster.Image) Then
                Me.pbPosterCache.Image = Me.MainPoster.Image
                ImageUtils.ResizePB(Me.pbPoster, Me.pbPosterCache, Me.PosterMaxHeight, Me.PosterMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbPoster)
                Me.pnlPoster.Size = New Size(Me.pbPoster.Width + 10, Me.pbPoster.Height + 10)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbPoster.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainPoster.Image.Width, Me.MainPoster.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2 - 15), Me.pbPoster.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2), Me.pbPoster.Height - 20)
                End If

                Me.pbPoster.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbPoster.Image) Then
                    Me.pbPoster.Image.Dispose()
                    Me.pbPoster.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanartSmall.Image) Then
                Me.pbFanartSmallCache.Image = Me.MainFanartSmall.Image
                ImageUtils.ResizePB(Me.pbFanartSmall, Me.pbFanartSmallCache, Me.FanartSmallMaxHeight, Me.FanartSmallMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbFanartSmall)
                Me.pnlFanartSmall.Size = New Size(Me.pbFanartSmall.Width + 10, Me.pbFanartSmall.Height + 10)
                Me.pnlFanartSmall.Location = New Point(Me.pnlPoster.Location.X + Me.pnlPoster.Width + 10, Me.pnlPoster.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanartSmall.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanartSmall.Image.Width, Me.MainFanartSmall.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2 - 15), Me.pbFanartSmall.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2), Me.pbFanartSmall.Height - 20)
                End If

                Me.pbFanartSmall.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbFanartSmall.Image) Then
                    Me.pbFanartSmall.Image.Dispose()
                    Me.pbFanartSmall.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainLandscape.Image) Then
                Me.pbLandscapeCache.Image = Me.MainLandscape.Image
                ImageUtils.ResizePB(Me.pbLandscape, Me.pbLandscapeCache, Me.LandscapeMaxHeight, Me.LandscapeMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbLandscape)
                Me.pnlLandscape.Size = New Size(Me.pbLandscape.Width + 10, Me.pbLandscape.Height + 10)
                Me.pnlLandscape.Location = New Point(Me.pnlFanartSmall.Location.X + Me.pnlFanartSmall.Width + 10, Me.pnlFanartSmall.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbLandscape.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainLandscape.Image.Width, Me.MainLandscape.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2 - 15), Me.pbLandscape.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2), Me.pbLandscape.Height - 20)
                End If

                Me.pbLandscape.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbLandscape.Image) Then
                    Me.pbLandscape.Image.Dispose()
                    Me.pbLandscape.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanart.Image) Then
                Me.pbFanartCache.Image = Me.MainFanart.Image

                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)

                If Not IsNothing(pbFanart.Image) AndAlso Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanart.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanart.Image.Width, Me.MainFanart.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2 - 15), Me.pbFanart.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((Me.pbFanart.Image.Width - lenSize) / 2), Me.pbFanart.Height - 20)
                End If
            Else
                If Not IsNothing(Me.pbFanartCache.Image) Then
                    Me.pbFanartCache.Image.Dispose()
                    Me.pbFanartCache.Image = Nothing
                End If
                If Not IsNothing(Me.pbFanart.Image) Then
                    Me.pbFanart.Image.Dispose()
                    Me.pbFanart.Image = Nothing
                End If
            End If

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwRefreshMovies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.dgvTVSeasons.Focus()
            Else
                Me.dgvTVEpisodes.Enabled = True
                Me.dgvTVSeasons.Enabled = True
                Me.dgvTVShows.Enabled = True
                Me.dgvTVSeasons.Focus()
            End If

            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Not IsNothing(Me.pbPoster.Image) Then Me.pnlPoster.Visible = True
            If Not IsNothing(Me.pbAllSeason.Image) Then Me.pnlAllSeason.Visible = True
            If Not IsNothing(Me.pbFanartSmall.Image) Then Me.pnlFanartSmall.Visible = True
            If Not IsNothing(Me.pbLandscape.Image) Then Me.pnlLandscape.Visible = True
            If Not IsNothing(Me.pbMPAA.Image) Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To UBound(Me.pnlGenre)
                Me.pnlGenre(i).Visible = True
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub fillScreenInfoWithShow()
        Dim g As Graphics
        Dim strSize As String
        Dim lenSize As Integer
        Dim rect As Rectangle

        Try
            Me.SuspendLayout()
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Title) Then
                Me.lblTitle.Text = Master.currShow.TVShow.Title
            End If

            Me.lblOriginalTitle.Text = String.Empty
            Me.txtPlot.Text = Master.currShow.TVShow.Plot
            Me.lblRuntime.Text = String.Format(Master.eLang.GetString(645, "Premiered: {0}"), If(String.IsNullOrEmpty(Master.currShow.TVShow.Premiered), "?", Master.currShow.TVShow.Premiered))

            Me.alActors = New List(Of String)

            If Master.currShow.TVShow.Actors.Count > 0 Then
                Me.pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In Master.currShow.TVShow.Actors
                    If Not String.IsNullOrEmpty(imdbAct.Thumb) Then
                        If Not imdbAct.Thumb.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.Thumb.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.Thumb)
                        Else
                            Me.alActors.Add("none")
                        End If
                    Else
                        Me.alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        Me.lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                Me.lstActors.SelectedIndex = 0
            End If

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.MPAA) Then
                Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(Master.currShow.TVShow.MPAA)
                If Not IsNothing(tmpRatingImg) Then
                    Me.pbMPAA.Image = tmpRatingImg
                    Me.MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(Master.currShow.TVShow.Rating)
            If tmpRating > 0 Then
                Me.BuildStars(tmpRating)
            End If

            If Master.currShow.TVShow.Genres.Count > 0 Then
                Me.createGenreThumbs(Master.currShow.TVShow.Genres)
            End If

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Studio) Then
                Me.pbStudio.Image = APIXML.GetStudioImage(Master.currShow.TVShow.Studio.ToLower) 'ByDef all images file a lower case
                Me.pbStudio.Tag = Master.currShow.TVShow.Studio
            Else
                Me.pbStudio.Image = APIXML.GetStudioImage("####")
                Me.pbStudio.Tag = String.Empty
            End If

            Me.pnlInfoIcons.Width = pbStudio.Width + 1
            Me.pbStudio.Left = 0

            If Not IsNothing(Me.MainPoster.Image) Then
                Me.pbPosterCache.Image = Me.MainPoster.Image
                ImageUtils.ResizePB(Me.pbPoster, Me.pbPosterCache, Me.PosterMaxHeight, Me.PosterMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbPoster)
                Me.pnlPoster.Size = New Size(Me.pbPoster.Width + 10, Me.pbPoster.Height + 10)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbPoster.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainPoster.Image.Width, Me.MainPoster.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2 - 15), Me.pbPoster.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbPoster.Image.Width - lenSize) / 2), Me.pbPoster.Height - 20)
                End If

                Me.pbPoster.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbPoster.Image) Then
                    Me.pbPoster.Image.Dispose()
                    Me.pbPoster.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanartSmall.Image) Then
                Me.pbFanartSmallCache.Image = Me.MainFanartSmall.Image
                ImageUtils.ResizePB(Me.pbFanartSmall, Me.pbFanartSmallCache, Me.FanartSmallMaxHeight, Me.FanartSmallMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbFanartSmall)
                Me.pnlFanartSmall.Size = New Size(Me.pbFanartSmall.Width + 10, Me.pbFanartSmall.Height + 10)
                Me.pnlFanartSmall.Location = New Point(Me.pnlPoster.Location.X + Me.pnlPoster.Width + 10, Me.pnlPoster.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanartSmall.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanartSmall.Image.Width, Me.MainFanartSmall.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2 - 15), Me.pbFanartSmall.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbFanartSmall.Image.Width - lenSize) / 2), Me.pbFanartSmall.Height - 20)
                End If

                Me.pbFanartSmall.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbFanartSmall.Image) Then
                    Me.pbFanartSmall.Image.Dispose()
                    Me.pbFanartSmall.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainLandscape.Image) Then
                Me.pbLandscapeCache.Image = Me.MainLandscape.Image
                ImageUtils.ResizePB(Me.pbLandscape, Me.pbLandscapeCache, Me.LandscapeMaxHeight, Me.LandscapeMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbLandscape)
                Me.pnlLandscape.Size = New Size(Me.pbLandscape.Width + 10, Me.pbLandscape.Height + 10)
                Me.pnlLandscape.Location = New Point(Me.pnlFanartSmall.Location.X + Me.pnlFanartSmall.Width + 10, Me.pnlFanartSmall.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbLandscape.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainLandscape.Image.Width, Me.MainLandscape.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2 - 15), Me.pbLandscape.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbLandscape.Image.Width - lenSize) / 2), Me.pbLandscape.Height - 20)
                End If

                Me.pbLandscape.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbLandscape.Image) Then
                    Me.pbLandscape.Image.Dispose()
                    Me.pbLandscape.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainClearArt.Image) Then
                Me.pbClearArtCache.Image = Me.MainClearArt.Image
                ImageUtils.ResizePB(Me.pbClearArt, Me.pbClearArtCache, Me.ClearArtMaxHeight, Me.ClearArtMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbClearArt)
                Me.pnlClearArt.Size = New Size(Me.pbClearArt.Width + 10, Me.pbClearArt.Height + 10)
                Me.pnlClearArt.Location = New Point(Me.pnlLandscape.Location.X + Me.pnlLandscape.Width + 10, Me.pnlLandscape.Location.Y)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbClearArt.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainClearArt.Image.Width, Me.MainClearArt.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2 - 15), Me.pbClearArt.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((pbClearArt.Image.Width - lenSize) / 2), Me.pbClearArt.Height - 20)
                End If

                Me.pbClearArt.Location = New Point(4, 4)
            Else
                If Not IsNothing(Me.pbClearArt.Image) Then
                    Me.pbClearArt.Image.Dispose()
                    Me.pbClearArt.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainFanart.Image) Then
                Me.pbFanartCache.Image = Me.MainFanart.Image

                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)

                If Not IsNothing(pbFanart.Image) AndAlso Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(pbFanart.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainFanart.Image.Width, Me.MainFanart.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2 - 15), Me.pbFanart.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((Me.pbFanart.Image.Width - lenSize) / 2), Me.pbFanart.Height - 20)
                End If
            Else
                If Not IsNothing(Me.pbFanartCache.Image) Then
                    Me.pbFanartCache.Image.Dispose()
                    Me.pbFanartCache.Image = Nothing
                End If
                If Not IsNothing(Me.pbFanart.Image) Then
                    Me.pbFanart.Image.Dispose()
                    Me.pbFanart.Image = Nothing
                End If
            End If

            If Not IsNothing(Me.MainAllSeason.Image) Then
                Me.pbAllSeasonCache.Image = Me.MainAllSeason.Image
                ImageUtils.ResizePB(Me.pbAllSeason, Me.pbAllSeasonCache, Me.PosterMaxHeight, Me.PosterMaxWidth)
                If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbAllSeason)
                Me.pnlAllSeason.Size = New Size(Me.pbAllSeason.Width + 10, Me.pbAllSeason.Height + 10)

                If Master.eSettings.GeneralShowImgDims Then
                    g = Graphics.FromImage(Me.pbAllSeason.Image)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    strSize = String.Format("{0} x {1}", Me.MainAllSeason.Image.Width, Me.MainAllSeason.Image.Height)
                    lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                    rect = New Rectangle(Convert.ToInt32((Me.pbAllSeason.Image.Width - lenSize) / 2 - 15), Me.pbAllSeason.Height - 25, lenSize + 30, 25)
                    ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                    g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((Me.pbAllSeason.Image.Width - lenSize) / 2), Me.pbAllSeason.Height - 20)
                End If

                Me.pbAllSeason.Location = New Point(4, 4)
                Me.pnlAllSeason.Location = New Point(Me.pbFanart.Width - Me.pnlAllSeason.Width - 9, 112)
            Else
                If Not IsNothing(Me.pbAllSeason.Image) Then
                    Me.pbAllSeason.Image.Dispose()
                    Me.pbAllSeason.Image = Nothing
                End If
            End If

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwRefreshMovies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.dgvTVShows.Focus()
            Else
                Me.dgvTVEpisodes.Enabled = True
                Me.dgvTVSeasons.Enabled = True
                Me.dgvTVShows.Enabled = True
                Me.dgvTVShows.Focus()
            End If

            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Not IsNothing(Me.pbAllSeason.Image) Then Me.pnlAllSeason.Visible = True
            If Not IsNothing(Me.pbClearArt.Image) Then Me.pnlClearArt.Visible = True
            If Not IsNothing(Me.pbPoster.Image) Then Me.pnlPoster.Visible = True
            If Not IsNothing(Me.pbFanartSmall.Image) Then Me.pnlFanartSmall.Visible = True
            If Not IsNothing(Me.pbLandscape.Image) Then Me.pnlLandscape.Visible = True
            If Not IsNothing(Me.pbMPAA.Image) Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To UBound(Me.pnlGenre)
                Me.pnlGenre(i).Visible = True
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub FillSeasons(ByVal ShowID As Integer)
        Me.bsSeasons.DataSource = Nothing
        Me.dgvTVSeasons.DataSource = Nothing
        Me.bsEpisodes.DataSource = Nothing
        Me.dgvTVEpisodes.DataSource = Nothing

        Application.DoEvents()

        Me.dgvTVSeasons.Enabled = False

        Master.DB.FillDataTable(Me.dtSeasons, String.Concat("SELECT TVShowID, SeasonText, Season, HasPoster, HasFanart, PosterPath, FanartPath, Lock, Mark, New, HasBanner, BannerPath, HasLandscape, LandscapePath FROM TVSeason WHERE TVShowID = ", ShowID, " AND Season <> 999 ORDER BY Season;"))

        If Me.dtSeasons.Rows.Count > 0 Then

            With Me
                .bsSeasons.DataSource = .dtSeasons
                .dgvTVSeasons.DataSource = .bsSeasons

                .dgvTVSeasons.Columns(0).Visible = False
                .dgvTVSeasons.Columns(1).Resizable = DataGridViewTriState.True
                .dgvTVSeasons.Columns(1).ReadOnly = True
                .dgvTVSeasons.Columns(1).MinimumWidth = 83
                .dgvTVSeasons.Columns(1).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns(1).ToolTipText = Master.eLang.GetString(650, "Season")
                .dgvTVSeasons.Columns(1).HeaderText = Master.eLang.GetString(650, "Season")
                .dgvTVSeasons.Columns(2).Visible = False
                .dgvTVSeasons.Columns(3).Width = 20
                .dgvTVSeasons.Columns(3).Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns(3).ReadOnly = True
                .dgvTVSeasons.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns(3).Visible = Not Master.eSettings.TVSeasonPosterCol
                .dgvTVSeasons.Columns(3).ToolTipText = Master.eLang.GetString(148, "Poster")
                .dgvTVSeasons.Columns(4).Width = 20
                .dgvTVSeasons.Columns(4).Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns(4).ReadOnly = True
                .dgvTVSeasons.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns(4).Visible = Not Master.eSettings.TVSeasonFanartCol
                .dgvTVSeasons.Columns(4).ToolTipText = Master.eLang.GetString(149, "Fanart")
                .dgvTVSeasons.Columns(5).Visible = False
                .dgvTVSeasons.Columns(6).Visible = False
                .dgvTVSeasons.Columns(7).Visible = False
                .dgvTVSeasons.Columns(8).Visible = False
                .dgvTVSeasons.Columns(9).Visible = False
                .dgvTVSeasons.Columns(10).Width = 20
                .dgvTVSeasons.Columns(10).Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns(10).ReadOnly = True
                .dgvTVSeasons.Columns(10).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns(10).Visible = Not Master.eSettings.TVSeasonBannerCol
                .dgvTVSeasons.Columns(10).ToolTipText = Master.eLang.GetString(838, "Banner")
                .dgvTVSeasons.Columns(11).Visible = False
                .dgvTVSeasons.Columns(12).Width = 20
                .dgvTVSeasons.Columns(12).Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns(12).ReadOnly = True
                .dgvTVSeasons.Columns(12).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns(12).Visible = Not Master.eSettings.TVSeasonLandscapeCol
                .dgvTVSeasons.Columns(12).ToolTipText = Master.eLang.GetString(1035, "Landscape")
                .dgvTVSeasons.Columns(13).Visible = False
                For i As Integer = 14 To .dgvTVSeasons.Columns.Count - 1
                    .dgvTVSeasons.Columns(i).Visible = False
                Next

                .dgvTVSeasons.Columns(0).ValueType = GetType(Int32)

                If Master.isWindows Then .dgvTVSeasons.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                ResizeTVLists(2)

                .dgvTVSeasons.Sort(.dgvTVSeasons.Columns(1), ComponentModel.ListSortDirection.Ascending)

                Me.FillEpisodes(ShowID, Convert.ToInt32(.dgvTVSeasons.Item(2, 0).Value))

            End With
        End If

        Me.dgvTVSeasons.Enabled = True
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

            Me.SetControlsEnabled(False, True)
            Me.EnableFilters(False)

            Master.eSettings.Version = String.Format("r{0}", My.Application.Info.Version.Revision)

            If Me.fScanner.IsBusy OrElse Master.isCL Then
                doSave = False
            End If

            If Me.fScanner.IsBusy Then Me.fScanner.Cancel()
            If Me.bwMetaInfo.IsBusy Then Me.bwMetaInfo.CancelAsync()
            If Me.bwLoadMovieInfo.IsBusy Then Me.bwLoadMovieInfo.CancelAsync()
            If Me.bwLoadMovieSetInfo.IsBusy Then Me.bwLoadMovieSetInfo.CancelAsync()
            If Me.bwLoadShowInfo.IsBusy Then Me.bwLoadShowInfo.CancelAsync()
            If Me.bwLoadSeasonInfo.IsBusy Then Me.bwLoadSeasonInfo.CancelAsync()
            If Me.bwLoadEpInfo.IsBusy Then Me.bwLoadEpInfo.CancelAsync()
            If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
            If Me.bwRefreshMovies.IsBusy Then Me.bwRefreshMovies.CancelAsync()
            If Me.bwCleanDB.IsBusy Then Me.bwCleanDB.CancelAsync()
            If Me.bwMovieScraper.IsBusy Then Me.bwMovieScraper.CancelAsync()
            If ModulesManager.Instance.TVIsBusy Then ModulesManager.Instance.TVCancelAsync()

            lblCanceling.Text = Master.eLang.GetString(99, "Canceling All Processes...")
            btnCancel.Visible = False
            lblCanceling.Visible = True
            prbCanceling.Visible = True
            pnlCancel.Visible = True
            Me.Refresh()

            While Me.fScanner.IsBusy OrElse Me.bwMetaInfo.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy _
            OrElse Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy _
            OrElse Me.bwRefreshMovies.IsBusy OrElse Me.bwRefreshMovieSets.IsBusy OrElse Me.bwCleanDB.IsBusy _
            OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy _
            OrElse ModulesManager.Instance.TVIsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If doSave Then Master.DB.ClearNew()

            If Not Master.isCL Then
                Master.DB.Close()
            End If

            If Not Master.isCL Then
                Master.eSettings.GeneralWindowLoc = Me.Location
                Master.eSettings.GeneralWindowSize = Me.Size
                Master.eSettings.GeneralWindowState = Me.WindowState
                Master.eSettings.GeneralMovieInfoPanelState = Me.aniMovieType
                Master.eSettings.GeneralMovieSetInfoPanelState = Me.aniMovieSetType
                Master.eSettings.GeneralTVShowInfoPanelState = Me.aniShowType
                Master.eSettings.GeneralFilterPanelState = Me.aniFilterRaise
                Master.eSettings.GeneralMainSplitterPanelState = Me.scMain.SplitterDistance
                Me.pnlFilter.Visible = False
                Master.eSettings.GeneralSeasonSplitterPanelState = Me.scTVSeasonsEpisodes.SplitterDistance
                Master.eSettings.GeneralShowSplitterPanelState = Me.scTV.SplitterDistance
            End If
            If Not Me.WindowState = FormWindowState.Minimized Then Master.eSettings.Save()

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
        Try
            Me.Visible = False
            logger.Info(New StackFrame().GetMethod().Name, "Embert startup")

            If Master.isWindows Then 'Dam mono on MacOSX don't have trayicon implemented yet
                Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
                Me.TrayIcon.Icon = Me.Icon
                Me.TrayIcon.ContextMenuStrip = Me.cmnuTray
                Me.TrayIcon.Text = "Ember Media Manager"
                Me.TrayIcon.Visible = True
            End If

            Me.bwCheckVersion.RunWorkerAsync()

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(854, "Basic setup"))

            Dim currentDomain As AppDomain = AppDomain.CurrentDomain
            ModulesManager.AssemblyList.Add(New ModulesManager.AssemblyListItem With {.AssemblyName = "EmberAPI", _
              .Assembly = Assembly.LoadFile(Path.Combine(Functions.AppPath, "EmberAPI.dll"))})
            'Cocotus 2014/01/25 By switching Ember to NET4.5 dependency , the LoadFile method is not supported anymore
            '  http://msdn.microsoft.com/de-de/library/system.reflection.assembly.loadfile(v=vs.110).aspx
            '.Assembly = Assembly.LoadFile(Path.Combine(Functions.AppPath, "EmberAPI.dll"), Assembly.GetExecutingAssembly().Evidence)})    
            AddHandler currentDomain.AssemblyResolve, AddressOf MyResolveEventHandler

            clsAdvancedSettings.Start()

            'Create Modules Folders
            Dim sPath = String.Concat(Functions.AppPath, "Modules")
            If Not Directory.Exists(sPath) Then
                Directory.CreateDirectory(sPath)
            End If

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(855, "Creating default options..."))
            Functions.CreateDefaultOptions()
            '//
            ' Add our handlers, load settings, set form colors, and try to load movies at startup
            '\\
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(856, "Loading modules..."))
            'Setup/Load Modules Manager and set runtime objects (ember application) so they can be exposed to modules
            'ExternalModulesManager = New ModulesManager
            ModulesManager.Instance.RuntimeObjects.MenuMediaList = Me.cmnuMovie
            ModulesManager.Instance.RuntimeObjects.MenuTVShowList = Me.cmnuShow
            ModulesManager.Instance.RuntimeObjects.MediaList = Me.dgvMovies
            ModulesManager.Instance.RuntimeObjects.TopMenu = Me.mnuMain
            ModulesManager.Instance.RuntimeObjects.MainTool = Me.tsMain
            ModulesManager.Instance.RuntimeObjects.TrayMenu = Me.cmnuTray
            ModulesManager.Instance.RuntimeObjects.DelegateLoadMedia(AddressOf LoadMedia)
            ModulesManager.Instance.RuntimeObjects.DelegateOpenImageViewer(AddressOf OpenImageViewer)
            ModulesManager.Instance.LoadAllModules()

            If Not Master.isCL Then
                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(857, "Creating GUI..."))
            End If
            'setup some dummies so we don't get exceptions when resizing form/info panel
            ReDim Preserve Me.pnlGenre(0)
            ReDim Preserve Me.pbGenre(0)
            Me.pnlGenre(0) = New Panel()
            Me.pbGenre(0) = New PictureBox()

            AddHandler fScanner.ScannerUpdated, AddressOf ScannerUpdated
            AddHandler fScanner.ScanningCompleted, AddressOf ScanningCompleted
            AddHandler ModulesManager.Instance.ScraperEvent_TV, AddressOf TVScraperEvent
            AddHandler ModulesManager.Instance.GenericEvent, AddressOf Me.GenericRunCallBack

            Functions.DGVDoubleBuffer(Me.dgvMovies)
            Functions.DGVDoubleBuffer(Me.dgvTVShows)
            Functions.DGVDoubleBuffer(Me.dgvTVSeasons)
            Functions.DGVDoubleBuffer(Me.dgvTVEpisodes)
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.UserPaint, True)

            If TypeOf tsMain.Renderer Is ToolStripProfessionalRenderer Then
                CType(tsMain.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
            End If

            If Not Directory.Exists(Master.TempPath) Then Directory.CreateDirectory(Master.TempPath)

            If Master.isCL Then ' Command Line
                LoadWithCommandLine(Master.appArgs)
            Else 'Regular Run (GUI)
                LoadWithGUI()
            End If
            While Not ModulesManager.Instance.ModulesLoaded()
                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(856, "Loading modules..."))
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
            Master.fLoading.Close()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.Close()
        End Try
    End Sub
    ''' <summary>
    ''' Performs startup routines specific to being initiated by the command line
    ''' </summary>
    ''' <param name="appArgs">Command line arguments. Must NOT be empty!</param>
    ''' <remarks></remarks>
    Private Sub LoadWithCommandLine(ByVal appArgs As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs)
        Dim Args() As String = appArgs.CommandLine.ToArray
        Try
            logger.Trace("LoadWithCommandLine()")

            Dim MoviePath As String = String.Empty
            Dim isSingle As Boolean = False
            Dim hasSpec As Boolean = False
            Dim clScrapeType As Enums.ScrapeType = Nothing
            Dim clExport As Boolean = False
            Dim clExportResizePoster As Integer = 0
            Dim clExportTemplate As String = "template"
            Dim clAsk As Boolean = False
            Dim nowindow As Boolean = False
            Dim RunModule As Boolean = False
            Dim ModuleName As String = String.Empty
            Dim UpdateTVShows As Boolean = False
            For i As Integer = 0 To Args.Count - 1

                Select Case Args(i).ToLower
                    Case "-fullask"
                        clScrapeType = Enums.ScrapeType.FullAsk
                        clAsk = True
                    Case "-fullauto"
                        clScrapeType = Enums.ScrapeType.FullAuto
                        clAsk = False
                    Case "-fullskip"
                        clScrapeType = Enums.ScrapeType.FullSkip
                        clAsk = False
                    Case "-missask"
                        clScrapeType = Enums.ScrapeType.UpdateAsk
                        clAsk = True
                    Case "-missauto"
                        clScrapeType = Enums.ScrapeType.UpdateAuto
                        clAsk = False
                    Case "-missskip"
                        clScrapeType = Enums.ScrapeType.UpdateSkip
                        clAsk = True
                    Case "-newask"
                        clScrapeType = Enums.ScrapeType.NewAsk
                        clAsk = True
                    Case "-newauto"
                        clScrapeType = Enums.ScrapeType.NewAuto
                        clAsk = False
                    Case "-newskip"
                        clScrapeType = Enums.ScrapeType.NewSkip
                        clAsk = False
                    Case "-markask"
                        clScrapeType = Enums.ScrapeType.MarkAsk
                        clAsk = True
                    Case "-markauto"
                        clScrapeType = Enums.ScrapeType.MarkAuto
                        clAsk = False
                    Case "-markskip"
                        clScrapeType = Enums.ScrapeType.MarkSkip
                        clAsk = True
                    Case "-file"
                        If Args.Count - 1 > i Then
                            isSingle = False
                            hasSpec = True
                            clScrapeType = Enums.ScrapeType.SingleScrape
                            If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                                MoviePath = Args(i + 1).Replace("""", String.Empty)
                                i += 1
                            End If
                        Else
                            Exit For
                        End If
                    Case "-folder"
                        If Args.Count - 1 > i Then
                            isSingle = True
                            hasSpec = True
                            clScrapeType = Enums.ScrapeType.SingleScrape
                            If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                                MoviePath = Args(i + 1).Replace("""", String.Empty)
                                i += 1
                            End If
                        Else
                            Exit For
                        End If
                    Case "-export"
                        If Args.Count - 1 > i Then
                            MoviePath = Args(i + 1).Replace("""", String.Empty)
                            clExport = True
                        Else
                            Exit For
                        End If
                    Case "-template"
                        If Args.Count - 1 > i Then
                            clExportTemplate = Args(i + 1).Replace("""", String.Empty)
                        Else
                            Exit For
                        End If
                    Case "-resize"
                        If Args.Count - 1 > i Then
                            clExportResizePoster = Convert.ToUInt16(Args(i + 1).Replace("""", String.Empty))
                        Else
                            Exit For
                        End If
                    Case "-all"
                        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
                    Case "-banner"
                        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
                    Case "-clearart"
                        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
                    Case "-clearlogo"
                        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
                    Case "-discart"
                        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
                    Case "-efanarts"
                        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
                    Case "-ethumbs"
                        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
                    Case "-fanart"
                        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
                    Case "-landscape"
                        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
                    Case "-nfo"
                        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
                    Case "-poster"
                        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
                    Case "-theme"
                        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
                    Case "-trailer"
                        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
                    Case "--verbose"
                        clAsk = True
                    Case "-nowindow"
                        nowindow = True
                    Case "-run"
                        If Args.Count - 1 > i Then
                            ModuleName = Args(i + 1).Replace("""", String.Empty)
                            RunModule = True
                        Else
                            Exit For
                        End If
                    Case "-tvupdate"
                        UpdateTVShows = True
                    Case Else
                        'If File.Exists(Args(2).Replace("""", String.Empty)) Then
                        'MoviePath = Args(2).Replace("""", String.Empty)
                        'End If
                End Select
            Next
            If nowindow Then Master.fLoading.Hide()
            APIXML.CacheXMLs()
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading database..."))
            If Master.DB.ConnectMyVideosDB() Then
                Me.LoadMedia(New Structures.Scans With {.Movies = True, .TV = True})
            End If
            Master.DB.LoadMovieSourcesFromDB()
            Master.DB.LoadTVSourcesFromDB()
            If RunModule Then
                Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(859, "Running Module..."))
                Dim gModule As ModulesManager._externalGenericModuleClass = ModulesManager.Instance.externalProcessorModules.FirstOrDefault(Function(y) y.ProcessorModule.ModuleName = ModuleName)
                If Not IsNothing(gModule) Then
                    gModule.ProcessorModule.RunGeneric(Enums.ModuleEventType.CommandLine, Nothing, Nothing, Nothing)
                End If
            End If
            If clExport = True Then
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {MoviePath, clExportTemplate, clExportResizePoster}))
                'dlgExportMovies.CLExport(MoviePath, clExportTemplate, clExportResizePoster)
            End If

            If Not IsNothing(clScrapeType) Then
                Me.cmnuTrayExit.Enabled = True
                Me.cmnuTray.Enabled = True
                If Functions.HasModifier AndAlso Not clScrapeType = Enums.ScrapeType.SingleScrape Then
                    Try
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(860, "Loading Media..."))
                        LoadMedia(New Structures.Scans With {.Movies = True})
                        While Not Me.LoadingDone
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        MovieScrapeData(False, clScrapeType, Master.DefaultMovieOptions)
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name, ex)
                    End Try
                Else
                    Try
                        If Not String.IsNullOrEmpty(MoviePath) AndAlso hasSpec Then
                            Master.currMovie = Master.DB.LoadMovieFromDB(MoviePath)
                            Dim tmpTitle As String = String.Empty
                            If FileUtils.Common.isVideoTS(MoviePath) Then
                                tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(MoviePath).FullName).Name, False)
                            ElseIf FileUtils.Common.isBDRip(MoviePath) Then
                                tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(MoviePath).FullName).FullName).Name, False)
                            Else
                                tmpTitle = StringUtils.FilterName(If(isSingle, Directory.GetParent(MoviePath).Name, Path.GetFileNameWithoutExtension(MoviePath)))
                            End If
                            If IsNothing(Master.currMovie.Movie) Then
                                Master.currMovie.Movie = New MediaContainers.Movie
                                Master.currMovie.Movie.Title = tmpTitle
                                Dim sFile As New Scanner.MovieContainer
                                sFile.Filename = MoviePath
                                sFile.isSingle = isSingle
                                sFile.UseFolder = If(isSingle, True, False)
                                fScanner.GetMovieFolderContents(sFile)
                                If Not String.IsNullOrEmpty(sFile.Nfo) Then
                                    Master.currMovie.Movie = NFO.LoadMovieFromNFO(sFile.Nfo, sFile.isSingle)
                                Else
                                    Master.currMovie.Movie = NFO.LoadMovieFromNFO(sFile.Filename, sFile.isSingle)
                                End If
                                If String.IsNullOrEmpty(Master.currMovie.Movie.Title) Then
                                    'no title so assume it's an invalid nfo, clear nfo path if exists
                                    sFile.Nfo = String.Empty
                                    If FileUtils.Common.isVideoTS(sFile.Filename) Then
                                        Master.currMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(sFile.Filename).FullName).Name)
                                    ElseIf FileUtils.Common.isBDRip(sFile.Filename) Then
                                        Master.currMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(sFile.Filename).FullName).FullName).Name)
                                    Else
                                        If sFile.UseFolder AndAlso sFile.isSingle Then
                                            Master.currMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(sFile.Filename).Name)
                                        Else
                                            Master.currMovie.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(sFile.Filename))
                                        End If
                                    End If
                                    If String.IsNullOrEmpty(Master.currMovie.Movie.SortTitle) Then Master.currMovie.Movie.SortTitle = Master.currMovie.ListTitle
                                Else
                                    Dim tTitle As String = StringUtils.FilterTokens_Movie(Master.currMovie.Movie.Title)
                                    If String.IsNullOrEmpty(Master.currMovie.Movie.SortTitle) Then Master.currMovie.Movie.SortTitle = tTitle
                                    If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(Master.currMovie.Movie.Year) Then
                                        Master.currMovie.ListTitle = String.Format("{0} ({1})", tTitle, Master.currMovie.Movie.Year)
                                    Else
                                        Master.currMovie.ListTitle = tTitle
                                    End If
                                End If

                                If Not String.IsNullOrEmpty(Master.currMovie.ListTitle) Then
                                    Master.currMovie.BannerPath = sFile.Banner
                                    Master.currMovie.ClearArtPath = sFile.ClearArt
                                    Master.currMovie.ClearLogoPath = sFile.ClearLogo
                                    Master.currMovie.DiscArtPath = sFile.DiscArt
                                    Master.currMovie.EFanartsPath = sFile.EFanarts
                                    Master.currMovie.EThumbsPath = sFile.EThumbs
                                    Master.currMovie.FanartPath = sFile.Fanart
                                    Master.currMovie.Filename = sFile.Filename
                                    Master.currMovie.LandscapePath = sFile.Landscape
                                    Master.currMovie.NfoPath = sFile.Nfo
                                    Master.currMovie.PosterPath = sFile.Poster
                                    Master.currMovie.Source = sFile.Source
                                    Master.currMovie.SubPath = sFile.Subs
                                    Master.currMovie.ThemePath = sFile.Theme
                                    Master.currMovie.TrailerPath = sFile.Trailer
                                    Master.currMovie.UseFolder = sFile.UseFolder
                                    Master.currMovie.IsSingle = sFile.isSingle
                                End If
                                Master.tmpMovie = Master.currMovie.Movie
                            End If
                            Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                            MovieScrapeData(False, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions)
                        Else
                            Me.ScraperDone = True
                        End If
                    Catch ex As Exception
                        Me.ScraperDone = True
                        logger.Error(New StackFrame().GetMethod().Name, ex)
                    End Try
                End If

                While Not Me.ScraperDone
                    Application.DoEvents()
                    Threading.Thread.Sleep(50)
                End While
            End If

            If UpdateTVShows Then
                Try
                    Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                    Master.fLoading.SetLoadingMesg(Master.eLang.GetString(860, "Loading Media..."))
                    LoadMedia(New Structures.Scans With {.TV = True})
                    While Not Me.LoadingDone
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                    Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                    Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                    MovieScrapeData(False, clScrapeType, Master.DefaultMovieOptions)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            End If

            Master.fLoading.Close()
            Me.Close()
        Catch ex As Exception
        End Try

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
                Me.SetUp(True)
                Me.cbSearch.SelectedIndex = 0

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(863, "Positioning controls..."))
                Me.Location = Master.eSettings.GeneralWindowLoc
                Me.Size = Master.eSettings.GeneralWindowSize
                Me.WindowState = Master.eSettings.GeneralWindowState

                Me.aniMovieType = Master.eSettings.GeneralMovieInfoPanelState
                Select Case Me.aniMovieType
                    Case 0
                        Me.pnlInfoPanel.Height = 25
                        Me.btnDown.Enabled = False
                        Me.btnMid.Enabled = True
                        Me.btnUp.Enabled = True
                    Case 1
                        Me.pnlInfoPanel.Height = Me.IPMid
                        Me.btnMid.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnUp.Enabled = True
                    Case 2
                        Me.pnlInfoPanel.Height = Me.IPUp
                        Me.btnUp.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnMid.Enabled = True
                End Select

                Me.aniMovieSetType = Master.eSettings.GeneralMovieSetInfoPanelState

                Me.aniShowType = Master.eSettings.GeneralTVShowInfoPanelState

                Me.aniFilterRaise = Master.eSettings.GeneralFilterPanelState
                If Me.aniFilterRaise Then
                    Me.pnlFilter.Height = Functions.Quantize(Me.gbFilterSpecific.Height + Me.lblFilter.Height + 15, 5)
                    Me.btnFilterDown.Enabled = True
                    Me.btnFilterUp.Enabled = False
                Else
                    Me.pnlFilter.Height = 25
                    Me.btnFilterDown.Enabled = False
                    Me.btnFilterUp.Enabled = True
                End If
                Try ' On error just ignore this a let it use default
                    Me.scMain.SplitterDistance = Master.eSettings.GeneralMainSplitterPanelState
                    Me.scTV.SplitterDistance = Master.eSettings.GeneralShowSplitterPanelState
                    Me.scTVSeasonsEpisodes.SplitterDistance = Master.eSettings.GeneralSeasonSplitterPanelState
                Catch ex As Exception
                End Try
                Me.pnlFilter.Visible = True

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(1165, "Initializing Main Form. Please wait..."))
                Me.ClearInfo()

                Application.DoEvents()
                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading database..."))
                If Master.eSettings.Version = String.Format("r{0}", My.Application.Info.Version.Revision) Then
                    If Master.DB.ConnectMyVideosDB() Then
                        Me.LoadMedia(New Structures.Scans With {.Movies = True, .MovieSets = True, .TV = True})
                    End If
                    Me.FillList(True, True, True)
                    Me.Visible = True
                Else
                    If Master.DB.ConnectMyVideosDB() Then
                        Me.LoadMedia(New Structures.Scans With {.Movies = True, .MovieSets = True, .TV = True})
                    End If
                    If dlgWizard.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Application.DoEvents()
                        Me.SetUp(False) 'just in case user changed languages
                        Me.Visible = True
                        Me.LoadMedia(New Structures.Scans With {.Movies = True, .MovieSets = True, .TV = True})
                    Else
                        Me.FillList(True, True, True)
                        Me.Visible = True
                    End If
                End If

                Master.DB.LoadMovieSourcesFromDB()
                Master.DB.LoadTVSourcesFromDB()

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(864, "Setting menus..."))
                Me.SetMenus(True)
                Functions.GetListOfSources()
                Me.cmnuTrayExit.Enabled = True
                Me.cmnuTraySettings.Enabled = True
                Me.mnuMainEdit.Enabled = True
                If tsbMediaCenters.DropDownItems.Count > 0 Then tsbMediaCenters.Enabled = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub
    ''' <summary>
    ''' The form has been resized, so re-position those controls that need to be re-located
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            If Me.Created Then
                Me.MoveMPAA()
                Me.MoveGenres()
                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)
                Me.pnlNoInfo.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), Convert.ToInt32((Me.scMain.Panel2.Height - Me.pnlNoInfo.Height) / 2))
                Me.pnlCancel.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), 100)
                Me.pnlFilterGenre.Location = New Point(Me.gbFilterSpecific.Left + Me.txtFilterGenre.Left, (Me.pnlFilter.Top + Me.txtFilterGenre.Top + Me.gbFilterSpecific.Top) - Me.pnlFilterGenre.Height)
                Me.pnlFilterSource.Location = New Point(Me.gbFilterSpecific.Left + Me.txtFilterSource.Left, (Me.pnlFilter.Top + Me.txtFilterSource.Top + Me.gbFilterSpecific.Top) - Me.pnlFilterSource.Height)
                Me.pnlLoadSettings.Location = New Point(Convert.ToInt32((Me.Width - Me.pnlLoadSettings.Width) / 2), Convert.ToInt32((Me.Height - Me.pnlLoadSettings.Height) / 2))
                Me.pnlAllSeason.Location = New Point(Me.pbFanart.Width - Me.pnlAllSeason.Width - 9, 112)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not CloseApp Then
            Me.BringToFront()
            Me.Activate()
            Me.cmnuTray.Enabled = True
            If Not Functions.CheckIfWindows Then Mono_Shown()
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
            Case Enums.ModuleEventType.Generic
                Select Case _params(0).ToString
                    Case "controlsenabled"
                        Me.SetControlsEnabled(Convert.ToBoolean(_params(1)), If(_params.Count = 3, Convert.ToBoolean(_params(2)), False))
                End Select
            Case Enums.ModuleEventType.Notification
                Select Case _params(0).ToString
                    Case "error"
                        dlgErrorViewer.Show(Me)
                    Case Else
                        Me.Activate()
                End Select
            Case Enums.ModuleEventType.RenameMovie
                Try
                    Me.SetMovieListItemAfterEdit(Convert.ToInt16(_params(0)), Convert.ToInt16(_params(1)))
                    If Me.RefreshMovie(Convert.ToInt16(_params(0))) Then
                        Me.FillList(True, True, False)
                    End If
                    Me.SetStatus(Master.currMovie.Filename)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Case Enums.ModuleEventType.RenameMovieManual
                Try
                    Me.SetMovieListItemAfterEdit(Convert.ToInt16(_params(0)), Convert.ToInt16(_params(1)))
                    If Me.RefreshMovie(Convert.ToInt16(_params(0))) Then
                        Me.FillList(True, True, False)
                    End If
                    Me.SetStatus(Master.currMovie.Filename)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Case Else
                logger.Warn("Callback for <{0}> with no handler.", mType)
        End Select
    End Sub

    Private Sub cmnuMovieGenresGenre_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresGenre.DropDown
        Me.cmnuMovieGenresGenre.Items.Remove(Master.eLang.GetString(98, "Select Genre..."))
    End Sub

    Private Sub cmnuMovieGenresGenre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresGenre.SelectedIndexChanged
        If dgvMovies.SelectedRows.Count > 1 Then
            cmnuMovieGenresRemove.Enabled = True
            cmnuMovieGenresAdd.Enabled = True
        Else
            cmnuMovieGenresRemove.Enabled = cmnuMovieGenresGenre.Tag.ToString.Contains(cmnuMovieGenresGenre.Text)
            cmnuMovieGenresAdd.Enabled = Not cmnuMovieGenresGenre.Tag.ToString.Contains(cmnuMovieGenresGenre.Text)
        End If
        cmnuMovieGenresSet.Enabled = True
    End Sub

    Private Sub cmnuShowLanguageLanguages_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowLanguageLanguages.DropDown
        Me.cmnuShowLanguageLanguages.Items.Remove(Master.eLang.GetString(1199, "Select Language..."))
    End Sub

    Private Sub cmnuShowLanguageLanguages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowLanguageLanguages.SelectedIndexChanged
        Me.cmnuShowLanguageSet.Enabled = True
    End Sub

    Private Sub lblGFilClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblGFilClose.Click
        Me.txtFilterGenre.Focus()
        Me.pnlFilterGenre.Tag = String.Empty
    End Sub

    Private Sub lblSFilClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSFilClose.Click
        Me.txtFilterSource.Focus()
        Me.pnlFilterSource.Tag = String.Empty
    End Sub

    Private Sub LoadEpisodeInfo(ByVal ID As Integer)
        Try
            Me.dgvTVEpisodes.SuspendLayout()
            Me.SetControlsEnabled(False, True)
            Me.ShowNoInfo(False)

            If Not Me.currThemeType = Theming.ThemeType.Episode Then Me.ApplyTheme(Theming.ThemeType.Episode)

            Me.ClearInfo(False)

            Me.bwLoadEpInfo = New System.ComponentModel.BackgroundWorker
            Me.bwLoadEpInfo.WorkerSupportsCancellation = True
            Me.bwLoadEpInfo.RunWorkerAsync(New Arguments With {.ID = ID})

        Catch ex As Exception
            Me.SetControlsEnabled(True)
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub LoadMovieInfo(ByVal ID As Integer, ByVal sPath As String, ByVal doInfo As Boolean, ByVal doMI As Boolean, Optional ByVal setEnabled As Boolean = False)
        Try
            Me.dgvMovies.SuspendLayout()
            Me.SetControlsEnabled(False, True)
            Me.ShowNoInfo(False)

            If doMI Then
                If Me.bwMetaInfo.IsBusy Then Me.bwMetaInfo.CancelAsync()

                Me.txtMetaData.Clear()
                Me.pbMILoading.Visible = True

                Me.bwMetaInfo = New System.ComponentModel.BackgroundWorker
                Me.bwMetaInfo.WorkerSupportsCancellation = True
                Me.bwMetaInfo.RunWorkerAsync(New Arguments With {.setEnabled = setEnabled, .Path = sPath, .Movie = Master.currMovie})
            End If

            If doInfo Then
                Me.ClearInfo()

                Me.bwLoadMovieInfo = New System.ComponentModel.BackgroundWorker
                Me.bwLoadMovieInfo.WorkerSupportsCancellation = True
                Me.bwLoadMovieInfo.RunWorkerAsync(New Arguments With {.ID = ID})
            End If
        Catch ex As Exception
            Me.SetControlsEnabled(True)
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub LoadMovieSetInfo(ByVal ID As Integer, ByVal sPath As String, ByVal doInfo As Boolean, ByVal doMI As Boolean, Optional ByVal setEnabled As Boolean = False)
        Try
            Me.dgvMovieSets.SuspendLayout()
            Me.SetControlsEnabled(False, True)
            Me.ShowNoInfo(False)

            If doMI Then
                If Me.bwMovieSetInfo.IsBusy Then Me.bwMovieSetInfo.CancelAsync()

                Me.txtMetaData.Clear()
                Me.pbMILoading.Visible = True

                Me.bwMovieSetInfo = New System.ComponentModel.BackgroundWorker
                Me.bwMovieSetInfo.WorkerSupportsCancellation = True
                Me.bwMovieSetInfo.RunWorkerAsync(New Arguments With {.setEnabled = setEnabled, .Path = sPath, .MovieSet = Master.currMovieSet})
            End If

            If doInfo Then
                Me.ClearInfo()

                Me.bwLoadMovieSetInfo = New System.ComponentModel.BackgroundWorker
                Me.bwLoadMovieSetInfo.WorkerSupportsCancellation = True
                Me.bwLoadMovieSetInfo.RunWorkerAsync(New Arguments With {.ID = ID})
            End If
        Catch ex As Exception
            Me.SetControlsEnabled(True)
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub LoadSeasonInfo(ByVal ShowID As Integer, ByVal Season As Integer)
        Try
            Me.dgvTVSeasons.SuspendLayout()
            Me.SetControlsEnabled(False, True)
            Me.ShowNoInfo(False)

            If Not Me.currThemeType = Theming.ThemeType.Show Then
                Me.ApplyTheme(Theming.ThemeType.Show)
            End If

            Me.ClearInfo(False)

            Me.bwLoadSeasonInfo = New System.ComponentModel.BackgroundWorker
            Me.bwLoadSeasonInfo.WorkerSupportsCancellation = True
            Me.bwLoadSeasonInfo.RunWorkerAsync(New Arguments With {.ID = ShowID, .Season = Season})

            Me.FillEpisodes(ShowID, Season)

        Catch ex As Exception
            Me.SetControlsEnabled(True)
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub LoadShowInfo(ByVal ID As Integer)
        Try
            Me.dgvTVShows.SuspendLayout()
            Me.SetControlsEnabled(False, True)
            Me.ShowNoInfo(False)

            If Not Me.currThemeType = Theming.ThemeType.Show Then Me.ApplyTheme(Theming.ThemeType.Show)

            Me.ClearInfo()

            Me.bwLoadShowInfo = New System.ComponentModel.BackgroundWorker
            Me.bwLoadShowInfo.WorkerSupportsCancellation = True
            Me.bwLoadShowInfo.RunWorkerAsync(New Arguments With {.ID = ID})

            Me.FillSeasons(ID)

        Catch ex As Exception
            Me.SetControlsEnabled(True)
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lstActors_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstActors.SelectedValueChanged
        '//
        ' Begin thread to download actor image if one exists
        '\\
        Try
            If Me.lstActors.Items.Count > 0 AndAlso Me.lstActors.SelectedItems.Count > 0 AndAlso Not IsNothing(Me.alActors.Item(Me.lstActors.SelectedIndex)) AndAlso Not Me.alActors.Item(Me.lstActors.SelectedIndex).ToString = "none" Then

                If Not IsNothing(Me.pbActors.Image) Then
                    Me.pbActors.Image.Dispose()
                    Me.pbActors.Image = Nothing
                End If

                Me.pbActLoad.Visible = True

                If Me.bwDownloadPic.IsBusy Then
                    Me.bwDownloadPic.CancelAsync()
                    While Me.bwDownloadPic.IsBusy
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                End If

                Me.bwDownloadPic = New System.ComponentModel.BackgroundWorker
                Me.bwDownloadPic.WorkerSupportsCancellation = True
                Me.bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = Me.alActors.Item(Me.lstActors.SelectedIndex).ToString})

            Else
                Me.pbActors.Image = My.Resources.actor_silhouette
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub mnuAllAskAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskAll.Click, cmnuTrayAllAskAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskBanner.Click, cmnuTrayAllAskBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskClearArt.Click, cmnuTrayAllAskClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskClearLogo.Click, cmnuTrayAllAskClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskDiscArt.Click, cmnuTrayAllAskDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskEFanarts.Click, cmnuTrayAllAskEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskEThumbs.Click, cmnuTrayAllAskEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskFanart.Click, cmnuTrayAllAskFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskLandscape.Click, cmnuTrayAllAskLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskMI.Click, cmnuTrayAllAskMI.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskNfo.Click, cmnuTrayAllAskNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskPoster.Click, cmnuTrayAllAskPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskTheme.Click, cmnuTrayAllAskTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAskTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAskTrailer.Click, cmnuTrayAllAskTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoAll.Click, cmnuTrayAllAutoAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoBanner.Click, cmnuTrayAllAutoBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoClearArt.Click, cmnuTrayAllAutoClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoClearLogo.Click, cmnuTrayAllAutoClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoDiscArt.Click, cmnuTrayAllAutoDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoEFanarts.Click, cmnuTrayAllAutoEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoEThumbs.Click, cmnuTrayAllAutoEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoFanart.Click, cmnuTrayAllAutoFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoLandscape.Click, cmnuTrayAllAutoLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoMI.Click, cmnuTrayAllAutoMetaData.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoNfo.Click, cmnuTrayAllAutoNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoPoster.Click, cmnuTrayAllAutoPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoTheme.Click, cmnuTrayAllAutoTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoTrailer.Click, cmnuTrayAllAutoTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllAutoActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllAutoActor.Click, cmnuTrayAllAutoActor.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ActorThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuAllSkipAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAllSkipAll.Click, cmnuTrayAllSkipAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FullSkip, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskAll.Click, cmnuTrayFilterAskAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskBanner.Click, cmnuTrayFilterAskBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskClearArt.Click, cmnuTrayFilterAskClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskClearLogo.Click, cmnuTrayFilterAskClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskDiscArt.Click, cmnuTrayFilterAskDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskEFanarts.Click, cmnuTrayFilterAskEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskEThumbs.Click, cmnuTrayFilterAskEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskFanart.Click, cmnuTrayFilterAskFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskLandscape.Click, cmnuTrayFilterAskLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskMI.Click, cmnuTrayFilterAskMI.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskNfo.Click, cmnuTrayFilterAskNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskPoster.Click, cmnuTrayFilterAskPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskTheme.Click, cmnuTrayFilterAskTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAskTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAskTrailer.Click, cmnuTrayFilterAskTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoAll.Click, cmnuTrayFilterAutoAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoBanner.Click, cmnuTrayFilterAutoBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoClearArt.Click, cmnuTrayFilterAutoClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoClearLogo.Click, cmnuTrayFilterAutoClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoDiscArt.Click, cmnuTrayFilterAutoDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoEFanarts.Click, cmnuTrayFilterAutoEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoEThumbs.Click, cmnuTrayFilterAutoEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoFanart.Click, cmnuTrayFilterAutoFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoLandscape.Click, cmnuTrayFilterAutoLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoMI.Click, cmnuTrayFilterAutoMI.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoNfo.Click, cmnuTrayFilterAutoNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoPoster.Click, cmnuTrayFilterAutoPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoTheme.Click, cmnuTrayFilterAutoTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterAutoTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterAutoTrailer.Click, cmnuTrayFilterAutoTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuFilterSkipAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilterSkipAll.Click, cmnuTrayFilterSkipAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.FilterSkip, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskAll.Click, cmnuTrayMarkAskAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskBanner.Click, cmnuTrayMarkAskBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskClearArt.Click, cmnuTrayMarkAskClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskClearLogo.Click, cmnuTrayMarkAskClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskDiscArt.Click, cmnuTrayMarkAskDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskEFanarts.Click, cmnuTrayMarkAskEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskEThumbs.Click, cmnuTrayMarkAskEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskFanart.Click, cmnuTrayMarkAskFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskLandscape.Click, cmnuTrayMarkAskLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskMI.Click, cmnuTrayMarkAskMI.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskNfo.Click, cmnuTrayMarkAskNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskPoster.Click, cmnuTrayMarkAskPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAskTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskTheme.Click, cmnuTrayMarkAskTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub '

    Private Sub mnuMarkAskTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAskTrailer.Click, cmnuTrayMarkAskTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoAll.Click, cmnuTrayMarkAutoAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoBanner.Click, cmnuTrayMarkAutoBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoClearArt.Click, cmnuTrayMarkAutoClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoClearLogo.Click, cmnuTrayMarkAutoClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoDiscArt.Click, cmnuTrayMarkAutoDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoEFanarts.Click, cmnuTrayMarkAutoEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoEThumbs.Click, cmnuTrayMarkAutoEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoFanart.Click, cmnuTrayMarkAutoFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoLandscape.Click, cmnuTrayMarkAutoLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoMI.Click, cmnuTrayMarkAutoMI.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoNfo.Click, cmnuTrayMarkAutoNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoPoster.Click, cmnuTrayMarkAutoPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoTheme.Click, cmnuTrayMarkAutoTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoTrailer.Click, cmnuTrayMarkAutoTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkAutoActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkAutoActor.Click, cmnuTrayMarkAutoActor.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ActorThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMarkSkipAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarkSkipAll.Click, cmnuTrayMarkSkipAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.MarkSkip, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskAll.Click, cmnuTrayMissAskAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskBanner.Click, cmnuTrayMissAskBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskClearArt.Click, cmnuTrayMissAskClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskClearLogo.Click, cmnuTrayMissAskClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskDiscArt.Click, cmnuTrayMissAskDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskEFanarts.Click, cmnuTrayMissAskEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskEThumbs.Click, cmnuTrayMissAskEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskFanart.Click, cmnuTrayMissAskFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskLandscape.Click, cmnuTrayMissAskLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskNfo.Click, cmnuTrayMissAskNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskPoster.Click, cmnuTrayMissAskPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskTheme.Click, cmnuTrayMissAskTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAskTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAskTrailer.Click, cmnuTrayMissAskTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoAll.Click, cmnuTrayMissAutoAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoBanner.Click, cmnuTrayMissAutoBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoClearArt.Click, cmnuTrayMissAutoClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoClearLogo.Click, cmnuTrayMissAutoClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoDiscArt.Click, cmnuTrayMissAutoDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoEFanarts.Click, cmnuTrayMissAutoEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoEThumbs.Click, cmnuTrayMissAutoEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoFanart.Click, cmnuTrayMissAutoFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoLandscape.Click, cmnuTrayMissAutoLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoNfo.Click, cmnuTrayMissAutoNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoPoster.Click, cmnuTrayMissAutoPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoTheme.Click, cmnuTrayMissAutoTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissAutoTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissAutoTrailer.Click, cmnuTrayMissAutoTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuMissSkipAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMissSkipAll.Click, cmnuTrayMissSkipAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.UpdateSkip, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskAll.Click, cmnuTrayNewAskAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskBanner.Click, cmnuTrayNewAskBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskClearArt.Click, cmnuTrayNewAskClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskClearLogo.Click, cmnuTrayNewAskClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskDiscArt.Click, cmnuTrayNewAskDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskEFanarts.Click, cmnuTrayNewAskEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskEThumbs.Click, cmnuTrayNewAskEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskFanart.Click, cmnuTrayNewAskFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskLandscape.Click, cmnuTrayNewAskLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskMI.Click, cmnuTrayNewAskMI.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskNfo.Click, cmnuTrayNewAskNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskPoster.Click, cmnuTrayNewAskPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskTheme.Click, cmnuTrayNewAskTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAskTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAskTrailer.Click, cmnuTrayNewAskTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoAll.Click, cmnuTrayNewAutoAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoBanner.Click, cmnuTrayNewAutoBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoClearArt.Click, cmnuTrayNewAutoClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoClearLogo.Click, cmnuTrayNewAutoClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoDiscArt.Click, cmnuTrayNewAutoDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoEFanarts.Click, cmnuTrayNewAutoEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoEThumbs.Click, cmnuTrayNewAutoEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoFanart.Click, cmnuTrayNewAutoFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoLandscape.Click, cmnuTrayNewAutoLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoMI.Click, cmnuTrayNewAutoMI.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoNfo.Click, cmnuTrayNewAutoNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoPoster.Click, cmnuTrayNewAutoPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoTheme.Click, cmnuTrayNewAutoTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewAutoTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAutoTrailer.Click, cmnuTrayNewAutoTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub mnuNewSkipAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewSkipAll.Click, cmnuTrayNewSkipAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        Me.MovieScrapeData(False, Enums.ScrapeType.NewSkip, Master.DefaultMovieOptions)
    End Sub

    Private Sub Mono_Shown()
        Me.pnlNoInfo.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), Convert.ToInt32((Me.scMain.Panel2.Height - Me.pnlNoInfo.Height) / 2))
    End Sub
    ''' <summary>
    ''' Slide the genre images along with the panel and move with form resizing
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveGenres()
        Try
            For i As Integer = 0 To UBound(Me.pnlGenre)
                Me.pnlGenre(i).Left = ((Me.pnlInfoPanel.Right) - (i * 73)) - 73
                Me.pnlGenre(i).Top = Me.pnlInfoPanel.Top - 105
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Slide the MPAA image along with the panel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveMPAA()
        Me.pnlMPAA.BringToFront()
        Me.pnlMPAA.Size = New Size(Me.pbMPAA.Width + 10, Me.pbMPAA.Height + 10)
        Me.pbMPAA.Location = New Point(4, 4)
        Me.pnlMPAA.Top = Me.pnlInfoPanel.Top - (Me.pnlMPAA.Height + 10)
    End Sub

    Private Sub MovieInfoDownloaded()
        'Dim dlgImgS As dlgImgSelect
        'Dim aList As New List(Of MediaContainers.Image)

        Try
            If Not String.IsNullOrEmpty(Master.currMovie.Movie.Title) Then 'changed from Master.tmpMovie.Title to Master.currMovie.Movie.Title)

                Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)

                Me.tslLoading.Text = Master.eLang.GetString(576, "Verifying Movie Details:")
                Application.DoEvents()

                Using dEditMovie As New dlgEditMovie
                    AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                    Select Case dEditMovie.ShowDialog()
                        Case Windows.Forms.DialogResult.OK
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.RenameMovie, New List(Of Object)(New Object() {False, False, False}), Master.currMovie)
                            Me.SetMovieListItemAfterEdit(ID, indX)
                            If Me.RefreshMovie(ID) Then
                                Me.FillList(True, True, False)
                            Else
                                Me.FillList(False, True, False)
                            End If
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                        Case Windows.Forms.DialogResult.Retry
                            Master.currMovie.RemoveActorThumbs = False
                            Master.currMovie.RemoveBanner = False
                            Master.currMovie.RemoveClearArt = False
                            Master.currMovie.RemoveClearLogo = False
                            Master.currMovie.RemoveDiscArt = False
                            Master.currMovie.RemoveEThumbs = False
                            Master.currMovie.RemoveEFanarts = False
                            Master.currMovie.RemoveFanart = False
                            Master.currMovie.RemoveLandscape = False
                            Master.currMovie.RemovePoster = False
                            Master.currMovie.RemoveTheme = False
                            Master.currMovie.RemoveTrailer = False
                            Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                            Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions) ', ID)
                        Case Windows.Forms.DialogResult.Abort
                            Master.currMovie.RemoveActorThumbs = True
                            Master.currMovie.RemoveBanner = True
                            Master.currMovie.RemoveClearArt = True
                            Master.currMovie.RemoveClearLogo = True
                            Master.currMovie.RemoveDiscArt = True
                            Master.currMovie.RemoveEThumbs = True
                            Master.currMovie.RemoveEFanarts = True
                            Master.currMovie.RemoveFanart = True
                            Master.currMovie.RemoveLandscape = True
                            Master.currMovie.RemovePoster = True
                            Master.currMovie.RemoveTheme = True
                            Master.currMovie.RemoveTrailer = True
                            Functions.SetScraperMod(Enums.ModType_Movie.DoSearch, True)
                            Functions.SetScraperMod(Enums.ModType_Movie.All, True, False)
                            Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieOptions) ', ID, True)
                        Case Else
                            If Me.InfoCleared Then Me.LoadMovieInfo(ID, Me.dgvMovies.Item(1, indX).Value.ToString, True, False)
                    End Select
                    RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                End Using

            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Master.currMovie.RemoveActorThumbs = False
        Master.currMovie.RemoveBanner = False
        Master.currMovie.RemoveClearArt = False
        Master.currMovie.RemoveClearLogo = False
        Master.currMovie.RemoveDiscArt = False
        Master.currMovie.RemoveEThumbs = False
        Master.currMovie.RemoveEFanarts = False
        Master.currMovie.RemoveFanart = False
        Master.currMovie.RemoveLandscape = False
        Master.currMovie.RemovePoster = False
        Master.currMovie.RemoveTheme = False
        Master.currMovie.RemoveTrailer = False

        Me.pnlCancel.Visible = False
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.SetStatus(String.Empty)
        Me.SetControlsEnabled(True)
        Me.EnableFilters(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub MovieInfoDownloadedPercent(ByVal iPercent As Integer)
        If Me.ReportDownloadPercent = True Then
            Me.tspbLoading.Value = iPercent
            Me.Refresh()
        End If
    End Sub

    Private Sub MovieScrapeData(ByVal selected As Boolean, ByVal sType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie, Optional ByVal Restart As Boolean = False)
        ScrapeList.Clear()

        If selected Then
            'create snapshoot list of selected movies
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                ScrapeList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
            Next
        Else
            Dim BannerAllowed As Boolean = Master.eSettings.MovieBannerAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Banner)
            Dim ClearArtAllowed As Boolean = Master.eSettings.MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.ClearArt)
            Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.ClearLogo)
            Dim DiscArtAllowed As Boolean = Master.eSettings.MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.DiscArt)
            Dim EFanartsAllowed As Boolean = Master.eSettings.MovieEFanartsAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Fanart)
            Dim EThumbsAllowed As Boolean = Master.eSettings.MovieEFanartsAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Fanart)
            Dim FanartAllowed As Boolean = Master.eSettings.MovieFanartAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Fanart)
            Dim LandscapeAllowed As Boolean = Master.eSettings.MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Landscape)
            Dim PosterAllowed As Boolean = Master.eSettings.MoviePosterAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Poster)
            Dim ThemeAllowed As Boolean = Master.eSettings.MovieThemeEnable AndAlso Master.eSettings.MovieThemeAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Theme)
            Dim TrailerAllowed As Boolean = Master.eSettings.MovieTrailerEnable AndAlso Master.eSettings.MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Trailer_Movie(Enums.ScraperCapabilities.Trailer)

            'create list of movies acording to scrapetype
            For Each drvRow As DataRow In Me.dtMovies.Rows

                If Convert.ToBoolean(drvRow.Item(14)) Then Continue For

                Select Case sType
                    Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                        If Not Convert.ToBoolean(drvRow.Item(10)) Then Continue For
                    Case Enums.ScrapeType.MarkAsk, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.MarkSkip
                        If Not Convert.ToBoolean(drvRow.Item(11)) Then Continue For
                    Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                        Dim index As Integer = Me.bsMovies.Find("id", drvRow.Item(0))
                        If Not index >= 0 Then Continue For
                    Case Enums.ScrapeType.UpdateAsk, Enums.ScrapeType.UpdateAuto, Enums.ScrapeType.UpdateSkip
                        If Not ((Master.GlobalScrapeMod.Banner AndAlso Master.eSettings.MovieMissingBanner AndAlso BannerAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(51))) OrElse _
                                (Master.GlobalScrapeMod.ClearArt AndAlso Master.eSettings.MovieMissingClearArt AndAlso ClearArtAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(61))) OrElse _
                                (Master.GlobalScrapeMod.ClearLogo AndAlso Master.eSettings.MovieMissingClearLogo AndAlso ClearLogoAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(59))) OrElse _
                                (Master.GlobalScrapeMod.DiscArt AndAlso Master.eSettings.MovieMissingDiscArt AndAlso DiscArtAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(57))) OrElse _
                                (Master.GlobalScrapeMod.EFanarts AndAlso Master.eSettings.MovieMissingEFanarts AndAlso EFanartsAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(49))) OrElse _
                                (Master.GlobalScrapeMod.EThumbs AndAlso Master.eSettings.MovieMissingEThumbs AndAlso EThumbsAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(9))) OrElse _
                                (Master.GlobalScrapeMod.Fanart AndAlso Master.eSettings.MovieMissingFanart AndAlso FanartAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(5))) OrElse _
                                (Master.GlobalScrapeMod.Landscape AndAlso Master.eSettings.MovieMissingLandscape AndAlso LandscapeAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(53))) OrElse _
                                (Master.GlobalScrapeMod.NFO AndAlso Master.eSettings.MovieMissingNFO AndAlso Not Convert.ToBoolean(drvRow.Item(6))) OrElse _
                                (Master.GlobalScrapeMod.Poster AndAlso Master.eSettings.MovieMissingPoster AndAlso PosterAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(4))) OrElse _
                                (Master.GlobalScrapeMod.Theme AndAlso Master.eSettings.MovieMissingTheme AndAlso ThemeAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(55))) OrElse _
                                (Master.GlobalScrapeMod.Trailer AndAlso Master.eSettings.MovieMissingTrailer AndAlso TrailerAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(7)))) Then
                            Continue For
                        End If
                End Select

                ScrapeList.Add(drvRow)
            Next
        End If

        Me.SetControlsEnabled(False)

        Me.tspbLoading.Value = 0
        If ScrapeList.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Maximum = ScrapeList.Count
        Else
            Me.tspbLoading.Maximum = 100
            Me.tspbLoading.Style = ProgressBarStyle.Marquee
        End If

        If Not selected Then
            Select Case sType
                Case Enums.ScrapeType.FullAsk
                    Me.tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
                Case Enums.ScrapeType.FullAuto
                    Me.tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
                Case Enums.ScrapeType.FullSkip
                    Me.tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
                Case Enums.ScrapeType.UpdateAuto
                    Me.tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
                Case Enums.ScrapeType.UpdateAsk
                    Me.tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
                Case Enums.ScrapeType.UpdateSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
                Case Enums.ScrapeType.NewAsk
                    Me.tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
                Case Enums.ScrapeType.NewAuto
                    Me.tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
                Case Enums.ScrapeType.NewSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
                Case Enums.ScrapeType.MarkAsk
                    Me.tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
                Case Enums.ScrapeType.MarkAuto
                    Me.tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
                Case Enums.ScrapeType.MarkSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
                Case Enums.ScrapeType.FilterAsk
                    Me.tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
                Case Enums.ScrapeType.FilterAuto
                    Me.tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
                Case Enums.ScrapeType.FilterAuto
                    Me.tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
                Case Enums.ScrapeType.SingleScrape
                    Me.tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
            End Select
        Else
            Select Case sType
                Case Enums.ScrapeType.FullAsk
                    Me.tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
                Case Enums.ScrapeType.FullAuto
                    Me.tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
                Case Enums.ScrapeType.FullSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
                Case Enums.ScrapeType.SingleField
                    Me.tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
            End Select
        End If

        If Not sType = Enums.ScrapeType.SingleScrape Then
            Me.btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
            Me.lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True
        End If

        Me.tslLoading.Visible = True
        Me.tspbLoading.Visible = True
        Application.DoEvents()
        bwMovieScraper.WorkerSupportsCancellation = True
        bwMovieScraper.WorkerReportsProgress = True
        bwMovieScraper.RunWorkerAsync(New Arguments With {.scrapeType = sType, .Options = Options})
    End Sub

    Private Sub MovieSetInfoDownloaded()
        'Dim dlgImgS As dlgImgSelect
        'Dim aList As New List(Of MediaContainers.Image)

        Try
            If Not String.IsNullOrEmpty(Master.currMovieSet.ListTitle) Then 'changed from Master.tmpMovie.Title to Master.currMovie.Movie.Title)

                Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item(0, indX).Value)

                Me.tslLoading.Text = Master.eLang.GetString(1205, "Verifying MovieSet Details:")
                Application.DoEvents()

                Using dEditMovieSet As New dlgEditMovieSet
                    'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
                    Select Case dEditMovieSet.ShowDialog()
                        Case Windows.Forms.DialogResult.OK
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSetScraperRDYtoSave, Nothing, Master.currMovieSet)
                            Me.SetMovieSetListItemAfterEdit(ID, indX)
                            If Me.RefreshMovieSet(ID) Then
                                Me.FillList(False, True, False)
                            Else
                                Me.FillList(False, False, False) 'TODO: check if correct
                            End If
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSetSync, Nothing, Master.currMovieSet)
                        Case Windows.Forms.DialogResult.Retry
                            Master.currMovieSet.RemoveBanner = False
                            Master.currMovieSet.RemoveClearArt = False
                            Master.currMovieSet.RemoveClearLogo = False
                            Master.currMovieSet.RemoveDiscArt = False
                            Master.currMovieSet.RemoveFanart = False
                            Master.currMovieSet.RemoveLandscape = False
                            Master.currMovieSet.RemovePoster = False
                            Functions.SetScraperMod(Enums.ModType_Movie.All, True, True)
                            Me.MovieSetScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieSetOptions) ', ID)
                        Case Windows.Forms.DialogResult.Abort
                            'Master.currMovieSet.RemoveBanner = True
                            'Master.currMovieSet.RemoveClearArt = True
                            'Master.currMovieSet.RemoveClearLogo = True
                            'Master.currMovieSet.RemoveDiscArt = True
                            'Master.currMovieSet.RemoveFanart = True
                            'Master.currMovieSet.RemoveLandscape = True
                            'Master.currMovieSet.RemovePoster = True
                            'Functions.SetScraperMod(Enums.MovieModType.DoSearch, True)
                            'Functions.SetScraperMod(Enums.MovieModType.All, True, False)
                            'Me.MovieSetScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultMovieSetOptions) ', ID, True)
                        Case Else
                            If Me.InfoCleared Then Me.LoadMovieSetInfo(ID, Me.dgvMovieSets.Item(1, indX).Value.ToString, True, False)
                    End Select
                    'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovieSet.GenericRunCallBack
                End Using

            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Master.currMovieSet.RemoveBanner = False
        Master.currMovieSet.RemoveClearArt = False
        Master.currMovieSet.RemoveClearLogo = False
        Master.currMovieSet.RemoveDiscArt = False
        Master.currMovieSet.RemoveFanart = False
        Master.currMovieSet.RemoveLandscape = False
        Master.currMovieSet.RemovePoster = False

        Me.pnlCancel.Visible = False
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.SetStatus(String.Empty)
        Me.SetControlsEnabled(True)
        Me.EnableFilters(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub MovieSetInfoDownloadedPercent(ByVal iPercent As Integer)
        If Me.ReportDownloadPercent = True Then
            Me.tspbLoading.Value = iPercent
            Me.Refresh()
        End If
    End Sub

    Private Sub MovieSetScrapeData(ByVal selected As Boolean, ByVal sType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_MovieSet)
        ScrapeList.Clear()

        If selected Then
            'create snapshoot list of selected movies
            For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                ScrapeList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
            Next
        Else
            Dim BannerAllowed As Boolean = Master.eSettings.MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ScraperCapabilities.Banner)
            Dim ClearArtAllowed As Boolean = Master.eSettings.MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ScraperCapabilities.ClearArt)
            Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ScraperCapabilities.ClearLogo)
            Dim DiscArtAllowed As Boolean = Master.eSettings.MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ScraperCapabilities.DiscArt)
            Dim FanartAllowed As Boolean = Master.eSettings.MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ScraperCapabilities.Fanart)
            Dim LandscapeAllowed As Boolean = Master.eSettings.MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ScraperCapabilities.Landscape)
            Dim PosterAllowed As Boolean = Master.eSettings.MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ScraperCapabilities.Poster)

            'create list of moviesets acording to scrapetype
            For Each drvRow As DataRow In Me.dtMovieSets.Rows

                'If Convert.ToBoolean(drvRow.Item(14)) Then Continue For 'no Lock for MovieSets

                Select Case sType
                    Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                        Continue For 'If Not Convert.ToBoolean(drvRow.Item(10)) Then Continue For
                    Case Enums.ScrapeType.MarkAsk, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.MarkSkip
                        Continue For 'If Not Convert.ToBoolean(drvRow.Item(11)) Then Continue For
                    Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                        'Dim index As Integer = Me.bsMovies.Find("id", drvRow.Item(0))
                        'If Not index >= 0 Then Continue For
                        Continue For
                    Case Enums.ScrapeType.UpdateAsk, Enums.ScrapeType.UpdateAuto, Enums.ScrapeType.UpdateSkip
                        If Not ((Master.GlobalScrapeMod.Banner AndAlso Master.eSettings.MovieSetMissingBanner AndAlso BannerAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(8))) OrElse _
                                (Master.GlobalScrapeMod.ClearArt AndAlso Master.eSettings.MovieSetMissingClearArt AndAlso ClearArtAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(16))) OrElse _
                                (Master.GlobalScrapeMod.ClearLogo AndAlso Master.eSettings.MovieSetMissingClearLogo AndAlso ClearLogoAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(14))) OrElse _
                                (Master.GlobalScrapeMod.DiscArt AndAlso Master.eSettings.MovieSetMissingDiscArt AndAlso DiscArtAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(12))) OrElse _
                                (Master.GlobalScrapeMod.Fanart AndAlso Master.eSettings.MovieSetMissingFanart AndAlso FanartAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(6))) OrElse _
                                (Master.GlobalScrapeMod.Landscape AndAlso Master.eSettings.MovieSetMissingLandscape AndAlso LandscapeAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(10))) OrElse _
                                (Master.GlobalScrapeMod.NFO AndAlso Master.eSettings.MovieSetMissingNFO AndAlso Not Convert.ToBoolean(drvRow.Item(2))) OrElse _
                                (Master.GlobalScrapeMod.Poster AndAlso Master.eSettings.MovieSetMissingPoster AndAlso PosterAllowed AndAlso Not Convert.ToBoolean(drvRow.Item(4)))) Then
                            Continue For
                        End If
                End Select

                ScrapeList.Add(drvRow)
            Next
        End If

        Me.SetControlsEnabled(False)

        Me.tspbLoading.Value = 0
        If ScrapeList.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Maximum = ScrapeList.Count
        Else
            Me.tspbLoading.Maximum = 100
            Me.tspbLoading.Style = ProgressBarStyle.Marquee
        End If

        If Not selected Then
            Select Case sType
                Case Enums.ScrapeType.FullAsk
                    Me.tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
                Case Enums.ScrapeType.FullAuto
                    Me.tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
                Case Enums.ScrapeType.FullSkip
                    Me.tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
                Case Enums.ScrapeType.UpdateAuto
                    Me.tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
                Case Enums.ScrapeType.UpdateAsk
                    Me.tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
                Case Enums.ScrapeType.UpdateSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
                Case Enums.ScrapeType.NewAsk
                    Me.tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
                Case Enums.ScrapeType.NewAuto
                    Me.tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
                Case Enums.ScrapeType.NewSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
                Case Enums.ScrapeType.MarkAsk
                    Me.tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
                Case Enums.ScrapeType.MarkAuto
                    Me.tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
                Case Enums.ScrapeType.MarkSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
                Case Enums.ScrapeType.FilterAsk
                    Me.tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
                Case Enums.ScrapeType.FilterAuto
                    Me.tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
                Case Enums.ScrapeType.FilterAuto
                    Me.tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
                Case Enums.ScrapeType.SingleScrape
                    Me.tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
            End Select
        Else
            Select Case sType
                Case Enums.ScrapeType.FullAsk
                    Me.tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
                Case Enums.ScrapeType.FullAuto
                    Me.tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
                Case Enums.ScrapeType.FullSkip
                    Me.tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
                Case Enums.ScrapeType.SingleField
                    Me.tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
            End Select
        End If

        If Not sType = Enums.ScrapeType.SingleScrape Then
            Me.btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
            Me.lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True
        End If

        Me.tslLoading.Visible = True
        Me.tspbLoading.Visible = True
        Application.DoEvents()
        bwMovieSetScraper.WorkerSupportsCancellation = True
        bwMovieSetScraper.WorkerReportsProgress = True
        bwMovieSetScraper.RunWorkerAsync(New Arguments With {.scrapeType = sType, .Options_MovieSet = Options})
    End Sub

    Private Sub MovieScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)
        If (Me.InvokeRequired) Then
            Me.Invoke(New DelegateEvent_Movie(AddressOf MovieScraperEvent), New Object() {eType, Parameter})
        Else
            Select Case eType
                Case Enums.ScraperEventType_Movie.BannerItem
                    dScrapeRow(51) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.ClearArtItem
                    dScrapeRow(61) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.ClearLogoItem
                    dScrapeRow(59) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.DiscArtItem
                    dScrapeRow(57) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.EFanartsItem
                    dScrapeRow(49) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.EThumbsItem
                    dScrapeRow(9) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.FanartItem
                    dScrapeRow(5) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.LandscapeItem
                    dScrapeRow(53) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.ListTitle
                    dScrapeRow(3) = DirectCast(Parameter, String)
                Case Enums.ScraperEventType_Movie.MoviePath
                    dScrapeRow(1) = DirectCast(Parameter, String)
                Case Enums.ScraperEventType_Movie.NFOItem
                    dScrapeRow(6) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.PosterItem
                    dScrapeRow(4) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.SortTitle
                    dScrapeRow(47) = DirectCast(Parameter, String)
                Case Enums.ScraperEventType_Movie.ThemeItem
                    dScrapeRow(55) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_Movie.TrailerItem
                    dScrapeRow(7) = DirectCast(Parameter, Boolean)
            End Select
            Me.dgvMovies.Invalidate()
        End If
    End Sub

    Private Sub MovieSetScraperEvent(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)
        If (Me.InvokeRequired) Then
            Me.Invoke(New DelegateEvent_MovieSet(AddressOf MovieSetScraperEvent), New Object() {eType, Parameter})
        Else
            Select Case eType
                Case Enums.ScraperEventType_MovieSet.BannerItem
                    dScrapeRow(8) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_MovieSet.ClearArtItem
                    dScrapeRow(16) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_MovieSet.ClearLogoItem
                    dScrapeRow(14) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_MovieSet.DiscArtItem
                    dScrapeRow(12) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_MovieSet.FanartItem
                    dScrapeRow(6) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_MovieSet.LandscapeItem
                    dScrapeRow(10) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_MovieSet.ListTitle
                    dScrapeRow(1) = DirectCast(Parameter, String)
                Case Enums.ScraperEventType_MovieSet.NFOItem
                    dScrapeRow(2) = DirectCast(Parameter, Boolean)
                Case Enums.ScraperEventType_MovieSet.PosterItem
                    dScrapeRow(4) = DirectCast(Parameter, Boolean)
            End Select
            Me.dgvMovieSets.Invalidate()
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

    Private Sub NonScrape(ByVal sType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie)
        Me.Cursor = Cursors.WaitCursor

        Select Case sType
            Case Enums.ScrapeType.CleanFolders
                Me.btnCancel.Text = Master.eLang.GetString(120, "Cancel Cleaner")
                Me.lblCanceling.Text = Master.eLang.GetString(119, "Canceling File Cleaner...")
                Me.tslLoading.Text = Master.eLang.GetString(129, "Cleaning Files:")
            Case Enums.ScrapeType.CopyBackdrops
                Me.btnCancel.Text = Master.eLang.GetString(122, "Cancel Copy")
                Me.lblCanceling.Text = Master.eLang.GetString(121, "Canceling Backdrop Copy...")
                Me.tslLoading.Text = Master.eLang.GetString(130, "Copying Fanart to Backdrops Folder:")
            Case Else
                logger.Warn("Invalid sType: <{0}>", sType)
        End Select

        btnCancel.Visible = True
        lblCanceling.Visible = False
        prbCanceling.Visible = False
        Me.pnlCancel.Visible = True
        Me.tslLoading.Visible = True
        Me.tspbLoading.Value = 0
        Me.tspbLoading.Maximum = Me.dtMovies.Rows.Count
        Me.tspbLoading.Visible = True
        Me.SetControlsEnabled(False, True)
        Me.EnableFilters(False)

        bwNonScrape.WorkerReportsProgress = True
        bwNonScrape.WorkerSupportsCancellation = True
        bwNonScrape.RunWorkerAsync(New Arguments With {.scrapeType = sType, .Options = Options})
    End Sub

    Private Sub cmnuMovieOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieOpenFolder.Click
        If Me.dgvMovies.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If Me.dgvMovies.SelectedRows.Count > 10 Then
                If Not MsgBox(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvMovies.SelectedRows.Count), MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    Using Explorer As New Diagnostics.Process

                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", sRow.Cells(1).Value)
                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            Explorer.StartInfo.Arguments = String.Format("""{0}""", Path.GetDirectoryName(sRow.Cells(1).Value.ToString))
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
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
        Me.GenreImage = DirectCast(sender, PictureBox).Image    'Store the image for later retrieval
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

    Private Sub pbClearArt_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbClearArt.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Not IsNothing(Me.pbClearArtCache.Image) Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbClearArtCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)

                        Dim dlgImgS As dlgImgSelect
                        Dim aList As New List(Of MediaContainers.Image)
                        Dim pResults As New MediaContainers.Image
                        Dim efList As New List(Of String)
                        Dim etList As New List(Of String)
                        Dim newImage As New Images

                        If Not ModulesManager.Instance.ScrapeImage_Movie(Master.currMovie, Enums.ScraperCapabilities.ClearArt, aList) Then
                            If aList.Count > 0 Then
                                dlgImgS = New dlgImgSelect()
                                If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.ClearArt, aList, efList, etList, True) = DialogResult.OK Then
                                    pResults = dlgImgS.Results
                                    If Not String.IsNullOrEmpty(pResults.URL) Then
                                        Cursor = Cursors.WaitCursor
                                        pResults.WebImage.FromWeb(pResults.URL)
                                        newImage = pResults.WebImage
                                        newImage.IsEdit = True
                                        newImage.SaveAsMovieClearArt(Master.currMovie)
                                        Cursor = Cursors.Default
                                        Me.SetMovieListItemAfterEdit(ID, indX)
                                        Me.RefreshMovie(ID, False, False, False, False)
                                    End If
                                End If
                            Else
                                MsgBox(Master.eLang.GetString(1099, "No ClearArt images could be found. Please check to see if any ClearArt scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(1102, "No ClearArts Found"))
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVShows.Item(0, indX).Value)

                            Master.currShow = Master.DB.LoadTVFullShowFromDB(ShowID)

                            If Not String.IsNullOrEmpty(Master.currShow.ShowClearArtPath) Then
                                oldImage.FromFile(Master.currShow.ShowClearArtPath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.ShowClearArt, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVShowClearArt(Master.currShow)
                                Me.SetShowListItemAfterEdit(ShowID, indX)
                                If Me.RefreshShow(ShowID, False, True, False, False) Then
                                    Me.FillList(False, False, True)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)
                            'not supportet
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)
                            'not supportet
                            Me.SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
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
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Not IsNothing(Me.pbFanartCache.Image) Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbFanartCache.Image)
                    End Using
                ElseIf Not IsNothing(Me.pbFanartSmallCache.Image) Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbFanartSmallCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)

                        Dim dlgImgS As dlgImgSelect
                        Dim aList As New List(Of MediaContainers.Image)
                        Dim pResults As New MediaContainers.Image
                        Dim efList As New List(Of String)
                        Dim etList As New List(Of String)
                        Dim newImage As New Images

                        If Not ModulesManager.Instance.ScrapeImage_Movie(Master.currMovie, Enums.ScraperCapabilities.Fanart, aList) Then
                            If aList.Count > 0 Then
                                dlgImgS = New dlgImgSelect()
                                If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.Fanart, aList, efList, etList, True) = DialogResult.OK Then
                                    pResults = dlgImgS.Results
                                    If Not String.IsNullOrEmpty(pResults.URL) Then
                                        Cursor = Cursors.WaitCursor
                                        pResults.WebImage.FromWeb(pResults.URL)
                                        newImage = pResults.WebImage
                                        newImage.IsEdit = True
                                        newImage.SaveAsMovieFanart(Master.currMovie)
                                        Cursor = Cursors.Default
                                        Me.SetMovieListItemAfterEdit(ID, indX)
                                        Me.RefreshMovie(ID, False, False, False, False)
                                    End If
                                End If
                            Else
                                MsgBox(Master.eLang.GetString(969, "No fanart could be found. Please check to see if any fanart scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(970, "No Fanart Found"))
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVShows.Item(0, indX).Value)

                            Master.currShow = Master.DB.LoadTVFullShowFromDB(ShowID)

                            If Not String.IsNullOrEmpty(Master.currShow.ShowFanartPath) Then
                                oldImage.FromFile(Master.currShow.ShowFanartPath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.ShowFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVShowFanart(Master.currShow)
                                Me.SetShowListItemAfterEdit(ShowID, indX)
                                If Me.RefreshShow(ShowID, False, True, False, False) Then
                                    Me.FillList(False, False, True)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(0, indX).Value)
                            Dim Season As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(2, indX).Value)

                            Master.currShow = Master.DB.LoadTVSeasonFromDB(ShowID, Season, True)

                            If Not String.IsNullOrEmpty(Master.currShow.SeasonFanartPath) Then
                                oldImage.FromFile(Master.currShow.SeasonFanartPath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonFanart, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVSeasonFanart(Master.currShow)
                                If Me.RefreshSeason(ShowID, Season, False) Then
                                    Me.FillSeasons(ShowID)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
                            Dim EpisodeID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item(0, indX).Value)

                            Master.currShow = Master.DB.LoadTVEpFromDB(EpisodeID, True)

                            If Not String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then
                                oldImage.FromFile(Master.currShow.EpFanartPath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.EpisodeFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVEpisodeFanart(Master.currShow)
                                If Me.RefreshEpisode(EpisodeID) Then
                                    Me.FillEpisodes(Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVEp.Season)
                                End If
                            End If
                            Me.SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbPoster_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPoster.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Not IsNothing(Me.pbPosterCache.Image) Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbPosterCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)

                        Dim dlgImgS As dlgImgSelect
                        Dim aList As New List(Of MediaContainers.Image)
                        Dim pResults As New MediaContainers.Image
                        Dim efList As New List(Of String)
                        Dim etList As New List(Of String)
                        Dim newImage As New Images

                        If Not ModulesManager.Instance.ScrapeImage_Movie(Master.currMovie, Enums.ScraperCapabilities.Poster, aList) Then
                            If aList.Count > 0 Then
                                dlgImgS = New dlgImgSelect()
                                If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.Poster, aList, efList, etList, True) = DialogResult.OK Then
                                    pResults = dlgImgS.Results
                                    If Not String.IsNullOrEmpty(pResults.URL) Then
                                        Cursor = Cursors.WaitCursor
                                        pResults.WebImage.FromWeb(pResults.URL)
                                        newImage = pResults.WebImage
                                        newImage.IsEdit = True
                                        newImage.SaveAsMoviePoster(Master.currMovie)
                                        Cursor = Cursors.Default
                                        Me.SetMovieListItemAfterEdit(ID, indX)
                                        Me.RefreshMovie(ID, False, False, False, False)
                                    End If
                                End If
                            Else
                                MsgBox(Master.eLang.GetString(971, "No poster could be found. Please check to see if any poster scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(972, "No Poster Found"))
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVShows.Item(0, indX).Value)

                            Master.currShow = Master.DB.LoadTVFullShowFromDB(ShowID)

                            If Not String.IsNullOrEmpty(Master.currShow.ShowPosterPath) Then
                                oldImage.FromFile(Master.currShow.ShowPosterPath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.ShowPoster, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVShowPoster(Master.currShow)
                                Me.SetShowListItemAfterEdit(ShowID, indX)
                                If Me.RefreshShow(ShowID, False, True, False, False) Then
                                    Me.FillList(False, False, True)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(0, indX).Value)
                            Dim Season As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(2, indX).Value)

                            Master.currShow = Master.DB.LoadTVSeasonFromDB(ShowID, Season, True)

                            If Not String.IsNullOrEmpty(Master.currShow.SeasonPosterPath) Then
                                oldImage.FromFile(Master.currShow.SeasonPosterPath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonPoster, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVSeasonPoster(Master.currShow)
                                If Me.RefreshSeason(ShowID, Season, False) Then
                                    Me.FillSeasons(ShowID)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
                            Dim EpisodeID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item(0, indX).Value)

                            Master.currShow = Master.DB.LoadTVEpFromDB(EpisodeID, True)

                            If Not String.IsNullOrEmpty(Master.currShow.EpPosterPath) Then
                                oldImage.FromFile(Master.currShow.EpPosterPath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.EpisodePoster, Master.currShow.TVEp.Season, Master.currShow.TVEp.Episode, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVEpisodePoster(Master.currShow)
                                If Me.RefreshEpisode(EpisodeID) Then
                                    Me.FillEpisodes(Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVEp.Season)
                                End If
                            End If
                            Me.SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbLandscape_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbLandscape.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Not IsNothing(Me.pbLandscapeCache.Image) Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbLandscapeCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item(0, indX).Value)

                        Dim dlgImgS As dlgImgSelect
                        Dim aList As New List(Of MediaContainers.Image)
                        Dim pResults As New MediaContainers.Image
                        Dim efList As New List(Of String)
                        Dim etList As New List(Of String)
                        Dim newImage As New Images

                        If Not ModulesManager.Instance.ScrapeImage_Movie(Master.currMovie, Enums.ScraperCapabilities.Landscape, aList) Then
                            If aList.Count > 0 Then
                                dlgImgS = New dlgImgSelect()
                                If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.Landscape, aList, efList, etList, True) = DialogResult.OK Then
                                    pResults = dlgImgS.Results
                                    If Not String.IsNullOrEmpty(pResults.URL) Then
                                        Cursor = Cursors.WaitCursor
                                        pResults.WebImage.FromWeb(pResults.URL)
                                        newImage = pResults.WebImage
                                        newImage.IsEdit = True
                                        newImage.SaveAsMovieLandscape(Master.currMovie)
                                        Cursor = Cursors.Default
                                        Me.SetMovieListItemAfterEdit(ID, indX)
                                        Me.RefreshMovie(ID, False, False, False, False)
                                    End If
                                End If
                            Else
                                MsgBox(Master.eLang.GetString(1058, "No Landscape images could be found. Please check to see if any Landscape scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(1197, "No Landscape Found"))
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVShows.Item(0, indX).Value)

                            Master.currShow = Master.DB.LoadTVFullShowFromDB(ShowID)

                            If Not String.IsNullOrEmpty(Master.currShow.ShowLandscapePath) Then
                                oldImage.FromFile(Master.currShow.ShowLandscapePath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.ShowLandscape, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVShowLandscape(Master.currShow)
                                Me.SetShowListItemAfterEdit(ShowID, indX)
                                If Me.RefreshShow(ShowID, False, True, False, False) Then
                                    Me.FillList(False, False, True)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim newImage As New Images
                            Dim oldImage As New Images
                            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                            Dim ShowID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(0, indX).Value)
                            Dim Season As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item(2, indX).Value)

                            Master.currShow = Master.DB.LoadTVSeasonFromDB(ShowID, Season, True)

                            If Not String.IsNullOrEmpty(Master.currShow.SeasonLandscapePath) Then
                                oldImage.FromFile(Master.currShow.SeasonLandscapePath)
                            End If

                            Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonLandscape, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, oldImage)

                            If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
                                newImage = tImage
                                newImage.IsEdit = True
                                newImage.SaveAsTVSeasonLandscape(Master.currShow)
                                If Me.RefreshSeason(ShowID, Season, False) Then
                                    Me.FillSeasons(ShowID)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)
                            'not supportet
                            Me.SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbAllSeason_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbAllSeason.DoubleClick
        Try
            If Not IsNothing(Me.pbAllSeason.Image) Then
                Using dImgView As New dlgImgView
                    dImgView.ShowDialog(Me.pbAllSeasonCache.Image)
                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub rbFilterAnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterAnd.Click
        If clbFilterGenres.CheckedItems.Count > 0 Then
            Me.txtFilterGenre.Text = String.Empty
            Me.FilterArray.Remove(Me.filGenre)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterGenre.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            For i As Integer = 0 To alGenres.Count - 1
                alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
            Next

            Me.filGenre = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            Me.FilterArray.Add(Me.filGenre)
        End If

        If (Not String.IsNullOrEmpty(Me.cbFilterYear.Text) AndAlso Not Me.cbFilterYear.Text = Master.eLang.All) OrElse Me.clbFilterGenres.CheckedItems.Count > 0 OrElse _
        Me.chkFilterMark.Checked OrElse Me.chkFilterNew.Checked OrElse Me.chkFilterLock.Checked OrElse Not Me.clbFilterSource.CheckedItems.Count > 0 OrElse _
        Me.chkFilterDupe.Checked OrElse Me.chkFilterMissing.Checked OrElse Me.chkFilterTolerance.Checked OrElse Not Me.cbFilterFileSource.Text = Master.eLang.All Then Me.RunFilter()
    End Sub

    Private Sub rbFilterOr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterOr.Click
        If clbFilterGenres.CheckedItems.Count > 0 Then
            Me.txtFilterGenre.Text = String.Empty
            Me.FilterArray.Remove(Me.filGenre)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterGenre.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            For i As Integer = 0 To alGenres.Count - 1
                alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
            Next

            Me.filGenre = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            Me.FilterArray.Add(Me.filGenre)
        End If

        If (Not String.IsNullOrEmpty(Me.cbFilterYear.Text) AndAlso Not Me.cbFilterYear.Text = Master.eLang.All) OrElse Me.clbFilterGenres.CheckedItems.Count > 0 OrElse _
        Me.chkFilterMark.Checked OrElse Me.chkFilterNew.Checked OrElse Me.chkFilterLock.Checked OrElse Not Me.clbFilterSource.CheckedItems.Count > 0 OrElse _
        Me.chkFilterDupe.Checked OrElse Me.chkFilterMissing.Checked OrElse Me.chkFilterTolerance.Checked OrElse Not Me.cbFilterFileSource.Text = Master.eLang.All Then Me.RunFilter()
    End Sub

    Private Sub RefreshAllMovies()
        If Me.dtMovies.Rows.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.EnableFilters(False)

            Me.tspbLoading.Maximum = Me.dtMovies.Rows.Count + 1
            Me.tspbLoading.Value = 0
            Me.tslLoading.Text = Master.eLang.GetString(110, "Refreshing Media:")
            Me.tspbLoading.Visible = True
            Me.tslLoading.Visible = True

            Me.bwRefreshMovies.WorkerReportsProgress = True
            Me.bwRefreshMovies.WorkerSupportsCancellation = True
            Me.bwRefreshMovies.RunWorkerAsync()
        Else
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub RefreshAllMovieSets()
        If Me.dtMovieSets.Rows.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.EnableFilters(False)

            Me.tspbLoading.Maximum = Me.dtMovieSets.Rows.Count + 1
            Me.tspbLoading.Value = 0
            Me.tslLoading.Text = Master.eLang.GetString(110, "Refreshing Media:")
            Me.tspbLoading.Visible = True
            Me.tslLoading.Visible = True

            Me.bwRefreshMovieSets.WorkerReportsProgress = True
            Me.bwRefreshMovieSets.WorkerSupportsCancellation = True
            Me.bwRefreshMovieSets.RunWorkerAsync()
        Else
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub mnuMainToolsReloadMovies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadMovies.Click, cmnuTrayToolsReloadMovies.Click
        RefreshAllMovies()
    End Sub

    Private Sub mnuMainToolsReloadMovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadMovieSets.Click
        RefreshAllMovieSets()
    End Sub

    Private Function RefreshEpisode(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False, Optional ByVal FromNfo As Boolean = True, Optional ByVal ToNfo As Boolean = False) As Boolean
        Dim tmpShowDb As New Structures.DBTV
        Dim tmpEp As New MediaContainers.EpisodeDetails
        Dim SeasonChanged As Boolean = False
        Dim EpisodeChanged As Boolean = False
        Dim ShowID As Integer = -1

        Dim hasFanart As Boolean = False
        Dim hasPoster As Boolean = False
        Dim hasWatched As Boolean = False

        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)

        Try

            tmpShowDb = Master.DB.LoadTVEpFromDB(ID, True)

            If Directory.Exists(tmpShowDb.ShowPath) Then

                If FromNfo Then
                    If String.IsNullOrEmpty(tmpShowDb.EpNfoPath) Then
                        Dim sNFO As String = NFO.GetEpNfoPath(tmpShowDb.Filename)
                        tmpShowDb.EpNfoPath = sNFO
                        tmpEp = NFO.LoadTVEpFromNFO(sNFO, tmpShowDb.TVEp.Season, tmpShowDb.TVEp.Episode)
                    Else
                        tmpEp = NFO.LoadTVEpFromNFO(tmpShowDb.EpNfoPath, tmpShowDb.TVEp.Season, tmpShowDb.TVEp.Episode)
                    End If

                    If Not tmpEp.Episode = -999 Then
                        tmpShowDb.TVEp = tmpEp
                    End If
                End If

                If String.IsNullOrEmpty(tmpShowDb.TVEp.Title) Then
                    tmpShowDb.TVEp.Title = StringUtils.FilterTVEpName(Path.GetFileNameWithoutExtension(tmpShowDb.Filename), tmpShowDb.TVShow.Title, False)
                End If

                Dim eContainer As New Scanner.EpisodeContainer With {.Filename = tmpShowDb.Filename}
                fScanner.GetTVEpisodeFolderContents(eContainer)
                tmpShowDb.EpPosterPath = eContainer.Poster
                tmpShowDb.EpFanartPath = eContainer.Fanart
                'assume invalid nfo if no title
                tmpShowDb.EpNfoPath = If(String.IsNullOrEmpty(tmpShowDb.TVEp.Title), String.Empty, eContainer.Nfo)

                hasFanart = Not String.IsNullOrEmpty(eContainer.Fanart)
                hasPoster = Not String.IsNullOrEmpty(eContainer.Poster)
                hasWatched = Not String.IsNullOrEmpty(tmpShowDb.TVEp.Playcount) AndAlso Not tmpShowDb.TVEp.Playcount = "0"

                Dim dRow = From drvRow In dtEpisodes.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item(0)) = ID Select drvRow

                If Not IsNothing(dRow(0)) Then
                    tmpShowDb.IsMarkEp = Convert.ToBoolean(DirectCast(dRow(0), DataRow).Item(8))
                    tmpShowDb.IsLockEp = Convert.ToBoolean(DirectCast(dRow(0), DataRow).Item(11))

                    If Not Convert.ToInt32(DirectCast(dRow(0), DataRow).Item(12)) = tmpShowDb.TVEp.Season Then
                        SeasonChanged = True
                        ShowID = Convert.ToInt32(tmpShowDb.ShowID)
                    End If

                    If Not Convert.ToInt32(DirectCast(dRow(0), DataRow).Item(2)) = tmpShowDb.TVEp.Episode Then
                        EpisodeChanged = True
                        ShowID = Convert.ToInt32(tmpShowDb.ShowID)
                    End If

                    If Me.InvokeRequired Then
                        Me.Invoke(myDelegate, New Object() {dRow(0), 3, tmpShowDb.TVEp.Title})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 4, hasPoster})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 5, hasFanart})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 6, If(String.IsNullOrEmpty(tmpShowDb.EpNfoPath), False, True)})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 7, False})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 24, hasWatched})
                    Else
                        DirectCast(dRow(0), DataRow).Item(3) = tmpShowDb.TVEp.Title
                        DirectCast(dRow(0), DataRow).Item(4) = hasPoster
                        DirectCast(dRow(0), DataRow).Item(5) = hasFanart
                        DirectCast(dRow(0), DataRow).Item(6) = If(String.IsNullOrEmpty(tmpShowDb.EpNfoPath), False, True)
                        DirectCast(dRow(0), DataRow).Item(7) = False
                        DirectCast(dRow(0), DataRow).Item(24) = hasWatched
                    End If
                End If

                Master.DB.SaveTVEpToDB(tmpShowDb, False, False, BatchMode, ToNfo)

            Else
                Master.DB.DeleteTVEpFromDB(ID, False, True, BatchMode)
                Return True
            End If

            If Not BatchMode Then
                If (SeasonChanged OrElse EpisodeChanged) AndAlso ShowID > -1 Then
                    Master.DB.CleanSeasons(BatchMode)
                    Me.FillSeasons(ShowID)
                Else
                    Me.LoadEpisodeInfo(Convert.ToInt32(ID))
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return False
    End Function

    Private Function RefreshMovie(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False, Optional ByVal FromNfo As Boolean = True, Optional ByVal ToNfo As Boolean = False, Optional ByVal delWatched As Boolean = False) As Boolean
        Dim tmpMovie As New MediaContainers.Movie
        Dim tmpMovieDb As New Structures.DBMovie
        Dim OldTitle As String = String.Empty
        Dim selRow As DataRow = Nothing

        Dim hasBanner As Boolean = False
        Dim hasClearArt As Boolean = False
        Dim hasClearLogo As Boolean = False
        Dim hasDiscArt As Boolean = False
        Dim hasEFanarts As Boolean = False
        Dim hasEThumbs As Boolean = False
        Dim hasFanart As Boolean = False
        Dim hasLandscape As Boolean = False
        Dim hasNfo As Boolean = False
        Dim hasPoster As Boolean = False
        Dim hasTheme As Boolean = False
        Dim hasTrailer As Boolean = False
        Dim hasSub As Boolean = False
        Dim hasWatched As Boolean = False

        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)

        Try

            tmpMovieDb = Master.DB.LoadMovieFromDB(ID)

            OldTitle = tmpMovieDb.Movie.Title

            If Directory.Exists(Directory.GetParent(tmpMovieDb.Filename).FullName) Then

                If FromNfo Then
                    If String.IsNullOrEmpty(tmpMovieDb.NfoPath) Then
                        Dim sNFO As String = NFO.GetNfoPath(tmpMovieDb.Filename, tmpMovieDb.IsSingle)
                        tmpMovieDb.NfoPath = sNFO
                        tmpMovie = NFO.LoadMovieFromNFO(sNFO, tmpMovieDb.IsSingle)
                    Else
                        tmpMovie = NFO.LoadMovieFromNFO(tmpMovieDb.NfoPath, tmpMovieDb.IsSingle)
                    End If
                    'subsType and subsPath not in NFO , try to load it from DB
                    For x = 0 To tmpMovie.FileInfo.StreamDetails.Subtitle.Count - 1
                        If tmpMovieDb.Movie.FileInfo.StreamDetails.Subtitle.Count > 0 AndAlso Not tmpMovieDb.Movie.FileInfo.StreamDetails.Subtitle(x) Is Nothing AndAlso tmpMovieDb.Movie.FileInfo.StreamDetails.Subtitle(x).Language = tmpMovie.FileInfo.StreamDetails.Subtitle(x).Language Then
                            tmpMovie.FileInfo.StreamDetails.Subtitle(x).SubsType = tmpMovieDb.Movie.FileInfo.StreamDetails.Subtitle(x).SubsType
                            tmpMovie.FileInfo.StreamDetails.Subtitle(x).SubsPath = tmpMovieDb.Movie.FileInfo.StreamDetails.Subtitle(x).SubsPath
                        End If
                    Next
                    tmpMovieDb.Movie = tmpMovie
                End If

                If String.IsNullOrEmpty(tmpMovieDb.Movie.Title) Then
                    If FileUtils.Common.isVideoTS(tmpMovieDb.Filename) Then
                        tmpMovieDb.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(tmpMovieDb.Filename).FullName).Name)
                        tmpMovieDb.Movie.Title = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(tmpMovieDb.Filename).FullName).Name, False)
                    ElseIf FileUtils.Common.isBDRip(tmpMovieDb.Filename) Then
                        tmpMovieDb.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(tmpMovieDb.Filename).FullName).FullName).Name)
                        tmpMovieDb.Movie.Title = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(tmpMovieDb.Filename).FullName).FullName).Name, False)
                    Else
                        If tmpMovieDb.UseFolder AndAlso tmpMovieDb.IsSingle Then
                            tmpMovieDb.ListTitle = StringUtils.FilterName(Directory.GetParent(tmpMovieDb.Filename).Name)
                            tmpMovieDb.Movie.Title = StringUtils.FilterName(Directory.GetParent(tmpMovieDb.Filename).Name, False)
                        Else
                            tmpMovieDb.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(tmpMovieDb.Filename))
                            tmpMovieDb.Movie.Title = StringUtils.FilterName(Path.GetFileNameWithoutExtension(tmpMovieDb.Filename), False)
                        End If
                    End If
                    If Not OldTitle = tmpMovieDb.Movie.Title OrElse String.IsNullOrEmpty(tmpMovieDb.Movie.SortTitle) Then tmpMovieDb.Movie.SortTitle = tmpMovieDb.ListTitle
                Else
                    Dim tTitle As String = StringUtils.FilterTokens_Movie(tmpMovieDb.Movie.Title)
                    If Not OldTitle = tmpMovieDb.Movie.Title OrElse String.IsNullOrEmpty(tmpMovieDb.Movie.SortTitle) Then tmpMovieDb.Movie.SortTitle = tTitle
                    If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(tmpMovieDb.Movie.Year) Then
                        tmpMovieDb.ListTitle = String.Format("{0} ({1})", tTitle, tmpMovieDb.Movie.Year)
                    Else
                        tmpMovieDb.ListTitle = tTitle
                    End If
                End If
                Dim fromFile As String = APIXML.GetFileSource(tmpMovieDb.Filename)
                If Not String.IsNullOrEmpty(fromFile) Then
                    tmpMovieDb.FileSource = fromFile
                ElseIf String.IsNullOrEmpty(tmpMovieDb.FileSource) AndAlso clsAdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                    tmpMovieDb.FileSource = clsAdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(tmpMovieDb.Filename)), String.Empty, "*EmberAPP")
                End If

                If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                    For Each a In FileUtils.GetFilenameList.Movie(tmpMovieDb.Filename, tmpMovieDb.IsSingle, Enums.ModType_Movie.WatchedFile)
                        If delWatched Then
                            If File.Exists(a) Then
                                File.Delete(a)
                            End If
                        End If
                        If Not String.IsNullOrEmpty(tmpMovieDb.Movie.PlayCount) AndAlso Not tmpMovieDb.Movie.PlayCount = "0" Then
                            If Not File.Exists(a) Then
                                Dim fs As FileStream = File.Create(a)
                                fs.Close()
                            End If
                        Else
                            If File.Exists(a) Then
                                tmpMovieDb.Movie.PlayCount = "1"
                                If Not tmpMovieDb.NfoPath = tmpMovieDb.Filename AndAlso Not String.IsNullOrEmpty(tmpMovieDb.NfoPath) Then
                                    ToNfo = True
                                End If
                            End If
                        End If
                    Next
                End If

                Dim mContainer As New Scanner.MovieContainer With {.Filename = tmpMovieDb.Filename, .isSingle = tmpMovieDb.IsSingle}
                fScanner.GetMovieFolderContents(mContainer)
                tmpMovieDb.BannerPath = mContainer.Banner
                tmpMovieDb.ClearArtPath = mContainer.ClearArt
                tmpMovieDb.ClearLogoPath = mContainer.ClearLogo
                tmpMovieDb.DiscArtPath = mContainer.DiscArt
                tmpMovieDb.EFanartsPath = mContainer.EFanarts
                tmpMovieDb.EThumbsPath = mContainer.EThumbs
                tmpMovieDb.FanartPath = mContainer.Fanart
                tmpMovieDb.LandscapePath = mContainer.Landscape
                tmpMovieDb.NfoPath = If(String.IsNullOrEmpty(tmpMovieDb.Movie.Title), String.Empty, mContainer.Nfo) 'assume invalid nfo if no title
                tmpMovieDb.PosterPath = mContainer.Poster
                tmpMovieDb.SubPath = mContainer.Subs
                tmpMovieDb.ThemePath = mContainer.Theme
                tmpMovieDb.TrailerPath = mContainer.Trailer

                hasBanner = Not String.IsNullOrEmpty(mContainer.Banner)
                hasClearArt = Not String.IsNullOrEmpty(mContainer.ClearArt)
                hasClearLogo = Not String.IsNullOrEmpty(mContainer.ClearLogo)
                hasDiscArt = Not String.IsNullOrEmpty(mContainer.DiscArt)
                hasEFanarts = Not String.IsNullOrEmpty(mContainer.EFanarts)
                hasEThumbs = Not String.IsNullOrEmpty(mContainer.EThumbs)
                hasFanart = Not String.IsNullOrEmpty(mContainer.Fanart)
                hasLandscape = Not String.IsNullOrEmpty(mContainer.Landscape)
                hasNfo = Not String.IsNullOrEmpty(tmpMovieDb.NfoPath)
                hasPoster = Not String.IsNullOrEmpty(mContainer.Poster)
                hasSub = Not String.IsNullOrEmpty(mContainer.Subs)
                hasTheme = Not String.IsNullOrEmpty(mContainer.Theme)
                hasTrailer = Not String.IsNullOrEmpty(mContainer.Trailer)
                hasWatched = Not String.IsNullOrEmpty(tmpMovieDb.Movie.PlayCount) AndAlso Not tmpMovieDb.Movie.PlayCount = "0"

                Dim dRow = From drvRow In dtMovies.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item(0)) = ID Select drvRow

                If Not IsNothing(dRow(0)) Then
                    selRow = DirectCast(dRow(0), DataRow)
                    tmpMovieDb.IsMark = Convert.ToBoolean(selRow.Item(11))
                    tmpMovieDb.IsLock = Convert.ToBoolean(selRow.Item(14))

                    If Me.InvokeRequired Then
                        Me.Invoke(myDelegate, New Object() {dRow(0), 1, tmpMovieDb.Filename})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 3, tmpMovieDb.ListTitle})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 4, hasPoster})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 5, hasFanart})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 6, hasNfo})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 7, hasTrailer})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 8, hasSub})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 9, hasEThumbs})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 10, False})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 15, tmpMovieDb.Movie.Title})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 47, tmpMovieDb.Movie.SortTitle})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 27, tmpMovieDb.Movie.Genre})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 34, hasWatched})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 49, hasEFanarts})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 51, hasBanner})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 53, hasLandscape})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 55, hasTheme})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 57, hasDiscArt})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 59, hasClearLogo})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 61, hasClearArt})
                    Else
                        selRow.Item(1) = tmpMovieDb.Filename
                        selRow.Item(3) = tmpMovieDb.ListTitle
                        selRow.Item(4) = hasPoster
                        selRow.Item(5) = hasFanart
                        selRow.Item(6) = hasNfo
                        selRow.Item(7) = hasTrailer
                        selRow.Item(8) = hasSub
                        selRow.Item(9) = hasEThumbs
                        selRow.Item(10) = False
                        selRow.Item(15) = tmpMovieDb.Movie.Title
                        selRow.Item(47) = tmpMovieDb.Movie.SortTitle
                        selRow.Item(27) = tmpMovieDb.Movie.Genre
                        selRow.Item(34) = hasWatched
                        selRow.Item(49) = hasEFanarts
                        selRow.Item(51) = hasBanner
                        selRow.Item(53) = hasLandscape
                        selRow.Item(55) = hasTheme
                        selRow.Item(57) = hasDiscArt
                        selRow.Item(59) = hasClearLogo
                        selRow.Item(61) = hasClearArt
                    End If
                End If

                Master.DB.SaveMovieToDB(tmpMovieDb, False, BatchMode, ToNfo)

            Else
                Master.DB.DeleteMovieFromDB(ID, BatchMode)
                Return True
            End If

            If Not BatchMode Then
                Me.DoTitleCheck()

                Dim selI As Integer = 0

                If Me.dgvMovies.SelectedRows.Count > 0 Then selI = Me.dgvMovies.SelectedRows(0).Index

                Me.dgvMovies.ClearSelection()
                Me.dgvMovies.CurrentCell = Nothing

                If Me.dgvMovies.RowCount - 1 < selI Then selI = Me.dgvMovies.RowCount

                Me.ClearInfo()
                Me.prevMovieRow = -2
                Me.currMovieRow = -1

                If Me.dgvMovies.RowCount > 0 Then
                    Me.dgvMovies.Rows(selI).Cells(3).Selected = True
                    Me.dgvMovies.CurrentCell = Me.dgvMovies.Rows(selI).Cells(3)
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return False
    End Function

    Private Function RefreshMovieSet(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False, Optional ByVal FromNfo As Boolean = True, Optional ByVal ToNfo As Boolean = False, Optional ByVal delWatched As Boolean = False) As Boolean
        'Dim tmpMovieSet As New MediaContainers.Movie
        Dim tmpMovieSetDb As New Structures.DBMovieSet
        Dim OldTitle As String = String.Empty
        Dim selRow As DataRow = Nothing

        Dim hasBanner As Boolean = False
        Dim hasClearArt As Boolean = False
        Dim hasClearLogo As Boolean = False
        Dim hasDiscArt As Boolean = False
        Dim hasFanart As Boolean = False
        Dim hasLandscape As Boolean = False
        Dim hasNfo As Boolean = False
        Dim hasPoster As Boolean = False

        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)

        Try

            tmpMovieSetDb = Master.DB.LoadMovieSetFromDB(ID)

            OldTitle = tmpMovieSetDb.MovieSet.Title

            Dim tTitle As String = StringUtils.FilterTokens_MovieSet(tmpMovieSetDb.MovieSet.Title)
            If Not String.IsNullOrEmpty(tTitle) Then
                tmpMovieSetDb.ListTitle = tTitle
            Else
                tmpMovieSetDb.ListTitle = OldTitle
            End If

            Dim mContainer As New Scanner.MovieSetContainer With {.SetName = tmpMovieSetDb.MovieSet.Title}
            fScanner.GetMovieSetFolderContents(mContainer)
            tmpMovieSetDb.BannerPath = mContainer.Banner
            tmpMovieSetDb.ClearArtPath = mContainer.ClearArt
            tmpMovieSetDb.ClearLogoPath = mContainer.ClearLogo
            tmpMovieSetDb.DiscArtPath = mContainer.DiscArt
            tmpMovieSetDb.FanartPath = mContainer.Fanart
            tmpMovieSetDb.LandscapePath = mContainer.Landscape
            tmpMovieSetDb.NfoPath = mContainer.Nfo
            tmpMovieSetDb.PosterPath = mContainer.Poster

            hasBanner = Not String.IsNullOrEmpty(mContainer.Banner)
            hasClearArt = Not String.IsNullOrEmpty(mContainer.ClearArt)
            hasClearLogo = Not String.IsNullOrEmpty(mContainer.ClearLogo)
            hasDiscArt = Not String.IsNullOrEmpty(mContainer.DiscArt)
            hasFanart = Not String.IsNullOrEmpty(mContainer.Fanart)
            hasLandscape = Not String.IsNullOrEmpty(mContainer.Landscape)
            hasNfo = Not String.IsNullOrEmpty(mContainer.Nfo)
            hasPoster = Not String.IsNullOrEmpty(mContainer.Poster)

            Dim dRow = From drvRow In dtMovieSets.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item(0)) = ID Select drvRow

            If Not IsNothing(dRow(0)) Then
                selRow = DirectCast(dRow(0), DataRow)

                If Me.InvokeRequired Then
                    Me.Invoke(myDelegate, New Object() {dRow(0), 1, tmpMovieSetDb.ListTitle})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 2, hasNfo})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 4, hasPoster})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 6, hasFanart})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 8, hasBanner})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 10, hasLandscape})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 12, hasDiscArt})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 14, hasClearLogo})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 16, hasClearArt})
                Else
                    selRow.Item(1) = tmpMovieSetDb.ListTitle
                    selRow.Item(2) = hasNfo
                    selRow.Item(4) = hasPoster
                    selRow.Item(6) = hasFanart
                    selRow.Item(8) = hasBanner
                    selRow.Item(10) = hasLandscape
                    selRow.Item(12) = hasDiscArt
                    selRow.Item(14) = hasClearLogo
                    selRow.Item(16) = hasClearArt
                End If
            End If

            Master.DB.SaveMovieSetToDB(tmpMovieSetDb, False, BatchMode, ToNfo)

            If Not BatchMode Then

                Dim selI As Integer = 0

                If Me.dgvMovieSets.SelectedRows.Count > 0 Then selI = Me.dgvMovieSets.SelectedRows(0).Index

                Me.dgvMovieSets.ClearSelection()
                Me.dgvMovieSets.CurrentCell = Nothing

                If Me.dgvMovieSets.RowCount - 1 < selI Then selI = Me.dgvMovieSets.RowCount

                Me.ClearInfo()
                Me.prevMovieSetRow = -2
                Me.currMovieSetRow = -1

                If Me.dgvMovieSets.RowCount > 0 Then
                    Me.dgvMovieSets.Rows(selI).Cells(0).Selected = True
                    Me.dgvMovieSets.CurrentCell = Me.dgvMovieSets.Rows(selI).Cells(1)
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return False
    End Function

    Private Function RefreshSeason(ByVal ShowID As Integer, ByVal Season As Integer, ByVal BatchMode As Boolean) As Boolean
        Dim tmpSeasonDb As New Structures.DBTV
        Dim tmpShow As New MediaContainers.TVShow

        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)

        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()

            tmpSeasonDb = Master.DB.LoadTVSeasonFromDB(ShowID, Season, True)

            Dim tPath As String = Functions.GetSeasonDirectoryFromShowPath(tmpSeasonDb.ShowPath, Season)

            If String.IsNullOrEmpty(tPath) Then
                tPath = tmpSeasonDb.ShowPath
            End If

            'fake file just for getting images
            tmpSeasonDb.Filename = Path.Combine(tPath, "file.ext")
            fScanner.GetTVSeasonImages(tmpSeasonDb, Season)

            Dim dRow = From drvRow In dtSeasons.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item(0)) = ShowID AndAlso Convert.ToInt32(DirectCast(drvRow, DataRow).Item(2)) = Season Select drvRow

            If Not IsNothing(dRow(0)) Then
                If Me.InvokeRequired Then
                    Me.Invoke(myDelegate, New Object() {dRow(0), 3, If(String.IsNullOrEmpty(tmpSeasonDb.SeasonPosterPath), False, True)})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 4, If(String.IsNullOrEmpty(tmpSeasonDb.SeasonFanartPath), False, True)})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 9, False})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 10, If(String.IsNullOrEmpty(tmpSeasonDb.SeasonBannerPath), False, True)})
                    Me.Invoke(myDelegate, New Object() {dRow(0), 12, If(String.IsNullOrEmpty(tmpSeasonDb.SeasonLandscapePath), False, True)})
                Else
                    DirectCast(dRow(0), DataRow).Item(3) = If(String.IsNullOrEmpty(tmpSeasonDb.SeasonPosterPath), False, True)
                    DirectCast(dRow(0), DataRow).Item(4) = If(String.IsNullOrEmpty(tmpSeasonDb.SeasonFanartPath), False, True)
                    DirectCast(dRow(0), DataRow).Item(9) = False
                    DirectCast(dRow(0), DataRow).Item(10) = If(String.IsNullOrEmpty(tmpSeasonDb.SeasonBannerPath), False, True)
                    DirectCast(dRow(0), DataRow).Item(12) = If(String.IsNullOrEmpty(tmpSeasonDb.SeasonLandscapePath), False, True)
                End If

                Master.DB.SaveTVSeasonToDB(tmpSeasonDb, False, False)

            End If

            If Not BatchMode Then
                SQLtransaction.Commit()
                SQLtransaction = Nothing

                Me.LoadSeasonInfo(ShowID, Season)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return False
    End Function

    Private Function RefreshShow(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal FromNfo As Boolean, ByVal ToNfo As Boolean, ByVal WithEpisodes As Boolean) As Boolean
        If Not BatchMode Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Value = 0

            Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLCommand.CommandText = String.Concat("SELECT COUNT(ID) AS COUNT FROM TVEps WHERE TVShowID = ", ID, " AND Missing = 0;")
                Me.tspbLoading.Maximum = Convert.ToInt32(SQLCommand.ExecuteScalar) + 1
            End Using

            Me.tslLoading.Text = Master.eLang.GetString(731, "Refreshing Show:")
            Me.tslLoading.Visible = True
            Me.tspbLoading.Visible = True
            Application.DoEvents()
        End If

        Dim tmpShowDb As New Structures.DBTV
        Dim tmpShow As New MediaContainers.TVShow

        Dim hasASBanner As Boolean = False
        Dim hasASFanart As Boolean = False
        Dim hasASLandscape As Boolean = False
        Dim hasASPoster As Boolean = False
        Dim hasBanner As Boolean = False
        Dim hasCharacterArt As Boolean = False
        Dim hasClearArt As Boolean = False
        Dim hasClearLogo As Boolean = False
        Dim hasEFanarts As Boolean = False
        Dim hasFanart As Boolean = False
        Dim hasLandscape As Boolean = False
        Dim hasPoster As Boolean = False
        Dim hasTheme As Boolean = False

        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)

        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()

            tmpShowDb = Master.DB.LoadTVFullShowFromDB(ID)

            If Directory.Exists(tmpShowDb.ShowPath) Then

                If FromNfo Then
                    If String.IsNullOrEmpty(tmpShowDb.ShowNfoPath) Then
                        Dim sNFO As String = NFO.GetShowNfoPath(tmpShowDb.ShowPath)
                        tmpShowDb.ShowNfoPath = sNFO
                        tmpShow = NFO.LoadTVShowFromNFO(sNFO)
                    Else
                        tmpShow = NFO.LoadTVShowFromNFO(tmpShowDb.ShowNfoPath)
                    End If
                    tmpShowDb.TVShow = tmpShow
                End If

                If String.IsNullOrEmpty(tmpShowDb.TVShow.Title) Then
                    tmpShowDb.TVShow.Title = StringUtils.FilterTVShowName(Path.GetFileNameWithoutExtension(tmpShowDb.ShowPath), False)
                End If

                Dim sContainer As New Scanner.TVShowContainer With {.ShowPath = tmpShowDb.ShowPath}
                fScanner.GetTVShowFolderContents(sContainer, ID)
                tmpShowDb.ShowBannerPath = sContainer.ShowBanner
                tmpShowDb.ShowCharacterArtPath = sContainer.ShowCharacterArt
                tmpShowDb.ShowClearArtPath = sContainer.ShowClearArt
                tmpShowDb.ShowClearLogoPath = sContainer.ShowClearLogo
                tmpShowDb.ShowEFanartsPath = sContainer.ShowEFanarts
                tmpShowDb.ShowFanartPath = sContainer.ShowFanart
                tmpShowDb.ShowLandscapePath = sContainer.ShowLandscape
                tmpShowDb.ShowPosterPath = sContainer.ShowPoster
                tmpShowDb.ShowThemePath = sContainer.ShowTheme
                'assume invalid nfo if no title
                tmpShowDb.ShowNfoPath = If(String.IsNullOrEmpty(tmpShowDb.TVShow.Title), String.Empty, sContainer.ShowNfo)

                hasBanner = Not String.IsNullOrEmpty(sContainer.ShowBanner)
                hasCharacterArt = Not String.IsNullOrEmpty(sContainer.ShowCharacterArt)
                hasClearArt = Not String.IsNullOrEmpty(sContainer.ShowClearArt)
                hasClearLogo = Not String.IsNullOrEmpty(sContainer.ShowClearLogo)
                hasEFanarts = Not String.IsNullOrEmpty(sContainer.ShowEFanarts)
                hasFanart = Not String.IsNullOrEmpty(sContainer.ShowFanart)
                hasLandscape = Not String.IsNullOrEmpty(sContainer.ShowLandscape)
                hasPoster = Not String.IsNullOrEmpty(sContainer.ShowPoster)
                hasTheme = Not String.IsNullOrEmpty(sContainer.ShowTheme)

                Dim dRow = From drvRow In dtShows.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item(0)) = ID Select drvRow

                If Not IsNothing(dRow(0)) Then
                    tmpShowDb.IsMarkShow = Convert.ToBoolean(DirectCast(dRow(0), DataRow).Item(6))
                    tmpShowDb.IsLockShow = Convert.ToBoolean(DirectCast(dRow(0), DataRow).Item(10))

                    If Me.InvokeRequired Then
                        Me.Invoke(myDelegate, New Object() {dRow(0), 1, tmpShowDb.TVShow.Title})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 2, hasPoster})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 3, hasFanart})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 4, If(String.IsNullOrEmpty(tmpShowDb.ShowNfoPath), False, True)})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 5, False})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 22, tmpShowDb.ShowLanguage})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 24, hasBanner})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 26, hasLandscape})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 29, hasTheme})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 31, hasCharacterArt})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 33, hasClearLogo})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 35, hasClearArt})
                        Me.Invoke(myDelegate, New Object() {dRow(0), 37, hasEFanarts})
                    Else
                        DirectCast(dRow(0), DataRow).Item(1) = tmpShowDb.TVShow.Title
                        DirectCast(dRow(0), DataRow).Item(2) = hasPoster
                        DirectCast(dRow(0), DataRow).Item(3) = hasFanart
                        DirectCast(dRow(0), DataRow).Item(4) = If(String.IsNullOrEmpty(tmpShowDb.ShowNfoPath), False, True)
                        DirectCast(dRow(0), DataRow).Item(5) = False
                        DirectCast(dRow(0), DataRow).Item(22) = tmpShowDb.ShowLanguage
                        DirectCast(dRow(0), DataRow).Item(24) = hasBanner
                        DirectCast(dRow(0), DataRow).Item(26) = hasLandscape
                        DirectCast(dRow(0), DataRow).Item(29) = hasTheme
                        DirectCast(dRow(0), DataRow).Item(31) = hasCharacterArt
                        DirectCast(dRow(0), DataRow).Item(33) = hasClearLogo
                        DirectCast(dRow(0), DataRow).Item(35) = hasClearArt
                        DirectCast(dRow(0), DataRow).Item(37) = hasEFanarts
                    End If
                End If

                Master.DB.SaveTVShowToDB(tmpShowDb, False, WithEpisodes, ToNfo)

                ' DanCooper: i'm not shure if this is a proper solution...
                If Master.eSettings.TVASPosterAnyEnabled Then
                    tmpShowDb.SeasonBannerPath = sContainer.AllSeasonsBanner
                    tmpShowDb.SeasonFanartPath = sContainer.AllSeasonsFanart
                    tmpShowDb.SeasonLandscapePath = sContainer.AllSeasonsLandscape
                    tmpShowDb.SeasonPosterPath = sContainer.AllSeasonsPoster
                    Master.DB.SaveTVSeasonToDB(tmpShowDb, False, False)
                End If

                If Not BatchMode Then
                    Me.tspbLoading.Value += 1
                    Application.DoEvents()
                End If

                If WithEpisodes Then
                    Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Concat("SELECT ID FROM TVEps WHERE TVShowID = ", ID, " AND Missing = 0;")
                        Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                            While SQLReader.Read
                                Me.RefreshEpisode(Convert.ToInt64(SQLReader("ID")), True)
                                If Not BatchMode Then
                                    Me.tspbLoading.Value += 1
                                    Application.DoEvents()
                                    Threading.Thread.Sleep(50)
                                End If
                            End While
                        End Using
                    End Using

                    Master.DB.CleanSeasons(True)
                End If

            Else
                Master.DB.DeleteTVShowFromDB(ID, WithEpisodes)
                Return True
            End If

            If Not BatchMode Then
                SQLtransaction.Commit()
                SQLtransaction = Nothing

                Me.LoadShowInfo(Convert.ToInt32(ID))

                Me.tslLoading.Visible = False
                Me.tspbLoading.Visible = False
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return False
    End Function

    Private Sub ReloadMovie()
        Try
            Me.dgvMovies.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)

            Dim doFill As Boolean = False
            Dim tFill As Boolean = False

            Dim doBatch As Boolean = Not Me.dgvMovies.SelectedRows.Count = 1

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    tFill = Me.RefreshMovie(Convert.ToInt64(sRow.Cells(0).Value), doBatch)
                    If tFill Then doFill = True
                Next
                SQLtransaction.Commit()
            End Using

            Me.dgvMovies.Cursor = Cursors.Default
            Me.SetControlsEnabled(True)

            If doFill Then FillList(True, True, False) Else DoTitleCheck()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ReloadMovieSet()
        Try
            Me.dgvMovieSets.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)

            Dim doFill As Boolean = False
            Dim tFill As Boolean = False

            Dim doBatch As Boolean = Not Me.dgvMovieSets.SelectedRows.Count = 1

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                    tFill = Me.RefreshMovieSet(Convert.ToInt64(sRow.Cells(0).Value), doBatch)
                    If tFill Then doFill = True
                Next
                SQLtransaction.Commit()
            End Using

            Me.dgvMovieSets.Cursor = Cursors.Default
            Me.SetControlsEnabled(True)

            If doFill Then FillList(False, True, False)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieRemoveFromDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRemoveFromDB.Click
        Try
            Me.ClearInfo()

            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Master.DB.DeleteMovieFromDB(Convert.ToInt64(sRow.Cells(0).Value))
            Next

            Me.FillList(True, True, False)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieGenresRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresRemove.Click
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parGenre As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parGenre", DbType.String, 0, "Genre")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE movies SET Genre = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        If sRow.Cells(27).Value.ToString.Contains(Me.cmnuMovieGenresGenre.Text) Then
                            parGenre.Value = sRow.Cells(27).Value.ToString.Replace(String.Concat(" / ", Me.cmnuMovieGenresGenre.Text), String.Empty).Replace(String.Concat(Me.cmnuMovieGenresGenre.Text, " / "), String.Empty).Replace(Me.cmnuMovieGenresGenre.Text, String.Empty).Trim
                            parID.Value = sRow.Cells(0).Value
                            SQLcommand.ExecuteNonQuery()
                        End If
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    Me.RefreshMovie(Convert.ToInt64(sRow.Cells(0).Value), True, False, True)
                Next
                SQLtransaction.Commit()
            End Using

            Me.LoadMovieInfo(Convert.ToInt32(Me.dgvMovies.Item(0, Me.dgvMovies.CurrentCell.RowIndex).Value), Me.dgvMovies.Item(1, Me.dgvMovies.CurrentCell.RowIndex).Value.ToString, True, False)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ResizeMoviesList()
        If Not Master.isWindows Then
            If Me.dgvMovies.ColumnCount > 0 Then
                Me.dgvMovies.Columns(3).Width = Me.dgvMovies.Width - _
                If(Master.eSettings.MoviePosterCol, 0, 20) - _
                If(Master.eSettings.MovieFanartCol, 0, 20) - _
                If(Master.eSettings.MovieNFOCol, 0, 20) - _
                If(Master.eSettings.MovieTrailerCol, 0, 20) - _
                If(Master.eSettings.MovieSubCol, 0, 20) - _
                If(Master.eSettings.MovieEThumbsCol, 0, 20) - _
                If(Master.eSettings.MovieWatchedCol, 0, 20) - _
                If(Master.eSettings.MovieEFanartsCol, 0, 20) - _
                If(Master.eSettings.MovieBannerCol, 0, 20) - _
                If(Master.eSettings.MovieLandscapeCol, 0, 20) - _
                If(Master.eSettings.MovieThemeCol, 0, 20) - _
                If(Master.eSettings.MovieDiscArtCol, 0, 20) - _
                If(Master.eSettings.MovieClearLogoCol, 0, 20) - _
                If(Master.eSettings.MovieClearArtCol, 0, 20) - _
                If(Me.dgvMovies.DisplayRectangle.Height > Me.dgvMovies.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If
        End If
    End Sub

    Private Sub ResizeMovieSetsList()
        If Not Master.isWindows Then
            If Me.dgvMovieSets.ColumnCount > 0 Then
                Me.dgvMovieSets.Columns(0).Width = Me.dgvMovieSets.Width - _
                If(Master.eSettings.MovieSetNfoCol, 0, 20) - _
                If(Master.eSettings.MovieSetPosterCol, 0, 20) - _
                If(Master.eSettings.MovieSetFanartCol, 0, 20) - _
                If(Master.eSettings.MovieSetBannerCol, 0, 20) - _
                If(Master.eSettings.MovieSetLandscapeCol, 0, 20) - _
                If(Master.eSettings.MovieSetDiscArtCol, 0, 20) - _
                If(Master.eSettings.MovieSetClearLogoCol, 0, 20) - _
                If(Master.eSettings.MovieSetClearArtCol, 0, 20) - _
                If(Me.dgvMovieSets.DisplayRectangle.Height > Me.dgvMovieSets.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If
        End If
    End Sub

    Private Sub ResizeTVLists(ByVal iType As Integer)
        '0 = all.... needed???

        If Not Master.isWindows Then
            If (iType = 0 OrElse iType = 1) AndAlso Me.dgvTVShows.ColumnCount > 0 Then
                Me.dgvTVShows.Columns(1).Width = Me.dgvTVShows.Width - _
                If(Master.eSettings.TVShowPosterCol, 0, 20) - _
                If(Master.eSettings.TVShowFanartCol, 0, 20) - _
                If(Master.eSettings.TVShowNfoCol, 0, 20) - _
                If(Me.dgvTVShows.DisplayRectangle.Height > Me.dgvTVShows.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

            If (iType = 0 OrElse iType = 2) AndAlso Me.dgvTVSeasons.ColumnCount > 0 Then
                Me.dgvTVSeasons.Columns(1).Width = Me.dgvTVSeasons.Width - _
                If(Master.eSettings.TVSeasonPosterCol, 0, 20) - _
                If(Master.eSettings.TVSeasonFanartCol, 0, 20) - _
                If(Me.dgvTVSeasons.DisplayRectangle.Height > Me.dgvTVSeasons.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

            If (iType = 0 OrElse iType = 3) AndAlso Me.dgvTVEpisodes.ColumnCount > 0 Then
                Me.dgvTVEpisodes.Columns(2).Width = Me.dgvTVEpisodes.Width - 40 - _
                If(Master.eSettings.TVEpisodePosterCol, 0, 20) - _
                If(Master.eSettings.TVEpisodeFanartCol, 0, 20) - _
                If(Master.eSettings.TVEpisodeNfoCol, 0, 20) - _
                If(Me.dgvTVEpisodes.DisplayRectangle.Height > Me.dgvTVEpisodes.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

        End If
    End Sub

    Private Sub RunFilter(Optional ByVal doFill As Boolean = False)
        Try
            If Me.Visible Then

                Me.ClearInfo()

                Me.prevMovieRow = -2
                Me.currMovieRow = -1
                Me.dgvMovies.ClearSelection()
                Me.dgvMovies.CurrentCell = Nothing

                If FilterArray.Count > 0 Then
                    Dim FilterString As String = String.Empty

                    If rbFilterAnd.Checked Then
                        FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray.ToArray, " AND ")
                    Else
                        FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray.ToArray, " OR ")
                    End If

                    bsMovies.Filter = FilterString
                Else
                    bsMovies.RemoveFilter()
                End If

                If doFill Then
                    Me.FillList(True, False, False)
                Else
                    Me.txtSearch.Focus()
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ScannerUpdated(ByVal iType As Integer, ByVal sText As String)
        Select Case iType
            Case 1
                Me.SetStatus(String.Concat(Master.eLang.GetString(814, "Added Episode:"), " " & sText))
            Case 2
                Me.SetStatus(Master.eLang.GetString(116, "Performing Preliminary Tasks (Gathering Data)..."))
            Case 3
                Me.SetStatus(Master.eLang.GetString(644, "Cleaning Database..."))
            Case Else
                Me.SetStatus(String.Concat(Master.eLang.GetString(815, "Added Movie:"), " " & sText))
        End Select
    End Sub

    Private Sub ScanningCompleted()
        If Not Master.isCL Then
            Me.SetStatus(String.Empty)
            Me.FillList(True, True, True)
            Me.tspbLoading.Visible = False
            Me.tslLoading.Visible = False
        Else
            Me.FillList(True, True, True)
            LoadingDone = True
        End If
    End Sub

    Private Sub scMain_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles scMain.SplitterMoved
        Try
            If Me.Created Then
                Me.SuspendLayout()
                Me.MoveMPAA()
                Me.MoveGenres()

                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)
                Me.pnlNoInfo.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), Convert.ToInt32((Me.scMain.Panel2.Height - Me.pnlNoInfo.Height) / 2))
                Me.pnlCancel.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), 100)
                Me.pnlFilterGenre.Location = New Point(Me.gbFilterSpecific.Left + Me.txtFilterGenre.Left, (Me.pnlFilter.Top + Me.txtFilterGenre.Top + Me.gbFilterSpecific.Top) - Me.pnlFilterGenre.Height)
                Me.pnlFilterSource.Location = New Point(Me.gbFilterSpecific.Left + Me.txtFilterSource.Left, (Me.pnlFilter.Top + Me.txtFilterSource.Top + Me.gbFilterSpecific.Top) - Me.pnlFilterSource.Height)

                Select Case Me.tcMain.SelectedIndex
                    Case 0
                        Me.dgvMovies.Focus()
                    Case 1
                        Me.dgvMovieSets.Focus()
                    Case 2
                        Me.dgvTVShows.Focus()
                End Select

                Me.ResumeLayout(True)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SelectEpisodeRow(ByVal iRow As Integer)
        Try
            If Not Convert.ToBoolean(Me.dgvTVEpisodes.Item(4, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvTVEpisodes.Item(5, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvTVEpisodes.Item(6, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvTVEpisodes.Item(22, iRow).Value) Then
                Me.ClearInfo(False)
                Me.ShowNoInfo(True, 2)

                Master.currShow = Master.DB.LoadTVEpFromDB(Convert.ToInt32(Me.dgvTVEpisodes.Item(0, iRow).Value), True)
                Me.fillScreenInfoWithEpisode()

                If Not Convert.ToBoolean(Me.dgvTVEpisodes.Item(22, iRow).Value) AndAlso Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaInfo.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwRefreshMovies.IsBusy AndAlso Not Me.bwRefreshMovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuEpisode.Enabled = True
                End If
            Else
                Me.LoadEpisodeInfo(Convert.ToInt32(Me.dgvTVEpisodes.SelectedRows(0).Cells(0).Value))
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieReSelAskAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskMetaData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskMetaData.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAskTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAskTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAsk, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoBanner.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoClearArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoClearLogo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoDiscArt.Click
        Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoEFanarts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoEFanarts.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoEThumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoEThumbs.Click
        Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoFanart.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoLandscape.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoMetaData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoMetaData.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Meta, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoNfo.Click
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoPoster.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoTheme.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelAutoTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelAutoTrailer.Click
        Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
        MovieScrapeData(True, Enums.ScrapeType.FullAuto, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieReSelSkipAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReSelSkipAll.Click
        Functions.SetScraperMod(Enums.ModType_Movie.All, True)
        MovieScrapeData(True, Enums.ScrapeType.FullSkip, Master.DefaultMovieOptions)
    End Sub

    Private Sub cmnuMovieUpSelCert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelCert.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bCert = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelCountry.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bCountry = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelDirector_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelDirector.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bDirector = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelMPAA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelMPAA.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bMPAA = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelOutline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelOutline.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bOutline = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelPlot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelPlot.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bPlot = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelProducers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelProducers.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bProducers = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelRating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelRating.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bRating = True
        cScrapeOptions.bVotes = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelRelease.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bRelease = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelStudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelStudio.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bStudio = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelTagline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTagline.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTagline = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTitle.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTitle = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelTop250_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTop250.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTop250 = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTrailer.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTrailer = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelWriters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelWriter.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bWriters = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub

    Private Sub cmnuMovieUpSelYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelYear.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bYear = True
        Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
        MovieScrapeData(True, Enums.ScrapeType.SingleField, cScrapeOptions)
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub SelectMovieRow(ByVal iRow As Integer)
        Try
            If Not Convert.ToBoolean(Me.dgvMovies.Item(4, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvMovies.Item(5, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvMovies.Item(6, iRow).Value) Then
                Me.ClearInfo()
                Me.ShowNoInfo(True, 0)
                Master.currMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(Me.dgvMovies.Item(0, iRow).Value))
                Me.fillScreenInfoWithMovie()

                If Not Me.bwMovieScraper.IsBusy AndAlso Not Me.bwMovieSetScraper.IsBusy AndAlso Not Me.bwNonScrape.IsBusy AndAlso Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaInfo.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwRefreshMovies.IsBusy AndAlso Not Me.bwRefreshMovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuMovie.Enabled = True
                End If
            Else
                Me.LoadMovieInfo(Convert.ToInt32(Me.dgvMovies.Item(0, iRow).Value), Me.dgvMovies.Item(1, iRow).Value.ToString, True, False)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub SelectMovieSetRow(ByVal iRow As Integer)
        Try
            If Not Convert.ToBoolean(Me.dgvMovieSets.Item(4, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvMovieSets.Item(6, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvMovieSets.Item(2, iRow).Value) Then
                Me.ClearInfo()
                Me.ShowNoInfo(True, 3)
                Master.currMovieSet = Master.DB.LoadMovieSetFromDB(Convert.ToInt64(Me.dgvMovieSets.Item(0, iRow).Value))
                Me.fillScreenInfoWithMovieSet()

                If Not Me.bwMovieScraper.IsBusy AndAlso Not Me.bwMovieSetScraper.IsBusy AndAlso Not Me.bwNonScrape.IsBusy AndAlso Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaInfo.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwRefreshMovies.IsBusy AndAlso Not Me.bwRefreshMovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuMovie.Enabled = True
                End If
            Else
                Me.LoadMovieSetInfo(Convert.ToInt32(Me.dgvMovieSets.Item(0, iRow).Value), Me.dgvMovieSets.Item(1, iRow).Value.ToString, True, False)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Season selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub SelectSeasonRow(ByVal iRow As Integer)
        Try
            Me.ClearInfo(False)
            If String.IsNullOrEmpty(Master.currShow.ShowPosterPath) AndAlso String.IsNullOrEmpty(Master.currShow.ShowFanartPath) AndAlso _
               String.IsNullOrEmpty(Master.currShow.ShowNfoPath) AndAlso Not Convert.ToBoolean(Me.dgvTVSeasons.Item(3, iRow).Value) AndAlso _
               Not Convert.ToBoolean(Me.dgvTVSeasons.Item(4, iRow).Value) Then
                If Not Me.currThemeType = Theming.ThemeType.Show Then Me.ApplyTheme(Theming.ThemeType.Show)
                Me.ShowNoInfo(True, 1)
                Master.currShow = Master.DB.LoadTVSeasonFromDB(Convert.ToInt32(Me.dgvTVSeasons.Item(0, iRow).Value), Convert.ToInt32(Me.dgvTVSeasons.Item(2, iRow).Value), True)

                Me.FillEpisodes(Convert.ToInt32(Me.dgvTVSeasons.Item(0, iRow).Value), Convert.ToInt32(Me.dgvTVSeasons.Item(2, iRow).Value))

                If Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaInfo.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwRefreshMovies.IsBusy AndAlso Not Me.bwRefreshMovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuSeason.Enabled = True
                End If
            Else
                Me.LoadSeasonInfo(Convert.ToInt32(Me.dgvTVSeasons.Item(0, iRow).Value), Convert.ToInt32(Me.dgvTVSeasons.Item(2, iRow).Value))
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Show selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub SelectShowRow(ByVal iRow As Integer)
        Try
            Me.tmpTitle = Me.dgvTVShows.Item(1, iRow).Value.ToString
            Me.tmpTVDB = Me.dgvTVShows.Item(9, iRow).Value.ToString
            Me.tmpLang = Me.dgvTVShows.Item(22, iRow).Value.ToString
            Me.tmpOrdering = DirectCast(Convert.ToInt32(Me.dgvTVShows.Item(23, iRow).Value), Enums.Ordering)

            If Not Convert.ToBoolean(Me.dgvTVShows.Item(2, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvTVShows.Item(3, iRow).Value) AndAlso Not Convert.ToBoolean(Me.dgvTVShows.Item(4, iRow).Value) Then
                Me.ClearInfo()
                Me.ShowNoInfo(True, 1)

                Master.currShow = Master.DB.LoadTVFullShowFromDB(Convert.ToInt64(Me.dgvTVShows.Item(0, iRow).Value))

                Me.FillSeasons(Convert.ToInt32(Me.dgvTVShows.Item(0, iRow).Value))

                If Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaInfo.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwRefreshMovies.IsBusy AndAlso Not Me.bwRefreshMovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuShow.Enabled = True
                End If
            Else
                Me.LoadShowInfo(Convert.ToInt32(Me.dgvTVShows.Item(0, iRow).Value))
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetAVImages(ByVal aImage As Image())
        Try
            Me.pbResolution.Image = aImage(0)
            Me.pbVideo.Image = aImage(1)
            Me.pbVType.Image = aImage(2)
            Me.pbAudio.Image = aImage(3)
            Me.pbChannels.Image = aImage(4)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean, Optional ByVal withLists As Boolean = False, Optional ByVal withTools As Boolean = True)
        'Me.mnuMainTools.Enabled = isEnabled AndAlso (Me.dgvMediaList.RowCount > 0 OrElse Me.dgvTVShows.RowCount > 0)
        For Each i As Object In Me.mnuMainTools.DropDownItems
            If TypeOf i Is ToolStripMenuItem Then
                Dim o As ToolStripMenuItem = DirectCast(i, ToolStripMenuItem)
                If o.Tag Is Nothing Then
                    o.Enabled = isEnabled AndAlso (Me.dgvMovies.RowCount > 0 OrElse Me.dgvMovieSets.RowCount > 0 OrElse Me.dgvTVShows.RowCount > 0) AndAlso tcMain.SelectedIndex = 0
                ElseIf TypeOf o.Tag Is Structures.ModulesMenus Then
                    Dim tagmenu As Structures.ModulesMenus = DirectCast(o.Tag, Structures.ModulesMenus)
                    o.Enabled = (isEnabled OrElse Not withTools) AndAlso (((Me.dgvMovies.RowCount > 0 OrElse tagmenu.IfNoMovies) AndAlso tcMain.SelectedIndex = 0) OrElse ((Me.dgvTVShows.RowCount > 0 OrElse tagmenu.IfNoTVShow) AndAlso tcMain.SelectedIndex = 2))
                End If
            ElseIf TypeOf i Is ToolStripSeparator Then
                Dim o As ToolStripSeparator = DirectCast(i, ToolStripSeparator)
                o.Visible = (Me.mnuMainTools.DropDownItems.IndexOf(o) < Me.mnuMainTools.DropDownItems.Count - 1)
            End If
        Next
        With Master.eSettings
            If (Not .FileSystemExpertCleaner AndAlso (.CleanDotFanartJPG OrElse .CleanFanartJPG OrElse .CleanFolderJPG OrElse .CleanMovieFanartJPG OrElse _
            .CleanMovieJPG OrElse .CleanMovieNameJPG OrElse .CleanMovieNFO OrElse .CleanMovieNFOB OrElse _
            .CleanMovieTBN OrElse .CleanMovieTBNB OrElse .CleanPosterJPG OrElse .CleanPosterTBN OrElse .CleanExtrathumbs)) OrElse _
            (.FileSystemExpertCleaner AndAlso (.FileSystemCleanerWhitelist OrElse .FileSystemCleanerWhitelistExts.Count > 0)) Then
                Me.mnuMainToolsCleanFiles.Enabled = isEnabled AndAlso Me.dgvMovies.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 0
            Else
                Me.mnuMainToolsCleanFiles.Enabled = False
            End If
        End With
        Me.mnuMainEdit.Enabled = isEnabled
        Me.tsbAutoPilot.Enabled = isEnabled AndAlso Me.dgvMovies.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 0
        Me.mnuUpdate.Enabled = isEnabled
        Me.tsbMediaCenters.Enabled = isEnabled
        Me.cmnuMovie.Enabled = isEnabled
        Me.cmnuMovieSet.Enabled = isEnabled
        Me.cmnuShow.Enabled = isEnabled
        Me.cmnuSeason.Enabled = isEnabled
        Me.cmnuEpisode.Enabled = isEnabled
        Me.txtSearch.Enabled = isEnabled
        Me.tcMain.Enabled = isEnabled
        Me.btnMarkAll.Enabled = isEnabled
        Me.btnMetaDataRefresh.Enabled = isEnabled
        Me.scMain.IsSplitterFixed = Not isEnabled
        Me.scTV.IsSplitterFixed = Not isEnabled
        Me.scTVSeasonsEpisodes.IsSplitterFixed = Not isEnabled
        Me.mnuMainHelp.Enabled = isEnabled
        Me.cmnuTrayTools.Enabled = Me.mnuMainTools.Enabled
        Me.cmnuTrayScrape.Enabled = Me.tsbAutoPilot.Enabled
        Me.cmnuTrayUpdate.Enabled = isEnabled
        Me.cmnuTrayMediaCenters.Enabled = isEnabled
        Me.cmnuTraySettings.Enabled = isEnabled
        Me.cmnuTrayExit.Enabled = isEnabled

        If withLists OrElse isEnabled Then
            Me.dgvMovies.TabStop = isEnabled
            Me.dgvMovieSets.TabStop = isEnabled
            Me.dgvTVShows.TabStop = isEnabled
            Me.dgvTVSeasons.TabStop = isEnabled
            Me.dgvTVEpisodes.TabStop = isEnabled
            Me.dgvMovies.Enabled = isEnabled
            Me.dgvMovieSets.Enabled = isEnabled
            Me.dgvTVShows.Enabled = isEnabled
            Me.dgvTVSeasons.Enabled = isEnabled
            Me.dgvTVEpisodes.Enabled = isEnabled
        End If
    End Sub

    Private Sub cmnuMovieGenresSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresSet.Click
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parGenre As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parGenre", DbType.String, 0, "Genre")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE movies SET Genre = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parGenre.Value = Me.cmnuMovieGenresGenre.Text.Trim
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    Me.RefreshMovie(Convert.ToInt64(sRow.Cells(0).Value), True, False, True)
                Next
                SQLtransaction.Commit()
            End Using

            Me.LoadMovieInfo(Convert.ToInt32(Me.dgvMovies.Item(0, Me.dgvMovies.CurrentCell.RowIndex).Value), Me.dgvMovies.Item(1, Me.dgvMovies.CurrentCell.RowIndex).Value.ToString, True, False)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuShowLanguageSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowLanguageSet.Click
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLanguage As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLanguage", DbType.String, 0, "Language")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                    SQLcommand.CommandText = "UPDATE TVShows SET Language = (?) WHERE id = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                        parLanguage.Value = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = Me.cmnuShowLanguageLanguages.Text).abbreviation
                        parID.Value = sRow.Cells(0).Value
                        SQLcommand.ExecuteNonQuery()
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    Me.RefreshShow(Convert.ToInt64(sRow.Cells(0).Value), True, False, True, False)
                Next
                SQLtransaction.Commit()
            End Using

            Me.LoadShowInfo(Convert.ToInt32(Me.dgvTVShows.Item(0, Me.dgvTVShows.CurrentCell.RowIndex).Value))
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Enable or disable the various menu and context-menu actions based on the currently-defined settings
    ''' </summary>
    ''' <param name="ReloadFilters"></param>
    ''' <remarks></remarks>
    Private Sub SetMenus(ByVal ReloadFilters As Boolean)
        Dim mnuItem As ToolStripItem

        Try
            With Master.eSettings
                If (Not .FileSystemExpertCleaner AndAlso (.CleanDotFanartJPG OrElse .CleanFanartJPG OrElse .CleanFolderJPG OrElse .CleanMovieFanartJPG OrElse _
                .CleanMovieJPG OrElse .CleanMovieNameJPG OrElse .CleanMovieNFO OrElse .CleanMovieNFOB OrElse _
                .CleanMovieTBN OrElse .CleanMovieTBNB OrElse .CleanPosterJPG OrElse .CleanPosterTBN OrElse .CleanExtrathumbs)) OrElse _
                (.FileSystemExpertCleaner AndAlso (.FileSystemCleanerWhitelist OrElse .FileSystemCleanerWhitelistExts.Count > 0)) Then
                    Me.mnuMainToolsCleanFiles.Enabled = True AndAlso Me.dgvMovies.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 0
                Else
                    Me.mnuMainToolsCleanFiles.Enabled = False
                End If

                Me.mnuMainToolsBackdrops.Enabled = Directory.Exists(.MovieBackdropsPath)

                ' for future use
                Me.mnuMainToolsClearCache.Enabled = False

                'Me.mnuAllAutoExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuAllAskExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuMissAutoExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuMissAskExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuMarkAutoExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuMarkAskExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuNewAutoExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuNewAskExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuFilterAutoExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET
                'Me.mnuFilterAskExtra.Enabled = .AutoThumbs > 0 OrElse .AutoET

                'Actor Thumbs
                Dim ActorAllowed As Boolean = .MovieActorThumbsAnyEnabled
                Me.mnuAllAutoActor.Enabled = ActorAllowed
                Me.mnuAllAskActor.Enabled = ActorAllowed
                Me.mnuMissAutoActor.Enabled = ActorAllowed
                Me.mnuMissAskActor.Enabled = ActorAllowed
                Me.mnuNewAutoActor.Enabled = ActorAllowed
                Me.mnuNewAskActor.Enabled = ActorAllowed
                Me.mnuMarkAutoActor.Enabled = ActorAllowed
                Me.mnuMarkAskActor.Enabled = ActorAllowed
                Me.mnuFilterAutoActor.Enabled = ActorAllowed
                Me.mnuFilterAskActor.Enabled = ActorAllowed
                Me.cmnuMovieReSelAskActor.Enabled = ActorAllowed
                Me.cmnuMovieReSelAutoActor.Enabled = ActorAllowed
                Me.cmnuTrayAllAutoActor.Enabled = ActorAllowed
                Me.cmnuTrayAllAskActor.Enabled = ActorAllowed
                Me.cmnuTrayMissAutoActor.Enabled = ActorAllowed
                Me.cmnuTrayMissAskActor.Enabled = ActorAllowed
                Me.cmnuTrayNewAutoActor.Enabled = ActorAllowed
                Me.cmnuTrayNewAskActor.Enabled = ActorAllowed
                Me.cmnuTrayMarkAutoActor.Enabled = ActorAllowed
                Me.cmnuTrayMarkAskActor.Enabled = ActorAllowed
                Me.cmnuTrayFilterAutoActor.Enabled = ActorAllowed
                Me.cmnuTrayFilterAskActor.Enabled = ActorAllowed

                'Banner
                Dim BannerAllowed As Boolean = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Banner)
                Me.mnuAllAutoBanner.Enabled = BannerAllowed
                Me.mnuAllAskBanner.Enabled = BannerAllowed
                Me.mnuMissAutoBanner.Enabled = BannerAllowed
                Me.mnuMissAskBanner.Enabled = BannerAllowed
                Me.mnuNewAutoBanner.Enabled = BannerAllowed
                Me.mnuNewAskBanner.Enabled = BannerAllowed
                Me.mnuMarkAutoBanner.Enabled = BannerAllowed
                Me.mnuMarkAskBanner.Enabled = BannerAllowed
                Me.mnuFilterAutoBanner.Enabled = BannerAllowed
                Me.mnuFilterAskBanner.Enabled = BannerAllowed
                Me.cmnuMovieReSelAskBanner.Enabled = BannerAllowed
                Me.cmnuMovieReSelAutoBanner.Enabled = BannerAllowed
                Me.cmnuTrayAllAutoBanner.Enabled = BannerAllowed
                Me.cmnuTrayAllAskBanner.Enabled = BannerAllowed
                Me.cmnuTrayMissAutoBanner.Enabled = BannerAllowed
                Me.cmnuTrayMissAskBanner.Enabled = BannerAllowed
                Me.cmnuTrayNewAutoBanner.Enabled = BannerAllowed
                Me.cmnuTrayNewAskBanner.Enabled = BannerAllowed
                Me.cmnuTrayMarkAutoBanner.Enabled = BannerAllowed
                Me.cmnuTrayMarkAskBanner.Enabled = BannerAllowed
                Me.cmnuTrayFilterAutoBanner.Enabled = BannerAllowed
                Me.cmnuTrayFilterAskBanner.Enabled = BannerAllowed

                'ClearArt
                Dim ClearArtAllowed As Boolean = .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.ClearArt)
                Me.mnuAllAutoClearArt.Enabled = ClearArtAllowed
                Me.mnuAllAskClearArt.Enabled = ClearArtAllowed
                Me.mnuMissAutoClearArt.Enabled = ClearArtAllowed
                Me.mnuMissAskClearArt.Enabled = ClearArtAllowed
                Me.mnuNewAutoClearArt.Enabled = ClearArtAllowed
                Me.mnuNewAskClearArt.Enabled = ClearArtAllowed
                Me.mnuMarkAutoClearArt.Enabled = ClearArtAllowed
                Me.mnuMarkAskClearArt.Enabled = ClearArtAllowed
                Me.mnuFilterAutoClearArt.Enabled = ClearArtAllowed
                Me.mnuFilterAskClearArt.Enabled = ClearArtAllowed
                Me.cmnuMovieReSelAskClearArt.Enabled = ClearArtAllowed
                Me.cmnuMovieReSelAutoClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayAllAutoClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayAllAskClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayMissAutoClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayMissAskClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayNewAutoClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayNewAskClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayMarkAutoClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayMarkAskClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayFilterAutoClearArt.Enabled = ClearArtAllowed
                Me.cmnuTrayFilterAskClearArt.Enabled = ClearArtAllowed

                'ClearLogo
                Dim ClearLogoAllowed As Boolean = .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.ClearLogo)
                Me.mnuAllAutoClearLogo.Enabled = ClearLogoAllowed
                Me.mnuAllAskClearLogo.Enabled = ClearLogoAllowed
                Me.mnuMissAutoClearLogo.Enabled = ClearLogoAllowed
                Me.mnuMissAskClearLogo.Enabled = ClearLogoAllowed
                Me.mnuNewAutoClearLogo.Enabled = ClearLogoAllowed
                Me.mnuNewAskClearLogo.Enabled = ClearLogoAllowed
                Me.mnuMarkAutoClearLogo.Enabled = ClearLogoAllowed
                Me.mnuMarkAskClearLogo.Enabled = ClearLogoAllowed
                Me.mnuFilterAutoClearLogo.Enabled = ClearLogoAllowed
                Me.mnuFilterAskClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuMovieReSelAskClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuMovieReSelAutoClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayAllAutoClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayAllAskClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayMissAutoClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayMissAskClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayNewAutoClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayNewAskClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayMarkAutoClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayMarkAskClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayFilterAutoClearLogo.Enabled = ClearLogoAllowed
                Me.cmnuTrayFilterAskClearLogo.Enabled = ClearLogoAllowed

                'DiscArt
                Dim DiscArtAllowed As Boolean = .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.DiscArt)
                Me.mnuAllAutoDiscArt.Enabled = DiscArtAllowed
                Me.mnuAllAskDiscArt.Enabled = DiscArtAllowed
                Me.mnuMissAutoDiscArt.Enabled = DiscArtAllowed
                Me.mnuMissAskDiscArt.Enabled = DiscArtAllowed
                Me.mnuNewAutoDiscArt.Enabled = DiscArtAllowed
                Me.mnuNewAskDiscArt.Enabled = DiscArtAllowed
                Me.mnuMarkAutoDiscArt.Enabled = DiscArtAllowed
                Me.mnuMarkAskDiscArt.Enabled = DiscArtAllowed
                Me.mnuFilterAutoDiscArt.Enabled = DiscArtAllowed
                Me.mnuFilterAskDiscArt.Enabled = DiscArtAllowed
                Me.cmnuMovieReSelAskDiscArt.Enabled = DiscArtAllowed
                Me.cmnuMovieReSelAutoDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayAllAutoDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayAllAskDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayMissAutoDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayMissAskDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayNewAutoDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayNewAskDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayMarkAutoDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayMarkAskDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayFilterAutoDiscArt.Enabled = DiscArtAllowed
                Me.cmnuTrayFilterAskDiscArt.Enabled = DiscArtAllowed

                'Extrafanart
                Dim EFanartsAllowed As Boolean = .MovieEFanartsAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Fanart)
                Me.mnuAllAutoEFanarts.Enabled = EFanartsAllowed
                Me.mnuAllAskEFanarts.Enabled = EFanartsAllowed
                Me.mnuMissAutoEFanarts.Enabled = EFanartsAllowed
                Me.mnuMissAskEFanarts.Enabled = EFanartsAllowed
                Me.mnuMarkAutoEFanarts.Enabled = EFanartsAllowed
                Me.mnuMarkAskEFanarts.Enabled = EFanartsAllowed
                Me.mnuNewAutoEFanarts.Enabled = EFanartsAllowed
                Me.mnuNewAskEFanarts.Enabled = EFanartsAllowed
                Me.mnuFilterAutoEFanarts.Enabled = EFanartsAllowed
                Me.mnuFilterAskEFanarts.Enabled = EFanartsAllowed
                Me.cmnuMovieReSelAskEFanarts.Enabled = EFanartsAllowed
                Me.cmnuMovieReSelAutoEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayAllAutoEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayAllAskEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayMissAutoEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayMissAskEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayMarkAutoEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayMarkAskEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayNewAutoEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayNewAskEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayFilterAutoEFanarts.Enabled = EFanartsAllowed
                Me.cmnuTrayFilterAskEFanarts.Enabled = EFanartsAllowed

                'Extrathumb
                Dim EThumbsAllowed As Boolean = .MovieEThumbsAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Fanart)
                Me.mnuAllAutoEThumbs.Enabled = EThumbsAllowed
                Me.mnuAllAskEThumbs.Enabled = EThumbsAllowed
                Me.mnuMissAutoEThumbs.Enabled = EThumbsAllowed
                Me.mnuMissAskEThumbs.Enabled = EThumbsAllowed
                Me.mnuMarkAutoEThumbs.Enabled = EThumbsAllowed
                Me.mnuMarkAskEThumbs.Enabled = EThumbsAllowed
                Me.mnuNewAutoEThumbs.Enabled = EThumbsAllowed
                Me.mnuNewAskEThumbs.Enabled = EThumbsAllowed
                Me.mnuFilterAutoEThumbs.Enabled = EThumbsAllowed
                Me.mnuFilterAskEThumbs.Enabled = EThumbsAllowed
                Me.cmnuMovieReSelAskEThumbs.Enabled = EThumbsAllowed
                Me.cmnuMovieReSelAutoEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayAllAutoEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayAllAskEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayMissAutoEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayMissAskEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayMarkAutoEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayMarkAskEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayNewAutoEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayNewAskEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayFilterAutoEThumbs.Enabled = EThumbsAllowed
                Me.cmnuTrayFilterAskEThumbs.Enabled = EThumbsAllowed

                'Fanart
                Dim FanartAllowed As Boolean = .MovieFanartAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Fanart)
                Me.mnuAllAutoFanart.Enabled = FanartAllowed
                Me.mnuAllAskFanart.Enabled = FanartAllowed
                Me.mnuMissAutoFanart.Enabled = FanartAllowed
                Me.mnuMissAskFanart.Enabled = FanartAllowed
                Me.mnuMarkAutoFanart.Enabled = FanartAllowed
                Me.mnuMarkAskFanart.Enabled = FanartAllowed
                Me.mnuNewAutoFanart.Enabled = FanartAllowed
                Me.mnuNewAskFanart.Enabled = FanartAllowed
                Me.mnuFilterAutoFanart.Enabled = FanartAllowed
                Me.mnuFilterAskFanart.Enabled = FanartAllowed
                Me.cmnuMovieReSelAskFanart.Enabled = FanartAllowed
                Me.cmnuMovieReSelAutoFanart.Enabled = FanartAllowed
                Me.cmnuTrayAllAutoFanart.Enabled = FanartAllowed
                Me.cmnuTrayAllAskFanart.Enabled = FanartAllowed
                Me.cmnuTrayMissAutoFanart.Enabled = FanartAllowed
                Me.cmnuTrayMissAskFanart.Enabled = FanartAllowed
                Me.cmnuTrayMarkAutoFanart.Enabled = FanartAllowed
                Me.cmnuTrayMarkAskFanart.Enabled = FanartAllowed
                Me.cmnuTrayNewAutoFanart.Enabled = FanartAllowed
                Me.cmnuTrayNewAskFanart.Enabled = FanartAllowed
                Me.cmnuTrayFilterAutoFanart.Enabled = FanartAllowed
                Me.cmnuTrayFilterAskFanart.Enabled = FanartAllowed

                'Landscape
                Dim LandscapeAllowed As Boolean = .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Landscape)
                Me.mnuAllAutoLandscape.Enabled = LandscapeAllowed
                Me.mnuAllAskLandscape.Enabled = LandscapeAllowed
                Me.mnuMissAutoLandscape.Enabled = LandscapeAllowed
                Me.mnuMissAskLandscape.Enabled = LandscapeAllowed
                Me.mnuNewAutoLandscape.Enabled = LandscapeAllowed
                Me.mnuNewAskLandscape.Enabled = LandscapeAllowed
                Me.mnuMarkAutoLandscape.Enabled = LandscapeAllowed
                Me.mnuMarkAskLandscape.Enabled = LandscapeAllowed
                Me.mnuFilterAutoLandscape.Enabled = LandscapeAllowed
                Me.mnuFilterAskLandscape.Enabled = LandscapeAllowed
                Me.cmnuMovieReSelAskLandscape.Enabled = LandscapeAllowed
                Me.cmnuMovieReSelAutoLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayAllAutoLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayAllAskLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayMissAutoLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayMissAskLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayNewAutoLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayNewAskLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayMarkAutoLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayMarkAskLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayFilterAutoLandscape.Enabled = LandscapeAllowed
                Me.cmnuTrayFilterAskLandscape.Enabled = LandscapeAllowed

                'Metadata
                Me.mnuAllAskMI.Enabled = .MovieScraperMetaDataScan
                Me.mnuAllAutoMI.Enabled = .MovieScraperMetaDataScan
                Me.mnuNewAskMI.Enabled = .MovieScraperMetaDataScan
                Me.mnuNewAutoMI.Enabled = .MovieScraperMetaDataScan
                Me.mnuMarkAskMI.Enabled = .MovieScraperMetaDataScan
                Me.mnuMarkAutoMI.Enabled = .MovieScraperMetaDataScan
                Me.mnuFilterAskMI.Enabled = .MovieScraperMetaDataScan
                Me.mnuFilterAutoMI.Enabled = .MovieScraperMetaDataScan
                Me.cmnuMovieReSelAskMetaData.Enabled = .MovieScraperMetaDataScan
                Me.cmnuMovieReSelAutoMetaData.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayAllAskMI.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayAllAutoMetaData.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayNewAskMI.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayNewAutoMI.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayMarkAskMI.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayMarkAutoMI.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayFilterAskMI.Enabled = .MovieScraperMetaDataScan
                Me.cmnuTrayFilterAutoMI.Enabled = .MovieScraperMetaDataScan

                'Poster
                Dim PosterAllowed As Boolean = .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Image_Movie(Enums.ScraperCapabilities.Poster)
                Me.mnuAllAutoPoster.Enabled = PosterAllowed
                Me.mnuAllAskPoster.Enabled = PosterAllowed
                Me.mnuMissAutoPoster.Enabled = PosterAllowed
                Me.mnuMissAskPoster.Enabled = PosterAllowed
                Me.mnuMarkAutoPoster.Enabled = PosterAllowed
                Me.mnuMarkAskPoster.Enabled = PosterAllowed
                Me.mnuNewAutoPoster.Enabled = PosterAllowed
                Me.mnuNewAskPoster.Enabled = PosterAllowed
                Me.mnuFilterAutoPoster.Enabled = PosterAllowed
                Me.mnuFilterAskPoster.Enabled = PosterAllowed
                Me.cmnuMovieReSelAskPoster.Enabled = PosterAllowed
                Me.cmnuMovieReSelAutoPoster.Enabled = PosterAllowed
                Me.cmnuTrayAllAutoPoster.Enabled = PosterAllowed
                Me.cmnuTrayAllAskPoster.Enabled = PosterAllowed
                Me.cmnuTrayMissAutoPoster.Enabled = PosterAllowed
                Me.cmnuTrayMissAskPoster.Enabled = PosterAllowed
                Me.cmnuTrayMarkAutoPoster.Enabled = PosterAllowed
                Me.cmnuTrayMarkAskPoster.Enabled = PosterAllowed
                Me.cmnuTrayNewAutoPoster.Enabled = PosterAllowed
                Me.cmnuTrayNewAskPoster.Enabled = PosterAllowed
                Me.cmnuTrayFilterAutoPoster.Enabled = PosterAllowed
                Me.cmnuTrayFilterAskPoster.Enabled = PosterAllowed

                'Theme
                Dim ThemeAllowed As Boolean = .MovieThemeEnable AndAlso .MovieThemeAnyEnabled ' AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Theme) 'TODO
                Me.mnuAllAutoTheme.Enabled = ThemeAllowed
                Me.mnuAllAskTheme.Enabled = ThemeAllowed
                Me.mnuMissAutoTheme.Enabled = ThemeAllowed
                Me.mnuMissAskTheme.Enabled = ThemeAllowed
                Me.mnuNewAutoTheme.Enabled = ThemeAllowed
                Me.mnuNewAskTheme.Enabled = ThemeAllowed
                Me.mnuMarkAutoTheme.Enabled = ThemeAllowed
                Me.mnuMarkAskTheme.Enabled = ThemeAllowed
                Me.mnuFilterAutoTheme.Enabled = ThemeAllowed
                Me.mnuFilterAskTheme.Enabled = ThemeAllowed
                Me.cmnuMovieReSelAskTheme.Enabled = ThemeAllowed
                Me.cmnuMovieReSelAutoTheme.Enabled = ThemeAllowed
                Me.cmnuTrayAllAutoTheme.Enabled = ThemeAllowed
                Me.cmnuTrayAllAskTheme.Enabled = ThemeAllowed
                Me.cmnuTrayMissAutoTheme.Enabled = ThemeAllowed
                Me.cmnuTrayMissAskTheme.Enabled = ThemeAllowed
                Me.cmnuTrayNewAutoTheme.Enabled = ThemeAllowed
                Me.cmnuTrayNewAskTheme.Enabled = ThemeAllowed
                Me.cmnuTrayMarkAutoTheme.Enabled = ThemeAllowed
                Me.cmnuTrayMarkAskTheme.Enabled = ThemeAllowed
                Me.cmnuTrayFilterAutoTheme.Enabled = ThemeAllowed
                Me.cmnuTrayFilterAskTheme.Enabled = ThemeAllowed

                'Trailer
                Dim TrailerAllowed As Boolean = .MovieTrailerEnable AndAlso .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.QueryScraperCapabilities_Trailer_Movie(Enums.ScraperCapabilities.Trailer)
                Me.mnuAllAutoTrailer.Enabled = TrailerAllowed
                Me.mnuAllAskTrailer.Enabled = TrailerAllowed
                Me.mnuMissAutoTrailer.Enabled = TrailerAllowed
                Me.mnuMissAskTrailer.Enabled = TrailerAllowed
                Me.mnuNewAutoTrailer.Enabled = TrailerAllowed
                Me.mnuNewAskTrailer.Enabled = TrailerAllowed
                Me.mnuMarkAutoTrailer.Enabled = TrailerAllowed
                Me.mnuMarkAskTrailer.Enabled = TrailerAllowed
                Me.mnuFilterAutoTrailer.Enabled = TrailerAllowed
                Me.mnuFilterAskTrailer.Enabled = TrailerAllowed
                Me.cmnuMovieReSelAskTrailer.Enabled = TrailerAllowed
                Me.cmnuMovieReSelAutoTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayAllAutoTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayAllAskTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayMissAutoTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayMissAskTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayNewAutoTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayNewAskTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayMarkAutoTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayMarkAskTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayFilterAutoTrailer.Enabled = TrailerAllowed
                Me.cmnuTrayFilterAskTrailer.Enabled = TrailerAllowed

                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT COUNT(id) AS mcount FROM movies WHERE mark = 1;")
                    Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        SQLcount.Read()
                        If SQLcount.HasRows AndAlso Convert.ToInt32(SQLcount("mcount")) > 0 Then
                            Me.btnMarkAll.Text = Master.eLang.GetString(105, "Unmark All")
                        Else
                            Me.btnMarkAll.Text = Master.eLang.GetString(35, "Mark All")
                        End If
                    End Using
                End Using

                Me.mnuUpdateMovies.DropDownItems.Clear()
                Me.cmnuTrayUpdateMovies.DropDownItems.Clear()
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = "SELECT COUNT(ID) AS cID FROM Sources;"
                    If Convert.ToInt32(SQLNewcommand.ExecuteScalar) > 1 Then
                        mnuItem = Me.mnuUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                        mnuItem.Tag = String.Empty
                        mnuItem = Me.cmnuTrayUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                        mnuItem.Tag = String.Empty
                    End If
                    SQLNewcommand.CommandText = "SELECT Name FROM Sources;"
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLReader.Read
                            mnuItem = Me.mnuUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                            mnuItem.Tag = SQLReader("Name").ToString
                            mnuItem = Me.cmnuTrayUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                            mnuItem.Tag = SQLReader("Name").ToString
                        End While
                    End Using
                End Using

                Me.mnuUpdateShows.DropDownItems.Clear()
                Me.cmnuTrayUpdateShows.DropDownItems.Clear()
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = "SELECT COUNT(ID) AS cID FROM TVSources;"
                    If Convert.ToInt32(SQLNewcommand.ExecuteScalar) > 1 Then
                        mnuItem = Me.mnuUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                        mnuItem.Tag = String.Empty
                        mnuItem = Me.cmnuTrayUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                        mnuItem.Tag = String.Empty
                    End If
                    SQLNewcommand.CommandText = "SELECT Name FROM TVSources;"
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLReader.Read
                            mnuItem = Me.mnuUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                            mnuItem.Tag = SQLReader("Name").ToString
                            mnuItem = Me.cmnuTrayUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                            mnuItem.Tag = SQLReader("Name").ToString
                        End While
                    End Using
                End Using

                cmnuMovieGenresGenre.Items.Clear()
                Me.clbFilterGenres.Items.Clear()
                Dim lGenre() As Object = APIXML.GetGenreList
                cmnuMovieGenresGenre.Items.AddRange(lGenre)
                clbFilterGenres.Items.AddRange(lGenre)

                cmnuShowLanguageLanguages.Items.Clear()
                cmnuShowLanguageLanguages.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)

                'not technically a menu, but it's a good place to put it
                If ReloadFilters Then

                    clbFilterSource.Items.Clear()
                    Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLNewcommand.CommandText = String.Concat("SELECT Name FROM Sources;")
                        Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                            While SQLReader.Read
                                clbFilterSource.Items.Add(SQLReader("Name"))
                            End While
                        End Using
                    End Using

                    RemoveHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged
                    Me.cbFilterYear.Items.Clear()
                    cbFilterYear.Items.Add(Master.eLang.All)
                    For i As Integer = (Year(Today) + 1) To 1888 Step -1
                        Me.cbFilterYear.Items.Add(i)
                    Next
                    cbFilterYear.SelectedIndex = 0
                    AddHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged

                    RemoveHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged
                    cbFilterYearMod.SelectedIndex = 0
                    AddHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged

                    RemoveHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged
                    cbFilterFileSource.Items.Clear()
                    cbFilterFileSource.Items.Add(Master.eLang.All)
                    cbFilterFileSource.Items.AddRange(APIXML.SourceList.ToArray)
                    cbFilterFileSource.Items.Add(Master.eLang.None)
                    cbFilterFileSource.SelectedIndex = 0
                    AddHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged

                End If

            End With
            Me.tsbAutoPilot.Enabled = (Me.dgvMovies.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 0)
            Me.cmnuTrayScrape.Enabled = Me.tsbAutoPilot.Enabled
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub mnuMainToolsOfflineMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsOfflineHolder.Click, cmnuTrayToolsOfflineHolder.Click
        Me.SetControlsEnabled(False)
        Using dOfflineHolder As New dlgOfflineHolder
            dOfflineHolder.ShowDialog()
        End Using
        Me.LoadMedia(New Structures.Scans With {.Movies = True, .TV = False})
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub mnuMainToolsSetsManager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsSetsManager.Click, cmnuTrayToolsSetsManager.Click
        Me.SetControlsEnabled(False)
        Using dSetsManager As New dlgSetsManager
            dSetsManager.ShowDialog()
        End Using
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub SetStatus(ByVal sText As String)
        Me.tslStatus.Text = sText.Replace("&", "&&")
    End Sub

    Private Function GetStatus() As String
        Return Me.tslStatus.Text.Replace("&&", "&")
    End Function

    Sub HideLoadingSettings()
        If Not Me.pnlLoadSettings.InvokeRequired Then
            Me.pnlLoadSettings.Visible = False
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
        Me.mnuMainEditSettings.Enabled = True
        Me.pnlLoadSettings.Visible = False
        Me.cmnuTraySettings.Enabled = True
        Me.cmnuTrayExit.Enabled = True
        If Not dresult.DidCancel Then

            If Not Master.eSettings.TVDisplayMissingEpisodes Then
                Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = "DELETE FROM TVEps WHERE Missing = 1"
                        SQLCommand.ExecuteNonQuery()

                        Master.DB.CleanSeasons(True)
                    End Using
                    SQLTrans.Commit()
                End Using
            End If

            Me.SetUp(True)

            If Me.dgvMovies.RowCount > 0 Then
                Me.dgvMovies.Columns(4).Visible = Not Master.eSettings.MoviePosterCol
                Me.dgvMovies.Columns(5).Visible = Not Master.eSettings.MovieFanartCol
                Me.dgvMovies.Columns(6).Visible = Not Master.eSettings.MovieNFOCol
                Me.dgvMovies.Columns(7).Visible = Not Master.eSettings.MovieTrailerCol
                Me.dgvMovies.Columns(8).Visible = Not Master.eSettings.MovieSubCol
                Me.dgvMovies.Columns(9).Visible = Not Master.eSettings.MovieEThumbsCol
                Me.dgvMovies.Columns(34).Visible = Not Master.eSettings.MovieWatchedCol
            End If

            If Me.dgvTVShows.RowCount > 0 Then
                Me.dgvTVShows.Columns(2).Visible = Not Master.eSettings.TVShowPosterCol
                Me.dgvTVShows.Columns(3).Visible = Not Master.eSettings.TVShowFanartCol
                Me.dgvTVShows.Columns(4).Visible = Not Master.eSettings.TVShowNfoCol
            End If

            If Me.dgvTVSeasons.RowCount > 0 Then
                Me.dgvTVSeasons.Columns(3).Visible = Not Master.eSettings.TVSeasonPosterCol
                Me.dgvTVSeasons.Columns(4).Visible = Not Master.eSettings.TVSeasonFanartCol
            End If

            If Me.dgvTVEpisodes.RowCount > 0 Then
                Me.dgvTVEpisodes.Columns(4).Visible = Not Master.eSettings.TVEpisodePosterCol
                Me.dgvTVEpisodes.Columns(5).Visible = Not Master.eSettings.TVEpisodeFanartCol
                Me.dgvTVEpisodes.Columns(6).Visible = Not Master.eSettings.TVEpisodeNfoCol
            End If

            'might as well wait for these
            While Me.bwMetaInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If dresult.NeedsRefresh OrElse dresult.NeedsUpdate Then 'TODO: Check for moviesets
                If dresult.NeedsRefresh Then
                    If Not Me.fScanner.IsBusy Then
                        While Me.bwLoadMovieInfo.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse _
                             Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwRefreshMovieSets.IsBusy OrElse Me.bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        Me.RefreshAllMovies()
                        Me.RefreshAllMovieSets()
                    End If
                End If
                If dresult.NeedsUpdate Then
                    If Not Me.fScanner.IsBusy Then
                        While Me.bwLoadMovieInfo.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwRefreshMovies.IsBusy OrElse _
                            Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwRefreshMovieSets.IsBusy OrElse Me.bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        Me.LoadMedia(New Structures.Scans With {.Movies = True, .TV = True})
                    End If
                End If
            Else
                If Not Me.fScanner.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwMovieScraper.IsBusy AndAlso Not Me.bwRefreshMovies.IsBusy AndAlso _
                    Not Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwMovieSetScraper.IsBusy AndAlso Not Me.bwRefreshMovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.FillList(True, True, True)
                End If
            End If

            Me.SetMenus(True)
            If dresult.NeedsRestart Then
                Using dRestart As New dlgRestart
                    If dRestart.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Application.Restart()
                    End If
                End Using
            End If
        Else
            Me.SetMenus(False)
            Me.SetControlsEnabled(True)
        End If

    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainEditSettings.Click, cmnuTraySettings.Click
        Try
            Me.SetControlsEnabled(False)
            Me.pnlLoadSettings.Visible = True

            Dim dThread As Threading.Thread = New Threading.Thread(AddressOf ShowSettings)
            dThread.SetApartmentState(Threading.ApartmentState.STA)
            dThread.Start()
        Catch ex As Exception
            Me.mnuMainEditSettings.Enabled = True
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update the displayed moviesets counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMovieSetCount()
        Dim MovieSetCount As Integer = 0

        Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLCommand.CommandText = "SELECT COUNT(SetName) AS COUNT FROM Sets"
            MovieSetCount = Convert.ToInt32(SQLCommand.ExecuteScalar)
        End Using

        If MovieSetCount > 0 Then
            Me.tpMovieSets.Text = String.Format("{0} ({1})", Master.eLang.GetString(366, "Sets"), MovieSetCount)
        Else
            Me.tpMovieSets.Text = Master.eLang.GetString(366, "Sets")
        End If
    End Sub
    ''' <summary>
    ''' Update the displayed show/episode counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTVCount()
        Dim ShowCount As Integer = 0
        Dim EpCount As Integer = 0

        Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLCommand.CommandText = "SELECT COUNT(ID) AS COUNT FROM TVShows"
            ShowCount = Convert.ToInt32(SQLCommand.ExecuteScalar)

            SQLCommand.CommandText = "SELECT COUNT(ID) AS COUNT FROM TVEps WHERE Missing = 0"
            EpCount = Convert.ToInt32(SQLCommand.ExecuteScalar)
        End Using

        If ShowCount > 0 Then
            Me.tpTVShows.Text = String.Format("{0} ({1}/{2})", Master.eLang.GetString(653, "TV"), ShowCount, EpCount)
        Else
            Me.tpTVShows.Text = Master.eLang.GetString(653, "TV")
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

                ' Scrape Media Menu: All Movies
                .mnuAll.Text = Master.eLang.GetString(68, "All Movies")
                .cmnuTrayAll.Text = .mnuAll.Text

                ' Scrape Media Menu: Movie Missing Items
                .mnuMiss.Text = Master.eLang.GetString(78, "Movies Missing Items")
                .cmnuTrayMiss.Text = .mnuMiss.Text

                'Scrape Media Menu: New Movies
                .mnuNew.Text = Master.eLang.GetString(79, "New Movies")
                .cmnuTrayNew.Text = .mnuNew.Text

                ' Scrape Media menu: Marked Movies
                .mnuMark.Text = Master.eLang.GetString(80, "Marked Movies")
                .cmnuTrayMark.Text = .mnuMark.Text

                ' Scrape Media Menu: Current Filter
                .mnuFilter.Text = Master.eLang.GetString(624, "Current Filter")
                .cmnuTrayFilter.Text = .mnuFilter.Text

                ' Scrape Media Menu: Custom Scraper
                .mnuCustom.Text = Master.eLang.GetString(81, "Custom Scraper...")
                .cmnuTrayCustom.Text = .mnuCustom.Text

                ' Scrape Media Menu: Restart last scraper
                .mnuRestartScrape.Text = Master.eLang.GetString(1160, "Restart last scrape...")
                .cmnuTrayRestart.Text = .mnuRestartScrape.Text
                .mnuRestartScrape.Visible = Master.eSettings.RestartScraper
                .cmnuTrayRestart.Visible = Master.eSettings.RestartScraper

                ' Scrape Media Menu: FullAuto
                .mnuAllAuto.Text = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                .mnuMissAuto.Text = .mnuAllAuto.Text
                .mnuNewAuto.Text = .mnuAllAuto.Text
                .mnuMarkAuto.Text = .mnuAllAuto.Text
                .mnuFilterAuto.Text = .mnuAllAuto.Text
                .cmnuTrayAllAuto.Text = .mnuAllAuto.Text
                .cmnuTrayMissAuto.Text = .mnuAllAuto.Text
                .cmnuTrayNewAuto.Text = .mnuAllAuto.Text
                .cmnuTrayMarkAuto.Text = .mnuAllAuto.Text
                .cmnuTrayFilterAuto.Text = .mnuAllAuto.Text
                .cmnuMovieReSelAuto.Text = .mnuAllAuto.Text

                ' Scrape Media Menu: Ask
                .mnuAllAsk.Text = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                .mnuMissAsk.Text = .mnuAllAsk.Text
                .mnuNewAsk.Text = .mnuAllAsk.Text
                .mnuMarkAsk.Text = .mnuAllAsk.Text
                .mnuFilterAsk.Text = .mnuAllAsk.Text
                .cmnuTrayAllAsk.Text = .mnuAllAsk.Text
                .cmnuTrayMissAsk.Text = .mnuAllAsk.Text
                .cmnuTrayNewAsk.Text = .mnuAllAsk.Text
                .cmnuTrayMarkAsk.Text = .mnuAllAsk.Text
                .cmnuTrayFilterAsk.Text = .mnuAllAsk.Text
                .cmnuMovieReSelAsk.Text = .mnuAllAsk.Text

                ' Scrape Media Menu: Skip
                .mnuAllSkip.Text = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
                .mnuMissSkip.Text = .mnuAllSkip.Text
                .mnuNewSkip.Text = .mnuAllSkip.Text
                .mnuMarkSkip.Text = .mnuAllSkip.Text
                .mnuFilterSkip.Text = .mnuAllSkip.Text
                .cmnuTrayAllSkip.Text = .mnuAllSkip.Text
                .cmnuTrayMissSkip.Text = .mnuAllSkip.Text
                .cmnuTrayNewSkip.Text = .mnuAllSkip.Text
                .cmnuTrayMarkSkip.Text = .mnuAllSkip.Text
                .cmnuTrayFilterSkip.Text = .mnuAllSkip.Text
                .cmnuMovieReSelSkip.Text = .mnuAllSkip.Text

                ' Scrape Media Content: Actor Thumbs
                .mnuAllAutoActor.Text = Master.eLang.GetString(973, "Actor Thumbs Only")
                .mnuAllAskActor.Text = .mnuAllAutoActor.Text
                .mnuMissAutoActor.Text = .mnuAllAutoActor.Text
                .mnuMissAskActor.Text = .mnuAllAutoActor.Text
                .mnuNewAutoActor.Text = .mnuAllAutoActor.Text
                .mnuNewAskActor.Text = .mnuAllAutoActor.Text
                .mnuMarkAutoActor.Text = .mnuAllAutoActor.Text
                .mnuMarkAskActor.Text = .mnuAllAutoActor.Text
                .mnuFilterAutoActor.Text = .mnuAllAutoActor.Text
                .mnuFilterAskActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayAllAutoActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayAllAskActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayMissAutoActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayMissAskActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayNewAutoActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayNewAskActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayMarkAutoActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayMarkAskActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayFilterAutoActor.Text = .mnuAllAutoActor.Text
                .cmnuTrayFilterAskActor.Text = .mnuAllAutoActor.Text
                .cmnuMovieReSelAskActor.Text = .mnuAllAutoActor.Text
                .cmnuMovieReSelAutoActor.Text = .mnuAllAutoActor.Text

                ' Scrape Media Content: All Items
                .mnuAllAutoAll.Text = Master.eLang.GetString(70, "All Items")
                .mnuAllAskAll.Text = mnuAllAutoAll.Text
                .mnuAllSkipAll.Text = mnuAllAutoAll.Text
                .mnuMissAutoAll.Text = .mnuAllAutoAll.Text
                .mnuMissAskAll.Text = .mnuAllAutoAll.Text
                .mnuMissSkipAll.Text = .mnuAllAutoAll.Text
                .mnuNewAutoAll.Text = .mnuAllAutoAll.Text
                .mnuNewAskAll.Text = .mnuAllAutoAll.Text
                .mnuNewSkipAll.Text = .mnuAllAutoAll.Text
                .mnuMarkAutoAll.Text = .mnuAllAutoAll.Text
                .mnuMarkAskAll.Text = .mnuAllAutoAll.Text
                .mnuMarkSkipAll.Text = .mnuAllAutoAll.Text
                .mnuFilterAutoAll.Text = .mnuAllAutoAll.Text
                .mnuFilterAskAll.Text = .mnuAllAutoAll.Text
                .mnuFilterSkipAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayAllAutoAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayAllAskAll.Text = mnuAllAutoAll.Text
                .cmnuTrayAllSkipAll.Text = mnuAllAutoAll.Text
                .cmnuTrayMissAutoAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayMissAskAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayMissSkipAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayNewAutoAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayNewAskAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayNewSkipAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayMarkAutoAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayMarkAskAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayMarkSkipAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayFilterAutoAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayFilterAskAll.Text = .mnuAllAutoAll.Text
                .cmnuTrayFilterSkipAll.Text = .mnuAllAutoAll.Text
                .cmnuMovieReSelAskAll.Text = .mnuAllAutoAll.Text
                .cmnuMovieReSelAutoAll.Text = .mnuAllAutoAll.Text
                .cmnuMovieReSelSkipAll.Text = .mnuAllAutoAll.Text

                ' Scrape Media Content: Banner
                .mnuAllAutoBanner.Text = Master.eLang.GetString(1060, "Banner Only")
                .mnuAllAskBanner.Text = .mnuAllAutoBanner.Text
                .mnuMissAutoBanner.Text = .mnuAllAutoBanner.Text
                .mnuMissAskBanner.Text = .mnuAllAutoBanner.Text
                .mnuNewAutoBanner.Text = .mnuAllAutoBanner.Text
                .mnuNewAskBanner.Text = .mnuAllAutoBanner.Text
                .mnuMarkAutoBanner.Text = .mnuAllAutoBanner.Text
                .mnuMarkAskBanner.Text = .mnuAllAutoBanner.Text
                .mnuFilterAutoBanner.Text = .mnuAllAutoBanner.Text
                .mnuFilterAskBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayAllAutoBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayAllAskBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayMissAutoBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayMissAskBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayNewAutoBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayNewAskBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayMarkAutoBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayMarkAskBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayFilterAutoBanner.Text = .mnuAllAutoBanner.Text
                .cmnuTrayFilterAskBanner.Text = .mnuAllAutoBanner.Text
                .cmnuMovieReSelAskBanner.Text = .mnuAllAutoBanner.Text
                .cmnuMovieReSelAutoBanner.Text = .mnuAllAutoBanner.Text

                ' Scrape Media Content: ClearArt
                .mnuAllAutoClearArt.Text = Master.eLang.GetString(1122, "ClearArt Only")
                .mnuAllAskClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuMissAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuMissAskClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuNewAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuNewAskClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuMarkAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuMarkAskClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuFilterAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .mnuFilterAskClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayAllAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayAllAskClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayMissAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayMissAskClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayNewAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayNewAskClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayMarkAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayMarkAskClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayFilterAutoClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuTrayFilterAskClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuMovieReSelAskClearArt.Text = .mnuAllAutoClearArt.Text
                .cmnuMovieReSelAutoClearArt.Text = .mnuAllAutoClearArt.Text

                ' Scrape Media Content: ClearLogo
                .mnuAllAutoClearLogo.Text = Master.eLang.GetString(1123, "ClearLogo Only")
                .mnuAllAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuMissAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuMissAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuNewAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuNewAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuMarkAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuMarkAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuFilterAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .mnuFilterAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayAllAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayAllAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayMissAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayMissAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayNewAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayNewAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayMarkAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayMarkAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayFilterAutoClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuTrayFilterAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuMovieReSelAskClearLogo.Text = .mnuAllAutoClearLogo.Text
                .cmnuMovieReSelAutoClearLogo.Text = .mnuAllAutoClearLogo.Text

                ' Scrape Media Content: DiscArt
                .mnuAllAutoDiscArt.Text = Master.eLang.GetString(1124, "DiscArt Only")
                .mnuAllAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuMissAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuMissAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuNewAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuNewAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuMarkAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuMarkAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuFilterAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .mnuFilterAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayAllAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayAllAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayMissAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayMissAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayNewAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayNewAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayMarkAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayMarkAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayFilterAutoDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuTrayFilterAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuMovieReSelAskDiscArt.Text = .mnuAllAutoDiscArt.Text
                .cmnuMovieReSelAutoDiscArt.Text = .mnuAllAutoDiscArt.Text

                ' Scrape Media Content: Extrafanarts
                .mnuAllAutoEFanarts.Text = Master.eLang.GetString(975, "Extrafanarts Only")
                .mnuAllAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuMissAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuMissAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuNewAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuNewAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuMarkAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuMarkAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuFilterAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .mnuFilterAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayAllAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayAllAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayMissAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayMissAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayNewAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayNewAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayMarkAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayMarkAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayFilterAutoEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuTrayFilterAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuMovieReSelAskEFanarts.Text = .mnuAllAutoEFanarts.Text
                .cmnuMovieReSelAutoEFanarts.Text = .mnuAllAutoEFanarts.Text

                ' Scrape Media Content: Extrathumbs
                .mnuAllAutoEThumbs.Text = Master.eLang.GetString(74, "Extrathumbs Only")
                .mnuAllAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuMissAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuMissAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuNewAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuNewAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuMarkAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuMarkAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuFilterAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .mnuFilterAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayAllAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayAllAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayMissAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayMissAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayNewAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayNewAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayMarkAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayMarkAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayFilterAutoEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuTrayFilterAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuMovieReSelAskEThumbs.Text = .mnuAllAutoEThumbs.Text
                .cmnuMovieReSelAutoEThumbs.Text = .mnuAllAutoEThumbs.Text

                ' Scrape Media Content: Fanart
                .mnuAllAutoFanart.Text = Master.eLang.GetString(73, "Fanart Only")
                .mnuAllAskFanart.Text = .mnuAllAutoFanart.Text
                .mnuMissAutoFanart.Text = .mnuAllAutoFanart.Text
                .mnuMissAskFanart.Text = .mnuAllAutoFanart.Text
                .mnuNewAutoFanart.Text = .mnuAllAutoFanart.Text
                .mnuNewAskFanart.Text = .mnuAllAutoFanart.Text
                .mnuMarkAutoFanart.Text = .mnuAllAutoFanart.Text
                .mnuMarkAskFanart.Text = .mnuAllAutoFanart.Text
                .mnuFilterAutoFanart.Text = .mnuAllAutoFanart.Text
                .mnuFilterAskFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayAllAutoFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayAllAskFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayMissAutoFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayMissAskFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayNewAutoFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayNewAskFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayMarkAutoFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayMarkAskFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayFilterAutoFanart.Text = .mnuAllAutoFanart.Text
                .cmnuTrayFilterAskFanart.Text = .mnuAllAutoFanart.Text
                .cmnuMovieReSelAskFanart.Text = .mnuAllAutoFanart.Text
                .cmnuMovieReSelAutoFanart.Text = .mnuAllAutoFanart.Text

                ' Scrape Media Content: Landscape
                .mnuAllAutoLandscape.Text = Master.eLang.GetString(1061, "Landscape Only")
                .mnuAllAskLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuMissAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuMissAskLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuNewAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuNewAskLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuMarkAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuMarkAskLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuFilterAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .mnuFilterAskLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayAllAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayAllAskLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayMissAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayMissAskLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayNewAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayNewAskLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayMarkAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayMarkAskLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayFilterAutoLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuTrayFilterAskLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuMovieReSelAskLandscape.Text = .mnuAllAutoLandscape.Text
                .cmnuMovieReSelAutoLandscape.Text = .mnuAllAutoLandscape.Text

                ' Scrape Media Content: Meta Data
                .mnuAllAutoMI.Text = Master.eLang.GetString(76, "Meta Data Only")
                .mnuAllAskMI.Text = .mnuAllAutoMI.Text
                .mnuNewAutoMI.Text = .mnuAllAutoMI.Text
                .mnuNewAskMI.Text = .mnuAllAutoMI.Text
                .mnuMarkAutoMI.Text = .mnuAllAutoMI.Text
                .mnuMarkAskMI.Text = .mnuAllAutoMI.Text
                .mnuFilterAutoMI.Text = .mnuAllAutoMI.Text
                .mnuFilterAskMI.Text = .mnuAllAutoMI.Text
                .cmnuTrayAllAutoMetaData.Text = .mnuAllAutoMI.Text
                .cmnuTrayAllAskMI.Text = .mnuAllAutoMI.Text
                .cmnuTrayNewAutoMI.Text = .mnuAllAutoMI.Text
                .cmnuTrayNewAskMI.Text = .mnuAllAutoMI.Text
                .cmnuTrayMarkAutoMI.Text = .mnuAllAutoMI.Text
                .cmnuTrayMarkAskMI.Text = .mnuAllAutoMI.Text
                .cmnuTrayFilterAutoMI.Text = .mnuAllAutoMI.Text
                .cmnuTrayFilterAskMI.Text = .mnuAllAutoMI.Text
                .cmnuMovieReSelAskMetaData.Text = .mnuAllAutoMI.Text
                .cmnuMovieReSelAutoMetaData.Text = .mnuAllAutoMI.Text

                ' Scrape Media Content: NFO
                .mnuAllAutoNfo.Text = Master.eLang.GetString(71, "NFO Only")
                .mnuAllAskNfo.Text = .mnuAllAutoNfo.Text
                .mnuMissAutoNfo.Text = .mnuAllAutoNfo.Text
                .mnuMissAskNfo.Text = .mnuAllAutoNfo.Text
                .mnuNewAutoNfo.Text = .mnuAllAutoNfo.Text
                .mnuNewAskNfo.Text = .mnuAllAutoNfo.Text
                .mnuMarkAutoNfo.Text = .mnuAllAutoNfo.Text
                .mnuMarkAskNfo.Text = .mnuAllAutoNfo.Text
                .mnuFilterAutoNfo.Text = .mnuAllAutoNfo.Text
                .mnuFilterAskNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayAllAutoNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayAllAskNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayMissAutoNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayMissAskNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayNewAutoNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayNewAskNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayMarkAutoNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayMarkAskNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayFilterAutoNfo.Text = .mnuAllAutoNfo.Text
                .cmnuTrayFilterAskNfo.Text = .mnuAllAutoNfo.Text
                .cmnuMovieReSelAskNfo.Text = .mnuAllAutoNfo.Text
                .cmnuMovieReSelAutoNfo.Text = .mnuAllAutoNfo.Text

                ' Scrape Media Content: Poster
                .mnuAllAutoPoster.Text = Master.eLang.GetString(72, "Poster Only")
                .mnuAllAskPoster.Text = .mnuAllAutoPoster.Text
                .mnuMissAutoPoster.Text = .mnuAllAutoPoster.Text
                .mnuMissAskPoster.Text = .mnuAllAutoPoster.Text
                .mnuNewAutoPoster.Text = .mnuAllAutoPoster.Text
                .mnuNewAskPoster.Text = .mnuAllAutoPoster.Text
                .mnuMarkAutoPoster.Text = .mnuAllAutoPoster.Text
                .mnuMarkAskPoster.Text = .mnuAllAutoPoster.Text
                .mnuFilterAutoPoster.Text = .mnuAllAutoPoster.Text
                .mnuFilterAskPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayAllAutoPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayAllAskPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayMissAutoPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayMissAskPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayNewAutoPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayNewAskPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayMarkAutoPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayMarkAskPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayFilterAutoPoster.Text = .mnuAllAutoPoster.Text
                .cmnuTrayFilterAskPoster.Text = .mnuAllAutoPoster.Text
                .cmnuMovieReSelAskPoster.Text = .mnuAllAutoPoster.Text
                .cmnuMovieReSelAutoPoster.Text = .mnuAllAutoPoster.Text

                ' Scrape Media Content: Theme
                .mnuAllAutoTheme.Text = Master.eLang.GetString(1125, "Theme Only")
                .mnuAllAskTheme.Text = .mnuAllAutoTheme.Text
                .mnuMissAutoTheme.Text = .mnuAllAutoTheme.Text
                .mnuMissAskTheme.Text = .mnuAllAutoTheme.Text
                .mnuNewAutoTheme.Text = .mnuAllAutoTheme.Text
                .mnuNewAskTheme.Text = .mnuAllAutoTheme.Text
                .mnuMarkAutoTheme.Text = .mnuAllAutoTheme.Text
                .mnuMarkAskTheme.Text = .mnuAllAutoTheme.Text
                .mnuFilterAutoTheme.Text = .mnuAllAutoTheme.Text
                .mnuFilterAskTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayAllAutoTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayAllAskTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayMissAutoTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayMissAskTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayNewAutoTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayNewAskTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayMarkAutoTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayMarkAskTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayFilterAutoTheme.Text = .mnuAllAutoTheme.Text
                .cmnuTrayFilterAskTheme.Text = .mnuAllAutoTheme.Text
                .cmnuMovieReSelAskTheme.Text = .mnuAllAutoTheme.Text
                .cmnuMovieReSelAutoTheme.Text = .mnuAllAutoTheme.Text

                ' Scrape Media Content: Trailer
                .mnuAllAutoTrailer.Text = Master.eLang.GetString(75, "Trailer Only")
                .mnuAllAskTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuMissAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuMissAskTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuNewAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuNewAskTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuMarkAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuMarkAskTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuFilterAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .mnuFilterAskTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayAllAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayAllAskTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayMissAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayMissAskTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayNewAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayNewAskTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayMarkAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayMarkAskTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayFilterAutoTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuTrayFilterAskTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuMovieReSelAskTrailer.Text = .mnuAllAutoTrailer.Text
                .cmnuMovieReSelAutoTrailer.Text = .mnuAllAutoTrailer.Text

                ' others
                .btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                .btnClearFilters.Text = Master.eLang.GetString(37, "Clear Filters")
                .btnIMDBRating.Tag = String.Empty
                .btnIMDBRating.Text = Master.eLang.GetString(400, "Rating")
                .btnMarkAll.Text = Master.eLang.GetString(35, "Mark All")
                .btnMetaDataRefresh.Text = Master.eLang.GetString(58, "Refresh")
                .btnSortDate.Tag = String.Empty
                .btnSortDate.Text = Master.eLang.GetString(601, "Date Added")
                .btnSortTitle.Tag = String.Empty
                .btnSortTitle.Text = Master.eLang.GetString(642, "Sort Title")
                .chkFilterDupe.Text = Master.eLang.GetString(41, "Duplicates")
                .chkFilterLock.Text = Master.eLang.GetString(43, "Locked")
                .chkFilterMark.Text = Master.eLang.GetString(48, "Marked")
                .chkFilterMissing.Text = Master.eLang.GetString(40, "Missing Items")
                .chkFilterNew.Text = Master.eLang.GetString(47, "New")
                .chkFilterTolerance.Text = Master.eLang.GetString(39, "Out of Tolerance")
                .cmnuEpisodeChange.Text = Master.eLang.GetString(772, "Change Episode")
                .cmnuEpisodeEdit.Text = Master.eLang.GetString(656, "Edit Episode")
                .cmnuEpisodeEdit.Text = Master.eLang.GetString(656, "Edit Episode")
                .cmnuEpisodeLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuEpisodeMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuEpisodeReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuEpisodeRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuEpisodeRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuEpisodeRemoveFromDisk.Text = Master.eLang.GetString(773, "Delete Episode")
                .cmnuEpisodeRescrape.Text = Master.eLang.GetString(147, "(Re)Scrape Episode")
                .cmnuMovieChange.Text = Master.eLang.GetString(32, "Change Movie")
                .cmnuMovieEdit.Text = Master.eLang.GetString(25, "Edit Movie")
                .cmnuMovieEditMetaData.Text = Master.eLang.GetString(603, "Edit Meta Data")
                .cmnuMovieGenres.Text = Master.eLang.GetString(20, "Genre")
                .cmnuMovieGenresAdd.Text = Master.eLang.GetString(28, "Add")
                .cmnuMovieGenresRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuMovieGenresSet.Text = Master.eLang.GetString(29, "Set")
                .cmnuMovieGenresTitle.Text = Master.eLang.GetString(27, ">> Select Genre <<")
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
                .cmnuMovieRemoveFromDisc.Text = Master.eLang.GetString(34, "Delete Movie")
                .cmnuMovieRescrape.Text = Master.eLang.GetString(163, "(Re)Scrape Movie")
                .cmnuMovieReSel.Text = Master.eLang.GetString(31, "(Re)Scrape Selected Movies")
                .cmnuMovieUpSelCert.Text = Master.eLang.GetString(722, "Certification")
                .cmnuMovieUpSelCountry.Text = Master.eLang.GetString(301, "Country")
                .cmnuMovieUpSelDirector.Text = Master.eLang.GetString(62, "Director")
                .cmnuMovieUpSelMPAA.Text = Master.eLang.GetString(401, "MPAA")
                .cmnuMovieUpSelOutline.Text = Master.eLang.GetString(64, "Plot Outline")
                .cmnuMovieUpSelPlot.Text = Master.eLang.GetString(65, "Plot")
                .cmnuMovieUpSelProducers.Text = Master.eLang.GetString(393, "Producers")
                .cmnuMovieUpSelRating.Text = String.Concat(Master.eLang.GetString(400, "Rating"), " / ", Master.eLang.GetString(399, "Votes"))
                .cmnuMovieUpSelRelease.Text = Master.eLang.GetString(57, "Release Date")
                .cmnuMovieUpSelStudio.Text = Master.eLang.GetString(395, "Studio")
                .cmnuMovieUpSelTagline.Text = Master.eLang.GetString(397, "Tagline")
                .cmnuMovieUpSelTitle.Text = Master.eLang.GetString(21, "Title")
                .cmnuMovieUpSelTop250.Text = Master.eLang.GetString(591, "Top 250")
                .cmnuMovieUpSelTrailer.Text = Master.eLang.GetString(151, "Trailer")
                .cmnuMovieUpSelWriter.Text = Master.eLang.GetString(777, "Writer")
                .cmnuMovieUpSelYear.Text = Master.eLang.GetString(278, "Year")
                .cmnuMovieTitle.Text = Master.eLang.GetString(21, "Title")
                .cmnuMovieUpSel.Text = Master.eLang.GetString(1126, "Update Single Data Field")
                .cmnuRemoveSeasonFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuSeasonChangeImages.Text = Master.eLang.GetString(770, "Change Images")
                .cmnuSeasonLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuSeasonMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuSeasonReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuSeasonRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuSeasonRemoveFromDisk.Text = Master.eLang.GetString(771, "Delete Season")
                .cmnuSeasonRescrape.Text = Master.eLang.GetString(146, "(Re)Scrape Season")
                .cmnuShowChange.Text = Master.eLang.GetString(767, "Change Show")
                .cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
                .cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
                .cmnuShowLanguage.Text = Master.eLang.GetString(1200, "Change Language")
                .cmnuShowLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuShowMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuShowReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuShowRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuShowRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuShowRemoveFromDisk.Text = Master.eLang.GetString(768, "Delete TV Show")
                .cmnuShowRefresh.Text = Master.eLang.GetString(1066, "Refresh Data")
                .cmnuShowRescrape.Text = Master.eLang.GetString(766, "(Re)Scrape Show")
                .cmnuTrayExit.Text = Master.eLang.GetString(2, "E&xit")
                .cmnuTrayScrape.Text = Master.eLang.GetString(67, "Scrape Media")
                .cmnuTraySettings.Text = Master.eLang.GetString(4, "&Settings...")
                .cmnuTrayTools.Text = Master.eLang.GetString(8, "&Tools")
                .cmnuTrayUpdate.Text = Master.eLang.GetString(82, "Update Library")
                .gbFilterGeneral.Text = Master.eLang.GetString(38, "General")
                .gbFilterModifier.Text = Master.eLang.GetString(44, "Modifier")
                .gbFilterSpecific.Text = Master.eLang.GetString(42, "Specific")
                .gbSort.Text = Master.eLang.GetString(600, "Extra Sorting")
                .lblActorsHeader.Text = Master.eLang.GetString(63, "Cast")
                .lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                .lblCertsHeader.Text = Master.eLang.GetString(56, "Certification(s)")
                .lblDirectorHeader.Text = Master.eLang.GetString(62, "Director")
                .lblFilePathHeader.Text = Master.eLang.GetString(60, "File Path")
                .lblFilter.Text = Master.eLang.GetString(52, "Filters")
                .lblFilterFileSource.Text = Master.eLang.GetString(579, "File Source:")
                .lblFilterGenre.Text = Master.eLang.GetString(51, "Genre:")
                .lblFilterGenres.Text = Master.eLang.GetString(20, "Genre")
                .lblFilterSource.Text = Master.eLang.GetString(50, "Source:")
                .lblFilterSources.Text = Master.eLang.GetString(602, "Sources")
                .lblFilterYear.Text = Master.eLang.GetString(49, "Year:")
                .lblGFilClose.Text = Master.eLang.GetString(19, "Close")
                .lblIMDBHeader.Text = Master.eLang.GetString(61, "IMDB ID")
                .lblInfoPanelHeader.Text = Master.eLang.GetString(66, "Info")
                .lblLoadSettings.Text = Master.eLang.GetString(484, "Loading Settings...")
                .lblMetaDataHeader.Text = Master.eLang.GetString(59, "Meta Data")
                .lblOutlineHeader.Text = Master.eLang.GetString(64, "Plot Outline")
                .lblPlotHeader.Text = Master.eLang.GetString(65, "Plot")
                .lblReleaseDateHeader.Text = Master.eLang.GetString(57, "Release Date")
                .lblSFilClose.Text = Master.eLang.GetString(19, "Close")
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
                .mnuMainToolsReloadMovies.Text = Master.eLang.GetString(18, "Re&load All Movies")
                .mnuMainToolsReloadMovieSets.Text = Master.eLang.GetString(12088, "Reload All MovieSets")
                .mnuMainToolsSetsManager.Text = Master.eLang.GetString(14, "Sets &Manager")
                .mnuMainToolsSortFiles.Text = Master.eLang.GetString(10, "&Sort Files Into Folders")
                .mnuUpdate.Text = Master.eLang.GetString(82, "Update Library")
                .mnuUpdateMovies.Text = Master.eLang.GetString(36, "Movies")
                .mnuUpdateShows.Text = Master.eLang.GetString(653, "TV Shows")
                .pnlFilterGenre.Tag = String.Empty
                .pnlFilterSource.Tag = String.Empty
                .rbFilterAnd.Text = Master.eLang.GetString(45, "And")
                .rbFilterOr.Text = Master.eLang.GetString(46, "Or")
                .tpMovies.Text = Master.eLang.GetString(36, "Movies")
                .tpMovieSets.Text = Master.eLang.GetString(366, "Sets")
                .tpTVShows.Text = Master.eLang.GetString(653, "TV")
                .tsbAutoPilot.Text = Master.eLang.GetString(67, "Scrape Media")
                .tsbMediaCenters.Text = Master.eLang.GetString(83, "Media Centers")
                .tslLoading.Text = Master.eLang.GetString(7, "Loading Media:")

                .cmnuEpisodeOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuMovieSetReload.Text = cmnuMovieReload.Text
                .cmnuSeasonOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuShowLanguageSet.Text = cmnuMovieGenresSet.Text
                .cmnuShowOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuTrayMediaCenters.Text = .tsbMediaCenters.Text
                .cmnuTrayToolsBackdrops.Text = .mnuMainToolsBackdrops.Text
                .cmnuTrayToolsCleanFiles.Text = .mnuMainToolsCleanFiles.Text
                .cmnuTrayToolsClearCache.Text = .mnuMainToolsClearCache.Text
                .cmnuTrayToolsOfflineHolder.Text = .mnuMainToolsOfflineHolder.Text
                .cmnuTrayToolsReloadMovies.Text = .mnuMainToolsReloadMovies.Text
                .cmnuTrayToolsSetsManager.Text = .mnuMainToolsSetsManager.Text
                .cmnuTrayToolsSortFiles.Text = .mnuMainToolsSortFiles.Text

                Dim TT As ToolTip = New System.Windows.Forms.ToolTip(.components)
                .tsbAutoPilot.ToolTipText = Master.eLang.GetString(84, "Scrape/download data from the internet for multiple movies.")
                .mnuUpdate.ToolTipText = Master.eLang.GetString(85, "Scans sources for new content and cleans database.")
                TT.SetToolTip(.btnMarkAll, Master.eLang.GetString(87, "Mark or Unmark all movies in the list."))
                TT.SetToolTip(.txtSearch, Master.eLang.GetString(88, "Search the movie titles by entering text here."))
                TT.SetToolTip(.btnPlay, Master.eLang.GetString(89, "Play the movie file with the system default media player."))
                TT.SetToolTip(.btnMetaDataRefresh, Master.eLang.GetString(90, "Rescan and save the meta data for the selected movie."))
                TT.SetToolTip(.chkFilterDupe, Master.eLang.GetString(91, "Display only movies that have duplicate IMDB IDs."))
                TT.SetToolTip(.chkFilterTolerance, Master.eLang.GetString(92, "Display only movies whose title matching is out of tolerance."))
                TT.SetToolTip(.chkFilterMissing, Master.eLang.GetString(93, "Display only movies that have items missing."))
                TT.SetToolTip(.chkFilterNew, Master.eLang.GetString(94, "Display only new movies."))
                TT.SetToolTip(.chkFilterMark, Master.eLang.GetString(95, "Display only marked movies."))
                TT.SetToolTip(.chkFilterLock, Master.eLang.GetString(96, "Display only locked movies."))
                TT.SetToolTip(.txtFilterSource, Master.eLang.GetString(97, "Display only movies from the selected source."))
                TT.SetToolTip(.cbFilterFileSource, Master.eLang.GetString(580, "Display only movies from the selected file source."))
                TT.Active = True

                .cbSearch.Items.Clear()
                .cbSearch.Items.AddRange(New Object() {Master.eLang.GetString(21, "Title"), Master.eLang.GetString(100, "Actor"), Master.eLang.GetString(233, "Role"), Master.eLang.GetString(62, "Director"), Master.eLang.GetString(729, "Credits")})

                If doTheme Then
                    Me.tTheme = New Theming
                    .ApplyTheme(If(Me.tcMain.SelectedIndex = 0, Theming.ThemeType.Movie, If(Me.tcMain.SelectedIndex = 1, Theming.ThemeType.MovieSet, Theming.ThemeType.Show)))
                End If

            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Updates the label indicating there is no information for the current item.
    ''' </summary>
    ''' <param name="ShowIt"><c>Boolean</c> indicating whether the panel should be shown or not</param>
    ''' <param name="tType"></param>
    ''' <remarks></remarks>
    Private Sub ShowNoInfo(ByVal ShowIt As Boolean, Optional ByVal tType As Integer = 0)
        If ShowIt Then
            Select Case tType
                Case 0
                    Me.lblNoInfo.Text = Master.eLang.GetString(55, "No Information is Available for This Movie")
                    If Not Me.currThemeType = Theming.ThemeType.Movie Then Me.ApplyTheme(Theming.ThemeType.Movie)
                Case 1
                    Me.lblNoInfo.Text = Master.eLang.GetString(651, "No Information is Available for This Show")
                    If Not Me.currThemeType = Theming.ThemeType.Show Then Me.ApplyTheme(Theming.ThemeType.Show)
                Case 2
                    Me.lblNoInfo.Text = Master.eLang.GetString(652, "No Information is Available for This Episode")
                    If Not Me.currThemeType = Theming.ThemeType.Episode Then Me.ApplyTheme(Theming.ThemeType.Episode)
                Case 3
                    Me.lblNoInfo.Text = Master.eLang.GetString(1154, "No Information is Available for This MovieSet")
                    If Not Me.currThemeType = Theming.ThemeType.MovieSet Then Me.ApplyTheme(Theming.ThemeType.MovieSet)
                Case Else
                    logger.Warn("Invalid media type <{0}>", tType)
            End Select
        End If

        Me.pnlNoInfo.Visible = ShowIt
    End Sub
    ''' <summary>
    ''' Triggers the display of the Settings dialog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowSettings()
        Using dSettings As New dlgSettings
            Me.Invoke(New MySettingsShow(AddressOf SettingsShow), dSettings)
        End Using
    End Sub

    Private Sub SourceSubClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SourceName As String = DirectCast(sender, ToolStripItem).Tag.ToString

        'Remove any previous scrape as there is no warranty that the new dataset will match with the old one
        Dim aPath As String = FileUtils.Common.ReturnSettingsFile("Settings", "ScraperStatus.dat")
        If File.Exists(aPath) Then
            Try
                File.Delete(aPath)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Throw
            End Try
        End If

        Me.LoadMedia(New Structures.Scans With {.Movies = True}, SourceName)
    End Sub

    Private Sub tabsMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcMain.SelectedIndexChanged
        Me.ClearInfo()
        Me.ShowNoInfo(False)
        ModulesManager.Instance.RuntimeObjects.MediaTabSelected = tcMain.SelectedIndex
        Select Case tcMain.SelectedIndex
            Case 0 'Movies list
                Me.mnuMainTools.Enabled = True
                Me.cmnuTrayTools.Enabled = True
                Me.pnlFilter.Visible = True
                Me.pnlListTop.Height = 56
                Me.btnMarkAll.Visible = True
                Me.scTV.Visible = False
                Me.dgvMovieSets.Visible = False
                Me.dgvMovies.Visible = True
                Me.ApplyTheme(Theming.ThemeType.Movie)
                If Me.bwLoadEpInfo.IsBusy Then Me.bwLoadEpInfo.CancelAsync()
                If Me.bwLoadSeasonInfo.IsBusy Then Me.bwLoadSeasonInfo.CancelAsync()
                If Me.bwLoadShowInfo.IsBusy Then Me.bwLoadShowInfo.CancelAsync()
                If Me.bwLoadMovieSetInfo.IsBusy Then Me.bwLoadMovieSetInfo.CancelAsync()
                If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
                If Me.dgvMovies.RowCount > 0 Then
                    Me.prevMovieRow = -1

                    Me.dgvMovies.CurrentCell = Nothing
                    Me.dgvMovies.ClearSelection()
                    Me.dgvMovies.Rows(0).Selected = True
                    Me.dgvMovies.CurrentCell = Me.dgvMovies.Rows(0).Cells(3)

                    Me.dgvMovies.Focus()
                End If

            Case 1 'MovieSets list
                Me.mnuMainTools.Enabled = True
                Me.cmnuTrayTools.Enabled = True
                Me.pnlFilter.Visible = False
                Me.pnlListTop.Height = 56
                Me.btnMarkAll.Visible = False
                Me.scTV.Visible = False
                Me.dgvMovies.Visible = False
                Me.dgvMovieSets.Visible = True
                Me.ApplyTheme(Theming.ThemeType.MovieSet)
                If Me.bwLoadMovieInfo.IsBusy Then Me.bwLoadMovieInfo.CancelAsync()
                If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
                If Me.bwLoadEpInfo.IsBusy Then Me.bwLoadEpInfo.CancelAsync()
                If Me.bwLoadSeasonInfo.IsBusy Then Me.bwLoadSeasonInfo.CancelAsync()
                If Me.bwLoadShowInfo.IsBusy Then Me.bwLoadShowInfo.CancelAsync()
                If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
                If Me.dgvMovieSets.RowCount > 0 Then
                    Me.prevMovieSetRow = -1

                    Me.dgvMovieSets.CurrentCell = Nothing
                    Me.dgvMovieSets.ClearSelection()
                    Me.dgvMovieSets.Rows(0).Selected = True
                    Me.dgvMovieSets.CurrentCell = Me.dgvMovieSets.Rows(0).Cells(1)

                    Me.dgvMovieSets.Focus()
                End If

            Case 2 'TV Shows list
                Me.mnuMainTools.Enabled = True
                Me.cmnuTrayTools.Enabled = False
                Me.tsbAutoPilot.Enabled = False
                Me.cmnuTrayScrape.Enabled = False
                Me.dgvMovies.Visible = False
                Me.dgvMovieSets.Visible = False
                Me.pnlFilter.Visible = False
                Me.pnlListTop.Height = 23
                Me.btnMarkAll.Visible = False
                Me.scTV.Visible = True
                Me.ApplyTheme(Theming.ThemeType.Show)
                If Me.bwLoadMovieInfo.IsBusy Then Me.bwLoadMovieInfo.CancelAsync()
                If Me.bwLoadMovieSetInfo.IsBusy Then Me.bwLoadMovieSetInfo.CancelAsync()
                If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
                If Me.dgvTVShows.RowCount > 0 Then
                    Me.prevShowRow = -1
                    Me.currList = 0

                    Me.dgvTVShows.CurrentCell = Nothing
                    Me.dgvTVShows.ClearSelection()
                    Me.dgvTVShows.Rows(0).Selected = True
                    Me.dgvTVShows.CurrentCell = Me.dgvTVShows.Rows(0).Cells(1)

                    Me.dgvTVShows.Focus()

                End If
        End Select
    End Sub
    ''' <summary>
    ''' Just some crappy animation to make the GUI slightly more interesting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmrAni_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrAni.Tick
        Try
            If Master.eSettings.GeneralInfoPanelAnim Then
                If Me.aniRaise Then
                    Me.pnlInfoPanel.Height += 5
                Else
                    Me.pnlInfoPanel.Height -= 5
                End If
            Else
                Select Case If(Me.tcMain.SelectedIndex = 0, Me.aniMovieType, If(Me.tcMain.SelectedIndex = 1, Me.aniMovieSetType, Me.aniShowType))
                    Case 0
                        Me.pnlInfoPanel.Height = 25

                    Case 1
                        Me.pnlInfoPanel.Height = Me.IPMid

                    Case 2
                        Me.pnlInfoPanel.Height = Me.IPUp

                End Select
            End If

            Me.MoveGenres()
            Me.MoveMPAA()

            Dim aType As Integer = If(Me.tcMain.SelectedIndex = 0, Me.aniMovieType, If(Me.tcMain.SelectedIndex = 1, Me.aniMovieSetType, Me.aniShowType))
            Select Case aType
                Case 0
                    If Me.pnlInfoPanel.Height = 25 Then
                        Me.tmrAni.Stop()
                        Me.btnDown.Enabled = False
                        Me.btnMid.Enabled = True
                        Me.btnUp.Enabled = True
                    End If
                Case 1
                    If Me.pnlInfoPanel.Height = Me.IPMid Then
                        Me.tmrAni.Stop()
                        Me.btnMid.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnUp.Enabled = True
                    End If
                Case 2
                    If Me.pnlInfoPanel.Height = Me.IPUp Then
                        Me.tmrAni.Stop()
                        Me.btnUp.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnMid.Enabled = True
                    End If
            End Select

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Just some crappy animation to make the GUI slightly more interesting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmrFilterAni_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrFilterAni.Tick
        Try

            Dim pHeight As Integer = Functions.Quantize(Me.gbFilterSpecific.Height + Me.lblFilter.Height + 15, 5)

            If Master.eSettings.GeneralInfoPanelAnim Then
                If Me.aniFilterRaise Then
                    Me.pnlFilter.Height += 5
                Else
                    Me.pnlFilter.Height -= 5
                End If
            Else
                If Me.aniFilterRaise Then
                    Me.pnlFilter.Height = pHeight
                Else
                    Me.pnlFilter.Height = 25
                End If
            End If

            If Me.pnlFilter.Height = 25 Then
                Me.tmrFilterAni.Stop()
                Me.btnFilterUp.Enabled = True
                Me.btnFilterDown.Enabled = False
            ElseIf Me.pnlFilter.Height = pHeight Then
                Me.tmrFilterAni.Stop()
                Me.btnFilterUp.Enabled = False
                Me.btnFilterDown.Enabled = True
            End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub tmrLoadEp_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoadEp.Tick
        Me.tmrWaitEp.Stop()
        Me.tmrLoadEp.Stop()
        Try

            If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then

                If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVEpisodes.SelectedRows.Count))
                ElseIf Me.dgvTVEpisodes.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvTVEpisodes.SelectedRows(0).Cells(3).Value.ToString)
                End If

                Me.SelectEpisodeRow(Me.dgvTVEpisodes.SelectedRows(0).Index)
            End If
        Catch
        End Try
    End Sub

    Private Sub tmrLoadSeason_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoadSeason.Tick
        Me.tmrWaitSeason.Stop()
        Me.tmrLoadSeason.Stop()
        Try
            If Me.dgvTVSeasons.SelectedRows.Count > 0 Then

                If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVSeasons.SelectedRows.Count))
                ElseIf Me.dgvMovies.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvTVSeasons.SelectedRows(0).Cells(1).Value.ToString)
                End If

                Me.SelectSeasonRow(Me.dgvTVSeasons.SelectedRows(0).Index)
            End If
        Catch
        End Try
    End Sub

    Private Sub tmrLoadShow_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoadShow.Tick
        Me.tmrWaitShow.Stop()
        Me.tmrLoadShow.Stop()
        Try
            If Me.dgvTVShows.SelectedRows.Count > 0 Then

                If Me.dgvTVShows.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVShows.SelectedRows.Count))
                ElseIf Me.dgvTVShows.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvTVShows.SelectedRows(0).Cells(1).Value.ToString)
                End If

                Me.SelectShowRow(Me.dgvTVShows.SelectedRows(0).Index)
            End If
        Catch
        End Try
    End Sub

    Private Sub tmrLoadMovie_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoadMovie.Tick
        Me.tmrWaitMovie.Stop()
        Me.tmrLoadMovie.Stop()
        Try
            If Me.dgvMovies.SelectedRows.Count > 0 Then

                If Me.dgvMovies.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovies.SelectedRows.Count))
                ElseIf Me.dgvMovies.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvMovies.SelectedRows(0).Cells(1).Value.ToString)
                End If

                Me.SelectMovieRow(Me.dgvMovies.SelectedRows(0).Index)
            End If
        Catch
        End Try
    End Sub

    Private Sub tmrLoadMovieSet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoadMovieSet.Tick
        Me.tmrWaitMovieSet.Stop()
        Me.tmrLoadMovieSet.Stop()
        Try
            If Me.dgvMovieSets.SelectedRows.Count > 0 Then

                If Me.dgvMovieSets.SelectedRows.Count > 1 Then
                    Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovieSets.SelectedRows.Count))
                ElseIf Me.dgvMovieSets.SelectedRows.Count = 1 Then
                    Me.SetStatus(Me.dgvMovieSets.SelectedRows(0).Cells(1).Value.ToString)
                End If

                Me.SelectMovieSetRow(Me.dgvMovieSets.SelectedRows(0).Index)
            End If
        Catch
        End Try
    End Sub

    Private Sub tmrSearchWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait.Tick
        Me.tmrSearchMovie.Enabled = False
        If Me.prevText = Me.currText Then
            Me.tmrSearchMovie.Enabled = True
        Else
            Me.prevText = Me.currText
        End If
    End Sub

    Private Sub tmrSearch_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchMovie.Tick
        Me.tmrSearchWait.Enabled = False
        Me.tmrSearchMovie.Enabled = False
        bDoingSearch = True
        Try
            If Not String.IsNullOrEmpty(Me.txtSearch.Text) Then
                Me.FilterArray.Remove(Me.filSearch)
                Me.filSearch = String.Empty

                Select Case Me.cbSearch.Text
                    Case Master.eLang.GetString(21, "Title")
                        Me.filSearch = String.Concat("ListTitle LIKE '%", Me.txtSearch.Text, "%'")
                        Me.FilterArray.Add(Me.filSearch)
                    Case Master.eLang.GetString(100, "Actor")
                        Me.filSearch = Me.txtSearch.Text
                    Case Master.eLang.GetString(233, "Role")
                        Me.filSearch = Me.txtSearch.Text
                    Case Master.eLang.GetString(62, "Director")
                        Me.filSearch = String.Concat("Director LIKE '%", Me.txtSearch.Text, "%'")
                        Me.FilterArray.Add(Me.filSearch)
                    Case Master.eLang.GetString(729, "Credits")
                        Me.filSearch = String.Concat("Credits LIKE '%", Me.txtSearch.Text, "%'")
                        Me.FilterArray.Add(Me.filSearch)

                End Select

                Me.RunFilter(Me.cbSearch.Text = Master.eLang.GetString(100, "Actor") OrElse Me.cbSearch.Text = Master.eLang.GetString(233, "Role"))

            Else
                If Not String.IsNullOrEmpty(Me.filSearch) Then
                    Me.FilterArray.Remove(Me.filSearch)
                    Me.filSearch = String.Empty
                End If
                Me.RunFilter(True)
            End If
        Catch
        End Try
    End Sub

    Private Sub tmrWaitEp_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWaitEp.Tick
        Me.tmrLoadSeason.Stop()
        Me.tmrLoadShow.Stop()
        Me.tmrWaitSeason.Stop()
        Me.tmrWaitShow.Stop()

        If Not Me.prevEpRow = Me.currEpRow Then
            Me.prevEpRow = Me.currEpRow
            Me.tmrWaitEp.Stop()
            Me.tmrLoadEp.Start()
        Else
            Me.tmrLoadEp.Stop()
            Me.tmrWaitEp.Stop()
        End If
    End Sub

    Private Sub tmrWaitSeason_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWaitSeason.Tick
        Me.tmrLoadShow.Stop()
        Me.tmrLoadEp.Stop()
        Me.tmrWaitShow.Stop()
        Me.tmrWaitEp.Stop()

        If Not Me.prevSeasonRow = Me.currSeasonRow Then
            Me.prevSeasonRow = Me.currSeasonRow
            Me.tmrWaitSeason.Stop()
            Me.tmrLoadSeason.Start()
        Else
            Me.tmrLoadSeason.Stop()
            Me.tmrWaitSeason.Stop()
        End If
    End Sub

    Private Sub tmrWaitShow_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWaitShow.Tick
        Me.tmrLoadSeason.Stop()
        Me.tmrLoadEp.Stop()
        Me.tmrWaitSeason.Stop()
        Me.tmrWaitEp.Stop()

        If Not Me.prevShowRow = Me.currShowRow Then
            Me.prevShowRow = Me.currShowRow
            Me.tmrWaitShow.Stop()
            Me.tmrLoadShow.Start()
        Else
            Me.tmrLoadShow.Stop()
            Me.tmrWaitShow.Stop()
        End If
    End Sub

    Private Sub tmrWaitMovie_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWaitMovie.Tick
        If Not Me.prevMovieRow = Me.currMovieRow Then
            Me.prevMovieRow = Me.currMovieRow
            Me.tmrWaitMovie.Stop()
            Me.tmrLoadMovie.Start()
        Else
            Me.tmrLoadMovie.Stop()
            Me.tmrWaitMovie.Stop()
        End If
    End Sub

    Private Sub tmrWaitMovieSet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWaitMovieSet.Tick
        If Not Me.prevMovieSetRow = Me.currMovieSetRow Then
            Me.prevMovieSetRow = Me.currMovieSetRow
            Me.tmrWaitMovieSet.Stop()
            Me.tmrLoadMovieSet.Start()
        Else
            Me.tmrLoadMovieSet.Stop()
            Me.tmrWaitMovieSet.Stop()
        End If
    End Sub

    Private Sub mnuUpdate_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUpdate.ButtonClick
        Me.LoadMedia(New Structures.Scans With {.Movies = True, .TV = True})
    End Sub

    Private Sub TVScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)
        Select Case eType
            Case Enums.ScraperEventType_TV.LoadingEpisodes
                Me.tspbLoading.Style = ProgressBarStyle.Marquee
                Me.tslLoading.Text = Master.eLang.GetString(756, "Loading All Episodes:")
                Me.tspbLoading.Visible = True
                Me.tslLoading.Visible = True
            Case Enums.ScraperEventType_TV.SavingStarted
                Me.tspbLoading.Style = ProgressBarStyle.Marquee
                Me.tslLoading.Text = Master.eLang.GetString(757, "Saving All Images:")
                Me.tspbLoading.Visible = True
                Me.tslLoading.Visible = True
            Case Enums.ScraperEventType_TV.ScraperDone
                Me.RefreshShow(Master.currShow.ShowID, False, False, False, True)

                Me.tspbLoading.Visible = False
                Me.tslLoading.Visible = False
                'Me.tslStatus.Visible = False

                Me.SetControlsEnabled(True)

            Case Enums.ScraperEventType_TV.Searching
                Me.tspbLoading.Style = ProgressBarStyle.Marquee
                Me.tslLoading.Text = Master.eLang.GetString(758, "Searching theTVDB:")
                Me.tspbLoading.Visible = True
                Me.tslLoading.Visible = True
            Case Enums.ScraperEventType_TV.SelectImages
                Me.tspbLoading.Style = ProgressBarStyle.Marquee
                Me.tslLoading.Text = Master.eLang.GetString(759, "Select Images:")
                Me.tspbLoading.Visible = True
                Me.tslLoading.Visible = True
            Case Enums.ScraperEventType_TV.StartingDownload
                Me.tspbLoading.Style = ProgressBarStyle.Marquee
                Me.tslLoading.Text = Master.eLang.GetString(760, "Downloading Show Zip:")
                Me.tspbLoading.Visible = True
                Me.tslLoading.Visible = True
            Case Enums.ScraperEventType_TV.SaveAuto
                Me.tspbLoading.Style = ProgressBarStyle.Marquee
                Select Case iProgress
                    Case 0 ' show
                        Me.SetShowListItemAfterEdit(Convert.ToInt32(Master.currShow.ShowID), Me.dgvTVShows.SelectedRows(0).Index)
                        ModulesManager.Instance.TVSaveImages()
                    Case Else
                        logger.Warn("Unhandled TVScraperEventType.SaveAuto <{0}>", iProgress)
                End Select

            Case Enums.ScraperEventType_TV.Verifying
                Me.tspbLoading.Style = ProgressBarStyle.Marquee

                Select Case iProgress
                    Case 0 ' show
                        Me.tslLoading.Text = Master.eLang.GetString(761, "Verifying TV Show:")
                        Me.tspbLoading.Visible = True
                        Me.tslLoading.Visible = True
                        Using dEditShow As New dlgEditShow
                            If dEditShow.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                Me.SetShowListItemAfterEdit(Convert.ToInt32(Master.currShow.ShowID), Me.dgvTVShows.SelectedRows(0).Index)
                                ModulesManager.Instance.TVSaveImages()
                            Else
                                Me.tspbLoading.Visible = False
                                Me.tslLoading.Visible = False

                                Me.LoadShowInfo(Convert.ToInt32(Master.currShow.ShowID))

                                Me.SetControlsEnabled(True)
                            End If
                        End Using
                    Case 1 ' episode
                        Me.tslLoading.Text = Master.eLang.GetString(762, "Verifying TV Episode:")
                        Me.tspbLoading.Visible = True
                        Me.tslLoading.Visible = True
                        Using dEditEp As New dlgEditEpisode
                            AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEp.GenericRunCallBack
                            If dEditEp.ShowDialog = Windows.Forms.DialogResult.OK Then
                                Me.RefreshEpisode(Master.currShow.EpID)
                            End If
                            RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditEp.GenericRunCallBack
                        End Using
                        Me.tspbLoading.Visible = False
                        Me.tslLoading.Visible = False
                        Me.SetControlsEnabled(True)
                    Case Else
                        logger.Warn("Unhandled TVScraperEventType.Verifying <{0}>", iProgress)
                End Select

            Case Enums.ScraperEventType_TV.Progress
                Select Case Parameter.ToString
                    Case "max"
                        Me.tspbLoading.Value = 0
                        Me.tspbLoading.Style = ProgressBarStyle.Continuous
                        Me.tspbLoading.Maximum = iProgress
                    Case "progress"
                        Me.tspbLoading.Value = iProgress
                    Case Else
                        logger.Warn("Unhandled TVScraperEventType.Progress <{0}>", iProgress)
                End Select
                Me.tspbLoading.Visible = True
                Me.tslLoading.Visible = True

            Case Enums.ScraperEventType_TV.Cancelled
                Me.tspbLoading.Visible = False
                Me.tslLoading.Visible = False

                Me.LoadShowInfo(Convert.ToInt32(Master.currShow.ShowID))

                Me.SetControlsEnabled(True)
            Case Else
                logger.Warn("Unhandled TVScraperEventType <{0}>", eType)
        End Select
    End Sub

    Private Sub TVSourceSubClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SourceName As String = DirectCast(sender, ToolStripItem).Tag.ToString

        'Remove any previous scrape as there is no warranty that the new dataset will match with the old one
        Dim aPath As String = FileUtils.Common.ReturnSettingsFile("Settings", "ScraperStatus.dat")
        If File.Exists(aPath) Then
            Try
                File.Delete(aPath)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Throw
            End Try
        End If


        Me.LoadMedia(New Structures.Scans With {.TV = True}, SourceName)
    End Sub

    Private Sub txtFilterGenre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterGenre.Click
        Me.pnlFilterGenre.Location = New Point(Me.gbFilterSpecific.Left + Me.txtFilterGenre.Left, (Me.pnlFilter.Top + Me.txtFilterGenre.Top + Me.gbFilterSpecific.Top) - Me.pnlFilterGenre.Height)
        If Me.pnlFilterGenre.Visible Then
            Me.pnlFilterGenre.Visible = False
        ElseIf Not Me.pnlFilterGenre.Tag.ToString = "NO" Then
            Me.pnlFilterGenre.Tag = String.Empty
            Me.pnlFilterGenre.Visible = True
            Me.clbFilterGenres.Focus()
        Else
            Me.pnlFilterGenre.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterSource_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterSource.Click
        Me.pnlFilterSource.Location = New Point(Me.gbFilterSpecific.Left + Me.txtFilterSource.Left, (Me.pnlFilter.Top + Me.txtFilterSource.Top + Me.gbFilterSpecific.Top) - Me.pnlFilterSource.Height)
        If Me.pnlFilterSource.Visible Then
            Me.pnlFilterSource.Visible = False
        ElseIf Not Me.pnlFilterSource.Tag.ToString = "NO" Then
            Me.pnlFilterSource.Tag = String.Empty
            Me.pnlFilterSource.Visible = True
            Me.clbFilterSource.Focus()
        Else
            Me.pnlFilterSource.Tag = String.Empty
        End If
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        e.Handled = Not StringUtils.AlphaNumericOnly(e.KeyChar, True)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Me.dgvMovies.Focus()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Me.currText = Me.txtSearch.Text

        Me.tmrSearchWait.Enabled = False
        Me.tmrSearchMovie.Enabled = False
        Me.tmrSearchWait.Enabled = True
    End Sub

    Private Sub VersionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpVersions.Click
        ModulesManager.Instance.GetVersions()
    End Sub

    Private Sub WikiStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpWiki.Click
        Functions.Launch(My.Resources.urlEmberWiki)
    End Sub

    Private Sub tmrAppExit_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAppExit.Tick
        tmrAppExit.Enabled = False
        Me.Close()
    End Sub

    Private Sub CheckUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpUpdate.Click
        If Functions.CheckNeedUpdate() Then
            Using dNewVer As New dlgNewVersion
                If dNewVer.ShowDialog() = Windows.Forms.DialogResult.Abort Then
                    tmrAppExit.Enabled = True
                    CloseApp = True
                End If
            End Using
        Else
            MsgBox(Master.eLang.GetString(851, "No Updates at this time"), MsgBoxStyle.OkOnly, "Updates")
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
            Me.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblIMDBHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblIMDBHeader.MouseLeave
        If Not lblIMDBHeader.Tag Is Nothing Then
            lblIMDBHeader.ForeColor = DirectCast(lblIMDBHeader.Tag, Color)
            Me.Cursor = Cursors.Default
            lblIMDBHeader.Tag = Nothing
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
            Dim aBit As String = Master.eLang.GetString(1008, "x64")
            If Master.is32Bit Then
                aBit = Master.eLang.GetString(1007, "x86")
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
                        Me.Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), System.String.Format(Master.eLang.GetString(1009, "{0} - (New version available!)"), VersionNumber), Color.DarkRed)
                    Else
                        'Ember already up to date!
                        Me.Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), VersionNumber, Color.DarkGreen)
                    End If
                End If
                'if no github query possible, than simply display Ember version on form
            Else
                Me.Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), VersionNumber, Color.DarkBlue)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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

        Dim ID As Integer
        Dim IsTV As Boolean
        Dim Movie As Structures.DBMovie
        Dim MovieSet As Structures.DBMovieSet
        Dim Options As Structures.ScrapeOptions_Movie
        Dim Options_MovieSet As Structures.ScrapeOptions_MovieSet
        Dim Path As String
        Dim pURL As String
        Dim scrapeType As Enums.ScrapeType
        Dim Season As Integer
        Dim setEnabled As Boolean
        Dim TVShow As Structures.DBTV
        Dim SetName As String

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim fileInfo As String
        Dim IsTV As Boolean
        Dim Movie As Structures.DBMovie
        Dim MovieSet As Structures.DBMovieSet
        Dim Options As Structures.ScrapeOptions_Movie
        Dim Options_MovieSet As Structures.ScrapeOptions_MovieSet
        Dim Path As String
        Dim Result As Image
        Dim scrapeType As Enums.ScrapeType
        Dim setEnabled As Boolean
        Dim TVShow As Structures.DBTV

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class