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

Imports EmberAPI
Imports NLog
Imports System.IO

Public Class frmMovieset_FileNaming
    Implements Interfaces.ISettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.ISettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.ISettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.ISettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.ISettingsPanel.NeedsReload_Movieset
    Public Event NeedsReload_TVEpisode() Implements Interfaces.ISettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.ISettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.ISettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.ISettingsPanel.SettingsChanged
    Public Event StateChanged(ByVal uniqueSettingsPanelId As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.ISettingsPanel.StateChanged

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ChildType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ChildType

    Public Property Image As Image Implements Interfaces.ISettingsPanel.Image

    Public Property ImageIndex As Integer Implements Interfaces.ISettingsPanel.ImageIndex

    Public Property IsEnabled As Boolean Implements Interfaces.ISettingsPanel.IsEnabled

    Public ReadOnly Property MainPanel As Panel Implements Interfaces.ISettingsPanel.MainPanel

    Public Property Order As Integer Implements Interfaces.ISettingsPanel.Order

    Public ReadOnly Property ParentType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ParentType

    Public ReadOnly Property Title As String Implements Interfaces.ISettingsPanel.Title

    Public Property UniqueId As String Implements Interfaces.ISettingsPanel.UniqueId

#End Region 'Properties

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        'Set Master Panel Data
        ChildType = Enums.SettingsPanelType.MoviesetFileNaming
        IsEnabled = True
        Image = Nothing
        ImageIndex = 4
        Order = 300
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(471, "File Naming")
        ParentType = Enums.SettingsPanelType.Movieset
        UniqueId = "Movieset_FileNaming"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        lblMovieSetSourcesFilenamingExpertSingleBanner.Text = strBanner
        lblMovieSetSourcesFilenamingKodiExtendedBanner.Text = strBanner
        lblMovieSetSourcesFilenamingKodiMSAABanner.Text = strBanner

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        lblMovieSetSourcesFilenamingExpertSingleClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingKodiExtendedClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingKodiMSAAClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        lblMovieSetSourcesClearLogoExpertSingle.Text = strClearLogo
        lblMovieSetSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo
        lblMovieSetSourcesFilenamingKodiMSAAClearLogo.Text = strClearLogo

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        lblMovieSetSourcesFilenamingExpertSingleDiscArt.Text = strDiscArt
        lblMovieSetSourcesFilenamingKodiExtendedDiscArt.Text = strDiscArt

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        lblMovieSetSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblMovieSetSourcesFilenamingKodiMSAAEnabled.Text = strEnabled
        chkMovieSetUseExpert.Text = strEnabled

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        lblMovieSetSourcesFilenamingExpertSingleFanart.Text = strFanart
        lblMovieSetSourcesFilenamingKodiExtendedFanart.Text = strFanart
        lblMovieSetSourcesFilenamingKodiMSAAFanart.Text = strFanart

        'KeyArt
        Dim strKeyArt As String = Master.eLang.GetString(296, "KeyArt")
        lblMovieSetSourcesFilenamingKodiExtendedKeyArt.Text = strKeyArt

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        lblMovieSetLandscapeExpertSingle.Text = strLandscape
        lblMovieSetSourcesFilenamingKodiExtendedLandscape.Text = strLandscape
        lblMovieSetSourcesFilenamingKodiMSAALandscape.Text = strLandscape

        'Path
        Dim strPath As String = Master.eLang.GetString(410, "Path")
        lblMovieSetPathExpertSingle.Text = strPath
        lblMovieSetSourcesFilenamingKodiExtendedPath.Text = strPath
        lblMovieSetSourcesFilenamingKodiMSAAPath.Text = strPath

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        lblMovieSetPosterExpertSingle.Text = strPoster
        lblMovieSetSourcesFilenamingKodiExtendedPoster.Text = strPoster
        lblMovieSetSourcesFilenamingKodiMSAAPoster.Text = strPoster

        tpMovieSetSourcesFilenamingExpert.Text = Master.eLang.GetString(439, "Expert")
        gbMovieSetSourcesFilenamingExpertOpts.Text = Master.eLang.GetString(1181, "Expert Settings")
        gbMovieSetSourcesFilenamingKodiExtendedOpts.Text = Master.eLang.GetString(822, "Extended Images")
        gbMovieSetSourcesFilenamingOpts.Text = Master.eLang.GetString(471, "File Naming")
        gbMovieSetSourcesMiscOpts.Text = Master.eLang.GetString(429, "Miscellaneous")
        chkMovieSetCleanFiles.Text = Master.eLang.GetString(1276, "Remove Images and NFOs with MovieSets")
        tpMovieSetFilenamingExpertSingle.Text = Master.eLang.GetString(879, "Single Folder")
        chkMovieSetCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        lblMovieSetSourcesFilenamingExpertSingleNFO.Text = Master.eLang.GetString(150, "NFO")
    End Sub

#End Region 'Dialog Methods 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.ISettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Sub Addon_Order_Changed(ByVal totalCount As Integer) Implements Interfaces.ISettingsPanel.OrderChanged
        Return
    End Sub

    Public Sub SaveSettings() Implements Interfaces.ISettingsPanel.SaveSettings
        With Master.eSettings
            .MovieSetCleanDB = chkMovieSetCleanDB.Checked
            .MovieSetCleanFiles = chkMovieSetCleanFiles.Checked

            '**************** XBMC MSAA settings ***************
            .MovieSetUseMSAA = chkMovieSetUseMSAA.Checked
            .MovieSetBannerMSAA = chkMovieSetBannerMSAA.Checked
            .MovieSetClearArtMSAA = chkMovieSetClearArtMSAA.Checked
            .MovieSetClearLogoMSAA = chkMovieSetClearLogoMSAA.Checked
            .MovieSetFanartMSAA = chkMovieSetFanartMSAA.Checked
            .MovieSetLandscapeMSAA = chkMovieSetLandscapeMSAA.Checked
            .MovieSetPathMSAA = txtMovieSetPathMSAA.Text
            .MovieSetPosterMSAA = chkMovieSetPosterMSAA.Checked

            '********* XBMC Extended Images settings ***********
            .MovieSetUseExtended = chkMovieSetUseExtended.Checked
            .MovieSetBannerExtended = chkMovieSetBannerExtended.Checked
            .MovieSetClearArtExtended = chkMovieSetClearArtExtended.Checked
            .MovieSetClearLogoExtended = chkMovieSetClearLogoExtended.Checked
            .MovieSetDiscArtExtended = chkMovieSetDiscArtExtended.Checked
            .MovieSetFanartExtended = chkMovieSetFanartExtended.Checked
            .MovieSetKeyartExtended = chkMovieSetKeyArtExtended.Checked
            .MovieSetLandscapeExtended = chkMovieSetLandscapeExtended.Checked
            .MovieSetPathExtended = txtMovieSetPathExtended.Text
            .MovieSetPosterExtended = chkMovieSetPosterExtended.Checked

            '***************** Expert settings ****************
            .MovieSetUseExpert = chkMovieSetUseExpert.Checked

            '***************** Expert Single ******************
            .MovieSetBannerExpertSingle = txtMovieSetBannerExpertSingle.Text
            .MovieSetClearArtExpertSingle = txtMovieSetClearArtExpertSingle.Text
            .MovieSetClearLogoExpertSingle = txtMovieSetClearLogoExpertSingle.Text
            .MovieSetDiscArtExpertSingle = txtMovieSetDiscArtExpertSingle.Text
            .MovieSetFanartExpertSingle = txtMovieSetFanartExpertSingle.Text
            .MovieSetLandscapeExpertSingle = txtMovieSetLandscapeExpertSingle.Text
            .MovieSetNFOExpertSingle = txtMovieSetNFOExpertSingle.Text
            .MovieSetPathExpertSingle = txtMovieSetPathExpertSingle.Text
            .MovieSetPosterExpertSingle = txtMovieSetPosterExpertSingle.Text
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            chkMovieSetCleanDB.Checked = .MovieSetCleanDB
            chkMovieSetCleanFiles.Checked = .MovieSetCleanFiles

            '********* Kodi Extended Images settings ***********
            chkMovieSetUseExtended.Checked = .MovieSetUseExtended
            chkMovieSetBannerExtended.Checked = .MovieSetBannerExtended
            chkMovieSetClearArtExtended.Checked = .MovieSetClearArtExtended
            chkMovieSetClearLogoExtended.Checked = .MovieSetClearLogoExtended
            chkMovieSetDiscArtExtended.Checked = .MovieSetDiscArtExtended
            chkMovieSetFanartExtended.Checked = .MovieSetFanartExtended
            chkMovieSetKeyArtExtended.Checked = .MovieSetKeyArtExtended
            chkMovieSetLandscapeExtended.Checked = .MovieSetLandscapeExtended
            chkMovieSetPosterExtended.Checked = .MovieSetPosterExtended
            txtMovieSetPathExtended.Text = .MovieSetPathExtended

            '**************** XBMC MSAA settings ***************
            chkMovieSetUseMSAA.Checked = .MovieSetUseMSAA
            chkMovieSetBannerMSAA.Checked = .MovieSetBannerMSAA
            chkMovieSetClearArtMSAA.Checked = .MovieSetClearArtMSAA
            chkMovieSetClearLogoMSAA.Checked = .MovieSetClearLogoMSAA
            chkMovieSetFanartMSAA.Checked = .MovieSetFanartMSAA
            chkMovieSetLandscapeMSAA.Checked = .MovieSetLandscapeMSAA
            chkMovieSetPosterMSAA.Checked = .MovieSetPosterMSAA
            txtMovieSetPathMSAA.Text = .MovieSetPathMSAA

            '***************** Expert settings *****************
            chkMovieSetUseExpert.Checked = .MovieSetUseExpert

            '***************** Expert Single ******************
            txtMovieSetBannerExpertSingle.Text = .MovieSetBannerExpertSingle
            txtMovieSetClearArtExpertSingle.Text = .MovieSetClearArtExpertSingle
            txtMovieSetClearLogoExpertSingle.Text = .MovieSetClearLogoExpertSingle
            txtMovieSetDiscArtExpertSingle.Text = .MovieSetDiscArtExpertSingle
            txtMovieSetFanartExpertSingle.Text = .MovieSetFanartExpertSingle
            txtMovieSetLandscapeExpertSingle.Text = .MovieSetLandscapeExpertSingle
            txtMovieSetNFOExpertSingle.Text = .MovieSetNFOExpertSingle
            txtMovieSetPathExpertSingle.Text = .MovieSetPathExpertSingle
            txtMovieSetPosterExpertSingle.Text = .MovieSetPosterExpertSingle
        End With
    End Sub

    Private Sub chkMovieSetUseExtended_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub chkMovieSetUseMSAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        btnMovieSetPathMSAABrowse.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetBannerMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetClearArtMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetClearLogoMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetFanartMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetLandscapeMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetPosterMSAA.Enabled = chkMovieSetUseMSAA.Checked
        txtMovieSetPathMSAA.Enabled = chkMovieSetUseMSAA.Checked

        If Not chkMovieSetUseMSAA.Checked Then
            chkMovieSetBannerMSAA.Checked = False
            chkMovieSetClearArtMSAA.Checked = False
            chkMovieSetClearLogoMSAA.Checked = False
            chkMovieSetFanartMSAA.Checked = False
            chkMovieSetLandscapeMSAA.Checked = False
            chkMovieSetPosterMSAA.Checked = False
        Else
            chkMovieSetBannerMSAA.Checked = True
            chkMovieSetClearArtMSAA.Checked = True
            chkMovieSetClearLogoMSAA.Checked = True
            chkMovieSetFanartMSAA.Checked = True
            chkMovieSetLandscapeMSAA.Checked = True
            chkMovieSetPosterMSAA.Checked = True
        End If
    End Sub

    Private Sub btnMovieSetPathExtendedBrowse_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnMovieSetPathMSAABrowse_Click(sender As Object, e As EventArgs)
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieSetPathMSAA.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetPathExpertSingleBrowse_Click(sender As Object, e As EventArgs)
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieSetPathExpertSingle.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub chkMovieSetUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        btnMovieSetPathExpertSingleBrowse.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetBannerExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearArtExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearLogoExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetDiscArtExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetFanartExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetLandscapeExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetNFOExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPathExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPosterExpertSingle.Enabled = chkMovieSetUseExpert.Checked
    End Sub

    Private Sub pbMSAAInfo_Click(sender As Object, e As EventArgs)
        Process.Start("http://forum.xbmc.org/showthread.php?tid=153502")
    End Sub

    Private Sub pbMSAAInfo_MouseEnter(sender As Object, e As EventArgs)
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbMSAAInfo_MouseLeave(sender As Object, e As EventArgs)
        Cursor = Cursors.Default
    End Sub

#End Region 'Methods

End Class