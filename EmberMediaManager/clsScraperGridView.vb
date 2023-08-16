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

Public Class ScraperGridView
    Inherits DataGridView

    Public Sub New()
        MyBase.New()
        StartProcess()
    End Sub

    Private Sub StartProcess()
        AllowUserToAddRows = False
        AllowUserToDeleteRows = False
        AllowUserToOrderColumns = True
        AllowUserToResizeColumns = False
        AllowUserToResizeRows = False
        Anchor = AnchorStyles.Left
        BorderStyle = BorderStyle.None
        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        ColumnHeadersVisible = False
        Height = 23
        Margin = New Padding(0)
        MultiSelect = False
        RowHeadersVisible = False
        ScrollBars = ScrollBars.None
        Width = 100
    End Sub

    Public Sub AddScrapers(ByVal scraperList As List(Of ScraperProperties))
        Dim lstScrapers As New List(Of String)
        For Each nScraper In scraperList
            Dim nButtonColumn As New DataGridViewButtonColumn With {
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                .FlatStyle = FlatStyle.Popup,
                .Name = nScraper.AssemblyName,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                .ForeColor = Color.White,
                .SelectionForeColor = Color.White
            }
            }
            lstScrapers.Add(nScraper.AssemblyName)
            Columns.Add(nButtonColumn)
        Next

        If lstScrapers.Count > 0 Then
            Rows.Add(lstScrapers.ToArray)
        End If

        For i As Integer = 0 To lstScrapers.Count - 1
            Rows(0).Cells(i).Tag = False
        Next
    End Sub

    Public Sub AddSearchEngines(ByVal searchEngineList As List(Of SearchEngineProperties))
        Dim lstScrapers As New List(Of String)
        For Each nScraper In searchEngineList
            Dim nButtonColumn As New DataGridViewButtonColumn With {
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                .FlatStyle = FlatStyle.Popup,
                .Name = nScraper.AssemblyName,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                .ForeColor = Color.White,
                .SelectionForeColor = Color.White
            }
            }
            lstScrapers.Add(nScraper.AssemblyName)
            Columns.Add(nButtonColumn)
        Next

        If lstScrapers.Count > 0 Then
            Rows.Add(lstScrapers.ToArray)
        End If

        For i As Integer = 0 To lstScrapers.Count - 1
            Rows(0).Cells(i).Tag = False
        Next
    End Sub

    Public Sub AddSettings(ByVal list As List(Of String))
        For i As Integer = 0 To list.Count - 1
            Dim column As DataGridViewColumn = Columns(list(i))
            If column IsNot Nothing Then
                column.DisplayIndex = i
                Rows(0).Cells(column.Index).Tag = True
            End If
        Next
    End Sub

    Public Function Save() As List(Of String)
        Dim nList As New List(Of String)
        For Each datarow As DataGridViewRow In Rows
            For Each item As DataGridViewColumn In Columns.OfType(Of DataGridViewColumn)().OrderBy(Function(f) f.DisplayIndex)
                If Convert.ToBoolean(datarow.Cells(item.Index).Tag) Then nList.Add(item.Name.ToString)
            Next
        Next
        Return nList
    End Function

    Protected Shadows Sub My_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellContentClick
        Dim tDataGridView As DataGridView = CType(sender, DataGridView)
        If e.RowIndex >= 0 Then
            tDataGridView.Item(e.ColumnIndex, e.RowIndex).Tag = Not CType(tDataGridView.Item(e.ColumnIndex, e.RowIndex).Tag, Boolean)
        End If
    End Sub

    Protected Shadows Sub My_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
        Dim tDataGridView As DataGridView = CType(sender, DataGridView)
        Dim colName As String = tDataGridView.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If e.RowIndex >= 0 AndAlso Not tDataGridView.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        If e.RowIndex >= 0 Then
            If Convert.ToBoolean(tDataGridView.Item(e.ColumnIndex, e.RowIndex).Tag) Then
                e.CellStyle.BackColor = Color.Green
                e.CellStyle.SelectionBackColor = Color.Green
            Else
                e.CellStyle.BackColor = Color.Gray
                e.CellStyle.SelectionBackColor = Color.Gray
            End If
        End If
    End Sub

    Protected Shadows Sub My_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim tDataGridView As DataGridView = CType(sender, DataGridView)
            tDataGridView.ColumnHeadersVisible = Not tDataGridView.ColumnHeadersVisible
        End If
    End Sub

End Class