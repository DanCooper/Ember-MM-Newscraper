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

Public Class dlgClearOrReplace

#Region "Fields"

    Private _Result As New TaskManager.TaskItem

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As TaskManager.TaskItem
        Get
            Return _Result
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

    Public Overloads Function ShowDialog(ByVal tContentType As Enums.ContentType) As DialogResult
        SetUp()

        _Result.ContentType = tContentType
        _Result.TaskType = Enums.TaskManagerType.DataFields_ClearOrReplace
        Select Case tContentType
            Case Enums.ContentType.Movie
                chkAired.Visible = False
                chkCreators.Visible = False
                chkGuestStars.Visible = False
                chkPremiered.Visible = False
                chkStatus.Visible = False
                chkTitle.Visible = False
                txtAired.Visible = False
                txtCreators.Visible = False
                txtPremiered.Visible = False
                txtStatus.Visible = False
            Case Enums.ContentType.MovieSet
                chkActors.Visible = False
                chkAired.Visible = False
                chkCertifications.Visible = False
                chkCountries.Visible = False
                chkCreators.Visible = False
                chkDirectors.Visible = False
                chkGenres.Visible = False
                chkGuestStars.Visible = False
                chkMPAA.Visible = False
                chkOriginalTitle.Visible = False
                chkOutline.Visible = False
                chkPremiered.Visible = False
                chkRating.Visible = False
                chkReleaseDate.Visible = False
                chkRuntime.Visible = False
                chkStatus.Visible = False
                chkStudios.Visible = False
                chkTagline.Visible = False
                chkTags.Visible = False
                chkTitle.Visible = False
                chkTop250.Visible = False
                chkTrailer.Visible = False
                chkUserRating.Visible = False
                chkVideoSource.Visible = False
                chkWriters.Visible = False
                chkYear.Visible = False
                txtAired.Visible = False
                txtCertifications.Visible = False
                txtCountries.Visible = False
                txtCreators.Visible = False
                txtDirectors.Visible = False
                txtGenres.Visible = False
                txtMPAA.Visible = False
                txtPremiered.Visible = False
                txtReleaseDate.Visible = False
                txtStatus.Visible = False
                txtStudios.Visible = False
                txtTagline.Visible = False
                txtTags.Visible = False
                txtUserRating.Visible = False
                txtVideoSource.Visible = False
                txtWriters.Visible = False
                txtYear.Visible = False
            Case Enums.ContentType.TVEpisode
                chkCertifications.Visible = False
                chkCountries.Visible = False
                chkCreators.Visible = False
                chkGenres.Visible = False
                chkMPAA.Visible = False
                chkOriginalTitle.Visible = False
                chkOutline.Visible = False
                chkPremiered.Visible = False
                chkReleaseDate.Visible = False
                chkStatus.Visible = False
                chkStudios.Visible = False
                chkTagline.Visible = False
                chkTags.Visible = False
                chkTitle.Visible = False
                chkTop250.Visible = False
                chkTrailer.Visible = False
                chkYear.Visible = False
                txtCertifications.Visible = False
                txtCountries.Visible = False
                txtCreators.Visible = False
                txtGenres.Visible = False
                txtMPAA.Visible = False
                txtPremiered.Visible = False
                txtReleaseDate.Visible = False
                txtStatus.Visible = False
                txtStudios.Visible = False
                txtTagline.Visible = False
                txtTags.Visible = False
                txtYear.Visible = False
            Case Enums.ContentType.TVSeason
                chkActors.Visible = False
                chkCertifications.Visible = False
                chkCountries.Visible = False
                chkCreators.Visible = False
                chkDirectors.Visible = False
                chkGenres.Visible = False
                chkGuestStars.Visible = False
                chkMPAA.Visible = False
                chkOriginalTitle.Visible = False
                chkOutline.Visible = False
                chkPremiered.Visible = False
                chkRating.Visible = False
                chkReleaseDate.Visible = False
                chkRuntime.Visible = False
                chkStatus.Visible = False
                chkStudios.Visible = False
                chkTagline.Visible = False
                chkTags.Visible = False
                chkTop250.Visible = False
                chkTrailer.Visible = False
                chkUserRating.Visible = False
                chkVideoSource.Visible = False
                chkWriters.Visible = False
                chkYear.Visible = False
                txtCertifications.Visible = False
                txtCountries.Visible = False
                txtCreators.Visible = False
                txtDirectors.Visible = False
                txtGenres.Visible = False
                txtMPAA.Visible = False
                txtPremiered.Visible = False
                txtReleaseDate.Visible = False
                txtStatus.Visible = False
                txtStudios.Visible = False
                txtTagline.Visible = False
                txtTags.Visible = False
                txtUserRating.Visible = False
                txtVideoSource.Visible = False
                txtWriters.Visible = False
                txtYear.Visible = False
            Case Enums.ContentType.TVShow
                chkAired.Visible = False
                chkGuestStars.Visible = False
                chkOutline.Visible = False
                chkReleaseDate.Visible = False
                chkTagline.Visible = False
                chkTitle.Visible = False
                chkTop250.Visible = False
                chkTrailer.Visible = False
                chkVideoSource.Visible = False
                chkWriters.Visible = False
                chkYear.Visible = False
                txtAired.Visible = False
                txtReleaseDate.Visible = False
                txtTagline.Visible = False
                txtVideoSource.Visible = False
                txtWriters.Visible = False
                txtYear.Visible = False
        End Select
        Return ShowDialog()
    End Function

    Private Sub dlgEditDataField_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim iHeight As Integer = Height
        Dim iWidth As Integer = Width
        AutoSize = False
        Height = iHeight
        Width = iWidth
    End Sub

    Private Sub dlgEditDataField_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUp()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        With _Result.ScrapeOptions
            Select Case _Result.ContentType
                Case Enums.ContentType.Movie
                    Dim nInfo = New MediaContainers.Movie
                    .bMainActors = chkActors.Checked
                    .bMainCertifications = chkCertifications.Checked
                    nInfo.Certifications = DoSplit(txtCertifications)
                    .bMainCountries = chkCountries.Checked
                    nInfo.Countries = DoSplit(txtCountries)
                    .bMainDirectors = chkDirectors.Checked
                    nInfo.Directors = DoSplit(txtDirectors)
                    .bMainGenres = chkGenres.Checked
                    nInfo.Genres = DoSplit(txtGenres)
                    .bMainMPAA = chkMPAA.Checked
                    nInfo.MPAA = txtMPAA.Text.Trim
                    .bMainOriginalTitle = chkOriginalTitle.Checked
                    .bMainOutline = chkOutline.Checked
                    .bMainPlot = chkPlot.Checked
                    .bMainRating = chkRating.Checked
                    .bMainRelease = chkReleaseDate.Checked
                    nInfo.ReleaseDate = txtReleaseDate.Text.Trim
                    .bMainRuntime = chkRuntime.Checked
                    .bMainStudios = chkStudios.Checked
                    nInfo.Studios = DoSplit(txtStudios)
                    .bMainTagline = chkTagline.Checked
                    nInfo.Tagline = txtTagline.Text.Trim
                    .bMainTags = chkTags.Checked
                    nInfo.Tags = DoSplit(txtTags)
                    .bMainTop250 = chkTop250.Checked
                    .bMainTrailer = chkTrailer.Checked
                    .bMainUserRating = chkUserRating.Checked
                    Dim uiUserRating As UInteger = 0
                    UInteger.TryParse(txtUserRating.Text.Trim, uiUserRating)
                    nInfo.UserRating = CInt(uiUserRating)
                    .bMainVideoSource = chkVideoSource.Checked
                    nInfo.VideoSource = txtVideoSource.Text.Trim
                    .bMainWriters = chkWriters.Checked
                    nInfo.Credits = DoSplit(txtWriters)
                    .bMainYear = chkYear.Checked
                    Dim uiYear As UInteger = 0
                    UInteger.TryParse(txtYear.Text.Trim, uiYear)
                    nInfo.Year = uiYear.ToString
                    _Result.GenericObject = nInfo
                Case Enums.ContentType.MovieSet
                    .bMainPlot = chkPlot.Checked
                Case Enums.ContentType.TVEpisode
                    Dim nInfo = New MediaContainers.EpisodeDetails
                    .bEpisodeActors = chkActors.Checked
                    .bEpisodeAired = chkAired.Checked
                    nInfo.Aired = txtAired.Text.Trim
                    .bEpisodeCredits = chkWriters.Checked
                    nInfo.Credits = DoSplit(txtWriters)
                    .bEpisodeDirectors = chkDirectors.Checked
                    nInfo.Directors = DoSplit(txtDirectors)
                    .bEpisodeGuestStars = chkGuestStars.Checked
                    .bEpisodePlot = chkPlot.Checked
                    .bEpisodeRating = chkRating.Checked
                    .bEpisodeRuntime = chkRuntime.Checked
                    .bEpisodeUserRating = chkUserRating.Checked
                    Dim uiUserRating As UInteger = 0
                    UInteger.TryParse(txtUserRating.Text.Trim, uiUserRating)
                    nInfo.UserRating = CInt(uiUserRating)
                    .bEpisodeVideoSource = chkVideoSource.Checked
                    nInfo.VideoSource = txtVideoSource.Text.Trim
                    _Result.GenericObject = nInfo
                Case Enums.ContentType.TVSeason
                    Dim nInfo = New MediaContainers.SeasonDetails
                    .bSeasonAired = chkAired.Checked
                    nInfo.Aired = txtAired.Text.Trim
                    .bSeasonPlot = chkPlot.Checked
                    .bSeasonTitle = chkTitle.Checked
                    _Result.GenericObject = nInfo
                Case Enums.ContentType.TVShow
                    Dim nInfo = New MediaContainers.TVShow
                    .bMainActors = chkActors.Checked
                    .bMainCertifications = chkCertifications.Checked
                    nInfo.Certifications = DoSplit(txtCertifications)
                    .bMainCountries = chkCountries.Checked
                    nInfo.Countries = DoSplit(txtCountries)
                    .bMainCreators = chkCreators.Checked
                    nInfo.Creators = DoSplit(txtCreators)
                    .bMainDirectors = chkDirectors.Checked
                    nInfo.Directors = DoSplit(txtDirectors)
                    .bMainGenres = chkGenres.Checked
                    nInfo.Genres = DoSplit(txtGenres)
                    .bMainMPAA = chkMPAA.Checked
                    nInfo.MPAA = txtMPAA.Text.Trim
                    .bMainOriginalTitle = chkOriginalTitle.Checked
                    .bMainPlot = chkPlot.Checked
                    .bMainPremiered = chkPremiered.Checked
                    nInfo.Premiered = txtPremiered.Text.Trim
                    .bMainRating = chkRating.Checked
                    .bMainRuntime = chkRuntime.Checked
                    .bMainStatus = chkStatus.Checked
                    nInfo.Status = txtStatus.Text.Trim
                    .bMainStudios = chkStudios.Checked
                    nInfo.Studios = DoSplit(txtStudios)
                    .bMainTags = chkTags.Checked
                    nInfo.Tags = DoSplit(txtTags)
                    .bMainUserRating = chkUserRating.Checked
                    Dim uiUserRating As UInteger = 0
                    UInteger.TryParse(txtUserRating.Text.Trim, uiUserRating)
                    nInfo.UserRating = CInt(uiUserRating)
                    _Result.GenericObject = nInfo
            End Select
        End With
        DialogResult = DialogResult.OK
    End Sub

    Private Sub chkActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkActors.CheckedChanged
        lblActors.Visible = chkActors.Checked
    End Sub

    Private Sub chkAired_CheckedChanged(sender As Object, e As EventArgs) Handles chkAired.CheckedChanged
        txtAired.Enabled = chkAired.Checked
    End Sub

    Private Sub chkCertifications_CheckedChanged(sender As Object, e As EventArgs) Handles chkCertifications.CheckedChanged
        txtCertifications.Enabled = chkCertifications.Checked
    End Sub

    Private Sub chkCountries_CheckedChanged(sender As Object, e As EventArgs) Handles chkCountries.CheckedChanged
        txtCountries.Enabled = chkCountries.Checked
    End Sub

    Private Sub chkCreators_CheckedChanged(sender As Object, e As EventArgs) Handles chkCreators.CheckedChanged
        txtCreators.Enabled = chkCreators.Checked
    End Sub

    Private Sub chkDirectors_CheckedChanged(sender As Object, e As EventArgs) Handles chkDirectors.CheckedChanged
        txtDirectors.Enabled = chkDirectors.Checked
    End Sub

    Private Sub chkGenres_CheckedChanged(sender As Object, e As EventArgs) Handles chkGenres.CheckedChanged
        txtGenres.Enabled = chkGenres.Checked
    End Sub

    Private Sub chkGuestStars_CheckedChanged(sender As Object, e As EventArgs) Handles chkGuestStars.CheckedChanged
        lblGuestStars.Visible = chkGuestStars.Checked
    End Sub

    Private Sub chkMPAA_CheckedChanged(sender As Object, e As EventArgs) Handles chkMPAA.CheckedChanged
        txtMPAA.Enabled = chkMPAA.Checked
    End Sub

    Private Sub chkOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkOriginalTitle.CheckedChanged
        lblOriginalTitle.Visible = chkOriginalTitle.Checked
    End Sub

    Private Sub chkOutline_CheckedChanged(sender As Object, e As EventArgs) Handles chkOutline.CheckedChanged
        lblOutline.Visible = chkOutline.Checked
    End Sub

    Private Sub chkPlot_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlot.CheckedChanged
        lblPlot.Visible = chkPlot.Checked
    End Sub

    Private Sub chkPremiered_CheckedChanged(sender As Object, e As EventArgs) Handles chkPremiered.CheckedChanged
        txtPremiered.Enabled = chkPremiered.Checked
    End Sub

    Private Sub chkRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkRating.CheckedChanged
        lblRating.Visible = chkRating.Checked
    End Sub

    Private Sub chkReleaseDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkReleaseDate.CheckedChanged
        txtReleaseDate.Enabled = chkReleaseDate.Checked
    End Sub

    Private Sub chkRuntime_CheckedChanged(sender As Object, e As EventArgs) Handles chkRuntime.CheckedChanged
        lblRuntime.Visible = chkRuntime.Checked
    End Sub

    Private Sub chkStatus_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatus.CheckedChanged
        txtStatus.Enabled = chkStatus.Checked
    End Sub

    Private Sub chkStudios_CheckedChanged(sender As Object, e As EventArgs) Handles chkStudios.CheckedChanged
        txtStudios.Enabled = chkStudios.Checked
    End Sub

    Private Sub chkTagline_CheckedChanged(sender As Object, e As EventArgs) Handles chkTagline.CheckedChanged
        txtTagline.Enabled = chkTagline.Checked
    End Sub

    Private Sub chkTags_CheckedChanged(sender As Object, e As EventArgs) Handles chkTags.CheckedChanged
        txtTags.Enabled = chkTags.Checked
    End Sub

    Private Sub chkTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkTitle.CheckedChanged
        lblTitle.Visible = chkTitle.Checked
    End Sub

    Private Sub chkTop250_CheckedChanged(sender As Object, e As EventArgs) Handles chkTop250.CheckedChanged
        lblTop250.Visible = chkTop250.Checked
    End Sub

    Private Sub chkTrailer_CheckedChanged(sender As Object, e As EventArgs) Handles chkTrailer.CheckedChanged
        lblTrailer.Visible = chkTrailer.Checked
    End Sub

    Private Sub chkUserRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkUserRating.CheckedChanged
        txtUserRating.Enabled = chkUserRating.Checked
    End Sub

    Private Sub chkVideoSource_CheckedChanged(sender As Object, e As EventArgs) Handles chkVideoSource.CheckedChanged
        txtVideoSource.Enabled = chkVideoSource.Checked
    End Sub

    Private Sub chkWriters_CheckedChanged(sender As Object, e As EventArgs) Handles chkWriters.CheckedChanged
        txtWriters.Enabled = chkWriters.Checked
    End Sub

    Private Sub chkYear_CheckedChanged(sender As Object, e As EventArgs) Handles chkYear.CheckedChanged
        txtYear.Enabled = chkYear.Checked
    End Sub

    Private Function DoSplit(tTextbox As TextBox) As List(Of String)
        Dim nList = tTextbox.Text.Trim.Split(",".ToCharArray, StringSplitOptions.RemoveEmptyEntries).Distinct.ToList
        'remove empty entries that has not been removed with "StringSplitOptions.RemoveEmptyEntries"
        For i As Integer = 0 To nList.Count - 1
            nList(i) = nList(i).Trim()
        Next
        nList.Remove(String.Empty)
        Return nList
    End Function

    Private Sub SetUp()
        Text = Master.eLang.GetString(1087, "Clear or Replace Data Fields")
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK

        chkActors.Text = Master.eLang.GetString(231, "Actors")
        chkAired.Text = Master.eLang.GetString(728, "Aired")
        chkCertifications.Text = Master.eLang.GetString(56, "Certifications")
        chkCountries.Text = Master.eLang.GetString(237, "Countries")
        chkCreators.Text = Master.eLang.GetString(744, "Creators")
        chkDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkGenres.Text = Master.eLang.GetString(725, "Genres")
        chkGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        chkMPAA.Text = Master.eLang.GetString(401, "MPAA")
        chkOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        chkPlot.Text = Master.eLang.GetString(65, "Plot")
        chkPremiered.Text = Master.eLang.GetString(724, "Premiered")
        chkRating.Text = String.Format("{0} / {1}", Master.eLang.GetString(400, "Rating"), Master.eLang.GetString(244, "Votes"))
        chkReleaseDate.Text = Master.eLang.GetString(57, "Release Date")
        chkRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkStatus.Text = Master.eLang.GetString(215, "Status")
        chkStudios.Text = Master.eLang.GetString(226, "Studios")
        chkTagline.Text = Master.eLang.GetString(397, "Tagline")
        chkTags.Text = Master.eLang.GetString(243, "Tags")
        chkTitle.Text = Master.eLang.GetString(21, "Title")
        chkTop250.Text = Master.eLang.GetString(591, "Top 250")
        chkTrailer.Text = Master.eLang.GetString(151, "Trailer")
        chkUserRating.Text = Master.eLang.GetString(1467, "User Rating")
        chkVideoSource.Text = Master.eLang.GetString(824, "Video Source")
        chkWriters.Text = Master.eLang.GetString(394, "Credits (Writers)")
        chkYear.Text = Master.eLang.GetString(278, "Year")


        Dim strCommaSeparated As String = Master.eLang.GetString(882, "comma separated")
        txtCertifications.WatermarkText = strCommaSeparated
        txtCountries.WatermarkText = strCommaSeparated
        txtCreators.WatermarkText = strCommaSeparated
        txtDirectors.WatermarkText = strCommaSeparated
        txtGenres.WatermarkText = strCommaSeparated
        txtStudios.WatermarkText = strCommaSeparated
        txtTags.WatermarkText = strCommaSeparated
        txtWriters.WatermarkText = strCommaSeparated

        Dim strWillBeCleared As String = Master.eLang.GetString(49, "will be cleared")
        lblActors.Text = strWillBeCleared
        lblGuestStars.Text = strWillBeCleared
        lblOriginalTitle.Text = strWillBeCleared
        lblOutline.Text = strWillBeCleared
        lblPlot.Text = strWillBeCleared
        lblRating.Text = strWillBeCleared
        lblRuntime.Text = strWillBeCleared
        lblTitle.Text = strWillBeCleared
        lblTop250.Text = strWillBeCleared
        lblTrailer.Text = strWillBeCleared
    End Sub

#End Region 'Methods

End Class