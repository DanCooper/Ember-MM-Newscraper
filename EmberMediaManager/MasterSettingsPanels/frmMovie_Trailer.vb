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

Public Class frmMovie_Trailer
    Implements Interfaces.ISettingsPanel

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
        ChildType = Enums.SettingsPanelType.MovieTrailer
        IsEnabled = True
        Image = Nothing
        ImageIndex = 2
        Order = 600
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(1195, "Trailers")
        ParentType = Enums.SettingsPanelType.Movie
        UniqueId = "Movie_Trailer"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        With Master.eLang
            lblKeepExisting.Text = .GetString(971, "Keep existing")
            lblDefaultSearchParameter.Text = String.Concat(.GetString(1172, "Default Search Parameter"), ":")
            lblMinimumQuality.Text = String.Concat(.GetString(1027, "Minimum Quality"), ":")
            lblPreferredQuality.Text = String.Concat(.GetString(800, "Preferred Quality"), ":")
        End With

        Load_VideoResolutions()
        Load_VideoTypes()
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
        With Master.eSettings.Movie.TrailerSettings
            .DefaultSearchParameter = txtDefaultSearchParameter.Text
            .KeepExisting = chkKeepExisting.Checked
            .MinimumVideoQuality = CType(cbMinimumQuality.SelectedItem, KeyValuePair(Of String, Enums.VideoResolution)).Value
            .PreferredVideoQuality = CType(cbPreferredQuality.SelectedItem, KeyValuePair(Of String, Enums.VideoResolution)).Value

            'Languages
            .ForceLanguage = chkForceLanguage.Checked
            If Not String.IsNullOrEmpty(cbForcedLanguage.Text) Then
                .ForcedLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = cbForcedLanguage.Text).Abbrevation_MainLanguage
            End If
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Private Sub EnableApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        cbForcedLanguage.SelectedIndexChanged,
        cbPreferredType.SelectedIndexChanged,
        cbMinimumQuality.SelectedIndexChanged,
        cbPreferredQuality.SelectedIndexChanged,
        chkKeepExisting.CheckedChanged,
        chkPreferredTypeOnly.CheckedChanged,
        txtDefaultSearchParameter.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Public Sub Settings_Load()
        With Master.eSettings.Movie.TrailerSettings
            cbMinimumQuality.SelectedValue = .MinimumVideoQuality
            cbPreferredQuality.SelectedValue = .PreferredVideoQuality
            chkKeepExisting.Checked = .KeepExisting
            txtDefaultSearchParameter.Text = .DefaultSearchParameter

            'Language
            chkForceLanguage.Checked = .ForceLanguage
            Try
                cbForcedLanguage.Items.Clear()
                cbForcedLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Name).Distinct.ToArray)
                If cbForcedLanguage.Items.Count > 0 Then
                    If .ForcedLanguageSpecified Then
                        Dim tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = .ForcedLanguage)
                        If tLanguage IsNot Nothing Then
                            cbForcedLanguage.Text = tLanguage.Name
                        Else
                            tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.ForcedLanguage))
                            If tLanguage IsNot Nothing Then
                                cbForcedLanguage.Text = tLanguage.Name
                            Else
                                cbForcedLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                            End If
                        End If
                    Else
                        cbForcedLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                    End If
                End If
            Catch ex As Exception
                'logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End With
    End Sub

    Private Sub Load_VideoResolutions()
        Dim items As New Dictionary(Of String, Enums.VideoResolution) From {
            {Master.eLang.GetString(745, "Any"), Enums.VideoResolution.Any},
            {"2160p 60fps", Enums.VideoResolution.HD2160p60fps},
            {"2160p", Enums.VideoResolution.HD2160p},
            {"1440p", Enums.VideoResolution.HD1440p},
            {"1080p 60fps", Enums.VideoResolution.HD1080p60fps},
            {"1080p", Enums.VideoResolution.HD1080p},
            {"720p 60fps", Enums.VideoResolution.HD720p60fps},
            {"720p", Enums.VideoResolution.HD720p},
            {"480p", Enums.VideoResolution.HQ480p},
            {"360p", Enums.VideoResolution.SQ360p},
            {"240p", Enums.VideoResolution.SQ240p},
            {"144p", Enums.VideoResolution.SQ144p},
            {"144p 15fps", Enums.VideoResolution.SQ144p15fps}
        }
        cbMinimumQuality.DataSource = items.ToList
        cbMinimumQuality.DisplayMember = "Key"
        cbMinimumQuality.ValueMember = "Value"
        cbPreferredQuality.DataSource = items.ToList
        cbPreferredQuality.DisplayMember = "Key"
        cbPreferredQuality.ValueMember = "Value"
    End Sub

    Private Sub Load_VideoTypes()
        Dim items As New Dictionary(Of String, Enums.VideoType) From {
            {Master.eLang.GetString(745, "Any"), Enums.VideoType.Any},
            {"2160p 60fps", Enums.VideoType.Clip},
            {"2160p", Enums.VideoType.Featurette},
            {"1440p", Enums.VideoType.Teaser},
            {"1080p 60fps", Enums.VideoType.Trailer}
        }
        cbPreferredType.DataSource = items.ToList
        cbPreferredType.DisplayMember = "Key"
        cbPreferredType.ValueMember = "Value"
    End Sub

    Private Sub PreferredQuality_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If CType(cbPreferredQuality.SelectedItem, KeyValuePair(Of String, Enums.VideoResolution)).Value = Enums.VideoResolution.Any Then
            cbMinimumQuality.Enabled = False
        Else
            cbMinimumQuality.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Methods

End Class