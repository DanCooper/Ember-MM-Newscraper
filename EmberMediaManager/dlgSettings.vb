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
' #
' # Dialog size: 1230; 1100
' # Move the panels (pnl*) from 1200;1200 to 0;0 to edit. Move it back after editing.

Imports System.IO
Imports System.Net
Imports EmberAPI
Imports NLog

Public Class dlgSettings

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _currPanel As New Panel
    Private _currbutton As New ButtonTag
    Private didApply As Boolean = False
    Private _SettingsPanels As New List(Of Containers.SettingsPanel)
    Private _MasterSettingsPanels As New List(Of Interfaces.IMasterSettingsPanel)

    Private GeneralAudioCodecMapping As New List(Of Settings.CodecMapping)
    Private GeneralVideoCodecMapping As New List(Of Settings.CodecMapping)
    Private GeneralVideoSourceByExtension As New List(Of Settings.VideoSourceByExtension)
    Private GeneralVideoSourceByRegex As New List(Of Settings.VideoSourceByRegex)
    Private MovieGeneralMediaListSorting As New List(Of Settings.ListSorting)
    Private MovieSetGeneralMediaListSorting As New List(Of Settings.ListSorting)
    Private TempTVScraperSeasonTitleBlacklist As New List(Of String)
    Private TVGeneralEpisodeListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralSeasonListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralShowListSorting As New List(Of Settings.ListSorting)
    Private NoUpdate As Boolean = True
    Private TVShowMatching As New List(Of Settings.regexp)
    Private sResult As New Structures.SettingsResult
    Private TVMeta As New List(Of Settings.MetadataPerType)
    Public Event LoadEnd()

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Overloads Function ShowDialog() As Structures.SettingsResult
        MyBase.ShowDialog()
        Return sResult
    End Function

    Private Sub dlgSettings_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Dim iBackground As New Bitmap(pnlSettingsTop.Width, pnlSettingsTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsTop.ClientRectangle)
            pnlSettingsTop.BackgroundImage = iBackground
        End Using

        iBackground = New Bitmap(pnlSettingsCurrent.Width, pnlSettingsCurrent.Height)
        Using b As Graphics = Graphics.FromImage(iBackground)
            b.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsCurrent.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsCurrent.ClientRectangle)
            pnlSettingsCurrent.BackgroundImage = iBackground
        End Using

        If tsSettingsTopMenu.Items.Count > 0 Then
            Dim ButtonsWidth As Integer = 0
            Dim ButtonsCount As Integer = 0
            Dim sLength As Integer = 0
            Dim sRest As Double = 0
            Dim sSpacer As String = String.Empty

            'calculate the buttons width and count
            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If TypeOf item Is ToolStripButton Then
                    ButtonsWidth += item.Width
                    ButtonsCount += 1
                End If
            Next

            sRest = (tsSettingsTopMenu.Width - ButtonsWidth - 1) / (ButtonsCount + 1)

            'formula to calculate the count of spaces to reach the label.width
            'spaces (x) to width (y) in px: 1 = 10, 2 = 13, 3 = 16, 4 = 19, 5 = 22
            'x = 10 + ((y - 1) * 3) or
            'y = (x - 10) / 3 + 1
            sLength = Convert.ToInt32((sRest - 10) / 3 + 1)

            If Not sRest < 10 Then
                sSpacer = New String(Convert.ToChar(" "), sLength)
            Else
                sSpacer = New String(Convert.ToChar(" "), 1)
            End If

            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If item.Tag IsNot Nothing AndAlso item.Tag.ToString = "spacer" Then
                    item.Text = sSpacer
                End If
            Next
        End If
    End Sub

    Private Sub AddButtons()
        Dim lstTSB As New List(Of ToolStripButton)
        Dim nTSB As ToolStripButton

        tsSettingsTopMenu.Items.Clear()

        'first create all the buttons so we can get their size to calculate the spacer
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(390, "Options"),
            .Image = My.Resources.General,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.ePanelType = Enums.SettingsPanelType.Options, .iIndex = 100, .strTitle = Master.eLang.GetString(390, "Options")}}
        AddHandler nTSB.Click, AddressOf ToolStripButton_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(36, "Movies"),
            .Image = My.Resources.Movie,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.ePanelType = Enums.SettingsPanelType.Movie, .iIndex = 200, .strTitle = Master.eLang.GetString(36, "Movies")}}
        AddHandler nTSB.Click, AddressOf ToolStripButton_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(1203, "MovieSets"),
            .Image = My.Resources.MovieSet,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.ePanelType = Enums.SettingsPanelType.Movieset, .iIndex = 300, .strTitle = Master.eLang.GetString(1203, "MovieSets")}}
        AddHandler nTSB.Click, AddressOf ToolStripButton_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(653, "TV Shows"),
            .Image = My.Resources.TVShows,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.ePanelType = Enums.SettingsPanelType.TV, .iIndex = 400, .strTitle = Master.eLang.GetString(653, "TV Shows")}}
        AddHandler nTSB.Click, AddressOf ToolStripButton_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(802, "Addons"),
            .Image = My.Resources.modules,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.ePanelType = Enums.SettingsPanelType.Addon, .iIndex = 500, .strTitle = Master.eLang.GetString(802, "Addons")}}
        AddHandler nTSB.Click, AddressOf ToolStripButton_Click
        lstTSB.Add(nTSB)

        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(429, "Miscellaneous"),
            .Image = My.Resources.Miscellaneous,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.ePanelType = Enums.SettingsPanelType.Core, .iIndex = 600, .strTitle = Master.eLang.GetString(429, "Miscellaneous")}}
        AddHandler nTSB.Click, AddressOf ToolStripButton_Click
        lstTSB.Add(nTSB)

        If lstTSB.Count > 0 Then
            Dim ButtonsWidth As Integer = 0
            Dim ButtonsCount As Integer = 0
            Dim sLength As Integer = 0
            Dim sRest As Double = 0
            Dim sSpacer As String = String.Empty

            'add all buttons to the top horizontal menu
            For Each tButton As ToolStripButton In lstTSB.OrderBy(Function(b) DirectCast(b.Tag, ButtonTag).iIndex)
                tsSettingsTopMenu.Items.Add(New ToolStripLabel With {.Text = String.Empty, .Tag = "spacer"})
                tsSettingsTopMenu.Items.Add(tButton)
            Next

            'calculate the buttons width and count
            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If TypeOf item Is ToolStripButton Then
                    ButtonsWidth += item.Width
                    ButtonsCount += 1
                End If
            Next

            sRest = (tsSettingsTopMenu.Width - ButtonsWidth - 1) / (ButtonsCount + 1)

            'formula to calculate the count of spaces to reach the label.width
            'spaces (x) to width (y) in px: 1 = 10, 2 = 13, 3 = 16, 4 = 19, 5 = 22
            'x = 10 + ((y - 1) * 3) or
            'y = (x - 10) / 3 + 1
            sLength = Convert.ToInt32((sRest - 10) / 3 + 1)

            If Not sRest < 10 Then
                sSpacer = New String(Convert.ToChar(" "), sLength)
            Else
                sSpacer = New String(Convert.ToChar(" "), 1)
            End If

            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If item.Tag IsNot Nothing AndAlso item.Tag.ToString = "spacer" Then
                    item.Text = sSpacer
                End If
            Next

            'set default page
            _currbutton = DirectCast(lstTSB.Item(0).Tag, ButtonTag)
            FillList(DirectCast(lstTSB.Item(0).Tag, ButtonTag).ePanelType)
        End If
    End Sub

    Private Sub SettingsPanels_AddMasterPanels()
        _MasterSettingsPanels.Add(frmMovie_Data)
        _MasterSettingsPanels.Add(frmMovie_FileNaming)
        '_lstMasterSettingsPanels.Add(frmMovie_GUI)
        _MasterSettingsPanels.Add(frmMovie_Image)
        '_lstMasterSettingsPanels.Add(frmMovie_Theme)
        '_lstMasterSettingsPanels.Add(frmMovie_Trailer)
        '_lstMasterSettingsPanels.Add(frmMovie_Source)
        '_lstMasterSettingsPanels.Add(frmMovieSet_Data)
        '_lstMasterSettingsPanels.Add(frmMovieSet_FileNaming)
        '_lstMasterSettingsPanels.Add(frmMovieSet_GUI)
        '_lstMasterSettingsPanels.Add(frmMovieSet_Image)
        '_lstMasterSettingsPanels.Add(frmOption_FileSystem)
        '_lstMasterSettingsPanels.Add(frmOption_GUI)
        '_lstMasterSettingsPanels.Add(frmOption_Proxy)
        '_lstMasterSettingsPanels.Add(frmTV_FileNaming)
        '_lstMasterSettingsPanels.Add(frmTV_GUI)
        '_lstMasterSettingsPanels.Add(frmTV_Image)
        '_lstMasterSettingsPanels.Add(frmTV_Source)
        '_lstMasterSettingsPanels.Add(frmTV_Theme)

        For Each s As Interfaces.IMasterSettingsPanel In _MasterSettingsPanels
            Dim nPanel As Containers.SettingsPanel = s.InjectSettingsPanel()
            If nPanel IsNot Nothing Then
                _SettingsPanels.Add(nPanel)
                AddHandler s.NeedsDBClean_Movie, AddressOf Handle_NeedsDBClean_Movie
                AddHandler s.NeedsDBClean_TV, AddressOf Handle_NeedsDBClean_TV
                AddHandler s.NeedsDBUpdate_Movie, AddressOf Handle_NeedsDBUpdate_Movie
                AddHandler s.NeedsDBUpdate_TV, AddressOf Handle_NeedsDBUpdate_TV
                AddHandler s.NeedsReload_Movie, AddressOf Handle_NeedsReload_Movie
                AddHandler s.NeedsReload_MovieSet, AddressOf Handle_NeedsReload_MovieSet
                AddHandler s.NeedsReload_TVEpisode, AddressOf Handle_NeedsReload_TVEpisode
                AddHandler s.NeedsReload_TVShow, AddressOf Handle_NeedsReload_TVShow
                AddHandler s.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.SettingsChanged, AddressOf Handle_SettingsChanged
            End If
        Next

        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovies",
        '     .Text = Master.eLang.GetString(38, "General"),
        '     .ImageIndex = 2,
        '     .Type = Master.eLang.GetString(36, "Movies"),
        '     .Panel = pnlMovieGeneral,
        '     .Order = 100})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlSources",
        '     .Text = Master.eLang.GetString(555, "Files and Sources"),
        '     .ImageIndex = 5,
        '     .Type = Master.eLang.GetString(36, "Movies"),
        '     .Panel = pnlMovieSources,
        '     .Order = 200})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieData",
        '     .Text = Master.eLang.GetString(556, "Scrapers - Data"),
        '     .ImageIndex = 3,
        '     .Type = Master.eLang.GetString(36, "Movies"),
        '     .Panel = pnlMovieScraper,
        '     .Order = 300})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieMedia",
        '     .Text = Master.eLang.GetString(557, "Scrapers - Images"),
        '     .ImageIndex = 6,
        '     .Type = Master.eLang.GetString(36, "Movies"),
        '     .Panel = pnlMovieImages,
        '     .Order = 400})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieTrailer",
        '     .Text = Master.eLang.GetString(559, "Scrapers - Trailers"),
        '     .ImageIndex = 6,
        '     .Type = Master.eLang.GetString(36, "Movies"),
        '     .Panel = pnlMovieTrailers,
        '     .Order = 500})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieTheme",
        '     .Text = Master.eLang.GetString(1068, "Scrapers - Themes"),
        '     .ImageIndex = 11,
        '     .Type = Master.eLang.GetString(36, "Movies"),
        '     .Panel = pnlMovieThemes,
        '     .Order = 600})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieSets",
        '     .Text = Master.eLang.GetString(38, "General"),
        '     .ImageIndex = 2,
        '     .Type = Master.eLang.GetString(1203, "MovieSets"),
        '     .Panel = pnlMovieSetGeneral,
        '     .Order = 100})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieSetSources",
        '     .Text = Master.eLang.GetString(555, "Files and Sources"),
        '     .ImageIndex = 5,
        '     .Type = Master.eLang.GetString(1203, "MovieSets"),
        '     .Panel = pnlMovieSetSources,
        '     .Order = 200})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieSetData",
        '     .Text = Master.eLang.GetString(556, "Scrapers - Data"),
        '     .ImageIndex = 3,
        '     .Type = Master.eLang.GetString(1203, "MovieSets"),
        '     .Panel = pnlMovieSetScraper,
        '     .Order = 300})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlMovieSetMedia",
        '     .Text = Master.eLang.GetString(557, "Scrapers - Images"),
        '     .ImageIndex = 6,
        '     .Type = Master.eLang.GetString(1203, "MovieSets"),
        '     .Panel = pnlMovieSetImages,
        '     .Order = 400})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlShows",
        '     .Text = Master.eLang.GetString(38, "General"),
        '     .ImageIndex = 7,
        '     .Type = Master.eLang.GetString(653, "TV Shows"),
        '     .Panel = pnlTVGeneral,
        '     .Order = 100})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlTVSources",
        '     .Text = Master.eLang.GetString(555, "Files and Sources"),
        '     .ImageIndex = 5,
        '     .Type = Master.eLang.GetString(653, "TV Shows"),
        '     .Panel = pnlTVSources,
        '     .Order = 200})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlTVData",
        '     .Text = Master.eLang.GetString(556, "Scrapers - Data"),
        '     .ImageIndex = 3,
        '     .Type = Master.eLang.GetString(653, "TV Shows"),
        '     .Panel = pnlTVScraper,
        '     .Order = 300})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlTVMedia",
        '     .Text = Master.eLang.GetString(557, "Scrapers - Images"),
        '     .ImageIndex = 6,
        '     .Type = Master.eLang.GetString(653, "TV Shows"),
        '     .Panel = pnlTVImages,
        '     .Order = 400})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlTVTheme",
        '     .Text = Master.eLang.GetString(1068, "Scrapers - Themes"),
        '     .ImageIndex = 11,
        '     .Type = Master.eLang.GetString(653, "TV Shows"),
        '     .Panel = pnlTVThemes,
        '     .Order = 500})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlGeneral",
        '     .Text = Master.eLang.GetString(38, "General"),
        '     .ImageIndex = 0,
        '     .Type = Master.eLang.GetString(390, "Options"),
        '     .Panel = pnlGeneral,
        '     .Order = 100})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlExtensions",
        '     .Text = Master.eLang.GetString(553, "File System"),
        '     .ImageIndex = 4,
        '     .Type = Master.eLang.GetString(390, "Options"),
        '     .Panel = pnlFileSystem,
        '     .Order = 200})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlProxy",
        '     .Text = Master.eLang.GetString(421, "Connection"),
        '     .ImageIndex = 1,
        '     .Type = Master.eLang.GetString(390, "Options"),
        '     .Panel = pnlProxy,
        '     .Order = 300})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlGeneralAVCodecMapping",
        '     .Text = Master.eLang.GetString(785, "Audio & Video Codec Mapping"),
        '     .ImageIndex = 13,
        '     .Type = Master.eLang.GetString(390, "Options"),
        '     .Panel = pnlGeneralAVCodecMapping,
        '     .Order = 400})
        '_settingsPanels.Add(New Containers.SettingsPanel With {
        '     .Name = "pnlGeneralVideoSourceMapping",
        '     .Text = Master.eLang.GetString(784, "Video Source Mapping"),
        '     .ImageIndex = 12,
        '     .Type = Master.eLang.GetString(390, "Options"),
        '     .Panel = pnlGeneralVideoSourceMapping,
        '     .Order = 500}) 
    End Sub

    Sub SettingsPanels_AddAddonPanels()
        Dim iPanelCounter As Integer = 0
        Dim nPanel As Containers.SettingsPanel = Nothing
        For Each s As ModulesManager._externalScraperModuleClass_Data_Movie In ModulesManager.Instance.externalScrapersModules_Data_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Data_MovieSet In ModulesManager.Instance.externalScrapersModules_Data_MovieSet.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Data_TV In ModulesManager.Instance.externalScrapersModules_Data_TV.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In ModulesManager.Instance.externalScrapersModules_Image_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In ModulesManager.Instance.externalScrapersModules_Image_Movieset.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Image_TV In ModulesManager.Instance.externalScrapersModules_Image_TV.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In ModulesManager.Instance.externalScrapersModules_Theme_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In ModulesManager.Instance.externalScrapersModules_Trailer_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalGenericModules.OrderBy(Function(f) f.AssemblyName)
            s.ProcessorModule.InjectSettingsPanel()
            nPanel = s.ProcessorModule.SettingsPanel
            If nPanel IsNot Nothing AndAlso nPanel.Panel IsNot Nothing Then
                nPanel.Order = iPanelCounter
                nPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.ContentType)
                If nPanel.ImageIndex = -1 AndAlso nPanel.Image IsNot Nothing Then
                    ilSettings.Images.Add(nPanel.SettingsPanelID, nPanel.Image)
                    nPanel.ImageIndex = ilSettings.Images.IndexOfKey(nPanel.SettingsPanelID)
                End If
                _SettingsPanels.Add(nPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
    End Sub

    Sub SettingsPanels_RemoveAllPanels()
        'SettingsPanels
        For Each s As Interfaces.IMasterSettingsPanel In _MasterSettingsPanels
            RemoveHandler s.NeedsDBClean_Movie, AddressOf Handle_NeedsDBClean_Movie
            RemoveHandler s.NeedsDBClean_TV, AddressOf Handle_NeedsDBClean_TV
            RemoveHandler s.NeedsDBUpdate_Movie, AddressOf Handle_NeedsDBUpdate_Movie
            RemoveHandler s.NeedsDBUpdate_TV, AddressOf Handle_NeedsDBUpdate_TV
            RemoveHandler s.NeedsReload_Movie, AddressOf Handle_NeedsReload_Movie
            RemoveHandler s.NeedsReload_MovieSet, AddressOf Handle_NeedsReload_MovieSet
            RemoveHandler s.NeedsReload_TVEpisode, AddressOf Handle_NeedsReload_TVEpisode
            RemoveHandler s.NeedsReload_TVShow, AddressOf Handle_NeedsReload_TVShow
            RemoveHandler s.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.SettingsChanged, AddressOf Handle_SettingsChanged
            s.DoDispose()
        Next

        'AddonSettingsPanels
        For Each s As ModulesManager._externalScraperModuleClass_Data_Movie In ModulesManager.Instance.externalScrapersModules_Data_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Data_MovieSet In ModulesManager.Instance.externalScrapersModules_Data_MovieSet
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Data_TV In ModulesManager.Instance.externalScrapersModules_Data_TV
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In ModulesManager.Instance.externalScrapersModules_Image_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In ModulesManager.Instance.externalScrapersModules_Image_Movieset
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_TV In ModulesManager.Instance.externalScrapersModules_Image_TV
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In ModulesManager.Instance.externalScrapersModules_Theme_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In ModulesManager.Instance.externalScrapersModules_Trailer_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalGenericModules
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If Not didApply Then sResult.DidCancel = True
        Master.eLang.LoadAllLanguage(Master.eSettings.GeneralLanguage, True)
        SettingsPanels_RemoveAllPanels()
        Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        NoUpdate = True
        SaveSettings(False)
        SettingsPanels_RemoveAllPanels()
        Close()
    End Sub

    Private Sub Handle_NeedsDBClean_Movie()
        sResult.NeedsDBClean_Movie = True
    End Sub

    Private Sub Handle_NeedsDBClean_TV()
        sResult.NeedsDBClean_TV = True
    End Sub

    Private Sub Handle_NeedsDBUpdate_Movie()
        sResult.NeedsDBUpdate_Movie = True
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV()
        sResult.NeedsDBUpdate_TV = True
    End Sub

    Private Sub Handle_NeedsReload_Movie()
        sResult.NeedsReload_Movie = True
    End Sub

    Private Sub Handle_NeedsReload_MovieSet()
        sResult.NeedsReload_MovieSet = True
    End Sub

    Private Sub Handle_NeedsReload_TVEpisode()
        sResult.NeedsReload_TVEpisode = True
    End Sub

    Private Sub Handle_NeedsReload_TVShow()
        sResult.NeedsReload_TVShow = True
    End Sub

    Private Sub Handle_NeedsRestart()
        sResult.NeedsRestart = True
    End Sub

    Private Sub Handle_SettingsChanged()
        SetApplyButton(True)
    End Sub

    Private Sub btnTVEpisodeFilterAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterAdd.Click
        If Not String.IsNullOrEmpty(txtTVEpisodeFilter.Text) Then
            lstTVEpisodeFilter.Items.Add(txtTVEpisodeFilter.Text)
            txtTVEpisodeFilter.Text = String.Empty
            SetApplyButton(True)
            sResult.NeedsReload_TVEpisode = True
        End If

        txtTVEpisodeFilter.Focus()
    End Sub

    Private Sub btnMovieFilterAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterAdd.Click
        If Not String.IsNullOrEmpty(txtMovieFilter.Text) Then
            lstMovieFilters.Items.Add(txtMovieFilter.Text)
            txtMovieFilter.Text = String.Empty
            SetApplyButton(True)
            sResult.NeedsReload_Movie = True
        End If

        txtMovieFilter.Focus()
    End Sub

    Private Sub btnFileSystemExcludedPathsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemExcludedPathsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemExcludedPaths.Text) Then
            If Not lstFileSystemExcludedPaths.Items.Contains(txtFileSystemExcludedPaths.Text.ToLower) Then
                AddExcludedPath(txtFileSystemExcludedPaths.Text)
                RefreshFileSystemExcludedPaths()
                txtFileSystemExcludedPaths.Text = String.Empty
                txtFileSystemExcludedPaths.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemExcludedPathsBrowse_Click(sender As Object, e As EventArgs) Handles btnFileSystemExcludedPathsBrowse.Click
        With fbdBrowse
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    txtFileSystemExcludedPaths.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnFileSystemExcludedPathsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemExcludedPathsRemove.Click
        RemoveExcludedPath()
        RefreshFileSystemExcludedPaths()
    End Sub

    Private Sub lstFileSystemExcludedPaths_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemExcludedPaths.KeyDown
        If e.KeyCode = Keys.Delete Then
            RemoveExcludedPath()
            RefreshFileSystemExcludedPaths()
        End If
    End Sub

    Private Sub AddExcludedPath(ByVal path As String)
        Master.DB.AddExcludedPath(path)
        SetApplyButton(True)
        sResult.NeedsDBClean_Movie = True
        sResult.NeedsDBClean_TV = True
    End Sub

    Private Sub RemoveExcludedPath()
        If lstFileSystemExcludedPaths.SelectedItems.Count > 0 Then
            lstFileSystemExcludedPaths.BeginUpdate()

            Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                While lstFileSystemExcludedPaths.SelectedItems.Count > 0
                    Master.DB.RemoveExcludedPath(lstFileSystemExcludedPaths.SelectedItems(0).ToString, True)
                    lstFileSystemExcludedPaths.Items.Remove(lstFileSystemExcludedPaths.SelectedItems(0))
                End While
                SQLTransaction.Commit()
            End Using

            lstFileSystemExcludedPaths.EndUpdate()
            lstFileSystemExcludedPaths.Refresh()

            SetApplyButton(True)
            sResult.NeedsDBUpdate_Movie = True
            sResult.NeedsDBUpdate_TV = True
        End If
    End Sub

    Private Sub btnFileSystemValidVideoExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidVideoExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidVideoExts.Text) Then
            If Not txtFileSystemValidVideoExts.Text.Substring(0, 1) = "." Then txtFileSystemValidVideoExts.Text = String.Concat(".", txtFileSystemValidVideoExts.Text)
            If Not lstFileSystemValidVideoExts.Items.Contains(txtFileSystemValidVideoExts.Text.ToLower) Then
                lstFileSystemValidVideoExts.Items.Add(txtFileSystemValidVideoExts.Text.ToLower)
                SetApplyButton(True)
                sResult.NeedsDBUpdate_Movie = True
                sResult.NeedsDBUpdate_TV = True
                txtFileSystemValidVideoExts.Text = String.Empty
                txtFileSystemValidVideoExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidSubtitlesExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidSubtitlesExts.Text) Then
            If Not txtFileSystemValidSubtitlesExts.Text.Substring(0, 1) = "." Then txtFileSystemValidSubtitlesExts.Text = String.Concat(".", txtFileSystemValidSubtitlesExts.Text)
            If Not lstFileSystemValidSubtitlesExts.Items.Contains(txtFileSystemValidSubtitlesExts.Text.ToLower) Then
                lstFileSystemValidSubtitlesExts.Items.Add(txtFileSystemValidSubtitlesExts.Text.ToLower)
                SetApplyButton(True)
                sResult.NeedsReload_Movie = True
                sResult.NeedsReload_TVEpisode = True
                txtFileSystemValidSubtitlesExts.Text = String.Empty
                txtFileSystemValidSubtitlesExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidThemeExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidThemeExts.Text) Then
            If Not txtFileSystemValidThemeExts.Text.Substring(0, 1) = "." Then txtFileSystemValidThemeExts.Text = String.Concat(".", txtFileSystemValidThemeExts.Text)
            If Not lstFileSystemValidThemeExts.Items.Contains(txtFileSystemValidThemeExts.Text.ToLower) Then
                lstFileSystemValidThemeExts.Items.Add(txtFileSystemValidThemeExts.Text.ToLower)
                SetApplyButton(True)
                sResult.NeedsReload_Movie = True
                sResult.NeedsReload_TVShow = True
                txtFileSystemValidThemeExts.Text = String.Empty
                txtFileSystemValidThemeExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemNoStackExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemNoStackExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemNoStackExts.Text) Then
            If Not txtFileSystemNoStackExts.Text.Substring(0, 1) = "." Then txtFileSystemNoStackExts.Text = String.Concat(".", txtFileSystemNoStackExts.Text)
            If Not lstFileSystemNoStackExts.Items.Contains(txtFileSystemNoStackExts.Text) Then
                lstFileSystemNoStackExts.Items.Add(txtFileSystemNoStackExts.Text)
                SetApplyButton(True)
                sResult.NeedsDBUpdate_Movie = True
                sResult.NeedsDBUpdate_TV = True
                txtFileSystemNoStackExts.Text = String.Empty
                txtFileSystemNoStackExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVShowFilterAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterAdd.Click
        If Not String.IsNullOrEmpty(txtTVShowFilter.Text) Then
            lstTVShowFilter.Items.Add(txtTVShowFilter.Text)
            txtTVShowFilter.Text = String.Empty
            SetApplyButton(True)
            sResult.NeedsReload_TVShow = True
        End If

        txtTVShowFilter.Focus()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingAdd.Click
        If String.IsNullOrEmpty(btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then
            Dim lID = (From lRegex As Settings.regexp In TVShowMatching Select lRegex.ID).Max
            TVShowMatching.Add(New Settings.regexp With {
                               .ID = Convert.ToInt32(lID) + 1,
                               .Regexp = txtTVSourcesRegexTVShowMatchingRegex.Text,
                               .defaultSeason = If(String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text) OrElse Not Integer.TryParse(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0), -2, CInt(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text)),
                               .byDate = chkTVSourcesRegexTVShowMatchingByDate.Checked})
        Else
            Dim selRex = From lRegex As Settings.regexp In TVShowMatching Where lRegex.ID = Convert.ToInt32(btnTVSourcesRegexTVShowMatchingAdd.Tag)
            If selRex.Count > 0 Then
                selRex(0).Regexp = txtTVSourcesRegexTVShowMatchingRegex.Text
                selRex(0).defaultSeason = CInt(If(String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text), "-2", txtTVSourcesRegexTVShowMatchingDefaultSeason.Text))
                selRex(0).byDate = chkTVSourcesRegexTVShowMatchingByDate.Checked
            End If
        End If

        ClearTVShowMatching()
        SetApplyButton(True)
        LoadTVShowMatching()
    End Sub

    Private Sub btnMovieSortTokenAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtMovieSortToken.Text) Then
            If Not lstMovieSortTokens.Items.Contains(txtMovieSortToken.Text) Then
                lstMovieSortTokens.Items.Add(txtMovieSortToken.Text)
                SetApplyButton(True)
                txtMovieSortToken.Text = String.Empty
                txtMovieSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnMovieSetSortTokenAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtMovieSetSortToken.Text) Then
            If Not lstMovieSetSortTokens.Items.Contains(txtMovieSetSortToken.Text) Then
                lstMovieSetSortTokens.Items.Add(txtMovieSetSortToken.Text)
                SetApplyButton(True)
                txtMovieSetSortToken.Text = String.Empty
                txtMovieSetSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVSortTokenAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtTVSortToken.Text) Then
            If Not lstTVSortTokens.Items.Contains(txtTVSortToken.Text) Then
                lstTVSortTokens.Items.Add(txtTVSortToken.Text)
                SetApplyButton(True)
                txtTVSortToken.Text = String.Empty
                txtTVSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVSourceAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourceAdd.Click
        Using dSource As New dlgSourceTVShow
            If dSource.ShowDialog = DialogResult.OK Then
                RefreshTVSources()
                SetApplyButton(True)
                sResult.NeedsDBUpdate_TV = True
            End If
        End Using
    End Sub

    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
        SaveSettings(True)
        SetApplyButton(False)
        If sResult.NeedsDBClean_Movie OrElse
            sResult.NeedsDBClean_TV OrElse
            sResult.NeedsDBUpdate_Movie OrElse
            sResult.NeedsDBUpdate_TV OrElse
            sResult.NeedsReload_Movie OrElse
            sResult.NeedsReload_MovieSet OrElse
            sResult.NeedsReload_TVShow Then
            didApply = True
        End If
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingClear.Click
        ClearTVShowMatching()
    End Sub

    Private Sub btnMovieFilterDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterDown.Click
        Try
            If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItem IsNot Nothing AndAlso lstMovieFilters.SelectedIndex < (lstMovieFilters.Items.Count - 1) Then
                Dim iIndex As Integer = lstMovieFilters.SelectedIndices(0)
                lstMovieFilters.Items.Insert(iIndex + 2, lstMovieFilters.SelectedItems(0))
                lstMovieFilters.Items.RemoveAt(iIndex)
                lstMovieFilters.SelectedIndex = iIndex + 1
                SetApplyButton(True)
                sResult.NeedsReload_Movie = True
                lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieGeneralCustomMarker1_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker1.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker1.BackColor = .Color
                    SetApplyButton(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker2_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker2.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker2.BackColor = .Color
                    SetApplyButton(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker3_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker3.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker3.BackColor = .Color
                    SetApplyButton(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker4_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker4.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker4.BackColor = .Color
                    SetApplyButton(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingEdit.Click
        If lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub btnTVSourceEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourceEdit.Click
        If lvTVSources.SelectedItems.Count > 0 Then
            Using dTVSource As New dlgSourceTVShow
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = DialogResult.OK Then
                    RefreshTVSources()
                    sResult.NeedsReload_TVShow = True
                    SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub btnTVEpisodeFilterDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterDown.Click
        Try
            If lstTVEpisodeFilter.Items.Count > 0 AndAlso lstTVEpisodeFilter.SelectedItem IsNot Nothing AndAlso lstTVEpisodeFilter.SelectedIndex < (lstTVEpisodeFilter.Items.Count - 1) Then
                Dim iIndex As Integer = lstTVEpisodeFilter.SelectedIndices(0)
                lstTVEpisodeFilter.Items.Insert(iIndex + 2, lstTVEpisodeFilter.SelectedItems(0))
                lstTVEpisodeFilter.Items.RemoveAt(iIndex)
                lstTVEpisodeFilter.SelectedIndex = iIndex + 1
                SetApplyButton(True)
                sResult.NeedsReload_TVEpisode = True
                lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVEpisodeFilterUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterUp.Click
        Try
            If lstTVEpisodeFilter.Items.Count > 0 AndAlso lstTVEpisodeFilter.SelectedItem IsNot Nothing AndAlso lstTVEpisodeFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = lstTVEpisodeFilter.SelectedIndices(0)
                lstTVEpisodeFilter.Items.Insert(iIndex - 1, lstTVEpisodeFilter.SelectedItems(0))
                lstTVEpisodeFilter.Items.RemoveAt(iIndex + 1)
                lstTVEpisodeFilter.SelectedIndex = iIndex - 1
                SetApplyButton(True)
                sResult.NeedsReload_TVEpisode = True
                lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVScraperDefFIExtAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVScraperDefFIExtAdd.Click
        If Not txtTVScraperDefFIExt.Text.StartsWith(".") Then txtTVScraperDefFIExt.Text = String.Concat(".", txtTVScraperDefFIExt.Text)
        Using dFileInfo As New dlgFileInfo(New MediaContainers.FileInfo)
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
                    SetApplyButton(True)
                End If
            End If
        End Using
    End Sub

    Private Sub btnTVScraperDefFIExtEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVScraperDefFIExtEdit.Click, lstTVScraperDefFIExt.DoubleClick
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each tMetadata As Settings.MetadataPerType In TVMeta
                If tMetadata.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                    Using dFileInfo As New dlgFileInfo(tMetadata.MetaData)
                        If dFileInfo.ShowDialog = DialogResult.OK Then
                            tMetadata.MetaData = dFileInfo.Result
                            LoadTVMetadata()
                            SetApplyButton(True)
                        End If
                    End Using
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingUp.Click
        Try
            If lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso Not lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.regexp = TVShowMatching.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVSourcesRegexTVShowMatching.SuspendLayout()
                    Dim iIndex As Integer = TVShowMatching.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                    TVShowMatching.Remove(selItem)
                    TVShowMatching.Insert(iIndex - 1, selItem)

                    RenumberTVShowMatching()
                    LoadTVShowMatching()

                    lvTVSourcesRegexTVShowMatching.Items(selIndex - 1).Selected = True
                    lvTVSourcesRegexTVShowMatching.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVSourcesRegexTVShowMatching.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingDown.Click
        Try
            If lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index < (lvTVSourcesRegexTVShowMatching.Items.Count - 1) Then
                Dim selItem As Settings.regexp = TVShowMatching.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVSourcesRegexTVShowMatching.SuspendLayout()
                    Dim iIndex As Integer = TVShowMatching.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                    TVShowMatching.Remove(selItem)
                    TVShowMatching.Insert(iIndex + 1, selItem)

                    RenumberTVShowMatching()
                    LoadTVShowMatching()

                    lvTVSourcesRegexTVShowMatching.Items(selIndex + 1).Selected = True
                    lvTVSourcesRegexTVShowMatching.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVSourcesRegexTVShowMatching.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieGeneralMediaListSortingUp.Click
        Try
            If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not lvMovieGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    MovieGeneralMediaListSorting.Remove(selItem)
                    MovieGeneralMediaListSorting.Insert(iIndex - 1, selItem)

                    RenumberMovieGeneralMediaListSorting()
                    LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(selIndex - 3)
                    End If
                    lvMovieGeneralMediaListSorting.Items(selIndex - 1).Selected = True
                    lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetGeneralMediaListSortingUp.Click
        Try
            If lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not lvMovieSetGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieSetGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieSetGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieSetGeneralMediaListSorting.SelectedIndices(0)
                    MovieSetGeneralMediaListSorting.Remove(selItem)
                    MovieSetGeneralMediaListSorting.Insert(iIndex - 1, selItem)

                    RenumberMovieSetGeneralMediaListSorting()
                    LoadMovieSetGeneralMediaListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvMovieSetGeneralMediaListSorting.TopItem = lvMovieSetGeneralMediaListSorting.Items(selIndex - 3)
                    End If
                    lvMovieSetGeneralMediaListSorting.Items(selIndex - 1).Selected = True
                    lvMovieSetGeneralMediaListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvMovieSetGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralEpisodeListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralEpisodeListSortingUp.Click
        Try
            If lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 AndAlso Not lvTVGeneralEpisodeListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralEpisodeListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralEpisodeListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralEpisodeListSorting.SelectedIndices(0)
                    TVGeneralEpisodeListSorting.Remove(selItem)
                    TVGeneralEpisodeListSorting.Insert(iIndex - 1, selItem)

                    RenumberTVEpisodeGeneralMediaListSorting()
                    LoadTVGeneralEpisodeListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvTVGeneralEpisodeListSorting.TopItem = lvTVGeneralEpisodeListSorting.Items(selIndex - 3)
                    End If
                    lvTVGeneralEpisodeListSorting.Items(selIndex - 1).Selected = True
                    lvTVGeneralEpisodeListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVGeneralEpisodeListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralSeasonListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralSeasonListSortingUp.Click
        Try
            If lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 AndAlso Not lvTVGeneralSeasonListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralSeasonListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralSeasonListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralSeasonListSorting.SelectedIndices(0)
                    TVGeneralSeasonListSorting.Remove(selItem)
                    TVGeneralSeasonListSorting.Insert(iIndex - 1, selItem)

                    RenumberTVSeasonGeneralMediaListSorting()
                    LoadTVGeneralSeasonListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvTVGeneralSeasonListSorting.TopItem = lvTVGeneralSeasonListSorting.Items(selIndex - 3)
                    End If
                    lvTVGeneralSeasonListSorting.Items(selIndex - 1).Selected = True
                    lvTVGeneralSeasonListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVGeneralSeasonListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralShowListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralShowListSortingUp.Click
        Try
            If lvTVGeneralShowListSorting.Items.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems.Count > 0 AndAlso Not lvTVGeneralShowListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralShowListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralShowListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralShowListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralShowListSorting.SelectedIndices(0)
                    TVGeneralShowListSorting.Remove(selItem)
                    TVGeneralShowListSorting.Insert(iIndex - 1, selItem)

                    RenumberTVShowGeneralMediaListSorting()
                    LoadTVGeneralShowListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvTVGeneralShowListSorting.TopItem = lvTVGeneralShowListSorting.Items(selIndex - 3)
                    End If
                    lvTVGeneralShowListSorting.Items(selIndex - 1).Selected = True
                    lvTVGeneralShowListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVGeneralShowListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieGeneralMediaListSortingDown.Click
        Try
            If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems(0).Index < (lvMovieGeneralMediaListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    MovieGeneralMediaListSorting.Remove(selItem)
                    MovieGeneralMediaListSorting.Insert(iIndex + 1, selItem)

                    RenumberMovieGeneralMediaListSorting()
                    LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(selIndex - 2)
                    End If
                    lvMovieGeneralMediaListSorting.Items(selIndex + 1).Selected = True
                    lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetGeneralMediaListSortingDown.Click
        Try
            If lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems(0).Index < (lvMovieSetGeneralMediaListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieSetGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieSetGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieSetGeneralMediaListSorting.SelectedIndices(0)
                    MovieSetGeneralMediaListSorting.Remove(selItem)
                    MovieSetGeneralMediaListSorting.Insert(iIndex + 1, selItem)

                    RenumberMovieSetGeneralMediaListSorting()
                    LoadMovieSetGeneralMediaListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvMovieSetGeneralMediaListSorting.TopItem = lvMovieSetGeneralMediaListSorting.Items(selIndex - 2)
                    End If
                    lvMovieSetGeneralMediaListSorting.Items(selIndex + 1).Selected = True
                    lvMovieSetGeneralMediaListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvMovieSetGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralEpisodeListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralEpisodeListSortingDown.Click
        Try
            If lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems(0).Index < (lvTVGeneralEpisodeListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralEpisodeListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralEpisodeListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralEpisodeListSorting.SelectedIndices(0)
                    TVGeneralEpisodeListSorting.Remove(selItem)
                    TVGeneralEpisodeListSorting.Insert(iIndex + 1, selItem)

                    RenumberTVEpisodeGeneralMediaListSorting()
                    LoadTVGeneralEpisodeListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvTVGeneralEpisodeListSorting.TopItem = lvTVGeneralEpisodeListSorting.Items(selIndex - 2)
                    End If
                    lvTVGeneralEpisodeListSorting.Items(selIndex + 1).Selected = True
                    lvTVGeneralEpisodeListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVGeneralEpisodeListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralSeasonListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralSeasonListSortingDown.Click
        Try
            If lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems(0).Index < (lvTVGeneralSeasonListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralSeasonListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralSeasonListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralSeasonListSorting.SelectedIndices(0)
                    TVGeneralSeasonListSorting.Remove(selItem)
                    TVGeneralSeasonListSorting.Insert(iIndex + 1, selItem)

                    RenumberTVSeasonGeneralMediaListSorting()
                    LoadTVGeneralSeasonListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvTVGeneralSeasonListSorting.TopItem = lvTVGeneralSeasonListSorting.Items(selIndex - 2)
                    End If
                    lvTVGeneralSeasonListSorting.Items(selIndex + 1).Selected = True
                    lvTVGeneralSeasonListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVGeneralSeasonListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralShowListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralShowListSortingDown.Click
        Try
            If lvTVGeneralShowListSorting.Items.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems(0).Index < (lvTVGeneralShowListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralShowListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralShowListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralShowListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralShowListSorting.SelectedIndices(0)
                    TVGeneralShowListSorting.Remove(selItem)
                    TVGeneralShowListSorting.Insert(iIndex + 1, selItem)

                    RenumberTVShowGeneralMediaListSorting()
                    LoadTVGeneralShowListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvTVGeneralShowListSorting.TopItem = lvTVGeneralShowListSorting.Items(selIndex - 2)
                    End If
                    lvTVGeneralShowListSorting.Items(selIndex + 1).Selected = True
                    lvTVGeneralShowListSorting.ResumeLayout()
                End If

                SetApplyButton(True)
                lvTVGeneralShowListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub lvMovieGeneralMediaListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvMovieGeneralMediaListSorting.MouseDoubleClick
        If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvMovieGeneralMediaListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvMovieGeneralMediaListSorting.TopItem.Index
                Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)

                LoadMovieGeneralMediaListSorting()

                lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(topIndex)
                lvMovieGeneralMediaListSorting.Items(selIndex).Selected = True
                lvMovieGeneralMediaListSorting.ResumeLayout()
            End If

            SetApplyButton(True)
            lvMovieGeneralMediaListSorting.Focus()
        End If
    End Sub

    Private Sub lvMovieSetGeneralMediaListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvMovieSetGeneralMediaListSorting.MouseDoubleClick
        If lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvMovieSetGeneralMediaListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvMovieSetGeneralMediaListSorting.TopItem.Index
                Dim selIndex As Integer = lvMovieSetGeneralMediaListSorting.SelectedIndices(0)

                LoadMovieSetGeneralMediaListSorting()

                lvMovieSetGeneralMediaListSorting.TopItem = lvMovieSetGeneralMediaListSorting.Items(topIndex)
                lvMovieSetGeneralMediaListSorting.Items(selIndex).Selected = True
                lvMovieSetGeneralMediaListSorting.ResumeLayout()
            End If

            SetApplyButton(True)
            lvMovieSetGeneralMediaListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralEpisodeListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralEpisodeListSorting.MouseDoubleClick
        If lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVGeneralEpisodeListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvTVGeneralEpisodeListSorting.TopItem.Index
                Dim selIndex As Integer = lvTVGeneralEpisodeListSorting.SelectedIndices(0)

                LoadTVGeneralEpisodeListSorting()

                lvTVGeneralEpisodeListSorting.TopItem = lvTVGeneralEpisodeListSorting.Items(topIndex)
                lvTVGeneralEpisodeListSorting.Items(selIndex).Selected = True
                lvTVGeneralEpisodeListSorting.ResumeLayout()
            End If

            SetApplyButton(True)
            lvTVGeneralEpisodeListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralSeasonListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralSeasonListSorting.MouseDoubleClick
        If lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVGeneralSeasonListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvTVGeneralSeasonListSorting.TopItem.Index
                Dim selIndex As Integer = lvTVGeneralSeasonListSorting.SelectedIndices(0)

                LoadTVGeneralSeasonListSorting()

                lvTVGeneralSeasonListSorting.TopItem = lvTVGeneralSeasonListSorting.Items(topIndex)
                lvTVGeneralSeasonListSorting.Items(selIndex).Selected = True
                lvTVGeneralSeasonListSorting.ResumeLayout()
            End If

            SetApplyButton(True)
            lvTVGeneralSeasonListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralShowListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralShowListSorting.MouseDoubleClick
        If lvTVGeneralShowListSorting.Items.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralShowListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVGeneralShowListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvTVGeneralShowListSorting.TopItem.Index
                Dim selIndex As Integer = lvTVGeneralShowListSorting.SelectedIndices(0)

                LoadTVGeneralShowListSorting()

                lvTVGeneralShowListSorting.TopItem = lvTVGeneralShowListSorting.Items(topIndex)
                lvTVGeneralShowListSorting.Items(selIndex).Selected = True
                lvTVGeneralShowListSorting.ResumeLayout()
            End If

            SetApplyButton(True)
            lvTVGeneralShowListSorting.Focus()
        End If
    End Sub

    Private Sub btnTVShowFilterReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default list of show filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowFilters, True)
            RefreshTVShowFilters()
            SetApplyButton(True)
        End If
    End Sub

    Private Sub btnTVEpisodeFilterReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(841, "Are you sure you want to reset to the default list of episode filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVEpisodeFilters, True)
            RefreshTVEpisodeFilters()
            SetApplyButton(True)
        End If
    End Sub

    Private Sub btnMovieFilterReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(842, "Are you sure you want to reset to the default list of movie filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieFilters, True)
            RefreshMovieFilters()
            SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidVideoExtsReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidVideoExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(843, "Are you sure you want to reset to the default list of valid video extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidExts, True)
            RefreshFileSystemValidExts()
            SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidSubtitlesExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(1283, "Are you sure you want to reset to the default list of valid subtitle extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidSubtitleExts, True)
            RefreshFileSystemValidSubtitlesExts()
            SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidThemeExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(1080, "Are you sure you want to reset to the default list of valid theme extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidThemeExts, True)
            RefreshFileSystemValidThemeExts()
            SetApplyButton(True)
        End If
    End Sub

    Private Sub btnGeneralAVCodecMappingAudioDefaults_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGeneralAVCodecMappingAudioDefaults.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.AudioCodecMapping, True)
        GeneralAudioCodecMapping.Clear()
        GeneralAudioCodecMapping.AddRange(Master.eSettings.GeneralAudioCodecMapping)
        FillGeneralAudioCodecMapping()
        SetApplyButton(True)
    End Sub

    Private Sub btnGeneralAVCodecMappingVideoDefaults_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGeneralAVCodecMappingVideoDefaults.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.VideoCodecMapping, True)
        GeneralVideoCodecMapping.Clear()
        GeneralVideoCodecMapping.AddRange(Master.eSettings.GeneralVideoCodecMapping)
        FillGeneralVideoCodecMapping()
        SetApplyButton(True)
    End Sub

    Private Sub btnGeneralVideoSourceMappingByRegexDefaults_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGeneralVideoSourceMappingByRegexDefaults.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.VideosourceMappingByRegex, True)
        GeneralVideoSourceByRegex.Clear()
        GeneralVideoSourceByRegex.AddRange(Master.eSettings.GeneralVideoSourceByRegex)
        FillGeneralVideoSourceMappingByRegex()
        SetApplyButton(True)
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingReset.Click
        If MessageBox.Show(Master.eLang.GetString(844, "Are you sure you want to reset to the default list of show regex?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowMatching, True)
            TVShowMatching.Clear()
            TVShowMatching.AddRange(Master.eSettings.TVShowMatching)
            LoadTVShowMatching()
            SetApplyButton(True)
        End If
    End Sub

    Private Sub btnTVSourcesRegexMultiPartMatchingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexMultiPartMatchingReset.Click
        txtTVSourcesRegexMultiPartMatching.Text = "^[-_ex]+([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)"
        SetApplyButton(True)
    End Sub

    Private Sub btnMovieGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieGeneralMediaListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieListSorting, True)
        MovieGeneralMediaListSorting.Clear()
        MovieGeneralMediaListSorting.AddRange(Master.eSettings.MovieGeneralMediaListSorting)
        LoadMovieGeneralMediaListSorting()
        SetApplyButton(True)
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetGeneralMediaListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MoviesetListSorting, True)
        MovieSetGeneralMediaListSorting.Clear()
        MovieSetGeneralMediaListSorting.AddRange(Master.eSettings.MovieSetGeneralMediaListSorting)
        LoadMovieSetGeneralMediaListSorting()
        SetApplyButton(True)
    End Sub

    Private Sub btnTVEpisodeGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralEpisodeListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVEpisodeListSorting, True)
        TVGeneralEpisodeListSorting.Clear()
        TVGeneralEpisodeListSorting.AddRange(Master.eSettings.TVGeneralEpisodeListSorting)
        LoadTVGeneralEpisodeListSorting()
        SetApplyButton(True)
    End Sub

    Private Sub btnTVSeasonGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralSeasonListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVSeasonListSorting, True)
        TVGeneralSeasonListSorting.Clear()
        TVGeneralSeasonListSorting.AddRange(Master.eSettings.TVGeneralSeasonListSorting)
        LoadTVGeneralSeasonListSorting()
        SetApplyButton(True)
    End Sub

    Private Sub btnTVGeneralShowListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralShowListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowListSorting, True)
        TVGeneralShowListSorting.Clear()
        TVGeneralShowListSorting.AddRange(Master.eSettings.TVGeneralShowListSorting)
        LoadTVGeneralShowListSorting()
        SetApplyButton(True)
    End Sub

    Private Sub btnFileSystemValidExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidVideoExtsRemove.Click
        RemoveFileSystemValidExts()
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidSubtitlesExtsRemove.Click
        RemoveFileSystemValidSubtitlesExts()
    End Sub

    Private Sub btnFileSystemValidThemeExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidThemeExtsRemove.Click
        RemoveFileSystemValidThemeExts()
    End Sub

    Private Sub btnTVEpisodeFilterRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterRemove.Click
        RemoveTVEpisodeFilter()
    End Sub

    Private Sub btnMovieFilterRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterRemove.Click
        RemoveMovieFilter()
    End Sub

    Private Sub btnFileSystemNoStackExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemNoStackExtsRemove.Click
        RemoveFileSystemNoStackExts()
    End Sub

    Private Sub btnTVShowFilterRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterRemove.Click
        RemoveTVShowFilter()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingRemove.Click
        RemoveTVShowMatching()
    End Sub

    Private Sub btnMovieSortTokenRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSortTokenRemove.Click
        RemoveMovieSortToken()
    End Sub

    Private Sub btnMovieSortTokenReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSortTokenReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieSortTokens, True)
        RefreshMovieSortTokens()
        SetApplyButton(True)
    End Sub

    Private Sub btnMovieSetSortTokenRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetSortTokenRemove.Click
        RemoveMovieSetSortToken()
    End Sub

    Private Sub btnMovieSetSortTokenReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetSortTokenReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MoviesetSortTokens, True)
        RefreshMovieSetSortTokens()
        SetApplyButton(True)
    End Sub

    Private Sub btnTVSortTokenRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSortTokenRemove.Click
        RemoveTVSortToken()
    End Sub

    Private Sub btnTVSortTokenReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSortTokenReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowSortTokens, True)
        RefreshTVSortTokens()
        SetApplyButton(True)
    End Sub

    Private Sub btnTVScraperDefFIExtRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVScraperDefFIExtRemove.Click
        RemoveTVMetaData()
    End Sub

    Private Sub btnRemTVSource_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemTVSource.Click
        RemoveTVSource()
    End Sub

    Private Sub btnTVShowFilterDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterDown.Click
        Try
            If lstTVShowFilter.Items.Count > 0 AndAlso lstTVShowFilter.SelectedItem IsNot Nothing AndAlso lstTVShowFilter.SelectedIndex < (lstTVShowFilter.Items.Count - 1) Then
                Dim iIndex As Integer = lstTVShowFilter.SelectedIndices(0)
                lstTVShowFilter.Items.Insert(iIndex + 2, lstTVShowFilter.SelectedItems(0))
                lstTVShowFilter.Items.RemoveAt(iIndex)
                lstTVShowFilter.SelectedIndex = iIndex + 1
                SetApplyButton(True)
                sResult.NeedsReload_TVShow = True
                lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVShowFilterUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterUp.Click
        Try
            If lstTVShowFilter.Items.Count > 0 AndAlso lstTVShowFilter.SelectedItem IsNot Nothing AndAlso lstTVShowFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = lstTVShowFilter.SelectedIndices(0)
                lstTVShowFilter.Items.Insert(iIndex - 1, lstTVShowFilter.SelectedItems(0))
                lstTVShowFilter.Items.RemoveAt(iIndex + 1)
                lstTVShowFilter.SelectedIndex = iIndex - 1
                SetApplyButton(True)
                sResult.NeedsReload_TVShow = True
                lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub


    Private Sub btnMovieFilterUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterUp.Click
        Try
            If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItem IsNot Nothing AndAlso lstMovieFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = lstMovieFilters.SelectedIndices(0)
                lstMovieFilters.Items.Insert(iIndex - 1, lstMovieFilters.SelectedItems(0))
                lstMovieFilters.Items.RemoveAt(iIndex + 1)
                lstMovieFilters.SelectedIndex = iIndex - 1
                SetApplyButton(True)
                sResult.NeedsReload_Movie = True
                lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cbGeneralLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbGeneralLanguage.SelectedIndexChanged
        Cursor.Current = Cursors.WaitCursor
        Master.eLang.LoadAllLanguage(cbGeneralLanguage.SelectedItem.ToString, True)
        SetUp()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cbMovieTrailerPrefVideoQual_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMovieTrailerPrefVideoQual.SelectedIndexChanged
        If CType(cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value = Enums.TrailerVideoQuality.Any Then
            cbMovieTrailerMinVideoQual.Enabled = False
        Else
            cbMovieTrailerMinVideoQual.Enabled = True
        End If
        SetApplyButton(True)
    End Sub

    Private Sub CheckHideSettings()
        If chkGeneralDisplayBanner.Checked OrElse chkGeneralDisplayCharacterArt.Checked OrElse chkGeneralDisplayClearArt.Checked OrElse chkGeneralDisplayClearLogo.Checked OrElse
              chkGeneralDisplayDiscArt.Checked OrElse chkGeneralDisplayFanart.Checked OrElse chkGeneralDisplayFanartSmall.Checked OrElse chkGeneralDisplayLandscape.Checked OrElse chkGeneralDisplayPoster.Checked Then
            chkGeneralImagesGlassOverlay.Enabled = True
            chkGeneralDisplayImgDims.Enabled = True
            chkGeneralDisplayImgNames.Enabled = True
        Else
            chkGeneralImagesGlassOverlay.Enabled = False
            chkGeneralDisplayImgDims.Enabled = False
            chkGeneralDisplayImgNames.Enabled = False
        End If
    End Sub

    Private Sub chkMovieClickScrape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieClickScrape.CheckedChanged
        chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
        SetApplyButton(True)
    End Sub

    Private Sub chkTVGeneralClickScrape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVGeneralClickScrape.CheckedChanged
        chkTVGeneralClickScrapeAsk.Enabled = chkTVGeneralClickScrape.Checked
        SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowStudio.CheckedChanged
        SetApplyButton(True)
        txtTVScraperShowStudioLimit.Enabled = chkTVScraperShowStudio.Checked
        If Not chkTVScraperShowStudio.Checked Then txtTVScraperShowStudioLimit.Text = "0"
    End Sub
    Private Sub TVScraperActors_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperEpisodeActors.CheckedChanged, chkTVScraperEpisodeGuestStars.CheckedChanged, chkTVScraperShowActors.CheckedChanged
        SetApplyButton(True)

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
    End Sub

    Private Sub chkTVScraperShowCert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowCert.CheckedChanged
        SetApplyButton(True)

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

    Private Sub chkTVScraperSeasonTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperSeasonTitle.CheckedChanged
        SetApplyButton(True)

        btnTVScraperSeasonTitleBlacklist.Enabled = chkTVScraperSeasonTitle.Checked
    End Sub

    Private Sub chkTVScraperShowCertForMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowCertForMPAA.CheckedChanged
        SetApplyButton(True)

        If Not chkTVScraperShowCertForMPAA.Checked Then
            chkTVScraperShowCertForMPAAFallback.Enabled = False
            chkTVScraperShowCertForMPAAFallback.Checked = False
        Else
            chkTVScraperShowCertForMPAAFallback.Enabled = True
        End If
    End Sub
    Private Sub chkMovieLevTolerance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieLevTolerance.CheckedChanged
        SetApplyButton(True)

        txtMovieLevTolerance.Enabled = chkMovieLevTolerance.Checked
        If Not chkMovieLevTolerance.Checked Then txtMovieLevTolerance.Text = String.Empty
    End Sub

    Private Sub chkMovieDisplayYear_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieDisplayYear.CheckedChanged
        sResult.NeedsReload_Movie = True
        SetApplyButton(True)
    End Sub

    Private Sub chkTVDisplayStatus_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVDisplayStatus.CheckedChanged
        sResult.NeedsReload_TVShow = True
        SetApplyButton(True)
    End Sub

    Private Sub chkProxyCredsEnable_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkProxyCredsEnable.CheckedChanged
        SetApplyButton(True)
        txtProxyUsername.Enabled = chkProxyCredsEnable.Checked
        txtProxyPassword.Enabled = chkProxyCredsEnable.Checked
        txtProxyDomain.Enabled = chkProxyCredsEnable.Checked

        If Not chkProxyCredsEnable.Checked Then
            txtProxyUsername.Text = String.Empty
            txtProxyPassword.Text = String.Empty
            txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkProxyEnable_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkProxyEnable.CheckedChanged
        SetApplyButton(True)
        txtProxyURI.Enabled = chkProxyEnable.Checked
        txtProxyPort.Enabled = chkProxyEnable.Checked
        gbProxyCredsOpts.Enabled = chkProxyEnable.Checked

        If Not chkProxyEnable.Checked Then
            txtProxyURI.Text = String.Empty
            txtProxyPort.Text = String.Empty
            chkProxyCredsEnable.Checked = False
            txtProxyUsername.Text = String.Empty
            txtProxyPassword.Text = String.Empty
            txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodeProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodeProperCase.CheckedChanged
        SetApplyButton(True)
        sResult.NeedsReload_TVEpisode = True
    End Sub


    Private Sub chkTVScraperShowGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowGenre.CheckedChanged
        SetApplyButton(True)
        txtTVScraperShowGenreLimit.Enabled = chkTVScraperShowGenre.Checked
        If Not chkTVScraperShowGenre.Checked Then txtTVScraperShowGenreLimit.Text = "0"
    End Sub

    Private Sub chkGeneralDisplayBanner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayBanner.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayCharacterArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayCharacterArt.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayClearArt.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayClearLogo.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayDiscArt.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayFanart.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayLandscape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayLandscape.CheckedChanged, chkGeneralDisplayKeyArt.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayPoster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayPoster.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanartSmall_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayFanartSmall.CheckedChanged
        SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkTVEpisodeNoFilter_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodeNoFilter.CheckedChanged
        SetApplyButton(True)

        chkTVEpisodeProperCase.Enabled = Not chkTVEpisodeNoFilter.Checked
        lstTVEpisodeFilter.Enabled = Not chkTVEpisodeNoFilter.Checked
        txtTVEpisodeFilter.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterAdd.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterUp.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterDown.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterRemove.Enabled = Not chkTVEpisodeNoFilter.Checked
    End Sub

    Private Sub chkTVScraperShowOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVScraperShowOriginalTitle.CheckedChanged
        SetApplyButton(True)

        chkTVScraperShowOriginalTitleAsTitle.Enabled = chkTVScraperShowOriginalTitle.Checked
        If Not chkTVScraperShowOriginalTitle.Checked Then chkTVScraperShowOriginalTitleAsTitle.Checked = False
    End Sub

    Private Sub chkMovieProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieProperCase.CheckedChanged
        SetApplyButton(True)
        sResult.NeedsReload_Movie = True
    End Sub

    Private Sub chkTVAllSeasonsBannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVAllSeasonsBannerResize.CheckedChanged
        SetApplyButton(True)

        txtTVAllSeasonsBannerWidth.Enabled = chkTVAllSeasonsBannerResize.Checked
        txtTVAllSeasonsBannerHeight.Enabled = chkTVAllSeasonsBannerResize.Checked

        If Not chkTVAllSeasonsBannerResize.Checked Then
            txtTVAllSeasonsBannerWidth.Text = String.Empty
            txtTVAllSeasonsBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVAllSeasonsFanartResize.CheckedChanged
        SetApplyButton(True)

        txtTVAllSeasonsFanartWidth.Enabled = chkTVAllSeasonsFanartResize.Checked
        txtTVAllSeasonsFanartHeight.Enabled = chkTVAllSeasonsFanartResize.Checked

        If Not chkTVAllSeasonsFanartResize.Checked Then
            txtTVAllSeasonsFanartWidth.Text = String.Empty
            txtTVAllSeasonsFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVAllSeasonsPosterResize.CheckedChanged
        SetApplyButton(True)

        txtTVAllSeasonsPosterWidth.Enabled = chkTVAllSeasonsPosterResize.Checked
        txtTVAllSeasonsPosterHeight.Enabled = chkTVAllSeasonsPosterResize.Checked

        If Not chkTVAllSeasonsPosterResize.Checked Then
            txtTVAllSeasonsPosterWidth.Text = String.Empty
            txtTVAllSeasonsPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodeFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodeFanartResize.CheckedChanged
        SetApplyButton(True)

        txtTVEpisodeFanartWidth.Enabled = chkTVEpisodeFanartResize.Checked
        txtTVEpisodeFanartHeight.Enabled = chkTVEpisodeFanartResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodeFanartWidth.Text = String.Empty
            txtTVEpisodeFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodePosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodePosterResize.CheckedChanged
        SetApplyButton(True)

        txtTVEpisodePosterWidth.Enabled = chkTVEpisodePosterResize.Checked
        txtTVEpisodePosterHeight.Enabled = chkTVEpisodePosterResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodePosterWidth.Text = String.Empty
            txtTVEpisodePosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodePosterVideoExtraction_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVEpisodePosterVideoExtraction.CheckedChanged
        SetApplyButton(True)
        chkTVEpisodePosterVideoExtractionPref.Enabled = chkTVEpisodePosterVideoExtraction.Checked
        If Not chkTVEpisodePosterVideoExtraction.Checked Then
            chkTVEpisodePosterVideoExtractionPref.Checked = False
        End If
    End Sub

    Private Sub chkTVShowExtrafanartsResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowExtrafanartsResize.CheckedChanged
        SetApplyButton(True)

        txtTVShowExtrafanartsWidth.Enabled = chkTVShowExtrafanartsResize.Checked
        txtTVShowExtrafanartsHeight.Enabled = chkTVShowExtrafanartsResize.Checked

        If Not chkTVShowExtrafanartsResize.Checked Then
            txtTVShowExtrafanartsWidth.Text = String.Empty
            txtTVShowExtrafanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetBannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetBannerResize.CheckedChanged
        SetApplyButton(True)

        txtMovieSetBannerWidth.Enabled = chkMovieSetBannerResize.Checked
        txtMovieSetBannerHeight.Enabled = chkMovieSetBannerResize.Checked

        If Not chkMovieSetBannerResize.Checked Then
            txtMovieSetBannerWidth.Text = String.Empty
            txtMovieSetBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetFanartResize.CheckedChanged
        SetApplyButton(True)

        txtMovieSetFanartWidth.Enabled = chkMovieSetFanartResize.Checked
        txtMovieSetFanartHeight.Enabled = chkMovieSetFanartResize.Checked

        If Not chkMovieSetFanartResize.Checked Then
            txtMovieSetFanartWidth.Text = String.Empty
            txtMovieSetFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetKeyArtResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetKeyArtResize.CheckedChanged
        SetApplyButton(True)

        txtMovieSetKeyArtWidth.Enabled = chkMovieSetKeyArtResize.Checked
        txtMovieSetKeyArtHeight.Enabled = chkMovieSetKeyArtResize.Checked

        If Not chkMovieSetKeyArtResize.Checked Then
            txtMovieSetKeyArtWidth.Text = String.Empty
            txtMovieSetKeyArtHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetPosterResize.CheckedChanged
        SetApplyButton(True)

        txtMovieSetPosterWidth.Enabled = chkMovieSetPosterResize.Checked
        txtMovieSetPosterHeight.Enabled = chkMovieSetPosterResize.Checked

        If Not chkMovieSetPosterResize.Checked Then
            txtMovieSetPosterWidth.Text = String.Empty
            txtMovieSetPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowbannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowBannerResize.CheckedChanged
        SetApplyButton(True)

        txtTVShowBannerWidth.Enabled = chkTVShowBannerResize.Checked
        txtTVShowBannerHeight.Enabled = chkTVShowBannerResize.Checked

        If Not chkTVShowBannerResize.Checked Then
            txtTVShowBannerWidth.Text = String.Empty
            txtTVShowBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowFanartResize.CheckedChanged
        SetApplyButton(True)

        txtTVShowFanartWidth.Enabled = chkTVShowFanartResize.Checked
        txtTVShowFanartHeight.Enabled = chkTVShowFanartResize.Checked

        If Not chkTVShowFanartResize.Checked Then
            txtTVShowFanartWidth.Text = String.Empty
            txtTVShowFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowKeyArtResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowKeyArtResize.CheckedChanged
        SetApplyButton(True)

        txtTVShowKeyArtWidth.Enabled = chkTVShowKeyArtResize.Checked
        txtTVShowKeyArtHeight.Enabled = chkTVShowKeyArtResize.Checked

        If Not chkTVShowKeyArtResize.Checked Then
            txtTVShowKeyArtWidth.Text = String.Empty
            txtTVShowKeyArtHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowPosterResize.CheckedChanged
        SetApplyButton(True)

        txtTVShowPosterWidth.Enabled = chkTVShowPosterResize.Checked
        txtTVShowPosterHeight.Enabled = chkTVShowPosterResize.Checked

        If Not chkTVShowPosterResize.Checked Then
            txtTVShowPosterWidth.Text = String.Empty
            txtTVShowPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVScraperShowRuntime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowRuntime.CheckedChanged
        chkTVScraperUseSRuntimeForEp.Enabled = chkTVScraperShowRuntime.Checked
        If Not chkTVScraperShowRuntime.Checked Then
            chkTVScraperUseSRuntimeForEp.Checked = False
        End If
        SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonbannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVSeasonBannerResize.CheckedChanged
        SetApplyButton(True)

        txtTVSeasonBannerWidth.Enabled = chkTVSeasonBannerResize.Checked
        txtTVSeasonBannerHeight.Enabled = chkTVSeasonBannerResize.Checked

        If Not chkTVSeasonBannerResize.Checked Then
            txtTVSeasonBannerWidth.Text = String.Empty
            txtTVSeasonBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVSeasonFanartResize.CheckedChanged
        SetApplyButton(True)

        txtTVSeasonFanartWidth.Enabled = chkTVSeasonFanartResize.Checked
        txtTVSeasonFanartHeight.Enabled = chkTVSeasonFanartResize.Checked

        If Not chkTVSeasonFanartResize.Checked Then
            txtTVSeasonFanartWidth.Text = String.Empty
            txtTVSeasonFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVSeasonPosterResize.CheckedChanged
        SetApplyButton(True)

        txtTVSeasonPosterWidth.Enabled = chkTVSeasonPosterResize.Checked
        txtTVSeasonPosterHeight.Enabled = chkTVSeasonPosterResize.Checked

        If Not chkTVSeasonPosterResize.Checked Then
            txtTVSeasonPosterWidth.Text = String.Empty
            txtTVSeasonPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowProperCase.CheckedChanged
        SetApplyButton(True)
        sResult.NeedsReload_TVShow = True
    End Sub

    Private Sub chkTVScraperMetaDataScan_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperMetaDataScan.CheckedChanged
        SetApplyButton(True)

        cbTVLanguageOverlay.Enabled = chkTVScraperMetaDataScan.Checked

        If Not chkTVScraperMetaDataScan.Checked Then
            cbTVLanguageOverlay.SelectedIndex = 0
        End If
    End Sub

    Private Sub chkMovieSetUseExtended_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetUseExtended.CheckedChanged
        SetApplyButton(True)

        btnMovieSetPathExtendedBrowse.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetBannerExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetClearArtExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetClearLogoExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetDiscArtExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetFanartExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetKeyArtExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetLandscapeExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetPosterExtended.Enabled = chkMovieSetUseExtended.Checked
        txtMovieSetPathExtended.Enabled = chkMovieSetUseExtended.Checked

        If Not chkMovieSetUseExtended.Checked Then
            chkMovieSetBannerExtended.Checked = False
            chkMovieSetClearArtExtended.Checked = False
            chkMovieSetClearLogoExtended.Checked = False
            chkMovieSetDiscArtExtended.Checked = False
            chkMovieSetFanartExtended.Checked = False
            chkMovieSetKeyArtExtended.Checked = False
            chkMovieSetLandscapeExtended.Checked = False
            chkMovieSetPosterExtended.Checked = False
        Else
            chkMovieSetBannerExtended.Checked = True
            chkMovieSetClearArtExtended.Checked = True
            chkMovieSetClearLogoExtended.Checked = True
            chkMovieSetDiscArtExtended.Checked = True
            chkMovieSetFanartExtended.Checked = True
            chkMovieSetKeyArtExtended.Checked = True
            chkMovieSetLandscapeExtended.Checked = True
            chkMovieSetPosterExtended.Checked = True
        End If
    End Sub

    Private Sub chkMovieSetUseMSAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetUseMSAA.CheckedChanged
        SetApplyButton(True)

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

    Private Sub chkTVScraperUseMDDuration_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperUseMDDuration.CheckedChanged
        txtTVScraperDurationRuntimeFormat.Enabled = chkTVScraperUseMDDuration.Checked
        SetApplyButton(True)
    End Sub

    Private Sub chkTVShowThemeTvTunesCustom_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesCustom.CheckedChanged
        SetApplyButton(True)

        txtTVShowThemeTvTunesCustomPath.Enabled = chkTVShowThemeTvTunesCustom.Checked
        btnTVShowThemeTvTunesCustomPathBrowse.Enabled = chkTVShowThemeTvTunesCustom.Checked

        If chkTVShowThemeTvTunesCustom.Checked Then
            chkTVShowThemeTvTunesShowPath.Enabled = False
            chkTVShowThemeTvTunesShowPath.Checked = False
            chkTVShowThemeTvTunesSub.Enabled = False
            chkTVShowThemeTvTunesSub.Checked = False
        End If

        If Not chkTVShowThemeTvTunesCustom.Checked AndAlso chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesShowPath.Enabled = True
            chkTVShowThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkTVShowThemeTvTunesEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesEnabled.CheckedChanged
        SetApplyButton(True)

        chkTVShowThemeTvTunesCustom.Enabled = chkTVShowThemeTvTunesEnabled.Checked
        chkTVShowThemeTvTunesShowPath.Enabled = chkTVShowThemeTvTunesEnabled.Checked
        chkTVShowThemeTvTunesSub.Enabled = chkTVShowThemeTvTunesEnabled.Checked

        If Not chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesCustom.Checked = False
            chkTVShowThemeTvTunesShowPath.Checked = False
            chkTVShowThemeTvTunesSub.Checked = False
        Else
            chkTVShowThemeTvTunesShowPath.Checked = True
        End If
    End Sub

    Private Sub chkTVShowThemeTvTunesTVShowPath_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesShowPath.CheckedChanged
        SetApplyButton(True)

        If chkTVShowThemeTvTunesShowPath.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = False
            chkTVShowThemeTvTunesCustom.Checked = False
            chkTVShowThemeTvTunesSub.Enabled = False
            chkTVShowThemeTvTunesSub.Checked = False
        End If

        If Not chkTVShowThemeTvTunesShowPath.Checked AndAlso chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = True
            chkTVShowThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkTVShowThemeTvTunesSub_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesSub.CheckedChanged
        SetApplyButton(True)

        txtTVShowThemeTvTunesSubDir.Enabled = chkTVShowThemeTvTunesSub.Checked

        If chkTVShowThemeTvTunesSub.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = False
            chkTVShowThemeTvTunesCustom.Checked = False
            chkTVShowThemeTvTunesShowPath.Enabled = False
            chkTVShowThemeTvTunesShowPath.Checked = False
        End If

        If Not chkTVShowThemeTvTunesSub.Checked AndAlso chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = True
            chkTVShowThemeTvTunesShowPath.Enabled = True
        End If
    End Sub

    Private Sub ClearTVShowMatching()
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(115, "Add Regex")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        txtTVSourcesRegexTVShowMatchingRegex.Text = String.Empty
        txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = String.Empty
        chkTVSourcesRegexTVShowMatchingByDate.Checked = False
    End Sub

    Private Sub dlgSettings_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub EditTVShowMatching(ByVal lItem As ListViewItem)
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(124, "Update Regex")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = lItem.Text

        txtTVSourcesRegexTVShowMatchingRegex.Text = lItem.SubItems(1).Text.ToString
        txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = If(Not lItem.SubItems(2).Text = "-2", lItem.SubItems(2).Text, String.Empty)

        Select Case lItem.SubItems(3).Text
            Case "Yes"
                chkTVSourcesRegexTVShowMatchingByDate.Checked = True
            Case "No"
                chkTVSourcesRegexTVShowMatchingByDate.Checked = False
        End Select
    End Sub

    Private Sub FillMovieSetScraperTitleRenamer()
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
            Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {sett.Name.Substring(21), sett.Value})
            If Not sett.DefaultValue = String.Empty Then
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).ReadOnly = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).Style.SelectionForeColor = Color.Red
            Else
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
            End If
        Next
        dgvMovieSetScraperTitleRenamer.ClearSelection()
    End Sub

    Private Sub SaveMovieSetScraperTitleRenamer()
        Dim deleteitem As New List(Of String)
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
            deleteitem.Add(sett.Name)
        Next

        Using settings = New AdvancedSettings()
            For Each s As String In deleteitem
                settings.CleanSetting(s, "*EmberAPP")
            Next
            For Each r As DataGridViewRow In dgvMovieSetScraperTitleRenamer.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString) Then
                    settings.SetSetting(String.Concat("MovieSetTitleRenamer:", r.Cells(0).Value.ToString), r.Cells(1).Value.ToString, "*EmberAPP")
                End If
            Next
        End Using
    End Sub

    Private Sub FillGeneralAudioCodecMapping()
        dgvGeneralAVCodecMappingAudio.Rows.Clear()
        For Each sett In GeneralAudioCodecMapping
            Dim i As Integer = dgvGeneralAVCodecMappingAudio.Rows.Add(New Object() {sett.Codec, sett.Mapping, sett.AdditionalFeatures})
        Next
        dgvGeneralAVCodecMappingAudio.ClearSelection()
    End Sub

    Private Sub FillGeneralVideoCodecMapping()
        dgvGeneralAVCodecMappingVideo.Rows.Clear()
        For Each sett In GeneralVideoCodecMapping
            Dim i As Integer = dgvGeneralAVCodecMappingVideo.Rows.Add(New Object() {sett.Codec, sett.Mapping})
        Next
        dgvGeneralAVCodecMappingVideo.ClearSelection()
    End Sub

    Private Sub FillGeneralVideoSourceMappingByExtension()
        dgvGeneralVideoSourceMappingByExtension.Rows.Clear()
        For Each sett In GeneralVideoSourceByExtension
            Dim i As Integer = dgvGeneralVideoSourceMappingByExtension.Rows.Add(New Object() {sett.Extension, sett.VideoSource})
        Next
        dgvGeneralVideoSourceMappingByExtension.ClearSelection()
    End Sub

    Private Sub FillGeneralVideoSourceMappingByRegex()
        dgvGeneralVideoSourceMappingByRegex.Rows.Clear()
        For Each sett In GeneralVideoSourceByRegex
            Dim i As Integer = dgvGeneralVideoSourceMappingByRegex.Rows.Add(New Object() {sett.Regexp, sett.Videosource})
        Next
        dgvGeneralVideoSourceMappingByRegex.ClearSelection()
    End Sub

    Private Sub SaveGeneralAudioCodecMapping()
        Master.eSettings.GeneralAudioCodecMapping.Clear()
        For Each r As DataGridViewRow In dgvGeneralAVCodecMappingAudio.Rows
            If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
                Master.eSettings.GeneralAudioCodecMapping.Add(New Settings.CodecMapping With {
                                                              .Codec = r.Cells(0).Value.ToString,
                                                              .Mapping = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty),
                                                              .AdditionalFeatures = If(r.Cells(2).Value IsNot Nothing, r.Cells(2).Value.ToString, String.Empty)
                                                              })
            End If
        Next
    End Sub

    Private Sub SaveGeneralVideoCodecMapping()
        Master.eSettings.GeneralVideoCodecMapping.Clear()
        For Each r As DataGridViewRow In dgvGeneralAVCodecMappingVideo.Rows
            If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
                Master.eSettings.GeneralVideoCodecMapping.Add(New Settings.CodecMapping With {
                                                              .Codec = r.Cells(0).Value.ToString,
                                                              .Mapping = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty)
                                                              })
            End If
        Next
    End Sub

    Private Sub SaveGeneralVideoSourceByExtension()
        Master.eSettings.GeneralVideoSourceByExtension.Clear()
        For Each r As DataGridViewRow In dgvGeneralVideoSourceMappingByExtension.Rows
            If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
                Master.eSettings.GeneralVideoSourceByExtension.Add(New Settings.VideoSourceByExtension With {
                                                                   .Extension = r.Cells(0).Value.ToString,
                                                                   .VideoSource = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty)
                                                                   })
            End If
        Next
    End Sub

    Private Sub SaveGeneralVideoSourceByRegex()
        Master.eSettings.GeneralVideoSourceByRegex.Clear()
        For Each r As DataGridViewRow In dgvGeneralVideoSourceMappingByRegex.Rows
            If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
                Master.eSettings.GeneralVideoSourceByRegex.Add(New Settings.VideoSourceByRegex With {
                                                               .Regexp = r.Cells(0).Value.ToString,
                                                               .Videosource = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty)
                                                               })
            End If
        Next
    End Sub

    Private Sub FillList(ByVal PanelType As Enums.SettingsPanelType)
        Dim pNode As New TreeNode

        tvSettingsList.Nodes.Clear()
        RemoveCurrPanel()

        For Each MainPanel As Containers.SettingsPanel In _SettingsPanels.Where(Function(m) m.Type = PanelType).OrderBy(Function(s) s.Order)
            pNode = New TreeNode(MainPanel.Title, MainPanel.ImageIndex, MainPanel.ImageIndex) With {.Name = MainPanel.SettingsPanelID}
            For Each SubPanel As Containers.SettingsPanel In _SettingsPanels.Where(Function(s) s.Type = MainPanel.Contains).OrderBy(Function(s) s.Order)
                pNode.Nodes.Add(New TreeNode(SubPanel.Title, SubPanel.ImageIndex, SubPanel.ImageIndex) With {.Name = SubPanel.SettingsPanelID})
            Next
            tvSettingsList.Nodes.Add(pNode)
        Next

        If tvSettingsList.Nodes.Count > 0 Then
            tvSettingsList.ExpandAll()
            tvSettingsList.SelectedNode = tvSettingsList.Nodes(0)
        Else
            pbSettingsCurrent.Image = Nothing
            lblSettingsCurrent.Text = String.Empty
        End If
    End Sub

    Private Sub FillSettings()
        With Master.eSettings
            btnMovieGeneralCustomMarker1.BackColor = Color.FromArgb(.MovieGeneralCustomMarker1Color)
            btnMovieGeneralCustomMarker2.BackColor = Color.FromArgb(.MovieGeneralCustomMarker2Color)
            btnMovieGeneralCustomMarker3.BackColor = Color.FromArgb(.MovieGeneralCustomMarker3Color)
            btnMovieGeneralCustomMarker4.BackColor = Color.FromArgb(.MovieGeneralCustomMarker4Color)
            cbGeneralDateTime.SelectedValue = .GeneralDateTime
            RemoveHandler cbGeneralLanguage.SelectedIndexChanged, AddressOf cbGeneralLanguage_SelectedIndexChanged
            cbGeneralLanguage.SelectedItem = .GeneralLanguage
            AddHandler cbGeneralLanguage.SelectedIndexChanged, AddressOf cbGeneralLanguage_SelectedIndexChanged
            cbGeneralTheme.SelectedItem = .GeneralTheme
            cbGeneralVirtualDriveLetter.SelectedItem = .GeneralVirtualDriveLetter
            cbMovieGeneralCustomScrapeButtonModifierType.SelectedValue = .MovieGeneralCustomScrapeButtonModifierType
            cbMovieGeneralCustomScrapeButtonScrapeType.SelectedValue = .MovieGeneralCustomScrapeButtonScrapeType
            cbMovieLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.MovieGeneralFlagLang), Master.eLang.Disabled, .MovieGeneralFlagLang)
            cbMovieSetBannerPrefSize.SelectedValue = .MovieSetBannerPrefSize
            cbMovieSetClearArtPrefSize.SelectedValue = .MovieSetClearArtPrefSize
            cbMovieSetClearLogoPrefSize.SelectedValue = .MovieSetClearLogoPrefSize
            cbMovieSetDiscArtPrefSize.SelectedValue = .MovieSetDiscArtPrefSize
            cbMovieSetFanartPrefSize.SelectedValue = .MovieSetFanartPrefSize
            cbMovieSetGeneralCustomScrapeButtonModifierType.SelectedValue = .MovieSetGeneralCustomScrapeButtonModifierType
            cbMovieSetGeneralCustomScrapeButtonScrapeType.SelectedValue = .MovieSetGeneralCustomScrapeButtonScrapeType
            cbMovieSetKeyArtPrefSize.SelectedValue = .MovieSetKeyArtPrefSize
            cbMovieSetLandscapePrefSize.SelectedValue = .MovieSetLandscapePrefSize
            cbMovieSetPosterPrefSize.SelectedValue = .MovieSetPosterPrefSize
            cbMovieTrailerMinVideoQual.SelectedValue = .MovieTrailerMinVideoQual
            cbMovieTrailerPrefVideoQual.SelectedValue = .MovieTrailerPrefVideoQual
            cbTVAllSeasonsBannerPrefSize.SelectedValue = .TVAllSeasonsBannerPrefSize
            cbTVAllSeasonsBannerPrefType.SelectedValue = .TVAllSeasonsBannerPrefType
            cbTVAllSeasonsFanartPrefSize.SelectedValue = .TVAllSeasonsFanartPrefSize
            cbTVAllSeasonsLandscapePrefSize.SelectedValue = .TVAllSeasonsLandscapePrefSize
            cbTVAllSeasonsPosterPrefSize.SelectedValue = .TVAllSeasonsPosterPrefSize
            cbTVEpisodeFanartPrefSize.SelectedValue = .TVEpisodeFanartPrefSize
            cbTVEpisodePosterPrefSize.SelectedValue = .TVEpisodePosterPrefSize
            cbTVGeneralCustomScrapeButtonModifierType.SelectedValue = .TVGeneralCustomScrapeButtonModifierType
            cbTVGeneralCustomScrapeButtonScrapeType.SelectedValue = .TVGeneralCustomScrapeButtonScrapeType
            cbTVLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.TVGeneralFlagLang), Master.eLang.Disabled, .TVGeneralFlagLang)
            cbTVScraperOptionsOrdering.SelectedValue = .TVScraperOptionsOrdering
            cbTVSeasonBannerPrefSize.SelectedValue = .TVSeasonBannerPrefSize
            cbTVSeasonBannerPrefType.SelectedValue = .TVSeasonBannerPrefType
            cbTVSeasonFanartPrefSize.SelectedValue = .TVSeasonFanartPrefSize
            cbTVSeasonLandscapePrefSize.SelectedValue = .TVSeasonLandscapePrefSize
            cbTVSeasonPosterPrefSize.SelectedValue = .TVSeasonPosterPrefSize
            cbTVShowBannerPrefSize.SelectedValue = .TVShowBannerPrefSize
            cbTVShowBannerPrefType.SelectedValue = .TVShowBannerPrefType
            cbTVShowCharacterArtPrefSize.SelectedValue = .TVShowCharacterArtPrefSize
            cbTVShowClearArtPrefSize.SelectedValue = .TVShowClearArtPrefSize
            cbTVShowClearLogoPrefSize.SelectedValue = .TVShowClearLogoPrefSize
            cbTVShowExtrafanartsPrefSize.SelectedValue = .TVShowExtrafanartsPrefSize
            cbTVShowFanartPrefSize.SelectedValue = .TVShowFanartPrefSize
            cbTVShowKeyArtPrefSize.SelectedValue = .TVShowKeyArtPrefSize
            cbTVShowLandscapePrefSize.SelectedValue = .TVShowLandscapePrefSize
            cbTVShowPosterPrefSize.SelectedValue = .TVShowPosterPrefSize
            chkGeneralCheckUpdates.Checked = .GeneralCheckUpdates
            chkGeneralDateAddedIgnoreNFO.Checked = .GeneralDateAddedIgnoreNFO
            chkGeneralDigitGrpSymbolVotes.Checked = .GeneralDigitGrpSymbolVotes
            chkGeneralImageFilter.Checked = .GeneralImageFilter
            chkGeneralImageFilterAutoscraper.Checked = .GeneralImageFilterAutoscraper
            txtGeneralImageFilterFanartMatchRate.Enabled = .GeneralImageFilterFanart
            chkGeneralImageFilterFanart.Checked = .GeneralImageFilterFanart
            chkGeneralImageFilterImagedialog.Checked = .GeneralImageFilterImagedialog
            chkGeneralImageFilterPoster.Checked = .GeneralImageFilterPoster
            txtGeneralImageFilterPosterMatchRate.Enabled = .GeneralImageFilterPoster
            chkGeneralDoubleClickScrape.Checked = .GeneralDoubleClickScrape
            chkGeneralDisplayBanner.Checked = .GeneralDisplayBanner
            chkGeneralDisplayCharacterArt.Checked = .GeneralDisplayCharacterArt
            chkGeneralDisplayClearArt.Checked = .GeneralDisplayClearArt
            chkGeneralDisplayClearLogo.Checked = .GeneralDisplayClearLogo
            chkGeneralDisplayDiscArt.Checked = .GeneralDisplayDiscArt
            chkGeneralDisplayFanart.Checked = .GeneralDisplayFanart
            chkGeneralDisplayFanartSmall.Checked = .GeneralDisplayFanartSmall
            chkGeneralDisplayKeyArt.Checked = .GeneralDisplayKeyArt
            chkGeneralDisplayLandscape.Checked = .GeneralDisplayLandscape
            chkGeneralDisplayPoster.Checked = .GeneralDisplayPoster
            chkGeneralImagesGlassOverlay.Checked = .GeneralImagesGlassOverlay
            chkGeneralOverwriteNfo.Checked = .GeneralOverwriteNfo
            chkGeneralDisplayGenresText.Checked = .GeneralShowGenresText
            chkGeneralDisplayLangFlags.Checked = .GeneralShowLangFlags
            chkGeneralDisplayImgDims.Checked = .GeneralShowImgDims
            chkGeneralDisplayImgNames.Checked = .GeneralShowImgNames
            chkGeneralSourceFromFolder.Checked = .GeneralSourceFromFolder
            chkGeneralVideoSourceMappingByExtensionEnabled.Checked = .GeneralVideoSourceByExtensionEnabled
            chkGeneralVideoSourceMappingByRegexEnabled.Checked = .GeneralVideoSourceByRegexEnabled
            chkMovieClickScrape.Checked = .MovieClickScrape
            chkMovieClickScrapeAsk.Checked = .MovieClickScrapeAsk
            chkMovieDisplayYear.Checked = .MovieDisplayYear
            chkMovieGeneralMarkNew.Checked = .MovieGeneralMarkNew
            chkMovieProperCase.Checked = .MovieProperCase
            chkMovieSetBannerKeepExisting.Checked = .MovieSetBannerKeepExisting
            chkMovieSetBannerPrefSizeOnly.Checked = .MovieSetBannerPrefSizeOnly
            chkMovieSetBannerResize.Checked = .MovieSetBannerResize
            If .MovieSetBannerResize Then
                txtMovieSetBannerHeight.Text = .MovieSetBannerHeight.ToString
                txtMovieSetBannerWidth.Text = .MovieSetBannerWidth.ToString
            End If
            chkMovieSetCleanDB.Checked = .MovieSetCleanDB
            chkMovieSetCleanFiles.Checked = .MovieSetCleanFiles
            chkMovieSetClearArtKeepExisting.Checked = .MovieSetClearArtKeepExisting
            chkMovieSetClearArtPrefSizeOnly.Checked = .MovieSetClearArtPrefSizeOnly
            chkMovieSetClearLogoKeepExisting.Checked = .MovieSetClearLogoKeepExisting
            chkMovieSetClearLogoPrefSizeOnly.Checked = .MovieSetClearLogoPrefSizeOnly
            chkMovieSetClickScrape.Checked = .MovieSetClickScrape
            chkMovieSetClickScrapeAsk.Checked = .MovieSetClickScrapeAsk
            chkMovieSetDiscArtKeepExisting.Checked = .MovieSetDiscArtKeepExisting
            chkMovieSetDiscArtPrefSizeOnly.Checked = .MovieSetDiscArtPrefSizeOnly
            chkMovieSetFanartKeepExisting.Checked = .MovieSetFanartKeepExisting
            chkMovieSetFanartPrefSizeOnly.Checked = .MovieSetFanartPrefSizeOnly
            chkMovieSetFanartResize.Checked = .MovieSetFanartResize
            If .MovieSetFanartResize Then
                txtMovieSetFanartHeight.Text = .MovieSetFanartHeight.ToString
                txtMovieSetFanartWidth.Text = .MovieSetFanartWidth.ToString
            End If
            chkMovieSetGeneralMarkNew.Checked = .MovieSetGeneralMarkNew
            chkMovieSetImagesCacheEnabled.Checked = .MovieSetImagesCacheEnabled
            chkMovieSetImagesDisplayImageSelect.Checked = .MovieSetImagesDisplayImageSelect
            chkMovieSetImagesForceLanguage.Checked = .MovieSetImagesForceLanguage
            If .MovieSetImagesMediaLanguageOnly Then
                chkMovieSetImagesMediaLanguageOnly.Checked = True
                chkMovieSetImagesGetBlankImages.Checked = .MovieSetImagesGetBlankImages
                chkMovieSetImagesGetEnglishImages.Checked = .MovieSetImagesGetEnglishImages
            End If
            chkMovieSetKeyArtKeepExisting.Checked = .MovieSetKeyArtKeepExisting
            chkMovieSetKeyArtPrefSizeOnly.Checked = .MovieSetKeyArtPrefSizeOnly
            chkMovieSetKeyArtResize.Checked = .MovieSetKeyArtResize
            If .MovieSetKeyArtResize Then
                txtMovieSetKeyArtHeight.Text = .MovieSetKeyArtHeight.ToString
                txtMovieSetKeyArtWidth.Text = .MovieSetKeyArtWidth.ToString
            End If
            chkMovieSetLandscapeKeepExisting.Checked = .MovieSetLandscapeKeepExisting
            chkMovieSetLandscapePrefSizeOnly.Checked = .MovieSetLandscapePrefSizeOnly
            chkMovieSetLockPlot.Checked = .MovieSetLockPlot
            chkMovieSetLockTitle.Checked = .MovieSetLockTitle
            chkMovieSetPosterKeepExisting.Checked = .MovieSetPosterKeepExisting
            chkMovieSetPosterPrefSizeOnly.Checked = .MovieSetPosterPrefSizeOnly
            chkMovieSetPosterResize.Checked = .MovieSetPosterResize
            If .MovieSetPosterResize Then
                txtMovieSetPosterHeight.Text = .MovieSetPosterHeight.ToString
                txtMovieSetPosterWidth.Text = .MovieSetPosterWidth.ToString
            End If
            chkMovieSetScraperPlot.Checked = .MovieSetScraperPlot
            chkMovieSetScraperTitle.Checked = .MovieSetScraperTitle
            chkMovieThemeKeepExisting.Checked = .MovieThemeKeepExisting
            chkMovieTrailerKeepExisting.Checked = .MovieTrailerKeepExisting
            chkTVAllSeasonsBannerKeepExisting.Checked = .TVAllSeasonsBannerKeepExisting
            chkTVAllSeasonsBannerPrefSizeOnly.Checked = .TVAllSeasonsBannerPrefSizeOnly
            chkTVAllSeasonsBannerResize.Checked = .TVAllSeasonsBannerResize
            If .TVAllSeasonsBannerResize Then
                txtTVAllSeasonsBannerHeight.Text = .TVAllSeasonsBannerHeight.ToString
                txtTVAllSeasonsBannerWidth.Text = .TVAllSeasonsBannerWidth.ToString
            End If
            chkTVAllSeasonsFanartKeepExisting.Checked = .TVAllSeasonsFanartKeepExisting
            chkTVAllSeasonsFanartPrefSizeOnly.Checked = .TVAllSeasonsFanartPrefSizeOnly
            chkTVAllSeasonsFanartResize.Checked = .TVAllSeasonsFanartResize
            If .TVAllSeasonsFanartResize Then
                txtTVAllSeasonsFanartHeight.Text = .TVAllSeasonsFanartHeight.ToString
                txtTVAllSeasonsFanartWidth.Text = .TVAllSeasonsFanartWidth.ToString
            End If
            chkTVAllSeasonsLandscapeKeepExisting.Checked = .TVAllSeasonsLandscapeKeepExisting
            chkTVAllSeasonsLandscapePrefSizeOnly.Checked = .TVAllSeasonsLandscapePrefSizeOnly
            chkTVAllSeasonsPosterKeepExisting.Checked = .TVAllSeasonsPosterKeepExisting
            chkTVAllSeasonsPosterPrefSizeOnly.Checked = .TVAllSeasonsPosterPrefSizeOnly
            chkTVAllSeasonsPosterResize.Checked = .TVAllSeasonsPosterResize
            If .TVAllSeasonsPosterResize Then
                txtTVAllSeasonsPosterHeight.Text = .TVAllSeasonsPosterHeight.ToString
                txtTVAllSeasonsPosterWidth.Text = .TVAllSeasonsPosterWidth.ToString
            End If
            chkTVCleanDB.Checked = .TVCleanDB
            chkTVDisplayMissingEpisodes.Checked = .TVDisplayMissingEpisodes
            chkTVDisplayStatus.Checked = .TVDisplayStatus
            chkTVEpisodeFanartKeepExisting.Checked = .TVEpisodeFanartKeepExisting
            chkTVEpisodeFanartPrefSizeOnly.Checked = .TVEpisodeFanartPrefSizeOnly
            chkTVEpisodeFanartResize.Checked = .TVEpisodeFanartResize
            If .TVEpisodeFanartResize Then
                txtTVEpisodeFanartHeight.Text = .TVEpisodeFanartHeight.ToString
                txtTVEpisodeFanartWidth.Text = .TVEpisodeFanartWidth.ToString
            End If
            chkTVEpisodeNoFilter.Checked = .TVEpisodeNoFilter
            chkTVEpisodePosterKeepExisting.Checked = .TVEpisodePosterKeepExisting
            chkTVEpisodePosterPrefSizeOnly.Checked = .TVEpisodePosterPrefSizeOnly
            chkTVEpisodePosterResize.Checked = .TVEpisodePosterResize
            If .TVEpisodePosterResize Then
                txtTVEpisodePosterHeight.Text = .TVEpisodePosterHeight.ToString
                txtTVEpisodePosterWidth.Text = .TVEpisodePosterWidth.ToString
            End If
            chkTVEpisodePosterVideoExtraction.Checked = .TVEpisodePosterVideoExtraction
            chkTVEpisodePosterVideoExtractionPref.Checked = .TVEpisodePosterVideoExtractionPref
            chkTVEpisodeProperCase.Checked = .TVEpisodeProperCase
            chkTVGeneralClickScrape.Checked = .TVGeneralClickScrape
            chkTVGeneralClickScrapeAsk.Checked = .TVGeneralClickScrapeAsk
            chkTVGeneralMarkNewEpisodes.Checked = .TVGeneralMarkNewEpisodes
            chkTVGeneralMarkNewShows.Checked = .TVGeneralMarkNewShows
            chkTVImagesCacheEnabled.Checked = .TVImagesCacheEnabled
            chkTVImagesDisplayImageSelect.Checked = .TVImagesDisplayImageSelect
            chkTVImagesForceLanguage.Checked = .TVImagesForceLanguage
            If .TVImagesMediaLanguageOnly Then
                chkTVImagesMediaLanguageOnly.Checked = True
                chkTVImagesGetBlankImages.Checked = .TVImagesGetBlankImages
                chkTVImagesGetEnglishImages.Checked = .TVImagesGetEnglishImages
            End If
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
            chkTVScraperMetaDataScan.Checked = .TVScraperMetaDataScan
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
            chkTVSeasonBannerKeepExisting.Checked = .TVSeasonBannerKeepExisting
            chkTVSeasonBannerPrefSizeOnly.Checked = .TVSeasonBannerPrefSizeOnly
            chkTVSeasonBannerResize.Checked = .TVSeasonBannerResize
            If .TVSeasonBannerResize Then
                txtTVSeasonBannerHeight.Text = .TVSeasonBannerHeight.ToString
                txtTVSeasonBannerWidth.Text = .TVSeasonBannerWidth.ToString
            End If
            chkTVShowExtrafanartsKeepExisting.Checked = .TVShowExtrafanartsKeepExisting
            chkTVShowExtrafanartsPrefSizeOnly.Checked = .TVShowExtrafanartsPrefSizeOnly
            chkTVShowExtrafanartsPreselect.Checked = .TVShowExtrafanartsPreselect
            chkTVShowExtrafanartsResize.Checked = .TVShowExtrafanartsResize
            If .TVShowExtrafanartsResize Then
                txtTVShowExtrafanartsHeight.Text = .TVShowExtrafanartsHeight.ToString
                txtTVShowExtrafanartsWidth.Text = .TVShowExtrafanartsWidth.ToString
            End If
            chkTVSeasonFanartKeepExisting.Checked = .TVSeasonFanartKeepExisting
            chkTVSeasonFanartPrefSizeOnly.Checked = .TVSeasonFanartPrefSizeOnly
            chkTVSeasonFanartResize.Checked = .TVSeasonFanartResize
            If .TVSeasonFanartResize Then
                txtTVSeasonFanartHeight.Text = .TVSeasonFanartHeight.ToString
                txtTVSeasonFanartWidth.Text = .TVSeasonFanartWidth.ToString
            End If
            chkTVSeasonLandscapeKeepExisting.Checked = .TVSeasonLandscapeKeepExisting
            chkTVSeasonLandscapePrefSizeOnly.Checked = .TVSeasonLandscapePrefSizeOnly
            chkTVSeasonPosterKeepExisting.Checked = .TVSeasonPosterKeepExisting
            chkTVSeasonPosterPrefSizeOnly.Checked = .TVSeasonPosterPrefSizeOnly
            chkTVSeasonPosterResize.Checked = .TVSeasonPosterResize
            If .TVSeasonPosterResize Then
                txtTVSeasonPosterHeight.Text = .TVSeasonPosterHeight.ToString
                txtTVSeasonPosterWidth.Text = .TVSeasonPosterWidth.ToString
            End If
            chkTVShowBannerKeepExisting.Checked = .TVShowBannerKeepExisting
            chkTVShowBannerPrefSizeOnly.Checked = .TVShowBannerPrefSizeOnly
            chkTVShowBannerResize.Checked = .TVShowBannerResize
            If .TVShowBannerResize Then
                txtTVShowBannerHeight.Text = .TVShowBannerHeight.ToString
                txtTVShowBannerWidth.Text = .TVShowBannerWidth.ToString
            End If
            chkTVShowCharacterArtKeepExisting.Checked = .TVShowCharacterArtKeepExisting
            chkTVShowCharacterArtPrefSizeOnly.Checked = .TVShowCharacterArtPrefSizeOnly
            chkTVShowClearArtKeepExisting.Checked = .TVShowClearArtKeepExisting
            chkTVShowClearArtPrefSizeOnly.Checked = .TVShowClearArtPrefSizeOnly
            chkTVShowClearLogoKeepExisting.Checked = .TVShowClearLogoKeepExisting
            chkTVShowClearLogoPrefSizeOnly.Checked = .TVShowClearLogoPrefSizeOnly
            chkTVShowFanartKeepExisting.Checked = .TVShowFanartKeepExisting
            chkTVShowFanartPrefSizeOnly.Checked = .TVShowFanartPrefSizeOnly
            chkTVShowFanartResize.Checked = .TVShowFanartResize
            If .TVShowFanartResize Then
                txtTVShowFanartHeight.Text = .TVShowFanartHeight.ToString
                txtTVShowFanartWidth.Text = .TVShowFanartWidth.ToString
            End If
            chkTVShowLandscapeKeepExisting.Checked = .TVShowLandscapeKeepExisting
            chkTVShowLandscapePrefSizeOnly.Checked = .TVShowLandscapePrefSizeOnly
            chkTVShowKeyArtKeepExisting.Checked = .TVShowKeyArtKeepExisting
            chkTVShowKeyArtPrefSizeOnly.Checked = .TVShowKeyArtPrefSizeOnly
            chkTVShowKeyArtResize.Checked = .TVShowKeyArtResize
            If .TVShowKeyArtResize Then
                txtTVShowKeyArtHeight.Text = .TVShowKeyArtHeight.ToString
                txtTVShowKeyArtWidth.Text = .TVShowKeyArtWidth.ToString
            End If
            chkTVShowPosterKeepExisting.Checked = .TVShowPosterKeepExisting
            chkTVShowPosterPrefSizeOnly.Checked = .TVShowPosterPrefSizeOnly
            chkTVShowPosterResize.Checked = .TVShowPosterResize
            If .TVShowPosterResize Then
                txtTVShowPosterHeight.Text = .TVShowPosterHeight.ToString
                txtTVShowPosterWidth.Text = .TVShowPosterWidth.ToString
            End If
            chkTVShowProperCase.Checked = .TVShowProperCase
            chkTVShowThemeKeepExisting.Checked = .TVShowThemeKeepExisting
            lstFileSystemNoStackExts.Items.AddRange(.FileSystemNoStackExts.ToArray)
            If .MovieGeneralCustomScrapeButtonEnabled Then
                rbMovieGeneralCustomScrapeButtonEnabled.Checked = True
            Else
                rbMovieGeneralCustomScrapeButtonDisabled.Checked = True
            End If
            If .MovieSetGeneralCustomScrapeButtonEnabled Then
                rbMovieSetGeneralCustomScrapeButtonEnabled.Checked = True
            Else
                rbMovieSetGeneralCustomScrapeButtonDisabled.Checked = True
            End If
            If .TVGeneralCustomScrapeButtonEnabled Then
                rbTVGeneralCustomScrapeButtonEnabled.Checked = True
            Else
                rbTVGeneralCustomScrapeButtonDisabled.Checked = True
            End If
            txtGeneralImageFilterPosterMatchRate.Text = .GeneralImageFilterPosterMatchTolerance.ToString
            txtGeneralImageFilterFanartMatchRate.Text = .GeneralImageFilterFanartMatchTolerance.ToString
            txtGeneralVirtualDriveBinPath.Text = .GeneralVirtualDriveBinPath
            txtMovieGeneralCustomMarker1.Text = .MovieGeneralCustomMarker1Name
            txtMovieGeneralCustomMarker2.Text = .MovieGeneralCustomMarker2Name
            txtMovieGeneralCustomMarker3.Text = .MovieGeneralCustomMarker3Name
            txtMovieGeneralCustomMarker4.Text = .MovieGeneralCustomMarker4Name
            txtMovieTrailerDefaultSearch.Text = .MovieTrailerDefaultSearch
            txtTVScraperDurationRuntimeFormat.Text = .TVScraperDurationRuntimeFormat
            txtTVScraperEpisodeActorsLimit.Text = .TVScraperEpisodeActorsLimit.ToString
            txtTVScraperEpisodeGuestStarsLimit.Text = .TVScraperEpisodeGuestStarsLimit.ToString
            txtTVScraperShowActorsLimit.Text = .TVScraperShowActorsLimit.ToString
            txtTVScraperShowMPAANotRated.Text = .TVScraperShowMPAANotRated
            txtTVScraperShowGenreLimit.Text = .TVScraperShowGenreLimit.ToString
            txtTVScraperShowStudioLimit.Text = .TVScraperShowStudioLimit.ToString
            txtTVShowExtrafanartsLimit.Text = .TVShowExtrafanartsLimit.ToString
            txtTVSourcesRegexMultiPartMatching.Text = .TVMultiPartMatching
            txtTVSkipLessThan.Text = .TVSkipLessThan.ToString

            TempTVScraperSeasonTitleBlacklist = .TVScraperSeasonTitleBlacklist

            FillMovieSetScraperTitleRenamer()

            If .MovieLevTolerance > 0 Then
                chkMovieLevTolerance.Checked = True
                txtMovieLevTolerance.Enabled = True
                txtMovieLevTolerance.Text = .MovieLevTolerance.ToString
            End If

            GeneralAudioCodecMapping.AddRange(Master.eSettings.GeneralAudioCodecMapping)
            FillGeneralAudioCodecMapping()

            GeneralVideoCodecMapping.AddRange(Master.eSettings.GeneralVideoCodecMapping)
            FillGeneralVideoCodecMapping()

            GeneralVideoSourceByExtension.AddRange(Master.eSettings.GeneralVideoSourceByExtension)
            FillGeneralVideoSourceMappingByExtension()

            GeneralVideoSourceByRegex.AddRange(Master.eSettings.GeneralVideoSourceByRegex)
            FillGeneralVideoSourceMappingByRegex()

            MovieGeneralMediaListSorting.AddRange(.MovieGeneralMediaListSorting)
            LoadMovieGeneralMediaListSorting()

            MovieSetGeneralMediaListSorting.AddRange(.MovieSetGeneralMediaListSorting)
            LoadMovieSetGeneralMediaListSorting()

            TVGeneralEpisodeListSorting.AddRange(.TVGeneralEpisodeListSorting)
            LoadTVGeneralEpisodeListSorting()

            TVGeneralSeasonListSorting.AddRange(.TVGeneralSeasonListSorting)
            LoadTVGeneralSeasonListSorting()

            TVGeneralShowListSorting.AddRange(.TVGeneralShowListSorting)
            LoadTVGeneralShowListSorting()

            TVMeta.AddRange(.TVMetadataPerFileType)
            LoadTVMetadata()

            TVShowMatching.AddRange(.TVShowMatching)
            LoadTVShowMatching()

            Try
                cbMovieSetImagesForcedLanguage.Items.Clear()
                cbMovieSetImagesForcedLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Name).Distinct.ToArray)
                If cbMovieSetImagesForcedLanguage.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.MovieSetImagesForcedLanguage) Then
                        Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = .MovieSetImagesForcedLanguage)
                        If tLanguage IsNot Nothing Then
                            cbMovieSetImagesForcedLanguage.Text = tLanguage.Name
                        Else
                            tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.MovieSetImagesForcedLanguage))
                            If tLanguage IsNot Nothing Then
                                cbMovieSetImagesForcedLanguage.Text = tLanguage.Name
                            Else
                                cbMovieSetImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                            End If
                        End If
                    Else
                        cbMovieSetImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Try
                cbTVScraperShowCertLang.Items.Clear()
                cbTVScraperShowCertLang.Items.Add(Master.eLang.All)
                cbTVScraperShowCertLang.Items.AddRange((From lLang In APIXML.CertLanguagesXML.Language Select lLang.name).ToArray)
                If cbTVScraperShowCertLang.Items.Count > 0 Then
                    If .TVScraperShowCertLang = Master.eLang.All Then
                        cbTVScraperShowCertLang.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.TVScraperShowCertLang) Then
                        Dim tLanguage As CertLanguages = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = .TVScraperShowCertLang)
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
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Try
                cbTVGeneralLang.Items.Clear()
                cbTVGeneralLang.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
                If cbTVGeneralLang.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.TVGeneralLanguage) Then
                        Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = .TVGeneralLanguage)
                        If tLanguage IsNot Nothing AndAlso tLanguage.Description IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLanguage.Description) Then
                            cbTVGeneralLang.Text = tLanguage.Description
                        Else
                            tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.TVGeneralLanguage))
                            If tLanguage IsNot Nothing Then
                                cbTVGeneralLang.Text = tLanguage.Description
                            Else
                                cbTVGeneralLang.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                            End If
                        End If
                    Else
                        cbTVGeneralLang.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Try
                cbTVImagesForcedLanguage.Items.Clear()
                cbTVImagesForcedLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Name).Distinct.ToArray)
                If cbTVImagesForcedLanguage.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.TVImagesForcedLanguage) Then
                        Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = .TVImagesForcedLanguage)
                        If tLanguage IsNot Nothing Then
                            cbTVImagesForcedLanguage.Text = tLanguage.Name
                        Else
                            tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.TVImagesForcedLanguage))
                            If tLanguage IsNot Nothing Then
                                cbTVImagesForcedLanguage.Text = tLanguage.Name
                            Else
                                cbTVImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                            End If
                        End If
                    Else
                        cbTVImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If Not String.IsNullOrEmpty(.ProxyURI) AndAlso .ProxyPort >= 0 Then
                chkProxyEnable.Checked = True
                txtProxyURI.Text = .ProxyURI
                txtProxyPort.Text = .ProxyPort.ToString

                If Not String.IsNullOrEmpty(.ProxyCredentials.UserName) Then
                    chkProxyCredsEnable.Checked = True
                    txtProxyUsername.Text = .ProxyCredentials.UserName
                    txtProxyPassword.Text = .ProxyCredentials.Password
                    txtProxyDomain.Text = .ProxyCredentials.Domain
                End If
            End If

            chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
            chkMovieSetClickScrapeAsk.Enabled = chkMovieSetClickScrape.Checked
            chkTVGeneralClickScrapeAsk.Enabled = chkTVGeneralClickScrape.Checked
            txtTVScraperDurationRuntimeFormat.Enabled = .TVScraperUseMDDuration

            RefreshMovieSetSortTokens()
            RefreshMovieSortTokens()
            RefreshTVSources()
            RefreshTVSortTokens()
            RefreshTVShowFilters()
            RefreshTVEpisodeFilters()
            RefreshMovieFilters()
            RefreshFileSystemExcludedPaths()
            RefreshFileSystemValidExts()
            RefreshFileSystemValidSubtitlesExts()
            RefreshFileSystemValidThemeExts()


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

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

            '***************** Expert Parent ******************
            txtMovieSetBannerExpertParent.Text = .MovieSetBannerExpertParent
            txtMovieSetClearArtExpertParent.Text = .MovieSetClearArtExpertParent
            txtMovieSetClearLogoExpertParent.Text = .MovieSetClearLogoExpertParent
            txtMovieSetDiscArtExpertParent.Text = .MovieSetDiscArtExpertParent
            txtMovieSetFanartExpertParent.Text = .MovieSetFanartExpertParent
            txtMovieSetLandscapeExpertParent.Text = .MovieSetLandscapeExpertParent
            txtMovieSetNFOExpertParent.Text = .MovieSetNFOExpertParent
            txtMovieSetPosterExpertParent.Text = .MovieSetPosterExpertParent


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            chkTVUseFrodo.Checked = .TVUseFrodo
            chkTVEpisodeActorThumbsFrodo.Checked = .TVEpisodeActorThumbsFrodo
            chkTVEpisodeNFOFrodo.Checked = .TVEpisodeNFOFrodo
            chkTVEpisodePosterFrodo.Checked = .TVEpisodePosterFrodo
            chkTVSeasonBannerFrodo.Checked = .TVSeasonBannerFrodo
            chkTVSeasonFanartFrodo.Checked = .TVSeasonFanartFrodo
            chkTVSeasonPosterFrodo.Checked = .TVSeasonPosterFrodo
            chkTVShowActorThumbsFrodo.Checked = .TVShowActorThumbsFrodo
            chkTVShowBannerFrodo.Checked = .TVShowBannerFrodo
            chkTVShowExtrafanartsFrodo.Checked = .TVShowExtrafanartsFrodo
            chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
            chkTVShowNFOFrodo.Checked = .TVShowNFOFrodo
            chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '******** XBMC ArtworkDownloader settings **********
            chkTVUseAD.Checked = .TVUseAD
            chkTVSeasonLandscapeAD.Checked = .TVSeasonLandscapeAD
            chkTVShowCharacterArtAD.Checked = .TVShowCharacterArtAD
            chkTVShowClearArtAD.Checked = .TVShowClearArtAD
            chkTVShowClearLogoAD.Checked = .TVShowClearLogoAD
            chkTVShowLandscapeAD.Checked = .TVShowLandscapeAD

            '********* XBMC Extended Images settings ***********
            chkTVUseExtended.Checked = .TVUseExtended
            chkTVSeasonLandscapeExtended.Checked = .TVSeasonLandscapeExtended
            chkTVShowCharacterArtExtended.Checked = .TVShowCharacterArtExtended
            chkTVShowClearArtExtended.Checked = .TVShowClearArtExtended
            chkTVShowClearLogoExtended.Checked = .TVShowClearLogoExtended
            chkTVShowKeyArtExtended.Checked = .TVShowKeyArtExtended
            chkTVShowLandscapeExtended.Checked = .TVShowLandscapeExtended

            '************* XBMC TvTunes settings ***************
            chkTVShowThemeTvTunesEnabled.Checked = .TVShowThemeTvTunesEnable
            chkTVShowThemeTvTunesCustom.Checked = .TVShowThemeTvTunesCustom
            chkTVShowThemeTvTunesShowPath.Checked = .TVShowThemeTvTunesShowPath
            chkTVShowThemeTvTunesSub.Checked = .TVShowThemeTvTunesSub
            txtTVShowThemeTvTunesCustomPath.Text = .TVShowThemeTvTunesCustomPath
            txtTVShowThemeTvTunesSubDir.Text = .TVShowThemeTvTunesSubDir

            '****************** YAMJ settings ******************
            chkTVUseYAMJ.Checked = .TVUseYAMJ
            chkTVEpisodeNFOYAMJ.Checked = .TVEpisodeNFOYAMJ
            chkTVEpisodePosterYAMJ.Checked = .TVEpisodePosterYAMJ
            chkTVSeasonBannerYAMJ.Checked = .TVSeasonBannerYAMJ
            chkTVSeasonFanartYAMJ.Checked = .TVSeasonFanartYAMJ
            chkTVSeasonPosterYAMJ.Checked = .TVSeasonPosterYAMJ
            chkTVShowBannerYAMJ.Checked = .TVShowBannerYAMJ
            chkTVShowFanartYAMJ.Checked = .TVShowFanartYAMJ
            chkTVShowNFOYAMJ.Checked = .TVShowNFOYAMJ
            chkTVShowPosterYAMJ.Checked = .TVShowPosterYAMJ

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            chkTVUseBoxee.Checked = .TVUseBoxee
            chkTVEpisodeNFOBoxee.Checked = .TVEpisodeNFOBoxee
            chkTVEpisodePosterBoxee.Checked = .TVEpisodePosterBoxee
            chkTVSeasonPosterBoxee.Checked = .TVSeasonPosterBoxee
            chkTVShowBannerBoxee.Checked = .TVShowBannerBoxee
            chkTVShowFanartBoxee.Checked = .TVShowFanartBoxee
            chkTVShowNFOBoxee.Checked = .TVShowNFOBoxee
            chkTVShowPosterBoxee.Checked = .TVShowPosterBoxee

            '***************** Expert settings ******************
            chkTVUseExpert.Checked = .TVUseExpert

            '***************** Expert AllSeasons ****************
            txtTVAllSeasonsBannerExpert.Text = .TVAllSeasonsBannerExpert
            txtTVAllSeasonsFanartExpert.Text = .TVAllSeasonsFanartExpert
            txtTVAllSeasonsLandscapeExpert.Text = .TVAllSeasonsLandscapeExpert
            txtTVAllSeasonsPosterExpert.Text = .TVAllSeasonsPosterExpert

            '***************** Expert Episode *******************
            chkTVEpisodeActorthumbsExpert.Checked = .TVEpisodeActorThumbsExpert
            txtTVEpisodeActorThumbsExtExpert.Text = .TVEpisodeActorThumbsExtExpert
            txtTVEpisodeFanartExpert.Text = .TVEpisodeFanartExpert
            txtTVEpisodeNFOExpert.Text = .TVEpisodeNFOExpert
            txtTVEpisodePosterExpert.Text = .TVEpisodePosterExpert

            '***************** Expert Season *******************
            txtTVSeasonBannerExpert.Text = .TVSeasonBannerExpert
            txtTVSeasonFanartExpert.Text = .TVSeasonFanartExpert
            txtTVSeasonLandscapeExpert.Text = .TVSeasonLandscapeExpert
            txtTVSeasonPosterExpert.Text = .TVSeasonPosterExpert

            '***************** Expert Show *********************
            chkTVShowActorthumbsExpert.Checked = .TVShowActorThumbsExpert
            txtTVShowActorThumbsExtExpert.Text = .TVShowActorThumbsExtExpert
            txtTVShowBannerExpert.Text = .TVShowBannerExpert
            txtTVShowCharacterArtExpert.Text = .TVShowCharacterArtExpert
            txtTVShowClearArtExpert.Text = .TVShowClearArtExpert
            txtTVShowClearLogoExpert.Text = .TVShowClearLogoExpert
            chkTVShowExtrafanartsExpert.Checked = .TVShowExtrafanartsExpert
            txtTVShowFanartExpert.Text = .TVShowFanartExpert
            txtTVShowLandscapeExpert.Text = .TVShowLandscapeExpert
            txtTVShowNFOExpert.Text = .TVShowNFOExpert
            txtTVShowPosterExpert.Text = .TVShowPosterExpert

        End With
    End Sub

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Functions.PNLDoubleBuffer(pnlSettingsMain)
        SetUp()
        _SettingsPanels.Clear()
        SettingsPanels_AddMasterPanels()
        SettingsPanels_AddAddonPanels()
        AddButtons()

        Dim iBackground As New Bitmap(pnlSettingsTop.Width, pnlSettingsTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsTop.ClientRectangle)
            pnlSettingsTop.BackgroundImage = iBackground
        End Using

        iBackground = New Bitmap(pnlSettingsCurrent.Width, pnlSettingsCurrent.Height)
        Using b As Graphics = Graphics.FromImage(iBackground)
            b.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsCurrent.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsCurrent.ClientRectangle)
            pnlSettingsCurrent.BackgroundImage = iBackground
        End Using

        LoadIntLangs()
        LoadLangs()
        LoadThemes()
        FillSettings()
        lvTVSources.ListViewItemSorter = New ListViewItemComparer(1)

        'reset all triggers
        sResult = New Structures.SettingsResult
        didApply = False
        NoUpdate = False
        RaiseEvent LoadEnd()
    End Sub

    Private Sub Handle_StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)
        If SettingsPanelID = "!#RELOAD" Then
            FillSettings()
            Return
        End If
        Dim tSettingsPanel As New Containers.SettingsPanel
        Dim oSettingsPanel As New Containers.SettingsPanel
        SuspendLayout()

        'Get the panel that has been changed
        tSettingsPanel = _SettingsPanels.FirstOrDefault(Function(f) f.SettingsPanelID = SettingsPanelID)
        'Get the type of this panel to later select the correct (sub)menu
        Dim EType As Enums.SettingsPanelType = tSettingsPanel.Type
        'Get the current position
        Dim iCurrPanelOrder As Integer = tSettingsPanel.Order
        'Get the total count of panels in this menu
        Dim iTotalPanelsCount As Integer = _SettingsPanels.Where(Function(f) f.Type = EType).Count
        'Check if there is more than this panel and we have to re-order all panels
        If DiffOrder = 1 Then
            'move the panel down
            If DiffOrder < iTotalPanelsCount - 1 Then
                'the panel isn't already on the lowest position, so move it one down and the next panel one up
                Select Case True
                    Case ModulesManager.Instance.externalGenericModules.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Data_Movie.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Data_MovieSet.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Data_TV.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Image_Movie.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Image_Movieset.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Image_TV.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Theme_Movie.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Theme_TV.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1
                    Case ModulesManager.Instance.externalScrapersModules_Trailer_Movie.Where(Function(f) f.ProcessorModule.SettingsPanel.SettingsPanelID = tSettingsPanel.SettingsPanelID).Count = 1

                End Select
            End If
        ElseIf DiffOrder = -1 Then
            'move the panel up
            If iCurrPanelOrder > 0 Then

            End If
        End If




        If tSettingsPanel IsNot Nothing Then
            tSettingsPanel.ImageIndex = If(State, 9, 10)
            Try
                Dim t() As TreeNode = tvSettingsList.Nodes.Find(SettingsPanelID, True)

                If t.Count > 0 Then
                    If Not DiffOrder = 0 Then
                        Dim p As TreeNode = t(0).Parent
                        Dim i As Integer = t(0).Index
                        If DiffOrder < 0 AndAlso Not t(0).PrevNode Is Nothing Then
                            oSettingsPanel = _SettingsPanels.FirstOrDefault(Function(f) f.SettingsPanelID = t(0).PrevNode.Name)
                            If oSettingsPanel IsNot Nothing Then oSettingsPanel.Order = i + (DiffOrder * -1)
                        End If
                        If DiffOrder > 0 AndAlso Not t(0).NextNode Is Nothing Then
                            oSettingsPanel = _SettingsPanels.FirstOrDefault(Function(f) f.SettingsPanelID = t(0).NextNode.Name)
                            If oSettingsPanel IsNot Nothing Then oSettingsPanel.Order = i + (DiffOrder * -1)
                        End If
                        p.Nodes.Remove(t(0))
                        p.Nodes.Insert(i + DiffOrder, t(0))
                        t(0).TreeView.SelectedNode = t(0)
                        tSettingsPanel.Order = i + DiffOrder
                    End If
                    t(0).ImageIndex = If(State, 9, 10)
                    t(0).SelectedImageIndex = If(State, 9, 10)
                    pbSettingsCurrent.Image = ilSettings.Images(If(State, 9, 10))
                End If

                Dim iPosition As Integer = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Data_Movie
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Data_Movie.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Data_MovieSet
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Data_MovieSet.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Data_TV
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Data_TV.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Image_Movie
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Image_Movie.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Image_Movieset
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Image_Movieset.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Image_TV
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Image_TV.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Theme_Movie
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Theme_Movie.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Theme_TV
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Theme_TV.Count})
                    iPosition += 1
                Next
                iPosition = 0
                For Each s In ModulesManager.Instance.externalScrapersModules_Trailer_Movie
                    s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                                   .Position = iPosition,
                                                   .TotalCount = ModulesManager.Instance.externalScrapersModules_Trailer_Movie.Count})
                    iPosition += 1
                Next
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
        ResumeLayout()
        SetApplyButton(True)
    End Sub

    Private Sub LoadIntLangs()
        cbGeneralLanguage.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            Try
                alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            Catch
            End Try
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToList)
            cbGeneralLanguage.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub LoadLangs()
        cbMovieLanguageOverlay.Items.Add(Master.eLang.Disabled)
        cbMovieLanguageOverlay.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
        cbTVLanguageOverlay.Items.Add(Master.eLang.Disabled)
        cbTVLanguageOverlay.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
    End Sub

    Private Sub LoadMovieGeneralMediaListSorting()
        Dim lvItem As ListViewItem
        lvMovieGeneralMediaListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In MovieGeneralMediaListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieGeneralMediaListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadMovieSetGeneralMediaListSorting()
        Dim lvItem As ListViewItem
        lvMovieSetGeneralMediaListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In MovieSetGeneralMediaListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieSetGeneralMediaListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralEpisodeListSorting()
        Dim lvItem As ListViewItem
        lvTVGeneralEpisodeListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In TVGeneralEpisodeListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVGeneralEpisodeListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralSeasonListSorting()
        Dim lvItem As ListViewItem
        lvTVGeneralSeasonListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In TVGeneralSeasonListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVGeneralSeasonListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralShowListSorting()
        Dim lvItem As ListViewItem
        lvTVGeneralShowListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In TVGeneralShowListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVGeneralShowListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVShowMatching()
        Dim lvItem As ListViewItem
        lvTVSourcesRegexTVShowMatching.Items.Clear()
        For Each rShow As Settings.regexp In TVShowMatching
            lvItem = New ListViewItem(rShow.ID.ToString)
            lvItem.SubItems.Add(rShow.Regexp)
            lvItem.SubItems.Add(If(Not rShow.defaultSeason.ToString = "-2", rShow.defaultSeason.ToString, String.Empty))
            lvItem.SubItems.Add(If(rShow.byDate, "Yes", "No"))
            lvTVSourcesRegexTVShowMatching.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadThemes()
        cbGeneralTheme.Items.Clear()
        Dim diDefaults As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Themes"))
        If diDefaults.Exists Then cbGeneralTheme.Items.AddRange(diDefaults.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)

        Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Themes"))
        If diCustom.Exists Then cbGeneralTheme.Items.AddRange(diCustom.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)
    End Sub

    Private Sub LoadCustomScraperButtonModifierTypes_Movie()
        Dim items As New Dictionary(Of String, Enums.ModifierType)
        items.Add(Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All)
        items.Add(Master.eLang.GetString(973, "Actor Thumbs Only"), Enums.ModifierType.MainActorThumbs)
        items.Add(Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner)
        items.Add(Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt)
        items.Add(Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo)
        items.Add(Master.eLang.GetString(1124, "DiscArt Only"), Enums.ModifierType.MainDiscArt)
        items.Add(Master.eLang.GetString(975, "Extrafanarts Only"), Enums.ModifierType.MainExtrafanarts)
        items.Add(Master.eLang.GetString(74, "Extrathumbs Only"), Enums.ModifierType.MainExtrathumbs)
        items.Add(Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart)
        items.Add(Master.eLang.GetString(303, "KeyArt Only"), Enums.ModifierType.MainKeyArt)
        items.Add(Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape)
        items.Add(Master.eLang.GetString(76, "Meta Data Only"), Enums.ModifierType.MainMeta)
        items.Add(Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO)
        items.Add(Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster)
        items.Add(Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme)
        items.Add(Master.eLang.GetString(75, "Trailer Only"), Enums.ModifierType.MainTrailer)
        cbMovieGeneralCustomScrapeButtonModifierType.DataSource = items.ToList
        cbMovieGeneralCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbMovieGeneralCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonModifierTypes_MovieSet()
        Dim items As New Dictionary(Of String, Enums.ModifierType)
        items.Add(Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All)
        items.Add(Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner)
        items.Add(Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt)
        items.Add(Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo)
        items.Add(Master.eLang.GetString(1124, "DiscArt Only"), Enums.ModifierType.MainDiscArt)
        items.Add(Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart)
        items.Add(Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape)
        items.Add(Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO)
        items.Add(Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster)
        cbMovieSetGeneralCustomScrapeButtonModifierType.DataSource = items.ToList
        cbMovieSetGeneralCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbMovieSetGeneralCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonModifierTypes_TV()
        Dim items As New Dictionary(Of String, Enums.ModifierType)
        items.Add(Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All)
        items.Add(Master.eLang.GetString(973, "Actor Thumbs Only"), Enums.ModifierType.MainActorThumbs)
        items.Add(Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner)
        items.Add(Master.eLang.GetString(1121, "CharacterArt Only"), Enums.ModifierType.MainCharacterArt)
        items.Add(Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt)
        items.Add(Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo)
        items.Add(Master.eLang.GetString(975, "Extrafanarts Only"), Enums.ModifierType.MainExtrafanarts)
        items.Add(Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart)
        items.Add(Master.eLang.GetString(303, "KeyArt Only"), Enums.ModifierType.MainKeyArt)
        items.Add(Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape)
        items.Add(Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO)
        items.Add(Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster)
        items.Add(Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme)
        cbTVGeneralCustomScrapeButtonModifierType.DataSource = items.ToList
        cbTVGeneralCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbTVGeneralCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonScrapeTypes()
        Dim strAll As String = Master.eLang.GetString(68, "All")
        Dim strFilter As String = Master.eLang.GetString(624, "Current Filter")
        Dim strMarked As String = Master.eLang.GetString(48, "Marked")
        Dim strMissing As String = Master.eLang.GetString(40, "Missing Items")
        Dim strNew As String = Master.eLang.GetString(47, "New")

        Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Dim strAuto As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")

        Dim items As New Dictionary(Of String, Enums.ScrapeType)
        items.Add(String.Concat(strAll, " - ", strAuto), Enums.ScrapeType.AllAuto)
        items.Add(String.Concat(strAll, " - ", strAsk), Enums.ScrapeType.AllAsk)
        items.Add(String.Concat(strAll, " - ", strSkip), Enums.ScrapeType.AllSkip)
        items.Add(String.Concat(strMissing, " - ", strAuto), Enums.ScrapeType.MissingAuto)
        items.Add(String.Concat(strMissing, " - ", strAsk), Enums.ScrapeType.MissingAsk)
        items.Add(String.Concat(strMissing, " - ", strSkip), Enums.ScrapeType.MissingSkip)
        items.Add(String.Concat(strNew, " - ", strAuto), Enums.ScrapeType.NewAuto)
        items.Add(String.Concat(strNew, " - ", strAsk), Enums.ScrapeType.NewAsk)
        items.Add(String.Concat(strNew, " - ", strSkip), Enums.ScrapeType.NewSkip)
        items.Add(String.Concat(strMarked, " - ", strAuto), Enums.ScrapeType.MarkedAuto)
        items.Add(String.Concat(strMarked, " - ", strAsk), Enums.ScrapeType.MarkedAsk)
        items.Add(String.Concat(strMarked, " - ", strSkip), Enums.ScrapeType.MarkedSkip)
        items.Add(String.Concat(strFilter, " - ", strAuto), Enums.ScrapeType.FilterAuto)
        items.Add(String.Concat(strFilter, " - ", strAsk), Enums.ScrapeType.FilterAsk)
        items.Add(String.Concat(strFilter, " - ", strSkip), Enums.ScrapeType.FilterSkip)
        cbMovieGeneralCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbMovieGeneralCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbMovieGeneralCustomScrapeButtonScrapeType.ValueMember = "Value"
        cbMovieSetGeneralCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbMovieSetGeneralCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbMovieSetGeneralCustomScrapeButtonScrapeType.ValueMember = "Value"
        cbTVGeneralCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbTVGeneralCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbTVGeneralCustomScrapeButtonScrapeType.ValueMember = "Value"
    End Sub

    Private Sub LoadGeneralDateTime()
        Dim items As New Dictionary(Of String, Enums.DateTime)
        items.Add(Master.eLang.GetString(1210, "Current DateTime when adding"), Enums.DateTime.Now)
        items.Add(Master.eLang.GetString(1227, "ctime (fallback to mtime)"), Enums.DateTime.ctime)
        items.Add(Master.eLang.GetString(1211, "mtime (fallback to ctime)"), Enums.DateTime.mtime)
        items.Add(Master.eLang.GetString(1212, "Newer of mtime and ctime"), Enums.DateTime.Newer)
        cbGeneralDateTime.DataSource = items.ToList
        cbGeneralDateTime.DisplayMember = "Key"
        cbGeneralDateTime.ValueMember = "Value"
    End Sub

    Private Sub LoadBannerSizes_Movie_MovieSet()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x185", Enums.ImageSize.HD185)
        LoadItemsToComboBox(cbMovieSetBannerPrefSize, items)
    End Sub

    Private Sub LoadBannerSizes_TV()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x185", Enums.ImageSize.HD185)
        items.Add("758x140", Enums.ImageSize.HD140)
        LoadItemsToComboBox(cbTVAllSeasonsBannerPrefSize, items)
        LoadItemsToComboBox(cbTVSeasonBannerPrefSize, items)
        LoadItemsToComboBox(cbTVShowBannerPrefSize, items)
    End Sub

    Private Sub LoadBannerTypes()
        Dim items As New Dictionary(Of String, Enums.TVBannerType)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.TVBannerType.Any)
        items.Add(Master.eLang.GetString(746, "Blank"), Enums.TVBannerType.Blank)
        items.Add(Master.eLang.GetString(747, "Graphical"), Enums.TVBannerType.Graphical)
        items.Add(Master.eLang.GetString(748, "Text"), Enums.TVBannerType.Text)
        cbTVAllSeasonsBannerPrefType.DataSource = items.ToList
        cbTVAllSeasonsBannerPrefType.DisplayMember = "Key"
        cbTVAllSeasonsBannerPrefType.ValueMember = "Value"
        cbTVSeasonBannerPrefType.DataSource = items.ToList
        cbTVSeasonBannerPrefType.DisplayMember = "Key"
        cbTVSeasonBannerPrefType.ValueMember = "Value"
        cbTVShowBannerPrefType.DataSource = items.ToList
        cbTVShowBannerPrefType.DisplayMember = "Key"
        cbTVShowBannerPrefType.ValueMember = "Value"
    End Sub

    Private Sub LoadCharacterArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("512x512", Enums.ImageSize.HD512)
        LoadItemsToComboBox(cbTVShowCharacterArtPrefSize, items)
    End Sub

    Private Sub LoadClearArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x562", Enums.ImageSize.HD562)
        items.Add("500x281", Enums.ImageSize.SD281)
        LoadItemsToComboBox(cbMovieSetClearArtPrefSize, items)
        LoadItemsToComboBox(cbTVShowClearArtPrefSize, items)
    End Sub

    Private Sub LoadClearLogoSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("800x310", Enums.ImageSize.HD310)
        items.Add("400x155", Enums.ImageSize.SD155)
        LoadItemsToComboBox(cbMovieSetClearLogoPrefSize, items)
        LoadItemsToComboBox(cbTVShowClearLogoPrefSize, items)
    End Sub

    Private Sub LoadDiscArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x1000", Enums.ImageSize.HD1000)
        LoadItemsToComboBox(cbMovieSetDiscArtPrefSize, items)
    End Sub

    Private Sub LoadFanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("3840x2160", Enums.ImageSize.UHD2160)
        items.Add("2560x1440", Enums.ImageSize.QHD1440)
        items.Add("1920x1080", Enums.ImageSize.HD1080)
        items.Add("1280x720", Enums.ImageSize.HD720)
        LoadItemsToComboBox(cbMovieSetFanartPrefSize, items)
        LoadItemsToComboBox(cbTVAllSeasonsFanartPrefSize, items)
        LoadItemsToComboBox(cbTVEpisodeFanartPrefSize, items)
        LoadItemsToComboBox(cbTVSeasonFanartPrefSize, items)
        LoadItemsToComboBox(cbTVShowExtrafanartsPrefSize, items)
        LoadItemsToComboBox(cbTVShowFanartPrefSize, items)
    End Sub

    Private Sub LoadItemsToComboBox(ByRef CBox As ComboBox, Items As Dictionary(Of String, Enums.ImageSize))
        CBox.DataSource = Items.ToList
        CBox.DisplayMember = "Key"
        CBox.ValueMember = "Value"
    End Sub

    Private Sub LoadLandscapeSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x562", Enums.ImageSize.HD562)
        LoadItemsToComboBox(cbMovieSetLandscapePrefSize, items)
        LoadItemsToComboBox(cbTVAllSeasonsLandscapePrefSize, items)
        LoadItemsToComboBox(cbTVSeasonLandscapePrefSize, items)
        LoadItemsToComboBox(cbTVShowLandscapePrefSize, items)
    End Sub

    Private Sub LoadPosterSizes_Movie_MovieSet()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("2000x3000", Enums.ImageSize.HD3000)
        items.Add("1400x2100", Enums.ImageSize.HD2100)
        items.Add("1000x1500", Enums.ImageSize.HD1500)
        items.Add("1000x1426", Enums.ImageSize.HD1426)
        LoadItemsToComboBox(cbMovieSetKeyArtPrefSize, items)
        LoadItemsToComboBox(cbMovieSetPosterPrefSize, items)
    End Sub

    Private Sub LoadPosterSizes_TV()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("2000x3000", Enums.ImageSize.HD3000)
        items.Add("1000x1500", Enums.ImageSize.HD1500)
        items.Add("1000x1426", Enums.ImageSize.HD1426)
        items.Add("680x1000", Enums.ImageSize.HD1000)
        LoadItemsToComboBox(cbTVAllSeasonsPosterPrefSize, items)
        LoadItemsToComboBox(cbTVShowKeyArtPrefSize, items)
        LoadItemsToComboBox(cbTVShowPosterPrefSize, items)

        Dim items2 As New Dictionary(Of String, Enums.ImageSize)
        items2.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items2.Add("1000x1500", Enums.ImageSize.HD1500)
        items2.Add("1000x1426", Enums.ImageSize.HD1426)
        items2.Add("400x578", Enums.ImageSize.HD578)
        LoadItemsToComboBox(cbTVSeasonPosterPrefSize, items2)

        Dim items3 As New Dictionary(Of String, Enums.ImageSize)
        items3.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items3.Add("3840x2160", Enums.ImageSize.UHD2160)
        items3.Add("1920x1080", Enums.ImageSize.HD1080)
        items3.Add("1280x720", Enums.ImageSize.HD720)
        items3.Add("400x225", Enums.ImageSize.SD225)
        LoadItemsToComboBox(cbTVEpisodePosterPrefSize, items3)
    End Sub

    Private Sub LoadMovieTrailerQualities()
        Dim items As New Dictionary(Of String, Enums.TrailerVideoQuality)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.TrailerVideoQuality.Any)
        items.Add("2160p 60fps", Enums.TrailerVideoQuality.HD2160p60fps)
        items.Add("2160p", Enums.TrailerVideoQuality.HD2160p)
        items.Add("1440p", Enums.TrailerVideoQuality.HD1440p)
        items.Add("1080p 60fps", Enums.TrailerVideoQuality.HD1080p60fps)
        items.Add("1080p", Enums.TrailerVideoQuality.HD1080p)
        items.Add("720p 60fps", Enums.TrailerVideoQuality.HD720p60fps)
        items.Add("720p", Enums.TrailerVideoQuality.HD720p)
        items.Add("480p", Enums.TrailerVideoQuality.HQ480p)
        items.Add("360p", Enums.TrailerVideoQuality.SQ360p)
        items.Add("240p", Enums.TrailerVideoQuality.SQ240p)
        items.Add("144p", Enums.TrailerVideoQuality.SQ144p)
        items.Add("144p 15fps", Enums.TrailerVideoQuality.SQ144p15fps)
        cbMovieTrailerMinVideoQual.DataSource = items.ToList
        cbMovieTrailerMinVideoQual.DisplayMember = "Key"
        cbMovieTrailerMinVideoQual.ValueMember = "Value"
        cbMovieTrailerPrefVideoQual.DataSource = items.ToList
        cbMovieTrailerPrefVideoQual.DisplayMember = "Key"
        cbMovieTrailerPrefVideoQual.ValueMember = "Value"
    End Sub

    Private Sub LoadTVScraperOptionsOrdering()
        Dim items As New Dictionary(Of String, Enums.EpisodeOrdering)
        items.Add(Master.eLang.GetString(438, "Standard"), Enums.EpisodeOrdering.Standard)
        items.Add(Master.eLang.GetString(1067, "DVD"), Enums.EpisodeOrdering.DVD)
        items.Add(Master.eLang.GetString(839, "Absolute"), Enums.EpisodeOrdering.Absolute)
        items.Add(Master.eLang.GetString(1332, "Day Of Year"), Enums.EpisodeOrdering.DayOfYear)
        cbTVScraperOptionsOrdering.DataSource = items.ToList
        cbTVScraperOptionsOrdering.DisplayMember = "Key"
        cbTVScraperOptionsOrdering.ValueMember = "Value"
    End Sub

    Private Sub LoadTVMetadata()
        lstTVScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In TVMeta
            lstTVScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub lstTVEpisodeFilter_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstTVEpisodeFilter.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVEpisodeFilter()
    End Sub

    Private Sub lstMovieFilters_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstMovieFilters.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveMovieFilter()
    End Sub

    Private Sub lstFileSystemValidExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemValidVideoExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemValidExts()
    End Sub

    Private Sub lstFileSystemValidSubtitlesExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemValidSubtitlesExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemValidSubtitlesExts()
    End Sub

    Private Sub lstFileSystemValidThemeExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemValidThemeExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemValidThemeExts()
    End Sub

    Private Sub lstFileSystemNoStackExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemNoStackExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemNoStackExts()
    End Sub

    Private Sub lstTVShowFilter_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstTVShowFilter.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVShowFilter()
    End Sub

    Private Sub lstMovieSortTokens_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstMovieSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveMovieSortToken()
    End Sub

    Private Sub lstMovieSetSortTokens_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstMovieSetSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveMovieSetSortToken()
    End Sub

    Private Sub lsttvSortTokens_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstTVSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVSortToken()
    End Sub

    Private Sub lstTVScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstTVScraperDefFIExt.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVMetaData()
    End Sub

    Private Sub lstTVScraperDefFIExt_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstTVScraperDefFIExt.SelectedIndexChanged
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            btnTVScraperDefFIExtEdit.Enabled = True
            btnTVScraperDefFIExtRemove.Enabled = True
            txtTVScraperDefFIExt.Text = String.Empty
        Else
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvTVSourcesRegexTVShowMatching.DoubleClick
        If lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvTVSourcesRegexTVShowMatching.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVShowMatching()
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvTVSourcesRegexTVShowMatching.SelectedIndexChanged
        If Not String.IsNullOrEmpty(btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then ClearTVShowMatching()
    End Sub

    Private Sub lvTVSources_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvTVSources.ColumnClick
        lvTVSources.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvTVSources_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvTVSources.DoubleClick
        If lvTVSources.SelectedItems.Count > 0 Then
            Using dTVSource As New dlgSourceTVShow
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = DialogResult.OK Then
                    RefreshTVSources()
                    sResult.NeedsReload_TVShow = True
                    SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub lvTVSources_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvTVSources.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVSource()
    End Sub

    Private Sub RefreshTVEpisodeFilters()
        lstTVEpisodeFilter.Items.Clear()
        lstTVEpisodeFilter.Items.AddRange(Master.eSettings.TVEpisodeFilterCustom.ToArray)
    End Sub

    Private Sub RefreshMovieFilters()
        lstMovieFilters.Items.Clear()
        lstMovieFilters.Items.AddRange(Master.eSettings.MovieFilterCustom.ToArray)
    End Sub

    Private Sub RefreshMovieSortTokens()
        lstMovieSortTokens.Items.Clear()
        lstMovieSortTokens.Items.AddRange(Master.eSettings.MovieSortTokens.ToArray)
    End Sub

    Private Sub RefreshMovieSetSortTokens()
        lstMovieSetSortTokens.Items.Clear()
        lstMovieSetSortTokens.Items.AddRange(Master.eSettings.MovieSetSortTokens.ToArray)
    End Sub

    Private Sub RefreshTVShowFilters()
        lstTVShowFilter.Items.Clear()
        lstTVShowFilter.Items.AddRange(Master.eSettings.TVShowFilterCustom.ToArray)
    End Sub

    Private Sub RefreshTVSortTokens()
        lstTVSortTokens.Items.Clear()
        lstTVSortTokens.Items.AddRange(Master.eSettings.TVSortTokens.ToArray)
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        lvTVSources.Items.Clear()
        For Each s As Database.DBSource In Master.DB.Load_AllSources_TVShow
            lvItem = New ListViewItem(CStr(s.ID))
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(s.Language)
            lvItem.SubItems.Add(s.EpisodeOrdering.ToString)
            lvItem.SubItems.Add(If(s.Exclude, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(s.EpisodeSorting.ToString)
            lvItem.SubItems.Add(If(s.IsSingle, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVSources.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshFileSystemExcludedPaths()
        lstFileSystemExcludedPaths.Items.Clear()
        lstFileSystemExcludedPaths.Items.AddRange(Master.DB.GetExcludedPaths.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidExts()
        lstFileSystemValidVideoExts.Items.Clear()
        lstFileSystemValidVideoExts.Items.AddRange(Master.eSettings.FileSystemValidExts.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidSubtitlesExts()
        lstFileSystemValidSubtitlesExts.Items.Clear()
        lstFileSystemValidSubtitlesExts.Items.AddRange(Master.eSettings.FileSystemValidSubtitlesExts.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidThemeExts()
        lstFileSystemValidThemeExts.Items.Clear()
        lstFileSystemValidThemeExts.Items.AddRange(Master.eSettings.FileSystemValidThemeExts.ToArray)
    End Sub

    Private Sub RemoveCurrPanel()
        If pnlSettingsMain.Controls.Count > 0 Then
            pnlSettingsMain.Controls.Remove(_currPanel)
        End If
    End Sub

    Private Sub RemoveTVEpisodeFilter()
        If lstTVEpisodeFilter.Items.Count > 0 AndAlso lstTVEpisodeFilter.SelectedItems.Count > 0 Then
            While lstTVEpisodeFilter.SelectedItems.Count > 0
                lstTVEpisodeFilter.Items.Remove(lstTVEpisodeFilter.SelectedItems(0))
            End While
            SetApplyButton(True)
            sResult.NeedsReload_TVEpisode = True
        End If
    End Sub

    Private Sub RemoveMovieFilter()
        If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItems.Count > 0 Then
            While lstMovieFilters.SelectedItems.Count > 0
                lstMovieFilters.Items.Remove(lstMovieFilters.SelectedItems(0))
            End While
            SetApplyButton(True)
            sResult.NeedsReload_Movie = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidExts()
        If lstFileSystemValidVideoExts.Items.Count > 0 AndAlso lstFileSystemValidVideoExts.SelectedItems.Count > 0 Then
            While lstFileSystemValidVideoExts.SelectedItems.Count > 0
                lstFileSystemValidVideoExts.Items.Remove(lstFileSystemValidVideoExts.SelectedItems(0))
            End While
            SetApplyButton(True)
            sResult.NeedsDBClean_Movie = True
            sResult.NeedsDBClean_TV = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidSubtitlesExts()
        If lstFileSystemValidSubtitlesExts.Items.Count > 0 AndAlso lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0 Then
            While lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0
                lstFileSystemValidSubtitlesExts.Items.Remove(lstFileSystemValidSubtitlesExts.SelectedItems(0))
            End While
            SetApplyButton(True)
            sResult.NeedsReload_Movie = True
            sResult.NeedsReload_TVEpisode = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidThemeExts()
        If lstFileSystemValidThemeExts.Items.Count > 0 AndAlso lstFileSystemValidThemeExts.SelectedItems.Count > 0 Then
            While lstFileSystemValidThemeExts.SelectedItems.Count > 0
                lstFileSystemValidThemeExts.Items.Remove(lstFileSystemValidThemeExts.SelectedItems(0))
            End While
            SetApplyButton(True)
            sResult.NeedsReload_Movie = True
            sResult.NeedsReload_TVEpisode = True
        End If
    End Sub

    Private Sub RemoveFileSystemNoStackExts()
        If lstFileSystemNoStackExts.Items.Count > 0 AndAlso lstFileSystemNoStackExts.SelectedItems.Count > 0 Then
            While lstFileSystemNoStackExts.SelectedItems.Count > 0
                lstFileSystemNoStackExts.Items.Remove(lstFileSystemNoStackExts.SelectedItems(0))
            End While
            SetApplyButton(True)
            sResult.NeedsDBUpdate_Movie = True
            sResult.NeedsDBUpdate_TV = True
        End If
    End Sub

    Private Sub RemoveTVShowMatching()
        Dim ID As Integer
        For Each lItem As ListViewItem In lvTVSourcesRegexTVShowMatching.SelectedItems
            ID = Convert.ToInt32(lItem.Text)
            Dim selRex = From lRegex As Settings.regexp In TVShowMatching Where lRegex.ID = ID
            If selRex.Count > 0 Then
                TVShowMatching.Remove(selRex(0))
                SetApplyButton(True)
            End If
        Next
        LoadTVShowMatching()
    End Sub

    Private Sub RemoveTVShowFilter()
        If lstTVShowFilter.Items.Count > 0 AndAlso lstTVShowFilter.SelectedItems.Count > 0 Then
            While lstTVShowFilter.SelectedItems.Count > 0
                lstTVShowFilter.Items.Remove(lstTVShowFilter.SelectedItems(0))
            End While
            SetApplyButton(True)
            sResult.NeedsReload_TVShow = True
        End If
    End Sub

    Private Sub RemoveMovieSortToken()
        If lstMovieSortTokens.Items.Count > 0 AndAlso lstMovieSortTokens.SelectedItems.Count > 0 Then
            While lstMovieSortTokens.SelectedItems.Count > 0
                lstMovieSortTokens.Items.Remove(lstMovieSortTokens.SelectedItems(0))
            End While
            SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveMovieSetSortToken()
        If lstMovieSetSortTokens.Items.Count > 0 AndAlso lstMovieSetSortTokens.SelectedItems.Count > 0 Then
            While lstMovieSetSortTokens.SelectedItems.Count > 0
                lstMovieSetSortTokens.Items.Remove(lstMovieSetSortTokens.SelectedItems(0))
            End While
            SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveTVSortToken()
        If lstTVSortTokens.Items.Count > 0 AndAlso lstTVSortTokens.SelectedItems.Count > 0 Then
            While lstTVSortTokens.SelectedItems.Count > 0
                lstTVSortTokens.Items.Remove(lstTVSortTokens.SelectedItems(0))
            End While
            SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveTVMetaData()
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In TVMeta
                If x.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                    TVMeta.Remove(x)
                    LoadTVMetadata()
                    SetApplyButton(True)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub RemoveTVSource()
        If lvTVSources.SelectedItems.Count > 0 Then
            If MessageBox.Show(Master.eLang.GetString(1033, "Are you sure you want to remove the selected sources? This will remove the tv shows from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                lvTVSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.Int64, 0, "idSource")
                        While lvTVSources.SelectedItems.Count > 0
                            parSource.Value = lvTVSources.SelectedItems(0).SubItems(0).Text
                            SQLcommand.CommandText = String.Concat("DELETE FROM tvshowsource WHERE idSource = (?);")
                            SQLcommand.ExecuteNonQuery()
                            lvTVSources.Items.Remove(lvTVSources.SelectedItems(0))
                        End While
                    End Using
                    SQLtransaction.Commit()
                End Using

                lvTVSources.Sort()
                lvTVSources.EndUpdate()
                lvTVSources.Refresh()

                SetApplyButton(True)
            End If
        End If
    End Sub

    Private Sub RenumberTVShowMatching()
        For i As Integer = 0 To TVShowMatching.Count - 1
            TVShowMatching(i).ID = i
        Next
    End Sub

    Private Sub RenumberMovieGeneralMediaListSorting()
        For i As Integer = 0 To MovieGeneralMediaListSorting.Count - 1
            MovieGeneralMediaListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberMovieSetGeneralMediaListSorting()
        For i As Integer = 0 To MovieSetGeneralMediaListSorting.Count - 1
            MovieSetGeneralMediaListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVEpisodeGeneralMediaListSorting()
        For i As Integer = 0 To TVGeneralEpisodeListSorting.Count - 1
            TVGeneralEpisodeListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVSeasonGeneralMediaListSorting()
        For i As Integer = 0 To TVGeneralSeasonListSorting.Count - 1
            TVGeneralSeasonListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVShowGeneralMediaListSorting()
        For i As Integer = 0 To TVGeneralShowListSorting.Count - 1
            TVGeneralShowListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub SaveSettings(ByVal isApply As Boolean)
        With Master.eSettings
            .FileSystemNoStackExts.Clear()
            .FileSystemNoStackExts.AddRange(lstFileSystemNoStackExts.Items.OfType(Of String).ToList)
            .FileSystemValidExts.Clear()
            .FileSystemValidExts.AddRange(lstFileSystemValidVideoExts.Items.OfType(Of String).ToList)
            .FileSystemValidSubtitlesExts.Clear()
            .FileSystemValidSubtitlesExts.AddRange(lstFileSystemValidSubtitlesExts.Items.OfType(Of String).ToList)
            .FileSystemValidThemeExts.Clear()
            .FileSystemValidThemeExts.AddRange(lstFileSystemValidThemeExts.Items.OfType(Of String).ToList)
            .GeneralCheckUpdates = chkGeneralCheckUpdates.Checked
            .GeneralDateAddedIgnoreNFO = chkGeneralDateAddedIgnoreNFO.Checked
            .GeneralDigitGrpSymbolVotes = chkGeneralDigitGrpSymbolVotes.Checked
            .GeneralDateTime = CType(cbGeneralDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTime)).Value
            .GeneralDoubleClickScrape = chkGeneralDoubleClickScrape.Checked
            .GeneralVirtualDriveLetter = cbGeneralVirtualDriveLetter.Text
            .GeneralVirtualDriveBinPath = txtGeneralVirtualDriveBinPath.Text
            .GeneralDisplayBanner = chkGeneralDisplayBanner.Checked
            .GeneralDisplayCharacterArt = chkGeneralDisplayCharacterArt.Checked
            .GeneralDisplayClearArt = chkGeneralDisplayClearArt.Checked
            .GeneralDisplayClearLogo = chkGeneralDisplayClearLogo.Checked
            .GeneralDisplayDiscArt = chkGeneralDisplayDiscArt.Checked
            .GeneralDisplayFanart = chkGeneralDisplayFanart.Checked
            .GeneralDisplayFanartSmall = chkGeneralDisplayFanartSmall.Checked
            .GeneralDisplayKeyArt = chkGeneralDisplayKeyArt.Checked
            .GeneralDisplayLandscape = chkGeneralDisplayLandscape.Checked
            .GeneralDisplayPoster = chkGeneralDisplayPoster.Checked
            .GeneralImagesGlassOverlay = chkGeneralImagesGlassOverlay.Checked
            .GeneralImageFilter = chkGeneralImageFilter.Checked
            .GeneralImageFilterAutoscraper = chkGeneralImageFilterAutoscraper.Checked
            .GeneralImageFilterFanart = chkGeneralImageFilterFanart.Checked
            .GeneralImageFilterImagedialog = chkGeneralImageFilterImagedialog.Checked
            .GeneralImageFilterPoster = chkGeneralImageFilterPoster.Checked
            If Not String.IsNullOrEmpty(txtGeneralImageFilterFanartMatchRate.Text) AndAlso Integer.TryParse(txtGeneralImageFilterFanartMatchRate.Text, 4) Then
                .GeneralImageFilterFanartMatchTolerance = Convert.ToInt32(txtGeneralImageFilterFanartMatchRate.Text)
            Else
                .GeneralImageFilterFanartMatchTolerance = 4
            End If
            If Not String.IsNullOrEmpty(txtGeneralImageFilterPosterMatchRate.Text) AndAlso Integer.TryParse(txtGeneralImageFilterPosterMatchRate.Text, 1) Then
                .GeneralImageFilterPosterMatchTolerance = Convert.ToInt32(txtGeneralImageFilterPosterMatchRate.Text)
            Else
                .GeneralImageFilterPosterMatchTolerance = 1
            End If
            .GeneralLanguage = cbGeneralLanguage.Text
            .GeneralOverwriteNfo = chkGeneralOverwriteNfo.Checked
            .GeneralShowGenresText = chkGeneralDisplayGenresText.Checked
            .GeneralShowLangFlags = chkGeneralDisplayLangFlags.Checked
            .GeneralShowImgDims = chkGeneralDisplayImgDims.Checked
            .GeneralShowImgNames = chkGeneralDisplayImgNames.Checked
            .GeneralSourceFromFolder = chkGeneralSourceFromFolder.Checked
            .GeneralTheme = cbGeneralTheme.Text
            .GeneralVideoSourceByExtensionEnabled = chkGeneralVideoSourceMappingByExtensionEnabled.Checked
            .GeneralVideoSourceByRegexEnabled = chkGeneralVideoSourceMappingByRegexEnabled.Checked
            .MovieClickScrape = chkMovieClickScrape.Checked
            .MovieClickScrapeAsk = chkMovieClickScrapeAsk.Checked
            .MovieDisplayYear = chkMovieDisplayYear.Checked
            .MovieFilterCustom.Clear()
            .MovieFilterCustom.AddRange(lstMovieFilters.Items.OfType(Of String).ToList)
            If .MovieFilterCustom.Count <= 0 Then .MovieFilterCustomIsEmpty = True
            .MovieGeneralCustomMarker1Color = btnMovieGeneralCustomMarker1.BackColor.ToArgb
            .MovieGeneralCustomMarker2Color = btnMovieGeneralCustomMarker2.BackColor.ToArgb
            .MovieGeneralCustomMarker3Color = btnMovieGeneralCustomMarker3.BackColor.ToArgb
            .MovieGeneralCustomMarker4Color = btnMovieGeneralCustomMarker4.BackColor.ToArgb
            .MovieGeneralCustomMarker1Name = txtMovieGeneralCustomMarker1.Text
            .MovieGeneralCustomMarker2Name = txtMovieGeneralCustomMarker2.Text
            .MovieGeneralCustomMarker3Name = txtMovieGeneralCustomMarker3.Text
            .MovieGeneralCustomMarker4Name = txtMovieGeneralCustomMarker4.Text
            .MovieGeneralCustomScrapeButtonEnabled = rbMovieGeneralCustomScrapeButtonEnabled.Checked
            .MovieGeneralCustomScrapeButtonModifierType = CType(cbMovieGeneralCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .MovieGeneralCustomScrapeButtonScrapeType = CType(cbMovieGeneralCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .MovieGeneralFlagLang = If(cbMovieLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, cbMovieLanguageOverlay.Text)

            .MovieGeneralMarkNew = chkMovieGeneralMarkNew.Checked
            .MovieGeneralMediaListSorting.Clear()
            .MovieGeneralMediaListSorting.AddRange(MovieGeneralMediaListSorting)
            .MovieLevTolerance = If(Not String.IsNullOrEmpty(txtMovieLevTolerance.Text), Convert.ToInt32(txtMovieLevTolerance.Text), 0)
            .MovieProperCase = chkMovieProperCase.Checked
            .MovieSetBannerHeight = If(Not String.IsNullOrEmpty(txtMovieSetBannerHeight.Text), Convert.ToInt32(txtMovieSetBannerHeight.Text), 0)
            .MovieSetBannerKeepExisting = chkMovieSetBannerKeepExisting.Checked
            .MovieSetBannerPrefSize = CType(cbMovieSetBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetBannerPrefSizeOnly = chkMovieSetBannerPrefSizeOnly.Checked
            .MovieSetBannerResize = chkMovieSetBannerResize.Checked
            .MovieSetBannerWidth = If(Not String.IsNullOrEmpty(txtMovieSetBannerWidth.Text), Convert.ToInt32(txtMovieSetBannerWidth.Text), 0)
            .MovieSetCleanDB = chkMovieSetCleanDB.Checked
            .MovieSetCleanFiles = chkMovieSetCleanFiles.Checked
            .MovieSetClearArtKeepExisting = chkMovieSetClearArtKeepExisting.Checked
            .MovieSetClearArtPrefSize = CType(cbMovieSetClearArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetClearArtPrefSizeOnly = chkMovieSetClearArtPrefSizeOnly.Checked
            .MovieSetClearLogoKeepExisting = chkMovieSetClearLogoKeepExisting.Checked
            .MovieSetClearLogoPrefSize = CType(cbMovieSetClearLogoPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetClearLogoPrefSizeOnly = chkMovieSetClearLogoPrefSizeOnly.Checked
            .MovieSetClickScrape = chkMovieSetClickScrape.Checked
            .MovieSetClickScrapeAsk = chkMovieSetClickScrapeAsk.Checked
            .MovieSetDiscArtKeepExisting = chkMovieSetDiscArtKeepExisting.Checked
            .MovieSetDiscArtPrefSize = CType(cbMovieSetDiscArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetDiscArtPrefSizeOnly = chkMovieSetDiscArtPrefSizeOnly.Checked
            .MovieSetFanartHeight = If(Not String.IsNullOrEmpty(txtMovieSetFanartHeight.Text), Convert.ToInt32(txtMovieSetFanartHeight.Text), 0)
            .MovieSetFanartKeepExisting = chkMovieSetFanartKeepExisting.Checked
            .MovieSetFanartPrefSize = CType(cbMovieSetFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetFanartPrefSizeOnly = chkMovieSetFanartPrefSizeOnly.Checked
            .MovieSetFanartResize = chkMovieSetFanartResize.Checked
            .MovieSetFanartWidth = If(Not String.IsNullOrEmpty(txtMovieSetFanartWidth.Text), Convert.ToInt32(txtMovieSetFanartWidth.Text), 0)
            .MovieSetGeneralCustomScrapeButtonEnabled = rbMovieSetGeneralCustomScrapeButtonEnabled.Checked
            .MovieSetGeneralCustomScrapeButtonModifierType = CType(cbMovieSetGeneralCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .MovieSetGeneralCustomScrapeButtonScrapeType = CType(cbMovieSetGeneralCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .MovieSetGeneralMarkNew = chkMovieSetGeneralMarkNew.Checked
            .MovieSetGeneralMediaListSorting.Clear()
            .MovieSetGeneralMediaListSorting.AddRange(MovieSetGeneralMediaListSorting)
            .MovieSetImagesCacheEnabled = chkMovieSetImagesCacheEnabled.Checked
            .MovieSetImagesDisplayImageSelect = chkMovieSetImagesDisplayImageSelect.Checked
            If Not String.IsNullOrEmpty(cbMovieSetImagesForcedLanguage.Text) Then
                .MovieSetImagesForcedLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Name = cbMovieSetImagesForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .MovieSetImagesForceLanguage = chkMovieSetImagesForceLanguage.Checked
            .MovieSetImagesGetBlankImages = chkMovieSetImagesGetBlankImages.Checked
            .MovieSetImagesGetEnglishImages = chkMovieSetImagesGetEnglishImages.Checked
            .MovieSetImagesMediaLanguageOnly = chkMovieSetImagesMediaLanguageOnly.Checked
            .MovieSetKeyArtHeight = If(Not String.IsNullOrEmpty(txtMovieSetKeyArtHeight.Text), Convert.ToInt32(txtMovieSetKeyArtHeight.Text), 0)
            .MovieSetKeyArtKeepExisting = chkMovieSetKeyArtKeepExisting.Checked
            .MovieSetKeyArtPrefSize = CType(cbMovieSetKeyArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetKeyArtPrefSizeOnly = chkMovieSetKeyArtPrefSizeOnly.Checked
            .MovieSetKeyArtResize = chkMovieSetKeyArtResize.Checked
            .MovieSetKeyArtWidth = If(Not String.IsNullOrEmpty(txtMovieSetKeyArtWidth.Text), Convert.ToInt32(txtMovieSetKeyArtWidth.Text), 0)
            .MovieSetLandscapeKeepExisting = chkMovieSetLandscapeKeepExisting.Checked
            .MovieSetLandscapePrefSize = CType(cbMovieSetLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetLandscapePrefSizeOnly = chkMovieSetLandscapePrefSizeOnly.Checked
            .MovieSetLockPlot = chkMovieSetLockPlot.Checked
            .MovieSetLockTitle = chkMovieSetLockTitle.Checked
            .MovieSetPosterHeight = If(Not String.IsNullOrEmpty(txtMovieSetPosterHeight.Text), Convert.ToInt32(txtMovieSetPosterHeight.Text), 0)
            .MovieSetPosterKeepExisting = chkMovieSetPosterKeepExisting.Checked
            .MovieSetPosterPrefSize = CType(cbMovieSetPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetPosterPrefSizeOnly = chkMovieSetPosterPrefSizeOnly.Checked
            .MovieSetPosterResize = chkMovieSetPosterResize.Checked
            .MovieSetPosterWidth = If(Not String.IsNullOrEmpty(txtMovieSetPosterWidth.Text), Convert.ToInt32(txtMovieSetPosterWidth.Text), 0)
            .MovieSetScraperPlot = chkMovieSetScraperPlot.Checked
            .MovieSetScraperTitle = chkMovieSetScraperTitle.Checked
            .MovieSortTokens.Clear()
            .MovieSortTokens.AddRange(lstMovieSortTokens.Items.OfType(Of String).ToList)
            If .MovieSortTokens.Count <= 0 Then .MovieSortTokensIsEmpty = True
            .MovieSetSortTokens.Clear()
            .MovieSetSortTokens.AddRange(lstMovieSetSortTokens.Items.OfType(Of String).ToList)
            If .MovieSetSortTokens.Count <= 0 Then .MovieSetSortTokensIsEmpty = True
            .MovieThemeKeepExisting = chkMovieThemeKeepExisting.Checked
            .MovieTrailerDefaultSearch = txtMovieTrailerDefaultSearch.Text
            .MovieTrailerKeepExisting = chkMovieTrailerKeepExisting.Checked
            .MovieTrailerMinVideoQual = CType(cbMovieTrailerMinVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
            .MovieTrailerPrefVideoQual = CType(cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
            .TVAllSeasonsBannerHeight = If(Not String.IsNullOrEmpty(txtTVAllSeasonsBannerHeight.Text), Convert.ToInt32(txtTVAllSeasonsBannerHeight.Text), 0)
            .TVAllSeasonsBannerKeepExisting = chkTVAllSeasonsBannerKeepExisting.Checked
            .TVAllSeasonsBannerPrefSize = CType(cbTVAllSeasonsBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsBannerPrefSizeOnly = chkTVAllSeasonsBannerPrefSizeOnly.Checked
            .TVAllSeasonsBannerPrefType = CType(cbTVAllSeasonsBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVAllSeasonsBannerResize = chkTVAllSeasonsBannerResize.Checked
            .TVAllSeasonsBannerWidth = If(Not String.IsNullOrEmpty(txtTVAllSeasonsBannerWidth.Text), Convert.ToInt32(txtTVAllSeasonsBannerWidth.Text), 0)
            .TVAllSeasonsFanartHeight = If(Not String.IsNullOrEmpty(txtTVAllSeasonsFanartHeight.Text), Convert.ToInt32(txtTVAllSeasonsFanartHeight.Text), 0)
            .TVAllSeasonsFanartKeepExisting = chkTVAllSeasonsFanartKeepExisting.Checked
            .TVAllSeasonsFanartPrefSize = CType(cbTVAllSeasonsFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsFanartPrefSizeOnly = chkTVAllSeasonsFanartPrefSizeOnly.Checked
            .TVAllSeasonsFanartResize = chkTVAllSeasonsFanartResize.Checked
            .TVAllSeasonsFanartWidth = If(Not String.IsNullOrEmpty(txtTVAllSeasonsFanartWidth.Text), Convert.ToInt32(txtTVAllSeasonsFanartWidth.Text), 0)
            .TVAllSeasonsLandscapeKeepExisting = chkTVAllSeasonsLandscapeKeepExisting.Checked
            .TVAllSeasonsLandscapePrefSize = CType(cbTVAllSeasonsLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsLandscapePrefSizeOnly = chkTVAllSeasonsLandscapePrefSizeOnly.Checked
            .TVAllSeasonsPosterHeight = If(Not String.IsNullOrEmpty(txtTVAllSeasonsPosterHeight.Text), Convert.ToInt32(txtTVAllSeasonsPosterHeight.Text), 0)
            .TVAllSeasonsPosterKeepExisting = chkTVAllSeasonsPosterKeepExisting.Checked
            .TVAllSeasonsPosterPrefSize = CType(cbTVAllSeasonsPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsPosterPrefSizeOnly = chkTVAllSeasonsPosterPrefSizeOnly.Checked
            .TVAllSeasonsPosterResize = chkTVAllSeasonsPosterResize.Checked
            .TVAllSeasonsPosterWidth = If(Not String.IsNullOrEmpty(txtTVAllSeasonsPosterWidth.Text), Convert.ToInt32(txtTVAllSeasonsPosterWidth.Text), 0)
            .TVCleanDB = chkTVCleanDB.Checked
            .TVDisplayMissingEpisodes = chkTVDisplayMissingEpisodes.Checked
            .TVDisplayStatus = chkTVDisplayStatus.Checked
            .TVEpisodeFanartHeight = If(Not String.IsNullOrEmpty(txtTVEpisodeFanartHeight.Text), Convert.ToInt32(txtTVEpisodeFanartHeight.Text), 0)
            .TVEpisodeFanartKeepExisting = chkTVEpisodeFanartKeepExisting.Checked
            .TVEpisodeFanartPrefSize = CType(cbTVEpisodeFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVEpisodeFanartPrefSizeOnly = chkTVEpisodeFanartPrefSizeOnly.Checked
            .TVEpisodeFanartResize = chkTVEpisodeFanartResize.Checked
            .TVEpisodeFanartWidth = If(Not String.IsNullOrEmpty(txtTVEpisodeFanartWidth.Text), Convert.ToInt32(txtTVEpisodeFanartWidth.Text), 0)
            .TVEpisodeFilterCustom.Clear()
            .TVEpisodeFilterCustom.AddRange(lstTVEpisodeFilter.Items.OfType(Of String).ToList)
            If .TVEpisodeFilterCustom.Count <= 0 Then .TVEpisodeFilterCustomIsEmpty = True
            .TVEpisodeNoFilter = chkTVEpisodeNoFilter.Checked
            .TVEpisodePosterHeight = If(Not String.IsNullOrEmpty(txtTVEpisodePosterHeight.Text), Convert.ToInt32(txtTVEpisodePosterHeight.Text), 0)
            .TVEpisodePosterKeepExisting = chkTVEpisodePosterKeepExisting.Checked
            .TVEpisodePosterPrefSize = CType(cbTVEpisodePosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVEpisodePosterPrefSizeOnly = chkTVEpisodePosterPrefSizeOnly.Checked
            .TVEpisodePosterResize = chkTVEpisodePosterResize.Checked
            .TVEpisodePosterVideoExtraction = chkTVEpisodePosterVideoExtraction.Checked
            .TVEpisodePosterWidth = If(Not String.IsNullOrEmpty(txtTVEpisodePosterWidth.Text), Convert.ToInt32(txtTVEpisodePosterWidth.Text), 0)
            .TVEpisodeProperCase = chkTVEpisodeProperCase.Checked
            .TVGeneralEpisodeListSorting.Clear()
            .TVGeneralEpisodeListSorting.AddRange(TVGeneralEpisodeListSorting)
            .TVGeneralFlagLang = If(cbTVLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, cbTVLanguageOverlay.Text)
            If Not String.IsNullOrEmpty(cbTVGeneralLang.Text) Then
                .TVGeneralLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbTVGeneralLang.Text).Abbreviation
            End If
            .TVGeneralClickScrape = chkTVGeneralClickScrape.Checked
            .TVGeneralClickScrapeAsk = chkTVGeneralClickScrapeAsk.Checked
            .TVGeneralCustomScrapeButtonEnabled = rbTVGeneralCustomScrapeButtonEnabled.Checked
            .TVGeneralCustomScrapeButtonModifierType = CType(cbTVGeneralCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .TVGeneralCustomScrapeButtonScrapeType = CType(cbTVGeneralCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .TVGeneralMarkNewEpisodes = chkTVGeneralMarkNewEpisodes.Checked
            .TVGeneralMarkNewShows = chkTVGeneralMarkNewShows.Checked
            .TVGeneralSeasonListSorting.Clear()
            .TVGeneralSeasonListSorting.AddRange(TVGeneralSeasonListSorting)
            .TVGeneralShowListSorting.Clear()
            .TVGeneralShowListSorting.AddRange(TVGeneralShowListSorting)
            .TVImagesCacheEnabled = chkTVImagesCacheEnabled.Checked
            .TVImagesDisplayImageSelect = chkTVImagesDisplayImageSelect.Checked
            If Not String.IsNullOrEmpty(cbTVImagesForcedLanguage.Text) Then
                .TVImagesForcedLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Name = cbTVImagesForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .TVImagesForceLanguage = chkTVImagesForceLanguage.Checked
            .TVImagesGetBlankImages = chkTVImagesGetBlankImages.Checked
            .TVImagesGetEnglishImages = chkTVImagesGetEnglishImages.Checked
            .TVImagesMediaLanguageOnly = chkTVImagesMediaLanguageOnly.Checked
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
            .TVMultiPartMatching = txtTVSourcesRegexMultiPartMatching.Text
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
            .TVScraperMetaDataScan = chkTVScraperMetaDataScan.Checked
            .TVScraperOptionsOrdering = CType(cbTVScraperOptionsOrdering.SelectedItem, KeyValuePair(Of String, Enums.EpisodeOrdering)).Value
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
                    .TVScraperShowCertLang = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.name = cbTVScraperShowCertLang.Text).abbreviation
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
            .TVScraperSeasonTitleBlacklist = TempTVScraperSeasonTitleBlacklist
            .TVScraperShowUserRating = chkTVScraperShowUserRating.Checked
            .TVScraperUseDisplaySeasonEpisode = chkTVScraperUseDisplaySeasonEpisode.Checked
            .TVScraperUseMDDuration = chkTVScraperUseMDDuration.Checked
            .TVScraperUseSRuntimeForEp = chkTVScraperUseSRuntimeForEp.Checked
            .TVSeasonBannerHeight = If(Not String.IsNullOrEmpty(txtTVSeasonBannerHeight.Text), Convert.ToInt32(txtTVSeasonBannerHeight.Text), 0)
            .TVSeasonBannerKeepExisting = chkTVSeasonBannerKeepExisting.Checked
            .TVSeasonBannerPrefSize = CType(cbTVSeasonBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonBannerPrefSizeOnly = chkTVSeasonBannerPrefSizeOnly.Checked
            .TVSeasonBannerPrefType = CType(cbTVSeasonBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVSeasonBannerResize = chkTVSeasonBannerResize.Checked
            .TVSeasonBannerWidth = If(Not String.IsNullOrEmpty(txtTVSeasonBannerWidth.Text), Convert.ToInt32(txtTVSeasonBannerWidth.Text), 0)
            .TVSeasonFanartHeight = If(Not String.IsNullOrEmpty(txtTVSeasonFanartHeight.Text), Convert.ToInt32(txtTVSeasonFanartHeight.Text), 0)
            .TVSeasonFanartKeepExisting = chkTVSeasonFanartKeepExisting.Checked
            .TVSeasonFanartPrefSize = CType(cbTVSeasonFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonFanartPrefSizeOnly = chkTVSeasonFanartPrefSizeOnly.Checked
            .TVSeasonFanartResize = chkTVSeasonFanartResize.Checked
            .TVSeasonFanartWidth = If(Not String.IsNullOrEmpty(txtTVSeasonFanartWidth.Text), Convert.ToInt32(txtTVSeasonFanartWidth.Text), 0)
            .TVSeasonLandscapeKeepExisting = chkTVSeasonLandscapeKeepExisting.Checked
            .TVSeasonLandscapePrefSize = CType(cbTVSeasonLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonLandscapePrefSizeOnly = chkTVSeasonLandscapePrefSizeOnly.Checked
            .TVSeasonPosterHeight = If(Not String.IsNullOrEmpty(txtTVSeasonPosterHeight.Text), Convert.ToInt32(txtTVSeasonPosterHeight.Text), 0)
            .TVSeasonPosterKeepExisting = chkTVSeasonPosterKeepExisting.Checked
            .TVSeasonPosterPrefSize = CType(cbTVSeasonPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonPosterPrefSizeOnly = chkTVSeasonPosterPrefSizeOnly.Checked
            .TVSeasonPosterResize = chkTVSeasonPosterResize.Checked
            .TVSeasonPosterWidth = If(Not String.IsNullOrEmpty(txtTVSeasonPosterWidth.Text), Convert.ToInt32(txtTVSeasonPosterWidth.Text), 0)
            .TVShowBannerHeight = If(Not String.IsNullOrEmpty(txtTVShowBannerHeight.Text), Convert.ToInt32(txtTVShowBannerHeight.Text), 0)
            .TVShowBannerKeepExisting = chkTVShowBannerKeepExisting.Checked
            .TVShowBannerPrefSize = CType(cbTVShowBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowBannerPrefSizeOnly = chkTVShowBannerPrefSizeOnly.Checked
            .TVShowBannerPrefType = CType(cbTVShowBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVShowBannerResize = chkTVShowBannerResize.Checked
            .TVShowBannerWidth = If(Not String.IsNullOrEmpty(txtTVShowBannerWidth.Text), Convert.ToInt32(txtTVShowBannerWidth.Text), 0)
            .TVShowCharacterArtKeepExisting = chkTVShowCharacterArtKeepExisting.Checked
            .TVShowCharacterArtPrefSize = CType(cbTVShowCharacterArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowCharacterArtPrefSizeOnly = chkTVShowCharacterArtPrefSizeOnly.Checked
            .TVShowClearArtKeepExisting = chkTVShowClearArtKeepExisting.Checked
            .TVShowClearArtPrefSize = CType(cbTVShowClearArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowClearArtPrefSizeOnly = chkTVShowClearArtPrefSizeOnly.Checked
            .TVShowClearLogoKeepExisting = chkTVShowClearLogoKeepExisting.Checked
            .TVShowClearLogoPrefSize = CType(cbTVShowClearLogoPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowClearLogoPrefSizeOnly = chkTVShowClearLogoPrefSizeOnly.Checked
            .TVShowExtrafanartsHeight = If(Not String.IsNullOrEmpty(txtTVShowExtrafanartsHeight.Text), Convert.ToInt32(txtTVShowExtrafanartsHeight.Text), 0)
            .TVShowExtrafanartsLimit = If(Not String.IsNullOrEmpty(txtTVShowExtrafanartsLimit.Text), Convert.ToInt32(txtTVShowExtrafanartsLimit.Text), 0)
            .TVShowExtrafanartsKeepExisting = chkTVShowExtrafanartsKeepExisting.Checked
            .TVShowExtrafanartsPrefSize = CType(cbTVShowExtrafanartsPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowExtrafanartsPrefSizeOnly = chkTVShowExtrafanartsPrefSizeOnly.Checked
            .TVShowExtrafanartsPreselect = chkTVShowExtrafanartsPreselect.Checked
            .TVShowExtrafanartsResize = chkTVShowExtrafanartsResize.Checked
            .TVShowExtrafanartsWidth = If(Not String.IsNullOrEmpty(txtTVShowExtrafanartsWidth.Text), Convert.ToInt32(txtTVShowExtrafanartsWidth.Text), 0)
            .TVShowFanartHeight = If(Not String.IsNullOrEmpty(txtTVShowFanartHeight.Text), Convert.ToInt32(txtTVShowFanartHeight.Text), 0)
            .TVShowFanartKeepExisting = chkTVShowFanartKeepExisting.Checked
            .TVShowFanartPrefSize = CType(cbTVShowFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowFanartPrefSizeOnly = chkTVShowFanartPrefSizeOnly.Checked
            .TVShowFanartResize = chkTVShowFanartResize.Checked
            .TVShowFanartWidth = If(Not String.IsNullOrEmpty(txtTVShowFanartWidth.Text), Convert.ToInt32(txtTVShowFanartWidth.Text), 0)
            .TVShowFilterCustom.Clear()
            .TVShowFilterCustom.AddRange(lstTVShowFilter.Items.OfType(Of String).ToList)
            If .TVShowFilterCustom.Count <= 0 Then .TVShowFilterCustomIsEmpty = True
            .TVShowKeyArtHeight = If(Not String.IsNullOrEmpty(txtTVShowKeyArtHeight.Text), Convert.ToInt32(txtTVShowKeyArtHeight.Text), 0)
            .TVShowKeyArtKeepExisting = chkTVShowKeyArtKeepExisting.Checked
            .TVShowKeyArtPrefSize = CType(cbTVShowKeyArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowKeyArtPrefSizeOnly = chkTVShowKeyArtPrefSizeOnly.Checked
            .TVShowKeyArtResize = chkTVShowKeyArtResize.Checked
            .TVShowKeyArtWidth = If(Not String.IsNullOrEmpty(txtTVShowKeyArtWidth.Text), Convert.ToInt32(txtTVShowKeyArtWidth.Text), 0)
            .TVShowLandscapeKeepExisting = chkTVShowLandscapeKeepExisting.Checked
            .TVShowLandscapePrefSize = CType(cbTVShowLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowLandscapePrefSizeOnly = chkTVShowLandscapePrefSizeOnly.Checked
            .TVShowPosterHeight = If(Not String.IsNullOrEmpty(txtTVShowPosterHeight.Text), Convert.ToInt32(txtTVShowPosterHeight.Text), 0)
            .TVShowPosterKeepExisting = chkTVShowPosterKeepExisting.Checked
            .TVShowPosterPrefSize = CType(cbTVShowPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowPosterPrefSizeOnly = chkTVShowPosterPrefSizeOnly.Checked
            .TVShowPosterResize = chkTVShowPosterResize.Checked
            .TVShowPosterWidth = If(Not String.IsNullOrEmpty(txtTVShowPosterWidth.Text), Convert.ToInt32(txtTVShowPosterWidth.Text), 0)
            .TVShowProperCase = chkTVShowProperCase.Checked
            .TVShowMatching.Clear()
            .TVShowMatching.AddRange(TVShowMatching)
            .TVShowThemeKeepExisting = chkTVShowThemeKeepExisting.Checked
            If Not String.IsNullOrEmpty(txtTVSkipLessThan.Text) AndAlso Integer.TryParse(txtTVSkipLessThan.Text, 0) Then
                .TVSkipLessThan = Convert.ToInt32(txtTVSkipLessThan.Text)
            Else
                .TVSkipLessThan = 0
            End If
            .TVSortTokens.Clear()
            .TVSortTokens.AddRange(lstTVSortTokens.Items.OfType(Of String).ToList)
            If .TVSortTokens.Count <= 0 Then .TVSortTokensIsEmpty = True

            SaveGeneralAudioCodecMapping()
            SaveGeneralVideoCodecMapping()
            SaveGeneralVideoSourceByExtension()
            SaveGeneralVideoSourceByRegex()
            SaveMovieSetScraperTitleRenamer()

            If Not String.IsNullOrEmpty(txtProxyURI.Text) AndAlso Not String.IsNullOrEmpty(txtProxyPort.Text) Then
                .ProxyURI = txtProxyURI.Text
                .ProxyPort = Convert.ToInt32(txtProxyPort.Text)

                If Not String.IsNullOrEmpty(txtProxyUsername.Text) AndAlso Not String.IsNullOrEmpty(txtProxyPassword.Text) Then
                    .ProxyCredentials.UserName = txtProxyUsername.Text
                    .ProxyCredentials.Password = txtProxyPassword.Text
                    .ProxyCredentials.Domain = txtProxyDomain.Text
                Else
                    .ProxyCredentials = New NetworkCredential
                End If
            Else
                .ProxyURI = String.Empty
                .ProxyPort = -1
            End If


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

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
            .MovieSetKeyArtExtended = chkMovieSetKeyArtExtended.Checked
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

            '***************** Expert Parent ******************
            .MovieSetBannerExpertParent = txtMovieSetBannerExpertParent.Text
            .MovieSetClearArtExpertParent = txtMovieSetClearArtExpertParent.Text
            .MovieSetClearLogoExpertParent = txtMovieSetClearLogoExpertParent.Text
            .MovieSetDiscArtExpertParent = txtMovieSetDiscArtExpertParent.Text
            .MovieSetFanartExpertParent = txtMovieSetFanartExpertParent.Text
            .MovieSetLandscapeExpertParent = txtMovieSetLandscapeExpertParent.Text
            .MovieSetNFOExpertParent = txtMovieSetNFOExpertParent.Text
            .MovieSetPosterExpertParent = txtMovieSetPosterExpertParent.Text


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            .TVUseFrodo = chkTVUseFrodo.Checked
            .TVEpisodeActorThumbsFrodo = chkTVEpisodeActorThumbsFrodo.Checked
            .TVEpisodeNFOFrodo = chkTVEpisodeNFOFrodo.Checked
            .TVEpisodePosterFrodo = chkTVEpisodePosterFrodo.Checked
            .TVSeasonBannerFrodo = chkTVSeasonBannerFrodo.Checked
            .TVSeasonFanartFrodo = chkTVSeasonFanartFrodo.Checked
            .TVSeasonPosterFrodo = chkTVSeasonPosterFrodo.Checked
            .TVShowActorThumbsFrodo = chkTVShowActorThumbsFrodo.Checked
            .TVShowBannerFrodo = chkTVShowBannerFrodo.Checked
            .TVShowExtrafanartsFrodo = chkTVShowExtrafanartsFrodo.Checked
            .TVShowFanartFrodo = chkTVShowFanartFrodo.Checked
            .TVShowNFOFrodo = chkTVShowNFOFrodo.Checked
            .TVShowPosterFrodo = chkTVShowPosterFrodo.Checked

            '*************** XBMC Eden settings ****************

            '************* XBMC ArtworkDownloader settings **************
            .TVUseAD = chkTVUseAD.Checked
            .TVSeasonLandscapeAD = chkTVSeasonLandscapeAD.Checked
            .TVShowCharacterArtAD = chkTVShowCharacterArtAD.Checked
            .TVShowClearArtAD = chkTVShowClearArtAD.Checked
            .TVShowClearLogoAD = chkTVShowClearLogoAD.Checked
            .TVShowLandscapeAD = chkTVShowLandscapeAD.Checked

            '************** XBMC TvTunes settings **************
            .TVShowThemeTvTunesEnable = chkTVShowThemeTvTunesEnabled.Checked
            .TVShowThemeTvTunesCustom = chkTVShowThemeTvTunesCustom.Checked
            .TVShowThemeTvTunesCustomPath = txtTVShowThemeTvTunesCustomPath.Text
            .TVShowThemeTvTunesShowPath = chkTVShowThemeTvTunesShowPath.Checked
            .TVShowThemeTvTunesSub = chkTVShowThemeTvTunesSub.Checked
            .TVShowThemeTvTunesSubDir = txtTVShowThemeTvTunesSubDir.Text

            '********* XBMC Extended Images settings ***********
            .TVUseExtended = chkTVUseExtended.Checked
            .TVSeasonLandscapeExtended = chkTVSeasonLandscapeExtended.Checked
            .TVShowCharacterArtExtended = chkTVShowCharacterArtExtended.Checked
            .TVShowClearArtExtended = chkTVShowClearArtExtended.Checked
            .TVShowClearLogoExtended = chkTVShowClearLogoExtended.Checked
            .TVShowKeyArtExtended = chkTVShowKeyArtExtended.Checked
            .TVShowLandscapeExtended = chkTVShowLandscapeExtended.Checked

            '****************** YAMJ settings ******************
            .TVUseYAMJ = chkTVUseYAMJ.Checked
            .TVEpisodeNFOYAMJ = chkTVEpisodeNFOYAMJ.Checked
            .TVEpisodePosterYAMJ = chkTVEpisodePosterYAMJ.Checked
            .TVSeasonBannerYAMJ = chkTVSeasonBannerYAMJ.Checked
            .TVSeasonFanartYAMJ = chkTVSeasonFanartYAMJ.Checked
            .TVSeasonPosterYAMJ = chkTVSeasonPosterYAMJ.Checked
            .TVShowBannerYAMJ = chkTVShowBannerYAMJ.Checked
            .TVShowFanartYAMJ = chkTVShowFanartYAMJ.Checked
            .TVShowNFOYAMJ = chkTVShowNFOYAMJ.Checked
            .TVShowPosterYAMJ = chkTVShowPosterYAMJ.Checked

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            .TVUseBoxee = chkTVUseBoxee.Checked
            .TVEpisodeNFOBoxee = chkTVEpisodeNFOBoxee.Checked
            .TVEpisodePosterBoxee = chkTVEpisodePosterBoxee.Checked
            .TVSeasonPosterBoxee = chkTVSeasonPosterBoxee.Checked
            .TVShowBannerBoxee = chkTVShowBannerBoxee.Checked
            .TVShowFanartBoxee = chkTVShowFanartBoxee.Checked
            .TVShowNFOBoxee = chkTVShowNFOBoxee.Checked
            .TVShowPosterBoxee = chkTVShowPosterBoxee.Checked

            '***************** Expert settings ******************
            .TVUseExpert = chkTVUseExpert.Checked

            '***************** Expert AllSeasons ****************
            .TVAllSeasonsBannerExpert = txtTVAllSeasonsBannerExpert.Text
            .TVAllSeasonsFanartExpert = txtTVAllSeasonsFanartExpert.Text
            .TVAllSeasonsLandscapeExpert = txtTVAllSeasonsLandscapeExpert.Text
            .TVAllSeasonsPosterExpert = txtTVAllSeasonsPosterExpert.Text

            '***************** Expert Episode *******************
            .TVEpisodeActorThumbsExpert = chkTVEpisodeActorthumbsExpert.Checked
            .TVEpisodeActorThumbsExtExpert = txtTVEpisodeActorThumbsExtExpert.Text
            .TVEpisodeFanartExpert = txtTVEpisodeFanartExpert.Text
            .TVEpisodeNFOExpert = txtTVEpisodeNFOExpert.Text
            .TVEpisodePosterExpert = txtTVEpisodePosterExpert.Text

            '***************** Expert Season ********************
            .TVSeasonBannerExpert = txtTVSeasonBannerExpert.Text
            .TVSeasonFanartExpert = txtTVSeasonFanartExpert.Text
            .TVSeasonLandscapeExpert = txtTVSeasonLandscapeExpert.Text
            .TVSeasonPosterExpert = txtTVSeasonPosterExpert.Text

            '***************** Expert Show **********************
            .TVShowActorThumbsExpert = chkTVShowActorthumbsExpert.Checked
            .TVShowActorThumbsExtExpert = txtTVShowActorThumbsExtExpert.Text
            .TVShowBannerExpert = txtTVShowBannerExpert.Text
            .TVShowCharacterArtExpert = txtTVShowCharacterArtExpert.Text
            .TVShowClearArtExpert = txtTVShowClearArtExpert.Text
            .TVShowClearLogoExpert = txtTVShowClearLogoExpert.Text
            .TVShowExtrafanartsExpert = chkTVShowExtrafanartsExpert.Checked
            .TVShowFanartExpert = txtTVShowFanartExpert.Text
            .TVShowLandscapeExpert = txtTVShowLandscapeExpert.Text
            .TVShowNFOExpert = txtTVShowNFOExpert.Text
            .TVShowPosterExpert = txtTVShowPosterExpert.Text
        End With

        For Each s As ModulesManager._externalScraperModuleClass_Data_Movie In ModulesManager.Instance.externalScrapersModules_Data_Movie
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Data_MovieSet In ModulesManager.Instance.externalScrapersModules_Data_MovieSet
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Data_TV In ModulesManager.Instance.externalScrapersModules_Data_TV
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In ModulesManager.Instance.externalScrapersModules_Image_Movie
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In ModulesManager.Instance.externalScrapersModules_Image_Movieset
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_TV In ModulesManager.Instance.externalScrapersModules_Image_TV
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In ModulesManager.Instance.externalScrapersModules_Theme_Movie
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In ModulesManager.Instance.externalScrapersModules_Trailer_Movie
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalGenericModules
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        ModulesManager.Instance.SaveSettings()
    End Sub

    Private Sub SetApplyButton(ByVal v As Boolean)
        If Not NoUpdate Then btnApply.Enabled = v
    End Sub

    Private Sub SetUp()

        'Actor Thumbs
        Dim strActorthumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        chkTVEpisodeActorthumbsExpert.Text = strActorthumbs
        chkTVShowActorthumbsExpert.Text = strActorthumbs
        lblTVImagesHeaderEpisodeActorthumbs.Text = strActorthumbs
        lblTVImagesHeaderTVShowActorthumbs.Text = strActorthumbs
        lblTVSourcesFilenamingKodiDefaultsActorthumbs.Text = strActorthumbs

        'Actors
        Dim strActors As String = Master.eLang.GetString(231, "Actors")
        lblTVScraperGlobalActors.Text = strActors

        'Add Episode Guest Stars to Actors list
        Dim strAddEPGuestStars As String = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        chkTVScraperEpisodeGuestStarsToActors.Text = strAddEPGuestStars

        'Add <displayseason> and <displayepisode> to special episodes
        Dim strAddDisplaySE As String = Master.eLang.GetString(976, "Add <displayseason> and <displayepisode> to special episodes")
        chkTVScraperUseDisplaySeasonEpisode.Text = strAddDisplaySE

        'Aired
        Dim strAired As String = Master.eLang.GetString(728, "Aired")
        lblTVScraperGlobalAired.Text = strAired

        'All Seasons
        Dim strAllSeasons As String = Master.eLang.GetString(1202, "All Seasons")
        lblTVImagesHeaderAllSeasons.Text = strAllSeasons
        tpTVSourcesFilenamingExpertAllSeasons.Text = strAllSeasons

        'Also Get Blank Images
        Dim strAlsoGetBlankImages As String = Master.eLang.GetString(1207, "Also Get Blank Images")
        chkMovieSetImagesGetBlankImages.Text = strAlsoGetBlankImages
        chkTVImagesGetBlankImages.Text = strAlsoGetBlankImages

        'Also Get English Images
        Dim strAlsoGetEnglishImages As String = Master.eLang.GetString(737, "Also Get English Images")
        chkMovieSetImagesGetEnglishImages.Text = strAlsoGetEnglishImages
        chkTVImagesGetEnglishImages.Text = strAlsoGetEnglishImages

        'Ask On Click Scrape
        Dim strAskOnClickScrape As String = Master.eLang.GetString(852, "Ask On Click Scrape")
        chkMovieClickScrapeAsk.Text = strAskOnClickScrape
        chkMovieSetClickScrapeAsk.Text = strAskOnClickScrape
        chkTVGeneralClickScrapeAsk.Text = strAskOnClickScrape

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        lblMovieSetImagesHeaderBanner.Text = strBanner
        lblMovieSetSourcesFilenamingExpertParentBanner.Text = strBanner
        lblMovieSetSourcesFilenamingExpertSingleBanner.Text = strBanner
        lblMovieSetSourcesFilenamingKodiExtendedBanner.Text = strBanner
        lblMovieSetSourcesFilenamingKodiMSAABanner.Text = strBanner
        lblTVImagesHeaderAllSeasonsBanner.Text = strBanner
        lblTVImagesHeaderSeasonBanner.Text = strBanner
        lblTVImagesHeaderTVShowBanner.Text = strBanner
        lblTVSourcesFilenamingExpertAllSeasonsBanner.Text = strBanner
        lblTVSourcesFilenamingExpertSeasonBanner.Text = strBanner
        lblTVSourcesFilenamingExpertShowBanner.Text = strBanner
        lblTVSourcesFilenamingBoxeeDefaultsBanner.Text = strBanner
        lblTVSourcesFilenamingKodiDefaultsBanner.Text = strBanner
        lblTVSourcesFilenamingNMTDefaultsBanner.Text = strBanner

        'Certifications
        Dim strCertifications As String = Master.eLang.GetString(56, "Certifications")
        gbTVScraperCertificationOpts.Text = strCertifications
        lblTVScraperGlobalCertifications.Text = strCertifications

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        lblTVImagesHeaderTVShowCharacterArt.Text = strCharacterArt
        lblTVShowCharacterArtExpert.Text = strCharacterArt
        lblTVSourcesFilenamingKodiADCharacterArt.Text = strCharacterArt
        lblTVSourcesFilenamingKodiExtendedCharacterArt.Text = strCharacterArt

        'Cleanup disabled fields
        Dim strCleanUpDisabledFileds As String = Master.eLang.GetString(125, "Cleanup disabled fields")
        chkTVScraperCleanFields.Text = strCleanUpDisabledFileds

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        lblMovieSetImagesHeaderClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingExpertParentClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingExpertSingleClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingKodiExtendedClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingKodiMSAAClearArt.Text = strClearArt
        lblTVImagesHeaderTVShowClearArt.Text = strClearArt
        lblTVSourcesFilenamingExpertClearArt.Text = strClearArt
        lblTVSourcesFileNamingKodiADClearArt.Text = strClearArt
        lblTVSourcesFileNamingKodiExtendedClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        lblMovieSetImagesHeaderClearLogo.Text = strClearLogo
        lblMovieSetSourcesClearLogoExpertParent.Text = strClearLogo
        lblMovieSetSourcesClearLogoExpertSingle.Text = strClearLogo
        lblMovieSetSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo
        lblMovieSetSourcesFilenamingKodiMSAAClearLogo.Text = strClearLogo
        lblTVImagesHeaderTVShowClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingExpertClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingKodiADClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo

        'Column
        Dim strColumn As String = Master.eLang.GetString(1331, "Column")
        colMovieGeneralMediaListSortingLabel.Text = strColumn
        colMovieSetGeneralMediaListSortingLabel.Text = strColumn
        colTVGeneralEpisodeListSortingLabel.Text = strColumn
        colTVGeneralSeasonListSortingLabel.Text = strColumn
        colTVGeneralShowListSortingLabel.Text = strColumn


        'Creators
        Dim strCreators As String = Master.eLang.GetString(744, "Creators")
        lblTVScraperGlobalCreators.Text = strCreators

        'Default Episode Ordering
        Dim strDefaultEpisodeOrdering As String = Master.eLang.GetString(797, "Default Episode Ordering")
        lblTVSourcesDefaultsOrdering.Text = String.Concat(strDefaultEpisodeOrdering, ":")

        'Default Language
        Dim strDefaultLanguage As String = Master.eLang.GetString(1166, "Default Language")
        lblTVSourcesDefaultsLanguage.Text = String.Concat(strDefaultLanguage, ":")

        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        btnGeneralVideoSourceMappingByRegexDefaults.Text = strDefaults
        gbTVSourcesFilenamingBoxeeDefaultsOpts.Text = strDefaults
        gbTVSourcesFilenamingNMTDefaultsOpts.Text = strDefaults
        gbTVSourcesFilenamingKodiDefaultsOpts.Text = strDefaults

        'Defaults for new Sources
        Dim strDefaultsForNewSources As String = Master.eLang.GetString(252, "Defaults for new Sources")
        gbTVSourcesDefaultsOpts.Text = strDefaultsForNewSources


        'Defaults by File Type
        Dim strDefaultsByFileType As String = Master.eLang.GetString(625, "Defaults by File Type")
        gbTVScraperDefFIExtOpts.Text = strDefaultsByFileType

        'Directors
        Dim strDirectors As String = Master.eLang.GetString(940, "Directors")
        lblTVScraperGlobalDirectors.Text = strDirectors

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        lblMovieSetImagesHeaderDiscArt.Text = strDiscArt
        lblMovieSetSourcesFilenamingExpertParentDiscArt.Text = strDiscArt
        lblMovieSetSourcesFilenamingExpertSingleDiscArt.Text = strDiscArt
        lblMovieSetSourcesFilenamingKodiExtendedDiscArt.Text = strDiscArt

        'Display best Audio Stream with the following Language
        Dim strDisplayLanguageBestAudio As String = String.Concat(Master.eLang.GetString(436, "Display best Audio Stream with the following Language"), ":")
        lblMovieLanguageOverlay.Text = strDisplayLanguageBestAudio
        lblTVLanguageOverlay.Text = strDisplayLanguageBestAudio

        'Display ""Select Images"" dialog while single scraping
        Dim strDisplaySelectImagesDialog As String = Master.eLang.GetString(499, "Display ""Select Images"" dialog while single scraping")
        chkMovieSetImagesDisplayImageSelect.Text = strDisplaySelectImagesDialog
        chkTVImagesDisplayImageSelect.Text = strDisplaySelectImagesDialog

        'Do not save URLs to NFO"
        Dim strNotSaveURLToNfo As String = Master.eLang.GetString(498, "Do not save URLs to NFO")
        chkTVImagesNotSaveURLToNfo.Text = strNotSaveURLToNfo

        'Duration Format
        Dim strDurationFormat As String = Master.eLang.GetString(515, "Duration Format")
        gbTVScraperDurationFormatOpts.Text = strDurationFormat

        'Duration Runtime Format
        Dim strDurationRuntimeFormat As String = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
        lblTVScraperDurationRuntimeFormat.Text = strDurationRuntimeFormat

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        lblMovieSetSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblMovieSetSourcesFilenamingKodiMSAAEnabled.Text = strEnabled
        lblTVSourcesFilenamingBoxeeDefaultsEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiADEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiDefaultsEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblTVSourcesFilenamingNMTDefaultsEnabled.Text = strEnabled
        chkMovieSetUseExpert.Text = strEnabled
        chkTVShowThemeTvTunesEnabled.Text = strEnabled
        chkTVUseExpert.Text = strEnabled

        'Enabled Click Scrape
        Dim strEnabledClickScrape As String = Master.eLang.GetString(849, "Enable Click Scrape")
        chkMovieClickScrape.Text = strEnabledClickScrape
        chkMovieSetClickScrape.Text = strEnabledClickScrape
        chkTVGeneralClickScrape.Text = strEnabledClickScrape

        'Enable Image Caching
        Dim strEnableImageCaching As String = Master.eLang.GetString(249, "Enable Image Caching")
        chkMovieSetImagesCacheEnabled.Text = strEnableImageCaching
        chkTVImagesCacheEnabled.Text = strEnableImageCaching

        'Enable Theme Support
        Dim strEnableThemeSupport As String = Master.eLang.GetString(1082, "Enable Theme Support")

        'Episode
        Dim strEpisode As String = Master.eLang.GetString(727, "Episode")
        lblTVImagesHeaderEpisode.Text = strEpisode
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeEpisode.Text = strEpisode
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixEpisode.Text = strEpisode
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJEpisode.Text = strEpisode
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJEpisode.Text = strEpisode
        tpTVSourcesFilenamingExpertEpisode.Text = strEpisode

        'Episode #
        Dim strEpisodeNR As String = Master.eLang.GetString(660, "Episode #")

        'Episode List Sorting
        Dim strEpisodeListSorting As String = Master.eLang.GetString(494, "Episode List Sorting")
        gbTVGeneralEpisodeListSorting.Text = strEpisodeListSorting

        'Episode Guide URL
        Dim strEpisodeGuideURL As String = Master.eLang.GetString(723, "Episode Guide URL")
        lblTVScraperGlobalEpisodeGuideURL.Text = strEpisodeGuideURL

        'Episodes
        Dim strEpisodes As String = Master.eLang.GetString(682, "Episodes")
        lblTVScraperGlobalHeaderEpisodes.Text = strEpisodes

        'Exclude
        Dim strExclude As String = Master.eLang.GetString(264, "Exclude")
        colTVSourcesExclude.Text = strExclude

        'Expert
        Dim strExpert As String = Master.eLang.GetString(439, "Expert")
        tpMovieSetSourcesFilenamingExpert.Text = strExpert
        tpTVSourcesFilenamingExpert.Text = strExpert

        'Expert Settings
        Dim strExpertSettings As String = Master.eLang.GetString(1181, "Expert Settings")
        gbMovieSetSourcesFilenamingExpertOpts.Text = strExpertSettings
        gbTVSourcesFilenamingExpertOpts.Text = strExpertSettings

        'Extended Images
        Dim strExtendedImages As String = Master.eLang.GetString(822, "Extended Images")
        gbMovieSetSourcesFilenamingKodiExtendedOpts.Text = strExtendedImages
        gbTVSourcesFilenamingKodiExtendedOpts.Text = strExtendedImages

        'Extract
        Dim strExtract As String = Master.eLang.GetString(538, "Extract")
        lblTVImagesHeaderVideoExtraction.Text = String.Concat(strExtract, " **")

        'Extract images from video file
        Dim strExtractImagesFromVideoFile As String = Master.eLang.GetString(666, "Extract images from video file")
        lblTVImagesHintVideoExtraction.Text = String.Concat("** ", strExtractImagesFromVideoFile)

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        chkTVShowExtrafanartsExpert.Text = strExtrafanarts
        lblTVImagesHeaderTVShowExtrafanarts.Text = strExtrafanarts
        lblTVSourcesFilenamingKodiDefaultsExtrafanarts.Text = strExtrafanarts

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        lblMovieSetImagesHeaderFanart.Text = strFanart
        lblMovieSetSourcesFilenamingExpertParentFanart.Text = strFanart
        lblMovieSetSourcesFilenamingExpertSingleFanart.Text = strFanart
        lblMovieSetSourcesFilenamingKodiExtendedFanart.Text = strFanart
        lblMovieSetSourcesFilenamingKodiMSAAFanart.Text = strFanart
        lblTVImagesHeaderAllSeasonsFanart.Text = strFanart
        lblTVImagesHeaderEpisodeFanart.Text = strFanart
        lblTVImagesHeaderSeasonFanart.Text = strFanart
        lblTVImagesHeaderTVShowFanart.Text = strFanart
        lblTVSourcesFilenamingExpertAllSeasonsFanart.Text = strFanart
        lblTVSourcesFilenamingExpertEpisodeFanart.Text = strFanart
        lblTVSourcesFilenamingExpertSeasonFanart.Text = strFanart
        lblTVSourcesFilenamingExpertShowFanart.Text = strFanart
        lblTVSourcesFilenamingBoxeeDefaultsFanart.Text = strFanart
        lblTVSourcesFilenamingKodiDefaultsFanart.Text = strFanart
        lblTVSourcesFilenamingNMTDefaultsFanart.Text = strFanart

        'File Extension
        Dim strFileExtension As String = Master.eLang.GetString(775, "File Extension")
        colGeneralVideoSourceMappingByExtensionFileExtension.HeaderText = strFileExtension

        'File Naming
        Dim strFilenaming As String = Master.eLang.GetString(471, "File Naming")
        gbMovieSetSourcesFilenamingOpts.Text = strFilenaming
        gbTVSourcesFilenamingOpts.Text = strFilenaming

        'File Type
        Dim strFileType As String = String.Concat(Master.eLang.GetString(626, "File Type"), ":")
        lblTVScraperDefFIExt.Text = strFileType

        'Force Language
        Dim strForceLanguage As String = Master.eLang.GetString(1034, "Force Language")
        chkMovieSetImagesForceLanguage.Text = strForceLanguage
        chkTVImagesForceLanguage.Text = strForceLanguage

        'Genres
        Dim strGenres As String = Master.eLang.GetString(725, "Genres")
        lblTVScraperGlobalGenres.Text = strGenres


        'Hide
        Dim strHide As String = Master.eLang.GetString(465, "Hide")
        colMovieGeneralMediaListSortingHide.Text = strHide
        colMovieSetGeneralMediaListSortingHide.Text = strHide
        colTVGeneralEpisodeListSortingHide.Text = strHide
        colTVGeneralSeasonListSortingHide.Text = strHide
        colTVGeneralShowListSortingHide.Text = strHide

        'Images
        Dim strImages As String = Master.eLang.GetString(497, "Images")
        gbMovieSetImagesOpts.Text = strImages
        gbTVImagesOpts.Text = strImages

        'Image Types
        Dim strImageTypes As String = Master.eLang.GetString(304, "Image Types")
        gbMovieSetImagesImageTypesOpts.Text = strImageTypes
        gbTVImagesImageTypesOpts.Text = strImageTypes

        'Keep existing
        Dim strKeepExisting As String = Master.eLang.GetString(971, "Keep existing")
        chkMovieThemeKeepExisting.Text = strKeepExisting
        chkMovieTrailerKeepExisting.Text = strKeepExisting
        lblMovieSetImagesHeaderKeepExisting.Text = strKeepExisting
        lblTVImagesHeaderKeepExisting.Text = strKeepExisting

        'KeyArt
        Dim strKeyArt As String = Master.eLang.GetString(296, "KeyArt")
        lblMovieSetImagesHeaderKeyArt.Text = strKeyArt
        lblMovieSetSourcesFilenamingKodiExtendedKeyArt.Text = strKeyArt
        lblTVImagesHeaderTVShowKeyArt.Text = strKeyArt
        lblTVSourcesFilenamingKodiExtendedKeyArt.Text = strKeyArt

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        lblMovieSetImagesHeaderLandscape.Text = strLandscape
        lblMovieSetLandscapeExpertParent.Text = strLandscape
        lblMovieSetLandscapeExpertSingle.Text = strLandscape
        lblMovieSetSourcesFilenamingKodiExtendedLandscape.Text = strLandscape
        lblMovieSetSourcesFilenamingKodiMSAALandscape.Text = strLandscape
        lblTVImagesHeaderAllSeasonsLandscape.Text = strLandscape
        lblTVImagesHeaderSeasonLandscape.Text = strLandscape
        lblTVImagesHeaderTVShowLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertAllSeasonsLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertSeasonLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertShowLandscape.Text = strLandscape

        'Language
        Dim strLanguage As String = Master.eLang.GetString(610, "Language")
        colTVSourcesLanguage.Text = strLanguage

        'Language (Audio)
        Dim strLanguageAudio As String = Master.eLang.GetString(431, "Language (Audio)")
        lblTVScraperGlobalLanguageA.Text = strLanguageAudio

        'Language (Video)
        Dim strLanguageVideo As String = Master.eLang.GetString(435, "Language (Video)")
        lblTVScraperGlobalLanguageV.Text = strLanguageVideo

        'Limit
        Dim strLimit As String = Master.eLang.GetString(578, "Limit")
        lblTVImagesHeaderLimit.Text = strLimit
        lblTVScraperGlobalHeaderEpisodesLimit.Text = strLimit
        lblTVScraperGlobalHeaderSeasonsLimit.Text = strLimit
        lblTVScraperGlobalHeaderShowsLimit.Text = strLimit

        'Lock
        Dim strLock As String = Master.eLang.GetString(24, "Lock")
        lblMovieSetScraperGlobalHeaderLock.Text = strLock
        lblTVScraperGlobalHeaderEpisodesLock.Text = strLock
        lblTVScraperGlobalHeaderSeasonsLock.Text = strLock
        lblTVScraperGlobalHeaderShowsLock.Text = strLock

        'Main Window
        Dim strMainWindow As String = Master.eLang.GetString(1152, "Main Window")
        gbGeneralMainWindowOpts.Text = strMainWindow
        gbMovieGeneralMainWindowOpts.Text = strMainWindow
        gbTVGeneralMainWindowOpts.Text = strMainWindow

        'Mapping by File Extension
        Dim strByFileExtension As String = Master.eLang.GetString(763, "Mapping by File Extension")
        gbGeneralVideoSourceMappingByExtension.Text = strByFileExtension

        'Mapping by Regex
        Dim strByRegex As String = Master.eLang.GetString(764, "Mapping by Regex")
        gbGeneralVideoSourceMappingByRegex.Text = strByRegex

        'Max Height
        Dim strMaxHeight As String = Master.eLang.GetString(480, "Max Height")
        lblMovieSetImagesHeaderMaxHeight.Text = strMaxHeight
        lblTVImagesHeaderMaxHeight.Text = strMaxHeight

        'Max Width
        Dim strMaxWidth As String = Master.eLang.GetString(479, "Max Width")
        lblMovieSetImagesHeaderMaxWidth.Text = strMaxWidth
        lblTVImagesHeaderMaxWidth.Text = strMaxWidth

        'Meta Data
        Dim strMetaData As String = Master.eLang.GetString(59, "Meta Data")
        gbTVScraperMetaDataOpts.Text = strMetaData

        'Miscellaneous
        Dim strMiscellaneous As String = Master.eLang.GetString(429, "Miscellaneous")
        gbGeneralMiscOpts.Text = strMiscellaneous
        gbMovieGeneralMiscOpts.Text = strMiscellaneous
        gbMovieSetGeneralMiscOpts.Text = strMiscellaneous
        gbMovieSetSourcesMiscOpts.Text = strMiscellaneous
        gbTVGeneralMiscOpts.Text = strMiscellaneous
        gbTVScraperMiscOpts.Text = strMiscellaneous
        gbTVSourcesMiscOpts.Text = strMiscellaneous

        'Missing
        Dim strMissing As String = Master.eLang.GetString(582, "Missing")

        'MPAA
        Dim strMPAA As String = Master.eLang.GetString(401, "MPAA")
        lblTVScraperGlobalMPAA.Text = strMPAA

        'MPAA value if no rating is available
        Dim strMPAANotRated As String = Master.eLang.GetString(832, "MPAA value if no rating is available")
        lblTVScraperShowMPAANotRated.Text = strMPAANotRated

        'Movie List Sorting
        Dim strMovieListSorting As String = Master.eLang.GetString(490, "Movie List Sorting")
        gbMovieGeneralMediaListSorting.Text = strMovieListSorting

        'MovieSet List Sorting
        Dim strMovieSetListSorting As String = Master.eLang.GetString(491, "MovieSet List Sorting")
        gbMovieSetGeneralMediaListSorting.Text = strMovieSetListSorting

        'Name
        Dim strName As String = Master.eLang.GetString(232, "Name")
        colTVSourcesName.Text = strName

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        lblMovieSetSourcesFilenamingExpertParentNFO.Text = strNFO
        lblMovieSetSourcesFilenamingExpertSingleNFO.Text = strNFO
        lblTVSourcesFilenamingExpertEpisodeNFO.Text = strNFO
        lblTVSourcesFilenamingExpertShowNFO.Text = strNFO
        lblTVSourcesFilenamingKodiDefaultsNFO.Text = strNFO

        'Only
        Dim strOnly As String = Master.eLang.GetString(145, "Only")
        lblMovieSetImagesHeaderPrefSizeOnly.Text = strOnly
        lblTVImagesHeaderPrefSizeOnly.Text = strOnly

        'Only Get Images for the Media Language
        Dim strOnlyImgMediaLang As String = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        chkMovieSetImagesMediaLanguageOnly.Text = strOnlyImgMediaLang
        chkTVImagesMediaLanguageOnly.Text = strOnlyImgMediaLang

        'Only if no MPAA is found
        Dim strOnlyIfNoMPAA As String = Master.eLang.GetString(1293, "Only if no MPAA is found")
        chkTVScraperShowCertForMPAAFallback.Text = strOnlyIfNoMPAA

        'Only Save the Value to NFO
        Dim strOnlySaveValueToNFO As String = Master.eLang.GetString(835, "Only Save the Value to NFO")
        chkTVScraperShowCertOnlyValue.Text = strOnlySaveValueToNFO

        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        gbTVSourcesFilenamingExpertEpisodeImagesOpts.Text = strOptionalImages
        gbTVSourcesFilenamingExpertShowImagesOpts.Text = strOptionalImages


        'Ordering
        Dim strOrdering As String = Master.eLang.GetString(1167, "Ordering")
        colTVSourcesOrdering.Text = strOrdering

        'Part of a MovieSet
        Dim strPartOfAMovieSet As String = Master.eLang.GetString(1295, "Part of a MovieSet")

        'Path
        Dim strPath As String = Master.eLang.GetString(410, "Path")
        lblMovieSetPathExpertSingle.Text = strPath
        lblMovieSetSourcesFilenamingKodiExtendedPath.Text = strPath
        lblMovieSetSourcesFilenamingKodiMSAAPath.Text = strPath
        colTVSourcesPath.Text = strPath

        'Plot
        Dim strPlot As String = Master.eLang.GetString(65, "Plot")
        lblMovieSetScraperGlobalPlot.Text = strPlot
        lblTVScraperGlobalPlot.Text = strPlot

        'Plot Outline
        Dim strPlotOutline As String = Master.eLang.GetString(64, "Plot Outline")

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        lblMovieSetImagesHeaderPoster.Text = strPoster
        lblMovieSetPosterExpertParent.Text = strPoster
        lblMovieSetPosterExpertSingle.Text = strPoster
        lblMovieSetSourcesFilenamingKodiExtendedPoster.Text = strPoster
        lblMovieSetSourcesFilenamingKodiMSAAPoster.Text = strPoster
        lblTVAllSeasonsPosterExpert.Text = strPoster
        lblTVEpisodePosterExpert.Text = strPoster
        lblTVImagesHeaderAllSeasonsPoster.Text = strPoster
        lblTVImagesHeaderEpisodePoster.Text = strPoster
        lblTVImagesHeaderSeasonPoster.Text = strPoster
        lblTVImagesHeaderTVShowPoster.Text = strPoster
        lblTVSeasonPosterExpert.Text = strPoster
        lblTVShowPosterExpert.Text = strPoster
        lblTVSourcesFilenamingBoxeeDefaultsPoster.Text = strPoster
        lblTVSourcesFilenamingKodiDefaultsPoster.Text = strPoster
        lblTVSourcesFilenamingNMTDefaultsPoster.Text = strPoster

        'Prefer extracted images
        Dim strPreferExtractedImages As String = Master.eLang.GetString(667, "Prefer extracted images")
        lblTVImagesHintVideoExtractionPref.Text = String.Concat("*** ", strPreferExtractedImages)

        'Preferred Language
        Dim strPreferredLanguage As String = Master.eLang.GetString(741, "Preferred Language")
        gbMovieSetImagesLanguageOpts.Text = strPreferredLanguage
        gbTVImagesLanguageOpts.Text = strPreferredLanguage

        'Preferred Size
        Dim strPreferredSize As String = Master.eLang.GetString(482, "Preferred Size")
        lblMovieSetImagesHeaderPrefSize.Text = strPreferredSize
        lblTVImagesHeaderPrefSize.Text = strPreferredSize

        'Preferred Type
        Dim strPreferredType As String = Master.eLang.GetString(730, "Preferred Type")
        lblTVImagesHeaderPrefType.Text = strPreferredType

        'Premiered
        Dim strPremiered As String = Master.eLang.GetString(724, "Premiered")
        lblTVScraperGlobalPremiered.Text = strPremiered

        'Preslect
        Dim strPreselect As String = Master.eLang.GetString(308, "Preselect")
        lblTVImagesHeaderPreselect.Text = String.Concat(strPreselect, " *")

        'Preselect images in ""Select Images"" dialog
        Dim strPreselectSelectImagesDialog As String = Master.eLang.GetString(1023, "Preselect images in ""Select Images"" dialog")
        lblTVImagesHintPreselect.Text = String.Concat("* ", strPreselectSelectImagesDialog)

        'Rating
        Dim strRating As String = Master.eLang.GetString(400, "Rating")
        lblTVScraperGlobalRating.Text = strRating

        'Recusive
        Dim strRecursive = Master.eLang.GetString(411, "Recursive")

        'Regex
        Dim strRegex As String = Master.eLang.GetString(699, "Regex")
        colGeneralVideoSourceMappingByRegexRegex.HeaderText = strRegex

        'Resize
        Dim stryResize As String = Master.eLang.GetString(481, "Resize")
        lblMovieSetImagesHeaderBanner.Text = stryResize
        lblTVImagesHeaderResize.Text = stryResize

        'Runtime
        Dim strRuntime As String = Master.eLang.GetString(238, "Runtime")
        lblTVScraperGlobalRuntime.Text = strRuntime

        'Save extended Collection information to NFO (Kodi 16.0 "Jarvis" and newer)
        Dim strSaveExtended As String = Master.eLang.GetString(1075, "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)")

        'Scrape Only Actors With Images
        Dim strScrapeOnlyActorsWithImages As String = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        chkTVScraperCastWithImg.Text = strScrapeOnlyActorsWithImages

        'Scraper Fields - Global
        Dim strScraperGlobal As String = Master.eLang.GetString(577, "Scraper Fields - Global")
        gbMovieSetScraperGlobalOpts.Text = strScraperGlobal
        gbTVScraperGlobalOpts.Text = strScraperGlobal

        'Season
        Dim strSeason As String = Master.eLang.GetString(650, "Season")
        lblTVImagesHeaderSeason.Text = strSeason
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeSeason.Text = strSeason
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJSeason.Text = strSeason
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixSeason.Text = strSeason
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJSeason.Text = strSeason
        tpTVSourcesFilenamingExpertSeason.Text = strSeason

        'Season #
        Dim strSeasonNR As String = Master.eLang.GetString(659, "Season #")

        'Season Landscape
        Dim strSeasonLandscape As String = Master.eLang.GetString(1018, "Season Landscape")
        lblTVSourcesFilenamingKodiADSeasonLandscape.Text = strSeasonLandscape
        lblTVSourcesFilenamingKodiExtendedSeasonLandscape.Text = strSeasonLandscape

        'Season List Sorting
        Dim strSeasonListSorting As String = Master.eLang.GetString(493, "Season List Sorting")
        gbTVGeneralSeasonListSortingOpts.Text = strSeasonListSorting

        'Seasons
        Dim strSeasons As String = Master.eLang.GetString(681, "Seasons")
        lblTVScraperGlobalHeaderSeasons.Text = strSeasons

        'Show List Sorting
        Dim strShowListSorting As String = Master.eLang.GetString(492, "Show List Sorting")
        gbTVGeneralShowListSortingOpts.Text = strShowListSorting

        'Shows
        Dim strShows As String = Master.eLang.GetString(680, "Shows")
        lblTVScraperGlobalHeaderShows.Text = strShows

        'Single Video
        Dim strSingleVideo As String = Master.eLang.GetString(413, "Single Video")

        'Sort Tokens to Ignore
        Dim strSortTokens As String = Master.eLang.GetString(463, "Sort Tokens to Ignore")
        gbMovieGeneralMediaListSortTokensOpts.Text = strSortTokens
        gbMovieSetGeneralMediaListSortTokensOpts.Text = strSortTokens
        gbTVGeneralMediaListSortTokensOpts.Text = strSortTokens

        'Sorting
        Dim strSorting As String = Master.eLang.GetString(895, "Sorting")
        colTVSourcesSorting.Text = strSorting

        'Status
        Dim strStatus As String = Master.eLang.GetString(215, "Status")
        lblTVScraperGlobalStatus.Text = strStatus

        'Store themes in movie directory
        Dim strStoreThemeInMovieDirectory As String = Master.eLang.GetString(1258, "Store themes in movie directory")

        'Store themes in custom path
        Dim strStoreThemeInCustomPath As String = Master.eLang.GetString(1259, "Store themes in a custom path")
        chkTVShowThemeTvTunesCustom.Text = strStoreThemeInCustomPath

        'Store themes in sub directories
        Dim strStoreThemeInSubDirectory As String = Master.eLang.GetString(1260, "Store themes in sub directories")
        chkTVShowThemeTvTunesSub.Text = strStoreThemeInSubDirectory

        'Store themes in tv show directory
        Dim strStoreThemeInShowDirectory As String = Master.eLang.GetString(1265, "Store themes in tv show directory")
        chkTVShowThemeTvTunesShowPath.Text = strStoreThemeInShowDirectory

        'Studios
        Dim strStudio As String = Master.eLang.GetString(226, "Studios")
        lblTVScraperGlobalStudios.Text = strStudio

        'Subtitles
        Dim strSubtitles As String = Master.eLang.GetString(152, "Subtitles")

        'Tagline
        Dim strTagline As String = Master.eLang.GetString(397, "Tagline")

        'Theme
        Dim strTheme As String = Master.eLang.GetString(1118, "Theme")

        'Themes
        Dim strThemes As String = Master.eLang.GetString(1285, "Themes")
        gbMovieThemeOpts.Text = strThemes

        'Title
        Dim strTitle As String = Master.eLang.GetString(21, "Title")
        lblMovieSetScraperGlobalTitle.Text = strTitle
        lblTVScraperGlobalTitle.Text = strTitle

        'Top250
        Dim strTop250 As String = Master.eLang.GetString(591, "Top 250")


        'TV Show
        Dim strTVShow As String = Master.eLang.GetString(700, "TV Show")
        lblTVImagesHeaderTVShow.Text = strTVShow
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeTVShow.Text = strTVShow
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixTVShow.Text = strTVShow
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJTVShow.Text = strTVShow
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJTVShow.Text = strTVShow
        tpTVSourcesFilenamingExpertTVShow.Text = strTVShow

        'TV Show Landscape
        Dim strTVShowLandscape As String = Master.eLang.GetString(1010, "TV Show Landscape")
        lblTVSourcesFilenamingKodiADTVShowLandscape.Text = strTVShowLandscape
        lblTVSourcesFilenamingKodiExtendedTVShowLandscape.Text = strTVShowLandscape

        'Use
        Dim strUse As String = Master.eLang.GetString(872, "Use")

        'Use Certification for MPAA
        Dim strUseCertForMPAA As String = Master.eLang.GetString(511, "Use Certification for MPAA")
        chkTVScraperShowCertForMPAA.Text = strUseCertForMPAA

        'Use Original Title as Title
        Dim strUseOriginalTitleAsTitle As String = Master.eLang.GetString(240, "Use Original Title as Title")
        chkTVScraperShowOriginalTitleAsTitle.Text = strUseOriginalTitleAsTitle

        'VideoSource
        Dim strVideoSource As String = Master.eLang.GetString(824, "Video Source")
        colGeneralVideoSourceMappingByExtensionVideoSource.HeaderText = strVideoSource
        colGeneralVideoSourceMappingByRegexVideoSource.HeaderText = strVideoSource

        'Watched
        Dim strWatched As String = Master.eLang.GetString(981, "Watched")

        'Use Folder Name
        Dim strUseFolderName As String = Master.eLang.GetString(412, "Use Folder Name")

        'User Rating
        Dim strUserRating As String = Master.eLang.GetString(1467, "User Rating")
        lblTVScraperGlobalUserRating.Text = strUserRating

        'Writers
        Dim strWriters As String = Master.eLang.GetString(394, "Writers")
        lblTVScraperGlobalCredits.Text = strWriters

        'Year
        Dim strYear As String = Master.eLang.GetString(278, "Year")

        Text = Master.eLang.GetString(420, "Settings")
        btnApply.Text = Master.eLang.GetString(276, "Apply")
        btnCancel.Text = Master.eLang.Cancel
        btnGeneralDigitGrpSymbolSettings.Text = Master.eLang.GetString(420, "Settings")
        btnMovieSetScraperTitleRenamerAdd.Text = Master.eLang.GetString(28, "Add")
        btnMovieSetScraperTitleRenamerRemove.Text = Master.eLang.GetString(30, "Remove")
        btnOK.Text = Master.eLang.OK
        btnRemTVSource.Text = Master.eLang.GetString(30, "Remove")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(690, "Edit Regex")
        btnTVSourcesRegexTVShowMatchingClear.Text = Master.eLang.GetString(123, "Clear")
        btnTVSourcesRegexTVShowMatchingEdit.Text = Master.eLang.GetString(690, "Edit Regex")
        btnTVSourcesRegexTVShowMatchingRemove.Text = Master.eLang.GetString(30, "Remove")
        btnTVSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        chkGeneralCheckUpdates.Text = Master.eLang.GetString(432, "Check for Updates")
        chkGeneralDateAddedIgnoreNFO.Text = Master.eLang.GetString(1209, "Ignore <dateadded> from NFO")
        chkGeneralDigitGrpSymbolVotes.Text = Master.eLang.GetString(1387, "Use digit grouping symbol for Votes count")
        chkGeneralDoubleClickScrape.Text = Master.eLang.GetString(1198, "Enable Image Scrape On Double Right Click")
        chkGeneralDisplayBanner.Text = Master.eLang.GetString(1146, "Display Banner")
        chkGeneralDisplayCharacterArt.Text = Master.eLang.GetString(1147, "Display CharacterArt")
        chkGeneralDisplayClearArt.Text = Master.eLang.GetString(1148, "Display ClearArt")
        chkGeneralDisplayClearLogo.Text = Master.eLang.GetString(1149, "Display ClearLogo")
        chkGeneralDisplayDiscArt.Text = Master.eLang.GetString(1150, "Display DiscArt")
        chkGeneralDisplayFanart.Text = Master.eLang.GetString(455, "Display Fanart")
        chkGeneralDisplayFanartSmall.Text = Master.eLang.GetString(967, "Display Small Fanart")
        chkGeneralDisplayLandscape.Text = Master.eLang.GetString(1151, "Display Landscape")
        chkGeneralDisplayPoster.Text = Master.eLang.GetString(456, "Display Poster")
        chkGeneralImagesGlassOverlay.Text = Master.eLang.GetString(966, "Enable Images Glass Overlay")
        chkGeneralImageFilter.Text = Master.eLang.GetString(1459, "Activate ImageFilter to avoid duplicate images")
        chkGeneralImageFilterAutoscraper.Text = Master.eLang.GetString(1457, "Autoscraper")
        chkGeneralImageFilterFanart.Text = Master.eLang.GetString(149, "Fanart")
        chkGeneralImageFilterImagedialog.Text = Master.eLang.GetString(1458, "Imagedialog")
        chkGeneralImageFilterPoster.Text = Master.eLang.GetString(148, "Poster")
        chkGeneralOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        chkGeneralDisplayGenresText.Text = Master.eLang.GetString(453, "Always Display Genre Text")
        chkGeneralDisplayLangFlags.Text = Master.eLang.GetString(489, "Display Language Flags")
        chkGeneralDisplayImgDims.Text = Master.eLang.GetString(457, "Display Image Dimensions")
        chkGeneralDisplayImgNames.Text = Master.eLang.GetString(1255, "Display Image Names")
        chkGeneralSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        chkMovieDisplayYear.Text = Master.eLang.GetString(464, "Display Year in List Title")
        chkMovieGeneralMarkNew.Text = Master.eLang.GetString(459, "Mark New Movies")
        chkMovieLevTolerance.Text = Master.eLang.GetString(462, "Check Title Match Confidence")
        chkMovieProperCase.Text = Master.eLang.GetString(452, "Convert Names to Proper Case")
        chkMovieSetCleanFiles.Text = Master.eLang.GetString(1276, "Remove Images and NFOs with MovieSets")
        chkMovieSetGeneralMarkNew.Text = Master.eLang.GetString(1301, "Mark New MovieSets")
        chkProxyCredsEnable.Text = Master.eLang.GetString(677, "Enable Credentials")
        chkProxyEnable.Text = Master.eLang.GetString(673, "Enable Proxy")
        chkTVDisplayMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        chkTVDisplayStatus.Text = Master.eLang.GetString(126, "Display Status in List Title")
        chkTVEpisodeNoFilter.Text = Master.eLang.GetString(734, "Build Episode Title Instead of Filtering")
        chkTVGeneralMarkNewEpisodes.Text = Master.eLang.GetString(621, "Mark New Episodes")
        chkTVGeneralMarkNewShows.Text = Master.eLang.GetString(549, "Mark New Shows")
        chkTVScraperEpisodeGuestStarsToActors.Text = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        chkTVScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        chkTVScraperUseSRuntimeForEp.Text = Master.eLang.GetString(1262, "Use Show Runtime for Episodes if no Episode Runtime can be found")
        dgvMovieSetScraperTitleRenamer.Columns(0).HeaderText = Master.eLang.GetString(1277, "From")
        dgvMovieSetScraperTitleRenamer.Columns(1).HeaderText = Master.eLang.GetString(1278, "To")
        gbFileSystemExcludedPaths.Text = Master.eLang.GetString(1273, "Excluded Paths")
        gbFileSystemNoStackExts.Text = Master.eLang.GetString(530, "No Stack Extensions")
        gbFileSystemValidVideoExts.Text = Master.eLang.GetString(534, "Valid Video Extensions")
        gbFileSystemValidSubtitlesExts.Text = Master.eLang.GetString(1284, "Valid Subtitles Extensions")
        gbFileSystemValidThemeExts.Text = Master.eLang.GetString(1081, "Valid Theme Extensions")
        gbGeneralDateAdded.Text = Master.eLang.GetString(792, "Adding Date")
        gbGeneralInterface.Text = Master.eLang.GetString(795, "Interface")
        gbGeneralVirtualDrive.Text = Master.eLang.GetString(1261, "Configuration ISO Filescanning")
        gbMovieGeneralCustomMarker.Text = Master.eLang.GetString(1190, "Custom Marker")
        gbMovieGeneralFiltersOpts.Text = Master.eLang.GetString(451, "Folder/File Name Filters")
        gbMovieGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        gbMovieSetScraperTitleRenamerOpts.Text = Master.eLang.GetString(1279, "Title Renamer")
        gbProxyCredsOpts.Text = Master.eLang.GetString(676, "Credentials")
        gbProxyOpts.Text = Master.eLang.GetString(672, "Proxy")
        gbTVEpisodeFilterOpts.Text = Master.eLang.GetString(671, "Episode Folder/File Name Filters")
        gbTVGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        gbTVScraperDurationFormatOpts.Text = Master.eLang.GetString(515, "Duration Format")
        gbTVScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields")
        gbTVShowFilterOpts.Text = Master.eLang.GetString(670, "Show Folder/File Name Filters")
        gbTVSourcesRegexTVShowMatching.Text = Master.eLang.GetString(691, "Show Match Regex")
        lblGeneralImageFilterPosterMatchRate.Text = Master.eLang.GetString(148, "Poster") & " " & Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblGeneralImageFilterFanartMatchRate.Text = Master.eLang.GetString(149, "Fanart") & " " & Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblGeneralOverwriteNfo.Text = Master.eLang.GetString(434, "(If unchecked, non-conforming nfos will be renamed to <filename>.info)")
        lblGeneralIntLang.Text = Master.eLang.GetString(430, "Interface Language:")
        lblGeneralTheme.Text = String.Concat(Master.eLang.GetString(620, "Theme"), ":")
        lblGeneralVirtualDriveLetter.Text = Master.eLang.GetString(989, "Driveletter")
        lblGeneralVirtualDrivePath.Text = Master.eLang.GetString(990, "Path to VCDMount.exe (Virtual CloneDrive)")
        lblMovieGeneralCustomMarker1.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #1")
        lblMovieGeneralCustomMarker2.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #2")
        lblMovieGeneralCustomMarker3.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #3")
        lblMovieGeneralCustomMarker4.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #4")
        lblMovieLevTolerance.Text = Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblMovieTrailerDefaultSearch.Text = Master.eLang.GetString(1172, "Default Search Parameter:")
        lblMovieTrailerMinQual.Text = Master.eLang.GetString(1027, "Minimum Quality:")
        lblMovieTrailerPrefQual.Text = Master.eLang.GetString(800, "Preferred Quality:")
        lblProxyDomain.Text = Master.eLang.GetString(678, "Domain:")
        lblProxyPassword.Text = Master.eLang.GetString(426, "Password:")
        lblProxyPort.Text = Master.eLang.GetString(675, "Proxy Port:")
        lblProxyURI.Text = Master.eLang.GetString(674, "Proxy URL:")
        lblProxyUsername.Text = Master.eLang.GetString(425, "Username:")
        lblSettingsTopDetails.Text = Master.eLang.GetString(518, "Configure Ember's appearance and operation.")
        lblTVScraperGlobalGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        lblTVSourcesRegexTVShowMatchingByDate.Text = Master.eLang.GetString(698, "by Date")
        lblTVSourcesRegexTVShowMatchingRegex.Text = Master.eLang.GetString(699, "Regex")
        lblTVSourcesRegexTVShowMatchingDefaultSeason.Text = Master.eLang.GetString(695, "Default Season")
        tpMovieSetFilenamingExpertParent.Text = Master.eLang.GetString(880, "Parent Folder")
        tpMovieSetFilenamingExpertSingle.Text = Master.eLang.GetString(879, "Single Folder")
        tpTVSourcesGeneral.Text = Master.eLang.GetString(38, "General")
        tpTVSourcesRegex.Text = Master.eLang.GetString(699, "Regex")

        'items with text from other items
        btnTVSourceAdd.Text = Master.eLang.GetString(407, "Add Source")
        chkMovieSetCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        chkTVCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        chkTVEpisodeProperCase.Text = chkMovieProperCase.Text
        chkTVScraperMetaDataScan.Text = Master.eLang.GetString(517, "Scan Meta Data")
        chkTVShowProperCase.Text = chkMovieProperCase.Text
        gbMovieSetGeneralMediaListOpts.Text = gbMovieGeneralMediaListOpts.Text
        gbTVScraperDefFIExtOpts.Text = gbTVScraperDefFIExtOpts.Text
        lblSettingsTopTitle.Text = Text
        lblTVSkipLessThan.Text = Master.eLang.GetString(540, "Skip files smaller than:")
        lblTVSkipLessThanMB.Text = Master.eLang.GetString(539, "MB")

        LoadGeneralDateTime()
        LoadCustomScraperButtonModifierTypes_Movie()
        LoadCustomScraperButtonModifierTypes_MovieSet()
        LoadCustomScraperButtonModifierTypes_TV()
        LoadCustomScraperButtonScrapeTypes()
        LoadBannerSizes_Movie_MovieSet()
        LoadBannerSizes_TV()
        LoadBannerTypes()
        LoadCharacterArtSizes()
        LoadClearArtSizes()
        LoadClearLogoSizes()
        LoadDiscArtSizes()
        LoadFanartSizes()
        LoadLandscapeSizes()
        LoadPosterSizes_Movie_MovieSet()
        LoadPosterSizes_TV()
        LoadMovieTrailerQualities()
        LoadTVScraperOptionsOrdering()
    End Sub

    Private Sub ToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        _currbutton = DirectCast(DirectCast(sender, ToolStripButton).Tag, ButtonTag)
        FillList(_currbutton.ePanelType)
    End Sub

    Private Sub tvSettingsList_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSettingsList.AfterSelect
        If Not tvSettingsList.SelectedNode.ImageIndex = -1 Then
            pbSettingsCurrent.Image = ilSettings.Images(tvSettingsList.SelectedNode.ImageIndex)
        Else
            pbSettingsCurrent.Image = Nothing
        End If
        lblSettingsCurrent.Text = String.Format("{0} - {1}", _currbutton.strTitle, tvSettingsList.SelectedNode.Text)

        RemoveCurrPanel()

        _currPanel = _SettingsPanels.FirstOrDefault(Function(p) p.SettingsPanelID = tvSettingsList.SelectedNode.Name).Panel
        _currPanel.Location = New Point(0, 0)
        _currPanel.Dock = DockStyle.Fill
        pnlSettingsMain.Controls.Add(_currPanel)
        _currPanel.Visible = True
        pnlSettingsMain.Refresh()
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingRegex_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVSourcesRegexTVShowMatchingRegex.TextChanged
        ValidateTVShowMatching()
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingDefaultSeason_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVSourcesRegexTVShowMatchingDefaultSeason.TextChanged
        ValidateTVShowMatching()
    End Sub

    Private Sub txtTVSkipLessThan_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVSkipLessThan.TextChanged
        SetApplyButton(True)
        sResult.NeedsDBClean_TV = True
        sResult.NeedsDBUpdate_TV = True
    End Sub

    Private Sub txtTVScraperDefFIExt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVScraperDefFIExt.TextChanged
        btnTVScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtTVScraperDefFIExt.Text) AndAlso Not lstTVScraperDefFIExt.Items.Contains(If(txtTVScraperDefFIExt.Text.StartsWith("."), txtTVScraperDefFIExt.Text, String.Concat(".", txtTVScraperDefFIExt.Text)))
        If btnTVScraperDefFIExtAdd.Enabled Then
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub ValidateTVShowMatching()
        If Not String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingRegex.Text) AndAlso
            (String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text.Trim) OrElse Integer.TryParse(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0) AndAlso
            CInt(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text.Trim) >= 0) Then
            btnTVSourcesRegexTVShowMatchingAdd.Enabled = True
        Else
            btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        End If
    End Sub

    Class ListViewItemComparer
        Implements IComparer
        Private col As Integer

        Public Sub New()
            col = 0
        End Sub

        Public Sub New(ByVal column As Integer)
            col = column
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
           Implements IComparer.Compare
            Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        End Function
    End Class

    Private Sub btnMovieSetPathExtendedBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathExtendedBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieSetPathExtended.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetPathMSAABrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathMSAABrowse.Click
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetPathExpertSingleBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathExpertSingleBrowse.Click
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVShowThemeTvTunesCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnTVShowThemeTvTunesCustomPathBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtTVShowThemeTvTunesCustomPath.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnGeneralDaemonPathBrowse_Click(sender As Object, e As EventArgs) Handles btnGeneralVirtualDriveBinPathBrowse.Click
        Try
            With fileBrowse
                .Filter = "Virtual CloneDrive|VCDMount.exe"
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.FileName) AndAlso File.Exists(.FileName) Then
                        txtGeneralVirtualDriveBinPath.Text = .FileName
                        SetApplyButton(True)
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnGeneralDigitGrpSymbolSettings_Click(sender As Object, e As EventArgs) Handles btnGeneralDigitGrpSymbolSettings.Click
        Process.Start("INTL.CPL")
    End Sub

    Private Sub chkMovieSetUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetUseExpert.CheckedChanged
        SetApplyButton(True)

        btnMovieSetPathExpertSingleBrowse.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetBannerExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetBannerExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetClearArtExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetClearArtExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetClearLogoExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetClearLogoExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetDiscArtExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetDiscArtExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetFanartExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetFanartExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetLandscapeExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetLandscapeExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetNFOExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetNFOExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPathExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        'Me.txtMovieSetPosterExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        txtMovieSetPosterExpertSingle.Enabled = chkMovieSetUseExpert.Checked
    End Sub

    Private Sub chkTVUseBoxee_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseBoxee.CheckedChanged
        SetApplyButton(True)

        chkTVEpisodeNFOBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVEpisodePosterBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVSeasonPosterBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowBannerBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowFanartBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowNFOBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowPosterBoxee.Enabled = chkTVUseBoxee.Checked

        If Not chkTVUseBoxee.Checked Then
            chkTVEpisodeNFOBoxee.Checked = False
            chkTVEpisodePosterBoxee.Checked = False
            chkTVSeasonPosterBoxee.Checked = False
            chkTVShowBannerBoxee.Checked = False
            chkTVShowFanartBoxee.Checked = False
            chkTVShowNFOBoxee.Checked = False
            chkTVShowPosterBoxee.Checked = False
        Else
            chkTVEpisodeNFOBoxee.Checked = True
            chkTVEpisodePosterBoxee.Checked = True
            chkTVSeasonPosterBoxee.Checked = True
            chkTVShowBannerBoxee.Checked = True
            chkTVShowFanartBoxee.Checked = True
            chkTVShowNFOBoxee.Checked = True
            chkTVShowPosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkTVUseAD_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseAD.CheckedChanged
        SetApplyButton(True)

        chkTVSeasonLandscapeAD.Enabled = chkTVUseAD.Checked
        chkTVShowCharacterArtAD.Enabled = chkTVUseAD.Checked
        chkTVShowClearArtAD.Enabled = chkTVUseAD.Checked
        chkTVShowClearLogoAD.Enabled = chkTVUseAD.Checked
        chkTVShowLandscapeAD.Enabled = chkTVUseAD.Checked

        If Not chkTVUseAD.Checked Then
            chkTVSeasonLandscapeAD.Checked = False
            chkTVShowCharacterArtAD.Checked = False
            chkTVShowClearArtAD.Checked = False
            chkTVShowClearLogoAD.Checked = False
            chkTVShowLandscapeAD.Checked = False
        Else
            chkTVSeasonLandscapeAD.Checked = True
            chkTVShowCharacterArtAD.Checked = True
            chkTVShowClearArtAD.Checked = True
            chkTVShowClearLogoAD.Checked = True
            chkTVShowLandscapeAD.Checked = True
        End If
    End Sub

    Private Sub chkTVUseExtended_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseExtended.CheckedChanged
        SetApplyButton(True)

        chkTVSeasonLandscapeExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowCharacterArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowClearArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowClearLogoExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowKeyArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowLandscapeExtended.Enabled = chkTVUseExtended.Checked

        If Not chkTVUseExtended.Checked Then
            chkTVSeasonLandscapeExtended.Checked = False
            chkTVShowCharacterArtExtended.Checked = False
            chkTVShowClearArtExtended.Checked = False
            chkTVShowClearLogoExtended.Checked = False
            chkTVShowKeyArtExtended.Checked = False
            chkTVShowLandscapeExtended.Checked = False
        Else
            chkTVSeasonLandscapeExtended.Checked = True
            chkTVShowCharacterArtExtended.Checked = True
            chkTVShowClearArtExtended.Checked = True
            chkTVShowClearLogoExtended.Checked = True
            chkTVShowKeyArtExtended.Checked = True
            chkTVShowLandscapeExtended.Checked = True
        End If
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseFrodo.CheckedChanged
        SetApplyButton(True)

        chkTVEpisodeActorThumbsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVEpisodeNFOFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVEpisodePosterFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonBannerFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonFanartFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonPosterFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowActorThumbsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowBannerFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowExtrafanartsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowFanartFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowNFOFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowPosterFrodo.Enabled = chkTVUseFrodo.Checked

        If Not chkTVUseFrodo.Checked Then
            chkTVEpisodeActorThumbsFrodo.Checked = False
            chkTVEpisodeNFOFrodo.Checked = False
            chkTVEpisodePosterFrodo.Checked = False
            chkTVSeasonBannerFrodo.Checked = False
            chkTVSeasonFanartFrodo.Checked = False
            chkTVSeasonPosterFrodo.Checked = False
            chkTVShowActorThumbsFrodo.Checked = False
            chkTVShowBannerFrodo.Checked = False
            chkTVShowExtrafanartsFrodo.Checked = False
            chkTVShowFanartFrodo.Checked = False
            chkTVShowNFOFrodo.Checked = False
            chkTVShowPosterFrodo.Checked = False
        Else
            chkTVEpisodeActorThumbsFrodo.Checked = True
            chkTVEpisodeNFOFrodo.Checked = True
            chkTVEpisodePosterFrodo.Checked = True
            chkTVSeasonBannerFrodo.Checked = True
            chkTVSeasonFanartFrodo.Checked = True
            chkTVSeasonPosterFrodo.Checked = True
            chkTVShowActorThumbsFrodo.Checked = True
            chkTVShowBannerFrodo.Checked = True
            chkTVShowExtrafanartsFrodo.Checked = True
            chkTVShowFanartFrodo.Checked = True
            chkTVShowNFOFrodo.Checked = True
            chkTVShowPosterFrodo.Checked = True
        End If
    End Sub

    Private Sub chkTVUseYAMJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseYAMJ.CheckedChanged
        SetApplyButton(True)

        chkTVEpisodeNFOYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVEpisodePosterYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonBannerYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonFanartYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonPosterYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowBannerYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowFanartYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowNFOYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowPosterYAMJ.Enabled = chkTVUseYAMJ.Checked

        If Not chkTVUseYAMJ.Checked Then
            chkTVEpisodeNFOYAMJ.Checked = False
            chkTVEpisodePosterYAMJ.Checked = False
            chkTVSeasonBannerYAMJ.Checked = False
            chkTVSeasonFanartYAMJ.Checked = False
            chkTVSeasonPosterYAMJ.Checked = False
            chkTVShowBannerYAMJ.Checked = False
            chkTVShowFanartYAMJ.Checked = False
            chkTVShowNFOYAMJ.Checked = False
            chkTVShowPosterYAMJ.Checked = False
        Else
            chkTVEpisodeNFOYAMJ.Checked = True
            chkTVEpisodePosterYAMJ.Checked = True
            chkTVSeasonBannerYAMJ.Checked = True
            chkTVSeasonFanartYAMJ.Checked = True
            chkTVSeasonPosterYAMJ.Checked = True
            chkTVShowBannerYAMJ.Checked = True
            chkTVShowFanartYAMJ.Checked = True
            chkTVShowNFOYAMJ.Checked = True
            chkTVShowPosterYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkTVUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseExpert.CheckedChanged
        SetApplyButton(True)

        chkTVEpisodeActorthumbsExpert.Enabled = chkTVUseExpert.Checked
        chkTVShowActorthumbsExpert.Enabled = chkTVUseExpert.Checked
        chkTVShowExtrafanartsExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsBannerExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsLandscapeExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsPosterExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodeActorThumbsExtExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodeFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodeNFOExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodePosterExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonBannerExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonLandscapeExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonPosterExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowActorThumbsExtExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowBannerExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowCharacterArtExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowClearArtExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowClearLogoExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowLandscapeExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowNFOExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowPosterExpert.Enabled = chkTVUseExpert.Checked
    End Sub

    Private Sub chkMovieSetClickScrape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetClickScrape.CheckedChanged
        chkMovieSetClickScrapeAsk.Enabled = chkMovieSetClickScrape.Checked
        SetApplyButton(True)
    End Sub

    Private Sub pbMSAAInfo_Click(sender As Object, e As EventArgs) Handles pbMSAAInfo.Click
        Process.Start("http://forum.xbmc.org/showthread.php?tid=153502")
    End Sub

    Private Sub pbMSAAInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbMSAAInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbMSAAInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbMSAAInfo.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub pbTVSourcesADInfo_Click(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.Click
        Process.Start("http://kodi.wiki/view/Add-on:Artwork_Downloader#Filenaming")
    End Sub

    Private Sub pbTVSourcesADInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbTVSourcesADInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_Click(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.Click
        Process.Start("http://kodi.wiki/view/Add-on:TvTunes")
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub btnMovieSetScraperMapperAdd_Click(sender As Object, e As EventArgs) Handles btnMovieSetScraperTitleRenamerAdd.Click
        Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
        dgvMovieSetScraperTitleRenamer.CurrentCell = dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0)
        dgvMovieSetScraperTitleRenamer.BeginEdit(True)
        SetApplyButton(True)
    End Sub

    Private Sub btnMovieSetScraperMapperRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetScraperTitleRenamerRemove.Click
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            dgvMovieSetScraperTitleRenamer.Rows.RemoveAt(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex)
            SetApplyButton(True)
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMovieSetScraperTitleRenamer.CurrentCellDirtyStateChanged
        SetApplyButton(True)
    End Sub

    Private Sub dgvMovieSetScraperMapper_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMovieSetScraperTitleRenamer.SelectionChanged
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            btnMovieSetScraperTitleRenamerRemove.Enabled = True
        Else
            btnMovieSetScraperTitleRenamerRemove.Enabled = False
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvMovieSetScraperTitleRenamer.KeyDown
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub chkMovieSetImagesForceLanguage_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieSetImagesForceLanguage.CheckedChanged
        SetApplyButton(True)

        cbMovieSetImagesForcedLanguage.Enabled = chkMovieSetImagesForceLanguage.Checked
    End Sub

    Private Sub chkMovieSetImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieSetImagesMediaLanguageOnly.CheckedChanged
        SetApplyButton(True)

        chkMovieSetImagesGetBlankImages.Enabled = chkMovieSetImagesMediaLanguageOnly.Checked
        chkMovieSetImagesGetEnglishImages.Enabled = chkMovieSetImagesMediaLanguageOnly.Checked

        If Not chkMovieSetImagesMediaLanguageOnly.Checked Then
            chkMovieSetImagesGetBlankImages.Checked = False
            chkMovieSetImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub chkTVImagesForceLanguage_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVImagesForceLanguage.CheckedChanged
        SetApplyButton(True)

        cbTVImagesForcedLanguage.Enabled = chkTVImagesForceLanguage.Checked
    End Sub

    Private Sub chkTVImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVImagesMediaLanguageOnly.CheckedChanged
        SetApplyButton(True)

        chkTVImagesGetBlankImages.Enabled = chkTVImagesMediaLanguageOnly.Checked
        chkTVImagesGetEnglishImages.Enabled = chkTVImagesMediaLanguageOnly.Checked

        If Not chkTVImagesMediaLanguageOnly.Checked Then
            chkTVImagesGetBlankImages.Checked = False
            chkTVImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub chkGeneralImageFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilter.CheckedChanged
        SetApplyButton(True)
        chkGeneralImageFilterAutoscraper.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterFanart.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterImagedialog.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterPoster.Enabled = chkGeneralImageFilter.Checked
        lblGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilter.Checked
        lblGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilter.Checked
        txtGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilter.Checked
        txtGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilter.Checked
    End Sub

    Private Sub chkGeneralImageFilterAutoscraperImagedialog_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilterAutoscraper.CheckedChanged, chkGeneralImageFilterImagedialog.CheckedChanged
        SetApplyButton(True)
        If chkGeneralImageFilterImagedialog.Checked = False AndAlso chkGeneralImageFilterAutoscraper.Checked = False Then
            chkGeneralImageFilterPoster.Enabled = False
            chkGeneralImageFilterFanart.Enabled = False
        Else
            chkGeneralImageFilterPoster.Enabled = True
            chkGeneralImageFilterFanart.Enabled = True
        End If
    End Sub
    Private Sub chkGeneralImageFilterPoster_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilterPoster.CheckedChanged
        SetApplyButton(True)
        lblGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilterPoster.Checked
        txtGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilterPoster.Checked
    End Sub
    Private Sub chkGeneralImageFilterFanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilterFanart.CheckedChanged
        SetApplyButton(True)
        lblGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilterFanart.Checked
        txtGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilterFanart.Checked
    End Sub

    Private Sub txtGeneralImageFilterMatchRate_TextChanged(sender As Object, e As EventArgs) Handles txtGeneralImageFilterPosterMatchRate.LostFocus, txtGeneralImageFilterFanartMatchRate.LostFocus
        If chkGeneralImageFilter.Checked Then
            Dim txtbox As TextBox = CType(sender, TextBox)
            Dim matchfactor As Integer = 0
            Dim NotGood As Boolean = False
            If Integer.TryParse(txtbox.Text, matchfactor) Then
                If matchfactor < 0 OrElse matchfactor > 10 Then
                    NotGood = True
                End If
            Else
                NotGood = True
            End If
            If NotGood Then
                txtbox.Text = String.Empty
                MessageBox.Show(Master.eLang.GetString(1460, "Match Tolerance should be between 0 - 10 | 0 = 100% identical images, 10= different images"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub rbMovieGeneralCustomScrapeButtonDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieGeneralCustomScrapeButtonDisabled.CheckedChanged
        If rbMovieGeneralCustomScrapeButtonDisabled.Checked Then
            cbMovieGeneralCustomScrapeButtonModifierType.Enabled = False
            cbMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
            txtMovieGeneralCustomScrapeButtonModifierType.Enabled = False
            txtMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
        End If
        SetApplyButton(True)
    End Sub

    Private Sub rbMovieGeneralCustomScrapeButtonEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieGeneralCustomScrapeButtonEnabled.CheckedChanged
        If rbMovieGeneralCustomScrapeButtonEnabled.Checked Then
            cbMovieGeneralCustomScrapeButtonModifierType.Enabled = True
            cbMovieGeneralCustomScrapeButtonScrapeType.Enabled = True
            txtMovieGeneralCustomScrapeButtonModifierType.Enabled = True
            txtMovieGeneralCustomScrapeButtonScrapeType.Enabled = True
        End If
        SetApplyButton(True)
    End Sub

    Private Sub rbMovieSetGeneralCustomScrapeButtonDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieSetGeneralCustomScrapeButtonDisabled.CheckedChanged
        If rbMovieSetGeneralCustomScrapeButtonDisabled.Checked Then
            cbMovieSetGeneralCustomScrapeButtonModifierType.Enabled = False
            cbMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = False
            txtMovieSetGeneralCustomScrapeButtonModifierType.Enabled = False
            txtMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = False
        End If
        SetApplyButton(True)
    End Sub

    Private Sub rbMovieSetGeneralCustomScrapeButtonEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieSetGeneralCustomScrapeButtonEnabled.CheckedChanged
        If rbMovieSetGeneralCustomScrapeButtonEnabled.Checked Then
            cbMovieSetGeneralCustomScrapeButtonModifierType.Enabled = True
            cbMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = True
            txtMovieSetGeneralCustomScrapeButtonModifierType.Enabled = True
            txtMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = True
        End If
        SetApplyButton(True)
    End Sub

    Private Sub rbTVGeneralCustomScrapeButtonDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbTVGeneralCustomScrapeButtonDisabled.CheckedChanged
        If rbTVGeneralCustomScrapeButtonDisabled.Checked Then
            cbTVGeneralCustomScrapeButtonModifierType.Enabled = False
            cbTVGeneralCustomScrapeButtonScrapeType.Enabled = False
            txtTVGeneralCustomScrapeButtonModifierType.Enabled = False
            txtTVGeneralCustomScrapeButtonScrapeType.Enabled = False
        End If
        SetApplyButton(True)
    End Sub

    Private Sub rbTVGeneralCustomScrapeButtonEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbTVGeneralCustomScrapeButtonEnabled.CheckedChanged
        If rbTVGeneralCustomScrapeButtonEnabled.Checked Then
            cbTVGeneralCustomScrapeButtonModifierType.Enabled = True
            cbTVGeneralCustomScrapeButtonScrapeType.Enabled = True
            txtTVGeneralCustomScrapeButtonModifierType.Enabled = True
            txtTVGeneralCustomScrapeButtonScrapeType.Enabled = True
        End If
        SetApplyButton(True)
    End Sub

    Private Sub EnableApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        cbGeneralDateTime.SelectedIndexChanged,
        cbGeneralTheme.SelectedIndexChanged,
        cbGeneralVirtualDriveLetter.SelectedIndexChanged,
        cbMovieGeneralCustomScrapeButtonModifierType.SelectedIndexChanged,
        cbMovieGeneralCustomScrapeButtonScrapeType.SelectedIndexChanged,
        cbMovieLanguageOverlay.SelectedIndexChanged,
        cbMovieSetBannerPrefSize.SelectedIndexChanged,
        cbMovieSetClearArtPrefSize.SelectedIndexChanged,
        cbMovieSetClearLogoPrefSize.SelectedIndexChanged,
        cbMovieSetDiscArtPrefSize.SelectedIndexChanged,
        cbMovieSetFanartPrefSize.SelectedIndexChanged,
        cbMovieSetGeneralCustomScrapeButtonModifierType.SelectedIndexChanged,
        cbMovieSetGeneralCustomScrapeButtonScrapeType.SelectedIndexChanged,
        cbMovieSetImagesForcedLanguage.SelectedIndexChanged,
        cbMovieSetLandscapePrefSize.SelectedIndexChanged,
        cbMovieSetPosterPrefSize.SelectedIndexChanged,
        cbMovieTrailerMinVideoQual.SelectedIndexChanged,
        cbTVAllSeasonsBannerPrefType.SelectedIndexChanged,
        cbTVAllSeasonsFanartPrefSize.SelectedIndexChanged,
        cbTVAllSeasonsLandscapePrefSize.SelectedIndexChanged,
        cbTVAllSeasonsPosterPrefSize.SelectedIndexChanged,
        cbTVEpisodeFanartPrefSize.SelectedIndexChanged,
        cbTVEpisodePosterPrefSize.SelectedIndexChanged,
        cbTVGeneralCustomScrapeButtonModifierType.SelectedIndexChanged,
        cbTVGeneralCustomScrapeButtonScrapeType.SelectedIndexChanged,
        cbTVGeneralLang.SelectedIndexChanged,
        cbTVImagesForcedLanguage.SelectedIndexChanged,
        cbTVLanguageOverlay.SelectedIndexChanged,
        cbTVScraperOptionsOrdering.SelectedIndexChanged,
        cbTVSeasonBannerPrefType.SelectedIndexChanged,
        cbTVSeasonFanartPrefSize.SelectedIndexChanged,
        cbTVSeasonLandscapePrefSize.SelectedIndexChanged,
        cbTVSeasonPosterPrefSize.SelectedIndexChanged,
        cbTVShowBannerPrefSize.SelectedIndexChanged,
        cbTVShowBannerPrefType.SelectedIndexChanged,
        cbTVShowCharacterArtPrefSize.SelectedIndexChanged,
        cbTVShowClearArtPrefSize.SelectedIndexChanged,
        cbTVShowClearLogoPrefSize.SelectedIndexChanged,
        cbTVShowExtrafanartsPrefSize.SelectedIndexChanged,
        cbTVShowFanartPrefSize.SelectedIndexChanged,
        cbTVShowKeyArtPrefSize.SelectedIndexChanged,
        cbTVShowLandscapePrefSize.SelectedIndexChanged,
        cbTVShowPosterPrefSize.SelectedIndexChanged,
        chkGeneralCheckUpdates.CheckedChanged,
        chkGeneralDateAddedIgnoreNFO.CheckedChanged,
        chkGeneralDigitGrpSymbolVotes.CheckedChanged,
        chkGeneralDisplayGenresText.CheckedChanged,
        chkGeneralDisplayImgDims.CheckedChanged,
        chkGeneralDisplayImgNames.CheckedChanged,
        chkGeneralDisplayLangFlags.CheckedChanged,
        chkGeneralDoubleClickScrape.CheckedChanged,
        chkGeneralImageFilterAutoscraper.CheckedChanged,
        chkGeneralImageFilterFanart.CheckedChanged,
        chkGeneralImageFilterImagedialog.CheckedChanged,
        chkGeneralImageFilterPoster.CheckedChanged,
        chkGeneralImagesGlassOverlay.CheckedChanged,
        chkGeneralOverwriteNfo.CheckedChanged,
        chkGeneralSourceFromFolder.CheckedChanged,
        chkGeneralVideoSourceMappingByExtensionEnabled.CheckedChanged,
        chkGeneralVideoSourceMappingByRegexEnabled.CheckedChanged,
        chkMovieClickScrapeAsk.CheckedChanged,
        chkMovieGeneralMarkNew.CheckedChanged,
        chkMovieSetBannerExtended.CheckedChanged,
        chkMovieSetBannerKeepExisting.CheckedChanged,
        chkMovieSetBannerMSAA.CheckedChanged,
        chkMovieSetBannerPrefSizeOnly.CheckedChanged,
        chkMovieSetCleanDB.CheckedChanged,
        chkMovieSetCleanFiles.CheckedChanged,
        chkMovieSetClearArtExtended.CheckedChanged,
        chkMovieSetClearArtKeepExisting.CheckedChanged,
        chkMovieSetClearArtMSAA.CheckedChanged,
        chkMovieSetClearArtPrefSizeOnly.CheckedChanged,
        chkMovieSetClearLogoExtended.CheckedChanged,
        chkMovieSetClearLogoKeepExisting.CheckedChanged,
        chkMovieSetClearLogoMSAA.CheckedChanged,
        chkMovieSetClearLogoPrefSizeOnly.CheckedChanged,
        chkMovieSetClickScrapeAsk.CheckedChanged,
        chkMovieSetDiscArtExtended.CheckedChanged,
        chkMovieSetDiscArtPrefSizeOnly.CheckedChanged,
        chkMovieSetFanartExtended.CheckedChanged,
        chkMovieSetFanartKeepExisting.CheckedChanged,
        chkMovieSetFanartMSAA.CheckedChanged,
        chkMovieSetFanartPrefSizeOnly.CheckedChanged,
        chkMovieSetGeneralMarkNew.CheckedChanged,
        chkMovieSetImagesCacheEnabled.CheckedChanged,
        chkMovieSetImagesGetBlankImages.CheckedChanged,
        chkMovieSetImagesGetEnglishImages.CheckedChanged,
        chkMovieSetLandscapeExtended.CheckedChanged,
        chkMovieSetLandscapeKeepExisting.CheckedChanged,
        chkMovieSetLandscapeMSAA.CheckedChanged,
        chkMovieSetLandscapePrefSizeOnly.CheckedChanged,
        chkMovieSetLockPlot.CheckedChanged,
        chkMovieSetLockTitle.CheckedChanged,
        chkMovieSetPosterExtended.CheckedChanged,
        chkMovieSetPosterKeepExisting.CheckedChanged,
        chkMovieSetPosterMSAA.CheckedChanged,
        chkMovieSetPosterPrefSizeOnly.CheckedChanged,
        chkMovieSetScraperPlot.CheckedChanged,
        chkMovieSetScraperTitle.CheckedChanged,
        chkMovieThemeKeepExisting.CheckedChanged,
        chkMovieTrailerKeepExisting.CheckedChanged,
        chkTVAllSeasonsBannerKeepExisting.CheckedChanged,
        chkTVAllSeasonsBannerPrefSizeOnly.CheckedChanged,
        chkTVAllSeasonsFanartKeepExisting.CheckedChanged,
        chkTVAllSeasonsFanartPrefSizeOnly.CheckedChanged,
        chkTVAllSeasonsLandscapeKeepExisting.CheckedChanged,
        chkTVAllSeasonsLandscapePrefSizeOnly.CheckedChanged,
        chkTVAllSeasonsPosterKeepExisting.CheckedChanged,
        chkTVAllSeasonsPosterPrefSizeOnly.CheckedChanged,
        chkTVCleanDB.CheckedChanged,
        chkTVDisplayMissingEpisodes.CheckedChanged,
        chkTVEpisodeActorThumbsFrodo.CheckedChanged,
        chkTVEpisodeFanartKeepExisting.CheckedChanged,
        chkTVEpisodeFanartPrefSizeOnly.CheckedChanged,
        chkTVEpisodeNFOBoxee.CheckedChanged,
        chkTVEpisodeNFOFrodo.CheckedChanged,
        chkTVEpisodeNFONMJ.CheckedChanged,
        chkTVEpisodeNFOYAMJ.CheckedChanged,
        chkTVEpisodePosterBoxee.CheckedChanged,
        chkTVEpisodePosterFrodo.CheckedChanged,
        chkTVEpisodePosterKeepExisting.CheckedChanged,
        chkTVEpisodePosterPrefSizeOnly.CheckedChanged,
        chkTVEpisodePosterVideoExtractionPref.CheckedChanged,
        chkTVEpisodePosterYAMJ.CheckedChanged,
        chkTVGeneralClickScrapeAsk.CheckedChanged,
        chkTVGeneralMarkNewEpisodes.CheckedChanged,
        chkTVGeneralMarkNewShows.CheckedChanged,
        chkTVImagesCacheEnabled.CheckedChanged,
        chkTVImagesDisplayImageSelect.CheckedChanged,
        chkTVImagesGetBlankImages.CheckedChanged,
        chkTVImagesGetEnglishImages.CheckedChanged,
        chkTVImagesNotSaveURLToNfo.CheckedChanged,
        chkTVLockEpisodeLanguageA.CheckedChanged,
        chkTVLockEpisodeLanguageV.CheckedChanged,
        chkTVLockEpisodePlot.CheckedChanged,
        chkTVLockEpisodeRating.CheckedChanged,
        chkTVLockEpisodeRuntime.CheckedChanged,
        chkTVLockEpisodeTitle.CheckedChanged,
        chkTVLockEpisodeUserRating.CheckedChanged,
        chkTVLockSeasonPlot.CheckedChanged,
        chkTVLockShowCert.CheckedChanged,
        chkTVLockShowCreators.CheckedChanged,
        chkTVLockShowGenre.CheckedChanged,
        chkTVLockShowMPAA.CheckedChanged,
        chkTVLockShowOriginalTitle.CheckedChanged,
        chkTVLockShowPlot.CheckedChanged,
        chkTVLockShowRating.CheckedChanged,
        chkTVLockShowRuntime.CheckedChanged,
        chkTVLockShowStatus.CheckedChanged,
        chkTVLockShowStudio.CheckedChanged,
        chkTVLockShowTitle.CheckedChanged,
        chkTVLockShowUserRating.CheckedChanged,
        chkTVScraperCastWithImg.CheckedChanged,
        chkTVScraperCleanFields.CheckedChanged,
        chkTVScraperEpisodeAired.CheckedChanged,
        chkTVScraperEpisodeCredits.CheckedChanged,
        chkTVScraperEpisodeDirector.CheckedChanged,
        chkTVScraperEpisodeGuestStarsToActors.CheckedChanged,
        chkTVScraperEpisodePlot.CheckedChanged,
        chkTVScraperEpisodeRating.CheckedChanged,
        chkTVScraperEpisodeRuntime.CheckedChanged,
        chkTVScraperEpisodeTitle.CheckedChanged,
        chkTVScraperEpisodeUserRating.CheckedChanged,
        chkTVScraperSeasonAired.CheckedChanged,
        chkTVScraperSeasonPlot.CheckedChanged,
        chkTVScraperShowCreators.CheckedChanged,
        chkTVScraperShowEpiGuideURL.CheckedChanged,
        chkTVScraperShowMPAA.CheckedChanged,
        chkTVScraperShowOriginalTitleAsTitle.CheckedChanged,
        chkTVScraperShowPlot.CheckedChanged,
        chkTVScraperShowPremiered.CheckedChanged,
        chkTVScraperShowRating.CheckedChanged,
        chkTVScraperShowStatus.CheckedChanged,
        chkTVScraperShowTitle.CheckedChanged,
        chkTVScraperShowUserRating.CheckedChanged,
        chkTVScraperUseDisplaySeasonEpisode.CheckedChanged,
        chkTVScraperUseSRuntimeForEp.CheckedChanged,
        chkTVSeasonBannerFrodo.CheckedChanged,
        chkTVSeasonBannerKeepExisting.CheckedChanged,
        chkTVSeasonBannerPrefSizeOnly.CheckedChanged,
        chkTVSeasonBannerYAMJ.CheckedChanged,
        chkTVSeasonFanartFrodo.CheckedChanged,
        chkTVSeasonFanartKeepExisting.CheckedChanged,
        chkTVSeasonFanartPrefSizeOnly.CheckedChanged,
        chkTVSeasonFanartYAMJ.CheckedChanged,
        chkTVSeasonLandscapeAD.CheckedChanged,
        chkTVSeasonLandscapeExtended.CheckedChanged,
        chkTVSeasonLandscapeKeepExisting.CheckedChanged,
        chkTVSeasonLandscapePrefSizeOnly.CheckedChanged,
        chkTVSeasonPosterBoxee.CheckedChanged,
        chkTVSeasonPosterFrodo.CheckedChanged,
        chkTVSeasonPosterKeepExisting.CheckedChanged,
        chkTVSeasonPosterPrefSizeOnly.CheckedChanged,
        chkTVSeasonPosterYAMJ.CheckedChanged,
        chkTVShowActorThumbsFrodo.CheckedChanged,
        chkTVShowActorthumbsExpert.CheckedChanged,
        chkTVShowBannerBoxee.CheckedChanged,
        chkTVShowBannerFrodo.CheckedChanged,
        chkTVShowBannerKeepExisting.CheckedChanged,
        chkTVShowBannerPrefSizeOnly.CheckedChanged,
        chkTVShowBannerYAMJ.CheckedChanged,
        chkTVShowCharacterArtAD.CheckedChanged,
        chkTVShowCharacterArtExtended.CheckedChanged,
        chkTVShowCharacterArtKeepExisting.CheckedChanged,
        chkTVShowCharacterArtPrefSizeOnly.CheckedChanged,
        chkTVShowClearArtAD.CheckedChanged,
        chkTVShowClearArtExtended.CheckedChanged,
        chkTVShowClearArtKeepExisting.CheckedChanged,
        chkTVShowClearArtPrefSizeOnly.CheckedChanged,
        chkTVShowClearLogoAD.CheckedChanged,
        chkTVShowClearLogoExtended.CheckedChanged,
        chkTVShowClearLogoKeepExisting.CheckedChanged,
        chkTVShowClearLogoPrefSizeOnly.CheckedChanged,
        chkTVShowExtrafanartsExpert.CheckedChanged,
        chkTVShowExtrafanartsFrodo.CheckedChanged,
        chkTVShowExtrafanartsKeepExisting.CheckedChanged,
        chkTVShowExtrafanartsPrefSizeOnly.CheckedChanged,
        chkTVShowExtrafanartsPreselect.CheckedChanged,
        chkTVShowFanartBoxee.CheckedChanged,
        chkTVShowFanartFrodo.CheckedChanged,
        chkTVShowFanartKeepExisting.CheckedChanged,
        chkTVShowFanartPrefSizeOnly.CheckedChanged,
        chkTVShowFanartYAMJ.CheckedChanged,
        chkTVShowKeyArtExtended.CheckedChanged,
        chkTVShowKeyArtKeepExisting.CheckedChanged,
        chkTVShowKeyArtPrefSizeOnly.CheckedChanged,
        chkTVShowLandscapeAD.CheckedChanged,
        chkTVShowLandscapeExtended.CheckedChanged,
        chkTVShowLandscapeKeepExisting.CheckedChanged,
        chkTVShowLandscapePrefSizeOnly.CheckedChanged,
        chkTVShowNFOBoxee.CheckedChanged,
        chkTVShowNFOFrodo.CheckedChanged,
        chkTVShowNFONMJ.CheckedChanged,
        chkTVShowNFOYAMJ.CheckedChanged,
        chkTVShowPosterBoxee.CheckedChanged,
        chkTVShowPosterFrodo.CheckedChanged,
        chkTVShowPosterKeepExisting.CheckedChanged,
        chkTVShowPosterPrefSizeOnly.CheckedChanged,
        chkTVShowPosterYAMJ.CheckedChanged,
        chkTVShowThemeKeepExisting.CheckedChanged,
        dgvGeneralVideoSourceMappingByExtension.CellValueChanged,
        dgvGeneralVideoSourceMappingByExtension.RowsAdded,
        dgvGeneralVideoSourceMappingByExtension.RowsRemoved,
        dgvGeneralVideoSourceMappingByRegex.CellValueChanged,
        dgvGeneralVideoSourceMappingByRegex.RowsAdded,
        dgvGeneralVideoSourceMappingByRegex.RowsRemoved,
        txtGeneralImageFilterFanartMatchRate.TextChanged,
        txtGeneralImageFilterPosterMatchRate.TextChanged,
        txtGeneralVirtualDriveBinPath.TextChanged,
        txtMovieGeneralCustomMarker1.TextChanged,
        txtMovieGeneralCustomMarker2.TextChanged,
        txtMovieGeneralCustomMarker3.TextChanged,
        txtMovieGeneralCustomMarker4.TextChanged,
        txtMovieLevTolerance.TextChanged,
        txtMovieSetBannerExpertParent.TextChanged,
        txtMovieSetBannerExpertSingle.TextChanged,
        txtMovieSetBannerHeight.TextChanged,
        txtMovieSetBannerWidth.TextChanged,
        txtMovieSetClearArtExpertParent.TextChanged,
        txtMovieSetClearArtExpertSingle.TextChanged,
        txtMovieSetClearLogoExpertParent.TextChanged,
        txtMovieSetClearLogoExpertSingle.TextChanged,
        txtMovieSetDiscArtExpertParent.TextChanged,
        txtMovieSetDiscArtExpertSingle.TextChanged,
        txtMovieSetFanartExpertParent.TextChanged,
        txtMovieSetFanartExpertSingle.TextChanged,
        txtMovieSetFanartHeight.TextChanged,
        txtMovieSetFanartWidth.TextChanged,
        txtMovieSetLandscapeExpertParent.TextChanged,
        txtMovieSetLandscapeExpertSingle.TextChanged,
        txtMovieSetNFOExpertParent.TextChanged,
        txtMovieSetNFOExpertSingle.TextChanged,
        txtMovieSetPathMSAA.TextChanged,
        txtMovieSetPosterExpertParent.TextChanged,
        txtMovieSetPosterExpertSingle.TextChanged,
        txtMovieSetPosterHeight.TextChanged,
        txtMovieSetPosterWidth.TextChanged,
        txtProxyDomain.TextChanged,
        txtProxyPassword.TextChanged,
        txtProxyPort.TextChanged,
        txtProxyUsername.TextChanged,
        txtTVAllSeasonsBannerHeight.TextChanged,
        txtTVAllSeasonsBannerWidth.TextChanged,
        txtTVAllSeasonsFanartHeight.TextChanged,
        txtTVAllSeasonsFanartWidth.TextChanged,
        txtTVAllSeasonsPosterHeight.TextChanged,
        txtTVAllSeasonsPosterWidth.TextChanged,
        txtTVEpisodeFanartHeight.TextChanged,
        txtTVEpisodeFanartWidth.TextChanged,
        txtTVEpisodePosterHeight.TextChanged,
        txtTVEpisodePosterWidth.TextChanged,
        txtTVScraperDurationRuntimeFormat.TextChanged,
        txtTVScraperEpisodeActorsLimit.TextChanged,
        txtTVScraperEpisodeGuestStarsLimit.TextChanged,
        txtTVScraperShowActorsLimit.TextChanged,
        txtTVScraperShowStudioLimit.TextChanged,
        txtTVSeasonBannerHeight.TextChanged,
        txtTVSeasonBannerWidth.TextChanged,
        txtTVSeasonFanartHeight.TextChanged,
        txtTVSeasonFanartWidth.TextChanged,
        txtTVSeasonPosterHeight.TextChanged,
        txtTVSeasonPosterWidth.TextChanged,
        txtTVShowActorThumbsExtExpert.TextChanged,
        txtTVShowBannerExpert.TextChanged,
        txtTVShowBannerHeight.TextChanged,
        txtTVShowBannerWidth.TextChanged,
        txtTVShowCharacterArtExpert.TextChanged,
        txtTVShowClearArtExpert.TextChanged,
        txtTVShowClearLogoExpert.TextChanged,
        txtTVShowExtrafanartsHeight.TextChanged,
        txtTVShowExtrafanartsLimit.TextChanged,
        txtTVShowExtrafanartsWidth.TextChanged,
        txtTVShowFanartExpert.TextChanged,
        txtTVShowFanartHeight.TextChanged,
        txtTVShowFanartWidth.TextChanged,
        txtTVShowKeyArtHeight.TextChanged,
        txtTVShowKeyArtWidth.TextChanged,
        txtTVShowLandscapeExpert.TextChanged,
        txtTVShowNFOExpert.TextChanged,
        txtTVShowPosterExpert.TextChanged,
        txtTVShowPosterHeight.TextChanged,
        txtTVShowPosterWidth.TextChanged,
        txtTVShowThemeTvTunesCustomPath.TextChanged,
        txtTVShowThemeTvTunesSubDir.TextChanged,
        txtMovieSetKeyArtWidth.TextChanged,
        txtMovieSetKeyArtHeight.TextChanged,
        chkMovieSetKeyArtPrefSizeOnly.CheckedChanged,
        chkMovieSetKeyArtKeepExisting.CheckedChanged,
        cbMovieSetKeyArtPrefSize.SelectedIndexChanged,
        chkMovieSetKeyArtExtended.CheckedChanged, chkTVLockSeasonTitle.CheckedChanged

        SetApplyButton(True)
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtMovieLevTolerance.KeyPress,
        txtMovieSetBannerHeight.KeyPress,
        txtMovieSetBannerWidth.KeyPress,
        txtMovieSetFanartHeight.KeyPress,
        txtMovieSetFanartWidth.KeyPress,
        txtMovieSetKeyArtHeight.KeyPress,
        txtMovieSetKeyArtWidth.KeyPress,
        txtMovieSetPosterHeight.KeyPress,
        txtMovieSetPosterWidth.KeyPress,
        txtProxyPort.KeyPress,
        txtTVAllSeasonsBannerHeight.KeyPress,
        txtTVAllSeasonsBannerWidth.KeyPress,
        txtTVAllSeasonsFanartHeight.KeyPress,
        txtTVAllSeasonsFanartWidth.KeyPress,
        txtTVAllSeasonsPosterHeight.KeyPress,
        txtTVAllSeasonsPosterWidth.KeyPress,
        txtTVEpisodeFanartHeight.KeyPress,
        txtTVEpisodeFanartWidth.KeyPress,
        txtTVEpisodePosterHeight.KeyPress,
        txtTVEpisodePosterWidth.KeyPress,
        txtTVScraperEpisodeActorsLimit.KeyPress,
        txtTVScraperEpisodeGuestStarsLimit.KeyPress,
        txtTVScraperShowActorsLimit.KeyPress,
        txtTVScraperShowGenreLimit.KeyPress,
        txtTVScraperShowStudioLimit.KeyPress,
        txtTVShowBannerHeight.KeyPress,
        txtTVShowBannerWidth.KeyPress,
        txtTVShowExtrafanartsHeight.KeyPress,
        txtTVShowExtrafanartsLimit.KeyPress,
        txtTVShowExtrafanartsWidth.KeyPress,
        txtTVShowFanartHeight.KeyPress,
        txtTVShowFanartWidth.KeyPress,
        txtTVShowFanartWidth.KeyPress,
        txtTVShowKeyArtHeight.KeyPress,
        txtTVShowKeyArtWidth.KeyPress,
        txtTVShowPosterHeight.KeyPress,
        txtTVShowPosterWidth.KeyPress

        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_Limit_Leave(sender As Object, e As EventArgs) Handles _
        txtTVShowExtrafanartsLimit.Leave
        Dim iLimit As Integer
        Dim tTextBox = CType(sender, TextBox)
        If Not Integer.TryParse(tTextBox.Text, iLimit) OrElse iLimit > Settings.ExtraImagesLimit OrElse iLimit = 0 Then
            Dim strTitle As String = Master.eLang.GetString(934, "Image Limit")
            Dim strText As String = Master.eLang.GetString(935, "We have to limit the amount of images downloaded to a suitable value to prevent needless traffic on the image providers.{0}The limit for automatically downloaded Extrafanarts and Extrathumbs is 20.{0}{0}Notes: Most skins can't show more than 4 Extrathumbs.{0}It's still possible to manually select as many as you want in the ""Image Select"" dialog.")
            MessageBox.Show(String.Format(strText, Environment.NewLine), strTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            tTextBox.Text = "20"
        End If
    End Sub

    Private Sub btnTVScraperSeasonTitleBlacklist_Click(sender As Object, e As EventArgs) Handles btnTVScraperSeasonTitleBlacklist.Click
        If dlgSettingsSeasonTitleBlacklist.ShowDialog(TempTVScraperSeasonTitleBlacklist) = DialogResult.OK Then
            TempTVScraperSeasonTitleBlacklist = dlgSettingsSeasonTitleBlacklist.Result
            SetApplyButton(True)
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure ButtonTag

        Dim iIndex As Integer
        Dim strTitle As String
        Dim ePanelType As Enums.SettingsPanelType

    End Structure

#End Region 'Nested Types

End Class