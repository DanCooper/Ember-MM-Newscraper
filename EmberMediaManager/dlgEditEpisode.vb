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
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class dlgEditEpisode

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private EpisodeFanart As New Images With {.IsEdit = True}
    Private lvwActorSorter As ListViewColumnSorter
    Private EpisodePoster As New Images With {.IsEdit = True}
    Private PreviousFrameValue As Integer
    Private tmpRating As String

#End Region 'Fields

#Region "Methods"

    Private Sub btnActorDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorDown.Click
        If Me.lvActors.SelectedItems.Count > 0 AndAlso Not IsNothing(Me.lvActors.SelectedItems(0)) AndAlso Me.lvActors.SelectedIndices(0) < (Me.lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = Me.lvActors.SelectedIndices(0)
            Me.lvActors.Items.Insert(iIndex + 2, DirectCast(Me.lvActors.SelectedItems(0).Clone, ListViewItem))
            Me.lvActors.Items.RemoveAt(iIndex)
            Me.lvActors.Items(iIndex + 1).Selected = True
            Me.lvActors.Select()
        End If
    End Sub

    Private Sub btnActorUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorUp.Click
        Try
            If Me.lvActors.SelectedItems.Count > 0 AndAlso Not IsNothing(Me.lvActors.SelectedItems(0)) AndAlso Me.lvActors.SelectedIndices(0) > 0 Then
                Dim iIndex As Integer = Me.lvActors.SelectedIndices(0)
                Me.lvActors.Items.Insert(iIndex - 1, DirectCast(Me.lvActors.SelectedItems(0).Clone, ListViewItem))
                Me.lvActors.Items.RemoveAt(iIndex + 1)
                Me.lvActors.Items(iIndex - 1).Selected = True
                Me.lvActors.Select()
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnAddActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddActor.Click
        Try
            Dim eActor As New MediaContainers.Person
            Using dAddEditActor As New dlgAddEditActor
                eActor = dAddEditActor.ShowDialog(True)
            End Using
            If Not IsNothing(eActor) Then
                Dim lvItem As ListViewItem = Me.lvActors.Items.Add(eActor.Name)
                lvItem.SubItems.Add(eActor.Role)
                lvItem.SubItems.Add(eActor.Thumb)
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnEditActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditActor.Click
        Me.EditActor()
    End Sub


    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        Try
            If dlgManualEdit.ShowDialog(Master.currShow.EpNfoPath) = Windows.Forms.DialogResult.OK Then
                Master.currShow.TVEp = NFO.LoadTVEpFromNFO(Master.currShow.EpNfoPath, Master.currShow.TVEp.Season, Master.currShow.TVEp.Episode)
                Me.FillInfo()
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnRemoveEpisodeFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveEpisodeFanart.Click
        Me.pbEpisodeFanart.Image = Nothing
        Me.pbEpisodeFanart.Tag = Nothing
        Me.EpisodeFanart.Dispose()
    End Sub

    Private Sub btnRemoveEpisodePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveEpisodePoster.Click
        Me.pbEpisodePoster.Image = Nothing
        Me.pbEpisodePoster.Tag = Nothing
        Me.EpisodePoster.Dispose()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Me.DeleteActors()
    End Sub

    Private Sub btnSetEpisodeFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetEpisodeFanartScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.EpisodeFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(EpisodeFanart, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            EpisodeFanart = tImage
            Me.pbEpisodeFanart.Image = tImage.Image
            Me.pbEpisodeFanart.Tag = tImage

            Me.lblEpisodeFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodeFanart.Image.Width, Me.pbEpisodeFanart.Image.Height)
            Me.lblEpisodeFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetEpisodePosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetEpisodePosterScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.EpisodePoster, Master.currShow.TVEp.Season, Master.currShow.TVEp.Episode, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(EpisodePoster, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            EpisodePoster = tImage
            Me.pbEpisodePoster.Image = tImage.Image
            Me.pbEpisodePoster.Tag = tImage

            Me.lblEpisodePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodePoster.Image.Width, Me.pbEpisodePoster.Image.Height)
            Me.lblEpisodePosterSize.Visible = True
        End If
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

                If sinRating >= 0.5 Then ' if rating is less than .5 out of ten, consider it a 0
                    Select Case (sinRating / 2)
                        Case Is <= 0.5
                            .pbStar1.Image = My.Resources.starhalf
                        Case Is <= 1
                            .pbStar1.Image = My.Resources.star
                        Case Is <= 1.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starhalf
                        Case Is <= 2
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                        Case Is <= 2.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starhalf
                        Case Is <= 3
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                        Case Is <= 3.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starhalf
                        Case Is <= 4
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                        Case Is <= 4.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starhalf
                        Case Else
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                    End Select
                End If
            End With
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
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
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub DeleteActors()
        Try
            If Me.lvActors.Items.Count > 0 Then
                While Me.lvActors.SelectedItems.Count > 0
                    Me.lvActors.Items.Remove(Me.lvActors.SelectedItems(0))
                End While
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub dlgEditEpisode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Master.eSettings.TVEpisodeFanartAnyEnabled Then tcEditEpisode.TabPages.Remove(tpEpisodeFanart)
        If Not Master.eSettings.TVEpisodePosterAnyEnabled Then
            tcEditEpisode.TabPages.Remove(tpEpisodePoster)
            tcEditEpisode.TabPages.Remove(tpFrameExtraction)
        End If

        Me.pbEpisodeFanart.AllowDrop = True
        Me.pbEpisodePoster.AllowDrop = True

        Me.SetUp()

        Me.lvwActorSorter = New ListViewColumnSorter()
        Me.lvActors.ListViewItemSorter = Me.lvwActorSorter

        Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            Me.pnlTop.BackgroundImage = iBackground
        End Using

        Dim dFileInfoEdit As New dlgFileInfo
        dFileInfoEdit.TopLevel = False
        dFileInfoEdit.FormBorderStyle = FormBorderStyle.None
        dFileInfoEdit.BackColor = Color.White
        dFileInfoEdit.Cancel_Button.Visible = False
        Me.pnlFileInfo.Controls.Add(dFileInfoEdit)
        Dim oldwidth As Integer = dFileInfoEdit.Width
        dFileInfoEdit.Width = pnlFileInfo.Width
        dFileInfoEdit.Height = pnlFileInfo.Height
        dFileInfoEdit.Show(True)

        Dim params As New List(Of Object)(New Object() {New Panel})
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVFrameExtrator, params, Nothing, True)
        pnlFrameExtrator.Controls.Add(DirectCast(params(0), Panel))
        If String.IsNullOrEmpty(pnlFrameExtrator.Controls.Item(0).Name) Then
            tcEditEpisode.TabPages.Remove(tpFrameExtraction)
        End If

        Me.FillInfo()
    End Sub

    Private Sub EditActor()
        Try
            If Me.lvActors.SelectedItems.Count > 0 Then
                Dim lvwItem As ListViewItem = Me.lvActors.SelectedItems(0)
                Dim eActor As New MediaContainers.Person With {.Name = lvwItem.Text, .Role = lvwItem.SubItems(1).Text, .Thumb = lvwItem.SubItems(2).Text}
                Using dAddEditActor As New dlgAddEditActor
                    eActor = dAddEditActor.ShowDialog(False, eActor)
                End Using
                If Not IsNothing(eActor) Then
                    lvwItem.Text = eActor.Name
                    lvwItem.SubItems(1).Text = eActor.Role
                    lvwItem.SubItems(2).Text = eActor.Thumb
                    lvwItem.Selected = True
                    lvwItem.EnsureVisible()
                End If
                eActor = Nothing
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub FillInfo()
        With Me
            If Not String.IsNullOrEmpty(Master.currShow.TVEp.Title) Then .txtTitle.Text = Master.currShow.TVEp.Title
            If Not String.IsNullOrEmpty(Master.currShow.TVEp.Plot) Then .txtPlot.Text = Master.currShow.TVEp.Plot
            If Not String.IsNullOrEmpty(Master.currShow.TVEp.Aired) Then .txtAired.Text = Master.currShow.TVEp.Aired
            If Not String.IsNullOrEmpty(Master.currShow.TVEp.Director) Then .txtDirector.Text = Master.currShow.TVEp.Director
            If Not String.IsNullOrEmpty(Master.currShow.TVEp.Credits) Then .txtCredits.Text = Master.currShow.TVEp.Credits
            If Not String.IsNullOrEmpty(Master.currShow.TVEp.Season.ToString) Then .txtSeason.Text = Master.currShow.TVEp.Season.ToString
            If Not String.IsNullOrEmpty(Master.currShow.TVEp.Episode.ToString) Then .txtEpisode.Text = Master.currShow.TVEp.Episode.ToString

            Dim lvItem As ListViewItem
            .lvActors.Items.Clear()
            For Each imdbAct As MediaContainers.Person In Master.currShow.TVEp.Actors
                lvItem = .lvActors.Items.Add(imdbAct.Name)
                lvItem.SubItems.Add(imdbAct.Role)
                lvItem.SubItems.Add(imdbAct.Thumb)
            Next

            Dim tRating As Single = NumUtils.ConvertToSingle(Master.currShow.TVEp.Rating)
            .tmpRating = tRating.ToString
            .pbStar1.Tag = tRating
            .pbStar2.Tag = tRating
            .pbStar3.Tag = tRating
            .pbStar4.Tag = tRating
            .pbStar5.Tag = tRating
            If tRating > 0 Then .BuildStars(tRating)

            If Master.currShow.TVEp.Playcount = "" Or Master.currShow.TVEp.Playcount = "0" Then
                Me.chkWatched.Checked = False
            Else
                'Playcount <> Empty and not 0 -> Tag filled -> Checked!
                Me.chkWatched.Checked = True
            End If

            If Master.eSettings.TVEpisodeFanartAnyEnabled Then
                EpisodeFanart.FromFile(Master.currShow.EpFanartPath)
                If Not IsNothing(EpisodeFanart.Image) Then
                    .pbEpisodeFanart.Image = EpisodeFanart.Image
                    .pbEpisodeFanart.Tag = EpisodeFanart

                    .lblEpisodeFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbEpisodeFanart.Image.Width, .pbEpisodeFanart.Image.Height)
                    .lblEpisodeFanartSize.Visible = True
                End If
            End If

            If Master.eSettings.TVEpisodePosterAnyEnabled Then
                EpisodePoster.FromFile(Master.currShow.EpPosterPath)
                If Not IsNothing(EpisodePoster.Image) Then
                    .pbEpisodePoster.Image = EpisodePoster.Image
                    .pbEpisodePoster.Tag = EpisodePoster

                    .lblEpisodePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbEpisodePoster.Image.Width, .pbEpisodePoster.Image.Height)
                    .lblEpisodePosterSize.Visible = True
                End If
            End If
        End With
    End Sub

    Private Sub lvActors_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActors.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = Me.lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (Me.lvwActorSorter.Order = SortOrder.Ascending) Then
                    Me.lvwActorSorter.Order = SortOrder.Descending
                Else
                    Me.lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                Me.lvwActorSorter.SortColumn = e.Column
                Me.lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            Me.lvActors.Sort()
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvActors.DoubleClick
        EditActor()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.SetInfo()

            Master.DB.SaveTVEpToDB(Master.currShow, False, True, False, True)

            Me.CleanUp()

        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbEpisodeFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbEpisodeFanart.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            EpisodeFanart = tImage
            Me.pbEpisodeFanart.Image = EpisodeFanart.Image
            Me.pbEpisodeFanart.Tag = EpisodeFanart
            Me.lblEpisodeFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodeFanart.Image.Width, Me.pbEpisodeFanart.Image.Height)
            Me.lblEpisodeFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbEpisodeFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbEpisodeFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbEpisodePoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbEpisodePoster.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            EpisodePoster = tImage
            Me.pbEpisodePoster.Image = EpisodePoster.Image
            Me.pbEpisodePoster.Tag = EpisodePoster
            Me.lblEpisodePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodePoster.Image.Width, Me.pbEpisodePoster.Image.Height)
            Me.lblEpisodePosterSize.Visible = True
        End If
    End Sub

    Private Sub pbEpisodePoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbEpisodePoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbStar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar1.Click
        Me.tmpRating = Me.pbStar1.Tag.ToString
    End Sub

    Private Sub pbStar1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar1.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar1.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar1.Tag = 1
                Me.BuildStars(1)
            Else
                Me.pbStar1.Tag = 2
                Me.BuildStars(2)
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar2.Click
        Me.tmpRating = Me.pbStar2.Tag.ToString
    End Sub

    Private Sub pbStar2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar2.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar2.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar2.Tag = 3
                Me.BuildStars(3)
            Else
                Me.pbStar2.Tag = 4
                Me.BuildStars(4)
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar3.Click
        Me.tmpRating = Me.pbStar3.Tag.ToString
    End Sub

    Private Sub pbStar3_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar3.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar3.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar3.Tag = 5
                Me.BuildStars(5)
            Else
                Me.pbStar3.Tag = 6
                Me.BuildStars(6)
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar4.Click
        Me.tmpRating = Me.pbStar4.Tag.ToString
    End Sub

    Private Sub pbStar4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar4.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar4.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar4.Tag = 7
                Me.BuildStars(7)
            Else
                Me.pbStar4.Tag = 8
                Me.BuildStars(8)
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar5.Click
        Me.tmpRating = Me.pbStar5.Tag.ToString
    End Sub

    Private Sub pbStar5_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar5.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub pbStar5_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar5.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar5.Tag = 9
                Me.BuildStars(9)
            Else
                Me.pbStar5.Tag = 10
                Me.BuildStars(10)
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub SetInfo()
        Try
            With Me

                Master.currShow.TVEp.Title = .txtTitle.Text.Trim
                Master.currShow.TVEp.Plot = .txtPlot.Text.Trim
                Master.currShow.TVEp.Aired = .txtAired.Text.Trim
                Master.currShow.TVEp.Director = .txtDirector.Text.Trim
                Master.currShow.TVEp.Credits = .txtCredits.Text.Trim
                Master.currShow.TVEp.Season = Convert.ToInt32(.txtSeason.Text.Trim)
                Master.currShow.TVEp.Episode = Convert.ToInt32(.txtEpisode.Text.Trim)
                Master.currShow.TVEp.Rating = .tmpRating

                Master.currShow.TVEp.Actors.Clear()

                If .lvActors.Items.Count > 0 Then
                    For Each lviActor As ListViewItem In .lvActors.Items
                        Dim addActor As New MediaContainers.Person
                        addActor.Name = lviActor.Text.Trim
                        addActor.Role = lviActor.SubItems(1).Text.Trim
                        addActor.Thumb = lviActor.SubItems(2).Text.Trim

                        Master.currShow.TVEp.Actors.Add(addActor)
                    Next
                End If

                If chkWatched.Checked Then
                    'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
                    If String.IsNullOrEmpty(Master.currShow.TVEp.Playcount) Or Master.currShow.TVEp.Playcount = "0" Then
                        Master.currShow.TVEp.Playcount = "1"
                    End If

                    'If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                    '    For Each a In FileUtils.GetFilenameList.Movie(Master.currMovie.Filename, Master.currMovie.isSingle, Enums.MovieModType.WatchedFile)
                    '        If Not File.Exists(a) Then
                    '            Dim fs As FileStream = File.Create(a)
                    '            fs.Close()
                    '        End If
                    '    Next
                    'End If
                Else
                    'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
                    If IsNumeric(Master.currShow.TVEp.Playcount) AndAlso CInt(Master.currShow.TVEp.Playcount) > 0 Then
                        Master.currShow.TVEp.Playcount = ""
                    End If

                    'If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                    '    For Each a In FileUtils.GetFilenameList.Movie(Master.currMovie.Filename, Master.currMovie.isSingle, Enums.MovieModType.WatchedFile)
                    '        If File.Exists(a) Then
                    '            File.Delete(a)
                    '        End If
                    '    Next
                    'End If
                End If

                'Episode Fanart
                If Not IsNothing(.EpisodeFanart.Image) Then
                    Master.currShow.EpFanartPath = .EpisodeFanart.SaveAsTVEpisodeFanart(Master.currShow)
                Else
                    .EpisodeFanart.DeleteTVEpisodeFanart(Master.currShow)
                    Master.currShow.EpFanartPath = String.Empty
                End If

                'Episode Poster
                If Not IsNothing(.EpisodePoster.Image) Then
                    Master.currShow.EpPosterPath = .EpisodePoster.SaveAsTVEpisodePoster(Master.currShow)
                Else
                    .EpisodePoster.DeleteTVEpisodePosters(Master.currShow)
                    Master.currShow.EpPosterPath = String.Empty
                End If
            End With
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub SetUp()
        Dim mTitle As String = String.Empty
        mTitle = Master.currShow.TVEp.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(656, "Edit Episode"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Me.Text = sTitle
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        Me.btnRemoveEpisodeFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnRemoveEpisodePoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnSetEpisodeFanart.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.btnSetEpisodeFanartDL.Text = Master.eLang.GetString(266, "Change Fanart (Download)")
        Me.btnSetEpisodeFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetEpisodePoster.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.btnSetEpisodePosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetEpisodePosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.chkWatched.Text = Master.eLang.GetString(981, "Watched")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colRole.Text = Master.eLang.GetString(233, "Role")
        Me.colThumb.Text = Master.eLang.GetString(234, "Thumb")
        Me.lblActors.Text = Master.eLang.GetString(231, "Actors:")
        Me.lblAired.Text = Master.eLang.GetString(658, "Aired:")
        Me.lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        Me.lblDirector.Text = Master.eLang.GetString(239, "Director:")
        Me.lblEpisode.Text = Master.eLang.GetString(660, "Episode:")
        Me.lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        Me.lblRating.Text = Master.eLang.GetString(245, "Rating:")
        Me.lblSeason.Text = Master.eLang.GetString(659, "Season:")
        Me.lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Me.lblTopDetails.Text = Master.eLang.GetString(656, "Edit the details for the selected episode.")
        Me.lblTopTitle.Text = Master.eLang.GetString(657, "Edit Episode")
        Me.tpEpisodeFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpEpisodePoster.Text = Master.eLang.GetString(148, "Poster")
        Me.tpEpsiodeDetails.Text = Master.eLang.GetString(26, "Details")
        Me.tpFrameExtraction.Text = Master.eLang.GetString(256, "Frame Extraction")
        Me.tpEpisodeMetaData.Text = Master.eLang.GetString(59, "Meta Data")
    End Sub

    Private Sub txtEpisode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEpisode.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar, True) AndAlso Not e.KeyChar = "-"
    End Sub

    Private Sub txtSeason_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSeason.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar, True) AndAlso Not e.KeyChar = "-"
    End Sub

    Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        If mType = Enums.ModuleEventType.TVFrameExtrator Then
            EpisodePoster.FromFile(Path.Combine(Master.TempPath, "frame.jpg"))
            If Not IsNothing(EpisodePoster.Image) Then
                Me.pbEpisodePoster.Image = EpisodePoster.Image
                Me.pbEpisodePoster.Tag = EpisodePoster

                Me.lblEpisodePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodePoster.Image.Width, Me.pbEpisodePoster.Image.Height)
                Me.lblEpisodePosterSize.Visible = True
            End If
            'Me.Poster.Image = DirectCast(_params(0), Bitmap)   'New Bitmap(pbFrame.Image)
            ' Me.pbPoster.Image = DirectCast(_params(1), Image)   'pbFrame.Image
        End If
    End Sub


    Private Sub btnSetEpisodePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetEpisodePoster.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currShow.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                EpisodePoster.FromFile(ofdImage.FileName)
                If Not IsNothing(EpisodePoster.Image) Then
                    Me.pbEpisodePoster.Image = EpisodePoster.Image
                    Me.pbEpisodePoster.Tag = EpisodePoster

                    Me.lblEpisodePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodePoster.Image.Width, Me.pbEpisodePoster.Image.Height)
                    Me.lblEpisodePosterSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnSetEpisodePosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetEpisodePosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        EpisodePoster = tImage
                        Me.pbEpisodePoster.Image = EpisodePoster.Image
                        Me.pbEpisodePoster.Tag = EpisodePoster

                        Me.lblEpisodePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodePoster.Image.Width, Me.pbEpisodePoster.Image.Height)
                        Me.lblEpisodePosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnSetEpisodeFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetEpisodeFanart.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currShow.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                EpisodeFanart.FromFile(ofdImage.FileName)
                If Not IsNothing(EpisodeFanart.Image) Then
                    Me.pbEpisodeFanart.Image = EpisodeFanart.Image
                    Me.pbEpisodeFanart.Tag = EpisodeFanart

                    Me.lblEpisodeFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodeFanart.Image.Width, Me.pbEpisodeFanart.Image.Height)
                    Me.lblEpisodeFanartSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnSetEpisodeFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetEpisodeFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        EpisodeFanart = tImage
                        Me.pbEpisodeFanart.Image = EpisodeFanart.Image
                        Me.pbEpisodeFanart.Tag = EpisodeFanart

                        Me.lblEpisodeFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEpisodeFanart.Image.Width, Me.pbEpisodeFanart.Image.Height)
                        Me.lblEpisodeFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            Me.txtPlot.SelectAll()
        End If
    End Sub

#End Region 'Methods

End Class