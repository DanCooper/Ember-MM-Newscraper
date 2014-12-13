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
Public Class dlgEditSeason

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private SeasonBanner As New Images With {.IsEdit = True}
    Private SeasonFanart As New Images With {.IsEdit = True}
    Private SeasonLandscape As New Images With {.IsEdit = True}
    Private SeasonPoster As New Images With {.IsEdit = True}

#End Region 'Fields

#Region "Methods"

    Private Sub btnRemoveSeasonBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonBanner.Click
        Me.pbSeasonBanner.Image = Nothing
        Me.pbSeasonBanner.Tag = Nothing
        Me.SeasonBanner.Dispose()
    End Sub

    Private Sub btnRemoveSeasonFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonFanart.Click
        Me.pbSeasonFanart.Image = Nothing
        Me.pbSeasonFanart.Tag = Nothing
        Me.SeasonFanart.Dispose()
    End Sub

    Private Sub btnRemoveSeasonLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonLandscape.Click
        Me.pbSeasonLandscape.Image = Nothing
        Me.pbSeasonLandscape.Tag = Nothing
        Me.SeasonLandscape.Dispose()
    End Sub

    Private Sub btnRemoveSeasonPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonPoster.Click
        Me.pbSeasonPoster.Image = Nothing
        Me.pbSeasonPoster.Tag = Nothing
        Me.SeasonPoster.Dispose()
    End Sub

    Private Sub btnSetSeasonBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        SeasonBanner = tImage
                        Me.pbSeasonBanner.Image = SeasonBanner.Image
                        Me.pbSeasonBanner.Tag = SeasonBanner

                        Me.lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonBanner.Image.Width, Me.pbSeasonBanner.Image.Height)
                        Me.lblSeasonBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Async Sub btnSetSeasonBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonBannerScrape.Click
        Dim tImage As New Images

        If Master.currShow.TVEp.Season = 999 Then
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.AllSeasonsBanner, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonBanner, Images))
        Else
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonBanner, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonBanner, Images))
        End If

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            SeasonBanner = tImage
            Me.pbSeasonBanner.Image = SeasonBanner.Image
            Me.pbSeasonBanner.Tag = SeasonBanner

            Me.lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonBanner.Image.Width, Me.pbSeasonBanner.Image.Height)
            Me.lblSeasonBannerSize.Visible = True
        End If
    End Sub

    Private Sub btnSetSeasonBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonBannerLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                SeasonBanner.FromFile(ofdImage.FileName)
                Me.pbSeasonBanner.Image = SeasonBanner.Image
                Me.pbSeasonBanner.Tag = SeasonBanner

                Me.lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonBanner.Image.Width, Me.pbSeasonBanner.Image.Height)
                Me.lblSeasonBannerSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnSetSeasonFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        SeasonFanart = tImage
                        Me.pbSeasonFanart.Image = SeasonFanart.Image
                        Me.pbSeasonFanart.Tag = SeasonFanart

                        Me.lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonFanart.Image.Width, Me.pbSeasonFanart.Image.Height)
                        Me.lblSeasonFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Async Sub btnSetSeasonFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonFanartScrape.Click
        Dim tImage As New Images

        If Master.currShow.TVEp.Season = 999 Then
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.AllSeasonsFanart, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonFanart, Images))
        Else
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonFanart, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonFanart, Images))
        End If

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            SeasonFanart = tImage
            Me.pbSeasonFanart.Image = SeasonFanart.Image
            Me.pbSeasonFanart.Tag = SeasonFanart

            Me.lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonFanart.Image.Width, Me.pbSeasonFanart.Image.Height)
            Me.lblSeasonFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetSeasonFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonFanartLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                SeasonFanart.FromFile(ofdImage.FileName)
                Me.pbSeasonFanart.Image = SeasonFanart.Image
                Me.pbSeasonFanart.Tag = SeasonFanart

                Me.lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonFanart.Image.Width, Me.pbSeasonFanart.Image.Height)
                Me.lblSeasonFanartSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnSetSeasonLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonLandscapeDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        SeasonLandscape = tImage
                        Me.pbSeasonLandscape.Image = SeasonLandscape.Image
                        Me.pbSeasonLandscape.Tag = SeasonLandscape

                        Me.lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonLandscape.Image.Width, Me.pbSeasonLandscape.Image.Height)
                        Me.lblSeasonLandscapeSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Async Sub btnSetSeasonLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonLandscapeScrape.Click
        Dim tImage As New Images

        If Master.currShow.TVEp.Season = 999 Then
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.AllSeasonsLandscape, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonLandscape, Images))
        Else
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonLandscape, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonLandscape, Images))
        End If

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            SeasonLandscape = tImage
            Me.pbSeasonLandscape.Image = SeasonLandscape.Image
            Me.pbSeasonLandscape.Tag = SeasonLandscape

            Me.lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonLandscape.Image.Width, Me.pbSeasonLandscape.Image.Height)
            Me.lblSeasonLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub btnSetSeasonLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonLandscapeLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                SeasonLandscape.FromFile(ofdImage.FileName)
                Me.pbSeasonLandscape.Image = SeasonLandscape.Image
                Me.pbSeasonLandscape.Tag = SeasonLandscape

                Me.lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonLandscape.Image.Width, Me.pbSeasonLandscape.Image.Height)
                Me.lblSeasonLandscapeSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub btnSetSeasonPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        SeasonPoster = tImage
                        Me.pbSeasonPoster.Image = SeasonPoster.Image
                        Me.pbSeasonPoster.Tag = SeasonPoster

                        Me.lblSeasonPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonPoster.Image.Width, Me.pbSeasonPoster.Image.Height)
                        Me.lblSeasonPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Async Sub btnSetSeasonPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonPosterScrape.Click
        Dim tImage As New Images

        If Master.currShow.TVEp.Season = 999 Then
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.AllSeasonsPoster, 0, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonPoster, Images))
        Else
            tImage = Await ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonPoster, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(SeasonPoster, Images))
        End If

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            SeasonPoster = tImage
            Me.pbSeasonPoster.Image = SeasonPoster.Image
            Me.pbSeasonPoster.Tag = SeasonPoster

            Me.lblSeasonPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonPoster.Image.Width, Me.pbSeasonPoster.Image.Height)
            Me.lblSeasonPosterSize.Visible = True
        End If
    End Sub

    Private Sub btnSetSeasonPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonPosterLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Master.currShow.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                SeasonPoster.FromFile(ofdImage.FileName)
                Me.pbSeasonPoster.Image = SeasonPoster.Image
                Me.pbSeasonPoster.Tag = SeasonPoster

                Me.lblSeasonPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonPoster.Image.Width, Me.pbSeasonPoster.Image.Height)
                Me.lblSeasonPosterSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgEditSeason_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Master.eSettings.TVSeasonBannerAnyEnabled Then tcEditSeason.TabPages.Remove(tpSeasonBanner)
        If Not Master.eSettings.TVSeasonFanartAnyEnabled Then tcEditSeason.TabPages.Remove(tpSeasonFanart)
        If Not Master.eSettings.TVSeasonLandscapeAnyEnabled Then tcEditSeason.TabPages.Remove(tpSeasonLandscape)
        If Not Master.eSettings.TVSeasonPosterAnyEnabled Then tcEditSeason.TabPages.Remove(tpSeasonPoster)

        Me.pbSeasonBanner.AllowDrop = True
        Me.pbSeasonFanart.AllowDrop = True
        Me.pbSeasonLandscape.AllowDrop = True
        Me.pbSeasonPoster.AllowDrop = True

        Me.SetUp()

        Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            Me.pnlTop.BackgroundImage = iBackground
        End Using

        Me.FillInfo()
    End Sub

    Private Sub FillInfo()
        With Me
            If Master.eSettings.TVSeasonBannerAnyEnabled Then
                SeasonBanner.FromFile(Master.currShow.SeasonBannerPath)
                If Not IsNothing(SeasonBanner.Image) Then
                    .pbSeasonBanner.Image = SeasonBanner.Image
                    .pbSeasonBanner.Tag = SeasonBanner

                    .lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbSeasonBanner.Image.Width, .pbSeasonBanner.Image.Height)
                    .lblSeasonBannerSize.Visible = True
                End If
            End If

            If Master.eSettings.TVSeasonFanartAnyEnabled Then
                SeasonFanart.FromFile(Master.currShow.SeasonFanartPath)
                If Not IsNothing(SeasonFanart.Image) Then
                    .pbSeasonFanart.Image = SeasonFanart.Image
                    .pbSeasonFanart.Tag = SeasonFanart

                    .lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbSeasonFanart.Image.Width, .pbSeasonFanart.Image.Height)
                    .lblSeasonFanartSize.Visible = True
                End If
            End If

            If Master.eSettings.TVSeasonLandscapeAnyEnabled Then
                SeasonLandscape.FromFile(Master.currShow.SeasonLandscapePath)
                If Not IsNothing(SeasonLandscape.Image) Then
                    .pbSeasonLandscape.Image = SeasonLandscape.Image
                    .pbSeasonLandscape.Tag = SeasonLandscape

                    .lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbSeasonLandscape.Image.Width, .pbSeasonLandscape.Image.Height)
                    .lblSeasonLandscapeSize.Visible = True
                End If
            End If

            If Master.eSettings.TVSeasonPosterAnyEnabled Then
                SeasonPoster.FromFile(Master.currShow.SeasonPosterPath)
                If Not IsNothing(SeasonPoster.Image) Then
                    .pbSeasonPoster.Image = SeasonPoster.Image
                    .pbSeasonPoster.Tag = SeasonPoster

                    .lblSeasonPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbSeasonPoster.Image.Width, .pbSeasonPoster.Image.Height)
                    .lblSeasonPosterSize.Visible = True
                End If
            End If
        End With
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.SetInfo()

            Master.DB.SaveTVSeasonToDB(Master.currShow, False)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Async Sub pbSeasonBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbSeasonBanner.DragDrop
        Dim tImage As Images = Await FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            SeasonBanner = tImage
            Me.pbSeasonBanner.Image = SeasonBanner.Image
            Me.pbSeasonBanner.Tag = SeasonBanner
            Me.lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonBanner.Image.Width, Me.pbSeasonBanner.Image.Height)
            Me.lblSeasonBannerSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonBanner_DragEnter(sender As Object, e As DragEventArgs) Handles pbSeasonBanner.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Async Sub pbSeasonFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbSeasonFanart.DragDrop
        Dim tImage As Images = Await FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            SeasonFanart = tImage
            Me.pbSeasonFanart.Image = SeasonFanart.Image
            Me.pbSeasonFanart.Tag = SeasonFanart
            Me.lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonFanart.Image.Width, Me.pbSeasonFanart.Image.Height)
            Me.lblSeasonFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbSeasonFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Async Sub pbSeasonLandscape_DragDrop(sender As Object, e As DragEventArgs) Handles pbSeasonLandscape.DragDrop
        Dim tImage As Images = Await FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            SeasonLandscape = tImage
            Me.pbSeasonLandscape.Image = SeasonLandscape.Image
            Me.pbSeasonLandscape.Tag = SeasonLandscape
            Me.lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonLandscape.Image.Width, Me.pbSeasonLandscape.Image.Height)
            Me.lblSeasonLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonLandscape_DragEnter(sender As Object, e As DragEventArgs) Handles pbSeasonLandscape.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Async Sub pbSeasonPoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbSeasonPoster.DragDrop
        Dim tImage As Images = Await FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            SeasonPoster = tImage
            Me.pbSeasonPoster.Image = SeasonPoster.Image
            Me.pbSeasonPoster.Tag = SeasonPoster
            Me.lblSeasonPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonPoster.Image.Width, Me.pbSeasonPoster.Image.Height)
            Me.lblSeasonPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonPoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbSeasonPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Async Sub SetInfo()
        Try
            'Banner
            If Master.currShow.TVEp.Season = 999 Then
                If Not IsNothing(Me.SeasonBanner.Image) Then
                    'AllSeasons Banner
                    Master.currShow.SeasonBannerPath = Await Me.SeasonBanner.SaveAsTVASBanner(Master.currShow, "")
                Else
                    Me.SeasonBanner.DeleteTVASBanner(Master.currShow)
                    Master.currShow.SeasonBannerPath = String.Empty
                End If
            Else
                'Season Banner
                If Not IsNothing(Me.SeasonBanner.Image) Then
                    Master.currShow.SeasonBannerPath = Await Me.SeasonBanner.SaveAsTVSeasonBanner(Master.currShow)
                Else
                    Me.SeasonBanner.DeleteTVSeasonBanner(Master.currShow)
                    Master.currShow.SeasonBannerPath = String.Empty
                End If
            End If

            'Fanart
            If Master.currShow.TVEp.Season = 999 Then
                If Not IsNothing(Me.SeasonFanart.Image) Then
                    'AllSeasons Fanart
                    Master.currShow.SeasonFanartPath = Await Me.SeasonFanart.SaveAsTVASFanart(Master.currShow, "")
                Else
                    Me.SeasonFanart.DeleteTVASFanart(Master.currShow)
                    Master.currShow.SeasonFanartPath = String.Empty
                End If
            Else
                'Season Fanart
                If Not IsNothing(Me.SeasonFanart.Image) Then
                    Master.currShow.SeasonFanartPath = Await Me.SeasonFanart.SaveAsTVSeasonFanart(Master.currShow)
                Else
                    Me.SeasonFanart.DeleteTVSeasonFanart(Master.currShow)
                    Master.currShow.SeasonFanartPath = String.Empty
                End If
            End If

            'Landscape
            If Master.currShow.TVEp.Season = 999 Then
                If Not IsNothing(Me.SeasonLandscape.Image) Then
                    'AllSeasons Landscape
                    Master.currShow.SeasonLandscapePath = Await Me.SeasonLandscape.SaveAsTVASLandscape(Master.currShow, "")
                Else
                    Me.SeasonLandscape.DeleteTVASLandscape(Master.currShow)
                    Master.currShow.SeasonLandscapePath = String.Empty
                End If
            Else
                'Season Landscape
                If Not IsNothing(Me.SeasonLandscape.Image) Then
                    Master.currShow.SeasonLandscapePath = Await Me.SeasonLandscape.SaveAsTVSeasonLandscape(Master.currShow)
                Else
                    Me.SeasonLandscape.DeleteTVSeasonLandscape(Master.currShow)
                    Master.currShow.SeasonLandscapePath = String.Empty
                End If
            End If

            'Poster
            If Master.currShow.TVEp.Season = 999 Then
                If Not IsNothing(Me.SeasonPoster.Image) Then
                    'AllSeasons Poster
                    Master.currShow.SeasonPosterPath = Await Me.SeasonPoster.SaveAsTVASPoster(Master.currShow, "")
                Else
                    Me.SeasonPoster.DeleteTVASPoster(Master.currShow)
                    Master.currShow.SeasonPosterPath = String.Empty
                End If
            Else
                'Season Poster
                If Not IsNothing(Me.SeasonPoster.Image) Then
                    Master.currShow.SeasonPosterPath = Await Me.SeasonPoster.SaveAsTVSeasonPoster(Master.currShow)
                Else
                    Me.SeasonPoster.DeleteTVSeasonPoster(Master.currShow)
                    Master.currShow.SeasonPosterPath = String.Empty
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(769, "Edit Season")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.btnRemoveSeasonBanner.Text = Master.eLang.GetString(1024, "Remove Banner")
        Me.btnRemoveSeasonFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnRemoveSeasonLandscape.Text = Master.eLang.GetString(1034, "Remove Landscape")
        Me.btnRemoveSeasonPoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnSetSeasonBannerDL.Text = Master.eLang.GetString(1023, "Change Banner (Download)")
        Me.btnSetSeasonBannerLocal.Text = Master.eLang.GetString(1021, "Change Banner (Local)")
        Me.btnSetSeasonBannerScrape.Text = Master.eLang.GetString(1022, "Change Banner (Scrape)")
        Me.btnSetSeasonFanartDL.Text = Master.eLang.GetString(266, "Change Fanart (Download)")
        Me.btnSetSeasonFanartLocal.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.btnSetSeasonFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetSeasonLandscapeDL.Text = Master.eLang.GetString(1033, "Change Landscape (Download)")
        Me.btnSetSeasonLandscapeLocal.Text = Master.eLang.GetString(1031, "Change Landscape (Local)/")
        Me.btnSetSeasonLandscapeScrape.Text = Master.eLang.GetString(1032, "Change Landscape (Scrape)")
        Me.btnSetSeasonPosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetSeasonPosterLocal.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.btnSetSeasonPosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.lblTopTitle.Text = Me.Text
        Me.tblTopDetails.Text = Master.eLang.GetString(830, "Edit the details for the selected season.")
        Me.tpSeasonBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpSeasonFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpSeasonLandscape.Text = Master.eLang.GetString(1035, "Landscape")
        Me.tpSeasonPoster.Text = Master.eLang.GetString(148, "Poster")
    End Sub

#End Region 'Methods

End Class