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

Public Class dlgEditMovie

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
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub Dialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tmpDBElement, True) Then
            pbBanner.AllowDrop = True
            pbClearArt.AllowDrop = True
            pbClearLogo.AllowDrop = True
            pbDiscArt.AllowDrop = True
            pbFanart.AllowDrop = True
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

            Dim dFileInfoEdit As New dlgFileInfo(tmpDBElement, False)
            dFileInfoEdit.TopLevel = False
            dFileInfoEdit.FormBorderStyle = FormBorderStyle.None
            dFileInfoEdit.BackColor = Color.White
            dFileInfoEdit.btnClose.Visible = False
            pnlFileInfo.Controls.Add(dFileInfoEdit)
            Dim oldwidth As Integer = dFileInfoEdit.Width
            dFileInfoEdit.Width = pnlFileInfo.Width
            dFileInfoEdit.Height = pnlFileInfo.Height
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
        Dim mTitle As String = tmpDBElement.Movie.Title
        Text = String.Concat(Master.eLang.GetString(25, "Edit Movie"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        btnCancel.Text = Master.eLang.Cancel
        btnChange.Text = Master.eLang.GetString(32, "Change Movie")
        btnFrameLoadVideo.Text = Master.eLang.GetString(307, "Load Video")
        btnFrameSaveAsExtrafanart.Text = Master.eLang.GetString(1050, "Save as Extrafanart")
        btnFrameSaveAsExtrathumb.Text = Master.eLang.GetString(305, "Save as Extrathumb")
        btnFrameSaveAsFanart.Text = Master.eLang.GetString(1049, "Save as Fanart")
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
        lblClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        lblClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblCountries.Text = String.Concat(Master.eLang.GetString(237, "Countries"), ":")
        lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        lblDirectors.Text = String.Concat(Master.eLang.GetString(940, "Directors"), ":")
        lblDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        lblExtrafanarts.Text = String.Format("{0} ({1})", Master.eLang.GetString(992, "Extrafanarts"), pnlExtrafanartsList.Controls.Count)
        lblExtrathumbs.Text = String.Format("{0} ({1})", Master.eLang.GetString(153, "Extrathumbs"), pnlExtrafanartsList.Controls.Count)
        lblFanart.Text = Master.eLang.GetString(149, "Fanart")
        lblGenres.Text = Master.eLang.GetString(51, "Genre(s):")
        lblLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        lblLanguage.Text = Master.eLang.GetString(610, "Language")
        lblLinkTrailer.Text = String.Concat(Master.eLang.GetString(227, "Trailer URL"), ":")
        lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        lblMPAADesc.Text = Master.eLang.GetString(229, "MPAA Rating Description:")
        lblOriginalTitle.Text = String.Concat(Master.eLang.GetString(302, "Original Title"), ":")
        lblOutline.Text = String.Concat(Master.eLang.GetString(64, "Plot Outline"), ":")
        lblPlot.Text = String.Concat(Master.eLang.GetString(65, "Plot"), ":")
        lblPoster.Text = Master.eLang.GetString(148, "Poster")
        lblRatings.Text = String.Concat(Master.eLang.GetString(245, "Ratings"), ":")
        lblReleaseDate.Text = Master.eLang.GetString(236, "Release Date:")
        lblRuntime.Text = String.Concat(Master.eLang.GetString(238, "Runtime"), ":")
        lblSortTilte.Text = String.Concat(Master.eLang.GetString(642, "Sort Title"), ":")
        lblStudios.Text = String.Concat(Master.eLang.GetString(395, "Studio"), ":")
        lblTagline.Text = String.Concat(Master.eLang.GetString(397, "Tagline"), ":")
        lblTitle.Text = String.Concat(Master.eLang.GetString(21, "Title"), ":")
        lblTop250.Text = String.Concat(Master.eLang.GetString(591, "Top 250"), ":")
        lblTopDetails.Text = Master.eLang.GetString(224, "Edit the details for the selected movie.")
        lblTopTitle.Text = Master.eLang.GetString(25, "Edit Movie")
        lblUserRating.Text = String.Concat(Master.eLang.GetString(1467, "User Rating"), ":")
        lblVideoSource.Text = String.Concat(Master.eLang.GetString(824, "Video Source"), ":")
        lblYear.Text = String.Concat(Master.eLang.GetString(278, "Year"), ":")
        tpDetails.Text = Master.eLang.GetString(26, "Details")
        tpFrameExtraction.Text = Master.eLang.GetString(256, "Frame Extraction")
        tpMetaData.Text = Master.eLang.GetString(866, "Metadata")
        tsslFilename.Text = tmpDBElement.FileItem.FullPath

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
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
            Case sender Is btnRemoveClearArt, sender Is btnSetClearArtDL, sender Is btnSetClearArtLocal, sender Is btnSetClearArtScrape
                Return Enums.ModifierType.MainClearArt
            Case sender Is btnRemoveClearLogo, sender Is btnSetClearLogoDL, sender Is btnSetClearLogoLocal, sender Is btnSetClearLogoScrape
                Return Enums.ModifierType.MainClearLogo
            Case sender Is btnRemoveDiscArt, sender Is btnSetDiscArtDL, sender Is btnSetDiscArtLocal, sender Is btnSetDiscArtScrape
                Return Enums.ModifierType.MainDiscArt
            Case sender Is btnExtrafanartsRemove, sender Is btnSetExtrafanartsDL, sender Is btnSetExtrafanartsLocal, sender Is btnSetExtrafanartsScrape
                Return Enums.ModifierType.MainExtrafanarts
            Case sender Is btnExtrathumbsRemove, sender Is btnSetExtrathumbsDL, sender Is btnSetExtrathumbsLocal, sender Is btnSetExtrathumbsScrape
                Return Enums.ModifierType.MainExtrathumbs
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
            'Ratings
            Ratings_Fill()
            'ReleaseDate
            dtpReleaseDate.Text = .ReleaseDate
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
            btnLinkTrailerGet.Enabled = Master.eSettings.DefaultOptions_Movie.bMainTrailer
            'UserRating
            txtUserRating.Text = .UserRating.ToString
            'Videosource
            If tmpDBElement.FileItemSpecified AndAlso Not .VideoSourceSpecified Then
                Dim vSource As String = APIXML.GetVideoSource(tmpDBElement.FileItem, False)
                If Not String.IsNullOrEmpty(vSource) Then
                    tmpDBElement.VideoSource = vSource
                    .VideoSource = tmpDBElement.VideoSource
                ElseIf Not tmpDBElement.VideoSourceSpecified AndAlso AdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                    tmpDBElement.VideoSource = AdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(tmpDBElement.FileItem.FirstStackedPath)), String.Empty, "*EmberAPP")
                    .VideoSource = tmpDBElement.VideoSource
                ElseIf .VideoSourceSpecified Then
                    tmpDBElement.VideoSource = .VideoSource
                End If
            End If
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
            'Year
            txtYear.Text = .Year.ToString
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
                    btnSetBannerScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'ClearArt
                If Master.eSettings.MovieClearArtAnyEnabled Then
                    btnSetClearArtScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearArt.Visible = False
                End If

                'ClearLogo
                If Master.eSettings.MovieClearLogoAnyEnabled Then
                    btnSetClearLogoScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearLogo.Visible = False
                End If

                'DiscArt
                If Master.eSettings.MovieDiscArtAnyEnabled Then
                    btnSetDiscArtScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    If .DiscArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainDiscArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlDiscArt.Visible = False
                End If

                'Extrafanarts
                If Master.eSettings.MovieExtrafanartsAnyEnabled Then
                    btnSetExtrafanartsScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
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
                    btnSetExtrathumbsScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
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
                    btnSetFanartScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'Landscape
                If Master.eSettings.MovieLandscapeAnyEnabled Then
                    btnSetLandscapeScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If Master.eSettings.MoviePosterAnyEnabled Then
                    btnSetPosterScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
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
                DiscStub = MediaStub.LoadDiscStub(tmpDBElement.FileItem.FirstStackedPath)
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
                btnSetThemeScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                If tmpDBElement.Theme.LocalFilePathSpecified OrElse tmpDBElement.Theme.URLAudioStreamSpecified Then
                    Theme_Load(tmpDBElement.Theme)
                End If
                bNeedTab_Other = True
            Else
                gbTheme.Visible = False
            End If

            'Trailer
            If Master.eSettings.MovieTrailerAnyEnabled Then
                btnSetTrailerScrape.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
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
                .Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
                .Movie.Language = .Language
            Else
                .Language = "en-US"
                .Movie.Language = .Language
            End If
            'ListTitle
            .ListTitle = StringUtils.ListTitle_Movie(txtTitle.Text, txtYear.Text)
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
            'Ratings
            'TODO
            'ReleaseDate
            .ReleaseDate = dtpReleaseDate.Value.ToString("yyyy-MM-dd")
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
            'Year
            .Year = If(Integer.TryParse(txtYear.Text.Trim, 0), CInt(txtYear.Text.Trim), 0)
        End With

        'DiscStub
        If tmpDBElement.FileItem.bIsDiscStub Then
            Dim StubFile As String = tmpDBElement.FileItem.FirstStackedPath
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

        Dim nFrame = FFmpeg.FFmpeg.ExtractImageFromVideo(tmpDBElement.FileItem.FirstStackedPath, tbFrame.Value, True)

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
        Dim nFrame = FFmpeg.FFmpeg.ExtractImageFromVideo(tmpDBElement.FileItem.FirstStackedPath, 0, True)
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

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbBanner.DoubleClick,
        pbClearArt.DoubleClick,
        pbClearLogo.DoubleClick,
        pbDiscArt.DoubleClick,
        pbFanart.DoubleClick,
        pbFrame.DoubleClick,
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
        btnSetClearArtDL.Click,
        btnSetClearLogoDL.Click,
        btnSetDiscArtDL.Click,
        btnSetExtrafanartsDL.Click,
        btnSetExtrathumbsDL.Click,
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
                            Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                        Case Enums.ModifierType.MainClearArt
                            tmpDBElement.ImagesContainer.ClearArt = tImage
                            Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                        Case Enums.ModifierType.MainClearLogo
                            tmpDBElement.ImagesContainer.ClearLogo = tImage
                            Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                        Case Enums.ModifierType.MainDiscArt
                            tmpDBElement.ImagesContainer.DiscArt = tImage
                            Image_LoadPictureBox(Enums.ModifierType.MainDiscArt)
                        Case Enums.ModifierType.MainExtrafanarts
                            tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                            Image_Extrafanarts_Refresh()
                        Case Enums.ModifierType.MainExtrathumbs
                            tmpDBElement.ImagesContainer.Extrathumbs.Add(tImage)
                            Image_Extrathumbs_Refresh
                        Case Enums.ModifierType.MainFanart
                            tmpDBElement.ImagesContainer.Fanart = tImage
                            Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                        Case Enums.ModifierType.MainLandscape
                            tmpDBElement.ImagesContainer.Landscape = tImage
                            Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                        Case Enums.ModifierType.MainPoster
                            tmpDBElement.ImagesContainer.Poster = tImage
                            Image_LoadPictureBox(Enums.ModifierType.MainPoster)
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
        pbLandscape.DragDrop,
        pbPoster.DragDrop,
        pnlExtrafanarts.DragDrop,
        pnlExtrathumbs.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Select Case True
                Case sender Is pbBanner
                    tmpDBElement.ImagesContainer.Banner = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                Case sender Is pbClearArt
                    tmpDBElement.ImagesContainer.ClearArt = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                Case sender Is pbClearLogo
                    tmpDBElement.ImagesContainer.ClearLogo = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                Case sender Is pbDiscArt
                    tmpDBElement.ImagesContainer.DiscArt = tImage
                    Image_LoadPictureBox(Enums.ModifierType.MainDiscArt)
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
                Case sender Is pnlExtrathumbs
                    tmpDBElement.ImagesContainer.Extrathumbs.Add(tImage)
                    Image_Extrathumbs_Refresh()
            End Select
        End If
    End Sub

    Private Sub Image_DragEnter(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragEnter,
        pbClearArt.DragEnter,
        pbClearLogo.DragEnter,
        pbDiscArt.DragEnter,
        pbFanart.DragEnter,
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

    Private Sub Image_Extraimages_Add(ByVal sDescription As String, ByVal iIndex As Integer, tImage As MediaContainers.Image, modType As Enums.ModifierType)
        Dim iNextTop As Integer
        Dim tLabel() As Label = Nothing
        Dim tMainPanel As New Panel
        Dim tPanel() As Panel = Nothing
        Dim tPictureBox() As PictureBox
        Select Case modType
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
            If tImage.ImageOriginal.Image Is Nothing Then
                tImage.LoadAndCache(Enums.ContentType.Movie, True, True)
            End If
            ReDim Preserve tPanel(iIndex)
            ReDim Preserve tPictureBox(iIndex)
            ReDim Preserve tLabel(iIndex)

            tPanel(iIndex) = New Panel()
            tPictureBox(iIndex) = New PictureBox()
            tLabel(iIndex) = New Label()

            tLabel(iIndex).AutoSize = False
            tLabel(iIndex).BackColor = Color.White
            tLabel(iIndex).Location = iImageList_Location_Resolution
            tLabel(iIndex).Name = iIndex.ToString
            tLabel(iIndex).Size = iImageList_Size_Resolution
            tLabel(iIndex).Tag = tImage
            tLabel(iIndex).Text = String.Format("{0}x{1}", tImage.ImageOriginal.Image.Width, tImage.ImageOriginal.Image.Height)
            tLabel(iIndex).TextAlign = ContentAlignment.MiddleCenter

            tPictureBox(iIndex).Image = tImage.ImageOriginal.Image
            tPictureBox(iIndex).Location = iImageList_Location_Image
            tPictureBox(iIndex).Name = iIndex.ToString
            tPictureBox(iIndex).Size = iImageList_Size_Image
            tPictureBox(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            tPictureBox(iIndex).Tag = tImage

            tPanel(iIndex).BackColor = Color.White
            tPanel(iIndex).BorderStyle = BorderStyle.FixedSingle
            tPanel(iIndex).Left = iImageList_DistanceLeft
            tPanel(iIndex).Name = iIndex.ToString
            tPanel(iIndex).Size = iImageList_Size_Panel
            tPanel(iIndex).Tag = tImage
            tPanel(iIndex).Top = iNextTop

            tMainPanel.Controls.Add(tPanel(iIndex))
            tPanel(iIndex).Controls.Add(tPictureBox(iIndex))
            tPanel(iIndex).Controls.Add(tLabel(iIndex))
            tPanel(iIndex).BringToFront()

            AddHandler tLabel(iIndex).Click, AddressOf Image_Extraimages_Click
            AddHandler tLabel(iIndex).DoubleClick, AddressOf Image_DoubleClick
            AddHandler tPictureBox(iIndex).Click, AddressOf Image_Extraimages_Click
            AddHandler tPictureBox(iIndex).DoubleClick, AddressOf Image_DoubleClick
            AddHandler tPictureBox(iIndex).MouseDown, AddressOf Image_Drag_MouseDown
            AddHandler tPictureBox(iIndex).MouseMove, AddressOf Image_Drag_MouseMove
            AddHandler tPictureBox(iIndex).MouseUp, AddressOf Image_Drag_MouseUp
            AddHandler tPanel(iIndex).Click, AddressOf Image_Extraimages_Click
            AddHandler tPanel(iIndex).DoubleClick, AddressOf Image_DoubleClick
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Select Case modType
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
        Dim tModType As Enums.ModifierType
        Select Case True
            Case TypeOf (sender) Is Label
                iIndex = Convert.ToInt32(DirectCast(sender, Label).Name)
                tImage = DirectCast(DirectCast(sender, Label).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, Label).Parent) Then
                    tModType = Enums.ModifierType.MainExtrafanarts
                ElseIf pnlExtrathumbsList.Controls.Contains(DirectCast(sender, Label).Parent) Then
                    tModType = Enums.ModifierType.MainExtrathumbs
                End If
            Case TypeOf (sender) Is Panel
                iIndex = Convert.ToInt32(DirectCast(sender, Panel).Name)
                tImage = DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, Panel).Parent) Then
                    tModType = Enums.ModifierType.MainExtrafanarts
                ElseIf pnlExtrathumbsList.Controls.Contains(DirectCast(sender, Panel).Parent) Then
                    tModType = Enums.ModifierType.MainExtrathumbs
                End If
            Case TypeOf (sender) Is PictureBox
                iIndex = Convert.ToInt32(DirectCast(sender, PictureBox).Name)
                tImage = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
                If pnlExtrafanartsList.Controls.Contains(DirectCast(sender, PictureBox).Parent) Then
                    tModType = Enums.ModifierType.MainExtrafanarts
                ElseIf pnlExtrathumbsList.Controls.Contains(DirectCast(sender, PictureBox).Parent) Then
                    tModType = Enums.ModifierType.MainExtrathumbs
                End If
        End Select
        If tImage IsNot Nothing Then
            Select Case tModType
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
        btnExtrafanartsRemove.Enabled = False
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
        btnExtrathumbsRemove.Enabled = False
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
        btnExtrafanartsRemove.Enabled = True
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
        btnExtrathumbsRemove.Enabled = True
    End Sub

    Private Sub Image_Extrafanarts_Refresh() Handles btnExtrafanartsRefresh.Click
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

    Private Sub Image_Extrathumbs_Refresh() Handles btnExtrathumbsRefresh.Click
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

    Private Sub Image_LoadPictureBox(ByVal imageType As Enums.ModifierType)
        Dim cImage As MediaContainers.Image
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Select Case imageType
            Case Enums.ModifierType.MainBanner
                cImage = tmpDBElement.ImagesContainer.Banner
                lblSize = lblBannerSize
                pbImage = pbBanner
            Case Enums.ModifierType.MainClearArt
                cImage = tmpDBElement.ImagesContainer.ClearArt
                lblSize = lblClearArtSize
                pbImage = pbClearArt
            Case Enums.ModifierType.MainClearLogo
                cImage = tmpDBElement.ImagesContainer.ClearLogo
                lblSize = lblClearLogoSize
                pbImage = pbClearLogo
            Case Enums.ModifierType.MainDiscArt
                cImage = tmpDBElement.ImagesContainer.DiscArt
                lblSize = lblDiscArtSize
                pbImage = pbDiscArt
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
        btnSetClearArtLocal.Click,
        btnSetClearLogoLocal.Click,
        btnSetDiscArtLocal.Click,
        btnSetExtrafanartsLocal.Click,
        btnSetFanartLocal.Click,
        btnSetLandscapeLocal.Click,
        btnSetPosterLocal.Click,
        btnSetExtrathumbsLocal.Click
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
                    Case Enums.ModifierType.MainClearArt
                        tmpDBElement.ImagesContainer.ClearArt = tImage
                    Case Enums.ModifierType.MainClearLogo
                        tmpDBElement.ImagesContainer.ClearLogo = tImage
                    Case Enums.ModifierType.MainDiscArt
                        tmpDBElement.ImagesContainer.DiscArt = tImage
                    Case Enums.ModifierType.MainExtrafanarts
                        tmpDBElement.ImagesContainer.Extrafanarts.Add(tImage)
                    Case Enums.ModifierType.MainExtrathumbs
                        tmpDBElement.ImagesContainer.Extrathumbs.Add(tImage)
                    Case Enums.ModifierType.MainFanart
                        tmpDBElement.ImagesContainer.Fanart = tImage
                    Case Enums.ModifierType.MainLandscape
                        tmpDBElement.ImagesContainer.Landscape = tImage
                    Case Enums.ModifierType.MainPoster
                        tmpDBElement.ImagesContainer.Poster = tImage
                End Select
                Select Case modType
                    Case Enums.ModifierType.MainExtrafanarts
                        Image_Extrafanarts_Refresh()
                    Case Enums.ModifierType.MainExtrathumbs
                        Image_Extrathumbs_Refresh()
                    Case Else
                        Image_LoadPictureBox(modType)
                End Select
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
            btnRemoveBanner.Click,
            btnRemoveClearArt.Click,
            btnRemoveClearLogo.Click,
            btnRemoveDiscArt.Click,
            btnExtrafanartsRemove.Click,
            btnRemoveFanart.Click,
            btnRemoveLandscape.Click,
            btnRemovePoster.Click,
            btnExtrathumbsRemove.Click
        Dim lblSize As Label = Nothing
        Dim pbImage As PictureBox = Nothing
        Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
        Select Case modType
            Case Enums.ModifierType.MainBanner
                lblSize = lblBannerSize
                pbImage = pbBanner
                tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
            Case Enums.ModifierType.MainClearArt
                lblSize = lblClearArtSize
                pbImage = pbClearArt
                tmpDBElement.ImagesContainer.ClearArt = New MediaContainers.Image
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblClearLogoSize
                pbImage = pbClearLogo
                tmpDBElement.ImagesContainer.ClearLogo = New MediaContainers.Image
            Case Enums.ModifierType.MainDiscArt
                lblSize = lblDiscArtSize
                pbImage = pbDiscArt
                tmpDBElement.ImagesContainer.DiscArt = New MediaContainers.Image
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
        btnSetClearArtScrape.Click,
        btnSetClearLogoScrape.Click,
        btnSetDiscArtScrape.Click,
        btnSetExtrafanartsScrape.Click,
        btnSetFanartScrape.Click,
        btnSetLandscapeScrape.Click,
        btnSetPosterScrape.Click,
        btnSetExtrathumbsScrape.Click
        Cursor = Cursors.WaitCursor
        Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, modType, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            Dim iImageCount = 0
            Dim strNoImagesFound As String = String.Empty
            Select Case modType
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
                        Case Enums.ModifierType.MainDiscArt
                            tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
                            If tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.MainExtrafanarts
                            tmpDBElement.ImagesContainer.Extrafanarts = dlgImgS.Result.ImagesContainer.Extrafanarts
                            Image_Extrafanarts_Refresh()
                        Case Enums.ModifierType.MainExtrathumbs
                            tmpDBElement.ImagesContainer.Extrathumbs = dlgImgS.Result.ImagesContainer.Extrathumbs
                            Image_Extrathumbs_Refresh()
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
        If Not String.IsNullOrEmpty(Master.eSettings.MovieScraperMPAANotRated) Then lbMPAA.Items.Add(Master.eSettings.MovieScraperMPAANotRated)
        lbMPAA.Items.AddRange(APIXML.GetRatingList_Movie)
    End Sub

    Private Sub MPAA_Select()
        If tmpDBElement.Movie.MPAASpecified Then
            If Master.eSettings.MovieScraperCertOnlyValue Then
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
        txtUserRating.KeyPress,
        txtYear.KeyPress
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
        If Not ModulesManager.Instance.ScrapeTheme_Movie(tmpDBElement, Enums.ModifierType.MainTheme, tList) Then
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
        Dim strValidExtesions As String() = Master.eSettings.FileSystemValidExts.ToArray
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