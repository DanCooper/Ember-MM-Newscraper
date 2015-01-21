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
Imports System.Diagnostics

Public Class dlgEditShow

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwEFanarts As New System.ComponentModel.BackgroundWorker

    Private ASBanner As New Images With {.IsEdit = True}
    Private ASFanart As New Images With {.IsEdit = True}
    Private ASLandscape As New Images With {.IsEdit = True}
    Private ASPoster As New Images With {.IsEdit = True}
    Private lvwActorSorter As ListViewColumnSorter
    Private ShowBanner As New Images With {.IsEdit = True}
    Private ShowCharacterArt As New Images With {.IsEdit = True}
    Private ShowClearArt As New Images With {.IsEdit = True}
    Private ShowClearLogo As New Images With {.IsEdit = True}
    Private ShowFanart As New Images With {.IsEdit = True}
    Private ShowLandscape As New Images With {.IsEdit = True}
    Private ShowPoster As New Images With {.IsEdit = True}
    Private tmpRating As String

    'Extrafanarts
    Private efDeleteList As New List(Of String)
    Private EFanartsIndex As Integer = -1
    Private EFanartsList As New List(Of ExtraImages)
    Private EFanartsExtractor As New List(Of String)
    Private EFanartsWarning As Boolean = True
    Private hasClearedEF As Boolean = False
    Private iEFCounter As Integer = 0
    Private iEFLeft As Integer = 1
    Private iEFTop As Integer = 1
    Private pbEFImage() As PictureBox
    Private pnlEFImage() As Panel

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
            Logger.Error(New StackFrame().GetMethod().Name,ex)
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
            Logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub AddEFImage(ByVal sDescription As String, ByVal iIndex As Integer, Extrafanart As ExtraImages)
        Try
            ReDim Preserve Me.pnlEFImage(iIndex)
            ReDim Preserve Me.pbEFImage(iIndex)
            Me.pnlEFImage(iIndex) = New Panel()
            Me.pbEFImage(iIndex) = New PictureBox()
            Me.pbEFImage(iIndex).Name = iIndex.ToString
            Me.pnlEFImage(iIndex).Name = iIndex.ToString
            Me.pnlEFImage(iIndex).Size = New Size(128, 72)
            Me.pbEFImage(iIndex).Size = New Size(128, 72)
            Me.pnlEFImage(iIndex).BackColor = Color.White
            Me.pnlEFImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbEFImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlEFImage(iIndex).Tag = Extrafanart.Image
            Me.pbEFImage(iIndex).Tag = Extrafanart.Image
            Me.pbEFImage(iIndex).Image = CType(Extrafanart.Image.Image.Clone(), Image)
            Me.pnlEFImage(iIndex).Left = iEFLeft
            Me.pbEFImage(iIndex).Left = 0
            Me.pnlEFImage(iIndex).Top = iEFTop
            Me.pbEFImage(iIndex).Top = 0
            Me.pnlShowEFanartsBG.Controls.Add(Me.pnlEFImage(iIndex))
            Me.pnlEFImage(iIndex).Controls.Add(Me.pbEFImage(iIndex))
            Me.pnlEFImage(iIndex).BringToFront()
            AddHandler pbEFImage(iIndex).Click, AddressOf pbEFImage_Click
            AddHandler pnlEFImage(iIndex).Click, AddressOf pnlEFImage_Click
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iEFTop += 74

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
            Logger.Error(New StackFrame().GetMethod().Name,ex)
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

    Private Sub btnRemoveASLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveASLandscape.Click
        Me.pbASLandscape.Image = Nothing
        Me.pbASLandscape.Tag = Nothing
        Me.ASLandscape.Dispose()
    End Sub

    Private Sub btnRemoveASPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveASPoster.Click
        Me.pbASPoster.Image = Nothing
        Me.pbASPoster.Tag = Nothing
        Me.ASPoster.Dispose()
    End Sub

    Private Sub btnRemoveShowBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowBanner.Click
        Me.pbShowBanner.Image = Nothing
        Me.pbShowBanner.Tag = Nothing
        Me.ShowBanner.Dispose()
    End Sub

    Private Sub btnRemoveShowCharacterArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowCharacterArt.Click
        Me.pbShowCharacterArt.Image = Nothing
        Me.pbShowCharacterArt.Tag = Nothing
        Me.ShowCharacterArt.Dispose()
    End Sub

    Private Sub btnRemoveShowClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowClearArt.Click
        Me.pbShowClearArt.Image = Nothing
        Me.pbShowClearArt.Tag = Nothing
        Me.ShowClearArt.Dispose()
    End Sub

    Private Sub btnRemoveShowClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowClearLogo.Click
        Me.pbShowClearLogo.Image = Nothing
        Me.pbShowClearLogo.Tag = Nothing
        Me.ShowClearLogo.Dispose()
    End Sub

    Private Sub btnRemoveShowFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowFanart.Click
        Me.pbShowFanart.Image = Nothing
        Me.pbShowFanart.Tag = Nothing
        Me.ShowFanart.Dispose()
    End Sub

    Private Sub btnRemoveShowLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowLandscape.Click
        Me.pbShowLandscape.Image = Nothing
        Me.pbShowLandscape.Tag = Nothing
        Me.ShowLandscape.Dispose()
    End Sub

    Private Sub btnRemoveShowPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveShowPoster.Click
        Me.pbShowPoster.Image = Nothing
        Me.pbShowPoster.Tag = Nothing
        Me.ShowPoster.Dispose()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Me.DeleteActors()
    End Sub

    Private Sub btnSetASBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASBannerScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.AllSeasonsBanner, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ASBanner, Images))

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
            Logger.Error(New StackFrame().GetMethod().Name, ex)
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
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetASFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASFanartScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.AllSeasonsFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ASFanart, Images))

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
            Logger.Error(New StackFrame().GetMethod().Name, ex)
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
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetASLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASLandscapeScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.AllSeasonsLandscape, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ASLandscape, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ASLandscape = tImage
            Me.pbASLandscape.Image = tImage.Image
            Me.pbASLandscape.Tag = tImage
        End If
    End Sub

    Private Sub btnSetASLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASLandscapeLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ASLandscape.FromFile(ofdImage.FileName)
                If Not IsNothing(ASLandscape.Image) Then
                    Me.pbASLandscape.Image = ASLandscape.Image
                    Me.pbASLandscape.Tag = ASLandscape

                    Me.lblASLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASLandscape.Image.Width, Me.pbASLandscape.Image.Height)
                    Me.lblASLandscapeSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetASLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASLandscapeDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ASLandscape = tImage
                        Me.pbASLandscape.Image = ASLandscape.Image
                        Me.pbASLandscape.Tag = ASLandscape

                        Me.lblASLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASLandscape.Image.Width, Me.pbASLandscape.Image.Height)
                        Me.lblASLandscapeSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetASPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetASPosterScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.AllSeasonsPoster, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ASPoster, Images))

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
            Logger.Error(New StackFrame().GetMethod().Name, ex)
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
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowBannerScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.ShowBanner, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ShowBanner, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ShowBanner = tImage
            Me.pbShowBanner.Image = tImage.Image
            Me.pbShowBanner.Tag = tImage
        End If
    End Sub

    Private Sub btnSetShowBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowBannerLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ShowBanner.FromFile(ofdImage.FileName)
                If Not IsNothing(ShowBanner.Image) Then
                    Me.pbShowBanner.Image = ShowBanner.Image
                    Me.pbShowBanner.Tag = ShowBanner

                    Me.lblShowBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowBanner.Image.Width, Me.pbShowBanner.Image.Height)
                    Me.lblShowBannerSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ShowBanner = tImage
                        Me.pbShowBanner.Image = ShowBanner.Image
                        Me.pbShowBanner.Tag = ShowBanner

                        Me.lblShowBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowBanner.Image.Width, Me.pbShowBanner.Image.Height)
                        Me.lblShowBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowCharacterArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowCharacterArtScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.ShowCharacterArt, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ShowCharacterArt, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ShowCharacterArt = tImage
            Me.pbShowCharacterArt.Image = tImage.Image
            Me.pbShowCharacterArt.Tag = tImage
        End If
    End Sub

    Private Sub btnSetShowCharacterArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowCharacterArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ShowCharacterArt.FromFile(ofdImage.FileName)
                If Not IsNothing(ShowCharacterArt.Image) Then
                    Me.pbShowCharacterArt.Image = ShowCharacterArt.Image
                    Me.pbShowCharacterArt.Tag = ShowCharacterArt

                    Me.lblShowCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowCharacterArt.Image.Width, Me.pbShowCharacterArt.Image.Height)
                    Me.lblShowCharacterArtSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowCharacterArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowCharacterArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ShowCharacterArt = tImage
                        Me.pbShowCharacterArt.Image = ShowCharacterArt.Image
                        Me.pbShowCharacterArt.Tag = ShowCharacterArt

                        Me.lblShowCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowCharacterArt.Image.Width, Me.pbShowCharacterArt.Image.Height)
                        Me.lblShowCharacterArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ShowFanart = tImage
                        Me.pbShowFanart.Image = ShowFanart.Image
                        Me.pbShowFanart.Tag = ShowFanart

                        Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
                        Me.lblShowFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowClearArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowClearArtScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.ShowClearArt, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ShowClearArt, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ShowClearArt = tImage
            Me.pbShowClearArt.Image = tImage.Image
            Me.pbShowClearArt.Tag = tImage
        End If
    End Sub

    Private Sub btnSetShowClearArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowClearArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ShowClearArt.FromFile(ofdImage.FileName)
                If Not IsNothing(ShowClearArt.Image) Then
                    Me.pbShowClearArt.Image = ShowClearArt.Image
                    Me.pbShowClearArt.Tag = ShowClearArt

                    Me.lblShowClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowClearArt.Image.Width, Me.pbShowClearArt.Image.Height)
                    Me.lblShowClearArtSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowClearArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowClearArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ShowClearArt = tImage
                        Me.pbShowClearArt.Image = ShowClearArt.Image
                        Me.pbShowClearArt.Tag = ShowClearArt

                        Me.lblShowClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowClearArt.Image.Width, Me.pbShowClearArt.Image.Height)
                        Me.lblShowClearArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowClearLogoScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowClearLogoScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.ShowClearLogo, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ShowClearLogo, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ShowClearLogo = tImage
            Me.pbShowClearLogo.Image = tImage.Image
            Me.pbShowClearLogo.Tag = tImage
        End If
    End Sub

    Private Sub btnSetShowClearLogoLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowClearLogoLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ShowClearLogo.FromFile(ofdImage.FileName)
                If Not IsNothing(ShowClearLogo.Image) Then
                    Me.pbShowClearLogo.Image = ShowClearLogo.Image
                    Me.pbShowClearLogo.Tag = ShowClearLogo

                    Me.lblShowClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowClearLogo.Image.Width, Me.pbShowClearLogo.Image.Height)
                    Me.lblShowClearLogoSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowClearLogoDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowClearLogoDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ShowClearLogo = tImage
                        Me.pbShowClearLogo.Image = ShowClearLogo.Image
                        Me.pbShowClearLogo.Tag = ShowClearLogo

                        Me.lblShowClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowClearLogo.Image.Width, Me.pbShowClearLogo.Image.Height)
                        Me.lblShowClearLogoSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowFanartScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.ShowFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ShowFanart, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ShowFanart = tImage
            Me.pbShowFanart.Image = tImage.Image
            Me.pbShowFanart.Tag = tImage

            Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
            Me.lblShowFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetShowFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowFanartLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ShowFanart.FromFile(ofdImage.FileName)
                If Not IsNothing(ShowFanart.Image) Then
                    Me.pbShowFanart.Image = ShowFanart.Image
                    Me.pbShowFanart.Tag = ShowFanart

                    Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
                    Me.lblShowFanartSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowLandscapeScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.ShowLandscape, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ShowLandscape, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ShowLandscape = tImage
            Me.pbShowLandscape.Image = tImage.Image
            Me.pbShowLandscape.Tag = tImage
        End If
    End Sub

    Private Sub btnSetShowLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowLandscapeLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ShowLandscape.FromFile(ofdImage.FileName)
                If Not IsNothing(ShowLandscape.Image) Then
                    Me.pbShowLandscape.Image = ShowLandscape.Image
                    Me.pbShowLandscape.Tag = ShowLandscape

                    Me.lblShowLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowLandscape.Image.Width, Me.pbShowLandscape.Image.Height)
                    Me.lblShowLandscapeSize.Visible = True
                End If
            End If
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowLandscapeDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ShowLandscape = tImage
                        Me.pbShowLandscape.Image = ShowLandscape.Image
                        Me.pbShowLandscape.Tag = ShowLandscape

                        Me.lblShowLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowLandscape.Image.Width, Me.pbShowLandscape.Image.Height)
                        Me.lblShowLandscapeSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        ShowPoster = tImage
                        Me.pbShowPoster.Image = ShowPoster.Image
                        Me.pbShowPoster.Tag = ShowPoster

                        Me.lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowPoster.Image.Width, Me.pbShowPoster.Image.Height)
                        Me.lblShowPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetShowPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowPosterScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.ImageType_TV.ShowPoster, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(ShowPoster, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            ShowPoster = tImage
            Me.pbShowPoster.Image = tImage.Image
            Me.pbShowPoster.Tag = tImage

            Me.lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowPoster.Image.Width, Me.pbShowPoster.Image.Height)
            Me.lblShowPosterSize.Visible = True
        End If
    End Sub

    Private Sub btnSetShowPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetShowPosterLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                ShowPoster.FromFile(ofdImage.FileName)
                Me.pbShowPoster.Image = ShowPoster.Image
                Me.pbShowPoster.Tag = ShowPoster

                Me.lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowPoster.Image.Width, Me.pbShowPoster.Image.Height)
                Me.lblShowPosterSize.Visible = True
            End If
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnShowEFanartsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowEFanartsRefresh.Click
        Me.RefreshEFanarts()
    End Sub

    Private Sub btnShowEFanartsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowEFanartsRemove.Click
        Me.DeleteEFanarts()
        Me.RefreshEFanarts()
        Me.lblShowEFanartsSize.Text = ""
        Me.lblShowEFanartsSize.Visible = False
    End Sub

    Private Sub btnShowEFanartsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowEFanartsSetAsFanart.Click
        If Not String.IsNullOrEmpty(Me.EFanartsList.Item(Me.EFanartsIndex).Path) AndAlso Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(0, 1) = ":" Then
            ShowFanart.FromWeb(Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(1, Me.EFanartsList.Item(Me.EFanartsIndex).Path.Length - 1))
        Else
            ShowFanart.FromFile(Me.EFanartsList.Item(Me.EFanartsIndex).Path)
        End If
        If Not IsNothing(ShowFanart.Image) Then
            Me.pbShowFanart.Image = ShowFanart.Image
            Me.pbShowFanart.Tag = ShowFanart

            Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
            Me.lblShowFanartSize.Visible = True
        End If
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
            Logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub bwEFanarts_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwEFanarts.DoWork
        If Not Master.currShow.ClearShowEFanarts OrElse hasClearedEF Then LoadEFanarts()
    End Sub

    Private Sub bwEFanarts_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwEFanarts.RunWorkerCompleted
        Try
            If EFanartsList.Count > 0 Then
                For Each tEFanart As ExtraImages In EFanartsList
                    AddEFImage(tEFanart.Name, tEFanart.Index, tEFanart)
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Try
            If Not IsNothing(Me.pnlEFImage) Then
                For Each Pan In Me.pnlEFImage
                    CType(Pan.Tag, Images).Dispose()
                Next
            End If
            If Not IsNothing(Me.pbEFImage) Then
                For Each Pan In Me.pbEFImage
                    CType(Pan.Tag, Images).Dispose()
                    Pan.Image.Dispose()
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

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
            Logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub DeleteEFanarts()
        Try
            Dim iIndex As Integer = EFanartsIndex

            If iIndex >= 0 Then
                Dim tPath As String = Me.EFanartsList.Item(iIndex).Path
                If Me.EFanartsList.Item(iIndex).Path.Substring(0, 1) = ":" Then
                    Master.currShow.efList.RemoveAll(Function(Str) Str = tPath)
                    EFanartsList.Remove(EFanartsList.Item(iIndex))
                Else
                    efDeleteList.Add(Me.EFanartsList.Item(iIndex).Path)
                    EFanartsList.Remove(EFanartsList.Item(iIndex))
                End If
                pbShowEFanarts.Image = Nothing
                btnShowEFanartsSetAsFanart.Enabled = False
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgEditShow_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.bwEFanarts.IsBusy Then Me.bwEFanarts.CancelAsync()
        While Me.bwEFanarts.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub dlgEditShow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Master.eSettings.TVASBannerAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpASBanner)
        If Not Master.eSettings.TVASFanartAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpASFanart)
        If Not Master.eSettings.TVASLandscapeAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpASLandscape)
        If Not Master.eSettings.TVASPosterAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpASPoster)
        If Not Master.eSettings.TVShowBannerAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowBanner)
        If Not Master.eSettings.TVShowCharacterArtAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowCharacterArt)
        If Not Master.eSettings.TVShowClearArtAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowClearArt)
        If Not Master.eSettings.TVShowClearLogoAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowClearLogo)
        If Not Master.eSettings.TVShowEFanartsAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowEFanarts)
        If Not Master.eSettings.TVShowFanartAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowFanart)
        If Not Master.eSettings.TVShowLandscapeAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowLandscape)
        If Not Master.eSettings.TVShowPosterAnyEnabled Then Me.tcEditShow.TabPages.Remove(tpShowPoster)

        Me.pbASBanner.AllowDrop = True
        Me.pbASFanart.AllowDrop = True
        Me.pbASLandscape.AllowDrop = True
        Me.pbASPoster.AllowDrop = True
        Me.pbShowBanner.AllowDrop = True
        Me.pbShowCharacterArt.AllowDrop = True
        Me.pbShowClearArt.AllowDrop = True
        Me.pbShowClearLogo.AllowDrop = True
        Me.pbShowFanart.AllowDrop = True
        Me.pbShowLandscape.AllowDrop = True
        Me.pbShowPoster.AllowDrop = True

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

    Private Sub DoSelectEF(ByVal iIndex As Integer, tPoster As Images)
        Try
            Me.pbShowEFanarts.Image = tPoster.Image
            Me.pbShowEFanarts.Tag = tPoster
            Me.btnShowEFanartsSetAsFanart.Enabled = True
            Me.EFanartsIndex = iIndex
            Me.lblShowEFanartsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowEFanarts.Image.Width, Me.pbShowEFanarts.Image.Height)
            Me.lblShowEFanartsSize.Visible = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
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
            Logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub FillInfo()
        With Me
            .cbOrdering.SelectedIndex = Master.currShow.Ordering
            .cbEpisodeSorting.SelectedIndex = Master.currShow.EpisodeSorting

            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Title) Then .txtTitle.Text = Master.currShow.TVShow.Title
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Plot) Then .txtPlot.Text = Master.currShow.TVShow.Plot
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Premiered) Then .txtPremiered.Text = Master.currShow.TVShow.Premiered
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Runtime) Then .txtRuntime.Text = Master.currShow.TVShow.Runtime
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Status) Then .txtStatus.Text = Master.currShow.TVShow.Status
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Studio) Then .txtStudio.Text = Master.currShow.TVShow.Studio
            If Not String.IsNullOrEmpty(Master.currShow.TVShow.Votes) Then .txtVotes.Text = Master.currShow.TVShow.Votes

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
            .pbStar6.Tag = tRating
            .pbStar7.Tag = tRating
            .pbStar8.Tag = tRating
            .pbStar9.Tag = tRating
            .pbStar10.Tag = tRating
            If tRating > 0 Then .BuildStars(tRating)

            Me.SelectMPAA()

            If Master.eSettings.TVASBannerAnyEnabled Then
                .ASBanner.FromFile(Master.currShow.SeasonBannerPath)
                If Not IsNothing(.ASBanner.Image) Then
                    .pbASBanner.Image = .ASBanner.Image
                    .pbASBanner.Tag = ASBanner

                    .lblASBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbASBanner.Image.Width, .pbASBanner.Image.Height)
                    .lblASBannerSize.Visible = True
                End If
            End If

            If Master.eSettings.TVASFanartAnyEnabled Then
                .ASFanart.FromFile(Master.currShow.SeasonFanartPath)
                If Not IsNothing(.ASFanart.Image) Then
                    .pbASFanart.Image = .ASFanart.Image
                    .pbASFanart.Tag = ASFanart

                    .lblASFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbASFanart.Image.Width, .pbASFanart.Image.Height)
                    .lblASFanartSize.Visible = True
                End If
            End If

            If Master.eSettings.TVASLandscapeAnyEnabled Then
                .ASLandscape.FromFile(Master.currShow.SeasonLandscapePath)
                If Not IsNothing(.ASLandscape.Image) Then
                    .pbASLandscape.Image = .ASLandscape.Image
                    .pbASLandscape.Tag = ASLandscape

                    .lblASLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbASLandscape.Image.Width, .pbASLandscape.Image.Height)
                    .lblASLandscapeSize.Visible = True
                End If
            End If

            If Master.eSettings.TVASPosterAnyEnabled Then
                .ASPoster.FromFile(Master.currShow.SeasonPosterPath)
                If Not IsNothing(.ASPoster.Image) Then
                    .pbASPoster.Image = .ASPoster.Image
                    .pbASPoster.Tag = ASPoster

                    .lblASPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbASPoster.Image.Width, .pbASPoster.Image.Height)
                    .lblASPosterSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowBannerAnyEnabled Then
                ShowBanner.FromFile(Master.currShow.ShowBannerPath)
                If Not IsNothing(ShowBanner.Image) Then
                    .pbShowBanner.Image = ShowBanner.Image
                    .pbShowBanner.Tag = ShowBanner

                    .lblShowBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowBanner.Image.Width, .pbShowBanner.Image.Height)
                    .lblShowBannerSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                ShowCharacterArt.FromFile(Master.currShow.ShowCharacterArtPath)
                If Not IsNothing(ShowCharacterArt.Image) Then
                    .pbShowCharacterArt.Image = ShowCharacterArt.Image
                    .pbShowCharacterArt.Tag = ShowCharacterArt

                    .lblShowCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowCharacterArt.Image.Width, .pbShowCharacterArt.Image.Height)
                    .lblShowCharacterArtSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowClearArtAnyEnabled Then
                ShowClearArt.FromFile(Master.currShow.ShowClearArtPath)
                If Not IsNothing(ShowClearArt.Image) Then
                    .pbShowClearArt.Image = ShowClearArt.Image
                    .pbShowClearArt.Tag = ShowClearArt

                    .lblShowClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowClearArt.Image.Width, .pbShowClearArt.Image.Height)
                    .lblShowClearArtSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowClearLogoAnyEnabled Then
                ShowClearLogo.FromFile(Master.currShow.ShowClearLogoPath)
                If Not IsNothing(ShowClearLogo.Image) Then
                    .pbShowClearLogo.Image = ShowClearLogo.Image
                    .pbShowClearLogo.Tag = ShowClearLogo

                    .lblShowClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowClearLogo.Image.Width, .pbShowClearLogo.Image.Height)
                    .lblShowClearLogoSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowFanartAnyEnabled Then
                ShowFanart.FromFile(Master.currShow.ShowFanartPath)
                If Not IsNothing(ShowFanart.Image) Then
                    .pbShowFanart.Image = ShowFanart.Image
                    .pbShowFanart.Tag = ShowFanart

                    .lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowFanart.Image.Width, .pbShowFanart.Image.Height)
                    .lblShowFanartSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowLandscapeAnyEnabled Then
                ShowLandscape.FromFile(Master.currShow.ShowLandscapePath)
                If Not IsNothing(ShowLandscape.Image) Then
                    .pbShowLandscape.Image = ShowLandscape.Image
                    .pbShowLandscape.Tag = ShowLandscape

                    .lblShowLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowLandscape.Image.Width, .pbShowLandscape.Image.Height)
                    .lblShowLandscapeSize.Visible = True
                End If
            End If

            If Master.eSettings.TVShowPosterAnyEnabled Then
                ShowPoster.FromFile(Master.currShow.ShowPosterPath)
                If Not IsNothing(ShowPoster.Image) Then
                    .pbShowPoster.Image = ShowPoster.Image
                    .pbShowPoster.Tag = ShowPoster

                    .lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbShowPoster.Image.Width, .pbShowPoster.Image.Height)
                    .lblShowPosterSize.Visible = True
                End If
            End If

            .bwEFanarts.WorkerSupportsCancellation = True
            .bwEFanarts.RunWorkerAsync()

            'TODO: add ScraperCapabilities for tv shows (need splitted data and poster scraper for tv shows)

            'If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Banner) Then
            '    .btnSetASBannerScrape.Enabled = False
            '    .btnSetShowBannerScrape.Enabled = False
            'End If

            'If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart) Then
            '    .btnSetASFanartScrape.Enabled = False
            '    .btnSetShowFanartScrape.Enabled = False
            'End If

            'If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Landscape) Then
            '    .btnSetASLandscapeScrape.Enabled = False
            '    .btnSetShowLandscapeScrape.Enabled = False
            'End If

            'If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Poster) Then
            '    .btnSetASPosterScrape.Enabled = False
            '    .btnSetShowPosterScrape.Enabled = False
            'End If

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

    Private Sub LoadEFanarts()
        Dim EF_tPath As String = String.Empty
        Dim EF_lFI As New List(Of String)
        Dim EF_i As Integer = 0
        Dim EF_max As Integer = 30 'limited the number of images to avoid a memory error

        For Each a In FileUtils.GetFilenameList.TVShow(Master.currShow.ShowPath, Enums.ModType_TV.ShowEFanarts)
            If Directory.Exists(a) Then
                EF_lFI.AddRange(Directory.GetFiles(a))
            End If
        Next

        Try
            If EF_lFI.Count > 0 Then

                ' load local Extrafanarts
                If EF_lFI.Count > 0 Then
                    For Each fanart As String In EF_lFI
                        Dim EFImage As New Images
                        If Me.bwEFanarts.CancellationPending Then Return
                        If Not Me.efDeleteList.Contains(fanart) Then
                            EFImage.FromFile(fanart)
                            EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(fanart), .Index = EF_i, .Path = fanart})
                            EF_i += 1
                            If EF_i >= EF_max Then Exit For
                        End If
                    Next
                End If
            End If

            ' load scraped Extrafanarts
            If Not Master.currShow.efList Is Nothing Then
                If Not EF_i >= EF_max Then
                    For Each fanart As String In Master.currShow.efList
                        Dim EFImage As New Images
                        If Not String.IsNullOrEmpty(fanart) Then
                            EFImage.FromWeb(fanart.Substring(1, fanart.Length - 1))
                        End If
                        If Not IsNothing(EFImage.Image) Then
                            EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(fanart), .Index = EF_i, .Path = fanart})
                            EF_i += 1
                            If EF_i >= EF_max Then Exit For
                        End If
                    Next
                End If
            End If

            If EF_i >= EF_max AndAlso EFanartsWarning Then
                MsgBox(String.Format(Master.eLang.GetString(1119, "To prevent a memory overflow will not display more than {0} Extrafanarts."), EF_max), MsgBoxStyle.OkOnly, Master.eLang.GetString(356, "Warning"))
                EFanartsWarning = False 'show warning only one time
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        EF_lFI = Nothing
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
            Logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvActors.DoubleClick
        EditActor()
    End Sub

    Private Sub lvEFanart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then Me.DeleteEFanarts()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.SetInfo()

            Master.DB.SaveTVShowToDB(Master.currShow, False, False, True)

            If Master.eSettings.TVASAnyEnabled Then Master.DB.SaveTVSeasonToDB(Master.currShow, False)

            If Not IsNothing(Me.pnlEFImage) Then
                For Each Pan In Me.pnlEFImage
                    CType(Pan.Tag, Images).Dispose()
                Next
            End If
            If Not IsNothing(Me.pbEFImage) Then
                For Each Pan In Me.pbEFImage
                    CType(Pan.Tag, Images).Dispose()
                    Pan.Image.Dispose()
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbASBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbASBanner.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ASBanner = tImage
            Me.pbASBanner.Image = ASBanner.Image
            Me.pbASBanner.Tag = ASBanner
            Me.lblASBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASBanner.Image.Width, Me.pbASBanner.Image.Height)
            Me.lblASBannerSize.Visible = True
        End If
    End Sub

    Private Sub pbASBanner_DragEnter(sender As Object, e As DragEventArgs) Handles pbASBanner.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbASFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbASFanart.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ASFanart = tImage
            Me.pbASFanart.Image = ASFanart.Image
            Me.pbASFanart.Tag = ASFanart
            Me.lblASFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASFanart.Image.Width, Me.pbASFanart.Image.Height)
            Me.lblASFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbASFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbASFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbASLandscape_DragDrop(sender As Object, e As DragEventArgs) Handles pbASLandscape.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ASLandscape = tImage
            Me.pbASLandscape.Image = ASLandscape.Image
            Me.pbASLandscape.Tag = ASLandscape
            Me.lblASLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASLandscape.Image.Width, Me.pbASLandscape.Image.Height)
            Me.lblASLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub pbASLandscape_DragEnter(sender As Object, e As DragEventArgs) Handles pbASLandscape.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbASPoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbASPoster.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ASPoster = tImage
            Me.pbASPoster.Image = ASPoster.Image
            Me.pbASPoster.Tag = ASPoster
            Me.lblASPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbASPoster.Image.Width, Me.pbASPoster.Image.Height)
            Me.lblASPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbASPoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbASPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbEFImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, Images))
    End Sub

    Private Sub pbShowBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbShowBanner.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ShowBanner = tImage
            Me.pbShowBanner.Image = ShowBanner.Image
            Me.pbShowBanner.Tag = ShowBanner
            Me.lblShowBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowBanner.Image.Width, Me.pbShowBanner.Image.Height)
            Me.lblShowBannerSize.Visible = True
        End If
    End Sub

    Private Sub pbShowBanner_DragEnter(sender As Object, e As DragEventArgs) Handles pbShowBanner.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbShowCharacterArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbShowCharacterArt.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ShowCharacterArt = tImage
            Me.pbShowCharacterArt.Image = ShowCharacterArt.Image
            Me.pbShowCharacterArt.Tag = ShowCharacterArt
            Me.lblShowCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowCharacterArt.Image.Width, Me.pbShowCharacterArt.Image.Height)
            Me.lblShowCharacterArtSize.Visible = True
        End If
    End Sub

    Private Sub pbShowCharacterArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbShowCharacterArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbShowClearArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbShowClearArt.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ShowClearArt = tImage
            Me.pbShowClearArt.Image = ShowClearArt.Image
            Me.pbShowClearArt.Tag = ShowClearArt
            Me.lblShowClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowClearArt.Image.Width, Me.pbShowClearArt.Image.Height)
            Me.lblShowClearArtSize.Visible = True
        End If
    End Sub

    Private Sub pbShowClearArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbShowClearArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbShowClearLogo_DragDrop(sender As Object, e As DragEventArgs) Handles pbShowClearLogo.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ShowClearLogo = tImage
            Me.pbShowClearLogo.Image = ShowClearLogo.Image
            Me.pbShowClearLogo.Tag = ShowClearLogo
            Me.lblShowClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowClearLogo.Image.Width, Me.pbShowClearLogo.Image.Height)
            Me.lblShowClearLogoSize.Visible = True
        End If
    End Sub

    Private Sub pbShowClearLogo_DragEnter(sender As Object, e As DragEventArgs) Handles pbShowClearLogo.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbShowFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbShowFanart.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ShowFanart = tImage
            Me.pbShowFanart.Image = ShowFanart.Image
            Me.pbShowFanart.Tag = ShowFanart
            Me.lblShowFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowFanart.Image.Width, Me.pbShowFanart.Image.Height)
            Me.lblShowFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbShowFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbShowFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbShowLandscape_DragDrop(sender As Object, e As DragEventArgs) Handles pbShowLandscape.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ShowLandscape = tImage
            Me.pbShowLandscape.Image = ShowLandscape.Image
            Me.pbShowLandscape.Tag = ShowLandscape
            Me.lblShowLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowLandscape.Image.Width, Me.pbShowLandscape.Image.Height)
            Me.lblShowLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub pbShowLandscape_DragEnter(sender As Object, e As DragEventArgs) Handles pbShowLandscape.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbShowPoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbShowPoster.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            ShowPoster = tImage
            Me.pbShowPoster.Image = ShowPoster.Image
            Me.pbShowPoster.Tag = ShowPoster
            Me.lblShowPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbShowPoster.Image.Width, Me.pbShowPoster.Image.Height)
            Me.lblShowPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbShowPoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbShowPoster.DragEnter
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar1.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar1.Tag = 0.5
                Me.BuildStars(0.5)
            Else
                Me.pbStar1.Tag = 1
                Me.BuildStars(1)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar2.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar2.Tag = 1.5
                Me.BuildStars(1.5)
            Else
                Me.pbStar2.Tag = 2
                Me.BuildStars(2)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar3.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar3.Tag = 2.5
                Me.BuildStars(2.5)
            Else
                Me.pbStar3.Tag = 3
                Me.BuildStars(3)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar4.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar4.Tag = 3.5
                Me.BuildStars(3.5)
            Else
                Me.pbStar4.Tag = 4
                Me.BuildStars(4)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar5_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar5.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar5.Tag = 4.5
                Me.BuildStars(4.5)
            Else
                Me.pbStar5.Tag = 5
                Me.BuildStars(5)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar6.Click
        Me.tmpRating = Me.pbStar6.Tag.ToString
    End Sub

    Private Sub pbStar6_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar6.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar6.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar6.Tag = 5.5
                Me.BuildStars(5.5)
            Else
                Me.pbStar6.Tag = 6
                Me.BuildStars(6)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar7.Click
        Me.tmpRating = Me.pbStar7.Tag.ToString
    End Sub

    Private Sub pbStar7_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar7.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar7.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar7.Tag = 6.5
                Me.BuildStars(6.5)
            Else
                Me.pbStar7.Tag = 7
                Me.BuildStars(7)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar8.Click
        Me.tmpRating = Me.pbStar8.Tag.ToString
    End Sub

    Private Sub pbStar8_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar8.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar8_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar8.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar8.Tag = 7.5
                Me.BuildStars(7.5)
            Else
                Me.pbStar8.Tag = 8
                Me.BuildStars(8)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar9.Click
        Me.tmpRating = Me.pbStar9.Tag.ToString
    End Sub

    Private Sub pbStar9_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar9.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar9_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar9.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar9.Tag = 8.5
                Me.BuildStars(8.5)
            Else
                Me.pbStar9.Tag = 9
                Me.BuildStars(9)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar10.Click
        Me.tmpRating = Me.pbStar10.Tag.ToString
    End Sub

    Private Sub pbStar10_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar10.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar10_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar10.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar10.Tag = 9.5
                Me.BuildStars(9.5)
            Else
                Me.pbStar10.Tag = 10
                Me.BuildStars(10)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pnlEFImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, Images))
    End Sub

    Private Sub RefreshEFanarts()
        Try
            If Me.bwEFanarts.IsBusy Then Me.bwEFanarts.CancelAsync()
            While Me.bwEFanarts.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            Me.iEFTop = 1 ' set first image top position back to 1
            Me.EFanartsList.Clear()
            While Me.pnlShowEFanartsBG.Controls.Count > 0
                Me.pnlShowEFanartsBG.Controls(0).Dispose()
            End While

            Me.bwEFanarts.WorkerSupportsCancellation = True
            Me.bwEFanarts.RunWorkerAsync()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SaveEFanartsList()
        Try
            For Each a In FileUtils.GetFilenameList.TVShow(Master.currShow.ShowPath, Enums.ModType_TV.ShowEFanarts)
                If Not String.IsNullOrEmpty(a) Then
                    If Master.currShow.ClearShowEFanarts AndAlso Not hasClearedEF Then
                        FileUtils.Delete.DeleteDirectory(a)
                        hasClearedEF = True
                    Else
                        'first delete the ones from the delete list
                        For Each del As String In efDeleteList
                            File.Delete(Path.Combine(a, del))
                        Next

                        'now name the rest something arbitrary so we don't get any conflicts
                        For Each lItem As ExtraImages In EFanartsList
                            If Not lItem.Path.Substring(0, 1) = ":" Then
                                FileSystem.Rename(lItem.Path, Path.Combine(Directory.GetParent(lItem.Path).FullName, String.Concat("temp", lItem.Name)))
                            End If
                        Next

                        'now rename them properly
                        For Each lItem As ExtraImages In EFanartsList
                            Dim efPath As String = lItem.Image.SaveAsTVShowExtrafanart(Master.currShow, lItem.Name)
                            If lItem.Index = 0 Then
                                Master.currShow.ShowEFanartsPath = efPath
                            End If
                        Next

                        'now remove the temp images
                        Dim tList As New List(Of String)

                        If Directory.Exists(a) Then
                            tList.AddRange(Directory.GetFiles(a, "temp*.jpg"))
                            For Each tFile As String In tList
                                File.Delete(Path.Combine(a, tFile))
                            Next
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SelectMPAA()
        If Not String.IsNullOrEmpty(Master.currShow.TVShow.MPAA) Then
            Try
                If Not APIXML.RatingXML.movies.FindAll(Function(f) f.country = Master.eSettings.MovieScraperCertLang.ToLower).Count > 0 Then
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
                Logger.Error(New StackFrame().GetMethod().Name,ex)
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
                Master.currShow.EpisodeSorting = DirectCast(.cbEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)

                Master.currShow.TVShow.Title = .txtTitle.Text.Trim
                Master.currShow.TVShow.Plot = .txtPlot.Text.Trim
                Master.currShow.TVShow.Premiered = .txtPremiered.Text.Trim
                Master.currShow.TVShow.Runtime = .txtRuntime.Text.Trim
                Master.currShow.TVShow.Status = .txtStatus.Text.Trim
                Master.currShow.TVShow.Studio = .txtStudio.Text.Trim
                Master.currShow.TVShow.Votes = .txtVotes.Text.Trim

                If Not String.IsNullOrEmpty(.txtTitle.Text) Then
                    If Master.eSettings.TVDisplayStatus AndAlso Not String.IsNullOrEmpty(.txtStatus.Text.Trim) Then
                        Master.currShow.ListTitle = String.Format("{0} ({1})", StringUtils.FilterTokens_TV(.txtTitle.Text.Trim), .txtStatus.Text.Trim)
                    Else
                        Master.currShow.ListTitle = StringUtils.FilterTokens_TV(.txtTitle.Text.Trim)
                    End If
                End If

                If .lbMPAA.SelectedIndices.Count > 0 AndAlso Not .lbMPAA.SelectedIndex <= 0 Then
                    Master.currShow.TVShow.MPAA = .lbMPAA.SelectedItem.ToString
                Else
                    Master.currShow.TVShow.MPAA = String.Empty
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

                'AllSeasonBanner
                If Not IsNothing(.ASBanner.Image) Then
                    Master.currShow.SeasonBannerPath = .ASBanner.SaveAsTVASBanner(Master.currShow, "")
                Else
                    .ASBanner.DeleteTVASBanner(Master.currShow)
                    Master.currShow.SeasonBannerPath = String.Empty
                End If

                'AllSeason Fanart
                If Not IsNothing(.ASFanart.Image) Then
                    Master.currShow.SeasonFanartPath = .ASFanart.SaveAsTVASFanart(Master.currShow, "")
                Else
                    .ASFanart.DeleteTVASFanart(Master.currShow)
                    Master.currShow.SeasonFanartPath = String.Empty
                End If

                'AllSeason Landscape
                If Not IsNothing(.ASLandscape.Image) Then
                    Master.currShow.SeasonLandscapePath = .ASLandscape.SaveAsTVASLandscape(Master.currShow, "")
                Else
                    .ASLandscape.DeleteTVASLandscape(Master.currShow)
                    Master.currShow.SeasonLandscapePath = String.Empty
                End If

                'AllSeason Poster
                If Not IsNothing(.ASPoster.Image) Then
                    Master.currShow.SeasonPosterPath = .ASPoster.SaveAsTVASPoster(Master.currShow, "")
                Else
                    .ASPoster.DeleteTVASPoster(Master.currShow)
                    Master.currShow.SeasonPosterPath = String.Empty
                End If

                'Show Banner 
                If Not IsNothing(.ShowBanner.Image) Then
                    Master.currShow.ShowBannerPath = .ShowBanner.SaveAsTVShowBanner(Master.currShow, "")
                Else
                    .ShowBanner.DeleteTVShowBanner(Master.currShow)
                    Master.currShow.ShowBannerPath = String.Empty
                End If

                'Show CharacterArt 
                If Not IsNothing(.ShowCharacterArt.Image) Then
                    Master.currShow.ShowCharacterArtPath = .ShowCharacterArt.SaveAsTVShowCharacterArt(Master.currShow, "")
                Else
                    .ShowCharacterArt.DeleteTVShowCharacterArt(Master.currShow)
                    Master.currShow.ShowCharacterArtPath = String.Empty
                End If

                'Show ClearArt 
                If Not IsNothing(.ShowClearArt.Image) Then
                    Master.currShow.ShowClearArtPath = .ShowClearArt.SaveAsTVShowClearArt(Master.currShow, "")
                Else
                    .ShowClearArt.DeleteTVShowClearArt(Master.currShow)
                    Master.currShow.ShowClearArtPath = String.Empty
                End If

                'Show ClearLogo 
                If Not IsNothing(.ShowClearLogo.Image) Then
                    Master.currShow.ShowClearLogoPath = .ShowClearLogo.SaveAsTVShowClearLogo(Master.currShow, "")
                Else
                    .ShowClearLogo.DeleteTVShowClearLogo(Master.currShow)
                    Master.currShow.ShowClearLogoPath = String.Empty
                End If

                'Show Fanart
                If Not IsNothing(.ShowFanart.Image) Then
                    Master.currShow.ShowFanartPath = .ShowFanart.SaveAsTVShowFanart(Master.currShow, "")
                Else
                    .ShowFanart.DeleteTVShowFanart(Master.currShow)
                    Master.currShow.ShowFanartPath = String.Empty
                End If

                'Show Landscape
                If Not IsNothing(.ShowLandscape.Image) Then
                    Master.currShow.ShowLandscapePath = .ShowLandscape.SaveAsTVShowLandscape(Master.currShow, "")
                Else
                    .ShowLandscape.DeleteTVShowLandscape(Master.currShow)
                    Master.currShow.ShowLandscapePath = String.Empty
                End If

                'Show Poster
                If Not IsNothing(.ShowPoster.Image) Then
                    Master.currShow.ShowPosterPath = .ShowPoster.SaveAsTVShowPoster(Master.currShow, "")
                Else
                    .ShowPoster.DeleteTVShowPosters(Master.currShow)
                    Master.currShow.ShowPosterPath = String.Empty
                End If

                'Show Extrafanarts
                .SaveEFanartsList()

            End With
        Catch ex As Exception
            Logger.Error(New StackFrame().GetMethod().Name,ex)
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
        Me.btnRemoveShowCharacterArt.Text = Master.eLang.GetString(1145, "Remove CharacterArt")
        Me.btnRemoveShowClearArt.Text = Master.eLang.GetString(1087, "Remove ClearArt")
        Me.btnRemoveShowClearLogo.Text = Master.eLang.GetString(1091, "Remove ClearLogo")
        Me.btnRemoveASFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnRemoveASLandscape.Text = Master.eLang.GetString(1034, "Remove Landscape")
        Me.btnRemoveASPoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnRemoveShowBanner.Text = Me.btnRemoveASBanner.Text
        Me.btnRemoveShowFanart.Text = Me.btnRemoveASFanart.Text
        Me.btnRemoveShowLandscape.Text = Me.btnRemoveASLandscape.Text
        Me.btnRemoveShowPoster.Text = Me.btnRemoveASPoster.Text
        Me.btnSetASBannerDL.Text = Master.eLang.GetString(1023, "Change Banner (Download)")
        Me.btnSetASBannerLocal.Text = Master.eLang.GetString(1021, "Change Banner (Local)")
        Me.btnSetASBannerScrape.Text = Master.eLang.GetString(1022, "Change Banner (Scrape)")
        Me.btnSetShowCharacterArtDL.Text = Master.eLang.GetString(1144, "Change CharacterArt (Download)")
        Me.btnSetShowCharacterArtLocal.Text = Master.eLang.GetString(1142, "Change CharacterArt (Local)")
        Me.btnSetShowCharacterArtScrape.Text = Master.eLang.GetString(1143, "Change CharacterArt (Scrape)")
        Me.btnSetShowClearArtDL.Text = Master.eLang.GetString(1086, "Change ClearArt (Download)")
        Me.btnSetShowClearArtLocal.Text = Master.eLang.GetString(1084, "Change ClearArt (Local)")
        Me.btnSetShowClearArtScrape.Text = Master.eLang.GetString(1085, "Change ClearArt (Scrape)")
        Me.btnSetShowClearLogoDL.Text = Master.eLang.GetString(1090, "Change ClearLogo (Download)")
        Me.btnSetShowClearLogoLocal.Text = Master.eLang.GetString(1088, "Change ClearLogo (Local)")
        Me.btnSetShowClearLogoScrape.Text = Master.eLang.GetString(1089, "Change ClearLogo (Scrape)")
        Me.btnSetASFanartDL.Text = Master.eLang.GetString(266, "Change Fanart (Download)")
        Me.btnSetASFanartLocal.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.btnSetASFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetASLandscapeDL.Text = Master.eLang.GetString(1033, "Change Landscape (Download)")
        Me.btnSetASLandscapeLocal.Text = Master.eLang.GetString(1031, "Change Landscape (Local)")
        Me.btnSetASLandscapeScrape.Text = Master.eLang.GetString(1032, "Change Landscape (Scrape)")
        Me.btnSetASPosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetASPosterLocal.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.btnSetASPosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.btnSetShowBannerDL.Text = Me.btnSetASBannerDL.Text
        Me.btnSetShowBannerLocal.Text = Me.btnSetASBannerLocal.Text
        Me.btnSetShowBannerScrape.Text = Me.btnSetASBannerScrape.Text
        Me.btnSetShowFanartDL.Text = Me.btnSetASFanartDL.Text
        Me.btnSetShowFanartLocal.Text = Me.btnSetASFanartLocal.Text
        Me.btnSetShowFanartScrape.Text = Me.btnSetASFanartScrape.Text
        Me.btnSetShowLandscapeDL.Text = Me.btnSetASLandscapeDL.Text
        Me.btnSetShowLandscapeLocal.Text = Me.btnSetASLandscapeLocal.Text
        Me.btnSetShowLandscapeScrape.Text = Me.btnSetASLandscapeScrape.Text
        Me.btnSetShowPosterDL.Text = Me.btnSetASPosterDL.Text
        Me.btnSetShowPosterLocal.Text = Me.btnSetASPosterLocal.Text
        Me.btnSetShowPosterScrape.Text = Me.btnSetASPosterScrape.Text
        Me.btnShowEFanartsSetAsFanart.Text = Master.eLang.GetString(255, "Set As Fanart")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colRole.Text = Master.eLang.GetString(233, "Role")
        Me.colThumb.Text = Master.eLang.GetString(234, "Thumb")
        Me.lblActors.Text = Master.eLang.GetString(231, "Actors:")
        Me.lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblEpisodeSorting.Text = String.Concat(Master.eLang.GetString(364, "Show Episodes by"), ":")
        Me.lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        Me.lblOrdering.Text = Master.eLang.GetString(739, "Episode Ordering:")
        Me.lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        Me.lblPremiered.Text = Master.eLang.GetString(665, "Premiered:")
        Me.lblRating.Text = Master.eLang.GetString(245, "Rating:")
        Me.lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        Me.lblStatus.Text = Master.eLang.GetString(1048, "Status:")
        Me.lblStudio.Text = Master.eLang.GetString(226, "Studio:")
        Me.lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Me.lblTopDetails.Text = Master.eLang.GetString(664, "Edit the details for the selected show.")
        Me.lblTopTitle.Text = Master.eLang.GetString(663, "Edit Show")
        Me.lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        Me.tpASBanner.Text = Master.eLang.GetString(1014, "All Seasons Banner")
        Me.tpASFanart.Text = Master.eLang.GetString(1015, "All Seasons Fanart")
        Me.tpASLandscape.Text = Master.eLang.GetString(1016, "All Seasons Landscape")
        Me.tpASPoster.Text = Master.eLang.GetString(735, "All Season Poster")
        Me.tpShowBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpShowCharacterArt.Text = Master.eLang.GetString(1140, "CharacterArt")
        Me.tpShowClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.tpShowClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.tpShowDetails.Text = Master.eLang.GetString(26, "Details")
        Me.tpShowEFanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        Me.tpShowFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpShowLandscape.Text = Master.eLang.GetString(1035, "Landscape")
        Me.tpShowPoster.Text = Master.eLang.GetString(148, "Poster")

        Me.cbOrdering.Items.Clear()
        Me.cbOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(1067, "DVD"), Master.eLang.GetString(839, "Absolute"), Master.eLang.GetString(1332, "Day Of Year")})

        Me.cbEpisodeSorting.Items.Clear()
        Me.cbEpisodeSorting.Items.AddRange(New String() {Master.eLang.GetString(755, "Episode #"), Master.eLang.GetString(728, "Aired")})
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Friend Class ExtraImages

#Region "Fields"

        Private _image As New Images
        Private _index As Integer
        Private _name As String
        Private _path As String

#End Region 'Fields

#Region "Constructors"

        Friend Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Friend Property Image() As Images
            Get
                Return _image
            End Get
            Set(ByVal value As Images)
                _image = value
            End Set
        End Property

        Friend Property Index() As Integer
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        Friend Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Friend Property Path() As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _image = Nothing
            _name = String.Empty
            _index = Nothing
            _path = String.Empty
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class