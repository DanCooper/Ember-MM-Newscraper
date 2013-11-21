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

Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports Ember_Media_Manager.frmMain

Public Class dlgEditMovie

#Region "Fields"

    Friend WithEvents bwEThumbs As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwEFanarts As New System.ComponentModel.BackgroundWorker

    Private CachePath As String = String.Empty
    Private Fanart As New Images With {.IsEdit = True}
    Private fResults As New Containers.ImgResult
    Private isAborting As Boolean = False
    Private lvwActorSorter As ListViewColumnSorter
    'Private lvwEThumbsSorter As ListViewColumnSorter
    'Private lvwEFanartsSorter As ListViewColumnSorter
    Private Poster As New Images With {.IsEdit = True}
    Private pResults As New Containers.ImgResult
    Private PreviousFrameValue As Integer
    Private tmpRating As String = String.Empty

    'Extrathumbs
    Private etDeleteList As New List(Of String)
    Private EThumbsIndex As Integer = -1
    Private EThumbsList As New List(Of ExtraImages)
    Private hasClearedET As Boolean = False
    Private iETCounter As Integer = 0
    Private iETLeft As Integer = 1
    Private iETTop As Integer = 1
    Private pbETImage() As PictureBox
    Private pnlETImage() As Panel

    'Extrafanarts
    Private efDeleteList As New List(Of String)
    Private EFanartsIndex As Integer = -1
    Private EFanartsList As New List(Of ExtraImages)
    Private hasClearedEF As Boolean = False
    Private iEFCounter As Integer = 0
    Private iEFLeft As Integer = 1
    Private iEFTop As Integer = 1
    Private pbEFImage() As PictureBox
    Private pnlEFImage() As Panel

#End Region 'Fields

