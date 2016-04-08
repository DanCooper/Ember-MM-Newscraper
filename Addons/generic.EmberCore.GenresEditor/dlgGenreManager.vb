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

Imports System.Windows.Forms
Imports System.IO
Imports EmberAPI
Imports NLog
Imports System.Drawing

Public Class dlgGenreManager

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Friend WithEvents bwCleanDatabase As New System.ComponentModel.BackgroundWorker
    Private tmpGenreXML As clsXMLGenres

#End Region 'Fields

#Region "Methods"

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        SetUp()
        Master.DB.LoadAllGenres()
        tmpGenreXML = CType(APIXML.GenreXML.CloneDeep, clsXMLGenres)
        PopulateGenres()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnGenreAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenreAdd.Click
        Dim strGenre As String = InputBox(Master.eLang.GetString(640, "Enter the new Genre"), Master.eLang.GetString(641, "New Genre"))
        If Not String.IsNullOrEmpty(strGenre) Then
            Dim gProperty As New genreProperty With {.isNew = False, .Name = strGenre}
            tmpGenreXML.Genres.Add(gProperty)
            Dim iRow As Integer = dgvGenres.Rows.Add(New Object() {False, strGenre})
            dgvGenres.Rows(iRow).Tag = gProperty
            dgvGenres.CurrentCell = dgvGenres.Rows(iRow).Cells(1)
        End If
    End Sub

    Private Sub btnGenreConfirm_Click(sender As Object, e As EventArgs) Handles btnGenreConfirm.Click
        If dgvGenres.SelectedRows.Count > 0 AndAlso dgvGenres.CurrentRow.Tag IsNot Nothing Then
            Dim gProperty As genreProperty = DirectCast(dgvGenres.CurrentRow.Tag, genreProperty)
            gProperty.isNew = False
            btnGenreConfirm.Enabled = False
            dgvGenres.Refresh()
        End If
    End Sub

    Private Sub btnGenreConfirmAll_Click(sender As Object, e As EventArgs) Handles btnGenreConfirmAll.Click
        For Each gProperty As genreProperty In tmpGenreXML.Genres.Where(Function(f) f.isNew)
            gProperty.isNew = False
        Next
        dgvGenres.Refresh()
    End Sub

    Private Sub btnGenreRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenreRemove.Click
        If MsgBox(Master.eLang.GetString(661, "This will remove the Genre from all Mappings. Are you sure?"), MsgBoxStyle.YesNo, Master.eLang.GetString(662, "Remove Genre")) = MsgBoxResult.Yes Then
            If dgvGenres.SelectedRows.Count > 0 Then 'AndAlso Not dgvLang.CurrentRow.Cells(1).Value Is Nothing Then
                Dim gProperty As genreProperty = DirectCast(dgvGenres.SelectedRows(0).Tag, genreProperty)
                tmpGenreXML.Genres.Remove(gProperty)
                For Each gMapping As genreMapping In tmpGenreXML.Mappings
                    If gMapping.MappedTo.Contains(gProperty.Name) Then
                        gMapping.MappedTo.Remove(gProperty.Name)
                    End If
                Next
                dgvGenres.Rows.Remove(dgvGenres.SelectedRows(0))
                cbMappingFilter.Items.Remove(gProperty.Name)
                dgvMappings.Refresh()
            End If
        End If
    End Sub

    Private Sub btnImageChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImageChange.Click
        Using fbImages As New OpenFileDialog
            fbImages.InitialDirectory = Path.Combine(Functions.AppPath, String.Format("Images{0}Genres{0}", Path.DirectorySeparatorChar))
            fbImages.Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            If fbImages.ShowDialog() = DialogResult.OK AndAlso Not String.IsNullOrEmpty(fbImages.FileName) Then
                Dim gProperty As genreProperty = DirectCast(dgvGenres.CurrentRow.Tag, genreProperty)
                gProperty.Image = Path.GetFileName(fbImages.FileName)
                pbImage.Load(Path.Combine(Functions.AppPath, String.Format("Images{0}Genres{0}{1}", Path.DirectorySeparatorChar, gProperty.Image)))
                btnImageRemove.Enabled = True
                dgvGenres.Refresh()
            End If
        End Using
    End Sub

    Private Sub btnImageRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImageRemove.Click
        Dim gProperty As genreProperty = DirectCast(dgvGenres.CurrentRow.Tag, genreProperty)
        gProperty.Image = String.Empty
        pbImage.Image = Nothing
        btnImageRemove.Enabled = False
        dgvGenres.Refresh()
    End Sub

    Private Sub btnMappingAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMappingAdd.Click
        Dim strSearchString As String = InputBox(Master.eLang.GetString(1000, "Enter the new Mapping"), Master.eLang.GetString(1005, "New Mapping"))
        If Not String.IsNullOrEmpty(strSearchString) Then
            Dim gMapping As New genreMapping With {.SearchString = strSearchString}
            tmpGenreXML.Mappings.Add(gMapping)
            Dim iRow As Integer = dgvMappings.Rows.Add(New Object() {strSearchString})
            dgvMappings.Rows(iRow).Tag = gMapping
            dgvMappings.CurrentCell = dgvMappings.Rows(iRow).Cells(0)
        End If
    End Sub

    Private Sub btnMappingConfirm_Click(sender As Object, e As EventArgs) Handles btnMappingConfirm.Click
        If dgvMappings.SelectedRows.Count > 0 AndAlso dgvMappings.CurrentRow.Tag IsNot Nothing Then
            Dim gMapping As genreMapping = DirectCast(dgvMappings.CurrentRow.Tag, genreMapping)
            gMapping.isNew = False
            btnMappingConfirm.Enabled = False
            dgvMappings.Refresh()
        End If
    End Sub

    Private Sub btnMappingConfirmAll_Click(sender As Object, e As EventArgs) Handles btnMappingConfirmAll.Click
        For Each gMapping As genreMapping In tmpGenreXML.Mappings.Where(Function(f) f.isNew)
            gMapping.isNew = False
        Next
        dgvMappings.Refresh()
    End Sub

    Private Sub btnMappingRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMappingRemove.Click
        If dgvMappings.SelectedCells.Count > 0 Then
            tmpGenreXML.Mappings.Remove(DirectCast(dgvMappings.SelectedRows(0).Tag, genreMapping))
            dgvMappings.Rows.RemoveAt(dgvMappings.SelectedCells(0).RowIndex)
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        btnOK.Enabled = False
        tspbStatus.Visible = True
        SaveChanges()
        bwCleanDatabase.WorkerReportsProgress = True
        bwCleanDatabase.RunWorkerAsync()
    End Sub

    Private Sub bwCleanDatabase_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCleanDatabase.RunWorkerCompleted
        tspbStatus.Visible = False
        MessageBox.Show(Master.eLang.GetString(362, "Done"))
        DialogResult = DialogResult.OK
    End Sub

    Private Sub bwCleanDatabase_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCleanDatabase.DoWork
        Master.DB.Cleanup_Genres()
    End Sub

    Private Sub bwCleanDatabase_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwCleanDatabase.ProgressChanged

    End Sub

    Private Sub cbMappingFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMappingFilter.SelectedIndexChanged
        PopulateMappings()
    End Sub

    Private Sub dgvGenres_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvGenres.CellPainting
        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvGenres.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        If e.RowIndex >= 0 Then

            Dim gProperty As genreProperty = DirectCast(dgvGenres.Rows(e.RowIndex).Tag, genreProperty)

            'text 
            If gProperty.isNew Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If

            'background
            If String.IsNullOrEmpty(gProperty.Image) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If
        End If
    End Sub

    Private Sub dgvGenres_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGenres.CellValueChanged
        If dgvGenres.SelectedRows.Count > 0 AndAlso dgvGenres.CurrentRow.Tag IsNot Nothing Then
            'checkbox part
            If dgvMappings.SelectedRows.Count > 0 AndAlso dgvMappings.CurrentRow.Tag IsNot Nothing Then
                Dim gMapping As genreMapping = DirectCast(dgvMappings.CurrentRow.Tag, genreMapping)
                If gMapping IsNot Nothing Then
                    'dgvGenres.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    If Convert.ToBoolean(dgvGenres.CurrentRow.Cells(0).Value) Then
                        If Not gMapping.MappedTo.Contains(dgvGenres.CurrentRow.Cells(1).Value.ToString) Then
                            gMapping.MappedTo.Add(dgvGenres.CurrentRow.Cells(1).Value.ToString)
                            gMapping.isNew = False
                        End If
                    Else
                        If gMapping.MappedTo.Contains(dgvGenres.CurrentRow.Cells(1).Value.ToString) Then
                            gMapping.MappedTo.Remove(dgvGenres.CurrentRow.Cells(1).Value.ToString)
                            gMapping.isNew = False
                        End If
                    End If
                    dgvMappings.Refresh()
                End If
            End If

            'genre name part (renaming Genre)
            Dim gProperty As genreProperty = DirectCast(dgvGenres.CurrentRow.Tag, genreProperty)
            Dim strNewName As String = dgvGenres.CurrentRow.Cells(1).Value.ToString
            If Not gProperty.Name = strNewName Then
                For Each tMapping As genreMapping In tmpGenreXML.Mappings.Where(Function(f) f.MappedTo.Contains(gProperty.Name))
                    While tMapping.MappedTo.Contains(gProperty.Name)
                        tMapping.MappedTo.Remove(gProperty.Name)
                    End While
                    tMapping.MappedTo.Add(strNewName)
                Next
                gProperty.Name = strNewName
                gProperty.isNew = False
            End If
        End If
    End Sub

    Private Sub dgvGenres_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvGenres.SelectionChanged
        If dgvGenres.SelectedRows.Count > 0 AndAlso Not dgvGenres.CurrentRow.Tag Is Nothing Then
            Dim gProperty As genreProperty = DirectCast(dgvGenres.CurrentRow.Tag, genreProperty)
            If Not String.IsNullOrEmpty(gProperty.Image) Then
                Dim imgPath As String = Path.Combine(Functions.AppPath, String.Format("Images{0}Genres{0}{1}", Path.DirectorySeparatorChar, gProperty.Image))
                If File.Exists(imgPath) Then
                    pbImage.Load(imgPath)
                End If
            Else
                pbImage.Image = Nothing
            End If
            btnGenreConfirm.Enabled = gProperty.isNew
            btnGenreRemove.Enabled = True
            btnImageChange.Enabled = True
            btnImageRemove.Enabled = Not String.IsNullOrEmpty(gProperty.Image)
        Else
            btnGenreRemove.Enabled = False
            btnImageChange.Enabled = False
            btnImageRemove.Enabled = False
            btnGenreConfirm.Enabled = False
            pbImage.Image = Nothing
        End If
    End Sub

    Private Sub dgvMappings_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvMappings.CellPainting
        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvMappings.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        If e.RowIndex >= 0 Then

            Dim gMapping As genreMapping = DirectCast(dgvMappings.Rows(e.RowIndex).Tag, genreMapping)

            'text 
            If gMapping.isNew Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If

            'background
            If gMapping.MappedTo.Count = 0 Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If
        End If
    End Sub

    Private Sub dgvMappings_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMappings.CurrentCellDirtyStateChanged
        Dim gMapping As genreMapping = DirectCast(dgvMappings.CurrentRow.Tag, genreMapping)
        If Not gMapping Is Nothing Then
            dgvMappings.CommitEdit(DataGridViewDataErrorContexts.Commit)
            gMapping.SearchString = dgvMappings.CurrentRow.Cells(0).Value.ToString
        End If
    End Sub

    Private Sub dgvMappings_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvMappings.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

    Private Sub dgvMappings_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMappings.SelectionChanged
        btnMappingRemove.Enabled = False
        dgvGenres.Columns(0).ReadOnly = True
        dgvGenres.ClearSelection()

        If dgvMappings.SelectedCells.Count > 0 Then
            Dim gMapping As genreMapping = DirectCast(dgvMappings.Rows(dgvMappings.SelectedCells(0).RowIndex).Tag, genreMapping)
            If Not gMapping Is Nothing Then
                GenreClearSelection()
                For Each dRow As DataGridViewRow In dgvGenres.Rows
                    For Each tGenre As String In gMapping.MappedTo
                        dRow.Cells(0).Value = If(dRow.Cells(1).Value.ToString = tGenre, True, dRow.Cells(0).Value)
                    Next
                Next
                btnMappingConfirm.Enabled = gMapping.isNew
                btnMappingRemove.Enabled = True
                dgvGenres.Columns(0).ReadOnly = False
            Else
                If dgvMappings.Rows.Count > 0 Then
                    dgvMappings.ClearSelection()
                End If
            End If
        Else
            btnMappingConfirm.Enabled = False
            btnMappingRemove.Enabled = False
            GenreClearSelection()
        End If
    End Sub

    Private Sub GenreClearSelection()
        For Each dRow As DataGridViewRow In dgvGenres.Rows
            dRow.Cells(0).Value = False
        Next
    End Sub

    Private Sub PopulateGenres()
        tmpGenreXML.Sort()
        cbMappingFilter.Items.Clear()
        dgvGenres.Rows.Clear()
        cbMappingFilter.Items.Add(Master.eLang.GetString(639, "< All >"))
        For Each gProperty As genreProperty In tmpGenreXML.Genres
            cbMappingFilter.Items.Add(gProperty.Name)
            Dim iRow As Integer = dgvGenres.Rows.Add(New Object() {False, gProperty.Name})
            dgvGenres.Rows(iRow).Tag = gProperty
        Next
        cbMappingFilter.SelectedIndex = 0
        dgvGenres.ClearSelection()
    End Sub

    Private Sub PopulateMappings()
        dgvMappings.Rows.Clear()
        GenreClearSelection()
        If cbMappingFilter.SelectedItem.ToString = Master.eLang.GetString(639, "< All >") Then
            For Each gMapping As genreMapping In tmpGenreXML.Mappings
                Dim iRow As Integer = dgvMappings.Rows.Add(New Object() {gMapping.SearchString})
                dgvMappings.Rows(iRow).Tag = gMapping
            Next
        Else
            btnMappingRemove.Enabled = False
            For Each gMapping As genreMapping In tmpGenreXML.Mappings.Where(Function(f) f.MappedTo.Contains(cbMappingFilter.SelectedItem.ToString))
                Dim i As Integer = dgvMappings.Rows.Add(New Object() {gMapping.SearchString})
                dgvMappings.Rows(i).Tag = gMapping
            Next
        End If
    End Sub

    Public Sub SaveChanges()
        APIXML.GenreXML = tmpGenreXML
        APIXML.GenreXML.Save()
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(782, "Genre Manager")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnGenreAdd.Text = Master.eLang.GetString(28, "Add")
        btnGenreConfirm.Text = Master.eLang.GetString(987, "Confirm")
        btnGenreConfirmAll.Text = Master.eLang.GetString(993, "Confirm All")
        btnGenreRemove.Text = Master.eLang.GetString(30, "Remove")
        btnImageChange.Text = Master.eLang.GetString(702, "Change")
        btnImageRemove.Text = Master.eLang.GetString(30, "Remove")
        btnMappingAdd.Text = Master.eLang.GetString(28, "Add")
        btnMappingConfirm.Text = Master.eLang.GetString(987, "Confirm")
        btnMappingConfirmAll.Text = Master.eLang.GetString(993, "Confirm All")
        btnMappingRemove.Text = Master.eLang.GetString(30, "Remove")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        dgvGenres.Columns(1).HeaderText = Master.eLang.GetString(20, "Genre")
        dgvMappings.Columns(0).HeaderText = Master.eLang.GetString(454, "Mapping")
        gbImage.Text = Master.eLang.GetString(497, "Images")
        lblMappingFilter.Text = Master.eLang.GetString(330, "Filter")
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
