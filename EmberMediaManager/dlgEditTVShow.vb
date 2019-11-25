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
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tmpDBElement, True) Then
            pbBanner.AllowDrop = True
            pbCharacterArt.AllowDrop = True
            pbClearArt.AllowDrop = True
            pbClearLogo.AllowDrop = True
            pbFanart.AllowDrop = True
            pbKeyArt.AllowDrop = True
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
        'chkMarkedCustom1.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
        'chkMarkedCustom2.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
        'chkMarkedCustom3.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
        'chkMarkedCustom4.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
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
        lblKeyArt.Text = Master.eLang.GetString(296, "KeyArt")
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

    Private Function ConvertControlToImageType(ByVal sender As System.Object) As Enums.ModifierType
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
            Case sender Is btnRemoveKeyArt, sender Is btnDLKeyArt, sender Is btnLocalKeyArt, sender Is btnScrapeKeyArt, sender Is btnClipboardKeyArt, sender Is pbKeyArt
                Return Enums.ModifierType.MainKeyArt
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
                    btnScrapeBanner.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'CharacterArt
                If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                    btnScrapeCharacterArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    If .CharacterArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainCharacterArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlCharacterArt.Visible = False
                End If

                'ClearArt
                If Master.eSettings.TVShowClearArtAnyEnabled Then
                    btnScrapeClearArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearArt.Visible = False
                End If

                'ClearLogo
                If Master.eSettings.TVShowClearLogoAnyEnabled Then
                    btnScrapeClearLogo.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearLogo.Visible = False
                End If

                'Extrafanarts
                If Master.eSettings.TVShowExtrafanartsAnyEnabled Then
                    btnScrapeExtrafanarts.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
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
                    btnScrapeFanart.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainExtrafanarts)
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'KeyArt
                If Master.eSettings.TVShowKeyArtAnyEnabled Then
                    btnScrapeKeyArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainKeyArt)
                    If .KeyArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainKeyArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlKeyArt.Visible = False
                End If

                'Landscape
                If Master.eSettings.TVShowLandscapeAnyEnabled Then
                    btnScrapeLandscape.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If Master.eSettings.TVShowPosterAnyEnabled Then
                    btnScrapePoster.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
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
                btnSetThemeScrape.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
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

    Private Sub Image_Clipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnClipboardBanner.Click,
        btnClipboardCharacterArt.Click,
        btnClipboardClearArt.Click,
        btnClipboardClearLogo.Click,
        btnClipboardExtrafanarts.Click,
        btnClipboardFanart.Click,
        btnClipboardKeyArt.Click,
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
        pbKeyArt.DoubleClick,
        pbLandscape.DoubleClick,
        pbPoster.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            AddonsManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnDLBanner.Click,
        btnDLCharacterArt.Click,
        btnDLClearArt.Click,
        btnDLClearLogo.Click,
        btnDLExtrafanarts.Click,
        btnDLFanart.Click,
        btnDLLandscape.Click,
        btnDLPoster.Click, btnDLKeyArt.Click
        Using dImgManual As New dlgImgManual
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
        pbKeyArt.DragDrop,
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
        pbKeyArt.DragEnter,
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
            Case Enums.ModifierType.MainKeyArt
                lblSize = lblSizeKeyArt
                pbImage = pbKeyArt
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

    Private Sub Image_Local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnLocalBanner.Click,
        btnDLCharacterArt.Click,
        btnLocalClearArt.Click,
        btnLocalClearLogo.Click,
        btnLocalExtrafanarts.Click,
        btnLocalFanart.Click,
        btnLocalKeyArt.Click,
        btnLocalLandscape.Click,
        btnLocalPoster.Click
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.FileItem.MainPath.FullName
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

    Private Sub Image_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnRemoveBanner.Click,
        btnRemoveCharacterArt.Click,
        btnRemoveClearArt.Click,
        btnRemoveClearLogo.Click,
        btnRemoveExtrafanarts.Click,
        btnRemoveFanart.Click,
        btnRemoveKeyArt.Click,
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
            Case Enums.ModifierType.MainKeyArt
                lblSize = lblSizeKeyArt
                pbImage = pbKeyArt
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

    Private Sub Image_Scrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnScrapeBanner.Click,
        btnScrapeCharacterArt.Click,
        btnScrapeClearArt.Click,
        btnScrapeClearLogo.Click,
        btnScrapeExtrafanarts.Click,
        btnScrapeFanart.Click,
        btnScrapeKeyArt.Click,
        btnScrapeLandscape.Click,
        btnScrapePoster.Click
        Cursor = Cursors.WaitCursor
        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, eImageType, True)
        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            Dim iImageCount = 0
            Dim strNoImagesFound As String = String.Empty
            Select Case eImageType
                Case Enums.ModifierType.MainBanner
                    iImageCount = aContainer.MainBanners.Count
                    strNoImagesFound = Master.eLang.GetString(1363, "No Banners found")
                Case Enums.ModifierType.MainClearArt
                    iImageCount = aContainer.MainClearArts.Count
                    strNoImagesFound = Master.eLang.GetString(1102, "No ClearArts found")
                Case Enums.ModifierType.MainClearLogo
                    iImageCount = aContainer.MainClearLogos.Count
                    strNoImagesFound = Master.eLang.GetString(1103, "No ClearLogos found")
                Case Enums.ModifierType.MainCharacterArt
                    iImageCount = aContainer.MainCharacterArts.Count
                    strNoImagesFound = Master.eLang.GetString(1343, "No CharacterArts found")
                Case Enums.ModifierType.MainExtrafanarts, Enums.ModifierType.MainExtrathumbs, Enums.ModifierType.MainFanart
                    iImageCount = aContainer.MainFanarts.Count
                    strNoImagesFound = Master.eLang.GetString(970, "No Fanarts found")
                Case Enums.ModifierType.MainKeyArt
                    iImageCount = aContainer.MainKeyArts.Count
                    strNoImagesFound = Master.eLang.GetString(855, "No KeyArts found")
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
        If Not AddonsManager.Instance.ScrapeTheme_TVShow(tmpDBElement, Enums.ModifierType.MainTheme, tList) Then
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