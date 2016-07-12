Imports EmberAPI
Imports System.IO

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

Public Class frmSettingsHolder

#Region "Fields"

    Private _fDummyMultiEpisode As New FileFolderRenamer.FileRename
    Private _fDummyMultiSeason As New FileFolderRenamer.FileRename
    Private _fDummySingleEpisode As New FileFolderRenamer.FileRename
    Private _fDummySingleMovie As New FileFolderRenamer.FileRename

#End Region 'Fields

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal State As Boolean)

    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub CreateDummies()
        _fDummyMultiEpisode.Clear()
        _fDummyMultiEpisode.AudioChannels = "2"
        _fDummyMultiEpisode.AudioCodec = "mp3"
        _fDummyMultiEpisode.BasePath = ""
        _fDummyMultiEpisode.Collection = ""
        _fDummyMultiEpisode.Country = ""
        _fDummyMultiEpisode.Director = ""
        _fDummyMultiEpisode.DirExist = False
        _fDummyMultiEpisode.FileExist = False
        _fDummyMultiEpisode.FileName = "OldFileName"
        _fDummyMultiEpisode.Genre = "Comedy / Lovestory"
        _fDummyMultiEpisode.ID = -1
        _fDummyMultiEpisode.IMDB = ""
        _fDummyMultiEpisode.IsBDMV = False
        _fDummyMultiEpisode.IsLocked = False
        _fDummyMultiEpisode.IsMultiEpisode = True
        _fDummyMultiEpisode.IsRenamed = False
        _fDummyMultiEpisode.IsSingle = True
        _fDummyMultiEpisode.IsVideo_TS = False
        _fDummyMultiEpisode.ListTitle = "Big Bang Theory, The"
        _fDummyMultiEpisode.MPAA = "TV-14"
        _fDummyMultiEpisode.MultiViewCount = "3d"
        _fDummyMultiEpisode.MultiViewLayout = "Side by Side (left eye first)"
        _fDummyMultiEpisode.NewFileName = ""
        _fDummyMultiEpisode.NewPath = ""
        _fDummyMultiEpisode.OldPath = ""
        _fDummyMultiEpisode.OriginalTitle = ""
        _fDummyMultiEpisode.Parent = "OldDirectoryName"
        _fDummyMultiEpisode.Path = ""
        _fDummyMultiEpisode.Rating = "7.3"
        _fDummyMultiEpisode.Resolution = "720p"
        _fDummyMultiEpisode.ShortStereoMode = "sbs"
        _fDummyMultiEpisode.ShowTitle = "The Big Bang Theory"
        _fDummyMultiEpisode.SortTitle = "Big Bang Theory"
        _fDummyMultiEpisode.StereoMode = "left_right"
        _fDummyMultiEpisode.Title = "Pilot"
        _fDummyMultiEpisode.TVDBID = "58056"
        _fDummyMultiEpisode.VideoCodec = "xvid"
        _fDummyMultiEpisode.VideoSource = "dvd"
        _fDummyMultiEpisode.Year = "2007"
        Dim dMEpisode1 As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dMEpisode2 As New FileFolderRenamer.Episode With {.ID = 2, .Episode = 2, .Title = "The Big Bran Hypothesis"}
        Dim dMEpisodeList As New List(Of FileFolderRenamer.Episode)
        dMEpisodeList.Add(dMEpisode1)
        dMEpisodeList.Add(dMEpisode2)
        _fDummyMultiEpisode.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dMEpisodeList})

        _fDummyMultiSeason.Clear()
        _fDummyMultiSeason.AudioChannels = "2"
        _fDummyMultiSeason.AudioCodec = "mp3"
        _fDummyMultiSeason.BasePath = ""
        _fDummyMultiSeason.Collection = ""
        _fDummyMultiSeason.Country = ""
        _fDummyMultiSeason.Director = ""
        _fDummyMultiSeason.DirExist = False
        _fDummyMultiSeason.FileExist = False
        _fDummyMultiSeason.FileName = "OldFileName"
        _fDummyMultiSeason.Genre = "Comedy / Lovestory"
        _fDummyMultiSeason.ID = -1
        _fDummyMultiSeason.IMDB = ""
        _fDummyMultiSeason.IsBDMV = False
        _fDummyMultiSeason.IsLocked = False
        _fDummyMultiSeason.IsMultiEpisode = True
        _fDummyMultiSeason.IsRenamed = False
        _fDummyMultiSeason.IsSingle = True
        _fDummyMultiSeason.IsVideo_TS = False
        _fDummyMultiSeason.ListTitle = "Big Bang Theory, The"
        _fDummyMultiSeason.MPAA = "TV-14"
        _fDummyMultiSeason.MultiViewCount = "3d"
        _fDummyMultiSeason.MultiViewLayout = "Side by Side (left eye first)"
        _fDummyMultiSeason.NewFileName = ""
        _fDummyMultiSeason.NewPath = ""
        _fDummyMultiSeason.OldPath = ""
        _fDummyMultiSeason.OriginalTitle = ""
        _fDummyMultiSeason.Parent = "OldDirectoryName"
        _fDummyMultiSeason.Path = ""
        _fDummyMultiSeason.Rating = "7.3"
        _fDummyMultiSeason.Resolution = "720p"
        _fDummyMultiSeason.ShortStereoMode = "sbs"
        _fDummyMultiSeason.ShowTitle = "The Big Bang Theory"
        _fDummyMultiSeason.SortTitle = "Big Bang Theory"
        _fDummyMultiSeason.StereoMode = "left_right"
        _fDummyMultiSeason.Title = "Pilot"
        _fDummyMultiSeason.TVDBID = "58056"
        _fDummyMultiSeason.VideoCodec = "xvid"
        _fDummyMultiSeason.VideoSource = "dvd"
        _fDummyMultiSeason.Year = "2007"
        Dim dMSEpisode1 As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dMSEpisode2 As New FileFolderRenamer.Episode With {.ID = 2, .Episode = 2, .Title = "The Big Bran Hypothesis"}
        Dim dMSEpisode3 As New FileFolderRenamer.Episode With {.ID = 3, .Episode = 1, .Title = "The Bad Fish Paradigm"}
        Dim dMSEpisode4 As New FileFolderRenamer.Episode With {.ID = 4, .Episode = 2, .Title = "The Codpiece Topology"}
        Dim dMSEpisodeList1 As New List(Of FileFolderRenamer.Episode)
        Dim dMSEpisodeList2 As New List(Of FileFolderRenamer.Episode)
        dMSEpisodeList1.Add(dMSEpisode1)
        dMSEpisodeList1.Add(dMSEpisode2)
        dMSEpisodeList2.Add(dMSEpisode3)
        dMSEpisodeList2.Add(dMSEpisode4)
        _fDummyMultiSeason.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dMSEpisodeList1})
        _fDummyMultiSeason.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 2, .Episodes = dMSEpisodeList2})

        _fDummySingleEpisode.Clear()
        _fDummySingleEpisode.AudioChannels = "2"
        _fDummySingleEpisode.AudioCodec = "mp3"
        _fDummySingleEpisode.BasePath = ""
        _fDummySingleEpisode.Collection = ""
        _fDummySingleEpisode.Country = ""
        _fDummySingleEpisode.Director = ""
        _fDummySingleEpisode.DirExist = False
        _fDummySingleEpisode.FileExist = False
        _fDummySingleEpisode.FileName = "OldFileName"
        _fDummySingleEpisode.Genre = "Comedy / Lovestory"
        _fDummySingleEpisode.ID = -1
        _fDummySingleEpisode.IMDB = ""
        _fDummySingleEpisode.IsBDMV = False
        _fDummySingleEpisode.IsLocked = False
        _fDummySingleEpisode.IsMultiEpisode = False
        _fDummySingleEpisode.IsRenamed = False
        _fDummySingleEpisode.IsSingle = True
        _fDummySingleEpisode.IsVideo_TS = False
        _fDummySingleEpisode.ListTitle = "Big Bang Theory, The"
        _fDummySingleEpisode.MPAA = "TV-14"
        _fDummySingleEpisode.MultiViewCount = "3d"
        _fDummySingleEpisode.MultiViewLayout = "Side by Side (left eye first)"
        _fDummySingleEpisode.NewFileName = ""
        _fDummySingleEpisode.NewPath = ""
        _fDummySingleEpisode.OldPath = ""
        _fDummySingleEpisode.OriginalTitle = ""
        _fDummySingleEpisode.Parent = "OldDirectoryName"
        _fDummySingleEpisode.Path = ""
        _fDummySingleEpisode.Rating = "7.3"
        _fDummySingleEpisode.Resolution = "720p"
        _fDummySingleEpisode.ShortStereoMode = "sbs"
        _fDummySingleEpisode.ShowTitle = "The Big Bang Theory"
        _fDummySingleEpisode.SortTitle = "Big Bang Theory"
        _fDummySingleEpisode.StereoMode = "left_right"
        _fDummySingleEpisode.Title = "Pilot"
        _fDummySingleEpisode.TVDBID = "58056"
        _fDummySingleEpisode.VideoCodec = "xvid"
        _fDummySingleEpisode.VideoSource = "dvd"
        _fDummySingleEpisode.Year = "2007"
        Dim dSEpisode As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dSEpisodeList As New List(Of FileFolderRenamer.Episode)
        dSEpisodeList.Add(dSEpisode)
        _fDummySingleEpisode.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dSEpisodeList})

        _fDummySingleMovie.Clear()
        _fDummySingleMovie.AudioChannels = "6"
        _fDummySingleMovie.AudioCodec = "dts"
        _fDummySingleMovie.BasePath = "D:\Movies"
        _fDummySingleMovie.Collection = "The Avengers Collection"
        _fDummySingleMovie.Country = "United States of America / Japan"
        _fDummySingleMovie.Director = "Joss Whedon"
        _fDummySingleMovie.DirExist = False
        _fDummySingleMovie.FileExist = False
        _fDummySingleMovie.FileName = "OldFileName"
        _fDummySingleMovie.Genre = "Action / Sci-Fi"
        _fDummySingleMovie.ID = -1
        _fDummySingleMovie.IMDB = "0848228"
        _fDummySingleMovie.IsBDMV = False
        _fDummySingleMovie.IsLocked = False
        _fDummySingleMovie.IsMultiEpisode = False
        _fDummySingleMovie.IsRenamed = False
        _fDummySingleMovie.IsSingle = True
        _fDummySingleMovie.IsVideo_TS = False
        _fDummySingleMovie.ListTitle = "Avengers, The"
        _fDummySingleMovie.MPAA = "13"
        _fDummySingleMovie.MultiViewCount = "3d"
        _fDummySingleMovie.MultiViewLayout = "Side by Side (left eye first)"
        _fDummySingleMovie.NewFileName = ""
        _fDummySingleMovie.NewPath = ""
        _fDummySingleMovie.OldPath = ""
        _fDummySingleMovie.OriginalTitle = "Marvel's The Avengers"
        _fDummySingleMovie.Parent = "OldDirectoryName"
        _fDummySingleMovie.Path = ""
        _fDummySingleMovie.Rating = "7.3"
        _fDummySingleMovie.Resolution = "1080p"
        _fDummySingleMovie.ShortStereoMode = "sbs"
        _fDummySingleMovie.ShowTitle = ""
        _fDummySingleMovie.SortTitle = "Avengers"
        _fDummySingleMovie.StereoMode = "left_right"
        _fDummySingleMovie.Title = "The Avengers"
        _fDummySingleMovie.TVDBID = ""
        _fDummySingleMovie.VideoCodec = "h264"
        _fDummySingleMovie.VideoSource = "bluray"
        _fDummySingleMovie.Year = "2012"
    End Sub

    Private Sub CreatePreview_MultiEpisode()
        If Not String.IsNullOrEmpty(txtFilePatternEpisodes.Text) AndAlso Not String.IsNullOrEmpty(txtFolderPatternShows.Text) Then
            Dim dFilename As String = FileFolderRenamer.ProccessPattern(_fDummyMultiEpisode, txtFilePatternEpisodes.Text, False, False)
            Dim dSeasonPath As String = FileFolderRenamer.ProccessPattern(_fDummyMultiEpisode, txtFolderPatternSeasons.Text, True, False)
            Dim dShowPath As String = FileFolderRenamer.ProccessPattern(_fDummyMultiEpisode, txtFolderPatternShows.Text, True, False)

            txtMultiEpisodeFile.Text = Path.Combine(dShowPath, dSeasonPath, dFilename)
        Else
            txtMultiEpisodeFile.Text = String.Empty
        End If
    End Sub

    Private Sub CreatePreview_MultiSeason()
        If Not String.IsNullOrEmpty(txtFilePatternEpisodes.Text) AndAlso Not String.IsNullOrEmpty(txtFolderPatternShows.Text) Then
            Dim dFilename As String = FileFolderRenamer.ProccessPattern(_fDummyMultiSeason, txtFilePatternEpisodes.Text, False, False)
            Dim dSeasonPath As String = FileFolderRenamer.ProccessPattern(_fDummyMultiSeason, txtFolderPatternSeasons.Text, True, False)
            Dim dShowPath As String = FileFolderRenamer.ProccessPattern(_fDummyMultiSeason, txtFolderPatternShows.Text, True, False)

            txtMultiSeasonFile.Text = Path.Combine(dShowPath, dSeasonPath, dFilename)
        Else
            txtMultiSeasonFile.Text = String.Empty
        End If
    End Sub

    Private Sub CreatePreview_SingleEpisode()
        If Not String.IsNullOrEmpty(txtFilePatternEpisodes.Text) AndAlso Not String.IsNullOrEmpty(txtFolderPatternShows.Text) Then
            Dim dFilename As String = FileFolderRenamer.ProccessPattern(_fDummySingleEpisode, txtFilePatternEpisodes.Text, False, False)
            Dim dSeasonPath As String = FileFolderRenamer.ProccessPattern(_fDummySingleEpisode, txtFolderPatternSeasons.Text, True, False)
            Dim dShowPath As String = FileFolderRenamer.ProccessPattern(_fDummySingleEpisode, txtFolderPatternShows.Text, True, False)

            txtSingleEpisodeFile.Text = Path.Combine(dShowPath, dSeasonPath, dFilename)
        Else
            txtSingleEpisodeFile.Text = String.Empty
        End If
    End Sub

    Private Sub CreatePreview_SingleMovie()
        If Not String.IsNullOrEmpty(txtFilePatternMovies.Text) AndAlso Not String.IsNullOrEmpty(txtFolderPatternMovies.Text) Then
            Dim dFilename As String = FileFolderRenamer.ProccessPattern(_fDummySingleMovie, txtFilePatternMovies.Text, False, False)
            Dim dPath As String = FileFolderRenamer.ProccessPattern(_fDummySingleMovie, txtFolderPatternMovies.Text, True, False)

            txtSingleMovieFile.Text = Path.Combine(dPath, dFilename)
        Else
            txtSingleMovieFile.Text = String.Empty
        End If
    End Sub

    Private Sub btnFilePatternEpisodesReset_Click(sender As Object, e As EventArgs) Handles btnFilePatternEpisodesReset.Click
        txtFilePatternEpisodes.Text = "$Z - $W2_S?2E?{ - $T}"
    End Sub

    Private Sub btnFilePatternMoviesReset_Click(sender As Object, e As EventArgs) Handles btnFilePatternMoviesReset.Click
        txtFilePatternMovies.Text = "$T{.$S}"
    End Sub

    Private Sub btnFolderPatternMoviesReset_Click(sender As Object, e As EventArgs) Handles btnFolderPatternMoviesReset.Click
        txtFolderPatternMovies.Text = "$T {($Y)}"
    End Sub

    Private Sub btnFolderPatternSeasonsReset_Click(sender As Object, e As EventArgs) Handles btnFolderPatternSeasonsReset.Click
        txtFolderPatternSeasons.Text = "Season $K2_?"
    End Sub

    Private Sub btnFolderPatternShowsReset_Click(sender As Object, e As EventArgs) Handles btnFolderPatternShowsReset.Click
        txtFolderPatternShows.Text = "$Z"
    End Sub

    Private Sub chkEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkEnabled.Checked)
    End Sub

    Private Sub chkRenameEditMovies_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRenameEditMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameEditEpisodes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRenameEditEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameMulti_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRenameMultiMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameMultiShows_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRenameMultiShows.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameSingleMovies_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRenameSingleMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameSingleShows_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRenameSingleShows.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameUpdateEpisodes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRenameUpdateEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub SetUp()
        chkRenameEditMovies.Text = Master.eLang.GetString(466, "Automatically Rename Files After Edit")
        chkRenameEditEpisodes.Text = Master.eLang.GetString(467, "Automatically Rename Files After Edit Episodes")
        chkRenameMultiMovies.Text = Master.eLang.GetString(281, "Automatically Rename Files During Multi-Scraper")
        chkRenameSingleMovies.Text = Master.eLang.GetString(282, "Automatically Rename Files During Single-Scraper")
        chkRenameUpdateEpisodes.Text = Master.eLang.GetString(468, "Automatically Rename Files During DB Update")
        gbRenamerPatternsMovie.Text = Master.eLang.GetString(285, "Default Movie Renaming Patterns")
        gbRenamerPatternsTV.Text = Master.eLang.GetString(470, "Default TV Renaming Patterns")
        lblFilePatternEpisodes.Text = Master.eLang.GetString(469, "Episode Files Pattern")
        lblFilePatternMovies.Text = Master.eLang.GetString(286, "Files Pattern")
        lblFolderPatternMovies.Text = Master.eLang.GetString(287, "Folders Pattern")
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        lblTips.Text = String.Format(Master.eLang.GetString(262, "$1 = First Letter of the Title{0}$2 = Aired date (episodes only){0}$3 = ShortStereoMode{0}$4 = StereoMode{0}$A = Audio Channels{0}$B = Base Path{0}$C = Director{0}$D = Directory{0}$E = Sort Title{0}$F = File Name{0}$G = Genre (Follow with a space, dot or hyphen to change separator){0}$H = Video Codec{0}$I = IMDB ID{0}$J = Audio Codec{0}$K#.S? = #Padding (0-9), Season Separator (. or _ or x), Season Prefix{0}$L = List Title{0}$M = MPAA{0}$N = Collection Name{0}$O = OriginalTitle{0}$OO = OriginalTitle if different from Title{0}$P = Rating{0}$Q#.E? = #Padding (0-9), Episode Separator (. or _ or x), Episode Prefix{0}$R = Resolution{0}$S = Video Source{0}$T = Title{0}$U = Country (Follow with a space, dot or hyphen to change separator){0}$V = 3D (If Multiview > 1){0}$W#.S?#.E? = #Padding (0-9), Seasons Separator (. or _), Season Prefix, #Padding (0-9), Episode Separator (. or _ or x), Episode Prefix{0}$Y = Year{0}$X. (Replace Space with .){0}$Z = Show Title{0}{{}} = Optional{0}$?aaa?bbb? = Replace aaa with bbb{0}$! = Uppercase first letter in each word{0}$; = Lowercase all letters{0}$- = Remove previous char if next pattern does not have a value{0}$+ = Remove next char if previous pattern does not have a value{0}$^ = Remove previous and next char if next pattern does not have a value"), Environment.NewLine)
    End Sub

    Private Sub txtFilePatternMovies_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilePatternMovies.TextChanged
        RaiseEvent ModuleSettingsChanged()
        CreatePreview_SingleMovie()
    End Sub

    Private Sub txtFolderPatternMovies_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFolderPatternMovies.TextChanged
        RaiseEvent ModuleSettingsChanged()
        CreatePreview_SingleMovie()
    End Sub

    Private Sub txtFolderPatternSeasons_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFolderPatternSeasons.TextChanged
        RaiseEvent ModuleSettingsChanged()
        CreatePreview_MultiEpisode()
        CreatePreview_MultiSeason()
        CreatePreview_SingleEpisode()
    End Sub

    Private Sub txtFolderPatternShows_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFolderPatternShows.TextChanged
        RaiseEvent ModuleSettingsChanged()
        CreatePreview_MultiEpisode()
        CreatePreview_MultiSeason()
        CreatePreview_SingleEpisode()
    End Sub

    Private Sub txtFilePatternEpisodes_TextChanged(sender As Object, e As EventArgs) Handles txtFilePatternEpisodes.TextChanged
        RaiseEvent ModuleSettingsChanged()
        CreatePreview_MultiEpisode()
        CreatePreview_MultiSeason()
        CreatePreview_SingleEpisode()
    End Sub

    Public Sub New()
        InitializeComponent()
        SetUp()
        CreateDummies()
    End Sub

#End Region 'Methods

End Class