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

Imports System.IO
Imports EmberAPI

Public Class frmTVMediaSettingsHolder

#Region "Fields"

    Private tLangList As New List(Of Containers.TVLanguage)

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupPostScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalTVScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ScraperOrder
        If order < ModulesManager.Instance.externalTVScrapersModules.Where(Function(y) y.ProcessorModule.IsScraper).Count - 1 Then
            ModulesManager.Instance.externalTVScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order + 1).ScraperOrder = order
            ModulesManager.Instance.externalTVScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ScraperOrder = order + 1
            RaiseEvent SetupPostScraperChanged(cbEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalTVScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ScraperOrder
        If order > 0 Then
            ModulesManager.Instance.externalTVScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order - 1).ScraperOrder = order
            ModulesManager.Instance.externalTVScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ScraperOrder = order - 1
            RaiseEvent SetupPostScraperChanged(cbEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent SetupPostScraperChanged(cbEnabled.Checked, 0)
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
        orderChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalTVScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ScraperOrder
        If ModulesManager.Instance.externalTVScrapersModules.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalTVScrapersModules.Where(Function(y) y.ProcessorModule.IsPostScraper).Count - 1)
            btnUp.Enabled = (order > 1)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Dim cLang As Containers.TVLanguage
        Dim xmlTVDB As XDocument
        Try
            xmlTVDB = XDocument.Parse(My.Resources.languages)
            Dim xLangs = From xLanguages In xmlTVDB.Descendants("Language")

            For Each xL As XElement In xLangs
                cLang = New Containers.TVLanguage
                cLang.LongLang = xL.Element("name").Value
                cLang.ShortLang = xL.Element("abbreviation").Value
                tLangList.Add(cLang)
            Next
        Catch
        End Try

        Me.Label2.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.Label18.Text = Master.eLang.GetString(932, "TVDB API Key")
        Me.Label1.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), vbCrLf)
        Me.chkOnlyTVImagesLanguage.Text = Master.eLang.GetString(736, "Only Get Images for the Selected Language")
        Me.chkGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        Me.gbLanguage.Text = Master.eLang.GetString(610, "Language")
        Me.lblTVDBMirror.Text = Master.eLang.GetString(801, "TVDB Mirror")
        Me.tLangList.Clear()
        Me.cbTVLanguage.Items.AddRange((From lLang In tLangList Select lLang.LongLang).ToArray)
    End Sub

    Private Sub txtTMDBApiKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVDBApiKey.TextChanged
        RaiseEvent ModuleSettingsChanged()
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

    Private Sub btnTVLanguageFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.tLangList.Clear()
        Me.tLangList.AddRange(ModulesManager.Instance.TVGetLangs(Master.eSettings.TVDBMirror))
        Me.cbTVLanguage.Items.AddRange((From lLang In tLangList Select lLang.LongLang).ToArray)

        If Me.cbTVLanguage.Items.Count > 0 Then
            Me.cbTVLanguage.Text = Me.tLangList.FirstOrDefault(Function(l) l.ShortLang = Master.eSettings.TVDBLanguage).LongLang
        End If
    End Sub

    Private Sub chkOnlyTVImagesLanguage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnlyTVImagesLanguage.CheckedChanged

        Me.chkGetEnglishImages.Enabled = Me.chkOnlyTVImagesLanguage.Checked

        If Not Me.chkOnlyTVImagesLanguage.Checked Then Me.chkGetEnglishImages.Checked = False
    End Sub

#End Region 'Methods

End Class