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

Imports System.Data
Imports System.IO
Imports System.Text

Imports ICSharpCode.SharpZipLib.Zip
Imports EmberAPI
Imports NLog
Imports System.Xml.Serialization

Public Class Scraper


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    ''' <summary>
    ''' Please use the APIKey property, and NOT this variable.
    ''' </summary>
    Private Shared _APIKey As String = String.Empty
    ''' <summary>
    ''' APIKey has the potential to be undefined, or at least http://thetvdb.com/?tab=apiregister
    ''' Strongly recommend using the Property, so a warning can be issued if the APIKey is used without being
    ''' adequately initialized
    ''' </summary>
    Public Shared Property APIKey As String
        Get
            If (_APIKey.Contains("http://")) Then
                logger.Warn("The API key for TheTVDB.com has not been set. Expect some errors.", New StackTrace().ToString())
            End If

            Return _APIKey
        End Get
        Set(value As String)
            _APIKey = value
        End Set
    End Property
    Public Shared _TVDBMirror As String = String.Empty

    Public Shared WithEvents sObject As New ScraperObject
    Public Shared tEpisodes As New List(Of MediaContainers.EpisodeDetails)
    Public Shared tmpTVDBShow As New TVDBShow
    Public Shared TVDBImages As New TVImages
    Public Shared onlyScrapeSeason As Integer

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal _Api As String)
        AddHandler sObject.ScraperEvent, AddressOf InnerEvent
        APIKey = _Api
        _TVDBMirror = clsAdvancedSettings.GetSetting("TVDBMirror", "thetvdb.com")
    End Sub

#End Region 'Constructors

#Region "Events"

    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)

#End Region 'Events

#Region "Methods"

    Public Sub CancelAsync()
        sObject.CancelAsync()
    End Sub

    Public Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String) As MediaContainers.EpisodeDetails
        Return sObject.ChangeEpisode(New Structures.ScrapeInfo With {.ShowID = ShowID, .TVDBID = TVDBID, .ShowLang = Lang, .iSeason = -999})
    End Function

    Public Function GetLangs(ByVal sMirror As String) As clsXMLTVDBLanguages
        Dim sHTTP As New HTTP
        Dim aTVDBLang As New clsXMLTVDBLanguages

        Dim apiXML As String = sHTTP.DownloadData(String.Format("http://{0}/api/{1}/languages.xml", sMirror, APIKey))
        sHTTP = Nothing
        Using reader As StringReader = New StringReader(apiXML)
            Dim xTVDBLang As New XmlSerializer(aTVDBLang.GetType)
            aTVDBLang = CType(xTVDBLang.Deserialize(reader), clsXMLTVDBLanguages)
        End Using

        Return aTVDBLang
    End Function

    Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As MediaContainers.EpisodeDetails
        Return sObject.GetSingleEpisode(New Structures.ScrapeInfo With {.ShowID = ShowID, .TVDBID = TVDBID, .iSeason = Season, .iEpisode = Episode, .showLang = Lang, .Ordering = Ordering, .Options = Options})
    End Function

    Public Sub GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByRef RetImage As Images)
        sObject.GetSingleImage(New Structures.ScrapeInfo With {.ShowTitle = Title, .ShowID = ShowID, .TVDBID = TVDBID, .ImageType = Type, .iSeason = Season, .iEpisode = Episode, .showLang = Lang, .Ordering = Ordering, .CurrentImage = CurrentImage}, RetImage)
    End Sub

    Public Sub InnerEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)
        RaiseEvent ScraperEvent(eType, iProgress, Parameter)
    End Sub

    Public Function IsBusy() As Boolean
        Return sObject.IsBusy
    End Function

    Public Sub SaveImages()
        sObject.SaveImages()
    End Sub

    Public Sub ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions)
        sObject.ScrapeEpisode(New Structures.ScrapeInfo With {.ShowID = ShowID, .ShowTitle = ShowTitle, .TVDBID = TVDBID, .iEpisode = iEpisode, .iSeason = iSeason, .ShowLang = Lang, .Ordering = Ordering, .Options = Options})
    End Sub

    Public Sub ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions)
        sObject.ScrapeSeason(New Structures.ScrapeInfo With {.ShowID = ShowID, .ShowTitle = ShowTitle, .TVDBID = TVDBID, .iSeason = iSeason, .ShowLang = Lang, .Ordering = Ordering, .Options = Options})
    End Sub

    Public Sub SingleScrape(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal ShowLang As String, ByVal SourceLang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean)
        sObject.SingleScrape(New Structures.ScrapeInfo With {.ShowID = ShowID, .ShowTitle = ShowTitle, .TVDBID = TVDBID, .ShowLang = ShowLang, .SourceLang = SourceLang, .Ordering = Ordering, .Options = Options, .ScrapeType = ScrapeType, .WithCurrent = WithCurrent, .iSeason = -999})
    End Sub

#End Region 'Methods

#Region "Nested Types"

    <Serializable()> _
    Public Structure TVImages

#Region "Fields"

        Dim AllSeasonsPoster As TVDBPoster
        Dim AllSeasonsBanner As TVDBShowBanner
        Dim AllSeasonsFanart As TVDBFanart
        Dim SeasonImageList As List(Of TVDBSeasonImage)
        Dim ShowFanart As TVDBFanart
        Dim ShowPoster As TVDBPoster
        Dim ShowBanner As TVDBShowBanner

#End Region 'Fields

#Region "Methods"
        'TODO: make the new class serializable
        Public Function Clone() As TVImages
            Dim newTVI As New TVImages
            'Try
            '    Using ms As New IO.MemoryStream()
            '        Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            '        bf.Serialize(ms, Me)
            '        ms.Position = 0
            '        newTVI = DirectCast(bf.Deserialize(ms), TVImages)
            '        ms.Close()
            '    End Using
            'Catch ex As Exception
            '    logger.Error(New StackFrame().GetMethod().Name,ex)
            'End Try
            Return newTVI
        End Function

#End Region 'Methods

    End Structure

    Public Class ScraperObject

#Region "Fields"

        Friend WithEvents bwTVDB As New System.ComponentModel.BackgroundWorker

        Private aXML As String = String.Empty
        Private bXML As String = String.Empty
        Private sXML As String = String.Empty

#End Region 'Fields

#Region "Events"

        Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)

#End Region 'Events

