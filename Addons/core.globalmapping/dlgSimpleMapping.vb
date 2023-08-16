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

Public Class dlgSimpleMapping

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
        bwCleanDatabase.WorkerReportsProgress = True
        bwCleanDatabase.RunWorkerAsync()
    End Sub

    Private Sub bwCleanDatabase_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCleanDatabase.RunWorkerCompleted
        Dim iCount As Integer = DirectCast(e.Result, Integer)
        tspbStatus.Visible = False
        MessageBox.Show(String.Format("{0} item(s) changed", iCount), Master.eLang.GetString(362, "Done"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        DialogResult = DialogResult.OK
    End Sub

    Private Sub bwCleanDatabase_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCleanDatabase.DoWork
        Select Case _Type
            Case MappingType.CertificationMapping
                e.Result = Master.DB.Cleanup_Certifications()
            Case MappingType.CountryMapping
                e.Result = Master.DB.Cleanup_Countries()
            Case MappingType.StatusMapping
                e.Result = Master.DB.Cleanup_Status()
            Case MappingType.StudioMapping
                e.Result = Master.DB.Cleanup_Studios()
        End Select
    End Sub

    Private Sub PopulateMappings()
        Dim nMapping As XmlSimpleMapping = Nothing
        Select Case _Type
            Case MappingType.CertificationMapping
                Master.DB.LoadAll_Certifications()
                nMapping = CType(APIXML.CertificationMappings.CloneDeep, XmlSimpleMapping)
            Case MappingType.CountryMapping
                Master.DB.LoadAll_Countries()
                nMapping = CType(APIXML.CountryMappings.CloneDeep, XmlSimpleMapping)
            Case MappingType.StatusMapping
                Master.DB.LoadAll_Status()
                nMapping = CType(APIXML.StatusMappings.CloneDeep, XmlSimpleMapping)
            Case MappingType.StudioMapping
                Master.DB.LoadAll_Studios()
                nMapping = CType(APIXML.StudioMappings.CloneDeep, XmlSimpleMapping)
        End Select
        If nMapping IsNot Nothing Then
            For Each aProperty As SimpleMapping In nMapping.Mappings.OrderBy(Function(f) f.Input)
                Dim iRow As Integer = dgvMappings.Rows.Add(New Object() {
                                                           aProperty.Input,
                                                           aProperty.MappedTo
                                                           })
                dgvMappings.Rows(iRow).Tag = aProperty
            Next
        End If
        dgvMappings.ClearSelection()
    End Sub

    Public Sub SaveChanges()
        Dim nSimpleMapping As New List(Of SimpleMapping)
        For Each aRow As DataGridViewRow In dgvMappings.Rows
            If Not aRow.IsNewRow AndAlso Not String.IsNullOrEmpty(aRow.Cells(0).Value.ToString.Trim) Then
                nSimpleMapping.Add(New SimpleMapping With {
                                   .Input = aRow.Cells(0).Value.ToString.Trim,
                                   .MappedTo = aRow.Cells(1).Value.ToString.Trim
                                   })
            End If
        Next
        Select Case _Type
            Case MappingType.CertificationMapping
                APIXML.CertificationMappings.Mappings = nSimpleMapping
                APIXML.CertificationMappings.Save()
            Case MappingType.CountryMapping
                APIXML.CountryMappings.Mappings = nSimpleMapping
                APIXML.CountryMappings.Save()
            Case MappingType.StatusMapping
                APIXML.StatusMappings.Mappings = nSimpleMapping
                APIXML.StatusMappings.Save()
            Case MappingType.StudioMapping
                APIXML.StudioMappings.Mappings = nSimpleMapping
                APIXML.StudioMappings.Save()
        End Select
    End Sub

    Private Sub Setup()
        Select Case _Type
            Case MappingType.CertificationMapping
                Text = Master.eLang.GetString(1114, "Certification Mapping")
            Case MappingType.CountryMapping
                Text = Master.eLang.GetString(884, "Country Mapping")
            Case MappingType.StatusMapping
                Text = Master.eLang.GetString(1144, "Status Mapping")
            Case MappingType.StudioMapping
                Text = Master.eLang.GetString(1113, "Studio Mapping")
        End Select
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        colInput.HeaderText = Master.eLang.GetString(1115, "Input")
        colMapping.HeaderText = Master.eLang.GetString(1116, "Mapped to")
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Enum MappingType As Integer
        CountryMapping
        CertificationMapping
        StatusMapping
        StudioMapping
    End Enum

#End Region 'Nested Types

End Class