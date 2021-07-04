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

Public Class dlgEdit_TVShow

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
    Private pnlExtrafanartsList_Panel() As Panel
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
        FormUtils.Forms.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(tmpDBElement, True) Then
            pbBanner.AllowDrop = True
            pbCharacterArt.AllowDrop = True
            pbClearArt.AllowDrop = True
            pbClearLogo.AllowDrop = True
            pbFanart.AllowDrop = True
            pbKeyart.AllowDrop = True
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

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_Change_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChange.Click
        DialogResult = DialogResult.Abort
    End Sub

    Private Sub DialogResult_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        Data_Save()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub DialogResult_Rescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRescrape.Click
        DialogResult = DialogResult.Retry
    End Sub

    Private Sub Setup()
        With Master.eLang
            Dim mTitle As String = tmpDBElement.TVShow.Title
            Text = String.Concat(.GetString(663, "Edit Show"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
            btnCancel.Text = .Cancel
            btnChange.Text = .GetString(767, "Change TV Show")
            btnOK.Text = .OK
            btnRescrape.Text = .GetString(716, "Re-Scrape")
            chkLocked.Text = .GetString(43, "Locked")
            chkMarked.Text = .GetString(48, "Marked")
            chkWatched.Text = .GetString(981, "Watched")
            colActorsName.Text = .GetString(232, "Name")
            colActorsRole.Text = .GetString(233, "Role")
            colActorsThumb.Text = .GetString(234, "Thumb")
            colRatingsDefault.HeaderText = .GetString(1229, "Default")
            colRatingsMax.HeaderText = .GetString(1230, "Max")
            colRatingsSource.HeaderText = .GetString(232, "Name")
            colRatingsValue.HeaderText = .GetString(345, "Value")
            colRatingsVotes.HeaderText = .GetString(244, "Votes")
            gbTheme.Text = .GetString(1118, "Theme")
            lblActors.Text = String.Concat(.GetString(231, "Actors"), ":")
            lblBanner.Text = .GetString(838, "Banner")
            lblCertifications.Text = String.Concat(.GetString(56, "Certifications"), ":")
            lblCharacterArt.Text = .GetString(1140, "CharacterArt")
            lblClearArt.Text = .GetString(1096, "ClearArt")
            lblClearLogo.Text = .GetString(1097, "ClearLogo")
            lblCountries.Text = String.Concat(.GetString(237, "Countries"), ":")
            lblCreators.Text = String.Concat(.GetString(744, "Creators"), ":")
            lblEpisodeOrdering.Text = String.Concat(.GetString(739, "Episode Ordering"), ":")
            lblEpisodeSorting.Text = String.Concat(.GetString(364, "Show Episodes by"), ":")
            lblExtrafanarts.Text = String.Format("{0} ({1})", .GetString(992, "Extrafanarts"), pnlExtrafanartsList.Controls.Count)
            lblFanart.Text = .GetString(149, "Fanart")
            lblGenres.Text = String.Concat(.GetString(725, "Genres"), ":")
            lblKeyart.Text = .GetString(1237, "Keyart")
            lblLandscape.Text = .GetString(1059, "Landscape")
            lblLanguage.Text = String.Concat(.GetString(610, "Language"), ":")
            lblMPAA.Text = String.Concat(.GetString(235, "MPAA Rating"), ":")
            lblMPAADesc.Text = String.Concat(.GetString(229, "MPAA Rating Description:"), ":")
            lblOriginalTitle.Text = String.Concat(.GetString(302, "Original Title"), ":")
            lblPlot.Text = String.Concat(.GetString(65, "Plot"), ":")
            lblPoster.Text = .GetString(148, "Poster")
            lblPremiered.Text = String.Concat(.GetString(724, "Premiered"), ":")
            lblRatings.Text = String.Concat(.GetString(245, "Ratings"), ":")
            lblRuntime.Text = String.Concat(.GetString(238, "Runtime"), ":")
            lblSortTilte.Text = String.Concat(.GetString(642, "Sort Title"), ":")
            lblStatus.Text = String.Concat(.GetString(215, "Status"), ":")
            lblStudios.Text = String.Concat(.GetString(395, "Studio"), ":")
            lblTagline.Text = String.Concat(.GetString(397, "Tagline"), ":")
            lblTags.Text = String.Concat(.GetString(243, "Tags"), ":")
            lblTitle.Text = String.Concat(.GetString(21, "Title"), ":")
            lblTopDetails.Text = .GetString(664, "Edit the details for the selected show.")
            lblTopTitle.Text = .GetString(663, "Edit Show")
            lblUniqueIds.Text = String.Concat(.GetString(667, "Unique IDs"), ":")
            lblUserNote.Text = String.Concat(.GetString(666, "Note"), ":")
            lblUserRating.Text = String.Concat(.GetString(1467, "User Rating"), ":")
            tpCastCrew.Text = String.Concat(.GetString(63, "Cast"), " & ", .GetString(909, "Crew"))
            tpDetails.Text = .GetString(26, "Details")
            tpDetails2.Text = String.Concat(.GetString(26, "Details"), " 2")
            tpImages.Text = .GetString(497, "Images")
            tpOther.Text = .GetString(391, "Other")
            tsslFilename.Text = tmpDBElement.ShowPath

            cbEpisodeOrdering.Items.Clear()
            cbEpisodeOrdering.Items.AddRange(New String() { .GetString(438, "Standard"), .GetString(1067, "DVD"), .GetString(839, "Absolute"), .GetString(1332, "Day Of Year")})

            cbEpisodeSorting.Items.Clear()
            cbEpisodeSorting.Items.AddRange(New String() { .GetString(755, "Episode #"), .GetString(728, "Aired")})

            cbSourceLanguage.Items.Clear()
            cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Description).ToArray)
        End With
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

    Private Sub Actors_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lvActors.ColumnClick
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

    Private Sub Actors_Down_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorsDown.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) < (lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex + 2, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex)
            lvActors.Items(iIndex + 1).Selected = True
            lvActors.Select()
        End If
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

    Private Sub Actors_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvActors.KeyDown
        If e.KeyCode = Keys.Delete Then Actors_Remove_Click()
    End Sub

    Private Sub Actors_Remove_Click() Handles btnActorsRemove.Click
        If lvActors.Items.Count > 0 Then
            While lvActors.SelectedItems.Count > 0
                lvActors.Items.Remove(lvActors.SelectedItems(0))
            End While
        End If
    End Sub

    Private Sub Actors_Up_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorsUp.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) > 0 Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex - 1, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex + 1)
            lvActors.Items(iIndex - 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub CertificationsAsMPAARating_Click(sender As Object, e As EventArgs) Handles btnCertificationsAsMPAARating.Click
        Dim lstRows As New List(Of DataGridViewRow)
        For Each r As DataGridViewRow In dgvCertifications.SelectedRows
            If Not r.IsNewRow AndAlso
                r.Cells(0).Value IsNot Nothing AndAlso
                Not String.IsNullOrEmpty(r.Cells(0).Value.ToString.Trim) Then lstRows.Add(r)
        Next
        Dim lstValues As New List(Of String)
        For Each r As DataGridViewRow In lstRows.OrderBy(Function(f) f.Index)
            lstValues.Add(r.Cells(0).Value.ToString)
        Next
        txtMPAA.Text = String.Join(" / ", lstValues)
        dgvCertifications.ClearSelection()
    End Sub

    Private Sub Certifications_SelectionChanged(sender As Object, e As EventArgs) Handles dgvCertifications.SelectionChanged
        If dgvCertifications.SelectedRows.Count > 0 AndAlso Not dgvCertifications.SelectedRows(0).IsNewRow Then
            btnCertificationsAsMPAARating.Enabled = True
        Else
            btnCertificationsAsMPAARating.Enabled = False
        End If
    End Sub

    Private Sub CheckedListBox_ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs)
        Dim nCheckedListBox = DirectCast(sender, CheckedListBox)
        If e.Index = 0 Then
            For i As Integer = 1 To nCheckedListBox.Items.Count - 1
                nCheckedListBox.SetItemChecked(i, False)
            Next
        Else
            nCheckedListBox.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub ClearSelection() Handles tcEdit.SelectedIndexChanged
        dgvCertifications.ClearSelection()
        dgvCountries.ClearSelection()
        dgvCreators.ClearSelection()
        dgvRatings.ClearSelection()
        dgvStudios.ClearSelection()
        dgvUniqueIds.ClearSelection()
        Image_Extrafanarts_DeselectAllImages()
    End Sub

    Private Function ConvertControlToImageType(ByVal sender As Object) As Enums.ModifierType
        Select Case True
            Case sender Is btnRemoveBanner, sender Is btnDLBanner, sender Is btnLocalBanner, sender Is btnScrapeBanner, sender Is btnClipboardBanner, sender Is pbBanner
                Return Enums.ModifierType.MainBanner
            Case sender Is btnRemoveCharacterArt, sender Is btnDLCharacterArt, sender Is btnLocalCharacterArt, sender Is btnScrapeCharacterArt, sender Is btnClipboardCharacterArt, sender Is pbCharacterArt
                Return Enums.ModifierType.MainCharacterArt
            Case sender Is btnRemoveClearArt, sender Is btnDLClearArt, sender Is btnLocalClearArt, sender Is btnScrapeClearArt, sender Is btnClipboardClearArt, sender Is pbClearArt
                Return Enums.ModifierType.MainClearArt
            Case sender Is btnRemoveClearLogo, sender Is btnDLClearLogo, sender Is btnLocalClearLogo, sender Is btnScrapeClearLogo, sender Is btnClipboardClearLogo, sender Is pbClearLogo
                Return Enums.ModifierType.MainClearLogo
            Case sender Is btnRemoveExtrafanarts, sender Is btnDLExtrafanarts, sender Is btnLocalExtrafanarts, sender Is btnScrapeExtrafanarts, sender Is btnClipboardExtrafanarts, sender Is pnlExtrafanarts
                Return Enums.ModifierType.MainExtrafanarts
            Case sender Is btnRemoveFanart, sender Is btnDLFanart, sender Is btnLocalFanart, sender Is btnScrapeFanart, sender Is btnClipboardFanart, sender Is pbFanart
                Return Enums.ModifierType.MainFanart
            Case sender Is btnRemoveKeyart, sender Is btnDLKeyart, sender Is btnLocalKeyart, sender Is btnScrapeKeyart, sender Is btnClipboardKeyart, sender Is pbKeyart
                Return Enums.ModifierType.MainKeyart
            Case sender Is btnRemoveLandscape, sender Is btnDLLandscape, sender Is btnLocalLandscape, sender Is btnScrapeLandscape, sender Is btnClipboardLandscape, sender Is pbLandscape
                Return Enums.ModifierType.MainLandscape
            Case sender Is btnRemovePoster, sender Is btnDLPoster, sender Is btnLocalPoster, sender Is btnScrapePoster, sender Is btnClipboardPoster, sender Is pbPoster
                Return Enums.ModifierType.MainPoster
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Data_Fill(Optional ByVal DoAll As Boolean = True)
        'Database related part
        With tmpDBElement
            cbEpisodeOrdering.SelectedIndex = .Ordering
            cbEpisodeSorting.SelectedIndex = .EpisodeSorting
            chkLocked.Checked = .IsLock
            chkMarked.Checked = .IsMark
            chkMarkedCustom1.Checked = .IsMarkCustom1
            chkMarkedCustom2.Checked = .IsMarkCustom2
            chkMarkedCustom3.Checked = .IsMarkCustom3
            chkMarkedCustom4.Checked = .IsMarkCustom4
            'Language
            If cbSourceLanguage.Items.Count > 0 Then
                Dim tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .Language)
                If tLanguage IsNot Nothing Then
                    cbSourceLanguage.Text = tLanguage.Description
                Else
                    tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.Language_Main))
                    If tLanguage IsNot Nothing Then
                        cbSourceLanguage.Text = tLanguage.Description
                    Else
                        cbSourceLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
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
            'Plot
            txtPlot.Text = .Plot
            'Premiered
            If .PremieredSpecified Then
                dtpPremiered.Text = .Premiered
                dtpPremiered.Checked = True
            End If
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
            'Tagline
            txtTagline.Text = .Tagline
            'Tags
            Tags_Fill()
            'Title
            txtTitle.Text = .Title
            'Unique IDs
            UniqueIds_Fill()
            'UserNote
            txtUserNote.Text = .UserNote
            'UserRating
            cbUserRating.Text = .UserRating.ToString
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
                    btnScrapeBanner.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'CharacterArt
                If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                    btnScrapeCharacterArt.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    If .CharacterArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainCharacterArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlCharacterArt.Visible = False
                End If

                'ClearArt
                If Master.eSettings.TVShowClearArtAnyEnabled Then
                    btnScrapeClearArt.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearArt.Visible = False
                End If

                'ClearLogo
                If Master.eSettings.TVShowClearLogoAnyEnabled Then
                    btnScrapeClearLogo.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearLogo.Visible = False
                End If

                'Extrafanarts
                If Master.eSettings.TVShowExtrafanartsAnyEnabled Then
                    btnScrapeExtrafanarts.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    If .Extrafanarts.Count > 0 Then
                        Dim iIndex As Integer = 0
                        For Each tImg As MediaContainers.Image In .Extrafanarts
                            Image_Extraimages_Add(String.Concat(tImg.Width, " x ", tImg.Height), iIndex, tImg, Enums.ModifierType.MainExtrafanarts)
                            iIndex += 1
                        Next
                    End If
                    bNeedTab_Images = True
                Else
                    pnlExtrafanarts.Visible = False
                End If

                'Fanart
                If Master.eSettings.TVShowFanartAnyEnabled Then
                    btnScrapeFanart.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainExtrafanarts)
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'Keyart
                If Master.eSettings.TVShowKeyartAnyEnabled Then
                    btnScrapeKeyart.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainKeyart)
                    If .Keyart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainKeyart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlKeyart.Visible = False
                End If

                'Landscape
                If Master.eSettings.TVShowLandscapeAnyEnabled Then
                    btnScrapeLandscape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If Master.eSettings.TVShowPosterAnyEnabled Then
                    btnScrapePoster.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
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
                If tmpDBElement.Theme.LocalFilePathSpecified OrElse tmpDBElement.Theme.UrlAudioStreamSpecified Then
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
            .Ordering = DirectCast(cbEpisodeOrdering.SelectedIndex, Enums.EpisodeOrdering)
            .EpisodeSorting = DirectCast(cbEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)
            .IsLock = chkLocked.Checked
            .IsMark = chkMarked.Checked
            .IsMarkCustom1 = chkMarkedCustom1.Checked
            .IsMarkCustom2 = chkMarkedCustom2.Checked
            .IsMarkCustom3 = chkMarkedCustom3.Checked
            .IsMarkCustom4 = chkMarkedCustom4.Checked
            'Language
            If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
                .Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
                .TVShow.Language = .Language
            Else
                .Language = "en-US"
                .TVShow.Language = .Language
            End If
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
            If lbGenres.Items.Count > 0 Then
                .Genres = lbGenres.Items.Cast(Of String).ToList
            Else
                .Genres.Clear()
            End If
            'MPAA
            If Not String.IsNullOrEmpty(txtMPAA.Text.Trim) Then
                .MPAA = txtMPAA.Text.Trim
            Else
                .MPAA = txtMPAA.WatermarkText.Trim
            End If
            'OriginalTitle
            .OriginalTitle = txtOriginalTitle.Text.Trim
            'Plot
            .Plot = txtPlot.Text.Trim
            'Premiered
            If dtpPremiered.Checked Then
                .Premiered = dtpPremiered.Value.ToString("yyyy-MM-dd")
            Else
                .Premiered = String.Empty
            End If
            'Ratings
            .Ratings.Items = Ratings_Get()
            'Runtime
            .Runtime = txtRuntime.Text.Trim
            'SortTitle
            .SortTitle = txtSortTitle.Text.Trim
            'Status
            .Status = txtStatus.Text.Trim
            'Studios
            .Studios = DataGridView_RowsToList(dgvStudios)
            'Tagline
            .Tagline = txtTagline.Text.Trim
            'Tags
            If lbTags.Items.Count > 0 Then
                .Tags = lbTags.Items.Cast(Of String).ToList
            Else
                .Tags.Clear()
            End If
            'Title
            .Title = txtTitle.Text.Trim
            'UniqueIDs
            .UniqueIDs.Items = UniqueIds_Get()
            'UserNote
            .UserNote = txtUserNote.Text.Trim
            'UserRating
            .UserRating = CInt(cbUserRating.SelectedItem)
        End With
    End Sub

    Private Sub DataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRatings.CellValueChanged
        If e.RowIndex > -1 AndAlso e.ColumnIndex = 0 Then
            If CBool(dgvRatings.Rows(e.RowIndex).Cells(colRatingsDefault.Name).Value) Then
                For Each tRow As DataGridViewRow In dgvRatings.Rows
                    If Not tRow.Index = e.RowIndex Then tRow.Cells(colRatingsDefault.Name).Value = False
                Next
            End If
        End If
    End Sub

    Private Sub DataGridView_Leave(sender As Object, e As EventArgs) Handles _
        dgvCountries.Leave,
        dgvCreators.Leave,
        dgvRatings.Leave,
        dgvStudios.Leave,
        dgvUniqueIds.Leave
        'skip dgvCertifications otherwise it's not possible to copy the selection to the MPAA field
        DirectCast(sender, DataGridView).ClearSelection()
    End Sub

    Private Function DataGridView_RowsToList(dgv As DataGridView) As List(Of String)
        Dim newList As New List(Of String)
        For Each r As DataGridViewRow In dgv.Rows
            If r.Cells(0).Value IsNot Nothing Then newList.Add(r.Cells(0).Value.ToString)
        Next
        Return newList
    End Function

    Private Sub Genres_Add(sender As Object, e As EventArgs) Handles btnGenres_Add.Click
        If Not String.IsNullOrEmpty(cbGenres.Text.Trim) AndAlso Not lbGenres.Items.Contains(cbGenres.Text.Trim) Then lbGenres.Items.Add(cbGenres.Text.Trim)
    End Sub

    Private Sub Genres_Down(sender As Object, e As EventArgs) Handles btnGenres_Down.Click
        If lbGenres.SelectedIndex < lbGenres.Items.Count - 1 Then
            Dim i = lbGenres.SelectedIndex + 2
            lbGenres.Items.Insert(i, lbGenres.SelectedItem)
            lbGenres.Items.RemoveAt(lbGenres.SelectedIndex)
            lbGenres.SelectedIndex = i - 1
        End If
    End Sub

    Private Sub Genres_Fill()
        If tmpDBElement.TVShow.GenresSpecified Then
            lbGenres.Items.AddRange(tmpDBElement.TVShow.Genres.ToArray)
        End If
        'add the rest of all genres to the ComboBox
        cbGenres.Items.AddRange(APIXML.GetGenreList.ToArray)
    End Sub

    Private Sub Genres_Remove(sender As Object, e As EventArgs) Handles btnGenres_Remove.Click
        If lbGenres.SelectedItem IsNot Nothing Then lbGenres.Items.Remove(lbGenres.SelectedItem)
    End Sub

    Private Sub Genres_Up(sender As Object, e As EventArgs) Handles btnGenres_Up.Click
        If lbGenres.SelectedIndex > 0 Then
            Dim i = lbGenres.SelectedIndex - 1
            lbGenres.Items.Insert(i, lbGenres.SelectedItem)
            lbGenres.Items.RemoveAt(lbGenres.SelectedIndex)
            lbGenres.SelectedIndex = i
        End If
    End Sub

    Private Sub Image_Clipboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnClipboardBanner.Click,
        btnClipboardCharacterArt.Click,
        btnClipboardClearArt.Click,
        btnClipboardClearLogo.Click,
        btnClipboardExtrafanarts.Click,
        btnClipboardFanart.Click,
        btnClipboardKeyart.Click,
        btnClipboardLandscape.Click,
        btnClipboardPoster.Click

        Dim lstImages = FileUtils.ClipboardHandler.GetImagesFromClipboard
        If lstImages.Count > 0 Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            Select Case eImageType
                Case Enums.ModifierType.MainExtrafanarts
                    tmpDBElement.ImagesContainer.Extrafanarts.AddRange(lstImages)
                    Image_Extrafanarts_Refresh()
                Case Else
                    tmpDBElement.ImagesContainer.SetImageByType(lstImages(0), eImageType)
                    Image_LoadPictureBox(eImageType)
            End Select
        End If
    End Sub

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbBanner.DoubleClick,
        pbCharacterArt.DoubleClick,
        pbClearArt.DoubleClick,
        pbClearLogo.DoubleClick,
        pbFanart.DoubleClick,
        pbKeyart.DoubleClick,
        pbLandscape.DoubleClick,
        pbPoster.DoubleClick

        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnDLBanner.Click,
        btnDLCharacterArt.Click,
        btnDLClearArt.Click,
        btnDLClearLogo.Click,
        btnDLExtrafanarts.Click,
        btnDLFanart.Click,
        btnDLKeyart.Click,
        btnDLLandscape.Click,
        btnDLPoster.Click

        Using dImgManual As New dlgImageManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                    Select Case eImageType
                        Case Enums.ModifierType.MainExtrafanarts
                            tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                            Image_Extrafanarts_Refresh()
                        Case Else
                            tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                            Image_LoadPictureBox(eImageType)
                    End Select
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
        pbKeyart.DragDrop,
        pbLandscape.DragDrop,
        pbPoster.DragDrop,
        pnlExtrafanarts.DragDrop

        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            Select Case eImageType
                Case Enums.ModifierType.MainExtrafanarts
                    tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                    Image_Extrafanarts_Refresh()
                Case Else
                    tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                    Image_LoadPictureBox(eImageType)
            End Select
        End If
    End Sub

    Private Sub Image_DragEnter(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragEnter,
        pbCharacterArt.DragEnter,
        pbClearArt.DragEnter,
        pbClearLogo.DragEnter,
        pbFanart.DragEnter,
        pbKeyart.DragEnter,
        pbLandscape.DragEnter,
        pbPoster.DragEnter,
        pnlExtrafanarts.DragEnter

        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Image_Extraimages_Add(ByVal Description As String, ByVal Index As Integer, Image As MediaContainers.Image, ImageType As Enums.ModifierType)
        Dim iNextTop As Integer
        Dim tLabel() As Label = Nothing
        Dim tMainPanel As New Panel
        Dim tPanel() As Panel = Nothing
        Dim tPictureBox() As PictureBox
        Select Case ImageType
            Case Enums.ModifierType.MainExtrafanarts
                iNextTop = iExtrafanartsList_NextTop
                tLabel = lblExtrafanartsList_Resolution
                tMainPanel = pnlExtrafanartsList
                tPanel = pnlExtrafanartsList_Panel
            Case Else
                Return
        End Select

        Try
            If Image.ImageOriginal.Image Is Nothing Then
                Image.LoadAndCache(Enums.ContentType.TVShow, True, True)
            End If
            ReDim Preserve tPanel(Index)
            ReDim Preserve tPictureBox(Index)
            ReDim Preserve tLabel(Index)

            tPanel(Index) = New Panel()
            tPictureBox(Index) = New PictureBox()
            tLabel(Index) = New Label()

            tLabel(Index).AutoSize = False
            tLabel(Index).BackColor = Color.White
            tLabel(Index).Location = iImageList_Location_Resolution
            tLabel(Index).Name = Index.ToString
            tLabel(Index).Size = iImageList_Size_Resolution
            tLabel(Index).Tag = Image
            tLabel(Index).Text = String.Format("{0}x{1}", Image.ImageOriginal.Image.Width, Image.ImageOriginal.Image.Height)
            tLabel(Index).TextAlign = ContentAlignment.MiddleCenter

            tPictureBox(Index).Image = Image.ImageOriginal.Image
            tPictureBox(Index).Location = iImageList_Location_Image
            tPictureBox(Index).Name = Index.ToString
            tPictureBox(Index).Size = iImageList_Size_Image
            tPictureBox(Index).SizeMode = PictureBoxSizeMode.Zoom
            tPictureBox(Index).Tag = Image

            tPanel(Index).BackColor = Color.White
            tPanel(Index).BorderStyle = BorderStyle.FixedSingle
            tPanel(Index).Left = iImageList_DistanceLeft
            tPanel(Index).Name = Index.ToString
            tPanel(Index).Size = iImageList_Size_Panel
            tPanel(Index).Tag = Image
            tPanel(Index).Top = iNextTop

            tMainPanel.Controls.Add(tPanel(Index))
            tPanel(Index).Controls.Add(tPictureBox(Index))
            tPanel(Index).Controls.Add(tLabel(Index))
            tPanel(Index).BringToFront()

            AddHandler tLabel(Index).Click, AddressOf Image_Extraimages_Click
            AddHandler tLabel(Index).DoubleClick, AddressOf Image_DoubleClick
            AddHandler tPictureBox(Index).Click, AddressOf Image_Extraimages_Click
            AddHandler tPictureBox(Index).DoubleClick, AddressOf Image_DoubleClick
            AddHandler tPictureBox(Index).MouseDown, AddressOf Image_Drag_MouseDown
            AddHandler tPictureBox(Index).MouseMove, AddressOf Image_Drag_MouseMove
            AddHandler tPictureBox(Index).MouseUp, AddressOf Image_Drag_MouseUp
            AddHandler tPanel(Index).Click, AddressOf Image_Extraimages_Click
            AddHandler tPanel(Index).DoubleClick, AddressOf Image_DoubleClick
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Select Case ImageType
            Case Enums.ModifierType.MainExtrafanarts
                iExtrafanartsList_NextTop = iNextTop + iImageList_Size_Panel.Height + iImageList_DistanceTop
                lblExtrafanarts.Text = String.Format("{0} ({1})", Master.eLang.GetString(992, "Extrafanarts"), pnlExtrafanartsList.Controls.Count)
                lblExtrafanartsList_Resolution = tLabel
                pnlExtrafanartsList_Panel = tPanel
        End Select
    End Sub

    Private Sub Image_Extraimages_Click(sender As Object, e As EventArgs)
        Dim iIndex As Integer
        Dim tImage As MediaContainers.Image = Nothing
        Dim eImageType As Enums.ModifierType
        Select Case True
            Case TypeOf (sender) Is Label
                iIndex = Convert.ToInt32(DirectCast(sender, Label).Name)
                tImage = DirectCast(DirectCast(sender, Label).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, Label).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrafanarts
                End If
            Case TypeOf (sender) Is Panel
                iIndex = Convert.ToInt32(DirectCast(sender, Panel).Name)
                tImage = DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, Panel).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrafanarts
                End If
            Case TypeOf (sender) Is PictureBox
                iIndex = Convert.ToInt32(DirectCast(sender, PictureBox).Name)
                tImage = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, PictureBox).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrafanarts
                End If
        End Select
        If tImage IsNot Nothing Then
            Select Case eImageType
                Case Enums.ModifierType.MainExtrafanarts
                    Image_Extrafanarts_DoSelect(iIndex, tImage)
            End Select
        End If
    End Sub

    Private Sub Image_Extrafanarts_DeselectAllImages()
        If pnlExtrafanartsList_Panel IsNot Nothing Then
            For i As Integer = 0 To pnlExtrafanartsList_Panel.Count - 1
                pnlExtrafanartsList_Panel(i).BackColor = Color.White
                lblExtrafanartsList_Resolution(i).BackColor = Color.White
                lblExtrafanartsList_Resolution(i).ForeColor = Color.Black
            Next
        End If
        currExtrafanartsList_Item = Nothing
        btnRemoveExtrafanarts.Enabled = False
    End Sub

    Private Sub Image_Extrafanarts_DoSelect(ByVal iIndex As Integer, ByVal tTag As MediaContainers.Image)
        For i As Integer = 0 To pnlExtrafanartsList_Panel.Count - 1
            pnlExtrafanartsList_Panel(i).BackColor = Color.White
            lblExtrafanartsList_Resolution(i).BackColor = Color.White
            lblExtrafanartsList_Resolution(i).ForeColor = Color.Black
        Next

        pnlExtrafanartsList_Panel(iIndex).BackColor = Color.Gray
        lblExtrafanartsList_Resolution(iIndex).BackColor = Color.Gray
        lblExtrafanartsList_Resolution(iIndex).ForeColor = Color.White
        currExtrafanartsList_Item = tTag
        btnRemoveExtrafanarts.Enabled = True
    End Sub

    Private Sub Image_Extrafanarts_Refresh() Handles btnRefreshExtrafanarts.Click
        Image_Extrafanarts_DeselectAllImages()
        iExtrafanartsList_NextTop = iImageList_DistanceTop
        While pnlExtrafanartsList.Controls.Count > 0
            pnlExtrafanartsList.Controls(0).Dispose()
        End While

        If tmpDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            Dim iIndex As Integer = 0
            For Each img As MediaContainers.Image In tmpDBElement.ImagesContainer.Extrafanarts
                Image_Extraimages_Add(String.Concat(img.Width, " x ", img.Height), iIndex, img, Enums.ModifierType.MainExtrafanarts)
                iIndex += 1
            Next
        End If
    End Sub

    Private Sub Image_LoadPictureBox(ByVal ImageType As Enums.ModifierType)
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Select Case ImageType
            Case Enums.ModifierType.MainBanner
                lblSize = lblSizeBanner
                pbImage = pbBanner
            Case Enums.ModifierType.MainCharacterArt
                lblSize = lblSizeCharacterArt
                pbImage = pbCharacterArt
            Case Enums.ModifierType.MainClearArt
                lblSize = lblSizeClearArt
                pbImage = pbClearArt
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblSizeClearLogo
                pbImage = pbClearLogo
            Case Enums.ModifierType.MainFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
            Case Enums.ModifierType.MainKeyart
                lblSize = lblSizeKeyart
                pbImage = pbKeyart
            Case Enums.ModifierType.MainLandscape
                lblSize = lblSizeLandscape
                pbImage = pbLandscape
            Case Enums.ModifierType.MainPoster
                lblSize = lblSizePoster
                pbImage = pbPoster
            Case Else
                Return
        End Select
        Dim cImage = tmpDBElement.ImagesContainer.GetImageByType(ImageType)
        If cImage.ImageOriginal.Image IsNot Nothing Then
            pbImage.Image = cImage.ImageOriginal.Image
            pbImage.Tag = cImage
            lblSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbImage.Image.Width, pbImage.Image.Height)
            lblSize.Visible = True
        End If
    End Sub

    Private Sub Image_Local_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnLocalBanner.Click,
        btnLocalCharacterArt.Click,
        btnLocalClearArt.Click,
        btnLocalClearLogo.Click,
        btnLocalExtrafanarts.Click,
        btnLocalFanart.Click,
        btnLocalKeyart.Click,
        btnLocalLandscape.Click,
        btnLocalPoster.Click

        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With
        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                Select Case eImageType
                    Case Enums.ModifierType.MainExtrafanarts
                        tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                        Image_Extrafanarts_Refresh()
                    Case Else
                        tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                        Image_LoadPictureBox(eImageType)
                End Select
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnRemoveBanner.Click,
        btnRemoveCharacterArt.Click,
        btnRemoveClearArt.Click,
        btnRemoveClearLogo.Click,
        btnRemoveExtrafanarts.Click,
        btnRemoveFanart.Click,
        btnRemoveKeyart.Click,
        btnRemoveLandscape.Click,
        btnRemovePoster.Click

        Dim lblSize As Label = Nothing
        Dim pbImage As PictureBox = Nothing
        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Select Case eImageType
            Case Enums.ModifierType.MainBanner
                lblSize = lblSizeBanner
                pbImage = pbBanner
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainCharacterArt
                lblSize = lblSizeCharacterArt
                pbImage = pbCharacterArt
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainClearArt
                lblSize = lblSizeClearArt
                pbImage = pbClearArt
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblSizeClearLogo
                pbImage = pbClearLogo
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainExtrafanarts
                If currExtrafanartsList_Item IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Extrafanarts.Remove(currExtrafanartsList_Item)
                    Image_Extrafanarts_Refresh()
                End If
            Case Enums.ModifierType.MainFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainKeyart
                lblSize = lblSizeKeyart
                pbImage = pbKeyart
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainLandscape
                lblSize = lblSizeLandscape
                pbImage = pbLandscape
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainPoster
                lblSize = lblSizePoster
                pbImage = pbPoster
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
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

    Private Sub Image_Scrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnScrapeBanner.Click,
        btnScrapeCharacterArt.Click,
        btnScrapeClearArt.Click,
        btnScrapeClearLogo.Click,
        btnScrapeExtrafanarts.Click,
        btnScrapeFanart.Click,
        btnScrapeKeyart.Click,
        btnScrapeLandscape.Click,
        btnScrapePoster.Click

        Cursor = Cursors.WaitCursor
        Dim nContainer As New MediaContainers.SearchResultsContainer
        Dim nScrapeModifiers As New Structures.ScrapeModifiers

        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Functions.SetScrapeModifiers(nScrapeModifiers, eImageType, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, nContainer, nScrapeModifiers, True) Then
            Dim iImageCount = 0
            Dim strNoImagesFound As String = String.Empty
            Select Case eImageType
                Case Enums.ModifierType.MainBanner
                    iImageCount = nContainer.MainBanners.Count
                    strNoImagesFound = Master.eLang.GetString(1363, "No Banners found")
                Case Enums.ModifierType.MainClearArt
                    iImageCount = nContainer.MainClearArts.Count
                    strNoImagesFound = Master.eLang.GetString(1102, "No ClearArts found")
                Case Enums.ModifierType.MainClearLogo
                    iImageCount = nContainer.MainClearLogos.Count
                    strNoImagesFound = Master.eLang.GetString(1103, "No ClearLogos found")
                Case Enums.ModifierType.MainCharacterArt
                    iImageCount = nContainer.MainCharacterArts.Count
                    strNoImagesFound = Master.eLang.GetString(1343, "No CharacterArts found")
                Case Enums.ModifierType.MainExtrafanarts, Enums.ModifierType.MainExtrathumbs, Enums.ModifierType.MainFanart
                    iImageCount = nContainer.MainFanarts.Count
                    strNoImagesFound = Master.eLang.GetString(970, "No Fanarts found")
                Case Enums.ModifierType.MainKeyart
                    iImageCount = nContainer.MainKeyarts.Count
                    strNoImagesFound = Master.eLang.GetString(1239, "No Keyarts found")
                Case Enums.ModifierType.MainLandscape
                    iImageCount = nContainer.MainLandscapes.Count
                    strNoImagesFound = Master.eLang.GetString(1197, "No Landscapes found")
                Case Enums.ModifierType.MainPoster
                    iImageCount = nContainer.MainPosters.Count
                    strNoImagesFound = Master.eLang.GetString(972, "No Posters found")
            End Select
            If iImageCount > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, nContainer, nScrapeModifiers) = DialogResult.OK Then
                    Select Case eImageType
                        Case Enums.ModifierType.MainExtrafanarts
                            tmpDBElement.ImagesContainer.Extrafanarts = dlgImgS.Result.ImagesContainer.Extrafanarts
                            Image_Extrafanarts_Refresh()
                        Case Else
                            tmpDBElement.ImagesContainer.SetImageByType(dlgImgS.Result.ImagesContainer.GetImageByType(eImageType), eImageType)
                            If tmpDBElement.ImagesContainer.GetImageByType(eImageType) IsNot Nothing AndAlso
                                tmpDBElement.ImagesContainer.GetImageByType(eImageType).ImageOriginal.LoadFromMemoryStream Then
                                Image_LoadPictureBox(eImageType)
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

    Private Sub MPAA_Changed(sender As Object, e As EventArgs) Handles lbMPAA.SelectedIndexChanged, txtMPAADescription.TextChanged
        If Not lbMPAA.SelectedIndex = 0 AndAlso lbMPAA.SelectedItems.Count = 1 Then
            txtMPAADescription.Enabled = True
            txtMPAA.WatermarkText = String.Format("{0} {1}", lbMPAA.SelectedItem.ToString, txtMPAADescription.Text).Trim
        Else
            txtMPAADescription.Enabled = False
            txtMPAA.WatermarkText = String.Empty
        End If
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

                If i > 0 Then
                    txtMPAADescription.Text = tmpDBElement.TVShow.MPAA.Replace(lbMPAA.Items.Item(i).ToString, String.Empty).Trim
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
        dgvRatings.SuspendLayout()

        For Each tRating In tmpDBElement.TVShow.Ratings.Items.OrderBy(Function(f) Not f.IsDefault)
            Dim i As Integer = dgvRatings.Rows.Add
            dgvRatings.Rows(i).Tag = tRating
            dgvRatings.Rows(i).Cells(colRatingsDefault.Name).Value = tRating.IsDefault
            dgvRatings.Rows(i).Cells(colRatingsSource.Name).Value = tRating.Type
            dgvRatings.Rows(i).Cells(colRatingsValue.Name).Value = tRating.Value
            dgvRatings.Rows(i).Cells(colRatingsMax.Name).Value = tRating.Max
            dgvRatings.Rows(i).Cells(colRatingsVotes.Name).Value = tRating.Votes
        Next

        dgvRatings.ResumeLayout()
    End Sub

    Private Function Ratings_Get() As List(Of MediaContainers.RatingDetails)
        Dim nList As New List(Of MediaContainers.RatingDetails)
        For Each r As DataGridViewRow In dgvRatings.Rows
            If Not r.IsNewRow Then
                Dim dblValue As Double
                Dim iMax As Integer
                Dim iVotes As Integer
                If r.Cells(colRatingsSource.Name).Value IsNot Nothing AndAlso
                    Not String.IsNullOrEmpty(r.Cells(colRatingsSource.Name).Value.ToString.Trim) AndAlso
                    Integer.TryParse(r.Cells(colRatingsMax.Name).Value.ToString, iMax) AndAlso
                    Double.TryParse(r.Cells(colRatingsValue.Name).Value.ToString, dblValue) AndAlso
                    Integer.TryParse(r.Cells(colRatingsVotes.Name).Value.ToString, iVotes) Then
                    nList.Add(New MediaContainers.RatingDetails With {
                              .IsDefault = CBool(r.Cells(colRatingsDefault.Name).Value),
                             .Max = iMax,
                             .Type = r.Cells(colRatingsSource.Name).Value.ToString.Trim,
                             .Value = dblValue,
                             .Votes = iVotes
                             })
                End If
            End If
        Next
        Return nList
    End Function

    Private Sub Tags_Add(sender As Object, e As EventArgs) Handles btnTags_Add.Click
        If Not String.IsNullOrEmpty(cbTags.Text.Trim) AndAlso Not lbTags.Items.Contains(cbTags.Text.Trim) Then lbTags.Items.Add(cbTags.Text.Trim)
    End Sub

    Private Sub Tags_Down(sender As Object, e As EventArgs) Handles btnTags_Down.Click
        If lbTags.SelectedIndex < lbTags.Items.Count - 1 Then
            Dim i = lbTags.SelectedIndex + 2
            lbTags.Items.Insert(i, lbTags.SelectedItem)
            lbTags.Items.RemoveAt(lbTags.SelectedIndex)
            lbTags.SelectedIndex = i - 1
        End If
    End Sub

    Private Sub Tags_Fill()
        If tmpDBElement.TVShow.TagsSpecified Then
            lbTags.Items.AddRange(tmpDBElement.TVShow.Tags.ToArray)
        End If
        'add the rest of all tags to the ComboBox
        cbTags.Items.AddRange(Master.DB.GetAll_Tags)
    End Sub

    Private Sub Tags_Remove(sender As Object, e As EventArgs) Handles btnTags_Remove.Click
        If lbTags.SelectedItem IsNot Nothing Then lbTags.Items.Remove(lbTags.SelectedItem)
    End Sub

    Private Sub Tags_Up(sender As Object, e As EventArgs) Handles btnTags_Up.Click
        If lbTags.SelectedIndex > 0 Then
            Dim i = lbTags.SelectedIndex - 1
            lbTags.Items.Insert(i, lbTags.SelectedItem)
            lbTags.Items.RemoveAt(lbTags.SelectedIndex)
            lbTags.SelectedIndex = i
        End If
    End Sub

    Private Sub TextBox_SelectAll(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

    Private Sub Title_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        btnOK.Enabled = Not String.IsNullOrEmpty(txtTitle.Text.Trim)
    End Sub

    Private Sub Theme_Load(ByVal Theme As MediaContainers.MediaFile)
        txtLocalTheme.Text =
            If(Theme.LocalFilePathSpecified, Theme.LocalFilePath,
            If(Theme.UrlAudioStreamSpecified, Theme.UrlAudioStream,
            If(Theme.UrlWebsiteSpecified, Theme.UrlWebsite, String.Empty)))
    End Sub

    Private Sub Theme_Local_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetThemeLocal.Click
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.ShowPath
            .Filter = FileUtils.Common.GetOpenFileDialogFilter_Theme()
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.Theme = New MediaContainers.MediaFile With {.LocalFilePath = ofdLocalFiles.FileName}
            tmpDBElement.Theme.LoadAndCache()
            Theme_Load(tmpDBElement.Theme)
        End If
    End Sub

    Private Sub Theme_Play_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLocalThemePlay.Click
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

    Private Sub Theme_Remove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveTheme.Click
        tmpDBElement.Theme = New MediaContainers.MediaFile
        txtLocalTheme.Text = String.Empty
    End Sub

    Private Sub Theme_Scrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetThemeScrape.Click
        Dim dlgMediaFile As dlgMediaFileSelect
        Dim tList As New List(Of MediaContainers.MediaFile)
        dlgMediaFile = New dlgMediaFileSelect(Enums.ModifierType.MainTheme)
        If dlgMediaFile.ShowDialog(tmpDBElement, tList, False) = DialogResult.OK Then
            tmpDBElement.Theme = dlgMediaFile.Result
            Theme_Load(tmpDBElement.Theme)
        End If
    End Sub

    Private Sub Theme_TextChanged(sender As Object, e As EventArgs) Handles txtLocalTheme.TextChanged
        btnLocalThemePlay.Enabled = Not String.IsNullOrEmpty(txtLocalTheme.Text)
    End Sub

    Private Sub UniqueIds_Fill()
        dgvUniqueIds.SuspendLayout()

        For Each tId In tmpDBElement.TVShow.UniqueIDs.Items.OrderBy(Function(f) Not f.IsDefault)
            Dim i As Integer = dgvUniqueIds.Rows.Add
            dgvUniqueIds.Rows(i).Tag = tId
            dgvUniqueIds.Rows(i).Cells(colUniqueIdsDefault.Name).Value = tId.IsDefault
            dgvUniqueIds.Rows(i).Cells(colUniqueIdsType.Name).Value = tId.Type
            dgvUniqueIds.Rows(i).Cells(colUniqueIdsValue.Name).Value = tId.Value
        Next

        dgvUniqueIds.ResumeLayout()
    End Sub

    Private Function UniqueIds_Get() As List(Of MediaContainers.Uniqueid)
        Dim nList As New List(Of MediaContainers.Uniqueid)
        For Each r As DataGridViewRow In dgvUniqueIds.Rows
            If Not r.IsNewRow Then
                If r.Cells(colUniqueIdsType.Name).Value IsNot Nothing AndAlso
                    Not String.IsNullOrEmpty(r.Cells(colUniqueIdsType.Name).Value.ToString.Trim) AndAlso
                    r.Cells(colUniqueIdsValue.Name).Value IsNot Nothing AndAlso
                    Not String.IsNullOrEmpty(r.Cells(colUniqueIdsValue.Name).Value.ToString.Trim) Then
                    nList.Add(New MediaContainers.Uniqueid With {
                              .IsDefault = CBool(r.Cells(colUniqueIdsDefault.Name).Value),
                              .Type = r.Cells(colUniqueIdsType.Name).Value.ToString.Trim,
                              .Value = r.Cells(colUniqueIdsValue.Name).Value.ToString.Trim
                              })
                End If
            End If
        Next
        Return nList
    End Function

    Private Sub Watched_CheckedChanged(sender As Object, e As EventArgs) Handles chkWatched.CheckedChanged
        dtpLastPlayed_Date.Enabled = chkWatched.Checked
        dtpLastPlayed_Time.Enabled = chkWatched.Checked
    End Sub

#End Region 'Methods

End Class