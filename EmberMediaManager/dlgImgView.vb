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
Imports System.Diagnostics

Public Class dlgImgView

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private isFull As Boolean = False
    Private PanStartPoint As New Point

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal iImage As Image) As Windows.Forms.DialogResult
        Me.pbCache.Image = iImage
        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgImgView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.Enter Then
            Me.Close()
        End If
    End Sub

    Private Sub dlgImgView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetUp()
        Me.DoFit(True)
        Me.Activate()
    End Sub

    Private Sub DoFit(ByVal firstCall As Boolean)
        If Me.isFull OrElse firstCall Then
            Me.Visible = False 'hide form until resizing is done... hides Full -> Fit position whackiness not fixable by .SuspendLayout
            Me.ResetScroll()
            Me.isFull = False
            Me.pnlBG.AutoScroll = False

            ImageUtils.ResizePB(Me.pbPicture, Me.pbCache, My.Computer.Screen.WorkingArea.Height - 60, My.Computer.Screen.WorkingArea.Width)

            Me.Width = Me.pbPicture.Width + 16
            Me.Height = Me.pbPicture.Height + 64
            Me.Left = Convert.ToInt32((My.Computer.Screen.WorkingArea.Width - Me.Width) / 2)
            Me.Top = Convert.ToInt32((My.Computer.Screen.WorkingArea.Height - Me.Height) / 2)

            Me.Visible = True
        End If
    End Sub

    Private Sub DoFull()
        If Not Me.isFull Then
            Me.Visible = False 'hide form until resizing is done... hides Full -> Fit position whackiness not fixable by .SuspendLayout
            Dim screenHeight As Integer = My.Computer.Screen.WorkingArea.Height
            Dim screenWidth As Integer = My.Computer.Screen.WorkingArea.Width
            Dim HasVertBar As Boolean = False

            Me.ResetScroll()
            Me.isFull = True

            Me.pbPicture.Image = CType(Me.pbCache.Image.Clone, Image)
            Me.pbPicture.SizeMode = PictureBoxSizeMode.AutoSize
            Me.pnlBG.AutoScroll = True

            'set dlg size

            If Me.pbPicture.Height >= (screenHeight - 32) Then
                Me.Height = screenHeight
                HasVertBar = True
            Else
                Me.Height = pbPicture.Height + 64
            End If
            Me.Top = Convert.ToInt32((screenHeight - Me.Height) / 2)

            If Me.pbPicture.Width >= (screenWidth - 25) Then
                Me.Width = screenWidth
            Else
                If HasVertBar Then
                    Me.Width = Me.pbPicture.Width + 33
                Else
                    Me.Width = Me.pbPicture.Width + 16
                End If
            End If
            Me.Left = Convert.ToInt32((screenWidth - Me.Width) / 2)

            Me.Visible = True
        End If
    End Sub

    Private Sub pbPicture_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbPicture.DoubleClick
        Me.Close()
    End Sub

    Private Sub pbPicture_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPicture.MouseDown
        If Me.isFull Then
            PanStartPoint = New Point(e.X, e.Y)
            pbPicture.Cursor = Cursors.NoMove2D
        End If
    End Sub

    Private Sub pbPicture_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPicture.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left AndAlso pbPicture.Cursor = Cursors.NoMove2D Then

            Dim DeltaX As Integer = (PanStartPoint.X - e.X)
            Dim DeltaY As Integer = (PanStartPoint.Y - e.Y)

            Me.pnlBG.AutoScrollPosition = New Drawing.Point((DeltaX - pnlBG.AutoScrollPosition.X), (DeltaY - pnlBG.AutoScrollPosition.Y))

        End If
    End Sub

    Private Sub pbPicture_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPicture.MouseUp
        Me.pbPicture.Cursor = Cursors.Default
    End Sub

    Private Sub ResetScroll()
        Me.pnlBG.AutoScrollPosition = New Drawing.Point(0, 0)
        Me.pbPicture.Location = New Point(0, 25)
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(184, "Image Viewer")
        Me.tsbFit.Text = Master.eLang.GetString(185, "Fit")
        Me.tsbFull.Text = Master.eLang.GetString(186, "Full Size")
    End Sub

    Private Sub tsbFit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbFit.Click
        Me.DoFit(False)
    End Sub

    Private Sub tsbFull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbFull.Click
        Me.DoFull()
    End Sub

#End Region 'Methods

End Class