'*************************** Module Header ******************************'
' Module Name:  XmlTextEncoder.vb
' Project:	    VBRichTextBoxSyntaxHighlighting
' Copyright (c) Microsoft Corporation.
' 
' This CharacterEncoder class supplies a static(Shared) method to encode some 
' special characters in Xml and Rtf, such as '<', '>', '"', '&', ''', '\',
' '{' and '}' .
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

Public Class CharacterEncoder
    Public Shared Function Encode(ByVal originalText As String) As String
        If (String.IsNullOrEmpty(originalText) OrElse originalText.Trim().Length = 0) Then
            Return String.Empty
        End If

        Dim encodedText As New StringBuilder()
        For i As Integer = 0 To originalText.Length - 1
            Select Case originalText.Chars(i)
                Case """"c
                    encodedText.Append("&quot;")
                Case "&"c
                    encodedText.Append("&amp;")
                Case "'"c
                    encodedText.Append("&apos;")
                Case "<"c
                    encodedText.Append("&lt;")
                Case ">"c
                    encodedText.Append("&gt;")

                    ' The character '\' should be converted to "\\" 
                Case "\"c
                    encodedText.Append("\\")

                    ' The character '{' should be converted to "\{" 
                Case "{"c
                    encodedText.Append("\{")

                    ' The character '}' should be converted to "\}" 
                Case "}"c
                    encodedText.Append("\}")

                Case Else
                    encodedText.Append(originalText.Chars(i))
            End Select

        Next i
        Return encodedText.ToString()
    End Function
End Class
