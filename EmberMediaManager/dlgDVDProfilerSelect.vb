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
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports EmberAPI

Public Class dlgDVDProfilerSelect


#Region "Fields"

    Private _results As New List(Of DVDProfiler.cDVD)
    Dim xmlMov As New DVDProfiler.Collection

#End Region

#Region "Properties"

    Public Property Results As List(Of DVDProfiler.cDVD)
        Get
            Return _results
        End Get
        Set(value As List(Of DVDProfiler.cDVD))
            _results = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Private Sub AddCollection(ByVal fPath As String)
        Dim xmlSer As XmlSerializer = Nothing

        Try
            If File.Exists(fPath) Then
                Using xmlSR As StreamReader = New StreamReader(fPath)
                    xmlSer = New XmlSerializer(GetType(DVDProfiler.Collection))
                    xmlMov = DirectCast(xmlSer.Deserialize(xmlSR), DVDProfiler.Collection)
                End Using
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        Dim ID As Integer = 1
        Dim str(4) As String
        For Each cMovie In xmlMov.DVD
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = cMovie.Title
            str(2) = cMovie.ProductionYear
            str(3) = cMovie.CaseType
            itm = New ListViewItem(str)
            lvCollection.Items.Add(itm)
            ID = ID + 1
        Next

        Me.lvCollection.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub

    Private Sub btnCollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadCollection.Click
        Try
            With ofdCollectionXML
                .Filter = "Collection.xml (*.xml)|*.xml"
                .FilterIndex = 0
            End With

            If ofdCollectionXML.ShowDialog() = DialogResult.OK Then
                lvCollection.Clear()
                PrepareList()
                AddCollection(ofdCollectionXML.FileName)
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lvCollection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvCollection.SelectedIndexChanged
        If lvCollection.SelectedItems.Count > 0 Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

    Private Function AddMovieToList() As List(Of DVDProfiler.cDVD)
        For Each Movie As ListViewItem In Me.lvCollection.SelectedItems
            _results.Add(xmlMov.DVD(Movie.Index))
        Next
            Return _results
    End Function

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Me.MergeSelectedMovie(xmlMov.DVD(Me.lvCollection.SelectedItems(0).Index))
        AddMovieToList()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub PrepareList()
        'set ListView
        Me.lvCollection.FullRowSelect = True
        Me.lvCollection.HideSelection = False
        Me.lvCollection.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        Me.lvCollection.Columns.Add("#", HorizontalAlignment.Right)
        Me.lvCollection.Columns.Add("Moviename", HorizontalAlignment.Left)
        Me.lvCollection.Columns.Add("Year", HorizontalAlignment.Left)
        Me.lvCollection.Columns.Add("CaseType", HorizontalAlignment.Left)
    End Sub

    Private Sub SetUp()
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.CANCEL_Button.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

    Public Overloads Function ShowDialog(ByVal DVDProfilerCollection As String, ByVal isMulti As Boolean) As DialogResult
        Me.SetUp()
        Me.lvCollection.MultiSelect = isMulti
        lvCollection.Clear()
        PrepareList()
        If Not String.IsNullOrEmpty(DVDProfilerCollection) Then
            AddCollection(DVDProfilerCollection)
        End If
        Return MyBase.ShowDialog()
    End Function

#End Region 'Methods

End Class