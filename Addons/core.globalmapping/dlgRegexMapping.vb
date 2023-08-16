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
Imports System.Windows.Forms

Public Class dlgRegexMapping

#Region "Fields"

    Private _Type As MappingType

    Friend WithEvents bwCleanDatabase As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

    Sub New(ByRef mappingType As MappingType)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _Type = mappingType
        Setup()
        PopulateMappings()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        btnOK.Enabled = False
        tspbStatus.Visible = True
        SaveChanges()
        DialogResult = DialogResult.OK
        'bwCleanDatabase.WorkerReportsProgress = True
        'bwCleanDatabase.RunWorkerAsync()
    End Sub

    Private Sub bwCleanDatabase_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCleanDatabase.RunWorkerCompleted
        Dim iCount As Integer = DirectCast(e.Result, Integer)
        tspbStatus.Visible = False
        MessageBox.Show(String.Format("{0} item(s) changed", iCount), Master.eLang.GetString(362, "Done"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        DialogResult = DialogResult.OK
    End Sub

    Private Sub bwCleanDatabase_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCleanDatabase.DoWork
        Select Case _Type
            'Case MappingType.CertificationMapping
            '    e.Result = Master.DB.Cleanup_Certifications()
            'Case MappingType.CountryMapping
            '    e.Result = Master.DB.Cleanup_Countries()
            'Case MappingType.StatusMapping
            '    e.Result = Master.DB.Cleanup_Status()
            'Case MappingType.StudioMapping
            '    e.Result = Master.DB.Cleanup_Studios()
        End Select
    End Sub

    Private Sub PopulateMappings()
        Dim nMapping As XmlRegexMapping = Nothing
        Select Case _Type
            Case MappingType.EditionMapping
                nMapping = CType(APIXML.EditionMappings.CloneDeep, XmlRegexMapping)
        End Select
        If nMapping IsNot Nothing Then
            For Each aProperty As RegexMapping In nMapping.Mappings
                Dim iRow As Integer = dgvMappings.Rows.Add(New Object() {
                                                           aProperty.RegExp,
                                                           aProperty.Result
                                                           })
                dgvMappings.Rows(iRow).Tag = aProperty
            Next
        End If
        dgvMappings.ClearSelection()
    End Sub

    Public Sub SaveChanges()
        Dim nRegexMapping As New List(Of RegexMapping)
        For Each aRow As DataGridViewRow In dgvMappings.Rows
            If Not aRow.IsNewRow AndAlso Not String.IsNullOrEmpty(aRow.Cells(0).Value.ToString.Trim) Then
                nRegexMapping.Add(New RegexMapping With {
                                  .RegExp = aRow.Cells(0).Value.ToString.Trim,
                                  .Result = aRow.Cells(1).Value.ToString.Trim
                                  })
            End If
        Next
        Select Case _Type
            Case MappingType.EditionMapping
                APIXML.EditionMappings.Mappings = nRegexMapping
                APIXML.EditionMappings.Save()
        End Select
    End Sub

    Private Sub Setup()
        Select Case _Type
            Case MappingType.EditionMapping
                Text = Master.eLang.GetString(126, "Edition Mapping")
        End Select
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        colRegex.HeaderText = Master.eLang.GetString(699, "Regex")
        colMapping.HeaderText = Master.eLang.GetString(1116, "Mapped to")
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Enum MappingType As Integer
        EditionMapping
    End Enum

#End Region 'Nested Types

End Class