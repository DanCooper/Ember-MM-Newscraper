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
' # Dialog size: 1230; 900
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
    Private TVShowMatching As New List(Of Settings.regexp)
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
                If item.Tag IsNot Nothing AndAlso item.Tag.ToString = "spacer" Then
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
                If item.Tag IsNot Nothing AndAlso item.Tag.ToString = "spacer" Then
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
        For Each s As ModulesManager._externalScraperModuleClass_Data_TV In ModulesManager.Instance.externalScrapersModules_Data_TV.OrderBy(Function(x) x.ModuleOrder)
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
                AddHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
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
        For Each s As ModulesManager._externalScraperModuleClass_Data_TV In ModulesManager.Instance.externalScrapersModules_Data_TV.OrderBy(Function(x) x.ModuleOrder)
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
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In ModulesManager.Instance.externalScrapersModules_Trailer_Movie.OrderBy(Function(x) x.ModuleOrder)
            RemoveHandler s.ProcessorModule.ScraperSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Next
        For Each s As ModulesManager._externalGenericModuleClass In ModulesManager.Instance.externalProcessorModules
            RemoveHandler s.ProcessorModule.ModuleSetupChanged, AddressOf Handle_ModuleSetupChanged
            RemoveHandler s.ProcessorModule.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler s.ProcessorModule.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
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
            Me.sResult.NeedsReload_TV = True
        End If

        Me.txtTVEpisodeFilter.Focus()
    End Sub

    Private Sub btnMovieFilterAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterAdd.Click
        If Not String.IsNullOrEmpty(Me.txtMovieFilter.Text) Then
            Me.lstMovieFilters.Items.Add(Me.txtMovieFilter.Text)
            Me.txtMovieFilter.Text = String.Empty
            Me.SetApplyButton(True)
            Me.sResult.NeedsReload_Movie = True
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
            Me.sResult.NeedsDBClean_Movie = True
            Me.sResult.NeedsDBClean_TV = True
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
                Me.sResult.NeedsDBUpdate_Movie = True
                Me.sResult.NeedsDBUpdate_TV = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        Finally
            Master.DB.LoadExcludeDirsFromDB()
        End Try
    End Sub

    Private Sub btnFileSystemValidVideoExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidVideoExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidVideoExts.Text) Then
            If Not txtFileSystemValidVideoExts.Text.Substring(0, 1) = "." Then txtFileSystemValidVideoExts.Text = String.Concat(".", txtFileSystemValidVideoExts.Text)
            If Not lstFileSystemValidVideoExts.Items.Contains(txtFileSystemValidVideoExts.Text.ToLower) Then
                lstFileSystemValidVideoExts.Items.Add(txtFileSystemValidVideoExts.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsDBUpdate_Movie = True
                Me.sResult.NeedsDBUpdate_TV = True
                txtFileSystemValidVideoExts.Text = String.Empty
                txtFileSystemValidVideoExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidSubtitlesExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidSubtitlesExts.Text) Then
            If Not txtFileSystemValidSubtitlesExts.Text.Substring(0, 1) = "." Then txtFileSystemValidSubtitlesExts.Text = String.Concat(".", txtFileSystemValidSubtitlesExts.Text)
            If Not lstFileSystemValidSubtitlesExts.Items.Contains(txtFileSystemValidSubtitlesExts.Text.ToLower) Then
                lstFileSystemValidSubtitlesExts.Items.Add(txtFileSystemValidSubtitlesExts.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_Movie = True
                Me.sResult.NeedsReload_TV = True
                txtFileSystemValidSubtitlesExts.Text = String.Empty
                txtFileSystemValidSubtitlesExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidThemeExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidThemeExts.Text) Then
            If Not txtFileSystemValidThemeExts.Text.Substring(0, 1) = "." Then txtFileSystemValidThemeExts.Text = String.Concat(".", txtFileSystemValidThemeExts.Text)
            If Not lstFileSystemValidThemeExts.Items.Contains(txtFileSystemValidThemeExts.Text.ToLower) Then
                lstFileSystemValidThemeExts.Items.Add(txtFileSystemValidThemeExts.Text.ToLower)
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_Movie = True
                Me.sResult.NeedsReload_TV = True
                txtFileSystemValidThemeExts.Text = String.Empty
                txtFileSystemValidThemeExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemNoStackExtsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemNoStackExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemNoStackExts.Text) Then
            If Not txtFileSystemNoStackExts.Text.Substring(0, 1) = "." Then txtFileSystemNoStackExts.Text = String.Concat(".", txtFileSystemNoStackExts.Text)
            If Not lstFileSystemNoStackExts.Items.Contains(txtFileSystemNoStackExts.Text) Then
                lstFileSystemNoStackExts.Items.Add(txtFileSystemNoStackExts.Text)
                Me.SetApplyButton(True)
                Me.sResult.NeedsDBUpdate_Movie = True
                Me.sResult.NeedsDBUpdate_TV = True
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
            Me.sResult.NeedsReload_TV = True
        End If

        Me.txtTVShowFilter.Focus()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingAdd.Click
        If String.IsNullOrEmpty(Me.btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then
            Dim lID = (From lRegex As Settings.regexp In Me.TVShowMatching Select lRegex.ID).Max
            Me.TVShowMatching.Add(New Settings.regexp With {.ID = Convert.ToInt32(lID) + 1, _
                                                            .Regexp = Me.txtTVSourcesRegexTVShowMatchingRegex.Text, _
                                                            .defaultSeason = If(String.IsNullOrEmpty(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text) OrElse Not Integer.TryParse(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0), -1, CInt(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text)), _
                                                            .byDate = Me.chkTVSourcesRegexTVShowMatchingByDate.Checked})
        Else
            Dim selRex = From lRegex As Settings.regexp In Me.TVShowMatching Where lRegex.ID = Convert.ToInt32(Me.btnTVSourcesRegexTVShowMatchingAdd.Tag)
            If selRex.Count > 0 Then
                selRex(0).Regexp = Me.txtTVSourcesRegexTVShowMatchingRegex.Text
                selRex(0).defaultSeason = CInt(If(String.IsNullOrEmpty(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text), "-1", Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text))
                selRex(0).byDate = Me.chkTVSourcesRegexTVShowMatchingByDate.Checked
            End If
        End If

        Me.ClearTVShowMatching()
        Me.SetApplyButton(True)
        Me.LoadTVShowMatching()
    End Sub

    Private Sub btnMovieSortTokenAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtMovieSortToken.Text) Then
            If Not lstMovieSortTokens.Items.Contains(txtMovieSortToken.Text) Then
                lstMovieSortTokens.Items.Add(txtMovieSortToken.Text)
                Me.sResult.NeedsReload_Movie = True
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
                Me.sResult.NeedsReload_MovieSet = True
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
                Me.sResult.NeedsReload_TV = True
                Me.SetApplyButton(True)
                txtTVSortToken.Text = String.Empty
                txtTVSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVSourceAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourceAdd.Click
        Using dSource As New dlgSourceTVShow
            If dSource.ShowDialog = Windows.Forms.DialogResult.OK Then
                RefreshTVSources()
                Me.SetApplyButton(True)
                Me.sResult.NeedsDBUpdate_TV = True
            End If
        End Using
    End Sub

    Private Sub btnFileSystemCleanerWhitelistAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemCleanerWhitelistAdd.Click
        If Not String.IsNullOrEmpty(Me.txtFileSystemCleanerWhitelist.Text) Then
            If Not txtFileSystemCleanerWhitelist.Text.Substring(0, 1) = "." Then txtFileSystemCleanerWhitelist.Text = String.Concat(".", txtFileSystemCleanerWhitelist.Text)
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
        If Me.sResult.NeedsDBClean_Movie OrElse _
            Me.sResult.NeedsDBClean_TV OrElse _
            Me.sResult.NeedsDBUpdate_Movie OrElse _
            Me.sResult.NeedsDBUpdate_TV OrElse _
            Me.sResult.NeedsReload_Movie OrElse _
            Me.sResult.NeedsReload_MovieSet OrElse _
            Me.sResult.NeedsReload_TV Then _
            Me.didApply = True
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

    Private Sub btnTVSourcesRegexTVShowMatchingClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingClear.Click
        Me.ClearTVShowMatching()
    End Sub

    Private Sub btnMovieFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterDown.Click
        Try
            If Me.lstMovieFilters.Items.Count > 0 AndAlso Me.lstMovieFilters.SelectedItem IsNot Nothing AndAlso Me.lstMovieFilters.SelectedIndex < (Me.lstMovieFilters.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstMovieFilters.SelectedIndices(0)
                Me.lstMovieFilters.Items.Insert(iIndex + 2, Me.lstMovieFilters.SelectedItems(0))
                Me.lstMovieFilters.Items.RemoveAt(iIndex)
                Me.lstMovieFilters.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_Movie = True
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
        Using dEditMeta As New dlgFileInfo(New Database.DBElement, False)
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

    Private Sub btnTVSourcesRegexTVShowMatchingEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingEdit.Click
        If Me.lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then Me.EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub btnMovieSourceEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSourceEdit.Click
        If lvMovieSources.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgSourceMovie
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshMovieSources()
                    Me.sResult.NeedsReload_Movie = True 'TODO: Check if we have to use Reload or DBUpdate
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub btnTVScraperDefFIExtEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVScraperDefFIExtEdit.Click
        Using dEditMeta As New dlgFileInfo(New Database.DBElement, True)
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
            Using dTVSource As New dlgSourceTVShow
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshTVSources()
                    Me.sResult.NeedsReload_TV = True 'TODO: Check if we have to use Reload or DBUpdate
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub btnTVEpisodeFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterDown.Click
        Try
            If Me.lstTVEpisodeFilter.Items.Count > 0 AndAlso Me.lstTVEpisodeFilter.SelectedItem IsNot Nothing AndAlso Me.lstTVEpisodeFilter.SelectedIndex < (Me.lstTVEpisodeFilter.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstTVEpisodeFilter.SelectedIndices(0)
                Me.lstTVEpisodeFilter.Items.Insert(iIndex + 2, Me.lstTVEpisodeFilter.SelectedItems(0))
                Me.lstTVEpisodeFilter.Items.RemoveAt(iIndex)
                Me.lstTVEpisodeFilter.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_TV = True
                Me.lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVEpisodeFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterUp.Click
        Try
            If Me.lstTVEpisodeFilter.Items.Count > 0 AndAlso Me.lstTVEpisodeFilter.SelectedItem IsNot Nothing AndAlso Me.lstTVEpisodeFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstTVEpisodeFilter.SelectedIndices(0)
                Me.lstTVEpisodeFilter.Items.Insert(iIndex - 1, Me.lstTVEpisodeFilter.SelectedItems(0))
                Me.lstTVEpisodeFilter.Items.RemoveAt(iIndex + 1)
                Me.lstTVEpisodeFilter.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_TV = True
                Me.lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieSourceAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSourceAdd.Click
        Using dSource As New dlgSourceMovie
            If dSource.ShowDialog = Windows.Forms.DialogResult.OK Then
                RefreshMovieSources()
                Me.SetApplyButton(True)
                Me.sResult.NeedsDBUpdate_Movie = True
            End If
        End Using
    End Sub

    Private Sub btnMovieSourceRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSourceRemove.Click
        Me.RemoveMovieSource()
        Master.DB.LoadMovieSourcesFromDB()
    End Sub

    Private Sub btnMovieScraperDefFIExtAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieScraperDefFIExtAdd.Click
        If Not txtMovieScraperDefFIExt.Text.StartsWith(".") Then txtMovieScraperDefFIExt.Text = String.Concat(".", txtMovieScraperDefFIExt.Text)
        Using dEditMeta As New dlgFileInfo(New Database.DBElement, False)
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
        Using dEditMeta As New dlgFileInfo(New Database.DBElement, True)
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

    Private Sub btnTVSourcesRegexTVShowMatchingUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingUp.Click
        Try
            If Me.lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso Me.lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso Not Me.lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.regexp = Me.TVShowMatching.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(Me.lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    Me.lvTVSourcesRegexTVShowMatching.SuspendLayout()
                    Dim iIndex As Integer = Me.TVShowMatching.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                    Me.TVShowMatching.Remove(selItem)
                    Me.TVShowMatching.Insert(iIndex - 1, selItem)

                    Me.RenumberTVShowMatching()
                    Me.LoadTVShowMatching()

                    Me.lvTVSourcesRegexTVShowMatching.Items(selIndex - 1).Selected = True
                    Me.lvTVSourcesRegexTVShowMatching.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVSourcesRegexTVShowMatching.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingDown.Click
        Try
            If Me.lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso Me.lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso Me.lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index < (Me.lvTVSourcesRegexTVShowMatching.Items.Count - 1) Then
                Dim selItem As Settings.regexp = Me.TVShowMatching.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(Me.lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    Me.lvTVSourcesRegexTVShowMatching.SuspendLayout()
                    Dim iIndex As Integer = Me.TVShowMatching.IndexOf(selItem)
                    Dim selIndex As Integer = Me.lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                    Me.TVShowMatching.Remove(selItem)
                    Me.TVShowMatching.Insert(iIndex + 1, selItem)

                    Me.RenumberTVShowMatching()
                    Me.LoadTVShowMatching()

                    Me.lvTVSourcesRegexTVShowMatching.Items(selIndex + 1).Selected = True
                    Me.lvTVSourcesRegexTVShowMatching.ResumeLayout()
                End If

                Me.SetApplyButton(True)
                Me.lvTVSourcesRegexTVShowMatching.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieGeneralMediaListSortingUp.Click
        Try
            If Me.lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso Me.lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not Me.lvMovieGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = Me.MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(Me.lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

                If selItem IsNot Nothing Then
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

            If selItem IsNot Nothing Then
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

            If selItem IsNot Nothing Then
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

            If selItem IsNot Nothing Then
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

            If selItem IsNot Nothing Then
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

            If selItem IsNot Nothing Then
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

    Private Sub btnTVShowFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default list of show filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ShowFilters, True)
            Me.RefreshTVShowFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnTVEpisodeFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVEpisodeFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(841, "Are you sure you want to reset to the default list of episode filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.EpFilters, True)
            Me.RefreshTVEpisodeFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnMovieFilterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(842, "Are you sure you want to reset to the default list of movie filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieFilters, True)
            Me.RefreshMovieFilters()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidVideoExtsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidVideoExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(843, "Are you sure you want to reset to the default list of valid video extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidExts, True)
            Me.RefreshFileSystemValidExts()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidSubtitlesExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(1283, "Are you sure you want to reset to the default list of valid subtitle extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidSubtitleExts, True)
            Me.RefreshFileSystemValidSubtitlesExts()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSystemValidThemeExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(1080, "Are you sure you want to reset to the default list of valid theme extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ValidThemeExts, True)
            Me.RefreshFileSystemValidThemeExts()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingGet.Click
        Using dd As New dlgTVRegExProfiles
            If dd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.TVShowMatching.Clear()
                Me.TVShowMatching.AddRange(dd.ShowRegex)
                Me.LoadTVShowMatching()
                Me.SetApplyButton(True)
            End If
        End Using
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingReset.Click
        If MessageBox.Show(Master.eLang.GetString(844, "Are you sure you want to reset to the default list of show regex?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowMatching, True)
            Me.TVShowMatching.Clear()
            Me.TVShowMatching.AddRange(Master.eSettings.TVShowMatching)
            Me.LoadTVShowMatching()
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub btnTVSourcesRegexMultiPartMatchingReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexMultiPartMatchingReset.Click
        Me.txtTVSourcesRegexMultiPartMatching.Text = "^[-_ex]+([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)"
        Me.SetApplyButton(True)
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

    Private Sub btnTVSourcesRegexTVShowMatchingRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVSourcesRegexTVShowMatchingRemove.Click
        Me.RemoveTVShowMatching()
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
        Master.eSettings.TVGeneralLanguages = ModulesManager.Instance.GetTVLanguages()
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
        Master.DB.LoadTVShowSourcesFromDB()
    End Sub

    Private Sub btnTVShowFilterDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterDown.Click
        Try
            If Me.lstTVShowFilter.Items.Count > 0 AndAlso Me.lstTVShowFilter.SelectedItem IsNot Nothing AndAlso Me.lstTVShowFilter.SelectedIndex < (Me.lstTVShowFilter.Items.Count - 1) Then
                Dim iIndex As Integer = Me.lstTVShowFilter.SelectedIndices(0)
                Me.lstTVShowFilter.Items.Insert(iIndex + 2, Me.lstTVShowFilter.SelectedItems(0))
                Me.lstTVShowFilter.Items.RemoveAt(iIndex)
                Me.lstTVShowFilter.SelectedIndex = iIndex + 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_TV = True
                Me.lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVShowFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVShowFilterUp.Click
        Try
            If Me.lstTVShowFilter.Items.Count > 0 AndAlso Me.lstTVShowFilter.SelectedItem IsNot Nothing AndAlso Me.lstTVShowFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstTVShowFilter.SelectedIndices(0)
                Me.lstTVShowFilter.Items.Insert(iIndex - 1, Me.lstTVShowFilter.SelectedItems(0))
                Me.lstTVShowFilter.Items.RemoveAt(iIndex + 1)
                Me.lstTVShowFilter.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_TV = True
                Me.lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub


    Private Sub btnMovieFilterUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieFilterUp.Click
        Try
            If Me.lstMovieFilters.Items.Count > 0 AndAlso Me.lstMovieFilters.SelectedItem IsNot Nothing AndAlso Me.lstMovieFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = Me.lstMovieFilters.SelectedIndices(0)
                Me.lstMovieFilters.Items.Insert(iIndex - 1, Me.lstMovieFilters.SelectedItems(0))
                Me.lstMovieFilters.Items.RemoveAt(iIndex + 1)
                Me.lstMovieFilters.SelectedIndex = iIndex - 1
                Me.SetApplyButton(True)
                Me.sResult.NeedsReload_Movie = True
                Me.lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cbGeneralLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGeneralLanguage.SelectedIndexChanged
        If Not Me.cbGeneralLanguage.SelectedItem.ToString = Master.eSettings.GeneralLanguage Then
            Handle_SetupNeedsRestart()
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub cbMovieTrailerPrefVideoQual_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTrailerPrefVideoQual.SelectedIndexChanged
        If CType(Me.cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value = Enums.TrailerVideoQuality.Any Then
            Me.cbMovieTrailerMinVideoQual.Enabled = False
        Else
            Me.cbMovieTrailerMinVideoQual.Enabled = True
        End If
        Me.SetApplyButton(True)
    End Sub

    Private Sub CheckHideSettings()
        If chkGeneralDisplayBanner.Checked OrElse chkGeneralDisplayCharacterArt.Checked OrElse chkGeneralDisplayClearArt.Checked OrElse chkGeneralDisplayClearLogo.Checked OrElse _
              chkGeneralDisplayDiscArt.Checked OrElse chkGeneralDisplayFanart.Checked OrElse chkGeneralDisplayFanartSmall.Checked OrElse chkGeneralDisplayLandscape.Checked OrElse chkGeneralDisplayPoster.Checked Then
            Me.chkGeneralImagesGlassOverlay.Enabled = True
            Me.chkGeneralDisplayImgDims.Enabled = True
            Me.chkGeneralDisplayImgNames.Enabled = True
        Else
            Me.chkGeneralImagesGlassOverlay.Enabled = False
            Me.chkGeneralDisplayImgDims.Enabled = False
            Me.chkGeneralDisplayImgNames.Enabled = False
        End If
    End Sub

    Private Sub chkMovieClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieClickScrape.CheckedChanged
        chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVGeneralClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVGeneralClickScrape.CheckedChanged
        chkTVGeneralClickScrapeAsk.Enabled = chkTVGeneralClickScrape.Checked
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

    Private Sub chkTVScraperShowCert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowCert.CheckedChanged
        Me.SetApplyButton(True)

        If Not Me.chkTVScraperShowCert.Checked Then
            Me.cbTVScraperShowCertLang.Enabled = False
            Me.cbTVScraperShowCertLang.SelectedIndex = 0
            Me.chkTVScraperShowCertForMPAA.Enabled = False
            Me.chkTVScraperShowCertForMPAA.Checked = False
            Me.chkTVScraperShowCertFSK.Enabled = False
            Me.chkTVScraperShowCertFSK.Checked = False
            Me.chkTVScraperShowCertOnlyValue.Enabled = False
            Me.chkTVScraperShowCertOnlyValue.Checked = False
        Else
            Me.cbTVScraperShowCertLang.Enabled = True
            Me.cbTVScraperShowCertLang.SelectedIndex = 0
            Me.chkTVScraperShowCertForMPAA.Enabled = True
            Me.chkTVScraperShowCertFSK.Enabled = True
            Me.chkTVScraperShowCertOnlyValue.Enabled = True
        End If
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

    Private Sub chkTVScraperShowCertForMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowCertForMPAA.CheckedChanged
        Me.SetApplyButton(True)

        If Not Me.chkTVScraperShowCertForMPAA.Checked Then
            Me.chkTVScraperShowCertForMPAAFallback.Enabled = False
            Me.chkTVScraperShowCertForMPAAFallback.Checked = False
        Else
            Me.chkTVScraperShowCertForMPAAFallback.Enabled = True
        End If
    End Sub
    Private Sub chkMovieLevTolerance_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieLevTolerance.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieLevTolerance.Enabled = Me.chkMovieLevTolerance.Checked
        If Not Me.chkMovieLevTolerance.Checked Then Me.txtMovieLevTolerance.Text = String.Empty
    End Sub

    Private Sub chkMovieDisplayYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieDisplayYear.CheckedChanged
        Me.sResult.NeedsReload_Movie = True
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkTVDisplayStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVDisplayStatus.CheckedChanged
        Me.sResult.NeedsReload_TV = True
        Me.SetApplyButton(True)
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

    Private Sub chkTVEpisodeProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVEpisodeProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsReload_TV = True
    End Sub


    Private Sub chkMovieScraperGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperGenre.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieScraperGenreLimit.Enabled = Me.chkMovieScraperGenre.Checked

        If Not Me.chkMovieScraperGenre.Checked Then Me.txtMovieScraperGenreLimit.Text = "0"
    End Sub

    Private Sub chkGeneralDisplayBanner_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayBanner.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayCharacterArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayCharacterArt.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayClearArt.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearLogo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayClearLogo.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayDiscArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayDiscArt.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayFanart.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayLandscape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayLandscape.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayPoster.CheckedChanged
        Me.SetApplyButton(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanartSmall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneralDisplayFanartSmall.CheckedChanged
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

    Private Sub chkMovieScraperPlotForOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperPlotForOutline.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieScraperOutlineLimit.Enabled = Me.chkMovieScraperPlotForOutline.Checked
        Me.chkMovieScraperPlotForOutlineIfEmpty.Enabled = Me.chkMovieScraperPlotForOutline.Checked
        If Not Me.chkMovieScraperPlotForOutline.Checked Then
            Me.txtMovieScraperOutlineLimit.Enabled = False
            Me.chkMovieScraperPlotForOutlineIfEmpty.Checked = False
            Me.chkMovieScraperPlotForOutlineIfEmpty.Enabled = False
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
        Me.sResult.NeedsReload_Movie = True
    End Sub

    Private Sub chkTVAllSeasonsBannerResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVAllSeasonsBannerResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVAllSeasonsBannerWidth.Enabled = chkTVAllSeasonsBannerResize.Checked
        txtTVAllSeasonsBannerHeight.Enabled = chkTVAllSeasonsBannerResize.Checked

        If Not chkTVAllSeasonsBannerResize.Checked Then
            txtTVAllSeasonsBannerWidth.Text = String.Empty
            txtTVAllSeasonsBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsFanartResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVAllSeasonsFanartResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVAllSeasonsFanartWidth.Enabled = chkTVAllSeasonsFanartResize.Checked
        txtTVAllSeasonsFanartHeight.Enabled = chkTVAllSeasonsFanartResize.Checked

        If Not chkTVAllSeasonsFanartResize.Checked Then
            txtTVAllSeasonsFanartWidth.Text = String.Empty
            txtTVAllSeasonsFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsosterResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVAllSeasonsPosterResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVAllSeasonsPosterWidth.Enabled = chkTVAllSeasonsPosterResize.Checked
        txtTVAllSeasonsPosterHeight.Enabled = chkTVAllSeasonsPosterResize.Checked

        If Not chkTVAllSeasonsPosterResize.Checked Then
            txtTVAllSeasonsPosterWidth.Text = String.Empty
            txtTVAllSeasonsPosterHeight.Text = String.Empty
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

    Private Sub chkMovieExtrafanartsResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrafanartsResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieExtrafanartsWidth.Enabled = chkMovieExtrafanartsResize.Checked
        txtMovieExtrafanartsHeight.Enabled = chkMovieExtrafanartsResize.Checked

        If Not chkMovieExtrafanartsResize.Checked Then
            txtMovieExtrafanartsWidth.Text = String.Empty
            txtMovieExtrafanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowExtrafanartsResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowExtrafanartsResize.CheckedChanged
        Me.SetApplyButton(True)

        txtTVShowExtrafanartsWidth.Enabled = chkTVShowExtrafanartsResize.Checked
        txtTVShowExtrafanartsHeight.Enabled = chkTVShowExtrafanartsResize.Checked

        If Not chkTVShowExtrafanartsResize.Checked Then
            txtTVShowExtrafanartsWidth.Text = String.Empty
            txtTVShowExtrafanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieExtrathumbsResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieExtrathumbsResize.CheckedChanged
        Me.SetApplyButton(True)

        txtMovieExtrathumbsWidth.Enabled = chkMovieExtrathumbsResize.Checked
        txtMovieExtrathumbsHeight.Enabled = chkMovieExtrathumbsResize.Checked

        If Not chkMovieExtrathumbsResize.Checked Then
            txtMovieExtrathumbsWidth.Text = String.Empty
            txtMovieExtrathumbsHeight.Text = String.Empty
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

    Private Sub chkTVScraperShowRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperShowRuntime.CheckedChanged
        Me.chkTVScraperUseSRuntimeForEp.Enabled = Me.chkTVScraperShowRuntime.Checked
        If Not Me.chkTVScraperShowRuntime.Checked Then
            Me.chkTVScraperUseSRuntimeForEp.Checked = False
        End If
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

    Private Sub chkTVShowProperCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowProperCase.CheckedChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsReload_TV = True
    End Sub

    Private Sub chkMovieScraperCollectionID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieScraperCollectionID.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieScraperCollectionsAuto.Enabled = Me.chkMovieScraperCollectionID.Checked
        If Not Me.chkMovieScraperCollectionID.Checked Then
            Me.chkMovieScraperCollectionsAuto.Checked = False
        End If
    End Sub

    Private Sub chkTVScraperMetaDataScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVScraperMetaDataScan.CheckedChanged
        Me.SetApplyButton(True)

        Me.cbTVLanguageOverlay.Enabled = Me.chkTVScraperMetaDataScan.Checked

        If Not Me.chkTVScraperMetaDataScan.Checked Then
            Me.cbTVLanguageOverlay.SelectedIndex = 0
        End If
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
        Me.chkMovieBannerExtended.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearArtAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearArtExtended.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearLogoAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearLogoExtended.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrafanartsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrathumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieDiscArtAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieDiscArtExtended.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieFanartFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieLandscapeAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieLandscapeExtended.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieNFOFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMoviePosterFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieTrailerFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieThemeTvTunesEnabled.Enabled = Me.chkMovieUseFrodo.Checked OrElse Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked

        If Not Me.chkMovieUseFrodo.Checked Then
            Me.chkMovieActorThumbsFrodo.Checked = False
            Me.chkMovieBannerAD.Checked = False
            Me.chkMovieBannerExtended.Checked = False
            Me.chkMovieClearArtAD.Checked = False
            Me.chkMovieClearArtExtended.Checked = False
            Me.chkMovieClearLogoAD.Checked = False
            Me.chkMovieClearLogoExtended.Checked = False
            Me.chkMovieDiscArtAD.Checked = False
            Me.chkMovieDiscArtExtended.Checked = False
            Me.chkMovieExtrafanartsFrodo.Checked = False
            Me.chkMovieExtrathumbsFrodo.Checked = False
            Me.chkMovieFanartFrodo.Checked = False
            Me.chkMovieLandscapeAD.Checked = False
            Me.chkMovieLandscapeExtended.Checked = False
            Me.chkMovieNFOFrodo.Checked = False
            Me.chkMoviePosterFrodo.Checked = False
            Me.chkMovieTrailerFrodo.Checked = False
            Me.chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            Me.chkMovieActorThumbsFrodo.Checked = True
            Me.chkMovieBannerExtended.Checked = True
            Me.chkMovieClearArtExtended.Checked = True
            Me.chkMovieClearLogoExtended.Checked = True
            Me.chkMovieDiscArtExtended.Checked = True
            Me.chkMovieExtrafanartsFrodo.Checked = True
            Me.chkMovieExtrathumbsFrodo.Checked = True
            Me.chkMovieFanartFrodo.Checked = True
            Me.chkMovieLandscapeExtended.Checked = True
            Me.chkMovieNFOFrodo.Checked = True
            Me.chkMoviePosterFrodo.Checked = True
            Me.chkMovieTrailerFrodo.Checked = True
        End If

        If Not Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked Then
            Me.chkMovieThemeTvTunesEnabled.Checked = False
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
        Me.chkMovieThemeTvTunesEnabled.Enabled = Me.chkMovieUseEden.Checked OrElse Me.chkMovieUseFrodo.Checked
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
            Me.chkMovieThemeTvTunesEnabled.Checked = False
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
        Me.chkMovieSetPosterMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.txtMovieSetPathMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked

        If Not Me.chkMovieSetUseMSAA.Checked Then
            Me.chkMovieSetBannerMSAA.Checked = False
            Me.chkMovieSetClearArtMSAA.Checked = False
            Me.chkMovieSetClearLogoMSAA.Checked = False
            Me.chkMovieSetFanartMSAA.Checked = False
            Me.chkMovieSetLandscapeMSAA.Checked = False
            Me.chkMovieSetPosterMSAA.Checked = False
        Else
            Me.chkMovieSetBannerMSAA.Checked = True
            Me.chkMovieSetClearArtMSAA.Checked = True
            Me.chkMovieSetClearLogoMSAA.Checked = True
            Me.chkMovieSetFanartMSAA.Checked = True
            Me.chkMovieSetLandscapeMSAA.Checked = True
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

    Private Sub chkMovieThemeTvTunesCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesCustom.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieThemeTvTunesCustomPath.Enabled = Me.chkMovieThemeTvTunesCustom.Checked
        Me.btnMovieThemeTvTunesCustomPathBrowse.Enabled = Me.chkMovieThemeTvTunesCustom.Checked

        If Me.chkMovieThemeTvTunesCustom.Checked Then
            Me.chkMovieThemeTvTunesMoviePath.Enabled = False
            Me.chkMovieThemeTvTunesMoviePath.Checked = False
            Me.chkMovieThemeTvTunesSub.Enabled = False
            Me.chkMovieThemeTvTunesSub.Checked = False
        End If

        If Not Me.chkMovieThemeTvTunesCustom.Checked AndAlso Me.chkMovieThemeTvTunesEnabled.Checked Then
            Me.chkMovieThemeTvTunesMoviePath.Enabled = True
            Me.chkMovieThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkTVShowThemeTvTunesCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowThemeTvTunesCustom.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtTVShowThemeTvTunesCustomPath.Enabled = Me.chkTVShowThemeTvTunesCustom.Checked
        Me.btnTVShowThemeTvTunesCustomPathBrowse.Enabled = Me.chkTVShowThemeTvTunesCustom.Checked

        If Me.chkTVShowThemeTvTunesCustom.Checked Then
            Me.chkTVShowThemeTvTunesShowPath.Enabled = False
            Me.chkTVShowThemeTvTunesShowPath.Checked = False
            Me.chkTVShowThemeTvTunesSub.Enabled = False
            Me.chkTVShowThemeTvTunesSub.Checked = False
        End If

        If Not Me.chkTVShowThemeTvTunesCustom.Checked AndAlso Me.chkTVShowThemeTvTunesEnabled.Checked Then
            Me.chkTVShowThemeTvTunesShowPath.Enabled = True
            Me.chkTVShowThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieThemeTvTunesEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesEnabled.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieThemeTvTunesCustom.Enabled = Me.chkMovieThemeTvTunesEnabled.Checked
        Me.chkMovieThemeTvTunesMoviePath.Enabled = Me.chkMovieThemeTvTunesEnabled.Checked
        Me.chkMovieThemeTvTunesSub.Enabled = Me.chkMovieThemeTvTunesEnabled.Checked

        If Not Me.chkMovieThemeTvTunesEnabled.Checked Then
            Me.chkMovieThemeTvTunesCustom.Checked = False
            Me.chkMovieThemeTvTunesMoviePath.Checked = False
            Me.chkMovieThemeTvTunesSub.Checked = False
        Else
            Me.chkMovieThemeTvTunesMoviePath.Checked = True
        End If
    End Sub

    Private Sub chkTVShowThemeTvTunesEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowThemeTvTunesEnabled.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVShowThemeTvTunesCustom.Enabled = Me.chkTVShowThemeTvTunesEnabled.Checked
        Me.chkTVShowThemeTvTunesShowPath.Enabled = Me.chkTVShowThemeTvTunesEnabled.Checked
        Me.chkTVShowThemeTvTunesSub.Enabled = Me.chkTVShowThemeTvTunesEnabled.Checked

        If Not Me.chkTVShowThemeTvTunesEnabled.Checked Then
            Me.chkTVShowThemeTvTunesCustom.Checked = False
            Me.chkTVShowThemeTvTunesShowPath.Checked = False
            Me.chkTVShowThemeTvTunesSub.Checked = False
        Else
            Me.chkTVShowThemeTvTunesShowPath.Checked = True
        End If
    End Sub

    Private Sub chkMovieThemeTvTunesMoviePath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesMoviePath.CheckedChanged
        Me.SetApplyButton(True)

        If Me.chkMovieThemeTvTunesMoviePath.Checked Then
            Me.chkMovieThemeTvTunesCustom.Enabled = False
            Me.chkMovieThemeTvTunesCustom.Checked = False
            Me.chkMovieThemeTvTunesSub.Enabled = False
            Me.chkMovieThemeTvTunesSub.Checked = False
        End If

        If Not Me.chkMovieThemeTvTunesMoviePath.Checked AndAlso Me.chkMovieThemeTvTunesEnabled.Checked Then
            Me.chkMovieThemeTvTunesCustom.Enabled = True
            Me.chkMovieThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkTVShowThemeTvTunesTVShowPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowThemeTvTunesShowPath.CheckedChanged
        Me.SetApplyButton(True)

        If Me.chkTVShowThemeTvTunesShowPath.Checked Then
            Me.chkTVShowThemeTvTunesCustom.Enabled = False
            Me.chkTVShowThemeTvTunesCustom.Checked = False
            Me.chkTVShowThemeTvTunesSub.Enabled = False
            Me.chkTVShowThemeTvTunesSub.Checked = False
        End If

        If Not Me.chkTVShowThemeTvTunesShowPath.Checked AndAlso Me.chkTVShowThemeTvTunesEnabled.Checked Then
            Me.chkTVShowThemeTvTunesCustom.Enabled = True
            Me.chkTVShowThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieThemeTvTunesSub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesSub.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtMovieThemeTvTunesSubDir.Enabled = Me.chkMovieThemeTvTunesSub.Checked

        If Me.chkMovieThemeTvTunesSub.Checked Then
            Me.chkMovieThemeTvTunesCustom.Enabled = False
            Me.chkMovieThemeTvTunesCustom.Checked = False
            Me.chkMovieThemeTvTunesMoviePath.Enabled = False
            Me.chkMovieThemeTvTunesMoviePath.Checked = False
        End If

        If Not Me.chkMovieThemeTvTunesSub.Checked AndAlso Me.chkMovieThemeTvTunesEnabled.Checked Then
            Me.chkMovieThemeTvTunesCustom.Enabled = True
            Me.chkMovieThemeTvTunesMoviePath.Enabled = True
        End If
    End Sub

    Private Sub chkTVShowThemeTvTunesSub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowThemeTvTunesSub.CheckedChanged
        Me.SetApplyButton(True)

        Me.txtTVShowThemeTvTunesSubDir.Enabled = Me.chkTVShowThemeTvTunesSub.Checked

        If Me.chkTVShowThemeTvTunesSub.Checked Then
            Me.chkTVShowThemeTvTunesCustom.Enabled = False
            Me.chkTVShowThemeTvTunesCustom.Checked = False
            Me.chkTVShowThemeTvTunesShowPath.Enabled = False
            Me.chkTVShowThemeTvTunesShowPath.Checked = False
        End If

        If Not Me.chkTVShowThemeTvTunesSub.Checked AndAlso Me.chkTVShowThemeTvTunesEnabled.Checked Then
            Me.chkTVShowThemeTvTunesCustom.Enabled = True
            Me.chkTVShowThemeTvTunesShowPath.Enabled = True
        End If
    End Sub

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieYAMJWatchedFile.CheckedChanged
        Me.txtMovieYAMJWatchedFolder.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.btnMovieYAMJWatchedFilesBrowse.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.SetApplyButton(True)
    End Sub

    Private Sub ClearTVShowMatching()
        Me.btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(115, "Add Regex")
        Me.btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        Me.btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        Me.txtTVSourcesRegexTVShowMatchingRegex.Text = String.Empty
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = String.Empty
        Me.chkTVSourcesRegexTVShowMatchingByDate.Checked = False
    End Sub

    Private Sub dlgSettings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub EditTVShowMatching(ByVal lItem As ListViewItem)
        Me.btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(124, "Update Regex")
        Me.btnTVSourcesRegexTVShowMatchingAdd.Tag = lItem.Text

        Me.txtTVSourcesRegexTVShowMatchingRegex.Text = lItem.SubItems(1).Text.ToString
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = If(Not lItem.SubItems(2).Text = "-1", lItem.SubItems(2).Text, String.Empty)

        Select Case lItem.SubItems(3).Text
            Case "Yes"
                Me.chkTVSourcesRegexTVShowMatchingByDate.Checked = True
            Case "No"
                Me.chkTVSourcesRegexTVShowMatchingByDate.Checked = False
        End Select
    End Sub

    Private Sub FillGenres()
        If Not String.IsNullOrEmpty(Master.eSettings.GenreFilter) Then
            Dim genreArray() As String
            genreArray = Master.eSettings.GenreFilter.Split(","c)
            For g As Integer = 0 To genreArray.Count - 1
                If Me.clbMovieGenre.FindString(genreArray(g).Trim) > 0 Then Me.clbMovieGenre.SetItemChecked(Me.clbMovieGenre.FindString(genreArray(g).Trim), True)
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
            Me.cbMovieBannerPrefSize.SelectedValue = .MovieBannerPrefSize
            Me.cbMovieExtrafanartsPrefSize.SelectedValue = .MovieExtrafanartsPrefSize
            Me.cbMovieExtrathumbsPrefSize.SelectedValue = .MovieExtrathumbsPrefSize
            Me.cbMovieFanartPrefSize.SelectedValue = .MovieFanartPrefSize
            Me.cbMovieLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.MovieGeneralFlagLang), Master.eLang.Disabled, .MovieGeneralFlagLang)
            Me.cbMoviePosterPrefSize.SelectedValue = .MoviePosterPrefSize
            Me.cbMovieSetBannerPrefSize.SelectedValue = .MovieSetBannerPrefSize
            Me.cbMovieSetFanartPrefSize.SelectedValue = .MovieSetFanartPrefSize
            Me.cbMovieSetPosterPrefSize.SelectedValue = .MovieSetPosterPrefSize
            Me.cbMovieTrailerMinVideoQual.SelectedValue = .MovieTrailerMinVideoQual
            Me.cbMovieTrailerPrefVideoQual.SelectedValue = .MovieTrailerPrefVideoQual
            Me.cbTVAllSeasonsBannerPrefSize.SelectedValue = .TVAllSeasonsBannerPrefSize
            Me.cbTVAllSeasonsBannerPrefType.SelectedValue = .TVAllSeasonsBannerPrefType
            Me.cbTVAllSeasonsFanartPrefSize.SelectedValue = .TVAllSeasonsFanartPrefSize
            Me.cbTVAllSeasonsPosterPrefSize.SelectedValue = .TVAllSeasonsPosterPrefSize
            Me.cbTVEpisodeFanartPrefSize.SelectedValue = .TVEpisodeFanartPrefSize
            Me.cbTVEpisodePosterPrefSize.SelectedValue = .TVEpisodePosterPrefSize
            Me.cbTVLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.TVGeneralFlagLang), Master.eLang.Disabled, .TVGeneralFlagLang)
            Me.cbTVScraperOptionsOrdering.SelectedValue = .TVScraperOptionsOrdering
            Me.cbTVSeasonBannerPrefSize.SelectedValue = .TVSeasonBannerPrefSize
            Me.cbTVSeasonBannerPrefType.SelectedValue = .TVSeasonBannerPrefType
            Me.cbTVSeasonFanartPrefSize.SelectedValue = .TVSeasonFanartPrefSize
            Me.cbTVSeasonPosterPrefSize.SelectedValue = .TVSeasonPosterPrefSize
            Me.cbTVShowBannerPrefSize.SelectedValue = .TVShowBannerPrefSize
            Me.cbTVShowBannerPrefType.SelectedValue = .TVShowBannerPrefType
            Me.cbTVShowExtrafanartsPrefSize.SelectedValue = .TVShowExtrafanartsPrefSize
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
            Me.chkGeneralDigitGrpSymbolVotes.Checked = .GeneralDigitGrpSymbolVotes
            Me.chkGeneralDoubleClickScrape.Checked = .GeneralDoubleClickScrape
            Me.chkGeneralDisplayBanner.Checked = .GeneralDisplayBanner
            Me.chkGeneralDisplayCharacterArt.Checked = .GeneralDisplayCharacterArt
            Me.chkGeneralDisplayClearArt.Checked = .GeneralDisplayClearArt
            Me.chkGeneralDisplayClearLogo.Checked = .GeneralDisplayClearLogo
            Me.chkGeneralDisplayDiscArt.Checked = .GeneralDisplayDiscArt
            Me.chkGeneralDisplayFanart.Checked = .GeneralDisplayFanart
            Me.chkGeneralDisplayFanartSmall.Checked = .GeneralDisplayFanartSmall
            Me.chkGeneralDisplayLandscape.Checked = .GeneralDisplayLandscape
            Me.chkGeneralDisplayPoster.Checked = .GeneralDisplayPoster
            Me.chkGeneralImagesGlassOverlay.Checked = .GeneralImagesGlassOverlay
            Me.chkGeneralOverwriteNfo.Checked = .GeneralOverwriteNfo
            Me.chkGeneralDisplayGenresText.Checked = .GeneralShowGenresText
            Me.chkGeneralDisplayLangFlags.Checked = .GeneralShowLangFlags
            Me.chkGeneralDisplayImgDims.Checked = .GeneralShowImgDims
            Me.chkGeneralDisplayImgNames.Checked = .GeneralShowImgNames
            Me.chkGeneralSourceFromFolder.Checked = .GeneralSourceFromFolder
            Me.chkMovieActorThumbsKeepExisting.Checked = .MovieActorThumbsKeepExisting
            Me.chkMovieSourcesBackdropsAuto.Checked = .MovieBackdropsAuto
            Me.chkMovieBannerKeepExisting.Checked = .MovieBannerKeepExisting
            Me.chkMovieBannerPrefOnly.Checked = .MovieBannerPrefSizeOnly
            Me.chkMovieBannerResize.Checked = .MovieBannerResize
            If .MovieBannerResize Then
                Me.txtMovieBannerHeight.Text = .MovieBannerHeight.ToString
                Me.txtMovieBannerWidth.Text = .MovieBannerWidth.ToString
            End If
            Me.chkMovieCleanDB.Checked = .MovieCleanDB
            Me.chkMovieClearArtKeepExisting.Checked = .MovieClearArtKeepExisting
            Me.chkMovieClearLogoKeepExisting.Checked = .MovieClearLogoKeepExisting
            Me.chkMovieClickScrape.Checked = .MovieClickScrape
            Me.chkMovieClickScrapeAsk.Checked = .MovieClickScrapeAsk
            Me.chkMovieDiscArtKeepExisting.Checked = .MovieDiscArtKeepExisting
            Me.chkMovieDisplayYear.Checked = .MovieDisplayYear
            Me.chkMovieExtrafanartsKeepExisting.Checked = .MovieExtrafanartsKeepExisting
            Me.chkMovieExtrafanartsPrefOnly.Checked = .MovieExtrafanartsPrefSizeOnly
            Me.chkMovieExtrafanartsResize.Checked = .MovieExtrafanartsResize
            If .MovieExtrafanartsResize Then
                Me.txtMovieExtrafanartsHeight.Text = .MovieExtrafanartsHeight.ToString
                Me.txtMovieExtrafanartsWidth.Text = .MovieExtrafanartsWidth.ToString
            End If
            Me.chkMovieExtrathumbsKeepExisting.Checked = .MovieExtrathumbsKeepExisting
            Me.chkMovieExtrathumbsPrefOnly.Checked = .MovieExtrathumbsPrefSizeOnly
            Me.chkMovieExtrathumbsResize.Checked = .MovieExtrathumbsResize
            If .MovieExtrathumbsResize Then
                Me.txtMovieExtrathumbsHeight.Text = .MovieExtrathumbsHeight.ToString
                Me.txtMovieExtrathumbsWidth.Text = .MovieExtrathumbsWidth.ToString
            End If
            Me.chkMovieFanartKeepExisting.Checked = .MovieFanartKeepExisting
            Me.chkMovieFanartPrefOnly.Checked = .MovieFanartPrefSizeOnly
            Me.chkMovieFanartResize.Checked = .MovieFanartResize
            If .MovieFanartResize Then
                Me.txtMovieFanartHeight.Text = .MovieFanartHeight.ToString
                Me.txtMovieFanartWidth.Text = .MovieFanartWidth.ToString
            End If
            Me.chkMovieGeneralIgnoreLastScan.Checked = .MovieGeneralIgnoreLastScan
            Me.chkMovieGeneralMarkNew.Checked = .MovieGeneralMarkNew
            Me.chkMovieImagesCacheEnabled.Checked = .MovieImagesCacheEnabled
            Me.chkMovieImagesDisplayImageSelect.Checked = .MovieImagesDisplayImageSelect
            If .MovieImagesMediaLanguageOnly Then
                Me.chkMovieImagesMediaLanguageOnly.Checked = True
                Me.chkMovieImagesGetBlankImages.Checked = .MovieImagesGetBlankImages
                Me.chkMovieImagesGetEnglishImages.Checked = .MovieImagesGetEnglishImages
            End If
            Me.chkMovieImagesNotSaveURLToNfo.Checked = .MovieImagesNotSaveURLToNfo
            Me.chkMovieLandscapeKeepExisting.Checked = .MovieLandscapeKeepExisting
            Me.chkMovieLockActors.Checked = .MovieLockActors
            Me.chkMovieLockCert.Checked = .MovieLockCert
            Me.chkMovieLockCountry.Checked = .MovieLockCountry
            Me.chkMovieLockCollectionID.Checked = .MovieLockCollectionID
            Me.chkMovieLockCollections.Checked = .MovieLockCollections
            Me.chkMovieLockDirector.Checked = .MovieLockDirector
            Me.chkMovieLockGenre.Checked = .MovieLockGenre
            Me.chkMovieLockLanguageA.Checked = .MovieLockLanguageA
            Me.chkMovieLockLanguageV.Checked = .MovieLockLanguageV
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
            Me.chkMovieLockCredits.Checked = .MovieLockCredits
            Me.chkMovieLockYear.Checked = .MovieLockYear
            Me.chkMoviePosterKeepExisting.Checked = .MoviePosterKeepExisting
            Me.chkMoviePosterPrefOnly.Checked = .MoviePosterPrefSizeOnly
            Me.chkMoviePosterResize.Checked = .MoviePosterResize
            If .MoviePosterResize Then
                Me.txtMoviePosterHeight.Text = .MoviePosterHeight.ToString
                Me.txtMoviePosterWidth.Text = .MoviePosterWidth.ToString
            End If
            Me.chkMovieProperCase.Checked = .MovieProperCase
            Me.chkMovieSetBannerKeepExisting.Checked = .MovieSetBannerKeepExisting
            Me.chkMovieSetBannerPrefOnly.Checked = .MovieSetBannerPrefSizeOnly
            Me.chkMovieSetBannerResize.Checked = .MovieSetBannerResize
            If .MovieSetBannerResize Then
                Me.txtMovieSetBannerHeight.Text = .MovieSetBannerHeight.ToString
                Me.txtMovieSetBannerWidth.Text = .MovieSetBannerWidth.ToString
            End If
            Me.chkMovieSetCleanDB.Checked = .MovieSetCleanDB
            Me.chkMovieSetCleanFiles.Checked = .MovieSetCleanFiles
            Me.chkMovieSetClearArtKeepExisting.Checked = .MovieSetClearArtKeepExisting
            Me.chkMovieSetClearLogoKeepExisting.Checked = .MovieSetClearLogoKeepExisting
            Me.chkMovieSetClickScrape.Checked = .MovieSetClickScrape
            Me.chkMovieSetClickScrapeAsk.Checked = .MovieSetClickScrapeAsk
            Me.chkMovieSetDiscArtKeepExisting.Checked = .MovieSetDiscArtKeepExisting
            Me.chkMovieSetFanartKeepExisting.Checked = .MovieSetFanartKeepExisting
            Me.chkMovieSetFanartPrefOnly.Checked = .MovieSetFanartPrefSizeOnly
            Me.chkMovieSetFanartResize.Checked = .MovieSetFanartResize
            If .MovieSetFanartResize Then
                Me.txtMovieSetFanartHeight.Text = .MovieSetFanartHeight.ToString
                Me.txtMovieSetFanartWidth.Text = .MovieSetFanartWidth.ToString
            End If
            Me.chkMovieSetGeneralMarkNew.Checked = .MovieSetGeneralMarkNew
            Me.chkMovieSetImagesCacheEnabled.Checked = .MovieSetImagesCacheEnabled
            Me.chkMovieSetImagesDisplayImageSelect.Checked = .MovieSetImagesDisplayImageSelect
            If .MovieSetImagesMediaLanguageOnly Then
                Me.chkMovieSetImagesMediaLanguageOnly.Checked = True
                Me.chkMovieSetImagesGetBlankImages.Checked = .MovieSetImagesGetBlankImages
                Me.chkMovieSetImagesGetEnglishImages.Checked = .MovieSetImagesGetEnglishImages
            End If
            Me.chkMovieSetLandscapeKeepExisting.Checked = .MovieSetLandscapeKeepExisting
            Me.chkMovieSetLockPlot.Checked = .MovieSetLockPlot
            Me.chkMovieSetLockTitle.Checked = .MovieSetLockTitle
            Me.chkMovieSetPosterKeepExisting.Checked = .MovieSetPosterKeepExisting
            Me.chkMovieSetPosterPrefOnly.Checked = .MovieSetPosterPrefSizeOnly
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
            Me.chkMovieScraperOriginalTitle.Checked = .MovieScraperOriginalTitle
            Me.chkMovieScraperDetailView.Checked = .MovieScraperUseDetailView
            Me.chkMovieScraperOutline.Checked = .MovieScraperOutline
            Me.chkMovieScraperPlot.Checked = .MovieScraperPlot
            Me.chkMovieScraperPlotForOutline.Checked = .MovieScraperPlotForOutline
            Me.chkMovieScraperPlotForOutlineIfEmpty.Checked = .MovieScraperPlotForOutlineIfEmpty
            Me.chkMovieScraperRating.Checked = .MovieScraperRating
            Me.chkMovieScraperRelease.Checked = .MovieScraperRelease
            Me.chkMovieScraperReleaseFormat.Checked = .MovieScraperReleaseFormat
            Me.chkMovieScraperRuntime.Checked = .MovieScraperRuntime
            Me.chkMovieScraperStudio.Checked = .MovieScraperStudio
            Me.chkMovieScraperStudioWithImg.Checked = .MovieScraperStudioWithImgOnly
            Me.chkMovieScraperTagline.Checked = .MovieScraperTagline
            Me.chkMovieScraperTitle.Checked = .MovieScraperTitle
            Me.chkMovieScraperTop250.Checked = .MovieScraperTop250
            Me.chkMovieScraperTrailer.Checked = .MovieScraperTrailer
            Me.chkMovieScraperUseMDDuration.Checked = .MovieScraperUseMDDuration
            Me.chkMovieScraperCredits.Checked = .MovieScraperCredits
            Me.chkMovieScraperXBMCTrailerFormat.Checked = .MovieScraperXBMCTrailerFormat
            Me.chkMovieScraperYear.Checked = .MovieScraperYear
            Me.chkMovieSkipStackedSizeCheck.Checked = .MovieSkipStackedSizeCheck
            Me.chkMovieSortBeforeScan.Checked = .MovieSortBeforeScan
            Me.chkMovieThemeKeepExisting.Checked = .MovieThemeKeepExisting
            Me.chkMovieTrailerKeepExisting.Checked = .MovieTrailerKeepExisting
            Me.chkTVAllSeasonsBannerKeepExisting.Checked = .TVAllSeasonsBannerKeepExisting
            Me.chkTVAllSeasonsBannerPrefSizeOnly.Checked = .TVAllSeasonsBannerPrefSizeOnly
            Me.chkTVAllSeasonsBannerResize.Checked = .TVAllSeasonsBannerResize
            If .TVAllSeasonsBannerResize Then
                Me.txtTVAllSeasonsBannerHeight.Text = .TVAllSeasonsBannerHeight.ToString
                Me.txtTVAllSeasonsBannerWidth.Text = .TVAllSeasonsBannerWidth.ToString
            End If
            Me.chkTVAllSeasonsFanartKeepExisting.Checked = .TVAllSeasonsFanartKeepExisting
            Me.chkTVAllSeasonsFanartPrefSizeOnly.Checked = .TVAllSeasonsFanartPrefSizeOnly
            Me.chkTVAllSeasonsFanartResize.Checked = .TVAllSeasonsFanartResize
            If .TVAllSeasonsFanartResize Then
                Me.txtTVAllSeasonsFanartHeight.Text = .TVAllSeasonsFanartHeight.ToString
                Me.txtTVAllSeasonsFanartWidth.Text = .TVAllSeasonsFanartWidth.ToString
            End If
            Me.chkTVAllSeasonsLandscapeKeepExisting.Checked = .TVAllSeasonsLandscapeKeepExisting
            Me.chkTVAllSeasonsPosterKeepExisting.Checked = .TVAllSeasonsPosterKeepExisting
            Me.chkTVAllSeasonsPosterPrefSizeOnly.Checked = .TVAllSeasonsPosterPrefSizeOnly
            Me.chkTVAllSeasonsPosterResize.Checked = .TVAllSeasonsPosterResize
            If .TVAllSeasonsPosterResize Then
                Me.txtTVAllSeasonsPosterHeight.Text = .TVAllSeasonsPosterHeight.ToString
                Me.txtTVAllSeasonsPosterWidth.Text = .TVAllSeasonsPosterWidth.ToString
            End If
            Me.chkTVCleanDB.Checked = .TVCleanDB
            Me.chkTVDisplayMissingEpisodes.Checked = .TVDisplayMissingEpisodes
            Me.chkTVDisplayStatus.Checked = .TVDisplayStatus
            Me.chkTVEpisodeFanartKeepExisting.Checked = .TVEpisodeFanartKeepExisting
            Me.chkTVEpisodeFanartPrefSizeOnly.Checked = .TVEpisodeFanartPrefSizeOnly
            Me.chkTVEpisodeFanartResize.Checked = .TVEpisodeFanartResize
            If .TVEpisodeFanartResize Then
                Me.txtTVEpisodeFanartHeight.Text = .TVEpisodeFanartHeight.ToString
                Me.txtTVEpisodeFanartWidth.Text = .TVEpisodeFanartWidth.ToString
            End If
            Me.chkTVEpisodeNoFilter.Checked = .TVEpisodeNoFilter
            Me.chkTVEpisodePosterKeepExisting.Checked = .TVEpisodePosterKeepExisting
            Me.chkTVEpisodePosterPrefSizeOnly.Checked = .TVEpisodePosterPrefSizeOnly
            Me.chkTVEpisodePosterResize.Checked = .TVEpisodePosterResize
            If .TVEpisodePosterResize Then
                Me.txtTVEpisodePosterHeight.Text = .TVEpisodePosterHeight.ToString
                Me.txtTVEpisodePosterWidth.Text = .TVEpisodePosterWidth.ToString
            End If
            Me.chkTVEpisodeProperCase.Checked = .TVEpisodeProperCase
            Me.chkTVGeneralClickScrape.Checked = .TVGeneralClickScrape
            Me.chkTVGeneralClickScrapeAsk.Checked = .TVGeneralClickScrapeAsk
            Me.chkTVGeneralMarkNewEpisodes.Checked = .TVGeneralMarkNewEpisodes
            Me.chkTVGeneralMarkNewShows.Checked = .TVGeneralMarkNewShows
            Me.chkTVGeneralIgnoreLastScan.Checked = .TVGeneralIgnoreLastScan
            Me.chkTVImagesCacheEnabled.Checked = .TVImagesCacheEnabled
            Me.chkTVImagesDisplayImageSelect.Checked = .TVImagesDisplayImageSelect
            If .TVImagesMediaLanguageOnly Then
                Me.chkTVImagesMediaLanguageOnly.Checked = True
                Me.chkTVImagesGetBlankImages.Checked = .TVImagesGetBlankImages
                Me.chkTVImagesGetEnglishImages.Checked = .TVImagesGetEnglishImages
            End If
            Me.chkTVLockEpisodeLanguageA.Checked = .TVLockEpisodeLanguageA
            Me.chkTVLockEpisodeLanguageV.Checked = .TVLockEpisodeLanguageV
            Me.chkTVLockEpisodePlot.Checked = .TVLockEpisodePlot
            Me.chkTVLockEpisodeRating.Checked = .TVLockEpisodeRating
            Me.chkTVLockEpisodeRuntime.Checked = .TVLockEpisodeRuntime
            Me.chkTVLockEpisodeTitle.Checked = .TVLockEpisodeTitle
            Me.chkTVLockSeasonPlot.Checked = .TVLockSeasonPlot
            Me.chkTVLockSeasonTitle.Checked = .TVLockSeasonTitle
            Me.chkTVLockShowCert.Checked = .TVLockShowCert
            Me.chkTVLockShowCreators.Checked = .TVLockShowCreators
            Me.chkTVLockShowGenre.Checked = .TVLockShowGenre
            Me.chkTVLockShowMPAA.Checked = .TVLockShowMPAA
            Me.chkTVLockShowOriginalTitle.Checked = .TVLockShowOriginalTitle
            Me.chkTVLockShowPlot.Checked = .TVLockShowPlot
            Me.chkTVLockShowRating.Checked = .TVLockShowRating
            Me.chkTVLockShowRuntime.Checked = .TVLockShowRuntime
            Me.chkTVLockShowStatus.Checked = .TVLockShowStatus
            Me.chkTVLockShowStudio.Checked = .TVLockShowStudio
            Me.chkTVLockShowTitle.Checked = .TVLockShowTitle
            Me.chkTVScanOrderModify.Checked = .TVScanOrderModify
            Me.chkTVScraperCleanFields.Checked = .TVScraperCleanFields
            Me.chkTVScraperEpisodeActors.Checked = .TVScraperEpisodeActors
            Me.chkTVScraperEpisodeAired.Checked = .TVScraperEpisodeAired
            Me.chkTVScraperEpisodeCredits.Checked = .TVScraperEpisodeCredits
            Me.chkTVScraperEpisodeDirector.Checked = .TVScraperEpisodeDirector
            Me.chkTVScraperEpisodeGuestStars.Checked = .TVScraperEpisodeGuestStars
            Me.chkTVScraperEpisodeGuestStarsToActors.Checked = .TVScraperEpisodeGuestStarsToActors
            Me.chkTVScraperEpisodePlot.Checked = .TVScraperEpisodePlot
            Me.chkTVScraperEpisodeRating.Checked = .TVScraperEpisodeRating
            Me.chkTVScraperEpisodeRuntime.Checked = .TVScraperEpisodeRuntime
            Me.chkTVScraperEpisodeTitle.Checked = .TVScraperEpisodeTitle
            Me.chkTVScraperMetaDataScan.Checked = .TVScraperMetaDataScan
            Me.chkTVScraperSeasonAired.Checked = .TVScraperSeasonAired
            Me.chkTVScraperSeasonPlot.Checked = .TVScraperSeasonPlot
            Me.chkTVScraperSeasonTitle.Checked = .TVScraperSeasonTitle
            Me.chkTVScraperShowActors.Checked = .TVScraperShowActors
            Me.chkTVScraperShowCert.Checked = .TVScraperShowCert
            Me.chkTVScraperShowCreators.Checked = .TVScraperShowCreators
            Me.chkTVScraperShowCertForMPAA.Checked = .TVScraperShowCertForMPAA
            Me.chkTVScraperShowCertForMPAAFallback.Checked = .TVScraperShowCertForMPAAFallback
            Me.chkTVScraperShowCertFSK.Checked = .TVScraperShowCertFSK
            Me.chkTVScraperShowCertOnlyValue.Checked = .TVScraperShowCertOnlyValue
            Me.chkTVScraperShowEpiGuideURL.Checked = .TVScraperShowEpiGuideURL
            Me.chkTVScraperShowGenre.Checked = .TVScraperShowGenre
            Me.chkTVScraperShowMPAA.Checked = .TVScraperShowMPAA
            Me.chkTVScraperShowOriginalTitle.Checked = .TVScraperShowOriginalTitle
            Me.chkTVScraperShowPlot.Checked = .TVScraperShowPlot
            Me.chkTVScraperShowPremiered.Checked = .TVScraperShowPremiered
            Me.chkTVScraperShowRating.Checked = .TVScraperShowRating
            Me.chkTVScraperShowRuntime.Checked = .TVScraperShowRuntime
            Me.chkTVScraperShowStatus.Checked = .TVScraperShowStatus
            Me.chkTVScraperShowStudio.Checked = .TVScraperShowStudio
            Me.chkTVScraperShowTitle.Checked = .TVScraperShowTitle
            Me.chkTVScraperUseDisplaySeasonEpisode.Checked = .TVScraperUseDisplaySeasonEpisode
            Me.chkTVScraperUseMDDuration.Checked = .TVScraperUseMDDuration
            Me.chkTVScraperUseSRuntimeForEp.Checked = .TVScraperUseSRuntimeForEp
            Me.chkTVSeasonBannerKeepExisting.Checked = .TVSeasonBannerKeepExisting
            Me.chkTVSeasonBannerPrefSizeOnly.Checked = .TVSeasonBannerPrefSizeOnly
            Me.chkTVSeasonBannerResize.Checked = .TVSeasonBannerResize
            If .TVSeasonBannerResize Then
                Me.txtTVSeasonBannerHeight.Text = .TVSeasonBannerHeight.ToString
                Me.txtTVSeasonBannerWidth.Text = .TVSeasonBannerWidth.ToString
            End If
            Me.chkTVShowExtrafanartsKeepExisting.Checked = .TVShowExtrafanartsKeepExisting
            Me.chkTVShowExtrafanartsPrefSizeOnly.Checked = .TVShowExtrafanartsPrefOnly
            Me.chkTVShowExtrafanartsResize.Checked = .TVShowExtrafanartsResize
            If .TVShowExtrafanartsResize Then
                Me.txtTVShowExtrafanartsHeight.Text = .TVShowExtrafanartsHeight.ToString
                Me.txtTVShowExtrafanartsWidth.Text = .TVShowExtrafanartsWidth.ToString
            End If
            Me.chkTVSeasonFanartKeepExisting.Checked = .TVSeasonFanartKeepExisting
            Me.chkTVSeasonFanartPrefSizeOnly.Checked = .TVSeasonFanartPrefSizeOnly
            Me.chkTVSeasonFanartResize.Checked = .TVSeasonFanartResize
            If .TVSeasonFanartResize Then
                Me.txtTVSeasonFanartHeight.Text = .TVSeasonFanartHeight.ToString
                Me.txtTVSeasonFanartWidth.Text = .TVSeasonFanartWidth.ToString
            End If
            Me.chkTVSeasonLandscapeKeepExisting.Checked = .TVSeasonLandscapeKeepExisting
            Me.chkTVSeasonPosterKeepExisting.Checked = .TVSeasonPosterKeepExisting
            Me.chkTVSeasonPosterPrefSizeOnly.Checked = .TVSeasonPosterPrefSizeOnly
            Me.chkTVSeasonPosterResize.Checked = .TVSeasonPosterResize
            If .TVSeasonPosterResize Then
                Me.txtTVSeasonPosterHeight.Text = .TVSeasonPosterHeight.ToString
                Me.txtTVSeasonPosterWidth.Text = .TVSeasonPosterWidth.ToString
            End If
            Me.chkTVShowBannerKeepExisting.Checked = .TVShowBannerKeepExisting
            Me.chkTVShowBannerPrefSizeOnly.Checked = .TVShowBannerPrefSizeOnly
            Me.chkTVShowBannerResize.Checked = .TVShowBannerResize
            If .TVShowBannerResize Then
                Me.txtTVShowBannerHeight.Text = .TVShowBannerHeight.ToString
                Me.txtTVShowBannerWidth.Text = .TVShowBannerWidth.ToString
            End If
            Me.chkTVShowCharacterArtKeepExisting.Checked = .TVShowCharacterArtKeepExisting
            Me.chkTVShowClearArtKeepExisting.Checked = .TVShowClearArtKeepExisting
            Me.chkTVShowClearLogoKeepExisting.Checked = .TVShowClearLogoKeepExisting
            Me.chkTVShowFanartKeepExisting.Checked = .TVShowFanartKeepExisting
            Me.chkTVShowFanartPrefSizeOnly.Checked = .TVShowFanartPrefSizeOnly
            Me.chkTVShowFanartResize.Checked = .TVShowFanartResize
            If .TVShowFanartResize Then
                Me.txtTVShowFanartHeight.Text = .TVShowFanartHeight.ToString
                Me.txtTVShowFanartWidth.Text = .TVShowFanartWidth.ToString
            End If
            Me.chkTVShowLandscapeKeepExisting.Checked = .TVShowLandscapeKeepExisting
            Me.chkTVShowPosterKeepExisting.Checked = .TVShowPosterKeepExisting
            Me.chkTVShowPosterPrefSizeOnly.Checked = .TVShowPosterPrefSizeOnly
            Me.chkTVShowPosterResize.Checked = .TVShowPosterResize
            If .TVShowPosterResize Then
                Me.txtTVShowPosterHeight.Text = .TVShowPosterHeight.ToString
                Me.txtTVShowPosterWidth.Text = .TVShowPosterWidth.ToString
            End If
            Me.chkTVShowProperCase.Checked = .TVShowProperCase
            Me.chkTVShowThemeKeepExisting.Checked = .TVShowThemeKeepExisting
            Me.lstFileSystemCleanerWhitelist.Items.AddRange(.FileSystemCleanerWhitelistExts.ToArray)
            Me.lstFileSystemNoStackExts.Items.AddRange(.FileSystemNoStackExts.ToArray)
            Me.tcFileSystemCleaner.SelectedTab = If(.FileSystemExpertCleaner, Me.tpFileSystemCleanerExpert, Me.tpFileSystemCleanerStandard)
            Me.txtGeneralDaemonPath.Text = .GeneralDaemonPath.ToString
            Me.txtMovieSourcesBackdropsFolderPath.Text = .MovieBackdropsPath.ToString
            Me.txtMovieExtrafanartsLimit.Text = .MovieExtrafanartsLimit.ToString
            Me.txtMovieExtrathumbsLimit.Text = .MovieExtrathumbsLimit.ToString
            Me.txtMovieGeneralCustomMarker1.Text = .MovieGeneralCustomMarker1Name.ToString
            Me.txtMovieGeneralCustomMarker2.Text = .MovieGeneralCustomMarker2Name.ToString
            Me.txtMovieGeneralCustomMarker3.Text = .MovieGeneralCustomMarker3Name.ToString
            Me.txtMovieGeneralCustomMarker4.Text = .MovieGeneralCustomMarker4Name.ToString
            Me.txtMovieIMDBURL.Text = .MovieIMDBURL.ToString
            Me.txtMovieScraperCastLimit.Text = .MovieScraperCastLimit.ToString
            Me.txtMovieScraperDurationRuntimeFormat.Text = .MovieScraperDurationRuntimeFormat
            Me.txtMovieScraperGenreLimit.Text = .MovieScraperGenreLimit.ToString
            Me.txtMovieScraperMPAANotRated.Text = .MovieScraperMPAANotRated
            Me.txtMovieScraperOutlineLimit.Text = .MovieScraperOutlineLimit.ToString
            Me.txtMovieScraperStudioLimit.Text = .MovieScraperStudioLimit.ToString
            Me.txtMovieSkipLessThan.Text = .MovieSkipLessThan.ToString
            Me.txtMovieTrailerDefaultSearch.Text = .MovieTrailerDefaultSearch.ToString
            Me.txtTVScraperDurationRuntimeFormat.Text = .TVScraperDurationRuntimeFormat.ToString
            Me.txtTVScraperShowMPAANotRated.Text = .TVScraperShowMPAANotRated
            Me.txtTVShowExtrafanartsLimit.Text = .TVShowExtrafanartsLimit.ToString
            Me.txtTVSourcesRegexMultiPartMatching.Text = .TVMultiPartMatching
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

            Me.TVShowMatching.AddRange(.TVShowMatching)
            Me.LoadTVShowMatching()

            Try
                Me.cbMovieScraperCertLang.Items.Clear()
                Me.cbMovieScraperCertLang.Items.Add(Master.eLang.All)
                Me.cbMovieScraperCertLang.Items.AddRange((From lLang In APIXML.CertLanguagesXML.Language Select lLang.name).ToArray)
                If Me.cbMovieScraperCertLang.Items.Count > 0 Then
                    If .MovieScraperCertLang = Master.eLang.All Then
                        Me.cbMovieScraperCertLang.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.MovieScraperCertLang) Then
                        Me.cbMovieScraperCertLang.Text = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = .MovieScraperCertLang).name
                    Else
                        Me.cbMovieScraperCertLang.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Try
                Me.cbMovieGeneralLang.Items.Clear()
                Me.cbMovieGeneralLang.Items.AddRange((From lLang In .TVGeneralLanguages.Language Select lLang.name).ToArray)
                If Me.cbMovieGeneralLang.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.MovieGeneralLanguage) Then
                        Me.cbMovieGeneralLang.Text = .TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .MovieGeneralLanguage).name
                    Else
                        Me.cbMovieGeneralLang.Text = .TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = "en").name
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Try
                Me.cbTVScraperShowCertLang.Items.Clear()
                Me.cbTVScraperShowCertLang.Items.Add(Master.eLang.All)
                Me.cbTVScraperShowCertLang.Items.AddRange((From lLang In APIXML.CertLanguagesXML.Language Select lLang.name).ToArray)
                If Me.cbTVScraperShowCertLang.Items.Count > 0 Then
                    If .TVScraperShowCertLang = Master.eLang.All Then
                        Me.cbTVScraperShowCertLang.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.TVScraperShowCertLang) Then
                        Me.cbTVScraperShowCertLang.Text = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = .TVScraperShowCertLang).name
                    Else
                        Me.cbTVScraperShowCertLang.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Try
                Me.cbTVGeneralLang.Items.Clear()
                Me.cbTVGeneralLang.Items.AddRange((From lLang In .TVGeneralLanguages.Language Select lLang.name).ToArray)
                If Me.cbTVGeneralLang.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.TVGeneralLanguage) Then
                        Me.cbTVGeneralLang.Text = .TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .TVGeneralLanguage).name
                    Else
                        Me.cbTVGeneralLang.Text = .TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = "en").name
                    End If
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
            Me.chkTVGeneralClickScrapeAsk.Enabled = Me.chkTVGeneralClickScrape.Checked
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

            '********* XBMC Extended Images settings ***********
            Me.chkMovieBannerExtended.Checked = .MovieBannerExtended
            Me.chkMovieClearArtExtended.Checked = .MovieClearArtExtended
            Me.chkMovieClearLogoExtended.Checked = .MovieClearLogoExtended
            Me.chkMovieDiscArtExtended.Checked = .MovieDiscArtExtended
            Me.chkMovieLandscapeExtended.Checked = .MovieLandscapeExtended

            '************** XBMC TvTunes settings **************
            Me.chkMovieThemeTvTunesEnabled.Checked = .MovieThemeTvTunesEnable
            Me.chkMovieThemeTvTunesCustom.Checked = .MovieThemeTvTunesCustom
            Me.chkMovieThemeTvTunesMoviePath.Checked = .MovieThemeTvTunesMoviePath
            Me.chkMovieThemeTvTunesSub.Checked = .MovieThemeTvTunesSub
            Me.txtMovieThemeTvTunesCustomPath.Text = .MovieThemeTvTunesCustomPath
            Me.txtMovieThemeTvTunesSubDir.Text = .MovieThemeTvTunesSubDir

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
            Me.chkMovieExtrafanartsExpertSingle.Checked = .MovieExtrafanartsExpertSingle
            Me.chkMovieExtrathumbsExpertSingle.Checked = .MovieExtrathumbsExpertSingle
            Me.chkMovieStackExpertSingle.Checked = .MovieStackExpertSingle
            Me.chkMovieUnstackExpertSingle.Checked = .MovieUnstackExpertSingle
            Me.txtMovieActorThumbsExtExpertSingle.Text = .MovieActorThumbsExtExpertSingle
            Me.txtMovieBannerExpertSingle.Text = .MovieBannerExpertSingle
            Me.txtMovieClearArtExpertSingle.Text = .MovieClearArtExpertSingle
            Me.txtMovieClearLogoExpertSingle.Text = .MovieClearLogoExpertSingle
            Me.txtMovieDiscArtExpertSingle.Text = .MovieDiscArtExpertSingle
            Me.txtMovieFanartExpertSingle.Text = .MovieFanartExpertSingle
            Me.txtMovieLandscapeExpertSingle.Text = .MovieLandscapeExpertSingle
            Me.txtMovieNFOExpertSingle.Text = .MovieNFOExpertSingle
            Me.txtMoviePosterExpertSingle.Text = .MoviePosterExpertSingle
            Me.txtMovieTrailerExpertSingle.Text = .MovieTrailerExpertSingle

            '******************* Expert Multi ******************
            Me.chkMovieActorThumbsExpertMulti.Checked = .MovieActorThumbsExpertMulti
            Me.chkMovieUnstackExpertMulti.Checked = .MovieUnstackExpertMulti
            Me.chkMovieStackExpertMulti.Checked = .MovieStackExpertMulti
            Me.txtMovieActorThumbsExtExpertMulti.Text = .MovieActorThumbsExtExpertMulti
            Me.txtMovieBannerExpertMulti.Text = .MovieBannerExpertMulti
            Me.txtMovieClearArtExpertMulti.Text = .MovieClearArtExpertMulti
            Me.txtMovieClearLogoExpertMulti.Text = .MovieClearLogoExpertMulti
            Me.txtMovieDiscArtExpertMulti.Text = .MovieDiscArtExpertMulti
            Me.txtMovieFanartExpertMulti.Text = .MovieFanartExpertMulti
            Me.txtMovieLandscapeExpertMulti.Text = .MovieLandscapeExpertMulti
            Me.txtMovieNFOExpertMulti.Text = .MovieNFOExpertMulti
            Me.txtMoviePosterExpertMulti.Text = .MoviePosterExpertMulti
            Me.txtMovieTrailerExpertMulti.Text = .MovieTrailerExpertMulti

            '******************* Expert VTS *******************
            Me.chkMovieActorThumbsExpertVTS.Checked = .MovieActorThumbsExpertVTS
            Me.chkMovieExtrafanartsExpertVTS.Checked = .MovieExtrafanartsExpertVTS
            Me.chkMovieExtrathumbsExpertVTS.Checked = .MovieExtrathumbsExpertVTS
            Me.chkMovieRecognizeVTSExpertVTS.Checked = .MovieRecognizeVTSExpertVTS
            Me.chkMovieUseBaseDirectoryExpertVTS.Checked = .MovieUseBaseDirectoryExpertVTS
            Me.txtMovieActorThumbsExtExpertVTS.Text = .MovieActorThumbsExtExpertVTS
            Me.txtMovieBannerExpertVTS.Text = .MovieBannerExpertVTS
            Me.txtMovieClearArtExpertVTS.Text = .MovieClearArtExpertVTS
            Me.txtMovieClearLogoExpertVTS.Text = .MovieClearLogoExpertVTS
            Me.txtMovieDiscArtExpertVTS.Text = .MovieDiscArtExpertVTS
            Me.txtMovieFanartExpertVTS.Text = .MovieFanartExpertVTS
            Me.txtMovieLandscapeExpertVTS.Text = .MovieLandscapeExpertVTS
            Me.txtMovieNFOExpertVTS.Text = .MovieNFOExpertVTS
            Me.txtMoviePosterExpertVTS.Text = .MoviePosterExpertVTS
            Me.txtMovieTrailerExpertVTS.Text = .MovieTrailerExpertVTS

            '******************* Expert BDMV *******************
            Me.chkMovieActorThumbsExpertBDMV.Checked = .MovieActorThumbsExpertBDMV
            Me.chkMovieExtrafanartsExpertBDMV.Checked = .MovieExtrafanartsExpertBDMV
            Me.chkMovieExtrathumbsExpertBDMV.Checked = .MovieExtrathumbsExpertBDMV
            Me.chkMovieUseBaseDirectoryExpertBDMV.Checked = .MovieUseBaseDirectoryExpertBDMV
            Me.txtMovieActorThumbsExtExpertBDMV.Text = .MovieActorThumbsExtExpertBDMV
            Me.txtMovieBannerExpertBDMV.Text = .MovieBannerExpertBDMV
            Me.txtMovieClearArtExpertBDMV.Text = .MovieClearArtExpertBDMV
            Me.txtMovieClearLogoExpertBDMV.Text = .MovieClearLogoExpertBDMV
            Me.txtMovieDiscArtExpertBDMV.Text = .MovieDiscArtExpertBDMV
            Me.txtMovieFanartExpertBDMV.Text = .MovieFanartExpertBDMV
            Me.txtMovieLandscapeExpertBDMV.Text = .MovieLandscapeExpertBDMV
            Me.txtMovieNFOExpertBDMV.Text = .MovieNFOExpertBDMV
            Me.txtMoviePosterExpertBDMV.Text = .MoviePosterExpertBDMV
            Me.txtMovieTrailerExpertBDMV.Text = .MovieTrailerExpertBDMV


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

            '********* Kodi Extended Images settings ***********
            Me.chkMovieSetBannerExtended.Checked = .MovieSetBannerExtended
            Me.chkMovieSetClearArtExtended.Checked = .MovieSetClearArtExtended
            Me.chkMovieSetClearLogoExtended.Checked = .MovieSetClearLogoExtended
            Me.chkMovieSetDiscArtExtended.Checked = .MovieSetDiscArtExtended
            Me.chkMovieSetFanartExtended.Checked = .MovieSetFanartExtended
            Me.chkMovieSetLandscapeExtended.Checked = .MovieSetLandscapeExtended
            Me.chkMovieSetPosterExtended.Checked = .MovieSetPosterExtended
            Me.txtMovieSetPathExtended.Text = .MovieSetPathExtended

            '**************** XBMC MSAA settings ***************
            Me.chkMovieSetUseMSAA.Checked = .MovieSetUseMSAA
            Me.chkMovieSetBannerMSAA.Checked = .MovieSetBannerMSAA
            Me.chkMovieSetClearArtMSAA.Checked = .MovieSetClearArtMSAA
            Me.chkMovieSetClearLogoMSAA.Checked = .MovieSetClearLogoMSAA
            Me.chkMovieSetFanartMSAA.Checked = .MovieSetFanartMSAA
            Me.chkMovieSetLandscapeMSAA.Checked = .MovieSetLandscapeMSAA
            Me.chkMovieSetPosterMSAA.Checked = .MovieSetPosterMSAA
            Me.txtMovieSetPathMSAA.Text = .MovieSetPathMSAA

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
            Me.chkTVEpisodeNFOFrodo.Checked = .TVEpisodeNFOFrodo
            Me.chkTVEpisodePosterFrodo.Checked = .TVEpisodePosterFrodo
            Me.chkTVSeasonBannerFrodo.Checked = .TVSeasonBannerFrodo
            Me.chkTVSeasonFanartFrodo.Checked = .TVSeasonFanartFrodo
            Me.chkTVSeasonPosterFrodo.Checked = .TVSeasonPosterFrodo
            Me.chkTVShowActorThumbsFrodo.Checked = .TVShowActorThumbsFrodo
            Me.chkTVShowBannerFrodo.Checked = .TVShowBannerFrodo
            Me.chkTVShowExtrafanartsFrodo.Checked = .TVShowExtrafanartsFrodo
            Me.chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
            Me.chkTVShowNFOFrodo.Checked = .TVShowNFOFrodo
            Me.chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '******** XBMC ArtworkDownloader settings **********
            Me.chkTVSeasonLandscapeAD.Checked = .TVSeasonLandscapeAD
            Me.chkTVShowCharacterArtAD.Checked = .TVShowCharacterArtAD
            Me.chkTVShowClearArtAD.Checked = .TVShowClearArtAD
            Me.chkTVShowClearLogoAD.Checked = .TVShowClearLogoAD
            Me.chkTVShowLandscapeAD.Checked = .TVShowLandscapeAD

            '********* XBMC Extended Images settings ***********
            Me.chkTVSeasonLandscapeExtended.Checked = .TVSeasonLandscapeExtended
            Me.chkTVShowCharacterArtExtended.Checked = .TVShowCharacterArtExtended
            Me.chkTVShowClearArtExtended.Checked = .TVShowClearArtExtended
            Me.chkTVShowClearLogoExtended.Checked = .TVShowClearLogoExtended
            Me.chkTVShowLandscapeExtended.Checked = .TVShowLandscapeExtended

            '************* XBMC TvTunes settings ***************
            Me.chkTVShowThemeTvTunesEnabled.Checked = .TVShowThemeTvTunesEnable
            Me.chkTVShowThemeTvTunesCustom.Checked = .TVShowThemeTvTunesCustom
            Me.chkTVShowThemeTvTunesShowPath.Checked = .TVShowThemeTvTunesShowPath
            Me.chkTVShowThemeTvTunesSub.Checked = .TVShowThemeTvTunesSub
            Me.txtTVShowThemeTvTunesCustomPath.Text = .TVShowThemeTvTunesCustomPath
            Me.txtTVShowThemeTvTunesSubDir.Text = .TVShowThemeTvTunesSubDir

            '****************** YAMJ settings ******************
            Me.chkTVUseYAMJ.Checked = .TVUseYAMJ
            Me.chkTVEpisodeNFOYAMJ.Checked = .TVEpisodeNFOYAMJ
            Me.chkTVEpisodePosterYAMJ.Checked = .TVEpisodePosterYAMJ
            Me.chkTVSeasonBannerYAMJ.Checked = .TVSeasonBannerYAMJ
            Me.chkTVSeasonFanartYAMJ.Checked = .TVSeasonFanartYAMJ
            Me.chkTVSeasonPosterYAMJ.Checked = .TVSeasonPosterYAMJ
            Me.chkTVShowBannerYAMJ.Checked = .TVShowBannerYAMJ
            Me.chkTVShowFanartYAMJ.Checked = .TVShowFanartYAMJ
            Me.chkTVShowNFOYAMJ.Checked = .TVShowNFOYAMJ
            Me.chkTVShowPosterYAMJ.Checked = .TVShowPosterYAMJ

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            Me.chkTVUseBoxee.Checked = .TVUseBoxee
            Me.chkTVEpisodeNFOBoxee.Checked = .TVEpisodeNFOBoxee
            Me.chkTVEpisodePosterBoxee.Checked = .TVEpisodePosterBoxee
            Me.chkTVSeasonPosterBoxee.Checked = .TVSeasonPosterBoxee
            Me.chkTVShowBannerBoxee.Checked = .TVShowBannerBoxee
            Me.chkTVShowFanartBoxee.Checked = .TVShowFanartBoxee
            Me.chkTVShowNFOBoxee.Checked = .TVShowNFOBoxee
            Me.chkTVShowPosterBoxee.Checked = .TVShowPosterBoxee

            '***************** Expert settings ******************
            Me.chkTVUseExpert.Checked = .TVUseExpert

            '***************** Expert AllSeasons ****************
            Me.txtTVAllSeasonsBannerExpert.Text = .TVAllSeasonsBannerExpert
            Me.txtTVAllSeasonsFanartExpert.Text = .TVAllSeasonsFanartExpert
            Me.txtTVAllSeasonsLandscapeExpert.Text = .TVAllSeasonsLandscapeExpert
            Me.txtTVAllSeasonsPosterExpert.Text = .TVAllSeasonsPosterExpert

            '***************** Expert Episode *******************
            Me.chkTVEpisodeActorThumbsExpert.Checked = .TVEpisodeActorThumbsExpert
            Me.txtTVEpisodeActorThumbsExtExpert.Text = .TVEpisodeActorThumbsExtExpert
            Me.txtTVEpisodeFanartExpert.Text = .TVEpisodeFanartExpert
            Me.txtTVEpisodeNFOExpert.Text = .TVEpisodeNFOExpert
            Me.txtTVEpisodePosterExpert.Text = .TVEpisodePosterExpert

            '***************** Expert Season *******************
            Me.txtTVSeasonBannerExpert.Text = .TVSeasonBannerExpert
            Me.txtTVSeasonFanartExpert.Text = .TVSeasonFanartExpert
            Me.txtTVSeasonLandscapeExpert.Text = .TVSeasonLandscapeExpert
            Me.txtTVSeasonPosterExpert.Text = .TVSeasonPosterExpert

            '***************** Expert Show *********************
            Me.chkTVShowActorThumbsExpert.Checked = .TVShowActorThumbsExpert
            Me.txtTVShowActorThumbsExtExpert.Text = .TVShowActorThumbsExtExpert
            Me.txtTVShowBannerExpert.Text = .TVShowBannerExpert
            Me.txtTVShowCharacterArtExpert.Text = .TVShowCharacterArtExpert
            Me.txtTVShowClearArtExpert.Text = .TVShowClearArtExpert
            Me.txtTVShowClearLogoExpert.Text = .TVShowClearLogoExpert
            Me.chkTVShowExtrafanartsExpert.Checked = .TVShowExtrafanartsExpert
            Me.txtTVShowFanartExpert.Text = .TVShowFanartExpert
            Me.txtTVShowLandscapeExpert.Text = .TVShowLandscapeExpert
            Me.txtTVShowNFOExpert.Text = .TVShowNFOExpert
            Me.txtTVShowPosterExpert.Text = .TVShowPosterExpert

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
        Me.FillSettings()
        Me.lvMovieSources.ListViewItemSorter = New ListViewItemComparer(1)
        Me.lvTVSources.ListViewItemSorter = New ListViewItemComparer(1)
        Me.sResult.NeedsDBClean_Movie = False
        Me.sResult.NeedsDBClean_TV = False
        Me.sResult.NeedsDBUpdate_Movie = False
        Me.sResult.NeedsDBUpdate_TV = False
        Me.sResult.NeedsReload_Movie = False
        Me.sResult.NeedsReload_MovieSet = False
        Me.sResult.NeedsReload_TV = False
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

        If tSetPan IsNot Nothing Then
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
                            If oSetPan IsNot Nothing Then oSetPan.Order = i + (diffOrder * -1)
                        End If
                        If diffOrder > 0 AndAlso Not t(0).NextNode Is Nothing Then
                            oSetPan = SettingsPanels.FirstOrDefault(Function(s) s.Name = t(0).NextNode.Name)
                            If oSetPan IsNot Nothing Then oSetPan.Order = i + (diffOrder * -1)
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
                For Each s As ModulesManager._externalScraperModuleClass_Data_TV In (ModulesManager.Instance.externalScrapersModules_Data_TV.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Image_Movie In (ModulesManager.Instance.externalScrapersModules_Image_Movie.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Image_MovieSet In (ModulesManager.Instance.externalScrapersModules_Image_MovieSet.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Image_TV In (ModulesManager.Instance.externalScrapersModules_Image_TV.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Theme_Movie In (ModulesManager.Instance.externalScrapersModules_Theme_Movie.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In (ModulesManager.Instance.externalScrapersModules_Theme_TV.Where(Function(y) y.AssemblyName <> Name))
                    s.ProcessorModule.ScraperOrderChanged()
                Next
                For Each s As ModulesManager._externalScraperModuleClass_Trailer_Movie In (ModulesManager.Instance.externalScrapersModules_Trailer_Movie.Where(Function(y) y.AssemblyName <> Name))
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

    Private Sub LoadTVShowMatching()
        Dim lvItem As ListViewItem
        lvTVSourcesRegexTVShowMatching.Items.Clear()
        For Each rShow As Settings.regexp In Me.TVShowMatching
            lvItem = New ListViewItem(rShow.ID.ToString)
            lvItem.SubItems.Add(rShow.Regexp)
            lvItem.SubItems.Add(If(Not rShow.defaultSeason.ToString = "-1", rShow.defaultSeason.ToString, String.Empty))
            lvItem.SubItems.Add(If(rShow.byDate, "Yes", "No"))
            Me.lvTVSourcesRegexTVShowMatching.Items.Add(lvItem)
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

    Private Sub LoadMovieBannerSizes()
        Dim items As New Dictionary(Of String, Enums.MovieBannerSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.MovieBannerSize.Any)
        items.Add("1000x185", Enums.MovieBannerSize.HD185)
        Me.cbMovieBannerPrefSize.DataSource = items.ToList
        Me.cbMovieBannerPrefSize.DisplayMember = "Key"
        Me.cbMovieBannerPrefSize.ValueMember = "Value"
        Me.cbMovieSetBannerPrefSize.DataSource = items.ToList
        Me.cbMovieSetBannerPrefSize.DisplayMember = "Key"
        Me.cbMovieSetBannerPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadMovieFanartSizes()
        Dim items As New Dictionary(Of String, Enums.MovieFanartSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.MovieFanartSize.Any)
        items.Add("1920x1080", Enums.MovieFanartSize.HD1080)
        items.Add("1280x720", Enums.MovieFanartSize.HD720)
        items.Add("Thumb", Enums.MovieFanartSize.Thumb)
        Me.cbMovieExtrafanartsPrefSize.DataSource = items.ToList
        Me.cbMovieExtrafanartsPrefSize.DisplayMember = "Key"
        Me.cbMovieExtrafanartsPrefSize.ValueMember = "Value"
        Me.cbMovieExtrathumbsPrefSize.DataSource = items.ToList
        Me.cbMovieExtrathumbsPrefSize.DisplayMember = "Key"
        Me.cbMovieExtrathumbsPrefSize.ValueMember = "Value"
        Me.cbMovieFanartPrefSize.DataSource = items.ToList
        Me.cbMovieFanartPrefSize.DisplayMember = "Key"
        Me.cbMovieFanartPrefSize.ValueMember = "Value"
        Me.cbMovieSetFanartPrefSize.DataSource = items.ToList
        Me.cbMovieSetFanartPrefSize.DisplayMember = "Key"
        Me.cbMovieSetFanartPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadMoviePosterSizes()
        Dim items As New Dictionary(Of String, Enums.MoviePosterSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.MoviePosterSize.Any)
        items.Add("1400x2100", Enums.MoviePosterSize.HD2100)
        items.Add("1000x1500", Enums.MoviePosterSize.HD1500)
        items.Add("1000x1426", Enums.MoviePosterSize.HD1426)
        Me.cbMoviePosterPrefSize.DataSource = items.ToList
        Me.cbMoviePosterPrefSize.DisplayMember = "Key"
        Me.cbMoviePosterPrefSize.ValueMember = "Value"
        Me.cbMovieSetPosterPrefSize.DataSource = items.ToList
        Me.cbMovieSetPosterPrefSize.DisplayMember = "Key"
        Me.cbMovieSetPosterPrefSize.ValueMember = "Value"
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
        Me.cbMovieTrailerMinVideoQual.DataSource = items.ToList
        Me.cbMovieTrailerMinVideoQual.DisplayMember = "Key"
        Me.cbMovieTrailerMinVideoQual.ValueMember = "Value"
        Me.cbMovieTrailerPrefVideoQual.DataSource = items.ToList
        Me.cbMovieTrailerPrefVideoQual.DisplayMember = "Key"
        Me.cbMovieTrailerPrefVideoQual.ValueMember = "Value"
    End Sub

    Private Sub LoadTVFanartSizes()
        Dim items As New Dictionary(Of String, Enums.TVFanartSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.TVFanartSize.Any)
        items.Add("1920x1080", Enums.TVFanartSize.HD1080)
        items.Add("1280x720", Enums.TVFanartSize.HD720)
        Me.cbTVAllSeasonsFanartPrefSize.DataSource = items.ToList
        Me.cbTVAllSeasonsFanartPrefSize.DisplayMember = "Key"
        Me.cbTVAllSeasonsFanartPrefSize.ValueMember = "Value"
        Me.cbTVEpisodeFanartPrefSize.DataSource = items.ToList
        Me.cbTVEpisodeFanartPrefSize.DisplayMember = "Key"
        Me.cbTVEpisodeFanartPrefSize.ValueMember = "Value"
        Me.cbTVSeasonFanartPrefSize.DataSource = items.ToList
        Me.cbTVSeasonFanartPrefSize.DisplayMember = "Key"
        Me.cbTVSeasonFanartPrefSize.ValueMember = "Value"
        Me.cbTVShowExtrafanartsPrefSize.DataSource = items.ToList
        Me.cbTVShowExtrafanartsPrefSize.DisplayMember = "Key"
        Me.cbTVShowExtrafanartsPrefSize.ValueMember = "Value"
        Me.cbTVShowFanartPrefSize.DataSource = items.ToList
        Me.cbTVShowFanartPrefSize.DisplayMember = "Key"
        Me.cbTVShowFanartPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVPosterSizes()
        Dim items As New Dictionary(Of String, Enums.TVPosterSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.TVPosterSize.Any)
        items.Add("1000x1500", Enums.TVPosterSize.HD1500)
        items.Add("1000x1426", Enums.TVPosterSize.HD1426)
        items.Add("680x1000", Enums.TVPosterSize.HD1000)
        Me.cbTVAllSeasonsPosterPrefSize.DataSource = items.ToList
        Me.cbTVAllSeasonsPosterPrefSize.DisplayMember = "Key"
        Me.cbTVAllSeasonsPosterPrefSize.ValueMember = "Value"
        Me.cbTVShowPosterPrefSize.DataSource = items.ToList
        Me.cbTVShowPosterPrefSize.DisplayMember = "Key"
        Me.cbTVShowPosterPrefSize.ValueMember = "Value"

        Dim items2 As New Dictionary(Of String, Enums.TVSeasonPosterSize)
        items2.Add(Master.eLang.GetString(745, "Any"), Enums.TVSeasonPosterSize.Any)
        items2.Add("1000x1500", Enums.TVSeasonPosterSize.HD1500)
        items2.Add("1000x1426", Enums.TVSeasonPosterSize.HD1426)
        items2.Add("400x578", Enums.TVSeasonPosterSize.HD578)
        Me.cbTVSeasonPosterPrefSize.DataSource = items2.ToList
        Me.cbTVSeasonPosterPrefSize.DisplayMember = "Key"
        Me.cbTVSeasonPosterPrefSize.ValueMember = "Value"

        Dim items3 As New Dictionary(Of String, Enums.TVEpisodePosterSize)
        items3.Add(Master.eLang.GetString(745, "Any"), Enums.TVEpisodePosterSize.Any)
        items3.Add("1920x1080", Enums.TVEpisodePosterSize.HD1080)
        items3.Add("1280x720", Enums.TVEpisodePosterSize.HD720)
        items3.Add("400x225", Enums.TVEpisodePosterSize.SD225)
        Me.cbTVEpisodePosterPrefSize.DataSource = items3.ToList
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

    Private Sub LoadTVBannerSizes()
        Dim items As New Dictionary(Of String, Enums.TVBannerSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.TVBannerSize.Any)
        items.Add("1000x185", Enums.TVBannerSize.HD185)
        items.Add("758x140", Enums.TVBannerSize.HD140)
        Me.cbTVAllSeasonsBannerPrefSize.DataSource = items.ToList
        Me.cbTVAllSeasonsBannerPrefSize.DisplayMember = "Key"
        Me.cbTVAllSeasonsBannerPrefSize.ValueMember = "Value"
        Me.cbTVSeasonBannerPrefSize.DataSource = items.ToList
        Me.cbTVSeasonBannerPrefSize.DisplayMember = "Key"
        Me.cbTVSeasonBannerPrefSize.ValueMember = "Value"
        Me.cbTVShowBannerPrefSize.DataSource = items.ToList
        Me.cbTVShowBannerPrefSize.DisplayMember = "Key"
        Me.cbTVShowBannerPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVBannerTypes()
        Dim items As New Dictionary(Of String, Enums.TVBannerType)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.TVBannerType.Any)
        items.Add(Master.eLang.GetString(746, "Blank"), Enums.TVBannerType.Blank)
        items.Add(Master.eLang.GetString(747, "Graphical"), Enums.TVBannerType.Graphical)
        items.Add(Master.eLang.GetString(748, "Text"), Enums.TVBannerType.Text)
        Me.cbTVAllSeasonsBannerPrefType.DataSource = items.ToList
        Me.cbTVAllSeasonsBannerPrefType.DisplayMember = "Key"
        Me.cbTVAllSeasonsBannerPrefType.ValueMember = "Value"
        Me.cbTVSeasonBannerPrefType.DataSource = items.ToList
        Me.cbTVSeasonBannerPrefType.DisplayMember = "Key"
        Me.cbTVSeasonBannerPrefType.ValueMember = "Value"
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
            Using dEditMeta As New dlgFileInfo(New Database.DBElement, False)
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
            Using dEditMeta As New dlgFileInfo(New Database.DBElement, True)
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
            Using dMovieSource As New dlgSourceMovie
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshMovieSources()
                    Me.sResult.NeedsReload_Movie = True 'TODO: Check if we have to use Reload or DBUpdate
                    Me.SetApplyButton(True)
                End If
            End Using
        End If
    End Sub

    Private Sub lvMovieSources_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvMovieSources.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveMovieSource()
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVSourcesRegexTVShowMatching.DoubleClick
        If Me.lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then Me.EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvTVSourcesRegexTVShowMatching.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVShowMatching()
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVSourcesRegexTVShowMatching.SelectedIndexChanged
        If Not String.IsNullOrEmpty(Me.btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then Me.ClearTVShowMatching()
    End Sub

    Private Sub lvTVSources_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvTVSources.ColumnClick
        Me.lvTVSources.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvTVSources_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVSources.DoubleClick
        If lvTVSources.SelectedItems.Count > 0 Then
            Using dTVSource As New dlgSourceTVShow
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshTVSources()
                    Me.sResult.NeedsReload_TV = True 'TODO: Check if we have to use Reload or DBUpdate
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
        For Each s As Database.DBSource In Master.MovieSources
            lvItem = New ListViewItem(CStr(s.ID))
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(s.Language)
            lvItem.SubItems.Add(If(s.Recursive, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.UseFolderName, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.IsSingle, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.Exclude, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.GetYear, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieSources.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        lvTVSources.Items.Clear()
        Master.DB.LoadTVShowSourcesFromDB()
        For Each s As Database.DBSource In Master.TVShowSources
            lvItem = New ListViewItem(CStr(s.ID))
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(s.Language)
            lvItem.SubItems.Add(s.Ordering.ToString)
            lvItem.SubItems.Add(If(s.Exclude, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(s.EpisodeSorting.ToString)
            lvTVSources.Items.Add(lvItem)
        Next
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
            Me.sResult.NeedsReload_TV = True
        End If
    End Sub

    Private Sub RemoveMovieFilter()
        If Me.lstMovieFilters.Items.Count > 0 AndAlso Me.lstMovieFilters.SelectedItems.Count > 0 Then
            While Me.lstMovieFilters.SelectedItems.Count > 0
                Me.lstMovieFilters.Items.Remove(Me.lstMovieFilters.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsReload_Movie = True
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
            Me.sResult.NeedsDBClean_Movie = True
            Me.sResult.NeedsDBClean_TV = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidSubtitlesExts()
        If lstFileSystemValidSubtitlesExts.Items.Count > 0 AndAlso lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0
                Me.lstFileSystemValidSubtitlesExts.Items.Remove(Me.lstFileSystemValidSubtitlesExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsReload_Movie = True
            Me.sResult.NeedsReload_TV = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidThemeExts()
        If lstFileSystemValidThemeExts.Items.Count > 0 AndAlso lstFileSystemValidThemeExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemValidThemeExts.SelectedItems.Count > 0
                Me.lstFileSystemValidThemeExts.Items.Remove(Me.lstFileSystemValidThemeExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsReload_Movie = True
            Me.sResult.NeedsReload_TV = True
        End If
    End Sub

    Private Sub RemoveMovieSource()
        If Me.lvMovieSources.SelectedItems.Count > 0 Then
            If MessageBox.Show(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Me.lvMovieSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "idSource")
                        While Me.lvMovieSources.SelectedItems.Count > 0
                            parSource.Value = lvMovieSources.SelectedItems(0).SubItems(0).Text
                            SQLcommand.CommandText = String.Concat("DELETE FROM moviesource WHERE idSource = (?);")
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
                Me.sResult.NeedsDBClean_Movie = True
            End If
        End If
    End Sub

    Private Sub RemoveFileSystemNoStackExts()
        If lstFileSystemNoStackExts.Items.Count > 0 AndAlso lstFileSystemNoStackExts.SelectedItems.Count > 0 Then
            While Me.lstFileSystemNoStackExts.SelectedItems.Count > 0
                Me.lstFileSystemNoStackExts.Items.Remove(Me.lstFileSystemNoStackExts.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsDBUpdate_Movie = True
            Me.sResult.NeedsDBUpdate_TV = True
        End If
    End Sub

    Private Sub RemoveTVShowMatching()
        Dim ID As Integer
        For Each lItem As ListViewItem In lvTVSourcesRegexTVShowMatching.SelectedItems
            ID = Convert.ToInt32(lItem.Text)
            Dim selRex = From lRegex As Settings.regexp In Me.TVShowMatching Where lRegex.ID = ID
            If selRex.Count > 0 Then
                Me.TVShowMatching.Remove(selRex(0))
                Me.SetApplyButton(True)
            End If
        Next
        Me.LoadTVShowMatching()
    End Sub

    Private Sub RemoveTVShowFilter()
        If Me.lstTVShowFilter.Items.Count > 0 AndAlso Me.lstTVShowFilter.SelectedItems.Count > 0 Then
            While Me.lstTVShowFilter.SelectedItems.Count > 0
                Me.lstTVShowFilter.Items.Remove(Me.lstTVShowFilter.SelectedItems(0))
            End While
            Me.SetApplyButton(True)
            Me.sResult.NeedsReload_TV = True
        End If
    End Sub

    Private Sub RemoveMovieSortToken()
        If Me.lstMovieSortTokens.Items.Count > 0 AndAlso Me.lstMovieSortTokens.SelectedItems.Count > 0 Then
            While Me.lstMovieSortTokens.SelectedItems.Count > 0
                Me.lstMovieSortTokens.Items.Remove(Me.lstMovieSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsReload_Movie = True
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveMovieSetSortToken()
        If Me.lstMovieSetSortTokens.Items.Count > 0 AndAlso Me.lstMovieSetSortTokens.SelectedItems.Count > 0 Then
            While Me.lstMovieSetSortTokens.SelectedItems.Count > 0
                Me.lstMovieSetSortTokens.Items.Remove(Me.lstMovieSetSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsReload_MovieSet = True
            Me.SetApplyButton(True)
        End If
    End Sub

    Private Sub RemoveTVSortToken()
        If Me.lstTVSortTokens.Items.Count > 0 AndAlso Me.lstTVSortTokens.SelectedItems.Count > 0 Then
            While Me.lstTVSortTokens.SelectedItems.Count > 0
                Me.lstTVSortTokens.Items.Remove(Me.lstTVSortTokens.SelectedItems(0))
            End While
            Me.sResult.NeedsReload_TV = True
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
            If MessageBox.Show(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the TV Shows from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Me.lvTVSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.UInt64, 0, "idSource")
                        While Me.lvTVSources.SelectedItems.Count > 0
                            parSource.Value = lvTVSources.SelectedItems(0).SubItems(0).Text
                            SQLcommand.CommandText = String.Concat("DELETE FROM tvshowsource WHERE idSource = (?);")
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
                Me.sResult.NeedsDBClean_TV = True
            End If
        End If
    End Sub

    Private Sub RenumberTVShowMatching()
        For i As Integer = 0 To Me.TVShowMatching.Count - 1
            Me.TVShowMatching(i).ID = i
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
            .GeneralCheckUpdates = chkGeneralCheckUpdates.Checked
            .GeneralDateAddedIgnoreNFO = Me.chkGeneralDateAddedIgnoreNFO.Checked
            .GeneralDigitGrpSymbolVotes = Me.chkGeneralDigitGrpSymbolVotes.Checked
            .GeneralDateTime = CType(Me.cbGeneralDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTime)).Value
            .GeneralDoubleClickScrape = Me.chkGeneralDoubleClickScrape.Checked
            .GeneralDaemonDrive = Me.cbGeneralDaemonDrive.Text
            .GeneralDaemonPath = Me.txtGeneralDaemonPath.Text
            .GeneralDisplayBanner = Me.chkGeneralDisplayBanner.Checked
            .GeneralDisplayCharacterArt = Me.chkGeneralDisplayCharacterArt.Checked
            .GeneralDisplayClearArt = Me.chkGeneralDisplayClearArt.Checked
            .GeneralDisplayClearLogo = Me.chkGeneralDisplayClearLogo.Checked
            .GeneralDisplayDiscArt = Me.chkGeneralDisplayDiscArt.Checked
            .GeneralDisplayFanart = Me.chkGeneralDisplayFanart.Checked
            .GeneralDisplayFanartSmall = Me.chkGeneralDisplayFanartSmall.Checked
            .GeneralDisplayLandscape = Me.chkGeneralDisplayLandscape.Checked
            .GeneralDisplayPoster = Me.chkGeneralDisplayPoster.Checked
            .GeneralImagesGlassOverlay = Me.chkGeneralImagesGlassOverlay.Checked
            .GeneralLanguage = Me.cbGeneralLanguage.Text
            .GeneralMovieTheme = Me.cbGeneralMovieTheme.Text
            .GeneralMovieSetTheme = Me.cbGeneralMovieSetTheme.Text
            .GeneralOverwriteNfo = Me.chkGeneralOverwriteNfo.Checked
            .GeneralShowGenresText = Me.chkGeneralDisplayGenresText.Checked
            .GeneralShowLangFlags = Me.chkGeneralDisplayLangFlags.Checked
            .GeneralShowImgDims = Me.chkGeneralDisplayImgDims.Checked
            .GeneralShowImgNames = Me.chkGeneralDisplayImgNames.Checked
            .GeneralSourceFromFolder = Me.chkGeneralSourceFromFolder.Checked
            .GeneralTVEpisodeTheme = Me.cbGeneralTVEpisodeTheme.Text
            .GeneralTVShowTheme = Me.cbGeneralTVShowTheme.Text
            .MovieActorThumbsKeepExisting = Me.chkMovieActorThumbsKeepExisting.Checked
            '.MovieActorThumbsQual = Me.tbMovieActorThumbsQual.value
            .MovieBackdropsPath = Me.txtMovieSourcesBackdropsFolderPath.Text
            If Not String.IsNullOrEmpty(Me.txtMovieSourcesBackdropsFolderPath.Text) Then
                .MovieBackdropsAuto = Me.chkMovieSourcesBackdropsAuto.Checked
            Else
                .MovieBackdropsAuto = False
            End If
            .MovieBannerHeight = If(Not String.IsNullOrEmpty(Me.txtMovieBannerHeight.Text), Convert.ToInt32(Me.txtMovieBannerHeight.Text), 0)
            .MovieBannerKeepExisting = Me.chkMovieBannerKeepExisting.Checked
            .MovieBannerPrefSizeOnly = Me.chkMovieBannerPrefOnly.Checked
            .MovieBannerPrefSize = CType(Me.cbMovieBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MovieBannerSize)).Value
            .MovieBannerResize = Me.chkMovieBannerResize.Checked
            .MovieBannerWidth = If(Not String.IsNullOrEmpty(Me.txtMovieBannerWidth.Text), Convert.ToInt32(Me.txtMovieBannerWidth.Text), 0)
            .MovieCleanDB = Me.chkMovieCleanDB.Checked
            .MovieClearArtKeepExisting = Me.chkMovieClearArtKeepExisting.Checked
            .MovieClearLogoKeepExisting = Me.chkMovieClearLogoKeepExisting.Checked
            .MovieClickScrape = Me.chkMovieClickScrape.Checked
            .MovieClickScrapeAsk = Me.chkMovieClickScrapeAsk.Checked
            .MovieDiscArtKeepExisting = Me.chkMovieDiscArtKeepExisting.Checked
            .MovieDisplayYear = Me.chkMovieDisplayYear.Checked
            .MovieExtrafanartsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieExtrafanartsHeight.Text), Convert.ToInt32(Me.txtMovieExtrafanartsHeight.Text), 0)
            .MovieExtrafanartsLimit = If(Not String.IsNullOrEmpty(Me.txtMovieExtrafanartsLimit.Text), Convert.ToInt32(Me.txtMovieExtrafanartsLimit.Text), 0)
            .MovieExtrafanartsKeepExisting = Me.chkMovieExtrafanartsKeepExisting.Checked
            .MovieExtrafanartsPrefSizeOnly = Me.chkMovieExtrafanartsPrefOnly.Checked
            .MovieExtrafanartsPrefSize = CType(Me.cbMovieExtrafanartsPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MovieFanartSize)).Value
            .MovieExtrafanartsResize = Me.chkMovieExtrafanartsResize.Checked
            .MovieExtrafanartsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieExtrafanartsWidth.Text), Convert.ToInt32(Me.txtMovieExtrafanartsWidth.Text), 0)
            .MovieExtrathumbsHeight = If(Not String.IsNullOrEmpty(Me.txtMovieExtrathumbsHeight.Text), Convert.ToInt32(Me.txtMovieExtrathumbsHeight.Text), 0)
            .MovieExtrathumbsLimit = If(Not String.IsNullOrEmpty(Me.txtMovieExtrathumbsLimit.Text), Convert.ToInt32(Me.txtMovieExtrathumbsLimit.Text), 0)
            .MovieExtrathumbsKeepExisting = Me.chkMovieExtrathumbsKeepExisting.Checked
            .MovieExtrathumbsPrefSizeOnly = Me.chkMovieExtrathumbsPrefOnly.Checked
            .MovieExtrathumbsPrefSize = CType(Me.cbMovieExtrathumbsPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MovieFanartSize)).Value
            .MovieExtrathumbsResize = Me.chkMovieExtrathumbsResize.Checked
            .MovieExtrathumbsWidth = If(Not String.IsNullOrEmpty(Me.txtMovieExtrathumbsWidth.Text), Convert.ToInt32(Me.txtMovieExtrathumbsWidth.Text), 0)
            .MovieFanartHeight = If(Not String.IsNullOrEmpty(Me.txtMovieFanartHeight.Text), Convert.ToInt32(Me.txtMovieFanartHeight.Text), 0)
            .MovieFanartKeepExisting = Me.chkMovieFanartKeepExisting.Checked
            .MovieFanartPrefSizeOnly = Me.chkMovieFanartPrefOnly.Checked
            .MovieFanartPrefSize = CType(Me.cbMovieFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MovieFanartSize)).Value
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
            If Not String.IsNullOrEmpty(cbMovieGeneralLang.Text) Then
                .MovieGeneralLanguage = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbMovieGeneralLang.Text).abbreviation
            End If
            .MovieGeneralMarkNew = Me.chkMovieGeneralMarkNew.Checked
            .MovieGeneralMediaListSorting.Clear()
            .MovieGeneralMediaListSorting.AddRange(Me.MovieGeneralMediaListSorting)
            .MovieImagesCacheEnabled = Me.chkMovieImagesCacheEnabled.Checked
            .MovieImagesDisplayImageSelect = Me.chkMovieImagesDisplayImageSelect.Checked
            .MovieImagesGetBlankImages = Me.chkMovieImagesGetBlankImages.Checked
            .MovieImagesGetEnglishImages = Me.chkMovieImagesGetEnglishImages.Checked
            .MovieImagesMediaLanguageOnly = Me.chkMovieImagesMediaLanguageOnly.Checked
            .MovieImagesNotSaveURLToNfo = Me.chkMovieImagesNotSaveURLToNfo.Checked
            If Not String.IsNullOrEmpty(Me.txtMovieIMDBURL.Text) Then
                .MovieIMDBURL = Me.txtMovieIMDBURL.Text.Replace("http://", String.Empty).Trim
            Else
                .MovieIMDBURL = "akas.imdb.com"
            End If
            .MovieLandscapeKeepExisting = Me.chkMovieLandscapeKeepExisting.Checked
            .MovieLevTolerance = If(Not String.IsNullOrEmpty(Me.txtMovieLevTolerance.Text), Convert.ToInt32(Me.txtMovieLevTolerance.Text), 0)
            .MovieLockActors = Me.chkMovieLockActors.Checked
            .MovieLockCert = Me.chkMovieLockCert.Checked
            .MovieLockCollectionID = Me.chkMovieLockCollectionID.Checked
            .MovieLockCollections = Me.chkMovieLockCollections.Checked
            .MovieLockCountry = Me.chkMovieLockCountry.Checked
            .MovieLockDirector = Me.chkMovieLockDirector.Checked
            .MovieLockGenre = Me.chkMovieLockGenre.Checked
            .MovieLockLanguageA = Me.chkMovieLockLanguageA.Checked
            .MovieLockLanguageV = Me.chkMovieLockLanguageV.Checked
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
            .MovieLockCredits = Me.chkMovieLockCredits.Checked
            .MovieLockYear = Me.chkMovieLockYear.Checked
            .MovieMetadataPerFileType.Clear()
            .MovieMetadataPerFileType.AddRange(Me.MovieMeta)
            .MoviePosterHeight = If(Not String.IsNullOrEmpty(Me.txtMoviePosterHeight.Text), Convert.ToInt32(Me.txtMoviePosterHeight.Text), 0)
            .MoviePosterKeepExisting = Me.chkMoviePosterKeepExisting.Checked
            .MoviePosterPrefSizeOnly = Me.chkMoviePosterPrefOnly.Checked
            .MoviePosterPrefSize = CType(Me.cbMoviePosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MoviePosterSize)).Value
            .MoviePosterResize = Me.chkMoviePosterResize.Checked
            .MoviePosterWidth = If(Not String.IsNullOrEmpty(Me.txtMoviePosterWidth.Text), Convert.ToInt32(Me.txtMoviePosterWidth.Text), 0)
            .MovieProperCase = Me.chkMovieProperCase.Checked
            .MovieSetBannerHeight = If(Not String.IsNullOrEmpty(Me.txtMovieSetBannerHeight.Text), Convert.ToInt32(Me.txtMovieSetBannerHeight.Text), 0)
            .MovieSetBannerKeepExisting = Me.chkMovieSetBannerKeepExisting.Checked
            .MovieSetBannerPrefSizeOnly = Me.chkMovieSetBannerPrefOnly.Checked
            .MovieSetBannerPrefSize = CType(Me.cbMovieSetBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MovieBannerSize)).Value
            .MovieSetBannerResize = Me.chkMovieSetBannerResize.Checked
            .MovieSetBannerWidth = If(Not String.IsNullOrEmpty(Me.txtMovieSetBannerWidth.Text), Convert.ToInt32(Me.txtMovieSetBannerWidth.Text), 0)
            .MovieSetCleanDB = Me.chkMovieSetCleanDB.Checked
            .MovieSetCleanFiles = Me.chkMovieSetCleanFiles.Checked
            .MovieSetClearArtKeepExisting = Me.chkMovieSetClearArtKeepExisting.Checked
            .MovieSetClearLogoKeepExisting = Me.chkMovieSetClearLogoKeepExisting.Checked
            .MovieSetClickScrape = Me.chkMovieSetClickScrape.Checked
            .MovieSetClickScrapeAsk = Me.chkMovieSetClickScrapeAsk.Checked
            .MovieSetDiscArtKeepExisting = Me.chkMovieSetDiscArtKeepExisting.Checked
            .MovieSetFanartHeight = If(Not String.IsNullOrEmpty(Me.txtMovieSetFanartHeight.Text), Convert.ToInt32(Me.txtMovieSetFanartHeight.Text), 0)
            .MovieSetFanartKeepExisting = Me.chkMovieSetFanartKeepExisting.Checked
            .MovieSetFanartPrefSizeOnly = Me.chkMovieSetFanartPrefOnly.Checked
            .MovieSetFanartPrefSize = CType(Me.cbMovieSetFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MovieFanartSize)).Value
            .MovieSetFanartResize = Me.chkMovieSetFanartResize.Checked
            .MovieSetFanartWidth = If(Not String.IsNullOrEmpty(Me.txtMovieSetFanartWidth.Text), Convert.ToInt32(Me.txtMovieSetFanartWidth.Text), 0)
            .MovieSetGeneralMarkNew = Me.chkMovieSetGeneralMarkNew.Checked
            .MovieSetGeneralMediaListSorting.Clear()
            .MovieSetGeneralMediaListSorting.AddRange(Me.MovieSetGeneralMediaListSorting)
            .MovieSetImagesCacheEnabled = Me.chkMovieSetImagesCacheEnabled.Checked
            .MovieSetImagesDisplayImageSelect = Me.chkMovieSetImagesDisplayImageSelect.Checked
            .MovieSetImagesGetBlankImages = Me.chkMovieSetImagesGetBlankImages.Checked
            .MovieSetImagesGetEnglishImages = Me.chkMovieSetImagesGetEnglishImages.Checked
            .MovieSetImagesMediaLanguageOnly = Me.chkMovieSetImagesMediaLanguageOnly.Checked
            .MovieSetLandscapeKeepExisting = Me.chkMovieSetLandscapeKeepExisting.Checked
            .MovieSetLockPlot = Me.chkMovieSetLockPlot.Checked
            .MovieSetLockTitle = Me.chkMovieSetLockTitle.Checked
            .MovieSetPosterHeight = If(Not String.IsNullOrEmpty(Me.txtMovieSetPosterHeight.Text), Convert.ToInt32(Me.txtMovieSetPosterHeight.Text), 0)
            .MovieSetPosterKeepExisting = Me.chkMovieSetPosterKeepExisting.Checked
            .MovieSetPosterPrefSizeOnly = Me.chkMovieSetPosterPrefOnly.Checked
            .MovieSetPosterPrefSize = CType(Me.cbMovieSetPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.MoviePosterSize)).Value
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
                    .MovieScraperCertLang = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.name = cbMovieScraperCertLang.Text).abbreviation
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
            .MovieScraperPlotForOutlineIfEmpty = Me.chkMovieScraperPlotForOutlineIfEmpty.Checked
            .MovieScraperRating = Me.chkMovieScraperRating.Checked
            .MovieScraperRelease = Me.chkMovieScraperRelease.Checked
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
            .MovieScraperCredits = Me.chkMovieScraperCredits.Checked
            .MovieScraperXBMCTrailerFormat = Me.chkMovieScraperXBMCTrailerFormat.Checked
            .MovieScraperYear = Me.chkMovieScraperYear.Checked
            If Not String.IsNullOrEmpty(Me.txtMovieSkipLessThan.Text) AndAlso Integer.TryParse(Me.txtMovieSkipLessThan.Text, 0) Then
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
            .MovieThemeKeepExisting = Me.chkMovieThemeKeepExisting.Checked
            .MovieTrailerDefaultSearch = Me.txtMovieTrailerDefaultSearch.Text
            .MovieTrailerKeepExisting = Me.chkMovieTrailerKeepExisting.Checked
            .MovieTrailerMinVideoQual = CType(Me.cbMovieTrailerMinVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
            .MovieTrailerPrefVideoQual = CType(Me.cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
            .TVAllSeasonsBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVAllSeasonsBannerHeight.Text), Convert.ToInt32(Me.txtTVAllSeasonsBannerHeight.Text), 0)
            .TVAllSeasonsBannerKeepExisting = Me.chkTVAllSeasonsBannerKeepExisting.Checked
            .TVAllSeasonsBannerPrefSize = CType(Me.cbTVAllSeasonsBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVBannerSize)).Value
            .TVAllSeasonsBannerPrefSizeOnly = Me.chkTVAllSeasonsBannerPrefSizeOnly.Checked
            .TVAllSeasonsBannerPrefType = CType(Me.cbTVAllSeasonsBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVAllSeasonsBannerResize = Me.chkTVAllSeasonsBannerResize.Checked
            .TVAllSeasonsBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVAllSeasonsBannerWidth.Text), Convert.ToInt32(Me.txtTVAllSeasonsBannerWidth.Text), 0)
            .TVAllSeasonsFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVAllSeasonsFanartHeight.Text), Convert.ToInt32(Me.txtTVAllSeasonsFanartHeight.Text), 0)
            .TVAllSeasonsFanartKeepExisting = Me.chkTVAllSeasonsFanartKeepExisting.Checked
            .TVAllSeasonsFanartPrefSize = CType(Me.cbTVAllSeasonsFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVFanartSize)).Value
            .TVAllSeasonsFanartPrefSizeOnly = Me.chkTVAllSeasonsFanartPrefSizeOnly.Checked
            .TVAllSeasonsFanartResize = Me.chkTVAllSeasonsFanartResize.Checked
            .TVAllSeasonsFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVAllSeasonsFanartWidth.Text), Convert.ToInt32(Me.txtTVAllSeasonsFanartWidth.Text), 0)
            .TVAllSeasonsLandscapeKeepExisting = Me.chkTVAllSeasonsLandscapeKeepExisting.Checked
            .TVAllSeasonsPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVAllSeasonsPosterHeight.Text), Convert.ToInt32(Me.txtTVAllSeasonsPosterHeight.Text), 0)
            .TVAllSeasonsPosterKeepExisting = Me.chkTVAllSeasonsPosterKeepExisting.Checked
            .TVAllSeasonsPosterPrefSize = CType(Me.cbTVAllSeasonsPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVPosterSize)).Value
            .TVAllSeasonsPosterPrefSizeOnly = Me.chkTVAllSeasonsPosterPrefSizeOnly.Checked
            .TVAllSeasonsPosterResize = Me.chkTVAllSeasonsPosterResize.Checked
            .TVAllSeasonsPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVAllSeasonsPosterWidth.Text), Convert.ToInt32(Me.txtTVAllSeasonsPosterWidth.Text), 0)
            .TVCleanDB = Me.chkTVCleanDB.Checked
            .TVDisplayMissingEpisodes = Me.chkTVDisplayMissingEpisodes.Checked
            .TVDisplayStatus = Me.chkTVDisplayStatus.Checked
            .TVEpisodeFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVEpisodeFanartHeight.Text), Convert.ToInt32(Me.txtTVEpisodeFanartHeight.Text), 0)
            .TVEpisodeFanartKeepExisting = Me.chkTVEpisodeFanartKeepExisting.Checked
            .TVEpisodeFanartPrefSize = CType(Me.cbTVEpisodeFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVFanartSize)).Value
            .TVEpisodeFanartPrefSizeOnly = Me.chkTVEpisodeFanartPrefSizeOnly.Checked
            .TVEpisodeFanartResize = Me.chkTVEpisodeFanartResize.Checked
            .TVEpisodeFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVEpisodeFanartWidth.Text), Convert.ToInt32(Me.txtTVEpisodeFanartWidth.Text), 0)
            .TVEpisodeFilterCustom.Clear()
            .TVEpisodeFilterCustom.AddRange(Me.lstTVEpisodeFilter.Items.OfType(Of String).ToList)
            If .TVEpisodeFilterCustom.Count <= 0 Then .TVEpisodeFilterCustomIsEmpty = True
            .TVEpisodeNoFilter = Me.chkTVEpisodeNoFilter.Checked
            .TVEpisodePosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVEpisodePosterHeight.Text), Convert.ToInt32(Me.txtTVEpisodePosterHeight.Text), 0)
            .TVEpisodePosterKeepExisting = Me.chkTVEpisodePosterKeepExisting.Checked
            .TVEpisodePosterPrefSize = CType(Me.cbTVEpisodePosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVEpisodePosterSize)).Value
            .TVEpisodePosterPrefSizeOnly = Me.chkTVEpisodePosterPrefSizeOnly.Checked
            .TVEpisodePosterResize = Me.chkTVEpisodePosterResize.Checked
            .TVEpisodePosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVEpisodePosterWidth.Text), Convert.ToInt32(Me.txtTVEpisodePosterWidth.Text), 0)
            .TVEpisodeProperCase = Me.chkTVEpisodeProperCase.Checked
            .TVGeneralEpisodeListSorting.Clear()
            .TVGeneralEpisodeListSorting.AddRange(Me.TVGeneralEpisodeListSorting)
            .TVGeneralFlagLang = If(Me.cbTVLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, Me.cbTVLanguageOverlay.Text)
            .TVGeneralIgnoreLastScan = Me.chkTVGeneralIgnoreLastScan.Checked
            'cocotus, 2014/05/21 Fixed: If cbTVGeneralLang.Text is empty it will crash here -> no AdvancedSettings.xml will be built/saved!!(happens when user has not yet set TVLanguage via Fetch language button!)
            'old:    .TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.LongLang = cbTVGeneralLang.Text).ShortLang
            If Not String.IsNullOrEmpty(cbTVGeneralLang.Text) Then
                .TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbTVGeneralLang.Text).abbreviation
            End If
            .TVGeneralClickScrape = Me.chkTVGeneralClickScrape.Checked
            .TVGeneralClickScrapeAsk = Me.chkTVGeneralClickScrapeAsk.Checked
            .TVGeneralMarkNewEpisodes = Me.chkTVGeneralMarkNewEpisodes.Checked
            .TVGeneralMarkNewShows = Me.chkTVGeneralMarkNewShows.Checked
            .TVGeneralSeasonListSorting.Clear()
            .TVGeneralSeasonListSorting.AddRange(Me.TVGeneralSeasonListSorting)
            .TVGeneralShowListSorting.Clear()
            .TVGeneralShowListSorting.AddRange(Me.TVGeneralShowListSorting)
            .TVImagesCacheEnabled = Me.chkTVImagesCacheEnabled.Checked
            .TVImagesDisplayImageSelect = Me.chkTVImagesDisplayImageSelect.Checked
            .TVImagesGetBlankImages = Me.chkTVImagesGetBlankImages.Checked
            .TVImagesGetEnglishImages = Me.chkTVImagesGetEnglishImages.Checked
            .TVImagesMediaLanguageOnly = Me.chkTVImagesMediaLanguageOnly.Checked
            .TVLockEpisodeLanguageA = Me.chkTVLockEpisodeLanguageA.Checked
            .TVLockEpisodeLanguageV = Me.chkTVLockEpisodeLanguageV.Checked
            .TVLockEpisodePlot = Me.chkTVLockEpisodePlot.Checked
            .TVLockEpisodeRating = Me.chkTVLockEpisodeRating.Checked
            .TVLockEpisodeRuntime = Me.chkTVLockEpisodeRuntime.Checked
            .TVLockEpisodeTitle = Me.chkTVLockEpisodeTitle.Checked
            .TVLockSeasonPlot = Me.chkTVLockSeasonPlot.Checked
            .TVLockSeasonTitle = Me.chkTVLockSeasonTitle.Checked
            .TVLockShowCert = Me.chkTVLockShowCert.Checked
            .TVLockShowCreators = Me.chkTVLockShowCreators.Checked
            .TVLockShowGenre = Me.chkTVLockShowGenre.Checked
            .TVLockShowMPAA = Me.chkTVLockShowMPAA.Checked
            .TVLockShowOriginalTitle = Me.chkTVLockShowOriginalTitle.Checked
            .TVLockShowPlot = Me.chkTVLockShowPlot.Checked
            .TVLockShowRating = Me.chkTVLockShowRating.Checked
            .TVLockShowRuntime = Me.chkTVLockShowRuntime.Checked
            .TVLockShowStatus = Me.chkTVLockShowStatus.Checked
            .TVLockShowStudio = Me.chkTVLockShowStudio.Checked
            .TVLockShowTitle = Me.chkTVLockShowTitle.Checked
            .TVMetadataPerFileType.Clear()
            .TVMetadataPerFileType.AddRange(Me.TVMeta)
            .TVMultiPartMatching = Me.txtTVSourcesRegexMultiPartMatching.Text
            .TVScanOrderModify = Me.chkTVScanOrderModify.Checked
            .TVScraperCleanFields = Me.chkTVScraperCleanFields.Checked
            .TVScraperDurationRuntimeFormat = Me.txtTVScraperDurationRuntimeFormat.Text
            .TVScraperEpisodeActors = Me.chkTVScraperEpisodeActors.Checked
            .TVScraperEpisodeAired = Me.chkTVScraperEpisodeAired.Checked
            .TVScraperEpisodeCredits = Me.chkTVScraperEpisodeCredits.Checked
            .TVScraperEpisodeDirector = Me.chkTVScraperEpisodeDirector.Checked
            .TVScraperEpisodeGuestStars = Me.chkTVScraperEpisodeGuestStars.Checked
            .TVScraperEpisodeGuestStarsToActors = Me.chkTVScraperEpisodeGuestStarsToActors.Checked
            .TVScraperEpisodePlot = Me.chkTVScraperEpisodePlot.Checked
            .TVScraperEpisodeRating = Me.chkTVScraperEpisodeRating.Checked
            .TVScraperEpisodeRuntime = Me.chkTVScraperEpisodeRuntime.Checked
            .TVScraperEpisodeTitle = Me.chkTVScraperEpisodeTitle.Checked
            .TVScraperMetaDataScan = Me.chkTVScraperMetaDataScan.Checked
            .TVScraperOptionsOrdering = CType(Me.cbTVScraperOptionsOrdering.SelectedItem, KeyValuePair(Of String, Enums.Ordering)).Value
            .TVScraperSeasonAired = Me.chkTVScraperSeasonAired.Checked
            .TVScraperSeasonPlot = Me.chkTVScraperSeasonPlot.Checked
            .TVScraperSeasonTitle = Me.chkTVScraperSeasonTitle.Checked
            .TVScraperShowActors = Me.chkTVScraperShowActors.Checked
            .TVScraperShowCert = Me.chkTVScraperShowCert.Checked
            .TVScraperShowCreators = Me.chkTVScraperShowCreators.Checked
            .TVScraperShowCertForMPAA = Me.chkTVScraperShowCertForMPAA.Checked
            .TVScraperShowCertForMPAAFallback = Me.chkTVScraperShowCertForMPAAFallback.Checked
            .TVScraperShowCertFSK = Me.chkTVScraperShowCertFSK.Checked
            .TVScraperShowCertOnlyValue = Me.chkTVScraperShowCertOnlyValue.Checked
            If Me.cbTVScraperShowCertLang.Text <> String.Empty Then
                If Me.cbTVScraperShowCertLang.SelectedIndex = 0 Then
                    .TVScraperShowCertLang = Master.eLang.All
                Else
                    .TVScraperShowCertLang = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.name = cbTVScraperShowCertLang.Text).abbreviation
                End If
            End If
            .TVScraperShowEpiGuideURL = Me.chkTVScraperShowEpiGuideURL.Checked
            .TVScraperShowGenre = Me.chkTVScraperShowGenre.Checked
            .TVScraperShowMPAA = Me.chkTVScraperShowMPAA.Checked
            .TVScraperShowOriginalTitle = Me.chkTVScraperShowOriginalTitle.Checked
            .TVScraperShowMPAANotRated = Me.txtTVScraperShowMPAANotRated.Text
            .TVScraperShowPlot = Me.chkTVScraperShowPlot.Checked
            .TVScraperShowPremiered = Me.chkTVScraperShowPremiered.Checked
            .TVScraperShowRating = Me.chkTVScraperShowRating.Checked
            .TVScraperShowRuntime = Me.chkTVScraperShowRuntime.Checked
            .TVScraperShowStatus = Me.chkTVScraperShowStatus.Checked
            .TVScraperShowStudio = Me.chkTVScraperShowStudio.Checked
            .TVScraperShowTitle = Me.chkTVScraperShowTitle.Checked
            .TVScraperUseDisplaySeasonEpisode = Me.chkTVScraperUseDisplaySeasonEpisode.Checked
            .TVScraperUseMDDuration = Me.chkTVScraperUseMDDuration.Checked
            .TVScraperUseSRuntimeForEp = Me.chkTVScraperUseSRuntimeForEp.Checked
            .TVSeasonBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonBannerHeight.Text), Convert.ToInt32(Me.txtTVSeasonBannerHeight.Text), 0)
            .TVSeasonBannerKeepExisting = Me.chkTVSeasonBannerKeepExisting.Checked
            .TVSeasonBannerPrefSize = CType(Me.cbTVSeasonBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVBannerSize)).Value
            .TVSeasonBannerPrefSizeOnly = Me.chkTVSeasonBannerPrefSizeOnly.Checked
            .TVSeasonBannerPrefType = CType(Me.cbTVSeasonBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVSeasonBannerResize = Me.chkTVSeasonBannerResize.Checked
            .TVSeasonBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonBannerWidth.Text), Convert.ToInt32(Me.txtTVSeasonBannerWidth.Text), 0)
            .TVSeasonFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonFanartHeight.Text), Convert.ToInt32(Me.txtTVSeasonFanartHeight.Text), 0)
            .TVSeasonFanartKeepExisting = Me.chkTVSeasonFanartKeepExisting.Checked
            .TVSeasonFanartPrefSize = CType(Me.cbTVSeasonFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVFanartSize)).Value
            .TVSeasonFanartPrefSizeOnly = Me.chkTVSeasonFanartPrefSizeOnly.Checked
            .TVSeasonFanartResize = Me.chkTVSeasonFanartResize.Checked
            .TVSeasonFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonFanartWidth.Text), Convert.ToInt32(Me.txtTVSeasonFanartWidth.Text), 0)
            .TVSeasonLandscapeKeepExisting = Me.chkTVSeasonLandscapeKeepExisting.Checked
            .TVSeasonPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVSeasonPosterHeight.Text), Convert.ToInt32(Me.txtTVSeasonPosterHeight.Text), 0)
            .TVSeasonPosterKeepExisting = Me.chkTVSeasonPosterKeepExisting.Checked
            .TVSeasonPosterPrefSize = CType(Me.cbTVSeasonPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVSeasonPosterSize)).Value
            .TVSeasonPosterPrefSizeOnly = Me.chkTVSeasonPosterPrefSizeOnly.Checked
            .TVSeasonPosterResize = Me.chkTVSeasonPosterResize.Checked
            .TVSeasonPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVSeasonPosterWidth.Text), Convert.ToInt32(Me.txtTVSeasonPosterWidth.Text), 0)
            .TVShowBannerHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowBannerHeight.Text), Convert.ToInt32(Me.txtTVShowBannerHeight.Text), 0)
            .TVShowBannerKeepExisting = Me.chkTVShowBannerKeepExisting.Checked
            .TVShowBannerPrefSize = CType(Me.cbTVShowBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVBannerSize)).Value
            .TVShowBannerPrefSizeOnly = Me.chkTVShowBannerPrefSizeOnly.Checked
            .TVShowBannerPrefType = CType(Me.cbTVShowBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVShowBannerResize = Me.chkTVShowBannerResize.Checked
            .TVShowBannerWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowBannerWidth.Text), Convert.ToInt32(Me.txtTVShowBannerWidth.Text), 0)
            .TVShowCharacterArtKeepExisting = Me.chkTVShowCharacterArtKeepExisting.Checked
            .TVShowClearArtKeepExisting = Me.chkTVShowClearArtKeepExisting.Checked
            .TVShowClearLogoKeepExisting = Me.chkTVShowClearLogoKeepExisting.Checked
            .TVShowExtrafanartsHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowExtrafanartsHeight.Text), Convert.ToInt32(Me.txtTVShowExtrafanartsHeight.Text), 0)
            .TVShowExtrafanartsLimit = If(Not String.IsNullOrEmpty(Me.txtTVShowExtrafanartsLimit.Text), Convert.ToInt32(Me.txtTVShowExtrafanartsLimit.Text), 0)
            .TVShowExtrafanartsKeepExisting = Me.chkTVShowExtrafanartsKeepExisting.Checked
            .TVShowExtrafanartsPrefOnly = Me.chkTVShowExtrafanartsPrefSizeOnly.Checked
            .TVShowExtrafanartsPrefSize = CType(Me.cbTVShowExtrafanartsPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVFanartSize)).Value
            .TVShowExtrafanartsPrefSizeOnly = Me.chkTVShowExtrafanartsPrefSizeOnly.Checked
            .TVShowExtrafanartsResize = Me.chkTVShowExtrafanartsResize.Checked
            .TVShowExtrafanartsWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowExtrafanartsWidth.Text), Convert.ToInt32(Me.txtTVShowExtrafanartsWidth.Text), 0)
            .TVShowFanartHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowFanartHeight.Text), Convert.ToInt32(Me.txtTVShowFanartHeight.Text), 0)
            .TVShowFanartKeepExisting = Me.chkTVShowFanartKeepExisting.Checked
            .TVShowFanartPrefSize = CType(Me.cbTVShowFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVFanartSize)).Value
            .TVShowFanartPrefSizeOnly = Me.chkTVShowFanartKeepExisting.Checked
            .TVShowFanartResize = Me.chkTVShowFanartResize.Checked
            .TVShowFanartWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowFanartWidth.Text), Convert.ToInt32(Me.txtTVShowFanartWidth.Text), 0)
            .TVShowFilterCustom.Clear()
            .TVShowFilterCustom.AddRange(Me.lstTVShowFilter.Items.OfType(Of String).ToList)
            If .TVShowFilterCustom.Count <= 0 Then .TVShowFilterCustomIsEmpty = True
            .TVShowLandscapeKeepExisting = Me.chkTVShowLandscapeKeepExisting.Checked
            .TVShowPosterHeight = If(Not String.IsNullOrEmpty(Me.txtTVShowPosterHeight.Text), Convert.ToInt32(Me.txtTVShowPosterHeight.Text), 0)
            .TVShowPosterKeepExisting = Me.chkTVShowPosterKeepExisting.Checked
            .TVShowPosterPrefSize = CType(Me.cbTVShowPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.TVPosterSize)).Value
            .TVShowPosterPrefSizeOnly = Me.chkTVShowPosterPrefSizeOnly.Checked
            .TVShowPosterResize = Me.chkTVShowPosterResize.Checked
            .TVShowPosterWidth = If(Not String.IsNullOrEmpty(Me.txtTVShowPosterWidth.Text), Convert.ToInt32(Me.txtTVShowPosterWidth.Text), 0)
            .TVShowProperCase = Me.chkTVShowProperCase.Checked
            .TVShowMatching.Clear()
            .TVShowMatching.AddRange(Me.TVShowMatching)
            .TVShowThemeKeepExisting = Me.chkTVShowThemeKeepExisting.Checked
            If Not String.IsNullOrEmpty(Me.txtTVSkipLessThan.Text) AndAlso Integer.TryParse(Me.txtTVSkipLessThan.Text, 0) Then
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
                    strGenre = String.Join(",", iChecked.ToArray)
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

            '********* XBMC Extended Images settings ***********
            .MovieBannerExtended = Me.chkMovieBannerExtended.Checked
            .MovieClearArtExtended = Me.chkMovieClearArtExtended.Checked
            .MovieClearLogoExtended = Me.chkMovieClearLogoExtended.Checked
            .MovieDiscArtExtended = Me.chkMovieDiscArtExtended.Checked
            .MovieLandscapeExtended = Me.chkMovieLandscapeExtended.Checked

            '************** XBMC TvTunes settings **************
            .MovieThemeTvTunesCustom = Me.chkMovieThemeTvTunesCustom.Checked
            .MovieThemeTvTunesCustomPath = Me.txtMovieThemeTvTunesCustomPath.Text
            .MovieThemeTvTunesEnable = Me.chkMovieThemeTvTunesEnabled.Checked
            .MovieThemeTvTunesMoviePath = Me.chkMovieThemeTvTunesMoviePath.Checked
            .MovieThemeTvTunesSub = Me.chkMovieThemeTvTunesSub.Checked
            .MovieThemeTvTunesSubDir = Me.txtMovieThemeTvTunesSubDir.Text

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
            .MovieSetPathMSAA = Me.txtMovieSetPathMSAA.Text
            .MovieSetPosterMSAA = Me.chkMovieSetPosterMSAA.Checked

            '********* XBMC Extended Images settings ***********
            .MovieSetBannerExtended = Me.chkMovieSetBannerExtended.Checked
            .MovieSetClearArtExtended = Me.chkMovieSetClearArtExtended.Checked
            .MovieSetClearLogoExtended = Me.chkMovieSetClearLogoExtended.Checked
            .MovieSetDiscArtExtended = Me.chkMovieSetDiscArtExtended.Checked
            .MovieSetFanartExtended = Me.chkMovieSetFanartExtended.Checked
            .MovieSetLandscapeExtended = Me.chkMovieSetLandscapeExtended.Checked
            .MovieSetPathExtended = Me.txtMovieSetPathExtended.Text
            .MovieSetPosterExtended = Me.chkMovieSetPosterExtended.Checked

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
            .TVEpisodeNFOFrodo = Me.chkTVEpisodeNFOFrodo.Checked
            .TVEpisodePosterFrodo = Me.chkTVEpisodePosterFrodo.Checked
            .TVSeasonBannerFrodo = Me.chkTVSeasonBannerFrodo.Checked
            .TVSeasonFanartFrodo = Me.chkTVSeasonFanartFrodo.Checked
            .TVSeasonPosterFrodo = Me.chkTVSeasonPosterFrodo.Checked
            .TVShowActorThumbsFrodo = Me.chkTVShowActorThumbsFrodo.Checked
            .TVShowBannerFrodo = Me.chkTVShowBannerFrodo.Checked
            .TVShowExtrafanartsFrodo = Me.chkTVShowExtrafanartsFrodo.Checked
            .TVShowFanartFrodo = Me.chkTVShowFanartFrodo.Checked
            .TVShowNFOFrodo = Me.chkTVShowNFOFrodo.Checked
            .TVShowPosterFrodo = Me.chkTVShowPosterFrodo.Checked

            '*************** XBMC Eden settings ****************

            '************* XBMC ArtworkDownloader settings **************
            .TVSeasonLandscapeAD = Me.chkTVSeasonLandscapeAD.Checked
            .TVShowCharacterArtAD = Me.chkTVShowCharacterArtAD.Checked
            .TVShowClearArtAD = Me.chkTVShowClearArtAD.Checked
            .TVShowClearLogoAD = Me.chkTVShowClearLogoAD.Checked
            .TVShowLandscapeAD = Me.chkTVShowLandscapeAD.Checked

            '************** XBMC TvTunes settings **************
            .TVShowThemeTvTunesEnable = Me.chkTVShowThemeTvTunesEnabled.Checked
            .TVShowThemeTvTunesCustom = Me.chkTVShowThemeTvTunesCustom.Checked
            .TVShowThemeTvTunesCustomPath = Me.txtTVShowThemeTvTunesCustomPath.Text
            .TVShowThemeTvTunesShowPath = Me.chkTVShowThemeTvTunesShowPath.Checked
            .TVShowThemeTvTunesSub = Me.chkTVShowThemeTvTunesSub.Checked
            .TVShowThemeTvTunesSubDir = Me.txtTVShowThemeTvTunesSubDir.Text

            '********* XBMC Extended Images settings ***********
            .TVSeasonLandscapeExtended = Me.chkTVSeasonLandscapeExtended.Checked
            .TVShowCharacterArtExtended = Me.chkTVShowCharacterArtExtended.Checked
            .TVShowClearArtExtended = Me.chkTVShowClearArtExtended.Checked
            .TVShowClearLogoExtended = Me.chkTVShowClearLogoExtended.Checked
            .TVShowLandscapeExtended = Me.chkTVShowLandscapeExtended.Checked

            '****************** YAMJ settings ******************
            .TVUseYAMJ = Me.chkTVUseYAMJ.Checked
            .TVEpisodeNFOYAMJ = Me.chkTVEpisodeNFOYAMJ.Checked
            .TVEpisodePosterYAMJ = Me.chkTVEpisodePosterYAMJ.Checked
            .TVSeasonBannerYAMJ = Me.chkTVSeasonBannerYAMJ.Checked
            .TVSeasonFanartYAMJ = Me.chkTVSeasonFanartYAMJ.Checked
            .TVSeasonPosterYAMJ = Me.chkTVSeasonPosterYAMJ.Checked
            .TVShowBannerYAMJ = Me.chkTVShowBannerYAMJ.Checked
            .TVShowFanartYAMJ = Me.chkTVShowFanartYAMJ.Checked
            .TVShowNFOYAMJ = Me.chkTVShowNFOYAMJ.Checked
            .TVShowPosterYAMJ = Me.chkTVShowPosterYAMJ.Checked

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            .TVUseBoxee = Me.chkTVUseBoxee.Checked
            .TVEpisodeNFOBoxee = Me.chkTVEpisodeNFOBoxee.Checked
            .TVEpisodePosterBoxee = Me.chkTVEpisodePosterBoxee.Checked
            .TVSeasonPosterBoxee = Me.chkTVSeasonPosterBoxee.Checked
            .TVShowBannerBoxee = Me.chkTVShowBannerBoxee.Checked
            .TVShowFanartBoxee = Me.chkTVShowFanartBoxee.Checked
            .TVShowNFOBoxee = Me.chkTVShowNFOBoxee.Checked
            .TVShowPosterBoxee = Me.chkTVShowPosterBoxee.Checked

            '***************** Expert settings ******************
            .TVUseExpert = Me.chkTVUseExpert.Checked

            '***************** Expert AllSeasons ****************
            .TVAllSeasonsBannerExpert = Me.txtTVAllSeasonsBannerExpert.Text
            .TVAllSeasonsFanartExpert = Me.txtTVAllSeasonsFanartExpert.Text
            .TVAllSeasonsLandscapeExpert = Me.txtTVAllSeasonsLandscapeExpert.Text
            .TVAllSeasonsPosterExpert = Me.txtTVAllSeasonsPosterExpert.Text

            '***************** Expert Episode *******************
            .TVEpisodeActorThumbsExpert = Me.chkTVEpisodeActorThumbsExpert.Checked
            .TVEpisodeActorThumbsExtExpert = Me.txtTVEpisodeActorThumbsExtExpert.Text
            .TVEpisodeFanartExpert = Me.txtTVEpisodeFanartExpert.Text
            .TVEpisodeNFOExpert = Me.txtTVEpisodeNFOExpert.Text
            .TVEpisodePosterExpert = Me.txtTVEpisodePosterExpert.Text

            '***************** Expert Season ********************
            .TVSeasonBannerExpert = Me.txtTVSeasonBannerExpert.Text
            .TVSeasonFanartExpert = Me.txtTVSeasonFanartExpert.Text
            .TVSeasonLandscapeExpert = Me.txtTVSeasonLandscapeExpert.Text
            .TVSeasonPosterExpert = Me.txtTVSeasonPosterExpert.Text

            '***************** Expert Show **********************
            .TVShowActorThumbsExpert = Me.chkTVShowActorThumbsExpert.Checked
            .TVShowActorThumbsExtExpert = Me.txtTVShowActorThumbsExtExpert.Text
            .TVShowBannerExpert = Me.txtTVShowBannerExpert.Text
            .TVShowCharacterArtExpert = Me.txtTVShowCharacterArtExpert.Text
            .TVShowClearArtExpert = Me.txtTVShowClearArtExpert.Text
            .TVShowClearLogoExpert = Me.txtTVShowClearLogoExpert.Text
            .TVShowExtrafanartsExpert = Me.chkTVShowExtrafanartsExpert.Checked
            .TVShowFanartExpert = Me.txtTVShowFanartExpert.Text
            .TVShowLandscapeExpert = Me.txtTVShowLandscapeExpert.Text
            .TVShowNFOExpert = Me.txtTVShowNFOExpert.Text
            .TVShowPosterExpert = Me.txtTVShowPosterExpert.Text


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
        For Each s As ModulesManager._externalScraperModuleClass_Data_TV In ModulesManager.Instance.externalScrapersModules_Data_TV
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
        For Each s As ModulesManager._externalScraperModuleClass_Theme_TV In ModulesManager.Instance.externalScrapersModules_Theme_TV
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

    Private Sub SetUp()

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        Me.chkMovieActorThumbsExpertBDMV.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertMulti.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertSingle.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertVTS.Text = strActorThumbs
        Me.chkTVEpisodeActorThumbsExpert.Text = strActorThumbs
        Me.chkTVShowActorThumbsExpert.Text = strActorThumbs
        Me.gbMovieImagesActorThumbsOpts.Text = strActorThumbs
        Me.lblMovieSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs
        Me.lblTVSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs

        'Actors
        Dim strActors As String = Master.eLang.GetString(231, "Actors")
        Me.lblMovieScraperGlobalActors.Text = strActors
        Me.lblTVScraperGlobalActors.Text = strActors

        'Add Episode Guest Stars to Actors list
        Dim strAddEPGuestStars As String = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        Me.chkTVScraperEpisodeGuestStarsToActors.Text = strAddEPGuestStars

        'Add <displayseason> and <displayepisode> to special episodes
        Dim strAddDisplaySE As String = Master.eLang.GetString(976, "Add <displayseason> and <displayepisode> to special episodes")
        Me.chkTVScraperUseDisplaySeasonEpisode.Text = strAddDisplaySE

        'Aired
        Dim strAired As String = Master.eLang.GetString(728, "Aired")
        Me.lblTVScraperGlobalAired.Text = strAired

        'All Seasons
        Dim strAllSeasons As String = Master.eLang.GetString(1202, "All Seasons")
        Me.tpTVImagesAllSeasons.Text = strAllSeasons
        Me.tpTVSourcesFileNamingExpertAllSeasons.Text = strAllSeasons

        'Also Get Blank Images
        Dim strAlsoGetBlankImages As String = Master.eLang.GetString(1207, "Also Get Blank Images")
        Me.chkMovieImagesGetBlankImages.Text = strAlsoGetBlankImages
        Me.chkMovieSetImagesGetBlankImages.Text = strAlsoGetBlankImages
        Me.chkTVImagesGetBlankImages.Text = strAlsoGetBlankImages

        'Also Get English Images
        Dim strAlsoGetEnglishImages As String = Master.eLang.GetString(737, "Also Get English Images")
        Me.chkMovieImagesGetEnglishImages.Text = strAlsoGetEnglishImages
        Me.chkMovieSetImagesGetEnglishImages.Text = strAlsoGetEnglishImages
        Me.chkTVImagesGetEnglishImages.Text = strAlsoGetEnglishImages

        'Ask On Click Scrape
        Dim strAskOnClickScrape As String = Master.eLang.GetString(852, "Ask On Click Scrape")
        Me.chkMovieClickScrapeAsk.Text = strAskOnClickScrape
        Me.chkMovieSetClickScrapeAsk.Text = strAskOnClickScrape
        Me.chkTVGeneralClickScrapeAsk.Text = strAskOnClickScrape

        'Automatically Resize:
        Dim strAutomaticallyResize As String = Master.eLang.GetString(481, "Automatically Resize:")
        Me.chkMoviePosterResize.Text = strAutomaticallyResize
        Me.chkMovieBannerResize.Text = strAutomaticallyResize
        Me.chkMovieExtrafanartsResize.Text = strAutomaticallyResize
        Me.chkMovieExtrathumbsResize.Text = strAutomaticallyResize
        Me.chkMovieFanartResize.Text = strAutomaticallyResize
        Me.chkMovieSetBannerResize.Text = strAutomaticallyResize
        Me.chkMovieSetFanartResize.Text = strAutomaticallyResize
        Me.chkMovieSetPosterResize.Text = strAutomaticallyResize
        Me.chkTVAllSeasonsBannerResize.Text = strAutomaticallyResize
        Me.chkTVAllSeasonsFanartResize.Text = strAutomaticallyResize
        Me.chkTVAllSeasonsPosterResize.Text = strAutomaticallyResize
        Me.chkTVEpisodeFanartResize.Text = strAutomaticallyResize
        Me.chkTVEpisodePosterResize.Text = strAutomaticallyResize
        Me.chkTVSeasonBannerResize.Text = strAutomaticallyResize
        Me.chkTVSeasonFanartResize.Text = strAutomaticallyResize
        Me.chkTVSeasonPosterResize.Text = strAutomaticallyResize
        Me.chkTVShowBannerResize.Text = strAutomaticallyResize
        Me.chkTVShowExtrafanartsResize.Text = strAutomaticallyResize
        Me.chkTVShowFanartResize.Text = strAutomaticallyResize
        Me.chkTVShowPosterResize.Text = strAutomaticallyResize

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        Me.gbMovieImagesBannerOpts.Text = strBanner
        Me.gbMovieSetImagesBannerOpts.Text = strBanner
        Me.gbTVImagesAllSeasonsBannerOpts.Text = strBanner
        Me.gbTVImagesSeasonBannerOpts.Text = strBanner
        Me.gbTVImagesShowBannerOpts.Text = strBanner
        Me.lblMovieSetSourcesFilenamingExpertParentBanner.Text = strBanner
        Me.lblMovieSetSourcesFilenamingExpertSingleBanner.Text = strBanner
        Me.lblMovieSetSourcesFilenamingKodiExtendedBanner.Text = strBanner
        Me.lblMovieSetSourcesFilenamingKodiMSAABanner.Text = strBanner
        Me.lblMovieSourcesFilenamingExpertBDMVBanner.Text = strBanner
        Me.lblMovieSourcesFilenamingExpertMultiBanner.Text = strBanner
        Me.lblMovieSourcesFilenamingExpertSingleBanner.Text = strBanner
        Me.lblMovieSourcesFilenamingExpertVTSBanner.Text = strBanner
        Me.lblMovieSourcesFilenamingKodiADBanner.Text = strBanner
        Me.lblMovieSourcesFilenamingKodiExtendedBanner.Text = strBanner
        Me.lblMovieSourcesFilenamingNMTDefaultsBanner.Text = strBanner
        Me.lblTVAllSeasonsBannerExpert.Text = strBanner
        Me.lblTVSeasonBannerExpert.Text = strBanner
        Me.lblTVShowBannerExpert.Text = strBanner
        Me.lblTVSourcesFilenamingBoxeeDefaultsBanner.Text = strBanner
        Me.lblTVSourcesFilenamingKodiDefaultsBanner.Text = strBanner
        Me.lblTVSourcesFilenamingNMTDefaultsBanner.Text = strBanner

        'Certifications
        Dim strCertifications As String = Master.eLang.GetString(56, "Certifications")
        Me.gbMovieScraperCertificationOpts.Text = strCertifications
        Me.gbTVScraperCertificationOpts.Text = strCertifications
        Me.lblMovieScraperGlobalCertifications.Text = strCertifications
        Me.lblTVScraperGlobalCertifications.Text = strCertifications

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        Me.gbTVImagesShowCharacterArtOpts.Text = strCharacterArt
        Me.lblTVShowCharacterArtExpert.Text = strCharacterArt
        Me.lblTVSourcesFileNamingXBMCADCharacterArt.Text = strCharacterArt
        Me.lblTVSourcesFileNamingKodiExtendedCharacterArt.Text = strCharacterArt

        'Cleanup disabled fields
        Dim strCleanUpDisabledFileds As String = Master.eLang.GetString(125, "Cleanup disabled fields")
        Me.chkMovieScraperCleanFields.Text = strCleanUpDisabledFileds
        Me.chkTVScraperCleanFields.Text = strCleanUpDisabledFileds

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        Me.gbMovieImagesClearArtOpts.Text = strClearArt
        Me.gbMovieSetImagesClearArtOpts.Text = strClearArt
        Me.gbTVImagesShowClearArtOpts.Text = strClearArt
        Me.lblMovieSetSourcesFilenamingExpertParentClearArt.Text = strClearArt
        Me.lblMovieSetSourcesFilenamingExpertSingleClearArt.Text = strClearArt
        Me.lblMovieSetSourcesFilenamingKodiExtendedClearArt.Text = strClearArt
        Me.lblMovieSetSourcesFilenamingKodiMSAAClearArt.Text = strClearArt
        Me.lblMovieSourcesFilenamingExpertBDMVClearArt.Text = strClearArt
        Me.lblMovieSourcesFilenamingExpertMultiClearArt.Text = strClearArt
        Me.lblMovieSourcesFilenamingExpertSingleClearArt.Text = strClearArt
        Me.lblMovieSourcesFilenamingExpertVTSClearArt.Text = strClearArt
        Me.lblMovieSourcesFileNamingKodiADClearArt.Text = strClearArt
        Me.lblMovieSourcesFileNamingKodiExtendedClearArt.Text = strClearArt
        Me.lblTVSourcesFilenamingExpertClearArt.Text = strClearArt
        Me.lblTVSourcesFileNamingKodiADClearArt.Text = strClearArt
        Me.lblTVSourcesFileNamingKodiExtendedClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        Me.gbMovieImagesClearLogoOpts.Text = strClearLogo
        Me.gbMovieSetImagesClearLogoOpts.Text = strClearLogo
        Me.gbTVImagesShowClearLogoOpts.Text = strClearLogo
        Me.lblMovieSetClearLogoExpertParent.Text = strClearLogo
        Me.lblMovieSetClearLogoExpertSingle.Text = strClearLogo
        Me.lblMovieSetSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo
        Me.lblMovieSetSourcesFilenamingKodiMSAAClearLogo.Text = strClearLogo
        Me.lblMovieClearLogoExpertBDMV.Text = strClearLogo
        Me.lblMovieClearLogoExpertMulti.Text = strClearLogo
        Me.lblMovieClearLogoExpertSingle.Text = strClearLogo
        Me.lblMovieClearLogoExpertVTS.Text = strClearLogo
        Me.lblMovieSourcesFileNamingXBMCADClearLogo.Text = strClearLogo
        Me.lblMovieSourcesFileNamingXBMCExtendedClearLogo.Text = strClearLogo
        Me.lblTVShowClearLogoExpert.Text = strClearLogo
        Me.lblTVSourcesFileNamingXBMCADClearLogo.Text = strClearLogo
        Me.lblTVSourcesFileNamingKodiExtendedClearLogo.Text = strClearLogo

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

        'Countries
        Dim strCountries As String = Master.eLang.GetString(237, "Countries")
        Me.lblMovieScraperGlobalCountries.Text = strCountries

        'Creators
        Dim strCreators As String = Master.eLang.GetString(744, "Creators")
        Me.lblTVScraperGlobalCreators.Text = strCreators

        'Default Episode Ordering
        Dim strDefaultEpisodeOrdering As String = Master.eLang.GetString(797, "Default Episode Ordering")
        Me.lblTVSourcesDefaultsOrdering.Text = String.Concat(strDefaultEpisodeOrdering, ":")

        'Default Language
        Dim strDefaultLanguage As String = Master.eLang.GetString(1166, "Default Language")
        Me.lblMovieSourcesDefaultsLanguage.Text = String.Concat(strDefaultLanguage, ":")
        Me.lblTVSourcesDefaultsLanguage.Text = String.Concat(strDefaultLanguage, ":")

        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        Me.gbMovieSourcesFileNamingBoxeeDefaultsOpts.Text = strDefaults
        Me.gbMovieSourcesFileNamingNMTDefaultsOpts.Text = strDefaults
        Me.gbMovieSourcesFileNamingKodiDefaultsOpts.Text = strDefaults
        Me.gbTVSourcesFileNamingBoxeeDefaultsOpts.Text = strDefaults
        Me.gbTVSourcesFileNamingNMTDefaultsOpts.Text = strDefaults
        Me.gbTVSourcesFileNamingKodiDefaultsOpts.Text = strDefaults

        'Defaults for new Sources
        Dim strDefaultsForNewSources As String = Master.eLang.GetString(252, "Defaults for new Sources")
        Me.gbMovieSourcesDefaultsOpts.Text = strDefaultsForNewSources
        Me.gbTVSourcesDefaultsOpts.Text = strDefaultsForNewSources


        'Defaults by File Type
        Dim strDefaultsByFileType As String = Master.eLang.GetString(625, "Defaults by File Type")
        Me.gbMovieScraperDefFIExtOpts.Text = strDefaultsByFileType
        Me.gbTVScraperDefFIExtOpts.Text = strDefaultsByFileType

        'Directors
        Dim strDirectors As String = Master.eLang.GetString(940, "Directors")
        Me.lblMovieScraperGlobalDirectors.Text = strDirectors
        Me.lblTVScraperGlobalDirectors.Text = strDirectors

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        Me.gbMovieImagesDiscArtOpts.Text = strDiscArt
        Me.gbMovieSetImagesDiscArtOpts.Text = strDiscArt
        Me.lblMovieSetDiscArtExpertParent.Text = strDiscArt
        Me.lblMovieSetDiscArtExpertSingle.Text = strDiscArt
        Me.lblMovieSetSourcesFilenamingKodiExtendedDiscArt.Text = strDiscArt
        Me.lblMovieDiscArtExpertBDMV.Text = strDiscArt
        Me.lblMovieDiscArtExpertMulti.Text = strDiscArt
        Me.lblMovieDiscArtExpertSingle.Text = strDiscArt
        Me.lblMovieDiscArtExpertVTS.Text = strDiscArt
        Me.lblMovieSourcesFileNamingXBMCADDiscArt.Text = strDiscArt
        Me.lblMovieSourcesFileNamingXBMCExtendedDiscArt.Text = strDiscArt

        'Display best Audio Stream with the following Language
        Dim strDisplayLanguageBestAudio As String = String.Concat(Master.eLang.GetString(436, "Display best Audio Stream with the following Language"), ":")
        Me.lblMovieLanguageOverlay.Text = strDisplayLanguageBestAudio
        Me.lblTVLanguageOverlay.Text = strDisplayLanguageBestAudio

        'Display "Image Select" dialog while single scraping
        Dim strDisplayImgDialog As String = Master.eLang.GetString(499, "Display ""Image Select"" dialog while single scraping")
        Me.chkMovieImagesDisplayImageSelect.Text = strDisplayImgDialog
        Me.chkMovieSetImagesDisplayImageSelect.Text = strDisplayImgDialog
        Me.chkTVImagesDisplayImageSelect.Text = strDisplayImgDialog

        'Do not save URLs to NFO"
        Dim strNotSaveURLToNfo As String = Master.eLang.GetString(498, "Do not save URLs to NFO")
        Me.chkMovieImagesNotSaveURLToNfo.Text = strNotSaveURLToNfo
        Me.chkTVImagesNotSaveURLToNfo.Text = strNotSaveURLToNfo

        'Duration Format
        Dim strDurationFormat As String = Master.eLang.GetString(515, "Duration Format")
        Me.gbMovieScraperDurationFormatOpts.Text = strDurationFormat
        Me.gbTVScraperDurationFormatOpts.Text = strDurationFormat

        'Duration Runtime Format
        Dim strDurationRuntimeFormat As String = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
        Me.lblMovieScraperDurationRuntimeFormat.Text = strDurationRuntimeFormat
        Me.lblTVScraperDurationRuntimeFormat.Text = strDurationRuntimeFormat

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        Me.lblMovieSetSourcesFilenamingKodiMSAAEnabled.Text = strEnabled
        Me.lblMovieSourcesFileNamingBoxeeDefaultsEnabled.Text = strEnabled
        Me.lblMovieSourcesFileNamingNMTDefaultsEnabled.Text = strEnabled
        Me.lblMovieSourcesFileNamingXBMCDefaultsEnabled.Text = strEnabled
        Me.lblTVSourcesFileNamingBoxeeDefaultsEnabled.Text = strEnabled
        Me.lblTVSourcesFileNamingNMTDefaultsEnabled.Text = strEnabled
        Me.lblTVSourcesFileNamingXBMCDefaultsEnabled.Text = strEnabled
        Me.chkMovieUseExpert.Text = strEnabled
        Me.chkMovieSetUseExpert.Text = strEnabled
        Me.chkMovieThemeTvTunesEnabled.Text = strEnabled
        Me.chkTVShowThemeTvTunesEnabled.Text = strEnabled
        Me.chkTVUseExpert.Text = strEnabled

        'Enabled Click Scrape
        Dim strEnabledClickScrape As String = Master.eLang.GetString(849, "Enable Click Scrape")
        Me.chkMovieClickScrape.Text = strEnabledClickScrape
        Me.chkMovieSetClickScrape.Text = strEnabledClickScrape
        Me.chkTVGeneralClickScrape.Text = strEnabledClickScrape

        'Enable Image Caching
        Dim strEnableImageCaching As String = Master.eLang.GetString(249, "Enable Image Caching")
        Me.chkMovieImagesCacheEnabled.Text = strEnableImageCaching
        Me.chkMovieSetImagesCacheEnabled.Text = strEnableImageCaching
        Me.chkTVImagesCacheEnabled.Text = strEnableImageCaching

        'Enable Theme Support
        Dim strEnableThemeSupport As String = Master.eLang.GetString(1082, "Enable Theme Support")

        'Episode
        Dim strEpisode As String = Master.eLang.GetString(727, "Episode")
        Me.lblTVSourcesFileNamingBoxeeDefaultsHeaderBoxeeEpisode.Text = strEpisode
        Me.lblTVSourcesFileNamingNMTDefaultsHeaderNMJEpisode.Text = strEpisode
        Me.lblTVSourcesFileNamingNMTDefaultsHeaderYAMJEpisode.Text = strEpisode
        Me.lblTVSourcesFileNamingXBMCDefaultsHeaderFrodoHelixEpisode.Text = strEpisode
        Me.tpTVImagesEpisode.Text = strEpisode
        Me.tpTVSourcesFileNamingExpertEpisode.Text = strEpisode

        'Episode #
        Dim strEpisodeNR As String = Master.eLang.GetString(660, "Episode #")

        'Episode List Sorting
        Dim strEpisodeListSorting As String = Master.eLang.GetString(494, "Episode List Sorting")
        Me.gbTVGeneralEpisodeListSorting.Text = strEpisodeListSorting

        'Episode Guide URL
        Dim strEpisodeGuideURL As String = Master.eLang.GetString(723, "Episode Guide URL")
        lblTVScraperGlobalEpisodeGuideURL.Text = strEpisodeGuideURL

        'Episodes
        Dim strEpisodes As String = Master.eLang.GetString(682, "Episodes")
        Me.lblTVScraperGlobalHeaderEpisodes.Text = strEpisodes

        'Exclude
        Dim strExclude As String = Master.eLang.GetString(264, "Exclude")
        Me.colMovieSourcesExclude.Text = strExclude
        Me.colTVSourcesExclude.Text = strExclude

        'Expert
        Dim strExpert As String = Master.eLang.GetString(439, "Expert")
        Me.tpMovieSourcesFileNamingExpert.Text = strExpert
        Me.tpMovieSetFileNamingExpert.Text = strExpert
        Me.tpTVSourcesFileNamingExpert.Text = strExpert

        'Expert Settings
        Dim strExpertSettings As String = Master.eLang.GetString(1181, "Expert Settings")
        Me.gbMovieSourcesFileNamingExpertOpts.Text = strExpertSettings
        Me.gbMovieSetSourcesFileNamingExpertOpts.Text = strExpertSettings
        Me.gbTVSourcesFileNamingExpertOpts.Text = strExpertSettings

        'Extended Images
        Dim strExtendedImages As String = Master.eLang.GetString(822, "Extended Images")
        Me.gbMovieSourcesFileNamingKodiExtendedOpts.Text = strExtendedImages
        Me.gbMovieSetSourcesFileNamingKodiExtendedOpts.Text = strExtendedImages
        Me.gbTVSourcesFileNamingKodiExtendedOpts.Text = strExtendedImages

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        Me.chkMovieExtrafanartsExpertBDMV.Text = strExtrafanarts
        Me.chkMovieExtrafanartsExpertSingle.Text = strExtrafanarts
        Me.chkMovieExtrafanartsExpertVTS.Text = strExtrafanarts
        Me.chkTVShowExtrafanartsExpert.Text = strExtrafanarts
        Me.gbMovieImagesExtrafanartsOpts.Text = strExtrafanarts
        Me.gbTVImagesShowExtrafanartsOpts.Text = strExtrafanarts
        Me.lblMovieSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts
        Me.lblTVSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        Me.chkMovieExtrathumbsExpertBDMV.Text = strExtrathumbs
        Me.chkMovieExtrathumbsExpertSingle.Text = strExtrathumbs
        Me.chkMovieExtrathumbsExpertVTS.Text = strExtrathumbs
        Me.gbMovieImagesExtrathumbsOpts.Text = strExtrathumbs
        Me.lblMovieSourcesFileNamingXBMCDefaultsExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        Me.gbMovieImagesFanartOpts.Text = strFanart
        Me.gbMovieSetImagesFanartOpts.Text = strFanart
        Me.gbTVImagesAllSeasonsFanartOpts.Text = strFanart
        Me.gbTVImagesEpisodeFanartOpts.Text = strFanart
        Me.gbTVImagesSeasonFanartOpts.Text = strFanart
        Me.gbTVImagesShowFanartOpts.Text = strFanart
        Me.lblMovieSetSourcesFilenamingExpertParentFanart.Text = strFanart
        Me.lblMovieSetSourcesFilenamingExpertSingleFanart.Text = strFanart
        Me.lblMovieSetSourcesFilenamingKodiExtendedFanart.Text = strFanart
        Me.lblMovieSetSourcesFilenamingKodiMSAAFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingBoxeeDefaultsFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertBDMVFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertMultiFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertSingleFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertVTSFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingNMTDefaultsFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingXBMCDefaultsFanart.Text = strFanart
        Me.lblTVAllSeasonsFanartExpert.Text = strFanart
        Me.lblTVEpisodeFanartExpert.Text = strFanart
        Me.lblTVSeasonFanartExpert.Text = strFanart
        Me.lblTVShowFanartExpert.Text = strFanart
        Me.lblTVSourcesFilenamingBoxeeDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFilenamingNMTDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFilenamingXBMCDefaultsFanart.Text = strFanart

        'File Naming
        Dim strFileNaming As String = Master.eLang.GetString(471, "File Naming")
        Me.gbMovieSourcesFileNamingOpts.Text = strFileNaming
        Me.gbMovieSetSourcesFileNamingOpts.Text = strFileNaming
        Me.gbTVSourcesFileNamingOpts.Text = strFileNaming

        'File Type
        Dim strFileType As String = String.Concat(Master.eLang.GetString(626, "File Type"), ":")
        Me.lblMovieScraperDefFIExt.Text = strFileType
        Me.lblTVScraperDefFIExt.Text = strFileType

        'Genres
        Dim strGenres As String = Master.eLang.GetString(725, "Genres")
        Me.lblMovieScraperGlobalGenres.Text = strGenres
        Me.lblTVScraperGlobalGenres.Text = strGenres

        'Get Year
        Dim strGetYear As String = Master.eLang.GetString(586, "Get Year")
        Me.colMovieSourcesGetYear.Text = strGetYear

        'Hide
        Dim strHide As String = Master.eLang.GetString(465, "Hide")
        Me.colMovieGeneralMediaListSortingHide.Text = strHide
        Me.colMovieSetGeneralMediaListSortingHide.Text = strHide
        Me.colTVGeneralEpisodeListSortingHide.Text = strHide
        Me.colTVGeneralSeasonListSortingHide.Text = strHide
        Me.colTVGeneralShowListSortingHide.Text = strHide

        'Images
        Dim strImages As String = Master.eLang.GetString(497, "Images")
        Me.gbMovieImagesOpts.Text = strImages
        Me.gbMovieSetImagesOpts.Text = strImages
        Me.gbTVImagesOpts.Text = strImages

        'Keep existing
        Dim strKeepExisting As String = Master.eLang.GetString(971, "Keep existing")
        Me.chkMovieActorThumbsKeepExisting.Text = strKeepExisting
        Me.chkMovieBannerKeepExisting.Text = strKeepExisting
        Me.chkMovieClearArtKeepExisting.Text = strKeepExisting
        Me.chkMovieClearLogoKeepExisting.Text = strKeepExisting
        Me.chkMovieDiscArtKeepExisting.Text = strKeepExisting
        Me.chkMovieExtrafanartsKeepExisting.Text = strKeepExisting
        Me.chkMovieExtrathumbsKeepExisting.Text = strKeepExisting
        Me.chkMovieFanartKeepExisting.Text = strKeepExisting
        Me.chkMovieLandscapeKeepExisting.Text = strKeepExisting
        Me.chkMoviePosterKeepExisting.Text = strKeepExisting
        Me.chkMovieSetBannerKeepExisting.Text = strKeepExisting
        Me.chkMovieSetClearArtKeepExisting.Text = strKeepExisting
        Me.chkMovieSetClearLogoKeepExisting.Text = strKeepExisting
        Me.chkMovieSetDiscArtKeepExisting.Text = strKeepExisting
        Me.chkMovieSetFanartKeepExisting.Text = strKeepExisting
        Me.chkMovieSetLandscapeKeepExisting.Text = strKeepExisting
        Me.chkMovieSetPosterKeepExisting.Text = strKeepExisting
        Me.chkMovieThemeKeepExisting.Text = strKeepExisting
        Me.chkMovieTrailerKeepExisting.Text = strKeepExisting
        Me.chkTVAllSeasonsBannerKeepExisting.Text = strKeepExisting
        Me.chkTVAllSeasonsFanartKeepExisting.Text = strKeepExisting
        Me.chkTVAllSeasonsLandscapeKeepExisting.Text = strKeepExisting
        Me.chkTVAllSeasonsPosterKeepExisting.Text = strKeepExisting
        Me.chkTVEpisodeFanartKeepExisting.Text = strKeepExisting
        Me.chkTVEpisodePosterKeepExisting.Text = strKeepExisting
        Me.chkTVSeasonBannerKeepExisting.Text = strKeepExisting
        Me.chkTVSeasonFanartKeepExisting.Text = strKeepExisting
        Me.chkTVSeasonLandscapeKeepExisting.Text = strKeepExisting
        Me.chkTVSeasonPosterKeepExisting.Text = strKeepExisting
        Me.chkTVShowBannerKeepExisting.Text = strKeepExisting
        Me.chkTVShowCharacterArtKeepExisting.Text = strKeepExisting
        Me.chkTVShowClearArtKeepExisting.Text = strKeepExisting
        Me.chkTVShowClearLogoKeepExisting.Text = strKeepExisting
        Me.chkTVShowExtrafanartsKeepExisting.Text = strKeepExisting
        Me.chkTVShowFanartKeepExisting.Text = strKeepExisting
        Me.chkTVShowLandscapeKeepExisting.Text = strKeepExisting
        Me.chkTVShowPosterKeepExisting.Text = strKeepExisting
        Me.chkTVShowThemeKeepExisting.Text = strKeepExisting

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        Me.gbMovieImagesLandscapeOpts.Text = strLandscape
        Me.gbMovieSetImagesLandscapeOpts.Text = strLandscape
        Me.gbTVImagesAllSeasonsLandscapeOpts.Text = strLandscape
        Me.gbTVImagesSeasonLandscapeOpts.Text = strLandscape
        Me.gbTVImagesShowLandscapeOpts.Text = strLandscape
        Me.lblMovieSetLandscapeExpertParent.Text = strLandscape
        Me.lblMovieSetLandscapeExpertSingle.Text = strLandscape
        Me.lblMovieSetSourcesFilenamingKodiExtendedLandscape.Text = strLandscape
        Me.lblMovieSetSourcesFilenamingKodiMSAALandscape.Text = strLandscape
        Me.lblMovieSourcesFilenamingExpertBDMVLandscape.Text = strLandscape
        Me.lblMovieSourcesFilenamingExpertMultiLandscape.Text = strLandscape
        Me.lblMovieSourcesFilenamingExpertSingleLandscape.Text = strLandscape
        Me.lblMovieSourcesFilenamingExpertVTSLandscape.Text = strLandscape
        Me.lblMovieSourcesFilenamingXBMCADLandscape.Text = strLandscape
        Me.lblMovieSourcesFilenamingXBMCExtendedLandscape.Text = strLandscape
        Me.lblTVAllSeasonsLandscapeExpert.Text = strLandscape
        Me.lblTVSeasonLandscapeExpert.Text = strLandscape
        Me.lblTVShowLandscapeExpert.Text = strLandscape

        'Language
        Dim strLanguage As String = Master.eLang.GetString(610, "Language")
        Me.colMovieSourcesLanguage.Text = strLanguage
        Me.colTVSourcesLanguage.Text = strLanguage

        'Language (Audio)
        Dim strLanguageAudio As String = Master.eLang.GetString(431, "Language (Audio)")
        Me.lblMovieScraperGlobalLanguageA.Text = strLanguageAudio
        Me.lblTVScraperGlobalLanguageA.Text = strLanguageAudio

        'Language (Video)
        Dim strLanguageVideo As String = Master.eLang.GetString(435, "Language (Video)")
        Me.lblMovieScraperGlobalLanguageV.Text = strLanguageVideo
        Me.lblTVScraperGlobalLanguageV.Text = strLanguageVideo

        'Limit
        Dim strLimit As String = Master.eLang.GetString(578, "Limit")
        Me.lblMovieExtrafanartsLimit.Text = String.Concat(strLimit, ":")
        Me.lblMovieExtrathumbsLimit.Text = String.Concat(strLimit, ":")
        Me.lblMovieScraperGlobalHeaderLimit.Text = strLimit
        Me.lblMovieScraperOutlineLimit.Text = String.Concat(strLimit, ":")
        Me.lblTVShowExtrafanartsLimit.Text = String.Concat(strLimit, ":")

        'Lock
        Dim strLock As String = Master.eLang.GetString(24, "Lock")
        Me.lblMovieScraperGlobalHeaderLock.Text = strLock
        Me.lblMovieSetScraperGlobalHeaderLock.Text = strLock
        Me.lblTVScraperGlobalHeaderEpisodesLock.Text = strLock
        Me.lblTVScraperGlobalHeaderSeasonsLock.Text = strLock
        Me.lblTVScraperGlobalHeaderShowsLock.Text = strLock

        'Main Window
        Dim strMainWindow As String = Master.eLang.GetString(1152, "Main Window")
        Me.gbGeneralMainWindowOpts.Text = strMainWindow
        Me.gbMovieGeneralMainWindowOpts.Text = strMainWindow
        Me.gbTVGeneralMainWindowOpts.Text = strMainWindow

        'Max Height:
        Dim strMaxHeight As String = Master.eLang.GetString(480, "Max Height:")
        Me.lblMovieBannerHeight.Text = strMaxHeight
        Me.lblMovieExtrafanartsHeight.Text = strMaxHeight
        Me.lblMovieExtrathumbsHeight.Text = strMaxHeight
        Me.lblMovieFanartHeight.Text = strMaxHeight
        Me.lblMoviePosterHeight.Text = strMaxHeight
        Me.lblMovieSetBannerHeight.Text = strMaxHeight
        Me.lblMovieSetFanartHeight.Text = strMaxHeight
        Me.lblMovieSetPosterHeight.Text = strMaxHeight
        Me.lblTVAllSeasonsBannerHeight.Text = strMaxHeight
        Me.lblTVAllSeasonsFanartHeight.Text = strMaxHeight
        Me.lblTVAllSeasonsPosterHeight.Text = strMaxHeight
        Me.lblTVEpisodeFanartHeight.Text = strMaxHeight
        Me.lblTVEpisodePosterHeight.Text = strMaxHeight
        Me.lblTVSeasonBannerHeight.Text = strMaxHeight
        Me.lblTVSeasonFanartHeight.Text = strMaxHeight
        Me.lblTVSeasonPosterHeight.Text = strMaxHeight
        Me.lblTVShowBannerHeight.Text = strMaxHeight
        Me.lblTVShowExtrafanartsHeight.Text = strMaxHeight
        Me.lblTVShowFanartHeight.Text = strMaxHeight
        Me.lblTVShowPosterHeight.Text = strMaxHeight

        'Max Height:
        Dim strMaxWidth As String = Master.eLang.GetString(479, "Max Width:")
        Me.lblMovieBannerWidth.Text = strMaxWidth
        Me.lblMovieExtrafanartsWidth.Text = strMaxWidth
        Me.lblMovieExtrathumbsWidth.Text = strMaxWidth
        Me.lblMovieFanartWidth.Text = strMaxWidth
        Me.lblMoviePosterWidth.Text = strMaxWidth
        Me.lblMovieSetBannerWidth.Text = strMaxWidth
        Me.lblMovieSetFanartWidth.Text = strMaxWidth
        Me.lblMovieSetPosterWidth.Text = strMaxWidth
        Me.lblTVAllSeasonsBannerWidth.Text = strMaxWidth
        Me.lblTVAllSeasonsFanartWidth.Text = strMaxWidth
        Me.lblTVAllSeasonsPosterWidth.Text = strMaxWidth
        Me.lblTVEpisodeFanartWidth.Text = strMaxWidth
        Me.lblTVEpisodePosterWidth.Text = strMaxWidth
        Me.lblTVSeasonBannerWidth.Text = strMaxWidth
        Me.lblTVSeasonFanartWidth.Text = strMaxWidth
        Me.lblTVSeasonPosterWidth.Text = strMaxWidth
        Me.lblTVShowBannerWidth.Text = strMaxWidth
        Me.lblTVShowExtrafanartsWidth.Text = strMaxWidth
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
        Me.lblTVScraperGlobalMPAA.Text = strMPAA

        'MPAA value if no rating is available
        Dim strMPAANotRated As String = Master.eLang.GetString(832, "MPAA value if no rating is available")
        Me.lblTVScraperShowMPAANotRated.Text = strMPAANotRated

        'Movie List Sorting
        Dim strMovieListSorting As String = Master.eLang.GetString(490, "Movie List Sorting")
        Me.gbMovieGeneralMediaListSorting.Text = strMovieListSorting

        'MovieSet List Sorting
        Dim strMovieSetListSorting As String = Master.eLang.GetString(491, "MovieSet List Sorting")
        Me.gbMovieSetGeneralMediaListSorting.Text = strMovieSetListSorting

        'Name
        Dim strName As String = Master.eLang.GetString(232, "Name")
        Me.colMovieSourcesName.Text = strName
        Me.colTVSourcesName.Text = strName

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        Me.lblMovieSetSourcesFileNamingExpertParentNFO.Text = strNFO
        Me.lblMovieSetSourcesFileNamingExpertSingleNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingBoxeeDefaultsNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertBDMVNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertMultiNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertSingleNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertVTSNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingNMTDefaultsNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingXBMCDefaultsNFO.Text = strNFO
        Me.lblTVEpisodeNFOExpert.Text = strNFO
        Me.lblTVShowNFOExpert.Text = strNFO
        Me.lblTVSourcesFilenamingXBMCDefaultsNFO.Text = strNFO

        'Only
        Dim strOnly As String = Master.eLang.GetString(145, "Only")
        Me.chkMovieBannerPrefOnly.Text = strOnly
        Me.chkMovieExtrafanartsPrefOnly.Text = strOnly
        Me.chkMovieExtrathumbsPrefOnly.Text = strOnly
        Me.chkMovieFanartPrefOnly.Text = strOnly
        Me.chkMoviePosterPrefOnly.Text = strOnly
        Me.chkMovieSetBannerPrefOnly.Text = strOnly
        Me.chkMovieSetFanartPrefOnly.Text = strOnly
        Me.chkMovieSetPosterPrefOnly.Text = strOnly
        Me.chkTVAllSeasonsBannerPrefSizeOnly.Text = strOnly
        Me.chkTVAllSeasonsFanartPrefSizeOnly.Text = strOnly
        Me.chkTVAllSeasonsPosterPrefSizeOnly.Text = strOnly
        Me.chkTVEpisodeFanartPrefSizeOnly.Text = strOnly
        Me.chkTVEpisodePosterPrefSizeOnly.Text = strOnly
        Me.chkTVSeasonBannerPrefSizeOnly.Text = strOnly
        Me.chkTVSeasonFanartPrefSizeOnly.Text = strOnly
        Me.chkTVSeasonPosterPrefSizeOnly.Text = strOnly
        Me.chkTVShowBannerPrefSizeOnly.Text = strOnly
        Me.chkTVShowExtrafanartsPrefSizeOnly.Text = strOnly
        Me.chkTVShowFanartPrefSizeOnly.Text = strOnly
        Me.chkTVShowPosterPrefSizeOnly.Text = strOnly

        'Only Get Images for the Media Language
        Dim strOnlyImgMediaLang As String = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        Me.chkMovieImagesMediaLanguageOnly.Text = strOnlyImgMediaLang
        Me.chkMovieSetImagesMediaLanguageOnly.Text = strOnlyImgMediaLang
        Me.chkTVImagesMediaLanguageOnly.Text = strOnlyImgMediaLang

        'Only if no MPAA is found
        Dim strOnlyIfNoMPAA As String = Master.eLang.GetString(1293, "Only if no MPAA is found")
        Me.chkMovieScraperCertForMPAAFallback.Text = strOnlyIfNoMPAA
        Me.chkTVScraperShowCertForMPAAFallback.Text = strOnlyIfNoMPAA

        'Only Save the Value to NFO
        Dim strOnlySaveValueToNFO As String = Master.eLang.GetString(835, "Only Save the Value to NFO")
        Me.chkMovieScraperCertOnlyValue.Text = strOnlySaveValueToNFO
        Me.chkTVScraperShowCertOnlyValue.Text = strOnlySaveValueToNFO

        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        Me.gbMovieSourcesFileNamingExpertBDMVImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertMultiImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertSingleImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertVTSImagesOpts.Text = strOptionalImages
        Me.gbTVSourcesFileNamingExpertEpisodeImagesOpts.Text = strOptionalImages
        Me.gbTVSourcesFileNamingExpertShowImagesOpts.Text = strOptionalImages

        'Optional Settings
        Dim strOptionalSettings As String = Master.eLang.GetString(1175, "Optional Settings")
        Me.gbMovieSourcesFileNamingExpertBDMVOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertMultiOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertSingleOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertVTSOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingNMTOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingKodiOptionalOpts.Text = strOptionalSettings

        'Ordering
        Dim strOrdering As String = Master.eLang.GetString(1167, "Ordering")
        Me.colTVSourcesOrdering.Text = strOrdering

        'Original Title
        Dim strOriginalTitle As String = Master.eLang.GetString(302, "Original Title")
        Me.lblMovieScraperGlobalOriginalTitle.Text = strOriginalTitle

        'Part of a MovieSet
        Dim strPartOfAMovieSet As String = Master.eLang.GetString(1295, "Part of a MovieSet")

        'Path
        Dim strPath As String = Master.eLang.GetString(410, "Path")
        Me.lblMovieSetPathExpertSingle.Text = strPath
        Me.lblMovieSetSourcesFilenamingKodiExtendedPath.Text = strPath
        Me.lblMovieSetSourcesFilenamingKodiMSAAPath.Text = strPath
        Me.colMovieSourcesPath.Text = strPath
        Me.colTVSourcesPath.Text = strPath

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
        Me.lblMovieSetPosterExpertParent.Text = strPoster
        Me.lblMovieSetPosterExpertSingle.Text = strPoster
        Me.lblMovieSetSourcesFilenamingKodiExtendedPoster.Text = strPoster
        Me.lblMovieSetSourcesFilenamingKodiMSAAPoster.Text = strPoster
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
        Me.lblTVAllSeasonsPosterExpert.Text = strPoster
        Me.lblTVEpisodePosterExpert.Text = strPoster
        Me.lblTVSeasonPosterExpert.Text = strPoster
        Me.lblTVShowPosterExpert.Text = strPoster

        'Preferred Language
        Dim strPreferredLanguage As String = Master.eLang.GetString(741, "Preferred Language")
        Me.gbMovieImagesLanguageOpts.Text = strPreferredLanguage
        Me.gbMovieSetImagesLanguageOpts.Text = strPreferredLanguage
        Me.gbTVImagesLanguageOpts.Text = strPreferredLanguage

        'Preferred Size:
        Dim strPreferredSize As String = Master.eLang.GetString(482, "Preferred Size:")
        Me.lblMovieBannerSize.Text = strPreferredSize
        Me.lblMovieExtrafanartsSize.Text = strPreferredSize
        Me.lblMovieExtrathumbsSize.Text = strPreferredSize
        Me.lblMovieFanartSize.Text = strPreferredSize
        Me.lblMoviePosterSize.Text = strPreferredSize
        Me.lblMovieSetBannerSize.Text = strPreferredSize
        Me.lblMovieSetFanartSize.Text = strPreferredSize
        Me.lblMovieSetPosterSize.Text = strPreferredSize
        Me.lblTVAllSeasonsBannerPrefSize.Text = strPreferredSize
        Me.lblTVAllSeasonsFanartPrefSize.Text = strPreferredSize
        Me.lblTVAllSeasonsPosterPrefSize.Text = strPreferredSize
        Me.lblTVEpisodeFanartPrefSize.Text = strPreferredSize
        Me.lblTVEpisodePosterPrefSize.Text = strPreferredSize
        Me.lblTVSeasonBannerPrefSize.Text = strPreferredSize
        Me.lblTVSeasonFanartPrefSize.Text = strPreferredSize
        Me.lblTVSeasonPosterPrefSize.Text = strPreferredSize
        Me.lblTVShowBannerPrefSize.Text = strPreferredSize
        Me.lblTVShowExtrafanartsSize.Text = strPreferredSize
        Me.lblTVShowFanartPrefSize.Text = strPreferredSize
        Me.lblTVShowPosterPrefSize.Text = strPreferredSize

        'Preferred Type:
        Dim strPreferredType As String = Master.eLang.GetString(730, "Preferred Type:")
        Me.lblTVAllSeasonsBannerPrefType.Text = strPreferredType
        Me.lblTVSeasonBannerPrefType.Text = strPreferredType
        Me.lblTVShowBannerPrefType.Text = strPreferredType

        'Recusive
        Dim strRecursive = Master.eLang.GetString(411, "Recursive")
        Me.colMovieSourcesRecur.Text = strRecursive

        'Release Date
        Dim strReleaseDate As String = Master.eLang.GetString(57, "Release Date")
        Me.lblMovieScraperGlobalReleaseDate.Text = strReleaseDate

        'Premiered
        Dim strPremiered As String = Master.eLang.GetString(724, "Premiered")
        Me.lblTVScraperGlobalPremiered.Text = strPremiered

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

        'Season
        Dim strSeason As String = Master.eLang.GetString(650, "Season")
        Me.lblTVSourcesFileNamingBoxeeDefaultsHeaderBoxeeSeason.Text = strSeason
        Me.lblTVSourcesFileNamingNMTDefaultsHeaderNMJSeason.Text = strSeason
        Me.lblTVSourcesFileNamingNMTDefaultsHeaderYAMJSeason.Text = strSeason
        Me.lblTVSourcesFileNamingXBMCDefaultsHeaderFrodoHelixSeason.Text = strSeason
        Me.tpTVImagesSeason.Text = strSeason
        Me.tpTVSourcesFileNamingExpertSeason.Text = strSeason

        'Season #
        Dim strSeasonNR As String = Master.eLang.GetString(659, "Season #")

        'Season Landscape
        Dim strSeasonLandscape As String = Master.eLang.GetString(1018, "Season Landscape")
        Me.lblTVSourcesFileNamingXBMCADSeasonLandscape.Text = strSeasonLandscape
        Me.lblTVSourcesFileNamingKodiExtendedSeasonLandscape.Text = strSeasonLandscape

        'Season List Sorting
        Dim strSeasonListSorting As String = Master.eLang.GetString(493, "Season List Sorting")
        Me.gbTVGeneralSeasonListSortingOpts.Text = strSeasonListSorting

        'Seasons
        Dim strSeasons As String = Master.eLang.GetString(681, "Seasons")
        Me.lblTVScraperGlobalHeaderSeasons.Text = strSeasons

        'Show List Sorting
        Dim strShowListSorting As String = Master.eLang.GetString(492, "Show List Sorting")
        Me.gbTVGeneralShowListSortingOpts.Text = strShowListSorting

        'Shows
        Dim strShows As String = Master.eLang.GetString(680, "Shows")
        Me.lblTVScraperGlobalHeaderShows.Text = strShows

        'Single Video
        Dim strSingleVideo As String = Master.eLang.GetString(413, "Single Video")
        Me.colMovieSourcesSingle.Text = strSingleVideo

        'Sort Tokens to Ignore
        Dim strSortTokens As String = Master.eLang.GetString(463, "Sort Tokens to Ignore")
        Me.gbMovieGeneralMediaListSortTokensOpts.Text = strSortTokens
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Text = strSortTokens
        Me.gbTVGeneralMediaListSortTokensOpts.Text = strSortTokens

        'Sorting
        Dim strSorting As String = Master.eLang.GetString(895, "Sorting")
        Me.colTVSourcesSorting.Text = strSorting

        'Status
        Dim strStatus As String = Master.eLang.GetString(215, "Status")
        Me.lblTVScraperGlobalStatus.Text = strStatus

        'Store themes in movie directory
        Dim strStoreThemeInMovieDirectory As String = Master.eLang.GetString(1258, "Store themes in movie directory")
        Me.chkMovieThemeTvTunesMoviePath.Text = strStoreThemeInMovieDirectory

        'Store themes in custom path
        Dim strStoreThemeInCustomPath As String = Master.eLang.GetString(1259, "Store themes in a custom path")
        Me.chkMovieThemeTvTunesCustom.Text = strStoreThemeInCustomPath
        Me.chkTVShowThemeTvTunesCustom.Text = strStoreThemeInCustomPath

        'Store themes in sub directories
        Dim strStoreThemeInSubDirectory As String = Master.eLang.GetString(1260, "Store themes in sub directories")
        Me.chkMovieThemeTvTunesSub.Text = strStoreThemeInSubDirectory
        Me.chkTVShowThemeTvTunesSub.Text = strStoreThemeInSubDirectory

        'Store themes in tv show directory
        Dim strStoreThemeInShowDirectory As String = Master.eLang.GetString(1265, "Store themes in tv show directory")
        Me.chkTVShowThemeTvTunesShowPath.Text = strStoreThemeInShowDirectory

        'Studios
        Dim strStudio As String = Master.eLang.GetString(226, "Studios")
        Me.lblMovieScraperGlobalStudios.Text = strStudio
        Me.lblTVScraperGlobalStudios.Text = strStudio

        'Subtitles
        Dim strSubtitles As String = Master.eLang.GetString(152, "Subtitles")

        'Tagline
        Dim strTagline As String = Master.eLang.GetString(397, "Tagline")
        Me.lblMovieScraperGlobalTagline.Text = strTagline

        'Theme
        Dim strTheme As String = Master.eLang.GetString(1118, "Theme")

        'Themes
        Dim strThemes As String = Master.eLang.GetString(1285, "Themes")
        Me.gbMovieThemeOpts.Text = strThemes

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

        'TV Show
        Dim strTVShow As String = Master.eLang.GetString(700, "TV Show")
        Me.lblTVSourcesFileNamingBoxeeDefaultsHeaderBoxeeTVShow.Text = strTVShow
        Me.lblTVSourcesFileNamingNMTDefaultsHeaderNMJTVShow.Text = strTVShow
        Me.lblTVSourcesFileNamingNMTDefaultsHeaderYAMJTVShow.Text = strTVShow
        Me.lblTVSourcesFileNamingXBMCDefaultsHeaderFrodoHelixTVShow.Text = strTVShow
        Me.tpTVImagesTVShow.Text = strTVShow
        Me.tpTVSourcesFileNamingExpertTVShow.Text = strTVShow

        'TV Show Landscape
        Dim strTVShowLandscape As String = Master.eLang.GetString(1010, "TV Show Landscape")
        Me.lblTVSourcesFileNamingXBMCADTVShowLandscape.Text = strTVShowLandscape
        Me.lblTVSourcesFileNamingKodiExtendedTVShowLandscape.Text = strTVShowLandscape

        'Use
        Dim strUse As String = Master.eLang.GetString(872, "Use")

        'Use Certification for MPAA
        Dim strUseCertForMPAA As String = Master.eLang.GetString(511, "Use Certification for MPAA")
        Me.chkMovieScraperCertForMPAA.Text = strUseCertForMPAA
        Me.chkTVScraperShowCertForMPAA.Text = strUseCertForMPAA

        'Use MPAA as Fallback for FSK Rating
        Dim strUseMPAAAsFallbackForFSK As String = Master.eLang.GetString(882, "Use MPAA as Fallback for FSK Rating")
        Me.chkMovieScraperCertFSK.Text = strUseMPAAAsFallbackForFSK
        Me.chkTVScraperShowCertFSK.Text = strUseMPAAAsFallbackForFSK

        'Watched
        Dim strWatched As String = Master.eLang.GetString(981, "Watched")

        'Use Folder Name
        Dim strUseFolderName As String = Master.eLang.GetString(412, "Use Folder Name")
        Me.colMovieSourcesFolder.Text = strUseFolderName

        'Writers
        Dim strWriters As String = Master.eLang.GetString(394, "Writers")
        Me.lblMovieScraperGlobalCredits.Text = strWriters
        Me.lblTVScraperGlobalCredits.Text = strWriters

        'Year
        Dim strYear As String = Master.eLang.GetString(278, "Year")
        Me.lblMovieScraperGlobalYear.Text = strYear

        Me.Text = Master.eLang.GetString(420, "Settings")
        Me.btnApply.Text = Master.eLang.GetString(276, "Apply")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.btnGeneralDigitGrpSymbolSettings.Text = Master.eLang.GetString(420, "Settings")
        Me.btnMovieSetScraperTitleRenamerAdd.Text = Master.eLang.GetString(28, "Add")
        Me.btnMovieSetScraperTitleRenamerRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.btnMovieSourceAdd.Text = Master.eLang.GetString(407, "Add Source")
        Me.btnMovieSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        Me.btnMovieSourceRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnRemTVSource.Text = Master.eLang.GetString(30, "Remove")
        Me.btnTVGeneralLangFetch.Text = Master.eLang.GetString(742, "Fetch Available Languages")
        Me.btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        Me.btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.btnTVSourcesRegexTVShowMatchingClear.Text = Master.eLang.GetString(123, "Clear")
        Me.btnTVSourcesRegexTVShowMatchingEdit.Text = Master.eLang.GetString(690, "Edit Regex")
        Me.btnTVSourcesRegexTVShowMatchingRemove.Text = Master.eLang.GetString(30, "Remove")
        Me.btnTVSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        Me.chkFileSystemCleanerWhitelist.Text = Master.eLang.GetString(440, "Whitelist Video Extensions")
        Me.chkGeneralCheckUpdates.Text = Master.eLang.GetString(432, "Check for Updates")
        Me.chkGeneralDateAddedIgnoreNFO.Text = Master.eLang.GetString(1209, "Ignore <dateadded> from NFO")
        Me.chkGeneralDigitGrpSymbolVotes.Text = Master.eLang.GetString(1387, "Use digit grouping symbol for Votes count")
        Me.chkGeneralDoubleClickScrape.Text = Master.eLang.GetString(1198, "Enable Image Scrape On Double Right Click")
        Me.chkGeneralDisplayBanner.Text = Master.eLang.GetString(1146, "Display Banner")
        Me.chkGeneralDisplayCharacterArt.Text = Master.eLang.GetString(1147, "Display CharacterArt")
        Me.chkGeneralDisplayClearArt.Text = Master.eLang.GetString(1148, "Display ClearArt")
        Me.chkGeneralDisplayClearLogo.Text = Master.eLang.GetString(1149, "Display ClearLogo")
        Me.chkGeneralDisplayDiscArt.Text = Master.eLang.GetString(1150, "Display DiscArt")
        Me.chkGeneralDisplayFanart.Text = Master.eLang.GetString(455, "Display Fanart")
        Me.chkGeneralDisplayFanartSmall.Text = Master.eLang.GetString(967, "Display Small Fanart")
        Me.chkGeneralDisplayLandscape.Text = Master.eLang.GetString(1151, "Display Landscape")
        Me.chkGeneralDisplayPoster.Text = Master.eLang.GetString(456, "Display Poster")
        Me.chkGeneralImagesGlassOverlay.Text = Master.eLang.GetString(966, "Enable Images Glass Overlay")
        Me.chkGeneralOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        Me.chkGeneralDisplayGenresText.Text = Master.eLang.GetString(453, "Always Display Genre Text")
        Me.chkGeneralDisplayLangFlags.Text = Master.eLang.GetString(489, "Display Language Flags")
        Me.chkGeneralDisplayImgDims.Text = Master.eLang.GetString(457, "Display Image Dimensions")
        Me.chkGeneralDisplayImgNames.Text = Master.eLang.GetString(1255, "Display Image Names")
        Me.chkGeneralSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        Me.chkMovieSourcesBackdropsAuto.Text = Master.eLang.GetString(521, "Automatically Save Fanart To Backdrops Folder")
        Me.chkMovieCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        Me.chkMovieDisplayYear.Text = Master.eLang.GetString(464, "Display Year in List Title")
        Me.chkMovieGeneralIgnoreLastScan.Text = Master.eLang.GetString(669, "Ignore last scan time when updating library")
        Me.chkMovieGeneralMarkNew.Text = Master.eLang.GetString(459, "Mark New Movies")
        Me.chkMovieLevTolerance.Text = Master.eLang.GetString(462, "Check Title Match Confidence")
        Me.chkMovieProperCase.Text = Master.eLang.GetString(452, "Convert Names to Proper Case")
        Me.chkMovieRecognizeVTSExpertVTS.Text = String.Format(Master.eLang.GetString(537, "Recognize VIDEO_TS{0}without VIDEO_TS folder"), Environment.NewLine)
        Me.chkMovieScanOrderModify.Text = Master.eLang.GetString(796, "Scan in order of last write time")
        Me.chkMovieSetCleanFiles.Text = Master.eLang.GetString(1276, "Remove Images and NFOs with MovieSets")
        Me.chkMovieSetGeneralMarkNew.Text = Master.eLang.GetString(1301, "Mark New MovieSets")
        Me.chkMovieScraperCastWithImg.Text = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        Me.chkMovieScraperCleanPlotOutline.Text = Master.eLang.GetString(985, "Clean Plot/Outline")
        Me.chkMovieScraperCollectionsAuto.Text = Master.eLang.GetString(1266, "Add Movie automatically to Collections")
        Me.chkMovieScraperDetailView.Text = Master.eLang.GetString(1249, "Show scraped results in detailed view")
        Me.chkMovieScraperReleaseFormat.Text = Master.eLang.GetString(1272, "Date format Releasedate: yyyy-mm-dd")
        Me.chkMovieScraperMetaDataIFOScan.Text = Master.eLang.GetString(628, "Enable IFO Parsing")
        Me.chkMovieScraperMetaDataScan.Text = Master.eLang.GetString(517, "Scan Meta Data")
        Me.chkMovieScraperPlotForOutline.Text = Master.eLang.GetString(965, "Use Plot for Plot Outline")
        Me.chkMovieScraperPlotForOutlineIfEmpty.Text = Master.eLang.GetString(958, "Only if Plot Outline is empty")
        Me.chkMovieScraperStudioWithImg.Text = Master.eLang.GetString(1280, "Scrape Only Studios With Images")
        Me.chkMovieScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.chkMovieScraperXBMCTrailerFormat.Text = Master.eLang.GetString(1187, "Save YouTube-Trailer-Links in XBMC compatible format")
        Me.chkMovieYAMJCompatibleSets.Text = Master.eLang.GetString(561, "YAMJ Compatible Sets")
        Me.chkMovieSkipStackedSizeCheck.Text = Master.eLang.GetString(538, "Skip Size Check of Stacked Files")
        Me.chkMovieSortBeforeScan.Text = Master.eLang.GetString(712, "Sort files into folder before each library update")
        Me.chkMovieStackExpertMulti.Text = String.Format(Master.eLang.GetString(1178, "Stack {0}filename{1}"), "<", ">")
        Me.chkMovieUnstackExpertMulti.Text = Master.eLang.GetString(1179, "also save unstacked")
        Me.chkMovieUseBaseDirectoryExpertBDMV.Text = Master.eLang.GetString(1180, "Use Base Directory")
        Me.chkMovieXBMCProtectVTSBDMV.Text = Master.eLang.GetString(1176, "Protect DVD/Bluray Structure")
        Me.chkMovieYAMJWatchedFile.Text = Master.eLang.GetString(1177, "Use .watched Files")
        Me.chkProxyCredsEnable.Text = Master.eLang.GetString(677, "Enable Credentials")
        Me.chkProxyEnable.Text = Master.eLang.GetString(673, "Enable Proxy")
        Me.chkTVDisplayMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        Me.chkTVDisplayStatus.Text = Master.eLang.GetString(126, "Display Status in List Title")
        Me.chkTVEpisodeNoFilter.Text = Master.eLang.GetString(734, "Build Episode Title Instead of Filtering")
        Me.chkTVGeneralMarkNewEpisodes.Text = Master.eLang.GetString(621, "Mark New Episodes")
        Me.chkTVGeneralMarkNewShows.Text = Master.eLang.GetString(549, "Mark New Shows")
        Me.chkTVScraperEpisodeGuestStarsToActors.Text = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        Me.chkTVScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        Me.chkTVScraperUseSRuntimeForEp.Text = Master.eLang.GetString(1262, "Use Show Runtime for Episodes if no Episode Runtime can be found")
        Me.dgvMovieSetScraperTitleRenamer.Columns(0).HeaderText = Master.eLang.GetString(1277, "From")
        Me.dgvMovieSetScraperTitleRenamer.Columns(1).HeaderText = Master.eLang.GetString(1278, "To")
        Me.gbFileSystemExcludedDirs.Text = Master.eLang.GetString(1273, "Excluded Directories")
        Me.gbFileSystemCleanFiles.Text = Master.eLang.GetString(437, "Clean Files")
        Me.gbFileSystemNoStackExts.Text = Master.eLang.GetString(530, "No Stack Extensions")
        Me.gbFileSystemValidVideoExts.Text = Master.eLang.GetString(534, "Valid Video Extensions")
        Me.gbFileSystemValidSubtitlesExts.Text = Master.eLang.GetString(1284, "Valid Subtitles Extensions")
        Me.gbFileSystemValidThemeExts.Text = Master.eLang.GetString(1081, "Valid Theme Extensions")
        Me.gbGeneralDaemon.Text = Master.eLang.GetString(1261, "Configuration ISO Filescanning")
        Me.gbGeneralDateAdded.Text = Master.eLang.GetString(792, "Adding Date")
        Me.gbGeneralInterface.Text = Master.eLang.GetString(795, "Interface")
        Me.gbGeneralThemes.Text = Master.eLang.GetString(629, "GUI Themes")
        Me.gbMovieGeneralCustomMarker.Text = Master.eLang.GetString(1190, "Custom Marker")
        Me.gbMovieSourcesBackdropsFolderOpts.Text = Master.eLang.GetString(520, "Backdrops Folder")
        Me.gbMovieImagesFanartOpts.Text = Master.eLang.GetString(149, "Fanart")
        Me.gbMovieGeneralFiltersOpts.Text = Master.eLang.GetString(451, "Folder/File Name Filters")
        Me.gbMovieGeneralGenreFilterOpts.Text = Master.eLang.GetString(454, "Genre Language Filter")
        Me.gbMovieGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        Me.gbMovieScraperDefFIExtOpts.Text = Master.eLang.GetString(625, "Defaults by File Type")
        Me.gbMovieSetScraperTitleRenamerOpts.Text = Master.eLang.GetString(1279, "Title Renamer")
        Me.gbProxyCredsOpts.Text = Master.eLang.GetString(676, "Credentials")
        Me.gbProxyOpts.Text = Master.eLang.GetString(672, "Proxy")
        Me.gbSettingsHelp.Text = String.Concat("     ", Master.eLang.GetString(458, "Help"))
        Me.gbTVEpisodeFilterOpts.Text = Master.eLang.GetString(671, "Episode Folder/File Name Filters")
        Me.gbTVGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        Me.gbTVScraperDurationFormatOpts.Text = Master.eLang.GetString(515, "Duration Format")
        Me.gbTVScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields")
        Me.gbTVShowFilterOpts.Text = Master.eLang.GetString(670, "Show Folder/File Name Filters")
        Me.gbTVSourcesRegexTVShowMatching.Text = Master.eLang.GetString(691, "Show Match Regex")
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
        Me.lblMovieLevTolerance.Text = Master.eLang.GetString(461, "Mismatch Tolerance:")
        Me.lblMovieScraperDurationRuntimeFormat.Text = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
        Me.lblMovieScraperMPAANotRated.Text = String.Concat(Master.eLang.GetString(832, "MPAA value if no rating is available"), ":")
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
        Me.lblTVScraperGlobalGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        Me.lblTVSourcesRegexTVShowMatchingByDate.Text = Master.eLang.GetString(698, "by Date")
        Me.lblTVSourcesRegexTVShowMatchingRegex.Text = Master.eLang.GetString(699, "Regex")
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Text = Master.eLang.GetString(695, "Default Season")
        Me.tpFileSystemCleanerExpert.Text = Master.eLang.GetString(439, "Expert")
        Me.tpFileSystemCleanerStandard.Text = Master.eLang.GetString(438, "Standard")
        Me.tpMovieSetFileNamingExpertParent.Text = Master.eLang.GetString(880, "Parent Folder")
        Me.tpMovieSetFileNamingExpertSingle.Text = Master.eLang.GetString(879, "Single Folder")
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
        Me.gbMovieSetGeneralMediaListOpts.Text = Me.gbMovieGeneralMediaListOpts.Text
        Me.gbTVScraperDefFIExtOpts.Text = Me.gbTVScraperDefFIExtOpts.Text
        Me.lblSettingsTopTitle.Text = Me.Text
        Me.lblTVSkipLessThan.Text = Me.lblMovieSkipLessThan.Text
        Me.lblTVSkipLessThanMB.Text = Me.lblMovieSkipLessThanMB.Text

        Me.LoadGeneralDateTime()
        Me.LoadMovieBannerSizes()
        Me.LoadMovieFanartSizes()
        Me.LoadMoviePosterSizes()
        Me.LoadMovieTrailerQualities()
        Me.LoadTVBannerSizes()
        Me.LoadTVBannerTypes()
        Me.LoadTVFanartSizes()
        Me.LoadTVPosterSizes()
        Me.LoadTVScraperOptionsOrdering()
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

    Private Sub txtTVAllSeasonsBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVAllSeasonsBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVAllSeasonsBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVAllSeasonsBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVAllSeasonsPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVAllSeasonsPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVAllSeasonsPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVAllSeasonsPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVAllSeasonsFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVAllSeasonsFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVAllSeasonsFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVAllSeasonsFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
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

    Private Sub txtTVEpisodeFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVEpisodeFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVEpisodePosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVEpisodePosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVEpisodePosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVEpisodePosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowExtrafanartsHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowExtrafanartsHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowExtrafanartsLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowExtrafanartsLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowExtrafanartsWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowExtrafanartsWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieExtrafanartsHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieExtrafanartsHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieExtrafanartsLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieExtrafanartsLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieExtrafanartsWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieExtrafanartsWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieExtrathumbsHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieExtrathumbsHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieExtrathumbsLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieExtrathumbsLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieExtrathumbsWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieExtrathumbsWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieScraperGenreLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieScraperGenreLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub
    Private Sub txtMovieScraperOutlineLimit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieScraperOutlineLimit.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMoviePosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoviePosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMoviePosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoviePosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSetPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSetPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtProxyPort_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProxyPort.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingRegex_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTVSourcesRegexTVShowMatchingRegex.TextChanged
        Me.ValidateTVShowMatching()
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingDefaultSeason_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTVSourcesRegexTVShowMatchingDefaultSeason.TextChanged
        Me.ValidateTVShowMatching()
    End Sub

    Private Sub txtTVShowBannerHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowBannerHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowBannerWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowBannerWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowFanartHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowFanartHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowFanartWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowFanartWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowPosterHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowPosterHeight.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVShowPosterWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVShowPosterWidth.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSkipLessThan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMovieSkipLessThan.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieSkipLessThan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieSkipLessThan.TextChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsDBClean_Movie = True
        Me.sResult.NeedsDBUpdate_Movie = True
    End Sub

    Private Sub txtTVSkipLessThan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTVSkipLessThan.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtTVSkipLessThan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVSkipLessThan.TextChanged
        Me.SetApplyButton(True)
        Me.sResult.NeedsDBClean_TV = True
        Me.sResult.NeedsDBUpdate_TV = True
    End Sub

    Private Sub txtTVScraperDefFIExt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVScraperDefFIExt.TextChanged
        btnTVScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtTVScraperDefFIExt.Text) AndAlso Not Me.lstTVScraperDefFIExt.Items.Contains(If(txtTVScraperDefFIExt.Text.StartsWith("."), txtTVScraperDefFIExt.Text, String.Concat(".", txtTVScraperDefFIExt.Text)))
        If btnTVScraperDefFIExtAdd.Enabled Then
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub ValidateTVShowMatching()
        If Not String.IsNullOrEmpty(Me.txtTVSourcesRegexTVShowMatchingRegex.Text) AndAlso (String.IsNullOrEmpty(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text) OrElse Integer.TryParse(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0)) Then
            Me.btnTVSourcesRegexTVShowMatchingAdd.Enabled = True
        Else
            Me.btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
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
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieSetPathExtended.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
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

    Private Sub btnMovieThemeTvTunesCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieThemeTvTunesCustomPathBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieThemeTvTunesCustomPath.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnTVShowThemeTvTunesCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnTVShowThemeTvTunesCustomPathBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtTVShowThemeTvTunesCustomPath.Text = .SelectedPath.ToString
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

    Private Sub btnGeneralDaemonPathBrowse_Click(sender As Object, e As EventArgs) Handles btnGeneralDaemonPathBrowse.Click
        Try
            With Me.fileBrowse
                .Filter = "Exe (*.exe*)|*.exe*|Exe (*.exe*)|*.exe*"
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.FileName) Then
                        Me.txtGeneralDaemonPath.Text = .FileName
                        Me.SetApplyButton(True)
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnGeneralDigitGrpSymbolSettings_Click(sender As Object, e As EventArgs) Handles btnGeneralDigitGrpSymbolSettings.Click
        Try
            Process.Start("INTL.CPL")
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

    Private Sub chkMovieStackExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertSingle.CheckedChanged
        Me.chkMovieUnstackExpertSingle.Enabled = Me.chkMovieStackExpertSingle.Checked AndAlso Me.chkMovieStackExpertSingle.Enabled
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieStackExpertMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertMulti.CheckedChanged
        Me.chkMovieUnstackExpertMulti.Enabled = Me.chkMovieStackExpertMulti.Checked AndAlso Me.chkMovieStackExpertMulti.Enabled
        Me.SetApplyButton(True)
    End Sub

    Private Sub chkMovieSetUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetUseExpert.CheckedChanged
        Me.SetApplyButton(True)

        Me.btnMovieSetPathExpertSingleBrowse.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetBannerExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetBannerExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetClearArtExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearArtExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetClearLogoExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearLogoExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetDiscArtExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetDiscArtExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetFanartExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetFanartExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetLandscapeExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetLandscapeExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetNFOExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetNFOExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPathExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        'Me.txtMovieSetPosterExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPosterExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
    End Sub

    Private Sub chkTVUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseBoxee.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodeNFOBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVEpisodePosterBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVSeasonPosterBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowBannerBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowFanartBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowNFOBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowPosterBoxee.Enabled = Me.chkTVUseBoxee.Checked

        If Not Me.chkTVUseBoxee.Checked Then
            Me.chkTVEpisodeNFOBoxee.Checked = False
            Me.chkTVEpisodePosterBoxee.Checked = False
            Me.chkTVSeasonPosterBoxee.Checked = False
            Me.chkTVShowBannerBoxee.Checked = False
            Me.chkTVShowFanartBoxee.Checked = False
            Me.chkTVShowNFOBoxee.Checked = False
            Me.chkTVShowPosterBoxee.Checked = False
        Else
            Me.chkTVEpisodeNFOBoxee.Checked = True
            Me.chkTVEpisodePosterBoxee.Checked = True
            Me.chkTVSeasonPosterBoxee.Checked = True
            Me.chkTVShowBannerBoxee.Checked = True
            Me.chkTVShowFanartBoxee.Checked = True
            Me.chkTVShowNFOBoxee.Checked = True
            Me.chkTVShowPosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseFrodo.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodeActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVEpisodeNFOFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVEpisodePosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonLandscapeAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonLandscapeExtended.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowCharacterArtAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowCharacterArtExtended.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearArtAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearArtExtended.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearLogoAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearLogoExtended.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowExtrafanartsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowLandscapeAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowLandscapeExtended.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowNFOFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowThemeTvTunesEnabled.Enabled = Me.chkTVUseFrodo.Checked

        If Not Me.chkTVUseFrodo.Checked Then
            Me.chkTVEpisodeActorThumbsFrodo.Checked = False
            Me.chkTVEpisodeNFOFrodo.Checked = False
            Me.chkTVEpisodePosterFrodo.Checked = False
            Me.chkTVSeasonBannerFrodo.Checked = False
            Me.chkTVSeasonFanartFrodo.Checked = False
            Me.chkTVSeasonLandscapeAD.Checked = False
            Me.chkTVSeasonLandscapeExtended.Checked = False
            Me.chkTVSeasonPosterFrodo.Checked = False
            Me.chkTVShowActorThumbsFrodo.Checked = False
            Me.chkTVShowBannerFrodo.Checked = False
            Me.chkTVShowCharacterArtAD.Checked = False
            Me.chkTVShowCharacterArtExtended.Checked = False
            Me.chkTVShowClearArtAD.Checked = False
            Me.chkTVShowClearArtExtended.Checked = False
            Me.chkTVShowClearLogoAD.Checked = False
            Me.chkTVShowClearLogoExtended.Checked = False
            Me.chkTVShowExtrafanartsFrodo.Checked = False
            Me.chkTVShowFanartFrodo.Checked = False
            Me.chkTVShowLandscapeAD.Checked = False
            Me.chkTVShowLandscapeExtended.Checked = False
            Me.chkTVShowNFOFrodo.Checked = False
            Me.chkTVShowPosterFrodo.Checked = False
            Me.chkTVShowThemeTvTunesEnabled.Checked = False
        Else
            Me.chkTVEpisodeActorThumbsFrodo.Checked = True
            Me.chkTVEpisodeNFOFrodo.Checked = True
            Me.chkTVEpisodePosterFrodo.Checked = True
            Me.chkTVSeasonBannerFrodo.Checked = True
            Me.chkTVSeasonFanartFrodo.Checked = True
            Me.chkTVSeasonLandscapeExtended.Checked = True
            Me.chkTVSeasonPosterFrodo.Checked = True
            Me.chkTVShowActorThumbsFrodo.Checked = True
            Me.chkTVShowBannerFrodo.Checked = True
            Me.chkTVShowCharacterArtExtended.Checked = True
            Me.chkTVShowClearArtExtended.Checked = True
            Me.chkTVShowClearLogoExtended.Checked = True
            Me.chkTVShowExtrafanartsFrodo.Checked = True
            Me.chkTVShowFanartFrodo.Checked = True
            Me.chkTVShowLandscapeExtended.Checked = True
            Me.chkTVShowNFOFrodo.Checked = True
            Me.chkTVShowPosterFrodo.Checked = True
        End If
    End Sub

    Private Sub chkTVUseYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseYAMJ.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodeNFOYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVEpisodePosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonBannerYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonFanartYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonPosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowBannerYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowFanartYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowNFOYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowPosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked

        If Not Me.chkTVUseYAMJ.Checked Then
            Me.chkTVEpisodeNFOYAMJ.Checked = False
            Me.chkTVEpisodePosterYAMJ.Checked = False
            Me.chkTVSeasonBannerYAMJ.Checked = False
            Me.chkTVSeasonFanartYAMJ.Checked = False
            Me.chkTVSeasonPosterYAMJ.Checked = False
            Me.chkTVShowBannerYAMJ.Checked = False
            Me.chkTVShowFanartYAMJ.Checked = False
            Me.chkTVShowNFOYAMJ.Checked = False
            Me.chkTVShowPosterYAMJ.Checked = False
        Else
            Me.chkTVEpisodeNFOYAMJ.Checked = True
            Me.chkTVEpisodePosterYAMJ.Checked = True
            Me.chkTVSeasonBannerYAMJ.Checked = True
            Me.chkTVSeasonFanartYAMJ.Checked = True
            Me.chkTVSeasonPosterYAMJ.Checked = True
            Me.chkTVShowBannerYAMJ.Checked = True
            Me.chkTVShowFanartYAMJ.Checked = True
            Me.chkTVShowNFOYAMJ.Checked = True
            Me.chkTVShowPosterYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkTVUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseExpert.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVEpisodeActorThumbsExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.chkTVShowActorThumbsExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.chkTVShowExtrafanartsExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVAllSeasonsBannerExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVAllSeasonsFanartExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVAllSeasonsLandscapeExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVAllSeasonsPosterExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVEpisodeActorThumbsExtExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVEpisodeFanartExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVEpisodeNFOExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVEpisodePosterExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVSeasonBannerExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVSeasonFanartExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVSeasonLandscapeExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVSeasonPosterExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowActorThumbsExtExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowBannerExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowCharacterArtExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowClearArtExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowClearLogoExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowFanartExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowLandscapeExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowNFOExpert.Enabled = Me.chkTVUseExpert.Checked
        Me.txtTVShowPosterExpert.Enabled = Me.chkTVUseExpert.Checked
    End Sub

    Private Sub chkMovieSetClickScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetClickScrape.CheckedChanged
        chkMovieSetClickScrapeAsk.Enabled = chkMovieSetClickScrape.Checked
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
            Process.Start("http://kodi.wiki/view/Add-on:Artwork_Downloader")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://kodi.wiki/view/Add-on:Artwork_Downloader"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_Click(sender As Object, e As EventArgs) Handles pbMovieSourcesTvTunesInfo.Click
        If Master.isWindows Then
            Process.Start("http://kodi.wiki/view/Add-on:TvTunes")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://kodi.wiki/view/Add-on:TvTunes"
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

    Private Sub pbMovieSourcesTvTunesInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbMovieSourcesTvTunesInfo.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbMovieSourcesTvTunesInfo.MouseLeave
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

    Private Sub pbTVSourcesTvTunesInfo_Click(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.Click
        If Master.isWindows Then
            Process.Start("http://kodi.wiki/view/Add-on:TvTunes")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://kodi.wiki/view/Add-on:TvTunes"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.MouseLeave
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

    Private Sub chkMovieImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieImagesMediaLanguageOnly.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieImagesGetBlankImages.Enabled = Me.chkMovieImagesMediaLanguageOnly.Checked
        Me.chkMovieImagesGetEnglishImages.Enabled = Me.chkMovieImagesMediaLanguageOnly.Checked

        If Not Me.chkMovieImagesMediaLanguageOnly.Checked Then
            Me.chkMovieImagesGetBlankImages.Checked = False
            Me.chkMovieImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub chkMovieSetImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieSetImagesMediaLanguageOnly.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkMovieSetImagesGetBlankImages.Enabled = Me.chkMovieSetImagesMediaLanguageOnly.Checked
        Me.chkMovieSetImagesGetEnglishImages.Enabled = Me.chkMovieSetImagesMediaLanguageOnly.Checked

        If Not Me.chkMovieSetImagesMediaLanguageOnly.Checked Then
            Me.chkMovieSetImagesGetBlankImages.Checked = False
            Me.chkMovieSetImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub chkTVImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVImagesMediaLanguageOnly.CheckedChanged
        Me.SetApplyButton(True)

        Me.chkTVImagesGetBlankImages.Enabled = Me.chkTVImagesMediaLanguageOnly.Checked
        Me.chkTVImagesGetEnglishImages.Enabled = Me.chkTVImagesMediaLanguageOnly.Checked

        If Not Me.chkTVImagesMediaLanguageOnly.Checked Then
            Me.chkTVImagesGetBlankImages.Checked = False
            Me.chkTVImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub EnableApplyButton(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        cbGeneralDaemonDrive.SelectedIndexChanged, _
        cbGeneralDateTime.SelectedIndexChanged, _
        cbGeneralMovieSetTheme.SelectedIndexChanged, _
        cbGeneralMovieTheme.SelectedIndexChanged, _
        cbGeneralTVEpisodeTheme.SelectedIndexChanged, _
        cbGeneralTVShowTheme.SelectedIndexChanged, _
        cbMovieBannerPrefSize.SelectedIndexChanged, _
        cbMovieExtrafanartsPrefSize.SelectedIndexChanged, _
        cbMovieExtrathumbsPrefSize.SelectedIndexChanged, _
        cbMovieFanartPrefSize.SelectedIndexChanged, _
        cbMovieGeneralLang.SelectedIndexChanged, _
        cbMovieLanguageOverlay.SelectedIndexChanged, _
        cbMoviePosterPrefSize.SelectedIndexChanged, _
        cbMovieScraperCertLang.SelectedIndexChanged, _
        cbMovieSetBannerPrefSize.SelectedIndexChanged, _
        cbMovieSetFanartPrefSize.SelectedIndexChanged, _
        cbMovieSetPosterPrefSize.SelectedIndexChanged, _
        cbMovieTrailerMinVideoQual.SelectedIndexChanged, _
        cbTVAllSeasonsBannerPrefType.SelectedIndexChanged, _
        cbTVAllSeasonsFanartPrefSize.SelectedIndexChanged, _
        cbTVAllSeasonsPosterPrefSize.SelectedIndexChanged, _
        cbTVEpisodeFanartPrefSize.SelectedIndexChanged, _
        cbTVEpisodePosterPrefSize.SelectedIndexChanged, _
        cbTVGeneralLang.SelectedIndexChanged, _
        cbTVLanguageOverlay.SelectedIndexChanged, _
        cbTVScraperOptionsOrdering.SelectedIndexChanged, _
        cbTVSeasonBannerPrefType.SelectedIndexChanged, _
        cbTVSeasonFanartPrefSize.SelectedIndexChanged, _
        cbTVSeasonPosterPrefSize.SelectedIndexChanged, _
        cbTVShowBannerPrefType.SelectedIndexChanged, _
        cbTVShowExtrafanartsPrefSize.SelectedIndexChanged, _
        cbTVShowFanartPrefSize.SelectedIndexChanged, _
        cbTVShowPosterPrefSize.SelectedIndexChanged, _
        chkCleanDotFanartJPG.CheckedChanged, _
        chkCleanExtrathumbs.CheckedChanged, _
        chkCleanFanartJPG.CheckedChanged, _
        chkCleanFolderJPG.CheckedChanged, _
        chkCleanMovieFanartJPG.CheckedChanged, _
        chkCleanMovieJPG.CheckedChanged, _
        chkCleanMovieNFO.CheckedChanged, _
        chkCleanMovieNFOb.CheckedChanged, _
        chkCleanMovieNameJPG.CheckedChanged, _
        chkCleanMovieTBN.CheckedChanged, _
        chkCleanMovieTBNb.CheckedChanged, _
        chkCleanPosterJPG.CheckedChanged, _
        chkCleanPosterTBN.CheckedChanged, _
        chkFileSystemCleanerWhitelist.CheckedChanged, _
        chkGeneralCheckUpdates.CheckedChanged, _
        chkGeneralDateAddedIgnoreNFO.CheckedChanged, _
        chkGeneralDigitGrpSymbolVotes.CheckedChanged, _
        chkGeneralDoubleClickScrape.CheckedChanged, _
        chkGeneralImagesGlassOverlay.CheckedChanged, _
        chkGeneralOverwriteNfo.CheckedChanged, _
        chkGeneralDisplayGenresText.CheckedChanged, _
        chkGeneralDisplayImgDims.CheckedChanged, _
        chkGeneralDisplayImgNames.CheckedChanged, _
        chkGeneralDisplayLangFlags.CheckedChanged, _
        chkGeneralSourceFromFolder.CheckedChanged, _
        chkMovieActorThumbsExpertBDMV.CheckedChanged, _
        chkMovieActorThumbsExpertMulti.CheckedChanged, _
        chkMovieActorThumbsExpertSingle.CheckedChanged, _
        chkMovieActorThumbsExpertVTS.CheckedChanged, _
        chkMovieActorThumbsKeepExisting.CheckedChanged, _
        chkMovieBannerKeepExisting.CheckedChanged, _
        chkMovieBannerPrefOnly.CheckedChanged, _
        chkMovieCleanDB.CheckedChanged, _
        chkMovieClearArtKeepExisting.CheckedChanged, _
        chkMovieClearLogoKeepExisting.CheckedChanged, _
        chkMovieClickScrapeAsk.CheckedChanged, _
        chkMovieDiscArtKeepExisting.CheckedChanged, _
        chkMovieExtrafanartsKeepExisting.CheckedChanged, _
        chkMovieExtrafanartsPrefOnly.CheckedChanged, _
        chkMovieExtrathumbsKeepExisting.CheckedChanged, _
        chkMovieExtrathumbsPrefOnly.CheckedChanged, _
        chkMovieExtrafanartsExpertBDMV.CheckedChanged, _
        chkMovieExtrafanartsExpertSingle.CheckedChanged, _
        chkMovieExtrafanartsExpertVTS.CheckedChanged, _
        chkMovieExtrathumbsExpertBDMV.CheckedChanged, _
        chkMovieExtrathumbsExpertSingle.CheckedChanged, _
        chkMovieExtrathumbsExpertVTS.CheckedChanged, _
        chkMovieFanartKeepExisting.CheckedChanged, _
        chkMovieFanartPrefOnly.CheckedChanged, _
        chkMovieGeneralIgnoreLastScan.CheckedChanged, _
        chkMovieGeneralMarkNew.CheckedChanged, _
        chkMovieImagesCacheEnabled.CheckedChanged, _
        chkMovieImagesDisplayImageSelect.CheckedChanged, _
        chkMovieImagesGetBlankImages.CheckedChanged, _
        chkMovieImagesGetEnglishImages.CheckedChanged, _
        chkMovieImagesNotSaveURLToNfo.CheckedChanged, _
        chkMovieLandscapeKeepExisting.CheckedChanged, _
        chkMovieLockActors.CheckedChanged, _
        chkMovieLockCert.CheckedChanged, _
        chkMovieLockCollectionID.CheckedChanged, _
        chkMovieLockCollections.CheckedChanged, _
        chkMovieLockCountry.CheckedChanged, _
        chkMovieLockCredits.CheckedChanged, _
        chkMovieLockDirector.CheckedChanged, _
        chkMovieLockGenre.CheckedChanged, _
        chkMovieLockLanguageA.CheckedChanged, _
        chkMovieLockLanguageV.CheckedChanged, _
        chkMovieLockMPAA.CheckedChanged, _
        chkMovieLockOriginalTitle.CheckedChanged, _
        chkMovieLockOutline.CheckedChanged, _
        chkMovieLockPlot.CheckedChanged, _
        chkMovieLockRating.CheckedChanged, _
        chkMovieLockReleaseDate.CheckedChanged, _
        chkMovieLockRuntime.CheckedChanged, _
        chkMovieLockStudio.CheckedChanged, _
        chkMovieLockTagline.CheckedChanged, _
        chkMovieLockTags.CheckedChanged, _
        chkMovieLockTitle.CheckedChanged, _
        chkMovieLockTop250.CheckedChanged, _
        chkMovieLockTrailer.CheckedChanged, _
        chkMovieLockYear.CheckedChanged, _
        chkMoviePosterKeepExisting.CheckedChanged, _
        chkMoviePosterPrefOnly.CheckedChanged, _
        chkMovieRecognizeVTSExpertVTS.CheckedChanged, _
        chkMovieScanOrderModify.CheckedChanged, _
        chkMovieScraperCastWithImg.CheckedChanged, _
        chkMovieScraperCertFSK.CheckedChanged, _
        chkMovieScraperCertForMPAAFallback.CheckedChanged, _
        chkMovieScraperCertOnlyValue.CheckedChanged, _
        chkMovieScraperCleanFields.CheckedChanged, _
        chkMovieScraperCleanPlotOutline.CheckedChanged, _
        chkMovieScraperCollectionsAuto.CheckedChanged, _
        chkMovieScraperCountry.CheckedChanged, _
        chkMovieScraperCredits.CheckedChanged, _
        chkMovieScraperDetailView.CheckedChanged, _
        chkMovieScraperDirector.CheckedChanged, _
        chkMovieScraperMPAA.CheckedChanged, _
        chkMovieScraperMetaDataIFOScan.CheckedChanged, _
        chkMovieScraperMetaDataScan.CheckedChanged, _
        chkMovieScraperOriginalTitle.CheckedChanged, _
        chkMovieScraperOutline.CheckedChanged, _
        chkMovieScraperPlotForOutlineIfEmpty.CheckedChanged, _
        chkMovieScraperRating.CheckedChanged, _
        chkMovieScraperRelease.CheckedChanged, _
        chkMovieScraperReleaseFormat.CheckedChanged, _
        chkMovieScraperRuntime.CheckedChanged, _
        chkMovieScraperStudioWithImg.CheckedChanged, _
        chkMovieScraperTagline.CheckedChanged, _
        chkMovieScraperTitle.CheckedChanged, _
        chkMovieScraperTop250.CheckedChanged, _
        chkMovieScraperTrailer.CheckedChanged, _
        chkMovieScraperXBMCTrailerFormat.CheckedChanged, _
        chkMovieScraperYear.CheckedChanged, _
        chkMovieSetBannerExtended.CheckedChanged, _
        chkMovieSetBannerMSAA.CheckedChanged, _
        chkMovieSetBannerKeepExisting.CheckedChanged, _
        chkMovieSetBannerPrefOnly.CheckedChanged, _
        chkMovieSetCleanDB.CheckedChanged, _
        chkMovieSetCleanFiles.CheckedChanged, _
        chkMovieSetClearArtExtended.CheckedChanged, _
        chkMovieSetClearArtMSAA.CheckedChanged, _
        chkMovieSetClearArtKeepExisting.CheckedChanged, _
        chkMovieSetClearLogoExtended.CheckedChanged, _
        chkMovieSetClearLogoMSAA.CheckedChanged, _
        chkMovieSetClearLogoKeepExisting.CheckedChanged, _
        chkMovieSetClickScrapeAsk.CheckedChanged, _
        chkMovieSetDiscArtExtended.CheckedChanged, _
        chkMovieSetFanartExtended.CheckedChanged, _
        chkMovieSetFanartMSAA.CheckedChanged, _
        chkMovieSetFanartKeepExisting.CheckedChanged, _
        chkMovieSetFanartPrefOnly.CheckedChanged, _
        chkMovieSetGeneralMarkNew.CheckedChanged, _
        chkMovieSetImagesCacheEnabled.CheckedChanged, _
        chkMovieSetImagesGetBlankImages.CheckedChanged, _
        chkMovieSetImagesGetEnglishImages.CheckedChanged, _
        chkMovieSetLandscapeExtended.CheckedChanged, _
        chkMovieSetLandscapeMSAA.CheckedChanged, _
        chkMovieSetLandscapeKeepExisting.CheckedChanged, _
        chkMovieSetLockPlot.CheckedChanged, _
        chkMovieSetLockTitle.CheckedChanged, _
        chkMovieSetPosterExtended.CheckedChanged, _
        chkMovieSetPosterMSAA.CheckedChanged, _
        chkMovieSetPosterKeepExisting.CheckedChanged, _
        chkMovieSetPosterPrefOnly.CheckedChanged, _
        chkMovieSetScraperPlot.CheckedChanged, _
        chkMovieSetScraperTitle.CheckedChanged, _
        chkMovieSortBeforeScan.CheckedChanged, _
        chkMovieSourcesBackdropsAuto.CheckedChanged, _
        chkMovieThemeKeepExisting.CheckedChanged, _
 _
        chkMovieTrailerKeepExisting.CheckedChanged, _
        chkMovieUnstackExpertMulti.CheckedChanged, _
        chkMovieUnstackExpertSingle.CheckedChanged, _
        chkMovieUseBaseDirectoryExpertBDMV.CheckedChanged, _
        chkMovieUseBaseDirectoryExpertVTS.CheckedChanged, _
        chkMovieXBMCProtectVTSBDMV.CheckedChanged, _
        chkMovieYAMJCompatibleSets.CheckedChanged, _
        chkTVAllSeasonsBannerKeepExisting.CheckedChanged, _
        chkTVAllSeasonsBannerPrefSizeOnly.CheckedChanged, _
        chkTVAllSeasonsFanartKeepExisting.CheckedChanged, _
        chkTVAllSeasonsFanartPrefSizeOnly.CheckedChanged, _
        chkTVAllSeasonsLandscapeKeepExisting.CheckedChanged, _
        chkTVAllSeasonsPosterKeepExisting.CheckedChanged, _
        chkTVAllSeasonsPosterPrefSizeOnly.CheckedChanged, _
        chkTVCleanDB.CheckedChanged, _
        chkTVDisplayMissingEpisodes.CheckedChanged, _
        chkTVEpisodeActorThumbsFrodo.CheckedChanged, _
        chkTVEpisodeFanartKeepExisting.CheckedChanged, _
        chkTVEpisodeFanartPrefSizeOnly.CheckedChanged, _
        chkTVEpisodeNFOBoxee.CheckedChanged, _
        chkTVEpisodeNFOFrodo.CheckedChanged, _
        chkTVEpisodeNFONMJ.CheckedChanged, _
        chkTVEpisodeNFOYAMJ.CheckedChanged, _
        chkTVEpisodePosterBoxee.CheckedChanged, _
        chkTVEpisodePosterFrodo.CheckedChanged, _
        chkTVEpisodePosterKeepExisting.CheckedChanged, _
        chkTVEpisodePosterPrefSizeOnly.CheckedChanged, _
        chkTVEpisodePosterYAMJ.CheckedChanged, _
        chkTVGeneralClickScrapeAsk.CheckedChanged, _
        chkTVGeneralIgnoreLastScan.CheckedChanged, _
        chkTVGeneralMarkNewEpisodes.CheckedChanged, _
        chkTVGeneralMarkNewShows.CheckedChanged, _
        chkTVImagesCacheEnabled.CheckedChanged, _
        chkTVImagesDisplayImageSelect.CheckedChanged, _
        chkTVImagesGetBlankImages.CheckedChanged, _
        chkTVImagesGetEnglishImages.CheckedChanged, _
        chkTVImagesNotSaveURLToNfo.CheckedChanged, _
        chkTVLockEpisodeLanguageA.CheckedChanged, _
        chkTVLockEpisodeLanguageV.CheckedChanged, _
        chkTVLockEpisodePlot.CheckedChanged, _
        chkTVLockEpisodeRating.CheckedChanged, _
        chkTVLockEpisodeRuntime.CheckedChanged, _
        chkTVLockEpisodeTitle.CheckedChanged, _
        chkTVLockSeasonPlot.CheckedChanged, _
        chkTVLockSeasonTitle.CheckedChanged, _
        chkTVLockShowCert.CheckedChanged, _
        chkTVLockShowCreators.CheckedChanged, _
        chkTVLockShowGenre.CheckedChanged, _
        chkTVLockShowMPAA.CheckedChanged, _
        chkTVLockShowOriginalTitle.CheckedChanged, _
        chkTVLockShowPlot.CheckedChanged, _
        chkTVLockShowRating.CheckedChanged, _
        chkTVLockShowRuntime.CheckedChanged, _
        chkTVLockShowStatus.CheckedChanged, _
        chkTVLockShowStudio.CheckedChanged, _
        chkTVLockShowTitle.CheckedChanged, _
        chkTVScanOrderModify.CheckedChanged, _
        chkTVScraperCleanFields.CheckedChanged, _
        chkTVScraperEpisodeActors.CheckedChanged, _
        chkTVScraperEpisodeAired.CheckedChanged, _
        chkTVScraperEpisodeCredits.CheckedChanged, _
        chkTVScraperEpisodeDirector.CheckedChanged, _
        chkTVScraperEpisodeGuestStars.CheckedChanged, _
        chkTVScraperEpisodeGuestStarsToActors.CheckedChanged, _
        chkTVScraperEpisodePlot.CheckedChanged, _
        chkTVScraperEpisodeRating.CheckedChanged, _
        chkTVScraperEpisodeRuntime.CheckedChanged, _
        chkTVScraperEpisodeTitle.CheckedChanged, _
        chkTVScraperSeasonAired.CheckedChanged, _
        chkTVScraperSeasonPlot.CheckedChanged, _
        chkTVScraperSeasonTitle.CheckedChanged, _
        chkTVScraperShowActors.CheckedChanged, _
        chkTVScraperShowCreators.CheckedChanged, _
        chkTVScraperShowEpiGuideURL.CheckedChanged, _
        chkTVScraperShowGenre.CheckedChanged, _
        chkTVScraperShowMPAA.CheckedChanged, _
        chkTVScraperShowOriginalTitle.CheckedChanged, _
        chkTVScraperShowPlot.CheckedChanged, _
        chkTVScraperShowPremiered.CheckedChanged, _
        chkTVScraperShowRating.CheckedChanged, _
        chkTVScraperShowStatus.CheckedChanged, _
        chkTVScraperShowStudio.CheckedChanged, _
        chkTVScraperShowTitle.CheckedChanged, _
        chkTVScraperUseDisplaySeasonEpisode.CheckedChanged, _
        chkTVScraperUseSRuntimeForEp.CheckedChanged, _
        chkTVSeasonBannerFrodo.CheckedChanged, _
        chkTVSeasonBannerKeepExisting.CheckedChanged, _
        chkTVSeasonBannerPrefSizeOnly.CheckedChanged, _
        chkTVSeasonBannerYAMJ.CheckedChanged, _
        chkTVSeasonFanartFrodo.CheckedChanged, _
        chkTVSeasonFanartKeepExisting.CheckedChanged, _
        chkTVSeasonFanartPrefSizeOnly.CheckedChanged, _
        chkTVSeasonFanartYAMJ.CheckedChanged, _
        chkTVSeasonLandscapeAD.CheckedChanged, _
        chkTVSeasonLandscapeExtended.CheckedChanged, _
        chkTVSeasonLandscapeKeepExisting.CheckedChanged, _
        chkTVSeasonPosterBoxee.CheckedChanged, _
        chkTVSeasonPosterFrodo.CheckedChanged, _
        chkTVSeasonPosterKeepExisting.CheckedChanged, _
        chkTVSeasonPosterPrefSizeOnly.CheckedChanged, _
        chkTVSeasonPosterYAMJ.CheckedChanged, _
        chkTVShowActorThumbsExpert.CheckedChanged, _
        chkTVShowActorThumbsFrodo.CheckedChanged, _
        chkTVShowBannerBoxee.CheckedChanged, _
        chkTVShowBannerFrodo.CheckedChanged, _
        chkTVShowBannerKeepExisting.CheckedChanged, _
        chkTVShowBannerPrefSizeOnly.CheckedChanged, _
        chkTVShowBannerYAMJ.CheckedChanged, _
        chkTVShowCharacterArtAD.CheckedChanged, _
        chkTVShowCharacterArtExtended.CheckedChanged, _
        chkTVShowCharacterArtKeepExisting.CheckedChanged, _
        chkTVShowClearArtAD.CheckedChanged, _
        chkTVShowClearArtExtended.CheckedChanged, _
        chkTVShowClearArtKeepExisting.CheckedChanged, _
        chkTVShowClearLogoAD.CheckedChanged, _
        chkTVShowClearLogoExtended.CheckedChanged, _
        chkTVShowClearLogoKeepExisting.CheckedChanged, _
        chkTVShowExtrafanartsKeepExisting.CheckedChanged, _
        chkTVShowExtrafanartsPrefSizeOnly.CheckedChanged, _
        chkTVShowExtrafanartsExpert.CheckedChanged, _
        chkTVShowExtrafanartsFrodo.CheckedChanged, _
        chkTVShowFanartBoxee.CheckedChanged, _
        chkTVShowFanartFrodo.CheckedChanged, _
        chkTVShowFanartKeepExisting.CheckedChanged, _
        chkTVShowFanartPrefSizeOnly.CheckedChanged, _
        chkTVShowFanartYAMJ.CheckedChanged, _
        chkTVShowLandscapeAD.CheckedChanged, _
        chkTVShowLandscapeExtended.CheckedChanged, _
        chkTVShowLandscapeKeepExisting.CheckedChanged, _
        chkTVShowNFOBoxee.CheckedChanged, _
        chkTVShowNFOFrodo.CheckedChanged, _
        chkTVShowNFONMJ.CheckedChanged, _
        chkTVShowNFOYAMJ.CheckedChanged, _
        chkTVShowPosterBoxee.CheckedChanged, _
        chkTVShowPosterFrodo.CheckedChanged, _
        chkTVShowPosterKeepExisting.CheckedChanged, _
        chkTVShowPosterPrefSizeOnly.CheckedChanged, _
        chkTVShowPosterYAMJ.CheckedChanged, _
        chkTVShowThemeKeepExisting.CheckedChanged, _
        tcFileSystemCleaner.SelectedIndexChanged, _
        txtGeneralDaemonPath.TextChanged, _
        txtMovieActorThumbsExtExpertBDMV.TextChanged, _
        txtMovieActorThumbsExtExpertMulti.TextChanged, _
        txtMovieActorThumbsExtExpertSingle.TextChanged, _
        txtMovieActorThumbsExtExpertVTS.TextChanged, _
        txtMovieBannerExpertBDMV.TextChanged, _
        txtMovieBannerExpertMulti.TextChanged, _
        txtMovieBannerExpertSingle.TextChanged, _
        txtMovieBannerExpertVTS.TextChanged, _
        txtMovieBannerHeight.TextChanged, _
        txtMovieBannerWidth.TextChanged, _
        txtMovieClearArtExpertBDMV.TextChanged, _
        txtMovieClearArtExpertMulti.TextChanged, _
        txtMovieClearArtExpertSingle.TextChanged, _
        txtMovieClearArtExpertVTS.TextChanged, _
        txtMovieClearLogoExpertBDMV.TextChanged, _
        txtMovieClearLogoExpertMulti.TextChanged, _
        txtMovieClearLogoExpertSingle.TextChanged, _
        txtMovieClearLogoExpertVTS.TextChanged, _
        txtMovieDiscArtExpertBDMV.TextChanged, _
        txtMovieDiscArtExpertMulti.TextChanged, _
        txtMovieDiscArtExpertSingle.TextChanged, _
        txtMovieDiscArtExpertVTS.TextChanged, _
        txtMovieExtrafanartsHeight.TextChanged, _
        txtMovieExtrafanartsLimit.TextChanged, _
        txtMovieExtrafanartsWidth.TextChanged, _
        txtMovieExtrathumbsHeight.TextChanged, _
        txtMovieExtrathumbsLimit.TextChanged, _
        txtMovieExtrathumbsWidth.TextChanged, _
        txtMovieFanartExpertBDMV.TextChanged, _
        txtMovieFanartExpertMulti.TextChanged, _
        txtMovieFanartExpertSingle.TextChanged, _
        txtMovieFanartExpertVTS.TextChanged, _
        txtMovieFanartHeight.TextChanged, _
        txtMovieFanartWidth.TextChanged, _
        txtMovieGeneralCustomMarker1.TextChanged, _
        txtMovieGeneralCustomMarker2.TextChanged, _
        txtMovieGeneralCustomMarker3.TextChanged, _
        txtMovieGeneralCustomMarker4.TextChanged, _
        txtMovieLandscapeExpertBDMV.TextChanged, _
        txtMovieLandscapeExpertMulti.TextChanged, _
        txtMovieLandscapeExpertSingle.TextChanged, _
        txtMovieLandscapeExpertVTS.TextChanged, _
        txtMovieLevTolerance.TextChanged, _
        txtMovieNFOExpertBDMV.TextChanged, _
        txtMovieNFOExpertMulti.TextChanged, _
        txtMovieNFOExpertSingle.TextChanged, _
        txtMovieNFOExpertVTS.TextChanged, _
        txtMoviePosterExpertBDMV.TextChanged, _
        txtMoviePosterExpertMulti.TextChanged, _
        txtMoviePosterExpertSingle.TextChanged, _
        txtMoviePosterExpertVTS.TextChanged, _
        txtMoviePosterHeight.TextChanged, _
        txtMoviePosterWidth.TextChanged, _
        txtMovieScraperCastLimit.TextChanged, _
        txtMovieScraperDurationRuntimeFormat.TextChanged, _
        txtMovieScraperGenreLimit.TextChanged, _
        txtMovieScraperMPAANotRated.TextChanged, _
        txtMovieScraperOutlineLimit.TextChanged, _
        txtMovieScraperStudioLimit.TextChanged, _
        txtMovieSetBannerExpertParent.TextChanged, _
        txtMovieSetBannerExpertSingle.TextChanged, _
        txtMovieSetBannerHeight.TextChanged, _
        txtMovieSetBannerWidth.TextChanged, _
        txtMovieSetClearArtExpertParent.TextChanged, _
        txtMovieSetClearArtExpertSingle.TextChanged, _
        txtMovieSetClearLogoExpertParent.TextChanged, _
        txtMovieSetClearLogoExpertSingle.TextChanged, _
        txtMovieSetDiscArtExpertParent.TextChanged, _
        txtMovieSetDiscArtExpertSingle.TextChanged, _
        txtMovieSetFanartExpertParent.TextChanged, _
        txtMovieSetFanartExpertSingle.TextChanged, _
        txtMovieSetFanartHeight.TextChanged, _
        txtMovieSetFanartWidth.TextChanged, _
        txtMovieSetLandscapeExpertParent.TextChanged, _
        txtMovieSetLandscapeExpertSingle.TextChanged, _
        txtMovieSetNFOExpertParent.TextChanged, _
        txtMovieSetNFOExpertSingle.TextChanged, _
        txtMovieSetPathMSAA.TextChanged, _
        txtMovieSetPosterExpertParent.TextChanged, _
        txtMovieSetPosterExpertSingle.TextChanged, _
        txtMovieSetPosterHeight.TextChanged, _
        txtMovieSetPosterWidth.TextChanged, _
        txtMovieThemeTvTunesCustomPath.TextChanged, _
        txtMovieThemeTvTunesSubDir.TextChanged, _
        txtMovieTrailerExpertBDMV.TextChanged, _
        txtMovieTrailerExpertMulti.TextChanged, _
        txtMovieTrailerExpertSingle.TextChanged, _
        txtMovieTrailerExpertVTS.TextChanged, _
        txtMovieYAMJWatchedFolder.TextChanged, _
        txtProxyDomain.TextChanged, _
        txtProxyPassword.TextChanged, _
        txtProxyPort.TextChanged, _
        txtProxyUsername.TextChanged, _
        txtTVAllSeasonsBannerHeight.TextChanged, _
        txtTVAllSeasonsBannerWidth.TextChanged, _
        txtTVAllSeasonsFanartHeight.TextChanged, _
        txtTVAllSeasonsFanartWidth.TextChanged, _
        txtTVAllSeasonsPosterHeight.TextChanged, _
        txtTVAllSeasonsPosterWidth.TextChanged, _
        txtTVEpisodeFanartHeight.TextChanged, _
        txtTVEpisodeFanartWidth.TextChanged, _
        txtTVEpisodePosterHeight.TextChanged, _
        txtTVEpisodePosterWidth.TextChanged, _
        txtTVScraperDurationRuntimeFormat.TextChanged, _
        txtTVSeasonBannerHeight.TextChanged, _
        txtTVSeasonBannerWidth.TextChanged, _
        txtTVSeasonFanartHeight.TextChanged, _
        txtTVSeasonFanartWidth.TextChanged, _
        txtTVSeasonPosterHeight.TextChanged, _
        txtTVSeasonPosterWidth.TextChanged, _
        txtTVShowActorThumbsExtExpert.TextChanged, _
        txtTVShowBannerExpert.TextChanged, _
        txtTVShowBannerHeight.TextChanged, _
        txtTVShowBannerWidth.TextChanged, _
        txtTVShowCharacterArtExpert.TextChanged, _
        txtTVShowClearArtExpert.TextChanged, _
        txtTVShowClearLogoExpert.TextChanged, _
        txtTVShowExtrafanartsHeight.TextChanged, _
        txtTVShowExtrafanartsLimit.TextChanged, _
        txtTVShowExtrafanartsWidth.TextChanged, _
        txtTVShowFanartExpert.TextChanged, _
        txtTVShowFanartHeight.TextChanged, _
        txtTVShowFanartWidth.TextChanged, _
        txtTVShowLandscapeExpert.TextChanged, _
        txtTVShowNFOExpert.TextChanged, _
        txtTVShowPosterExpert.TextChanged, _
        txtTVShowPosterHeight.TextChanged, _
        txtTVShowPosterWidth.TextChanged, _
        txtTVShowThemeTvTunesCustomPath.TextChanged, _
        txtTVShowThemeTvTunesSubDir.TextChanged

        Me.SetApplyButton(True)
    End Sub

#End Region 'Methods

End Class