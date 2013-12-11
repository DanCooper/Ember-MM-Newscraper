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

Public Class ImageFeedback
    ''' <summary>
    ''' Supplies an image and question. The image is displayed in the dialog, and the question
    ''' asks the user whether the image portrays the correct aspect.
    ''' </summary>
    ''' <param name="img">Image to display</param>
    ''' <param name="instr">Text to place in the text box</param>
    ''' <remarks></remarks>
    Public Sub LoadInfo(img As Image, instr As String)
        Instructions.Text = instr

        ShownPicture.Image = img
        'ShownPicture.Width = img.Width
        'ShownPicture.Height = img.Height

        ShownPicture.Size = ShownPicture.Image.Size


    End Sub
    ''' <summary>
    ''' Loads just the question. Assumes the caller has directly manipulated the dialog's PictureBox control
    ''' </summary>
    ''' <param name="instr">Text to place in the text box</param>
    ''' <remarks></remarks>
    Public Sub LoadInfo(instr As String)
        Instructions.Text = instr
        'ShownPicture.Size = ShownPicture.Image.Size

    End Sub

    Private Sub ImageFeedback_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Due to a bug in the MS VisualStudio debugger, you have to manually set visible!
        Me.Visible = True
        ' Instructions.Dock = Windows.Forms.DockStyle.Top

    End Sub
End Class