#Region "Methods"
    Private Sub AddETImage(ByVal sDescription As String, ByVal iIndex As Integer, Extrathumb As ExtraImages)
        Try
            ReDim Preserve Me.pnlETImage(iIndex)
            ReDim Preserve Me.pbETImage(iIndex)
            Me.pnlETImage(iIndex) = New Panel()
            Me.pbETImage(iIndex) = New PictureBox()
            Me.pbETImage(iIndex).Name = iIndex.ToString
            Me.pnlETImage(iIndex).Name = iIndex.ToString
            Me.pnlETImage(iIndex).Size = New Size(128, 72)
            Me.pbETImage(iIndex).Size = New Size(128, 72)
            Me.pnlETImage(iIndex).BackColor = Color.White
            Me.pnlETImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbETImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlETImage(iIndex).Tag = Extrathumb.Image
            Me.pbETImage(iIndex).Tag = Extrathumb.Image
            Me.pbETImage(iIndex).Image = CType(Extrathumb.Image.Image.Clone(), Image)
            Me.pnlETImage(iIndex).Left = iETLeft
            Me.pbETImage(iIndex).Left = 0
            Me.pnlETImage(iIndex).Top = iETTop
            Me.pbETImage(iIndex).Top = 0
            Me.pnlEThumbsBG.Controls.Add(Me.pnlETImage(iIndex))
            Me.pnlETImage(iIndex).Controls.Add(Me.pbETImage(iIndex))
            Me.pnlETImage(iIndex).BringToFront()
            AddHandler pbETImage(iIndex).Click, AddressOf pbETImage_Click
            AddHandler pnlETImage(iIndex).Click, AddressOf pnlETImage_Click
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        Me.iETTop += 74

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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        Me.iEFTop += 74

    End Sub

    Private Sub pbETImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectET(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, Images))
    End Sub

    Private Sub pnlETImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectET(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, Images))
    End Sub

    Private Sub pbEFImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, Images))
    End Sub

    Private Sub pnlEFImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, Images))
    End Sub

    Private Sub DoSelectET(ByVal iIndex As Integer, poster As Images)
        Try
            Me.pbEThumbs.Image = poster.Image
            Me.pbEThumbs.Tag = poster
            Me.btnEThumbsSetAsFanart.Enabled = True
            Me.EThumbsIndex = iIndex
            Me.lblEThumbsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEThumbs.Image.Width, Me.pbEThumbs.Image.Height)
            Me.lblEThumbsSize.Visible = True
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub DoSelectEF(ByVal iIndex As Integer, poster As Images)
        Try
            Me.pbEFanarts.Image = poster.Image
            Me.pbEFanarts.Tag = poster
            Me.btnEFanartsSetAsFanart.Enabled = True
            Me.EFanartsIndex = iIndex
            Me.lblEFanartsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbEFanarts.Image.Width, Me.pbEFanarts.Image.Height)
            Me.lblEFanartsSize.Visible = True
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnChangeMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeMovie.Click
        Me.CleanUp()
        ' ***
        Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.Close()
    End Sub

    Private Sub btnDLTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDLTrailer.Click
        Dim aUrlList As New List(Of Trailers)
        Dim tURL As String = String.Empty
        If Not ModulesManager.Instance.MovieScrapeTrailer(Master.currMovie, Enums.ScraperCapabilities.Trailer, aUrlList) Then
            Using dTrailerSelect As New dlgTrailerSelect()
                tURL = dTrailerSelect.ShowDialog(Master.currMovie, aUrlList)
            End Using
        End If

        If Not String.IsNullOrEmpty(tURL) Then
            Me.btnPlayTrailer.Enabled = True
            If StringUtils.isValidURL(tURL) Then
                Me.txtTrailer.Text = tURL
            Else
                Master.currMovie.TrailerPath = tURL
                Me.lblLocalTrailer.Visible = True
            End If
        End If
    End Sub

    ' temporarily disabled
    'Private Sub btnEThumbsDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsDown.Click
    '    Try
    '        If EThumbsList.Count > 0 AndAlso EThumbsIndex < (EThumbsList.Count - 1) Then
    '            Dim iIndex As Integer = EThumbsIndex
    '            EThumbsList.Item(iIndex).Index = EThumbsList.Item(iIndex).Index + 1
    '            EThumbsList.Item(iIndex + 1).Index = EThumbsList.Item(iIndex + 1).Index - 1
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    Private Sub btnEditActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditActor.Click
        EditActor()
    End Sub

    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        Try
            If dlgManualEdit.ShowDialog(Master.currMovie.NfoPath) = Windows.Forms.DialogResult.OK Then
                Master.currMovie.Movie = NFO.LoadMovieFromNFO(Master.currMovie.NfoPath, Master.currMovie.isSingle)
                Me.FillInfo(False)
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayTrailer.Click
        Try

            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(Master.currMovie.TrailerPath) Then
                tPath = String.Concat("""", Master.currMovie.TrailerPath, """")
            ElseIf Not String.IsNullOrEmpty(Me.txtTrailer.Text) Then
                tPath = String.Concat("""", Me.txtTrailer.Text, """")
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.isWindows Then
                    If Regex.IsMatch(tPath, "plugin:\/\/plugin\.video\.youtube\/\?action=play_video&videoid=") Then
                        tPath = Replace(tPath, "plugin://plugin.video.youtube/?action=play_video&videoid=", "http://www.youtube.com/watch?v=")
                    End If
                    Process.Start(tPath)
                Else
                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "xdg-open"
                        Explorer.StartInfo.Arguments = tPath
                        Explorer.Start()
                    End Using
                End If
            End If

        Catch
            MsgBox(Master.eLang.GetString(270, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), MsgBoxStyle.Critical, Master.eLang.GetString(271, "Error Playing Trailer"))
        End Try
    End Sub

    Private Sub btnRemoveFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFanart.Click
        Me.pbFanart.Image = Nothing
        Me.pbFanart.Tag = Nothing
        Me.Fanart.Dispose()
    End Sub

    Private Sub btnRemovePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePoster.Click
        Me.pbPoster.Image = Nothing
        Me.pbPoster.Tag = Nothing
        Me.Poster.Dispose()
    End Sub

    Private Sub btnEThumbsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsRemove.Click
        Me.DeleteEThumbs()
        Me.RefreshEThumbs()
        Me.lblEThumbsSize.Text = ""
        Me.lblEThumbsSize.Visible = False
    End Sub

    Private Sub btnEFanartsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEFanartsRemove.Click
        Me.DeleteEFanarts()
        Me.RefreshEFanarts()
        Me.lblEFanartsSize.Text = ""
        Me.lblEFanartsSize.Visible = False
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Me.DeleteActors()
    End Sub

    Private Sub btnRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
        Me.CleanUp()
        ' ***
        Me.DialogResult = System.Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub btnEThumbsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsSetAsFanart.Click
        If Not String.IsNullOrEmpty(Me.EThumbsList.Item(Me.EThumbsIndex).Path) AndAlso Me.EThumbsList.Item(Me.EThumbsIndex).Path.Substring(0, 1) = ":" Then
            Fanart.FromWeb(Me.EThumbsList.Item(Me.EThumbsIndex).Path.Substring(1, Me.EThumbsList.Item(Me.EThumbsIndex).Path.Length - 1))
        Else
            Fanart.FromFile(Me.EThumbsList.Item(Me.EThumbsIndex).Path)
        End If
        If Not IsNothing(Fanart.Image) Then
            Me.pbFanart.Image = Fanart.Image
            Me.pbFanart.Tag = Fanart

            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnEFanartsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEFanartsSetAsFanart.Click
        If Not String.IsNullOrEmpty(Me.EFanartsList.Item(Me.EFanartsIndex).Path) AndAlso Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(0, 1) = ":" Then
            Fanart.FromWeb(Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(1, Me.EFanartsList.Item(Me.EFanartsIndex).Path.Length - 1))
        Else
            Fanart.FromFile(Me.EFanartsList.Item(Me.EFanartsIndex).Path)
        End If
        If Not IsNothing(Fanart.Image) Then
            Me.pbFanart.Image = Fanart.Image
            Me.pbFanart.Tag = Fanart

            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog(Enums.ImageType.Fanart) = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Fanart = tImage
                        Me.pbFanart.Image = Fanart.Image
                        Me.pbFanart.Tag = Fanart

                        Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                        Me.lblFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartScrape.Click
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim pResults As New MediaContainers.Image
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.Fanart, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.ImageType.Fanart, aList, efList, etList, True) = DialogResult.OK Then
                        pResults = dlgImgS.Results
                        Master.currMovie.etList = dlgImgS.etList
                        Master.currMovie.efList = dlgImgS.efList
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            pbFanart.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Cursor = Cursors.Default

                            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                            Me.lblFanartSize.Visible = True
                        End If
                        Fanart = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(969, "No fanart could be found. Please check to see if any fanart scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(970, "No Fanart Found"))
                End If
            End If

            RefreshEFanarts()
            RefreshEThumbs()
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanart.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Fanart.FromFile(ofdImage.FileName)
                Me.pbFanart.Image = Fanart.Image
                Me.pbFanart.Tag = Fanart

                Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                Me.lblFanartSize.Visible = True
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog(Enums.ImageType.Posters) = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        Poster = tImage
                        Me.pbPoster.Image = Poster.Image
                        Me.pbPoster.Tag = Poster

                        Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                        Me.lblPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            Dim sPath As String = Path.Combine(Master.TempPath, "poster.jpg")

            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.Poster, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.ImageType.Posters, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                        pResults = dlgImgS.Results
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            pbPoster.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Cursor = Cursors.Default
                            Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                            Me.lblPosterSize.Visible = True
                        End If
                        Poster = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(971, "No poster images could be found. Please check to see if any poster scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(972, "No Posters Found"))
                End If
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSetPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPoster.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Poster.FromFile(ofdImage.FileName)
                Me.pbPoster.Image = Poster.Image
                Me.pbPoster.Tag = Poster

                Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                Me.lblPosterSize.Visible = True
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnStudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStudio.Click
        Using dStudio As New dlgStudioSelect
            Dim tStudio As String = dStudio.ShowDialog(Master.currMovie)
            If Not String.IsNullOrEmpty(tStudio) Then
                Me.txtStudio.Text = tStudio
            End If
        End Using
    End Sub

    Private Sub btnEThumbsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsRefresh.Click
        Me.RefreshEThumbs()
    End Sub

    Private Sub btnEFanartsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEFanartsRefresh.Click
        Me.RefreshEFanarts()
    End Sub

    ' temporarily disabled
    'Private Sub btnEThumbsTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsTransfer.Click
    '    Me.TransferEThumbs()
    '    Me.RefreshEThumbs()
    '    Me.pnlETQueue.Visible = False
    'End Sub

    ' temporarily disabled
    'Private Sub btnEFanartsTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEFanartsTransfer.Click
    '    Me.TransferEFanarts()
    '    Me.RefreshEFanarts()
    '    Me.pnlETQueue.Visible = False
    'End Sub

    ' temporarily disabled
    'Private Sub btnEThumbsUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsUp.Click
    '    Try
    '        If lvEThumbs.Items.Count > 0 AndAlso lvEThumbs.SelectedIndices(0) > 0 Then
    '            Dim iIndex As Integer = lvEThumbs.SelectedIndices(0)
    '            lvEThumbs.Items(iIndex).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEThumbs.Items(iIndex).Text.Trim) - 1))
    '            lvEThumbs.Items(iIndex - 1).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEThumbs.Items(iIndex - 1).Text.Trim) + 1))
    '            lvEThumbs.Sort()
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    ' temporarily disabled
    'Private Sub btnEFanartsUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If lvEFanarts.Items.Count > 0 AndAlso lvEFanarts.SelectedIndices(0) > 0 Then
    '            Dim iIndex As Integer = lvEFanarts.SelectedIndices(0)
    '            lvEFanarts.Items(iIndex).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEFanarts.Items(iIndex).Text.Trim) - 1))
    '            lvEFanarts.Items(iIndex - 1).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEFanarts.Items(iIndex - 1).Text.Trim) + 1))
    '            lvEFanarts.Sort()
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub bwEThumbs_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwEThumbs.DoWork
        If Not Master.currMovie.ClearEThumbs OrElse hasClearedET Then LoadEThumbs()
    End Sub

    Private Sub bwEFanarts_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwEFanarts.DoWork
        If Not Master.currMovie.ClearEFanarts OrElse hasClearedEF Then LoadEFanarts()
    End Sub

    Private Sub bwEThumbs_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwEThumbs.RunWorkerCompleted
        Try
            If EThumbsList.Count > 0 Then
                For Each tEThumb As ExtraImages In EThumbsList
                    AddETImage(tEThumb.Name, tEThumb.Index, tEThumb)
                Next
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub bwEFanarts_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwEFanarts.RunWorkerCompleted
        Try
            If EFanartsList.Count > 0 Then
                For Each tEFanart As ExtraImages In EFanartsList
                    AddEFImage(tEFanart.Name, tEFanart.Index, tEFanart)
                Next
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.CleanUp()
        Master.currMovie = Master.DB.LoadMovieFromDB(Master.currMovie.ID)
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

            If Directory.Exists(Path.Combine(Master.TempPath, "extrathumbs")) Then
                FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrathumbs"))
            End If

            If Directory.Exists(Path.Combine(Master.TempPath, "extrafanarts")) Then
                FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrafanarts"))
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub DelayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelayTimer.Tick
        DelayTimer.Stop()
        'GrabTheFrame()
    End Sub

    Private Sub DeleteActors()
        Try
            If Me.lvActors.Items.Count > 0 Then
                While Me.lvActors.SelectedItems.Count > 0
                    Me.lvActors.Items.Remove(Me.lvActors.SelectedItems(0))
                End While
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub DeleteEThumbs()
        Try
            Dim iIndex As Integer = EThumbsIndex

            If iIndex >= 0 Then
                Dim tPath As String = Me.EThumbsList.Item(iIndex).Path
                If Me.EThumbsList.Item(iIndex).Path.Substring(0, 1) = ":" Then
                    Master.currMovie.etList.RemoveAll(Function(Str) Str = tPath)
                    EThumbsList.Remove(EThumbsList.Item(iIndex))
                Else
                    etDeleteList.Add(Me.EThumbsList.Item(iIndex).Path)
                    EThumbsList.Remove(EThumbsList.Item(iIndex))
                End If
                pbEThumbs.Image = Nothing
                btnEThumbsSetAsFanart.Enabled = False
            End If
            RenumberEThumbs()
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub DeleteEFanarts()
        Try
            Dim iIndex As Integer = EFanartsIndex

            If iIndex >= 0 Then
                Dim tPath As String = Me.EFanartsList.Item(iIndex).Path
                If Me.EFanartsList.Item(iIndex).Path.Substring(0, 1) = ":" Then
                    Master.currMovie.efList.RemoveAll(Function(Str) Str = tPath)
                    EFanartsList.Remove(EFanartsList.Item(iIndex))
                Else
                    efDeleteList.Add(Me.EFanartsList.Item(iIndex).Path)
                    EFanartsList.Remove(EFanartsList.Item(iIndex))
                End If
                pbEFanarts.Image = Nothing
                btnEFanartsSetAsFanart.Enabled = False
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub dlgEditMovie_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.Poster.Dispose()
        Me.Poster = Nothing

        Me.Fanart.Dispose()
        Me.Fanart = Nothing

        Me.EThumbsList.Clear()
        Me.EThumbsList = Nothing

        Me.EFanartsList.Clear()
        Me.EFanartsList = Nothing
    End Sub

    Private Sub dlgEditMovie_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.bwEThumbs.IsBusy Then Me.bwEThumbs.CancelAsync()
        While Me.bwEThumbs.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If Me.bwEFanarts.IsBusy Then Me.bwEFanarts.CancelAsync()
        While Me.bwEFanarts.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub dlgEditMovie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.SetUp()
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEditMovie, Nothing, Master.currMovie)
            Me.lvwActorSorter = New ListViewColumnSorter()
            Me.lvActors.ListViewItemSorter = Me.lvwActorSorter
            'Me.lvwEThumbsSorter = New ListViewColumnSorter() With {.SortByText = True, .Order = SortOrder.Ascending, .NumericSort = True}
            'Me.lvEThumbs.ListViewItemSorter = Me.lvwEThumbsSorter
            'Me.lvwEFanartsSorter = New ListViewColumnSorter() With {.SortByText = True, .Order = SortOrder.Ascending, .NumericSort = True}
            'Me.lvEFanarts.ListViewItemSorter = Me.lvwEFanartsSorter

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
            dFileInfoEdit.Show(False)

            Me.LoadGenres()
            Me.LoadRatings()
            Dim params As New List(Of Object)(New Object() {New Panel})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieFrameExtrator, params, Nothing, True)
            pnlFrameExtrator.Controls.Add(DirectCast(params(0), Panel))

            Me.FillInfo()

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub dlgEditMovie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub FillInfo(Optional ByVal DoAll As Boolean = True)
        Try
            With Me
                If String.IsNullOrEmpty(Master.currMovie.NfoPath) Then
                    .btnManual.Enabled = False
                End If

                Me.chkMark.Checked = Master.currMovie.IsMark
                'cocotus, 2013/02 Playcount/Watched state support added
                'When Edit Movie-Page is loaded, checkbox will be unchecked of playcount=0 or not set at all... 
                If Master.currMovie.Movie.PlayCount = "" Or Master.currMovie.Movie.PlayCount = "0" Then
                    Me.chkWatched.Checked = False
                Else
                    'Playcount <> Empty and not 0 -> Tag filled -> Checked!
                    Me.chkWatched.Checked = True
                End If
                'cocotus end
                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Title) Then
                    .txtTitle.Text = Master.currMovie.Movie.Title
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.OriginalTitle) Then
                    If Master.currMovie.Movie.OriginalTitle <> StringUtils.FilterTokens(Master.currMovie.Movie.Title) Then
                        .txtOriginalTitle.Text = Master.currMovie.Movie.OriginalTitle
                    End If
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.SortTitle) Then
                    If Master.currMovie.Movie.SortTitle <> StringUtils.FilterTokens(Master.currMovie.Movie.Title) Then
                        .txtSortTitle.Text = Master.currMovie.Movie.SortTitle
                    End If
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Tagline) Then
                    .txtTagline.Text = Master.currMovie.Movie.Tagline
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Year) Then
                    .mtxtYear.Text = Master.currMovie.Movie.Year
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Votes) Then
                    .txtVotes.Text = Master.currMovie.Movie.Votes
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Outline) Then
                    .txtOutline.Text = Master.currMovie.Movie.Outline
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Plot) Then
                    .txtPlot.Text = Master.currMovie.Movie.Plot
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Top250) Then
                    .txtTop250.Text = Master.currMovie.Movie.Top250
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Country) Then
                    .txtCountry.Text = Master.currMovie.Movie.Country
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Runtime) Then
                    .txtRuntime.Text = Master.currMovie.Movie.Runtime
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.ReleaseDate) Then
                    .txtReleaseDate.Text = Master.currMovie.Movie.ReleaseDate
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Director) Then
                    .txtDirector.Text = Master.currMovie.Movie.Director
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.OldCredits) Then
                    .txtCredits.Text = Master.currMovie.Movie.OldCredits
                End If


                If Not String.IsNullOrEmpty(Master.currMovie.FileSource) Then
                    .txtFileSource.Text = Master.currMovie.FileSource
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Certification) Then
                    If Not String.IsNullOrEmpty(Master.eSettings.CertificationLang) Then
                        Dim lCert() As String = Master.currMovie.Movie.Certification.Trim.Split(Convert.ToChar("/"))
                        Dim fCert = From eCert In lCert Where Regex.IsMatch(eCert, String.Concat(Regex.Escape(Master.eSettings.CertificationLang), "\:(.*?)"))
                        If fCert.Count > 0 Then
                            .txtCerts.Text = fCert(0).ToString.Trim
                        Else
                            .txtCerts.Text = Master.currMovie.Movie.Certification
                        End If
                    Else
                        .txtCerts.Text = Master.currMovie.Movie.Certification
                    End If
                End If

                Me.lblLocalTrailer.Visible = Not String.IsNullOrEmpty(Master.currMovie.TrailerPath)
                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Trailer) Then
                    .txtTrailer.Text = Master.currMovie.Movie.Trailer
                Else
                    If String.IsNullOrEmpty(Master.currMovie.TrailerPath) Then
                        .btnPlayTrailer.Enabled = False
                    End If
                End If

                .btnDLTrailer.Enabled = Master.eSettings.UpdaterTrailers AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Trailer)

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Studio) Then
                    .txtStudio.Text = Master.currMovie.Movie.Studio
                End If

                Me.SelectMPAA()

                For i As Integer = 0 To .lbGenre.Items.Count - 1
                    .lbGenre.SetItemChecked(i, False)
                Next
                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Genre) Then
                    Dim genreArray() As String
                    genreArray = Strings.Split(Master.currMovie.Movie.Genre, " / ")
                    For g As Integer = 0 To UBound(genreArray)
                        If .lbGenre.FindString(genreArray(g).Trim) > 0 Then
                            .lbGenre.SetItemChecked(.lbGenre.FindString(genreArray(g).Trim), True)
                        End If
                    Next

                    If .lbGenre.CheckedItems.Count = 0 Then
                        .lbGenre.SetItemChecked(0, True)
                    End If
                Else
                    .lbGenre.SetItemChecked(0, True)
                End If

                Dim lvItem As ListViewItem
                .lvActors.Items.Clear()
                For Each imdbAct As MediaContainers.Person In Master.currMovie.Movie.Actors
                    lvItem = .lvActors.Items.Add(imdbAct.Name)
                    lvItem.SubItems.Add(imdbAct.Role)
                    lvItem.SubItems.Add(imdbAct.Thumb)
                Next

                Dim tRating As Single = NumUtils.ConvertToSingle(Master.currMovie.Movie.Rating)
                .tmpRating = tRating.ToString
                .pbStar1.Tag = tRating
                .pbStar2.Tag = tRating
                .pbStar3.Tag = tRating
                .pbStar4.Tag = tRating
                .pbStar5.Tag = tRating
                If tRating > 0 Then .BuildStars(tRating)

                If DoAll Then

                    If Not Master.currMovie.isSingle Then
                        tcEditMovie.TabPages.Remove(tpFrameExtraction)
                        tcEditMovie.TabPages.Remove(tpEThumbs)
                        tcEditMovie.TabPages.Remove(tpEFanarts)
                    Else
                        Dim pExt As String = Path.GetExtension(Master.currMovie.Filename).ToLower
                        If pExt = ".rar" OrElse pExt = ".iso" OrElse pExt = ".img" OrElse _
                        pExt = ".bin" OrElse pExt = ".cue" OrElse pExt = ".dat" Then
                            tcEditMovie.TabPages.Remove(tpFrameExtraction)
                        Else
                            .bwEThumbs.WorkerSupportsCancellation = True
                            .bwEThumbs.RunWorkerAsync()
                            .bwEFanarts.WorkerSupportsCancellation = True
                            .bwEFanarts.RunWorkerAsync()
                        End If
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.FanartPath) AndAlso Master.currMovie.FanartPath.Substring(0, 1) = ":" Then
                        Fanart.FromWeb(Master.currMovie.FanartPath.Substring(1, Master.currMovie.FanartPath.Length - 1))
                    Else
                        Fanart.FromFile(Master.currMovie.FanartPath)
                    End If
                    If Not IsNothing(Fanart.Image) Then
                        .pbFanart.Image = Fanart.Image
                        .pbFanart.Tag = Fanart

                        .lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbFanart.Image.Width, .pbFanart.Image.Height)
                        .lblFanartSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.PosterPath) AndAlso Master.currMovie.PosterPath.Substring(0, 1) = ":" Then
                        Poster.FromWeb(Master.currMovie.PosterPath.Substring(1, Master.currMovie.PosterPath.Length - 1))
                    Else
                        Poster.FromFile(Master.currMovie.PosterPath)
                    End If
                    If Not IsNothing(Poster.Image) Then
                        .pbPoster.Image = Poster.Image
                        .pbPoster.Tag = Poster

                        .lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbPoster.Image.Width, .pbPoster.Image.Height)
                        .lblPosterSize.Visible = True
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Poster) Then
                        .btnSetPosterScrape.Enabled = False
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart) Then
                        .btnSetFanartScrape.Enabled = False
                    End If

                End If
            End With
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lbGenre_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lbGenre.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To lbGenre.Items.Count - 1
                Me.lbGenre.SetItemChecked(i, False)
            Next
        Else
            Me.lbGenre.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub LoadGenres()
        '//
        ' Read all the genres from the xml and load into the list
        '\\

        Me.lbGenre.Items.Add(Master.eLang.None)

        Me.lbGenre.Items.AddRange(APIXML.GetGenreList)
    End Sub

    Private Sub LoadRatings()
        '//
        ' Read all the ratings from the xml and load into the list
        '\\

        Me.lbMPAA.Items.Add(Master.eLang.None)

        Me.lbMPAA.Items.AddRange(APIXML.GetRatingList)
    End Sub

    Private Sub LoadEThumbs()
        Dim ET_tPath As String = String.Empty
        Dim ET_lFI As New List(Of String)
        Dim ET_i As Integer = 0

        '*************** XBMC Frodo & Eden settings ***************
        If Master.eSettings.UseFrodo OrElse Master.eSettings.UseEden Then
            If Master.eSettings.ExtrathumbsFrodo OrElse Master.eSettings.ExtrathumbsEden Then
                If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                    ET_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                    ET_tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrathumbs")
                Else
                    ET_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                End If
            End If
        End If

        Try
            If Directory.Exists(ET_tPath) Then
                Try
                    ET_lFI.AddRange(Directory.GetFiles(ET_tPath, "thumb*.jpg"))
                Catch ex As Exception
                    Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                End Try

                ' load local Extrathumbs
                If ET_lFI.Count > 0 Then
                    For Each thumb As String In ET_lFI
                        Dim ETImage As New Images
                        If Me.bwEThumbs.CancellationPending Then Return
                        If Not Me.etDeleteList.Contains(thumb) Then
                            ETImage.FromFile(thumb)
                            EThumbsList.Add(New ExtraImages With {.Image = ETImage, .Name = Path.GetFileName(thumb), .Index = ET_i, .Path = thumb})
                            ET_i += 1
                        End If
                    Next
                End If
            End If

            ' load scraped Extrathumbs
            If Not Master.currMovie.etList Is Nothing Then
                For Each thumb As String In Master.currMovie.etList
                    Dim ETImage As New Images
                    If Not String.IsNullOrEmpty(thumb) Then
                        ETImage.FromWeb(thumb.Substring(1, thumb.Length - 1))
                    End If
                    If Not IsNothing(ETImage.Image) Then
                        EThumbsList.Add(New ExtraImages With {.Image = ETImage, .Name = Path.GetFileName(thumb), .Index = ET_i, .Path = thumb})
                        ET_i += 1
                    End If
                Next
            End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        ET_lFI = Nothing
    End Sub

    Private Sub LoadEFanarts()
        Dim EF_tPath As String = String.Empty
        Dim EF_lFI As New List(Of String)
        Dim EF_i As Integer = 0

        '*************** XBMC Frodo & Eden settings ***************
        If Master.eSettings.UseFrodo OrElse Master.eSettings.UseEden Then
            If Master.eSettings.ExtrafanartsFrodo OrElse Master.eSettings.ExtrafanartsEden Then
                If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                    EF_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                    EF_tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrafanart")
                Else
                    EF_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                End If
            End If
        End If

        Try
            If Directory.Exists(EF_tPath) Then
                Try
                    EF_lFI.AddRange(Directory.GetFiles(EF_tPath, "*.jpg"))
                Catch ex As Exception
                    Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                End Try

                ' load local Extrafanarts
                If EF_lFI.Count > 0 Then
                    For Each fanart As String In EF_lFI
                        Dim EFImage As New Images
                        If Me.bwEFanarts.CancellationPending Then Return
                        If Not Me.efDeleteList.Contains(fanart) Then
                            EFImage.FromFile(fanart)
                            EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(fanart), .Index = EF_i, .Path = fanart})
                            EF_i += 1
                        End If
                    Next
                End If
            End If

            ' load scraped Extrafanarts
            If Not Master.currMovie.efList Is Nothing Then
                For Each fanart As String In Master.currMovie.efList
                    Dim EFImage As New Images
                    If Not String.IsNullOrEmpty(fanart) Then
                        EFImage.FromWeb(fanart.Substring(1, fanart.Length - 1))
                    End If
                    If Not IsNothing(EFImage.Image) Then
                        EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(fanart), .Index = EF_i, .Path = fanart})
                        EF_i += 1
                    End If
                Next
            End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        EF_lFI = Nothing
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvActors.DoubleClick
        EditActor()
    End Sub

    Private Sub lvActors_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvActors.KeyDown
        If e.KeyCode = Keys.Delete Then Me.DeleteActors()
    End Sub

    Private Sub lvEThumbs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then Me.DeleteEThumbs()
    End Sub

    Private Sub lvEFanart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then Me.DeleteEFanarts()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.SetInfo()

            Master.DB.SaveMovieToDB(Master.currMovie, False, False, True)

            Me.CleanUp()

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RefreshEThumbs()
        Try
            If Me.bwEThumbs.IsBusy Then Me.bwEThumbs.CancelAsync()
            While Me.bwEThumbs.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            Me.iETTop = 1 ' set first image top position back to 1
            Me.EThumbsList.Clear()
            While Me.pnlEThumbsBG.Controls.Count > 0
                Me.pnlEThumbsBG.Controls(0).Dispose()
            End While

            Me.bwEThumbs.WorkerSupportsCancellation = True
            Me.bwEThumbs.RunWorkerAsync()
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RenumberEThumbs()
        For i As Integer = 0 To EThumbsList.Count - 1
            EThumbsList.Item(i).Index = i + 1
        Next
    End Sub

    Private Sub SaveEThumbsList()
        Dim tPath As String = String.Empty
        Try
            '*************** XBMC Frodo & Eden settings ***************
            If Master.eSettings.UseFrodo OrElse Master.eSettings.UseEden Then
                If Master.eSettings.ExtrathumbsFrodo OrElse Master.eSettings.ExtrathumbsEden Then
                    If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                    ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrathumbs")
                    Else
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                    End If
                End If
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.currMovie.ClearEThumbs AndAlso Not hasClearedET Then
                    FileUtils.Delete.DeleteDirectory(tPath)
                    hasClearedET = True
                Else
                    'first delete the ones from the delete list
                    For Each del As String In etDeleteList
                        File.Delete(Path.Combine(tPath, del))
                    Next

                    'now name the rest something arbitrary so we don't get any conflicts
                    For Each lItem As ExtraImages In EThumbsList
                        If Not lItem.Path.Substring(0, 1) = ":" Then
                            FileSystem.Rename(lItem.Path, Path.Combine(Directory.GetParent(lItem.Path).FullName, String.Concat("temp", lItem.Name)))
                        End If
                    Next

                    'now rename them properly
                    For Each lItem As ExtraImages In EThumbsList
                        Dim etPath As String = lItem.Image.SaveAsExtraThumb(Master.currMovie)
                        If lItem.Index = 0 Then
                            Master.currMovie.EThumbsPath = etPath
                        End If
                    Next

                    'now remove the temp images
                    Dim tList As New List(Of String)

                    If Directory.Exists(tPath) Then
                        tList.AddRange(Directory.GetFiles(tPath, "tempthumb*.jpg"))
                        For Each tFile As String In tList
                            File.Delete(Path.Combine(tPath, tFile))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SaveEFanartsList()
        Dim tPath As String = String.Empty
        Try
            '*************** XBMC Frodo & Eden settings ***************
            If Master.eSettings.UseFrodo OrElse Master.eSettings.UseEden Then
                If Master.eSettings.ExtrafanartsFrodo OrElse Master.eSettings.ExtrafanartsEden Then
                    If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                    ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrafanart")
                    Else
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                    End If
                End If
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.currMovie.ClearEFanarts AndAlso Not hasClearedEF Then
                    FileUtils.Delete.DeleteDirectory(tPath)
                    hasClearedEF = True
                Else
                    'first delete the ones from the delete list
                    For Each del As String In efDeleteList
                        File.Delete(Path.Combine(tPath, del))
                    Next

                    'now name the rest something arbitrary so we don't get any conflicts
                    For Each lItem As ExtraImages In EFanartsList
                        If Not lItem.Path.Substring(0, 1) = ":" Then
                            FileSystem.Rename(lItem.Path, Path.Combine(Directory.GetParent(lItem.Path).FullName, String.Concat("temp", lItem.Name)))
                        End If
                    Next

                    'now rename them properly
                    For Each lItem As ExtraImages In EFanartsList
                        Dim efPath As String = lItem.Image.SaveAsExtraFanart(Master.currMovie, lItem.Name)
                        If lItem.Index = 0 Then
                            Master.currMovie.EFanartsPath = efPath
                        End If
                    Next

                    'now remove the temp images
                    Dim tList As New List(Of String)

                    If Directory.Exists(tPath) Then
                        tList.AddRange(Directory.GetFiles(tPath, "temp*.jpg"))
                        For Each tFile As String In tList
                            File.Delete(Path.Combine(tPath, tFile))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SelectMPAA()
        If Not String.IsNullOrEmpty(Master.currMovie.Movie.MPAA) Then
            Try
                If Master.eSettings.UseCertForMPAA AndAlso Not Master.eSettings.CertificationLang = "USA" AndAlso Not IsNothing(APIXML.RatingXML.Element("ratings").Element(Master.eSettings.CertificationLang.ToLower)) AndAlso APIXML.RatingXML.Element("ratings").Element(Master.eSettings.CertificationLang.ToLower).Descendants("movie").Count > 0 Then
                    If Master.eSettings.OnlyValueForCert Then
                        Dim sItem As String = String.Empty
                        For i As Integer = 0 To Me.lbMPAA.Items.Count - 1
                            sItem = Me.lbMPAA.Items(i).ToString
                            If sItem.Contains(":") AndAlso sItem.Split(Convert.ToChar(":"))(1) = Master.currMovie.Movie.MPAA Then
                                Me.lbMPAA.SelectedIndex = i
                                Me.lbMPAA.TopIndex = i
                                Exit For
                            End If
                        Next
                    Else
                        Dim l As Integer = Me.lbMPAA.FindString(Strings.Trim(Master.currMovie.Movie.MPAA))
                        Me.lbMPAA.SelectedIndex = l
                        Me.lbMPAA.TopIndex = l
                    End If

                    If Me.lbMPAA.SelectedItems.Count = 0 Then
                        Me.lbMPAA.SelectedIndex = 0
                        Me.lbMPAA.TopIndex = 0
                    End If

                    txtMPAADesc.Enabled = False
                ElseIf Me.lbMPAA.Items.Count >= 6 Then
                    Dim strMPAA As String = Master.currMovie.Movie.MPAA
                    If strMPAA.ToLower.StartsWith("rated g") Then
                        Me.lbMPAA.SelectedIndex = 1
                    ElseIf strMPAA.ToLower.StartsWith("rated pg-13") Then
                        Me.lbMPAA.SelectedIndex = 3
                    ElseIf strMPAA.ToLower.StartsWith("rated pg") Then
                        Me.lbMPAA.SelectedIndex = 2
                    ElseIf strMPAA.ToLower.StartsWith("rated r") Then
                        Me.lbMPAA.SelectedIndex = 4
                    ElseIf strMPAA.ToLower.StartsWith("rated nc-17") Then
                        Me.lbMPAA.SelectedIndex = 5
                    Else
                        Me.lbMPAA.SelectedIndex = 0
                    End If

                    If Me.lbMPAA.SelectedIndex > 0 AndAlso Not String.IsNullOrEmpty(strMPAA) Then
                        Dim strMPAADesc As String = strMPAA
                        strMPAADesc = Strings.Replace(strMPAADesc, "rated g", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated pg-13", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated pg", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated r", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated nc-17", String.Empty, 1, -1, CompareMethod.Text).Trim
                        txtMPAADesc.Text = strMPAADesc
                    End If
                End If

            Catch ex As Exception
                Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try
        Else
            Me.lbMPAA.SelectedIndex = 0
        End If
    End Sub

    Private Sub SetInfo()
        Try
            With Me

                Me.OK_Button.Enabled = False
                Me.Cancel_Button.Enabled = False
                Me.btnRescrape.Enabled = False
                Me.btnChangeMovie.Enabled = False

                Master.currMovie.IsMark = Me.chkMark.Checked

                If Not String.IsNullOrEmpty(.txtTitle.Text) Then
                    If Master.eSettings.DisplayYear AndAlso Not String.IsNullOrEmpty(.mtxtYear.Text.Trim) Then
                        Master.currMovie.ListTitle = String.Format("{0} ({1})", StringUtils.FilterTokens(.txtTitle.Text.Trim), .mtxtYear.Text.Trim)
                    Else
                        Master.currMovie.ListTitle = StringUtils.FilterTokens(.txtTitle.Text.Trim)
                    End If
                    Master.currMovie.Movie.Title = .txtTitle.Text.Trim
                End If

                If Not String.IsNullOrEmpty(.txtOriginalTitle.Text) Then
                    Master.currMovie.Movie.OriginalTitle = .txtOriginalTitle.Text.Trim
                Else
                    Master.currMovie.Movie.OriginalTitle = StringUtils.FilterTokens(.txtTitle.Text.Trim)
                End If

                If Not String.IsNullOrEmpty(.txtSortTitle.Text) Then
                    Master.currMovie.Movie.SortTitle = .txtSortTitle.Text.Trim
                Else
                    Master.currMovie.Movie.SortTitle = StringUtils.FilterTokens(.txtTitle.Text.Trim)
                End If

                Master.currMovie.Movie.Tagline = .txtTagline.Text.Trim
                Master.currMovie.Movie.Year = .mtxtYear.Text.Trim
                Master.currMovie.Movie.Votes = .txtVotes.Text.Trim
                Master.currMovie.Movie.Outline = .txtOutline.Text.Trim
                Master.currMovie.Movie.Plot = .txtPlot.Text.Trim
                Master.currMovie.Movie.Top250 = .txtTop250.Text.Trim
                Master.currMovie.Movie.Country = .txtCountry.Text.Trim
                Master.currMovie.Movie.Director = .txtDirector.Text.Trim

                Master.currMovie.Movie.Certification = .txtCerts.Text.Trim

                Master.currMovie.FileSource = .txtFileSource.Text.Trim

                If .lbMPAA.SelectedIndices.Count > 0 AndAlso Not .lbMPAA.SelectedIndex <= 0 Then
                    Master.currMovie.Movie.MPAA = String.Concat(If(Master.eSettings.UseCertForMPAA AndAlso Master.eSettings.OnlyValueForCert AndAlso .lbMPAA.SelectedItem.ToString.Contains(":"), .lbMPAA.SelectedItem.ToString.Split(Convert.ToChar(":"))(1), .lbMPAA.SelectedItem.ToString), " ", .txtMPAADesc.Text).Trim
                Else
                    If Master.eSettings.UseCertForMPAA AndAlso (Not Master.eSettings.CertificationLang = "USA" OrElse (Master.eSettings.CertificationLang = "USA" AndAlso .lbMPAA.SelectedIndex = 0)) Then
                        Dim lCert() As String = .txtCerts.Text.Trim.Split(Convert.ToChar("/"))
                        Dim fCert = From eCert In lCert Where Regex.IsMatch(eCert, String.Concat(Regex.Escape(Master.eSettings.CertificationLang), "\:(.*?)"))
                        If fCert.Count > 0 Then
                            Master.currMovie.Movie.MPAA = If(Master.eSettings.CertificationLang = "USA", StringUtils.USACertToMPAA(fCert(0).ToString.Trim), If(Master.eSettings.OnlyValueForCert, fCert(0).ToString.Trim.Split(Convert.ToChar(":"))(1), fCert(0).ToString.Trim))
                        Else
                            Master.currMovie.Movie.MPAA = String.Empty
                        End If
                    Else
                        Master.currMovie.Movie.MPAA = String.Empty
                    End If
                End If

                If Not .tmpRating.Trim = String.Empty AndAlso .tmpRating.Trim <> "0" Then
                    Master.currMovie.Movie.Rating = .tmpRating
                Else
                    Master.currMovie.Movie.Rating = String.Empty
                End If

                Master.currMovie.Movie.Runtime = .txtRuntime.Text.Trim
                Master.currMovie.Movie.ReleaseDate = .txtReleaseDate.Text.Trim
                Master.currMovie.Movie.OldCredits = .txtCredits.Text.Trim
                Master.currMovie.Movie.Studio = .txtStudio.Text.Trim

                If Master.eSettings.XBMCTrailerFormat Then
                    Master.currMovie.Movie.Trailer = Replace(.txtTrailer.Text.Trim, "http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                Else
                    Master.currMovie.Movie.Trailer = .txtTrailer.Text.Trim
                End If

                'cocotus, 2013/02 Playcount/Watched state support added
                'if watched-checkbox is checked -> save Playcount=1 in nfo
                If chkWatched.Checked Then
                    'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
                    If String.IsNullOrEmpty(Master.currMovie.Movie.PlayCount) Or Master.currMovie.Movie.PlayCount = "0" Then
                        Master.currMovie.Movie.PlayCount = "1"
                    End If
                Else
                    'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
                    If IsNumeric(Master.currMovie.Movie.PlayCount) AndAlso CInt(Master.currMovie.Movie.PlayCount) > 0 Then
                        Master.currMovie.Movie.PlayCount = ""
                    End If
                End If
                'cocotus End

                If .lbGenre.CheckedItems.Count > 0 Then

                    If .lbGenre.CheckedIndices.Contains(0) Then
                        Master.currMovie.Movie.Genre = String.Empty
                    Else
                        Dim strGenre As String = String.Empty
                        Dim isFirst As Boolean = True
                        Dim iChecked = From iCheck In .lbGenre.CheckedItems
                        strGenre = Strings.Join(iChecked.ToArray, " / ")
                        Master.currMovie.Movie.Genre = strGenre.Trim
                    End If
                End If

                Master.currMovie.Movie.Actors.Clear()

                If .lvActors.Items.Count > 0 Then
                    For Each lviActor As ListViewItem In .lvActors.Items
                        Dim addActor As New MediaContainers.Person
                        addActor.Name = lviActor.Text.Trim
                        addActor.Role = lviActor.SubItems(1).Text.Trim
                        addActor.Thumb = lviActor.SubItems(2).Text.Trim

                        Master.currMovie.Movie.Actors.Add(addActor)
                    Next
                End If

                If Master.currMovie.ClearFanart Then
                    .Fanart.DeleteFanart(Master.currMovie)
                End If

                If Master.currMovie.ClearPoster Then
                    .Poster.DeletePosters(Master.currMovie)
                End If

                If Not IsNothing(.Fanart.Image) Then
                    Dim fPath As String = .Fanart.SaveAsFanart(Master.currMovie)
                    Master.currMovie.FanartPath = fPath
                Else
                    .Fanart.DeleteFanart(Master.currMovie)
                    Master.currMovie.FanartPath = String.Empty
                End If

                If Not IsNothing(.Poster.Image) Then
                    Dim pPath As String = .Poster.SaveAsPoster(Master.currMovie)
                    Master.currMovie.PosterPath = pPath
                Else
                    .Poster.DeletePosters(Master.currMovie)
                    Master.currMovie.PosterPath = String.Empty
                End If

                If Master.GlobalScrapeMod.Actors AndAlso Master.eSettings.ScraperActorThumbs AndAlso (Master.eSettings.ActorThumbsFrodo OrElse Master.eSettings.ActorThumbsEden) Then
                    frmMain.tslLoading.Text = Master.eLang.GetString(568, "Generating Actor Thumbs:")
                    For Each act As MediaContainers.Person In Master.currMovie.Movie.Actors
                        Dim img As New Images
                        img.FromWeb(act.Thumb)
                        If Not IsNothing(img.Image) Then
                            img.SaveAsActorThumb(act, Directory.GetParent(Master.currMovie.Filename).FullName, Master.currMovie)
                        End If
                    Next
                End If

                If Not Master.eSettings.NoSaveImagesToNfo AndAlso pResults.Posters.Count > 0 Then Master.currMovie.Movie.Thumb = pResults.Posters
                If Not Master.eSettings.NoSaveImagesToNfo AndAlso fResults.Fanart.Thumb.Count > 0 Then Master.currMovie.Movie.Fanart = pResults.Fanart

                .SaveEThumbsList()
                '.TransferEThumbs()

                .SaveEFanartsList()
                '.TransferEFanarts()

            End With
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SetUp()
        Dim mTitle As String = Master.currMovie.Movie.Title
        Dim mPathPieces() As String = Master.currMovie.Filename.Split(Path.DirectorySeparatorChar)
        Dim mShortPath As String = Master.currMovie.Filename
        If Not String.IsNullOrEmpty(mShortPath) AndAlso FileUtils.Common.isVideoTS(mShortPath) Then
            mShortPath = String.Concat(Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 3), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 2), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 1))
        ElseIf Not String.IsNullOrEmpty(mShortPath) AndAlso FileUtils.Common.isBDRip(mShortPath) Then
            mShortPath = String.Concat(Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 4), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 3), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 2), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 1))
        Else
            mShortPath = String.Concat(Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 2), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 1))
        End If
        Dim sTitle As String = String.Concat(Master.eLang.GetString(25, "Edit Movie"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)), If(String.IsNullOrEmpty(mShortPath), String.Empty, String.Concat(" | ", mShortPath)))
        Me.Text = sTitle
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.Label2.Text = Master.eLang.GetString(224, "Edit the details for the selected movie.")
        Me.Label1.Text = Master.eLang.GetString(25, "Edit Movie")
        Me.tpDetails.Text = Master.eLang.GetString(26, "Details")
        Me.lblLocalTrailer.Text = Master.eLang.GetString(225, "Local Trailer Found")
        Me.lblStudio.Text = Master.eLang.GetString(226, "Studio:")
        Me.lblTrailer.Text = Master.eLang.GetString(227, "Trailer URL:")
        Me.lblReleaseDate.Text = Master.eLang.GetString(236, "Release Date:")
        Me.lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        Me.lblCerts.Text = Master.eLang.GetString(237, "Certification(s):")
        Me.lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        Me.lblMPAADesc.Text = Master.eLang.GetString(229, "MPAA Rating Description:")
        Me.btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        Me.lblActors.Text = Master.eLang.GetString(231, "Actors:")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colRole.Text = Master.eLang.GetString(233, "Role")
        Me.colThumb.Text = Master.eLang.GetString(234, "Thumb")
        Me.lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        Me.lblDirector.Text = Master.eLang.GetString(239, "Director:")
        Me.lblTop250.Text = Master.eLang.GetString(240, "Top 250:")
        Me.lblCountry.Text = String.Concat(Master.eLang.GetString(301, "Country"), ":")
        Me.lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        Me.lblOutline.Text = Master.eLang.GetString(242, "Plot Outline:")
        Me.lblTagline.Text = Master.eLang.GetString(243, "Tagline:")
        Me.lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        Me.lblRating.Text = Master.eLang.GetString(245, "Rating:")
        Me.lblYear.Text = Master.eLang.GetString(49, "Year:")
        Me.lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Me.tpPoster.Text = Master.eLang.GetString(148, "Poster")
        Me.btnRemovePoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnSetPosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.btnSetPoster.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.btnRemoveFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnSetFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetFanart.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.tpEThumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        Me.lbEThumbsQueue.Text = Master.eLang.GetString(253, "You have extrathumbs queued to be transferred to the movie directory.")
        Me.btnEThumbsTransfer.Text = Master.eLang.GetString(254, "Transfer Now")
        Me.btnEThumbsSetAsFanart.Text = Master.eLang.GetString(255, "Set As Fanart")
        Me.lbEFanartsQueue.Text = Master.eLang.GetString(974, "You have extratfanarts queued to be transferred to the movie directory.")
        Me.btnEFanartsTransfer.Text = Me.btnEThumbsTransfer.Text
        Me.btnEFanartsSetAsFanart.Text = Me.btnEThumbsSetAsFanart.Text
        Me.tpFrameExtraction.Text = Master.eLang.GetString(256, "Frame Extraction")
        Me.chkMark.Text = Master.eLang.GetString(23, "Mark")
        Me.btnRescrape.Text = Master.eLang.GetString(716, "Re-scrape")
        Me.btnChangeMovie.Text = Master.eLang.GetString(32, "Change Movie")
        Me.btnSetPosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetFanartDL.Text = Master.eLang.GetString(266, "Change Fanart (Download)")
        Me.Label6.Text = String.Concat(Master.eLang.GetString(642, "Sort Title"), ":")
        Me.lblOriginalTitle.Text = String.Concat(Master.eLang.GetString(302, "Original Title"), ":")
        Me.lblFileSource.Text = Master.eLang.GetString(824, "Video Source:")
        Me.tpMetaData.Text = Master.eLang.GetString(866, "Metadata")
    End Sub

    ' temporarily disabled
    'Private Sub tcEditMovie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcEditMovie.SelectedIndexChanged
    '    Try
    '        If tcEditMovie.SelectedIndex = 3 Then
    '            If File.Exists(String.Concat(Master.TempPath, Path.DirectorySeparatorChar, "extrathumbs", Path.DirectorySeparatorChar, "thumb1.jpg")) Then
    '                Me.pnlETQueue.Visible = True
    '            Else
    '                Me.pnlETQueue.Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    ' temporarily disabled
    'Private Sub TransferEThumbs()
    '    Try
    '        If Directory.Exists(Path.Combine(Master.TempPath, "extrathumbs")) Then
    '            Dim ePath As String = String.Empty
    '            If Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
    '                ePath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrathumbs")
    '                'ElseIf Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
    '                '    ePath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
    '            ElseIf Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
    '                ePath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName).FullName, "extrathumbs")
    '            ElseIf Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
    '                ePath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrathumbs")
    '            Else
    '                ePath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
    '            End If

    '            If Master.currMovie.ClearEThumbs AndAlso Not hasCleared Then
    '                FileUtils.Delete.DeleteDirectory(ePath)
    '                hasCleared = True
    '            End If

    '            Dim iMod As Integer = Functions.GetExtraModifier(ePath)
    '            Dim iVal As Integer = iMod + 1
    '            Dim hasET As Boolean = Not iMod = 0
    '            Dim fList As New List(Of String)

    '            Try
    '                fList.AddRange(Directory.GetFiles(Path.Combine(Master.TempPath, "extrathumbs"), "thumb*.jpg"))
    '            Catch
    '            End Try

    '            If fList.Count > 0 Then

    '                If Not hasET Then
    '                    Directory.CreateDirectory(ePath)
    '                End If

    '                For Each sFile As String In fList
    '                    FileUtils.Common.MoveFileWithStream(sFile, Path.Combine(ePath, String.Concat("thumb", iVal, ".jpg")))
    '                    iVal += 1
    '                Next
    '            End If

    '            Master.currMovie.EThumbsPath = ePath

    '            FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrathumbs"))
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    ' temporarily disabled
    'Private Sub TransferEFanarts()
    '    Try
    '        If Directory.Exists(Path.Combine(Master.TempPath, "extrafanart")) Then
    '            Dim ePath As String = String.Empty
    '            If Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
    '                ePath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrafanart")
    '                'ElseIf Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
    '                '    ePath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
    '            ElseIf Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
    '                ePath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName).FullName, "extrafanart")
    '            ElseIf Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
    '                ePath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrafanart")
    '            Else
    '                ePath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
    '            End If

    '            If Master.currMovie.ClearEFanarts AndAlso Not hasCleared Then
    '                FileUtils.Delete.DeleteDirectory(ePath)
    '                hasCleared = True
    '            End If

    '            Dim iMod As Integer = Functions.GetExtraModifier(ePath)
    '            Dim iVal As Integer = iMod + 1
    '            Dim hasET As Boolean = Not iMod = 0
    '            Dim fList As New List(Of String)

    '            Try
    '                fList.AddRange(Directory.GetFiles(Path.Combine(Master.TempPath, "extrafanart"), "fanart*.jpg"))
    '            Catch
    '            End Try

    '            If fList.Count > 0 Then

    '                If Not hasET Then
    '                    Directory.CreateDirectory(ePath)
    '                End If

    '                For Each sFile As String In fList
    '                    FileUtils.Common.MoveFileWithStream(sFile, Path.Combine(ePath, String.Concat("fanart", iVal, ".jpg")))
    '                    iVal += 1
    '                Next
    '            End If

    '            Master.currMovie.EFanartsPath = ePath

    '            FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrafanart"))
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    Private Sub txtThumbCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtThumbCount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.AcceptButton = Me.OK_Button
    End Sub

    Private Sub txtTrailer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTrailer.TextChanged
        If StringUtils.isValidURL(txtTrailer.Text) Then
            Me.btnPlayTrailer.Enabled = True
        Else
            Me.btnPlayTrailer.Enabled = False
        End If
    End Sub

    Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        If mType = Enums.ModuleEventType.MovieFrameExtrator Then
            Me.RefreshEThumbs()
        End If

    End Sub

    Private Sub txtOutline_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtOutline.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            Me.txtOutline.SelectAll()
        End If
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            Me.txtPlot.SelectAll()
        End If
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