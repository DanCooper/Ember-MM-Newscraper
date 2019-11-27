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

Public Class frmMovie_Data
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Private MovieMeta As New List(Of Settings.MetadataPerType)

#End Region 'Fields

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_TV
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

    Private Sub Handle_NeedsDBUpdate_Movie()
        RaiseEvent NeedsDBUpdate_Movie()
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV()
        RaiseEvent NeedsDBUpdate_TV()
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
            .Contains = Enums.SettingsPanelType.MovieData,
            .ImageIndex = 3,
            .Order = 400,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movie_Data",
            .Title = Master.eLang.GetString(556, "NFO"),
            .Type = Enums.SettingsPanelType.Movie
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings
            .MovieLockActors = chkMovieLockActors.Checked
            .MovieLockCert = chkMovieLockCert.Checked
            .MovieLockCollectionID = chkMovieLockCollectionID.Checked
            .MovieLockCollections = chkMovieLockCollections.Checked
            .MovieLockCountry = chkMovieLockCountry.Checked
            .MovieLockDirector = chkMovieLockDirector.Checked
            .MovieLockGenre = chkMovieLockGenre.Checked
            .MovieLockLanguageA = chkMovieLockLanguageA.Checked
            .MovieLockLanguageV = chkMovieLockLanguageV.Checked
            .MovieLockMPAA = chkMovieLockMPAA.Checked
            .MovieLockOutline = chkMovieLockOutline.Checked
            .MovieLockPlot = chkMovieLockPlot.Checked
            .MovieLockRating = chkMovieLockRating.Checked
            .MovieLockReleaseDate = chkMovieLockReleaseDate.Checked
            .MovieLockRuntime = chkMovieLockRuntime.Checked
            .MovieLockStudio = chkMovieLockStudio.Checked
            .MovieLockTags = chkMovieLockTags.Checked
            .MovieLockTagline = chkMovieLockTagline.Checked
            .MovieLockOriginalTitle = chkMovieLockOriginalTitle.Checked
            .MovieLockTitle = chkMovieLockTitle.Checked
            .MovieLockTop250 = chkMovieLockTop250.Checked
            .MovieLockTrailer = chkMovieLockTrailer.Checked
            .MovieLockUserRating = chkMovieLockUserRating.Checked
            .MovieLockCredits = chkMovieLockCredits.Checked
            .MovieLockYear = chkMovieLockYear.Checked
            .MovieScraperCast = chkMovieScraperCast.Checked
            Integer.TryParse(txtMovieScraperCastLimit.Text, .MovieScraperCastLimit)
            .MovieScraperCastWithImgOnly = chkMovieScraperCastWithImg.Checked
            .MovieScraperCert = chkMovieScraperCert.Checked
            .MovieScraperCertForMPAA = chkMovieScraperCertForMPAA.Checked
            .MovieScraperCertForMPAAFallback = chkMovieScraperCertForMPAAFallback.Checked
            .MovieScraperCertOnlyValue = chkMovieScraperCertOnlyValue.Checked
            If Not String.IsNullOrEmpty(cbMovieScraperCertLang.Text) Then
                If cbMovieScraperCertLang.SelectedIndex = 0 Then
                    .MovieScraperCertLang = Master.eLang.All
                Else
                    .MovieScraperCertLang = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.name = cbMovieScraperCertLang.Text).abbreviation
                End If
            End If
            .MovieMetadataPerFileType.Clear()
            .MovieMetadataPerFileType.AddRange(MovieMeta)
            .MovieScraperCleanFields = chkMovieScraperCleanFields.Checked
            .MovieScraperCleanPlotOutline = chkMovieScraperCleanPlotOutline.Checked
            .MovieScraperCollectionID = chkMovieScraperCollectionID.Checked
            .MovieScraperCollectionsAuto = chkMovieScraperCollectionsAuto.Checked
            .MovieScraperCollectionsExtendedInfo = chkMovieScraperCollectionsExtendedInfo.Checked
            .MovieScraperCollectionsYAMJCompatibleSets = chkMovieScraperCollectionsYAMJCompatibleSets.Checked
            .MovieScraperCountry = chkMovieScraperCountry.Checked
            Integer.TryParse(txtMovieScraperCountryLimit.Text, .MovieScraperCountryLimit)
            .MovieScraperDirector = chkMovieScraperDirector.Checked
            .MovieScraperDurationRuntimeFormat = txtMovieScraperDurationRuntimeFormat.Text
            .MovieScraperGenre = chkMovieScraperGenre.Checked
            Integer.TryParse(txtMovieScraperGenreLimit.Text, .MovieScraperGenreLimit)
            .MovieScraperMetaDataIFOScan = chkMovieScraperMetaDataIFOScan.Checked
            .MovieScraperMetaDataScan = chkMovieScraperMetaDataScan.Checked
            .MovieScraperMPAA = chkMovieScraperMPAA.Checked
            .MovieScraperMPAANotRated = txtMovieScraperMPAANotRated.Text
            .MovieScraperOriginalTitle = chkMovieScraperOriginalTitle.Checked
            .MovieScraperOriginalTitleAsTitle = chkMovieScraperOriginalTitleAsTitle.Checked
            .MovieScraperOutline = chkMovieScraperOutline.Checked
            If Not String.IsNullOrEmpty(txtMovieScraperOutlineLimit.Text) Then
                .MovieScraperOutlineLimit = Convert.ToInt32(txtMovieScraperOutlineLimit.Text)
            Else
                .MovieScraperOutlineLimit = 0
            End If
            .MovieScraperPlot = chkMovieScraperPlot.Checked
            .MovieScraperPlotForOutline = chkMovieScraperPlotForOutline.Checked
            .MovieScraperPlotForOutlineIfEmpty = chkMovieScraperPlotForOutlineIfEmpty.Checked
            .MovieScraperRating = chkMovieScraperRating.Checked
            .MovieScraperRelease = chkMovieScraperRelease.Checked
            .MovieScraperRuntime = chkMovieScraperRuntime.Checked
            .MovieScraperStudio = chkMovieScraperStudio.Checked
            .MovieScraperStudioWithImgOnly = chkMovieScraperStudioWithImg.Checked
            Integer.TryParse(txtMovieScraperStudioLimit.Text, .MovieScraperStudioLimit)
            .MovieScraperTagline = chkMovieScraperTagline.Checked
            .MovieScraperTitle = chkMovieScraperTitle.Checked
            .MovieScraperTop250 = chkMovieScraperTop250.Checked
            .MovieScraperTrailer = chkMovieScraperTrailer.Checked
            .MovieScraperUserRating = chkMovieScraperUserRating.Checked
            .MovieScraperUseDetailView = chkMovieScraperDetailView.Checked
            .MovieScraperUseMDDuration = chkMovieScraperUseMDDuration.Checked
            .MovieScraperCredits = chkMovieScraperCredits.Checked
            .MovieScraperXBMCTrailerFormat = chkMovieScraperXBMCTrailerFormat.Checked
            .MovieScraperYear = chkMovieScraperYear.Checked
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Private Sub btnMovieScraperDefFIExtAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not txtMovieScraperDefFIExt.Text.StartsWith(".") Then txtMovieScraperDefFIExt.Text = String.Concat(".", txtMovieScraperDefFIExt.Text)
        Using dFileInfo As New dlgFileInfo(New MediaContainers.FileInfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                Dim fi = dFileInfo.Result
                If Not fi Is Nothing Then
                    Dim m As New Settings.MetadataPerType With {
                        .FileType = txtMovieScraperDefFIExt.Text,
                        .MetaData = fi
                    }
                    MovieMeta.Add(m)
                    LoadMovieMetadata()
                    txtMovieScraperDefFIExt.Text = String.Empty
                    txtMovieScraperDefFIExt.Focus()
                    Handle_SettingsChanged()
                End If
            End If
        End Using
    End Sub

    Private Sub btnMovieScraperDefFIExtEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each tMetadata As Settings.MetadataPerType In MovieMeta
                If tMetadata.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    Using dFileInfo As New dlgFileInfo(tMetadata.MetaData)
                        If dFileInfo.ShowDialog = DialogResult.OK Then
                            tMetadata.MetaData = dFileInfo.Result
                            LoadMovieMetadata()
                            Handle_SettingsChanged()
                        End If
                    End Using
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnMovieScraperDefFIExtRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        RemoveMovieMetaData()
    End Sub

    Private Sub chkMovieScraperStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        chkMovieScraperStudioWithImg.Enabled = chkMovieScraperStudio.Checked
        txtMovieScraperStudioLimit.Enabled = chkMovieScraperStudio.Checked
        If Not chkMovieScraperStudio.Checked Then
            chkMovieScraperStudioWithImg.Checked = False
            txtMovieScraperStudioLimit.Text = "0"
        End If
    End Sub

    Private Sub chkMovieScraperCountry_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        txtMovieScraperCountryLimit.Enabled = chkMovieScraperCountry.Checked
        If Not chkMovieScraperCountry.Checked Then txtMovieScraperCountryLimit.Text = "0"
    End Sub

    Private Sub chkMovieScraperCast_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        chkMovieScraperCastWithImg.Enabled = chkMovieScraperCast.Checked
        txtMovieScraperCastLimit.Enabled = chkMovieScraperCast.Checked

        If Not chkMovieScraperCast.Checked Then
            chkMovieScraperCastWithImg.Checked = False
            txtMovieScraperCastLimit.Text = "0"
        End If
    End Sub

    Private Sub chkMovieScraperCert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        If Not chkMovieScraperCert.Checked Then
            cbMovieScraperCertLang.Enabled = False
            cbMovieScraperCertLang.SelectedIndex = 0
            chkMovieScraperCertForMPAA.Enabled = False
            chkMovieScraperCertForMPAA.Checked = False
            chkMovieScraperCertOnlyValue.Enabled = False
            chkMovieScraperCertOnlyValue.Checked = False
        Else
            cbMovieScraperCertLang.Enabled = True
            cbMovieScraperCertLang.SelectedIndex = 0
            chkMovieScraperCertForMPAA.Enabled = True
            chkMovieScraperCertOnlyValue.Enabled = True
        End If
    End Sub

    Private Sub chkMovieScraperCertForMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        If Not chkMovieScraperCertForMPAA.Checked Then
            chkMovieScraperCertForMPAAFallback.Enabled = False
            chkMovieScraperCertForMPAAFallback.Checked = False
        Else
            chkMovieScraperCertForMPAAFallback.Enabled = True
        End If
    End Sub

    Private Sub chkMovieScraperGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        txtMovieScraperGenreLimit.Enabled = chkMovieScraperGenre.Checked
        If Not chkMovieScraperGenre.Checked Then txtMovieScraperGenreLimit.Text = "0"
    End Sub

    Private Sub chkMovieScraperPlotForOutline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieScraperOutlineLimit.Enabled = chkMovieScraperPlotForOutline.Checked
        chkMovieScraperPlotForOutlineIfEmpty.Enabled = chkMovieScraperPlotForOutline.Checked
        If Not chkMovieScraperPlotForOutline.Checked Then
            txtMovieScraperOutlineLimit.Enabled = False
            chkMovieScraperPlotForOutlineIfEmpty.Checked = False
            chkMovieScraperPlotForOutlineIfEmpty.Enabled = False
        End If
    End Sub

    Private Sub chkMovieScraperOriginalTitle_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()

        chkMovieScraperOriginalTitleAsTitle.Enabled = chkMovieScraperOriginalTitle.Checked
        If Not chkMovieScraperOriginalTitle.Checked Then chkMovieScraperOriginalTitleAsTitle.Checked = False
    End Sub

    Private Sub chkMovieScraperPlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        chkMovieScraperPlotForOutline.Enabled = chkMovieScraperPlot.Checked
        If Not chkMovieScraperPlot.Checked Then
            chkMovieScraperPlotForOutline.Checked = False
            txtMovieScraperOutlineLimit.Enabled = False
        End If
    End Sub

    Private Sub chkMovieScraperCollectionID_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        chkMovieScraperCollectionsAuto.Enabled = chkMovieScraperCollectionID.Checked
        If Not chkMovieScraperCollectionID.Checked Then
            chkMovieScraperCollectionsAuto.Checked = False
        End If
    End Sub

    Private Sub chkMovieScraperUseMDDuration_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        txtMovieScraperDurationRuntimeFormat.Enabled = chkMovieScraperUseMDDuration.Checked
        Handle_SettingsChanged()
    End Sub

    Private Sub LoadMovieMetadata()
        lstMovieScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In MovieMeta
            lstMovieScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Public Sub Settings_Load()
        With Master.eSettings
            chkMovieLockActors.Checked = .MovieLockActors
            chkMovieLockCert.Checked = .MovieLockCert
            chkMovieLockCountry.Checked = .MovieLockCountry
            chkMovieLockCollectionID.Checked = .MovieLockCollectionID
            chkMovieLockCollections.Checked = .MovieLockCollections
            chkMovieLockDirector.Checked = .MovieLockDirector
            chkMovieLockGenre.Checked = .MovieLockGenre
            chkMovieLockLanguageA.Checked = .MovieLockLanguageA
            chkMovieLockLanguageV.Checked = .MovieLockLanguageV
            chkMovieLockMPAA.Checked = .MovieLockMPAA
            chkMovieLockOriginalTitle.Checked = .MovieLockOriginalTitle
            chkMovieLockOutline.Checked = .MovieLockOutline
            chkMovieLockPlot.Checked = .MovieLockPlot
            chkMovieLockRating.Checked = .MovieLockRating
            chkMovieLockReleaseDate.Checked = .MovieLockReleaseDate
            chkMovieLockRuntime.Checked = .MovieLockRuntime
            chkMovieLockStudio.Checked = .MovieLockStudio
            chkMovieLockTagline.Checked = .MovieLockTagline
            chkMovieLockTags.Checked = .MovieLockTags
            chkMovieLockTitle.Checked = .MovieLockTitle
            chkMovieLockUserRating.Checked = .MovieLockUserRating
            chkMovieLockTop250.Checked = .MovieLockTop250
            chkMovieLockTrailer.Checked = .MovieLockTrailer
            chkMovieLockCredits.Checked = .MovieLockCredits
            chkMovieLockYear.Checked = .MovieLockYear
            chkMovieScraperCast.Checked = .MovieScraperCast
            chkMovieScraperCastWithImg.Checked = .MovieScraperCastWithImgOnly
            chkMovieScraperCert.Checked = .MovieScraperCert
            chkMovieScraperCertForMPAA.Checked = .MovieScraperCertForMPAA
            chkMovieScraperCertForMPAAFallback.Checked = .MovieScraperCertForMPAAFallback
            chkMovieScraperCertOnlyValue.Checked = .MovieScraperCertOnlyValue
            chkMovieScraperCleanFields.Checked = .MovieScraperCleanFields
            chkMovieScraperCleanPlotOutline.Checked = .MovieScraperCleanPlotOutline
            chkMovieScraperCollectionID.Checked = .MovieScraperCollectionID
            chkMovieScraperCollectionsAuto.Checked = .MovieScraperCollectionsAuto
            chkMovieScraperCollectionsExtendedInfo.Checked = .MovieScraperCollectionsExtendedInfo
            chkMovieScraperCollectionsYAMJCompatibleSets.Checked = .MovieScraperCollectionsYAMJCompatibleSets
            chkMovieScraperCountry.Checked = .MovieScraperCountry
            chkMovieScraperDirector.Checked = .MovieScraperDirector
            chkMovieScraperGenre.Checked = .MovieScraperGenre
            chkMovieScraperMetaDataIFOScan.Checked = .MovieScraperMetaDataIFOScan
            chkMovieScraperMetaDataScan.Checked = .MovieScraperMetaDataScan
            chkMovieScraperMPAA.Checked = .MovieScraperMPAA
            chkMovieScraperOriginalTitle.Checked = .MovieScraperOriginalTitle
            chkMovieScraperOriginalTitleAsTitle.Checked = .MovieScraperOriginalTitleAsTitle
            chkMovieScraperDetailView.Checked = .MovieScraperUseDetailView
            chkMovieScraperOutline.Checked = .MovieScraperOutline
            chkMovieScraperPlot.Checked = .MovieScraperPlot
            chkMovieScraperPlotForOutline.Checked = .MovieScraperPlotForOutline
            chkMovieScraperPlotForOutlineIfEmpty.Checked = .MovieScraperPlotForOutlineIfEmpty
            chkMovieScraperRating.Checked = .MovieScraperRating
            chkMovieScraperRelease.Checked = .MovieScraperRelease
            chkMovieScraperRuntime.Checked = .MovieScraperRuntime
            chkMovieScraperStudio.Checked = .MovieScraperStudio
            chkMovieScraperStudioWithImg.Checked = .MovieScraperStudioWithImgOnly
            chkMovieScraperTagline.Checked = .MovieScraperTagline
            chkMovieScraperTitle.Checked = .MovieScraperTitle
            chkMovieScraperUserRating.Checked = .MovieScraperUserRating
            chkMovieScraperTop250.Checked = .MovieScraperTop250
            chkMovieScraperTrailer.Checked = .MovieScraperTrailer
            chkMovieScraperUseMDDuration.Checked = .MovieScraperUseMDDuration
            chkMovieScraperCredits.Checked = .MovieScraperCredits
            chkMovieScraperXBMCTrailerFormat.Checked = .MovieScraperXBMCTrailerFormat
            chkMovieScraperYear.Checked = .MovieScraperYear
            txtMovieScraperCastLimit.Text = .MovieScraperCastLimit.ToString
            txtMovieScraperCountryLimit.Text = .MovieScraperCountryLimit.ToString
            txtMovieScraperDurationRuntimeFormat.Text = .MovieScraperDurationRuntimeFormat
            txtMovieScraperGenreLimit.Text = .MovieScraperGenreLimit.ToString
            txtMovieScraperMPAANotRated.Text = .MovieScraperMPAANotRated
            txtMovieScraperOutlineLimit.Text = .MovieScraperOutlineLimit.ToString
            txtMovieScraperStudioLimit.Text = .MovieScraperStudioLimit.ToString
            txtMovieScraperDurationRuntimeFormat.Enabled = .MovieScraperUseMDDuration

            Try
                cbMovieScraperCertLang.Items.Clear()
                cbMovieScraperCertLang.Items.Add(Master.eLang.All)
                cbMovieScraperCertLang.Items.AddRange((From lLang In APIXML.CertLanguagesXML.Language Select lLang.name).ToArray)
                If cbMovieScraperCertLang.Items.Count > 0 Then
                    If .MovieScraperCertLang = Master.eLang.All Then
                        cbMovieScraperCertLang.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.MovieScraperCertLang) Then
                        Dim tLanguage As CertLanguages = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = .MovieScraperCertLang)
                        If tLanguage IsNot Nothing AndAlso tLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLanguage.name) Then
                            cbMovieScraperCertLang.Text = tLanguage.name
                        Else
                            cbMovieScraperCertLang.SelectedIndex = 0
                        End If
                    Else
                        cbMovieScraperCertLang.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
                'logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
            MovieMeta.AddRange(.MovieMetadataPerFileType)
        End With

        LoadMovieMetadata()
    End Sub

    Private Sub lstMovieScraperDefFIExt_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            btnMovieScraperDefFIExtEdit.Enabled = True
            btnMovieScraperDefFIExtRemove.Enabled = True
            txtMovieScraperDefFIExt.Text = String.Empty
        Else
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub RemoveMovieMetaData()
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In MovieMeta
                If x.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    MovieMeta.Remove(x)
                    LoadMovieMetadata()
                    Handle_SettingsChanged()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub lstMovieScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then RemoveMovieMetaData()
    End Sub

    Private Sub Setup()
        lblMovieScraperGlobalActors.Text = Master.eLang.GetString(231, "Actors")
        gbMovieScraperCertificationOpts.Text = Master.eLang.GetString(56, "Certifications")
        lblMovieScraperGlobalCertifications.Text = Master.eLang.GetString(56, "Certifications")
        chkMovieScraperCleanFields.Text = Master.eLang.GetString(125, "Cleanup disabled fields")
        lblMovieScraperGlobalCollectionID.Text = Master.eLang.GetString(1135, "Collection ID")
        lblMovieScraperGlobalCollections.Text = Master.eLang.GetString(424, "Collections")
        lblMovieScraperGlobalCountries.Text = Master.eLang.GetString(237, "Countries")
        gbMovieScraperDefFIExtOpts.Text = Master.eLang.GetString(625, "Defaults by File Type")
        lblMovieScraperGlobalDirectors.Text = Master.eLang.GetString(940, "Directors")
        gbMovieScraperDurationFormatOpts.Text = Master.eLang.GetString(515, "Duration Format")
        lblMovieScraperDurationRuntimeFormat.Text = Master.eLang.GetString(515, "Duration Format")
        lblMovieScraperDefFIExt.Text = String.Concat(Master.eLang.GetString(626, "File Type"), ":")
        lblMovieScraperGlobalGenres.Text = Master.eLang.GetString(725, "Genres")
        lblMovieScraperGlobalLanguageA.Text = Master.eLang.GetString(431, "Language (Audio)")
        lblMovieScraperGlobalLanguageV.Text = Master.eLang.GetString(435, "Language (Video)")
        lblMovieScraperGlobalHeaderLimit.Text = Master.eLang.GetString(578, "Limit")
        lblMovieScraperOutlineLimit.Text = String.Concat(Master.eLang.GetString(578, "Limit"), ":")
        lblMovieScraperGlobalHeaderLock.Text = Master.eLang.GetString(24, "Lock")
        gbMovieScraperMetaDataOpts.Text = Master.eLang.GetString(59, "Meta Data")
        gbMovieScraperMiscOpts.Text = Master.eLang.GetString(429, "Miscellaneous")
        lblMovieScraperGlobalMPAA.Text = Master.eLang.GetString(401, "MPAA")
        chkMovieScraperCertForMPAAFallback.Text = Master.eLang.GetString(1293, "Only if no MPAA is found")
        chkMovieScraperCertOnlyValue.Text = Master.eLang.GetString(835, "Only Save the Value to NFO")
        lblMovieScraperGlobalOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        lblMovieScraperGlobalPlot.Text = Master.eLang.GetString(65, "Plot")
        lblMovieScraperGlobalOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        lblMovieScraperGlobalRating.Text = Master.eLang.GetString(400, "Rating")
        lblMovieScraperGlobalReleaseDate.Text = Master.eLang.GetString(57, "Release Date")
        lblMovieScraperGlobalRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkMovieScraperCollectionsExtendedInfo.Text = Master.eLang.GetString(1075, "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)")
        chkMovieScraperCastWithImg.Text = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        gbMovieScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields - Global")
        lblMovieScraperGlobalStudios.Text = Master.eLang.GetString(226, "Studios")
        lblMovieScraperGlobalTagline.Text = Master.eLang.GetString(397, "Tagline")
        lblMovieScraperGlobalTitle.Text = Master.eLang.GetString(21, "Title")
        lblMovieScraperGlobalTop250.Text = Master.eLang.GetString(591, "Top 250")
        lblMovieScraperGlobalTrailer.Text = Master.eLang.GetString(151, "Trailer")
        chkMovieScraperCertForMPAA.Text = Master.eLang.GetString(511, "Use Certification for MPAA")
        chkMovieScraperOriginalTitleAsTitle.Text = Master.eLang.GetString(240, "Use Original Title as Title")
        lblMovieScraperGlobalUserRating.Text = Master.eLang.GetString(1467, "User Rating")
        lblMovieScraperGlobalCredits.Text = Master.eLang.GetString(394, "Writers")
        lblMovieScraperGlobalYear.Text = Master.eLang.GetString(278, "Year")
        chkMovieScraperCleanPlotOutline.Text = Master.eLang.GetString(985, "Clean Plot/Outline")
        chkMovieScraperCollectionsAuto.Text = Master.eLang.GetString(1266, "Add Movie automatically to Collections")
        chkMovieScraperDetailView.Text = Master.eLang.GetString(1249, "Show scraped results in detailed view")
        chkMovieScraperMetaDataIFOScan.Text = Master.eLang.GetString(628, "Enable IFO Parsing")
        chkMovieScraperMetaDataScan.Text = Master.eLang.GetString(517, "Scan Meta Data")
        chkMovieScraperPlotForOutline.Text = Master.eLang.GetString(965, "Use Plot for Plot Outline")
        chkMovieScraperPlotForOutlineIfEmpty.Text = Master.eLang.GetString(958, "Only if Plot Outline is empty")
        chkMovieScraperStudioWithImg.Text = Master.eLang.GetString(1280, "Scrape Only Studios With Images")
        chkMovieScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        chkMovieScraperXBMCTrailerFormat.Text = Master.eLang.GetString(1187, "Save YouTube-Trailer-Links in XBMC compatible format")
        chkMovieScraperCollectionsYAMJCompatibleSets.Text = Master.eLang.GetString(561, "Save YAMJ Compatible Sets to NFO")
        gbMovieScraperDefFIExtOpts.Text = Master.eLang.GetString(625, "Defaults by File Type")
        lblMovieScraperDurationRuntimeFormat.Text = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
        lblMovieScraperMPAANotRated.Text = String.Concat(Master.eLang.GetString(832, "MPAA value if no rating is available"), ":")
    End Sub

    Private Sub txtMovieScraperDefFIExt_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        btnMovieScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtMovieScraperDefFIExt.Text) AndAlso Not lstMovieScraperDefFIExt.Items.Contains(If(txtMovieScraperDefFIExt.Text.StartsWith("."), txtMovieScraperDefFIExt.Text, String.Concat(".", txtMovieScraperDefFIExt.Text)))
        If btnMovieScraperDefFIExtAdd.Enabled Then
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class