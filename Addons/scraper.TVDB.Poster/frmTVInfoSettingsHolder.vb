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

Public Class frmTVInfoSettingsHolder

#Region "Fields"

    Private _api As String

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

    Public Property API() As String
        Get
            Return Me._api
        End Get
        Set(ByVal value As String)
            Me._api = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"
    Public Sub New()
        _api = String.Empty
        InitializeComponent()
        Me.SetUp()
        orderChanged()
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsScraper).Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbTVLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVScraperLanguage.SelectedIndexChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(cbEnabled.Checked, 0)
    End Sub

    Private Sub chkScraperEpActors_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpActors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpAired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpAired.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpCredits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpCredits.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpDirector.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpEpisode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpEpisode.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpSeason.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperEpTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowActors_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowActors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowEGU_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowEGU.CheckedChanged
        If String.IsNullOrEmpty(txtTVDBApiKey.Text) AndAlso chkScraperShowEGU.Checked Then
            MsgBox(Master.eLang.GetString(1133, "You need your own API key for that"), MsgBoxStyle.OkOnly, Master.eLang.GetString(1134, "Error"))
            chkScraperShowEGU.Checked = False
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowGenre.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowMPAA.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowPremiered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowPremiered.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowStatus.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowStudio.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScraperShowTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_TV.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsScraper).Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub pbTVDB_Click(sender As System.Object, e As System.EventArgs) Handles pbTVDB.Click
        If Master.isWindows Then
            Process.Start("http://thetvdb.com/?tab=apiregister")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://thetvdb.com/?tab=apiregister"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub SetUp()
        Me.lblScrapeOrder.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.lblTVDBApiKey.Text = Master.eLang.GetString(932, "TVDB API Key")
        Me.lblModuleInfo.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), vbCrLf)
        Me.gbLanguage.Text = Master.eLang.GetString(610, "Language")
        Me.lblTVLanguagePreferred.Text = Master.eLang.GetString(741, "Preferred Language:")
        Me.gbTMDB.Text = Master.eLang.GetString(941, "TVDB")
        Me.gbScraperFields.Text = Master.eLang.GetString(577, "Scraper Fields - Scraper specific")
        Me.gbScraperFieldsShow.Text = Master.eLang.GetString(743, "Show")
        Me.gbScraperFieldsEpisode.Text = Master.eLang.GetString(727, "Episode")
        Me.lblTVDBMirror.Text = Master.eLang.GetString(801, "TVDB Mirror")
        Me.cbTVScraperLanguage.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)
        Me.chkScraperEpActors.Text = Master.eLang.GetString(725, "Actors")
        Me.chkScraperEpAired.Text = Master.eLang.GetString(728, "Aired")
        Me.chkScraperEpCredits.Text = Master.eLang.GetString(729, "Credits")
        Me.chkScraperEpDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkScraperEpEpisode.Text = Master.eLang.GetString(755, "Episode #")
        Me.chkScraperEpPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkScraperEpRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkScraperEpSeason.Text = Master.eLang.GetString(650, "Season")
        Me.chkScraperEpTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkScraperShowActors.Text = Master.eLang.GetString(725, "Actors")
        Me.chkScraperShowEGU.Text = Master.eLang.GetString(723, "Episode Guide URL")
        Me.chkScraperShowGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkScraperShowMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        Me.chkScraperShowRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkScraperShowStatus.Text = Master.eLang.GetString(215, "Status")
        Me.chkScraperShowStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        Me.lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager API key")
    End Sub

    Private Sub txtTVDBApiKey_TextEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVDBApiKey.Enter
        _api = txtTVDBApiKey.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTVDBApiKey_TestValidated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVDBApiKey.Validated
        If Not (_api = txtTVDBApiKey.Text) Then
            RaiseEvent SetupNeedsRestart()
        End If
    End Sub

    Private Sub txtTVDBMirror_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVDBMirror.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnUnlockAPI_Click(sender As Object, e As EventArgs) Handles btnUnlockAPI.Click
        Me.lblEMMAPI.Visible = False
        Me.txtTVDBApiKey.Enabled = True
        Me.txtTVDBApiKey.Visible = True
    End Sub

#End Region 'Methods

End Class