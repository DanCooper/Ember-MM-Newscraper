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
' # Dialog size: 1120; 900
' # Move the panels (pnl*) from 900;900 to 0;0 to edit. Move it back after editing.

Imports System
Imports System.IO
Imports EmberAPI
Imports System.Net
Imports NLog

Public Class dlgSettings

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private currPanel As New Panel
    Private currText As String = String.Empty
    Private dHelp As New Dictionary(Of String, String)
    Private didApply As Boolean = False
    Private MovieMeta As New List(Of Settings.MetadataPerType)
    Private MovieGeneralMediaListSorting As New List(Of Settings.ListSorting)
    Private MovieSetGeneralMediaListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralEpisodeListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralSeasonListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralShowListSorting As New List(Of Settings.ListSorting)
    Private NoUpdate As Boolean = True
    Private SettingsPanels As New List(Of Containers.SettingsPanel)
    Private TVShowRegex As New List(Of Settings.TVShowRegEx)
    Private sResult As New Structures.SettingsResult
    'Private tLangList As New List(Of Containers.TVLanguage)
    Private TVMeta As New List(Of Settings.MetadataPerType)
    Public Event LoadEnd()

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog() As Structures.SettingsResult
        MyBase.ShowDialog()
        Return Me.sResult
    End Function

    Private Sub dlgSettings_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Dim iBackground As New Bitmap(Me.pnlSettingsTop.Width, Me.pnlSettingsTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlSettingsTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsTop.ClientRectangle)
            Me.pnlSettingsTop.BackgroundImage = iBackground
        End Using

        iBackground = New Bitmap(Me.pnlSettingsCurrent.Width, Me.pnlSettingsCurrent.Height)
        Using b As Graphics = Graphics.FromImage(iBackground)
            b.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlSettingsCurrent.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsCurrent.ClientRectangle)
            Me.pnlSettingsCurrent.BackgroundImage = iBackground
        End Using

        If Me.tsSettingsTopMenu.Items.Count > 0 Then
            Dim ButtonsWidth As Integer = 0
            Dim ButtonsCount As Integer = 0
            Dim sLength As Integer = 0
            Dim sRest As Double = 0
            Dim sSpacer As String = String.Empty

            'calculate the buttons width and count
            For Each item As ToolStripItem In Me.tsSettingsTopMenu.Items
                If TypeOf item Is ToolStripButton Then
                    ButtonsWidth += item.Width
                    ButtonsCount += 1
                End If
            Next

            sRest = (Me.tsSettingsTopMenu.Width - ButtonsWidth - 1) / (ButtonsCount + 1)

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

            For Each item As ToolStripItem In Me.tsSettingsTopMenu.Items
                If Not IsNothing(item.Tag) AndAlso item.Tag.ToString = "spacer" Then
                    item.Text = sSpacer
                End If
            Next
        End If
    End Sub

    Private Sub AddButtons()
        Dim TSBs As New List(Of ToolStripButton)
        Dim TSB As ToolStripButton

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
              .Text = Master.eLang.GetString(1203, "MovieSets"), _
              .Image = My.Resources.MovieSet, _
              .TextImageRelation = TextImageRelation.ImageAboveText, _
              .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
              .Tag = 300}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)
        TSB = New ToolStripButton With { _
              .Text = Master.eLang.GetString(653, "TV Shows"), _
              .Image = My.Resources.TVShows, _
              .TextImageRelation = TextImageRelation.ImageAboveText, _
              .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
              .Tag = 400}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)
        TSB = New ToolStripButton With { _
              .Text = Master.eLang.GetString(802, "Modules"), _
              .Image = My.Resources.modules, _
              .TextImageRelation = TextImageRelation.ImageAboveText, _
              .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
              .Tag = 500}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)

        TSB = New ToolStripButton With { _
            .Text = Master.eLang.GetString(429, "Miscellaneous"), _
            .Image = My.Resources.Miscellaneous, _
            .TextImageRelation = TextImageRelation.ImageAboveText, _
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText, _
            .Tag = 600}
        AddHandler TSB.Click, AddressOf ToolStripButton_Click
        TSBs.Add(TSB)

        If TSBs.Count > 0 Then
            Dim ButtonsWidth As Integer = 0
            Dim ButtonsCount As Integer = 0
            Dim sLength As Integer = 0
            Dim sRest As Double = 0
            Dim sSpacer As String = String.Empty

            'add it all
            For Each tButton As ToolStripButton In TSBs.OrderBy(Function(b) Convert.ToInt32(b.Tag))
                Me.tsSettingsTopMenu.Items.Add(New ToolStripLabel With {.Text = String.Empty, .Tag = "spacer"})
                Me.tsSettingsTopMenu.Items.Add(tButton)
            Next

            'calculate the buttons width and count
            For Each item As ToolStripItem In Me.tsSettingsTopMenu.Items
                If TypeOf item Is ToolStripButton Then
                    ButtonsWidth += item.Width
                    ButtonsCount += 1
                End If
            Next

            sRest = (Me.tsSettingsTopMenu.Width - ButtonsWidth - 1) / (ButtonsCount + 1)

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

            For Each item As ToolStripItem In Me.tsSettingsTopMenu.Items
                If Not IsNothing(item.Tag) AndAlso item.Tag.ToString = "spacer" Then
                    item.Text = sSpacer
                End If
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
             .Name = "pnlMovieSets", _
             .Text = Master.eLang.GetString(38, "General"), _
             .ImageIndex = 2, _
             .Type = Master.eLang.GetString(1203, "MovieSets"), _
             .Panel = Me.pnlMovieSetGeneral, _
             .Order = 100})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovieSetSources", _
             .Text = Master.eLang.GetString(555, "Files and Sources"), _
             .ImageIndex = 5, _
             .Type = Master.eLang.GetString(1203, "MovieSets"), _
             .Panel = Me.pnlMovieSetSources, _
             .Order = 200})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovieSetData", _
             .Text = Master.eLang.GetString(556, "Scrapers - Data"), _
             .ImageIndex = 3, _
             .Type = Master.eLang.GetString(1203, "MovieSets"), _
             .Panel = Me.pnlMovieSetScraper, _
             .Order = 300})
        Me.SettingsPanels.Add(New Containers.SettingsPanel With { _
             .Name = "pnlMovieSetMedia", _
             .Text = Master.eLang.GetString(557, "Scrapers - Images"), _
             .ImageIndex = 6, _
             .Type = Master.eLang.GetString(1203, "MovieSets"), _
             .Panel = Me.pnlMovieSetImages, _
             .Order = 400})
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
        For Each s As ModulesManager._externalScraperModuleClass_Data_Movie In ModulesManager.Instance.externalScrapersModules_Data_Movie.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Data_MovieSet In ModulesManager.Instance.externalScrapersModules_Data_MovieSet.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In ModulesManager.Instance.externalScrapersModules_Image_Movie.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In ModulesManager.Instance.externalScrapersModules_Image_MovieSet.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Image_TV In ModulesManager.Instance.externalScrapersModules_Image_TV.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In ModulesManager.Instance.externalScrapersModules_Theme_Movie.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In ModulesManager.Instance.externalScrapersModules_Trailer_Movie.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_TV In ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsScraper).OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_TV In ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.PostScraperOrder)
            tPanel = s.ProcessorModule.InjectSetupPostScraper
            tPanel.Order += ModuleCounter
            Me.SettingsPanels.Add(tPanel)
            ModuleCounter += 1
            AddHandler s.ProcessorModule.SetupPostScraperChanged, AddressOf Handle_ModuleSetupChanged
            AddHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            Me.AddHelpHandlers(tPanel.Panel, tPanel.Prefix)
        Next
        ModuleCounter = 1
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Data_Movie In ModulesManager.Instance.externalScrapersModules_Data_Movie.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Data_MovieSet In ModulesManager.Instance.externalScrapersModules_Data_MovieSet.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In ModulesManager.Instance.externalScrapersModules_Image_Movie.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In ModulesManager.Instance.externalScrapersModules_Image_MovieSet.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_TV In ModulesManager.Instance.externalScrapersModules_Image_TV.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In ModulesManager.Instance.externalScrapersModules_Theme_Movie.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In ModulesManager.Instance.externalScrapersModules_Trailer_Movie.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_TV In ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.PostScraperOrder)
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV.OrderBy(Function(x) x.ModuleOrder)
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

    Private Sub btnFileSystemExcludedDirsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemExcludedDirsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemExcludedDirs.Text) Then
            If Not lstFileSystemExcludedDirs.Items.Contains(txtFileSystemExcludedDirs.Text.ToLower) Then
                AddExcludedDir(txtFileSystemExcludedDirs.Text)
                Me.RefreshFileSystemExcludeDirs()
                txtFileSystemExcludedDirs.Text = String.Empty
                txtFileSystemExcludedDirs.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemExcludedDirsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemExcludedDirsRemove.Click
        Me.RemoveExcludeDir()
        Me.RefreshFileSystemExcludeDirs()
    End Sub

    Private Sub lstFileSystemExcludedDirs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstFileSystemExcludedDirs.KeyDown
        If e.KeyCode = Keys.Delete Then
            Me.RemoveExcludeDir()
            Me.RefreshFileSystemExcludeDirs()
        End If
    End Sub

    Private Sub AddExcludedDir(ByVal Path As String)
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "INSERT OR REPLACE INTO ExcludeDir (Dirname) VALUES (?);"

                    Dim parDirname As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDirname", DbType.String, 0, "Dirname")
                    parDirname.Value = Path

                    SQLcommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using

            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        Finally
            Master.DB.LoadExcludeDirsFromDB()
        End Try
    End Sub

    Private Sub RemoveExcludeDir()
        Try
            If Me.lstFileSystemExcludedDirs.SelectedItems.Count > 0 Then
                Me.lstFileSystemExcludedDirs.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parDirname As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDirname", DbType.String, 0, "Dirname")
                        While Me.lstFileSystemExcludedDirs.SelectedItems.Count > 0
                            parDirname.Value = lstFileSystemExcludedDirs.SelectedItems(0).ToString
                            SQLcommand.CommandText = String.Concat("DELETE FROM ExcludeDir WHERE Dirname = (?);")
                            SQLcommand.ExecuteNonQuery()
                            lstFileSystemExcludedDirs.Items.Remove(lstFileSystemExcludedDirs.SelectedItems(0))
                        End While
                    End Using
                    SQLtransaction.Commit()
                End Using

                Me.lstFileSystemExcludedDirs.EndUpdate()
                Me.lstFileSystemExcludedDirs.Refresh()

                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        Finally
            Master.DB.LoadExcludeDirsFromDB()
        End Try
    End Sub

    Private Sub btnFileSystemValidExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidVideoExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidVideoExts.Text) Then
            If Not Strings.Left(txtFileSystemValidVideoExts.Text, 1) = "." Then txtFileSystemValidVideoExts.Text = String.Concat(".", txtFileSystemValidVideoExts.Text)
            If Not lstFileSystemValidVideoExts.Items.Contains(txtFileSystemValidVideoExts.Text.ToLower) Then
                lstFileSystemValidVideoExts.Items.Add(txtFileSystemValidVideoExts.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
                txtFileSystemValidVideoExts.Text = String.Empty
                txtFileSystemValidVideoExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidSubtitlesExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidSubtitlesExts.Text) Then
            If Not Strings.Left(txtFileSystemValidSubtitlesExts.Text, 1) = "." Then txtFileSystemValidSubtitlesExts.Text = String.Concat(".", txtFileSystemValidSubtitlesExts.Text)
            If Not lstFileSystemValidSubtitlesExts.Items.Contains(txtFileSystemValidSubtitlesExts.Text.ToLower) Then
                lstFileSystemValidSubtitlesExts.Items.Add(txtFileSystemValidSubtitlesExts.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsUpdate = True
                txtFileSystemValidSubtitlesExts.Text = String.Empty
                txtFileSystemValidSubtitlesExts.Focus()
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
            Me.TVShowRegex.Add(New Settings.TVShowRegEx With {.ID = Convert.ToInt32(lID) + 1, .SeasonRegex = Me.txtTVSeasonRegex.Text, .SeasonFromDirectory = If(CType(Me.cbTVSeasonRetrieve.SelectedValue, Settings.EpRetrieve) = Settings.EpRetrieve.FromDirectory, True, False), .EpisodeRegex = Me.txtTVEpisodeRegex.Text, .EpisodeRetrieve = CType(Me.cbTVEpisodeRetrieve.SelectedValue, Settings.EpRetrieve), .byDate = Me.chkTVEpisodeAired.Checked})
        Else
            Dim selRex = From lRegex As Settings.TVShowRegEx In Me.TVShowRegex Where lRegex.ID = Convert.ToInt32(Me.btnTVShowRegexAdd.Tag)
            If selRex.Count > 0 Then
                selRex(0).SeasonRegex = Me.txtTVSeasonRegex.Text
                selRex(0).SeasonFromDirectory = If(CType(Me.cbTVSeasonRetrieve.SelectedValue, Settings.EpRetrieve) = Settings.EpRetrieve.FromDirectory, True, False)
                selRex(0).EpisodeRegex = Me.txtTVEpisodeRegex.Text
                selRex(0).EpisodeRetrieve = CType(Me.cbTVEpisodeRetrieve.SelectedValue, Settings.EpRetrieve)
                selRex(0).byDate = Me.chkTVEpisodeAired.Checked
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
                Me.sResult.NeedsRefresh_Movie = True
                Me.SetApplyButton(True)
                txtMovieSortToken.Text = String.Empty
                txtMovieSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnMovieSetSortTokenAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtMovieSetSortToken.Text) Then
            If Not lstMovieSetSortTokens.Items.Contains(txtMovieSetSortToken.Text) Then
                lstMovieSetSortTokens.Items.Add(txtMovieSetSortToken.Text)
                Me.sResult.NeedsRefresh_MovieSet = True
                Me.SetApplyButton(True)
                txtMovieSetSortToken.Text = String.Empty
                txtMovieSetSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVSortTokenAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtTVSortToken.Text) Then
            If Not lstTVSortTokens.Items.Contains(txtTVSortToken.Text) Then
                lstTVSortTokens.Items.Add(txtTVSortToken.Text)
                Me.sResult.NeedsRefresh_TV = True
                Me.SetApplyButton(True)
                txtTVSortToken.Text = String.Empty
                txtTVSortToken.Focus()
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
        Me.SaveSettings(True)
        Me.SetApplyButton(False)
        If Me.sResult.NeedsUpdate OrElse Me.sResult.NeedsRefresh_Movie OrElse Me.sResult.NeedsRefresh_MovieSet OrElse Me.sResult.NeedsRefresh_TV Then Me.didApply = True
    End Sub

    Private Sub btnMovieBackdropsPathBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSourcesBackdropsFolderPathBrowse.Click
        With Me.fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(552, "Select the folder where you wish to store your backdrops...")
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtMovieSourcesBackdropsFolderPath.Text = .SelectedPath.ToString
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
                Me.sResult.NeedsRefresh_Movie = True
                Me.lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieGeneralCustomMarker1_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker1.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnMovieGeneralCustomMarker1.BackColor = .Color
                    Me.SetApplyButton(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker2_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker2.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnMovieGeneralCustomMarker2.BackColor = .Color
                    Me.SetApplyButton(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker3_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker3.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnMovieGeneralCustomMarker3.BackColor = .Color
                    Me.SetApplyButton(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker4_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker4.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnMovieGeneralCustomMarker4.BackColor = .Color
                    Me.SetApplyButton(True)
                End If
            End If
        End With
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
                Me.sResult.NeedsRefresh_TV = True
                Me.lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
                Me.sResult.NeedsRefresh_TV = True
                Me.lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieGeneralMediaListSortingUp.Click
        Try
            If Me.lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso Me.lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not Me.lvMovieGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = Me.MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    Me.MovieGeneralMediaListSorting.Remove(selItem)
                    Me.MovieGeneralMediaListSorting.Insert(iIndex - 1, selItem)

                    Me.RenumberMovieGeneralMediaListSorting()
                    Me.LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 3 < 0 Then
                        Me.lvMovieGeneralMediaListSorting.TopItem = Me.lvMovieGeneralMediaListSorting.Items(selIndex - 3)
                    End If
                    Me.lvMovieGeneralMediaListSorting.Items(selIndex - 1).Selected = True
                    Me.lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetGeneralMediaListSortingUp.Click
        Try
            If Me.lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso Me.lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not Me.lvMovieSetGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = Me.MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvMovieSetGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.MovieSetGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvMovieSetGeneralMediaListSorting.SelectedIndices(0)
                    Me.MovieSetGeneralMediaListSorting.Remove(selItem)
                    Me.MovieSetGeneralMediaListSorting.Insert(iIndex - 1, selItem)

                    Me.RenumberMovieSetGeneralMediaListSorting()
                    Me.LoadMovieSetGeneralMediaListSorting()

                    If Not selIndex - 3 < 0 Then
                        Me.lvMovieSetGeneralMediaListSorting.TopItem = Me.lvMovieSetGeneralMediaListSorting.Items(selIndex - 3)
                    End If
                    Me.lvMovieSetGeneralMediaListSorting.Items(selIndex - 1).Selected = True
                    Me.lvMovieSetGeneralMediaListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvMovieSetGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVGeneralEpisodeListSortingUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralEpisodeListSortingUp.Click
        Try
            If Me.lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 AndAlso Not Me.lvTVGeneralEpisodeListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = Me.TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVGeneralEpisodeListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.TVGeneralEpisodeListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVGeneralEpisodeListSorting.SelectedIndices(0)
                    Me.TVGeneralEpisodeListSorting.Remove(selItem)
                    Me.TVGeneralEpisodeListSorting.Insert(iIndex - 1, selItem)

                    Me.RenumberTVEpisodeGeneralMediaListSorting()
                    Me.LoadTVGeneralEpisodeListSorting()

                    If Not selIndex - 3 < 0 Then
                        Me.lvTVGeneralEpisodeListSorting.TopItem = Me.lvTVGeneralEpisodeListSorting.Items(selIndex - 3)
                    End If
                    Me.lvTVGeneralEpisodeListSorting.Items(selIndex - 1).Selected = True
                    Me.lvTVGeneralEpisodeListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVGeneralEpisodeListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVGeneralSeasonListSortingUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralSeasonListSortingUp.Click
        Try
            If Me.lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 AndAlso Not Me.lvTVGeneralSeasonListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = Me.TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVGeneralSeasonListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.TVGeneralSeasonListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVGeneralSeasonListSorting.SelectedIndices(0)
                    Me.TVGeneralSeasonListSorting.Remove(selItem)
                    Me.TVGeneralSeasonListSorting.Insert(iIndex - 1, selItem)

                    Me.RenumberTVSeasonGeneralMediaListSorting()
                    Me.LoadTVGeneralSeasonListSorting()

                    If Not selIndex - 3 < 0 Then
                        Me.lvTVGeneralSeasonListSorting.TopItem = Me.lvTVGeneralSeasonListSorting.Items(selIndex - 3)
                    End If
                    Me.lvTVGeneralSeasonListSorting.Items(selIndex - 1).Selected = True
                    Me.lvTVGeneralSeasonListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVGeneralSeasonListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVGeneralShowListSortingUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralShowListSortingUp.Click
        Try
            If Me.lvTVGeneralShowListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralShowListSorting.SelectedItems.Count > 0 AndAlso Not Me.lvTVGeneralShowListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = Me.TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralShowListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVGeneralShowListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.TVGeneralShowListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVGeneralShowListSorting.SelectedIndices(0)
                    Me.TVGeneralShowListSorting.Remove(selItem)
                    Me.TVGeneralShowListSorting.Insert(iIndex - 1, selItem)

                    Me.RenumberTVShowGeneralMediaListSorting()
                    Me.LoadTVGeneralShowListSorting()

                    If Not selIndex - 3 < 0 Then
                        Me.lvTVGeneralShowListSorting.TopItem = Me.lvTVGeneralShowListSorting.Items(selIndex - 3)
                    End If
                    Me.lvTVGeneralShowListSorting.Items(selIndex - 1).Selected = True
                    Me.lvTVGeneralShowListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVGeneralShowListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieGeneralMediaListSortingDown.Click
        Try
            If Me.lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso Me.lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Me.lvMovieGeneralMediaListSorting.SelectedItems(0).Index < (Me.lvMovieGeneralMediaListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = Me.MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    Me.MovieGeneralMediaListSorting.Remove(selItem)
                    Me.MovieGeneralMediaListSorting.Insert(iIndex + 1, selItem)

                    Me.RenumberMovieGeneralMediaListSorting()
                    Me.LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 2 < 0 Then
                        Me.lvMovieGeneralMediaListSorting.TopItem = Me.lvMovieGeneralMediaListSorting.Items(selIndex - 2)
                    End If
                    Me.lvMovieGeneralMediaListSorting.Items(selIndex + 1).Selected = True
                    Me.lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetGeneralMediaListSortingDown.Click
        Try
            If Me.lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso Me.lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Me.lvMovieSetGeneralMediaListSorting.SelectedItems(0).Index < (Me.lvMovieSetGeneralMediaListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = Me.MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvMovieSetGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.MovieSetGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvMovieSetGeneralMediaListSorting.SelectedIndices(0)
                    Me.MovieSetGeneralMediaListSorting.Remove(selItem)
                    Me.MovieSetGeneralMediaListSorting.Insert(iIndex + 1, selItem)

                    Me.RenumberMovieSetGeneralMediaListSorting()
                    Me.LoadMovieSetGeneralMediaListSorting()

                    If Not selIndex - 2 < 0 Then
                        Me.lvMovieSetGeneralMediaListSorting.TopItem = Me.lvMovieSetGeneralMediaListSorting.Items(selIndex - 2)
                    End If
                    Me.lvMovieSetGeneralMediaListSorting.Items(selIndex + 1).Selected = True
                    Me.lvMovieSetGeneralMediaListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvMovieSetGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVGeneralEpisodeListSortingDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralEpisodeListSortingDown.Click
        Try
            If Me.lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 AndAlso Me.lvTVGeneralEpisodeListSorting.SelectedItems(0).Index < (Me.lvTVGeneralEpisodeListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = Me.TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVGeneralEpisodeListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.TVGeneralEpisodeListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVGeneralEpisodeListSorting.SelectedIndices(0)
                    Me.TVGeneralEpisodeListSorting.Remove(selItem)
                    Me.TVGeneralEpisodeListSorting.Insert(iIndex + 1, selItem)

                    Me.RenumberTVEpisodeGeneralMediaListSorting()
                    Me.LoadTVGeneralEpisodeListSorting()

                    If Not selIndex - 2 < 0 Then
                        Me.lvTVGeneralEpisodeListSorting.TopItem = Me.lvTVGeneralEpisodeListSorting.Items(selIndex - 2)
                    End If
                    Me.lvTVGeneralEpisodeListSorting.Items(selIndex + 1).Selected = True
                    Me.lvTVGeneralEpisodeListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVGeneralEpisodeListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVGeneralSeasonListSortingDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralSeasonListSortingDown.Click
        Try
            If Me.lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 AndAlso Me.lvTVGeneralSeasonListSorting.SelectedItems(0).Index < (Me.lvTVGeneralSeasonListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = Me.TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVGeneralSeasonListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.TVGeneralSeasonListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVGeneralSeasonListSorting.SelectedIndices(0)
                    Me.TVGeneralSeasonListSorting.Remove(selItem)
                    Me.TVGeneralSeasonListSorting.Insert(iIndex + 1, selItem)

                    Me.RenumberTVSeasonGeneralMediaListSorting()
                    Me.LoadTVGeneralSeasonListSorting()

                    If Not selIndex - 2 < 0 Then
                        Me.lvTVGeneralSeasonListSorting.TopItem = Me.lvTVGeneralSeasonListSorting.Items(selIndex - 2)
                    End If
                    Me.lvTVGeneralSeasonListSorting.Items(selIndex + 1).Selected = True
                    Me.lvTVGeneralSeasonListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVGeneralSeasonListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVGeneralShowListSortingDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralShowListSortingDown.Click
        Try
            If Me.lvTVGeneralShowListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralShowListSorting.SelectedItems.Count > 0 AndAlso Me.lvTVGeneralShowListSorting.SelectedItems(0).Index < (Me.lvTVGeneralShowListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = Me.TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralShowListSorting.SelectedItems(0).Text))

                If Not IsNothing(selItem) Then
                    Me.lvTVGeneralShowListSorting.SuspendLayout()
                    Dim iIndex As Integer = Me.TVGeneralShowListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVGeneralShowListSorting.SelectedIndices(0)
                    Me.TVGeneralShowListSorting.Remove(selItem)
                    Me.TVGeneralShowListSorting.Insert(iIndex + 1, selItem)

                    Me.RenumberTVShowGeneralMediaListSorting()
                    Me.LoadTVGeneralShowListSorting()

                    If Not selIndex - 2 < 0 Then
                        Me.lvTVGeneralShowListSorting.TopItem = Me.lvTVGeneralShowListSorting.Items(selIndex - 2)
                    End If
                    Me.lvTVGeneralShowListSorting.Items(selIndex + 1).Selected = True
                    Me.lvTVGeneralShowListSorting.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVGeneralShowListSorting.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lvMovieGeneralMediaListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvMovieGeneralMediaListSorting.MouseDoubleClick
        If Me.lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso Me.lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = Me.MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

            If Not IsNothing(selItem) Then
                Me.lvMovieGeneralMediaListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = Me.lvMovieGeneralMediaListSorting.TopItem.Index
                Dim selIndex As Integer = Me.lvMovieGeneralMediaListSorting.SelectedIndices(0)

                Me.LoadMovieGeneralMediaListSorting()

                Me.lvMovieGeneralMediaListSorting.TopItem = Me.lvMovieGeneralMediaListSorting.Items(topIndex)
                Me.lvMovieGeneralMediaListSorting.Items(selIndex).Selected = True
                Me.lvMovieGeneralMediaListSorting.ResumeLayout()
            End If

            Me.SetApplyButton(True)
            Me.lvMovieGeneralMediaListSorting.Focus()
        End If
    End Sub

    Private Sub lvMovieSetGeneralMediaListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvMovieSetGeneralMediaListSorting.MouseDoubleClick
        If Me.lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso Me.lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = Me.MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

            If Not IsNothing(selItem) Then
                Me.lvMovieSetGeneralMediaListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = Me.lvMovieSetGeneralMediaListSorting.TopItem.Index
                Dim selIndex As Integer = Me.lvMovieSetGeneralMediaListSorting.SelectedIndices(0)

                Me.LoadMovieSetGeneralMediaListSorting()

                Me.lvMovieSetGeneralMediaListSorting.TopItem = Me.lvMovieSetGeneralMediaListSorting.Items(topIndex)
                Me.lvMovieSetGeneralMediaListSorting.Items(selIndex).Selected = True
                Me.lvMovieSetGeneralMediaListSorting.ResumeLayout()
            End If

            Me.SetApplyButton(True)
            Me.lvMovieSetGeneralMediaListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralEpisodeListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralEpisodeListSorting.MouseDoubleClick
        If Me.lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = Me.TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

            If Not IsNothing(selItem) Then
                Me.lvTVGeneralEpisodeListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = Me.lvTVGeneralEpisodeListSorting.TopItem.Index
                Dim selIndex As Integer = Me.lvTVGeneralEpisodeListSorting.SelectedIndices(0)

                Me.LoadTVGeneralEpisodeListSorting()

                Me.lvTVGeneralEpisodeListSorting.TopItem = Me.lvTVGeneralEpisodeListSorting.Items(topIndex)
                Me.lvTVGeneralEpisodeListSorting.Items(selIndex).Selected = True
                Me.lvTVGeneralEpisodeListSorting.ResumeLayout()
            End If

            Me.SetApplyButton(True)
            Me.lvTVGeneralEpisodeListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralSeasonListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralSeasonListSorting.MouseDoubleClick
        If Me.lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = Me.TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

            If Not IsNothing(selItem) Then
                Me.lvTVGeneralSeasonListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = Me.lvTVGeneralSeasonListSorting.TopItem.Index
                Dim selIndex As Integer = Me.lvTVGeneralSeasonListSorting.SelectedIndices(0)

                Me.LoadTVGeneralSeasonListSorting()

                Me.lvTVGeneralSeasonListSorting.TopItem = Me.lvTVGeneralSeasonListSorting.Items(topIndex)
                Me.lvTVGeneralSeasonListSorting.Items(selIndex).Selected = True
                Me.lvTVGeneralSeasonListSorting.ResumeLayout()
            End If

            Me.SetApplyButton(True)
            Me.lvTVGeneralSeasonListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralShowListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralShowListSorting.MouseDoubleClick
        If Me.lvTVGeneralShowListSorting.Items.Count > 0 AndAlso Me.lvTVGeneralShowListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = Me.TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvTVGeneralShowListSorting.SelectedItems(0).Text))

            If Not IsNothing(selItem) Then
                Me.lvTVGeneralShowListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = Me.lvTVGeneralShowListSorting.TopItem.Index
                Dim selIndex As Integer = Me.lvTVGeneralShowListSorting.SelectedIndices(0)

                Me.LoadTVGeneralShowListSorting()

                Me.lvTVGeneralShowListSorting.TopItem = Me.lvTVGeneralShowListSorting.Items(topIndex)
                Me.lvTVGeneralShowListSorting.Items(selIndex).Selected = True
                Me.lvTVGeneralShowListSorting.ResumeLayout()
            End If

            Me.SetApplyButton(True)
            Me.lvTVGeneralShowListSorting.Focus()
        End If
    End Sub

    Private Sub btnTVShowFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterReset.Click, btnMovieFilterReset.Click
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

    Private Sub btnMovieFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox(Master.eLang.GetString(842, "Are you sure you want to reset to the default list of movie filters?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieFilters, True)
            Me.RefreshMovieFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidExtsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidVideoExtsReset.Click
        If MsgBox(Master.eLang.GetString(843, "Are you sure you want to reset to the default list of valid video extensions?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidExts, True)
            Me.RefreshFileSystemValidExts()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidSubtitlesExtsReset.Click, btnFileSystemValidVideoExtsReset.Click
        If MsgBox(Master.eLang.GetString(1283, "Are you sure you want to reset to the default list of valid subtitle extensions?"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidSubtitleExts, True)
            Me.RefreshFileSystemValidSubtitlesExts()
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

    Private Sub btnMovieGeneralMediaListSortingReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieGeneralMediaListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieListSorting, True)
        Me.MovieGeneralMediaListSorting.Clear()
        Me.MovieGeneralMediaListSorting.AddRange(Master.eSettings.MovieGeneralMediaListSorting)
        Me.LoadMovieGeneralMediaListSorting()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetGeneralMediaListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieSetListSorting, True)
        Me.MovieSetGeneralMediaListSorting.Clear()
        Me.MovieSetGeneralMediaListSorting.AddRange(Master.eSettings.MovieSetGeneralMediaListSorting)
        Me.LoadMovieSetGeneralMediaListSorting()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVEpisodeGeneralMediaListSortingReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralEpisodeListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVEpisodeListSorting, True)
        Me.TVGeneralEpisodeListSorting.Clear()
        Me.TVGeneralEpisodeListSorting.AddRange(Master.eSettings.TVGeneralEpisodeListSorting)
        Me.LoadTVGeneralEpisodeListSorting()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVSeasonGeneralMediaListSortingReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralSeasonListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVSeasonListSorting, True)
        Me.TVGeneralSeasonListSorting.Clear()
        Me.TVGeneralSeasonListSorting.AddRange(Master.eSettings.TVGeneralSeasonListSorting)
        Me.LoadTVGeneralSeasonListSorting()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVGeneralShowListSortingReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralShowListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowListSorting, True)
        Me.TVGeneralShowListSorting.Clear()
        Me.TVGeneralShowListSorting.AddRange(Master.eSettings.TVGeneralShowListSorting)
        Me.LoadTVGeneralShowListSorting()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnFileSystemValidExtsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidVideoExtsRemove.Click
        Me.RemoveFileSystemValidExts()
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidSubtitlesExtsRemove.Click
        Me.RemoveFileSystemValidSubtitlesExts()
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

    Private Sub btnMovieSortTokenReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSortTokenReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieSortTokens, True)
        Me.RefreshMovieSortTokens()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnMovieSetSortTokenRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetSortTokenRemove.Click
        Me.RemoveMovieSetSortToken()
    End Sub

    Private Sub btnMovieSetSortTokenReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetSortTokenReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieSetSortTokens, True)
        Me.RefreshMovieSetSortTokens()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVSortTokenRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSortTokenRemove.Click
        Me.RemoveTVSortToken()
    End Sub

    Private Sub btnTVSortTokenReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSortTokenReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVSortTokens, True)
        Me.RefreshTVSortTokens()
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVGeneralLangFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralLangFetch.Click
        Master.eSettings.TVGeneralLanguages = ModulesManager.Instance.TVGetLangs("thetvdb.com")
        Me.cbTVGeneralLang.Items.Clear()
        Me.cbTVGeneralLang.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)

        If Me.cbTVGeneralLang.Items.Count > 0 Then
            Me.cbTVGeneralLang.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.TVGeneralLanguage).name
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
                Me.sResult.NeedsRefresh_TV = True
                Me.lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
                Me.sResult.NeedsRefresh_TV = True
                Me.lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
                Me.sResult.NeedsRefresh_Movie = True
                Me.lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cbGeneralDateTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGeneralDateTime.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieScraperCertLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieScraperCertLang.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieEFanartsPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieEFanartsPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbTVShowEFanartsPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVShowEFanartsPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieEThumbsPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieEThumbsPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieFanartPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieFanartPrefSize.SelectedIndexChanged
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

    Private Sub cbGeneralMovieSetTheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGeneralMovieSetTheme.SelectedIndexChanged
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

    Private Sub cbMovieSetBannerPrefType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieSetBannerPrefType.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieSetFanartPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieSetFanartPrefSize.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieSetPosterPrefSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieSetPosterPrefSize.SelectedIndexChanged
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

    Private Sub cbMovieTrailerMinQual_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTrailerMinVideoQual.SelectedIndexChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieTrailerPrefQual_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTrailerPrefVideoQual.SelectedIndexChanged
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

    Private Sub CheckHideSettings()
        If chkGeneralHideBanner.Checked AndAlso chkGeneralHideCharacterArt.Checked AndAlso chkGeneralHideClearArt.Checked AndAlso chkGeneralHideClearLogo.Checked AndAlso _
              chkGeneralHideDiscArt.Checked AndAlso chkGeneralHideFanart.Checked AndAlso chkGeneralHideFanartSmall.Checked AndAlso chkGeneralHideLandscape.Checked AndAlso chkGeneralHidePoster.Checked Then
            Me.chkGeneralImagesGlassOverlay.Enabled = False
            Me.chkGeneralShowImgDims.Enabled = False
            Me.chkGeneralShowImgNames.Enabled = False
        Else
            Me.chkGeneralImagesGlassOverlay.Enabled = True
            Me.chkGeneralShowImgDims.Enabled = True
            Me.chkGeneralShowImgNames.Enabled = True
        End If
    End Sub

    Private Sub chkMovieClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClickScrape.CheckedChanged
        chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieClickScrapeAsk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClickScrapeAsk.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieBackdropsAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSourcesBackdropsAuto.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCastWithImg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCastWithImg.CheckedChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkMovieScraperStudioWithImg_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperStudioWithImg.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperStudio.CheckedChanged
        Me.SetApplyButton(True)
        Me.chkMovieScraperStudioWithImg.Enabled = Me.chkMovieScraperStudio.Checked
        Me.txtMovieScraperStudioLimit.Enabled = Me.chkMovieScraperStudio.Checked
        If Not Me.chkMovieScraperStudio.Checked Then
            Me.chkMovieScraperStudioWithImg.Checked = False
            Me.txtMovieScraperStudioLimit.Text = "0"
        End If
    End Sub
    Private Sub chkMovieScraperCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCast.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperCastWithImg.Enabled = Me.chkMovieScraperCast.Checked
        Me.txtMovieScraperCastLimit.Enabled = Me.chkMovieScraperCast.Checked

        If Not chkMovieScraperCast.Checked Then
            Me.chkMovieScraperCastWithImg.Checked = False
            Me.txtMovieScraperCastLimit.Text = "0"
        End If
    End Sub

    Private Sub chkMovieScraperCert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCert.CheckedChanged
        Me.SetApplyButton(True)

        If Not Me.chkMovieScraperCert.Checked Then
            Me.cbMovieScraperCertLang.Enabled = False
            Me.cbMovieScraperCertLang.SelectedIndex = 0
            Me.chkMovieScraperCertForMPAA.Enabled = False
            Me.chkMovieScraperCertForMPAA.Checked = False
            Me.chkMovieScraperCertFSK.Enabled = False
            Me.chkMovieScraperCertFSK.Checked = False
            Me.chkMovieScraperCertOnlyValue.Enabled = False
            Me.chkMovieScraperCertOnlyValue.Checked = False
        Else
            Me.cbMovieScraperCertLang.Enabled = True
            Me.cbMovieScraperCertLang.SelectedIndex = 0
            Me.chkMovieScraperCertForMPAA.Enabled = True
            Me.chkMovieScraperCertFSK.Enabled = True
            Me.chkMovieScraperCertOnlyValue.Enabled = True
        End If
    End Sub

    Private Sub chkMovieScraperMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperMPAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieScraperMPAANotRated_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieScraperMPAANotRated.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCertForMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCertForMPAA.CheckedChanged
        Me.SetApplyButton(True)

        If Not Me.chkMovieScraperCertForMPAA.Checked Then
            Me.chkMovieScraperCertForMPAAFallback.Enabled = False
            Me.chkMovieScraperCertForMPAAFallback.Checked = False
        Else
            Me.chkMovieScraperCertForMPAAFallback.Enabled = True
        End If
    End Sub

    Private Sub chkMovieScraperCertForMPAAFallback_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCertForMPAAFallback.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCertOnlyValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCertOnlyValue.CheckedChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkMovieScraperCertFSK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCertFSK.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCleanFields_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCleanFields.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkIMDBCleanPlotOutline_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperCleanPlotOutline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperDetailView_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperDetailView.CheckedChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkMovieScraperReleaseFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperReleaseFormat.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperVotesSeperator_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperVotesSeperator.CheckedChanged
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

    Private Sub chkMovieSetCleanDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetCleanDB.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetCleanFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetCleanFiles.CheckedChanged
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

    Private Sub chkMovieScraperDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperDirector.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVDisplayMissingEpisodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVDisplayMissingEpisodes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieDisplayYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieDisplayYear.CheckedChanged
        Me.sResult.NeedsRefresh_Movie = True
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVDisplayStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVDisplayStatus.CheckedChanged
        Me.sResult.NeedsRefresh_TV = True
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
            Me.cbMovieTrailerMinVideoQual.Enabled = False
            Me.cbMovieTrailerPrefVideoQual.Enabled = False
        Else
            Me.cbMovieTrailerMinVideoQual.Enabled = True
            Me.cbMovieTrailerPrefVideoQual.Enabled = True
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

    Private Sub chkTVLockEpisodePlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodePlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockEpisodeRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodeRating.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockEpisodeRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodeRuntime.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockEpisodeTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodeTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVLockEpisodeVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockEpisodeVotes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVEpisodeProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh_TV = True
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

    Private Sub chkTVShowEFanartsPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowEFanartsPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieEThumbsPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieEThumbsPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieFanartPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieFanartPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
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

    Private Sub chkMovieLockCollectionID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockCollectionID.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockCollections_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockCollections.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockTagline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockOriginalTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLockTrailer.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockYear_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockYear.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockRuntime_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockRuntime.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTop250_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockTop250.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockStudio_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkMovieLockStudio.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockCountry_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockCountry.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockReleaseDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockReleaseDate.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockCertification_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockCert.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockMPAA_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockMPAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockVotes_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockVotes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockActors.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockDirector_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockDirector.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockCredits_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockCredits.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockTags_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockTags.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockLanguageA_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockLanguageA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieLockLanguageV_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieLockLanguageV.CheckedChanged
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

    Private Sub chkGeneralHideBanner_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideBanner.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHideCharacterArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideCharacterArt.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHideClearArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideClearArt.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHideClearLogo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideClearLogo.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHideDiscArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideDiscArt.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHideFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideFanart.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHideLandscape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideLandscape.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHidePoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHidePoster.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralHideFanartSmall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralHideFanartSmall.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
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

    Private Sub chkGeneralImagesGlassOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralImagesGlassOverlay.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperOutline.CheckedChanged
        Me.SetApplyButton(True)
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

    Private Sub chkGeneralDateAddedIgnoreNFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDateAddedIgnoreNFO.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralDoubleClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDoubleClickScrape.CheckedChanged
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

    Private Sub chkTVShowEFanartsOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowEFanartsOverwrite.CheckedChanged
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

    Private Sub chkMovieProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh_Movie = True
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

    Private Sub chkTVShowEFanartsResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowEFanartsResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVShowEFanartsWidth.Enabled = chkTVShowEFanartsResize.Checked
        txtTVShowEFanartsHeight.Enabled = chkTVShowEFanartsResize.Checked

        If Not chkTVShowEFanartsResize.Checked Then
            txtTVShowEFanartsWidth.Text = String.Empty
            txtTVShowEFanartsHeight.Text = String.Empty
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

    Private Sub chkMovieSetBannerResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetBannerResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieSetBannerWidth.Enabled = chkMovieSetBannerResize.Checked
        txtMovieSetBannerHeight.Enabled = chkMovieSetBannerResize.Checked

        If Not chkMovieSetBannerResize.Checked Then
            txtMovieSetBannerWidth.Text = String.Empty
            txtMovieSetBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetFanartResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetFanartResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieSetFanartWidth.Enabled = chkMovieSetFanartResize.Checked
        txtMovieSetFanartHeight.Enabled = chkMovieSetFanartResize.Checked

        If Not chkMovieSetFanartResize.Checked Then
            txtMovieSetFanartWidth.Text = String.Empty
            txtMovieSetFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetPosterResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetPosterResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieSetPosterWidth.Enabled = chkMovieSetPosterResize.Checked
        txtMovieSetPosterHeight.Enabled = chkMovieSetPosterResize.Checked

        If Not chkMovieSetPosterResize.Checked Then
            txtMovieSetPosterWidth.Text = String.Empty
            txtMovieSetPosterHeight.Text = String.Empty
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

    Private Sub chkTVScraperEpisodeRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeRuntime.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeSeason.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVScraperEpisodeVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperEpisodeVotes.CheckedChanged
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

    Private Sub txtTVScraperShowMPAANotRated_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVScraperShowMPAANotRated.TextChanged
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

    Private Sub chkTVScraperShowRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowRuntime.CheckedChanged
        Me.chkTVScraperUseSRuntimeForEp.Enabled = Me.chkTVScraperShowRuntime.Checked
        If Not Me.chkTVScraperShowRuntime.Checked Then
            Me.chkTVScraperUseSRuntimeForEp.Checked = False
        End If
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

    Private Sub chkTVScraperShowVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowVotes.CheckedChanged
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

    Private Sub chkGeneralShowLangFlags_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralShowLangFlags.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralShowImgDims_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralShowImgDims.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralShowImgNames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralShowImgNames.CheckedChanged
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

    Private Sub chkTVLockShowRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowRuntime.CheckedChanged
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

    Private Sub chkTVLockShowVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVLockShowVotes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsRefresh_TV = True
    End Sub

    Private Sub chkMovieSortBeforeScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSortBeforeScan.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkGeneralSourceFromFolder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralSourceFromFolder.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperTagline.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkMovieScraperOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperOriginalTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub chkMovieScraperTop250_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperTop250.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCollectionID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCollectionID.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperCollectionsAuto.Enabled = Me.chkMovieScraperCollectionID.Checked
        If Not Me.chkMovieScraperCollectionID.Checked Then
            Me.chkMovieScraperCollectionsAuto.Checked = False
        End If
    End Sub

    Private Sub chkMovieScraperCollectionsAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCollectionsAuto.CheckedChanged
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


    Private Sub chkMovieUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseBoxee.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieFanartBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMovieNFOBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMoviePosterBoxee.Enabled = Me.chkMovieUseBoxee.Checked

        If Not Me.chkMovieUseBoxee.Checked Then
            Me.chkMovieFanartBoxee.Checked = False
            Me.chkMovieNFOBoxee.Checked = False
            Me.chkMoviePosterBoxee.Checked = False
        Else
            Me.chkMovieFanartBoxee.Checked = True
            Me.chkMovieNFOBoxee.Checked = True
            Me.chkMoviePosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseFrodo.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieActorThumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieBannerAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearArtAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearLogoAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrafanartsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrathumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieDiscArtAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieFanartFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieLandscapeAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieNFOFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMoviePosterFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieTrailerFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseFrodo.Checked OrElse Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked

        If Not Me.chkMovieUseFrodo.Checked Then
            Me.chkMovieActorThumbsFrodo.Checked = False
            Me.chkMovieBannerAD.Checked = False
            Me.chkMovieClearArtAD.Checked = False
            Me.chkMovieClearLogoAD.Checked = False
            Me.chkMovieDiscArtAD.Checked = False
            Me.chkMovieExtrafanartsFrodo.Checked = False
            Me.chkMovieExtrathumbsFrodo.Checked = False
            Me.chkMovieFanartFrodo.Checked = False
            Me.chkMovieLandscapeAD.Checked = False
            Me.chkMovieNFOFrodo.Checked = False
            Me.chkMoviePosterFrodo.Checked = False
            Me.chkMovieTrailerFrodo.Checked = False
            Me.chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            Me.chkMovieActorThumbsFrodo.Checked = True
            Me.chkMovieBannerAD.Checked = True
            Me.chkMovieClearArtAD.Checked = True
            Me.chkMovieClearLogoAD.Checked = True
            Me.chkMovieDiscArtAD.Checked = True
            Me.chkMovieExtrafanartsFrodo.Checked = True
            Me.chkMovieExtrathumbsFrodo.Checked = True
            Me.chkMovieFanartFrodo.Checked = True
            Me.chkMovieLandscapeAD.Checked = True
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
        Me.chkMovieExtrafanartsEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieExtrathumbsEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieFanartEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieNFOEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMoviePosterEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieTrailerEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseEden.Checked OrElse Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Not Me.chkMovieUseEden.Checked AndAlso Me.chkMovieUseFrodo.Checked

        If Not Me.chkMovieUseEden.Checked Then
            Me.chkMovieActorThumbsEden.Checked = False
            Me.chkMovieExtrafanartsEden.Checked = False
            Me.chkMovieExtrathumbsEden.Checked = False
            Me.chkMovieFanartEden.Checked = False
            Me.chkMovieNFOEden.Checked = False
            Me.chkMoviePosterEden.Checked = False
            Me.chkMovieTrailerEden.Checked = False
        Else
            Me.chkMovieActorThumbsEden.Checked = True
            Me.chkMovieExtrafanartsEden.Checked = True
            Me.chkMovieExtrathumbsEden.Checked = True
            Me.chkMovieFanartEden.Checked = True
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

        Me.chkMovieBannerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieFanartYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieNFOYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMoviePosterYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieTrailerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieYAMJWatchedFile.Enabled = Me.chkMovieUseYAMJ.Checked

        If Not Me.chkMovieUseYAMJ.Checked Then
            Me.chkMovieBannerYAMJ.Checked = False
            Me.chkMovieFanartYAMJ.Checked = False
            Me.chkMovieNFOYAMJ.Checked = False
            Me.chkMoviePosterYAMJ.Checked = False
            Me.chkMovieTrailerYAMJ.Checked = False
            Me.chkMovieYAMJWatchedFile.Checked = False
        Else
            Me.chkMovieBannerYAMJ.Checked = True
            Me.chkMovieFanartYAMJ.Checked = True
            Me.chkMovieNFOYAMJ.Checked = True
            Me.chkMoviePosterYAMJ.Checked = True
            Me.chkMovieTrailerYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseNMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseNMJ.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieBannerNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieFanartNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieNFONMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMoviePosterNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieTrailerNMJ.Enabled = Me.chkMovieUseNMJ.Checked

        If Not Me.chkMovieUseNMJ.Checked Then
            Me.chkMovieBannerNMJ.Checked = False
            Me.chkMovieFanartNMJ.Checked = False
            Me.chkMovieNFONMJ.Checked = False
            Me.chkMoviePosterNMJ.Checked = False
            Me.chkMovieTrailerNMJ.Checked = False
        Else
            Me.chkMovieBannerNMJ.Checked = True
            Me.chkMovieFanartNMJ.Checked = True
            Me.chkMovieNFONMJ.Checked = True
            Me.chkMoviePosterNMJ.Checked = True
            Me.chkMovieTrailerNMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieSetUseMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetUseMSAA.CheckedChanged
        Me.SetApplyButton(True)

        Me.btnMovieSetPathMSAABrowse.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetBannerMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetClearArtMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetClearLogoMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetFanartMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetLandscapeMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetNFOMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetPosterMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.txtMovieSetPathMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked

        If Not Me.chkMovieSetUseMSAA.Checked Then
            Me.chkMovieSetBannerMSAA.Checked = False
            Me.chkMovieSetClearArtMSAA.Checked = False
            Me.chkMovieSetClearLogoMSAA.Checked = False
            Me.chkMovieSetFanartMSAA.Checked = False
            Me.chkMovieSetLandscapeMSAA.Checked = False
            Me.chkMovieSetNFOMSAA.Checked = False
            Me.chkMovieSetPosterMSAA.Checked = False
        Else
            Me.chkMovieSetBannerMSAA.Checked = True
            Me.chkMovieSetClearArtMSAA.Checked = True
            Me.chkMovieSetClearLogoMSAA.Checked = True
            Me.chkMovieSetFanartMSAA.Checked = True
            Me.chkMovieSetLandscapeMSAA.Checked = True
            Me.chkMovieSetNFOMSAA.Checked = True
            Me.chkMovieSetPosterMSAA.Checked = True
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

    Private Sub chkTVScraperUseSRuntimeForEp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperUseSRuntimeForEp.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperVotes.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkFileSystemCleanerWhitelist_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileSystemCleanerWhitelist.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieScraperCredits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCredits.CheckedChanged
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

    Private Sub txtMovieXBMCThemeSubDir_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieXBMCThemeSubDir.TextChanged, txtTVXBMCThemeSubDir.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieXBMCTrailerFormat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperXBMCTrailerFormat.CheckedChanged
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
        Me.chkTVEpisodeAired.Checked = False
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
                Me.cbTVSeasonRetrieve.SelectedValue = Settings.EpRetrieve.FromDirectory
            Case "File"
                Me.cbTVSeasonRetrieve.SelectedValue = Settings.EpRetrieve.FromFilename
        End Select

        Me.txtTVEpisodeRegex.Text = lItem.SubItems(3).Text

        Select Case lItem.SubItems(4).Text
            Case "Folder"
                Me.cbTVEpisodeRetrieve.SelectedValue = Settings.EpRetrieve.FromDirectory
            Case "File"
                Me.cbTVEpisodeRetrieve.SelectedValue = Settings.EpRetrieve.FromFilename
            Case "Result"
                Me.cbTVEpisodeRetrieve.SelectedValue = Settings.EpRetrieve.FromSeasonResult
        End Select

        Select Case lItem.SubItems(5).Text
            Case "Yes"
                Me.chkTVEpisodeAired.Checked = True
            Case "No"
                Me.chkTVEpisodeAired.Checked = False
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

    Private Sub FillMovieSetScraperTitleRenamer()
        For Each sett As AdvancedSettingsSetting In clsAdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
            Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {sett.Name.Substring(21), sett.Value})
            If Not sett.DefaultValue = String.Empty Then
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).ReadOnly = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).Style.SelectionForeColor = Drawing.Color.Red
            Else
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
            End If
        Next
        dgvMovieSetScraperTitleRenamer.ClearSelection()
    End Sub

    Private Sub SaveMovieSetScraperTitleRenamer()
        Dim deleteitem As New List(Of String)
        For Each sett As AdvancedSettingsSetting In clsAdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
            deleteitem.Add(sett.Name)
        Next

        Using settings = New clsAdvancedSettings()
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
        With Master.eSettings
            Me.chkGeneralResumeScraper.Checked = .RestartScraper
            Me.btnMovieGeneralCustomMarker1.BackColor = System.Drawing.Color.FromArgb(.MovieGeneralCustomMarker1Color)
            Me.btnMovieGeneralCustomMarker2.BackColor = System.Drawing.Color.FromArgb(.MovieGeneralCustomMarker2Color)
            Me.btnMovieGeneralCustomMarker3.BackColor = System.Drawing.Color.FromArgb(.MovieGeneralCustomMarker3Color)
            Me.btnMovieGeneralCustomMarker4.BackColor = System.Drawing.Color.FromArgb(.MovieGeneralCustomMarker4Color)
            Me.cbGeneralDaemonDrive.SelectedItem = .GeneralDaemonDrive
            Me.cbGeneralDateTime.SelectedIndex = .GeneralDateTime
            Me.cbGeneralLanguage.SelectedItem = .GeneralLanguage
            Me.cbGeneralMovieTheme.SelectedItem = .GeneralMovieTheme
            Me.cbGeneralMovieSetTheme.SelectedItem = .GeneralMovieSetTheme
            Me.cbGeneralTVEpisodeTheme.SelectedItem = .GeneralTVEpisodeTheme
            Me.cbGeneralTVShowTheme.SelectedItem = .GeneralTVShowTheme
            Me.cbMovieBannerPrefType.SelectedValue = .MovieBannerPrefType
            Me.cbMovieEFanartsPrefSize.SelectedValue = .MovieEFanartsPrefSize
            Me.cbMovieEThumbsPrefSize.SelectedValue = .MovieEThumbsPrefSize
            Me.cbMovieFanartPrefSize.SelectedValue = .MovieFanartPrefSize
            Me.cbMovieLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.MovieGeneralFlagLang), Master.eLang.Disabled, .MovieGeneralFlagLang)
            Me.cbMoviePosterPrefSize.SelectedValue = .MoviePosterPrefSize
            Me.cbMovieSetBannerPrefType.SelectedValue = .MovieSetBannerPrefType
            Me.cbMovieSetFanartPrefSize.SelectedValue = .MovieSetFanartPrefSize
            Me.cbMovieSetPosterPrefSize.SelectedValue = .MovieSetPosterPrefSize
            Me.cbMovieTrailerMinVideoQual.SelectedValue = .MovieTrailerMinVideoQual
            Me.cbMovieTrailerPrefVideoQual.SelectedValue = .MovieTrailerPrefVideoQual
            Me.cbTVASBannerPrefType.SelectedValue = .TVASBannerPrefType
            Me.cbTVASFanartPrefSize.SelectedValue = .TVASFanartPrefSize
            Me.cbTVASPosterPrefSize.SelectedValue = .TVASPosterPrefSize
            Me.cbTVEpisodeFanartPrefSize.SelectedValue = .TVEpisodeFanartPrefSize
            Me.cbTVLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.TVGeneralFlagLang), Master.eLang.Disabled, .TVGeneralFlagLang)
            Me.cbTVScraperOptionsOrdering.SelectedValue = .TVScraperOptionsOrdering
            Me.cbTVScraperRatingRegion.Text = .TVScraperRatingRegion
            Me.cbTVScraperUpdateTime.SelectedValue = .TVScraperUpdateTime
            Me.cbTVSeasonBannerPrefType.SelectedValue = .TVSeasonBannerPrefType
            Me.cbTVSeasonFanartPrefSize.SelectedValue = .TVSeasonFanartPrefSize
            Me.cbTVSeasonPosterPrefSize.SelectedValue = .TVSeasonPosterPrefSize
            Me.cbTVShowBannerPrefType.SelectedValue = .TVShowBannerPrefType
            Me.cbTVShowEFanartsPrefSize.SelectedValue = .TVShowEFanartsPrefSize
            Me.cbTVShowFanartPrefSize.SelectedValue = .TVShowFanartPrefSize
            Me.cbTVShowPosterPrefSize.SelectedValue = .TVShowPosterPrefSize
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
            Me.chkGeneralDateAddedIgnoreNFO.Checked = .GeneralDateAddedIgnoreNFO
            Me.chkGeneralDoubleClickScrape.Checked = .GeneralDoubleClickScrape
            Me.chkGeneralHideBanner.Checked = .GeneralHideBanner
            Me.chkGeneralHideCharacterArt.Checked = .GeneralHideCharacterArt
            Me.chkGeneralHideClearArt.Checked = .GeneralHideClearArt
            Me.chkGeneralHideClearLogo.Checked = .GeneralHideClearLogo
            Me.chkGeneralHideDiscArt.Checked = .GeneralHideDiscArt
            Me.chkGeneralHideFanart.Checked = .GeneralHideFanart
            Me.chkGeneralHideFanartSmall.Checked = .GeneralHideFanartSmall
            Me.chkGeneralHideLandscape.Checked = .GeneralHideLandscape
            Me.chkGeneralHidePoster.Checked = .GeneralHidePoster
            Me.chkGeneralImagesGlassOverlay.Checked = .GeneralImagesGlassOverlay
            Me.chkGeneralOverwriteNfo.Checked = .GeneralOverwriteNfo
            Me.chkGeneralShowGenresText.Checked = .GeneralShowGenresText
            Me.chkGeneralShowLangFlags.Checked = .GeneralShowLangFlags
            Me.chkGeneralShowImgDims.Checked = .GeneralShowImgDims
            Me.chkGeneralShowImgNames.Checked = .GeneralShowImgNames
            Me.chkGeneralSourceFromFolder.Checked = .GeneralSourceFromFolder
            Me.chkMovieActorThumbsOverwrite.Checked = .MovieActorThumbsOverwrite
            Me.chkMovieSourcesBackdropsAuto.Checked = .MovieBackdropsAuto
            Me.chkMovieBannerOverwrite.Checked = .MovieBannerOverwrite
            Me.chkMovieBannerPrefOnly.Checked = .MovieBannerPrefOnly
            Me.chkMovieBannerResize.Checked = .MovieBannerResize
            If .MovieBannerResize Then
                Me.txtMovieBannerHeight.Text = .MovieBannerHeight.ToString
                Me.txtMovieBannerWidth.Text = .MovieBannerWidth.ToString
            End If
            Me.chkMovieCleanDB.Checked = .MovieCleanDB
            Me.chkMovieClearArtOverwrite.Checked = .MovieClearArtOverwrite
            Me.chkMovieClearLogoOverwrite.Checked = .MovieClearLogoOverwrite
            Me.chkMovieClickScrape.Checked = .MovieClickScrape
            Me.chkMovieClickScrapeAsk.Checked = .MovieClickScrapeAsk
            Me.chkMovieDiscArtOverwrite.Checked = .MovieDiscArtOverwrite
            Me.chkMovieDisplayYear.Checked = .MovieDisplayYear
            Me.chkMovieEFanartsOverwrite.Checked = .MovieEFanartsOverwrite
            Me.chkMovieEFanartsPrefOnly.Checked = .MovieEFanartsPrefOnly
            Me.chkMovieEFanartsResize.Checked = .MovieEFanartsResize
            If .MovieEFanartsResize Then
                Me.txtMovieEFanartsHeight.Text = .MovieEFanartsHeight.ToString
                Me.txtMovieEFanartsWidth.Text = .MovieEFanartsWidth.ToString
            End If
            Me.chkMovieEThumbsOverwrite.Checked = .MovieEThumbsOverwrite
            Me.chkMovieEThumbsPrefOnly.Checked = .MovieEThumbsPrefOnly
            Me.chkMovieEThumbsResize.Checked = .MovieEThumbsResize
            If .MovieEThumbsResize Then
                Me.txtMovieEThumbsHeight.Text = .MovieEThumbsHeight.ToString
                Me.txtMovieEThumbsWidth.Text = .MovieEThumbsWidth.ToString
            End If
            Me.chkMovieFanartOverwrite.Checked = .MovieFanartOverwrite
            Me.chkMovieFanartPrefOnly.Checked = .MovieFanartPrefOnly
            Me.chkMovieFanartResize.Checked = .MovieFanartResize
            If .MovieFanartResize Then
                Me.txtMovieFanartHeight.Text = .MovieFanartHeight.ToString
                Me.txtMovieFanartWidth.Text = .MovieFanartWidth.ToString
            End If
            Me.chkMovieGeneralIgnoreLastScan.Checked = .MovieGeneralIgnoreLastScan
            Me.chkMovieGeneralMarkNew.Checked = .MovieGeneralMarkNew
            Me.chkMovieLandscapeOverwrite.Checked = .MovieLandscapeOverwrite
            Me.chkMovieLockActors.Checked = .MovieLockActors
            Me.chkMovieLockCountry.Checked = .MovieLockCountry
            Me.chkMovieLockCollectionID.Checked = .MovieLockCollectionID
            Me.chkMovieLockCollections.Checked = .MovieLockCollections
            Me.chkMovieLockDirector.Checked = .MovieLockDirector
            Me.chkMovieLockGenre.Checked = .MovieLockGenre
            Me.chkMovieLockLanguageA.Checked = .MovieLockLanguageA
            Me.chkMovieLockLanguageV.Checked = .MovieLockLanguageV
            Me.chkMovieLockCert.Checked = .MovieLockCert
            Me.chkMovieLockMPAA.Checked = .MovieLockMPAA
            Me.chkMovieLockOriginalTitle.Checked = .MovieLockOriginalTitle
            Me.chkMovieLockOutline.Checked = .MovieLockOutline
            Me.chkMovieLockPlot.Checked = .MovieLockPlot
            Me.chkMovieLockRating.Checked = .MovieLockRating
            Me.chkMovieLockReleaseDate.Checked = .MovieLockReleaseDate
            Me.chkMovieLockRuntime.Checked = .MovieLockRuntime
            Me.chkMovieLockStudio.Checked = .MovieLockStudio
            Me.chkMovieLockTagline.Checked = .MovieLockTagline
            Me.chkMovieLockTags.Checked = .MovieLockTags
            Me.chkMovieLockTitle.Checked = .MovieLockTitle
            Me.chkMovieLockTop250.Checked = .MovieLockTop250
            Me.chkMovieLockTrailer.Checked = .MovieLockTrailer
            Me.chkMovieLockVotes.Checked = .MovieLockVotes
            Me.chkMovieLockCredits.Checked = .MovieLockCredits
            Me.chkMovieLockYear.Checked = .MovieLockYear
            Me.chkMovieNoSaveImagesToNfo.Checked = .MovieNoSaveImagesToNfo
            Me.chkMoviePosterOverwrite.Checked = .MoviePosterOverwrite
            Me.chkMoviePosterPrefOnly.Checked = .MoviePosterPrefOnly
            Me.chkMoviePosterResize.Checked = .MoviePosterResize
            If .MoviePosterResize Then
                Me.txtMoviePosterHeight.Text = .MoviePosterHeight.ToString
                Me.txtMoviePosterWidth.Text = .MoviePosterWidth.ToString
            End If
            Me.chkMovieProperCase.Checked = .MovieProperCase
            Me.chkMovieSetBannerOverwrite.Checked = .MovieSetBannerOverwrite
            Me.chkMovieSetBannerPrefOnly.Checked = .MovieSetBannerPrefOnly
            Me.chkMovieSetBannerResize.Checked = .MovieSetBannerResize
            If .MovieSetBannerResize Then
                Me.txtMovieSetBannerHeight.Text = .MovieSetBannerHeight.ToString
                Me.txtMovieSetBannerWidth.Text = .MovieSetBannerWidth.ToString
            End If
            Me.chkMovieSetCleanDB.Checked = .MovieSetCleanDB
            Me.chkMovieSetCleanFiles.Checked = .MovieSetCleanFiles
            Me.chkMovieSetClearArtOverwrite.Checked = .MovieSetClearArtOverwrite
            Me.chkMovieSetClearLogoOverwrite.Checked = .MovieSetClearLogoOverwrite
            Me.chkMovieSetClickScrape.Checked = .MovieSetClickScrape
            Me.chkMovieSetClickScrapeAsk.Checked = .MovieSetClickScrapeAsk
            Me.chkMovieSetFanartOverwrite.Checked = .MovieSetFanartOverwrite
            Me.chkMovieSetFanartPrefOnly.Checked = .MovieSetFanartPrefOnly
            Me.chkMovieSetFanartResize.Checked = .MovieSetFanartResize
            If .MovieSetFanartResize Then
                Me.txtMovieSetFanartHeight.Text = .MovieSetFanartHeight.ToString
                Me.txtMovieSetFanartWidth.Text = .MovieSetFanartWidth.ToString
            End If
            Me.chkMovieSetGeneralMarkNew.Checked = .MovieSetGeneralMarkNew
            Me.chkMovieSetLandscapeOverwrite.Checked = .MovieSetLandscapeOverwrite
            Me.chkMovieSetLockPlot.Checked = .MovieSetLockPlot
            Me.chkMovieSetLockTitle.Checked = .MovieSetLockTitle
            Me.chkMovieSetPosterOverwrite.Checked = .MovieSetPosterOverwrite
            Me.chkMovieSetPosterPrefOnly.Checked = .MovieSetPosterPrefOnly
            Me.chkMovieSetPosterResize.Checked = .MovieSetPosterResize
            If .MovieSetPosterResize Then
                Me.txtMovieSetPosterHeight.Text = .MovieSetPosterHeight.ToString
                Me.txtMovieSetPosterWidth.Text = .MovieSetPosterWidth.ToString
            End If
            Me.chkMovieSetScraperPlot.Checked = .MovieSetScraperPlot
            Me.chkMovieSetScraperTitle.Checked = .MovieSetScraperTitle
            Me.chkMovieScanOrderModify.Checked = .MovieScanOrderModify
            Me.chkMovieScraperCast.Checked = .MovieScraperCast
            Me.chkMovieScraperCastWithImg.Checked = .MovieScraperCastWithImgOnly
            Me.chkMovieScraperCert.Checked = .MovieScraperCert
            Me.chkMovieScraperCertForMPAA.Checked = .MovieScraperCertForMPAA
            Me.chkMovieScraperCertForMPAAFallback.Checked = .MovieScraperCertForMPAAFallback
            Me.chkMovieScraperCertFSK.Checked = .MovieScraperCertFSK
            Me.chkMovieScraperCertOnlyValue.Checked = .MovieScraperCertOnlyValue
            Me.chkMovieScraperCleanFields.Checked = .MovieScraperCleanFields
            Me.chkMovieScraperCleanPlotOutline.Checked = .MovieScraperCleanPlotOutline
            Me.chkMovieScraperCollectionID.Checked = .MovieScraperCollectionID
            Me.chkMovieScraperCollectionsAuto.Checked = .MovieScraperCollectionsAuto
            Me.chkMovieScraperCountry.Checked = .MovieScraperCountry
            Me.chkMovieScraperDirector.Checked = .MovieScraperDirector
            Me.chkMovieScraperGenre.Checked = .MovieScraperGenre
            Me.chkMovieScraperMetaDataIFOScan.Checked = .MovieScraperMetaDataIFOScan
            Me.chkMovieScraperMetaDataScan.Checked = .MovieScraperMetaDataScan
            Me.chkMovieScraperMPAA.Checked = .MovieScraperMPAA
            Me.txtMovieScraperMPAANotRated.Text = .MovieScraperMPAANotRated
            Me.chkMovieScraperOriginalTitle.Checked = .MovieScraperOriginalTitle
            Me.chkMovieScraperDetailView.Checked = .MovieScraperUseDetailView
            Me.chkMovieScraperOutline.Checked = .MovieScraperOutline
            Me.chkMovieScraperPlot.Checked = .MovieScraperPlot
            Me.chkMovieScraperPlotForOutline.Checked = .MovieScraperPlotForOutline
            Me.chkMovieScraperRating.Checked = .MovieScraperRating
            Me.chkMovieScraperRelease.Checked = .MovieScraperRelease
            Me.chkMovieScraperReleaseFormat.Checked = .MovieScraperReleaseFormat
            Me.chkMovieScraperVotesSeperator.Checked = .MovieScraperVotesSeparator
            Me.chkMovieScraperRuntime.Checked = .MovieScraperRuntime
            Me.chkMovieScraperStudio.Checked = .MovieScraperStudio
            Me.chkMovieScraperStudioWithImg.Checked = .MovieScraperStudioWithImgOnly
            Me.chkMovieScraperTagline.Checked = .MovieScraperTagline
            Me.chkMovieScraperTitle.Checked = .MovieScraperTitle
            Me.chkMovieScraperTop250.Checked = .MovieScraperTop250
            Me.chkMovieScraperTrailer.Checked = .MovieScraperTrailer
            Me.chkMovieScraperUseMDDuration.Checked = .MovieScraperUseMDDuration
            Me.chkMovieScraperVotes.Checked = .MovieScraperVotes
            Me.chkMovieScraperCredits.Checked = .MovieScraperCredits
            Me.chkMovieScraperXBMCTrailerFormat.Checked = .MovieScraperXBMCTrailerFormat
            Me.chkMovieScraperYear.Checked = .MovieScraperYear
            Me.chkMovieSkipStackedSizeCheck.Checked = .MovieSkipStackedSizeCheck
            Me.chkMovieSortBeforeScan.Checked = .MovieSortBeforeScan
            Me.chkMovieThemeEnable.Checked = .MovieThemeEnable
            Me.chkMovieThemeOverwrite.Checked = .MovieThemeOverwrite
            Me.chkMovieTrailerDeleteExisting.Checked = .MovieTrailerDeleteExisting
            Me.chkMovieTrailerEnable.Checked = .MovieTrailerEnable
            Me.chkMovieTrailerOverwrite.Checked = .MovieTrailerOverwrite
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
            Me.chkTVDisplayStatus.Checked = .TVDisplayStatus
            Me.chkTVEpisodeFanartOverwrite.Checked = .TVEpisodeFanartOverwrite
            Me.chkTVEpisodeFanartResize.Checked = .TVEpisodeFanartResize
            If .TVEpisodeFanartResize Then
                Me.txtTVEpisodeFanartHeight.Text = .TVEpisodeFanartHeight.ToString
                Me.txtTVEpisodeFanartWidth.Text = .TVEpisodeFanartWidth.ToString
            End If
            Me.chkTVEpisodeNoFilter.Checked = .TVEpisodeNoFilter
            Me.chkTVEpisodePosterOverwrite.Checked = .TVEpisodePosterOverwrite
            Me.chkTVEpisodePosterResize.Checked = .TVEpisodePosterResize
            If .TVEpisodePosterResize Then
                Me.txtTVEpisodePosterHeight.Text = .TVEpisodePosterHeight.ToString
                Me.txtTVEpisodePosterWidth.Text = .TVEpisodePosterWidth.ToString
            End If
            Me.chkTVEpisodeProperCase.Checked = .TVEpisodeProperCase
            Me.chkTVGeneralMarkNewEpisodes.Checked = .TVGeneralMarkNewEpisodes
            Me.chkTVGeneralMarkNewShows.Checked = .TVGeneralMarkNewShows
            Me.chkTVGeneralIgnoreLastScan.Checked = .TVGeneralIgnoreLastScan
            Me.chkTVLockEpisodePlot.Checked = .TVLockEpisodePlot
            Me.chkTVLockEpisodeRating.Checked = .TVLockEpisodeRating
            Me.chkTVLockEpisodeRuntime.Checked = .TVLockEpisodeRuntime
            Me.chkTVLockEpisodeTitle.Checked = .TVLockEpisodeTitle
            Me.chkTVLockEpisodeVotes.Checked = .TVLockEpisodeVotes
            Me.chkTVLockShowGenre.Checked = .TVLockShowGenre
            Me.chkTVLockShowPlot.Checked = .TVLockShowPlot
            Me.chkTVLockShowRating.Checked = .TVLockShowRating
            Me.chkTVLockShowRuntime.Checked = .TVLockShowRuntime
            Me.chkTVLockShowStatus.Checked = .TVLockShowStatus
            Me.chkTVLockShowStudio.Checked = .TVLockShowStudio
            Me.chkTVLockShowTitle.Checked = .TVLockShowTitle
            Me.chkTVLockShowVotes.Checked = .TVLockShowVotes
            Me.chkTVScanOrderModify.Checked = .TVScanOrderModify
            Me.chkTVScraperEpisodeActors.Checked = .TVScraperEpisodeActors
            Me.chkTVScraperEpisodeAired.Checked = .TVScraperEpisodeAired
            Me.chkTVScraperEpisodeCredits.Checked = .TVScraperEpisodeCredits
            Me.chkTVScraperEpisodeDirector.Checked = .TVScraperEpisodeDirector
            Me.chkTVScraperEpisodeEpisode.Checked = .TVScraperEpisodeEpisode
            Me.chkTVScraperEpisodePlot.Checked = .TVScraperEpisodePlot
            Me.chkTVScraperEpisodeRating.Checked = .TVScraperEpisodeRating
            Me.chkTVScraperEpisodeRuntime.Checked = .TVScraperEpisodeRuntime
            Me.chkTVScraperEpisodeSeason.Checked = .TVScraperEpisodeSeason
            Me.chkTVScraperEpisodeTitle.Checked = .TVScraperEpisodeTitle
            Me.chkTVScraperEpisodeVotes.Checked = .TVScraperEpisodeVotes
            Me.chkTVScraperMetaDataScan.Checked = .TVScraperMetaDataScan
            Me.chkTVScraperShowActors.Checked = .TVScraperShowActors
            Me.chkTVScraperShowEpiGuideURL.Checked = .TVScraperShowEpiGuideURL
            Me.chkTVScraperShowGenre.Checked = .TVScraperShowGenre
            Me.chkTVScraperShowMPAA.Checked = .TVScraperShowMPAA
            Me.txtTVScraperShowMPAANotRated.Text = .TVScraperShowMPAANotRated
            Me.chkTVScraperShowPlot.Checked = .TVScraperShowPlot
            Me.chkTVScraperShowPremiered.Checked = .TVScraperShowPremiered
            Me.chkTVScraperShowRating.Checked = .TVScraperShowRating
            Me.chkTVScraperShowRuntime.Checked = .TVScraperShowRuntime
            Me.chkTVScraperShowStatus.Checked = .TVScraperShowStatus
            Me.chkTVScraperShowStudio.Checked = .TVScraperShowStudio
            Me.chkTVScraperShowTitle.Checked = .TVScraperShowTitle
            Me.chkTVScraperShowVotes.Checked = .TVScraperShowVotes
            Me.chkTVScraperUseMDDuration.Checked = .TVScraperUseMDDuration
            Me.chkTVScraperUseSRuntimeForEp.Checked = .TVScraperUseSRuntimeForEp
            Me.chkTVSeasonBannerOverwrite.Checked = .TVSeasonBannerOverwrite
            Me.chkTVSeasonBannerResize.Checked = .TVSeasonBannerResize
            If .TVSeasonBannerResize Then
                Me.txtTVSeasonBannerHeight.Text = .TVSeasonBannerHeight.ToString
                Me.txtTVSeasonBannerWidth.Text = .TVSeasonBannerWidth.ToString
            End If
            Me.chkTVShowEFanartsOverwrite.Checked = .TVShowEFanartsOverwrite
            Me.chkTVShowEFanartsPrefOnly.Checked = .TVShowEFanartsPrefOnly
            Me.chkTVShowEFanartsResize.Checked = .TVShowEFanartsResize
            If .TVShowEFanartsResize Then
                Me.txtTVShowEFanartsHeight.Text = .TVShowEFanartsHeight.ToString
                Me.txtTVShowEFanartsWidth.Text = .TVShowEFanartsWidth.ToString
            End If
            Me.chkTVSeasonFanartOverwrite.Checked = .TVSeasonFanartOverwrite
            Me.chkTVSeasonFanartResize.Checked = .TVSeasonFanartResize
            If .TVSeasonFanartResize Then
                Me.txtTVSeasonFanartHeight.Text = .TVSeasonFanartHeight.ToString
                Me.txtTVSeasonFanartWidth.Text = .TVSeasonFanartWidth.ToString
            End If
            Me.chkTVSeasonLandscapeOverwrite.Checked = .TVSeasonLandscapeOverwrite
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
            Me.chkTVShowFanartOverwrite.Checked = .TVShowFanartOverwrite
            Me.chkTVShowFanartResize.Checked = .TVShowFanartResize
            If .TVShowFanartResize Then
                Me.txtTVShowFanartHeight.Text = .TVShowFanartHeight.ToString
                Me.txtTVShowFanartWidth.Text = .TVShowFanartWidth.ToString
            End If
            Me.chkTVShowLandscapeOverwrite.Checked = .TVShowLandscapeOverwrite
            Me.chkTVShowPosterOverwrite.Checked = .TVShowPosterOverwrite
            Me.chkTVShowPosterResize.Checked = .TVShowPosterResize
            If .TVShowPosterResize Then
                Me.txtTVShowPosterHeight.Text = .TVShowPosterHeight.ToString
                Me.txtTVShowPosterWidth.Text = .TVShowPosterWidth.ToString
            End If
            Me.chkTVShowProperCase.Checked = .TVShowProperCase
            Me.lstFileSystemCleanerWhitelist.Items.AddRange(.FileSystemCleanerWhitelistExts.ToArray)
            Me.lstFileSystemNoStackExts.Items.AddRange(.FileSystemNoStackExts.ToArray)
            Me.tcFileSystemCleaner.SelectedTab = If(.FileSystemExpertCleaner, Me.tpFileSystemCleanerExpert, Me.tpFileSystemCleanerStandard)
            Me.txtGeneralDaemonPath.Text = .GeneralDaemonPath.ToString
            Me.txtMovieSourcesBackdropsFolderPath.Text = .MovieBackdropsPath.ToString
            Me.txtMovieEFanartsLimit.Text = .MovieEFanartsLimit.ToString
            Me.txtMovieEThumbsLimit.Text = .MovieEThumbsLimit.ToString
            Me.txtMovieGeneralCustomMarker1.Text = .MovieGeneralCustomMarker1Name.ToString
            Me.txtMovieGeneralCustomMarker2.Text = .MovieGeneralCustomMarker2Name.ToString
            Me.txtMovieGeneralCustomMarker3.Text = .MovieGeneralCustomMarker3Name.ToString
            Me.txtMovieGeneralCustomMarker4.Text = .MovieGeneralCustomMarker4Name.ToString
            Me.txtMovieIMDBURL.Text = .MovieIMDBURL.ToString
            Me.txtMovieScraperCastLimit.Text = .MovieScraperCastLimit.ToString
            Me.txtMovieScraperDurationRuntimeFormat.Text = .MovieScraperDurationRuntimeFormat
            Me.txtMovieScraperGenreLimit.Text = .MovieScraperGenreLimit.ToString
            Me.txtMovieScraperOutlineLimit.Text = .MovieScraperOutlineLimit.ToString
            Me.txtMovieScraperStudioLimit.Text = .MovieScraperStudioLimit.ToString
            Me.txtMovieSkipLessThan.Text = .MovieSkipLessThan.ToString
            Me.txtMovieTrailerDefaultSearch.Text = .MovieTrailerDefaultSearch.ToString
            Me.txtTVScraperDurationRuntimeFormat.Text = .TVScraperDurationRuntimeFormat.ToString
            Me.txtTVShowEFanartsLimit.Text = .TVShowEFanartsLimit.ToString
            Me.txtTVSkipLessThan.Text = .TVSkipLessThan.ToString

            FillGenres()
            FillMovieSetScraperTitleRenamer()

            If .MovieLevTolerance > 0 Then
                Me.chkMovieLevTolerance.Checked = True
                Me.txtMovieLevTolerance.Enabled = True
                Me.txtMovieLevTolerance.Text = .MovieLevTolerance.ToString
            End If

            Me.MovieMeta.AddRange(.MovieMetadataPerFileType)
            Me.LoadMovieMetadata()

            Me.MovieGeneralMediaListSorting.AddRange(.MovieGeneralMediaListSorting)
            Me.LoadMovieGeneralMediaListSorting()

            Me.MovieSetGeneralMediaListSorting.AddRange(.MovieSetGeneralMediaListSorting)
            Me.LoadMovieSetGeneralMediaListSorting()

            Me.TVGeneralEpisodeListSorting.AddRange(.TVGeneralEpisodeListSorting)
            Me.LoadTVGeneralEpisodeListSorting()

            Me.TVGeneralSeasonListSorting.AddRange(.TVGeneralSeasonListSorting)
            Me.LoadTVGeneralSeasonListSorting()

            Me.TVGeneralShowListSorting.AddRange(.TVGeneralShowListSorting)
            Me.LoadTVGeneralShowListSorting()

            Me.TVMeta.AddRange(.TVMetadataPerFileType)
            Me.LoadTVMetadata()

            Me.TVShowRegex.AddRange(.TVShowRegexes)
            Me.LoadTVShowRegex()

            Try
                Me.cbMovieScraperCertLang.Items.Clear()
                Me.cbMovieScraperCertLang.Items.Add(Master.eLang.All)
                Me.cbMovieScraperCertLang.Items.AddRange((From lLang In APIXML.MovieCertLanguagesXML.Language Select lLang.name).ToArray)
                If Me.cbMovieScraperCertLang.Items.Count > 0 Then
                    If .MovieScraperCertLang = Master.eLang.All Then
                        Me.cbMovieScraperCertLang.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.MovieScraperCertLang) Then
                        Me.cbMovieScraperCertLang.Text = APIXML.MovieCertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = .MovieScraperCertLang).name
                    Else
                        Me.cbMovieScraperCertLang.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Try
                Me.cbTVGeneralLang.Items.Clear()
                Me.cbTVGeneralLang.Items.AddRange((From lLang In .TVGeneralLanguages.Language Select lLang.name).ToArray)
                If Me.cbTVGeneralLang.Items.Count > 0 Then
                    Me.cbTVGeneralLang.Text = .TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .TVGeneralLanguage).name
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            If Not String.IsNullOrEmpty(.ProxyURI) AndAlso .ProxyPort >= 0 Then
                Me.chkProxyEnable.Checked = True
                Me.txtProxyURI.Text = .ProxyURI
                Me.txtProxyPort.Text = .ProxyPort.ToString

                If Not String.IsNullOrEmpty(.ProxyCredentials.UserName) Then
                    Me.chkProxyCredsEnable.Checked = True
                    Me.txtProxyUsername.Text = .ProxyCredentials.UserName
                    Me.txtProxyPassword.Text = .ProxyCredentials.Password
                    Me.txtProxyDomain.Text = .ProxyCredentials.Domain
                End If
            End If

            Me.chkMovieClickScrapeAsk.Enabled = Me.chkMovieClickScrape.Checked
            Me.chkMovieSetClickScrapeAsk.Enabled = Me.chkMovieSetClickScrape.Checked
            Me.txtMovieScraperDurationRuntimeFormat.Enabled = .MovieScraperUseMDDuration
            Me.txtTVScraperDurationRuntimeFormat.Enabled = .TVScraperUseMDDuration

            Me.RefreshMovieSetSortTokens()
            Me.RefreshMovieSortTokens()
            Me.RefreshMovieSources()
            Me.RefreshTVSources()
            Me.RefreshTVSortTokens()
            Me.RefreshTVShowFilters()
            Me.RefreshTVEpisodeFilters()
            Me.RefreshMovieFilters()
            Me.RefreshFileSystemExcludeDirs()
            Me.RefreshFileSystemValidExts()
            Me.RefreshFileSystemValidSubtitlesExts()
            Me.RefreshFileSystemValidThemeExts()

            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Me.chkMovieUseFrodo.Checked = .MovieUseFrodo
            Me.chkMovieActorThumbsFrodo.Checked = .MovieActorThumbsFrodo
            Me.chkMovieExtrafanartsFrodo.Checked = .MovieExtrafanartsFrodo
            Me.chkMovieExtrathumbsFrodo.Checked = .MovieExtrathumbsFrodo
            Me.chkMovieFanartFrodo.Checked = .MovieFanartFrodo
            Me.chkMovieNFOFrodo.Checked = .MovieNFOFrodo
            Me.chkMoviePosterFrodo.Checked = .MoviePosterFrodo
            Me.chkMovieTrailerFrodo.Checked = .MovieTrailerFrodo

            '*************** XBMC Eden settings ****************
            Me.chkMovieUseEden.Checked = .MovieUseEden
            Me.chkMovieActorThumbsEden.Checked = .MovieActorThumbsEden
            Me.chkMovieExtrafanartsEden.Checked = .MovieExtrafanartsEden
            Me.chkMovieExtrathumbsEden.Checked = .MovieExtrathumbsEden
            Me.chkMovieFanartEden.Checked = .MovieFanartEden
            Me.chkMovieNFOEden.Checked = .MovieNFOEden
            Me.chkMoviePosterEden.Checked = .MoviePosterEden
            Me.chkMovieTrailerEden.Checked = .MovieTrailerEden

            '************* XBMC optional settings **************
            Me.chkMovieXBMCProtectVTSBDMV.Checked = .MovieXBMCProtectVTSBDMV

            '******** XBMC ArtworkDownloader settings **********
            Me.chkMovieBannerAD.Checked = .MovieBannerAD
            Me.chkMovieClearArtAD.Checked = .MovieClearArtAD
            Me.chkMovieClearLogoAD.Checked = .MovieClearLogoAD
            Me.chkMovieDiscArtAD.Checked = .MovieDiscArtAD
            Me.chkMovieLandscapeAD.Checked = .MovieLandscapeAD

            '************** XBMC TvTunes settings **************
            Me.chkMovieXBMCThemeEnable.Checked = .MovieXBMCThemeEnable
            Me.chkMovieXBMCThemeCustom.Checked = .MovieXBMCThemeCustom
            Me.chkMovieXBMCThemeMovie.Checked = .MovieXBMCThemeMovie
            Me.chkMovieXBMCThemeSub.Checked = .MovieXBMCThemeSub
            Me.txtMovieXBMCThemeCustomPath.Text = .MovieXBMCThemeCustomPath
            Me.txtMovieXBMCThemeSubDir.Text = .MovieXBMCThemeSubDir

            '****************** YAMJ settings ******************
            Me.chkMovieUseYAMJ.Checked = .MovieUseYAMJ
            Me.chkMovieBannerYAMJ.Checked = .MovieBannerYAMJ
            Me.chkMovieFanartYAMJ.Checked = .MovieFanartYAMJ
            Me.chkMovieNFOYAMJ.Checked = .MovieNFOYAMJ
            Me.chkMoviePosterYAMJ.Checked = .MoviePosterYAMJ
            Me.chkMovieTrailerYAMJ.Checked = .MovieTrailerYAMJ

            '****************** NMJ settings ******************
            Me.chkMovieUseNMJ.Checked = .MovieUseNMJ
            Me.chkMovieBannerNMJ.Checked = .MovieBannerNMJ
            Me.chkMovieFanartNMJ.Checked = .MovieFanartNMJ
            Me.chkMovieNFONMJ.Checked = .MovieNFONMJ
            Me.chkMoviePosterNMJ.Checked = .MoviePosterNMJ
            Me.chkMovieTrailerNMJ.Checked = .MovieTrailerNMJ

            '************** NMT optional settings **************
            Me.chkMovieYAMJCompatibleSets.Checked = .MovieYAMJCompatibleSets
            Me.chkMovieYAMJWatchedFile.Checked = .MovieYAMJWatchedFile
            Me.txtMovieYAMJWatchedFolder.Text = .MovieYAMJWatchedFolder

            '***************** Boxee settings ******************
            Me.chkMovieUseBoxee.Checked = .MovieUseBoxee
            Me.chkMovieFanartBoxee.Checked = .MovieFanartBoxee
            Me.chkMovieNFOBoxee.Checked = .MovieNFOBoxee
            Me.chkMoviePosterBoxee.Checked = .MoviePosterBoxee

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
            '****************** MovieSet Part ******************
            '***************************************************

            '**************** XBMC MSAA settings ***************
            Me.chkMovieSetUseMSAA.Checked = .MovieSetUseMSAA
            Me.chkMovieSetBannerMSAA.Checked = .MovieSetBannerMSAA
            Me.chkMovieSetClearArtMSAA.Checked = .MovieSetClearArtMSAA
            Me.chkMovieSetClearLogoMSAA.Checked = .MovieSetClearLogoMSAA
            Me.chkMovieSetFanartMSAA.Checked = .MovieSetFanartMSAA
            Me.chkMovieSetLandscapeMSAA.Checked = .MovieSetLandscapeMSAA
            Me.chkMovieSetNFOMSAA.Checked = .MovieSetNFOMSAA
            Me.txtMovieSetPathMSAA.Text = .MovieSetPathMSAA
            Me.chkMovieSetPosterMSAA.Checked = .MovieSetPosterMSAA

            '***************** Expert settings *****************
            Me.chkMovieSetUseExpert.Checked = .MovieSetUseExpert

            '***************** Expert Single ******************
            Me.txtMovieSetBannerExpertSingle.Text = .MovieSetBannerExpertSingle
            Me.txtMovieSetClearArtExpertSingle.Text = .MovieSetClearArtExpertSingle
            Me.txtMovieSetClearLogoExpertSingle.Text = .MovieSetClearLogoExpertSingle
            Me.txtMovieSetDiscArtExpertSingle.Text = .MovieSetDiscArtExpertSingle
            Me.txtMovieSetFanartExpertSingle.Text = .MovieSetFanartExpertSingle
            Me.txtMovieSetLandscapeExpertSingle.Text = .MovieSetLandscapeExpertSingle
            Me.txtMovieSetNFOExpertSingle.Text = .MovieSetNFOExpertSingle
            Me.txtMovieSetPathExpertSingle.Text = .MovieSetPathExpertSingle
            Me.txtMovieSetPosterExpertSingle.Text = .MovieSetPosterExpertSingle

            '***************** Expert Parent ******************
            Me.txtMovieSetBannerExpertParent.Text = .MovieSetBannerExpertParent
            Me.txtMovieSetClearArtExpertParent.Text = .MovieSetClearArtExpertParent
            Me.txtMovieSetClearLogoExpertParent.Text = .MovieSetClearLogoExpertParent
            Me.txtMovieSetDiscArtExpertParent.Text = .MovieSetDiscArtExpertParent
            Me.txtMovieSetFanartExpertParent.Text = .MovieSetFanartExpertParent
            Me.txtMovieSetLandscapeExpertParent.Text = .MovieSetLandscapeExpertParent
            Me.txtMovieSetNFOExpertParent.Text = .MovieSetNFOExpertParent
            Me.txtMovieSetPosterExpertParent.Text = .MovieSetPosterExpertParent


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
            Me.chkTVShowExtrafanartsFrodo.Checked = .TVShowExtrafanartsFrodo
            Me.chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
            Me.chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '******** XBMC ArtworkDownloader settings **********
            Me.chkTVSeasonLandscapeAD.Checked = .TVSeasonLandscapeAD
            Me.chkTVShowCharacterArtAD.Checked = .TVShowCharacterArtAD
            Me.chkTVShowClearArtAD.Checked = .TVShowClearArtAD
            Me.chkTVShowClearLogoAD.Checked = .TVShowClearLogoAD
            Me.chkTVShowLandscapeAD.Checked = .TVShowLandscapeAD

            '************* XBMC TvTunes settings ***************
            Me.chkTVXBMCThemeEnable.Checked = .TVShowTVThemeXBMC
            Me.txtTVXBMCThemeCustomPath.Text = .TVShowTVThemeFolderXBMC

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
    End Sub

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Functions.PNLDoubleBuffer(Me.pnlSettingsMain)
        Me.SetUp()
        Me.AddPanels()
        Me.AddButtons()
        Me.AddHelpHandlers(Me, "Core_")

        'get optimal panel size
        Dim pWidth As Integer = CInt(Me.Width)
        Dim pHeight As Integer = CInt(Me.Height)
        If My.Computer.Screen.WorkingArea.Width < 1120 Then
            pWidth = CInt(My.Computer.Screen.WorkingArea.Width)
        End If
        If My.Computer.Screen.WorkingArea.Height < 900 Then
            pHeight = CInt(My.Computer.Screen.WorkingArea.Height)
        End If
        Me.Size = New Size(pWidth, pHeight)
        Dim pLeft As Integer
        Dim pTop As Integer
        pLeft = CInt((My.Computer.Screen.WorkingArea.Width - pWidth) / 2)
        pTop = CInt((My.Computer.Screen.WorkingArea.Height - pHeight) / 2)
        Me.Location = New Point(pLeft, pTop)

        Dim iBackground As New Bitmap(Me.pnlSettingsTop.Width, Me.pnlSettingsTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlSettingsTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsTop.ClientRectangle)
            Me.pnlSettingsTop.BackgroundImage = iBackground
        End Using

        iBackground = New Bitmap(Me.pnlSettingsCurrent.Width, Me.pnlSettingsCurrent.Height)
        Using b As Graphics = Graphics.FromImage(iBackground)
            b.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlSettingsCurrent.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsCurrent.ClientRectangle)
            Me.pnlSettingsCurrent.BackgroundImage = iBackground
        End Using

        Me.LoadGenreLangs()
        Me.LoadIntLangs()
        Me.LoadLangs()
        Me.LoadThemes()
        Me.LoadTVRatingRegions()
        Me.FillSettings()
        Me.lvMovieSources.ListViewItemSorter = New ListViewItemComparer(1)
        Me.lvTVSources.ListViewItemSorter = New ListViewItemComparer(1)
        Me.sResult.NeedsUpdate = False
        Me.sResult.NeedsRefresh_Movie = False
        Me.sResult.NeedsRefresh_MovieSet = False
        Me.sResult.NeedsRefresh_TV = False
        Me.sResult.DidCancel = False
        Me.didApply = False
        Me.NoUpdate = False
        RaiseEvent LoadEnd()
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

                For Each s As ModulesManager._externalScraperModuleClass_Data_Movie In (ModulesManager.Instance.externalScrapersModules_Data_Movie.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Data_MovieSet In (ModulesManager.Instance.externalScrapersModules_Data_MovieSet.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In (ModulesManager.Instance.externalScrapersModules_Image_Movie.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In (ModulesManager.Instance.externalScrapersModules_Image_MovieSet.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In (ModulesManager.Instance.externalScrapersModules_Theme_Movie.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In (ModulesManager.Instance.externalScrapersModules_Trailer_Movie.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_TV In ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.ModuleOrder)
                    RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
                Next
                For Each s As ModulesManager._externalScraperModuleClass_TV In ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsPostScraper).OrderBy(Function(x) x.PostScraperOrder)
                    RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In (ModulesManager.Instance.externalScrapersModules_Theme_TV.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
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

    Private Sub LoadMovieGeneralMediaListSorting()
        Dim lvItem As ListViewItem
        Me.lvMovieGeneralMediaListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In Me.MovieGeneralMediaListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            Me.lvMovieGeneralMediaListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadMovieSetGeneralMediaListSorting()
        Dim lvItem As ListViewItem
        Me.lvMovieSetGeneralMediaListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In Me.MovieSetGeneralMediaListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            Me.lvMovieSetGeneralMediaListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralEpisodeListSorting()
        Dim lvItem As ListViewItem
        Me.lvTVGeneralEpisodeListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In Me.TVGeneralEpisodeListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            Me.lvTVGeneralEpisodeListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralSeasonListSorting()
        Dim lvItem As ListViewItem
        Me.lvTVGeneralSeasonListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In Me.TVGeneralSeasonListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            Me.lvTVGeneralSeasonListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralShowListSorting()
        Dim lvItem As ListViewItem
        Me.lvTVGeneralShowListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In Me.TVGeneralShowListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            Me.lvTVGeneralShowListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadMovieMetadata()
        Me.lstMovieScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In MovieMeta
            Me.lstMovieScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub LoadTVRatingRegions()
        Me.cbTVScraperRatingRegion.Items.AddRange(APIXML.GetTVRatingRegions)
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
            lvItem.SubItems.Add(If(rShow.byDate, "Yes", "No"))
            Me.lvTVShowRegex.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadThemes()
        Me.cbGeneralMovieTheme.Items.Clear()
        Me.cbGeneralMovieSetTheme.Items.Clear()
        Me.cbGeneralTVShowTheme.Items.Clear()
        Me.cbGeneralTVEpisodeTheme.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Themes")) Then
            Dim mT As New List(Of String)
            Dim msT As New List(Of String)
            Dim sT As New List(Of String)
            Dim eT As New List(Of String)
            Try
                mT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "movie-*.xml"))
            Catch
            End Try
            Me.cbGeneralMovieTheme.Items.AddRange(mT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("movie-", String.Empty)).ToArray)
            Try
                msT.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Themes"), "movieset-*.xml"))
            Catch
            End Try
            Me.cbGeneralMovieSetTheme.Items.AddRange(msT.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL).Replace("movieset-", String.Empty)).ToArray)
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

    Private Sub LoadGeneralDateTime()
        Dim items As New Dictionary(Of String, Enums.DateTime)
        items.Add(Master.eLang.GetString(1210, "Current DateTime when adding"), Enums.DateTime.Now)
        items.Add(Master.eLang.GetString(1211, "mtime (fallback to ctime)"), Enums.DateTime.mtime)
        items.Add(Master.eLang.GetString(1212, "Newer of mtime and ctime"), Enums.DateTime.Newer)
        Me.cbGeneralDateTime.DataSource = items.ToList
        Me.cbGeneralDateTime.DisplayMember = "Key"
        Me.cbGeneralDateTime.ValueMember = "Value"
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
        Me.cbMovieSetFanartPrefSize.DataSource = items.ToList
        Me.cbMovieSetFanartPrefSize.DisplayMember = "Key"
        Me.cbMovieSetFanartPrefSize.ValueMember = "Value"
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
        Me.cbMovieSetPosterPrefSize.DataSource = items.ToList
        Me.cbMovieSetPosterPrefSize.DisplayMember = "Key"
        Me.cbMovieSetPosterPrefSize.ValueMember = "Value"
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
        Me.cbMovieSetBannerPrefType.DataSource = items.ToList
        Me.cbMovieSetBannerPrefType.DisplayMember = "Key"
        Me.cbMovieSetBannerPrefType.ValueMember = "Value"
    End Sub

    Private Sub LoadMovieTrailerQualities()
        Dim items As New Dictionary(Of String, Enums.TrailerVideoQuality)
        items.Add(Master.eLang.GetString(569, "All"), Enums.TrailerVideoQuality.All)
        items.Add("2160p", Enums.TrailerVideoQuality.HD2160p)
        items.Add("2160p 60fps", Enums.TrailerVideoQuality.HD2160p60fps)
        items.Add("1440p", Enums.TrailerVideoQuality.HD1440p)
        items.Add("1080p", Enums.TrailerVideoQuality.HD1080p)
        items.Add("1080p 60fps", Enums.TrailerVideoQuality.HD1080p60fps)
        items.Add("720p", Enums.TrailerVideoQuality.HD720p)
        items.Add("720p 60fps", Enums.TrailerVideoQuality.HD720p60fps)
        items.Add("480p", Enums.TrailerVideoQuality.HQ480p)
        items.Add("360p", Enums.TrailerVideoQuality.SQ360p)
        items.Add("240p", Enums.TrailerVideoQuality.SQ240p)
        items.Add("144p", Enums.TrailerVideoQuality.SQ144p)
        Me.cbMovieTrailerMinVideoQual.DataSource = items.ToList
        Me.cbMovieTrailerMinVideoQual.DisplayMember = "Key"
        Me.cbMovieTrailerMinVideoQual.ValueMember = "Value"
        Me.cbMovieTrailerPrefVideoQual.DataSource = items.ToList
        Me.cbMovieTrailerPrefVideoQual.DisplayMember = "Key"
        Me.cbMovieTrailerPrefVideoQual.ValueMember = "Value"
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
        Me.cbTVShowEFanartsPrefSize.DataSource = items.ToList
        Me.cbTVShowEFanartsPrefSize.DisplayMember = "Key"
        Me.cbTVShowEFanartsPrefSize.ValueMember = "Value"
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
        Dim items2 As New Dictionary(Of String, Enums.TVEpisodePosterSize)
        items2.Add("400x225", Enums.TVEpisodePosterSize.SD225)
        Me.cbTVEpisodePosterPrefSize.DataSource = items2.ToList
        Me.cbTVEpisodePosterPrefSize.DisplayMember = "Key"
        Me.cbTVEpisodePosterPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVScraperOptionsOrdering()
        Dim items As New Dictionary(Of String, Enums.Ordering)
        items.Add(Master.eLang.GetString(438, "Standard"), Enums.Ordering.Standard)
        items.Add(Master.eLang.GetString(1067, "DVD"), Enums.Ordering.DVD)
        items.Add(Master.eLang.GetString(839, "Absolute"), Enums.Ordering.Absolute)
        items.Add(Master.eLang.GetString(1332, "Day Of Year"), Enums.Ordering.DayOfYear)
        Me.cbTVScraperOptionsOrdering.DataSource = items.ToList
        Me.cbTVScraperOptionsOrdering.DisplayMember = "Key"
        Me.cbTVScraperOptionsOrdering.ValueMember = "Value"
    End Sub

    Private Sub LoadTVScraperUpdateTime()
        Dim items As New Dictionary(Of String, Enums.TVScraperUpdateTime)
        items.Add(Master.eLang.GetString(749, "Week"), Enums.TVScraperUpdateTime.Week)
        items.Add(Master.eLang.GetString(750, "Bi-Weekly"), Enums.TVScraperUpdateTime.BiWeekly)
        items.Add(Master.eLang.GetString(751, "Month"), Enums.TVScraperUpdateTime.Month)
        items.Add(Master.eLang.GetString(752, "Never"), Enums.TVScraperUpdateTime.Never)
        items.Add(Master.eLang.GetString(753, "Always"), Enums.TVScraperUpdateTime.Always)
        Me.cbTVScraperUpdateTime.DataSource = items.ToList
        Me.cbTVScraperUpdateTime.DisplayMember = "Key"
        Me.cbTVScraperUpdateTime.ValueMember = "Value"
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
        items.Add(Master.eLang.GetString(745, "None"), Enums.TVShowBannerType.None)
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

    Private Sub LoadTVSeasonRetrieve()
        Dim items As New Dictionary(Of String, Settings.EpRetrieve)
        items.Add(Master.eLang.GetString(13, "Folder Name"), Settings.EpRetrieve.FromDirectory)
        items.Add(Master.eLang.GetString(15, "File Name"), Settings.EpRetrieve.FromFilename)
        Me.cbTVSeasonRetrieve.DataSource = items.ToList
        Me.cbTVSeasonRetrieve.DisplayMember = "Key"
        Me.cbTVSeasonRetrieve.ValueMember = "Value"
    End Sub

    Private Sub LoadTVEpisodeRetrieve()
        Dim items As New Dictionary(Of String, Settings.EpRetrieve)
        items.Add(Master.eLang.GetString(13, "Folder Name"), Settings.EpRetrieve.FromDirectory)
        items.Add(Master.eLang.GetString(15, "File Name"), Settings.EpRetrieve.FromFilename)
        items.Add(Master.eLang.GetString(16, "Season Result"), Settings.EpRetrieve.FromSeasonResult)
        Me.cbTVEpisodeRetrieve.DataSource = items.ToList
        Me.cbTVEpisodeRetrieve.DisplayMember = "Key"
        Me.cbTVEpisodeRetrieve.ValueMember = "Value"
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

    Private Sub lstFileSystemValidExts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstFileSystemValidVideoExts.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveFileSystemValidExts()
    End Sub

    Private Sub lstFileSystemValidSubtitlesExts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstFileSystemValidSubtitlesExts.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveFileSystemValidSubtitlesExts()
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

    Private Sub lstMovieSetSortTokens_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstMovieSetSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieSetSortToken()
    End Sub

    Private Sub lsttvSortTokens_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstTVSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVSortToken()
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

    Private Sub RefreshMovieSortTokens()
        Me.lstMovieSortTokens.Items.Clear()
        Me.lstMovieSortTokens.Items.AddRange(Master.eSettings.MovieSortTokens.ToArray)
    End Sub

    Private Sub RefreshMovieSetSortTokens()
        Me.lstMovieSetSortTokens.Items.Clear()
        Me.lstMovieSetSortTokens.Items.AddRange(Master.eSettings.MovieSetSortTokens.ToArray)
    End Sub

    Private Sub RefreshTVShowFilters()
        Me.lstTVShowFilter.Items.Clear()
        Me.lstTVShowFilter.Items.AddRange(Master.eSettings.TVShowFilterCustom.ToArray)
    End Sub

    Private Sub RefreshTVSortTokens()
        Me.lstTVSortTokens.Items.Clear()
        Me.lstTVSortTokens.Items.AddRange(Master.eSettings.TVSortTokens.ToArray)
    End Sub

    Private Sub RefreshMovieSources()
        Dim lvItem As ListViewItem

        lvMovieSources.Items.Clear()
        Master.DB.LoadMovieSourcesFromDB()
        For Each s As Structures.MovieSource In Master.MovieSources
            lvItem = New ListViewItem(s.id)
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(If(s.Recursive, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.UseFolderName, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.IsSingle, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.Exclude, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieSources.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        Master.DB.LoadTVSourcesFromDB()
        lvTVSources.Items.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT ID, Name, Path, LastScan, Language, Ordering, Exclude, EpisodeSorting FROM TVSources;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lvItem = New ListViewItem(SQLreader("ID").ToString)
                    lvItem.SubItems.Add(SQLreader("Name").ToString)
                    lvItem.SubItems.Add(SQLreader("Path").ToString)
                    lvItem.SubItems.Add(Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = SQLreader("Language").ToString).name)
                    lvItem.SubItems.Add(DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.Ordering).ToString)
                    lvItem.SubItems.Add(If(Convert.ToBoolean(SQLreader("Exclude")), Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
                    lvItem.SubItems.Add(DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting).ToString)
                    lvTVSources.Items.Add(lvItem)
                End While
            End Using
        End Using
    End Sub

    Private Sub RefreshFileSystemExcludeDirs()
        Me.lstFileSystemExcludedDirs.Items.Clear()
        Me.lstFileSystemExcludedDirs.Items.AddRange(Master.ExcludeDirs.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidExts()
        Me.lstFileSystemValidVideoExts.Items.Clear()
        Me.lstFileSystemValidVideoExts.Items.AddRange(Master.eSettings.FileSystemValidExts.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidSubtitlesExts()
        Me.lstFileSystemValidSubtitlesExts.Items.Clear()
        Me.lstFileSystemValidSubtitlesExts.Items.AddRange(Master.eSettings.FileSystemValidSubtitlesExts.ToArray)
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
            Me.sResult.NeedsRefresh_TV = True
        End If
    End Sub

    Private Sub RemoveMovieFilter()
        If Me.lstMovieFilters.Items.Count > 0 AndAlso Me.lstMovieFilters.SelectedItems.Count > 0 Then
            While Me.lstMovieFilters.SelectedItems.Count > 0
                Me.lstMovieFilters.Items.Remove(Me.lstMovieFilters.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsRefresh_Movie = True
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
        If lstFileSystemValidVideoExts.Items.Count > 0 AndAlso lstFileSystemValidVideoExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemValidVideoExts.SelectedItems.Count > 0
                Me.lstFileSystemValidVideoExts.Items.Remove(Me.lstFileSystemValidVideoExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsUpdate = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidSubtitlesExts()
        If lstFileSystemValidSubtitlesExts.Items.Count > 0 AndAlso lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0
                Me.lstFileSystemValidSubtitlesExts.Items.Remove(Me.lstFileSystemValidSubtitlesExts.SelectedItems(0))
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
        If Me.lvMovieSources.SelectedItems.Count > 0 Then
            If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                Me.lvMovieSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                        While Me.lvMovieSources.SelectedItems.Count > 0
                            parSource.Value = lvMovieSources.SelectedItems(0).SubItems(1).Text
                            SQLcommand.CommandText = "SELECT idMovie FROM movie WHERE source = (?);"
                            Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                                While SQLReader.Read
                                    Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("idMovie")), True)
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
            Me.sResult.NeedsRefresh_TV = True
        End If
    End Sub

    Private Sub RemoveMovieSortToken()
        If Me.lstMovieSortTokens.Items.Count > 0 AndAlso Me.lstMovieSortTokens.SelectedItems.Count > 0 Then
            While Me.lstMovieSortTokens.SelectedItems.Count > 0
                Me.lstMovieSortTokens.Items.Remove(Me.lstMovieSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsRefresh_Movie = True
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveMovieSetSortToken()
        If Me.lstMovieSetSortTokens.Items.Count > 0 AndAlso Me.lstMovieSetSortTokens.SelectedItems.Count > 0 Then
            While Me.lstMovieSetSortTokens.SelectedItems.Count > 0
                Me.lstMovieSetSortTokens.Items.Remove(Me.lstMovieSetSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsRefresh_MovieSet = True
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveTVSortToken()
        If Me.lstTVSortTokens.Items.Count > 0 AndAlso Me.lstTVSortTokens.SelectedItems.Count > 0 Then
            While Me.lstTVSortTokens.SelectedItems.Count > 0
                Me.lstTVSortTokens.Items.Remove(Me.lstTVSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsRefresh_TV = True
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
        If Me.lvTVSources.SelectedItems.Count > 0 Then
            If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the TV Shows from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                Me.lvTVSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                        While Me.lvTVSources.SelectedItems.Count > 0
                            parSource.Value = lvTVSources.SelectedItems(0).SubItems(1).Text
                            SQLcommand.CommandText = "SELECT idShow FROM tvshow WHERE Source = (?);"
                            Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                                While SQLReader.Read
                                    Master.DB.DeleteTVShowFromDB(Convert.ToInt64(SQLReader("idShow")), True)
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
    End Sub

    Private Sub RenumberTVShowRegex()
        For i As Integer = 0 To Me.TVShowRegex.Count - 1
            Me.TVShowRegex(i).ID = i
        Next
    End Sub

    Private Sub RenumberMovieGeneralMediaListSorting()
        For i As Integer = 0 To Me.MovieGeneralMediaListSorting.Count - 1
            Me.MovieGeneralMediaListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberMovieSetGeneralMediaListSorting()
        For i As Integer = 0 To Me.MovieSetGeneralMediaListSorting.Count - 1
            Me.MovieSetGeneralMediaListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVEpisodeGeneralMediaListSorting()
        For i As Integer = 0 To Me.TVGeneralEpisodeListSorting.Count - 1
            Me.TVGeneralEpisodeListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVSeasonGeneralMediaListSorting()
        For i As Integer = 0 To Me.TVGeneralSeasonListSorting.Count - 1
            Me.TVGeneralSeasonListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVShowGeneralMediaListSorting()
        For i As Integer = 0 To Me.TVGeneralShowListSorting.Count - 1
            Me.TVGeneralShowListSorting(i).DisplayIndex = i
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
            .RestartScraper = Me.chkGeneralResumeScraper.Checked
            .GeneralCheckUpdates = chkGeneralCheckUpdates.Checked
            .GeneralDateAddedIgnoreNFO = Me.chkGeneralDateAddedIgnoreNFO.Checked
            If Me.cbGeneralDateTime.SelectedValue IsNot Nothing Then
                .GeneralDateTime = CType(Me.cbGeneralDateTime.SelectedValue, Enums.DateTime)
            End If
            .GeneralDoubleClickScrape = Me.chkGeneralDoubleClickScrape.Checked
            .GeneralDaemonDrive = Me.cbGeneralDaemonDrive.Text
            .GeneralDaemonPath = Me.txtGeneralDaemonPath.Text
            .GeneralHideBanner = Me.chkGeneralHideBanner.Checked
            .GeneralHideCharacterArt = Me.chkGeneralHideCharacterArt.Checked
            .GeneralHideClearArt = Me.chkGeneralHideClearArt.Checked
            .GeneralHideClearLogo = Me.chkGeneralHideClearLogo.Checked
            .GeneralHideDiscArt = Me.chkGeneralHideDiscArt.Checked
            .GeneralHideFanart = Me.chkGeneralHideFanart.Checked
            .GeneralHideFanartSmall = Me.chkGeneralHideFanartSmall.Checked
            .GeneralHideLandscape = Me.chkGeneralHideLandscape.Checked
            .GeneralHidePoster = Me.chkGeneralHidePoster.Checked
            .GeneralImagesGlassOverlay = Me.chkGeneralImagesGlassOverlay.Checked
            .GeneralLanguage = Me.cbGeneralLanguage.Text
            .GeneralMovieTheme = Me.cbGeneralMovieTheme.Text
            .GeneralMovieSetTheme = Me.cbGeneralMovieSetTheme.Text
            .GeneralOverwriteNfo = Me.chkGeneralOverwriteNfo.Checked
            .GeneralShowGenresText = Me.chkGeneralShowGenresText.Checked
            .GeneralShowLangFlags = Me.chkGeneralShowLangFlags.Checked
            .GeneralShowImgDims = Me.chkGeneralShowImgDims.Checked
            .GeneralShowImgNames = Me.chkGeneralShowImgNames.Checked
            .GeneralSourceFromFolder = Me.chkGeneralSourceFromFolder.Checked
            .GeneralTVEpisodeTheme = Me.cbGeneralTVEpisodeTheme.Text
            .GeneralTVShowTheme = Me.cbGeneralTVShowTheme.Text
            .MovieActorThumbsOverwrite = Me.chkMovieActorThumbsOverwrite.Checked
            '.MovieActorThumbsQual = Me.tbMovieActorThumbsQual.value
            .MovieBackdropsPath = Me.txtMovieSourcesBackdropsFolderPath.Text
            If Not String.IsNullOrEmpty(Me.txtMovieSourcesBackdropsFolderPath.Text) Then
                .MovieBackdropsAuto = Me.chkMovieSourcesBackdropsAuto.Checked
            Else
                .MovieBackdropsAuto = False
            End If
            .MovieBannerHeight = If(Not String.IsNullOrEmpty(Me.txtMovieBannerHeight.Text), Convert.ToInt32(Me.txtMovieBannerHeight.Text), 0)
            .MovieBannerOverwrite = Me.chkMovieBannerOverwrite.Checked
            .MovieBannerPrefOnly = Me.chkMovieBannerPrefOnly.Checked
            If Me.cbMovieBannerPrefType.SelectedValue IsNot Nothing Then
                .MovieBannerPrefType = CType(Me.cbMovieBannerPrefType.SelectedValue, Enums.MovieBannerType)
            End If
            .MovieBannerResize = Me.chkMovieBannerResize.Checked
            .MovieBannerWidth = If(Not String.IsNullOrEmpty(Me.txtMovieBannerWidth.Text), Convert.ToInt32(Me.txtMovieBannerWidth.Text), 0)
            .MovieCleanDB = Me.chkMovieCleanDB.Checked
            .MovieClearArtOverwrite = Me.chkMovieClearArtOverwrite.Checked
            .MovieClearLogoOverwrite = Me.chkMovieClearLogoOverwrite.Checked
            .MovieClickScrape = Me.chkMovieClickScrape.Checked
            .MovieClickScrapeAsk = Me.chkMovieClickScrapeAsk.Checked
            .MovieDiscArtOverwrite = Me.chkMovieDiscArtOverwrite.Checked
            .MovieDisplayYear = Me.chkMovieDisplayYear.Checked
            .MovieEFanartsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsHeight.Text), Convert.ToInt32(Me.txtMovieEFanartsHeight.Text), 0)
            .MovieEFanartsLimit = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsLimit.Text), Convert.ToInt32(Me.txtMovieEFanartsLimit.Text), 0)
            .MovieEFanartsOverwrite = Me.chkMovieEFanartsOverwrite.Checked
            .MovieEFanartsPrefOnly = Me.chkMovieEFanartsPrefOnly.Checked
            If Me.cbMovieEFanartsPrefSize.SelectedValue IsNot Nothing Then
                .MovieEFanartsPrefSize = CType(Me.cbMovieEFanartsPrefSize.SelectedValue, Enums.FanartSize)
            End If
            .MovieEFanartsResize = Me.chkMovieEFanartsResize.Checked
            .MovieEFanartsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieEFanartsWidth.Text), Convert.ToInt32(Me.txtMovieEFanartsWidth.Text), 0)
            .MovieEThumbsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsHeight.Text), Convert.ToInt32(Me.txtMovieEThumbsHeight.Text), 0)
            .MovieEThumbsLimit = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsLimit.Text), Convert.ToInt32(Me.txtMovieEThumbsLimit.Text), 0)
            .MovieEThumbsOverwrite = Me.chkMovieEThumbsOverwrite.Checked
            .MovieEThumbsPrefOnly = Me.chkMovieEThumbsPrefOnly.Checked
            If Me.cbMovieEThumbsPrefSize.SelectedValue IsNot Nothing Then
                .MovieEThumbsPrefSize = CType(Me.cbMovieEThumbsPrefSize.SelectedValue, Enums.FanartSize)
            End If
            .MovieEThumbsResize = Me.chkMovieEThumbsResize.Checked
            .MovieEThumbsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieEThumbsWidth.Text), Convert.ToInt32(Me.txtMovieEThumbsWidth.Text), 0)
            .MovieFanartHeight = If(Not String.IsNullOrEmpty(Me.txtMovieFanartHeight.Text), Convert.ToInt32(Me.txtMovieFanartHeight.Text), 0)
            .MovieFanartOverwrite = Me.chkMovieFanartOverwrite.Checked
            .MovieFanartPrefOnly = Me.chkMovieFanartPrefOnly.Checked
            If Me.cbMovieFanartPrefSize.SelectedValue IsNot Nothing Then
                .MovieFanartPrefSize = CType(Me.cbMovieFanartPrefSize.SelectedValue, Enums.FanartSize)
            End If
            .MovieFanartResize = Me.chkMovieFanartResize.Checked
            .MovieFanartWidth = If(Not String.IsNullOrEmpty(Me.txtMovieFanartWidth.Text), Convert.ToInt32(Me.txtMovieFanartWidth.Text), 0)
            .MovieFilterCustom.Clear()
            .MovieFilterCustom.AddRange(Me.lstMovieFilters.Items.OfType(Of String).ToList)
            If .MovieFilterCustom.Count <= 0 Then .MovieFilterCustomIsEmpty = True
            .MovieGeneralCustomMarker1Color = Me.btnMovieGeneralCustomMarker1.BackColor.ToArgb
            .MovieGeneralCustomMarker2Color = Me.btnMovieGeneralCustomMarker2.BackColor.ToArgb
            .MovieGeneralCustomMarker3Color = Me.btnMovieGeneralCustomMarker3.BackColor.ToArgb
            .MovieGeneralCustomMarker4Color = Me.btnMovieGeneralCustomMarker4.BackColor.ToArgb
            .MovieGeneralCustomMarker1Name = Me.txtMovieGeneralCustomMarker1.Text
            .MovieGeneralCustomMarker2Name = Me.txtMovieGeneralCustomMarker2.Text
            .MovieGeneralCustomMarker3Name = Me.txtMovieGeneralCustomMarker3.Text
            .MovieGeneralCustomMarker4Name = Me.txtMovieGeneralCustomMarker4.Text
            .MovieGeneralFlagLang = If(Me.cbMovieLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, Me.cbMovieLanguageOverlay.Text)
            .MovieGeneralIgnoreLastScan = Me.chkMovieGeneralIgnoreLastScan.Checked
            .MovieGeneralMarkNew = Me.chkMovieGeneralMarkNew.Checked
            .MovieGeneralMediaListSorting.Clear()
            .MovieGeneralMediaListSorting.AddRange(Me.MovieGeneralMediaListSorting)
            If Not String.IsNullOrEmpty(Me.txtMovieIMDBURL.Text) Then
                .MovieIMDBURL = Strings.Replace(Me.txtMovieIMDBURL.Text, "http://", String.Empty)
            Else
                .MovieIMDBURL = "akas.imdb.com"
            End If
            .MovieLandscapeOverwrite = Me.chkMovieLandscapeOverwrite.Checked
            .MovieLevTolerance = If(Not String.IsNullOrEmpty(Me.txtMovieLevTolerance.Text), Convert.ToInt32(Me.txtMovieLevTolerance.Text), 0)
            .MovieLockActors = Me.chkMovieLockActors.Checked
            .MovieLockCollectionID = Me.chkMovieLockCollectionID.Checked
            .MovieLockCollections = Me.chkMovieLockCollections.Checked
            .MovieLockCountry = Me.chkMovieLockCountry.Checked
            .MovieLockDirector = Me.chkMovieLockDirector.Checked
            .MovieLockGenre = Me.chkMovieLockGenre.Checked
            .MovieLockLanguageA = Me.chkMovieLockLanguageA.Checked
            .MovieLockLanguageV = Me.chkMovieLockLanguageV.Checked
            .MovieLockCert = Me.chkMovieLockCert.Checked
            .MovieLockMPAA = Me.chkMovieLockMPAA.Checked
            .MovieLockOutline = Me.chkMovieLockOutline.Checked
            .MovieLockPlot = Me.chkMovieLockPlot.Checked
            .MovieLockRating = Me.chkMovieLockRating.Checked
            .MovieLockReleaseDate = Me.chkMovieLockReleaseDate.Checked
            .MovieLockRuntime = Me.chkMovieLockRuntime.Checked
            .MovieLockStudio = Me.chkMovieLockStudio.Checked
            .MovieLockTags = Me.chkMovieLockTags.Checked
            .MovieLockTagline = Me.chkMovieLockTagline.Checked
            .MovieLockOriginalTitle = Me.chkMovieLockOriginalTitle.Checked
            .MovieLockTitle = Me.chkMovieLockTitle.Checked
            .MovieLockTop250 = Me.chkMovieLockTop250.Checked
            .MovieLockTrailer = Me.chkMovieLockTrailer.Checked
            .MovieLockVotes = Me.chkMovieLockVotes.Checked
            .MovieLockCredits = Me.chkMovieLockCredits.Checked
            .MovieLockYear = Me.chkMovieLockYear.Checked
            .MovieMetadataPerFileType.Clear()
            .MovieMetadataPerFileType.AddRange(Me.MovieMeta)
            .MovieNoSaveImagesToNfo = Me.chkMovieNoSaveImagesToNfo.Checked
            .MoviePosterHeight = If(Not String.IsNullOrEmpty(Me.txtMoviePosterHeight.Text), Convert.ToInt32(Me.txtMoviePosterHeight.Text), 0)
            .MoviePosterOverwrite = Me.chkMoviePosterOverwrite.Checked
            .MoviePosterPrefOnly = Me.chkMoviePosterPrefOnly.Checked
            If Me.cbMoviePosterPrefSize.SelectedValue IsNot Nothing Then
                .MoviePosterPrefSize = CType(Me.cbMoviePosterPrefSize.SelectedValue, Enums.PosterSize)
            End If
            .MoviePosterResize = Me.chkMoviePosterResize.Checked
            .MoviePosterWidth = If(Not String.IsNullOrEmpty(Me.txtMoviePosterWidth.Text), Convert.ToInt32(Me.txtMoviePosterWidth.Text), 0)
            .MovieProperCase = Me.chkMovieProperCase.Checked
            .MovieSetBannerHeight = If(Not String.IsNullOrEmpty(Me.txtMovieSetBannerHeight.Text), Convert.ToInt32(Me.txtMovieSetBannerHeight.Text), 0)
            .MovieSetBannerOverwrite = Me.chkMovieSetBannerOverwrite.Checked
            .MovieSetBannerPrefOnly = Me.chkMovieSetBannerPrefOnly.Checked
            If Me.cbMovieSetBannerPrefType.SelectedValue IsNot Nothing Then
                .MovieSetBannerPrefType = CType(Me.cbMovieSetBannerPrefType.SelectedValue, Enums.MovieBannerType)
            End If
            .MovieSetBannerResize = Me.chkMovieSetBannerResize.Checked
            .MovieSetBannerWidth = If(Not String.IsNullOrEmpty(Me.txtMovieSetBannerWidth.Text), Convert.ToInt32(Me.txtMovieSetBannerWidth.Text), 0)
            .MovieSetCleanDB = Me.chkMovieSetCleanDB.Checked
            .MovieSetCleanFiles = Me.chkMovieSetCleanFiles.Checked
            .MovieSetClearArtOverwrite = Me.chkMovieSetClearArtOverwrite.Checked
            .MovieSetClearLogoOverwrite = Me.chkMovieSetClearLogoOverwrite.Checked
            .MovieSetClickScrape = Me.chkMovieSetClickScrape.Checked
            .MovieSetClickScrapeAsk = Me.chkMovieSetClickScrapeAsk.Checked
            .MovieSetFanartHeight = If(Not String.IsNullOrEmpty(Me.txtMovieSetFanartHeight.Text), Convert.ToInt32(Me.txtMovieSetFanartHeight.Text), 0)
            .MovieSetFanartOverwrite = Me.chkMovieSetFanartOverwrite.Checked
            .MovieSetFanartPrefOnly = Me.chkMovieSetFanartPrefOnly.Checked
            If Me.cbMovieSetFanartPrefSize.SelectedValue IsNot Nothing Then
                .MovieSetFanartPrefSize = CType(Me.cbMovieSetFanartPrefSize.SelectedValue, Enums.FanartSize)
            End If
            .MovieSetFanartResize = Me.chkMovieSetFanartResize.Checked
            .MovieSetFanartWidth = If(Not String.IsNullOrEmpty(Me.txtMovieSetFanartWidth.Text), Convert.ToInt32(Me.txtMovieSetFanartWidth.Text), 0)
            .MovieSetGeneralMarkNew = Me.chkMovieSetGeneralMarkNew.Checked
            .MovieSetGeneralMediaListSorting.Clear()
            .MovieSetGeneralMediaListSorting.AddRange(Me.MovieSetGeneralMediaListSorting)
            .MovieSetLandscapeOverwrite = Me.chkMovieSetLandscapeOverwrite.Checked
            .MovieSetLockPlot = Me.chkMovieSetLockPlot.Checked
            .MovieSetLockTitle = Me.chkMovieSetLockTitle.Checked
            .MovieSetPosterHeight = If(Not String.IsNullOrEmpty(Me.txtMovieSetPosterHeight.Text), Convert.ToInt32(Me.txtMovieSetPosterHeight.Text), 0)
            .MovieSetPosterOverwrite = Me.chkMovieSetPosterOverwrite.Checked
            .MovieSetPosterPrefOnly = Me.chkMovieSetPosterPrefOnly.Checked
            If Me.cbMovieSetPosterPrefSize.SelectedValue IsNot Nothing Then
                .MovieSetPosterPrefSize = CType(Me.cbMovieSetPosterPrefSize.SelectedValue, Enums.PosterSize)
            End If
            .MovieSetPosterResize = Me.chkMovieSetPosterResize.Checked
            .MovieSetPosterWidth = If(Not String.IsNullOrEmpty(Me.txtMovieSetPosterWidth.Text), Convert.ToInt32(Me.txtMovieSetPosterWidth.Text), 0)
            .MovieSetScraperPlot = Me.chkMovieSetScraperPlot.Checked
            .MovieSetScraperTitle = Me.chkMovieSetScraperTitle.Checked
            .MovieScanOrderModify = Me.chkMovieScanOrderModify.Checked
            .MovieScraperCast = Me.chkMovieScraperCast.Checked
            If Not String.IsNullOrEmpty(Me.txtMovieScraperCastLimit.Text) Then
                .MovieScraperCastLimit = Convert.ToInt32(Me.txtMovieScraperCastLimit.Text)
            Else
                .MovieScraperCastLimit = 0
            End If
            .MovieScraperCastWithImgOnly = Me.chkMovieScraperCastWithImg.Checked
            .MovieScraperCert = Me.chkMovieScraperCert.Checked
            .MovieScraperCertForMPAA = Me.chkMovieScraperCertForMPAA.Checked
            .MovieScraperCertForMPAAFallback = Me.chkMovieScraperCertForMPAAFallback.Checked
            .MovieScraperCertFSK = Me.chkMovieScraperCertFSK.Checked
            .MovieScraperCertOnlyValue = Me.chkMovieScraperCertOnlyValue.Checked
            If Me.cbMovieScraperCertLang.Text <> String.Empty Then
                If Me.cbMovieScraperCertLang.SelectedIndex = 0 Then
                    .MovieScraperCertLang = Master.eLang.All
                Else
                    .MovieScraperCertLang = APIXML.MovieCertLanguagesXML.Language.FirstOrDefault(Function(l) l.name = cbMovieScraperCertLang.Text).abbreviation
                End If
            End If
            .MovieScraperCleanFields = Me.chkMovieScraperCleanFields.Checked
            .MovieScraperCleanPlotOutline = Me.chkMovieScraperCleanPlotOutline.Checked
            .MovieScraperCollectionID = Me.chkMovieScraperCollectionID.Checked
            .MovieScraperCollectionsAuto = Me.chkMovieScraperCollectionsAuto.Checked
            .MovieScraperCountry = Me.chkMovieScraperCountry.Checked
            .MovieScraperDirector = Me.chkMovieScraperDirector.Checked
            .MovieScraperDurationRuntimeFormat = Me.txtMovieScraperDurationRuntimeFormat.Text
            .MovieScraperGenre = Me.chkMovieScraperGenre.Checked
            If Not String.IsNullOrEmpty(Me.txtMovieScraperGenreLimit.Text) Then
                .MovieScraperGenreLimit = Convert.ToInt32(Me.txtMovieScraperGenreLimit.Text)
            Else
                .MovieScraperGenreLimit = 0
            End If
            .MovieScraperMetaDataIFOScan = Me.chkMovieScraperMetaDataIFOScan.Checked
            .MovieScraperMetaDataScan = Me.chkMovieScraperMetaDataScan.Checked
            .MovieScraperMPAA = Me.chkMovieScraperMPAA.Checked
            .MovieScraperMPAANotRated = Me.txtMovieScraperMPAANotRated.Text
            .MovieScraperOriginalTitle = Me.chkMovieScraperOriginalTitle.Checked
            .MovieScraperOutline = Me.chkMovieScraperOutline.Checked
            If Not String.IsNullOrEmpty(Me.txtMovieScraperOutlineLimit.Text) Then
                .MovieScraperOutlineLimit = Convert.ToInt32(Me.txtMovieScraperOutlineLimit.Text)
            Else
                .MovieScraperOutlineLimit = 0
            End If
            .MovieScraperPlot = Me.chkMovieScraperPlot.Checked
            .MovieScraperPlotForOutline = Me.chkMovieScraperPlotForOutline.Checked
            .MovieScraperRating = Me.chkMovieScraperRating.Checked
            .MovieScraperRelease = Me.chkMovieScraperRelease.Checked
            .MovieScraperVotesSeparator = Me.chkMovieScraperVotesSeperator.Checked
            .MovieScraperReleaseFormat = Me.chkMovieScraperReleaseFormat.Checked
            .MovieScraperRuntime = Me.chkMovieScraperRuntime.Checked
            .MovieScraperStudio = Me.chkMovieScraperStudio.Checked
            .MovieScraperStudioWithImgOnly = Me.chkMovieScraperStudioWithImg.Checked
            If Not String.IsNullOrEmpty(Me.txtMovieScraperStudioLimit.Text) Then
                .MovieScraperStudioLimit = Convert.ToInt32(Me.txtMovieScraperStudioLimit.Text)
            Else
                .MovieScraperStudioLimit = 0
            End If
            .MovieScraperTagline = Me.chkMovieScraperTagline.Checked
            .MovieScraperTitle = Me.chkMovieScraperTitle.Checked
            .MovieScraperTop250 = Me.chkMovieScraperTop250.Checked
            .MovieScraperTrailer = Me.chkMovieScraperTrailer.Checked
            .MovieScraperUseDetailView = Me.chkMovieScraperDetailView.Checked
            .MovieScraperUseMDDuration = Me.chkMovieScraperUseMDDuration.Checked
            .MovieScraperVotes = Me.chkMovieScraperVotes.Checked
            .MovieScraperCredits = Me.chkMovieScraperCredits.Checked
            .MovieScraperXBMCTrailerFormat = Me.chkMovieScraperXBMCTrailerFormat.Checked
            .MovieScraperYear = Me.chkMovieScraperYear.Checked
            If Not String.IsNullOrEmpty(Me.txtMovieSkipLessThan.Text) AndAlso IsNumeric(Me.txtMovieSkipLessThan.Text) Then
                .MovieSkipLessThan = Convert.ToInt32(Me.txtMovieSkipLessThan.Text)
            Else
                .MovieSkipLessThan = 0
            End If
            .MovieSkipStackedSizeCheck = Me.chkMovieSkipStackedSizeCheck.Checked
            .MovieSortBeforeScan = Me.chkMovieSortBeforeScan.Checked
            .MovieSortTokens.Clear()
            .MovieSortTokens.AddRange(lstMovieSortTokens.Items.OfType(Of String).ToList)
            If .MovieSortTokens.Count <= 0 Then .MovieSortTokensIsEmpty = True
            .MovieSetSortTokens.Clear()
            .MovieSetSortTokens.AddRange(lstMovieSetSortTokens.Items.OfType(Of String).ToList)
            If .MovieSetSortTokens.Count <= 0 Then .MovieSetSortTokensIsEmpty = True
            .MovieThemeEnable = Me.chkMovieThemeEnable.Checked
            .MovieThemeOverwrite = Me.chkMovieThemeOverwrite.Checked
            .MovieTrailerDefaultSearch = Me.txtMovieTrailerDefaultSearch.Text
            .MovieTrailerDeleteExisting = Me.chkMovieTrailerDeleteExisting.Checked
            .MovieTrailerEnable = Me.chkMovieTrailerEnable.Checked
            .MovieTrailerOverwrite = Me.chkMovieTrailerOverwrite.Checked
            If Me.cbMovieTrailerMinVideoQual.SelectedValue IsNot Nothing Then
                .MovieTrailerMinVideoQual = CType(Me.cbMovieTrailerMinVideoQual.SelectedValue, Enums.TrailerVideoQuality)
            End If
            If Me.cbMovieTrailerPrefVideoQual.SelectedValue IsNot Nothing Then
                .MovieTrailerPrefVideoQual = CType(Me.cbMovieTrailerPrefVideoQual.SelectedValue, Enums.TrailerVideoQuality)
            End If
            .TVASBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVASBannerHeight.Text), Convert.ToInt32(Me.txtTVASBannerHeight.Text), 0)
            .TVASBannerOverwrite = Me.chkTVASBannerOverwrite.Checked
            If Me.cbTVASBannerPrefType.SelectedValue IsNot Nothing Then
                .TVASBannerPrefType = CType(Me.cbTVASBannerPrefType.SelectedValue, Enums.TVShowBannerType)
            End If
            .TVASBannerResize = Me.chkTVASBannerResize.Checked
            .TVASBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVASBannerWidth.Text), Convert.ToInt32(Me.txtTVASBannerWidth.Text), 0)
            .TVASFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVASFanartHeight.Text), Convert.ToInt32(Me.txtTVASFanartHeight.Text), 0)
            .TVASFanartOverwrite = Me.chkTVASFanartOverwrite.Checked
            If Me.cbTVASFanartPrefSize.SelectedValue IsNot Nothing Then
                .TVASFanartPrefSize = CType(Me.cbTVASFanartPrefSize.SelectedValue, Enums.TVFanartSize)
            End If
            .TVASFanartResize = Me.chkTVASFanartResize.Checked
            .TVASFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVASFanartWidth.Text), Convert.ToInt32(Me.txtTVASFanartWidth.Text), 0)
            .TVASLandscapeOverwrite = Me.chkTVASLandscapeOverwrite.Checked
            .TVASPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVASPosterHeight.Text), Convert.ToInt32(Me.txtTVASPosterHeight.Text), 0)
            .TVASPosterOverwrite = Me.chkTVASPosterOverwrite.Checked
            If Me.cbTVASPosterPrefSize.SelectedValue IsNot Nothing Then
                .TVASPosterPrefSize = CType(Me.cbTVASPosterPrefSize.SelectedValue, Enums.TVPosterSize)
            End If
            .TVASPosterResize = Me.chkTVASPosterResize.Checked
            .TVASPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVASPosterWidth.Text), Convert.ToInt32(Me.txtTVASPosterWidth.Text), 0)
            .TVCleanDB = Me.chkTVCleanDB.Checked
            .TVDisplayMissingEpisodes = Me.chkTVDisplayMissingEpisodes.Checked
            .TVDisplayStatus = Me.chkTVDisplayStatus.Checked
            .TVEpisodeFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVEpisodeFanartHeight.Text), Convert.ToInt32(Me.txtTVEpisodeFanartHeight.Text), 0)
            .TVEpisodeFanartOverwrite = Me.chkTVEpisodeFanartOverwrite.Checked
            If Me.cbTVEpisodeFanartPrefSize.SelectedValue IsNot Nothing Then
                .TVEpisodeFanartPrefSize = CType(Me.cbTVEpisodeFanartPrefSize.SelectedValue, Enums.TVFanartSize)
            End If
            .TVEpisodeFanartResize = Me.chkTVEpisodeFanartResize.Checked
            .TVEpisodeFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVEpisodeFanartWidth.Text), Convert.ToInt32(Me.txtTVEpisodeFanartWidth.Text), 0)
            .TVEpisodeFilterCustom.Clear()
            .TVEpisodeFilterCustom.AddRange(Me.lstTVEpisodeFilter.Items.OfType(Of String).ToList)
            If .TVEpisodeFilterCustom.Count <= 0 Then .TVEpisodeFilterCustomIsEmpty = True
            .TVEpisodeNoFilter = Me.chkTVEpisodeNoFilter.Checked
            .TVEpisodePosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVEpisodePosterHeight.Text), Convert.ToInt32(Me.txtTVEpisodePosterHeight.Text), 0)
            .TVEpisodePosterOverwrite = Me.chkTVEpisodePosterOverwrite.Checked
            .TVEpisodePosterResize = Me.chkTVEpisodePosterResize.Checked
            .TVEpisodePosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVEpisodePosterWidth.Text), Convert.ToInt32(Me.txtTVEpisodePosterWidth.Text), 0)
            .TVEpisodeProperCase = Me.chkTVEpisodeProperCase.Checked
            .TVGeneralEpisodeListSorting.Clear()
            .TVGeneralEpisodeListSorting.AddRange(Me.TVGeneralEpisodeListSorting)
            .TVGeneralFlagLang = If(Me.cbTVLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, Me.cbTVLanguageOverlay.Text)
            .TVGeneralIgnoreLastScan = Me.chkTVGeneralIgnoreLastScan.Checked
            'cocotus, 2014/05/21 Fixed: If cbTVGeneralLang.Text is empty it will crash here -> no AdvancedSettings.xml will be built/saved!!(happens when user has not yet set TVLanguage via Fetch language button!)
            'old:    .TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.LongLang = cbTVGeneralLang.Text).ShortLang
            If cbTVGeneralLang.Text <> String.Empty Then
                .TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbTVGeneralLang.Text).abbreviation
            End If
            .TVGeneralMarkNewEpisodes = Me.chkTVGeneralMarkNewEpisodes.Checked
            .TVGeneralMarkNewShows = Me.chkTVGeneralMarkNewShows.Checked
            .TVGeneralSeasonListSorting.Clear()
            .TVGeneralSeasonListSorting.AddRange(Me.TVGeneralSeasonListSorting)
            .TVGeneralShowListSorting.Clear()
            .TVGeneralShowListSorting.AddRange(Me.TVGeneralShowListSorting)
            .TVLockEpisodePlot = Me.chkTVLockEpisodePlot.Checked
            .TVLockEpisodeRating = Me.chkTVLockEpisodeRating.Checked
            .TVLockEpisodeRuntime = Me.chkTVLockEpisodeRuntime.Checked
            .TVLockEpisodeTitle = Me.chkTVLockEpisodeTitle.Checked
            .TVLockEpisodeVotes = Me.chkTVLockEpisodeVotes.Checked
            .TVLockShowGenre = Me.chkTVLockShowGenre.Checked
            .TVLockShowPlot = Me.chkTVLockShowPlot.Checked
            .TVLockShowRating = Me.chkTVLockShowRating.Checked
            .TVLockShowRuntime = Me.chkTVLockShowRuntime.Checked
            .TVLockShowStatus = Me.chkTVLockShowStatus.Checked
            .TVLockShowStudio = Me.chkTVLockShowStudio.Checked
            .TVLockShowTitle = Me.chkTVLockShowTitle.Checked
            .TVLockShowVotes = Me.chkTVLockShowVotes.Checked
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
            .TVScraperEpisodeRuntime = Me.chkTVScraperEpisodeRuntime.Checked
            .TVScraperEpisodeSeason = Me.chkTVScraperEpisodeSeason.Checked
            .TVScraperEpisodeTitle = Me.chkTVScraperEpisodeTitle.Checked
            .TVScraperEpisodeVotes = Me.chkTVScraperEpisodeVotes.Checked
            .TVScraperMetaDataScan = Me.chkTVScraperMetaDataScan.Checked
            If Me.cbTVScraperOptionsOrdering.SelectedValue IsNot Nothing Then
                .TVScraperOptionsOrdering = CType(Me.cbTVScraperOptionsOrdering.SelectedValue, Enums.Ordering)
            End If
            If String.IsNullOrEmpty(Me.cbTVScraperRatingRegion.Text) Then
                .TVScraperRatingRegion = "usa"
            Else
                .TVScraperRatingRegion = Me.cbTVScraperRatingRegion.Text
            End If
            .TVScraperShowActors = Me.chkTVScraperShowActors.Checked
            .TVScraperShowEpiGuideURL = Me.chkTVScraperShowEpiGuideURL.Checked
            .TVScraperShowGenre = Me.chkTVScraperShowGenre.Checked
            .TVScraperShowMPAA = Me.chkTVScraperShowMPAA.Checked
            .TVScraperShowMPAANotRated = Me.txtTVScraperShowMPAANotRated.Text
            .TVScraperShowPlot = Me.chkTVScraperShowPlot.Checked
            .TVScraperShowPremiered = Me.chkTVScraperShowPremiered.Checked
            .TVScraperShowRating = Me.chkTVScraperShowRating.Checked
            .TVScraperShowRuntime = Me.chkTVScraperShowRuntime.Checked
            .TVScraperShowStatus = Me.chkTVScraperShowStatus.Checked
            .TVScraperShowStudio = Me.chkTVScraperShowStudio.Checked
            .TVScraperShowTitle = Me.chkTVScraperShowTitle.Checked
            .TVScraperShowVotes = Me.chkTVScraperShowVotes.Checked
            If Me.cbTVScraperUpdateTime.SelectedValue IsNot Nothing Then
                .TVScraperUpdateTime = CType(Me.cbTVScraperUpdateTime.SelectedValue, Enums.TVScraperUpdateTime)
            End If
            .TVScraperUseMDDuration = Me.chkTVScraperUseMDDuration.Checked
            .TVScraperUseSRuntimeForEp = Me.chkTVScraperUseSRuntimeForEp.Checked
            .TVSeasonBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonBannerHeight.Text), Convert.ToInt32(Me.txtTVSeasonBannerHeight.Text), 0)
            .TVSeasonBannerOverwrite = Me.chkTVSeasonBannerOverwrite.Checked
            If Me.cbTVSeasonBannerPrefType.SelectedValue IsNot Nothing Then
                .TVSeasonBannerPrefType = CType(Me.cbTVSeasonBannerPrefType.SelectedValue, Enums.TVSeasonBannerType)
            End If
            .TVSeasonBannerResize = Me.chkTVSeasonBannerResize.Checked
            .TVSeasonBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonBannerWidth.Text), Convert.ToInt32(Me.txtTVSeasonBannerWidth.Text), 0)
            .TVSeasonFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonFanartHeight.Text), Convert.ToInt32(Me.txtTVSeasonFanartHeight.Text), 0)
            .TVSeasonFanartOverwrite = Me.chkTVSeasonFanartOverwrite.Checked
            If Me.cbTVSeasonFanartPrefSize.SelectedValue IsNot Nothing Then
                .TVSeasonFanartPrefSize = CType(Me.cbTVSeasonFanartPrefSize.SelectedValue, Enums.TVFanartSize)
            End If
            .TVSeasonFanartResize = Me.chkTVSeasonFanartResize.Checked
            .TVSeasonFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonFanartWidth.Text), Convert.ToInt32(Me.txtTVSeasonFanartWidth.Text), 0)
            .TVSeasonLandscapeOverwrite = Me.chkTVSeasonLandscapeOverwrite.Checked
            .TVSeasonPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonPosterHeight.Text), Convert.ToInt32(Me.txtTVSeasonPosterHeight.Text), 0)
            .TVSeasonPosterOverwrite = Me.chkTVSeasonPosterOverwrite.Checked
            If Me.cbTVSeasonPosterPrefSize.SelectedValue IsNot Nothing Then
                .TVSeasonPosterPrefSize = CType(Me.cbTVSeasonPosterPrefSize.SelectedValue, Enums.TVPosterSize)
            End If
            If Me.cbTVEpisodePosterPrefSize.SelectedValue IsNot Nothing Then
                .TVEpisodePosterPrefSize = CType(Me.cbTVEpisodePosterPrefSize.SelectedValue, Enums.TVEpisodePosterSize)
            End If
            .TVSeasonPosterResize = Me.chkTVSeasonPosterResize.Checked
            .TVSeasonPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonPosterWidth.Text), Convert.ToInt32(Me.txtTVSeasonPosterWidth.Text), 0)
            .TVShowBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowBannerHeight.Text), Convert.ToInt32(Me.txtTVShowBannerHeight.Text), 0)
            .TVShowBannerOverwrite = Me.chkTVShowBannerOverwrite.Checked
            If Me.cbTVShowBannerPrefType.SelectedValue IsNot Nothing Then
                .TVShowBannerPrefType = CType(Me.cbTVShowBannerPrefType.SelectedValue, Enums.TVShowBannerType)
            End If
            .TVShowBannerResize = Me.chkTVShowBannerResize.Checked
            .TVShowBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowBannerWidth.Text), Convert.ToInt32(Me.txtTVShowBannerWidth.Text), 0)
            .TVShowCharacterArtOverwrite = Me.chkTVShowCharacterArtOverwrite.Checked
            .TVShowClearArtOverwrite = Me.chkTVShowClearArtOverwrite.Checked
            .TVShowClearLogoOverwrite = Me.chkTVShowClearLogoOverwrite.Checked
            .TVShowEFanartsHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowEFanartsHeight.Text), Convert.ToInt32(Me.txtTVShowEFanartsHeight.Text), 0)
            .TVShowEFanartsLimit = If(Not String.IsNullOrEmpty(Me.txtTVShowEFanartsLimit.Text), Convert.ToInt32(Me.txtTVShowEFanartsLimit.Text), 0)
            .TVShowEFanartsOverwrite = Me.chkTVShowEFanartsOverwrite.Checked
            .TVShowEFanartsPrefOnly = Me.chkTVShowEFanartsPrefOnly.Checked
            If Me.cbTVShowEFanartsPrefSize.SelectedValue IsNot Nothing Then
                .TVShowEFanartsPrefSize = CType(Me.cbTVShowEFanartsPrefSize.SelectedValue, Enums.TVFanartSize)
            End If
            .TVShowEFanartsResize = Me.chkTVShowEFanartsResize.Checked
            .TVShowEFanartsWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowEFanartsWidth.Text), Convert.ToInt32(Me.txtTVShowEFanartsWidth.Text), 0)
            .TVShowFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowFanartHeight.Text), Convert.ToInt32(Me.txtTVShowFanartHeight.Text), 0)
            .TVShowFanartOverwrite = Me.chkTVShowFanartOverwrite.Checked
            If Me.cbTVShowFanartPrefSize.SelectedValue IsNot Nothing Then
                .TVShowFanartPrefSize = CType(Me.cbTVShowFanartPrefSize.SelectedValue, Enums.TVFanartSize)
            End If
            .TVShowFanartResize = Me.chkTVShowFanartResize.Checked
            .TVShowFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowFanartWidth.Text), Convert.ToInt32(Me.txtTVShowFanartWidth.Text), 0)
            .TVShowFilterCustom.Clear()
            .TVShowFilterCustom.AddRange(Me.lstTVShowFilter.Items.OfType(Of String).ToList)
            If .TVShowFilterCustom.Count <= 0 Then .TVShowFilterCustomIsEmpty = True
            .TVShowLandscapeOverwrite = Me.chkTVShowLandscapeOverwrite.Checked
            .TVShowPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowPosterHeight.Text), Convert.ToInt32(Me.txtTVShowPosterHeight.Text), 0)
            .TVShowPosterOverwrite = Me.chkTVShowPosterOverwrite.Checked
            If Me.cbTVShowPosterPrefSize.SelectedValue IsNot Nothing Then
                .TVShowPosterPrefSize = CType(Me.cbTVShowPosterPrefSize.SelectedValue, Enums.TVPosterSize)
            End If
            .TVShowPosterResize = Me.chkTVShowPosterResize.Checked
            .TVShowPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowPosterWidth.Text), Convert.ToInt32(Me.txtTVShowPosterWidth.Text), 0)
            .TVShowProperCase = Me.chkTVShowProperCase.Checked
            .TVShowRegexes.Clear()
            .TVShowRegexes.AddRange(Me.TVShowRegex)
            If Not String.IsNullOrEmpty(Me.txtTVSkipLessThan.Text) AndAlso IsNumeric(Me.txtTVSkipLessThan.Text) Then
                .TVSkipLessThan = Convert.ToInt32(Me.txtTVSkipLessThan.Text)
            Else
                .TVSkipLessThan = 0
            End If
            .TVSortTokens.Clear()
            .TVSortTokens.AddRange(lstTVSortTokens.Items.OfType(Of String).ToList)
            If .TVSortTokens.Count <= 0 Then .TVSortTokensIsEmpty = True

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

            SaveMovieSetScraperTitleRenamer()

            If Not String.IsNullOrEmpty(Me.txtProxyURI.Text) AndAlso Not String.IsNullOrEmpty(Me.txtProxyPort.Text) Then
                .ProxyURI = Me.txtProxyURI.Text
                .ProxyPort = Convert.ToInt32(Me.txtProxyPort.Text)

                If Not String.IsNullOrEmpty(Me.txtProxyUsername.Text) AndAlso Not String.IsNullOrEmpty(Me.txtProxyPassword.Text) Then
                    .ProxyCredentials.UserName = Me.txtProxyUsername.Text
                    .ProxyCredentials.Password = Me.txtProxyPassword.Text
                    .ProxyCredentials.Domain = Me.txtProxyDomain.Text
                Else
                    .ProxyCredentials = New NetworkCredential
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
            .MovieExtrafanartsFrodo = Me.chkMovieExtrafanartsFrodo.Checked
            .MovieExtrathumbsFrodo = Me.chkMovieExtrathumbsFrodo.Checked
            .MovieFanartFrodo = Me.chkMovieFanartFrodo.Checked
            .MovieNFOFrodo = Me.chkMovieNFOFrodo.Checked
            .MoviePosterFrodo = Me.chkMoviePosterFrodo.Checked
            .MovieTrailerFrodo = Me.chkMovieTrailerFrodo.Checked

            '*************** XBMC Eden settings ***************
            .MovieUseEden = Me.chkMovieUseEden.Checked
            .MovieActorThumbsEden = Me.chkMovieActorThumbsEden.Checked
            .MovieExtrafanartsEden = Me.chkMovieExtrafanartsEden.Checked
            .MovieExtrathumbsEden = Me.chkMovieExtrathumbsEden.Checked
            .MovieFanartEden = Me.chkMovieFanartEden.Checked
            .MovieNFOEden = Me.chkMovieNFOEden.Checked
            .MoviePosterEden = Me.chkMoviePosterEden.Checked
            .MovieTrailerEden = Me.chkMovieTrailerEden.Checked

            '************* XBMC optional settings *************
            .MovieXBMCProtectVTSBDMV = Me.chkMovieXBMCProtectVTSBDMV.Checked

            '******** XBMC ArtworkDownloader settings **********
            .MovieBannerAD = Me.chkMovieBannerAD.Checked
            .MovieClearArtAD = Me.chkMovieClearArtAD.Checked
            .MovieClearLogoAD = Me.chkMovieClearLogoAD.Checked
            .MovieDiscArtAD = Me.chkMovieDiscArtAD.Checked
            .MovieLandscapeAD = Me.chkMovieLandscapeAD.Checked

            '************** XBMC TvTunes settings **************
            .MovieXBMCThemeCustom = Me.chkMovieXBMCThemeCustom.Checked
            .MovieXBMCThemeCustomPath = Me.txtMovieXBMCThemeCustomPath.Text
            .MovieXBMCThemeEnable = Me.chkMovieXBMCThemeEnable.Checked
            .MovieXBMCThemeMovie = Me.chkMovieXBMCThemeMovie.Checked
            .MovieXBMCThemeSub = Me.chkMovieXBMCThemeSub.Checked
            .MovieXBMCThemeSubDir = Me.txtMovieXBMCThemeSubDir.Text

            '****************** YAMJ settings *****************
            .MovieUseYAMJ = Me.chkMovieUseYAMJ.Checked
            .MovieBannerYAMJ = Me.chkMovieBannerYAMJ.Checked
            .MovieFanartYAMJ = Me.chkMovieFanartYAMJ.Checked
            .MovieNFOYAMJ = Me.chkMovieNFOYAMJ.Checked
            .MoviePosterYAMJ = Me.chkMoviePosterYAMJ.Checked
            .MovieTrailerYAMJ = Me.chkMovieTrailerYAMJ.Checked

            '****************** NMJ settings *****************
            .MovieUseNMJ = Me.chkMovieUseNMJ.Checked
            .MovieBannerNMJ = Me.chkMovieBannerNMJ.Checked
            .MovieFanartNMJ = Me.chkMovieFanartNMJ.Checked
            .MovieNFONMJ = Me.chkMovieNFONMJ.Checked
            .MoviePosterNMJ = Me.chkMoviePosterNMJ.Checked
            .MovieTrailerNMJ = Me.chkMovieTrailerNMJ.Checked

            '************** NMJ optional settings *************
            .MovieYAMJCompatibleSets = Me.chkMovieYAMJCompatibleSets.Checked
            .MovieYAMJWatchedFile = Me.chkMovieYAMJWatchedFile.Checked
            .MovieYAMJWatchedFolder = Me.txtMovieYAMJWatchedFolder.Text

            '***************** Boxee settings *****************
            .MovieUseBoxee = Me.chkMovieUseBoxee.Checked
            .MovieFanartBoxee = Me.chkMovieFanartBoxee.Checked
            .MovieNFOBoxee = Me.chkMovieNFOBoxee.Checked
            .MoviePosterBoxee = Me.chkMoviePosterBoxee.Checked

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
            '****************** MovieSet Part ******************
            '***************************************************

            '**************** XBMC MSAA settings ***************
            .MovieSetUseMSAA = Me.chkMovieSetUseMSAA.Checked
            .MovieSetBannerMSAA = Me.chkMovieSetBannerMSAA.Checked
            .MovieSetClearArtMSAA = Me.chkMovieSetClearArtMSAA.Checked
            .MovieSetClearLogoMSAA = Me.chkMovieSetClearLogoMSAA.Checked
            .MovieSetFanartMSAA = Me.chkMovieSetFanartMSAA.Checked
            .MovieSetLandscapeMSAA = Me.chkMovieSetLandscapeMSAA.Checked
            .MovieSetNFOMSAA = Me.chkMovieSetNFOMSAA.Checked
            .MovieSetPathMSAA = Me.txtMovieSetPathMSAA.Text
            .MovieSetPosterMSAA = Me.chkMovieSetPosterMSAA.Checked

            '***************** Expert settings ****************
            .MovieSetUseExpert = Me.chkMovieSetUseExpert.Checked

            '***************** Expert Single ******************
            .MovieSetBannerExpertSingle = Me.txtMovieSetBannerExpertSingle.Text
            .MovieSetClearArtExpertSingle = Me.txtMovieSetClearArtExpertSingle.Text
            .MovieSetClearLogoExpertSingle = Me.txtMovieSetClearLogoExpertSingle.Text
            .MovieSetDiscArtExpertSingle = Me.txtMovieSetDiscArtExpertSingle.Text
            .MovieSetFanartExpertSingle = Me.txtMovieSetFanartExpertSingle.Text
            .MovieSetLandscapeExpertSingle = Me.txtMovieSetLandscapeExpertSingle.Text
            .MovieSetNFOExpertSingle = Me.txtMovieSetNFOExpertSingle.Text
            .MovieSetPathExpertSingle = Me.txtMovieSetPathExpertSingle.Text
            .MovieSetPosterExpertSingle = Me.txtMovieSetPosterExpertSingle.Text

            '***************** Expert Parent ******************
            .MovieSetBannerExpertParent = Me.txtMovieSetBannerExpertParent.Text
            .MovieSetClearArtExpertParent = Me.txtMovieSetClearArtExpertParent.Text
            .MovieSetClearLogoExpertParent = Me.txtMovieSetClearLogoExpertParent.Text
            .MovieSetDiscArtExpertParent = Me.txtMovieSetDiscArtExpertParent.Text
            .MovieSetFanartExpertParent = Me.txtMovieSetFanartExpertParent.Text
            .MovieSetLandscapeExpertParent = Me.txtMovieSetLandscapeExpertParent.Text
            .MovieSetNFOExpertParent = Me.txtMovieSetNFOExpertParent.Text
            .MovieSetPosterExpertParent = Me.txtMovieSetPosterExpertParent.Text


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
            .TVSeasonLandscapeAD = Me.chkTVSeasonLandscapeAD.Checked
            .TVShowCharacterArtAD = Me.chkTVShowCharacterArtAD.Checked
            .TVShowClearArtAD = Me.chkTVShowClearArtAD.Checked
            .TVShowClearLogoAD = Me.chkTVShowClearLogoAD.Checked
            .TVShowExtrafanartsFrodo = Me.chkTVShowExtrafanartsFrodo.Checked
            .TVShowLandscapeAD = Me.chkTVShowLandscapeAD.Checked
            .TVShowTVThemeXBMC = Me.chkTVXBMCThemeEnable.Checked
            .TVShowTVThemeFolderXBMC = Me.txtTVXBMCThemeCustomPath.Text

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
                .MovieBannerAD = True
                .MovieClearArtAD = True
                .MovieClearLogoAD = True
                .MovieDiscArtAD = True
                .MovieExtrafanartsFrodo = True
                .MovieExtrathumbsFrodo = True
                .MovieFanartFrodo = True
                .MovieLandscapeAD = True
                .MovieNFOFrodo = True
                .MoviePosterFrodo = True
                .MovieTrailerFrodo = True
            End If

            'Default to Frodo for tvshows
            'TODO

        End With

        For Each s As ModulesManager._externalScraperModuleClass_Data_Movie In ModulesManager.Instance.externalScrapersModules_Data_Movie
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Data_MovieSet In ModulesManager.Instance.externalScrapersModules_Data_MovieSet
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In ModulesManager.Instance.externalScrapersModules_Image_Movie
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In ModulesManager.Instance.externalScrapersModules_Image_MovieSet
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Image_TV In ModulesManager.Instance.externalScrapersModules_Image_TV
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In ModulesManager.Instance.externalScrapersModules_Theme_Movie
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In ModulesManager.Instance.externalScrapersModules_Trailer_Movie
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_TV In ModulesManager.Instance.externalScrapersModules_TV
            Try
                If s.ProcessorModule.IsScraper Then s.ProcessorModule.SaveSetupScraper(Not isApply)
                If s.ProcessorModule.IsPostScraper Then s.ProcessorModule.SaveSetupPosterScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV
            Try
                s.ProcessorModule.SaveSetupScraper(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalProcessorModules
            Try
                s.ProcessorModule.SaveSetup(Not isApply)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
        ModulesManager.Instance.SaveSettings()
        Functions.CreateDefaultOptions()
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

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        Me.chkMovieActorThumbsExpertBDMV.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertMulti.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertSingle.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertVTS.Text = strActorThumbs
        Me.gbMovieImagesActorThumbsOpts.Text = strActorThumbs
        Me.lblMovieSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs
        Me.lblTVSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs

        'Ask On Click Scrape
        Dim strAskOnClickScrape As String = Master.eLang.GetString(852, "Ask On Click Scrape")
        Me.chkMovieClickScrapeAsk.Text = strAskOnClickScrape
        Me.chkMovieSetClickScrapeAsk.Text = strAskOnClickScrape

        'Automatically Resize:
        Dim strAutomaticallyResize As String = Master.eLang.GetString(481, "Automatically Resize:")
        Me.chkMoviePosterResize.Text = strAutomaticallyResize
        Me.chkMovieBannerResize.Text = strAutomaticallyResize
        Me.chkMovieEFanartsResize.Text = strAutomaticallyResize
        Me.chkMovieEThumbsResize.Text = strAutomaticallyResize
        Me.chkMovieFanartResize.Text = strAutomaticallyResize
        Me.chkMovieSetBannerResize.Text = strAutomaticallyResize
        Me.chkMovieSetFanartResize.Text = strAutomaticallyResize
        Me.chkMovieSetPosterResize.Text = strAutomaticallyResize
        Me.chkTVASBannerResize.Text = strAutomaticallyResize
        Me.chkTVASFanartResize.Text = strAutomaticallyResize
        Me.chkTVASPosterResize.Text = strAutomaticallyResize
        Me.chkTVEpisodeFanartResize.Text = strAutomaticallyResize
        Me.chkTVEpisodePosterResize.Text = strAutomaticallyResize
        Me.chkTVSeasonBannerResize.Text = strAutomaticallyResize
        Me.chkTVSeasonFanartResize.Text = strAutomaticallyResize
        Me.chkTVSeasonPosterResize.Text = strAutomaticallyResize
        Me.chkTVShowBannerResize.Text = strAutomaticallyResize
        Me.chkTVShowEFanartsResize.Text = strAutomaticallyResize
        Me.chkTVShowFanartResize.Text = strAutomaticallyResize
        Me.chkTVShowPosterResize.Text = strAutomaticallyResize

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        Me.gbMovieImagesBannerOpts.Text = strBanner
        Me.gbMovieSetImagesBannerOpts.Text = strBanner
        Me.gbTVImagesAllSeasonsBannerOpts.Text = strBanner
        Me.gbTVImagesSeasonBannerOpts.Text = strBanner
        Me.gbTVImagesShowBannerOpts.Text = strBanner
        Me.lblMovieSourcesFileNamingExpertBDMVBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingExpertMultiBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingExpertSingleBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingExpertVTSBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingNMTDefaultsBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingXBMCADBanner.Text = strBanner
        Me.lblTVSourcesFileNamingBoxeeDefaultsBanner.Text = strBanner
        Me.lblTVSourcesFileNamingNMTDefaultsBanner.Text = strBanner
        Me.lblTVSourcesFileNamingXBMCDefaultsBanner.Text = strBanner

        'Cast
        Dim strCast As String = Master.eLang.GetString(63, "Cast")
        Me.lblMovieScraperGlobalCast.Text = strCast

        'Certification
        Dim strCertification As String = Master.eLang.GetString(722, "Certification")
        Me.gbMovieScraperCertificationOpts.Text = strCertification
        Me.lblMovieScraperGlobalCertification.Text = strCertification

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        Me.gbTVImagesShowCharacterArtOpts.Text = strCharacterArt

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        Me.gbMovieImagesClearArtOpts.Text = strClearArt
        Me.gbMovieSetImagesClearArtOpts.Text = strClearArt
        Me.gbTVImagesShowClearArtOpts.Text = strClearArt
        Me.lblMovieClearArtExpertBDMV.Text = strClearArt
        Me.lblMovieClearArtExpertMulti.Text = strClearArt
        Me.lblMovieClearArtExpertSingle.Text = strClearArt
        Me.lblMovieClearArtExpertVTS.Text = strClearArt
        Me.lblMovieSourcesFileNamingXBMCADClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        Me.gbMovieImagesClearLogoOpts.Text = strClearLogo
        Me.gbMovieSetImagesClearLogoOpts.Text = strClearLogo
        Me.gbTVImagesShowClearLogoOpts.Text = strClearLogo
        Me.lblMovieClearLogoExpertBDMV.Text = strClearLogo
        Me.lblMovieClearLogoExpertMulti.Text = strClearLogo
        Me.lblMovieClearLogoExpertSingle.Text = strClearLogo
        Me.lblMovieClearLogoExpertVTS.Text = strClearLogo
        Me.lblMovieSourcesFileNamingXBMCADClearLogo.Text = strClearLogo

        'Collection ID
        Dim strCollectionID As String = Master.eLang.GetString(1135, "Collection ID")
        Me.lblMovieScraperGlobalCollectionID.Text = strCollectionID

        'Collections
        Dim strCollections As String = Master.eLang.GetString(424, "Collections")
        Me.lblMovieScraperGlobalCollections.Text = strCollections

        'Column
        Dim strColumn As String = Master.eLang.GetString(1331, "Column")
        Me.colMovieGeneralMediaListSortingLabel.Text = strColumn
        Me.colMovieSetGeneralMediaListSortingLabel.Text = strColumn
        Me.colTVGeneralEpisodeListSortingLabel.Text = strColumn
        Me.colTVGeneralSeasonListSortingLabel.Text = strColumn
        Me.colTVGeneralShowListSortingLabel.Text = strColumn

        'Country
        Dim strCountry As String = Master.eLang.GetString(301, "Country")
        Me.lblMovieScraperGlobalCountry.Text = strCountry

        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        Me.gbMovieSourcesFileNamingBoxeeDefaultsOpts.Text = strDefaults
        Me.gbMovieSourcesFileNamingNMTDefaultsOpts.Text = strDefaults
        Me.gbMovieSourcesFileNamingXBMCDefaultsOpts.Text = strDefaults
        Me.gbTVSourcesDefaultsOpts.Text = strDefaults
        Me.gbTVSourcesFileNamingBoxeeDefaultsOpts.Text = strDefaults
        Me.gbTVSourcesFileNamingNMTDefaultsOpts.Text = strDefaults
        Me.gbTVSourcesFileNamingXBMCDefaultsOpts.Text = strDefaults

        'Defaults by File Type
        Dim strDefaultsByFileType As String = Master.eLang.GetString(625, "Defaults by File Type")
        Me.gbMovieScraperDefFIExtOpts.Text = strDefaultsByFileType
        Me.gbTVScraperDefFIExtOpts.Text = strDefaultsByFileType

        'Director
        Dim strDirector As String = Master.eLang.GetString(62, "Director")
        Me.lblMovieScraperGlobalDirector.Text = strDirector
        Me.lblTVScraperGlobalDirector.Text = strDirector

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        Me.gbMovieImagesDiscArtOpts.Text = strDiscArt
        Me.lblMovieDiscArtExpertBDMV.Text = strDiscArt
        Me.lblMovieDiscArtExpertMulti.Text = strDiscArt
        Me.lblMovieDiscArtExpertSingle.Text = strDiscArt
        Me.lblMovieDiscArtExpertVTS.Text = strDiscArt
        Me.lblMovieSourcesFileNamingXBMCADDiscArt.Text = strDiscArt

        'Duration Format
        Dim strDurationFormat As String = Master.eLang.GetString(515, "Duration Format")
        Me.gbMovieScraperDurationFormatOpts.Text = strDurationFormat
        Me.gbTVScraperDurationFormatOpts.Text = strDurationFormat

        'Duration Runtime Format
        Dim strDurationRuntimeFormat As String = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), vbNewLine)
        Me.lblMovieScraperDurationRuntimeFormat.Text = strDurationRuntimeFormat
        Me.lblTVScraperDurationRuntimeFormat.Text = strDurationRuntimeFormat

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        Me.lblMovieSourcesFileNamingBoxeeDefaultsEnabled.Text = strEnabled
        Me.lblMovieSourcesFileNamingNMTDefaultsEnabled.Text = strEnabled
        Me.lblMovieSourcesFileNamingXBMCDefaultsEnabled.Text = strEnabled
        Me.lblTVSourcesFileNamingBoxeeDefaultsEnabled.Text = strEnabled
        Me.lblTVSourcesFileNamingNMTDefaultsEnabled.Text = strEnabled
        Me.lblTVSourcesFileNamingXBMCDefaultsEnabled.Text = strEnabled
        Me.chkMovieUseExpert.Text = strEnabled

        'Enabled Click Scrape
        Dim strEnabledClickScrape As String = Master.eLang.GetString(849, "Enable Click Scrape")
        Me.chkMovieClickScrape.Text = strEnabledClickScrape
        Me.chkMovieSetClickScrape.Text = strEnabledClickScrape

        'Episode #
        Dim strEpisodeNR As String = Master.eLang.GetString(660, "Episode #")
        Me.lblTVScraperGlobalEpisode.Text = strEpisodeNR

        'Episode List Sorting
        Dim strEpisodeListSorting As String = Master.eLang.GetString(494, "Episode List Sorting")
        Me.gbTVGeneralEpisodeListSorting.Text = strEpisodeListSorting

        'Episodes
        Dim strEpisodes As String = Master.eLang.GetString(682, "Episodes")
        Me.lblTVScraperGlobalHeaderEpisodes.Text = strEpisodes

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        Me.chkMovieExtrafanartsExpertBDMV.Text = strExtrafanarts
        Me.chkMovieExtrafanartsExpertSingle.Text = strExtrafanarts
        Me.chkMovieExtrafanartsExpertVTS.Text = strExtrafanarts
        Me.gbMovieImagesEFanartsOpts.Text = strExtrafanarts
        Me.gbTVImagesShowEFanartsOpts.Text = strExtrafanarts
        Me.lblMovieSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts
        Me.lblTVSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        Me.chkMovieExtrathumbsExpertBDMV.Text = strExtrathumbs
        Me.chkMovieExtrathumbsExpertSingle.Text = strExtrathumbs
        Me.chkMovieExtrathumbsExpertVTS.Text = strExtrathumbs
        Me.gbMovieImagesEThumbsOpts.Text = strExtrathumbs
        Me.lblMovieSourcesFileNamingXBMCDefaultsExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        Me.gbMovieImagesFanartOpts.Text = strFanart
        Me.gbMovieSetImagesFanartOpts.Text = strFanart
        Me.gbTVImagesAllSeasonsFanartOpts.Text = strFanart
        Me.gbTVImagesEpisodeFanartOpts.Text = strFanart
        Me.gbTVImagesSeasonFanartOpts.Text = strFanart
        Me.gbTVImagesShowFanartOpts.Text = strFanart
        Me.lblMovieSourcesFileNamingBoxeeDefaultsFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertBDMVFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertMultiFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertSingleFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertVTSFanart.Text = strFanart
        Me.lblMovieSourcesFileNamingNMTDefaultsFanart.Text = strFanart
        Me.lblMovieSourcesFileNamingXBMCDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFileNamingBoxeeDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFileNamingNMTDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFileNamingXBMCDefaultsFanart.Text = strFanart

        'File Type
        Dim strFileType As String = String.Concat(Master.eLang.GetString(626, "File Type"), ":")
        Me.lblMovieScraperDefFIExt.Text = strFileType
        Me.lblTVScraperDefFIExt.Text = strFileType

        'Genre
        Dim strGenre As String = Master.eLang.GetString(20, "Genre")
        Me.lblMovieScraperGlobalGenre.Text = strGenre
        Me.lblTVScraperGlobalGenre.Text = strGenre

        'Hide
        Dim strHide As String = Master.eLang.GetString(465, "Hide")
        Me.colMovieGeneralMediaListSortingHide.Text = strHide
        Me.colMovieSetGeneralMediaListSortingHide.Text = strHide
        Me.colTVGeneralEpisodeListSortingHide.Text = strHide
        Me.colTVGeneralSeasonListSortingHide.Text = strHide
        Me.colTVGeneralShowListSortingHide.Text = strHide

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        Me.gbMovieImagesLandscapeOpts.Text = strLandscape
        Me.gbMovieSetImagesLandscapeOpts.Text = strLandscape
        Me.gbTVImagesAllSeasonsLandscapeOpts.Text = strLandscape
        Me.gbTVImagesSeasonLandscapeOpts.Text = strLandscape
        Me.gbTVImagesShowLandscapeOpts.Text = strLandscape
        Me.lblMovieSourcesFileNamingExpertBDMVLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingExpertMultiLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingExpertSingleLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingExpertVTSLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingXBMCADLandscape.Text = strLandscape

        'Language (Audio)
        Dim strLanguageAudio As String = Master.eLang.GetString(431, "Language (Audio)")
        Me.lblMovieScraperGlobalLanguageA.Text = strLanguageAudio

        'Language (Video)
        Dim strLanguageVideo As String = Master.eLang.GetString(435, "Language (Video)")
        Me.lblMovieScraperGlobalLanguageV.Text = strLanguageVideo

        'Limit
        Dim strLimit As String = Master.eLang.GetString(578, "Limit")
        Me.lblMovieEFanartsLimit.Text = String.Concat(strLimit, ":")
        Me.lblMovieEThumbsLimit.Text = String.Concat(strLimit, ":")
        Me.lblMovieScraperGlobalHeaderLimit.Text = strLimit
        Me.lblMovieScraperOutlineLimit.Text = String.Concat(strLimit, ":")
        Me.lblTVShowEFanartsLimit.Text = String.Concat(strLimit, ":")

        'Lock
        Dim strLock As String = Master.eLang.GetString(24, "Lock")
        Me.lblMovieScraperGlobalHeaderLock.Text = strLock
        Me.lblMovieSetScraperGlobalHeaderLock.Text = strLock
        Me.lblTVScraperGlobalHeaderShowsLock.Text = strLock
        Me.lblTVScraperGlobalHeaderEpisodesLock.Text = strLock

        'Max Height:
        Dim strMaxHeight As String = Master.eLang.GetString(480, "Max Height:")
        Me.lblMovieBannerHeight.Text = strMaxHeight
        Me.lblMovieEFanartsHeight.Text = strMaxHeight
        Me.lblMovieEThumbsHeight.Text = strMaxHeight
        Me.lblMovieFanartHeight.Text = strMaxHeight
        Me.lblMoviePosterHeight.Text = strMaxHeight
        Me.lblMovieSetBannerHeight.Text = strMaxHeight
        Me.lblMovieSetFanartHeight.Text = strMaxHeight
        Me.lblMovieSetPosterHeight.Text = strMaxHeight
        Me.lblTVASBannerHeight.Text = strMaxHeight
        Me.lblTVASFanartHeight.Text = strMaxHeight
        Me.lblTVASPosterHeight.Text = strMaxHeight
        Me.lblTVEpisodeFanartHeight.Text = strMaxHeight
        Me.lblTVEpisodePosterHeight.Text = strMaxHeight
        Me.lblTVSeasonBannerHeight.Text = strMaxHeight
        Me.lblTVSeasonFanartHeight.Text = strMaxHeight
        Me.lblTVSeasonPosterHeight.Text = strMaxHeight
        Me.lblTVShowBannerHeight.Text = strMaxHeight
        Me.lblTVShowEFanartsHeight.Text = strMaxHeight
        Me.lblTVShowFanartHeight.Text = strMaxHeight
        Me.lblTVShowPosterHeight.Text = strMaxHeight

        'Max Height:
        Dim strMaxWidth As String = Master.eLang.GetString(479, "Max Width:")
        Me.lblMovieBannerWidth.Text = strMaxWidth
        Me.lblMovieEFanartsWidth.Text = strMaxWidth
        Me.lblMovieEThumbsWidth.Text = strMaxWidth
        Me.lblMovieFanartWidth.Text = strMaxWidth
        Me.lblMoviePosterWidth.Text = strMaxWidth
        Me.lblMovieSetBannerWidth.Text = strMaxWidth
        Me.lblMovieSetFanartWidth.Text = strMaxWidth
        Me.lblMovieSetPosterWidth.Text = strMaxWidth
        Me.lblTVASBannerWidth.Text = strMaxWidth
        Me.lblTVASFanartWidth.Text = strMaxWidth
        Me.lblTVASPosterWidth.Text = strMaxWidth
        Me.lblTVEpisodeFanartWidth.Text = strMaxWidth
        Me.lblTVEpisodePosterWidth.Text = strMaxWidth
        Me.lblTVSeasonBannerWidth.Text = strMaxWidth
        Me.lblTVSeasonFanartWidth.Text = strMaxWidth
        Me.lblTVSeasonPosterWidth.Text = strMaxWidth
        Me.lblTVShowBannerWidth.Text = strMaxWidth
        Me.lblTVShowEFanartsWidth.Text = strMaxWidth
        Me.lblTVShowFanartWidth.Text = strMaxWidth
        Me.lblTVShowPosterWidth.Text = strMaxWidth

        'Meta Data
        Dim strMetaData As String = Master.eLang.GetString(59, "Meta Data")
        Me.gbMovieScraperMetaDataOpts.Text = strMetaData
        Me.gbTVScraperMetaDataOpts.Text = strMetaData

        'Miscellaneous
        Dim strMiscellaneous As String = Master.eLang.GetString(429, "Miscellaneous")
        Me.gbGeneralMiscOpts.Text = strMiscellaneous
        Me.gbMovieGeneralMiscOpts.Text = strMiscellaneous
        Me.gbMovieScraperMiscOpts.Text = strMiscellaneous
        Me.gbMovieSetGeneralMiscOpts.Text = strMiscellaneous
        Me.gbMovieSetSourcesMiscOpts.Text = strMiscellaneous
        Me.gbMovieSourcesMiscOpts.Text = strMiscellaneous
        Me.gbTVGeneralMiscOpts.Text = strMiscellaneous
        Me.gbTVScraperMiscOpts.Text = strMiscellaneous
        Me.gbTVSourcesMiscOpts.Text = strMiscellaneous

        'Missing
        Dim strMissing As String = Master.eLang.GetString(582, "Missing")

        'MPAA
        Dim strMPAA As String = Master.eLang.GetString(401, "MPAA")
        Me.lblMovieScraperGlobalMPAA.Text = strMPAA

        'Movie List Sorting
        Dim strMovieListSorting As String = Master.eLang.GetString(490, "Movie List Sorting")
        Me.gbMovieGeneralMediaListSorting.Text = strMovieListSorting

        'MovieSet List Sorting
        Dim strMovieSetListSorting As String = Master.eLang.GetString(491, "MovieSet List Sorting")
        Me.gbMovieSetGeneralMediaListSorting.Text = strMovieSetListSorting

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        Me.lblMovieSourcesFileNamingBoxeeDefaultsNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertBDMVNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertMultiNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertSingleNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertVTSNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingNMTDefaultsNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingXBMCDefaultsNFO.Text = strNFO

        'Only
        Dim strOnly As String = Master.eLang.GetString(145, "Only")
        Me.chkMovieBannerPrefOnly.Text = strOnly
        Me.chkMovieEFanartsPrefOnly.Text = strOnly
        Me.chkMovieEThumbsPrefOnly.Text = strOnly
        Me.chkMovieFanartPrefOnly.Text = strOnly
        Me.chkMoviePosterPrefOnly.Text = strOnly
        Me.chkMovieSetBannerPrefOnly.Text = strOnly
        Me.chkMovieSetFanartPrefOnly.Text = strOnly
        Me.chkMovieSetPosterPrefOnly.Text = strOnly
        Me.chkTVShowEFanartsPrefOnly.Text = strOnly

        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        Me.gbMovieSourcesFileNamingExpertBDMVImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertMultiImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertSingleImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertVTSImagesOpts.Text = strOptionalImages

        'Optional Settings
        Dim strOptionalSettings As String = Master.eLang.GetString(1175, "Optional Settings")
        Me.gbMovieSourcesFileNamingExpertBDMVOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertMultiOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertSingleOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertVTSOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingNMTOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingXBMCOptionalOpts.Text = strOptionalSettings

        'Original Title
        Dim strOriginalTitle As String = Master.eLang.GetString(302, "Original Title")
        Me.lblMovieScraperGlobalOriginalTitle.Text = strOriginalTitle

        'Overwrite existing
        Dim strOverwriteExisting As String = Master.eLang.GetString(483, "Overwrite Existing")
        Me.chkMovieActorThumbsOverwrite.Text = strOverwriteExisting
        Me.chkMovieBannerOverwrite.Text = strOverwriteExisting
        Me.chkMovieClearArtOverwrite.Text = strOverwriteExisting
        Me.chkMovieClearLogoOverwrite.Text = strOverwriteExisting
        Me.chkMovieDiscArtOverwrite.Text = strOverwriteExisting
        Me.chkMovieEFanartsOverwrite.Text = strOverwriteExisting
        Me.chkMovieEThumbsOverwrite.Text = strOverwriteExisting
        Me.chkMovieFanartOverwrite.Text = strOverwriteExisting
        Me.chkMovieLandscapeOverwrite.Text = strOverwriteExisting
        Me.chkMoviePosterOverwrite.Text = strOverwriteExisting
        Me.chkMovieSetBannerOverwrite.Text = strOverwriteExisting
        Me.chkMovieSetClearArtOverwrite.Text = strOverwriteExisting
        Me.chkMovieSetClearLogoOverwrite.Text = strOverwriteExisting
        Me.chkMovieSetFanartOverwrite.Text = strOverwriteExisting
        Me.chkMovieSetLandscapeOverwrite.Text = strOverwriteExisting
        Me.chkMovieSetPosterOverwrite.Text = strOverwriteExisting
        Me.chkMovieThemeOverwrite.Text = strOverwriteExisting
        Me.chkMovieTrailerOverwrite.Text = strOverwriteExisting
        Me.chkTVASBannerOverwrite.Text = strOverwriteExisting
        Me.chkTVASFanartOverwrite.Text = strOverwriteExisting
        Me.chkTVASLandscapeOverwrite.Text = strOverwriteExisting
        Me.chkTVASPosterOverwrite.Text = strOverwriteExisting
        Me.chkTVEpisodeFanartOverwrite.Text = strOverwriteExisting
        Me.chkTVEpisodePosterOverwrite.Text = strOverwriteExisting
        Me.chkTVSeasonBannerOverwrite.Text = strOverwriteExisting
        Me.chkTVSeasonFanartOverwrite.Text = strOverwriteExisting
        Me.chkTVSeasonLandscapeOverwrite.Text = strOverwriteExisting
        Me.chkTVSeasonPosterOverwrite.Text = strOverwriteExisting
        Me.chkTVShowBannerOverwrite.Text = strOverwriteExisting
        Me.chkTVShowCharacterArtOverwrite.Text = strOverwriteExisting
        Me.chkTVShowClearArtOverwrite.Text = strOverwriteExisting
        Me.chkTVShowClearLogoOverwrite.Text = strOverwriteExisting
        Me.chkTVShowEFanartsOverwrite.Text = strOverwriteExisting
        Me.chkTVShowFanartOverwrite.Text = strOverwriteExisting
        Me.chkTVShowLandscapeOverwrite.Text = strOverwriteExisting
        Me.chkTVShowPosterOverwrite.Text = strOverwriteExisting

        'Part of a MovieSet
        Dim strPartOfAMovieSet As String = Master.eLang.GetString(1295, "Part of a MovieSet")

        'Plot
        Dim strPlot As String = Master.eLang.GetString(65, "Plot")
        Me.lblMovieScraperGlobalPlot.Text = strPlot
        Me.lblMovieSetScraperGlobalPlot.Text = strPlot
        Me.lblTVScraperGlobalPlot.Text = strPlot

        'Plot Outline
        Dim strPlotOutline As String = Master.eLang.GetString(64, "Plot Outline")
        Me.lblMovieScraperGlobalOutline.Text = strPlotOutline

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        Me.gbMovieImagesPosterOpts.Text = strPoster
        Me.gbMovieSetImagesPosterOpts.Text = strPoster
        Me.gbTVImagesAllSeasonsPosterOpts.Text = strPoster
        Me.gbTVImagesEpisodePosterOpts.Text = strPoster
        Me.gbTVImagesSeasonPosterOpts.Text = strPoster
        Me.gbTVImagesShowPosterOpts.Text = strPoster
        Me.lblMoviePosterExpertBDMV.Text = strPoster
        Me.lblMoviePosterExpertMulti.Text = strPoster
        Me.lblMoviePosterExpertSingle.Text = strPoster
        Me.lblMoviePosterExpertVTS.Text = strPoster
        Me.lblTVSourcesFileNamingBoxeeDefaultsPoster.Text = strPoster
        Me.lblTVSourcesFileNamingNMTDefaultsPoster.Text = strPoster
        Me.lblTVSourcesFileNamingXBMCDefaultsPoster.Text = strPoster
        Me.lblMovieSourcesFileNamingBoxeeDefaultsPoster.Text = strPoster
        Me.lblMovieSourcesFileNamingNMTDefaultsPoster.Text = strPoster
        Me.lblMovieSourcesFileNamingXBMCDefaultsPoster.Text = strPoster

        'Preferred Size:
        Dim strPreferredSize As String = Master.eLang.GetString(482, "Preferred Size:")
        Me.lblMovieEFanartsSize.Text = strPreferredSize
        Me.lblMovieEThumbsSize.Text = strPreferredSize
        Me.lblMovieFanartSize.Text = strPreferredSize
        Me.lblMoviePosterSize.Text = strPreferredSize
        Me.lblMovieSetFanartSize.Text = strPreferredSize
        Me.lblMovieSetPosterSize.Text = strPreferredSize
        Me.lblTVASFanartSize.Text = strPreferredSize
        Me.lblTVASPosterSize.Text = strPreferredSize
        Me.lblTVEpisodeFanartSize.Text = strPreferredSize
        Me.lblTVEpisodePosterSize.Text = strPreferredSize
        Me.lblTVSeasonFanartSize.Text = strPreferredSize
        Me.lblTVSeasonPosterSize.Text = strPreferredSize
        Me.lblTVShowEFanartsSize.Text = strPreferredSize
        Me.lblTVShowFanartSize.Text = strPreferredSize
        Me.lblTVShowPosterSize.Text = strPreferredSize

        'Preferred Type:
        Dim strPreferredType As String = Master.eLang.GetString(730, "Preferred Type:")
        Me.lblMovieBannerType.Text = strPreferredType
        Me.lblMovieSetBannerType.Text = strPreferredType
        Me.lblTVASBannerType.Text = strPreferredType
        Me.lblTVSeasonBannerType.Text = strPreferredType
        Me.lblTVShowBannerType.Text = strPreferredType

        'Release Date
        Dim strReleaseDate As String = Master.eLang.GetString(57, "Release Date")
        Me.lblMovieScraperGlobalReleaseDate.Text = strReleaseDate

        'Rating
        Dim strRating As String = Master.eLang.GetString(400, "Rating")
        Me.lblMovieScraperGlobalRating.Text = strRating
        Me.lblTVScraperGlobalRating.Text = strRating

        'Runtime
        Dim strRuntime As String = Master.eLang.GetString(396, "Runtime")
        Me.lblMovieScraperGlobalRuntime.Text = strRuntime
        Me.lblTVScraperGlobalRuntime.Text = strRuntime

        'Scraper Fields - Global
        Dim strScraperGlobal As String = Master.eLang.GetString(577, "Scraper Fields - Global")
        Me.gbMovieScraperGlobalOpts.Text = strScraperGlobal
        Me.gbMovieSetScraperGlobalOpts.Text = strScraperGlobal
        Me.gbTVScraperGlobalOpts.Text = strScraperGlobal

        'Season #
        Dim strSeasonNR As String = Master.eLang.GetString(659, "Season #")
        Me.lblTVScraperGlobalSeason.Text = strSeasonNR

        'Season List Sorting
        Dim strSeasonListSorting As String = Master.eLang.GetString(493, "Season List Sorting")
        Me.gbTVGeneralSeasonListSorting.Text = strSeasonListSorting

        'Seasons
        Dim strSeasons As String = Master.eLang.GetString(681, "Seasons")

        'Show List Sorting
        Dim strShowListSorting As String = Master.eLang.GetString(492, "Show List Sorting")
        Me.gbTVGeneralShowListSorting.Text = strShowListSorting

        'Shows
        Dim strShows As String = Master.eLang.GetString(680, "Shows")
        Me.lblTVScraperGlobalHeaderShows.Text = strShows

        'Sort Tokens to Ignore
        Dim strSortTokens As String = Master.eLang.GetString(463, "Sort Tokens to Ignore")
        Me.gbMovieGeneralMediaListSortTokensOpts.Text = strSortTokens
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Text = strSortTokens
        Me.gbTVGeneralMediaListSortTokensOpts.Text = strSortTokens

        'Studio
        Dim strStudio As String = Master.eLang.GetString(395, "Studio")
        Me.lblMovieScraperGlobalStudio.Text = strStudio
        Me.lblTVScraperGlobalStudio.Text = strStudio

        'Subtitles
        Dim strSubtitles As String = Master.eLang.GetString(152, "Subtitles")

        'Tagline
        Dim strTagline As String = Master.eLang.GetString(397, "Tagline")
        Me.lblMovieScraperGlobalTagline.Text = strTagline

        'Theme
        Dim strTheme As String = Master.eLang.GetString(1118, "Theme")

        'Title
        Dim strTitle As String = Master.eLang.GetString(21, "Title")
        Me.lblMovieScraperGlobalTitle.Text = strTitle
        Me.lblMovieSetScraperGlobalTitle.Text = strTitle
        Me.lblTVScraperGlobalTitle.Text = strTitle

        'Top250
        Dim strTop250 As String = Master.eLang.GetString(591, "Top 250")
        Me.lblMovieScraperGlobalTop250.Text = strTop250

        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        Me.lblMovieScraperGlobalTrailer.Text = strTrailer
        Me.lblMovieTrailerExpertBDMV.Text = strTrailer
        Me.lblMovieTrailerExpertMulti.Text = strTrailer
        Me.lblMovieTrailerExpertSingle.Text = strTrailer
        Me.lblMovieTrailerExpertVTS.Text = strTrailer
        Me.lblMovieSourcesFileNamingNMTDefaultsTrailer.Text = strTrailer
        Me.lblMovieSourcesFileNamingXBMCDefaultsTrailer.Text = strTrailer

        'Watched
        Dim strWatched As String = Master.eLang.GetString(981, "Watched")

        'Votes
        Dim strVotes As String = Master.eLang.GetString(399, "Votes")
        Me.lblMovieScraperGlobalVotes.Text = strVotes
        Me.lblTVScraperGlobalVotes.Text = strVotes

        'Writers
        Dim strWriters As String = Master.eLang.GetString(394, "Writers")
        Me.lblMovieScraperGlobalCredits.Text = strWriters
        Me.lblTVScraperGlobalCredits.Text = strWriters

        'Year
        Dim strYear As String = Master.eLang.GetString(278, "Year")
        Me.lblMovieScraperGlobalYear.Text = strYear

        Me.gbGeneralScrapers.Text = Master.eLang.GetString(1193, "Scrapers")
        Me.chkGeneralResumeScraper.Text = Master.eLang.GetString(1194, "Enable Scraper Resume")
        Me.Text = Master.eLang.GetString(420, "Settings")
        Me.btnApply.Text = Master.eLang.GetString(276, "Apply")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.btnMovieSetScraperTitleRenamerAdd.Text = Master.eLang.GetString(28, "Add")
        Me.btnMovieSetScraperTitleRenamerRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.btnMovieSourceAdd.Text = Master.eLang.GetString(407, "Add Source")
        Me.btnMovieSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        Me.btnMovieSourceRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnRemTVSource.Text = Master.eLang.GetString(30, "Remove")
        Me.btnTVGeneralLangFetch.Text = Master.eLang.GetString(742, "Fetch Available Languages")
        Me.btnTVShowRegexAdd.Tag = String.Empty
        Me.btnTVShowRegexAdd.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.btnTVShowRegexClear.Text = Master.eLang.GetString(123, "Clear")
        Me.btnTVShowRegexEdit.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.btnTVShowRegexRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.btnTVSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        Me.chkFileSystemCleanerWhitelist.Text = Master.eLang.GetString(440, "Whitelist Video Extensions")
        Me.chkGeneralCheckUpdates.Text = Master.eLang.GetString(432, "Check for Updates")
        Me.chkGeneralDateAddedIgnoreNFO.Text = Master.eLang.GetString(1209, "Ignore <dateadded> from NFO")
        Me.chkGeneralDoubleClickScrape.Text = Master.eLang.GetString(1198, "Enable Image Scrape On Double Right Click")
        Me.chkGeneralHideBanner.Text = Master.eLang.GetString(1146, "Do Not Display Banner")
        Me.chkGeneralHideCharacterArt.Text = Master.eLang.GetString(1147, "Do Not Display CharacterArt")
        Me.chkGeneralHideClearArt.Text = Master.eLang.GetString(1148, "Do Not Display ClearArt")
        Me.chkGeneralHideClearLogo.Text = Master.eLang.GetString(1149, "Do Not Display ClearLogo")
        Me.chkGeneralHideDiscArt.Text = Master.eLang.GetString(1150, "Do Not Display DiscArt")
        Me.chkGeneralHideFanart.Text = Master.eLang.GetString(455, "Do Not Display Fanart")
        Me.chkGeneralHideFanartSmall.Text = Master.eLang.GetString(967, "Do Not Display Small Fanart")
        Me.chkGeneralHideLandscape.Text = Master.eLang.GetString(1151, "Do Not Display Landscape")
        Me.chkGeneralHidePoster.Text = Master.eLang.GetString(456, "Do Not Display Poster")
        Me.chkGeneralImagesGlassOverlay.Text = Master.eLang.GetString(966, "Enable Images Glass Overlay")
        Me.chkGeneralOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        Me.chkGeneralShowGenresText.Text = Master.eLang.GetString(453, "Always Display Genre Text")
        Me.chkGeneralShowLangFlags.Text = Master.eLang.GetString(489, "Display Language Flags")
        Me.chkGeneralShowImgDims.Text = Master.eLang.GetString(457, "Display Image Dimensions")
        Me.chkGeneralShowImgNames.Text = Master.eLang.GetString(1255, "Display Image Names")
        Me.chkGeneralSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        Me.chkMovieSourcesBackdropsAuto.Text = Master.eLang.GetString(521, "Automatically Save Fanart To Backdrops Folder")
        Me.chkMovieCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        Me.chkMovieDisplayYear.Text = Master.eLang.GetString(464, "Display Year in List Title")
        Me.chkMovieGeneralIgnoreLastScan.Text = Master.eLang.GetString(669, "Ignore last scan time when updating library")
        Me.chkMovieGeneralMarkNew.Text = Master.eLang.GetString(459, "Mark New Movies")
        Me.chkMovieLevTolerance.Text = Master.eLang.GetString(462, "Check Title Match Confidence")
        Me.chkMovieNoSaveImagesToNfo.Text = Master.eLang.GetString(498, "Do Not Save URLs to Nfo")
        Me.chkMovieProperCase.Text = Master.eLang.GetString(452, "Convert Names to Proper Case")
        Me.chkMovieRecognizeVTSExpertVTS.Text = Master.eLang.GetString(537, "Detect VIDEO_TS folders even if they are not named VIDEO_TS")
        Me.chkMovieScanOrderModify.Text = Master.eLang.GetString(796, "Scan in order of last write time")
        Me.chkMovieSetCleanFiles.Text = Master.eLang.GetString(1276, "Remove Images and NFOs with MovieSets")
        Me.chkMovieSetGeneralMarkNew.Text = Master.eLang.GetString(1301, "Mark New MovieSets")
        Me.chkMovieScraperCastWithImg.Text = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        Me.chkMovieScraperCertForMPAA.Text = Master.eLang.GetString(511, "Use Certification for MPAA")
        Me.chkMovieScraperCertForMPAAFallback.Text = Master.eLang.GetString(1293, "Only if no MPAA is found")
        Me.chkMovieScraperCleanFields.Text = Master.eLang.GetString(125, "Cleanup disabled fields")
        Me.chkMovieScraperCleanPlotOutline.Text = Master.eLang.GetString(985, "Clean Plot/Outline")
        Me.chkMovieScraperCollectionsAuto.Text = Master.eLang.GetString(1266, "Add Movie automatically to Collections")
        Me.chkMovieScraperVotesSeperator.Text = Master.eLang.GetString(1387, "Use thousands separator for votes")
        Me.chkMovieScraperDetailView.Text = Master.eLang.GetString(1249, "Show scraped results in detailed view")
        Me.chkMovieScraperReleaseFormat.Text = Master.eLang.GetString(1272, "Date format Releasedate: yyyy-mm-dd")
        Me.chkMovieScraperMetaDataIFOScan.Text = Master.eLang.GetString(628, "Enable IFO Parsing")
        Me.chkMovieScraperMetaDataScan.Text = Master.eLang.GetString(517, "Scan Meta Data")
        Me.chkMovieScraperCertOnlyValue.Text = Master.eLang.GetString(835, "Only Save the Value to NFO")
        Me.chkMovieScraperPlotForOutline.Text = Master.eLang.GetString(965, "Use Plot for Outline if Outline is Empty")
        Me.chkMovieScraperStudioWithImg.Text = Master.eLang.GetString(1280, "Scrape Only Studios With Images")
        Me.chkMovieScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.chkMovieScraperCertFSK.Text = Master.eLang.GetString(882, "Use MPAA as Fallback for FSK Rating")
        Me.chkMovieScraperXBMCTrailerFormat.Text = Master.eLang.GetString(1187, "Save YouTube-Trailer-Links in XBMC compatible format")
        Me.chkMovieYAMJCompatibleSets.Text = Master.eLang.GetString(561, "YAMJ Compatible Sets")
        Me.chkMovieSkipStackedSizeCheck.Text = Master.eLang.GetString(538, "Skip Size Check of Stacked Files")
        Me.chkMovieSortBeforeScan.Text = Master.eLang.GetString(712, "Sort files into folder before each library update")
        Me.chkMovieStackExpertMulti.Text = String.Format(Master.eLang.GetString(1178, "Stack {0}filename{1}"), "<", ">")
        Me.chkMovieThemeEnable.Text = Master.eLang.GetString(1082, "Enable Theme Support")
        Me.chkMovieTrailerDeleteExisting.Text = Master.eLang.GetString(522, "Delete All Existing")
        Me.chkMovieTrailerEnable.Text = Master.eLang.GetString(529, "Enable Trailer Support")
        Me.chkMovieUnstackExpertMulti.Text = Master.eLang.GetString(1179, "also save unstacked")
        Me.chkMovieUseBaseDirectoryExpertBDMV.Text = Master.eLang.GetString(1180, "Use Base Directory")
        Me.chkMovieXBMCProtectVTSBDMV.Text = Master.eLang.GetString(1176, "Protect DVD/Bluray Structure")
        Me.chkMovieXBMCThemeEnable.Text = Master.eLang.GetString(1082, "Enable Theme Support")
        Me.chkMovieXBMCThemeMovie.Text = Master.eLang.GetString(1258, "Store themes in movie directory")
        Me.chkMovieXBMCThemeCustom.Text = Master.eLang.GetString(1259, "Store themes in a custom path")
        Me.chkMovieXBMCThemeSub.Text = Master.eLang.GetString(1260, "Store themes in sub directorys")
        Me.chkMovieYAMJWatchedFile.Text = Master.eLang.GetString(1177, "Use .watched Files")
        Me.chkProxyCredsEnable.Text = Master.eLang.GetString(677, "Enable Credentials")
        Me.chkProxyEnable.Text = Master.eLang.GetString(673, "Enable Proxy")
        Me.chkTVDisplayMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        Me.chkTVDisplayStatus.Text = Master.eLang.GetString(126, "Display Status in List Title")
        Me.chkTVEpisodeNoFilter.Text = Master.eLang.GetString(734, "Build Episode Title Instead of Filtering")
        Me.chkTVGeneralMarkNewEpisodes.Text = Master.eLang.GetString(621, "Mark New Episodes")
        Me.chkTVGeneralMarkNewShows.Text = Master.eLang.GetString(549, "Mark New Shows")
        Me.chkTVScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.chkTVScraperUseSRuntimeForEp.Text = Master.eLang.GetString(1262, "Use Show Runtime for Episodes if no Episode Runtime can be found")
        Me.colFolder.Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colPath.Text = Master.eLang.GetString(410, "Path")
        Me.colRecur.Text = Master.eLang.GetString(411, "Recursive")
        Me.colSingle.Text = Master.eLang.GetString(413, "Single Video")
        Me.dgvMovieSetScraperTitleRenamer.Columns(0).HeaderText = Master.eLang.GetString(1277, "From")
        Me.dgvMovieSetScraperTitleRenamer.Columns(1).HeaderText = Master.eLang.GetString(1278, "To")
        Me.gbFileSystemExcludedDirs.Text = Master.eLang.GetString(1273, "Excluded Directories")
        Me.gbFileSystemCleanFiles.Text = Master.eLang.GetString(437, "Clean Files")
        Me.gbFileSystemNoStackExts.Text = Master.eLang.GetString(530, "No Stack Extensions")
        Me.gbFileSystemValidVideoExts.Text = Master.eLang.GetString(534, "Valid Video Extensions")
        Me.gbFileSystemValidSubtitlesExts.Text = Master.eLang.GetString(1284, "Valid Subtitles Extensions")
        Me.gbFileSystemValidThemeExts.Text = Master.eLang.GetString(1081, "Valid Theme Extensions")
        Me.gbGeneralDaemon.Text = Master.eLang.GetString(1261, "Configuration ISO Filescanning")
        Me.gbGeneralInterface.Text = Master.eLang.GetString(795, "Interface")
        Me.gbGeneralMainWindow.Text = Master.eLang.GetString(1152, "Main Window")
        Me.gbGeneralThemes.Text = Master.eLang.GetString(629, "GUI Themes")
        Me.gbMovieGeneralCustomMarker.Text = Master.eLang.GetString(1190, "Custom Marker")
        Me.gbMovieSourcesBackdropsFolderOpts.Text = Master.eLang.GetString(520, "Backdrops Folder")
        Me.gbMovieImagesFanartOpts.Text = Master.eLang.GetString(149, "Fanart")
        Me.gbMovieSourcesFileNaming.Text = Master.eLang.GetString(471, "File Naming")
        Me.gbMovieGeneralFiltersOpts.Text = Master.eLang.GetString(451, "Folder/File Name Filters")
        Me.gbMovieGeneralGenreFilterOpts.Text = Master.eLang.GetString(454, "Genre Language Filter")
        Me.gbMovieGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        Me.gbMovieImagesOpts.Text = Master.eLang.GetString(497, "Images")
        Me.gbMovieScraperCertificationOpts.Text = Master.eLang.GetString(56, "Certification")
        Me.gbMovieScraperDefFIExtOpts.Text = Master.eLang.GetString(625, "Defaults by File Type")
        Me.gbMovieSetMSAAPath.Text = Master.eLang.GetString(986, "Movieset Artwork Folder")
        Me.gbMovieSetScraperTitleRenamerOpts.Text = Master.eLang.GetString(1279, "Title Renamer")
        Me.gbMovieSourcesFileNamingXBMCTvTunesOpts.Text = Master.eLang.GetString(1076, "Theme Settings")
        Me.gbProxyCredsOpts.Text = Master.eLang.GetString(676, "Credentials")
        Me.gbProxyOpts.Text = Master.eLang.GetString(672, "Proxy")
        Me.gbSettingsHelp.Text = String.Concat("     ", Master.eLang.GetString(458, "Help"))
        Me.gbTVEpisodeFilterOpts.Text = Master.eLang.GetString(671, "Episode Folder/File Name Filters")
        Me.gbTVGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        Me.gbTVScraperDurationFormatOpts.Text = Master.eLang.GetString(515, "Duration Format")
        Me.gbTVScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields")
        Me.gbTVShowFilterOpts.Text = Master.eLang.GetString(670, "Show Folder/File Name Filters")
        Me.gbTVSourcesRegexMatch.Text = Master.eLang.GetString(691, "Show Match Regex")
        Me.lblFileSystemCleanerWarning.Text = Master.eLang.GetString(442, "WARNING: Using the Expert Mode Cleaner could potentially delete wanted files. Take care when using this tool.")
        Me.lblFileSystemCleanerWhitelist.Text = Master.eLang.GetString(441, "Whitelisted Extensions:")
        Me.lblGeneralDaemonDrive.Text = Master.eLang.GetString(989, "Driveletter")
        Me.lblGeneralDaemonPath.Text = Master.eLang.GetString(990, "Path to DTLite.exe")
        Me.lblGeneralMovieSetTheme.Text = String.Concat(Master.eLang.GetString(1155, "MovieSet Theme"), ":")
        Me.lblGeneralMovieTheme.Text = String.Concat(Master.eLang.GetString(620, "Movie Theme"), ":")
        Me.lblGeneralOverwriteNfo.Text = Master.eLang.GetString(434, "(If unchecked, non-conforming nfos will be renamed to <filename>.info)")
        Me.lblGeneralTVEpisodeTheme.Text = String.Concat(Master.eLang.GetString(667, "Episode Theme"), ":")
        Me.lblGeneralTVShowTheme.Text = String.Concat(Master.eLang.GetString(666, "TV Show Theme"), ":")
        Me.lblGeneralntLang.Text = Master.eLang.GetString(430, "Interface Language:")
        Me.lblMovieGeneralCustomMarker1.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #1")
        Me.lblMovieGeneralCustomMarker2.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #2")
        Me.lblMovieGeneralCustomMarker3.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #3")
        Me.lblMovieGeneralCustomMarker4.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #4")
        Me.lblMovieIMDBMirror.Text = Master.eLang.GetString(884, "IMDB Mirror:")
        Me.lblMovieLanguageOverlay.Text = Master.eLang.GetString(436, "Display Overlay if Video Contains an Audio Stream With the Following Language:")
        Me.lblMovieLevTolerance.Text = Master.eLang.GetString(461, "Mismatch Tolerance:")
        Me.lblMovieScraperDurationRuntimeFormat.Text = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), vbNewLine)
        Me.lblMovieSkipLessThan.Text = Master.eLang.GetString(540, "Skip files smaller than:")
        Me.lblMovieSkipLessThanMB.Text = Master.eLang.GetString(539, "MB")
        Me.lblMovieTrailerDefaultSearch.Text = Master.eLang.GetString(1172, "Default Search Parameter:")
        Me.lblMovieTrailerMinQual.Text = Master.eLang.GetString(1027, "Minimum Quality:")
        Me.lblMovieTrailerPrefQual.Text = Master.eLang.GetString(800, "Preferred Quality:")
        Me.lblProxyDomain.Text = Master.eLang.GetString(678, "Domain:")
        Me.lblProxyPassword.Text = Master.eLang.GetString(426, "Password:")
        Me.lblProxyPort.Text = Master.eLang.GetString(675, "Proxy Port:")
        Me.lblProxyURI.Text = Master.eLang.GetString(674, "Proxy URL:")
        Me.lblProxyUsername.Text = Master.eLang.GetString(425, "Username:")
        Me.lblSettingsTopDetails.Text = Master.eLang.GetString(518, "Configure Ember's appearance and operation.")
        Me.lblTVEpisodeMatch.Text = Master.eLang.GetString(693, "Episode Match Regex:")
        Me.lblTVSourcesDefaultsOrdering.Text = Master.eLang.GetString(797, "Default Episode Ordering:")
        Me.lblTVScraperRatingRegion.Text = Master.eLang.GetString(679, "TV Rating Region")
        Me.lblTVScraperUpdateTime.Text = Master.eLang.GetString(740, "Re-download Show Information Every:")
        Me.lblTVSeasonMatch.Text = Master.eLang.GetString(692, "Season Match Regex:")
        Me.lblTVSeasonRetrieve.Text = String.Concat(Master.eLang.GetString(694, "Apply To"), ":")
        Me.lvMovieSources.Columns(1).Text = Master.eLang.GetString(232, "Name")
        Me.lvMovieSources.Columns(2).Text = Master.eLang.GetString(410, "Path")
        Me.lvMovieSources.Columns(3).Text = Master.eLang.GetString(411, "Recursive")
        Me.lvMovieSources.Columns(4).Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.lvMovieSources.Columns(5).Text = Master.eLang.GetString(413, "Single Video")
        Me.lvMovieSources.Columns(6).Text = Master.eLang.GetString(264, "Exclude")
        Me.lvTVShowRegex.Columns(1).Text = Master.eLang.GetString(696, "Season Regex")
        Me.lvTVShowRegex.Columns(2).Text = Master.eLang.GetString(694, "Apply To")
        Me.lvTVShowRegex.Columns(3).Text = Master.eLang.GetString(697, "Episode Regex")
        Me.lvTVShowRegex.Columns(4).Text = Master.eLang.GetString(694, "Apply To")
        Me.lvTVShowRegex.Columns(5).Text = Master.eLang.GetString(728, "Aired")
        Me.lvTVSources.Columns(1).Text = Master.eLang.GetString(232, "Name")
        Me.lvTVSources.Columns(2).Text = Master.eLang.GetString(410, "Path")
        Me.lvTVSources.Columns(3).Text = Master.eLang.GetString(610, "Language")
        Me.lvTVSources.Columns(4).Text = Master.eLang.GetString(1167, "Ordering")
        Me.lvTVSources.Columns(5).Text = Master.eLang.GetString(264, "Exclude")
        Me.lvTVSources.Columns(6).Text = "Sorting"
        Me.tpFileSystemCleanerExpert.Text = Master.eLang.GetString(439, "Expert")
        Me.tpFileSystemCleanerStandard.Text = Master.eLang.GetString(438, "Standard")
        Me.tpTVImagesAllSeasons.Text = Master.eLang.GetString(1202, "TV All Seasons")
        Me.tpTVImagesEpisode.Text = Master.eLang.GetString(701, "TV Episode")
        Me.tpTVImagesSeason.Text = Master.eLang.GetString(744, "TV Season")
        Me.tpTVImagesShow.Text = Master.eLang.GetString(700, "TV Show")
        Me.tpTVSourcesGeneral.Text = Master.eLang.GetString(38, "General")
        Me.tpTVSourcesRegex.Text = Master.eLang.GetString(699, "Regex")

        'items with text from other items
        Me.btnTVSourceAdd.Text = Me.btnMovieSourceAdd.Text
        Me.chkMovieSetCleanDB.Text = Me.chkMovieCleanDB.Text
        Me.chkMovieStackExpertSingle.Text = Me.chkMovieStackExpertMulti.Text
        Me.chkMovieUnstackExpertSingle.Text = Me.chkMovieUnstackExpertMulti.Text
        Me.chkMovieUseBaseDirectoryExpertVTS.Text = Me.chkMovieUseBaseDirectoryExpertBDMV.Text
        Me.chkTVCleanDB.Text = Me.chkMovieCleanDB.Text
        Me.chkTVEpisodeProperCase.Text = Me.chkMovieProperCase.Text
        Me.chkTVGeneralIgnoreLastScan.Text = Me.chkMovieGeneralIgnoreLastScan.Text
        Me.chkTVScanOrderModify.Text = Me.chkMovieScanOrderModify.Text
        Me.chkTVScraperMetaDataScan.Text = Me.chkMovieScraperMetaDataScan.Text
        Me.chkTVShowProperCase.Text = Me.chkMovieProperCase.Text
        Me.gbMovieSetFileNamingOpts.Text = Me.gbMovieSourcesFileNaming.Text
        Me.gbMovieSetGeneralMediaListOpts.Text = Me.gbMovieGeneralMediaListOpts.Text
        Me.gbMovieThemeOpts.Text = Me.gbGeneralThemes.Text
        Me.gbTVScraperDefFIExtOpts.Text = Me.gbTVScraperDefFIExtOpts.Text
        Me.lblSettingsTopTitle.Text = Me.Text
        Me.lblTVEpisodeRetrieve.Text = Me.lblTVSeasonRetrieve.Text
        Me.lblTVLanguageOverlay.Text = Me.lblMovieLanguageOverlay.Text
        Me.lblTVSkipLessThan.Text = Me.lblMovieSkipLessThan.Text
        Me.lblTVSkipLessThanMB.Text = Me.lblMovieSkipLessThanMB.Text

        Me.LoadGeneralDateTime()
        Me.LoadGenericFanartSizes()
        Me.LoadGenericPosterSizes()
        Me.LoadMovieBannerTypes()
        Me.LoadMovieTrailerQualities()
        Me.LoadTVFanartSizes()
        Me.LoadTVPosterSizes()
        Me.LoadTVScraperOptionsOrdering()
        Me.LoadTVScraperUpdateTime()
        Me.LoadTVSeasonBannerTypes()
        Me.LoadTVShowBannerTypes()
        Me.LoadTVSeasonRetrieve()
        Me.LoadTVEpisodeRetrieve()
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
        Me.currPanel.Dock = DockStyle.Fill
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

    Private Sub txtMovieBackdropsPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSourcesBackdropsFolderPath.TextChanged
        Me.SetApplyButton(True)

        If String.IsNullOrEmpty(Me.txtMovieSourcesBackdropsFolderPath.Text) Then
            Me.chkMovieSourcesBackdropsAuto.Checked = False
            Me.chkMovieSourcesBackdropsAuto.Enabled = False
        Else
            Me.chkMovieSourcesBackdropsAuto.Enabled = True
        End If
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

    Private Sub txtMovieSetBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetBannerHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetBannerHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetBannerWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetBannerWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowEFanartsHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowEFanartsHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowEFanartsHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowEFanartsHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowEFanartsLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowEFanartsLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowEFanartsLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowEFanartsLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtTVShowEFanartsWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowEFanartsWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowEFanartsWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVShowEFanartsWidth.TextChanged
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

    Private Sub txtMovieSetFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetFanartHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetFanartHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetFanartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetFanartWidth.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieScraperGenreLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieScraperGenreLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieScraperGenreLimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieScraperGenreLimit.TextChanged
        Me.SetApplyButton(True)
    End Sub
    Private Sub txtMovieScraperStudioLimit_TextChanged(sender As Object, e As EventArgs) Handles txtMovieScraperStudioLimit.TextChanged
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

    Private Sub txtMovieSetPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetPosterHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetPosterHeight.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetPosterWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetPosterWidth.TextChanged
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

    Private Sub txtMovieSetPathMSAA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetPathMSAA.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnMovieSetPathMSAABrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathMSAABrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieSetPathMSAA.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieSetPathExpertSingleBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathExpertSingleBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieSetPathExpertSingle.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
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

    Private Sub chkMovieSetUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetUseExpert.CheckedChanged
        Me.SetApplyButton(True)

        Me.btnMovieSetPathExpertSingleBrowse.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetBannerExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetBannerExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearArtExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearArtExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearLogoExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearLogoExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetDiscArtExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetDiscArtExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetFanartExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetFanartExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetLandscapeExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetLandscapeExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetNFOExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetNFOExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPathExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPosterExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPosterExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
    End Sub

    Private Sub txtMovieSetBannerExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetBannerExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetClearArtExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetClearArtExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetClearLogoExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetClearLogoExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetDiscArtExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetDiscArtExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetFanartExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetFanartExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetLandscapeExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetLandscapeExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetNFOExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetNFOExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetPosterExpertSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetPosterExpertSingle.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetBannerExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetBannerExpertParent.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetClearArtExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetClearArtExpertParent.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetClearLogoExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetClearLogoExpertParent.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetDiscArtExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetDiscArtExpertParent.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetFanartExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetFanartExpertParent.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetLandscapeExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetLandscapeExpertParent.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetNFOExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetNFOExpertParent.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieSetPosterExpertParent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSetPosterExpertParent.TextChanged
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
        Me.chkTVSeasonLandscapeAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowCharacterArtAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearArtAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearLogoAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowExtrafanartsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowLandscapeAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVXBMCThemeEnable.Enabled = Me.chkTVUseFrodo.Checked

        If Not Me.chkTVUseFrodo.Checked Then
            Me.chkTVEpisodeActorThumbsFrodo.Checked = False
            Me.chkTVEpisodePosterFrodo.Checked = False
            Me.chkTVSeasonBannerFrodo.Checked = False
            Me.chkTVSeasonFanartFrodo.Checked = False
            Me.chkTVSeasonLandscapeAD.Checked = False
            Me.chkTVSeasonPosterFrodo.Checked = False
            Me.chkTVShowActorThumbsFrodo.Checked = False
            Me.chkTVShowBannerFrodo.Checked = False
            Me.chkTVShowCharacterArtAD.Checked = False
            Me.chkTVShowClearArtAD.Checked = False
            Me.chkTVShowClearLogoAD.Checked = False
            Me.chkTVShowExtrafanartsFrodo.Checked = False
            Me.chkTVShowFanartFrodo.Checked = False
            Me.chkTVShowLandscapeAD.Checked = False
            Me.chkTVShowPosterFrodo.Checked = False
            Me.chkTVXBMCThemeEnable.Checked = False
        Else
            Me.chkTVEpisodeActorThumbsFrodo.Checked = True
            Me.chkTVEpisodePosterFrodo.Checked = True
            Me.chkTVSeasonBannerFrodo.Checked = True
            Me.chkTVSeasonFanartFrodo.Checked = True
            Me.chkTVSeasonLandscapeAD.Checked = True
            Me.chkTVSeasonPosterFrodo.Checked = True
            Me.chkTVShowActorThumbsFrodo.Checked = True
            Me.chkTVShowBannerFrodo.Checked = True
            Me.chkTVShowCharacterArtAD.Checked = True
            Me.chkTVShowClearArtAD.Checked = True
            Me.chkTVShowClearLogoAD.Checked = True
            Me.chkTVShowExtrafanartsFrodo.Checked = True
            Me.chkTVShowFanartFrodo.Checked = True
            Me.chkTVShowLandscapeAD.Checked = True
            Me.chkTVShowPosterFrodo.Checked = True
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

    Private Sub chkTVSeasonLandscapeXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVSeasonLandscapeAD.CheckedChanged
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

    Private Sub chkTVShowCharacterArtXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowCharacterArtAD.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowClearArtXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowClearArtAD.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowClearLogoXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowClearLogoAD.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowExtrafanartsXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowExtrafanartsFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowFanartFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowFanartFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowLandscapeXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowLandscapeAD.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowPosterFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowPosterFrodo.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVShowTVThemeXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVXBMCThemeEnable.CheckedChanged
        Me.SetApplyButton(True)

        Me.btnTVXBMCThemeCustomPathBrowse.Enabled = Me.chkTVXBMCThemeEnable.Checked
        Me.txtTVXBMCThemeCustomPath.Enabled = Me.chkTVXBMCThemeEnable.Checked
    End Sub

    Private Sub txtTVShowTVThemeFolderXBMC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVXBMCThemeCustomPath.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnTVShowTVThemeBrowse_Click(sender As Object, e As EventArgs) Handles btnTVXBMCThemeCustomPathBrowse.Click
        With Me.fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(1028, "Select the folder where you wish to store your tv themes...")
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtTVXBMCThemeCustomPath.Text = .SelectedPath.ToString
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

    Private Sub txtMovieGeneralCustomMarker1_TextChanged(sender As Object, e As EventArgs) Handles txtMovieGeneralCustomMarker1.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieGeneralCustomMarker2_TextChanged(sender As Object, e As EventArgs) Handles txtMovieGeneralCustomMarker2.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieGeneralCustomMarker3_TextChanged(sender As Object, e As EventArgs) Handles txtMovieGeneralCustomMarker3.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub txtMovieGeneralCustomMarker4_TextChanged(sender As Object, e As EventArgs) Handles txtMovieGeneralCustomMarker4.TextChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetGeneralMarkNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetGeneralMarkNew.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetBannerMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetBannerMSAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetClearArtMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetClearArtMSAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetClearLogoMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetClearLogoMSAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetFanartMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetFanartMSAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetLandscapeMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetLandscapeMSAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetNFOMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetNFOMSAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetPosterMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetPosterMSAA.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetLockPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetLockPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetLockTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetLockTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetBannerOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetBannerOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetBannerPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetBannerPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetClearArtOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetClearArtOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetClearLogoOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetClearLogoOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetClickScrape.CheckedChanged
        chkMovieSetClickScrapeAsk.Enabled = chkMovieSetClickScrape.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetClickScrapeAsk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetClickScrapeAsk.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetFanartOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetFanartOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetFanartPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetFanartPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetLandscapeOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetLandscapeOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetPosterOverwrite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetPosterOverwrite.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetPosterPrefOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetPosterPrefOnly.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetScraperPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetScraperPlot.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetScraperTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetScraperTitle.CheckedChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub pbMSAAInfo_Click(sender As Object, e As EventArgs) Handles pbMSAAInfo.Click
        If Master.isWindows Then
            Process.Start("http://forum.xbmc.org/showthread.php?tid=153502")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://forum.xbmc.org/showthread.php?tid=153502"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub pbMSAAInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbMSAAInfo.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pbMSAAInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbMSAAInfo.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pbMovieSourcesADInfo_Click(sender As Object, e As EventArgs) Handles pbMovieSourcesADInfo.Click
        If Master.isWindows Then
            Process.Start("http://kodi.wiki/view/Add-on:Artwork_Downloader#Filenaming")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://kodi.wiki/view/Add-on:Artwork_Downloader#Filenaming"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub pbMovieSourcesADInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbMovieSourcesADInfo.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pbMovieSourcesADInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbMovieSourcesADInfo.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pbTVSourcesADInfo_Click(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.Click
        If Master.isWindows Then
            Process.Start("http://kodi.wiki/view/Add-on:Artwork_Downloader#Filenaming")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://kodi.wiki/view/Add-on:Artwork_Downloader#Filenaming"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub pbTVSourcesADInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pbTVSourcesADInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnMovieSetScraperMapperAdd_Click(sender As Object, e As EventArgs) Handles btnMovieSetScraperTitleRenamerAdd.Click
        Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
        dgvMovieSetScraperTitleRenamer.CurrentCell = dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0)
        dgvMovieSetScraperTitleRenamer.BeginEdit(True)
        Me.SetApplyButton(True)
    End Sub

    Private Sub btnMovieSetScraperMapperRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetScraperTitleRenamerRemove.Click
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            dgvMovieSetScraperTitleRenamer.Rows.RemoveAt(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex)
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSetScraperTitleRenamer.CurrentCellDirtyStateChanged
        Me.SetApplyButton(True)
    End Sub

    Private Sub dgvMovieSetScraperMapper_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSetScraperTitleRenamer.SelectionChanged
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            btnMovieSetScraperTitleRenamerRemove.Enabled = True
        Else
            btnMovieSetScraperTitleRenamerRemove.Enabled = False
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovieSetScraperTitleRenamer.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

#End Region 'Methods


End Class