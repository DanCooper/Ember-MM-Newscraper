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
Imports System.IO
Imports System.Text.RegularExpressions

Public Class dlgEdit_Movie

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private dragBoxFromMouseDown As Rectangle
    Private fResults As New Containers.ImgResult
    Private iPreviousFrameValue As Integer
    Private lvwActorsSorter As ListViewColumnSorter
    Private pResults As New Containers.ImgResult

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
    Private pnlExtrafanartsList_Panel() As Panel
    Private currExtrafanartsList_Item As MediaContainers.Image

    'Extrathumbs
    Private iExtrathumbsList_NextTop As Integer = 1
    Private lblExtrathumbsList_Resolution() As Label
    Private pnlExtrathumbsList_Panel() As Panel
    Private currExtrathumbsList_Item As MediaContainers.Image

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

    Private Sub Dialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tmpDBElement, True) Then
            pbBanner.AllowDrop = True
            pbClearArt.AllowDrop = True
            pbClearLogo.AllowDrop = True
            pbDiscArt.AllowDrop = True
            pbFanart.AllowDrop = True
            pbKeyArt.AllowDrop = True
            pbLandscape.AllowDrop = True
            pbPoster.AllowDrop = True
            pnlExtrafanarts.AllowDrop = True
            pnlExtrathumbs.AllowDrop = True

            Setup()
            lvwActorsSorter = New ListViewColumnSorter()
            lvActors.ListViewItemSorter = lvwActorsSorter

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            Dim dFileInfoEdit As New dlgFileInfo(tmpDBElement.Movie.FileInfo) With {
                .BackColor = Color.White,
                .Dock = DockStyle.Fill,
                .FormBorderStyle = FormBorderStyle.None,
                .TopLevel = False
            }
            pnlFileInfo.Controls.Add(dFileInfoEdit)
            dFileInfoEdit.Show()

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
        With Master.eLang
            Dim mTitle As String = tmpDBElement.Movie.Title
            Text = String.Concat(.GetString(25, "Edit Movie"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
            btnCancel.Text = .Cancel
            btnChange.Text = .GetString(32, "Change Movie")
            btnFrameLoadVideo.Text = .GetString(307, "Load Video")
            btnFrameSaveAsExtrafanart.Text = .GetString(1050, "Save as Extrafanart")
            btnFrameSaveAsExtrathumb.Text = .GetString(305, "Save as Extrathumb")
            btnFrameSaveAsFanart.Text = .GetString(1049, "Save as Fanart")
            btnManual.Text = .GetString(230, "Manual Edit")
            btnOK.Text = .OK
            btnRescrape.Text = .GetString(716, "Re-Scrape")
            chkLocked.Text = .GetString(43, "Locked")
            chkMarked.Text = .GetString(48, "Marked")
            chkMarkedCustom1.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(.GetString(1191, "Custom"), " #1"))
            chkMarkedCustom2.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(.GetString(1191, "Custom"), " #2"))
            chkMarkedCustom3.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(.GetString(1191, "Custom"), " #3"))
            chkMarkedCustom4.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(.GetString(1191, "Custom"), " #4"))
            chkWatched.Text = .GetString(981, "Watched")
            colActorsName.Text = .GetString(232, "Name")
            colActorsRole.Text = .GetString(233, "Role")
            colActorsThumb.Text = .GetString(234, "Thumb")
            colRatingsVotes.Text = .GetString(244, "Votes")
            lblActors.Text = String.Concat(.GetString(231, "Actors"), ":")
            lblBanner.Text = .GetString(838, "Banner")
            lblCertifications.Text = String.Concat(.GetString(56, "Certifications"), ":")
            lblClearArt.Text = .GetString(1096, "ClearArt")
            lblClearLogo.Text = .GetString(1097, "ClearLogo")
            lblCountries.Text = String.Concat(.GetString(237, "Countries"), ":")
            lblCredits.Text = .GetString(228, "Credits:")
            lblDirectors.Text = String.Concat(.GetString(940, "Directors"), ":")
            lblDiscArt.Text = .GetString(1098, "DiscArt")
            lblExtrafanarts.Text = String.Format("{0} ({1})", .GetString(992, "Extrafanarts"), pnlExtrafanartsList.Controls.Count)
            lblExtrathumbs.Text = String.Format("{0} ({1})", .GetString(153, "Extrathumbs"), pnlExtrafanartsList.Controls.Count)
            lblFanart.Text = .GetString(149, "Fanart")
            lblGenres.Text = .GetString(51, "Genre(s):")
            lblKeyArt.Text = .GetString(296, "KeyArt")
            lblLandscape.Text = .GetString(1059, "Landscape")
            lblLanguage.Text = .GetString(610, "Language")
            lblLinkTrailer.Text = String.Concat(.GetString(227, "Trailer URL"), ":")
            lblMPAA.Text = .GetString(235, "MPAA Rating:")
            lblMPAADesc.Text = .GetString(229, "MPAA Rating Description:")
            lblOriginalTitle.Text = String.Concat(.GetString(302, "Original Title"), ":")
            lblOutline.Text = String.Concat(.GetString(64, "Plot Outline"), ":")
            lblPlot.Text = String.Concat(.GetString(65, "Plot"), ":")
            lblPoster.Text = .GetString(148, "Poster")
            lblPremiered.Text = String.Concat(.GetString(724, "Premiered"), ":")
            lblRatings.Text = String.Concat(.GetString(245, "Ratings"), ":")
            lblRuntime.Text = String.Concat(.GetString(238, "Runtime"), ":")
            lblSortTilte.Text = String.Concat(.GetString(642, "Sort Title"), ":")
            lblStudios.Text = String.Concat(.GetString(395, "Studio"), ":")
            lblTagline.Text = String.Concat(.GetString(397, "Tagline"), ":")
            lblTitle.Text = String.Concat(.GetString(21, "Title"), ":")
            lblTop250.Text = String.Concat(.GetString(591, "Top 250"), ":")
            lblTopDetails.Text = .GetString(224, "Edit the details for the selected movie.")
            lblTopTitle.Text = .GetString(25, "Edit Movie")
            lblTVShowLinks.Text = String.Concat(.GetString(884, "TV Show Links"), ":")
            lblUserRating.Text = String.Concat(.GetString(1467, "User Rating"), ":")
            lblVideoSource.Text = String.Concat(.GetString(824, "Video Source"), ":")
            tpDetails.Text = .GetString(26, "Details")
            tpFrameExtraction.Text = .GetString(256, "Frame Extraction")
            tpMetaData.Text = .GetString(866, "Metadata")
            tsslFilename.Text = tmpDBElement.FileItem.FullPath
        End With

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Description).ToArray)
    End Sub

    Public Overloads Function ShowDialog(ByVal dbElement As Database.DBElement) As DialogResult
        tmpDBElement = dbElement
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
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) > 0 Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex - 1, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex + 1)
            lvActors.Items(iIndex - 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub CertificationsAsMPAARating_Click(sender As Object, e As EventArgs) Handles btnCertificationsAsMPAARating.Click
        txtMPAA.Text = String.Join(" / ", DataGridView_RowsToList(dgvCertifications))
    End Sub

    Private Sub CheckedListBox_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles _
        clbGenres.ItemCheck,
        clbTags.ItemCheck,
        clbTVShowLinks.ItemCheck
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
            Case sender Is btnRemoveBanner, sender Is btnClipboardBanner, sender Is btnDLBanner, sender Is btnLocalBanner, sender Is btnScrapeBanner, sender Is pbBanner
                Return Enums.ModifierType.MainBanner
            Case sender Is btnRemoveClearArt, sender Is btnClipboardClearArt, sender Is btnDLClearArt, sender Is btnLocalClearArt, sender Is btnScrapeClearArt, sender Is pbClearArt
                Return Enums.ModifierType.MainClearArt
            Case sender Is btnRemoveClearLogo, sender Is btnClipboardClearLogo, sender Is btnDLClearLogo, sender Is btnLocalClearLogo, sender Is btnScrapeClearLogo, sender Is pbClearLogo
                Return Enums.ModifierType.MainClearLogo
            Case sender Is btnRemoveDiscArt, sender Is btnClipboardDiscArt, sender Is btnDLDiscArt, sender Is btnLocalDiscArt, sender Is btnScrapeDiscArt, sender Is pbDiscArt
                Return Enums.ModifierType.MainDiscArt
            Case sender Is btnRemoveExtrafanarts, sender Is btnClipboardExtrafanarts, sender Is btnDLExtrafanarts, sender Is btnLocalExtrafanarts, sender Is btnScrapeExtrafanarts, sender Is pnlExtrafanarts
                Return Enums.ModifierType.MainExtrafanarts
            Case sender Is btnRemoveExtrathumbs, sender Is btnClipboardExtrathumbs, sender Is btnDLExtrathumbs, sender Is btnLocalExtrathumbs, sender Is btnScrapeExtrathumbs, sender Is pnlExtrathumbs
                Return Enums.ModifierType.MainExtrathumbs
            Case sender Is btnRemoveFanart, sender Is btnClipboardFanart, sender Is btnDLFanart, sender Is btnLocalFanart, sender Is btnScrapeFanart, sender Is pbFanart
                Return Enums.ModifierType.MainFanart
            Case sender Is btnRemoveKeyArt, sender Is btnClipboardKeyArt, sender Is btnDLKeyArt, sender Is btnLocalKeyArt, sender Is btnScrapeKeyArt, sender Is pbKeyArt
                Return Enums.ModifierType.MainKeyArt
            Case sender Is btnRemoveLandscape, sender Is btnClipboardLandscape, sender Is btnDLLandscape, sender Is btnLocalLandscape, sender Is btnScrapeLandscape, sender Is pbLandscape
                Return Enums.ModifierType.MainLandscape
            Case sender Is btnRemovePoster, sender Is btnClipboardPoster, sender Is btnDLPoster, sender Is btnLocalPoster, sender Is btnScrapePoster, sender Is pbPoster
                Return Enums.ModifierType.MainPoster
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Data_Fill(Optional ByVal DoAll As Boolean = True)
        'Database related part
        With tmpDBElement
            btnManual.Enabled = .NfoPathSpecified
            chkLocked.Checked = .IsLocked
            chkMarked.Checked = .IsMarked
            chkMarkedCustom1.Checked = .IsMarkCustom1
            chkMarkedCustom2.Checked = .IsMarkCustom2
            chkMarkedCustom3.Checked = .IsMarkCustom3
            chkMarkedCustom4.Checked = .IsMarkCustom4
            'Language
            If cbSourceLanguage.Items.Count > 0 Then
                Dim tLanguage As languageProperty = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .Language)
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
        With tmpDBElement.Movie
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
            'Credits
            For Each v In .Credits
                dgvCredits.Rows.Add(New Object() {v})
            Next
            dgvCredits.ClearSelection()
            'Directors
            For Each v In .Directors
                dgvDirectors.Rows.Add(New Object() {v})
            Next
            dgvDirectors.ClearSelection()
            'Genres
            Genres_Fill()
            'Moviesets
            Moviesets_Fill()
            'MPAA
            MPAA_Fill()
            MPAA_Select()
            'OriginalTitle
            txtOriginalTitle.Text = .OriginalTitle
            'Outline
            txtOutline.Text = .Outline
            'Plot
            txtPlot.Text = .Plot
            'Premiered
            dtpPremiered.Text = .Premiered
            'Ratings
            Ratings_Fill()
            'Runtime
            txtRuntime.Text = .Runtime
            'SortTitle
            txtSortTitle.Text = .SortTitle
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
            'Top250
            txtTop250.Text = .Top250.ToString
            'Trailer Link
            txtLinkTrailer.Text = tmpDBElement.Movie.Trailer
            btnLinkTrailerPlay.Enabled = tmpDBElement.Movie.TrailerSpecified
            btnLinkTrailerGet.Enabled = Master.eSettings.Movie.DataSettings.TrailerLink.Enabled
            'TV Show Links
            TVShowLinks_Fill()
            'UserRating
            txtUserRating.Text = .UserRating.ToString
            'Videosource
            Videosources_Fill()
            'Watched/Lastplayed
            chkWatched.Checked = .LastPlayedSpecified
            If .LastPlayedSpecified Then
                dtpLastPlayed.Enabled = True
                Dim timecode As Double = 0
                Double.TryParse(.LastPlayed, timecode)
                If timecode > 0 Then
                    dtpLastPlayed.Text = Functions.ConvertFromUnixTimestamp(timecode).ToString("yyyy-MM-dd HH:mm:ss")
                Else
                    dtpLastPlayed.Text = .LastPlayed
                End If
            End If
        End With

        If DoAll Then
            'Images and TabPages/Panels controll
            Dim bNeedTab_Images As Boolean
            Dim bNeedTab_Other As Boolean

            With tmpDBElement.ImagesContainer
                'Load all images to MemoryStream and Bitmap
                tmpDBElement.LoadAllImages(True, True)

                'Banner
                If Master.eSettings.MovieBannerAnyEnabled Then
                    btnScrapeBanner.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'ClearArt
                If Master.eSettings.MovieClearArtAnyEnabled Then
                    btnScrapeClearArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearArt.Visible = False
                End If

                'ClearLogo
                If Master.eSettings.MovieClearLogoAnyEnabled Then
                    btnScrapeClearLogo.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearLogo.Visible = False
                End If

                'DiscArt
                If Master.eSettings.MovieDiscArtAnyEnabled Then
                    btnScrapeDiscArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    If .DiscArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainDiscArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlDiscArt.Visible = False
                End If

                'Extrafanarts
                If Master.eSettings.MovieExtrafanartsAnyEnabled Then
                    btnScrapeExtrafanarts.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrafanarts)
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

                'Extrathumbs
                If Master.eSettings.MovieExtrathumbsAnyEnabled Then
                    btnScrapeExtrathumbs.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrathumbs)
                    If .Extrathumbs.Count > 0 Then
                        Dim iIndex As Integer = 0
                        For Each tImg As MediaContainers.Image In .Extrathumbs.OrderBy(Function(f) f.Index)
                            Image_Extraimages_Add(String.Concat(tImg.Width, " x ", tImg.Height), iIndex, tImg, Enums.ModifierType.MainExtrathumbs)
                            iIndex += 1
                        Next
                    End If
                    bNeedTab_Images = True
                Else
                    pnlExtrathumbs.Visible = False
                End If

                'Fanart
                If Master.eSettings.MovieFanartAnyEnabled Then
                    btnScrapeFanart.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'KeyArt
                If Master.eSettings.MovieKeyArtAnyEnabled Then
                    btnScrapeKeyArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainKeyArt)
                    If .KeyArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainKeyArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlKeyArt.Visible = False
                End If

                'Landscape
                If Master.eSettings.MovieLandscapeAnyEnabled Then
                    btnScrapeLandscape.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If Master.eSettings.MoviePosterAnyEnabled Then
                    btnScrapePoster.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    If .Poster.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainPoster)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlPoster.Visible = False
                End If
            End With

            'DiscStub
            If tmpDBElement.FileItem.bIsDiscStub Then
                Dim DiscStub As New MediaStub.DiscStub
                DiscStub = MediaStub.LoadDiscStub(tmpDBElement.FileItem.FirstPathFromStack)
                txtMediaStubTitle.Text = DiscStub.Title
                txtMediaStubMessage.Text = DiscStub.Message
                bNeedTab_Other = True
            Else
                gbMediaStub.Visible = False
            End If

            'FrameExtracion
            If tmpDBElement.FileItem.bIsDiscImage OrElse tmpDBElement.FileItem.bIsDiscStub OrElse tmpDBElement.FileItem.bIsArchive Then
                tcEdit.TabPages.Remove(tpFrameExtraction)
            End If

            'Subtitles
            bNeedTab_Other = True
            Subtitles_Load()

            'Theme
            If Master.eSettings.MovieThemeAnyEnabled Then
                btnSetThemeScrape.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                If tmpDBElement.Theme.LocalFilePathSpecified OrElse tmpDBElement.Theme.URLAudioStreamSpecified Then
                    Theme_Load(tmpDBElement.Theme)
                End If
                bNeedTab_Other = True
            Else
                gbTheme.Visible = False
            End If

            'Trailer
            If Master.eSettings.MovieTrailerAnyEnabled Then
                btnSetTrailerScrape.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
                If tmpDBElement.Trailer.LocalFilePathSpecified OrElse tmpDBElement.Trailer.URLVideoStreamSpecified Then
                    Trailer_Load(tmpDBElement.Trailer)
                End If
                bNeedTab_Other = True
            Else
                gbTrailer.Visible = False
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
            .IsLocked = chkLocked.Checked
            .IsMarked = chkMarked.Checked
            .IsMarkCustom1 = chkMarkedCustom1.Checked
            .IsMarkCustom2 = chkMarkedCustom2.Checked
            .IsMarkCustom3 = chkMarkedCustom3.Checked
            .IsMarkCustom4 = chkMarkedCustom4.Checked
            'Language
            If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
                .Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
                .Movie.Language = .Language
            Else
                .Language = "en-US"
                .Movie.Language = .Language
            End If
            'Videosource
            .VideoSource = cbVideoSource.Text.Trim
            .Movie.VideoSource = .VideoSource
        End With

        'Information part
        With tmpDBElement.Movie
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
            'Credits
            .Credits = DataGridView_RowsToList(dgvCredits)
            'Directors
            .Directors = DataGridView_RowsToList(dgvDirectors)
            'Genres
            If clbGenres.CheckedItems.Count > 0 Then
                If clbGenres.CheckedIndices.Contains(0) Then
                    .Genres.Clear()
                Else
                    .Genres = clbGenres.CheckedItems.Cast(Of String).ToList
                    .Genres.Sort()
                End If
            End If
            'Movieset
            .Sets.Clear()
            Select Case cbMovieset.SelectedIndex
                Case -1
                    'new manually added entry
                    If Not String.IsNullOrEmpty(cbMovieset.Text.Trim) Then
                        .Sets.Add(New MediaContainers.SetDetails With {
                                 .Title = cbMovieset.Text.Trim
                                 })
                    End If
                Case 0
                    '[none]
                    'do nothing, Sets has already been cleared
                Case Else
                    'scraped or existing MovieSet has been selected
                    .Sets.Add(DirectCast(cbMovieset.SelectedValue, MediaContainers.SetDetails))
            End Select
            'MPAA
            .MPAA = String.Concat(txtMPAA.Text, " ", txtMPAADesc.Text).Trim
            'OriginalTitle
            .OriginalTitle = txtOriginalTitle.Text.Trim
            'Outline
            .Outline = txtOutline.Text.Trim
            'Plot
            .Plot = txtPlot.Text.Trim
            'Premiered
            .Premiered = dtpPremiered.Value.ToString("yyyy-MM-dd")
            'Ratings
            'TODO
            'Runtime
            .Runtime = txtRuntime.Text.Trim
            'SortTitle
            .SortTitle = txtSortTitle.Text.Trim
            'Studios
            .Studios = DataGridView_RowsToList(dgvStudios)
            'Tagline
            .Tagline = txtTagline.Text.Trim
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
            'Top250
            .Top250 = If(Integer.TryParse(txtTop250.Text.Trim, 0), CInt(txtTop250.Text.Trim), 0)
            'Trailer Link
            .Trailer = txtLinkTrailer.Text.Trim
            'TV Show Links
            If clbTVShowLinks.CheckedItems.Count > 0 Then
                If clbTVShowLinks.CheckedIndices.Contains(0) Then
                    .ShowLinks.Clear()
                Else
                    .ShowLinks = clbTVShowLinks.CheckedItems.Cast(Of String).ToList
                    .ShowLinks.Sort()
                End If
            End If
            'UserRating
            .UserRating = If(Integer.TryParse(txtUserRating.Text.Trim, 0), CInt(txtUserRating.Text.Trim), 0)
            'Watched/Lastplayed
            'if watched-checkbox is checked -> save Playcount=1 in nfo
            If chkWatched.Checked Then
                'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
                If Not .PlayCountSpecified Then
                    .PlayCount = 1
                    .LastPlayed = dtpLastPlayed.Value.ToString("yyyy-MM-dd HH:mm:ss")
                End If
            Else
                'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
                If .PlayCountSpecified Then
                    .PlayCount = 0
                    .LastPlayed = String.Empty
                End If
            End If
        End With

        'DiscStub
        If tmpDBElement.FileItem.bIsDiscStub Then
            Dim StubFile As String = tmpDBElement.FileItem.FirstPathFromStack
            Dim Title As String = txtMediaStubTitle.Text
            Dim Message As String = txtMediaStubMessage.Text
            MediaStub.SaveDiscStub(StubFile, Title, Message)
        End If

        'Subtitles
        Dim removeSubtitles As New List(Of MediaContainers.Subtitle)
        For Each Subtitle In tmpDBElement.Subtitles
            If Subtitle.toRemove Then
                removeSubtitles.Add(Subtitle)
            End If
        Next
        For Each Subtitle In removeSubtitles
            If File.Exists(Subtitle.Path) Then
                File.Delete(Subtitle.Path)
            End If
            tmpDBElement.Subtitles.Remove(Subtitle)
        Next

        If Not Master.eSettings.MovieImagesNotSaveURLToNfo AndAlso pResults.Posters.Count > 0 Then tmpDBElement.Movie.Thumb = pResults.Posters
        If Not Master.eSettings.MovieImagesNotSaveURLToNfo AndAlso fResults.Fanart.Thumb.Count > 0 Then tmpDBElement.Movie.Fanart = pResults.Fanart
    End Sub

    Private Sub DataGridView_Leave(sender As Object, e As EventArgs) Handles _
        dgvCertifications.Leave,
        dgvCountries.Leave,
        dgvCredits.Leave,
        dgvDirectors.Leave,
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

    'Private Sub Extrathumbs_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 AndAlso currExtrathumbImage.Index < tmpDBElement.ImagesContainer.Extrathumbs.Count - 1 Then
    '        Dim iIndex As Integer = currExtrathumbImage.Index
    '        tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index + 1
    '        tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index - 1
    '        Extrathumbs_Refresh()
    '        Extrathumbs_DoSelect(iIndex + 1, CType(pnlExtrathumbsImage(iIndex + 1).Tag, MediaContainers.Image))
    '    End If
    'End Sub

    'Private Sub Extrathumbs_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 AndAlso currExtrathumbImage.Index > 0 Then
    '        Dim iIndex As Integer = currExtrathumbImage.Index
    '        tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index - 1
    '        tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index + 1
    '        Extrathumbs_Refresh()
    '        Extrathumbs_DoSelect(iIndex - 1, CType(pnlExtrathumbsImage(iIndex - 1).Tag, MediaContainers.Image))
    '    End If
    'End Sub

    Private Sub FrameExtraction_DelayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDelay.Tick
        tmrDelay.Stop()
        FrameExtraction_GrabImage()
    End Sub

    Private Sub FrameExtraction_FrameChange() Handles tbFrame.KeyUp, tbFrame.MouseUp
        If tbFrame.Value <> iPreviousFrameValue Then
            FrameExtraction_GrabImage()
        End If
    End Sub

    Private Sub FrameExtraction_GrabImage()
        tbFrame.Enabled = False
        tspbStatus.Style = ProgressBarStyle.Marquee
        tspbStatus.Visible = True
        tsslStatus.Text = Master.eLang.GetString(306, "Extracting Frame...")
        tsslStatus.Visible = True
        btnFrameSaveAsExtrafanart.Enabled = False
        btnFrameSaveAsExtrathumb.Enabled = False
        btnFrameSaveAsFanart.Enabled = False

        Dim nFrame = FFmpeg.FFmpeg.ExtractImageFromVideo(tmpDBElement.FileItem.FirstPathFromStack, tbFrame.Value, True)

        If nFrame IsNot Nothing AndAlso nFrame.Image IsNot Nothing AndAlso nFrame.Image.ImageOriginal.Image IsNot Nothing Then
            pbFrame.Image = nFrame.Image.ImageOriginal.Image
            pbFrame.Tag = nFrame.Image
            tbFrame.Enabled = True
            btnFrameSaveAsExtrafanart.Enabled = True
            btnFrameSaveAsExtrathumb.Enabled = True
            btnFrameSaveAsFanart.Enabled = True
        Else
            lblTime.Text = String.Empty
            tbFrame.Maximum = 0
            tbFrame.Value = 0
            tbFrame.Enabled = False
            btnFrameSaveAsExtrafanart.Enabled = False
            btnFrameSaveAsExtrathumb.Enabled = False
            btnFrameSaveAsFanart.Enabled = False
            btnFrameLoadVideo.Enabled = True
            pbFrame.Image = Nothing
            pbFrame.Tag = Nothing
        End If
        tspbStatus.Visible = False
        tsslStatus.Visible = False
        iPreviousFrameValue = tbFrame.Value

        tbFrame.Focus()
    End Sub

    Private Sub FrameExtraction_LoadVideo_Click(sender As Object, e As EventArgs) Handles btnFrameLoadVideo.Click
        Dim nFrame = FFmpeg.FFmpeg.ExtractImageFromVideo(tmpDBElement.FileItem.FirstPathFromStack, 0, True)
        If nFrame IsNot Nothing AndAlso nFrame.Duration > 0 AndAlso nFrame.Image IsNot Nothing AndAlso nFrame.Image.ImageOriginal IsNot Nothing Then
            tbFrame.Maximum = nFrame.Duration
            tbFrame.Value = 0
            tbFrame.Enabled = True
            pbFrame.Image = nFrame.Image.ImageOriginal.Image
            pbFrame.Tag = nFrame.Image
            btnFrameLoadVideo.Enabled = False
            btnFrameSaveAsExtrafanart.Enabled = True
            btnFrameSaveAsExtrathumb.Enabled = True
            btnFrameSaveAsFanart.Enabled = True
        Else
            tbFrame.Maximum = 0
            tbFrame.Value = 0
            tbFrame.Enabled = False
            pbFrame.Image = Nothing
            pbFrame.Tag = Nothing
        End If
    End Sub

    Private Sub FrameExtraction_SaveAsFanart_Click(sender As Object, e As EventArgs) Handles btnFrameSaveAsFanart.Click
        If pbFrame.Image IsNot Nothing AndAlso pbFrame.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Fanart = DirectCast(pbFrame.Tag, MediaContainers.Image)
            If tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                Image_LoadPictureBox(Enums.ModifierType.MainFanart)
            End If
            btnFrameSaveAsFanart.Enabled = False
        End If
    End Sub

    Private Sub FrameExtraction_SaveAsExtrafanart_Click(sender As Object, e As EventArgs) Handles btnFrameSaveAsExtrafanart.Click
        If pbFrame.Image IsNot Nothing AndAlso pbFrame.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Extrafanarts.Add(DirectCast(pbFrame.Tag, MediaContainers.Image))
            Image_Extrafanarts_Refresh()
            btnFrameSaveAsExtrafanart.Enabled = False
        End If
    End Sub

    Private Sub FrameExtraction_SaveAsExtrathumb_Click(sender As Object, e As EventArgs) Handles btnFrameSaveAsExtrathumb.Click
        If pbFrame.Image IsNot Nothing AndAlso pbFrame.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Extrathumbs.Add(DirectCast(pbFrame.Tag, MediaContainers.Image))
            tmpDBElement.ImagesContainer.Extrathumbs.Last.Index = tmpDBElement.ImagesContainer.Extrathumbs.Count - 1
            Image_Extrathumbs_Refresh()
            btnFrameSaveAsExtrathumb.Enabled = False
        End If
    End Sub

    Private Sub FrameExtraction_Scroll(sender As Object, e As EventArgs) Handles tbFrame.Scroll
        Dim sec2Time As New TimeSpan(0, 0, tbFrame.Value)
        lblTime.Text = String.Format("{0}:{1:00}:{2:00}", sec2Time.Hours, sec2Time.Minutes, sec2Time.Seconds)
    End Sub
    ''' <summary>
    ''' Fills the genre list with selected genres first in list and all known genres from database as second
    ''' </summary>
    Private Sub Genres_Fill()
        clbGenres.Items.Add(Master.eLang.None)
        If tmpDBElement.Movie.GenresSpecified Then
            tmpDBElement.Movie.Genres.Sort()
            clbGenres.Items.AddRange(tmpDBElement.Movie.Genres.ToArray)
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
        btnClipboardClearArt.Click,
        btnClipboardClearLogo.Click,
        btnClipboardDiscArt.Click,
        btnClipboardExtrafanarts.Click,
        btnClipboardExtrathumbs.Click,
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
                Case Enums.ModifierType.MainExtrathumbs
                    tmpDBElement.ImagesContainer.Extrathumbs.AddRange(lstImages)
                    Image_Extrathumbs_Refresh()
                Case Else
                    tmpDBElement.ImagesContainer.SetImageByType(lstImages(0), eImageType)
                    Image_LoadPictureBox(eImageType)
            End Select
        End If
    End Sub

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbBanner.DoubleClick,
        pbClearArt.DoubleClick,
        pbClearLogo.DoubleClick,
        pbDiscArt.DoubleClick,
        pbFanart.DoubleClick,
        pbFrame.DoubleClick,
        pbKeyArt.DoubleClick,
        pbLandscape.DoubleClick,
        pbPoster.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnDLBanner.Click,
        btnDLClearArt.Click,
        btnDLClearLogo.Click,
        btnDLDiscArt.Click,
        btnDLExtrafanarts.Click,
        btnDLExtrathumbs.Click,
        btnDLFanart.Click,
        btnDLKeyArt.Click,
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
                        Case Enums.ModifierType.MainExtrathumbs
                            tmpDBElement.ImagesContainer.Extrathumbs.Add(tImage)
                            Image_Extrathumbs_Refresh()
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
        pbClearArt.DragDrop,
        pbClearLogo.DragDrop,
        pbDiscArt.DragDrop,
        pbFanart.DragDrop,
        pbKeyArt.DragDrop,
        pbLandscape.DragDrop,
        pbPoster.DragDrop,
        pnlExtrafanarts.DragDrop,
        pnlExtrathumbs.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            Select Case eImageType
                Case Enums.ModifierType.MainExtrafanarts
                    tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                    Image_Extrafanarts_Refresh()
                Case Enums.ModifierType.MainExtrathumbs
                    tmpDBElement.ImagesContainer.Extrathumbs.Add(tImage)
                    Image_Extrathumbs_Refresh()
                Case Else
                    tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                    Image_LoadPictureBox(eImageType)
            End Select
        End If
    End Sub

    Private Sub Image_DragEnter(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragEnter,
        pbClearArt.DragEnter,
        pbClearLogo.DragEnter,
        pbDiscArt.DragEnter,
        pbFanart.DragEnter,
        pbKeyArt.DragEnter,
        pbLandscape.DragEnter,
        pbPoster.DragEnter,
        pnlExtrafanarts.DragEnter,
        pnlExtrathumbs.DragEnter
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
            Case Enums.ModifierType.MainExtrathumbs
                iNextTop = iExtrathumbsList_NextTop
                tLabel = lblExtrathumbsList_Resolution
                tMainPanel = pnlExtrathumbsList
                tPanel = pnlExtrathumbsList_Panel
            Case Else
                Return
        End Select

        Try
            If Image.ImageOriginal.Image Is Nothing Then
                Image.LoadAndCache(Enums.ContentType.Movie, True, True)
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
            Case Enums.ModifierType.MainExtrathumbs
                iExtrathumbsList_NextTop = iNextTop + iImageList_Size_Panel.Height + iImageList_DistanceTop
                lblExtrathumbs.Text = String.Format("{0} ({1})", Master.eLang.GetString(153, "Extrathumbs"), pnlExtrathumbsList.Controls.Count)
                lblExtrathumbsList_Resolution = tLabel
                pnlExtrathumbsList_Panel = tPanel
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
                ElseIf pnlExtrathumbsList.Controls.Contains(DirectCast(sender, Label).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrathumbs
                End If
            Case TypeOf (sender) Is Panel
                iIndex = Convert.ToInt32(DirectCast(sender, Panel).Name)
                tImage = DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, Panel).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrafanarts
                ElseIf pnlExtrathumbsList.Controls.Contains(DirectCast(sender, Panel).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrathumbs
                End If
            Case TypeOf (sender) Is PictureBox
                iIndex = Convert.ToInt32(DirectCast(sender, PictureBox).Name)
                tImage = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, PictureBox).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrafanarts
                ElseIf pnlExtrathumbsList.Controls.Contains(DirectCast(sender, PictureBox).Parent) Then
                    eImageType = Enums.ModifierType.MainExtrathumbs
                End If
        End Select
        If tImage IsNot Nothing Then
            Select Case eImageType
                Case Enums.ModifierType.MainExtrafanarts
                    Image_Extrafanarts_DoSelect(iIndex, tImage)
                Case Enums.ModifierType.MainExtrathumbs
                    Image_Extrathumbs_DoSelect(iIndex, tImage)
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

    Private Sub Image_Extrathumbs_DeselectAllImages()
        If pnlExtrathumbsList_Panel IsNot Nothing Then
            For i As Integer = 0 To pnlExtrathumbsList_Panel.Count - 1
                pnlExtrathumbsList_Panel(i).BackColor = Color.White
                lblExtrathumbsList_Resolution(i).BackColor = Color.White
                lblExtrathumbsList_Resolution(i).ForeColor = Color.Black
            Next
        End If
        currExtrathumbsList_Item = Nothing
        btnRemoveExtrathumbs.Enabled = False
    End Sub

    Private Sub Image_Extrafanarts_DoSelect(ByVal iIndex As Integer, ByVal tTag As MediaContainers.Image)
        Image_Extrathumbs_DeselectAllImages()
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

    Private Sub Image_Extrathumbs_DoSelect(ByVal iIndex As Integer, ByVal tTag As MediaContainers.Image)
        Image_Extrafanarts_DeselectAllImages()
        For i As Integer = 0 To pnlExtrathumbsList_Panel.Count - 1
            pnlExtrathumbsList_Panel(i).BackColor = Color.White
            lblExtrathumbsList_Resolution(i).BackColor = Color.White
            lblExtrathumbsList_Resolution(i).ForeColor = Color.Black
        Next

        pnlExtrathumbsList_Panel(iIndex).BackColor = Color.Gray
        lblExtrathumbsList_Resolution(iIndex).BackColor = Color.Gray
        lblExtrathumbsList_Resolution(iIndex).ForeColor = Color.White
        currExtrathumbsList_Item = tTag
        btnRemoveExtrathumbs.Enabled = True
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

    Private Sub Image_Extrathumbs_Refresh() Handles btnRefreshExtrathumbs.Click
        Image_Extrathumbs_DeselectAllImages()
        iExtrathumbsList_NextTop = iImageList_DistanceTop
        While pnlExtrathumbsList.Controls.Count > 0
            pnlExtrathumbsList.Controls(0).Dispose()
        End While

        If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 Then
            Dim iIndex As Integer = 0
            For Each img As MediaContainers.Image In tmpDBElement.ImagesContainer.Extrathumbs
                Image_Extraimages_Add(String.Concat(img.Width, " x ", img.Height), iIndex, img, Enums.ModifierType.MainExtrathumbs)
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
            Case Enums.ModifierType.MainClearArt
                lblSize = lblSizeClearArt
                pbImage = pbClearArt
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblSizeClearLogo
                pbImage = pbClearLogo
            Case Enums.ModifierType.MainDiscArt
                lblSize = lblSizeDiscArt
                pbImage = pbDiscArt
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
        btnLocalClearArt.Click,
        btnLocalClearLogo.Click,
        btnLocalDiscArt.Click,
        btnLocalExtrafanarts.Click,
        btnLocalExtrathumbs.Click,
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
                    Case Enums.ModifierType.MainExtrathumbs
                        tmpDBElement.ImagesContainer.Extrathumbs.Add(tImage)
                        Image_Extrathumbs_Refresh()
                    Case Else
                        tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                        Image_LoadPictureBox(eImageType)
                End Select
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnRemoveBanner.Click,
        btnRemoveClearArt.Click,
        btnRemoveClearLogo.Click,
        btnRemoveDiscArt.Click,
        btnRemoveExtrafanarts.Click,
        btnRemoveExtrathumbs.Click,
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
            Case Enums.ModifierType.MainClearArt
                lblSize = lblSizeClearArt
                pbImage = pbClearArt
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblSizeClearLogo
                pbImage = pbClearLogo
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainDiscArt
                lblSize = lblSizeDiscArt
                pbImage = pbDiscArt
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.MainExtrafanarts
                If currExtrafanartsList_Item IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Extrafanarts.Remove(currExtrafanartsList_Item)
                    Image_Extrafanarts_Refresh()
                End If
            Case Enums.ModifierType.MainExtrathumbs
                If currExtrathumbsList_Item IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Extrathumbs.Remove(currExtrathumbsList_Item)
                    Image_Extrathumbs_Refresh()
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
        btnScrapeClearArt.Click,
        btnScrapeClearLogo.Click,
        btnScrapeDiscArt.Click,
        btnScrapeExtrafanarts.Click,
        btnScrapeExtrathumbs.Click,
        btnScrapeFanart.Click,
        btnScrapeKeyArt.Click,
        btnScrapeLandscape.Click,
        btnScrapePoster.Click
        Cursor = Cursors.WaitCursor
        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, eImageType, True)
        If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
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
                Case Enums.ModifierType.MainDiscArt
                    iImageCount = aContainer.MainDiscArts.Count
                    strNoImagesFound = Master.eLang.GetString(1104, "No DiscArts found")
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
                Dim dlgImgS = New dlgImageSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    Select Case eImageType
                        Case Enums.ModifierType.MainExtrafanarts
                            tmpDBElement.ImagesContainer.Extrafanarts = dlgImgS.Result.ImagesContainer.Extrafanarts
                            Image_Extrafanarts_Refresh()
                        Case Enums.ModifierType.MainExtrathumbs
                            tmpDBElement.ImagesContainer.Extrathumbs = dlgImgS.Result.ImagesContainer.Extrathumbs
                            Image_Extrathumbs_Refresh()
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

    Private Sub Movie_EditManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        If dlgManualEdit.ShowDialog(tmpDBElement.NfoPath) = DialogResult.OK Then
            tmpDBElement.Movie = Info.LoadFromNFO_Movie(tmpDBElement.NfoPath, tmpDBElement.IsSingle)
            Data_Fill(False)
        End If
    End Sub

    Private Sub Moviesets_Fill()
        Dim items As New Dictionary(Of MediaContainers.SetDetails, String)
        items.Add(New MediaContainers.SetDetails, Master.eLang.None)
        If tmpDBElement.Movie.SetsSpecified Then
            items.Add(tmpDBElement.Movie.Sets.First, tmpDBElement.Movie.Sets.First.Title)
        End If
        For Each nSet In Master.DB.GetAllMovieSetDetails.Where(Function(f) Not items.Keys.Contains(f) AndAlso Not items.Values.Contains(f.Title))
            items.Add(nSet, nSet.Title)
        Next
        cbMovieset.DataSource = items.ToList
        cbMovieset.DisplayMember = "Value"
        cbMovieset.ValueMember = "Key"
        If tmpDBElement.Movie.SetsSpecified Then
            cbMovieset.SelectedIndex = 1
        Else
            cbMovieset.SelectedIndex = 0
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
        If Master.eSettings.Movie.DataSettings.MPAANotRatedValueSpecified Then lbMPAA.Items.Add(Master.eSettings.Movie.DataSettings.MPAANotRatedValue)
        lbMPAA.Items.AddRange(MediaFlags.GetRatingList_Movie)
    End Sub

    Private Sub MPAA_Select()
        If tmpDBElement.Movie.MPAASpecified Then
            If Master.eSettings.Movie.DataSettings.CertificationsOnlyValue Then
                Dim sItem As String = String.Empty
                For i As Integer = 0 To lbMPAA.Items.Count - 1
                    sItem = lbMPAA.Items(i).ToString
                    If sItem.Contains(":") AndAlso sItem.Split(Convert.ToChar(":"))(1) = tmpDBElement.Movie.MPAA Then
                        lbMPAA.SelectedIndex = i
                        lbMPAA.TopIndex = i
                        Exit For
                    End If
                Next
            Else
                Dim i As Integer = 0
                For ctr As Integer = 0 To lbMPAA.Items.Count - 1
                    If tmpDBElement.Movie.MPAA.ToLower.StartsWith(lbMPAA.Items.Item(ctr).ToString.ToLower) Then
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
                    strMPAADesc = tmpDBElement.Movie.MPAA.Replace(strMPAA, String.Empty).Trim
                    txtMPAA.Text = strMPAA
                    txtMPAADesc.Text = strMPAADesc
                Else
                    txtMPAA.Text = tmpDBElement.Movie.MPAA
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
        For Each tRating As MediaContainers.RatingDetails In tmpDBElement.Movie.Ratings
            lvItem = lvRatings.Items.Add(tRating.Name)
            lvItem.SubItems.Add(tRating.Value.ToString)
            lvItem.SubItems.Add(tRating.Votes.ToString)
            lvItem.SubItems.Add(tRating.Max.ToString)
            lvItem.Tag = tRating
        Next
    End Sub

    Private Sub Subtitles_Delete()
        If lvSubtitles.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
            If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                tmpDBElement.Subtitles(Convert.ToInt16(i.Text)).toRemove = True
            End If
            Subtitles_Load()
        End If
    End Sub

    Private Sub Subtitles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSubtitles.DoubleClick
        If lvSubtitles.SelectedItems.Count > 0 Then
            If lvSubtitles.SelectedItems.Item(0).Tag.ToString <> "Header" Then
                Subtitles_Edit()
            End If
        End If
    End Sub

    Private Sub Subtitles_Edit()
        If lvSubtitles.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
            Dim tmpFileInfo As New MediaContainers.FileInfo
            tmpFileInfo.StreamDetails.Subtitle.AddRange(tmpDBElement.Subtitles)
            Using dEditStream As New dlgFIStreamEditor
                Dim stream As Object = dEditStream.ShowDialog(i.Tag.ToString, tmpFileInfo, Convert.ToInt16(i.Text))
                If Not stream Is Nothing Then
                    If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                        tmpDBElement.Subtitles(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Subtitle)
                    End If
                    Subtitles_Load()
                End If
            End Using
        End If
    End Sub

    Private Sub Subtitles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSubtitles.KeyDown
        If e.KeyCode = Keys.Delete Then Subtitles_Delete()
    End Sub

    Private Sub Subtitles_Load()
        Dim c As Integer
        Dim g As New ListViewGroup
        Dim i As New ListViewItem
        lvSubtitles.Groups.Clear()
        lvSubtitles.Items.Clear()

        If tmpDBElement.Subtitles.Count > 0 Then
            g = New ListViewGroup
            g.Header = Master.eLang.GetString(597, "Subtitle Stream")
            lvSubtitles.Groups.Add(g)
            c = 1
            ' Fake Group Header
            i = New ListViewItem
            'i.UseItemStyleForSubItems = False
            i.ForeColor = Color.DarkBlue
            i.Tag = "Header"
            i.Text = String.Empty
            i.SubItems.Add(Master.eLang.GetString(60, "File Path"))
            i.SubItems.Add(Master.eLang.GetString(610, "Language"))
            i.SubItems.Add(Master.eLang.GetString(1288, "Type"))
            i.SubItems.Add(Master.eLang.GetString(1287, "Forced"))

            g.Items.Add(i)
            lvSubtitles.Items.Add(i)
            Dim s As MediaContainers.Subtitle
            For c = 0 To tmpDBElement.Subtitles.Count - 1
                s = tmpDBElement.Subtitles(c)
                If Not s Is Nothing Then
                    i = New ListViewItem
                    i.Tag = Master.eLang.GetString(597, "Subtitle Stream")
                    i.Text = c.ToString
                    i.SubItems.Add(s.Path)
                    i.SubItems.Add(s.LongLanguage)
                    i.SubItems.Add(s.SubsType)
                    i.SubItems.Add(If(s.Forced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))

                    If s.toRemove Then
                        i.ForeColor = Color.Red
                    End If

                    g.Items.Add(i)
                    lvSubtitles.Items.Add(i)
                End If
            Next
        End If
    End Sub

    Private Function Subtitles_Read(ByVal sPath As String) As String
        Dim sText As String = String.Empty

        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            Dim objReader As New StreamReader(sPath)
            sText = objReader.ReadToEnd
            objReader.Close()
            Return sText
        End If

        Return String.Empty
    End Function

    Private Sub Subtitles_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSubtitle.Click
        Subtitles_Delete()
    End Sub

    Private Sub Subtitles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvSubtitles.SelectedIndexChanged
        If lvSubtitles.SelectedItems.Count > 0 Then
            If lvSubtitles.SelectedItems.Item(0).Tag.ToString = "Header" Then
                lvSubtitles.SelectedItems.Clear()
                btnRemoveSubtitle.Enabled = False
                txtSubtitlesPreview.Clear()
            Else
                btnRemoveSubtitle.Enabled = True
                txtSubtitlesPreview.Text = Subtitles_Read(lvSubtitles.SelectedItems.Item(0).SubItems(1).Text.ToString)
            End If
        Else
            btnRemoveSubtitle.Enabled = False
            txtSubtitlesPreview.Clear()
        End If
    End Sub

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcEdit.SelectedIndexChanged
        Image_Extrafanarts_DeselectAllImages()
        Image_Extrathumbs_DeselectAllImages()
        lvSubtitles.SelectedItems.Clear()
    End Sub
    ''' <summary>
    ''' Fills the tag list with selected tags first in list and all known gtagsenres from database as second
    ''' </summary>
    Private Sub Tags_Fill()
        clbTags.Items.Add(Master.eLang.None)
        If tmpDBElement.Movie.TagsSpecified Then
            tmpDBElement.Movie.Tags.Sort()
            clbTags.Items.AddRange(tmpDBElement.Movie.Tags.ToArray)
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

    Private Sub TextBox_NumericOnly(sender As Object, e As KeyPressEventArgs) Handles _
        txtTop250.KeyPress,
        txtUserRating.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_SelectAll(ByVal sender As Object, e As KeyEventArgs) Handles txtOutline.KeyDown, txtPlot.KeyDown
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
        If Not AddonsManager.Instance.ScrapeTheme_Movie(tmpDBElement, Enums.ModifierType.MainTheme, tList) Then
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

    Private Sub Trailer_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerDL.Click
        Dim tResults As New MediaContainers.Trailer
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)
        dlgTrlS = New dlgTrailerSelect()
        If dlgTrlS.ShowDialog(tmpDBElement, tList, False, True, True) = DialogResult.OK Then
            tResults = dlgTrlS.Result
            tmpDBElement.Trailer = tResults
            Trailer_Load(tmpDBElement.Trailer)
        End If
    End Sub

    Private Sub Trailer_Load(ByVal Trailer As MediaContainers.Trailer)
        txtLocalTrailer.Text =
            If(Trailer.LocalFilePathSpecified, Trailer.LocalFilePath,
            If(Trailer.URLVideoStreamSpecified, Trailer.URLVideoStream,
            If(Trailer.URLWebsiteSpecified, Trailer.URLWebsite, String.Empty)))
    End Sub

    Private Sub Trailer_Local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerLocal.Click
        Dim strValidExtesions As String() = Master.eSettings.Options.FileSystem.ValidVideoExtensions.ToArray
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.FileItem.MainPath.FullName
            .Filter = FileUtils.Common.GetOpenFileDialogFilter_Video(Master.eLang.GetString(1195, "Trailers"))
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.Trailer = New MediaContainers.Trailer With {.LocalFilePath = ofdLocalFiles.FileName}
            tmpDBElement.Trailer.LoadAndCache()
            Trailer_Load(tmpDBElement.Trailer)
        End If
    End Sub

    Private Sub Trailer_Play_Click(sender As Object, e As EventArgs) Handles btnLocalTrailerPlay.Click
        Try
            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(txtLocalTrailer.Text) Then
                tPath = String.Concat("""", txtLocalTrailer.Text, """")
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                Process.Start(tPath)
            End If
        Catch
            MessageBox.Show(Master.eLang.GetString(270, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), Master.eLang.GetString(271, "Error Playing Trailer"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub Trailer_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTrailer.Click
        tmpDBElement.Trailer = New MediaContainers.Trailer
        txtLocalTrailer.Text = String.Empty
    End Sub

    Private Sub Trailer_Scrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerScrape.Click
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)
        dlgTrlS = New dlgTrailerSelect()
        If dlgTrlS.ShowDialog(tmpDBElement, tList, False, True, True) = DialogResult.OK Then
            tmpDBElement.Trailer = dlgTrlS.Result
            Trailer_Load(tmpDBElement.Trailer)
        End If
    End Sub

    Private Sub Trailer_TextChanged(sender As Object, e As EventArgs) Handles txtLocalTrailer.TextChanged
        btnLocalTrailerPlay.Enabled = Not String.IsNullOrEmpty(txtLocalTrailer.Text)
    End Sub

    Private Sub TrailerLink_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinkTrailerPlay.Click
        Try
            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(txtLinkTrailer.Text) Then
                tPath = String.Concat("""", txtLinkTrailer.Text, """")
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Regex.IsMatch(tPath, "plugin:\/\/plugin\.video\.youtube\/\?action=play_video&videoid=") Then
                    tPath = tPath.Replace("plugin://plugin.video.youtube/?action=play_video&videoid=", "http://www.youtube.com/watch?v=")
                End If
                Process.Start(tPath)
            End If
        Catch
            MessageBox.Show(Master.eLang.GetString(270, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), Master.eLang.GetString(271, "Error Playing Trailer"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub TrailerLink_Scrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinkTrailerGet.Click
        Dim tResults As New MediaContainers.Trailer
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)
        Dim tURL As String = String.Empty

        Try
            dlgTrlS = New dlgTrailerSelect()
            If dlgTrlS.ShowDialog(tmpDBElement, tList, True, True, True) = DialogResult.OK Then
                tURL = dlgTrlS.Result.URLWebsite
            End If

            If Not String.IsNullOrEmpty(tURL) Then
                btnLinkTrailerPlay.Enabled = True
                txtLinkTrailer.Text = tURL
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub TrailerLink_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLinkTrailer.TextChanged
        If StringUtils.isValidURL(txtLinkTrailer.Text) Then
            btnLinkTrailerPlay.Enabled = True
        Else
            btnLinkTrailerPlay.Enabled = False
        End If
    End Sub
    ''' <summary>
    ''' Fills the genre list with selected genres first in list and all known genres from database as second
    ''' </summary>
    Private Sub TVShowLinks_Fill()
        clbTVShowLinks.Items.Add(Master.eLang.None)
        If tmpDBElement.Movie.ShowLinksSpecified Then
            tmpDBElement.Movie.ShowLinks.Sort()
            clbTVShowLinks.Items.AddRange(tmpDBElement.Movie.ShowLinks.ToArray)
            'enable all selected tv shows, skip the first entry "[none]"
            For i As Integer = 1 To clbTVShowLinks.Items.Count - 1
                clbTVShowLinks.SetItemChecked(i, True)
            Next
        Else
            'select "[none]" if no tv show link has been specified
            clbTVShowLinks.SetItemChecked(0, True)
        End If
        'add the rest of all tv shows
        clbTVShowLinks.Items.AddRange(Master.DB.GetAllTVShowTitles.Where(Function(f) Not clbTVShowLinks.Items.Contains(f)).ToArray)
    End Sub

    Private Sub UserRating_TextChanged(sender As Object, e As EventArgs) Handles txtUserRating.TextChanged
        If Not String.IsNullOrEmpty(txtUserRating.Text) Then
            Dim iUserRating As Integer
            If Integer.TryParse(txtUserRating.Text, iUserRating) Then
                If iUserRating > 10 Then
                    txtUserRating.Text = "10"
                    txtUserRating.Select(txtUserRating.Text.Length, 0)
                End If
            End If
        End If
    End Sub

    Private Sub Videosources_Fill()
        If tmpDBElement.Movie.VideoSourceSpecified Then
            cbVideoSource.Items.Add(tmpDBElement.Movie.VideoSource)
            cbVideoSource.SelectedItem = tmpDBElement.Movie.VideoSource
        End If
        cbVideoSource.Items.AddRange(Master.DB.GetAllVideoSources_Movie.Where(Function(f) Not cbVideoSource.Items.Contains(f)).ToArray)
    End Sub

    Private Sub Watched_CheckedChanged(sender As Object, e As EventArgs) Handles chkWatched.CheckedChanged
        dtpLastPlayed.Enabled = chkWatched.Checked
    End Sub

#End Region 'Methods

End Class