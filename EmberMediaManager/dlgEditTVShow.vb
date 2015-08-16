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

Public Class dlgEditTVShow

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwEFanarts As New System.ComponentModel.BackgroundWorker

    Private tmpDBElement As New Database.DBElement

    Private ActorThumbsHasChanged As Boolean = False
    Private lvwActorSorter As ListViewColumnSorter
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
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBTVShow As Database.DBElement) As DialogResult
        Me.tmpDBElement = DBTVShow
        Return MyBase.ShowDialog()
    End Function

    Private Sub btnActorDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorDown.Click
        If Me.lvActors.SelectedItems.Count > 0 AndAlso Me.lvActors.SelectedItems(0) IsNot Nothing AndAlso Me.lvActors.SelectedIndices(0) < (Me.lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = Me.lvActors.SelectedIndices(0)
            Me.lvActors.Items.Insert(iIndex + 2, DirectCast(Me.lvActors.SelectedItems(0).Clone, ListViewItem))
            Me.lvActors.Items.RemoveAt(iIndex)
            Me.lvActors.Items(iIndex + 1).Selected = True
            Me.lvActors.Select()
        End If
    End Sub

    Private Sub btnActorUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorUp.Click
        Try
            If Me.lvActors.SelectedItems.Count > 0 AndAlso Me.lvActors.SelectedItems(0) IsNot Nothing AndAlso Me.lvActors.SelectedIndices(0) > 0 Then
                Dim iIndex As Integer = Me.lvActors.SelectedIndices(0)
                Me.lvActors.Items.Insert(iIndex - 1, DirectCast(Me.lvActors.SelectedItems(0).Clone, ListViewItem))
                Me.lvActors.Items.RemoveAt(iIndex + 1)
                Me.lvActors.Items(iIndex - 1).Selected = True
                Me.lvActors.Select()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnAddActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddActor.Click
        Try
            Dim eActor As New MediaContainers.Person
            Using dAddEditActor As New dlgAddEditActor
                eActor = dAddEditActor.ShowDialog(True)
            End Using
            If eActor IsNot Nothing Then
                Dim lvItem As ListViewItem = Me.lvActors.Items.Add(eActor.ID.ToString)
                lvItem.SubItems.Add(eActor.Name)
                lvItem.SubItems.Add(eActor.Role)
                lvItem.SubItems.Add(eActor.ThumbURL)
                ActorThumbsHasChanged = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            Me.pnlEFanartsBG.Controls.Add(Me.pnlEFImage(iIndex))
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
            If dlgManualEdit.ShowDialog(Me.tmpDBElement.NfoPath) = Windows.Forms.DialogResult.OK Then
                Me.tmpDBElement.TVShow = NFO.LoadTVShowFromNFO(Me.tmpDBElement.NfoPath)
                Me.FillInfo()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnRemoveBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveBanner.Click
        Me.pbBanner.Image = Nothing
        Me.pbBanner.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image

        Me.lblBannerSize.Text = String.Empty
        Me.lblBannerSize.Visible = False
    End Sub

    Private Sub btnRemoveCharacterArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveCharacterArt.Click
        Me.pbCharacterArt.Image = Nothing
        Me.pbCharacterArt.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.CharacterArt = New MediaContainers.Image

        Me.lblCharacterArtSize.Text = String.Empty
        Me.lblCharacterArtSize.Visible = False
    End Sub

    Private Sub btnRemoveClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveClearArt.Click
        Me.pbClearArt.Image = Nothing
        Me.pbClearArt.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.ClearArt = New MediaContainers.Image

        Me.lblClearArtSize.Text = String.Empty
        Me.lblClearArtSize.Visible = False
    End Sub

    Private Sub btnRemoveClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveClearLogo.Click
        Me.pbClearLogo.Image = Nothing
        Me.pbClearLogo.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.ClearLogo = New MediaContainers.Image

        Me.lblClearLogoSize.Text = String.Empty
        Me.lblClearLogoSize.Visible = False
    End Sub

    Private Sub btnRemoveFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFanart.Click
        Me.pbFanart.Image = Nothing
        Me.pbFanart.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Fanart = New MediaContainers.Image

        Me.lblFanartSize.Text = String.Empty
        Me.lblFanartSize.Visible = False
    End Sub

    Private Sub btnRemoveLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLandscape.Click
        Me.pbLandscape.Image = Nothing
        Me.pbLandscape.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Landscape = New MediaContainers.Image

        Me.lblLandscapeSize.Text = String.Empty
        Me.lblLandscapeSize.Visible = False
    End Sub

    Private Sub btnRemovePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePoster.Click
        Me.pbPoster.Image = Nothing
        Me.pbPoster.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Poster = New MediaContainers.Image

        Me.lblPosterSize.Text = String.Empty
        Me.lblPosterSize.Visible = False
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Me.DeleteActors()
    End Sub

    Private Sub btnSetBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainBanners.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Banner
                    Me.tmpDBElement.ImagesContainer.Banner = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbBanner.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                        Me.lblBannerSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Me.tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.FromFile(ofdImage.FileName)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Banner = tImage
                    Me.pbBanner.Image = tImage.ImageOriginal.Image
                    Me.pbBanner.Tag = tImage

                    Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                    Me.lblBannerSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Banner = tImage
                        Me.pbBanner.Image = tImage.ImageOriginal.Image
                        Me.pbBanner.Tag = tImage

                        Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                        Me.lblBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetCharacterArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetCharacterArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainCharacterArt, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainCharacterArts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.CharacterArt
                    Me.tmpDBElement.ImagesContainer.CharacterArt = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbCharacterArt.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbCharacterArt.Image.Width, Me.pbCharacterArt.Image.Height)
                        Me.lblCharacterArtSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1343, "No CharacterArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetCharacterArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetCharacterArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Me.tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.FromFile(ofdImage.FileName)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.CharacterArt = tImage
                    Me.pbCharacterArt.Image = tImage.ImageOriginal.Image
                    Me.pbCharacterArt.Tag = tImage

                    Me.lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbCharacterArt.Image.Width, Me.pbCharacterArt.Image.Height)
                    Me.lblCharacterArtSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetCharacterArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetCharacterArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.CharacterArt = tImage
                        Me.pbCharacterArt.Image = tImage.ImageOriginal.Image
                        Me.pbCharacterArt.Tag = tImage

                        Me.lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbCharacterArt.Image.Width, Me.pbCharacterArt.Image.Height)
                        Me.lblCharacterArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Fanart = tImage
                        Me.pbFanart.Image = tImage.ImageOriginal.Image
                        Me.pbFanart.Tag = tImage

                        Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                        Me.lblFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainClearArts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.ClearArt
                    Me.tmpDBElement.ImagesContainer.ClearArt = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbClearArt.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
                        Me.lblClearArtSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetClearArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Me.tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.FromFile(ofdImage.FileName)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.ClearArt = tImage
                    Me.pbClearArt.Image = tImage.ImageOriginal.Image
                    Me.pbClearArt.Tag = tImage

                    Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
                    Me.lblClearArtSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.ClearArt = tImage
                        Me.pbClearArt.Image = tImage.ImageOriginal.Image
                        Me.pbClearArt.Tag = tImage

                        Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
                        Me.lblClearArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearLogoScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainClearLogos.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.ClearLogo
                    Me.tmpDBElement.ImagesContainer.ClearLogo = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbClearLogo.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
                        Me.lblClearLogoSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetClearLogoLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Me.tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.FromFile(ofdImage.FileName)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.ClearLogo = tImage
                    Me.pbClearLogo.Image = tImage.ImageOriginal.Image
                    Me.pbClearLogo.Tag = tImage

                    Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
                    Me.lblClearLogoSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearLogoDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.ClearLogo = tImage
                        Me.pbClearLogo.Image = tImage.ImageOriginal.Image
                        Me.pbClearLogo.Tag = tImage

                        Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
                        Me.lblClearLogoSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Fanart
                    Me.tmpDBElement.ImagesContainer.Fanart = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbFanart.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                        Me.lblFanartSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Me.tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.FromFile(ofdImage.FileName)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Fanart = tImage
                    Me.pbFanart.Image = tImage.ImageOriginal.Image
                    Me.pbFanart.Tag = tImage

                    Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                    Me.lblFanartSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainLandscapes.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Landscape
                    Me.tmpDBElement.ImagesContainer.Landscape = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbLandscape.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                        Me.lblLandscapeSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(972, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Me.tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.FromFile(ofdImage.FileName)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Landscape = tImage
                    Me.pbLandscape.Image = tImage.ImageOriginal.Image
                    Me.pbLandscape.Tag = tImage

                    Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                    Me.lblLandscapeSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Landscape = tImage
                        Me.pbLandscape.Image = tImage.ImageOriginal.Image
                        Me.pbLandscape.Tag = tImage

                        Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                        Me.lblLandscapeSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Poster = tImage
                        Me.pbPoster.Image = tImage.ImageOriginal.Image
                        Me.pbPoster.Tag = tImage

                        Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                        Me.lblPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainPosters.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Poster
                    Me.tmpDBElement.ImagesContainer.Poster = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbPoster.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                        Me.lblPosterSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Me.tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.FromFile(ofdImage.FileName)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Poster = tImage
                    Me.pbPoster.Image = tImage.ImageOriginal.Image
                    Me.pbPoster.Tag = tImage

                    Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                    Me.lblPosterSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnEFanartsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEFanartsRefresh.Click
        Me.RefreshEFanarts()
    End Sub

    Private Sub btnEFanartsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEFanartsRemove.Click
        'Me.DeleteEFanarts()
        Me.RefreshEFanarts()
        Me.lblEFanartsSize.Text = ""
        Me.lblEFanartsSize.Visible = False
    End Sub

    Private Sub btnEFanartsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEFanartsSetAsFanart.Click
        If Not String.IsNullOrEmpty(Me.EFanartsList.Item(Me.EFanartsIndex).Path) AndAlso Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(0, 1) = ":" Then
            Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.FromWeb(Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(1, Me.EFanartsList.Item(Me.EFanartsIndex).Path.Length - 1))
        Else
            Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.FromFile(Me.EFanartsList.Item(Me.EFanartsIndex).Path)
        End If
        If Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
            Me.pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            Me.pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwEFanarts_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwEFanarts.DoWork
        If hasClearedEF Then LoadEFanarts()
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
            If Me.pnlEFImage IsNot Nothing Then
                For Each Pan In Me.pnlEFImage
                    CType(Pan.Tag, Images).Dispose()
                Next
            End If
            If Me.pbEFImage IsNot Nothing Then
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

    Private Sub CleanUp()
        Try
            If Me.pnlEFImage IsNot Nothing Then
                For Each Pan In Me.pnlEFImage
                    CType(Pan.Tag, Images).Dispose()
                Next
            End If
            If Me.pbEFImage IsNot Nothing Then
                For Each Pan In Me.pbEFImage
                    CType(Pan.Tag, Images).Dispose()
                    Pan.Image.Dispose()
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DeleteActors()
        Try
            If Me.lvActors.Items.Count > 0 Then
                While Me.lvActors.SelectedItems.Count > 0
                    Me.lvActors.Items.Remove(Me.lvActors.SelectedItems(0))
                End While
                ActorThumbsHasChanged = True
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
        If Me.tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(Me.tmpDBElement, True) Then
            If Not Master.eSettings.TVShowBannerAnyEnabled Then Me.tcEdit.TabPages.Remove(tpBanner)
            If Not Master.eSettings.TVShowCharacterArtAnyEnabled Then Me.tcEdit.TabPages.Remove(tpCharacterArt)
            If Not Master.eSettings.TVShowClearArtAnyEnabled Then Me.tcEdit.TabPages.Remove(tpClearArt)
            If Not Master.eSettings.TVShowClearLogoAnyEnabled Then Me.tcEdit.TabPages.Remove(tpClearLogo)
            If Not Master.eSettings.TVShowEFanartsAnyEnabled Then Me.tcEdit.TabPages.Remove(tpEFanarts)
            If Not Master.eSettings.TVShowFanartAnyEnabled Then Me.tcEdit.TabPages.Remove(tpFanart)
            If Not Master.eSettings.TVShowLandscapeAnyEnabled Then Me.tcEdit.TabPages.Remove(tpLandscape)
            If Not Master.eSettings.TVShowPosterAnyEnabled Then Me.tcEdit.TabPages.Remove(tpPoster)

            Me.pbBanner.AllowDrop = True
            Me.pbCharacterArt.AllowDrop = True
            Me.pbClearArt.AllowDrop = True
            Me.pbClearLogo.AllowDrop = True
            Me.pbFanart.AllowDrop = True
            Me.pbLandscape.AllowDrop = True
            Me.pbPoster.AllowDrop = True

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
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub DoSelectEF(ByVal iIndex As Integer, tPoster As MediaContainers.Image)
        Try
            Me.pbEFanarts.Image = tPoster.ImageOriginal.Image
            Me.pbEFanarts.Tag = tPoster
            Me.btnEFanartsSetAsFanart.Enabled = True
            Me.EFanartsIndex = iIndex
            Me.lblEFanartsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEFanarts.Image.Width, Me.pbEFanarts.Image.Height)
            Me.lblEFanartsSize.Visible = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub EditActor()
        Try
            If Me.lvActors.SelectedItems.Count > 0 Then
                Dim lvwItem As ListViewItem = Me.lvActors.SelectedItems(0)
                Dim eActor As New MediaContainers.Person With {.ID = CInt(lvwItem.Text), .Name = lvwItem.SubItems(1).Text, .Role = lvwItem.SubItems(2).Text, .ThumbURL = lvwItem.SubItems(3).Text}
                Using dAddEditActor As New dlgAddEditActor
                    eActor = dAddEditActor.ShowDialog(False, eActor)
                End Using
                If eActor IsNot Nothing Then
                    lvwItem.Text = eActor.ID.ToString
                    lvwItem.SubItems(1).Text = eActor.Name
                    lvwItem.SubItems(2).Text = eActor.Role
                    lvwItem.SubItems(3).Text = eActor.ThumbURL
                    lvwItem.Selected = True
                    lvwItem.EnsureVisible()
                    ActorThumbsHasChanged = True
                End If
                eActor = Nothing
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub FillInfo()
        With Me
            .cbOrdering.SelectedIndex = Me.tmpDBElement.Ordering
            .cbEpisodeSorting.SelectedIndex = Me.tmpDBElement.EpisodeSorting

            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Title) Then .txtTitle.Text = Me.tmpDBElement.TVShow.Title
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Plot) Then .txtPlot.Text = Me.tmpDBElement.TVShow.Plot
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Premiered) Then .txtPremiered.Text = Me.tmpDBElement.TVShow.Premiered
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Runtime) Then .txtRuntime.Text = Me.tmpDBElement.TVShow.Runtime
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.SortTitle) Then .txtSortTitle.Text = Me.tmpDBElement.TVShow.SortTitle
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Status) Then .txtStatus.Text = Me.tmpDBElement.TVShow.Status
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Studio) Then .txtStudio.Text = Me.tmpDBElement.TVShow.Studio
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Votes) Then .txtVotes.Text = Me.tmpDBElement.TVShow.Votes

            For i As Integer = 0 To .clbGenre.Items.Count - 1
                .clbGenre.SetItemChecked(i, False)
            Next
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.Genre) Then
                Dim genreArray() As String
                genreArray = Me.tmpDBElement.TVShow.Genre.Split("/"c)
                For g As Integer = 0 To genreArray.Count - 1
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
            For Each imdbAct As MediaContainers.Person In Me.tmpDBElement.TVShow.Actors
                lvItem = .lvActors.Items.Add(imdbAct.ID.ToString)
                lvItem.SubItems.Add(imdbAct.Name)
                lvItem.SubItems.Add(imdbAct.Role)
                lvItem.SubItems.Add(imdbAct.ThumbURL)
            Next

            Dim tRating As Single = NumUtils.ConvertToSingle(Me.tmpDBElement.TVShow.Rating)
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



            'Images and TabPages

            If Master.eSettings.TVShowBannerAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner) Then
                    .btnSetBannerScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image IsNot Nothing Then
                    .pbBanner.Image = Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
                    .pbBanner.Tag = Me.tmpDBElement.ImagesContainer.Banner

                    .lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbBanner.Image.Width, .pbBanner.Image.Height)
                    .lblBannerSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpBanner)
            End If

            If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt) Then
                    .btnSetCharacterArtScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.CharacterArt.ImageOriginal.Image IsNot Nothing Then
                    .pbCharacterArt.Image = Me.tmpDBElement.ImagesContainer.CharacterArt.ImageOriginal.Image
                    .pbCharacterArt.Tag = Me.tmpDBElement.ImagesContainer.CharacterArt

                    .lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbCharacterArt.Image.Width, .pbCharacterArt.Image.Height)
                    .lblCharacterArtSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpCharacterArt)
            End If

            If Master.eSettings.TVShowClearArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt) Then
                    .btnSetClearArtScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image IsNot Nothing Then
                    .pbClearArt.Image = Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
                    .pbClearArt.Tag = Me.tmpDBElement.ImagesContainer.ClearArt

                    .lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbClearArt.Image.Width, .pbClearArt.Image.Height)
                    .lblClearArtSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpClearArt)
            End If

            If Master.eSettings.TVShowClearLogoAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo) Then
                    .btnSetClearLogoScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image IsNot Nothing Then
                    .pbClearLogo.Image = Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
                    .pbClearLogo.Tag = Me.tmpDBElement.ImagesContainer.ClearLogo

                    .lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbClearLogo.Image.Width, .pbClearLogo.Image.Height)
                    .lblClearLogoSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpClearLogo)
            End If

            If Master.eSettings.TVShowFanartAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart) Then
                    .btnSetFanartScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                    .pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                    .pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

                    .lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbFanart.Image.Width, .pbFanart.Image.Height)
                    .lblFanartSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpFanart)
            End If

            If Master.eSettings.TVShowLandscapeAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape) Then
                    .btnSetLandscapeScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image IsNot Nothing Then
                    .pbLandscape.Image = Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
                    .pbLandscape.Tag = Me.tmpDBElement.ImagesContainer.Landscape

                    .lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbLandscape.Image.Width, .pbLandscape.Image.Height)
                    .lblLandscapeSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpLandscape)
            End If

            If Master.eSettings.TVShowPosterAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster) Then
                    .btnSetPosterScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing Then
                    .pbPoster.Image = Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                    .pbPoster.Tag = Me.tmpDBElement.ImagesContainer.Poster

                    .lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbPoster.Image.Width, .pbPoster.Image.Height)
                    .lblPosterSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpPoster)
            End If

            .bwEFanarts.WorkerSupportsCancellation = True
            .bwEFanarts.RunWorkerAsync()
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

    Private Sub lbMPAA_DoubleClick(sender As Object, e As EventArgs) Handles lbMPAA.DoubleClick
        If Me.lbMPAA.SelectedItems.Count = 1 Then
            If Me.lbMPAA.SelectedIndex = 0 Then
                Me.txtMPAA.Text = String.Empty
            Else
                Me.txtMPAA.Text = Me.lbMPAA.SelectedItem.ToString
            End If
        End If
    End Sub

    Private Sub LoadEFanarts()
        Dim EF_tPath As String = String.Empty
        Dim EF_lFI As New List(Of String)
        Dim EF_i As Integer = 0
        Dim EF_max As Integer = 30 'limited the number of images to avoid a memory error

        For Each a In FileUtils.GetFilenameList.TVShow(Me.tmpDBElement.ShowPath, Enums.ModifierType.MainExtrafanarts)
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
            If Me.tmpDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
                If Not EF_i >= EF_max Then
                    For Each fanart As MediaContainers.Image In Me.tmpDBElement.ImagesContainer.Extrafanarts
                        'Dim EFImage As New Images
                        'If Not String.IsNullOrEmpty(fanart) Then
                        '    EFImage.FromWeb(fanart.Substring(1, fanart.Length - 1))
                        'End If
                        'If EFImage.Image IsNot Nothing Then
                        '    EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(fanart), .Index = EF_i, .Path = fanart})
                        '    EF_i += 1
                        '    If EF_i >= EF_max Then Exit For
                        'End If
                    Next
                End If
            End If

            If EF_i >= EF_max AndAlso EFanartsWarning Then
                MessageBox.Show(String.Format(Master.eLang.GetString(1119, "To prevent a memory overflow will not display more than {0} Extrafanarts."), EF_max), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        If Not String.IsNullOrEmpty(Master.eSettings.TVScraperShowMPAANotRated) Then Me.lbMPAA.Items.Add(Master.eSettings.TVScraperShowMPAANotRated)
        Me.lbMPAA.Items.AddRange(APIXML.GetRatingList_TV)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvActors.DoubleClick
        EditActor()
    End Sub

    Private Sub lvEFanart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'If e.KeyCode = Keys.Delete Then Me.DeleteEFanarts()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.SetInfo()
        Me.CleanUp()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbEFImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pbBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbBanner.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Banner = tImage
            Me.pbBanner.Image = tImage.ImageOriginal.Image
            Me.pbBanner.Tag = tImage
            Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
            Me.lblBannerSize.Visible = True
        End If
    End Sub

    Private Sub pbBanner_DragEnter(sender As Object, e As DragEventArgs) Handles pbBanner.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbCharacterArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbCharacterArt.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.CharacterArt = tImage
            Me.pbCharacterArt.Image = tImage.ImageOriginal.Image
            Me.pbCharacterArt.Tag = tImage
            Me.lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbCharacterArt.Image.Width, Me.pbCharacterArt.Image.Height)
            Me.lblCharacterArtSize.Visible = True
        End If
    End Sub

    Private Sub pbCharacterArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbCharacterArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbClearArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbClearArt.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.ClearArt = tImage
            Me.pbClearArt.Image = tImage.ImageOriginal.Image
            Me.pbClearArt.Tag = tImage
            Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
            Me.lblClearArtSize.Visible = True
        End If
    End Sub

    Private Sub pbClearArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbClearArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbClearLogo_DragDrop(sender As Object, e As DragEventArgs) Handles pbClearLogo.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.ClearLogo = tImage
            Me.pbClearLogo.Image = tImage.ImageOriginal.Image
            Me.pbClearLogo.Tag = tImage
            Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
            Me.lblClearLogoSize.Visible = True
        End If
    End Sub

    Private Sub pbClearLogo_DragEnter(sender As Object, e As DragEventArgs) Handles pbClearLogo.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbFanart.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Fanart = tImage
            Me.pbFanart.Image = tImage.ImageOriginal.Image
            Me.pbFanart.Tag = tImage
            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbLandscape_DragDrop(sender As Object, e As DragEventArgs) Handles pbLandscape.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Landscape = tImage
            Me.pbLandscape.Image = tImage.ImageOriginal.Image
            Me.pbLandscape.Tag = tImage
            Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
            Me.lblLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub pbLandscape_DragEnter(sender As Object, e As DragEventArgs) Handles pbLandscape.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbPoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbPoster.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Poster = tImage
            Me.pbPoster.Image = tImage.ImageOriginal.Image
            Me.pbPoster.Tag = tImage
            Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
            Me.lblPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbPoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbPoster.DragEnter
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
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
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
            While Me.pnlEFanartsBG.Controls.Count > 0
                Me.pnlEFanartsBG.Controls(0).Dispose()
            End While

            Me.bwEFanarts.WorkerSupportsCancellation = True
            Me.bwEFanarts.RunWorkerAsync()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SaveEFanartsList()
        Try
            For Each a In FileUtils.GetFilenameList.TVShow(Me.tmpDBElement.ShowPath, Enums.ModifierType.MainExtrafanarts)
                If Not String.IsNullOrEmpty(a) Then
                    If Not hasClearedEF Then
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
                                File.Move(lItem.Path, Path.Combine(Directory.GetParent(lItem.Path).FullName, String.Concat("temp", lItem.Name)))
                            End If
                        Next

                        'now rename them properly
                        For Each lItem As ExtraImages In EFanartsList
                            Dim efPath As String = lItem.Image.SaveAsTVShowExtrafanart(Me.tmpDBElement, lItem.Name)
                            If lItem.Index = 0 Then
                                Me.tmpDBElement.EFanartsPath = efPath
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
        Try
            If Not String.IsNullOrEmpty(Me.tmpDBElement.TVShow.MPAA) Then
                Dim i As Integer = 0
                For ctr As Integer = 0 To Me.lbMPAA.Items.Count - 1
                    If Me.tmpDBElement.TVShow.MPAA.ToLower.StartsWith(Me.lbMPAA.Items.Item(ctr).ToString.ToLower) Then
                        i = ctr
                        Exit For
                    End If
                Next
                Me.lbMPAA.SelectedIndex = i
                Me.lbMPAA.TopIndex = i
                Me.txtMPAA.Text = Me.tmpDBElement.TVShow.MPAA
            End If

            If Me.lbMPAA.SelectedItems.Count = 0 Then
                Me.lbMPAA.SelectedIndex = 0
                Me.lbMPAA.TopIndex = 0
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            Me.txtPlot.SelectAll()
        End If
    End Sub

    Private Sub SetInfo()
        Try
            With Me
                Me.tmpDBElement.Ordering = DirectCast(.cbOrdering.SelectedIndex, Enums.Ordering)
                Me.tmpDBElement.EpisodeSorting = DirectCast(.cbEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)

                Me.tmpDBElement.TVShow.Title = .txtTitle.Text.Trim
                Me.tmpDBElement.TVShow.Plot = .txtPlot.Text.Trim
                Me.tmpDBElement.TVShow.Premiered = .txtPremiered.Text.Trim
                Me.tmpDBElement.TVShow.Runtime = .txtRuntime.Text.Trim
                Me.tmpDBElement.TVShow.SortTitle = .txtSortTitle.Text.Trim
                Me.tmpDBElement.TVShow.Status = .txtStatus.Text.Trim
                Me.tmpDBElement.TVShow.Studio = .txtStudio.Text.Trim
                Me.tmpDBElement.TVShow.Votes = .txtVotes.Text.Trim

                If Not String.IsNullOrEmpty(.txtTitle.Text) Then
                    If Master.eSettings.TVDisplayStatus AndAlso Not String.IsNullOrEmpty(.txtStatus.Text.Trim) Then
                        Me.tmpDBElement.ListTitle = String.Format("{0} ({1})", StringUtils.SortTokens_TV(.txtTitle.Text.Trim), .txtStatus.Text.Trim)
                    Else
                        Me.tmpDBElement.ListTitle = StringUtils.SortTokens_TV(.txtTitle.Text.Trim)
                    End If
                End If

                Me.tmpDBElement.TVShow.MPAA = .txtMPAA.Text.Trim

                Me.tmpDBElement.TVShow.Rating = .tmpRating

                If .clbGenre.CheckedItems.Count > 0 Then

                    If .clbGenre.CheckedIndices.Contains(0) Then
                        Me.tmpDBElement.TVShow.Genre = String.Empty
                    Else
                        Dim strGenre As String = String.Empty
                        Dim isFirst As Boolean = True
                        Dim iChecked = From iCheck In .clbGenre.CheckedItems
                        strGenre = String.Join(" / ", iChecked.ToArray)
                        Me.tmpDBElement.TVShow.Genre = strGenre.Trim
                    End If
                End If

                Me.tmpDBElement.TVShow.Actors.Clear()

                If .lvActors.Items.Count > 0 Then
                    Dim iOrder As Integer = 0
                    For Each lviActor As ListViewItem In .lvActors.Items
                        Dim addActor As New MediaContainers.Person
                        addActor.ID = CInt(lviActor.Text.Trim)
                        addActor.Name = lviActor.SubItems(1).Text.Trim
                        addActor.Role = lviActor.SubItems(2).Text.Trim
                        addActor.ThumbURL = lviActor.SubItems(3).Text.Trim
                        addActor.Order = iOrder
                        iOrder += 1
                        Me.tmpDBElement.TVShow.Actors.Add(addActor)
                    Next
                End If

                If ActorThumbsHasChanged Then
                    For Each a In FileUtils.GetFilenameList.TVShow(Me.tmpDBElement.ShowPath, Enums.ModifierType.MainActorThumbs)
                        Dim tmpPath As String = Directory.GetParent(a.Replace("<placeholder>", "dummy")).FullName
                        If Directory.Exists(tmpPath) Then
                            FileUtils.Delete.DeleteDirectory(tmpPath)
                        End If
                    Next
                End If

                'Actor Thumbs
                If ActorThumbsHasChanged Then
                    For Each act As MediaContainers.Person In Me.tmpDBElement.TVShow.Actors
                        Dim img As New Images
                        img.FromWeb(act.ThumbURL)
                        If img.Image IsNot Nothing Then
                            act.ThumbPath = img.SaveAsTVShowActorThumb(act, Me.tmpDBElement)
                        Else
                            act.ThumbPath = String.Empty
                        End If
                    Next
                End If

            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        Dim mTitle As String = Me.tmpDBElement.TVShow.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(663, "Edit Show"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Text = sTitle
        Me.btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        Me.btnRemoveBanner.Text = Master.eLang.GetString(1024, "Remove Banner")
        Me.btnRemoveCharacterArt.Text = Master.eLang.GetString(1145, "Remove CharacterArt")
        Me.btnRemoveClearArt.Text = Master.eLang.GetString(1087, "Remove ClearArt")
        Me.btnRemoveClearLogo.Text = Master.eLang.GetString(1091, "Remove ClearLogo")
        Me.btnRemoveFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnRemoveLandscape.Text = Master.eLang.GetString(1034, "Remove Landscape")
        Me.btnRemovePoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnSetCharacterArtDL.Text = Master.eLang.GetString(1144, "Change CharacterArt (Download)")
        Me.btnSetCharacterArtLocal.Text = Master.eLang.GetString(1142, "Change CharacterArt (Local)")
        Me.btnSetCharacterArtScrape.Text = Master.eLang.GetString(1143, "Change CharacterArt (Scrape)")
        Me.btnSetClearArtDL.Text = Master.eLang.GetString(1086, "Change ClearArt (Download)")
        Me.btnSetClearArtLocal.Text = Master.eLang.GetString(1084, "Change ClearArt (Local)")
        Me.btnSetClearArtScrape.Text = Master.eLang.GetString(1085, "Change ClearArt (Scrape)")
        Me.btnSetClearLogoDL.Text = Master.eLang.GetString(1090, "Change ClearLogo (Download)")
        Me.btnSetClearLogoLocal.Text = Master.eLang.GetString(1088, "Change ClearLogo (Local)")
        Me.btnSetClearLogoScrape.Text = Master.eLang.GetString(1089, "Change ClearLogo (Scrape)")
        Me.btnSetBannerDL.Text = Master.eLang.GetString(1023, "Change Banner (Download)")
        Me.btnSetBannerLocal.Text = Master.eLang.GetString(1021, "Change Banner (Local)")
        Me.btnSetBannerScrape.Text = Master.eLang.GetString(1022, "Change Banner (Scrape)")
        Me.btnSetFanartDL.Text = Master.eLang.GetString(1033, "Change Landscape (Download)")
        Me.btnSetFanartLocal.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.btnSetFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetLandscapeDL.Text = Master.eLang.GetString(1033, "Change Landscape (Download)")
        Me.btnSetLandscapeLocal.Text = Master.eLang.GetString(1031, "Change Landscape (Local)")
        Me.btnSetLandscapeScrape.Text = Master.eLang.GetString(1032, "Change Landscape (Scrape)")
        Me.btnSetPosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetPosterLocal.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.btnSetPosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.btnEFanartsSetAsFanart.Text = Master.eLang.GetString(255, "Set As Fanart")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colRole.Text = Master.eLang.GetString(233, "Role")
        Me.colThumb.Text = Master.eLang.GetString(234, "Thumb")
        Me.lblActors.Text = Master.eLang.GetString(231, "Actors:")
        Me.lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblEpisodeSorting.Text = String.Concat(Master.eLang.GetString(364, "Show Episodes by"), ":")
        Me.lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        Me.lblOrdering.Text = Master.eLang.GetString(739, "Episode Ordering:")
        Me.lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        Me.lblPremiered.Text = String.Concat(Master.eLang.GetString(724, "Premiered"), ":")
        Me.lblRating.Text = Master.eLang.GetString(245, "Rating:")
        Me.lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        Me.lblStatus.Text = String.Concat(Master.eLang.GetString(215, "Status"), ":")
        Me.lblStudio.Text = Master.eLang.GetString(226, "Studio:")
        Me.lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Me.lblTopDetails.Text = Master.eLang.GetString(664, "Edit the details for the selected show.")
        Me.lblTopTitle.Text = Master.eLang.GetString(663, "Edit Show")
        Me.lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        Me.tpBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpCharacterArt.Text = Master.eLang.GetString(1140, "CharacterArt")
        Me.tpClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.tpClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.tpDetails.Text = Master.eLang.GetString(26, "Details")
        Me.tpEFanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        Me.tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpLandscape.Text = Master.eLang.GetString(1035, "Landscape")
        Me.tpPoster.Text = Master.eLang.GetString(148, "Poster")

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