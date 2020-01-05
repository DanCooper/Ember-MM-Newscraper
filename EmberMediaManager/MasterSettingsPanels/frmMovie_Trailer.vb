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
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Private _SGVWidthCalculated As Boolean

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
            .Contains = Enums.SettingsPanelType.MovieTrailer,
            .ImageIndex = 2,
            .Order = 600,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movie_Trailer",
            .Title = Master.eLang.GetString(1195, "Trailers"),
            .Type = Enums.SettingsPanelType.Movie
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.Movie.TrailerSettings
            .DefaultSearchParameter = txtDefaultSearchParameter.Text
            .KeepExisting = chkKeepExisting.Checked
            .MinimumVideoQuality = CType(cbMinimumQuality.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
            .PreferredVideoQuality = CType(cbPreferredQuality.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
            .ScraperSettings = sgvScraper.Save
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.Movie.TrailerSettings
            cbMinimumQuality.SelectedValue = .MinimumVideoQuality
            cbPreferredQuality.SelectedValue = .PreferredVideoQuality
            chkKeepExisting.Checked = .KeepExisting
            sgvScraper.AddSettings(.ScraperSettings)
            txtDefaultSearchParameter.Text = .DefaultSearchParameter
        End With
        Load_MovieTrailerQualities()
    End Sub
    ''' <summary>
    ''' Workaround to autosize the DGV based on column widths without change the row hights
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Settings_VisibleChanged(sender As Object, e As EventArgs) Handles pnlSettings.VisibleChanged
        If Not _SGVWidthCalculated AndAlso CType(sender, Panel).Visible Then
            tblTrailers.SuspendLayout()
            For i As Integer = 0 To tblTrailers.Controls.Count - 1
                Dim nType As Type = tblTrailers.Controls(i).GetType
                If nType.Name = "ScraperGridView" Then
                    Dim nDataGridView As DataGridView = CType(tblTrailers.Controls(i), DataGridView)
                    Dim intWidth As Integer = 0
                    For Each nColumn As DataGridViewColumn In nDataGridView.Columns
                        intWidth += nColumn.Width
                    Next
                    nDataGridView.Width = intWidth + 1
                End If
            Next
            tblTrailers.ResumeLayout()
            _SGVWidthCalculated = True
        End If
    End Sub

    Private Sub Setup()
        With Master.eLang
            lblKeepExisting.Text = .GetString(971, "Keep existing")
            lblDefaultSearchParameter.Text = String.Concat(.GetString(1172, "Default Search Parameter"), ":")
            lblMinimumQuality.Text = String.Concat(.GetString(1027, "Minimum Quality"), ":")
            lblPreferredQuality.Text = String.Concat(.GetString(800, "Preferred Quality"), ":")
            lblScraper.Text = "Scraper"
        End With

        sgvScraper.AddScrapers(Master.ScraperList.FindAll(Function(f) f.ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Trailer)))
    End Sub

    Private Sub EnableApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        cbMinimumQuality.SelectedIndexChanged,
        chkKeepExisting.CheckedChanged,
        txtDefaultSearchParameter.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Load_MovieTrailerQualities()
        Dim items As New Dictionary(Of String, Enums.TrailerVideoQuality) From {
            {Master.eLang.GetString(745, "Any"), Enums.TrailerVideoQuality.Any},
            {"2160p 60fps", Enums.TrailerVideoQuality.HD2160p60fps},
            {"2160p", Enums.TrailerVideoQuality.HD2160p},
            {"1440p", Enums.TrailerVideoQuality.HD1440p},
            {"1080p 60fps", Enums.TrailerVideoQuality.HD1080p60fps},
            {"1080p", Enums.TrailerVideoQuality.HD1080p},
            {"720p 60fps", Enums.TrailerVideoQuality.HD720p60fps},
            {"720p", Enums.TrailerVideoQuality.HD720p},
            {"480p", Enums.TrailerVideoQuality.HQ480p},
            {"360p", Enums.TrailerVideoQuality.SQ360p},
            {"240p", Enums.TrailerVideoQuality.SQ240p},
            {"144p", Enums.TrailerVideoQuality.SQ144p},
            {"144p 15fps", Enums.TrailerVideoQuality.SQ144p15fps}
        }
        cbMinimumQuality.DataSource = items.ToList
        cbMinimumQuality.DisplayMember = "Key"
        cbMinimumQuality.ValueMember = "Value"
        cbPreferredQuality.DataSource = items.ToList
        cbPreferredQuality.DisplayMember = "Key"
        cbPreferredQuality.ValueMember = "Value"
    End Sub

    Private Sub PreferredQuality_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbPreferredQuality.SelectedIndexChanged
        If CType(cbPreferredQuality.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value = Enums.TrailerVideoQuality.Any Then
            cbMinimumQuality.Enabled = False
        Else
            cbMinimumQuality.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Methods

End Class