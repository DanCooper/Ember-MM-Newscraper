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
Imports System.Drawing

Public Class PixelRuler

    Private Sub PixelRuler_Paint(sender As Object, e As Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Using decimalPen As Pen = New System.Drawing.Pen(System.Drawing.Color.Blue), _
            centuryPen As Pen = New System.Drawing.Pen(System.Drawing.Color.Red), _
            formGraphics = Me.CreateGraphics(), _
            brush As SolidBrush = New SolidBrush(Color.Black), _
            font As Font = New Font("Arial", 8, FontStyle.Bold)

            decimalPen.Width = 1
            centuryPen.Width = 1

            Dim century As Boolean = False

            For tick As Integer = 0 To 2000 Step 10
                If (tick Mod 100) = 0 Then
                    century = True
                Else
                    century = False
                End If
                If Me.Width > Me.Height Then
                    'horizontal ruler
                    formGraphics.DrawLine(IIf(century, centuryPen, decimalPen), tick, 0, tick, Me.Height)
                    If century Then
                        formGraphics.DrawString((tick \ 100).ToString(), font, brush, tick, 0)
                    End If
                Else
                    'vertical ruler
                    formGraphics.DrawLine(IIf(century, centuryPen, decimalPen), 0, tick, Me.Width, tick)
                    If century Then
                        formGraphics.DrawString((tick \ 100).ToString(), font, brush, 0, tick)
                    End If
                End If
            Next

        End Using
    End Sub
End Class
