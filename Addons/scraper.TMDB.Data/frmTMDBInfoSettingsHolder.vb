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

Imports System.IO
Imports EmberAPI
Imports System.Diagnostics

Public Class frmTMDBInfoSettingsHolder

#Region "Events"

	Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

	Public Event SetupNeedsRestart()

#End Region	'Events

#Region "Fields"

    Private _api As String
    Private _language As String
    Private _getadultitems As Boolean

#End Region 'Fields


#Region "Properties"

    Public Property API() As String
        Get
            Return Me._api
        End Get
        Set(ByVal value As String)
            Me._api = value
        End Set
    End Property

    Public Property Lang() As String
        Get
            Return Me._language
        End Get
        Set(ByVal value As String)
            Me._language = value
        End Set
    End Property

    Public Property GetAdultItems() As Boolean
        Get
            Return Me._getadultitems
        End Get
        Set(ByVal value As Boolean)
            Me._getadultitems = value
        End Set
    End Property

#End Region 'Properties


#Region "Methods"
    Private Sub PictureBox2_Click(sender As System.Object, e As System.EventArgs) Handles pbTMDBApiKeyInfo.Click
        If Master.isWindows Then
            Process.Start("http://docs.themoviedb.apiary.io/")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://docs.themoviedb.apiary.io/"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Data_Movie.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUnlockAPI_Click(sender As Object, e As EventArgs) Handles btnUnlockAPI.Click
        Me.lblEMMAPI.Visible = False
        Me.txtTMDBApiKey.Enabled = True
        Me.txtTMDBApiKey.Visible = True
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(cbEnabled.Checked, 0)
    End Sub

    Private Sub chkCrew_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCrew.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCast.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCollection_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCollection.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGenre.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkGetAdult_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGetAdultItems.CheckedChanged
        If Not (_getadultitems = chkGetAdultItems.Checked) Then
            RaiseEvent SetupNeedsRestart()
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMPAA.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRelease.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRuntime.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStudio.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTagline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCountry.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTrailer.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVotes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkYear.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkFallBackEng_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFallBackEng.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTMDBCleanPlotOutline_CheckedChanged(sender As Object, e As EventArgs) Handles chkCleanPlotOutline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbTMDBPrefLanguage_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTMDBPrefLanguage.SelectedIndexChanged
        If Not (_language = cbTMDBPrefLanguage.Text) Then
            RaiseEvent SetupNeedsRestart()
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTMDBApiKey_TextEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTMDBApiKey.Enter
        _api = txtTMDBApiKey.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTMDBApiKey_TextValidated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTMDBApiKey.Validated
        If Not (_api = txtTMDBApiKey.Text) Then
            RaiseEvent SetupNeedsRestart()
        End If
    End Sub

    Public Sub New()
        _api = String.Empty
        _language = String.Empty
        _getadultitems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False)
        InitializeComponent()
        Me.SetUp()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Data_Movie.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Data_Movie.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        Me.lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager API key")
        Me.Label1.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), vbCrLf)
        Me.cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkCast.Text = Master.eLang.GetString(63, "Cast")
        Me.chkCleanPlotOutline.Text = Master.eLang.GetString(985, "Clean Plot/Outline")
        Me.chkCollection.Text = Master.eLang.GetString(1135, "Collection")
        Me.chkCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkCrew.Text = Master.eLang.GetString(909, "Crew")
        Me.chkFallBackEng.Text = Master.eLang.GetString(922, "Fall back on english")
        Me.chkGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkGetAdultItems.Text = Master.eLang.GetString(1046, "Include Adult Items")
        Me.chkMPAA.Text = Master.eLang.GetString(881, "MPAA & Certification")
        Me.chkPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkRelease.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkTagline.Text = Master.eLang.GetString(397, "Tagline")
        Me.chkTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkVotes.Text = Master.eLang.GetString(399, "Votes")
        Me.chkYear.Text = Master.eLang.GetString(278, "Year")
        Me.gbTMDBGlobalOpts.Text = Master.eLang.GetString(937, "TMDB")
        Me.gbTMDBScraperOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        Me.lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.lblTMDBApiKey.Text = Master.eLang.GetString(870, "TMDB API Key")
        Me.lblTMDBPrefLanguage.Text = Master.eLang.GetString(741, "Preferred Language:")
    End Sub


#End Region 'Methods
 
End Class