#Region "Methods"

        Public Shared Sub LoadAllEpisodes(ByVal _ID As Integer, ByVal OnlySeason As Integer)
            Try

                tmpTVDBShow = New TVDBShow

                tmpTVDBShow.Show = Master.DB.LoadTVFullShowFromDB(_ID)
                tmpTVDBShow.AllSeason = Master.DB.LoadTVAllSeasonFromDB(_ID)

                Using SQLCount As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If OnlySeason = 999 Then
                        SQLCount.CommandText = String.Concat("SELECT COUNT(ID) AS eCount FROM TVEps WHERE TVShowID = ", _ID, " AND Missing = 0;")
                    Else
                        SQLCount.CommandText = String.Concat("SELECT COUNT(ID) AS eCount FROM TVEps WHERE TVShowID = ", _ID, " AND Season = ", OnlySeason, " AND Missing = 0;")
                    End If
                    Using SQLRCount As SQLite.SQLiteDataReader = SQLCount.ExecuteReader
                        If SQLRCount.HasRows Then
                            SQLRCount.Read()
                            If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                    If OnlySeason = 999 Then
                                        SQLCommand.CommandText = String.Concat("SELECT ID, Lock FROM TVEps WHERE TVShowID = ", _ID, " AND Missing = 0;")
                                    Else
                                        SQLCommand.CommandText = String.Concat("SELECT ID, Lock FROM TVEps WHERE TVShowID = ", _ID, " AND Season = ", OnlySeason, " AND Missing = 0;")
                                    End If
                                    Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                                        While SQLReader.Read
                                            tmpTVDBShow.Episodes.Add(Master.DB.LoadTVEpFromDB(Convert.ToInt64(SQLReader("ID")), True))
                                        End While
                                    End Using
                                End Using
                            End If
                        End If

                    End Using
                End Using
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub CancelAsync()
            If bwTVDB.IsBusy Then bwTVDB.CancelAsync()
        End Sub

        Public Function ChangeEpisode(ByVal sInfo As Structures.ScrapeInfo) As MediaContainers.EpisodeDetails
            Try
                Dim tEpisodes As List(Of MediaContainers.EpisodeDetails) = Me.GetListOfKnownEpisodes(sInfo)
                If tEpisodes.Count > 0 Then
                    Using dChangeEp As New dlgTVChangeEp
                        Return dChangeEp.ShowDialog(tEpisodes)
                    End Using
                Else
                    MsgBox(Master.eLang.GetString(943, "There are no known episodes for this show. Scrape the show, season, or episode and try again."), MsgBoxStyle.OkOnly, Master.eLang.GetString(944, "No Known Episodes"))
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return Nothing
        End Function

        Public Sub DownloadSeries(ByVal sInfo As Structures.ScrapeInfo, Optional ByVal ImagesOnly As Boolean = False)
            Try
                Dim fPath As String = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sInfo.TVDBID, Path.DirectorySeparatorChar, sInfo.ShowLang, ".zip"))
                Dim fExists As Boolean = File.Exists(fPath)
                Dim doDownload As Boolean = False

                Select Case Master.eSettings.TVScraperUpdateTime
                    Case Enums.TVScraperUpdateTime.Always
                        doDownload = True
                    Case Enums.TVScraperUpdateTime.Never
                        doDownload = False
                    Case Enums.TVScraperUpdateTime.Week
                        If fExists AndAlso File.GetCreationTime(fPath).AddDays(7) < Now Then doDownload = True
                    Case Enums.TVScraperUpdateTime.BiWeekly
                        If fExists AndAlso File.GetCreationTime(fPath).AddDays(14) < Now Then doDownload = True
                    Case Enums.TVScraperUpdateTime.Month
                        If fExists AndAlso File.GetCreationTime(fPath).AddMonths(1) < Now Then doDownload = True
                End Select

                If doDownload OrElse Not fExists Then
                    Using sHTTP As New HTTP
                        Dim xZip As Byte() = sHTTP.DownloadZip(String.Format("http://{0}/api/{1}/series/{2}/all/{3}.zip", _TVDBMirror, APIKey, sInfo.TVDBID, sInfo.ShowLang))

                        If Not IsNothing(xZip) AndAlso xZip.Length > 0 Then
                            'save it to the temp dir
                            Directory.CreateDirectory(Directory.GetParent(fPath).FullName)
                            Using fStream As FileStream = New FileStream(fPath, FileMode.Create, FileAccess.Write)
                                fStream.Write(xZip, 0, xZip.Length)
                            End Using

                            Me.ProcessTVDBZip(xZip, sInfo)
                            Me.ShowFromXML(sInfo, ImagesOnly)
                        End If
                    End Using
                Else
                    Using fStream As FileStream = New FileStream(fPath, FileMode.Open, FileAccess.Read)
                        Dim fZip As Byte() = Functions.ReadStreamToEnd(fStream)

                        Me.ProcessTVDBZip(fZip, sInfo)
                        Me.ShowFromXML(sInfo, ImagesOnly)
                    End Using
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub
        Public Sub DownloadSeriesAsync(ByVal sInfo As Structures.ScrapeInfo)
            Try
                If Not bwTVDB.IsBusy Then
                    RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.StartingDownload, 0, Nothing)
                    bwTVDB.WorkerReportsProgress = True
                    bwTVDB.WorkerSupportsCancellation = True
                    bwTVDB.RunWorkerAsync(New Arguments With {.Type = 1, .Parameter = sInfo})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Function GetListOfKnownEpisodes(ByVal sInfo As Structures.ScrapeInfo) As List(Of MediaContainers.EpisodeDetails)
            Dim Actors As New List(Of MediaContainers.Person)
            Dim tEpisodes As New List(Of MediaContainers.EpisodeDetails)
            Dim tEpisode As New MediaContainers.EpisodeDetails
            Dim fPath As String = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sInfo.TVDBID, Path.DirectorySeparatorChar, sInfo.ShowLang, ".zip"))
            Dim tSeas As Integer = -1
            Dim tOrdering As Enums.Ordering = Enums.Ordering.Standard

            Try
                If File.Exists(fPath) Then

                    'Check how old the ZIP file is. If it's older than 12h -> try to get a fresh/updated file
                    'This is usefull for non english users because TVDB can has much "not yet translated" information if the file is to old
                    Dim fileInfo As New FileInfo(fPath)
                    If fileInfo.LastWriteTime < DateTime.Now.AddHours(-12) Then
                        DownloadSeries(sInfo)
                    End If

                    Using fStream As FileStream = New FileStream(fPath, FileMode.Open, FileAccess.Read)
                        Dim fZip As Byte() = Functions.ReadStreamToEnd(fStream)
                        Me.ProcessTVDBZip(fZip, sInfo)

                        'get the actors first
                        Try
                            If Not String.IsNullOrEmpty(aXML) Then
                                Dim xdActors As XDocument = XDocument.Parse(aXML)
                                For Each Actor As XElement In xdActors.Descendants("Actor")
                                    If Not IsNothing(Actor.Element("Name")) AndAlso Not String.IsNullOrEmpty(Actor.Element("Name").Value) Then
                                        Actors.Add(New MediaContainers.Person With {.Name = Actor.Element("Name").Value, .Role = If(IsNothing(Actor.Element("Role")), String.Empty, Actor.Element("Role").Value), .Thumb = If(IsNothing(Actor.Element("Image")) OrElse String.IsNullOrEmpty(Actor.Element("Image").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, Actor.Element("Image").Value))})
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try

                        If Not String.IsNullOrEmpty(sXML) Then
                            Dim xdEps As XDocument = XDocument.Parse(sXML)

                            For Each Episode As XElement In xdEps.Descendants("Episode")
                                If Not IsNothing(Episode.Element("EpisodeName").Value) AndAlso Not String.IsNullOrEmpty(Episode.Element("EpisodeName").Value) Then
                                    tEpisode = New MediaContainers.EpisodeDetails

                                    tOrdering = Enums.Ordering.Standard

                                    If sInfo.Ordering = Enums.Ordering.DVD Then
                                        If Not IsNothing(Episode.Element("SeasonNumber")) AndAlso Not String.IsNullOrEmpty(Episode.Element("SeasonNumber").Value.ToString) AndAlso _
                                        Not IsNothing(Episode.Element("DVD_season")) AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_season").Value.ToString) AndAlso _
                                        Not IsNothing(Episode.Element("DVD_episodenumber")) AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_episodenumber").Value.ToString) Then
                                            tSeas = Convert.ToInt32(Episode.Element("SeasonNumber").Value)
                                            If sInfo.iSeason >= 0 AndAlso Not tSeas = sInfo.iSeason Then Continue For
                                            If xdEps.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("SeasonNumber").Value) = tSeas AndAlso (IsNothing(e.Element("DVD_season")) OrElse String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString) OrElse IsNothing(e.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))).Count = 0 Then
                                                tOrdering = Enums.Ordering.DVD
                                            End If
                                        ElseIf Not IsNothing(Episode.Element("DVD_season")) AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_season").Value.ToString) AndAlso _
                                        Not IsNothing(Episode.Element("DVD_episodenumber")) AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_episodenumber").Value.ToString) Then
                                            tSeas = Convert.ToInt32(Episode.Element("DVD_season").Value)
                                            If xdEps.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("DVD_season").Value) = tSeas AndAlso (IsNothing(e.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))).Count = 0 Then
                                                tOrdering = Enums.Ordering.DVD
                                            End If
                                        End If
                                    ElseIf sInfo.Ordering = Enums.Ordering.Absolute Then
                                        If Not IsNothing(Episode.Element("absolute_number")) AndAlso Not String.IsNullOrEmpty(Episode.Element("absolute_number").Value.ToString) Then
                                            If xdEps.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("SeasonNumber").Value) > 0 AndAlso (IsNothing(e.Element("absolute_number")) OrElse String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString))).Count = 0 Then
                                                tOrdering = Enums.Ordering.Absolute
                                            End If
                                        End If
                                    Else
                                        If sInfo.iSeason >= 0 AndAlso Not Convert.ToInt32(Episode.Element("SeasonNumber").Value) = sInfo.iSeason Then Continue For
                                    End If

                                    With tEpisode
                                        .Title = Episode.Element("EpisodeName").Value
                                        If tOrdering = Enums.Ordering.DVD Then
                                            .Season = Convert.ToInt32(Episode.Element("DVD_season").Value)
                                            .Episode = Convert.ToInt32(CLng(Episode.Element("DVD_episodenumber").Value))
                                        ElseIf tOrdering = Enums.Ordering.Absolute Then
                                            .Season = 1
                                            .Episode = Convert.ToInt32(Episode.Element("absolute_number").Value)
                                        Else
                                            .Season = If(IsNothing(Episode.Element("SeasonNumber")) OrElse String.IsNullOrEmpty(Episode.Element("SeasonNumber").Value), 0, Convert.ToInt32(Episode.Element("SeasonNumber").Value))
                                            .Episode = If(IsNothing(Episode.Element("EpisodeNumber")) OrElse String.IsNullOrEmpty(Episode.Element("EpisodeNumber").Value), 0, Convert.ToInt32(Episode.Element("EpisodeNumber").Value))
                                        End If
                                        If Not IsNothing(Episode.Element("airsafter_season")) AndAlso Not String.IsNullOrEmpty(Episode.Element("airsafter_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(Episode.Element("airsafter_season").Value)
                                            .DisplayEpisode = 4096
                                            .displaySEset = True
                                        End If
                                        If Not IsNothing(Episode.Element("airsbefore_season")) AndAlso Not String.IsNullOrEmpty(Episode.Element("airsbefore_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(Episode.Element("airsbefore_season").Value)
                                            .displaySEset = True
                                        End If
                                        If Not IsNothing(Episode.Element("airsbefore_episode")) AndAlso Not String.IsNullOrEmpty(Episode.Element("airsbefore_episode").Value) Then
                                            .DisplayEpisode = Convert.ToInt32(CLng(Episode.Element("airsbefore_episode").Value))
                                            .displaySEset = True
                                        End If

                                        .Aired = If(IsNothing(Episode.Element("FirstAired")), String.Empty, Episode.Element("FirstAired").Value)
                                        .Rating = If(IsNothing(Episode.Element("Rating")), String.Empty, Episode.Element("Rating").Value)
                                        .Votes = If(IsNothing(Episode.Element("RatingCount")), String.Empty, Episode.Element("RatingCount").Value)
                                        .Plot = If(IsNothing(Episode.Element("Overview")), String.Empty, Episode.Element("Overview").Value.ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf))
                                        .Director = If(IsNothing(Episode.Element("Director")), String.Empty, Strings.Join(Episode.Element("Director").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|")), " / "))
                                        .OldCredits = If(IsNothing(Episode.Element("Writer")), String.Empty, Strings.Join(Episode.Element("Writer").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|")), " / "))
                                        .Actors = Actors
                                        .PosterURL = If(IsNothing(Episode.Element("filename")), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, Episode.Element("filename").Value))
                                        .LocalFile = If(IsNothing(Episode.Element("filename")), String.Empty, Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sInfo.TVDBID, Path.DirectorySeparatorChar, "episodeposters", Path.DirectorySeparatorChar, Episode.Element("filename").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))))
                                    End With

                                    tEpisodes.Add(tEpisode)
                                End If
                            Next

                        End If
                    End Using
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return tEpisodes
        End Function

        Public Sub GetSearchResultsAsync(ByVal sInfo As Structures.ScrapeInfo)
            Try
                If Not bwTVDB.IsBusy Then
                    bwTVDB.WorkerReportsProgress = True
                    bwTVDB.WorkerSupportsCancellation = True
                    bwTVDB.RunWorkerAsync(New Arguments With {.Type = 0, .Parameter = sInfo})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Function GetSingleEpisode(ByVal sInfo As Structures.ScrapeInfo) As MediaContainers.EpisodeDetails
            Dim tEp As New MediaContainers.EpisodeDetails
            Try

                tEp = Me.GetListOfKnownEpisodes(sInfo).FirstOrDefault(Function(e) e.Season = sInfo.iSeason AndAlso e.Episode = sInfo.iEpisode)

                If Not IsNothing(tEp) Then
                    Return tEp
                Else
                    DownloadSeries(sInfo)
                    tEp = Me.GetListOfKnownEpisodes(sInfo).FirstOrDefault(Function(e) e.Season = sInfo.iSeason AndAlso e.Episode = sInfo.iEpisode)
                    If Not IsNothing(tEp) Then
                        Return tEp
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return New MediaContainers.EpisodeDetails
        End Function

        Public Sub GetSingleImage(ByVal sInfo As Structures.ScrapeInfo, ByRef RetImage As Images)
            tmpTVDBShow = New TVDBShow

            If sInfo.ImageType = Enums.TVImageType.EpisodePoster Then

                If String.IsNullOrEmpty(sInfo.TVDBID) Then
                    Using dTVDBSearch As New dlgTVDBSearchResults
                        sInfo = dTVDBSearch.ShowDialog(sInfo, True)
                        If Not String.IsNullOrEmpty(sInfo.TVDBID) Then
                            Master.currShow.TVShow.ID = sInfo.TVDBID

                            Dim tmpEp As MediaContainers.EpisodeDetails = Me.GetListOfKnownEpisodes(sInfo).FirstOrDefault(Function(e) e.Episode = sInfo.iEpisode AndAlso e.Season = sInfo.iSeason)
                            If Not IsNothing(tmpEp) Then

                                If File.Exists(tmpEp.LocalFile) Then
                                    RetImage.FromFile(tmpEp.LocalFile)
                                Else
                                    RetImage.FromWeb(tmpEp.PosterURL)
                                    If Not IsNothing(RetImage.Image) Then
                                        Directory.CreateDirectory(Directory.GetParent(tmpEp.LocalFile).FullName)
                                        RetImage.Save(tmpEp.LocalFile)
                                    End If
                                End If

                                If Not IsNothing(RetImage.Image) Then
                                    Using dPosterConfirm As New dlgTVEpisodePoster
                                        If Not (dPosterConfirm.ShowDialog(RetImage.Image) = DialogResult.OK) Then
                                            RetImage.Dispose()
                                        End If
                                    End Using
                                Else
                                    MsgBox(Master.eLang.GetString(945, "There is no poster available for this episode."), MsgBoxStyle.OkOnly, Master.eLang.GetString(946, "No Posters Found"))
                                    RetImage.Dispose()
                                End If
                            Else
                                RetImage.Dispose()
                            End If
                        Else
                            RetImage.Dispose()
                        End If
                    End Using
                Else
                    Dim tmpEp As MediaContainers.EpisodeDetails = Me.GetListOfKnownEpisodes(sInfo).FirstOrDefault(Function(e) e.Episode = sInfo.iEpisode AndAlso e.Season = sInfo.iSeason)
                    If Not IsNothing(tmpEp) Then
                        If File.Exists(tmpEp.LocalFile) Then
                            RetImage.FromFile(tmpEp.LocalFile)
                        Else
                            RetImage.FromWeb(tmpEp.PosterURL)
                            If Not IsNothing(RetImage.Image) Then
                                Directory.CreateDirectory(Directory.GetParent(tmpEp.LocalFile).FullName)
                                RetImage.Save(tmpEp.LocalFile)
                            End If
                        End If

                        If Not IsNothing(RetImage.Image) Then
                            Using dPosterConfirm As New dlgTVEpisodePoster
                                If Not (dPosterConfirm.ShowDialog(RetImage.Image) = DialogResult.OK) Then
                                    RetImage.Dispose()
                                End If
                            End Using
                        Else
                            MsgBox(Master.eLang.GetString(945, "There is no poster available for this episode."), MsgBoxStyle.OkOnly, Master.eLang.GetString(946, "No Posters Found"))
                            RetImage.Dispose()
                        End If
                    Else
                        RetImage.Dispose()
                    End If
                End If
            Else
                If String.IsNullOrEmpty(sInfo.TVDBID) Then
                    Using dTVDBSearch As New dlgTVDBSearchResults
                        sInfo = dTVDBSearch.ShowDialog(sInfo, True)
                        If Not String.IsNullOrEmpty(sInfo.TVDBID) Then
                            Master.currShow.TVShow.ID = sInfo.TVDBID
                            Me.DownloadSeries(sInfo, True)
                            Using dImageSelect As New dlgTVImageSelect
                                RetImage = dImageSelect.ShowDialog(sInfo.ShowID, sInfo.ImageType, sInfo.iSeason, sInfo.CurrentImage)
                            End Using
                        Else
                            RetImage.Dispose()
                        End If
                    End Using
                Else
                    Me.DownloadSeries(sInfo, True)
                    Using dImageSelect As New dlgTVImageSelect
                        RetImage = dImageSelect.ShowDialog(sInfo.ShowID, sInfo.ImageType, sInfo.iSeason, sInfo.CurrentImage)
                    End Using
                End If
            End If
        End Sub

        Public Function IsBusy() As Boolean
            Return bwTVDB.IsBusy
        End Function

        Public Sub PassEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)
            RaiseEvent ScraperEvent(eType, iProgress, Parameter)
        End Sub

        Public Sub ProcessTVDBZip(ByVal tvZip As Byte(), ByVal sInfo As Structures.ScrapeInfo)
            sXML = String.Empty
            bXML = String.Empty
            aXML = String.Empty

            Try
                Using zStream As ZipInputStream = New ZipInputStream(New MemoryStream(tvZip))
                    Dim zEntry As ZipEntry = zStream.GetNextEntry

                    While Not IsNothing(zEntry)
                        Dim zBuffer As Byte() = Functions.ReadStreamToEnd(zStream)

                        Select Case True
                            Case zEntry.Name.Equals(String.Concat(sInfo.ShowLang, ".xml"))
                                sXML = System.Text.Encoding.UTF8.GetString(zBuffer)
                            Case zEntry.Name.Equals("banners.xml")
                                bXML = System.Text.Encoding.UTF8.GetString(zBuffer)
                            Case zEntry.Name.Equals("actors.xml")
                                aXML = System.Text.Encoding.UTF8.GetString(zBuffer)
                        End Select

                        zEntry = zStream.GetNextEntry
                    End While
                End Using
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SaveImages()
            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.SavingStarted, 0, Nothing)
            Me.bwTVDB = New System.ComponentModel.BackgroundWorker
            Me.bwTVDB.WorkerReportsProgress = True
            Me.bwTVDB.WorkerSupportsCancellation = True
            Me.bwTVDB.RunWorkerAsync(New Arguments With {.Type = 3})
        End Sub

        Public Sub ScrapeEpisode(ByVal sInfo As Structures.ScrapeInfo)
            Try
                tmpTVDBShow = New TVDBShow
                tmpTVDBShow.Episodes.Add(Master.currShow)

                If String.IsNullOrEmpty(sInfo.TVDBID) Then
                    RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Searching, 0, Nothing)
                    Using dTVDBSearch As New dlgTVDBSearchResults
                        If dTVDBSearch.ShowDialog(sInfo) = Windows.Forms.DialogResult.OK Then
                            Master.currShow = tmpTVDBShow.Episodes(0)
                            If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) AndAlso File.Exists(Master.currShow.TVEp.LocalFile) Then
                                Master.currShow.TVEp.Poster.FromWeb(Master.currShow.TVEp.PosterURL)
                                If Not IsNothing(Master.currShow.TVEp.Poster.Image) Then
                                    Directory.CreateDirectory(Directory.GetParent(Master.currShow.TVEp.LocalFile).FullName)
                                    Master.currShow.TVEp.Poster.Save(Master.currShow.TVEp.LocalFile)
                                End If
                            End If
                            If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) Then Master.currShow.EpPosterPath = Master.currShow.TVEp.LocalFile
                            If String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then Master.currShow.EpFanartPath = Master.currShow.ShowFanartPath

                            If Master.eSettings.TVScraperMetaDataScan Then MediaInfo.UpdateTVMediaInfo(Master.currShow)

                            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Verifying, 1, Nothing)
                        Else
                            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                        End If
                    End Using
                Else
                    DownloadSeries(sInfo)
                    If tmpTVDBShow.Episodes(0).TVShow.ID.Length > 0 Then
                        Master.currShow = tmpTVDBShow.Episodes(0)
                        If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) AndAlso Not File.Exists(Master.currShow.TVEp.LocalFile) Then
                            Master.currShow.TVEp.Poster.FromWeb(Master.currShow.TVEp.PosterURL)
                            If Not IsNothing(Master.currShow.TVEp.Poster.Image) Then
                                Directory.CreateDirectory(Directory.GetParent(Master.currShow.TVEp.LocalFile).FullName)
                                Master.currShow.TVEp.Poster.Save(Master.currShow.TVEp.LocalFile)
                            End If
                        End If
                        If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) Then Master.currShow.EpPosterPath = Master.currShow.TVEp.LocalFile
                        If String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then Master.currShow.EpFanartPath = Master.currShow.ShowFanartPath

                        If Master.eSettings.TVScraperMetaDataScan Then MediaInfo.UpdateTVMediaInfo(Master.currShow)

                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Verifying, 1, Nothing)
                    Else
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Searching, 0, Nothing)
                        Using dTVDBSearch As New dlgTVDBSearchResults
                            If dTVDBSearch.ShowDialog(sInfo) = Windows.Forms.DialogResult.OK Then
                                Master.currShow = tmpTVDBShow.Episodes(0)
                                If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) AndAlso Not File.Exists(Master.currShow.TVEp.LocalFile) Then
                                    Master.currShow.TVEp.Poster.FromWeb(Master.currShow.TVEp.PosterURL)
                                    If Not IsNothing(Master.currShow.TVEp.Poster) Then
                                        Directory.CreateDirectory(Directory.GetParent(Master.currShow.TVEp.LocalFile).FullName)
                                        Master.currShow.TVEp.Poster.Save(Master.currShow.TVEp.LocalFile)
                                    End If
                                End If
                                If Not String.IsNullOrEmpty(Master.currShow.TVEp.LocalFile) Then Master.currShow.EpPosterPath = Master.currShow.TVEp.LocalFile

                                If String.IsNullOrEmpty(Master.currShow.EpFanartPath) Then Master.currShow.EpFanartPath = Master.currShow.ShowFanartPath

                                If Master.eSettings.TVScraperMetaDataScan Then MediaInfo.UpdateTVMediaInfo(Master.currShow)

                                RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Verifying, 1, Nothing)
                            Else
                                RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                            End If
                        End Using
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub ScrapeSeason(ByVal sInfo As Structures.ScrapeInfo)
            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.LoadingEpisodes, 0, Nothing)
            bwTVDB.WorkerReportsProgress = True
            bwTVDB.WorkerSupportsCancellation = True
            bwTVDB.RunWorkerAsync(New Arguments With {.Type = 4, .Parameter = sInfo})
        End Sub

        Public Sub SingleScrape(ByVal sInfo As Structures.ScrapeInfo)
            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.LoadingEpisodes, 0, Nothing)
            bwTVDB.WorkerReportsProgress = False
            bwTVDB.WorkerSupportsCancellation = True
            bwTVDB.RunWorkerAsync(New Arguments With {.Type = 2, .Parameter = sInfo})
            While bwTVDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub

        Public Sub StartSingleScraper(ByVal sInfo As Structures.ScrapeInfo)
            Try
                If String.IsNullOrEmpty(sInfo.TVDBID) AndAlso sInfo.ScrapeType = Enums.ScrapeType.FullAsk Then
                    RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Searching, 0, Nothing)
                    Using dTVDBSearch As New dlgTVDBSearchResults
                        If dTVDBSearch.ShowDialog(sInfo) = Windows.Forms.DialogResult.OK Then
                            Master.currShow = tmpTVDBShow.Show
                            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.SelectImages, 0, Nothing)
                            Using dTVImageSel As New dlgTVImageSelect
                                If dTVImageSel.ShowDialog(sInfo.ShowID, Enums.TVImageType.All, sInfo.ScrapeType, sInfo.WithCurrent) = Windows.Forms.DialogResult.OK Then
                                    If Not IsNothing(sInfo.iSeason) AndAlso sInfo.iSeason >= 0 Then
                                        Me.SaveImages()
                                    Else
                                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Verifying, 0, Nothing)
                                    End If
                                Else
                                    RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                                End If
                            End Using
                        Else
                            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                        End If
                    End Using
                Else
                    DownloadSeries(sInfo)
                    If tmpTVDBShow.Show.TVShow.ID.Length > 0 Then
                        Master.currShow = tmpTVDBShow.Show
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.SelectImages, 0, Nothing)
                        Using dTVImageSel As New dlgTVImageSelect
                            If dTVImageSel.ShowDialog(sInfo.ShowID, Enums.TVImageType.All, sInfo.ScrapeType, sInfo.WithCurrent) = Windows.Forms.DialogResult.OK Then
                                If Not IsNothing(sInfo.iSeason) AndAlso sInfo.iSeason >= 0 Then
                                    Me.SaveImages()
                                Else
                                    If sInfo.ScrapeType = Enums.ScrapeType.FullAuto Then
                                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.SaveAuto, 0, Nothing)
                                    Else
                                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Verifying, 0, Nothing)
                                    End If
                                End If
                            Else
                                RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                            End If
                        End Using
                    ElseIf sInfo.ScrapeType = Enums.ScrapeType.FullAsk Then
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Searching, 0, Nothing)
                        Using dTVDBSearch As New dlgTVDBSearchResults
                            If dTVDBSearch.ShowDialog(sInfo) = Windows.Forms.DialogResult.OK Then
                                Master.currShow = tmpTVDBShow.Show
                                RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.SelectImages, 0, Nothing)
                                Using dTVImageSel As New dlgTVImageSelect
                                    If dTVImageSel.ShowDialog(sInfo.ShowID, Enums.TVImageType.All, sInfo.ScrapeType, sInfo.WithCurrent) = Windows.Forms.DialogResult.OK Then
                                        If Not IsNothing(sInfo.iSeason) AndAlso sInfo.iSeason >= 0 Then
                                            Me.SaveImages()
                                        Else
                                            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Verifying, 0, Nothing)
                                        End If
                                    Else
                                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                                    End If
                                End Using
                            Else
                                RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                            End If
                        End Using
                    Else
                        'Ignore Show scrape if ScrapeAuto and show don't have ID
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Cancelled, 0, Nothing)
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Sub bwtvDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)

            Try
                Select Case Args.Type
                    Case 0 'search
                        e.Result = New Results With {.Type = 0, .Result = SearchSeries(DirectCast(Args.Parameter, Structures.ScrapeInfo))}
                    Case 1 'show download
                        Me.DownloadSeries(DirectCast(Args.Parameter, Structures.ScrapeInfo))
                        e.Result = New Results With {.Type = 1}
                    Case 2 'load episodes
                        LoadAllEpisodes(DirectCast(Args.Parameter, Structures.ScrapeInfo).ShowID, 999)
                        onlyScrapeSeason = 999
                        e.Result = New Results With {.Type = 2, .Result = Args.Parameter}
                    Case 3 'save
                        Me.SaveAllTVInfo()
                        e.Result = New Results With {.Type = 3}
                    Case 4
                        Dim sInfo As Structures.ScrapeInfo = DirectCast(Args.Parameter, Structures.ScrapeInfo)
                        LoadAllEpisodes(sInfo.ShowID, sInfo.iSeason)
                        onlyScrapeSeason = sInfo.iSeason
                        e.Result = New Results With {.Type = 2, .Result = Args.Parameter}
                End Select
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Sub bwTVDB_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVDB.ProgressChanged
            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Progress, e.ProgressPercentage, e.UserState.ToString)
        End Sub

        Private Sub bwTVDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

            Try
                Select Case Res.Type
                    Case 0 'search
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.SearchResultsDownloaded, 0, DirectCast(Res.Result, List(Of TVSearchResults)))
                    Case 1 'show download
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.ShowDownloaded, 0, Nothing)
                    Case 2 'load episodes
                        If Not e.Cancelled Then
                            StartSingleScraper(DirectCast(Res.Result, Structures.ScrapeInfo))
                        Else
                            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.ScraperDone, 0, Nothing)
                        End If
                    Case 3 'save
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.ScraperDone, 0, Nothing)
                End Select
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Sub SaveAllTVInfo()
            Dim iEp As Integer = -1
            Dim iSea As Integer = -1
            Dim iProgress As Integer = 1

            Dim tShow As New Structures.DBTV
            Dim tEpisode As New MediaContainers.EpisodeDetails

            Me.bwTVDB.ReportProgress(tmpTVDBShow.Episodes.Count, "max")

            Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                If Master.eSettings.TVDisplayMissingEpisodes Then
                    'clear old missing episode from db
                    Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        If Not IsNothing(onlyScrapeSeason) AndAlso onlyScrapeSeason <> 999 Then
                            SQLCommand.CommandText = String.Concat("DELETE FROM TVEps WHERE Missing = 1 AND Season = ", onlyScrapeSeason, " AND TVShowID = ", Master.currShow.ShowID, ";")
                        Else
                            SQLCommand.CommandText = String.Concat("DELETE FROM TVEps WHERE Missing = 1 AND TVShowID = ", Master.currShow.ShowID, ";")
                        End If
                        SQLCommand.ExecuteNonQuery()
                    End Using
                End If

                Try
                    'For Each Episode As Structures.DBTV In tmpTVDBShow.Episodes

                    For i As Integer = 0 To tmpTVDBShow.Episodes.Count - 1
                        Try
                            If Me.bwTVDB.CancellationPending Then Return

                            Dim Episode As Structures.DBTV = tmpTVDBShow.Episodes.Item(i)

                            Episode.ShowID = Master.currShow.ShowID

                            iEp = Episode.TVEp.Episode
                            iSea = Episode.TVEp.Season

                            'remove it from tepisodes since it's a real episode
                            If Master.eSettings.TVDisplayMissingEpisodes Then
                                tEpisode = tEpisodes.FirstOrDefault(Function(e) e.Episode = iEp AndAlso e.Season = iSea)
                                If Not IsNothing(tEpisode) Then tEpisodes.Remove(tEpisode)
                                tShow = Episode
                            End If

                            If Me.bwTVDB.CancellationPending Then Return

                            If Episode.TVEp.Season > -1 AndAlso Episode.TVEp.Episode > -1 AndAlso Not Episode.IsLockEp Then
                                If Not IsNothing(Episode.TVEp.Poster.Image) Then Episode.EpPosterPath = Episode.TVEp.Poster.SaveAsTVEpisodePoster(Episode, Episode.TVEp.PosterURL)

                                If Me.bwTVDB.CancellationPending Then Return

                                If Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso Not IsNothing(Episode.TVEp.Fanart.Image) Then Episode.EpFanartPath = Episode.TVEp.Fanart.SaveAsTVEpisodeFanart(Episode, )

                                If Me.bwTVDB.CancellationPending Then Return

                                Dim cSea = From cSeason As TVDBSeasonImage In TVDBImages.SeasonImageList Where cSeason.Season = iSea Take 1
                                If cSea.Count > 0 Then
                                    If Not IsNothing(cSea(0).Poster.Image) Then Episode.SeasonPosterPath = cSea(0).Poster.SaveAsTVSeasonPoster(Episode)
                                    If Not IsNothing(cSea(0).Banner.Image) Then Episode.SeasonBannerPath = cSea(0).Banner.SaveAsTVSeasonBanner(Episode)

                                    If Me.bwTVDB.CancellationPending Then Return

                                    If Master.eSettings.TVSeasonFanartAnyEnabled Then
                                        If Not String.IsNullOrEmpty(cSea(0).Fanart.LocalFile) AndAlso File.Exists(cSea(0).Fanart.LocalFile) Then
                                            cSea(0).Fanart.Image.FromFile(cSea(0).Fanart.LocalFile)
                                            Episode.SeasonFanartPath = cSea(0).Fanart.Image.SaveAsTVSeasonFanart(Episode)
                                        ElseIf Not String.IsNullOrEmpty(cSea(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(cSea(0).Fanart.LocalFile) Then
                                            cSea(0).Fanart.Image.Clear()
                                            cSea(0).Fanart.Image.FromWeb(cSea(0).Fanart.URL)
                                            If Not IsNothing(cSea(0).Fanart.Image.Image) Then
                                                Directory.CreateDirectory(Directory.GetParent(cSea(0).Fanart.LocalFile).FullName)
                                                cSea(0).Fanart.Image.Save(cSea(0).Fanart.LocalFile)
                                                Episode.SeasonFanartPath = cSea(0).Fanart.Image.SaveAsTVSeasonFanart(Episode)
                                            End If
                                        End If
                                    End If
                                End If

                                If Me.bwTVDB.CancellationPending Then Return

                                If Master.eSettings.TVScraperMetaDataScan Then MediaInfo.UpdateTVMediaInfo(Episode)

                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVEpisode, Nothing, Nothing, False, , Episode)

                                Master.DB.SaveTVEpToDB(Episode, False, True, True, True)

                                'fix/workaround for multi-episode files after renaming
                                For e As Integer = 0 To tmpTVDBShow.Episodes.Count - 1
                                    If tmpTVDBShow.Episodes.Item(e).FilenameID = Episode.FilenameID AndAlso Not tmpTVDBShow.Episodes.Item(e).EpID = Episode.EpID Then
                                        Dim newEpDetails As New Structures.DBTV
                                        newEpDetails = tmpTVDBShow.Episodes.Item(e)
                                        newEpDetails.EpFanartPath = Episode.EpFanartPath
                                        newEpDetails.EpNfoPath = Episode.EpNfoPath
                                        newEpDetails.EpPosterPath = Episode.EpPosterPath
                                        newEpDetails.EpSubtitles = Episode.EpSubtitles
                                        newEpDetails.Filename = Episode.Filename
                                        tmpTVDBShow.Episodes.Item(e) = newEpDetails
                                    End If
                                Next

                                If Me.bwTVDB.CancellationPending Then Return
                            End If
                            Me.bwTVDB.ReportProgress(iProgress, "progress")

                            'If AdvancedSettings.GetBooleanSetting("ScrapeActorsThumbs", False) Then 
                            'For Each act As MediaContainers.Person In Episode.TVEp.Actors
                            'Dim img As New Images
                            'img.FromWeb(act.Thumb)
                            'img.SaveAsActorThumb(act, Directory.GetParent(Episode.Filename).FullName)
                            'Next
                            'End If

                            iProgress += 1
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    Next

                    'now save all missing episodes
                    If Master.eSettings.TVDisplayMissingEpisodes Then
                        tShow.Filename = String.Empty
                        tShow.EpFanartPath = String.Empty
                        tShow.EpPosterPath = String.Empty
                        tShow.EpNfoPath = String.Empty
                        tShow.SeasonFanartPath = String.Empty
                        tShow.SeasonPosterPath = String.Empty
                        tShow.ShowFanartPath = String.Empty
                        tShow.IsLockEp = False
                        tShow.IsMarkEp = False
                        tShow.EpID = -1
                        If tEpisodes.Count > 0 Then
                            For Each Episode As MediaContainers.EpisodeDetails In tEpisodes
                                tShow.TVEp = Episode
                                Master.DB.SaveTVEpToDB(tShow, True, True, True)
                            Next
                        End If
                    End If

                    If Me.bwTVDB.CancellationPending Then Return

                    SQLTrans.Commit()

                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try

            End Using
        End Sub

        Private Sub SaveToCache(ByVal sID As String, ByVal sURL As String, ByVal sPath As String)
            Dim sImage As New Images

            sImage.FromWeb(sURL)

            If Not IsNothing(sImage.Image) Then
                sImage.Save(Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, sPath.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))))
            End If
            sImage = Nothing
        End Sub

        Private Function SearchSeries(ByVal sInfo As Structures.ScrapeInfo) As List(Of TVSearchResults)
            Dim tvdbResults As New List(Of TVSearchResults)
            Dim cResult As New TVSearchResults
            Dim xmlTVDB As XDocument
            Dim tmpXML As XDocument
            Dim sHTTP As New HTTP
            Dim sLang As String = String.Empty
            Dim tmpID As String = String.Empty

            Try
                Dim apiXML As String = sHTTP.DownloadData(String.Format("http://{0}/api/GetSeries.php?seriesname={1}&language={2}", _TVDBMirror, sInfo.ShowTitle, sInfo.ShowLang))

                If Not String.IsNullOrEmpty(apiXML) Then
                    Try
                        xmlTVDB = XDocument.Parse(apiXML)
                    Catch
                        Return tvdbResults
                    End Try

                    Dim xSer = From xSeries In xmlTVDB.Descendants("Series") Where xSeries.HasElements

                    'check each unique showid to see if we have an entry for the preferred languages. If not, try to force download it
                    For Each tID As String In xSer.GroupBy(Function(s) s.Element("seriesid").Value.ToString).Select(Function(group) group.Key)
                        tmpID = tID
                        If xSer.Where(Function(s) s.Element("seriesid").Value.ToString = tmpID AndAlso s.Element("language").Value.ToString = sInfo.ShowLang).Count = 0 Then
                            'no preferred language in this series, force it
                            Dim forceXML As String = sHTTP.DownloadData(String.Format("http://{0}/api/{1}/series/{2}/{3}.xml", _TVDBMirror, APIKey, tmpID, sInfo.ShowLang))
                            If Not String.IsNullOrEmpty(forceXML) Then
                                Try
                                    tmpXML = XDocument.Parse(forceXML)
                                Catch
                                    Continue For
                                End Try

                                For Each tSer As XElement In tmpXML.Descendants("Series").Where(Function(s) s.HasElements)
                                    sLang = String.Empty
                                    cResult = New TVSearchResults
                                    cResult.ID = Convert.ToInt32(tSer.Element("id").Value)
                                    cResult.Name = If(Not IsNothing(tSer.Element("SeriesName")), tSer.Element("SeriesName").Value, String.Empty)
                                    If Not IsNothing(tSer.Element("Language")) AndAlso Master.eSettings.TVGeneralLanguages.Language.Count > 0 Then
                                        sLang = tSer.Element("Language").Value
                                        cResult.Language = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(s) s.abbreviation = sLang)
                                    ElseIf Not IsNothing(tSer.Element("Language")) Then
                                        sLang = tSer.Element("Language").Value
                                        cResult.Language = New TVDBLanguagesLanguage With {.name = String.Format("Unknown ({0})", sLang), .abbreviation = sLang, .id = 0}
                                    Else
                                        'no language info available... don't bother adding it
                                        Continue For
                                    End If
                                    cResult.Aired = If(Not IsNothing(tSer.Element("FirstAired")), tSer.Element("FirstAired").Value, String.Empty)
                                    cResult.Overview = If(Not IsNothing(tSer.Element("Overview")), tSer.Element("Overview").Value.ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf), String.Empty)
                                    cResult.Banner = If(Not IsNothing(tSer.Element("banner")), tSer.Element("banner").Value, String.Empty)
                                    If Not String.IsNullOrEmpty(cResult.Name) AndAlso Not String.IsNullOrEmpty(sLang) AndAlso xSer.Where(Function(s) s.Element("seriesid").Value.ToString = cResult.ID.ToString AndAlso s.Element("language").Value.ToString = sLang).Count = 0 Then
                                        cResult.Lev = StringUtils.ComputeLevenshtein(sInfo.ShowTitle, cResult.Name)
                                        tvdbResults.Add(cResult)
                                    End If
                                Next
                            End If
                        End If
                    Next
                    sHTTP = Nothing

                    For Each xS As XElement In xSer
                        sLang = String.Empty
                        cResult = New TVSearchResults
                        cResult.ID = Convert.ToInt32(xS.Element("seriesid").Value)
                        cResult.Name = If(Not IsNothing(xS.Element("SeriesName")), xS.Element("SeriesName").Value, String.Empty)
                        If Not IsNothing(xS.Element("language")) AndAlso Master.eSettings.TVGeneralLanguages.Language.Count > 0 Then
                            sLang = xS.Element("language").Value
                            cResult.Language = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(s) s.abbreviation = sLang)
                        ElseIf Not IsNothing(xS.Element("language")) Then
                            sLang = xS.Element("language").Value
                            cResult.Language = New TVDBLanguagesLanguage With {.name = String.Format("Unknown ({0})", sLang), .abbreviation = sLang, .id = 0}
                        Else
                            'no language info available... don't bother adding it
                            Continue For
                        End If
                        cResult.Aired = If(Not IsNothing(xS.Element("FirstAired")), xS.Element("FirstAired").Value, String.Empty)
                        cResult.Overview = If(Not IsNothing(xS.Element("Overview")), xS.Element("Overview").Value.ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf), String.Empty)
                        cResult.Banner = If(Not IsNothing(xS.Element("banner")), xS.Element("banner").Value, String.Empty)
                        If Not String.IsNullOrEmpty(cResult.Name) AndAlso Not String.IsNullOrEmpty(sLang) Then
                            cResult.Lev = StringUtils.ComputeLevenshtein(sInfo.ShowTitle, cResult.Name)
                            tvdbResults.Add(cResult)
                        End If
                    Next
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return tvdbResults
        End Function

        Private Sub ShowFromXML(ByVal sInfo As Structures.ScrapeInfo, ByVal ImagesOnly As Boolean)
            Dim Actors As New List(Of MediaContainers.Person)
            Dim sID As String = String.Empty
            Dim iEp As Integer = -1
            Dim iSeas As Integer = -1
            Dim sTitle As String = String.Empty
            Dim byTitle As Boolean = False
            Dim xE As XElement = Nothing
            Dim tShow As Structures.DBTV = tmpTVDBShow.Show
            Dim tOrdering As Enums.Ordering = Enums.Ordering.Standard

            If Not ImagesOnly Then
                If Master.eSettings.TVDisplayMissingEpisodes Then tEpisodes = Me.GetListOfKnownEpisodes(sInfo)

                'get the actors first
                Try
                    If sInfo.Options.bShowActors OrElse sInfo.Options.bEpActors Then
                        If Not String.IsNullOrEmpty(aXML) Then
                            Dim xdActors As XDocument = XDocument.Parse(aXML)
                            For Each Actor As XElement In xdActors.Descendants("Actor")
                                If Not IsNothing(Actor.Element("Name")) AndAlso Not String.IsNullOrEmpty(Actor.Element("Name").Value) Then
                                    Actors.Add(New MediaContainers.Person With {.Name = Actor.Element("Name").Value, .Role = Actor.Element("Role").Value, .Thumb = If(IsNothing(Actor.Element("Image")) OrElse String.IsNullOrEmpty(Actor.Element("Image").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, Actor.Element("Image").Value))})
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try

                'now let's get the show info and all the episodes
                Try
                    If Not String.IsNullOrEmpty(sXML) Then
                        Dim xdShow As XDocument = XDocument.Parse(sXML)
                        Dim xS = From xShow In xdShow.Descendants("Series")
                        If xS.Count > 0 Then
                            tShow.ShowLanguage = sInfo.ShowLang
                            If Not IsNothing(tShow.TVShow) Then
                                With tShow.TVShow
                                    sID = xS(0).Element("id").Value
                                    .ID = sID
                                    If sInfo.Options.bShowTitle AndAlso (String.IsNullOrEmpty(.Title) OrElse Not Master.eSettings.TVLockShowTitle) Then .Title = If(IsNothing(xS(0).Element("SeriesName")), .Title, xS(0).Element("SeriesName").Value)
                                    If sInfo.Options.bShowEpisodeGuide Then .EpisodeGuide.URL = If(Not String.IsNullOrEmpty(clsAdvancedSettings.GetSetting("TVDBAPIKey", "")), String.Format("http://{0}/api/{1}/series/{2}/all/{3}.zip", _TVDBMirror, clsAdvancedSettings.GetSetting("TVDBAPIKey", ""), sID, clsAdvancedSettings.GetSetting("TVDBLanguage", "en")), String.Empty)
                                    If sInfo.Options.bShowGenre AndAlso (String.IsNullOrEmpty(.Genre) OrElse Not Master.eSettings.TVLockShowGenre) Then .Genre = If(IsNothing(xS(0).Element("Genre")), .Genre, Strings.Join(xS(0).Element("Genre").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|")), " / "))
                                    If sInfo.Options.bShowMPAA Then .MPAA = If(IsNothing(xS(0).Element("ContentRating")), .MPAA, xS(0).Element("ContentRating").Value)
                                    If sInfo.Options.bShowPlot AndAlso (String.IsNullOrEmpty(.Plot) OrElse Not Master.eSettings.TVLockShowPlot) Then .Plot = If(IsNothing(xS(0).Element("Overview")), .Plot, xS(0).Element("Overview").Value.ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf))
                                    If sInfo.Options.bShowPremiered Then .Premiered = If(IsNothing(xS(0).Element("FirstAired")), .Premiered, xS(0).Element("FirstAired").Value)
                                    If sInfo.Options.bShowRating AndAlso (String.IsNullOrEmpty(.Rating) OrElse Not Master.eSettings.TVLockShowRating) Then .Rating = If(IsNothing(xS(0).Element("Rating")), .Rating, xS(0).Element("Rating").Value)
                                    If sInfo.Options.bShowRuntime AndAlso (String.IsNullOrEmpty(.Runtime) OrElse Not Master.eSettings.TVLockShowRuntime) Then .Runtime = If(IsNothing(xS(0).Element("Runtime")), .Runtime, xS(0).Element("Runtime").Value)
                                    If sInfo.Options.bShowStatus AndAlso (String.IsNullOrEmpty(.Status) OrElse Not Master.eSettings.TVLockShowStatus) Then .Status = If(IsNothing(xS(0).Element("Status")), .Status, xS(0).Element("Status").Value)
                                    If sInfo.Options.bShowStudio AndAlso (String.IsNullOrEmpty(.Studio) OrElse Not Master.eSettings.TVLockShowStudio) Then .Studio = If(IsNothing(xS(0).Element("Network")), .Studio, xS(0).Element("Network").Value)
                                    If sInfo.Options.bShowVotes AndAlso (String.IsNullOrEmpty(.Votes) OrElse Not Master.eSettings.TVLockShowVotes) Then .Votes = If(IsNothing(xS(0).Element("RatingCount")), .Votes, xS(0).Element("RatingCount").Value)
                                    If sInfo.Options.bShowActors Then .Actors = Actors
                                End With
                            End If

                            'set it back
                            tmpTVDBShow.Show = tShow

                            For Each Episode As Structures.DBTV In tmpTVDBShow.Episodes

                                Episode.ShowLanguage = sInfo.ShowLang

                                iEp = Episode.TVEp.Episode
                                iSeas = Episode.TVEp.Season
                                sTitle = Episode.TVEp.Title
                                byTitle = False
                                tOrdering = Enums.Ordering.Standard

                                If Not IsNothing(tShow.TVShow) Then Episode.TVShow = tShow.TVShow

                                If sInfo.Ordering = Enums.Ordering.DVD Then
                                    'first we need to check if dvd order is specified for every episode in the season
                                    'we'll use the regular season number as an indicator even though there are some cases
                                    'where this will not work (season 1 episode 1 = dvd_season 2 dvd_episode 1) but it
                                    'should work in most cases and is the best solution I could come up with

                                    If xdShow.Descendants("Episode").Where(Function(e) Not IsNothing(e.Element("SeasonNumber")) AndAlso Convert.ToInt32(e.Element("SeasonNumber").Value) = iSeas AndAlso (IsNothing(e.Element("DVD_season")) OrElse String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString) OrElse IsNothing(e.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))).Count = 0 Then
                                        tOrdering = Enums.Ordering.DVD
                                    End If
                                ElseIf sInfo.Ordering = Enums.Ordering.Absolute Then
                                    If xdShow.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("SeasonNumber").Value) > 0 AndAlso (IsNothing(e.Element("absolute_number")) OrElse String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString))).Count = 0 Then
                                        tOrdering = Enums.Ordering.Absolute
                                    End If
                                End If

                                If tOrdering = Enums.Ordering.DVD Then
                                    xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) Not IsNothing(e.Element("DVD_episodenumber")) AndAlso Not String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString) AndAlso Convert.ToInt32(CLng(e.Element("DVD_episodenumber").Value.ToString)) = iEp AndAlso Not IsNothing(e.Element("DVD_season")) AndAlso Not String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString) AndAlso Convert.ToInt32(e.Element("DVD_season").Value) = iSeas)
                                ElseIf tOrdering = Enums.Ordering.Absolute Then
                                    If iSeas = 1 Then
                                        xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) Not IsNothing(e.Element("absolute_number")) AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString) AndAlso Convert.ToInt32(e.Element("absolute_number").Value.ToString) = iEp)
                                    Else
                                        xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) Not IsNothing(e.Element("absolute_number")) AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString) AndAlso Convert.ToInt32(e.Element("EpisodeNumber").Value.ToString) = iEp AndAlso Convert.ToInt32(e.Element("SeasonNumber").Value) = iSeas)
                                    End If
                                Else
                                    xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) Convert.ToInt32(e.Element("EpisodeNumber").Value) = iEp AndAlso Convert.ToInt32(e.Element("SeasonNumber").Value) = iSeas)
                                End If

                                If IsNothing(xE) Then
                                    xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) StringUtils.ComputeLevenshtein(e.Element("EpisodeName").Value, sTitle) < 5)
                                    byTitle = True
                                End If

                                If Not IsNothing(xE) Then
                                    With Episode.TVEp
                                        If sInfo.Options.bEpTitle AndAlso (String.IsNullOrEmpty(.Title) OrElse Not Master.eSettings.TVLockEpisodeTitle) AndAlso Not String.IsNullOrEmpty(xE.Element("EpisodeName").Value) Then .Title = xE.Element("EpisodeName").Value
                                        If byTitle Then
                                            If tOrdering = Enums.Ordering.DVD Then
                                                If sInfo.Options.bEpSeason Then .Season = If(IsNothing(xE.Element("DVD_season")) OrElse String.IsNullOrEmpty(xE.Element("DVD_season").Value), 0, Convert.ToInt32(xE.Element("DVD_season").Value))
                                                If sInfo.Options.bEpEpisode Then .Episode = If(IsNothing(xE.Element("DVD_episodenumber")) OrElse String.IsNullOrEmpty(xE.Element("DVD_episodenumber").Value), 0, Convert.ToInt32(xE.Element("DVD_episodenumber").Value))
                                            ElseIf tOrdering = Enums.Ordering.Absolute Then
                                                If sInfo.Options.bEpSeason Then .Season = 1
                                                If sInfo.Options.bEpEpisode Then .Episode = If(IsNothing(xE.Element("absolute_number")) OrElse String.IsNullOrEmpty(xE.Element("absolute_number").Value), 0, Convert.ToInt32(xE.Element("absolute_number").Value))
                                            Else
                                                If sInfo.Options.bEpSeason Then .Season = If(IsNothing(xE.Element("SeasonNumber")) OrElse String.IsNullOrEmpty(xE.Element("SeasonNumber").Value), 0, Convert.ToInt32(xE.Element("SeasonNumber").Value))
                                                If sInfo.Options.bEpEpisode Then .Episode = If(IsNothing(xE.Element("EpisodeNumber")) OrElse String.IsNullOrEmpty(xE.Element("EpisodeNumber").Value), 0, Convert.ToInt32(xE.Element("EpisodeNumber").Value))
                                            End If
                                        End If
                                        If Not IsNothing(xE.Element("airsafter_season")) AndAlso Not String.IsNullOrEmpty(xE.Element("airsafter_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(xE.Element("airsafter_season").Value) + 1
                                            .DisplayEpisode = 0
                                            .displaySEset = True
                                        End If
                                        If Not IsNothing(xE.Element("airsbefore_season")) AndAlso Not String.IsNullOrEmpty(xE.Element("airsbefore_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(xE.Element("airsbefore_season").Value)
                                            .displaySEset = True
                                        End If
                                        If Not IsNothing(xE.Element("airsbefore_episode")) AndAlso Not String.IsNullOrEmpty(xE.Element("airsbefore_episode").Value) Then
                                            .DisplayEpisode = Convert.ToInt32(CLng(xE.Element("airsbefore_episode").Value)) - 1
                                            .displaySEset = True
                                        End If
                                        If sInfo.Options.bEpAired Then .Aired = If(IsNothing(xE.Element("FirstAired")), .Aired, xE.Element("FirstAired").Value)
                                        If sInfo.Options.bEpRating AndAlso (String.IsNullOrEmpty(.Rating) OrElse Not Master.eSettings.TVLockEpisodeRating) Then .Rating = If(IsNothing(xE.Element("Rating")), .Rating, xE.Element("Rating").Value)
                                        If sInfo.Options.bEpVotes AndAlso (String.IsNullOrEmpty(.Votes) OrElse Not Master.eSettings.TVLockEpisodeVotes) Then .Votes = If(IsNothing(xE.Element("RatingCount")), .Votes, xE.Element("RatingCount").Value)
                                        If sInfo.Options.bEpPlot AndAlso (String.IsNullOrEmpty(.Plot) OrElse Not Master.eSettings.TVLockEpisodePlot) Then .Plot = If(IsNothing(xE.Element("Overview")), .Plot, xE.Element("Overview").Value.ToString.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf))
                                        If sInfo.Options.bEpDirector Then .Director = If(IsNothing(xE.Element("Director")), .Director, Strings.Join(xE.Element("Director").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|")), " / "))
                                        If sInfo.Options.bEpCredits Then .OldCredits = If(IsNothing(xE.Element("Writer")), .OldCredits, Strings.Join(xE.Element("Writer").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|")), " / "))
                                        If sInfo.Options.bEpActors Then .Actors = Actors
                                        .PosterURL = If(IsNothing(xE.Element("filename")) OrElse String.IsNullOrEmpty(xE.Element("filename").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, xE.Element("filename").Value))
                                        .LocalFile = If(IsNothing(xE.Element("filename")) OrElse String.IsNullOrEmpty(xE.Element("filename").Value), String.Empty, Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "episodeposters", Path.DirectorySeparatorChar, xE.Element("filename").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))))
                                    End With
                                End If
                            Next

                        End If
                    End If
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Else
                sID = sInfo.TVDBID
            End If
            'and finally the images
            Try
                If ImagesOnly OrElse Not IsNothing(tShow.TVShow) Then
                    If Not String.IsNullOrEmpty(bXML) Then
                        Dim xdImage As XDocument = XDocument.Parse(bXML)
                        For Each tImage As XElement In xdImage.Descendants("Banner")
                            If (Not IsNothing(tImage.Element("BannerPath")) AndAlso Not String.IsNullOrEmpty(tImage.Element("BannerPath").Value)) AndAlso _
                               (Not CBool(clsAdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) OrElse ((Not IsNothing(tImage.Element("Language")) AndAlso tImage.Element("Language").Value = clsAdvancedSettings.GetSetting("TVDBLanguage", "en")) OrElse _
                               ((IsNothing(tImage.Element("Language")) OrElse tImage.Element("Language").Value = "en") AndAlso CBool(clsAdvancedSettings.GetSetting("AlwaysGetEnglishTVImages", "True"))))) Then
                                Select Case tImage.Element("BannerType").Value
                                    Case "fanart"
                                        tmpTVDBShow.Fanarts.Add(New TVDBFanart With { _
                                                             .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                             .ThumbnailURL = If(IsNothing(tImage.Element("ThumbnailPath")) OrElse String.IsNullOrEmpty(tImage.Element("ThumbnailPath").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("ThumbnailPath").Value)), _
                                                             .Size = If(IsNothing(tImage.Element("BannerType2")) OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), New Size With {.Width = 0, .Height = 0}, StringUtils.StringToSize(tImage.Element("BannerType2").Value)), _
                                                             .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "fanart", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                             .LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "fanart", Path.DirectorySeparatorChar, tImage.Element("ThumbnailPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                             .Language = If(IsNothing(tImage.Element("Language")) OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value)})
                                    Case "poster"
                                        tmpTVDBShow.Posters.Add(New TVDBPoster With { _
                                                              .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                              .Size = If(IsNothing(tImage.Element("BannerType2")) OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), New Size With {.Width = 0, .Height = 0}, StringUtils.StringToSize(tImage.Element("BannerType2").Value)), _
                                                              .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "posters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                              .Language = If(IsNothing(tImage.Element("Language")) OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value)})
                                    Case "season"
                                        If tImage.Element("BannerType2").Value.ToLower = "season" Then
                                            tmpTVDBShow.SeasonPosters.Add(New TVDBSeasonPoster With { _
                                                                    .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                                    .Season = If(IsNothing(tImage.Element("Season")) OrElse String.IsNullOrEmpty(tImage.Element("Season").Value), 0, Convert.ToInt32(tImage.Element("Season").Value)), _
                                                                    .Type = If(IsNothing(tImage.Element("BannerType2")) OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), Enums.TVSeasonPosterType.None, StringToSeasonPosterType(tImage.Element("BannerType2").Value)), _
                                                                    .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                                    .Language = If(IsNothing(tImage.Element("Language")) OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value)})
                                        Else
                                            tmpTVDBShow.SeasonBanners.Add(New TVDBSeasonBanner With { _
                                                                    .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                                    .Season = If(IsNothing(tImage.Element("Season")) OrElse String.IsNullOrEmpty(tImage.Element("Season").Value), 0, Convert.ToInt32(tImage.Element("Season").Value)), _
                                                                    .Type = If(IsNothing(tImage.Element("BannerType2")) OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), Enums.TVSeasonPosterType.None, StringToSeasonPosterType(tImage.Element("BannerType2").Value)), _
                                                                    .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                                    .Language = If(IsNothing(tImage.Element("Language")) OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value)})
                                        End If
                                    Case "series"
                                        tmpTVDBShow.ShowBanners.Add(New TVDBShowBanner With { _
                                                              .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                              .Type = If(IsNothing(tImage.Element("BannerType2")) OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), Enums.TVShowBannerType.None, StringToShowPosterType(tImage.Element("BannerType2").Value)), _
                                                              .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriesposters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                              .Language = If(IsNothing(tImage.Element("Language")) OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value)})
                                End Select
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Function StringToSeasonPosterType(ByVal sType As String) As Enums.TVSeasonPosterType
            Select Case sType.ToLower
                Case "season"
                    Return Enums.TVSeasonPosterType.Poster
                Case "seasonwide"
                    Return Enums.TVSeasonPosterType.Wide
                Case Else
                    Return Enums.TVSeasonPosterType.None
            End Select
        End Function

        Private Function StringToShowPosterType(ByVal sType As String) As Enums.TVShowBannerType
            Select Case sType.ToLower
                Case "blank"
                    Return Enums.TVShowBannerType.Blank
                Case "graphical"
                    Return Enums.TVShowBannerType.Graphical
                Case "text"
                    Return Enums.TVShowBannerType.Text
                Case Else
                    Return Enums.TVShowBannerType.None
            End Select
        End Function

#End Region 'Methods

#Region "Other"

        Private Structure Arguments

            Dim Parameter As Object
            Dim Type As Integer

        End Structure

        Private Structure Results

            Dim Result As Object
            Dim Type As Integer '0 = search, 1 = show download, 2 = load eps, 3 = save

        End Structure

#End Region 'Other

    End Class

    <Serializable()> _
    Public Class TVDBFanart

#Region "Fields"

        Private _image As Images
        Private _localfile As String
        Private _localthumb As String
        Private _size As Size
        Private _thumbnailurl As String
        Private _url As String
        Private _language As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Image() As Images
            Get
                Return Me._image
            End Get
            Set(ByVal value As Images)
                Me._image = value
            End Set
        End Property

        Public Property LocalFile() As String
            Get
                Return Me._localfile
            End Get
            Set(ByVal value As String)
                Me._localfile = value
            End Set
        End Property

        Public Property LocalThumb() As String
            Get
                Return Me._localthumb
            End Get
            Set(ByVal value As String)
                Me._localthumb = value
            End Set
        End Property

        Public Property Size() As Size
            Get
                Return Me._size
            End Get
            Set(ByVal value As Size)
                Me._size = value
            End Set
        End Property

        Public Property ThumbnailURL() As String
            Get
                Return Me._thumbnailurl
            End Get
            Set(ByVal value As String)
                Me._thumbnailurl = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return Me._url
            End Get
            Set(ByVal value As String)
                Me._url = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return Me._language
            End Get
            Set(ByVal value As String)
                Me._language = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._url = String.Empty
            Me._thumbnailurl = String.Empty
            Me._size = New Size
            Me._localfile = String.Empty
            Me._localthumb = String.Empty
            Me._image = New Images
            Me._language = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class TVDBPoster

#Region "Fields"

        Private _image As Images
        Private _localfile As String
        Private _size As Size
        Private _url As String
        Private _language As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Image() As Images
            Get
                Return Me._image
            End Get
            Set(ByVal value As Images)
                Me._image = value
            End Set
        End Property

        Public Property LocalFile() As String
            Get
                Return Me._localfile
            End Get
            Set(ByVal value As String)
                Me._localfile = value
            End Set
        End Property

        Public Property Size() As Size
            Get
                Return Me._size
            End Get
            Set(ByVal value As Size)
                Me._size = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return Me._url
            End Get
            Set(ByVal value As String)
                Me._url = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return Me._language
            End Get
            Set(ByVal value As String)
                Me._language = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._url = String.Empty
            Me._size = New Size
            Me._localfile = String.Empty
            Me._image = New Images
            Me._language = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class TVDBSeasonImage

#Region "Fields"

        Private _fanart As TVDBFanart
        Private _poster As Images
        Private _banner As Images
        Private _landscape As Images
        Private _season As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Fanart() As TVDBFanart
            Get
                Return Me._fanart
            End Get
            Set(ByVal value As TVDBFanart)
                Me._fanart = value
            End Set
        End Property

        Public Property Poster() As Images
            Get
                Return Me._poster
            End Get
            Set(ByVal value As Images)
                Me._poster = value
            End Set
        End Property

        Public Property Banner() As Images
            Get
                Return Me._banner
            End Get
            Set(ByVal value As Images)
                Me._banner = value
            End Set
        End Property

        Public Property Landscape() As Images
            Get
                Return Me._landscape
            End Get
            Set(ByVal value As Images)
                Me._landscape = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._season = -1
            Me._poster = New Images
            Me._banner = New Images
            Me._landscape = New Images
            Me._fanart = New TVDBFanart
        End Sub

#End Region 'Methods

    End Class

    Public Class TVDBSeasonPoster

#Region "Fields"

        Private _image As Images
        Private _localfile As String
        Private _season As Integer
        Private _type As Enums.TVSeasonPosterType
        Private _url As String
        Private _language As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Image() As Images
            Get
                Return Me._image
            End Get
            Set(ByVal value As Images)
                Me._image = value
            End Set
        End Property

        Public Property LocalFile() As String
            Get
                Return Me._localfile
            End Get
            Set(ByVal value As String)
                Me._localfile = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

        Public Property Type() As Enums.TVSeasonPosterType
            Get
                Return Me._type
            End Get
            Set(ByVal value As Enums.TVSeasonPosterType)
                Me._type = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return Me._url
            End Get
            Set(ByVal value As String)
                Me._url = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return Me._language
            End Get
            Set(ByVal value As String)
                Me._language = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._url = String.Empty
            Me._season = 0
            Me._type = Enums.TVSeasonPosterType.None
            Me._localfile = String.Empty
            Me._image = New Images
            Me._language = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Public Class TVDBSeasonBanner

#Region "Fields"

        Private _image As Images
        Private _localfile As String
        Private _season As Integer
        Private _type As Enums.TVSeasonPosterType
        Private _url As String
        Private _language As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Image() As Images
            Get
                Return Me._image
            End Get
            Set(ByVal value As Images)
                Me._image = value
            End Set
        End Property

        Public Property LocalFile() As String
            Get
                Return Me._localfile
            End Get
            Set(ByVal value As String)
                Me._localfile = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

        Public Property Type() As Enums.TVSeasonPosterType
            Get
                Return Me._type
            End Get
            Set(ByVal value As Enums.TVSeasonPosterType)
                Me._type = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return Me._url
            End Get
            Set(ByVal value As String)
                Me._url = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return Me._language
            End Get
            Set(ByVal value As String)
                Me._language = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._url = String.Empty
            Me._season = 0
            Me._type = Enums.TVSeasonPosterType.None
            Me._localfile = String.Empty
            Me._image = New Images
            Me._language = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Public Class TVDBShow

#Region "Fields"

        Private _allseason As Structures.DBTV
        Private _episodes As New List(Of Structures.DBTV)
        Private _fanarts As New List(Of TVDBFanart)
        Private _posters As New List(Of TVDBPoster)
        Private _seasonposters As New List(Of TVDBSeasonPoster)
        Private _seasonbanners As New List(Of TVDBSeasonBanner)
        Private _show As Structures.DBTV
        Private _showbanners As New List(Of TVDBShowBanner)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AllSeason() As Structures.DBTV
            Get
                Return Me._allseason
            End Get
            Set(ByVal value As Structures.DBTV)
                Me._allseason = value
            End Set
        End Property

        Public Property Episodes() As List(Of Structures.DBTV)
            Get
                Return Me._episodes
            End Get
            Set(ByVal value As List(Of Structures.DBTV))
                Me._episodes = value
            End Set
        End Property

        Public Property Fanarts() As List(Of TVDBFanart)
            Get
                Return Me._fanarts
            End Get
            Set(ByVal value As List(Of TVDBFanart))
                Me._fanarts = value
            End Set
        End Property

        Public Property Posters() As List(Of TVDBPoster)
            Get
                Return Me._posters
            End Get
            Set(ByVal value As List(Of TVDBPoster))
                Me._posters = value
            End Set
        End Property

        Public Property SeasonPosters() As List(Of TVDBSeasonPoster)
            Get
                Return Me._seasonposters
            End Get
            Set(ByVal value As List(Of TVDBSeasonPoster))
                Me._seasonposters = value
            End Set
        End Property

        Public Property SeasonBanners() As List(Of TVDBSeasonBanner)
            Get
                Return Me._seasonbanners
            End Get
            Set(ByVal value As List(Of TVDBSeasonBanner))
                Me._seasonbanners = value
            End Set
        End Property

        Public Property Show() As Structures.DBTV
            Get
                Return Me._show
            End Get
            Set(ByVal value As Structures.DBTV)
                Me._show = value
            End Set
        End Property

        Public Property ShowBanners() As List(Of TVDBShowBanner)
            Get
                Return Me._showbanners
            End Get
            Set(ByVal value As List(Of TVDBShowBanner))
                Me._showbanners = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._show = New Structures.DBTV
            Me._allseason = New Structures.DBTV
            Me._episodes = New List(Of Structures.DBTV)
            Me._fanarts = New List(Of TVDBFanart)
            Me._showbanners = New List(Of TVDBShowBanner)
            Me._seasonposters = New List(Of TVDBSeasonPoster)
            Me._seasonbanners = New List(Of TVDBSeasonBanner)
            Me._posters = New List(Of TVDBPoster)
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class TVDBShowBanner

#Region "Fields"

        Private _image As Images
        Private _localfile As String
        Private _type As Enums.TVShowBannerType
        Private _url As String
        Private _language As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Image() As Images
            Get
                Return Me._image
            End Get
            Set(ByVal value As Images)
                Me._image = value
            End Set
        End Property

        Public Property LocalFile() As String
            Get
                Return Me._localfile
            End Get
            Set(ByVal value As String)
                Me._localfile = value
            End Set
        End Property

        Public Property Type() As Enums.TVShowBannerType
            Get
                Return Me._type
            End Get
            Set(ByVal value As Enums.TVShowBannerType)
                Me._type = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return Me._url
            End Get
            Set(ByVal value As String)
                Me._url = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return Me._language
            End Get
            Set(ByVal value As String)
                Me._language = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._url = String.Empty
            Me._type = Enums.TVShowBannerType.None
            Me._localfile = String.Empty
            Me._image = New Images
            Me._language = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Public Class TVSearchResults

#Region "Fields"

        Private _aired As String
        Private _banner As String
        Private _id As Integer
        Private _language As TVDBLanguagesLanguage
        Private _lev As Integer
        Private _name As String
        Private _overview As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Aired() As String
            Get
                Return Me._aired
            End Get
            Set(ByVal value As String)
                Me._aired = value
            End Set
        End Property

        Public Property Banner() As String
            Get
                Return Me._banner
            End Get
            Set(ByVal value As String)
                Me._banner = value
            End Set
        End Property

        Public Property ID() As Integer
            Get
                Return Me._id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property Language() As TVDBLanguagesLanguage
            Get
                Return Me._language
            End Get
            Set(ByVal value As TVDBLanguagesLanguage)
                Me._language = value
            End Set
        End Property

        Public Property Lev() As Integer
            Get
                Return Me._lev
            End Get
            Set(ByVal value As Integer)
                Me._lev = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return Me._name
            End Get
            Set(ByVal value As String)
                Me._name = value
            End Set
        End Property

        Public Property Overview() As String
            Get
                Return Me._overview
            End Get
            Set(ByVal value As String)
                Me._overview = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._id = 0
            Me._name = String.Empty
            Me._aired = String.Empty
            Me._language = New TVDBLanguagesLanguage
            Me._overview = String.Empty
            Me._banner = String.Empty
            Me._lev = 0
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class
