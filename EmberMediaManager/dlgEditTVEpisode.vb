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
Imports NLog

Public Class dlgEditTVEpisode

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private lvwActorSorter As ListViewColumnSorter
    Private PreviousFrameValue As Integer
    Private tmpRating As String

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As Database.DBElement
        Get
            Return tmpDBElement
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBTVEpisode As Database.DBElement) As DialogResult
        tmpDBElement = DBTVEpisode
        Return ShowDialog()
    End Function

    Private Sub ActorEdit()
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

    Private Sub ActorRemove()
        If lvActors.Items.Count > 0 Then
            While lvActors.SelectedItems.Count > 0
                lvActors.Items.Remove(lvActors.SelectedItems(0))
            End While
        End If
    End Sub

    Private Sub btnActorAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorAdd.Click
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

    Private Sub btnActorDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorDown.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) < (lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex + 2, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex)
            lvActors.Items(iIndex + 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub btnActorEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorEdit.Click
        ActorEdit()
    End Sub

    Private Sub btnActorRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorRemove.Click
        ActorRemove()
    End Sub

    Private Sub btnActorUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorUp.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) > 0 Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex - 1, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex + 1)
            lvActors.Items(iIndex - 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub btnManual_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnManual.Click
        If dlgManualEdit.ShowDialog(tmpDBElement.NfoPath) = DialogResult.OK Then
            tmpDBElement.TVEpisode = NFO.LoadFromNFO_TVEpisode(tmpDBElement.NfoPath, tmpDBElement.TVEpisode.Season, tmpDBElement.TVEpisode.Episode)
            FillInfo()
        End If
    End Sub

    Private Sub btnRemoveFanart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveFanart.Click
        pbFanart.Image = Nothing
        pbFanart.Tag = Nothing
        lblFanartSize.Text = String.Empty
        lblFanartSize.Visible = False
        tmpDBElement.ImagesContainer.Fanart = New MediaContainers.Image
    End Sub

    Private Sub btnRemovePoster_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemovePoster.Click
        pbPoster.Image = Nothing
        pbPoster.Tag = Nothing
        lblPosterSize.Text = String.Empty
        lblPosterSize.Visible = False
        tmpDBElement.ImagesContainer.Poster = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveSubtitle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveSubtitle.Click
        DeleteSubtitle()
    End Sub

    Private Sub BuildStars(ByVal sinRating As Single)

        Try
            'f'in MS and them leaving control arrays out of VB.NET
            With Me
                .pbStar1.Image = Nothing
                .pbStar2.Image = Nothing
                .pbStar3.Image = Nothing
                .pbStar4.Image = Nothing
                .pbStar5.Image = Nothing
                .pbStar6.Image = Nothing
                .pbStar7.Image = Nothing
                .pbStar8.Image = Nothing
                .pbStar9.Image = Nothing
                .pbStar10.Image = Nothing

                If sinRating >= 0.5 Then ' if rating is less than .5 out of ten, consider it a 0
                    Select Case (sinRating)
                        Case Is <= 0.5
                            .pbStar1.Image = My.Resources.starhalf
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starhalf
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starhalf
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starhalf
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starhalf
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starhalf
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starhalf
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starhalf
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starhalf
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starhalf
                        Case Else
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.star
                    End Select
                End If
            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        CleanUp()
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub CleanUp()
        Try
            If File.Exists(Path.Combine(Master.TempPath, "poster.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "poster.jpg"))
            End If

            If File.Exists(Path.Combine(Master.TempPath, "fanart.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "fanart.jpg"))
            End If

            If File.Exists(Path.Combine(Master.TempPath, "frame.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "frame.jpg"))
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub dlgEditEpisode_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(tmpDBElement, True) Then
            If Not Master.eSettings.TVEpisodeFanartAnyEnabled Then tcEdit.TabPages.Remove(tpFanart)
            If Not Master.eSettings.TVEpisodePosterAnyEnabled Then
                tcEdit.TabPages.Remove(tpPoster)
                tcEdit.TabPages.Remove(tpFrameExtraction)
            End If

            pbFanart.AllowDrop = True
            pbPoster.AllowDrop = True

            SetUp()

            lvwActorSorter = New ListViewColumnSorter()
            lvActors.ListViewItemSorter = lvwActorSorter

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            Dim dFileInfoEdit As New dlgFileInfo(tmpDBElement, True)
            dFileInfoEdit.TopLevel = False
            dFileInfoEdit.FormBorderStyle = FormBorderStyle.None
            dFileInfoEdit.BackColor = Color.White
            dFileInfoEdit.btnClose.Visible = False
            pnlFileInfo.Controls.Add(dFileInfoEdit)
            Dim oldwidth As Integer = dFileInfoEdit.Width
            dFileInfoEdit.Width = pnlFileInfo.Width
            dFileInfoEdit.Height = pnlFileInfo.Height
            dFileInfoEdit.Show()

            Dim params As New List(Of Object)(New Object() {New Panel})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.FrameExtrator_TVEpisode, params, Nothing, True, tmpDBElement)
            pnlFrameExtrator.Controls.Add(DirectCast(params(0), Panel))
            If String.IsNullOrEmpty(pnlFrameExtrator.Controls.Item(0).Name) Then
                tcEdit.TabPages.Remove(tpFrameExtraction)
            End If

            FillInfo()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub FillInfo()
        txtAired.Text = tmpDBElement.TVEpisode.Aired
        txtCredits.Text = String.Join(" / ", tmpDBElement.TVEpisode.Credits.ToArray)
        txtDirectors.Text = String.Join(" / ", tmpDBElement.TVEpisode.Directors.ToArray)
        txtEpisode.Text = tmpDBElement.TVEpisode.Episode.ToString
        txtPlot.Text = tmpDBElement.TVEpisode.Plot
        txtRuntime.Text = tmpDBElement.TVEpisode.Runtime
        txtSeason.Text = tmpDBElement.TVEpisode.Season.ToString
        txtTitle.Text = tmpDBElement.TVEpisode.Title
        txtUserRating.Text = tmpDBElement.TVEpisode.UserRating.ToString
        txtVotes.Text = tmpDBElement.TVEpisode.Votes

        If Not String.IsNullOrEmpty(tmpDBElement.VideoSource) Then
            txtVideoSource.Text = tmpDBElement.VideoSource
        ElseIf Not String.IsNullOrEmpty(tmpDBElement.TVEpisode.VideoSource) Then
            txtVideoSource.Text = tmpDBElement.TVEpisode.VideoSource
        End If

        'Actors
        Dim lvItem As ListViewItem
        lvActors.Items.Clear()
        For Each tActor As MediaContainers.Person In tmpDBElement.TVEpisode.Actors
            lvItem = lvActors.Items.Add(tActor.ID.ToString)
            lvItem.Tag = tActor
            lvItem.SubItems.Add(tActor.Name)
            lvItem.SubItems.Add(tActor.Role)
            lvItem.SubItems.Add(tActor.URLOriginal)
        Next

        Dim tRating As Single = NumUtils.ConvertToSingle(tmpDBElement.TVEpisode.Rating)
        tmpRating = tRating.ToString
        pbStar1.Tag = tRating
        pbStar2.Tag = tRating
        pbStar3.Tag = tRating
        pbStar4.Tag = tRating
        pbStar5.Tag = tRating
        pbStar6.Tag = tRating
        pbStar7.Tag = tRating
        pbStar8.Tag = tRating
        pbStar9.Tag = tRating
        pbStar10.Tag = tRating
        If tRating > 0 Then BuildStars(tRating)

        If tmpDBElement.TVEpisode.PlaycountSpecified Then
            chkWatched.Checked = True
        Else
            chkWatched.Checked = False
        End If
        If Not String.IsNullOrEmpty(tmpDBElement.TVEpisode.LastPlayed) Then
            Dim timecode As Double = 0
            Double.TryParse(tmpDBElement.TVEpisode.LastPlayed, timecode)
            If timecode > 0 Then
                txtLastPlayed.Text = Functions.ConvertFromUnixTimestamp(timecode).ToString("yyyy-MM-dd HH:mm:ss")
            Else
                txtLastPlayed.Text = tmpDBElement.TVEpisode.LastPlayed
            End If
        End If

        'Images and TabPages
        With tmpDBElement.ImagesContainer

            'Load all images to MemoryStream and Bitmap
            tmpDBElement.LoadAllImages(True, True)

            'Fanart
            If Master.eSettings.TVEpisodeFanartAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart) AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart) Then
                    btnSetFanartScrape.Enabled = False
                End If
                If .Fanart.ImageOriginal.Image IsNot Nothing Then
                    pbFanart.Image = .Fanart.ImageOriginal.Image
                    pbFanart.Tag = .Fanart

                    lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                    lblFanartSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpFanart)
            End If

            'Poster
            If Master.eSettings.TVEpisodePosterAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster) Then
                    btnSetPosterScrape.Enabled = False
                End If
                If .Poster.ImageOriginal.Image IsNot Nothing Then
                    pbPoster.Image = .Poster.ImageOriginal.Image
                    pbPoster.Tag = .Poster

                    lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                    lblPosterSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpPoster)
            End If
        End With

        LoadSubtitles()
    End Sub

    Private Sub lvActors_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lvActors.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (lvwActorSorter.Order = SortOrder.Ascending) Then
                    lvwActorSorter.Order = SortOrder.Descending
                Else
                    lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                lvwActorSorter.SortColumn = e.Column
                lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            lvActors.Sort()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvActors.DoubleClick
        ActorEdit()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        SetInfo()
        CleanUp()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub pbEpisodeFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbFanart.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Fanart = tImage
            pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart
            lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
            lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbEpisodeFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbEpisodePoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbPoster.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Poster = tImage
            pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
            pbPoster.Tag = tmpDBElement.ImagesContainer.Poster
            lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
            lblPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbEpisodePoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbStar1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar1.Click
        tmpRating = pbStar1.Tag.ToString
    End Sub

    Private Sub pbStar1_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar1.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar1.MouseMove
        Try
            If e.X < 12 Then
                pbStar1.Tag = 0.5
                BuildStars(0.5)
            Else
                pbStar1.Tag = 1
                BuildStars(1)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar2.Click
        tmpRating = pbStar2.Tag.ToString
    End Sub

    Private Sub pbStar2_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar2.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar2_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar2.MouseMove
        Try
            If e.X < 12 Then
                pbStar2.Tag = 1.5
                BuildStars(1.5)
            Else
                pbStar2.Tag = 2
                BuildStars(2)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar3.Click
        tmpRating = pbStar3.Tag.ToString
    End Sub

    Private Sub pbStar3_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar3.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar3_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar3.MouseMove
        Try
            If e.X < 12 Then
                pbStar3.Tag = 2.5
                BuildStars(2.5)
            Else
                pbStar3.Tag = 3
                BuildStars(3)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar4.Click
        tmpRating = pbStar4.Tag.ToString
    End Sub

    Private Sub pbStar4_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar4.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar4_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar4.MouseMove
        Try
            If e.X < 12 Then
                pbStar4.Tag = 3.5
                BuildStars(3.5)
            Else
                pbStar4.Tag = 4
                BuildStars(4)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar5.Click
        tmpRating = pbStar5.Tag.ToString
    End Sub

    Private Sub pbStar5_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar5.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar5_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar5.MouseMove
        Try
            If e.X < 12 Then
                pbStar5.Tag = 4.5
                BuildStars(4.5)
            Else
                pbStar5.Tag = 5
                BuildStars(5)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar6.Click
        tmpRating = pbStar6.Tag.ToString
    End Sub

    Private Sub pbStar6_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar6.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar6_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar6.MouseMove
        Try
            If e.X < 12 Then
                pbStar6.Tag = 5.5
                BuildStars(5.5)
            Else
                pbStar6.Tag = 6
                BuildStars(6)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar7.Click
        tmpRating = pbStar7.Tag.ToString
    End Sub

    Private Sub pbStar7_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar7.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar7_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar7.MouseMove
        Try
            If e.X < 12 Then
                pbStar7.Tag = 6.5
                BuildStars(6.5)
            Else
                pbStar7.Tag = 7
                BuildStars(7)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar8.Click
        tmpRating = pbStar8.Tag.ToString
    End Sub

    Private Sub pbStar8_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar8.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar8_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar8.MouseMove
        Try
            If e.X < 12 Then
                pbStar8.Tag = 7.5
                BuildStars(7.5)
            Else
                pbStar8.Tag = 8
                BuildStars(8)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar9.Click
        tmpRating = pbStar9.Tag.ToString
    End Sub

    Private Sub pbStar9_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar9.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar9_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar9.MouseMove
        Try
            If e.X < 12 Then
                pbStar9.Tag = 8.5
                BuildStars(8.5)
            Else
                pbStar9.Tag = 9
                BuildStars(9)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar10.Click
        tmpRating = pbStar10.Tag.ToString
    End Sub

    Private Sub pbStar10_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar10.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar10_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbStar10.MouseMove
        Try
            If e.X < 12 Then
                pbStar10.Tag = 9.5
                BuildStars(9.5)
            Else
                pbStar10.Tag = 10
                BuildStars(10)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub SetInfo()
        tmpDBElement.TVEpisode.Aired = txtAired.Text.Trim
        tmpDBElement.TVEpisode.AddCreditsFromString(txtCredits.Text.Trim)
        tmpDBElement.TVEpisode.AddDirectorsFromString(txtDirectors.Text.Trim)
        tmpDBElement.TVEpisode.Episode = Convert.ToInt32(txtEpisode.Text.Trim)
        tmpDBElement.TVEpisode.Plot = txtPlot.Text.Trim
        tmpDBElement.TVEpisode.Rating = tmpRating
        tmpDBElement.TVEpisode.Runtime = txtRuntime.Text.Trim
        tmpDBElement.TVEpisode.Season = Convert.ToInt32(txtSeason.Text.Trim)
        tmpDBElement.TVEpisode.Title = txtTitle.Text.Trim
        tmpDBElement.TVEpisode.Votes = txtVotes.Text.Trim
        tmpDBElement.TVEpisode.VideoSource = txtVideoSource.Text.Trim
        tmpDBElement.TVEpisode.UserRating = If(Integer.TryParse(txtUserRating.Text.Trim, 0), CInt(txtUserRating.Text.Trim), 0)
        tmpDBElement.VideoSource = txtVideoSource.Text.Trim

        'Actors
        tmpDBElement.TVEpisode.Actors.Clear()
        If lvActors.Items.Count > 0 Then
            Dim iOrder As Integer = 0
            For Each lviActor As ListViewItem In lvActors.Items
                Dim addActor As MediaContainers.Person = DirectCast(lviActor.Tag, MediaContainers.Person)
                addActor.Order = iOrder
                iOrder += 1
                tmpDBElement.TVEpisode.Actors.Add(addActor)
            Next
        End If

        If chkWatched.Checked Then
            'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
            If Not tmpDBElement.TVEpisode.PlaycountSpecified Then
                tmpDBElement.TVEpisode.Playcount = 1
                tmpDBElement.TVEpisode.LastPlayed = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
            End If
        Else
            'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
            If tmpDBElement.TVEpisode.PlaycountSpecified Then
                tmpDBElement.TVEpisode.Playcount = 0
                tmpDBElement.TVEpisode.LastPlayed = String.Empty
            End If
        End If

        Dim removeSubtitles As New List(Of MediaContainers.Subtitle)
        For Each Subtitle In tmpDBElement.Subtitles
            If Subtitle.toRemove Then
                removeSubtitles.Add(Subtitle)
            End If
        Next
        For Each Subtitle In removeSubtitles
            If File.Exists(Subtitle.SubsPath) Then
                File.Delete(Subtitle.SubsPath)
            End If
            tmpDBElement.Subtitles.Remove(Subtitle)
        Next
    End Sub

    Private Sub SetUp()
        'Download
        Dim strDownload As String = Master.eLang.GetString(373, "Download")
        btnSetFanartDL.Text = strDownload
        btnSetPosterDL.Text = strDownload
        btnSetSubtitleDL.Text = strDownload

        'Loacal Browse
        Dim strLocalBrowse As String = Master.eLang.GetString(78, "Local Browse")
        btnSetFanartLocal.Text = strLocalBrowse
        btnSetPosterLocal.Text = strLocalBrowse
        btnSetSubtitleLocal.Text = strLocalBrowse

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        btnRemoveFanart.Text = strRemove
        btnRemovePoster.Text = strRemove
        btnRemoveSubtitle.Text = strRemove

        'Scrape
        Dim strScrape As String = Master.eLang.GetString(79, "Scrape")
        btnSetFanartScrape.Text = strScrape
        btnSetPosterScrape.Text = strScrape
        btnSetSubtitleScrape.Text = strScrape

        Dim mTitle As String = String.Empty
        mTitle = tmpDBElement.TVEpisode.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(656, "Edit Episode"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Text = sTitle
        tsFilename.Text = tmpDBElement.Filename
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        chkWatched.Text = Master.eLang.GetString(981, "Watched")
        colName.Text = Master.eLang.GetString(232, "Name")
        colRole.Text = Master.eLang.GetString(233, "Role")
        colThumb.Text = Master.eLang.GetString(234, "Thumb")
        lblActors.Text = String.Concat(Master.eLang.GetString(231, "Actors"), ":")
        lblAired.Text = String.Concat(Master.eLang.GetString(728, "Aired"), ":")
        lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        lblDirectors.Text = String.Concat(Master.eLang.GetString(940, "Directors"), ":")
        lblEpisode.Text = String.Concat(Master.eLang.GetString(727, "Episode"), ":")
        lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        lblRating.Text = Master.eLang.GetString(245, "Rating:")
        lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        lblSeason.Text = String.Concat(Master.eLang.GetString(650, "Season"), ":")
        lblTitle.Text = Master.eLang.GetString(246, "Title:")
        lblTopDetails.Text = Master.eLang.GetString(656, "Edit the details for the selected episode.")
        lblTopTitle.Text = Master.eLang.GetString(657, "Edit Episode")
        lblUserRating.Text = String.Concat(Master.eLang.GetString(1467, "User Rating"), ":")
        lblVideoSource.Text = String.Concat(Master.eLang.GetString(824, "Video Source"), ":")
        lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        tpPoster.Text = Master.eLang.GetString(148, "Poster")
        tpDetails.Text = Master.eLang.GetString(26, "Details")
        tpFrameExtraction.Text = Master.eLang.GetString(256, "Frame Extraction")
        tpMetaData.Text = Master.eLang.GetString(59, "Meta Data")
    End Sub

    Private Sub txtEpisode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEpisode.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtSeason_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSeason.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        If mType = Enums.ModuleEventType.FrameExtrator_TVEpisode Then
            tmpDBElement.ImagesContainer.Poster.ImageOriginal.LoadFromFile(Path.Combine(Master.TempPath, "frame.jpg"), True)
            If tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing Then
                pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                pbPoster.Tag = tmpDBElement.ImagesContainer.Poster

                lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                lblPosterSize.Visible = True
            End If
            'Poster.Image = DirectCast(_params(0), Bitmap)   'New Bitmap(pbFrame.Image)
            ' pbPoster.Image = DirectCast(_params(1), Image)   'pbFrame.Image
        End If
    End Sub

    Private Sub btnSetFanartLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                tmpDBElement.ImagesContainer.Fanart.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
                If tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                    pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                    pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

                    lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                    lblFanartSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeFanart, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.EpisodeFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                    If tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.Fanart.ImageOriginal.LoadFromMemoryStream Then
                        pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                        lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                        lblFanartSize.Visible = True
                    Else
                        pbFanart.Image = Nothing
                        pbFanart.Tag = Nothing
                        lblFanartSize.Text = String.Empty
                        lblFanartSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetEpisodeFanartDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        tmpDBElement.ImagesContainer.Fanart = tImage
                        pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                        pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

                        lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                        lblFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub


    Private Sub btnSetPosterLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                tmpDBElement.ImagesContainer.Poster.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
                If tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing Then
                    pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                    pbPoster.Tag = tmpDBElement.ImagesContainer.Poster

                    lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                    lblPosterSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodePoster, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.EpisodePosters.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                    If tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.Poster.ImageOriginal.LoadFromMemoryStream Then
                        pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                        lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                        lblPosterSize.Visible = True
                    Else
                        pbPoster.Image = Nothing
                        pbPoster.Tag = Nothing
                        lblPosterSize.Text = String.Empty
                        lblPosterSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetEpisodePosterDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        tmpDBElement.ImagesContainer.Poster = tImage
                        pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                        pbPoster.Tag = tmpDBElement.ImagesContainer.Poster

                        lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                        lblPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            txtPlot.SelectAll()
        End If
    End Sub

    Private Sub lvSubtitles_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvSubtitles.KeyDown
        If e.KeyCode = Keys.Delete Then DeleteSubtitle()
    End Sub

    Private Sub lvSubtitles_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvSubtitles.DoubleClick
        If lvSubtitles.SelectedItems.Count > 0 Then
            If lvSubtitles.SelectedItems.Item(0).Tag.ToString <> "Header" Then
                EditSubtitle()
            End If
        End If
    End Sub

    Private Sub lvSubtitles_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvSubtitles.SelectedIndexChanged
        If lvSubtitles.SelectedItems.Count > 0 Then
            If lvSubtitles.SelectedItems.Item(0).Tag.ToString = "Header" Then
                lvSubtitles.SelectedItems.Clear()
                btnRemoveSubtitle.Enabled = False
                txtSubtitlesPreview.Clear()
            Else
                btnRemoveSubtitle.Enabled = True
                txtSubtitlesPreview.Text = ReadSubtitle(lvSubtitles.SelectedItems.Item(0).SubItems(1).Text.ToString)
            End If
        Else
            btnRemoveSubtitle.Enabled = False
            txtSubtitlesPreview.Clear()
        End If
    End Sub

    Private Function ReadSubtitle(ByVal sPath As String) As String
        Dim sText As String = String.Empty

        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            Try
                Dim objReader As New StreamReader(sPath)

                sText = objReader.ReadToEnd

                objReader.Close()

                Return sText
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If

        Return String.Empty
    End Function

    Private Sub EditSubtitle()
        Try
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
                        'NeedToRefresh = True
                        LoadSubtitles()
                    End If
                End Using
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub DeleteSubtitle()
        Try
            If lvSubtitles.SelectedItems.Count > 0 Then
                Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
                If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                    tmpDBElement.Subtitles(Convert.ToInt16(i.Text)).toRemove = True
                End If
                'NeedToRefresh = True
                LoadSubtitles()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub LoadSubtitles()
        Dim c As Integer
        Dim g As New ListViewGroup
        Dim i As New ListViewItem
        lvSubtitles.Groups.Clear()
        lvSubtitles.Items.Clear()
        Try
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
                        i.SubItems.Add(s.SubsPath)
                        i.SubItems.Add(s.LongLanguage)
                        i.SubItems.Add(s.SubsType)
                        i.SubItems.Add(If(s.SubsForced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))

                        If s.toRemove Then
                            i.ForeColor = Color.Red
                        End If

                        g.Items.Add(i)
                        lvSubtitles.Items.Add(i)
                    End If
                Next
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub txtUserRating_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUserRating.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtUserRating_TextChanged(sender As Object, e As EventArgs) Handles txtUserRating.TextChanged
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

#End Region 'Methods

End Class