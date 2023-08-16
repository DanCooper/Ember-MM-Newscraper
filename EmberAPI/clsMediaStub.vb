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

Imports NLog
Imports System.IO
Imports System.Xml.Serialization


Public Class MediaStub

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"
    ''' <summary>
    ''' Read informations from a DiscStub file
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LoadDiscStub(ByVal sPath As String) As DiscStub
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlStub As New DiscStub

        If Not String.IsNullOrEmpty(sPath) Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".disc" Then
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        xmlSer = New XmlSerializer(GetType(DiscStub))
                        xmlStub = DirectCast(xmlSer.Deserialize(xmlSR), DiscStub)
                    End Using
                Else
                    Return New DiscStub
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If

        Return xmlStub
    End Function
    ''' <summary>
    ''' Write informations to a DiscStub file
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <param name="sTitle"></param>
    ''' <param name="sMessage"></param>
    ''' <remarks></remarks>
    Public Shared Sub SaveDiscStub(ByVal sPath As String, Optional ByVal sTitle As String = "", Optional ByVal sMessage As String = "")
        Dim doesExist As Boolean = False
        Dim xmlSer As New XmlSerializer(GetType(DiscStub))
        Dim fAtt As New FileAttributes
        Dim fAttWritable As Boolean = True
        Dim StubFile As String = sPath
        Dim StubPath As String = Directory.GetParent(StubFile).FullName
        Dim DiscStub As New DiscStub

        DiscStub.Title = sTitle
        DiscStub.Message = sMessage

        Try
            doesExist = File.Exists(StubFile)
            If Not doesExist OrElse (Not CBool(File.GetAttributes(StubFile) And FileAttributes.ReadOnly)) Then

                If Not Directory.Exists(StubPath) Then
                    Directory.CreateDirectory(StubPath)
                End If

                If doesExist Then
                    fAtt = File.GetAttributes(StubFile)
                    Try
                        File.SetAttributes(StubFile, FileAttributes.Normal)
                    Catch ex As Exception
                        fAttWritable = False
                    End Try
                End If

                Using xmlSW As New StreamWriter(StubFile)
                    xmlSer.Serialize(xmlSW, DiscStub)
                End Using

                If doesExist And fAttWritable Then File.SetAttributes(StubFile, fAtt)
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    <XmlRoot("discstub")>
    Public Class DiscStub

#Region "Properties"

        <XmlElement("title")>
        Public Property Title() As String = String.Empty

        <XmlElement("message")>
        Public Property Message() As String = String.Empty

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class
