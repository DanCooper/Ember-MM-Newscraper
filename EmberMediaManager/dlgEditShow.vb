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

Public Class dlgEditShow

#Region "Fields"

    Private ASBanner As New Images With {.IsEdit = True}
    Private ASFanart As New Images With {.IsEdit = True}
    Private ASPoster As New Images With {.IsEdit = True}
    Private Banner As New Images With {.IsEdit = True}
    Private Fanart As New Images With {.IsEdit = True}
    Private lvwActorSorter As ListViewColumnSorter
    Private Poster As New Images With {.IsEdit = True}
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetASBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASBannerScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.AllSeasonsBanner, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ASBanner, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ASBanner = tImage
            Me.pbASBanner.Image = tImage.Image
            Me.pbASBanner.Tag = tImage
        End If
    End Sub

    Private Sub btnSetASBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASBannerLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ASBanner.FromFile(ofdImage.FileName)
                If Not IsNothing(ASBanner.Image) Then
                    Me.pbASBanner.Image = ASBanner.Image
                    Me.pbASBanner.Tag = ASBanner

                    Me.lblASBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASBanner.Image.Width, Me.pbASBanner.Image.Height)
                    Me.lblASBannerSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetASBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ASBanner = tImage
                        Me.pbASBanner.Image = ASBanner.Image
                        Me.pbASBanner.Tag = ASBanner

                        Me.lblASBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASBanner.Image.Width, Me.pbASBanner.Image.Height)
                        Me.lblASBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetASFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASFanartScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.AllSeasonsFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ASFanart, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ASFanart = tImage
            Me.pbASFanart.Image = tImage.Image
            Me.pbASFanart.Tag = tImage
        End If
    End Sub

    Private Sub btnSetASFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASFanartLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ASFanart.FromFile(ofdImage.FileName)
                If Not IsNothing(ASFanart.Image) Then
                    Me.pbASFanart.Image = ASFanart.Image
                    Me.pbASFanart.Tag = ASFanart

                    Me.lblASFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASFanart.Image.Width, Me.pbASFanart.Image.Height)
                    Me.lblASFanartSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetASFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ASFanart = tImage
                        Me.pbASFanart.Image = ASFanart.Image
                        Me.pbASFanart.Tag = ASFanart

                        Me.lblASFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASFanart.Image.Width, Me.pbASFanart.Image.Height)
                        Me.lblASFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetASPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASPosterScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.AllSeasonsPoster, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ASPoster, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ASPoster = tImage
            Me.pbASPoster.Image = tImage.Image
            Me.pbASPoster.Tag = tImage
        End If
    End Sub

    Private Sub btnSetASPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASPosterLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ASPoster.FromFile(ofdImage.FileName)
                If Not IsNothing(ASPoster.Image) Then
                    Me.pbASPoster.Image = ASPoster.Image
                    Me.pbASPoster.Tag = ASPoster

                    Me.lblASPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASPoster.Image.Width, Me.pbASPoster.Image.Height)
                    Me.lblASPosterSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetASPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ASPoster = tImage
                        Me.pbASPoster.Image = ASPoster.Image
                        Me.pbASPoster.Tag = ASPoster

                        Me.lblASPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASPoster.Image.Width, Me.pbASPoster.Image.Height)
                        Me.lblASPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowBannerScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.ShowBanner, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(Banner, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            Banner = tImage
            Me.pbShowBanner.Image = tImage.Image
            Me.pbShowBanner.Tag = tImage
        End If
    End Sub

    Private Sub btnSetBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowBannerLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Banner.FromFile(ofdImage.FileName)
                If Not IsNothing(Banner.Image) Then
                    Me.pbShowBanner.Image = Banner.Image
                    Me.pbShowBanner.Tag = Banner

                    Me.lblShowBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowBanner.Image.Width, Me.pbShowBanner.Image.Height)
                    Me.lblShowBannerSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Banner = tImage
                        Me.pbShowBanner.Image = Banner.Image
                        Me.pbShowBanner.Tag = Banner

                        Me.lblShowBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowBanner.Image.Width, Me.pbShowBanner.Image.Height)
                        Me.lblShowBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnEditActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditActor.Click
        Me.EditActor()
    End Sub

    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        Try
            If dlgManualEdit.ShowDialog(Master.currShow.ShowNfoPath) = Windows.Forms.DialogResult.OK Then
                Master.currShow.TVShow = NFO.LoadTVShowFromNFO(Master.currShow.ShowNfoPath)
                Me.FillInfo()
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnRemoveASBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveASBanner.Click
        Me.pbASBanner.Image = Nothing
        Me.pbASBanner.Tag = Nothing
        Me.ASBanner.Dispose()
    End Sub

    Private Sub btnRemoveASFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveASFanart.Click
        Me.pbASFanart.Image = Nothing
        Me.pbASFanart.Tag = Nothing
        Me.ASFanart.Dispose()
    End Sub

    Private Sub btnRemoveASPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveASPoster.Click
        Me.pbASPoster.Image = Nothing
        Me.pbASPoster.Tag = Nothing
        Me.ASPoster.Dispose()
    End Sub

    Private Sub btnRemoveBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowBanner.Click
        Me.pbShowBanner.Image = Nothing
        Me.pbShowBanner.Tag = Nothing
        Me.Banner.Dispose()
    End Sub

    Private Sub btnRemoveFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowFanart.Click
        Me.pbShowFanart.Image = Nothing
        Me.pbShowFanart.Tag = Nothing
        Me.Fanart.Dispose()
    End Sub

    Private Sub btnRemovePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowPoster.Click
        Me.pbShowPoster.Image = Nothing
        Me.pbShowPoster.Tag = Nothing
        Me.Poster.Dispose()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Me.DeleteActors()
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Fanart = tImage
                        Me.pbShowFanart.Image = Fanart.Image
                        Me.pbShowFanart.Tag = Fanart

                        Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
                        Me.lblShowFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowFanartScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.ShowFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(Fanart, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            Fanart = tImage
            Me.pbShowFanart.Image = tImage.Image
            Me.pbShowFanart.Tag = tImage

            Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
            Me.lblShowFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowFanartLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Fanart.FromFile(ofdImage.FileName)
                If Not IsNothing(Fanart.Image) Then
                    Me.pbShowFanart.Image = Fanart.Image
                    Me.pbShowFanart.Tag = Fanart

                    Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
                    Me.lblShowFanartSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Poster = tImage
                        Me.pbShowPoster.Image = Poster.Image
                        Me.pbShowPoster.Tag = Poster

                        Me.lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowPoster.Image.Width, Me.pbShowPoster.Image.Height)
                        Me.lblShowPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowPosterScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.ShowPoster, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(Poster, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            Poster = tImage
            Me.pbShowPoster.Image = tImage.Image
            Me.pbShowPoster.Tag = tImage

            Me.lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowPoster.Image.Width, Me.pbShowPoster.Image.Height)
            Me.lblShowPosterSize.Visible = True
        End If
    End Sub

    Private Sub btnSetPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowPosterLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Poster.FromFile(ofdImage.FileName)
                Me.pbShowPoster.Image = Poster.Image
                Me.pbShowPoster.Tag = Poster

                Me.lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowPoster.Image.Width, Me.pbShowPoster.Image.Height)
                Me.lblShowPosterSize.Visible = True
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub BuildStars(ByVal sinRating As Single)
        '//
        ' Convert # rating to star images
        '\\

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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DeleteActors()
        Try
            If Me.lvActors.Items.Count > 0 Then
                While Me.lvActors.SelectedItems.Count > 0
                    Me.lvActors.Items.Remove(Me.lvActors.SelectedItems(0))
                End While
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub dlgEditShow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Master.eSettings.TVASPosterEnabled Then Me.tcEditShow.TabPages.Remove(tpASPoster)

        Me.SetUp()

        Me.lvwActorSorter = New ListViewColumnSorter()
        Me.lvActors.ListViewItemSorter = Me.lvwActorSorter

        Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            Me.pnlTop.BackgroundImage = iBackground
        End Using

        Me.LoadGenres()
        Me.LoadRatings()

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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub FillInfo()
        With Me
            .cbOrdering.SelectedIndex = Master.currShow.Ordering

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Title) Then .txtTitle.Text = Master.currShow.TVShow.Title
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Plot) Then .txtPlot.Text = Master.currShow.TVShow.Plot
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Premiered) Then .txtPremiered.Text = Master.currShow.TVShow.Premiered
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Studio) Then .txtStudio.Text = Master.currShow.TVShow.Studio

            For i As Integer = 0 To .clbGenre.Items.Count - 1
                .clbGenre.SetItemChecked(i, False)
            Next
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Genre) Then
                Dim genreArray() As String
                genreArray = Strings.Split(Master.currShow.TVShow.Genre, " / ")
                For g As Integer = 0 To UBound(genreArray)
                    If .clbGenre.FindString(genreArray(g).Trim) > 0 Then
                        .clbGenre.SetItemChecked(.clbGenre.FindString(genreArray(g).Trim), True)
                    End If
                Next

                If .clbGenre.CheckedItems.Count = 0 Then
                    .clbGenre.SetItemChecked(0, True)
                End If
            Else
                .clbGenre.SetItemChecked(0, True)
            End If

            Dim lvItem As ListViewItem
            .lvActors.Items.Clear()
            For Each imdbAct As MediaContainers.Person In Master.currShow.TVShow.Actors
                lvItem = .lvActors.Items.Add(imdbAct.Name)
                lvItem.SubItems.Add(imdbAct.Role)
                lvItem.SubItems.Add(imdbAct.Thumb)
            Next

            Dim tRating As Single = NumUtils.ConvertToSingle(Master.currShow.TVShow.Rating)
            .tmpRating = tRating.ToString
            .pbStar1.Tag = tRating
            .pbStar2.Tag = tRating
            .pbStar3.Tag = tRating
            .pbStar4.Tag = tRating
            .pbStar5.Tag = tRating
            If tRating > 0 Then .BuildStars(tRating)

            Me.SelectMPAA()

            If Master.eSettings.TVShowBannerEnabled Then
                Banner.FromFile(Master.currShow.ShowBannerPath)
                If Not IsNothing(Banner.Image) Then
                    .pbShowBanner.Image = Banner.Image
                    .pbShowBanner.Tag = Banner

                    .lblShowBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowBanner.Image.Width, .pbShowBanner.Image.Height)
                    .lblShowBannerSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowFanartEnabled Then
                Fanart.FromFile(Master.currShow.ShowFanartPath)
                If Not IsNothing(Fanart.Image) Then
                    .pbShowFanart.Image = Fanart.Image
                    .pbShowFanart.Tag = Fanart

                    .lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowFanart.Image.Width, .pbShowFanart.Image.Height)
                    .lblShowFanartSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowPosterEnabled Then
                Poster.FromFile(Master.currShow.ShowPosterPath)
                If Not IsNothing(Poster.Image) Then
                    .pbShowPoster.Image = Poster.Image
                    .pbShowPoster.Tag = Poster

                    .lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowPoster.Image.Width, .pbShowPoster.Image.Height)
                    .lblShowPosterSize.Visible = True
                End If
            End If

            If Master.eSettings.TVASBannerEnabled Then
                .ASBanner.FromFile(Master.currShow.SeasonBannerPath)
                If Not IsNothing(.ASBanner.Image) Then
                    .pbASBanner.Image = .ASBanner.Image
                    .pbASBanner.Tag = ASBanner

                    .lblASBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbASBanner.Image.Width, .pbASBanner.Image.Height)
                    .lblASBannerSize.Visible = True
                End If
            End If

            If Master.eSettings.TVASPosterEnabled Then
                .ASPoster.FromFile(Master.currShow.SeasonPosterPath)
                If Not IsNothing(.ASPoster.Image) Then
                    .pbASPoster.Image = .ASPoster.Image
                    .pbASPoster.Tag = ASPoster

                    .lblASPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbASPoster.Image.Width, .pbASPoster.Image.Height)
                    .lblASPosterSize.Visible = True
                End If
            End If

            If Master.eSettings.TVASFanartEnabled Then
                .ASFanart.FromFile(Master.currShow.SeasonFanartPath)
                If Not IsNothing(.ASFanart.Image) Then
                    .pbASFanart.Image = .ASFanart.Image
                    .pbASFanart.Tag = ASFanart

                    .lblASFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbASFanart.Image.Width, .pbASFanart.Image.Height)
                    .lblASFanartSize.Visible = True
                End If
            End If
        End With
    End Sub

    Private Sub lbGenre_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbGenre.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbGenre.Items.Count - 1
                Me.clbGenre.SetItemChecked(i, False)
            Next
        Else
            Me.clbGenre.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub LoadGenres()
        Me.clbGenre.Items.Add(Master.eLang.None)

        Me.clbGenre.Items.AddRange(APIXML.GetGenreList)
    End Sub

    Private Sub LoadRatings()
        Me.lbMPAA.Items.Add(Master.eLang.None)

        Me.lbMPAA.Items.AddRange(APIXML.GetTVRatingList)
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvActors.DoubleClick
        EditActor()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.SetInfo()

            Master.DB.SaveTVShowToDB(Master.currShow, False, False, True)

            If Master.eSettings.TVASPosterEnabled Then Master.DB.SaveTVSeasonToDB(Master.currShow, False)

        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SelectMPAA()
        If Not String.IsNullOrEmpty(Master.currShow.TVShow.MPAA) Then
            Try
                If Not IsNothing(APIXML.RatingXML.Element("ratings").Element(Master.eSettings.TVScraperRatingRegion.ToLower)) AndAlso APIXML.RatingXML.Element("ratings").Element(Master.eSettings.TVScraperRatingRegion.ToLower).Descendants("tv").Count > 0 Then
                    Dim l As Integer = Me.lbMPAA.FindString(Strings.Trim(Master.currShow.TVShow.MPAA))
                    Me.lbMPAA.SelectedIndex = l
                    If Me.lbMPAA.SelectedItems.Count = 0 Then
                        Me.lbMPAA.SelectedIndex = 0
                    End If

                    Me.lbMPAA.TopIndex = 0

                Else

                    Me.lbMPAA.SelectedIndex = 0
                End If

            Catch ex As Exception
                Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
            End Try
        Else
            Me.lbMPAA.SelectedIndex = 0
        End If
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            Me.txtPlot.SelectAll()
        End If
    End Sub

    Private Sub SetInfo()
        Try
            With Me
                Master.currShow.Ordering = DirectCast(.cbOrdering.SelectedIndex, Enums.Ordering)

                Master.currShow.TVShow.Title = .txtTitle.Text.Trim
                Master.currShow.TVShow.Plot = .txtPlot.Text.Trim
                Master.currShow.TVShow.Premiered = .txtPremiered.Text.Trim
                Master.currShow.TVShow.Studio = .txtStudio.Text.Trim

                If .lbMPAA.SelectedIndices.Count > 0 AndAlso Not .lbMPAA.SelectedIndex <= 0 Then
                    Master.currShow.TVShow.MPAA = .lbMPAA.SelectedItem.ToString
                End If

                Master.currShow.TVShow.Rating = .tmpRating

                If .clbGenre.CheckedItems.Count > 0 Then

                    If .clbGenre.CheckedIndices.Contains(0) Then
                        Master.currShow.TVShow.Genre = String.Empty
                    Else
                        Dim strGenre As String = String.Empty
                        Dim isFirst As Boolean = True
                        Dim iChecked = From iCheck In .clbGenre.CheckedItems
                        strGenre = Strings.Join(iChecked.ToArray, " / ")
                        Master.currShow.TVShow.Genre = strGenre.Trim
                    End If
                End If

                Master.currShow.TVShow.Actors.Clear()

                If .lvActors.Items.Count > 0 Then
                    For Each lviActor As ListViewItem In .lvActors.Items
                        Dim addActor As New MediaContainers.Person
                        addActor.Name = lviActor.Text.Trim
                        addActor.Role = lviActor.SubItems(1).Text.Trim
                        addActor.Thumb = lviActor.SubItems(2).Text.Trim

                        Master.currShow.TVShow.Actors.Add(addActor)
                    Next
                End If

                If Not IsNothing(.Fanart.Image) Then
                    Master.currShow.ShowFanartPath = .Fanart.SaveAsTVShowFanart(Master.currShow, "")
                Else
                    .Fanart.DeleteTVShowFanart(Master.currShow)
                    Master.currShow.ShowFanartPath = String.Empty
                End If

                If Not IsNothing(.Poster.Image) Then
                    Master.currShow.ShowPosterPath = .Poster.SaveAsTVShowPoster(Master.currShow, "")
                Else
                    .Poster.DeleteTVShowPosters(Master.currShow)
                    Master.currShow.ShowPosterPath = String.Empty
                End If

                If Not IsNothing(.Banner.Image) Then
                    Master.currShow.ShowBannerPath = .Banner.SaveAsTVShowBanner(Master.currShow, "")
                Else
                    .Banner.DeleteTVShowBanner(Master.currShow)
                    Master.currShow.ShowBannerPath = String.Empty
                End If

                If Not IsNothing(.ASBanner.Image) Then
                    Master.currShow.SeasonBannerPath = .ASBanner.SaveAsTVASBanner(Master.currShow, "")
                Else
                    .ASBanner.DeleteTVASBanner(Master.currShow)
                    Master.currShow.SeasonBannerPath = String.Empty
                End If

                If Not IsNothing(.ASFanart.Image) Then
                    Master.currShow.SeasonFanartPath = .ASFanart.SaveAsTVASFanart(Master.currShow, "")
                Else
                    .ASFanart.DeleteTVASFanart(Master.currShow)
                    Master.currShow.SeasonFanartPath = String.Empty
                End If

                If Not IsNothing(.ASPoster.Image) Then
                    Master.currShow.SeasonPosterPath = .ASPoster.SaveAsTVASPoster(Master.currShow, "")
                Else
                    .ASPoster.DeleteTVASPosters(Master.currShow)
                    Master.currShow.SeasonPosterPath = String.Empty
                End If

            End With
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SetUp()
        Dim mTitle As String = Master.currShow.TVShow.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(663, "Edit Show"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Text = sTitle
        Me.btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        Me.btnRemoveASBanner.Text = Master.eLang.GetString(1024, "Remove Banner")
        Me.btnRemoveASFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnRemoveASPoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnRemoveShowBanner.Text = Me.btnRemoveASBanner.Text
        Me.btnRemoveShowFanart.Text = Me.btnRemoveASFanart.Text
        Me.btnRemoveShowPoster.Text = Me.btnRemoveASPoster.Text
        Me.btnSetASBannerDL.Text = Master.eLang.GetString(1023, "Change Banner (Download)")
        Me.btnSetASBannerLocal.Text = Master.eLang.GetString(1021, "Change Banner (Local)")
        Me.btnSetASBannerScrape.Text = Master.eLang.GetString(1022, "Change Banner (Scrape)")
        Me.btnSetASFanartDL.Text = Master.eLang.GetString(266, "Change Fanart (Download)")
        Me.btnSetASFanartLocal.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.btnSetASFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetASPosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetASPosterLocal.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.btnSetASPosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.btnSetShowBannerDL.Text = Me.btnSetASBannerDL.Text
        Me.btnSetShowBannerLocal.Text = Me.btnSetASBannerLocal.Text
        Me.btnSetShowBannerScrape.Text = Me.btnSetASBannerScrape.Text
        Me.btnSetShowFanartDL.Text = Me.btnSetASFanartDL.Text
        Me.btnSetShowFanartLocal.Text = Me.btnSetASFanartLocal.Text
        Me.btnSetShowFanartScrape.Text = Me.btnSetASFanartScrape.Text
        Me.btnSetShowPosterDL.Text = Me.btnSetASPosterDL.Text
        Me.btnSetShowPosterLocal.Text = Me.btnSetASPosterLocal.Text
        Me.btnSetShowPosterScrape.Text = Me.btnSetASPosterScrape.Text
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colRole.Text = Master.eLang.GetString(233, "Role")
        Me.colThumb.Text = Master.eLang.GetString(234, "Thumb")
        Me.lblActors.Text = Master.eLang.GetString(231, "Actors:")
        Me.lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        Me.lblOrdering.Text = Master.eLang.GetString(739, "Episode Ordering:")
        Me.lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        Me.lblPremiered.Text = Master.eLang.GetString(665, "Premiered:")
        Me.lblRating.Text = Master.eLang.GetString(245, "Rating:")
        Me.lblStudio.Text = Master.eLang.GetString(226, "Studio:")
        Me.lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Me.lblTopDetails.Text = Master.eLang.GetString(664, "Edit the details for the selected show.")
        Me.lblTopTitle.Text = Master.eLang.GetString(663, "Edit Show")
        Me.tpASBanner.Text = Master.eLang.GetString(1014, "All Seasons Banner")
        Me.tpASFanart.Text = Master.eLang.GetString(1015, "All Seasons Fanart")
        Me.tpASPoster.Text = Master.eLang.GetString(735, "All Season Poster")
        Me.tpShowBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpShowDetails.Text = Master.eLang.GetString(26, "Details")
        Me.tpShowFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpShowPoster.Text = Master.eLang.GetString(148, "Poster")

        Me.cbOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(350, "DVD"), Master.eLang.GetString(839, "Absolute")})

    End Sub

#End Region 'Methods

End Class