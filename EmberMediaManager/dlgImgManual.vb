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

Public Class dlgImgManual

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    'Dim DLType As New Enums.MovieImageType
    Dim tImage As New MediaContainers.Image

#End Region 'Fields

#Region "Properties"

    Public Property Results As MediaContainers.Image
        Get
            Return tImage
        End Get
        Set(value As MediaContainers.Image)
            tImage = value
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog() As DialogResult
        Return MyBase.ShowDialog()
    End Function

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try
            tImage.ImageOriginal.FromWeb(txtURL.Text)

            If tImage.ImageOriginal.Image IsNot Nothing Then

                Using dImgView As New dlgImgView
                    dImgView.ShowDialog(tImage.ImageOriginal.Image)
                End Using

            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub dlgImgManual_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'tImage.Dispose() cannot dispose as is used by calling entity
        tImage = Nothing
    End Sub

    Private Sub dlgImgManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()

        'If Me.DLType = Enums.MovieImageType.Fanart Then
        '    Me.Text = Master.eLang.GetString(182, "Manual Fanart Entry")
        'Else
        '    Me.Text = Master.eLang.GetString(183, "Manual Poster Entry")
        'End If
    End Sub

    Private Sub dlgImgManual_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
        txtURL.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Try

            If tImage.ImageOriginal.Image Is Nothing Then
                tImage.ImageOriginal.FromWeb(txtURL.Text)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub SetUp()
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        btnPreview.Text = Master.eLang.GetString(180, "Preview")
        lblURL.Text = Master.eLang.GetString(181, "Enter URL to Image:")
    End Sub

    Private Sub txtURL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtURL.TextChanged
        If Not String.IsNullOrEmpty(txtURL.Text) AndAlso StringUtils.isValidURL(txtURL.Text) Then
            btnPreview.Enabled = True
            OK_Button.Enabled = True
        Else
            btnPreview.Enabled = False
            OK_Button.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class