Imports EmberAPI

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

    Public Event ModuleEnabledChanged(ByVal State As Boolean, ByVal difforder As Integer)

    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub CreateDummies()
        _fDummyMultiEpisode.Clear()
        _fDummyMultiEpisode.AudioChannels = ""
        _fDummyMultiEpisode.AudioCodec = ""
        _fDummyMultiEpisode.BasePath = ""
        _fDummyMultiEpisode.Collection = ""
        _fDummyMultiEpisode.Country = ""
        _fDummyMultiEpisode.Director = ""
        _fDummyMultiEpisode.DirExist = False
        _fDummyMultiEpisode.FileExist = False
        _fDummyMultiEpisode.FileName = ""
        _fDummyMultiEpisode.Genre = ""
        _fDummyMultiEpisode.ID = -1
        _fDummyMultiEpisode.IMDBID = ""
        _fDummyMultiEpisode.IsBDMV = False
        _fDummyMultiEpisode.IsLocked = False
        _fDummyMultiEpisode.IsRenamed = False
        _fDummyMultiEpisode.IsSingle = True
        _fDummyMultiEpisode.IsVideo_TS = False
        _fDummyMultiEpisode.ListTitle = ""
        _fDummyMultiEpisode.MPAARate = ""
        _fDummyMultiEpisode.MultiViewCount = ""
        _fDummyMultiEpisode.MultiViewLayout = ""
        _fDummyMultiEpisode.NewFileName = ""
        _fDummyMultiEpisode.NewPath = ""
        _fDummyMultiEpisode.OldPath = ""
        _fDummyMultiEpisode.OriginalTitle = ""
        _fDummyMultiEpisode.Parent = ""
        _fDummyMultiEpisode.Path = ""
        _fDummyMultiEpisode.Rating = ""
        _fDummyMultiEpisode.Resolution = ""
        _fDummyMultiEpisode.ShowTitle = ""
        _fDummyMultiEpisode.SortTitle = ""
        _fDummyMultiEpisode.Title = ""
        _fDummyMultiEpisode.VideoCodec = ""
        _fDummyMultiEpisode.VideoSource = ""
        _fDummyMultiEpisode.Year = ""

        _fDummySingleMovie.Clear()
        _fDummySingleMovie.AudioChannels = "6"
        _fDummySingleMovie.AudioCodec = "dts"
        _fDummySingleMovie.BasePath = "D:\Movies"
        _fDummySingleMovie.Collection = "The Avengers Collection"
        _fDummySingleMovie.Country = "United States of America"
        _fDummySingleMovie.Director = "Joss Whedon"
        _fDummySingleMovie.DirExist = False
        _fDummySingleMovie.FileExist = False
        _fDummySingleMovie.FileName = ""
        _fDummySingleMovie.Genre = "Action / Sci-Fi"
        _fDummySingleMovie.ID = -1
        _fDummySingleMovie.IMDBID = "0848228"
        _fDummySingleMovie.IsBDMV = False
        _fDummySingleMovie.IsLocked = False
        _fDummySingleMovie.IsRenamed = False
        _fDummySingleMovie.IsSingle = True
        _fDummySingleMovie.IsVideo_TS = False
        _fDummySingleMovie.ListTitle = "Avengers, The"
        _fDummySingleMovie.MPAARate = "13"
        _fDummySingleMovie.MultiViewCount = "2"
        _fDummySingleMovie.MultiViewLayout = ""
        _fDummySingleMovie.NewFileName = ""
        _fDummySingleMovie.NewPath = ""
        _fDummySingleMovie.OldPath = ""
        _fDummySingleMovie.OriginalTitle = "Marvel's The Avengers"
        _fDummySingleMovie.Parent = ""
        _fDummySingleMovie.Path = ""
        _fDummySingleMovie.Rating = "7.3"
        _fDummySingleMovie.Resolution = "1080p"
        _fDummySingleMovie.ShowTitle = ""
        _fDummySingleMovie.SortTitle = "Avengers"
        _fDummySingleMovie.Title = "The Avengers"
        _fDummySingleMovie.VideoCodec = "h264"
        _fDummySingleMovie.VideoSource = "bluray"
        _fDummySingleMovie.Year = "2012"

    End Sub

    Private Sub chkBulRenamer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBulkRenamer.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub chkGenericModule_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGenericModule.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkOnError_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameEditMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenameEditMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameEditEpisodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenameEditEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenameMultiMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameMultiShows_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenameMultiShows.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameSingleMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenameSingleMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameSingleShows_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenameSingleShows.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRenameUpdateEpisodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenameUpdateEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub SetUp()
        Me.chkRenameEditMovies.Text = Master.eLang.GetString(466, "Automatically Rename Files After Edit")
        Me.chkRenameEditEpisodes.Text = Master.eLang.GetString(467, "Automatically Rename Files After Edit Episodes")
        Me.chkRenameMultiMovies.Text = Master.eLang.GetString(281, "Automatically Rename Files During Multi-Scraper")
        Me.chkRenameSingleMovies.Text = Master.eLang.GetString(282, "Automatically Rename Files During Single-Scraper")
        Me.chkRenameUpdateEpisodes.Text = Master.eLang.GetString(468, "Automatically Rename Files During DB Update")
        Me.gbRenamerPatternsMovies.Text = Master.eLang.GetString(285, "Default Renaming Patterns")
        Me.lblFilePatternEpisodes.Text = Master.eLang.GetString(469, "Episode Files Pattern")
        Me.lblFilePatternMovies.Text = Master.eLang.GetString(286, "Files Pattern")
        Me.lblFolderPatternMovies.Text = Master.eLang.GetString(287, "Folders Pattern")
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkGenericModule.Text = Master.eLang.GetString(288, "Enable Generic Rename Module")
        Me.chkBulkRenamer.Text = Master.eLang.GetString(290, "Enable Bulk Renamer Tool")
        Me.lblTips.Text = String.Format(Master.eLang.GetString(262, "$1 = First Letter of the Title{0}$A = Audio Channels{0}$B = Base Path{0}$C = Director{0}$D = Directory{0}$E = Sort Title{0}$F = File Name{0}$G = Genre (Follow with a space, dot or hyphen to change separator){0}$H = Video Codec{0}$I = IMDB ID{0}$J = Audio Codec{0}$L = List Title{0}$M = MPAA{0}$N = Collection Name{0}$O = OriginalTitle{0}$P = Rating{0}$Q.E? = Episode Separator (. or _ or x), Episode Prefix{0}$R = Resolution{0}$S = Video Source{0}$T = Title{0}$V = 3D (If Multiview > 1){0}$W.S?.E? = Seasons Separator (. or _), Season Prefix, Episode Separator (. or _ or x), Episode Prefix{0}$Y = Year{0}$X. (Replace Space with .){0}$Z = Show Title{0}{{}} = Optional{0}$?aaa?bbb? = Replace aaa with bbb{0}$- = Remove previous char if next pattern does not have a value{0}$+ = Remove next char if previous pattern does not have a value{0}$^ = Remove previous and next char if next pattern does not have a value"), vbNewLine)
    End Sub

    Private Sub txtFilePatternMovies_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilePatternMovies.TextChanged
        RaiseEvent ModuleSettingsChanged()

        txtSingleMovieFile.Text = FileFolderRenamer.ProccessPattern(_fDummySingleMovie, txtFilePatternMovies.Text, False, False)
    End Sub

    Private Sub txtFolderPattern_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderPatternMovies.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub txtFilePatternEpisodes_TextChanged(sender As Object, e As EventArgs) Handles txtFilePatternEpisodes.TextChanged
        RaiseEvent ModuleSettingsChanged()
        Dim fDummySingleEpisode As New FileFolderRenamer.FileRename
        fDummySingleEpisode.MPAARate = "TV-14"
        fDummySingleEpisode.Rating = "8.9"
        fDummySingleEpisode.Resolution = "480p"
        fDummySingleEpisode.ShowTitle = "The Big Bang Theory"
        fDummySingleEpisode.ListTitle = StringUtils.FilterTokens_TV("The Big Bang Theory")
        fDummySingleEpisode.Title = "Pilot"
        fDummySingleEpisode.VideoCodec = "divx"
        fDummySingleEpisode.VideoSource = "dvd"
        Dim dSEpisode As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dSEpisodeList As New List(Of FileFolderRenamer.Episode)
        dSEpisodeList.Add(dSEpisode)
        fDummySingleEpisode.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dSEpisodeList})

        txtSingleEpisodeFile.Text = FileFolderRenamer.ProccessPattern(_fDummySingleEpisode, txtFilePatternEpisodes.Text, False, False)


        Dim fDummyMultiEpisode As New FileFolderRenamer.FileRename
        fDummyMultiEpisode.MPAARate = "TV-14"
        fDummyMultiEpisode.Rating = "8.9"
        fDummyMultiEpisode.Resolution = "480p"
        fDummyMultiEpisode.ShowTitle = "The Big Bang Theory"
        fDummyMultiEpisode.ListTitle = StringUtils.FilterTokens_TV("The Big Bang Theory")
        fDummyMultiEpisode.Title = "Pilot"
        fDummyMultiEpisode.VideoCodec = "divx"
        fDummyMultiEpisode.VideoSource = "dvd"
        Dim dMEpisode1 As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dMEpisode2 As New FileFolderRenamer.Episode With {.ID = 2, .Episode = 2, .Title = "The Big Bran Hypothesis"}
        Dim dMEpisodeList As New List(Of FileFolderRenamer.Episode)
        dMEpisodeList.Add(dMEpisode1)
        dMEpisodeList.Add(dMEpisode2)
        fDummyMultiEpisode.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dMEpisodeList})

        txtMultiEpisodeFile.Text = FileFolderRenamer.ProccessPattern(_fDummyMultiEpisode, txtFilePatternEpisodes.Text, False, False)


        Dim fDummyMultiSeason As New FileFolderRenamer.FileRename
        fDummyMultiSeason.MPAARate = "TV-14"
        fDummyMultiSeason.Rating = "8.9"
        fDummyMultiSeason.Resolution = "480p"
        fDummyMultiSeason.ShowTitle = "The Big Bang Theory"
        fDummyMultiSeason.ListTitle = StringUtils.FilterTokens_TV("The Big Bang Theory")
        fDummyMultiSeason.Title = "Pilot"
        fDummyMultiSeason.VideoCodec = "divx"
        fDummyMultiSeason.VideoSource = "dvd"
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
        fDummyMultiSeason.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dMSEpisodeList1})
        fDummyMultiSeason.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 2, .Episodes = dMSEpisodeList2})

        txtMultiSeasonFile.Text = FileFolderRenamer.ProccessPattern(_fDummyMultiSeason, txtFilePatternEpisodes.Text, False, False)
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
        Me.CreateDummies()
    End Sub

#End Region 'Methods

End Class