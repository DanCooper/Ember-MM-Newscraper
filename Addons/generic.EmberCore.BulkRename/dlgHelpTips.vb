Imports System.Windows.Forms
Imports EmberAPI

Public Class dlgHelpTips

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        DialogResult = DialogResult.Cancel
    End Sub

End Class
