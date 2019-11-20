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

Namespace TeraCopy
    Public Class Filelist

#Region "Fields"

        Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

        Public Property Sources() As List(Of String) = New List(Of String)

#End Region 'Properties

#Region "Methods"

        Public Sub RunTeraCopy(ByVal AppPath As String, ByVal Destination As String, ByVal DoMove As Boolean)
            Try
                If File.Exists(AppPath) Then
                    If Not String.IsNullOrEmpty(Destination) AndAlso _Sources.Count > 0 Then
                        If Not Directory.Exists(Destination) Then
                            Directory.CreateDirectory(Destination)
                        End If

                        Dim BatchListPath As String = Path.Combine(Master.TempPath, "batchlist.txt")

                        Using sw As StreamWriter = New StreamWriter(BatchListPath, False, System.Text.Encoding.Unicode)
                            For Each Movie As String In _Sources
                                sw.Write(Movie.ToString & Environment.NewLine)
                            Next
                            sw.Close()
                        End Using

                        Dim p As New Process()
                        Dim Arguments As String = String.Concat("""""", If(DoMove, "move", "copy"), " *""", BatchListPath, """ """, Destination, """""")
                        p.StartInfo = New ProcessStartInfo(AppPath, Arguments) With {
                            .CreateNoWindow = True,
                            .WindowStyle = ProcessWindowStyle.Hidden
                        }
                        p.Start()
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

#End Region 'Methods

    End Class

End Namespace