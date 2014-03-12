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

Public Class dlgEditSeason

#Region "Fields"

    Private Banner As New Images With {.IsEdit = True}
    Private Fanart As New Images With {.IsEdit = True}
    Private Landscape As New Images With {.IsEdit = True}
    Private Poster As New Images With {.IsEdit = True}

#End Region 'Fields

#Region "Methods"

    Private Sub btnRemoveSeasonBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonBanner.Click
        Me.pbSeasonBanner.Image = Nothing
        Me.pbSeasonBanner.Tag = Nothing
        Me.Banner.Dispose()
    End Sub

    Private Sub btnRemoveSeasonFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonFanart.Click
        Me.pbSeasonFanart.Image = Nothing
        Me.pbSeasonFanart.Tag = Nothing
        Me.Fanart.Dispose()
    End Sub

    Private Sub btnRemoveSeasonLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonLandscape.Click
        Me.pbSeasonLandscape.Image = Nothing
        Me.pbSeasonLandscape.Tag = Nothing
        Me.Landscape.Dispose()
    End Sub

    Private Sub btnRemoveSeasonPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSeasonPoster.Click
        Me.pbSeasonPoster.Image = Nothing
        Me.pbSeasonPoster.Tag = Nothing
        Me.Poster.Dispose()
    End Sub

    Private Sub btnSetSeasonBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Banner = tImage
                        Me.pbSeasonBanner.Image = Banner.Image
                        Me.pbSeasonBanner.Tag = Banner

                        Me.lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonBanner.Image.Width, Me.pbSeasonBanner.Image.Height)
                        Me.lblSeasonBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetSeasonBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonBannerScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonBanner, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(Banner, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            Banner = tImage
            Me.pbSeasonBanner.Image = tImage.Image
            Me.pbSeasonBanner.Tag = tImage

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
                Banner.FromFile(ofdImage.FileName)
                Me.pbSeasonBanner.Image = Banner.Image
                Me.pbSeasonBanner.Tag = Banner

                Me.lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonBanner.Image.Width, Me.pbSeasonBanner.Image.Height)
                Me.lblSeasonBannerSize.Visible = True
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetSeasonFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Fanart = tImage
                        Me.pbSeasonFanart.Image = Fanart.Image
                        Me.pbSeasonFanart.Tag = Fanart

                        Me.lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonFanart.Image.Width, Me.pbSeasonFanart.Image.Height)
                        Me.lblSeasonFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetSeasonFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonFanartScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonFanart, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(Fanart, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            Fanart = tImage
            Me.pbSeasonFanart.Image = tImage.Image
            Me.pbSeasonFanart.Tag = tImage

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
                Fanart.FromFile(ofdImage.FileName)
                Me.pbSeasonFanart.Image = Fanart.Image
                Me.pbSeasonFanart.Tag = Fanart

                Me.lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonFanart.Image.Width, Me.pbSeasonFanart.Image.Height)
                Me.lblSeasonFanartSize.Visible = True
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetSeasonLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonLandscapeDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Landscape = tImage
                        Me.pbSeasonLandscape.Image = Landscape.Image
                        Me.pbSeasonLandscape.Tag = Landscape

                        Me.lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonLandscape.Image.Width, Me.pbSeasonLandscape.Image.Height)
                        Me.lblSeasonLandscapeSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetSeasonLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonLandscapeScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonLandscape, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(Landscape, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            Landscape = tImage
            Me.pbSeasonLandscape.Image = tImage.Image
            Me.pbSeasonLandscape.Tag = tImage

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
                Landscape.FromFile(ofdImage.FileName)
                Me.pbSeasonLandscape.Image = Landscape.Image
                Me.pbSeasonLandscape.Tag = Landscape

                Me.lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonLandscape.Image.Width, Me.pbSeasonLandscape.Image.Height)
                Me.lblSeasonLandscapeSize.Visible = True
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetSeasonPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Poster = tImage
                        Me.pbSeasonPoster.Image = Poster.Image
                        Me.pbSeasonPoster.Tag = Poster

                        Me.lblSeasonPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonPoster.Image.Width, Me.pbSeasonPoster.Image.Height)
                        Me.lblSeasonPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetSeasonPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSeasonPosterScrape.Click
        Dim tImage As Images = ModulesManager.Instance.TVSingleImageOnly(Master.currShow.TVShow.Title, Convert.ToInt32(Master.currShow.ShowID), Master.currShow.TVShow.ID, Enums.TVImageType.SeasonPoster, Master.currShow.TVEp.Season, 0, Master.currShow.ShowLanguage, Master.currShow.Ordering, CType(Poster, Images))

        If Not IsNothing(tImage) AndAlso Not IsNothing(tImage.Image) Then
            Poster = tImage
            Me.pbSeasonPoster.Image = tImage.Image
            Me.pbSeasonPoster.Tag = tImage

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
                Poster.FromFile(ofdImage.FileName)
                Me.pbSeasonPoster.Image = Poster.Image
                Me.pbSeasonPoster.Tag = Poster

                Me.lblSeasonPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbSeasonPoster.Image.Width, Me.pbSeasonPoster.Image.Height)
                Me.lblSeasonPosterSize.Visible = True
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgEditSeason_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Master.eSettings.TVSeasonBannerEnabled Then tcEditSeason.TabPages.Remove(tpSeasonBanner)
        If Not Master.eSettings.TVSeasonFanartEnabled Then tcEditSeason.TabPages.Remove(tpSeasonFanart)
        If Not Master.eSettings.TVSeasonLandscapeEnabled Then tcEditSeason.TabPages.Remove(tpSeasonLandscape)
        If Not Master.eSettings.TVSeasonPosterEnabled Then tcEditSeason.TabPages.Remove(tpSeasonPoster)

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
            If Master.eSettings.TVSeasonBannerEnabled Then
                Banner.FromFile(Master.currShow.SeasonBannerPath)
                If Not IsNothing(Banner.Image) Then
                    .pbSeasonBanner.Image = Banner.Image
                    .pbSeasonBanner.Tag = Banner

                    .lblSeasonBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbSeasonBanner.Image.Width, .pbSeasonBanner.Image.Height)
                    .lblSeasonBannerSize.Visible = True
                End If
            End If

            If Master.eSettings.TVSeasonFanartEnabled Then
                Fanart.FromFile(Master.currShow.SeasonFanartPath)
                If Not IsNothing(Fanart.Image) Then
                    .pbSeasonFanart.Image = Fanart.Image
                    .pbSeasonFanart.Tag = Fanart

                    .lblSeasonFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbSeasonFanart.Image.Width, .pbSeasonFanart.Image.Height)
                    .lblSeasonFanartSize.Visible = True
                End If
            End If

            If Master.eSettings.TVSeasonLandscapeEnabled Then
                Landscape.FromFile(Master.currShow.SeasonLandscapePath)
                If Not IsNothing(Landscape.Image) Then
                    .pbSeasonLandscape.Image = Landscape.Image
                    .pbSeasonLandscape.Tag = Landscape

                    .lblSeasonLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbSeasonLandscape.Image.Width, .pbSeasonLandscape.Image.Height)
                    .lblSeasonLandscapeSize.Visible = True
                End If
            End If

            If Master.eSettings.TVSeasonPosterEnabled Then
                Poster.FromFile(Master.currShow.SeasonPosterPath)
                If Not IsNothing(Poster.Image) Then
                    .pbSeasonPoster.Image = Poster.Image
                    .pbSeasonPoster.Tag = Poster

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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SetInfo()
        Try
            With Me

                'Banner
                If Not IsNothing(.Banner.Image) Then
                    Dim fPath As String = .Banner.SaveAsTVSeasonBanner(Master.currShow)
                    Master.currShow.SeasonBannerPath = fPath
                Else
                    .Fanart.DeleteTVSeasonBanner(Master.currShow)
                    Master.currShow.SeasonBannerPath = String.Empty
                End If

                'Fanart
                If Not IsNothing(.Fanart.Image) Then
                    Dim fPath As String = .Fanart.SaveAsTVSeasonFanart(Master.currShow)
                    Master.currShow.SeasonFanartPath = fPath
                Else
                    .Fanart.DeleteTVSeasonFanart(Master.currShow)
                    Master.currShow.SeasonFanartPath = String.Empty
                End If

                'Landscape
                If Not IsNothing(.Landscape.Image) Then
                    Dim fPath As String = .Landscape.SaveAsTVSeasonLandscape(Master.currShow)
                    Master.currShow.SeasonLandscapePath = fPath
                Else
                    .Landscape.DeleteTVSeasonLandscape(Master.currShow)
                    Master.currShow.SeasonLandscapePath = String.Empty
                End If

                'Poster
                If Not IsNothing(.Poster.Image) Then
                    Dim pPath As String = .Poster.SaveAsTVSeasonPoster(Master.currShow)
                    Master.currShow.SeasonPosterPath = pPath
                Else
                    .Poster.DeleteTVSeasonPosters(Master.currShow)
                    Master.currShow.SeasonPosterPath = String.Empty
                End If

            End With
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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