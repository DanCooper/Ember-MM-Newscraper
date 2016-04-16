﻿' ################################################################################
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

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace FormUtils

    Public Class TextBox_with_Watermark
        Inherits TextBox

        'Declare A Few Variables
        Dim WaterText As String
        Dim WaterColor As Color
        Dim WaterFont As Font
        Dim WaterBrush As SolidBrush
        Dim WaterContainer As Panel

        Public Sub New()
            MyBase.New()
            StartProcess()
        End Sub

        Private Sub StartProcess()
            'Assign Values To the Variables
            WaterText = "Default Watermark"
            WaterColor = Color.Gray
            WaterFont = New Font(Me.Font, FontStyle.Italic)
            WaterBrush = New SolidBrush(WaterColor)

            CreateWatermark()

            AddHandler TextChanged, AddressOf ChangeText
            AddHandler FontChanged, AddressOf ChangeFont
        End Sub

        Private Sub CreateWatermark()
            WaterContainer = New Panel
            Me.Controls.Add(WaterContainer)
            AddHandler WaterContainer.Click, AddressOf Clicked
            AddHandler WaterContainer.Paint, AddressOf Painted
        End Sub

        Private Sub RemoveWatermark()
            Me.Controls.Remove(WaterContainer)
        End Sub

        Private Sub ChangeText(sender As Object, e As EventArgs)
            If Me.TextLength <= 0 Then
                CreateWatermark()
            ElseIf Me.TextLength > 0 Then
                RemoveWatermark()
            End If
        End Sub

        Private Sub ChangeFont(sender As Object, e As EventArgs)
            WaterFont = New Font(Me.Font, FontStyle.Italic)
        End Sub

        Private Sub Clicked(sender As Object, e As EventArgs)
            Me.Focus()
        End Sub

        Private Sub Painted(sender As Object, e As PaintEventArgs)
            WaterContainer.Location = New Point(2, 0)
            WaterContainer.Anchor = AnchorStyles.Left Or AnchorStyles.Right
            WaterContainer.Height = Me.Height
            WaterContainer.Width = Me.Width
            WaterBrush = New SolidBrush(WaterColor)

            Dim Graphic As Graphics = e.Graphics
            Graphic.DrawString(WaterText, WaterFont, WaterBrush, New PointF(-2.0!, 1.0!))
        End Sub

        Protected Overrides Sub OnInvalidated(e As System.Windows.Forms.InvalidateEventArgs)
            MyBase.OnInvalidated(e)
            WaterContainer.Invalidate()
        End Sub

        <Category("Watermark Attributes"), Description("Sets Watermark Text")> Public Property WatermarkText As String
            Get
                Return WaterText
            End Get
            Set(value As String)
                WaterText = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Watermark Attributes"), Description("Sets Watermark Color")> Public Property WatermarkColor As Color
            Get
                Return WaterColor
            End Get
            Set(value As Color)
                WaterColor = value
                Me.Invalidate()
            End Set
        End Property
    End Class

End Namespace
