﻿' ################################################################################
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

Imports EmberAPI
Imports NLog

Public Class frmTV_Data
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private TVMeta As New List(Of Settings.MetadataPerType)
    Private TempTVScraperSeasonTitleBlacklist As New ExtendedListOfString

#End Region 'Fields

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.IMasterSettingsPanel.NeedsReload_MovieSet
    Public Event NeedsReload_TVEpisode() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.IMasterSettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IMasterSettingsPanel.SettingsChanged

#End Region 'Events

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

#End Region 'Constructors 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.IMasterSettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IMasterSettingsPanel.InjectSettingsPanel
        Settings_Load()

        Return New Containers.SettingsPanel With {
            .Contains = Enums.SettingsPanelType.TVData,
            .ImageIndex = 3,
            .Order = 400,
            .Panel = pnlSettings,
            .SettingsPanelID = "TV_Data",
            .Title = Master.eLang.GetString(556, "NFO"),
            .Type = Enums.SettingsPanelType.TV
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings
            .TVLockEpisodeLanguageA = chkTVLockEpisodeLanguageA.Checked
            .TVLockEpisodeLanguageV = chkTVLockEpisodeLanguageV.Checked
            .TVLockEpisodePlot = chkTVLockEpisodePlot.Checked
            .TVLockEpisodeRating = chkTVLockEpisodeRating.Checked
            .TVLockEpisodeRuntime = chkTVLockEpisodeRuntime.Checked
            .TVLockEpisodeTitle = chkTVLockEpisodeTitle.Checked
            .TVLockEpisodeUserRating = chkTVLockEpisodeUserRating.Checked
            .TVLockSeasonPlot = chkTVLockSeasonPlot.Checked
            .TVLockSeasonTitle = chkTVLockSeasonTitle.Checked
            .TVLockShowCert = chkTVLockShowCert.Checked
            .TVLockShowCreators = chkTVLockShowCreators.Checked
            .TVLockShowGenre = chkTVLockShowGenre.Checked
            .TVLockShowMPAA = chkTVLockShowMPAA.Checked
            .TVLockShowOriginalTitle = chkTVLockShowOriginalTitle.Checked
            .TVLockShowPlot = chkTVLockShowPlot.Checked
            .TVLockShowRating = chkTVLockShowRating.Checked
            .TVLockShowRuntime = chkTVLockShowRuntime.Checked
            .TVLockShowStatus = chkTVLockShowStatus.Checked
            .TVLockShowStudio = chkTVLockShowStudio.Checked
            .TVLockShowTitle = chkTVLockShowTitle.Checked
            .TVLockShowUserRating = chkTVLockShowUserRating.Checked
            .TVMetadataPerFileType.Clear()
            .TVMetadataPerFileType.AddRange(TVMeta)
            .TVScraperCastWithImgOnly = chkTVScraperCastWithImg.Checked
            .TVScraperCleanFields = chkTVScraperCleanFields.Checked
            .TVScraperDurationRuntimeFormat = txtTVScraperDurationRuntimeFormat.Text
            .TVScraperEpisodeActors = chkTVScraperEpisodeActors.Checked
            Integer.TryParse(txtTVScraperEpisodeActorsLimit.Text, .TVScraperEpisodeActorsLimit)
            .TVScraperEpisodeAired = chkTVScraperEpisodeAired.Checked
            .TVScraperEpisodeCredits = chkTVScraperEpisodeCredits.Checked
            .TVScraperEpisodeDirector = chkTVScraperEpisodeDirector.Checked
            .TVScraperEpisodeGuestStars = chkTVScraperEpisodeGuestStars.Checked
            Integer.TryParse(txtTVScraperEpisodeGuestStarsLimit.Text, .TVScraperEpisodeGuestStarsLimit)
            .TVScraperEpisodeGuestStarsToActors = chkTVScraperEpisodeGuestStarsToActors.Checked
            .TVScraperEpisodePlot = chkTVScraperEpisodePlot.Checked
            .TVScraperEpisodeRating = chkTVScraperEpisodeRating.Checked
            .TVScraperEpisodeRuntime = chkTVScraperEpisodeRuntime.Checked
            .TVScraperEpisodeTitle = chkTVScraperEpisodeTitle.Checked
            .TVScraperEpisodeUserRating = chkTVScraperEpisodeUserRating.Checked
            .TVScraperMetaDataScan = chkMetadata.Checked
            .TVScraperSeasonAired = chkTVScraperSeasonAired.Checked
            .TVScraperSeasonPlot = chkTVScraperSeasonPlot.Checked
            .TVScraperSeasonTitle = chkTVScraperSeasonTitle.Checked
            .TVScraperShowActors = chkTVScraperShowActors.Checked
            Integer.TryParse(txtTVScraperShowActorsLimit.Text, .TVScraperShowActorsLimit)
            .TVScraperShowCert = chkTVScraperShowCert.Checked
            .TVScraperShowCreators = chkTVScraperShowCreators.Checked
            .TVScraperShowCertForMPAA = chkTVScraperShowCertForMPAA.Checked
            .TVScraperShowCertForMPAAFallback = chkTVScraperShowCertForMPAAFallback.Checked
            .TVScraperShowCertOnlyValue = chkTVScraperShowCertOnlyValue.Checked
            If Not String.IsNullOrEmpty(cbTVScraperShowCertLang.Text) Then
                If cbTVScraperShowCertLang.SelectedIndex = 0 Then
                    .TVScraperShowCertLang = Master.eLang.All
                Else
                    .TVScraperShowCertLang = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.name = cbTVScraperShowCertLang.Text).abbreviation
                End If
            End If
            .TVScraperShowEpiGuideURL = chkTVScraperShowEpiGuideURL.Checked
            .TVScraperShowGenre = chkTVScraperShowGenre.Checked
            Integer.TryParse(txtTVScraperShowGenreLimit.Text, .TVScraperShowGenreLimit)
            .TVScraperShowMPAA = chkTVScraperShowMPAA.Checked
            .TVScraperShowOriginalTitle = chkTVScraperShowOriginalTitle.Checked
            .TVScraperShowOriginalTitleAsTitle = chkTVScraperShowOriginalTitleAsTitle.Checked
            .TVScraperShowMPAANotRated = txtTVScraperShowMPAANotRated.Text
            .TVScraperShowPlot = chkTVScraperShowPlot.Checked
            .TVScraperShowPremiered = chkTVScraperShowPremiered.Checked
            .TVScraperShowRating = chkTVScraperShowRating.Checked
            .TVScraperShowRuntime = chkTVScraperShowRuntime.Checked
            .TVScraperShowStatus = chkTVScraperShowStatus.Checked
            .TVScraperShowStudio = chkTVScraperShowStudio.Checked
            Integer.TryParse(txtTVScraperShowStudioLimit.Text, .TVScraperShowStudioLimit)
            .TVScraperShowTitle = chkTVScraperShowTitle.Checked
            .TVScraperShowUserRating = chkTVScraperShowUserRating.Checked
            .TVScraperUseDisplaySeasonEpisode = chkTVScraperUseDisplaySeasonEpisode.Checked
            .TVScraperUseMDDuration = chkTVScraperUseMDDuration.Checked
            .TVScraperUseSRuntimeForEp = chkTVScraperUseSRuntimeForEp.Checked
            .TVScraperSeasonTitleBlacklist = TempTVScraperSeasonTitleBlacklist
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            chkTVLockEpisodeLanguageA.Checked = .TVLockEpisodeLanguageA
            chkTVLockEpisodeLanguageV.Checked = .TVLockEpisodeLanguageV
            chkTVLockEpisodePlot.Checked = .TVLockEpisodePlot
            chkTVLockEpisodeRating.Checked = .TVLockEpisodeRating
            chkTVLockEpisodeRuntime.Checked = .TVLockEpisodeRuntime
            chkTVLockEpisodeTitle.Checked = .TVLockEpisodeTitle
            chkTVLockEpisodeUserRating.Checked = .TVLockEpisodeUserRating
            chkTVLockSeasonPlot.Checked = .TVLockSeasonPlot
            chkTVLockSeasonTitle.Checked = .TVLockSeasonTitle
            chkTVLockShowCert.Checked = .TVLockShowCert
            chkTVLockShowCreators.Checked = .TVLockShowCreators
            chkTVLockShowGenre.Checked = .TVLockShowGenre
            chkTVLockShowMPAA.Checked = .TVLockShowMPAA
            chkTVLockShowOriginalTitle.Checked = .TVLockShowOriginalTitle
            chkTVLockShowPlot.Checked = .TVLockShowPlot
            chkTVLockShowRating.Checked = .TVLockShowRating
            chkTVLockShowRuntime.Checked = .TVLockShowRuntime
            chkTVLockShowStatus.Checked = .TVLockShowStatus
            chkTVLockShowStudio.Checked = .TVLockShowStudio
            chkTVLockShowTitle.Checked = .TVLockShowTitle
            chkTVLockShowUserRating.Checked = .TVLockShowUserRating
            chkTVScraperCastWithImg.Checked = .TVScraperCastWithImgOnly
            chkTVScraperCleanFields.Checked = .TVScraperCleanFields
            chkTVScraperEpisodeActors.Checked = .TVScraperEpisodeActors
            chkTVScraperEpisodeAired.Checked = .TVScraperEpisodeAired
            chkTVScraperEpisodeCredits.Checked = .TVScraperEpisodeCredits
            chkTVScraperEpisodeDirector.Checked = .TVScraperEpisodeDirector
            chkTVScraperEpisodeGuestStars.Checked = .TVScraperEpisodeGuestStars
            chkTVScraperEpisodeGuestStarsToActors.Checked = .TVScraperEpisodeGuestStarsToActors
            chkTVScraperEpisodePlot.Checked = .TVScraperEpisodePlot
            chkTVScraperEpisodeRating.Checked = .TVScraperEpisodeRating
            chkTVScraperEpisodeRuntime.Checked = .TVScraperEpisodeRuntime
            chkTVScraperEpisodeTitle.Checked = .TVScraperEpisodeTitle
            chkTVScraperEpisodeUserRating.Checked = .TVScraperEpisodeUserRating
            chkMetadata.Checked = .TVScraperMetaDataScan
            chkTVScraperSeasonAired.Checked = .TVScraperSeasonAired
            chkTVScraperSeasonPlot.Checked = .TVScraperSeasonPlot
            chkTVScraperSeasonTitle.Checked = .TVScraperSeasonTitle
            chkTVScraperShowActors.Checked = .TVScraperShowActors
            chkTVScraperShowCert.Checked = .TVScraperShowCert
            chkTVScraperShowCreators.Checked = .TVScraperShowCreators
            chkTVScraperShowCertForMPAA.Checked = .TVScraperShowCertForMPAA
            chkTVScraperShowCertForMPAAFallback.Checked = .TVScraperShowCertForMPAAFallback
            chkTVScraperShowCertOnlyValue.Checked = .TVScraperShowCertOnlyValue
            chkTVScraperShowEpiGuideURL.Checked = .TVScraperShowEpiGuideURL
            chkTVScraperShowGenre.Checked = .TVScraperShowGenre
            chkTVScraperShowMPAA.Checked = .TVScraperShowMPAA
            chkTVScraperShowOriginalTitle.Checked = .TVScraperShowOriginalTitle
            chkTVScraperShowOriginalTitleAsTitle.Checked = .TVScraperShowOriginalTitleAsTitle
            chkTVScraperShowPlot.Checked = .TVScraperShowPlot
            chkTVScraperShowPremiered.Checked = .TVScraperShowPremiered
            chkTVScraperShowRating.Checked = .TVScraperShowRating
            chkTVScraperShowRuntime.Checked = .TVScraperShowRuntime
            chkTVScraperShowStatus.Checked = .TVScraperShowStatus
            chkTVScraperShowStudio.Checked = .TVScraperShowStudio
            chkTVScraperShowTitle.Checked = .TVScraperShowTitle
            chkTVScraperShowUserRating.Checked = .TVScraperShowUserRating
            chkTVScraperUseDisplaySeasonEpisode.Checked = .TVScraperUseDisplaySeasonEpisode
            chkTVScraperUseMDDuration.Checked = .TVScraperUseMDDuration
            chkTVScraperUseSRuntimeForEp.Checked = .TVScraperUseSRuntimeForEp
            txtTVScraperDurationRuntimeFormat.Text = .TVScraperDurationRuntimeFormat
            txtTVScraperEpisodeActorsLimit.Text = .TVScraperEpisodeActorsLimit.ToString
            txtTVScraperEpisodeGuestStarsLimit.Text = .TVScraperEpisodeGuestStarsLimit.ToString
            txtTVScraperShowActorsLimit.Text = .TVScraperShowActorsLimit.ToString
            txtTVScraperShowMPAANotRated.Text = .TVScraperShowMPAANotRated
            txtTVScraperShowGenreLimit.Text = .TVScraperShowGenreLimit.ToString
            txtTVScraperShowStudioLimit.Text = .TVScraperShowStudioLimit.ToString
            txtTVScraperDurationRuntimeFormat.Enabled = .TVScraperUseMDDuration

            Try
                cbTVScraperShowCertLang.Items.Clear()
                cbTVScraperShowCertLang.Items.Add(Master.eLang.All)
                cbTVScraperShowCertLang.Items.AddRange((From lLang In APIXML.CertificationLanguages.Language Select lLang.name).ToArray)
                If cbTVScraperShowCertLang.Items.Count > 0 Then
                    If .TVScraperShowCertLang = Master.eLang.All Then
                        cbTVScraperShowCertLang.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.TVScraperShowCertLang) Then
                        Dim tLanguage As CertLanguages = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .TVScraperShowCertLang)
                        If tLanguage IsNot Nothing AndAlso tLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLanguage.name) Then
                            cbTVScraperShowCertLang.Text = tLanguage.name
                        Else
                            cbTVScraperShowCertLang.SelectedIndex = 0
                        End If
                    Else
                        cbTVScraperShowCertLang.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            TempTVScraperSeasonTitleBlacklist = .TVScraperSeasonTitleBlacklist

            TVMeta.AddRange(.TVMetadataPerFileType)
            LoadTVMetadata()
        End With
    End Sub

    Private Sub Setup()
        'Certifications
        Dim strCertifications As String = Master.eLang.GetString(56, "Certifications")
        gbTVScraperCertificationOpts.Text = strCertifications
        lblTVScraperGlobalCertifications.Text = strCertifications
        'Limit
        Dim strLimit As String = Master.eLang.GetString(578, "Limit")
        lblTVScraperGlobalHeaderEpisodesLimit.Text = strLimit
        lblTVScraperGlobalHeaderSeasonsLimit.Text = strLimit
        lblTVScraperGlobalHeaderShowsLimit.Text = strLimit
        'Lock
        Dim strLock As String = Master.eLang.GetString(24, "Lock")
        lblTVScraperGlobalHeaderEpisodesLock.Text = strLock
        lblTVScraperGlobalHeaderSeasonsLock.Text = strLock
        lblTVScraperGlobalHeaderShowsLock.Text = strLock

        lblTVScraperGlobalActors.Text = Master.eLang.GetString(231, "Actors")
        chkTVScraperEpisodeGuestStarsToActors.Text = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        chkTVScraperUseDisplaySeasonEpisode.Text = Master.eLang.GetString(976, "Add <displayseason> and <displayepisode> to special episodes")
        lblTVScraperGlobalAired.Text = Master.eLang.GetString(728, "Aired")
        chkTVScraperCleanFields.Text = Master.eLang.GetString(125, "Clear disabled fields")
        lblTVScraperGlobalCreators.Text = Master.eLang.GetString(744, "Creators")
        gbTVScraperDefFIExtOpts.Text = Master.eLang.GetString(625, "Defaults by File Type")
        lblTVScraperGlobalDirectors.Text = Master.eLang.GetString(940, "Directors")
        lblTVScraperDurationRuntimeFormat.Text = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
        lblTVScraperGlobalEpisodeGuideURL.Text = Master.eLang.GetString(723, "Episode Guide URL")
        lblTVScraperGlobalHeaderEpisodes.Text = Master.eLang.GetString(682, "Episodes")
        lblTVScraperDefFIExt.Text = String.Concat(Master.eLang.GetString(626, "File Type"), ":")
        lblTVScraperGlobalGenres.Text = Master.eLang.GetString(725, "Genres")
        lblTVScraperGlobalLanguageA.Text = Master.eLang.GetString(431, "Metadata Audio Language")
        lblTVScraperGlobalLanguageV.Text = Master.eLang.GetString(435, "Metadata Video Language")
        gbMetadata.Text = Master.eLang.GetString(59, "Metadata")
        gbTVScraperMiscOpts.Text = Master.eLang.GetString(429, "Miscellaneous")
        lblTVScraperGlobalMPAA.Text = Master.eLang.GetString(401, "MPAA")
        lblTVScraperShowMPAANotRated.Text = Master.eLang.GetString(832, "MPAA value if no rating is available")
        chkTVScraperShowCertForMPAAFallback.Text = Master.eLang.GetString(1293, "Only if no MPAA is found")
        chkTVScraperShowCertOnlyValue.Text = Master.eLang.GetString(835, "Only Save the Value to NFO")
        lblTVScraperGlobalPlot.Text = Master.eLang.GetString(65, "Plot")
        lblTVScraperGlobalPremiered.Text = Master.eLang.GetString(724, "Premiered")
        lblTVScraperGlobalRating.Text = Master.eLang.GetString(400, "Rating")
        lblTVScraperGlobalRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkTVScraperCastWithImg.Text = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        gbTVScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields - Global")
        lblTVScraperGlobalHeaderSeasons.Text = Master.eLang.GetString(681, "Seasons")
        lblTVScraperGlobalHeaderShows.Text = Master.eLang.GetString(680, "Shows")
        lblTVScraperGlobalStatus.Text = Master.eLang.GetString(215, "Status")
        lblTVScraperGlobalStudios.Text = Master.eLang.GetString(226, "Studios")
        lblTVScraperGlobalTitle.Text = Master.eLang.GetString(21, "Title")
        chkTVScraperShowCertForMPAA.Text = Master.eLang.GetString(511, "Use Certification for MPAA")
        chkTVScraperShowOriginalTitleAsTitle.Text = Master.eLang.GetString(240, "Use Original Title as Title")
        lblTVScraperGlobalUserRating.Text = Master.eLang.GetString(1467, "User Rating")
        lblTVScraperGlobalCredits.Text = Master.eLang.GetString(394, "Writers")
        chkTVScraperEpisodeGuestStarsToActors.Text = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        chkTVScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        chkTVScraperUseSRuntimeForEp.Text = Master.eLang.GetString(1262, "Use Show Runtime for Episodes if no Episode Runtime can be found")
        gbTVScraperDurationFormatOpts.Text = Master.eLang.GetString(515, "Duration Format")
        gbTVScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields")
        lblTVScraperGlobalGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        chkMetadata.Text = Master.eLang.GetString(517, "Scan Metadata")
    End Sub

    Private Sub btnTVScraperDefFIExtAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not txtTVScraperDefFIExt.Text.StartsWith(".") Then txtTVScraperDefFIExt.Text = String.Concat(".", txtTVScraperDefFIExt.Text)
        Using dFileInfo As New dlgFileInfo(New MediaContainers.Fileinfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                Dim fi = dFileInfo.Result
                If Not fi Is Nothing Then
                    Dim m As New Settings.MetadataPerType With {
                        .FileType = txtTVScraperDefFIExt.Text,
                        .MetaData = fi
                    }
                    TVMeta.Add(m)
                    LoadTVMetadata()
                    txtTVScraperDefFIExt.Text = String.Empty
                    txtTVScraperDefFIExt.Focus()
                    RaiseEvent SettingsChanged()
                End If
            End If
        End Using
    End Sub

    Private Sub btnTVScraperDefFIExtEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each tMetadata As Settings.MetadataPerType In TVMeta
                If tMetadata.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                    Using dFileInfo As New dlgFileInfo(tMetadata.MetaData)
                        If dFileInfo.ShowDialog = DialogResult.OK Then
                            tMetadata.MetaData = dFileInfo.Result
                            LoadTVMetadata()
                            RaiseEvent SettingsChanged()
                        End If
                    End Using
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnTVScraperDefFIExtRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        RemoveTVMetaData()
    End Sub

    Private Sub chkTVScraperShowStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        txtTVScraperShowStudioLimit.Enabled = chkTVScraperShowStudio.Checked
        If Not chkTVScraperShowStudio.Checked Then txtTVScraperShowStudioLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub
    Private Sub TVScraperActors_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        chkTVScraperCastWithImg.Enabled = chkTVScraperEpisodeActors.Checked OrElse chkTVScraperEpisodeGuestStars.Checked OrElse chkTVScraperShowActors.Checked
        txtTVScraperEpisodeActorsLimit.Enabled = chkTVScraperEpisodeActors.Checked
        txtTVScraperEpisodeGuestStarsLimit.Enabled = chkTVScraperEpisodeGuestStars.Checked
        txtTVScraperShowActorsLimit.Enabled = chkTVScraperShowActors.Checked

        If Not chkTVScraperEpisodeActors.Checked AndAlso
            Not chkTVScraperEpisodeGuestStars.Checked AndAlso
            Not chkTVScraperShowActors.Checked Then
            chkTVScraperCastWithImg.Checked = False
        End If

        If Not chkTVScraperEpisodeActors.Checked Then txtTVScraperEpisodeActorsLimit.Text = "0"
        If Not chkTVScraperEpisodeGuestStars.Checked Then txtTVScraperEpisodeGuestStarsLimit.Text = "0"
        If Not chkTVScraperShowActors.Checked Then txtTVScraperShowActorsLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVScraperShowCert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        If Not chkTVScraperShowCert.Checked Then
            cbTVScraperShowCertLang.Enabled = False
            cbTVScraperShowCertLang.SelectedIndex = 0
            chkTVScraperShowCertForMPAA.Enabled = False
            chkTVScraperShowCertForMPAA.Checked = False
            chkTVScraperShowCertOnlyValue.Enabled = False
            chkTVScraperShowCertOnlyValue.Checked = False
        Else
            cbTVScraperShowCertLang.Enabled = True
            cbTVScraperShowCertLang.SelectedIndex = 0
            chkTVScraperShowCertForMPAA.Enabled = True
            chkTVScraperShowCertOnlyValue.Enabled = True
        End If
    End Sub

    Private Sub chkTVScraperSeasonTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperSeasonTitle.Click
        RaiseEvent SettingsChanged()
        btnTVScraperSeasonTitleBlacklist.Enabled = chkTVScraperSeasonTitle.Checked
    End Sub

    Private Sub chkTVScraperShowCertForMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        If Not chkTVScraperShowCertForMPAA.Checked Then
            chkTVScraperShowCertForMPAAFallback.Enabled = False
            chkTVScraperShowCertForMPAAFallback.Checked = False
        Else
            chkTVScraperShowCertForMPAAFallback.Enabled = True
        End If
    End Sub


    Private Sub chkTVScraperShowGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        txtTVScraperShowGenreLimit.Enabled = chkTVScraperShowGenre.Checked
        If Not chkTVScraperShowGenre.Checked Then txtTVScraperShowGenreLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVScraperShowOriginalTitle_CheckedChanged(sender As Object, e As EventArgs)
        chkTVScraperShowOriginalTitleAsTitle.Enabled = chkTVScraperShowOriginalTitle.Checked
        If Not chkTVScraperShowOriginalTitle.Checked Then chkTVScraperShowOriginalTitleAsTitle.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVScraperShowRuntime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        chkTVScraperUseSRuntimeForEp.Enabled = chkTVScraperShowRuntime.Checked
        If Not chkTVScraperShowRuntime.Checked Then
            chkTVScraperUseSRuntimeForEp.Checked = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVScraperUseMDDuration_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        txtTVScraperDurationRuntimeFormat.Enabled = chkTVScraperUseMDDuration.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub LoadTVMetadata()
        lstTVScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In TVMeta
            lstTVScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub lstTVScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then RemoveTVMetaData()
    End Sub

    Private Sub lstTVScraperDefFIExt_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            btnTVScraperDefFIExtEdit.Enabled = True
            btnTVScraperDefFIExtRemove.Enabled = True
            txtTVScraperDefFIExt.Text = String.Empty
        Else
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub RemoveTVMetaData()
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In TVMeta
                If x.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                    TVMeta.Remove(x)
                    LoadTVMetadata()
                    RaiseEvent SettingsChanged()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub txtTVScraperDefFIExt_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        btnTVScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtTVScraperDefFIExt.Text) AndAlso Not lstTVScraperDefFIExt.Items.Contains(If(txtTVScraperDefFIExt.Text.StartsWith("."), txtTVScraperDefFIExt.Text, String.Concat(".", txtTVScraperDefFIExt.Text)))
        If btnTVScraperDefFIExtAdd.Enabled Then
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub btnTVScraperSeasonTitleBlacklist_Click(sender As Object, e As EventArgs) Handles btnTVScraperSeasonTitleBlacklist.Click
        If frmTV_Data_SeasonTitleBlacklist.ShowDialog(TempTVScraperSeasonTitleBlacklist) = DialogResult.OK Then
            TempTVScraperSeasonTitleBlacklist = frmTV_Data_SeasonTitleBlacklist.Result
            RaiseEvent SettingsChanged()
        End If
    End Sub

#End Region 'Methods

End Class