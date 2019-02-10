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

Public Class dlgAddEditActor

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared selIndex As Integer = 0

    Private tmpActor As MediaContainers.Person
    Private strOldURLOriginal As String
    Private isNew As Boolean = True
    Private sHTTP As New HTTP

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As MediaContainers.Person
        Get
            Return tmpActor
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Public Sub SetUp()
        lblName.Text = Master.eLang.GetString(154, "Actor Name:")
        lblRole.Text = Master.eLang.GetString(155, "Actor Role:")
        lblThumb.Text = Master.eLang.GetString(156, "Actor Thumb (URL):")
    End Sub

    Public Overloads Function ShowDialog(Optional ByVal inActor As MediaContainers.Person = Nothing) As DialogResult
        isNew = inActor Is Nothing
        If isNew Then
            tmpActor = New MediaContainers.Person
            strOldURLOriginal = String.Empty
        Else
            tmpActor = inActor
            strOldURLOriginal = inActor.URLOriginal
        End If
        Return MyBase.ShowDialog()
    End Function

    Private Sub btnVerify_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerify.Click
        If Not String.IsNullOrEmpty(txtThumb.Text) Then
            If StringUtils.isValidURL(txtThumb.Text) Then
                If bwDownloadPic.IsBusy Then
                    bwDownloadPic.CancelAsync()
                End If

                pbActLoad.Visible = True

                bwDownloadPic = New System.ComponentModel.BackgroundWorker
                bwDownloadPic.WorkerSupportsCancellation = True
                bwDownloadPic.RunWorkerAsync()
            Else
                MessageBox.Show(Master.eLang.GetString(159, "Specified URL is not valid."), Master.eLang.GetString(160, "Invalid URL"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show(Master.eLang.GetString(161, "Please enter a URL to verify."), Master.eLang.GetString(162, "No Thumb URL Specified"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        sHTTP.StartDownloadImage(txtThumb.Text)

        While sHTTP.IsDownloading
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        e.Result = sHTTP.Image
    End Sub

    Private Sub bwDownloadPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadPic.RunWorkerCompleted
        pbActLoad.Visible = False
        pbActors.Image = DirectCast(e.Result, Image)
        tmpActor.Thumb = New MediaContainers.Image With {.URLOriginal = txtThumb.Text}
        tmpActor.Thumb.ImageOriginal.UpdateMSfromImg(pbActors.Image)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub dlgAddEditActor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        SetUp()

        If isNew Then
            Text = Master.eLang.GetString(157, "New Actor")
        Else
            Text = Master.eLang.GetString(158, "Edit Actor")
            txtName.Text = tmpActor.Name
            txtRole.Text = tmpActor.Role
            txtThumb.Text = tmpActor.URLOriginal
        End If

        Activate()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        tmpActor.Name = txtName.Text
        tmpActor.Role = txtRole.Text
        DialogResult = DialogResult.OK
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        btnOK.Enabled = Not String.IsNullOrEmpty(txtName.Text)
    End Sub

#End Region 'Methods

End Class