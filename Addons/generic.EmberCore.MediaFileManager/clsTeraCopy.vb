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

Imports NLog
Imports System.IO
Imports EmberAPI

Namespace TeraCopy
    Public Class Filelist

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _apppath As String
        Private _destination As String
        Private _doMove As Boolean
        Private _sources As New List(Of String)

#End Region 'Fields

#Region "Properties"

        Public Property Sources() As List(Of String)
            Get
                Return Me._sources
            End Get
            Set(ByVal value As List(Of String))
                Me._sources = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub New(ByVal AppPath As String, ByVal Destination As String, ByVal doMove As Boolean)
            Me.Clear()
            Me._apppath = AppPath
            Me._destination = Destination
            Me._doMove = doMove
        End Sub

        Private Sub Clear()
            _apppath = String.Empty
            _destination = String.Empty
            _doMove = False
            _sources.Clear()
        End Sub

        Public Sub RunTeraCopy()
            Try
                If File.Exists(_apppath) Then
                    If Not String.IsNullOrEmpty(Me._destination) AndAlso Me._sources.Count > 0 Then
                        If Not Directory.Exists(_destination) Then
                            Directory.CreateDirectory(_destination)
                        End If

                        Dim BatchListPath As String = Path.Combine(Master.TempPath, "batchlist.txt")

                        Using sw As StreamWriter = New StreamWriter(BatchListPath, False, System.Text.Encoding.Unicode)
                            For Each Movie As String In _sources
                                sw.Write(Movie.ToString & Environment.NewLine)
                            Next
                            sw.Close()
                        End Using

                        Dim p As New Process()
                        Dim Arguments As String = String.Format("{0} *""{1}"" ""{2}""",
                                                                If(_doMove, "move", "copy"),
                                                                BatchListPath,
                                                                _destination)
                        p.StartInfo = New ProcessStartInfo(_apppath, Arguments)
                        p.StartInfo.CreateNoWindow = True
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                        p.Start()
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

#End Region 'Methods

    End Class

End Namespace
