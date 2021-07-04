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

Public Class dlgEdit_TVEpisode

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private iPreviousFrameValue As Integer
    Private lvwActorsSorter As ListViewColumnSorter
    Private lvwGuestStarsSorter As ListViewColumnSorter

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

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(tmpDBElement, True) Then
            pbFanart.AllowDrop = True
            pbPoster.AllowDrop = True

            Setup()
            lvwActorsSorter = New ListViewColumnSorter()
            lvActors.ListViewItemSorter = lvwActorsSorter
            lvwGuestStarsSorter = New ListViewColumnSorter()
            lvGuestStars.ListViewItemSorter = lvwGuestStarsSorter

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            Dim dFileInfoEdit As New dlgFileInfo(tmpDBElement.TVEpisode.FileInfo) With {
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

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
        ClearSelection()
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
            Dim mTitle As String = String.Empty
            mTitle = tmpDBElement.TVEpisode.Title
            Text = String.Concat(.GetString(656, "Edit Episode"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
            btnCancel.Text = .Cancel
            btnChange.Text = .GetString(772, "Change Episode")
            btnFrameLoadVideo.Text = .GetString(307, "Load Video")
            btnFrameSaveAsFanart.Text = .GetString(1049, "Save as Fanart")
            btnFrameSaveAsPoster.Text = .GetString(309, "Save as Poster")
            btnOK.Text = .OK
            btnRescrape.Text = .GetString(716, "Re-Scrape")
            chkLocked.Text = .GetString(43, "Locked")
            chkMarked.Text = .GetString(48, "Marked")
            chkWatched.Text = .GetString(981, "Watched")
            colActorsName.Text = .GetString(232, "Name")
            colActorsRole.Text = .GetString(233, "Role")
            colActorsThumb.Text = .GetString(234, "Thumb")
            colGuestStarsID.Text = .GetString(232, "Name")
            colGuestStarsRole.Text = .GetString(233, "Role")
            colGuestStarsThumb.Text = .GetString(234, "Thumb")
            colRatingsDefault.HeaderText = .GetString(1229, "Default")
            colRatingsMax.HeaderText = .GetString(1230, "Max")
            colRatingsSource.HeaderText = .GetString(232, "Name")
            colRatingsValue.HeaderText = .GetString(345, "Value")
            colRatingsVotes.HeaderText = .GetString(244, "Votes")
            lblActors.Text = String.Concat(.GetString(231, "Actors"), ":")
            lblAired.Text = String.Concat(.GetString(728, "Aired"), ":")
            gbSubtitles.Text = .GetString(152, "Subtitles")
            lblCredits.Text = String.Concat(.GetString(228, "Credits"), ":")
            lblDateAdded.Text = String.Concat(.GetString(601, "Date Added"), ":")
            lblDirectors.Text = String.Concat(.GetString(940, "Directors"), ":")
            lblDisplayEpisode.Text = String.Concat("Display Episode #", ":")
            lblDisplaySeason.Text = String.Concat("Display Season #", ":")
            lblEpisode.Text = String.Concat(.GetString(660, "Episode #"), ":")
            lblFanart.Text = .GetString(149, "Fanart")
            lblGuestStars.Text = String.Concat(.GetString(508, "Guest Stars"), ":")
            lblPlot.Text = String.Concat(.GetString(65, "Plot"), ":")
            lblPoster.Text = .GetString(148, "Poster")
            lblRatings.Text = String.Concat(.GetString(245, "Ratings"), ":")
            lblRuntime.Text = String.Concat(.GetString(238, "Runtime"), ":")
            lblSeason.Text = String.Concat(.GetString(659, "Season #"), ":")
            lblSubtitlesPreview.Text = String.Concat(.GetString(180, "Preview"), ":")
            lblTitle.Text = String.Concat(.GetString(21, "Title"), ":")
            lblTopDetails.Text = .GetString(656, "Edit the details for the selected episode.")
            lblTopTitle.Text = .GetString(657, "Edit Episode")
            lblUniqueIds.Text = String.Concat(.GetString(667, "Unique IDs"), ":")
            lblUserNote.Text = String.Concat(.GetString(666, "Note"), ":")
            lblUserRating.Text = String.Concat(.GetString(1467, "User Rating"), ":")
            lblVideoSource.Text = String.Concat(.GetString(824, "Video Source"), ":")
            tpCastCrew.Text = String.Concat(.GetString(63, "Cast"), " & ", .GetString(909, "Crew"))
            tpDetails.Text = .GetString(26, "Details")
            tpFrameExtraction.Text = .GetString(256, "Frame Extraction")
            tpImages.Text = .GetString(497, "Images")
            tpMetaData.Text = .GetString(866, "Metadata")
            tpOther.Text = .GetString(391, "Other")
            tsslFilename.Text = tmpDBElement.Filename
        End With
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

    Private Sub Actors_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lvActors.ColumnClick, lvGuestStars.ColumnClick
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

    Private Sub ClearSelection() Handles tcEdit.SelectedIndexChanged
        dgvCredits.ClearSelection()
        dgvDirectors.ClearSelection()
        dgvRatings.ClearSelection()
        dgvUniqueIds.ClearSelection()
        lvSubtitles.SelectedItems.Clear()
    End Sub

    Private Function ConvertControlToImageType(ByVal sender As Object) As Enums.ModifierType
        Select Case True
            Case sender Is btnRemoveFanart, sender Is btnDLFanart, sender Is btnLocalFanart, sender Is btnScrapeFanart, sender Is btnClipboardFanart
                Return Enums.ModifierType.EpisodeFanart
            Case sender Is btnRemovePoster, sender Is btnDLPoster, sender Is btnLocalPoster, sender Is btnScrapePoster, sender Is btnClipboardPoster
                Return Enums.ModifierType.EpisodePoster
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Data_Fill(Optional ByVal DoAll As Boolean = True)
        'Database related part
        With tmpDBElement
            chkLocked.Checked = .IsLock
            chkMarked.Checked = .IsMark
        End With

        'Information part
        With tmpDBElement.TVEpisode
            'Actors
            Dim lvActorsItem As ListViewItem
            lvActors.Items.Clear()
            For Each tActor As MediaContainers.Person In .Actors
                lvActorsItem = lvActors.Items.Add(tActor.ID.ToString)
                lvActorsItem.Tag = tActor
                lvActorsItem.SubItems.Add(tActor.Name)
                lvActorsItem.SubItems.Add(tActor.Role)
                lvActorsItem.SubItems.Add(tActor.URLOriginal)
            Next
            'Aired
            If .AiredSpecified Then
                dtpAired.Text = .Aired
                dtpAired.Checked = True
            End If
            'Credits
            For Each v In .Credits
                dgvCredits.Rows.Add(New Object() {v})
            Next
            dgvCredits.ClearSelection()
            'DateAdded
            Dim nDateAdded As Date
            If Date.TryParseExact(.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, nDateAdded) Then
                dtpDateAdded_Date.Value = nDateAdded
                dtpDateAdded_Time.Value = nDateAdded
            Else
                dtpDateAdded_Date.Value = Date.Now
                dtpDateAdded_Time.Value = Date.Now
            End If
            'Directors
            For Each v In .Directors
                dgvDirectors.Rows.Add(New Object() {v})
            Next
            dgvDirectors.ClearSelection()
            'DisplayEpisode
            txtDisplayEpisode.Text = .DisplayEpisode.ToString
            'DisplaySeason
            txtDisplaySeason.Text = .DisplaySeason.ToString
            'Episode
            txtEpisode.Text = .Episode.ToString
            'GuestStars
            Dim lvGuestStarsItem As ListViewItem
            lvGuestStars.Items.Clear()
            For Each tGuestStars As MediaContainers.Person In .GuestStars
                lvGuestStarsItem = lvGuestStars.Items.Add(tGuestStars.ID.ToString)
                lvGuestStarsItem.Tag = tGuestStars
                lvGuestStarsItem.SubItems.Add(tGuestStars.Name)
                lvGuestStarsItem.SubItems.Add(tGuestStars.Role)
                lvGuestStarsItem.SubItems.Add(tGuestStars.URLOriginal)
            Next
            'Plot
            txtPlot.Text = .Plot
            'Ratings
            Ratings_Fill()
            'Runtime
            txtRuntime.Text = .Runtime
            'Season
            txtSeason.Text = .Season.ToString
            'Title
            txtTitle.Text = .Title
            'Unique IDs
            UniqueIds_Fill()
            'UserNote
            txtUserNote.Text = .UserNote
            'UserRating
            cbUserRating.Text = .UserRating.ToString
            'Videosource
            Videosources_Fill()
            'Watched/Lastplayed
            chkWatched.Checked = .LastPlayedSpecified
            If .LastPlayedSpecified Then
                Dim nLastPlayed As Date
                If Date.TryParseExact(.LastPlayed, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, nLastPlayed) Then
                    dtpLastPlayed_Date.Value = nLastPlayed
                    dtpLastPlayed_Time.Value = nLastPlayed
                Else
                    dtpLastPlayed_Date.Value = Date.Now
                    dtpLastPlayed_Time.Value = Date.Now
                End If
            Else
                'just pre-fill the fields with current date/time
                dtpLastPlayed_Date.Value = Date.Now
                dtpLastPlayed_Time.Value = Date.Now
            End If
        End With

        If DoAll Then
            'Images and TabPages/Panels controll
            Dim bNeedTab_FrameExtraction As Boolean
            Dim bNeedTab_Images As Boolean
            Dim bNeedTab_Other As Boolean

            With tmpDBElement.ImagesContainer
                'Load all images to MemoryStream and Bitmap
                tmpDBElement.LoadAllImages(True, True)

                'Fanart
                If Master.eSettings.TVEpisodeFanartAnyEnabled Then
                    btnScrapeFanart.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.EpisodeFanart)
                    End If
                    bNeedTab_FrameExtraction = True
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                    btnFrameSaveAsFanart.Visible = False
                End If

                'Poster
                If Master.eSettings.TVEpisodePosterAnyEnabled Then
                    btnScrapePoster.Enabled = ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    If .Poster.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.EpisodePoster)
                    End If
                    bNeedTab_Images = True
                    bNeedTab_FrameExtraction = True
                Else
                    pnlPoster.Visible = False
                    btnFrameSaveAsPoster.Visible = False
                End If
            End With

            'DiscStub
            If FileUtils.Common.IsDiscStub(tmpDBElement.Filename) Then
                Dim DiscStub As New MediaStub.DiscStub
                DiscStub = MediaStub.LoadDiscStub(tmpDBElement.Filename)
                txtMediaStubTitle.Text = DiscStub.Title
                txtMediaStubMessage.Text = DiscStub.Message
                bNeedTab_Other = True
            Else
                gbMediaStub.Visible = False
            End If

            'FrameExtracion
            If FileUtils.Common.IsArchive(tmpDBElement.Filename) OrElse
                FileUtils.Common.IsDiscImage(tmpDBElement.Filename) OrElse
                FileUtils.Common.IsDiscStub(tmpDBElement.Filename) Then
                tcEdit.TabPages.Remove(tpFrameExtraction)
                bNeedTab_FrameExtraction = False
            End If

            'Subtitles
            bNeedTab_Other = True
            Subtitles_Load()

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
            'States
            .IsLock = chkLocked.Checked
            .IsMark = chkMarked.Checked
            'Videosource
            .VideoSource = cbVideosource.Text.Trim
            .TVEpisode.VideoSource = .VideoSource
        End With

        'Information part
        With tmpDBElement.TVEpisode
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
            'Aired
            If dtpAired.Checked Then
                .Aired = dtpAired.Value.ToString("yyyy-MM-dd")
            Else
                .Aired = String.Empty
            End If
            'Credits
            .Credits = DataGridView_RowsToList(dgvCredits)
            'Directors
            .Directors = DataGridView_RowsToList(dgvDirectors)
            'DisplayEpisode
            .DisplayEpisode = If(Integer.TryParse(txtDisplayEpisode.Text.Trim, 0), CInt(txtDisplayEpisode.Text.Trim), 0)
            'DisplaySeason
            .DisplaySeason = If(Integer.TryParse(txtDisplaySeason.Text.Trim, 0), CInt(txtDisplaySeason.Text.Trim), 0)
            'Episode
            .Episode = If(Integer.TryParse(txtEpisode.Text.Trim, 0), CInt(txtEpisode.Text.Trim), 0)
            'FileInfo
            Dim cIndex = pnlFileInfo.Controls.IndexOfKey("dlgFileInfo")
            If Not cIndex = -1 Then
                Dim nResult = DirectCast(pnlFileInfo.Controls.Item(cIndex), dlgFileInfo)
                tmpDBElement.TVEpisode.FileInfo = nResult.Result
            End If
            'GuestStars
            .GuestStars.Clear()
            If lvGuestStars.Items.Count > 0 Then
                Dim iOrder As Integer = 0
                For Each lviGuestStars As ListViewItem In lvGuestStars.Items
                    Dim addActor As MediaContainers.Person = DirectCast(lviGuestStars.Tag, MediaContainers.Person)
                    addActor.Order = iOrder
                    iOrder += 1
                    .GuestStars.Add(addActor)
                Next
            End If
            'Plot
            .Plot = txtPlot.Text.Trim
            'Ratings
            .Ratings.Items = Ratings_Get()
            'Runtime
            .Runtime = txtRuntime.Text.Trim
            'Season
            .Season = If(Integer.TryParse(txtSeason.Text.Trim, 0), CInt(txtSeason.Text.Trim), 0)
            'Title
            .Title = txtTitle.Text.Trim
            'UserNote
            .UserNote = txtUserNote.Text.Trim
            'UserRating
            .UserRating = CInt(cbUserRating.SelectedItem)
            'Watched/Lastplayed
            'if watched-checkbox is checked -> save Playcount=1 in nfo
            If chkWatched.Checked Then
                'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
                If Not .PlaycountSpecified Then .Playcount = 1
                Dim nLastPlayed As New Date(dtpLastPlayed_Date.Value.Year,
                                            dtpLastPlayed_Date.Value.Month,
                                            dtpLastPlayed_Date.Value.Day,
                                            dtpLastPlayed_Time.Value.Hour,
                                            dtpLastPlayed_Time.Value.Minute,
                                            dtpLastPlayed_Time.Value.Second)
                .LastPlayed = nLastPlayed.ToString("yyyy-MM-dd HH:mm:ss")
            Else
                'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
                If .PlaycountSpecified Then
                    .Playcount = 0
                    .LastPlayed = String.Empty
                End If
            End If
        End With

        'DiscStub
        If FileUtils.Common.IsDiscStub(tmpDBElement.Filename) Then
            Dim StubFile As String = tmpDBElement.Filename
            Dim Title As String = txtMediaStubTitle.Text
            Dim Message As String = txtMediaStubMessage.Text
            MediaStub.SaveDiscStub(StubFile, Title, Message)
        End If

        'Subtitles
        Dim removeSubtitles As New List(Of MediaContainers.Subtitle)
        For Each Subtitle In tmpDBElement.Subtitles
            If Subtitle.ToRemove Then
                removeSubtitles.Add(Subtitle)
            End If
        Next
        For Each Subtitle In removeSubtitles
            If File.Exists(Subtitle.Path) Then
                File.Delete(Subtitle.Path)
            End If
            tmpDBElement.Subtitles.Remove(Subtitle)
        Next
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
        dgvCredits.Leave,
        dgvDirectors.Leave,
        dgvRatings.Leave,
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

    Private Sub FrameExtraction_DelayTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrDelay.Tick
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
        btnFrameSaveAsFanart.Enabled = False
        btnFrameSaveAsPoster.Enabled = False

        Dim nFrame = FFmpeg.FFmpeg.ExtractImageFromVideo(tmpDBElement.Filename, tbFrame.Value, True)

        If nFrame IsNot Nothing AndAlso nFrame.Image IsNot Nothing AndAlso nFrame.Image.ImageOriginal.Image IsNot Nothing Then
            pbFrame.Image = nFrame.Image.ImageOriginal.Image
            pbFrame.Tag = nFrame.Image
            tbFrame.Enabled = True
            btnFrameSaveAsFanart.Enabled = True
            btnFrameSaveAsPoster.Enabled = True
        Else
            lblTime.Text = String.Empty
            tbFrame.Maximum = 0
            tbFrame.Value = 0
            tbFrame.Enabled = False
            btnFrameSaveAsFanart.Enabled = False
            btnFrameSaveAsPoster.Enabled = False
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
        Dim nFrame = FFmpeg.FFmpeg.ExtractImageFromVideo(tmpDBElement.Filename, 0, True)
        If nFrame IsNot Nothing AndAlso nFrame.Duration > 0 AndAlso nFrame.Image IsNot Nothing AndAlso nFrame.Image.ImageOriginal IsNot Nothing Then
            tbFrame.Maximum = nFrame.Duration
            tbFrame.Value = 0
            tbFrame.Enabled = True
            pbFrame.Image = nFrame.Image.ImageOriginal.Image
            pbFrame.Tag = nFrame.Image
            btnFrameLoadVideo.Enabled = False
            btnFrameSaveAsFanart.Enabled = True
            btnFrameSaveAsPoster.Enabled = True
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
                Image_LoadPictureBox(Enums.ModifierType.EpisodeFanart)
            End If
            btnFrameSaveAsFanart.Enabled = False
        End If
    End Sub

    Private Sub FrameExtraction_SaveAsPoster_Click(sender As Object, e As EventArgs) Handles btnFrameSaveAsPoster.Click
        If pbFrame.Image IsNot Nothing AndAlso pbFrame.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Poster = DirectCast(pbFrame.Tag, MediaContainers.Image)
            If tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing Then
                Image_LoadPictureBox(Enums.ModifierType.EpisodePoster)
            End If
            btnFrameSaveAsFanart.Enabled = False
        End If
    End Sub

    Private Sub FrameExtraction_Scroll(sender As Object, e As EventArgs) Handles tbFrame.Scroll
        Dim sec2Time As New TimeSpan(0, 0, tbFrame.Value)
        lblTime.Text = String.Format("{0}:{1:00}:{2:00}", sec2Time.Hours, sec2Time.Minutes, sec2Time.Seconds)
    End Sub

    Private Sub GuestStars_Add() Handles btnGuestStarsAdd.Click
        Using dAddEditGuestStar As New dlgAddEditActor
            If dAddEditGuestStar.ShowDialog() = DialogResult.OK Then
                Dim nGuestStar As MediaContainers.Person = dAddEditGuestStar.Result
                Dim lvItem As ListViewItem = lvGuestStars.Items.Add(nGuestStar.ID.ToString)
                lvItem.Tag = nGuestStar
                lvItem.SubItems.Add(nGuestStar.Name)
                lvItem.SubItems.Add(nGuestStar.Role)
                lvItem.SubItems.Add(nGuestStar.URLOriginal)
            End If
        End Using
    End Sub

    Private Sub GuestStars_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lvGuestStars.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.

        If (e.Column = lvwGuestStarsSorter.SortColumn) Then
            ' Reverse the current sort direction for this column.
            If (lvwGuestStarsSorter.Order = SortOrder.Ascending) Then
                lvwGuestStarsSorter.Order = SortOrder.Descending
            Else
                lvwGuestStarsSorter.Order = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            lvwGuestStarsSorter.SortColumn = e.Column
            lvwGuestStarsSorter.Order = SortOrder.Ascending
        End If

        ' Perform the sort with these new sort options.
        lvGuestStars.Sort()
    End Sub

    Private Sub GuestStars_Down_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuestStarsDown.Click
        If lvGuestStars.SelectedItems.Count > 0 AndAlso lvGuestStars.SelectedItems(0) IsNot Nothing AndAlso lvGuestStars.SelectedIndices(0) < (lvGuestStars.Items.Count - 1) Then
            Dim iIndex As Integer = lvGuestStars.SelectedIndices(0)
            lvGuestStars.Items.Insert(iIndex + 2, DirectCast(lvGuestStars.SelectedItems(0).Clone, ListViewItem))
            lvGuestStars.Items.RemoveAt(iIndex)
            lvGuestStars.Items(iIndex + 1).Selected = True
            lvGuestStars.Select()
        End If
    End Sub

    Private Sub GuestStars_Edit_Click() Handles btnGuestStarsEdit.Click, lvGuestStars.DoubleClick
        If lvGuestStars.SelectedItems.Count > 0 Then
            Dim lvwItem As ListViewItem = lvGuestStars.SelectedItems(0)
            Dim eGuestStar As MediaContainers.Person = DirectCast(lvwItem.Tag, MediaContainers.Person)
            Using dAddEditGuestStar As New dlgAddEditActor
                If dAddEditGuestStar.ShowDialog(eGuestStar) = DialogResult.OK Then
                    eGuestStar = dAddEditGuestStar.Result
                    lvwItem.Text = eGuestStar.ID.ToString
                    lvwItem.Tag = eGuestStar
                    lvwItem.SubItems(1).Text = eGuestStar.Name
                    lvwItem.SubItems(2).Text = eGuestStar.Role
                    lvwItem.SubItems(3).Text = eGuestStar.URLOriginal
                    lvwItem.Selected = True
                    lvwItem.EnsureVisible()
                End If
            End Using
        End If
    End Sub

    Private Sub GuestStars_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvGuestStars.KeyDown
        If e.KeyCode = Keys.Delete Then GuestStars_Remove_Click()
    End Sub

    Private Sub GuestStars_Remove_Click() Handles btnGuestStarsRemove.Click
        If lvGuestStars.Items.Count > 0 Then
            While lvGuestStars.SelectedItems.Count > 0
                lvGuestStars.Items.Remove(lvGuestStars.SelectedItems(0))
            End While
        End If
    End Sub

    Private Sub GuestStars_Up_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuestStarsUp.Click
        If lvGuestStars.SelectedItems.Count > 0 AndAlso lvGuestStars.SelectedItems(0) IsNot Nothing AndAlso lvGuestStars.SelectedIndices(0) > 0 Then
            Dim iIndex As Integer = lvGuestStars.SelectedIndices(0)
            lvGuestStars.Items.Insert(iIndex - 1, DirectCast(lvGuestStars.SelectedItems(0).Clone, ListViewItem))
            lvGuestStars.Items.RemoveAt(iIndex + 1)
            lvGuestStars.Items(iIndex - 1).Selected = True
            lvGuestStars.Select()
        End If
    End Sub

    Private Sub Image_Clipboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnClipboardFanart.Click,
        btnClipboardPoster.Click

        Dim lstImages = FileUtils.ClipboardHandler.GetImagesFromClipboard
        If lstImages.Count > 0 Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            tmpDBElement.ImagesContainer.SetImageByType(lstImages(0), eImageType)
            Image_LoadPictureBox(eImageType)
        End If
    End Sub

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbFanart.DoubleClick,
        pbFrame.DoubleClick,
        pbPoster.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnDLFanart.Click,
        btnDLPoster.Click
        Using dImgManual As New dlgImageManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                    tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                    Image_LoadPictureBox(eImageType)
                End If
            End If
        End Using
    End Sub

    Private Sub Image_DragDrop(sender As Object, e As DragEventArgs) Handles _
        pbFanart.DragDrop,
        pbPoster.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
            Image_LoadPictureBox(eImageType)
        End If
    End Sub

    Private Sub Image_DragEnter(sender As Object, e As DragEventArgs) Handles _
        pbFanart.DragEnter,
        pbPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Image_LoadPictureBox(ByVal ImageType As Enums.ModifierType)
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Select Case ImageType
            Case Enums.ModifierType.EpisodeFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
            Case Enums.ModifierType.EpisodePoster
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
        btnLocalFanart.Click,
        btnLocalPoster.Click
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With
        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                Image_LoadPictureBox(eImageType)
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnRemoveFanart.Click,
        btnRemovePoster.Click
        Dim lblSize As Label = Nothing
        Dim pbImage As PictureBox = Nothing
        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Select Case eImageType
            Case Enums.ModifierType.EpisodeFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.EpisodePoster
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
        btnScrapeFanart.Click,
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
                Case Enums.ModifierType.EpisodeFanart
                    iImageCount = nContainer.EpisodeFanarts.Count + nContainer.MainFanarts.Count
                    strNoImagesFound = Master.eLang.GetString(970, "No Fanarts found")
                Case Enums.ModifierType.EpisodePoster
                    iImageCount = nContainer.EpisodePosters.Count
                    strNoImagesFound = Master.eLang.GetString(972, "No Posters found")
            End Select
            If iImageCount > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, nContainer, nScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.SetImageByType(dlgImgS.Result.ImagesContainer.GetImageByType(eImageType), eImageType)
                    If tmpDBElement.ImagesContainer.GetImageByType(eImageType) IsNot Nothing AndAlso
                        tmpDBElement.ImagesContainer.GetImageByType(eImageType).ImageOriginal.LoadFromMemoryStream Then
                        Image_LoadPictureBox(eImageType)
                    Else
                        Image_Remove_Click(sender, e)
                    End If
                End If
            Else
                MessageBox.Show(strNoImagesFound, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub Ratings_Fill()
        dgvRatings.SuspendLayout()

        For Each tRating In tmpDBElement.TVEpisode.Ratings.Items.OrderBy(Function(f) Not f.IsDefault)
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

    Private Sub Subtitles_Delete()
        If lvSubtitles.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
            If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                tmpDBElement.Subtitles(Convert.ToInt16(i.Text)).ToRemove = True
            End If
            Subtitles_Load()
        End If
    End Sub

    Private Sub Subtitles_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvSubtitles.DoubleClick
        If lvSubtitles.SelectedItems.Count > 0 Then
            If lvSubtitles.SelectedItems.Item(0).Tag.ToString <> "Header" Then
                Subtitles_Edit()
            End If
        End If
    End Sub

    Private Sub Subtitles_Edit()
        If lvSubtitles.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
            Dim tmpFileInfo As New MediaContainers.Fileinfo
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

    Private Sub Subtitles_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvSubtitles.KeyDown
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
                    i.SubItems.Add(s.Type)
                    i.SubItems.Add(If(s.Forced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))

                    If s.ToRemove Then
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

    Private Sub Subtitles_Remove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveSubtitle.Click
        Subtitles_Delete()
    End Sub

    Private Sub Subtitles_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvSubtitles.SelectedIndexChanged
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
        lvSubtitles.SelectedItems.Clear()
    End Sub

    Private Sub TextBox_IntegerOnly(sender As Object, e As KeyPressEventArgs) Handles _
        txtDisplayEpisode.KeyPress,
        txtDisplaySeason.KeyPress
        e.Handled = StringUtils.IntegerOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_UIntegerOnly(sender As Object, e As KeyPressEventArgs) Handles _
        txtEpisode.KeyPress,
        txtSeason.KeyPress
        e.Handled = StringUtils.UIntegerOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_SelectAll(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

    Private Sub Title_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        btnOK.Enabled = Not String.IsNullOrEmpty(txtTitle.Text.Trim)
    End Sub

    Private Sub UniqueIds_Fill()
        dgvUniqueIds.SuspendLayout()

        For Each tId In tmpDBElement.TVEpisode.UniqueIDs.Items.OrderBy(Function(f) Not f.IsDefault)
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

    Private Sub Videosources_Fill()
        If tmpDBElement.TVEpisode.VideoSourceSpecified Then
            cbVideosource.Items.Add(tmpDBElement.TVEpisode.VideoSource)
            cbVideosource.SelectedItem = tmpDBElement.TVEpisode.VideoSource
        End If
        cbVideosource.Items.AddRange(Master.DB.GetAll_VideoSources_TVEpisode.Where(Function(f) Not cbVideosource.Items.Contains(f)).ToArray)
    End Sub

    Private Sub Watched_CheckedChanged(sender As Object, e As EventArgs) Handles chkWatched.CheckedChanged
        dtpLastPlayed_Date.Enabled = chkWatched.Checked
        dtpLastPlayed_Time.Enabled = chkWatched.Checked
    End Sub

#End Region 'Methods

End Class