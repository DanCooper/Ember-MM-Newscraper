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

#Region "Event Methods"

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

#End Region 'Event Methods

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
        With Master.eSettings
            .MovieTrailerDefaultSearch = txtMovieTrailerDefaultSearch.Text
            .MovieTrailerKeepExisting = chkMovieTrailerKeepExisting.Checked
            .MovieTrailerMinVideoQual = CType(cbMovieTrailerMinVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
            .MovieTrailerPrefVideoQual = CType(cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            cbMovieTrailerMinVideoQual.SelectedValue = .MovieTrailerMinVideoQual
            cbMovieTrailerPrefVideoQual.SelectedValue = .MovieTrailerPrefVideoQual
            chkMovieTrailerKeepExisting.Checked = .MovieTrailerKeepExisting
            txtMovieTrailerDefaultSearch.Text = .MovieTrailerDefaultSearch

            LoadMovieTrailerQualities()
        End With
    End Sub

    Private Sub Setup()
        chkMovieTrailerKeepExisting.Text = Master.eLang.GetString(971, "Keep existing")
        lblMovieTrailerDefaultSearch.Text = Master.eLang.GetString(1172, "Default Search Parameter:")
        lblMovieTrailerMinQual.Text = Master.eLang.GetString(1027, "Minimum Quality:")
        lblMovieTrailerPrefQual.Text = Master.eLang.GetString(800, "Preferred Quality:")
    End Sub

    Private Sub cbMovieTrailerPrefVideoQual_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If CType(cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.TrailerVideoQuality)).Value = Enums.TrailerVideoQuality.Any Then
            cbMovieTrailerMinVideoQual.Enabled = False
        Else
            cbMovieTrailerMinVideoQual.Enabled = True
        End If
        Handle_SettingsChanged()
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

#End Region 'Methods

End Class