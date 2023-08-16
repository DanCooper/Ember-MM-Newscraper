'################################################################################
'#                             EMBER MEDIA MANAGER                              #
'################################################################################
'################################################################################
'# This file is part of Ember Media Manager.                                    #
'#                                                                              #
'# Ember Media Manager is free software: you can redistribute it and/or modify  #
'# it under the terms of the GNU General Public License as published by         #
'# the Free Software Foundation, either version 3 of the License, or            #
'# (at your option) any later version.                                          #
'#                                                                              #
'# Ember Media Manager is distributed in the hope that it will be useful,       #
'# but WITHOUT ANY WARRANTY; without even the implied warranty of               #
'# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
'# GNU General Public License for more details.                                 #
'#                                                                              #
'# You should have received a copy of the GNU General Public License            #
'# along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
'################################################################################

Imports EmberAPI
Imports NLog
Imports System.IO

Public Class TeraCopy

#Region "Fields"

    Shared _logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    Private Property AppPath As String = String.Empty

    Private Property Destination As String = String.Empty

    Private Property DoMove As Boolean = False

    Public Property Sources() As List(Of String) = New List(Of String)

#End Region 'Properties

#Region "Methods"

    Public Sub New(ByVal appPath As String, ByVal destination As String, ByVal doMove As Boolean)
        Me.AppPath = appPath
        Me.Destination = destination
        Me.DoMove = doMove
    End Sub

    Public Sub RunTeraCopy()
        Try
            If File.Exists(AppPath) Then
                If Not String.IsNullOrEmpty(Destination) AndAlso Sources.Count > 0 Then
                    If Not Directory.Exists(Destination) Then
                        Directory.CreateDirectory(Destination)
                    End If

                    Dim BatchListPath As String = Path.Combine(Master.TempPath, "batchlist.txt")

                    Using sw As StreamWriter = New StreamWriter(BatchListPath, False, Text.Encoding.Unicode)
                        For Each Movie As String In Sources
                            sw.Write(String.Concat(Movie.ToString, Environment.NewLine))
                        Next
                        sw.Close()
                    End Using

                    Dim p As New Process()
                    Dim Arguments As String = String.Format("{0} *""{1}"" ""{2}""",
                                                                If(DoMove, "move", "copy"),
                                                                BatchListPath,
                                                                Destination)
                    p.StartInfo = New ProcessStartInfo(AppPath, Arguments) With {
                            .CreateNoWindow = True,
                            .WindowStyle = ProcessWindowStyle.Normal
                        }
                    p.Start()
                End If
            End If
        Catch ex As Exception
            _logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

End Class