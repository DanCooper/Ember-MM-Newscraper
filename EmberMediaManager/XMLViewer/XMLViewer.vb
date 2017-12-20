'*************************** Module Header ******************************'
' Module Name:  XMLViewer.vb
' Project:	    VBRichTextBoxSyntaxHighlighting
' Copyright (c) Microsoft Corporation.
' 
' This XMLViewer class inherits System.Windows.Forms.RichTextBox and it is used 
' to display an Xml in a specified format. 
' 
' RichTextBox uses the Rtf format to show the test. The XMLViewer will 
' convert the Xml to Rtf with some formats specified in the XMLViewerSettings,
' and then set the Rtf property to the value.
' 
' This source is subject to the Microsoft Public License.
' See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
' All other rights reserved.
' 
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
' EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
' WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
'*************************************************************************'

Imports System.Text

Public Class XMLViewer
    Inherits RichTextBox
   
    Private _settings As XMLViewerSettings
    ''' <summary>
    ''' The format settings.
    ''' </summary>
    Public Property Settings() As XMLViewerSettings
        Get
            If _settings Is Nothing Then
                _settings = New XMLViewerSettings With {
                    .AttributeKey = Color.Red,
                    .AttributeValue = Color.Blue,
                    .Tag = Color.Blue,
                    .Element = Color.DarkRed,
                    .Value = Color.Black}
            End If
            Return _settings
        End Get
        Set(ByVal value As XMLViewerSettings)
            _settings = value
        End Set
    End Property
    ''' <summary>
    ''' Convert the Xml to Rtf with some formats specified in the XMLViewerSettings,
    ''' and then set the Rtf property to the value.
    ''' </summary>
    ''' <param name="includeDeclaration">
    ''' Specify whether include the declaration.
    ''' </param>
    Public Sub Process(ByVal includeDeclaration As Boolean)
        Try
            ' The Rtf contains 2 parts, header and content. The colortbl is a part of
            ' the header, and the {1} will be replaced with the content.
            Dim rtfFormat As String = "{{\rtf1\ansi\ansicpg1252\deff0\deflang1033\deflangfe2052" _
                                      & "{{\fonttbl{{\f0\fnil Courier New;}}}}" _
                                      & "{{\colortbl ;{0}}}" _
                                      & "\viewkind4\uc1\pard\lang1033\f0 {1}}}"

            ' Get the XDocument from the Text property.
            Dim xmlDoc = XDocument.Parse(Text, LoadOptions.None)

            Dim xmlRtfContent As New StringBuilder()

            ' If includeDeclaration is true and the XDocument has declaration,
            ' then add the declaration to the content.
            If includeDeclaration AndAlso xmlDoc.Declaration IsNot Nothing Then

                ' The constants in XMLViewerSettings are used to specify the order 
                ' in colortbl of the Rtf.
                xmlRtfContent.AppendFormat(
                    "\cf{0} <?\cf{1} xml \cf{2} version\cf{0} =\cf0 ""\cf{3} {4}\cf0 "" " _
                    & "\cf{2} encoding\cf{0} =\cf0 ""\cf{3} {5}\cf0 ""\cf{0} ?>\par",
                    XMLViewerSettings.TagID,
                    XMLViewerSettings.ElementID,
                    XMLViewerSettings.AttributeKeyID,
                    XMLViewerSettings.AttributeValueID,
                    xmlDoc.Declaration.Version,
                    xmlDoc.Declaration.Encoding)
            End If

            ' Get the Rtf of the root element.
            Dim rootRtfContent As String = ProcessElement(xmlDoc.Root, 0)

            xmlRtfContent.Append(rootRtfContent)

            ' Construct the completed Rtf, and set the Rtf property to this value.
            Rtf = String.Format(rtfFormat, Settings.ToRtfFormatString(),
                                   xmlRtfContent.ToString())


        Catch xmlException As Xml.XmlException
            Throw New ApplicationException("Please check the input Xml. Error:" _
                                           & xmlException.Message, xmlException)
        Catch
            Throw
        End Try
    End Sub

    ' Get the Rtf of the xml element.
    Private Function ProcessElement(ByVal element As XElement,
                                    ByVal level As Integer) As String

        ' This viewer does not support the Xml file that has Namespace.
        If Not String.IsNullOrEmpty(element.Name.Namespace.NamespaceName) Then
            Throw New ApplicationException(
                "This viewer does not support the Xml file that has Namespace.")
        End If

        Dim elementRtfFormat As String = String.Empty
        Dim childElementsRtfContent As New StringBuilder()
        Dim attributesRtfContent As New StringBuilder()

        ' Construct the indent.
        Dim indent As New String(" "c, 4 * level)

        ' If the element has child elements or value, then add the element to the 
        ' Rtf. {{0}} will be replaced with the attributes and {{1}} will be replaced
        ' with the child elements or value.
        If element.HasElements OrElse (Not ((String.IsNullOrEmpty(element.Value) OrElse element.Value.Trim().Length = 0))) Then
            elementRtfFormat =
                String.Format("{0}\cf{1} <\cf{2} {3}{{0}}\cf{1} >\par" _
                              & "{{1}}" _
                              & "{0}\cf{1} </\cf{2} {3}\cf{1} >\par",
                              indent,
                              XMLViewerSettings.TagID,
                              XMLViewerSettings.ElementID,
                              element.Name)

            ' Construct the Rtf of child elements.
            If element.HasElements Then
                For Each childElement In element.Elements()
                    Dim childElementRtfContent As String =
                        ProcessElement(childElement, level + 1)
                    childElementsRtfContent.Append(childElementRtfContent)
                Next childElement

                ' If !string.IsNullOrWhiteSpace(element.Value), then construct the Rtf 
                ' of the value.
            Else
                childElementsRtfContent.AppendFormat(
                    "{0}\cf{1} {2}\par",
                    New String(" "c, 4 * (level + 1)),
                    XMLViewerSettings.ValueID,
                    CharacterEncoder.Encode(element.Value.Trim()))
            End If

            ' This element only has attributes. {{0}} will be replaced with the attributes.
        Else
            elementRtfFormat = String.Format("{0}\cf{1} <\cf{2} {3}{{0}}\cf{1} />\par",
                                             indent,
                                             XMLViewerSettings.TagID,
                                             XMLViewerSettings.ElementID,
                                             element.Name)
        End If

        ' Construct the Rtf of the attributes.
        If element.HasAttributes Then
            For Each attribute As XAttribute In element.Attributes()
                Dim attributeRtfContent As String =
                    String.Format(" \cf{0} {3}\cf{1} =\cf0 ""\cf{2} {4}\cf0 """,
                                  XMLViewerSettings.AttributeKeyID,
                                  XMLViewerSettings.TagID,
                                  XMLViewerSettings.AttributeValueID,
                                  attribute.Name,
                                  CharacterEncoder.Encode(attribute.Value))
                attributesRtfContent.Append(attributeRtfContent)
            Next attribute
            attributesRtfContent.Append(" ")
        End If

        Return String.Format(elementRtfFormat, attributesRtfContent, childElementsRtfContent)
    End Function

End Class
