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

Public Class frmSettingsPanel_Information_TV
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
        Get
            Return If(IsEnabled, 9, 10)
        End Get
        Set(value As Integer)
            Return
        End Set
    End Property

    Public Property IsEnabled As Boolean Implements Interfaces.ISettingsPanel.IsEnabled
        Get
            Return chkEnabled.Checked
        End Get
        Set(value As Boolean)
            chkEnabled.Checked = value
        End Set
    End Property

    Public ReadOnly Property MainPanel As Panel Implements Interfaces.ISettingsPanel.MainPanel

    Public Property Order As Integer Implements Interfaces.ISettingsPanel.Order

    Public ReadOnly Property Title As String Implements Interfaces.ISettingsPanel.Title

    Public ReadOnly Property ParentType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ParentType

    Public Property UniqueId As String Implements Interfaces.ISettingsPanel.UniqueId

#End Region 'Properties

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        'Set Addon Panel Data
        ChildType = Enums.SettingsPanelType.None
        Image = Nothing
        MainPanel = pnlSettings
        Title = "TheMovieDb.org"
        ParentType = Enums.SettingsPanelType.TVInformation

        Setup()
    End Sub

    Private Sub Setup()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkFallBackEng.Text = Master.eLang.GetString(922, "Fallback to english")
        chkGetAdultItems.Text = Master.eLang.GetString(1046, "Include Adult Items")
        chkScraperEpisodeActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperEpisodeAired.Text = Master.eLang.GetString(728, "Aired")
        chkScraperEpisodeCredits.Text = Master.eLang.GetString(394, "Credits (Writers)")
        chkScraperEpisodeDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkScraperEpisodeGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        chkScraperEpisodePlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperEpisodeRating.Text = Master.eLang.GetString(245, "Rating")
        chkScraperEpisodeTitle.Text = Master.eLang.GetString(21, "Title")
        chkScraperSeasonAired.Text = Master.eLang.GetString(728, "Aired")
        chkScraperSeasonPlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperSeasonTitle.Text = Master.eLang.GetString(21, "Title")
        chkScraperShowActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperShowCertifications.Text = Master.eLang.GetString(56, "Certifications")
        chkScraperShowCountries.Text = Master.eLang.GetString(237, "Countries")
        chkScraperShowCreators.Text = Master.eLang.GetString(744, "Creators")
        chkScraperShowGenres.Text = Master.eLang.GetString(725, "Genres")
        chkScraperShowOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        chkScraperShowRating.Text = Master.eLang.GetString(245, "Rating")
        chkScraperShowRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkScraperShowStatus.Text = Master.eLang.GetString(215, "Status")
        chkScraperShowStudios.Text = Master.eLang.GetString(226, "Studios")
        chkScraperShowTagline.Text = Master.eLang.GetString(397, "Tagline")
        chkScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Dialog Methods

#Region "Interface Methods"

    Public Sub DoDispose() Implements Interfaces.ISettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Sub Addon_Order_Changed(ByVal totalCount As Integer) Implements Interfaces.ISettingsPanel.OrderChanged
        If totalCount > 1 Then
            btnDown.Enabled = (Order < totalCount - 1)
            btnUp.Enabled = (Order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Public Sub SaveSettings() Implements Interfaces.ISettingsPanel.SaveSettings
        Return
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Private Sub Addon_State_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent StateChanged(UniqueId, IsEnabled, 0)
    End Sub

    Private Sub Addon_Order_Down(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        RaiseEvent StateChanged(UniqueId, IsEnabled, 1)
    End Sub

    Private Sub Addon_Order_Up(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        RaiseEvent StateChanged(UniqueId, IsEnabled, -1)
    End Sub

    Private Sub Handle_SettingsChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkFallBackEng.CheckedChanged,
        chkGetAdultItems.CheckedChanged,
        chkScraperEpisodeActors.CheckedChanged,
        chkScraperEpisodeAired.CheckedChanged,
        chkScraperEpisodeCredits.CheckedChanged,
        chkScraperEpisodeDirectors.CheckedChanged,
        chkScraperEpisodeGuestStars.CheckedChanged,
        chkScraperEpisodePlot.CheckedChanged,
        chkScraperEpisodeRating.CheckedChanged,
        chkScraperEpisodeTitle.CheckedChanged,
        chkScraperSeasonAired.CheckedChanged,
        chkScraperSeasonPlot.CheckedChanged,
        chkScraperSeasonTitle.CheckedChanged,
        chkScraperShowActors.CheckedChanged,
        chkScraperShowCertifications.CheckedChanged,
        chkScraperShowCountries.CheckedChanged,
        chkScraperShowCreators.CheckedChanged,
        chkScraperShowGenres.CheckedChanged,
        chkScraperShowOriginalTitle.CheckedChanged,
        chkScraperShowPlot.CheckedChanged,
        chkScraperShowPremiered.CheckedChanged,
        chkScraperShowRating.CheckedChanged,
        chkScraperShowRuntime.CheckedChanged,
        chkScraperShowStatus.CheckedChanged,
        chkScraperShowStudios.CheckedChanged,
        chkScraperShowTagline.CheckedChanged,
        chkScraperShowTitle.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Methods

End Class