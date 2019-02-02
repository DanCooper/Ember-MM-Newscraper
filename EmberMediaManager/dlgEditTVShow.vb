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
Imports NLog

Public Class dlgEditTVShow

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private dragBoxFromMouseDown As Rectangle
    Private lvwActorsSorter As ListViewColumnSorter

    'Extrafanarts/Extrathumbs list settings
    Private iImageList_DistanceLeft As Integer = 1
    Private iImageList_DistanceTop As Integer = 1
    Private iImageList_Location_Image As Point = New Point(2, 2)
    Private iImageList_Location_Resolution As Point = New Point(2, 102)
    Private iImageList_Size_Image As Size = New Size(174, 98)
    Private iImageList_Size_Panel As Size = New Size(180, 119)
    Private iImageList_Size_Resolution As Size = New Size(174, 15)

    'Extrafanarts
    Private iExtrafanartsList_NextTop As Integer = 1
    Private lblExtrafanartsList_Resolution() As Label
    Private pbExtrafanartsList_Image() As PictureBox
    Private pnExtrafanartsList_Panel() As Panel
    Private currExtrafanartsList_Item As MediaContainers.Image

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As Database.DBElement
        Get
            Return tmpDBElement
        End Get
    End Property

#End Region 'Properties

#Region "Dialog"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tmpDBElement, True) Then
            pbBanner.AllowDrop = True
            pbCharacterArt.AllowDrop = True
            pbClearArt.AllowDrop = True
            pbClearLogo.AllowDrop = True
            pbFanart.AllowDrop = True
            pbLandscape.AllowDrop = True
            pbPoster.AllowDrop = True
            pnlExtrafanarts.AllowDrop = True

            Setup()
            lvwActorsSorter = New ListViewColumnSorter()
            lvActors.ListViewItemSorter = lvwActorsSorter

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            Data_Fill()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_Change_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        DialogResult = DialogResult.Abort
    End Sub

    Private Sub DialogResult_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Data_Save()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub DialogResult_Rescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
        DialogResult = DialogResult.Retry
    End Sub

    Private Sub Setup()
        Dim mTitle As String = tmpDBElement.TVShow.Title
        Text = String.Concat(Master.eLang.GetString(663, "Edit Show"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        btnCancel.Text = Master.eLang.Cancel
        btnChange.Text = Master.eLang.GetString(767, "Change TV Show")
        btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        btnOK.Text = Master.eLang.OK
        btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        chkLocked.Text = Master.eLang.GetString(43, "Locked")
        chkMarked.Text = Master.eLang.GetString(48, "Marked")
        chkMarkedCustom1.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
        chkMarkedCustom2.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
        chkMarkedCustom3.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
        chkMarkedCustom4.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
        chkWatched.Text = Master.eLang.GetString(981, "Watched")
        colActorsName.Text = Master.eLang.GetString(232, "Name")
        colActorsRole.Text = Master.eLang.GetString(233, "Role")
        colActorsThumb.Text = Master.eLang.GetString(234, "Thumb")
        colRatingsVotes.Text = Master.eLang.GetString(244, "Votes")
        lblActors.Text = String.Concat(Master.eLang.GetString(231, "Actors"), ":")
        lblBanner.Text = Master.eLang.GetString(838, "Banner")
        lblCertifications.Text = String.Concat(Master.eLang.GetString(56, "Certifications"), ":")
        lblCharacterArt.Text = Master.eLang.GetString(1140, "CharacterArt")
        lblClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        lblClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblCountries.Text = String.Concat(Master.eLang.GetString(237, "Countries"), ":")
        lblCreators.Text = String.Concat(Master.eLang.GetString(744, "Creators"), ":")
        lblEpisodeOrdering.Text = String.Concat(Master.eLang.GetString(739, "Episode Ordering"), ":")
        lblEpisodeSorting.Text = String.Concat(Master.eLang.GetString(364, "Show Episodes by"), ":")
        lblExtrafanarts.Text = String.Format("{0} ({1})", Master.eLang.GetString(992, "Extrafanarts"), pnlExtrafanartsList.Controls.Count)
        lblFanart.Text = Master.eLang.GetString(149, "Fanart")
        lblGenres.Text = Master.eLang.GetString(51, "Genre(s):")
        lblLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        lblLanguage.Text = Master.eLang.GetString(610, "Language")
        lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        lblMPAADesc.Text = Master.eLang.GetString(229, "MPAA Rating Description:")
        lblOriginalTitle.Text = String.Concat(Master.eLang.GetString(302, "Original Title"), ":")
        lblPlot.Text = String.Concat(Master.eLang.GetString(65, "Plot"), ":")
        lblPoster.Text = Master.eLang.GetString(148, "Poster")
        lblPremiered.Text = String.Concat(Master.eLang.GetString(724, "Premiered"), ":")
        lblRatings.Text = String.Concat(Master.eLang.GetString(245, "Ratings"), ":")
        lblRuntime.Text = String.Concat(Master.eLang.GetString(238, "Runtime"), ":")
        lblSortTilte.Text = String.Concat(Master.eLang.GetString(642, "Sort Title"), ":")
        lblStatus.Text = String.Concat(Master.eLang.GetString(215, "Status"), ":")
        lblStudios.Text = String.Concat(Master.eLang.GetString(395, "Studio"), ":")
        lblTitle.Text = String.Concat(Master.eLang.GetString(21, "Title"), ":")
        lblTopDetails.Text = Master.eLang.GetString(664, "Edit the details for the selected show.")
        lblTopTitle.Text = Master.eLang.GetString(663, "Edit Show")
        lblUserRating.Text = String.Concat(Master.eLang.GetString(1467, "User Rating"), ":")
        tpDetails.Text = Master.eLang.GetString(26, "Details")
        tsslFilename.Text = tmpDBElement.ShowPath

        cbEpisodeOrdering.Items.Clear()
        cbEpisodeOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(1067, "DVD"), Master.eLang.GetString(839, "Absolute"), Master.eLang.GetString(1332, "Day Of Year")})

        cbEpisodeSorting.Items.Clear()
        cbEpisodeSorting.Items.AddRange(New String() {Master.eLang.GetString(755, "Episode #"), Master.eLang.GetString(728, "Aired")})

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
    End Sub

    Public Overloads Function ShowDialog(ByVal DBTVShow As Database.DBElement) As DialogResult
        tmpDBElement = DBTVShow
        Return ShowDialog()
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Sub Actors_Add() Handles btnActorsAdd.Click
        Using dAddEditActor As New dlgAddEditActor
            If dAddEditActor.ShowDialog() = DialogResult.OK Then
                Dim nActor As MediaContainers.Person = dAddEditActor.Result
                Dim lvItem As ListViewItem = lvActors.Items.Add(nActor.ID.ToString)
                lvItem.Tag = nActor
                lvItem.SubItems.Add(nActor.Name)
                lvItem.SubItems.Add(nActor.Role)
                lvItem.SubItems.Add(nActor.URLOriginal)
            End If
        End Using
    End Sub

    Private Sub Actors_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActors.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.

        If (e.Column = lvwActorsSorter.SortColumn) Then
            ' Reverse the current sort direction for this column.
            If (lvwActorsSorter.Order = SortOrder.Ascending) Then
                lvwActorsSorter.Order = SortOrder.Descending
            Else
                lvwActorsSorter.Order = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            lvwActorsSorter.SortColumn = e.Column
            lvwActorsSorter.Order = SortOrder.Ascending
        End If

        ' Perform the sort with these new sort options.
        lvActors.Sort()
    End Sub

    Private Sub Actors_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorsDown.Click

    End Sub

    Private Sub Actors_Edit_Click() Handles btnActorsEdit.Click, lvActors.DoubleClick
        If lvActors.SelectedItems.Count > 0 Then
            Dim lvwItem As ListViewItem = lvActors.SelectedItems(0)
            Dim eActor As MediaContainers.Person = DirectCast(lvwItem.Tag, MediaContainers.Person)
            Using dAddEditActor As New dlgAddEditActor
                If dAddEditActor.ShowDialog(eActor) = DialogResult.OK Then
                    eActor = dAddEditActor.Result
                    lvwItem.Text = eActor.ID.ToString
                    lvwItem.Tag = eActor
                    lvwItem.SubItems(1).Text = eActor.Name
                    lvwItem.SubItems(2).Text = eActor.Role
                    lvwItem.SubItems(3).Text = eActor.URLOriginal
                    lvwItem.Selected = True
                    lvwItem.EnsureVisible()
                End If
            End Using
        End If
    End Sub

    Private Sub Actors_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvActors.KeyDown
        If e.KeyCode = Keys.Delete Then Actors_Remove_Click()
    End Sub

    Private Sub Actors_Remove_Click() Handles btnActorsRemove.Click
        If lvActors.Items.Count > 0 Then
            While lvActors.SelectedItems.Count > 0
                lvActors.Items.Remove(lvActors.SelectedItems(0))
            End While
        End If
    End Sub

    Private Sub Actors_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorsUp.Click

    End Sub

    Private Sub CertificationsAsMPAARating_Click(sender As Object, e As EventArgs) Handles btnCertificationsAsMPAARating.Click
        txtMPAA.Text = String.Join(" / ", DataGridView_RowsToList(dgvCertifications))
    End Sub

    Private Sub CheckedListBox_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles _
        clbGenres.ItemCheck,
        clbTags.ItemCheck
        Dim nCheckedListBox = DirectCast(sender, CheckedListBox)
        If e.Index = 0 Then
            For i As Integer = 1 To nCheckedListBox.Items.Count - 1
                nCheckedListBox.SetItemChecked(i, False)
            Next
        Else
            nCheckedListBox.SetItemChecked(0, False)
        End If
    End Sub

    Private Function ConvertButtonToModifierType(ByVal sender As System.Object) As Enums.ModifierType
        Select Case True
            Case sender Is btnRemoveBanner, sender Is btnSetBannerDL, sender Is btnSetBannerLocal, sender Is btnSetBannerScrape
                Return Enums.ModifierType.MainBanner
            Case sender Is btnRemoveCharacterArt, sender Is btnSetCharacterArtDL, sender Is btnSetCharacterArtLocal, sender Is btnSetCharacterArtScrape
                Return Enums.ModifierType.MainCharacterArt
            Case sender Is btnRemoveClearArt, sender Is btnSetClearArtDL, sender Is btnSetClearArtLocal, sender Is btnSetClearArtScrape
                Return Enums.ModifierType.MainClearArt
            Case sender Is btnRemoveClearLogo, sender Is btnSetClearLogoDL, sender Is btnSetClearLogoLocal, sender Is btnSetClearLogoScrape
                Return Enums.ModifierType.MainClearLogo
            Case sender Is btnExtrafanartsRemove, sender Is btnSetExtrafanartsDL, sender Is btnSetExtrafanartsLocal, sender Is btnSetExtrafanartsScrape
                Return Enums.ModifierType.MainExtrafanarts
            Case sender Is btnRemoveFanart, sender Is btnSetFanartDL, sender Is btnSetFanartLocal, sender Is btnSetFanartScrape
                Return Enums.ModifierType.MainFanart
            Case sender Is btnRemoveLandscape, sender Is btnSetLandscapeDL, sender Is btnSetLandscapeLocal, sender Is btnSetLandscapeScrape
                Return Enums.ModifierType.MainLandscape
            Case sender Is btnRemovePoster, sender Is btnSetPosterDL, sender Is btnSetPosterLocal, sender Is btnSetPosterScrape
                Return Enums.ModifierType.MainPoster
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Data_Fill(Optional ByVal DoAll As Boolean = True)
        'Database related part
        With tmpDBElement
            btnManual.Enabled = .NfoPathSpecified
            cbEpisodeOrdering.SelectedIndex = .EpisodeOrdering
            cbEpisodeSorting.SelectedIndex = .EpisodeSorting
            chkLocked.Checked = .IsLocked
            chkMarked.Checked = .IsMarked
            chkMarkedCustom1.Checked = .IsMarkCustom1
            chkMarkedCustom2.Checked = .IsMarkCustom2
            chkMarkedCustom3.Checked = .IsMarkCustom3
            chkMarkedCustom4.Checked = .IsMarkCustom4
            'Language
            If cbSourceLanguage.Items.Count > 0 Then
                Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = .Language)
                If tLanguage IsNot Nothing Then
                    cbSourceLanguage.Text = tLanguage.Description
                Else
                    tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.Language_Main))
                    If tLanguage IsNot Nothing Then
                        cbSourceLanguage.Text = tLanguage.Description
                    Else
                        cbSourceLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                    End If
                End If
            End If
        End With

        'Information part
        With tmpDBElement.TVShow
            'Actors
            Dim lvItem As ListViewItem
            lvActors.Items.Clear()
            For Each tActor As MediaContainers.Person In .Actors
                lvItem = lvActors.Items.Add(tActor.ID.ToString)
                lvItem.Tag = tActor
                lvItem.SubItems.Add(tActor.Name)
                lvItem.SubItems.Add(tActor.Role)
                lvItem.SubItems.Add(tActor.URLOriginal)
            Next
            'Certifications
            For Each v In .Certifications
                dgvCertifications.Rows.Add(New Object() {v})
            Next
            dgvCertifications.ClearSelection()
            'Countries
            For Each v In .Countries
                dgvCountries.Rows.Add(New Object() {v})
            Next
            dgvCountries.ClearSelection()
            'Creators
            For Each v In .Creators
                dgvCreators.Rows.Add(New Object() {v})
            Next
            dgvCreators.ClearSelection()
            'Genres
            Genres_Fill()
            'MPAA
            MPAA_Fill()
            MPAA_Select()
            'OriginalTitle
            txtOriginalTitle.Text = .OriginalTitle
            'Premiered
            dtpPremiered.Text = .Premiered
            'Plot
            txtPlot.Text = .Plot
            'Ratings
            Ratings_Fill()
            'Runtime
            txtRuntime.Text = .Runtime
            'SortTitle
            txtSortTitle.Text = .SortTitle
            'Status
            txtStatus.Text = .Status
            'Studios
            For Each v In .Studios
                dgvStudios.Rows.Add(New Object() {v})
            Next
            dgvStudios.ClearSelection()
            'Tags
            Tags_Fill()
            'Title
            txtTitle.Text = .Title
            'UserRating
            txtUserRating.Text = .UserRating.ToString
        End With

        If DoAll Then
            'Images and TabPages/Panels controll
            Dim bNeedTab_Images As Boolean
            Dim bNeedTab_Other As Boolean

            With tmpDBElement.ImagesContainer
                'Load all images to MemoryStream and Bitmap
                tmpDBElement.LoadAllImages(True, True)

                'Banner
                If Master.eSettings.TVShowBannerAnyEnabled Then
                    btnSetBannerScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'CharacterArt
                If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                    btnSetCharacterArtScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    If .CharacterArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainCharacterArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlCharacterArt.Visible = False
                End If

                'ClearArt
                If Master.eSettings.TVShowClearArtAnyEnabled Then
                    btnSetClearArtScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearArt.Visible = False
                End If

                'ClearLogo
                If Master.eSettings.TVShowClearLogoAnyEnabled Then
                    btnSetClearLogoScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearLogo.Visible = False
                End If

                'Extrafanarts
                If Master.eSettings.TVShowExtrafanartsAnyEnabled Then
                    btnSetExtrafanartsScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    If .Extrafanarts.Count > 0 Then
                        Dim iIndex As Integer = 0
                        For Each tImg As MediaContainers.Image In .Extrafanarts
                            Image_Extrafanarts_Add(String.Concat(tImg.Width, " x ", tImg.Height), iIndex, tImg)
                            iIndex += 1
                        Next
                    End If
                    bNeedTab_Images = True
                Else
                    pnlExtrafanarts.Visible = False
                End If

                'Fanart
                If Master.eSettings.TVShowFanartAnyEnabled Then
                    btnSetFanartScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'Landscape
                If Master.eSettings.TVShowLandscapeAnyEnabled Then
                    btnSetLandscapeScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If Master.eSettings.TVShowPosterAnyEnabled Then
                    btnSetPosterScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    If .Poster.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainPoster)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlPoster.Visible = False
                End If
            End With

            'Theme
            If Master.eSettings.TvShowThemeAnyEnabled Then
                btnSetThemeScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
                If tmpDBElement.Theme.LocalFilePathSpecified OrElse tmpDBElement.Theme.URLAudioStreamSpecified Then
                    Theme_Load(tmpDBElement.Theme)
                End If
                bNeedTab_Other = True
            Else
                gbTheme.Visible = False
            End If

            'Remove empty tab pages
            If Not bNeedTab_Images Then
                tcEdit.TabPages.Remove(tpImages)
            End If
            If Not bNeedTab_Other Then
                tcEdit.TabPages.Remove(tpOther)
            End If
        End If
    End Sub

    Private Sub Data_Save()
        btnOK.Enabled = False
        btnCancel.Enabled = False
        btnRescrape.Enabled = False
        btnChange.Enabled = False

        'Database related part
        With tmpDBElement
            .EpisodeOrdering = DirectCast(cbEpisodeOrdering.SelectedIndex, Enums.EpisodeOrdering)
            .EpisodeSorting = DirectCast(cbEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)
            .IsLocked = chkLocked.Checked
            .IsMarked = chkMarked.Checked
            .IsMarkCustom1 = chkMarkedCustom1.Checked
            .IsMarkCustom2 = chkMarkedCustom2.Checked
            .IsMarkCustom3 = chkMarkedCustom3.Checked
            .IsMarkCustom4 = chkMarkedCustom4.Checked
            'Language
            If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
                .Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
                .TVShow.Language = .Language
            Else
                .Language = "en-US"
                .TVShow.Language = .Language
            End If
            'ListTitle
            .ListTitle = StringUtils.ListTitle_TVShow(txtTitle.Text, txtStatus.Text)
        End With

        'Information part
        With tmpDBElement.TVShow
            'Actors
            .Actors.Clear()
            If lvActors.Items.Count > 0 Then
                Dim iOrder As Integer = 0
                For Each lviActor As ListViewItem In lvActors.Items
                    Dim addActor As MediaContainers.Person = DirectCast(lviActor.Tag, MediaContainers.Person)
                    addActor.Order = iOrder
                    iOrder += 1
                    .Actors.Add(addActor)
                Next
            End If
            'Certifications
            .Certifications = DataGridView_RowsToList(dgvCertifications)
            'Countries
            .Countries = DataGridView_RowsToList(dgvCountries)
            'Creators
            .Creators = DataGridView_RowsToList(dgvCreators)
            'Genres
            If clbGenres.CheckedItems.Count > 0 Then
                If clbGenres.CheckedIndices.Contains(0) Then
                    .Genres.Clear()
                Else
                    .Genres = clbGenres.CheckedItems.Cast(Of String).ToList
                    .Genres.Sort()
                End If
            End If
            'MPAA
            .MPAA = String.Concat(txtMPAA.Text, " ", txtMPAADesc.Text).Trim
            'OriginalTitle
            .OriginalTitle = txtOriginalTitle.Text.Trim
            'Plot
            .Plot = txtPlot.Text.Trim
            'Premiered
            .Premiered = dtpPremiered.Value.ToString("yyyy-MM-dd")
            'Runtime
            .Runtime = txtRuntime.Text.Trim
            'SortTitle
            .SortTitle = txtSortTitle.Text.Trim
            'Status
            .Status = txtStatus.Text.Trim
            'Studios
            .Studios = DataGridView_RowsToList(dgvStudios)
            'Tags
            If clbTags.CheckedItems.Count > 0 Then
                If clbTags.CheckedIndices.Contains(0) Then
                    .Tags.Clear()
                Else
                    .Tags = clbTags.CheckedItems.Cast(Of String).ToList
                    .Tags.Sort()
                End If
            End If
            'Title
            .Title = txtTitle.Text.Trim
            'UserRating
            .UserRating = If(Integer.TryParse(txtUserRating.Text.Trim, 0), CInt(txtUserRating.Text.Trim), 0)
        End With
    End Sub

    Private Sub DataGridView_Leave(sender As Object, e As EventArgs) Handles _
        dgvCertifications.Leave,
        dgvCountries.Leave,
        dgvCreators.Leave,
        dgvStudios.Leave
        DirectCast(sender, DataGridView).ClearSelection()
    End Sub

    Private Function DataGridView_RowsToList(dgv As DataGridView) As List(Of String)
        Dim newList As New List(Of String)
        For Each r As DataGridViewRow In dgv.Rows
            If r.Cells(0).Value IsNot Nothing Then newList.Add(r.Cells(0).Value.ToString)
        Next
        Return newList
    End Function
    ''' <summary>
    ''' Fills the genre list with selected genres first in list and all known genres from database as second
    ''' </summary>
    Private Sub Genres_Fill()
        clbGenres.Items.Add(Master.eLang.None)
        If tmpDBElement.TVShow.GenresSpecified Then
            tmpDBElement.TVShow.Genres.Sort()
            clbGenres.Items.AddRange(tmpDBElement.TVShow.Genres.ToArray)
            'enable all selected genres, skip the first entry "[none]"
            For i As Integer = 1 To clbGenres.Items.Count - 1
                clbGenres.SetItemChecked(i, True)
            Next
        Else
            'select "[none]" if no genre has been specified
            clbGenres.SetItemChecked(0, True)
        End If
        'add the rest of all genres
        clbGenres.Items.AddRange(APIXML.GetGenreList.Where(Function(f) Not clbGenres.Items.Contains(f)).ToArray)
    End Sub

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbBanner.DoubleClick,
        pbCharacterArt.DoubleClick,
        pbClearArt.DoubleClick,
        pbClearLogo.DoubleClick,
        pbFanart.DoubleClick,
        pbLandscape.DoubleClick,
        pbPoster.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnSetBannerDL.Click,
        btnSetCharacterArtDL.Click,
        btnSetClearArtDL.Click,
        btnSetClearLogoDL.Click,
        btnSetExtrafanartsDL.Click,
        btnSetFanartDL.Click,
        btnSetLandscapeDL.Click,
        btnSetPosterDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
                    Select Case modType
                        Case Enums.ModifierType.MainBanner
                            tmpDBElement.ImagesContainer.Banner = tImage
                        Case Enums.ModifierType.MainCharacterArt
                            tmpDBElement.ImagesContainer.CharacterArt = tImage
                        Case Enums.ModifierType.MainClearArt
                            tmpDBElement.ImagesContainer.ClearArt = tImage
                        Case Enums.ModifierType.MainClearLogo
                            tmpDBElement.ImagesContainer.ClearLogo = tImage
                        Case Enums.ModifierType.MainExtrafanarts
                            tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                            Image_Extrafanarts_Refresh()
                        Case Enums.ModifierType.MainFanart
                            tmpDBElement.ImagesContainer.Fanart = tImage
                        Case Enums.ModifierType.MainLandscape
                            tmpDBElement.ImagesContainer.Landscape = tImage
                        Case Enums.ModifierType.MainPoster
                            tmpDBElement.ImagesContainer.Poster = tImage
                    End Select
                    Image_LoadPictureBox(modType)
                End If
            End If
        End Using
    End Sub

    Private Sub Image_Drag_MouseDown(sender As Object, e As MouseEventArgs)
        Dim dragSize = SystemInformation.DragSize
        dragBoxFromMouseDown = New Rectangle(New Point(e.X - CInt((dragSize.Width / 2)), e.Y - CInt((dragSize.Height / 2))), dragSize)
    End Sub

    Private Sub Image_Drag_MouseMove(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If Not dragBoxFromMouseDown = Rectangle.Empty AndAlso
                Not dragBoxFromMouseDown.Contains(e.X, e.Y) Then
                Dim tPictureBox As PictureBox = DirectCast(sender, PictureBox)
                tPictureBox.DoDragDrop(tPictureBox, DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub Image_Drag_MouseUp(sender As Object, e As MouseEventArgs)
        dragBoxFromMouseDown = Rectangle.Empty
    End Sub

    Private Sub Image_DragDrop(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragDrop,
        pbCharacterArt.DragDrop,
        pbClearArt.DragDrop,
        pbClearLogo.DragDrop,
        pbFanart.DragDrop,
        pbLandscape.DragDrop,
        pbPoster.DragDrop,
        pnlExtrafanarts.DragDrop

        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Select Case True
                Case sender Is pbBanner
                    tmpDBElement.ImagesContainer.Banner = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                Case sender Is pbCharacterArt
                    tmpDBElement.ImagesContainer.CharacterArt = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainCharacterArt)
                Case sender Is pbClearArt
                    tmpDBElement.ImagesContainer.ClearArt = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                Case sender Is pbClearLogo
                    tmpDBElement.ImagesContainer.ClearLogo = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                Case sender Is pbFanart
                    tmpDBElement.ImagesContainer.Fanart = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                Case sender Is pbLandscape
                    tmpDBElement.ImagesContainer.Landscape = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                Case sender Is pbPoster
                    tmpDBElement.ImagesContainer.Poster = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainPoster)
                Case sender Is pnlExtrafanarts
                    tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                    Image_Extrafanarts_Refresh()
            End Select
        End If
    End Sub

    Private Sub Image_DragEnter(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragEnter,
        pbCharacterArt.DragEnter,
        pbClearArt.DragEnter,
        pbClearLogo.DragEnter,
        pbFanart.DragEnter,
        pbLandscape.DragEnter,
        pbPoster.DragEnter,
        pnlExtrafanarts.DragEnter

        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Image_Extrafanarts_Add(ByVal sDescription As String, ByVal iIndex As Integer, tImage As MediaContainers.Image)
        Try
            If tImage.ImageOriginal.Image Is Nothing Then
                tImage.LoadAndCache(Enums.ContentType.TVShow, True, True)
            End If
            ReDim Preserve pnExtrafanartsList_Panel(iIndex)
            ReDim Preserve pbExtrafanartsList_Image(iIndex)
            ReDim Preserve lblExtrafanartsList_Resolution(iIndex)

            pnExtrafanartsList_Panel(iIndex) = New Panel()
            pbExtrafanartsList_Image(iIndex) = New PictureBox()
            lblExtrafanartsList_Resolution(iIndex) = New Label()

            lblExtrafanartsList_Resolution(iIndex).AutoSize = False
            lblExtrafanartsList_Resolution(iIndex).BackColor = Color.White
            lblExtrafanartsList_Resolution(iIndex).Location = iImageList_Location_Resolution
            lblExtrafanartsList_Resolution(iIndex).Name = iIndex.ToString
            lblExtrafanartsList_Resolution(iIndex).Size = iImageList_Size_Resolution
            lblExtrafanartsList_Resolution(iIndex).Tag = tImage
            lblExtrafanartsList_Resolution(iIndex).Text = String.Format("{0}x{1}", tImage.ImageOriginal.Image.Width, tImage.ImageOriginal.Image.Height)
            lblExtrafanartsList_Resolution(iIndex).TextAlign = ContentAlignment.MiddleCenter

            pbExtrafanartsList_Image(iIndex).Image = tImage.ImageOriginal.Image
            pbExtrafanartsList_Image(iIndex).Location = iImageList_Location_Image
            pbExtrafanartsList_Image(iIndex).Name = iIndex.ToString
            pbExtrafanartsList_Image(iIndex).Size = iImageList_Size_Image
            pbExtrafanartsList_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            pbExtrafanartsList_Image(iIndex).Tag = tImage

            pnExtrafanartsList_Panel(iIndex).BackColor = Color.White
            pnExtrafanartsList_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
            pnExtrafanartsList_Panel(iIndex).Left = iImageList_DistanceLeft
            pnExtrafanartsList_Panel(iIndex).Name = iIndex.ToString
            pnExtrafanartsList_Panel(iIndex).Size = iImageList_Size_Panel
            pnExtrafanartsList_Panel(iIndex).Tag = tImage
            pnExtrafanartsList_Panel(iIndex).Top = iExtrafanartsList_NextTop

            pnlExtrafanartsList.Controls.Add(pnExtrafanartsList_Panel(iIndex))
            pnExtrafanartsList_Panel(iIndex).Controls.Add(pbExtrafanartsList_Image(iIndex))
            pnExtrafanartsList_Panel(iIndex).Controls.Add(lblExtrafanartsList_Resolution(iIndex))
            pnExtrafanartsList_Panel(iIndex).BringToFront()

            AddHandler lblExtrafanartsList_Resolution(iIndex).Click, AddressOf Image_Extrafanarts_Label_Click
            AddHandler lblExtrafanartsList_Resolution(iIndex).DoubleClick, AddressOf Image_DoubleClick
            AddHandler pbExtrafanartsList_Image(iIndex).Click, AddressOf Image_Extrafanarts_PictureBox_Click
            AddHandler pbExtrafanartsList_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
            AddHandler pbExtrafanartsList_Image(iIndex).MouseDown, AddressOf Image_Drag_MouseDown
            AddHandler pbExtrafanartsList_Image(iIndex).MouseMove, AddressOf Image_Drag_MouseMove
            AddHandler pbExtrafanartsList_Image(iIndex).MouseUp, AddressOf Image_Drag_MouseUp
            AddHandler pnExtrafanartsList_Panel(iIndex).Click, AddressOf Image_Extrafanarts_Panel_Click
            AddHandler pnExtrafanartsList_Panel(iIndex).DoubleClick, AddressOf Image_DoubleClick
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        iExtrafanartsList_NextTop = iExtrafanartsList_NextTop + iImageList_Size_Panel.Height + iImageList_DistanceTop
        lblExtrafanarts.Text = String.Format("{0} ({1})", Master.eLang.GetString(992, "Extrafanarts"), pnlExtrafanartsList.Controls.Count)
    End Sub

    Private Sub Image_Extrafanarts_DeselectAllImages()
        If pnExtrafanartsList_Panel IsNot Nothing Then
            For i As Integer = 0 To pnExtrafanartsList_Panel.Count - 1
                pnExtrafanartsList_Panel(i).BackColor = Color.White
                lblExtrafanartsList_Resolution(i).BackColor = Color.White
                lblExtrafanartsList_Resolution(i).ForeColor = Color.Black
            Next
        End If
        currExtrafanartsList_Item = Nothing
        btnExtrafanartsRemove.Enabled = False
    End Sub

    Private Sub Image_Extrafanarts_DoSelect(ByVal iIndex As Integer, ByVal tTag As MediaContainers.Image)
        For i As Integer = 0 To pnExtrafanartsList_Panel.Count - 1
            pnExtrafanartsList_Panel(i).BackColor = Color.White
            lblExtrafanartsList_Resolution(i).BackColor = Color.White
            lblExtrafanartsList_Resolution(i).ForeColor = Color.Black
        Next

        pnExtrafanartsList_Panel(iIndex).BackColor = Color.Gray
        lblExtrafanartsList_Resolution(iIndex).BackColor = Color.Gray
        lblExtrafanartsList_Resolution(iIndex).ForeColor = Color.White
        currExtrafanartsList_Item = tTag
        btnExtrafanartsRemove.Enabled = True
    End Sub

    Private Sub Image_Extrafanarts_Label_Click(sender As Object, e As EventArgs)
        Image_Extrafanarts_DoSelect(Convert.ToInt32(DirectCast(sender, Label).Name), DirectCast(DirectCast(sender, Label).Tag, MediaContainers.Image))
    End Sub

    Private Sub Image_Extrafanarts_Panel_Click(sender As Object, e As EventArgs)
        Image_Extrafanarts_DoSelect(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub Image_Extrafanarts_PictureBox_Click(sender As Object, e As EventArgs)
        Image_Extrafanarts_DoSelect(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub Image_Extrafanarts_Refresh() Handles btnExtrafanartsRefresh.Click
        iExtrafanartsList_NextTop = iImageList_DistanceTop
        While pnlExtrafanartsList.Controls.Count > 0
            pnlExtrafanartsList.Controls(0).Dispose()
        End While

        If tmpDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            Dim iIndex As Integer = 0
            For Each img As MediaContainers.Image In tmpDBElement.ImagesContainer.Extrafanarts
                Image_Extrafanarts_Add(String.Concat(img.Width, " x ", img.Height), iIndex, img)
                iIndex += 1
            Next
        End If
    End Sub

    Private Sub Image_LoadPictureBox(ByVal imageType As Enums.ModifierType)
        Dim cImage As MediaContainers.Image
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Select Case imageType
            Case Enums.ModifierType.MainBanner
                cImage = tmpDBElement.ImagesContainer.Banner
                lblSize = lblBannerSize
                pbImage = pbBanner
            Case Enums.ModifierType.MainCharacterArt
                cImage = tmpDBElement.ImagesContainer.CharacterArt
                lblSize = lblCharacterArtSize
                pbImage = pbCharacterArt
            Case Enums.ModifierType.MainClearArt
                cImage = tmpDBElement.ImagesContainer.ClearArt
                lblSize = lblClearArtSize
                pbImage = pbClearArt
            Case Enums.ModifierType.MainClearLogo
                cImage = tmpDBElement.ImagesContainer.ClearLogo
                lblSize = lblClearLogoSize
                pbImage = pbClearLogo
            Case Enums.ModifierType.MainFanart
                cImage = tmpDBElement.ImagesContainer.Fanart
                lblSize = lblFanartSize
                pbImage = pbFanart
            Case Enums.ModifierType.MainLandscape
                cImage = tmpDBElement.ImagesContainer.Landscape
                lblSize = lblLandscapeSize
                pbImage = pbLandscape
            Case Enums.ModifierType.MainPoster
                cImage = tmpDBElement.ImagesContainer.Poster
                lblSize = lblPosterSize
                pbImage = pbPoster
            Case Else
                Return
        End Select
        If cImage.ImageOriginal.Image IsNot Nothing Then
            pbImage.Image = cImage.ImageOriginal.Image
            pbImage.Tag = cImage
            lblSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbImage.Image.Width, pbImage.Image.Height)
            lblSize.Visible = True
        End If
    End Sub

    Private Sub Image_Local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnSetBannerLocal.Click,
        btnSetCharacterArtLocal.Click,
        btnSetClearArtLocal.Click,
        btnSetClearLogoLocal.Click,
        btnSetExtrafanartsLocal.Click,
        btnSetFanartLocal.Click,
        btnSetLandscapeLocal.Click,
        btnSetPosterLocal.Click
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.FileItem.MainPath.FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With
        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
                Select Case modType
                    Case Enums.ModifierType.MainBanner
                        tmpDBElement.ImagesContainer.Banner = tImage
                    Case Enums.ModifierType.MainCharacterArt
                        tmpDBElement.ImagesContainer.CharacterArt = tImage
                    Case Enums.ModifierType.MainClearArt
                        tmpDBElement.ImagesContainer.ClearArt = tImage
                    Case Enums.ModifierType.MainClearLogo
                        tmpDBElement.ImagesContainer.ClearLogo = tImage
                    Case Enums.ModifierType.MainExtrafanarts
                        tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                    Case Enums.ModifierType.MainFanart
                        tmpDBElement.ImagesContainer.Fanart = tImage
                    Case Enums.ModifierType.MainLandscape
                        tmpDBElement.ImagesContainer.Landscape = tImage
                    Case Enums.ModifierType.MainPoster
                        tmpDBElement.ImagesContainer.Poster = tImage
                End Select
                If modType = Enums.ModifierType.MainExtrafanarts Then
                    Image_Extrafanarts_Refresh()
                Else
                    Image_LoadPictureBox(modType)
                End If
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnRemoveBanner.Click,
        btnRemoveCharacterArt.Click,
        btnRemoveClearArt.Click,
        btnRemoveClearLogo.Click,
        btnExtrafanartsRemove.Click,
        btnRemoveFanart.Click,
        btnRemoveLandscape.Click,
        btnRemovePoster.Click
        Dim lblSize As Label = Nothing
        Dim pbImage As PictureBox = Nothing
        Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
        Select Case modType
            Case Enums.ModifierType.MainBanner
                lblSize = lblBannerSize
                pbImage = pbBanner
                tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
            Case Enums.ModifierType.MainCharacterArt
                lblSize = lblCharacterArtSize
                pbImage = pbCharacterArt
                tmpDBElement.ImagesContainer.CharacterArt = New MediaContainers.Image
            Case Enums.ModifierType.MainClearArt
                lblSize = lblClearArtSize
                pbImage = pbClearArt
                tmpDBElement.ImagesContainer.ClearArt = New MediaContainers.Image
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblClearLogoSize
                pbImage = pbClearLogo
                tmpDBElement.ImagesContainer.ClearLogo = New MediaContainers.Image
            Case Enums.ModifierType.MainExtrafanarts
                If currExtrafanartsList_Item IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Extrafanarts.Remove(currExtrafanartsList_Item)
                    Image_Extrafanarts_Refresh()
                End If
            Case Enums.ModifierType.MainFanart
                lblSize = lblFanartSize
                pbImage = pbFanart
                tmpDBElement.ImagesContainer.Fanart = New MediaContainers.Image
            Case Enums.ModifierType.MainLandscape
                lblSize = lblLandscapeSize
                pbImage = pbLandscape
                tmpDBElement.ImagesContainer.Landscape = New MediaContainers.Image
            Case Enums.ModifierType.MainPoster
                lblSize = lblPosterSize
                pbImage = pbPoster
                tmpDBElement.ImagesContainer.Poster = New MediaContainers.Image
            Case Else
                Return
        End Select
        If lblSize IsNot Nothing Then
            lblSize.Text = String.Empty
            lblSize.Visible = False
        End If
        If pbImage IsNot Nothing Then
            pbImage.Image = Nothing
            pbImage.Tag = Nothing
        End If
    End Sub

    Private Sub Image_Scrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnSetBannerScrape.Click,
        btnSetCharacterArtScrape.Click,
        btnSetClearArtScrape.Click,
        btnSetClearLogoScrape.Click,
        btnSetExtrafanartsScrape.Click,
        btnSetFanartScrape.Click,
        btnSetLandscapeScrape.Click,
        btnSetPosterScrape.Click
        Cursor = Cursors.WaitCursor
        Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, modType, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            Dim iImageCount = 0
            Dim strNoImagesFound As String = String.Empty
            Select Case modType
                Case Enums.ModifierType.MainBanner
                    iImageCount = aContainer.MainBanners.Count
                    strNoImagesFound = Master.eLang.GetString(1363, "No Banners found")
                Case Enums.ModifierType.MainCharacterArt
                    iImageCount = aContainer.MainCharacterArts.Count
                    strNoImagesFound = Master.eLang.GetString(1343, "No CharacterArts found")
                Case Enums.ModifierType.MainClearArt
                    iImageCount = aContainer.MainClearArts.Count
                    strNoImagesFound = Master.eLang.GetString(1102, "No ClearArts found")
                Case Enums.ModifierType.MainClearLogo
                    iImageCount = aContainer.MainClearLogos.Count
                    strNoImagesFound = Master.eLang.GetString(1103, "No ClearLogos found")
                Case Enums.ModifierType.MainExtrafanarts, Enums.ModifierType.MainFanart
                    iImageCount = aContainer.MainFanarts.Count
                    strNoImagesFound = Master.eLang.GetString(970, "No Fanarts found")
                Case Enums.ModifierType.MainLandscape
                    iImageCount = aContainer.MainLandscapes.Count
                    strNoImagesFound = Master.eLang.GetString(1197, "No Landscapes found")
                Case Enums.ModifierType.MainPoster
                    iImageCount = aContainer.MainPosters.Count
                    strNoImagesFound = Master.eLang.GetString(972, "No Posters found")
            End Select
            If iImageCount > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    Select Case modType
                        Case Enums.ModifierType.MainBanner
                            tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                            If tmpDBElement.ImagesContainer.Banner.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.MainCharacterArt
                            tmpDBElement.ImagesContainer.CharacterArt = dlgImgS.Result.ImagesContainer.CharacterArt
                            If tmpDBElement.ImagesContainer.CharacterArt.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.MainClearArt
                            tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                            If tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.MainClearLogo
                            tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                            If tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.MainExtrafanarts
                            tmpDBElement.ImagesContainer.Extrafanarts = dlgImgS.Result.ImagesContainer.Extrafanarts
                            Image_Extrafanarts_Refresh()
                        Case Enums.ModifierType.MainFanart
                            tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                            If tmpDBElement.ImagesContainer.Fanart.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.MainLandscape
                            tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                            If tmpDBElement.ImagesContainer.Landscape.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.MainPoster
                            tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                            If tmpDBElement.ImagesContainer.Poster.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                    End Select
                End If
            Else
                MessageBox.Show(strNoImagesFound, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub MPAA_DoubleClick(sender As Object, e As EventArgs) Handles lbMPAA.DoubleClick
        If lbMPAA.SelectedItems.Count = 1 Then
            If lbMPAA.SelectedIndex = 0 Then
                txtMPAA.Text = String.Empty
            Else
                txtMPAA.Text = lbMPAA.SelectedItem.ToString
            End If
        End If
    End Sub

    Private Sub MPAA_Fill()
        lbMPAA.Items.Add(Master.eLang.None)
        If Not String.IsNullOrEmpty(Master.eSettings.TVScraperShowMPAANotRated) Then lbMPAA.Items.Add(Master.eSettings.TVScraperShowMPAANotRated)
        lbMPAA.Items.AddRange(APIXML.GetRatingList_TV)
    End Sub

    Private Sub MPAA_Select()
        If tmpDBElement.TVShow.MPAASpecified Then
            If Master.eSettings.TVScraperShowCertOnlyValue Then
                Dim sItem As String = String.Empty
                For i As Integer = 0 To lbMPAA.Items.Count - 1
                    sItem = lbMPAA.Items(i).ToString
                    If sItem.Contains(":") AndAlso sItem.Split(Convert.ToChar(":"))(1) = tmpDBElement.TVShow.MPAA Then
                        lbMPAA.SelectedIndex = i
                        lbMPAA.TopIndex = i
                        Exit For
                    End If
                Next
            Else
                Dim i As Integer = 0
                For ctr As Integer = 0 To lbMPAA.Items.Count - 1
                    If tmpDBElement.TVShow.MPAA.ToLower.StartsWith(lbMPAA.Items.Item(ctr).ToString.ToLower) Then
                        i = ctr
                        Exit For
                    End If
                Next
                lbMPAA.SelectedIndex = i
                lbMPAA.TopIndex = i

                Dim strMPAA As String = String.Empty
                Dim strMPAADesc As String = String.Empty
                If i > 0 Then
                    strMPAA = lbMPAA.Items.Item(i).ToString
                    strMPAADesc = tmpDBElement.TVShow.MPAA.Replace(strMPAA, String.Empty).Trim
                    txtMPAA.Text = strMPAA
                    txtMPAADesc.Text = strMPAADesc
                Else
                    txtMPAA.Text = tmpDBElement.TVShow.MPAA
                End If
            End If
        End If

        If lbMPAA.SelectedItems.Count = 0 Then
            lbMPAA.SelectedIndex = 0
            lbMPAA.TopIndex = 0
        End If
    End Sub

    Private Sub Ratings_Fill()
        Dim lvItem As ListViewItem
        lvRatings.Items.Clear()
        For Each tRating As MediaContainers.RatingDetails In tmpDBElement.TVShow.Ratings
            lvItem = lvRatings.Items.Add(tRating.Name)
            lvItem.SubItems.Add(tRating.Value.ToString)
            lvItem.SubItems.Add(tRating.Votes.ToString)
            lvItem.SubItems.Add(tRating.Max.ToString)
            lvItem.Tag = tRating
        Next
    End Sub

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcEdit.SelectedIndexChanged
        Image_Extrafanarts_DeselectAllImages()
    End Sub
    ''' <summary>
    ''' Fills the tag list with selected tags first in list and all known gtagsenres from database as second
    ''' </summary>
    Private Sub Tags_Fill()
        clbTags.Items.Add(Master.eLang.None)
        If tmpDBElement.TVShow.TagsSpecified Then
            tmpDBElement.TVShow.Tags.Sort()
            clbTags.Items.AddRange(tmpDBElement.TVShow.Tags.ToArray)
            'enable all selected tags, skip the first entry "[none]"
            For i As Integer = 1 To clbTags.Items.Count - 1
                clbTags.SetItemChecked(i, True)
            Next
        Else
            'select "[none]" if no tag has been specified
            clbTags.SetItemChecked(0, True)
        End If
        'add the rest of all tags
        clbTags.Items.AddRange(Master.DB.GetAllTags.Where(Function(f) Not clbTags.Items.Contains(f)).ToArray)
    End Sub

    Private Sub TextBox_NumericOnly(sender As Object, e As KeyPressEventArgs) Handles txtUserRating.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_SelectAll(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

    Private Sub Theme_Load(ByVal Theme As MediaContainers.Theme)
        txtLocalTheme.Text =
            If(Theme.LocalFilePathSpecified, Theme.LocalFilePath,
            If(Theme.URLAudioStreamSpecified, Theme.URLAudioStream,
            If(Theme.URLWebsiteSpecified, Theme.URLWebsite, String.Empty)))
    End Sub

    Private Sub Theme_Local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetThemeLocal.Click
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.FileItem.MainPath.FullName
            .Filter = FileUtils.Common.GetOpenFileDialogFilter_Theme()
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.Theme = New MediaContainers.Theme With {.LocalFilePath = ofdLocalFiles.FileName}
            tmpDBElement.Theme.LoadAndCache()
            Theme_Load(tmpDBElement.Theme)
        End If
    End Sub

    Private Sub Theme_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalThemePlay.Click
        Try
            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(txtLocalTheme.Text) Then
                tPath = String.Concat("""", txtLocalTheme.Text, """")
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                Process.Start(tPath)
            End If
        Catch
            MessageBox.Show(Master.eLang.GetString(1078, "The theme could not be played. This could due be you don't have the proper player to play the theme type."), Master.eLang.GetString(1079, "Error Playing Theme"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub Theme_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTheme.Click
        tmpDBElement.Theme = New MediaContainers.Theme
        txtLocalTheme.Text = String.Empty
    End Sub

    Private Sub Theme_Scrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetThemeScrape.Click
        Dim dThemeSelect As dlgThemeSelect
        Dim tList As New List(Of MediaContainers.Theme)
        If Not ModulesManager.Instance.ScrapeTheme_TVShow(tmpDBElement, Enums.ModifierType.MainTheme, tList) Then
            If tList.Count > 0 Then
                dThemeSelect = New dlgThemeSelect()
                If dThemeSelect.ShowDialog(tmpDBElement, tList, True) = DialogResult.OK Then
                    tmpDBElement.Theme = dThemeSelect.Result
                    Theme_Load(tmpDBElement.Theme)
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1163, "No Themes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub Theme_TextChanged(sender As Object, e As EventArgs) Handles txtLocalTheme.TextChanged
        btnLocalThemePlay.Enabled = Not String.IsNullOrEmpty(txtLocalTheme.Text)
    End Sub

    Private Sub TVShow_EditManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If dlgManualEdit.ShowDialog(tmpDBElement.NfoPath) = DialogResult.OK Then
            tmpDBElement.TVShow = Info.LoadFromNFO_TVShow(tmpDBElement.NfoPath)
            Data_Fill(False)
        End If
    End Sub

    Private Sub Watched_CheckedChanged(sender As Object, e As EventArgs) Handles chkWatched.CheckedChanged
        dtpLastPlayed.Enabled = chkWatched.Checked
    End Sub

#End Region 'Methods

End Class