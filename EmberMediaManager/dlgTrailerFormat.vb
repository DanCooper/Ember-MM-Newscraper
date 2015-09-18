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
Imports System.Text.RegularExpressions
Imports NLog

Public Class dlgTrailerFormat

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwParseTrailer As New System.ComponentModel.BackgroundWorker

    Private WithEvents YouTube As YouTube.Scraper
    Private WithEvents IMDb As IMDb.Scraper
    Private _trailerlinkscontainer As New TrailerLinksContainer
    Private _url As String
    Private _isYouTube As Boolean
    Private _isIMDb As Boolean

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal URL As String) As TrailerLinksContainer

        Me._url = URL

        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return _trailerlinkscontainer
        Else
            Return Nothing
        End If

    End Function

    Private Sub dlgTrailerFormat_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        prbStatus.Style = ProgressBarStyle.Marquee
        Application.DoEvents()

        Me.bwParseTrailer = New System.ComponentModel.BackgroundWorker
        Me.bwParseTrailer.WorkerReportsProgress = False
        Me.bwParseTrailer.WorkerSupportsCancellation = True
        Me.bwParseTrailer.RunWorkerAsync(New Arguments With {.bType = False, .Parameter = Me._url})
    End Sub


    Private Sub bwParseTrailer_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwParseTrailer.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            If EmberAPI.YouTube.UrlUtils.IsYouTubeURL(Me._url) Then
                _isIMDb = False
                _isYouTube = True
                YouTube.GetVideoLinks(Me._url)
                Args.bType = True
            ElseIf Regex.IsMatch(Me._url, "https?:\/\/.*imdb.*") Then
                _isIMDb = True
                _isYouTube = False
                IMDb.GetVideoLinks(Me._url)
                Args.bType = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        e.Result = Args.bType

        If Me.bwParseTrailer.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwParseTrailer_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwParseTrailer.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                If Me._isYouTube Then
                    If YouTube.YouTubeLinks.VideoLinks.Count > 0 Then
                        Me.pnlStatus.Visible = False

                        lbVideoFormats.DataSource = YouTube.YouTubeLinks.VideoLinks.ToList
                        lbVideoFormats.DisplayMember = "Description"
                        lbVideoFormats.ValueMember = "URL"

                        Me.lbVideoFormats.Enabled = True

                        If YouTube.YouTubeLinks.AudioLinks.Count > 0 Then
                            lbAudioFormats.DataSource = YouTube.YouTubeLinks.AudioLinks.ToList
                            lbAudioFormats.DisplayMember = "Description"
                            lbAudioFormats.ValueMember = "URL"

                            'If YouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Master.eSettings.MovieTrailerPrefQual).count > 0 Then
                            '    Me.lbFormats.SelectedIndex = YouTube.VideoLinks.IndexOfKey(Master.eSettings.MovieTrailerPrefQual)
                            'ElseIf Me.lbFormats.Items.Count = 1 Then
                            '    Me.lbFormats.SelectedIndex = 0
                            'End If

                            Me.lbAudioFormats.Enabled = True
                        End If

                        Dim prevQualLink As YouTube.VideoLinkItem
                        prevQualLink = YouTube.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Master.eSettings.MovieTrailerPrefVideoQual)
                        If prevQualLink IsNot Nothing Then
                            Me.lbVideoFormats.SelectedItem = prevQualLink
                        ElseIf Me.lbVideoFormats.Items.Count = 1 Then
                            Me.lbVideoFormats.SelectedIndex = 0
                        End If
                    Else
                        MessageBox.Show(Master.eLang.GetString(1170, "Trailer could not be parsed"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                ElseIf Me._isIMDb Then
                    If IMDb.VideoLinks.Count > 0 Then
                        Me.pnlStatus.Visible = False

                        lbVideoFormats.DataSource = IMDb.VideoLinks.Values.ToList
                        lbVideoFormats.DisplayMember = "Description"
                        lbVideoFormats.ValueMember = "URL"

                        If IMDb.VideoLinks.ContainsKey(Master.eSettings.MovieTrailerPrefVideoQual) Then
                            Me.lbVideoFormats.SelectedIndex = IMDb.VideoLinks.IndexOfKey(Master.eSettings.MovieTrailerPrefVideoQual)
                        ElseIf Me.lbVideoFormats.Items.Count = 1 Then
                            Me.lbVideoFormats.SelectedIndex = 0
                        End If
                        Me.lbVideoFormats.Enabled = True
                    Else
                        MessageBox.Show(Master.eLang.GetString(1170, "Trailer could not be parsed"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1170, "Trailer could not be parsed"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        Me.pnlStatus.Visible = False
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgTrailerFormat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.SetUp()

            lbAudioFormats.DataSource = Nothing
            lbVideoFormats.DataSource = Nothing

            YouTube = New YouTube.Scraper
            IMDb = New IMDb.Scraper

        Catch ex As Exception
            MessageBox.Show(Master.eLang.GetString(921, "The video format links could not be retrieved."), Master.eLang.GetString(72, "Error Retrieving Video Format Links"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub lbAudioFormats_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbAudioFormats.SelectedIndexChanged
        Try
            If _isYouTube Then
                Me._trailerlinkscontainer.AudioURL = DirectCast(lbAudioFormats.SelectedItem, YouTube.AudioLinkItem).URL
            ElseIf _isIMDb Then
                Me._trailerlinkscontainer.AudioURL = String.Empty
            End If

            If Me._trailerlinkscontainer.isDash AndAlso Me.lbVideoFormats.SelectedItems.Count > 0 AndAlso Me.lbAudioFormats.SelectedItems.Count > 0 Then
                Me.OK_Button.Enabled = True
            ElseIf Not Me._trailerlinkscontainer.isDash Then
                Me.OK_Button.Enabled = True
            Else
                Me.OK_Button.Enabled = False
            End If
        Catch
        End Try
    End Sub

    Private Sub lbVideoFormats_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbVideoFormats.SelectedIndexChanged
        Try
            If _isYouTube Then
                Me._trailerlinkscontainer.VideoURL = DirectCast(lbVideoFormats.SelectedItem, YouTube.VideoLinkItem).URL
                Me._trailerlinkscontainer.isDash = DirectCast(lbVideoFormats.SelectedItem, YouTube.VideoLinkItem).isDash
                If Me._trailerlinkscontainer.isDash Then
                    Me.lbAudioFormats.Enabled = True
                Else
                    Me.lbAudioFormats.Enabled = False
                End If
            ElseIf _isIMDb Then
                Me._trailerlinkscontainer.VideoURL = DirectCast(lbVideoFormats.SelectedItem, IMDb.VideoLinkItem).URL
                Me._trailerlinkscontainer.isDash = False
            End If

            If Me.lbVideoFormats.SelectedItems.Count > 0 Then
                If Me._trailerlinkscontainer.isDash Then
                    If lbAudioFormats.SelectedItems.Count > 0 Then
                        Me.OK_Button.Enabled = True
                    Else
                        Me.OK_Button.Enabled = False
                    End If
                Else
                    Me.OK_Button.Enabled = True
                End If
            Else
                Me.OK_Button.Enabled = False
            End If
        Catch
        End Try
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(923, "Select Format")
        Me.lblStatus.Text = Master.eLang.GetString(924, "Getting available formats...")
        Me.gbAudioFormats.Text = Master.eLang.GetString(1333, "Available Audio Formats")
        Me.gbVideoFormats.Text = Master.eLang.GetString(925, "Available Video Formats")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim bType As Boolean
        Dim Parameter As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class