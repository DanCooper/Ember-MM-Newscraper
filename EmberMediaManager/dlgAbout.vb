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

Public NotInheritable Class dlgAbout

#Region "Fields"

    Dim CredList As New List(Of CredLine)
    Dim PicY As Single

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub dlgAbout_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
        Refresh()
    End Sub

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim iBackground As New Bitmap(picDisplay.Width, picDisplay.Height)
        Dim iLogo As New Bitmap(My.Resources.Logo)
        For xPix As Integer = 0 To iLogo.Width - 1
            For yPix As Integer = 0 To iLogo.Height - 1
                Dim clr As Color = iLogo.GetPixel(xPix, yPix)
                If clr.A > 128 Then
                    clr = Color.FromArgb(128, clr.R, clr.G, clr.B)
                    iLogo.SetPixel(xPix, yPix, clr)
                End If
            Next
        Next

        Using g As Graphics = Graphics.FromImage(iBackground)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            Dim DrawRect As New Rectangle(0, 0, picDisplay.ClientSize.Width, Convert.ToInt32(picDisplay.ClientSize.Height * 0.735))
            g.FillRectangle(New Drawing2D.LinearGradientBrush(DrawRect, Color.FromArgb(255, 200, 200, 255), Color.FromArgb(255, 250, 250, 250), Drawing2D.LinearGradientMode.Vertical), DrawRect)
            DrawRect = New Rectangle(0, Convert.ToInt32(picDisplay.ClientSize.Height * 0.735), picDisplay.ClientSize.Width, Convert.ToInt32(picDisplay.ClientSize.Height * 0.265))
            g.FillRectangle(New Drawing2D.LinearGradientBrush(DrawRect, Color.White, Color.FromArgb(255, 230, 230, 230), Drawing2D.LinearGradientMode.Vertical), DrawRect)
            Dim x As Integer = Convert.ToInt32((picDisplay.Width - My.Resources.Logo.Width) / 2)
            Dim y As Integer = Convert.ToInt32((picDisplay.Height - My.Resources.Logo.Height) / 2)
            g.DrawImage(iLogo, x, y, My.Resources.Logo.Width, My.Resources.Logo.Height)
            picDisplay.BackgroundImage = iBackground
        End Using

        Text = "About Ember Media Manager"

        ' Optimize Painting.
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.DoubleBuffer Or
                 ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)

        Dim VersionNumber As String = Master.Version

        CredList.Add(New CredLine With {.Text = String.Concat("Ember Media Manager"), .Font = New Font("Microsoft Sans Serif", 24, FontStyle.Bold)})
        CredList.Add(New CredLine With {.Text = VersionNumber, .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)})
        CredList.Add(New CredLine With {.Text = String.Empty, .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)})
        CredList.Add(New CredLine With {.Text = My.Application.Info.Description, .Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)})
        CredList.Add(New CredLine With {.Text = String.Empty, .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)})
        CredList.Add(New CredLine With {.Text = My.Application.Info.Copyright, .Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)})
        CredList.Add(New CredLine With {.Text = String.Empty, .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)})
        Dim aStr() As String
        aStr = Master.eLang.GetString(968, "credits").Split(Environment.NewLine.ToCharArray)
        Dim aFont As Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)
        For Each aSt In aStr
            aSt = aSt.Trim
            If (aSt.Length > 0) AndAlso (aSt.Substring(0, 1) = "-") Then
                Dim aI As Single = CSng(Microsoft.VisualBasic.Val(aSt.Substring(1, aSt.Length - 1))) 'TODO: remove the old VB code
                aFont = New Font("Microsoft Sans Serif", aI, FontStyle.Regular)
                CredList.Add(New CredLine With {.Text = String.Empty, .Font = aFont})
            Else
                CredList.Add(New CredLine With {.Text = aSt, .Font = aFont})
            End If
        Next

        PicY = picDisplay.ClientSize.Height
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Close()
    End Sub
    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbFFMPEG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbFFMPEG.Click
        Functions.Launch(My.Resources.urlFfmpeg)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbIMDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbIMDB.Click
        Functions.Launch(My.Resources.urlIMDB)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbIMPA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbIMPA.Click
        Functions.Launch(My.Resources.urlIMPAwards)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbMI.Click
        Functions.Launch(My.Resources.urlMediaInfo)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbMPDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbMPDB.Click
        Functions.Launch(My.Resources.urlMoviePostersDb)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbTMDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbTMDB.Click
        Functions.Launch(My.Resources.urlTheMovieDb)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbXBMC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbXBMC.Click
        Functions.Launch(My.Resources.urlXBMC)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbYouTube_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbYouTube.Click
        Functions.Launch(My.Resources.urlYouTube)
    End Sub

    ''' <summary>
    ''' Launch the default browser to the appropriate URL.
    ''' The URL is defined in the project's resources
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub picDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picDisplay.Click
        Functions.Launch(My.Resources.urlEmberMediaManager)
    End Sub
    ''' <summary>
    ''' Scroll the credit lines <c>CredList</c> in the dialog window.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub picDisplay_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picDisplay.Paint
        Dim CurrentX As Single, CurrentY As Single, FontMod As Single = 0

        For i As Integer = 0 To CredList.Count - 1

            CurrentY = PicY + FontMod
            FontMod += CredList(i).Font.Size + 5

            CurrentX = (picDisplay.ClientSize.Width - e.Graphics.MeasureString(CredList(i).Text, CredList(i).Font).Width) / 2

            If i = CredList.Count - 1 AndAlso CurrentY < -25 Then PicY = picDisplay.ClientSize.Height

            e.Graphics.DrawString(CredList(i).Text, CredList(i).Font, Brushes.Black, CurrentX, CurrentY)

        Next

        PicY -= 1

        'Allow other things to redraw if required
        Application.DoEvents()

        System.Threading.Thread.Sleep(30)

        picDisplay.Invalidate()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Friend Class CredLine

#Region "Fields"

        Private _font As Font
        Private _text As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Font() As Font
            Get
                Return _font
            End Get
            Set(ByVal value As Font)
                _font = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _text = String.Empty
            _font = New Font("Microsoft Sans Serif", 11, FontStyle.Bold)
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class