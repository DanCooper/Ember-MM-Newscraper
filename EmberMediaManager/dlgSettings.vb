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

Imports EmberAPI
Imports NLog

Public Class dlgSettings

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AllSettingsPanels As New List(Of Containers.SettingsPanel)
    Private _CurrPanel As New Panel
    Private _CurrButton As New ButtonTag
    Private _DidApply As Boolean = False
    Private _MasterSettingsPanels As New List(Of Interfaces.IMasterSettingsPanel)
    Private _NoUpdate As Boolean = True
    Private _Results As New Structures.SettingsResult

#End Region 'Fields

#Region "Events"

    Public Event LoadEnd()

#End Region 'Events

#Region "Dialog"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Functions.PNLDoubleBuffer(pnlSettingsMain)
        Setup()
        _AllSettingsPanels.Clear()
        _MasterSettingsPanels.Clear()
        SettingsPanels_Add_MasterPanels()
        SettingsPanels_Add_AddonPanels()
        SettingsPanels_Reorder_AddonPanels()
        TopMenu_AddButtons()

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

        'reset all triggers
        _Results = New Structures.SettingsResult
        _DidApply = False
        _NoUpdate = False
        RaiseEvent LoadEnd()
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub Dialog_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
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

    Private Sub Setup()
        Text = Master.eLang.GetString(420, "Settings")
        btnApply.Text = Master.eLang.Apply
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK
        lblSettingsTopDetails.Text = Master.eLang.GetString(518, "Configure Ember's appearance and operation.")
        lblSettingsTopTitle.Text = Text
    End Sub

    Public Overloads Function ShowDialog() As Structures.SettingsResult
        MyBase.ShowDialog()
        Return _Results
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Sub Button_Apply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
        Settings_Save_All(True)
        Button_Apply_SetEnabled(False)
        If _Results.NeedsDBClean_Movie OrElse
            _Results.NeedsDBClean_TV OrElse
            _Results.NeedsDBUpdate_Movie OrElse
            _Results.NeedsDBUpdate_TV OrElse
            _Results.NeedsReload_Movie OrElse
            _Results.NeedsReload_Movieset OrElse
            _Results.NeedsReload_TVShow Then
            _DidApply = True
        End If
    End Sub

    Private Sub Button_Apply_SetEnabled(ByVal Value As Boolean)
        If Not _NoUpdate Then btnApply.Enabled = Value
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If Not _DidApply Then _Results.DidCancel = True
        Master.eLang.LoadAllLanguage(Master.eSettings.GeneralLanguage, True)
        SettingsPanels_Remove_AllPanels()
        Close()
    End Sub

    Private Sub Button_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        _NoUpdate = True
        Settings_Save_All(False)
        SettingsPanels_Remove_AllPanels()
        Close()
    End Sub

    Private Sub Handle_NeedsDBClean_Movie()
        _Results.NeedsDBClean_Movie = True
    End Sub

    Private Sub Handle_NeedsDBClean_TV()
        _Results.NeedsDBClean_TV = True
    End Sub

    Private Sub Handle_NeedsDBUpdate_Movie()
        _Results.NeedsDBUpdate_Movie = True
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV()
        _Results.NeedsDBUpdate_TV = True
    End Sub

    Private Sub Handle_NeedsReload_Movie()
        _Results.NeedsReload_Movie = True
    End Sub

    Private Sub Handle_NeedsReload_Movieset()
        _Results.NeedsReload_Movieset = True
    End Sub

    Private Sub Handle_NeedsReload_TVEpisode()
        _Results.NeedsReload_TVEpisode = True
    End Sub

    Private Sub Handle_NeedsReload_TVShow()
        _Results.NeedsReload_TVShow = True
    End Sub

    Private Sub Handle_NeedsRestart()
        _Results.NeedsRestart = True
    End Sub

    Private Sub Handle_SettingsChanged()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub Handle_StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)
        Dim tSettingsPanel As New Containers.SettingsPanel
        Dim oSettingsPanel As Containers.SettingsPanel = Nothing
        SuspendLayout()

        'Get the panel that has been changed
        tSettingsPanel = _AllSettingsPanels.FirstOrDefault(Function(f) f.SettingsPanelID = SettingsPanelID)
        If tSettingsPanel IsNot Nothing Then
            Try
                Dim t() As TreeNode = tvSettingsList.Nodes.Find(SettingsPanelID, True)
                If t.Count > 0 Then
                    If Not DiffOrder = 0 Then
                        Dim p As TreeNode = t(0).Parent
                        Dim i As Integer = t(0).Index
                        If DiffOrder < 0 AndAlso t(0).PrevNode IsNot Nothing Then
                            'move down prev node
                            oSettingsPanel = _AllSettingsPanels.FirstOrDefault(Function(f) f.SettingsPanelID = t(0).PrevNode.Name)
                            If oSettingsPanel IsNot Nothing Then oSettingsPanel.Order = i ' + (DiffOrder * -1)
                        End If
                        If DiffOrder > 0 AndAlso t(0).NextNode IsNot Nothing Then
                            'move up next node
                            oSettingsPanel = _AllSettingsPanels.FirstOrDefault(Function(f) f.SettingsPanelID = t(0).NextNode.Name)
                            If oSettingsPanel IsNot Nothing Then oSettingsPanel.Order = i ' + (DiffOrder * -1)
                        End If
                        p.Nodes.Remove(t(0))
                        p.Nodes.Insert(i + DiffOrder, t(0))
                        t(0).TreeView.SelectedNode = t(0)
                        tSettingsPanel.ImageIndex = If(State, 9, 10)
                        tSettingsPanel.Order = i + DiffOrder
                    End If
                    t(0).ImageIndex = If(State, 9, 10)
                    t(0).SelectedImageIndex = If(State, 9, 10)
                    pbSettingsCurrent.Image = ilSettings.Images(If(State, 9, 10))
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
        SettingsPanels_Reorder_AddonPanels()
        ResumeLayout()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub Settings_Save_All(ByVal IsApply As Boolean)
        'MasterSettingsPanels
        For Each s In _MasterSettingsPanels
            s.SaveSettings()
        Next

        'AddonSettingsPanels
        For Each s As AddonsManager.GenericAddon In AddonsManager.Instance.GenericAddons
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Data_Movie In AddonsManager.Instance.ScraperAddons_Data_Movie
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Data_Movieset In AddonsManager.Instance.ScraperAddons_Data_Movieset
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Data_TV In AddonsManager.Instance.ScraperAddons_Data_TV
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Image_Movie In AddonsManager.Instance.ScraperAddons_Image_Movie
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Image_MovieSet In AddonsManager.Instance.ScraperAddons_Image_Movieset
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Image_TV In AddonsManager.Instance.ScraperAddons_Image_TV
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Theme_Movie In AddonsManager.Instance.ScraperAddons_Theme_Movie
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Theme_TV In AddonsManager.Instance.ScraperAddons_Theme_TV
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each s As AddonsManager.ScraperAddon_Trailer_Movie In AddonsManager.Instance.ScraperAddons_Trailer_Movie
            Try
                s.ProcessorModule.SaveSetup(Not IsApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        AddonsManager.Instance.Settings_Save()
    End Sub

    Private Sub SettingsPanels_Add_MasterPanels()
        _MasterSettingsPanels.Add(frmMovie_Data)
        _MasterSettingsPanels.Add(frmMovie_FileNaming)
        _MasterSettingsPanels.Add(frmMovie_GUI)
        _MasterSettingsPanels.Add(frmMovie_Image)
        _MasterSettingsPanels.Add(frmMovie_Source)
        _MasterSettingsPanels.Add(frmMovie_Theme)
        _MasterSettingsPanels.Add(frmMovie_Trailer)
        _MasterSettingsPanels.Add(frmMovieset_Data)
        _MasterSettingsPanels.Add(frmMovieset_FileNaming)
        _MasterSettingsPanels.Add(frmMovieset_GUI)
        _MasterSettingsPanels.Add(frmMovieset_Image)
        _MasterSettingsPanels.Add(frmOption_AVCodecMapping)
        _MasterSettingsPanels.Add(frmOption_Connection)
        _MasterSettingsPanels.Add(frmOption_FileSystem)
        _MasterSettingsPanels.Add(frmOption_General)
        _MasterSettingsPanels.Add(frmOption_VideoSourceMapping)
        _MasterSettingsPanels.Add(frmTV_Data)
        _MasterSettingsPanels.Add(frmTV_FileNaming)
        _MasterSettingsPanels.Add(frmTV_GUI)
        _MasterSettingsPanels.Add(frmTV_Image)
        _MasterSettingsPanels.Add(frmTV_Source)
        _MasterSettingsPanels.Add(frmTV_Theme)

        For Each s As Interfaces.IMasterSettingsPanel In _MasterSettingsPanels
            Dim nPanel As Containers.SettingsPanel = s.InjectSettingsPanel()
            If nPanel IsNot Nothing Then
                _AllSettingsPanels.Add(nPanel)
                AddHandler s.NeedsDBClean_Movie, AddressOf Handle_NeedsDBClean_Movie
                AddHandler s.NeedsDBClean_TV, AddressOf Handle_NeedsDBClean_TV
                AddHandler s.NeedsDBUpdate_Movie, AddressOf Handle_NeedsDBUpdate_Movie
                AddHandler s.NeedsDBUpdate_TV, AddressOf Handle_NeedsDBUpdate_TV
                AddHandler s.NeedsReload_Movie, AddressOf Handle_NeedsReload_Movie
                AddHandler s.NeedsReload_MovieSet, AddressOf Handle_NeedsReload_Movieset
                AddHandler s.NeedsReload_TVEpisode, AddressOf Handle_NeedsReload_TVEpisode
                AddHandler s.NeedsReload_TVShow, AddressOf Handle_NeedsReload_TVShow
                AddHandler s.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.SettingsChanged, AddressOf Handle_SettingsChanged
            End If
        Next
    End Sub

    Private Sub SettingsPanels_Add_AddonPanels()
        Dim nSettingsPanel As Containers.SettingsPanel = Nothing
        'Use 9000 as panel order beginning to show all generic panels after MasterPanels
        Dim iPanelCounter As Integer = 9000
        For Each s As AddonsManager.GenericAddon In AddonsManager.Instance.GenericAddons.OrderBy(Function(f) f.AssemblyName)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                If nSettingsPanel.ImageIndex = -1 AndAlso nSettingsPanel.Image IsNot Nothing Then
                    ilSettings.Images.Add(nSettingsPanel.SettingsPanelID, nSettingsPanel.Image)
                    nSettingsPanel.ImageIndex = ilSettings.Images.IndexOfKey(nSettingsPanel.SettingsPanelID)
                End If
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Data_Movie In AddonsManager.Instance.ScraperAddons_Data_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Data_Movieset In AddonsManager.Instance.ScraperAddons_Data_Movieset.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Data_TV In AddonsManager.Instance.ScraperAddons_Data_TV.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Image_Movie In AddonsManager.Instance.ScraperAddons_Image_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Image_MovieSet In AddonsManager.Instance.ScraperAddons_Image_Movieset.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Image_TV In AddonsManager.Instance.ScraperAddons_Image_TV.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Theme_Movie In AddonsManager.Instance.ScraperAddons_Theme_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Theme_TV In AddonsManager.Instance.ScraperAddons_Theme_TV.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
        iPanelCounter = 0
        For Each s As AddonsManager.ScraperAddon_Trailer_Movie In AddonsManager.Instance.ScraperAddons_Trailer_Movie.OrderBy(Function(x) x.ProcessorModule.Order)
            s.ProcessorModule.InjectSettingsPanel()
            nSettingsPanel = s.ProcessorModule.SettingsPanel
            If nSettingsPanel IsNot Nothing AndAlso nSettingsPanel.Panel IsNot Nothing Then
                nSettingsPanel.Order = iPanelCounter
                nSettingsPanel.SettingsPanelID = String.Concat(s.AssemblyName, "_", s.InterfaceType)
                _AllSettingsPanels.Add(nSettingsPanel)
                iPanelCounter += 1
                AddHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
                AddHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
                AddHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
            End If
        Next
    End Sub

    Private Sub SettingsPanels_Remove_AllPanels()
        'MasterSettingsPanels
        For Each s As Interfaces.IMasterSettingsPanel In _MasterSettingsPanels
            RemoveHandler s.NeedsDBClean_Movie, AddressOf Handle_NeedsDBClean_Movie
            RemoveHandler s.NeedsDBClean_TV, AddressOf Handle_NeedsDBClean_TV
            RemoveHandler s.NeedsDBUpdate_Movie, AddressOf Handle_NeedsDBUpdate_Movie
            RemoveHandler s.NeedsDBUpdate_TV, AddressOf Handle_NeedsDBUpdate_TV
            RemoveHandler s.NeedsReload_Movie, AddressOf Handle_NeedsReload_Movie
            RemoveHandler s.NeedsReload_MovieSet, AddressOf Handle_NeedsReload_Movieset
            RemoveHandler s.NeedsReload_TVEpisode, AddressOf Handle_NeedsReload_TVEpisode
            RemoveHandler s.NeedsReload_TVShow, AddressOf Handle_NeedsReload_TVShow
            RemoveHandler s.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.SettingsChanged, AddressOf Handle_SettingsChanged
            s.DoDispose()
        Next

        'AddonSettingsPanels
        For Each s As AddonsManager.ScraperAddon_Data_Movie In AddonsManager.Instance.ScraperAddons_Data_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Data_Movieset In AddonsManager.Instance.ScraperAddons_Data_Movieset
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Data_TV In AddonsManager.Instance.ScraperAddons_Data_TV
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Image_Movie In AddonsManager.Instance.ScraperAddons_Image_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Image_MovieSet In AddonsManager.Instance.ScraperAddons_Image_Movieset
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Image_TV In AddonsManager.Instance.ScraperAddons_Image_TV
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Theme_Movie In AddonsManager.Instance.ScraperAddons_Theme_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Theme_TV In AddonsManager.Instance.ScraperAddons_Theme_TV
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.ScraperAddon_Trailer_Movie In AddonsManager.Instance.ScraperAddons_Trailer_Movie
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
        For Each s As AddonsManager.GenericAddon In AddonsManager.Instance.GenericAddons
            RemoveHandler s.ProcessorModule.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler s.ProcessorModule.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler s.ProcessorModule.StateChanged, AddressOf Handle_StateChanged
        Next
    End Sub

    Private Sub SettingsPanels_Remove_CurrPanel()
        If pnlSettingsMain.Controls.Count > 0 Then
            pnlSettingsMain.Controls.Remove(_CurrPanel)
        End If
    End Sub

    Private Sub SettingsPanels_Reorder_AddonPanels()
        Dim iPosition As Integer = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Data_Movie.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Data_Movie.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Data_Movieset.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Data_Movieset.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Data_TV.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Data_TV.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Image_Movie.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Image_Movie.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Image_Movieset.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Image_Movieset.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Image_TV.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Image_TV.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Theme_Movie.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Theme_Movie.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Theme_TV.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Theme_TV.Count})
            iPosition += 1
        Next
        iPosition = 0
        For Each s In AddonsManager.Instance.ScraperAddons_Trailer_Movie.OrderBy(Function(f) f.ProcessorModule.SettingsPanel.Order)
            s.ProcessorModule.Order = s.ProcessorModule.SettingsPanel.Order
            s.ProcessorModule.OrderChanged(New Containers.SettingsPanel.OrderState With {
                                           .Position = iPosition,
                                           .TotalCount = AddonsManager.Instance.ScraperAddons_Trailer_Movie.Count})
            iPosition += 1
        Next
    End Sub

    Private Sub TopMenu_AddButtons()
        Dim lstTSB As New List(Of ToolStripButton)
        Dim nTSB As ToolStripButton

        tsSettingsTopMenu.Items.Clear()

        'first create all the buttons so we can get their size to calculate the spacer
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(390, "Options"),
            .Image = My.Resources.General,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Options, .Index = 100, .Title = Master.eLang.GetString(390, "Options")}}
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(36, "Movies"),
            .Image = My.Resources.Movie,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Movie, .Index = 200, .Title = Master.eLang.GetString(36, "Movies")}}
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(1203, "MovieSets"),
            .Image = My.Resources.MovieSet,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Movieset, .Index = 300, .Title = Master.eLang.GetString(1203, "MovieSets")}}
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(653, "TV Shows"),
            .Image = My.Resources.TVShows,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.TV, .Index = 400, .Title = Master.eLang.GetString(653, "TV Shows")}}
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(802, "Addons"),
            .Image = My.Resources.modules,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Addon, .Index = 500, .Title = Master.eLang.GetString(802, "Addons")}}
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)

        If lstTSB.Count > 0 Then
            Dim ButtonsWidth As Integer = 0
            Dim ButtonsCount As Integer = 0
            Dim sLength As Integer = 0
            Dim sRest As Double = 0
            Dim sSpacer As String = String.Empty

            'add all buttons to the top horizontal menu
            For Each tButton As ToolStripButton In lstTSB.OrderBy(Function(b) DirectCast(b.Tag, ButtonTag).Index)
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
            _CurrButton = DirectCast(lstTSB.Item(0).Tag, ButtonTag)
            TreeView_Fill(DirectCast(lstTSB.Item(0).Tag, ButtonTag).PanelType)
        End If
    End Sub

    Private Sub TopMenu_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        _CurrButton = DirectCast(DirectCast(sender, ToolStripButton).Tag, ButtonTag)
        TreeView_Fill(_CurrButton.PanelType)
    End Sub

    Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvSettingsList.AfterSelect
        If Not tvSettingsList.SelectedNode.ImageIndex = -1 Then
            pbSettingsCurrent.Image = ilSettings.Images(tvSettingsList.SelectedNode.ImageIndex)
        Else
            pbSettingsCurrent.Image = Nothing
        End If
        lblSettingsCurrent.Text = String.Format("{0} - {1}", _CurrButton.Title, tvSettingsList.SelectedNode.Text)

        SettingsPanels_Remove_CurrPanel()

        _CurrPanel = _AllSettingsPanels.FirstOrDefault(Function(p) p.SettingsPanelID = tvSettingsList.SelectedNode.Name).Panel
        _CurrPanel.Location = New Point(0, 0)
        _CurrPanel.Dock = DockStyle.Fill
        pnlSettingsMain.Controls.Add(_CurrPanel)
        _CurrPanel.Visible = True
        pnlSettingsMain.Refresh()
    End Sub

    Private Sub TreeView_Fill(ByVal PanelType As Enums.SettingsPanelType)
        Dim pNode As New TreeNode

        tvSettingsList.Nodes.Clear()
        SettingsPanels_Remove_CurrPanel()

        For Each MainPanel As Containers.SettingsPanel In _AllSettingsPanels.Where(Function(m) m.Type = PanelType).OrderBy(Function(s) s.Order)
            pNode = New TreeNode(MainPanel.Title, MainPanel.ImageIndex, MainPanel.ImageIndex) With {.Name = MainPanel.SettingsPanelID}
            For Each SubPanel As Containers.SettingsPanel In _AllSettingsPanels.Where(Function(s) s.Type = MainPanel.Contains).OrderBy(Function(s) s.Order)
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

#End Region 'Methods

#Region "Nested Types"

    Private Structure ButtonTag

        Dim Index As Integer
        Dim Title As String
        Dim PanelType As Enums.SettingsPanelType

    End Structure

#End Region 'Nested Types

End Class