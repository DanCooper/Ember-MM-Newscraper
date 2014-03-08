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
' #
' # Dialog size: 1024, 768
' # Panel size: 750, 500
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
    Private Meta As New List(Of Settings.MetadataPerType)
    Private NoUpdate As Boolean = True
    Private SettingsPanels As New List(Of Containers.SettingsPanel)
    Private ShowRegex As New List(Of Settings.TVShowRegEx)
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

        Me.ToolStrip1.Items.Clear()

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

            Dim sSpacer As String = New String(Convert.ToChar(" "), Convert.ToInt32(((Me.ToolStrip1.Width - ButtonsWidth) / (TSBs.Count + 1)) / spacerMod))

            'add it all
            For Each tButton As ToolStripButton In TSBs.OrderBy(Function(b) Convert.ToInt32(b.Tag))
                If sSpacer.Length > 0 Then Me.ToolStrip1.Items.Add(New ToolStripLabel With {.Text = sSpacer})
                Me.ToolStrip1.Items.Add(tButton)
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
             .Panel = Me.pnlExtensions, _
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

        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalProcessorModules
            RemoveHandler s.ProcessorModule.ModuleSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Next
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        Me.sResult.NeedsRestart = True
    End Sub

    Private Sub btnAddEpFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEpFilter.Click
        If Not String.IsNullOrEmpty(Me.txtEpFilter.Text) Then
            Me.lstEpFilters.Items.Add(Me.txtEpFilter.Text)
            Me.txtEpFilter.Text = String.Empty
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If

        Me.txtEpFilter.Focus()
    End Sub

    Private Sub btnAddFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFilter.Click
        If Not String.IsNullOrEmpty(Me.txtFilter.Text) Then
            Me.lstFilters.Items.Add(Me.txtFilter.Text)
            Me.txtFilter.Text = String.Empty
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If

        Me.txtFilter.Focus()
    End Sub

    Private Sub btnAddMovieExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMovieExt.Click
        If Not String.IsNullOrEmpty(txtMovieExt.Text) Then
            If Not Strings.Left(txtMovieExt.Text, 1) = "." Then txtMovieExt.Text = String.Concat(".", txtMovieExt.Text)
            If Not lstMovieExts.Items.Contains(txtMovieExt.Text.ToLower) Then
                lstMovieExts.Items.Add(txtMovieExt.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
                txtMovieExt.Text = String.Empty
                txtMovieExt.Focus()
            End If
        End If
    End Sub

    Private Sub btnAddNoStack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNoStack.Click
        If Not String.IsNullOrEmpty(txtNoStack.Text) Then
            If Not Strings.Left(txtNoStack.Text, 1) = "." Then txtNoStack.Text = String.Concat(".", txtNoStack.Text)
            If Not lstNoStack.Items.Contains(txtNoStack.Text) Then
                lstNoStack.Items.Add(txtNoStack.Text)
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
                txtNoStack.Text = String.Empty
                txtNoStack.Focus()
            End If
        End If
    End Sub

    Private Sub btnAddShowFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddShowFilter.Click
        If Not String.IsNullOrEmpty(Me.txtShowFilter.Text) Then
            Me.lstShowFilters.Items.Add(Me.txtShowFilter.Text)
            Me.txtShowFilter.Text = String.Empty
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If

        Me.txtShowFilter.Focus()
    End Sub

    Private Sub btnAddShowRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddShowRegex.Click
        If String.IsNullOrEmpty(Me.btnAddShowRegex.Tag.ToString) Then
            Dim lID = (From lRegex As Settings.TVShowRegEx In Me.ShowRegex Select lRegex.ID).Max
            Me.ShowRegex.Add(New Settings.TVShowRegEx With {.ID = Convert.ToInt32(lID) + 1, .SeasonRegex = Me.txtSeasonRegex.Text, .SeasonFromDirectory = Not Convert.ToBoolean(Me.cboSeasonRetrieve.SelectedIndex), .EpisodeRegex = Me.txtEpRegex.Text, .EpisodeRetrieve = DirectCast(Me.cboEpRetrieve.SelectedIndex, Settings.EpRetrieve)})
        Else
            Dim selRex = From lRegex As Settings.TVShowRegEx In Me.ShowRegex Where lRegex.ID = Convert.ToInt32(Me.btnAddShowRegex.Tag)
            If selRex.Count > 0 Then
                selRex(0).SeasonRegex = Me.txtSeasonRegex.Text
                selRex(0).SeasonFromDirectory = Not Convert.ToBoolean(Me.cboSeasonRetrieve.SelectedIndex)
                selRex(0).EpisodeRegex = Me.txtEpRegex.Text
                selRex(0).EpisodeRetrieve = DirectCast(Me.cboEpRetrieve.SelectedIndex, Settings.EpRetrieve)
            End If
        End If

        Me.ClearRegex()
        Me.SetApplyButton(True)
        Me.LoadShowRegex()
    End Sub

    Private Sub btnAddToken_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToken.Click
        If Not String.IsNullOrEmpty(txtSortToken.Text) Then
            If Not lstSortTokens.Items.Contains(txtSortToken.Text) Then
                lstSortTokens.Items.Add(txtSortToken.Text)
                Me.sResult.NeedsRefresh = True
                Me.SetApplyButton(True)
                txtSortToken.Text = String.Empty
                txtSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnAddTVSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTVSource.Click
        Using dSource As New dlgTVSource
            If dSource.ShowDialog = Windows.Forms.DialogResult.OK Then
                RefreshTVSources()
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
            End If
        End Using
    End Sub

    Private Sub btnAddWhitelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddWhitelist.Click
        If Not String.IsNullOrEmpty(Me.txtWhitelist.Text) Then
            If Not Strings.Left(txtWhitelist.Text, 1) = "." Then txtWhitelist.Text = String.Concat(".", txtWhitelist.Text)
            If Not lstWhitelist.Items.Contains(txtWhitelist.Text.ToLower) Then
                lstWhitelist.Items.Add(txtWhitelist.Text.ToLower)
                Me.SetApplyButton(True)
                txtWhitelist.Text = String.Empty
                txtWhitelist.Focus()
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

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseBackdrops.Click
        With Me.fbdBrowse
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtBDPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Not didApply Then sResult.DidCancel = True
        RemoveScraperPanels()
        Me.Close()
    End Sub

    Private Sub btnClearRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearRegex.Click
        Me.ClearRegex()
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Try
            If Me.lstFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstFilters.SelectedItem) AndAlso Me.lstFilters.SelectedIndex < (Me.lstFilters.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstFilters.SelectedIndices(0)
                Me.lstFilters.Items.Insert(iIndex + 2, Me.lstFilters.SelectedItems(0))
                Me.lstFilters.Items.RemoveAt(iIndex)
                Me.lstFilters.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnEditMetaDataFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditMetaDataFT.Click
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            For Each x As Settings.MetadataPerType In Meta
                If x.FileType = lstMetaData.SelectedItems(0).ToString Then
                    fi = dEditMeta.ShowDialog(x.MetaData, False)
                    If Not fi Is Nothing Then
                        Meta.Remove(x)
                        Dim m As New Settings.MetadataPerType
                        m.FileType = x.FileType
                        m.MetaData = New MediaInfo.Fileinfo
                        m.MetaData = fi
                        Meta.Add(m)
                        LoadMetadata()
                        Me.SetApplyButton(True)
                    End If
                    Exit For
                End If
            Next
        End Using
    End Sub

    Private Sub btnEditShowRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditShowRegex.Click
        If Me.lvShowRegex.SelectedItems.Count > 0 Then Me.EditShowRegex(lvShowRegex.SelectedItems(0))
    End Sub

    Private Sub btnEditSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSource.Click
        If lvMovies.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgMovieSource
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovies.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshSources()
                    Me.sResult.NeedsUpdate = True
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub btnEditTVMetaDataFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditTVMetaDataFT.Click
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            For Each x As Settings.MetadataPerType In TVMeta
                If x.FileType = lstTVMetaData.SelectedItems(0).ToString Then
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

    Private Sub btnEditTVSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditTVSource.Click
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

    Private Sub btnEpFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEpFilterDown.Click
        Try
            If Me.lstEpFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstEpFilters.SelectedItem) AndAlso Me.lstEpFilters.SelectedIndex < (Me.lstEpFilters.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstEpFilters.SelectedIndices(0)
                Me.lstEpFilters.Items.Insert(iIndex + 2, Me.lstEpFilters.SelectedItems(0))
                Me.lstEpFilters.Items.RemoveAt(iIndex)
                Me.lstEpFilters.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstEpFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnEpFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEpFilterUp.Click
        Try
            If Me.lstEpFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstEpFilters.SelectedItem) AndAlso Me.lstEpFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstEpFilters.SelectedIndices(0)
                Me.lstEpFilters.Items.Insert(iIndex - 1, Me.lstEpFilters.SelectedItems(0))
                Me.lstEpFilters.Items.RemoveAt(iIndex + 1)
                Me.lstEpFilters.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstEpFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnMovieAddFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieAddFolder.Click
        Using dSource As New dlgMovieSource
            If dSource.ShowDialog = Windows.Forms.DialogResult.OK Then
                RefreshSources()
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
            End If
        End Using
    End Sub

    Private Sub btnMovieRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieRem.Click
        Me.RemoveMovieSource()
        Master.DB.LoadMovieSourcesFromDB()
    End Sub

    Private Sub btnNewMetaDataFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewMetaDataFT.Click
        If Not txtDefFIExt.Text.StartsWith(".") Then txtDefFIExt.Text = String.Concat(".", txtDefFIExt.Text)
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            fi = dEditMeta.ShowDialog(fi, False)
            If Not fi Is Nothing Then
                Dim m As New Settings.MetadataPerType
                m.FileType = txtDefFIExt.Text
                m.MetaData = New MediaInfo.Fileinfo
                m.MetaData = fi
                Meta.Add(m)
                LoadMetadata()
                Me.SetApplyButton(True)
                Me.txtDefFIExt.Text = String.Empty
                Me.txtDefFIExt.Focus()
            End If
        End Using
    End Sub

    Private Sub btnNewTVMetaDataFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewTVMetaDataFT.Click
        If Not txtTVDefFIExt.Text.StartsWith(".") Then txtTVDefFIExt.Text = String.Concat(".", txtTVDefFIExt.Text)
        Using dEditMeta As New dlgFileInfo
            Dim fi As New MediaInfo.Fileinfo
            fi = dEditMeta.ShowDialog(fi, True)
            If Not fi Is Nothing Then
                Dim m As New Settings.MetadataPerType
                m.FileType = txtTVDefFIExt.Text
                m.MetaData = New MediaInfo.Fileinfo
                m.MetaData = fi
                TVMeta.Add(m)
                LoadTVMetadata()
                Me.SetApplyButton(True)
                Me.txtTVDefFIExt.Text = String.Empty
                Me.txtTVDefFIExt.Focus()
            End If
        End Using
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        NoUpdate = True
        Me.SaveSettings(False)
        RemoveScraperPanels()
        Me.Close()
    End Sub

    Private Sub btnRegexUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegexUp.Click
        Try
            If Me.lvShowRegex.Items.Count > 0 AndAlso Me.lvShowRegex.SelectedItems.Count > 0 AndAlso Not Me.lvShowRegex.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.TVShowRegEx = Me.ShowRegex.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(Me.lvShowRegex.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvShowRegex.SuspendLayout()
                    Dim iIndex As Integer = Me.ShowRegex.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvShowRegex.SelectedIndices(0)
                    Me.ShowRegex.Remove(selItem)
                    Me.ShowRegex.Insert(iIndex - 1, selItem)

                    Me.RenumberRegex()
                    Me.LoadShowRegex()

                    Me.lvShowRegex.Items(selIndex - 1).Selected = True
                    Me.lvShowRegex.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvShowRegex.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnRegexDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegexDown.Click
        Try
            If Me.lvShowRegex.Items.Count > 0 AndAlso Me.lvShowRegex.SelectedItems.Count > 0 AndAlso Me.lvShowRegex.SelectedItems(0).Index < (Me.lvShowRegex.Items.Count - 1) Then
                Dim selItem As Settings.TVShowRegEx = Me.ShowRegex.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(Me.lvShowRegex.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvShowRegex.SuspendLayout()
                    Dim iIndex As Integer = Me.ShowRegex.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvShowRegex.SelectedIndices(0)
                    Me.ShowRegex.Remove(selItem)
                    Me.ShowRegex.Insert(iIndex + 1, selItem)

                    Me.RenumberRegex()
                    Me.LoadShowRegex()

                    Me.lvShowRegex.Items(selIndex + 1).Selected = True
                    Me.lvShowRegex.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvShowRegex.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnResetShowFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetShowFilters.Click
        If MsgBox(Master.eLang.GetString(840, "Are you sure you want to reset to the default list of show filters?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ShowFilters, True)
            Me.RefreshShowFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnResetEpFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetEpFilter.Click
        If MsgBox(Master.eLang.GetString(841, "Are you sure you want to reset to the default list of episode filters?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.EpFilters, True)
            Me.RefreshEpFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnResetMovieFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetMovieFilters.Click
        If MsgBox(Master.eLang.GetString(842, "Are you sure you want to reset to the default list of movie filters?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieFilters, True)
            Me.RefreshMovieFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnResetValidExts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetValidExts.Click
        If MsgBox(Master.eLang.GetString(843, "Are you sure you want to reset to the default list of valid video extensions?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidExts, True)
            Me.RefreshValidExts()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnGetTVProfiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTVProfiles.Click
        Using dd As New dlgTVRegExProfiles
            If dd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.ShowRegex.Clear()
                Me.ShowRegex.AddRange(dd.ShowRegex)
                Me.LoadShowRegex()
                Me.SetApplyButton(True)
            End If
        End Using
    End Sub

    Private Sub btnResetShowRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetShowRegex.Click
        If MsgBox(Master.eLang.GetString(844, "Are you sure you want to reset to the default list of show regex?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ShowRegex, True)
            Me.ShowRegex.Clear()
            Me.ShowRegex.AddRange(Master.eSettings.TVShowRegexes)
            Me.LoadShowRegex()
            Me.SetApplyButton(True)
        End If

    End Sub

    Private Sub btnRemMovieExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemMovieExt.Click
        Me.RemoveMovieExt()
    End Sub

    Private Sub btnRemoveEpFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveEpFilter.Click
        Me.RemoveEpFilter()
    End Sub

    Private Sub btnRemoveFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFilter.Click
        Me.RemoveFilter()
    End Sub

    Private Sub btnRemoveMetaDataFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMetaDataFT.Click
        Me.RemoveMetaData()
    End Sub

    Private Sub btnRemoveNoStack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveNoStack.Click
        Me.RemoveNoStack()
    End Sub

    Private Sub btnRemoveShowFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowFilter.Click
        Me.RemoveShowFilter()
    End Sub

    Private Sub btnRemoveShowRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowRegex.Click
        Me.RemoveRegex()
    End Sub

    Private Sub btnRemoveToken_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveToken.Click
        Me.RemoveSortToken()
    End Sub

    Private Sub btnRemoveTVMetaDataFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTVMetaDataFT.Click
        Me.RemoveTVMetaData()
    End Sub

    Private Sub btnRemoveWhitelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveWhitelist.Click
        If Me.lstWhitelist.Items.Count > 0 AndAlso Me.lstWhitelist.SelectedItems.Count > 0 Then
            While Me.lstWhitelist.SelectedItems.Count > 0
                lstWhitelist.Items.Remove(Me.lstWhitelist.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnRemTVSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTVSource.Click
        Me.RemoveTVSource()
        Master.DB.LoadTVSourcesFromDB()
    End Sub

    Private Sub btnShowFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowFilterDown.Click
        Try
            If Me.lstShowFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstShowFilters.SelectedItem) AndAlso Me.lstShowFilters.SelectedIndex < (Me.lstShowFilters.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstShowFilters.SelectedIndices(0)
                Me.lstShowFilters.Items.Insert(iIndex + 2, Me.lstShowFilters.SelectedItems(0))
                Me.lstShowFilters.Items.RemoveAt(iIndex)
                Me.lstShowFilters.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstShowFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnShowFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowFilterUp.Click
        Try
            If Me.lstShowFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstShowFilters.SelectedItem) AndAlso Me.lstShowFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstShowFilters.SelectedIndices(0)
                Me.lstShowFilters.Items.Insert(iIndex - 1, Me.lstShowFilters.SelectedItems(0))
                Me.lstShowFilters.Items.RemoveAt(iIndex + 1)
                Me.lstShowFilters.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstShowFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Try
            If Me.lstFilters.Items.Count > 0 AndAlso Not IsNothing(Me.lstFilters.SelectedItem) AndAlso Me.lstFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstFilters.SelectedIndices(0)
                Me.lstFilters.Items.Insert(iIndex - 1, Me.lstFilters.SelectedItems(0))
                Me.lstFilters.Items.RemoveAt(iIndex + 1)
                Me.lstFilters.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsRefresh = True
                Me.lstFilters.Focus()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub cbCert_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCert.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbEFanartsSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieEFanartsSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbEThumbsSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieEThumbsSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbFanartSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieFanartSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbForce_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbForce.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbIntLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbIntLang.SelectedIndexChanged
        Me.SetApplyButton(True)
        If Not Me.cbIntLang.SelectedItem.ToString = Master.eSettings.Language Then
            Handle_SetupNeedsRestart()
        End If
    End Sub

    Private Sub cbLanguages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLanguages.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieTheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTheme.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbOrdering_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrdering.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cboEpRetrieve_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEpRetrieve.SelectedIndexChanged
        Me.ValidateRegex()
    End Sub

    Private Sub cboSeasonRetrieve_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeasonRetrieve.SelectedIndexChanged
        Me.ValidateRegex()
    End Sub

    Private Sub cboTVMetaDataOverlay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTVMetaDataOverlay.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cboTVUpdate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTVUpdate.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbPosterSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMoviePosterSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbRatingRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRatingRegion.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbSeaFanartSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeaFanartSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbSeaPosterSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeaPosterSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbShowFanartSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowFanartSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTrailerQuality_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTrailerQuality.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVEpTheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEpTheme.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVShowTheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVShowTheme.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chAllSPosterSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAllSPosterSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chEpFanartSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEpFanartSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClickScrape.CheckedChanged
        chkAskCheckboxScrape.Enabled = chkClickScrape.Checked
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkAskCheckboxScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAskCheckboxScrape.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkAutoBD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoBD.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtBDPath.Enabled = chkAutoBD.Checked
        Me.btnBrowseBackdrops.Enabled = chkAutoBD.Checked
    End Sub

    Private Sub chkCastWithImg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCastWithImg.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCast.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkFullCast.Enabled = Me.chkCast.Checked
        Me.chkCastWithImg.Enabled = Me.chkCast.Checked
        Me.txtActorLimit.Enabled = Me.chkCast.Checked

        If Not chkCast.Checked Then
            Me.chkFullCast.Checked = False
            Me.chkCastWithImg.Checked = False
            Me.txtActorLimit.Text = "0"
        End If
    End Sub

    Private Sub chkCertification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCertification.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCert.CheckedChanged
        Me.cbCert.SelectedIndex = -1
        Me.cbCert.Enabled = Me.chkCert.Checked
        Me.chkUseCertForMPAA.Enabled = Me.chkCert.Checked
        If Not Me.chkCert.Checked Then
            Me.chkUseCertForMPAA.Checked = False

            Me.chkOnlyValueForCert.Checked = False
            Me.chkOnlyValueForCert.Enabled = False
            Me.chkUseMPAAFSK.Checked = False
            Me.chkUseMPAAFSK.Enabled = False
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCheckTitles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckTitles.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtCheckTitleTol.Enabled = Me.chkCheckTitles.Checked
        If Not Me.chkCheckTitles.Checked Then Me.txtCheckTitleTol.Text = String.Empty
    End Sub

    Private Sub chkCleanDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCleanDB.CheckedChanged
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

    Private Sub chkCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCrew.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDirector.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkDisplayAllSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisplayAllSeason.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkDisplayMissingEpisodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisplayMissingEpisodes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkDisplayYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisplayYear.CheckedChanged
        Me.sResult.NeedsRefresh = True
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkDownloadTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDownloadTrailer.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkOverwriteTrailer.Enabled = Me.chkDownloadTrailer.Checked
        Me.chkDeleteAllTrailers.Enabled = Me.chkDownloadTrailer.Checked

        If Not Me.chkDownloadTrailer.Checked Then
            Me.chkOverwriteTrailer.Checked = False
            Me.chkDeleteAllTrailers.Checked = False
            Me.cbTrailerQuality.Enabled = False
            'Me.cbTrailerQuality.SelectedIndex = -1
        Else
            Me.cbTrailerQuality.Enabled = True
            'Me.cbTrailerQuality.SelectedValue = Master.eSettings.PreferredTrailerQuality
        End If
    End Sub

    Private Sub chkEnableCredentials_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableCredentials.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtProxyUsername.Enabled = Me.chkEnableCredentials.Checked
        Me.txtProxyPassword.Enabled = Me.chkEnableCredentials.Checked
        Me.txtProxyDomain.Enabled = Me.chkEnableCredentials.Checked

        If Not Me.chkEnableCredentials.Checked Then
            Me.txtProxyUsername.Text = String.Empty
            Me.txtProxyPassword.Text = String.Empty
            Me.txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkEnableProxy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableProxy.CheckedChanged
        Me.SetApplyButton(True)
        Me.txtProxyURI.Enabled = Me.chkEnableProxy.Checked
        Me.txtProxyPort.Enabled = Me.chkEnableProxy.Checked
        Me.gbCreds.Enabled = Me.chkEnableProxy.Checked

        If Not Me.chkEnableProxy.Checked Then
            Me.txtProxyURI.Text = String.Empty
            Me.txtProxyPort.Text = String.Empty
            Me.chkEnableCredentials.Checked = False
            Me.txtProxyUsername.Text = String.Empty
            Me.txtProxyPassword.Text = String.Empty
            Me.txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkEpisodeFanartCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEpisodeFanartCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEpisodeNfoCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEpisodeNfoCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEpisodePosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEpisodePosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEpLockPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEpLockPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEpLockRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEpLockRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEpLockTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEpLockTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEpProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEpProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh = True
    End Sub

    Private Sub chkPosterOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviePosterOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEFanartsOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEFanartsOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkEThumbsOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEThumbsOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkFanartOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieFanartOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkForceTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkForceTitle.CheckedChanged
        Me.cbForce.SelectedIndex = -1
        Me.cbForce.Enabled = Me.chkForceTitle.Checked
        Me.chkTitleFallback.Enabled = Me.chkForceTitle.Checked
        If Me.chkForceTitle.Checked = False Then
            Me.chkTitleFallback.Checked = False
            Me.chkTitleFallback.Enabled = False
        Else
            Me.chkTitleFallback.Enabled = True
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTitleFallback_CheckedChanged(sender As Object, e As EventArgs) Handles chkTitleFallback.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkFullCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFullCast.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkFullCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFullCrew.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkProducers.Enabled = Me.chkFullCrew.Checked
        Me.chkMusicBy.Enabled = Me.chkFullCrew.Checked
        Me.chkCrew.Enabled = Me.chkFullCrew.Checked

        If Not Me.chkFullCrew.Checked Then
            Me.chkProducers.Checked = False
            Me.chkMusicBy.Checked = False
            Me.chkCrew.Checked = False
        End If
    End Sub

    Private Sub chkGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGenre.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtGenreLimit.Enabled = Me.chkGenre.Checked

        If Not Me.chkGenre.Checked Then Me.txtGenreLimit.Text = "0"
    End Sub

    Private Sub chkIFOScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIFOScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkIgnoreLastScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIgnoreLastScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkInfoPanelAnim_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInfoPanelAnim.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockGenre.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockOutline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockRealStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockRealStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockTagline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkLockTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLockTrailer.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMarkNewEpisodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMarkNewEpisodes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMarkNewShows_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMarkNewShows.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMarkNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMarkNew.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMissingEThumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMissingEThumbs.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMissingEFanarts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMissingEFanarts.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMissingFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMissingFanart.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMissingNFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMissingNFO.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMissingPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMissingPoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMissingSubs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMissingSubs.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMissingTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMissingTrailer.CheckedChanged
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

    Private Sub chkMovieInfoCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieInfoCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMoviePosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviePosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSubCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSubCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieTrailerCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieTrailerCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieWatchedCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieWatchedCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMPAA.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkCert.Enabled = Me.chkMPAA.Checked

        If Not Me.chkMPAA.Checked Then
            Me.chkCert.Checked = False
            Me.cbCert.Enabled = False
            Me.cbCert.SelectedIndex = -1
            Me.chkUseCertForMPAA.Enabled = False
            Me.chkUseCertForMPAA.Checked = False
        End If
    End Sub

    Private Sub chkMusicBy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMusicBy.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkNoDisplayFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNoDisplayFanart.CheckedChanged
        Me.SetApplyButton(True)
        If Me.chkNoDisplayFanart.Checked AndAlso Me.chkNoDisplayPoster.Checked AndAlso Me.chkNoDisplayFanartSmall.Checked Then
            Me.chkShowDims.Enabled = False
            Me.chkShowDims.Checked = False
        Else
            Me.chkShowDims.Enabled = True
        End If
    End Sub

    Private Sub chkNoDisplayPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNoDisplayPoster.CheckedChanged
        Me.SetApplyButton(True)
        If Me.chkNoDisplayFanart.Checked AndAlso Me.chkNoDisplayPoster.Checked AndAlso Me.chkNoDisplayFanartSmall.Checked Then
            Me.chkShowDims.Enabled = False
            Me.chkShowDims.Checked = False
        Else
            Me.chkShowDims.Enabled = True
        End If
    End Sub

    Private Sub chkNoDisplayFanartSmall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNoDisplayFanartSmall.CheckedChanged
        Me.SetApplyButton(True)
        If Me.chkNoDisplayFanart.Checked AndAlso Me.chkNoDisplayPoster.Checked AndAlso Me.chkNoDisplayFanartSmall.Checked Then
            Me.chkShowDims.Enabled = False
            Me.chkShowDims.Checked = False
        Else
            Me.chkShowDims.Enabled = True
        End If
    End Sub

    Private Sub chkNoFilterEpisode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNoFilterEpisode.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkEpProperCase.Enabled = Not Me.chkNoFilterEpisode.Checked
        Me.lstEpFilters.Enabled = Not Me.chkNoFilterEpisode.Checked
        Me.txtEpFilter.Enabled = Not Me.chkNoFilterEpisode.Checked
        Me.btnAddEpFilter.Enabled = Not Me.chkNoFilterEpisode.Checked
        Me.btnEpFilterUp.Enabled = Not Me.chkNoFilterEpisode.Checked
        Me.btnEpFilterDown.Enabled = Not Me.chkNoFilterEpisode.Checked
        Me.btnRemoveEpFilter.Enabled = Not Me.chkNoFilterEpisode.Checked
    End Sub

    Private Sub chkNoSaveImagesToNfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieNoSaveImagesToNfo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOnlyValueForCert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnlyValueForCert.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkImagesGlassOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkImagesGlassOverlay.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkUseMPAAFSK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseMPAAFSK.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOutlineForPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutlineForPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOutlinePlotEnglishOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutlinePlotEnglishOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutline.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkOutlineForPlot.Enabled = Me.chkOutline.Checked
        If Not Me.chkOutline.Checked Then Me.chkOutlineForPlot.Checked = False
    End Sub

    Private Sub chkOverwriteAllSPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwriteAllSPoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteEpFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwriteEpFanart.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteEpPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwriteEpPoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteEFanarts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieOverwriteEFanarts.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteEThumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieOverwriteEThumbs.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieOverwriteFanart.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteNfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwriteNfo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub
    'cocotus 20130303 Special DateAddvalue
    Private Sub chkSpecialDateAdd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSpecialDateAdd.CheckedChanged
        Me.SetApplyButton(True)
    End Sub
    'cocotus end
    Private Sub chkOverwritePoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieOverwritePoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteShowFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwriteShowFanart.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkOverwriteShowPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwriteShowPoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkPlotForOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlotForOutline.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtOutlineLimit.Enabled = Me.chkPlotForOutline.Checked
        If Not Me.chkPlotForOutline.Checked Then
            Me.txtOutlineLimit.Enabled = False
            'Me.txtOutlineLimit.Text = "0"
        End If
    End Sub

    Private Sub chkPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlot.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkPlotForOutline.Enabled = Me.chkPlot.Checked
        If Not Me.chkPlot.Checked Then
            Me.chkPlotForOutline.Checked = False
            Me.txtOutlineLimit.Enabled = False
        End If
    End Sub

    Private Sub chkProducers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProducers.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh = True
    End Sub

    Private Sub chkRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRelease.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkResizeAllSPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResizeAllSPoster.CheckedChanged
        Me.SetApplyButton(True)

        txtAllSPosterWidth.Enabled = chkResizeAllSPoster.Checked
        txtAllSPosterHeight.Enabled = chkResizeAllSPoster.Checked

        If Not chkResizeAllSPoster.Checked Then
            txtAllSPosterWidth.Text = String.Empty
            txtAllSPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizeEpFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResizeEpFanart.CheckedChanged
        Me.SetApplyButton(True)

        txtEpFanartWidth.Enabled = chkResizeEpFanart.Checked
        txtEpFanartHeight.Enabled = chkResizeEpFanart.Checked

        If Not chkResizeEpFanart.Checked Then
            txtEpFanartWidth.Text = String.Empty
            txtEpFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizeEpPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResizeEpPoster.CheckedChanged
        Me.SetApplyButton(True)

        txtEpPosterWidth.Enabled = chkResizeEpPoster.Checked
        txtEpPosterHeight.Enabled = chkResizeEpPoster.Checked

        If Not chkResizeEpFanart.Checked Then
            txtEpPosterWidth.Text = String.Empty
            txtEpPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizeEFanarts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieResizeEFanarts.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieEFanartsWidth.Enabled = chkMovieResizeEFanarts.Checked
        txtMovieEFanartsHeight.Enabled = chkMovieResizeEFanarts.Checked

        If Not chkMovieResizeEFanarts.Checked Then
            txtMovieEFanartsWidth.Text = String.Empty
            txtMovieEFanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizeEThumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieResizeEThumbs.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieEThumbsWidth.Enabled = chkMovieResizeEThumbs.Checked
        txtMovieEThumbsHeight.Enabled = chkMovieResizeEThumbs.Checked

        If Not chkMovieResizeEThumbs.Checked Then
            txtMovieEThumbsWidth.Text = String.Empty
            txtMovieEThumbsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizeFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieResizeFanart.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieFanartWidth.Enabled = chkMovieResizeFanart.Checked
        txtMovieFanartHeight.Enabled = chkMovieResizeFanart.Checked

        If Not chkMovieResizeFanart.Checked Then
            txtMovieFanartWidth.Text = String.Empty
            txtMovieFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizePoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieResizePoster.CheckedChanged
        Me.SetApplyButton(True)

        txtMoviePosterWidth.Enabled = chkMovieResizePoster.Checked
        txtMoviePosterHeight.Enabled = chkMovieResizePoster.Checked

        If Not chkMovieResizePoster.Checked Then
            txtMoviePosterWidth.Text = String.Empty
            txtMoviePosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizeShowFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResizeShowFanart.CheckedChanged
        Me.SetApplyButton(True)

        txtShowFanartWidth.Enabled = chkResizeShowFanart.Checked
        txtShowFanartHeight.Enabled = chkResizeShowFanart.Checked

        If Not chkResizeShowFanart.Checked Then
            txtShowFanartWidth.Text = String.Empty
            txtShowFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkResizeShowPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResizeShowPoster.CheckedChanged
        Me.SetApplyButton(True)

        txtShowPosterWidth.Enabled = chkResizeShowPoster.Checked
        txtShowPosterHeight.Enabled = chkResizeShowPoster.Checked

        If Not chkResizeShowPoster.Checked Then
            txtShowPosterWidth.Text = String.Empty
            txtShowPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRuntime.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScanMediaInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScanMediaInfo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScanOrderModify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScanOrderModify.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperActorThumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperActorThumbs.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpActors_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpActors.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpAired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpAired.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpCredits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpCredits.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpDirector.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpEpisode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpEpisode.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpSeason.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperEpTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowActors_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowActors.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowEGU_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowEGU.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowGenre.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowMPAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowPremiered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowPremiered.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkScraperShowTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkSeaOverwriteFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeaOverwriteFanart.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkSeaOverwritePoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeaOverwritePoster.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkSeaResizeFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeaResizeFanart.CheckedChanged
        Me.SetApplyButton(True)

        txtSeaFanartWidth.Enabled = chkSeaResizeFanart.Checked
        txtSeaFanartHeight.Enabled = chkSeaResizeFanart.Checked

        If Not chkSeaResizeFanart.Checked Then
            txtSeaFanartWidth.Text = String.Empty
            txtSeaFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkSeaResizePoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeaResizePoster.CheckedChanged
        Me.SetApplyButton(True)

        txtSeaPosterWidth.Enabled = chkSeaResizePoster.Checked
        txtSeaPosterHeight.Enabled = chkSeaResizePoster.Checked

        If Not chkSeaResizePoster.Checked Then
            txtSeaPosterWidth.Text = String.Empty
            txtSeaPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkSeasonFanartCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeasonFanartCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkSeasonPosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeasonPosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowDims_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowDims.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowFanartCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowFanartCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowGenresText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowGenresText.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowLockGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowLockGenre.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowLockPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowLockPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowLockRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowLockRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowLockStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowLockStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowLockTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowLockTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowNfoCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowNfoCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowPosterCol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowPosterCol.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkShowProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh = True
    End Sub

    Private Sub chkSingleScrapeImages_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSingleScrapeImages.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkSortBeforeScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSortBeforeScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkSourceFromFolder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSourceFromFolder.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkStudio_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTagline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTop250_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTop250.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCountry.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTrailer.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVCleanDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVCleanDB.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVIgnoreLastScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVIgnoreLastScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScanMetaData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScanMetaData.CheckedChanged
        Me.SetApplyButton(True)

        Me.cboTVMetaDataOverlay.Enabled = Me.chkTVScanMetaData.Checked

        If Not Me.chkTVScanMetaData.Checked Then
            Me.cboTVMetaDataOverlay.SelectedIndex = 0
        End If
    End Sub

    Private Sub chkTVScanOrderModify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScanOrderModify.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkUpdates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUpdates.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkUseCertForMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseCertForMPAA.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkOnlyValueForCert.Enabled = Me.chkUseCertForMPAA.Checked
        Me.chkUseMPAAFSK.Enabled = Me.chkUseCertForMPAA.Checked

        If Not Me.chkUseCertForMPAA.Checked Then Me.chkOnlyValueForCert.Checked = False
        If Not Me.chkUseCertForMPAA.Checked Then Me.chkUseMPAAFSK.Checked = False
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
    End Sub

    Private Sub chkMovieUseYAMJCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseYAMJ.CheckedChanged
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

    Private Sub chkMovieUseNMJCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseNMJ.CheckedChanged
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

    Private Sub chkUseMIDuration_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseMIDuration.CheckedChanged
        Me.txtRuntimeFormat.Enabled = Me.chkUseMIDuration.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkUseEPDuration_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseEPDuration.CheckedChanged
        Me.txtEPRuntimeFormat.Enabled = Me.chkUseEPDuration.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVotes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkWhitelistVideo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWhitelistVideo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkWriters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWriters.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieXBMCProtectVTSBDMV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCProtectVTSBDMV.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieXBMCTrailerFormat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCTrailerFormat.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkYear.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieYAMJWatchedFile.CheckedChanged
        Me.txtMovieYAMJWatchedFolder.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.btnMovieBrowseWatchedFiles.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chShowPosterSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowPosterSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub ClearRegex()
        Me.btnAddShowRegex.Text = Master.eLang.GetString(115, "Add Regex")
        Me.btnAddShowRegex.Tag = String.Empty
        Me.btnAddShowRegex.Enabled = False
        Me.txtSeasonRegex.Text = String.Empty
        Me.cboSeasonRetrieve.SelectedIndex = -1
        Me.txtEpRegex.Text = String.Empty
        Me.cboEpRetrieve.SelectedIndex = -1
    End Sub

    Private Sub dlgSettings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub EditShowRegex(ByVal lItem As ListViewItem)
        Me.btnAddShowRegex.Text = Master.eLang.GetString(124, "Update Regex")
        Me.btnAddShowRegex.Tag = lItem.Text

        Me.txtSeasonRegex.Text = lItem.SubItems(1).Text.ToString

        Select Case lItem.SubItems(2).Text
            Case "Folder"
                Me.cboSeasonRetrieve.SelectedIndex = 0
            Case "File"
                Me.cboSeasonRetrieve.SelectedIndex = 1
        End Select

        Me.txtEpRegex.Text = lItem.SubItems(3).Text

        Select Case lItem.SubItems(4).Text
            Case "Folder"
                Me.cboEpRetrieve.SelectedIndex = 0
            Case "File"
                Me.cboEpRetrieve.SelectedIndex = 1
            Case "Result"
                Me.cboEpRetrieve.SelectedIndex = 2
        End Select
    End Sub

    Private Sub FillGenres()
        If Not String.IsNullOrEmpty(Master.eSettings.GenreFilter) Then
            Dim genreArray() As String
            genreArray = Strings.Split(Master.eSettings.GenreFilter, ",")
            For g As Integer = 0 To UBound(genreArray)
                If Me.lbGenre.FindString(Strings.Trim(genreArray(g))) > 0 Then Me.lbGenre.SetItemChecked(Me.lbGenre.FindString(Strings.Trim(genreArray(g))), True)
            Next

            If Me.lbGenre.CheckedItems.Count = 0 Then
                Me.lbGenre.SetItemChecked(0, True)
            End If
        Else
            Me.lbGenre.SetItemChecked(0, True)
        End If
    End Sub

    Private Sub FillList(ByVal sType As String)
        Dim pNode As New TreeNode
        Dim cNode As New TreeNode

        Me.tvSettings.Nodes.Clear()
        Me.RemoveCurrPanel()

        For Each pPanel As Containers.SettingsPanel In SettingsPanels.Where(Function(s) s.Type = sType AndAlso String.IsNullOrEmpty(s.Parent)).OrderBy(Function(s) s.Order)
            pNode = New TreeNode(pPanel.Text, pPanel.ImageIndex, pPanel.ImageIndex)
            pNode.Name = pPanel.Name
            For Each cPanel As Containers.SettingsPanel In SettingsPanels.Where(Function(p) p.Type = sType AndAlso p.Parent = pNode.Name).OrderBy(Function(s) s.Order)
                cNode = New TreeNode(cPanel.Text, cPanel.ImageIndex, cPanel.ImageIndex)
                cNode.Name = cPanel.Name
                pNode.Nodes.Add(cNode)
            Next
            Me.tvSettings.Nodes.Add(pNode)
        Next

        If Me.tvSettings.Nodes.Count > 0 Then
            Me.tvSettings.ExpandAll()
            Me.tvSettings.SelectedNode = Me.tvSettings.Nodes(0)
        Else
            Me.pbCurrent.Image = Nothing
            Me.lblCurrent.Text = String.Empty
        End If
    End Sub

    Private Sub FillSettings()
        Try
            Me.chkProperCase.Checked = Master.eSettings.ProperCase
            Me.chkShowProperCase.Checked = Master.eSettings.ShowProperCase
            Me.chkEpProperCase.Checked = Master.eSettings.EpProperCase
            Me.chkCleanFolderJPG.Checked = Master.eSettings.CleanFolderJPG
            Me.chkCleanMovieTBN.Checked = Master.eSettings.CleanMovieTBN
            Me.chkCleanMovieTBNb.Checked = Master.eSettings.CleanMovieTBNB
            Me.chkCleanFanartJPG.Checked = Master.eSettings.CleanFanartJPG
            Me.chkCleanMovieFanartJPG.Checked = Master.eSettings.CleanMovieFanartJPG
            Me.chkCleanMovieNFO.Checked = Master.eSettings.CleanMovieNFO
            Me.chkCleanMovieNFOb.Checked = Master.eSettings.CleanMovieNFOB
            Me.chkCleanPosterTBN.Checked = Master.eSettings.CleanPosterTBN
            Me.chkCleanPosterJPG.Checked = Master.eSettings.CleanPosterJPG
            Me.chkCleanMovieJPG.Checked = Master.eSettings.CleanMovieJPG
            Me.chkCleanMovieNameJPG.Checked = Master.eSettings.CleanMovieNameJPG
            Me.chkCleanDotFanartJPG.Checked = Master.eSettings.CleanDotFanartJPG
            Me.chkCleanExtrathumbs.Checked = Master.eSettings.CleanExtraThumbs
            Me.tcCleaner.SelectedTab = If(Master.eSettings.ExpertCleaner, Me.tpExpert, Me.tpStandard)
            Me.chkWhitelistVideo.Checked = Master.eSettings.CleanWhitelistVideo
            Me.lstWhitelist.Items.AddRange(Master.eSettings.CleanWhitelistExts.ToArray)
            Me.chkOverwriteNfo.Checked = Master.eSettings.OverwriteNfo
            'cocotus 20130303 Special DateAddvalue
            Me.chkSpecialDateAdd.Checked = Master.eSettings.UseSpecialDateAddvalue
            'cocotus end
            'Me.chkYAMJCompatibleSets.Checked = Master.eSettings.YAMJSetsCompatible
            Me.lstNoStack.Items.AddRange(Master.eSettings.NoStackExts.ToArray)
            Me.chkUpdates.Checked = Master.eSettings.CheckUpdates
            Me.chkInfoPanelAnim.Checked = Master.eSettings.InfoPanelAnim

            If Not String.IsNullOrEmpty(Master.eSettings.CertificationLang) Then
                Me.chkCert.Checked = True
                Me.cbCert.Enabled = True
                Me.cbCert.Text = Master.eSettings.CertificationLang
                Me.chkUseCertForMPAA.Enabled = True
                Me.chkUseCertForMPAA.Checked = Master.eSettings.UseCertForMPAA
            End If
            If Not String.IsNullOrEmpty(Master.eSettings.ForceTitle) Then
                Me.chkForceTitle.Checked = True
                Me.cbForce.Enabled = True
                Me.chkTitleFallback.Enabled = True
                Me.cbForce.Text = Master.eSettings.ForceTitle
            End If
            Me.chkTitleFallback.Checked = Master.eSettings.UseTitleFallback
            Me.chkScanMediaInfo.Checked = Master.eSettings.ScanMediaInfo
            Me.chkTVScanMetaData.Checked = Master.eSettings.ScanTVMediaInfo
            Me.chkFullCast.Checked = Master.eSettings.FullCast
            Me.chkFullCrew.Checked = Master.eSettings.FullCrew
            Me.chkCastWithImg.Checked = Master.eSettings.CastImagesOnly
            Me.chkMoviePosterCol.Checked = Master.eSettings.MoviePosterCol
            Me.chkMovieFanartCol.Checked = Master.eSettings.MovieFanartCol
            Me.chkMovieInfoCol.Checked = Master.eSettings.MovieInfoCol
            Me.chkMovieTrailerCol.Checked = Master.eSettings.MovieTrailerCol
            Me.chkMovieSubCol.Checked = Master.eSettings.MovieSubCol
            Me.chkMovieEFanartsCol.Checked = Master.eSettings.MovieEFanartsCol
            Me.chkMovieEThumbsCol.Checked = Master.eSettings.MovieEThumbsCol
            Me.chkMovieWatchedCol.Checked = Master.eSettings.MovieWatchedCol

            Me.chkOverwriteTrailer.Checked = Master.eSettings.OverwriteTrailer
            Me.chkDeleteAllTrailers.Checked = Master.eSettings.DeleteAllTrailers

            Me.cbTrailerQuality.SelectedValue = Master.eSettings.PreferredTrailerQuality

            Me.cbMovieEFanartsSize.SelectedIndex = Master.eSettings.PreferredEFanartsSize
            Me.cbMovieEThumbsSize.SelectedIndex = Master.eSettings.PreferredEThumbsSize
            Me.cbMoviePosterSize.SelectedIndex = Master.eSettings.PreferredPosterSize
            Me.cbMovieFanartSize.SelectedIndex = Master.eSettings.PreferredFanartSize
            If Master.eSettings.IsShowBanner Then
                Me.rbBanner.Checked = True
                Me.cbShowPosterSize.SelectedIndex = Master.eSettings.PreferredShowBannerType
            Else
                Me.rbPoster.Checked = True
                Me.cbShowPosterSize.SelectedIndex = Master.eSettings.PreferredShowPosterSize
            End If
            If Master.eSettings.IsAllSBanner Then
                Me.rbAllSBanner.Checked = True
                Me.cbAllSPosterSize.SelectedIndex = Master.eSettings.PreferredAllSBannerType
            Else
                Me.rbAllSPoster.Checked = True
                Me.cbAllSPosterSize.SelectedIndex = Master.eSettings.PreferredAllSPosterSize
            End If
            Me.cbShowFanartSize.SelectedIndex = Master.eSettings.PreferredShowFanartSize
            Me.cbEpFanartSize.SelectedIndex = Master.eSettings.PreferredEpFanartSize
            Me.cbSeaPosterSize.SelectedIndex = Master.eSettings.PreferredSeasonPosterSize
            Me.cbSeaFanartSize.SelectedIndex = Master.eSettings.PreferredSeasonFanartSize
            Me.chkMovieEFanartsOnly.Checked = Master.eSettings.EFanartsPrefSizeOnly
            Me.chkMovieEThumbsOnly.Checked = Master.eSettings.EThumbsPrefSizeOnly
            Me.chkMovieFanartOnly.Checked = Master.eSettings.FanartPrefSizeOnly
            Me.chkMoviePosterOnly.Checked = Master.eSettings.PosterPrefSizeOnly
            Me.tbMovieEFanartsQual.Value = Master.eSettings.EFanartsQuality
            Me.tbMovieEThumbsQual.Value = Master.eSettings.EThumbsQuality
            Me.tbMoviePosterQual.Value = Master.eSettings.PosterQuality
            Me.tbMovieFanartQual.Value = Master.eSettings.FanartQuality
            Me.tbShowPosterQual.Value = Master.eSettings.ShowPosterQuality
            Me.tbShowFanartQual.Value = Master.eSettings.ShowFanartQuality
            Me.tbAllSPosterQual.Value = Master.eSettings.AllSPosterQuality
            Me.tbEpPosterQual.Value = Master.eSettings.EpPosterQuality
            Me.tbEpFanartQual.Value = Master.eSettings.EpFanartQuality
            Me.tbSeaPosterQual.Value = Master.eSettings.SeasonPosterQuality
            Me.tbSeaFanartQual.Value = Master.eSettings.SeasonFanartQuality
            Me.chkMovieOverwriteEFanarts.Checked = Master.eSettings.OverwriteEFanarts
            Me.chkMovieOverwriteEThumbs.Checked = Master.eSettings.OverwriteEThumbs
            Me.chkMovieOverwritePoster.Checked = Master.eSettings.OverwritePoster
            Me.chkMovieOverwriteFanart.Checked = Master.eSettings.OverwriteFanart
            Me.chkOverwriteShowPoster.Checked = Master.eSettings.OverwriteShowPoster
            Me.chkOverwriteAllSPoster.Checked = Master.eSettings.OverwriteAllSPoster
            Me.chkOverwriteShowFanart.Checked = Master.eSettings.OverwriteShowFanart
            Me.chkOverwriteEpPoster.Checked = Master.eSettings.OverwriteEpPoster
            Me.chkOverwriteEpFanart.Checked = Master.eSettings.OverwriteEpFanart
            Me.chkSeaOverwritePoster.Checked = Master.eSettings.OverwriteSeasonPoster
            Me.chkSeaOverwriteFanart.Checked = Master.eSettings.OverwriteSeasonFanart
            Me.chkLockPlot.Checked = Master.eSettings.LockPlot
            Me.chkLockOutline.Checked = Master.eSettings.LockOutline
            Me.chkLockTitle.Checked = Master.eSettings.LockTitle
            Me.chkLockTagline.Checked = Master.eSettings.LockTagline
            Me.chkLockRating.Checked = Master.eSettings.LockRating
            Me.chkLockRealStudio.Checked = Master.eSettings.LockStudio
            Me.chkLockLanguageA.Checked = Master.eSettings.LockLanguageA
            Me.chkLockLanguageV.Checked = Master.eSettings.LockLanguageV
            Me.chkLockMPAA.Checked = Master.eSettings.LockMPAA
            Me.chkUseMPAAFSK.Checked = Master.eSettings.UseMPAAForFSK
            Me.chkLockGenre.Checked = Master.eSettings.LockGenre
            Me.chkLockTrailer.Checked = Master.eSettings.LockTrailer
            Me.chkMovieSingleScrapeImages.Checked = Master.eSettings.SingleScrapeImages
            Me.chkClickScrape.Checked = Master.eSettings.ClickScrape
            Me.chkAskCheckboxScrape.Enabled = Me.chkClickScrape.Checked
            Me.chkAskCheckboxScrape.Checked = Master.eSettings.AskCheckboxScrape
            Me.chkMarkNew.Checked = Master.eSettings.MarkNew
            Me.chkMovieResizeEFanarts.Checked = Master.eSettings.ResizeEFanarts
            If Master.eSettings.ResizeEFanarts Then
                Me.txtMovieEFanartsWidth.Text = Master.eSettings.EFanartsWidth.ToString
                Me.txtMovieEFanartsHeight.Text = Master.eSettings.EFanartsHeight.ToString
            End If
            Me.chkMovieResizeEThumbs.Checked = Master.eSettings.ResizeEThumbs
            If Master.eSettings.ResizeEThumbs Then
                Me.txtMovieEThumbsWidth.Text = Master.eSettings.EThumbsWidth.ToString
                Me.txtMovieEThumbsHeight.Text = Master.eSettings.EThumbsHeight.ToString
            End If
            Me.chkMovieResizeFanart.Checked = Master.eSettings.ResizeFanart
            If Master.eSettings.ResizeFanart Then
                Me.txtMovieFanartWidth.Text = Master.eSettings.FanartWidth.ToString
                Me.txtMovieFanartHeight.Text = Master.eSettings.FanartHeight.ToString
            End If
            Me.chkMovieResizePoster.Checked = Master.eSettings.ResizePoster
            If Master.eSettings.ResizePoster Then
                Me.txtMoviePosterWidth.Text = Master.eSettings.PosterWidth.ToString
                Me.txtMoviePosterHeight.Text = Master.eSettings.PosterHeight.ToString
            End If
            Me.chkResizeShowFanart.Checked = Master.eSettings.ResizeShowFanart
            If Master.eSettings.ResizeShowFanart Then
                Me.txtShowFanartWidth.Text = Master.eSettings.ShowFanartWidth.ToString
                Me.txtShowFanartHeight.Text = Master.eSettings.ShowFanartHeight.ToString
            End If
            Me.chkResizeShowPoster.Checked = Master.eSettings.ResizeShowPoster
            If Master.eSettings.ResizeShowPoster Then
                Me.txtShowPosterWidth.Text = Master.eSettings.ShowPosterWidth.ToString
                Me.txtShowPosterHeight.Text = Master.eSettings.ShowPosterHeight.ToString
            End If
            Me.chkResizeAllSPoster.Checked = Master.eSettings.ResizeAllSPoster
            If Master.eSettings.ResizeAllSPoster Then
                Me.txtAllSPosterWidth.Text = Master.eSettings.AllSPosterWidth.ToString
                Me.txtAllSPosterHeight.Text = Master.eSettings.AllSPosterHeight.ToString
            End If
            Me.chkResizeEpFanart.Checked = Master.eSettings.ResizeEpFanart
            If Master.eSettings.ResizeEpFanart Then
                Me.txtEpFanartWidth.Text = Master.eSettings.EpFanartWidth.ToString
                Me.txtEpFanartHeight.Text = Master.eSettings.EpFanartHeight.ToString
            End If
            Me.chkResizeEpPoster.Checked = Master.eSettings.ResizeEpPoster
            If Master.eSettings.ResizeEpPoster Then
                Me.txtEpPosterWidth.Text = Master.eSettings.EpPosterWidth.ToString
                Me.txtEpPosterHeight.Text = Master.eSettings.EpPosterHeight.ToString
            End If
            Me.chkSeaResizeFanart.Checked = Master.eSettings.ResizeSeasonFanart
            If Master.eSettings.ResizeSeasonFanart Then
                Me.txtSeaFanartWidth.Text = Master.eSettings.SeasonFanartWidth.ToString
                Me.txtSeaFanartHeight.Text = Master.eSettings.SeasonFanartHeight.ToString
            End If
            Me.chkSeaResizePoster.Checked = Master.eSettings.ResizeSeasonPoster
            If Master.eSettings.ResizeSeasonPoster Then
                Me.txtSeaPosterWidth.Text = Master.eSettings.SeasonPosterWidth.ToString
                Me.txtSeaPosterHeight.Text = Master.eSettings.SeasonPosterHeight.ToString
            End If

            Me.txtBDPath.Text = Master.eSettings.BDPath
            Me.txtMoviesetsPath.Text = Master.eSettings.MoviesetsPath
            Me.txtBDPath.Enabled = Master.eSettings.AutoBD
            Me.btnBrowseBackdrops.Enabled = Master.eSettings.AutoBD
            Me.chkAutoBD.Checked = Master.eSettings.AutoBD
            Me.chkUseMIDuration.Checked = Master.eSettings.UseMIDuration
            Me.txtRuntimeFormat.Enabled = Master.eSettings.UseMIDuration
            Me.txtRuntimeFormat.Text = Master.eSettings.RuntimeMask
            Me.chkUseEPDuration.Checked = Master.eSettings.UseEPDuration
            Me.txtEPRuntimeFormat.Enabled = Master.eSettings.UseEPDuration
            Me.txtEPRuntimeFormat.Text = Master.eSettings.EPRuntimeMask
            Me.txtSkipLessThan.Text = Master.eSettings.SkipLessThan.ToString
            Me.chkSkipStackedSizeCheck.Checked = Master.eSettings.SkipStackSizeCheck
            Me.txtTVSkipLessThan.Text = Master.eSettings.SkipLessThanEp.ToString
            Me.chkMovieNoSaveImagesToNfo.Checked = Master.eSettings.NoSaveImagesToNfo

            Me.chkDownloadTrailer.Checked = Master.eSettings.UpdaterTrailers
            Me.chkOverwriteTrailer.Checked = Master.eSettings.OverwriteTrailer
            Me.chkDeleteAllTrailers.Checked = Master.eSettings.DeleteAllTrailers

            Me.cbTrailerQuality.SelectedValue = Master.eSettings.PreferredTrailerQuality

            FillGenres()

            Me.chkShowDims.Checked = Master.eSettings.ShowDims
            Me.chkNoDisplayFanart.Checked = Master.eSettings.NoDisplayFanart
            Me.chkNoDisplayPoster.Checked = Master.eSettings.NoDisplayPoster
            Me.chkNoDisplayFanartSmall.Checked = Master.eSettings.NoDisplayFanartSmall
            Me.chkOutlineForPlot.Checked = Master.eSettings.OutlineForPlot
            Me.chkOutlinePlotEnglishOverwrite.Checked = Master.eSettings.OutlinePlotEnglishOverwrite
            Me.chkPlotForOutline.Checked = Master.eSettings.PlotForOutline
            Me.chkShowGenresText.Checked = Master.eSettings.AllwaysDisplayGenresText
            Me.chkDisplayYear.Checked = Master.eSettings.DisplayYear

            Me.lstSortTokens.Items.AddRange(Master.eSettings.SortTokens.ToArray)

            If Master.eSettings.LevTolerance > 0 Then
                Me.chkCheckTitles.Checked = True
                Me.txtCheckTitleTol.Enabled = True
                Me.txtCheckTitleTol.Text = Master.eSettings.LevTolerance.ToString
            End If
            Me.chkMovieRecognizeVTSExpertVTS.Checked = Master.eSettings.MovieRecognizeVTSExpertVTS
            Me.cbLanguages.SelectedItem = If(String.IsNullOrEmpty(Master.eSettings.FlagLang), Master.eLang.Disabled, Master.eSettings.FlagLang)
            Me.cboTVMetaDataOverlay.SelectedItem = If(String.IsNullOrEmpty(Master.eSettings.TVFlagLang), Master.eLang.Disabled, Master.eSettings.TVFlagLang)
            Me.cbIntLang.SelectedItem = Master.eSettings.Language

            Me.chkTitle.Checked = Master.eSettings.FieldTitle
            Me.chkYear.Checked = Master.eSettings.FieldYear
            Me.chkMPAA.Checked = Master.eSettings.FieldMPAA
            Me.chkCertification.Checked = Master.eSettings.FieldCert
            Me.chkRelease.Checked = Master.eSettings.FieldRelease
            Me.chkRuntime.Checked = Master.eSettings.FieldRuntime
            Me.chkRating.Checked = Master.eSettings.FieldRating
            Me.chkVotes.Checked = Master.eSettings.FieldVotes
            Me.chkStudio.Checked = Master.eSettings.FieldStudio
            Me.chkGenre.Checked = Master.eSettings.FieldGenre
            Me.chkTrailer.Checked = Master.eSettings.FieldTrailer
            Me.chkTagline.Checked = Master.eSettings.FieldTagline
            Me.chkOutline.Checked = Master.eSettings.FieldOutline
            Me.chkPlot.Checked = Master.eSettings.FieldPlot
            Me.chkImagesGlassOverlay.Checked = Master.eSettings.ImagesGlassOverlay
            Me.chkCast.Checked = Master.eSettings.FieldCast
            Me.chkDirector.Checked = Master.eSettings.FieldDirector
            Me.chkWriters.Checked = Master.eSettings.FieldWriters
            If Master.eSettings.FullCrew Then
                Me.chkProducers.Checked = Master.eSettings.FieldProducers
                Me.chkMusicBy.Checked = Master.eSettings.FieldMusic
                Me.chkCrew.Checked = Master.eSettings.FieldCrew
            End If
            Me.chkTop250.Checked = Master.eSettings.Field250
            Me.chkCountry.Checked = Master.eSettings.FieldCountry
            Me.txtActorLimit.Text = Master.eSettings.ActorLimit.ToString
            Me.txtGenreLimit.Text = Master.eSettings.GenreLimit.ToString
            Me.txtOutlineLimit.Text = Master.eSettings.OutlineLimit.ToString

            Me.chkMissingPoster.Checked = Master.eSettings.MissingFilterPoster
            Me.chkMissingFanart.Checked = Master.eSettings.MissingFilterFanart
            Me.chkMissingNFO.Checked = Master.eSettings.MissingFilterNFO
            Me.chkMissingTrailer.Checked = Master.eSettings.MissingFilterTrailer
            Me.chkMissingSubs.Checked = Master.eSettings.MissingFilterSubs
            Me.chkMissingEThumbs.Checked = Master.eSettings.MissingFilterEThumbs
            Me.chkMissingEFanarts.Checked = Master.eSettings.MissingFilterEFanarts
            Me.cbMovieTheme.SelectedItem = Master.eSettings.MovieTheme
            Me.cbTVShowTheme.SelectedItem = Master.eSettings.TVShowTheme
            Me.cbo_DAEMON_driveletter.SelectedItem = Master.eSettings.DAEMON_driveletter
            Me.txt_DAEMON_Programpath.Text = Master.eSettings.DAEMON_Programpath
            Me.cbEpTheme.SelectedItem = Master.eSettings.TVEpTheme
            Me.Meta.AddRange(Master.eSettings.MetadataPerFileType)
            Me.LoadMetadata()
            Me.TVMeta.AddRange(Master.eSettings.TVMetadataperFileType)
            Me.LoadTVMetadata()
            Me.chkIFOScan.Checked = Master.eSettings.EnableIFOScan
            Me.chkCleanDB.Checked = Master.eSettings.CleanDB
            Me.chkIgnoreLastScan.Checked = Master.eSettings.IgnoreLastScan
            Me.chkTVCleanDB.Checked = Master.eSettings.TVCleanDB
            Me.chkTVIgnoreLastScan.Checked = Master.eSettings.TVIgnoreLastScan
            Me.ShowRegex.AddRange(Master.eSettings.TVShowRegexes)
            Me.LoadShowRegex()
            Me.cbRatingRegion.Text = Master.eSettings.ShowRatingRegion
            Me.chkShowPosterCol.Checked = Master.eSettings.ShowPosterCol
            Me.chkShowFanartCol.Checked = Master.eSettings.ShowFanartCol
            Me.chkShowNfoCol.Checked = Master.eSettings.ShowNfoCol
            Me.chkSeasonPosterCol.Checked = Master.eSettings.SeasonPosterCol
            Me.chkSeasonFanartCol.Checked = Master.eSettings.SeasonFanartCol
            Me.chkEpisodePosterCol.Checked = Master.eSettings.EpisodePosterCol
            Me.chkEpisodeFanartCol.Checked = Master.eSettings.EpisodeFanartCol
            Me.chkEpisodeNfoCol.Checked = Master.eSettings.EpisodeNfoCol
            Me.chkSourceFromFolder.Checked = Master.eSettings.SourceFromFolder
            Me.chkSortBeforeScan.Checked = Master.eSettings.SortBeforeScan
            'Me.tLangList.Clear()
            'Me.tLangList.AddRange(Master.eSettings.TVDBLanguages)

            If Not String.IsNullOrEmpty(Master.eSettings.ProxyURI) AndAlso Master.eSettings.ProxyPort >= 0 Then
                Me.chkEnableProxy.Checked = True
                Me.txtProxyURI.Text = Master.eSettings.ProxyURI
                Me.txtProxyPort.Text = Master.eSettings.ProxyPort.ToString

                If Not String.IsNullOrEmpty(Master.eSettings.ProxyCreds.UserName) Then
                    Me.chkEnableCredentials.Checked = True
                    Me.txtProxyUsername.Text = Master.eSettings.ProxyCreds.UserName
                    Me.txtProxyPassword.Text = Master.eSettings.ProxyCreds.Password
                    Me.txtProxyDomain.Text = Master.eSettings.ProxyCreds.Domain
                End If
            End If

            Me.chkScanOrderModify.Checked = Master.eSettings.ScanOrderModify
            Me.chkTVScanOrderModify.Checked = Master.eSettings.TVScanOrderModify
            Me.cboTVUpdate.SelectedIndex = Master.eSettings.TVUpdateTime
            Me.chkNoFilterEpisode.Checked = Master.eSettings.NoFilterEpisode
            Me.chkDisplayMissingEpisodes.Checked = Master.eSettings.DisplayMissingEpisodes
            Me.chkShowLockTitle.Checked = Master.eSettings.ShowLockTitle
            Me.chkShowLockPlot.Checked = Master.eSettings.ShowLockPlot
            Me.chkShowLockRating.Checked = Master.eSettings.ShowLockRating
            Me.chkShowLockGenre.Checked = Master.eSettings.ShowLockGenre
            Me.chkShowLockStudio.Checked = Master.eSettings.ShowLockStudio
            Me.chkEpLockTitle.Checked = Master.eSettings.EpLockTitle
            Me.chkEpLockPlot.Checked = Master.eSettings.EpLockPlot
            Me.chkEpLockRating.Checked = Master.eSettings.EpLockRating
            Me.chkScraperShowTitle.Checked = Master.eSettings.ScraperShowTitle
            Me.chkScraperShowEGU.Checked = Master.eSettings.ScraperShowEGU
            Me.chkScraperShowGenre.Checked = Master.eSettings.ScraperShowGenre
            Me.chkScraperShowMPAA.Checked = Master.eSettings.ScraperShowMPAA
            Me.chkScraperShowPlot.Checked = Master.eSettings.ScraperShowPlot
            Me.chkScraperShowPremiered.Checked = Master.eSettings.ScraperShowPremiered
            Me.chkScraperShowRating.Checked = Master.eSettings.ScraperShowRating
            Me.chkScraperShowStudio.Checked = Master.eSettings.ScraperShowStudio
            Me.chkScraperShowActors.Checked = Master.eSettings.ScraperShowActors
            Me.chkScraperEpTitle.Checked = Master.eSettings.ScraperEpTitle
            Me.chkScraperEpSeason.Checked = Master.eSettings.ScraperEpSeason
            Me.chkScraperEpEpisode.Checked = Master.eSettings.ScraperEpEpisode
            Me.chkScraperEpAired.Checked = Master.eSettings.ScraperEpAired
            Me.chkScraperEpRating.Checked = Master.eSettings.ScraperEpRating
            Me.chkScraperEpPlot.Checked = Master.eSettings.ScraperEpPlot
            Me.chkScraperEpDirector.Checked = Master.eSettings.ScraperEpDirector
            Me.chkScraperEpCredits.Checked = Master.eSettings.ScraperEpCredits
            Me.chkScraperEpActors.Checked = Master.eSettings.ScraperEpActors
            Me.chkDisplayAllSeason.Checked = Master.eSettings.DisplayAllSeason
            Me.chkMarkNewShows.Checked = Master.eSettings.MarkNewShows
            Me.chkMarkNewEpisodes.Checked = Master.eSettings.MarkNewEpisodes
            Me.cbOrdering.SelectedIndex = Master.eSettings.OrderDefault
            Me.chkOnlyValueForCert.Checked = Master.eSettings.OnlyValueForCert
            Me.chkMovieScraperActorThumbs.Checked = Master.eSettings.ScraperActorThumbs
            Me.txtIMDBURL.Text = Master.eSettings.IMDBURL
            Me.RefreshSources()
            Me.RefreshTVSources()
            Me.RefreshShowFilters()
            Me.RefreshEpFilters()
            Me.RefreshMovieFilters()
            Me.RefreshValidExts()

            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Me.chkMovieUseFrodo.Checked = Master.eSettings.MovieUseFrodo
            Me.chkMovieActorThumbsFrodo.Checked = Master.eSettings.MovieActorThumbsFrodo
            Me.chkMovieBannerFrodo.Checked = Master.eSettings.MovieBannerFrodo
            Me.chkMovieClearArtFrodo.Checked = Master.eSettings.MovieClearArtFrodo
            Me.chkMovieClearLogoFrodo.Checked = Master.eSettings.MovieClearLogoFrodo
            Me.chkMovieDiscArtFrodo.Checked = Master.eSettings.MovieDiscArtFrodo
            Me.chkMovieExtrafanartsFrodo.Checked = Master.eSettings.MovieExtrafanartsFrodo
            Me.chkMovieExtrathumbsFrodo.Checked = Master.eSettings.MovieExtrathumbsFrodo
            Me.chkMovieFanartFrodo.Checked = Master.eSettings.MovieFanartFrodo
            Me.chkMovieLandscapeFrodo.Checked = Master.eSettings.MovieLandscapeFrodo
            Me.chkMovieNFOFrodo.Checked = Master.eSettings.MovieNFOFrodo
            Me.chkMoviePosterFrodo.Checked = Master.eSettings.MoviePosterFrodo
            Me.chkMovieTrailerFrodo.Checked = Master.eSettings.MovieTrailerFrodo

            '*************** XBMC Eden settings ****************
            Me.chkMovieUseEden.Checked = Master.eSettings.MovieUseEden
            Me.chkMovieActorThumbsEden.Checked = Master.eSettings.MovieActorThumbsEden
            'Me.chkBannerEden.Checked = Master.eSettings.MovieBannerEden
            'Me.chkClearArtEden.Checked = Master.eSettings.MovieClearArtEden
            'Me.chkClearLogoEden.Checked = Master.eSettings.MovieClearLogoEden
            'Me.chkDiscArtEden.Checked = Master.eSettings.MovieDiscArtEden
            Me.chkMovieExtrafanartsEden.Checked = Master.eSettings.MovieExtrafanartsEden
            Me.chkMovieExtrathumbsEden.Checked = Master.eSettings.MovieExtrathumbsEden
            Me.chkMovieFanartEden.Checked = Master.eSettings.MovieFanartEden
            'Me.chkLandscapeEden.Checked = Master.eSettings.MovieLandscapeEden
            Me.chkMovieNFOEden.Checked = Master.eSettings.MovieNFOEden
            Me.chkMoviePosterEden.Checked = Master.eSettings.MoviePosterEden
            Me.chkMovieTrailerEden.Checked = Master.eSettings.MovieTrailerEden

            '************* XBMC optional settings **************
            Me.chkMovieXBMCTrailerFormat.Checked = Master.eSettings.MovieXBMCTrailerFormat
            Me.chkMovieXBMCProtectVTSBDMV.Checked = Master.eSettings.MovieXBMCProtectVTSBDMV

            '****************** YAMJ settings ******************
            Me.chkMovieUseYAMJ.Checked = Master.eSettings.MovieUseYAMJ
            'Me.chkActorThumbsYAMJ.Checked = Master.eSettings.MovieActorThumbsYAMJ
            Me.chkMovieBannerYAMJ.Checked = Master.eSettings.MovieBannerYAMJ
            'Me.chkClearArtYAMJ.Checked = Master.eSettings.MovieClearArtYAMJ
            'Me.chkClearLogoYAMJ.Checked = Master.eSettings.MovieClearLogoYAMJ
            'Me.chkDiscArtYAMJ.Checked = Master.eSettings.MovieDiscArtYAMJ
            'Me.chkExtrafanartYAMJ.Checked = Master.eSettings.MovieExtrafanartYAMJ
            'Me.chkExtrathumbsYAMJ.Checked = Master.eSettings.MovieExtrathumbsYAMJ
            Me.chkMovieFanartYAMJ.Checked = Master.eSettings.MovieFanartYAMJ
            'Me.chkLandscapeYAMJ.Checked = Master.eSettings.MovieLandscapeYAMJ
            Me.chkMovieNFOYAMJ.Checked = Master.eSettings.MovieNFOYAMJ
            Me.chkMoviePosterYAMJ.Checked = Master.eSettings.MoviePosterYAMJ
            Me.chkMovieTrailerYAMJ.Checked = Master.eSettings.MovieTrailerYAMJ

            '****************** NMJ settings ******************
            Me.chkMovieUseNMJ.Checked = Master.eSettings.MovieUseNMJ
            'Me.chkActorThumbsNMJ.Checked = Master.eSettings.MovieActorThumbsNMJ
            Me.chkMovieBannerNMJ.Checked = Master.eSettings.MovieBannerNMJ
            'Me.chkClearArtNMJ.Checked = Master.eSettings.MovieClearArtNMJ
            'Me.chkClearLogoNMJ.Checked = Master.eSettings.MovieClearLogoNMJ
            'Me.chkDiscArtNMJ.Checked = Master.eSettings.MovieDiscArtNMJ
            'Me.chkExtrafanartNMJ.Checked = Master.eSettings.MovieExtrafanartNMJ
            'Me.chkExtrathumbsNMJ.Checked = Master.eSettings.MovieExtrathumbsNMJ
            Me.chkMovieFanartNMJ.Checked = Master.eSettings.MovieFanartNMJ
            'Me.chkLandscapeNMJ.Checked = Master.eSettings.MovieLandscapeNMJ
            Me.chkMovieNFONMJ.Checked = Master.eSettings.MovieNFONMJ
            Me.chkMoviePosterNMJ.Checked = Master.eSettings.MoviePosterNMJ
            Me.chkMovieTrailerNMJ.Checked = Master.eSettings.MovieTrailerNMJ

            '************** NMT optional settings **************
            Me.chkMovieYAMJWatchedFile.Checked = Master.eSettings.MovieYAMJWatchedFile
            Me.txtMovieYAMJWatchedFolder.Text = Master.eSettings.MovieYAMJWatchedFolder

            '***************** Expert settings *****************
            Me.chkMovieUseExpert.Checked = Master.eSettings.MovieUseExpert

            '***************** Expert Single *******************
            Me.chkMovieActorThumbsExpertSingle.Checked = Master.eSettings.MovieActorThumbsExpertSingle
            Me.txtMovieActorThumbsExtExpertSingle.Text = Master.eSettings.MovieActorThumbsExtExpertSingle
            Me.txtMovieBannerExpertSingle.Text = Master.eSettings.MovieBannerExpertSingle
            Me.txtMovieClearArtExpertSingle.Text = Master.eSettings.MovieClearArtExpertSingle
            Me.txtMovieClearLogoExpertSingle.Text = Master.eSettings.MovieClearLogoExpertSingle
            Me.txtMovieDiscArtExpertSingle.Text = Master.eSettings.MovieDiscArtExpertSingle
            Me.chkMovieExtrafanartsExpertSingle.Checked = Master.eSettings.MovieExtrafanartsExpertSingle
            Me.chkMovieExtrathumbsExpertSingle.Checked = Master.eSettings.MovieExtrathumbsExpertSingle
            Me.txtMovieFanartExpertSingle.Text = Master.eSettings.MovieFanartExpertSingle
            Me.txtMovieLandscapeExpertSingle.Text = Master.eSettings.MovieLandscapeExpertSingle
            Me.txtMovieNFOExpertSingle.Text = Master.eSettings.MovieNFOExpertSingle
            Me.txtMoviePosterExpertSingle.Text = Master.eSettings.MoviePosterExpertSingle
            Me.chkMovieStackExpertSingle.Checked = Master.eSettings.MovieStackExpertSingle
            Me.txtMovieTrailerExpertSingle.Text = Master.eSettings.MovieTrailerExpertSingle
            Me.chkMovieUnstackExpertSingle.Checked = Master.eSettings.MovieUnstackExpertSingle

            '******************* Expert Multi ******************
            Me.chkMovieActorThumbsExpertMulti.Checked = Master.eSettings.MovieActorThumbsExpertMulti
            Me.txtMovieActorThumbsExtExpertMulti.Text = Master.eSettings.MovieActorThumbsExtExpertMulti
            Me.txtMovieBannerExpertMulti.Text = Master.eSettings.MovieBannerExpertMulti
            Me.txtMovieClearArtExpertMulti.Text = Master.eSettings.MovieClearArtExpertMulti
            Me.txtMovieClearLogoExpertMulti.Text = Master.eSettings.MovieClearLogoExpertMulti
            Me.txtMovieDiscArtExpertMulti.Text = Master.eSettings.MovieDiscArtExpertMulti
            Me.txtMovieFanartExpertMulti.Text = Master.eSettings.MovieFanartExpertMulti
            Me.txtMovieLandscapeExpertMulti.Text = Master.eSettings.MovieLandscapeExpertMulti
            Me.txtMovieNFOExpertMulti.Text = Master.eSettings.MovieNFOExpertMulti
            Me.txtMoviePosterExpertMulti.Text = Master.eSettings.MoviePosterExpertMulti
            Me.chkMovieStackExpertMulti.Checked = Master.eSettings.MovieStackExpertMulti
            Me.txtMovieTrailerExpertMulti.Text = Master.eSettings.MovieTrailerExpertMulti
            Me.chkMovieUnstackExpertMulti.Checked = Master.eSettings.MovieUnstackExpertMulti

            '******************* Expert VTS *******************
            Me.chkMovieActorThumbsExpertVTS.Checked = Master.eSettings.MovieActorThumbsExpertVTS
            Me.txtMovieActorThumbsExtExpertVTS.Text = Master.eSettings.MovieActorThumbsExtExpertVTS
            Me.txtMovieBannerExpertVTS.Text = Master.eSettings.MovieBannerExpertVTS
            Me.txtMovieClearArtExpertVTS.Text = Master.eSettings.MovieClearArtExpertVTS
            Me.txtMovieClearLogoExpertVTS.Text = Master.eSettings.MovieClearLogoExpertVTS
            Me.txtMovieDiscArtExpertVTS.Text = Master.eSettings.MovieDiscArtExpertVTS
            Me.chkMovieExtrafanartsExpertVTS.Checked = Master.eSettings.MovieExtrafanartsExpertVTS
            Me.chkMovieExtrathumbsExpertVTS.Checked = Master.eSettings.MovieExtrathumbsExpertVTS
            Me.txtMovieFanartExpertVTS.Text = Master.eSettings.MovieFanartExpertVTS
            Me.txtMovieLandscapeExpertVTS.Text = Master.eSettings.MovieLandscapeExpertVTS
            Me.txtMovieNFOExpertVTS.Text = Master.eSettings.MovieNFOExpertVTS
            Me.txtMoviePosterExpertVTS.Text = Master.eSettings.MoviePosterExpertVTS
            Me.chkMovieRecognizeVTSExpertVTS.Checked = Master.eSettings.MovieRecognizeVTSExpertVTS
            Me.txtMovieTrailerExpertVTS.Text = Master.eSettings.MovieTrailerExpertVTS
            Me.chkMovieUseBaseDirectoryExpertVTS.Checked = Master.eSettings.MovieUseBaseDirectoryExpertVTS

            '******************* Expert BDMV *******************
            Me.chkMovieActorThumbsExpertBDMV.Checked = Master.eSettings.MovieActorThumbsExpertBDMV
            Me.txtMovieActorThumbsExtExpertBDMV.Text = Master.eSettings.MovieActorThumbsExtExpertBDMV
            Me.txtMovieBannerExpertBDMV.Text = Master.eSettings.MovieBannerExpertBDMV
            Me.txtMovieClearArtExpertBDMV.Text = Master.eSettings.MovieClearArtExpertBDMV
            Me.txtMovieClearLogoExpertBDMV.Text = Master.eSettings.MovieClearLogoExpertBDMV
            Me.txtMovieDiscArtExpertBDMV.Text = Master.eSettings.MovieDiscArtExpertBDMV
            Me.chkMovieExtrafanartsExpertBDMV.Checked = Master.eSettings.MovieExtrafanartsExpertBDMV
            Me.chkMovieExtrathumbsExpertBDMV.Checked = Master.eSettings.MovieExtrathumbsExpertBDMV
            Me.txtMovieFanartExpertBDMV.Text = Master.eSettings.MovieFanartExpertBDMV
            Me.txtMovieLandscapeExpertBDMV.Text = Master.eSettings.MovieLandscapeExpertBDMV
            Me.txtMovieNFOExpertBDMV.Text = Master.eSettings.MovieNFOExpertBDMV
            Me.txtMoviePosterExpertBDMV.Text = Master.eSettings.MoviePosterExpertBDMV
            Me.txtMovieTrailerExpertBDMV.Text = Master.eSettings.MovieTrailerExpertBDMV
            Me.chkMovieUseBaseDirectoryExpertBDMV.Checked = Master.eSettings.MovieUseBaseDirectoryExpertBDMV


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Me.chkTVUseFrodo.Checked = Master.eSettings.TVUseFrodo
            Me.chkTVEpisodeActorThumbsFrodo.Checked = Master.eSettings.TVEpisodeActorThumbsFrodo
            Me.chkTVEpisodeFanartFrodo.Checked = Master.eSettings.TVEpisodeFanartFrodo
            Me.chkTVEpisodePosterFrodo.Checked = Master.eSettings.TVEpisodePosterFrodo
            Me.chkTVSeasonBannerFrodo.Checked = Master.eSettings.TVSeasonBannerFrodo
            Me.chkTVSeasonFanartFrodo.Checked = Master.eSettings.TVSeasonFanartFrodo
            Me.chkTVSeasonPosterFrodo.Checked = Master.eSettings.TVSeasonPosterFrodo
            Me.chkTVShowActorThumbsFrodo.Checked = Master.eSettings.TVShowActorThumbsFrodo
            Me.chkTVShowBannerFrodo.Checked = Master.eSettings.TVShowBannerFrodo
            Me.chkTVShowFanartFrodo.Checked = Master.eSettings.TVShowFanartFrodo
            Me.chkTVShowPosterFrodo.Checked = Master.eSettings.TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '************* XBMC optional settings **************
            Me.chkTVSeasonLandscapeXBMC.Checked = Master.eSettings.TVSeasonLandscapeXBMC
            Me.chkTVShowCharacterArtXBMC.Checked = Master.eSettings.TVShowCharacterArtXBMC
            Me.chkTVShowClearArtXBMC.Checked = Master.eSettings.TVShowClearArtXBMC
            Me.chkTVShowClearLogoXBMC.Checked = Master.eSettings.TVShowClearLogoXBMC
            Me.chkTVShowLandscapeXBMC.Checked = Master.eSettings.TVShowLandscapeXBMC
            Me.chkTVShowTVThemeXBMC.Checked = Master.eSettings.TVShowTVThemeXBMC
            Me.txtTVShowTVThemeFolderXBMC.Text = Master.eSettings.TVShowTVThemeFolderXBMC

            '****************** YAMJ settings ******************

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Expert settings *****************

        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Functions.PNLDoubleBuffer(Me.pnlMain)
            Me.SetUp()
            Me.AddPanels()
            Me.AddButtons()
            Me.AddHelpHandlers(Me, "Core_")

            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using

            iBackground = New Bitmap(Me.pnlCurrent.Width, Me.pnlCurrent.Height)
            Using b As Graphics = Graphics.FromImage(iBackground)
                b.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlCurrent.ClientRectangle, Color.SteelBlue, Color.FromKnownColor(KnownColor.Control), Drawing2D.LinearGradientMode.Horizontal), pnlCurrent.ClientRectangle)
                Me.pnlCurrent.BackgroundImage = iBackground
            End Using

            Me.LoadGenreLangs()
            Me.LoadIntLangs()
            Me.LoadLangs()
            Me.LoadThemes()
            Me.LoadRatingRegions()
            Me.FillSettings()
            Me.lvMovies.ListViewItemSorter = New ListViewItemComparer(1)
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
                Dim t() As TreeNode = tvSettings.Nodes.Find(Name, True)

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
                    Me.pbCurrent.Image = Me.ilSettings.Images(If(State, 9, 10))
                End If

                For Each s As ModulesManager._externalScraperModuleClass_Data In (ModulesManager.Instance.externalDataScrapersModules.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Poster In (ModulesManager.Instance.externalPosterScrapersModules.Where(Function(y) y.AssemblyName <> Name))
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

    Private Sub lbGenre_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lbGenre.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To lbGenre.Items.Count - 1
                Me.lbGenre.SetItemChecked(i, False)
            Next
        Else
            Me.lbGenre.SetItemChecked(0, False)
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub LoadGenreLangs()
        Me.lbGenre.Items.Add(Master.eLang.All)
        Me.lbGenre.Items.AddRange(APIXML.GetGenreList(True))
    End Sub

    Private Sub LoadIntLangs()
        Me.cbIntLang.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            Try
                alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            Catch
            End Try
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToList)
            Me.cbIntLang.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub LoadLangs()
        Me.cbLanguages.Items.Add(Master.eLang.Disabled)
        Me.cbLanguages.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
        Me.cboTVMetaDataOverlay.Items.Add(Master.eLang.Disabled)
        Me.cboTVMetaDataOverlay.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
    End Sub

    Private Sub LoadMetadata()
        Me.lstMetaData.Items.Clear()
        For Each x As Settings.MetadataPerType In Meta
            Me.lstMetaData.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub LoadRatingRegions()
        Me.cbRatingRegion.Items.AddRange(APIXML.GetRatingRegions)
    End Sub

    Private Sub LoadShowRegex()
        Dim lvItem As ListViewItem
        lvShowRegex.Items.Clear()
        For Each rShow As Settings.TVShowRegEx In Me.ShowRegex
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
            Me.lvShowRegex.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadThemes()
        Me.cbMovieTheme.Items.Clear()
        Me.cbTVShowTheme.Items.Clear()
        Me.cbEpTheme.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Themes")) Then
            Dim mT As New List(Of String)
            Dim sT As New List(Of String)
            Dim eT As New List(Of String)
            Try
                mT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "movie-*.xml"))
            Catch
            End Try
            Me.cbMovieTheme.Items.AddRange(mT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("movie-", String.Empty)).ToArray)
            Try
                sT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "tvshow-*.xml"))
            Catch
            End Try
            Me.cbTVShowTheme.Items.AddRange(sT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("tvshow-", String.Empty)).ToArray)
            Try
                eT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "tvep-*.xml"))
            Catch
            End Try
            Me.cbEpTheme.Items.AddRange(eT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("tvep-", String.Empty)).ToArray)
        End If
    End Sub

    Private Sub LoadTrailerQualities()
        Dim items As New Dictionary(Of String, Enums.TrailerQuality)
        items.Add("1080p", Enums.TrailerQuality.HD1080p)
        items.Add("1080p (VP8)", Enums.TrailerQuality.HD1080pVP8)
        items.Add("720p", Enums.TrailerQuality.HD720p)
        items.Add("720p (VP8)", Enums.TrailerQuality.HD720pVP8)
        items.Add("SQ (MP4)", Enums.TrailerQuality.SQMP4)
        items.Add("HQ (FLV)", Enums.TrailerQuality.HQFLV)
        items.Add("HQ (VP8)", Enums.TrailerQuality.HQVP8)
        items.Add("SQ (FLV)", Enums.TrailerQuality.SQFLV)
        items.Add("SQ (VP8)", Enums.TrailerQuality.SQVP8)
        Me.cbTrailerQuality.DataSource = items.ToList
        Me.cbTrailerQuality.DisplayMember = "Key"
        Me.cbTrailerQuality.ValueMember = "Value"
    End Sub

    Private Sub LoadTVMetadata()
        Me.lstTVMetaData.Items.Clear()
        For Each x As Settings.MetadataPerType In TVMeta
            Me.lstTVMetaData.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub lstEpFilters_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstEpFilters.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveEpFilter()
    End Sub

    Private Sub lstFilters_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstFilters.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveFilter()
    End Sub

    Private Sub lstMetaData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMetaData.DoubleClick
        If Me.lstMetaData.SelectedItems.Count > 0 Then
            Using dEditMeta As New dlgFileInfo
                Dim fi As New MediaInfo.Fileinfo
                For Each x As Settings.MetadataPerType In Meta
                    If x.FileType = lstMetaData.SelectedItems(0).ToString Then
                        fi = dEditMeta.ShowDialog(x.MetaData, False)
                        If Not fi Is Nothing Then
                            Meta.Remove(x)
                            Dim m As New Settings.MetadataPerType
                            m.FileType = x.FileType
                            m.MetaData = New MediaInfo.Fileinfo
                            m.MetaData = fi
                            Meta.Add(m)
                            LoadMetadata()
                            Me.SetApplyButton(True)
                        End If
                        Exit For
                    End If
                Next
            End Using
        End If
    End Sub

    Private Sub lstMetaData_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstMetaData.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMetaData()
    End Sub

    Private Sub lstMetadata_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMetaData.SelectedIndexChanged
        If lstMetaData.SelectedItems.Count > 0 Then
            btnEditMetaDataFT.Enabled = True
            btnRemoveMetaDataFT.Enabled = True
            txtDefFIExt.Text = String.Empty
        Else
            btnEditMetaDataFT.Enabled = False
            btnRemoveMetaDataFT.Enabled = False
        End If
    End Sub

    Private Sub lstMovieExts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstMovieExts.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieExt()
    End Sub

    Private Sub lstNoStack_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstNoStack.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveNoStack()
    End Sub

    Private Sub lstShowFilters_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstShowFilters.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveShowFilter()
    End Sub

    Private Sub lstSortTokens_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveSortToken()
    End Sub

    Private Sub lstTVMetaData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTVMetaData.DoubleClick
        If Me.lstTVMetaData.SelectedItems.Count > 0 Then
            Using dEditMeta As New dlgFileInfo
                Dim fi As New MediaInfo.Fileinfo
                For Each x As Settings.MetadataPerType In TVMeta
                    If x.FileType = lstTVMetaData.SelectedItems(0).ToString Then
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

    Private Sub lstTVMetaData_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstTVMetaData.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVMetaData()
    End Sub

    Private Sub lstTVMetadata_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTVMetaData.SelectedIndexChanged
        If lstTVMetaData.SelectedItems.Count > 0 Then
            btnEditTVMetaDataFT.Enabled = True
            btnRemoveTVMetaDataFT.Enabled = True
            txtTVDefFIExt.Text = String.Empty
        Else
            btnEditTVMetaDataFT.Enabled = False
            btnRemoveTVMetaDataFT.Enabled = False
        End If
    End Sub

    Private Sub lvMovies_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvMovies.ColumnClick
        Me.lvMovies.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvMovies_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMovies.DoubleClick
        If lvMovies.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgMovieSource
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovies.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshSources()
                    Me.sResult.NeedsUpdate = True
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub lvMovies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvMovies.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieSource()
    End Sub

    Private Sub lvShowRegex_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvShowRegex.DoubleClick
        If Me.lvShowRegex.SelectedItems.Count > 0 Then Me.EditShowRegex(lvShowRegex.SelectedItems(0))
    End Sub

    Private Sub lvShowRegex_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvShowRegex.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveRegex()
    End Sub

    Private Sub lvShowRegex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvShowRegex.SelectedIndexChanged
        If Not String.IsNullOrEmpty(Me.btnAddShowRegex.Tag.ToString) Then Me.ClearRegex()
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

    Private Sub rbAllSBanner_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAllSBanner.CheckedChanged, rbAllSPoster.CheckedChanged
        Me.SetApplyButton(True)

        Me.cbAllSPosterSize.Items.Clear()

        If Me.rbAllSBanner.Checked Then
            Me.cbAllSPosterSize.Items.AddRange(New String() {Master.eLang.GetString(745, "None"), Master.eLang.GetString(746, "Blank"), Master.eLang.GetString(747, "Graphical"), Master.eLang.GetString(748, "Text")})
        Else
            Me.cbAllSPosterSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small"), Master.eLang.GetString(558, "Wide")})
        End If
        Me.cbAllSPosterSize.SelectedIndex = 0
    End Sub

    Private Sub rbBanner_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbBanner.CheckedChanged, rbPoster.CheckedChanged
        Me.SetApplyButton(True)

        Me.cbShowPosterSize.Items.Clear()

        If Me.rbBanner.Checked Then
            Me.cbShowPosterSize.Items.AddRange(New String() {Master.eLang.GetString(745, "None"), Master.eLang.GetString(746, "Blank"), Master.eLang.GetString(747, "Graphical"), Master.eLang.GetString(748, "Text")})
        Else
            Me.cbShowPosterSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small"), Master.eLang.GetString(558, "Wide")})
        End If
        Me.cbShowPosterSize.SelectedIndex = 0
    End Sub

    Private Sub RefreshEpFilters()
        Me.lstEpFilters.Items.Clear()
        Me.lstEpFilters.Items.AddRange(Master.eSettings.EpFilterCustom.ToArray)
    End Sub

    Private Sub RefreshHelpStrings(ByVal Language As String)
        For Each sKey As String In dHelp.Keys
            dHelp.Item(sKey) = Master.eLang.GetHelpString(sKey)
        Next
    End Sub

    Private Sub RefreshMovieFilters()
        Me.lstFilters.Items.Clear()
        Me.lstFilters.Items.AddRange(Master.eSettings.FilterCustom.ToArray)
    End Sub

    Private Sub RefreshShowFilters()
        Me.lstShowFilters.Items.Clear()
        Me.lstShowFilters.Items.AddRange(Master.eSettings.ShowFilterCustom.ToArray)
    End Sub

    Private Sub RefreshSources()
        Dim lvItem As ListViewItem

        lvMovies.Items.Clear()
        Master.DB.LoadMovieSourcesFromDB()
        For Each s As Structures.MovieSource In Master.MovieSources
            lvItem = New ListViewItem(s.id)
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(If(s.Recursive, "Yes", "No"))
            lvItem.SubItems.Add(If(s.UseFolderName, "Yes", "No"))
            lvItem.SubItems.Add(If(s.IsSingle, "Yes", "No"))
            lvMovies.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        Master.DB.LoadTVSourcesFromDB()
        lvTVSources.Items.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
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

    Private Sub RefreshValidExts()
        Me.lstMovieExts.Items.Clear()
        Me.lstMovieExts.Items.AddRange(Master.eSettings.ValidExts.ToArray)
    End Sub

    Private Sub RemoveCurrPanel()
        If Me.pnlMain.Controls.Count > 0 Then
            Me.pnlMain.Controls.Remove(Me.currPanel)
        End If
    End Sub

    Private Sub RemoveEpFilter()
        If Me.lstEpFilters.Items.Count > 0 AndAlso Me.lstEpFilters.SelectedItems.Count > 0 Then
            While Me.lstEpFilters.SelectedItems.Count > 0
                Me.lstEpFilters.Items.Remove(Me.lstEpFilters.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsRefresh = True
        End If
    End Sub

    Private Sub RemoveFilter()
        If Me.lstFilters.Items.Count > 0 AndAlso Me.lstFilters.SelectedItems.Count > 0 Then
            While Me.lstFilters.SelectedItems.Count > 0
                Me.lstFilters.Items.Remove(Me.lstFilters.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsRefresh = True
        End If
    End Sub

    Private Sub RemoveMetaData()
        If Me.lstMetaData.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In Meta
                If x.FileType = lstMetaData.SelectedItems(0).ToString Then
                    Meta.Remove(x)
                    LoadMetadata()
                    Me.SetApplyButton(True)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub RemoveMovieExt()
        If lstMovieExts.Items.Count > 0 AndAlso lstMovieExts.SelectedItems.Count > 0 Then
            While Me.lstMovieExts.SelectedItems.Count > 0
                Me.lstMovieExts.Items.Remove(Me.lstMovieExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If
    End Sub

    Private Sub RemoveMovieSource()
        Try
            If Me.lvMovies.SelectedItems.Count > 0 Then
                If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                    Me.lvMovies.BeginUpdate()

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MediaDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
                            Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                            While Me.lvMovies.SelectedItems.Count > 0
                                parSource.Value = lvMovies.SelectedItems(0).SubItems(1).Text
                                SQLcommand.CommandText = "SELECT Id FROM movies WHERE source = (?);"
                                Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                                    While SQLReader.Read
                                        Master.DB.DeleteFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                    End While
                                End Using
                                SQLcommand.CommandText = String.Concat("DELETE FROM sources WHERE name = (?);")
                                SQLcommand.ExecuteNonQuery()
                                lvMovies.Items.Remove(lvMovies.SelectedItems(0))
                            End While
                        End Using
                        SQLtransaction.Commit()
                    End Using

                    Me.lvMovies.Sort()
                    Me.lvMovies.EndUpdate()
                    Me.lvMovies.Refresh()

                    Functions.GetListOfSources()

                    Me.SetApplyButton(True)
                    Me.sResult.NeedsUpdate = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RemoveNoStack()
        If lstNoStack.Items.Count > 0 AndAlso lstNoStack.SelectedItems.Count > 0 Then
            While Me.lstNoStack.SelectedItems.Count > 0
                Me.lstNoStack.Items.Remove(Me.lstNoStack.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If
    End Sub

    Private Sub RemoveRegex()
        Dim ID As Integer
        For Each lItem As ListViewItem In lvShowRegex.SelectedItems
            ID = Convert.ToInt32(lItem.Text)
            Dim selRex = From lRegex As Settings.TVShowRegEx In Me.ShowRegex Where lRegex.ID = ID
            If selRex.Count > 0 Then
                Me.ShowRegex.Remove(selRex(0))
                Me.SetApplyButton(True)
            End If
        Next
        Me.LoadShowRegex()
    End Sub

    Private Sub RemoveShowFilter()
        If Me.lstShowFilters.Items.Count > 0 AndAlso Me.lstShowFilters.SelectedItems.Count > 0 Then
            While Me.lstShowFilters.SelectedItems.Count > 0
                Me.lstShowFilters.Items.Remove(Me.lstShowFilters.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsRefresh = True
        End If
    End Sub

    Private Sub RemoveSortToken()
        If Me.lstSortTokens.Items.Count > 0 AndAlso Me.lstSortTokens.SelectedItems.Count > 0 Then
            While Me.lstSortTokens.SelectedItems.Count > 0
                Me.lstSortTokens.Items.Remove(Me.lstSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsRefresh = True
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveTVMetaData()
        If Me.lstTVMetaData.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In TVMeta
                If x.FileType = lstTVMetaData.SelectedItems(0).ToString Then
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

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MediaDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
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

    Private Sub RenumberRegex()
        For i As Integer = 0 To Me.ShowRegex.Count - 1
            Me.ShowRegex(i).ID = i
        Next
    End Sub

    Private Sub SaveSettings(ByVal isApply As Boolean)
        Try
            Master.eSettings.FilterCustom.Clear()
            Master.eSettings.FilterCustom.AddRange(Me.lstFilters.Items.OfType(Of String).ToList)
            If Master.eSettings.FilterCustom.Count <= 0 Then Master.eSettings.NoFilters = True
            Master.eSettings.ProperCase = Me.chkProperCase.Checked

            Master.eSettings.ShowFilterCustom.Clear()
            Master.eSettings.ShowFilterCustom.AddRange(Me.lstShowFilters.Items.OfType(Of String).ToList)
            If Master.eSettings.ShowFilterCustom.Count <= 0 Then Master.eSettings.NoShowFilters = True
            Master.eSettings.ShowProperCase = Me.chkShowProperCase.Checked

            Master.eSettings.EpFilterCustom.Clear()
            Master.eSettings.EpFilterCustom.AddRange(Me.lstEpFilters.Items.OfType(Of String).ToList)
            If Master.eSettings.EpFilterCustom.Count <= 0 Then Master.eSettings.NoEpFilters = True
            Master.eSettings.EpProperCase = Me.chkEpProperCase.Checked

            If Me.tcCleaner.SelectedTab.Name = "tpExpert" Then
                Master.eSettings.ExpertCleaner = True
                Master.eSettings.CleanFolderJPG = False
                Master.eSettings.CleanMovieTBN = False
                Master.eSettings.CleanMovieTBNB = False
                Master.eSettings.CleanFanartJPG = False
                Master.eSettings.CleanMovieFanartJPG = False
                Master.eSettings.CleanMovieNFO = False
                Master.eSettings.CleanMovieNFOB = False
                Master.eSettings.CleanPosterTBN = False
                Master.eSettings.CleanPosterJPG = False
                Master.eSettings.CleanMovieJPG = False
                Master.eSettings.CleanMovieNameJPG = False
                Master.eSettings.CleanDotFanartJPG = False
                Master.eSettings.CleanExtraThumbs = False
                Master.eSettings.CleanWhitelistVideo = Me.chkWhitelistVideo.Checked
                Master.eSettings.CleanWhitelistExts.Clear()
                Master.eSettings.CleanWhitelistExts.AddRange(Me.lstWhitelist.Items.OfType(Of String).ToList)
            Else
                Master.eSettings.ExpertCleaner = False
                Master.eSettings.CleanFolderJPG = Me.chkCleanFolderJPG.Checked
                Master.eSettings.CleanMovieTBN = Me.chkCleanMovieTBN.Checked
                Master.eSettings.CleanMovieTBNB = Me.chkCleanMovieTBNb.Checked
                Master.eSettings.CleanFanartJPG = Me.chkCleanFanartJPG.Checked
                Master.eSettings.CleanMovieFanartJPG = Me.chkCleanMovieFanartJPG.Checked
                Master.eSettings.CleanMovieNFO = Me.chkCleanMovieNFO.Checked
                Master.eSettings.CleanMovieNFOB = Me.chkCleanMovieNFOb.Checked
                Master.eSettings.CleanPosterTBN = Me.chkCleanPosterTBN.Checked
                Master.eSettings.CleanPosterJPG = Me.chkCleanPosterJPG.Checked
                Master.eSettings.CleanMovieJPG = Me.chkCleanMovieJPG.Checked
                Master.eSettings.CleanMovieNameJPG = Me.chkCleanMovieNameJPG.Checked
                Master.eSettings.CleanDotFanartJPG = Me.chkCleanDotFanartJPG.Checked
                Master.eSettings.CleanExtraThumbs = Me.chkCleanExtrathumbs.Checked
                Master.eSettings.CleanWhitelistVideo = False
                Master.eSettings.CleanWhitelistExts.Clear()
            End If

            Master.eSettings.OverwriteNfo = Me.chkOverwriteNfo.Checked
            'cocotus 20130303 Special DateAddvalue
            Master.eSettings.UseSpecialDateAddvalue = Me.chkSpecialDateAdd.Checked
            'cocotus end
            Master.eSettings.ValidExts.Clear()
            Master.eSettings.ValidExts.AddRange(lstMovieExts.Items.OfType(Of String).ToList)
            Master.eSettings.NoStackExts.Clear()
            Master.eSettings.NoStackExts.AddRange(lstNoStack.Items.OfType(Of String).ToList)
            Master.eSettings.CheckUpdates = chkUpdates.Checked
            Master.eSettings.InfoPanelAnim = chkInfoPanelAnim.Checked
            Master.eSettings.CertificationLang = Me.cbCert.Text
            If Not String.IsNullOrEmpty(Me.cbCert.Text) Then
                Master.eSettings.UseCertForMPAA = Me.chkUseCertForMPAA.Checked
            Else
                Master.eSettings.UseCertForMPAA = False
            End If

            Master.eSettings.ForceTitle = Me.cbForce.Text
            Master.eSettings.UseTitleFallback = Me.chkTitleFallback.Checked
            Master.eSettings.ScanMediaInfo = Me.chkScanMediaInfo.Checked
            Master.eSettings.ScanTVMediaInfo = Me.chkTVScanMetaData.Checked
            Master.eSettings.FullCast = Me.chkFullCast.Checked
            Master.eSettings.FullCrew = Me.chkFullCrew.Checked
            Master.eSettings.CastImagesOnly = Me.chkCastWithImg.Checked
            Master.eSettings.MoviePosterCol = Me.chkMoviePosterCol.Checked
            Master.eSettings.MovieFanartCol = Me.chkMovieFanartCol.Checked
            Master.eSettings.MovieInfoCol = Me.chkMovieInfoCol.Checked
            Master.eSettings.MovieTrailerCol = Me.chkMovieTrailerCol.Checked
            Master.eSettings.MovieSubCol = Me.chkMovieSubCol.Checked
            Master.eSettings.MovieEFanartsCol = Me.chkMovieEFanartsCol.Checked
            Master.eSettings.MovieEThumbsCol = Me.chkMovieEThumbsCol.Checked
            Master.eSettings.movieWatchedCol = Me.chkMovieWatchedCol.Checked

            Master.eSettings.PreferredEFanartsSize = DirectCast(Me.cbMovieEFanartsSize.SelectedIndex, Enums.FanartSize)
            Master.eSettings.PreferredEThumbsSize = DirectCast(Me.cbMovieEThumbsSize.SelectedIndex, Enums.FanartSize)
            Master.eSettings.PreferredPosterSize = DirectCast(Me.cbMoviePosterSize.SelectedIndex, Enums.PosterSize)
            Master.eSettings.PreferredFanartSize = DirectCast(Me.cbMovieFanartSize.SelectedIndex, Enums.FanartSize)
            If Me.rbBanner.Checked Then
                Master.eSettings.IsShowBanner = True
                Master.eSettings.PreferredShowBannerType = DirectCast(Me.cbShowPosterSize.SelectedIndex, Enums.ShowBannerType)
            Else
                Master.eSettings.IsShowBanner = False
                Master.eSettings.PreferredShowPosterSize = DirectCast(Me.cbShowPosterSize.SelectedIndex, Enums.PosterSize)
            End If
            If Me.rbAllSBanner.Checked Then
                Master.eSettings.IsAllSBanner = True
                Master.eSettings.PreferredAllSBannerType = DirectCast(Me.cbAllSPosterSize.SelectedIndex, Enums.ShowBannerType)
            Else
                Master.eSettings.IsAllSBanner = False
                Master.eSettings.PreferredAllSPosterSize = DirectCast(Me.cbAllSPosterSize.SelectedIndex, Enums.PosterSize)
            End If
            Master.eSettings.PreferredShowFanartSize = DirectCast(Me.cbShowFanartSize.SelectedIndex, Enums.FanartSize)
            Master.eSettings.PreferredEpFanartSize = DirectCast(Me.cbEpFanartSize.SelectedIndex, Enums.FanartSize)
            Master.eSettings.PreferredSeasonPosterSize = DirectCast(Me.cbSeaPosterSize.SelectedIndex, Enums.SeasonPosterType)
            Master.eSettings.PreferredEpFanartSize = DirectCast(Me.cbSeaFanartSize.SelectedIndex, Enums.FanartSize)
            Master.eSettings.EFanartsPrefSizeOnly = Me.chkMovieEFanartsOnly.Checked
            Master.eSettings.EThumbsPrefSizeOnly = Me.chkMovieEThumbsOnly.Checked
            Master.eSettings.FanartPrefSizeOnly = Me.chkMovieFanartOnly.Checked
            Master.eSettings.PosterPrefSizeOnly = Me.chkMoviePosterOnly.Checked
            Master.eSettings.PosterQuality = Me.tbMoviePosterQual.Value
            Master.eSettings.FanartQuality = Me.tbMovieFanartQual.Value
            Master.eSettings.EFanartsQuality = Me.tbMovieEFanartsQual.Value
            Master.eSettings.EThumbsQuality = Me.tbMovieEThumbsQual.Value
            Master.eSettings.OverwritePoster = Me.chkMovieOverwritePoster.Checked
            Master.eSettings.OverwriteFanart = Me.chkMovieOverwriteFanart.Checked
            Master.eSettings.OverwriteEFanarts = Me.chkMovieOverwriteEFanarts.Checked
            Master.eSettings.OverwriteEThumbs = Me.chkMovieOverwriteEThumbs.Checked
            Master.eSettings.ShowPosterQuality = Me.tbShowPosterQual.Value
            Master.eSettings.ShowFanartQuality = Me.tbShowFanartQual.Value
            Master.eSettings.OverwriteShowPoster = Me.chkOverwriteShowPoster.Checked
            Master.eSettings.OverwriteShowFanart = Me.chkOverwriteShowFanart.Checked
            Master.eSettings.AllSPosterQuality = Me.tbAllSPosterQual.Value
            Master.eSettings.OverwriteAllSPoster = Me.chkOverwriteAllSPoster.Checked
            Master.eSettings.EpPosterQuality = Me.tbEpPosterQual.Value
            Master.eSettings.EpFanartQuality = Me.tbEpFanartQual.Value
            Master.eSettings.OverwriteEpPoster = Me.chkOverwriteEpPoster.Checked
            Master.eSettings.OverwriteEpFanart = Me.chkOverwriteEpFanart.Checked
            Master.eSettings.SeasonPosterQuality = Me.tbSeaPosterQual.Value
            Master.eSettings.SeasonFanartQuality = Me.tbSeaFanartQual.Value
            Master.eSettings.OverwriteSeasonPoster = Me.chkSeaOverwritePoster.Checked
            Master.eSettings.OverwriteSeasonFanart = Me.chkSeaOverwriteFanart.Checked
            Master.eSettings.LockPlot = Me.chkLockPlot.Checked
            Master.eSettings.LockOutline = Me.chkLockOutline.Checked
            Master.eSettings.LockTitle = Me.chkLockTitle.Checked
            Master.eSettings.LockTagline = Me.chkLockTagline.Checked
            Master.eSettings.LockRating = Me.chkLockRating.Checked
            Master.eSettings.LockLanguageV = Me.chkLockLanguageV.Checked
            Master.eSettings.LockLanguageA = Me.chkLockLanguageA.Checked
            Master.eSettings.LockMPAA = Me.chkLockMPAA.Checked
            Master.eSettings.UseMPAAForFSK = Me.chkUseMPAAFSK.Checked
            Master.eSettings.LockStudio = Me.chkLockRealStudio.Checked
            Master.eSettings.LockGenre = Me.chkLockGenre.Checked
            Master.eSettings.LockTrailer = Me.chkLockTrailer.Checked
            Master.eSettings.SingleScrapeImages = Me.chkMovieSingleScrapeImages.Checked
            Master.eSettings.ClickScrape = Me.chkClickScrape.Checked
            Master.eSettings.AskCheckboxScrape = Me.chkAskCheckboxScrape.Checked
            Master.eSettings.MarkNew = Me.chkMarkNew.Checked
            Master.eSettings.ResizeEFanarts = Me.chkMovieResizeEFanarts.Checked
            Master.eSettings.EFanartsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsHeight.Text), Convert.ToInt32(Me.txtMovieEFanartsHeight.Text), 0)
            Master.eSettings.EFanartsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsWidth.Text), Convert.ToInt32(Me.txtMovieEFanartsWidth.Text), 0)
            Master.eSettings.ResizeEThumbs = Me.chkMovieResizeEThumbs.Checked
            Master.eSettings.EThumbsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsHeight.Text), Convert.ToInt32(Me.txtMovieEThumbsHeight.Text), 0)
            Master.eSettings.EThumbsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsWidth.Text), Convert.ToInt32(Me.txtMovieEThumbsWidth.Text), 0)
            Master.eSettings.ResizeFanart = Me.chkMovieResizeFanart.Checked
            Master.eSettings.FanartHeight = If(Not String.IsNullOrEmpty(Me.txtMovieFanartHeight.Text), Convert.ToInt32(Me.txtMovieFanartHeight.Text), 0)
            Master.eSettings.FanartWidth = If(Not String.IsNullOrEmpty(Me.txtMovieFanartWidth.Text), Convert.ToInt32(Me.txtMovieFanartWidth.Text), 0)
            Master.eSettings.ResizePoster = Me.chkMovieResizePoster.Checked
            Master.eSettings.PosterHeight = If(Not String.IsNullOrEmpty(Me.txtMoviePosterHeight.Text), Convert.ToInt32(Me.txtMoviePosterHeight.Text), 0)
            Master.eSettings.PosterWidth = If(Not String.IsNullOrEmpty(Me.txtMoviePosterWidth.Text), Convert.ToInt32(Me.txtMoviePosterWidth.Text), 0)
            Master.eSettings.ResizeShowFanart = Me.chkResizeShowFanart.Checked
            Master.eSettings.ShowFanartHeight = If(Not String.IsNullOrEmpty(Me.txtShowFanartHeight.Text), Convert.ToInt32(Me.txtShowFanartHeight.Text), 0)
            Master.eSettings.ShowFanartWidth = If(Not String.IsNullOrEmpty(Me.txtShowFanartWidth.Text), Convert.ToInt32(Me.txtShowFanartWidth.Text), 0)
            Master.eSettings.ResizeShowPoster = Me.chkResizeShowPoster.Checked
            Master.eSettings.ShowPosterHeight = If(Not String.IsNullOrEmpty(Me.txtShowPosterHeight.Text), Convert.ToInt32(Me.txtShowPosterHeight.Text), 0)
            Master.eSettings.ShowPosterWidth = If(Not String.IsNullOrEmpty(Me.txtShowPosterWidth.Text), Convert.ToInt32(Me.txtShowPosterWidth.Text), 0)
            Master.eSettings.ResizeAllSPoster = Me.chkResizeAllSPoster.Checked
            Master.eSettings.AllSPosterHeight = If(Not String.IsNullOrEmpty(Me.txtAllSPosterHeight.Text), Convert.ToInt32(Me.txtAllSPosterHeight.Text), 0)
            Master.eSettings.AllSPosterWidth = If(Not String.IsNullOrEmpty(Me.txtAllSPosterWidth.Text), Convert.ToInt32(Me.txtAllSPosterWidth.Text), 0)
            Master.eSettings.ResizeEpFanart = Me.chkResizeEpFanart.Checked
            Master.eSettings.EpFanartHeight = If(Not String.IsNullOrEmpty(Me.txtEpFanartHeight.Text), Convert.ToInt32(Me.txtEpFanartHeight.Text), 0)
            Master.eSettings.EpFanartWidth = If(Not String.IsNullOrEmpty(Me.txtEpFanartWidth.Text), Convert.ToInt32(Me.txtEpFanartWidth.Text), 0)
            Master.eSettings.ResizeEpPoster = Me.chkResizeEpPoster.Checked
            Master.eSettings.EpPosterHeight = If(Not String.IsNullOrEmpty(Me.txtEpPosterHeight.Text), Convert.ToInt32(Me.txtEpPosterHeight.Text), 0)
            Master.eSettings.EpPosterWidth = If(Not String.IsNullOrEmpty(Me.txtEpPosterWidth.Text), Convert.ToInt32(Me.txtEpPosterWidth.Text), 0)
            Master.eSettings.ResizeSeasonFanart = Me.chkSeaResizeFanart.Checked
            Master.eSettings.SeasonFanartHeight = If(Not String.IsNullOrEmpty(Me.txtSeaFanartHeight.Text), Convert.ToInt32(Me.txtSeaFanartHeight.Text), 0)
            Master.eSettings.SeasonFanartWidth = If(Not String.IsNullOrEmpty(Me.txtSeaFanartWidth.Text), Convert.ToInt32(Me.txtSeaFanartWidth.Text), 0)
            Master.eSettings.ResizeSeasonPoster = Me.chkSeaResizePoster.Checked
            Master.eSettings.SeasonPosterHeight = If(Not String.IsNullOrEmpty(Me.txtSeaPosterHeight.Text), Convert.ToInt32(Me.txtSeaPosterHeight.Text), 0)
            Master.eSettings.SeasonPosterWidth = If(Not String.IsNullOrEmpty(Me.txtSeaPosterWidth.Text), Convert.ToInt32(Me.txtSeaPosterWidth.Text), 0)
            If Not String.IsNullOrEmpty(Me.txtIMDBURL.Text) Then
                Master.eSettings.IMDBURL = Strings.Replace(Me.txtIMDBURL.Text, "http://", String.Empty)
            Else
                Master.eSettings.IMDBURL = "akas.imdb.com"
            End If

            Master.eSettings.BDPath = Me.txtBDPath.Text
            If Not String.IsNullOrEmpty(Me.txtBDPath.Text) Then
                Master.eSettings.AutoBD = Me.chkAutoBD.Checked
            Else
                Master.eSettings.AutoBD = False
            End If
            Master.eSettings.MoviesetsPath = Me.txtMoviesetsPath.Text
            Master.eSettings.UseMIDuration = Me.chkUseMIDuration.Checked
            Master.eSettings.RuntimeMask = Me.txtRuntimeFormat.Text
            Master.eSettings.UseEPDuration = Me.chkUseEPDuration.Checked
            Master.eSettings.EPRuntimeMask = Me.txtEPRuntimeFormat.Text
            Master.eSettings.SkipLessThan = Convert.ToInt32(Me.txtSkipLessThan.Text)
            Master.eSettings.SkipStackSizeCheck = Me.chkSkipStackedSizeCheck.Checked
            Master.eSettings.SkipLessThanEp = Convert.ToInt32(Me.txtTVSkipLessThan.Text)
            Master.eSettings.NoSaveImagesToNfo = Me.chkMovieNoSaveImagesToNfo.Checked

            If Me.cbTrailerQuality.SelectedValue IsNot Nothing Then
                Master.eSettings.PreferredTrailerQuality = DirectCast(Me.cbTrailerQuality.SelectedValue, Enums.TrailerQuality)
            End If

            Master.eSettings.UpdaterTrailers = Me.chkDownloadTrailer.Checked
            Master.eSettings.OverwriteTrailer = Me.chkOverwriteTrailer.Checked
            Master.eSettings.DeleteAllTrailers = Me.chkDeleteAllTrailers.Checked

            If Me.lbGenre.CheckedItems.Count > 0 Then
                If Me.lbGenre.CheckedItems.Contains(String.Format("{0}", Master.eLang.GetString(569, Master.eLang.All))) Then
                    Master.eSettings.GenreFilter = String.Format("{0}", Master.eLang.GetString(569, Master.eLang.All))
                Else
                    Dim strGenre As String = String.Empty
                    Dim iChecked = From iCheck In Me.lbGenre.CheckedItems
                    strGenre = Strings.Join(iChecked.ToArray, ",")
                    Master.eSettings.GenreFilter = strGenre.Trim
                End If
            End If

            Master.eSettings.ShowDims = Me.chkShowDims.Checked
            Master.eSettings.NoDisplayFanart = Me.chkNoDisplayFanart.Checked
            Master.eSettings.NoDisplayPoster = Me.chkNoDisplayPoster.Checked
            Master.eSettings.NoDisplayFanartSmall = Me.chkNoDisplayFanartSmall.Checked
            Master.eSettings.ImagesGlassOverlay = Me.chkImagesGlassOverlay.Checked
            Master.eSettings.OutlineForPlot = Me.chkOutlineForPlot.Checked

            Master.eSettings.OutlinePlotEnglishOverwrite = Me.chkOutlinePlotEnglishOverwrite.Checked

            Master.eSettings.AllwaysDisplayGenresText = Me.chkShowGenresText.Checked
            Master.eSettings.DisplayYear = Me.chkDisplayYear.Checked

            Master.eSettings.SortTokens.Clear()
            Master.eSettings.SortTokens.AddRange(lstSortTokens.Items.OfType(Of String).ToList)
            If Master.eSettings.SortTokens.Count <= 0 Then Master.eSettings.NoTokens = True

            Master.eSettings.LevTolerance = If(Not String.IsNullOrEmpty(Me.txtCheckTitleTol.Text), Convert.ToInt32(Me.txtCheckTitleTol.Text), 0)
            Master.eSettings.MovieRecognizeVTSExpertVTS = Me.chkMovieRecognizeVTSExpertVTS.Checked
            Master.eSettings.FlagLang = If(Me.cbLanguages.Text = Master.eLang.Disabled, String.Empty, Me.cbLanguages.Text)
            Master.eSettings.TVFlagLang = If(Me.cboTVMetaDataOverlay.Text = Master.eLang.Disabled, String.Empty, Me.cboTVMetaDataOverlay.Text)
            Master.eSettings.Language = Me.cbIntLang.Text
            Me.lbGenre.Items.Clear()
            LoadGenreLangs()
            FillGenres()
            Master.eSettings.FieldTitle = Me.chkTitle.Checked
            Master.eSettings.FieldYear = Me.chkYear.Checked
            Master.eSettings.FieldMPAA = Me.chkMPAA.Checked
            Master.eSettings.FieldCert = Me.chkCertification.Checked
            Master.eSettings.FieldRelease = Me.chkRelease.Checked
            Master.eSettings.FieldRuntime = Me.chkRuntime.Checked
            Master.eSettings.FieldRating = Me.chkRating.Checked
            Master.eSettings.FieldVotes = Me.chkVotes.Checked
            Master.eSettings.FieldStudio = Me.chkStudio.Checked
            Master.eSettings.FieldGenre = Me.chkGenre.Checked
            Master.eSettings.FieldTrailer = Me.chkTrailer.Checked
            Master.eSettings.FieldTagline = Me.chkTagline.Checked
            Master.eSettings.FieldOutline = Me.chkOutline.Checked
            Master.eSettings.PlotForOutline = Me.chkPlotForOutline.Checked
            Master.eSettings.FieldPlot = Me.chkPlot.Checked
            Master.eSettings.FieldCast = Me.chkCast.Checked
            Master.eSettings.FieldDirector = Me.chkDirector.Checked
            Master.eSettings.FieldWriters = Me.chkWriters.Checked
            Master.eSettings.FieldProducers = Me.chkProducers.Checked
            Master.eSettings.FieldMusic = Me.chkMusicBy.Checked
            Master.eSettings.FieldCrew = Me.chkCrew.Checked
            Master.eSettings.Field250 = Me.chkTop250.Checked
            Master.eSettings.FieldCountry = Me.chkCountry.Checked

            If Not String.IsNullOrEmpty(Me.txtActorLimit.Text) Then
                Master.eSettings.ActorLimit = Convert.ToInt32(Me.txtActorLimit.Text)
            Else
                Master.eSettings.ActorLimit = 0
            End If
            If Not String.IsNullOrEmpty(Me.txtGenreLimit.Text) Then
                Master.eSettings.GenreLimit = Convert.ToInt32(Me.txtGenreLimit.Text)
            Else
                Master.eSettings.GenreLimit = 0
            End If
            If Not String.IsNullOrEmpty(Me.txtOutlineLimit.Text) Then
                Master.eSettings.OutlineLimit = Convert.ToInt32(Me.txtOutlineLimit.Text)
            Else
                Master.eSettings.OutlineLimit = 0
            End If

            Master.eSettings.MissingFilterPoster = Me.chkMissingPoster.Checked
            Master.eSettings.MissingFilterFanart = Me.chkMissingFanart.Checked
            Master.eSettings.MissingFilterNFO = Me.chkMissingNFO.Checked
            Master.eSettings.MissingFilterTrailer = Me.chkMissingTrailer.Checked
            Master.eSettings.MissingFilterSubs = Me.chkMissingSubs.Checked
            Master.eSettings.MissingFilterEThumbs = Me.chkMissingEThumbs.Checked
            Master.eSettings.MissingFilterEFanarts = Me.chkMissingEFanarts.Checked
            Master.eSettings.DAEMON_driveletter = Me.cbo_DAEMON_driveletter.Text
            Master.eSettings.DAEMON_Programpath = Me.txt_DAEMON_Programpath.Text
            Master.eSettings.MovieTheme = Me.cbMovieTheme.Text
            Master.eSettings.TVShowTheme = Me.cbTVShowTheme.Text
            Master.eSettings.TVEpTheme = Me.cbEpTheme.Text
            Master.eSettings.MetadataPerFileType.Clear()
            Master.eSettings.MetadataPerFileType.AddRange(Me.Meta)
            Master.eSettings.TVMetadataperFileType.Clear()
            Master.eSettings.TVMetadataperFileType.AddRange(Me.TVMeta)
            Master.eSettings.EnableIFOScan = Me.chkIFOScan.Checked
            Master.eSettings.CleanDB = Me.chkCleanDB.Checked
            Master.eSettings.IgnoreLastScan = Me.chkIgnoreLastScan.Checked
            Master.eSettings.TVCleanDB = Me.chkTVCleanDB.Checked
            Master.eSettings.TVIgnoreLastScan = Me.chkTVIgnoreLastScan.Checked
            Master.eSettings.TVShowRegexes.Clear()
            Master.eSettings.TVShowRegexes.AddRange(Me.ShowRegex)
            If String.IsNullOrEmpty(Me.cbRatingRegion.Text) Then
                Master.eSettings.ShowRatingRegion = "usa"
            Else
                Master.eSettings.ShowRatingRegion = Me.cbRatingRegion.Text
            End If

            Master.eSettings.ShowPosterCol = Me.chkShowPosterCol.Checked
            Master.eSettings.ShowFanartCol = Me.chkShowFanartCol.Checked
            Master.eSettings.ShowNfoCol = Me.chkShowNfoCol.Checked
            Master.eSettings.SeasonPosterCol = Me.chkSeasonPosterCol.Checked
            Master.eSettings.SeasonFanartCol = Me.chkSeasonFanartCol.Checked
            Master.eSettings.EpisodePosterCol = Me.chkEpisodePosterCol.Checked
            Master.eSettings.EpisodeFanartCol = Me.chkEpisodeFanartCol.Checked
            Master.eSettings.EpisodeNfoCol = Me.chkEpisodeNfoCol.Checked
            Master.eSettings.SourceFromFolder = Me.chkSourceFromFolder.Checked
            Master.eSettings.SortBeforeScan = Me.chkSortBeforeScan.Checked

            If Not String.IsNullOrEmpty(Me.txtProxyURI.Text) AndAlso Not String.IsNullOrEmpty(Me.txtProxyPort.Text) Then
                Master.eSettings.ProxyURI = Me.txtProxyURI.Text
                Master.eSettings.ProxyPort = Convert.ToInt32(Me.txtProxyPort.Text)

                If Not String.IsNullOrEmpty(Me.txtProxyUsername.Text) AndAlso Not String.IsNullOrEmpty(Me.txtProxyPassword.Text) Then
                    Master.eSettings.ProxyCreds.UserName = Me.txtProxyUsername.Text
                    Master.eSettings.ProxyCreds.Password = Me.txtProxyPassword.Text
                    Master.eSettings.ProxyCreds.Domain = Me.txtProxyDomain.Text
                Else
                    Master.eSettings.ProxyCreds = New NetworkCredential
                End If
            Else
                Master.eSettings.ProxyURI = String.Empty
                Master.eSettings.ProxyPort = -1
            End If

            Master.eSettings.ScanOrderModify = Me.chkScanOrderModify.Checked
            Master.eSettings.TVScanOrderModify = Me.chkTVScanOrderModify.Checked
            Master.eSettings.TVUpdateTime = DirectCast(Me.cboTVUpdate.SelectedIndex, Enums.TVUpdateTime)
            Master.eSettings.NoFilterEpisode = Me.chkNoFilterEpisode.Checked
            Master.eSettings.DisplayMissingEpisodes = Me.chkDisplayMissingEpisodes.Checked
            Master.eSettings.ShowLockTitle = Me.chkShowLockTitle.Checked
            Master.eSettings.ShowLockPlot = Me.chkShowLockPlot.Checked
            Master.eSettings.ShowLockRating = Me.chkShowLockRating.Checked
            Master.eSettings.ShowLockGenre = Me.chkShowLockGenre.Checked
            Master.eSettings.ShowLockStudio = Me.chkShowLockStudio.Checked
            Master.eSettings.EpLockTitle = Me.chkEpLockTitle.Checked
            Master.eSettings.EpLockPlot = Me.chkEpLockPlot.Checked
            Master.eSettings.EpLockRating = Me.chkEpLockRating.Checked
            Master.eSettings.ScraperShowTitle = Me.chkScraperShowTitle.Checked
            Master.eSettings.ScraperShowEGU = Me.chkScraperShowEGU.Checked
            Master.eSettings.ScraperShowGenre = Me.chkScraperShowGenre.Checked
            Master.eSettings.ScraperShowMPAA = Me.chkScraperShowMPAA.Checked
            Master.eSettings.ScraperShowPlot = Me.chkScraperShowPlot.Checked
            Master.eSettings.ScraperShowPremiered = Me.chkScraperShowPremiered.Checked
            Master.eSettings.ScraperShowRating = Me.chkScraperShowRating.Checked
            Master.eSettings.ScraperShowStudio = Me.chkScraperShowStudio.Checked
            Master.eSettings.ScraperShowActors = Me.chkScraperShowActors.Checked
            Master.eSettings.ScraperEpTitle = Me.chkScraperEpTitle.Checked
            Master.eSettings.ScraperEpSeason = Me.chkScraperEpSeason.Checked
            Master.eSettings.ScraperEpEpisode = Me.chkScraperEpEpisode.Checked
            Master.eSettings.ScraperEpAired = Me.chkScraperEpAired.Checked
            Master.eSettings.ScraperEpRating = Me.chkScraperEpRating.Checked
            Master.eSettings.ScraperEpPlot = Me.chkScraperEpPlot.Checked
            Master.eSettings.ScraperEpDirector = Me.chkScraperEpDirector.Checked
            Master.eSettings.ScraperEpCredits = Me.chkScraperEpCredits.Checked
            Master.eSettings.ScraperEpActors = Me.chkScraperEpActors.Checked
            Master.eSettings.DisplayAllSeason = Me.chkDisplayAllSeason.Checked
            Master.eSettings.MarkNewShows = Me.chkMarkNewShows.Checked
            Master.eSettings.MarkNewEpisodes = Me.chkMarkNewEpisodes.Checked
            Master.eSettings.OrderDefault = DirectCast(Me.cbOrdering.SelectedIndex, Enums.Ordering)
            Master.eSettings.OnlyValueForCert = Me.chkOnlyValueForCert.Checked
            Master.eSettings.ScraperActorThumbs = Me.chkMovieScraperActorThumbs.Checked

            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Master.eSettings.MovieUseFrodo = Me.chkMovieUseFrodo.Checked
            Master.eSettings.MovieActorThumbsFrodo = Me.chkMovieActorThumbsFrodo.Checked
            Master.eSettings.MovieBannerFrodo = Me.chkMovieBannerFrodo.Checked
            Master.eSettings.MovieClearArtFrodo = Me.chkMovieClearArtFrodo.Checked
            Master.eSettings.MovieClearLogoFrodo = Me.chkMovieClearLogoFrodo.Checked
            Master.eSettings.MovieDiscArtFrodo = Me.chkMovieDiscArtFrodo.Checked
            Master.eSettings.MovieExtrafanartsFrodo = Me.chkMovieExtrafanartsFrodo.Checked
            Master.eSettings.MovieExtrathumbsFrodo = Me.chkMovieExtrathumbsFrodo.Checked
            Master.eSettings.MovieFanartFrodo = Me.chkMovieFanartFrodo.Checked
            Master.eSettings.MovieLandscapeFrodo = Me.chkMovieLandscapeFrodo.Checked
            Master.eSettings.MovieNFOFrodo = Me.chkMovieNFOFrodo.Checked
            Master.eSettings.MoviePosterFrodo = Me.chkMoviePosterFrodo.Checked
            Master.eSettings.MovieTrailerFrodo = Me.chkMovieTrailerFrodo.Checked

            '*************** XBMC Eden settings ***************
            Master.eSettings.MovieUseEden = Me.chkMovieUseEden.Checked
            Master.eSettings.MovieActorThumbsEden = Me.chkMovieActorThumbsEden.Checked
            'Master.eSettings.MovieBannerEden = Me.chkBannerEden.Checked
            'Master.eSettings.MovieClearArtEden = Me.chkClearArtEden.Checked
            'Master.eSettings.MovieClearLogoEden = Me.chkClearLogoEden.Checked
            'Master.eSettings.MovieDiscArtEden = Me.chkDiscArtEden.Checked
            Master.eSettings.MovieExtrafanartsEden = Me.chkMovieExtrafanartsEden.Checked
            Master.eSettings.MovieExtrathumbsEden = Me.chkMovieExtrathumbsEden.Checked
            Master.eSettings.MovieFanartEden = Me.chkMovieFanartEden.Checked
            'Master.eSettings.MovieLandscapeEden = Me.chkLandscapeEden.Checked
            Master.eSettings.MovieNFOEden = Me.chkMovieNFOEden.Checked
            Master.eSettings.MoviePosterEden = Me.chkMoviePosterEden.Checked
            Master.eSettings.MovieTrailerEden = Me.chkMovieTrailerEden.Checked

            '************* XBMC optional settings *************
            Master.eSettings.MovieXBMCTrailerFormat = Me.chkMovieXBMCTrailerFormat.Checked
            Master.eSettings.MovieXBMCProtectVTSBDMV = Me.chkMovieXBMCProtectVTSBDMV.Checked

            '****************** YAMJ settings *****************
            Master.eSettings.MovieUseYAMJ = Me.chkMovieUseYAMJ.Checked
            'Master.eSettings.MovieActorThumbsYAMJ = Me.chkActorThumbsYAMJ.Checked
            Master.eSettings.MovieBannerYAMJ = Me.chkMovieBannerYAMJ.Checked
            'Master.eSettings.MovieClearArtYAMJ = Me.chkClearArtYAMJ.Checked
            'Master.eSettings.MovieClearLogoYAMJ = Me.chkClearLogoYAMJ.Checked
            'Master.eSettings.MovieDiscArtYAMJ = Me.chkDiscArtYAMJ.Checked
            'Master.eSettings.MovieExtrafanartYAMJ = Me.chkExtrafanartYAMJ.Checked
            'Master.eSettings.MovieExtrathumbsYAMJ = Me.chkExtrathumbsYAMJ.Checked
            Master.eSettings.MovieFanartYAMJ = Me.chkMovieFanartYAMJ.Checked
            'Master.eSettings.MovieLandscapeYAMJ = Me.chkLandscapeYAMJ.Checked
            Master.eSettings.MovieNFOYAMJ = Me.chkMovieNFOYAMJ.Checked
            Master.eSettings.MoviePosterYAMJ = Me.chkMoviePosterYAMJ.Checked
            Master.eSettings.MovieTrailerYAMJ = Me.chkMovieTrailerYAMJ.Checked

            '****************** NMJ settings *****************
            Master.eSettings.MovieUseNMJ = Me.chkMovieUseNMJ.Checked
            'Master.eSettings.MovieActorThumbsNMJ = Me.chkActorThumbsNMJ.Checked
            Master.eSettings.MovieBannerNMJ = Me.chkMovieBannerNMJ.Checked
            'Master.eSettings.MovieClearArtNMJ = Me.chkClearArtNMJ.Checked
            'Master.eSettings.MovieClearLogoNMJ = Me.chkClearLogoNMJ.Checked
            'Master.eSettings.MovieDiscArtNMJ = Me.chkDiscArtNMJ.Checked
            'Master.eSettings.MovieExtrafanartNMJ = Me.chkExtrafanartNMJ.Checked
            'Master.eSettings.MovieExtrathumbsNMJ = Me.chkExtrathumbsNMJ.Checked
            Master.eSettings.MovieFanartNMJ = Me.chkMovieFanartNMJ.Checked
            'Master.eSettings.MovieLandscapeNMJ = Me.chkLandscapeNMJ.Checked
            Master.eSettings.MovieNFONMJ = Me.chkMovieNFONMJ.Checked
            Master.eSettings.MoviePosterNMJ = Me.chkMoviePosterNMJ.Checked
            Master.eSettings.MovieTrailerNMJ = Me.chkMovieTrailerNMJ.Checked

            '************** NMJ optional settings *************
            Master.eSettings.MovieYAMJWatchedFile = Me.chkMovieYAMJWatchedFile.Checked
            Master.eSettings.MovieYAMJWatchedFolder = Me.txtMovieYAMJWatchedFolder.Text

            '***************** Expert settings ****************
            Master.eSettings.MovieUseExpert = Me.chkMovieUseExpert.Checked

            '***************** Expert Single ******************
            Master.eSettings.MovieActorThumbsExpertSingle = Me.chkMovieActorThumbsExpertSingle.Checked
            Master.eSettings.MovieActorThumbsExtExpertSingle = Me.txtMovieActorThumbsExtExpertSingle.Text
            Master.eSettings.MovieBannerExpertSingle = Me.txtMovieBannerExpertSingle.Text
            Master.eSettings.MovieClearArtExpertSingle = Me.txtMovieClearArtExpertSingle.Text
            Master.eSettings.MovieClearLogoExpertSingle = Me.txtMovieClearLogoExpertSingle.Text
            Master.eSettings.MovieDiscArtExpertSingle = Me.txtMovieDiscArtExpertSingle.Text
            Master.eSettings.MovieExtrafanartsExpertSingle = Me.chkMovieExtrafanartsExpertSingle.Checked
            Master.eSettings.MovieExtrathumbsExpertSingle = Me.chkMovieExtrathumbsExpertSingle.Checked
            Master.eSettings.MovieFanartExpertSingle = Me.txtMovieFanartExpertSingle.Text
            Master.eSettings.MovieLandscapeExpertSingle = Me.txtMovieLandscapeExpertSingle.Text
            Master.eSettings.MovieNFOExpertSingle = Me.txtMovieNFOExpertSingle.Text
            Master.eSettings.MoviePosterExpertSingle = Me.txtMoviePosterExpertSingle.Text
            Master.eSettings.MovieStackExpertSingle = Me.chkMovieStackExpertSingle.Checked
            Master.eSettings.MovieTrailerExpertSingle = Me.txtMovieTrailerExpertSingle.Text
            Master.eSettings.MovieUnstackExpertSingle = Me.chkMovieUnstackExpertSingle.Checked

            '***************** Expert Multi ******************
            Master.eSettings.MovieActorThumbsExpertMulti = Me.chkMovieActorThumbsExpertMulti.Checked
            Master.eSettings.MovieActorThumbsExtExpertMulti = Me.txtMovieActorThumbsExtExpertMulti.Text
            Master.eSettings.MovieBannerExpertMulti = Me.txtMovieBannerExpertMulti.Text
            Master.eSettings.MovieClearArtExpertMulti = Me.txtMovieClearArtExpertMulti.Text
            Master.eSettings.MovieClearLogoExpertMulti = Me.txtMovieClearLogoExpertMulti.Text
            Master.eSettings.MovieDiscArtExpertMulti = Me.txtMovieDiscArtExpertMulti.Text
            Master.eSettings.MovieFanartExpertMulti = Me.txtMovieFanartExpertMulti.Text
            Master.eSettings.MovieLandscapeExpertMulti = Me.txtMovieLandscapeExpertMulti.Text
            Master.eSettings.MovieNFOExpertMulti = Me.txtMovieNFOExpertMulti.Text
            Master.eSettings.MoviePosterExpertMulti = Me.txtMoviePosterExpertMulti.Text
            Master.eSettings.MovieStackExpertMulti = Me.chkMovieStackExpertMulti.Checked
            Master.eSettings.MovieTrailerExpertMulti = Me.txtMovieTrailerExpertMulti.Text
            Master.eSettings.MovieUnstackExpertMulti = Me.chkMovieUnstackExpertMulti.Checked

            '***************** Expert VTS ******************
            Master.eSettings.MovieActorThumbsExpertVTS = Me.chkMovieActorThumbsExpertVTS.Checked
            Master.eSettings.MovieActorThumbsExtExpertVTS = Me.txtMovieActorThumbsExtExpertVTS.Text
            Master.eSettings.MovieBannerExpertVTS = Me.txtMovieBannerExpertVTS.Text
            Master.eSettings.MovieClearArtExpertVTS = Me.txtMovieClearArtExpertVTS.Text
            Master.eSettings.MovieClearLogoExpertVTS = Me.txtMovieClearLogoExpertVTS.Text
            Master.eSettings.MovieDiscArtExpertVTS = Me.txtMovieDiscArtExpertVTS.Text
            Master.eSettings.MovieExtrafanartsExpertVTS = Me.chkMovieExtrafanartsExpertVTS.Checked
            Master.eSettings.MovieExtrathumbsExpertVTS = Me.chkMovieExtrathumbsExpertVTS.Checked
            Master.eSettings.MovieFanartExpertVTS = Me.txtMovieFanartExpertVTS.Text
            Master.eSettings.MovieLandscapeExpertVTS = Me.txtMovieLandscapeExpertVTS.Text
            Master.eSettings.MovieNFOExpertVTS = Me.txtMovieNFOExpertVTS.Text
            Master.eSettings.MoviePosterExpertVTS = Me.txtMoviePosterExpertVTS.Text
            Master.eSettings.MovieRecognizeVTSExpertVTS = Me.chkMovieRecognizeVTSExpertVTS.Checked
            Master.eSettings.MovieTrailerExpertVTS = Me.txtMovieTrailerExpertVTS.Text
            Master.eSettings.MovieUseBaseDirectoryExpertVTS = Me.chkMovieUseBaseDirectoryExpertVTS.Checked

            '***************** Expert BDMV ******************
            Master.eSettings.MovieActorThumbsExpertBDMV = Me.chkMovieActorThumbsExpertBDMV.Checked
            Master.eSettings.MovieActorThumbsExtExpertBDMV = Me.txtMovieActorThumbsExtExpertBDMV.Text
            Master.eSettings.MovieBannerExpertBDMV = Me.txtMovieBannerExpertBDMV.Text
            Master.eSettings.MovieClearArtExpertBDMV = Me.txtMovieClearArtExpertBDMV.Text
            Master.eSettings.MovieClearLogoExpertBDMV = Me.txtMovieClearLogoExpertBDMV.Text
            Master.eSettings.MovieDiscArtExpertBDMV = Me.txtMovieDiscArtExpertBDMV.Text
            Master.eSettings.MovieExtrafanartsExpertBDMV = Me.chkMovieExtrafanartsExpertBDMV.Checked
            Master.eSettings.MovieExtrathumbsExpertBDMV = Me.chkMovieExtrathumbsExpertBDMV.Checked
            Master.eSettings.MovieFanartExpertBDMV = Me.txtMovieFanartExpertBDMV.Text
            Master.eSettings.MovieLandscapeExpertBDMV = Me.txtMovieLandscapeExpertBDMV.Text
            Master.eSettings.MovieNFOExpertBDMV = Me.txtMovieNFOExpertBDMV.Text
            Master.eSettings.MoviePosterExpertBDMV = Me.txtMoviePosterExpertBDMV.Text
            Master.eSettings.MovieTrailerExpertBDMV = Me.txtMovieTrailerExpertBDMV.Text
            Master.eSettings.MovieUseBaseDirectoryExpertBDMV = Me.chkMovieUseBaseDirectoryExpertBDMV.Checked

            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Master.eSettings.TVUseFrodo = Me.chkTVUseFrodo.Checked
            Master.eSettings.TVEpisodeActorThumbsFrodo = Me.chkTVEpisodeActorThumbsFrodo.Checked
            Master.eSettings.TVEpisodeFanartFrodo = Me.chkTVEpisodeFanartFrodo.Checked
            Master.eSettings.TVEpisodePosterFrodo = Me.chkTVEpisodePosterFrodo.Checked
            Master.eSettings.TVSeasonBannerFrodo = Me.chkTVSeasonBannerFrodo.Checked
            Master.eSettings.TVSeasonFanartFrodo = Me.chkTVSeasonFanartFrodo.Checked
            Master.eSettings.TVSeasonPosterFrodo = Me.chkTVSeasonPosterFrodo.Checked
            Master.eSettings.TVShowActorThumbsFrodo = Me.chkTVShowActorThumbsFrodo.Checked
            Master.eSettings.TVShowBannerFrodo = Me.chkTVShowBannerFrodo.Checked
            Master.eSettings.TVShowFanartFrodo = Me.chkTVShowFanartFrodo.Checked
            Master.eSettings.TVShowPosterFrodo = Me.chkTVShowPosterFrodo.Checked

            '*************** XBMC Eden settings ****************

            '************* XBMC optional settings **************
            Master.eSettings.TVSeasonLandscapeXBMC = Me.chkTVSeasonLandscapeXBMC.Checked
            Master.eSettings.TVShowCharacterArtXBMC = Me.chkTVShowCharacterArtXBMC.Checked
            Master.eSettings.TVShowClearArtXBMC = Me.chkTVShowClearArtXBMC.Checked
            Master.eSettings.TVShowClearLogoXBMC = Me.chkTVShowClearLogoXBMC.Checked
            Master.eSettings.TVShowLandscapeXBMC = Me.chkTVShowLandscapeXBMC.Checked
            Master.eSettings.TVShowTVThemeXBMC = Me.chkTVShowTVThemeXBMC.Checked
            Master.eSettings.TVShowTVThemeFolderXBMC = Me.txtTVShowTVThemeFolderXBMC.Text

            '****************** YAMJ settings ******************

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Expert settings *****************


            'Default to Frodo
            If Not (Master.eSettings.MovieUseEden Or Master.eSettings.MovieUseExpert Or Master.eSettings.MovieUseFrodo Or Master.eSettings.MovieUseNMJ Or Master.eSettings.MovieUseYAMJ) Then
                Master.eSettings.MovieUseFrodo = True
                Master.eSettings.MovieActorThumbsFrodo = True
                Master.eSettings.MovieBannerFrodo = True
                Master.eSettings.MovieClearArtFrodo = True
                Master.eSettings.MovieClearLogoFrodo = True
                Master.eSettings.MovieDiscArtFrodo = True
                Master.eSettings.MovieExtrafanartsFrodo = True
                Master.eSettings.MovieExtrathumbsFrodo = True
                Master.eSettings.MovieFanartFrodo = True
                Master.eSettings.MovieLandscapeFrodo = True
                Master.eSettings.MovieNFOFrodo = True
                Master.eSettings.MoviePosterFrodo = True
                Master.eSettings.MovieTrailerFrodo = True
            End If

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

    Private Sub chkOverwriteTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwriteTrailer.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkDeleteAllTrailers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDeleteAllTrailers.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub SetUp()
        Me.Label18.Text = Master.eLang.GetString(884, "IMDB Mirror:")
        Me.cbForce.Items.AddRange(Strings.Split(AdvancedSettings.GetSetting("ForceTitle", ""), "|"))
        Me.btnAddShowRegex.Tag = String.Empty
        Me.Text = Master.eLang.GetString(420, "Settings")
        Me.GroupBox4.Text = Master.eLang.GetString(429, "Miscellaneous")
        Me.Label32.Text = Master.eLang.GetString(430, "Interface Language:")
        Me.chkInfoPanelAnim.Text = Master.eLang.GetString(431, "Enable Panel Animation")
        Me.chkUpdates.Text = Master.eLang.GetString(432, "Check for Updates")
        Me.chkOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        'cocotus 20130303
        Me.chkSpecialDateAdd.Text = Master.eLang.GetString(874, "Use FileCreated information of videofile")
        'cocotus end
        Me.Label5.Text = Master.eLang.GetString(434, "(If unchecked, non-conforming nfos will be renamed to <filename>.info)")

        Me.Label31.Text = Master.eLang.GetString(436, "Display Overlay if Video Contains an Audio Stream With the Following Language:")
        Me.Label50.Text = Me.Label31.Text
        Me.GroupBox3.Text = Master.eLang.GetString(437, "Clean Files")
        Me.tpStandard.Text = Master.eLang.GetString(438, "Standard")
        Me.tpExpert.Text = Master.eLang.GetString(439, "Expert")
        Me.chkWhitelistVideo.Text = Master.eLang.GetString(440, "Whitelist Video Extensions")
        Me.Label27.Text = Master.eLang.GetString(441, "Whitelisted Extensions:")
        Me.Label25.Text = Master.eLang.GetString(442, "WARNING: Using the Expert Mode Cleaner could potentially delete wanted files. Take care when using this tool.")
        Me.gbFilters.Text = Master.eLang.GetString(451, "Folder/File Name Filters")
        Me.chkProperCase.Text = Master.eLang.GetString(452, "Convert Names to Proper Case")
        Me.chkShowProperCase.Text = Me.chkProperCase.Text
        Me.chkEpProperCase.Text = Me.chkProperCase.Text
        Me.GroupBox12.Text = Me.GroupBox4.Text
        Me.chkShowGenresText.Text = Master.eLang.GetString(453, "Always Display Genre Text")
        Me.gbGenreFilter.Text = Master.eLang.GetString(454, "Genre Language Filter")
        Me.chkNoDisplayFanart.Text = Master.eLang.GetString(455, "Do Not Display Fanart")
        Me.chkNoDisplayPoster.Text = Master.eLang.GetString(456, "Do Not Display Poster")
        Me.chkNoDisplayFanartSmall.Text = Master.eLang.GetString(967, "Do Not Display Small Fanart")
        Me.chkImagesGlassOverlay.Text = Master.eLang.GetString(966, "Enable Images Glass Overlay")
        Me.chkShowDims.Text = Master.eLang.GetString(457, "Display Image Dimensions")
        Me.chkMarkNew.Text = Master.eLang.GetString(459, "Mark New Movies")
        Me.GroupBox2.Text = Master.eLang.GetString(460, "Media List Options")
        Me.Label30.Text = Master.eLang.GetString(461, "Mismatch Tolerance:")
        Me.chkCheckTitles.Text = Master.eLang.GetString(462, "Check Title Match Confidence")
        Me.GroupBox25.Text = Master.eLang.GetString(463, "Sort Tokens to Ignore")
        Me.chkDisplayYear.Text = Master.eLang.GetString(464, "Display Year in List Title")
        Me.chkMovieEFanartsCol.Text = Master.eLang.GetString(983, "Hide Extrafanart Column")
        Me.chkMovieEThumbsCol.Text = Master.eLang.GetString(465, "Hide Extrathumb Column")
        Me.chkMovieSubCol.Text = Master.eLang.GetString(466, "Hide Sub Column")
        Me.chkMovieTrailerCol.Text = Master.eLang.GetString(467, "Hide Trailer Column")
        Me.chkMovieInfoCol.Text = Master.eLang.GetString(468, "Hide Info Column")
        Me.chkMovieFanartCol.Text = Master.eLang.GetString(469, "Hide Fanart Column")
        Me.chkMoviePosterCol.Text = Master.eLang.GetString(470, "Hide Poster Column")
        Me.chkMovieWatchedCol.Text = Master.eLang.GetString(982, "Hide Watched Column")
        Me.gbMovieFileNaming.Text = Master.eLang.GetString(471, "File Naming")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colPath.Text = Master.eLang.GetString(410, "Path")
        Me.colRecur.Text = Master.eLang.GetString(411, "Recursive")
        Me.colFolder.Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.colSingle.Text = Master.eLang.GetString(413, "Single Video")
        Me.btnMovieRem.Text = Master.eLang.GetString(30, "Remove")
        Me.btnRemTVSource.Text = Master.eLang.GetString(30, "Remove")
        Me.btnMovieAddFolder.Text = Master.eLang.GetString(407, "Add Source")
        Me.btnAddTVSource.Text = Me.btnMovieAddFolder.Text
        Me.gbMovieImagesPoster.Text = Master.eLang.GetString(148, "Poster")
        Me.Label24.Text = Master.eLang.GetString(478, "Quality:")
        Me.Label11.Text = Master.eLang.GetString(479, "Max Width:")
        Me.Label12.Text = Master.eLang.GetString(480, "Max Height:")
        Me.chkMovieResizePoster.Text = Master.eLang.GetString(481, "Automatically Resize:")
        Me.lblPosterSize.Text = Master.eLang.GetString(482, "Preferred Size:")
        Me.chkMovieOverwritePoster.Text = Master.eLang.GetString(483, "Overwrite Existing")
        Me.gbMovieImagesFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.chkMovieFanartOnly.Text = Master.eLang.GetString(145, "Only")
        Me.chkMoviePosterOnly.Text = Master.eLang.GetString(145, "Only")
        Me.Label26.Text = Me.Label24.Text
        Me.Label9.Text = Me.Label11.Text
        Me.Label10.Text = Me.Label12.Text
        Me.chkMovieResizeFanart.Text = Me.chkMovieResizePoster.Text
        Me.lblFanartSize.Text = Me.lblPosterSize.Text
        Me.chkMovieOverwriteFanart.Text = Me.chkMovieOverwritePoster.Text
        Me.GroupBox10.Text = Master.eLang.GetString(488, "Global Locks")
        Me.chkLockTrailer.Text = Master.eLang.GetString(489, "Lock Trailer")
        Me.chkLockGenre.Text = Master.eLang.GetString(490, "Lock Genre")
        Me.chkLockRealStudio.Text = Master.eLang.GetString(491, "Lock Studio")
        Me.chkLockRating.Text = Master.eLang.GetString(492, "Lock Rating")
        Me.chkLockTagline.Text = Master.eLang.GetString(493, "Lock Tagline")
        Me.chkLockTitle.Text = Master.eLang.GetString(494, "Lock Title")
        Me.chkLockOutline.Text = Master.eLang.GetString(495, "Lock Outline")
        Me.chkLockPlot.Text = Master.eLang.GetString(496, "Lock Plot")
        Me.GroupBox9.Text = Master.eLang.GetString(497, "Images")
        Me.chkMovieNoSaveImagesToNfo.Text = Master.eLang.GetString(498, "Do Not Save URLs to Nfo")
        Me.chkMovieSingleScrapeImages.Text = Master.eLang.GetString(499, "Get on Single Scrape")
        Me.chkLockLanguageV.Text = Master.eLang.GetString(879, "Lock Language (video)")
        Me.chkLockLanguageA.Text = Master.eLang.GetString(880, "Lock Language (audio)")
        Me.chkLockMPAA.Text = Master.eLang.GetString(881, "Lock MPAA/Certification")
        Me.chkUseMPAAFSK.Text = Master.eLang.GetString(882, "Use MPAA as Fallback for FSK Rating")
        Me.chkOutlineForPlot.Text = Master.eLang.GetString(508, "Use Outline for Plot if Plot is Empty")
        Me.chkPlotForOutline.Text = Master.eLang.GetString(965, "Use Plot for Outline if Outline is Empty")
        Me.chkOutlinePlotEnglishOverwrite.Text = Master.eLang.GetString(1005, "Only overwrite english Outline and Plot")
        Me.chkCastWithImg.Text = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        Me.chkUseCertForMPAA.Text = Master.eLang.GetString(511, "Use Certification for MPAA")
        Me.chkFullCast.Text = Master.eLang.GetString(512, "Scrape Full Cast")
        Me.chkFullCrew.Text = Master.eLang.GetString(513, "Scrape Full Crew")
        Me.chkCert.Text = Master.eLang.GetString(514, "Use Certification Language:")
        Me.gbRTFormat.Text = Master.eLang.GetString(515, "Duration Format")
        Me.chkUseMIDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.gbTVScraperDuration.Text = Master.eLang.GetString(515, "Duration Format")
        Me.chkUseEPDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.chkScanMediaInfo.Text = Master.eLang.GetString(517, "Scan Meta Data")
        Me.chkTVScanMetaData.Text = Me.chkScanMediaInfo.Text
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnApply.Text = Master.eLang.GetString(276, "Apply")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.lblTopDetails.Text = Master.eLang.GetString(518, "Configure Ember's appearance and operation.")
        Me.lblTopTitle.Text = Me.Text
        Me.gbMovieBackdropsFolder.Text = Master.eLang.GetString(520, "Backdrops Folder")
        Me.chkAutoBD.Text = Master.eLang.GetString(521, "Automatically Save Fanart To Backdrops Folder")
        Me.GroupBox26.Text = Master.eLang.GetString(59, "Meta Data")
        Me.GroupBox31.Text = Me.GroupBox26.Text
        Me.gbMovieSetsFolder.Text = Master.eLang.GetString(986, "Movieset Artwork Folder")

        Me.chkDeleteAllTrailers.Text = Master.eLang.GetString(522, "Delete All Existing")
        Me.chkOverwriteTrailer.Text = Master.eLang.GetString(483, "Overwrite Existing")

        Me.chkDownloadTrailer.Text = Master.eLang.GetString(529, "Enable Trailer Support")
        Me.GroupBox22.Text = Master.eLang.GetString(530, "No Stack Extensions")

        Me.GroupBox18.Text = Master.eLang.GetString(534, "Valid Video Extensions")
        Me.btnEditSource.Text = Master.eLang.GetString(535, "Edit Source")
        Me.btnEditTVSource.Text = Master.eLang.GetString(535, "Edit Source")
        Me.gbMovieMiscOptions.Text = Master.eLang.GetString(536, "Miscellaneous Options")
        Me.gbMiscTVSourceOpts.Text = Master.eLang.GetString(536, "Miscellaneous Options")
        Me.chkMovieRecognizeVTSExpertVTS.Text = Master.eLang.GetString(537, "Automatically Detect VIDEO_TS Folders Even if They Are Not Named ""VIDEO_TS""")
        Me.chkSkipStackedSizeCheck.Text = Master.eLang.GetString(538, "Skip Size Check of Stacked Files")
        Me.Label21.Text = Master.eLang.GetString(539, "MB")
        Me.Label20.Text = Master.eLang.GetString(540, "Skip files smaller than:")
        Me.Label6.Text = Me.Label21.Text
        Me.Label7.Text = Me.Label20.Text
        Me.fbdBrowse.Description = Master.eLang.GetString(552, "Select the folder where you wish to store your backdrops.")
        Me.gbOptions.Text = Master.eLang.GetString(577, "Scraper Fields")
        Me.GroupBox32.Text = Master.eLang.GetString(577, "Scraper Fields")
        Me.chkCrew.Text = Master.eLang.GetString(391, "Other Crew")
        Me.chkMusicBy.Text = Master.eLang.GetString(392, "Music By")
        Me.chkProducers.Text = Master.eLang.GetString(393, "Producers")
        Me.chkWriters.Text = Master.eLang.GetString(394, "Writers")
        Me.chkStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        Me.chkGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkTagline.Text = Master.eLang.GetString(397, "Tagline")
        Me.chkCast.Text = Master.eLang.GetString(63, "Cast")
        Me.chkVotes.Text = Master.eLang.GetString(399, "Votes")
        Me.chkTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkRelease.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkCertification.Text = Master.eLang.GetString(722, "Certification")
        Me.chkYear.Text = Master.eLang.GetString(278, "Year")
        Me.chkTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkScraperShowEGU.Text = Master.eLang.GetString(723, "EpisodeGuideURL")
        Me.chkScraperShowGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkScraperShowMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        Me.chkScraperShowRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkScraperShowStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkScraperShowActors.Text = Master.eLang.GetString(725, "Actors")
        Me.chkScraperEpTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkScraperEpSeason.Text = Master.eLang.GetString(650, "Season")
        Me.chkScraperEpEpisode.Text = Master.eLang.GetString(727, "Episode")
        Me.chkScraperEpAired.Text = Master.eLang.GetString(728, "Aired")
        Me.chkScraperEpRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkScraperEpPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkScraperEpDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkScraperEpCredits.Text = Master.eLang.GetString(729, "Credits")
        Me.chkScraperEpActors.Text = Master.eLang.GetString(725, "Actors")
        Me.GroupBox1.Text = Me.GroupBox4.Text
        Me.lblLimit.Text = Master.eLang.GetString(578, "Limit:")
        Me.lblLimit2.Text = Me.lblLimit.Text
        Me.lblLimit3.Text = Me.lblLimit.Text
        Me.GroupBox27.Text = Master.eLang.GetString(581, "Missing Items Filter")
        Me.chkMissingPoster.Text = Master.eLang.GetString(582, "Check for Poster")
        Me.chkMissingFanart.Text = Master.eLang.GetString(583, "Check for Fanart")
        Me.chkMissingNFO.Text = Master.eLang.GetString(584, "Check for NFO")
        Me.chkMissingTrailer.Text = Master.eLang.GetString(585, "Check for Trailer")
        Me.chkMissingSubs.Text = Master.eLang.GetString(586, "Check for Subs")
        Me.chkMissingEThumbs.Text = Master.eLang.GetString(587, "Check for Extrathumbs")
        Me.chkMissingEFanarts.Text = Master.eLang.GetString(976, "Check for Extrafanarts")
        Me.chkTop250.Text = Master.eLang.GetString(591, "Top 250")
        Me.chkCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkClickScrape.Text = Master.eLang.GetString(849, "Enable Click Scrape")

        Me.Label35.Text = String.Concat(Master.eLang.GetString(620, "Movie Theme"), ":")
        Me.Label1.Text = String.Concat(Master.eLang.GetString(666, "TV Show Theme"), ":")
        Me.Label3.Text = String.Concat(Master.eLang.GetString(667, "Episode Theme"), ":")
        Me.GroupBox28.Text = Master.eLang.GetString(625, "Defaults by File Type")
        Me.gbTVMIDefaults.Text = Me.gbTVMIDefaults.Text
        Me.Label34.Text = Master.eLang.GetString(626, "File Type")
        Me.Label49.Text = Me.Label34.Text
        Me.chkIFOScan.Text = Master.eLang.GetString(628, "Enable IFO Parsing")
        Me.GroupBox29.Text = Master.eLang.GetString(629, "Themes")
        Me.chkCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        Me.chkTVCleanDB.Text = Me.chkCleanDB.Text
        Me.chkIgnoreLastScan.Text = Master.eLang.GetString(669, "Ignore last scan time when updating library")
        Me.chkTVIgnoreLastScan.Text = Me.chkIgnoreLastScan.Text
        Me.gbShowFilter.Text = Master.eLang.GetString(670, "Show Folder/File Name Filters")
        Me.gbEpFilter.Text = Master.eLang.GetString(671, "Episode Folder/File Name Filters")
        Me.gbProxy.Text = Master.eLang.GetString(672, "Proxy")
        Me.chkEnableProxy.Text = Master.eLang.GetString(673, "Enable Proxy")
        Me.lblProxyURI.Text = Master.eLang.GetString(674, "Proxy URL:")
        Me.lblProxyPort.Text = Master.eLang.GetString(675, "Proxy Port:")
        Me.gbCreds.Text = Master.eLang.GetString(676, "Credentials")
        Me.chkEnableCredentials.Text = Master.eLang.GetString(677, "Enable Credentials")
        Me.lblProxyUN.Text = Master.eLang.GetString(425, "Username:")
        Me.lblProxyPW.Text = Master.eLang.GetString(426, "Password:")
        Me.lblProxyDomain.Text = Master.eLang.GetString(678, "Domain:")
        Me.gbTVMisc.Text = Me.GroupBox4.Text
        Me.lblRatingRegion.Text = Master.eLang.GetString(679, "TV Rating Region")
        Me.gbTVListOptions.Text = Master.eLang.GetString(460, "Media List Options")
        Me.gbShowListOptions.Text = Master.eLang.GetString(680, "Shows")
        Me.gbSeasonListOptions.Text = Master.eLang.GetString(681, "Seasons")
        Me.gbEpisodeListOptions.Text = Master.eLang.GetString(682, "Episodes")
        Me.chkShowPosterCol.Text = Me.chkMoviePosterCol.Text
        Me.chkSeasonPosterCol.Text = Me.chkMoviePosterCol.Text
        Me.chkEpisodePosterCol.Text = Me.chkMoviePosterCol.Text
        Me.chkShowFanartCol.Text = Me.chkMovieFanartCol.Text
        Me.chkSeasonFanartCol.Text = Me.chkMovieFanartCol.Text
        Me.chkEpisodeFanartCol.Text = Me.chkMovieFanartCol.Text
        Me.chkShowNfoCol.Text = Me.chkMovieInfoCol.Text
        Me.chkEpisodeNfoCol.Text = Me.chkMovieInfoCol.Text
        Me.btnEditShowRegex.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.btnRemoveShowRegex.Text = Master.eLang.GetString(30, "Remove")
        Me.gbShowRegex.Text = Master.eLang.GetString(691, "Show Match Regex")
        Me.lblSeasonMatch.Text = Master.eLang.GetString(692, "Season Match Regex:")
        Me.lblEpisodeMatch.Text = Master.eLang.GetString(693, "Episode Match Regex:")
        Me.lblSeasonRetrieve.Text = String.Concat(Master.eLang.GetString(694, "Apply To"), ":")
        Me.lblEpisodeRetrieve.Text = Me.lblSeasonRetrieve.Text
        Me.btnAddShowRegex.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.gbShowPosterOpts.Text = Me.gbMovieImagesPoster.Text
        Me.lblShowPosterSize.Text = Master.eLang.GetString(730, "Preferred Type:")
        Me.chkOverwriteShowPoster.Text = Me.chkMovieOverwritePoster.Text
        Me.chkResizeShowPoster.Text = Me.chkMovieResizePoster.Text
        Me.lblShowPosterWidth.Text = Me.Label11.Text
        Me.lblShowPosterHeight.Text = Me.Label12.Text
        Me.lblShowPosterQ.Text = Me.Label24.Text
        Me.gbShowFanartOpts.Text = Me.gbMovieImagesFanart.Text
        Me.lblShowFanartSize.Text = Me.lblFanartSize.Text
        Me.chkOverwriteShowFanart.Text = Me.chkMovieOverwriteFanart.Text
        Me.chkResizeShowFanart.Text = Me.chkMovieResizeFanart.Text
        Me.lblShowFanartWidth.Text = Me.Label11.Text
        Me.lblShowFanartHeight.Text = Me.Label12.Text
        Me.lblShowFanartQ.Text = Me.Label26.Text
        Me.gbEpPosterOpts.Text = Me.gbMovieImagesPoster.Text
        Me.chkOverwriteEpPoster.Text = Me.chkMovieOverwritePoster.Text
        Me.chkResizeEpPoster.Text = Me.chkMovieResizePoster.Text
        Me.lblEpPosterWidth.Text = Me.Label11.Text
        Me.lblEpPosterHeight.Text = Me.Label12.Text
        Me.lblEpPosterQ.Text = Me.Label24.Text
        Me.gbEpFanartOpts.Text = Me.gbMovieImagesFanart.Text
        Me.lblEpFanartSize.Text = Me.lblFanartSize.Text
        Me.chkOverwriteEpFanart.Text = Me.chkMovieOverwriteFanart.Text
        Me.chkResizeEpFanart.Text = Me.chkMovieResizeFanart.Text
        Me.lblEpFanartWidth.Text = Me.Label11.Text
        Me.lblEpFanartHeight.Text = Me.Label12.Text
        Me.lblEpFanartQ.Text = Me.Label26.Text
        Me.gbSeaPosterOpts.Text = Me.gbMovieImagesPoster.Text
        Me.lblSeaPosterSize.Text = Me.lblShowPosterSize.Text
        Me.chkSeaOverwritePoster.Text = Me.chkMovieOverwritePoster.Text
        Me.chkSeaResizePoster.Text = Me.chkMovieResizePoster.Text
        Me.lblSeaPosterWidth.Text = Me.Label11.Text
        Me.lblSeaPosterHeight.Text = Me.Label12.Text
        Me.lblSeaPosterQ.Text = Me.Label24.Text
        Me.gbSeaFanartOpts.Text = Me.gbMovieImagesFanart.Text
        Me.lblSeaFanartSize.Text = Me.lblFanartSize.Text
        Me.chkSeaOverwriteFanart.Text = Me.chkMovieOverwriteFanart.Text
        Me.chkSeaResizeFanart.Text = Me.chkMovieResizeFanart.Text
        Me.lblSeaFanartWidth.Text = Me.Label11.Text
        Me.lblSeaFanartHeight.Text = Me.Label12.Text
        Me.lblSeaFanartQ.Text = Me.Label26.Text
        Me.Label51.Text = Master.eLang.GetString(732, "<h>=Hours <m>=Minutes <s>=Seconds")
        Me.chkDisplayMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        Me.chkForceTitle.Text = Master.eLang.GetString(710, "Force Title Language:")
        Me.chkTitleFallback.Text = Master.eLang.GetString(984, "Worldwide title as fallback")
        Me.chkSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        Me.chkSortBeforeScan.Text = Master.eLang.GetString(712, "Sort files into folder before each library update")
        Me.chkNoFilterEpisode.Text = Master.eLang.GetString(734, "Build Episode Title Instead of Filtering")
        Me.lblTVUpdate.Text = Master.eLang.GetString(740, "Re-download Show Information Every:")
        Me.GroupBox33.Text = Master.eLang.GetString(488, "Global Locks")
        Me.gbShowLocks.Text = Master.eLang.GetString(743, "Show")
        Me.chkShowLockTitle.Text = Master.eLang.GetString(494, "Lock Title")
        Me.chkShowLockPlot.Text = Master.eLang.GetString(496, "Lock Plot")
        Me.chkShowLockRating.Text = Master.eLang.GetString(492, "Lock Rating")
        Me.chkShowLockGenre.Text = Master.eLang.GetString(490, "Lock Genre")
        Me.chkShowLockStudio.Text = Master.eLang.GetString(491, "Lock Studio")
        Me.gbEpLocks.Text = Master.eLang.GetString(727, "Episode")
        Me.chkEpLockTitle.Text = Master.eLang.GetString(494, "Lock Title")
        Me.chkEpLockPlot.Text = Master.eLang.GetString(496, "Lock Plot")
        Me.chkEpLockRating.Text = Master.eLang.GetString(492, "Lock Rating")
        Me.GroupBox35.Text = Master.eLang.GetString(743, "Show")
        Me.GroupBox34.Text = Master.eLang.GetString(727, "Episode")
        Me.gbInterface.Text = Master.eLang.GetString(795, "Interface")
        Me.chkScanOrderModify.Text = Master.eLang.GetString(796, "Scan in order of last write time")
        Me.chkTVScanOrderModify.Text = Me.chkScanOrderModify.Text
        Me.lblPreferredQuality.Text = Master.eLang.GetString(800, "Preferred Quality:")
        Me.gbTVScraperOptions.Text = Master.eLang.GetString(390, "Options")
        Me.chkDisplayAllSeason.Text = Master.eLang.GetString(832, "Display All Season Poster")
        Me.gbHelp.Text = String.Concat("     ", Master.eLang.GetString(458, "Help"))
        Me.chkMarkNewShows.Text = Master.eLang.GetString(549, "Mark New Shows")
        Me.chkMarkNewEpisodes.Text = Master.eLang.GetString(621, "Mark New Episodes")
        Me.lblOrdering.Text = Master.eLang.GetString(797, "Default Episode Ordering:")
        Me.chkOnlyValueForCert.Text = Master.eLang.GetString(835, "Only Save the Value to NFO")
        Me.chkMovieScraperActorThumbs.Text = Master.eLang.GetString(828, "Enable Actor Thumbs")
        Me.rbBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.rbPoster.Text = Me.gbMovieImagesPoster.Text
        Me.rbAllSBanner.Text = Me.rbBanner.Text
        Me.rbAllSPoster.Text = Me.gbMovieImagesPoster.Text
        Me.gbAllSPosterOpts.Text = Master.eLang.GetString(735, "All Season Posters")
        Me.lblAllSPosterSize.Text = Me.lblShowPosterSize.Text
        Me.chkOverwriteAllSPoster.Text = Me.chkMovieOverwritePoster.Text
        Me.chkResizeAllSPoster.Text = Me.chkMovieResizePoster.Text
        Me.lblAllSPosterWidth.Text = Me.Label11.Text
        Me.lblAllSPosterHeight.Text = Me.Label12.Text
        Me.lblAllSPosterQ.Text = Me.Label24.Text
        Me.btnClearRegex.Text = Master.eLang.GetString(123, "Clear")
        Me.chkAskCheckboxScrape.Text = Master.eLang.GetString(852, "Ask On Click Scrape")

        Me.lvTVSources.Columns(1).Text = Master.eLang.GetString(232, "Name")
        Me.lvTVSources.Columns(2).Text = Master.eLang.GetString(410, "Path")

        Me.lvShowRegex.Columns(1).Text = Master.eLang.GetString(696, "Show Regex")
        Me.lvShowRegex.Columns(2).Text = Master.eLang.GetString(694, "Apply To")
        Me.lvShowRegex.Columns(3).Text = Master.eLang.GetString(697, "Episode Regex")
        Me.lvShowRegex.Columns(4).Text = Master.eLang.GetString(694, "Apply To")

        Me.lvMovies.Columns(1).Text = Master.eLang.GetString(232, "Name")
        Me.lvMovies.Columns(2).Text = Master.eLang.GetString(410, "Path")
        Me.lvMovies.Columns(3).Text = Master.eLang.GetString(411, "Recursive")
        Me.lvMovies.Columns(4).Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.lvMovies.Columns(5).Text = Master.eLang.GetString(413, "Single Video")

        Me.TabPage3.Text = Master.eLang.GetString(38, "General")
        Me.TabPage4.Text = Master.eLang.GetString(699, "Regex")
        Me.TabPage5.Text = Master.eLang.GetString(700, "TV Show")
        Me.TabPage6.Text = Master.eLang.GetString(744, "TV Season")
        Me.TabPage7.Text = Master.eLang.GetString(701, "TV Episode")

        Me.cbMoviePosterSize.Items.Clear()
        Me.cbMoviePosterSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small"), Master.eLang.GetString(558, "Wide")})
        Me.cbMovieFanartSize.Items.Clear()
        Me.cbMovieFanartSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small")})
        Me.cbMovieEFanartsSize.Items.Clear()
        Me.cbMovieEFanartsSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small")})
        Me.cbMovieEThumbsSize.Items.Clear()
        Me.cbMovieEThumbsSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small")})
        Me.cbShowFanartSize.Items.Clear()
        Me.cbShowFanartSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small")})
        Me.cbEpFanartSize.Items.Clear()
        Me.cbEpFanartSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small")})
        Me.cbSeaPosterSize.Items.Clear()
        Me.cbSeaPosterSize.Items.AddRange(New String() {Master.eLang.GetString(745, "None"), Me.gbMovieImagesPoster.Text, Master.eLang.GetString(558, "Wide")})
        Me.cbSeaFanartSize.Items.Clear()
        Me.cbSeaFanartSize.Items.AddRange(New String() {Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small")})

        Me.cboTVUpdate.Items.Clear()
        Me.cboTVUpdate.Items.AddRange(New String() {Master.eLang.GetString(749, "Week"), Master.eLang.GetString(750, "Bi-Weekly"), Master.eLang.GetString(751, "Month"), Master.eLang.GetString(752, "Never"), Master.eLang.GetString(753, "Always")})

        Me.cbOrdering.Items.Clear()
        Me.cbOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(350, "DVD"), Master.eLang.GetString(839, "Absolute")})

        Me.cboSeasonRetrieve.Items.Clear()
        Me.cboSeasonRetrieve.Items.AddRange(New String() {Master.eLang.GetString(13, "Folder Name"), Master.eLang.GetString(15, "File Name")})

        Me.cboEpRetrieve.Items.Clear()
        Me.cboEpRetrieve.Items.AddRange(New String() {Master.eLang.GetString(13, "Folder Name"), Master.eLang.GetString(15, "File Name"), Master.eLang.GetString(16, "Season Result")})
        Me.GroupBox30.Text = Master.eLang.GetString(885, "IMDB")

        Me.LoadTrailerQualities()
    End Sub

    Private Sub tbAllSPosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbAllSPosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblAllSPosterQual.Text = tbAllSPosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblAllSPosterQual
            Select Case True
                Case tbAllSPosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbAllSPosterQual.Value > 95 OrElse tbAllSPosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbAllSPosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbAllSPosterQual.Value, 300 - tbAllSPosterQual.Value, 0)
                Case tbAllSPosterQual.Value >= 80 AndAlso tbAllSPosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbAllSPosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbAllSPosterQual.Value - 20)), 0)
                Case tbAllSPosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbAllSPosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbEpFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbEpFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblEpFanartQual.Text = tbEpFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblEpFanartQual
            Select Case True
                Case tbEpFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbEpFanartQual.Value > 95 OrElse tbEpFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbEpFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbEpFanartQual.Value, 300 - tbEpFanartQual.Value, 0)
                Case tbEpFanartQual.Value >= 80 AndAlso tbEpFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbEpFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbEpFanartQual.Value - 20)), 0)
                Case tbEpFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbEpFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbEpPosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbEpPosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblEpPosterQual.Text = tbEpPosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblEpPosterQual
            Select Case True
                Case tbEpPosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbEpPosterQual.Value > 95 OrElse tbEpPosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbEpPosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbEpPosterQual.Value, 300 - tbEpPosterQual.Value, 0)
                Case tbEpPosterQual.Value >= 80 AndAlso tbEpPosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbEpPosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbEpPosterQual.Value - 20)), 0)
                Case tbEpPosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbEpPosterQual.Value - 50))), 255, 0)
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

    Private Sub tbSeaFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbSeaFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblSeaFanartQual.Text = tbSeaFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblSeaFanartQual
            Select Case True
                Case tbSeaFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbSeaFanartQual.Value > 95 OrElse tbSeaFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbSeaFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbSeaFanartQual.Value, 300 - tbSeaFanartQual.Value, 0)
                Case tbSeaFanartQual.Value >= 80 AndAlso tbSeaFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbSeaFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbSeaFanartQual.Value - 20)), 0)
                Case tbSeaFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbSeaFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbSeaPosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbSeaPosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblSeaPosterQual.Text = tbSeaPosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblSeaPosterQual
            Select Case True
                Case tbSeaPosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbSeaPosterQual.Value > 95 OrElse tbSeaPosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbSeaPosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbSeaPosterQual.Value, 300 - tbSeaPosterQual.Value, 0)
                Case tbSeaPosterQual.Value >= 80 AndAlso tbSeaPosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbSeaPosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbSeaPosterQual.Value - 20)), 0)
                Case tbSeaPosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbSeaPosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbShowFanartQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbShowFanartQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblShowFanartQual.Text = tbShowFanartQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblShowFanartQual
            Select Case True
                Case tbShowFanartQual.Value = 0
                    .ForeColor = Color.Black
                Case tbShowFanartQual.Value > 95 OrElse tbShowFanartQual.Value < 20
                    .ForeColor = Color.Red
                Case tbShowFanartQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbShowFanartQual.Value, 300 - tbShowFanartQual.Value, 0)
                Case tbShowFanartQual.Value >= 80 AndAlso tbShowFanartQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbShowFanartQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbShowFanartQual.Value - 20)), 0)
                Case tbShowFanartQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbShowFanartQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tbShowPosterQual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbShowPosterQual.ValueChanged
        Me.SetApplyButton(True)
        Me.lblShowPosterQual.Text = tbShowPosterQual.Value.ToString
        'change text color to indicate recommendations
        With Me.lblShowPosterQual
            Select Case True
                Case tbShowPosterQual.Value = 0
                    .ForeColor = Color.Black
                Case tbShowPosterQual.Value > 95 OrElse tbShowPosterQual.Value < 20
                    .ForeColor = Color.Red
                Case tbShowPosterQual.Value > 85
                    .ForeColor = Color.FromArgb(255, 155 + tbShowPosterQual.Value, 300 - tbShowPosterQual.Value, 0)
                Case tbShowPosterQual.Value >= 80 AndAlso tbShowPosterQual.Value <= 85
                    .ForeColor = Color.Blue
                Case tbShowPosterQual.Value <= 50
                    .ForeColor = Color.FromArgb(255, 255, Convert.ToInt32(8.5 * (tbShowPosterQual.Value - 20)), 0)
                Case tbShowPosterQual.Value < 80
                    .ForeColor = Color.FromArgb(255, Convert.ToInt32(255 - (8.5 * (tbShowPosterQual.Value - 50))), 255, 0)
            End Select
        End With
    End Sub

    Private Sub tcCleaner_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcCleaner.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        currText = DirectCast(sender, ToolStripButton).Text
        Me.FillList(currText)
    End Sub

    Private Sub tvSettings_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSettings.AfterSelect
        Me.pbCurrent.Image = Me.ilSettings.Images(Me.tvSettings.SelectedNode.ImageIndex)
        Me.lblCurrent.Text = String.Format("{0} - {1}", Me.currText, Me.tvSettings.SelectedNode.Text)

        Me.RemoveCurrPanel()

        Me.currPanel = Me.SettingsPanels.FirstOrDefault(Function(p) p.Name = tvSettings.SelectedNode.Name).Panel
        Me.currPanel.Location = New Point(0, 0)
        Me.pnlMain.Controls.Add(Me.currPanel)
        Me.currPanel.Visible = True
        Me.pnlMain.Refresh()
    End Sub

    Private Sub txtActorLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtActorLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtActorLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtActorLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtAllSPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAllSPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtAllSPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAllSPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtAllSPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAllSPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtAllSPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAllSPosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtBDPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBDPath.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtCheckTitleTol_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCheckTitleTol.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtCheckTitleTol_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCheckTitleTol.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtDefFIExt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDefFIExt.TextChanged
        btnNewMetaDataFT.Enabled = Not String.IsNullOrEmpty(txtDefFIExt.Text) AndAlso Not Me.lstMetaData.Items.Contains(If(txtDefFIExt.Text.StartsWith("."), txtDefFIExt.Text, String.Concat(".", txtDefFIExt.Text)))
        If btnNewMetaDataFT.Enabled Then
            btnEditMetaDataFT.Enabled = False
            btnRemoveMetaDataFT.Enabled = False
        End If
    End Sub

    Private Sub txtEpFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEpFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtEpFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEpFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtEpFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEpFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtEpFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEpFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtEpPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEpPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtEpPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEpPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtEpPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEpPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtEpPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEpPosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtEpRegex_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEpRegex.TextChanged
        Me.ValidateRegex()
    End Sub

    Private Sub txtFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtGenreLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGenreLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtGenreLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGenreLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtOutlineLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOutlineLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtOutlineLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOutlineLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoviePosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoviePosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviePosterWidth.TextChanged
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

    Private Sub txtRuntimeFormat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRuntimeFormat.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtEPRuntimeFormat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEPRuntimeFormat.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtSeaFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeaFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtSeaFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeaFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtSeaPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeaPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtSeaPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeaPosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtSeasonRegex_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeasonRegex.TextChanged
        Me.ValidateRegex()
    End Sub

    Private Sub txtShowFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShowFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtShowFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShowFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtShowFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShowFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtShowFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShowFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtShowPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShowPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtShowPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShowPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtShowPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShowPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtShowPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShowPosterWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtSkipLessThan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSkipLessThan.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtSkipLessThan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSkipLessThan.TextChanged
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

    Private Sub txtTVDefFIExt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVDefFIExt.TextChanged
        btnNewTVMetaDataFT.Enabled = Not String.IsNullOrEmpty(txtTVDefFIExt.Text) AndAlso Not Me.lstTVMetaData.Items.Contains(If(txtTVDefFIExt.Text.StartsWith("."), txtTVDefFIExt.Text, String.Concat(".", txtTVDefFIExt.Text)))
        If btnNewTVMetaDataFT.Enabled Then
            btnEditTVMetaDataFT.Enabled = False
            btnRemoveTVMetaDataFT.Enabled = False
        End If
    End Sub

    Private Sub txtMovieYAMJWatchedFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieYAMJWatchedFolder.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub ValidateRegex()
        If Not String.IsNullOrEmpty(Me.txtSeasonRegex.Text) AndAlso Not String.IsNullOrEmpty(Me.txtEpRegex.Text) Then
            If Me.cboSeasonRetrieve.SelectedIndex > -1 AndAlso Me.cboEpRetrieve.SelectedIndex > -1 Then
                Me.btnAddShowRegex.Enabled = True
            Else
                Me.btnAddShowRegex.Enabled = False
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
    Private Sub txtMoviesetsPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMoviesetsPath.TextChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub btnBrowseMoviesets_Click(sender As Object, e As EventArgs) Handles btnBrowseMoviesets.Click
        With Me.fbdBrowse
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtMoviesetsPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub
    Private Sub btnMovieBrowseWatchedFiles_Click(sender As Object, e As EventArgs) Handles btnMovieBrowseWatchedFiles.Click
        With Me.fbdBrowse
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtMovieYAMJWatchedFolder.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub cbo_DAEMON_driveletter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_DAEMON_driveletter.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txt_DAEMON_Programpath_TextChanged(sender As Object, e As EventArgs) Handles txt_DAEMON_Programpath.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub bt_DAEMON_Programpath_Click(sender As Object, e As EventArgs) Handles bt_DAEMON_Programpath.Click
        Try
            With Me.fileBrowse
                .Filter = "Exe (*.exe*)|*.exe*|Exe (*.exe*)|*.exe*"
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.FileName) Then
                        Me.txt_DAEMON_Programpath.Text = .FileName

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

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseFrodo.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodeActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVEpisodeFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
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
            Me.chkTVEpisodeFanartFrodo.Checked = False
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
            Me.chkTVEpisodeFanartFrodo.Checked = True
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

    Private Sub chkTVEpisodeFanartFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeFanartFrodo.CheckedChanged
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

        Me.btnTVBrowseTVTheme.Enabled = Me.chkTVShowTVThemeXBMC.Checked
        Me.txtTVShowTVThemeFolderXBMC.Enabled = Me.chkTVShowTVThemeXBMC.Checked
    End Sub

    Private Sub txtTVShowTVThemeFolderXBMC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowTVThemeFolderXBMC.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVBrowseTVTheme_Click(sender As Object, e As EventArgs) Handles btnTVBrowseTVTheme.Click
        With Me.fbdBrowse
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtTVShowTVThemeFolderXBMC.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

#End Region 'Methods

End Class