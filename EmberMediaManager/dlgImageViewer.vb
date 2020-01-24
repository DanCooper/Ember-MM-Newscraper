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

Public Class dlgImageViewer

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private isFull As Boolean = False
    Private PanStartPoint As New Point

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal iImage As Image) As Windows.Forms.DialogResult
        pbCache.Image = iImage
        Return ShowDialog()
    End Function

    Private Sub dlgImgView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.Enter Then
            Close()
        End If
    End Sub

    Private Sub dlgImgView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUp()
        DoFit(True)
        Activate()
    End Sub

    Private Sub DoFit(ByVal firstCall As Boolean)
        If isFull OrElse firstCall Then
            Visible = False 'hide form until resizing is done... hides Full -> Fit position whackiness not fixable by .SuspendLayout
            ResetScroll()
            isFull = False
            pnlBG.AutoScroll = False

            ImageUtils.ResizePB(pbPicture, pbCache, My.Computer.Screen.WorkingArea.Height - 60, My.Computer.Screen.WorkingArea.Width)

            Width = pbPicture.Width + 16
            Height = pbPicture.Height + 64
            Left = Convert.ToInt32((My.Computer.Screen.WorkingArea.Width - Width) / 2)
            Top = Convert.ToInt32((My.Computer.Screen.WorkingArea.Height - Height) / 2)

            Visible = True
        End If
    End Sub

    Private Sub DoFull()
        If Not isFull Then
            Visible = False 'hide form until resizing is done... hides Full -> Fit position whackiness not fixable by .SuspendLayout
            Dim screenHeight As Integer = My.Computer.Screen.WorkingArea.Height
            Dim screenWidth As Integer = My.Computer.Screen.WorkingArea.Width
            Dim HasVertBar As Boolean = False

            ResetScroll()
            isFull = True

            pbPicture.Image = CType(pbCache.Image.Clone, Image)
            pbPicture.SizeMode = PictureBoxSizeMode.AutoSize
            pnlBG.AutoScroll = True

            'set dlg size

            If pbPicture.Height >= (screenHeight - 32) Then
                Height = screenHeight
                HasVertBar = True
            Else
                Height = pbPicture.Height + 64
            End If
            Top = Convert.ToInt32((screenHeight - Height) / 2)

            If pbPicture.Width >= (screenWidth - 25) Then
                Width = screenWidth
            Else
                If HasVertBar Then
                    Width = pbPicture.Width + 33
                Else
                    Width = pbPicture.Width + 16
                End If
            End If
            Left = Convert.ToInt32((screenWidth - Width) / 2)

            Visible = True
        End If
    End Sub

    Private Sub pbPicture_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbPicture.DoubleClick
        Close()
    End Sub

    Private Sub pbPicture_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPicture.MouseDown
        If isFull Then
            PanStartPoint = New Point(e.X, e.Y)
            pbPicture.Cursor = Cursors.NoMove2D
        End If
    End Sub

    Private Sub pbPicture_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPicture.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left AndAlso pbPicture.Cursor = Cursors.NoMove2D Then

            Dim DeltaX As Integer = (PanStartPoint.X - e.X)
            Dim DeltaY As Integer = (PanStartPoint.Y - e.Y)

            pnlBG.AutoScrollPosition = New Drawing.Point((DeltaX - pnlBG.AutoScrollPosition.X), (DeltaY - pnlBG.AutoScrollPosition.Y))

        End If
    End Sub

    Private Sub pbPicture_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPicture.MouseUp
        pbPicture.Cursor = Cursors.Default
    End Sub

    Private Sub ResetScroll()
        pnlBG.AutoScrollPosition = New Drawing.Point(0, 0)
        pbPicture.Location = New Point(0, 25)
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(184, "Image Viewer")
        tsbFit.Text = Master.eLang.GetString(185, "Fit")
        tsbFull.Text = Master.eLang.GetString(186, "Full Size")
    End Sub

    Private Sub tsbFit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbFit.Click
        DoFit(False)
    End Sub

    Private Sub tsbFull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbFull.Click
        DoFull()
    End Sub

#End Region 'Methods

End Class