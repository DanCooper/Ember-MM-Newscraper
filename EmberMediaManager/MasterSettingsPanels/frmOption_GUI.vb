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
Imports System.IO

Public Class frmOption_GUI
    Implements Interfaces.IMasterSettingsPanel

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

#Region "Handles"

    Private Sub Handle_NeedsDBClean_Movie()
        RaiseEvent NeedsDBClean_Movie()
    End Sub

    Private Sub Handle_NeedsDBClean_TV()
        RaiseEvent NeedsDBClean_TV()
    End Sub

    Private Sub Handle_NeedsDBUpdate_Movie(ByVal id As Long)
        RaiseEvent NeedsDBUpdate_Movie(id)
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV(ByVal id As Long)
        RaiseEvent NeedsDBUpdate_TV(id)
    End Sub

    Private Sub Handle_NeedsReload_Movie()
        RaiseEvent NeedsReload_Movie()
    End Sub

    Private Sub Handle_NeedsReload_MovieSet()
        RaiseEvent NeedsReload_MovieSet()
    End Sub

    Private Sub Handle_NeedsReload_TVEpisode()
        RaiseEvent NeedsReload_TVEpisode()
    End Sub

    Private Sub Handle_NeedsReload_TVShow()
        RaiseEvent NeedsReload_TVShow()
    End Sub

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Handles

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
            .Contains = Enums.SettingsPanelType.OptionsGUI,
            .ImageIndex = 0,
            .Order = 100,
            .Panel = pnlSettings,
            .SettingsPanelID = "Option_GUI",
            .Title = Master.eLang.GetString(335, "GUI"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Manager.mSettings.MainOptions.GuiSettings
            .DisplayAudioChannelsFlag = chkDisplayAudioChannelsFlag.Checked
            .DisplayAudioSourceFlag = chkDisplayAudioCodecFlag.Checked
            .DisplayBanner = chkDisplayBanner.Checked
            .DisplayCharacterArt = chkDisplayCharacterArt.Checked
            .DisplayClearArt = chkDisplayClearArt.Checked
            .DisplayClearLogo = chkDisplayClearLogo.Checked
            .DisplayDiscArt = chkDisplayDiscArt.Checked
            .DisplayFanart = chkDisplayFanart.Checked
            .DisplayFanartAsBackground = chkDisplayFanartAsBackGround.Checked
            .DisplayGenreFlags = chkDisplayGenreFlags.Checked
            .DisplayGenreText = chkDisplayGenreText.Checked
            .DisplayImgageDimensions = chkDisplayImageDimension.Checked
            .DisplayImgageGlassOverlay = chkDisplayImagesGlassOverlay.Checked
            .DisplayImgageNames = chkDisplayImageNames.Checked
            .DisplayKeyArt = chkDisplayKeyArt.Checked
            .DisplayLandscape = chkDisplayLandscape.Checked
            .DisplayLanguageFlags = chkDisplayLanguageFlags.Checked
            .DisplayPoster = chkDisplayPoster.Checked
            .DisplayStudioFlag = chkDisplayStudioFlag.Checked
            .DisplayStudioName = chkDisplayStudioName.Checked
            .DisplayVideoCodecFlag = chkDisplayVideoCodecFlag.Checked
            .DisplayVideoResolutionFlag = chkDisplayVideoResolutionFlag.Checked
            .DisplayVideoSourceFlag = chkDisplayVideoSourceFlag.Checked
            .DoubleClickScrapeEnabled = chkDoubleClickScrape.Checked
            .Theme = cbTheme.Text
        End With
        With Master.eSettings.Options.Global
            .Language = cbInterfaceLanguage.Text
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Manager.mSettings.MainOptions.GuiSettings
            cbTheme.SelectedItem = .Theme
            chkDisplayAudioChannelsFlag.Checked = .DisplayAudioChannelsFlag
            chkDisplayAudioCodecFlag.Checked = .DisplayAudioSourceFlag
            chkDisplayBanner.Checked = .DisplayBanner
            chkDisplayCharacterArt.Checked = .DisplayCharacterArt
            chkDisplayClearArt.Checked = .DisplayClearArt
            chkDisplayClearLogo.Checked = .DisplayClearLogo
            chkDisplayDiscArt.Checked = .DisplayDiscArt
            chkDisplayFanartAsBackGround.Checked = .DisplayFanartAsBackground
            chkDisplayFanart.Checked = .DisplayFanart
            chkDisplayGenreFlags.Checked = .DisplayGenreFlags
            chkDisplayGenreText.Enabled = .DisplayGenreFlags
            chkDisplayGenreText.Checked = .DisplayGenreText AndAlso .DisplayGenreFlags
            chkDisplayImageDimension.Checked = .DisplayImgageDimensions
            chkDisplayImagesGlassOverlay.Checked = .DisplayImgageGlassOverlay
            chkDisplayImageNames.Checked = .DisplayImgageNames
            chkDisplayKeyArt.Checked = .DisplayKeyArt
            chkDisplayLandscape.Checked = .DisplayLandscape
            chkDisplayLanguageFlags.Checked = .DisplayLanguageFlags
            chkDisplayPoster.Checked = .DisplayPoster
            chkDisplayStudioFlag.Checked = .DisplayStudioFlag
            chkDisplayStudioName.Enabled = .DisplayStudioFlag
            chkDisplayStudioName.Checked = .DisplayStudioName AndAlso .DisplayStudioFlag
            chkDisplayVideoCodecFlag.Checked = .DisplayVideoCodecFlag
            chkDisplayVideoResolutionFlag.Checked = .DisplayVideoResolutionFlag
            chkDisplayVideoSourceFlag.Checked = .DisplayVideoSourceFlag
            chkDoubleClickScrape.Checked = .DoubleClickScrapeEnabled
        End With
        With Master.eSettings.Options.Global
            RemoveHandler cbInterfaceLanguage.SelectedIndexChanged, AddressOf InterfaceLanguage_SelectedIndexChanged
            cbInterfaceLanguage.SelectedItem = .Language
            AddHandler cbInterfaceLanguage.SelectedIndexChanged, AddressOf InterfaceLanguage_SelectedIndexChanged
        End With
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkDisplayBanner.Text = .GetString(838, "Banner")
            chkDisplayCharacterArt.Text = .GetString(1140, "CharacterArt")
            chkDisplayClearArt.Text = .GetString(1096, "ClearArt")
            chkDisplayClearLogo.Text = .GetString(1097, "ClearLogo")
            chkDisplayDiscArt.Text = .GetString(1098, "DiscArt")
            chkDisplayFanart.Text = .GetString(149, "Fanart")
            chkDisplayFanartAsBackGround.Text = .GetString(967, "Fanart as Background")
            chkDisplayGenreText.Text = .GetString(453, "Always Display Genre Text")
            chkDisplayImageDimension.Text = .GetString(457, "Image Dimensions")
            chkDisplayImageNames.Text = .GetString(1255, "Image Names")
            chkDisplayLandscape.Text = .GetString(1035, "Landscape")
            chkDisplayLanguageFlags.Text = .GetString(489, "Language Flags")
            chkDisplayPoster.Text = .GetString(148, "Poster")
            chkDisplayStudioName.Text = .GetString(491, "Always Display Studio Name")
            chkDoubleClickScrape.Text = .GetString(1198, "Enable Image Scrape On Double Right Click")
            chkDisplayImagesGlassOverlay.Text = .GetString(966, "Enable Images Glass Overlay")
            gbInterface.Text = .GetString(795, "Interface")
            gbDisplayInMainWindow.Text = .GetString(492, "Display in Main Window")
            lblInterfaceLanguage.Text = .GetString(430, "Interface Language:")
            lblTheme.Text = String.Concat(.GetString(620, "Theme"), ":")
        End With
        Load_InterfaceLanguages()
        Load_Themes()
    End Sub
    Private Sub Enable_ApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkDisplayAudioChannelsFlag.CheckedChanged,
        chkDisplayAudioCodecFlag.CheckedChanged,
        chkDisplayBanner.CheckedChanged,
        chkDisplayCharacterArt.CheckedChanged,
        chkDisplayClearArt.CheckedChanged,
        chkDisplayClearLogo.CheckedChanged,
        chkDisplayDiscArt.CheckedChanged,
        chkDisplayFanart.CheckedChanged,
        chkDisplayFanartAsBackGround.CheckedChanged,
        chkDisplayGenreText.CheckedChanged,
        chkDisplayImageDimension.CheckedChanged,
        chkDisplayImageNames.CheckedChanged,
        chkDisplayKeyArt.CheckedChanged,
        chkDisplayLandscape.CheckedChanged,
        chkDisplayLanguageFlags.CheckedChanged,
        chkDisplayPoster.CheckedChanged,
        chkDisplayStudioName.CheckedChanged,
        chkDisplayVideoCodecFlag.CheckedChanged,
        chkDisplayVideoResolutionFlag.CheckedChanged,
        chkDisplayVideoSourceFlag.CheckedChanged,
        chkDoubleClickScrape.CheckedChanged,
        chkDisplayImagesGlassOverlay.CheckedChanged

        CheckHideSettings()
        Handle_SettingsChanged()
    End Sub

    Private Sub CheckHideSettings()
        If chkDisplayBanner.Checked OrElse
            chkDisplayCharacterArt.Checked OrElse
            chkDisplayClearArt.Checked OrElse
            chkDisplayClearLogo.Checked OrElse
            chkDisplayDiscArt.Checked OrElse
            chkDisplayFanart.Checked OrElse
            chkDisplayFanartAsBackGround.Checked OrElse
            chkDisplayKeyArt.Checked OrElse
            chkDisplayLandscape.Checked OrElse
            chkDisplayPoster.Checked Then
            chkDisplayImagesGlassOverlay.Enabled = True
            chkDisplayImageDimension.Enabled = True
            chkDisplayImageNames.Enabled = True
        Else
            chkDisplayImagesGlassOverlay.Enabled = False
            chkDisplayImageDimension.Enabled = False
            chkDisplayImageNames.Enabled = False
        End If
    End Sub

    Private Sub DisplayGenreFlags_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisplayGenreFlags.CheckedChanged
        chkDisplayGenreText.Enabled = chkDisplayGenreFlags.Checked
        If Not chkDisplayGenreFlags.Checked Then chkDisplayGenreText.Checked = False
        Handle_SettingsChanged()
    End Sub

    Private Sub DisplayStudioFlag_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisplayStudioFlag.CheckedChanged
        chkDisplayStudioName.Enabled = chkDisplayStudioFlag.Checked
        If Not chkDisplayStudioFlag.Checked Then chkDisplayStudioName.Checked = False
        Handle_SettingsChanged()
    End Sub

    Private Sub InterfaceLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Master.eLang.LoadAllLanguage(cbInterfaceLanguage.SelectedItem.ToString, True)
        Setup()
        Cursor.Current = Cursors.Default
        Handle_SettingsChanged()
    End Sub

    Private Sub Load_InterfaceLanguages()
        cbInterfaceLanguage.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            Try
                alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            Catch
            End Try
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToList)
            cbInterfaceLanguage.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub Load_Themes()
        cbTheme.Items.Clear()
        Dim diDefaults As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Themes"))
        If diDefaults.Exists Then cbTheme.Items.AddRange(diDefaults.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)

        Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Themes"))
        If diCustom.Exists Then cbTheme.Items.AddRange(diCustom.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)
    End Sub

#End Region 'Methods
End Class