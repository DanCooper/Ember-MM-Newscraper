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
Imports NLog
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Xml.Serialization
Imports System.Xml

Namespace ThemeLibrary

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _themelist As New List(Of Themes)

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal tDBElement As Database.DBElement)
            Clear()
            GetThemes(tDBElement)
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ThemeList() As List(Of Themes)
            Get
                Return _themelist
            End Get
            Set(ByVal value As List(Of Themes))
                _themelist = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _themelist = New List(Of Themes)
        End Sub

        Private Sub GetThemes(ByVal tDBElement As Database.DBElement)
            Dim strSearchURL As String = String.Empty
            Dim strTitle As String = String.Empty

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie
                    If tDBElement.Movie.IMDBSpecified Then
                        strSearchURL = String.Concat("http://kodi.ziggy73701.seedr.io/TvTunes/movies/", tDBElement.Movie.IMDB)
                        strTitle = tDBElement.Movie.Title
                    End If
                Case Enums.ContentType.TVShow
                    If tDBElement.TVShow.TVDBSpecified Then
                        strSearchURL = String.Concat("http://kodi.ziggy73701.seedr.io/TvTunes/tvshows/", tDBElement.TVShow.TVDB)
                        strTitle = tDBElement.TVShow.Title
                    End If
            End Select

            Try
                If Not String.IsNullOrEmpty(strSearchURL) Then
                    Dim sPattern As String = "<a href=""(?<URL>.*?)"">"
                    Dim tInfo As New InfoXML

                    Dim sHTTP As New HTTP
                    Dim Html As String = sHTTP.DownloadData(strSearchURL)
                    sHTTP = Nothing

                    If Not String.IsNullOrEmpty(Html) Then
                        Dim sResult As MatchCollection = Regex.Matches(Html, sPattern, RegexOptions.Singleline)

                        Try
                            'search info.xml file
                            For ctr As Integer = 0 To sResult.Count - 1
                                If sResult.Item(ctr).Groups("URL").Value.Trim = "info.xml" Then
                                    Dim xmlSer As XmlSerializer = Nothing
                                    Using xmlSR As XmlTextReader = New XmlTextReader(String.Concat(strSearchURL, "/", sResult.Item(ctr).Groups("URL").Value.Trim))
                                        xmlSer = New XmlSerializer(GetType(InfoXML))
                                        tInfo = DirectCast(xmlSer.Deserialize(xmlSR), InfoXML)
                                    End Using
                                    Exit For
                                End If
                            Next
                        Catch ex As Exception
                            logger.Error(ex, New StackFrame().GetMethod().Name)
                        End Try

                        If String.IsNullOrEmpty(tInfo.Name) Then
                            tInfo.Name = strTitle
                        End If

                        'search themes
                        For ctr As Integer = 0 To sResult.Count - 1
                            If Master.eSettings.FileSystemValidThemeExts.Contains(Path.GetExtension(sResult.Item(ctr).Groups("URL").Value.Trim)) Then
                                Dim strURL As String = String.Concat(strSearchURL, "/", sResult.Item(ctr).Groups("URL").Value.Trim)
                                _themelist.Add(New Themes With {.Title = tInfo.Name, .URL = strURL, .WebURL = strURL})
                            End If
                        Next
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Private Function GetDownloadURL(ByVal strURL As String) As String
            If Not String.IsNullOrEmpty(strURL) Then
                Dim sHTTP As New HTTP
                Dim Html As String = sHTTP.DownloadData(strURL)
                sHTTP = Nothing

                If Not String.IsNullOrEmpty(Html) Then
                    Dim sResult As MatchCollection = Regex.Matches(Html, "<input id=""song_name"" type=""hidden"" value=""(?<URL>.*?)""", RegexOptions.Singleline)
                    If sResult.Count > 0 Then
                        Return sResult.Item(0).Groups("URL").Value.Trim
                    End If
                End If
            End If
            Return String.Empty
        End Function

#End Region 'Methods

#Region "Nested Types"

        <Serializable()>
        <XmlRoot("info")>
        Class InfoXML

#Region "Fields"

            Private _imdb As String
            Private _name As String
            Private _tmdb As String
            Private _tvdb As String

#End Region 'Fields

#Region "Properties"

            <XmlElement("imdb")>
            Public Property IMDB() As String
                Get
                    Return _imdb
                End Get
                Set(ByVal value As String)
                    _imdb = value
                End Set
            End Property

            <XmlElement("name")>
            Public Property Name() As String
                Get
                    Return _name
                End Get
                Set(ByVal value As String)
                    _name = value
                End Set
            End Property

            <XmlElement("tmdb")>
            Public Property TMDB() As String
                Get
                    Return _tmdb
                End Get
                Set(ByVal value As String)
                    _tmdb = value
                End Set
            End Property

            <XmlElement("tvdb")>
            Public Property TVDB() As String
                Get
                    Return _tvdb
                End Get
                Set(ByVal value As String)
                    _tvdb = value
                End Set
            End Property

#End Region 'Properties

#Region "Constructors"

            Public Sub New()
                Clear()
            End Sub

#End Region 'Constructors

#Region "Methods"

            Public Sub Clear()
                _imdb = String.Empty
                _name = String.Empty
                _tmdb = String.Empty
                _tvdb = String.Empty
            End Sub

#End Region 'Methods

        End Class

#End Region 'Nested Types

    End Class

End Namespace

