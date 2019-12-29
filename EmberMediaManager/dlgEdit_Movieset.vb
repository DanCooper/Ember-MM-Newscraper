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
Imports NLog

Public Class dlgEdit_Movieset

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private sMovieID As String = String.Empty

    Private bsMovies As New BindingSource
    Private dtMovies As New DataTable
    Private KeyBuffer As String = String.Empty

    'filter movies
    Private bDoingSearch As Boolean = False
    Private FilterArray As New List(Of String)
    Private filMoviesInSet As String = String.Empty
    Private filMoviesToRemove As String = String.Empty
    Private filSearch As String = String.Empty
    Private currTextSearch As String = String.Empty
    Private prevTextSearch As String = String.Empty

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As Database.DBElement
        Get
            Return tmpDBElement
        End Get
    End Property

#End Region 'Properties

#Region "Dialog"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tmpDBElement, True) Then
            pbBanner.AllowDrop = True
            pbClearArt.AllowDrop = True
            pbClearLogo.AllowDrop = True
            pbDiscArt.AllowDrop = True
            pbFanart.AllowDrop = True
            pbKeyArt.AllowDrop = True
            pbLandscape.AllowDrop = True
            pbPoster.AllowDrop = True

            If Master.eSettings.Movie.DataSettings.Collection.SaveYAMJCompatible Then
                btnMovieDown.Visible = True
                btnMovieUp.Visible = True
                colOrdering.Width = 25
            End If

            Setup()

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            Data_Fill()
            MoviesInSetList_Fill()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Data_Save()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub DialogResult_Rescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
        DialogResult = DialogResult.Retry
    End Sub

    Private Sub Setup()
        Dim mTitle As String = tmpDBElement.Movieset.Title
        Text = String.Concat(Master.eLang.GetString(207, "Edit MovieSet"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK
        btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        btnSearchMovie.Text = Master.eLang.GetString(528, "Search Movie")
        chkLocked.Text = Master.eLang.GetString(43, "Locked")
        chkMarked.Text = Master.eLang.GetString(48, "Marked")
        gbMovieAssignment.Text = Master.eLang.GetString(241, "Movie Assignment")
        lblBanner.Text = Master.eLang.GetString(838, "Banner")
        lblClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        lblClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblCollectionID.Text = String.Concat(Master.eLang.GetString(1135, "Collection ID"), ":")
        lblDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        lblFanart.Text = Master.eLang.GetString(149, "Fanart")
        lblKeyArt.Text = Master.eLang.GetString(296, "KeyArt")
        lblLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        lblMovieSorting.Text = String.Concat(Master.eLang.GetString(665, "Movies sorted by"), ":")
        lblMoviesInDB.Text = Master.eLang.GetString(242, "Database")
        lblMoviesInMovieset.Text = Master.eLang.GetString(367, "Movies In SetS")
        lblPlot.Text = String.Concat(Master.eLang.GetString(65, "Plot"), ":")
        lblPoster.Text = Master.eLang.GetString(148, "Poster")
        lblTitle.Text = String.Concat(Master.eLang.GetString(21, "Title"), ":")
        lblTopDetails.Text = Master.eLang.GetString(1132, "Edit the details for the selected movieset.")
        lblTopTitle.Text = Master.eLang.GetString(207, "Edit MovieSet")

        cbMovieSorting.Items.Clear()
        cbMovieSorting.Items.AddRange(New String() {Master.eLang.GetString(278, "Year"), Master.eLang.GetString(21, "Title")})

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Description).ToArray)
    End Sub

    Public Overloads Function ShowDialog(ByVal dbElement As Database.DBElement) As DialogResult
        tmpDBElement = dbElement
        Return ShowDialog()
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Function ConvertControlToImageType(ByVal sender As System.Object) As Enums.ModifierType
        Select Case True
            Case sender Is btnRemoveBanner, sender Is btnDLBanner, sender Is btnLocalBanner, sender Is btnScrapeBanner, sender Is pbBanner, sender Is btnClipboardBanner
                Return Enums.ModifierType.MainBanner
            Case sender Is btnRemoveClearArt, sender Is btnDLCLearArt, sender Is btnLocalClearArt, sender Is btnScrapeClearArt, sender Is pbClearArt, sender Is btnClipboardClearArt
                Return Enums.ModifierType.MainClearArt
            Case sender Is btnRemoveClearLogo, sender Is btnDLClearLogo, sender Is btnLocalClearLogo, sender Is btnScrapeClearLogo, sender Is pbClearLogo, sender Is btnClipboardClearLogo
                Return Enums.ModifierType.MainClearLogo
            Case sender Is btnRemoveDiscArt, sender Is btnDLDiscArt, sender Is btnLocalDiscArt, sender Is btnScrapeDiscArt, sender Is pbDiscArt, sender Is btnClipboardDiscArt
                Return Enums.ModifierType.MainDiscArt
            Case sender Is btnRemoveFanart, sender Is btnDLFanart, sender Is btnLocalFanart, sender Is btnScrapeFanart, sender Is pbFanart, sender Is btnClipboardFanart
                Return Enums.ModifierType.MainFanart
            Case sender Is btnRemoveKeyArt, sender Is btnDLKeyArt, sender Is btnLocalKeyArt, sender Is btnScrapeKeyArt, sender Is pbKeyArt, sender Is btnClipboardKeyArt
                Return Enums.ModifierType.MainKeyArt
            Case sender Is btnRemoveLandscape, sender Is btnDLLandscape, sender Is btnLocalLandscape, sender Is btnScrapeLandscape, sender Is pbLandscape, sender Is btnClipboardLandscape
                Return Enums.ModifierType.MainLandscape
            Case sender Is btnRemovePoster, sender Is btnDLPoster, sender Is btnLocalPoster, sender Is btnScrapePoster, sender Is pbPoster, sender Is btnClipboardPoster
                Return Enums.ModifierType.MainPoster
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Controls_SetEnabled(ByVal isEnabled As Boolean)
        btnOK.Enabled = isEnabled
        btnSearchMovie.Enabled = isEnabled
        btnMovieAdd.Enabled = isEnabled
        btnMovieDown.Enabled = isEnabled
        btnMovieRemove.Enabled = isEnabled
        btnMovieUp.Enabled = isEnabled
        btnRescrape.Enabled = isEnabled
        dgvDatabaseList.Enabled = isEnabled
        lvMoviesInSet.Enabled = isEnabled
        Application.DoEvents()
    End Sub

    Private Sub Data_Fill(Optional ByVal DoAll As Boolean = True)
        'Database related part
        With tmpDBElement
            btnRescrape.Enabled = .Movieset.UniqueIDs.TMDbIdSpecified
            chkLocked.Checked = .IsLocked
            chkMarked.Checked = .IsMarked
            'Language
            If cbSourceLanguage.Items.Count > 0 Then
                Dim tLanguage As languageProperty = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .Language)
                If tLanguage IsNot Nothing Then
                    cbSourceLanguage.Text = tLanguage.Description
                Else
                    tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.Language_Main))
                    If tLanguage IsNot Nothing Then
                        cbSourceLanguage.Text = tLanguage.Description
                    Else
                        cbSourceLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                    End If
                End If
            End If
            'SortMethod
            cbMovieSorting.SelectedIndex = tmpDBElement.SortMethod
        End With

        'Information part
        With tmpDBElement.Movieset
            'CollectionID
            txtCollectionID.Text = .UniqueIDs.TMDbId
            'Plot
            txtPlot.Text = .Plot
            'Title
            txtTitle.Text = .Title
        End With

        If DoAll Then
            'Images and TabPages/Panels controll
            Dim bNeedTab_Images As Boolean

            With tmpDBElement.ImagesContainer
                'Load all images to MemoryStream and Bitmap
                tmpDBElement.LoadAllImages(True, True)

                'Banner
                If Master.eSettings.MovieBannerAnyEnabled Then
                    btnScrapeBanner.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'ClearArt
                If Master.eSettings.MovieClearArtAnyEnabled Then
                    btnScrapeClearArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearArt.Visible = False
                End If

                'ClearLogo
                If Master.eSettings.MovieClearLogoAnyEnabled Then
                    btnScrapeClearLogo.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainClearLogo)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlClearLogo.Visible = False
                End If

                'DiscArt
                If Master.eSettings.MovieDiscArtAnyEnabled Then
                    btnScrapeDiscArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    If .DiscArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainDiscArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlDiscArt.Visible = False
                End If

                'Fanart
                If Master.eSettings.MovieFanartAnyEnabled Then
                    btnScrapeFanart.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'KeyArt
                If Master.eSettings.MovieSetKeyArtAnyEnabled Then
                    btnScrapeKeyArt.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainKeyArt)
                    If .KeyArt.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainKeyArt)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlKeyArt.Visible = False
                End If

                'Landscape
                If Master.eSettings.MovieLandscapeAnyEnabled Then
                    btnScrapeLandscape.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If Master.eSettings.MoviePosterAnyEnabled Then
                    btnScrapePoster.Enabled = AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
                    If .Poster.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.MainPoster)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlPoster.Visible = False
                End If
            End With

            'Remove empty tab pages
            If Not bNeedTab_Images Then
                tcEdit.TabPages.Remove(tpImages)
            End If
        End If
    End Sub

    Private Sub Data_Save()
        btnOK.Enabled = False
        btnCancel.Enabled = False
        btnRescrape.Enabled = False

        'Database related part
        With tmpDBElement
            .IsLocked = chkLocked.Checked
            .IsMarked = chkMarked.Checked
            'Language
            If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
                .Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
                .Movieset.Language = .Language
            Else
                .Language = "en-US"
                .Movieset.Language = .Language
            End If
            'SortMethod
            .SortMethod = DirectCast(cbMovieSorting.SelectedIndex, Enums.SortMethod_MovieSet)
        End With

        'Information part
        With tmpDBElement.Movieset
            'Plot
            .Plot = txtPlot.Text.Trim
            'Title
            .Title = txtTitle.Text.Trim
        End With
    End Sub

    Private Sub DatabaseList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDatabaseList.CellClick
        btnMovieAdd.Enabled = dgvDatabaseList.SelectedRows.Count > 0
    End Sub

    Private Sub DatabaseList_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvDatabaseList.CellPainting
        If e.RowIndex >= 0 AndAlso Not dgvDatabaseList.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If
        'text
        If e.ColumnIndex = 3 AndAlso e.RowIndex >= 0 Then
            e.CellStyle.BackColor = Color.White
            e.CellStyle.ForeColor = Color.Black
            e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
            e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
        End If
    End Sub

    Private Sub DatabaseList_DoubleClick(sender As Object, e As EventArgs) Handles dgvDatabaseList.DoubleClick
        If dgvDatabaseList.SelectedRows.Count = 1 Then
            MoviesInSetList_Add()
        End If
    End Sub

    Private Sub DatabaseList_Fill()
        dgvDatabaseList.SuspendLayout()
        bsMovies.DataSource = Nothing
        dgvDatabaseList.DataSource = Nothing
        dtMovies = Master.DB.GetMovies()

        If dtMovies.Rows.Count > 0 Then
            bsMovies.DataSource = dtMovies
            dgvDatabaseList.DataSource = bsMovies
            For i As Integer = 0 To dgvDatabaseList.Columns.Count - 1
                dgvDatabaseList.Columns(i).Visible = False
            Next
            dgvDatabaseList.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).HeaderText = Master.eLang.GetString(21, "Title")
            dgvDatabaseList.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).MinimumWidth = 83
            dgvDatabaseList.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ReadOnly = True
            dgvDatabaseList.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Resizable = DataGridViewTriState.True
            dgvDatabaseList.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).SortMode = DataGridViewColumnSortMode.Automatic
            dgvDatabaseList.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToolTipText = Master.eLang.GetString(21, "Title")
            dgvDatabaseList.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Visible = True
            Controls_SetEnabled(True)
        End If
        dgvDatabaseList.ResumeLayout()
        Controls_SetEnabled(True)
        btnMovieAdd.Enabled = False
    End Sub

    Private Sub DatabaseList_KeyBuffer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrKeyBuffer.Tick
        tmrKeyBuffer.Enabled = False
        KeyBuffer = String.Empty
    End Sub

    Private Sub DatabaseList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvDatabaseList.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchMovies.Focus()
    End Sub

    Private Sub DatabaseList_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles dgvDatabaseList.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In dgvDatabaseList.Rows
                If drvRow.Cells(3).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    dgvDatabaseList.CurrentCell = drvRow.Cells(3)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub DatabaseList_RunFilter()
        If Visible Then
            dgvDatabaseList.ClearSelection()
            dgvDatabaseList.CurrentCell = Nothing
            If FilterArray.Count > 0 Then
                Dim FilterString As String = String.Empty
                FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray.ToArray, " AND ")
                bsMovies.Filter = FilterString
            Else
                bsMovies.RemoveFilter()
            End If
            txtSearchMovies.Focus()
        End If
    End Sub

    Private Sub DatabaseList_SearchMovies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchMovie.Click
        Controls_SetEnabled(False)
        Application.DoEvents()
        DatabaseList_Fill()
        DatabaseList_RunFilter()
    End Sub

    Private Sub DatabaseList_SearchMovies_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchMovies.KeyPress
        e.Handled = Not StringUtils.AlphaNumericOnly(e.KeyChar, True)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvDatabaseList.Focus()
        End If
    End Sub

    Private Sub DatabaseList_SearchMovies_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearchMovies.TextChanged
        currTextSearch = txtSearchMovies.Text
        tmrSearch_Wait.Enabled = False
        tmrSearch_Movies.Enabled = False
        tmrSearch_Wait.Enabled = True
    End Sub

    Private Sub DatabaseList_SearchMovies_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearch_Movies.Tick
        tmrSearch_Wait.Enabled = False
        tmrSearch_Movies.Enabled = False
        bDoingSearch = True
        If Not String.IsNullOrEmpty(txtSearchMovies.Text) Then
            FilterArray.Remove(filSearch)
            filSearch = String.Empty
            filSearch = String.Concat("title LIKE '%", txtSearchMovies.Text, "%'")
            FilterArray.Add(filSearch)
            DatabaseList_RunFilter()
        Else
            If Not String.IsNullOrEmpty(filSearch) Then
                FilterArray.Remove(filSearch)
                filSearch = String.Empty
            End If
            DatabaseList_RunFilter()
        End If
    End Sub

    Private Sub DatabaseList_SearchWait_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearch_Wait.Tick
        tmrSearch_Movies.Enabled = False
        If prevTextSearch = currTextSearch Then
            tmrSearch_Movies.Enabled = True
        Else
            prevTextSearch = currTextSearch
        End If
    End Sub

    Private Sub DatabaseList_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDatabaseList.SelectionChanged
        btnMovieAdd.Enabled = dgvDatabaseList.SelectedRows.Count > 0
    End Sub

    Private Sub DatabaseList_Sorted(ByVal sender As Object, ByVal e As EventArgs) Handles dgvDatabaseList.Sorted
        If dgvDatabaseList.RowCount > 0 Then
            dgvDatabaseList.CurrentCell = Nothing
            dgvDatabaseList.ClearSelection()
            dgvDatabaseList.Rows(0).Selected = True
            dgvDatabaseList.CurrentCell = dgvDatabaseList.Rows(0).Cells("listTitle")
        End If
    End Sub

    Private Sub Image_Clipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnClipboardBanner.Click,
        btnClipboardClearArt.Click,
        btnClipboardClearLogo.Click,
        btnClipboardDiscArt.Click,
        btnClipboardFanart.Click,
        btnClipboardKeyArt.Click,
        btnClipboardLandscape.Click,
        btnClipboardPoster.Click

        Dim lstImages = FileUtils.ClipboardHandler.GetImagesFromClipboard
        If lstImages.Count > 0 Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            tmpDBElement.ImagesContainer.SetImageByType(lstImages(0), eImageType)
            Image_LoadPictureBox(eImageType)
        End If
    End Sub

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbBanner.DoubleClick,
        pbClearArt.DoubleClick,
        pbClearLogo.DoubleClick,
        pbDiscArt.DoubleClick,
        pbFanart.DoubleClick,
        pbKeyArt.DoubleClick,
        pbLandscape.DoubleClick,
        pbPoster.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnDLBanner.Click,
        btnDLCLearArt.Click,
        btnDLClearLogo.Click,
        btnDLDiscArt.Click,
        btnDLFanart.Click,
        btnDLKeyArt.Click,
        btnDLLandscape.Click,
        btnDLPoster.Click
        Using dImgManual As New dlgImageManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                    tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                    Image_LoadPictureBox(eImageType)
                End If
            End If
        End Using
    End Sub

    Private Sub Image_DragDrop(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragDrop,
        pbClearArt.DragDrop,
        pbClearLogo.DragDrop,
        pbDiscArt.DragDrop,
        pbFanart.DragDrop,
        pbKeyArt.DragDrop,
        pbLandscape.DragDrop,
        pbPoster.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
            Image_LoadPictureBox(eImageType)
        End If
    End Sub

    Private Sub Image_DragEnter(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragEnter,
        pbClearArt.DragEnter,
        pbClearLogo.DragEnter,
        pbDiscArt.DragEnter,
        pbFanart.DragEnter,
        pbKeyArt.DragEnter,
        pbLandscape.DragEnter,
        pbPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Image_LoadPictureBox(ByVal ImageType As Enums.ModifierType)
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Select Case ImageType
            Case Enums.ModifierType.MainBanner
                lblSize = lblSizeBanner
                pbImage = pbBanner
            Case Enums.ModifierType.MainClearArt
                lblSize = lblClearArtSize
                pbImage = pbClearArt
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblSizeClearLogo
                pbImage = pbClearLogo
            Case Enums.ModifierType.MainDiscArt
                lblSize = lblDiscArtSize
                pbImage = pbDiscArt
            Case Enums.ModifierType.MainFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
            Case Enums.ModifierType.MainKeyArt
                lblSize = lblSizeKeyArt
                pbImage = pbKeyArt
            Case Enums.ModifierType.MainLandscape
                lblSize = lblSizeLandscape
                pbImage = pbLandscape
            Case Enums.ModifierType.MainPoster
                lblSize = lblSizePoster
                pbImage = pbPoster
            Case Else
                Return
        End Select
        Dim cImage = tmpDBElement.ImagesContainer.GetImageByType(ImageType)
        If cImage.ImageOriginal.Image IsNot Nothing Then
            pbImage.Image = cImage.ImageOriginal.Image
            pbImage.Tag = cImage
            lblSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbImage.Image.Width, pbImage.Image.Height)
            lblSize.Visible = True
        End If
    End Sub

    Private Sub Image_Local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnLocalBanner.Click,
        btnLocalClearArt.Click,
        btnLocalClearLogo.Click,
        btnLocalDiscArt.Click,
        btnLocalFanart.Click,
        btnLocalKeyArt.Click,
        btnLocalLandscape.Click,
        btnLocalPoster.Click
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.FileItem.MainPath.FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With
        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                Image_LoadPictureBox(eImageType)
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
            btnRemoveBanner.Click,
            btnRemoveClearArt.Click,
            btnRemoveClearLogo.Click,
            btnRemoveDiscArt.Click,
            btnRemoveFanart.Click,
            btnRemoveLandscape.Click,
            btnRemovePoster.Click, btnRemoveKeyArt.Click
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Select Case eImageType
            Case Enums.ModifierType.MainBanner
                lblSize = lblSizeBanner
                pbImage = pbBanner
            Case Enums.ModifierType.MainClearArt
                lblSize = lblClearArtSize
                pbImage = pbClearArt
            Case Enums.ModifierType.MainClearLogo
                lblSize = lblSizeClearLogo
                pbImage = pbClearLogo
            Case Enums.ModifierType.MainDiscArt
                lblSize = lblDiscArtSize
                pbImage = pbDiscArt
            Case Enums.ModifierType.MainFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
            Case Enums.ModifierType.MainLandscape
                lblSize = lblSizeLandscape
                pbImage = pbLandscape
            Case Enums.ModifierType.MainPoster
                lblSize = lblSizePoster
                pbImage = pbPoster
            Case Else
                Return
        End Select
        tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
        lblSize.Text = String.Empty
        lblSize.Visible = False
        pbImage.Image = Nothing
        pbImage.Tag = Nothing
    End Sub

    Private Sub Image_Scrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnScrapeBanner.Click,
        btnScrapeClearArt.Click,
        btnScrapeClearLogo.Click,
        btnScrapeDiscArt.Click,
        btnScrapeFanart.Click,
        btnScrapeLandscape.Click,
        btnScrapePoster.Click, btnScrapeKeyArt.Click
        Cursor = Cursors.WaitCursor
        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, eImageType, True)
        If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
            Dim iImageCount = 0
            Dim strNoImagesFound As String = String.Empty
            Select Case eImageType
                Case Enums.ModifierType.MainBanner
                    iImageCount = aContainer.MainBanners.Count
                    strNoImagesFound = Master.eLang.GetString(1363, "No Banners found")
                Case Enums.ModifierType.MainClearArt
                    iImageCount = aContainer.MainClearArts.Count
                    strNoImagesFound = Master.eLang.GetString(1102, "No ClearArts found")
                Case Enums.ModifierType.MainClearLogo
                    iImageCount = aContainer.MainClearLogos.Count
                    strNoImagesFound = Master.eLang.GetString(1103, "No ClearLogos found")
                Case Enums.ModifierType.MainDiscArt
                    iImageCount = aContainer.MainDiscArts.Count
                    strNoImagesFound = Master.eLang.GetString(1104, "No DiscArts found")
                Case Enums.ModifierType.MainFanart
                    iImageCount = aContainer.MainFanarts.Count
                    strNoImagesFound = Master.eLang.GetString(970, "No Fanarts found")
                Case Enums.ModifierType.MainKeyArt
                    iImageCount = aContainer.MainKeyArts.Count
                    strNoImagesFound = Master.eLang.GetString(855, "No KeyArts found")
                Case Enums.ModifierType.MainLandscape
                    iImageCount = aContainer.MainLandscapes.Count
                    strNoImagesFound = Master.eLang.GetString(1197, "No Landscapes found")
                Case Enums.ModifierType.MainPoster
                    iImageCount = aContainer.MainPosters.Count
                    strNoImagesFound = Master.eLang.GetString(972, "No Posters found")
            End Select
            If iImageCount > 0 Then
                Dim dlgImgS = New dlgImageSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.SetImageByType(dlgImgS.Result.ImagesContainer.GetImageByType(eImageType), eImageType)
                    If tmpDBElement.ImagesContainer.GetImageByType(eImageType) IsNot Nothing AndAlso
                        tmpDBElement.ImagesContainer.GetImageByType(eImageType).ImageOriginal.LoadFromMemoryStream() Then
                        Image_LoadPictureBox(eImageType)
                    Else
                        Image_Remove_Click(sender, e)
                    End If
                End If
            Else
                MessageBox.Show(strNoImagesFound, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub MoviesInSetList_Add() Handles btnMovieAdd.Click
        If dgvDatabaseList.SelectedRows.Count > 0 Then
            Controls_SetEnabled(False)
            For Each sRow As DataGridViewRow In dgvDatabaseList.SelectedRows
                Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(0).Value))
                If String.IsNullOrEmpty(txtCollectionID.Text) AndAlso tmpMovie.Movie.UniqueIDs.TMDbCollectionIdSpecified Then
                    If MessageBox.Show(String.Format(Master.eLang.GetString(1264, "Should the Collection ID of the movie ""{0}"" be used as ID for this Collection?"), tmpMovie.Movie.Title), Master.eLang.GetString(1263, "TMDB Collection ID found"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                        txtCollectionID.Text = tmpMovie.Movie.UniqueIDs.TMDbCollectionId
                        tmpDBElement.Movieset.UniqueIDs.TMDbId = tmpMovie.Movie.UniqueIDs.TMDbCollectionId
                    End If
                End If
                Dim newMovieInSet As New MediaContainers.MovieInSet With {.DBMovie = tmpMovie, .Order = tmpDBElement.MoviesInSet.Count}
                tmpDBElement.MoviesInSet.Add(newMovieInSet)
            Next
            dgvDatabaseList.ClearSelection()
            dgvDatabaseList.CurrentCell = Nothing
            MoviesInSetList_Fill()
            DatabaseList_RunFilter()
            Controls_SetEnabled(True)
            btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Sub MoviesInSetList_Down_Click(sender As Object, e As EventArgs) Handles btnMovieDown.Click
        If lvMoviesInSet.Items.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Item(0).Index < lvMoviesInSet.Items.Count Then
            Dim iIndex As Integer = lvMoviesInSet.SelectedItems.Item(0).Index
            tmpDBElement.MoviesInSet(iIndex).Order += 1
            tmpDBElement.MoviesInSet(iIndex + 1).Order -= 1
            MoviesInSetList_Fill()
            lvMoviesInSet.Items(iIndex + 1).Selected = True
            lvMoviesInSet.Focus()
        End If
    End Sub

    Private Sub MoviesInSetList_Fill()
        lvMoviesInSet.SuspendLayout()
        lvMoviesInSet.Items.Clear()
        If Master.eSettings.Movie.DataSettings.Collection.SaveYAMJCompatible Then
            tmpDBElement.MoviesInSet.Sort()
        End If
        Dim lvItem As ListViewItem
        lvMoviesInSet.Items.Clear()
        Dim iOrder As Integer = 0
        For Each tMovie As MediaContainers.MovieInSet In tmpDBElement.MoviesInSet
            tMovie.Order = iOrder
            lvItem = lvMoviesInSet.Items.Add(tMovie.DBMovie.ID.ToString)
            lvItem.SubItems.Add(tMovie.Order.ToString)
            lvItem.SubItems.Add(tMovie.ListTitle)
            iOrder += 1
        Next
        'filter out all movies that are already in movieset
        If tmpDBElement.MoviesInSetSpecified Then
            FilterArray.Remove(filMoviesInSet)
            Dim alMoviesInSet As New List(Of String)
            For Each movie As MediaContainers.MovieInSet In tmpDBElement.MoviesInSet
                alMoviesInSet.Add(movie.DBMovie.ID.ToString)
            Next
            For i As Integer = 0 To alMoviesInSet.Count - 1
                alMoviesInSet.Item(i) = String.Format("idMovie NOT = {0}", alMoviesInSet.Item(i))
            Next
            filMoviesInSet = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alMoviesInSet.ToArray, " AND "))
            FilterArray.Add(filMoviesInSet)
        Else
            If Not String.IsNullOrEmpty(filMoviesInSet) Then
                FilterArray.Remove(filMoviesInSet)
                filMoviesInSet = String.Empty
            End If
        End If
        lvMoviesInSet.ResumeLayout()
        btnMovieUp.Enabled = False
        btnMovieDown.Enabled = False
        btnMovieRemove.Enabled = False
    End Sub

    Private Function MoviesInSetList_FindMovie(ByVal lMov As MediaContainers.MovieInSet) As Boolean
        Return lMov.DBMovie.ID = CType(sMovieID, Long)
    End Function

    Private Sub MoviesInSetList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvMoviesInSet.KeyDown
        If e.KeyCode = Keys.Delete Then MoviesInSetList_Remove()
    End Sub

    Private Sub MoviesInSetList_Remove() Handles btnMovieRemove.Click
        Dim lMov As New MediaContainers.MovieInSet

        If lvMoviesInSet.SelectedItems.Count > 0 Then
            Controls_SetEnabled(False)
            While lvMoviesInSet.SelectedItems.Count > 0
                sMovieID = lvMoviesInSet.SelectedItems(0).SubItems(0).Text.ToString
                lMov = tmpDBElement.MoviesInSet.Find(AddressOf MoviesInSetList_FindMovie)
                If lMov IsNot Nothing Then
                    tmpDBElement.MoviesInSet.Remove(lMov)
                Else
                    lvMoviesInSet.Items.Remove(lvMoviesInSet.SelectedItems(0))
                End If
            End While

            MoviesInSetList_Fill()
            DatabaseList_RunFilter()
            Controls_SetEnabled(True)
            btnMovieUp.Enabled = False
            btnMovieDown.Enabled = False
            btnMovieRemove.Enabled = False
        End If
    End Sub

    Private Sub MoviesInSetList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvMoviesInSet.SelectedIndexChanged
        btnMovieDown.Enabled = lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems(0).Index < lvMoviesInSet.Items.Count - 1
        btnMovieRemove.Enabled = lvMoviesInSet.SelectedItems.Count > 0
        btnMovieUp.Enabled = lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems(0).Index > 0
    End Sub

    Private Sub MoviesInSetList_Up_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieUp.Click
        If lvMoviesInSet.Items.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Item(0).Index > 0 Then
            Dim iIndex As Integer = lvMoviesInSet.SelectedItems.Item(0).Index
            tmpDBElement.MoviesInSet(iIndex).Order -= 1
            tmpDBElement.MoviesInSet(iIndex - 1).Order += 1
            MoviesInSetList_Fill()
            lvMoviesInSet.Items(iIndex - 1).Selected = True
            lvMoviesInSet.Focus()
        End If
    End Sub

    Private Sub TextBox_NumericOnly(sender As Object, e As KeyPressEventArgs) Handles txtCollectionID.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_SelectAll(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

    Private Sub Title_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        btnOK.Enabled = Not String.IsNullOrEmpty(txtTitle.Text)
    End Sub

    Private Sub TMDbColID_Get_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetTMDbColID.Click
        Dim newColID As String = String.Empty

        If tmpDBElement.MoviesInSetSpecified Then
            If tmpDBElement.MoviesInSet.Item(0).DBMovie.Movie.UniqueIDs.TMDbCollectionIdSpecified Then
                newColID = tmpDBElement.MoviesInSet.Item(0).DBMovie.Movie.UniqueIDs.TMDbCollectionId
            Else
                newColID = AddonsManager.Instance.GetMovieCollectionID(tmpDBElement.MoviesInSet.Item(0).DBMovie.Movie.UniqueIDs.IMDbId)
            End If

            If Not String.IsNullOrEmpty(newColID) Then
                txtCollectionID.Text = newColID
                tmpDBElement.Movieset.UniqueIDs.TMDbId = newColID
            End If
        End If
    End Sub

#End Region 'Methods 

End Class