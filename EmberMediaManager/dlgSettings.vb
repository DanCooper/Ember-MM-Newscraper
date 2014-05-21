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
' # Dialog size: 1024; 768
' # Panel size: 750; 500
' # Enlarge it to see all the panels.

Imports System
Imports System.IO
Imports EmberAPI
Imports System.Net

Public Class dlgSettings

#Region "Fields"

    Private currPanel As New Panel
    Private currText As String = String.Empty
    Private dHelp As New Dictionary(Of String, String)
    Private didApply As Boolean = False
    Private MovieMeta As New List(Of Settings.MetadataPerType)
    Private NoUpdate As Boolean = True
    Private SettingsPanels As New List(Of Containers.SettingsPanel)
    Private TVShowRegex As New List(Of Settings.TVShowRegEx)
    Private sResult As New Structures.SettingsResult
    'Private tLangList As New List(Of Containers.TVLanguage)
    Private TVMeta As New List(Of Settings.MetadataPerType)
    Public Event LoadEnd()

#End Region 'Fields

#Region "Methods"

    Public Overloads Function ShowDialog() As Structures.SettingsResult
        MyBase.ShowDialog()
        Return Me.sResult
    End Function

    Private Sub AddButtons()
        Dim TSBs As New List(Of ToolStripButton)
        Dim TSB As ToolStripButton
        Dim ButtonsWidth As Integer = 0

        Me.tsSettingsTopMenu.Items.Clear()

        'first create all the buttons so we can get their size to calculate the spacer
        TSB = New ToolStripButton With { _
              .Text = Master.eLang.GetString(390, "Options"), _
              .Image = My.Resources.General, _
              .TextImageRelation = TextImageRelation.ImageAboveText, _
              .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
              .Tag = 100}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)
        TSB = New ToolStripButton With { _
              .Text = Master.eLang.GetString(36, "Movies"), _
              .Image = My.Resources.Movie, _
              .TextImageRelation = TextImageRelation.ImageAboveText, _
              .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
              .Tag = 200}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)
        TSB = New ToolStripButton With { _
              .Text = Master.eLang.GetString(653, "TV Shows"), _
              .Image = My.Resources.TVShows, _
              .TextImageRelation = TextImageRelation.ImageAboveText, _
              .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
              .Tag = 300}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)
        TSB = New ToolStripButton With { _
              .Text = Master.eLang.GetString(802, "Modules"), _
              .Image = My.Resources.modules, _
              .TextImageRelation = TextImageRelation.ImageAboveText, _
              .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
              .Tag = 400}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)

        TSB = New ToolStripButton With { _
            .Text = Master.eLang.GetString(429, "Miscellaneous"), _
            .Image = My.Resources.Miscellaneous, _
            .TextImageRelation = TextImageRelation.ImageAboveText, _
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
            .Tag = 400}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)

        If TSBs.Count > 0 Then
            Dim spacerMod As Integer = 4

            'calculate the spacer width
            For Each tsbWidth As ToolStripButton In TSBs
                ButtonsWidth += tsbWidth.Width
            Next

            Using g As Graphics = Me.CreateGraphics
                spacerMod = Convert.ToInt32(4 * (g.DpiX / 100))
            End Using

            Dim sSpacer As String = New String(Convert.ToChar(" "), Convert.ToInt32(((Me.tsSettingsTopMenu.Width - ButtonsWidth) / (TSBs.Count + 1)) / spacerMod))

            'add it all
            For Each tButton As ToolStripButton In TSBs.OrderBy(Function(b) Convert.ToInt32(b.Tag))
                If sSpacer.Length > 0 Then Me.tsSettingsTopMenu.Items.Add(New ToolStripLabel With {.Text = sSpacer})
                Me.tsSettingsTopMenu.Items.Add(tButton)
            Next

            'set default page
            Me.currText = TSBs.Item(0).Text
            Me.FillList(currText)
        End If
    End Sub

    Private Sub AddHelpHandlers(ByVal Parent As Control, ByVal Prefix As String)
        Dim pfName As String = String.Empty

        For Each ctrl As Control In Parent.Controls
            If Not TypeOf ctrl Is GroupBox AndAlso Not TypeOf ctrl Is Panel AndAlso Not TypeOf ctrl Is Label AndAlso _
            Not TypeOf ctrl Is TreeView AndAlso Not TypeOf ctrl Is ToolStrip AndAlso Not TypeOf ctrl Is PictureBox AndAlso _
            Not TypeOf ctrl Is TabControl Then
                pfName = String.Concat(Prefix, ctrl.Name)
                ctrl.AccessibleDescription = pfName
                If dHelp.ContainsKey(pfName) Then
                    dHelp.Item(pfName) = Master.eLang.GetHelpString(pfName)
                Else
                    AddHandler ctrl.MouseEnter, AddressOf HelpMouseEnter
                    AddHandler ctrl.MouseLeave, AddressOf HelpMouseLeave
                    dHelp.Add(pfName, Master.eLang.GetHelpString(pfName))
                End If
            End If
            If ctrl.HasChildren Then
                AddHelpHandlers(ctrl, Prefix)
            End If
        Next
    End Sub

    Private Sub AddPanels()
        Me.SettingsPanels.Clear()

        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovies", _
             .Text = Master.eLang.GetString(38, "General"), _
             .ImageIndex = 2, _
             .Type = Master.eLang.GetString(36, "Movies"), _
             .Panel = Me.pnlMovieGeneral, _
             .Order = 100})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlSources", _
             .Text = Master.eLang.GetString(555, "Files and Sources"), _
             .ImageIndex = 5, _
             .Type = Master.eLang.GetString(36, "Movies"), _
             .Panel = Me.pnlMovieSources, _
             .Order = 200})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovieData", _
             .Text = Master.eLang.GetString(556, "Scrapers - Data"), _
             .ImageIndex = 3, _
             .Type = Master.eLang.GetString(36, "Movies"), _
             .Panel = Me.pnlMovieScraper, _
             .Order = 300})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovieMedia", _
             .Text = Master.eLang.GetString(557, "Scrapers - Images"), _
             .ImageIndex = 6, _
             .Type = Master.eLang.GetString(36, "Movies"), _
             .Panel = Me.pnlMovieImages, _
             .Order = 400})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovieTrailer", _
             .Text = Master.eLang.GetString(559, "Scrapers - Trailers"), _
             .ImageIndex = 6, _
             .Type = Master.eLang.GetString(36, "Movies"), _
             .Panel = Me.pnlMovieTrailers, _
             .Order = 500})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovieTheme", _
             .Text = Master.eLang.GetString(1068, "Scrapers - Themes"), _
             .ImageIndex = 11, _
             .Type = Master.eLang.GetString(36, "Movies"), _
             .Panel = Me.pnlMovieThemes, _
             .Order = 600})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlShows", _
             .Text = Master.eLang.GetString(38, "General"), _
             .ImageIndex = 7, _
             .Type = Master.eLang.GetString(653, "TV Shows"), _
             .Panel = Me.pnlTVGeneral, _
             .Order = 100})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlTVSources", _
             .Text = Master.eLang.GetString(555, "Files and Sources"), _
             .ImageIndex = 5, _
             .Type = Master.eLang.GetString(653, "TV Shows"), _
             .Panel = Me.pnlTVSources, _
             .Order = 200})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlTVData", _
             .Text = Master.eLang.GetString(556, "Scrapers - Data"), _
             .ImageIndex = 3, _
             .Type = Master.eLang.GetString(653, "TV Shows"), _
             .Panel = Me.pnlTVScraper, _
             .Order = 300})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlTVMedia", _
             .Text = Master.eLang.GetString(557, "Scrapers - Images"), _
             .ImageIndex = 6, _
             .Type = Master.eLang.GetString(653, "TV Shows"), _
             .Panel = Me.pnlTVImages, _
             .Order = 400})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlTVTheme", _
             .Text = Master.eLang.GetString(1068, "Scrapers - Themes"), _
             .ImageIndex = 11, _
             .Type = Master.eLang.GetString(653, "TV Shows"), _
             .Panel = Me.pnlTVThemes, _
             .Order = 500})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlGeneral", _
             .Text = Master.eLang.GetString(38, "General"), _
             .ImageIndex = 0, _
             .Type = Master.eLang.GetString(390, "Options"), _
             .Panel = Me.pnlGeneral, _
             .Order = 100})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlExtensions", _
             .Text = Master.eLang.GetString(553, "File System"), _
             .ImageIndex = 4, _
             .Type = Master.eLang.GetString(390, "Options"), _
             .Panel = Me.pnlFileSystem, _
             .Order = 200})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlProxy", _
             .Text = Master.eLang.GetString(421, "Connection"), _
             .ImageIndex = 1, _
             .Type = Master.eLang.GetString(390, "Options"), _
             .Panel = Me.pnlProxy, _
             .Order = 300})
        AddScraperPanels()
    End Sub

    Sub AddScraperPanels()
        Dim ModuleCounter As Integer = 1
        Dim tPanel As New Containers.SettingsPanel
        For Each s As ModulesManager._externalScraperModuleClass_Data In ModulesManager.Instance.externalDataScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            AddHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalScraperModuleClass_Poster In ModulesManager.Instance.externalPosterScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            AddHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalScraperModuleClass_Theme In ModulesManager.Instance.externalThemeScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            AddHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalScraperModuleClass_Trailer In ModulesManager.Instance.externalTrailerScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            AddHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalTVScraperModuleClass In ModulesManager.Instance.externalTVScrapersModules.Where(Function(y) y.ProcessorModule.IsScraper).OrderBy(Function(x) x.ScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.SetupScraperChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            AddHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalTVScraperModuleClass In ModulesManager.Instance.externalTVScrapersModules.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.PostScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupPostScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.SetupPostScraperChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalTVScraperModuleClass_Theme In ModulesManager.Instance.externalTVThemeScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            AddHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalProcessorModules
            tPanel = s.ProcessorModule.InjectSetup
            If Not tPanel Is Nothing Then
                tPanel.Order += ModuleCounter
                If tPanel.ImageIndex = -1 AndAlso Not tPanel.Image Is Nothing Then
                    ilSettings.Images.Add(String.Concat(s.AssemblyName, tPanel.Name), tPanel.Image)
                    tPanel.ImageIndex = ilSettings.Images.IndexOfKey(String.Concat(s.AssemblyName, tPanel.Name))
                End If
                Me.SettingsPanels.Add(tPanel)
                ModuleCounter += 1
                AddHandler s.ProcessorModule.ModuleSetupChanged, AddressOf Handle_ModuleSetupChanged
                AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
                Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
            End If
        Next
    End Sub

    Sub RemoveScraperPanels()
        For Each s As ModulesManager._externalScraperModuleClass_Data In ModulesManager.Instance.externalDataScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Poster In ModulesManager.Instance.externalPosterScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme In ModulesManager.Instance.externalThemeScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Trailer In ModulesManager.Instance.externalTrailerScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
		For Each s As ModulesManager._externalTVScraperModuleClass In ModulesManager.Instance.externalTVScrapersModules.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.PostScraperOrder)
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalTVScraperModuleClass_Theme In ModulesManager.Instance.externalTVThemeScrapersModules.OrderBy(Function(x) x.ScraperOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalProcessorModules
            RemoveHandler s.ProcessorModule.ModuleSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Next
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        Me.sResult.NeedsRestart = True
    End Sub

    Private Sub btnTVEpisodeFilterAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterAdd.Click
        If Not String.IsNullOrEmpty(Me.txtTVEpisodeFilter.Text) Then
            Me.lstTVEpisodeFilter.Items.Add(Me.txtTVEpisodeFilter.Text)
            Me.txtTVEpisodeFilter.Text = String.Empty
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If

        Me.txtTVEpisodeFilter.Focus()
    End Sub

    Private Sub btnMovieFilterAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterAdd.Click
        If Not String.IsNullOrEmpty(Me.txtMovieFilter.Text) Then
            Me.lstMovieFilters.Items.Add(Me.txtMovieFilter.Text)
            Me.txtMovieFilter.Text = String.Empty
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If

        Me.txtMovieFilter.Focus()
    End Sub

    Private Sub btnFileSystemValidExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidExts.Text) Then
            If Not Strings.Left(txtFileSystemValidExts.Text, 1) = "." Then txtFileSystemValidExts.Text = String.Concat(".", txtFileSystemValidExts.Text)
            If Not lstFileSystemValidExts.Items.Contains(txtFileSystemValidExts.Text.ToLower) Then
                lstFileSystemValidExts.Items.Add(txtFileSystemValidExts.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
                txtFileSystemValidExts.Text = String.Empty
                txtFileSystemValidExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidThemeExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidThemeExts.Text) Then
            If Not Strings.Left(txtFileSystemValidThemeExts.Text, 1) = "." Then txtFileSystemValidThemeExts.Text = String.Concat(".", txtFileSystemValidThemeExts.Text)
            If Not lstFileSystemValidThemeExts.Items.Contains(txtFileSystemValidThemeExts.Text.ToLower) Then
                lstFileSystemValidThemeExts.Items.Add(txtFileSystemValidThemeExts.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
                txtFileSystemValidThemeExts.Text = String.Empty
                txtFileSystemValidThemeExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemNoStackExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemNoStackExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemNoStackExts.Text) Then
            If Not Strings.Left(txtFileSystemNoStackExts.Text, 1) = "." Then txtFileSystemNoStackExts.Text = String.Concat(".", txtFileSystemNoStackExts.Text)
            If Not lstFileSystemNoStackExts.Items.Contains(txtFileSystemNoStackExts.Text) Then
                lstFileSystemNoStackExts.Items.Add(txtFileSystemNoStackExts.Text)
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
                txtFileSystemNoStackExts.Text = String.Empty
                txtFileSystemNoStackExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVShowFilterAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterAdd.Click
        If Not String.IsNullOrEmpty(Me.txtTVShowFilter.Text) Then
            Me.lstTVShowFilter.Items.Add(Me.txtTVShowFilter.Text)
            Me.txtTVShowFilter.Text = String.Empty
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If

        Me.txtTVShowFilter.Focus()
    End Sub

    Private Sub btnTVShowRegexAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexAdd.Click
        If String.IsNullOrEmpty(Me.btnTVShowRegexAdd.Tag.ToString) Then
            Dim lID = (From lRegex As Settings.TVShowRegEx In Me.TVShowRegex Select lRegex.ID).Max
            Me.TVShowRegex.Add(New Settings.TVShowRegEx With {.ID = Convert.ToInt32(lID) + 1, .SeasonRegex = Me.txtTVSeasonRegex.Text, .SeasonFromDirectory = Not Convert.ToBoolean(Me.cbTVSeasonRetrieve.SelectedIndex), .EpisodeRegex = Me.txtTVEpisodeRegex.Text, .EpisodeRetrieve = DirectCast(Me.cbTVEpisodeRetrieve.SelectedIndex, Settings.EpRetrieve)})
        Else
            Dim selRex = From lRegex As Settings.TVShowRegEx In Me.TVShowRegex Where lRegex.ID = Convert.ToInt32(Me.btnTVShowRegexAdd.Tag)
            If selRex.Count > 0 Then
                selRex(0).SeasonRegex = Me.txtTVSeasonRegex.Text
                selRex(0).SeasonFromDirectory = Not Convert.ToBoolean(Me.cbTVSeasonRetrieve.SelectedIndex)
                selRex(0).EpisodeRegex = Me.txtTVEpisodeRegex.Text
                selRex(0).EpisodeRetrieve = DirectCast(Me.cbTVEpisodeRetrieve.SelectedIndex, Settings.EpRetrieve)
            End If
        End If

        Me.ClearTVRegex()
        Me.SetApplyButton(True)
        Me.LoadTVShowRegex()
    End Sub

    Private Sub btnMovieSortTokenAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtMovieSortToken.Text) Then
            If Not lstMovieSortTokens.Items.Contains(txtMovieSortToken.Text) Then
                lstMovieSortTokens.Items.Add(txtMovieSortToken.Text)
                Me.sResult.NeedsRefresh = True
                Me.SetApplyButton(True)
                txtMovieSortToken.Text = String.Empty
                txtMovieSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVSourceAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourceAdd.Click
        Using dSource As New dlgTVSource
            If dSource.ShowDialog = Windows.Forms.DialogResult.OK Then
                RefreshTVSources()
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
            End If
        End Using
    End Sub

    Private Sub btnFileSystemCleanerWhitelistAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemCleanerWhitelistAdd.Click
        If Not String.IsNullOrEmpty(Me.txtFileSystemCleanerWhitelist.Text) Then
            If Not Strings.Left(txtFileSystemCleanerWhitelist.Text, 1) = "." Then txtFileSystemCleanerWhitelist.Text = String.Concat(".", txtFileSystemCleanerWhitelist.Text)
            If Not lstFileSystemCleanerWhitelist.Items.Contains(txtFileSystemCleanerWhitelist.Text.ToLower) Then
                lstFileSystemCleanerWhitelist.Items.Add(txtFileSystemCleanerWhitelist.Text.ToLower)
                Me.SetApplyButton(True)
                txtFileSystemCleanerWhitelist.Text = String.Empty
                txtFileSystemCleanerWhitelist.Focus()
            End If
        End If
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Me.SaveSettings(True)
            Me.SetApplyButton(False)
            If Me.sResult.NeedsUpdate OrElse Me.sResult.NeedsRefresh Then Me.didApply = True
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnMovieBackdropsBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieBackdropsBrowse.Click
        With Me.fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(552, "Select the folder where you wish to store your backdrops...")
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtMovieBackdropsPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Not didApply Then sResult.DidCancel = True
        RemoveScraperPanels()
        Me.Close()
    End Sub

    Private Sub btnTVShowRegexClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexClear.Click
        Me.ClearTVRegex()
    End Sub

    Private Sub btnMovieFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterDown.Click
        Try
            If Me.lstMovieFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstMovieFilters.SelectedItem) AndAlso Me.lstMovieFilters.SelectedIndex < (Me.lstMovieFilters.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstMovieFilters.SelectedIndices(0)
                Me.lstMovieFilters.Items.Insert(iIndex + 2, Me.lstMovieFilters.SelectedItems(0))
                Me.lstMovieFilters.Items.RemoveAt(iIndex)
                Me.lstMovieFilters.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnMovieScraperDefFIExtEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieScraperDefFIExtEdit.Click
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            For Each x As Settings.MetadataPerType In MovieMeta
                If x.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    fi = dEditMeta.ShowDialog(x.MetaData, False)
                    If Not fi Is Nothing Then
                        MovieMeta.Remove(x)
                        Dim m As New Settings.MetadataPerType
                        m.FileType = x.FileType
                        m.MetaData = New MediaInfo.Fileinfo
                        m.MetaData = fi
                        MovieMeta.Add(m)
                        LoadMovieMetadata()
                        Me.SetApplyButton(True)
                    End If
                    Exit For
                End If
            Next
        End Using
    End Sub

    Private Sub btnTVShowRegexEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexEdit.Click
        If Me.lvTVShowRegex.SelectedItems.Count > 0 Then Me.EditShowRegex(lvTVShowRegex.SelectedItems(0))
    End Sub

    Private Sub btnMovieSourceEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSourceEdit.Click
        If lvMovieSources.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgMovieSource
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshMovieSources()
                    Me.sResult.NeedsUpdate = True
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub btnTVScraperDefFIExtEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVScraperDefFIExtEdit.Click
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            For Each x As Settings.MetadataPerType In TVMeta
                If x.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                    fi = dEditMeta.ShowDialog(x.MetaData, True)
                    If Not fi Is Nothing Then
                        TVMeta.Remove(x)
                        Dim m As New Settings.MetadataPerType
                        m.FileType = x.FileType
                        m.MetaData = New MediaInfo.Fileinfo
                        m.MetaData = fi
                        TVMeta.Add(m)
                        LoadTVMetadata()
                        Me.SetApplyButton(True)
                    End If
                    Exit For
                End If
            Next
        End Using
    End Sub

    Private Sub btnTVSourceEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourceEdit.Click
        If lvTVSources.SelectedItems.Count > 0 Then
            Using dTVSource As New dlgTVSource
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshTVSources()
                    Me.sResult.NeedsUpdate = True
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub btnTVEpisodeFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterDown.Click
        Try
            If Me.lstTVEpisodeFilter.Items.Count > 0 AndAlso Not IsNothing(Me.lstTVEpisodeFilter.SelectedItem) AndAlso Me.lstTVEpisodeFilter.SelectedIndex < (Me.lstTVEpisodeFilter.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstTVEpisodeFilter.SelectedIndices(0)
                Me.lstTVEpisodeFilter.Items.Insert(iIndex + 2, Me.lstTVEpisodeFilter.SelectedItems(0))
                Me.lstTVEpisodeFilter.Items.RemoveAt(iIndex)
                Me.lstTVEpisodeFilter.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnTVEpisodeFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterUp.Click
        Try
            If Me.lstTVEpisodeFilter.Items.Count > 0 AndAlso Not IsNothing(Me.lstTVEpisodeFilter.SelectedItem) AndAlso Me.lstTVEpisodeFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstTVEpisodeFilter.SelectedIndices(0)
                Me.lstTVEpisodeFilter.Items.Insert(iIndex - 1, Me.lstTVEpisodeFilter.SelectedItems(0))
                Me.lstTVEpisodeFilter.Items.RemoveAt(iIndex + 1)
                Me.lstTVEpisodeFilter.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnMovieSourceAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSourceAdd.Click
        Using dSource As New dlgMovieSource
            If dSource.ShowDialog = Windows.Forms.DialogResult.OK Then
                RefreshMovieSources()
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
            End If
        End Using
    End Sub

    Private Sub btnMovieSourceRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSourceRemove.Click
        Me.RemoveMovieSource()
        Master.DB.LoadMovieSourcesFromDB()
    End Sub

    Private Sub btnMovieScraperDefFIExtAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieScraperDefFIExtAdd.Click
        If Not txtMovieScraperDefFIExt.Text.StartsWith(".") Then txtMovieScraperDefFIExt.Text = String.Concat(".", txtMovieScraperDefFIExt.Text)
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            fi = dEditMeta.ShowDialog(fi, False)
            If Not fi Is Nothing Then
                Dim m As New Settings.MetadataPerType
                m.FileType = txtMovieScraperDefFIExt.Text
                m.MetaData = New MediaInfo.Fileinfo
                m.MetaData = fi
                MovieMeta.Add(m)
                LoadMovieMetadata()
                Me.SetApplyButton(True)
                Me.txtMovieScraperDefFIExt.Text = String.Empty
                Me.txtMovieScraperDefFIExt.Focus()
            End If
        End Using
    End Sub

    Private Sub btnTVScraperDefFIExtAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVScraperDefFIExtAdd.Click
        If Not txtTVScraperDefFIExt.Text.StartsWith(".") Then txtTVScraperDefFIExt.Text = String.Concat(".", txtTVScraperDefFIExt.Text)
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            fi = dEditMeta.ShowDialog(fi, True)
            If Not fi Is Nothing Then
                Dim m As New Settings.MetadataPerType
                m.FileType = txtTVScraperDefFIExt.Text
                m.MetaData = New MediaInfo.Fileinfo
                m.MetaData = fi
                TVMeta.Add(m)
                LoadTVMetadata()
                Me.SetApplyButton(True)
                Me.txtTVScraperDefFIExt.Text = String.Empty
                Me.txtTVScraperDefFIExt.Focus()
            End If
        End Using
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        NoUpdate = True
        Me.SaveSettings(False)
        RemoveScraperPanels()
        Me.Close()
    End Sub

    Private Sub btnTVShowRegexUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexUp.Click
        Try
            If Me.lvTVShowRegex.Items.Count > 0 AndAlso Me.lvTVShowRegex.SelectedItems.Count > 0 AndAlso Not Me.lvTVShowRegex.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.TVShowRegEx = Me.TVShowRegex.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(Me.lvTVShowRegex.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVShowRegex.SuspendLayout()
                    Dim iIndex As Integer = Me.TVShowRegex.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVShowRegex.SelectedIndices(0)
                    Me.TVShowRegex.Remove(selItem)
                    Me.TVShowRegex.Insert(iIndex - 1, selItem)

                    Me.RenumberTVShowRegex()
                    Me.LoadTVShowRegex()

                    Me.lvTVShowRegex.Items(selIndex - 1).Selected = True
                    Me.lvTVShowRegex.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVShowRegex.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnTVShowRegexDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexDown.Click
        Try
            If Me.lvTVShowRegex.Items.Count > 0 AndAlso Me.lvTVShowRegex.SelectedItems.Count > 0 AndAlso Me.lvTVShowRegex.SelectedItems(0).Index < (Me.lvTVShowRegex.Items.Count - 1) Then
                Dim selItem As Settings.TVShowRegEx = Me.TVShowRegex.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(Me.lvTVShowRegex.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVShowRegex.SuspendLayout()
                    Dim iIndex As Integer = Me.TVShowRegex.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVShowRegex.SelectedIndices(0)
                    Me.TVShowRegex.Remove(selItem)
                    Me.TVShowRegex.Insert(iIndex + 1, selItem)

                    Me.RenumberTVShowRegex()
                    Me.LoadTVShowRegex()

                    Me.lvTVShowRegex.Items(selIndex + 1).Selected = True
                    Me.lvTVShowRegex.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVShowRegex.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnTVShowFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterReset.Click
        If MsgBox(Master.eLang.GetString(840, "Are you sure you want to reset to the default list of show filters?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ShowFilters, True)
            Me.RefreshTVShowFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnTVEpisodeFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterReset.Click
        If MsgBox(Master.eLang.GetString(841, "Are you sure you want to reset to the default list of episode filters?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.EpFilters, True)
            Me.RefreshTVEpisodeFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnMovieFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterReset.Click
        If MsgBox(Master.eLang.GetString(842, "Are you sure you want to reset to the default list of movie filters?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieFilters, True)
            Me.RefreshMovieFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidExtsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidExtsReset.Click
        If MsgBox(Master.eLang.GetString(843, "Are you sure you want to reset to the default list of valid video extensions?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidExts, True)
            Me.RefreshFileSystemValidExts()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidThemeExtsReset.Click
        If MsgBox(Master.eLang.GetString(1080, "Are you sure you want to reset to the default list of valid theme extensions?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidThemeExts, True)
            Me.RefreshFileSystemValidThemeExts()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnTVShowRegexGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexGet.Click
        Using dd As New dlgTVRegExProfiles
            If dd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.TVShowRegex.Clear()
                Me.TVShowRegex.AddRange(dd.ShowRegex)
                Me.LoadTVShowRegex()
                Me.SetApplyButton(True)
            End If
        End Using
    End Sub

    Private Sub btnTVShowRegexReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexReset.Click
        If MsgBox(Master.eLang.GetString(844, "Are you sure you want to reset to the default list of show regex?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ShowRegex, True)
            Me.TVShowRegex.Clear()
            Me.TVShowRegex.AddRange(Master.eSettings.TVShowRegexes)
            Me.LoadTVShowRegex()
            Me.SetApplyButton(True)
        End If

    End Sub

    Private Sub btnFileSystemValidExtsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidExtsRemove.Click
        Me.RemoveFileSystemValidExts()
    End Sub

    Private Sub btnFileSystemValidThemeExtsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidThemeExtsRemove.Click
        Me.RemoveFileSystemValidThemeExts()
    End Sub

    Private Sub btnTVEpisodeFilterRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterRemove.Click
        Me.RemoveTVEpisodeFilter()
    End Sub

    Private Sub btnMovieFilterRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterRemove.Click
        Me.RemoveMovieFilter()
    End Sub

    Private Sub btnMovieScraperDefFIExtRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieScraperDefFIExtRemove.Click
        Me.RemoveMovieMetaData()
    End Sub

    Private Sub btnFileSystemNoStackExtsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemNoStackExtsRemove.Click
        Me.RemoveFileSystemNoStackExts()
    End Sub

    Private Sub btnTVShowFilterRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterRemove.Click
        Me.RemoveTVShowFilter()
    End Sub

    Private Sub btnTVShowRegexRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowRegexRemove.Click
        Me.RemoveTVShowRegex()
    End Sub

    Private Sub btnMovieSortTokenRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSortTokenRemove.Click
        Me.RemoveMovieSortToken()
    End Sub

    Private Sub btnTVGeneralLangFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralLangFetch.Click
        Master.eSettings.TVGeneralLanguages.Clear()
        Master.eSettings.TVGeneralLanguages.AddRange(ModulesManager.Instance.TVGetLangs("thetvdb.com"))
        Me.cbTVGeneralLang.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages Select lLang.LongLang).ToArray)

        If Me.cbTVGeneralLang.Items.Count > 0 Then
            Me.cbTVGeneralLang.Text = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.ShortLang = Master.eSettings.TVGeneralLanguage).LongLang
        End If
    End Sub

    Private Sub btnTVScraperDefFIExtRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVScraperDefFIExtRemove.Click
        Me.RemoveTVMetaData()
    End Sub

    Private Sub btnFileSystemCleanerWhitelistRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemCleanerWhitelistRemove.Click
        If Me.lstFileSystemCleanerWhitelist.Items.Count > 0 AndAlso Me.lstFileSystemCleanerWhitelist.SelectedItems.Count > 0 Then
            While Me.lstFileSystemCleanerWhitelist.SelectedItems.Count > 0
                lstFileSystemCleanerWhitelist.Items.Remove(Me.lstFileSystemCleanerWhitelist.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnRemTVSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTVSource.Click
        Me.RemoveTVSource()
        Master.DB.LoadTVSourcesFromDB()
    End Sub

    Private Sub btnTVShowFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterDown.Click
        Try
            If Me.lstTVShowFilter.Items.Count > 0 AndAlso Not IsNothing(Me.lstTVShowFilter.SelectedItem) AndAlso Me.lstTVShowFilter.SelectedIndex < (Me.lstTVShowFilter.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstTVShowFilter.SelectedIndices(0)
                Me.lstTVShowFilter.Items.Insert(iIndex + 2, Me.lstTVShowFilter.SelectedItems(0))
                Me.lstTVShowFilter.Items.RemoveAt(iIndex)
                Me.lstTVShowFilter.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnTVShowFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterUp.Click
        Try
            If Me.lstTVShowFilter.Items.Count > 0 AndAlso Not IsNothing(Me.lstTVShowFilter.SelectedItem) AndAlso Me.lstTVShowFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstTVShowFilter.SelectedIndices(0)
                Me.lstTVShowFilter.Items.Insert(iIndex - 1, Me.lstTVShowFilter.SelectedItems(0))
                Me.lstTVShowFilter.Items.RemoveAt(iIndex + 1)
                Me.lstTVShowFilter.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnMovieFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterUp.Click
        Try
            If Me.lstMovieFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstMovieFilters.SelectedItem) AndAlso Me.lstMovieFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstMovieFilters.SelectedIndices(0)
                Me.lstMovieFilters.Items.Insert(iIndex - 1, Me.lstMovieFilters.SelectedItems(0))
                Me.lstMovieFilters.Items.RemoveAt(iIndex + 1)
                Me.lstMovieFilters.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub cbMovieScraperCertLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieScraperCertLang.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieEFanartsPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieEFanartsPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieEThumbsPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieEThumbsPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieFanartPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieFanartPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieScraperForceTitle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieScraperForceTitle.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbGeneralLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGeneralLanguage.SelectedIndexChanged
        Me.SetApplyButton(True)
        If Not Me.cbGeneralLanguage.SelectedItem.ToString = Master.eSettings.GeneralLanguage Then
            Handle_SetupNeedsRestart()
        End If
    End Sub

    Private Sub cbMovieLanguageOverlay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieLanguageOverlay.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbGeneralMovieTheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGeneralMovieTheme.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVGeneralLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVGeneralLang.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVScraperOptionsOrdering_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVScraperOptionsOrdering.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVEpisodeRetrieve_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVEpisodeRetrieve.SelectedIndexChanged
        Me.ValidateRegex()
    End Sub

    Private Sub cbTVSeasonRetrieve_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVSeasonRetrieve.SelectedIndexChanged
        Me.ValidateRegex()
    End Sub

    Private Sub cbTVLanguageOverlay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVLanguageOverlay.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVScraperUpdateTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVScraperUpdateTime.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieBannerPrefType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieBannerPrefType.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMoviePosterPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMoviePosterPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVScraperRatingRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVScraperRatingRegion.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVSeasonBannerPrefType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVSeasonBannerPrefType.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVSeasonFanartPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVSeasonFanartPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVSeasonPosterPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVSeasonPosterPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVShowFanartPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVShowFanartPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieTrailerMinQual_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTrailerMinQual.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieTrailerPrefQual_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTrailerPrefQual.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbGeneralTVEpisodeTheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGeneralTVEpisodeTheme.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbGeneralTVShowTheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGeneralTVShowTheme.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVASBannerPrefType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVASBannerPrefType.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVASFanartPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVASFanartPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVASPosterPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVASPosterPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVEpisodeFanartPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVEpisodeFanartPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkMovieClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClickScrape.CheckedChanged
        chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkMovieClickScrapeAsk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClickScrapeAsk.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieBackdropsAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieBackdropsAuto.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtMovieBackdropsPath.Enabled = chkMovieBackdropsAuto.Checked
        Me.btnMovieBackdropsBrowse.Enabled = chkMovieBackdropsAuto.Checked
    End Sub

    Private Sub chkMovieScraperCastWithImg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCastWithImg.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCast.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperFullCast.Enabled = Me.chkMovieScraperCast.Checked
        Me.chkMovieScraperCastWithImg.Enabled = Me.chkMovieScraperCast.Checked
        Me.txtMovieScraperCastLimit.Enabled = Me.chkMovieScraperCast.Checked

        If Not chkMovieScraperCast.Checked Then
            Me.chkMovieScraperFullCast.Checked = False
            Me.chkMovieScraperCastWithImg.Checked = False
            Me.txtMovieScraperCastLimit.Text = "0"
        End If
    End Sub

    Private Sub chkMovieScraperCertification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCertification.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCertLang_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCertLang.CheckedChanged
        Me.cbMovieScraperCertLang.SelectedIndex = -1
        Me.cbMovieScraperCertLang.Enabled = Me.chkMovieScraperCertLang.Checked
        Me.chkMovieScraperCertForMPAA.Enabled = Me.chkMovieScraperCertLang.Checked
        If Not Me.chkMovieScraperCertLang.Checked Then
            Me.chkMovieScraperCertForMPAA.Checked = False

            Me.chkMovieScraperOnlyValueForMPAA.Checked = False
            Me.chkMovieScraperOnlyValueForMPAA.Enabled = False
            Me.chkMovieScraperUseMPAAFSK.Checked = False
            Me.chkMovieScraperUseMPAAFSK.Enabled = False
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLevTolerance_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLevTolerance.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtMovieLevTolerance.Enabled = Me.chkMovieLevTolerance.Checked
        If Not Me.chkMovieLevTolerance.Checked Then Me.txtMovieLevTolerance.Text = String.Empty
    End Sub

    Private Sub chkMovieCleanDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieCleanDB.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanDotFanartJPG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanDotFanartJPG.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanExtrathumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanExtrathumbs.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanFanartJPG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanFanartJPG.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanFolderJPG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanFolderJPG.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanMovieFanartJPG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanMovieFanartJPG.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanMovieJPG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanMovieJPG.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanMovieNameJPG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanMovieNameJPG.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanMovieNFOb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanMovieNFOb.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanMovieNFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanMovieNFO.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanMovieTBNb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanMovieTBNb.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanMovieTBN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanMovieTBN.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanPosterJPG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanPosterJPG.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCleanPosterTBN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanPosterTBN.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCrew.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperDirector.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVGeneralDisplayASPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVGeneralDisplayASPoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVDisplayMissingEpisodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVDisplayMissingEpisodes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieDisplayYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieDisplayYear.CheckedChanged
        Me.sResult.NeedsRefresh = True
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieThemeEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeEnable.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieThemeOverwrite.Enabled = Me.chkMovieThemeEnable.Checked

        If Not Me.chkMovieThemeEnable.Checked Then
            Me.chkMovieThemeOverwrite.Checked = False
        End If
    End Sub

    Private Sub chkMovieTrailerEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieTrailerEnable.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieTrailerOverwrite.Enabled = Me.chkMovieTrailerEnable.Checked
        Me.chkMovieTrailerDeleteExisting.Enabled = Me.chkMovieTrailerEnable.Checked

        If Not Me.chkMovieTrailerEnable.Checked Then
            Me.chkMovieTrailerOverwrite.Checked = False
            Me.chkMovieTrailerDeleteExisting.Checked = False
            Me.cbMovieTrailerMinQual.Enabled = False
            Me.cbMovieTrailerPrefQual.Enabled = False
        Else
            Me.cbMovieTrailerMinQual.Enabled = True
            Me.cbMovieTrailerPrefQual.Enabled = True
        End If
    End Sub

    Private Sub chkProxyCredsEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProxyCredsEnable.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtProxyUsername.Enabled = Me.chkProxyCredsEnable.Checked
        Me.txtProxyPassword.Enabled = Me.chkProxyCredsEnable.Checked
        Me.txtProxyDomain.Enabled = Me.chkProxyCredsEnable.Checked

        If Not Me.chkProxyCredsEnable.Checked Then
            Me.txtProxyUsername.Text = String.Empty
            Me.txtProxyPassword.Text = String.Empty
            Me.txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkProxyEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProxyEnable.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtProxyURI.Enabled = Me.chkProxyEnable.Checked
        Me.txtProxyPort.Enabled = Me.chkProxyEnable.Checked
        Me.gbProxyCredsOpts.Enabled = Me.chkProxyEnable.Checked

        If Not Me.chkProxyEnable.Checked Then
            Me.txtProxyURI.Text = String.Empty
            Me.txtProxyPort.Text = String.Empty
            Me.chkProxyCredsEnable.Checked = False
            Me.txtProxyUsername.Text = String.Empty
            Me.txtProxyPassword.Text = String.Empty
            Me.txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodeFanartCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeFanartCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVEpisodeNfoCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeNfoCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVEpisodePosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodePosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockEpisodePlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodePlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockEpisodeRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodeRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockEpisodeTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodeTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVEpisodeProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh = True
    End Sub

    Private Sub chkMovieBannerPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieBannerPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMoviePosterPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviePosterPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieEFanartsPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEFanartsPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieEThumbsPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEThumbsPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieFanartPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieFanartPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperForceTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperForceTitle.CheckedChanged
        Me.cbMovieScraperForceTitle.SelectedIndex = -1
        Me.cbMovieScraperForceTitle.Enabled = Me.chkMovieScraperForceTitle.Checked
        Me.chkMovieScraperTitleFallback.Enabled = Me.chkMovieScraperForceTitle.Checked
        If Me.chkMovieScraperForceTitle.Checked = False Then
            Me.chkMovieScraperTitleFallback.Checked = False
            Me.chkMovieScraperTitleFallback.Enabled = False
        Else
            Me.chkMovieScraperTitleFallback.Enabled = True
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperTitleFallback_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperTitleFallback.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperFullCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperFullCast.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperFullCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperFullCrew.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperProducers.Enabled = Me.chkMovieScraperFullCrew.Checked
        Me.chkMovieScraperMusicBy.Enabled = Me.chkMovieScraperFullCrew.Checked
        Me.chkMovieScraperCrew.Enabled = Me.chkMovieScraperFullCrew.Checked

        If Not Me.chkMovieScraperFullCrew.Checked Then
            Me.chkMovieScraperProducers.Checked = False
            Me.chkMovieScraperMusicBy.Checked = False
            Me.chkMovieScraperCrew.Checked = False
        End If
    End Sub

    Private Sub chkMovieScraperGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperGenre.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieScraperGenreLimit.Enabled = Me.chkMovieScraperGenre.Checked

        If Not Me.chkMovieScraperGenre.Checked Then Me.txtMovieScraperGenreLimit.Text = "0"
    End Sub

    Private Sub chkMovieScraperMetaDataIFOScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperMetaDataIFOScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieGeneralIgnoreLastScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieGeneralIgnoreLastScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralInfoPanelAnim_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralInfoPanelAnim.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockGenre.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockOutline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockTagline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockTrailer.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVGeneralMarkNewEpisodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVGeneralMarkNewEpisodes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVGeneralMarkNewShows_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVGeneralMarkNewShows.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieGeneralMarkNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieGeneralMarkNew.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingBanner_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingBanner.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingClearArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingClearArt.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingClearLogo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingClearLogo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingDiscArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingDiscArt.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingEThumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingEThumbs.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingEFanarts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingEFanarts.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingFanart.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingLandscape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingLandscape.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingNFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingNFO.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingPoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingSubs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingSubs.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingTheme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingTheme.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieMissingTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieMissingTrailer.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieBannerCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieBannerCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieClearArtCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClearArtCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMoviechkMovieClearLogoCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClearLogoCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieDiscArtCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieDiscArtCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieEFanartsCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEFanartsCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieEThumbsCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEThumbsCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieFanartCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieFanartCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLandscapeCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLandscapeCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieNFOCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieNFOCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMoviePosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviePosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSubCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSubCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieThemeCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieTrailerCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieTrailerCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieWatchedCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieWatchedCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperMPAA.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperCertLang.Enabled = Me.chkMovieScraperMPAA.Checked

        If Not Me.chkMovieScraperMPAA.Checked Then
            Me.chkMovieScraperCertLang.Checked = False
            Me.cbMovieScraperCertLang.Enabled = False
            Me.cbMovieScraperCertLang.SelectedIndex = -1
            Me.chkMovieScraperCertForMPAA.Enabled = False
            Me.chkMovieScraperCertForMPAA.Checked = False
        End If
    End Sub

    Private Sub chkMovieScraperMusicBy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperMusicBy.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralHideFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideFanart.CheckedChanged
        Me.SetApplyButton(True)
        If Me.chkGeneralHideFanart.Checked AndAlso Me.chkGeneralHidePoster.Checked AndAlso Me.chkGeneralHideFanartSmall.Checked Then
            Me.chkGeneralShowImgDims.Enabled = False
            Me.chkGeneralShowImgDims.Checked = False
        Else
            Me.chkGeneralShowImgDims.Enabled = True
        End If
    End Sub

    Private Sub chkGeneralHidePoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHidePoster.CheckedChanged
        Me.SetApplyButton(True)
        If Me.chkGeneralHideFanart.Checked AndAlso Me.chkGeneralHidePoster.Checked AndAlso Me.chkGeneralHideFanartSmall.Checked Then
            Me.chkGeneralShowImgDims.Enabled = False
            Me.chkGeneralShowImgDims.Checked = False
        Else
            Me.chkGeneralShowImgDims.Enabled = True
        End If
    End Sub

    Private Sub chkGeneralHideFanartSmall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideFanartSmall.CheckedChanged
        Me.SetApplyButton(True)
        If Me.chkGeneralHideFanart.Checked AndAlso Me.chkGeneralHidePoster.Checked AndAlso Me.chkGeneralHideFanartSmall.Checked Then
            Me.chkGeneralShowImgDims.Enabled = False
            Me.chkGeneralShowImgDims.Checked = False
        Else
            Me.chkGeneralShowImgDims.Enabled = True
        End If
    End Sub

    Private Sub chkTVEpisodeNoFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeNoFilter.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodeProperCase.Enabled = Not Me.chkTVEpisodeNoFilter.Checked
        Me.lstTVEpisodeFilter.Enabled = Not Me.chkTVEpisodeNoFilter.Checked
        Me.txtTVEpisodeFilter.Enabled = Not Me.chkTVEpisodeNoFilter.Checked
        Me.btnTVEpisodeFilterAdd.Enabled = Not Me.chkTVEpisodeNoFilter.Checked
        Me.btnTVEpisodeFilterUp.Enabled = Not Me.chkTVEpisodeNoFilter.Checked
        Me.btnTVEpisodeFilterDown.Enabled = Not Me.chkTVEpisodeNoFilter.Checked
        Me.btnTVEpisodeFilterRemove.Enabled = Not Me.chkTVEpisodeNoFilter.Checked
    End Sub

    Private Sub chkNoSaveImagesToNfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieNoSaveImagesToNfo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperOnlyValueForMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperOnlyValueForMPAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralImagesGlassOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralImagesGlassOverlay.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperUseMPAAFSK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperUseMPAAFSK.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperOutlineForPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperOutlineForPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperOutlinePlotEnglishOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperOutlinePlotEnglishOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperOutline.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperOutlineForPlot.Enabled = Me.chkMovieScraperOutline.Checked
        If Not Me.chkMovieScraperOutline.Checked Then Me.chkMovieScraperOutlineForPlot.Checked = False
    End Sub

    Private Sub chkTVEpisodeFanartOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeFanartOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVEpisodePosterOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodePosterOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralOverwriteNfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralOverwriteNfo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralCreationDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralCreationDate.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieActorThumbsOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieActorThumbsOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieBannerOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieBannerOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieClearArtOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClearArtOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieClearLogoOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClearLogoOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieDiscArtOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieDiscArtOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieEFanartsOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEFanartsOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieEThumbsOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEThumbsOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieFanartOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieFanartOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLandscapeOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLandscapeOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMoviePosterOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviePosterOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVASBannerOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVASBannerOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVASPosterOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVASPosterOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVASFanartOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVASFanartOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVASLandscapeOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVASLandscapeOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowBannerOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowBannerOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowCharacterArtOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowCharacterArtOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowClearArtOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowClearArtOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowClearLogoOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowClearLogoOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowFanartOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowFanartOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowLandscapeOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowLandscapeOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowPosterOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowPosterOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperPlotForOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperPlotForOutline.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieScraperOutlineLimit.Enabled = Me.chkMovieScraperPlotForOutline.Checked
        If Not Me.chkMovieScraperPlotForOutline.Checked Then
            Me.txtMovieScraperOutlineLimit.Enabled = False
            'Me.txtOutlineLimit.Text = "0"
        End If
    End Sub

    Private Sub chkMovieScraperPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperPlot.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperPlotForOutline.Enabled = Me.chkMovieScraperPlot.Checked
        If Not Me.chkMovieScraperPlot.Checked Then
            Me.chkMovieScraperPlotForOutline.Checked = False
            Me.txtMovieScraperOutlineLimit.Enabled = False
        End If
    End Sub

    Private Sub chkMovieScraperProducers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperProducers.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh = True
    End Sub

    Private Sub chkMovieScraperRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperRelease.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVASBannerResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVASBannerResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVASBannerWidth.Enabled = chkTVASBannerResize.Checked
        txtTVASBannerHeight.Enabled = chkTVASBannerResize.Checked

        If Not chkTVASBannerResize.Checked Then
            txtTVASBannerWidth.Text = String.Empty
            txtTVASBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVASFanartResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVASFanartResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVASFanartWidth.Enabled = chkTVASFanartResize.Checked
        txtTVASFanartHeight.Enabled = chkTVASFanartResize.Checked

        If Not chkTVASFanartResize.Checked Then
            txtTVASFanartWidth.Text = String.Empty
            txtTVASFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVASPosterResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVASPosterResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVASPosterWidth.Enabled = chkTVASPosterResize.Checked
        txtTVASPosterHeight.Enabled = chkTVASPosterResize.Checked

        If Not chkTVASPosterResize.Checked Then
            txtTVASPosterWidth.Text = String.Empty
            txtTVASPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodeFanartResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeFanartResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVEpisodeFanartWidth.Enabled = chkTVEpisodeFanartResize.Checked
        txtTVEpisodeFanartHeight.Enabled = chkTVEpisodeFanartResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodeFanartWidth.Text = String.Empty
            txtTVEpisodeFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodePosterResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodePosterResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVEpisodePosterWidth.Enabled = chkTVEpisodePosterResize.Checked
        txtTVEpisodePosterHeight.Enabled = chkTVEpisodePosterResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodePosterWidth.Text = String.Empty
            txtTVEpisodePosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieBannerResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieBannerResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieBannerWidth.Enabled = chkMovieBannerResize.Checked
        txtMovieBannerHeight.Enabled = chkMovieBannerResize.Checked

        If Not chkMovieBannerResize.Checked Then
            txtMovieBannerWidth.Text = String.Empty
            txtMovieBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieEFanartsResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEFanartsResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieEFanartsWidth.Enabled = chkMovieEFanartsResize.Checked
        txtMovieEFanartsHeight.Enabled = chkMovieEFanartsResize.Checked

        If Not chkMovieEFanartsResize.Checked Then
            txtMovieEFanartsWidth.Text = String.Empty
            txtMovieEFanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieEThumbsResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEThumbsResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieEThumbsWidth.Enabled = chkMovieEThumbsResize.Checked
        txtMovieEThumbsHeight.Enabled = chkMovieEThumbsResize.Checked

        If Not chkMovieEThumbsResize.Checked Then
            txtMovieEThumbsWidth.Text = String.Empty
            txtMovieEThumbsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieFanartResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieFanartResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieFanartWidth.Enabled = chkMovieFanartResize.Checked
        txtMovieFanartHeight.Enabled = chkMovieFanartResize.Checked

        If Not chkMovieFanartResize.Checked Then
            txtMovieFanartWidth.Text = String.Empty
            txtMovieFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMoviePosterResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviePosterResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMoviePosterWidth.Enabled = chkMoviePosterResize.Checked
        txtMoviePosterHeight.Enabled = chkMoviePosterResize.Checked

        If Not chkMoviePosterResize.Checked Then
            txtMoviePosterWidth.Text = String.Empty
            txtMoviePosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowbannerResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowBannerResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVShowBannerWidth.Enabled = chkTVShowBannerResize.Checked
        txtTVShowBannerHeight.Enabled = chkTVShowBannerResize.Checked

        If Not chkTVShowBannerResize.Checked Then
            txtTVShowBannerWidth.Text = String.Empty
            txtTVShowBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowFanartResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowFanartResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVShowFanartWidth.Enabled = chkTVShowFanartResize.Checked
        txtTVShowFanartHeight.Enabled = chkTVShowFanartResize.Checked

        If Not chkTVShowFanartResize.Checked Then
            txtTVShowFanartWidth.Text = String.Empty
            txtTVShowFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowPosterResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowPosterResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVShowPosterWidth.Enabled = chkTVShowPosterResize.Checked
        txtTVShowPosterHeight.Enabled = chkTVShowPosterResize.Checked

        If Not chkTVShowPosterResize.Checked Then
            txtTVShowPosterWidth.Text = String.Empty
            txtTVShowPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieScraperRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperRuntime.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperMetaDataScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperMetaDataScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScanOrderModify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScanOrderModify.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeActors_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeActors.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeAired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeAired.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeCredits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeCredits.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeDirector.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeEpisode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeEpisode.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodePlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodePlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeSeason.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowActors_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowActors.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowEpiGuideURL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowEpiGuideURL.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowGenre.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowMPAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowPremiered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowPremiered.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowStatus.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperShowTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonBannerOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonBannerOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonFanartOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonFanartOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonLandscapeOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonLandscapeOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonPosterOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonPosterOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonbannerResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonBannerResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVSeasonBannerWidth.Enabled = chkTVSeasonBannerResize.Checked
        txtTVSeasonBannerHeight.Enabled = chkTVSeasonBannerResize.Checked

        If Not chkTVSeasonBannerResize.Checked Then
            txtTVSeasonBannerWidth.Text = String.Empty
            txtTVSeasonBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonFanartResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonFanartResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVSeasonFanartWidth.Enabled = chkTVSeasonFanartResize.Checked
        txtTVSeasonFanartHeight.Enabled = chkTVSeasonFanartResize.Checked

        If Not chkTVSeasonFanartResize.Checked Then
            txtTVSeasonFanartWidth.Text = String.Empty
            txtTVSeasonFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonPosterResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonPosterResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVSeasonPosterWidth.Enabled = chkTVSeasonPosterResize.Checked
        txtTVSeasonPosterHeight.Enabled = chkTVSeasonPosterResize.Checked

        If Not chkTVSeasonPosterResize.Checked Then
            txtTVSeasonPosterWidth.Text = String.Empty
            txtTVSeasonPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonFanartCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonFanartCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonPosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonPosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralShowImgDims_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralShowImgDims.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowFanartCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowFanartCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralShowGenresText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralShowGenresText.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockShowGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowGenre.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockShowPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockShowRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockShowStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowStatus.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockShowStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockShowTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowNfoCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowNfoCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowPosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowPosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh = True
    End Sub

    Private Sub chkMovieSortBeforeScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSortBeforeScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralSourceFromFolder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralSourceFromFolder.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperTagline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperTop250_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperTop250.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCollection_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCollection.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCountry.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperTrailer.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVCleanDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVCleanDB.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVGeneralIgnoreLastScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVGeneralIgnoreLastScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperMetaDataScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperMetaDataScan.CheckedChanged
        Me.SetApplyButton(True)

        Me.cbTVLanguageOverlay.Enabled = Me.chkTVScraperMetaDataScan.Checked

        If Not Me.chkTVScraperMetaDataScan.Checked Then
            Me.cbTVLanguageOverlay.SelectedIndex = 0
        End If
    End Sub

    Private Sub chkTVScanOrderModify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScanOrderModify.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralCheckUpdates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralCheckUpdates.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCertForMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCertForMPAA.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperOnlyValueForMPAA.Enabled = Me.chkMovieScraperCertForMPAA.Checked
        Me.chkMovieScraperUseMPAAFSK.Enabled = Me.chkMovieScraperCertForMPAA.Checked

        If Not Me.chkMovieScraperCertForMPAA.Checked Then Me.chkMovieScraperOnlyValueForMPAA.Checked = False
        If Not Me.chkMovieScraperCertForMPAA.Checked Then Me.chkMovieScraperUseMPAAFSK.Checked = False
    End Sub

    Private Sub chkMovieUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseBoxee.CheckedChanged
        Me.SetApplyButton(True)

        'Me.chkActorThumbsBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkMovieBannerBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkClearArtBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkClearLogoBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkExtrafanartBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkExtrathumbsBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkDiscArtBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMovieFanartBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkLandscapeBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMovieNFOBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMoviePosterBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkMovieTrailerBoxee.Enabled = Me.chkMovieUseBoxee.Checked

        If Not Me.chkMovieUseBoxee.Checked Then
            ' Me.chkActorThumbsBoxee.Checked = False
            'Me.chkMovieBannerBoxee.Checked = False
            'Me.chkClearArtBoxee.Checked = False
            'Me.chkClearLogoBoxee.Checked = False
            'Me.chkDiscArtBoxee.Checked = False
            'Me.chkExtrafanartBoxee.Checked = False
            'Me.chkExtrathumbsBoxee.Checked = False
            Me.chkMovieFanartBoxee.Checked = False
            'Me.chkLandscapeBoxee.Checked = False
            Me.chkMovieNFOBoxee.Checked = False
            Me.chkMoviePosterBoxee.Checked = False
            'Me.chkMovieTrailerBoxee.Checked = False
        Else
            'Me.chkActorThumbsBoxee.Checked = True
            'Me.chkMovieBannerBoxee.Checked = True
            'Me.chkClearArtBoxee.Checked = True
            'Me.chkClearLogoBoxee.Checked = True
            'Me.chkDiscArtBoxee.Checked = True
            'Me.chkExtrafanartBoxee.Checked = True
            'Me.chkExtrathumbsBoxee.Checked = True
            Me.chkMovieFanartBoxee.Checked = True
            'Me.chkLandscapeBoxee.Checked = True
            Me.chkMovieNFOBoxee.Checked = True
            Me.chkMoviePosterBoxee.Checked = True
            'Me.chkMovieTrailerBoxee.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseFrodo.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieActorThumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieBannerFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearArtFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearLogoFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrafanartsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrathumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieDiscArtFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieFanartFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieLandscapeFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieNFOFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMoviePosterFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieTrailerFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseFrodo.Checked OrElse Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked

        If Not Me.chkMovieUseFrodo.Checked Then
            Me.chkMovieActorThumbsFrodo.Checked = False
            Me.chkMovieBannerFrodo.Checked = False
            Me.chkMovieClearArtFrodo.Checked = False
            Me.chkMovieClearLogoFrodo.Checked = False
            Me.chkMovieDiscArtFrodo.Checked = False
            Me.chkMovieExtrafanartsFrodo.Checked = False
            Me.chkMovieExtrathumbsFrodo.Checked = False
            Me.chkMovieFanartFrodo.Checked = False
            Me.chkMovieLandscapeFrodo.Checked = False
            Me.chkMovieNFOFrodo.Checked = False
            Me.chkMoviePosterFrodo.Checked = False
            Me.chkMovieTrailerFrodo.Checked = False
            Me.chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            Me.chkMovieActorThumbsFrodo.Checked = True
            Me.chkMovieBannerFrodo.Checked = True
            Me.chkMovieClearArtFrodo.Checked = True
            Me.chkMovieClearLogoFrodo.Checked = True
            Me.chkMovieDiscArtFrodo.Checked = True
            Me.chkMovieExtrafanartsFrodo.Checked = True
            Me.chkMovieExtrathumbsFrodo.Checked = True
            Me.chkMovieFanartFrodo.Checked = True
            Me.chkMovieLandscapeFrodo.Checked = True
            Me.chkMovieNFOFrodo.Checked = True
            Me.chkMoviePosterFrodo.Checked = True
            Me.chkMovieTrailerFrodo.Checked = True
        End If

        If Not Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked Then
            Me.chkMovieXBMCThemeEnable.Checked = False
        End If
    End Sub

    Private Sub chkMovieUseEden_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseEden.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieActorThumbsEden.Enabled = Me.chkMovieUseEden.Checked
        'Me.chkBannerEden.Enabled = Me.chkMovieUseEden.Checked
        'Me.chkClearArtEden.Enabled = Me.chkMovieUseEden.Checked
        'Me.chkClearLogoEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieExtrafanartsEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieExtrathumbsEden.Enabled = Me.chkMovieUseEden.Checked
        'Me.chkDiscArtEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieFanartEden.Enabled = Me.chkMovieUseEden.Checked
        'Me.chkLandscapeEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieNFOEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMoviePosterEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieTrailerEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseEden.Checked OrElse Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Not Me.chkMovieUseEden.Checked AndAlso Me.chkMovieUseFrodo.Checked

        If Not Me.chkMovieUseEden.Checked Then
            Me.chkMovieActorThumbsEden.Checked = False
            'Me.chkBannerEden.Checked = False
            'Me.chkClearArtEden.Checked = False
            'Me.chkClearLogoEden.Checked = False
            'Me.chkDiscArtEden.Checked = False
            Me.chkMovieExtrafanartsEden.Checked = False
            Me.chkMovieExtrathumbsEden.Checked = False
            Me.chkMovieFanartEden.Checked = False
            'Me.chkLandscapeEden.Checked = False
            Me.chkMovieNFOEden.Checked = False
            Me.chkMoviePosterEden.Checked = False
            Me.chkMovieTrailerEden.Checked = False
        Else
            Me.chkMovieActorThumbsEden.Checked = True
            'Me.chkBannerEden.Checked = True
            'Me.chkClearArtEden.Checked = True
            'Me.chkClearLogoEden.Checked = True
            'Me.chkDiscArtEden.Checked = True
            Me.chkMovieExtrafanartsEden.Checked = True
            Me.chkMovieExtrathumbsEden.Checked = True
            Me.chkMovieFanartEden.Checked = True
            'Me.chkLandscapeEden.Checked = True
            Me.chkMovieNFOEden.Checked = True
            Me.chkMoviePosterEden.Checked = True
            Me.chkMovieTrailerEden.Checked = True
            Me.chkMovieXBMCProtectVTSBDMV.Checked = False
        End If

        If Not Me.chkMovieUseEden.Checked AndAlso Not Me.chkMovieUseFrodo.Checked Then
            Me.chkMovieXBMCThemeEnable.Checked = False
        End If
    End Sub

    Private Sub chkMovieUseYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseYAMJ.CheckedChanged
        Me.SetApplyButton(True)

        'Me.chkActorThumbsYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieBannerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkClearArtYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkClearLogoYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkExtrafanartYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkExtrathumbsYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkDiscArtYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieFanartYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkLandscapeYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieNFOYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMoviePosterYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieTrailerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieYAMJWatchedFile.Enabled = Me.chkMovieUseYAMJ.Checked

        If Not Me.chkMovieUseYAMJ.Checked Then
            ' Me.chkActorThumbsYAMJ.Checked = False
            Me.chkMovieBannerYAMJ.Checked = False
            'Me.chkClearArtYAMJ.Checked = False
            'Me.chkClearLogoYAMJ.Checked = False
            'Me.chkDiscArtYAMJ.Checked = False
            'Me.chkExtrafanartYAMJ.Checked = False
            'Me.chkExtrathumbsYAMJ.Checked = False
            Me.chkMovieFanartYAMJ.Checked = False
            'Me.chkLandscapeYAMJ.Checked = False
            Me.chkMovieNFOYAMJ.Checked = False
            Me.chkMoviePosterYAMJ.Checked = False
            Me.chkMovieTrailerYAMJ.Checked = False
            Me.chkMovieYAMJWatchedFile.Checked = False
        Else
            'Me.chkActorThumbsYAMJ.Checked = True
            Me.chkMovieBannerYAMJ.Checked = True
            'Me.chkClearArtYAMJ.Checked = True
            'Me.chkClearLogoYAMJ.Checked = True
            'Me.chkDiscArtYAMJ.Checked = True
            'Me.chkExtrafanartYAMJ.Checked = True
            'Me.chkExtrathumbsYAMJ.Checked = True
            Me.chkMovieFanartYAMJ.Checked = True
            'Me.chkLandscapeYAMJ.Checked = True
            Me.chkMovieNFOYAMJ.Checked = True
            Me.chkMoviePosterYAMJ.Checked = True
            Me.chkMovieTrailerYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseNMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseNMJ.CheckedChanged
        Me.SetApplyButton(True)

        'Me.chkActorThumbsNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieBannerNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkClearArtNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkClearLogoNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkExtrafanartNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkExtrathumbsNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkDiscArtNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieFanartNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkLandscapeNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieNFONMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMoviePosterNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieTrailerNMJ.Enabled = Me.chkMovieUseNMJ.Checked

        If Not Me.chkMovieUseNMJ.Checked Then
            ' Me.chkActorThumbsNMJ.Checked = False
            Me.chkMovieBannerNMJ.Checked = False
            'Me.chkClearArtNMJ.Checked = False
            'Me.chkClearLogoNMJ.Checked = False
            'Me.chkDiscArtNMJ.Checked = False
            'Me.chkExtrafanartNMJ.Checked = False
            'Me.chkExtrathumbsNMJ.Checked = False
            Me.chkMovieFanartNMJ.Checked = False
            'Me.chkLandscapeNMJ.Checked = False
            Me.chkMovieNFONMJ.Checked = False
            Me.chkMoviePosterNMJ.Checked = False
            Me.chkMovieTrailerNMJ.Checked = False
        Else
            'Me.chkActorThumbsNMJ.Checked = True
            Me.chkMovieBannerNMJ.Checked = True
            'Me.chkClearArtNMJ.Checked = True
            'Me.chkClearLogoNMJ.Checked = True
            'Me.chkDiscArtNMJ.Checked = True
            'Me.chkExtrafanartNMJ.Checked = True
            'Me.chkExtrathumbsNMJ.Checked = True
            Me.chkMovieFanartNMJ.Checked = True
            'Me.chkLandscapeNMJ.Checked = True
            Me.chkMovieNFONMJ.Checked = True
            Me.chkMoviePosterNMJ.Checked = True
            Me.chkMovieTrailerNMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieScraperUseMDDuration_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperUseMDDuration.CheckedChanged
        Me.txtMovieScraperDurationRuntimeFormat.Enabled = Me.chkMovieScraperUseMDDuration.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperUseMDDuration_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperUseMDDuration.CheckedChanged
        Me.txtTVScraperDurationRuntimeFormat.Enabled = Me.chkTVScraperUseMDDuration.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperVotes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkFileSystemCleanerWhitelist_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileSystemCleanerWhitelist.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperWriters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperWriters.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieXBMCProtectVTSBDMV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCProtectVTSBDMV.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieXBMCThemeCustomPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeCustom.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieXBMCThemeCustomPath.Enabled = Me.chkMovieXBMCThemeCustom.Checked
        Me.btnMovieXBMCThemeCustomPathBrowse.Enabled = Me.chkMovieXBMCThemeCustom.Checked

        If Me.chkMovieXBMCThemeCustom.Checked Then
            Me.chkMovieXBMCThemeMovie.Enabled = False
            Me.chkMovieXBMCThemeMovie.Checked = False
            Me.chkMovieXBMCThemeSub.Enabled = False
            Me.chkMovieXBMCThemeSub.Checked = False
        End If

        If Not Me.chkMovieXBMCThemeCustom.Checked AndAlso Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeMovie.Enabled = True
            Me.chkMovieXBMCThemeSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeEnable.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieXBMCThemeCustom.Enabled = Me.chkMovieXBMCThemeEnable.Checked
        Me.chkMovieXBMCThemeMovie.Enabled = Me.chkMovieXBMCThemeEnable.Checked
        Me.chkMovieXBMCThemeSub.Enabled = Me.chkMovieXBMCThemeEnable.Checked

        If Not Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeCustom.Checked = False
            Me.chkMovieXBMCThemeMovie.Checked = False
            Me.chkMovieXBMCThemeSub.Checked = False
        Else
            Me.chkMovieXBMCThemeMovie.Checked = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeMovie.CheckedChanged
        Me.SetApplyButton(True)

        If Me.chkMovieXBMCThemeMovie.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = False
            Me.chkMovieXBMCThemeCustom.Checked = False
            Me.chkMovieXBMCThemeSub.Enabled = False
            Me.chkMovieXBMCThemeSub.Checked = False
        End If

        If Not Me.chkMovieXBMCThemeMovie.Checked AndAlso Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = True
            Me.chkMovieXBMCThemeSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeSubPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeSub.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieXBMCThemeSubDir.Enabled = Me.chkMovieXBMCThemeSub.Checked

        If Me.chkMovieXBMCThemeSub.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = False
            Me.chkMovieXBMCThemeCustom.Checked = False
            Me.chkMovieXBMCThemeMovie.Enabled = False
            Me.chkMovieXBMCThemeMovie.Checked = False
        End If

        If Not Me.chkMovieXBMCThemeSub.Checked AndAlso Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = True
            Me.chkMovieXBMCThemeMovie.Enabled = True
        End If
    End Sub

    Private Sub txtMovieXBMCThemeCustomPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieXBMCThemeCustomPath.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieXBMCThemeSubDir_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieXBMCThemeSubDir.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieXBMCTrailerFormat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCTrailerFormat.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperYear.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieYAMJWatchedFile.CheckedChanged
        Me.txtMovieYAMJWatchedFolder.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.btnMovieYAMJWatchedFilesBrowse.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVShowBannerPrefType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVShowBannerPrefType.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVShowPosterPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVShowPosterPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub ClearTVRegex()
        Me.btnTVShowRegexAdd.Text = Master.eLang.GetString(115, "Add Regex")
        Me.btnTVShowRegexAdd.Tag = String.Empty
        Me.btnTVShowRegexAdd.Enabled = False
        Me.txtTVSeasonRegex.Text = String.Empty
        Me.cbTVSeasonRetrieve.SelectedIndex = -1
        Me.txtTVEpisodeRegex.Text = String.Empty
        Me.cbTVEpisodeRetrieve.SelectedIndex = -1
    End Sub

    Private Sub dlgSettings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub EditShowRegex(ByVal lItem As ListViewItem)
        Me.btnTVShowRegexAdd.Text = Master.eLang.GetString(124, "Update Regex")
        Me.btnTVShowRegexAdd.Tag = lItem.Text

        Me.txtTVSeasonRegex.Text = lItem.SubItems(1).Text.ToString

        Select Case lItem.SubItems(2).Text
            Case "Folder"
                Me.cbTVSeasonRetrieve.SelectedIndex = 0
            Case "File"
                Me.cbTVSeasonRetrieve.SelectedIndex = 1
        End Select

        Me.txtTVEpisodeRegex.Text = lItem.SubItems(3).Text

        Select Case lItem.SubItems(4).Text
            Case "Folder"
                Me.cbTVEpisodeRetrieve.SelectedIndex = 0
            Case "File"
                Me.cbTVEpisodeRetrieve.SelectedIndex = 1
            Case "Result"
                Me.cbTVEpisodeRetrieve.SelectedIndex = 2
        End Select
    End Sub

    Private Sub FillGenres()
        If Not String.IsNullOrEmpty(Master.eSettings.GenreFilter) Then
            Dim genreArray() As String
            genreArray = Strings.Split(Master.eSettings.GenreFilter, ",")
            For g As Integer = 0 To UBound(genreArray)
                If Me.clbMovieGenre.FindString(Strings.Trim(genreArray(g))) > 0 Then Me.clbMovieGenre.SetItemChecked(Me.clbMovieGenre.FindString(Strings.Trim(genreArray(g))), True)
            Next

            If Me.clbMovieGenre.CheckedItems.Count = 0 Then
                Me.clbMovieGenre.SetItemChecked(0, True)
            End If
        Else
            Me.clbMovieGenre.SetItemChecked(0, True)
        End If
    End Sub

    Private Sub FillList(ByVal sType As String)
        Dim pNode As New TreeNode
        Dim cNode As New TreeNode

        Me.tvSettingsList.Nodes.Clear()
        Me.RemoveCurrPanel()

        For Each pPanel As Containers.SettingsPanel In SettingsPanels.Where(Function(s) s.Type = sType AndAlso String.IsNullOrEmpty(s.Parent)).OrderBy(Function(s) s.Order)
            pNode = New TreeNode(pPanel.Text, pPanel.ImageIndex, pPanel.ImageIndex)
            pNode.Name = pPanel.Name
            For Each cPanel As Containers.SettingsPanel In SettingsPanels.Where(Function(p) p.Type = sType AndAlso p.Parent = pNode.Name).OrderBy(Function(s) s.Order)
                cNode = New TreeNode(cPanel.Text, cPanel.ImageIndex, cPanel.ImageIndex)
                cNode.Name = cPanel.Name
                pNode.Nodes.Add(cNode)
            Next
            Me.tvSettingsList.Nodes.Add(pNode)
        Next

        If Me.tvSettingsList.Nodes.Count > 0 Then
            Me.tvSettingsList.ExpandAll()
            Me.tvSettingsList.SelectedNode = Me.tvSettingsList.Nodes(0)
        Else
            Me.pbSettingsCurrent.Image = Nothing
            Me.lblSettingsCurrent.Text = String.Empty
        End If
    End Sub

    Private Sub FillSettings()
        Try
            With Master.eSettings
                Me.cbGeneralDaemonDrive.SelectedItem = .GeneralDaemonDrive
                Me.cbGeneralLanguage.SelectedItem = .GeneralLanguage
                Me.cbGeneralMovieTheme.SelectedItem = .GeneralMovieTheme
                Me.cbGeneralTVEpisodeTheme.SelectedItem = .GeneralTVEpisodeTheme
                Me.cbGeneralTVShowTheme.SelectedItem = .GeneralTVShowTheme
                Me.cbMovieBannerPrefType.SelectedIndex = .MovieBannerPrefType
                Me.cbMovieEFanartsPrefSize.SelectedIndex = .MovieEFanartsPrefSize
                Me.cbMovieEThumbsPrefSize.SelectedIndex = .MovieEThumbsPrefSize
                Me.cbMovieFanartPrefSize.SelectedIndex = .MovieFanartPrefSize
                Me.cbMovieLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.MovieGeneralFlagLang), Master.eLang.Disabled, .MovieGeneralFlagLang)
                Me.cbMoviePosterPrefSize.SelectedIndex = .MoviePosterPrefSize
                Me.cbMovieTrailerMinQual.SelectedIndex = .MovieTrailerMinQual
                Me.cbMovieTrailerPrefQual.SelectedIndex = .MovieTrailerPrefQual
                Me.cbTVASBannerPrefType.SelectedIndex = .TVASBannerPrefType
                Me.cbTVASFanartPrefSize.SelectedIndex = .TVASFanartPrefSize
                Me.cbTVASPosterPrefSize.SelectedIndex = .TVASPosterPrefSize
                Me.cbTVEpisodeFanartPrefSize.SelectedIndex = .TVEpisodeFanartPrefSize
                Me.cbTVLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.TVGeneralFlagLang), Master.eLang.Disabled, .TVGeneralFlagLang)
                Me.cbTVScraperOptionsOrdering.SelectedIndex = .TVScraperOptionsOrdering
                Me.cbTVScraperRatingRegion.Text = .TVScraperRatingRegion
                Me.cbTVScraperUpdateTime.SelectedIndex = .TVScraperUpdateTime
                Me.cbTVSeasonBannerPrefType.SelectedIndex = .TVSeasonBannerPrefType
                Me.cbTVSeasonFanartPrefSize.SelectedIndex = .TVSeasonFanartPrefSize
                Me.cbTVSeasonPosterPrefSize.SelectedIndex = .TVSeasonPosterPrefSize
                Me.cbTVShowBannerPrefType.SelectedIndex = .TVShowBannerPrefType
                Me.cbTVShowFanartPrefSize.SelectedIndex = .TVShowFanartPrefSize
                Me.cbTVShowPosterPrefSize.SelectedIndex = .TVShowPosterPrefSize
                Me.chkCleanDotFanartJPG.Checked = .CleanDotFanartJPG
                Me.chkCleanExtrathumbs.Checked = .CleanExtrathumbs
                Me.chkCleanFanartJPG.Checked = .CleanFanartJPG
                Me.chkCleanFolderJPG.Checked = .CleanFolderJPG
                Me.chkCleanMovieFanartJPG.Checked = .CleanMovieFanartJPG
                Me.chkCleanMovieJPG.Checked = .CleanMovieJPG
                Me.chkCleanMovieNFO.Checked = .CleanMovieNFO
                Me.chkCleanMovieNFOb.Checked = .CleanMovieNFOB
                Me.chkCleanMovieNameJPG.Checked = .CleanMovieNameJPG
                Me.chkCleanMovieTBN.Checked = .CleanMovieTBN
                Me.chkCleanMovieTBNb.Checked = .CleanMovieTBNB
                Me.chkCleanPosterJPG.Checked = .CleanPosterJPG
                Me.chkCleanPosterTBN.Checked = .CleanPosterTBN
                Me.chkFileSystemCleanerWhitelist.Checked = .FileSystemCleanerWhitelist
                Me.chkGeneralCheckUpdates.Checked = .GeneralCheckUpdates
                Me.chkGeneralCreationDate.Checked = .GeneralCreationDate
                Me.chkGeneralHideFanart.Checked = .GeneralHideFanart
                Me.chkGeneralHideFanartSmall.Checked = .GeneralHideFanartSmall
                Me.chkGeneralHidePoster.Checked = .GeneralHidePoster
                Me.chkGeneralImagesGlassOverlay.Checked = .GeneralImagesGlassOverlay
                Me.chkGeneralInfoPanelAnim.Checked = .GeneralInfoPanelAnim
                Me.chkGeneralOverwriteNfo.Checked = .GeneralOverwriteNfo
                Me.chkGeneralShowGenresText.Checked = .GeneralShowGenresText
                Me.chkGeneralShowImgDims.Checked = .GeneralShowImgDims
                Me.chkGeneralSourceFromFolder.Checked = .GeneralSourceFromFolder
                Me.chkMovieActorThumbsOverwrite.Checked = .MovieActorThumbsOverwrite
                Me.chkMovieBackdropsAuto.Checked = .MovieBackdropsAuto
                Me.chkMovieBannerCol.Checked = .MovieBannerCol
                Me.chkMovieBannerOverwrite.Checked = .MovieBannerOverwrite
                Me.chkMovieBannerPrefOnly.Checked = .MovieBannerPrefOnly
                Me.chkMovieBannerResize.Checked = .MovieBannerResize
                If .MovieBannerResize Then
                    Me.txtMovieBannerHeight.Text = .MovieBannerHeight.ToString
                    Me.txtMovieBannerWidth.Text = .MovieBannerWidth.ToString
                End If
                Me.chkMovieCleanDB.Checked = .MovieCleanDB
                Me.chkMovieClearArtCol.Checked = .MovieClearArtCol
                Me.chkMovieClearArtOverwrite.Checked = .MovieClearArtOverwrite
                Me.chkMovieClearLogoCol.Checked = .MovieClearLogoCol
                Me.chkMovieClearLogoOverwrite.Checked = .MovieClearLogoOverwrite
                Me.chkMovieClickScrape.Checked = .MovieClickScrape
                Me.chkMovieClickScrapeAsk.Checked = .MovieClickScrapeAsk
                Me.chkMovieDiscArtCol.Checked = .MovieDiscArtCol
                Me.chkMovieDiscArtOverwrite.Checked = .MovieDiscArtOverwrite
                Me.chkMovieDisplayYear.Checked = .MovieDisplayYear
                Me.chkMovieEFanartsCol.Checked = .MovieEFanartsCol
                Me.chkMovieEFanartsOverwrite.Checked = .MovieEFanartsOverwrite
                Me.chkMovieEFanartsPrefOnly.Checked = .MovieEFanartsPrefOnly
                Me.chkMovieEFanartsResize.Checked = .MovieEFanartsResize
                If .MovieEFanartsResize Then
                    Me.txtMovieEFanartsHeight.Text = .MovieEFanartsHeight.ToString
                    Me.txtMovieEFanartsWidth.Text = .MovieEFanartsWidth.ToString
                End If
                Me.chkMovieEThumbsCol.Checked = .MovieEThumbsCol
                Me.chkMovieEThumbsOverwrite.Checked = .MovieEThumbsOverwrite
                Me.chkMovieEThumbsPrefOnly.Checked = .MovieEThumbsPrefOnly
                Me.chkMovieEThumbsResize.Checked = .MovieEThumbsResize
                If .MovieEThumbsResize Then
                    Me.txtMovieEThumbsHeight.Text = .MovieEThumbsHeight.ToString
                    Me.txtMovieEThumbsWidth.Text = .MovieEThumbsWidth.ToString
                End If
                Me.chkMovieFanartCol.Checked = .MovieFanartCol
                Me.chkMovieFanartOverwrite.Checked = .MovieFanartOverwrite
                Me.chkMovieFanartPrefOnly.Checked = .MovieFanartPrefOnly
                Me.chkMovieFanartResize.Checked = .MovieFanartResize
                If .MovieFanartResize Then
                    Me.txtMovieFanartHeight.Text = .MovieFanartHeight.ToString
                    Me.txtMovieFanartWidth.Text = .MovieFanartWidth.ToString
                End If
                Me.chkMovieGeneralIgnoreLastScan.Checked = .MovieGeneralIgnoreLastScan
                Me.chkMovieGeneralMarkNew.Checked = .MovieGeneralMarkNew
                Me.chkMovieNFOCol.Checked = .MovieNFOCol
                Me.chkMovieLandscapeCol.Checked = .MovieLandscapeCol
                Me.chkMovieLandscapeOverwrite.Checked = .MovieLandscapeOverwrite
                Me.chkMovieLockGenre.Checked = .MovieLockGenre
                Me.chkMovieLockLanguageA.Checked = .MovieLockLanguageA
                Me.chkMovieLockLanguageV.Checked = .MovieLockLanguageV
                Me.chkMovieLockMPAA.Checked = .MovieLockMPAA
                Me.chkMovieLockOutline.Checked = .MovieLockOutline
                Me.chkMovieLockPlot.Checked = .MovieLockPlot
                Me.chkMovieLockRating.Checked = .MovieLockRating
                Me.chkMovieLockStudio.Checked = .MovieLockStudio
                Me.chkMovieLockTagline.Checked = .MovieLockTagline
                Me.chkMovieLockTitle.Checked = .MovieLockTitle
                Me.chkMovieLockTrailer.Checked = .MovieLockTrailer
                Me.chkMovieMissingBanner.Checked = .MovieMissingBanner
                Me.chkMovieMissingClearArt.Checked = .MovieMissingClearArt
                Me.chkMovieMissingClearLogo.Checked = .MovieMissingClearLogo
                Me.chkMovieMissingDiscArt.Checked = .MovieMissingDiscArt
                Me.chkMovieMissingEFanarts.Checked = .MovieMissingEFanarts
                Me.chkMovieMissingEThumbs.Checked = .MovieMissingEThumbs
                Me.chkMovieMissingFanart.Checked = .MovieMissingFanart
                Me.chkMovieMissingLandscape.Checked = .MovieMissingLandscape
                Me.chkMovieMissingNFO.Checked = .MovieMissingNFO
                Me.chkMovieMissingPoster.Checked = .MovieMissingPoster
                Me.chkMovieMissingSubs.Checked = .MovieMissingSubs
                Me.chkMovieMissingTheme.Checked = .MovieMissingTheme
                Me.chkMovieMissingTrailer.Checked = .MovieMissingTrailer
                Me.chkMovieNoSaveImagesToNfo.Checked = .MovieNoSaveImagesToNfo
                Me.chkMoviePosterCol.Checked = .MoviePosterCol
                Me.chkMoviePosterOverwrite.Checked = .MoviePosterOverwrite
                Me.chkMoviePosterPrefOnly.Checked = .MoviePosterPrefOnly
                Me.chkMoviePosterResize.Checked = .MoviePosterResize
                If .MoviePosterResize Then
                    Me.txtMoviePosterHeight.Text = .MoviePosterHeight.ToString
                    Me.txtMoviePosterWidth.Text = .MoviePosterWidth.ToString
                End If
                Me.chkMovieProperCase.Checked = .MovieProperCase
                Me.chkMovieScanOrderModify.Checked = .MovieScanOrderModify
                Me.chkMovieScraperCast.Checked = .MovieScraperCast
                Me.chkMovieScraperCastWithImg.Checked = .MovieScraperCastWithImgOnly
                Me.chkMovieScraperCertification.Checked = .MovieScraperCertification
                Me.chkMovieScraperCollection.Checked = .MovieScraperCollection
                Me.chkMovieScraperCountry.Checked = .MovieScraperCountry
                Me.chkMovieScraperDirector.Checked = .MovieScraperDirector
                Me.chkMovieScraperFullCast.Checked = .MovieScraperFullCast
                Me.chkMovieScraperFullCrew.Checked = .MovieScraperFullCrew
                Me.chkMovieScraperGenre.Checked = .MovieScraperGenre
                Me.chkMovieScraperMetaDataIFOScan.Checked = .MovieScraperMetaDataIFOScan
                Me.chkMovieScraperMetaDataScan.Checked = .MovieScraperMetaDataScan
                Me.chkMovieScraperMPAA.Checked = .MovieScraperMPAA
                Me.chkMovieScraperOnlyValueForMPAA.Checked = .MovieScraperOnlyValueForMPAA
                Me.chkMovieScraperOutline.Checked = .MovieScraperOutline
                Me.chkMovieScraperOutlineForPlot.Checked = .MovieScraperOutlineForPlot
                Me.chkMovieScraperOutlinePlotEnglishOverwrite.Checked = .MovieScraperOutlinePlotEnglishOverwrite
                Me.chkMovieScraperPlot.Checked = .MovieScraperPlot
                Me.chkMovieScraperPlotForOutline.Checked = .MovieScraperPlotForOutline
                Me.chkMovieScraperRating.Checked = .MovieScraperRating
                Me.chkMovieScraperRelease.Checked = .MovieScraperRelease
                Me.chkMovieScraperRuntime.Checked = .MovieScraperRuntime
                Me.chkMovieScraperStudio.Checked = .MovieScraperStudio
                Me.chkMovieScraperTagline.Checked = .MovieScraperTagline
                Me.chkMovieScraperTitle.Checked = .MovieScraperTitle
                Me.chkMovieScraperTitleFallback.Checked = .MovieScraperTitleFallback
                Me.chkMovieScraperTop250.Checked = .MovieScraperTop250
                Me.chkMovieScraperTrailer.Checked = .MovieScraperTrailer
                Me.chkMovieScraperUseMDDuration.Checked = .MovieScraperUseMDDuration
                Me.chkMovieScraperUseMPAAFSK.Checked = .MovieScraperUseMPAAFSK
                Me.chkMovieScraperVotes.Checked = .MovieScraperVotes
                Me.chkMovieScraperWriters.Checked = .MovieScraperWriters
                Me.chkMovieScraperYear.Checked = .MovieScraperYear
                Me.chkMovieSkipStackedSizeCheck.Checked = .MovieSkipStackedSizeCheck
                Me.chkMovieSortBeforeScan.Checked = .MovieSortBeforeScan
                Me.chkMovieSubCol.Checked = .MovieSubCol
                Me.chkMovieThemeCol.Checked = .MovieThemeCol
                Me.chkMovieThemeEnable.Checked = .MovieThemeEnable
                Me.chkMovieThemeOverwrite.Checked = .MovieThemeOverwrite
                Me.chkMovieTrailerCol.Checked = .MovieTrailerCol
                Me.chkMovieTrailerDeleteExisting.Checked = .MovieTrailerDeleteExisting
                Me.chkMovieTrailerEnable.Checked = .MovieTrailerEnable
                Me.chkMovieTrailerOverwrite.Checked = .MovieTrailerOverwrite
                Me.chkMovieWatchedCol.Checked = .MovieWatchedCol
                Me.chkTVASBannerOverwrite.Checked = .TVASBannerOverwrite
                Me.chkTVASBannerResize.Checked = .TVASBannerResize
                If .TVASBannerResize Then
                    Me.txtTVASBannerHeight.Text = .TVASBannerHeight.ToString
                    Me.txtTVASBannerWidth.Text = .TVASBannerWidth.ToString
                End If
                Me.chkTVASFanartOverwrite.Checked = .TVASFanartOverwrite
                Me.chkTVASFanartResize.Checked = .TVASFanartResize
                If .TVASFanartResize Then
                    Me.txtTVASFanartHeight.Text = .TVASFanartHeight.ToString
                    Me.txtTVASFanartWidth.Text = .TVASFanartWidth.ToString
                End If
                Me.chkTVASLandscapeOverwrite.Checked = .TVASLandscapeOverwrite
                Me.chkTVASPosterOverwrite.Checked = .TVASPosterOverwrite
                Me.chkTVASPosterResize.Checked = .TVASPosterResize
                If .TVASPosterResize Then
                    Me.txtTVASPosterHeight.Text = .TVASPosterHeight.ToString
                    Me.txtTVASPosterWidth.Text = .TVASPosterWidth.ToString
                End If
                Me.chkTVCleanDB.Checked = .TVCleanDB
                Me.chkTVDisplayMissingEpisodes.Checked = .TVDisplayMissingEpisodes
                Me.chkTVEpisodeFanartCol.Checked = .TVEpisodeFanartCol
                Me.chkTVEpisodeFanartOverwrite.Checked = .TVEpisodeFanartOverwrite
                Me.chkTVEpisodeFanartResize.Checked = .TVEpisodeFanartResize
                If .TVEpisodeFanartResize Then
                    Me.txtTVEpisodeFanartHeight.Text = .TVEpisodeFanartHeight.ToString
                    Me.txtTVEpisodeFanartWidth.Text = .TVEpisodeFanartWidth.ToString
                End If
                Me.chkTVEpisodeNfoCol.Checked = .TVEpisodeNfoCol
                Me.chkTVEpisodeNoFilter.Checked = .TVEpisodeNoFilter
                Me.chkTVEpisodePosterCol.Checked = .TVEpisodePosterCol
                Me.chkTVEpisodePosterOverwrite.Checked = .TVEpisodePosterOverwrite
                Me.chkTVEpisodePosterResize.Checked = .TVEpisodePosterResize
                If .TVEpisodePosterResize Then
                    Me.txtTVEpisodePosterHeight.Text = .TVEpisodePosterHeight.ToString
                    Me.txtTVEpisodePosterWidth.Text = .TVEpisodePosterWidth.ToString
                End If
                Me.chkTVEpisodeProperCase.Checked = .TVEpisodeProperCase
                Me.chkTVGeneralDisplayASPoster.Checked = .TVGeneralDisplayASPoster
                Me.chkTVGeneralMarkNewEpisodes.Checked = .TVGeneralMarkNewEpisodes
                Me.chkTVGeneralMarkNewShows.Checked = .TVGeneralMarkNewShows
                Me.chkTVGeneralIgnoreLastScan.Checked = .TVGeneralIgnoreLastScan
                Me.chkTVLockEpisodePlot.Checked = .TVLockEpisodePlot
                Me.chkTVLockEpisodeRating.Checked = .TVLockEpisodeRating
                Me.chkTVLockEpisodeTitle.Checked = .TVLockEpisodeTitle
                Me.chkTVLockShowGenre.Checked = .TVLockShowGenre
                Me.chkTVLockShowPlot.Checked = .TVLockShowPlot
                Me.chkTVLockShowRating.Checked = .TVLockShowRating
                Me.chkTVLockShowStatus.Checked = .TVLockShowStatus
                Me.chkTVLockShowStudio.Checked = .TVLockShowStudio
                Me.chkTVLockShowTitle.Checked = .TVLockShowTitle
                Me.chkTVScanOrderModify.Checked = .TVScanOrderModify
                Me.chkTVScraperEpisodeActors.Checked = .TVScraperEpisodeActors
                Me.chkTVScraperEpisodeAired.Checked = .TVScraperEpisodeAired
                Me.chkTVScraperEpisodeCredits.Checked = .TVScraperEpisodeCredits
                Me.chkTVScraperEpisodeDirector.Checked = .TVScraperEpisodeDirector
                Me.chkTVScraperEpisodeEpisode.Checked = .TVScraperEpisodeEpisode
                Me.chkTVScraperEpisodePlot.Checked = .TVScraperEpisodePlot
                Me.chkTVScraperEpisodeRating.Checked = .TVScraperEpisodeRating
                Me.chkTVScraperEpisodeSeason.Checked = .TVScraperEpisodeSeason
                Me.chkTVScraperEpisodeTitle.Checked = .TVScraperEpisodeTitle
                Me.chkTVScraperMetaDataScan.Checked = .TVScraperMetaDataScan
                Me.chkTVScraperShowActors.Checked = .TVScraperShowActors
                Me.chkTVScraperShowEpiGuideURL.Checked = .TVScraperShowEpiGuideURL
                Me.chkTVScraperShowGenre.Checked = .TVScraperShowGenre
                Me.chkTVScraperShowMPAA.Checked = .TVScraperShowMPAA
                Me.chkTVScraperShowPlot.Checked = .TVScraperShowPlot
                Me.chkTVScraperShowPremiered.Checked = .TVScraperShowPremiered
                Me.chkTVScraperShowRating.Checked = .TVScraperShowRating
                Me.chkTVScraperShowStatus.Checked = .TVScraperShowStatus
                Me.chkTVScraperShowStudio.Checked = .TVScraperShowStudio
                Me.chkTVScraperShowTitle.Checked = .TVScraperShowTitle
                Me.chkTVScraperUseMDDuration.Checked = .TVScraperUseMDDuration
                Me.chkTVSeasonBannerOverwrite.Checked = .TVSeasonBannerOverwrite
                Me.chkTVSeasonBannerResize.Checked = .TVSeasonBannerResize
                If .TVSeasonBannerResize Then
                    Me.txtTVSeasonBannerHeight.Text = .TVSeasonBannerHeight.ToString
                    Me.txtTVSeasonBannerWidth.Text = .TVSeasonBannerWidth.ToString
                End If
                Me.chkTVSeasonFanartCol.Checked = .TVSeasonFanartCol
                Me.chkTVSeasonFanartOverwrite.Checked = .TVSeasonFanartOverwrite
                Me.chkTVSeasonFanartResize.Checked = .TVSeasonFanartResize
                If .TVSeasonFanartResize Then
                    Me.txtTVSeasonFanartHeight.Text = .TVSeasonFanartHeight.ToString
                    Me.txtTVSeasonFanartWidth.Text = .TVSeasonFanartWidth.ToString
                End If
                Me.chkTVSeasonLandscapeOverwrite.Checked = .TVSeasonLandscapeOverwrite
                Me.chkTVSeasonPosterCol.Checked = .TVSeasonPosterCol
                Me.chkTVSeasonPosterOverwrite.Checked = .TVSeasonPosterOverwrite
                Me.chkTVSeasonPosterResize.Checked = .TVSeasonPosterResize
                If .TVSeasonPosterResize Then
                    Me.txtTVSeasonPosterHeight.Text = .TVSeasonPosterHeight.ToString
                    Me.txtTVSeasonPosterWidth.Text = .TVSeasonPosterWidth.ToString
                End If
                Me.chkTVShowBannerOverwrite.Checked = .TVShowBannerOverwrite
                Me.chkTVShowBannerResize.Checked = .TVShowBannerResize
                If .TVShowBannerResize Then
                    Me.txtTVShowBannerHeight.Text = .TVShowBannerHeight.ToString
                    Me.txtTVShowBannerWidth.Text = .TVShowBannerWidth.ToString
                End If
                Me.chkTVShowCharacterArtOverwrite.Checked = .TVShowCharacterArtOverwrite
                Me.chkTVShowClearArtOverwrite.Checked = .TVShowClearArtOverwrite
                Me.chkTVShowClearLogoOverwrite.Checked = .TVShowClearLogoOverwrite
                Me.chkTVShowFanartCol.Checked = .TVShowFanartCol
                Me.chkTVShowFanartOverwrite.Checked = .TVShowFanartOverwrite
                Me.chkTVShowFanartResize.Checked = .TVShowFanartResize
                If .TVShowFanartResize Then
                    Me.txtTVShowFanartHeight.Text = .TVShowFanartHeight.ToString
                    Me.txtTVShowFanartWidth.Text = .TVShowFanartWidth.ToString
                End If
                Me.chkTVShowLandscapeOverwrite.Checked = .TVShowLandscapeOverwrite
                Me.chkTVShowNfoCol.Checked = .TVShowNfoCol
                Me.chkTVShowPosterCol.Checked = .TVShowPosterCol
                Me.chkTVShowPosterOverwrite.Checked = .TVShowPosterOverwrite
                Me.chkTVShowPosterResize.Checked = .TVShowPosterResize
                If .TVShowPosterResize Then
                    Me.txtTVShowPosterHeight.Text = .TVShowPosterHeight.ToString
                    Me.txtTVShowPosterWidth.Text = .TVShowPosterWidth.ToString
                End If
                Me.chkTVShowProperCase.Checked = .TVShowProperCase
                Me.lstFileSystemCleanerWhitelist.Items.AddRange(.FileSystemCleanerWhitelistExts.ToArray)
                Me.lstFileSystemNoStackExts.Items.AddRange(.FileSystemNoStackExts.ToArray)
                Me.lstMovieSortTokens.Items.AddRange(.MovieSortTokens.ToArray)
                Me.tbMovieBannerQual.Value = .MovieBannerQual
                Me.tbMovieEFanartsQual.Value = .MovieEFanartsQual
                Me.tbMovieEThumbsQual.Value = .MovieEThumbsQual
                Me.tbMovieFanartQual.Value = .MovieFanartQual
                Me.tbMoviePosterQual.Value = .MoviePosterQual
                Me.tbTVASBannerQual.Value = .TVASBannerQual
                Me.tbTVASFanartQual.Value = .TVASFanartQual
                Me.tbTVASPosterQual.Value = .TVASPosterQual
                Me.tbTVEpisodeFanartQual.Value = .TVEpisodeFanartQual
                Me.tbTVEpisodePosterQual.Value = .TVEpisodePosterQual
                Me.tbTVSeasonBannerQual.Value = .TVSeasonBannerQual
                Me.tbTVSeasonFanartQual.Value = .TVSeasonFanartQual
                Me.tbTVSeasonPosterQual.Value = .TVSeasonPosterQual
                Me.tbTVShowBannerQual.Value = .TVShowBannerQual
                Me.tbTVShowFanartQual.Value = .TVShowFanartQual
                Me.tbTVShowPosterQual.Value = .TVShowPosterQual
                Me.tcFileSystemCleaner.SelectedTab = If(.FileSystemExpertCleaner, Me.tpFileSystemCleanerExpert, Me.tpFileSystemCleanerStandard)
                Me.txtGeneralDaemonPath.Text = .GeneralDaemonPath
                Me.txtMovieBackdropsPath.Text = .MovieBackdropsPath
                Me.txtMovieEFanartsLimit.Text = .MovieEFanartsLimit.ToString
                Me.txtMovieEThumbsLimit.Text = .MovieEThumbsLimit.ToString
                Me.txtMovieIMDBURL.Text = .MovieIMDBURL
                Me.txtMovieMoviesetsPath.Text = .MovieMoviesetsPath
                Me.txtMovieScraperCastLimit.Text = .MovieScraperCastLimit.ToString
                Me.txtMovieScraperDurationRuntimeFormat.Text = .MovieScraperDurationRuntimeFormat
                Me.txtMovieScraperGenreLimit.Text = .MovieScraperGenreLimit.ToString
                Me.txtMovieScraperOutlineLimit.Text = .MovieScraperOutlineLimit.ToString
                Me.txtMovieSkipLessThan.Text = .MovieSkipLessThan.ToString
                Me.txtTVScraperDurationRuntimeFormat.Text = .TVScraperDurationRuntimeFormat
                Me.txtTVSkipLessThan.Text = .TVSkipLessThan.ToString


                If Not String.IsNullOrEmpty(.MovieScraperCertLang) Then
                    Me.chkMovieScraperCertLang.Checked = True
                    Me.cbMovieScraperCertLang.Enabled = True
                    Me.cbMovieScraperCertLang.Text = .MovieScraperCertLang
                    Me.chkMovieScraperCertForMPAA.Enabled = True
                    Me.chkMovieScraperCertForMPAA.Checked = .MovieScraperCertForMPAA
                End If

                If Not String.IsNullOrEmpty(.MovieScraperForceTitle) Then
                    Me.chkMovieScraperForceTitle.Checked = True
                    Me.cbMovieScraperForceTitle.Enabled = True
                    Me.chkMovieScraperTitleFallback.Enabled = True
                    Me.cbMovieScraperForceTitle.Text = .MovieScraperForceTitle
                End If

                FillGenres()

                If .MovieLevTolerance > 0 Then
                    Me.chkMovieLevTolerance.Checked = True
                    Me.txtMovieLevTolerance.Enabled = True
                    Me.txtMovieLevTolerance.Text = .MovieLevTolerance.ToString
                End If

                If .MovieScraperFullCrew Then
                    Me.chkMovieScraperCrew.Checked = .MovieScraperCrew
                    Me.chkMovieScraperMusicBy.Checked = .MovieScraperMusicBy
                    Me.chkMovieScraperProducers.Checked = .MovieScraperProducers
                End If

                Me.MovieMeta.AddRange(.MovieMetadataPerFileType)
                Me.LoadMovieMetadata()

                Me.TVMeta.AddRange(.TVMetadataPerFileType)
                Me.LoadTVMetadata()

                Me.TVShowRegex.AddRange(.TVShowRegexes)
                Me.LoadTVShowRegex()

                Me.cbTVGeneralLang.Items.AddRange((From lLang In .TVGeneralLanguages Select lLang.LongLang).ToArray)
                If Me.cbTVGeneralLang.Items.Count > 0 Then
                    Me.cbTVGeneralLang.Text = .TVGeneralLanguages.FirstOrDefault(Function(l) l.ShortLang = .TVGeneralLanguage).LongLang
                End If

                If Not String.IsNullOrEmpty(.ProxyURI) AndAlso .ProxyPort >= 0 Then
                    Me.chkProxyEnable.Checked = True
                    Me.txtProxyURI.Text = .ProxyURI
                    Me.txtProxyPort.Text = .ProxyPort.ToString

                    If Not String.IsNullOrEmpty(.ProxyCreds.UserName) Then
                        Me.chkProxyCredsEnable.Checked = True
                        Me.txtProxyUsername.Text = .ProxyCreds.UserName
                        Me.txtProxyPassword.Text = .ProxyCreds.Password
                        Me.txtProxyDomain.Text = .ProxyCreds.Domain
                    End If
                End If

                Me.btnMovieBackdropsBrowse.Enabled = .MovieBackdropsAuto
                Me.txtMovieBackdropsPath.Enabled = .MovieBackdropsAuto
                Me.chkMovieClickScrapeAsk.Enabled = Me.chkMovieClickScrape.Checked
                Me.txtMovieScraperDurationRuntimeFormat.Enabled = .MovieScraperUseMDDuration
                Me.txtTVScraperDurationRuntimeFormat.Enabled = .TVScraperUseMDDuration

                Me.RefreshMovieSources()
                Me.RefreshTVSources()
                Me.RefreshTVShowFilters()
                Me.RefreshTVEpisodeFilters()
                Me.RefreshMovieFilters()
                Me.RefreshFileSystemValidExts()
                Me.RefreshFileSystemValidThemeExts()

                '***************************************************
                '******************* Movie Part ********************
                '***************************************************

                '*************** XBMC Frodo settings ***************
                Me.chkMovieUseFrodo.Checked = .MovieUseFrodo
                Me.chkMovieActorThumbsFrodo.Checked = .MovieActorThumbsFrodo
                Me.chkMovieBannerFrodo.Checked = .MovieBannerFrodo
                Me.chkMovieClearArtFrodo.Checked = .MovieClearArtFrodo
                Me.chkMovieClearLogoFrodo.Checked = .MovieClearLogoFrodo
                Me.chkMovieDiscArtFrodo.Checked = .MovieDiscArtFrodo
                Me.chkMovieExtrafanartsFrodo.Checked = .MovieExtrafanartsFrodo
                Me.chkMovieExtrathumbsFrodo.Checked = .MovieExtrathumbsFrodo
                Me.chkMovieFanartFrodo.Checked = .MovieFanartFrodo
                Me.chkMovieLandscapeFrodo.Checked = .MovieLandscapeFrodo
                Me.chkMovieNFOFrodo.Checked = .MovieNFOFrodo
                Me.chkMoviePosterFrodo.Checked = .MoviePosterFrodo
                Me.chkMovieTrailerFrodo.Checked = .MovieTrailerFrodo

                '*************** XBMC Eden settings ****************
                Me.chkMovieUseEden.Checked = .MovieUseEden
                Me.chkMovieActorThumbsEden.Checked = .MovieActorThumbsEden
                'Me.chkBannerEden.Checked = .MovieBannerEden
                'Me.chkClearArtEden.Checked = .MovieClearArtEden
                'Me.chkClearLogoEden.Checked = .MovieClearLogoEden
                'Me.chkDiscArtEden.Checked = .MovieDiscArtEden
                Me.chkMovieExtrafanartsEden.Checked = .MovieExtrafanartsEden
                Me.chkMovieExtrathumbsEden.Checked = .MovieExtrathumbsEden
                Me.chkMovieFanartEden.Checked = .MovieFanartEden
                'Me.chkLandscapeEden.Checked = .MovieLandscapeEden
                Me.chkMovieNFOEden.Checked = .MovieNFOEden
                Me.chkMoviePosterEden.Checked = .MoviePosterEden
                Me.chkMovieTrailerEden.Checked = .MovieTrailerEden

                '************* XBMC optional settings **************
                Me.chkMovieXBMCTrailerFormat.Checked = .MovieXBMCTrailerFormat
                Me.chkMovieXBMCProtectVTSBDMV.Checked = .MovieXBMCProtectVTSBDMV

                '*************** XBMC theme settings ***************
                Me.chkMovieXBMCThemeEnable.Checked = .MovieXBMCThemeEnable
                Me.chkMovieXBMCThemeCustom.Checked = .MovieXBMCThemeCustom
                Me.chkMovieXBMCThemeMovie.Checked = .MovieXBMCThemeMovie
                Me.chkMovieXBMCThemeSub.Checked = .MovieXBMCThemeSub
                Me.txtMovieXBMCThemeCustomPath.Text = .MovieXBMCThemeCustomPath
                Me.txtMovieXBMCThemeSubDir.Text = .MovieXBMCThemeSubDir

                '****************** YAMJ settings ******************
                Me.chkMovieUseYAMJ.Checked = .MovieUseYAMJ
                'Me.chkActorThumbsYAMJ.Checked = .MovieActorThumbsYAMJ
                Me.chkMovieBannerYAMJ.Checked = .MovieBannerYAMJ
                'Me.chkClearArtYAMJ.Checked = .MovieClearArtYAMJ
                'Me.chkClearLogoYAMJ.Checked = .MovieClearLogoYAMJ
                'Me.chkDiscArtYAMJ.Checked = .MovieDiscArtYAMJ
                'Me.chkExtrafanartYAMJ.Checked = .MovieExtrafanartYAMJ
                'Me.chkExtrathumbsYAMJ.Checked = .MovieExtrathumbsYAMJ
                Me.chkMovieFanartYAMJ.Checked = .MovieFanartYAMJ
                'Me.chkLandscapeYAMJ.Checked = .MovieLandscapeYAMJ
                Me.chkMovieNFOYAMJ.Checked = .MovieNFOYAMJ
                Me.chkMoviePosterYAMJ.Checked = .MoviePosterYAMJ
                Me.chkMovieTrailerYAMJ.Checked = .MovieTrailerYAMJ

                '****************** NMJ settings ******************
                Me.chkMovieUseNMJ.Checked = .MovieUseNMJ
                'Me.chkActorThumbsNMJ.Checked = .MovieActorThumbsNMJ
                Me.chkMovieBannerNMJ.Checked = .MovieBannerNMJ
                'Me.chkClearArtNMJ.Checked = .MovieClearArtNMJ
                'Me.chkClearLogoNMJ.Checked = .MovieClearLogoNMJ
                'Me.chkDiscArtNMJ.Checked = .MovieDiscArtNMJ
                'Me.chkExtrafanartNMJ.Checked = .MovieExtrafanartNMJ
                'Me.chkExtrathumbsNMJ.Checked = .MovieExtrathumbsNMJ
                Me.chkMovieFanartNMJ.Checked = .MovieFanartNMJ
                'Me.chkLandscapeNMJ.Checked = .MovieLandscapeNMJ
                Me.chkMovieNFONMJ.Checked = .MovieNFONMJ
                Me.chkMoviePosterNMJ.Checked = .MoviePosterNMJ
                Me.chkMovieTrailerNMJ.Checked = .MovieTrailerNMJ

                '************** NMT optional settings **************
                Me.chkMovieYAMJCompatibleSets.Checked = .MovieYAMJCompatibleSets
                Me.chkMovieYAMJWatchedFile.Checked = .MovieYAMJWatchedFile
                Me.txtMovieYAMJWatchedFolder.Text = .MovieYAMJWatchedFolder

                '***************** Boxee settings ******************
                Me.chkMovieUseBoxee.Checked = .MovieUseBoxee
                'Me.chkActorThumbsBoxee.Checked = .MovieActorThumbsBoxee
                'Me.chkMovieBannerBoxee.Checked = .MovieBannerBoxee
                'Me.chkClearArtBoxee.Checked = .MovieClearArtBoxee
                'Me.chkClearLogoBoxee.Checked = .MovieClearLogoBoxee
                'Me.chkDiscArtBoxee.Checked = .MovieDiscArtBoxee
                'Me.chkExtrafanartBoxee.Checked = .MovieExtrafanartBoxee
                'Me.chkExtrathumbsBoxee.Checked = .MovieExtrathumbsBoxee
                Me.chkMovieFanartBoxee.Checked = .MovieFanartBoxee
                'Me.chkLandscapeBoxee.Checked = .MovieLandscapeBoxee
                Me.chkMovieNFOBoxee.Checked = .MovieNFOBoxee
                Me.chkMoviePosterBoxee.Checked = .MoviePosterBoxee
                'Me.chkMovieTrailerBoxee.Checked = .MovieTrailerBoxee

                '***************** Expert settings *****************
                Me.chkMovieUseExpert.Checked = .MovieUseExpert

                '***************** Expert Single *******************
                Me.chkMovieActorThumbsExpertSingle.Checked = .MovieActorThumbsExpertSingle
                Me.txtMovieActorThumbsExtExpertSingle.Text = .MovieActorThumbsExtExpertSingle
                Me.txtMovieBannerExpertSingle.Text = .MovieBannerExpertSingle
                Me.txtMovieClearArtExpertSingle.Text = .MovieClearArtExpertSingle
                Me.txtMovieClearLogoExpertSingle.Text = .MovieClearLogoExpertSingle
                Me.txtMovieDiscArtExpertSingle.Text = .MovieDiscArtExpertSingle
                Me.chkMovieExtrafanartsExpertSingle.Checked = .MovieExtrafanartsExpertSingle
                Me.chkMovieExtrathumbsExpertSingle.Checked = .MovieExtrathumbsExpertSingle
                Me.txtMovieFanartExpertSingle.Text = .MovieFanartExpertSingle
                Me.txtMovieLandscapeExpertSingle.Text = .MovieLandscapeExpertSingle
                Me.txtMovieNFOExpertSingle.Text = .MovieNFOExpertSingle
                Me.txtMoviePosterExpertSingle.Text = .MoviePosterExpertSingle
                Me.chkMovieStackExpertSingle.Checked = .MovieStackExpertSingle
                Me.txtMovieTrailerExpertSingle.Text = .MovieTrailerExpertSingle
                Me.chkMovieUnstackExpertSingle.Checked = .MovieUnstackExpertSingle

                '******************* Expert Multi ******************
                Me.chkMovieActorThumbsExpertMulti.Checked = .MovieActorThumbsExpertMulti
                Me.txtMovieActorThumbsExtExpertMulti.Text = .MovieActorThumbsExtExpertMulti
                Me.txtMovieBannerExpertMulti.Text = .MovieBannerExpertMulti
                Me.txtMovieClearArtExpertMulti.Text = .MovieClearArtExpertMulti
                Me.txtMovieClearLogoExpertMulti.Text = .MovieClearLogoExpertMulti
                Me.txtMovieDiscArtExpertMulti.Text = .MovieDiscArtExpertMulti
                Me.txtMovieFanartExpertMulti.Text = .MovieFanartExpertMulti
                Me.txtMovieLandscapeExpertMulti.Text = .MovieLandscapeExpertMulti
                Me.txtMovieNFOExpertMulti.Text = .MovieNFOExpertMulti
                Me.txtMoviePosterExpertMulti.Text = .MoviePosterExpertMulti
                Me.chkMovieStackExpertMulti.Checked = .MovieStackExpertMulti
                Me.txtMovieTrailerExpertMulti.Text = .MovieTrailerExpertMulti
                Me.chkMovieUnstackExpertMulti.Checked = .MovieUnstackExpertMulti

                '******************* Expert VTS *******************
                Me.chkMovieActorThumbsExpertVTS.Checked = .MovieActorThumbsExpertVTS
                Me.txtMovieActorThumbsExtExpertVTS.Text = .MovieActorThumbsExtExpertVTS
                Me.txtMovieBannerExpertVTS.Text = .MovieBannerExpertVTS
                Me.txtMovieClearArtExpertVTS.Text = .MovieClearArtExpertVTS
                Me.txtMovieClearLogoExpertVTS.Text = .MovieClearLogoExpertVTS
                Me.txtMovieDiscArtExpertVTS.Text = .MovieDiscArtExpertVTS
                Me.chkMovieExtrafanartsExpertVTS.Checked = .MovieExtrafanartsExpertVTS
                Me.chkMovieExtrathumbsExpertVTS.Checked = .MovieExtrathumbsExpertVTS
                Me.txtMovieFanartExpertVTS.Text = .MovieFanartExpertVTS
                Me.txtMovieLandscapeExpertVTS.Text = .MovieLandscapeExpertVTS
                Me.txtMovieNFOExpertVTS.Text = .MovieNFOExpertVTS
                Me.txtMoviePosterExpertVTS.Text = .MoviePosterExpertVTS
                Me.chkMovieRecognizeVTSExpertVTS.Checked = .MovieRecognizeVTSExpertVTS
                Me.txtMovieTrailerExpertVTS.Text = .MovieTrailerExpertVTS
                Me.chkMovieUseBaseDirectoryExpertVTS.Checked = .MovieUseBaseDirectoryExpertVTS

                '******************* Expert BDMV *******************
                Me.chkMovieActorThumbsExpertBDMV.Checked = .MovieActorThumbsExpertBDMV
                Me.txtMovieActorThumbsExtExpertBDMV.Text = .MovieActorThumbsExtExpertBDMV
                Me.txtMovieBannerExpertBDMV.Text = .MovieBannerExpertBDMV
                Me.txtMovieClearArtExpertBDMV.Text = .MovieClearArtExpertBDMV
                Me.txtMovieClearLogoExpertBDMV.Text = .MovieClearLogoExpertBDMV
                Me.txtMovieDiscArtExpertBDMV.Text = .MovieDiscArtExpertBDMV
                Me.chkMovieExtrafanartsExpertBDMV.Checked = .MovieExtrafanartsExpertBDMV
                Me.chkMovieExtrathumbsExpertBDMV.Checked = .MovieExtrathumbsExpertBDMV
                Me.txtMovieFanartExpertBDMV.Text = .MovieFanartExpertBDMV
                Me.txtMovieLandscapeExpertBDMV.Text = .MovieLandscapeExpertBDMV
                Me.txtMovieNFOExpertBDMV.Text = .MovieNFOExpertBDMV
                Me.txtMoviePosterExpertBDMV.Text = .MoviePosterExpertBDMV
                Me.txtMovieTrailerExpertBDMV.Text = .MovieTrailerExpertBDMV
                Me.chkMovieUseBaseDirectoryExpertBDMV.Checked = .MovieUseBaseDirectoryExpertBDMV


                '***************************************************
                '****************** TV Show Part *******************
                '***************************************************

                '*************** XBMC Frodo settings ***************
                Me.chkTVUseFrodo.Checked = .TVUseFrodo
                Me.chkTVEpisodeActorThumbsFrodo.Checked = .TVEpisodeActorThumbsFrodo
                Me.chkTVEpisodePosterFrodo.Checked = .TVEpisodePosterFrodo
                Me.chkTVSeasonBannerFrodo.Checked = .TVSeasonBannerFrodo
                Me.chkTVSeasonFanartFrodo.Checked = .TVSeasonFanartFrodo
                Me.chkTVSeasonPosterFrodo.Checked = .TVSeasonPosterFrodo
                Me.chkTVShowActorThumbsFrodo.Checked = .TVShowActorThumbsFrodo
                Me.chkTVShowBannerFrodo.Checked = .TVShowBannerFrodo
                Me.chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
                Me.chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

                '*************** XBMC Eden settings ****************

                '************* XBMC optional settings **************
                Me.chkTVSeasonLandscapeXBMC.Checked = .TVSeasonLandscapeXBMC
                Me.chkTVShowCharacterArtXBMC.Checked = .TVShowCharacterArtXBMC
                Me.chkTVShowClearArtXBMC.Checked = .TVShowClearArtXBMC
                Me.chkTVShowClearLogoXBMC.Checked = .TVShowClearLogoXBMC
                Me.chkTVShowLandscapeXBMC.Checked = .TVShowLandscapeXBMC
                Me.chkTVShowTVThemeXBMC.Checked = .TVShowTVThemeXBMC
                Me.txtTVShowTVThemeFolderXBMC.Text = .TVShowTVThemeFolderXBMC

                '****************** YAMJ settings ******************
                Me.chkTVUseYAMJ.Checked = .TVUseYAMJ
                Me.chkTVEpisodePosterYAMJ.Checked = .TVEpisodePosterYAMJ
                Me.chkTVSeasonBannerYAMJ.Checked = .TVSeasonBannerYAMJ
                Me.chkTVSeasonFanartYAMJ.Checked = .TVSeasonFanartYAMJ
                Me.chkTVSeasonPosterYAMJ.Checked = .TVSeasonPosterYAMJ
                Me.chkTVShowBannerYAMJ.Checked = .TVShowBannerYAMJ
                Me.chkTVShowFanartYAMJ.Checked = .TVShowFanartYAMJ
                Me.chkTVShowPosterYAMJ.Checked = .TVShowPosterYAMJ

                '****************** NMJ settings *******************

                '************** NMT optional settings **************

                '***************** Boxee settings ******************
                Me.chkTVUseBoxee.Checked = .TVUseBoxee
                Me.chkTVEpisodePosterBoxee.Checked = .TVEpisodePosterBoxee
                Me.chkTVSeasonPosterBoxee.Checked = .TVSeasonPosterBoxee
                Me.chkTVShowBannerBoxee.Checked = .TVShowBannerBoxee
                Me.chkTVShowFanartBoxee.Checked = .TVShowFanartBoxee
                Me.chkTVShowPosterBoxee.Checked = .TVShowPosterBoxee

                '***************** Expert settings *****************

            End With

        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Functions.PNLDoubleBuffer(Me.pnlSettingsMain)
            Me.SetUp()
            Me.AddPanels()
            Me.AddButtons()
            Me.AddHelpHandlers(Me, "Core_")

            Dim iBackground As New Bitmap(Me.pnlSettingsTop.Width, Me.pnlSettingsTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlSettingsTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsTop.ClientRectangle)
                Me.pnlSettingsTop.BackgroundImage = iBackground
            End Using

            iBackground = New Bitmap(Me.pnlSettingsCurrentBGGradient.Width, Me.pnlSettingsCurrentBGGradient.Height)
            Using b As Graphics = Graphics.FromImage(iBackground)
                b.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlSettingsCurrentBGGradient.ClientRectangle, Color.SteelBlue, Color.FromKnownColor(KnownColor.Control), Drawing2D.LinearGradientMode.Horizontal), pnlSettingsCurrentBGGradient.ClientRectangle)
                Me.pnlSettingsCurrentBGGradient.BackgroundImage = iBackground
            End Using

            Me.LoadGenreLangs()
            Me.LoadIntLangs()
            Me.LoadLangs()
            Me.LoadThemes()
            Me.LoadRatingRegions()
            Me.FillSettings()
            Me.lvMovieSources.ListViewItemSorter = New ListViewItemComparer(1)
            Me.lvTVSources.ListViewItemSorter = New ListViewItemComparer(1)
            Me.sResult.NeedsUpdate = False
            Me.sResult.NeedsRefresh = False
            Me.sResult.DidCancel = False
            Me.didApply = False
            Me.NoUpdate = False
            RaiseEvent LoadEnd()
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        Me.SetApplyButton(True)
    End Sub

    Private Sub Handle_ModuleSetupChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer)
        If Name = "!#RELOAD" Then
            Me.FillSettings()
            Return
        End If
        Dim tSetPan As New Containers.SettingsPanel
        Dim oSetPan As New Containers.SettingsPanel
        Me.SuspendLayout()
        tSetPan = SettingsPanels.FirstOrDefault(Function(s) s.Name = Name)

        If Not IsNothing(tSetPan) Then
            tSetPan.ImageIndex = If(State, 9, 10)

            Try
                'If tvSettings.Nodes.Count > 0 AndAlso tvSettings.Nodes(0).TreeView.IsDisposed Then Return 'Dont know yet why we need this. second call to settings will raise Exception with treview been disposed
                Dim t() As TreeNode = tvSettingsList.Nodes.Find(Name, True)

                If t.Count > 0 Then
                    If Not diffOrder = 0 Then
                        Dim p As TreeNode = t(0).Parent
                        Dim i As Integer = t(0).Index
                        If diffOrder < 0 AndAlso Not t(0).PrevNode Is Nothing Then
                            oSetPan = SettingsPanels.FirstOrDefault(Function(s) s.Name = t(0).PrevNode.Name)
                            If Not IsNothing(oSetPan) Then oSetPan.Order = i + (diffOrder * -1)
                        End If
                        If diffOrder > 0 AndAlso Not t(0).NextNode Is Nothing Then
                            oSetPan = SettingsPanels.FirstOrDefault(Function(s) s.Name = t(0).NextNode.Name)
                            If Not IsNothing(oSetPan) Then oSetPan.Order = i + (diffOrder * -1)
                        End If
                        p.Nodes.Remove(t(0))
                        p.Nodes.Insert(i + diffOrder, t(0))
                        t(0).TreeView.SelectedNode = t(0)
                        tSetPan.Order = i + diffOrder
                    End If
                    t(0).ImageIndex = If(State, 9, 10)
                    t(0).SelectedImageIndex = If(State, 9, 10)
                    Me.pbSettingsCurrent.Image = Me.ilSettings.Images(If(State, 9, 10))
                End If

                For Each s As ModulesManager._externalScraperModuleClass_Data In (ModulesManager.Instance.externalDataScrapersModules.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Poster In (ModulesManager.Instance.externalPosterScrapersModules.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Theme In (ModulesManager.Instance.externalThemeScrapersModules.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Trailer In (ModulesManager.Instance.externalTrailerScrapersModules.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalTVScraperModuleClass In ModulesManager.Instance.externalTVScrapersModules.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.ScraperOrder)
                    RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
                Next
                For Each s As ModulesManager._externalTVScraperModuleClass In ModulesManager.Instance.externalTVScrapersModules.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.PostScraperOrder)
                    RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
                Next
                For Each s As ModulesManager._externalTVScraperModuleClass_Theme In (ModulesManager.Instance.externalTVThemeScrapersModules.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
            Catch ex As Exception
                Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
            End Try
        End If
        Me.ResumeLayout()
        Me.SetApplyButton(True)
    End Sub

    Private Sub HelpMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.lblHelp.Text = dHelp.Item(DirectCast(sender, Control).AccessibleDescription)
    End Sub

    Private Sub HelpMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.lblHelp.Text = String.Empty
    End Sub

    Private Sub clbMovieGenre_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbMovieGenre.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbMovieGenre.Items.Count - 1
                Me.clbMovieGenre.SetItemChecked(i, False)
            Next
        Else
            Me.clbMovieGenre.SetItemChecked(0, False)
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub LoadGenreLangs()
        Me.clbMovieGenre.Items.Add(Master.eLang.All)
        Me.clbMovieGenre.Items.AddRange(APIXML.GetGenreList(True))
    End Sub

    Private Sub LoadIntLangs()
        Me.cbGeneralLanguage.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            Try
                alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            Catch
            End Try
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToList)
            Me.cbGeneralLanguage.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub LoadLangs()
        Me.cbMovieLanguageOverlay.Items.Add(Master.eLang.Disabled)
        Me.cbMovieLanguageOverlay.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
        Me.cbTVLanguageOverlay.Items.Add(Master.eLang.Disabled)
        Me.cbTVLanguageOverlay.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
    End Sub

    Private Sub LoadMovieMetadata()
        Me.lstMovieScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In MovieMeta
            Me.lstMovieScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub LoadRatingRegions()
        Me.cbTVScraperRatingRegion.Items.AddRange(APIXML.GetRatingRegions)
    End Sub

    Private Sub LoadTVShowRegex()
        Dim lvItem As ListViewItem
        lvTVShowRegex.Items.Clear()
        For Each rShow As Settings.TVShowRegEx In Me.TVShowRegex
            lvItem = New ListViewItem(rShow.ID.ToString)
            lvItem.SubItems.Add(rShow.SeasonRegex)
            lvItem.SubItems.Add(If(rShow.SeasonFromDirectory, "Folder", "File"))
            lvItem.SubItems.Add(rShow.EpisodeRegex)
            Select Case rShow.EpisodeRetrieve
                Case Settings.EpRetrieve.FromDirectory
                    lvItem.SubItems.Add("Folder")
                Case Settings.EpRetrieve.FromFilename
                    lvItem.SubItems.Add("File")
                Case Settings.EpRetrieve.FromSeasonResult
                    lvItem.SubItems.Add("Result")
            End Select
            Me.lvTVShowRegex.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadThemes()
        Me.cbGeneralMovieTheme.Items.Clear()
        Me.cbGeneralTVShowTheme.Items.Clear()
        Me.cbGeneralTVEpisodeTheme.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Themes")) Then
            Dim mT As New List(Of String)
            Dim sT As New List(Of String)
            Dim eT As New List(Of String)
            Try
                mT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "movie-*.xml"))
            Catch
            End Try
            Me.cbGeneralMovieTheme.Items.AddRange(mT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("movie-", String.Empty)).ToArray)
            Try
                sT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "tvshow-*.xml"))
            Catch
            End Try
            Me.cbGeneralTVShowTheme.Items.AddRange(sT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("tvshow-", String.Empty)).ToArray)
            Try
                eT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "tvep-*.xml"))
            Catch
            End Try
            Me.cbGeneralTVEpisodeTheme.Items.AddRange(eT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("tvep-", String.Empty)).ToArray)
        End If
    End Sub


    Private Sub LoadGenericFanartSizes()
        Dim items As New Dictionary(Of String, Enums.FanartSize)
        items.Add(Master.eLang.GetString(322, "X-Large"), Enums.FanartSize.Xlrg)
        items.Add(Master.eLang.GetString(323, "Large"), Enums.FanartSize.Lrg)
        items.Add(Master.eLang.GetString(324, "Medium"), Enums.FanartSize.Mid)
        items.Add(Master.eLang.GetString(325, "Small"), Enums.FanartSize.Small)
        Me.cbMovieEFanartsPrefSize.DataSource = items.ToList
        Me.cbMovieEFanartsPrefSize.DisplayMember = "Key"
        Me.cbMovieEFanartsPrefSize.ValueMember = "Value"
        Me.cbMovieEThumbsPrefSize.DataSource = items.ToList
        Me.cbMovieEThumbsPrefSize.DisplayMember = "Key"
        Me.cbMovieEThumbsPrefSize.ValueMember = "Value"
        Me.cbMovieFanartPrefSize.DataSource = items.ToList
        Me.cbMovieFanartPrefSize.DisplayMember = "Key"
        Me.cbMovieFanartPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadGenericPosterSizes()
        Dim items As New Dictionary(Of String, Enums.PosterSize)
        items.Add(Master.eLang.GetString(322, "X-Large"), Enums.PosterSize.Xlrg)
        items.Add(Master.eLang.GetString(323, "Large"), Enums.PosterSize.Lrg)
        items.Add(Master.eLang.GetString(324, "Medium"), Enums.PosterSize.Mid)
        items.Add(Master.eLang.GetString(325, "Small"), Enums.PosterSize.Small)
        Me.cbMoviePosterPrefSize.DataSource = items.ToList
        Me.cbMoviePosterPrefSize.DisplayMember = "Key"
        Me.cbMoviePosterPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadMovieBannerTypes()
        Dim items As New Dictionary(Of String, Enums.MovieBannerType)
        items.Add(Master.eLang.GetString(745, "None"), Enums.MovieBannerType.None)
        items.Add(Master.eLang.GetString(746, "Blank"), Enums.MovieBannerType.Blank)
        items.Add(Master.eLang.GetString(747, "Graphical"), Enums.MovieBannerType.Graphical)
        items.Add(Master.eLang.GetString(748, "Text"), Enums.MovieBannerType.Text)
        Me.cbMovieBannerPrefType.DataSource = items.ToList
        Me.cbMovieBannerPrefType.DisplayMember = "Key"
        Me.cbMovieBannerPrefType.ValueMember = "Value"
    End Sub

    Private Sub LoadMovieTrailerQualities()
        Dim items As New Dictionary(Of String, Enums.TrailerQuality)
        items.Add(Master.eLang.GetString(569, "All"), Enums.TrailerQuality.All)
        items.Add("1080p", Enums.TrailerQuality.HD1080p)
        items.Add("720p", Enums.TrailerQuality.HD720p)
        items.Add("480p", Enums.TrailerQuality.HQ480p)
        items.Add("360p", Enums.TrailerQuality.SQ360p)
        items.Add("240p", Enums.TrailerQuality.SQ240p)
        items.Add("144p", Enums.TrailerQuality.SQ144p)
        Me.cbMovieTrailerMinQual.DataSource = items.ToList
        Me.cbMovieTrailerMinQual.DisplayMember = "Key"
        Me.cbMovieTrailerMinQual.ValueMember = "Value"
        Me.cbMovieTrailerPrefQual.DataSource = items.ToList
        Me.cbMovieTrailerPrefQual.DisplayMember = "Key"
        Me.cbMovieTrailerPrefQual.ValueMember = "Value"
    End Sub

    Private Sub LoadTVFanartSizes()
        Dim items As New Dictionary(Of String, Enums.TVFanartSize)
        items.Add("1920x1080", Enums.TVFanartSize.HD1080)
        items.Add("1280x720", Enums.TVFanartSize.HD720)
        Me.cbTVASFanartPrefSize.DataSource = items.ToList
        Me.cbTVASFanartPrefSize.DisplayMember = "Key"
        Me.cbTVASFanartPrefSize.ValueMember = "Value"
        Me.cbTVEpisodeFanartPrefSize.DataSource = items.ToList
        Me.cbTVEpisodeFanartPrefSize.DisplayMember = "Key"
        Me.cbTVEpisodeFanartPrefSize.ValueMember = "Value"
        Me.cbTVSeasonFanartPrefSize.DataSource = items.ToList
        Me.cbTVSeasonFanartPrefSize.DisplayMember = "Key"
        Me.cbTVSeasonFanartPrefSize.ValueMember = "Value"
        Me.cbTVShowFanartPrefSize.DataSource = items.ToList
        Me.cbTVShowFanartPrefSize.DisplayMember = "Key"
        Me.cbTVShowFanartPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVPosterSizes()
        Dim items As New Dictionary(Of String, Enums.TVPosterSize)
        items.Add("680x1000", Enums.TVPosterSize.HD1000)
        Me.cbTVASPosterPrefSize.DataSource = items.ToList
        Me.cbTVASPosterPrefSize.DisplayMember = "Key"
        Me.cbTVASPosterPrefSize.ValueMember = "Value"
        Me.cbTVSeasonPosterPrefSize.DataSource = items.ToList
        Me.cbTVSeasonPosterPrefSize.DisplayMember = "Key"
        Me.cbTVSeasonPosterPrefSize.ValueMember = "Value"
        Me.cbTVShowPosterPrefSize.DataSource = items.ToList
        Me.cbTVShowPosterPrefSize.DisplayMember = "Key"
        Me.cbTVShowPosterPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVSeasonBannerTypes()
        Dim items As New Dictionary(Of String, Enums.TVSeasonBannerType)
        items.Add(Master.eLang.GetString(746, "Blank"), Enums.TVSeasonBannerType.Blank)
        items.Add(Master.eLang.GetString(747, "Graphical"), Enums.TVSeasonBannerType.Graphical)
        items.Add(Master.eLang.GetString(748, "Text"), Enums.TVSeasonBannerType.Text)
        Me.cbTVSeasonBannerPrefType.DataSource = items.ToList
        Me.cbTVSeasonBannerPrefType.DisplayMember = "Key"
        Me.cbTVSeasonBannerPrefType.ValueMember = "Value"
    End Sub

    Private Sub LoadTVShowBannerTypes()
        Dim items As New Dictionary(Of String, Enums.TVShowBannerType)
        items.Add(Master.eLang.GetString(746, "Blank"), Enums.TVShowBannerType.Blank)
        items.Add(Master.eLang.GetString(747, "Graphical"), Enums.TVShowBannerType.Graphical)
        items.Add(Master.eLang.GetString(748, "Text"), Enums.TVShowBannerType.Text)
        Me.cbTVASBannerPrefType.DataSource = items.ToList
        Me.cbTVASBannerPrefType.DisplayMember = "Key"
        Me.cbTVASBannerPrefType.ValueMember = "Value"
        Me.cbTVShowBannerPrefType.DataSource = items.ToList
        Me.cbTVShowBannerPrefType.DisplayMember = "Key"
        Me.cbTVShowBannerPrefType.ValueMember = "Value"
    End Sub

    Private Sub LoadTVMetadata()
        Me.lstTVScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In TVMeta
            Me.lstTVScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub lstTVEpisodeFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstTVEpisodeFilter.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVEpisodeFilter()
    End Sub

    Private Sub lstMovieFilters_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstMovieFilters.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieFilter()
    End Sub

    Private Sub lstMovieScraperDefFIExt_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMovieScraperDefFIExt.DoubleClick
        If Me.lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            Using dEditMeta As New dlgFileInfo
                Dim fi As New MediaInfo.Fileinfo
                For Each x As Settings.MetadataPerType In MovieMeta
                    If x.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                        fi = dEditMeta.ShowDialog(x.MetaData, False)
                        If Not fi Is Nothing Then
                            MovieMeta.Remove(x)
                            Dim m As New Settings.MetadataPerType
                            m.FileType = x.FileType
                            m.MetaData = New MediaInfo.Fileinfo
                            m.MetaData = fi
                            MovieMeta.Add(m)
                            LoadMovieMetadata()
                            Me.SetApplyButton(True)
                        End If
                        Exit For
                    End If
                Next
            End Using
        End If
    End Sub

    Private Sub lstMovieScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstMovieScraperDefFIExt.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieMetaData()
    End Sub

    Private Sub lstMovieScraperDefFIExt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMovieScraperDefFIExt.SelectedIndexChanged
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            btnMovieScraperDefFIExtEdit.Enabled = True
            btnMovieScraperDefFIExtRemove.Enabled = True
            txtMovieScraperDefFIExt.Text = String.Empty
        Else
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub lstFileSystemValidExts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstFileSystemValidExts.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveFileSystemValidExts()
    End Sub

    Private Sub lstFileSystemValidThemeExts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstFileSystemValidThemeExts.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveFileSystemValidThemeExts()
    End Sub

    Private Sub lstFileSystemNoStackExts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstFileSystemNoStackExts.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveFileSystemNoStackExts()
    End Sub

    Private Sub lstTVShowFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstTVShowFilter.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVShowFilter()
    End Sub

    Private Sub lstMovieSortTokens_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstMovieSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieSortToken()
    End Sub

    Private Sub lstTVScraperDefFIExt_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTVScraperDefFIExt.DoubleClick
        If Me.lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            Using dEditMeta As New dlgFileInfo
                Dim fi As New MediaInfo.Fileinfo
                For Each x As Settings.MetadataPerType In TVMeta
                    If x.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                        fi = dEditMeta.ShowDialog(x.MetaData, True)
                        If Not fi Is Nothing Then
                            TVMeta.Remove(x)
                            Dim m As New Settings.MetadataPerType
                            m.FileType = x.FileType
                            m.MetaData = New MediaInfo.Fileinfo
                            m.MetaData = fi
                            TVMeta.Add(m)
                            LoadTVMetadata()
                            Me.SetApplyButton(True)
                        End If
                        Exit For
                    End If
                Next
            End Using
        End If
    End Sub

    Private Sub lstTVScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstTVScraperDefFIExt.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVMetaData()
    End Sub

    Private Sub lstTVScraperDefFIExt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTVScraperDefFIExt.SelectedIndexChanged
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            btnTVScraperDefFIExtEdit.Enabled = True
            btnTVScraperDefFIExtRemove.Enabled = True
            txtTVScraperDefFIExt.Text = String.Empty
        Else
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub lvMovieSources_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvMovieSources.ColumnClick
        Me.lvMovieSources.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvMovieSources_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMovieSources.DoubleClick
        If lvMovieSources.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgMovieSource
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshMovieSources()
                    Me.sResult.NeedsUpdate = True
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub lvMovieSources_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvMovieSources.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieSource()
    End Sub

    Private Sub lvTVShowRegex_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVShowRegex.DoubleClick
        If Me.lvTVShowRegex.SelectedItems.Count > 0 Then Me.EditShowRegex(lvTVShowRegex.SelectedItems(0))
    End Sub

    Private Sub lvTVShowRegex_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvTVShowRegex.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVShowRegex()
    End Sub

    Private Sub lvTVShowRegex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVShowRegex.SelectedIndexChanged
        If Not String.IsNullOrEmpty(Me.btnTVShowRegexAdd.Tag.ToString) Then Me.ClearTVRegex()
    End Sub

    Private Sub lvTVSources_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvTVSources.ColumnClick
        Me.lvTVSources.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvTVSources_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVSources.DoubleClick
        If lvTVSources.SelectedItems.Count > 0 Then
            Using dTVSource As New dlgTVSource
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshTVSources()
                    Me.sResult.NeedsUpdate = True
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub lvTVSources_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvTVSources.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVSource()
    End Sub

    Private Sub RefreshTVEpisodeFilters()
        Me.lstTVEpisodeFilter.Items.Clear()
        Me.lstTVEpisodeFilter.Items.AddRange(Master.eSettings.TVEpisodeFilterCustom.ToArray)
    End Sub

    Private Sub RefreshHelpStrings(ByVal Language As String)
        For Each sKey As String In dHelp.Keys
            dHelp.Item(sKey) = Master.eLang.GetHelpString(sKey)
        Next
    End Sub

    Private Sub RefreshMovieFilters()
        Me.lstMovieFilters.Items.Clear()
        Me.lstMovieFilters.Items.AddRange(Master.eSettings.MovieFilterCustom.ToArray)
    End Sub

    Private Sub RefreshTVShowFilters()
        Me.lstTVShowFilter.Items.Clear()
        Me.lstTVShowFilter.Items.AddRange(Master.eSettings.TVShowFilterCustom.ToArray)
    End Sub

    Private Sub RefreshMovieSources()
        Dim lvItem As ListViewItem

        lvMovieSources.Items.Clear()
        Master.DB.LoadMovieSourcesFromDB()
        For Each s As Structures.MovieSource In Master.MovieSources
            lvItem = New ListViewItem(s.id)
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(If(s.Recursive, "Yes", "No"))
            lvItem.SubItems.Add(If(s.UseFolderName, "Yes", "No"))
            lvItem.SubItems.Add(If(s.IsSingle, "Yes", "No"))
            lvMovieSources.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        Master.DB.LoadTVSourcesFromDB()
        lvTVSources.Items.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT ID, Name, path, LastScan FROM TVSources;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lvItem = New ListViewItem(SQLreader("ID").ToString)
                    lvItem.SubItems.Add(SQLreader("Name").ToString)
                    lvItem.SubItems.Add(SQLreader("Path").ToString)
                    lvTVSources.Items.Add(lvItem)
                End While
            End Using
        End Using
    End Sub

    Private Sub RefreshFileSystemValidExts()
        Me.lstFileSystemValidExts.Items.Clear()
        Me.lstFileSystemValidExts.Items.AddRange(Master.eSettings.FileSystemValidExts.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidThemeExts()
        Me.lstFileSystemValidThemeExts.Items.Clear()
        Me.lstFileSystemValidThemeExts.Items.AddRange(Master.eSettings.FileSystemValidThemeExts.ToArray)
    End Sub

    Private Sub RemoveCurrPanel()
        If Me.pnlSettingsMain.Controls.Count > 0 Then
            Me.pnlSettingsMain.Controls.Remove(Me.currPanel)
        End If
    End Sub

    Private Sub RemoveTVEpisodeFilter()
        If Me.lstTVEpisodeFilter.Items.Count > 0 AndAlso Me.lstTVEpisodeFilter.SelectedItems.Count > 0 Then
            While Me.lstTVEpisodeFilter.SelectedItems.Count > 0
                Me.lstTVEpisodeFilter.Items.Remove(Me.lstTVEpisodeFilter.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsRefresh = True
        End If
    End Sub

    Private Sub RemoveMovieFilter()
        If Me.lstMovieFilters.Items.Count > 0 AndAlso Me.lstMovieFilters.SelectedItems.Count > 0 Then
            While Me.lstMovieFilters.SelectedItems.Count > 0
                Me.lstMovieFilters.Items.Remove(Me.lstMovieFilters.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsRefresh = True
        End If
    End Sub

    Private Sub RemoveMovieMetaData()
        If Me.lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In MovieMeta
                If x.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    MovieMeta.Remove(x)
                    LoadMovieMetadata()
                    Me.SetApplyButton(True)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub RemoveFileSystemValidExts()
        If lstFileSystemValidExts.Items.Count > 0 AndAlso lstFileSystemValidExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemValidExts.SelectedItems.Count > 0
                Me.lstFileSystemValidExts.Items.Remove(Me.lstFileSystemValidExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidThemeExts()
        If lstFileSystemValidThemeExts.Items.Count > 0 AndAlso lstFileSystemValidThemeExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemValidThemeExts.SelectedItems.Count > 0
                Me.lstFileSystemValidThemeExts.Items.Remove(Me.lstFileSystemValidThemeExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If
    End Sub

    Private Sub RemoveMovieSource()
        Try
            If Me.lvMovieSources.SelectedItems.Count > 0 Then
                If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                    Me.lvMovieSources.BeginUpdate()

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                            While Me.lvMovieSources.SelectedItems.Count > 0
                                parSource.Value = lvMovieSources.SelectedItems(0).SubItems(1).Text
                                SQLcommand.CommandText = "SELECT Id FROM movies WHERE source = (?);"
                                Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                                    While SQLReader.Read
                                        Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                    End While
                                End Using
                                SQLcommand.CommandText = String.Concat("DELETE FROM sources WHERE name = (?);")
                                SQLcommand.ExecuteNonQuery()
                                lvMovieSources.Items.Remove(lvMovieSources.SelectedItems(0))
                            End While
                        End Using
                        SQLtransaction.Commit()
                    End Using

                    Me.lvMovieSources.Sort()
                    Me.lvMovieSources.EndUpdate()
                    Me.lvMovieSources.Refresh()

                    Functions.GetListOfSources()

                    Me.SetApplyButton(True)
                    Me.sResult.NeedsUpdate = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RemoveFileSystemNoStackExts()
        If lstFileSystemNoStackExts.Items.Count > 0 AndAlso lstFileSystemNoStackExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemNoStackExts.SelectedItems.Count > 0
                Me.lstFileSystemNoStackExts.Items.Remove(Me.lstFileSystemNoStackExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If
    End Sub

    Private Sub RemoveTVShowRegex()
        Dim ID As Integer
        For Each lItem As ListViewItem In lvTVShowRegex.SelectedItems
            ID = Convert.ToInt32(lItem.Text)
            Dim selRex = From lRegex As Settings.TVShowRegEx In Me.TVShowRegex Where lRegex.ID = ID
            If selRex.Count > 0 Then
                Me.TVShowRegex.Remove(selRex(0))
                Me.SetApplyButton(True)
            End If
        Next
        Me.LoadTVShowRegex()
    End Sub

    Private Sub RemoveTVShowFilter()
        If Me.lstTVShowFilter.Items.Count > 0 AndAlso Me.lstTVShowFilter.SelectedItems.Count > 0 Then
            While Me.lstTVShowFilter.SelectedItems.Count > 0
                Me.lstTVShowFilter.Items.Remove(Me.lstTVShowFilter.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsRefresh = True
        End If
    End Sub

    Private Sub RemoveMovieSortToken()
        If Me.lstMovieSortTokens.Items.Count > 0 AndAlso Me.lstMovieSortTokens.SelectedItems.Count > 0 Then
            While Me.lstMovieSortTokens.SelectedItems.Count > 0
                Me.lstMovieSortTokens.Items.Remove(Me.lstMovieSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsRefresh = True
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveTVMetaData()
        If Me.lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In TVMeta
                If x.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                    TVMeta.Remove(x)
                    LoadTVMetadata()
                    Me.SetApplyButton(True)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub RemoveTVSource()
        Try
            If Me.lvTVSources.SelectedItems.Count > 0 Then
                If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the TV Shows from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                    Me.lvTVSources.BeginUpdate()

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                            While Me.lvTVSources.SelectedItems.Count > 0
                                parSource.Value = lvTVSources.SelectedItems(0).SubItems(1).Text
                                SQLcommand.CommandText = "SELECT Id FROM TVShows WHERE Source = (?);"
                                Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                                    While SQLReader.Read
                                        Master.DB.DeleteTVShowFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                    End While
                                End Using
                                SQLcommand.CommandText = String.Concat("DELETE FROM TVSources WHERE name = (?);")
                                SQLcommand.ExecuteNonQuery()
                                lvTVSources.Items.Remove(lvTVSources.SelectedItems(0))
                            End While
                        End Using
                        SQLtransaction.Commit()
                    End Using

                    Me.lvTVSources.Sort()
                    Me.lvTVSources.EndUpdate()
                    Me.lvTVSources.Refresh()
                    Me.SetApplyButton(True)
                    Me.sResult.NeedsUpdate = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RenumberTVShowRegex()
        For i As Integer = 0 To Me.TVShowRegex.Count - 1
            Me.TVShowRegex(i).ID = i
        Next
    End Sub

    Private Sub SaveSettings(ByVal isApply As Boolean)
        Try
            With Master.eSettings
                .FileSystemNoStackExts.Clear()
                .FileSystemNoStackExts.AddRange(lstFileSystemNoStackExts.Items.OfType(Of String).ToList)
                .FileSystemValidExts.Clear()
                .FileSystemValidExts.AddRange(lstFileSystemValidExts.Items.OfType(Of String).ToList)
                .FileSystemValidThemeExts.Clear()
                .FileSystemValidThemeExts.AddRange(lstFileSystemValidThemeExts.Items.OfType(Of String).ToList)
                .GeneralCheckUpdates = chkGeneralCheckUpdates.Checked
                .GeneralCreationDate = Me.chkGeneralCreationDate.Checked
                .GeneralDaemonDrive = Me.cbGeneralDaemonDrive.Text
                .GeneralDaemonPath = Me.txtGeneralDaemonPath.Text
                .GeneralHideFanart = Me.chkGeneralHideFanart.Checked
                .GeneralHideFanartSmall = Me.chkGeneralHideFanartSmall.Checked
                .GeneralHidePoster = Me.chkGeneralHidePoster.Checked
                .GeneralImagesGlassOverlay = Me.chkGeneralImagesGlassOverlay.Checked
                .GeneralInfoPanelAnim = chkGeneralInfoPanelAnim.Checked
                .GeneralLanguage = Me.cbGeneralLanguage.Text
                .GeneralMovieTheme = Me.cbGeneralMovieTheme.Text
                .GeneralOverwriteNfo = Me.chkGeneralOverwriteNfo.Checked
                .GeneralShowGenresText = Me.chkGeneralShowGenresText.Checked
                .GeneralShowImgDims = Me.chkGeneralShowImgDims.Checked
                .GeneralSourceFromFolder = Me.chkGeneralSourceFromFolder.Checked
                .GeneralTVEpisodeTheme = Me.cbGeneralTVEpisodeTheme.Text
                .GeneralTVShowTheme = Me.cbGeneralTVShowTheme.Text
                .MovieActorThumbsOverwrite = Me.chkMovieActorThumbsOverwrite.Checked
                '.MovieActorThumbsQual = Me.tbMovieActorThumbsQual.value
                .MovieBackdropsPath = Me.txtMovieBackdropsPath.Text
                If Not String.IsNullOrEmpty(Me.txtMovieBackdropsPath.Text) Then
                    .MovieBackdropsAuto = Me.chkMovieBackdropsAuto.Checked
                Else
                    .MovieBackdropsAuto = False
                End If
                .MovieBannerCol = Me.chkMovieBannerCol.Checked
                .MovieBannerHeight = If(Not String.IsNullOrEmpty(Me.txtMovieBannerHeight.Text), Convert.ToInt32(Me.txtMovieBannerHeight.Text), 0)
                .MovieBannerOverwrite = Me.chkMovieBannerOverwrite.Checked
                .MovieBannerPrefOnly = Me.chkMovieBannerPrefOnly.Checked
                .MovieBannerPrefType = DirectCast(Me.cbMovieBannerPrefType.SelectedIndex, Enums.MovieBannerType)
                .MovieBannerQual = Me.tbMovieBannerQual.Value
                .MovieBannerResize = Me.chkMovieBannerResize.Checked
                .MovieBannerWidth = If(Not String.IsNullOrEmpty(Me.txtMovieBannerWidth.Text), Convert.ToInt32(Me.txtMovieBannerWidth.Text), 0)
                .MovieCleanDB = Me.chkMovieCleanDB.Checked
                .MovieClearArtCol = Me.chkMovieClearArtCol.Checked
                .MovieClearArtOverwrite = Me.chkMovieClearArtOverwrite.Checked
                .MovieClearLogoCol = Me.chkMovieClearLogoCol.Checked
                .MovieClearLogoOverwrite = Me.chkMovieClearLogoOverwrite.Checked
                .MovieClickScrape = Me.chkMovieClickScrape.Checked
                .MovieClickScrapeAsk = Me.chkMovieClickScrapeAsk.Checked
                .MovieDiscArtCol = Me.chkMovieDiscArtCol.Checked
                .MovieDiscArtOverwrite = Me.chkMovieDiscArtOverwrite.Checked
                .MovieDisplayYear = Me.chkMovieDisplayYear.Checked
                .MovieEFanartsCol = Me.chkMovieEFanartsCol.Checked
                .MovieEFanartsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsHeight.Text), Convert.ToInt32(Me.txtMovieEFanartsHeight.Text), 0)
                .MovieEFanartsLimit = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsLimit.Text), Convert.ToInt32(Me.txtMovieEFanartsLimit.Text), 0)
                .MovieEFanartsOverwrite = Me.chkMovieEFanartsOverwrite.Checked
                .MovieEFanartsPrefOnly = Me.chkMovieEFanartsPrefOnly.Checked
                .MovieEFanartsPrefSize = DirectCast(Me.cbMovieEFanartsPrefSize.SelectedIndex, Enums.FanartSize)
                .MovieEFanartsQual = Me.tbMovieEFanartsQual.Value
                .MovieEFanartsResize = Me.chkMovieEFanartsResize.Checked
                .MovieEFanartsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsWidth.Text), Convert.ToInt32(Me.txtMovieEFanartsWidth.Text), 0)
                .MovieEThumbsCol = Me.chkMovieEThumbsCol.Checked
                .MovieEThumbsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsHeight.Text), Convert.ToInt32(Me.txtMovieEThumbsHeight.Text), 0)
                .MovieEThumbsLimit = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsLimit.Text), Convert.ToInt32(Me.txtMovieEThumbsLimit.Text), 0)
                .MovieEThumbsOverwrite = Me.chkMovieEThumbsOverwrite.Checked
                .MovieEThumbsPrefOnly = Me.chkMovieEThumbsPrefOnly.Checked
                .MovieEThumbsPrefSize = DirectCast(Me.cbMovieEThumbsPrefSize.SelectedIndex, Enums.FanartSize)
                .MovieEThumbsQual = Me.tbMovieEThumbsQual.Value
                .MovieEThumbsResize = Me.chkMovieEThumbsResize.Checked
                .MovieEThumbsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsWidth.Text), Convert.ToInt32(Me.txtMovieEThumbsWidth.Text), 0)
                .MovieFanartCol = Me.chkMovieFanartCol.Checked
                .MovieFanartHeight = If(Not String.IsNullOrEmpty(Me.txtMovieFanartHeight.Text), Convert.ToInt32(Me.txtMovieFanartHeight.Text), 0)
                .MovieFanartOverwrite = Me.chkMovieFanartOverwrite.Checked
                .MovieFanartPrefOnly = Me.chkMovieFanartPrefOnly.Checked
                .MovieFanartPrefSize = DirectCast(Me.cbMovieFanartPrefSize.SelectedIndex, Enums.FanartSize)
                .MovieFanartQual = Me.tbMovieFanartQual.Value
                .MovieFanartResize = Me.chkMovieFanartResize.Checked
                .MovieFanartWidth = If(Not String.IsNullOrEmpty(Me.txtMovieFanartWidth.Text), Convert.ToInt32(Me.txtMovieFanartWidth.Text), 0)
                .MovieFilterCustom.Clear()
                .MovieFilterCustom.AddRange(Me.lstMovieFilters.Items.OfType(Of String).ToList)
                If .MovieFilterCustom.Count <= 0 Then .MovieFilterCustomIsEmpty = True
                .MovieGeneralFlagLang = If(Me.cbMovieLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, Me.cbMovieLanguageOverlay.Text)
                .MovieGeneralIgnoreLastScan = Me.chkMovieGeneralIgnoreLastScan.Checked
                .MovieGeneralMarkNew = Me.chkMovieGeneralMarkNew.Checked
                If Not String.IsNullOrEmpty(Me.txtMovieIMDBURL.Text) Then
                    .MovieIMDBURL = Strings.Replace(Me.txtMovieIMDBURL.Text, "http://", String.Empty)
                Else
                    .MovieIMDBURL = "akas.imdb.com"
                End If
                .MovieNFOCol = Me.chkMovieNFOCol.Checked
                .MovieLandscapeCol = Me.chkMovieLandscapeCol.Checked
                .MovieLandscapeOverwrite = Me.chkMovieLandscapeOverwrite.Checked
                .MovieLevTolerance = If(Not String.IsNullOrEmpty(Me.txtMovieLevTolerance.Text), Convert.ToInt32(Me.txtMovieLevTolerance.Text), 0)
                .MovieLockGenre = Me.chkMovieLockGenre.Checked
                .MovieLockLanguageA = Me.chkMovieLockLanguageA.Checked
                .MovieLockLanguageV = Me.chkMovieLockLanguageV.Checked
                .MovieLockMPAA = Me.chkMovieLockMPAA.Checked
                .MovieLockOutline = Me.chkMovieLockOutline.Checked
                .MovieLockPlot = Me.chkMovieLockPlot.Checked
                .MovieLockRating = Me.chkMovieLockRating.Checked
                .MovieLockStudio = Me.chkMovieLockStudio.Checked
                .MovieLockTagline = Me.chkMovieLockTagline.Checked
                .MovieLockTitle = Me.chkMovieLockTitle.Checked
                .MovieLockTrailer = Me.chkMovieLockTrailer.Checked
                .MovieMetadataPerFileType.Clear()
                .MovieMetadataPerFileType.AddRange(Me.MovieMeta)
                .MovieMissingBanner = Me.chkMovieMissingBanner.Checked
                .MovieMissingClearArt = Me.chkMovieMissingClearArt.Checked
                .MovieMissingClearLogo = Me.chkMovieMissingClearLogo.Checked
                .MovieMissingDiscArt = Me.chkMovieMissingDiscArt.Checked
                .MovieMissingEFanarts = Me.chkMovieMissingEFanarts.Checked
                .MovieMissingEThumbs = Me.chkMovieMissingEThumbs.Checked
                .MovieMissingFanart = Me.chkMovieMissingFanart.Checked
                .MovieMissingLandscape = Me.chkMovieMissingLandscape.Checked
                .MovieMissingNFO = Me.chkMovieMissingNFO.Checked
                .MovieMissingPoster = Me.chkMovieMissingPoster.Checked
                .MovieMissingSubs = Me.chkMovieMissingSubs.Checked
                .MovieMissingTheme = Me.chkMovieMissingTheme.Checked
                .MovieMissingTrailer = Me.chkMovieMissingTrailer.Checked
                .MovieMoviesetsPath = Me.txtMovieMoviesetsPath.Text
                .MovieNoSaveImagesToNfo = Me.chkMovieNoSaveImagesToNfo.Checked
                .MoviePosterCol = Me.chkMoviePosterCol.Checked
                .MoviePosterHeight = If(Not String.IsNullOrEmpty(Me.txtMoviePosterHeight.Text), Convert.ToInt32(Me.txtMoviePosterHeight.Text), 0)
                .MoviePosterOverwrite = Me.chkMoviePosterOverwrite.Checked
                .MoviePosterPrefOnly = Me.chkMoviePosterPrefOnly.Checked
                .MoviePosterPrefSize = DirectCast(Me.cbMoviePosterPrefSize.SelectedIndex, Enums.PosterSize)
                .MoviePosterQual = Me.tbMoviePosterQual.Value
                .MoviePosterResize = Me.chkMoviePosterResize.Checked
                .MoviePosterWidth = If(Not String.IsNullOrEmpty(Me.txtMoviePosterWidth.Text), Convert.ToInt32(Me.txtMoviePosterWidth.Text), 0)
                .MovieProperCase = Me.chkMovieProperCase.Checked
                .MovieScanOrderModify = Me.chkMovieScanOrderModify.Checked
                .MovieScraperCast = Me.chkMovieScraperCast.Checked
                If Not String.IsNullOrEmpty(Me.txtMovieScraperCastLimit.Text) Then
                    .MovieScraperCastLimit = Convert.ToInt32(Me.txtMovieScraperCastLimit.Text)
                Else
                    .MovieScraperCastLimit = 0
                End If
                .MovieScraperCastWithImgOnly = Me.chkMovieScraperCastWithImg.Checked
                .MovieScraperCertForMPAA = Me.chkMovieScraperCertForMPAA.Checked        'TODO
                .MovieScraperCertification = Me.chkMovieScraperCertification.Checked
                .MovieScraperCertLang = Me.cbMovieScraperCertLang.Text                  'TODO
                If Not String.IsNullOrEmpty(Me.cbMovieScraperCertLang.Text) Then
                    .MovieScraperCertForMPAA = Me.chkMovieScraperCertForMPAA.Checked    'TODO
                Else
                    .MovieScraperCertForMPAA = False
                End If
                .MovieScraperCollection = Me.chkMovieScraperCollection.Checked
                .MovieScraperCountry = Me.chkMovieScraperCountry.Checked
                .MovieScraperCrew = Me.chkMovieScraperCrew.Checked
                .MovieScraperDirector = Me.chkMovieScraperDirector.Checked
                .MovieScraperDurationRuntimeFormat = Me.txtMovieScraperDurationRuntimeFormat.Text
                .MovieScraperForceTitle = Me.cbMovieScraperForceTitle.Text
                .MovieScraperFullCast = Me.chkMovieScraperFullCast.Checked
                .MovieScraperFullCrew = Me.chkMovieScraperFullCrew.Checked
                .MovieScraperGenre = Me.chkMovieScraperGenre.Checked
                If Not String.IsNullOrEmpty(Me.txtMovieScraperGenreLimit.Text) Then
                    .MovieScraperGenreLimit = Convert.ToInt32(Me.txtMovieScraperGenreLimit.Text)
                Else
                    .MovieScraperGenreLimit = 0
                End If
                .MovieScraperMetaDataIFOScan = Me.chkMovieScraperMetaDataIFOScan.Checked
                .MovieScraperMetaDataScan = Me.chkMovieScraperMetaDataScan.Checked
                .MovieScraperMPAA = Me.chkMovieScraperMPAA.Checked
                .MovieScraperMusicBy = Me.chkMovieScraperMusicBy.Checked
                .MovieScraperOnlyValueForMPAA = Me.chkMovieScraperOnlyValueForMPAA.Checked
                .MovieScraperOutline = Me.chkMovieScraperOutline.Checked
                .MovieScraperOutlineForPlot = Me.chkMovieScraperOutlineForPlot.Checked
                If Not String.IsNullOrEmpty(Me.txtMovieScraperOutlineLimit.Text) Then
                    .MovieScraperOutlineLimit = Convert.ToInt32(Me.txtMovieScraperOutlineLimit.Text)
                Else
                    .MovieScraperOutlineLimit = 0
                End If
                .MovieScraperOutlinePlotEnglishOverwrite = Me.chkMovieScraperOutlinePlotEnglishOverwrite.Checked
                .MovieScraperPlot = Me.chkMovieScraperPlot.Checked
                .MovieScraperPlotForOutline = Me.chkMovieScraperPlotForOutline.Checked
                .MovieScraperProducers = Me.chkMovieScraperProducers.Checked
                .MovieScraperRating = Me.chkMovieScraperRating.Checked
                .MovieScraperRelease = Me.chkMovieScraperRelease.Checked
                .MovieScraperRuntime = Me.chkMovieScraperRuntime.Checked
                .MovieScraperStudio = Me.chkMovieScraperStudio.Checked
                .MovieScraperTagline = Me.chkMovieScraperTagline.Checked
                .MovieScraperTitle = Me.chkMovieScraperTitle.Checked
                .MovieScraperTitleFallback = Me.chkMovieScraperTitleFallback.Checked
                .MovieScraperTop250 = Me.chkMovieScraperTop250.Checked
                .MovieScraperTrailer = Me.chkMovieScraperTrailer.Checked
                .MovieScraperUseMDDuration = Me.chkMovieScraperUseMDDuration.Checked
                .MovieScraperUseMPAAFSK = Me.chkMovieScraperUseMPAAFSK.Checked
                .MovieScraperVotes = Me.chkMovieScraperVotes.Checked
                .MovieScraperWriters = Me.chkMovieScraperWriters.Checked
                .MovieScraperYear = Me.chkMovieScraperYear.Checked
                .MovieSkipLessThan = Convert.ToInt32(Me.txtMovieSkipLessThan.Text)
                .MovieSkipStackedSizeCheck = Me.chkMovieSkipStackedSizeCheck.Checked
                .MovieSortBeforeScan = Me.chkMovieSortBeforeScan.Checked
                .MovieSortTokens.Clear()
                .MovieSortTokens.AddRange(lstMovieSortTokens.Items.OfType(Of String).ToList)
                If .MovieSortTokens.Count <= 0 Then .MovieSortTokensIsEmpty = True
                .MovieSubCol = Me.chkMovieSubCol.Checked
                .MovieThemeCol = Me.chkMovieThemeCol.Checked
                .MovieThemeEnable = Me.chkMovieThemeEnable.Checked
                .MovieThemeOverwrite = Me.chkMovieThemeOverwrite.Checked
                .MovieTrailerCol = Me.chkMovieTrailerCol.Checked
                .MovieTrailerDeleteExisting = Me.chkMovieTrailerDeleteExisting.Checked
                .MovieTrailerEnable = Me.chkMovieTrailerEnable.Checked
                .MovieTrailerOverwrite = Me.chkMovieTrailerOverwrite.Checked
                .MovieTrailerMinQual = DirectCast(Me.cbMovieTrailerMinQual.SelectedIndex, Enums.TrailerQuality)
                .MovieTrailerPrefQual = DirectCast(Me.cbMovieTrailerPrefQual.SelectedIndex, Enums.TrailerQuality)
                .MovieWatchedCol = Me.chkMovieWatchedCol.Checked
                .TVASBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVASBannerHeight.Text), Convert.ToInt32(Me.txtTVASBannerHeight.Text), 0)
                .TVASBannerOverwrite = Me.chkTVASBannerOverwrite.Checked
                .TVASBannerPrefType = DirectCast(Me.cbTVASBannerPrefType.SelectedIndex, Enums.TVShowBannerType)
                .TVASBannerQual = Me.tbTVASBannerQual.Value
                .TVASBannerResize = Me.chkTVASBannerResize.Checked
                .TVASBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVASBannerWidth.Text), Convert.ToInt32(Me.txtTVASBannerWidth.Text), 0)
                .TVASFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVASFanartHeight.Text), Convert.ToInt32(Me.txtTVASFanartHeight.Text), 0)
                .TVASFanartOverwrite = Me.chkTVASFanartOverwrite.Checked
                .TVASFanartPrefSize = DirectCast(Me.cbTVASFanartPrefSize.SelectedIndex, Enums.TVFanartSize)
                .TVASFanartQual = Me.tbTVASFanartQual.Value
                .TVASFanartResize = Me.chkTVASFanartResize.Checked
                .TVASFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVASFanartWidth.Text), Convert.ToInt32(Me.txtTVASFanartWidth.Text), 0)
                .TVASLandscapeOverwrite = Me.chkTVASLandscapeOverwrite.Checked
                .TVASPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVASPosterHeight.Text), Convert.ToInt32(Me.txtTVASPosterHeight.Text), 0)
                .TVASPosterOverwrite = Me.chkTVASPosterOverwrite.Checked
                .TVASPosterPrefSize = DirectCast(Me.cbTVASPosterPrefSize.SelectedIndex, Enums.TVPosterSize)
                .TVASPosterQual = Me.tbTVASPosterQual.Value
                .TVASPosterResize = Me.chkTVASPosterResize.Checked
                .TVASPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVASPosterWidth.Text), Convert.ToInt32(Me.txtTVASPosterWidth.Text), 0)
                .TVCleanDB = Me.chkTVCleanDB.Checked
                .TVDisplayMissingEpisodes = Me.chkTVDisplayMissingEpisodes.Checked
                .TVEpisodeFanartCol = Me.chkTVEpisodeFanartCol.Checked
                .TVEpisodeFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVEpisodeFanartHeight.Text), Convert.ToInt32(Me.txtTVEpisodeFanartHeight.Text), 0)
                .TVEpisodeFanartOverwrite = Me.chkTVEpisodeFanartOverwrite.Checked
                .TVEpisodeFanartPrefSize = DirectCast(Me.cbTVEpisodeFanartPrefSize.SelectedIndex, Enums.TVFanartSize)
                .TVEpisodeFanartQual = Me.tbTVEpisodeFanartQual.Value
                .TVEpisodeFanartResize = Me.chkTVEpisodeFanartResize.Checked
                .TVEpisodeFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVEpisodeFanartWidth.Text), Convert.ToInt32(Me.txtTVEpisodeFanartWidth.Text), 0)
                .TVEpisodeFilterCustom.Clear()
                .TVEpisodeFilterCustom.AddRange(Me.lstTVEpisodeFilter.Items.OfType(Of String).ToList)
                If .TVEpisodeFilterCustom.Count <= 0 Then .TVEpisodeFilterCustomIsEmpty = True
                .TVEpisodeNfoCol = Me.chkTVEpisodeNfoCol.Checked
                .TVEpisodeNoFilter = Me.chkTVEpisodeNoFilter.Checked
                .TVEpisodePosterCol = Me.chkTVEpisodePosterCol.Checked
                .TVEpisodePosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVEpisodePosterHeight.Text), Convert.ToInt32(Me.txtTVEpisodePosterHeight.Text), 0)
                .TVEpisodePosterOverwrite = Me.chkTVEpisodePosterOverwrite.Checked
                .TVEpisodePosterQual = Me.tbTVEpisodePosterQual.Value
                .TVEpisodePosterResize = Me.chkTVEpisodePosterResize.Checked
                .TVEpisodePosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVEpisodePosterWidth.Text), Convert.ToInt32(Me.txtTVEpisodePosterWidth.Text), 0)
                .TVEpisodeProperCase = Me.chkTVEpisodeProperCase.Checked
                .TVGeneralDisplayASPoster = Me.chkTVGeneralDisplayASPoster.Checked
                .TVGeneralFlagLang = If(Me.cbTVLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, Me.cbTVLanguageOverlay.Text)
                .TVGeneralIgnoreLastScan = Me.chkTVGeneralIgnoreLastScan.Checked
                'cocotus, 2014/05/21 Fixed: If cbTVGeneralLang.Text is empty it will crash here -> no AdvancedSettings.xml will be built/saved!!(happens when user has not yet set TVLanguage via Fetch language button!)
                'old:    .TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.LongLang = cbTVGeneralLang.Text).ShortLang
                If cbTVGeneralLang.Text <> String.Empty Then
                    .TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.LongLang = cbTVGeneralLang.Text).ShortLang
                End If
                .TVGeneralMarkNewEpisodes = Me.chkTVGeneralMarkNewEpisodes.Checked
                .TVGeneralMarkNewShows = Me.chkTVGeneralMarkNewShows.Checked
                .TVLockEpisodePlot = Me.chkTVLockEpisodePlot.Checked
                .TVLockEpisodeRating = Me.chkTVLockEpisodeRating.Checked
                .TVLockEpisodeTitle = Me.chkTVLockEpisodeTitle.Checked
                .TVLockShowGenre = Me.chkTVLockShowGenre.Checked
                .TVLockShowPlot = Me.chkTVLockShowPlot.Checked
                .TVLockShowRating = Me.chkTVLockShowRating.Checked
                .TVLockShowStatus = Me.chkTVLockShowStatus.Checked
                .TVLockShowStudio = Me.chkTVLockShowStudio.Checked
                .TVLockShowTitle = Me.chkTVLockShowTitle.Checked
                .TVMetadataPerFileType.Clear()
                .TVMetadataPerFileType.AddRange(Me.TVMeta)
                .TVScanOrderModify = Me.chkTVScanOrderModify.Checked
                .TVScraperDurationRuntimeFormat = Me.txtTVScraperDurationRuntimeFormat.Text
                .TVScraperEpisodeActors = Me.chkTVScraperEpisodeActors.Checked
                .TVScraperEpisodeAired = Me.chkTVScraperEpisodeAired.Checked
                .TVScraperEpisodeCredits = Me.chkTVScraperEpisodeCredits.Checked
                .TVScraperEpisodeDirector = Me.chkTVScraperEpisodeDirector.Checked
                .TVScraperEpisodeEpisode = Me.chkTVScraperEpisodeEpisode.Checked
                .TVScraperEpisodePlot = Me.chkTVScraperEpisodePlot.Checked
                .TVScraperEpisodeRating = Me.chkTVScraperEpisodeRating.Checked
                .TVScraperEpisodeSeason = Me.chkTVScraperEpisodeSeason.Checked
                .TVScraperEpisodeTitle = Me.chkTVScraperEpisodeTitle.Checked
                .TVScraperMetaDataScan = Me.chkTVScraperMetaDataScan.Checked
                .TVScraperOptionsOrdering = DirectCast(Me.cbTVScraperOptionsOrdering.SelectedIndex, Enums.Ordering)
                If String.IsNullOrEmpty(Me.cbTVScraperRatingRegion.Text) Then
                    .TVScraperRatingRegion = "usa"
                Else
                    .TVScraperRatingRegion = Me.cbTVScraperRatingRegion.Text
                End If
                .TVScraperShowActors = Me.chkTVScraperShowActors.Checked
                .TVScraperShowEpiGuideURL = Me.chkTVScraperShowEpiGuideURL.Checked
                .TVScraperShowGenre = Me.chkTVScraperShowGenre.Checked
                .TVScraperShowMPAA = Me.chkTVScraperShowMPAA.Checked
                .TVScraperShowPlot = Me.chkTVScraperShowPlot.Checked
                .TVScraperShowPremiered = Me.chkTVScraperShowPremiered.Checked
                .TVScraperShowRating = Me.chkTVScraperShowRating.Checked
                .TVScraperShowStatus = Me.chkTVScraperShowStatus.Checked
                .TVScraperShowStudio = Me.chkTVScraperShowStudio.Checked
                .TVScraperShowTitle = Me.chkTVScraperShowTitle.Checked
                .TVScraperUpdateTime = DirectCast(Me.cbTVScraperUpdateTime.SelectedIndex, Enums.TVScraperUpdateTime)
                .TVScraperUseMDDuration = Me.chkTVScraperUseMDDuration.Checked
                .TVSeasonBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonBannerHeight.Text), Convert.ToInt32(Me.txtTVSeasonBannerHeight.Text), 0)
                .TVSeasonBannerOverwrite = Me.chkTVSeasonBannerOverwrite.Checked
                .TVSeasonBannerPrefType = DirectCast(Me.cbTVSeasonBannerPrefType.SelectedIndex, Enums.TVSeasonBannerType)
                .TVSeasonBannerQual = Me.tbTVSeasonBannerQual.Value
                .TVSeasonBannerResize = Me.chkTVSeasonBannerResize.Checked
                .TVSeasonBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonBannerWidth.Text), Convert.ToInt32(Me.txtTVSeasonBannerWidth.Text), 0)
                .TVSeasonFanartCol = Me.chkTVSeasonFanartCol.Checked
                .TVSeasonFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonFanartHeight.Text), Convert.ToInt32(Me.txtTVSeasonFanartHeight.Text), 0)
                .TVSeasonFanartOverwrite = Me.chkTVSeasonFanartOverwrite.Checked
                .TVSeasonFanartPrefSize = DirectCast(Me.cbTVSeasonFanartPrefSize.SelectedIndex, Enums.TVFanartSize)
                .TVSeasonFanartQual = Me.tbTVSeasonFanartQual.Value
                .TVSeasonFanartResize = Me.chkTVSeasonFanartResize.Checked
                .TVSeasonFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonFanartWidth.Text), Convert.ToInt32(Me.txtTVSeasonFanartWidth.Text), 0)
                .TVSeasonLandscapeOverwrite = Me.chkTVSeasonLandscapeOverwrite.Checked
                .TVSeasonPosterCol = Me.chkTVSeasonPosterCol.Checked
                .TVSeasonPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonPosterHeight.Text), Convert.ToInt32(Me.txtTVSeasonPosterHeight.Text), 0)
                .TVSeasonPosterOverwrite = Me.chkTVSeasonPosterOverwrite.Checked
                .TVSeasonPosterPrefSize = DirectCast(Me.cbTVSeasonPosterPrefSize.SelectedIndex, Enums.TVPosterSize)
                .TVSeasonPosterQual = Me.tbTVSeasonPosterQual.Value
                .TVSeasonPosterResize = Me.chkTVSeasonPosterResize.Checked
                .TVSeasonPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonPosterWidth.Text), Convert.ToInt32(Me.txtTVSeasonPosterWidth.Text), 0)
                .TVShowBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowBannerHeight.Text), Convert.ToInt32(Me.txtTVShowBannerHeight.Text), 0)
                .TVShowBannerOverwrite = Me.chkTVShowBannerOverwrite.Checked
                .TVShowBannerPrefType = DirectCast(Me.cbTVShowBannerPrefType.SelectedIndex, Enums.TVShowBannerType)
                .TVShowBannerQual = Me.tbTVShowBannerQual.Value
                .TVShowBannerResize = Me.chkTVShowBannerResize.Checked
                .TVShowBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowBannerWidth.Text), Convert.ToInt32(Me.txtTVShowBannerWidth.Text), 0)
                .TVShowCharacterArtOverwrite = Me.chkTVShowCharacterArtOverwrite.Checked
                .TVShowClearArtOverwrite = Me.chkTVShowClearArtOverwrite.Checked
                .TVShowClearLogoOverwrite = Me.chkTVShowClearLogoOverwrite.Checked
                .TVShowFanartCol = Me.chkTVShowFanartCol.Checked
                .TVShowFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowFanartHeight.Text), Convert.ToInt32(Me.txtTVShowFanartHeight.Text), 0)
                .TVShowFanartOverwrite = Me.chkTVShowFanartOverwrite.Checked
                .TVShowFanartPrefSize = DirectCast(Me.cbTVShowFanartPrefSize.SelectedIndex, Enums.TVFanartSize)
                .TVShowFanartQual = Me.tbTVShowFanartQual.Value
                .TVShowFanartResize = Me.chkTVShowFanartResize.Checked
                .TVShowFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowFanartWidth.Text), Convert.ToInt32(Me.txtTVShowFanartWidth.Text), 0)
                .TVShowFilterCustom.Clear()
                .TVShowFilterCustom.AddRange(Me.lstTVShowFilter.Items.OfType(Of String).ToList)
                If .TVShowFilterCustom.Count <= 0 Then .TVShowFilterCustomIsEmpty = True
                .TVShowLandscapeOverwrite = Me.chkTVShowLandscapeOverwrite.Checked
                .TVShowNfoCol = Me.chkTVShowNfoCol.Checked
                .TVShowPosterCol = Me.chkTVShowPosterCol.Checked
                .TVShowPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowPosterHeight.Text), Convert.ToInt32(Me.txtTVShowPosterHeight.Text), 0)
                .TVShowPosterOverwrite = Me.chkTVShowPosterOverwrite.Checked
                .TVShowPosterPrefSize = DirectCast(Me.cbTVShowPosterPrefSize.SelectedIndex, Enums.TVPosterSize)
                .TVShowPosterQual = Me.tbTVShowPosterQual.Value
                .TVShowPosterResize = Me.chkTVShowPosterResize.Checked
                .TVShowPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowPosterWidth.Text), Convert.ToInt32(Me.txtTVShowPosterWidth.Text), 0)
                .TVShowProperCase = Me.chkTVShowProperCase.Checked
                .TVShowRegexes.Clear()
                .TVShowRegexes.AddRange(Me.TVShowRegex)
                .TVSkipLessThan = Convert.ToInt32(Me.txtTVSkipLessThan.Text)

                If Me.tcFileSystemCleaner.SelectedTab.Name = "tpFileSystemCleanerExpert" Then
                    .FileSystemExpertCleaner = True
                    .CleanFolderJPG = False
                    .CleanMovieTBN = False
                    .CleanMovieTBNB = False
                    .CleanFanartJPG = False
                    .CleanMovieFanartJPG = False
                    .CleanMovieNFO = False
                    .CleanMovieNFOB = False
                    .CleanPosterTBN = False
                    .CleanPosterJPG = False
                    .CleanMovieJPG = False
                    .CleanMovieNameJPG = False
                    .CleanDotFanartJPG = False
                    .CleanExtrathumbs = False
                    .FileSystemCleanerWhitelist = Me.chkFileSystemCleanerWhitelist.Checked
                    .FileSystemCleanerWhitelistExts.Clear()
                    .FileSystemCleanerWhitelistExts.AddRange(Me.lstFileSystemCleanerWhitelist.Items.OfType(Of String).ToList)
                Else
                    .FileSystemExpertCleaner = False
                    .CleanFolderJPG = Me.chkCleanFolderJPG.Checked
                    .CleanMovieTBN = Me.chkCleanMovieTBN.Checked
                    .CleanMovieTBNB = Me.chkCleanMovieTBNb.Checked
                    .CleanFanartJPG = Me.chkCleanFanartJPG.Checked
                    .CleanMovieFanartJPG = Me.chkCleanMovieFanartJPG.Checked
                    .CleanMovieNFO = Me.chkCleanMovieNFO.Checked
                    .CleanMovieNFOB = Me.chkCleanMovieNFOb.Checked
                    .CleanPosterTBN = Me.chkCleanPosterTBN.Checked
                    .CleanPosterJPG = Me.chkCleanPosterJPG.Checked
                    .CleanMovieJPG = Me.chkCleanMovieJPG.Checked
                    .CleanMovieNameJPG = Me.chkCleanMovieNameJPG.Checked
                    .CleanDotFanartJPG = Me.chkCleanDotFanartJPG.Checked
                    .CleanExtrathumbs = Me.chkCleanExtrathumbs.Checked
                    .FileSystemCleanerWhitelist = False
                    .FileSystemCleanerWhitelistExts.Clear()
                End If

                If Me.clbMovieGenre.CheckedItems.Count > 0 Then
                    If Me.clbMovieGenre.CheckedItems.Contains(String.Format("{0}", Master.eLang.GetString(569, Master.eLang.All))) Then
                        .GenreFilter = String.Format("{0}", Master.eLang.GetString(569, Master.eLang.All))
                    Else
                        Dim strGenre As String = String.Empty
                        Dim iChecked = From iCheck In Me.clbMovieGenre.CheckedItems
                        strGenre = Strings.Join(iChecked.ToArray, ",")
                        .GenreFilter = strGenre.Trim
                    End If
                End If

                Me.clbMovieGenre.Items.Clear()
                LoadGenreLangs()
                FillGenres()

                If Not String.IsNullOrEmpty(Me.txtProxyURI.Text) AndAlso Not String.IsNullOrEmpty(Me.txtProxyPort.Text) Then
                    .ProxyURI = Me.txtProxyURI.Text
                    .ProxyPort = Convert.ToInt32(Me.txtProxyPort.Text)

                    If Not String.IsNullOrEmpty(Me.txtProxyUsername.Text) AndAlso Not String.IsNullOrEmpty(Me.txtProxyPassword.Text) Then
                        .ProxyCreds.UserName = Me.txtProxyUsername.Text
                        .ProxyCreds.Password = Me.txtProxyPassword.Text
                        .ProxyCreds.Domain = Me.txtProxyDomain.Text
                    Else
                        .ProxyCreds = New NetworkCredential
                    End If
                Else
                    .ProxyURI = String.Empty
                    .ProxyPort = -1
                End If


                '***************************************************
                '******************* Movie Part ********************
                '***************************************************

                '*************** XBMC Frodo settings ***************
                .MovieUseFrodo = Me.chkMovieUseFrodo.Checked
                .MovieActorThumbsFrodo = Me.chkMovieActorThumbsFrodo.Checked
                .MovieBannerFrodo = Me.chkMovieBannerFrodo.Checked
                .MovieClearArtFrodo = Me.chkMovieClearArtFrodo.Checked
                .MovieClearLogoFrodo = Me.chkMovieClearLogoFrodo.Checked
                .MovieDiscArtFrodo = Me.chkMovieDiscArtFrodo.Checked
                .MovieExtrafanartsFrodo = Me.chkMovieExtrafanartsFrodo.Checked
                .MovieExtrathumbsFrodo = Me.chkMovieExtrathumbsFrodo.Checked
                .MovieFanartFrodo = Me.chkMovieFanartFrodo.Checked
                .MovieLandscapeFrodo = Me.chkMovieLandscapeFrodo.Checked
                .MovieNFOFrodo = Me.chkMovieNFOFrodo.Checked
                .MoviePosterFrodo = Me.chkMoviePosterFrodo.Checked
                .MovieTrailerFrodo = Me.chkMovieTrailerFrodo.Checked

                '*************** XBMC Eden settings ***************
                .MovieUseEden = Me.chkMovieUseEden.Checked
                .MovieActorThumbsEden = Me.chkMovieActorThumbsEden.Checked
                '.MovieBannerEden = Me.chkBannerEden.Checked
                '.MovieClearArtEden = Me.chkClearArtEden.Checked
                '.MovieClearLogoEden = Me.chkClearLogoEden.Checked
                '.MovieDiscArtEden = Me.chkDiscArtEden.Checked
                .MovieExtrafanartsEden = Me.chkMovieExtrafanartsEden.Checked
                .MovieExtrathumbsEden = Me.chkMovieExtrathumbsEden.Checked
                .MovieFanartEden = Me.chkMovieFanartEden.Checked
                '.MovieLandscapeEden = Me.chkLandscapeEden.Checked
                .MovieNFOEden = Me.chkMovieNFOEden.Checked
                .MoviePosterEden = Me.chkMoviePosterEden.Checked
                .MovieTrailerEden = Me.chkMovieTrailerEden.Checked

                '************* XBMC optional settings *************
                .MovieXBMCTrailerFormat = Me.chkMovieXBMCTrailerFormat.Checked
                .MovieXBMCProtectVTSBDMV = Me.chkMovieXBMCProtectVTSBDMV.Checked

                '*************** XBMC theme settings ***************
                .MovieXBMCThemeCustom = Me.chkMovieXBMCThemeCustom.Checked
                .MovieXBMCThemeCustomPath = Me.txtMovieXBMCThemeCustomPath.Text
                .MovieXBMCThemeEnable = Me.chkMovieXBMCThemeEnable.Checked
                .MovieXBMCThemeMovie = Me.chkMovieXBMCThemeMovie.Checked
                .MovieXBMCThemeSub = Me.chkMovieXBMCThemeSub.Checked
                .MovieXBMCThemeSubDir = Me.txtMovieXBMCThemeSubDir.Text

                '****************** YAMJ settings *****************
                .MovieUseYAMJ = Me.chkMovieUseYAMJ.Checked
                '.MovieActorThumbsYAMJ = Me.chkActorThumbsYAMJ.Checked
                .MovieBannerYAMJ = Me.chkMovieBannerYAMJ.Checked
                '.MovieClearArtYAMJ = Me.chkClearArtYAMJ.Checked
                '.MovieClearLogoYAMJ = Me.chkClearLogoYAMJ.Checked
                '.MovieDiscArtYAMJ = Me.chkDiscArtYAMJ.Checked
                '.MovieExtrafanartYAMJ = Me.chkExtrafanartYAMJ.Checked
                '.MovieExtrathumbsYAMJ = Me.chkExtrathumbsYAMJ.Checked
                .MovieFanartYAMJ = Me.chkMovieFanartYAMJ.Checked
                '.MovieLandscapeYAMJ = Me.chkLandscapeYAMJ.Checked
                .MovieNFOYAMJ = Me.chkMovieNFOYAMJ.Checked
                .MoviePosterYAMJ = Me.chkMoviePosterYAMJ.Checked
                .MovieTrailerYAMJ = Me.chkMovieTrailerYAMJ.Checked

                '****************** NMJ settings *****************
                .MovieUseNMJ = Me.chkMovieUseNMJ.Checked
                '.MovieActorThumbsNMJ = Me.chkActorThumbsNMJ.Checked
                .MovieBannerNMJ = Me.chkMovieBannerNMJ.Checked
                '.MovieClearArtNMJ = Me.chkClearArtNMJ.Checked
                '.MovieClearLogoNMJ = Me.chkClearLogoNMJ.Checked
                '.MovieDiscArtNMJ = Me.chkDiscArtNMJ.Checked
                '.MovieExtrafanartNMJ = Me.chkExtrafanartNMJ.Checked
                '.MovieExtrathumbsNMJ = Me.chkExtrathumbsNMJ.Checked
                .MovieFanartNMJ = Me.chkMovieFanartNMJ.Checked
                '.MovieLandscapeNMJ = Me.chkLandscapeNMJ.Checked
                .MovieNFONMJ = Me.chkMovieNFONMJ.Checked
                .MoviePosterNMJ = Me.chkMoviePosterNMJ.Checked
                .MovieTrailerNMJ = Me.chkMovieTrailerNMJ.Checked

                '************** NMJ optional settings *************
                .MovieYAMJCompatibleSets = Me.chkMovieYAMJCompatibleSets.Checked
                .MovieYAMJWatchedFile = Me.chkMovieYAMJWatchedFile.Checked
                .MovieYAMJWatchedFolder = Me.txtMovieYAMJWatchedFolder.Text

                '***************** Boxee settings *****************
                .MovieUseBoxee = Me.chkMovieUseBoxee.Checked
                '.MovieActorThumbsBoxee = Me.chkActorThumbsBoxee.Checked
                '.MovieBannerBoxee = Me.chkMovieBannerBoxee.Checked
                '.MovieClearArtBoxee = Me.chkClearArtBoxee.Checked
                '.MovieClearLogoBoxee = Me.chkClearLogoBoxee.Checked
                '.MovieDiscArtBoxee = Me.chkDiscArtBoxee.Checked
                '.MovieExtrafanartBoxee = Me.chkExtrafanartBoxee.Checked
                '.MovieExtrathumbsBoxee = Me.chkExtrathumbsBoxee.Checked
                .MovieFanartBoxee = Me.chkMovieFanartBoxee.Checked
                '.MovieLandscapeBoxee = Me.chkLandscapeBoxee.Checked
                .MovieNFOBoxee = Me.chkMovieNFOBoxee.Checked
                .MoviePosterBoxee = Me.chkMoviePosterBoxee.Checked
                '.MovieTrailerBoxee = Me.chkMovieTrailerBoxee.Checked

                '***************** Expert settings ****************
                .MovieUseExpert = Me.chkMovieUseExpert.Checked

                '***************** Expert Single ******************
                .MovieActorThumbsExpertSingle = Me.chkMovieActorThumbsExpertSingle.Checked
                .MovieActorThumbsExtExpertSingle = Me.txtMovieActorThumbsExtExpertSingle.Text
                .MovieBannerExpertSingle = Me.txtMovieBannerExpertSingle.Text
                .MovieClearArtExpertSingle = Me.txtMovieClearArtExpertSingle.Text
                .MovieClearLogoExpertSingle = Me.txtMovieClearLogoExpertSingle.Text
                .MovieDiscArtExpertSingle = Me.txtMovieDiscArtExpertSingle.Text
                .MovieExtrafanartsExpertSingle = Me.chkMovieExtrafanartsExpertSingle.Checked
                .MovieExtrathumbsExpertSingle = Me.chkMovieExtrathumbsExpertSingle.Checked
                .MovieFanartExpertSingle = Me.txtMovieFanartExpertSingle.Text
                .MovieLandscapeExpertSingle = Me.txtMovieLandscapeExpertSingle.Text
                .MovieNFOExpertSingle = Me.txtMovieNFOExpertSingle.Text
                .MoviePosterExpertSingle = Me.txtMoviePosterExpertSingle.Text
                .MovieStackExpertSingle = Me.chkMovieStackExpertSingle.Checked
                .MovieTrailerExpertSingle = Me.txtMovieTrailerExpertSingle.Text
                .MovieUnstackExpertSingle = Me.chkMovieUnstackExpertSingle.Checked

                '***************** Expert Multi ******************
                .MovieActorThumbsExpertMulti = Me.chkMovieActorThumbsExpertMulti.Checked
                .MovieActorThumbsExtExpertMulti = Me.txtMovieActorThumbsExtExpertMulti.Text
                .MovieBannerExpertMulti = Me.txtMovieBannerExpertMulti.Text
                .MovieClearArtExpertMulti = Me.txtMovieClearArtExpertMulti.Text
                .MovieClearLogoExpertMulti = Me.txtMovieClearLogoExpertMulti.Text
                .MovieDiscArtExpertMulti = Me.txtMovieDiscArtExpertMulti.Text
                .MovieFanartExpertMulti = Me.txtMovieFanartExpertMulti.Text
                .MovieLandscapeExpertMulti = Me.txtMovieLandscapeExpertMulti.Text
                .MovieNFOExpertMulti = Me.txtMovieNFOExpertMulti.Text
                .MoviePosterExpertMulti = Me.txtMoviePosterExpertMulti.Text
                .MovieStackExpertMulti = Me.chkMovieStackExpertMulti.Checked
                .MovieTrailerExpertMulti = Me.txtMovieTrailerExpertMulti.Text
                .MovieUnstackExpertMulti = Me.chkMovieUnstackExpertMulti.Checked

                '***************** Expert VTS ******************
                .MovieActorThumbsExpertVTS = Me.chkMovieActorThumbsExpertVTS.Checked
                .MovieActorThumbsExtExpertVTS = Me.txtMovieActorThumbsExtExpertVTS.Text
                .MovieBannerExpertVTS = Me.txtMovieBannerExpertVTS.Text
                .MovieClearArtExpertVTS = Me.txtMovieClearArtExpertVTS.Text
                .MovieClearLogoExpertVTS = Me.txtMovieClearLogoExpertVTS.Text
                .MovieDiscArtExpertVTS = Me.txtMovieDiscArtExpertVTS.Text
                .MovieExtrafanartsExpertVTS = Me.chkMovieExtrafanartsExpertVTS.Checked
                .MovieExtrathumbsExpertVTS = Me.chkMovieExtrathumbsExpertVTS.Checked
                .MovieFanartExpertVTS = Me.txtMovieFanartExpertVTS.Text
                .MovieLandscapeExpertVTS = Me.txtMovieLandscapeExpertVTS.Text
                .MovieNFOExpertVTS = Me.txtMovieNFOExpertVTS.Text
                .MoviePosterExpertVTS = Me.txtMoviePosterExpertVTS.Text
                .MovieRecognizeVTSExpertVTS = Me.chkMovieRecognizeVTSExpertVTS.Checked
                .MovieTrailerExpertVTS = Me.txtMovieTrailerExpertVTS.Text
                .MovieUseBaseDirectoryExpertVTS = Me.chkMovieUseBaseDirectoryExpertVTS.Checked

                '***************** Expert BDMV ******************
                .MovieActorThumbsExpertBDMV = Me.chkMovieActorThumbsExpertBDMV.Checked
                .MovieActorThumbsExtExpertBDMV = Me.txtMovieActorThumbsExtExpertBDMV.Text
                .MovieBannerExpertBDMV = Me.txtMovieBannerExpertBDMV.Text
                .MovieClearArtExpertBDMV = Me.txtMovieClearArtExpertBDMV.Text
                .MovieClearLogoExpertBDMV = Me.txtMovieClearLogoExpertBDMV.Text
                .MovieDiscArtExpertBDMV = Me.txtMovieDiscArtExpertBDMV.Text
                .MovieExtrafanartsExpertBDMV = Me.chkMovieExtrafanartsExpertBDMV.Checked
                .MovieExtrathumbsExpertBDMV = Me.chkMovieExtrathumbsExpertBDMV.Checked
                .MovieFanartExpertBDMV = Me.txtMovieFanartExpertBDMV.Text
                .MovieLandscapeExpertBDMV = Me.txtMovieLandscapeExpertBDMV.Text
                .MovieNFOExpertBDMV = Me.txtMovieNFOExpertBDMV.Text
                .MoviePosterExpertBDMV = Me.txtMoviePosterExpertBDMV.Text
                .MovieTrailerExpertBDMV = Me.txtMovieTrailerExpertBDMV.Text
                .MovieUseBaseDirectoryExpertBDMV = Me.chkMovieUseBaseDirectoryExpertBDMV.Checked

                '***************************************************
                '****************** TV Show Part *******************
                '***************************************************

                '*************** XBMC Frodo settings ***************
                .TVUseFrodo = Me.chkTVUseFrodo.Checked
                .TVEpisodeActorThumbsFrodo = Me.chkTVEpisodeActorThumbsFrodo.Checked
                .TVEpisodePosterFrodo = Me.chkTVEpisodePosterFrodo.Checked
                .TVSeasonBannerFrodo = Me.chkTVSeasonBannerFrodo.Checked
                .TVSeasonFanartFrodo = Me.chkTVSeasonFanartFrodo.Checked
                .TVSeasonPosterFrodo = Me.chkTVSeasonPosterFrodo.Checked
                .TVShowActorThumbsFrodo = Me.chkTVShowActorThumbsFrodo.Checked
                .TVShowBannerFrodo = Me.chkTVShowBannerFrodo.Checked
                .TVShowFanartFrodo = Me.chkTVShowFanartFrodo.Checked
                .TVShowPosterFrodo = Me.chkTVShowPosterFrodo.Checked

                '*************** XBMC Eden settings ****************

                '************* XBMC optional settings **************
                .TVSeasonLandscapeXBMC = Me.chkTVSeasonLandscapeXBMC.Checked
                .TVShowCharacterArtXBMC = Me.chkTVShowCharacterArtXBMC.Checked
                .TVShowClearArtXBMC = Me.chkTVShowClearArtXBMC.Checked
                .TVShowClearLogoXBMC = Me.chkTVShowClearLogoXBMC.Checked
                .TVShowLandscapeXBMC = Me.chkTVShowLandscapeXBMC.Checked
                .TVShowTVThemeXBMC = Me.chkTVShowTVThemeXBMC.Checked
                .TVShowTVThemeFolderXBMC = Me.txtTVShowTVThemeFolderXBMC.Text

                '****************** YAMJ settings ******************
                .TVUseYAMJ = Me.chkTVUseYAMJ.Checked
                .TVEpisodePosterYAMJ = Me.chkTVEpisodePosterYAMJ.Checked
                .TVSeasonBannerYAMJ = Me.chkTVSeasonBannerYAMJ.Checked
                .TVSeasonFanartYAMJ = Me.chkTVSeasonFanartYAMJ.Checked
                .TVSeasonPosterYAMJ = Me.chkTVSeasonPosterYAMJ.Checked
                .TVShowBannerYAMJ = Me.chkTVShowBannerYAMJ.Checked
                .TVShowFanartYAMJ = Me.chkTVShowFanartYAMJ.Checked
                .TVShowPosterYAMJ = Me.chkTVShowPosterYAMJ.Checked

                '****************** NMJ settings *******************

                '************** NMT optional settings **************

                '***************** Boxee settings ******************
                .TVUseBoxee = Me.chkTVUseBoxee.Checked
                .TVEpisodePosterBoxee = Me.chkTVEpisodePosterBoxee.Checked
                .TVSeasonPosterBoxee = Me.chkTVSeasonPosterBoxee.Checked
                .TVShowBannerBoxee = Me.chkTVShowBannerBoxee.Checked
                .TVShowFanartBoxee = Me.chkTVShowFanartBoxee.Checked
                .TVShowPosterBoxee = Me.chkTVShowPosterBoxee.Checked

                '***************** Expert settings *****************


                'Default to Frodo for movies
                If Not (.MovieUseBoxee OrElse .MovieUseEden OrElse .MovieUseExpert OrElse .MovieUseFrodo OrElse .MovieUseNMJ OrElse .MovieUseYAMJ) Then
                    .MovieUseFrodo = True
                    .MovieActorThumbsFrodo = True
                    .MovieBannerFrodo = True
                    .MovieClearArtFrodo = True
                    .MovieClearLogoFrodo = True
                    .MovieDiscArtFrodo = True
                    .MovieExtrafanartsFrodo = True
                    .MovieExtrathumbsFrodo = True
                    .MovieFanartFrodo = True
                    .MovieLandscapeFrodo = True
                    .MovieNFOFrodo = True
                    .MoviePosterFrodo = True
                    .MovieTrailerFrodo = True
                End If

                'Default to Frodo for tvshows
                'TODO

            End With

            For Each s As ModulesManager._externalScraperModuleClass_Data In ModulesManager.Instance.externalDataScrapersModules
                Try
                    s.ProcessorModule.SaveSetupScraper(Not isApply)
                Catch ex As Exception
                    Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
                End Try
            Next
            For Each s As ModulesManager._externalScraperModuleClass_Poster In ModulesManager.Instance.externalPosterScrapersModules
                Try
                    s.ProcessorModule.SaveSetupScraper(Not isApply)
                Catch ex As Exception
                    Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
                End Try
            Next
            For Each s As ModulesManager._externalScraperModuleClass_Theme In ModulesManager.Instance.externalThemeScrapersModules
                Try
                    s.ProcessorModule.SaveSetupScraper(Not isApply)
                Catch ex As Exception
                    Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
                End Try
            Next
            For Each s As ModulesManager._externalScraperModuleClass_Trailer In ModulesManager.Instance.externalTrailerScrapersModules
                Try
                    s.ProcessorModule.SaveSetupScraper(Not isApply)
                Catch ex As Exception
                    Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
                End Try
            Next
            For Each s As ModulesManager._externalTVScraperModuleClass In ModulesManager.Instance.externalTVScrapersModules
                Try
                    If s.ProcessorModule.IsScraper Then s.ProcessorModule.SaveSetupScraper(Not isApply)
                    If s.ProcessorModule.IsPostScraper Then s.ProcessorModule.SaveSetupPostScraper(Not isApply)
                Catch ex As Exception
                    Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
                End Try
            Next
            For Each s As ModulesManager._externalTVScraperModuleClass_Theme In ModulesManager.Instance.externalTVThemeScrapersModules
                Try
                    s.ProcessorModule.SaveSetupScraper(Not isApply)
                Catch ex As Exception
                    Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
                End Try
            Next
            For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalProcessorModules
                Try
                    s.ProcessorModule.SaveSetup(Not isApply)
                Catch ex As Exception
                    Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
                End Try
            Next
            ModulesManager.Instance.SaveSettings()
            Functions.CreateDefaultOptions()
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SetApplyButton(ByVal v As Boolean)
        If Not NoUpdate Then Me.btnApply.Enabled = v
    End Sub

    Private Sub chkMovieThemeOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieTrailerOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieTrailerOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieTrailerDeleteExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieTrailerDeleteExisting.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub SetUp()
        Me.gbMovieScraperMiscOpts.Text = Me.gbGeneralMisc.Text
        Me.gbMovieScraperGlobalLocksOpts.Text = Master.eLang.GetString(488, "Global Locks")
        Me.gbMovieGeneralMiscOpts.Text = Me.gbGeneralMisc.Text
        Me.gbFileSystemValidExts.Text = Master.eLang.GetString(534, "Valid Video Extensions")
        Me.gbFileSystemValidThemeExts.Text = Master.eLang.GetString(1081, "Valid Theme Extensions")
        Me.gbMovieGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        Me.gbFileSystemNoStackExts.Text = Master.eLang.GetString(530, "No Stack Extensions")
        Me.gbMovieSortTokensOpts.Text = Master.eLang.GetString(463, "Sort Tokens to Ignore")
        Me.gbMovieScraperMetaDataOpts.Text = Master.eLang.GetString(59, "Meta Data")
        Me.gbMovieGeneralMissingItemsOpts.Text = Master.eLang.GetString(581, "Missing Items Filter")
        Me.gbMovieScraperDefFIExtOpts.Text = Master.eLang.GetString(625, "Defaults by File Type")
        Me.gbGeneralThemes.Text = Master.eLang.GetString(629, "Themes")
        Me.gbFileSystemCleanFiles.Text = Master.eLang.GetString(437, "Clean Files")
        Me.gbTVScraperMetaDataOpts.Text = Me.gbMovieScraperMetaDataOpts.Text
        Me.gbTVScraperFieldsOpts.Text = Master.eLang.GetString(577, "Scraper Fields")
        Me.gbTVScraperGlobalLocksOpts.Text = Master.eLang.GetString(488, "Global Locks")
        Me.gbTVScraperFieldsEpisodeOpts.Text = Master.eLang.GetString(727, "Episode")
        Me.gbTVScraperFieldsShowOpts.Text = Master.eLang.GetString(743, "Show")
        Me.gbGeneralMisc.Text = Master.eLang.GetString(429, "Miscellaneous")
        Me.gbMovieImagesOpts.Text = Master.eLang.GetString(497, "Images")
        Me.lblGeneralTVShowTheme.Text = String.Concat(Master.eLang.GetString(666, "TV Show Theme"), ":")
        Me.lblMovieFanartHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblMoviePosterWidth.Text = Master.eLang.GetString(479, "Max Width:")
        Me.lblMoviePosterHeight.Text = Master.eLang.GetString(480, "Max Height:")
        Me.lblMovieIMDBMirror.Text = Master.eLang.GetString(884, "IMDB Mirror:")
        Me.lblMovieSkipLessThan.Text = Master.eLang.GetString(540, "Skip files smaller than:")
        Me.lblMovieSkipLessThanMB.Text = Master.eLang.GetString(539, "MB")
        Me.lblMoviePosterQ.Text = Master.eLang.GetString(478, "Quality:")
        Me.lblFileSystemCleanerWarning.Text = Master.eLang.GetString(442, "WARNING: Using the Expert Mode Cleaner could potentially delete wanted files. Take care when using this tool.")
        Me.lblMovieFanartQ.Text = Me.lblMoviePosterQ.Text
        Me.lblFileSystemCleanerWhitelist.Text = Master.eLang.GetString(441, "Whitelisted Extensions:")
        Me.lblGeneralTVEpisodeTheme.Text = String.Concat(Master.eLang.GetString(667, "Episode Theme"), ":")
        Me.lblMovieLevTolerance.Text = Master.eLang.GetString(461, "Mismatch Tolerance:")
        Me.lblMovieLanguageOverlay.Text = Master.eLang.GetString(436, "Display Overlay if Video Contains an Audio Stream With the Following Language:")
        Me.lblGeneralntLang.Text = Master.eLang.GetString(430, "Interface Language:")
        Me.lblMovieScraperDefFIExt.Text = Master.eLang.GetString(626, "File Type")
        Me.lblGeneralMovieTheme.Text = String.Concat(Master.eLang.GetString(620, "Movie Theme"), ":")
        Me.lblTVScraperDefFIExt.Text = Me.lblMovieScraperDefFIExt.Text
        Me.lblGeneralOverwriteNfo.Text = Master.eLang.GetString(434, "(If unchecked, non-conforming nfos will be renamed to <filename>.info)")
        Me.lblTVLanguageOverlay.Text = Me.lblMovieLanguageOverlay.Text
        Me.lblMovieScraperDurationRuntimeFormat.Text = Master.eLang.GetString(732, "<h>=Hours <m>=Minutes <s>=Seconds")
        Me.TVSkipLessThanMB.Text = Me.lblMovieSkipLessThanMB.Text
        Me.lblTVSkipLessThan.Text = Me.lblMovieSkipLessThan.Text
        Me.lblMovieFanartWidth.Text = Me.lblMoviePosterWidth.Text
        Me.tpTVSourcesGeneral.Text = Master.eLang.GetString(38, "General")
        Me.tpTVSourcesRegex.Text = Master.eLang.GetString(699, "Regex")
        Me.Text = Master.eLang.GetString(420, "Settings")
        Me.btnTVShowRegexAdd.Tag = String.Empty
        Me.btnTVShowRegexAdd.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.btnTVSourceAdd.Text = Me.btnMovieSourceAdd.Text
        Me.btnApply.Text = Master.eLang.GetString(276, "Apply")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.btnTVShowRegexClear.Text = Master.eLang.GetString(123, "Clear")
        Me.btnTVShowRegexEdit.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.btnMovieSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        Me.btnTVSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        Me.btnMovieSourceAdd.Text = Master.eLang.GetString(407, "Add Source")
        Me.btnMovieSourceRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnRemTVSource.Text = Master.eLang.GetString(30, "Remove")
        Me.btnTVShowRegexRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.cbMovieScraperForceTitle.Items.AddRange(Strings.Split(AdvancedSettings.GetSetting("ForceTitle", ""), "|"))
        Me.chkMovieClickScrapeAsk.Text = Master.eLang.GetString(852, "Ask On Click Scrape")
        Me.chkMovieBackdropsAuto.Text = Master.eLang.GetString(521, "Automatically Save Fanart To Backdrops Folder")
        Me.chkMovieScraperCast.Text = Master.eLang.GetString(63, "Cast")
        Me.chkMovieScraperCastWithImg.Text = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        Me.chkMovieScraperCertLang.Text = Master.eLang.GetString(514, "Use Certification Language:")
        Me.chkMovieScraperCertification.Text = Master.eLang.GetString(722, "Certification")
        Me.chkMovieLevTolerance.Text = Master.eLang.GetString(462, "Check Title Match Confidence")
        Me.chkMovieCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        Me.chkMovieClickScrape.Text = Master.eLang.GetString(849, "Enable Click Scrape")
        Me.chkMovieScraperCollection.Text = Master.eLang.GetString(1135, "Collection")
        Me.chkMovieScraperCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkMovieScraperCrew.Text = Master.eLang.GetString(391, "Other Crew")
        Me.chkMovieTrailerDeleteExisting.Text = Master.eLang.GetString(522, "Delete All Existing")
        Me.chkMovieScraperDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkTVGeneralDisplayASPoster.Text = Master.eLang.GetString(832, "Display All Season Poster")
        Me.chkTVDisplayMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        Me.chkMovieDisplayYear.Text = Master.eLang.GetString(464, "Display Year in List Title")
        Me.chkMovieThemeEnable.Text = Master.eLang.GetString(1082, "Enable Theme Support")
        Me.chkMovieTrailerEnable.Text = Master.eLang.GetString(529, "Enable Trailer Support")
        Me.chkProxyCredsEnable.Text = Master.eLang.GetString(677, "Enable Credentials")
        Me.chkProxyEnable.Text = Master.eLang.GetString(673, "Enable Proxy")
        Me.chkTVLockEpisodePlot.Text = Master.eLang.GetString(496, "Lock Plot")
        Me.chkTVLockEpisodeRating.Text = Master.eLang.GetString(492, "Lock Rating")
        Me.chkTVLockEpisodeTitle.Text = Master.eLang.GetString(494, "Lock Title")
        Me.chkTVEpisodeProperCase.Text = Me.chkMovieProperCase.Text
        Me.chkTVEpisodeFanartCol.Text = Me.chkMovieFanartCol.Text
        Me.chkTVEpisodeNfoCol.Text = Me.chkMovieNFOCol.Text
        Me.chkTVEpisodePosterCol.Text = Me.chkMoviePosterCol.Text
        Me.chkMovieScraperForceTitle.Text = Master.eLang.GetString(710, "Force Title Language:")
        Me.chkMovieScraperFullCast.Text = Master.eLang.GetString(512, "Scrape Full Cast")
        Me.chkMovieScraperFullCrew.Text = Master.eLang.GetString(513, "Scrape Full Crew")
        Me.chkMovieScraperGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkMovieScraperMetaDataIFOScan.Text = Master.eLang.GetString(628, "Enable IFO Parsing")
        Me.chkMovieGeneralIgnoreLastScan.Text = Master.eLang.GetString(669, "Ignore last scan time when updating library")
        Me.chkGeneralImagesGlassOverlay.Text = Master.eLang.GetString(966, "Enable Images Glass Overlay")
        Me.chkGeneralInfoPanelAnim.Text = Master.eLang.GetString(431, "Enable Panel Animation")
        Me.chkMovieLockGenre.Text = Master.eLang.GetString(490, "Lock Genre")
        Me.chkMovieLockLanguageA.Text = Master.eLang.GetString(880, "Lock Language (audio)")
        Me.chkMovieLockLanguageV.Text = Master.eLang.GetString(879, "Lock Language (video)")
        Me.chkMovieLockMPAA.Text = Master.eLang.GetString(881, "Lock MPAA/Certification")
        Me.chkMovieLockOutline.Text = Master.eLang.GetString(495, "Lock Outline")
        Me.chkMovieLockPlot.Text = Master.eLang.GetString(496, "Lock Plot")
        Me.chkMovieLockRating.Text = Master.eLang.GetString(492, "Lock Rating")
        Me.chkMovieLockStudio.Text = Master.eLang.GetString(491, "Lock Studio")
        Me.chkMovieLockTagline.Text = Master.eLang.GetString(493, "Lock Tagline")
        Me.chkMovieLockTitle.Text = Master.eLang.GetString(494, "Lock Title")
        Me.chkMovieLockTrailer.Text = Master.eLang.GetString(489, "Lock Trailer")
        Me.chkMovieScraperMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkMovieGeneralMarkNew.Text = Master.eLang.GetString(459, "Mark New Movies")
        Me.chkTVGeneralMarkNewEpisodes.Text = Master.eLang.GetString(621, "Mark New Episodes")
        Me.chkTVGeneralMarkNewShows.Text = Master.eLang.GetString(549, "Mark New Shows")
        Me.chkMovieMissingBanner.Text = Master.eLang.GetString(1073, "Check for Banner")
        Me.chkMovieMissingClearArt.Text = Master.eLang.GetString(1112, "Check for ClearArt")
        Me.chkMovieMissingClearLogo.Text = Master.eLang.GetString(1113, "Check for ClearLogo")
        Me.chkMovieMissingDiscArt.Text = Master.eLang.GetString(1114, "Check for DiscArt")
        Me.chkMovieMissingEFanarts.Text = Master.eLang.GetString(976, "Check for Extrafanarts")
        Me.chkMovieMissingEThumbs.Text = Master.eLang.GetString(587, "Check for Extrathumbs")
        Me.chkMovieMissingFanart.Text = Master.eLang.GetString(583, "Check for Fanart")
        Me.chkMovieMissingLandscape.Text = Master.eLang.GetString(1074, "Check for Landscape")
        Me.chkMovieMissingNFO.Text = Master.eLang.GetString(584, "Check for NFO")
        Me.chkMovieMissingPoster.Text = Master.eLang.GetString(582, "Check for Poster")
        Me.chkMovieMissingSubs.Text = Master.eLang.GetString(586, "Check for Subs")
        Me.chkMovieMissingTheme.Text = Master.eLang.GetString(1075, "Check for Theme")
        Me.chkMovieMissingTrailer.Text = Master.eLang.GetString(585, "Check for Trailer")
        Me.chkMovieBannerCol.Text = Master.eLang.GetString(1070, "Hide Banner Column")
        Me.chkMovieClearArtCol.Text = Master.eLang.GetString(1115, "Hide ClearArt Column")
        Me.chkMovieClearLogoCol.Text = Master.eLang.GetString(1116, "Hide ClearLogo Column")
        Me.chkMovieDiscArtCol.Text = Master.eLang.GetString(1117, "Hide DiscArt Column")
        Me.chkMovieEFanartsCol.Text = Master.eLang.GetString(983, "Hide Extrafanart Column")
        Me.chkMovieEThumbsCol.Text = Master.eLang.GetString(465, "Hide Extrathumb Column")
        Me.chkMovieFanartCol.Text = Master.eLang.GetString(469, "Hide Fanart Column")
        Me.chkMovieFanartPrefOnly.Text = Master.eLang.GetString(145, "Only")
        Me.chkMovieLandscapeCol.Text = Master.eLang.GetString(1071, "Hide Landscape Column")
        Me.chkMovieNFOCol.Text = Master.eLang.GetString(468, "Hide NFO Column")
        Me.chkMovieNoSaveImagesToNfo.Text = Master.eLang.GetString(498, "Do Not Save URLs to Nfo")
        Me.chkMovieFanartOverwrite.Text = Me.chkMoviePosterOverwrite.Text
        Me.chkMoviePosterOverwrite.Text = Master.eLang.GetString(483, "Overwrite Existing")
        Me.chkMoviePosterCol.Text = Master.eLang.GetString(470, "Hide Poster Column")
        Me.chkMoviePosterPrefOnly.Text = Master.eLang.GetString(145, "Only")
        Me.chkMovieRecognizeVTSExpertVTS.Text = Master.eLang.GetString(537, "Automatically Detect VIDEO_TS Folders Even if They Are Not Named ""VIDEO_TS""")
        Me.chkMovieFanartResize.Text = Me.chkMoviePosterResize.Text
        Me.chkMoviePosterResize.Text = Master.eLang.GetString(481, "Automatically Resize:")
        Me.chkMovieSubCol.Text = Master.eLang.GetString(466, "Hide Sub Column")
        Me.chkMovieThemeCol.Text = Master.eLang.GetString(1072, "Hide Theme Column")
        Me.chkMovieThemeOverwrite.Text = Master.eLang.GetString(483, "Overwrite Existing")
        Me.chkMovieTrailerCol.Text = Master.eLang.GetString(467, "Hide Trailer Column")
        Me.chkMovieWatchedCol.Text = Master.eLang.GetString(982, "Hide Watched Column")
        Me.chkMovieScraperMusicBy.Text = Master.eLang.GetString(392, "Music By")
        Me.chkGeneralHideFanart.Text = Master.eLang.GetString(455, "Do Not Display Fanart")
        Me.chkGeneralHideFanartSmall.Text = Master.eLang.GetString(967, "Do Not Display Small Fanart")
        Me.chkGeneralHidePoster.Text = Master.eLang.GetString(456, "Do Not Display Poster")
        Me.chkTVEpisodeNoFilter.Text = Master.eLang.GetString(734, "Build Episode Title Instead of Filtering")
        Me.chkMovieScraperOnlyValueForMPAA.Text = Master.eLang.GetString(835, "Only Save the Value to NFO")
        Me.chkMovieScraperOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        Me.chkMovieScraperOutlineForPlot.Text = Master.eLang.GetString(508, "Use Outline for Plot if Plot is Empty")
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.Text = Master.eLang.GetString(1005, "Only overwrite english Outline and Plot")
        Me.chkGeneralOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        Me.chkMovieTrailerOverwrite.Text = Master.eLang.GetString(483, "Overwrite Existing")
        Me.chkMovieScraperPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkMovieScraperPlotForOutline.Text = Master.eLang.GetString(965, "Use Plot for Outline if Outline is Empty")
        Me.chkMovieScraperProducers.Text = Master.eLang.GetString(393, "Producers")
        Me.chkMovieProperCase.Text = Master.eLang.GetString(452, "Convert Names to Proper Case")
        Me.chkMovieScraperRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkMovieScraperRelease.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkMovieScraperRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkMovieScraperMetaDataScan.Text = Master.eLang.GetString(517, "Scan Meta Data")
        Me.chkMovieScanOrderModify.Text = Master.eLang.GetString(796, "Scan in order of last write time")
        Me.chkTVScraperEpisodeActors.Text = Master.eLang.GetString(725, "Actors")
        Me.chkTVScraperEpisodeAired.Text = Master.eLang.GetString(728, "Aired")
        Me.chkTVScraperEpisodeCredits.Text = Master.eLang.GetString(729, "Credits")
        Me.chkTVScraperEpisodeDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkTVScraperEpisodeEpisode.Text = Master.eLang.GetString(727, "Episode")
        Me.chkTVScraperEpisodePlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkTVScraperEpisodeRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkTVScraperEpisodeSeason.Text = Master.eLang.GetString(650, "Season")
        Me.chkTVScraperEpisodeTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkTVScraperShowActors.Text = Master.eLang.GetString(725, "Actors")
        Me.chkTVScraperShowEpiGuideURL.Text = Master.eLang.GetString(723, "EpisodeGuideURL")
        Me.chkTVScraperShowGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkTVScraperShowMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkTVScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkTVScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        Me.chkTVScraperShowRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkTVScraperShowStatus.Text = Master.eLang.GetString(215, "Status")
        Me.chkTVScraperShowStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkTVScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkTVSeasonFanartCol.Text = Me.chkMovieFanartCol.Text
        Me.chkTVSeasonPosterCol.Text = Me.chkMoviePosterCol.Text
        Me.chkGeneralShowImgDims.Text = Master.eLang.GetString(457, "Display Image Dimensions")
        Me.chkTVShowFanartCol.Text = Me.chkMovieFanartCol.Text
        Me.chkGeneralShowGenresText.Text = Master.eLang.GetString(453, "Always Display Genre Text")
        Me.chkTVLockShowGenre.Text = Master.eLang.GetString(490, "Lock Genre")
        Me.chkTVLockShowPlot.Text = Master.eLang.GetString(496, "Lock Plot")
        Me.chkTVLockShowRating.Text = Master.eLang.GetString(492, "Lock Rating")
        Me.chkTVLockShowStatus.Text = Master.eLang.GetString(1047, "Lock Status")
        Me.chkTVLockShowStudio.Text = Master.eLang.GetString(491, "Lock Studio")
        Me.chkTVLockShowTitle.Text = Master.eLang.GetString(494, "Lock Title")
        Me.chkTVShowNfoCol.Text = Me.chkMovieNFOCol.Text
        Me.chkTVShowPosterCol.Text = Me.chkMoviePosterCol.Text
        Me.chkTVShowProperCase.Text = Me.chkMovieProperCase.Text
        Me.chkMovieSkipStackedSizeCheck.Text = Master.eLang.GetString(538, "Skip Size Check of Stacked Files")
        Me.chkMovieSortBeforeScan.Text = Master.eLang.GetString(712, "Sort files into folder before each library update")
        Me.chkGeneralSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        Me.chkGeneralCreationDate.Text = Master.eLang.GetString(874, "Use FileCreated information of videofile")
        Me.chkMovieScraperStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkTVASPosterOverwrite.Text = Me.chkMoviePosterOverwrite.Text
        Me.chkTVASPosterResize.Text = Me.chkMoviePosterResize.Text
        Me.chkTVCleanDB.Text = Me.chkMovieCleanDB.Text
        Me.chkTVEpisodeFanartOverwrite.Text = Me.chkMovieFanartOverwrite.Text
        Me.chkTVEpisodeFanartResize.Text = Me.chkMovieFanartResize.Text
        Me.chkTVEpisodePosterOverwrite.Text = Me.chkMoviePosterOverwrite.Text
        Me.chkTVEpisodePosterResize.Text = Me.chkMoviePosterResize.Text
        Me.chkTVGeneralIgnoreLastScan.Text = Me.chkMovieGeneralIgnoreLastScan.Text
        Me.chkTVScraperMetaDataScan.Text = Me.chkMovieScraperMetaDataScan.Text
        Me.chkTVScanOrderModify.Text = Me.chkMovieScanOrderModify.Text
        Me.chkTVSeasonFanartOverwrite.Text = Me.chkMovieFanartOverwrite.Text
        Me.chkTVSeasonFanartResize.Text = Me.chkMovieFanartResize.Text
        Me.chkTVSeasonPosterOverwrite.Text = Me.chkMoviePosterOverwrite.Text
        Me.chkTVSeasonPosterResize.Text = Me.chkMoviePosterResize.Text
        Me.chkTVShowFanartOverwrite.Text = Me.chkMovieFanartOverwrite.Text
        Me.chkTVShowFanartResize.Text = Me.chkMovieFanartResize.Text
        Me.chkTVShowPosterOverwrite.Text = Me.chkMoviePosterOverwrite.Text
        Me.chkTVShowPosterResize.Text = Me.chkMoviePosterResize.Text
        Me.chkMovieScraperTagline.Text = Master.eLang.GetString(397, "Tagline")
        Me.chkMovieScraperTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkMovieScraperTitleFallback.Text = Master.eLang.GetString(984, "Worldwide title as fallback")
        Me.chkMovieScraperTop250.Text = Master.eLang.GetString(591, "Top 250")
        Me.chkMovieScraperTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkGeneralCheckUpdates.Text = Master.eLang.GetString(432, "Check for Updates")
        Me.chkMovieScraperCertForMPAA.Text = Master.eLang.GetString(511, "Use Certification for MPAA")
        Me.chkTVScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.chkMovieScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.chkMovieScraperUseMPAAFSK.Text = Master.eLang.GetString(882, "Use MPAA as Fallback for FSK Rating")
        Me.chkMovieScraperVotes.Text = Master.eLang.GetString(399, "Votes")
        Me.chkFileSystemCleanerWhitelist.Text = Master.eLang.GetString(440, "Whitelist Video Extensions")
        Me.chkMovieScraperWriters.Text = Master.eLang.GetString(394, "Writers")
        Me.chkMovieScraperYear.Text = Master.eLang.GetString(278, "Year")
        Me.colFolder.Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colPath.Text = Master.eLang.GetString(410, "Path")
        Me.colRecur.Text = Master.eLang.GetString(411, "Recursive")
        Me.colSingle.Text = Master.eLang.GetString(413, "Single Video")
        Me.gbProxyCredsOpts.Text = Master.eLang.GetString(676, "Credentials")
        Me.gbTVEpisodeFilterOpts.Text = Master.eLang.GetString(671, "Episode Folder/File Name Filters")
        Me.gbTVScraperGlobalLocksEpisodeOpts.Text = Master.eLang.GetString(727, "Episode")
        Me.gbTVGeneralListEpisodeOpts.Text = Master.eLang.GetString(682, "Episodes")
        Me.gbMovieGeneralFiltersOpts.Text = Master.eLang.GetString(451, "Folder/File Name Filters")
        Me.gbMovieGeneralGenreFilterOpts.Text = Master.eLang.GetString(454, "Genre Language Filter")
        Me.gbSettingsHelp.Text = String.Concat("     ", Master.eLang.GetString(458, "Help"))
        Me.gbGeneralInterface.Text = Master.eLang.GetString(795, "Interface")
        Me.gbTVSourcesMiscOpts.Text = Master.eLang.GetString(536, "Miscellaneous Options")
        Me.gbMovieBackdropsFolder.Text = Master.eLang.GetString(520, "Backdrops Folder")
        Me.gbMovieFileNaming.Text = Master.eLang.GetString(471, "File Naming")
        Me.gbMovieFanartOpts.Text = Master.eLang.GetString(149, "Fanart")
        Me.gbMoviePosterOpts.Text = Master.eLang.GetString(148, "Poster")
        Me.gbMovieMiscOpts.Text = Master.eLang.GetString(536, "Miscellaneous Options")
        Me.gbMovieSetsFolder.Text = Master.eLang.GetString(986, "Movieset Artwork Folder")
        Me.gbMovieScraperFieldsOpts.Text = Master.eLang.GetString(577, "Scraper Fields")
        Me.gbProxyOpts.Text = Master.eLang.GetString(672, "Proxy")
        Me.gbMovieScraperDurationFormatOpts.Text = Master.eLang.GetString(515, "Duration Format")
        Me.gbTVGeneralListSeasonOpts.Text = Master.eLang.GetString(681, "Seasons")
        Me.gbTVShowFilterOpts.Text = Master.eLang.GetString(670, "Show Folder/File Name Filters")
        Me.gbTVGeneralListShowOpts.Text = Master.eLang.GetString(680, "Shows")
        Me.gbTVScraperGlobalLocksShowOpts.Text = Master.eLang.GetString(743, "Show")
        Me.gbTVShowRegex.Text = Master.eLang.GetString(691, "Show Match Regex")
        Me.gbTVASPosterOpts.Text = Master.eLang.GetString(735, "All Season Posters")
        Me.gbTVEpisodeFanartOpts.Text = Me.gbMovieFanartOpts.Text
        Me.gbTVEpisodePosterOpts.Text = Me.gbMoviePosterOpts.Text
        Me.gbTVGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        Me.gbTVScraperDefFIExtOpts.Text = Me.gbTVScraperDefFIExtOpts.Text
        Me.gbTVGeneralMiscOpts.Text = Me.gbGeneralMisc.Text
        Me.gbTVScraperDurationOpts.Text = Master.eLang.GetString(515, "Duration Format")
        Me.gbTVScraperOptionsOpts.Text = Master.eLang.GetString(390, "Options")
        Me.gbTVSeasonFanartOpts.Text = Me.gbMovieFanartOpts.Text
        Me.gbTVSeasonPosterOpts.Text = Me.gbMoviePosterOpts.Text
        Me.gbTVShowFanartOpts.Text = Me.gbMovieFanartOpts.Text
        Me.gbTVShowPosterOpts.Text = Me.gbMoviePosterOpts.Text
        Me.lblTVEpisodeMatch.Text = Master.eLang.GetString(693, "Episode Match Regex:")
        Me.lblTVEpisodeRetrieve.Text = Me.lblTVSeasonRetrieve.Text
        Me.lblFanartSize.Text = Me.lblMoviePosterSize.Text
        Me.lblMovieScraperCastLimit.Text = Master.eLang.GetString(578, "Limit:")
        Me.lblMovieScraperGenreLimit.Text = Me.lblMovieScraperCastLimit.Text
        Me.lblMovieScraperOutlineLimit.Text = Me.lblMovieScraperCastLimit.Text
        Me.lblTVScraperOptionsOrdering.Text = Master.eLang.GetString(797, "Default Episode Ordering:")
        Me.lblMoviePosterSize.Text = Master.eLang.GetString(482, "Preferred Size:")
        Me.lblMovieTrailerPrefQual.Text = Master.eLang.GetString(1027, "Minimum Quality:")
        Me.lblMovieTrailerPrefQual.Text = Master.eLang.GetString(800, "Preferred Quality:")
        Me.lblProxyDomain.Text = Master.eLang.GetString(678, "Domain:")
        Me.lblProxyPassword.Text = Master.eLang.GetString(426, "Password:")
        Me.lblProxyPort.Text = Master.eLang.GetString(675, "Proxy Port:")
        Me.lblProxyUsername.Text = Master.eLang.GetString(425, "Username:")
        Me.lblProxyURI.Text = Master.eLang.GetString(674, "Proxy URL:")
        Me.lblTVScraperRatingRegion.Text = Master.eLang.GetString(679, "TV Rating Region")
        Me.lblTVSeasonMatch.Text = Master.eLang.GetString(692, "Season Match Regex:")
        Me.lblTVSeasonRetrieve.Text = String.Concat(Master.eLang.GetString(694, "Apply To"), ":")
        Me.lblTVASPosterHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblTVASPosterQ.Text = Me.lblMoviePosterQ.Text
        Me.lblTVASPosterSize.Text = Me.lblTVShowPosterSize.Text
        Me.lblTVASPosterWidth.Text = Me.lblMoviePosterWidth.Text
        Me.lblTVEpisodeFanartHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblTVEpisodeFanartQ.Text = Me.lblMovieFanartQ.Text
        Me.lblTVEpisodeFanartSize.Text = Me.lblFanartSize.Text
        Me.lblTVEpisodeFanartWidth.Text = Me.lblMoviePosterWidth.Text
        Me.lblTVEpisodePosterHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblTVEpisodePosterQ.Text = Me.lblMoviePosterQ.Text
        Me.lblTVEpisodePosterWidth.Text = Me.lblMoviePosterWidth.Text
        Me.lblTVSeasonFanartHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblTVSeasonFanartQ.Text = Me.lblMovieFanartQ.Text
        Me.lblTVSeasonFanartSize.Text = Me.lblFanartSize.Text
        Me.lblTVSeasonFanartWidth.Text = Me.lblMoviePosterWidth.Text
        Me.lblTVSeasonPosterHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblTVSeasonPosterQ.Text = Me.lblMoviePosterQ.Text
        Me.lblTVSeasonPosterSize.Text = Me.lblTVShowPosterSize.Text
        Me.lblTVSeasonPosterWidth.Text = Me.lblMoviePosterWidth.Text
        Me.lblTVShowFanartHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblTVShowFanartQ.Text = Me.lblMovieFanartQ.Text
        Me.lblTVShowFanartSize.Text = Me.lblFanartSize.Text
        Me.lblTVShowFanartWidth.Text = Me.lblMoviePosterWidth.Text
        Me.lblTVShowPosterHeight.Text = Me.lblMoviePosterHeight.Text
        Me.lblTVShowPosterQ.Text = Me.lblMoviePosterQ.Text
        Me.lblTVShowPosterSize.Text = Master.eLang.GetString(482, "Preferred Size:")
        Me.lblTVShowPosterWidth.Text = Me.lblMoviePosterWidth.Text
        Me.lblTVScraperUpdateTime.Text = Master.eLang.GetString(740, "Re-download Show Information Every:")
        Me.lblSettingsTopDetails.Text = Master.eLang.GetString(518, "Configure Ember's appearance and operation.")
        Me.lblSettingsTopTitle.Text = Me.Text
        Me.lvMovieSources.Columns(1).Text = Master.eLang.GetString(232, "Name")
        Me.lvMovieSources.Columns(2).Text = Master.eLang.GetString(410, "Path")
        Me.lvMovieSources.Columns(3).Text = Master.eLang.GetString(411, "Recursive")
        Me.lvMovieSources.Columns(4).Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.lvMovieSources.Columns(5).Text = Master.eLang.GetString(413, "Single Video")
        Me.lvTVShowRegex.Columns(1).Text = Master.eLang.GetString(696, "Show Regex")
        Me.lvTVShowRegex.Columns(2).Text = Master.eLang.GetString(694, "Apply To")
        Me.lvTVShowRegex.Columns(3).Text = Master.eLang.GetString(697, "Episode Regex")
        Me.lvTVShowRegex.Columns(4).Text = Master.eLang.GetString(694, "Apply To")
        Me.lvTVSources.Columns(1).Text = Master.eLang.GetString(232, "Name")
        Me.lvTVSources.Columns(2).Text = Master.eLang.GetString(410, "Path")
        Me.tpFileSystemCleanerExpert.Text = Master.eLang.GetString(439, "Expert")
        Me.tpFileSystemCleanerStandard.Text = Master.eLang.GetString(438, "Standard")
        Me.tpTVEpisode.Text = Master.eLang.GetString(701, "TV Episode")
        Me.tpTVSeason.Text = Master.eLang.GetString(744, "TV Season")
        Me.tpTVShow.Text = Master.eLang.GetString(700, "TV Show")
        Me.chkMovieYAMJCompatibleSets.Text = Master.eLang.GetString(561, "YAMJ Compatible Sets")

        Me.cbTVScraperUpdateTime.Items.Clear()
        Me.cbTVScraperUpdateTime.Items.AddRange(New String() {Master.eLang.GetString(749, "Week"), Master.eLang.GetString(750, "Bi-Weekly"), Master.eLang.GetString(751, "Month"), Master.eLang.GetString(752, "Never"), Master.eLang.GetString(753, "Always")})

        Me.cbTVScraperOptionsOrdering.Items.Clear()
        Me.cbTVScraperOptionsOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(350, "DVD"), Master.eLang.GetString(839, "Absolute")})

        Me.cbTVSeasonRetrieve.Items.Clear()
        Me.cbTVSeasonRetrieve.Items.AddRange(New String() {Master.eLang.GetString(13, "Folder Name"), Master.eLang.GetString(15, "File Name")})

        Me.cbTVEpisodeRetrieve.Items.Clear()
        Me.cbTVEpisodeRetrieve.Items.AddRange(New String() {Master.eLang.GetString(13, "Folder Name"), Master.eLang.GetString(15, "File Name"), Master.eLang.GetString(16, "Season Result")})
        Me.gbMovieGenrealIMDBMirrorOpts.Text = Master.eLang.GetString(885, "IMDB")

        Me.LoadGenericFanartSizes()
        Me.LoadGenericPosterSizes()
        Me.LoadMovieBannerTypes()
        Me.LoadMovieTrailerQualities()
        Me.LoadTVFanartSizes()
        Me.LoadTVPosterSizes()
        Me.LoadTVSeasonBannerTypes()
        Me.LoadTVShowBannerTypes()
    End Sub

    Private Sub tbTVASBannerQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVASBannerQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVASBannerQual.Text = tbTVASBannerQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVASBannerQual
            Select Case True
                Case tbTVASBannerQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVASBannerQual.Value > 95 OrElse tbTVASBannerQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVASBannerQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVASBannerQual.Value, 300 - tbTVASBannerQual.Value, 0)
                Case tbTVASBannerQual.Value >= 80 AndAlso tbTVASBannerQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVASBannerQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVASBannerQual.Value - 20)), 0)
                Case tbTVASBannerQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVASBannerQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVASPosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVASPosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVASPosterQual.Text = tbTVASPosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVASPosterQual
            Select Case True
                Case tbTVASPosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVASPosterQual.Value > 95 OrElse tbTVASPosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVASPosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVASPosterQual.Value, 300 - tbTVASPosterQual.Value, 0)
                Case tbTVASPosterQual.Value >= 80 AndAlso tbTVASPosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVASPosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVASPosterQual.Value - 20)), 0)
                Case tbTVASPosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVASPosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVASFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVASFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVASFanartQual.Text = tbTVASFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVASFanartQual
            Select Case True
                Case tbTVASFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVASFanartQual.Value > 95 OrElse tbTVASFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVASFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVASFanartQual.Value, 300 - tbTVASFanartQual.Value, 0)
                Case tbTVASFanartQual.Value >= 80 AndAlso tbTVASFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVASFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVASFanartQual.Value - 20)), 0)
                Case tbTVASFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVASFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVEpisodeFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVEpisodeFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVEpisodeFanartQual.Text = tbTVEpisodeFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVEpisodeFanartQual
            Select Case True
                Case tbTVEpisodeFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVEpisodeFanartQual.Value > 95 OrElse tbTVEpisodeFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVEpisodeFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVEpisodeFanartQual.Value, 300 - tbTVEpisodeFanartQual.Value, 0)
                Case tbTVEpisodeFanartQual.Value >= 80 AndAlso tbTVEpisodeFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVEpisodeFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVEpisodeFanartQual.Value - 20)), 0)
                Case tbTVEpisodeFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVEpisodeFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVEpisodePosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVEpisodePosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVEpisodePosterQual.Text = tbTVEpisodePosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVEpisodePosterQual
            Select Case True
                Case tbTVEpisodePosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVEpisodePosterQual.Value > 95 OrElse tbTVEpisodePosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVEpisodePosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVEpisodePosterQual.Value, 300 - tbTVEpisodePosterQual.Value, 0)
                Case tbTVEpisodePosterQual.Value >= 80 AndAlso tbTVEpisodePosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVEpisodePosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVEpisodePosterQual.Value - 20)), 0)
                Case tbTVEpisodePosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVEpisodePosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbMovieBannerQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbMovieBannerQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblMovieBannerQual.Text = tbMovieBannerQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblMovieBannerQual
            Select Case True
                Case tbMovieBannerQual.Value = 0
                    .ForeColor = Color.Black
                Case tbMovieBannerQual.Value > 95 OrElse tbMovieBannerQual.Value < 20
                    .ForeColor = Color.Red
                Case tbMovieBannerQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbMovieBannerQual.Value, 300 - tbMovieBannerQual.Value, 0)
                Case tbMovieBannerQual.Value >= 80 AndAlso tbMovieBannerQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbMovieBannerQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbMovieBannerQual.Value - 20)), 0)
                Case tbMovieBannerQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbMovieBannerQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbMovieEFanartsQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbMovieEFanartsQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblMovieEFanartsQual.Text = tbMovieEFanartsQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblMovieEFanartsQual
            Select Case True
                Case tbMovieEFanartsQual.Value = 0
                    .ForeColor = Color.Black
                Case tbMovieEFanartsQual.Value > 95 OrElse tbMovieEFanartsQual.Value < 20
                    .ForeColor = Color.Red
                Case tbMovieEFanartsQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbMovieEFanartsQual.Value, 300 - tbMovieEFanartsQual.Value, 0)
                Case tbMovieEFanartsQual.Value >= 80 AndAlso tbMovieEFanartsQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbMovieEFanartsQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbMovieEFanartsQual.Value - 20)), 0)
                Case tbMovieEFanartsQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbMovieEFanartsQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbMovieEThumbsQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbMovieEThumbsQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblMovieEThumbsQual.Text = tbMovieEThumbsQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblMovieEThumbsQual
            Select Case True
                Case tbMovieEThumbsQual.Value = 0
                    .ForeColor = Color.Black
                Case tbMovieEThumbsQual.Value > 95 OrElse tbMovieEThumbsQual.Value < 20
                    .ForeColor = Color.Red
                Case tbMovieEThumbsQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbMovieEThumbsQual.Value, 300 - tbMovieEThumbsQual.Value, 0)
                Case tbMovieEThumbsQual.Value >= 80 AndAlso tbMovieEThumbsQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbMovieEThumbsQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbMovieEThumbsQual.Value - 20)), 0)
                Case tbMovieEThumbsQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbMovieEThumbsQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbMovieFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbMovieFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblMovieFanartQual.Text = tbMovieFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblMovieFanartQual
            Select Case True
                Case tbMovieFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbMovieFanartQual.Value > 95 OrElse tbMovieFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbMovieFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbMovieFanartQual.Value, 300 - tbMovieFanartQual.Value, 0)
                Case tbMovieFanartQual.Value >= 80 AndAlso tbMovieFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbMovieFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbMovieFanartQual.Value - 20)), 0)
                Case tbMovieFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbMovieFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbMoviePosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbMoviePosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblMoviePosterQual.Text = tbMoviePosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblMoviePosterQual
            Select Case True
                Case tbMoviePosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbMoviePosterQual.Value > 95 OrElse tbMoviePosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbMoviePosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbMoviePosterQual.Value, 300 - tbMoviePosterQual.Value, 0)
                Case tbMoviePosterQual.Value >= 80 AndAlso tbMoviePosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbMoviePosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbMoviePosterQual.Value - 20)), 0)
                Case tbMoviePosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbMoviePosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVSeasonBannerQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVSeasonBannerQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVSeasonBannerQual.Text = tbTVSeasonBannerQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVSeasonBannerQual
            Select Case True
                Case tbTVSeasonBannerQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVSeasonBannerQual.Value > 95 OrElse tbTVSeasonBannerQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVSeasonBannerQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVSeasonBannerQual.Value, 300 - tbTVSeasonBannerQual.Value, 0)
                Case tbTVSeasonBannerQual.Value >= 80 AndAlso tbTVSeasonBannerQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVSeasonBannerQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVSeasonBannerQual.Value - 20)), 0)
                Case tbTVSeasonBannerQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVSeasonBannerQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVSeasonFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVSeasonFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVSeasonFanartQual.Text = tbTVSeasonFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVSeasonFanartQual
            Select Case True
                Case tbTVSeasonFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVSeasonFanartQual.Value > 95 OrElse tbTVSeasonFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVSeasonFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVSeasonFanartQual.Value, 300 - tbTVSeasonFanartQual.Value, 0)
                Case tbTVSeasonFanartQual.Value >= 80 AndAlso tbTVSeasonFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVSeasonFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVSeasonFanartQual.Value - 20)), 0)
                Case tbTVSeasonFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVSeasonFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVSeasonPosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVSeasonPosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVSeasonPosterQual.Text = tbTVSeasonPosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVSeasonPosterQual
            Select Case True
                Case tbTVSeasonPosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVSeasonPosterQual.Value > 95 OrElse tbTVSeasonPosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVSeasonPosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVSeasonPosterQual.Value, 300 - tbTVSeasonPosterQual.Value, 0)
                Case tbTVSeasonPosterQual.Value >= 80 AndAlso tbTVSeasonPosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVSeasonPosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVSeasonPosterQual.Value - 20)), 0)
                Case tbTVSeasonPosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVSeasonPosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVShowBannerQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVShowBannerQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVShowBannerQual.Text = tbTVShowBannerQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVShowBannerQual
            Select Case True
                Case tbTVShowBannerQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVShowBannerQual.Value > 95 OrElse tbTVShowBannerQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVShowBannerQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVShowBannerQual.Value, 300 - tbTVShowBannerQual.Value, 0)
                Case tbTVShowBannerQual.Value >= 80 AndAlso tbTVShowBannerQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVShowBannerQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVShowBannerQual.Value - 20)), 0)
                Case tbTVShowBannerQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVShowBannerQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVShowFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVShowFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVShowFanartQual.Text = tbTVShowFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVShowFanartQual
            Select Case True
                Case tbTVShowFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVShowFanartQual.Value > 95 OrElse tbTVShowFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVShowFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVShowFanartQual.Value, 300 - tbTVShowFanartQual.Value, 0)
                Case tbTVShowFanartQual.Value >= 80 AndAlso tbTVShowFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVShowFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVShowFanartQual.Value - 20)), 0)
                Case tbTVShowFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVShowFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbTVShowPosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTVShowPosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblTVShowPosterQual.Text = tbTVShowPosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblTVShowPosterQual
            Select Case True
                Case tbTVShowPosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbTVShowPosterQual.Value > 95 OrElse tbTVShowPosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbTVShowPosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbTVShowPosterQual.Value, 300 - tbTVShowPosterQual.Value, 0)
                Case tbTVShowPosterQual.Value >= 80 AndAlso tbTVShowPosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbTVShowPosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbTVShowPosterQual.Value - 20)), 0)
                Case tbTVShowPosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbTVShowPosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tcFileSystemCleaner_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcFileSystemCleaner.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'TODO: check why no Handles (maybe not needed)
        currText = DirectCast(sender, ToolStripButton).Text
        Me.FillList(currText)
    End Sub

    Private Sub tvSettingsList_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSettingsList.AfterSelect
        Me.pbSettingsCurrent.Image = Me.ilSettings.Images(Me.tvSettingsList.SelectedNode.ImageIndex)
        Me.lblSettingsCurrent.Text = String.Format("{0} - {1}", Me.currText, Me.tvSettingsList.SelectedNode.Text)

        Me.RemoveCurrPanel()

        Me.currPanel = Me.SettingsPanels.FirstOrDefault(Function(p) p.Name = tvSettingsList.SelectedNode.Name).Panel
        Me.currPanel.Location = New Point(0, 0)
        Me.pnlSettingsMain.Controls.Add(Me.currPanel)
        Me.currPanel.Visible = True
        Me.pnlSettingsMain.Refresh()
    End Sub

    Private Sub txtMovieScraperCastLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieScraperCastLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieScraperCastLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieScraperCastLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVASBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVASBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVASBannerHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVASBannerHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVASBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVASBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVASBannerWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVASBannerWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVASPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVASPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVASPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVASPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVASPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVASPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVASPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVASPosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVASFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVASFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVASFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVASFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVASFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVASFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVASFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVASFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieBackdropsPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieBackdropsPath.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieLevTolerance_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieLevTolerance.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieLevTolerance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieLevTolerance.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieScraperDefFIExt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieScraperDefFIExt.TextChanged
        btnMovieScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtMovieScraperDefFIExt.Text) AndAlso Not Me.lstMovieScraperDefFIExt.Items.Contains(If(txtMovieScraperDefFIExt.Text.StartsWith("."), txtMovieScraperDefFIExt.Text, String.Concat(".", txtMovieScraperDefFIExt.Text)))
        If btnMovieScraperDefFIExtAdd.Enabled Then
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub txtTVEpisodeFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVEpisodeFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVEpisodeFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVEpisodeFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVEpisodeFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVEpisodeFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVEpisodeFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVEpisodeFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVEpisodePosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVEpisodePosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVEpisodePosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVEpisodePosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVEpisodePosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVEpisodePosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVEpisodePosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVEpisodePosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVEpisodeRegex_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVEpisodeRegex.TextChanged
        Me.ValidateRegex()
    End Sub

    Private Sub txtMovieBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieBannerHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieBannerHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieBannerWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieBannerWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieEFanartsHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieEFanartsHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieEFanartsHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieEFanartsHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieEFanartsLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieEFanartsLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieEFanartsLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieEFanartsLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieEFanartsWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieEFanartsWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieEFanartsWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieEFanartsWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieEThumbsHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieEThumbsHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieEThumbsHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieEThumbsHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieEThumbsLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieEThumbsLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieEThumbsLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieEThumbsLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieEThumbsWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieEThumbsWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieEThumbsWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieEThumbsWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieScraperGenreLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieScraperGenreLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieScraperGenreLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieScraperGenreLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieScraperOutlineLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieScraperOutlineLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieScraperOutlineLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieScraperOutlineLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMoviePosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoviePosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMoviePosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMoviePosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoviePosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMoviePosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtProxyDomain_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProxyDomain.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtProxyPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProxyPassword.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtProxyPort_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProxyPort.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtProxyPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProxyPort.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtProxyUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProxyUsername.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieScraperDurationRuntimeFormat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieScraperDurationRuntimeFormat.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVScraperDurationRuntimeFormat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVScraperDurationRuntimeFormat.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVSeasonBannerHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSeasonBannerHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVSeasonBannerWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSeasonBannerWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVSeasonFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSeasonFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVSeasonFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSeasonFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVSeasonPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSeasonPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVSeasonPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSeasonPosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVSeasonRegex_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTVSeasonRegex.TextChanged
        Me.ValidateRegex()
    End Sub

    Private Sub txtTVShowBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowBannerHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowBannerHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowBannerWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowBannerWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowPosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSkipLessThan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSkipLessThan.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSkipLessThan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSkipLessThan.TextChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsUpdate = True
    End Sub

    Private Sub txtTVSkipLessThan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVSkipLessThan.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVSkipLessThan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSkipLessThan.TextChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsUpdate = True
    End Sub

    Private Sub txtTVScraperDefFIExt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVScraperDefFIExt.TextChanged
        btnTVScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtTVScraperDefFIExt.Text) AndAlso Not Me.lstTVScraperDefFIExt.Items.Contains(If(txtTVScraperDefFIExt.Text.StartsWith("."), txtTVScraperDefFIExt.Text, String.Concat(".", txtTVScraperDefFIExt.Text)))
        If btnTVScraperDefFIExtAdd.Enabled Then
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub txtMovieYAMJWatchedFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieYAMJWatchedFolder.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub ValidateRegex()
        If Not String.IsNullOrEmpty(Me.txtTVSeasonRegex.Text) AndAlso Not String.IsNullOrEmpty(Me.txtTVEpisodeRegex.Text) Then
            If Me.cbTVSeasonRetrieve.SelectedIndex > -1 AndAlso Me.cbTVEpisodeRetrieve.SelectedIndex > -1 Then
                Me.btnTVShowRegexAdd.Enabled = True
            Else
                Me.btnTVShowRegexAdd.Enabled = False
            End If
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

    Private Sub txtMovieMoviesetsPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieMoviesetsPath.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnMovieMoviesetsBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieMoviesetsBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieMoviesetsPath.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnMovieXBMCThemeCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieXBMCThemeCustomPathBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieXBMCThemeCustomPath.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnMovieYAMJWatchedFilesBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieYAMJWatchedFilesBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1029, "Select the folder where you wish to store your watched files...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieYAMJWatchedFolder.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub cbGeneralDaemonDrive_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGeneralDaemonDrive.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtGeneralDaemonPath_TextChanged(sender As Object, e As EventArgs) Handles txtGeneralDaemonPath.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnGeneralDaemonPathBrowse_Click(sender As Object, e As EventArgs) Handles btnGeneralDaemonPathBrowse.Click
        Try
            With Me.fileBrowse
                .Filter = "Exe (*.exe*)|*.exe*|Exe (*.exe*)|*.exe*"
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.FileName) Then
                        Me.txtGeneralDaemonPath.Text = .FileName

                    End If
                End If
            End With
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub chkMovieUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseExpert.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieActorThumbsExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieActorThumbsExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieActorThumbsExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieActorThumbsExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrafanartsExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrafanartsExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrafanartsExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrathumbsExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrathumbsExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrathumbsExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieRecognizeVTSExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieStackExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieStackExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieUnstackExpertMulti.Enabled = Me.chkMovieStackExpertMulti.Enabled AndAlso Me.chkMovieStackExpertMulti.Checked
        Me.chkMovieUnstackExpertSingle.Enabled = Me.chkMovieStackExpertSingle.Enabled AndAlso Me.chkMovieStackExpertSingle.Checked
        Me.chkMovieUseBaseDirectoryExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieUseBaseDirectoryExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
    End Sub
    Private Sub chkMovieActorThumbsExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieActorThumbsExpertSingle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieActorThumbsExtExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieActorThumbsExtExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieBannerExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieBannerExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearArtExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearArtExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearLogoExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearLogoExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieDiscArtExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieDiscArtExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieExtrafanartsExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrafanartsExpertSingle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieExtrathumbsExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrathumbsExpertSingle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieFanartExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieLandscapeExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieLandscapeExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieNFOExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieNFOExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMoviePosterExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieStackExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertSingle.CheckedChanged
        Me.chkMovieUnstackExpertSingle.Enabled = Me.chkMovieStackExpertSingle.Checked AndAlso Me.chkMovieStackExpertSingle.Enabled
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieTrailerExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieTrailerExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieUnstackExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUnstackExpertSingle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieActorThumbsExpertMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieActorThumbsExpertMulti.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieActorThumbsExtExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieActorThumbsExtExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieBannerExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieBannerExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearArtExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearArtExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearLogoExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearLogoExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieDiscArtExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieDiscArtExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieFanartExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieLandscapeExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieLandscapeExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieNFOExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieNFOExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMoviePosterExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieStackExpertMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertMulti.CheckedChanged
        Me.chkMovieUnstackExpertMulti.Enabled = Me.chkMovieStackExpertMulti.Checked AndAlso Me.chkMovieStackExpertMulti.Enabled
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieTrailerExpertMulti_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieTrailerExpertMulti.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieUnstackExpertMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUnstackExpertMulti.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieActorThumbsExpertVTS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieActorThumbsExpertVTS.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieActorThumbsExtExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieActorThumbsExtExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieBannerExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieBannerExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearArtExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearArtExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearLogoExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearLogoExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieDiscArtExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieDiscArtExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieExtrafanartsExpertVTS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrafanartsExpertVTS.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieExtrathumbsExpertVTS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrathumbsExpertVTS.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieFanartExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieLandscapeExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieLandscapeExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieNFOExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieNFOExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMoviePosterExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieRecognizeVTSExpertVTS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieRecognizeVTSExpertVTS.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieTrailerExpertVTS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieTrailerExpertVTS.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieUseBaseDirectoryExpertVTS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseBaseDirectoryExpertVTS.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieActorThumbsExpertBDMV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieActorThumbsExpertBDMV.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieActorThumbsExtExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieActorThumbsExtExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieBannerExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieBannerExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearArtExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearArtExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieClearLogoExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieClearLogoExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieDiscArtExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieDiscArtExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieExtrafanartsExpertBDMV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrafanartsExpertBDMV.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieExtrathumbsExpertBDMV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrathumbsExpertBDMV.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieFanartExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieLandscapeExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieLandscapeExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieNFOExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieNFOExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMoviePosterExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieTrailerExpertBDMV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieTrailerExpertBDMV.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieUseBaseDirectoryExpertBDMV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseBaseDirectoryExpertBDMV.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieYAMJCompatibleSets_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieYAMJCompatibleSets.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseBoxee.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodePosterBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVSeasonPosterBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowBannerBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowFanartBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowPosterBoxee.Enabled = Me.chkTVUseBoxee.Checked

        If Not Me.chkTVUseBoxee.Checked Then
            Me.chkTVEpisodePosterBoxee.Checked = False
            Me.chkTVSeasonPosterBoxee.Checked = False
            Me.chkTVShowBannerBoxee.Checked = False
            Me.chkTVShowFanartBoxee.Checked = False
            Me.chkTVShowPosterBoxee.Checked = False
        Else
            Me.chkTVEpisodePosterBoxee.Checked = True
            Me.chkTVSeasonPosterBoxee.Checked = True
            Me.chkTVShowBannerBoxee.Checked = True
            Me.chkTVShowFanartBoxee.Checked = True
            Me.chkTVShowPosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkTVEpisodePosterBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodePosterBoxee.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonPosterBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonPosterBoxee.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowBannerBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowBannerBoxee.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowFanartBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowFanartBoxee.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowPosterBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowPosterBoxee.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseFrodo.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodeActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVEpisodePosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonLandscapeXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowCharacterArtXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearArtXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearLogoXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowLandscapeXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowTVThemeXBMC.Enabled = Me.chkTVUseFrodo.Checked

        If Not Me.chkTVUseFrodo.Checked Then
            Me.chkTVEpisodeActorThumbsFrodo.Checked = False
            Me.chkTVEpisodePosterFrodo.Checked = False
            Me.chkTVSeasonBannerFrodo.Checked = False
            Me.chkTVSeasonFanartFrodo.Checked = False
            Me.chkTVSeasonLandscapeXBMC.Checked = False
            Me.chkTVSeasonPosterFrodo.Checked = False
            Me.chkTVShowActorThumbsFrodo.Checked = False
            Me.chkTVShowBannerFrodo.Checked = False
            Me.chkTVShowCharacterArtXBMC.Checked = False
            Me.chkTVShowClearArtXBMC.Checked = False
            Me.chkTVShowClearLogoXBMC.Checked = False
            Me.chkTVShowFanartFrodo.Checked = False
            Me.chkTVShowLandscapeXBMC.Checked = False
            Me.chkTVShowPosterFrodo.Checked = False
            Me.chkTVShowTVThemeXBMC.Checked = False
        Else
            Me.chkTVEpisodeActorThumbsFrodo.Checked = True
            Me.chkTVEpisodePosterFrodo.Checked = True
            Me.chkTVSeasonBannerFrodo.Checked = True
            Me.chkTVSeasonFanartFrodo.Checked = True
            Me.chkTVSeasonLandscapeXBMC.Checked = True
            Me.chkTVSeasonPosterFrodo.Checked = True
            Me.chkTVShowActorThumbsFrodo.Checked = True
            Me.chkTVShowBannerFrodo.Checked = True
            Me.chkTVShowCharacterArtXBMC.Checked = True
            Me.chkTVShowClearArtXBMC.Checked = True
            Me.chkTVShowClearLogoXBMC.Checked = True
            Me.chkTVShowFanartFrodo.Checked = True
            Me.chkTVShowLandscapeXBMC.Checked = True
            Me.chkTVShowPosterFrodo.Checked = True
            'Me.chkTVShowTVThemeXBMC.Checked = True
        End If
    End Sub
    Private Sub chkTVEpisodeActorThumbsFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeActorThumbsFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVEpisodePosterFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodePosterFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonBannerFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonBannerFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonFanartFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonFanartFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonLandscapeXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonLandscapeXBMC.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonPosterFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonPosterFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowActorThumbsFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowActorThumbsFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowBannerFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowBannerFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowCharacterArtXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowCharacterArtXBMC.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowClearArtXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowClearArtXBMC.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowClearLogoXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowClearLogoXBMC.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowFanartFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowFanartFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowLandscapeXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowLandscapeXBMC.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowPosterFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowPosterFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowTVThemeXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowTVThemeXBMC.CheckedChanged
        Me.SetApplyButton(True)

        Me.btnTVShowTVThemeBrowse.Enabled = Me.chkTVShowTVThemeXBMC.Checked
        Me.txtTVShowTVThemeFolderXBMC.Enabled = Me.chkTVShowTVThemeXBMC.Checked
    End Sub

    Private Sub txtTVShowTVThemeFolderXBMC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowTVThemeFolderXBMC.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVShowTVThemeBrowse_Click(sender As Object, e As EventArgs) Handles btnTVShowTVThemeBrowse.Click
        With Me.fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(1028, "Select the folder where you wish to store your tv themes...")
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtTVShowTVThemeFolderXBMC.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub chkTVUseYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseYAMJ.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodePosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonBannerYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonFanartYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonPosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowBannerYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowFanartYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowPosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked

        If Not Me.chkTVUseYAMJ.Checked Then
            Me.chkTVEpisodePosterYAMJ.Checked = False
            Me.chkTVSeasonBannerYAMJ.Checked = False
            Me.chkTVSeasonFanartYAMJ.Checked = False
            Me.chkTVSeasonPosterYAMJ.Checked = False
            Me.chkTVShowBannerYAMJ.Checked = False
            Me.chkTVShowFanartYAMJ.Checked = False
            Me.chkTVShowPosterYAMJ.Checked = False
        Else
            Me.chkTVEpisodePosterYAMJ.Checked = True
            Me.chkTVSeasonBannerYAMJ.Checked = True
            Me.chkTVSeasonFanartYAMJ.Checked = True
            Me.chkTVSeasonPosterYAMJ.Checked = True
            Me.chkTVShowBannerYAMJ.Checked = True
            Me.chkTVShowFanartYAMJ.Checked = True
            Me.chkTVShowPosterYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkTVEpisodePosterYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodePosterYAMJ.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonBannerYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonBannerYAMJ.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonFanartYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonFanartYAMJ.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVSeasonPosterYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonPosterYAMJ.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowBannerYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowBannerYAMJ.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowFanartYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowFanartYAMJ.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowPosterYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowPosterYAMJ.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

#End Region 'Methods

End Class