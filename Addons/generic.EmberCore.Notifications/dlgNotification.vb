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

Imports System.Runtime.InteropServices

Public Class dlgNotification

#Region "Fields"

    Protected _AnimationTimer As New Timer()
    Protected _AnimationType As AnimationTypes = AnimationTypes.Show

    Private _StrType As String

#End Region 'Fields

#Region "Enumerations"

    Public Enum AnimationTypes
        Show = 0
        Wait = 1
        Close = 2
    End Enum

#End Region 'Enumerations

#Region "Events"

    Public Event NotifierClicked(ByVal Type As String)
    Public Event NotifierClosed()

#End Region 'Events

#Region "Properties"

    Protected Overrides ReadOnly Property ShowWithoutActivation() As Boolean
        Get
            Return True
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Overloads Sub Show(ByVal _type As String, ByVal _icon As Integer, ByVal _title As String, ByVal _message As String, ByVal _customicon As Image)
        _StrType = _type
        If _customicon IsNot Nothing Then
            pbIcon.Image = _customicon
        Else
            Select Case _icon
                Case 1
                    pbIcon.Image = My.Resources._error
                Case 2
                    pbIcon.Image = My.Resources._comment
                Case 3
                    pbIcon.Image = My.Resources._movie
                Case 4
                    pbIcon.Image = My.Resources._tv
                Case Else
                    pbIcon.Image = My.Resources._info
            End Select
        End If
        lblTitle.Text = _title
        lblMessage.Text = _message

        MyBase.Show()
    End Sub

    Protected Sub OnTimer(ByVal sender As Object, ByVal e As EventArgs)
        Select Case _AnimationType

            Case AnimationTypes.Show

                If Height < 80 Then
                    SetBounds(Left, Top - 2, Width, Height + 2)
                    Application.DoEvents()
                Else
                    _AnimationTimer.Stop()
                    Height = 80
                    _AnimationTimer.Interval = 3000
                    _AnimationType = AnimationTypes.Wait
                    Application.DoEvents()
                    _AnimationTimer.Start()
                End If

            Case AnimationTypes.Wait

                _AnimationTimer.Stop()
                _AnimationTimer.Interval = 5
                _AnimationType = AnimationTypes.Close
                Application.DoEvents()
                _AnimationTimer.Start()

            Case AnimationTypes.Close

                If Height > 2 Then
                    SetBounds(Left, Top + 2, Width, Height - 2)
                    Application.DoEvents()
                Else
                    _AnimationTimer.Stop()
                    Application.DoEvents()
                    Close()
                End If

        End Select
    End Sub

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
    End Function

    Private Sub frmNotify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        RaiseEvent NotifierClicked(_StrType)
    End Sub

    Private Sub frmNotify_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        RaiseEvent NotifierClosed()
    End Sub

    Private Sub frmNotify_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetWindowPos(Handle, New IntPtr(-1), Screen.PrimaryScreen.WorkingArea.Width - Width - 5, Screen.PrimaryScreen.WorkingArea.Height - 5, 315, 0, &H10)
        AddHandler _AnimationTimer.Tick, AddressOf OnTimer
        Application.DoEvents()
    End Sub

    Private Sub frmNotify_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        _AnimationTimer.Stop()
        _AnimationTimer.Interval = 5
        _AnimationType = AnimationTypes.Show
        Application.DoEvents()
        _AnimationTimer.Start()
    End Sub

    Private Sub lblMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMessage.Click
        RaiseEvent NotifierClicked(_StrType)
    End Sub

    Private Sub lblTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTitle.Click
        RaiseEvent NotifierClicked(_StrType)
    End Sub

    Private Sub pbIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbIcon.Click
        RaiseEvent NotifierClicked(_StrType)
    End Sub

#End Region 'Methods

End Class