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

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Schema
Imports System.Text
Imports EmberAPI
Imports NLog

Public Class dlgManualEdit

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private Changed As Boolean = False
    Private currFile As String
    Private DtdDt As DataTable
    Private ErrStr As String
    Private IsValid As Boolean
    Private lineInf As IXmlLineInfo
    Private ReturnOK As Boolean = False
    Private TagStack As New Stack()

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Public Overloads Function ShowDialog(ByVal nfoPath As String) As DialogResult
        currFile = nfoPath
        Return ShowDialog()
    End Function

    Private Sub Editor_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        xmlvNFO.Focus()
    End Sub

    Private Sub Editor_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Changed Then
            If MessageBox.Show(Master.eLang.GetString(196, "Do you want to save changes?"), Master.eLang.GetString(197, "Save?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                File.WriteAllText(currFile, xmlvNFO.Text, Encoding.UTF8)
                DialogResult = DialogResult.OK
            Else
                e.Cancel = True
            End If
        Else
            If ReturnOK Then DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub Editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUp()

        If Not String.IsNullOrEmpty(currFile) Then
            If File.Exists(currFile) Then
                Cursor = Cursors.WaitCursor
                xmlvNFO.Text = File.ReadAllText(currFile, Encoding.UTF8)
                Try
                    'Let's remove the xmlns
                    Dim aI As Integer = xmlvNFO.Find(" xmlns")
                    If aI > 0 Then
                        Dim aJ As Integer = xmlvNFO.Find(">", aI + 1, RichTextBoxFinds.None)
                        Dim aS1 As String = xmlvNFO.Text.Substring(0, aI)
                        Dim aS2 As String = xmlvNFO.Text.Substring(aI, aJ - aI)
                        Dim aS3 As String = xmlvNFO.Text.Substring(aJ, xmlvNFO.Text.Length - aJ)
                        xmlvNFO.Text = aS1 & aS3
                    End If
                    xmlvNFO.Process(True)
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
                ParseFile(True)
                Cursor = Cursors.Default
            End If

            Text = String.Format("{0} | {1}", Master.eLang.GetString(190, "Manual NFO Editor"), currFile.Substring(currFile.LastIndexOf(Path.DirectorySeparatorChar) + 1))
        End If

        Activate()
    End Sub

    Private Sub lbErrorLog_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbErrorLog.SelectedIndexChanged
        Dim SelItem As String
        Dim linN, colN As Integer

        SelItem = lbErrorLog.SelectedItem.ToString

        If Not String.IsNullOrEmpty(SelItem) Then

            linN = CType(Regex.Replace(SelItem, "([0-9]+): ([0-9]+)(.*)", "$1"), Integer)
            colN = CType(Regex.Replace(SelItem, "([0-9]+): ([0-9]+)(.*)", "$2"), Integer)

            Dim mc As MatchCollection
            Dim i As Integer = 0

            mc = Regex.Matches(xmlvNFO.Text, "\n", RegexOptions.Singleline)

            Try
                xmlvNFO.Select(mc(linN - 2).Index + colN, 2)
                xmlvNFO.SelectionColor = Color.Blue
                xmlvNFO.Focus()

            Catch ex As Exception
                xmlvNFO.Focus()
            End Try
        End If
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Close()
    End Sub

    Private Sub mnuFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFormat.Click
        Try
            Cursor = Cursors.WaitCursor
            xmlvNFO.Process(True)
            Cursor = Cursors.Default
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub mnuParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuParse.Click
        ParseFile()
    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        File.WriteAllText(currFile, xmlvNFO.Text, Encoding.UTF8)
        ReturnOK = True
        Changed = False
    End Sub

    Private Sub ParseFile(Optional OnLoad As Boolean = False)
        If currFile Is Nothing Then
            Exit Sub
        End If

        Cursor = Cursors.WaitCursor

        Dim tempFile As String = Path.GetTempPath + "nfo.tmp"
        File.WriteAllText(tempFile, xmlvNFO.Text, Encoding.UTF8)

        Dim xmlP As New XmlTextReader(tempFile)
        ' Set the validation settings.
        Dim settings As XmlReaderSettings = New XmlReaderSettings()
        settings.ValidationType = ValidationType.Schema
        settings.ValidationFlags = settings.ValidationFlags Or XmlSchemaValidationFlags.ProcessInlineSchema
        settings.ValidationFlags = settings.ValidationFlags Or XmlSchemaValidationFlags.ReportValidationWarnings

        Dim xmlV As XmlReader = XmlReader.Create(xmlP, settings)
        ErrStr = String.Empty
        lbErrorLog.Items.Clear()

        IsValid = True

        Do
            Try
                If xmlV.Read() Then
                    lineInf = CType(xmlV, IXmlLineInfo)
                End If

            Catch exx As Exception

                Try

                    IsValid = False

                    If lineInf.HasLineInfo Then
                        ErrStr = lineInf.LineNumber.ToString + ":" + lineInf.LinePosition.ToString + " ==> " + exx.Message
                    End If

                    If exx.Message.IndexOf("EndElement") > 1 Then
                        Exit Do
                    End If

                    lbErrorLog.Items.Add(ErrStr)

                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Exit Do
                End Try

            End Try

        Loop While Not xmlP.EOF

        xmlV.Close()
        xmlP.Close()

        Cursor = Cursors.Default
        If OnLoad Then
            If IsValid = False Then
                MessageBox.Show(Master.eLang.GetString(192, "File is not valid."), Master.eLang.GetString(194, "Not Valid"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                MessageBox.Show(Master.eLang.GetString(193, "File is valid."), Master.eLang.GetString(195, "Valid"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub SetUp()
        mnuFormat.Text = Master.eLang.GetString(187, "&Format / Indent")
        mnuParse.Text = Master.eLang.GetString(188, "&Parse")
        mnuTools.Text = Master.eLang.GetString(8, "&Tools")
        mnuFile.Text = Master.eLang.GetString(1, "&File")
        mnuSave.Text = Master.eLang.GetString(189, "&Save")
        mnuExit.Text = Master.eLang.GetString(2, "E&xit")
    End Sub

#End Region 'Methods

End Class