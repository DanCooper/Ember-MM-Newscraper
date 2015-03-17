Imports System.Windows.Forms
Imports EmberAPI

Public Class dlgRestart

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgRestart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(298, "Restart Ember Media Manager?")
        Me.lblHeader.Text = Me.Text
        Me.lblBody.Text = Master.eLang.GetString(299, "Recent changes require a restart of Ember Media Manager to complete.\n\nWould you like to restart Ember Media Manager now?").Replace("\n", Environment.NewLine)

        Me.OK_Button.Text = Master.eLang.GetString(300, "Yes")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
    End Sub
End Class
