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
Imports System.Xml.Serialization
Imports System.IO
Imports EmberAPI
Imports NLog

Public Class frmSettingsHolder

#Region "Events"

    Public Event ModuleSettingsChanged()

#End Region

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Private xmlGenres As clsXMLGenres

#End Region 'Fields

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        SetUp()
    End Sub

    Public Sub SaveChanges(ByVal strPath As String)
        If Not File.Exists(strPath) OrElse (Not CBool(File.GetAttributes(strPath) And FileAttributes.ReadOnly)) Then
            If File.Exists(strPath) Then
                Dim fAtt As FileAttributes = File.GetAttributes(strPath)
                Try
                    File.SetAttributes(strPath, FileAttributes.Normal)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            End If
            Using xmlSW As New StreamWriter(strPath)
                Dim xmlSer As New XmlSerializer(GetType(clsXMLGenres))
                xmlSer.Serialize(xmlSW, xmlGenres)
            End Using
        End If
    End Sub

    Private Sub GetLanguages()
        'cbLangs.Items.Clear()
        'dgvLang.Rows.Clear()
        'cbLangs.Items.Add(Master.eLang.GetString(639, "< All >"))
        'cbLangs.Items.AddRange(xmlGenres.listOfLanguages.ToArray)
        'cbLangs.SelectedIndex = 0
        'For Each s As String In xmlGenres.listOfLanguages
        '    dgvLang.Rows.Add(New Object() {False, s})
        'Next
        'dgvLang.ClearSelection()
    End Sub

    Private Sub PopulateGenres()
        dgvMapping.Rows.Clear()
        ClearGenreSelection()
        'If cbLangs.SelectedItem.ToString = Master.eLang.GetString(639, "< All >") Then
        For Each gMapping As genreMapping In xmlGenres.MappingTable
            Dim i As Integer = dgvMapping.Rows.Add(New Object() {gMapping.SearchString})
            dgvMapping.Rows(i).Tag = gMapping
            If gMapping.Mappings.Count = 0 Then
                dgvMapping.Rows(i).DefaultCellStyle.ForeColor = Drawing.Color.Red
            End If
        Next
        'Else
        '    btnMappingRemove.Enabled = False
        '    For Each gMapping As genreMapping In xmlGenres.MappingTable.Where(Function(f) f.Mapping.Contains(cbLangs.SelectedItem.ToString))
        '        Dim i As Integer = dgvMapping.Rows.Add(New Object() {gMapping.SearchString})
        '        dgvMapping.Rows(i).Tag = gMapping
        '    Next
        'End If
    End Sub

    Private Sub ClearGenreSelection()
        For Each r As DataGridViewRow In dgvGenres.Rows
            r.Cells(0).Value = False
        Next
    End Sub

    Private Sub dgvMapping_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMapping.CurrentCellDirtyStateChanged
        Dim g As genreMapping = DirectCast(dgvMapping.CurrentRow.Tag, genreMapping)
        If Not g Is Nothing Then
            dgvMapping.CommitEdit(DataGridViewDataErrorContexts.Commit)
            g.SearchString = dgvMapping.CurrentRow.Cells(0).Value.ToString
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub
    Private Sub dgvMapping_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvMapping.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

    Private Sub dgvMapping_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMapping.SelectionChanged
        btnMappingRemove.Enabled = False
        dgvGenres.Enabled = False

        dgvGenres.ClearSelection()
        If dgvMapping.SelectedCells.Count > 0 Then
            Dim g As genreMapping = DirectCast(dgvMapping.CurrentRow.Tag, genreMapping)
            If Not g Is Nothing Then
                ClearGenreSelection()
                For Each r As DataGridViewRow In dgvGenres.Rows
                    For Each s As String In g.Mappings
                        r.Cells(0).Value = If(r.Cells(1).Value.ToString = s, True, r.Cells(0).Value)
                    Next
                Next
                btnMappingRemove.Enabled = True
                dgvGenres.Enabled = True
                'If g.ic Is Nothing Then
                '    pbIcon.Image = Nothing
                'Else
                '    pbIcon.Load(Path.Combine(Functions.AppPath, String.Format("Images{0}Genres{0}{1}", Path.DirectorySeparatorChar, g.icon)))
                'End If

            Else
                If dgvGenres.Rows.Count > 0 Then
                    dgvGenres.ClearSelection()
                End If
            End If
        Else
            ClearGenreSelection()
        End If
    End Sub

    Private Sub dgvGenres_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvGenres.CurrentCellDirtyStateChanged
        Dim gMapping As genreMapping = DirectCast(dgvMapping.CurrentRow.Tag, genreMapping)
        If Not gMapping Is Nothing Then
            dgvGenres.CommitEdit(DataGridViewDataErrorContexts.Commit)
            RaiseEvent ModuleSettingsChanged()
            If Convert.ToBoolean(dgvGenres.CurrentRow.Cells(0).Value) Then
                If Not gMapping.SearchString.Contains(dgvGenres.CurrentRow.Cells(1).Value.ToString) Then
                    gMapping.Mappings.Add(dgvGenres.CurrentRow.Cells(1).Value.ToString)
                End If
            Else
                If gMapping.Mappings.Contains(dgvGenres.CurrentRow.Cells(1).Value.ToString) Then
                    gMapping.Mappings.Remove(dgvGenres.CurrentRow.Cells(1).Value.ToString)
                End If
            End If

            If gMapping.Mappings.Count > 0 AndAlso dgvGenres.CurrentRow.DefaultCellStyle.ForeColor = Drawing.Color.Red Then
                dgvGenres.CurrentRow.DefaultCellStyle.ForeColor = Drawing.Color.Black
            End If
        End If
    End Sub
    Private Sub dgvGenres_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvGenres.SelectionChanged
        If dgvGenres.SelectedRows.Count > 0 AndAlso Not dgvGenres.CurrentRow.Cells(1).Value Is Nothing Then
            btnGenreRemove.Enabled = True
        Else
            btnGenreRemove.Enabled = False
        End If
    End Sub

    Private Sub cbLangs_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        PopulateGenres()
    End Sub

    Private Sub btnMappingAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMappingAdd.Click
        'Dim g As New xGenre
        'Dim i As Integer = dgvGenres.Rows.Add(New Object() {String.Empty})
        'dgvGenres.Rows(i).Tag = g
        'xmlGenres.listOfGenres.Add(g)
        'dgvGenres.CurrentCell = dgvGenres.Rows(i).Cells(0)
        'pbIcon.Image = Nothing
        'dgvLang.ClearSelection()
        'ClearLangSelection()
        'dgvGenres.BeginEdit(True)
        'RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnGenreAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenreAdd.Click
        'Dim s As String = InputBox(Master.eLang.GetString(640, "Enter the new Language"), Master.eLang.GetString(641, "New Language"))
        'Dim i As Integer = dgvLang.Rows.Add(New Object() {False, s})
        'dgvLang.CurrentCell = dgvLang.Rows(i).Cells(1)
        'xmlGenres.listOfLanguages.Add(s)
        'dgvLang.BeginEdit(True)
        'RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub btnMappingRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMappingRemove.Click
        'If dgvGenres.SelectedCells.Count > 0 Then
        '    xmlGenres.listOfGenres.RemoveAt(dgvGenres.SelectedCells(0).RowIndex)
        '    dgvGenres.Rows.RemoveAt(dgvGenres.SelectedCells(0).RowIndex)
        '    RaiseEvent ModuleSettingsChanged()
        'End If
    End Sub

    Private Sub btnGenreRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenreRemove.Click
        'If MsgBox(Master.eLang.GetString(661, "This will remove the Language from all Genres. Are you sure?"), MsgBoxStyle.YesNo, Master.eLang.GetString(662, "Remove Language")) = MsgBoxResult.Yes Then
        '    If dgvLang.SelectedRows.Count > 0 AndAlso Not dgvLang.CurrentRow.Cells(1).Value Is Nothing Then
        '        Dim lang As String = dgvLang.SelectedRows(0).Cells(1).Value.ToString
        '        dgvLang.Rows.Remove(dgvLang.SelectedRows(0))
        '        xmlGenres.listOfLanguages.Remove(lang)
        '        GetLanguages()
        '        RaiseEvent ModuleSettingsChanged()
        '        For Each g As xGenre In xmlGenres.listOfGenres
        '            If g.Langs.Contains(lang) Then
        '                g.Langs.Remove(lang)
        '                If g.Langs.Count = 0 Then
        '                    For Each d As DataGridViewRow In dgvGenres.Rows
        '                        If d.Cells(0).Value.ToString = g.searchstring Then
        '                            d.DefaultCellStyle.ForeColor = Drawing.Color.Red
        '                        End If
        '                    Next
        '                End If
        '            End If
        '        Next
        '    End If
        'End If
    End Sub

    Private Sub btnImageChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImageChange.Click
        'Using fbImages As New OpenFileDialog
        '    fbImages.InitialDirectory = Path.Combine(Functions.AppPath, String.Format("Images{0}Genres{0}", Path.DirectorySeparatorChar))
        '    fbImages.Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
        '    fbImages.ShowDialog()
        '    Dim g As xGenre = DirectCast(dgvGenres.CurrentRow.Tag, xGenre)
        '    g.icon = Path.GetFileName(fbImages.FileName)
        '    pbIcon.Load(Path.Combine(Functions.AppPath, String.Format("Images{0}Genres{0}{1}", Path.DirectorySeparatorChar, g.icon)))
        '    RaiseEvent ModuleSettingsChanged()
        'End Using
    End Sub

    Private Sub SetUp()
        btnMappingAdd.Text = Master.eLang.GetString(28, "Add")
        btnGenreAdd.Text = Master.eLang.GetString(28, "Add")
        btnMappingRemove.Text = Master.eLang.GetString(30, "Remove")
        btnGenreRemove.Text = Master.eLang.GetString(30, "Remove")
        btnImageChange.Text = Master.eLang.GetString(702, "Change")
        GroupBox1.Text = Master.eLang.GetString(497, "Images")
        dgvMapping.Columns(0).HeaderText = Master.eLang.GetString(20, "Genre")
        dgvGenres.Columns(1).HeaderText = Master.eLang.GetString(707, "Languages")

        xmlGenres = APIXML.GenreXML
    End Sub

    Private Sub btnGenresLoadFromDB_Click(sender As Object, e As EventArgs) Handles btnGenresLoadFromDB.Click
        For Each tGenre In Master.DB.GetAllGenres
            xmlGenres.Genres.Add(New genreProperty With {.Name = tGenre})
            Dim mList As New List(Of String)
            mList.Add(tGenre)
            xmlGenres.MappingTable.Add(New genreMapping With {.Mappings = mList, .SearchString = tGenre})
        Next
        PopulateGenres()
    End Sub

#Region "Nested Types"

#End Region 'Nested Types

End Class
